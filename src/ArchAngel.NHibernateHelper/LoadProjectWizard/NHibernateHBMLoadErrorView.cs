using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public partial class NHibernateHBMLoadErrorView : Form
	{
		private string nameOfFileWithError = "";
		private List<ValidationEventArgs> Errors;

		public NHibernateHBMLoadErrorView()
		{
			InitializeComponent();
		}

		public string Title
		{
			get { return Text; }
			set { Text = value; }
		}

		public string NameOfFileWithError
		{
			get { return nameOfFileWithError; }
			set
			{
				nameOfFileWithError = value;
				label1.Text =
					string.Format("The HBM file {0} could not be loaded because it contains elements we do not handle yet. Please send this file to Support@Slyce.com.", nameOfFileWithError);

			}
		}

		public void SetErrors(IEnumerable<ValidationEventArgs> errors)
		{
			listBox1.Items.Clear();

			Errors = errors.ToList();

			foreach(var error in GetErrorText(errors))
			{
				listBox1.Items.Add(error);
			}
		}

		private IEnumerable<string> GetErrorText(IEnumerable<ValidationEventArgs> errors)
		{
			return errors.Select(error => string.Format("({0}:{1}) - {2}", error.Exception.LineNumber, error.Exception.LinePosition, error.Message));
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void buttonClipboard_Click(object sender, System.EventArgs args)
		{
			var errors = GetErrorText(Errors);

			var sb = new StringBuilder();
			sb.Append("Error in file ").Append(nameOfFileWithError).AppendLine();
			sb.AppendLine("(Line Number : Column) - Error Message");
			errors.ForEach(e => sb.AppendLine(e));

			Clipboard.SetText(sb.ToString());
		}


	}
}
