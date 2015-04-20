using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using ArchAngel.Interfaces.NHibernateEnums;
using ArchAngel.NHibernateHelper.EntityExtensions;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.DotNet;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using log4net;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;
using Slyce.Common.StringExtensions;
using Property = ArchAngel.Providers.EntityModel.Model.EntityLayer.Property;
using Utility = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Utility;

namespace ArchAngel.NHibernateHelper
{
	public class EntityLoader
	{
		private readonly IFileController _fileController;
		private readonly XmlSchemaSet mappingSchemaSet;
		private EntityProcessor entityProcessor = new OneToOneEntityProcessor();
		private NHibernateFileVerifier nhibernateFileVerifier;
		private List<string> NHibernateTypeNames = Enum.GetNames(typeof(PropertyImpl.NHibernateTypes)).ToList();

		private static readonly ILog log = LogManager.GetLogger(typeof(EntityLoader));

		public EntityLoader(IFileController fileController)
		{
			_fileController = fileController;
			var assembly = typeof(EntityLoader).Assembly;
			var xsdStream = assembly.GetManifestResourceStream("ArchAngel.NHibernateHelper.supported-nhibernate-mapping.xsd");

			mappingSchemaSet = new XmlSchemaSet();
			mappingSchemaSet.Add(XmlSchema.Read(xsdStream, (s, e) => log.Error(e.Message)));

			nhibernateFileVerifier = new NHibernateFileVerifier();
		}

		private bool CheckAgainstSupportedXsd(List<ValidationEventArgs> mappingErrors, string filename)
		{
			mappingErrors.Clear();
			var settings = new XmlReaderSettings
							{
								ValidationType = ValidationType.Schema,
								Schemas = mappingSchemaSet
							};
			settings.ValidationEventHandler += (sender, e) =>
			{
				mappingErrors.Add(e);
				log.Error(e.Message);
			};

			using (XmlReader reader = XmlReader.Create(Slyce.Common.Utility.RemoveXmlEncodingHeader(filename), settings))
				while (reader.Read()) { }

			return mappingErrors.Count == 0;
		}

		internal List<ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.SchemaData> GetTablesFromHbmFiles(IEnumerable<string> hbmFiles)
		{
			List<ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.SchemaData> tablesToFetch = new List<Providers.EntityModel.Controller.DatabaseLayer.SchemaData>();
			List<hibernatemapping> mappingFiles = new List<hibernatemapping>();
			List<ValidationEventArgs> mappingErrors = new List<ValidationEventArgs>();
			List<NHibernateLoaderException.HbmXmlFile> errorFiles = new List<NHibernateLoaderException.HbmXmlFile>();

			GetMappingFiles(hbmFiles, mappingFiles, mappingErrors, errorFiles);

			if (errorFiles.Count > 0)
				throw new NHibernateLoaderException(string.Format("Unsupported Elements found in HBM file."), errorFiles, mappingErrors);

			foreach (hibernatemapping hm in mappingFiles)
			{
				foreach (ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.@class hClass in hm.Classes())
				{
					if (hClass.@abstract || hClass.table == null)
						continue;

					string schema = string.IsNullOrEmpty(hClass.schema) ? hm.schema : hClass.schema;
					schema = schema.UnBackTick();
					string tableName = hClass.table.UnBackTick();

					Providers.EntityModel.Controller.DatabaseLayer.SchemaData sd = tablesToFetch.SingleOrDefault(s => s.Name == schema);

					if (sd != null)
					{
						if (sd.TableNames.Contains(tableName))
							continue;

						sd.TableNames.Add(tableName);
					}
					else
					{
						List<string> tableNames = new List<string>();
						List<string> viewNames = new List<string>();
						tableNames.Add(tableName);
						tablesToFetch.Add(new Providers.EntityModel.Controller.DatabaseLayer.SchemaData(schema, tableNames, viewNames));
					}
					// Check for association-tables
					foreach (var hSet in hClass.Sets().Where(s => s.ManyToMany() != null))
					{
						key keyNode = hSet.key;

						if (keyNode == null) throw new Exception("Cannot find key node in set " + hSet.name);

						if (hSet.schema != null)
							schema = hSet.schema;
						else if (hClass.schema != null)
							schema = hClass.schema;
						else if (hm.schema != null)
							schema = hm.schema;

						schema = schema.UnBackTick();

						sd = tablesToFetch.SingleOrDefault(s => s.Name == schema);
						tableName = hSet.table.UnBackTick();

						if (sd != null)
						{
							if (!sd.TableNames.Contains(tableName))
								sd.TableNames.Add(tableName);
						}
						else
						{
							List<string> tableNames = new List<string>();
							List<string> viewNames = new List<string>();
							tableNames.Add(tableName);
							tablesToFetch.Add(new Providers.EntityModel.Controller.DatabaseLayer.SchemaData(schema, tableNames, viewNames));
						}
					}
				}
			}
			return tablesToFetch;
		}

