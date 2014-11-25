using System;
using System.Windows.Forms;

namespace ArchAngel.Workbench
{
	public partial class ucFindResults : UserControl
	{
		//ActiproSoftware.SyntaxEditor.SyntaxEditor Editor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();

		public ucFindResults()
		{
			InitializeComponent();
			if (Slyce.Common.Utility.InDesignMode) { return; }

			EnableDoubleBuffering();
			//Editor.Document.Multiline = false;
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

		public void Clear()
		{
			listResults.Items.Clear();
		}

		public void ShowResults()
		{
			listResults.Items.Clear();

			foreach (var location in SearchHelper.FoundLocations)
			{
				string snippet = GetText(location.ScriptFile, location.StartPos);
				string placeholder;

				if (location.ScriptFile.Iterator == Interfaces.Template.IteratorTypes.None)
					placeholder = "[Project]";
				else
					placeholder = string.Format("[{0}]", location.ScriptFile.Iterator.ToString());

				string displayText = ContentItems.Templates.GetNodeDisplayText(location.ScriptFile.Name, placeholder);
				//ListViewItem item = new ListViewItem(new string[] { displayText, location.StartPos.ToString(), location.Length.ToString(), snippet });
				ListViewItem item = new ListViewItem(new string[] { displayText, location.StartPos.ToString(), location.Length.ToString(), snippet });
				item.Tag = location.ScriptFile;
				item.ToolTipText = snippet;
				listResults.Items.Add(item);
			}
		}

		private string GetText(ArchAngel.Interfaces.Template.File scriptFile, int startPos)
		{
			string body;

			for (int i = 0; i < SearchHelper.FoundLocations.Count; i++)
			{
				if (SearchHelper.FoundLocations[i].ScriptFile == scriptFile)
				{
					body = SearchHelper.FoundLocations[i].Body;

					if (body.Length == 0)
					{
						continue;
					}
					int lineStart = startPos;

					// Find the start of the line
					while (lineStart > 0 && lineStart < body.Length)
					{
						if (body[lineStart] == '\n' ||
							body[lineStart] == '\r')
						{
							lineStart += 1;
							break;
						}
						lineStart -= 1;
					}
					int lineEnd = lineStart;// startPos;

					// Find the end of the line
					while (lineEnd >= 0 && lineEnd < body.Length)
					{
						if (body[lineEnd] == '\n' ||
							body[lineEnd] == '\r')
						{
							//lineEnd -= 1;
							break;
						}
						lineEnd += 1;
					}
					// Get the text of the line in question
					int textLength = lineEnd - lineStart;
					string returnVal = "";

					if (textLength > 0 && lineStart >= 0 && lineStart < body.Length && (lineStart + textLength) < body.Length)
					{
						returnVal = body.Substring(lineStart, textLength);

						if (returnVal.Length > 0)
						{
							// Remove any tabs from the line text
							returnVal = returnVal.Replace("\t", "  ");
						}
					}
					return returnVal;
				}
			}
			return "";
		}

		private void lstFunctions_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			//if (e.Item..Index == 5)
			//{
			//    toolTip1.Show(e.Item.Text, lstFunctions, Cursor.Position, 500);
			//}
		}

		private void ucFindResults_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void listResults_Resize(object sender, EventArgs e)
		{
			listResults.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			listResults.Columns[1].Width = 0;
			listResults.Columns[2].Width = 0;
			listResults.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void ShowResultInEditor()
		{
			if (listResults.SelectedItems.Count > 0)
			{
				ContentItems.Templates.Instance.SelectFile((ArchAngel.Interfaces.Template.File)listResults.SelectedItems[0].Tag);
				ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.StartOffset = int.Parse(listResults.SelectedItems[0].SubItems[1].Text);
				ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.EndOffset = int.Parse(listResults.SelectedItems[0].SubItems[1].Text) + int.Parse(listResults.SelectedItems[0].SubItems[2].Text);
			}
		}

		private void listResults_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowResultInEditor();
		}
	}
}
