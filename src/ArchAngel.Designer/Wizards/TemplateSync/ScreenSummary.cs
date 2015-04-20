using System;
using System.Windows.Forms;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
	public partial class ScreenSummary : Interfaces.Controls.ContentItems.ContentItem
	{
		public ScreenSummary()
		{
			InitializeComponent();
			PageHeader = "Summary";
			PageDescription = "Summary of changes.";
			HasNext = true;
			HasPrev = true;
			NextText = "Save";
			HasFinish = true;
		}

		public override bool Next()
		{
			var dialog = new SaveFileDialog
			                 {
			                     InitialDirectory = Environment.CurrentDirectory,
			                     DefaultExt = ".stz",
			                     Filter = "ArchAngel Template (*.stz)|*.stz"
			                 };

		    if (dialog.ShowDialog() == DialogResult.OK)
			{
				frmTemplateSyncWizard.MyProject.SaveToXml(dialog.FileName);
				return true;
			}
			return false;
		}
	}
}
