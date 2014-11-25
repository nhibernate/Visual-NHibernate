using System;
using System.Windows.Forms;

namespace Slyce.IntelliMerge.UI
{
	public partial class ucMergeErrorEditor : UserControl
	{
		public ucMergeErrorEditor()
		{
			InitializeComponent();
		}

		public ucMergeErrorEditor(string errorDescription, string baseConstructName, string baseConstructType) : this()
		{
			textBox1.Text =
				string.Format("There was an error merging your files that should be reported to Slyce as a defect. Please email " 
				+ "the text in this box to support@Slyce.com with the subject \"Merge Error\". {0}" 
				+ "---------------------------------------------------------"
				+ "{0}{0}Base Construct Type: {0}{1}{0}{0}Base Construct Name: {0}{2}{0}{0}Error Description: {0}{3}{0}{0}"
				+ "---------------------------------------------------------",
				Environment.NewLine, baseConstructType, baseConstructName, errorDescription);
			textBox1.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 16.0f);
		}
	}
}
