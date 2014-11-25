using System;
using System.Windows.Forms;
using ArchAngel.Providers.CodeProvider;

namespace CodeFormatter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string textToFormat = syntaxEditor1.Text;

			CSharpParser formatter = new CSharpParser();
			formatter.ParseCode(textToFormat);
			syntaxEditor2.Text = formatter.CreatedCodeRoot.ToString();
		}
	}
}
