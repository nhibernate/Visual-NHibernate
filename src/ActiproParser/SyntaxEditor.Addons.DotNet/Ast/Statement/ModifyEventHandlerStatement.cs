using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Represents an add/remove event handler statement.
	/// Used in Visual Basic only.
	/// </summary>
	public class ModifyEventHandlerStatement : Statement {

		private ModifyEventHandlerStatementType modificationType;
		
		/// <summary>
		/// Gets the context ID for the event AST node.
		/// </summary>
		/// <value>The context ID for the event AST node.</value>
		public const byte EventContextID = Statement.StatementContextIDBase;
		
		/// <summary>
		/// Gets the context ID for the event handler AST node.
		/// </summary>
		/// <value>The context ID for the event handler AST node.</value>
		public const byte EventHandlerContextID = Statement.StatementContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ModifyEventHandlerStatement</c> class. 
		/// </summary>
		/// <param name="modificationType">A <see cref="ModifyEventHandlerStatementType"/> indicating the modification type.</param>
		/// <param name="event">An <see cref="Expression"/> that specifies the event.</param>
		/// <param name="eventHandler">An <see cref="Expression"/> that specifies the event handler.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ModifyEventHandlerStatement(ModifyEventHandlerStatementType modificationType, Expression @event, Expression eventHandler, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.modificationType	= modificationType;
			this.Event				= @event;
			this.EventHandler		= eventHandler;
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
		/// Gets or sets a <see cref="Expression"/> that specifies the event.
		/// </summary>
		/// <value>A <see cref="Expression"/> that specifies the event.</value>
		public Expression Event {
			get {
				return this.GetChildNode(ModifyEventHandlerStatement.EventContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ModifyEventHandlerStatement.EventContextID);
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="Expression"/> that specifies the event handler.
		/// </summary>
		/// <value>A <see cref="Expression"/> that specifies the event handler.</value>
		public Expression EventHandler {
			get {
				return this.GetChildNode(ModifyEventHandlerStatement.EventHandlerContextID) as Expression;
			}
			set {
				this.ChildNodes.Replace(value, ModifyEventHandlerStatement.EventHandlerContextID);
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="ModifyEventHandlerStatementType"/> indicating the modification type.
		/// </summary>
		/// <value>A <see cref="ModifyEventHandlerStatementType"/> indicating the modification type.</value>
		public ModifyEventHandlerStatementType ModificationType {
			get {
				return modificationType;
			}
			set {
				modificationType = value;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="DotNetNodeType"/> that identifies the type of node.
		/// </summary>
		/// <value>The <see cref="DotNetNodeType"/> that identifies the type of node.</value>
		public override DotNetNodeType NodeType { 
			get {
				return DotNetNodeType.ModifyEventHandlerStatement;
			}
		}


	}
}
