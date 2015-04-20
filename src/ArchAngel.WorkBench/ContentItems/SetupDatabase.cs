using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.BLL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Workbench.ContentItems
{
    [DoNotObfuscate]
    public partial class SetupDatabase : ContentItem
    {
        [DoNotObfuscate]
        private class DatabaseProvider
        {
            public static string SQLServerDmo
            {
                get { return "SQL Server - DMO"; }
            }

            public static string SQLServerSmo
            {
                get { return "SQL Server - SMO"; }
            }

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
            Populate();
            Project.OnProjectLoaded += new Project.ProjectLoadedDelegate(Project_OnProjectLoaded);
        }

        public SetupDatabase(string title, string pageHeader, string pageDescription)
            : base(title, pageHeader, pageDescription)
        {
            Populate();
            Project.OnProjectLoaded += new Project.ProjectLoadedDelegate(Project_OnProjectLoaded);
        }

        void Project_OnProjectLoaded()
        {
            DisplayDatabaseList();

            DisplayListItems(listBoxTablePrefix, Controller.Instance.AppConfig.TablePrefixes);
            DisplayListItems(listBoxViewPrefix, Controller.Instance.AppConfig.ViewPrefixes);
            DisplayListItems(listBoxStoredProcedurePrefix, Controller.Instance.AppConfig.StoredProcedurePrefixes);
        }

        private void Populate()
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            if (Slyce.Common.Utility.InDesignMode) { return; }
            HasPrev = true;
            HasNext = true;
            HelpFileName = "TaskDatabaseSetup.htm";

            if (!string.IsNullOrEmpty(Controller.Instance.AppConfig.ProjectPath))
            {
                DisplayDatabaseList();
            }
            // Settings

            DisplayListItems(listBoxTablePrefix, Controller.Instance.AppConfig.TablePrefixes);
            DisplayListItems(listBoxViewPrefix, Controller.Instance.AppConfig.ViewPrefixes);
            DisplayListItems(listBoxStoredProcedurePrefix, Controller.Instance.AppConfig.StoredProcedurePrefixes);

            //DisplayDatabaseList();
        }

        public override bool Back()
        {
            return true;//ContentItemNames.SetupProject.ToString();
        }

        public override bool Next()
        {
            return true;// ContentItemNames.EditModel.ToString();
        }

        private void DisplayListItems(ListBox listBox, string[] items)
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

            if (Controller.Instance.BllDatabase.Databases != null)
            {
                foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
                {
                    listBoxDatabase.Items.Add(database);
                }
            }
        }

        private void toolStripMenuItemDatabaseDelete_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)contextMenuStripDatabase.SourceControl;
            Controller.Instance.BllDatabase.DeleteDatabase((ArchAngel.Providers.Database.Model.Database)listBox.SelectedItems[0]);
            listBox.Items.Remove(listBox.SelectedItems[0]);
        }

        private void toolStripMenuItemDeletePrefix_Click(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)contextMenuStripPrefix.SourceControl;
            listBox.Items.Remove(listBox.SelectedItems[0]);

            Controller.Instance.AppConfig.TablePrefixes = GetListItems(listBoxTablePrefix);
            Controller.Instance.AppConfig.ViewPrefixes = GetListItems(listBoxViewPrefix);
            Controller.Instance.AppConfig.StoredProcedurePrefixes = GetListItems(listBoxStoredProcedurePrefix);
        }

        public string[] GetListItems(ListBox listBox)
        {
            List<string> items = new List<string>();
            foreach (string prefix in listBox.Items)
            {
                items.Add(prefix);
            }

            return (string[])items.ToArray();
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

            Controller.Instance.AppConfig.TablePrefixes = GetListItems(listBoxTablePrefix);
        }

        private void buttonViewPrefixAdd_Click(object sender, EventArgs e)
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

            Controller.Instance.AppConfig.ViewPrefixes = GetListItems(listBoxViewPrefix);
        }

        private void buttonStoredProcedurePrefixAdd_Click(object sender, EventArgs e)
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
            textBoxViewPrefix.Clear();

            Controller.Instance.AppConfig.StoredProcedurePrefixes = GetListItems(listBoxStoredProcedurePrefix);
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
            if (string.IsNullOrEmpty(Controller.Instance.AppConfig.TemplateFileName))
            {
                MessageBox.Show("Please select a template on the Project Details screen before adding a database.", "No Template", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FormSelectDatabase form = new FormSelectDatabase();

            if (form.ShowAddDatabase())
            {
                ArchAngel.Providers.Database.Model.Database database = new ArchAngel.Providers.Database.Model.Database(form.DatabaseName, form.ConnectionString, form.DatabaseType);
                Controller.Instance.BllDatabase.AddDatabase(database);
                DisplayDatabaseList();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = listBoxDatabase.SelectedIndices.Count - 1; i >= 0; i--)
            {
                // Find the selected database
                for (int dbCounter = 0; dbCounter < Controller.Instance.BllDatabase.Databases.Length; dbCounter++)
                {
                    if (Controller.Instance.BllDatabase.Databases[dbCounter].Name == ((ArchAngel.Providers.Database.Model.Database)listBoxDatabase.SelectedItems[i]).Name)
                    {
                        Controller.Instance.BllDatabase.DeleteDatabase(Controller.Instance.BllDatabase.Databases[dbCounter]);
                        break;
                    }
                }
                listBoxDatabase.Items.Remove(listBoxDatabase.SelectedItems[i]);
                Controller.IsDirty = true;
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
            for (int i = 0; i < Controller.Instance.BllDatabase.Databases.Length; i++)
            {
                if (Controller.Instance.BllDatabase.Databases[i].Name == ((ArchAngel.Providers.Database.Model.Database)listBoxDatabase.SelectedItems[0]).Name)
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

            if (form.ShowEditDatabase(Controller.Instance.BllDatabase.Databases[foundIndex].ConnectionString))
            {
                ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)listBoxDatabase.SelectedItem;
                database.Name = form.DatabaseName;
                database.DatabaseType = form.DatabaseType;
                database.ConnectionString = form.ConnectionString;
                
                Controller.Instance.BllDatabase.UpdateDatabase(database, listBoxDatabase.SelectedIndex);
            }
        }

        private void buttonTablePrefixDelete_Click(object sender, EventArgs e)
        {
            DeletePrefixes(listBoxTablePrefix);
        }

        private void DeletePrefixes(ListBox listbox)
        {
            for (int i = listbox.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listbox.Items.RemoveAt(listbox.SelectedIndices[i]);
            }
            Controller.Instance.AppConfig.TablePrefixes = GetListItems(listBoxTablePrefix);
            Controller.Instance.AppConfig.ViewPrefixes = GetListItems(listBoxViewPrefix);
            Controller.Instance.AppConfig.StoredProcedurePrefixes = GetListItems(listBoxStoredProcedurePrefix);
        }

        private void buttonViewPrefixDelete_Click(object sender, EventArgs e)
        {
            DeletePrefixes(listBoxViewPrefix);
        }

        private void buttonStoredProcedurePrefixDelete_Click(object sender, EventArgs e)
        {
            DeletePrefixes(ListBoxStoredProcedurePrefix);
        }

    }
}