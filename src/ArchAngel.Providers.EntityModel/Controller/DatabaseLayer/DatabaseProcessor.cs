using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class DatabaseProcessor
	{
		public bool LogErrors = false;
		public List<Exception> Errors = new List<Exception>();
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DatabaseProcessor));

		public void CreateRelationships(IDatabase db)
		{
			if (db == null)
			{
				log.Error("Cannot create Relationships for a null database");
				throw new ArgumentNullException("db");
			}
			log.DebugFormat("Creating Relationships for database {0}", db.Name);

			foreach (Table table in db.Tables)
				CreateRelationships(table);

			log.DebugFormat("Successfully created Relationships for database {0}", db.Name);
		}

		public void CreateRelationships(Table table)
		{
			foreach (Key key in table.Keys)
			{
				try
				{
					if (key.Keytype != DatabaseKeyType.Foreign)
						continue;

					if (key.ReferencedKey == null)
						//continue;
						throw new DatabaseSchemaInvalidException(string.Format("Foreign key \"{0}\" on table \"{1}\" has no matching key on the referenced table.", key.Name, table.Name));

					if (key.Columns.Count != key.ReferencedKey.Columns.Count)
						throw new DatabaseSchemaInvalidException(string.Format("Foreign key \"{0}\" has {2} columns than its referenced key \"{1}\"", key.Name, key.ReferencedKey.Name, (key.Columns.Count > key.ReferencedKey.Columns.Count ? "more" : "less")));

					//if(key.ReferencedKey.Keytype != DatabaseKeyType.Primary)
					//{
					//    throw new DatabaseSchemaInvalidException(string.Format("Foreign key \"{0}\" refers to a non-primary key \"{1}\". The key is of type {2}", key.Name, key.ReferencedKey.Name, key.ReferencedKey.Keytype));
					//}

					ITable referencedTable = key.ReferencedKey.Parent;

					Relationship rel = new RelationshipImpl
										{
											PrimaryCardinality = key.IsUnique ? Cardinality.One : Cardinality.Many,
											ForeignCardinality = key.ReferencedKey.IsUnique ? Cardinality.One : Cardinality.Many,
											PrimaryTable = referencedTable,
											ForeignTable = key.Parent,
											Name = key.Name,
											PrimaryKey = key.ReferencedKey,
											ForeignKey = key
										};

					table.AddRelationship(rel);
					referencedTable.AddRelationship(rel);
				}
				catch (Exception e)
				{
					if (LogErrors)
						Errors.Add(e);
					else
						throw;
				}
			}
		}

		public DatabaseMergeResult MergeDatabases(IDatabase db1, IDatabase db2)
		{
			DatabaseMergeResult mergeResult = new DatabaseMergeResult();

			#region Tables
			IEnumerable<ITable> tables1 = db1.Tables;
			IEnumerable<ITable> tables2 = db2.Tables;

			List<ITable> newTables = tables1.Compliment(tables2).ToList();
			List<ITable> removedTables = tables2.Compliment(tables1).ToList();

			// Check for changed tables.
			Func<ITable, string> sorter = n => n.Name;
			Table.TableComparer comparer = new Table.TableComparer();
			IEnumerable<ITable> intersection1 = tables1.Intersect(tables2, comparer).OrderBy(sorter);
			var commonItems = intersection1.Zip(tables2.Intersect(tables1, comparer).OrderBy(sorter));

			foreach (var memberPair in commonItems)
				if (memberPair.Key.HasChanges(memberPair.Value))
					mergeResult.AddTableOperation(new TableChangeOperation(memberPair.Key, memberPair.Value));

			foreach (var t in newTables)
				mergeResult.AddTableOperation(new TableAdditionOperation(db1, t));

			foreach (var t in removedTables)
				mergeResult.AddTableOperation(new TableRemovalOperation(t));

			TableProcessor processor = new TableProcessor();
			var tableResults = processor.ProcessTables(tables1, tables2);
			mergeResult.CopyFrom(tableResults);
			#endregion

			#region Views
			IEnumerable<ITable> views1 = db1.Views;
			IEnumerable<ITable> views2 = db2.Views;

			List<ITable> newViews = views1.Compliment(views2).ToList();
			List<ITable> removedViews = views2.Compliment(views1).ToList();

			// Check for changed tables.
			IEnumerable<ITable> viewsIntersection1 = views1.Intersect(views2, comparer).OrderBy(sorter);
			var commonViewItems = intersection1.Zip(views2.Intersect(views1, comparer).OrderBy(sorter));

			foreach (var memberPair in commonViewItems)
				if (memberPair.Key.HasChanges(memberPair.Value))
					mergeResult.AddViewOperation(new TableChangeOperation(memberPair.Key, memberPair.Value));

			foreach (var t in newViews)
				mergeResult.AddViewOperation(new TableAdditionOperation(db1, t));

			foreach (var t in removedViews)
				mergeResult.AddViewOperation(new TableRemovalOperation(t));

			TableProcessor viewProcessor = new TableProcessor();
			var viewResults = processor.ProcessTables(views1, views2);
			mergeResult.CopyFrom(viewResults);
			#endregion

			return mergeResult;
		}
	}
}