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
	public partial class FormSelectEntityForInheritance : Form
	{
		public enum RequestorTypes
		{
			Entity_Select_Parent,
			Entity_Create_Abstract_Parent,
			Entity_Select_Child,
			Entity_Select_Existing,
			Table,
			Component
		}
		internal event EventHandler EntityDeleted;
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
		private bool MultiSelectEntities = false;
		private Slyce.Common.TextBoxFocusHelper FocusHelper;
		private Slyce.Common.TextBoxFocusHelper TextBoxFocusHelper;
		private string DefaultNewAbstractParentName = "NewName";

		public FormSelectEntityForInheritance(Entity entity, List<Entity> unavailableEntities, Entity selectedEntity, string title, bool multiSelect, RequestorTypes requestorType)
		{
			InitializeComponent();

			SetStyle(
					ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer, true);

			Clear();

			inheritanceHierarchy1.EntityDeleted += new EventHandler(inheritanceHierarchy1_EntityDeleted);
			TextBoxFocusHelper = new Slyce.Common.TextBoxFocusHelper(new Control[] { textBoxName });
			textBoxName.Text = DefaultNewAbstractParentName;

			MultiSelectEntities = multiSelect;
			RequestorType = requestorType;
			Entity = entity;
			UnavailableEntities = unavailableEntities;
			Text = title;
			SelectedEntity = selectedEntity;
			listViewEntities.CheckBoxes = MultiSelectEntities;
			FocusHelper = new Slyce.Common.TextBoxFocusHelper(new TextBox[] { textBoxName });

			if (requestorType == RequestorTypes.Entity_Create_Abstract_Parent)
				MultiSelectEntities = true;

			Populate();

			superTabControl1.SelectedTab = tabSelectEntity;
		}

		void inheritanceHierarchy1_EntityDeleted(object sender, EventArgs e)
		{
			if (EntityDeleted != null)
				EntityDeleted(null, null);
		}

		public FormSelectEntityForInheritance(Table table, List<Entity> unavailableEntities, Entity selectedEntity, string title, bool multiSelect)
		{
			InitializeComponent();

			Clear();
			MultiSelectEntities = multiSelect;
			RequestorType = RequestorTypes.Table;
			Table = table;
			UnavailableEntities = unavailableEntities;
			Text = title;
			SelectedEntity = selectedEntity;
			FocusHelper = new Slyce.Common.TextBoxFocusHelper(new TextBox[] { textBoxName });
			Populate();
		}

		private void Clear()
		{
			CardinalityPrimary = null;
			CardinalityForeign = null;
			AssociationTable = null;
			SelectedRelationship = null;
		}

		public FormSelectEntityForInheritance(ComponentSpecification componentSpecification, List<Entity> unavailableEntities, Entity selectedEntity, string title, bool multiSelect)
		{
			InitializeComponent();

			MultiSelectEntities = multiSelect;
			RequestorType = RequestorTypes.Component;
			ComponentSpecification = componentSpecification;
			UnavailableEntities = unavailableEntities;
			Text = title;
			SelectedEntity = selectedEntity;
			Populate();
		}

		private void Populate()
		{
			#region Set hierarchy heading
			List<ITable> mappedTables = Entity.MappedTables().ToList();

			if (mappedTables.Count == 0)
			{
				labelHierarchyWarning.Visible = true;
				inheritanceHierarchy1.Visible = false;
				labelHierarchyHeader.Text = "  Create an inheritance hierarchy from a single table, based on a discriminator.";
				labelHierarchyWarning.Text = "  Can't create hierarchy - entity must be mapped to exactly 1 table. It has none.";
			}
			else if (mappedTables.Count > 1)
			{
				labelHierarchyWarning.Visible = true;
				inheritanceHierarchy1.Visible = false;
				labelHierarchyHeader.Text = "  Create an inheritance hierarchy from a single table, based on a discriminator.";
				labelHierarchyWarning.Text = string.Format("  Can't create hierarchy - entity must be mapped to exactly 1 table. It has {0}.", mappedTables.Count);
			}
			else
			{
				labelHierarchyWarning.Visible = false;
				inheritanceHierarchy1.Visible = true;
				inheritanceHierarchy1.Table = mappedTables[0];
				labelHierarchyHeader.Text = string.Format("  Create an inheritance hierarchy from this table [{0}], based on a discriminator.", mappedTables[0].Name);
			}
			#endregion

			#region Set Select Existing Entity Warning

			#endregion

			#region Hide/show controls
			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Child:

					tabCreateHierarchy.Visible = Entity.Children.Count == 0;
					tabCreateAbstractParent.Visible = false;
					textBoxName.Visible = false;
					labelSelectText.Visible = true;
					break;
				case RequestorTypes.Entity_Select_Parent:
				case RequestorTypes.Entity_Select_Existing:
				case RequestorTypes.Component:
					textBoxName.Visible = true;
					labelSelectText.Visible = true;
					break;
				case RequestorTypes.Table:
					textBoxName.Visible = true;
					labelSelectText.Text = "Select existing entity:";
					labelSelectText.Visible = true;
					break;
				case RequestorTypes.Entity_Create_Abstract_Parent:
					Text = "Create abstract parent";
					//tabSelectEntity.Visible = false;
					//tabCreateHierarchy.Visible = false;
					textBoxName.Visible = false;
					labelSelectText.Text = "Select properties to extract to parent:";
					labelSelectText.Top = 15;
					labelSelectText.Visible = true;
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
			#endregion

			#region Populate Entities ListView

			listViewEntities.Items.Clear();
			listViewEntities.MultiSelect = MultiSelectEntities;

			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Existing:
					labelSelectText.Text = "Select entity:";
					break;
				case RequestorTypes.Entity_Select_Child:
					labelSelectText.Visible = true;

					if (Entity.IsAbstract)
					{
						labelSelectEntitiesHeading.Text = "  Table Per Concrete Class inheritance";
						labelSelectText.Text = "Select child entities (only entities with properties matching abstract parent):";
						labelWarningSelectEntity.Text = "  No entities with properties matching abstract parent";
					}
					else
					{
						labelSelectText.Text = "Select child entities:";
					}

					listViewEntities.Enabled = true;
					listViewEntities.CheckBoxes = true;
					break;
				case RequestorTypes.Entity_Select_Parent:
					labelSelectText.Text = "Select parent entity:";
					listViewEntities.Enabled = true;
					listViewEntities.CheckBoxes = false;
					break;
				case RequestorTypes.Table:
					labelSelectText.Text = "Select existing entity:";
					listViewEntities.Enabled = true;
					listViewEntities.CheckBoxes = false;
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
			#endregion

			#region Populate Properties ListView
			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Existing:
					labelSelectText.Text = "Select entity:";
					PopulateItemsForEntity();
					break;
				case RequestorTypes.Entity_Select_Child:
					//labelSelectText.Visible = false;
					PopulateItemsForEntity();
					listViewProperties.Enabled = true;
					break;
				case RequestorTypes.Entity_Select_Parent:
					//MultiSelectEntities = true;
					PopulateItemsForEntity();
					listViewProperties.Enabled = true;
					listViewProperties.CheckBoxes = true;
					break;
				case RequestorTypes.Table:
					labelSelectText.Text = "Select columns to create properties:";
					PopulateItemsForEntity();
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
			#endregion

			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Existing:
				case RequestorTypes.Entity_Select_Child:
				case RequestorTypes.Entity_Select_Parent:
				case RequestorTypes.Entity_Create_Abstract_Parent:
					break;
				case RequestorTypes.Component:
					foreach (Entity entity in ComponentSpecification.EntitySet.Entities.Where(en => !UnavailableEntities.Contains(en)))
						listViewEntities.Items.Add(new ListViewItem(entity.Name) { Tag = entity });
					break;
				case RequestorTypes.Table:
					foreach (Entity entity in Table.Database.MappingSet.EntitySet.Entities.Where(en => !UnavailableEntities.Contains(en)))
						listViewEntities.Items.Add(new ListViewItem(entity.Name) { Tag = entity });
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
			if (SelectedEntity != null)
			{
				foreach (ListViewItem item in listViewEntities.Items)
				{
					if (item.Tag == SelectedEntity)
						item.Selected = true;
					else
						item.Selected = false;
				}
			}
			listViewEntities.Focus();
		}

		private void PopulateItemsForEntity()
		{
			listViewEntities.Items.Clear();
			listViewProperties.Items.Clear();

			List<Entity> oneToOneEntities = Entity.DirectedReferences.Where(d => ArchAngel.Interfaces.Cardinality.IsOneToOne(d.FromEndCardinality, d.ToEndCardinality)).Select(d => d.ToEntity).Where(en => en != Entity && !UnavailableEntities.Contains(en)).ToList();

			if (RequestorType == RequestorTypes.Entity_Select_Child &&
				Entity.IsAbstract)
			{
				labelSelectText.Visible = true;
				listViewEntities.Visible = true;
				labelWarningSelectEntity.Visible = false;
			}
			if (oneToOneEntities.Count == 0)
			{
				labelSelectText.Visible = false;
				listViewEntities.Visible = false;
				labelWarningSelectEntity.Visible = true;
			}
			else
			{
				labelSelectText.Visible = true;
				listViewEntities.Visible = true;
				labelWarningSelectEntity.Visible = false;
			}

			switch (RequestorType)
			{
				case RequestorTypes.Entity_Select_Existing:
					foreach (Entity entity in oneToOneEntities)
						listViewEntities.Items.Add(new ListViewItem(entity.Name) { Tag = entity });
					break;
				case RequestorTypes.Entity_Select_Child:
				case RequestorTypes.Entity_Select_Parent:
					foreach (Property property in Entity.Properties)
					{
						ListViewItem item = new ListViewItem(property.Name)
						{
							Tag = property,
							Checked = property.IsKeyProperty
						};
						listViewProperties.Items.Add(item);
					}
					if (RequestorType == RequestorTypes.Entity_Select_Child &&
						Entity.IsAbstract)
					{
						foreach (var ent in Entity.EntitySet.Entities.Where(e => e != Entity && !Entity.Children.Contains(e)))
						{
							bool allPropsFound = true;

							foreach (var property in Entity.Properties)
							{
								if (!ent.Properties.Any(p => p.Name == property.Name && p.Type == property.Type))
								{
									allPropsFound = false;
									break;
								}
							}
							if (allPropsFound)
								listViewEntities.Items.Add(new ListViewItem(ent.Name) { Tag = ent });
						}
						if (listViewEntities.Items.Count > 0)
						{
							labelSelectText.Visible = true;
							listViewEntities.Visible = true;
							labelWarningSelectEntity.Visible = false;
						}
						else
						{
							labelSelectText.Visible = false;
							listViewEntities.Visible = false;
							labelWarningSelectEntity.Visible = true;
						}
					}
					else
					{
						foreach (Entity entity in oneToOneEntities)
							listViewEntities.Items.Add(new ListViewItem(entity.Name) { Tag = entity });
					}
					break;
				case RequestorTypes.Entity_Create_Abstract_Parent:
					listViewEntities.CheckBoxes = true;

					foreach (Property property in Entity.Properties)
						listViewEntities.Items.Add(new ListViewItem(property.Name) { Tag = property });
					break;
				case RequestorTypes.Table:
					listViewEntities.CheckBoxes = true;

					foreach (Column column in Table.Columns)
						listViewEntities.Items.Add(new ListViewItem(column.Name) { Tag = column, Checked = true });
					break;
				default:
					throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
			}
		}

		private List<ListViewItem> SelectedEntityItems
		{
			get
			{
				List<ListViewItem> selectedItems = new List<ListViewItem>();

				if (MultiSelectEntities)
					foreach (ListViewItem item in listViewEntities.CheckedItems)
						selectedItems.Add(item);
				else
					foreach (ListViewItem item in listViewEntities.SelectedItems)
						selectedItems.Add(item);

				return selectedItems;
			}
		}

		private List<ListViewItem> SelectedPropertyItems
		{
			get
			{
				List<ListViewItem> selectedItems = new List<ListViewItem>();

				foreach (ListViewItem item in listViewProperties.CheckedItems)
					selectedItems.Add(item);

				return selectedItems;
			}
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			SelectedEntities = new List<Entity>();

			if (superTabControl1.SelectedTab == tabCreateHierarchy)
			{
				if (inheritanceHierarchy1.Save())
				{
					Close();
					return;
				}
				else
					return;
			}
			if (superTabControl1.SelectedTab == tabCreateAbstractParent ||
				RequestorType == RequestorTypes.Entity_Create_Abstract_Parent)// checkBoxCreateNewEntity.Checked || RequestorType == RequestorTypes.Entity_Create_Abstract_Parent)
			{
				if (!CreateNewEntity())
					return;
			}
			else
			{
				if (SelectedEntityItems.Count > 0)
				{
					List<string> invalidSelections = new List<string>();

					foreach (ListViewItem item in SelectedEntityItems)
					{
						Entity entity = (Entity)item.Tag;

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

						return;
					}
				}
				else
					return;

				if (SelectedEntity == null && !MultiSelectEntities && SelectedEntities.Count > 0)
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
			if (!MultiSelectEntities)
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
			if (RequestorType == RequestorTypes.Table)
			{
				List<ITable> tablesToProcess = new List<ITable>();
				tablesToProcess.Add(Table);
				EntityModel.Controller.MappingLayer.MappingProcessor proc = new EntityModel.Controller.MappingLayer.MappingProcessor(new EntityModel.Controller.MappingLayer.OneToOneEntityProcessor(Table.Database.MappingSet.EntitySet.Entities.Select(ent => ent.Name)));
				List<Entity> newEntities = proc.CreateOneToOneMappingsFor(tablesToProcess, Table.Database.MappingSet);
				SelectedEntity = newEntities[0];

				#region Remove un-selected columns
				List<string> selectedColumnNames = new List<string>();

				foreach (ListViewItem col in listViewEntities.CheckedItems)
					selectedColumnNames.Add(col.Text);

				for (int i = SelectedEntity.Properties.Count() - 1; i >= 0; i--)
				{
					Property property = SelectedEntity.Properties.ElementAt(i);

					if (!selectedColumnNames.Contains(property.MappedColumn().Name))
						SelectedEntity.RemoveProperty(property);
				}
				#endregion

				SelectedEntity.Name = textBoxName.Text;
				return true;
			}
			else if (RequestorType == RequestorTypes.Entity_Select_Parent ||
				RequestorType == RequestorTypes.Entity_Create_Abstract_Parent)
			{
				if (SelectedPropertyItems.Count == 0)
				{
					MessageBox.Show(this, "You need to select some properties to be moved into the abstract parent (Table Per Concrete Class)", "No properties selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				if (RequestorType == RequestorTypes.Entity_Create_Abstract_Parent &&
					string.IsNullOrWhiteSpace(textBoxName.Text) ||
					textBoxName.Text == DefaultNewAbstractParentName)
				{
					MessageBox.Show(this, "Please enter a name for the new abstract parent entity.", "Name missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				Entity newEntity = new EntityImpl(textBoxName.Text) { Schema = Entity.Schema };
				newEntity.IsAbstract = true;

				// Check that all key-properties are selected
				List<Property> selectedProperties = new List<Property>();

				foreach (ListViewItem item in SelectedPropertyItems)
					selectedProperties.Add((Property)item.Tag);

				List<Property> unselectedKepProperties = new List<Property>();

				foreach (Property prop in Entity.Properties)
					if (prop.IsKeyProperty && !selectedProperties.Contains(prop))
						unselectedKepProperties.Add(prop);

				if (unselectedKepProperties.Count > 0)
				{
					string keyPropNames = "";

					foreach (Property p in unselectedKepProperties)
						keyPropNames += p.Name + ", ";

					MessageBox.Show(this, "All key properties need to be selected: " + keyPropNames.TrimEnd(',', ' '), "Invalid selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				foreach (ListViewItem item in SelectedPropertyItems)
				{
					Property existingProperty = (Property)item.Tag;
					PropertyImpl newProp = new PropertyImpl(existingProperty.Name)
						{
							Type = existingProperty.Type,
							NHibernateType = existingProperty.NHibernateType,
							IsKeyProperty = existingProperty.IsKeyProperty
						};

					//newProp.SetMappedColumn(existingProperty.MappedColumn());
					newEntity.AddProperty(newProp);
					//newEntity.AddProperty(existingProperty);

					// Remove this property from the existing Entity, as it's now been added to the parent
					//if (existingProperty.IsKeyProperty)
					existingProperty.IsHiddenByAbstractParent = true;
					//else
					//	Entity.RemoveProperty(existingProperty);
				}
				//newEntity.AddChild(Entity);
				//Entity.Parent = newEntity;
				Entity.EntitySet.AddEntity(newEntity);
				SelectedEntity = newEntity;
				return true;
			}
			else if (RequestorType == RequestorTypes.Entity_Select_Child)
			{
				if (SelectedPropertyItems.Count == 0)
				{
					MessageBox.Show(this, "You need to select some child entities.", "No entities selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
				Entity newEntity = new EntityImpl("NewChildEntity") { Schema = Entity.Schema };

				foreach (ListViewItem item in SelectedPropertyItems)
				{
					Property existingProperty = (Property)item.Tag;
					newEntity.AddProperty(new PropertyImpl(existingProperty.Name)
					{
						Type = existingProperty.Type,
						NHibernateType = existingProperty.NHibernateType
					});
				}
				//newEntity.AddChild(Entity);
				//Entity.Parent = newEntity;
				Entity.EntitySet.AddEntity(newEntity);
				SelectedEntity = newEntity;
				return true;
			}
			else
				throw new NotImplementedException("RequestorType not handled yet: " + RequestorType.ToString());
		}

		private void listViewEx1_SizeChanged(object sender, EventArgs e)
		{
			listViewEntities.Columns[0].Width = listViewEntities.Width - 2;
		}

		private void listViewProperties_SizeChanged(object sender, EventArgs e)
		{
			listViewProperties.Columns[0].Width = listViewProperties.Width - 2;
		}

		private void buttonLaunchHierarchyEditor_Click(object sender, EventArgs e)
		{
			//if (Entity.MappedTables().Count() == 0)
			//{
			//    MessageBox.Show(this, "No tables are mapped to this entity. 'Table per Hierarchy' inheritance requires exactly one mapped table.", "No mapped table", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}
			//else if (Entity.MappedTables().Count() > 1)
			//{
			//    MessageBox.Show(this, "Multiple tables are mapped to this entity. 'Table per Hierarchy' inheritance requires exactly one mapped table.", "Too many mapped tables", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}
			//UserControls.FormInheritanceHierarchy form = new UserControls.FormInheritanceHierarchy(Entity.MappedTables().ElementAt(0));
			//form.ShowDialog(this);
			//Populate();
		}

		private void tabCreateAbstractParent_GotFocus(object sender, EventArgs e)
		{
			textBoxName.Focus();
		}

		private void buttonSelectAllProperties_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewProperties.Items)
				item.Checked = true;
		}

		private void buttonClearAllProperties_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewProperties.Items)
				item.Checked = false;
		}

		private void superTabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
		{
			if (superTabControl1.SelectedTab == tabCreateAbstractParent)
				textBoxName.Focus();
		}
	}
}
