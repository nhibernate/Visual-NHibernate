using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Workbench.UserControls
{
	public partial class NamingEditor : UserControl
	{
		public enum NamingTypes
		{
			None,
			Entity,
			Property
		}
		private bool BusyPopulating = false;
		private ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.DotNetProjectResolver dotNetProjectResolver = new ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.DotNetProjectResolver();
		private string CurrentScriptType = "";
		private string TempAssembliesDir;
		private string Script;
		private NamingTypes CurrentType = NamingTypes.None;
		private bool IntelliSenseIsInitialized = false;

		public NamingEditor()
		{
			InitializeComponent();

			syntaxEditorScriptHeader.Document.Language = Slyce.Common.SyntaxEditorHelper.GetDynamicLanguage(Slyce.Common.TemplateContentLanguage.CSharp);
			syntaxEditorOffscreen.Document.Language = Slyce.Common.SyntaxEditorHelper.GetDynamicLanguage(Slyce.Common.TemplateContentLanguage.CSharp);

			//SetupIntelliSense();
		}

		internal void Populate(NamingTypes type, string script)
		{
			Script = script;
			CurrentType = type;
			SetText(script);

			if (CurrentType == NamingTypes.Entity)
			{
				labelHeader.Text = "Script to assign name to entity from table";
				labelDescription.Text = "         public string GetName(Table table)";
			}
			else if (CurrentType == NamingTypes.Property)
			{
				labelHeader.Text = "Script to assign name to property from column";
				labelDescription.Text = "         public string GetName(Column column)";
			}
			else
				throw new NotImplementedException("Not handled yet: " + CurrentType.ToString());

			if (!IntelliSenseIsInitialized)
				SetupIntelliSense();
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

		private void SetupIntelliSense()
		{
			// Start the parser service (only call this once at the start of your application)
			SemanticParserService.Start();
			string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Branding.ProductName + Path.DirectorySeparatorChar + "Temp");

			if (!Directory.Exists(temp))
				Directory.CreateDirectory(temp);

			string intellisenseCacheDir = Path.Combine(temp, @"IntellisenseCacheModelScripts");
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

					// TODO: Test, if conversion to new method signature is ok
					//((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).IntelliPromptCompleteWord(syntaxEditorOffscreen, editor);
					((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).IntelliPromptCompleteWord(editor);
				}
				else if (e.Shift && e.KeyValue == openParen)
				{
					syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, extraCode) + "(";
					syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline).Length;// -1;
					//syntaxEditorOffscreen.Focus();
			// TODO: Test, if conversion to new method signature is ok
					//((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptParameterInfo(syntaxEditorOffscreen, editor);
					((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptParameterInfo(editor);
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

			throw new NotImplementedException("currentScriptType not handled yet: " + currentScriptType);
			//if (currentScriptType == "Header")
			//    return ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.GetCodeForDebuggingHeader(rawCode);
			//else
			//    return ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.GetCodeForDebuggingTable(rawCode);
		}

		private void syntaxEditorScriptHeader_TextChanged(object sender, EventArgs e)
		{
			CheckSyntax();
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
			// TODO: Test, if conversion to new method signature is ok
			//((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptMemberList(syntaxEditorOffscreen, editor);
			((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptMemberList(editor);
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
			dotNetProjectResolver.AddExternalReference(Assembly.GetAssembly(typeof(Slyce.Common.Utility)), "Slyce.Common");
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
			Script = syntaxEditorScriptHeader.Document.GetText(LineTerminator.Newline);

			if (CurrentType == NamingTypes.Entity)
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Utility.EntityNamingScript = Script;
			else if (CurrentType == NamingTypes.Property)
				ArchAngel.Interfaces.ProjectOptions.ModelScripts.Utility.PropertyNamingScript = Script;
		}
	}
}
