using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents a raise event statement.
	/// Used in Visual Basic only.
	/// </summary>
	public class RaiseEventStatement : Statement {
		
		/// <summary>
		/// Gets the context ID for an event name AST node.
		/// </summary>
		/// <value>The context ID for an event name AST node.</value>
		public const byte EventNameContextID = Statement.StatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for an argument AST node.
		/// </summary>
		/// <value>The context ID for an argument AST node.</value>
		public const byte ArgumentContextID = Statement.StatementContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>RaiseEventStatement</c> class. 
		/// </summary>
		/// <param name="eventName">A <see cref="QualifiedIdentifier"/> indicating the event name.</param>
		/// <param name="arguments">An <see cref="IAstNodeList"/> containing the arguments.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public RaiseEventStatement(QualifiedIdentifier eventName, IAstNodeList arguments, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.EventName = eventName;
			if ((arguments != null) && (arguments.Count > 0))
				this.Arguments.AddRange(arguments.ToArray());
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
		/// Gets the collection of arguments.
		/// </summary>
		/// <value>The collection of arguments.</value>
		public IAstNodeList Arguments {
			get {
				return new AstNodeListWrapper(this, RaiseEventStatement.ArgumentContextID);
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="QualifiedIdentifier"/> indicating the event name.
		/// </summary>
		/// <value>A <see cref="QualifiedIdentifier"/> indicating the event name.</value>
		public QualifiedIdentifier EventName {
			get {
				return this.GetChildNode(RaiseEventStatement.EventNameContextID) as QualifiedIdentifier;
			}
			set {
				this.ChildNodes.Replace(value, RaiseEventStatement.EventNameContextID);
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.RaiseEventStatement;
			}
		}


	}
}
