using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using DevComponents.DotNetBar;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormRelationship : UserControl, IRelationshipForm, IEventSender
	{
		private readonly List<IKey> primaryKeys = new List<IKey>();
		private readonly List<IKey> foreignKeys = new List<IKey>();

		public bool EventRaisingDisabled { get; set; }

		public event EventHandler RelationshipNameChanged;
		public event EventHandler PrimaryKeyChanged;
		public event EventHandler ForeignKeyChanged;
		public event EventHandler DeleteRelationship;

		public FormRelationship()
		{
			InitializeComponent();

			tbName.TextChanged += (sender, e) => RelationshipNameChanged.RaiseEventEx(this);
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

        public void RefreshVirtualProperties()
        {
            virtualPropertyGrid1.RefreshVisibilities();
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
			get { return comboBoxPrimaryKey.GetSelectedItem<IKey>(); }
			set
			{
				EventRaisingDisabled = true;
				comboBoxPrimaryKey.SetSelectedItem(value);
				SetTableLabel(value, labelTablePK);
				SetColumns(value.Columns, labelPrimaryKeyColumns);
				EventRaisingDisabled = false;
			}
		}

		public IKey ForeignKey
		{
			get { return comboBoxForeignKey.GetSelectedItem<IKey>(); }
			set
			{
				EventRaisingDisabled = true;
				comboBoxForeignKey.SetSelectedItem(value);
				SetTableLabel(value, labelTableFK);
				SetColumns(value.Columns, labelForeignKeyColumns);
				EventRaisingDisabled = false;
			}
		}

		private void SetColumns(IEnumerable<IColumn> columns, LabelX label)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Columns in Key:");

			foreach(var column in columns)
			{
				sb.AppendLine(column.Name);
			}

			label.Text = sb.ToString();
		}

		private static void SetTableLabel(IKey value, Label label)
		{
			if(value != null && value.Parent != null)
			{
				label.Text = value.Parent.Name;
			}
		}

		public void SetPossiblePrimaryKeys(IEnumerable<IKey> keys)
		{
			primaryKeys.Clear();
			primaryKeys.AddRange(keys);

			RefreshPrimaryList();
		}

		public void SetPossibleForeignKeys(IEnumerable<IKey> keys)
		{
			foreignKeys.Clear();
			foreignKeys.AddRange(keys);

			RefreshForeignKeyList();
		}

		private void RefreshPrimaryList()
		{
			EventRaisingDisabled = true;
			comboBoxPrimaryKey.Items.Clear();

			foreach (var key in primaryKeys)
			{
				comboBoxPrimaryKey.Items.Add(new ComboBoxItemEx<IKey>(key, k => k.Name));
			}

			if (comboBoxPrimaryKey.Items.Count > 0)
			{
				comboBoxPrimaryKey.SelectedIndex = 0;
				SetTableLabel(primaryKeys[0], labelTablePK);
			}

			EventRaisingDisabled = false;
		}

		private void RefreshForeignKeyList()
		{
			EventRaisingDisabled = true;
			comboBoxForeignKey.Items.Clear();
			
			foreach(var key in foreignKeys)
			{
				comboBoxForeignKey.Items.Add(new ComboBoxItemEx<IKey>(key, k => k.Name));
			}
			
			if (comboBoxForeignKey.Items.Count > 0)
			{
				comboBoxForeignKey.SelectedIndex = 0;
				SetTableLabel(foreignKeys[0], labelTableFK);
			}
			EventRaisingDisabled = false;
		}

		public void Clear()
		{
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteRelationship.RaiseEventEx(this);
		}

		private void comboBoxKey1_SelectedIndexChanged(object sender, EventArgs e)
		{
			PrimaryKeyChanged.RaiseEventEx(this);
		}

		private void comboBoxKey2_SelectedIndexChanged(object sender, EventArgs e)
		{
			ForeignKeyChanged.RaiseEventEx(this);
		}
	}
}