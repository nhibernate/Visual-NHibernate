using System;
using System.Windows.Forms;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UI.Editors;

namespace Slyce.IntelliMerge.UI
{
	public partial class CodeTestForm : Form
	{
		public CodeTestForm()
		{
			InitializeComponent();

			if (DesignMode == false)
			{
				TextFileInformation tfi = new TextFileInformation();
				tfi.PrevGenFile = new TextFile("namespace Test{\npublic class TestClass {\npublic int i;\npublic void FunctionName() {\ni = 0;\n}\n}\n}");
				tfi.NewGenFile = new TextFile("namespace Test{\npublic class TestClass {\npublic int i;\npublic int j;\npublic void FunctionName() {\ni = 0;\nj = 0;\n}\n}\n}");
				tfi.UserFile = new TextFile("namespace Test{\npublic class TestClass {\npublic int i;\npublic int j;\npublic void FunctionName() {\nk = 0;\ni = 0;\n}\n}\n}");
				tfi.RelativeFilePath = "Test.cs";
				tfi.IntelliMerge = IntelliMergeType.CSharp;
				tfi.PerformDiff();

				ucCodeMergeEditor codeMergeEditor = new ucCodeMergeEditor(tfi);
				codeMergeEditor.Dock = DockStyle.Fill;
				this.Controls.Add(codeMergeEditor);
			}
		}

		[STAThread]
		public static void Main()
		{
			Application.Run(new CodeTestForm());
		}
	}
}
