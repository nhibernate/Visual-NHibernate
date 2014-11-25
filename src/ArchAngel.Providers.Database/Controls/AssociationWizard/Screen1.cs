using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Controls.AssociationWizard
{
    public partial class Screen1 : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        private string[] _FilterParts = new string[0];

        public Screen1()
        {
            InitializeComponent();
            HasPrev = false;
            HasNext = true;
            PageHeader = "Select Association Object";
            PageDescription = "Select what object you want to associate with.";
            labelSentence1.Margin = labelSentence2.Margin = labelSentence3.Margin = labelSentence4.Margin = new Padding(labelSentence1.Margin.Left, comboBoxActionTypes.Height / 2, labelSentence1.Margin.Right, labelSentence1.Margin.Bottom);
            Populate();
        }

        private void Populate()
        {
            txtName.Text = frmAssociationWizard.Association.Name;
            FillActionTypes();
            FillObjectTypes();
            FillObjects();
            txtFilter.Text = frmAssociationWizard.Association.AssociatedObject == null ? "" : frmAssociationWizard.Association.AssociatedObject.Alias;
        }

        private void FillActionTypes()
        {
            comboBoxActionTypes.Items.Clear();
            ArchAngel.Interfaces.Setting setting = ProviderInfo.Settings[ProviderInfo.SettingNames.AssociationActions.ToString()];
            List<string> values = (List<string>)setting.Value;

            foreach (string value in values)
            {
                comboBoxActionTypes.Items.Add(value);
            }
            if (values.Count > 0)
            {
                comboBoxActionTypes.Text = values[0];
            }
        }

        private void FillObjectTypes()
        {
            comboBoxObjectTypes.Items.Clear();
            comboBoxObjectTypes.Items.AddRange(new string[] { "Table", "View", "Stored Procedure" });
            comboBoxObjectTypes.SelectedIndex = 1;
        }

        private void FillObjects()
        {
            //lblTreeviewHeading.Text = string.Format("Select the {0} you want to associate with:", comboBoxObjectTypes.Text);
            gridListObjects.Rows.Clear();

            if (string.IsNullOrEmpty(comboBoxObjectTypes.Text))
            {
                return;
            }
            if (ProviderInfo.TheBllDatabase != null)
            {
                foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
                {
                    switch (comboBoxObjectTypes.Text)
                    {
                        case "Table":
                            foreach (Model.Table table in database.Tables)
                            {
                                AddGridRow(table);
                            }
                            break;
                        case "View":
                            foreach (Model.View view in database.Views)
                            {
                                AddGridRow(view);
                            }
                            break;
                        case "Stored Procedure":
                            foreach (Model.StoredProcedure storedProcedure in database.StoredProcedures)
                            {
                                AddGridRow(storedProcedure);
                            }
                            break;
                        default:
                            throw new NotImplementedException("This type has not been catered for yet.");
                    }
                }
            }
            if (gridListObjects.Rows.Count > 0)
            {
                gridListObjects.Rows[0].Selected = true;
            }
        }

        private bool IsFilteredOut(string name)
        {
            int startPos = 0;
            name = name.ToLower();

            for (int i = 0; i < FilterParts.Length; i++)
            {
                int index = name.IndexOf(FilterParts[i], startPos);

                if (index < 0)
                {
                    return true;
                }
                if (i == 0 && index > 0)
                {
                    return true;
                }
                startPos = index + FilterParts[i].Length;
            }
            return false;
        }

        private string[] FilterParts
        {
            get { return _FilterParts; }
            set { _FilterParts = value; }
        }

        private void AddGridRow(Model.ScriptObject scriptObject)
        {
            if (!scriptObject.Enabled) { return; }

            bool aliasIsDifferent = !Slyce.Common.Utility.StringsAreEqual(scriptObject.Alias, scriptObject.Name, false);

            if (!IsFilteredOut(scriptObject.Alias) || (aliasIsDifferent && !IsFilteredOut(scriptObject.Name)))
            {
                string text;

                if (!aliasIsDifferent)
                {
                    text = scriptObject.Alias;
                }
                else
                {
                    text = string.Format("{0} [{1}]", scriptObject.Alias, scriptObject.Name);
                }
                int newIndex = gridListObjects.Rows.Add(new object[] { text });
                gridListObjects.Rows[newIndex].Tag = scriptObject;
            }
        }

        private void comboBoxObjectTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObjects();
        }

        public override bool Next()
        {
            if (gridListObjects.SelectedRows.Count == 0 ||
                gridListObjects.SelectedRows[0].Tag == null)
            {
                MessageBox.Show("No object has been selected.", "Missing object", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtName.Text == "NewAssociation")
            {
                MessageBox.Show("A name hasn't been assigned yet. 'NewAssociation' is not a valid name.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                txtName.SelectAll();
                return false;
            }
            frmAssociationWizard.Association.AssociatedObject = (Model.ScriptObject)gridListObjects.SelectedRows[0].Tag;
            frmAssociationWizard.Association.AssociationKind = comboBoxActionTypes.Text;
            frmAssociationWizard.Association.Name = txtName.Text;
            return true;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterParts = txtFilter.Text.ToLower().Split('*');
            FillObjects();
        }

        public override void OnDisplaying()
        {
            comboBoxActionTypes.Text = frmAssociationWizard.Association.AssociationKind;
            txtName.Text = frmAssociationWizard.Association.Name;

            if (typeof(Model.StoredProcedure).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
            {
                comboBoxObjectTypes.Text = "Stored Procedure";
            }
            else if (typeof(Model.Table).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
            {
                comboBoxObjectTypes.Text = "Table";
            }
            else if (typeof(Model.View).IsInstanceOfType(frmAssociationWizard.Association.AssociatedObject))
            {
                comboBoxObjectTypes.Text = "View";
            }
            if (frmAssociationWizard.Association.AssociatedObject != null)
            {
                string text = frmAssociationWizard.Association.AssociatedObject.Name;

                foreach (DataGridViewRow row in gridListObjects.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        Slyce.Common.Utility.StringsAreEqual(row.Cells[0].Value.ToString(), text, false))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }
    }
}
