using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Designer
{
	public partial class frmConstantsDataEntry : Form
	{
		private string OriginalName = "";
		private bool SaveClicked = false;
		public bool AddingNew = false;

		public frmConstantsDataEntry()
		{
			InitializeComponent();
            EnableDoubleBuffering();
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
		}

        private void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
        }

		private void PositionControls()
		{
			ddlConstantFunctions.Location = txtValue.Location;
			ddlConstantFunctions.Visible = (ddlConstType.Text == "Runtime");
			txtValue.Visible = (ddlConstType.Text != "Runtime");
		}

		private void btnAddConstant_Click(object sender, EventArgs e)
		{
			if (txtConstName.TextLength == 0)
			{
				MessageBox.Show("Name is missing.", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
            bool assignedAtRuntime = (ddlConstType.Text == "Runtime");
            string value = assignedAtRuntime ? ddlConstantFunctions.Text : txtValue.Text;

			if (AddingNew)
			{    
				Project.Constant con = new Project.Constant(txtConstName.Text, ddlConstType.Text, value);//, assignedAtRuntime);
				Project.Instance.AddConstant(con);
			}
			else // Editing existing Constant
			{
				Project.Constant con = Project.Instance.FindConstant(OriginalName);

				con.Name = txtConstName.Text;
				con.DataType = ddlConstType.Text;
				con.Value = con.DataType == "Runtime" ? ddlConstantFunctions.Text : txtValue.Text;
				//con.IsAssignedAtRuntime = con.DataType == "Runtime";
			}
			Project.Instance.IsDirty = true;
			SaveClicked = true;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public bool ShowAddNew()
		{
			AddingNew = true;
			PopulateFunctions();
			PositionControls();
			this.ShowDialog(Controller.MainForm);

			if (SaveClicked)
			{
				Controller.MainForm.PopulateConstantsList();
			}
			return SaveClicked;
		}

		public bool ShowEdit(string name, string typeName, string functionName, string value)
		{
			AddingNew = false;
			OriginalName = name;
			ddlConstType.SelectedIndex = ddlConstType.Items.IndexOf(typeName);
			txtConstName.Text = name;
			ddlConstantFunctions.Text = functionName;
			txtValue.Text = value;
			PopulateFunctions();
			PositionControls();
            this.ShowDialog(Controller.MainForm);
			
			if (SaveClicked)
			{
				Controller.MainForm.PopulateConstantsList();
			}
			return SaveClicked;
		}

		private void PopulateFunctions()
		{
			// Functions for Constants
			ddlConstantFunctions.Items.Clear();
			foreach (Project.FunctionInfo func in Project.Instance.Functions)
			{
				if (func.Parameters.Length == 0)
				{
					ddlConstantFunctions.Items.Add(func.Name + "()");
				}
			}
			ddlConstantFunctions.Items.Add("System.Guid.NewGuid().ToString()");
			ddlConstantFunctions.Items.Add("System.DateTime.Now");
		}

		private void ddlConstType_SelectedIndexChanged(object sender, EventArgs e)
		{
			PositionControls();
		}

        private void frmConstantsDataEntry_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        private void frmConstantsDataEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }
	}
}