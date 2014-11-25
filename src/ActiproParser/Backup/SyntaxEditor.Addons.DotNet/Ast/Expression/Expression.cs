using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for an expression.
	/// </summary>
	public abstract class Expression : AstNode {

		/// <summary>
		/// Gets the context ID for a generic type argument AST node.
		/// </summary>
		/// <value>The context ID for a generic type argument AST node.</value>
		public const byte GenericTypeArgumentContextID = AstNode.AstNodeContextIDBase;
		
		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
		/// <value>The minimum context ID that should be used in your code for AST nodes inheriting this class.</value>
		/// <remarks>
		/// Base all your context ID constants off of this value.
		/// </remarks>
		protected const byte ExpressionContextIDBase = AstNode.AstNodeContextIDBase + 1;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>Expression</c> class. 
		/// </summary>
		public Expression() {}

		/// <summary>
		/// Initializes a new instance of the <c>Expression</c> class. 
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the AST node.</param>
		public Expression(TextRange textRange) : base(textRange) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of generic type arguments.
		/// </summary>
		/// <value>The collection of generic type arguments.</value>
		public IAstNodeList GenericTypeArguments {
			get {
				return new AstNodeListWrapper(this, Expression.GenericTypeArgumentContextID);
			}
		}
		
		/// <summary>
		/// Gets an <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.
		/// </summary>
		/// <value>An <see cref="DotNetNodeCategory"/> that indicates the generalized type of node.</value>
		public override DotNetNodeCategory NodeCategory {
			get {
				return DotNetNodeCategory.Expression;
			}
		}
		
	}
}
