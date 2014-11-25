using System;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of an <see cref="AstNode"/>.
	/// </summary>
	public enum DotNetNodeType {

		/// <summary>
		/// An address of expression node.
		/// </summary>
		AddressOfExpression,

		/// <summary>
		/// An anonymous method expression node.
		/// </summary>
		AnonymousMethodExpression,

		/// <summary>
		/// An argument expression node.
		/// </summary>
		ArgumentExpression,

		/// <summary>
		/// An assignment expression node.
		/// </summary>
		AssignmentExpression,

		/// <summary>
		/// A base access node.
		/// </summary>
		BaseAccess,

		/// <summary>
		/// A binary expression node.
		/// </summary>
		BinaryExpression,

		/// <summary>
		/// A cast expression node.
		/// </summary>
		CastExpression,

		/// <summary>
		/// A checked expression node.
		/// </summary>
		CheckedExpression,
		
		/// <summary>
		/// A class access node.
		/// </summary>
		ClassAccess,

		/// <summary>
		/// A conditional expression node.
		/// </summary>
		ConditionalExpression,

		/// <summary>
		/// A default value expression node.
		/// </summary>
		DefaultValueExpression,

		/// <summary>
		/// A dictionary access expression node.
		/// </summary>
		DictionaryAccessExpression,

		/// <summary>
		/// A get XML namespace expression node.
		/// </summary>
		GetXmlNamespaceExpression,

		/// <summary>
		/// An invocation expression node.
		/// </summary>
		InvocationExpression,

		/// <summary>
		/// An is type of expression node.
		/// </summary>
		IsTypeOfExpression,

		/// <summary>
		/// A lambda expression.
		/// </summary>
		LambdaExpression,

		/// <summary>
		/// A literal expression node.
		/// </summary>
		LiteralExpression,

		/// <summary>
		/// A member access node.
		/// </summary>
		MemberAccess,

		/// <summary>
		/// An object or collection initializer expression node.
		/// </summary>
		ObjectCollectionInitializerExpression,

		/// <summary>
		/// An object creation expression node.
		/// </summary>
		ObjectCreationExpression,

		/// <summary>
		/// A parenthesized expression node.
		/// </summary>
		ParenthesizedExpression,

		/// <summary>
		/// A pointer member access node.
		/// </summary>
		PointerMemberAccess,

		/// <summary>
		/// A simple name node.
		/// </summary>
		SimpleName,

		/// <summary>
		/// A size of expression node.
		/// </summary>
		SizeOfExpression,

		/// <summary>
		/// A stack alloc initializer node.
		/// </summary>
		StackAllocInitializer,

		/// <summary>
		/// A this access node.
		/// </summary>
		ThisAccess,

		/// <summary>
		/// A try cast expression node.
		/// </summary>
		TryCastExpression,

		/// <summary>
		/// A type of expression node.
		/// </summary>
		TypeOfExpression,

		/// <summary>
		/// A type reference expression node.
		/// </summary>
		TypeReferenceExpression,

		/// <summary>
		/// An unary expression node.
		/// </summary>
		UnaryExpression,

		/// <summary>
		/// An unchecked expression node.
		/// </summary>
		UncheckedExpression,

		/// <summary>
		/// An array redim clause node.
		/// </summary>
		ArrayRedimClause,

		/// <summary>
		/// An attribute node.
		/// </summary>
		Attribute,

		/// <summary>
		/// An attribute argument node.
		/// </summary>
		AttributeArgument,

		/// <summary>
		/// An attribute section node.
		/// </summary>
		AttributeSection,
		
		/// <summary>
		/// A comment node.
		/// </summary>
		Comment,

		/// <summary>
		/// A compilation unit node.
		/// </summary>
		CompilationUnit,

		/// <summary>
		/// A documentation comment node.
		/// </summary>
		DocumentationComment,
		
		/// <summary>
		/// An event member specifier node.
		/// </summary>
		EventMemberSpecifier,

		/// <summary>
		/// An extern alias directive node.
		/// </summary>
		ExternAliasDirective,

		/// <summary>
		/// An extern alias directive section node.
		/// </summary>
		ExternAliasDirectiveSection,

		/// <summary>
		/// A fixed size buffer declarator node.
		/// </summary>
		FixedSizeBufferDeclarator,

		/// <summary>
		/// An interface accessor node.
		/// </summary>
		InterfaceAccessor,

		/// <summary>
		/// A member specifier node.
		/// </summary>
		MemberSpecifier,

		/// <summary>
		/// A namespace declaration node.
		/// </summary>
		NamespaceDeclaration,

		/// <summary>
		/// A parameter declaration node.
		/// </summary>
		ParameterDeclaration,

		/// <summary>
		/// A qualified identifier node.
		/// </summary>
		QualifiedIdentifier,

		/// <summary>
		/// A region pre-processor directive node.
		/// </summary>
		RegionPreProcessorDirective,
		
		/// <summary>
		/// A single-line comment node.
		/// </summary>
		SingleLineComment,

		/// <summary>
		/// A type reference node.
		/// </summary>
		TypeReference,

		/// <summary>
		/// A using directive node.
		/// </summary>
		UsingDirective,

		/// <summary>
		/// A using directive section node.
		/// </summary>
		UsingDirectiveSection,

		/// <summary>
		/// A variable declarator node.
		/// </summary>
		VariableDeclarator,

		/// <summary>
		/// An accessor declaration node.
		/// </summary>
		AccessorDeclaration,

		/// <summary>
		/// A constructor declaration node.
		/// </summary>
		ConstructorDeclaration,

		/// <summary>
		/// A destructor declaration node.
		/// </summary>
		DestructorDeclaration,

		/// <summary>
		/// An enumeration member declaration node.
		/// </summary>
		EnumerationMemberDeclaration,

		/// <summary>
		/// An event declaration node.
		/// </summary>
		EventDeclaration,

		/// <summary>
		/// A field declaration node.
		/// </summary>
		FieldDeclaration,

		/// <summary>
		/// A fixed size buffer declaration node.
		/// </summary>
		FixedSizeBufferDeclaration,

		/// <summary>
		/// An interface event declaration node.
		/// </summary>
		InterfaceEventDeclaration,

		/// <summary>
		/// An interface method declaration node.
		/// </summary>
		InterfaceMethodDeclaration,

		/// <summary>
		/// An interface property declaration node.
		/// </summary>
		InterfacePropertyDeclaration,

		/// <summary>
		/// A method declaration node.
		/// </summary>
		MethodDeclaration,

		/// <summary>
		/// An operator declaration node.
		/// </summary>
		OperatorDeclaration,

		/// <summary>
		/// A property declaration node.
		/// </summary>
		PropertyDeclaration,

		/// <summary>
		/// A query expression aggregate operator.
		/// </summary>
		AggregateQueryOperator,

		/// <summary>
		/// A query expression collection range variable declaration.
		/// </summary>
		CollectionRangeVariableDeclaration,

		/// <summary>
		/// A query expression distinct operator.
		/// </summary>
		DistinctQueryOperator,

		/// <summary>
		/// A query expression from operator.
		/// </summary>
		FromQueryOperator,

		/// <summary>
		/// A query expression group operator.
		/// </summary>
		GroupQueryOperator,

		/// <summary>
		/// A query expression join condition.
		/// </summary>
		JoinCondition,

		/// <summary>
		/// A query expression join operator.
		/// </summary>
		JoinQueryOperator,

		/// <summary>
		/// A query expression let operator.
		/// </summary>
		LetQueryOperator,

		/// <summary>
		/// A query expression order-by operator.
		/// </summary>
		OrderByQueryOperator,

		/// <summary>
		/// A query expression ordering.
		/// </summary>
		Ordering,

		/// <summary>
		/// A query expression query expression.
		/// </summary>
		QueryExpression,

		/// <summary>
		/// A query expression select operator.
		/// </summary>
		SelectQueryOperator,

		/// <summary>
		/// A query expression skip operator.
		/// </summary>
		SkipQueryOperator,

		/// <summary>
		/// A query expression skip while operator.
		/// </summary>
		SkipWhileQueryOperator,

		/// <summary>
		/// A query expression take operator.
		/// </summary>
		TakeQueryOperator,

		/// <summary>
		/// A query expression take while operator.
		/// </summary>
		TakeWhileQueryOperator,

		/// <summary>
		/// A query expression where operator.
		/// </summary>
		WhereQueryOperator,

		/// <summary>
		/// An array erase statement node.
		/// </summary>
		ArrayEraseStatement,

		/// <summary>
		/// An array redim statement node.
		/// </summary>
		ArrayRedimStatement,
		
		/// <summary>
		/// A block statement node.
		/// </summary>
		BlockStatement,

		/// <summary>
		/// A branch statement node.
		/// </summary>
		BranchStatement,

		/// <summary>
		/// A break statement node.
		/// </summary>
		BreakStatement,

		/// <summary>
		/// A catch clause node.
		/// </summary>
		CatchClause,

		/// <summary>
		/// A checked statement node.
		/// </summary>
		CheckedStatement,

		/// <summary>
		/// A continue statement node.
		/// </summary>
		ContinueStatement,

		/// <summary>
		/// A do statement node.
		/// </summary>
		DoStatement,
		
		/// <summary>
		/// An else-if section node.
		/// </summary>
		ElseIfSection,

		/// <summary>
		/// An empty statement node.
		/// </summary>
		EmptyStatement,
		
		/// <summary>
		/// An exit statement node.
		/// </summary>
		ExitStatement,

		/// <summary>
		/// A fixed statement node.
		/// </summary>
		FixedStatement,

		/// <summary>
		/// A for each statement node.
		/// </summary>
		ForEachStatement,

		/// <summary>
		/// A for statement node.
		/// </summary>
		ForStatement,

		/// <summary>
		/// A go to statement node.
		/// </summary>
		GoToStatement,

		/// <summary>
		/// An if statement node.
		/// </summary>
		IfStatement,

		/// <summary>
		/// A labeled statement node.
		/// </summary>
		LabeledStatement,

		/// <summary>
		/// A local variable declaration node.
		/// </summary>
		LocalVariableDeclaration,

		/// <summary>
		/// A lock statement node.
		/// </summary>
		LockStatement,
		
		/// <summary>
		/// A modify event handler statement node.
		/// </summary>
		ModifyEventHandlerStatement,

		/// <summary>
		/// A raise event statement node.
		/// </summary>
		RaiseEventStatement,
		
		/// <summary>
		/// A return statement node.
		/// </summary>
		ReturnStatement,

		/// <summary>
		/// A statement expression node.
		/// </summary>
		StatementExpression,
		
		/// <summary>
		/// A switch label node.
		/// </summary>
		SwitchLabel,

		/// <summary>
		/// A switch section node.
		/// </summary>
		SwitchSection,

		/// <summary>
		/// A switch statement node.
		/// </summary>
		SwitchStatement,

		/// <summary>
		/// A throw statement node.
		/// </summary>
		ThrowStatement,

		/// <summary>
		/// A try statement node.
		/// </summary>
		TryStatement,

		/// <summary>
		/// An unchecked statement node.
		/// </summary>
		UncheckedStatement,

		/// <summary>
		/// An unsafe statement node.
		/// </summary>
		UnsafeStatement,
		
		/// <summary>
		/// An unstructured error error statement.
		/// </summary>
		UnstructuredErrorErrorStatement,

		/// <summary>
		/// An unstructured error on error statement.
		/// </summary>
		UnstructuredErrorOnErrorStatement,

		/// <summary>
		/// An unstructured error resume next statement.
		/// </summary>
		UnstructuredErrorResumeNextStatement,

		/// <summary>
		/// A using statement node.
		/// </summary>
		UsingStatement,

		/// <summary>
		/// A while statement node.
		/// </summary>
		WhileStatement,

		/// <summary>
		/// A with statement node.
		/// </summary>
		WithStatement,

		/// <summary>
		/// A yield statement node.
		/// </summary>
		YieldStatement,

		/// <summary>
		/// A class declaration node.
		/// </summary>
		ClassDeclaration,

		/// <summary>
		/// A delegate declaration node.
		/// </summary>
		DelegateDeclaration,

		/// <summary>
		/// An enumeration declaration node.
		/// </summary>
		EnumerationDeclaration,

		/// <summary>
		/// An interface declaration node.
		/// </summary>
		InterfaceDeclaration,
		
		/// <summary>
		/// A standard module declaration node.
		/// </summary>
		StandardModuleDeclaration,

		/// <summary>
		/// A structure declaration node.
		/// </summary>
		StructureDeclaration,

	}
}
