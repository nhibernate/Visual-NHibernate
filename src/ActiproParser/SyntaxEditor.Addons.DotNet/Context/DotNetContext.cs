using System;
using System.Collections;
using System.Text;
using ActiproSoftware.ComponentModel;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Context {
	
	/// <summary>
	/// Represents the base class for a .NET language context at a specific offset.
	/// </summary>
	public abstract class DotNetContext {

		private CompilationUnit			compilationUnit;
		private IAstNode				containingNode;
		private string					documentationComment;
		private string[]				importedNamespaces;
		private TextRange				initializationTextRange	= TextRange.Deleted;
		private DotNetContextItem[]		items;
		private SyntaxLanguage			language;
		private IDomMember				memberDeclarationNode;
		private TextRange				parameterTextRange		= TextRange.Deleted;
		private DotNetContext			parentContext;
		private DotNetProjectResolver	projectResolver;
		private bool					startsWithDot;
		private int						targetOffset;
		private DotNetContextType		type					= DotNetContextType.None;
		private IDomType				typeDeclarationNode;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DotNetContext</c> class.
		/// </summary>
		/// <param name="parentContext">The <see cref="DotNetContext"/>, if any, that created this context.</param>
		/// <param name="language">The <see cref="SyntaxLanguage"/> that created the context.</param>
		/// <param name="targetOffset">The target offset.</param>
		/// <param name="type">The <see cref="DotNetContextType"/> that describes the type of context.</param>
		/// <param name="items">The <see cref="ArrayList"/> of context items.</param>
		public DotNetContext(DotNetContext parentContext, SyntaxLanguage language, int targetOffset, DotNetContextType type, ArrayList items) {
			// Initialize parameters
			this.parentContext = parentContext;
			this.language = language;
			this.targetOffset = targetOffset;
			this.type = type;

			// Initialize items
			if ((items != null) && (items.Count > 0)) {
				this.items = new DotNetContextItem[items.Count];
				items.CopyTo(this.items, 0);
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets additional <see cref="DomBindingFlags"/> that should be used when doing reflection with this context.
		/// </summary>
		/// <value>Additional <see cref="DomBindingFlags"/> that should be used when doing reflection with this context.</value>
		internal DomBindingFlags AdditionalBindingFlags {
			get {
				return this.GetAdditionalBindingFlags(items != null ? items.Length : 1);
			}
		}
		
		/// <summary>
		/// Gets additional <see cref="DomBindingFlags"/> that should be used when doing reflection with this context.
		/// </summary>
		/// <value>Additional <see cref="DomBindingFlags"/> that should be used when doing reflection with this context.</value>
		internal DomBindingFlags GetAdditionalBindingFlags(int itemIndex) {
			DomBindingFlags flags = (this.IsLanguageCaseSensitive ? DomBindingFlags.None : DomBindingFlags.IgnoreCase);

			// Check to see if this is a possible object reference
			if ((items != null) && (items.Length > 0)) {
				switch (items[0].Type) {
					case DotNetContextItemType.Base:
					case DotNetContextItemType.This:
						if (items.Length == 1)
							flags |= DomBindingFlags.ObjectReference;
						break;
					case DotNetContextItemType.Unknown:
						// This gets called when resolving context							
						if (items.Length == 1)
							flags |= DomBindingFlags.ObjectReference;
						break;
				}

				if ((items.Length >= 2) && (itemIndex < items.Length) && (items[itemIndex].Type == DotNetContextItemType.Unknown)) {
					// This gets called when resolving context
					flags |= DomBindingFlags.ObjectReference;
				}
			}

			return flags;
		}

		/// <summary>
		/// Gets the <see cref="IAstNodeList"/> of statements for the specified node, if there are any.
		/// </summary>
		/// <param name="node">The <see cref="IAstNode"/> to examine.</param>
		/// <returns>The <see cref="IAstNodeList"/> of statements for the specified node, if there are any.</returns>
		internal static IAstNodeList GetStatements(IAstNode node) {
			if (node is BlockStatement)
				return ((BlockStatement)node).Statements;
			else if (node is ConstructorDeclaration)
				return ((ConstructorDeclaration)node).Statements;
			else if (node is DestructorDeclaration)
				return ((DestructorDeclaration)node).Statements;
			else if (node is MethodDeclaration)
				return ((MethodDeclaration)node).Statements;
			else if (node is OperatorDeclaration)
				return ((OperatorDeclaration)node).Statements;
			else if (node is SwitchSection)
				return ((SwitchSection)node).Statements;
			else
				return null;
		}

		/// <summary>
		/// Returns the <see cref="IDomTypeReference"/> return type for a variable declarator by attempting to resolve the return type
		/// if it was implicitly typed.
		/// </summary>
		/// <param name="variableDeclarator">The <see cref="VariableDeclarator"/> to examine.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <returns>The resolved <see cref="IDomTypeReference"/>.</returns>
		internal IDomTypeReference GetVariableDeclaratorReturnType(VariableDeclarator variableDeclarator, IDomType contextType) {
			// If the variable declarator is not impliclity typed (var name = value;), return the specified return type
			if (!variableDeclarator.IsImplicitlyTyped)
				return variableDeclarator.ReturnType;

			// If there is a return type...
			if (variableDeclarator.ReturnType != null) {
				if (variableDeclarator.ReturnType.Name == TypeReference.AnonymousTypeName) {
					// Try and construct an anonymous type
					IDomType anonymousType = ExpressionResolver.ResolveAnonymousType(this, contextType, variableDeclarator.Initializer);
					if (anonymousType != null)
						return anonymousType;
				}
				else if (variableDeclarator.ReturnType.Name != "System.Object") {
					// If the return type is not Object, it was already resolved
					return variableDeclarator.ReturnType;
				}
			}

			// If an initializer was specified...
			if (variableDeclarator.Initializer != null) {
				IDomTypeReference returnTypeReference = ExpressionResolver.Resolve(this, contextType, variableDeclarator.Initializer);
				if (returnTypeReference != null)
					return returnTypeReference;
			}

			// Default to return the original type that was specified
			return variableDeclarator.ReturnType;
		}
		
		/// <summary>
		/// Gets the variable name from a <see cref="GetVariables"/> method result.
		/// </summary>
		/// <param name="node">The <see cref="IAstNode"/> to examine.</param>
		/// <returns>The variable name from a <see cref="GetVariables"/> method result.</returns>
		internal static string GetVariableName(IAstNode node) {
			if (node is VariableDeclarator) {
				VariableDeclarator variableDeclarator = (VariableDeclarator)node;
				return variableDeclarator.Name.Text;
			}
			else if (node is ParameterDeclaration) {
				ParameterDeclaration parameter = (ParameterDeclaration)node;
				return parameter.Name;
			}
			else 
				return null;
		}
		
		/// <summary>
		/// Gets the variables that are declared before the specified <see cref="DotNetContext"/>.
		/// </summary>
		/// <param name="context">The <see cref="DotNetContext"/> that specifies context information.</param>
		/// <param name="variableDeclarators">Returns an <see cref="ArrayList"/> containing the variables that are declared before the specified <see cref="DotNetContext"/>.</param>
		/// <returns>The <see cref="IAstNode"/> at which searching ends.</returns>
		internal static IAstNode GetVariables(DotNetContext context, ArrayList variableDeclarators) {
			IAstNode node = context.ContainingNode;

			// If in the middle of statements but in a statement container, try and find the child statement that is closest
			bool startOnStatement = false;
			IAstNodeList statements = DotNetContext.GetStatements(node);
			if ((statements != null) && (statements.Count > 0)) {
				bool statementFound = false;
				foreach (IAstNode statement in statements) {
					if (statement.StartOffset >= context.TargetOffset)
						node = statement;
				}
				
				if (!statementFound) {
					node = statements[statements.Count - 1];
					startOnStatement = true;
				}
			}

			while (node != null) {
				// Quit if the node is a type or member declaration
				if (node is TypeMemberDeclaration) {
					// Get the parameters collection
					IAstNodeList parameters = null;
					if (node is ConstructorDeclaration)
						parameters = ((ConstructorDeclaration)node).Parameters;
					else if (node is PropertyDeclaration)
						parameters = ((PropertyDeclaration)node).Parameters;
					else if (node is MethodDeclaration)
						parameters = ((MethodDeclaration)node).Parameters;
					else if (node is OperatorDeclaration)
						parameters = ((OperatorDeclaration)node).Parameters;

					// Look for a matching parameter
					if (parameters != null) {
						foreach (ParameterDeclaration parameter in parameters) {
							if ((parameter.ParameterType != null) && (parameter.ParameterType.Name != null))
								variableDeclarators.Add(parameter);
						}
					}

					node = null;
					break;
				}

				// Find the statement (loop up to look for one)
				while ((node != null) && (!(node is Statement))) {
					// Check for a lambda expression 
					LambdaExpression lambdaExpression = node as LambdaExpression;
					if (lambdaExpression != null) {
						// Add lambda expression parameter declarations
						foreach (ParameterDeclaration parameter in lambdaExpression.Parameters) {
							// Don't do type check here... do resolution later (special case for lambda expressions)
							variableDeclarators.Add(parameter);
						}
					}

					node = node.ParentNode;
				}
				if (node == null)
					break;

				if (node is Statement) {
					// Save the statement
					AstNode statement = (Statement)node;

					// Check various statements that have variable declarations
					if (statement is ForStatement) {
						foreach (IAstNode initializer in ((ForStatement)statement).Initializers) {
							if (initializer is LocalVariableDeclaration) {
								LocalVariableDeclaration variableDeclaration = (LocalVariableDeclaration)initializer;
								foreach (VariableDeclarator declarator in variableDeclaration.Variables) {
									if ((declarator.Name != null) && (declarator.ReturnType != null) && (declarator.ReturnType.Name != null))
										variableDeclarators.Add(declarator);
								}
							}
						}
					}
					else if (statement is ForEachStatement) {
						ForEachStatement forEachStatement = (ForEachStatement)statement;
						LocalVariableDeclaration variableDeclaration = forEachStatement.VariableDeclaration as LocalVariableDeclaration;
						if (variableDeclaration != null) {
							foreach (VariableDeclarator declarator in variableDeclaration.Variables) {
								if ((declarator.Name != null) && (declarator.ReturnType != null) && (declarator.ReturnType.Name != null)) {
									// 10/27/2010 - If using the variable has been implicitly typed, look for an IEnumerable<T> on the enumerator
									if ((forEachStatement.Expression != null) && (declarator.IsImplicitlyTyped) && (declarator.ReturnType.Name == "System.Object")) {
										IDomTypeReference expressionTypeReference = ExpressionResolver.Resolve(context, context.TypeDeclarationNode, forEachStatement.Expression);
										if ((expressionTypeReference != null) && (context.ProjectResolver != null)) {
											IDomType expressionType = expressionTypeReference.Resolve(context.projectResolver);
											if (expressionType != null) {
												IDomType[] types = context.ProjectResolver.GetTypeInheritanceHierarchyAndImplementedInterfaces(expressionType);
												if (types != null) {
													foreach (IDomType type in types) {
														if ((type.FullName == "System.Collections.Generic.IEnumerable`1") && (type.GenericTypeArguments != null) &&
															(type.GenericTypeArguments.Count > 0)) {
															IDomTypeReference[] genericTypeArguments = new IDomTypeReference[type.GenericTypeArguments.Count];
															type.GenericTypeArguments.CopyTo(genericTypeArguments, 0);

															if ((genericTypeArguments[0] != null) && (!genericTypeArguments[0].IsGenericParameter)) {
																declarator.ReturnType = new TypeReference(genericTypeArguments[0].FullName, declarator.ReturnType.TextRange);
																break;
															}
														}
													}
												}
											}
											
										}
									}

									variableDeclarators.Add(declarator);
								}
							}
						}
					}
					else if (statement is UsingStatement) {
						foreach (IAstNode resourceAcquisition in ((UsingStatement)statement).ResourceAcquisitions) {
							LocalVariableDeclaration variableDeclaration = resourceAcquisition as LocalVariableDeclaration;
							if (variableDeclaration != null) {
								foreach (VariableDeclarator declarator in variableDeclaration.Variables) {
									if ((declarator.Name != null) && (declarator.ReturnType != null) && (declarator.ReturnType.Name != null))
										variableDeclarators.Add(declarator);
								}
							}
						}
					}

					// Get the statements collection
					node = node.ParentNode;
					if (node is CatchClause) {
						VariableDeclarator declarator = ((CatchClause)node).VariableDeclarator;
						if ((declarator != null) && (declarator.Name != null) && (declarator.ReturnType != null) && (declarator.ReturnType.Name != null))
							variableDeclarators.Add(declarator);
					}
					statements = DotNetContext.GetStatements(node);
					if (statements == null)
						continue;
					
					// Loop backwards through the statements
					int statementIndex = statements.IndexOf(statement) - (startOnStatement ? 0 : 1);
					startOnStatement = false;
					for (; statementIndex >= 0; statementIndex--) {
						if (statements[statementIndex] is LocalVariableDeclaration) {
							LocalVariableDeclaration variableDeclaration = (LocalVariableDeclaration)statements[statementIndex];
							foreach (VariableDeclarator declarator in variableDeclaration.Variables) {
								if ((declarator.Name != null) && (declarator.ReturnType != null) && (declarator.ReturnType.Name != null))
									variableDeclarators.Add(declarator);
							}
						}
					}
				}
			}

			return node;
		}

		/// <summary>
		/// Inserts new <see cref="DotNetContextItem"/> objects into the existing items.
		/// </summary>
		/// <param name="index">The index at which to insert the items.</param>
		/// <param name="newItems">The array of <see cref="DotNetContextItem"/> objects to insert.</param>
		internal void InsertItems(int index, DotNetContextItem[] newItems) {
			ArrayList itemsList = (items != null ? new ArrayList(items) : new ArrayList());
			itemsList.InsertRange(index, newItems);
			items = (DotNetContextItem[])itemsList.ToArray(typeof(DotNetContextItem));
		}
		
		/// <summary>
		/// Parses the specified XML tag.
		/// </summary>
		/// <param name="tagText">The XML tag.</param>
		/// <param name="isOpen">Returns whether the tag is open.</param>
		/// <param name="isClosed">Returns whether the tag is closed.</param>
		/// <param name="tagName">Returns the tag name.</param>
		internal static void ParseXmlTag(string tagText, out bool isOpen, out bool isClosed, out string tagName) {
			isClosed = tagText.StartsWith("</");
			if (isClosed)
				isOpen = false;
			else 
				isOpen = !tagText.EndsWith("/>");

			tagName = String.Empty;
			for (int index = (isClosed ? 2 : 1); index < tagText.Length; index++) {
				if (!Char.IsLetter(tagText[index]))
					break;
				tagName += tagText[index];
			}
		}

		/// <summary>
		/// Adds a type item into the items list.
		/// </summary>
		/// <param name="itemIndex">The index at which to insert the item.</param>
		/// <param name="type">The <see cref="IDomType"/> resolved info.</param>
		internal void PrefixWithTypeItem(int itemIndex, IDomType type)  {
			DotNetContextItem newItem = new DotNetContextItem(DotNetContextItemType.Type,
				new TextRange(items[itemIndex].TextRange.StartOffset), type.Name);
			newItem.ResolvedInfo = type;
			this.InsertItems(itemIndex, new DotNetContextItem[] { newItem });
		}
		
		/// <summary>
		/// Resolves the arguments in the <see cref="DotNetContextItem.ArgumentsText"/>.
		/// </summary>
		/// <param name="item">The <see cref="DotNetContextItem"/> to examine.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		internal virtual void ResolveArguments(DotNetContextItem item, IDomType contextType) {}
		
		/// <summary>
		/// Resolves a <see cref="DotNetContext"/>.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> being parsed.</param>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		internal virtual void ResolveForCode(Document document, CompilationUnit compilationUnit, DotNetProjectResolver projectResolver) {}

		/// <summary>
		/// Returns the appropriate array or indexer access for the specified item.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="itemIndex">The item index.</param>
		/// <param name="allowArray">Whether to look for an array access.</param>
		/// <param name="allowDefaultProperty">Whether to allow searching for a default indexer property.</param>
		/// <returns>The appropriate array or indexer access for the specified item.</returns>
		internal object ResolveToArrayOrIndexer(DotNetProjectResolver projectResolver, IDomType targetType, int itemIndex, bool allowArray, bool allowDefaultProperty) {
			// If a possible matching array access has been found...
			if ((allowArray) && (targetType is DomResolvedTypeReference) && (targetType.ArrayRanks != null) && 
				(targetType.ArrayRanks.Length == this.Items[itemIndex].IndexerParameterCounts.Length)) {

				bool matches = true;
				for (int index = 0; index < targetType.ArrayRanks.Length; index++) {
					if (targetType.ArrayRanks[index] != this.Items[itemIndex].IndexerParameterCounts[index]) {
						matches = false;
						break;
					}
				}
				if (matches)
					return ((DomResolvedTypeReference)targetType).CoreType;
			}

			for (int index = 0; index < this.Items[itemIndex].IndexerParameterCounts.Length; index++) {
				// Check for indexer access
				IDomMember[] indexers = projectResolver.GetIndexers(this.TypeDeclarationNode, targetType,
					DomBindingFlags.Instance | this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes, this.Items[itemIndex].IndexerParameterCounts[index]);
				if (indexers.Length > 0) {
					// Quit if on the last indexer
					if (index == this.Items[itemIndex].IndexerParameterCounts.Length - 1)
						return indexers[0];

					if (indexers[0].ReturnType == null)
						break;

					// Get the type of the return type reference
					targetType = indexers[0].ReturnType.Resolve(projectResolver);
					if (targetType == null)
						break;
				}
				else
					break;
			}

			// 1/24/2011 - If we can search for a default property...
			if (allowDefaultProperty) {
				// Return the target type
				return targetType;
			}

			return null;

		}
		
		/// <summary>
		/// Gets or sets whether the context starts with a dot.
		/// </summary>
		/// <value>
		/// <c>true</c> if the context starts with a dot; otherwise, <c>false</c>.
		/// </value>
		internal bool StartsWithDot {
			get {
				return startsWithDot;
			}
			set {
				startsWithDot = value;
			}
		}

		/// <summary>
		/// Returns whether the context is the start of an infinite recursion.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the context is the start of an infinite recursion; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// This method looks up the parent context chain to see if a matching context is already an ancestor.
		/// </remarks>
		internal bool IsStartOfRecursion() {
			string key = this.ToKeyString();
			DotNetContext context = parentContext;
			while (context != null) {
				// A matching ancestor was found
				if (context.ToKeyString() == key)
					return true;

				context = context.parentContext;
			}
			return false;
		}
		
		/// <summary>
		/// Converts the object to a <c>String</c> that can be used to identify the context.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		private string ToKeyString() {
			StringBuilder text = new StringBuilder();
			text.Append(targetOffset);
			text.Append(": ");
			if (items != null) {
				foreach (DotNetContextItem item in items) {
					if (text[text.Length - 1] != ' ')
						text.Append('.');
					text.Append(item.Text);
				}
			}
			return text.ToString();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="CompilationUnit"/> for the context.
		/// </summary>
		/// <value>The <see cref="CompilationUnit"/> for the context.</value>
		public CompilationUnit CompilationUnit {
			get {
				return compilationUnit;
			}
			set {
				compilationUnit = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the AST node that contains the context, if any.
		/// </summary>
		/// <value>The AST node that contains the context, if any.</value>
		public IAstNode ContainingNode {
			get {
				return containingNode;
			}
			set {
				containingNode = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the documentation comment related to the context.
		/// </summary>
		/// <value>The documentation comment related to the context.</value>
		/// <remarks>
		/// This property is only filled in when the context is for a documentation comment.
		/// </remarks>
		public string DocumentationComment {
			get {
				return documentationComment;
			}
			set {
				documentationComment = value;
			}
		}
	
		/// <summary>
		/// Gets or sets the array of imported namespaces for the context.
		/// </summary>
		/// <value>The array of imported namespaces for the context.</value>
		public string[] ImportedNamespaces {
			get {
				return importedNamespaces;
			}
			set {
				importedNamespaces = value;
			}
		}
	
		/// <summary>
		/// Gets or sets the <see cref="TextRange"/> with which the context was initialized.
		/// </summary>
		/// <value>The <see cref="TextRange"/> with which the context was initialized.</value>
		public TextRange InitializationTextRange {
			get {
				return initializationTextRange;
			}
			set {
				initializationTextRange = value;
			}
		}
	
		/// <summary>
		/// Gets whether the language is case sensitive.
		/// </summary>
		/// <value>
		/// <c>true</c> if the language is case sensitive; otherwise, <c>false</c>.
		/// </value>
		public abstract bool IsLanguageCaseSensitive { get; }

		/// <summary>
		/// Gets the array of <see cref="DotNetContextItem"/> objects the describe the current context.
		/// </summary>
		/// <value>The array of <see cref="DotNetContextItem"/> objects the describe the current context.</value>
		public DotNetContextItem[] Items { 
			get {
				return items;
			}
		}

		/// <summary>
		/// Gets the <see cref="SyntaxLanguage"/> that created the context.
		/// </summary>
		/// <value>The <see cref="SyntaxLanguage"/> that created the context.</value>
		public SyntaxLanguage Language {
			get {
				return language;
			}
		}
	
		/// <summary>
		/// Gets or sets the member AST node that contains the context, if any.
		/// </summary>
		/// <value>The member AST node that contains the context, if any.</value>
		public IDomMember MemberDeclarationNode {
			get {
				return memberDeclarationNode;
			}
			set {
				memberDeclarationNode = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="TextRange"/> that contains the parameters.
		/// </summary>
		/// <value>The <see cref="TextRange"/> that contains the parameters.</value>
		/// <remarks>
		/// This property is only filled in for certain situations.
		/// </remarks>
		public TextRange ParameterTextRange {
			get {
				return parameterTextRange;
			}
			set {
				parameterTextRange = value;
			}
		}
	
		/// <summary>
		/// Gets or sets the <see cref="ProjectResolver"/> for the context.
		/// </summary>
		/// <value>The <see cref="ProjectResolver"/> for the context.</value>
		public DotNetProjectResolver ProjectResolver {
			get {
				return projectResolver;
			}
			set {
				projectResolver = value;
			}
		}

		/// <summary>
		/// Gets the target <see cref="DotNetContextItem"/>, which is the last item in the <see cref="Items"/> collection.
		/// </summary>
		/// <value>The target <see cref="DotNetContextItem"/>, which is the last item in the <see cref="Items"/> collection.</value>
		public DotNetContextItem TargetItem {
			get {
				if (items != null) 
					return items[items.Length - 1];
				else
					return null;
			}
		}

		/// <summary>
		/// Gets the target offset of the context.
		/// </summary>
		/// <value>The target offset of the context.</value>
		public int TargetOffset {
			get {
				return targetOffset;
			}
		}
	
		/// <summary>
		/// Gets or sets the <see cref="DotNetContextType"/> that describes the type of context.
		/// </summary>
		/// <value>The <see cref="DotNetContextType"/> that describes the type of context.</value>
		public DotNetContextType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}

		/// <summary>
		/// Gets or sets the type AST node that contains the context, if any.
		/// </summary>
		/// <value>The type AST node that contains the context, if any.</value>
		public IDomType TypeDeclarationNode {
			get {
				return typeDeclarationNode;
			}
			set {
				typeDeclarationNode = value;
			}
		}
	
		/// <summary>
		/// Converts the object to a <c>String</c>.
		/// </summary>
		/// <returns>
		/// A string whose value represents this object.
		/// </returns>
		public override string ToString() {
			StringBuilder text = new StringBuilder();
			text.Append(type);
			text.Append(": ");
			if (items != null) {
				foreach (DotNetContextItem item in items) {
					if (text[text.Length - 1] != ' ')
						text.Append('.');
					text.Append(String.Format("{0}[{1}]", item.Text, item.Type));
				}
			}
			return text.ToString();
		}
	}
}
 