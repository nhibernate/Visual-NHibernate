using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for a statement that has a single child statement.
	/// </summary>
	public abstract class ChildStatementStatement : Statement {

		/// <summary>
		/// Gets the context ID for a statement AST node.
		/// </summary>
		/// <value>The context ID for a statement AST node.</value>
		public const byte StatementContextID = Statement.StatementContextIDBase;
		
		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte ChildStatementStatementContextIDBase = Statement.StatementContextIDBase + 1;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ChildStatementStatement</c> class. 
		/// </summary>
		public ChildStatementStatement() {}
		
		/// <summary>
		/// Initializes a new instance of the <c>ChildStatementStatement</c> class. 
		/// </summary>
		/// <param name="statement">The child <see cref="Statement"/>.</param>
		public ChildStatementStatement(Statement statement) {
			// Initialize parameters
			this.Statement = statement;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>ChildStatementStatement</c> class. 
		/// </summary>
		/// <param name="statement">The child <see cref="Statement"/>.</param>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public ChildStatementStatement(Statement statement, TextRange textRange) : base(textRange) {
			// Initialize parameters
			this.Statement = statement;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="Statement"/> affected by the unary operator.
		/// </summary>
		/// <value>The <see cref="Statement"/> affected by the unary operator.</value>
		public Statement Statement {
			get {
				return this.GetChildNode(ChildStatementStatement.StatementContextID) as Statement;
			}
			set {
				this.ChildNodes.Replace(value, ChildStatementStatement.StatementContextID);
			}
		}

	}
}
