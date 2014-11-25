using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.NHibernateHelper.UI
{
	public partial class FormErrors : Form
	{
		private List<Exception> Errors;

		public FormErrors(string heading, List<Exception> errors)
		{
			InitializeComponent();
			Errors = errors;
			labelHeading.Text = heading;
			Populate();
		}

		private void Populate()
		{
			dataGridViewX1.Rows.Clear();

			foreach (var e in Errors)
				dataGridViewX1.Rows.Add(e.Message);
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			StringBuilder sb = new StringBuilder();

			foreach (var ex in Errors)
				sb.AppendLine(ex.Message);

			Clipboard.SetText(sb.ToString());
			Cursor = Cursors.Default;
		}
	}
}
