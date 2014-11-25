using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.Products.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Context;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {

	/// <summary>
	/// Represents a <c>C#</c> language definition.
	/// </summary>
	#if !NOLICENSECHECK
	[LicenseProvider(typeof(AddonsDotNetLicenseProvider))]
	#endif
	public class CSharpSyntaxLanguage : DotNetSyntaxLanguage {

		private CSharpFormattingOptions	formattingOptions								= new CSharpFormattingOptions();
		private CSharpLexicalParser		lexicalParser									= new CSharpLexicalParser();

		private ActiproSoftware.Products.ActiproLicense	license;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CSharpSyntaxLanguage</c> class.
		/// </summary>
		public CSharpSyntaxLanguage() : base("C#") {
			this.ExampleText = @"// The automated C# IntelliPrompt features demoed here
// are part of the .NET Languages add-on, sold 
// separately from SyntaxEditor

// Hover over any identifier to get quick info
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ActiproSoftware.SyntaxEditor {

	/// <summary>
	/// Start typing anywhere in the code to get
	/// automated IntelliPrompt support like member
	/// lists, complete word (Ctrl+Space), and
	/// parameter info tips.
	/// </summary>
	public class TestClass : Control {

		/// <summary>
		/// Doubles the specified number.
		/// </summary>
		/// <param name=""x"">The number to double.</param>
		/// <returns>The result.</returns>
		public int Double(int x) {
			// Type this. to see a member list and then
			// Add( to see a parameter info tip
			
			return this.Add(x, x);
		}

		/// <summary>
		/// Adds two numbers together.
		/// </summary>
		/// <param name=""x"">The first number.</param>
		/// <param name=""y"">The second number.</param>
		/// <returns>The result.</returns>
		public int Add(int x, int y) {
			int result = x + y;			
			return result;
		}

		/// <summary>
		/// Adds three number together.
		/// </summary>
		/// <param name=""x"">The first number.</param>
		/// <param name=""y"">The second number.</param>
		/// <param name=""z"">The third number.</param>
		/// <returns>The result.</returns>
		public int Add(int x, int y, int z) {
			int result = x + y + z;			
			return result;
		}

		// Type /// on the next line to see method
		// XML documentation comment auto-complete
		
		public int Subtract(int x, int y) {
			int result = x - y;
			return result;
		}
	}
}";

			// Initialize highlighting styles
			this.HighlightingStyles.Add(new HighlightingStyle("KeywordStyle", null, Color.Blue, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("CommentStyle", null, Color.Green, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("DocumentationCommentStyle", null, Color.Gray, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("StringStyle", null, Color.Maroon, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("NumberStyle", null, Color.Purple, Color.Empty));

			// Initialize lexical states
			this.LexicalStates.Add(new DefaultLexicalState(CSharpLexicalStateID.Default, "DefaultState"));
			this.LexicalStates.Add(new DefaultLexicalState(CSharpLexicalStateID.DocumentationComment, "DocumentationCommentState"));
			this.LexicalStates.Add(new DefaultLexicalState(CSharpLexicalStateID.PreProcessorDirective, "PreProcessorDirectiveState"));
			this.DefaultLexicalState = this.LexicalStates["DefaultState"];
			this.LexicalStates["DocumentationCommentState"].LexicalScopes.Add(new ProgrammaticLexicalScope(new ProgrammaticLexicalScopeMatchDelegate(lexicalParser.IsDocumentationCommentStateScopeStart), new ProgrammaticLexicalScopeMatchDelegate(lexicalParser.IsDocumentationCommentStateScopeEnd)));
			this.LexicalStates["PreProcessorDirectiveState"].LexicalScopes.Add(new ProgrammaticLexicalScope(new ProgrammaticLexicalScopeMatchDelegate(lexicalParser.IsPreProcessorDirectiveStateScopeStart), new ProgrammaticLexicalScopeMatchDelegate(lexicalParser.IsPreProcessorDirectiveStateScopeEnd)));
			this.LexicalStates["DefaultState"].DefaultHighlightingStyle = this.HighlightingStyles["DefaultStyle"];
			this.LexicalStates["DocumentationCommentState"].DefaultHighlightingStyle = this.HighlightingStyles["CommentStyle"];
			this.LexicalStates["PreProcessorDirectiveState"].DefaultHighlightingStyle = this.HighlightingStyles["DefaultStyle"];
			this.LexicalStates["DefaultState"].ChildLexicalStates.Add(this.LexicalStates["DocumentationCommentState"]);
			this.LexicalStates["DefaultState"].ChildLexicalStates.Add(this.LexicalStates["PreProcessorDirectiveState"]);

			#if !NOLICENSECHECK
			// See if the control is licensed
			ActiproSoftware.Products.ActiproLicenseProvider.EnsureLicenseProvider(this, typeof(AddonsDotNetLicenseProvider));
			license = LicenseManager.Validate(typeof(CSharpSyntaxLanguage), this) as ActiproSoftware.Products.ActiproLicense;
			#endif
		}
		
		/// <summary>
		/// This constructor is for designer use only and should never be called by your code.
		/// </summary>
		/// <param name="container">An <see cref="IContainer"></see> that represents the container for the component.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public CSharpSyntaxLanguage(IContainer container) : this() {
			// Add to the container
			container.Add(this);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Add <see cref="IntelliPromptMemberListItem"/> items that indicate the language keywords to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		protected override void AddKeywordMemberListItems(Hashtable memberListItemHashtable) {
			for (int id = CSharpTokenID.ContextualKeywordStart + 1; id < CSharpTokenID.ContextualKeywordEnd; id++) {
				string keyword = CSharpTokenID.GetTokenKey(id).ToLower();
				memberListItemHashtable[keyword] = new IntelliPromptMemberListItem(keyword, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);
			}
			for (int id = CSharpTokenID.KeywordStart + 1; id < CSharpTokenID.KeywordEnd; id++) {
				string keyword = CSharpTokenID.GetTokenKey(id).ToLower();
				memberListItemHashtable[keyword] = new IntelliPromptMemberListItem(keyword, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);
			}
		}
		
		/// <summary>
		/// Creates an <see cref="IToken"/> that represents the end of a document.
		/// </summary>
		/// <param name="startOffset">The start offset of the <see cref="IToken"/>.</param>
		/// <param name="lexicalState">The <see cref="ILexicalState"/> that contains the token.</param>
		/// <returns>An <see cref="IToken"/> that represents the end of a document.</returns>
		public override IToken CreateDocumentEndToken(int startOffset, ILexicalState lexicalState) {
			return new CSharpToken(startOffset, 0, LexicalParseFlags.None, null, new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)CSharpTokenID.DocumentEnd));
		}

		/// <summary>
		/// Creates an <see cref="IToken"/> that represents an invalid range of text.
		/// </summary>
		/// <param name="startOffset">The start offset of the <see cref="IToken"/>.</param>
		/// <param name="length">The length of the <see cref="IToken"/>.</param>
		/// <param name="lexicalState">The <see cref="ILexicalState"/> that contains the token.</param>
		/// <returns>An <see cref="IToken"/> that represents an invalid range of text.</returns>
		public override IToken CreateInvalidToken(int startOffset, int length, ILexicalState lexicalState) {
			return new CSharpToken(startOffset, length, LexicalParseFlags.None, null, new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)CSharpTokenID.Invalid));
		}
		
		/// <summary>
		/// Creates an <see cref="IToken"/> that represents the range of text with the specified lexical parse data.
		/// </summary>
		/// <param name="startOffset">The start offset of the token.</param>
		/// <param name="length">The length of the token.</param>
		/// <param name="lexicalParseFlags">The <see cref="LexicalParseFlags"/> for the token.</param>
		/// <param name="parentToken">The <see cref="IToken"/> that starts the current state scope specified by the <see cref="IToken.LexicalState"/> property.</param>
		/// <param name="lexicalParseData">The <see cref="ITokenLexicalParseData"/> that contains lexical parse information about the token.</param>
		/// <returns></returns>
		public override IToken CreateToken(int startOffset, int length, LexicalParseFlags lexicalParseFlags, IToken parentToken, ITokenLexicalParseData lexicalParseData) {
			return new CSharpToken(startOffset, length, lexicalParseFlags, parentToken, lexicalParseData);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the object and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. 
		/// </param>
		/// <remarks>
		/// This method is called by the public <c>Dispose</c> method and the <c>Finalize</c> method. 
		/// <c>Dispose</c> invokes this method with the <paramref name="disposing"/> parameter set to <c>true</c>. 
		/// <c>Finalize</c> invokes this method with <paramref name="disposing"/> set to <c>false</c>.
		/// </remarks>
		protected override void Dispose(bool disposing) {
			// Call the base method
			base.Dispose(disposing);

			if (disposing) {
				// Dispose the license
				if (license != null) {
					license.Dispose();
					license = null;
				}
			}
		}
		
		/// <summary>
		/// Gets a <see cref="CSharpFormattingOptions"/> that contains language formatting options.
		/// </summary>
		/// <value>A <see cref="CSharpFormattingOptions"/> that contains language formatting options.</value>
		[
			Category("Behavior"),
			Description("A CSharpFormattingOptions that contains language formatting options."),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
		]
		public CSharpFormattingOptions FormattingOptions {
			get {
				return formattingOptions;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetContext"/> for the specified offset.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		/// <param name="offset">The offset at which to base the context.</param>
		/// <param name="beforeOffset">Whether to return the context before the offset.</param>
		/// <param name="forParameterInfo">Whether to return the context for parameter info.</param>
		/// <returns>The <see cref="DotNetContext"/> for the specified offset.</returns>
		protected override DotNetContext GetContext(SyntaxEditor syntaxEditor, int offset, bool beforeOffset, bool forParameterInfo) {
			// Get the compilation unit and project resolver
			CompilationUnit compilationUnit = syntaxEditor.Document.SemanticParseData as CompilationUnit;
			DotNetProjectResolver projectResolver = syntaxEditor.Document.LanguageData as DotNetProjectResolver;

			// Get the context
			if (beforeOffset)
				return CSharpContext.GetContextBeforeOffset(syntaxEditor.Document, offset, compilationUnit, projectResolver, forParameterInfo);
			else
				return CSharpContext.GetContextAtOffset(syntaxEditor.Document, offset, compilationUnit, projectResolver);
		}
		
		/// <summary>
		/// Returns the <see cref="HighlightingStyle"/> for the specified <see cref="IToken"/>.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>The <see cref="HighlightingStyle"/> for the specified <see cref="IToken"/>.</returns>
		public override HighlightingStyle GetHighlightingStyle(IToken token) {
			switch (token.LexicalStateID) {
				case CSharpLexicalStateID.Default:
					switch (token.ID) {
						case CSharpTokenID.SingleLineComment:
						case CSharpTokenID.MultiLineComment:
							return this.HighlightingStyles["CommentStyle"];
						case CSharpTokenID.StringLiteral:
						case CSharpTokenID.VerbatimStringLiteral:
						case CSharpTokenID.CharacterLiteral:
							return this.HighlightingStyles["StringStyle"];
						case CSharpTokenID.DecimalIntegerLiteral:
						case CSharpTokenID.HexadecimalIntegerLiteral:
						case CSharpTokenID.RealLiteral:
							return this.HighlightingStyles["NumberStyle"];
						case CSharpTokenID.DocumentationCommentDelimiter:
							return this.HighlightingStyles["DocumentationCommentStyle"];
						default:
							if (
								((token.ID > CSharpTokenID.KeywordStart) && (token.ID < CSharpTokenID.KeywordEnd)) ||
								((token.ID > CSharpTokenID.ContextualKeywordStart) && (token.ID < CSharpTokenID.ContextualKeywordEnd)) ||
								((token.ID > CSharpTokenID.PreProcessorDirectiveKeywordStart) && (token.ID < CSharpTokenID.PreProcessorDirectiveKeywordEnd))
								)
								return this.HighlightingStyles["KeywordStyle"];
							else
								return token.LexicalState.DefaultHighlightingStyle;
					}
				case CSharpLexicalStateID.DocumentationComment:
					switch (token.ID) {
						case CSharpTokenID.DocumentationCommentTag:
							return this.HighlightingStyles["DocumentationCommentStyle"];
						default:
							return token.LexicalState.DefaultHighlightingStyle;
					}
				case CSharpLexicalStateID.PreProcessorDirective:
					switch (token.ID) {
						case CSharpTokenID.SingleLineComment:
							return this.HighlightingStyles["CommentStyle"];
						default:
							return token.LexicalState.DefaultHighlightingStyle;
					}
				default:
					return token.LexicalState.DefaultHighlightingStyle;
			}
		}
			
		/// <summary>
		/// Gets the token string representation for the specified token ID.
		/// </summary>
		/// <param name="tokenID">The ID of the token to examine.</param>
		/// <returns>The token string representation for the specified token ID.</returns>
		public override string GetTokenString(int tokenID) {
			if ((tokenID > (int)CSharpTokenID.KeywordStart) && (tokenID < (int)CSharpTokenID.KeywordEnd))
				return CSharpTokenID.GetTokenKey(tokenID).ToLower();
			if ((tokenID > (int)CSharpTokenID.ContextualKeywordStart) && (tokenID < (int)CSharpTokenID.ContextualKeywordEnd))
				return CSharpTokenID.GetTokenKey(tokenID).ToLower();
			
			switch (tokenID) {
				// General
				case CSharpTokenID.DocumentEnd:
					return "Document end";
				case CSharpTokenID.Whitespace:
					return "Whitespace";
				case CSharpTokenID.LineTerminator:
					return "Line terminator";
				case CSharpTokenID.SingleLineComment:
					return "Single-line comment";
				case CSharpTokenID.MultiLineComment:
					return "Multi-line comment";
				case CSharpTokenID.DocumentationCommentDelimiter:
					return "///";
				case CSharpTokenID.DocumentationCommentText:
					return "Documentation comment text";
				case CSharpTokenID.DocumentationCommentTag:
					return "Documentation comment tag";
				case CSharpTokenID.DecimalIntegerLiteral:
					return "Integer number";
				case CSharpTokenID.HexadecimalIntegerLiteral:
					return "Hexidecimal integer number";
				case CSharpTokenID.RealLiteral:
					return "Real number";
				case CSharpTokenID.CharacterLiteral:
					return "Character";
				case CSharpTokenID.StringLiteral:
					return "String";
				case CSharpTokenID.VerbatimStringLiteral:
					return "Verbatim string";
				case CSharpTokenID.Identifier:
					return "Identifier";
				// Operators
				case CSharpTokenID.OpenCurlyBrace:
					return "{";
				case CSharpTokenID.CloseCurlyBrace:
					return "}";
				case CSharpTokenID.OpenSquareBrace:
					return "[";
				case CSharpTokenID.CloseSquareBrace:
					return "]";
				case CSharpTokenID.OpenParenthesis:
					return "(";
				case CSharpTokenID.CloseParenthesis:
					return ")";
				case CSharpTokenID.Dot:
					return ".";
				case CSharpTokenID.Comma:
					return ",";
				case CSharpTokenID.Colon:
					return ":";
				case CSharpTokenID.NamespaceAliasQualifier:
					return "::";
				case CSharpTokenID.SemiColon:
					return ";";
				case CSharpTokenID.Addition:
					return "+";
				case CSharpTokenID.Subtraction:
					return "-";
				case CSharpTokenID.Multiplication:
					return "*";
				case CSharpTokenID.Division:
					return "/";
				case CSharpTokenID.Modulus:
					return "%";
				case CSharpTokenID.BitwiseAnd:
					return "&";
				case CSharpTokenID.BitwiseOr:
					return "|";
				case CSharpTokenID.ExclusiveOr:
					return "^";
				case CSharpTokenID.Negation:
					return "!";
				case CSharpTokenID.OnesComplement:
					return "~";
				case CSharpTokenID.Assignment:
					return "=";
				case CSharpTokenID.LessThan:
					return "<";
				case CSharpTokenID.GreaterThan:
					return ">";
				case CSharpTokenID.QuestionMark:
					return "?";
				case CSharpTokenID.Increment:
					return "++";
				case CSharpTokenID.Decrement:
					return "--";
				case CSharpTokenID.ConditionalAnd:
					return "&&";
				case CSharpTokenID.ConditionalOr:
					return "||";
				case CSharpTokenID.LeftShift:
					return "<<";
				case CSharpTokenID.Equality:
					return "==";
				case CSharpTokenID.Inequality:
					return "!=";
				case CSharpTokenID.LessThanOrEqual:
					return "<=";
				case CSharpTokenID.GreaterThanOrEqual:
					return ">=";
				case CSharpTokenID.AdditionAssignment:
					return "+=";
				case CSharpTokenID.SubtractionAssignment:
					return "-=";
				case CSharpTokenID.MultiplicationAssignment:
					return "*=";
				case CSharpTokenID.DivisionAssignment:
					return "/=";
				case CSharpTokenID.ModulusAssignment:
					return "%=";
				case CSharpTokenID.BitwiseAndAssignment:
					return "&=";
				case CSharpTokenID.BitwiseOrAssignment:
					return "|=";
				case CSharpTokenID.ExclusiveOrAssignment:
					return "^=";
				case CSharpTokenID.LeftShiftAssignment:
					return "<<=";
				case CSharpTokenID.PointerDereference:
					return "->";
				case CSharpTokenID.NullCoalescing:
					return "??";
				// Pre-processor directives
				case CSharpTokenID.IfPreProcessorDirective:
					return "#if";
				case CSharpTokenID.ElsePreProcessorDirective:
					return "#else";
				case CSharpTokenID.ElIfPreProcessorDirective:
					return "#elif";
				case CSharpTokenID.EndIfPreProcessorDirective:
					return "#endif";
				case CSharpTokenID.DefinePreProcessorDirective:
					return "#define";
				case CSharpTokenID.UndefPreProcessorDirective:
					return "#undef";
				case CSharpTokenID.WarningPreProcessorDirective:
					return "#warning";
				case CSharpTokenID.ErrorPreProcessorDirective:
					return "#error";
				case CSharpTokenID.LinePreProcessorDirective:
					return "#line";
				case CSharpTokenID.RegionPreProcessorDirective:
					return "#region";
				case CSharpTokenID.EndRegionPreProcessorDirective:
					return "#endregion";
				case CSharpTokenID.PragmaPreProcessorDirective:
					return "#pragma";
				case CSharpTokenID.PreProcessorDirectiveText:
					return "Pre-processor directive text";
			}

			return null;
		}
		
		/// <summary>
		/// Performs an auto-complete if the <see cref="SyntaxEditor"/> context with which the IntelliPrompt member list is initialized causes a single selection.
		/// Otherwise, displays a member list in the <see cref="SyntaxEditor"/>.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt member list.</param>
		/// <returns>
		/// <c>true</c> if an auto-complete occurred or if an IntelliPrompt member list is displayed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Only call this method if the <see cref="SyntaxLanguage.IntelliPromptMemberListSupported"/> property is set to <c>true</c>.
		/// </remarks>
		public override bool IntelliPromptCompleteWord(SyntaxEditor syntaxEditor) {
			return this.ShowIntelliPromptMemberList(DotNetLanguage.CSharp, syntaxEditor, true);
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetLanguage"/> that this language represents.
		/// </summary>
		/// <value>The <see cref="DotNetLanguage"/> that this language represents.</value>
		public override DotNetLanguage LanguageType { 
			get {
				return DotNetLanguage.CSharp;
			}
		}

		/// <summary>
		/// Resets the <see cref="SyntaxLanguage.LineCommentDelimiter"/> property to its default value.
		/// </summary>
		public override void ResetLineCommentDelimiter() {
			this.LineCommentDelimiter = "//";
		}
		/// <summary>
		/// Indicates whether the <see cref="SyntaxLanguage.LineCommentDelimiter"/> property should be persisted.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the property value has changed from its default; otherwise, <c>false</c>.
		/// </returns>
		public override bool ShouldSerializeLineCommentDelimiter() {
			return (this.LineCommentDelimiter != "//");
		}
			
		/// <summary>
		/// Gets the <see cref="IMergableLexicalParser"/> that can be used for lexical parsing of the language.
		/// </summary>
		/// <value>The <see cref="IMergableLexicalParser"/> that can be used for lexical parsing of the language.</value>
		protected override IMergableLexicalParser MergableLexicalParser {
			get {
				return lexicalParser;
			}
		}				
	
		/// <summary>
		/// Occurs when a description tip is about to be displayed for the selected IntelliPrompt member list item,
		/// but the item has no description set.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorIntelliPromptMemberListItemDescriptionRequested(SyntaxEditor syntaxEditor, EventArgs e) {
			this.GetQuickInfoForMemberListItem(DotNetLanguage.CSharp, syntaxEditor);
		}
		
		/// <summary>
		/// Occurs after the parameter index of the IntelliPrompt parameter info is changed while the parameter info is visible.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorIntelliPromptParameterInfoParameterIndexChanged(SyntaxEditor syntaxEditor, EventArgs e) {
			if (this.UpdateParameterInfoSelectedText(DotNetLanguage.CSharp, syntaxEditor, true))
				syntaxEditor.IntelliPrompt.ParameterInfo.MeasureAndResize(syntaxEditor.IntelliPrompt.ParameterInfo.Bounds.Location);
		}

		/// <summary>
		/// Occurs after the selected index of the IntelliPrompt parameter info is changed while the parameter info is visible.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorIntelliPromptParameterInfoSelectedIndexChanged(SyntaxEditor syntaxEditor, EventArgs e) {
			// NOTE: This code provides late-binding for the parameter info
			this.UpdateParameterInfoSelectedText(DotNetLanguage.CSharp, syntaxEditor, false);
		}

		/// <summary>
		/// Occurs before a <see cref="SyntaxEditor.KeyTyped"/> event is raised 
		/// for a <see cref="SyntaxEditor"/> that has a <see cref="Document"/> using this language.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>KeyTypedEventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorKeyTyped(SyntaxEditor syntaxEditor, KeyTypedEventArgs e) {
			if (e.Command is ActiproSoftware.SyntaxEditor.Commands.TypingCommand) {
				switch (e.KeyChar) {
					case '.':
						if ((this.IntelliPromptMemberListEnabled) && (syntaxEditor.Caret.Offset > 1) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(this.IsCurrentOffsetInDefaultState(syntaxEditor, CSharpTokenID.LineTerminator))) {
							// Check the previous token to ensure it's not in a number
							TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset - 1);
							switch (stream.Token.ID) {
								case CSharpTokenID.DecimalIntegerLiteral:
								case CSharpTokenID.HexadecimalIntegerLiteral:
								case CSharpTokenID.RealLiteral:
									break;
								default:
									// Show the member list for code
									this.ShowIntelliPromptMemberList(syntaxEditor);
									break;
							}
						}
						break;
					case '{':
					case '}':
						// Auto-indent the curly brace
						CSharpFormatter.AutoIndent(formattingOptions, syntaxEditor.Document, syntaxEditor.Caret.Offset, e.KeyChar.ToString());
						break;
					case '(':
					case '[':
						if ((this.IntelliPromptParameterInfoEnabled) && (syntaxEditor.Caret.Offset > 1) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(this.IsCurrentOffsetInDefaultState(syntaxEditor, CSharpTokenID.LineTerminator))) {
							// Show the parameter info for code
							this.ShowIntelliPromptParameterInfoCore(DotNetLanguage.CSharp, syntaxEditor, syntaxEditor.Caret.Offset, (e.KeyChar == '['));
						}
						break;
					case ')':
					case ']':
						if ((this.IntelliPromptParameterInfoEnabled) && (syntaxEditor.Caret.Offset > 1) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(this.IsCurrentOffsetInDefaultState(syntaxEditor, CSharpTokenID.LineTerminator))) {
							// Show the parameter info for the parent context level if there is one
							this.ShowIntelliPromptParameterInfo(syntaxEditor);
						}
						break;
					case '/': 
						if ((this.DocumentationCommentAutoCompleteEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly)) {
							// Insert a documentation comment
							this.InsertDocumentationComment(syntaxEditor, CSharpTokenID.DocumentationCommentDelimiter, CSharpTokenID.LineTerminator, CSharpTokenID.Whitespace);
						}
						break;
					case ':': {
						TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
						while (!stream.IsAtDocumentLineStart) {
							if (stream.Token.ID == CSharpTokenID.Case) {
								// Auto-indent the case statement
								CSharpFormatter.AutoIndent(formattingOptions, syntaxEditor.Document, syntaxEditor.Document.Tokens.GetTokenEndOffset(stream.TokenIndex), "case");
								break;
							}
							stream.GoToPreviousToken();
						}
						break;
					}
					case ',':
						if ((this.IntelliPromptParameterInfoEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) && (!syntaxEditor.IntelliPrompt.ParameterInfo.Visible) &&
							(syntaxEditor.Caret.Offset > 1) && (this.IsCurrentOffsetInDefaultState(syntaxEditor, CSharpTokenID.LineTerminator))) {
							// Show the parameter info for the context level if parameter info is not already displayed
							this.ShowIntelliPromptParameterInfo(syntaxEditor);
						}
						break;
					case ' ':
						if ((this.IntelliPromptMemberListEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly)) {
							// Do a fast check to see if a using keyword is before the caret
							TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
							stream.GoToCurrentTokenStart();
							while ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().IsWhitespace)) {}
							switch (stream.Token.ID) {
								case CSharpTokenID.New:
								case CSharpTokenID.Using:
									// Show the member list 
									this.ShowIntelliPromptMemberList(syntaxEditor);
									break;
							}
						}
						break;
				}
			}
			else if ((e.Command is ActiproSoftware.SyntaxEditor.Commands.InsertLineBreakCommand) && 
				(this.DocumentationCommentAutoCompleteEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly)) {

				// Continue the documentation comment if appropriate
				DocumentPosition documentPosition = syntaxEditor.Caret.DocumentPosition;
				if (documentPosition.Line > 0) {
					IToken token = syntaxEditor.Document.Tokens.GetTokenAtOffset(syntaxEditor.Document.Lines[documentPosition.Line - 1].EndOffset);
					if ((token != null) && (token.LexicalState == this.LexicalStates["DocumentationCommentState"]))
						syntaxEditor.SelectedView.ReplaceSelectedText(DocumentModificationType.Typing, "/// ", DocumentModificationOptions.CheckReadOnly);
				}
			}

			// Call the base method
			base.OnSyntaxEditorKeyTyped(syntaxEditor, e);
		}
		
		/// <summary>
		/// Occurs when a <see cref="SyntaxEditor.SmartIndent"/> event is raised 
		/// for a <see cref="SyntaxEditor"/> that has a <see cref="Document"/> using this language.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">A <c>SmartIndentEventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorSmartIndent(SyntaxEditor syntaxEditor, SmartIndentEventArgs e) {
			e.IndentAmount = CSharpFormatter.GetIndentationForOffset(formattingOptions, syntaxEditor.Document, syntaxEditor.SelectedView.Selection.FirstOffset) * syntaxEditor.Document.TabSize;
		}
		
		/// <summary>
		/// Occurs when the mouse is hovered over an <see cref="EditorView"/>.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>EditorViewMouseEventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorViewMouseHover(SyntaxEditor syntaxEditor, EditorViewMouseEventArgs e) {
			if ((!this.IntelliPromptQuickInfoEnabled) || (e.HitTestResult.Token == null) || (e.ToolTipText != null))
				return;

			// Set the quick info
			int offset = e.HitTestResult.Token.StartOffset;
			e.ToolTipText = this.GetQuickInfo(DotNetLanguage.CSharp, syntaxEditor, ref offset);
		}

		/// <summary>
		/// Semantically parses the text in the <see cref="MergableLexicalParserManager"/>.
		/// </summary>
		/// <param name="manager">The <see cref="MergableLexicalParserManager"/> that is managing the mergable language and the text to parse.</param>
		/// <returns>An object that contains the results of the semantic parsing operation.</returns>
		protected override object PerformSemanticParse(MergableLexicalParserManager manager) {
			CSharpRecursiveDescentLexicalParser lexicalParser = new CSharpRecursiveDescentLexicalParser(this, manager);
			lexicalParser.InitializeTokens();
			CSharpSemanticParser semanticParser = new CSharpSemanticParser(lexicalParser);
			semanticParser.Parse();
			return semanticParser.CompilationUnit;
		}
		
		/// <summary>
		/// Sets the <see cref="OutliningNode.CollapsedText"/> property for the specified <see cref="OutliningNode"/>
		/// prior to the node being collapsed.
		/// </summary>
		/// <param name="node">The <see cref="OutliningNode"/> that is requesting collapsed text.</param>
		/// <remarks>
		/// The default implementation of this method does nothing.  In that case, the node will use default collapsed text.
		/// </remarks>
		public override void SetOutliningNodeCollapsedText(OutliningNode node) {
			if (node.ParseData is Comment) {
				node.CollapsedText = "/**/";
				if ((((Comment)node.ParseData).Type == CommentType.Documentation) && (node.Length > 0))
					node.CollapsedText = node.GetText(LineTerminator.Newline).Split(new char[] { '\n' })[0] + " ...";
			}
			else if (node.ParseData is RegionPreProcessorDirective) {
				if (node.Length > 0) {
					TextStream stream = node.Document.GetTextStream(node.StartOffset);
					if (stream.ReadToken().ID == CSharpTokenID.RegionPreProcessorDirective) {
						if (stream.Token.ID == CSharpTokenID.PreProcessorDirectiveText)
							node.CollapsedText = stream.TokenText.Trim();
					}
				}
			}
			else if (node.ParseData is UsingDirectiveSection)
				node.CollapsedText = "using ...";
		}
		
		/// <summary>
		/// Displays the <c>About</c> form for the component.
		/// </summary>
		public override void ShowAboutForm() {
			AssemblyInfo.Instance.ShowLicenseForm(license);
		}
		
		/// <summary>
		/// Displays an IntelliPrompt member list in a <see cref="SyntaxEditor"/> based on the current context.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt member list.</param>
		/// <returns>
		/// <c>true</c> if an IntelliPrompt member list is displayed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Only call this method if the <see cref="SyntaxLanguage.IntelliPromptMemberListSupported"/> property is set to <c>true</c>.
		/// </remarks>
		public override bool ShowIntelliPromptMemberList(SyntaxEditor syntaxEditor) {
			return this.ShowIntelliPromptMemberList(DotNetLanguage.CSharp, syntaxEditor, false);
		}
		
		/// <summary>
		/// Displays IntelliPrompt parameter info in a <see cref="SyntaxEditor"/> based on the current context.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt parameter info.</param>
		/// <returns>
		/// <c>true</c> if IntelliPrompt parameter info is displayed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Only call this method if the <see cref="SyntaxLanguage.IntelliPromptParameterInfoSupported"/> property is set to <c>true</c>.
		/// </remarks>
		public override bool ShowIntelliPromptParameterInfo(SyntaxEditor syntaxEditor) {
			// Determine the maximum number of characters to look back at
			int minOffset = Math.Max(0, syntaxEditor.Caret.Offset - 500);

			// Try and find an open '(' or '['
			int targetOffset = -1;
			TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
			stream.GoToCurrentTokenStart();
			bool exitLoop = false;
			IToken token = null;
			while (stream.Offset > minOffset) {
				token = stream.ReadTokenReverse();
				switch (token.ID) {
					case CSharpTokenID.CloseCurlyBrace:
					case CSharpTokenID.CloseParenthesis:
					case CSharpTokenID.CloseSquareBrace:
						stream.GoToPreviousMatchingToken(token);
						break;
					case CSharpTokenID.OpenParenthesis:
					case CSharpTokenID.OpenSquareBrace:
						targetOffset = token.EndOffset;
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}

			if (targetOffset != -1)
				return this.ShowIntelliPromptParameterInfoCore(DotNetLanguage.CSharp, syntaxEditor, targetOffset, (token.ID == CSharpTokenID.OpenSquareBrace));
			else
				return false;
		}

		/// <summary>
		/// Displays IntelliPrompt quick info in a <see cref="SyntaxEditor"/> based on the current context.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt quick info.</param>
		/// <returns>
		/// <c>true</c> if IntelliPrompt quick info is displayed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Only call this method if the <see cref="SyntaxLanguage.IntelliPromptQuickInfoSupported"/> property is set to <c>true</c>.
		/// </remarks>
		public override bool ShowIntelliPromptQuickInfo(SyntaxEditor syntaxEditor) {
			int offset = syntaxEditor.Caret.Offset;

			// Get the info for the context at the caret
			string quickInfo = this.GetQuickInfo(DotNetLanguage.CSharp, syntaxEditor, ref offset);

			// No info was found... try the offset right before the caret
			if (offset > 0) {
				offset = syntaxEditor.Caret.Offset - 1;
				quickInfo = this.GetQuickInfo(DotNetLanguage.CSharp, syntaxEditor, ref offset);
			}

			// Show the quick info if there is any
			if (quickInfo != null) {
				syntaxEditor.IntelliPrompt.QuickInfo.Show(offset, quickInfo);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets a <see cref="TextStatistics"/> for the language that can be used to provide numerous statistics about text
		/// such as word, sentence, character counts as well as readability scores and possibly language-specific statistics.
		/// </summary>
		/// <value>The <see cref="TextStatistics"/> for the language.</value>
		/// <remarks>
		/// To customize the statistics, override this method to return a <see cref="TextStatistics"/> object with customized code for the language.
		/// </remarks>
		public override TextStatistics TextStatistics {
			get {
				return new CSharpTextStatistics();
			}
		}
		
	}

}
