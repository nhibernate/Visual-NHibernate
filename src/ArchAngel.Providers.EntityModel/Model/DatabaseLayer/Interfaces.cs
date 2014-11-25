using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	public interface IScriptBase : IModelObject
	{
		string Description { get; set; }
		bool Enabled { get; set; }
		bool IsUserDefined { get; set; }
		string Name { get; set; }
		string Schema { get; set; }
		IDatabase Database { get; set; }
		void ResetDefaults();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface ITable : IIndexContainer, IKeyContainer, IScriptBase, IEntity, IColumnContainer
	{
		IEnumerable<IColumn> ColumnsInPrimaryKey { get; }
		IEnumerable<IColumn> ColumnsNotInPrimaryKey { get; }
		Relationship CreateRelationshipTo(ITable targetTable);
		Relationship CreateRelationshipUsing(IKey thisKey, IKey otherTableKey);

		void AddRelationship(Relationship relationship);
		void RemoveRelationship(Relationship relationship);
		new ReadOnlyCollection<Relationship> Relationships { get; }

		IEnumerable<DirectedRelationship> DirectedRelationships { get; }
		IKey FirstPrimaryKey { get; }
		IEnumerable<IKey> ForeignKeys { get; }
		IEnumerable<IKey> UniqueKeys { get; }

		void DeleteColumn(IColumn column);
		void DeleteSelf();

		ITable Clone();
		void CopyInto(ITable t);
		bool HasChanges(ITable value);
		bool IsView { get; set; }
	}

	public interface ITableMember<T>
	{
		bool HasChanges(T value, out string description);
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface Relationship : IRelationship, IScriptBase, ITableMember<Relationship>
	{
		Guid Identifier { get; set; }
		Cardinality PrimaryCardinality { get; set; }
		Cardinality ForeignCardinality { get; set; }
		IEnumerable<MappedColumn> MappedColumns { get; }
		ITable PrimaryTable { get; set; }
		ITable ForeignTable { get; set; }
		IKey PrimaryKey { get; set; }
		IKey ForeignKey { get; set; }
		void AddThisTo(ITable source, ITable target);
		new string Name { get; set; }
		void DeleteSelf();
		void SetPrimaryEnd(IKey key);
		void SetForeignEnd(IKey key);
		void CopyInto(Relationship relationship);
		Relationship Clone();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface IKey : IScriptBase, ITableMember<IKey>
	{
		ITable Parent { get; set; }
		ReadOnlyCollection<IColumn> Columns { get; }
		IKey ReferencedKey { get; set; }
		DatabaseKeyType Keytype { get; set; }
		bool IsUnique { get; set; }
		IKey AddColumn(string columnName);
		void ClearColumns();
		bool IsOneToOneRelationship();
		void RemoveColumn(IColumn column);
		IKey Clone();
		void CopyInto(IKey key);
		void DeleteSelf();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface IColumn : IScriptBase, ITableMember<IColumn>
	{
		bool IsComputed { get; set; }
		ITable Parent { get; set; }
		//ArchAngel.Providers.EntityModel.Helper.SQLServer.UniDbTypes Datatype { get; set; }
		//ArchAngel.Interfaces.ProjectOptions.TypeMappings.UniType UniDatatype { get; set; }
		string OriginalDataType { get; set; }
		string Default { get; set; }
		bool InPrimaryKey { get; set; }
		bool IsCalculated { get; set; }
		bool IsIdentity { get; set; }
		bool IsNullable { get; set; }
		bool IsUnique { get; set; }
		int OrdinalPosition { get; set; }
		int Precision { get; set; }
		bool IsReadOnly { get; set; }
		int Scale { get; set; }
		long Size { get; set; }
		bool SizeIsMax { get; set; }
		bool IsPseudoBoolean { get; set; }
		string PseudoTrue { get; set; }
		string PseudoFalse { get; set; }

		IColumn Clone();
		void CopyInto(IColumn column);
		void DeleteSelf();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public interface IIndex : IScriptBase, ITableMember<IIndex>
	{
		ITable Parent { get; set; }
		List<IColumn> Columns { get; }
		bool IsClustered { get; set; }
		bool IsUnique { get; set; }
		DatabaseIndexType IndexType { get; set; }
		IColumn AddColumn(string columnName);
		void RemoveColumn(IColumn column);
		void CopyInto(IIndex i);
		IIndex Clone();
		void DeleteSelf();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface IDatabase : ITableContainer, IViewContainer
	{
		string Name { get; }
		IDatabaseLoader Loader { get; set; }
		IEnumerable<Relationship> Relationships { get; }
		MappingSet MappingSet { get; set; }
		bool IsEmpty { get; }
		IEnumerable<ITable> GetRelatedEntities(ITable table, int i);
		IKey GetKey(string name, string schema, string keyName);
		string Serialise(IDatabaseSerialisationScheme scheme);
		NullEntityObject GetNullEntityObject();
		DatabaseTypes DatabaseType { get; set; }
		ArchAngel.Providers.EntityModel.Helper.ConnectionStringHelper ConnectionInformation { get; set; }
		IEnumerable<string> GetSchemas();

		void DeleteView(ITable table);
		void DeleteTable(ITable table);
		void DeleteRelationship(Relationship relationship);

		/// <summary>  Serialises the database to XML using the latest version of the serialisation scheme. </summary>
		string Serialise();

		bool Contains(IEntity entity);
		void AddEntity(IEntity entity);
		void RemoveEntity(IEntity entity);
		void AddRelationship(Relationship relationship);
		void RemoveRelationship(Relationship relationship);
		void RemoveRelationshipsContaining(IKey key);
		IEnumerable<IKey> GetAllKeys();

		event EventHandler<CollectionChangeEvent<Relationship>> RelationshipsChanged;
	}

	public interface IColumnContainer : IItemContainer
	{
		ReadOnlyCollection<IColumn> Columns { get; }
		void AddColumn(IColumn column);
		void RemoveColumn(IColumn column);
		IColumn GetColumn(string columnName);
		IColumn GetColumn(string columnName, StringComparison comparison);

		event EventHandler<CollectionChangeEvent<IColumn>> ColumnsChanged;
		event CollectionChangeHandler<Relationship> RelationshipsChanged;
	}

	public interface IKeyContainer : IItemContainer
	{
		ReadOnlyCollection<IKey> Keys { get; }
		IKey AddKey(IKey key);
		void RemoveKey(IKey key);
		IKey GetKey(string keyName);
	}

	public interface IIndexContainer : IItemContainer
	{
		ReadOnlyCollection<IIndex> Indexes { get; }
		IIndex AddIndex(IIndex index);
		void RemoveIndex(IIndex index);
		IIndex GetIndex(string indexName);
	}

	public interface ITableContainer : IItemContainer
	{
		ReadOnlyCollection<ITable> Tables { get; }
		ITable AddTable(ITable table);
		void RemoveTable(ITable table);
		ITable GetTable(string tableName, string schema);

		event EventHandler<CollectionChangeEvent<ITable>> TablesChanged;
	}

	public interface IViewContainer : IItemContainer
	{
		ReadOnlyCollection<ITable> Views { get; }
		ITable AddView(ITable view);
		void RemoveView(ITable view);
		ITable GetView(string viewName, string schema);

		event EventHandler<CollectionChangeEvent<ITable>> ViewsChanged;
	}
}
