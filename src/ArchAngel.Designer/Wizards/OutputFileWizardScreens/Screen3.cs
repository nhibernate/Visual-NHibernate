using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.Common.DesignerProject;
using Slyce.Common;

namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
	public partial class Screen3 : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
	{
		private char? TypedChar;

		public Screen3()
		{
			InitializeComponent();

			this.HasPrev = true;
			this.EnterKeyTriggersNext = false;

			SetPageOptions();
			SetupSyntaxEditor();
		}

		private void SetPageOptions()
		{
			switch (frmOutputFileWizard.FileType)
			{
				case frmOutputFileWizard.FileTypes.Folder:
					this.HasNext = false;
					this.HasFinish = true;
					this.NextText = "&Finish";
					this.PageHeader = "Create Folder Name";
					this.PageDescription = "Specify how you want the folder to be named. You can incorporate any property of the iterator in the name.";
					lblHeading.Text = "Folder name template:";
					lblExample.Text = "Place iterator properties between hash symbols (#) eg: My#iterator.Name#Folder";
					break;
				case frmOutputFileWizard.FileTypes.Script:
					this.HasNext = true;
					this.HasFinish = false;
					this.PageHeader = "Create Filename";
					this.PageDescription = "Specify how you want the file to be named. You can incorporate any property of the iterator in the name.";
					lblHeading.Text = "Filename template:";
					lblExample.Text = "Place iterator properties between hash symbols (#) eg: My#iterator.Name#Data.txt";
					break;
				case frmOutputFileWizard.FileTypes.Static:
					this.HasNext = true;
					this.HasFinish = false;
					this.NextText = "&Next";
					this.PageHeader = "Create Filename";
					this.PageDescription = "Specify how you want the file to be named. You can incorporate any property of the iterator in the name.";
					lblHeading.Text = "Filename template:";
					lblExample.Text = "Place iterator properties between hash symbols (#) eg: My#iterator.Name#Data.txt";
					break;
				default: throw new NotImplementedException("Not coded yet.");
			}
		}

		private void SetupSyntaxEditor()
		{
			syntaxEditorFilename.AcceptsTab = true;
			syntaxEditorFilename.HideSelection = true;
			syntaxEditorFilename.IndicatorMarginVisible = false;
			syntaxEditorFilename.LineNumberMarginVisible = false;
			syntaxEditorFilename.Document.Outlining.Mode = OutliningMode.None;
			syntaxEditorFilename.SelectionMarginWidth = 0;
			CreateDirectiveXmlToCSharpLanguage();
		}

		public override void OnDisplaying()
		{
			SetPageOptions();
			Populate();

			syntaxEditorFilename.Text = frmOutputFileWizard.FileName;

			if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Static &&
				string.IsNullOrEmpty(frmOutputFileWizard.FileName))
			{
				syntaxEditorFilename.Text = frmOutputFileWizard.StaticFileName;
			}
			SetInitialFocus();
			base.OnDisplaying();
		}

		private void SetInitialFocus()
		{
			syntaxEditorFilename.Focus();

			if (syntaxEditorFilename.Views.Count > 0)
			{
				syntaxEditorFilename.Views[0].Selection.SelectAll();
			}
		}

		private void Populate()
		{
			if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Script)
			{
				syntaxEditorFilename.Text = frmOutputFileWizard.FileName;
				this.NextText = "&Next >";
			}
			else if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Static)
			{
				syntaxEditorFilename.Text = frmOutputFileWizard.FileName;
				this.NextText = "&Next";
			}
			else
			{
				this.NextText = "&Finish";
			}
		}

		public override bool Back()
		{
			frmOutputFileWizard.FileName = syntaxEditorFilename.Text;
			return true;
		}

		public override bool Next()
		{
			if (string.IsNullOrEmpty(syntaxEditorFilename.Text))
			{
				if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Folder)
				{
					MessageBox.Show(this, "No folder name has been specified.", "Missing folder name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					MessageBox.Show(this, "No filename has been specified.", "Missing filename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				SetInitialFocus();
				return false;
			}
			frmOutputFileWizard.FileName = syntaxEditorFilename.Text;
			return true;
		}

		public override bool Save()
		{
			if (string.IsNullOrEmpty(syntaxEditorFilename.Text))
			{
				if (frmOutputFileWizard.FileType == frmOutputFileWizard.FileTypes.Folder)
				{
					MessageBox.Show(this, "No folder name has been specified.", "Missing folder name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					MessageBox.Show(this, "No filename has been specified.", "Missing filename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				SetInitialFocus();
				return false;
			}
			frmOutputFileWizard.FileName = syntaxEditorFilename.Text;
			return true;
		}

		/// <summary>
		/// Loads two languages and creates a state transition from XML to C# within ASP-style directives.
		/// </summary>
		private void CreateDirectiveXmlToCSharpLanguage()
		{
			string outputSyntaxFilePath = Slyce.Common.SyntaxEditorHelper.GetLanguageFileName(TemplateContentLanguage.PlainText);
			string scriptSyntaxFilePath = Slyce.Common.SyntaxEditorHelper.GetLanguageFileName(TemplateContentLanguage.CSharp);

			if (!File.Exists(outputSyntaxFilePath))
			{
				MessageBox.Show(this, "Language syntax file could not be loaded: " + outputSyntaxFilePath);
				return;
			}
			if (!File.Exists(scriptSyntaxFilePath))
			{
				MessageBox.Show(this, "Language syntax file could not be loaded: " + scriptSyntaxFilePath);
				return;
			}
			DynamicSyntaxLanguage language = DynamicSyntaxLanguage.LoadFromXml(outputSyntaxFilePath, 0);
			DynamicSyntaxLanguage cSharpLanguage = DynamicSyntaxLanguage.LoadFromXml(scriptSyntaxFilePath, 0);
			cSharpLanguage.DefaultLexicalState.DefaultHighlightingStyle.ForeColor = Color.Red;

			language.Tag = "TemplateLanguage";
			cSharpLanguage.Tag = "ScriptLanguage";

			// Mark that updating is starting
			language.IsUpdating = true;
			cSharpLanguage.IsUpdating = true;

			// Add a highlighting style
			language.HighlightingStyles.Add(new HighlightingStyle("ASPDirectiveDelimiterStyle", null, Color.Black, Color.Yellow));

			// Create a new lexical state
			DynamicLexicalState lexicalState = new DynamicLexicalState(0, "ASPDirectiveState");
			lexicalState.DefaultTokenKey = "ASPDirectiveDefaultToken";
			lexicalState.DefaultHighlightingStyle = language.HighlightingStyles["DefaultStyle"];
			lexicalState.LexicalStateTransitionLexicalState = cSharpLanguage.LexicalStates["DefaultState"];
			language.LexicalStates.Add(lexicalState);

			// Add the new lexical state at the beginning of the child states...
			// Remember that with an NFA regular expression, the first match is taken...
			// So since a < scope pattern is already in place, we must insert the new one before it
			language.LexicalStates["DefaultState"].ChildLexicalStates.Insert(0, lexicalState);

			// Create a lexical scope with a lexical state transition
			DynamicLexicalScope lexicalScope = new DynamicLexicalScope();
			lexicalState.LexicalScopes.Add(lexicalScope);
			lexicalScope.StartLexicalPatternGroup = new LexicalPatternGroup(LexicalPatternType.Explicit, "ASPDirectiveStartToken", language.HighlightingStyles["ASPDirectiveDelimiterStyle"], "#");
			lexicalScope.EndLexicalPatternGroup = new LexicalPatternGroup(LexicalPatternType.Explicit, "ASPDirectiveEndToken", language.HighlightingStyles["ASPDirectiveDelimiterStyle"], "#");

			// Mark that updating is complete (since linking is complete, the flag setting 
			// will filter from the XML language into the C# language)
			language.IsUpdating = false;
			syntaxEditorFilename.Document.Language = language;
		}

		private void syntaxEditorFilename_TriggerActivated(object sender, TriggerEventArgs e)
		{
			string language = "";
			TokenStream stream = syntaxEditorFilename.Document.GetTokenStream(syntaxEditorFilename.Document.Tokens.IndexOf(
								syntaxEditorFilename.SelectedView.Selection.EndOffset - 1));
			string allWords = "";

			if (stream.Position > 0)
			{
				DynamicToken token = (DynamicToken)stream.ReadReverse();
				language = token.LexicalState.Language.Key;
				int endPos = syntaxEditorFilename.SelectedView.Selection.EndOffset - 1;
				int startPos = syntaxEditorFilename.Document.Text.LastIndexOf('#', endPos) + 1;
				allWords = syntaxEditorFilename.Document.Text.Substring(startPos, endPos - startPos);
			}
			switch (e.Trigger.Key)
			{
				case "MemberListTrigger":
					SetupIntelliPromptList(allWords);

					// Show the list
					if (syntaxEditorFilename.IntelliPrompt.MemberList.Count > 0)
					{
						syntaxEditorFilename.IntelliPrompt.MemberList.Show();
					}
					return;
			}
		}

		private void SetupIntelliPromptList(string allWords)
		{
			syntaxEditorFilename.IntelliPrompt.MemberList.Clear();
			string[] words = allWords.Split('.');
			Type currentType = frmOutputFileWizard.IterationType;

			for (int i = 0; i < words.Length; i++)
			{
				bool found = false;
				string word = words[i].Trim();

				if (i == 0)
				{
					if (frmOutputFileWizard.IterationType == null || word != "iterator")//frmOutputFileWizard.IterationType.Name)
					{
						if (word == "UserOptions")
						{
							foreach (UserOption userOption in Project.Instance.UserOptions)
							{
								if (userOption.IteratorType == null)
								{
									syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(userOption.VariableName, 0, userOption.VarType.FullName));
								}
							}
							return;
						}
						return;
					}
				}
				// Find the matching field or property
				foreach (FieldInfo field in currentType.GetFields())
				{
					if (field.Name == words[i])
					{
						currentType = field.FieldType;
						found = true;
						break;
					}
				}
				if (!found)
				{
					foreach (PropertyInfo property in currentType.GetProperties())
					{
						if (property.Name == words[i])
						{
							if (property.Name != "Ex")
							{
								currentType = property.PropertyType;
							}
							found = true;
							break;
						}
					}
				}
				if (i == words.Length - 1)
				{
					if (words[i] == "Ex")
					{
						foreach (UserOption userOption in Project.Instance.UserOptions)
						{
							if (userOption.IteratorType == currentType)
							{
								syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(userOption.VariableName, 0, userOption.VarType.FullName));
							}
						}
						return;
					}
					// We have reached the final word. Display the properties and fields for this type in IntelliSense
					foreach (FieldInfo field in currentType.GetFields())
					{
						syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(field.Name, 0, field.FieldType.FullName));
					}
					foreach (PropertyInfo property in currentType.GetProperties())
					{
						if (property.Name != "VirtualProperites")
						{
							syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(property.Name, 0, property.PropertyType.FullName));
						}
						else
						{
							syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(property.Name, 0, "Virtual properties"));
						}
					}
					return;
				}
			}
			syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem("text", 0, "description"));
		}

		private void syntaxEditorFilename_KeyUp(object sender, KeyEventArgs e)
		{
			int index = syntaxEditorFilename.Caret.DocumentPosition.Character;

			if (TypedChar != null && index > 0)
			{
				char character = TypedChar.Value;// syntaxEditorFilename.Document.Text[index - 1];

				if (character == '#')
				{
					int count = 0;

					// How many hashes before this one?
					for (int i = 0; i < index; i++)
					{
						if (syntaxEditorFilename.Document.Text[i] == '#')
						{
							count++;
						}
					}
					int remainder = count % 2;

					if (remainder == 1)
					{
						syntaxEditorFilename.IntelliPrompt.MemberList.Clear();

						if (frmOutputFileWizard.IterationType != null)
						{
							syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(frmOutputFileWizard.IterationType.Name, 0, frmOutputFileWizard.IterationType.Name));
						}
						syntaxEditorFilename.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem("UserOptions", 0, "UserOptions"));
						syntaxEditorFilename.IntelliPrompt.MemberList.Show();
					}
				}
			}
			TypedChar = null;
		}

		private void syntaxEditorFilename_KeyPress(object sender, KeyPressEventArgs e)
		{
			TypedChar = e.KeyChar;
		}
	}
}
