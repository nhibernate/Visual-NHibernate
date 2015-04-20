using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IMergeOperation<T> where T : class
	{
		T Object { get; set; }
		string DisplayName { get; }
		void RunOperation();
		void NotApplied();
		IEnumerable<ChangeToNote> ChangesToNote { get; }
		string Description { get; set; }
	}

	public class ChangeToNote
	{
		public IModelObject ObjectToInspect { get; set; }
		public string TextForUser { get; set; }

		public ChangeToNote(IModelObject objectToInspect, string textForUser)
		{
			ObjectToInspect = objectToInspect;
			TextForUser = textForUser;
		}

		public override int GetHashCode()
		{
			return this.ObjectToInspect.GetHashCode() ^ this.TextForUser.GetHashCode();
		}
	}

	public abstract class MergeOperation<T> : IMergeOperation<T> where T : class
	{
		public T Object { get; set; }
		public virtual string DisplayName { get; protected set; }
		public string Description { get; set; }

		public void RunOperation()
		{
			if (Object == null)
				throw new InvalidOperationException("Cannot apply changes to null object");

			RunOperationInternal();
		}

		protected abstract void RunOperationInternal();

		public virtual void NotApplied()
		{
		}

		public abstract IEnumerable<ChangeToNote> ChangesToNote { get; }
	}

	public interface ITwoStepMergeOperation
	{
		void RunSecondStep();
	}

	#region Column operations

	public class ColumnChangeOperation : MergeOperation<IColumn>
	{
		private readonly IColumn columnToCopyFrom;
		public string Description { get; set; }

		public ColumnChangeOperation(IColumn columnToChange, IColumn columnToCopyFrom, string description)
		{
			Object = columnToChange;
			this.columnToCopyFrom = columnToCopyFrom;
			this.Description = description;
		}

		public override string DisplayName
		{
			get { return Object.Parent.Name; }
		}

		protected override void RunOperationInternal()
		{
			// Update any MappedProperties with the new data-type
			if (columnToCopyFrom.OriginalDataType != Object.OriginalDataType)
			{
				string newPropertyType = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.ConvertType(columnToCopyFrom);

				foreach (var prop in Object.MappedProperties())
					prop.Type = newPropertyType;
			}
			columnToCopyFrom.CopyInto(Object);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return ColumnChangeInformation(columnToCopyFrom, Object, "");
		}

		internal static string ColumnChangeInformation(IColumn original, IColumn newVersion, string tabs)
		{
			var sb = new StringBuilder();

			sb.AppendFormat("Column {0} on Table {1} Changed", newVersion.Name, newVersion.Parent.Name);

			if (original.OriginalDataType != newVersion.OriginalDataType)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("Datatype changed from \'{0}\' to \'{1}\'", original.OriginalDataType, newVersion.OriginalDataType);
			if (original.Default != newVersion.Default)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("Default changed from \'{0}\' to \'{1}\'", original.Default, newVersion.Default);
			if (original.IsNullable != newVersion.IsNullable)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("IsNullable changed from {0} to {1}", original.IsNullable, newVersion.IsNullable);
			if (original.InPrimaryKey != newVersion.InPrimaryKey)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("InPrimaryKey changed from {0} to {1}", original.InPrimaryKey, newVersion.InPrimaryKey);
			if (original.IsIdentity != newVersion.IsIdentity)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("IsIdentity changed from {0} to {1}", original.IsIdentity, newVersion.IsIdentity);
			if (original.IsReadOnly != newVersion.IsReadOnly)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("IsReadOnly changed from {0} to {1}", original.IsReadOnly, newVersion.IsReadOnly);
			if (original.IsUnique != newVersion.IsUnique)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("IsUnique changed from {0} to {1}", original.IsUnique, newVersion.IsUnique);
			if (original.Precision != newVersion.Precision)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("Precision changed from {0} to {1}", original.Precision, newVersion.Precision);
			if (original.Scale != newVersion.Scale)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("Scale changed from {0} to {1}", original.Scale, newVersion.Scale);
			if (original.Size != newVersion.Size)
				sb.AppendLine().Append(tabs).Append("\t").AppendFormat("Size changed from {0} to {1}", original.Size, newVersion.Size);

			return sb.ToString();
		}
	}

	public class ColumnAdditionOperation : MergeOperation<IColumn>
	{
		public ITable Table { get; set; }

		public ColumnAdditionOperation(ITable table, IColumn column)
		{
			Table = table;
			Object = column;
		}

		protected override void RunOperationInternal()
		{
			IColumn col = Object.Clone();
			Table.AddColumn(col);

			// Add new column to mapped entities
			foreach (var entity in Table.MappedEntities())
			{
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ExistingPropertyNames = entity.Properties.Select(p => p.Name).ToList();
				ArchAngel.Providers.EntityModel.Model.EntityLayer.Property newProperty = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.CreatePropertyFromColumn(col);
				entity.AddProperty(newProperty);
				newProperty.SetMappedColumn(col);
			}
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string DisplayName
		{
			get { return Table.Name; }
		}

		public override string ToString()
		{
			return PrintNewColumnInformation(Object, "");
		}

		internal static string PrintNewColumnInformation(IColumn column, string startingTabs)
		{
			var sb = new StringBuilder();

			sb.Append(startingTabs).AppendFormat("Column {0} Added on Table {1} ", column.Name, column.Parent.Name);

			//if (!string.IsNullOrEmpty(column.Datatype))
			sb.AppendLine().Append(startingTabs).Append("\t").Append("Type = ").Append(column.OriginalDataType);

			if (!string.IsNullOrEmpty(column.Default))
				sb.AppendLine().Append(startingTabs).Append("\t").Append("Default = ").Append(column.OriginalDataType);
			if (column.IsNullable)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("Nullable = true");
			if (column.InPrimaryKey)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("InPrimaryKey = true");
			if (column.IsIdentity)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("IsIdentity = true");
			if (column.IsReadOnly)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("IsReadOnly = true");
			if (column.IsUnique)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("IsUnique = true");
			if (column.Precision != 0)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("Precision = ").Append(column.Precision);
			if (column.Scale != 0)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("Scale = ").Append(column.Scale);
			if (column.Size != 0)
				sb.AppendLine().Append(startingTabs).Append("\t").Append("Size = ").Append(column.Size);

			return sb.ToString();
		}
	}

	public class ColumnRemovalOperation : MergeOperation<IColumn>
	{
		public ColumnRemovalOperation(IColumn column)
		{
			Object = column;
		}

		public override string DisplayName
		{
			get { return Object.Parent.Name; }
		}

		protected override void RunOperationInternal()
		{
			Object.Parent.RemoveColumn(Object);
			if (Object.Parent is Table)
			{
				Table table = Object.Parent as Table;

				foreach (var key in table.Keys.Where(k => k.Columns.Contains(Object)))
				{
					key.RemoveColumn(Object);
				}

				foreach (var index in table.Indexes.Where(i => i.Columns.Contains(Object)))
				{
					index.RemoveColumn(Object);
				}
				// Remove mapped property from mapped entities
				foreach (var prop in Object.MappedProperties())
					prop.Entity.RemoveProperty(prop);
			}
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get
			{
				var list = new List<ChangeToNote>();

				foreach (var property in Object.MappedProperties())
				{
					list.Add(new ChangeToNote(property, "Mapped Column for Property was deleted"));
				}
				return list;
			}
		}

		public override string ToString()
		{
			return string.Format("Column {0} on Table {1} Removed", Object.Name, Object.Parent.Name);
		}
	}

	#endregion

	#region Key operations

	public class KeyChangeOperation : MergeOperation<IKey>, ITwoStepMergeOperation
	{
		private readonly IKey keyToCopyFrom;
		public string Description { get; set; }

		public KeyChangeOperation(IKey keyToChange, IKey keyToCopyFrom, string description)
		{
			Object = keyToChange;
			this.keyToCopyFrom = keyToCopyFrom;
			this.Description = description;
		}

		public override string DisplayName
		{
			get { return Object.Parent.Name; }
		}

		protected override void RunOperationInternal()
		{
			keyToCopyFrom.CopyInto(Object);
		}

		public void RunSecondStep()
		{
			Object.ClearColumns();

			KeyOperationUtility.FixKeyReferences(Object, keyToCopyFrom);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} Changed", Object.Parent.Database.Name, Object.Parent.Name, Object.Name);
		}
	}

	public class KeyAdditionOperation : MergeOperation<IKey>, ITwoStepMergeOperation
	{
		public ITable Table { get; set; }

		private IKey newKey;

		public KeyAdditionOperation(ITable table, IKey key)
		{
			Table = table;
			Object = key;
		}

		public override string DisplayName
		{
			get { return Table.Name; }
		}

		protected override void RunOperationInternal()
		{
			newKey = Object.Clone();
			Table.AddKey(newKey);
		}

		public void RunSecondStep()
		{
			KeyOperationUtility.FixKeyReferences(newKey, Object);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} Added", Table.Database.Name, Table.Name, Object.Name);
		}
	}

	public class KeyRemovalOperation : MergeOperation<IKey>
	{
		public KeyRemovalOperation(IKey key)
		{
			Object = key;
		}

		public override string DisplayName
		{
			get { return Object.Parent.Name; }
		}

		protected override void RunOperationInternal()
		{
			if (Object.Parent != null)
				Object.Parent.RemoveKey(Object);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get
			{
				var list = new List<ChangeToNote>();

				foreach (var relationship in Object.Parent.Relationships.Where(r => Object.Equals(r.PrimaryKey)))
					foreach (var reference in relationship.MappedReferences())
						list.Add(new ChangeToNote(reference, "Mapped Relationship for Reference was removed."));

				return list;
			}
		}

		public override void NotApplied()
		{
			Object.IsUserDefined = true;
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} Removed", Object.Parent.Database.Name, Object.Parent.Name, Object.Name);
		}
	}

	internal static class KeyOperationUtility
	{
		internal static void FixKeyReferences(IKey keyToCopyInto, IKey keyToCopyFrom)
		{
			// Fix Column references
			foreach (var col in keyToCopyFrom.Columns)
				keyToCopyInto.AddColumn(col.Name);

			// Fix ReferencedKey reference
			if (keyToCopyFrom.ReferencedKey != null)
			{
				ITable referencedTable = keyToCopyInto.Parent.Database.GetTable(keyToCopyFrom.ReferencedKey.Parent.Name, keyToCopyFrom.ReferencedKey.Parent.Schema);

				if (referencedTable != null)
					keyToCopyInto.ReferencedKey = referencedTable.GetKey(keyToCopyFrom.ReferencedKey.Name);
			}
		}
	}

	#endregion

	#region Index operations

	internal static class IndexOperationUtility
	{
		internal static void FixIndexReferences(IIndex indexToCopyInto, IIndex indexToCopyFrom)
		{
			// Fix Column references
			foreach (var col in indexToCopyFrom.Columns)
				indexToCopyInto.AddColumn(col.Name);
		}
	}

	public class IndexChangeOperation : MergeOperation<IIndex>, ITwoStepMergeOperation
	{
		private readonly IIndex indexToCopyFrom;
		public string Description { get; set; }

		public IndexChangeOperation(IIndex indexToChange, IIndex indexToCopyFrom, string description)
		{
			Object = indexToChange;
			this.indexToCopyFrom = indexToCopyFrom;
			this.Description = description;
		}

		public override string DisplayName
		{
			get { return Object.Parent.Name; }
		}

		protected override void RunOperationInternal()
		{
			indexToCopyFrom.CopyInto(Object);
		}

		public void RunSecondStep()
		{
			Object.Columns.Clear();
			IndexOperationUtility.FixIndexReferences(Object, indexToCopyFrom);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} Changed", Object.Parent.Database.Name, Object.Parent.Name, Object.Name);
		}
	}

	public class IndexAdditionOperation : MergeOperation<IIndex>, ITwoStepMergeOperation
	{
		public ITable Table { get; set; }

		private IIndex indexToCopyInto;

		public IndexAdditionOperation(ITable table, IIndex index)
		{
			Table = table;
			Object = index;
		}

		public override string DisplayName
		{
			get { return Table.Name; }
		}

		protected override void RunOperationInternal()
		{
			indexToCopyInto = Object.Clone();
			Table.AddIndex(indexToCopyInto);
		}

		public void RunSecondStep()
		{
			IndexOperationUtility.FixIndexReferences(indexToCopyInto, Object);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} Added", Table.Database.Name, Table.Name, Object.Name);
		}
	}

	public class IndexRemovalOperation : MergeOperation<IIndex>
	{
		public IndexRemovalOperation(IIndex index)
		{
			Object = index;
		}

		protected override void RunOperationInternal()
		{
			Object.Parent.RemoveIndex(Object);
		}

		public override string DisplayName
		{
			get { return Object.Parent.Name; }
		}

		public override void NotApplied()
		{
			Object.IsUserDefined = true;
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2} Removed", Object.Parent.Database.Name, Object.Parent.Name, Object.Name);
		}
	}

	#endregion

	#region Table operations

	public class TableAdditionOperation : MergeOperation<ITable>
	{
		public IDatabase Database { get; set; }

		public TableAdditionOperation(IDatabase db, ITable table)
		{
			Database = db;
			Object = table;
		}

		public override string DisplayName
		{
			get { return Object.Name; }
		}

		protected override void RunOperationInternal()
		{
			var newTable = Object.Clone();

			if (newTable.IsView)
				Database.AddView(newTable);
			else
				Database.AddTable(newTable);

			foreach (IColumn col in Object.Columns)
				newTable.AddColumn(col.Clone());

			foreach (IKey key in Object.Keys)
			{
				IKey newKey = newTable.AddKey(key.Clone());
				newKey.Parent = newTable;

				foreach (var column in key.Columns)
					newKey.AddColumn(column.Name);
			}

			foreach (IIndex index in Object.Indexes)
			{
				IIndex newIndex = newTable.AddIndex(index.Clone());
				newIndex.Parent = newTable;

				foreach (var column in index.Columns)
					newIndex.AddColumn(column.Name);
			}
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat("{0}.{1} Added", Database.Name, Object.Name);

			foreach (var column in Object.Columns)
				sb.AppendLine().Append(ColumnAdditionOperation.PrintNewColumnInformation(column, "\t"));

			return sb.ToString();
		}
	}

	public class TableRemovalOperation : MergeOperation<ITable>
	{
		public TableRemovalOperation(ITable table)
		{
			Object = table;
		}

		public override string DisplayName
		{
			get { return Object.Name; }
		}

		protected override void RunOperationInternal()
		{
			Object.DeleteSelf();
		}

		public override void NotApplied()
		{
			Object.IsUserDefined = true;
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get
			{
				var list = new List<ChangeToNote>();

				foreach (var entity in Object.MappedEntities())
					list.Add(new ChangeToNote(entity, "Mapped Table for Entity Removed"));

				foreach (var reference in Object.MappedReferences())
					list.Add(new ChangeToNote(reference, "Mapped Table for Reference Removed"));

				return list;
			}
		}

		public override string ToString()
		{
			return string.Format("{0}.{1} Removed", Object.Database.Name, Object.Name);
		}
	}

	public class TableChangeOperation : MergeOperation<ITable>
	{
		private readonly ITable tableToCopyFrom;

		public TableChangeOperation(ITable tableToChange, ITable tableToCopyFrom)
		{
			Object = tableToChange;
			this.tableToCopyFrom = tableToCopyFrom;
		}

		public override string DisplayName
		{
			get { return Object.Name; }
		}

		protected override void RunOperationInternal()
		{
			tableToCopyFrom.CopyInto(Object);
		}

		public override void NotApplied()
		{
			Object.IsUserDefined = true;
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("{0}.{1} Changed", Object.Database.Name, Object.Name);
		}
	}

	#endregion

	#region Relationship operations

	internal static class RelationshipHelper
	{
		public static int GetHashCode(MergeOperation<Relationship> op)
		{
			int hash = op.DisplayName.GetHashCode() ^ op.ToString().GetHashCode() ^ op.ChangesToNote.Count();

			foreach (var change in op.ChangesToNote)
				hash = hash ^ change.GetHashCode();

			return hash;
		}

		public static bool Equals(MergeOperation<Relationship> op, object obj)
		{
			MergeOperation<Relationship> other = (MergeOperation<Relationship>)obj;

			if (op.DisplayName != other.DisplayName)
				return false;

			if (op.Object != other.Object)
				return false;

			if (op.ToString() != other.ToString())
				return false;

			if (op.ChangesToNote.Count() != other.ChangesToNote.Count())
				return false;

			for (int i = 0; i < op.ChangesToNote.Count(); i++)
			{
				if (op.ChangesToNote.ElementAt(i).ObjectToInspect != other.ChangesToNote.ElementAt(i).ObjectToInspect ||
					op.ChangesToNote.ElementAt(i).TextForUser != other.ChangesToNote.ElementAt(i).TextForUser)
					return false;
			}
			return true;
		}
	}

	public class RelationshipChangeOperation : MergeOperation<Relationship>, ITwoStepMergeOperation
	{
		private readonly Relationship relationshipToCopyFrom;
		public string Description { get; set; }

		public RelationshipChangeOperation(Relationship relationshipToChange, Relationship relationshipToCopyFrom, string description)
		{
			Object = relationshipToChange;
			this.relationshipToCopyFrom = relationshipToCopyFrom;
			this.Description = description;
		}

		public override string DisplayName
		{
			get { return Object.Name; }//.Parent.Name; }
		}

		protected override void RunOperationInternal()
		{
			relationshipToCopyFrom.CopyInto(Object);
		}

		public void RunSecondStep()
		{
			//Object.ClearColumns();

			//RelationshipOperationUtility.FixKeyReferences(Object, relationshipToCopyFrom);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("Relationship changed: {0} between {1}.{2} and {1}.{3}", Object.Name, Object.Database.Name, Object.PrimaryTable.Name, Object.ForeignTable.Name);
		}

		public override int GetHashCode()
		{
			return RelationshipHelper.GetHashCode(this);
		}

		public override bool Equals(object obj)
		{
			return RelationshipHelper.Equals(this, obj);
		}
	}

	public class RelationshipAdditionOperation : MergeOperation<Relationship>, ITwoStepMergeOperation
	{
		public ITable Table { get; set; }

		private Relationship newRelationship;

		public RelationshipAdditionOperation(ITable table, Relationship relationship)
		{
			Table = table;
			Object = relationship;
		}

		public override string DisplayName
		{
			get { return Table.Name; }
		}

		protected override void RunOperationInternal()
		{
			newRelationship = Object.Clone();
			//Table.AddRelationship(newRelationship);
			newRelationship.Database = Table.Database;
		}

		public void RunSecondStep()
		{
			RelationshipOperationUtility.FixRelationshipReferences(newRelationship, Object);

			if (newRelationship.PrimaryTable == null || newRelationship.ForeignTable == null)
			{
				newRelationship.DeleteSelf();
				return;
			}
			ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor p = new MappingLayer.OneToOneEntityProcessor();
			p.CreateReference(newRelationship, newRelationship.PrimaryTable.Database.MappingSet.EntitySet);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get { return new List<ChangeToNote>(); }
		}

		public override string ToString()
		{
			return string.Format("Relationship added: {0} between {1}.{2} and {1}.{3}", Object.Name, Object.Database.Name, Object.PrimaryTable.Name, Object.ForeignTable.Name); //Table.Database.Name, Table.Name);
		}

		public override int GetHashCode()
		{
			return RelationshipHelper.GetHashCode(this);
		}

		public override bool Equals(object obj)
		{
			return RelationshipHelper.Equals(this, obj);
		}
	}

	public class RelationshipRemovalOperation : MergeOperation<Relationship>
	{
		public RelationshipRemovalOperation(Relationship relationship)
		{
			Object = relationship;
		}

		public override string DisplayName
		{
			get { return Object.Name; }
		}

		protected override void RunOperationInternal()
		{
			//Object.PrimaryTable.RemoveRelationship(Object);
			//Object.ForeignTable.RemoveRelationship(Object);
			Object.DeleteSelf();//.Parent.RemoveKey(Object);
		}

		public override IEnumerable<ChangeToNote> ChangesToNote
		{
			get
			{
				var list = new List<ChangeToNote>();

				foreach (var reference in Object.MappedReferences())
					list.Add(new ChangeToNote(reference, "Mapped Relationship for Reference was removed."));

				return list;
			}
		}

		public override void NotApplied()
		{
			Object.IsUserDefined = true;
		}

		public override string ToString()
		{
			return string.Format("Relationship removed: {0} between {1}.{2} and {1}.{3}", Object.Name, Object.Database.Name, Object.PrimaryTable.Name, Object.ForeignTable.Name);
		}

		public override int GetHashCode()
		{
			return RelationshipHelper.GetHashCode(this);
		}

		public override bool Equals(object obj)
		{
			return RelationshipHelper.Equals(this, obj);
		}
	}

	internal static class RelationshipOperationUtility
	{
		internal static void FixRelationshipReferences(Relationship relationshipToCopyInto, Relationship relationshipToCopyFrom)
		{
			ITable primaryTable = relationshipToCopyInto.Database.Tables.FirstOrDefault(t => t.Name == relationshipToCopyFrom.PrimaryTable.Name && t.Schema == relationshipToCopyFrom.PrimaryTable.Schema);
			ITable foreignTable = relationshipToCopyInto.Database.Tables.FirstOrDefault(t => t.Name == relationshipToCopyFrom.ForeignTable.Name && t.Schema == relationshipToCopyFrom.ForeignTable.Schema);

			if (primaryTable == null || foreignTable == null)
				return;

			relationshipToCopyInto.AddThisTo(primaryTable, foreignTable);
			relationshipToCopyInto.ForeignKey.Parent = foreignTable;
			relationshipToCopyInto.PrimaryKey.Parent = primaryTable;
			//relationshipToCopyInto.ForeignKey.Parent = relationshipToCopyInto.Database.Tables.Single(t => t.Name == relationshipToCopyFrom.ForeignTable.Name);
			//relationshipToCopyInto.PrimaryKey.Parent = relationshipToCopyInto.Database.Tables.Single(t => t.Name == relationshipToCopyFrom.PrimaryTable.Name);

			// Fix Column references
			foreach (var col in relationshipToCopyFrom.ForeignKey.Columns)
				relationshipToCopyInto.ForeignKey.AddColumn(col.Name);

			foreach (var col in relationshipToCopyFrom.PrimaryKey.Columns)
				relationshipToCopyInto.PrimaryKey.AddColumn(col.Name);

			//// Fix ReferencedKey reference
			//if (relationshipToCopyFrom.ReferencedKey != null)
			//{
			//    ITable referencedTable = relationshipToCopyInto.Parent.Database.GetTable(relationshipToCopyFrom.ReferencedKey.Parent.Name);
			//    relationshipToCopyInto.ReferencedKey = referencedTable.GetKey(relationshipToCopyFrom.ReferencedKey.Name);
			//}
		}
	}

	#endregion
}
