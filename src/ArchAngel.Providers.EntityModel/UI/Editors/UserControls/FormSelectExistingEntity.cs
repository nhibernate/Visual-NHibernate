using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormSelectExistingEntity : Form
	{
		private class EntityRelationshipContainer
		{
			public Entity Entity;
			public Relationship Relationship;

			public EntityRelationshipContainer(Entity entity, Relationship relationship)
			{
				Entity = entity;
				Relationship = relationship;
			}

		}
		public enum RequestorTypes
		{
			Entity_Select_Parent,
			Entity_Create_Abstract_Parent,
			Entity_Select_Child,
			Entity_Select_Existing,
			Table,
			Component
		}
		private RequestorTypes RequestorType = RequestorTypes.Entity_Select_Existing;
		private Entity Entity;
		private Table Table;
		private ComponentSpecification ComponentSpecification;
		private List<Entity> UnavailableEntities;
		public Entity SelectedEntity = null;
		public List<Entity> SelectedEntities = null;
		public Cardinality CardinalityPrimary = null;
		public Cardinality CardinalityForeign = null;
		public ITable AssociationTable = null;
		public Relationship SelectedRelationship = null;
		private bool MultiSelect = false;
		private Slyce.Common.TextBoxFocusHelper FocusHelper;

		public FormSelectExistingEntity(Entity entity, List<Entity> unavailableEntities, Entity selectedEntity, string title, bool multiSelect, RequestorTypes requestorType)
		{
			InitializeComponent();

			Clear();

			MultiSelect = multiSelect;
			RequestorType = requestorType;
			Entity = entity;
			UnavailableEntities = unavailableEntities;
			Text = title;
			SelectedEntity = selectedEntity;
			listViewEx1.CheckBoxes = MultiSelect;
			FocusHelper = new Slyce.Common.TextBoxFocusHelper(new TextBox[] { textBoxName });

			if (requestorType == RequestorTypes.Entity_Create_Abstract_Parent)
				MultiSelect = true;

			Populate();
			PopulateItemsForEntity();
		}

		public FormSelectExistingEntity(Table table, List<Entity> unavailableEntities, Entity selectedEntity, string title, bool multiSelect)
		{
			InitializeComponent();

			Clear();
			MultiSelect = multiSelect;
			RequestorType = RequestorTypes.Table;
			Table = table;
			UnavailableEntities = unavailableEntities;
			Text = title;
			SelectedEntity = selectedEntity;
			FocusHelper = new Slyce.Common.TextBoxFocusHelper(new TextBox[] { textBoxName });
			Populate();
			PopulateItemsForEntity();
		}

		private void Clear()
		{
			CardinalityPrimary = null;
			CardinalityForeign = null;
			AssociationTable = null;
			SelectedRelationship = null;
		}

		public FormSelectExistingEntity(ComponentSpecification componentSpecification, List<Entity> unavailableEntities, Entity selectedEntity, string title, bool multiSelect)
		{
			InitializeComponent();

			MultiSelect = multiSelect;
			RequestorType = RequestorTypes.Component;
			ComponentSpecification = componentSpecification;
			UnavailableEntities = unavailableEntities;
			Text = title;
			SelectedEntity = selectedEntity;
			Populate();
		}

		private void Populate()
		{
			if (Table != null)
			{
				checkBoxCreateNewEntity.Text = "  Create new entity:";
				textBoxName.Left = checkBoxCreateNewEntity.Right + 5;
				textBoxName.Width = listViewEx1.Right - textBoxName.Left;
				checkBoxCreateNewEntity.Visible = true;
				checkBoxCreateNewEntity.Checked = true;
				checkBoxSelectExistingEntity.Visible = false;
				textBoxName.SelectAll();
				textBoxName.Focus();
				labelSelectText.Text = "Select properties:";
				//labelSelectText.Top = 15;
				listViewEx1.Height = buttonOk.Top - labelSelectText.Bottom - 30;
				listViewEx1.Top = labelSelectText.Bottom + 20;
			}
			else
			{
				// Entity
				checkBoxCreateNewEntity.Visible = false;
				textBoxName.Visible = false;
				textBoxName.Width = listViewEx1.Right - textBoxName.Left;
				checkBoxCreateNewEntity.Visible = false;
				checkBoxSelectExistingEntity.Visible = false;
				labelSelectText.Text = "Select entity (only entities mapped to related tables are displayed):";
				labelSelectText.Top = 15;
				listViewEx1.Height = buttonOk.Top - labelSelectText.Bottom - 30;
				listViewEx1.Top = labelSelectText.Bottom + 20;
			}

			listViewEx1.Items.Clear();
			listViewEx1.MultiSelect = MultiSelect;

			if (SelectedEntity != null)
			{
				foreach (ListViewItem item in listViewEx1.Items)
				{
					if (item.Tag == SelectedEntity)
						item.Selected = true;
					else
						item.Selected = false;
				}
			}
			buttonOk.Enabled = checkBoxCreateNewEntity.Checked || SelectedItems.Count > 0;
			listViewEx1.Focus();
		}

		private void PopulateItemsForEntity()
		{
			listViewEx1.Items.Clear();

			if (Table != null)
			{
				listViewEx1.CheckBoxes = true;

				foreach (Column column in Table.Columns)
					listViewEx1.Items.Add(new ListViewItem(column.Name) { Tag = column, Checked = true });
			}
			else
			{
				// Entity
				HashSet<Entity> possibleEntities = new HashSet<Entity>();
				List<Relationship> mappedRelationships = new List<Relationship>();
				List<ITable> mappedTables = new List<ITable>();
				Dictionary<Entity, List<Relationship>> possibleRelationships = new Dictionary<Entity, List<Relationship>>();
				Dictionary<Entity, List<ITable>> possibleAssociationTables = new Dictionary<Entity, List<ITable>>();

				foreach (var dr in Entity.DirectedReferences)
				{
					Relationship r = dr.Reference.MappedRelationship();

					if (r != null)
					{
						mappedRelationships.Add(r);
						break;
					}
					ITable t = dr.Reference.MappedTable();

					if (t != null)
					{
						mappedTables.Add(t);
						break;
					}
				}

				foreach (var table in Entity.MappedTables())
				{
					foreach (var directedRelationship in table.DirectedRelationships.Where(dr => !mappedRelationships.Contains(dr.Relationship)))
						if (!mappedRelationships.Contains(directedRelationship.Relationship))
							foreach (var referencedEntity in directedRelationship.ToTable.MappedEntities())
							{
								//if (referencedEntity != Entity)
								//{
								if (!possibleRelationships.ContainsKey(referencedEntity))
									possibleRelationships.Add(referencedEntity, new List<Relationship>());

								if (!possibleRelationships[referencedEntity].Contains(directedRelationship.Relationship))
									possibleRelationships[referencedEntity].Add(directedRelationship.Relationship);
								//}
							}

					foreach (var referencedEntity in table.MappedEntities())
						if (referencedEntity != Entity && !UnavailableEntities.Contains(referencedEntity))
							possibleEntities.Add(referencedEntity);

					//foreach (var relatedTable in table.DirectedRelationships.Select(d => d.ToKey.Parent))
					//    foreach (var referencedEntity in ((Table)relatedTable).MappedEntities())
					//        if (referencedEntity != Entity && !UnavailableEntities.Contains(referencedEntity))
					//            possibleEntities.Add(referencedEntity);

					// Get possible m:n tables
					// Get directed references with 1 at the this end
					foreach (var relatedTable in table.DirectedRelationships.Where(d => d.FromKey != null && d.FromKey.IsUnique && d.ToKey != null && d.ToKey.Parent != null).Select(d => d.ToKey.Parent))
							foreach (var t in relatedTable.DirectedRelationships.Where(d => d.ToKey != null && d.ToKey.IsUnique && d.ToKey.Parent !=  null && d.ToKey.Parent != table).Select(d => d.ToKey.Parent))
								foreach (var entity in t.MappedEntities())
									possibleEntities.Add(entity);
				}
				foreach (Entity entity in possibleEntities)
					listViewEx1.Items.Add(new ListViewItem(entity.Name) { Tag = entity });

				foreach (var item in possibleRelationships)
					foreach (var rel in item.Value)
						listViewEx1.Items.Add(new ListViewItem(string.Format("{0} ({1})", item.Key.Name, rel.Name)) { Tag = new EntityRelationshipContainer(item.Key, rel) });
			}
		}

		private List<ListViewItem> SelectedItems
		{
			get
			{
				List<ListViewItem> selectedItems = new List<ListViewItem>();

				if (MultiSelect)
					foreach (ListViewItem item in listViewEx1.CheckedItems)
						selectedItems.Add(item);
				else
					foreach (ListViewItem item in listViewEx1.SelectedItems)
						selectedItems.Add(item);

				return selectedItems;
			}
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			SelectedEntities = new List<Entity>();

			if (checkBoxCreateNewEntity.Checked || RequestorType == RequestorTypes.Entity_Create_Abstract_Parent)
			{
				if (!CreateNewEntity())
					return;
			}
			else
			{
				if (SelectedItems.Count > 0)
				{
					List<string> invalidSelections = new List<string>();

					foreach (ListViewItem item in SelectedItems)
					{
						Entity entity = null;

						if (item.Tag is Entity)
							entity = (Entity)item.Tag;
						else if (item.Tag is EntityRelationshipContainer)
						{
							EntityRelationshipContainer keyValue = (EntityRelationshipContainer)item.Tag;
							entity = keyValue.Entity;
							SelectedRelationship = keyValue.Relationship;

							CardinalityPrimary = SelectedRelationship.PrimaryCardinality;
							CardinalityForeign = SelectedRelationship.ForeignCardinality;

						}
						else
							throw new NotImplementedException("Not handled yet: " + item.Tag.GetType().Name);

						if (RequestorType == RequestorTypes.Entity_Select_Existing ||
							RequestorType == RequestorTypes.Entity_Select_Child ||
							RequestorType == RequestorTypes.Entity_Select_Parent)
						{
							if (IsSelectionValidForEntityRequestor(entity))
								SelectedEntities.Add(entity);
							else
								invalidSelections.Add(entity.Name);
						}
						else
							SelectedEntities.Add(entity);
					}
					if (invalidSelections.Count > 0)
					{
						StringBuilder sb = new StringBuilder();

						foreach (string invalidSelection in invalidSelections)
							sb.Append(invalidSelection + ", ");

						MessageBox.Show(this, string.Format("No relationships or association tables exist between {0} and:{2}{1}.{2}You need to add a relationship (or association table) between the tables in the database, or add a virtual relationship between these tables in the table-diagrammer.",
							Entity.Name,
							sb.ToString().Trim(',', ' '),
							Environment.NewLine), "No valid relationships", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
					return;

				if (SelectedEntity == null && !MultiSelect && SelectedEntities.Count > 0)
					SelectedEntity = SelectedEntities[0];

				if (RequestorType == RequestorTypes.Entity_Select_Parent &&
					SelectedEntity.MappedTables().Count() == 0)
				{
					// This is a Table Per Concrete Class inheritance, so remove the matching properties from the child.
					foreach (Property property in SelectedEntity.Properties)
					{
						Property childProperty = Entity.Properties.SingleOrDefault(p => p.Name == property.Name && p.Type == property.Type);

						if (childProperty == null)
							throw new Exception("This is meant to be a TPCC inheritance, but no matching property was found in the child.");

						Entity.RemoveProperty(childProperty);
					}
				}
			}
			if (!MultiSelect)
				if (SelectedEntities.Count > 0)
					SelectedEntity = SelectedEntities[0];
			//else
			//	SelectedEntity = null;

			Close();
		}

		private bool IsSelectionValidForEntityRequestor(Entity selectedEntity)
		{
			List<ITable> selectedEntitiesTables = selectedEntity.MappedTables().ToList();
			List<Relationship> validRelationships = new List<Relationship>();
			List<ITable> validAssociationTables = new List<ITable>();

			if (selectedEntitiesTables.Count == 1 &&
				Entity.MappedTables().Count() == 1 &&
				selectedEntitiesTables[0] == Entity.MappedTables().ElementAt(0))
			{
				// Table Per Hierarchy inheritance
				return true;
			}
			if (RequestorType == RequestorTypes.Entity_Select_Parent &&
				selectedEntitiesTables.Count == 0)
			{
				bool isOk = true;

				foreach (Property property in selectedEntity.Properties)
				{
					if (Entity.Properties.Count(p => p.Name == property.Name && p.Type == property.Type) == 0)
					{
						isOk = false;
						break;
					}
				}
				// This is OK for Table Per Concrete Class inheritance - the Base is totally virtual and doesn't have
				// a mapped table in the database, and all of it's properties exist in the child class.
				if (isOk)
					return true;
			}
			else if (RequestorType == RequestorTypes.Entity_Select_Child && selectedEntitiesTables.Count > 0)
			{
				bool isOk = true;

				foreach (Property property in Entity.Properties)
				{
					if (selectedEntity.Properties.Count(p => p.Name == property.Name && p.Type == property.Type) == 0)
					{
						isOk = false;
						break;
					}
				}
				// This is OK for Table Per Concrete Class inheritance - the Base is totally virtual and doesn't have
				// a mapped table in the database, and all of it's properties exist in the child class.
				if (isOk)
					return true;
			}
			if (SelectedRelationship != null)
				return true;

			// Check that a relationship or association table exists between the entity and the new selected entity
			foreach (ITable table in Entity.MappedTables())
			{
				foreach (Relationship relationship in table.Relationships)
				{
					if (selectedEntitiesTables.Contains(relationship.PrimaryTable) ||
						selectedEntitiesTables.Contains(relationship.ForeignTable))
					{
						validRelationships.Add(relationship);
						SelectedRelationship = relationship;

						if (table == relationship.PrimaryTable)
						{
							CardinalityPrimary = relationship.PrimaryCardinality;
							CardinalityForeign = relationship.ForeignCardinality;
						}
						else
						{
							CardinalityPrimary = relationship.ForeignCardinality;
							CardinalityForeign = relationship.PrimaryCardinality;
						}
						break;
					}
				}
				if (SelectedRelationship != null)
					break;
			}
			IKey primaryKey = null;
			IKey foreignKey = null;
			ITable associationTable = null;

			if (validRelationships.Count == 0)
				associationTable = Entity.GetAssociationTable(selectedEntity, out CardinalityPrimary, out CardinalityForeign, out primaryKey, out foreignKey);

			if (validRelationships.Count == 0 &&
				associationTable == null)
			{
				//MessageBox.Show(this, string.Format("No relationships or association tables exist between {0} and {1}.{2}You need to add a relationship (or association table) between the tables in the database, or add a virtual relationship between these tables in the table-diagrammer.", 
				//    Entity.Name, 
				//    selectedEntity.Name, 
				//    Environment.NewLine), "No valid relationships", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//Close();
				//return;
				return false;
			}
			if (associationTable != null)
				AssociationTable = associationTable;

			return true;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			SelectedEntities = new List<Entity>();
			Close();
		}

		private void FormSelectEntity_Load(object sender, EventArgs e)
		{

		}

		private bool CreateNewEntity()
		{
			if (textBoxName.Text == "NewName")
			{
				MessageBox.Show(this, "You need specify a name for the new entity.", "Invalid name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (Table.Database.MappingSet.EntitySet.Entities.Any(e => e.Name.ToLowerInvariant() == textBoxName.Text.ToLowerInvariant()))
			{
				MessageBox.Show(this, "An entity with this name already exists.", "Invalid name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (RequestorType == RequestorTypes.Table)
			{
				if (listViewEx1.CheckedItems.Count == 0)
				{
					MessageBox.Show(this, "You need to select at least one property.", "No properties selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				// Check if this table is currently used as an association table
				var reference = Table.Database.MappingSet.EntitySet.References.FirstOrDefault(r => r.MappedTable() == Table);
				bool keepAssociationEntities = false;

				if (reference != null)
				{
					if (MessageBox.Show(this, string.Format("This table is currently being used as an association table to implement a many-to-many relationship between {0} and {1} and therefore should normally have no mapped entity.\n\nAre you sure you want to create a new entity for it?", reference.Entity1.Name, reference.Entity2.Name), "Association table", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
						return false;
					else
						keepAssociationEntities = true;
				}

				List<ITable> tablesToProcess = new List<ITable>();
				tablesToProcess.Add(Table);
				EntityModel.Controller.MappingLayer.MappingProcessor proc = new EntityModel.Controller.MappingLayer.MappingProcessor(new EntityModel.Controller.MappingLayer.OneToOneEntityProcessor(Table.Database.MappingSet.EntitySet.Entities.Select(ent => ent.Name)));
				List<Entity> newEntities = proc.CreateOneToOneMappingsFor(tablesToProcess, Table.Database.MappingSet, keepAssociationEntities);
				SelectedEntity = newEntities[0];

				#region Remove un-selected columns
				List<string> selectedColumnNames = new List<string>();

				foreach (ListViewItem col in listViewEx1.CheckedItems)
					selectedColumnNames.Add(col.Text);

				for (int i = SelectedEntity.Properties.Count() - 1; i >= 0; i--)
				{
					Property property = SelectedEntity.Properties.ElementAt(i);
					var mappedColumn = property.MappedColumn();

					if (mappedColumn != null && !selectedColumnNames.Contains(mappedColumn.Name))
						SelectedEntity.RemoveProperty(property);
				}
				#endregion

				SelectedEntity.Name = textBoxName.Text;
				return true;
			}
			else
			//else if (RequestorType == RequestorTypes.Entity_Select_Parent ||
			//    RequestorType == RequestorTypes.Entity_Create_Abstract_Parent)
			{
				Entity newEntity = new EntityImpl(textBoxName.Text) { Schema = Entity.Schema };
				Entity.EntitySet.AddEntity(newEntity);
				SelectedEntity = newEntity;
				return true;
			}
			//else if (RequestorType == RequestorTypes.Entity_Select_Child)
			//{
			//    if (SelectedItems.Count == 0)
			//    {
			//        MessageBox.Show(this, "You need to select some child entities.", "No entities selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//        return false;
			//    }
			//    Entity newEntity = new EntityImpl("NewChildEntity") { Schema = Entity.Schema };

			//    foreach (ListViewItem item in SelectedItems)
			//    {
			//        Property existingProperty = (Property)item.Tag;
			//        newEntity.AddProperty(new PropertyImpl(existingProperty.Name)
			//        {
			//            Type = existingProperty.Type
			//        });
			//    }
			//    //newEntity.AddChild(Entity);
			//    //Entity.Parent = newEntity;
			//    Entity.EntitySet.AddEntity(newEntity);
			//    SelectedEntity = newEntity;
			//    return true;
			//}
			//else
			//    throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
		}

		private void checkBoxCreateNewEntity_CheckedChanged(object sender, EventArgs e)
		{
			// Only proceed if it is checked.
			if (checkBoxCreateNewEntity.Checked == false)
				return;

			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Existing:
					labelSelectText.Text = "Select entity:";
					PopulateItemsForEntity();
					listViewEx1.Enabled = checkBoxSelectExistingEntity.Checked;
					break;
				case RequestorTypes.Entity_Select_Child:
					labelSelectText.Visible = false;
					PopulateItemsForEntity();
					listViewEx1.Enabled = true;
					break;
				case RequestorTypes.Entity_Select_Parent:
					labelSelectText.Text = "Select properties to promote to parent:";
					MultiSelect = true;
					PopulateItemsForEntity();
					listViewEx1.Enabled = true;
					listViewEx1.CheckBoxes = true;
					break;
				case RequestorTypes.Table:
					labelSelectText.Text = "Select columns to create properties:";
					//MultiSelect = true;
					PopulateItemsForEntity();
					//listViewEx1.Enabled = true;
					//listViewEx1.CheckBoxes = true;
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
			buttonOk.Enabled = checkBoxCreateNewEntity.Checked || SelectedItems.Count > 0;
			textBoxName.Focus();
		}

		private void checkBoxSelectExistingEntity_CheckedChanged(object sender, EventArgs e)
		{
			// Only proceed if it is checked.
			if (checkBoxSelectExistingEntity.Checked == false)
				return;

			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Existing:
					labelSelectText.Text = "Select entity:";
					listViewEx1.Enabled = checkBoxSelectExistingEntity.Checked;
					break;
				case RequestorTypes.Entity_Select_Child:
					labelSelectText.Visible = true;
					labelSelectText.Text = "Select child entities:";
					listViewEx1.Enabled = true;
					listViewEx1.CheckBoxes = true;
					break;
				case RequestorTypes.Entity_Select_Parent:
					labelSelectText.Text = "Select parent entity:";
					listViewEx1.Enabled = true;
					listViewEx1.CheckBoxes = false;
					break;
				case RequestorTypes.Table:
					labelSelectText.Text = "Select existing entity:";
					listViewEx1.Enabled = true;
					listViewEx1.CheckBoxes = false;
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
			buttonOk.Enabled = checkBoxCreateNewEntity.Checked || SelectedItems.Count > 0;
		}

		private void listViewEx1_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			buttonOk.Enabled = checkBoxCreateNewEntity.Checked || SelectedItems.Count > 0;
		}

		private void listViewEx1_SizeChanged(object sender, EventArgs e)
		{
			listViewEx1.Columns[0].Width = listViewEx1.Width - 2;
		}

		private void listViewEx1_SelectedIndexChanged(object sender, EventArgs e)
		{
			buttonOk.Enabled = checkBoxCreateNewEntity.Checked || SelectedItems.Count > 0;
		}
	}
}
