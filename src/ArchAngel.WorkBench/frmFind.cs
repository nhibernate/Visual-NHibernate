using System;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Workbench
{
	public partial class frmFind : Form
	{
		public frmFind(bool showReplace, bool searchAllFiles)
		{
			InitializeComponent();

			EnableDoubleBuffering();

			if (showReplace)
			{
				buttonFindAll.Visible = false;
				buttonFindAll.Text = "Find All";
				buttonReplaceAll.Text = "Replace &All";
				Text = "Find and Replace";
			}
			else
			{
				lblReplace.Visible = false;
				ddlReplace.Visible = false;
				buttonReplace.Visible = false;
				buttonReplaceAll.Visible = false;
				Text = "Find";
				panelOptions.Top = lblReplace.Top;
				this.Height = this.Height - this.ClientSize.Height + panelOptions.Top + panelOptions.Height + 5;
			}
			radioButtonCurrentFile.Checked = !searchAllFiles;
			radioButtonAllFiles.Checked = searchAllFiles;
		}

		private void EnableDoubleBuffering()
		{
			// Set the value of the double-buffering style bits to true.
			this.SetStyle(ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint,
				true);
			this.UpdateStyles();
		}

		private void frmFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				MessageBox.Show(this, "About ");
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void frmFind_Load(object sender, EventArgs e)
		{
			btnClose.Top = -100;
			// Update options
			ddlFind.Text = SearchHelper.Options.FindText;
			ddlReplace.Text = SearchHelper.Options.ReplaceText;
			chkFindMatchCase.Checked = SearchHelper.Options.MatchCase;
			chkFindMatchWholeWord.Checked = SearchHelper.Options.MatchWholeWord;
			//chkFindSearchUp.Checked = SearchHelper.Options.SearchUp;
			//chkFindUse.Checked = (SearchHelper.Options.SearchType == FindReplaceSearchType.RegularExpression);
			ddlFind.Text = SearchHelper.Options.FindText;

			switch (SearchHelper.scope)
			{
				case SearchHelper.Scope.ScriptOnly:
					chkScopeScript.Checked = true;
					break;
				case SearchHelper.Scope.OutputOnly:
					chkScopeOutput.Checked = true;
					break;
				case SearchHelper.Scope.Both:
					chkScopeBoth.Checked = true;
					break;
				default:
					throw new NotImplementedException("Scope not coded yet: " + SearchHelper.scope.ToString());
			}
			//switch (SearchHelper.searchFunctions)
			//{
			//    case SearchHelper.SearchFunctions.CurrentFunction:
			//        radioButtonCurrentFile.Checked = true;
			//        break;
			//    case SearchHelper.SearchFunctions.AllFunctions:
			//        radioButtonAllFiles.Checked = true;
			//        break;
			//    default:
			//        throw new NotImplementedException("Not coded yet");
			//}
		}

		public void SetSearchText()
		{
			ddlFind.Text = SearchHelper.Options.FindText;
		}

		private void ClearAllSelections()
		{
			ContentItems.Templates.Instance.ClearAllSelections();
		}

		private void SearchAll()
		{
			ClearAllSelections();
			SearchHelper.Search(ddlFind.Text);
			// Display the results in the Find grid
			ContentItems.Templates.Instance.ShowFindResults();
		}

		private void SetSearchConditions()
		{
			SearchHelper.scope = SearchHelper.Scope.Both;

			if (chkScopeScript.Checked) { SearchHelper.scope = SearchHelper.Scope.ScriptOnly; }
			else if (chkScopeOutput.Checked) { SearchHelper.scope = SearchHelper.Scope.OutputOnly; }
			else if (chkScopeBoth.Checked) { SearchHelper.scope = SearchHelper.Scope.Both; }

			if (radioButtonCurrentFile.Checked)
				SearchHelper.searchFunctions = SearchHelper.SearchFunctions.CurrentFunction;
			else
				SearchHelper.searchFunctions = SearchHelper.SearchFunctions.AllFunctions;

			SearchHelper.Options.MatchCase = chkFindMatchCase.Checked;
			SearchHelper.Options.MatchWholeWord = chkFindMatchWholeWord.Checked;
			SearchHelper.Options.SearchHiddenText = false;// chkFindMatchCase.Checked;
			SearchHelper.Options.SearchUp = false;// chkFindSearchUp.Checked;
			SearchHelper.Options.FindText = ddlFind.Text;
			ContentItems.Templates.Instance.EnableFindNext();
		}

		private void FindNext()
		{
			Cursor = Cursors.WaitCursor;
			SetSearchConditions();

			if (radioButtonCurrentFile.Checked)
				SearchHelper.Search(ddlFind.Text);
			else
				SearchAll();

			Cursor = Cursors.Default;
		}

		private void Replace()
		{
			Cursor = Cursors.WaitCursor;
			SetSearchConditions();

			SyntaxEditor editor = ContentItems.Templates.Instance.SyntaxEditor;

			if (editor != null)
			{
				string text = editor.Document.GetText(LineTerminator.Newline);

				if (Slyce.Common.Utility.StringsAreEqual(text.Substring(editor.SelectedView.Selection.StartOffset, editor.SelectedView.Selection.Length), ddlFind.Text, false))
				{
					try
					{
						editor.SuspendPainting();
						int originalStartPos = editor.SelectedView.Selection.StartOffset;
						string newText = text.Substring(0, editor.SelectedView.Selection.StartOffset) + ddlReplace.Text + text.Substring(editor.SelectedView.Selection.EndOffset);
						editor.Document.Text = newText;
						editor.SelectedView.Selection.StartOffset = editor.SelectedView.Selection.EndOffset = originalStartPos + 1;
					}
					finally
					{
						editor.ResumePainting();
					}
				}
			}
			SearchHelper.Search(ddlFind.Text);
			Cursor = Cursors.Default;
		}

		/// <summary>
		/// Updates the find/replace options.
		/// </summary>
		private void UpdateFindReplaceOptions()
		{
			SearchHelper.Options.FindText = ddlFind.Text;
			SearchHelper.Options.ReplaceText = ddlReplace.Text;
			SearchHelper.Options.MatchCase = chkFindMatchCase.Checked;
			SearchHelper.Options.MatchWholeWord = chkFindMatchWholeWord.Checked;
			SearchHelper.Options.SearchUp = false;// chkFindSearchUp.Checked;
			SearchHelper.Options.SearchType = FindReplaceSearchType.Normal;// (chkFindUse.Checked ? FindReplaceSearchType.RegularExpression : FindReplaceSearchType.Normal);
		}

		private void frmFind_FormClosed(object sender, FormClosedEventArgs e)
		{
			ContentItems.Templates.Instance.FormFind = null;
			SetSearchConditions();
		}

		private void frmFind_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void buttonFind_Click(object sender, EventArgs e)
		{
			FindNext();
		}

		private void buttonReplace_Click(object sender, EventArgs e)
		{
			Replace();
		}

		private void buttonFindAll_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			SetSearchConditions();
			SearchAll();
			Cursor = Cursors.Default;
		}

		private void buttonReplaceAll_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			SetSearchConditions();
			int numReplacements = SearchHelper.ReplaceAll(ddlFind.Text, ddlReplace.Text);
			Cursor = Cursors.Default;
			MessageBox.Show(this, string.Format("{0} replacements were made.", numReplacements), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}