		public MappingSet GetEntities(IEnumerable<string> hbmFiles, ParseResults parseResults, IDatabase database)
		{
			EntitySet entities = new EntitySetImpl();
			MappingSet mappingSet = new MappingSetImpl(database, entities);
			Dictionary<Class, ComponentSpecification> existingComponentSpecs = new Dictionary<Class, ComponentSpecification>();

			List<hibernatemapping> mappingFiles = new List<hibernatemapping>();
			List<ValidationEventArgs> mappingErrors = new List<ValidationEventArgs>();
			List<NHibernateLoaderException.HbmXmlFile> errorFiles = new List<NHibernateLoaderException.HbmXmlFile>();

			GetMappingFiles(hbmFiles, mappingFiles, mappingErrors, errorFiles);

			if (errorFiles.Count > 0)
				throw new NHibernateLoaderException(string.Format("Unsupported Elements found in HBM file."), errorFiles, mappingErrors);

			foreach (var hm in mappingFiles)
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("AutoImport", hm.autoimport);
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("DefaultAccess", (TopLevelAccessTypes)Enum.Parse(typeof(TopLevelAccessTypes), hm.defaultaccess));
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("DefaultCascade", (TopLevelCascadeTypes)Enum.Parse(typeof(TopLevelCascadeTypes), hm.defaultcascade.Replace("-", "_"), true));
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("DefaultLazy", hm.defaultlazy);
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("ProjectNamespace", hm.@namespace);

				foreach (var hClass in hm.Classes())
				{
					Entity newEntity;
					string @namespace;
					string name;

					if (IsNameFullyQualified(hClass.name, out @namespace, out name))
					{
						newEntity = new EntityImpl(name);

						string currentNamespace = ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace") == null ? "" : ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString();

						if (string.IsNullOrWhiteSpace(currentNamespace))
							ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("ProjectNamespace", @namespace);
					}
					else
						newEntity = new EntityImpl(hClass.name);

					if (entities.GetEntity(newEntity.Name) != null)
						// This entity has already been added - the existing project probably has both HBM XML and Fluent mappings.
						continue;

					if (hClass.lazySpecified)
						newEntity.SetEntityLazy(hClass.lazy);
					else
						newEntity.SetEntityLazy(hm.defaultlazy);

					newEntity.SetEntitySqlWhereClause(hClass.where);
					newEntity.SetEntityDynamicInsert(hClass.dynamicinsert);
					newEntity.SetEntityDynamicUpdate(hClass.dynamicupdate);
					newEntity.SetEntityMutable(hClass.mutable);
					newEntity.SetEntityOptimisticLock((OptimisticLockModes)Enum.Parse(typeof(OptimisticLockModes), hClass.optimisticlock.ToString(), true));
					newEntity.SetEntityProxy(hClass.proxy);
					newEntity.SetEntitySelectBeforeUpdate(hClass.selectbeforeupdate);
					newEntity.SetEntityBatchSize(hClass.batchsize);

					if (hClass.abstractSpecified)
						newEntity.IsAbstract = hClass.@abstract;

					newEntity.SetEntityPersister(hClass.persister);
					Mapping mapping = null;

					if (!newEntity.IsAbstract)
						mapping = CreateMappingFor(newEntity, hClass, database, hm.schema);

					entities.AddEntity(newEntity);

					if (mapping != null)
						mappingSet.AddMapping(mapping);

					#region Cache
					if (hClass.cache != null)
					{
						newEntity.Cache = new Cache();

						switch (hClass.cache.include)
						{
							case cacheInclude.all:
								newEntity.Cache.Include = Cache.IncludeTypes.All;
								break;
							case cacheInclude.nonlazy:
								newEntity.Cache.Include = Cache.IncludeTypes.Non_Lazy;
								break;
							default:
								throw new NotImplementedException("Unexpected cache include value:" + hClass.cache.include.ToString());
						}
						newEntity.Cache.Region = hClass.cache.region;

						switch (hClass.cache.usage)
						{
							case cacheUsage.nonstrictreadwrite:
								newEntity.Cache.Usage = Cache.UsageTypes.NonStrict_Read_Write;
								break;
							case cacheUsage.@readonly:
								newEntity.Cache.Usage = Cache.UsageTypes.Read_Only;
								break;
							case cacheUsage.readwrite:
								newEntity.Cache.Usage = Cache.UsageTypes.Read_Write;
								break;
							case cacheUsage.transactional:
								newEntity.Cache.Usage = Cache.UsageTypes.Transactional;
								break;
							default:
								throw new NotImplementedException("Unexpected cache usage value:" + hClass.cache.usage.ToString());
						}
					}
					#endregion

					#region Discriminator
					if (hClass.discriminator != null)
					{
						ArchAngel.Providers.EntityModel.Model.EntityLayer.Discriminator d = new Discriminator()
						{
							AllowNull = !hClass.discriminator.notnull,
							ColumnName = hClass.discriminator.column,
							DiscriminatorType = string.IsNullOrWhiteSpace(hClass.discriminator.column) ? ArchAngel.Providers.EntityModel.Model.Enums.DiscriminatorTypes.Formula : ArchAngel.Providers.EntityModel.Model.Enums.DiscriminatorTypes.Column,
							Force = hClass.discriminator.force,
							Formula = hClass.discriminator.formula,
							Insert = hClass.discriminator.insert
						};
						newEntity.Discriminator = d;
						newEntity.DiscriminatorValue = d.DiscriminatorType == Enums.DiscriminatorTypes.Column ? hClass.discriminator.column : hClass.discriminator.formula;

						//string columnName = hClass.discriminator.column;

						//throw new NotImplementedException("TODO: fixup discriminator stuff");
						//Grouping g = new AndGrouping();
						//IColumn column = newEntity.MappedTables().First().Columns.Single(c => c.Name == columnName);
						//ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Operator op = ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Operator.Equal;
						//string discriminatorValue = string.IsNullOrWhiteSpace(hClass.discriminatorvalue) ? "" : hClass.discriminatorvalue;

						//ExpressionValue value = new ExpressionValueImpl(discriminatorValue);

						//if (column != null && op != null && value != null)
						//    g.AddCondition(new ConditionImpl(column, op, value));

						//if (newEntity.Discriminator == null)
						//    newEntity.Discriminator = new DiscriminatorImpl();

						//newEntity.Discriminator.RootGrouping = g;
					}
					#endregion

					///////////////////////////////////////////////////
					//foreach (var hProperty in hClass.Properties())
					//{
					//    var property = CreateProperty(newEntity, mapping, hProperty);
					//    SetPropertyInfoFromParsedCode(property, parseResults, hm.@namespace, mapping.FromTable.Schema, hClass.name);
					//}

					//foreach (var hComponent in hClass.Components())
					//{
					//    ProcessComponent(hComponent, newEntity, mapping.FromTable, existingComponentSpecs, hm.@namespace, parseResults);
					//}
					///////////////////////////////////////////////////
					id hId = hClass.Id();

					if (hId != null)
					{
						var idProperty = CreateProperty(newEntity, mapping, hId);
						string schema = mapping == null ? null : mapping.FromTable.Schema;
						SetPropertyInfoFromParsedCode(idProperty, parseResults, hm.@namespace, schema, hClass.name);
						idProperty.IsKeyProperty = true;

						if (hId.generator == null)
							newEntity.Generator.ClassName = NHibernateHelper.MappingFiles.Version_2_2.GeneratorTypes.assigned.ToString();
						else
						{
							newEntity.Generator.ClassName = hId.generator.@class;

							if (hId.generator.param != null)
								foreach (var param in hId.generator.param)
									newEntity.Generator.Parameters.Add(new EntityGenerator.Parameter(param.name, param.Text[0]));
						}
					}
					else
					{
						compositeid hCompId = hClass.CompositeId();

						if (hCompId != null)
						{
							// Check if this is a component. If so, we need to do some additional processing.
							if (!string.IsNullOrEmpty(hCompId.@class))
							{
								bool keyResult = ProcessComponentKey(hCompId, newEntity, mapping.FromTable, existingComponentSpecs, hm.@namespace, parseResults);

								if (keyResult == false)
								{
									// Fallback to composite key generation, log failure.
									ProcessCompositeKey(hm, hClass, newEntity, mapping, parseResults, hCompId);
									log.ErrorFormat("Could not create a component for composite key {0}", hCompId.name);
								}
							}
							else
							{
								// It is not a component.
								ProcessCompositeKey(hm, hClass, newEntity, mapping, parseResults, hCompId);
							}
						}
					}

					foreach (var hProperty in hClass.Properties())
					{
						var property = CreateProperty(newEntity, mapping, hProperty);
						string schema = mapping == null ? null : mapping.FromTable.Schema;
						SetPropertyInfoFromParsedCode(property, parseResults, hm.@namespace, schema, hClass.name);

						property.SetPropertyFormula(hProperty.formula);
						property.SetPropertyGenerated((ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes)Enum.Parse(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes), hProperty.generated.ToString()));

						if (hProperty.insertSpecified)
							property.SetPropertyInsert(hProperty.insert);

						property.SetPropertyOptimisticLock(hProperty.optimisticlock);

						if (hProperty.updateSpecified)
							property.SetPropertyUpdate(hProperty.update);
					}

					foreach (var hComponent in hClass.Components())
						ProcessComponent(hComponent, newEntity, mapping.FromTable, existingComponentSpecs, hm.@namespace, parseResults);

					if (hClass.Version() != null)
					{
						// Create a property from the version node.
						ProcessVersionProperty(hm, hClass, newEntity, mapping, parseResults);
					}
					ProcessSubclasses(mappingSet, database, entities, newEntity, hClass.SubClasses(), hClass.schema.UnBackTick(), hClass.table.UnBackTick(), parseResults, hm.@namespace);
					ProcessJoinedSubclasses(mappingSet, database, entities, newEntity, hClass.JoinedSubClasses(), parseResults, hm.@namespace);
					ProcessUnionSubclasses(hClass, mappingSet, database, entities, newEntity, hClass.UnionSubClasses(), parseResults, hm.@namespace);
				}
			}
			List<string> processedClassNames = new List<string>();

