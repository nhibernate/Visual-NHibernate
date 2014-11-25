using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Controls.ContentItems
{
    [DotfuscatorDoNotRename]
    public partial class SetupDatabase : Interfaces.Controls.ContentItems.ContentItem
    {
        public static SetupDatabase CurrentSetupDatabase;

        [DotfuscatorDoNotRename]
        private class DatabaseProvider
        {
        	public static string Access
            {
                get { return "Access"; }
            }
        }

        public ListBox ListBoxTablePrefix
        {
            get { return listBoxTablePrefix; }
        }

        public ListBox ListBoxViewPrefix
        {
            get { return listBoxViewPrefix; }
        }

        public ListBox ListBoxStoredProcedurePrefix
        {
            get { return listBoxStoredProcedurePrefix; }
        }

        public SetupDatabase()
        {
            CurrentSetupDatabase = this;
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;
            if (Slyce.Common.Utility.InDesignMode) { return; }
            HasPrev = true;
            HasNext = true;
            HelpPage = "Workbench_Screen_Database_Details.htm";
            Title = "Database Details";
            PageDescription = "Specify what databases you want to use, and what prefixes should be stripped.";
        }

        public override void Clear()
        {
            listBoxTablePrefix.Items.Clear();
            listBoxViewPrefix.Items.Clear();
            listBoxStoredProcedurePrefix.Items.Clear();
            listBoxDatabase.Items.Clear();
        }
        internal void Populate()
        {
            //if (!string.IsNullOrEmpty(ArchAngel.Interfaces.SharedData.ProjectPath))
            //{
                DisplayDatabaseList();
            //}
            // Settings
            DisplayListItems(listBoxTablePrefix, ProviderInfo.TablePrefixes);
            DisplayListItems(listBoxViewPrefix, ProviderInfo.ViewPrefixes);
            DisplayListItems(listBoxStoredProcedurePrefix, ProviderInfo.StoredProcedurePrefixes);
        }

        public override bool Back()
        {
            return true;
        }

        public override bool Next()
        {
            return true;
        }

        private static void DisplayListItems(ListBox listBox, List<string> items)
        {
            listBox.Items.Clear();
            if (items == null)
            {
                return;
            }

            foreach (string prefix in items)
            {
                listBox.Items.Add(prefix);
            }
        }

        private void DisplayDatabaseList()
        {
            // Dont bind as cannot delete
            listBoxDatabase.Items.Clear();
            listBoxDatabase.DisplayMember = "Name";

            //if (ProviderInfo.IsLoaded)
            //{
                if (ProviderInfo.TheBllDatabase.Databases != null)
                {
                    foreach (ArchAngel.Providers.Database.Model.Database database in ProviderInfo.TheBllDatabase.Databases)
                    {
                        listBoxDatabase.Items.Add(database);
                    }
                }
            //}
        }

        private void toolStripMenuItemDatabaseDelete_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)contextMenuStripDatabase.SourceControl;
            ProviderInfo.TheBllDatabase.DeleteDatabase((Model.Database)listBox.SelectedItems[0]);
            listBox.Items.Remove(listBox.SelectedItems[0]);
        }

        private void toolStripMenuItemDeletePrefix_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)contextMenuStripPrefix.SourceControl;
            listBox.Items.Remove(listBox.SelectedItems[0]);

            ProviderInfo.TablePrefixes = GetListItems(listBoxTablePrefix);
            ProviderInfo.ViewPrefixes = GetListItems(listBoxViewPrefix);
            ProviderInfo.StoredProcedurePrefixes = GetListItems(listBoxStoredProcedurePrefix);
        }

        public static List<string> GetListItems(ListBox listBox)
        {
            List<string> items = new List<string>();

            foreach (string prefix in listBox.Items)
            {
                items.Add(prefix);
            }
            return items;
        }

        private void listBoxDatabase_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listBoxDatabase.SelectedItems.Count == 0)
            {
                return;
            }
            contextMenuStripDatabase.Show(listBoxDatabase, point);
        }

        private void listBoxTablePrefix_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listBoxTablePrefix.SelectedItems.Count == 0)
            {
                return;
            }

            contextMenuStripPrefix.Show(listBoxTablePrefix, point);
        }

        private void listBoxViewPrefix_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listBoxViewPrefix.SelectedItems.Count == 0)
            {
                return;
            }

            contextMenuStripPrefix.Show(listBoxViewPrefix, point);
        }

        private void listBoxStoredProcedurePrefix_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            Point point = new Point(e.X, e.Y);

            if (listBoxStoredProcedurePrefix.SelectedItems.Count == 0)
            {
                return;
            }

            contextMenuStripPrefix.Show(listBoxStoredProcedurePrefix, point);
        }

        private void buttonTablePrefixAdd_Click(object sender, EventArgs e)
        {
            AddNewTablePrefix();
        }

        private void buttonViewPrefixAdd_Click(object sender, EventArgs e)
        {
            AddNewViewPrefix();
        }

        private void buttonStoredProcedurePrefixAdd_Click(object sender, EventArgs e)
        {
            AddNewStoredProcPrefix();
        }

        private void listBoxDatabase_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.Clear();

            if (listBoxDatabase.Items.Count == 0)
            {
                errorProvider.SetError(listBoxDatabase, "Must Include Database.");
                e.Cancel = true;
                return;
            }
        }

        private void buttonAddDatabase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Interfaces.SharedData.TemplateFileName))
            {
                MessageBox.Show("Please select a template on the Project Details screen before adding a database.", "No Template", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FormSelectDatabase form = new FormSelectDatabase();

            if (form.ShowAddDatabase(ParentForm))
            {
                Model.Database database = new Model.Database(form.DatabaseName, form.ConnectionString, form.DatabaseType);

                if (ProviderInfo.TheBllDatabase == null)
                {
                    ProviderInfo.TheBllDatabase = new BLL.Database();
                }
                ProviderInfo.TheBllDatabase.AddDatabase(database);
                DisplayDatabaseList();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = listBoxDatabase.SelectedIndices.Count - 1; i >= 0; i--)
            {
                // Find the selected database
                for (int dbCounter = 0; dbCounter < ProviderInfo.TheBllDatabase.Databases.Length; dbCounter++)
                {
                    if (ProviderInfo.TheBllDatabase.Databases[dbCounter].Name == ((Model.Database)listBoxDatabase.SelectedItems[i]).Name)
                    {
                        ProviderInfo.TheBllDatabase.DeleteDatabase(ProviderInfo.TheBllDatabase.Databases[dbCounter]);
                        break;
                    }
                }
                listBoxDatabase.Items.Remove(listBoxDatabase.SelectedItems[i]);
                Interfaces.Events.RaiseIsDirtyEvent();//.IsDirty = true;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxDatabase.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a database from the list.", "No Database Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int foundIndex = -1;

            // Find the selected database
            for (int i = 0; i < ProviderInfo.TheBllDatabase.Databases.Length; i++)
            {
                if (ProviderInfo.TheBllDatabase.Databases[i].Name == ((Model.Database)listBoxDatabase.SelectedItems[0]).Name)
                {
                    foundIndex = i;
                    break;
                }
            }
            if (foundIndex < 0)
            {
                MessageBox.Show("Database not found in internal collection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormSelectDatabase form = new FormSelectDatabase();

            if (form.ShowEditDatabase(ProviderInfo.TheBllDatabase.Databases[foundIndex].ConnectionString))
            {
                Model.Database database = (Model.Database)listBoxDatabase.SelectedItem;
                database.Name = form.DatabaseName;
                database.DatabaseType = form.DatabaseType;
                database.ConnectionString = form.ConnectionString;

                ProviderInfo.TheBllDatabase.UpdateDatabase(database, listBoxDatabase.SelectedIndex);
                DisplayDatabaseList();
            }
        }

        private void buttonTablePrefixDelete_Click(object sender, EventArgs e)
        {
            DeletePrefixes(listBoxTablePrefix);
        }

        private void DeletePrefixes(ListBox listbox)
        {
            int firstSelectedIndex = listbox.SelectedIndices.Count > 0 ? listbox.SelectedIndices[0] : -1;

            for (int i = listbox.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listbox.Items.RemoveAt(listbox.SelectedIndices[i]);
            }
            ProviderInfo.TablePrefixes = GetListItems(listBoxTablePrefix);
            ProviderInfo.ViewPrefixes = GetListItems(listBoxViewPrefix);
            ProviderInfo.StoredProcedurePrefixes = GetListItems(listBoxStoredProcedurePrefix);

            if (listbox.Items.Count >= 0 && firstSelectedIndex >= 0)
            {
                listbox.SelectedIndex = Math.Min(listbox.Items.Count - 1, firstSelectedIndex);
            }
            Interfaces.Events.RaiseIsDirtyEvent();
        }

        private void buttonViewPrefixDelete_Click(object sender, EventArgs e)
        {
            DeletePrefixes(listBoxViewPrefix);
        }

        private void buttonStoredProcedurePrefixDelete_Click(object sender, EventArgs e)
        {
            DeletePrefixes(ListBoxStoredProcedurePrefix);
        }

        private void textBoxTablePrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddNewTablePrefix();
            }
        }

        private void AddNewTablePrefix()
        {
            if (textBoxTablePrefix.Text == "")
            {
                return;
            }

            if (listBoxTablePrefix.Items.IndexOf(textBoxTablePrefix.Text) > -1)
            {
                return;
            }

            listBoxTablePrefix.Items.Add(textBoxTablePrefix.Text);
            textBoxTablePrefix.Clear();

            ProviderInfo.TablePrefixes = GetListItems(listBoxTablePrefix);
            Interfaces.Events.RaiseIsDirtyEvent();
        }

        private void textBoxViewPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddNewViewPrefix();
            }
        }

        private void AddNewViewPrefix()
        {
            if (textBoxViewPrefix.Text == "")
            {
                return;
            }

            if (listBoxViewPrefix.Items.IndexOf(textBoxViewPrefix.Text) > -1)
            {
                return;
            }

            listBoxViewPrefix.Items.Add(textBoxViewPrefix.Text);
            textBoxViewPrefix.Clear();

            ProviderInfo.ViewPrefixes = GetListItems(listBoxViewPrefix);
            Interfaces.Events.RaiseIsDirtyEvent();
        }

        private void textBoxStoredProcedurePrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddNewStoredProcPrefix();
            }
        }

        private void AddNewStoredProcPrefix()
        {
            if (textBoxStoredProcedurePrefix.Text == "")
            {
                return;
            }

            if (listBoxStoredProcedurePrefix.Items.IndexOf(textBoxStoredProcedurePrefix.Text) > -1)
            {
                return;
            }

            listBoxStoredProcedurePrefix.Items.Add(textBoxStoredProcedurePrefix.Text);
            textBoxStoredProcedurePrefix.Clear();

            ProviderInfo.StoredProcedurePrefixes = GetListItems(listBoxStoredProcedurePrefix);
            Interfaces.Events.RaiseIsDirtyEvent();
        }

        private void listBoxTablePrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletePrefixes(listBoxTablePrefix);
            }
        }

        private void listBoxViewPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletePrefixes(listBoxViewPrefix);
            }
        }

        private void listBoxStoredProcedurePrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletePrefixes(ListBoxStoredProcedurePrefix);
            }
        }

    }
}