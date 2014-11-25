using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for a statement.
	/// </summary>
	public abstract class Statement : AstNode {
		
		/// <summary>
		/// Gets the context ID for a comment AST node.
		/// </summary>
		/// <value>The context ID for a comment AST node.</value>
		public const byte CommentContextID = AstNode.AstNodeContextIDBase;

		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte StatementContextIDBase = AstNode.AstNodeContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>Statement</c> class. 
		/// </summary>
		public Statement() {}

		/// <summary>
		/// Initializes a new instance of the <c>Statement</c> class. 
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public Statement(TextRange textRange) : base(textRange) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the collection of comments that appear before the node.
		/// </summary>
		/// <value>The collection of comments that appear before the node.</value>
		public IAstNodeList Comments {
			get {
				return new AstNodeListWrapper(this, Statement.CommentContextID);
			}
		}
		
		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.Statement;
			}
		}
		
	}
}
