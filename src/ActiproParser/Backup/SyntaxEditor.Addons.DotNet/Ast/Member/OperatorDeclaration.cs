using System;
using System.Collections;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an operator declaration.
	/// </summary>
	public class OperatorDeclaration : TypeMemberDeclaration, IBlockAstNode, ICollapsibleNode, IDomMember {

		private int					blockEndOffset		= -1;
		private int					blockStartOffset	= -1;
		private OperatorType		operatorType		= OperatorType.None;
		
		/// <summary>
		/// Gets the context ID for a return type AST node.
		/// </summary>
		/// <value>The context ID for a return type AST node.</value>
		public const byte ReturnTypeContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase;
		
		/// <summary>
		/// Gets the context ID for a parameter AST node.
		/// </summary>
		/// <value>The context ID for a parameter AST node.</value>
		public const byte ParameterContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 1;
		
		/// <summary>
		/// Gets the context ID for a statement AST node.
		/// </summary>
		/// <value>The context ID for a statement AST node.</value>
		public const byte StatementContextID = TypeMemberDeclaration.TypeMemberDeclarationContextIDBase + 2;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>OperatorDeclaration</c> class. 
		/// </summary>
		/// <param name="modifiers">The modifiers.</param>
		/// <param name="operatorType">The operator type.</param>
		public OperatorDeclaration(Modifiers modifiers, OperatorType operatorType) : base(modifiers, null) {
			// Initialize parameters
			this.operatorType = operatorType;
		}
		
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
		/// Gets whether the member is static.
		/// </summary>
		/// <value>
		/// <c>true</c> if the member is static; otherwise, <c>false</c>.
		/// </value>
		bool IDomMember.IsStatic { 
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Gets a <see cref="DomMemberType"/> that indicates the type of member.
		/// </summary>
		/// <value>A <see cref="DomMemberType"/> that indicates the type of member.</value>
		DomMemberType IDomMember.MemberType { 
			get {
				return DomMemberType.Method;
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
		/// Returns display text that represents the AST node using the specified options.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for formatting the text.</param>
		/// <param name="detailLevel">A <see cref="DisplayTextDetailLevel"/> indicating the desired level of detail.</param>
		/// <returns>The display text that represents the AST node using the specified options.</returns>
		/// <remarks>
		/// This method is useful for getting text to display for the node for use in a type/member drop-down list or class browser.
		/// </remarks>
		public override string GetDisplayText(DotNetLanguage language, DisplayTextDetailLevel detailLevel) {
			StringBuilder text = new StringBuilder();
			if ((operatorType == OperatorType.Implicit) || (operatorType == OperatorType.Explicit)) {
				text.Append(this.GetOperatorTypeName(DotNetLanguage.CSharp));
				text.Append(" ");
			}
			text.Append("operator ");
			if ((operatorType == OperatorType.Implicit) || (operatorType == OperatorType.Explicit))
				text.Append(AstNode.GetTypeReferenceName(language, detailLevel, this.ReturnType));
			else
				text.Append(this.GetOperatorTypeName(DotNetLanguage.CSharp));
			text.Append("(");
			AstNode.AppendParametersToDisplayText(language, detailLevel, text, this.Parameters);
			text.Append(")");
			return text.ToString();
		}

		/// <summary>
		/// Gets the full type name, or a language shortcut keyword specifying the type if possible.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> for which to return a shortcut keyword if possible.</param>
		/// <returns>The full type name, or a language shortcut keyword specifying the type if possible.</returns>
		public string GetOperatorTypeName(DotNetLanguage language) {
			switch (language) {
				case DotNetLanguage.CSharp:
					switch (operatorType) {
						case OperatorType.Addition:
							return "+";
						case OperatorType.Subtraction:
							return "-";
						case OperatorType.Multiply:
							return "*";
						case OperatorType.Division:
							return "/";
						case OperatorType.Modulus:
							return "%";
						case OperatorType.BitwiseAnd:
							return "&";
						case OperatorType.BitwiseOr:
							return "|";
						case OperatorType.ExclusiveOr: 
							return "^";
						case OperatorType.Negation:
							return "!";
						case OperatorType.OnesComplement:  
							return "~";
						case OperatorType.LessThan:
							return "<";
						case OperatorType.GreaterThan:
							return ">";
						case OperatorType.PreIncrement:
							return "++";
						case OperatorType.PreDecrement:
							return "--";
						case OperatorType.PostIncrement:
							return "++";
						case OperatorType.PostDecrement:
							return "--";
						case OperatorType.LeftShift:
							return "<<";
						case OperatorType.RightShift:
							return ">>";
						case OperatorType.Equality:
							return "==";
						case OperatorType.Inequality:
							return "!=";
						case OperatorType.LessThanOrEqual:
							return "<=";
						case OperatorType.GreaterThanOrEqual:
							return ">=";
						case OperatorType.True:
							return "true";
						case OperatorType.False:
							return "false";
						case OperatorType.Implicit:
							return "implicit";
						case OperatorType.Explicit:
							return "explicit";
						default:
							return String.Empty;
					}
			}

			return "(" + operatorType + ")";
		}

		/// <summary>
		/// Gets the image index that is applicable for displaying this node in a user interface control.
		/// </summary>
		/// <value>The image index that is applicable for displaying this node in a user interface control.</value>
		public override int ImageIndex {
			get {
				return (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Operator;
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
				return DotNetNodeType.OperatorDeclaration;
			}
		}

		/// <summary>
		/// Gets or sets the operator type.
		/// </summary>
		/// <value>The operator type.</value>
		public OperatorType OperatorType {
			get {
				return operatorType;
			}
			set {
				operatorType = value;
			}
		}

		/// <summary>
		/// Gets the collection of parameters.
		/// </summary>
		/// <value>The collection of parameters.</value>
		public IAstNodeList Parameters {
			get {
				return new AstNodeListWrapper(this, OperatorDeclaration.ParameterContextID);
			}
		}

		/// <summary>
		/// Gets or sets the return type.
		/// </summary>
		/// <value>The return type.</value>
		public TypeReference ReturnType {
			get {
				return this.GetChildNode(OperatorDeclaration.ReturnTypeContextID) as TypeReference;
			}
			set {
				this.ChildNodes.Replace(value, OperatorDeclaration.ReturnTypeContextID);
			}
		}
		
		/// <summary>
		/// Gets the collection of statements.
		/// </summary>
		/// <value>The collection of statements.</value>
		public IAstNodeList Statements {
			get {
				return new AstNodeListWrapper(this, OperatorDeclaration.StatementContextID);
			}
		}

	}
}
