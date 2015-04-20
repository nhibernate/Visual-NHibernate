using System;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
	public partial class Screen4 : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
	{
		private readonly Func<FunctionInfo, bool> _templateGenTest;
		private readonly Func<FunctionInfo, bool> _noIteratorTest;
		private readonly Func<FunctionInfo, bool> _staticFileTest;

		public Screen4()
		{
			InitializeComponent();
			this.PageHeader = "Function";
			this.PageDescription = "Select the template function to populate the body of this file.";
			this.HasNext = true;
			this.HasPrev = true;
			this.NextText = "&Finish";
			this.HasFinish = true;

			_templateGenTest = (func => func.IsTemplateFunction && func.Parameters.Count == 1 && func.Parameters[0].DataType == frmOutputFileWizard.IterationType);
			_noIteratorTest = func => func.IsTemplateFunction && func.Parameters.Count == 0;
			_staticFileTest = func => func.IsTemplateFunction == false && func.Parameters.Count == 0 && func.ReturnType.Equals(typeof(bool));
		}

		public override void OnDisplaying()
		{
			PopulateExistingFunctions();
			base.OnDisplaying();
		}

		private void PopulateExistingFunctions()
		{
			Cursor = Cursors.WaitCursor;
			ddlExistingFunctions.Items.Clear();

			// Setup combo box items.
			Func<FunctionInfo, bool> testFunction;
			if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Script)
			{
				optNoSkip.Visible = false;
				if (frmOutputFileWizard.IterationType != null)
				{
					testFunction = _templateGenTest;
					labelFunctionSelectionDescription.Text = "Only functions with a single paramter of the following type are available for selection: " + frmOutputFileWizard.IterationType.FullName;
				}
				else // no iterator
				{
					testFunction = _noIteratorTest;
					labelFunctionSelectionDescription.Text = "Only functions with no paramters are available for selection.";
				}
			}
			else
			{
				testFunction = _staticFileTest;
				optNoSkip.Visible = true;
				labelFunctionSelectionDescription.Text = "Only functions with no paramters are available for selection.";
			}

			foreach (FunctionInfo function in Project.Instance.Functions)
			{
				if (testFunction(function))
				{
					ddlExistingFunctions.Items.Add(function.Name);
				}
			}

			if (ddlExistingFunctions.Items.Count == 0)
			{
				optNewFunction.Checked = true;
				optExistingFunction.Enabled = false;
				optExistingFunction.Text = "Use an existing function: No 'template functions' have this parameter";
				ddlExistingFunctions.Visible = false;
				labelFunctionSelectionDescription.Visible = false;
			}
			else
			{
				optExistingFunction.Enabled = true;
				optExistingFunction.Text = "Use an existing function:";
				ddlExistingFunctions.Visible = true;
				labelFunctionSelectionDescription.Visible = true;
			}

			if (!string.IsNullOrEmpty(frmOutputFileWizard.FunctionName))
			{
				foreach (string funcName in ddlExistingFunctions.Items)
				{
					if (funcName == frmOutputFileWizard.FunctionName)
					{
						ddlExistingFunctions.SelectedItem = funcName;
						break;
					}
				}
			}
			Cursor = Cursors.Default;
		}

		private void ddlExistingFunctions_SelectedIndexChanged(object sender, EventArgs e)
		{
			optExistingFunction.Checked = true;
		}

		public override bool Save()
		{
			foreach (FunctionInfo function in Project.Instance.Functions)
			{
				if (function.Name == ddlExistingFunctions.Text)
				{
					frmOutputFileWizard.FunctionName = function.Name;

					if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Static)
					{
						frmOutputFileWizard.StaticSkipFunction = frmOutputFileWizard.SkipFunctionChoice.UseExisting;
						frmOutputFileWizard.StaticSkipFunctionName = function.Name;
					}
					break;
				}
			}
			frmOutputFileWizard.ShowNewFunctionWizardOnClose = optNewFunction.Checked;

			if (optNewFunction.Checked)
			{
				frmOutputFileWizard.StaticSkipFunction = frmOutputFileWizard.SkipFunctionChoice.CreateNew;

			}


			return true;
		}

	}
}
