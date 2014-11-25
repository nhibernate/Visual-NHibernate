using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormCreateNewRelationship : Form, IEventSender
	{
		private readonly List<IKey> primaryKeys = new List<IKey>();
		private readonly List<IKey> foreignKeys = new List<IKey>();

		public bool EventRaisingDisabled { get; set; }

		public FormCreateNewRelationship()
		{
			InitializeComponent();
		}
		
		public string RelationshipName 
		{ 
			get { return tbName.Text; }
			set
			{
				EventRaisingDisabled = true;
				tbName.Text = value;
				EventRaisingDisabled = false;
			} 
		}

		public IKey PrimaryKey
		{
			get { return comboBoxPK.GetSelectedItem<IKey>(); }
			set
			{
				EventRaisingDisabled = true;
				comboBoxPK.SetSelectedItem(value);
				labelTablePK.Text = value.Parent.Name;
				EventRaisingDisabled = false;
			}
		}

		public IKey ForeignKey
		{
			get { return comboBoxFK.GetSelectedItem<IKey>(); }
			set
			{
				EventRaisingDisabled = true;
				comboBoxFK.SetSelectedItem(value);
				labelTableFK.Text = value.Parent.Name;
				EventRaisingDisabled = false;
			}
		}

		public void SetPossiblePrimaryKeys(IEnumerable<IKey> keys)
		{
			primaryKeys.Clear();
			primaryKeys.AddRange(keys);

			RefreshKey1List();
		}

		public void SetPossibleForeignKeys(IEnumerable<IKey> keys)
		{
			foreignKeys.Clear();
			foreignKeys.AddRange(keys);

			RefreshKey2List();
		}

		private void RefreshKey1List()
		{
			EventRaisingDisabled = true;
			comboBoxPK.Items.Clear();

			foreach (var key in primaryKeys)
			{
				comboBoxPK.Items.Add(new ComboBoxItemEx<IKey>(key, k => k.Name));
			}

			if (comboBoxPK.Items.Count > 0)
			{
				comboBoxPK.SelectedIndex = 0;
				SetTableLabel(primaryKeys[0], labelTablePK);
			}
			EventRaisingDisabled = false;
		}

		private void RefreshKey2List()
		{
			EventRaisingDisabled = true;
			comboBoxFK.Items.Clear();
			
			foreach(var key in foreignKeys)
			{
				comboBoxFK.Items.Add(new ComboBoxItemEx<IKey>(key, k => k.Name));
			}
			
			if (comboBoxFK.Items.Count > 0) comboBoxFK.SelectedIndex = 0;
			if (comboBoxFK.Items.Count > 0)
			{
				comboBoxFK.SelectedIndex = 0;
				SetTableLabel(foreignKeys[0], labelTableFK);
			}
			EventRaisingDisabled = false;
		}

		private static void SetTableLabel(IKey value, Label label)
		{
			if (value != null && value.Parent != null)
			{
				label.Text = value.Parent.Name;
			}
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		private void comboBoxKey1_SelectedIndexChanged(object sender, EventArgs e)
		{
			labelTablePK.Text = PrimaryKey.Name;
		}

		private void comboBoxKey2_SelectedIndexChanged(object sender, EventArgs e)
		{
			labelTableFK.Text = ForeignKey.Name;
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}