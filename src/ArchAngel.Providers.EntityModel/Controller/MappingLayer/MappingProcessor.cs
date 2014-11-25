using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Controller.MappingLayer
{
	public class MappingProcessor
	{
		private readonly EntityProcessor processor;

		public MappingProcessor(EntityProcessor processor)
		{
			if (processor == null) throw new ArgumentNullException("processor");
			this.processor = processor;
		}

		/// <summary>
		/// Creates one Entity per database table, and one Reference per database relationship.
		/// It then looks for association tables and maps them correctly. 
		/// </summary>
		/// <param name="database"></param>
		/// <returns></returns>
		public MappingSet CreateOneToOneMapping(IDatabase database, List<string> tablePrefixes, List<string> columnPrefixes, List<string> tableSuffixes, List<string> columnSuffixes)
		{
			EntitySet entitySet = new EntitySetImpl();
			MappingSet set = new MappingSetImpl(database, entitySet);
			set.TablePrefixes = tablePrefixes;
			set.ColumnPrefixes = columnPrefixes;
			set.TableSuffixes = tableSuffixes;
			set.ColumnSuffixes = columnSuffixes;
			entitySet.MappingSet = set;

			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ExistingEntityNames = new List<string>();
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.TablePrefixes = (List<string>)tablePrefixes;
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ColumnPrefixes = (List<string>)columnPrefixes;
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.TableSuffixes = (List<string>)tableSuffixes;
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ColumnSuffixes = (List<string>)columnSuffixes;

			foreach (var table in database.Tables)
				ProcessTable(set, table);

			foreach (var view in database.Views)
				ProcessTable(set, view);

			foreach (var relationship in database.Relationships)
				ProcessRelationship(set, relationship);

			CreateManyToManyMappings(entitySet.Entities.ToList(), entitySet);
			return set;
		}

		internal static void CreateManyToManyMappings(List<Entity> entities, EntitySet entitySet)
		{
			CreateManyToManyMappings(entities, entitySet, false);
		}

		internal static void CreateManyToManyMappings(List<Entity> entities, EntitySet entitySet, bool keepAssociationEntites)
		{
			// Now that we've built up all of the Entities and References, try to find many to many associations,
			// delete the entity we created from the association table, and map things up correctly.

			// Association Tables will have two foreign keys on them, and all of the columns will belong to one
			// or the other key.

			foreach (var entity in entities.OrderBy(e => e.DirectedReferences.Where(d => Cardinality.Many.Equals(d.ToEndCardinality)).Count()))
			{
				// Association tables should only have two foreign keys.
				//if (associationTable.ForeignKeys.Count() != 2) continue;
				if (entity.DirectedReferences.Count() < 2) continue;

				// Get the association table. We know there is only one mapped table because we
				// created this entity from it above.
				var associationTable = entity.MappedTables().Single();

				//var fk1 = associationTable.ForeignKeys.First();
				//var fk2 = associationTable.ForeignKeys.Last();

				//// All columns in the table should be in either of the foreign keys, or the primary key
				//if (associationTable.Columns.Any(c => fk1.Columns.Contains(c) == false && fk2.Columns.Contains(c) == false && !c.IsIdentity && !c.InPrimaryKey))
				//    continue;

				// All columns in the table should be in either of the foreign keys, or the primary key
				if (associationTable.Columns.Any(c => !c.InPrimaryKey && !c.IsIdentity && !associationTable.ForeignKeys.Any(fk => fk.Columns.Contains(c))))
					continue;

				List<DirectedReference> directedRefs = entity.DirectedReferences.Where(d => Cardinality.Many.Equals(d.ToEndCardinality)).ToList();
				bool oneStillExists = false;

				for (int i = directedRefs.Count - 1; i >= 0; i--)
				{
					var d1 = directedRefs[i];

					if (d1.Reference.MappedTable() != null)
					{
						oneStillExists = true;
						continue;
					}
					foreach (var d2 in directedRefs.Where(d => d != d1))
					{
						if (d2.Reference.MappedTable() != null)
						{
							oneStillExists = true;
							continue;
						}
						var entity1 = d1.ToEntity;
						var entity2 = d2.ToEntity;

						// Don't create m:n references between the same entity
						//if (entity1 == entity2) continue;

						if (entity1.DirectedReferences.Any(dr => dr.ToEntity == entity2))
							continue;

						Cardinality cardinality1 = d1.ToEndCardinality;
						Cardinality cardinality2 = d2.ToEndCardinality;

						// Create the new Many To Many Reference and set it up properly.
						var manyToManyReference = entity1.CreateReferenceTo(entity2);
						manyToManyReference.Cardinality1 = cardinality1;
						manyToManyReference.Cardinality2 = cardinality2;
						manyToManyReference.End1Enabled = true;
						manyToManyReference.End2Enabled = true;

						IList<string> existingNames2 = entity2.GetReferenceNames(entity1);
						manyToManyReference.End1Name = cardinality1 == Cardinality.Many ? manyToManyReference.Entity2.Name.Pluralize().GetNextName(existingNames2) : manyToManyReference.Entity2.Name.GetNextName(existingNames2);
						IList<string> existingNames1 = entity1.GetReferenceNames(entity2);
						manyToManyReference.End2Name = cardinality2 == Cardinality.Many ? manyToManyReference.Entity1.Name.Pluralize().GetNextName(existingNames1) : manyToManyReference.Entity1.Name.GetNextName(existingNames1);

						manyToManyReference.EntitySet = entitySet;

						// Map the reference to the association table
						manyToManyReference.SetMappedTable(associationTable);
					}
					//if (!oneStillExists)
					directedRefs.Remove(d1);
				}
				if (!oneStillExists && !entity.DirectedReferences.Any(d => Cardinality.One.Equals(d.ToEndCardinality)))
				{
					if (!keepAssociationEntites)
					{
						// Remove the entity created from the association table
						entity.DeleteSelf();
					}
				}
			}
		}

		public MappingSet CreateOneToOneMapping(EntitySet entitySet)
		{
			IDatabase database = new Database("Database", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005);
			MappingSet set = new MappingSetImpl(database, entitySet);
			database.MappingSet = set;

			foreach (var entity in entitySet.Entities)
				ProcessEntity(set, entity);

			foreach (var entity in entitySet.Entities)
				PostProcessEntity(set, entity);

			foreach (var reference in entitySet.References)
				ProcessReference(set, reference);

			return set;
		}

		private void ProcessEntity(MappingSet set, Entity entity)
		{
			var table = processor.CreateTable(entity);
			set.Database.AddTable(table);

			CreateMappingForNewTableAndEntity(entity, set, table);
		}

		private void PostProcessEntity(MappingSet set, Entity entity)
		{
			if (entity.HasParent == false) return;

			var childTable = entity.MappedTables().First();
			var parentTable = entity.Parent.MappedTables().First();

			// Create foreign key for parent relationship

			// This code makes a major assumption: That the primary key of the child has the same columns
			// as the primary key of the parent. 
			var name = GetNextKeyName("FK_" + parentTable.Name + "_" + childTable.Name, set);
			var foreignKey = new Key(name, DatabaseKeyType.Foreign);
			var primaryKey = parentTable.FirstPrimaryKey;
			var childPrimaryKey = childTable.FirstPrimaryKey;
			foreignKey.ReferencedKey = primaryKey;
			childTable.AddKey(foreignKey);

			foreach (var column in childPrimaryKey.Columns)
			{
				foreignKey.AddColumn(column.Name);
			}
		}

		private static string GetNextKeyName(string nameTemplate, MappingSet set)
		{
			return nameTemplate.GetNextName(set.Database.Tables.SelectMany(t => t.Keys).Select(k => k.Name));
		}

		private static void ProcessReference(MappingSet set, Reference reference)
		{
			// Figure out what type of reference it is, process each type differently.
			if (Cardinality.IsOneToMany(reference.Cardinality1, reference.Cardinality2))
			{
				ProcessOneToManyReference(set, reference);
			}
			else if (Cardinality.IsOneToOne(reference.Cardinality1, reference.Cardinality2))
			{
				ProcessOneToOneReference(set, reference);
			}
			else if (Cardinality.IsManyToMany(reference.Cardinality1, reference.Cardinality2))
			{
				ProcessManyToManyReference(set, reference);
			}
		}

		private static void ProcessOneToOneReference(MappingSet set, Reference reference)
		{
			var fromTable = reference.Entity1.MappedTables().First();
			var toTable = reference.Entity2.MappedTables().First();

			// Create a foreign key on the to table
			Key foreignKey = CreateForeignKey(set, fromTable, toTable);

			// Create a Unique key from the foreign key we just created. This constrains the
			// relationship between the tables to One to One.
			CreateUniqueKeyFor(foreignKey);

			// Create the relationship between the two.
			var relationship = fromTable.CreateRelationshipUsing(fromTable.FirstPrimaryKey, foreignKey);
			set.ChangeMappingFor(reference).To(relationship);
		}

		private static void ProcessOneToManyReference(MappingSet set, Reference reference)
		{
			var manyEnd = reference.Cardinality1 == Cardinality.Many ? reference.Entity1 : reference.Entity2;
			var directedRef = new DirectedReference(manyEnd, reference);

			var oneEnd = directedRef.ToEntity;
			var oneEndTable = oneEnd.MappedTables().First();
			var manyEndTable = manyEnd.MappedTables().First();

			IKey primaryKey = oneEndTable.FirstPrimaryKey;
			// Create a foreign key on the Many side
			Key foreignKey = CreateForeignKey(set, oneEndTable, manyEndTable);

			// Create the relationship between the two.
			var relationship = oneEndTable.CreateRelationshipUsing(primaryKey, foreignKey);

			set.ChangeMappingFor(reference).To(relationship);
		}

		private static void ProcessManyToManyReference(MappingSet set, Reference reference)
		{
			var table1 = reference.Entity1.MappedTables().First();
			var table2 = reference.Entity2.MappedTables().First();

			// Create association table
			var associationTable = new Table(table1.Name + "_" + table2.Name + "_AssociationTable", table1.Schema);
			set.Database.AddTable(associationTable);
			set.ChangeMappingFor(reference).To(associationTable);

			// Create foreign keys (and columns) on the association table.
			var foreignKey1 = CreateForeignKey(set, table1, associationTable);
			var foreignKey2 = CreateForeignKey(set, table2, associationTable);

			// Create Relationships to the association table.
			table1.CreateRelationshipUsing(table1.FirstPrimaryKey, foreignKey1);
			table2.CreateRelationshipUsing(table2.FirstPrimaryKey, foreignKey2);
		}

		private static void CreateUniqueKeyFor(Key key)
		{
			IKey uniqueKey = new Key("Unique_" + key.Name, DatabaseKeyType.Unique);
			key.Parent.AddKey(uniqueKey);

			foreach (var column in key.Columns)
			{
				uniqueKey.AddColumn(column.Name);
			}
		}

		private static Key CreateForeignKey(MappingSet set, ITable primaryTable, ITable foreignTable)
		{
			var foreignKey = new Key(GetNextKeyName("FK_" + primaryTable.Name + "_" + foreignTable.Name, set), DatabaseKeyType.Foreign);
			var primaryKey = primaryTable.FirstPrimaryKey;
			foreignKey.ReferencedKey = primaryKey;
			foreignTable.AddKey(foreignKey);

			// Create the columns for the foreign keys
			foreach (var column in primaryKey.Columns)
			{
				var newColumn = CloneColumn(primaryTable, foreignTable, column, "Foreign_");
				foreignKey.AddColumn(newColumn.Name);
			}
			return foreignKey;
		}

		private static IColumn CloneColumn(ITable primaryTable, ITable foreignTable, IColumn column, string columnNamePrefix)
		{
			var newColumn = column.Clone();
			var name = columnNamePrefix + primaryTable.Name + "_" + column.Name;
			newColumn.Name = name.GetNextName(foreignTable.Columns.Select(c => c.Name));

			foreignTable.AddColumn(newColumn);

			return newColumn;
		}

		private static void CreateMappingForNewTableAndEntity(Entity entity, MappingSet set, ITable table)
		{
			Mapping mapping = new MappingImpl();
			mapping.ToEntity = entity;
			mapping.FromTable = table;

			for (int i = 0; i < table.Columns.Count; i++)
			{
				mapping.AddPropertyAndColumn(entity.Properties.ElementAt(i), table.Columns[i]);
			}

			set.AddMapping(mapping);
		}

		private void ProcessRelationship(MappingSet set, Relationship relationship)
		{
			ProcessRelationshipInternal(set, relationship, processor);
		}

		public static void ProcessRelationshipInternal(MappingSet set, Relationship relationship, EntityProcessor processor)
		{
			Reference reference = processor.CreateReference(relationship, set.EntitySet);

			if (reference == null)
				return;

			RelationshipReferenceMapping mapping = new RelationshipReferenceMappingImpl();
			mapping.FromRelationship = relationship;
			mapping.ToReference = reference;
			//set.EntitySet.AddReference(reference);
			set.AddMapping(mapping);
		}

		private Entity ProcessTable(MappingSet set, ITable scriptObject)
		{
			Entity entity = processor.CreateEntity(scriptObject);
			//set.EntitySet.AddEntity(entity);

			CreateMappingForNewTableAndEntity(entity, set, scriptObject);
			set.EntitySet.AddEntity(entity);
			return entity;
		}

		public List<Entity> CreateOneToOneMappingsFor(IEnumerable<ITable> tables, MappingSet set)
		{
			return CreateOneToOneMappingsFor(tables, set, false);
		}

		/// <summary>
		/// Creates an Entity for each table specified. Note: This does not create proper Many to Many References!
		/// </summary>
		/// <param name="tables"></param>
		/// <param name="set"></param>
		public List<Entity> CreateOneToOneMappingsFor(IEnumerable<ITable> tables, MappingSet set, bool keepAssociationEntities)
		{
			List<Entity> newEntities = new List<Entity>();

			if (set.EntitySet == null)
				set.EntitySet = new EntitySetImpl();

			// Create new entities for each of the selected tables.
			foreach (var table in tables)
			{
				newEntities.Add(ProcessTable(set, table));
			}

			// Create new References for each of the relationships between the new tables and
			// tables that have already been mapped.

			// Get a set of all Guids for tables that we will want to create references from
			HashSet<Guid> existingTables = new HashSet<Guid>(tables.Select(t => t.InternalIdentifier));

			foreach (var mappedTable in set.Mappings.Select(m => m.FromTable))
				existingTables.Add(mappedTable.InternalIdentifier);

			HashSet<Guid> processedRelationships = new HashSet<Guid>();
			List<Entity> tempEntities = new List<Entity>();

			foreach (var table in tables)
			{
				foreach (var directedRel in table.DirectedRelationships)
				{
					var relationship = directedRel.Relationship;

					if (processedRelationships.Contains(relationship.InternalIdentifier))
						continue; // Skip relationships that have already been handled.
					if (relationship.MappedReferences().Any())
						continue; // Skip relationships that have been mapped by the user.
					if (existingTables.Contains(directedRel.ToTable.InternalIdentifier) == false)
					{
						// Check for possible many-to-many relationships ie: this relationship might 
						// link to an association table, which doesn't have a mapped entity in the model.

						// Call ProcessTable to create a temp entity. Keep a list of these, then delete them once
						// we call CreateManyToManyMappings below
						var tempEntity = ProcessTable(set, directedRel.ToTable);
						tempEntities.Add(tempEntity);
						newEntities.Add(tempEntity);

						ProcessRelationship(set, relationship);
						processedRelationships.Add(relationship.InternalIdentifier);

						// Now, process the 'other side' of these new relationships
						foreach (var directedRel2 in directedRel.ToTable.DirectedRelationships)
						{
							var relationship2 = directedRel2.Relationship;

							if (processedRelationships.Contains(relationship2.InternalIdentifier))
								continue; // Skip relationships that have already been handled.
							if (relationship2.MappedReferences().Any())
								continue; // Skip relationships that have been mapped by the user.
							if (existingTables.Contains(directedRel2.ToTable.InternalIdentifier) == false)
								continue; // Skip relationships that have tables that have no mapped Entity

							ProcessRelationship(set, relationship2);
							processedRelationships.Add(relationship2.InternalIdentifier);
						}
						continue; // Skip relationships that have tables that have no mapped Entity
					}
					ProcessRelationship(set, relationship);
					processedRelationships.Add(relationship.InternalIdentifier);
				}
			}
			CreateManyToManyMappings(newEntities, set.EntitySet, keepAssociationEntities);

			// Delete the temp added entities that were used to discover many-to-many relationships
			foreach (var entity in tempEntities)
			{
				newEntities.Remove(entity);
				entity.DeleteSelf();
			}
			return newEntities;
		}

		public string PrintMappingInformation(MappingSet mappings)
		{
			StringBuilder sb = new StringBuilder(100);
			sb.AppendLine("*** Mapping Information ***");
			sb.AppendLine("Database Layer\t->\tEntity Layer");
			foreach (var mapping in mappings.Mappings)
			{
				Entity entity = mapping.ToEntity;
				ITable dbObject = mapping.FromTable;

				foreach (var map in mapping.Mappings)
				{
					Property property = map.Property;
					IColumn column = map.Column;
					sb.AppendFormat("{0}.{1}\t->\t{2}.{3}", dbObject.Name, column.Name, entity.Name, property.Name).AppendLine();
				}

			}

			return sb.ToString();
		}
	}
}
