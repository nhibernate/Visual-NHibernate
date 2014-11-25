using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;
using DevComponents.AdvTree;

namespace ArchAngel.Designer
{
	public partial class frmCleanUp : Form
	{
		private class FunctionSpec
		{
			public FunctionSpec(string name)
			{
				Name = name;
				Parameters = new List<ParamInfo>();
			}

			public string Name { get; set; }
			public List<ParamInfo> Parameters { get; set; }
		}

		public frmCleanUp()
		{
			Controller.ShadeMainForm();
			Controller.Instance.MainForm.Refresh();
			InitializeComponent();
			ucHeading1.Text = "";
			Populate();
		}

		private void frmCleanUp_FormClosing(object sender, FormClosingEventArgs e)
		{
			Controller.UnshadeMainForm();
		}

		private void Populate()
		{
			PopulateFunctions();
			PopulateUserOptions();
		}

		private void PopulateFunctions()
		{
			List<string> specialFunctions = Project.Instance.InternalFunctionNames;

			treeListFunctions.BeginUpdate();

			foreach (FunctionInfo function in Project.Instance.Functions)
			{
				if (specialFunctions.BinarySearch(function.Name) < 0 &&
					Project.Instance.FindFilesUsingFunction(function.Name).Count == 0 &&
					Project.Instance.FindFunctionsUsing(function.Name, false).Count == 0)
				{
					StringBuilder sbParamTypes = new StringBuilder(100);

					foreach (var param in function.Parameters)
					{
						sbParamTypes.Append(param.DataType.FullName + ", ");
					}
					string displayName = string.Format("{0}({1})", function.Name, sbParamTypes.ToString().TrimEnd(',', ' '));

					Node node = new Node();
					node.Text = displayName;
					node.Tag = function;
					node.CheckBoxVisible = true;

					treeListFunctions.Nodes.Add(node);
				}
			}
			treeListFunctions.BeginUpdate();
		}

		private void PopulateUserOptions()
		{
			treeListUserOptions.BeginUpdate();

			foreach (UserOption userOption in Project.Instance.UserOptions)
			{
				if (Project.Instance.FindFunctionsUsing(userOption.VariableName, false).Count == 0)
				{
					if (!Project.Instance.UserOptionIsUsedInFilenames(userOption))
					{
						Node node = new Node();
						node.CheckBoxVisible = true;
						node.Text = userOption.VariableName;
						node.Tag = userOption;
						treeListUserOptions.Nodes.Add(node);
					}
				}
			}
			treeListUserOptions.EndUpdate(true);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			Refresh();

			foreach (Node node in treeListFunctions.Nodes)
			{
				if (node.Checked == false || node.Tag is FunctionInfo == false) continue;

				FunctionInfo func = node.Tag as FunctionInfo;

				Project.Instance.DeleteFunction(func, false);
			}

			foreach (Node node in treeListUserOptions.Nodes)
			{
				if (node.Checked && node.Tag is UserOption)
					Project.Instance.DeleteUserOption((node.Tag as UserOption).VariableName);
			}


			Controller.Instance.MainForm.UcFunctions.PopulateFunctionList();
			Controller.Instance.MainForm.UcOptions.Populate();
			Close();
			Cursor = Cursors.Default;
		}
	}
}
