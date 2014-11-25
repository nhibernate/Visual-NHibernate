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

namespace ActiproSoftware.SyntaxEditor.Addons.VB {

	/// <summary>
	/// Represents a <c>Visual Basic</c> language definition.
	/// </summary>
	#if !NOLICENSECHECK
	[LicenseProvider(typeof(AddonsDotNetLicenseProvider))]
	#endif
	public class VBSyntaxLanguage : DotNetSyntaxLanguage {

		private VBLexicalParser			lexicalParser									= new VBLexicalParser();

		private ActiproSoftware.Products.ActiproLicense	license;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>VBSyntaxLanguage</c> class.
		/// </summary>
		public VBSyntaxLanguage() : base("VB") {
			this.ExampleText = @"' The automated VB IntelliPrompt features demoed here
' are part of the .NET Languages add-on, sold 
' separately from SyntaxEditor

' Hover over any identifier to get quick info
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace ActiproSoftware.SyntaxEditor

	''' <summary>
	''' Start typing anywhere in the code to get
	''' automated IntelliPrompt support like member
	''' lists, complete word (Ctrl+Space), and
	''' parameter info tips.
	''' </summary>
	Public Class TestClass
		Inherits Control

		''' <summary>
		''' Doubles the specified number.
		''' </summary>
		''' <param name=""x"">The number to double.</param>
		''' <returns>The result.</returns>
		Public Function [Double](ByVal x As Integer) As Integer
			' Type Me. to see a member list and then
			' Add( to see a parameter info tip
			
			Return Me.Add(x, x)
		End Function
 
		''' <summary>
		''' Adds two numbers together.
		''' </summary>
		''' <param name=""x"">The first number.</param>
		''' <param name=""y"">The second number.</param>
		''' <returns>The result.</returns>
		Public Function Add(ByVal x As Integer, ByVal y As Integer) As Integer
			Dim result As Integer = x + y 
			Return result
		End Function

		''' <summary>
		''' Adds three number together.
		''' </summary>
		''' <param name=""x"">The first number.</param>
		''' <param name=""y"">The second number.</param>
		''' <param name=""z"">The third number.</param>
		''' <returns>The result.</returns>
		Public Function Add(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer) As Integer
			Dim result As Integer = x + y + z 
			Return result
		End Function

		' Type ''' on the next line to see method
		' XML documentation comment auto-complete

		Public Function Subtract(ByVal x As Integer, ByVal y As Integer) As Integer
			Dim result As Integer = x - y 
			Return result
		End Function
	End Class
End Namespace
";
			
			// Initialize highlighting styles
			this.HighlightingStyles.Add(new HighlightingStyle("KeywordStyle", null, Color.Blue, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("CommentStyle", null, Color.Green, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("DocumentationCommentStyle", null, Color.Gray, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("StringStyle", null, Color.Maroon, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("NumberStyle", null, Color.Purple, Color.Empty));
			this.HighlightingStyles.Add(new HighlightingStyle("XMLLiteralStyle", null, Color.Gray, Color.Empty));

			// Initialize lexical states
			this.LexicalStates.Add(new DefaultLexicalState(VBLexicalStateID.Default, "DefaultState"));
			this.LexicalStates.Add(new DefaultLexicalState(VBLexicalStateID.DocumentationComment, "DocumentationCommentState"));
			this.LexicalStates.Add(new DefaultLexicalState(VBLexicalStateID.PreProcessorDirective, "PreProcessorDirectiveState"));
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
			license = LicenseManager.Validate(typeof(VBSyntaxLanguage), this) as ActiproSoftware.Products.ActiproLicense;
			#endif
		}
		
		/// <summary>
		/// This constructor is for designer use only and should never be called by your code.
		/// </summary>
		/// <param name="container">An <see cref="IContainer"></see> that represents the container for the component.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public VBSyntaxLanguage(IContainer container) : this() {
			// Add to the container
			container.Add(this);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		//////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Add <see cref="IntelliPromptMemberListItem"/> items that indicate the language keywords to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		protected override void AddKeywordMemberListItems(Hashtable memberListItemHashtable) {
			for (int id = VBTokenID.ContextualKeywordStart + 1; id < VBTokenID.ContextualKeywordEnd; id++) {
				string keyword = VBTokenID.GetTokenKey(id);
				memberListItemHashtable[keyword] = new IntelliPromptMemberListItem(keyword, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);
			}
			for (int id = VBTokenID.KeywordStart + 1; id < VBTokenID.KeywordEnd; id++) {
				string keyword = VBTokenID.GetTokenKey(id);
				if (keyword.EndsWith("Keyword"))
					keyword = keyword.Substring(0, keyword.Length - 7);
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
			return new VBToken(startOffset, 0, LexicalParseFlags.None, null, new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)VBTokenID.DocumentEnd));
		}

		/// <summary>
		/// Creates an <see cref="IToken"/> that represents an invalid range of text.
		/// </summary>
		/// <param name="startOffset">The start offset of the <see cref="IToken"/>.</param>
		/// <param name="length">The length of the <see cref="IToken"/>.</param>
		/// <param name="lexicalState">The <see cref="ILexicalState"/> that contains the token.</param>
		/// <returns>An <see cref="IToken"/> that represents an invalid range of text.</returns>
		public override IToken CreateInvalidToken(int startOffset, int length, ILexicalState lexicalState) {
			return new VBToken(startOffset, length, LexicalParseFlags.None, null, new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)VBTokenID.Invalid));
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
			return new VBToken(startOffset, length, lexicalParseFlags, parentToken, lexicalParseData);
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
				return VBContext.GetContextBeforeOffset(syntaxEditor.Document, offset, compilationUnit, projectResolver, forParameterInfo);
			else
				return VBContext.GetContextAtOffset(syntaxEditor.Document, offset, compilationUnit, projectResolver);
		}
		
