using System;
using System.Collections;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a property or indexer declaration.
	/// </summary>
	public class PropertyDeclaration : TypeMemberDeclaration, IBlockAstNode, ICollapsibleNode, IDomMember {

		private int					blockEndOffset		= -1;
		private int					blockStartOffset	= -1;
		
		/// <summary>
		/// Gets the context ID for a return type AST node.
		/// </summary>
		/// <value>The context ID for a return type AST node.</value>
		public const byte ReturnTypeContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase;
		
		/// <summary>
		/// Gets the context ID for an implemented member AST node.
		/// </summary>
		/// <value>The context ID for an implemented member type AST node.</value>
		public const byte ImplementedMemberContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a get accessor AST node.
		/// </summary>
		/// <value>The context ID for a get accessor AST node.</value>
		public const byte GetAccessorContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 2;

		/// <summary>
		/// Gets the context ID for a set accessor AST node.
		/// </summary>
		/// <value>The context ID for a set accessor AST node.</value>
		public const byte SetAccessorContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 3;

		/// <summary>
		/// Gets the context ID for a parameter AST node.
		/// </summary>
		/// <value>The context ID for a parameter AST node.</value>
		public const byte ParameterContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 4;

		/// <summary>
		/// Gets the context ID for an initializer AST node.
		/// </summary>
		/// <value>The context ID for an initializer AST node.</value>
		public const byte InitializerContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 5;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>PropertyDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="name">The name.</param>
		public PropertyDeclaration(Modifiers modifiers, QualifiedIdentifier name) : base(modifiers, name) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the offset at which the outlining node ends.
		/// </summary>
		/// <value>The offset at which the outlining node ends.</value>
		int ICollapsibleNode.EndOffset { 
			get {
				return blockEndOffset;
			}
		}

		/// <summary>
		/// Gets whether the node is collapsible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the node is collapsible; otherwise, <c>false</c>.
		/// </value>
		bool ICollapsibleNode.IsCollapsible { 
			get {
				return (blockStartOffset != -1) && ((blockStartOffset < blockEndOffset) || (blockEndOffset == -1));
			}
		}

		/// <summary>
		/// Gets the offset at which the outlining node starts.
		/// </summary>
		/// <value>The offset at which the outlining node starts.</value>
		int ICollapsibleNode.StartOffset { 
			get {
				return blockStartOffset;
			}
		}
		
		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that declares the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that declares the member.</value>
		IDomTypeReference IDomMember.DeclaringType {
			get {
				return this.ParentTypeDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the type arguments if this is a generic method definition.
		/// </summary>
		/// <value>The type arguments if this is a generic method definition.</value>
		ICollection IDomMember.GenericTypeArguments { 
			get {
				return null;
			}
		}
		
		/// <summary>
		/// Gets whether the member is a generic method.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsGenericMethod { 
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets whether the member is a generic method definition, from which other generic methods can be constructed.
		/// </summary>
		/// <value>
		/// <c>true</c> if the method is a generic method definition, from which other generic methods can be constructed; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsGenericMethodDefinition { 
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets a <see cref="DomMemberType"/> that indicates the type of member.
		/// </summary>
		/// <value>A <see cref="DomMemberType"/> that indicates the type of member.</value>
		DomMemberType IDomMember.MemberType { 
			get {
				return DomMemberType.Property;
			}
		}

		/// <summary>
		/// Gets the name of the member.
		/// </summary>
		/// <value>The name of the member.</value>
		string IDomMember.Name { 
			get {
				if (this.Name != null)
					return this.Name.Text;
				else
					return null;
			}
		}
		
		/// <summary>
		/// Gets the array of <see cref="IDomParameter"/> parameters for the member.
		/// </summary>
		/// <value>The array of <see cref="IDomParameter"/> parameters for the member.</value>
		IDomParameter[] IDomMember.Parameters {
			get {
				IAstNodeList parameters = this.Parameters;
				if (parameters.Count > 0) {
					IDomParameter[] parameterArray = new IDomParameter[parameters.Count];
					for (int index = 0; index < parameters.Count; index++)
						parameterArray[index] = (ParameterDeclaration)parameters[index];
					return parameterArray;
				}
				return null;
			}
		}
		
		/// <summary>
		/// Gets a <see cref="IDomTypeReference"/> to the type that is returned by the member.
		/// </summary>
		/// <value>A <see cref="IDomTypeReference"/> to the type that is returned by the member.</value>
		IDomTypeReference IDomMember.ReturnType { 
			get {
				return this.ReturnType;
			}
		}

		/// <summary>
		/// Gets whether the outlining indicator should be visible for the node.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining indicator should be visible for the node; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IndicatorVisible { 
			get {
				return true;
			}
		}

		/// <summary>
		/// Gets whether the outlining node is for a language transition.
		/// </summary>
		/// <value>
		/// <c>true</c> if the outlining node is for a language transition; otherwise, <c>false</c>.
		/// </value>
		bool IOutliningNodeParseData.IsLanguageTransition { 
			get {
				return false;
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
		/// Gets or sets the end character offset of the end block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The end character offset of the end block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the end character offset.
		/// </remarks>
		public int BlockEndOffset {
			get {
				return blockEndOffset;
			}
			set {
				blockEndOffset = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the start character offset of the start block delimiter in the original source code that generated the AST node.
		/// </summary>
		/// <value>The start character offset of the start block delimiter in the original source code that generated the AST node.</value>
		/// <remarks>
		/// This value may be <c>-1</c> if there is no source code information for the start character offset.
		/// </remarks>
		public int BlockStartOffset {
			get {
				return blockStartOffset;
			}
			set {
				blockStartOffset = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the get <see cref="AccessorDeclaration"/>.
		/// </summary>
		/// <value>The get <see cref="AccessorDeclaration"/>.</value>
		public AccessorDeclaration GetAccessor {
			get {
				return this.GetChildNode(PropertyDeclaration.GetAccessorContextID) as AccessorDeclaration;
			}
			set {
				this.ChildNodes.Replace(value, PropertyDeclaration.GetAccessorContextID);
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
		public override string GetDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel) {
			if (this.IsIndexer) {
				StringBuilder text = new StringBuilder();
				text.Append("this[");
				AstNode.AppendParametersToDisplayText(language, detailLevel, text, this.Parameters);
				text.Append("]");
				return text.ToString();
			}
			else if (this.Name != null)
				return this.Name.Text;
			else
				return "?";
		}

		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return AssemblyDomMember.GetReflectionImageIndex(DomMemberType.Property, this.AccessModifiers, this.IsExtension);
			}
		}
		
		/// <summary>
		/// Gets the collection of implemented members.
		/// </summary>
		/// <value>The collection of implemented members.</value>
		public IAstNodeList ImplementedMembers {
			get {
				return new AstNodeListWrapper(this, PropertyDeclaration.ImplementedMemberContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the initializer.
		/// </summary>
		/// <value>The initializer.</value>
		public Expression Initializer {
			get {
				return this.GetChildNode(PropertyDeclaration.InitializerContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, PropertyDeclaration.InitializerContextID);
			}
		}
		
		/// <summary>
		/// Gets whether this property is an indexer.
		/// </summary>
		/// <value>
		/// <c>true</c> if this property is an indexer; otherwise, <c>false</c>.
		/// </value>
		public bool IsIndexer {
			get {
				return (this.Name == null) || (this.Parameters.Count > 0);
			}
		}
		
		/// <summary>
		/// Gets whether the member is static.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is static; otherwise, <c>false</c>.
		/// </value>
		public bool IsStatic { 
			get {
				return ((this.Modifiers & Modifiers.Static) == Modifiers.Static);
			}
		}
		
		/// <summary>
		/// Gets the string-based key that uniquely identifies the <see cref="AstNode"/>.
		/// </summary>
		/// <value>The string-based key that uniquely identifies the <see cref="AstNode"/>.</value>
		public override string Key {
			get {
				return DomDocumentationProvider.GetMemberDocumentationKey(this);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.PropertyDeclaration;
			}
		}
		
		/// <summary>
		/// Gets the collection of parameters.
		/// </summary>
		/// <value>The collection of parameters.</value>
		public IAstNodeList Parameters {
			get {
				return new AstNodeListWrapper(this, PropertyDeclaration.ParameterContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the return type.
		/// </summary>
		/// <value>The return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(PropertyDeclaration.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, PropertyDeclaration.ReturnTypeContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets the set <see cref="AccessorDeclaration"/>.
		/// </summary>
		/// <value>The set <see cref="AccessorDeclaration"/>.</value>
		public AccessorDeclaration SetAccessor {
			get {
				return this.GetChildNode(PropertyDeclaration.SetAccessorContextID) as AccessorDeclaration;
			}
			set {
				this.ChildNodes.Replace(value, PropertyDeclaration.SetAccessorContextID);
			}
		}
		
	}
}
