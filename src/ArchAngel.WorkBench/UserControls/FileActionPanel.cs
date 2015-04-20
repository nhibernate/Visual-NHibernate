using System.Windows.Forms;
using ArchAngel.Workbench.ContentItems;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Workbench.UserControls
{
	public partial class FileActionPanel : UserControl
	{
		private readonly Output output;
		private IntelliMergeType selectedItem = IntelliMergeType.NotSet;

		public FileActionPanel()
		{
			InitializeComponent();
		}

		public FileActionPanel(Output output)
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

		private void buttonNotSet_Click(object sender, System.EventArgs e)
		{
			SetIntelliMerge(IntelliMergeType.NotSet);
		}

		private void buttonCSIntelliMerge_Click(object sender, System.EventArgs e)
		{
			SetIntelliMerge(IntelliMergeType.CSharp);
		}

		private void buttonPlainTextMerge_Click(object sender, System.EventArgs e)
		{
			SetIntelliMerge(IntelliMergeType.PlainText);
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
