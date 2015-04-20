using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public interface IMainPanel
	{
		//void ShowPropertyGrid(IEditorForm item);
		void ShowObjectPropertyGrid(IModelObject obj);
		void ShowDatabaseRefreshResults(DatabaseMergeResult results, IDatabase db1, IDatabase db2);

		void ShowDatabaseRefreshResultsForm(ISimpleDBMergeResultForm form);
		void CloseDatabaseRefreshResultsForm(Changes status);

		void ShowCreateOneToOneMappingForm(ICreateOneToOneMappingsForm form);
		void CloseCreateOneToOneMappingForm(Changes status);

		void SyncCurrentlySelectedObject(IModelObject obj);

		DialogResult ShowDialog(Form form);
		//void DisableDiagramRefresh();
		//void EnableDiagramRefresh();
		//void ShowOnDiagram(IModelObject modelObject);
	}

	public enum Changes
	{
		WereAccepted,
		WereRejected
	}

	public interface IEditorForm
	{
		void Clear();
		void StartBulkUpdate();
		void EndBulkUpdate();
	}

	public interface ICreateOneToOneMappingsForm
	{
		event EventHandler ChangesAccepted;
		event EventHandler Cancelled;

		IEnumerable<ITable> SelectedTables { get; }
		void SetAlreadyMappedTables(IEnumerable<ITable> mappedTables);
		void SetAllTables(IEnumerable<ITable> tables);
	}

	public interface IScriptBaseForm
	{
		void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties);
		void RefreshVirtualProperties();
	}

	public interface IMappingForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler ToEntityChanged;
		event EventHandler FromTableChanged;
		event EventHandler MappingsChanged;
		event EventHandler RemoveMapping;
		Entity ToEntity { get; set; }
		ITable FromTable { get; set; }
		IEnumerable<ColumnPropertyMapping> Mappings { get; set; }
		IEnumerable<Entity> Entities { get; set; }
		IEnumerable<ITable> Tables { get; set; }
	}

	public interface IReferenceForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler Entity1Changed;
		event EventHandler Entity2Changed;
		event EventHandler End1NameChanged;
		event EventHandler End2NameChanged;
		event EventHandler End1EnabledChanged;
		event EventHandler End2EnabledChanged;
		event EventHandler End1CardinalityChanged;
		event EventHandler End2CardinalityChanged;
		event EventHandler MappedTableChanged;
		event EventHandler MappedRelationshipChanged;
		event EventHandler DeleteRelationship;
		Entity Entity1 { get; set; }
		Entity Entity2 { get; set; }
		bool End1Enabled { get; set; }
		bool End2Enabled { get; set; }
		Cardinality End1Cardinality { get; set; }
		Cardinality End2Cardinality { get; set; }
		string End1Name { get; set; }
		string End2Name { get; set; }
		IEnumerable<Entity> EntityList { get; set; }
		IEnumerable<ITable> MappedTableList { get; set; }
		IEnumerable<Relationship> MappedRelationshipList { get; set; }
		ITable MappedTable { get; set; }
		Relationship MappedRelationship { get; set; }
		void MappedTableSelectionEnabled(bool enabled);
		void MappedRelationshipSelectionEnabled(bool enabled);
	}

	public interface IPropertyForm : IEditorForm, IScriptBaseForm
	{
		bool ShouldShowReadOnly { get; set; }
		bool ShouldShowIsKeyProperty { get; set; }
		bool ShouldShowNullable { get; set; }

		string Datatype { get; set; }
		string PropertyName { get; set; }
		bool ReadOnly { get; set; }
		bool IsKeyProperty { get; set; }
		bool IsOveridden { get; set; }

		void SetValidationOptions(ValidationOptions options);

		event EventHandler DatatypeChanged;
		event EventHandler PropertyNameChanged;
		event EventHandler ReadOnlyChanged;
		event EventHandler IsKeyChanged;

		event EventHandler RemoveProperty;
	}

	public interface IEntityKeyForm : IEditorForm, IEventSender
	{
		EntityKeyType KeyType { get; set; }
		void SetProperties(IEnumerable<Property> enumerable);
		Component Component { get; set; }

		void SetParentEntityName(string name);
		void SetPossibleProperties(IEnumerable<Property> properties);
		void SetPossibleComponents(IEnumerable<Component> components);

		void SetVirtualProperties(IEnumerable<IUserOption> options);

		event EventHandler<GenericEventArgs<Property>> AddNewProperty;
		event EventHandler<GenericEventArgs<Property>> RemoveProperty;
		event EventHandler ComponentChanged;
		event EventHandler KeyTypeChanged;
		event EventHandler RunKeyConversionWizard;
	}

	public interface IEntityForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler<GenericEventArgs<Property>> SingleMappingChanged;
		event EventHandler NameChanged;
		event EventHandler AddNewProperty;
		event EventHandler RemoveEntity;
		event EventHandler<PropertyNameChangeEventArgs> PropertyNameChanged;
		event EventHandler<GenericEventArgs<Property>> RemoveProperty;
		event EventHandler<GenericEventArgs<Property>> EditProperty;
		event EventHandler MappingsChanged;
		event EventHandler CreateNewTableFromEntity;
		event EventHandler<MappingEventArgs> MappingRemoved;
		event EventHandler<MappingEventArgs> NewMappingAdded;
		event EventHandler<GenericEventArgs<Entity>> ChildEntityAdded;
		event EventHandler<GenericEventArgs<Entity>> ChildEntityRemoved;
		event EventHandler DiscriminatorChanged;
		event EventHandler ParentEntityChanged;
		event EventHandler<GenericEventArgs<Property>> CopyProperty;

		string EntityName { get; set; }
		//Discriminator Discriminator { get; set; }
		Entity ParentEntity { get; set; }
		IEnumerable<Mapping> Mappings { get; set; }

		void SetAvailableTables(IEnumerable<ITable> newTableList);
		void SetAvailableEntities(IEnumerable<Entity> newEntities);
		void SetProperties(IEnumerable<Property> newPropList);
		void SetChildEntities(IEnumerable<Entity> entities);
		void SetSelectedPropertyName(Property property);

		IColumn GetMappedColumnFor(Property property);
	}

	public class PropertyNameChangeEventArgs : EventArgs
	{
		public readonly Property ChangedProperty;
		public readonly string NewName;

		public PropertyNameChangeEventArgs(Property changedProperty, string newName)
		{
			ChangedProperty = changedProperty;
			NewName = newName;
		}
	}

	public interface IDatabaseForm : IEditorForm
	{
		//event EventHandler TestConnection;
		//event EventHandler RefreshSchema;

		event EventHandler UsernameChanged;
		event EventHandler PasswordChanged;
		event EventHandler SelectedDatabaseChanged;
		event EventHandler SelectedDatabaseTypeChanged;
		event EventHandler ServerNameChanged;

		void SetDatabaseOperationResults(DatabaseOperationResults results);

		void SetServersNames(IEnumerable<string> names);
		DatabaseTypes SelectedDatabaseType { get; set; }
		IEnumerable<string> DatabaseNames { get; set; }
		/// <summary>
		/// Is either a database or a database file, depending on which
		/// option is selected in the UI.
		/// </summary>
		string SelectedDatabase { get; }
		ConnectionStringHelper ConnectionStringHelper { get; }
		string SelectedServerName { get; set; }
		bool UsingDatabaseFile { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		bool UseIntegratedSecurity { get; set; }
		int Port { get; set; }
		string ServiceName { get; set; }
		IServerAndDatabaseHelper DatabaseHelper { get; set; }
		void SetDatabaseFilename(string newDatabaseFilename);
		void SetDatabase(string database);
		void SelectDatabase();
		void SelectDatabaseFile();
	}

	public class MappingEventArgs : EventArgs
	{
		public Mapping Mapping { get; private set; }

		public MappingEventArgs(Mapping mapping)
		{
			Mapping = mapping;
		}
	}

	public interface ITableForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler DescriptionChanged;
		event EventHandler EntityNameChanged;
		event EventHandler AddNewColumn;
		event EventHandler AddNewKey;
		event EventHandler DeleteEntity;
		event EventHandler<GenericEventArgs<IColumn>> EditColumn;
		event EventHandler<GenericEventArgs<IColumn>> DeleteColumn;
		event EventHandler<GenericEventArgs<IKey>> EditKey;
		event EventHandler<GenericEventArgs<IKey>> DeleteKey;

		string Title { get; set; }
		string EntityName { get; set; }
		string Description { get; set; }
		void SetColumns(IEnumerable<IColumn> columns);
		void SetSelectedColumnName(IColumn column);
		void SetKeys(IEnumerable<IKey> keys);
	}

	public interface IKeyForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler KeytypeChanged;
		event EventHandler DescriptionChanged;
		event EventHandler KeyNameChanged;
		event EventHandler<GenericEventArgs<IColumn>> AddNewColumn;
		event EventHandler EditColumn;
		event EventHandler<GenericEventArgs<IColumn>> RemoveColumn;
		event EventHandler DeleteKey;
		string KeyName { get; set; }
		DatabaseKeyType Keytype { get; set; }
		string Description { get; set; }
		IEnumerable<IColumn> Columns { get; set; }
		IColumn SelectedColumn { get; set; }
		void SetAvailableColumns(IEnumerable<IColumn> columnForKey);
	}

	public class ColumnEventArgs : EventArgs
	{
		public string ColumnName { get; set; }

		public ColumnEventArgs(string columnName)
		{
			ColumnName = columnName;
		}
	}

	public interface IRelationshipForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler RelationshipNameChanged;
		event EventHandler DeleteRelationship;
		event EventHandler PrimaryKeyChanged;
		event EventHandler ForeignKeyChanged;

		string RelationshipName { get; set; }
		IKey PrimaryKey { get; set; }
		IKey ForeignKey { get; set; }
		void SetPossiblePrimaryKeys(IEnumerable<IKey> keys);
		void SetPossibleForeignKeys(IEnumerable<IKey> keys);
	}

	public interface IIndexForm : IEditorForm, IScriptBaseForm
	{
		event EventHandler IndexNameChanged;
		event EventHandler DescriptionChanged;
		event EventHandler DatatypeChanged;
		event EventHandler SelectedColumnChanged;
		event EventHandler DeleteColumn;
		string IndexName { get; set; }
		string Description { get; set; }
		DatabaseIndexType Datatype { get; set; }
		List<IColumn> Columns { get; set; }
		IColumn SelectedColumn { get; }
	}

	public interface IColumnForm : IEditorForm, IScriptBaseForm
	{
		string ColumnName { get; set; }
		string Description { get; set; }
		bool IsNullable { get; set; }
		string Datatype { get; set; }
		string Default { get; set; }
		int OrdinalPosition { get; set; }
		int ColumnSize { get; set; }
		bool ColumnSizeIsMax { get; set; }
		int Precision { get; set; }
		int ColumnScale { get; set; }
		event EventHandler ColumnNameChanged;
		event EventHandler ColumnScaleChanged;
		event EventHandler ColumnSizeChanged;
		event EventHandler ColumnSizeIsMaxChanged;
		event EventHandler DatatypeChanged;
		event EventHandler DefaultChanged;
		event EventHandler DescriptionChanged;
		event EventHandler IsNullableChanged;
		event EventHandler OrdinalPositionChanged;
		event EventHandler PrecisionChanged;
		event EventHandler DeleteColumn;
	}

	public interface IComponentForm : IEditorForm, IScriptBaseForm
	{
		string ComponentName { get; set; }

		void SetParentEntity(string name);
		void SetMappings(IEnumerable<ColumnComponentPropertyMapping> mappings);
		/// <summary>
		/// Sets the Columns that the properties can be mapped to.
		/// </summary>
		/// <param name="columns"></param>
		void SetPossibleColumns(IEnumerable<IColumn> columns);

		void SetProperties(IEnumerable<ComponentPropertyMarker> properties);

		IColumn GetMappedColumnFor(ComponentPropertyMarker property);

		event EventHandler<GenericEventArgs<ComponentPropertyMarker>> PropertyMappingChanged;
		event EventHandler ComponentNameChanged;
		event EventHandler DeleteComponent;
	}

	public interface IComponentSpecificationForm : IEditorForm, IScriptBaseForm
	{
		string SpecName { get; set; }

		string GetPropertyName(ComponentProperty property);
		void SetProperties(IEnumerable<ComponentProperty> properties);
		void SetUsages(IEnumerable<Entity> entities);
		void SetFullEntityList(IEnumerable<Entity> entities);

		event EventHandler SpecNameChanged;
		event EventHandler<GenericEventArgs<ComponentProperty>> PropertyNameChanged;
		event EventHandler<GenericEventArgs<ComponentProperty>> EditProperty;
		event EventHandler<GenericEventArgs<ComponentProperty>> DeleteProperty;
		event EventHandler CreateNewProperty;
		event EventHandler DeleteSpec;
		event EventHandler<GenericEventArgs<Entity>> NavigateToUsage;
		event EventHandler<GenericEventArgs<Entity>> AddNewUsage;
	}
}