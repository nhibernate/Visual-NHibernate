using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace ArchAngel.Designer
{
	public partial class ucUserOptions : UserControl
	{
		private string UserOptionBeingEdited = "";
        //private Type ScriptBaseType = typeof(ArchAngel.Providers.Database.Model.ScriptBase);

		public ucUserOptions()
		{
			InitializeComponent();
            if (Slyce.Common.Utility.InDesignMode) { return; }

            EnableDoubleBuffering();
			ucLabel1.Text = "API Extensions - User Options";
			Populate();
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

		public void Populate()
		{
			lstOptions.Items.Clear();

			foreach (Project.UserOption option in Project.Instance.UserOptions)
			{
				string[] subItems = new string[8];
				subItems[0] = option.VariableName;
				subItems[1] = option.VarType.Name;
				subItems[2] = option.Text;
				subItems[3] = option.Description;

				for (int i = 0; i < option.Values.Length; i++)
				{
					if (i > 0)
					{
						subItems[4] += ", ";
					}
					subItems[4] += option.Values[i];
				}
				subItems[5] = option.Category;

                if (option.DefaultValueIsFunction)
                {
                    subItems[6] = option.DefaultValue + "()";
                }
                else
                {
                    subItems[6] = option.DefaultValue;
                }
				subItems[7] = option.IteratorType == null ? "" : option.IteratorType.Name;

				ListViewItem item = new ListViewItem(subItems);
				lstOptions.Items.Add(item);
			}
		}

        public TreeListNode AddTreeListNode(TreeList treelist, TreeListNode node, Type type, string propertyName, string defaultFunction, string validationFunction)
        {
            TreeListNode newNode = treelist.AppendNode(new object[] { propertyName, defaultFunction, validationFunction }, node);
            //newNode.ImageIndex = newNode.SelectImageIndex = (int)image;
            newNode.Tag = type;
            return newNode;
        }

		private void btnNew_Click(object sender, EventArgs e)
		{
            frmOptionEdit form = new frmOptionEdit("General", null);

            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                string[] subItems = new string[8];
                subItems[0] = form.CurrentOption.VariableName;
                subItems[1] = form.CurrentOption.VarType.Name;
                subItems[2] = form.CurrentOption.Text;
                subItems[3] = form.CurrentOption.Description;

                for (int i = 0; i < form.CurrentOption.Values.Length; i++)
                {
                    if (i > 0)
                    {
                        subItems[4] += ", ";
                    }
                    subItems[4] += form.CurrentOption.Values[i];
                }
                subItems[5] = form.CurrentOption.Category;
                subItems[6] = form.CurrentOption.DefaultValue;
                subItems[7] = form.CurrentOption.IteratorType == null ? "" : form.CurrentOption.IteratorType.Name;

                ListViewItem item = new ListViewItem(subItems);
                lstOptions.Items.Add(item);
                Project.Instance.AddUserOption(form.CurrentOption);
                return;
            }
		}

        public void Clear()
        {
            lstOptions.Items.Clear();
        }

		private void btnRemove_Click(object sender, EventArgs e)
		{
            Delete();
		}

        private void Delete()
        {
            if (lstOptions.SelectedIndices.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete all selected user options?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int firstSelectedIndex = -1;
            firstSelectedIndex = lstOptions.SelectedIndices[0];

            foreach (ListViewItem item in lstOptions.SelectedItems)
            {
                lstOptions.Items.Remove(item);

                for (int i = 0; i < Project.Instance.UserOptions.Length; i++)
                {
                    if (((Project.UserOption)Project.Instance.UserOptions[i]).VariableName == item.SubItems[0].Text)
                    {
                        Project.Instance.RemoveUserOption(item.SubItems[0].Text);
                    }
                }
            }
            if (firstSelectedIndex < lstOptions.Items.Count)
            {
                lstOptions.Items[firstSelectedIndex].Selected = true;
            }
        }

		private void lstOptions_DoubleClick(object sender, EventArgs e)
		{
            PerformEdit();
		}

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PerformEdit();
        }

        private void PerformEdit()
        {
            if (lstOptions.SelectedItems.Count == 0)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            string optionName = lstOptions.SelectedItems[0].SubItems[0].Text;
            int listIndex = lstOptions.SelectedIndices[0];
            Project.UserOption currentOption = null;
            int index = -1;

            for (int i = 0; i < Project.Instance.UserOptions.Length; i++)
            {
                Project.UserOption option = Project.Instance.UserOptions[i];

                if (option.VariableName == optionName)
                {
                    index = i;
                    currentOption = new Project.UserOption(option.VariableName, option.Category, option.VarType, option.Text, option.Description, option.Values, option.DefaultValue, option.IteratorType, option.ValidatorFunction, option.DefaultValueIsFunction, option.DisplayToUser, option.DisplayToUserIsFunction);
                    break;
                }
            }
            frmOptionEdit form = new frmOptionEdit(currentOption);
            Cursor = Cursors.Default;

            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                string[] subItems = new string[9];
                subItems[0] = form.CurrentOption.VariableName;
                subItems[1] = form.CurrentOption.VarType.Name;
                subItems[2] = form.CurrentOption.Text;
                subItems[3] = form.CurrentOption.Description;

                for (int i = 0; i < form.CurrentOption.Values.Length; i++)
                {
                    if (i > 0)
                    {
                        subItems[4] += ", ";
                    }
                    subItems[4] += form.CurrentOption.Values[i];
                }
                subItems[5] = form.CurrentOption.Category;
                subItems[6] = form.CurrentOption.DefaultValue;
                subItems[7] = form.CurrentOption.IteratorType == null ? "" : form.CurrentOption.IteratorType.Name;
                subItems[8] = form.CurrentOption.ValidatorFunction;

                ListViewItem item = new ListViewItem(subItems);
                lstOptions.BeginUpdate();
                lstOptions.Items[lstOptions.SelectedItems[0].Index] = item;
                Project.Instance.UserOptions[index] = form.CurrentOption;
                Project.Instance.IsDirty = true;
                lstOptions.Items[listIndex].Selected = true;
                lstOptions.EndUpdate();
                return;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformEdit();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ucUserOptions_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        private void lstOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformEdit();
            }
        }

        private void treeListAPI_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            bool isFocusedNode = e.Node.TreeList.FocusedNode == e.Node;

            FontStyle fontStyle;

            switch (e.Column.AbsoluteIndex)
            {
                case 0:
                    fontStyle = FontStyle.Regular;
                    break;
                case 1:
                    fontStyle = FontStyle.Regular | FontStyle.Italic;
                    break;
                case 2:
                    fontStyle = FontStyle.Regular | FontStyle.Italic;
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
            e.Appearance.BackColor = Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BackgroundColor, 0.9); //Slyce.Common.Colors.FadingTitleLightColor;// Color.AliceBlue;
            e.Appearance.Font = new Font(e.Appearance.Font, fontStyle);

            if (e.Node.Focused)
            {
                SetFocusedNodeProperties(e);
            }
        }

        private void SetFocusedNodeProperties(GetCustomNodeCellStyleEventArgs e)
        {
            double brightness = Slyce.Common.Colors.GetBrightness(e.Appearance.BackColor);
            double lightBrightness = brightness > 0.5 ? brightness - 0.2 : brightness + 0.1;
            double darkBrightness = brightness > 0.5 ? brightness - 0.6 : brightness - 0.4;

            if (lightBrightness > 1) { lightBrightness = 1; }
            if (darkBrightness < 0) { darkBrightness = 0; }

            Color lightColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, lightBrightness);
            Color darkColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, darkBrightness);

            e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(darkColor);// Color.White;
            e.Appearance.BackColor = lightColor;
            e.Appearance.BackColor2 = darkColor;
            e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            e.Appearance.Options.UseBackColor = true;
        }


	}
}
