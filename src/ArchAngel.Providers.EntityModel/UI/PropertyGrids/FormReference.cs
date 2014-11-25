using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormReference : UserControl, IReferenceForm, IEventSender
	{
		private IEnumerable<Entity> entityList;

		private static readonly Cardinality[] cardinalities = new[] {Cardinality.One, Cardinality.Many};

		public FormReference()
		{
			InitializeComponent();

			comboBoxCardinality1.Items.Add("One");
			comboBoxCardinality1.Items.Add("Many");
			comboBoxCardinality2.Items.Add("One");
			comboBoxCardinality2.Items.Add("Many");

			comboBoxCardinality1.SelectedIndex = 0;
			comboBoxCardinality1.SelectedIndex = 1;

			comboBoxEntity1.SelectedIndexChanged += (sender, e) => Entity1Changed.RaiseEventEx(this);
			comboBoxEntity2.SelectedIndexChanged += (sender, e) => Entity2Changed.RaiseEventEx(this);
			comboBoxCardinality1.SelectedIndexChanged += (sender, e) => End1CardinalityChanged.RaiseEventEx(this);
			comboBoxCardinality2.SelectedIndexChanged += (sender, e) => End2CardinalityChanged.RaiseEventEx(this);
			checkBoxEnd1Enabled.CheckedChanged += (sender, e) => End1EnabledChanged.RaiseEventEx(this);
			checkBoxEnd2Enabled.CheckedChanged += (sender, e) => End2EnabledChanged.RaiseEventEx(this);
			textBoxEnd1Name.TextChanged += (sender, e) => End1NameChanged.RaiseEventEx(this);
			textBoxEnd2Name.TextChanged += (sender, e) => End2NameChanged.RaiseEventEx(this);
			comboBoxMappedTable.SelectedIndexChanged += (sender, e) => MappedTableChanged.RaiseEventEx(this);
			comboBoxMappedRelationship.SelectedIndexChanged += (sender, e) => MappedRelationshipChanged.RaiseEventEx(this);
		}

		public void Clear()
		{
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

	    public void RefreshVirtualProperties()
	    {
	        virtualPropertyGrid1.RefreshVisibilities();
	    }

	    public void MappedTableSelectionEnabled(bool enabled)
		{
			comboBoxMappedTable.Enabled = enabled;
		}

		public void MappedRelationshipSelectionEnabled(bool enabled)
		{
			comboBoxMappedRelationship.Enabled = enabled;
		}

		public IEnumerable<Entity> EntityList
		{
			get
			{
				return entityList;
			}
			set
			{
				EventRaisingDisabled = true;
				entityList = value;
				RefreshEntityLists();
				EventRaisingDisabled = false;
			}
		}

		private IEnumerable<ITable> mappedTableList;
		public IEnumerable<ITable> MappedTableList
		{
			get
			{
				return mappedTableList;
			}
			set
			{
				EventRaisingDisabled = true;
				mappedTableList = value;
				RefreshMappedTableList();
				EventRaisingDisabled = false;
			}
		}

		private IEnumerable<Relationship> mappedRelationshipList;
		public IEnumerable<Relationship> MappedRelationshipList
		{
			get
			{
				return mappedRelationshipList;
			}
			set
			{
				EventRaisingDisabled = true;
				mappedRelationshipList = value;
				RefreshMappedRelationshipsList();
				EventRaisingDisabled = false;
			}
		}

		public Relationship MappedRelationship
		{
			get
			{
				return GetSelectedValue<Relationship>(comboBoxMappedRelationship);
			}
			set
			{
				EventRaisingDisabled = true;
				SetSelectedValue(comboBoxMappedRelationship, value, v => v.Name);
				EventRaisingDisabled = false;
			}
		}

		public ITable MappedTable
		{
			get
			{
				return GetSelectedValue<ITable>(comboBoxMappedTable);
			}
			set
			{
				EventRaisingDisabled = true;
				SetSelectedValue(comboBoxMappedTable, value, v => v.Name);
				EventRaisingDisabled = false;
			}
		}		

		public Entity Entity1
		{
			get
			{
				return GetSelectedValue<Entity>(comboBoxEntity1);
			}
			set
			{
				EventRaisingDisabled = true;
				SetSelectedValue(comboBoxEntity1, value, v => v.Name);
				EventRaisingDisabled = false;
			}
		}

		public Entity Entity2
		{
			get
			{
				return GetSelectedValue<Entity>(comboBoxEntity2);
			}
			set
			{
				EventRaisingDisabled = true;
				SetSelectedValue(comboBoxEntity2, value, v => v.Name);
				EventRaisingDisabled = false;
			}
		}
	
		
		public bool End1Enabled
		{
			get
			{
				return checkBoxEnd1Enabled.Checked;
			}
			set
			{
				EventRaisingDisabled = true;
				checkBoxEnd1Enabled.Checked = value;
				EventRaisingDisabled = false;
			}
		}

		public bool End2Enabled
		{
			get
			{
				return checkBoxEnd2Enabled.Checked;
			}
			set
			{
				EventRaisingDisabled = true;
				checkBoxEnd2Enabled.Checked = value;
				EventRaisingDisabled = false;
			}
		}

		public Cardinality End1Cardinality
		{
			get
			{
				return cardinalities[comboBoxCardinality1.SelectedIndex];
			}
			set
			{
				EventRaisingDisabled = true;
				comboBoxCardinality1.SelectedIndex = Cardinality.One.Equals(value) ? 0 : 1;
				EventRaisingDisabled = false;
			}
		}

		public Cardinality End2Cardinality
		{
			get
			{
				return cardinalities[comboBoxCardinality2.SelectedIndex];
			}
			set
			{
				EventRaisingDisabled = true;
				comboBoxCardinality2.SelectedIndex = Cardinality.One.Equals(value) ? 0 : 1;
				EventRaisingDisabled = false;
			}
		}

		public string End1Name
		{
			get
			{
				return textBoxEnd1Name.Text;
			}
			set
			{
				EventRaisingDisabled = true;
				textBoxEnd1Name.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public string End2Name
		{
			get
			{
				return textBoxEnd2Name.Text;
			}
			set
			{
				EventRaisingDisabled = true;
				textBoxEnd2Name.Text = value;
				EventRaisingDisabled = false;
			}
		}

		private void SetSelectedValue<T>(ComboBoxEx boxEx, T value, Func<T, string> getName) where T : class
		{
			ComboBoxItemEx<T> item = boxEx.Items.OfType<ComboBoxItemEx<T>>().FirstOrDefault(i => i.Object == value);
			if(item == null)
			{
				item = new ComboBoxItemEx<T>(value, getName);
				boxEx.Items.Add(item);
			}
			boxEx.SelectedItem = item;
		}

		private T GetSelectedValue<T>(ComboBoxEx boxEx) where T : class
		{
			ComboBoxItemEx<T> item = boxEx.SelectedItem as ComboBoxItemEx<T>;
			return item == null ? null : item.Object;
		}

		public bool EventRaisingDisabled { get; set; }

		private void RefreshEntityLists()
		{
			Entity entity1 = Entity1;
			Entity entity2 = Entity2;

			EventRaisingDisabled = true;

			comboBoxEntity1.Items.Clear();
			comboBoxEntity2.Items.Clear();
			comboBoxEntity1.Sorted = false;
			comboBoxEntity2.Sorted = false;

			foreach(var entity in entityList)
			{
				comboBoxEntity1.Items.Add(new ComboBoxItemEx<Entity>(entity, e => e.Name));
				comboBoxEntity2.Items.Add(new ComboBoxItemEx<Entity>(entity, e => e.Name));
				
				if (entity == entity1)
					comboBoxEntity1.SelectedIndex = comboBoxEntity1.Items.Count - 1;
				else if (entity == entity2)
					comboBoxEntity2.SelectedIndex = comboBoxEntity1.Items.Count - 1;
			}
			comboBoxEntity1.Sorted = true;
			comboBoxEntity2.Sorted = true;

			EventRaisingDisabled = false;
		}

		private void RefreshMappedTableList()
		{
			comboBoxMappedTable.Items.Clear();
			comboBoxMappedTable.Items.Add(new ComboBoxItemEx<ITable>(null, t => ""));

			ITable currentlySelected = MappedTable;
			AddAllToBox(comboBoxMappedTable, mappedTableList, currentlySelected, t => t.Name);
		}

		private void AddAllToBox<T>(ComboBoxEx comboBox, IEnumerable<T> items, T selectedItem, Func<T, string> getName)
			where T : class
		{
			foreach (var item in items)
			{
				comboBox.Items.Add(new ComboBoxItemEx<T>(item, getName));
				if (item == selectedItem)
					comboBox.SelectedIndex = comboBox.Items.Count - 1;
			}
			if (selectedItem == null)
				comboBox.SelectedIndex = 0;
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteRelationship.RaiseEventEx(this);
		}

		private void RefreshMappedRelationshipsList()
		{
			comboBoxMappedRelationship.Items.Clear();
			comboBoxMappedRelationship.Items.Add(new ComboBoxItemEx<Relationship>(null, t => ""));

			Relationship currentlySelected = MappedRelationship;
			AddAllToBox(comboBoxMappedRelationship, mappedRelationshipList, currentlySelected, t => string.Format("{0}: {1} to {2}", t.Name, t.PrimaryTable.Name, t.ForeignTable.Name));
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		public event EventHandler Entity1Changed;
		public event EventHandler Entity2Changed;
		public event EventHandler End1NameChanged;
		public event EventHandler End2NameChanged;
		public event EventHandler End1EnabledChanged;
		public event EventHandler End2EnabledChanged;
		public event EventHandler End1CardinalityChanged;
		public event EventHandler End2CardinalityChanged;
		public event EventHandler MappedTableChanged;
		public event EventHandler MappedRelationshipChanged;
		public event EventHandler DeleteRelationship;
	}
}
