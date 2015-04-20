using System;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
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
			FunctionInfo currentFunction = null;
			listResults.Items.Clear();

			for (int i = 0; i < SearchHelper.FoundLocations.Count; i++)
			{
				if (SearchHelper.FoundLocations[i].Function != currentFunction)
				{
					string snippet = GetText(SearchHelper.FoundLocations[i].Function, SearchHelper.FoundLocations[i].StartPos);
					ListViewItem item = new ListViewItem(new string[] { SearchHelper.FoundLocations[i].Function.Name, SearchHelper.FoundLocations[i].StartPos.ToString(), SearchHelper.FoundLocations[i].Length.ToString(), snippet });
					item.Tag = SearchHelper.FoundLocations[i].Function;
					item.ToolTipText = snippet;
					listResults.Items.Add(item);
				}
			}
		}

		private string GetText(FunctionInfo function, int startPos)
		{
			string body;

			for (int i = 0; i < SearchHelper.FoundLocations.Count; i++)
			{
				if (SearchHelper.FoundLocations[i].Function == function)
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
					while (lineEnd > 0 && lineEnd < body.Length)
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

					if (textLength > 0 && lineStart > 0 && lineStart < body.Length && (lineStart + textLength) < body.Length)
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

		private void lstFunctions_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (lstFunctions.SelectedItems.Count > 0)
			//{
			//   int left = lstFunctions.Left + lstFunctions.Columns[0].Width + lstFunctions.Columns[1].Width + lstFunctions.Columns[2].Width;
			//   Editor.Left = left;
			//   Editor.Top = lstFunctions.Top + lstFunctions.SelectedItems[0].Position.Y;
			//   Editor.Document.Text = "GFH";// lstFunctions.SelectedItems[0].SubItems[3].Text;
			//}
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

		private void listResults_DoubleClick(object sender, EventArgs e)
		{
			if (listResults.SelectedItems.Count > 0)
			{
				FunctionInfo function = (FunctionInfo)listResults.SelectedItems[0].Tag;
				Controller.Instance.MainForm.ShowFunction(function, null);
				ucFunction functionScreen = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();
				ActiproSoftware.SyntaxEditor.SyntaxEditor editor = functionScreen.syntaxEditor1;
				editor.SelectedView.Selection.StartOffset = int.Parse(listResults.SelectedItems[0].SubItems[1].Text);
				editor.SelectedView.Selection.EndOffset = int.Parse(listResults.SelectedItems[0].SubItems[1].Text) + int.Parse(listResults.SelectedItems[0].SubItems[2].Text);
			}
		}
	}
}
