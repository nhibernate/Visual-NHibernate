using System;
using ActiproSoftware.SyntaxEditor.Addons.CSharp;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Context;
using ActiproSoftware.SyntaxEditor.Addons.VB;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Helper class to resolve an <see cref="Expression"/> AST node into an <see cref="IDomTypeReference"/>.
	/// </summary>
	internal class ExpressionResolver {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Create a <see cref="DotNetContext"/> to use.
		/// </summary>
		/// <param name="callingContext">The <see cref="DotNetContext"/> that is calling the method.</param>
		/// <returns>The <see cref="DotNetContext"/> that was created.</returns>
		private static DotNetContext CreateContext(DotNetContext callingContext) {
			DotNetContext context;
			DotNetSyntaxLanguage dotNetLanguage = callingContext.Language as DotNetSyntaxLanguage;
			if ((dotNetLanguage != null) && (dotNetLanguage.LanguageType == DotNetLanguage.VB))
				context = new VBContext(callingContext, dotNetLanguage, callingContext.TargetOffset);
			else
				context = new CSharpContext(callingContext, dotNetLanguage, callingContext.TargetOffset);

			context.CompilationUnit = callingContext.CompilationUnit;
			context.ProjectResolver = callingContext.ProjectResolver;
			
			return context;
		}
		
		/// <summary>
		/// Creates the items in a <see cref="DotNetContext"/> based on a recursive search through expressions.
		/// </summary>
		/// <param name="context">The <see cref="DotNetContext"/> to update.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <param name="expression">The <see cref="Expression"/> to examine.</param>
		private static void CreateContextItems(DotNetContext context, IDomType contextType, Expression expression) {
			if (expression != null) {
				switch (expression.NodeType) {
					case DotNetNodeType.InvocationExpression: {
						// Invocation expression
						InvocationExpression invocationExpression = expression as InvocationExpression;
						if ((invocationExpression != null) && (invocationExpression.Expression != null)) {
							// Recurse
							ExpressionResolver.CreateContextItems(context, contextType, invocationExpression.Expression);

							// If there is a context item for this expression...
							if (context.Items.Length > 0) {
								// If there are arguments...
								IAstNodeList arguments = invocationExpression.Arguments;
								if ((arguments != null) && (arguments.Count > 0)) {
									DotNetContextItem item = context.TargetItem;

									// If the expression is definitely an indexer invocation, specify the argument count
									if ((invocationExpression.IsIndexerInvocation) ||
										(context is VBContext))  // 1/24/2011 - Added since VB indexer invocations are ambiguous with method invocations
										item.IndexerParameterCounts = new int[] { arguments.Count };

									// Create the unresolved arguments
									item.UnresolvedArguments = new Expression[arguments.Count];
									for (int index = 0; index < item.UnresolvedArguments.Length; index++)
										item.UnresolvedArguments[index] = arguments[index] as Expression;
								}
							}
						}
						break;
					}
					case DotNetNodeType.MemberAccess: {
						// Member access
						MemberAccess memberAccess = expression as MemberAccess;
						if ((memberAccess != null) && (memberAccess.MemberName != null)) {
							DotNetContextItem item = new DotNetContextItem(TextRange.Deleted, memberAccess.MemberName.Text);
							context.InsertItems(0, new DotNetContextItem[] { item });

							// Recurse
							ExpressionResolver.CreateContextItems(context, contextType, memberAccess.Expression);
						}
						break;
					}
					case DotNetNodeType.SimpleName: {
						// Simple name
						SimpleName simpleName = expression as SimpleName;
						if (simpleName != null) {
							DotNetContextItem item = new DotNetContextItem(TextRange.Deleted, simpleName.Name);
							context.InsertItems(0, new DotNetContextItem[] { item });
						}
						break;
					}
				}
			}
		}

		/// <summary>
		/// Resolves an <see cref="Expression"/> AST node into an <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="callingContext">The <see cref="DotNetContext"/> that is calling the method.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <param name="expression">The <see cref="Expression"/> to examine.</param>
		/// <returns>The <see cref="IDomTypeReference"/> result.</returns>
		internal static IDomTypeReference Resolve(DotNetContext callingContext, IDomType contextType, Expression expression) {
			if (expression != null) {
				switch (expression.NodeType) {
					case DotNetNodeType.ArgumentExpression:
					case DotNetNodeType.CheckedExpression:
					case DotNetNodeType.ParenthesizedExpression:
					case DotNetNodeType.UnaryExpression:  // Assume that the unary expression returns the same sort of expression result
					case DotNetNodeType.UncheckedExpression: {
						// Any expression that inherits ChildExpressionExpression
						ChildExpressionExpression childExpressionExp = expression as ChildExpressionExpression;
						if (childExpressionExp != null) 
							return ExpressionResolver.Resolve(callingContext, contextType, childExpressionExp.Expression);
						break;
					}
					case DotNetNodeType.BinaryExpression: {
						// Binary expression
						BinaryExpression binaryExp = expression as BinaryExpression;
						if (binaryExp != null) {
							IDomTypeReference left = ExpressionResolver.Resolve(callingContext, contextType, binaryExp.LeftExpression);
							IDomTypeReference right = ExpressionResolver.Resolve(callingContext, contextType, binaryExp.RightExpression);
							if ((left != null) && (right != null)) {
								switch (binaryExp.OperatorType) {
									case OperatorType.Addition:
									case OperatorType.BitwiseAnd:
									case OperatorType.BitwiseOr:
									case OperatorType.Division:
									case OperatorType.ExclusiveOr:
									case OperatorType.Exponentiation:
									case OperatorType.LeftShift:
									case OperatorType.Modulus:
									case OperatorType.Multiply:
									case OperatorType.RightShift:
									case OperatorType.Subtraction: {
										string leftFullName = left.FullName;
										string rightFullName = right.FullName;

										// Return a string if there is string concatination (C# doesn't have an explicit OperatorType.StringConcatenation)
										if ((binaryExp.OperatorType == OperatorType.Addition) && ((leftFullName == "System.String") || (rightFullName == "System.String")))
											return new TypeReference("System.String", expression.TextRange);

										// Reals
										if ((leftFullName == "System.Double") || (rightFullName == "System.Double"))
											return new TypeReference("System.Double", expression.TextRange);
										if ((leftFullName == "System.Single") || (rightFullName == "System.Single"))
											return new TypeReference("System.Single", expression.TextRange);
										if ((leftFullName == "System.Decimal") || (rightFullName == "System.Decimal"))
											return new TypeReference("System.Decimal", expression.TextRange);

										// Integers
										if ((leftFullName == "System.Int64") || (rightFullName == "System.Int64"))
											return new TypeReference("System.Int64", expression.TextRange);
										if ((leftFullName == "System.UInt64") || (rightFullName == "System.UInt64"))
											return new TypeReference("System.UInt64", expression.TextRange);
										if ((leftFullName == "System.Int32") || (rightFullName == "System.Int32"))
											return new TypeReference("System.Int32", expression.TextRange);
										if ((leftFullName == "System.UInt32") || (rightFullName == "System.UInt32"))
											return new TypeReference("System.UInt32", expression.TextRange);
										if ((leftFullName == "System.Int16") || (rightFullName == "System.Int16"))
											return new TypeReference("System.Int16", expression.TextRange);
										if ((leftFullName == "System.UInt16") || (rightFullName == "System.UInt16"))
											return new TypeReference("System.UInt16", expression.TextRange);
										if ((leftFullName == "System.SByte") || (rightFullName == "System.SByte"))
											return new TypeReference("System.SByte", expression.TextRange);
										if ((leftFullName == "System.Byte") || (rightFullName == "System.Byte"))
											return new TypeReference("System.Byte", expression.TextRange);
										break;
									}
									case OperatorType.ConditionalAnd:
									case OperatorType.ConditionalOr:
									case OperatorType.Equality:
									case OperatorType.False:
									case OperatorType.GreaterThan:
									case OperatorType.GreaterThanOrEqual:
									case OperatorType.Inequality:
									case OperatorType.LessThan:
									case OperatorType.LessThanOrEqual:
									case OperatorType.Like:
									case OperatorType.Negation:
									case OperatorType.ReferenceEquality:
									case OperatorType.ReferenceInequality:
									case OperatorType.True:
										return new TypeReference("System.Boolean", expression.TextRange);
									case OperatorType.IntegerDivision:
										return new TypeReference("System.Int32", expression.TextRange);
									case OperatorType.Mid:
									case OperatorType.StringConcatenation:
										return new TypeReference("System.String", expression.TextRange);
									case OperatorType.NullCoalescing:
										break;
								}
							}
						}
						break;
					}
					case DotNetNodeType.CastExpression: {
						// Cast expression
						CastExpression castExp = expression as CastExpression;
						if (castExp != null) 
							return castExp.ReturnType;
						break;
					}
					case DotNetNodeType.IsTypeOfExpression: {
						// Is-type-of expression
						return new TypeReference("System.Boolean", expression.TextRange);
					}
					case DotNetNodeType.LiteralExpression: {
						// Literal expression
						LiteralExpression literalExp = expression as LiteralExpression;
						if (literalExp != null) {
							// Examine the literal type
							switch (literalExp.LiteralType) {
								case LiteralType.Character:
									return new TypeReference("System.Char", TextRange.Deleted);
								case LiteralType.Date:
									return new TypeReference("System.DateTime", expression.TextRange);
								case LiteralType.DecimalInteger:
								case LiteralType.HexadecimalInteger:
								case LiteralType.OctalInteger:
									return new TypeReference("System.Int32", expression.TextRange);
								case LiteralType.False:
								case LiteralType.True:
									return new TypeReference("System.Boolean", expression.TextRange);
								case LiteralType.Real:
									return new TypeReference("System.Double", expression.TextRange);
								case LiteralType.String:
								case LiteralType.VerbatimString:
									return new TypeReference("System.String", expression.TextRange);
							}
						}
						break;
					}
					case DotNetNodeType.InvocationExpression:
					case DotNetNodeType.MemberAccess:
					case DotNetNodeType.SimpleName: {
						if ((callingContext != null) && (callingContext.Language != null) && (callingContext.CompilationUnit != null) && (callingContext.ProjectResolver != null)) {
							// Build and resolve a context
							DotNetContext context = ExpressionResolver.CreateContext(callingContext);
							context.Type = DotNetContextType.NamespaceTypeOrMember;
							ExpressionResolver.CreateContextItems(context, contextType, expression);
							context.ResolveForCode(null, callingContext.CompilationUnit, callingContext.ProjectResolver);

							// Recurse
							if (context.TargetItem != null) {
								IDomMember member = context.TargetItem.ResolvedInfo as IDomMember;
								if (member != null)
									return callingContext.ProjectResolver.ConstructAndResolveMemberReturnType(context, context.Items.Length - 1, contextType);

								IDomType type = context.TargetItem.ResolvedInfo as IDomType;
								if (type != null)
									return callingContext.ProjectResolver.ConstructAndResolveFromSelf(type);
							}
						}						
						break;
					}
					case DotNetNodeType.ObjectCreationExpression: {
						// Object creation expression
						ObjectCreationExpression objCreationExpression = expression as ObjectCreationExpression;
						if (objCreationExpression != null)
							return objCreationExpression.ObjectType;
						break;
					}
					case DotNetNodeType.TryCastExpression: {
						// Try-cast expression
						TryCastExpression tryCastExp = expression as TryCastExpression;
						if (tryCastExp != null) 
							return tryCastExp.ReturnType;
						break;
					}
					case DotNetNodeType.TypeOfExpression: {
						// Type-of expression
						return new TypeReference("System.Type", expression.TextRange);
					}
				}
			}

			return null;
		}
		
		/// <summary>
		/// Resolves an <see cref="Expression"/> AST node into an anonymous type.
		/// </summary>
		/// <param name="callingContext">The <see cref="DotNetContext"/> that is calling the method.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <param name="expression">The <see cref="Expression"/> to examine.</param>
		/// <returns>The <see cref="IDomType"/> result.</returns>
		internal static IDomType ResolveAnonymousType(DotNetContext callingContext, IDomType contextType, Expression expression) {
			if (callingContext != null) {
				// If an object creation expression was used...
				ObjectCreationExpression objCreationExpression = expression as ObjectCreationExpression;
				if (objCreationExpression != null) {
					// If the initializer is an object collection initializer...
					ObjectCollectionInitializerExpression objCollectionInitExpression = objCreationExpression.Initializer as ObjectCollectionInitializerExpression;
					if (objCollectionInitExpression != null) {
						// If there are initializers...
						IAstNodeList initializers = objCollectionInitExpression.Initializers;
						if (initializers != null) {
							// Create an anonymous type
							ClassDeclaration anonymousType = new ClassDeclaration(Modifiers.Public, new QualifiedIdentifier(TypeReference.AnonymousTypeName));

							// Loop through each initializer
							for (int index = 0; index < initializers.Count; index++) {
								AssignmentExpression assignment = initializers[index] as AssignmentExpression;
								if (assignment != null) {
									// An assignment expression
									SimpleName name = assignment.LeftExpression as SimpleName;
									if (name == null) {
										// There is no SimpleName in the left expression, so try and derive a name from the right expression
										name = assignment.RightExpression as SimpleName;
									}

									if (name != null) {
										// Get the return type
										IDomTypeReference returnTypeRef = ExpressionResolver.Resolve(callingContext, contextType, assignment.RightExpression);
										if (returnTypeRef != null) {
											// Try to resolve to a type
											IDomType returnType = returnTypeRef.Resolve(callingContext.ProjectResolver);
											if (returnType != null) {
												// Build a property
												PropertyDeclaration propertyDecl = new AnonymousTypePropertyDeclaration(Modifiers.Public, new QualifiedIdentifier(name.Name), returnType);
												anonymousType.Members.Add(propertyDecl);
											}
										}
									}
								}
							}

							// Return the anonymous type
							return anonymousType;
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Resolves a <see cref="ParameterDeclaration"/> within a <see cref="LambdaExpression"/> to an <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="callingContext">The <see cref="DotNetContext"/> that is calling the method.</param>
		/// <param name="parameter">The <see cref="ParameterDeclaration"/> to resolve.</param>
		/// <returns>The <see cref="IDomTypeReference"/> result.</returns>
		internal static IDomTypeReference ResolveLambdaExpressionParameter(DotNetContext callingContext, ParameterDeclaration parameter) {
			// The parent node should be a lambda expression if this method is called
			LambdaExpression lambdaExpression = parameter.ParentNode as LambdaExpression;
			if (lambdaExpression != null) {
				// Get the parameter index
				int parameterIndex = lambdaExpression.Parameters.IndexOf(parameter);
				if (parameterIndex != -1) {
					// If the parent is a variable declarator...
					VariableDeclarator declarator = lambdaExpression.ParentNode as VariableDeclarator;
					if (declarator != null) {
						// If there is a return type...
						if (declarator.ReturnType != null) {
							// If the return type resolves to a delegate...
							ConstructedGenericType returnType = callingContext.ProjectResolver.ConstructAndResolveFromSelf(declarator.ReturnType) as ConstructedGenericType;
							if ((returnType != null) && (returnType.Type == DomTypeType.Delegate)) {
								// Get the Invoke method
								IDomMember invokeMember = returnType.GetMember(new IDomType[] { returnType }, "Invoke", DomBindingFlags.Instance | DomBindingFlags.Public);
								if ((invokeMember != null) && (parameterIndex < invokeMember.Parameters.Length)) {
									// Get the parameter to examine
									IDomParameter invokeParameter = invokeMember.Parameters[parameterIndex];
									if ((invokeParameter.ParameterType != null) && (invokeParameter.ParameterType.IsGenericParameter) &&
										(returnType.GenericTypeArguments != null) && (returnType.GenericDefinitionType.GenericTypeArguments != null)) {
										// Build arrays of generic type parameters/arguments
										IDomTypeReference[] genericTypeParameters = new IDomTypeReference[returnType.GenericDefinitionType.GenericTypeArguments.Count];
										IDomTypeReference[] genericTypeArguments = new IDomTypeReference[returnType.GenericTypeArguments.Count];
										returnType.GenericDefinitionType.GenericTypeArguments.CopyTo(genericTypeParameters, 0);
										returnType.GenericTypeArguments.CopyTo(genericTypeArguments, 0);

										if (genericTypeParameters.Length == genericTypeArguments.Length) {
											// Find the parameter index within the generic type parmaeters
											for (parameterIndex = 0; parameterIndex < genericTypeParameters.Length; parameterIndex++) {
												if (genericTypeParameters[parameterIndex].Name == invokeParameter.ParameterType.Name)
													break;
											}

											// If a parameter was found
											if (parameterIndex < genericTypeParameters.Length) 
												return genericTypeArguments[parameterIndex];
										}
									}
								}
							}
						}
					}
					else {
						// If the parent is an argument expression...
						ArgumentExpression argumentExpression = lambdaExpression.ParentNode as ArgumentExpression;
						if (argumentExpression != null) {
							InvocationExpression invocationExpression = argumentExpression.ParentNode as InvocationExpression;
							if (invocationExpression != null) {
								int argumentIndex = invocationExpression.Arguments.IndexOf(argumentExpression);
								if (argumentIndex != -1) {
									// Lambda expression passed to a method invocation (maybe an extension method)
									// TODO: Code in here can easily cause infinite recursion... implement later							
								}
							}
						}
					}
				}
			}
			return null;
		}

	}

}
