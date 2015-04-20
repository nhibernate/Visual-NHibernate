using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Model
{
	public static class DatabaseExtensions
	{
		public static IEnumerable<Property> MappedProperties(this IColumn column)
		{
			MappingSet ms = column.GetMappingSet();
			return ms == null ? new List<Property>() : ms.GetMappedPropertiesFor(column);
		}

		public static IEnumerable<Entity> MappedEntities(this ITable table)
		{
			MappingSet ms = table.GetMappingSet();
			return ms == null ? new List<Entity>() : ms.GetMappedEntitiesFor(table);
		}

		public static IEnumerable<Reference> MappedReferences(this ITable table)
		{
			MappingSet ms = table.GetMappingSet();
			return ms == null ? new List<Reference>() : ms.GetMappedReferencesFor(table);
		}

		public static IEnumerable<Reference> MappedReferences(this Relationship relationship)
		{
			MappingSet ms = relationship.GetMappingSet();
			return ms == null ? new List<Reference>() : ms.GetMappedReferencesFor(relationship);
		}

		public static IEnumerable<ITable> AssociationTables(this IDatabase database)
		{
			MappingSet ms = database.MappingSet;
			return ms == null ? new List<ITable>() : ms.GetAssociationTablesFor(database);
		}

		/// <summary>
		/// Gets possible association tables between this entity and the supplied entity.
		/// </summary>
		/// <param name="entity1"></param>
		/// <param name="secondEntity"></param>
		/// <param name="CardinalityPrimary"></param>
		/// <param name="CardinalityForeign"></param>
		/// <returns></returns>
		public static ITable GetAssociationTable(this Entity entity1, Entity secondEntity, out Cardinality CardinalityPrimary, out Cardinality CardinalityForeign, out IKey mainKey, out IKey associationKey)
		{
			CardinalityPrimary = null;
			CardinalityForeign = null;
			mainKey = null;
			associationKey = null;

			ITable associationTable = null;

			foreach (ITable table in entity1.GetMappingSet().Database.AssociationTables())
			{
				//ITable primaryTable = table.Relationships.FirstOrDefault(r => entity1.MappedTables().Contains(r.PrimaryTable));
				bool primaryFound = false;
				bool foreignFound = false;

				#region Primary
				foreach (Relationship relationship in table.Relationships)
				{
					if (entity1.MappedTables().Contains(relationship.PrimaryTable))
					{
						associationTable = table;
						CardinalityPrimary = relationship.ForeignKey.IsUnique ? Cardinality.One : Cardinality.Many;// relationship.PrimaryCardinality;
						//CardinalityForeign = relationship.ForeignCardinality;
						mainKey = relationship.PrimaryKey;
						//associationKey = relationship.ForeignKey;
						primaryFound = true;
						break;
					}
					else if (entity1.MappedTables().Contains(relationship.ForeignTable))
					{
						associationTable = table;// relationship.ForeignTable;
						CardinalityPrimary = relationship.PrimaryKey.IsUnique ? Cardinality.One : Cardinality.Many;// relationship.ForeignCardinality;
						//CardinalityForeign = relationship.PrimaryCardinality;
						mainKey = relationship.ForeignKey;
						//associationKey = relationship.PrimaryKey;
						primaryFound = true;
						break;
					}
				}
				#endregion

				#region Foreign
				foreach (Relationship relationship in table.Relationships)
				{
					if (secondEntity.MappedTables().Contains(relationship.ForeignTable))
					{
						associationTable = table;
						//CardinalityPrimary = relationship.PrimaryCardinality;
						CardinalityForeign = relationship.PrimaryKey.IsUnique ? Cardinality.One : Cardinality.Many;// relationship.ForeignCardinality;
						//mainKey = relationship.PrimaryKey;
						associationKey = relationship.ForeignKey;
						foreignFound = true;
						break;
					}
					else if (secondEntity.MappedTables().Contains(relationship.PrimaryTable))
					{
						//associationTable = table;// relationship.ForeignTable;
						//CardinalityPrimary = relationship.ForeignCardinality;
						CardinalityForeign = relationship.ForeignKey.IsUnique ? Cardinality.One : Cardinality.Many;// relationship.PrimaryCardinality;
						//mainKey = relationship.ForeignKey;
						associationKey = relationship.PrimaryKey;
						foreignFound = true;
						break;
					}
				}
				#endregion

				if (primaryFound && foreignFound)
				{
					break;
				}
				else
				{
					associationTable = null;
					CardinalityPrimary = null;
					CardinalityForeign = null;
					mainKey = null;
					associationKey = null;
				}

				//                if (entity1.MappedTables().Contains(relationship.PrimaryTable))// &&
				//                //secondEntity.MappedTables().Contains(relationship.ForeignTable))
				//                {
				//                    associationTable = table;// relationship.PrimaryTable;
				//                    CardinalityPrimary = relationship.PrimaryCardinality;
				//                    CardinalityForeign = relationship.ForeignCardinality;
				//                    mainKey = relationship.PrimaryKey;
				//                    associationKey = relationship.ForeignKey;

				//                    if (oneFound)
				//                        break;
				//                    else
				//                        oneFound = true;
				//                }
				//                else if (entity1.MappedTables().Contains(relationship.ForeignTable) &&
				//secondEntity.MappedTables().Contains(relationship.PrimaryTable))
				//                {
				//                    associationTable = table;// relationship.ForeignTable;
				//                    CardinalityPrimary = relationship.ForeignCardinality;
				//                    CardinalityForeign = relationship.PrimaryCardinality;
				//                    mainKey = relationship.ForeignKey;
				//                    associationKey = relationship.PrimaryKey;

				//                    if (oneFound)
				//                        break;
				//                    else
				//                        oneFound = true;
				//                }
				//}
			}
			if (associationTable != null)
				return associationTable;

			ITable unpureAssociationTable = null;

			if (associationTable == null)
			{
				foreach (ITable table in entity1.MappedTables())
				{
					foreach (Relationship relationship in table.Relationships)
					{
						ITable possibleAssociationTable = null;

						if (entity1.MappedTables().Contains(relationship.PrimaryTable) &&
							Cardinality.IsOneToMany(relationship.ForeignCardinality, relationship.PrimaryCardinality))
						{
							possibleAssociationTable = relationship.ForeignTable;
							CardinalityPrimary = relationship.PrimaryCardinality;
							mainKey = relationship.ForeignKey;
						}
						else if (entity1.MappedTables().Contains(relationship.ForeignTable) &&
							Cardinality.IsOneToMany(relationship.PrimaryCardinality, relationship.ForeignCardinality))
						{
							possibleAssociationTable = relationship.PrimaryTable;
							CardinalityPrimary = relationship.ForeignCardinality;
							mainKey = relationship.PrimaryKey;
						}
						if (possibleAssociationTable != null)
						{
							foreach (ITable otherTable in secondEntity.MappedTables())
							{
								foreach (Relationship otherRelationship in otherTable.Relationships.Where(r => r.PrimaryTable == possibleAssociationTable || r.ForeignTable == possibleAssociationTable))
								{
									if (otherRelationship.PrimaryTable == otherTable &&
										Cardinality.IsOneToMany(otherRelationship.ForeignCardinality, otherRelationship.PrimaryCardinality) &&
										possibleAssociationTable.Relationships.Where(r => r.PrimaryTable == otherRelationship.ForeignTable || r.ForeignTable == otherRelationship.ForeignTable).Count() > 0)
									{
										unpureAssociationTable = otherRelationship.ForeignTable;
										CardinalityForeign = otherRelationship.PrimaryCardinality;
										associationKey = otherRelationship.ForeignKey;
										return unpureAssociationTable;
									}
									else if (otherRelationship.ForeignTable == otherTable &&
											 Cardinality.IsOneToMany(otherRelationship.PrimaryCardinality, otherRelationship.ForeignCardinality) &&
											 possibleAssociationTable.Relationships.Where(r => r.PrimaryTable == otherRelationship.PrimaryTable || r.ForeignTable == otherRelationship.PrimaryTable).Count() > 0)
									{
										unpureAssociationTable = otherRelationship.PrimaryTable;
										CardinalityForeign = otherRelationship.ForeignCardinality;
										associationKey = otherRelationship.PrimaryKey;
										return unpureAssociationTable;
									}
								}
							}
						}
					}
				}
			}
			return null;
		}
	}
}