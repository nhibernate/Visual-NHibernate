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

namespace ActiproSoftware.SyntaxEditor.Addons.VB {
	
	/// <summary>
	/// Represents the <c>Visual Basic</c> language context for a specific offset.
	/// </summary>
	public class VBContext : DotNetContext {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>VBContext</c> class.
		/// </summary>
		/// <param name="parentContext">The <see cref="DotNetContext"/>, if any, that created this context.</param>
		/// <param name="language">The <see cref="SyntaxLanguage"/> that created the context.</param>
		/// <param name="targetOffset">The target offset.</param>
		internal VBContext(DotNetContext parentContext, SyntaxLanguage language, int targetOffset) : 
			this(parentContext, language, targetOffset, DotNetContextType.None, null) { }
	
		/// <summary>
		/// Initializes a new instance of the <c>VBContext</c> class.
		/// </summary>
		/// <param name="parentContext">The <see cref="DotNetContext"/>, if any, that created this context.</param>
		/// <param name="language">The <see cref="SyntaxLanguage"/> that created the context.</param>
		/// <param name="targetOffset">The target offset.</param>
		/// <param name="type">The <see cref="DotNetContextType"/> that describes the type of context.</param>
		/// <param name="items">The <see cref="ArrayList"/> of context items.</param>
		internal VBContext(DotNetContext parentContext, SyntaxLanguage language, int targetOffset, DotNetContextType type, ArrayList items) : 
			base(parentContext, language, targetOffset, type, items) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="VBContext"/> that describes the context at the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <returns>The <see cref="VBContext"/> that describes the context at the specified offset.</returns>
		private static VBContext GetContextForCode(Document document, int offset) {
			DotNetContextType contextType = DotNetContextType.NamespaceTypeOrMember;

			// Move to the start of the current token
			TextStream stream = document.GetTextStream(offset);
			int targetOffset = stream.Token.StartOffset;
			stream.Offset = targetOffset;

			// 10/29/2010 - If getting context for an identifier, move forward if there are generic arguments specified after it
			//   and this is a method call (31E-144D7E88-8521)
			bool exitLoop = false;
			if (stream.Token.ID == VBTokenID.Identifier) {
				// Skip past the identifier
				int nestingLevel = 1;
				stream.ReadToken();

				switch (stream.Token.ID) {
					case VBTokenID.Whitespace:
						// Advance past whitespace
						stream.ReadToken();
						break;
					case VBTokenID.OpenParenthesis: {
						// Ensure the next token is 'Of'
						stream.ReadToken();
						if (stream.Token.ID != VBTokenID.Of)
							break;

						// Skip past any generic arguments
						stream.ReadToken();
						while (!stream.IsAtDocumentEnd) {
							switch (stream.Token.ID) {
								case VBTokenID.Comma:
								case VBTokenID.Dot:
								case VBTokenID.Identifier:
								case VBTokenID.Whitespace:
									// Allow
									break;
								case VBTokenID.OpenParenthesis:
									nestingLevel++;

									// Ensure the next token is 'Of'
									stream.ReadToken();
									if (stream.Token.ID != VBTokenID.Of)
										exitLoop = true;
									break;
								case VBTokenID.CloseParenthesis:
									if (--nestingLevel == 0) {
										// Store offset
										int closeParenOffset = stream.Offset;

										// Skip past ')' and ensure the next character is a '(', meaning a generic method call
										stream.ReadToken();

										// Skip whitespace
										while ((!stream.IsAtDocumentEnd) && (stream.Token.ID == VBTokenID.Whitespace))
											stream.ReadToken();

										if ((!stream.IsAtDocumentEnd) && (stream.Token.ID == VBTokenID.OpenParenthesis)) {
											// Is a generic method call, jump back to ')'
											stream.Offset = closeParenOffset;
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
					case VBTokenID.CloseParenthesis: {
						// Count the indexer parameters
						currentIndexerParameterCount = 0;
						int argumentsEndOffset = stream.Offset;
						while ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID != VBTokenID.OpenParenthesis)) {
							switch (stream.Token.ID) {
								case VBTokenID.CloseParenthesis:
									stream.GoToPreviousMatchingToken(stream.Token);
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
								case VBTokenID.Comma:
									if (currentIndexerParameterCount > 0)
										currentIndexerParameterCount++;
									break;
								case VBTokenID.Of:
									// If this is a generic parameter specification...
									if ((!stream.IsAtDocumentStart) && (stream.PeekTokenReverse().ID == VBTokenID.OpenParenthesis)) {
										// Move past the 'Of'
										int genericTypeParameterStartOffset = stream.Offset;
										stream.ReadToken();

										// Scan the generic type arguments
										genericTypeArguments = VBContext.GetGenericTypeArguments(stream, argumentsEndOffset);

										// Restore the stream offset
										stream.Offset = genericTypeParameterStartOffset;
									}
									break;
								default:
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
							}
						}
						if (genericTypeArguments == null) {
							// Get the arguments text
							if (stream.Offset + 1 < argumentsEndOffset)
								argumentsText = document.GetSubstring(new TextRange(stream.Offset + 1, argumentsEndOffset));

							DotNetContextItem.AppendIndexerParameterCountLevel(ref indexerParameterCounts, currentIndexerParameterCount);
						}
						stream.ReadTokenReverse();
						break;
					}
					case VBTokenID.Whitespace:
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
				case VBTokenID.MyBase:
					// If the context is on a "MyBase" keyword, simply return that
					targetItem.Type = DotNetContextItemType.Base;
					items.Add(targetItem);
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.BaseAccess, items);
				case VBTokenID.As:
					// If the context is on a "As" keyword, simply return that
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.AsType, null);
				case VBTokenID.DecimalIntegerLiteral:
					// If the context is on a decimal number, simply return that
					targetItem.Type = DotNetContextItemType.Number;
					items.Add(targetItem);
					if ((!stream.IsAtDocumentStart) && (stream.PeekTokenReverse().ID == VBTokenID.Subtraction))
						targetItem.Text = "-" + targetItem.Text;
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.DecimalIntegerLiteral, items);
				case VBTokenID.HexadecimalIntegerLiteral:
					// If the context is on a hexadecimal number, simply return that
					targetItem.Type = DotNetContextItemType.Number;
					items.Add(targetItem);
					if ((!stream.IsAtDocumentStart) && (stream.PeekTokenReverse().ID == VBTokenID.Subtraction))
						targetItem.Text = "-" + targetItem.Text;
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.HexadecimalIntegerLiteral, items);
				case VBTokenID.Identifier:
					// Ensure the target item is added to the items 
					items.Add(targetItem);
					break;
				case VBTokenID.Imports:
					// If the context is on a "Imports" keyword, simply return that
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.UsingDeclaration, null);
				case VBTokenID.Me:
					// If the context is on a "Me" keyword, simply return that
					targetItem.Type = DotNetContextItemType.This;
					items.Add(targetItem);
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.ThisAccess, items);
				case VBTokenID.New:
					// If the context is on a "New" keyword, simply return that
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.NewObjectDeclaration, null);
				case VBTokenID.StringLiteral:
					// If the context is on a string literal, simply return that
					targetItem.Type = DotNetContextItemType.StringLiteral;
					items.Add(targetItem);
					return new VBContext(null, document.Language, targetOffset, DotNetContextType.StringLiteral, items);
				case VBTokenID.Whitespace:
				case VBTokenID.LineTerminator:
				case VBTokenID.Equality:  // 6/22/2010 - Added Equality (1F8-13B3D6B8-02ED)
				case VBTokenID.If:  // 9/21/2010 - Added If, While, Until (208-14262967-F6EA)
				case VBTokenID.While:
				case VBTokenID.Until:
					{
					// See if there is a dot after the start offset (might be a With expression reference)
					VBContext possibleWithContext = new VBContext(null, document.Language, targetOffset);
					stream.Offset = offset;
					char ch = stream.ReadCharacter();
					if (stream.Token.ID == VBTokenID.Dot) {
						// Scan backwards to ensure that this really could be for a With
						while (!stream.IsAtDocumentStart) {
							exitLoop = false;
							stream.ReadTokenReverse();
							switch (stream.Token.ID) {
								case VBTokenID.Whitespace:
									// Skip over
									break;
								case VBTokenID.DecimalIntegerLiteral:
								case VBTokenID.FloatingPointLiteral:
								case VBTokenID.HexadecimalIntegerLiteral:
								case VBTokenID.Identifier:
								case VBTokenID.MyBase:
								case VBTokenID.MyClass:
								case VBTokenID.OctalIntegerLiteral:
								case VBTokenID.StringLiteral:
									exitLoop = true;
									break;
								default:
									if (!VBToken.IsNativeType(stream.Token.ID))
										possibleWithContext.StartsWithDot = true;
									exitLoop = true;
									break;
							}
							if (exitLoop)
								break;
						}
					}
					return possibleWithContext;
				}
				case VBTokenID.With:
					// Exit the loop
					stream.GoToNextNonWhitespaceOrCommentToken();
					break;
				default:
					if (VBToken.IsNativeType(stream.Token.ID)) {
						// The token is on a native type, simply return that
						string[] identifiers = DotNetProjectResolver.GetTypeFullNameFromShortcut(DotNetLanguage.VB, targetItem.Text).Split(new char[] { '.' });
						items.Add(new DotNetContextItem(DotNetContextItemType.Namespace, 
							new TextRange(stream.Token.StartOffset, stream.Token.StartOffset + identifiers[0].Length), identifiers[0]));
						targetItem.Type = DotNetContextItemType.Type;
						targetItem.TextRange = new TextRange(stream.Token.EndOffset - identifiers[0].Length, stream.Token.EndOffset);
						targetItem.Text = identifiers[1];
						items.Add(targetItem);
						return new VBContext(null, document.Language, targetOffset, DotNetContextType.NativeType, items);
					}
					return new VBContext(null, document.Language, targetOffset);
			}

			exitLoop = false;
			bool lastWasDot = (stream.Token.ID == VBTokenID.Dot);
			while (!stream.IsAtDocumentStart) {
				IToken token = stream.ReadTokenReverse();

				// Continue if on whitespace or a comment
				if (((token.IsWhitespace) || (token.IsComment)) && (token.ID != VBTokenID.LineTerminator))
					continue;

				switch (token.ID) {
					case VBTokenID.As:
						// The context is a "As" object declaration
						contextType = DotNetContextType.AsType;
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.CloseParenthesis:
						// If the last character was not a dot, quit the loop
						if (!lastWasDot) {
							exitLoop = true;
							break;
						}

						// Count the indexer parameters
						currentIndexerParameterCount = 0;
						int argumentsEndOffset = stream.Offset;
						while ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID != VBTokenID.OpenParenthesis)) {
							switch (stream.Token.ID) {
								case VBTokenID.CloseParenthesis:
									stream.GoToPreviousMatchingToken(stream.Token);
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
								case VBTokenID.Comma:
									if (currentIndexerParameterCount > 0)
										currentIndexerParameterCount++;
									break;
								case VBTokenID.Of:
									// If this is a generic parameter specification...
									if ((!stream.IsAtDocumentStart) && (stream.PeekTokenReverse().ID == VBTokenID.OpenParenthesis)) {
										// Move past the 'Of'
										int genericTypeParameterStartOffset = stream.Offset;
										stream.ReadToken();

										// Scan the generic type arguments
										genericTypeArguments = VBContext.GetGenericTypeArguments(stream, argumentsEndOffset);

										// Restore the stream offset
										stream.Offset = genericTypeParameterStartOffset;
									}
									break;
								default:
									currentIndexerParameterCount = Math.Max(1, currentIndexerParameterCount);
									break;
							}
						}
						if (genericTypeArguments == null) {
							// Get the arguments text
							if (stream.Offset + 1 < argumentsEndOffset)
								argumentsText = document.GetSubstring(new TextRange(stream.Offset + 1, argumentsEndOffset));

							DotNetContextItem.AppendIndexerParameterCountLevel(ref indexerParameterCounts, currentIndexerParameterCount);
						}
						continue;
					case VBTokenID.Dot:
						if (lastWasDot)
							exitLoop = true;
						else {
							lastWasDot = true;
							continue;
						}
						break;
					case VBTokenID.Equality:
						// Back up and see if there is a "Imports Identifier =" before this
						stream.GoToPreviousNonWhitespaceOrCommentToken();
						stream.GoToPreviousNonWhitespaceOrCommentToken();
						if ((!stream.IsAtDocumentEnd) && (stream.PeekToken().ID == VBTokenID.Imports)) {
							// Over a namespace alias declaration
							contextType = DotNetContextType.UsingDeclaration;
							lastWasDot = false;  // 6/22/2010 - lastWasDot setting moved inside 'if' block (1F8-13B3D6B8-02ED)
						}
						exitLoop = true;
						break;
					case VBTokenID.Identifier:
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
						}
						lastWasDot = false;
						break;
					case VBTokenID.Imports:
						// Check to make sure we are not over the alias of an Imports statement
						stream.Offset = targetOffset;
						stream.GoToNextNonWhitespaceOrCommentToken();
						if (stream.PeekToken().ID != VBTokenID.Equality) {
							// The context is using declaration... identifiers must all be namespaces
							contextType = DotNetContextType.UsingDeclaration;
						}
						else {
							// Nullify the context... over a namespace alias declaration
							contextType = DotNetContextType.None;
						}
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.LineTerminator:
						// Exit the loop
						exitLoop = true;
						break;
					case VBTokenID.Me:
						// The context is a member access on "Me"
						contextType = DotNetContextType.ThisMemberAccess;
						items.Insert(0, new DotNetContextItem(DotNetContextItemType.This, token.TextRange, stream.Document.GetTokenText(token)));
						((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
						genericTypeArguments = null;
						((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
						indexerParameterCounts = null;
						((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
						argumentsText = null;
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.MyBase:
						// The context is a member access on "MyBase"
						contextType = DotNetContextType.BaseMemberAccess;
						items.Insert(0, new DotNetContextItem(DotNetContextItemType.Base, token.TextRange, stream.Document.GetTokenText(token)));
						((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
						genericTypeArguments = null;
						((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
						indexerParameterCounts = null;
						((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
						argumentsText = null;
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.New:
						// The context is a "New" object declaration
						contextType = DotNetContextType.NewObjectDeclaration;
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.Class:
					case VBTokenID.Delegate:
					case VBTokenID.Enum:
					case VBTokenID.Interface:
					case VBTokenID.Module:
					case VBTokenID.Namespace:
					case VBTokenID.Structure:
						// Ignore this context item and exit the loop... 
						//   this is a shortcut since we easily now know that it is a namespace, type, or member name
						contextType = DotNetContextType.None;
						lastWasDot = false;
						exitLoop = true;
						break;
// TODO:
/*
					case VBTokenID.As:
						// Exit the loop
						contextType = DotNetContextType.TryCastType;
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.Is:
						// Exit the loop
						contextType = DotNetContextType.IsTypeOfType;
						lastWasDot = false;
						exitLoop = true;
						break;
 */
					case VBTokenID.StringLiteral:
						// The token is a string literal, simply return that
						items.Insert(0, new DotNetContextItem(DotNetContextItemType.StringLiteral, token.TextRange, "System.String"));
						((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
						genericTypeArguments = null;
						((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
						indexerParameterCounts = null;
						((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
						argumentsText = null;

						// Exit the loop
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.OpenParenthesis:
// TODO:
/*
						// Check the previous token for "typeof"
						if (!stream.IsAtDocumentStart) {
							token = stream.ReadTokenReverse();
							if (token.ID == VBTokenID.TypeOf)
								contextType = DotNetContextType.TypeOfType;
						}
*/

						// Exit the loop
						lastWasDot = false;
						exitLoop = true;
						break;
					case VBTokenID.With:
						// Exit the loop
						exitLoop = true;
						break;
					default:
						if (VBToken.IsNativeType(token.ID)) {
							// The token is on a native type, simply return that
							string nativeTypeName = DotNetProjectResolver.GetTypeFullNameFromShortcut(DotNetLanguage.VB, VBTokenID.GetTokenKey(token.ID).ToString().ToLower());
							items.Insert(0, new DotNetContextItem(DotNetContextItemType.Type, token.TextRange, nativeTypeName));
							((DotNetContextItem)items[0]).GenericTypeArguments = genericTypeArguments;
							genericTypeArguments = null;
							((DotNetContextItem)items[0]).IndexerParameterCounts = indexerParameterCounts;
							indexerParameterCounts = null;
							((DotNetContextItem)items[0]).ArgumentsText = argumentsText;
							argumentsText = null;
						}

						// Exit the loop
						lastWasDot = false;
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}
			
			if ((contextType == DotNetContextType.NamespaceTypeOrMember) && (items.Count == 0))
				contextType = DotNetContextType.None;

			VBContext context = new VBContext(null, document.Language, targetOffset, contextType, items);
			context.StartsWithDot = lastWasDot;
			return context;
		}
		
		/// <summary>
		/// Gets the <see cref="VBContext"/> that describes the context at the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <returns>The <see cref="VBContext"/> that describes the context at the specified offset.</returns>
		private static VBContext GetContextForDocumentationComment(Document document, int offset) {
			// Move to the start of the current token
			TextStream stream = document.GetTextStream(offset);
			stream.GoToCurrentTokenStart();
			int targetOffset = stream.Offset;

			// Move backwards and look for an open tag
			Stack tagStack = new Stack();
			while (!stream.IsAtDocumentStart) {
				stream.GoToPreviousToken();
				switch (stream.Token.ID) {
					case VBTokenID.DocumentationCommentDelimiter:
					case VBTokenID.DocumentationCommentText:
					case VBTokenID.LineTerminator:
					case VBTokenID.Whitespace:
						// Ignore
						break;
					case VBTokenID.DocumentationCommentTag: {
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
									return new VBContext(null, document.Language, targetOffset, DotNetContextType.DocumentationCommentTag, items);
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
						return new VBContext(null, document.Language, targetOffset, DotNetContextType.DocumentationCommentTag, null);
				}
			}

			// Return that there is no parent tag
			return new VBContext(null, document.Language, targetOffset, DotNetContextType.DocumentationCommentTag, null);
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

				if ((stream.Token.ID == VBTokenID.Identifier) || (VBToken.IsNativeType(stream.Token.ID))) {
					// Add the name
					string typeName = stream.Document.GetTokenText(stream.Token);

					// Loop 
					while (stream.Offset < endOffset) {
						// Read the next token
						stream.ReadToken();

						// Skip over whitespace
						if (stream.Token.IsWhitespace)
							continue;

						// Exit loop on ( or comma
						if ((stream.Token.ID == VBTokenID.OpenParenthesis) || (stream.Token.ID == VBTokenID.Comma))
							break;

						// Exit loop on ) and skip over it
						if (stream.Token.ID == VBTokenID.CloseParenthesis) {
							stream.ReadToken();
							break;
						}

						// If a continuation, add the name
						if ((stream.Token.ID == VBTokenID.Dot) || (stream.Token.ID == VBTokenID.Identifier) || 
							((stream.Token.ID > VBTokenID.KeywordStart) && (stream.Token.ID < VBTokenID.KeywordEnd))) {
							typeName += stream.Document.GetTokenText(stream.Token);
						}
					}

					if (typeName.Length > 0) {
						// Get a child generic type argument array if appropriate
						IDomTypeReference[] genericTypeArguments = null;
						if (stream.Token.ID == VBTokenID.OpenParenthesis) {
							stream.ReadToken();  // Skip over (
							if (stream.Token.ID == VBTokenID.Of)
								stream.ReadToken();  // Skip over 'Of'
							genericTypeArguments = VBContext.GetGenericTypeArguments(stream, endOffset);
						}

						// Add the type reference
						TypeReference typeReference = new TypeReference(
							DotNetProjectResolver.GetTypeFullNameFromShortcut(DotNetLanguage.VB, typeName), TextRange.Deleted);
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
				if (stream.Token.ID != VBTokenID.Comma)
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
		/// Returns an <see cref="IDomMember"/> with the specified name, if one is found in a standard module.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <param name="namespaceName">The name of the namespace.</param>
		/// <returns>An <see cref="IDomMember"/> with the specified name, if one is found in a standard module.</returns>
		private IDomMember GetMemberInStandardModule(DotNetProjectResolver projectResolver, string namespaceName) {
			// Get the standard modules in the specified namespace
			ICollection standardModules = projectResolver.GetStandardModules(this.TypeDeclarationNode, namespaceName, DomBindingFlags.Default | DomBindingFlags.IgnoreCase);
			foreach (IDomType standardModule in standardModules) {
				// Try to locate a matching member in the standard module
				IDomMember member = projectResolver.GetMember(this.TypeDeclarationNode, standardModule, this.Items[0].Text, 
					DomBindingFlags.Static | 
					this.GetAdditionalBindingFlags(0) | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
				if (member != null)
					return member;
			}
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
				case VBTokenID.DocumentationCommentDelimiter:
				case VBTokenID.DocumentationCommentTag:
				case VBTokenID.DocumentationCommentText:
					return true;
				default:
					return (token.LexicalStateID == VBLexicalStateID.DocumentationComment);
			}
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
					VBSyntaxLanguage language = this.Language as VBSyntaxLanguage;
					if (language == null)
						return;

					// Create a semantic parser
					ITextBufferReader reader = new StringTextBufferReader(item.ArgumentsText, 0, 0);
					MergableLexicalParserManager manager = new MergableLexicalParserManager(reader, language, "expression");
					VBRecursiveDescentLexicalParser lexicalParser = new VBRecursiveDescentLexicalParser(language, manager);
					lexicalParser.InitializeTokens();
					VBSemanticParser semanticParser = new VBSemanticParser(lexicalParser);

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

					// If this context starts with a dot, look for a With statement
					if (this.StartsWithDot) {
						int originalItemCount = (this.Items != null ? this.Items.Length : 0);
						if (this.MemberDeclarationNode != null) {
							// Do a full resolution at the with statement and merge the items into this context
							WithStatement withStatement = this.ContainingNode as WithStatement;
							if (withStatement == null)
								withStatement = this.ContainingNode.FindAncestor(typeof(WithStatement)) as WithStatement;
							while ((withStatement != null) && ((withStatement.Expression is SimpleName) || (withStatement.Expression is InvocationExpression))) {
								// If the target offset is not within the With statement (otherwise could lead to duplicate items for this expression)...
								if (!withStatement.Expression.TextRange.Contains(this.TargetOffset)) {
									// Get the unresolved context of the With statement
									VBContext withContext = VBContext.GetContextForCode(document, withStatement.Expression.EndOffset - 1);

									// If items were returned in the context, insert them at the start of this context
									if ((withContext.Items != null) && (withContext.Items.Length > 0))
										this.InsertItems(0, withContext.Items);

									// Exit the loop if this With expression doesn't start with a dot
									if (!withContext.StartsWithDot)
										break;
								}

								// Recurse up
								withStatement = withStatement.FindAncestor(typeof(WithStatement)) as WithStatement;
							}
						}

						// If no with data was loaded...
						if ((this.Items != null ? this.Items.Length : 0) == originalItemCount) {
							// Nullify the context
							this.Type = DotNetContextType.None;
							#if DEBUG && DEBUG_RESOLVE
							Trace.WriteLine("ResolveForCode-End: " + this);
							#endif
							return;
						}

						// Flag this as a member
						this.Type = DotNetContextType.NamespaceTypeOrMember;
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
									case VBTokenID.DocumentationCommentDelimiter:
									case VBTokenID.DocumentationCommentText:
									case VBTokenID.LineTerminator:
									case VBTokenID.Whitespace:
									case VBTokenID.DocumentationCommentTag:
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
									case VBTokenID.DocumentationCommentDelimiter:
									case VBTokenID.Whitespace:
										// Ignore
										break;
									case VBTokenID.LineTerminator:
										// Add whitespace
										comment.Append(" ");
										break;
									case VBTokenID.DocumentationCommentTag:
									case VBTokenID.DocumentationCommentText:
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
					case DotNetContextType.AsType:
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
								case DotNetContextType.AsType:
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
						// TODO:
						/*
						switch (this.Type) {
							case DotNetContextType.IsTypeOfType:
							case DotNetContextType.TryCastType:
							case DotNetContextType.TypeOfType:
								contextMustBeType = true;
								break;
						}
						 */

						// Convert "MyBase" and "Me" references to the appropriate type and flag as instance
						switch (this.Type) {
							case DotNetContextType.NamespaceTypeOrMember:
								// If there are items...
								if ((this.Items != null) && (this.Items.Length > 0)) {
									// 5/26/2010 - Loop backwards to see if a pre-initialized item from a With block can be found (190-1388400C-5385)
									for (int index = this.Items.Length - 1; index >= 0; index--) {
										IDomType preInitializedType = this.Items[index].ResolvedInfo as IDomType;
										if (preInitializedType == null) {
											IDomMember preInitializedMember = this.Items[index].ResolvedInfo as IDomMember;
											if ((preInitializedMember != null) && (preInitializedMember.ReturnType != null))
												preInitializedType = preInitializedMember.ReturnType.Resolve(projectResolver);
										}
										if (preInitializedType != null) {
											// Initialize to the type
											fullTypeName.Append(preInitializedType.FullName);
											type = preInitializedType;
											typeFound = true;
											itemIndex = index + 1;
											break;
										}
									}
								}
								break;
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
									else {
										// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
										this.Items[itemIndex].ResolvedInfo = baseType;
									}
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
									this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, this.TypeDeclarationNode, itemIndex, false, true);  //1/24/2011 - allowDefaultProperties changed to true
									IDomMember member = this.Items[itemIndex].ResolvedInfo as IDomMember;
									if ((member != null) && (member.ReturnType != null)) {
										this.Items[itemIndex].Type = DotNetContextItemType.Member;
										this.TypeDeclarationNode = projectResolver.ConstructAndResolveMemberReturnType(this, itemIndex, this.TypeDeclarationNode);
									}
									else {
										// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
										this.Items[itemIndex].ResolvedInfo = this.TypeDeclarationNode;
									}
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
									IAstNode node = VBContext.GetVariables(this, variableDeclarators);
									type = null;

									// Loop through the results 
									foreach (AstNode declarator in variableDeclarators) {
										if (String.Compare(VBContext.GetVariableName(declarator), fullTypeName.ToString(), true) == 0) {
											if (declarator is VariableDeclarator) {
												VariableDeclarator variableDeclarator = (VariableDeclarator)declarator;
												type = projectResolver.ConstructAndResolveFromSelf(this.GetVariableDeclaratorReturnType(variableDeclarator, this.TypeDeclarationNode));
												if (type != null) {
													this.Items[itemIndex].Type = (variableDeclarator.IsConstant ? DotNetContextItemType.Constant : DotNetContextItemType.Variable);

													// 9/21/2010 - Quit the loop immediately if the variable is a constant, or the type was defined in AST
													//   or the type is not Object... this attempts to handle the case in VB where For Each can use variables
													//   declared above them but an implicit Object type is assigned in the For Each's variable declaration
													if ((variableDeclarator.IsConstant) || (variableDeclarator.ReturnType == null) || 
														(!variableDeclarator.ReturnType.TextRange.IsDeleted) || (type.FullName != "System.Object"))
														break;
												}
											}
											else if (declarator is ParameterDeclaration) {
												ParameterDeclaration parameter = (ParameterDeclaration)declarator;

												// Get the type reference to resolve
												IDomTypeReference parameterType = parameter.ParameterType;
												if (parameter.ParentNode is LambdaExpression)
													parameterType = ExpressionResolver.ResolveLambdaExpressionParameter(this, parameter);

												// Try and resolve the type
												type = projectResolver.ConstructAndResolveFromSelf(parameterType);
												if (type != null) {
													this.Items[itemIndex].Type = DotNetContextItemType.Parameter;
													break;
												}
											}
										}
									}

									// If a type was found...
									if (type != null) {
										// Look for indexer references...
										if (this.Items[itemIndex].IndexerParameterCounts != null) {
											// Hard case... look for indexers
											this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, type, itemIndex, true, true);  //1/24/2011 - allowDefaultProperties changed to true
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
												// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
												this.Items[itemIndex].ResolvedInfo = type;
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
										DomBindingFlags.Static | (isStaticReference ? DomBindingFlags.Static : DomBindingFlags.Instance) |
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
													this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, type, itemIndex, true, true);
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
														// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
														this.Items[itemIndex].ResolvedInfo = member;
													}
												}
												else if ((member.MemberType == DomMemberType.Property) && (member.Parameters != null) && (member.Parameters.Length > 0)) {
													// Is an indexer but no arguments were passed
													type = null;
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
											DomBindingFlags.Static | 
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

								// Get the generic argument count
								int genericArgumentCount = 0;
								if ((this.Items[itemIndex].GenericTypeArguments != null) && (this.Items[itemIndex].GenericTypeArguments.Length > 0))
									genericArgumentCount = this.Items[itemIndex].GenericTypeArguments.Length;

								// Check for a type
								type = projectResolver.GetType(this, fullTypeName.ToString() + (genericArgumentCount > 0 ?  "`" + genericArgumentCount : String.Empty), 
									DomBindingFlags.Default | DomBindingFlags.IgnoreCase);
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

								// If this is the first item...
								if (itemIndex == 0) {
									// Check for a namespace alias
									if (namespaceAliases.ContainsKey(this.Items[itemIndex].Text)) {
										// Need to pull out the last identifier of the full type name and replace it with the aliased name
										fullTypeName.Remove(fullTypeName.Length - this.Items[itemIndex].Text.Length, this.Items[itemIndex].Text.Length);
										fullTypeName.Append(namespaceAliases[this.Items[itemIndex].Text]);

										this.Items[itemIndex].Type = DotNetContextItemType.NamespaceAlias;
										this.Items[itemIndex].ResolvedInfo = fullTypeName.ToString();
										isStaticReference = true;
										continue;
									}

									// Check for a standard module member
									IDomMember member = this.GetMemberInStandardModule(projectResolver, String.Empty);
									if (member == null) {
										foreach (string namespaceName in this.ImportedNamespaces) {
											member = this.GetMemberInStandardModule(projectResolver, namespaceName);
											if (member != null)
												break;
										}
									}
									if (member != null) {
										// Flag the item as a valid member
										this.Items[itemIndex].Type = DotNetContextItemType.Member;
										this.Items[itemIndex].ResolvedInfo = member;
										isStaticReference = true;
										
										// Transition to another type
										type = null;
										if (member.ReturnType != null) {
											// Get the member's return type
											type = member.ReturnType.Resolve(projectResolver);
										}
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
									(isStaticReference ? DomBindingFlags.Static : DomBindingFlags.Instance) | 
									this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
								if ((member == null) && (!isStaticReference)) {
									// Check for an extension
									member = projectResolver.GetExtensionMethod(this.TypeDeclarationNode, importedNamespaces, type, this.Items[itemIndex].Text, 
										DomBindingFlags.Instance | this.GetAdditionalBindingFlags(itemIndex) | DomBindingFlags.AllAccessTypes);
								}

								if (member != null) {
									// If the member appears after a construction (New StringBuilder().) then mark the context as a member context
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
											this.Items[itemIndex].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, type, itemIndex, true, true);
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
												// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
												this.Items[itemIndex].ResolvedInfo = member;
											}
										}
										else if ((member.MemberType == DomMemberType.Property) && (member.Parameters != null) && (member.Parameters.Length > 0)) {
											// Is an indexer but no arguments were passed
											type = null;
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

								// Try and locate a nested type
								type = projectResolver.GetType(this, fullTypeName.ToString(), DomBindingFlags.IgnoreCase);
								if (type != null) {
									this.Items[itemIndex].Type = DotNetContextItemType.Type;
									this.Items[itemIndex].ResolvedInfo = type;
									isStaticReference = (this.Type != DotNetContextType.NewObjectDeclaration);
									continue;
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
									else {
										// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
										this.Items[0].ResolvedInfo = baseType;
									}
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
								this.Items[0].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, projectResolver.GetNativeType("System.String"), 0, false, true);  //1/24/2011 - allowDefaultProperties changed to true
								if (this.Items[0].ResolvedInfo != null) {
									this.Items[0].Type = DotNetContextItemType.Member;
									this.Type = DotNetContextType.NamespaceTypeOrMember;
								}
								else {
									// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
									this.Items[0].ResolvedInfo = projectResolver.GetNativeType("System.String");
									if (this.Items[0].ResolvedInfo == null)
										this.Type = DotNetContextType.None;									
								}
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
								this.Items[0].ResolvedInfo = this.ResolveToArrayOrIndexer(projectResolver, this.TypeDeclarationNode, 0, false, true);  //1/24/2011 - allowDefaultProperties changed to true
								if (this.Items[0].ResolvedInfo != null) {
									this.Items[0].Type = DotNetContextItemType.Member;
									this.Type = DotNetContextType.ThisMemberAccess;
								}
								else {
									// In VB, if an indexer match fails (since parens can mean indexer or parameters), continue on
									this.Items[0].ResolvedInfo = this.TypeDeclarationNode;
								}
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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="VBContext"/> that describes the context at the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <returns>The <see cref="VBContext"/> that describes the context at the specified offset.</returns>
		public static VBContext GetContextAtOffset(Document document, int offset, CompilationUnit compilationUnit, DotNetProjectResolver projectResolver) {
			IToken token = document.Tokens.GetTokenAtOffset(offset);

			if (VBContext.IsDocumentationComment(token)) {
				VBContext context = VBContext.GetContextForDocumentationComment(document, offset);
				context.ResolveForCode(document, compilationUnit, projectResolver);
				return context;
			}
			else if (token.LexicalStateID == VBLexicalStateID.Default) {
				VBContext context = VBContext.GetContextForCode(document, offset);
				context.ResolveForCode(document, compilationUnit, projectResolver);
				return context;
			}
			else
				return new VBContext(null, document.Language, offset);
		}

		/// <summary>
		/// Gets the <see cref="VBContext"/> that describes the context before the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset to examine.</param>
		/// <param name="compilationUnit">The <see cref="CompilationUnit"/> to examine.</param>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving type references.</param>
		/// <param name="forParameterInfo">Whether the context if for parameter info.</param>
		/// <returns>The <see cref="VBContext"/> that describes the context at the specified offset.</returns>
		public static VBContext GetContextBeforeOffset(Document document, int offset, CompilationUnit compilationUnit, DotNetProjectResolver projectResolver, bool forParameterInfo) {
			// Move into the previous token or stay put if already in the middle of a token
			TextStream stream = document.GetTextStream(offset);
			if (offset == stream.Token.StartOffset)
				stream.ReadTokenReverse();
			else
				stream.Offset = stream.Token.StartOffset;

			// Determine the target offset
			int targetOffset = -1;

			if (VBContext.IsDocumentationComment(stream.Token)) {
				// In a documentation comment
				VBContext context = VBContext.GetContextForDocumentationComment(document, stream.Offset);
				string tagText = String.Empty;
				if (stream.Token.ID == VBTokenID.DocumentationCommentTag) {
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
						return new VBContext(null, document.Language, offset);
				}

				// Resolve the context
				context.ResolveForCode(document, compilationUnit, projectResolver);
				return context;
			}
			else if (stream.Token.LexicalStateID == VBLexicalStateID.Default) {
				// In the default state, make a code-based context

				// Quit if in certain tokens
				switch (stream.Token.ID) {
					case VBTokenID.CharacterLiteral:
					case VBTokenID.DateLiteral:
					case VBTokenID.RemComment:
					case VBTokenID.SingleLineComment:
					case VBTokenID.StringLiteral:
						return new VBContext(null, document.Language, offset);
				}
				// If the token is an identifier, store its range and use it for initializing a list
				TextRange initializationTextRange = TextRange.Deleted;
				if (stream.Token.ID == VBTokenID.Identifier) {
					initializationTextRange = stream.Token.TextRange;
					stream.ReadTokenReverse();
				}
				else if ((stream.Token.ID >= VBTokenID.KeywordStart) && (stream.Token.ID <= VBTokenID.KeywordEnd)) {
					initializationTextRange = stream.Token.TextRange;
				}

				// Loop and look for a dot
				bool isFirstToken = stream.IsAtDocumentStart;
				while ((!isFirstToken) || (!stream.IsAtDocumentStart)) {
					isFirstToken = stream.IsAtDocumentStart;

					if (stream.Token.ID == VBTokenID.Dot) {
						// Store the offset before the dot as the target offset and quit the loop
						targetOffset = stream.Offset - 1;
						break;
					}
					else if ((stream.Token.ID == VBTokenID.As) || (stream.Token.ID == VBTokenID.New) || (stream.Token.ID == VBTokenID.Imports)) {
						// Store the Imports start offset as the target offset and quit the loop
						targetOffset = stream.Token.StartOffset;
						break;
					}
					else if ((forParameterInfo) && (stream.Token.ID == VBTokenID.OpenParenthesis)) {
						// Store the offset before the parenthesis as the target offset and quit the loop
						targetOffset = stream.Offset - 1;
						break;
					}
					else if (stream.Token.ID == VBTokenID.Whitespace) {
						// Skip over whitespace but quit the loop if on a line terminator (unlike C#)
						stream.ReadTokenReverse();
					}
					else
						break;				
				}

				// If a target was found, get the context
				VBContext context = new VBContext(null, document.Language, offset, DotNetContextType.AnyCode, null);
				if (targetOffset >= 0) 
					context = VBContext.GetContextForCode(document, targetOffset);
				// TODO:
				/*
				else if (stream.Token != null) {
					switch (stream.Token.ID) {
						case VBTokenID.As:
							context.Type = DotNetContextType.TryCastType;
							break;
						case VBTokenID.Is:
							context.Type = DotNetContextType.IsTypeOfType;
							break;
						case VBTokenID.OpenParenthesis:
							if ((!stream.IsAtDocumentStart) && (stream.ReadTokenReverse().ID == VBTokenID.TypeOf))
								context.Type = DotNetContextType.IsTypeOfType;
							break;
					}
				}
				 */
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
						if (!insertInvokeMethod) {
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
							validParameterInfo = (
								(context.TargetItem.Type == DotNetContextItemType.Member) && (context.TargetItem.ResolvedInfo is IDomMember) ||
								(context.Type == DotNetContextType.NewObjectDeclaration) && (context.TargetItem.ResolvedInfo is IDomType)
								);
							break;
					}

					if (validParameterInfo) {
						stream.Offset = targetOffset + 2;
						bool exitLoop = false;
						while (!stream.IsAtDocumentEnd) {
							switch (stream.Token.ID) {
								case VBTokenID.Addition:
								case VBTokenID.AddressOf:
								case VBTokenID.And:
								case VBTokenID.AndAlso:
								case VBTokenID.As:
								case VBTokenID.Boolean:
								case VBTokenID.ByRef:
								case VBTokenID.Byte:
								case VBTokenID.ByVal:
								case VBTokenID.CBool:
								case VBTokenID.CByte:
								case VBTokenID.CChar:
								case VBTokenID.CDate:
								case VBTokenID.CDbl:
								case VBTokenID.CDec:
								case VBTokenID.Char:
								case VBTokenID.CharacterLiteral:
								case VBTokenID.CInt:
								case VBTokenID.CLng:
								case VBTokenID.CObj:
								case VBTokenID.ColonEquals:
								case VBTokenID.Comma:
								case VBTokenID.CSByte:
								case VBTokenID.CShort:
								case VBTokenID.CSng:
								case VBTokenID.CStr:
								case VBTokenID.CType:
								case VBTokenID.CUInt:
								case VBTokenID.CULng:
								case VBTokenID.CUShort:
								case VBTokenID.Date:
								case VBTokenID.DateLiteral:
								case VBTokenID.Decimal:
								case VBTokenID.DecimalIntegerLiteral:
								case VBTokenID.DirectCast:
								case VBTokenID.Dot:
								case VBTokenID.Double:
								case VBTokenID.Equality:
								case VBTokenID.ExclamationPoint:
								case VBTokenID.Exponentiation:
								case VBTokenID.False:
								case VBTokenID.FloatingPointDivision:
								case VBTokenID.FloatingPointLiteral:
								case VBTokenID.GetTypeKeyword:
								case VBTokenID.Global:
								case VBTokenID.GreaterThan:
								case VBTokenID.HexadecimalIntegerLiteral:
								case VBTokenID.Identifier:
								case VBTokenID.IIf:
								case VBTokenID.Inequality:
								case VBTokenID.Integer:
								case VBTokenID.IntegerDivision:
								case VBTokenID.Is:
								case VBTokenID.IsFalse:
								case VBTokenID.IsNot:
								case VBTokenID.IsTrue:
								case VBTokenID.LeftShift:
								case VBTokenID.LessThan:
								case VBTokenID.Like:
								case VBTokenID.LineContinuation:
								case VBTokenID.Long:
								case VBTokenID.Me:
								case VBTokenID.Mid:
								case VBTokenID.Mod:
								case VBTokenID.Multiplication:
								case VBTokenID.MyBase:
								case VBTokenID.MyClass:
								case VBTokenID.New:
								case VBTokenID.Not:
								case VBTokenID.Nothing:
								case VBTokenID.Object:
								case VBTokenID.OctalIntegerLiteral:
								case VBTokenID.Of:
								case VBTokenID.Or:
								case VBTokenID.OrElse:
								case VBTokenID.RemComment:
								case VBTokenID.RightShift:
								case VBTokenID.SByte:
								case VBTokenID.Short:
								case VBTokenID.Single:
								case VBTokenID.SingleLineComment:
								case VBTokenID.String:
								case VBTokenID.StringConcatenation:
								case VBTokenID.StringLiteral:
								case VBTokenID.Subtraction:
								case VBTokenID.True:
								case VBTokenID.TypeOf:
								case VBTokenID.UInteger:
								case VBTokenID.ULong:
								case VBTokenID.UShort:
								case VBTokenID.Variant:
								case VBTokenID.Whitespace:
								case VBTokenID.Xor:
									// Ignore
									break;
								case VBTokenID.OpenCurlyBrace:
								case VBTokenID.OpenParenthesis:
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
				return new VBContext(null, document.Language, offset);
		}


		/// <summary>
		/// Gets whether the language is case sensitive.
		/// </summary>
		/// <value>
		/// <c>true</c> if the language is case sensitive; otherwise, <c>false</c>.
		/// </value>
		public override bool IsLanguageCaseSensitive { 
			get {
				return false;
			}
		}
	}
}
 