		/// <summary>
		/// Returns the <see cref="HighlightingStyle"/> for the specified <see cref="IToken"/>.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>The <see cref="HighlightingStyle"/> for the specified <see cref="IToken"/>.</returns>
		public override HighlightingStyle GetHighlightingStyle(IToken token) {
			switch (token.LexicalStateID) {
				case VBLexicalStateID.Default:
					switch (token.ID) {
						case VBTokenID.SingleLineComment:
						case VBTokenID.RemComment:
							return this.HighlightingStyles["CommentStyle"];
						case VBTokenID.StringLiteral:
						case VBTokenID.CharacterLiteral:
						case VBTokenID.DateLiteral:
							return this.HighlightingStyles["StringStyle"];
						case VBTokenID.DecimalIntegerLiteral:
						case VBTokenID.HexadecimalIntegerLiteral:
						case VBTokenID.OctalIntegerLiteral:
						case VBTokenID.FloatingPointLiteral:
							return this.HighlightingStyles["NumberStyle"];
						case VBTokenID.DocumentationCommentDelimiter:
							return this.HighlightingStyles["DocumentationCommentStyle"];
						case VBTokenID.XmlLiteral:
							return this.HighlightingStyles["XMLLiteralStyle"];
						default:
							if (
								((token.ID > VBTokenID.KeywordStart) && (token.ID < VBTokenID.KeywordEnd)) ||
								((token.ID > VBTokenID.ContextualKeywordStart) && (token.ID < VBTokenID.ContextualKeywordEnd)) ||
								((token.ID > VBTokenID.PreProcessorDirectiveKeywordStart) && (token.ID < VBTokenID.PreProcessorDirectiveKeywordEnd))
								)
								return this.HighlightingStyles["KeywordStyle"];
							else
								return token.LexicalState.DefaultHighlightingStyle;
					}
				case VBLexicalStateID.DocumentationComment:
					switch (token.ID) {
						case VBTokenID.DocumentationCommentTag:
							return this.HighlightingStyles["DocumentationCommentStyle"];
						default:
							return token.LexicalState.DefaultHighlightingStyle;
					}
				case VBLexicalStateID.PreProcessorDirective:
					switch (token.ID) {
						case VBTokenID.SingleLineComment:
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
			if ((tokenID > (int)VBTokenID.KeywordStart) && (tokenID < (int)VBTokenID.KeywordEnd)) {
				string keyword = VBTokenID.GetTokenKey(tokenID);
				if (keyword.EndsWith("Keyword"))
					return keyword.Substring(0, keyword.Length - 7);
				return keyword;
			}
			if ((tokenID > (int)VBTokenID.ContextualKeywordStart) && (tokenID < (int)VBTokenID.ContextualKeywordEnd))
				return VBTokenID.GetTokenKey(tokenID);
			
			switch (tokenID) {
				// General
				case VBTokenID.DocumentEnd:
					return "Document end";
				case VBTokenID.Whitespace:
					return "Whitespace";
				case VBTokenID.LineTerminator:
					return "Line terminator";
				case VBTokenID.SingleLineComment:
					return "Single-line comment";
				case VBTokenID.RemComment:
					return "Rem comment";
				case VBTokenID.DocumentationCommentDelimiter:
					return "'''";
				case VBTokenID.DocumentationCommentText:
					return "Documentation comment text";
				case VBTokenID.DocumentationCommentTag:
					return "Documentation comment tag";
				case VBTokenID.DecimalIntegerLiteral:
					return "Integer number";
				case VBTokenID.HexadecimalIntegerLiteral:
					return "Hexidecimal integer number";
				case VBTokenID.OctalIntegerLiteral:
					return "Octal integer number";
				case VBTokenID.FloatingPointLiteral:
					return "Floating point number";
				case VBTokenID.CharacterLiteral:
					return "Character";
				case VBTokenID.StringLiteral:
					return "String";
				case VBTokenID.DateLiteral:
					return "Date";
				case VBTokenID.Identifier:
					return "Identifier";
				// Operators
				case VBTokenID.LineContinuation:
					return "_";
				case VBTokenID.OpenParenthesis:
					return "(";
				case VBTokenID.CloseParenthesis:
					return ")";
				case VBTokenID.OpenCurlyBrace:
					return "{";
				case VBTokenID.CloseCurlyBrace:
					return "}";
				case VBTokenID.Comma:
					return ",";
				case VBTokenID.Dot:
					return ".";
				case VBTokenID.Colon:
					return ":";
				case VBTokenID.ColonEquals:
					return ":=";
				case VBTokenID.ExclamationPoint:
					return "!";
				case VBTokenID.StringConcatenation:
					return "&";
				case VBTokenID.Multiplication:
					return "*";
				case VBTokenID.Addition:
					return "+";
				case VBTokenID.Subtraction:
					return "-";
				case VBTokenID.FloatingPointDivision:
					return "/";
				case VBTokenID.IntegerDivision:
					return "\\";
				case VBTokenID.Exponentiation:
					return "^";
				case VBTokenID.LessThan:
					return "<";
				case VBTokenID.Equality:
					return "==";
				case VBTokenID.GreaterThan:
					return ">";
				case VBTokenID.LessThanOrEqual:
					return "<=";
				case VBTokenID.GreaterThanOrEqual:
					return ">=";
				case VBTokenID.Inequality:
					return "<>";
				case VBTokenID.LeftShift:
					return "<<";
				case VBTokenID.RightShift:
					return ">>";
				case VBTokenID.StringConcatenationAssignment:
					return "&=";
				case VBTokenID.MultiplicationAssignment:
					return "*=";
				case VBTokenID.AdditionAssignment:
					return "+=";
				case VBTokenID.SubtractionAssignment:
					return "-=";
				case VBTokenID.FloatingPointDivisionAssignment:
					return "/=";
				case VBTokenID.IntegerDivisionAssignment:
					return "\\=";
				case VBTokenID.ExponentiationAssignment:
					return "^=";
				case VBTokenID.LeftShiftAssignment:
					return "<<=";
				case VBTokenID.RightShiftAssignment:
					return ">>=";
				// Pre-processor directives
				case VBTokenID.ConstPreProcessorDirective:
					return "#Const";
				case VBTokenID.IfPreProcessorDirective:
					return "#If";
				case VBTokenID.ElseIfPreProcessorDirective:
					return "#ElseIf";
				case VBTokenID.ElsePreProcessorDirective:
					return "#Else";
				case VBTokenID.EndIfPreProcessorDirective:
					return "#End If";
				case VBTokenID.ExternalSourcePreProcessorDirective:
					return "#ExternalSource";
				case VBTokenID.EndExternalSourcePreProcessorDirective:
					return "#End ExternalSource";
				case VBTokenID.RegionPreProcessorDirective:
					return "#Region";
				case VBTokenID.EndRegionPreProcessorDirective:
					return "#End Region";
				case VBTokenID.ExternalChecksumPreProcessorDirective:
					return "#ExternalChecksum";
				case VBTokenID.PreProcessorDirectiveText:
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
			return this.ShowIntelliPromptMemberList(DotNetLanguage.VB, syntaxEditor, true);
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetLanguage"/> that this language represents.
		/// </summary>
		/// <value>The <see cref="DotNetLanguage"/> that this language represents.</value>
		public override DotNetLanguage LanguageType { 
			get {
				return DotNetLanguage.VB;
			}
		}

		/// <summary>
		/// Resets the <see cref="SyntaxLanguage.LineCommentDelimiter"/> property to its default value.
		/// </summary>
		public override void ResetLineCommentDelimiter() {
			this.LineCommentDelimiter = "'";
		}
		/// <summary>
		/// Indicates whether the <see cref="SyntaxLanguage.LineCommentDelimiter"/> property should be persisted.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the property value has changed from its default; otherwise, <c>false</c>.
		/// </returns>
		public override bool ShouldSerializeLineCommentDelimiter() {
			return (this.LineCommentDelimiter != "'");
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
			this.GetQuickInfoForMemberListItem(DotNetLanguage.VB, syntaxEditor);
		}
		
		/// <summary>
		/// Occurs after the parameter index of the IntelliPrompt parameter info is changed while the parameter info is visible.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorIntelliPromptParameterInfoParameterIndexChanged(SyntaxEditor syntaxEditor, EventArgs e) {
			if (this.UpdateParameterInfoSelectedText(DotNetLanguage.VB, syntaxEditor, true))
				syntaxEditor.IntelliPrompt.ParameterInfo.MeasureAndResize(syntaxEditor.IntelliPrompt.ParameterInfo.Bounds.Location);
		}

		/// <summary>
		/// Occurs after the selected index of the IntelliPrompt parameter info is changed while the parameter info is visible.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorIntelliPromptParameterInfoSelectedIndexChanged(SyntaxEditor syntaxEditor, EventArgs e) {
			// NOTE: This code provides late-binding for the parameter info
			this.UpdateParameterInfoSelectedText(DotNetLanguage.VB, syntaxEditor, false);
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
							(this.IsCurrentOffsetInDefaultState(syntaxEditor, VBTokenID.LineTerminator))) {
							// Check the previous token to ensure it's not in a comment or number
							TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset - 1);
							switch (stream.Token.ID) {
								case VBTokenID.DecimalIntegerLiteral:
								case VBTokenID.FloatingPointLiteral:
								case VBTokenID.HexadecimalIntegerLiteral:
								case VBTokenID.OctalIntegerLiteral:
									break;
								default:
									// Show the member list for code
									this.ShowIntelliPromptMemberList(syntaxEditor);
									break;
							}
						}
						break;
					case '(':
						if ((this.IntelliPromptParameterInfoEnabled) && (syntaxEditor.Caret.Offset > 1) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(this.IsCurrentOffsetInDefaultState(syntaxEditor, VBTokenID.LineTerminator))) {
							// Show the parameter info for code
							this.ShowIntelliPromptParameterInfoCore(DotNetLanguage.VB, syntaxEditor, syntaxEditor.Caret.Offset, null);
						}
						break;
					case ')':
						if ((this.IntelliPromptParameterInfoEnabled) && (syntaxEditor.Caret.Offset > 1) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(this.IsCurrentOffsetInDefaultState(syntaxEditor, VBTokenID.LineTerminator))) {
							// Show the parameter info for the parent context level if there is one
							this.ShowIntelliPromptParameterInfo(syntaxEditor);
						}
						break;
					case '\'': 
						if ((this.DocumentationCommentAutoCompleteEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly)) {
							// Insert a documentation comment
							this.InsertDocumentationComment(syntaxEditor, VBTokenID.DocumentationCommentDelimiter, VBTokenID.LineTerminator, VBTokenID.Whitespace);
						}
						break;
					case ',':
						if ((this.IntelliPromptParameterInfoEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) && (!syntaxEditor.IntelliPrompt.ParameterInfo.Visible) &&
							(syntaxEditor.Caret.Offset > 1) && (this.IsCurrentOffsetInDefaultState(syntaxEditor, VBTokenID.LineTerminator))) {
							// Show the parameter info for the context level if parameter info is not already displayed
							this.ShowIntelliPromptParameterInfo(syntaxEditor);
						}
						break;
					case ' ':
						if ((this.IntelliPromptMemberListEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly)) {
							// Do a fast check to see if a using keyword is before the caret
							TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
							stream.GoToCurrentTokenStart();
							while ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID == VBTokenID.Whitespace)) {}
							switch (stream.Token.ID) {
								case VBTokenID.As:
								case VBTokenID.Imports:
								case VBTokenID.New:
									// Show the member list 
									this.ShowIntelliPromptMemberList(syntaxEditor);
									break;
								case VBTokenID.End: {
									// Auto-indent the End statement
									VBFormatter.AutoIndent(syntaxEditor.Document, syntaxEditor.Caret.Offset, "End");
									break;
								}
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
						syntaxEditor.SelectedView.ReplaceSelectedText(DocumentModificationType.Typing, "''' ", DocumentModificationOptions.CheckReadOnly);
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
			e.IndentAmount = VBFormatter.GetIndentationForOffset(syntaxEditor.Document, syntaxEditor.SelectedView.Selection.FirstOffset) * syntaxEditor.Document.TabSize;
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
			e.ToolTipText = this.GetQuickInfo(DotNetLanguage.VB, syntaxEditor, ref offset);
		}

