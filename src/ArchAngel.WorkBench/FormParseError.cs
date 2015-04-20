using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.Providers.CodeProvider;
using Slyce.Common;

namespace ArchAngel.Workbench
{
    public partial class FormParseError : Form
    {
        private List<ArchAngel.Providers.CodeProvider.ParserSyntaxError> Errors;

        public FormParseError()
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading2.Text = "";
            Controller.Instance.ShadeMainForm();
        }

        public string Code
        {
            get { return syntaxEditor1.Text; }
        }

		public void ShowParseErrors(ParserSyntaxError error)
        {
            listBoxFiles.Items.Add(Path.GetFileName(error.FileName));
            ShowParseError(error);
            this.TopMost = true;
            this.ShowDialog(this.ParentForm);
        }

        public void ShowParseErrors(List<ParserSyntaxError> errors)
        {
            Errors = errors;

			foreach (ParserSyntaxError error in Errors)
            {
                listBoxFiles.Items.Add(Path.GetFileName(error.Filename));
            }
            ShowParseError(Errors[0]);
            this.TopMost = true;
            this.ShowDialog(this.ParentForm);
        }

		private void ShowParseError(ParserSyntaxError error)
        {
            this.Text = "Parse Error: " + error.Filename;
            label1.Text = string.Format("{0} could not be parsed. There seems to be a syntax error near line {1}, column {2}. Please fix the error before trying to parse again. If you believe the code is syntactically correct then please send a copy of the file to support@slyce.com", error.FileName, error.LineNumber, error.StartPos);
            SetSyntaxLanguage();
            syntaxEditor1.Text = error.OriginalText;
            MarkErrorWord(syntaxEditor1, error.LineNumber, error.StartPos, "Parse Error");
        }

        private void MarkErrorWord(ActiproSoftware.SyntaxEditor.SyntaxEditor editor, int lineNumber, int characterPos, string message)
        {
            //string text = editor.Document.Lines[lineNumber].Text;
            //string preceedingText = characterPos <= compileText.Length ? compileText.Substring(0, characterPos) : "";

            ActiproSoftware.SyntaxEditor.DocumentPosition position = new ActiproSoftware.SyntaxEditor.DocumentPosition(lineNumber, characterPos);
            int offset = editor.Document.PositionToOffset(position);
            DynamicToken token = (DynamicToken)editor.Document.Tokens.GetTokenAtOffset(offset);
            ActiproSoftware.SyntaxEditor.SpanIndicator indicator = new ActiproSoftware.SyntaxEditor.WaveLineSpanIndicator("ErrorIndicator", Color.Red);
            indicator.Tag = message;
            ActiproSoftware.SyntaxEditor.SpanIndicatorLayer indicatorLayer = new ActiproSoftware.SyntaxEditor.SpanIndicatorLayer("kk", 1);
            editor.Document.SpanIndicatorLayers.Add(indicatorLayer);
            int startOffset = Math.Min(token.StartOffset, indicatorLayer.Document.Length - 1);
            int length = Math.Max(token.Length, 1);
            indicatorLayer.Add(indicator, startOffset, length);

            syntaxEditor1.Document.Lines[lineNumber].BackColor = Slyce.Common.Colors.BackgroundColor;
            syntaxEditor1.SelectedView.GoToLine(lineNumber, (lineNumber > 2) ? 2 : 0); // Allow 2 blank lines above selection
        }

        private void SetSyntaxLanguage()
        {
            TemplateContentLanguage language = TemplateContentLanguage.CSharp;
            string normalFilePath = Slyce.Common.SyntaxEditorHelper.GetLanguageFileName(language);
            normalFilePath = Path.Combine(Path.GetTempPath(), normalFilePath);

            if (!File.Exists(normalFilePath))
            {
                MessageBox.Show("Xml language syntax file could not be loaded: " + normalFilePath);
                return;
            }
            syntaxEditor1.Document.LoadLanguageFromXml(normalFilePath, 0);
        }

        private void buttonClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArchAngel.Providers.ParseError error = Errors[listBoxFiles.SelectedIndex];
            ShowParseError(error);
        }

        private void FormParseError_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.Instance.UnshadeMainForm();
        }

    }
}