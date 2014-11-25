using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.EventExtensions;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormCreateOneToOneMapping : Form, ICreateOneToOneMappingsForm
	{
		public event EventHandler ChangesAccepted;
		public event EventHandler Cancelled;


		private readonly List<ITable> allTables = new List<ITable>();
		private readonly HashSet<Guid> alreadyMappedTables = new HashSet<Guid>();

        public FormCreateOneToOneMapping(MappingSet set)
		{
			InitializeComponent();

            Set = set;
            SetAllTables(Set.Database.Tables);
            SetAlreadyMappedTables(Set.Mappings.Select(m => m.FromTable));
		}

        public MappingSet Set { get; set; }


		public IEnumerable<ITable> SelectedTables
		{
			get
			{
				var list = new List<ITable>();

				foreach (ListViewItem item in listViewTables.CheckedItems)
				{
					list.Add(item.Tag as ITable);
				}

				return list;
			}
		}

		public void SetAlreadyMappedTables(IEnumerable<ITable> tables)
		{
			alreadyMappedTables.Clear();
			foreach (var tableGuid in tables.Select(t => t.InternalIdentifier))
			{
				alreadyMappedTables.Add(tableGuid);
			}
			Populate();	
		}

		public void SetAllTables(IEnumerable<ITable> tables)
		{
			allTables.Clear();
			allTables.AddRange(tables);
			Populate();
		}

		private void Populate()
		{
			listViewTables.Items.Clear();
			
			foreach(var table in allTables)
			{
				var item = new ListViewItem();
				item.Text = table.Name;
				item.Checked = false;
				item.Tag = table;

				if (alreadyMappedTables.Contains(table.InternalIdentifier))
				{
					item.BackColor = Color.LightGray;
					item.ToolTipText = "This table has already been mapped to an entity.";
				}

				listViewTables.Items.Add(item);
			}
		}

		private void buttonCreate_Click(object sender, EventArgs e)
		{
            AcceptChanges();
		}

        private void AcceptChanges()
        {
            var tablesToProcess = SelectedTables;

            MappingProcessor proc = new MappingProcessor(new OneToOneEntityProcessor(Set.EntitySet.Entities.Select(e => e.Name)));
            proc.CreateOneToOneMappingsFor(tablesToProcess, Set);
            Close();
            //mainPanel.CloseDatabaseRefreshResultsForm(Changes.WereAccepted);
        }

		private void buttonCancel_Click(object sender, EventArgs e)
		{
            CancelChanges();
		}

        private void CancelChanges()
        {
            Close();
            //mainPanel.CloseDatabaseRefreshResultsForm(Changes.WereRejected);
        }

		private void selectAll_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem item in listViewTables.Items)
			{
				item.Checked = true;
			}
		}

		private void selectNone_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewTables.Items)
			{
				item.Checked = false;
			}
		}

		private void selectUnmapped_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewTables.Items)
			{
				ITable tag = (ITable)item.Tag;
				if (alreadyMappedTables.Contains(tag.InternalIdentifier))
					item.Checked = false;
				else
					item.Checked = true;
			}
		}
	}
}
