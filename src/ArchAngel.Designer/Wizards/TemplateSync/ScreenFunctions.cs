using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Designer.DesignerProject;
using DevExpress.XtraTreeList.Nodes;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
	public partial class ScreenFunctions : Interfaces.Controls.ContentItems.ContentItem
	{
		#region Enums
		enum TreeNodeImages
		{
			Unchecked = 0,
			Checked = 1
		}

		#endregion

		#region Inner Classes
		private class FunctionContainer
		{
			public readonly FunctionInfo MyFunction;
			public readonly FunctionInfo TheirFunction;
			public readonly string Name;

			public FunctionContainer(FunctionInfo myFunction, FunctionInfo theirFunction, string name)
			{
				MyFunction = myFunction;
				TheirFunction = theirFunction;
				Name = name;
			}

			public override string ToString()
			{
				return Name;
			}
		}
		#endregion

		private int ProjectsHash;
		private bool UseSplitLanguage;
		private Color EDITOR_BACK_COLOR_NORMAL = Color.White;
		private Color EDITOR_BACK_COLOR_FADED = Color.LightBlue;
		private Slyce.IntelliMerge.SlyceMerge.LineSpan OrigianlLineSpan;
	    private bool MustSkip;
	    readonly Bitmap GreenArrow = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.green_arrow.png"));
	    readonly Bitmap BlueArrow = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.blue_arrow.png"));
	    readonly Bitmap RemoveImage = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Designer.Resources.error_16.png"));
	    readonly DevExpress.XtraEditors.Repository.RepositoryItem EmptyRepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
		private bool BusyPopulatingEditors;
		private readonly List<Button> ApplyButtons = new List<Button>();
		private readonly List<Button> DeleteButtons = new List<Button>();
		private const int ImageWidth = 40;
		private FunctionContainer FunctionContainerDisplayedInEditors;

		public ScreenFunctions()
		{
			InitializeComponent();
			PageHeader = "Functions";
			PageDescription = "Select which functions to synchronise.";
			HasNext = true;
			HasPrev = true;
			syntaxEditorExternal.Document.ReadOnly = true;
			ucHeading1.Text = "Differences";

#if DEBUG
			btnTest.Visible = true;
#endif
		}

		private void Populate()
		{
			int newHash = frmTemplateSyncWizard.MyProject.GetHashCode() + frmTemplateSyncWizard.TheirProject.GetHashCode();

			if (newHash != ProjectsHash)
			{
				ProjectsHash = newHash;
				treeListReferencedFiles.BeginUnboundLoad();
				treeListReferencedFiles.Nodes.Clear();
				treeListReferencedFiles.Columns[1].Caption = Path.GetFileName(frmTemplateSyncWizard.TheirProject.ProjectFileName) + " (external project)";
				treeListReferencedFiles.Columns[3].Caption = Path.GetFileName(frmTemplateSyncWizard.MyProject.ProjectFileName) + " (current project)";

				int numChanges = AddFunctionNodes();
				treeListReferencedFiles.ExpandAll();
				treeListReferencedFiles.EndUnboundLoad();

				if (numChanges == 0)
				{
					MustSkip = true;
					frmTemplateSyncWizard.Instance.RemoveCurrentScreen();
					return;
				}
			    MustSkip = false;
			}
			else if (MustSkip)
			{
				frmTemplateSyncWizard.Instance.RemoveCurrentScreen();
				return;
			}
		}

		private int AddFunctionNodes()
		{
			int numAdded = 0;
			var functionsMine = new List<FunctionInfo>();
			var functionsTheirs = new List<FunctionInfo>();
			var bothRootNode = AddNodeTheirs("Modified Functions", "", "", null, "ModifiedFunctions", false);

			foreach (FunctionInfo theirFunction in frmTemplateSyncWizard.TheirProject.Functions)
			{
				var found = false;

				foreach (FunctionInfo myFunction in frmTemplateSyncWizard.MyProject.Functions)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirFunction.Name, myFunction.Name, false))
					{
						found = true;

						if (FunctionsAreDifferent(myFunction, theirFunction))
						{
							TreeListNode functionNode;

							if (myFunction.Body != theirFunction.Body)
							{
								functionNode = AddNodeBoth(myFunction.Name, "Code differences exist.", "", bothRootNode, new FunctionContainer(myFunction, theirFunction, myFunction.Name), false);
							}
							else
							{
								functionNode = AddNodeBoth(myFunction.Name, "", "", bothRootNode, new FunctionContainer(myFunction, theirFunction, myFunction.Name));
							}
							if (myFunction.Category != theirFunction.Category)
							{
								AddNodeBoth("Category", theirFunction.Category, myFunction.Category, functionNode, null, true);
							}
							if (myFunction.Description != theirFunction.Description)
							{
								AddNodeBoth("Description", theirFunction.Description, myFunction.Description, functionNode, null, true);
							}
							if (myFunction.IsTemplateFunction != theirFunction.IsTemplateFunction)
							{
								AddNodeBoth("Is Template Function", theirFunction.IsTemplateFunction, myFunction.IsTemplateFunction, functionNode, null, true);
							}
							if (!myFunction.ParametersAreEqual(theirFunction.Parameters))
							{
								AddNodeBoth("Parameters", "", "", functionNode, null, false);
								throw new NotImplementedException("Functions with differing parameters are not handled yet.");
								//foreach (Project.ParamInfo param in
							}
							if (myFunction.ReturnType.FullName != theirFunction.ReturnType.FullName)
							{
								AddNodeBoth("ReturnType", theirFunction.ReturnType.FullName, myFunction.ReturnType.FullName, functionNode, null, true);
							}
							if (myFunction.ScriptLanguage != theirFunction.ScriptLanguage)
							{
								AddNodeBoth("ScriptLanguage", theirFunction.ScriptLanguage, myFunction.ScriptLanguage, functionNode, null, true);
							}
							if (myFunction.TemplateReturnLanguage != theirFunction.TemplateReturnLanguage)
							{
								AddNodeBoth("TemplateReturnLanguage", theirFunction.TemplateReturnLanguage, myFunction.TemplateReturnLanguage, functionNode, null, true);
							}
							numAdded++;
						}
						break;
					}
				}
				if (!found)
				{
					functionsTheirs.Add(theirFunction);
					//AddNodeTheirs(theirFunction.Name, "", "", null, new FunctionContainer(null, theirFunction, theirFunction.Name));
					numAdded++;
				}
			}
			foreach (FunctionInfo myFunction in frmTemplateSyncWizard.MyProject.Functions)
			{
				bool found = false;

				foreach (FunctionInfo theirFunction in frmTemplateSyncWizard.TheirProject.Functions)
				{
					if (Slyce.Common.Utility.StringsAreEqual(theirFunction.Name, myFunction.Name, false))
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					functionsMine.Add(myFunction);
					//AddNodeMine(myFunction.Name, "", "", null, new FunctionContainer(myFunction, null, myFunction.Name));
					numAdded++;
				}
			}
			if (functionsTheirs.Count > 0 || functionsMine.Count > 0)
			{
				TreeListNode singleRootNode = AddNodeTheirs("Functions in single template", "", "", null, "Single", false);

				if (functionsTheirs.Count > 0)
				{
					foreach (FunctionInfo func in functionsTheirs)
					{
						AddNodeTheirs("", func.Name, "", singleRootNode, new FunctionContainer(null, func, func.Name));
					}
				}
				if (functionsMine.Count > 0)
				{
					foreach (FunctionInfo func in functionsMine)
					{
						AddNodeMine("", "", func.Name, singleRootNode, new FunctionContainer(func, null, func.Name));
					}
				}
			}
			return numAdded;
		}

		private static bool FunctionsAreDifferent(FunctionInfo function1, FunctionInfo function2)
		{
			bool differenceExists = false;

			if (function1 != null && function2 != null)
			{
				if (function1.Body != function2.Body) { differenceExists = true; }
				if (function1.Category != function2.Category) { differenceExists = true; }
				if (function1.Description != function2.Description) { differenceExists = true; }
				if (function1.IsTemplateFunction != function2.IsTemplateFunction) { differenceExists = true; }
				if (!function1.ParametersAreEqual(function2.Parameters)) { differenceExists = true; }
				if (function1.ReturnType.FullName != function2.ReturnType.FullName) { differenceExists = true; }
				if (function1.ScriptLanguage != function2.ScriptLanguage) { differenceExists = true; }
				if (function1.TemplateReturnLanguage != function2.TemplateReturnLanguage) { differenceExists = true; }
			}
			return differenceExists;
		}

		private TreeListNode AddNodeBoth(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeBoth(name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeBoth(string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, BlueArrow, myValue, "" }, parentNode);
			}
			else
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			return node;
		}

		private TreeListNode AddNodeTheirs(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeTheirs(name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeTheirs(string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, GreenArrow, myValue, "" }, parentNode);
			}
			else
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.StateImageIndex = (int)TreeNodeImages.Unchecked;
			return node;
		}

		private TreeListNode AddNodeMine(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
		{
			return AddNodeMine(name, theirValue, myValue, parentNode, tag, true);
		}

		private TreeListNode AddNodeMine(string name, object theirValue, object myValue, TreeListNode parentNode, object tag, bool addActionText)
		{
			TreeListNode node;

			if (addActionText)
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, RemoveImage }, parentNode);
			}
			else
			{
				node = treeListReferencedFiles.AppendNode(new[] { name, theirValue, "", myValue, "" }, parentNode);
			}
			node.Tag = tag;
			node.StateImageIndex = (int)TreeNodeImages.Unchecked;
			return node;
		}

		public override void OnDisplaying()
		{
			Populate();
		}

		private void syntaxEditor1_UserMarginPaint(object sender, UserMarginPaintEventArgs e)
		{
		
		}

		/// <summary>
		/// Swap faded and syntax-highlighted text.
		/// </summary>
		public void SwitchFormatting()
		{
			UseSplitLanguage = !UseSplitLanguage;

			//if (syntaxEditor1.Document.Language.LexicalStates.Count > 1)
			//{
			syntaxEditorExternal.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.Language.BackColor = EDITOR_BACK_COLOR_NORMAL;//UseSplitLanguage ? EDITOR_BACK_COLOR_FADED : EDITOR_BACK_COLOR_NORMAL;
			//syntaxEditor1.Document.Language.BackColor = UseSplitLanguage ? EDITOR_BACK_COLOR_NORMAL : EDITOR_BACK_COLOR_FADED;
			syntaxEditorExternal.Refresh();
			//}
		}

		private void syntaxEditor1_ContextMenuRequested(object sender, ContextMenuRequestEventArgs e)
		{
			int lineNumber = syntaxEditorExternal.Views[0].LocationToPosition(e.MenuLocation, LocationToPositionAlgorithm.Block).Line;

			if (syntaxEditorExternal.Document.Lines[lineNumber].BackColor != Color.White &&
				syntaxEditorExternal.Document.Lines[lineNumber].BackColor != Color.Empty)
			{
				SelectLines(e.MenuLocation);
				syntaxEditorExternal.DefaultContextMenuEnabled = false;
				contextMenuStrip1.Show(syntaxEditorExternal, e.MenuLocation);
			}
			else
			{
				DeselectLines();
				syntaxEditorExternal.DefaultContextMenuEnabled = false;
			}
		}

		/// <summary>
		/// Determines whether the location is part of a special region, and highlights the special region
		/// if it is.
		/// </summary>
		/// <param name="location"></param>
		private void SelectLines(Point location)
		{
			int lineNumber = syntaxEditorExternal.Views[0].LocationToPosition(location, LocationToPositionAlgorithm.Block).Line;
			SelectLines(lineNumber);
		}

		/// <summary>
		/// Determines whether the location is part of a special region, and highlights the special region
		/// if it is.
		/// </summary>
		/// <param name="lineNumber"></param>
		private void SelectLines(int lineNumber)
		{
			DeselectLines();
			int spanStartLine = lineNumber;
			int spanEndLine = lineNumber;
			Color selectedColor = syntaxEditorExternal.Document.Lines[lineNumber].BackColor;
			bool newColourFound = false;

			for (int i = lineNumber; i >= 0; i--)
			{
				if (syntaxEditorExternal.Document.Lines[i].BackColor != selectedColor)// &&
				//syntaxEditor1.Document.Lines[i].BackColor.ToArgb() != 0)
				{
					newColourFound = true;
					break;
				}
				spanStartLine = i;
			}
			if (!newColourFound && spanStartLine != 0)
			{
				spanStartLine = lineNumber;
			}
			newColourFound = false;
			for (int i = lineNumber; i < syntaxEditorExternal.Document.Lines.Count; i++)
			{
				if (syntaxEditorExternal.Document.Lines[i].BackColor != selectedColor)// &&
				//syntaxEditor1.Document.Lines[i].BackColor.ToArgb() != 0)
				{
					newColourFound = true;
					break;
				}
				spanEndLine = i;
			}
			if (!newColourFound)
			{
				spanEndLine = lineNumber;
			}
			for (int i = spanStartLine; i <= spanEndLine; i++)
			{
				int startOffset = syntaxEditorExternal.Document.Lines[i].StartOffset;
				int endOffset = syntaxEditorExternal.Document.Lines[i].EndOffset;
				SpanIndicator[] spans = syntaxEditorExternal.Document.SpanIndicatorLayers.GetIndicatorsForTextRange(new TextRange(startOffset, endOffset), false);

				if (spans.Length > 0)
				{
					((HighlightingStyleSpanIndicator)spans[0]).HighlightingStyle.BackColor = Color.Red;
				}
				syntaxEditorExternal.Document.Lines[i].BackColor = Color.Red;
			}
			OrigianlLineSpan = new Slyce.IntelliMerge.SlyceMerge.LineSpan(spanStartLine, spanEndLine);//, selectedColor);
		}

		/// <summary>
		/// Removes highlighting of special regions.
		/// </summary>
		private void DeselectLines()
		{
			if (OrigianlLineSpan != null)
			{
				DeselectLines(OrigianlLineSpan.StartLine, OrigianlLineSpan.EndLine);
			}
		}

		/// <summary>
		/// Removes highlighting of special regions.
		/// </summary>
		private void DeselectLines(int startLine, int endLine)
		{
			Color color = Color.White;
			for (int i = startLine; i <= endLine; i++)
			{
				if (i >= syntaxEditorExternal.Document.Lines.Count)
				{
					break;
				}
				int startOffset = syntaxEditorExternal.Document.Lines[i].StartOffset;
				int endOffset = syntaxEditorExternal.Document.Lines[i].EndOffset;
				SpanIndicator[] spans = syntaxEditorExternal.Document.SpanIndicatorLayers.GetIndicatorsForTextRange(new TextRange(startOffset, endOffset), false);

				if (spans.Length > 0)
				{
					((HighlightingStyleSpanIndicator)spans[0]).HighlightingStyle.BackColor = color;
				}
				syntaxEditorExternal.Document.Lines[i].BackColor = color;
			}
			OrigianlLineSpan = null;
		}

		private void syntaxEditor1_DocumentTextChanged(object sender, DocumentModificationEventArgs e)
		{
			// Colour lines correctly when new lines inserted or existing lines deleted
			if (e.Modification.LinesInserted + e.Modification.LinesDeleted > 0)
			{
				int lineNum = e.Modification.StartLineIndex;// -1;

				if (e.Modification.LinesInserted > 0 && syntaxEditorExternal.Document.Lines[lineNum].BackColor == Color.Red &&
					syntaxEditorExternal.Document.Lines[lineNum + 1].BackColor != Color.Red)
				{
					DeselectLines();
				}
				if (e.Modification.LinesDeleted > 0 && OrigianlLineSpan != null)
				{
					if (OrigianlLineSpan.EndLine - OrigianlLineSpan.StartLine > 0)
					{
						OrigianlLineSpan.EndLine -= 1;
					}
					if (OrigianlLineSpan.EndLine == OrigianlLineSpan.StartLine)
					{
						DeselectLines(OrigianlLineSpan.StartLine, OrigianlLineSpan.EndLine);
						OrigianlLineSpan = null;
					}
					if (lineNum + 1 < syntaxEditorExternal.Document.Lines.Count &&
						(syntaxEditorExternal.Document.Lines[lineNum].BackColor == Color.White &&
							syntaxEditorExternal.Document.Lines[lineNum + 1].BackColor == Color.Red))
					{
						DeselectLines();
					}
				}
			}
		}

		private void mnuAccept_Click(object sender, EventArgs e)
		{
			// Remove all lines associated with strikethrough
			int startOffset = syntaxEditorExternal.Document.Lines[OrigianlLineSpan.StartLine].StartOffset;
			int endOffset = syntaxEditorExternal.Document.Lines[OrigianlLineSpan.EndLine].EndOffset;
			int originalStartLine = OrigianlLineSpan.StartLine;
			int originalEndLine = OrigianlLineSpan.EndLine;
			SpanIndicator[] spans = syntaxEditorExternal.Document.SpanIndicatorLayers.GetIndicatorsForTextRange(new TextRange(startOffset, endOffset), false);
			int numLinesDeleted = 0;

			foreach (SpanIndicator span in spans)
			{
				var ss = (HighlightingStyleSpanIndicator)span;
				int firstLineToDelete = syntaxEditorExternal.Views[0].OffsetToPosition(ss.TextRange.StartOffset).Line;
				int lastLineToDelete = syntaxEditorExternal.Views[0].OffsetToPosition(ss.TextRange.EndOffset).Line;

				for (int i = lastLineToDelete; i >= firstLineToDelete; i--)
				{
					syntaxEditorExternal.Document.Lines.RemoveAt(i);
					numLinesDeleted++;
				}
			}
			OrigianlLineSpan = null;
			DeselectLines(originalStartLine, originalEndLine - numLinesDeleted + 1);
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			// Remove all lines associated with strikethrough
			int startOffset = syntaxEditorExternal.Document.Lines[OrigianlLineSpan.StartLine].StartOffset;
			int endOffset = syntaxEditorExternal.Document.Lines[OrigianlLineSpan.EndLine].EndOffset;
			SpanIndicator[] spans = syntaxEditorExternal.Document.SpanIndicatorLayers.GetIndicatorsForTextRange(new TextRange(startOffset, endOffset), false);
			var linesToKeep = new List<int>();

			foreach (SpanIndicator span in spans)
			{
				var ss = (HighlightingStyleSpanIndicator)span;
				int firstLineToKeep = syntaxEditorExternal.Views[0].OffsetToPosition(ss.TextRange.StartOffset).Line;
				int lastLineToKeep = syntaxEditorExternal.Views[0].OffsetToPosition(ss.TextRange.EndOffset).Line;

				for (int line = firstLineToKeep; line <= lastLineToKeep; line++)
				{
					linesToKeep.Add(line);
				}
			}
			linesToKeep.Sort();
			// Remove any remaining lines before the first strikethrough
			int originalStartLine = OrigianlLineSpan.StartLine;
			int originalEndLine = OrigianlLineSpan.EndLine;
			int numLinesRemoved = 0;

			for (int i = originalEndLine; i >= originalStartLine; i--)
			{
				if (linesToKeep.BinarySearch(i) < 0)
				{
					syntaxEditorExternal.Document.Lines.RemoveAt(i);
					numLinesRemoved++;
				}
			}
			// Remove strikethrough from all spans in the block
			for (int spanCounter = spans.Length - 1; spanCounter >= 0; spanCounter--)
			{
				SpanIndicator span = spans[spanCounter];
				span.Layer.Remove(span);
			}
			OrigianlLineSpan = null;
			DeselectLines(originalStartLine, originalEndLine - numLinesRemoved);
		}

		private void syntaxEditor1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				DeselectLines();
			}
		}

		private void treeListReferencedFiles_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
		{
			int columnIndex = e.Column.AbsoluteIndex;

			if (columnIndex == 2 || columnIndex == 4)
			{
				if (typeof(Bitmap).IsInstanceOfType(e.Node.GetValue(columnIndex)))
				{
					e.RepositoryItem = repositoryItemPictureEdit4;
				}
				else
				{
					e.RepositoryItem = EmptyRepositoryItem;
				}
			}
		}

		private void treeListReferencedFiles_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
		{
			if (e.Node.ParentNode == null)
			{
				e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
				SetFocusedNodeProperties(e);
			}
		}

		private static void SetFocusedNodeProperties(DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
		{
			double brightness = Slyce.Common.Colors.GetBrightness(e.Appearance.BackColor);
			double lightBrightness = brightness > 0.5 ? brightness - 0.2 : brightness + 0.1;
			double darkBrightness = brightness > 0.5 ? brightness - 0.6 : brightness - 0.4;

			if (lightBrightness > 1) { lightBrightness = 1; }
			if (darkBrightness < 0) { darkBrightness = 0; }

			Color lightColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, lightBrightness);
			Color darkColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, darkBrightness);

			e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(darkColor);
			e.Appearance.BackColor = lightColor;
			e.Appearance.BackColor2 = darkColor;
			e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			e.Appearance.Options.UseBackColor = true;
		}

		private void treeListReferencedFiles_MouseDown(object sender, MouseEventArgs e)
		{
			DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeListReferencedFiles.CalcHitInfo(new Point(e.X, e.Y));
			TreeListNode node = hInfo.Node;

			if (node == null ||
				hInfo.Column == null ||
				!(hInfo.Column.AbsoluteIndex == 2 || hInfo.Column.AbsoluteIndex == 4))
			{
				return;
			}
			TreeListNode topNode = node;

			while (topNode.ParentNode != null)
			{
				topNode = topNode.ParentNode;
			}
			treeListReferencedFiles.FocusedNode = node;
			string action;

			switch (hInfo.Column.AbsoluteIndex)
			{
				case 2:
					action = node.GetValue(2).ToString();

					if (!string.IsNullOrEmpty(action) && action == "System.Drawing.Bitmap")
					{
						object image = node.GetValue(2);

						if (image == GreenArrow)
						{
							action = "Import >";
						}
						else if (image == BlueArrow)
						{
							action = "Apply Change >";
						}
					}
					break;
				case 4:
					action = node.GetValue(4).ToString();

					if (!string.IsNullOrEmpty(action) && action == "System.Drawing.Bitmap")
					{
						action = "Remove";
					}
					break;
				default:
					return;
			}
			if (string.IsNullOrEmpty(action))
			{
				return;
			}
			var topNodeText = (string)topNode.GetValue(0);

			switch (action)
			{
				case "Import >":
					switch (topNodeText)
					{
						case "Functions in single template":
							frmTemplateSyncWizard.MyProject.AddFunction(((FunctionContainer)node.Tag).TheirFunction);
							break;
						default:
							throw new NotImplementedException("Not coded yet: " + topNodeText);
					}
					break;
				case "Apply Change >":
					switch (topNodeText)
					{
						default:
							throw new NotImplementedException("Not coded yet: " + topNodeText);
					}
				case "Remove":
					switch (topNodeText)
					{
						case "Functions in single template":
							frmTemplateSyncWizard.MyProject.DeleteFunction(((FunctionContainer)node.Tag).MyFunction);
							break;
						default:
							throw new NotImplementedException("Not coded yet: " + topNodeText);
					}
					break;
				default:
					throw new NotImplementedException("ActionType not handled yet: " + action);
			}
			// Do any siblings remain?
			while (node.ParentNode != null)
			{
				if (node.ParentNode.Nodes.Count == 1)
				{
					node = node.ParentNode;
				}
				else // The parent has > 1 child
				{
					node.ParentNode.Nodes.Remove(node);
					return;
				}

			}
			treeListReferencedFiles.Nodes.Remove(node);
		}

		private void treeListReferencedFiles_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
			TreeListNode parentNode = e.Node;

			while (parentNode.ParentNode != null && (parentNode.Tag == null || parentNode.Tag.GetType() != typeof(FunctionContainer)))
			{
				parentNode = parentNode.ParentNode;
			}
			if (parentNode.Tag != null && parentNode.Tag.GetType() == typeof(FunctionContainer))
			{
				var funcContainer = (FunctionContainer)parentNode.Tag;

				if (funcContainer == FunctionContainerDisplayedInEditors)
				{
					// We are already displaying this function. Don't redisplay as we'll cause a flicker and loose the current line
					return;
				}
				ClearApplyButtons();
				ClearDeleteButtons();
				FunctionContainerDisplayedInEditors = funcContainer;

				if (funcContainer.MyFunction != null && funcContainer.TheirFunction != null)
				{
					if (funcContainer.MyFunction.Body == funcContainer.TheirFunction.Body)
					{
						syntaxEditorCurrent.Visible = false;
						syntaxEditorExternal.Visible = false;
						ucHeading1.Text = string.Format("{0}: no code differences", funcContainer.MyFunction.Name);
						buttonResolve.Visible = false;
					}
					else
					{
						ucHeading1.Text = string.Format("{0}: code", funcContainer.MyFunction.Name);
						string bodyExternal = funcContainer.TheirFunction.Body;
						string bodyCurrent = funcContainer.MyFunction.Body;

						BusyPopulatingEditors = true;
						syntaxEditorCurrent.SuspendPainting();
						syntaxEditorExternal.SuspendPainting();
						Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInTwoEditors(syntaxEditorExternal, syntaxEditorCurrent, ref bodyExternal, ref bodyCurrent);
						BusyPopulatingEditors = false;
						buttonResolve.Visible = true;

						if (syntaxEditorCurrent.Document.Lines.Count > 0)
						{
							syntaxEditorCurrent.SelectedView.FirstVisibleDisplayLineIndex = 0;
						}
						if (syntaxEditorExternal.Document.Lines.Count > 0)
						{
							syntaxEditorExternal.SelectedView.FirstVisibleDisplayLineIndex = 0;
						}
						syntaxEditorCurrent.ResumePainting();
						syntaxEditorExternal.ResumePainting();
						AddApplyButtons();
						AddDeleteButtons();
						SetLineNumbers();
						syntaxEditorCurrent.Visible = true;
						syntaxEditorExternal.Visible = true;
					}
				}
				else if (funcContainer.MyFunction == null && funcContainer.TheirFunction != null)
				{
					syntaxEditorExternal.Text = funcContainer.TheirFunction.Body;
					syntaxEditorCurrent.Visible = false;
					syntaxEditorExternal.Visible = true;
					ucHeading1.Text = string.Format("{0}: no code differences", funcContainer.TheirFunction.Name);
					buttonResolve.Visible = false;
				}
				else if (funcContainer.MyFunction != null && funcContainer.TheirFunction == null)
				{
					syntaxEditorCurrent.Text = funcContainer.MyFunction.Body;
					syntaxEditorCurrent.Visible = true;
					syntaxEditorExternal.Visible = false;
					ucHeading1.Text = string.Format("{0}: no code differences", funcContainer.MyFunction.Name);
					buttonResolve.Visible = false;
				}
				else
				{
					syntaxEditorCurrent.Visible = false;
					syntaxEditorExternal.Visible = false;
					ucHeading1.Text = "No function selected";
					buttonResolve.Visible = false;
				}

			}
		}

		private void LayoutEditors()
		{
			const int gap = 5;
			int editorWidth = (splitContainerMain.Panel2.ClientSize.Width - gap * 2 - ImageWidth * 2) / 2;
			int editorHeight = splitContainerMain.Panel2.ClientSize.Height - syntaxEditorCurrent.Top - gap;

			syntaxEditorExternal.Anchor = AnchorStyles.None;
			syntaxEditorCurrent.Anchor = AnchorStyles.None;

			syntaxEditorExternal.Left = gap;
			syntaxEditorExternal.Width = editorWidth;
			syntaxEditorExternal.Height = editorHeight;

			syntaxEditorCurrent.Left = syntaxEditorExternal.Right + ImageWidth;
			syntaxEditorCurrent.Width = editorWidth;
			syntaxEditorCurrent.Height = editorHeight;
		}

		private void splitContainerMain_Panel2_Resize(object sender, EventArgs e)
		{
			LayoutEditors();
		}

		private void syntaxEditorExternal_ViewVerticalScroll(object sender, EditorViewEventArgs e)
		{
			if (!BusyPopulatingEditors)
			{
				ClearApplyButtons();
				syntaxEditorCurrent.SelectedView.FirstVisibleDisplayLineIndex = syntaxEditorExternal.SelectedView.FirstVisibleDisplayLineIndex;
				AddApplyButtons();
			}
		}

		private void syntaxEditorCurrent_ViewVerticalScroll(object sender, EditorViewEventArgs e)
		{
			if (!BusyPopulatingEditors)
			{
				ClearDeleteButtons();
				syntaxEditorExternal.SelectedView.FirstVisibleDisplayLineIndex = syntaxEditorCurrent.SelectedView.FirstVisibleDisplayLineIndex;
				AddDeleteButtons();
			}
		}

		private void ClearApplyButtons()
		{
			for (int i = ApplyButtons.Count - 1; i >= 0; i--)
			{
				splitContainerMain.Panel2.Controls.Remove(ApplyButtons[i]);
			}
			ApplyButtons.Clear();
		}

		private void ClearDeleteButtons()
		{
			for (int i = DeleteButtons.Count - 1; i >= 0; i--)
			{
				splitContainerMain.Panel2.Controls.Remove(DeleteButtons[i]);
			}
			DeleteButtons.Clear();
		}

		private void AddApplyButtons()
		{
			ClearApplyButtons();
			int gap = 2;
			int firstDisplayLine = syntaxEditorExternal.SelectedView.FirstVisibleDisplayLineIndex;
			int lineHeight = syntaxEditorExternal.SelectedView.DisplayLineHeight;

			for (int i = syntaxEditorExternal.SelectedView.DisplayLines.Count - 1; i >= firstDisplayLine; i--)
			{
				if (syntaxEditorExternal.Document.Lines[i].BackColor != Color.Empty)
				{
					while (i > 0 && syntaxEditorExternal.Document.Lines[i - 1].BackColor != Color.Empty &&
						i > firstDisplayLine)
					{
						i--;
					}
					// Now the get the first real line of colour
					int realFirstLine = i;

					while (realFirstLine > 0 && syntaxEditorExternal.Document.Lines[realFirstLine - 1].BackColor != Color.Empty)
					{
						realFirstLine--;
					}
					if (syntaxEditorExternal.Document.Lines[i].BackColor != Color.LightGray)
					{
						int numLinesOffset = i - firstDisplayLine;
						var button = new Button();
						button.Width = ImageWidth - gap * 2;
						button.Left = syntaxEditorExternal.Right + gap;
						button.Top = syntaxEditorExternal.Top + numLinesOffset * lineHeight;
						button.Visible = true;
						button.Tag = realFirstLine;

						if (syntaxEditorCurrent.Document.Lines[i].BackColor == Color.LightGray)
						{
							button.Image = GreenArrow;
							toolTip1.SetToolTip(button, "Insert >");
						}
						else
						{
							button.Image = BlueArrow;
							toolTip1.SetToolTip(button, "Overwrite >");
						}
						button.ImageAlign = ContentAlignment.MiddleCenter;
						button.Click += ApplyButton_Click;
						splitContainerMain.Panel2.Controls.Add(button);
						ApplyButtons.Add(button);
					}
				}
			}
		}

		private void SetLineNumbers()
		{
			int lineNumber = 1;

			foreach (DocumentLine line in syntaxEditorCurrent.Document.Lines)
			{
				if (line.BackColor != Color.LightGray)
				{
					line.CustomLineNumber = lineNumber.ToString();
					lineNumber++;
				}
				else
				{
					line.CustomLineNumber = string.Empty;
				}
			}
			lineNumber = 1;
			foreach (DocumentLine line in syntaxEditorExternal.Document.Lines)
			{
				if (line.BackColor != Color.LightGray)
				{
					line.CustomLineNumber = lineNumber.ToString();
					lineNumber++;
				}
				else
				{
					line.CustomLineNumber = string.Empty;
				}
			}
		}

		private void AddDeleteButtons()
		{
			ClearDeleteButtons();
			const int gap = 2;
			int firstDisplayLine = syntaxEditorCurrent.SelectedView.FirstVisibleDisplayLineIndex;
			int lineHeight = syntaxEditorCurrent.SelectedView.DisplayLineHeight;

			for (int i = syntaxEditorCurrent.SelectedView.DisplayLines.Count - 1; i >= firstDisplayLine; i--)
			{
				if (syntaxEditorCurrent.Document.Lines[i].BackColor != Color.Empty)
				{
					while (i > 0 && syntaxEditorCurrent.Document.Lines[i - 1].BackColor != Color.Empty &&
						i > firstDisplayLine)
					{
						i--;
					}
					if (syntaxEditorCurrent.Document.Lines[i].BackColor != Color.LightGray &&
						syntaxEditorExternal.Document.Lines[i].BackColor == Color.LightGray)
					{
						// Now the get the first real line of colour
						int realFirstLine = i;

						while (realFirstLine > 0 && syntaxEditorCurrent.Document.Lines[realFirstLine - 1].BackColor != Color.Empty)
						{
							realFirstLine--;
						}
						int numLinesOffset = i - firstDisplayLine;
						var button = new Button
						                 {
						                     Width = (ImageWidth - gap*2),
						                     Left = (syntaxEditorCurrent.Right + gap),
						                     Top = (syntaxEditorCurrent.Top + numLinesOffset*lineHeight),
						                     Visible = true,
						                     Tag = realFirstLine,
						                     Image = RemoveImage,
						                     ImageAlign = ContentAlignment.MiddleCenter
						                 };
					    button.Click += DeleteButton_Click;
                        toolTip1.SetToolTip(button, "Delete");
						splitContainerMain.Panel2.Controls.Add(button);
						DeleteButtons.Add(button);
					}
				}
			}
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var firstLine = (int)button.Tag;
			syntaxEditorCurrent.SelectedView.DisplayLines[firstLine].DocumentLine.BackColor = Color.Red;

			// Find the last line
			int lastLineOfContent = firstLine;

		    while (syntaxEditorExternal.Document.Lines[lastLineOfContent + 1].BackColor != Color.Empty &&
				syntaxEditorExternal.Document.Lines[lastLineOfContent + 1].BackColor != Color.LightGray)
			{
				lastLineOfContent++;
			}
			int lastLine = lastLineOfContent;

			while (syntaxEditorCurrent.Document.Lines[lastLine + 1].BackColor != Color.Empty)
			{
				lastLine++;
			}
			for (int i = firstLine; i <= lastLineOfContent; i++)
			{
				syntaxEditorCurrent.Document.Lines[i].Text = syntaxEditorExternal.Document.Lines[i].Text;
				syntaxEditorCurrent.Document.Lines[i].BackColor = Color.Empty;
				syntaxEditorExternal.Document.Lines[i].BackColor = Color.Empty;
			}
			// Delete all 'gray' lines
			for (int i = lastLine; i > lastLineOfContent; i--)
			{
				syntaxEditorCurrent.Document.Lines[i].BackColor = syntaxEditorCurrent.Document.Lines[i + 1].BackColor;
				syntaxEditorCurrent.Document.Lines.RemoveAt(i);

				syntaxEditorExternal.Document.Lines[i].BackColor = syntaxEditorExternal.Document.Lines[i + 1].BackColor;
				syntaxEditorExternal.Document.Lines.RemoveAt(i);
			}
			splitContainerMain.Panel2.Controls.Remove(button);
			DeleteButtons.Remove(button);
			AddApplyButtons();
			AddDeleteButtons();
			SetLineNumbers();
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var firstLine = (int)button.Tag;
			syntaxEditorCurrent.SelectedView.DisplayLines[firstLine].DocumentLine.BackColor = Color.Red;

			// Find the last line
			int lastLine = firstLine;

			while (syntaxEditorCurrent.Document.Lines[lastLine + 1].BackColor != Color.Empty)
			{
				lastLine++;
			}
			for (int i = lastLine; i >= firstLine; i--)
			{
				syntaxEditorCurrent.Document.Lines[i].BackColor = syntaxEditorCurrent.Document.Lines[i + 1].BackColor;
				syntaxEditorCurrent.Document.Lines.RemoveAt(i);

				syntaxEditorExternal.Document.Lines[i].BackColor = syntaxEditorExternal.Document.Lines[i + 1].BackColor;
				syntaxEditorExternal.Document.Lines.RemoveAt(i);
			}
			splitContainerMain.Panel2.Controls.Remove(button);
			DeleteButtons.Remove(button);
			AddApplyButtons();
			AddDeleteButtons();
			SetLineNumbers();
		}

		private void buttonResolve_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			TreeListNode parentNode = treeListReferencedFiles.FocusedNode;

			while (parentNode.ParentNode != null && (parentNode.Tag == null || parentNode.Tag.GetType() != typeof(FunctionContainer)))
			{
				parentNode = parentNode.ParentNode;
			}
			FunctionContainer funcContainer = FunctionContainerDisplayedInEditors;
			syntaxEditorCurrent.SuspendPainting();

			for (int i = syntaxEditorCurrent.Document.Lines.Count - 1; i >= 0; i--)
			{
				if (syntaxEditorCurrent.Document.Lines[i].BackColor == Color.LightGray)
				{
					syntaxEditorCurrent.Document.Lines.RemoveAt(i);
				}
			}
			funcContainer.MyFunction.Body = funcContainer.TheirFunction.Body = syntaxEditorCurrent.Text;

			if (FunctionsAreDifferent(funcContainer.MyFunction, funcContainer.TheirFunction))
			{
				// Don't delete node, just remove the 'code differences' text
				parentNode.SetValue(1, "");
				syntaxEditorCurrent.Visible = false;
				syntaxEditorExternal.Visible = false;
				ucHeading1.Text = string.Format("{0}: no code differences", funcContainer.MyFunction.Name);
			}
			else
			{
				TreeListNode topLevelNode = parentNode.ParentNode;
				int nodeIndex = topLevelNode.Nodes.IndexOf(parentNode);

				// Remove the node
				if (topLevelNode.Nodes.Count > nodeIndex)
				{
					treeListReferencedFiles.FocusedNode = topLevelNode.Nodes[nodeIndex];
				}
				else if (topLevelNode.Nodes.Count > 1)
				{
					treeListReferencedFiles.FocusedNode = topLevelNode.Nodes[nodeIndex - 1];
				}
				else // This was the only node in the tree
				{
					treeListReferencedFiles.FocusedNode = topLevelNode;
				}
				topLevelNode.Nodes.Remove(parentNode);
			}
			syntaxEditorCurrent.ResumePainting();
			Cursor = Cursors.Default;
		}

		private void syntaxEditorExternal_Resize(object sender, EventArgs e)
		{
			AddApplyButtons();
		}

		private void syntaxEditorCurrent_Resize(object sender, EventArgs e)
		{
			AddDeleteButtons();
		}

		private void btnTest_Click(object sender, EventArgs e)
		{
			string bodyExternal = Slyce.Common.Utility.ReadTextFile(@"C:\test1.txt");
			string bodyCurrent = Slyce.Common.Utility.ReadTextFile(@"C:\test2.txt");

			BusyPopulatingEditors = true;
			syntaxEditorCurrent.SuspendPainting();
			syntaxEditorExternal.SuspendPainting();
			Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInTwoEditors(syntaxEditorExternal, syntaxEditorCurrent, ref bodyExternal, ref bodyCurrent);
			BusyPopulatingEditors = false;

			if (syntaxEditorCurrent.Document.Lines.Count > 0)
			{
				syntaxEditorCurrent.SelectedView.FirstVisibleDisplayLineIndex = 0;
			}
			if (syntaxEditorExternal.Document.Lines.Count > 0)
			{
				syntaxEditorExternal.SelectedView.FirstVisibleDisplayLineIndex = 0;
			}
			syntaxEditorCurrent.ResumePainting();
			syntaxEditorExternal.ResumePainting();
			syntaxEditorCurrent.Visible = true;
			syntaxEditorExternal.Visible = true;
			AddApplyButtons();
			AddDeleteButtons();
			SetLineNumbers();
		}

		private void ScreenFunctions_Resize(object sender, EventArgs e)
		{
			buttonResolve.Left = Right - buttonResolve.Width - 5;
		}
	}
}
