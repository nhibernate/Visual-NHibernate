using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Workbench.UserControls
{
	public partial class DatabaseScriptEditor : UserControl
	{
		private ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.MaintenanceScript Script;
		private bool BusyPopulating = false;
		private ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.DotNetProjectResolver dotNetProjectResolver = new ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.DotNetProjectResolver();
		private string CurrentScriptType = "";
		private string TempAssembliesDir;
		private bool IntelliSenseIsInitialized = false;

		public DatabaseScriptEditor()
		{
			InitializeComponent();

			Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScriptHeader, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorOffscreen, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			SwitchFormatting(syntaxEditorScriptHeader);
			SwitchFormatting(syntaxEditorOffscreen);

			//SetupIntelliSense();
		}

		internal void Populate(ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.MaintenanceScript script, string scriptType)
		{
			Script = script;
			CurrentScriptType = scriptType;

			switch (scriptType)
			{
				case "Default":
					SetText(Script.Header);
					break;
				//case "Header":
				//    SetText(Script.Header);
				//    break;
				//case "Create":
				//    SetText(Script.Create);
				//    break;
				//case "Update":
				//    SetText(Script.Update);
				//    break;
				//case "Delete":
				//    SetText(Script.Delete);
				//    break;
				default:
					throw new NotImplementedException("Script type not handled yet: " + scriptType);
			}
			if (!IntelliSenseIsInitialized)
				SetupIntelliSense();
		}

		private void SetupIntelliSense()
		{
			// Start the parser service (only call this once at the start of your application)
			SemanticParserService.Start();
			string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Branding.ProductName + Path.DirectorySeparatorChar + "Temp");

			if (!Directory.Exists(temp))
				Directory.CreateDirectory(temp);

			string intellisenseCacheDir = Path.Combine(temp, @"IntellisenseCacheDBScripts");
			TempAssembliesDir = Path.Combine(temp, @"TempAssemblies");

			if (!Directory.Exists(intellisenseCacheDir))
				Directory.CreateDirectory(intellisenseCacheDir);

			dotNetProjectResolver.CachePath = intellisenseCacheDir;
			//AddMainFunctionAssemblyToProjectResolver();
			ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage cSharpLanguage = new ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage();
			syntaxEditorOffscreen.Document.Language = cSharpLanguage;
			syntaxEditorOffscreen.Document.LanguageData = dotNetProjectResolver;
			syntaxEditorOffscreen.Document.Filename = System.Guid.NewGuid().ToString() + ".cs";

			ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator(null, Controller.Instance.CurrentProject.TemplateLoader);
			gen.ClearAllDebugLines();

			if (!backgroundWorkerAddReferences.IsBusy)
				backgroundWorkerAddReferences.RunWorkerAsync();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Stop the parser service... note that you should only do this if you are closing your application down
				//   and are sure no other languages are still using the service... in the case of this sample project,
				//   each QuickStart Form is modal so we know another window doesn't have a language that is still accessing the service
				SemanticParserService.Stop();

				// Dispose the project resolver... this releases all its resources
				try
				{
					dotNetProjectResolver.Dispose();
				}
				catch
				{
					// Do nothing
				}

				// Prune the cache to remove files that no longer apply... note that you should only do this if you are closing your application down
				//   and are sure no other project resolvers are still using the cache... in the case of this sample project, 
				//   each QuickStart Form is modal so we know another window is still not accessing the cache
				dotNetProjectResolver.PruneCache();

				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void SwitchFormatting(ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor)
		{
			//UseSplitLanguage = !UseSplitLanguage;
			if (syntaxEditor.Document.Language.LexicalStates.Count > 1)
			{
				syntaxEditor.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.
					Language.BackColor = Slyce.Common.SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED;
				syntaxEditor.Document.Language.BackColor = Slyce.Common.SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL;
				syntaxEditor.Refresh();
			}
		}

		public string GetText(ActiproSoftware.SyntaxEditor.LineTerminator lineTerminator)
		{
			return syntaxEditorScriptHeader.Document.GetText(lineTerminator);
		}

		public void SetText(string text)
		{
			BusyPopulating = true;
			syntaxEditorScriptHeader.Document.Text = text;
			BusyPopulating = false;
			CheckSyntax();
		}

		private void syntaxEditorScriptHeader_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessEditorKeyDown(sender, e);
		}

		private void ProcessEditorKeyDown(object sender, KeyEventArgs e)
		{
			int openParen = 57;

			if ((e.Control && e.KeyCode == Keys.Space) ||
				(e.Shift && e.KeyValue == openParen))
			{
				SyntaxEditor editor = (SyntaxEditor)sender;
				int closePos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf("%>", editor.Caret.Offset);
				int openPos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf("<%", editor.Caret.Offset);

				if (openPos < 0 || closePos > openPos)
					return;

				ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
				string extraCode = Slyce.Common.Utility.StandardizeLineBreaks(gen.GetFunctionLookupClass(true), Slyce.Common.Utility.LineBreaks.Unix);

				if (e.Control && e.KeyCode == Keys.Space)
				{
					syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, extraCode) + "}";
					syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline).Length - 1;

					((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).IntelliPromptCompleteWord(syntaxEditorOffscreen, editor);
				}
				else if (e.Shift && e.KeyValue == openParen)
				{
					syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, extraCode) + "(";
					syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline).Length;// -1;
					//syntaxEditorOffscreen.Focus();
					((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptParameterInfo(syntaxEditorOffscreen, editor);
				}
			}
		}

		private string GetCSharpCode(string rawCode, int caretOffset, string currentScriptType)
		{
			StringBuilder sb = new StringBuilder(1000);

			rawCode = rawCode.Substring(0, caretOffset);

			int codeStart = rawCode.IndexOf("<%");
			int codeEnd = 0;

			if (codeStart >= 0)
				codeEnd = rawCode.IndexOf("%>", codeStart);

			while (codeStart >= 0)
			{
				bool addWrite = false;

				if (codeStart + 2 < rawCode.Length &&
					rawCode[codeStart + 2] == '=')
				{
					codeStart += 3;
					sb.Append("Write(");
					addWrite = true;
				}
				else
					codeStart += 2;

				if (codeEnd > codeStart)
				{
					codeEnd += 2;
					sb.Append(rawCode.Substring(codeStart, codeEnd - codeStart - 2));

					if (addWrite)
						sb.Append(")");

					sb.Append(";");
					codeStart = rawCode.IndexOf("<%", codeEnd);

					if (codeStart >= 0)
						codeEnd = rawCode.IndexOf("%>", codeStart);
				}
				else
				{
					sb.Append(rawCode.Substring(codeStart));
					break;
				}
			}
			rawCode = sb.ToString();

			if (currentScriptType == "Default")
				return ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.GetCodeForDebuggingHeader(rawCode);

			throw new NotImplementedException("currentScriptType not handled: " + currentScriptType);
			//if (currentScriptType == "Header")
			//    return ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.GetCodeForDebuggingHeader(rawCode);
			//else if (currentScriptType == "Update")
			//    return ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.GetCodeForDebuggingChangedTable(rawCode);
			//else
			//    return ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.GetCodeForDebuggingTable(rawCode);
		}

		private void syntaxEditorScriptHeader_TextChanged(object sender, System.EventArgs e)
		{
			CheckSyntax();
		}

		private void CheckSyntax()
		{
			if (BusyPopulating)
				return;

			if (ArchAngel.Interfaces.SharedData.CurrentProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject != null)
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty = true;
			}

			if (!timer1.Enabled)
			{
				timer1.Enabled = true;
				timer1.Start();
			}
			else
			{
				timer1.Stop();
				timer1.Start();
			}
		}

		private void syntaxEditorScriptHeader_TriggerActivated(object sender, TriggerEventArgs e)
		{
			ProcessEditorTriggerActivated(sender, e);
		}

		private void ProcessEditorTriggerActivated(object sender, ActiproSoftware.SyntaxEditor.TriggerEventArgs e)
		{
			SyntaxEditor editor = (SyntaxEditor)sender;
			int closePos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf("%>", editor.Caret.Offset);
			int openPos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf("<%", editor.Caret.Offset);

			if (openPos < 0 || closePos > openPos)
				return;

			ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
			string extraCode = Slyce.Common.Utility.StandardizeLineBreaks(gen.GetFunctionLookupClass(true), Slyce.Common.Utility.LineBreaks.Unix);

			syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, CurrentScriptType);
			syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.Text.Length - 1;
			((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptMemberList(syntaxEditorOffscreen, editor);
			return;
		}

		private void backgroundWorkerAddReferences_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			AddMainFunctionAssemblyToProjectResolver();
		}

		private void AddMainFunctionAssemblyToProjectResolver()
		{
			// Don't re-add if external references have already been added
			if (dotNetProjectResolver.ExternalReferences.Count > 0)
				return;

			dotNetProjectResolver.AddExternalReference(Assembly.GetAssembly(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn)), "ArchAngel.Interfaces");
			dotNetProjectResolver.AddExternalReference(Assembly.GetAssembly(typeof(ArchAngel.NHibernateHelper.BytecodeGenerator)), "ArchAngel.NHibernateHelper");