			// Second pass, to add missing foreign key columns and properties
			foreach (var hm in mappingFiles)
			{
				foreach (var hClass in hm.Classes())
				{
					string className;

					if (!IsNameFullyQualified(hClass.name, out className))
						className = hClass.name;

					if (processedClassNames.Contains(className))
						continue;

					processedClassNames.Add(className);

					foreach (var hManyToOne in hClass.ManyToOnes())
					{
						var fkColumnNames = ReferenceLoader.GetColumnNames(hManyToOne.column, hManyToOne.Columns()).ToList();
						Entity entity = entities.Entities.Single(e => e.Name == className);
						bool found = false;

						foreach (var prop in entity.Properties)
						{
							IColumn mappedColumn = prop.MappedColumn();

							if (mappedColumn != null)
							{
								string mappedColumnName = null;

								if (hManyToOne.column != null)
									mappedColumnName = hManyToOne.column;
								else if (hManyToOne.Columns().Count > 0)
									mappedColumnName = hManyToOne.Columns()[0].name;

								if (!string.IsNullOrEmpty(mappedColumnName) &&
									mappedColumn.Name == mappedColumnName.UnBackTick())
								{
									found = true;
									break;
								}
							}
							//else
							//{
							//    string gfh2 = string.Format("{0}.{1}", hClass.name, hManyToOne.column1.UnBackTick());
							//    gfh2 = "";
							//    // TODO: we should create a column for this property
							//    Entity referencedEntity = entities.Entities.Single(e => e.Name == hManyToOne.@class);
							//    Property referencedProperty = referencedEntity.Key.Properties.ElementAt(0);
							//    IColumn tempColumn = referencedProperty.MappedColumn();

							//    IColumn newColumn = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.CreateColumnFromProperty(referencedProperty);
							//    newColumn.Name = hManyToOne.column1.UnBackTick();
							//    ITable table = entity.MappedTables().First();
							//    table.AddColumn(newColumn);
							//    prop.SetMappedColumn(newColumn);
							//}
						}
						if (!found)
						{
							string name;

							string manyToOneClassName = hManyToOne.@class != null ? hManyToOne.@class : hManyToOne.name;
							IsNameFullyQualified(manyToOneClassName, out name);
							Entity referencedEntity = entities.Entities.Single(e => e.Name == name);
							Property referencedProperty = referencedEntity.Key.Properties.ElementAt(0);
							IColumn newColumn = null;

							foreach (Table t in entity.MappedTables())
							{
								string colName = null;

								if (hManyToOne.column != null)
									colName = hManyToOne.column;
								else if (hManyToOne.Columns().Count > 0)
									colName = hManyToOne.Columns()[0].name;

								newColumn = t.Columns.SingleOrDefault(c => !string.IsNullOrEmpty(colName) && c.Name == colName.UnBackTick());

								if (newColumn != null)
									break;
							}
							if (newColumn == null)
							{
								newColumn = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.CreateColumnFromProperty(referencedProperty);
								ITable table = entity.MappedTables().First();
								table.AddColumn(newColumn);
							}
							Property newProperty = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.CreatePropertyFromColumn(newColumn);
							//entity.Properties.SingleOrDefault(p => p.MappedColumn() == 
							entity.AddProperty(newProperty);
						}
						//ITable table = entity.MappedTables().First();
					}
				}
			}

			// Second pass, for references.
			var refProcessor = new ReferenceLoader();
			refProcessor.ProcessReferences(mappingFiles, mappingSet);

