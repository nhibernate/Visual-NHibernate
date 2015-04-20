using System;
using System.Windows.Forms;

namespace Demo.Providers.Test.Screens
{
    public partial class Screen1 : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        public Screen1()
        {
            InitializeComponent();
            HelpFile = "My Provider.chm"; // Your .CHM help file, which must exist in the ArchAngel installation folder
            HelpPage = "Screen1.htm"; // The page in your help file that corresponds to this page
            Title = "Pupil Allocation"; // This is displayed in the navigation pane, and at the top of the screen with 'Step x - ' before it
            PageDescription = "Allocation of pupils to schools.";
        }

        /// <summary>
        /// Gets called when the user clicks the 'Next' button. Return false to cancel the action 
        /// and prevent the previous screen from being loaded.
        /// </summary>
        /// <returns></returns>
        public override bool Next()
        {
            return true;
        }

        /// <summary>
        /// Gets called when the user clicks the 'Back' button. Return false to cancel the action 
        /// and prevent the previous screen from being loaded.
        /// </summary>
        /// <returns></returns>
		public override bool Back()
        {
            return true;
        }

        internal void Populate()
        {
            if (ProviderInfo.Schools != null)
            {
                foreach (School school in ProviderInfo.Schools)
                {
                    ListViewItem item = new ListViewItem(school.Name);
                    item.Tag = school;
                    listSchools.Items.Add(item);
                }
            }
        }

        private void listSchools_SelectedIndexChanged(object sender, EventArgs e)
        {
        	if (listSchools.SelectedItems.Count <= 0) return;

        	listPupils.Items.Clear();
        	School school = (School)listSchools.SelectedItems[0].Tag;

        	foreach (Pupil pupil in school.Pupils)
        	{
        		ListViewItem item = new ListViewItem(pupil.Name);
        		item.Tag = pupil;
        		listPupils.Items.Add(item);
        	}
        }

        private void btnAddSchool_Click(object sender, EventArgs e)
        {
            School school = new School();
            school.Name = txtSchool.Text;
            ProviderInfo.Schools.Add(school);
            ListViewItem item = new ListViewItem(school.Name);
            item.Tag = school;
            listSchools.Items.Add(item);
            txtSchool.Clear();
            txtSchool.Focus();
        }

        private void btnDeleteSchool_Click(object sender, EventArgs e)
        {
            if (listSchools.SelectedItems.Count > 0)
            {
                School school = (School)listSchools.SelectedItems[0].Tag;
                ProviderInfo.Schools.Remove(school);

                listSchools.Items.Remove(listSchools.SelectedItems[0]);
            }
        }

        private void btnAddPupil_Click(object sender, EventArgs e)
        {
            if (listSchools.SelectedItems.Count == 0)
            {
                MessageBox.Show("No school selected.");
                return;
            }
            Pupil pupil = new Pupil();
            pupil.Name = txtPupil.Text;
            School school = (School)listSchools.SelectedItems[0].Tag;
            school.Pupils.Add(pupil);

            ListViewItem item = new ListViewItem(pupil.Name);
            item.Tag = pupil;
            listPupils.Items.Add(item);

            txtPupil.Clear();
            txtPupil.Focus();
        }

        private void btnDeletePupil_Click(object sender, EventArgs e)
        {
            if (listPupils.SelectedItems.Count > 0)
            {
                Pupil pupil = (Pupil)listPupils.SelectedItems[0].Tag;
                School school = (School)listSchools.SelectedItems[0].Tag;

                //school.Pupils.Remove(pupil);
                listPupils.Items.Remove(listPupils.SelectedItems[0]);

                school.Pupils.Remove(pupil);
            }
        }

    }
}