#if DEBUG
			dotNetProjectResolver.AddExternalReference(Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\Inflector.Net.dll"));
#else
			dotNetProjectResolver.AddExternalReference(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Inflector.Net.dll"));
#endif

			dotNetProjectResolver.AddExternalReferenceForSystemAssembly("System");
			dotNetProjectResolver.AddExternalReferenceForSystemAssembly("System.Xml");
			dotNetProjectResolver.AddExternalReferenceForMSCorLib();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 600;
			//QuickCompile();
		}

		internal void Save()
		{
			switch (CurrentScriptType)
			{
				case "":
					break;
				case "Default":
					Script.Header = syntaxEditorScriptHeader.Document.GetText(LineTerminator.Newline);
					break;
				//case "Header":
				//    Script.Header = syntaxEditorScriptHeader.Document.GetText(LineTerminator.Newline);
				//    break;
				//case "Create":
				//    Script.Create = syntaxEditorScriptHeader.Document.GetText(LineTerminator.Newline);
				//    break;
				//case "Update":
				//    Script.Update = syntaxEditorScriptHeader.Document.GetText(LineTerminator.Newline);
				//    break;
				//case "Delete":
				//    Script.Delete = syntaxEditorScriptHeader.Document.GetText(LineTerminator.Newline);
				//    break;
				default:
					throw new NotImplementedException("ScriptType not handled yet: " + CurrentScriptType);
			}
		}
	}
}
