using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.Wizards;
using DevComponents.DotNetBar;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class FormConvertKeyToComponent : Form
	{
		private readonly IConvertKeyToComponentModelInformation _modelInformation;
		private readonly WizardPageDefinition pageTransitions = new WizardPageDefinition();
		private readonly Dictionary<Page, Action> onPageLoadActions = new Dictionary<Page, Action>();
		private readonly Dictionary<Page, WizardPage> pages = new Dictionary<Page, WizardPage>();

		private IConvertKeyToComponentModelResults _results;

		public FormConvertKeyToComponent(IConvertKeyToComponentModelInformation modelInformation)
		{
			InitializeComponent();

			_modelInformation = modelInformation;

			pageTransitions.AddPageTransition(Page.CreateNewOrChooseExistingPage, CreateNewOrChooseExistingPageTransition);
			
			pageTransitions.AddPageTransition(Page.CreateNewPage, () => Page.Finish);

			pageTransitions.AddPageTransition(Page.ChooseExistingPage, () => Page.MapExistingPage);
			pageTransitions.AddPageTransition(Page.MapExistingPage, () => Page.Finish);

			onPageLoadActions.Add(Page.CreateNewPage, CreateNewOnPageLoad);
			onPageLoadActions.Add(Page.ChooseExistingPage, ChooseExistingOnPageLoad);
			onPageLoadActions.Add(Page.MapExistingPage, MapExistingOnPageLoad);
			onPageLoadActions.Add(Page.Finish, FinishOnPageLoad);

			SetupWizardPage(wizardPageCreateNewOrChooseExisting, Page.CreateNewOrChooseExistingPage);
			SetupWizardPage(wizardPageCreateNew, Page.CreateNewPage);
			SetupWizardPage(wizardPageChooseExisting, Page.ChooseExistingPage);
			SetupWizardPage(wizardPageMapExisting, Page.MapExistingPage);
			SetupWizardPage(wizardPageFinish, Page.Finish);

			// Validation event handlers
			// ChooseExistingPage
			comboBoxExistingComponentDefs.SelectedIndexChanged += (sender, e) => wizardPageChooseExisting.NextButtonEnabled = ToWizardButtonState(ChooseExistingValidation());
			// CreateNewPage
			textBoxNewComponentDefName.TextChanged += (s, e) => wizardPageCreateNew.NextButtonEnabled = ToWizardButtonState(CreateNewValidation());
			textBoxNewComponentName.TextChanged += (s, e) => wizardPageCreateNew.NextButtonEnabled = ToWizardButtonState(CreateNewValidation());
			// MapExistingPage
			textBoxUseExistingNameOfComponent.TextChanged += (s, e) => wizardPageMapExisting.NextButtonEnabled = ToWizardButtonState(MapExistingValidation());
			dataGridViewPropertyMappings.CellEndEdit += dataGridViewPropertyMappings_CellEndEdit;
		}

		void dataGridViewPropertyMappings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			lastResultOfMappingValidation = null;
			wizardPageMapExisting.NextButtonEnabled = ToWizardButtonState(MapExistingValidation());
		}

		// This variable is used so that we only validate the mappings if one of the them has changed.
		private bool? lastResultOfMappingValidation;
		private bool MapExistingValidation()
		{
			if(string.IsNullOrEmpty(textBoxUseExistingNameOfComponent.Text))
				return false;

			if (lastResultOfMappingValidation.HasValue)
			{
				return lastResultOfMappingValidation.Value;
			}

			foreach (DataGridViewRow row in dataGridViewPropertyMappings.Rows)
			{
				var value = (string) row.Cells[1].Value;
				if (string.IsNullOrEmpty(value))
				{
					lastResultOfMappingValidation = false; return false;
				}
			}
			lastResultOfMappingValidation = true;
			return true;
		}

		private bool ChooseExistingValidation()
		{
			return 
				comboBoxExistingComponentDefs.GetSelectedItem<ComponentSpecification>() != null;
		}

		private bool CreateNewValidation()
		{
			return !string.IsNullOrEmpty(textBoxNewComponentDefName.Text)
			    && !string.IsNullOrEmpty(textBoxNewComponentName.Text);
		}

		private void FinishOnPageLoad()
		{
			if(radioButtonCreateNewComponent.Checked)
			{
				// Create the result object
				_results = new ConvertKeyToNewComponentResults(
					textBoxNewComponentDefName.Text,
					textBoxNewComponentName.Text,
					checkBoxCreateNewCompDeleteProperties.Checked);
			}
			else
			{
				_results = new ConvertKeyToExistingComponentResults(
					comboBoxExistingComponentDefs.GetSelectedItem<ComponentSpecification>(),
					textBoxUseExistingNameOfComponent.Text,
					checkBoxUseExistingDeleteExistingProperties.Checked,
					GetPropertyMappings());
			}

			labelFinishedChanges.Text = _results.GetTextualDescription();
		}


		private void CreateNewOnPageLoad()
		{
			wizardPageCreateNew.NextButtonEnabled = ToWizardButtonState(CreateNewValidation());
		}

		private IEnumerable<KeyValuePair<string, string>> GetPropertyMappings()
		{
			foreach(DataGridViewRow row in dataGridViewPropertyMappings.Rows)
			{
				string newPropertyName = (string) row.Cells[0].Value;
				string oldPropertyname = (string) row.Cells[1].Value;

				yield return new KeyValuePair<string, string>(newPropertyName, oldPropertyname);
			}
		}

		private ComponentSpecification _loadedComponentSpec;
		private void MapExistingOnPageLoad()
		{
			wizardPageMapExisting.NextButtonEnabled = ToWizardButtonState(MapExistingValidation());

			var currentComponentSpec = comboBoxExistingComponentDefs.GetSelectedItem<ComponentSpecification>();
			if (currentComponentSpec != _loadedComponentSpec || _loadedComponentSpec == null)
			{
				_loadedComponentSpec = currentComponentSpec;

				// Clear the current mappings
				dataGridViewPropertyMappings.Rows.Clear();

				// Add all the mappings
				var mappings = _modelInformation.AutoMapComponentAndKey(currentComponentSpec);
				var possibleProperties = _modelInformation.GetKeyProperties();
				foreach(var mapping in mappings)
				{
					DataGridViewRow row = new DataGridViewRow();
					row.CreateCells(dataGridViewPropertyMappings);
					row.Cells[0].Value = mapping.Key;

					DataGridViewComboBoxCell column = (DataGridViewComboBoxCell) row.Cells[1];
					column.Items.Add("");
					foreach(var item in possibleProperties)
						column.Items.Add(item);

					column.Value = mapping.Value;

					dataGridViewPropertyMappings.Rows.Add(row);
				}
			}
		}

		private void ChooseExistingOnPageLoad()
		{
			wizardPageChooseExisting.NextButtonEnabled = ToWizardButtonState(ChooseExistingValidation());

			var selectedComponent = comboBoxExistingComponentDefs.GetSelectedItem<ComponentSpecification>();

			// Fill Component Spec combo box
			comboBoxExistingComponentDefs.Items.Clear();

			comboBoxExistingComponentDefs.Fill(_modelInformation.GetComponentSpecifications(), cs => cs.Name);

			if(selectedComponent != null)
			{
				comboBoxExistingComponentDefs.SetSelectedItem(selectedComponent);
			}
		}

		private void SetupWizardPage(WizardPage wizardPage, Page page)
		{
			wizardPage.Tag = page;
			pages.Add(page, wizardPage);
		}

		private Page CreateNewOrChooseExistingPageTransition()
		{
			if (radioButtonCreateNewComponent.Checked)
				return Page.CreateNewPage;
			
			return Page.ChooseExistingPage;
		}

		private static Page GetPageFromWizardPage(WizardPage page)
		{
			return (Page) page.Tag;
		}

		private WizardPage GetWizardPageFromPage(Page page)
		{
			return pages[page];
		}

		private void wizard1_WizardPageChanging(object sender, WizardCancelPageChangeEventArgs e)
		{
			if(e.PageChangeSource == eWizardPageChangeSource.BackButton || e.PageChangeSource == eWizardPageChangeSource.Code)
			{
				return;
			}
			
			Page currentPage = GetPageFromWizardPage(e.OldPage);

			if (currentPage == Page.Finish) return;

			Page newPage = pageTransitions.GetNextPage(currentPage);
			e.NewPage = GetWizardPageFromPage(newPage);
			if(onPageLoadActions.ContainsKey(newPage))
			{
				onPageLoadActions[newPage]();
			}
		}

		private enum Page { CreateNewOrChooseExistingPage, CreateNewPage, ChooseExistingPage, MapExistingPage, Finish }

		private class WizardPageDefinition
		{
			private readonly Dictionary<Page, Func<Page>> ChangePageFunctions = new Dictionary<Page, Func<Page>>();

			public void AddPageTransition(Page fromPage, Func<Page> transitionFunction)
			{
				ChangePageFunctions.Add(fromPage, transitionFunction);
			}

			public Page GetNextPage(Page fromPage)
			{
				return ChangePageFunctions[fromPage]();
			}
		}

		private eWizardButtonState ToWizardButtonState(bool b)
		{
			return b ? eWizardButtonState.True : eWizardButtonState.False;
		}

		public IConvertKeyToComponentModelResults GetConversionResult()
		{
			return _results;
		}

		private void wizard1_FinishButtonClick(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void wizard1_CancelButtonClick(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
