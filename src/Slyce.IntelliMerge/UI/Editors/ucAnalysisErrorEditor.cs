using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.Providers.CodeProvider;
using DevComponents.AdvTree;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI.Editors
{
	public partial class ucAnalysisErrorEditor : UserControl
	{
		private readonly List<ParserSyntaxError> errors = new List<ParserSyntaxError>();
		private const string ErrorLayerKey = "error-layer";
		private IProjectFile<string> file;

		public ucAnalysisErrorEditor()
		{
			InitializeComponent();
			errorTreeList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
		}

		public ucAnalysisErrorEditor(IProjectFile<string> file, TemplateContentLanguage language)
		{
			InitializeComponent();

            Reset(file, language);
		}

        public void Reset(IProjectFile<string> file, TemplateContentLanguage language)
        {
            if (file.HasContents == false)
                throw new ArgumentException("file must have contents");
            
            this.file = file;
            editor.Text = file.GetContents();
            editor.Document.Language = SyntaxEditorHelper.GetDynamicLanguage(language);
            errorTreeList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            Reparse();
        }

		public void AddError(ParserSyntaxError parseError)
		{
			errors.Add(parseError);
		}

		public void Reparse()
		{
			ClearErrors();

			CSharpParser formatter = new CSharpParser();
			formatter.ParseCode(editor.Text);
			if(formatter.ErrorOccurred)
			{
				AddAllErrors(formatter.SyntaxErrors);
			}
			DisplayErrors();
		}

		private void AddAllErrors(IEnumerable<ParserSyntaxError> errorList)
		{
			foreach(ParserSyntaxError error in errorList)
			{
				AddError(error);
			}
		}

		private void DisplayErrors()
		{
			if(errors.Count > 0)
			{
				foreach (ParserSyntaxError error in errors)
				{
					AddErrorToToolWindow(error);
					MarkError(error);
				}
				errorToolWindow.Activate();
				errorToolWindow.State = ActiproSoftware.UIStudio.Dock.ToolWindowState.DockableInsideHost;
				errorTreeList.SelectedNode = errorTreeList.Nodes[0];
				errorTreeList_DoubleClick(null, new EventArgs());
			}
		}

		private void ClearErrors()
		{
			errors.Clear();
			errorTreeList.Nodes.Clear();
			SpanIndicatorLayer indicatorLayer = editor.Document.SpanIndicatorLayers[ErrorLayerKey];
			if (indicatorLayer != null)
			{
				indicatorLayer.Clear();
			}
		}

		private void AddErrorToToolWindow(ParserSyntaxError error)
		{
			Node node = new Node();

			errorTreeList.Nodes.Add(node);

			node.Cells.Add(new Cell());
			node.Cells.Add(new Cell());
			
			node.Text = error.LineNumber.ToString();
			node.Cells[1].Text = error.StartOffset.ToString();
			node.Cells[2].Text = error.ErrorMessage;

			errorToolWindow.Activate();
			errorToolWindow.State = ActiproSoftware.UIStudio.Dock.ToolWindowState.DockableInsideHost;
		}

		private void MarkError(ParserSyntaxError error)
		{            
			//DocumentPosition pos = ;
			int offset = error.StartOffset == -1 ? editor.Document.PositionToOffset(new DocumentPosition(error.LineNumber, 0)) : error.StartOffset;
			DynamicToken token = (DynamicToken)editor.Document.Tokens.GetTokenAtOffset(offset);
			SpanIndicatorLayer indicatorLayer = editor.Document.SpanIndicatorLayers[ErrorLayerKey];
			SpanIndicator indicator = new WaveLineSpanIndicator("ErrorIndicator", Color.Red);
			if (indicatorLayer == null)
			{
				indicatorLayer = new SpanIndicatorLayer(ErrorLayerKey, 1);
				editor.Document.SpanIndicatorLayers.Add(indicatorLayer);
			}
			int startOffset = Math.Min(token.StartOffset, indicatorLayer.Document.Length - 1);
			int length = Math.Max(token.Length, 1);

			if (startOffset < 0)
				return; // don't add indicators for errors without an offset.

			SpanIndicator[] indicators = indicatorLayer.GetIndicatorsForTextRange(new ActiproSoftware.SyntaxEditor.TextRange(startOffset, startOffset + length));
			foreach (SpanIndicator i in indicators)
			{
				// If there is already an error indicator on that word, don't add another one.
				if (i.TextRange.StartOffset == startOffset && i.TextRange.Length == length)
					continue;
			}
			indicatorLayer.Add(indicator, startOffset, length);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Reparse();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			file.ReplaceContents(editor.Text, false);
		}

		private void errorTreeList_DoubleClick(object sender, EventArgs e)
		{
			if (errorTreeList.SelectedNode == null) return;

			int lineNum = int.Parse(errorTreeList.SelectedNode.Text);

			editor.Caret.Offset = editor.Document.PositionToOffset(new DocumentPosition(lineNum, 0));
			editor.SelectedView.ScrollLineToVisibleMiddle();
		}
	}
}