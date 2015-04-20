using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons;

namespace ActiproSoftware.SyntaxEditor.Addons {

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
	public abstract partial class AstNode : ActiproSoftware.SyntaxEditor.AstNodeBase {

		private byte contextID;
		
		/// <summary>
		/// Gets the minimum context ID that should be used in your code for AST nodes inheriting this class.
		/// </summary>
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
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets a context value identifying the context of the AST node within its parent node.
		/// </summary>
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

	}

}
