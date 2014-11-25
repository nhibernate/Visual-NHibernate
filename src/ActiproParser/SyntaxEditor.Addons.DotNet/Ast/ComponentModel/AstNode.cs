using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for an AST node.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Each AST node has a single parent and optional children.
	/// The nodes may be navigated by parent to child or child to parent.
	/// When a node is created, it initially has no parent node.
	/// </para>
	/// <para>
	/// AST nodes implement the visitor pattern.
	/// </para>
	/// </remarks>
	public abstract class AstNode : ActiproSoftware.SyntaxEditor.AstNodeBase {

		private byte contextID;
		
		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte AstNodeContextIDBase = AstNode.ContextIDBase;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AstNode</c> class. 
		/// </summary>
		public AstNode() {}

		/// <summary>
		/// Initializes a new instance of the <c>AstNode</c> class. 
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public AstNode(TextRange textRange) : base(textRange) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends generic type arguments to the specified <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <param name="text">The <see cref="StringBuilder"/> to modify.</param>
		/// <param name="genericTypeArguments">The <see cref="IAstNodeList"/> of generic type arguments.</param>
		internal static void AppendGenericTypeArgumentsToDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel, StringBuilder text, IAstNodeList genericTypeArguments) {
			if (language == DotNetLanguage.VB) {
				text.Append("(Of ");			
				if (genericTypeArguments != null) {
					for (int index = 0; index < genericTypeArguments.Count; index++) {
						if (index > 0)
							text.Append(", ");

						IDomTypeReference genericTypeArgument = (IDomTypeReference)genericTypeArguments[index];
						text.Append(AstNode.GetTypeReferenceName(language, detailLevel, genericTypeArgument));
					}
				}		
				text.Append(")");
			}
			else {
				text.Append("<");			
				if (genericTypeArguments != null) {
					for (int index = 0; index < genericTypeArguments.Count; index++) {
						if (index > 0)
							text.Append(", ");

						IDomTypeReference genericTypeArgument = (IDomTypeReference)genericTypeArguments[index];
						text.Append(AstNode.GetTypeReferenceName(language, detailLevel, genericTypeArgument));
					}
				}		
				text.Append(">");
			}
		}

		/// <summary>
		/// Appends parameters to the specified <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <param name="text">The <see cref="StringBuilder"/> to modify.</param>
		/// <param name="parameters">The <see cref="IAstNodeList"/> of parameters.</param>
		internal static void AppendParametersToDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel, StringBuilder text, IAstNodeList parameters) {
			if (parameters != null) {
				for (int index = 0; index < parameters.Count; index++) {
					if (index > 0)
						text.Append(", ");

					ParameterDeclaration parameter = (ParameterDeclaration)parameters[index];
					if (parameter.IsOutput)
						text.Append("out ");
					if (parameter.IsByReference)
						text.Append("ref ");
					if (parameter.IsParameterArray)
						text.Append("params ");
					text.Append(AstNode.GetTypeReferenceName(language, detailLevel, parameter.ParameterType));
				}
			}
		}
		
		/// <summary>
		/// Returns the full name of a type declaration node.
		/// </summary>
		/// <param name="node">The <see cref="TypeMemberDeclaration"/> to examine.</param>
		/// <returns>The full name of a type declaration node.</returns>
		internal static string GetTypeFullName(TypeMemberDeclaration node) {
			StringBuilder fullName = new StringBuilder();
			if (node.ParentNode is NamespaceDeclaration) {
				fullName.Insert(0, ".");
				fullName.Insert(0, ((NamespaceDeclaration)node.ParentNode).FullName);
			}
			else if (node.ParentNode is IDomType) {
				fullName.Insert(0, "+");
				fullName.Insert(0, ((IDomType)node.ParentNode).FullName);
			}

			if (node.Name != null)
				fullName.Append(node.Name.Text);
			else
				fullName.Append("?");

			if (node is IDomType) {
				ICollection genericTypeArguments = ((IDomType)node).GenericTypeArguments;
				if ((genericTypeArguments != null) && (genericTypeArguments.Count > 0)) {
					// Append generic type argument count
					fullName.Append("`");
					fullName.Append(genericTypeArguments.Count.ToString());
				}
			}

			return fullName.ToString();
		}

		/// <summary>
		/// Returns the full name of the specified type reference.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <returns>The full name of the specified type reference.</returns>
		internal static string GetTypeReferenceName(DotNetLanguage language, DisplayTextDetailLevel detailLevel, IDomTypeReference typeReference) {
			if (typeReference == null)
				return "?";

			string originalFullName = DotNetProjectResolver.GetTypeFullNameForDisplay(typeReference.FullName);

			// Trim off the pointer/array suffix
			int suffixIndex = originalFullName.IndexOfAny(new char[] { '*', '[' });
			string suffix = (suffixIndex != -1 ? originalFullName.Substring(suffixIndex) : String.Empty);
			if (suffixIndex != -1) {
				originalFullName = originalFullName.Substring(0, suffixIndex);

				if (language == DotNetLanguage.VB)
					suffix = suffix.Replace('[', '(').Replace(']', ')');
			}

			// Convert to a shortcut if possible
			string shortcutName = DotNetProjectResolver.GetTypeShortcutName(language, originalFullName) + suffix;

			if ((detailLevel == DisplayTextDetailLevel.SimpleFullyQualified) || (originalFullName != shortcutName))
				return shortcutName + DotNetProjectResolver.GetGenericSpecification(typeReference);
			else
				return DotNetProjectResolver.GetTypeShortcutName(language, DotNetProjectResolver.GetTypeFullNameForDisplay(typeReference.Name)) + 
					DotNetProjectResolver.GetGenericSpecification(typeReference);
		}

		/// <summary>
		/// Returns whether an access modifier specified.
		/// </summary>
		/// <param name="modifiers">The <see cref="Modifiers"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if an access modifier is specified; otherwise, <c>false</c>.
		/// </returns>
		internal static bool IsAccessSpecified(Modifiers modifiers) {
			return !((modifiers & (Modifiers.Public | Modifiers.Assembly | Modifiers.Family | Modifiers.FamilyOrAssembly | Modifiers.Private)) == Modifiers.None);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Accepts the specified visitor for visiting this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <remarks>This method is part of the visitor design pattern implementation.</remarks>
		public void Accept(AstVisitor visitor) {
			if (visitor.OnPreVisiting(this))
				this.AcceptCore(visitor);
			visitor.OnPostVisited(this);
		}

		/// <summary>
		/// Accepts the specified visitor for visiting a child node of this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <param name="childNode">The child node to visit.</param>
		/// <remarks>This method is part of the visitor design pattern implementation.</remarks>
		public void AcceptChild(AstVisitor visitor, AstNode childNode) {
			if (childNode != null)
				childNode.Accept(visitor);
		}

		/// <summary>
		/// Accepts the specified visitor for visiting the child nodes of this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <param name="nodeList">The list of child nodes to visit.</param>
		/// <remarks>This method is part of the visitor design pattern implementation.</remarks>
		public void AcceptChildren(AstVisitor visitor, IAstNodeList nodeList) {
			if (nodeList != null) {
				for (int index = 0; index < nodeList.Count; index++) {
					if (nodeList[index] is AstNode)
						((AstNode)nodeList[index]).Accept(visitor);
				}
			}
		}

		/// <summary>
		/// Accepts the specified visitor for visiting this node.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <remarks>
		/// This method must be implemented by each AST node.  It will look like the following:
		/// <code>
		/// if (visitor.OnVisiting(this)) {
		///		// Visit children
		///		if (this.ChildNodeCount > 0)
		///			this.AcceptChildren(visitor, this.ChildNodes);
		///	}
		///	visitor.OnVisited(this);
		/// </code>
		/// </remarks>
		protected abstract void AcceptCore(AstVisitor visitor);

		/// <summary>
		/// Gets or sets a context value identifying the context of the AST node within its parent node.
		/// </summary>
		/// <value>A context value identifying the context of the AST node within its parent node.</value>
		/// <remarks>
		/// The context ID value is typically defined on the parent AST node as a constant.
		/// </remarks>
		public override int ContextID { 
			get {
				return contextID;
			}
			set {
				contextID = (byte)value;
			}
		}
		
		/// <summary>
		/// Gets text representing the node that can be used for display, such as in a document outline.
		/// </summary>
		/// <value>Text representing the node that can be used for display, such as in a document outline.</value>
		public override string DisplayText {
			get {
				CompilationUnit compilationUnit = this.FindAncestor(typeof(CompilationUnit)) as CompilationUnit;
				if (compilationUnit != null)
					return this.GetDisplayText(compilationUnit.SourceLanguage, DisplayTextDetailLevel.Simple);
				else
					return this.GetDisplayText(DotNetLanguage.CSharp, DisplayTextDetailLevel.Simple);
			}
		}

		/// <summary>
		/// Returns display text that represents the AST node using the specified options.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <returns>The display text that represents the AST node using the specified options.</returns>
		/// <remarks>
		/// This method is useful for getting text to display for the node for use in a type/member drop-down list or class browser.
		/// </remarks>
		public virtual string GetDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel) {
			throw new NotImplementedException("This method has not been implemented by this type of AST node.");
		}

		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public virtual DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.Other;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public abstract DotNetNodeType NodeType { get; }

		/// <summary>
		/// Gets the parent <see cref="TypeDeclaration"/> that defines this type or member.
		/// </summary>
		/// <value>The parent <see cref="TypeDeclaration"/> that defines this type or member.</value>
		public TypeDeclaration ParentTypeDeclaration {
			get {
				IAstNode node = this.ParentNode;
				while (node != null) {
					if (node is TypeDeclaration)
						return (TypeDeclaration)node;
					node = node.ParentNode;
				}
				return null;
			}
		}

	}
}
