using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Provides the base class for an AST node visitor.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class supports the visitor pattern.
	/// </para>
	/// <para>
	/// Each visitor implements a <c>OnVisiting</c> and an <c>OnVisited</c> method for each AST node type.
	/// By default the <c>OnVisiting</c> method returns <c>true</c>, while the <c>OnVisited</c> method does nothing.
	/// </para>
	/// <para>
	/// The <c>OnVisiting</c> method should return <c>true</c> if its child nodes should be visited.
	/// If <c>true</c> is returned, the child nodes are visiting in order.
	/// After the child nodes are visited, the <c>OnVisited</c> method is called.
	/// If <c>OnVisiting</c> returns <c>false</c>, child node visits are not made and <c>OnVisited</c> is called directly.
	/// </para>
	/// <para>
	/// Additionally, the generic <see cref="OnPreVisiting"/> method is called before any type-specific <c>OnVisiting</c> method is executed
	/// and the generic <see cref="OnPostVisited"/> method is called after any type-specific <c>OnVisited</c> method is executed.
	/// </para>
	/// </remarks>
	public abstract class AstVisitor {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// GENERIC VISIT PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Visits the <see cref="IAstNode"/> after the type-specific <c>OnVisited</c> method is executed.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnPostVisited(AstNode node) {}

		/// <summary>
		/// Visits the <see cref="IAstNode"/> before the type-specific <c>OnVisiting</c> method is executed.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node and its children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnPreVisiting(AstNode node) {
			return true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// VISITING PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AccessorDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AddressOfExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AggregateQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AnonymousMethodExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ArgumentExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ArrayEraseStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ArrayRedimClause node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ArrayRedimStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AssignmentExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(Attribute node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AttributeArgument node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(AttributeSection node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(BaseAccess node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(BinaryExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(BlockStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(BranchStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(BreakStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(CastExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(CatchClause node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(CheckedExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(CheckedStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ClassAccess node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ClassDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(CollectionRangeVariableDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(Comment node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(CompilationUnit node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ConditionalExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ConstructorDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ContinueStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(DefaultValueExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(DelegateDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(DestructorDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(DictionaryAccessExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(DistinctQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(DoStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ElseIfSection node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(EmptyStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(EnumerationDeclaration node) {
			return true;
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(EnumerationMemberDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(EventDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(EventMemberSpecifier node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ExternAliasDirective node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ExternAliasDirectiveSection node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ExitStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(FieldDeclaration node) {
			return true;
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(FixedSizeBufferDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(FixedSizeBufferDeclarator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(FixedStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ForEachStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ForStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(FromQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(GetXmlNamespaceExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(GotoStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(GroupQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(IfStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(InterfaceAccessor node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(InterfaceDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(InterfaceEventDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(InterfaceMethodDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(InterfacePropertyDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(InvocationExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(IsTypeOfExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(JoinCondition node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(JoinQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(LabeledStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(LambdaExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(LetQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(LiteralExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(LocalVariableDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(LockStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(MemberAccess node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(MemberSpecifier node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(MethodDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ModifyEventHandlerStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(NamespaceDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ObjectCollectionInitializerExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ObjectCreationExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(OperatorDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(OrderByQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(Ordering node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ParameterDeclaration node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ParenthesizedExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(PointerMemberAccess node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(PropertyDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(QualifiedIdentifier node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(QueryExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(RaiseEventStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(RegionPreProcessorDirective node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ReturnStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SelectQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SimpleName node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SizeOfExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SkipQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SkipWhileQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(StackAllocInitializer node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(StandardModuleDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(StatementExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(StructureDeclaration node) {
			return true;
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SwitchLabel node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SwitchSection node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(SwitchStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TakeQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TakeWhileQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ThisAccess node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(ThrowStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TryCastExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TryStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TypeOfExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TypeReference node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(TypeReferenceExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UnaryExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UncheckedExpression node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UncheckedStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UnsafeStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UnstructuredErrorErrorStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UnstructuredErrorOnErrorStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UnstructuredErrorResumeNextStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UsingStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UsingDirectiveSection node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(UsingDirective node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(VariableDeclarator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(WhereQueryOperator node) {
			return (this.Level >= AstVisitorLevel.All);
		}
		
		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(WhileStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(WithStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/// <summary>
		/// Visits the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		/// <returns>
		/// <c>true</c> if the node's children should be visited; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool OnVisiting(YieldStatement node) {
			return (this.Level >= AstVisitorLevel.All);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// VISITED PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AccessorDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AddressOfExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AggregateQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AnonymousMethodExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ArgumentExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ArrayEraseStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ArrayRedimClause node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ArrayRedimStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AssignmentExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(Attribute node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AttributeArgument node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(AttributeSection node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(BaseAccess node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(BinaryExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(BlockStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(BranchStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(BreakStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(CastExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(CatchClause node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(CheckedExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(CheckedStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ClassAccess node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ClassDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(CollectionRangeVariableDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(Comment node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(CompilationUnit node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ConditionalExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ConstructorDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ContinueStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(DefaultValueExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(DelegateDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(DestructorDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(DictionaryAccessExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(DistinctQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(DoStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ElseIfSection node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(EmptyStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(EnumerationDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(EnumerationMemberDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(EventDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(EventMemberSpecifier node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ExternAliasDirective node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ExternAliasDirectiveSection node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ExitStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(FieldDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(FixedSizeBufferDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(FixedSizeBufferDeclarator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(FixedStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ForEachStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ForStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(FromQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(GetXmlNamespaceExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(GotoStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(GroupQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(IfStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(InterfaceAccessor node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(InterfaceDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(InterfaceEventDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(InterfaceMethodDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(InterfacePropertyDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(InvocationExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(IsTypeOfExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(JoinCondition node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(JoinQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(LabeledStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(LambdaExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(LetQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(LiteralExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(LocalVariableDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(LockStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(MemberAccess node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(MemberSpecifier node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(MethodDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ModifyEventHandlerStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(NamespaceDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ObjectCollectionInitializerExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ObjectCreationExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(OperatorDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(OrderByQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(Ordering node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ParameterDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ParenthesizedExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(PointerMemberAccess node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(PropertyDeclaration node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(QualifiedIdentifier node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(QueryExpression node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(RaiseEventStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(RegionPreProcessorDirective node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ReturnStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SelectQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SimpleName node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SizeOfExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SkipQueryOperator node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SkipWhileQueryOperator node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(StackAllocInitializer node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(StandardModuleDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(StatementExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(StructureDeclaration node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SwitchLabel node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SwitchSection node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(SwitchStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TakeQueryOperator node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TakeWhileQueryOperator node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ThisAccess node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(ThrowStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TryCastExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TryStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TypeOfExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TypeReference node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(TypeReferenceExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UnaryExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UncheckedExpression node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UncheckedStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UnsafeStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UnstructuredErrorErrorStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UnstructuredErrorOnErrorStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UnstructuredErrorResumeNextStatement node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UsingStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UsingDirective node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(UsingDirectiveSection node) {}

		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(VariableDeclarator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(WhereQueryOperator node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(WhileStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(WithStatement node) {}
		
		/// <summary>
		/// Ends the visit of the specified type of <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="node">The node to visit.</param>
		public virtual void OnVisited(YieldStatement node) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the level through which the <see cref="AstVisitor"/> should visit children, by default.
		/// </summary>
		/// <value>An <see cref="AstVisitorLevel"/> indicating the level through which the <see cref="AstVisitor"/> should visit children.</value>
		protected virtual AstVisitorLevel Level {
			get {
				return AstVisitorLevel.All;
			}
		}

	}
}
