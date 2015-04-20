using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormSubClassDiscriminator : Form
	{
		public FormSubClassDiscriminator(Entity entity)
		{
			InitializeComponent();
			Entity = entity;
			Populate();
		}

		private Entity Entity
		{
			get;
			set;
		}

		private void Populate()
		{
			comboBoxColumns.Items.Clear();
			var tables = Entity.MappedTables().ToList();

			if (tables.Count == 1)
				foreach (var col in tables[0].Columns.OrderBy(c => c.Name))
					comboBoxColumns.Items.Add(col.Name);
			else
				foreach (var table in tables.OrderBy(t => t.Name))
				foreach (var col in table.Columns.OrderBy(c => c.Name))
					comboBoxColumns.Items.Add(string.Format("{0}.{1}", table.Name, col.Name));
		}
	}
}
