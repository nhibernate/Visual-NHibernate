using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ArchAngel.NHibernateHelper.EntityExtensions;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
//using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using log4net;
using Slyce.Common.StringExtensions;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2
{
	public class EntityMapper
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(EntityMapper));

		public @class ProcessEntity(Entity entity)
		{
			Log.DebugFormat("Processing entity {0}", entity.Name);
			var newClass = new @class { name = entity.Name };
			IList<ITable> mappedTables = entity.MappedTables().ToList();
			// One Entity to one or more tables
			ITable table = GetPrimaryTable(entity);

			if (table != null && !string.IsNullOrEmpty(table.Name))
				newClass.table = table.Name.BackTick();

			if (table != null && !string.IsNullOrEmpty(table.Schema))
				newClass.schema = table.Schema.BackTick();

			var entityDefaultLazy = entity.GetEntityLazy();

			if (entity.GetEntityLazy() == Interfaces.NHibernateEnums.EntityLazyTypes.inherit_default)
				newClass.lazy = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultLazy();
			else
				newClass.lazy = entityDefaultLazy == Interfaces.NHibernateEnums.EntityLazyTypes.@true;

			newClass.lazySpecified = !newClass.lazy;

			newClass.batchsize = entity.GetEntityBatchSize();
			newClass.batchsizeSpecified = entity.GetEntityBatchSize() != 1;

			if (entity.GetEntityDynamicInsert())
				newClass.dynamicinsert = true;

			if (entity.GetEntityDynamicUpdate())
				newClass.dynamicupdate = true;

			newClass.@abstract = entity.IsAbstract;

			if (entity.IsAbstract)
				newClass.abstractSpecified = true;

			newClass.mutable = entity.GetEntityMutable();
			newClass.optimisticlock = (optimisticLockMode)Enum.Parse(typeof(optimisticLockMode), entity.GetEntityOptimisticLock().ToString(), true);
			newClass.selectbeforeupdate = entity.GetEntitySelectBeforeUpdate();

			if (entity.Cache.Usage != Cache.UsageTypes.None)
			{
				newClass.cache = new cache()
				{
					include = (cacheInclude)Enum.Parse(typeof(cacheInclude), entity.Cache.Include.ToString().Replace("_", ""), true),
					region = entity.Cache.Region
				};
				switch (entity.Cache.Usage)
				{
					case Cache.UsageTypes.NonStrict_Read_Write:
						newClass.cache.usage = cacheUsage.nonstrictreadwrite;
						break;
					case Cache.UsageTypes.Read_Only:
						newClass.cache.usage = cacheUsage.@readonly;
						break;
					case Cache.UsageTypes.Read_Write:
						newClass.cache.usage = cacheUsage.readwrite;
						break;
					case Cache.UsageTypes.Transactional:
						newClass.cache.usage = cacheUsage.transactional;
						break;
					default:
						throw new NotImplementedException("This cache type not implemented yet: " + entity.Cache.Usage.ToString());
				}
			}

			if (entity.GetEntityBatchSize() != 1)
				newClass.batchsize = entity.GetEntityBatchSize();

			if (!string.IsNullOrWhiteSpace(entity.GetEntityProxy()))
				newClass.proxy = entity.GetEntityProxy();

			if (!string.IsNullOrWhiteSpace(entity.GetEntityPersister()))
				newClass.persister = entity.GetEntityPersister();

			string sqlWhere = entity.GetEntitySqlWhereClause();

			if (!string.IsNullOrEmpty(sqlWhere))
				newClass.where = sqlWhere;

			// bool isSingleColumnPK = false;

			if (entity.IsAbstract)
			{
				// This is an abstract class in Table Per Concrete Class inheritance. The child entities
				// should have properties of the same name.
				Entity firstChild = entity.Children.FirstOrDefault();

				while (firstChild != null && firstChild.IsAbstract)
					firstChild = firstChild.Children.FirstOrDefault();

				if (firstChild != null)
				{
					ITable childTable = GetPrimaryTable(firstChild);
					ProcessEntityKey(firstChild, newClass, childTable);
					// isSingleColumnPK = childTable.ColumnsInPrimaryKey.Count() == 1;

					foreach (var property in entity.Properties.OrderBy(p => p.Name))
					{
						if (firstChild.PropertiesHiddenByAbstractParent.Single(p => p.Name == property.Name).IsKeyProperty)
							continue;

						var mappedColumn = firstChild.PropertiesHiddenByAbstractParent.Single(p => p.Name == property.Name).MappedColumn();
						property prop = new property { name = property.Name, column = mappedColumn.Name.BackTick() };
						//AddExtraInfoToProperty(prop, property);
						newClass.AddProperty(prop);
					}
				}
				foreach (Entity child in entity.Children)
				{
					Entity theChild = child;

					while (theChild != null && theChild.IsAbstract)
						theChild = theChild.Children.FirstOrDefault();

					ITable mappedTable = theChild.MappedTables().First();

					unionsubclass union = new unionsubclass()
					{
						name = theChild.Name,
						table = mappedTable.Name.BackTick()
					};
					newClass.AddItem1(union);

					List<property> unionProperties = new List<property>();

					foreach (Property prop in theChild.ConcreteProperties)
					{
						if (prop.IsKeyProperty)
							continue;

						unionProperties.Add(
						new property()
						{
							name = prop.Name,
							column = prop.MappedColumn().Name.BackTick()
						});
						//AddExtraInfoToProperty(prop, property);
					}
					union.Items = unionProperties.ToArray();
				}
			}
			else
			{
				ProcessEntityKey(entity, newClass, table);
				// isSingleColumnPK = table.ColumnsInPrimaryKey.Count() == 1;
			}
			var propertiesAlreadyHandled = new List<Property>();

			foreach (ITable joinedTable in mappedTables.OrderBy(t => t.Name))
			{
				if (joinedTable == table)
					continue;

				join joinNode = new join();
				joinNode.table = joinedTable.Name.BackTick();
				joinNode.schema = joinedTable.Schema.BackTick();
				int numPrimaryKeyColumns = joinedTable.ColumnsInPrimaryKey.Count();

				if (numPrimaryKeyColumns > 0)
				{
					key keyNode = GetKeyNode(joinedTable);
					joinNode.key = keyNode;
				}

				foreach (var property in entity.Properties.OrderBy(p => p.Name))
				{
					var mappedColumn = property.MappedColumn();

					if (mappedColumn == null || mappedColumn.Parent != joinedTable)
						continue;

					property prop = new property { name = property.Name, column = mappedColumn.Name.BackTick() };
					AddExtraInfoToProperty(prop, property);
					joinNode.AddProperty(prop);
					propertiesAlreadyHandled.Add(property);
				}
				newClass.AddItem1(joinNode);
			}
			var versionProperty = entity.Properties.FirstOrDefault(p => p.GetPropertyIsVersion());

			if (versionProperty != null)
			{
				version versionNode = new version();
				versionNode.column1 = versionProperty.MappedColumn().Name.BackTick();
				versionNode.name = versionProperty.Name;
				versionNode.type = versionProperty.NHibernateType;
				// AddExtraInfoToProperty(prop, property);
				newClass.SetVersion(versionNode);
				propertiesAlreadyHandled.Add(versionProperty);
			}

			//foreach (var prop in ProcessProperties(entity.Properties.Except(propertiesAlreadyHandled).Except(entity.ForeignKeyPropertiesToExclude), isSingleColumnPK).OrderBy(p => p.name))
			foreach (var prop in ProcessProperties(entity.Properties.Except(propertiesAlreadyHandled).Except(entity.ForeignKeyPropertiesToExclude)).OrderBy(p => p.name))
				newClass.AddProperty(prop);

			// Process components, skip component used as Key.
			foreach (var component in ProcessComponent(entity.Components.Except(new[] { entity.Key.Component })).OrderBy(c => c.name))
				newClass.AddComponent(component);

			ProcessInheritance(entity, newClass);

			var referenceMapper = new ReferenceMapper();
			referenceMapper.ProcessReferences(entity, item => newClass.AddItem(item));
			return newClass;
		}

		private void AddExtraInfoToProperty(property xmlProp, Property originalProp)
		{
			xmlProp.insert = originalProp.GetPropertyInsert();
			xmlProp.update = originalProp.GetPropertyUpdate();

			string formula = originalProp.GetPropertyFormula();

			if (!string.IsNullOrWhiteSpace(formula))
				xmlProp.formula = formula;

			xmlProp.optimisticlock = originalProp.GetPropertyOptimisticLock();
			xmlProp.generated = (propertyGeneration)Enum.Parse(typeof(propertyGeneration), originalProp.GetPropertyGenerated().ToString());

			var access = originalProp.GetPropertyAccess();

			if (access != Interfaces.NHibernateEnums.PropertyAccessTypes.inherit_default)
				xmlProp.access = access.ToString().Replace("_", "-");

			if (!originalProp.GetPropertyInsert())
			{
				xmlProp.insertSpecified = true;
				xmlProp.insert = false;
			}
			if (!originalProp.GetPropertyUpdate())
			{
				xmlProp.updateSpecified = true;
				xmlProp.update = false;
			}
		}

		private IEnumerable<component> ProcessComponent(IEnumerable<Component> components)
		{
			foreach (var component in components.OrderBy(c => c.Name))
			{
				var componentNode = new component();
				componentNode.@class = component.Specification.Name;
				componentNode.name = component.Name;

				foreach (var property in component.Properties.OrderBy(p => p.PropertyName))
				{
					ComponentProperty representedProperty = property.RepresentedProperty;
					var propertyNode = new property();
					propertyNode.name = representedProperty.Name;
					propertyNode.type1 = representedProperty.Type;
					propertyNode.column = property.MappedColumn().Name.BackTick();
					propertyNode.notnull = true; // This must be true for Component Properties.
					componentNode.AddProperty(propertyNode);
				}
				yield return componentNode;
			}
		}

		private void ProcessInheritance(Entity entity, @class newClass)
		{
			EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithChildren(entity);

			switch (inheritanceType)
			{
				case EntityImpl.InheritanceType.None:
				case EntityImpl.InheritanceType.TablePerConcreteClass:
					// Table per concrete class doesn't need special treatment.
					break;
				case EntityImpl.InheritanceType.TablePerClassHierarchy:
					// All child entities are mapped to the same table as the parent.
					ProcessEntityTablePerClassHierarchy(entity, newClass);
					break;
				case EntityImpl.InheritanceType.TablePerSubClass:
					ProcessEntityTablePerSubClass(entity, newClass);
					break;
				case EntityImpl.InheritanceType.Unsupported:
					throw new Exception("An unsupported inheritance structure was detected. "
										+ "We only support Table Per Class Hierarchy, Table Per Sub Class, and Table Per Concrete Class. "
										+ "See the NHibernate documentation for more details.");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void ProcessInheritance(Entity entity, joinedsubclass newJoinedSubClass)
		{
			EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithChildren(entity);

			switch (inheritanceType)
			{
				case EntityImpl.InheritanceType.None:
				case EntityImpl.InheritanceType.TablePerConcreteClass:
					// Table per concrete class doesn't need special treatment.
					break;
				case EntityImpl.InheritanceType.TablePerClassHierarchy:
					// All child entities are mapped to the same table as the parent.
					//ProcessEntityTablePerClassHierarchy(entity, newJoinedSubClass);
					break;
				case EntityImpl.InheritanceType.TablePerSubClass:
					ProcessEntityTablePerSubClass(entity, newJoinedSubClass);
					break;
				case EntityImpl.InheritanceType.Unsupported:
					throw new Exception("An unsupported inheritance structure was detected. "
										+ "We only support Table Per Class Hierarchy, Table Per Sub Class, and Table Per Concrete Class. "
										+ "See the NHibernate documentation for more details.");
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private key GetKeyNode(ITable joinedTable)
		{
			int count = joinedTable.ColumnsInPrimaryKey.Count();
			key keyNode = new key();

			if (count > 1)
			{
				foreach (var col in joinedTable.ColumnsInPrimaryKey)
				{
					column column = new column { name = col.Name };
					keyNode.AddColumn(column);
				}
			}
			else if (count == 1)
				keyNode.column1 = joinedTable.ColumnsInPrimaryKey.First().Name.BackTick();

			return keyNode;
		}

		public static ITable GetPrimaryTable(Entity entity)
		{
			var mappedTables = entity.MappedTables().ToList();

			if (mappedTables.Count == 0)
			{
				return null;
				//string msg = string.Format(
				//    "Entity {0} does not have a mapped table. It should not be included in the entities to be processed by the HBM file template",
				//    entity.Name);

				//Log.Error(msg);
				//throw new Exception(msg);
			}

			if (mappedTables.Count == 1)
			{
				return mappedTables[0];
			}

			foreach (ITable table in mappedTables)
			{
				// ReSharper disable AccessToModifiedClosure
				if (table.DirectedRelationships.All(r => r.FromTable == table))
					// ReSharper restore AccessToModifiedClosure
					return table;
			}

			return mappedTables[0];
		}

		private IEnumerable<property> ProcessProperties(IEnumerable<Property> properties)//, bool isSingleColumnPK)
		{
			foreach (var property in properties)
			{
				IColumn mappedColumn = property.MappedColumn();
				if (mappedColumn == null)
					continue;

				// Skip key properties 
				if (property.IsKeyProperty)
					continue;

				yield return ProcessProperty(property, mappedColumn);
			}
		}

		private void ProcessEntityKey(Entity entity, @class newClass, ITable table)
		{
			// We do nothing with EntityKeyType.Empty, as there is nothing to do.
			if (entity.Key.KeyType == EntityKeyType.Properties)
				ProcessPropertyEntityKey(entity, newClass, table);
			else if (entity.Key.KeyType == EntityKeyType.Component)
			{
				compositeid cId = GetCompositeIDWithComponent(entity.Key.Component);
				newClass.SetCompositeId(cId);
			}
		}

		private void ProcessPropertyEntityKey(Entity entity, @class newClass, ITable table)
		{
			var keyProperties = entity.Key.Properties;

			bool isSingleColumnPK = keyProperties.Count() == 1;

			if (isSingleColumnPK)
			{
				id newClassId = SetupSingleColumnPrimaryKey(entity);
				newClass.SetId(newClassId);
			}
			else
			{
				compositeid cId = GetCompositeIDWithProperties(table, entity);
				newClass.SetCompositeId(cId);
			}
		}

		private static compositeid GetCompositeIDWithComponent(Component component)
		{
			compositeid cId = new compositeid();
			cId.@class = component.Specification.Name;
			cId.name = component.Name;

			foreach (var property in component.Properties)
			{
				IColumn mappedColumn = property.MappedColumn();
				if (mappedColumn == null)
					continue;

				cId.AddKeyProperty(new keyproperty
				{
					column1 = mappedColumn.Name.BackTick(),
					name = property.PropertyName
				});
			}
			return cId;
		}

		private static compositeid GetCompositeIDWithProperties(ITable table, Entity entity)
		{
			compositeid cId = new compositeid();
			foreach (var property in entity.Key.Properties)
			{
				IColumn mappedColumn = property.MappedColumn();
				if (mappedColumn == null)
					continue;

				cId.AddKeyProperty(new keyproperty
									{
										column1 = mappedColumn.Name.BackTick(),
										name = property.Name
									});
			}
			return cId;
		}

		private property ProcessProperty(Property property, IColumn mappedColumn)
		{
			var propertyNode = new property
			{
				column = mappedColumn.Name.BackTick(),
				name = property.Name,
				type1 = property.NHibernateType
			};

			var options = property.ValidationOptions;

			if (options.MaximumLength.HasValue && options.MaximumLength > 0 && options.MaximumLength != int.MaxValue)
				propertyNode.length = options.MaximumLength.Value.ToString(CultureInfo.InvariantCulture);

			AddExtraInfoToProperty(propertyNode, property);
			return propertyNode;
		}

		private static IColumn GetSingleColumnPrimaryKeyFromChildren(Entity entity, string propertyName)
		{
			foreach (var child in entity.Children)
			{
				Property primaryKeyProperty = child.Key.Properties.FirstOrDefault(p => p.Name == propertyName && p.MappedColumn() != null);

				if (primaryKeyProperty != null)
					return primaryKeyProperty.MappedColumn();

				IColumn col = GetSingleColumnPrimaryKeyFromChildren(child, propertyName);

				if (col != null)
					return col;
			}
			return null;
		}

		private static id SetupSingleColumnPrimaryKey(Entity entity)
		{
			IEnumerable<Property> primaryKeyProperties = entity.Key.Properties;
			Property primaryKeyProperty = primaryKeyProperties.FirstOrDefault(p => p.MappedColumn() != null);
			IColumn column = null;

			if (primaryKeyProperty == null)
			{
				if (entity.IsAbstract)
				{
					// Search the children and grandchildren for the actual mapped column
					primaryKeyProperty = primaryKeyProperties.FirstOrDefault();
					column = GetSingleColumnPrimaryKeyFromChildren(entity, primaryKeyProperty.Name);
				}
				if (column == null)
				{
					var msg = "Could not find mapped property for primary key in entity " + entity.Name;
					Log.Error(msg);
					throw new Exception(msg);
				}
			}
			if (column == null)
				column = primaryKeyProperty.MappedColumn();

			id newClassId = new id
			{
				access = AccessTypes.property.ToString(),
				column1 = column.Name.BackTick(),
				name = primaryKeyProperty.Name,
			};

			//if (column.IsIdentity)
			//{
			if (!string.IsNullOrEmpty(entity.Generator.ClassName) &&
				entity.Generator.ClassName != "unknown")
			{
				newClassId.generator = new generator
				{
					// TODO: handle other generator types that require params
					//@class = GeneratorTypes.native.ToString()
					@class = entity.Generator.ClassName
				};
				bool isCustomGenerator = Enum.GetNames(typeof(GeneratorTypes)).Contains(entity.Generator.ClassName);
				int countOfParams;

				if (isCustomGenerator)
					countOfParams = entity.Generator.Parameters.Where(p => !string.IsNullOrEmpty(p.Value.Trim())).Count();
				else
					countOfParams = entity.Generator.Parameters.Count;

				newClassId.generator.param = new param[countOfParams];

				for (int paramIndex = 0; paramIndex < entity.Generator.Parameters.Count; paramIndex++)
				{
					if (!isCustomGenerator ||
						!string.IsNullOrEmpty(entity.Generator.Parameters[paramIndex].Value.Trim()))
					{
						newClassId.generator.param[paramIndex] = new param();
						newClassId.generator.param[paramIndex].name = entity.Generator.Parameters[paramIndex].Name;
						newClassId.generator.param[paramIndex].Text = new string[] { entity.Generator.Parameters[paramIndex].Value };
					}
				}
			}
			//}
			if (column.Size > 0)
			{
				newClassId.length = column.Size.ToString();
			}
			return newClassId;
		}


		private void ProcessEntityTablePerClassHierarchy(Entity entity, @class classNode)
		{
			foreach (var childEntity in entity.Children)
			{
				var subclassNode = new subclass();

				subclassNode.name = childEntity.Name;
				subclassNode.discriminatorvalue = childEntity.DiscriminatorValue;

				foreach (var property in childEntity.ConcreteProperties)
				{
					IColumn column = property.MappedColumn();
					if (column == null) continue;

					property propNode = ProcessProperty(property, column);
					subclassNode.AddProperty(propNode);
				}
				var referenceMapper = new ReferenceMapper();
				referenceMapper.ProcessReferences(childEntity, item => subclassNode.AddItem(item));

				classNode.AddItem1(subclassNode);
			}
			classNode.discriminator = new discriminator();
			classNode.discriminator.force = entity.Discriminator.Force;
			//classNode.discriminator.length = entity.Discriminator.l.Force.ToString();
			//classNode.discriminator.notnull = entity.Discriminator..Force.ToString();

			if (!entity.Discriminator.Insert)
			{
				classNode.discriminator.insert = entity.Discriminator.Insert;
			}
			if (entity.Discriminator.DiscriminatorType == Providers.EntityModel.Model.Enums.DiscriminatorTypes.Column)
				classNode.discriminator.column = entity.Discriminator.ColumnName;
			else
				classNode.discriminator.formula = entity.Discriminator.Formula;
		}

		private void ProcessEntityTablePerSubClass(Entity entity, @class classNode)
		{
			foreach (var childEntity in entity.Children)
			{
				var mappedTable = childEntity.MappedTables().First();

				var subclassNode = new joinedsubclass();
				subclassNode.table = mappedTable.Name.BackTick();
				subclassNode.schema = mappedTable.Schema.BackTick();
				subclassNode.name = childEntity.Name;

				key keyNode = GetKeyNode(mappedTable);
				subclassNode.key = keyNode;

				foreach (var property in childEntity.ConcreteProperties)
				{
					IColumn column = property.MappedColumn();
					if (column == null) continue;

					property propNode = ProcessProperty(property, column);
					subclassNode.AddProperty(propNode);
				}
				classNode.AddItem1(subclassNode);

				var referenceMapper = new ReferenceMapper();
				referenceMapper.ProcessReferences(childEntity, item => subclassNode.AddItem(item));

				ProcessInheritance(childEntity, subclassNode);
			}
		}

		private void ProcessEntityTablePerSubClass(Entity entity, joinedsubclass joinedSubClassNode)
		{
			foreach (var childEntity in entity.Children)
			{
				var mappedTable = childEntity.MappedTables().First();

				var subclassNode = new joinedsubclass();
				subclassNode.table = mappedTable.Name.BackTick();
				subclassNode.schema = mappedTable.Schema.BackTick();
				subclassNode.name = childEntity.Name;

				key keyNode = GetKeyNode(mappedTable);
				subclassNode.key = keyNode;

				foreach (var property in childEntity.ConcreteProperties)
				{
					IColumn column = property.MappedColumn();
					if (column == null) continue;

					property propNode = ProcessProperty(property, column);
					subclassNode.AddProperty(propNode);
				}
				joinedSubClassNode.AddJoinedSubclass(subclassNode);

				var referenceMapper = new ReferenceMapper();
				referenceMapper.ProcessReferences(childEntity, item => subclassNode.AddItem(item));
			}
		}
	}

	//internal class DiscriminatorHelper
	//{
	//    public IColumn GetSingleDiscriminatorColumn(Discriminator discriminator)
	//    {
	//        var proc = new BasicDiscriminatorProcessor();
	//        proc.ProcessConditionObject = ProcessConditionObject;
	//        discriminator.AcceptVisitor(proc);

	//        return condition == null ? null : condition.Column;
	//    }

	//    public string GetSingleDiscriminatorValue(Discriminator discriminator)
	//    {
	//        var proc = new BasicDiscriminatorProcessor();
	//        proc.ProcessConditionObject = ProcessConditionObject;
	//        discriminator.AcceptVisitor(proc);

	//        return condition == null ? "" : condition.ExpressionValue.Value;
	//    }

	//    private Condition condition;

	//    /// <summary>
	//    /// Captures the first condition it is passed.
	//    /// </summary>
	//    /// <param name="cond"></param>
	//    private void ProcessConditionObject(Condition cond)
	//    {
	//        if (condition == null) condition = cond;
	//    }
	//}
}
