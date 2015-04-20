using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Controller.MappingLayer
{
	public class OneToOneEntityProcessor : EntityProcessor
	{
		private readonly List<string> currentlyUsedNames;

		public OneToOneEntityProcessor()
		{
			this.currentlyUsedNames = new List<string>();
		}

		public OneToOneEntityProcessor(IEnumerable<string> currentlyUsedEntityNames)
		{
			this.currentlyUsedNames = currentlyUsedEntityNames.ToList();
		}

		public Entity CreateEntity(ITable table)
		{
			//string newName = table.Name.RemovePrefixes(table.GetMappingSet().TablePrefixes).Singularize().GetCSharpFriendlyIdentifier().GetNextName(currentlyUsedNames);

			ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable newTable = new Interfaces.Scripting.NHibernate.Model.ITable(table.Database.Name)
			{
				Name = table.Name,
				Schema = table.Schema
			};
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ExistingPropertyNames = new List<string>();
			string newName = ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.GetEntityName(newTable);
			currentlyUsedNames.Add(newName);

			Entity entity = new EntityImpl(newName)
			{
				Schema = table.Schema.GetCSharpFriendlyIdentifier()
			};
			SetUserOption(entity, "Entity_Mutable", !table.IsView);

			foreach (var column in table.Columns)
			{
				Property prop = CreateProperty(column);
				entity.AddProperty(prop);

				if (prop.IsKeyProperty)
					entity.Key.AddProperty(prop);
			}
			//ProcessTable(table, entity);
			string sequenceName = "";

			if (table.Database.DatabaseType == DatabaseLayer.DatabaseTypes.PostgreSQL)
			{
				var seqColumn = table.ColumnsInPrimaryKey.FirstOrDefault(c => c.IsIdentity && c.Default.Contains("nextval"));

				if (seqColumn != null)
				{
					string sequence = seqColumn.Default.Replace("nextval('", "");
					sequence = sequence.Substring(0, sequence.IndexOf("'"));

					if (sequence.Contains("."))
					{
						string[] parts = sequence.Split('.');

						if (parts[0].Equals(table.Schema, StringComparison.InvariantCultureIgnoreCase))
							sequence = parts[1];
					}
					sequenceName = sequence;
				}
			}
			if (!string.IsNullOrWhiteSpace(sequenceName))
			{
				entity.Generator.ClassName = "sequence";
				entity.Generator.Parameters.Add(new EntityGenerator.Parameter("sequence", sequenceName));
			}
			else if (table.ColumnsInPrimaryKey.Count() == 1 &&
				table.ColumnsInPrimaryKey.ElementAt(0).IsIdentity)
			{
				entity.Generator.ClassName = "native";
			}
			else
				entity.Generator.ClassName = "assigned";

			foreach (var property in entity.Properties)
				property.Name = property.Name.GetNextName(new string[1] { entity.Name });

			return entity;
		}

		private static void SetUserOption<T>(IScriptBaseObject obj, string optionName, T value)
		{
			var option = obj.Ex.FirstOrDefault(uo => uo.Name == optionName);

			if (option != null)
				option.Value = value;
			else
				obj.AddUserOption(new UserOption(optionName, typeof(T), value));
		}
		public Property CreateProperty(IColumn column)
		{
			return CreatePropertyFromColumn(column);
		}

		public static Property CreatePropertyFromColumn(IColumn column)
		{
			var prop = new PropertyImpl();

			//prop.Name = column.Name.ToTitleCase();
			ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn newCol = new Interfaces.Scripting.NHibernate.Model.IColumn()
			{
				IsNullable = column.IsNullable,
				//IsText = 
				Length = column.Size,
				Name = column.Name,
				ScriptObject = column,
				Type = column.OriginalDataType
			};
			prop.Name = ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.GetPropertyName(newCol);
			prop.ValidationOptions.Nullable = column.IsNullable;
			prop.ValidationOptions.MaximumLength = column.SizeIsMax ? (long?)null : column.Size;
			prop.ValidationOptions.IntegerDigits = column.Precision > 0 ? column.Precision - column.Scale : (int?)null;
			prop.ReadOnly = column.IsReadOnly;
			prop.ValidationOptions.FractionalDigits = column.Scale > 0 ? column.Scale : (int?)null;
			prop.Type = ConvertType(column);
			prop.IsKeyProperty = column.InPrimaryKey;
			return prop;
		}

		//private void ProcessTable(ITable table, Entity entity)
		//{
		//    foreach (var column in table.ColumnsInPrimaryKey)
		//        entity.Key.AddProperty(entity.GetProperty(column.Name.ToTitleCase()));
		//}

		public ITable CreateTable(Entity entity)
		{
			ITable table = new Table(entity.Name.Pluralize().GetNextName(currentlyUsedNames), entity.Schema);

			foreach (var property in entity.ConcreteProperties)
			{
				var column = CreateColumn(property);
				table.AddColumn(column);
			}

			// Create primary key
			var key = new Key("PK_" + entity.Name, DatabaseKeyType.Primary);
			table.AddKey(key);

			var keyProperties = entity.Key.Properties.ToList();

			foreach (var property in keyProperties)
			{
				var name = property.Name;

				// Don't create columns for parent key columns. We will do this in the PostProcess step.
				if (table.Columns.Any(c => c.Name == name) == false)
					continue;

				key.AddColumn(name);
			}
			return table;
		}

		public IColumn CreateColumn(Property property)
		{
			return CreateColumnFromProperty(property);
		}

		public static IColumn CreateColumnFromProperty(Property property)
		{
			Column column = new Column(property.Name);

			column.IsNullable = property.ValidationOptions.Nullable ?? true;

			var options = property.ValidationOptions;

			if (options.MaximumLength.HasValue)
				column.Size = options.MaximumLength.Value;

			if (options.IntegerDigits.HasValue)
				column.Precision = options.IntegerDigits.Value + (options.FractionalDigits ?? 0);

			if (options.FractionalDigits.HasValue)
				column.Scale = options.FractionalDigits.Value;

			column.IsReadOnly = property.ReadOnly;
			column.OriginalDataType = ConvertToDatabaseType(property);
			return column;
		}

		public IColumn CreateColumn(ComponentProperty property)
		{
			Column column = new Column(property.Name);

			column.IsNullable = false;
			var options = property.ValidationOptions;

			if (options.MaximumLength.HasValue)
				column.Size = options.MaximumLength.Value;

			if (options.IntegerDigits.HasValue)
				column.Precision = options.IntegerDigits.Value + (options.FractionalDigits ?? 0);

			if (options.FractionalDigits.HasValue)
				column.Scale = options.FractionalDigits.Value;

			column.IsReadOnly = false;
			column.OriginalDataType = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetDefaultDatabaseType(column.Database.DatabaseType.ToString(), property.Type);
			column.IsNullable = property.Type.Trim().EndsWith("?");
			return column;
		}

		public Reference CreateReference(Relationship relationship, EntitySet entities)
		{
			return CreateReferenceFromRelationship(relationship, entities);
		}

		private static Reference CreateReferenceFromRelationship(Relationship relationship, EntitySet entitySet)
		{
			Reference reference = new ReferenceImpl();
			reference.Name = relationship.Name;
			reference.Entity1 = relationship.PrimaryTable.MappedEntities().FirstOrDefault();
			reference.Entity2 = relationship.ForeignTable.MappedEntities().FirstOrDefault();

			if (reference.Entity1 == null || reference.Entity2 == null)
				return null;

			// Don't add a reference if an inheritance structure exists between the two entities
			if ((reference.Entity1.Parent != null && reference.Entity1.Parent == reference.Entity2) ||
				(reference.Entity2.Parent != null && reference.Entity2.Parent == reference.Entity1))
			{
				return null;
			}
			reference.Cardinality1 = relationship.PrimaryCardinality;
			reference.Cardinality2 = relationship.ForeignCardinality;

			IList<string> existingNames1 = reference.Entity1.GetReferenceNames(reference.Entity2);
			IList<string> existingNames2 = reference.Entity2.GetReferenceNames(reference.Entity1);

			if (reference.Entity1 == reference.Entity2)
			{
				existingNames1.Add(reference.Entity1.Name);
				reference.End1Name = reference.Cardinality1 == Cardinality.Many ? reference.Entity2.Name.Pluralize().GetNextName(existingNames2) : reference.Entity2.Name.GetNextName(existingNames2);

				if (relationship.PrimaryKey.Columns.Count == 1)
				{
					if (relationship.ForeignKey == null)
						throw new Exception(string.Format("Relationship [{0}] on table [{1}] has no foreign key.", relationship.Name, relationship.PrimaryTable.Name));
					else if (relationship.ForeignKey.Columns.Count != 1)
						throw new Exception(string.Format("The foreign key [{0}] of relationship [{1}] on table [{2}] should have 1 column (to match the number of columns in the primary  key). It has {3} columns.", relationship.ForeignKey.Name, relationship.Name, relationship.PrimaryTable.Name, relationship.ForeignKey.Columns.Count));

					reference.End2Name = relationship.ForeignKey.Columns[0].Name.GetCSharpFriendlyIdentifier().GetNextName(existingNames1);
				}
				else
					reference.End2Name = reference.Cardinality2 == Cardinality.Many ? reference.Entity1.Name.Pluralize().GetNextName(existingNames1) : reference.Entity1.Name.GetNextName(existingNames1);
			}
			else
			{
				reference.End1Name = reference.Cardinality1 == Cardinality.Many ? reference.Entity2.Name.Pluralize().GetNextName(existingNames2) : reference.Entity2.Name.GetNextName(existingNames2);
				reference.End2Name = reference.Cardinality2 == Cardinality.Many ? reference.Entity1.Name.Pluralize().GetNextName(existingNames1) : reference.Entity1.Name.GetNextName(existingNames1);
			}
			reference.End1Enabled = true;
			reference.End2Enabled = true;
			reference.EntitySet = entitySet;
			entitySet.AddReference(reference);
			reference.Entity1.AddReference(reference);

			if (reference.Entity1 != reference.Entity2)
				reference.Entity2.AddReference(reference);

			reference.SetMappedRelationship(relationship);

			return reference;
		}

		public static string ConvertType(IColumn column)
		{
			ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.ColumnInfo columnInfo = new Interfaces.ProjectOptions.TypeMappings.Utility.ColumnInfo()
			{
				IsNullable = column.IsNullable,
				Name = column.Name,
				Precision = column.Precision,
				Scale = column.Scale,
				Size = column.Size,
				TypeName = column.OriginalDataType
			};
			return ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetCSharpTypeName(column.Parent.Database.DatabaseType.ToString(), columnInfo);
		}

		private static string ConvertToDatabaseType(Property property)
		{
			//throw new NotImplementedException("TODO: need to specify default reverse mappings: C# types -> DB types.");
			return "";
		}

		//private static SQLServer.UniDbTypes ConvertToDatabaseType(Property property)
		//{
		//    if ((property.Type == "System.String" || property.Type == "string") &&
		//        (property.ValidationOptions.MaximumLength.HasValue && property.ValidationOptions.MaximumLength > 0))
		//    {
		//        return SQLServer.UniDbTypes.NVarChar;
		//    }
		//    return SQLServer.ConvertCLRTypeNameToSQLTypeName(property.Type);
		//}

		private SQLServer.UniDbTypes ConvertToDatabaseType(ComponentProperty property)
		{
			if ((property.Type == "System.String" || property.Type == "string") &&
				(property.ValidationOptions.MaximumLength.HasValue && property.ValidationOptions.MaximumLength > 0))
			{
				return SQLServer.UniDbTypes.NVarChar;
			}
			return SQLServer.ConvertCLRTypeNameToSQLTypeName(property.Type);
		}
	}

	public interface EntityProcessor
	{
		Entity CreateEntity(ITable table);
		Reference CreateReference(Relationship relationship, EntitySet entities);
		Property CreateProperty(IColumn column);
		ITable CreateTable(Entity entity);
		IColumn CreateColumn(Property property);
		IColumn CreateColumn(ComponentProperty property);
	}
}
