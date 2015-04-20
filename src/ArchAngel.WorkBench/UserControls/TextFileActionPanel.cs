using System.Windows.Forms;
using ArchAngel.Workbench.ContentItems;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Workbench.UserControls
{
	public partial class TextFileActionPanel : UserControl
	{
		private readonly Output output;
		private IntelliMergeType selectedItem = IntelliMergeType.NotSet;

		public TextFileActionPanel()
		{
			InitializeComponent();
		}

		public TextFileActionPanel(Output output)
		{
			this.output = output;
			InitializeComponent();
		}

		private void SetIntelliMerge(IntelliMergeType intellimerge)
		{
			if (DesignMode)
				return;
			if (output == null)
				return;
			selectedItem = intellimerge;
			output.SetIntelliMergeOnAllSelectedNodes(this, selectedItem);
		}

		private void buttonOverwrite_Click(object sender, System.EventArgs e)
		{
			SetIntelliMerge(IntelliMergeType.Overwrite);
		}

		private void buttonCreateOnly_Click(object sender, System.EventArgs e)
		{
			SetIntelliMerge(IntelliMergeType.CreateOnly);
		}
	}
}
