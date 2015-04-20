using System;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Designer
{
	public partial class frmFind : Form
	{
		public frmFind(bool showReplace)
		{
			InitializeComponent();
            grouper1.ApplySlyceColorScheme();
            grouper2.ApplySlyceColorScheme();

            EnableDoubleBuffering();
			SearchHelper.Options = Controller.EditorFindReplaceOptions;

            if (showReplace)
            {
                btnFindAll.Visible = false;
            }
            else
            {
                lblReplace.Visible = false;
                ddlReplace.Visible = false;
                btnReplace.Visible = false;
                btnReplaceAll.Visible = false;
            }
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
			ddlScope.SelectedIndex = 0;
			// Update options
			ddlFind.Text = SearchHelper.Options.FindText;
			ddlReplace.Text = SearchHelper.Options.ReplaceText;
			chkFindMatchCase.Checked = SearchHelper.Options.MatchCase;
			chkFindMatchWholeWord.Checked = SearchHelper.Options.MatchWholeWord;
			chkFindSearchUp.Checked = SearchHelper.Options.SearchUp;
			chkFindUse.Checked = (SearchHelper.Options.SearchType == FindReplaceSearchType.RegularExpression);
            ddlFind.Text = SearchHelper.Options.FindText;
			//ddlReplace.Text = ???;

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
					throw new NotImplementedException("Not coded yet");
			}
			switch (SearchHelper.searchFunctions)
			{
				case SearchHelper.SearchFunctions.CurrentFunction:
					ddlScope.Text = "Current function";
					break;
				case SearchHelper.SearchFunctions.OpenFunctions:
					ddlScope.Text = "All open functions";
					break;
				case SearchHelper.SearchFunctions.AllFunctions:
					ddlScope.Text = "All functions";
					break;
				default:
					throw new NotImplementedException("Not coded yet");
			}
		}

		public void SetSearchText()
		{
			ddlFind.Text = SearchHelper.Options.FindText;
		}

		private void ClearAllSelections()
		{
			if (Controller.Instance.MainForm.UcFunctions != null)
			{
				Controller.Instance.MainForm.UcFunctions.ClearAllSelections();
			}
		}

		private void SearchAll()
		{
			ClearAllSelections();
			SearchHelper.Search(ddlFind.Text);
			// Display the results in the Find grid
			Controller.Instance.MainForm.UcFunctions.ShowFindResults();
		}

		private void SetSearchConditions()
		{
			SearchHelper.scope = SearchHelper.Scope.Both;

			if (chkScopeScript.Checked) { SearchHelper.scope = SearchHelper.Scope.ScriptOnly; }
			else if (chkScopeOutput.Checked) { SearchHelper.scope = SearchHelper.Scope.OutputOnly; }
			else if (chkScopeBoth.Checked) { SearchHelper.scope = SearchHelper.Scope.Both; }

			switch (ddlScope.Text.ToLower())
			{
				case "current function":
					SearchHelper.searchFunctions = SearchHelper.SearchFunctions.CurrentFunction;
					break;
				case "all open functions":
					SearchHelper.searchFunctions = SearchHelper.SearchFunctions.OpenFunctions;
					break;
				case "all functions":
					SearchHelper.searchFunctions = SearchHelper.SearchFunctions.AllFunctions;
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}
			SearchHelper.Options.MatchCase = chkFindMatchCase.Checked;
			SearchHelper.Options.MatchWholeWord = chkFindMatchWholeWord.Checked;
			//SearchHelper.Options.SearchHiddenText = chkFindMatchCase.Checked;
			SearchHelper.Options.SearchUp = chkFindSearchUp.Checked;
            SearchHelper.Options.FindText = ddlFind.Text;
            Controller.Instance.MainForm.EnableFindNext();
		}


		private void btnSearch_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			SetSearchConditions();

			if (ddlScope.Text.ToLower() != "all functions")
			{
				SearchHelper.Search(ddlFind.Text);
			}
			else
			{
				SearchAll();
			}
			Cursor = Cursors.Default;
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			SetSearchConditions();

			SyntaxEditor editor = ((ucFunction)Controller.Instance.MainForm.UcFunctions.tabStrip1.SelectedTab.AttachedControl.Controls[0]).syntaxEditor1;

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
			SearchHelper.Options.SearchUp = chkFindSearchUp.Checked;
			SearchHelper.Options.SearchType = (chkFindUse.Checked ? FindReplaceSearchType.RegularExpression : FindReplaceSearchType.Normal);
			//SearchHelper.Options.SearchInSelection = true;
			//SearchHelper.Options.ChangeSelection = false;
		}

		private void ddlFind_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void ddlReplace_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void ddlScope_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void chkScopeScript_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkScopeOutput_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkScopeBoth_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkFindMatchCase_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkFindMatchWholeWord_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkFindSearchUp_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkFindUse_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void ddlFind_TextChanged(object sender, EventArgs e)
		{
		}

		private void ddlReplace_TextChanged(object sender, EventArgs e)
		{
		}

		private void ddlScope_TextChanged(object sender, EventArgs e)
		{
		}

        private void btnFindAll_Click(object sender, EventArgs e)
        {
			  Cursor = Cursors.WaitCursor;
			  SetSearchConditions();
           SearchAll();
			  Cursor = Cursors.Default;
        }

		private void btnReplaceAll_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
            Application.DoEvents();
			SetSearchConditions();
			int numReplacements = SearchHelper.ReplaceAll(ddlFind.Text, ddlReplace.Text);
			Cursor = Cursors.Default;
            MessageBox.Show(this, string.Format("{0} replacements were made.", numReplacements), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void frmFind_FormClosed(object sender, FormClosedEventArgs e)
		{
			Controller.FindForm = null;
			SetSearchConditions();
		}

        private void frmFind_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }

	}
}