using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	public partial class MessageBoxWithFileSelector : Form
	{
		private static DialogResult Result = DialogResult.Cancel;

		private static string Caption;
		private static string TextValue;
		private static string FilterValue;
		private static string FilePath;
		private static string FolderValue;
		private static MessageBoxButtons Button;
		private static MessageBoxIcon IconValue;

		public MessageBoxWithFileSelector(string caption, string text, string filePath, string filter, string folder, MessageBoxButtons button, MessageBoxIcon icon)
		{
			InitializeComponent();
			this.Icon = GetIcon(icon);
			pictureBox1.Image = Icon.ToBitmap();
			labelText.Text = text;
			Text = caption;
			textBoxFile.Text = filePath;
			Filter = filter;

			if (File.Exists(filePath))
				Folder = Path.GetDirectoryName(filePath);
			else
				Folder = folder;
		}

		public static string Folder { get; set; }

		private static string Filepath
		{
			get;
			set;
		}

		private static string Filter { get; set; }

		private Icon GetIcon(MessageBoxIcon icon)
		{
			// Some icons are the same and have the same enum values, so switch barfs.
			if (icon == MessageBoxIcon.Hand ||
				icon == MessageBoxIcon.Stop)
				icon = MessageBoxIcon.Error;

			if (icon == MessageBoxIcon.Warning)
				icon = MessageBoxIcon.Exclamation;

			if (icon == MessageBoxIcon.Asterisk)
				icon = MessageBoxIcon.Information;

			switch (icon)
			{
				case MessageBoxIcon.Asterisk:
					return SystemIcons.Asterisk;
				case MessageBoxIcon.Error:
					return SystemIcons.Error;
				case MessageBoxIcon.Exclamation:
					return SystemIcons.Exclamation;
				case MessageBoxIcon.None:
					return null;
				case MessageBoxIcon.Question:
					return SystemIcons.Question;
				default:
					throw new NotImplementedException("Not handled yet");
			}
		}

		public static DialogResult Show(string caption, string text, string filter, ref string filePath, string folder, MessageBoxButtons button, MessageBoxIcon icon)
		{
			return Show(null, caption, text, filter, ref filePath, folder, button, icon);
		}

		public static DialogResult Show(Control owner, string caption, string text, string filter, ref string filePath, string folder, MessageBoxButtons button, MessageBoxIcon icon)
		{
			Caption = caption;
			TextValue = text;
			FilePath = filePath;
			FilterValue = filter;
			FolderValue = folder;
			Button = button;
			IconValue = icon;

			System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ShowMessageBox);
			System.Threading.Thread th = new System.Threading.Thread(ts);
			th.SetApartmentState(System.Threading.ApartmentState.STA);
			th.Start();
			th.Join();

			filePath = Filepath;
			return Result;
		}

		private static void ShowMessageBox()
		{
			MessageBoxWithFileSelector messageBox = new MessageBoxWithFileSelector(Caption, TextValue, Filepath, FilterValue, FolderValue, Button, IconValue);

			messageBox.StartPosition = FormStartPosition.CenterScreen;
			messageBox.TopMost = true;
			messageBox.Show();
			FormHelper.MakeTopMost(messageBox);
			messageBox.Visible = false;
			messageBox.ShowDialog();

		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			//FormHelper.MakeNormal(this);
			ShowOpenFileDialog();

			if (Result == DialogResult.OK)
				textBoxFile.Text = Filepath;

			//FormHelper.MakeTopMost(this);
		}

		private void ShowOpenFileDialog()
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.FileName = Filepath;
			dialog.Filter = Filter;
			//dialog.Title = Text;
			dialog.InitialDirectory = Folder;
			Result = dialog.ShowDialog(this);

			if (Result == DialogResult.OK)
				Filepath = dialog.FileName;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (!File.Exists(textBoxFile.Text))
			{
				MessageBox.Show("The file doesn't exist. Please select a file or click Cancel.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Result = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Result = DialogResult.Cancel;
			Close();
		}

	}
}
