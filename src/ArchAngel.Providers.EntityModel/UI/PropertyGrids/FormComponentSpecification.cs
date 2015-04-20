using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormComponentSpecification : UserControl, IComponentSpecificationForm, IEventSender
	{
		private readonly List<Entity> usageEntities = new List<Entity>();
		private readonly List<Entity> allEntities = new List<Entity>();
		private readonly List<ComponentProperty> properties = new List<ComponentProperty>();

		private readonly Image editImage;

		public FormComponentSpecification()
		{
			InitializeComponent();

			using (Stream editImageStream = GetType().Assembly.GetManifestResourceStream("ArchAngel.Providers.EntityModel.Resources.edit_16_h.png"))
			{
				if (editImageStream == null) return;

				editImage = Image.FromStream(editImageStream);
			}
		}

		public void Clear()
		{
			EventRaisingDisabled = true;

			properties.Clear();
			usageEntities.Clear();
			gridViewProperties.Rows.Clear();
			textBoxName.Text = "";
			listBoxUsages.Items.Clear();
			allEntities.Clear();

			EventRaisingDisabled = false;
		}

		public string SpecName
		{
			get { return textBoxName.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxName.Text = value;
				EventRaisingDisabled = false;
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

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid.SetVirtualProperties(virtualProperties);
		}

		public void RefreshVirtualProperties()
		{
			virtualPropertyGrid.RefreshVisibilities();
		}

		public string GetPropertyName(ComponentProperty property)
		{
			int index = properties.IndexOf(property);
			return gridViewProperties.Rows[index].Cells[PropertyNameColumn.Index].Value as string;
		}

		public void SetProperties(IEnumerable<ComponentProperty> properties)
		{
			this.properties.Clear();
			this.properties.AddRange(properties);

			EventRaisingDisabled = true;
			RefreshProperties();
			EventRaisingDisabled = false;
		}

		private void RefreshProperties()
		{
			Utility.SuspendPainting(this);
			
			gridViewProperties.Rows.Clear();
			AddAllPropertiesToGrid();

			Utility.ResumePainting(this);
		}

		private void AddAllPropertiesToGrid()
		{
			foreach (var property in properties)
			{
				AddNewColumnPropertyMappingRow(property);
			}
		}

		private void AddNewColumnPropertyMappingRow(ComponentProperty property)
		{
			var row = new DataGridViewRow();
			row.Tag = property;
			gridViewProperties.AllowUserToAddRows = true;
			row.CreateCells(gridViewProperties);
			row.Cells[PropertyNameColumn.Index].Value = property.Name;
			gridViewProperties.Rows.Add(row);

			gridViewProperties.AllowUserToAddRows = false;
		}

		public void SetUsages(IEnumerable<Entity> entities)
		{
			EventRaisingDisabled = true;
			listBoxUsages.Items.Clear();

			usageEntities.AddRange(entities);
			foreach(var entity in entities)
			{
				listBoxUsages.Items.Add(new ComboBoxItemEx<Entity>(entity, e => e.Name));
			}
			EventRaisingDisabled = false;
		}

		public void SetFullEntityList(IEnumerable<Entity> entities)
		{
			allEntities.Clear();
			allEntities.AddRange(entities);
		}

		private void buttonNewProperty_Click(object sender, EventArgs e)
		{
			CreateNewProperty.RaiseEventEx(this);
		}

		private void buttonRemoveSpec_Click(object sender, EventArgs e)
		{
			DeleteSpec.RaiseEventEx(this);	
		}

		private void gridViewProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			ComponentProperty property = gridViewProperties.Rows[e.RowIndex].Tag as ComponentProperty;

			if (e.ColumnIndex == DeletePropertyColumn.Index)
			{
				DeleteProperty.RaiseEventEx(this, new GenericEventArgs<ComponentProperty>(property));
			}
			else if (e.ColumnIndex == EditPropertyColumn.Index)
			{
				EditProperty.RaiseEventEx(this, new GenericEventArgs<ComponentProperty>(property));
			}
		}

		private void gridViewProperties_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var row = gridViewProperties.Rows[e.RowIndex];

			if (e.ColumnIndex == PropertyNameColumn.Index)
			{
				ComponentProperty changedProperty = row.Tag as ComponentProperty;
				PropertyNameChanged.RaiseEventEx(this, new GenericEventArgs<ComponentProperty>(changedProperty));
			}
		}

		private void gridViewProperties_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			throw new Exception("A data validation error occurred where it should not have", e.Exception);
		}

		private void gridViewProperties_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == EditPropertyColumn.Index && e.RowIndex >= 0)
			{
				if (editImage == null) return;

				e.Paint(e.CellBounds, DataGridViewPaintParts.All);

				int topPadding = (e.CellBounds.Height - 16) / 2;
				int leftPadding = (e.CellBounds.Width - 16) / 2;
				e.Graphics.DrawImage(editImage, e.CellBounds.Left + leftPadding, e.CellBounds.Top + topPadding, 16, 16);
				e.Handled = true;
			}
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			SpecNameChanged.RaiseEventEx(this);
		}

		private void listBoxUsages_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int index = listBoxUsages.IndexFromPoint(e.Location);
			if(index >= 0)
				NavigateToUsage.RaiseEventEx(this, new GenericEventArgs<Entity>(usageEntities[index]));
		}

		private void addUsage_Click(object sender, EventArgs e)
		{
			FormSelectEntity form = new FormSelectEntity(allEntities);

			var result = form.ShowDialog(this);

			if(result == DialogResult.OK)
			{
				AddNewUsage.RaiseEventEx(this, new GenericEventArgs<Entity>(form.SelectedEntity));
			}
		}

		public event EventHandler SpecNameChanged;
		public event EventHandler<GenericEventArgs<ComponentProperty>> PropertyNameChanged;
		public event EventHandler<GenericEventArgs<ComponentProperty>> EditProperty;
		public event EventHandler<GenericEventArgs<ComponentProperty>> DeleteProperty;
		public event EventHandler DeleteSpec;
		public event EventHandler CreateNewProperty;
		public event EventHandler<GenericEventArgs<Entity>> NavigateToUsage;
		public event EventHandler<GenericEventArgs<Entity>> AddNewUsage;
		public bool EventRaisingDisabled { get; set; }
	}
}
