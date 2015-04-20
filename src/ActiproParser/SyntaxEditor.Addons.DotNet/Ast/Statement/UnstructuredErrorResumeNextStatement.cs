using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an unstructured error resume next statement.
	/// Used in Visual Basic only.
	/// </summary>
	public class UnstructuredErrorResumeNextStatement : Statement {
		
		/// <summary>
		/// Gets the context ID for a label name AST node.
		/// </summary>
		/// <value>The context ID for a label name AST node.</value>
		public const byte LabelNameContextID = Statement.StatementContextIDBase;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>UnstructuredErrorResumeNextStatement</c> class. 
		/// </summary>
		/// <param name="labelName">An <see cref="Expression"/> indicating the label name.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public UnstructuredErrorResumeNextStatement(Expression labelName, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.LabelName	= labelName;
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
		/// Gets or sets an <see cref="Expression"/> indicating the label name.
		/// </summary>
		/// <value>An <see cref="Expression"/> indicating the label name.</value>
		public Expression LabelName {
			get {
				return this.GetChildNode(UnstructuredErrorResumeNextStatement.LabelNameContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, UnstructuredErrorResumeNextStatement.LabelNameContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.UnstructuredErrorResumeNextStatement;
			}
		}


	}
}
