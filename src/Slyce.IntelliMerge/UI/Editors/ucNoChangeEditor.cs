using System;
using System.Windows.Forms;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI.Editors
{
	public partial class ucNoChangeEditor : UserControl
	{
		/// <summary>
		/// Creates a new No Change editor, which displays the text in the NewGenFile of the file passed to it.
		/// </summary>
		/// <param name="textFileInfo">The text file to display.</param>
		public ucNoChangeEditor(FileInformation<string> textFileInfo)
		{
			InitializeComponent();

			if (textFileInfo == null)
			{
				throw new InvalidOperationException("Cannot initialise the NoChangeEditor with a null TextFileInformation object.");
			}

			//if (textFileInfo.CurrentDiffResult.DiffType != TypeOfDiff.ExactCopy)
			//{
			//    throw new Exception("This control is only inteneded to be used for files no changes.");
			//}
			IProjectFile<string> file = textFileInfo.MergedFileExists ? textFileInfo.MergedFile : textFileInfo.UserFile;
			if(file == null || file.HasContents == false)
			{
				if (textFileInfo.NewGenFile == null || textFileInfo.NewGenFile.HasContents == false)
				{
					throw new InvalidOperationException("The user and newly generated file do not exist, so the control has nothing to display.");
				}
				file = textFileInfo.NewGenFile;
			}
            

			syntaxEditor.Text = file.GetContents();

			syntaxEditor.Document.Language = SyntaxEditorHelper.GetSyntaxLanguageFromFileName(textFileInfo.RelativeFilePath);
			warningLabel.MaximumSize = new System.Drawing.Size(Size.Width, 0);
			warningLabel.Text = "";
		}

		/// <summary>
		/// Creates a new No Change editor, which displays the text in the NewGenFile of the file passed to it.
		/// </summary>
		/// <param name="textFileInfo">The text file to display.</param>
		/// <param name="warningText">The warning text to display with the file.</param>
		public ucNoChangeEditor(TextFileInformation textFileInfo, string warningText) : this(textFileInfo)
		{
			warningLabel.Text = warningText;
			if(warningText != null && warningText.Contains("User's") && warningText.Contains("deleted"))
			{
				warningLabel.Text =
					"It looks like you have deleted this file in your project directory. " +
					"The template is going to regenerate this file. If you don't want this to happen, " +
					"uncheck it in the file list. The ability to merge renamed files will be available in a future version of ArchAngel";
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			warningLabel.MaximumSize = new System.Drawing.Size(Size.Width, 0);
		}

		public string TitleText
		{
			get
			{
				return titleLabel.Text;
			}
			set
			{
				titleLabel.Text = value;
			}
		}
	}
}