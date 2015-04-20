using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Controls.AssociationWizard
{
	public partial class Screen2 : Interfaces.Controls.ContentItems.ContentItem
	{
		private List<int> AutoMappedNodes = new List<int>();

		public Screen2()
		{
			InitializeComponent();
			HasPrev = true;
			HasNext = false;
			HasFinish = true;
			NextText = "&Finish";
			PageHeader = "Column Mappings";
			PageDescription = "Specify how the columns of both objects are mapped.";
		}

		public void Populate()
		{
			AutoMappedNodes.Clear();
			lblPrimaryObjectName.Text = string.Format("{0} [{1}]", frmAssociationWizard.Association.PrimaryObject.GetType().Name, frmAssociationWizard.Association.PrimaryObject.Alias);
			lblAssociatedObjectName.Text = string.Format("{0} [{1}]", frmAssociationWizard.Association.AssociatedObject.GetType().Name, frmAssociationWizard.Association.AssociatedObject.Alias);

			if (typeof(Model.StoredProcedure).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
			{
				labelSentence1.Text = string.Format("I want to specify how the columns of {0} [{1}] map to the", frmAssociationWizard.Association.PrimaryObject.GetType().Name, frmAssociationWizard.Association.PrimaryObject.Alias);
			}
			else
			{
				labelSentence1.Text = string.Format("I want to specify how the columns of {0} [{1}] map to the columns of {2} [{3}].", frmAssociationWizard.Association.PrimaryObject.GetType().Name, frmAssociationWizard.Association.PrimaryObject.Alias, frmAssociationWizard.Association.AssociatedObject.GetType().Name, frmAssociationWizard.Association.AssociatedObject.Alias);
			}
			gridMappings.Rows.Clear();
            FillMapColumns();

			foreach (Model.Column column in frmAssociationWizard.Association.PrimaryObject.Columns)
			{
				AddTreelistNode(column);
			}
			LayoutControls();
			SetColumnHeading();

            DataGridViewComboBoxColumn columnColumn = (DataGridViewComboBoxColumn)gridMappings.Columns[1];
            DataGridViewComboBoxColumn paramColumn = (DataGridViewComboBoxColumn)gridMappings.Columns[2];

			foreach (Model.Association.Mapping mapping in frmAssociationWizard.Association.Mappings)
			{
                foreach (DataGridViewRow row in gridMappings.Rows)
				{
                    string nodeName = row.Cells[0].Value == null ? "" : row.Cells[0].Value.ToString();

                    if (!string.IsNullOrEmpty(nodeName))
                    {
                        nodeName = nodeName.Substring(0, nodeName.IndexOf("(")).Trim();

                        if (mapping.PrimaryColumn != null &&
                            nodeName == mapping.PrimaryColumn.Alias)
                        {
                            if (mapping.AssociatedColumn != null)
                            {
                                string name = string.Format("{0} ({1})", mapping.AssociatedColumn.Alias, mapping.AssociatedColumn.DataType);
                                
                                if (columnColumn.Items.IndexOf(name) >= 0)
                                {
                                    row.Cells[1].Value = name;
                                }
                            }
                            if (mapping.AssociatedParameter != null)
                            {
                                string name = string.Format("@{0} ({1})", mapping.AssociatedParameter.Alias, mapping.AssociatedParameter.DataType);

                                if (paramColumn.Items.IndexOf(name) >= 0)
                                {
                                    row.Cells[2].Value = name;
                                }
                            }
                            break;
                        }
                    }
				}
			}
		}

		private void FillMappings()
		{
			foreach (Model.Association.Mapping mapping in frmAssociationWizard.Association.Mappings)
			{
                foreach (DataGridViewRow row in gridMappings.Rows)
				{
					Model.Column column = (Model.Column)row.Tag;

					if (column == mapping.PrimaryColumn)
					{
						row.Cells[1].Value = mapping.AssociatedColumn;
						row.Cells[2].Value = mapping.AssociatedParameter;
						break;
					}
				}
			}
		}

		private void AutoMap()
		{
			List<string> unassignedAliases = UnMappedAssociatedColumnAliases;

            for (int nodeCounter = 0; nodeCounter < gridMappings.Rows.Count; nodeCounter++)
			{
                DataGridViewRow row = gridMappings.Rows[nodeCounter];
				string associatedColumnAlias = row.Cells[1].Value.ToString();

				if (string.IsNullOrEmpty(associatedColumnAlias))
				{
					Model.Column column = (Model.Column)row.Tag;

					for (int unassignedCounter = unassignedAliases.Count - 1; unassignedCounter >= 0; unassignedCounter--)
					{
						string availableAlias = unassignedAliases[unassignedCounter];

						if (availableAlias.ToLower().Replace("_", "").IndexOf(column.Alias.ToLower().Replace("_", "")) >= 0)
						{
							Model.Column associatedColumn = FindAssociatedColumn(availableAlias);

							if (associatedColumn != null && associatedColumn.DataType == column.DataType)
							{
								row.Cells[1].Value = string.Format("{0} ({1})", availableAlias, associatedColumn.DataType);
								unassignedAliases.RemoveAt(unassignedCounter);
								AutoMappedNodes.Add(nodeCounter);
                                row.Cells[1].Style.ForeColor = Color.Green;
							}
						}
					}
				}
			}
			AutoMappedNodes.Sort();
		}

		private List<string> UnMappedAssociatedColumnAliases
		{
			get
			{
				List<string> associatedColumnAliases = new List<string>();

				foreach (Model.Column column in frmAssociationWizard.Association.AssociatedObject.Columns)
				{
					associatedColumnAliases.Add(column.Alias);
				}
				associatedColumnAliases.Sort();

                foreach (DataGridViewRow row in gridMappings.Rows)
				{
					string alias = row.Cells[1].Value.ToString();

					if (!string.IsNullOrEmpty(alias))
					{
						associatedColumnAliases.Remove(alias);
					}
				}
				return associatedColumnAliases;
			}
		}

		private void FillMapColumns()
		{
            DataGridViewComboBoxColumn columnColumn = (DataGridViewComboBoxColumn)gridMappings.Columns[1];
            DataGridViewComboBoxColumn paramColumn = (DataGridViewComboBoxColumn)gridMappings.Columns[2];

            columnColumn.Items.Clear();
            paramColumn.Items.Clear();
            columnColumn.Sorted = false;
            paramColumn.Sorted = false;

			if (typeof(Model.StoredProcedure).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
			{
				foreach (Model.Column column in frmAssociationWizard.Association.AssociatedObject.Columns)
				{
                    columnColumn.Items.Add(string.Format("{0} ({1})", column.Alias, column.DataType));
				}
				foreach (Model.StoredProcedure.Parameter parameter in ((Model.StoredProcedure)frmAssociationWizard.Association.AssociatedObject).Parameters)
				{
                    paramColumn.Items.Add(string.Format("@{0} ({1})", parameter.Alias, parameter.DataType));
				}
			}
			else
			{
				foreach (Model.Column column in frmAssociationWizard.Association.AssociatedObject.Columns)
				{
                    columnColumn.Items.Add(string.Format("{0} ({1})", column.Alias, column.DataType));
				}
			}
            columnColumn.Sorted = true;
            paramColumn.Sorted = true;
		}

		private void AddTreelistNode(Model.Column column)
		{
			Model.Association.Mapping mapping = frmAssociationWizard.Association.FindMappingByPrimaryColumn(column);
			string associatedColumnName = "";

			if (mapping != null && mapping.AssociatedColumn != null && mapping.AssociatedColumn.Parent == frmAssociationWizard.Association.AssociatedObject)
			{
				associatedColumnName = string.Format("{0} ({1})", mapping.AssociatedColumn.Alias, mapping.AssociatedColumn.DataType);
			}
            int newIndex = gridMappings.Rows.Add(new object[] { string.Format("{0} ({1})", column.Alias, column.DataType), associatedColumnName });
            gridMappings.Rows[newIndex].Tag = column;
		}

		public override void OnDisplaying()
		{
			Populate();
		}

		private void Screen2_Resize(object sender, EventArgs e)
		{
			LayoutControls();
		}

		private void LayoutControls()
		{
			lblAssociatedObjectName.Left = ClientSize.Width / 2;
			lblAssociatedObjectName.Width = gridMappings.Right - lblAssociatedObjectName.Left - 5;

            lblPrimaryObjectName.Left = gridMappings.Left + 5;
			lblPrimaryObjectName.Width = lblAssociatedObjectName.Left - 5 - lblPrimaryObjectName.Left;
		}

		public override bool Back()
		{
			foreach (DataGridViewRow row in gridMappings.Rows)
			{
				string associatedColumnAlias = row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString();
                string associatedParameterAlias = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();

				if (associatedColumnAlias.IndexOf("(") > 0)
				{
					associatedColumnAlias = associatedColumnAlias.Substring(0, associatedColumnAlias.IndexOf("(")).Trim();
				}
				if (associatedParameterAlias.IndexOf("(") > 0)
				{
					associatedParameterAlias = associatedParameterAlias.Substring(0, associatedParameterAlias.IndexOf("(")).Trim();
				}
				associatedParameterAlias = associatedParameterAlias.Replace("@", "");
				Model.Column primaryColumn = (Model.Column)row.Tag;
				Model.Association.Mapping mapping = frmAssociationWizard.Association.FindMappingByPrimaryColumn(primaryColumn);

				if (mapping != null)
				{
					mapping.AssociatedColumn = FindAssociatedColumn(associatedColumnAlias);
				}
				else
				{
					Model.Association.Mapping newMapping = new Model.Association.Mapping(primaryColumn, FindAssociatedColumn(associatedColumnAlias), FindAssociatedParameter(associatedParameterAlias));
					frmAssociationWizard.Association.Mappings.Add(newMapping);
				}
			}
			return true;
		}

		private static Model.Column FindAssociatedColumn(string alias)
		{
			foreach (Model.Column column in frmAssociationWizard.Association.AssociatedObject.Columns)
			{
				if (Slyce.Common.Utility.StringsAreEqual(column.Alias, alias, false))
				{
					return column;
				}
			}
			return null;
		}

		private static Model.StoredProcedure.Parameter FindAssociatedParameter(string alias)
		{
			if (typeof(Model.StoredProcedure).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
			{
				Model.StoredProcedure associatedObject = (Model.StoredProcedure)frmAssociationWizard.Association.AssociatedObject;

				foreach (Model.StoredProcedure.Parameter parameter in associatedObject.Parameters)
				{
					if (Slyce.Common.Utility.StringsAreEqual(parameter.Alias, alias, false))
					{
						return parameter;
					}
				}
			}
			return null;
		}

		public override bool Save()
		{
			frmAssociationWizard.Association.Mappings.Clear();
			frmAssociationWizard.Association.Enabled = true;

			foreach (DataGridViewRow row in gridMappings.Rows)
			{
				string primaryColumnName = row.Cells[0].Value == null ? "" : row.Cells[0].Value.ToString();
                string associatedColumnName = row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString();
                string associatedParameterName = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();
				primaryColumnName = primaryColumnName.Substring(0, primaryColumnName.IndexOf("(")).Trim();

				if (!string.IsNullOrEmpty(associatedColumnName))
				{
					associatedColumnName = associatedColumnName.Substring(0, associatedColumnName.IndexOf("(")).Trim();
				}
				if (!string.IsNullOrEmpty(associatedParameterName))
				{
					associatedParameterName = associatedParameterName.Substring(1, associatedParameterName.IndexOf("(") - 1).Trim();
				}
				Model.Column primaryColumn = null;
				Model.Column associatedColumn = null;
				Model.StoredProcedure.Parameter associatedParameter = null;

				foreach (Model.Column column in frmAssociationWizard.Association.PrimaryObject.Columns)
				{
					if (Slyce.Common.Utility.StringsAreEqual(column.Alias, primaryColumnName, false))
					{
						primaryColumn = column;
						break;
					}
				}
				foreach (Model.Column column in frmAssociationWizard.Association.AssociatedObject.Columns)
				{
					if (Slyce.Common.Utility.StringsAreEqual(column.Alias, associatedColumnName, false))
					{
						associatedColumn = column;
						break;
					}
				}
				if (typeof(Model.StoredProcedure).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
				{
					foreach (Model.StoredProcedure.Parameter parameter in ((Model.StoredProcedure)frmAssociationWizard.Association.AssociatedObject).Parameters)
					{
						if (Slyce.Common.Utility.StringsAreEqual(parameter.Alias, associatedParameterName, false))
						{
							associatedParameter = parameter;
							break;
						}
					}
				}
				Model.Association.Mapping mapping = new Model.Association.Mapping(primaryColumn, associatedColumn, associatedParameter);
				frmAssociationWizard.Association.Mappings.Add(mapping);
			}
			return true;
		}

		private void comboBoxMapTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			//SetColumnHeading();
			FillMapColumns();
		}

		private void SetColumnHeading()
		{
			if (typeof(Model.StoredProcedure).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
			{
				labelSentence1.Text = string.Format("I want to specify how the columns of {0} map to the columns and parameters of {1}.", frmAssociationWizard.Association.PrimaryObject.Alias, frmAssociationWizard.Association.AssociatedObject.Alias);
                gridMappings.Columns[2].Visible = true;
			}
			else
			{
				labelSentence1.Text = string.Format("I want to specify how the columns of {0} map to the columns of {1}.", frmAssociationWizard.Association.PrimaryObject.Alias, frmAssociationWizard.Association.AssociatedObject.Alias);
                gridMappings.Columns[2].Visible = false;
			}
		}

		private void buttonAutoMap_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			Refresh();
			AutoMap();
			Cursor = Cursors.Default;
		}

        private void gridMappings_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridMappings.BeginEdit(false);

            if (e.ColumnIndex != 0)
            {
                if (gridMappings.EditingControl != null &&
                    gridMappings.EditingControl is ComboBox)
                {
                    ((ComboBox)gridMappings.EditingControl).DroppedDown = true;
                }
            }
        }

	}
}
