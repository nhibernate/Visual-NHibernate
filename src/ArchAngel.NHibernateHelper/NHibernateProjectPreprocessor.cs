using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.NHibernateEnums;
using ArchAngel.NHibernateHelper.EntityExtensions;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using log4net;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;

namespace ArchAngel.NHibernateHelper
{
	public class NHibernateProjectPreprocessor
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(NHibernateProjectPreprocessor));

		private readonly IFileController fileController;

		public NHibernateProjectPreprocessor(IFileController fileController)
		{
			this.fileController = fileController;
		}

		public void InitialiseNHibernateProject(ArchAngel.NHibernateHelper.ProviderInfo providerInfo, PreGenerationData data)
		{
			if (providerInfo.ProjectOrigin != ProviderInfo.ProjectType.FromExistingVisualStudioProject)
			{
				if (providerInfo.NhConfigFile.ProviderInfo == null)
					providerInfo.NhConfigFile.ProviderInfo = providerInfo;

				string outputFolder = null;
				string tempFolder = null;

				if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
					throw new Exception("CurrentProject is null.");

				if (ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject != null)
				{
					outputFolder = ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.OutputFolder;
					tempFolder = ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.TempFolder;
				}
				ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject = FillScriptModel(providerInfo.EntityProviderInfo.MappingSet, null, providerInfo.NhConfigFile);
				ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.OutputFolder = outputFolder;
				ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.TempFolder = tempFolder;
				return;
			}
			XmlDocument csprojDocument = null;
			string filename;

			if (providerInfo.CsProjFile == null || !providerInfo.CsProjFile.FileExists)
			{
				// Find the csproj file we are going to use
				csprojDocument = GetCSProjDocument(data, out filename);

				if (csprojDocument != null)
					providerInfo.CsProjFile = new CSProjFile(csprojDocument, filename);
			}
			if (!providerInfo.NhConfigFile.FileExists)
			{
				if (csprojDocument == null)
					csprojDocument = GetCSProjDocument(data, out filename);
				else
					filename = providerInfo.CsProjFile.FilePath;

				providerInfo.NhConfigFile = ProjectLoader.GetNhConfigFile(providerInfo.CsProjFile, fileController);
			}
			ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject = FillScriptModel(providerInfo.EntityProviderInfo.MappingSet, providerInfo.CsProjFile, providerInfo.NhConfigFile);
		}

		internal static ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject FillScriptModel(
			ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSet mappingSet,
			Slyce.Common.CSProjFile existingCsProjectFile,
			NHConfigFile nhConfigFile)
		{
			bool topLevelLazy = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultCollectionLazy();
			ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes topLevelAccess = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultAccess();
			ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes topLevelCascade = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultCascade();
			ArchAngel.Interfaces.NHibernateEnums.TopLevelCollectionCascadeTypes topLevelCollectionCascade = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultCollectionCascade();
			bool topLevelInverse = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultInverse();

			ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject project = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject();
			Dictionary<ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity, ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity> entityLookups = new Dictionary<Entity, ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity>();
			Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn, ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn> columnLookups = new Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn, ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn>();
			Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable, ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable> tableLookups = new Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable, ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable>();
			Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable, ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable> viewLookups = new Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable, ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable>();
			Dictionary<ArchAngel.Providers.EntityModel.Model.EntityLayer.Property, ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty> propertyLookups = new Dictionary<ArchAngel.Providers.EntityModel.Model.EntityLayer.Property, ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty>();

			project.ExistingCsProjectFile = existingCsProjectFile;
			project.OverwriteFiles = ArchAngel.Interfaces.SharedData.CurrentProject.ProjectSettings.OverwriteFiles;

			if (nhConfigFile != null)
			{
				project.NHibernateConfig = new ArchAngel.Interfaces.Scripting.NHibernate.Model.INhConfig()
				{
					ConnectionString = nhConfigFile.GetConnectionString(),
					Driver = nhConfigFile.GetDriver(),
					Dialect = nhConfigFile.GetDialect(false),
					DialectSpatial = nhConfigFile.GetDialect(true),
					FileExists = nhConfigFile.FileExists,
					ExistingFilePath = nhConfigFile.FilePath
				};
			}

			#region Add Tables
			foreach (var table in mappingSet.Database.Tables)
			{
				ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable newTable = new ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable(mappingSet.Database.Name)
				   {
					   Name = table.Name,
					   Schema = table.Schema,
					   ScriptObject = table,
					   IsView = false
				   };
				foreach (var column in table.Columns)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn newColumn = CreateIColumn(column);
					newTable.Columns.Add(newColumn);
					columnLookups.Add(column, newColumn);
				}
				foreach (var index in table.Indexes)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex newIndex = CreateIIndex(index);
					
					foreach (var column in index.Columns)
						newIndex.Columns.Add(newTable.Columns.Single(c => c.Name == column.Name));

					newTable.Indexes.Add(newIndex);
				}
				var primaryKey = table.Keys.SingleOrDefault(k => k.Keytype == Providers.EntityModel.Helper.DatabaseKeyType.Primary);

				if (primaryKey != null)
				{
					newTable.PrimaryKey = new Interfaces.Scripting.NHibernate.Model.IKey()
					{
						KeyType = Interfaces.Scripting.NHibernate.Model.IKey.KeyTypes.Primary,
						Name = primaryKey.Name,
						ReferencedPrimaryKey = null,
						TableName = newTable.Name,
						TableSchema = newTable.Schema
					};
					foreach (var column in primaryKey.Columns)
						newTable.PrimaryKey.Columns.Add(newTable.Columns.Single(c => c.Name == column.Name));
				}
				tableLookups.Add(table, newTable);
				project.Tables.Add(newTable);
			}
			#endregion

			#region Add Views
			foreach (var view in mappingSet.Database.Views)
			{
				ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable newView = new ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable(mappingSet.Database.Name)
				{
					Name = view.Name,
					Schema = view.Schema,
					ScriptObject = view,
					IsView = true
				};
				foreach (var column in view.Columns)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn newColumn = CreateIColumn(column);
					newView.Columns.Add(newColumn);
					columnLookups.Add(column, newColumn);
				}
				viewLookups.Add(view, newView);
				project.Views.Add(newView);
			}
			#endregion

			#region Add Entities
			foreach (var entity in mappingSet.EntitySet.Entities)
			{
				ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity newEntity = CreateIEntity(entity);
				ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable primaryTable = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.EntityMapper.GetPrimaryTable(entity);

				if (primaryTable != null)
				{
					if (tableLookups.ContainsKey(primaryTable))
						newEntity.PrimaryMappedTable = tableLookups[primaryTable];
					else
						newEntity.PrimaryMappedTable = viewLookups[primaryTable];
				}
				else
					newEntity.PrimaryMappedTable = null;

				newEntity.IsMapped = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Utility.IsEntityMappedToTables(entity);
				newEntity.IsAbstract = entity.IsAbstract;
				newEntity.Cache = entity.GetCache();
				newEntity.Proxy = entity.GetEntityProxy();
				newEntity.BatchSize = entity.GetEntityBatchSize();
				newEntity.SelectBeforeUpdate = entity.GetEntitySelectBeforeUpdate();

				var entityDefaultLazy = entity.GetEntityLazy();

				if (entityDefaultLazy == EntityLazyTypes.inherit_default)
				{
					if (topLevelLazy) entityDefaultLazy = EntityLazyTypes.@true;
					else entityDefaultLazy = EntityLazyTypes.@false;
				}
				newEntity.LazyLoad = entityDefaultLazy == EntityLazyTypes.@true;
				newEntity.OptimisticLock = (ArchAngel.Interfaces.Scripting.NHibernate.Model.OptimisticLocks)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.OptimisticLocks), entity.GetEntityOptimisticLock().ToString(), true);

				CollectionLazyTypes entityDefaultCollectionLazy;
				CascadeTypes entityDefaultCascade;
				PropertyAccessTypes entityDefaultCollectionAccess;

				GetEntityCollectionDefaults(topLevelLazy, topLevelCascade, topLevelAccess, entity, out entityDefaultCollectionLazy, out entityDefaultCascade, out entityDefaultCollectionAccess);

				if (entity.GetEntityDefaultCascade() == CascadeTypes.inherit_default)
					newEntity.DefaultCascade = (Interfaces.Scripting.NHibernate.Model.CascadeTypes)Enum.Parse(typeof(Interfaces.Scripting.NHibernate.Model.CascadeTypes), entityDefaultCascade.ToString().Replace("_", ""), true);
				else
					newEntity.DefaultCascade = (Interfaces.Scripting.NHibernate.Model.CascadeTypes)Enum.Parse(typeof(Interfaces.Scripting.NHibernate.Model.CascadeTypes), entity.GetEntityDefaultCascade().ToString().Replace("_", ""), true);

				#region Discriminator
				if (EntityImpl.DetermineInheritanceTypeWithChildren(entity) == EntityImpl.InheritanceType.TablePerClassHierarchy && entity.Discriminator != null)
				{
					newEntity.Discriminator = new Interfaces.Scripting.NHibernate.Model.IDiscriminator();

					if (entity.Discriminator.DiscriminatorType == Enums.DiscriminatorTypes.Column)
					{
						IColumn discriminatorColumn = entity.MappedTables().ElementAt(0).Columns.Single(c => c.Name == entity.Discriminator.ColumnName);
						ArchAngel.Providers.EntityModel.Model.EntityLayer.Property prop = entity.Properties.FirstOrDefault(p => p.MappedColumn() == discriminatorColumn);
						string csharpType = "";

						if (prop != null)
							csharpType = prop.NHibernateType;
						else
							csharpType = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.ConvertType(discriminatorColumn);

						newEntity.Discriminator.DiscriminatorType = Interfaces.Scripting.NHibernate.Model.IDiscriminator.DiscriminatorTypes.Column;
						newEntity.Discriminator.Column = columnLookups[entity.MappedTables().Single().Columns.Single(c => c.Name == entity.Discriminator.ColumnName)];
						newEntity.Discriminator.CSharpType = csharpType;
					}
					else if (entity.Discriminator.DiscriminatorType == Enums.DiscriminatorTypes.Formula)
					{
						newEntity.Discriminator.DiscriminatorType = Interfaces.Scripting.NHibernate.Model.IDiscriminator.DiscriminatorTypes.Formula;
						newEntity.Discriminator.Formula = entity.Discriminator.Formula;
						//newEntity.Discriminator.CSharpType = csharpType;
					}
				}
				#endregion

				if (entity.MappedClass != null)
				{
					newEntity.MappedClass = new ArchAngel.Interfaces.Scripting.NHibernate.Model.ISourceClass();//entity.MappedClass);
					newEntity.MappedClass.FilePath = entity.EntitySet.MappingSet.CodeParseResults.GetFilenameForParsedClass(entity.MappedClass);
				}
				EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(entity);

				switch (inheritanceType)
				{
					case EntityImpl.InheritanceType.None:
						newEntity.InheritanceTypeWithParent = Interfaces.Scripting.NHibernate.Model.IEntity.InheritanceTypes.None;
						break;
					case EntityImpl.InheritanceType.TablePerClassHierarchy:
						newEntity.InheritanceTypeWithParent = Interfaces.Scripting.NHibernate.Model.IEntity.InheritanceTypes.TablePerHierarchy;
						break;
					case EntityImpl.InheritanceType.TablePerConcreteClass:
						newEntity.InheritanceTypeWithParent = Interfaces.Scripting.NHibernate.Model.IEntity.InheritanceTypes.TablePerConcreteClass;
						break;
					case EntityImpl.InheritanceType.TablePerSubClass:
						newEntity.InheritanceTypeWithParent = Interfaces.Scripting.NHibernate.Model.IEntity.InheritanceTypes.TablePerSubClass;
						break;
					default:
						throw new NotImplementedException("InheritanceType not handled yet: " + inheritanceType.ToString());
				}
				entityLookups.Add(entity, newEntity);

				#region Add Components
				foreach (var component in entity.Components.OrderBy(c => c.Name))
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IComponentProperty newComponent = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IComponentProperty()
						{
							Name = component.Name,
							Type = component.Specification.Name,
							IsSetterPrivate = (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") ||
													(bool)component.GetUserOptionValue("Component_UsePrivateSetter"),
							ScriptObject = component
						};

					foreach (var prop in component.Properties)
					{
						newComponent.Properties.Add(new ArchAngel.Interfaces.Scripting.NHibernate.Model.IField()
						{
							// TODO: do component properties have a SetterIsPrivate user option?
							//IsSetterPrivate = prop.RepresentedProperty.
							MappedColumn = columnLookups[prop.MappedColumn()],
							Name = prop.PropertyName,
							ScriptObject = prop,
							Type = prop.RepresentedProperty.Type
						}
						);
					}
					newEntity.Components.Add(newComponent);
				}
				#endregion

				#region Add Properties
				List<string> propertyNamesFromReferences = entity.DirectedReferences.Where(r => r.FromEndEnabled).Select(d => d.FromName).ToList();
				List<string> propertyNamesFromComponents = entity.Components.Select(c => c.Name).ToList();

				foreach (var property in entity.ConcreteProperties.OrderBy(p => p.Name))
				{
					if (!entity.ForeignKeyPropertiesToExclude.Contains(property))
					{
						ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty newProperty = ConvertProperty(columnLookups, property, entityDefaultCollectionAccess);
						newEntity.Properties.Add(newProperty);
						newProperty.Parent = newEntity;
						propertyLookups.Add(property, newProperty);
					}
				}
				foreach (var property in entity.PropertiesHiddenByAbstractParent.OrderBy(p => p.Name))
				{
					if (!entity.ForeignKeyPropertiesToExclude.Contains(property))
					{
						if (newEntity.Properties.Any(p => p.Name == property.Name))
							continue;

						ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty newProperty = ConvertProperty(columnLookups, property, entityDefaultCollectionAccess);
						newProperty.IsInherited = true;
						newEntity.Properties.Add(newProperty);
						newProperty.Parent = newEntity;
						propertyLookups.Add(property, newProperty);
					}
				}
				newEntity.Properties = newEntity.Properties.OrderBy(p => p.Name).ToList();
				#endregion

				#region Add Key
				if (entity.Key != null)
				{
					newEntity.Key = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntityKey();
					newEntity.Key.KeyType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.KeyTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.KeyTypes), entity.Key.KeyType.ToString(), true);

					foreach (Property keyProperty in entity.Key.Properties)
					{
						ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty existingProperty = newEntity.Properties.SingleOrDefault(p => p.Name == keyProperty.Name);

						if (existingProperty != null)
							newEntity.Key.Properties.Add(existingProperty);
						else
							newEntity.Key.Properties.Add(ConvertProperty(columnLookups, keyProperty, entityDefaultCollectionAccess));
					}
					//foreach (var prop in newEntity.Properties)
					//{
					//    if (prop.IsKeyProperty)
					//        newEntity.Key.Properties.Add(prop);
					//}
				}
				#endregion

				#region Add Generator
				newEntity.Generator = new Interfaces.Scripting.NHibernate.Model.IEntityGenerator(entity.Generator.ClassName);

				foreach (var p in entity.Generator.Parameters)
					newEntity.Generator.Parameters.Add(new Interfaces.Scripting.NHibernate.Model.IEntityGenerator.IParameter(p.Name, p.Value));

				#endregion

				project.Entities.Add(newEntity);
			}

			#region Add Children
			foreach (var entity in mappingSet.EntitySet.Entities)
			{
				ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity newEntity = entityLookups[entity];

				foreach (var child in entity.Children.OrderBy(c => c.Name))
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity newChild = entityLookups[child];
					newChild.Parent = entityLookups[entity];

					if (EntityImpl.DetermineInheritanceTypeWithParent(child) == EntityImpl.InheritanceType.TablePerClassHierarchy)
						newChild.DiscriminatorValue = child.DiscriminatorValue;

					newEntity.Children.Add(entityLookups[child]);
				}
			}
			#endregion

			//foreach (var entity in mappingSet.EntitySet.Entities)
			//{
			//    if (entity.Parent != null)
			//        entityLookups[entity].Parent = entityLookups[entity.Parent];
			//}
			#endregion

			#region Add References

			foreach (var entity in mappingSet.EntitySet.Entities)
			{
				CollectionLazyTypes entityDefaultLazy;
				CascadeTypes entityDefaultCascade;
				PropertyAccessTypes entityDefaultAccess;

				GetEntityCollectionDefaults(topLevelLazy, topLevelCascade, topLevelAccess, entity, out entityDefaultLazy, out entityDefaultCascade, out entityDefaultAccess);

				Dictionary<ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable, int> manyToManySameTableProcessingCounts = new Dictionary<ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable, int>();
				Dictionary<string, int> fromProcessedRelationships = new Dictionary<string, int>();
				Dictionary<string, int> toProcessedRelationships = new Dictionary<string, int>();

				foreach (var reference in entity.DirectedReferences.Where(r => r.FromEndEnabled).OrderBy(r => r.FromName))
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IReference newReference = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IReference()
					{
						Name = reference.FromName,
						ToName = reference.ToName,
						IsSetterPrivate = (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") ||
									(reference.Entity1IsFromEnd ? (bool)reference.Reference.GetUserOptionValue("End1UsePrivateSetter") : (bool)reference.Reference.GetUserOptionValue("End2UsePrivateSetter")),
						ToEntity = entityLookups[reference.ToEntity],
						CollectionType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes), NHCollections.GetAssociationType(reference).ToString(), true),
						FetchType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.FetchTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.FetchTypes), NHCollections.GetFetchType(reference).ToString(), true),
						PreviousNames = reference.OldFromNames.ToList(),
						Insert = reference.Entity1IsFromEnd ? (bool)reference.Reference.GetUserOptionValue("Reference_End1Insert") : (bool)reference.Reference.GetUserOptionValue("Reference_End2Insert"),
						Update = reference.Entity1IsFromEnd ? (bool)reference.Reference.GetUserOptionValue("Reference_End1Update") : (bool)reference.Reference.GetUserOptionValue("Reference_End2Update")
					};
					#region Handle default values

					#region Cascade
					string cascade = reference.Entity1IsFromEnd ? reference.Reference.GetReferenceEnd1Cascade().ToString() : reference.Reference.GetReferenceEnd2Cascade().ToString();

					if (cascade == CascadeTypes.inherit_default.ToString())
						newReference.CascadeType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.CascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.CascadeTypes), entityDefaultCascade.ToString().Replace("_", ""), true);
					else
						newReference.CascadeType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.CascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.CascadeTypes), cascade.Replace("_", ""), true);
					#endregion

					#region Lazy
					string lazy = reference.Entity1IsFromEnd ? reference.Reference.GetReferenceEnd1Lazy().ToString() : reference.Reference.GetReferenceEnd2Lazy().ToString();

					if (lazy == CollectionLazyTypes.inherit_default.ToString())
						newReference.LazyType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.LazyTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.LazyTypes), entityDefaultLazy.ToString(), true);
					else
						newReference.LazyType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.LazyTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.LazyTypes), lazy, true);
					#endregion

					#region CollectionCascade
					string collectionCascade = reference.Entity1IsFromEnd ? reference.Reference.GetReferenceEnd1CollectionCascade().ToString() : reference.Reference.GetReferenceEnd2CollectionCascade().ToString();

					if (collectionCascade == CollectionCascadeTypes.inherit_default.ToString())
						newReference.CollectionCascadeType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionCascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionCascadeTypes), topLevelCollectionCascade.ToString().Replace("_", ""), true);
					else
						newReference.CollectionCascadeType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionCascadeTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionCascadeTypes), collectionCascade.Replace("_", ""), true);
					#endregion

					#region Inverse
					BooleanInheritedTypes inverse = reference.Entity1IsFromEnd ? (BooleanInheritedTypes)reference.Reference.GetUserOptionValue("End1Inverse") : (BooleanInheritedTypes)reference.Reference.GetUserOptionValue("End2Inverse");

					if (inverse == BooleanInheritedTypes.inherit_default)
						newReference.Inverse = topLevelInverse;
					else
						newReference.Inverse = inverse == BooleanInheritedTypes.@true;
					#endregion

					#region OrderBy
					Property orderByProperty = null;
					string orderByColumnName = "";
					bool orderByIsAsc = true;
					string orderByClause = "";

					if (reference.Entity1IsFromEnd)
					{
						orderByIsAsc = reference.Reference.GetReferenceEnd1OrderByIsAsc();
						orderByProperty = reference.Reference.GetReferenceEnd1OrderByProperty();
					}
					else
					{
						orderByIsAsc = reference.Reference.GetReferenceEnd2OrderByIsAsc();
						orderByProperty = reference.Reference.GetReferenceEnd1OrderByProperty();
					}
					if (orderByProperty != null)
					{
						var col = orderByProperty.MappedColumn();

						if (col != null)
							orderByColumnName = col.Name;

						if (orderByColumnName.Length > 0)
						{
							if (orderByIsAsc)
								orderByClause = orderByColumnName;
							else
								orderByClause = orderByColumnName + " desc";
						}
					}
					#endregion

					#endregion

					if (orderByProperty != null)
						newReference.OrderByProperty = propertyLookups[orderByProperty];

					newReference.OrderByIsAsc = orderByIsAsc;

					if (newReference.CollectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.Map ||
						newReference.CollectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.IDBag ||
						newReference.CollectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.List)// ||
					//newReference.CollectionType == ArchAngel.Interfaces.Scripting.NHibernate.Model.CollectionTypes.Bag)
					{
						var indexColumn = NHCollections.GetIndexColumn(reference, ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.EntityMapper.GetPrimaryTable(reference.ToEntity));

						if (indexColumn != null)
							newReference.CollectionIndexColumn = columnLookups[indexColumn];
						else
							throw new Exception(string.Format("No index column found for the {0} collection between {1} and {2}.", newReference.CollectionType, reference.ToEntity.Name, reference.FromEntity.Name));
					}
					ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceType refType = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceMapper.DetermineReferenceType(reference.Reference);

					switch (refType)
					{
						case ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceType.ManyToMany:
							newReference.ReferenceType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.ManyToMany;
							newReference.ManyToManyAssociationTable = tableLookups[reference.Reference.MappedTable()];
							break;
						case ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceType.ManyToOne:
							newReference.ReferenceType = reference.FromEndCardinality == Cardinality.One ? ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.OneToMany : ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.ManyToOne;
							break;
						case ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceType.OneToOne:
							newReference.ReferenceType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.OneToOne;
							break;
						case ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceType.Unsupported:
							newReference.ReferenceType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.Unsupported;
							break;
						default:
							newReference.ReferenceType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.Unsupported;
							break;
					}
					ITable referenceMappedTable = reference.Reference.MappedTable();

					if (referenceMappedTable == null &&
						(newReference.ReferenceType == ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.ManyToOne ||
						newReference.ReferenceType == ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.OneToOne ||
						newReference.ReferenceType == ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.OneToMany))
					{
						var directedRelationship = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ReferenceMapper.GetDirectedMappedRelationship(entity, reference.Reference);
						newReference.KeyType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceKeyTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceKeyTypes), directedRelationship.FromKey.Keytype.ToString(), true);

						if (directedRelationship.ToTable == directedRelationship.FromTable)
						{
							if (directedRelationship.ToKey.Keytype == Providers.EntityModel.Helper.DatabaseKeyType.Primary)
							{
								foreach (var column in directedRelationship.FromKey.Columns)
									newReference.KeyColumns.Add(columnLookups[column]);

								foreach (var column in directedRelationship.ToKey.Columns)
									newReference.ToKeyColumns.Add(columnLookups[column]);
							}
							else
							{
								foreach (var column in directedRelationship.ToKey.Columns)
									newReference.KeyColumns.Add(columnLookups[column]);

								foreach (var column in directedRelationship.FromKey.Columns)
									newReference.ToKeyColumns.Add(columnLookups[column]);
							}
						}
						else
						{
							foreach (var column in directedRelationship.FromKey.Columns)
								newReference.KeyColumns.Add(columnLookups[column]);

							foreach (var column in directedRelationship.ToKey.Columns)
								newReference.ToKeyColumns.Add(columnLookups[column]);
						}
					}
					else if (newReference.ReferenceType == ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceTypes.ManyToMany)
					{
						newReference.KeyType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceKeyTypes.None;

						ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable fromPrimaryMappedTable = entityLookups[entity].PrimaryMappedTable;
						ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable toPrimaryMappedTable = newReference.ToEntity.PrimaryMappedTable;

						List<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn> fromInPrimaryKey = new List<IColumn>();
						List<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn> toInPrimaryKey = new List<IColumn>();

						if (fromPrimaryMappedTable == toPrimaryMappedTable)
						{
							// This many-to-many relationship is to the same table
							if (manyToManySameTableProcessingCounts.ContainsKey(toPrimaryMappedTable))
							{
								int index = manyToManySameTableProcessingCounts[toPrimaryMappedTable];
								index++;
								fromInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).ElementAt(index).PrimaryKey.Columns);
								toInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).ElementAt(index).ForeignKey.Columns);
								manyToManySameTableProcessingCounts[toPrimaryMappedTable] = index;
							}
							else
							{
								fromInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).ElementAt(0).PrimaryKey.Columns);
								toInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).ElementAt(0).ForeignKey.Columns);
								manyToManySameTableProcessingCounts.Add(toPrimaryMappedTable, 0);
							}
						}
						else
						{
							//if (referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).Count() != 1)
							//    throw new Exception(string.Format("Association table [{0}.{1}] has an unexpected number of relationships to [{2}.{3}]. Please contact support@slyce.com to help us fix this in Visual NHibernate.", referenceMappedTable.Schema, referenceMappedTable.Name, fromPrimaryMappedTable.Schema, fromPrimaryMappedTable.Name));

							//if (referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).Count() != 1)
							//    throw new Exception(string.Format("Association table [{0}.{1}] has an unexpected number of relationships to [{2}.{3}]. Please contact support@slyce.com to help us fix this in Visual NHibernate.", referenceMappedTable.Schema, referenceMappedTable.Name, toPrimaryMappedTable.Schema, toPrimaryMappedTable.Name));

							//fromInPrimaryKey = referenceMappedTable.Relationships.Single(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).ForeignKey.Columns;
							//toInPrimaryKey = referenceMappedTable.Relationships.Single(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).ForeignKey.Columns;

							foreach (var rel in referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject))
								fromInPrimaryKey.AddRange(rel.ForeignKey.Columns);

							foreach (var rel in referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject))
								toInPrimaryKey.AddRange(rel.ForeignKey.Columns);

							//if (reference.Reference.MappedRelationship().PrimaryTable == fromPrimaryMappedTable.ScriptObject)
							//{
							//    fromInPrimaryKey = reference.Reference.MappedRelationship().ForeignKey.Columns;
							//    toInPrimaryKey = reference.Reference.MappedRelationship().PrimaryKey.Columns;
							//}
							//else
							//{
							//    fromInPrimaryKey = reference.Reference.MappedRelationship().PrimaryKey.Columns;
							//    toInPrimaryKey = reference.Reference.MappedRelationship().ForeignKey.Columns;
							//}
						}
						foreach (var column in fromInPrimaryKey)
							newReference.KeyColumns.Add(columnLookups[column]);

						foreach (var column in toInPrimaryKey)
							newReference.ToKeyColumns.Add(columnLookups[column]);
					}
					else if (referenceMappedTable != null) // Association table, but not many-to-many
					{
						newReference.KeyType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceKeyTypes.None;

						ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable fromPrimaryMappedTable = entityLookups[entity].PrimaryMappedTable;
						ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable toPrimaryMappedTable = newReference.ToEntity.PrimaryMappedTable;

						IEnumerable<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn> fromInPrimaryKey = null;
						IEnumerable<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn> toInPrimaryKey = null;

						//if (fromPrimaryMappedTable == toPrimaryMappedTable)
						//{
						//    // This many-to-many relationship is to the same table
						//    if (manyToManySameTableProcessingCounts.ContainsKey(toPrimaryMappedTable))
						//    {
						//        int index = manyToManySameTableProcessingCounts[toPrimaryMappedTable];
						//        index++;
						//        fromInPrimaryKey = referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).ElementAt(index).ForeignKey.Columns;
						//        toInPrimaryKey = referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).ElementAt(index).ForeignKey.Columns;
						//        manyToManySameTableProcessingCounts[toPrimaryMappedTable] = index;
						//    }
						//    else
						//    {
						//        fromInPrimaryKey = referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).ElementAt(0).ForeignKey.Columns;
						//        toInPrimaryKey = referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).ElementAt(0).ForeignKey.Columns;
						//        manyToManySameTableProcessingCounts.Add(toPrimaryMappedTable, 0);
						//    }
						//}
						//else
						//{
						//if (referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject).Count() != 1)
						//    throw new Exception(string.Format("Association table [{0}.{1}] has an unexpected number of relationships to [{2}.{3}]. Please contact support@slyce.com to help us fix this in Visual NHibernate.", referenceMappedTable.Schema, referenceMappedTable.Name, fromPrimaryMappedTable.Schema, fromPrimaryMappedTable.Name));

						//if (referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject).Count() != 1)
						//    throw new Exception(string.Format("Association table [{0}.{1}] has an unexpected number of relationships to [{2}.{3}]. Please contact support@slyce.com to help us fix this in Visual NHibernate.", referenceMappedTable.Schema, referenceMappedTable.Name, toPrimaryMappedTable.Schema, toPrimaryMappedTable.Name));

						var fromRelationships = referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject || t.ForeignTable == fromPrimaryMappedTable.ScriptObject).ToList();
						var toRelationships = referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject || t.ForeignTable == toPrimaryMappedTable.ScriptObject).ToList();
						string key = entity.Name + ": " + fromPrimaryMappedTable.Schema + fromPrimaryMappedTable.Name + " > " + toPrimaryMappedTable.Schema + toPrimaryMappedTable.Name;

						if (fromRelationships.Count == 1)
							fromInPrimaryKey = fromRelationships[0].ForeignKey.Columns;
						else
						{
							//string key = reference.GetHashCode().ToString() + "FROM" + fromPrimaryMappedTable.Schema + fromPrimaryMappedTable.Name;
							int prevIndex = 0;

							if (!fromProcessedRelationships.ContainsKey(key))
							{
								fromProcessedRelationships.Add(key, 0);
								fromInPrimaryKey = fromRelationships[0].ForeignKey.Columns;
							}
							else
							{
								prevIndex = fromProcessedRelationships[key];
								prevIndex++;
								fromInPrimaryKey = fromRelationships[prevIndex].ForeignKey.Columns;
								fromProcessedRelationships[key] = prevIndex;
							}
						}
						if (toRelationships.Count == 1)
							toInPrimaryKey = toRelationships[0].ForeignKey.Columns;
						else
						{
							//string key = reference.GetHashCode().ToString() + "TO" + toPrimaryMappedTable.Schema + toPrimaryMappedTable.Name;
							int prevIndex = 0;

							if (!toProcessedRelationships.ContainsKey(key))
							{
								toProcessedRelationships.Add(key, 0);
								toInPrimaryKey = toRelationships[0].ForeignKey.Columns;
							}
							else
							{
								prevIndex = toProcessedRelationships[key];
								prevIndex++;
								toInPrimaryKey = toRelationships[prevIndex].ForeignKey.Columns;
								toProcessedRelationships[key] = prevIndex;
							}
						}

						//fromInPrimaryKey = referenceMappedTable.Relationships.Single(t => t.PrimaryTable == fromPrimaryMappedTable.ScriptObject || t.ForeignTable == fromPrimaryMappedTable.ScriptObject).ForeignKey.Columns;
						//toInPrimaryKey = referenceMappedTable.Relationships.Single(t => t.PrimaryTable == toPrimaryMappedTable.ScriptObject || t.ForeignTable == toPrimaryMappedTable.ScriptObject).ForeignKey.Columns;
						//}
						foreach (var column in fromInPrimaryKey)
							newReference.KeyColumns.Add(columnLookups[column]);

						foreach (var column in toInPrimaryKey)
							newReference.ToKeyColumns.Add(columnLookups[column]);
					}
					else
						newReference.KeyType = ArchAngel.Interfaces.Scripting.NHibernate.Model.ReferenceKeyTypes.None;

					if (reference.FromEndCardinality == ArchAngel.Interfaces.Cardinality.Many)
						newReference.Type = NHCollections.GetCollectionType(reference).Replace(SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString() + ".Model.", "");
					else
						newReference.Type = reference.ToEntity.Name;

					entityLookups[entity].References.Add(newReference);
				}
			}
			#endregion

			#region Add ComponentSpecs
			foreach (var componentSpec in mappingSet.EntitySet.ComponentSpecifications)
			{
				ArchAngel.Interfaces.Scripting.NHibernate.Model.IComponent newComponentType = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IComponent()
				{
					Name = componentSpec.Name,
					ScriptObject = componentSpec
				};

				#region Add fields
				foreach (var field in componentSpec.Properties)
				{
					ArchAngel.Interfaces.Scripting.NHibernate.Model.IFieldDef newField = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IFieldDef()
					{
						Name = field.Name,
						Type = field.Type,
						IsSetterPrivate = (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") || (bool)field.GetUserOptionValue("ComponentProperty_UsePrivateSetter"),
						ScriptObject = field
					};
					newComponentType.Properties.Add(newField);
				}
				#endregion

				project.Components.Add(newComponentType);
			}
			#endregion

			return project;
		}

		private static string ConvertTypeToVb(string csharpType)
		{
			string nullableEnd = csharpType.EndsWith("?") ? "?" : "";

			switch (csharpType)
			{
				case "bool": return "Boolean" + nullableEnd;
				case "byte":
				case "sbyte": return "Byte" + nullableEnd;
				case "short":
				case "ushort": return "Short" + nullableEnd;
				case "int":
				case "uint": return "Integer" + nullableEnd;
				case "long":
				case "ulong": return "Long" + nullableEnd;
				case "float": return "Single" + nullableEnd;
				case "double": return "Double" + nullableEnd;
				case "DateTime": return "Date" + nullableEnd;
				case "object": return "Object" + nullableEnd;
				case "string": return "String" + nullableEnd;
				default: return csharpType + nullableEnd;
			}
		}

		internal static void GetEntityCollectionDefaults(
			bool topLevelLazy,
			ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes topLevelCascade,
			ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes topLevelAccess,
			Entity entity,
			out CollectionLazyTypes entityDefaultLazy,
			out CascadeTypes entityDefaultCascade,
			out PropertyAccessTypes entityDefaultAccess)
		{
			entityDefaultLazy = entity.GetEntityDefaultCollectionLazy();

			if (entityDefaultLazy == CollectionLazyTypes.inherit_default)
			{
				if (topLevelLazy) entityDefaultLazy = CollectionLazyTypes.@true;
				else entityDefaultLazy = CollectionLazyTypes.@false;
			}
			entityDefaultCascade = entity.GetEntityDefaultCascade();

			if (entityDefaultCascade == CascadeTypes.inherit_default)
				entityDefaultCascade = (CascadeTypes)Enum.Parse(typeof(CascadeTypes), topLevelCascade.ToString(), false);

			entityDefaultAccess = entity.GetEntityDefaultAccess();

			if (entityDefaultAccess == PropertyAccessTypes.inherit_default)
				entityDefaultAccess = (PropertyAccessTypes)Enum.Parse(typeof(PropertyAccessTypes), topLevelAccess.ToString(), false);
		}

		private static ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty ConvertProperty(Dictionary<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn, ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn> columnLookups, Property property, PropertyAccessTypes entityDefaultAccess)
		{
			ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty newProperty = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IProperty()
			{
				Name = property.Name,
				Type = property.Type.Replace(SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString() + ".Model.", ""),
				IsInherited = property.IsInherited,
				IsSetterPrivate = (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") || (bool)property.GetUserOptionValue("Property_UsePrivateSetter"),
				ScriptObject = property,
				IsKeyProperty = property.IsKeyProperty,
				PreviousNames = property.OldNames.ToList(),
				//Access = (ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyAccessTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyAccessTypes), property.GetPropertyAccess().ToString(), true),
				Formula = property.GetPropertyFormula(),
				Insert = property.GetPropertyInsert(),
				Update = property.GetPropertyUpdate(),
				Generate = (ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyGeneratedTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyGeneratedTypes), property.GetPropertyGenerated().ToString(), true),
				IsVersionProperty = property.GetPropertyIsVersion(),
				IsNullable = IsTypeNullable(property.Type),
				TypeVB = ConvertTypeToVb(property.Type)
			};
			string accessType = property.GetPropertyAccess().ToString();

			if (accessType == PropertyAccessTypes.inherit_default.ToString())
				newProperty.Access = (ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyAccessTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyAccessTypes), entityDefaultAccess.ToString(), true);
			else
				newProperty.Access = (ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyAccessTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.PropertyAccessTypes), accessType, true);

			ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn mappedColumn = property.MappedColumn();

			if (mappedColumn != null)
			{
				newProperty.MappedColumn = columnLookups[mappedColumn];
				newProperty.MappedColumnName = mappedColumn.Name;
			}
			//else
			//{
			//    List<Entity> children = property.Entity.Children.Where(c => EntityImpl.DetermineInheritanceTypeWithParent(c) == EntityImpl.InheritanceType.TablePerClassHierarchy).ToList();

			//    if (children.c)

			//}
			return newProperty;
		}

		private static bool IsTypeNullable(string typeName)
		{
			typeName = typeName.ToLower().Trim();

			switch (typeName)
			{
				case "bool":
				case "boolean":
				case "system.boolean":

				case "char":
				case "system.char":

				case "datetime":
				case "system.datetime":

				case "decimal":
				case "system.decimal":

				case "double":
				case "system.double":

				case "guid":
				case "system.guid":

				case "int16":
				case "short":
				case "system.int16":

				case "int32":
				case "int":
				case "system.int32":

				case "int64":
				case "long":
				case "system.int64":

				case "sbyte":
				case "system.sbyte":

				case "single":
				case "system.single":

				case "timespan":
				case "system.timespan":

				case "uint16":
				case "system.uint16":

				case "uint32":
				case "system.uint32":

				case "uint64":
				case "system.uint64":
					return typeName.EndsWith("?");
			}
			return true;
		}

		private static ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity CreateIEntity(ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity entity)
		{
			string camelName;

			if (entity.Name.Length > 1)
				camelName = entity.Name.Substring(0, 1).ToLower() + entity.Name.Substring(1);
			else if (entity.Name.Length > 0)
				camelName = entity.Name.Substring(0, 1).ToLower();
			else
				camelName = "";

			ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity newEntity = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity()
						{
							Name = entity.Name,
							NamePlural = entity.Name.Pluralize(),
							NameCamelCase = camelName,
							NameCamelCasePlural = camelName.Pluralize(),
							IsInherited = entity.HasParent,
							//ImplementEqualityMembers = (bool)entity.GetUserOptionValue("ImplementEqualityMembers"),
							ParentName = entity.HasParent ? entity.Parent.Name : "",
							IsMapped = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Utility.IsEntityMappedToTables(entity),
							ScriptObject = entity,
							IsAbstract = entity.IsAbstract,
							IsReadOnly = !entity.GetEntityMutable(),
							IsDynamicUpdate = entity.GetEntityDynamicUpdate(),
							IsDynamicInsert = entity.GetEntityDynamicInsert()
						};

			return newEntity;
		}

		private static ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn CreateIColumn(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn column)
		{
			ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn newColumn = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn();
			newColumn.Name = column.Name;
			newColumn.ScriptObject = column;
			newColumn.IsIdentity = column.IsIdentity;
			newColumn.IsNullable = column.IsNullable;
			newColumn.Length = column.Size;
			newColumn.Type = column.OriginalDataType;
			newColumn.SizeIsMax = column.SizeIsMax;
			return newColumn;
		}

		private static ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex CreateIIndex(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IIndex index)
		{
			ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex newIndex = new ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex()
				{
					Name = index.Name,
					IndexType = (ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex.DatabaseIndexType)Enum.Parse(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.IIndex.DatabaseIndexType), index.IndexType.ToString()),
					IsClustered = index.IsClustered,
					IsUnique = index.IsUnique,
					IsUserDefined = index.IsUserDefined
				};
			return newIndex;
		}

		private XmlDocument GetCSProjDocument(PreGenerationData data, out string filename)
		{
			var doc = new XmlDocument();

			var projectName = data.UserOptions.GetValueOrDefault("ProjectName") as string;
			if (string.IsNullOrEmpty(projectName) == false)
			{
				// Search for the project as named by the ProjectName User Option.
				filename = Path.Combine(data.OutputFolder, projectName + ".csproj");

				if (fileController.FileExists(filename))
				{
					doc.LoadXml(fileController.ReadAllText(filename));
					return doc;
				}
			}

			// If we get to this point, we couldn't find the project in the default location,
			// so we search for the first project that has any *.hbm.xml files in it.
			var csProjFilenames = fileController.FindAllFilesLike(data.OutputFolder, "*.csproj", SearchOption.AllDirectories);

			foreach (var csprojFilename in csProjFilenames)
			{
				doc = new XmlDocument();
				doc.LoadXml(fileController.ReadAllText(csprojFilename));

				var hbmFiles = ProjectLoader.GetHBMFilesFromCSProj(new CSProjFile(doc, csprojFilename), fileController);

				if (hbmFiles.Any())
				{
					filename = csprojFilename;
					return doc;
				}
			}

			filename = "";
			return null;
		}
	}
}