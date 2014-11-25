using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI.Editors
{
	public partial class ucBinaryMergeEditor : UserControl
	{
		private BinaryFileInformation fileInformation;

		public ucBinaryMergeEditor()
		{
			InitializeComponent();
		}

		public ucBinaryMergeEditor(BinaryFileInformation fileInformation)
			: this()
		{
			Reset(fileInformation);
		}

		public void Reset(BinaryFileInformation fileInformation)
		{
			this.fileInformation = fileInformation;
			bool versionsSame = ((BinaryFile)fileInformation.UserFile).GetVersionInformation() == ((BinaryFile)fileInformation.NewGenFile).GetVersionInformation();
			bool filesizeSame = ((BinaryFile)fileInformation.UserFile).GetFileSize() == ((BinaryFile)fileInformation.NewGenFile).GetFileSize();
			bool md5Same = ((BinaryFile)fileInformation.UserFile).GetMD5() == ((BinaryFile)fileInformation.NewGenFile).GetMD5();

			if (!fileInformation.UserFile.IsFileOnDisk)
			{
				labelHeader.Text = string.Format("New file    -   {0}     [Binary file]", fileInformation.RelativeFilePath);
				labelHeader.BackgroundStyle.BackColor = Color.Green;
				labelHeader.BackgroundStyle.BackColor2 = Color.GreenYellow;
			}
			else if (versionsSame && filesizeSame && md5Same)
			{
				labelHeader.Text = string.Format("No changes    -   {0}     [Binary file]", fileInformation.RelativeFilePath);
				labelHeader.BackgroundStyle.BackColor = Color.Green;
				labelHeader.BackgroundStyle.BackColor2 = Color.GreenYellow;
			}
			else
			{
				labelHeader.Text = string.Format("File is different    -   {0}", fileInformation.RelativeFilePath);
				labelHeader.BackgroundStyle.BackColor = Color.Brown;
				labelHeader.BackgroundStyle.BackColor2 = Color.OrangeRed;
			}
			SetVersionInfo(fileInformation.UserFile as BinaryFile, labelExistingFileVersion, versionsSame);
			SetFileSize(fileInformation.UserFile as BinaryFile, labelExistingFilesize, filesizeSame);
			SetMD5Info(fileInformation.UserFile as BinaryFile, labelExistingMD5, filesizeSame);

			SetVersionInfo(fileInformation.NewGenFile as BinaryFile, labelNewFileVersion, versionsSame);
			SetFileSize(fileInformation.NewGenFile as BinaryFile, labelNewFilesize, filesizeSame);
			SetMD5Info(fileInformation.NewGenFile as BinaryFile, labelNewMD5, filesizeSame);

			SetHeading(versionsSame && filesizeSame);

			int widthExistingMD5 = (int)Graphics.FromHwnd(labelExistingMD5.Handle).MeasureString(labelExistingMD5.Text, labelExistingMD5.Font).Width + 5;
			int widthExistingHeader = (int)Graphics.FromHwnd(labelExistingHeader.Handle).MeasureString(labelExistingHeader.Text, labelExistingHeader.Font).Width + 5;

			int widthNewMD5 = (int)Graphics.FromHwnd(labelNewMD5.Handle).MeasureString(labelNewMD5.Text, labelNewMD5.Font).Width + 5;
			int widthNewHeader = (int)Graphics.FromHwnd(labelNewHeader.Handle).MeasureString(labelNewHeader.Text, labelNewHeader.Font).Width + 5;

			panelExisting.Width = Math.Max(widthExistingMD5, widthExistingHeader);
			panelNew.Width = Math.Max(widthNewMD5, widthNewHeader); ;
		}

		private void SetHeading(bool filesAreSame)
		{
			string spacer = "  ";

			if (filesAreSame)
			{
				labelHeader.Text = "No changes";
				labelFilePath.Text = spacer + fileInformation.RelativeFilePath;
				labelFilePath.BackgroundStyle.BackColor = labelHeader.BackgroundStyle.BackColor = Color.Green;
				labelFilePath.BackgroundStyle.BackColor2 = labelHeader.BackgroundStyle.BackColor2 = Color.GreenYellow;
			}
			else if (fileInformation.UserFile.IsFileOnDisk) //(OldFileExists)
			{
				labelHeader.Text = "File changed";
				labelFilePath.Text = spacer + fileInformation.RelativeFilePath;
				labelFilePath.BackgroundStyle.BackColor = labelHeader.BackgroundStyle.BackColor = Color.Brown;
				labelFilePath.BackgroundStyle.BackColor2 = labelHeader.BackgroundStyle.BackColor2 = Color.Orange;
			}
			else
			{
				labelHeader.Text = "New file";
				labelFilePath.Text = spacer + fileInformation.RelativeFilePath;
				labelFilePath.BackgroundStyle.BackColor = labelHeader.BackgroundStyle.BackColor = Color.Green;
				labelFilePath.BackgroundStyle.BackColor2 = labelHeader.BackgroundStyle.BackColor2 = Color.GreenYellow;
			}
			labelFilePath.Width = Convert.ToInt32(System.Drawing.Graphics.FromHwnd(labelFilePath.Handle).MeasureString(labelFilePath.Text, labelFilePath.Font).Width) + 10;
		}

		private void SetFileSize(BinaryFile file, DevComponents.DotNetBar.LabelX label, bool sizesSame)
		{
			if (file == null || !file.IsFileOnDisk)
			{
				label.Text = "-";
				return;
			}

			double numMegaBytes = file.GetFileSize() / (1024d * 1024d);

			if (numMegaBytes >= 1)
				label.Text = numMegaBytes + " MB";
			else
			{
				numMegaBytes = file.GetFileSize() / 1024d;
				label.Text = numMegaBytes + " kB";
			}
			if (sizesSame)
				pictureBoxFilesize.Image = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.IntelliMerge.Resources.font_char61_green_16_h.png"));
			else
				pictureBoxFilesize.Image = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.IntelliMerge.Resources.font_char33_red_16_h.png"));
		}

		private void SetVersionInfo(BinaryFile file, DevComponents.DotNetBar.LabelX label, bool versionsSame)
		{
			if (file == null || !file.IsFileOnDisk)
			{
				label.Text = "-";
				return;
			}
			label.Text = file.GetVersionInformation();

			if (versionsSame)
				pictureBoxFileVersion.Image = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.IntelliMerge.Resources.font_char61_green_16_h.png"));
			else
				pictureBoxFileVersion.Image = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.IntelliMerge.Resources.font_char33_red_16_h.png"));
		}

		private void SetMD5Info(BinaryFile file, DevComponents.DotNetBar.LabelX label, bool md5Same)
		{
			if (file == null || !file.IsFileOnDisk)
			{
				label.Text = "-";
				return;
			}
			label.Text = file.GetMD5();

			if (md5Same)
				pictureBoxMD5.Image = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.IntelliMerge.Resources.font_char61_green_16_h.png"));
			else
				pictureBoxMD5.Image = System.Drawing.Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.IntelliMerge.Resources.font_char33_red_16_h.png"));
		}

		//private void prevgenLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		//{
		//    Process.Start(fileInformation.PrevGenFile.FilePath);
		//}

		private void templateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(fileInformation.NewGenFile.FilePath);
		}

		private void userLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(fileInformation.UserFile.FilePath);
		}

		//private void prevgenRadioButton_CheckedChanged(object sender, EventArgs e)
		//{
		//    if(prevgenRadioButton.Checked)
		//    {
		//        acceptButton.Enabled = true;
		//        cancelButton.Enabled = true;

		//        userRadioButton.Checked = false;
		//        templateRadioButton.Checked = false;
		//    }
		//}

		//private void templateRadioButton_CheckedChanged(object sender, EventArgs e)
		//{
		//    if (templateRadioButton.Checked)
		//    {
		//        acceptButton.Enabled = true;
		//        cancelButton.Enabled = true;

		//        userRadioButton.Checked = false;
		//        prevgenRadioButton.Checked = false;
		//    }
		//}

		//private void userRadioButton_CheckedChanged(object sender, EventArgs e)
		//{
		//    if (templateRadioButton.Checked)
		//    {
		//        acceptButton.Enabled = true;
		//        cancelButton.Enabled = true;

		//        userRadioButton.Checked = false;
		//        prevgenRadioButton.Checked = false;
		//    }
		//}

		//private void acceptButton_Click(object sender, EventArgs e)
		//{
		//    if(prevgenRadioButton.Checked)
		//    {
		//        fileInformation.MergedFile = fileInformation.PrevGenFile;
		//    }
		//    else 
		//    if (userRadioButton.Checked)
		//    {
		//        fileInformation.MergedFile = fileInformation.UserFile;
		//    }
		//    else if (templateRadioButton.Checked)
		//    {
		//        fileInformation.MergedFile = fileInformation.NewGenFile;
		//    }
		//    else
		//    {
		//        return;
		//    }

		//    fileInformation.PerformDiff();
		//}
	}
}