		/// <summary>
		/// Semantically parses the text in the <see cref="MergableLexicalParserManager"/>.
		/// </summary>
		/// <param name="manager">The <see cref="MergableLexicalParserManager"/> that is managing the mergable language and the text to parse.</param>
		/// <returns>An object that contains the results of the semantic parsing operation.</returns>
		protected override object PerformSemanticParse(MergableLexicalParserManager manager) {
			VBRecursiveDescentLexicalParser lexicalParser = new VBRecursiveDescentLexicalParser(this, manager);
			lexicalParser.InitializeTokens();
			VBSemanticParser semanticParser = new VBSemanticParser(lexicalParser);
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
				node.CollapsedText = "'";
				if ((((Comment)node.ParseData).Type == CommentType.Documentation) && (node.Length > 0))
					node.CollapsedText = node.GetText(LineTerminator.Newline).Split(new char[] { '\n' })[0] + " ...";
			}
			else if (node.ParseData is RegionPreProcessorDirective) {
				if (node.Length > 0) {
					TextStream stream = node.Document.GetTextStream(node.StartOffset);
					if (stream.ReadToken().ID == VBTokenID.RegionPreProcessorDirective) {
						if (stream.Token.ID == VBTokenID.PreProcessorDirectiveText)
							node.CollapsedText = stream.TokenText.Trim();
					}
				}
			}
			else if (node.ParseData is UsingDirectiveSection)
				node.CollapsedText = "Imports ...";
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
			return this.ShowIntelliPromptMemberList(DotNetLanguage.VB, syntaxEditor, false);
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

			// Try and find an open '('
			int targetOffset = -1;
			TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
			stream.GoToCurrentTokenStart();
			bool exitLoop = false;
			IToken token = null;
			while (stream.Offset > minOffset) {
				token = stream.ReadTokenReverse();
				switch (token.ID) {
					case VBTokenID.CloseCurlyBrace:
					case VBTokenID.CloseParenthesis:
						stream.GoToPreviousMatchingToken(token);
						break;
					case VBTokenID.OpenParenthesis:
						targetOffset = token.EndOffset;
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}

			if (targetOffset != -1)
				return this.ShowIntelliPromptParameterInfoCore(DotNetLanguage.VB, syntaxEditor, targetOffset, null);
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
			string quickInfo = this.GetQuickInfo(DotNetLanguage.VB, syntaxEditor, ref offset);

			// No info was found... try the offset right before the caret
			if (offset > 0) {
				offset = syntaxEditor.Caret.Offset - 1;
				quickInfo = this.GetQuickInfo(DotNetLanguage.VB, syntaxEditor, ref offset);
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
				return new VBTextStatistics();
			}
		}
		

	}

}