			processedClassNames.Clear();
			// Second pass, to add missing foreign key columns and properties
			foreach (var hm in mappingFiles)
			{
				foreach (var hClass in hm.Classes())
				{
					string className;
					IsNameFullyQualified(hClass.name, out className);

					if (processedClassNames.Contains(className))
						continue;

					processedClassNames.Add(className);

					foreach (var hManyToOne in hClass.ManyToOnes())
					{
						var fkColumnNames = ReferenceLoader.GetColumnNames(hManyToOne.column, hManyToOne.Columns()).ToList();
						Entity entity = entities.Entities.Single(e => e.Name == className);

						foreach (var prop in entity.Properties)
						{
							IColumn mappedColumn = prop.MappedColumn();

							if (mappedColumn == null)
							{
								// TODO: we should create a column for this property
								string manyToOneClassName;
								IsNameFullyQualified(hManyToOne.@class, out manyToOneClassName);
								Entity referencedEntity = entities.Entities.Single(e => e.Name == manyToOneClassName);
								Property referencedProperty = referencedEntity.Key.Properties.ElementAt(0);
								IColumn tempColumn = referencedProperty.MappedColumn();

								IColumn newColumn = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.CreateColumnFromProperty(referencedProperty);

								string newColName = "";

								if (hManyToOne.column != null)
									newColName = hManyToOne.column;
								else if (hManyToOne.Columns().Count > 0)
									newColName = hManyToOne.Columns()[0].name;

								newColumn.Name = newColName.UnBackTick();
								ITable table = entity.MappedTables().First();

								if (table.Columns.Count(c => c.Name == newColumn.Name) == 0)
									table.AddColumn(newColumn);
								else
									newColumn = table.Columns.First(c => c.Name == newColumn.Name);

								prop.SetMappedColumn(newColumn);
							}
						}
					}
				}
			}
			return mappingSet;
		}

		private void GetMappingFiles(IEnumerable<string> hbmFiles, List<hibernatemapping> mappingFiles, List<ValidationEventArgs> mappingErrors, List<NHibernateLoaderException.HbmXmlFile> errorFiles)
		{
			foreach (var filename in hbmFiles)
			{
				if (!CheckAgainstSupportedXsd(mappingErrors, filename))
				{
					// Create an inner exception to hold the contents of the XML file, so that we can inspect it.
					//Exception e = new Exception();
					string xmlBody = "";
					string elements = "";

					try
					{
						foreach (var err in mappingErrors)
							elements += err.Message + ",";

						xmlBody = xmlBody.TrimEnd(',');
						xmlBody += File.ReadAllText(filename);
					}
					catch
					{
						// Do nothing
					}
					elements = elements.TrimEnd(',');
					errorFiles.Add(new NHibernateLoaderException.HbmXmlFile(filename, elements, xmlBody));
					//throw new NHibernateLoaderException(filename, string.Format("Unsupported Elements found in HBM file: {0}", elements, xmlBody), xmlBody, mappingErrors);
				}
				var hm = Utility.Open(filename);
				mappingFiles.Add(hm);
			}
		}

		internal static bool IsNameFullyQualified(string fullName, out string className)
		{
			string ns;
			return IsNameFullyQualified(fullName, out ns, out className);
		}

		internal static bool IsNameFullyQualified(string fullName, out string @namespace, out string className)
		{
			if (fullName.IndexOf(",") > 0)
			{
				className = fullName.Substring(0, fullName.IndexOf(","));

				if (className.LastIndexOf(".") > 0)
				{
					@namespace = className.Substring(0, className.LastIndexOf("."));
					className = className.Substring(className.LastIndexOf(".") + 1);
				}
				else
					@namespace = "";

				return true;
			}
			className = fullName;
			@namespace = "";
			return false;
		}

		private void ProcessVersionProperty(hibernatemapping hm, @class hClass, Entity newEntity, Mapping mapping, ParseResults parseResults)
		{
			version version = hClass.Version();
			var typeName = string.IsNullOrEmpty(version.type) ? "int" : version.type;
			var columnName = string.IsNullOrEmpty(version.column1) ? version.name : version.column1;
			var property = CreateProperty(newEntity, mapping, typeName, version.name, columnName, "");
			property.SetPropertyIsVersion(true);
			SetPropertyInfoFromParsedCode(property, parseResults, hm.@namespace, mapping.FromTable.Schema, hClass.name);
		}

		private void ProcessComponent(component hComponent, Entity newEntity, ITable mappedTable, Dictionary<Class, ComponentSpecification> specifications, string hmNamespace, ParseResults parseResults)
		{
			if (hComponent.@class == null)
			{
				log.ErrorFormat("Could not load component named {0} on class {1} because it does not have a class attribute.", hComponent.name, newEntity.Name);
				return;
			}

			var possibleClasses = GetPossibleClasses(hComponent.@class, hmNamespace, mappedTable.Schema, parseResults);

			if (possibleClasses.Count == 0)
			{
				log.ErrorFormat("Could not load component named {0} on class {1} because we could not find the class named {2}.", hComponent.name, newEntity.Name, hComponent.@class);
				return;
			}
			ComponentSpecification spec = null;

			foreach (var possibleClass in possibleClasses)
			{
				spec = specifications.GetValueOrDefault(possibleClass);

				if (spec != null)
					break;
			}

			bool createProperties = false;

			if (spec == null)
			{
				// Create a new spec from these.
				spec = new ComponentSpecificationImpl(GetShortClassName(hComponent.@class));
				newEntity.EntitySet.AddComponentSpecification(spec);
				createProperties = true;
			}
			Component component = spec.CreateImplementedComponentFor(newEntity, hComponent.name);
			newEntity.Key.Component = component;

			var mapping = new ComponentMappingImpl();

			foreach (var prop in hComponent.Properties())
			{
				if (createProperties)
				{
					ComponentProperty idProperty = new ComponentPropertyImpl(prop.name);
					idProperty.Type = prop.type1;
					idProperty.ValidationOptions.MaximumLength = prop.length.As<int>();
					SetPropertyInfoFromParsedCode(possibleClasses, idProperty);

					spec.AddProperty(idProperty);
				}

				var compProperty = component.GetProperty(prop.name);
				var column = mappedTable.GetColumn(prop.column.UnBackTick());
				if (column == null)
				{
					// Create the column
					column = entityProcessor.CreateColumn(compProperty.RepresentedProperty);
					mapping.FromTable.AddColumn(column);
				}

				mapping.AddPropertyAndColumn(compProperty, column);
			}
			newEntity.EntitySet.MappingSet.AddMapping(mapping);
		}

		private void ProcessCompositeKey(hibernatemapping hm, @class hClass, Entity newEntity, Mapping mapping, ParseResults parseResults, compositeid hCompId)
		{
			foreach (var prop in hCompId.KeyProperties())
			{
				var idProperty = CreateProperty(newEntity, mapping, prop);
				SetPropertyInfoFromParsedCode(idProperty, parseResults, hm.@namespace, mapping.FromTable.Schema, hClass.name);
				idProperty.IsKeyProperty = true;
			}
		}

		private bool ProcessComponentKey(compositeid hCompId, Entity newEntity, ITable mappedTable, Dictionary<Class, ComponentSpecification> specifications, string hmNamespace, ParseResults parseResults)
		{
			var possibleClasses = GetPossibleClasses(hCompId.@class, hmNamespace, mappedTable.Schema, parseResults);

			if (possibleClasses.Count == 0) return false;

			ComponentSpecification spec = null;

			foreach (var possibleClass in possibleClasses)
			{
				spec = specifications.GetValueOrDefault(possibleClass);

				if (spec != null)
					break;
			}
			bool createProperties = false;

			if (spec == null)
			{
				// Create a new spec from these.
				spec = new ComponentSpecificationImpl(GetShortClassName(hCompId.@class));
				newEntity.EntitySet.AddComponentSpecification(spec);
				createProperties = true;
			}

			Component component = spec.CreateImplementedComponentFor(newEntity, hCompId.name);
			newEntity.Key.Component = component;

			var mapping = new ComponentMappingImpl();

			foreach (var prop in hCompId.KeyProperties())
			{
				if (createProperties)
				{

					ComponentProperty idProperty = new ComponentPropertyImpl(prop.name);
					idProperty.Type = prop.type1;
					idProperty.ValidationOptions.MaximumLength = prop.length.As<int>();
					SetPropertyInfoFromParsedCode(possibleClasses, idProperty);

					spec.AddProperty(idProperty);
				}

				var compProperty = component.GetProperty(prop.name);
				var column = mappedTable.GetColumn(prop.column1.UnBackTick());
				if (column == null)
				{
					// Create the column
					column = entityProcessor.CreateColumn(compProperty.RepresentedProperty);
					mapping.FromTable.AddColumn(column);
				}

				mapping.AddPropertyAndColumn(compProperty, column);
			}
			newEntity.EntitySet.MappingSet.AddMapping(mapping);

			return true;
		}

		private static void SetPropertyInfoFromParsedCode(Property property, ParseResults results, string @namespace, string tableSchema, string className)
		{
			if (string.IsNullOrEmpty(className)) return;

			List<Class> possibleClasses = GetPossibleClasses(className, @namespace, tableSchema, results);

			if (possibleClasses.Count == 0) return;

			// Attempt to find property
			SetPropertyInfoFromParsedCode(possibleClasses, property);
		}

		private static void SetPropertyInfoFromParsedCode(List<Class> possibleClasses, Property property)
		{
			foreach (var possibleClass in possibleClasses)
			{
				var possibleProperty = possibleClass.Properties.FirstOrDefault(p => p.Name == property.Name);

				if (possibleProperty != null)
				{
					// We have successfully found the property. Get the information we need from it.
					property.Type = possibleProperty.DataType.ToString();
				}
			}
		}

		private static void SetPropertyInfoFromParsedCode(List<Class> possibleClasses, ComponentProperty property)
		{
			foreach (var possibleClass in possibleClasses)
			{
				var possibleProperty = possibleClass.Properties.FirstOrDefault(p => p.Name == property.Name);

				if (possibleProperty != null)
				{
					// We have successfully found the property. Get the information we need from it.
					property.Type = possibleProperty.DataType.ToString();
				}
			}
		}

		private static string GetShortClassName(string className)
		{
			if (className.Contains(","))
			{
				className = className.Substring(0, className.IndexOf(","));
			}

			if (className.Contains("."))
			{
				className = className.Substring(className.LastIndexOf(".") + 1);
			}

			return className;
		}

		private static List<Class> GetPossibleClasses(string className, string @namespace, string tableSchema, ParseResults results)
		{
			if (className.Contains(","))
				className = className.Substring(0, className.IndexOf(","));

			if (className.Contains("."))
			{
				@namespace = className.Substring(0, className.LastIndexOf("."));
				className = className.Substring(className.LastIndexOf(".") + 1);
			}
			//if (string.IsNullOrEmpty(@namespace))
			//    return null;

			// Attempt to find namespace
			var possibleClasses = results.GetClassesInNamespace(@namespace);

			if (possibleClasses.Count() == 0)
				possibleClasses = results.GetClassesInNamespace(string.Format("{0}.{1}", @namespace, tableSchema));

			//if (possibleClasses.Count() == 0)
			//{
			//    var bruteCheckClasses = results.GetAllClasses();

			//    if (bruteCheckClasses.Where(c => c.Name == className).Count() == 1)
			//        possibleClasses = bruteCheckClasses;
			//}
			// Attempt to find class
			List<Class> possibles = possibleClasses.Where(c => c.Name == className).ToList();
			return possibles;
		}

		public MappingSet GetEntities(IEnumerable<string> hbmFiles, IDatabase database)
		{
			return GetEntities(hbmFiles, new ParseResults(), database);
		}

		private void ProcessSubclasses(MappingSet mappingSet, IDatabase database, EntitySet entities, Entity parentEntity, IEnumerable<subclass> subclasses, string schemaName, string tableName, ParseResults parseResults, string unqualifiedNamespace)
		{
			if (subclasses == null) return;

			foreach (subclass hSubclass in subclasses)
			{
				Entity childEntity;

				string @namespace;
				string name;

				if (IsNameFullyQualified(hSubclass.name, out @namespace, out name))
				{
					childEntity = new EntityImpl(name);

					string currentNamespace = ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace") == null ? "" : ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString();

					if (string.IsNullOrWhiteSpace(currentNamespace))
						ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("ProjectNamespace", @namespace);
				}
				else
					childEntity = new EntityImpl(hSubclass.name);

				childEntity.SetEntityLazy(hSubclass.lazy);

				#region Discriminator
				if (!string.IsNullOrWhiteSpace(hSubclass.discriminatorvalue))
				{
					childEntity.DiscriminatorValue = hSubclass.discriminatorvalue;

					//ArchAngel.Providers.EntityModel.Model.EntityLayer.Discriminator d = new Discriminator()
					//{
					//    AllowNull = xx,
					//    ColumnName = hSubclass.d,
					//    DiscriminatorType = xx,
					//    Force = xx,
					//    Formula = xx,
					//    Insert = xx
					//};
					//throw new NotImplementedException("TODO: fixup discriminator stuff");
					//Grouping g = new AndGrouping();
					//IColumn column = parentEntity.Discriminator.RootGrouping.Conditions[0].Column;
					//ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Operator op = ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Operator.Equal;
					//ExpressionValue value = new ExpressionValueImpl(hSubclass.discriminatorvalue);

					//if (column != null && op != null && value != null)
					//    g.AddCondition(new ConditionImpl(column, op, value));

					//if (childEntity.Discriminator == null)
					//    childEntity.Discriminator = new DiscriminatorImpl();

					//childEntity.Discriminator.RootGrouping = g;
				}
				#endregion

				parentEntity.AddChild(childEntity);

				var childTableMapping = new MappingImpl();

				childTableMapping.FromTable = database.GetTable(tableName, schemaName);
				childTableMapping.ToEntity = childEntity;

				foreach (var hProperty in hSubclass.Properties())
				{
					var property = CreateProperty(childEntity, childTableMapping, hProperty);
					SetPropertyInfoFromParsedCode(property, parseResults, unqualifiedNamespace, childTableMapping.FromTable.Schema, hSubclass.name);
				}

				entities.AddEntity(childEntity);
				mappingSet.AddMapping(childTableMapping);

				ProcessSubclasses(mappingSet, database, entities, childEntity, hSubclass.subclass1, schemaName, tableName, parseResults, unqualifiedNamespace);
			}
		}

		private void ProcessJoinedSubclasses(MappingSet mappingSet, IDatabase database, EntitySet entities, Entity newEntity, IEnumerable<joinedsubclass> joinedsubclasses, ParseResults results, string unqualifiedNamespace)
		{
			if (joinedsubclasses == null) return;

			ITable parentTable = null;

			if (newEntity.MappedTables().Count() > 0)
				parentTable = newEntity.MappedTables().ElementAt(0);

			foreach (joinedsubclass hSubclass in joinedsubclasses)
			{
				Entity childEntity;
				string @namespace;
				string name;

				if (IsNameFullyQualified(hSubclass.name, out @namespace, out name))
				{
					childEntity = new EntityImpl(name);

					string currentNamespace = ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace") == null ? "" : ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString();

					if (string.IsNullOrWhiteSpace(currentNamespace))
						ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("ProjectNamespace", @namespace);

					unqualifiedNamespace = @namespace;
				}
				else
					childEntity = new EntityImpl(hSubclass.name);

				childEntity.SetEntityLazy(hSubclass.lazy);
				newEntity.AddChild(childEntity);

				var childTableMapping = new MappingImpl();
				ITable subClassTable;

				if (!string.IsNullOrEmpty(hSubclass.table))
				{
					string schema = "";

					if (!string.IsNullOrEmpty(hSubclass.schema))
						schema = hSubclass.schema;
					else if (parentTable != null)
						schema = parentTable.Schema;

					subClassTable = database.GetTable(hSubclass.table.UnBackTick(), schema.UnBackTick());
					subClassTable.Database = database;
				}
				else
					subClassTable = parentTable;

				childTableMapping.FromTable = subClassTable;
				childTableMapping.ToEntity = childEntity;

				foreach (var hProperty in hSubclass.Properties())
				{
					var property = CreateProperty(childEntity, childTableMapping, hProperty);
					SetPropertyInfoFromParsedCode(property, results, unqualifiedNamespace, childTableMapping.FromTable.Schema, hSubclass.name);
				}
				if (hSubclass.key != null)
				{
					string keyColumnName;

					if (!string.IsNullOrEmpty(hSubclass.key.column1))
						keyColumnName = hSubclass.key.column1;
					else //if (hSubclass.key.column != null && hSubclass.key.column.Count() > 0)
						keyColumnName = hSubclass.key.column[0].name;

					Property keyProp = childEntity.Properties.FirstOrDefault(p => p.MappedColumn() != null && p.MappedColumn().Name == keyColumnName);

					if (keyProp == null)
					{
						keyProp = CreateProperty(childEntity, childTableMapping, hSubclass.key, subClassTable);
						SetPropertyInfoFromParsedCode(keyProp, results, unqualifiedNamespace, childTableMapping.FromTable.Schema, childEntity.Name);
						keyProp.IsHiddenByAbstractParent = true;
					}
					keyProp.IsKeyProperty = true;
				}
				entities.AddEntity(childEntity);
				mappingSet.AddMapping(childTableMapping);

				ProcessJoinedSubclasses(mappingSet, database, entities, childEntity, hSubclass.joinedsubclass1, results, unqualifiedNamespace);
			}
		}

		private void ProcessUnionSubclasses(object parent, MappingSet mappingSet, IDatabase database, EntitySet entities, Entity newEntity, IEnumerable<unionsubclass> unionsubclasses, ParseResults results, string unqualifiedNamespace)
		{
			if (unionsubclasses == null) return;

			@class hClass = null;
			unionsubclass parentSubClass = null;

			if (parent is @class)
				hClass = (@class)parent;
			else
				parentSubClass = (unionsubclass)parent;

			foreach (unionsubclass hSubclass in unionsubclasses)
			{
				Entity childEntity;

				string @namespace;
				string name;

				if (IsNameFullyQualified(hSubclass.name, out @namespace, out name))
				{
					childEntity = new EntityImpl(name);

					string currentNamespace = ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace") == null ? "" : ArchAngel.Interfaces.SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString();

					if (string.IsNullOrWhiteSpace(currentNamespace))
						ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("ProjectNamespace", @namespace);
				}
				else
					childEntity = new EntityImpl(hSubclass.name);

				childEntity.SetEntityLazy(hSubclass.lazy);
				newEntity.AddChild(childEntity);

				var childTableMapping = new MappingImpl();
				childTableMapping.FromTable = database.GetTable(hSubclass.table.UnBackTick(), hSubclass.schema.UnBackTick());
				childTableMapping.ToEntity = childEntity;

				foreach (var ip in childEntity.InheritedProperties)
				{
					PropertyImpl np = new PropertyImpl(ip);
					np.IsHiddenByAbstractParent = true;
					childEntity.AddProperty(np);
					string columnName = "";
					var props = hClass.Properties().ToList();

					if (hClass != null)
					{
						property matchingProp = hClass.Properties().SingleOrDefault(p => p.name == ip.Name);

						if (matchingProp != null)
						{
							if (matchingProp.column != null)
								columnName = matchingProp.column;
							else if (matchingProp.Columns().Count > 0)
								columnName = matchingProp.Columns()[0].name;
						}
						else
						{
							if (hClass.Id().column1 != null)
								columnName = hClass.Id().column1;
							else if (hClass.Id().column != null && hClass.Id().column.Count() > 0)
								columnName = hClass.Id().column[0].name;
						}
					}
					else
						columnName = parentSubClass.Properties().Single(p => p.name == ip.Name).column;

					IColumn column = childTableMapping.FromTable.GetColumn(columnName.UnBackTick());

					if (column != null)
						childTableMapping.AddPropertyAndColumn(np, column);
				}

				foreach (var hProperty in hSubclass.Properties())
				{
					var property = CreateProperty(childEntity, childTableMapping, hProperty);
					SetPropertyInfoFromParsedCode(property, results, unqualifiedNamespace, childTableMapping.FromTable.Schema, hSubclass.name);
				}
				// Add the parent's key properties as hidden properties
				foreach (var keyProp in newEntity.Key.Properties)
				{
					Property childP = childEntity.PropertiesHiddenByAbstractParent.SingleOrDefault(p => p.Name == keyProp.Name);

					if (childP != null)
					{
						//childP.IsHiddenByAbstractParent = true;
						childP.IsKeyProperty = true;
					}
				}

				entities.AddEntity(childEntity);
				mappingSet.AddMapping(childTableMapping);

				ProcessUnionSubclasses(hSubclass, mappingSet, database, entities, childEntity, hSubclass.unionsubclass1, results, unqualifiedNamespace);
			}
		}

		private Property CreateProperty(Entity newEntity, Mapping mapping, id hId)
		{
			if (hId.column1 != null)
				return CreateProperty(newEntity, mapping, hId.type1, hId.name, hId.column1, hId.length);
			else
				return CreateProperty(newEntity, mapping, hId.type1, hId.name, hId.column[0].name, hId.length);
		}

		private Property CreateProperty(Entity newEntity, Mapping mapping, keyproperty hProperty)
		{
			if (hProperty.column1 != null)
				return CreateProperty(newEntity, mapping, hProperty.type1, hProperty.name, hProperty.column1, hProperty.length);
			else
				return CreateProperty(newEntity, mapping, hProperty.type1, hProperty.name, hProperty.column[0].name, hProperty.length);
		}

		/// <summary>
		/// Creates a property for an key in a joined subclass in a Fluent project.
		/// </summary>
		/// <param name="newEntity"></param>
		/// <param name="mapping"></param>
		/// <param name="hKey"></param>
		/// <returns></returns>
		private Property CreateProperty(Entity newEntity, Mapping mapping, key hKey, ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable table)
		{
			Property prop;
			//mapping.
			string columnName;

			if (!string.IsNullOrEmpty(hKey.column1))
				columnName = hKey.column1.UnBackTick();
			else
				columnName = hKey.column[0].name.UnBackTick();

			ArchAngel.Providers.EntityModel.Model.EntityLayer.Property existingProperty = newEntity.Properties.SingleOrDefault(p => p.MappedColumn() != null && p.MappedColumn().Name.ToLowerInvariant() == columnName.ToLowerInvariant());

			if (existingProperty != null)
			{
				existingProperty.IsKeyProperty = true;
				return existingProperty;
			}

			//if (hKey.column1 != null)
			//    prop = CreateProperty(newEntity, mapping, hKey.type, hKey.name, hKey.column1, hKey.length);
			//else
			//    prop = CreateProperty(newEntity, mapping, hKey.type, hKey.name, hKey.column[0].name, hKey.length);

			//if (hKey.column1 != null)
			//    prop = CreateProperty(newEntity, mapping, hKey.type, hKey.name, hKey.column1, hKey.length);
			//else

			//string csharpType = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetCSharpTypeName(newEntity.MappedTables().ElementAt(0).Database.DatabaseType.ToString(), hKey.column[0].sqltype);
			ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn column = table.Columns.SingleOrDefault(c => c.Name.ToLowerInvariant() == columnName.ToLowerInvariant().UnBackTick());
			ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.ColumnInfo columnInfo = new Interfaces.ProjectOptions.TypeMappings.Utility.ColumnInfo();

			string type;
			bool nullability;

			if (column != null)
			{
				type = column.OriginalDataType;
				nullability = column.IsNullable;
				columnInfo.Precision = column.Precision;
				columnInfo.Scale = column.Scale;
				columnInfo.Size = column.Size;
			}
			else
			{
				type = hKey.column[0].sqltype;

				if (hKey.column[0].notnullSpecified)
					nullability = !hKey.column[0].notnull;
				else
					nullability = false;
			}
			columnInfo.IsNullable = nullability;
			columnInfo.Name = columnName;
			columnInfo.TypeName = type;

			string csharpType = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetCSharpTypeName(table.Database.DatabaseType.ToString(), columnInfo);
			prop = CreateProperty(newEntity, mapping, csharpType, columnName, columnName, "0");
			return prop;
		}

		private Property CreateProperty(Entity newEntity, Mapping mapping, property hProperty)
		{
			if (hProperty.column != null)
				return CreateProperty(newEntity, mapping, hProperty.type1, hProperty.name, hProperty.column, hProperty.length);
			else
			{
				var columns = hProperty.Columns();
				var columnName = columns.Count > 0 ? hProperty.Columns()[0].name : hProperty.name;
				return CreateProperty(newEntity, mapping, hProperty.type1, hProperty.name, columnName, hProperty.length);
			}
		}

		private Property CreateProperty(Entity newEntity, Mapping mapping, string typeName, string propertyName, string columnName, string length)
		{
			Property idProperty = new PropertyImpl(propertyName);

			if (typeName != null)
			{
				if (typeName.IndexOf(",") > 0)
					idProperty.Type = typeName.Substring(0, typeName.IndexOf(","));
				else
				{
					if (NHibernateTypeNames.Contains(typeName))
						idProperty.NHibernateType = typeName;
					else
						idProperty.Type = typeName;
				}
			}

			idProperty.ValidationOptions.MaximumLength = length.As<int>();

			newEntity.AddProperty(idProperty);

			if (mapping != null)
			{
				var column = mapping.FromTable.GetColumn(columnName.UnBackTick());

				if (column == null)
				{
					// Create the column
					column = entityProcessor.CreateColumn(idProperty);
					mapping.FromTable.AddColumn(column);
				}
				mapping.AddPropertyAndColumn(idProperty, column);
			}
			return idProperty;
		}

		private Mapping CreateMappingFor(Entity entity, @class hEntity, IDatabase database, string defaultSchema)
		{
			if (hEntity.table == null)
				return null;

			Mapping mapping = new MappingImpl();
			string schema = string.IsNullOrEmpty(hEntity.schema) ? defaultSchema : hEntity.schema;
			var table = database.GetTable(hEntity.table.UnBackTick(), schema.UnBackTick());

			if (table == null)
			{
				// create the table
				table = entityProcessor.CreateTable(entity);
				database.AddTable(table);
			}

			mapping.FromTable = table;
			mapping.ToEntity = entity;

			return mapping;
		}

		private const string NHIBERNATE_VALIDATOR_NAMESPACE = "urn:nhibernate-validator-1.0";

		public void ApplyConstraints(MappingSet set, IEnumerable<string> nhvFiles, ParseResults parseResults)
		{
			foreach (var nhv in nhvFiles)
			{
				string text = _fileController.ReadAllText(nhv);
				if (nhibernateFileVerifier.IsValidValidationMappingFile(new StringReader(text)) == false)
					continue;

				// Load the NHV file
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(text);

				var ns = new XmlNamespaceManager(doc.NameTable);
				ns.AddNamespace("nhv", NHIBERNATE_VALIDATOR_NAMESPACE);
				var className = doc.SelectSingleNode("/nhv:nhv-mapping/nhv:class", ns).GetAttributeValueIfExists("name");
				var shortClassName = GetShortClassName(className);
				var possibleEntity = set.EntitySet.Entities.FirstOrDefault(e => e.Name == shortClassName);

				if (possibleEntity == null)
				{
					log.WarnFormat("Could not find Entity named {1}", className);
					return;
				}
				var propertyNodes = doc.SelectNodes("/nhv:nhv-mapping/nhv:class/nhv:property", ns);

				if (propertyNodes == null)
				{
					log.WarnFormat("Could not find any property nodes in {0}", nhv);
					return;
				}
				foreach (XmlNode node in propertyNodes)
				{
					var proc = new NodeProcessor(node, ns);
					var propertyName = proc.Attributes.GetString("name");
					var property = possibleEntity.Properties.FirstOrDefault(p => p.Name == propertyName);

					if (property == null)
					{
						log.WarnFormat("Could not find property {0} in Entity {1}", propertyName, possibleEntity.Name);
						continue;
					}
					if (proc.Exists("nhv:length"))
					{
						if (proc.SubNode("nhv:length").Attributes.Exists("max"))
							property.ValidationOptions.MaximumLength = proc.SubNode("nhv:length").Attributes.GetInt("max");

						if (proc.SubNode("nhv:length").Attributes.Exists("min"))
							property.ValidationOptions.MinimumLength = proc.SubNode("nhv:length").Attributes.GetInt("min");
					}
					if (proc.Exists("nhv:digits"))
					{
						var subNode = proc.SubNode("nhv:digits");
						var integerDigits = subNode.Attributes.GetInt("integerDigits");
						var fractionalDigits = subNode.Attributes.GetNullableInt("fractionalDigits");
						property.ValidationOptions.IntegerDigits = integerDigits;
						property.ValidationOptions.FractionalDigits = fractionalDigits;
					}
					if (proc.Exists("nhv:not-null"))
						property.ValidationOptions.Nullable = false;
					if (proc.Exists("nhv:not-empty"))
						property.ValidationOptions.NotEmpty = true;
					if (proc.Exists("nhv:future"))
						property.ValidationOptions.FutureDate = true;
					if (proc.Exists("nhv:past"))
						property.ValidationOptions.PastDate = true;
					if (proc.Exists("nhv:email"))
						property.ValidationOptions.Email = true;
					//if (proc.Exists("nhv:size"))
					//    property.ValidationOptions.si = false;
					if (proc.Exists("nhv:min"))
					{
						var subNode = proc.SubNode("nhv:min");
						property.ValidationOptions.MinimumValue = subNode.Attributes.GetInt("value");
					}
					//if (proc.Exists("nhv:range"))
					//    property.ValidationOptions.patt = false;
					//if (proc.Exists("nhv:pattern"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:asserttrue"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:rule"))
					//    property.ValidationOptions.xxx = false;
					if (proc.Exists("nhv:max"))
					{
						var subNode = proc.SubNode("nhv:max");
						property.ValidationOptions.MaximumValue = subNode.Attributes.GetInt("value");
					}
					//if (proc.Exists("nhv:decimalmax"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:decimalmin"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:notnull-notempty"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:ipaddress"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:ean"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:creditcardnumber"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:assertfalse"))
					//    property.ValidationOptions.xxx = false;
					//if (proc.Exists("nhv:fileexists"))
					//    property.ValidationOptions.xxx = false;
					if (proc.Exists("nhv:valid"))
						property.ValidationOptions.Validate = true;
					//if (proc.Exists("nhv:iban"))
					//    property.ValidationOptions..xxx = false;
				}

			}
		}

		private static bool IsStringType(Property property)
		{
			return (property.Type == "string" || property.Type == "System.String" || property.Type == "String");
		}
	}
}
