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

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents an abstract base class for a .NET language definition.
	/// </summary>
	[
		Designer(typeof(ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.Design.DotNetSyntaxLanguageDesigner)),
		ToolboxBitmap(typeof(DotNetSyntaxLanguage)),
		ToolboxItem(true)
	]
	public abstract class DotNetSyntaxLanguage : MergableSyntaxLanguage, IDisposable, ISemanticParserServiceProcessor {

		private bool					codeSnippetsEnabled								= true;
		private bool					documentationCommentAutoCompleteEnabled			= true;
		private bool					intelliPromptMemberListEnabled					= true;
		private bool					intelliPromptParameterInfoEnabled				= true;
		private bool					intelliPromptQuickInfoEnabled					= true;
		private bool					sourceProjectContentUpdateEnabled				= true;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INNER TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		#region ParameterInfoContext class

		/// <summary>
		/// Represents the context of a parameter info.
		/// </summary>
		internal class ParameterInfoContext {

			private DotNetContext	context;
			private IDomMember[]	members;
			private IDomType		type;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Initializes a new instance of the <c>ParameterInfoContext</c> class.
			/// </summary>
			/// <param name="context">The <see cref="DotNetContext"/> that contains information about the target context.</param>
			/// <param name="type">The <see cref="IDomType"/> that contains the members.</param>
			/// <param name="members">The array of members that are included in the parameter info.</param>
			public ParameterInfoContext(DotNetContext context, IDomType type, IDomMember[] members) {
				this.context	= context;
				this.type		= type;
				this.members	= members;
			}
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Gets the <see cref="DotNetContext"/> that contains information about the target context.
			/// </summary>
			/// <value>The <see cref="DotNetContext"/> that contains information about the target context.</value>
			public DotNetContext Context {
				get {
					return context;
				}
			}

			/// <summary>
			/// Gets the array of members that are included in the parameter info.
			/// </summary>
			/// <value>The array of members that are included in the parameter info.</value>
			public IDomMember[] Members {
				get {
					return members;
				}
			}
			
			/// <summary>
			/// Gets the <see cref="IDomType"/> that contains the members.
			/// </summary>
			/// <value>The <see cref="IDomType"/> that contains the members.</value>
			public IDomType Type {
				get {
					return type;
				}
			}

		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs before a member list is displayed for a <see cref="SyntaxEditor"/>, allowing for the filtering (removal)
		/// or addition of member list items.
		/// </summary>
		/// <eventdata>
		/// The event handler receives an argument of type <c>IntelliPromptMemberListPreFilterEventArgs</c> containing data related to this event.
		/// </eventdata>
		/// <remarks>
		/// If you choose to perform any filtering, modify the <c>Items</c> property of the event arguments.
		/// </remarks>
		[
			Category("Action"),
			Description("Occurs before a member list is displayed for a SyntaxEditor, allowing for the filtering (removal) or addition of member list items.")
		]
		public event IntelliPromptMemberListPreFilterEventHandler SyntaxEditorIntelliPromptMemberListPreFilter;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CSharpSyntaxLanguage</c> class.
		/// </summary>
		/// <param name="key">The language key.</param>
		public DotNetSyntaxLanguage(string key) : base(key) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Performs a semantic parsing operation using the data in the <see cref="SemanticParserServiceRequest"/>.
		/// </summary>
		/// <param name="request">A <see cref="SemanticParserServiceRequest"/> containing data about what to parse.</param>
		void ISemanticParserServiceProcessor.Process(SemanticParserServiceRequest request) {
			request.SemanticParseData = MergableLexicalParserManager.PerformSemanticParse(this, request.TextBufferReader, request.Filename) as ISemanticParseData;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Completes the documentation tag at the specified offset.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		/// <param name="offset">The offset at which to base the context.</param>
		private void CompleteDocumentationCommentTag(SyntaxEditor syntaxEditor, int offset) {
			// Get the context
			DotNetContext context = this.GetContext(syntaxEditor, offset, false, false);

			// Complete the tag if appropriate
			if ((offset > 0) && (context.Type == DotNetContextType.DocumentationCommentTag) && (context.TargetItem != null) && 
				(context.TargetItem.TextRange == syntaxEditor.Document.Tokens.GetTokenAtOffset(offset - 1).TextRange)) {
				syntaxEditor.SelectedView.InsertSurroundingText(null, String.Format("</{0}>", context.TargetItem.Text));
			}
		}
		
		/// <summary>
		/// Returns the quick info for the <see cref="SyntaxEditor"/> at the specified offset.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		/// <param name="offset">The offset to examine.  The offset is updated to the start of the context.</param>
		/// <returns>The quick info for the <see cref="SyntaxEditor"/> at the specified offset.</returns>
		internal string GetQuickInfo(DotNetLanguage language, SyntaxEditor syntaxEditor, ref int offset) {
			// Get the context
			DotNetContext context = this.GetContext(syntaxEditor, offset, false, false);
			if (context.ProjectResolver == null)
				return null;

			return context.ProjectResolver.GetQuickInfo(language, context);
		}
		
		/// <summary>
		/// Returns the quick info for the current member list item in a <see cref="SyntaxEditor"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		internal void GetQuickInfoForMemberListItem(DotNetLanguage language, SyntaxEditor syntaxEditor) {
			// Get the selected item
			IntelliPromptMemberListItem item = syntaxEditor.IntelliPrompt.MemberList.SelectedItem;
			if (item == null)
				return;

			// Get the project resolver
			DotNetProjectResolver projectResolver = syntaxEditor.Document.LanguageData as DotNetProjectResolver;
			if (projectResolver == null)
				return;

			// Get the context
			DotNetContext context = syntaxEditor.IntelliPrompt.MemberList.Context as DotNetContext;
			if (context == null)
				return;
			
			// Update the item description
			if ((item.ImageIndex == (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Namespace) && (item.Tag is string))
				item.Description = projectResolver.GetQuickInfoForNamespace(language, (string)item.Tag, false);
			else if (item.Tag is IDomType)
				item.Description = projectResolver.GetQuickInfoForType(language, (IDomType)item.Tag, false);
			else if (item.Tag is ParameterDeclaration)
				item.Description = projectResolver.GetQuickInfoForParameter(language, context, 
					((ParameterDeclaration)item.Tag).ParameterType, item.Text, false);
			else if ((item.Tag is VariableDeclarator) && (((VariableDeclarator)item.Tag).IsLocal))
				item.Description = projectResolver.GetQuickInfoForLocalVariable(language, context, 
					((VariableDeclarator)item.Tag).ReturnType, item.Text, false);
			else if (item.Tag is IDomMember) {
				IDomType type = null;
				if (context.TargetItem != null) {
					type = context.TargetItem.ResolvedInfo as IDomType;
					if (type == null)
						type = projectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);
				}
				item.Description = projectResolver.GetQuickInfoForMember(language, context, type, (IDomMember)item.Tag, -1, false);
			}
			else if (item.Tag is CodeSnippet)
				item.Description = projectResolver.GetQuickInfoForCodeSnippet(language, (CodeSnippet)item.Tag);
			else if (item.ImageIndex == (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword)
				item.Description = projectResolver.GetQuickInfoForKeyword(language, item.Text);
		}

		/// <summary>
		/// Inserts a documentation comment based on the current context of the caret.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		/// <param name="delimiterTokenID">The ID of the documentation comment token.</param>
		/// <param name="lineTerminatorTokenID">The ID of the line terminator token.</param>
		/// <param name="whitespaceTokenID">The ID of the whitespace token.</param>
		/// <remarks>
		/// This method should only be called in response to the last '/' character being typed to complete
		/// a documentation comment delimiter.
		/// </remarks>
		internal void InsertDocumentationComment(SyntaxEditor syntaxEditor, int delimiterTokenID, int lineTerminatorTokenID, int whitespaceTokenID) {
			string delimiterString = this.GetTokenString(delimiterTokenID);

			TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
			stream.GoToPreviousToken();
			if ((stream.Token.ID == delimiterTokenID) && (stream.Token.Language == this)) {
				// Try and ensure the compilation unit is up-to-date
				SemanticParserService.WaitForParse(SemanticParserServiceRequest.GetParseHashKey(syntaxEditor.Document, syntaxEditor.Document));

				if (syntaxEditor.Document.SemanticParseData is CompilationUnit) {
					// NOTE: This can be improved because it may be inaccurate as is if the CompilationUnit is not up-to-date with the document text

					// Get the closest type or member
					int offset = syntaxEditor.Caret.Offset - 3;
					AstNode typeOrMemberNode = new DotNetContextLocator().FindClosestTypeOrMember((CompilationUnit)syntaxEditor.Document.SemanticParseData, offset);
					if ((typeOrMemberNode == null) || (typeOrMemberNode.StartOffset < syntaxEditor.Caret.Offset))
						return;
					if ((typeOrMemberNode.NodeCategory != DotNetNodeCategory.TypeDeclaration) && (offset < typeOrMemberNode.ParentNode.StartOffset))
						typeOrMemberNode = typeOrMemberNode.ParentNode as AstNode;
					if ((!(typeOrMemberNode is TypeMemberDeclaration)) || 
						(!(typeOrMemberNode.ParentNode is CompilationUnit)) && ((!(typeOrMemberNode.ParentNode is IBlockAstNode)) || (offset < ((IBlockAstNode)typeOrMemberNode.ParentNode).BlockStartOffset)))
						return;

					// Go backwards to ensure that there is no XML documentation token
					while (!stream.IsAtDocumentStart) {
						stream.GoToPreviousToken();
						if (stream.Token.LexicalState == this.LexicalStates["DocumentationCommentState"])
							return;
						if (!stream.Token.IsWhitespace)
							break;
					}

					// Go forwards to ensure that the line is empty
					stream.Offset = syntaxEditor.Caret.Offset;
					while (!stream.IsAtDocumentEnd) {
						if (stream.Token.Language != this)
							return;
						else if (stream.Token.ID == lineTerminatorTokenID)
							break;
						else if (stream.Token.ID != whitespaceTokenID)
							return;
						stream.GoToNextToken();
					}

					// Go forwards to ensure that there is no XML documentation token
					while (!stream.IsAtDocumentEnd) {
						stream.GoToNextToken();
						if ((stream.Token.Language == this) && (stream.Token.ID == delimiterTokenID))
							return;
						if (!stream.Token.IsWhitespace)
							break;
					}

					// Perform a quick-add of the documentation comment
					string indentText = new string('\t', syntaxEditor.SelectedView.CurrentDocumentLine.TabStopLevel);
					StringBuilder additionalDocumentation = new StringBuilder();
					ICollection genericTypeArguments = null;
					IDomParameter[] parameters = null;
					IDomTypeReference returnType = null;
					if (typeOrMemberNode is IDomMember) {
						genericTypeArguments = ((IDomMember)typeOrMemberNode).GenericTypeArguments;
						parameters = ((IDomMember)typeOrMemberNode).Parameters;
						returnType = ((IDomMember)typeOrMemberNode).ReturnType;
					}
					else if (typeOrMemberNode is IDomType)
						genericTypeArguments = ((IDomType)typeOrMemberNode).GenericTypeArguments;

					if (genericTypeArguments != null) {
						foreach (IDomTypeReference parameter in genericTypeArguments)
							additionalDocumentation.Append(String.Format("\n{0}{1} <typeparam name=\"{2}\"></typeparam>", indentText, delimiterString, parameter.Name));
					}
					if (parameters != null) {
						foreach (IDomParameter parameter in parameters)
							additionalDocumentation.Append(String.Format("\n{0}{1} <param name=\"{2}\"></param>", indentText, delimiterString, parameter.Name));
					}
					if ((returnType != null) && (!returnType.RawFullName.ToLower().EndsWith("void")))
						additionalDocumentation.Append(String.Format("\n{0}{1} <returns></returns>", indentText, delimiterString));
					syntaxEditor.SelectedView.InsertSurroundingText(DocumentModificationType.AutoComplete, String.Format(" <summary>\n{0}{1} ", indentText, delimiterString), 
						String.Format("\n{0}{1} </summary>{2}", indentText, delimiterString, additionalDocumentation));
				}
			}
		}
		
		/// <summary>
		/// Returns whether the caret is currently in a default state where IntelliPrompt can be displayed.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to check.</param>
		/// <param name="lineFeedTokenID">The line feed token ID.</param>
		/// <returns>
		/// <c>true</c> if the caret is currently in a default state; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// 3/2/2010 - Added since IntelliPrompt was showing at the line terminator of single line comments (0D9-131FA7F3-281A)
		/// </remarks>
		internal bool IsCurrentOffsetInDefaultState(SyntaxEditor syntaxEditor, int lineTerminatorTokenID) {
			if (syntaxEditor.Caret.Offset == 0)
				return true;

			TextStream stream = syntaxEditor.Document.GetTextStream(syntaxEditor.Caret.Offset);
			if (stream.Token.LexicalState == this.LexicalStates["DefaultState"]) {
				if ((stream.Token.ID == lineTerminatorTokenID) && (!stream.IsAtDocumentLineStart)) {
					stream.ReadCharacterReverse();
					return (!stream.Token.IsComment);
				}
				else
					return true;
			}
			return false;
		}

        /// <summary>
        /// Added by GFH
        /// </summary>
        /// <param name="language"></param>
        /// <param name="syntaxEditor"></param>
        /// <param name="completeWord"></param>
        /// <returns></returns>
        internal bool ShowIntelliPromptMemberList(DotNetLanguage language, SyntaxEditor syntaxEditor, bool completeWord)
        {
            return ShowIntelliPromptMemberList(language, syntaxEditor, completeWord, null);
        }

		/// <summary>
		/// Provides the core functionality to show an IntelliPrompt member list based on the current context in a <see cref="SyntaxEditor"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt member list.</param>
		/// <param name="completeWord">Whether to complete the word.</param>
		/// <returns>
		/// <c>true</c> if an auto-complete occurred or if a IntelliPrompt member list is displayed; otherwise, <c>false</c>.
		/// </returns>
        /// MODIFIED BY GFH: added extra parameter
        internal bool ShowIntelliPromptMemberList(DotNetLanguage language, SyntaxEditor syntaxEditor, bool completeWord, SyntaxEditor syntaxEditorToDisplayIn)
        {
			// Try and ensure the compilation unit is up-to-date
			SemanticParserService.WaitForParse(SemanticParserServiceRequest.GetParseHashKey(syntaxEditor.Document, syntaxEditor.Document));

			// Get the context
			DotNetContext context = this.GetContext(syntaxEditor, syntaxEditor.Caret.Offset, true, false);

			// Initialize the member list
            // GFH addded
            IntelliPromptMemberList memberList;

            if (syntaxEditorToDisplayIn == null)
                memberList = syntaxEditor.IntelliPrompt.MemberList;
            else
                memberList = syntaxEditorToDisplayIn.IntelliPrompt.MemberList;
			//IntelliPromptMemberList memberList = syntaxEditor.IntelliPrompt.MemberList;
			memberList.ResetAllowedCharacters();
			memberList.Clear();
			memberList.ImageList = SyntaxEditor.ReflectionImageList;
			memberList.Context = context;

			// Get the member list items
			Hashtable memberListItemHashtable = new Hashtable();
			switch (context.Type) {
				case DotNetContextType.AnyCode:
					// Fill with everything
					if (context.ProjectResolver != null) {
						// Fill with child namespace names in the global and imported namespaces
						context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, null);
						foreach (string namespaceName in context.ImportedNamespaces)
							context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, namespaceName);

						// Fill with the types in the global and imported namespaces
						context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, null, DomBindingFlags.Default, true);
						foreach (string namespaceName in context.ImportedNamespaces)
							context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, namespaceName, DomBindingFlags.Default, true);

						// Fill with static members of parent types
						if ((context.TypeDeclarationNode != null) && (context.TypeDeclarationNode.DeclaringType is IDomType))
							context.ProjectResolver.AddMemberListItemsForDeclaringTypeMembers(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TypeDeclarationNode.DeclaringType, DomBindingFlags.Static | DomBindingFlags.AllAccessTypes);
						
						// Fill with nested types
						if (context.TypeDeclarationNode != null)
							context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode, DomBindingFlags.Default, true);
						
						// Fill with members if in a member (pay attention to if member is instance or static)
						if (context.TypeDeclarationNode != null) {
							if (context.MemberDeclarationNode != null) {
								if (!((IDomMember)context.MemberDeclarationNode).IsStatic) {
									// Fill with extension methods
									context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
										context.TypeDeclarationNode, DomBindingFlags.Instance | 
										context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
								}

								// Fill with members
								context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode,
									(this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | 
									DomBindingFlags.Static | (((IDomMember)context.MemberDeclarationNode).IsStatic ? DomBindingFlags.None : DomBindingFlags.Instance) |
									context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
							}
							else {
								// Not within a member so fill with static members
								context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode,
									(this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Static | 
									context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
							}
						}
						
						// Fill with variables defined in the scope
						context.ProjectResolver.AddMemberListItemsForVariables(memberListItemHashtable, context);

						// Fill with language keywords
						this.AddKeywordMemberListItems(memberListItemHashtable);

						// Fill with code snippets
						if (this.CodeSnippetsEnabled)
							context.ProjectResolver.AddMemberListItemsForCodeSnippets(memberListItemHashtable);
					}
					break;
				case DotNetContextType.BaseAccess:
					// If the context is in an instance member declaration...
					if ((context.ProjectResolver != null) && (context.MemberDeclarationNode != null) && (!((IDomMember)context.MemberDeclarationNode).IsStatic)) {
						if (context.TargetItem.Type == DotNetContextItemType.Base) {
							// Fill with extension methods
							context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
								(IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.Public | DomBindingFlags.Family | DomBindingFlags.Assembly);

							// Fill with instance type members
							context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
								(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.Public | DomBindingFlags.Family | DomBindingFlags.Assembly);
						}
					}
					break;
				case DotNetContextType.DocumentationCommentTag:
					// Add tags
					if (context.ProjectResolver != null) {
						context.ProjectResolver.AddMemberListItemsForDocumentationComments(memberListItemHashtable, context,
							(syntaxEditor.Caret.Offset > 0) && (syntaxEditor.Document[syntaxEditor.Caret.Offset - 1] != '<'));
					}
					break;
				case DotNetContextType.AsType:
				case DotNetContextType.IsTypeOfType:
				case DotNetContextType.TryCastType:
				case DotNetContextType.TypeOfType:
					if (context.ProjectResolver != null) {
						if (context.TargetItem != null) {
							switch (context.TargetItem.Type) {
								case DotNetContextItemType.Namespace:
								case DotNetContextItemType.NamespaceAlias:
									// Fill with child namespaces and types
									context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, context.TargetItem.ResolvedInfo.ToString());
									context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TargetItem.ResolvedInfo.ToString(), DomBindingFlags.Default, false);
									break;
								case DotNetContextItemType.Type:
									// Fill with nested types
									context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Default, false);
									break;
							}							
						}
						else {
							// VB requires New added for As specifications
							if ((context.Type == DotNetContextType.AsType) && (language == DotNetLanguage.VB))
								memberListItemHashtable["New"] = new IntelliPromptMemberListItem("New", (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);

							// Fill with native types
							context.ProjectResolver.AddMemberListItemsForNativeTypes(language, memberListItemHashtable, context);

							// Fill with child namespace names in the global and imported namespaces
							context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, null);
							foreach (string namespaceName in context.ImportedNamespaces)
								context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, namespaceName);

							// Fill with the types in the imported namespaces
							context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, null, DomBindingFlags.Default, false);
							foreach (string namespaceName in context.ImportedNamespaces)
								context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, namespaceName, DomBindingFlags.Default, false);
						
							// Fill with nested types
							if (context.TypeDeclarationNode != null)
								context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode, DomBindingFlags.Default, true);
						}
					}
					break;
				case DotNetContextType.NamespaceTypeOrMember:
					if (context.ProjectResolver != null) {
						switch (context.TargetItem.Type) {
							case DotNetContextItemType.Namespace:
							case DotNetContextItemType.NamespaceAlias:
								// Fill with child namespaces and types
								context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, context.TargetItem.ResolvedInfo.ToString());
								context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TargetItem.ResolvedInfo.ToString(), DomBindingFlags.Default, false);
								break;
							case DotNetContextItemType.Constant:
							case DotNetContextItemType.Type:
								// Add nested types
								if (context.TargetItem.ResolvedInfo is IDomType) {
									// Fill with nested types
									context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Default, false);
								}

								// If the context is in a type declaration...
								if (context.TypeDeclarationNode != null) {
									// Fill with static type members
									context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
										(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Static | 
										context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
								}
								break;
							case DotNetContextItemType.Member:
								// If the context is in a type declaration...
								if (context.TypeDeclarationNode != null) {
									// Fill with instance type members of member return type
									IDomType type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);
									if (type != null) {
										// Fill with extension methods
										context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
											type, DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

										// Fill with instance type members
										context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
											type, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
											context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
									}
								}
								break;
							case DotNetContextItemType.ArrayItem:
							case DotNetContextItemType.Parameter:
							case DotNetContextItemType.Variable:
								// If the context is in a member declaration...
								if (context.MemberDeclarationNode != null) {
									// Fill with extension methods
									context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
										(IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance | 
										context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

									// Fill with instance type members
									context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
										(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
										context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
								}
								break;
						}
					}
					break;
				case DotNetContextType.NativeType:
					// If the context is in a member declaration...
					if ((context.ProjectResolver != null) && (context.TypeDeclarationNode != null)) {
						if (context.TargetItem.Type == DotNetContextItemType.Type) {
							// Fill with static type members
							context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
								(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Static | 
								context.AdditionalBindingFlags | DomBindingFlags.Public);
						}
					}
					break;
				case DotNetContextType.NewObjectDeclaration:
					if ((context.ProjectResolver != null) && (context.TypeDeclarationNode != null)) {
						if (context.TargetItem == null) {
							// Fill with child namespace names in the global and imported namespaces
							context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, null);
							foreach (string namespaceName in context.ImportedNamespaces)
								context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, namespaceName);

							// Fill with the creatable types in the global and imported namespaces
							context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, null, DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);
							foreach (string namespaceName in context.ImportedNamespaces)
								context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, namespaceName, DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);

							// Fill with the creatable nested types 
							context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode, DomBindingFlags.Default | DomBindingFlags.HasConstructor, true);
						}
						else {
							switch (context.TargetItem.Type) {
								case DotNetContextItemType.Namespace:
								case DotNetContextItemType.NamespaceAlias:
									// Fill with child namespaces and creatable types
									context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, context.TargetItem.ResolvedInfo.ToString());
									context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TargetItem.ResolvedInfo.ToString(), DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);
									break;
								case DotNetContextItemType.Type:
									// Fill with the creatable nested types 
									context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);

									// Fill with extension methods
									context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
										(IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

									// Fill with instance type members
									context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
										(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
										context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
									break;
							}
						}
					}
					break;
				case DotNetContextType.ThisAccess:
					// If the context is in an instance member declaration...
					if ((context.ProjectResolver != null) && (context.MemberDeclarationNode != null) && (!((IDomMember)context.MemberDeclarationNode).IsStatic)) {
						if (context.TargetItem.Type == DotNetContextItemType.This) {
							// Fill with extension methods
							context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
								(IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

							// Fill with instance type members
							context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
								(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
						}
					}
					break;
				case DotNetContextType.BaseMemberAccess:
				case DotNetContextType.ThisMemberAccess:
					// If the context is in an instance member declaration...
					if ((context.ProjectResolver != null) && (context.MemberDeclarationNode != null) && (!((IDomMember)context.MemberDeclarationNode).IsStatic)) {
						// Fill with instance type members of member return type
						IDomType type = null;
						if (context.TargetItem.ResolvedInfo is IDomType)
							type = (IDomType)context.TargetItem.ResolvedInfo;
						else
							type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);

						if (type != null) {
							// Fill with extension methods
							context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
								type, DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

							// Fill with instance type members
							context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
								type, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
						}
					}
					break;
				case DotNetContextType.StringLiteral:
					// If the context is in a member declaration...
					if (context.ProjectResolver != null) {
						if (context.TargetItem.Type == DotNetContextItemType.StringLiteral) {
							// Fill with extension methods
							context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
								(IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.Public);

							// Fill with string instance type members
							context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
								(IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | 
								context.AdditionalBindingFlags | DomBindingFlags.Public);
						}
					}
					break;
				case DotNetContextType.UsingDeclaration:
					// Fill with namespaces		
					if (context.ProjectResolver != null)
						context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, 
							(context.TargetItem != null ? context.TargetItem.ResolvedInfo.ToString() : String.Empty));
					break;
			}

			// Pre-filter the member list
			this.OnSyntaxEditorIntelliPromptMemberListPreFilter(syntaxEditor, 
				new IntelliPromptMemberListPreFilterEventArgs(syntaxEditor, context, memberListItemHashtable));

			// Add items
			if (memberListItemHashtable.Count > 0) {
				IntelliPromptMemberListItem[] items = new IntelliPromptMemberListItem[memberListItemHashtable.Count];
				memberListItemHashtable.Values.CopyTo(items, 0);
				memberList.AddRange(items);
			}

			// Show the list
			if (memberList.Count > 0) {
				if (context.InitializationTextRange.IsDeleted)
					memberList.Show();
                else if (completeWord)
                {
                    if (syntaxEditorToDisplayIn != null)
                        memberList.CompleteWord(syntaxEditorToDisplayIn.Caret.Offset - (syntaxEditor.Caret.Offset - context.InitializationTextRange.StartOffset), context.InitializationTextRange.Length);
                    else
                        memberList.CompleteWord(context.InitializationTextRange.StartOffset, context.InitializationTextRange.Length);
                }
                else
                {
                    if (syntaxEditorToDisplayIn != null)
                        memberList.Show(syntaxEditorToDisplayIn.Caret.Offset - (syntaxEditor.Caret.Offset - context.InitializationTextRange.StartOffset), context.InitializationTextRange.Length);
                    else
                        memberList.Show(context.InitializationTextRange.StartOffset, context.InitializationTextRange.Length);
                }
				return true;
			}
			else if (memberList.Visible)
				memberList.Abort();

			return false;
		}
		
		/// <summary>
		/// Provides the core functionality to shows IntelliPrompt parameter info based on the current context in a <see cref="SyntaxEditor"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt parameter info.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <param name="forIndexer">Whether the parameter info is for an indexer.</param>
		/// <returns>
		/// <c>true</c> if IntelliPrompt parameter info is displayed; otherwise, <c>false</c>.
		/// </returns>
        internal bool ShowIntelliPromptParameterInfoCore(DotNetLanguage language, SyntaxEditor syntaxEditor, int offset, bool? forIndexer)
        {
            return ShowIntelliPromptParameterInfoCore(language, syntaxEditor, syntaxEditor, offset, forIndexer);
        }

        internal bool ShowIntelliPromptParameterInfoCore(DotNetLanguage language, SyntaxEditor syntaxEditor, SyntaxEditor syntaxEditorToDisplayIn, int offset, bool? forIndexer)
        {
			// Initialize the parameter info
            syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Hide();
            syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Info.Clear();
            syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.SelectedIndex = 0;
			
			// Get the context
			DotNetContext context = this.GetContext(syntaxEditor, offset, true, true);

			// Set the appropriate tooltip text
			if ((context.ProjectResolver != null) && (context.TargetItem != null)) {
				// Move the offset to the start of the context
				if (!context.Items[0].TextRange.IsDeleted)
					offset = context.Items[0].TextRange.StartOffset;

				switch (context.Type) {
					case DotNetContextType.AsType:
					case DotNetContextType.BaseAccess:
					case DotNetContextType.BaseMemberAccess:
					case DotNetContextType.NamespaceTypeOrMember:
					case DotNetContextType.NewObjectDeclaration:
					case DotNetContextType.ThisAccess:
					case DotNetContextType.ThisMemberAccess: {
						IDomType type = null;
						IDomMember[] members = null;

						// If the "for indexer" value still needs to be determined (done in VB since '(' is for both methods and indexers)...
						if (!forIndexer.HasValue) {
							forIndexer = false;

							type = context.TargetItem.ResolvedInfo as IDomType;
							if (type != null) {
								// The context is for a type... look for an indexer
								forIndexer = (context.ProjectResolver.GetMember(context.TypeDeclarationNode, type, null,
									DomBindingFlags.OnlyIndexers | DomBindingFlags.Instance | DomBindingFlags.Static | 
									context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable) != null);

								// Reset value to default
								type = null;
							}
							else {
								// Look for an indexer property
								IDomMember member = context.TargetItem.ResolvedInfo as IDomMember;
								if ((member != null) && (member.MemberType == DomMemberType.Property)) {
									IDomParameter[] parameters = member.Parameters;
									forIndexer = (parameters != null) && (parameters.Length > 0);
								}
							}
						}

						if (forIndexer == true) {
							// Is for an indexer... get the type of the target
							type = context.TargetItem.ResolvedInfo as IDomType;
							if (type == null) {
								IDomMember member = context.TargetItem.ResolvedInfo as IDomMember;
								if ((member != null) && (member.ReturnType != null))
									type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);
							}

							if (type != null) {
								// Get the members for the type
								members = context.ProjectResolver.GetMemberOverloads(context.TypeDeclarationNode, type, null,
									DomBindingFlags.OnlyIndexers | DomBindingFlags.Instance | DomBindingFlags.Static | 
									context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
							}
						}
						else if (context.Type == DotNetContextType.NewObjectDeclaration) {
							// Is for a constructor
							type = context.TargetItem.ResolvedInfo as IDomType;

							if (type != null) {
								// Get the members for the type
								members = context.ProjectResolver.GetMemberOverloads(context.TypeDeclarationNode, type, null,
									DomBindingFlags.DeclaringTypeOnly | DomBindingFlags.OnlyConstructors | DomBindingFlags.Instance | 
									context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);

								// If no constructors were returned, return a default one
								if (((members == null) || (members.Length == 0)) && (type is TypeDeclaration) && ((type.Modifiers & Modifiers.Abstract & Modifiers.Static) == 0)) {
									switch (type.Type) {
										case DomTypeType.Class:
										case DomTypeType.Structure:
											members = new IDomMember[] { new ConstructorDeclaration(Modifiers.Public, new QualifiedIdentifier(type.Name)) };
											((ConstructorDeclaration)members[0]).ParentNode = (TypeDeclaration)type;
											break;
									}
								}
							}
						}
						else if (context.TargetItem.Type == DotNetContextItemType.Member) {
							// For a regular method or constructor... get the type of the item that calls the member
							if (context.Items.Length > 1) {
								type = context.Items[context.Items.Length - 2].ResolvedInfo as IDomType;
								if (type == null) {
									IDomMember member = context.Items[context.Items.Length - 2].ResolvedInfo as IDomMember;
									if ((member != null) && (member.ReturnType != null))
										type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 2);
								}
							}
							if (type == null) {
								IDomMember member = context.TargetItem.ResolvedInfo as IDomMember;
								if ((member != null) && (member.DeclaringType != null)) {
									if (member.DeclaringType.Type == DomTypeType.StandardModule)
										type = member.DeclaringType.Resolve(context.ProjectResolver);
									else
										type = member.DeclaringType as IDomType;
								}
								if (type == null)
									type = context.TypeDeclarationNode;
							}

							// Get the members for the type
							members = context.ProjectResolver.GetMemberOverloads(context.TypeDeclarationNode, type, ((IDomMember)context.TargetItem.ResolvedInfo).Name, 
								(this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance | DomBindingFlags.Static | 
								context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);

							// Get the extension method overloads
							if ((((IDomMember)context.TargetItem.ResolvedInfo).IsExtension) || (!((IDomMember)context.TargetItem.ResolvedInfo).IsStatic)) {
								IDomMember[] extensionMethods = context.ProjectResolver.GetExtensionMethods(context.TypeDeclarationNode, context.ImportedNamespaces, type, ((IDomMember)context.TargetItem.ResolvedInfo).Name, 
									DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
								if ((extensionMethods != null) && (extensionMethods.Length > 0)) {
									if ((members == null) || (members.Length == 0))
										members = extensionMethods;
									else {
										IDomMember[] newMembers = new IDomMember[members.Length + extensionMethods.Length];
										members.CopyTo(newMembers, 0);
										extensionMethods.CopyTo(newMembers, members.Length);
										members = newMembers;
									}
								}
							}
						}

						if ((members != null) && (members.Length > 0) && (
							((forIndexer == false) && ((members[0].MemberType == DomMemberType.Method) || (members[0].MemberType == DomMemberType.Constructor))) ||
							((forIndexer == true) && (members[0].MemberType == DomMemberType.Property) && (members[0].Parameters != null) && (members[0].Parameters.Length > 0))
							)) {

							// Configure the parameter info
                                // GFH changes start
                                //syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.ValidTextRange = context.ParameterTextRange;
                                int syntaxEditorDocLength = syntaxEditor.Document.GetText(LineTerminator.Newline).Length;
                                int diffStart = syntaxEditorDocLength - context.ParameterTextRange.StartOffset;
                                int diffEnd = syntaxEditorDocLength - context.ParameterTextRange.EndOffset;
                                int caretOffset = syntaxEditorToDisplayIn.Caret.Offset;
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.ValidTextRange = new TextRange(caretOffset - diffStart, caretOffset - diffEnd);
                                // GFH changes end
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.CloseDelimiterCharacter = ((forIndexer == true) && (language != DotNetLanguage.VB) ? ']' : ')');

							// Update the parameter index
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.UpdateParameterIndex();

							// Add markup for the first item
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Info.Add(context.ProjectResolver.GetQuickInfoForMember(language,
                                context, type, members[0], syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.ParameterIndex, true));

							// Fill the rest of the items with nulls (they will be filled later)
							for (int index = 1; index < members.Length; index++)
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Info.Add(null);

                            if (syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Info.Count > 0)
                            {
								// Store the member array in the context
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Context = new ParameterInfoContext(context, type, members);

								// Show the parameter info
                                //syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Show(offset);
                                // GFH
                                syntaxEditorToDisplayIn.IntelliPrompt.ParameterInfo.Show(caretOffset - diffStart);
								return true;
							}
						}
						break;
					}
				}
			}

			return false;
		}
			
		/// <summary>
		/// Updates the parameter info's selected item's text.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		/// <param name="force">Whether to force the update.  Otherwise the update only occurs if the text is not yet initialized.</param>
		/// <returns>
		/// <c>true</c> if an update occurred; otherwise, <c>false</c>.
		/// </returns>
		internal bool UpdateParameterInfoSelectedText(DotNetLanguage language, SyntaxEditor syntaxEditor, bool force) {
			// If the parameter info is visible and there is a context available...
			if ((syntaxEditor.IntelliPrompt.ParameterInfo.Visible) && (syntaxEditor.IntelliPrompt.ParameterInfo.Context is ParameterInfoContext)) {
				// Get the context
				ParameterInfoContext context = (ParameterInfoContext)syntaxEditor.IntelliPrompt.ParameterInfo.Context;

				// If the context is valid...
				if ((context.Members.Length == syntaxEditor.IntelliPrompt.ParameterInfo.Info.Count) && 
					(syntaxEditor.IntelliPrompt.ParameterInfo.SelectedIndex <  context.Members.Length)) {
					// If the parameter info item needs markup...
					if ((force) || (syntaxEditor.IntelliPrompt.ParameterInfo.Info[syntaxEditor.IntelliPrompt.ParameterInfo.SelectedIndex] == null)) {
						// Get the project resolver
						DotNetProjectResolver projectResolver = syntaxEditor.Document.LanguageData as DotNetProjectResolver;

						if (projectResolver != null) {
							// Update the parameter info item
							syntaxEditor.IntelliPrompt.ParameterInfo.Info[syntaxEditor.IntelliPrompt.ParameterInfo.SelectedIndex] = 
								projectResolver.GetQuickInfoForMember(language, context.Context, context.Type, 
								context.Members[syntaxEditor.IntelliPrompt.ParameterInfo.SelectedIndex], syntaxEditor.IntelliPrompt.ParameterInfo.ParameterIndex, true);
							return true;
						}
					}
				}
			}
			return false;
		}
	
		/// <summary>
		/// Updates the <see cref="SourceProjectContent"/> with data in the specified <see cref="Document"/>.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="addTypes">Whether to add types or simply clear data.</param>
		private void UpdateSourceProjectContent(Document document, bool addTypes) {
			// Get the .NET project resolver
			DotNetProjectResolver dotNetProjectResolver = document.LanguageData as DotNetProjectResolver;
			if (dotNetProjectResolver == null)
				return;

			// Update the source project content
			dotNetProjectResolver.SourceProjectContent.Clear(document.Filename);  

			// Get the compilation unit
			CompilationUnit compilationUnit = document.SemanticParseData as CompilationUnit;

			// Update the source project content with the new compilation unit type data 
			if ((addTypes) && (compilationUnit != null))
				dotNetProjectResolver.SourceProjectContent.AddRange(document.Filename, compilationUnit.Types);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Add <see cref="IntelliPromptMemberListItem"/> items that indicate the language keywords to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		protected abstract void AddKeywordMemberListItems(Hashtable memberListItemHashtable);

		/// <summary>
		/// Resets the <see cref="AutomaticOutliningBehavior"/> property to its default value.
		/// </summary>
		public override void ResetAutomaticOutliningBehavior() {
			this.AutomaticOutliningBehavior = ActiproSoftware.SyntaxEditor.AutomaticOutliningBehavior.SemanticParseDataChange;
		}
		/// <summary>
		/// Indicates whether the <see cref="AutomaticOutliningBehavior"/> property should be persisted.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the property value has changed from its default; otherwise, <c>false</c>.
		/// </returns>
		public override bool ShouldSerializeAutomaticOutliningBehavior() {
			return (this.AutomaticOutliningBehavior != ActiproSoftware.SyntaxEditor.AutomaticOutliningBehavior.SemanticParseDataChange);
		}
		
		/// <summary>
		/// Expands the code block selection level.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> whose selection will be modified.</param>
		/// <returns>
		/// <c>true</c> if the selection is modified; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// When executed, the selection will change to be over the offset range of the code block that contains the current selection.
		/// This method can be called multiple times to walk up the hierarchy of code blocks.
		/// Only call this method if the <see cref="CodeBlockSelectionSupported"/> property is set to <c>true</c>.
		/// </remarks>
		public override bool CodeBlockSelectionExpand(SyntaxEditor syntaxEditor) {
			// Get the compilation unit 
			CompilationUnit compilationUnit = syntaxEditor.Document.SemanticParseData as CompilationUnit;
			if (compilationUnit != null) {
				IAstNode containingNode = compilationUnit.FindNodeRecursive(Math.Max(0, syntaxEditor.SelectedView.Selection.FirstOffset - 1));
				while (containingNode != null) {
					if (containingNode is AstNode) {
						switch (((AstNode)containingNode).NodeCategory) {
							case DotNetNodeCategory.NamespaceDeclaration:
							case DotNetNodeCategory.Statement:
							case DotNetNodeCategory.TypeDeclaration:
							case DotNetNodeCategory.TypeMemberDeclaration:
							case DotNetNodeCategory.TypeMemberDeclarationSection:
								if ((containingNode.HasStartOffset) && (containingNode.HasEndOffset)) {
									// Select the node
									syntaxEditor.SelectedView.Selection.CodeBlockExpand(containingNode.TextRange);
									return true;
								}
								break;
						}
					}

					// Move up a level
					containingNode = containingNode.ParentNode;
				}
			}

			return false;
		}
		
		/// <summary>
		/// Gets whether code block selection features are supported by the language.
		/// </summary>
		/// <value>
		/// <c>true</c> if code block selection features are supported by the language; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If this property is <c>true</c>, then the <see cref="CodeBlockSelectionExpand"/> and <see cref="SyntaxLanguage.CodeBlockSelectionContract"/> methods may be used.
		/// </remarks>
		public override bool CodeBlockSelectionSupported {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets or sets whether the automatic code snippet features are enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the automatic code snippet features are enabled; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether the automatic code snippet features are enabled."),
			DefaultValue(true)
		]
		public bool CodeSnippetsEnabled {
			get {
				return codeSnippetsEnabled;
			}
			set {
				codeSnippetsEnabled = value;
			}
		}

		/// <summary>
		/// Gets or sets whether documentation comments will be added when a <c>///</c> is typed.
		/// </summary>
		/// <value>
		/// <c>true</c> if documentation comments will be added when a <c>///</c> is typed; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether documentation comments will be added when a /// is typed."),
			DefaultValue(true)
		]
		public bool DocumentationCommentAutoCompleteEnabled {
			get {
				return documentationCommentAutoCompleteEnabled;
			}
			set {
				documentationCommentAutoCompleteEnabled = value;
			}
		}

		/// <summary>
		/// Resets the <see cref="SyntaxLanguage.ErrorDisplayEnabled"/> property to its default value.
		/// </summary>
		public override void ResetErrorDisplayEnabled() {
			this.ErrorDisplayEnabled = true;
		}
		/// <summary>
		/// Indicates whether the <see cref="SyntaxLanguage.ErrorDisplayEnabled"/> property should be persisted.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the property value has changed from its default; otherwise, <c>false</c>.
		/// </returns>
		public override bool ShouldSerializeErrorDisplayEnabled() {
			return !this.ErrorDisplayEnabled;
		}

		/// <summary>
		/// Gets the <see cref="DotNetContext"/> for the specified offset.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
		/// <param name="offset">The offset at which to base the context.</param>
		/// <param name="beforeOffset">Whether to return the context before the offset.</param>
		/// <param name="forParameterInfo">Whether to return the context for parameter info.</param>
		/// <returns>The <see cref="DotNetContext"/> for the specified offset.</returns>
		protected abstract DotNetContext GetContext(SyntaxEditor syntaxEditor, int offset, bool beforeOffset, bool forParameterInfo);

		/// <summary>
		/// Gets whether IntelliPrompt code snippet features are supported by the language.
		/// </summary>
		/// <value>
		/// <c>true</c> if IntelliPrompt code snippet features are supported by the language; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If this property is <c>true</c>, then the <see cref="ShowIntelliPromptInsertSnippetPopup"/> method may be used.
		/// </remarks>
		public override bool IntelliPromptCodeSnippetsSupported {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets or sets whether the automatic IntelliPrompt member list features are enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the automatic IntelliPrompt member list features are enabled; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether the automatic IntelliPrompt member list features are enabled."),
			DefaultValue(true)
		]
		public bool IntelliPromptMemberListEnabled {
			get {
				return intelliPromptMemberListEnabled;
			}
			set {
				intelliPromptMemberListEnabled = value;
			}
		}
		
		/// <summary>
		/// Gets whether IntelliPrompt member list features are supported by the language.
		/// </summary>
		/// <value>
		/// <c>true</c> if IntelliPrompt member list features are supported by the language; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If this property is <c>true</c>, then the <see cref="SyntaxLanguage.IntelliPromptCompleteWord"/> and <see cref="SyntaxLanguage.ShowIntelliPromptMemberList"/> methods may be used.
		/// </remarks>
		public override bool IntelliPromptMemberListSupported {
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Gets or sets whether the automatic IntelliPrompt parameter info features are enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the automatic IntelliPrompt parameter info features are enabled; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether the automatic IntelliPrompt parameter info features are enabled."),
			DefaultValue(true)
		]
		public bool IntelliPromptParameterInfoEnabled {
			get {
				return intelliPromptParameterInfoEnabled;
			}
			set {
				intelliPromptParameterInfoEnabled = value;
			}
		}
		
		/// <summary>
		/// Gets whether IntelliPrompt parameter info features are supported by the language.
		/// </summary>
		/// <value>
		/// <c>true</c> if IntelliPrompt parameter info features are supported by the language; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If this property is <c>true</c>, then the <see cref="SyntaxLanguage.ShowIntelliPromptParameterInfo"/> method may be used.
		/// </remarks>
		public override bool IntelliPromptParameterInfoSupported {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets or sets whether the automatic IntelliPrompt quick info features are enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the automatic IntelliPrompt quick info features are enabled; otherwise, <c>false</c>.
		/// </value>
		[
			Category("Behavior"),
			Description("Indicates whether the automatic IntelliPrompt quick info features are enabled."),
			DefaultValue(true)
		]
		public bool IntelliPromptQuickInfoEnabled {
			get {
				return intelliPromptQuickInfoEnabled;
			}
			set {
				intelliPromptQuickInfoEnabled = value;
			}
		}
		
		/// <summary>
		/// Gets whether IntelliPrompt quick info features are supported by the language.
		/// </summary>
		/// <value>
		/// <c>true</c> if IntelliPrompt quick info features are supported by the language; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If this property is <c>true</c>, then the <see cref="SyntaxLanguage.ShowIntelliPromptQuickInfo"/> method may be used.
		/// </remarks>
		public override bool IntelliPromptQuickInfoSupported {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets the <see cref="DotNetLanguage"/> that this language represents.
		/// </summary>
		/// <value>The <see cref="DotNetLanguage"/> that this language represents.</value>
		public abstract DotNetLanguage LanguageType { get; }

		/// <summary>
		/// Occurs after automatic outlining is performed on a <see cref="Document"/> that uses this language.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> that is being modified.</param>
		/// <param name="e">A <c>DocumentModificationEventArgs</c> that contains the event data.</param>
		/// <remarks>
		/// A <see cref="DocumentModification"/> may or may not be passed in the event arguments, depending on if the outlining
		/// is performed in the main thread.
		/// </remarks>
		protected override void OnDocumentAutomaticOutliningComplete(Document document, DocumentModificationEventArgs e) {
			// If programmatically setting the text of a document...
			if (e.IsProgrammaticTextReplacement) {
				// Collapse all outlining region nodes
				document.Outlining.RootNode.CollapseDescendants("RegionPreProcessorDirective");
			}
		}
		
		/// <summary>
		/// Occurs after the <see cref="Document.Filename"/> property is changed on a <see cref="Document"/> that uses this language.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> that is being modified.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnDocumentFilenameChanged(Document document, EventArgs e) {
			if (sourceProjectContentUpdateEnabled)
				this.UpdateSourceProjectContent(document, true);
		}

		/// <summary>
		/// Occurs before the <see cref="Document.Filename"/> property is changed on a <see cref="Document"/> that uses this language.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> that is being modified.</param>
		/// <param name="e">An <c>EventArgs</c> that contains the event data.</param>
		protected override void OnDocumentFilenameChanging(Document document, EventArgs e) {
			if (sourceProjectContentUpdateEnabled)
				this.UpdateSourceProjectContent(document, false);
		}
		
		/// <summary>
		/// Occurs after the value of the <see cref="Document.SemanticParseData"/> property has changed
		/// on a <see cref="Document"/> that uses this language.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> that is being modified.</param>
		/// <param name="e">A <c>EventArgs</c> that contains the event data.</param>
		protected override void OnDocumentSemanticParseDataChanged(Document document, EventArgs e) {
			if (sourceProjectContentUpdateEnabled)
				this.UpdateSourceProjectContent(document, true);
		}
		
		/// <summary>
		/// Occurs before a member list is displayed for a <see cref="SyntaxEditor"/>, allowing for the filtering (removal)
		/// or addition of member list items.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>IntelliPromptMemberListPreFilterEventArgs</c> that contains the event data.</param>
		protected virtual void OnSyntaxEditorIntelliPromptMemberListPreFilter(SyntaxEditor syntaxEditor, IntelliPromptMemberListPreFilterEventArgs e) {
			if (this.SyntaxEditorIntelliPromptMemberListPreFilter != null)
				this.SyntaxEditorIntelliPromptMemberListPreFilter(this, e);
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
					case '<':
						if ((this.IntelliPromptMemberListEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(syntaxEditor.SelectedView.GetCurrentToken().LexicalState == this.LexicalStates["DocumentationCommentState"])) {
							// Show the member list for documentation comment tags
							this.ShowIntelliPromptMemberList(syntaxEditor);
						}
						break;
					case '>':
						if ((this.DocumentationCommentAutoCompleteEnabled) && (!syntaxEditor.SelectedView.Selection.IsReadOnly) &&
							(syntaxEditor.SelectedView.GetCurrentToken().LexicalState == this.LexicalStates["DocumentationCommentState"])) {
							// Complete a documentation tag if appropriate
							this.CompleteDocumentationCommentTag(syntaxEditor, syntaxEditor.Caret.Offset);
						}
						break;
				}
			}
		}

		/// <summary>
		/// Occurs before a <see cref="SyntaxEditor.KeyTyping"/> event is raised 
		/// for a <see cref="SyntaxEditor"/> that has a <see cref="Document"/> using this language.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
		/// <param name="e">An <c>KeyTypingEventArgs</c> that contains the event data.</param>
		protected override void OnSyntaxEditorKeyTyping(SyntaxEditor syntaxEditor, KeyTypingEventArgs e) {
			// If the TAB might be to insert a code snippet...
			if ((codeSnippetsEnabled) && (e.KeyData == Keys.Tab)) {
				// Get a project resolver
				DotNetProjectResolver projectResolver = syntaxEditor.Document.LanguageData as DotNetProjectResolver;
				if (projectResolver == null)
					return;

				// Check for a code snippet shortcut
				e.Cancel = syntaxEditor.IntelliPrompt.CodeSnippets.CheckForCodeSnippetShortcut(projectResolver.CodeSnippetFolders);
			}
		}

		/// <summary>
		/// Performs automatic outlining over the specified <see cref="TextRange"/> of the <see cref="Document"/>.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="parseTextRange">A <see cref="TextRange"/> indicating the offset range to parse.</param>
		/// <returns>A <see cref="TextRange"/> containing the offset range that was modified by outlining.</returns>
		public override TextRange PerformAutomaticOutlining(Document document, TextRange parseTextRange) {
			// If there is another pending semantic parser request (probably due to typing), assume that the existing outlining structure 
			//   in the document is more up-to-date and wait until the final request comes through before updating the outlining again
			if (!SemanticParserService.HasPendingRequest(SemanticParserServiceRequest.GetParseHashKey(document, document)))
				return new CollapsibleNodeOutliningParser().UpdateOutlining(document, parseTextRange, document.SemanticParseData as CompilationUnit);
			else
				return TextRange.Deleted;
		}
		
		/// <summary>
		/// Semantically parses the specified <see cref="TextRange"/> of the <see cref="Document"/>.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="parseTextRange">A <see cref="TextRange"/> indicating the offset range to parse.</param>
        /// <param name="flags">A <see cref="SemanticParseFlags"/> that indicates semantic parse flags.</param>
		public override void PerformSemanticParse(Document document, TextRange parseTextRange, SemanticParseFlags flags) {
			SemanticParserService.Parse(new SemanticParserServiceRequest(SemanticParserServiceRequest.MediumPriority,
				document, parseTextRange, flags, this, document));
		}
		
		/// <summary>
		/// Displays the <c>About</c> form for the component.
		/// </summary>
		public abstract void ShowAboutForm();
		
		/// <summary>
		/// Shows the IntelliPrompt code snippet <c>Insert Snippet</c> popup.
		/// </summary>
		/// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the popup.</param>
		/// <param name="labelText">The text of the label on the popup.</param>
		/// <param name="type">A <see cref="CodeSnippetTypes"/> indicating the type of code snippets to include.</param>
		/// <returns>
		/// <c>true</c> if the popup is displayed; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// Only call this method if the <see cref="IntelliPromptCodeSnippetsSupported"/> property is set to <c>true</c>.
		/// The popup is not displayed if there is nothing that matches the specified <see cref="CodeSnippetTypes"/> filter.
		/// </remarks>
		public override bool ShowIntelliPromptInsertSnippetPopup(SyntaxEditor syntaxEditor, string labelText, CodeSnippetTypes type) {
			// Get a project resolver
			DotNetProjectResolver projectResolver = syntaxEditor.Document.LanguageData as DotNetProjectResolver;
			if (projectResolver == null)
				return false;

			if (projectResolver.CodeSnippetFolders.Count == 1)
				return syntaxEditor.IntelliPrompt.CodeSnippets.ShowInsertSnippetPopup(syntaxEditor.Caret.Offset, labelText, projectResolver.CodeSnippetFolders[0], type);
			else
				return syntaxEditor.IntelliPrompt.CodeSnippets.ShowInsertSnippetPopup(syntaxEditor.Caret.Offset, labelText, projectResolver.CodeSnippetFolders, type);
		}

		/// <summary>
		/// Gets whether smart indent features are supported by the language.
		/// </summary>
		/// <value>
		/// <c>true</c> if smart indent features are supported by the language; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// If this property is <c>true</c>, then the language has implemented smart indent handler code in <see cref="SyntaxLanguage.OnSyntaxEditorSmartIndent"/>.
		/// </remarks>
		public override bool SmartIndentSupported {
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Gets or sets whether the <see cref="DotNetProjectResolver.SourceProjectContent"/> for the <see cref="DotNetProjectResolver"/>
		/// is automatically updated when a document's semantic parse data is updated.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="DotNetProjectResolver.SourceProjectContent"/> for the <see cref="DotNetProjectResolver"/>
		/// is automatically updated when a document's semantic parse data is updated; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// Source project content is only updated if the <see cref="Document"/> has a <see cref="Document.Filename"/> specified.
		/// In addition, a <see cref="DotNetProjectResolver"/> must be set to the <see cref="Document.LanguageData"/> property
		/// and the <see cref="Document.SemanticParseData"/> property must contain a <see cref="CompilationUnit"/>.
		/// </remarks>
		[
			Category("Behavior"),
			Description("Indicates whether the SourceProjectContent for the DotNetProjectResolver is automatically updated when a document's semantic parse data is updated."),
			DefaultValue(true)
		]
		public bool SourceProjectContentUpdateEnabled {
			get {
				return sourceProjectContentUpdateEnabled;
			}
			set {
				sourceProjectContentUpdateEnabled = value;
			}
		}
		
	}

}
