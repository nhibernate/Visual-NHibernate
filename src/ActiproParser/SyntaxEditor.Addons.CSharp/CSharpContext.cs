// #define DEBUG_RESOLVE

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using ActiproSoftware.ComponentModel;
using ActiproSoftware.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Context;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;
using ActiproSoftware.SyntaxEditor.ParserGenerator;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {
	
	/// <summary>
	/// Represents the <c>C#</c> language context for a specific offset.
	/// </summary>
	public class CSharpContext : DotNetContext {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>CSharpContext</c> class.
		/// </summary>
		/// <param name="parentContext">The <see cref="DotNetContext"/>, if any, that created this context.</param>
		/// <param name="language">The <see cref="SyntaxLanguage"/> that created the context.</param>
		/// <param name="targetOffset">The target offset.</param>
		internal CSharpContext(DotNetContext parentContext, SyntaxLanguage language, int targetOffset) : 
			this(parentContext, language, targetOffset, DotNetContextType.None, null) { }
	
		/// <summary>
		/// Initializes a new instance of the <c>CSharpContext</c> class.
		/// </summary>
		/// <param name="parentContext">The <see cref="DotNetContext"/>, if any, that created this context.</param>
		/// <param name="language">The <see cref="SyntaxLanguage"/> that created the context.</param>
		/// <param name="targetOffset">The target offset.</param>
		/// <param name="type">The <see cref="DotNetContextType"/> that describes the type of context.</param>
		/// <param name="items">The <see cref="ArrayList"/> of context items.</param>
		internal CSharpContext(DotNetContext parentContext, SyntaxLanguage language, int targetOffset, DotNetContextType type, ArrayList items) : 
			base(parentContext, language, targetOffset, type, items) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="CSharpContext"/> that describes the context at the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <returns>The <see cref="CSharpContext"/> that describes the context at the specified offset.</returns>
		private static CSharpContext GetContextForCode(Document document, int offset) {
			DotNetContextType contextType = DotNetContextType.NamespaceTypeOrMember;

			// Move to the start of the current token
			TextStream stream = document.GetTextStream(offset);
			int targetOffset = stream.Token.StartOffset;
			stream.Offset = targetOffset;

			// 10/29/2010 - If getting context for an identifier, move forward if there are generic arguments specified after it
			//   and this is a method call (31E-144D7E88-8521)
			bool exitLoop = false;
			if (stream.Token.ID == CSharpTokenID.Identifier) {
				// Skip past the identifier
				int nestingLevel = 1;
				stream.ReadToken();

				switch (stream.Token.ID) {
					case CSharpTokenID.Whitespace:
						// Advance past whitespace
						stream.ReadToken();
						break;
					case CSharpTokenID.LessThan: {
						// Skip past any generic arguments
						stream.ReadToken();
						while (!stream.IsAtDocumentEnd) {
							switch (stream.Token.ID) {
								case CSharpTokenID.Comma:
								case CSharpTokenID.Dot:
								case CSharpTokenID.Identifier:
								case CSharpTokenID.Whitespace:
									// Allow
									break;
								case CSharpTokenID.LessThan:
									nestingLevel++;
									break;
								case CSharpTokenID.GreaterThan:
									if (--nestingLevel == 0) {
										// Store offset
										int greaterThanOffset = stream.Offset;

										// Skip past '>' and ensure the next character is a '(', meaning a generic method call
										stream.ReadToken();

										// Skip whitespace
										while ((!stream.IsAtDocumentEnd) && (stream.Token.ID == CSharpTokenID.Whitespace))
											stream.ReadToken();

										if ((!stream.IsAtDocumentEnd) && (stream.Token.ID == CSharpTokenID.OpenParenthesis)) {
											// Is a generic method call, jump back to '>'
											stream.Offset = greaterThanOffset;
										}
										else {
											// Is not a generic method call... make sure nesting level check fails
											nestingLevel = 1;
										}

										// Exit the loop
										exitLoop = true;
									}
									break;
								default:
									// Exit the loop
									exitLoop = true;
									break;
							}

							if (exitLoop)
								break;

							stream.ReadToken();
						}
						break;
					}
				}

				// Is not a valid generic arguments specification so jump back to the target offset
				if (nestingLevel > 0)
					stream.Offset = targetOffset;
			}

			// Skip over brackets
			int[] indexerParameterCounts = null;
			int currentIndexerParameterCount;
			IDomTypeReference[] genericTypeArguments = null;
			string argumentsText = null;
			exitLoop = false;
			while (!stream.IsAtDocumentStart) {
				switch (stream.Token.ID) {
					case CSharpTokenID.GreaterThan: {
						// Move to just after the start of the generic type parameter list
						int genericTypeParameterEndOffset = stream.Offset;
						stream.GoToPreviousMatchingToken(stream.Token, CSharpTokenID.LessThan);
						int genericTypeParameterStartOffset = stream.Offset;
						stream.ReadToken();

						// Scan the generic type arguments
						genericTypeArguments = CSharpContext.GetGenericTypeArguments(stream, genericTypeParameterEndOffset);

						// Restore the stream offset
						stream.Offset = genericTypeParameterStartOffset;
						stream.ReadTokenReverse();
						break;
					}
					case CSharpTokenID.CloseParenthesis: {
						int argumentsEndOffset = stream.Offset;

						stream.GoToPreviousMatchingToken(stream.Token);

						// Get the arguments text
						if (stream.Offset + 1 < argumentsEndOffset)
							argumentsText = document.GetSubstring(new TextRange(stream.Offset + 1, argumentsEndOffset));

						stream.ReadTokenReverse();
						break;
					}
					case CSharpTokenID.CloseSquareBrace:
						// Count the indexer parameters
						currentIndexerParameterCount = 0;
						while ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID != CSharpTokenID.OpenSquareBrace)) {
							switch (stream.Token.ID) {
								case CSharpTokenID.CloseParenthesis:
								case CSharpTokenID.CloseSquareBrace:
									stream.GoToPreviousMatchingToken(stream.Token);
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
								case CSharpTokenID.Comma:
									if (currentIndexerParameterCount > 0)
										currentIndexerParameterCount++;
									break;
								default:
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
							}
						}
						stream.ReadTokenReverse();
						DotNetContextItem.AppendIndexerParameterCountLevel(ref indexerParameterCounts, currentIndexerParameterCount);
						break;
					case CSharpTokenID.Whitespace:
						// Skip over whitespace
						stream.ReadTokenReverse();
						break;
					default:
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}

			// Get the target
			DotNetContextItem targetItem = new DotNetContextItem(stream.Token.TextRange, stream.Document.GetTokenText(stream.Token));
			targetItem.GenericTypeArguments = genericTypeArguments;
			genericTypeArguments = null;
			targetItem.IndexerParameterCounts = indexerParameterCounts;
			indexerParameterCounts = null;
			targetItem.ArgumentsText = argumentsText;
			argumentsText = null;
			ArrayList items = new ArrayList();

			switch (stream.Token.ID) {
				case CSharpTokenID.Base:
					// If the context is on a "base" keyword, simply return that
					targetItem.Type = DotNetContextItemType.Base;
					items.Add(targetItem);
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.BaseAccess, items);
				case CSharpTokenID.DecimalIntegerLiteral:
					// If the context is on a decimal number, simply return that
					targetItem.Type = DotNetContextItemType.Number;
					items.Add(targetItem);
					if ((!stream.IsAtDocumentStart) && (stream.PeekTokenReverse().ID == CSharpTokenID.Subtraction))
						targetItem.Text = "-" + targetItem.Text;
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.DecimalIntegerLiteral, items);
				case CSharpTokenID.HexadecimalIntegerLiteral:
					// If the context is on a hexadecimal number, simply return that
					targetItem.Type = DotNetContextItemType.Number;
					items.Add(targetItem);
					if ((!stream.IsAtDocumentStart) && (stream.PeekTokenReverse().ID == CSharpTokenID.Subtraction))
						targetItem.Text = "-" + targetItem.Text;
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.HexadecimalIntegerLiteral, items);
				case CSharpTokenID.Identifier:
					// Ensure the target item is added to the items 
					items.Add(targetItem);
					break;
				case CSharpTokenID.New:
					// If the context is on a "new" keyword, simply return that
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.NewObjectDeclaration, null);
				case CSharpTokenID.StringLiteral:
				case CSharpTokenID.VerbatimStringLiteral:
					// If the context is on a string literal, simply return that
					targetItem.Type = DotNetContextItemType.StringLiteral;
					items.Add(targetItem);
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.StringLiteral, items);
				case CSharpTokenID.This:
					// If the context is on a "this" keyword, simply return that
					targetItem.Type = DotNetContextItemType.This;
					items.Add(targetItem);
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.ThisAccess, items);
				case CSharpTokenID.Using:
					// If the context is on a "using" keyword, simply return that
					return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.UsingDeclaration, null);
				default:
					if (CSharpToken.IsNativeType(stream.Token.ID)) {
						// The token is on a native type, simply return that
						string[] identifiers = DotNetProjectResolver.GetTypeFullNameFromShortcut(DotNetLanguage.CSharp, targetItem.Text).Split(new char[] { '.' });
						items.Add(new DotNetContextItem(DotNetContextItemType.Namespace, 
							new TextRange(stream.Token.StartOffset, stream.Token.StartOffset + identifiers[0].Length), identifiers[0]));
						targetItem.Type = DotNetContextItemType.Type;
						targetItem.TextRange = new TextRange(stream.Token.EndOffset - identifiers[0].Length, stream.Token.EndOffset);
						targetItem.Text = identifiers[1];
						items.Add(targetItem);
						return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.NativeType, items);
					}
					return new CSharpContext(null, document.Language, targetOffset);
			}

			exitLoop = false;
			bool lastWasDot = (stream.Token.ID == CSharpTokenID.Dot);
			while (!stream.IsAtDocumentStart) {
				IToken token = stream.ReadTokenReverse();

				// Continue if on whitespace or a comment
				if ((token.IsWhitespace) || (token.IsComment))
					continue;

				switch (token.ID) {
					case CSharpTokenID.Assignment:
						// Back up and see if there is a "using Identifier =" before this
						stream.GoToPreviousNonWhitespaceOrCommentToken();
						stream.GoToPreviousNonWhitespaceOrCommentToken();
						if ((!stream.IsAtDocumentEnd) && (stream.PeekToken().ID == CSharpTokenID.Using)) {
							// Over a namespace alias declaration
							contextType = DotNetContextType.UsingDeclaration;
						}
						exitLoop = true;
						break;
					case CSharpTokenID.CloseParenthesis: {
						// If the last character was not a dot, quit the loop
						if (!lastWasDot) {
							exitLoop = true;
							break;
						}

						int argumentsEndOffset = stream.Offset;

						// Skip over parenthesis
						stream.GoToPreviousMatchingToken(stream.Token);

						// Get the arguments text
						if (stream.Offset + 1 < argumentsEndOffset)
							argumentsText = document.GetSubstring(new TextRange(stream.Offset + 1, argumentsEndOffset));
						break;
					}
					case CSharpTokenID.CloseSquareBrace:
						// If the last character was not a dot, quit the loop
						if (!lastWasDot) {
							exitLoop = true;
							break;
						}

						// Count the indexer parameters
						currentIndexerParameterCount = 0;
						while ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID != CSharpTokenID.OpenSquareBrace)) {
							switch (stream.Token.ID) {
								case CSharpTokenID.CloseParenthesis:
								case CSharpTokenID.CloseSquareBrace:
									stream.GoToPreviousMatchingToken(stream.Token);
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
								case CSharpTokenID.Comma:
									if (currentIndexerParameterCount > 0)
										currentIndexerParameterCount++;
									break;
								default:
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
							}
						}
						DotNetContextItem.AppendIndexerParameterCountLevel(ref indexerParameterCounts, currentIndexerParameterCount);
						break;
					case CSharpTokenID.Dot:
						if (lastWasDot)
							exitLoop = true;
						else
							lastWasDot = true;
						break;
					case CSharpTokenID.GreaterThan: {
						// Move to just after the start of the generic type parameter list
						int genericTypeParameterEndOffset = stream.Offset;
						stream.GoToPreviousMatchingToken(stream.Token, CSharpTokenID.LessThan);
						int genericTypeParameterStartOffset = stream.Offset;
						stream.ReadToken();

						// Scan the generic type arguments
						genericTypeArguments = CSharpContext.GetGenericTypeArguments(stream, genericTypeParameterEndOffset);

						// Restore the stream offset
						stream.Offset = genericTypeParameterStartOffset;
						break;
					}
					case CSharpTokenID.Identifier:
						if (!lastWasDot)
							exitLoop = true;
						else {
							items.Insert(0, new DotNetContextItem(token.TextRange, stream.Document.GetTokenText(token)));
							((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
							genericTypeArguments = null;
							((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
							indexerParameterCounts = null;
							((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
							argumentsText = null;
							lastWasDot = false;
						}
						break;
					case CSharpTokenID.This:
						// The context is a member access on "this"
						contextType = DotNetContextType.ThisMemberAccess;
						items.Insert(0, new DotNetContextItem(DotNetContextItemType.This, token.TextRange, stream.Document.GetTokenText(token)));
						((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
						genericTypeArguments = null;
						((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
						indexerParameterCounts = null;
						((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
						argumentsText = null;
						exitLoop = true;
						break;
					case CSharpTokenID.Base:
						// The context is a member access on "base"
						contextType = DotNetContextType.BaseMemberAccess;
						items.Insert(0, new DotNetContextItem(DotNetContextItemType.Base, token.TextRange, stream.Document.GetTokenText(token)));
						((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
						genericTypeArguments = null;
						((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
						indexerParameterCounts = null;
						((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
						argumentsText = null;
						exitLoop = true;
						break;
					case CSharpTokenID.New:
						// The context is a "new" object declaration
						contextType = DotNetContextType.NewObjectDeclaration;
						exitLoop = true;
						break;
					case CSharpTokenID.Using:
						// Check to make sure we are not over the alias of a using statement
						stream.Offset = targetOffset;
						stream.GoToNextNonWhitespaceOrCommentToken();
						if (stream.PeekToken().ID != CSharpTokenID.Assignment) {
							// The context is using declaration... identifiers must all be namespaces
							contextType = DotNetContextType.UsingDeclaration;
						}
						else {
							// Nullify the context... over a namespace alias declaration
							contextType = DotNetContextType.None;
						}
						exitLoop = true;
						break;
					case CSharpTokenID.Class:
					case CSharpTokenID.Delegate:
					case CSharpTokenID.Enum:
					case CSharpTokenID.Interface:
					case CSharpTokenID.Namespace:
					case CSharpTokenID.Struct:
						// Ignore this context item and exit the loop... 
						//   this is a shortcut since we easily now know that it is a namespace, type, or member name
						contextType = DotNetContextType.None;
						exitLoop = true;
						break;
					case CSharpTokenID.As:
						// Exit the loop
						contextType = DotNetContextType.TryCastType;
						exitLoop = true;
						break;
					case CSharpTokenID.Is:
						// Exit the loop
						contextType = DotNetContextType.IsTypeOfType;
						exitLoop = true;
						break;
					case CSharpTokenID.StringLiteral:
						// The token is a string literal, simply return that
						items.Insert(0, new DotNetContextItem(DotNetContextItemType.StringLiteral, token.TextRange, "System.String"));
						((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
						genericTypeArguments = null;
						((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
						indexerParameterCounts = null;
						((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
						argumentsText = null;

						// Exit the loop
						exitLoop = true;
						break;
					case CSharpTokenID.OpenParenthesis:
						// Check the previous token for "typeof"
						if (!stream.IsAtDocumentStart) {
							token = stream.ReadTokenReverse();
							if (token.ID == CSharpTokenID.TypeOf)
								contextType = DotNetContextType.TypeOfType;
						}

						// Exit the loop
						exitLoop = true;
						break;
					default:
						if (CSharpToken.IsNativeType(token.ID)) {
							// The token is on a native type, simply return that
							string nativeTypeName = DotNetProjectResolver.GetTypeFullNameFromShortcut(DotNetLanguage.CSharp, CSharpTokenID.GetTokenKey(token.ID).ToString().ToLower());
							items.Insert(0, new DotNetContextItem(DotNetContextItemType.Type, token.TextRange, nativeTypeName));
							((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
							genericTypeArguments = null;
							((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
							indexerParameterCounts = null;
							((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
							argumentsText = null;
						}

						// Exit the loop
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}
			
			if ((contextType == DotNetContextType.NamespaceTypeOrMember) && (items.Count == 0))
				contextType = DotNetContextType.None;

			return new CSharpContext(null, document.Language, targetOffset, contextType, items);
		}
		
		/// <summary>
		/// Gets the <see cref="CSharpContext"/> that describes the context at the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <returns>The <see cref="CSharpContext"/> that describes the context at the specified offset.</returns>
		private static CSharpContext GetContextForDocumentationComment(Document document, int offset) {
			// Move to the start of the current token
			TextStream stream = document.GetTextStream(offset);
			stream.GoToCurrentTokenStart();
			int targetOffset = stream.Offset;

			// Move backwards and look for an open tag
			Stack tagStack = new Stack();
			while (!stream.IsAtDocumentStart) {
				stream.GoToPreviousToken();
				switch (stream.Token.ID) {
					case CSharpTokenID.DocumentationCommentDelimiter:
					case CSharpTokenID.DocumentationCommentText:
					case CSharpTokenID.LineTerminator:
					case CSharpTokenID.Whitespace:
						// Ignore
						break;
					case CSharpTokenID.DocumentationCommentTag: {
						bool isOpen, isClosed;
						string tagName;
						DotNetContext.ParseXmlTag(stream.TokenText, out isOpen, out isClosed, out tagName);
						if (tagName.Trim().Length > 0) {
							if (isOpen) {
								// If this open tag doesn't have a close tag that we already passed...
								if (tagStack.Count == 0) {
									ArrayList items = new ArrayList();
									items.Add(new DotNetContextItem(DotNetContextItemType.DocumentationCommentParentTag, stream.Token.TextRange, tagName));
									((DotNetContextItem)items[0]).TextRange = stream.Token.TextRange;
									return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.DocumentationCommentTag, items);
								}

								// Remove the last item on the stack
								tagStack.Pop();
							}
							else if (isClosed) {
								// Add the close tag name
								tagStack.Push(tagName.Trim());
							}
						}
						break;
					}
					default:
						// Return that there is no parent tag
						return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.DocumentationCommentTag, null);
				}
			}

			// Return that there is no parent tag
			return new CSharpContext(null, document.Language, targetOffset, DotNetContextType.DocumentationCommentTag, null);
		}

		/// <summary>
		/// Returns the generic type arguments that are scanned within the current stream offset and the specified end offset.
		/// </summary>
		/// <param name="stream">A <see cref="TextStream"/> used to scan text.</param>
		/// <param name="endOffset">The end offset.</param>
		/// <returns>The array of generic type arguments.</returns>
		private static IDomTypeReference[] GetGenericTypeArguments(TextStream stream, int endOffset) {
			ArrayList typeReferences = new ArrayList();

			while (stream.Offset < endOffset) {
				// Skip over whitespace
				if (stream.Token.IsWhitespace)
					stream.GoToNextNonWhitespaceToken();

				if ((stream.Token.ID == CSharpTokenID.Identifier) || (CSharpToken.IsNativeType(stream.Token.ID))) {
					// Add the name
					string typeName = stream.Document.GetTokenText(stream.Token);

					// Loop 
					while (stream.Offset < endOffset) {
						// Read the next token
						stream.ReadToken();

						// Skip over whitespace
						if (stream.Token.IsWhitespace)
							continue;

						// Exit loop on < or comma
						if ((stream.Token.ID == CSharpTokenID.LessThan) || (stream.Token.ID == CSharpTokenID.Comma))
							break;

						// Exit loop on > and skip over it
						if (stream.Token.ID == CSharpTokenID.GreaterThan) {
							stream.ReadToken();
							break;
						}

						// If a continuation, add the name
						if ((stream.Token.ID == CSharpTokenID.Dot) || (stream.Token.ID == CSharpTokenID.Identifier) || 
							((stream.Token.ID > CSharpTokenID.KeywordStart) && (stream.Token.ID < CSharpTokenID.KeywordEnd))) {
							typeName += stream.Document.GetTokenText(stream.Token);
						}
					}

					if (typeName.Length > 0) {
						// Get a child generic type argument array if appropriate
						IDomTypeReference[] genericTypeArguments = null;
						if (stream.Token.ID == CSharpTokenID.LessThan) {
							stream.ReadToken();  // Skip over <
							genericTypeArguments = CSharpContext.GetGenericTypeArguments(stream, endOffset);
						}

						// Add the type reference
						TypeReference typeReference = new TypeReference(
							DotNetProjectResolver.GetTypeFullNameFromShortcut(DotNetLanguage.CSharp, typeName), TextRange.Deleted);
						if (genericTypeArguments != null) {
							IAstNode[] astNodes = new IAstNode[genericTypeArguments.Length];
							genericTypeArguments.CopyTo(astNodes, 0);
							foreach (IAstNode node in astNodes)
								node.ParentNode = typeReference;
							typeReference.GenericTypeArguments.AddRange(astNodes);
						}
						typeReferences.Add(typeReference);
					}
				}

				// Quit if not a comma
				if (stream.Token.ID != CSharpTokenID.Comma)
					break;

				// Skip over comma
				stream.ReadToken();

				// Skip over whitespace
				if (stream.Token.IsWhitespace)
					stream.GoToNextNonWhitespaceToken();
			}

			if (typeReferences.Count > 0)
				return (IDomTypeReference[])typeReferences.ToArray(typeof(IDomTypeReference));
			else
				return null;
		}

		/// <summary>
		/// Gets whether the specified <see cref="IToken"/> is part of a documentation comment.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="IToken"/> is part of a documentation comment; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsDocumentationComment(IToken token) {
			switch (token.ID) {
				case CSharpTokenID.DocumentationCommentDelimiter:
				case CSharpTokenID.DocumentationCommentTag:
				case CSharpTokenID.DocumentationCommentText:
					return true;
				default:
					return (token.LexicalStateID == CSharpLexicalStateID.DocumentationComment);
			}
		}

		/// <summary>
		/// Resolves a <see cref="DotNetContext"/>.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> being parsed.</param>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		internal override void ResolveForCode(Document document, CompilationUnit compilationUnit, DotNetProjectResolver projectResolver) {
			#if DEBUG && DEBUG_RESOLVE
			Trace.WriteLine("ResolveForCode-Start: " + this);
			#endif

			// Save the compilation unit and project resolver
			this.CompilationUnit = compilationUnit;
			this.ProjectResolver = projectResolver;

			// If there is a compilation unit and this context does not introduce any recursion...
			if ((compilationUnit != null) && (!this.IsStartOfRecursion())) {
				// Get the containing node
				this.ContainingNode = compilationUnit.GetContainingNode(this.TargetOffset);
				if (this.Type == DotNetContextType.DocumentationCommentTag) {
					// Get the next type or member nodes, if any
					IAstNode typeOrMemberNode = new DotNetContextLocator().FindClosestTypeOrMember(compilationUnit, this.TargetOffset);
					if ((typeOrMemberNode is AstNode) && (typeOrMemberNode.StartOffset >= this.TargetOffset)) {
						if ((((AstNode)typeOrMemberNode).NodeCategory != DotNetNodeCategory.TypeDeclaration) && (this.TargetOffset < typeOrMemberNode.ParentNode.StartOffset))
							typeOrMemberNode = typeOrMemberNode.ParentNode as AstNode;
						if (
							(typeOrMemberNode.ParentNode == null) ||
							((typeOrMemberNode.ParentNode is IBlockAstNode) && (this.TargetOffset >= ((IBlockAstNode)typeOrMemberNode.ParentNode).BlockStartOffset))
							) {
							if (typeOrMemberNode is IDomMember)
								this.MemberDeclarationNode = (IDomMember)typeOrMemberNode;
							else if (typeOrMemberNode is IDomType)
								this.TypeDeclarationNode = (IDomType)typeOrMemberNode;
						}
					}
				}
				else {
					// Get the containing type node, if any
					this.TypeDeclarationNode = this.ContainingNode as TypeDeclaration;
					if ((this.TypeDeclarationNode == null) && (this.ContainingNode is AstNode))
						this.TypeDeclarationNode = ((AstNode)this.ContainingNode).ParentTypeDeclaration as TypeDeclaration;

					// Resolve the type... if it is a partial type, the merged partial type will be returned
					if (this.TypeDeclarationNode != null)
						this.TypeDeclarationNode = this.TypeDeclarationNode.Resolve(projectResolver);

					// Get the containing member node, if any
					this.MemberDeclarationNode = this.ContainingNode as IDomMember;
					if ((this.MemberDeclarationNode == null) && (this.ContainingNode != null)) {
						this.MemberDeclarationNode = this.ContainingNode.FindAncestor(typeof(IDomMember)) as IDomMember;
						if ((this.MemberDeclarationNode is VariableDeclarator) && (((VariableDeclarator)this.MemberDeclarationNode).IsLocal))
							this.MemberDeclarationNode = ((AstNode)this.MemberDeclarationNode).FindAncestor(typeof(IDomMember)) as IDomMember;
					}
					if ((this.MemberDeclarationNode != null) && (((AstNode)this.MemberDeclarationNode).NodeCategory == DotNetNodeCategory.TypeDeclaration))
						this.MemberDeclarationNode = null;
					if ((this.MemberDeclarationNode is TypeMemberDeclaration) && (((TypeMemberDeclaration)this.MemberDeclarationNode).Name != null) && (((TypeMemberDeclaration)this.MemberDeclarationNode).Name.Contains(this.TargetOffset))) {
						// If the context is for the name of a member, quit
						this.Type = DotNetContextType.None;
						#if DEBUG && DEBUG_RESOLVE
						Trace.WriteLine("ResolveForCode-End: " + this);
						#endif
						return;
					}
				}

				if (this.Items != null) {
					// Update the generic type argument parent nodes (for generic methods)
					foreach (DotNetContextItem item in this.Items) {
						if (item.GenericTypeArguments != null) {
							foreach (IAstNode typeReference in item.GenericTypeArguments)
								typeReference.ParentNode = this.ContainingNode;
						}
					}
				}

				// Get the imported namespaces
				string[] importedNamespaces;
				Hashtable namespaceAliases;
				compilationUnit.GetImportedNamespaces(this.ContainingNode, out importedNamespaces, out namespaceAliases);
				this.ImportedNamespaces = importedNamespaces;

				switch (this.Type) {
					case DotNetContextType.AnyCode:
					case DotNetContextType.DecimalIntegerLiteral:
					case DotNetContextType.HexadecimalIntegerLiteral:
						// Allow anything
						#if DEBUG && DEBUG_RESOLVE
						Trace.WriteLine("ResolveForCode-End: " + this);
						#endif
						return;
					case DotNetContextType.DocumentationCommentTag: {
						StringBuilder comment = new StringBuilder();

						if (document != null) {
							// Back up to find start of documentation comment
							TextStream stream = document.GetTextStream(this.TargetOffset);
							bool exitLoop = false;
							while (!stream.IsAtDocumentStart) {
								switch (stream.PeekTokenReverse().ID) {
									case CSharpTokenID.DocumentationCommentDelimiter:
									case CSharpTokenID.DocumentationCommentText:
									case CSharpTokenID.LineTerminator:
									case CSharpTokenID.Whitespace:
									case CSharpTokenID.DocumentationCommentTag:
										// Keep going...
										break;
									default:
										exitLoop = true;
										break;
								}
								if (exitLoop)
									break;
								stream.ReadTokenReverse();
							}

							// Build the comment
							exitLoop = false;
							while (!stream.IsAtDocumentEnd) {
								switch (stream.Token.ID) {
									case CSharpTokenID.DocumentationCommentDelimiter:
									case CSharpTokenID.Whitespace:
										// Ignore
										break;
									case CSharpTokenID.LineTerminator:
										// Add whitespace
										comment.Append(" ");
										break;
									case CSharpTokenID.DocumentationCommentTag:
									case CSharpTokenID.DocumentationCommentText:
										// Append comment text
										comment.Append(stream.TokenText);
										break;
									default:
										exitLoop = true;
										break;
								}
								if (exitLoop)
									break;
								stream.ReadToken();
							}
						}

						// Save the comment
						this.DocumentationComment = comment.ToString();

						#if DEBUG && DEBUG_RESOLVE
						Trace.WriteLine("ResolveForCode-End: " + this);
						#endif
						return;
					}
					case DotNetContextType.BaseMemberAccess:
					case DotNetContextType.IsTypeOfType:
					case DotNetContextType.NamespaceTypeOrMember:
					case DotNetContextType.NewObjectDeclaration:
					case DotNetContextType.ThisMemberAccess:
					case DotNetContextType.TryCastType:
					case DotNetContextType.TypeOfType: {
						// If there is no project resolver or items, quit right now
						if ((projectResolver == null) || (this.Items == null)) {
							switch (this.Type) {
								case DotNetContextType.IsTypeOfType:
								case DotNetContextType.NewObjectDeclaration:
								case DotNetContextType.TryCastType:
								case DotNetContextType.TypeOfType:
									// Allow type with no items
									break;
								default:
									// Clear context
									this.Type = DotNetContextType.None;
									break;
							}

							#if DEBUG && DEBUG_RESOLVE
							Trace.WriteLine("ResolveForCode-End: " + this);
							#endif
							return;
						}

						// Initialize variables
						int itemIndex = 0;
						StringBuilder fullTypeName = new StringBuilder();
						bool isStaticReference = false;
						bool typeFound = false;
						IDomType type = null;
						bool contextMustBeType = false;
						switch (this.Type) {
							case DotNetContextType.IsTypeOfType:
							case DotNetContextType.TryCastType:
							case DotNetContextType.TypeOfType:
								contextMustBeType = true;
								break;
						}

						// Convert "base" and "this" references to the appropriate type and flag as instance
						switch (this.Type) {
							case DotNetContextType.BaseMemberAccess:
								// If there is no type reference available, quit
								if (this.TypeDeclarationNode == null) {
									this.Type = DotNetContextType.None;
									#if DEBUG && DEBUG_RESOLVE
									Trace.WriteLine("ResolveForCode-End: " + this);
									#endif
									return;
								}

								// Get the base type reference
								IDomTypeReference baseTypeReference = this.TypeDeclarationNode.BaseType;
								if (baseTypeReference == null)
									baseTypeReference = new TypeReference("System.Object", TextRange.Deleted);

								// Look for indexer references...
								IDomType baseType = baseTypeReference.Resolve(projectResolver);
								if ((baseType != null) && (this.Items[itemIndex].IndexerParameterCounts != null)) {
									// Hard case... look for indexers
									this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, baseType, itemIndex, false, false);
									IDomMember member = this.Items[itemIndex].ResolvedInfo as IDomMember;
									if ((member != null) && (member.ReturnType != null)) {
										this.Items[itemIndex].Type = DotNetContextItemType.Member;
										baseType = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, baseType);
									}
									else
										baseType = null;
								}
								else {
									// Easy case, just return the type
									this.Items[itemIndex].ResolvedInfo = baseType;
								}

								// If there is no resolved base type, quit
								if (baseType == null) {
									this.Type = DotNetContextType.None;
									#if DEBUG && DEBUG_RESOLVE
									Trace.WriteLine("ResolveForCode-End: " + this);
									#endif
									return;
								}

								// We are looking at an instance type
								fullTypeName.Append(baseType.FullName);
								type = baseType;
								typeFound = true;
								itemIndex++;
								break;
							case DotNetContextType.ThisMemberAccess:
								// Look for indexer references...
								if ((this.TypeDeclarationNode != null) && (this.Items[itemIndex].IndexerParameterCounts != null)) {
									// Hard case... look for indexers
									this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, this.TypeDeclarationNode, itemIndex, false, false);
									IDomMember member = this.Items[itemIndex].ResolvedInfo as IDomMember;
									if ((member != null) && (member.ReturnType != null)) {
										this.Items[itemIndex].Type = DotNetContextItemType.Member;
										this.TypeDeclarationNode = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, this.TypeDeclarationNode);
									}
									else
										this.TypeDeclarationNode = null;
								}
								else {
									// Easy case, just return the type
									this.Items[itemIndex].ResolvedInfo = this.TypeDeclarationNode;
								}

								// If there is no type reference available, quit
								if (this.TypeDeclarationNode == null) {
									this.Type = DotNetContextType.None;
									#if DEBUG && DEBUG_RESOLVE
									Trace.WriteLine("ResolveForCode-End: " + this);
									#endif
									return;
								}

								// We are looking at an instance type
								fullTypeName.Append(this.TypeDeclarationNode.FullName);
								type = this.TypeDeclarationNode;
								typeFound = true;
								itemIndex++;
								break;
						}

						for ( ; itemIndex < this.Items.Length; itemIndex++) {
							if (!typeFound) {
								// Append the item text
								if (fullTypeName.Length > 0)
									fullTypeName.Append(".");
								fullTypeName.Append(this.Items[itemIndex].Text);

								// If this is the first item, check for a string literal
								if ((itemIndex == 0) && (this.Items[itemIndex].Type == DotNetContextItemType.StringLiteral)) {
									// Get the string type
									type = projectResolver.GetNativeType("System.String");

									// If a type was found...
									if (type != null) {
										this.Items[itemIndex].ResolvedInfo = type;
										typeFound = true;
										continue;
									}
								}

								// Check for a local variable, constant, parameter...
								if ((!contextMustBeType) && (itemIndex == 0) && (this.TypeDeclarationNode != null)) {
									ArrayList variableDeclarators = new ArrayList();
									IAstNode node = CSharpContext.GetVariables(this, variableDeclarators);
									type = null;

									// Loop through the results 
									foreach (AstNode declarator in variableDeclarators) {
										if (CSharpContext.GetVariableName(declarator) == fullTypeName.ToString()) {
											if (declarator is VariableDeclarator) {
												VariableDeclarator variableDeclarator = (VariableDeclarator)declarator;
												type = projectResolver.ConstructAndResolveFromSelf(this.GetVariableDeclaratorReturnType(variableDeclarator, this.TypeDeclarationNode));
												if (type != null)
													this.Items[itemIndex].Type = (variableDeclarator.IsConstant ? DotNetContextItemType.Constant : DotNetContextItemType.Variable);
											}
											else if (declarator is ParameterDeclaration) {
												ParameterDeclaration parameter = (ParameterDeclaration)declarator;

												// Get the type reference to resolve
												IDomTypeReference parameterType = parameter.ParameterType;
												if (parameter.ParentNode is LambdaExpression)
													parameterType = ExpressionResolver.ResolveLambdaExpressionParameter(this, parameter);

												// Try and resolve the type
												type = projectResolver.ConstructAndResolveFromSelf(parameterType);
												if (type != null)
													this.Items[itemIndex].Type = DotNetContextItemType.Parameter;
											}

											if (type != null)
												break;
										}
									}

									// If a type was found...
									if (type != null) {
										// Look for indexer references...
										if (this.Items[itemIndex].IndexerParameterCounts != null) {
											// Hard case... look for indexers
											this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, type, itemIndex, true, false);
											if (this.Items[itemIndex].ResolvedInfo is IDomType) {
												// Add an array item
												this.Items[itemIndex].Type = DotNetContextItemType.ArrayItem;
												type = (IDomType)this.Items[itemIndex].ResolvedInfo;
											}
											else if ((this.Items[itemIndex].ResolvedInfo is IDomMember) && (((IDomMember)this.Items[itemIndex].ResolvedInfo).ReturnType != null)) {
												this.Items[itemIndex].Type = DotNetContextItemType.Member;

												// Insert a new type item to split out the indexer from its parent type
												this.PrefixWithTypeItem(itemIndex++, type);

												type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, type);
											}
											else {
												this.Type = DotNetContextType.None;
												#if DEBUG && DEBUG_RESOLVE
												Trace.WriteLine("ResolveForCode-End: " + this);
												#endif
												return;
											}
										}
										else {
											// Easy case, just return the type
											this.Items[itemIndex].ResolvedInfo = type;
										}

										typeFound = true;
										continue;
									}
								}
							
								// If this is the first item, check for members 
								if ((!contextMustBeType) && (itemIndex == 0) && (this.TypeDeclarationNode != null)) {
									// Try to locate a matching member (always check for static members)
									type = this.TypeDeclarationNode;
									IDomMember member = projectResolver.GetMember(type, type, this.Items[itemIndex].Text, 
										DomBindingFlags.ExcludeIndexers | DomBindingFlags.Static | (isStaticReference ? DomBindingFlags.Static : DomBindingFlags.Instance) |
										this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
									if ((member == null) && (!isStaticReference)) {
										// Check for an extension
										member = projectResolver.GetExtensionMethod(type, importedNamespaces, type, this.Items[itemIndex].Text, 
											DomBindingFlags.Instance | this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes);
									}

									if (member != null) {
										// Ensure that constructors only match when desired
										if ((this.Type == DotNetContextType.NewObjectDeclaration) == (member.MemberType == DomMemberType.Constructor)) {
											// Flag the item as a valid member
											typeFound = true;
											this.Items[itemIndex].Type = DotNetContextItemType.Member;
											this.Items[itemIndex].ResolvedInfo = member;
											isStaticReference = false;

											// Add a type item at the start of the items list
											this.PrefixWithTypeItem(itemIndex++, type);

											// Transition to another type
											if (member.ReturnType != null) {
												// Get the member's return type
												type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, type);

												// Look for indexer references...
												if ((type != null) && (this.Items[itemIndex].IndexerParameterCounts != null)) {
													// Hard case... look for indexers
													this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, type, itemIndex, true, false);
													if (this.Items[itemIndex].ResolvedInfo is IDomType) {
														this.Items[itemIndex].Type = DotNetContextItemType.ArrayItem;
														type = (IDomType)this.Items[itemIndex].ResolvedInfo;
													}
													else if ((this.Items[itemIndex].ResolvedInfo is IDomMember) && (((IDomMember)this.Items[itemIndex].ResolvedInfo).ReturnType != null)) {
														this.Items[itemIndex].Type = DotNetContextItemType.Member;

														// Insert a new type item to split out the indexer from its parent type
														this.PrefixWithTypeItem(itemIndex++, type);

														type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, type);
													}
													else {
														this.Type = DotNetContextType.None;
														#if DEBUG && DEBUG_RESOLVE
														Trace.WriteLine("ResolveForCode-End: " + this);
														#endif
														return;
													}
												}
											}
											else
												type = null;
											continue;
										}
									}

									// Loop up parent declaring type chain to look for static members
									IDomType declaringType = this.TypeDeclarationNode.DeclaringType as IDomType;
									while (declaringType != null) {
										// Try to locate a matching static member in a parent declaring type
										member = projectResolver.GetMember(type, declaringType, this.Items[itemIndex].Text, 
											DomBindingFlags.ExcludeIndexers | DomBindingFlags.Static | 
											this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
										if (member != null) {
											// Flag the item as a valid member
											typeFound = true;
											this.Items[itemIndex].Type = DotNetContextItemType.Member;
											this.Items[itemIndex].ResolvedInfo = member;
											isStaticReference = true;

											// Transition to another type
											type = null;
											if (member.ReturnType != null) {
												// Get the member's return type
												type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, declaringType);
											}
											break;
										}

										// Recurse up
										declaringType = declaringType.DeclaringType as IDomType;
									}
									// If there is still a declaring type defined, we must have found a member so quit
									if (declaringType != null)
										continue;
								}

								// If there are no indexer parameters, it may be a type or namespace
								if (this.Items[itemIndex].IndexerParameterCounts == null) {
									// Get the generic argument count
									int genericArgumentCount = 0;
									if ((this.Items[itemIndex].GenericTypeArguments != null) && (this.Items[itemIndex].GenericTypeArguments.Length > 0))
										genericArgumentCount = this.Items[itemIndex].GenericTypeArguments.Length;

									// Check for a type
									type = projectResolver.GetType(this, fullTypeName.ToString() + (genericArgumentCount > 0 ?  "`" + genericArgumentCount : String.Empty), DomBindingFlags.Default);
									if (type != null) {
										// Construct if necessary
										if ((genericArgumentCount > 0) && (type.IsGenericTypeDefinition)) {
											ICollection declaredGenericTypeArguments = type.GenericTypeArguments;
											if ((declaredGenericTypeArguments != null) && (genericArgumentCount == declaredGenericTypeArguments.Count))
												type = new ConstructedGenericType(type, this.Items[itemIndex].GenericTypeArguments);
										}

										this.Items[itemIndex].Type = DotNetContextItemType.Type;
										this.Items[itemIndex].ResolvedInfo = type;
										typeFound = true;
										isStaticReference = (this.Type != DotNetContextType.NewObjectDeclaration);
										continue;
									}

									// If this is the first item, check for a namespace alias
									if (itemIndex == 0) {
										if (namespaceAliases.ContainsKey(this.Items[itemIndex].Text)) {
											// Need to pull out the last identifier of the full type name and replace it with the aliased name
											fullTypeName.Remove(fullTypeName.Length - this.Items[itemIndex].Text.Length, this.Items[itemIndex].Text.Length);
											fullTypeName.Append(namespaceAliases[this.Items[itemIndex].Text]);

											this.Items[itemIndex].Type = DotNetContextItemType.NamespaceAlias;
											this.Items[itemIndex].ResolvedInfo = fullTypeName.ToString();
											isStaticReference = true;
											continue;
										}
									}

									// If there is a namespace with the name, flag the item as a namespace
									if (projectResolver.HasNamespace(fullTypeName.ToString())) {
										this.Items[itemIndex].Type = DotNetContextItemType.Namespace;
										this.Items[itemIndex].ResolvedInfo = fullTypeName.ToString();
										isStaticReference = true;
										continue;
									}

									// Check imported namespaces to see if there is a child namespace with the name
									if (importedNamespaces != null) {
										bool childNamespaceFound = false;
										foreach (string importedNamespace in importedNamespaces) {
											if (projectResolver.HasNamespace(importedNamespace + "." + fullTypeName)) {
												this.Items[itemIndex].Type = DotNetContextItemType.Namespace;
												this.Items[itemIndex].ResolvedInfo = importedNamespace + "." + fullTypeName;
												isStaticReference = true;
												childNamespaceFound = true;
												break;
											}
										}
										if (childNamespaceFound)
											continue;
									}
								}
							}
							else if (contextMustBeType) {
								// If a type has been found and this context cannot be a member, so return a bad context
								this.Type = DotNetContextType.None;
								#if DEBUG && DEBUG_RESOLVE
								Trace.WriteLine("ResolveForCode-End: " + this);
								#endif
								return;
							}
							else if (type != null) {  // A type has been found already
								// If the type is a delegate...
								if (type.Type == DomTypeType.Delegate) {
									// Recurse into the Invoke method for a return type
									IDomMember invokeMember = type.GetMember(new IDomType[] { type }, "Invoke", DomBindingFlags.Instance | DomBindingFlags.Public);
									if ((invokeMember != null) && (invokeMember.ReturnType != null)) {
										// Insert an item for the implicit Invoke
										DotNetContextItem newItem = new DotNetContextItem(DotNetContextItemType.Member,
											new TextRange(this.Items[itemIndex].TextRange.StartOffset), "Invoke");
										newItem.ResolvedInfo = invokeMember;
										this.InsertItems(itemIndex, new DotNetContextItem[] { newItem });

										// Resolve the member return type
										type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, type);
										continue;
									}
								}
								
								// Try to locate a matching member
								IDomMember member = projectResolver.GetMember(this.TypeDeclarationNode, type, this.Items[itemIndex].Text, 
									DomBindingFlags.ExcludeIndexers | (isStaticReference ? DomBindingFlags.Static : DomBindingFlags.Instance) | 
									this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
								if ((member == null) && (!isStaticReference)) {
									// Check for an extension
									member = projectResolver.GetExtensionMethod(this.TypeDeclarationNode, importedNamespaces, type, this.Items[itemIndex].Text, 
										DomBindingFlags.Instance | this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes);
								}

								if (member != null) {
									// If the member appears after a construction (new StringBuilder().) then mark the context as a member context
									if (this.Type == DotNetContextType.NewObjectDeclaration)
										this.Type = DotNetContextType.NamespaceTypeOrMember;

									// Flag the item as a valid member
									this.Items[itemIndex].Type = DotNetContextItemType.Member;
									this.Items[itemIndex].ResolvedInfo = member;
									isStaticReference = false;
									
									// Transition to another type
									if (member.ReturnType != null) {
										// Get the member's return type
										type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, type);

										// Look for indexer references...
										if ((type != null) && (this.Items[itemIndex].IndexerParameterCounts != null)) {
											// Hard case... look for indexers
											this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, type, itemIndex, true, false);
											if (this.Items[itemIndex].ResolvedInfo is IDomType) {
												this.Items[itemIndex].Type = DotNetContextItemType.ArrayItem;
												type = (IDomType)this.Items[itemIndex].ResolvedInfo;
											}
											else if ((this.Items[itemIndex].ResolvedInfo is IDomMember) && (((IDomMember)this.Items[itemIndex].ResolvedInfo).ReturnType != null)) {
												this.Items[itemIndex].Type = DotNetContextItemType.Member;

												// Insert a new type item to split out the indexer from its parent type
												this.PrefixWithTypeItem(itemIndex++, type);

												type = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, type);
											}
											else {
												this.Type = DotNetContextType.None;
												#if DEBUG && DEBUG_RESOLVE
												Trace.WriteLine("ResolveForCode-End: " + this);
												#endif
												return;
											}
										}
									}
									else
										type = null;
									continue;
								}

								// Append the item text
								if (fullTypeName.Length > 0)
									fullTypeName.Append(".");
								fullTypeName.Append(this.Items[itemIndex].Text);

								// If there are no indexer parameters, it may be a nested type...
								if (this.Items[itemIndex].IndexerParameterCounts == null) {
									// Try and locate a nested type
									type = projectResolver.GetType(this, fullTypeName.ToString(), DomBindingFlags.Default);
									if (type != null) {
										this.Items[itemIndex].Type = DotNetContextItemType.Type;
										this.Items[itemIndex].ResolvedInfo = type;
										isStaticReference = (this.Type != DotNetContextType.NewObjectDeclaration);
										continue;
									}
								}
							}

							// Quit since no valid resolution was made
							this.Type = DotNetContextType.None;
							#if DEBUG && DEBUG_RESOLVE
							Trace.WriteLine("ResolveForCode-End: " + this);
							#endif
							return;
						}
						#if DEBUG && DEBUG_RESOLVE
						Trace.WriteLine("ResolveForCode-End: " + this);
						#endif
						return;
					}
					case DotNetContextType.BaseAccess:
						// Ensure that a valid type declaration is passed back
						if (this.TypeDeclarationNode != null) {
							IDomTypeReference baseTypeReference = this.TypeDeclarationNode.BaseType;
							if (baseTypeReference == null)
								baseTypeReference = new TypeReference("System.Object", TextRange.Deleted);

							IDomType baseType = baseTypeReference.Resolve(projectResolver);
							if (baseType != null) {
								// This context type always has one item
								if (this.Items[0].IndexerParameterCounts != null) {
									// Hard case... look for indexers
									this.Items[0].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, baseType, 0, false, false);
									if (this.Items[0].ResolvedInfo != null) {
										this.Items[0].Type = DotNetContextItemType.Member;
										this.Type = DotNetContextType.BaseMemberAccess;
									}
									else
										this.Type = DotNetContextType.None;
								}
								else {
									// Easy case, just return the type
									this.Items[0].ResolvedInfo = baseType;
								}

								#if DEBUG && DEBUG_RESOLVE
								Trace.WriteLine("ResolveForCode-End: " + this);
								#endif
								return;
							}
						}
						break;
					case DotNetContextType.NativeType: {
						// Try and load the native type
						IDomType nativeType = null;
						if (projectResolver != null)
							nativeType = projectResolver.GetNativeType("System." + this.Items[1].Text);

						// If there is a native type loaded, use it for resolution
						if (nativeType != null) {
							// This context type always has two items and the first is a namespace
							this.Items[0].ResolvedInfo = this.Items[0].Text;
							this.Items[1].ResolvedInfo = nativeType;

							#if DEBUG && DEBUG_RESOLVE
							Trace.WriteLine("ResolveForCode-End: " + this);
							#endif
							return;
						}
						else
							this.Type = DotNetContextType.None;									
						break;
					}
					case DotNetContextType.StringLiteral:
						// Try and resolve to a string type

						// This context type always has one item
						if (projectResolver != null) {
							if (this.Items[0].IndexerParameterCounts != null) {
								// Hard case... look for indexers
								this.Items[0].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, projectResolver.GetNativeType("System.String"), 0, false, false);
								if (this.Items[0].ResolvedInfo != null) {
									this.Items[0].Type = DotNetContextItemType.Member;
									this.Type = DotNetContextType.NamespaceTypeOrMember;
								}
								else
									this.Type = DotNetContextType.None;
							}
							else {
								// This context type always has one item
								this.Items[0].ResolvedInfo = projectResolver.GetNativeType("System.String");
								if (this.Items[0].ResolvedInfo == null)
									this.Type = DotNetContextType.None;									
							}
						}
						else
							this.Type = DotNetContextType.None;

						#if DEBUG && DEBUG_RESOLVE
						Trace.WriteLine("ResolveForCode-End: " + this);
						#endif
						return;
					case DotNetContextType.ThisAccess:
						// Ensure that a valid type declaration is passed back
						if (this.TypeDeclarationNode != null) {
							// This context type always has one item
							if (this.Items[0].IndexerParameterCounts != null) {
								// Hard case... look for indexers
								this.Items[0].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, this.TypeDeclarationNode, 0, false, false);
								if (this.Items[0].ResolvedInfo != null) {
									this.Items[0].Type = DotNetContextItemType.Member;
									this.Type = DotNetContextType.ThisMemberAccess;
								}
								else
									this.Type = DotNetContextType.None;
							}
							else {
								// Easy case, just return the type
								this.Items[0].ResolvedInfo = this.TypeDeclarationNode;
							}

							#if DEBUG && DEBUG_RESOLVE
							Trace.WriteLine("ResolveForCode-End: " + this);
							#endif
							return;
						}
						break;
					case DotNetContextType.UsingDeclaration:
						// Ensure that each item is a namespace
						if (this.Items != null) {
							StringBuilder fullTypeName = new StringBuilder();
							foreach (DotNetContextItem item in this.Items) {
								// Append the namespace name
								if (fullTypeName.Length > 0)
									fullTypeName.Append(".");
								fullTypeName.Append(item.Text);

								item.Type = DotNetContextItemType.Namespace;
								item.ResolvedInfo = fullTypeName.ToString();
							}
						}

						#if DEBUG && DEBUG_RESOLVE
						Trace.WriteLine("ResolveForCode-End: " + this);
						#endif
						return;
					default:
						break;
				}
			}

			// Nullify the context
			this.Type = DotNetContextType.None;
			#if DEBUG && DEBUG_RESOLVE
			Trace.WriteLine("ResolveForCode-End: " + this);
			#endif
		}
		
		/// <summary>
		/// Resolves the arguments in the <see cref="DotNetContextItem.ArgumentsText"/>.
		/// </summary>
		/// <param name="item">The <see cref="DotNetContextItem"/> to examine.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		internal override void ResolveArguments(DotNetContextItem item, IDomType contextType) {
			// If the arguments need to be resolved...
			if (item.ResolvedArguments == null) {
				// If the unresolved expression arguments need to be parsed...
				if ((item.UnresolvedArguments == null) && (item.ArgumentsText != null)) {
					// Quit if there is no language available
					CSharpSyntaxLanguage language = this.Language as CSharpSyntaxLanguage;
					if (language == null)
						return;

					// Create a semantic parser
					ITextBufferReader reader = new StringTextBufferReader(item.ArgumentsText, 0, 0);
					MergableLexicalParserManager manager = new MergableLexicalParserManager(reader, language, "expression");
					CSharpRecursiveDescentLexicalParser lexicalParser = new CSharpRecursiveDescentLexicalParser(language, manager);
					lexicalParser.InitializeTokens();
					CSharpSemanticParser semanticParser = new CSharpSemanticParser(lexicalParser);

					// Get the AST of arguments
					IAstNodeList argumentList = semanticParser.ParseArgumentList();
					if ((argumentList != null) && (argumentList.Count > 0)) {
						// Create an empty array of expressions for the results
						item.UnresolvedArguments = new Expression[argumentList.Count];

						// Loop through each expression and store it
						for (int index = 0; index < item.UnresolvedArguments.Length; index++)
							item.UnresolvedArguments[index] = argumentList[index] as Expression;
					}
				}

				// If there are unresolved arguments to examine...
				if (item.UnresolvedArguments != null) {
					// Create an empty array of type references for the results
					item.ResolvedArguments = new IDomTypeReference[item.UnresolvedArguments.Length];

					// Loop through each expression and try to resolve it to a type reference
					for (int index = 0; index < item.ResolvedArguments.Length; index++) {
						Expression expression = item.UnresolvedArguments[index];
						if (expression != null) 
							item.ResolvedArguments[index] = ExpressionResolver.Resolve(this, contextType, expression);				
					}
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="CSharpContext"/> that describes the context at the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <returns>The <see cref="CSharpContext"/> that describes the context at the specified offset.</returns>
		public static CSharpContext GetContextAtOffset(Document document, int offset, CompilationUnit compilationUnit, DotNetProjectResolver projectResolver) {
			IToken token = document.Tokens.GetTokenAtOffset(offset);

			if (CSharpContext.IsDocumentationComment(token)) {
				CSharpContext context = CSharpContext.GetContextForDocumentationComment(document, offset);
				context.ResolveForCode(document, compilationUnit, projectResolver);
				return context;
			}
			else if (token.LexicalStateID == CSharpLexicalStateID.Default) {
				CSharpContext context = CSharpContext.GetContextForCode(document, offset);
				context.ResolveForCode(document, compilationUnit, projectResolver);
				return context;
			}
			else
				return new CSharpContext(null, document.Language, offset);
		}

		/// <summary>
		/// Gets the <see cref="CSharpContext"/> that describes the context before the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <param name="forParameterInfo">Whether the context if for parameter info.</param>
		/// <returns>The <see cref="CSharpContext"/> that describes the context at the specified offset.</returns>
		public static CSharpContext GetContextBeforeOffset(Document document, int offset, CompilationUnit compilationUnit, DotNetProjectResolver projectResolver, bool forParameterInfo) {
			// Move into the previous token or stay put if already in the middle of a token
			TextStream stream = document.GetTextStream(offset);
			if (offset == stream.Token.StartOffset)
				stream.ReadTokenReverse();
			else
				stream.Offset = stream.Token.StartOffset;

			// Determine the target offset
			int targetOffset = -1;

			if (CSharpContext.IsDocumentationComment(stream.Token)) {
				// In a documentation comment
				CSharpContext context = CSharpContext.GetContextForDocumentationComment(document, stream.Offset);
				string tagText = String.Empty;
				if (stream.Token.ID == CSharpTokenID.DocumentationCommentTag) {
					// Get the tag name and ensure it is valid
					bool startsWithCaret = (stream.Character == '<');
					context.InitializationTextRange = new TextRange(stream.Offset + (startsWithCaret ? 1 : 0), offset);
					if (context.InitializationTextRange.Length > 0)
						tagText = document.GetSubstring(context.InitializationTextRange);
					bool validTagName = true;
					foreach (char ch in tagText) {
						if (!Char.IsLetter(ch)) {
							validTagName = false;
							break;
						}
					}
					if (!validTagName)
						return new CSharpContext(null, document.Language, offset);
				}

				// Resolve the context
				context.ResolveForCode(document, compilationUnit, projectResolver);
				return context;
			}
			else if (stream.Token.LexicalStateID == CSharpLexicalStateID.Default) {
				// In the default state, make a code-based context

				// Quit if in certain tokens
				switch (stream.Token.ID) {
					case CSharpTokenID.CharacterLiteral:
					case CSharpTokenID.MultiLineComment:
					case CSharpTokenID.SingleLineComment:
					case CSharpTokenID.StringLiteral:
					case CSharpTokenID.VerbatimStringLiteral:
						return new CSharpContext(null, document.Language, offset);
				}

				// If the token is an identifier or keyword, store its range and use it for initializing a list
				TextRange initializationTextRange = TextRange.Deleted;
				if (stream.Token.ID == CSharpTokenID.Identifier) {
					initializationTextRange = stream.Token.TextRange;
					stream.ReadTokenReverse();
				}
				else if ((stream.Token.ID >= CSharpTokenID.KeywordStart) && (stream.Token.ID <= CSharpTokenID.KeywordEnd)) {
					initializationTextRange = stream.Token.TextRange;
				}

				// Loop and look for a dot
				bool isFirstToken = stream.IsAtDocumentStart;
				while ((!isFirstToken) || (!stream.IsAtDocumentStart)) {
					isFirstToken = stream.IsAtDocumentStart;

					if (stream.Token.ID == CSharpTokenID.Dot) {
						// Store the offset before the dot as the target offset and quit the loop
						targetOffset = stream.Offset - 1;
						break;
					}
					else if ((stream.Token.ID == CSharpTokenID.New) || (stream.Token.ID == CSharpTokenID.Using)) {
						// Store the using start offset as the target offset and quit the loop
						targetOffset = stream.Token.StartOffset;
						break;
					}
					else if ((forParameterInfo) && ((stream.Token.ID == CSharpTokenID.OpenParenthesis) || (stream.Token.ID == CSharpTokenID.OpenSquareBrace))) {
						// Store the offset before the parenthesis as the target offset and quit the loop
						targetOffset = stream.Offset - 1;
						break;
					}
					else if ((stream.Token.ID == CSharpTokenID.Whitespace) || (stream.Token.ID == CSharpTokenID.LineTerminator)) {
						// Skip over whitespace
						stream.ReadTokenReverse();
					}
					else
						break;				
				}

				// If a target was found, get the context
				CSharpContext context = new CSharpContext(null, document.Language, offset, DotNetContextType.AnyCode, null);
				if (targetOffset >= 0) 
					context = CSharpContext.GetContextForCode(document, targetOffset);
				else if (stream.Token != null) {
					switch (stream.Token.ID) {
						case CSharpTokenID.As:
							context.Type = DotNetContextType.TryCastType;
							break;
						case CSharpTokenID.Is:
							context.Type = DotNetContextType.IsTypeOfType;
							break;
						case CSharpTokenID.OpenParenthesis:
							if ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID == CSharpTokenID.TypeOf))
								context.Type = DotNetContextType.IsTypeOfType;
							break;
					}
				}
				context.InitializationTextRange = initializationTextRange;
				context.ResolveForCode(document, compilationUnit, projectResolver);

				// See if a target delegate needs an Invoke item added
				if (context.TargetItem != null) {
					// If the target is a delegate...
					IDomType type = context.TargetItem.ResolvedInfo as IDomType;
					if (type == null) {
						IDomMember member = context.TargetItem.ResolvedInfo as IDomMember;
						if ((member != null) && (member.ReturnType != null))
							type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);
					}
					if ((type != null) && (type.Type == DomTypeType.Delegate)) {
						// Determine whether we need to insert the Invoke method into the items
						bool insertInvokeMethod = forParameterInfo;
						if ((!insertInvokeMethod) && (context.TargetItem.TextRange.EndOffset > 0)) {
							// Look for an open parenthesis to indicate invocation of the delegate
							stream.Offset = context.TargetItem.TextRange.EndOffset;
							while (stream.Offset < offset) {
								if (stream.ReadCharacter() == '(') {
									insertInvokeMethod = true;
									break;
								}
							}
						}

						if (insertInvokeMethod) {
							// Get the Invoke method on the delegate and add that to the items
							IDomMember member = type.GetMember(null, "Invoke", DomBindingFlags.Public | DomBindingFlags.Instance);
							if (member != null) {
								DotNetContextItem item = new DotNetContextItem(DotNetContextItemType.Member, new TextRange(context.TargetItem.TextRange.EndOffset), "Invoke");
								item.ResolvedInfo = member;
								context.InsertItems(context.Items.Length, new DotNetContextItem[] { item });
							}
						}
					}
				}

				// If for parameter info, find the valid parenthesis/brace text range
				if ((forParameterInfo) && (context.TargetItem != null)) {
					bool validParameterInfo = false;
					switch (document[targetOffset + 1]) {
						case '(':
							validParameterInfo = ((context.TargetItem.Type == DotNetContextItemType.Member) && (context.TargetItem.ResolvedInfo is IDomMember));
							if ((!validParameterInfo) && (context.TargetItem.ResolvedInfo is IDomType)) {
								validParameterInfo = (context.Type == DotNetContextType.NewObjectDeclaration) || 
									(((IDomType)context.TargetItem.ResolvedInfo).Type == DomTypeType.Delegate);
							}
							break;
						case '[':
							validParameterInfo = ((context.TargetItem.Type == DotNetContextItemType.Member) && (context.TargetItem.ResolvedInfo is IDomMember)) ||
								(context.TargetItem.ResolvedInfo is IDomType);
							break;
					}

					if (validParameterInfo) {
						stream.Offset = targetOffset + 2;
						bool exitLoop = false;
						while (!stream.IsAtDocumentEnd) {
							switch (stream.Token.ID) {
								case CSharpTokenID.Addition:
								case CSharpTokenID.Base:
								case CSharpTokenID.BitwiseAnd:
								case CSharpTokenID.BitwiseOr:
								case CSharpTokenID.Bool:
								case CSharpTokenID.Byte:
								case CSharpTokenID.Char:
								case CSharpTokenID.CharacterLiteral:
								case CSharpTokenID.Comma:
								case CSharpTokenID.ConditionalAnd:
								case CSharpTokenID.ConditionalOr:
								case CSharpTokenID.Decimal:
								case CSharpTokenID.DecimalIntegerLiteral:
								case CSharpTokenID.Decrement:
								case CSharpTokenID.Division:
								case CSharpTokenID.Dot:
								case CSharpTokenID.Double:
								case CSharpTokenID.Dynamic:
								case CSharpTokenID.Equality:
								case CSharpTokenID.ExclusiveOr:
								case CSharpTokenID.False:
								case CSharpTokenID.Float:
								case CSharpTokenID.GreaterThan:
								case CSharpTokenID.HexadecimalIntegerLiteral:
								case CSharpTokenID.Identifier:
								case CSharpTokenID.Increment:
								case CSharpTokenID.Inequality:
								case CSharpTokenID.Int:
								case CSharpTokenID.Is:
								case CSharpTokenID.LeftShift:
								case CSharpTokenID.LessThan:
								case CSharpTokenID.LineTerminator:
								case CSharpTokenID.Long:
								case CSharpTokenID.Modulus:
								case CSharpTokenID.MultiLineComment:
								case CSharpTokenID.Multiplication:
								case CSharpTokenID.Negation:
								case CSharpTokenID.New:
								case CSharpTokenID.Null:
								case CSharpTokenID.NullCoalescing:
								case CSharpTokenID.Object:
								case CSharpTokenID.OnesComplement:
								case CSharpTokenID.Out:
								case CSharpTokenID.Params:
								case CSharpTokenID.PointerDereference:
								case CSharpTokenID.QuestionMark:
								case CSharpTokenID.RealLiteral:
								case CSharpTokenID.Ref:
								case CSharpTokenID.SByte:
								case CSharpTokenID.Short:
								case CSharpTokenID.SingleLineComment:
								case CSharpTokenID.SizeOf:
								case CSharpTokenID.StackAlloc:
								case CSharpTokenID.StringLiteral:
								case CSharpTokenID.Subtraction:
								case CSharpTokenID.This:
								case CSharpTokenID.True:
								case CSharpTokenID.TypeOf:
								case CSharpTokenID.UInt:
								case CSharpTokenID.ULong:
								case CSharpTokenID.UShort:
								case CSharpTokenID.VerbatimStringLiteral:
								case CSharpTokenID.Whitespace:
									// Ignore
									break;
								case CSharpTokenID.OpenCurlyBrace:
								case CSharpTokenID.OpenParenthesis:
								case CSharpTokenID.OpenSquareBrace:
									// Skip braces
									stream.GoToNextMatchingToken(stream.Token);
									break;
								default:
									// Quit 
									exitLoop = true;
									break;
							}
							if (exitLoop)
								break;
							stream.ReadToken();
						}

						// Set the parameter text range
						context.ParameterTextRange = new TextRange(targetOffset + 2, stream.Offset + 1);
					}
				}

				return context;
			}
			else
				return new CSharpContext(null, document.Language, offset);
		}
		
		/// <summary>
		/// Gets whether the language is case sensitive.
		/// </summary>
		/// <value>
		/// <c>true</c> if the language is case sensitive; otherwise, <c>false</c>.
		/// </value>
		public override bool IsLanguageCaseSensitive { 
			get {
				return true;
			}
		}

	}
}
 