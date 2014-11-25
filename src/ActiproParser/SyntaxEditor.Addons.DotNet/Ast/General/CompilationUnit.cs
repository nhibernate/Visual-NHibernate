using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Context;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a .NET language compilation unit.
	/// </summary>
	public class CompilationUnit : AstNode, ICompilationUnit, ISemanticParseData {

		private IList						documentationCommentTextRanges;
		private bool						hasLanguageTransitions;
		private IList						multiLineCommentTextRanges;
		private string						optionCompare;
		private string						optionExplicit;
		private string						optionInfer;
		private string						optionStrict;
		private IList						regionTextRanges;
		private string						sourceKey;
		private DotNetLanguage				sourceLanguage					= DotNetLanguage.CSharp;
		private ArrayList					syntaxErrors;
		private object						tag;
		private AstNodeList					types;
		
		/// <summary>
		/// Gets the context ID for an extern alias directive section AST node.
		/// </summary>
		/// <value>The context ID for an extern alias directive section AST node.</value>
		public const byte ExternAliasDirectiveSectionContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a using directive section AST node.
		/// </summary>
		/// <value>The context ID for a using directive section AST node.</value>
		public const byte UsingDirectiveSectionContextID = AstNode.AstNodeContextIDBase + 1;

		/// <summary>
		/// Gets the context ID for a global attribute section AST node.
		/// </summary>
		/// <value>The context ID for a global attribute section AST node.</value>
		public const byte GlobalAttributeSectionContextID = AstNode.AstNodeContextIDBase + 2;

		/// <summary>
		/// Gets the context ID for a namespace member AST node.
		/// </summary>
		/// <value>The context ID for a namespace member AST node.</value>
		public const byte NamespaceMemberContextID = AstNode.AstNodeContextIDBase + 3;
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase + 4;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns whether an <see cref="CollapsibleNodeOutliningParser"/> should visit the child nodes of the specified <see cref="IAstNode"/>
		/// to look for collapsible nodes.
		/// </summary>
		/// <param name="node">The <see cref="IAstNode"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the child nodes should be visited; otherwise, <c>false</c>.
		/// </returns>
		bool ICompilationUnit.ShouldVisitChildNodesForOutlining(IAstNode node) {
			if (node is AstNode) {
				switch (((AstNode)node).NodeCategory) {
					case DotNetNodeCategory.CompilationUnit:
					case DotNetNodeCategory.NamespaceDeclaration:
					case DotNetNodeCategory.TypeDeclaration:
					case DotNetNodeCategory.TypeMemberDeclaration:
					case DotNetNodeCategory.TypeMemberDeclarationSection:
						return true;
					default:
						return false;
				}
			}
			return true;
		}
		
		/// <summary>
		/// Adds any extra <see cref="CollapsibleNodeOutliningParserData"/> nodes to the <see cref="CollapsibleNodeOutliningParser"/>,
		/// such as for comments that should be marked as collapsible.
		/// </summary>
		/// <param name="outliningParser">The <see cref="CollapsibleNodeOutliningParser"/> to update.</param>
		void ICompilationUnit.UpdateOutliningParser(CollapsibleNodeOutliningParser outliningParser) {
			if (documentationCommentTextRanges != null) {
				foreach (TextRange textRange in documentationCommentTextRanges) {
					Comment collapsibleNode = new Comment(CommentType.Documentation, textRange, null);
					outliningParser.Add(new CollapsibleNodeOutliningParserData(textRange.StartOffset, OutliningNodeAction.Start, collapsibleNode));
					outliningParser.Add(new CollapsibleNodeOutliningParserData(textRange.EndOffset - 1, OutliningNodeAction.End, collapsibleNode));
				}
			}
			if (multiLineCommentTextRanges != null) {
				foreach (TextRange textRange in multiLineCommentTextRanges) {
					Comment collapsibleNode = new Comment(CommentType.MultiLine, textRange, null);
					outliningParser.Add(new CollapsibleNodeOutliningParserData(textRange.StartOffset, OutliningNodeAction.Start, collapsibleNode));
					outliningParser.Add(new CollapsibleNodeOutliningParserData(textRange.EndOffset - 1, OutliningNodeAction.End, collapsibleNode));
				}
			}
			if (regionTextRanges != null) {
				foreach (TextRange textRange in regionTextRanges) {
					RegionPreProcessorDirective collapsibleNode = new RegionPreProcessorDirective(textRange);
					outliningParser.Add(new CollapsibleNodeOutliningParserData(textRange.StartOffset, OutliningNodeAction.Start, collapsibleNode));
					outliningParser.Add(new CollapsibleNodeOutliningParserData(textRange.EndOffset - 1, OutliningNodeAction.End, collapsibleNode));
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Accepts the specified visitor for visiting this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <remarks>This method is part of the visitor design pattern implementation.</remarks>
		protected override void AcceptCore(AstVisitor visitor) {
			if (visitor.OnVisiting(this)) {
				// Visit children
				if (this.ChildNodeCount > 0)
					this.AcceptChildren(visitor, this.ChildNodes);
			}
			visitor.OnVisited(this);
		}
		
		/// <summary>
		/// Gets the collection of comments that appear in the node.
		/// </summary>
		/// <value>The collection of comments that appear in the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, CompilationUnit.CommentContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the collection of <see cref="TextRange"/> objects that indicate the range of each documentation comment in the compilation unit.
		/// </summary>
		/// <value>The collection of <see cref="TextRange"/> objects that indicate the range of each documentation comment in the compilation unit.</value>
		public IList DocumentationCommentTextRanges {
			get {
				return documentationCommentTextRanges;
			}
			set {
				documentationCommentTextRanges = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the extern alias directives block.
		/// </summary>
		/// <value>The extern alias directives block.</value>
		public ExternAliasDirectiveSection ExternAliasDirectives {
			get {
				return this.GetChildNode(CompilationUnit.ExternAliasDirectiveSectionContextID) as ExternAliasDirectiveSection;
			}
			set {
				this.ChildNodes.Replace(value, CompilationUnit.ExternAliasDirectiveSectionContextID);
			}
		}

		/// <summary>
		/// Returns the closest type or member <see cref="AstNode"/> to the specified offset.
		/// </summary>
		/// <param name="offset">The offset to examine.</param>
		/// <returns>The closest type or member <see cref="AstNode"/> to the specified offset.</returns>
		public AstNode GetClosestTypeOrMember(int offset) {
			return new DotNetContextLocator().FindClosestTypeOrMember(this, offset);
		}
		
		/// <summary>
		/// Returns the <see cref="AstNode"/> that contains the specified offset.
		/// </summary>
		/// <param name="offset">The offset to examine.</param>
		/// <returns>The <see cref="AstNode"/> that contains the specified offset.</returns>
		public AstNode GetContainingNode(int offset) {
			return new DotNetExactContextLocator().FindContainingNode(this, offset);
		}
		
		/// <summary>
		/// Get the imported namespaces and namspace aliases.
		/// </summary>
		/// <param name="contextNode">The <see cref="IAstNode"/> that contains the context.</param>
		/// <param name="importedNamespaces">Returns an array of the imported namespaces.</param>
		/// <param name="namespaceAliases">Returns a <see cref="Hashtable"/> of the namespace aliases.</param>
		public void GetImportedNamespaces(IAstNode contextNode, out string[] importedNamespaces, out Hashtable namespaceAliases) {
			StringCollection namespaceNames = new StringCollection();
			namespaceAliases = new Hashtable();

			// Add any child namespace declarations
			while (contextNode != null) {
				if (contextNode is NamespaceDeclaration) {
					NamespaceDeclaration namespaceDeclaration = (NamespaceDeclaration)contextNode;

					// Add the namespace
					string namespaceFullName = namespaceDeclaration.FullName;
					if ((namespaceFullName != null) && (!namespaceNames.Contains(namespaceFullName)))
						namespaceNames.Add(namespaceFullName);

					// Add the namespace's imported namespaces
					if (namespaceDeclaration.UsingDirectives != null) {
						foreach (UsingDirective usingDirective in namespaceDeclaration.UsingDirectives.Directives) {
							if (usingDirective.NamespaceName != null) {
								if ((usingDirective.Alias != null) && (!namespaceAliases.ContainsKey(usingDirective.Alias)))
									namespaceAliases[usingDirective.Alias] = usingDirective.NamespaceName.Text;
								else if (!namespaceNames.Contains(usingDirective.NamespaceName.Text))
									namespaceNames.Add(usingDirective.NamespaceName.Text);
							}
						}
					}

					// If the namespace has dots in it, then add all the other smaller pieces
					if ((namespaceDeclaration.Name != null) && (namespaceDeclaration.Name.Text != null)) {
						int dotCount = 0;
						int dotIndex = namespaceDeclaration.Name.Text.IndexOf('.');
						while (dotIndex != -1) {
							dotCount++;
							dotIndex = namespaceDeclaration.Name.Text.IndexOf('.', dotIndex + 1);
						}

						dotIndex = namespaceFullName.LastIndexOf('.', namespaceFullName.Length - 1);
						while (dotCount-- > 0) {
							if (!namespaceNames.Contains(namespaceFullName.Substring(0, dotIndex)))
								namespaceNames.Add(namespaceFullName.Substring(0, dotIndex));
							dotIndex = namespaceFullName.LastIndexOf('.', dotIndex - 1);
						}
					}
				}
				contextNode = contextNode.ParentNode;
			}

			// Get the imported namespaces
			if (this.UsingDirectives != null) {
				foreach (UsingDirective usingDirective in this.UsingDirectives.Directives) {
					if (usingDirective.NamespaceName != null) {
						if ((usingDirective.Alias != null) && (!namespaceAliases.ContainsKey(usingDirective.Alias)))
							namespaceAliases[usingDirective.Alias] = usingDirective.NamespaceName.Text;
						else if (!namespaceNames.Contains(usingDirective.NamespaceName.Text))
							namespaceNames.Add(usingDirective.NamespaceName.Text);
					}
				}
			}

			// Create an array of imported namespaces
			importedNamespaces = new string[namespaceNames.Count];
			if (namespaceNames.Count > 0)
				namespaceNames.CopyTo(importedNamespaces, 0);
		}

		/// <summary>
		/// Gets the collection of global attribute sections.
		/// </summary>
		/// <value>The collection of global attribute sections.</value>
		public IAstNodeList GlobalAttributeSections {
			get {
				return new AstNodeListWrapper(this, CompilationUnit.GlobalAttributeSectionContextID);
			}
		}
		
		/// <summary>
		/// Gets whether the compilation unit contains errors.
		/// </summary>
		/// <value>
		/// <c>true</c> if the compilation unit contains errors.
		/// </value>
		public bool HasErrors { 
			get {
				return ((syntaxErrors != null) && (syntaxErrors.Count > 0));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the compilation unit contains any language transitions.
		/// </summary>
		/// <value>
		/// <c>true</c> if the compilation unit contains any language transitions; otherwise, <c>false</c>.
		/// </value>
		public bool HasLanguageTransitions { 
			get {
				return hasLanguageTransitions;
			}
			set {
				hasLanguageTransitions = value;
			}
		}

		/// <summary>
		/// Gets whether the AST node is a language root node.
		/// </summary>
		/// <value>
		/// <c>true</c> if the AST node is a language root node; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// When in a scenario where AST node trees from multiple languages have been merged together,
		/// it is useful to identify where child language AST node trees begin within their parents.
		/// </remarks>
		public override bool IsLanguageRoot {
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets or sets the collection of <see cref="TextRange"/> objects that indicate the range of each multi-line comment in the compilation unit.
		/// </summary>
		/// <value>The collection of <see cref="TextRange"/> objects that indicate the range of each multi-line comment in the compilation unit.</value>
		public IList MultiLineCommentTextRanges {
			get {
				return multiLineCommentTextRanges;
			}
			set {
				multiLineCommentTextRanges = value;
			}
		}

		/// <summary>
		/// Gets the collection of namespaces and members.
		/// </summary>
		/// <value>The collection of namespaces and members.</value>
		public IAstNodeList NamespaceMembers {
			get {
				return new AstNodeListWrapper(this, CompilationUnit.NamespaceMemberContextID);
			}
		}
		
		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.CompilationUnit;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.CompilationUnit;
			}
		}
		
		/// <summary>
		/// Gets or sets the value of the Visual Basic <c>Compare</c> option.
		/// </summary>
		/// <value>The value of the Visual Basic <c>Compare</c> option; or <see langword="null"/> if none is specified.</value>
		public string OptionCompare { 
			get {
				return optionCompare;
			}
			set {
				optionCompare = value;
			}
		}

		/// <summary>
		/// Gets or sets the value of the Visual Basic <c>Explicit</c> option.
		/// </summary>
		/// <value>The value of the Visual Basic <c>Explicit</c> option; or <see langword="null"/> if none is specified.</value>
		public string OptionExplicit { 
			get {
				return optionExplicit;
			}
			set {
				optionExplicit = value;
			}
		}

		/// <summary>
		/// Gets or sets the value of the Visual Basic <c>Infer</c> option.
		/// </summary>
		/// <value>The value of the Visual Basic <c>Infer</c> option; or <see langword="null"/> if none is specified.</value>
		/// <remarks>
		/// This option is part of the Visual Basic 9.0 (.NET 3.5) specification.
		/// </remarks>
		public string OptionInfer { 
			get {
				return optionInfer;
			}
			set {
				optionInfer = value;
			}
		}

		/// <summary>
		/// Gets or sets the value of the Visual Basic <c>Strict</c> option.
		/// </summary>
		/// <value>The value of the Visual Basic <c>Strict</c> option; or <see langword="null"/> if none is specified.</value>
		public string OptionStrict { 
			get {
				return optionStrict;
			}
			set {
				optionStrict = value;
			}
		}

		/// <summary>
		/// Gets or sets the collection of <see cref="TextRange"/> objects that indicate the range of each region pre-processor directive in the compilation unit.
		/// </summary>
		/// <value>The collection of <see cref="TextRange"/> objects that indicate the range of each region pre-processor directive in the compilation unit.</value>
		public IList RegionTextRanges {
			get {
				return regionTextRanges;
			}
			set {
				regionTextRanges = value;
			}
		}

		/// <summary>
		/// Gets or sets the string-based key that identifies the source of the code, which typically is a filename.
		/// </summary>
		/// <value>The string-based key that identifies the source of the code, which typically is a filename.</value>
		public string SourceKey {
			get {
				return sourceKey;
			}
			set {
				sourceKey = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="DotNetLanguage"/> that was used to generate the compilation unit.
		/// </summary>
		/// <value>The <see cref="DotNetLanguage"/> that was used to generate the compilation unit.</value>
		public DotNetLanguage SourceLanguage {
			get {
				return sourceLanguage;
			}
			set {
				sourceLanguage = value;
			}
		}

		/// <summary>
		/// Gets the collection of syntax errors that were found in the compilation unit.
		/// </summary>
		/// <value>The collection of syntax errors that were found in the compilation unit.</value>
		public IList SyntaxErrors {
			get {
				if (syntaxErrors == null)
					syntaxErrors = new ArrayList();

				return syntaxErrors;
			}
		}
		
		/// <summary>
		/// Gets or sets the object that contains user-defined data about the object.
		/// </summary>
		/// <value>
		/// An <see cref="Object"/> that contains user-defined data about the control. The default is <see langword="null"/>.
		/// </value>
		/// <remarks>
		/// Any type derived from the <see cref="Object"/> class can be assigned to this property. 
		/// </remarks>
		public object Tag {
			get {
				return tag;
			}
			set {
				tag = value;
			}
		}

		/// <summary>
		/// Gets the collection of namespaces and members.
		/// </summary>
		/// <value>The collection of namespaces and members.</value>
		public AstNodeList Types {
			get {
				if (types == null)
					types = new AstNodeList(null);

				return types;
			}
		}
		
		/// <summary>
		/// Gets or sets the using directives block.
		/// </summary>
		/// <value>The using directives block.</value>
		public UsingDirectiveSection UsingDirectives {
			get {
				return this.GetChildNode(CompilationUnit.UsingDirectiveSectionContextID) as UsingDirectiveSection;
			}
			set {
				this.ChildNodes.Replace(value, CompilationUnit.UsingDirectiveSectionContextID);
			}
		}

	}
}
