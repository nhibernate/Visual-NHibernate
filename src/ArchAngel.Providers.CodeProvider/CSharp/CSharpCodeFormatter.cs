using System;
using System.Collections.Generic;
using System.Text;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using UsingStatement = ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.UsingStatement;

namespace ArchAngel.Providers.CodeProvider
{
	///<summary>
	/// Used for formatting C# source code.
	///</summary>
	public class CSharpCodeFormatter
	{
		private readonly CSharpFormatSettings formatSettings;
		private readonly Controller controller;
		private readonly Document document;
		private readonly Dictionary<int, Comment> handledComments = new Dictionary<int, Comment>();
		private readonly SortedList<int, Region> unhandledRegions = new SortedList<int, Region>();
		private int indentLevel;

		/// <summary>
		/// Creates a new formatSettings that will use the given Document to access the original source code,
		/// and will start indents from the given level.
		/// </summary>
		/// <param name="document"></param>
		/// <param name="formatSettings">The formatSettings settings to use when formatting this document.</param>
		/// <param name="controller"></param>
		public CSharpCodeFormatter(Document document, CSharpFormatSettings formatSettings, Controller controller)
		{
			if (document == null) throw new ArgumentNullException("document");
			this.document = document;
			this.formatSettings = formatSettings;
			this.controller = controller;
		}

		public Dictionary<int, Comment> HandledComments
		{
			get { return handledComments; }
		}

		public SortedList<int, Region> UnhandledRegions
		{
			get { return unhandledRegions; }
		}

		private string Indent
		{
			get { return indentLevel == 0 ? "" : new string('\t', indentLevel); }
		}

		/// <summary>
		/// The formatSettings settings currently in use.
		/// </summary>
		public CSharpFormatSettings FormatSettings
		{
			get { return formatSettings; }
		}

		private void Reset()
		{
			indentLevel = 0;
		}

		///<summary>
		/// Formats the Statements in a method, and returns the formatted code as a string.
		///</summary>
		///<param name="statements">The statements in the method body.</param>
		///<param name="comments">The comments in the parent .</param>
		///<param name="startOffset">The start offset of the block we are processing.</param>
		///<param name="length">The length of the block we are processing.</param>
		///<returns>The formatted method body code.</returns>        
		public string ProcessBodyText(IAstNodeList statements, IAstNodeList comments, int startOffset, int length)
		{
			Reset();
			return ProcessBodyTextInternal(statements, comments, startOffset, length);
		}

		///<summary>
		/// Formats the Statements in a method, and returns the formatted code as a string.
		///</summary>
		///<param name="statements">The statements in the method body.</param>
		///<param name="comments">The comments in the parent .</param>
		///<param name="startOffset">The start offset of the block we are processing.</param>
		///<param name="length">The length of the block we are processing.</param>
		///<returns>The formatted method body code.</returns>        
		private string ProcessBodyTextInternal(IAstNodeList statements, IAstNodeList comments, int startOffset, int length)
		{
			return ProcessBodyTextInternal(statements, comments, startOffset, length, true);
		}

		///<summary>
		/// Formats the Statements in a method, and returns the formatted code as a string.
		///</summary>
		///<param name="statements">The statements in the method body.</param>
		///<param name="comments">The comments in the parent .</param>
		///<param name="startOffset">The start offset of the block we are processing.</param>
		///<param name="length">The length of the block we are processing.</param>
		///<param name="addBraces">If AddBraces is true, the statement block created will have braces surrounding it. </param>
		///<returns>The formatted method body code.</returns>        
		private string ProcessBodyTextInternal(IAstNodeList statements, IAstNodeList comments, int startOffset, int length,
											   bool addBraces)
		{
			StringBuilder sb = new StringBuilder(1000);
			if (statements.ParentNode.ParentNode is AccessorDeclaration && formatSettings.InlineSingleLineGettersAndSetters
				&& comments.Count == 0 && statements.Count < 2)
			{
				if (addBraces) sb.Append("{ ");
				if (statements.Count == 1)
				{
					sb.Append(ProcessStatement(statements[0] as Statement)).Append(" ");
				}
				if (addBraces) sb.Append("}");
				return sb.ToString();
			}

			if (addBraces) sb.AppendLine(Indent + "{");
			indentLevel++;

			SortedList<int, string> statementList = new SortedList<int, string>();

			foreach (IAstNode child in statements)
			{
				Statement statement = (Statement)child;

				if (statement is EmptyStatement && formatSettings.OmitEmptyStatements)
				{
					continue;
				}
				statementList.Add(child.StartOffset, ProcessStatement(statement));

				foreach (Comment c in statement.Comments)
				{
					if (statement.TextRange.Contains(c.StartOffset))
						continue;
					if (c.StartOffset < statement.ParentNode.StartOffset)
						continue;

					// We handle the other comments inside ProcessStatement
					handledComments[c.StartOffset] = c;

					string commentText = c.Text;
					if (formatSettings.CommentLinesAsCommentBlock)
					{
						commentText = "/* " + commentText.Replace("/*", "").Replace("*/", "").Replace("//", "").Trim() + " */";
					}

					statementList[c.StartOffset] = commentText;
				}
			}

			foreach (IAstNode child in comments)
			{
				if (child.StartOffset >= startOffset && child.StartOffset < startOffset + length)
				{
					string commentText = ((Comment)child).Text.Trim();
					if (formatSettings.CommentLinesAsCommentBlock)
					{
						commentText = "/* " + commentText.Replace("/*", "").Replace("*/", "").Replace("//", "").Trim() + " */";
					}

					statementList.Add(child.StartOffset, commentText);
				}
			}

			foreach (KeyValuePair<int, Region> r in unhandledRegions)
			{
				statementList.Add(r.Key, "#region " + r.Value.Name);
				statementList.Add(r.Value.EndOffset, "#endregion");
			}

			KeyValuePair<int, string>? prevLine = null;
			foreach (KeyValuePair<int, string> line in statementList)
			{
				bool sameLine = false;

				int lineBreaksBetween = FindPreviousBlankLines(line.Key);

				if (prevLine.HasValue && lineBreaksBetween == 0)
				{
					sameLine = true;
				}

				if (prevLine.HasValue && formatSettings.MaintainWhitespace == false)
					sb.AppendLine().Append(Indent);
				else if (sameLine)
					sb.Append(" ");
				else if (prevLine.HasValue) // If this is not the first item, add a new line.
				{
					for (int i = 0; i < lineBreaksBetween; i++)
						sb.AppendLine();

					sb.Append(Indent);
				}
				else
					sb.Append(Indent);

				sb.Append(line.Value.Trim());
				prevLine = line;
			}
			if (statementList.Count > 0)
				sb.AppendLine();
			indentLevel--;
			if (addBraces) sb.AppendLine(Indent + "}");

			return sb.ToString();
		}

		internal int FindPreviousBlankLines(int offset)
		{
			IToken token = document.Tokens.GetTokenAtOffset(offset);
			TokenStream tokens = document.GetTokenStream(token);
			if (tokens.Position == 0)
			{
				if (document.GetTokenText(tokens.Peek())[0] != document[0])
				{
					// There is a line break at the start of the file.
					return 1;
				}

				return 0;
			}
			if (tokens.Position == 1)
			{
				if (document.GetTokenText(tokens.PeekReverse()) == "\n")
					return 1;
				return 0;
			}
			if (tokens.Position < 2)
				return -1;

			if (document.GetTokenText(token) != "\n")
			{
				//	tokens.Seek(-1);
				token = tokens.ReadReverse();
			}
			int count = 0;

			while (tokens.Position > 0 && token != null && token.IsWhitespace)
			{
				if (document.GetTokenText(token) == "\n") count++;

				token = tokens.ReadReverse();
			}

			return count;
		}

		private string ProcessStatement(Statement node)
		{
			if (node is EmptyStatement)
			{
				return ";";
			}

			StringBuilder sb = new StringBuilder(100);

			if (node == null) throw new ArgumentNullException("node");

			switch (node.NodeType)
			{
				case DotNetNodeType.RegionPreProcessorDirective:

					break;
				case DotNetNodeType.BlockStatement:
					Process_Block_Statement(sb, (BlockStatement)node);
					break;
				case DotNetNodeType.BranchStatement:
					Process_Branch_Statement(node, sb);
					break;
				case DotNetNodeType.ForEachStatement:
					Process_ForEach_Statement(sb, (ForEachStatement)node);
					break;
				case DotNetNodeType.ForStatement:
					Process_For_Statement(sb, (ForStatement)node);
					break;
				case DotNetNodeType.IfStatement:
					Process_If_Statement(sb, (IfStatement)node);
					break;
				case DotNetNodeType.LocalVariableDeclaration:
					Process_Local_Variable_Declaration(sb, (LocalVariableDeclaration)node);
					sb.Append(";");
					break;
				case DotNetNodeType.StatementExpression:
					Process_Statement_Expression(sb, (StatementExpression)node);
					break;
				// working down from here.
				case DotNetNodeType.WhileStatement:
					Process_While_Statement(sb, (WhileStatement)node);
					break;
				case DotNetNodeType.ReturnStatement:
					Process_Return_Statement(sb, (ReturnStatement)node);
					break;
				case DotNetNodeType.SwitchStatement:
					Process_Switch_Statement(sb, (SwitchStatement)node);
					break;
				case DotNetNodeType.BreakStatement:
					Process_Break_Statement(sb, (BreakStatement)node);
					break;
				case DotNetNodeType.ContinueStatement:
					Process_Continue_Statement(sb, (ContinueStatement)node);
					break;
				case DotNetNodeType.DoStatement:
					Process_Do_Statement(sb, (DoStatement)node);
					break;
				case DotNetNodeType.TryStatement:
					Process_Try_Statement(sb, (TryStatement)node);
					break;
				case DotNetNodeType.ThrowStatement:
					Process_Throw_Statement(sb, (ThrowStatement)node);
					break;
				case DotNetNodeType.LockStatement:
					Process_Lock_Statement(sb, (LockStatement)node);
					break;
				case DotNetNodeType.CheckedStatement:
					Process_Checked_Statement(sb, (CheckedStatement)node);
					break;
				case DotNetNodeType.UncheckedStatement:
					Process_Unchecked_Statement(sb, (UncheckedStatement)node);
					break;
				case DotNetNodeType.GoToStatement:
					Process_Goto_Statement(sb, (GotoStatement)node);
					break;
				case DotNetNodeType.UnsafeStatement:
					Process_Unsafe_Statement(sb, (UnsafeStatement)node);
					break;
				case DotNetNodeType.FixedStatement:
					Process_Fixed_Statement(sb, (FixedStatement)node);
					break;
				case DotNetNodeType.LabeledStatement:
					Process_Labeled_Statement(sb, (LabeledStatement)node);
					break;
				case DotNetNodeType.YieldStatement:
					Process_Yield_Statement(sb, (YieldStatement)node);
					break;
				case DotNetNodeType.UsingStatement:
					Process_Using_Statement(sb, (UsingStatement)node);
					break;
				default:
					throw new ParserException("We do not currently handle " + node.GetType() + " statements");
			}

			return sb.ToString();
		}

		private void Process_Using_Statement(StringBuilder sb, UsingStatement statement)
		{
			sb.Append("using (");
			bool firstProcessed = false;
			foreach (IAstNode resAqNode in statement.ResourceAcquisitions)
			{
				if (firstProcessed) sb.Append(", ");
				if (resAqNode is LocalVariableDeclaration)
				{
					Process_Local_Variable_Declaration(sb, (LocalVariableDeclaration)resAqNode);
				}
				else if (resAqNode is Expression)
				{
					sb.Append(FormatExpression((Expression)resAqNode));
				}
				else
				{
					throw new ParserException("Not expecting node of type " + resAqNode.GetType() + " as part of the resource aquisition section of a UsingStatement.");
				}
				firstProcessed = true;
			}
			sb.AppendLine(")");
			sb.Append(ProcessStatement(statement.Statement));

		}

		private void Process_Yield_Statement(StringBuilder sb, YieldStatement statement)
		{
			sb.Append("yield ");
			if (statement.Expression == null)
			{
				sb.Append("break;");
			}
			else
			{
				sb.Append("return ").Append(FormatExpression(statement.Expression)).Append(";");
			}
		}

		private void Process_Labeled_Statement(StringBuilder sb, LabeledStatement statement)
		{
			sb.Append(FormatExpression(statement.Label)).Append(": ");
			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Fixed_Statement(StringBuilder sb, FixedStatement statement)
		{
			sb.Append("fixed(");
			bool typeAppended = false;

			foreach (IAstNode declNode in statement.Declarators)
			{
				VariableDeclarator decl = (VariableDeclarator)declNode;
				if (typeAppended == false)
				{
					sb.Append(FormatterUtility.FormatDataType(decl.ReturnType, document, controller)).Append(" ");
					typeAppended = true;
				}
				else
					sb.Append(", ");

				Process_Variable_Declarator(sb, decl);
			}

			sb.Append(")");

			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Unsafe_Statement(StringBuilder sb, UnsafeStatement statement)
		{
			sb.Append("unsafe");
			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Goto_Statement(StringBuilder sb, GotoStatement statement)
		{
			sb.Append("goto ");

			if (statement.Identifier != null)
			{
				sb.Append(statement.Identifier.Text).Append(";");
			}
			else if (statement.Expression == null)
			{
				sb.Append("default;");
			}
			else
			{
				sb.Append("case ").Append(FormatExpression(statement.Expression)).Append(";");
			}
		}

		private void Process_Unchecked_Statement(StringBuilder sb, UncheckedStatement statement)
		{
			sb.Append("unchecked");
			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Checked_Statement(StringBuilder sb, CheckedStatement statement)
		{
			sb.Append("checked");
			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Lock_Statement(StringBuilder sb, LockStatement statement)
		{
			sb.Append("lock(").Append(FormatExpression(statement.Expression)).Append(")");
			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Throw_Statement(StringBuilder sb, ThrowStatement statement)
		{
			sb.Append("throw");
			if (statement.Expression != null)
			{
				sb.Append(" ").Append(FormatExpression(statement.Expression));
			}
			sb.Append(";");
		}

		private void Process_Try_Statement(StringBuilder sb, TryStatement statement)
		{
			sb.Append("try");
			sb.Append(ProcessStatement(statement.TryBlock));
			Process_Catch_Clauses(sb, statement.CatchClauses);
		}

		private void Process_Catch_Clauses(StringBuilder sb, IAstNodeList clauses)
		{
			foreach (IAstNode catchNode in clauses)
			{
				CatchClause cat = (CatchClause)catchNode;
				sb.Append(Indent).Append("catch");

				if (cat.VariableDeclarator != null)
				{
					sb.Append("(");
					sb.Append(FormatterUtility.FormatDataType(cat.VariableDeclarator.ReturnType, document, controller));
					if (cat.VariableDeclarator.Name != null)
						sb.Append(" ").Append(cat.VariableDeclarator.Name.Text);
					sb.Append(")");
				}
				Process_Block_Statement(sb, cat.BlockStatement);
			}
		}

		private void Process_Do_Statement(StringBuilder sb, DoStatement statement)
		{
			sb.Append("do");
			sb.Append(ProcessStatement(statement.Statement));

			sb.Append(Indent).Append("while(").Append(FormatExpression(statement.Expression)).Append(");");
		}

		private void Process_Continue_Statement(StringBuilder sb, ContinueStatement o)
		{
			sb.Append("continue;");
		}

		private void Process_Break_Statement(StringBuilder sb, BreakStatement statement)
		{
			sb.Append("break;");
		}

		private void Process_Switch_Statement(StringBuilder sb, SwitchStatement statement)
		{
			sb.Append("switch(").Append(FormatExpression(statement.Expression)).Append(")");
			sb.Append(Environment.NewLine).Append(Indent).Append("{");
			indentLevel++;

			Process_Switch_Sections(sb, statement.Sections);

			indentLevel--;
			sb.Append(Environment.NewLine).Append(Indent).Append("}");
		}

		private void Process_Switch_Sections(StringBuilder sb, IAstNodeList sections)
		{
			foreach (IAstNode sectionNode in sections)
			{
				SwitchSection section = (SwitchSection)sectionNode;
				foreach (IAstNode labelNode in section.Labels)
				{
					SwitchLabel label = (SwitchLabel)labelNode;
					if (label.Expression == null)
					{
						sb.Append(Environment.NewLine).Append(Indent).Append("default:");
					}
					else
					{
						sb.Append(Environment.NewLine).Append(Indent).Append("case ");
						sb.Append(FormatExpression(label.Expression));
						sb.Append(":");
					}
				}
				sb.AppendLine();
				string textInternal = ProcessBodyTextInternal(section.Statements, section.Comments, section.StartOffset,
															  section.Length, false);
				textInternal = textInternal.TrimEnd(Environment.NewLine.ToCharArray());
				// ProcessBodyTextInternal puts an extra newline on the end that we don't want.
				sb.Append(textInternal);
			}
		}

		private void Process_Return_Statement(StringBuilder sb, ReturnStatement statement)
		{
			sb.Append("return");
			if (statement.Expression != null)
			{
				sb.Append(" ").Append(FormatExpression(statement.Expression));
			}
			sb.Append(";");
		}

		private void Process_While_Statement(StringBuilder sb, WhileStatement statement)
		{
			sb.Append("while(").Append(FormatExpression(statement.Expression)).Append(")");
			ProcessStatementBlock(sb, statement.Statement);
		}

		private void Process_For_Statement(StringBuilder sb, ForStatement statement)
		{
			sb.Append("for(");
			// Initialisers
			for (int i = 0; i < statement.Initializers.Count; i++)
			{
				if (statement.Initializers[i] is LocalVariableDeclaration)
					Process_Local_Variable_Declaration(sb, (LocalVariableDeclaration)statement.Initializers[i]);
				else if (statement.Initializers[i] is AssignmentExpression)
					Format_Assignment_Expression(sb, (AssignmentExpression)statement.Initializers[i]);
				else
					throw new ParserException("Not expecting type " + statement.Initializers[i].GetType() + " in for loop initialiser.");

				if (statement.Initializers.Count > 1 && i < statement.Initializers.Count - 1)
					sb.Append(", ");
			}
			sb.Append("; ");

			// Condition
			sb.Append(FormatExpression(statement.Condition));
			sb.Append("; ");

			// Iterators
			for (int i = 0; i < statement.Iterators.Count; i++)
			{
				sb.Append(FormatExpression(statement.Iterators[i] as Expression));
				if (statement.Iterators.Count > 1 && i < statement.Iterators.Count - 1)
					sb.Append(", ");
			}
			sb.Append(") ");

			sb.Append(ProcessStatement(statement.Statement));
		}


		private void Process_ForEach_Statement(StringBuilder sb, ForEachStatement statement)
		{
			sb.Append("foreach(");

			// Variable
			Process_Local_Variable_Declaration(sb, statement.VariableDeclaration as LocalVariableDeclaration);
			sb.Append(" in ");

			sb.Append(FormatExpression(statement.Expression));

			sb.Append(") ");

			sb.Append(ProcessStatement(statement.Statement));
		}

		private void Process_Branch_Statement(Statement node, StringBuilder sb)
		{
			if (node is ReturnStatement)
			{
				ReturnStatement statement = (ReturnStatement)node;
				sb.Append("return");
				if (statement.Expression != null)
				{
					sb.Append(" ");
					sb.Append(FormatExpression(statement.Expression));
				}
				sb.Append(";");

			}
			else
			{
				sb.Append(document.GetSubstring(node.TextRange));
			}
		}

		private void Process_Statement_Expression(StringBuilder sb, StatementExpression expression)
		{
			sb.Append(FormatExpression(expression.Expression));
			sb.Append(";");
		}

		private void Process_Local_Variable_Declaration(StringBuilder sb, LocalVariableDeclaration declaration)
		{
			if (declaration.Variables == null || declaration.Variables.Count == 0)
				return;

			if (declaration.IsConst)
				sb.Append("const ");

			VariableDeclarator firstVar = ((VariableDeclarator)declaration.Variables[0]);
			sb.Append(FormatterUtility.FormatDataType(firstVar, document, controller));
			sb.AppendFormat(" {0}", firstVar.Name.Text);
			if (firstVar.Initializer != null)
			{
				sb.Append(" = ");
				sb.Append(FormatExpression(firstVar.Initializer));
			}

			for (int i = 1; i < declaration.Variables.Count; i++)
			{
				sb.Append(", ");
				VariableDeclarator varDec = declaration.Variables[i] as VariableDeclarator;
				if (varDec == null)
					throw new ParserException("Not expecting node of type " + declaration.Variables[i].GetType() +
											  " in local variable declaration.");
				Process_Variable_Declarator(sb, varDec);
			}
		}

		private void Process_Variable_Declarator(StringBuilder sb, VariableDeclarator varDec)
		{
			sb.AppendFormat("{0}", varDec.Name.Text);
			if (varDec.Initializer != null)
			{
				sb.Append(" = ");
				sb.Append(FormatExpression(varDec.Initializer));
			}
		}
		///<summary>
		/// Pretty prints an expression if it can, otherwise throws a ParserException.
		///</summary>
		///<param name="exp">The expression to format.</param>
		///<returns>A string with the expression formatted according to the current formatting rules.</returns>
		public string FormatExpression(Expression exp)
		{
			if (exp == null) return "";

			if (exp is SimpleName)
			{
				SimpleName name = (SimpleName)exp;
				return name.Name;
			}

			if (exp is LiteralExpression)
			{
				return Format_Literal_Expression((LiteralExpression)exp);
			}

			StringBuilder sb = new StringBuilder(exp.Length);

			switch (exp.NodeType)
			{
				case DotNetNodeType.AnonymousMethodExpression:
					Format_Anonymous_Method_Expression(sb, (AnonymousMethodExpression)exp);
					break;
				case DotNetNodeType.ArgumentExpression:
					Format_Argument_Expression(sb, (ArgumentExpression)exp);
					break;
				case DotNetNodeType.AssignmentExpression:
					Format_Assignment_Expression(sb, (AssignmentExpression)exp);
					break;
				case DotNetNodeType.BaseAccess:
					sb.Append("base");
					break;
				case DotNetNodeType.BinaryExpression:
					Format_Binary_Expression(sb, (BinaryExpression)exp);
					break;
				case DotNetNodeType.CastExpression:
					Format_Cast_Expression(sb, (CastExpression)exp);
					break;
				case DotNetNodeType.CheckedExpression:
					Format_Checked_Expression(sb, (CheckedExpression)exp);
					break;
				case DotNetNodeType.ConditionalExpression:
					Format_Conditional_Expression(sb, (ConditionalExpression)exp);
					break;
				case DotNetNodeType.DefaultValueExpression:
					Format_Default_Value_Expression(sb, (DefaultValueExpression)exp);
					break;
				case DotNetNodeType.InvocationExpression:
					Format_Invocation_Expression(sb, (InvocationExpression)exp);
					break;
				case DotNetNodeType.IsTypeOfExpression:
					Format_IsTypeOf_Expression(sb, (IsTypeOfExpression)exp);
					break;
				case DotNetNodeType.LambdaExpression:
					Process_Lambda_Expression(sb, (LambdaExpression)exp);
					break;
				case DotNetNodeType.SimpleName:
				case DotNetNodeType.LiteralExpression:
					// Already handled. Should never get here.
					break;
				case DotNetNodeType.MemberAccess:
					Format_Member_Access_Expression(sb, (MemberAccess)exp);
					break;
				case DotNetNodeType.ObjectCollectionInitializerExpression:
					Format_Object_Collection_Initializer_Expression(sb, (ObjectCollectionInitializerExpression)exp);
					break;
				case DotNetNodeType.ObjectCreationExpression:
					Format_Object_Creation_Expression(sb, (ObjectCreationExpression)exp);
					break;
				case DotNetNodeType.ParenthesizedExpression:
					Format_Parenthesized_Expression(sb, (ParenthesizedExpression)exp);
					break;
				case DotNetNodeType.PointerMemberAccess:
					Format_Pointer_Member_Access_Expression(sb, (PointerMemberAccess)exp);
					break;
				case DotNetNodeType.SizeOfExpression:
					Format_SizeOf_Expression(sb, (SizeOfExpression)exp);
					break;
				case DotNetNodeType.StackAllocInitializer:
					Format_Stack_Alloc_Initializer_Expression(sb, (StackAllocInitializer)exp);
					break;
				case DotNetNodeType.ThisAccess:
					sb.Append("this");
					break;
				case DotNetNodeType.TryCastExpression:
					Format_TryCast_Expression(sb, (TryCastExpression)exp);
					break;
				case DotNetNodeType.TypeOfExpression:
					Format_TypeOf_Expression(sb, (TypeOfExpression)exp);
					break;
				case DotNetNodeType.TypeReferenceExpression:
					Format_Type_Reference_Expression(sb, (TypeReferenceExpression)exp);
					break;
				case DotNetNodeType.UnaryExpression:
					Format_Unary_Expression(sb, (UnaryExpression)exp);
					break;
				case DotNetNodeType.UncheckedExpression:
					Format_Unchecked_Expression(sb, (UncheckedExpression)exp);
					break;
				case DotNetNodeType.AggregateQueryOperator:
					goto default;
				case DotNetNodeType.DistinctQueryOperator:
					goto default;
				case DotNetNodeType.FromQueryOperator:
					goto default;
				case DotNetNodeType.GroupQueryOperator:
					goto default;
				case DotNetNodeType.JoinCondition:
					goto default;
				case DotNetNodeType.JoinQueryOperator:
					goto default;
				case DotNetNodeType.LetQueryOperator:
					goto default;
				case DotNetNodeType.OrderByQueryOperator:
					goto default;
				case DotNetNodeType.Ordering:
					goto default;
				case DotNetNodeType.QueryExpression:
					Format_Query_Expression(sb, (QueryExpression)exp);
					break;
				case DotNetNodeType.SelectQueryOperator:
					goto default;
				case DotNetNodeType.SkipQueryOperator:
					goto default;
				case DotNetNodeType.SkipWhileQueryOperator:
					goto default;
				case DotNetNodeType.TakeQueryOperator:
					goto default;
				case DotNetNodeType.TakeWhileQueryOperator:
					goto default;
				case DotNetNodeType.WhereQueryOperator:
					goto default;
				default:
					throw new ParserException("Not expecting node of type " + exp.NodeType + " in FormatExpression.");
			}

			return sb.ToString();
		}

		private string FormatQueryOperator(IAstNode node)
		{
			StringBuilder sb = new StringBuilder();

			if (node is FromQueryOperator)
			{
				FromQueryOperator op = (FromQueryOperator)node;

				foreach (CollectionRangeVariableDeclaration varDecl in op.CollectionRangeVariableDeclarations)
				{
					sb.Append("from ");

					// ReturnType is never null. If there wasn't one specified, it defaults to var, which is redundant.
					// better to just leave it out for the time being, until I talk to Actipro about the problem that
					// causes it.
					//if (varDecl.VariableDeclarator.ReturnType != null)
					//	sb.Append(FormatterUtility.FormatDataType(varDecl.VariableDeclarator.ReturnType, currentCode)).Append(" ");

					Format_Collection_Range_Variable_Declaration(sb, varDecl);
				}
			}
			else if (node is SelectQueryOperator)
			{
				SelectQueryOperator op = (SelectQueryOperator)node;
				sb.Append("select ");

				VariableDeclarator declarator = (VariableDeclarator)op.VariableDeclarators[0];
				sb.Append(FormatExpression(declarator.Initializer));
				if (declarator.Name != null)
				{
					sb.Append(" into ").Append(declarator.Name.Text);
				}
			}
			else if (node is LetQueryOperator)
			{
				LetQueryOperator op = (LetQueryOperator)node;
				sb.Append("let ");
				VariableDeclarator varDecl = (VariableDeclarator)op.VariableDeclarators[0];
				sb.Append(varDecl.Name.Text).Append(" = ");
				sb.Append(FormatExpression(varDecl.Initializer));
			}
			else if (node is WhereQueryOperator)
			{
				WhereQueryOperator op = (WhereQueryOperator)node;
				sb.Append("where ");
				sb.Append(FormatExpression(op.Condition));
			}
			else if (node is JoinQueryOperator)
			{
				JoinQueryOperator op = (JoinQueryOperator)node;
				sb.Append("join ");

				Format_Collection_Range_Variable_Declaration(sb, op.CollectionRangeVariableDeclaration);

				sb.Append(" on ");

				JoinCondition condition = (JoinCondition)op.Conditions[0];
				sb.Append(FormatExpression(condition.LeftConditionExpression));
				sb.Append(" equals ");
				sb.Append(FormatExpression(condition.RightConditionExpression));

				if (op.TargetExpressions != null && op.TargetExpressions.Count > 0)
				{
					sb.Append(" into ");
					Expression target = (Expression)op.TargetExpressions[0];
					sb.Append(FormatExpression(target));
				}
			}
			else if (node is OrderByQueryOperator)
			{
				OrderByQueryOperator op = (OrderByQueryOperator)node;
				sb.Append("orderby ");

				bool first = true;
				foreach (Ordering ordering in op.Orderings)
				{
					if (first == false)
						sb.Append(", ");
					sb.Append(FormatExpression(ordering.Expression));
					string queryText = document.GetSubstring(ordering.TextRange);
					if (queryText.EndsWith("descending") || queryText.EndsWith("ascending"))
						sb.Append(ordering.Direction == OrderingDirection.Ascending ? " ascending" : " descending");
					first = false;
				}
			}
			else if (node is GroupQueryOperator)
			{
				GroupQueryOperator op = (GroupQueryOperator)node;
				sb.Append("group ");

				// It is possible that this will break one day, as Actipro have chosen to
				// add the expression objects for these two expressions to VariableDeclarations for
				// some reason, event though Microsoft list the grammar for group clauses to be
				// "group" expression "by" expression, and the Actipro C# grammar reads them out as
				// expressions.
				VariableDeclarator groupDecl = (VariableDeclarator)op.Groupings[0];
				sb.Append(FormatExpression(groupDecl.Initializer));
				sb.Append(" by ");

				VariableDeclarator byDecl = (VariableDeclarator)op.GroupBys[0];
				sb.Append(FormatExpression(byDecl.Initializer));

				if (op.TargetExpressions.Count > 0)
				{
					sb.Append(" into ");
					VariableDeclarator targetDecl = (VariableDeclarator)op.TargetExpressions[0];
					sb.Append(FormatExpression(targetDecl.Initializer));
				}
			}

			return sb.ToString();
		}

		private void Format_Collection_Range_Variable_Declaration(StringBuilder sb, CollectionRangeVariableDeclaration varDecl)
		{
			sb.Append(varDecl.VariableDeclarator.Name.Text);

			sb.Append(" in ");

			sb.Append(FormatExpression(varDecl.Source));
		}

		private void Format_Query_Expression(StringBuilder sb, QueryExpression expression)
		{
			List<string> queryOperators = new List<string>();
			foreach (IAstNode operatorNode in expression.QueryOperators)
			{
				queryOperators.Add(FormatQueryOperator(operatorNode));
			}

			sb.Append(string.Join(" ", queryOperators.ToArray()));
		}

		private void Process_Lambda_Expression(StringBuilder sb, LambdaExpression expression)
		{
			StringBuilder paramString = new StringBuilder();
			bool explicitParamDecl = false;

			for (int i = 0; i < expression.Parameters.Count; i++)
			{
				ParameterDeclaration param = (ParameterDeclaration)expression.Parameters[i];

				if (i > 0)
					paramString.Append(", ");

				if (param.IsByReference)
				{
					paramString.Append("ref ");
				}
				else if (param.IsOutput)
				{
					paramString.Append("out ");
				}

				if (param.ParameterType != null)
				{
					paramString.Append(FormatterUtility.FormatDataType(param.ParameterType, document, controller));
					paramString.Append(" ");
					explicitParamDecl = true;
				}
				paramString.Append(param.Name);
			}

			if (explicitParamDecl)
			{
				sb.Append("(");
			}
			sb.Append(paramString.ToString());
			if (explicitParamDecl)
			{
				sb.Append(")");
			}

			sb.Append(" => ");

			Format_Lamba_Statement_Block(expression, sb);
		}

		private void Format_Lamba_Statement_Block(LambdaExpression expression, StringBuilder sb)
		{
			if (expression.Statement.NodeType == DotNetNodeType.StatementExpression)
			{
				sb.Append(FormatExpression(((StatementExpression)expression.Statement).Expression));
				return;
			}

			if (expression.Statement.NodeType == DotNetNodeType.BlockStatement)
			{
				BlockStatement statementBlock = (BlockStatement)expression.Statement;

				if (statementBlock.Statements.Count == 1)
				{
					sb.Append("{ ").Append(ProcessStatement(statementBlock.Statements[0] as Statement)).Append(" } ");
					return;
				}
			}

			indentLevel += 2;
			ProcessStatementBlock(sb, expression.Statement);
			indentLevel -= 2;
		}

		private void Format_Stack_Alloc_Initializer_Expression(StringBuilder sb, StackAllocInitializer exp)
		{
			sb.Append("stackalloc ").Append(FormatterUtility.FormatDataType(exp.TypeReference, document, controller))
				.Append("[").Append(FormatExpression(exp.Expression)).Append("]");
		}

		private void Format_SizeOf_Expression(StringBuilder sb, SizeOfExpression expression)
		{
			sb.Append("sizeof(").Append(FormatterUtility.FormatDataType(expression.TypeReference, document, controller)).Append(")");
		}

		private void Format_Pointer_Member_Access_Expression(StringBuilder sb, PointerMemberAccess exp)
		{
			sb.Append(FormatExpression(exp.Expression)).Append("->").Append(exp.MemberName.Text);
		}

		private void Format_Default_Value_Expression(StringBuilder sb, DefaultValueExpression expression)
		{
			sb.Append("default(").Append(FormatterUtility.FormatDataType(expression.ReturnType, document, controller)).Append(")");
		}

		private void Format_Checked_Expression(StringBuilder sb, CheckedExpression exp)
		{
			sb.Append("checked(").Append(FormatExpression(exp.Expression)).Append(")");
		}

		private void Format_Unchecked_Expression(StringBuilder sb, UncheckedExpression exp)
		{
			sb.Append("unchecked(").Append(FormatExpression(exp.Expression)).Append(")");
		}

		private void Format_Conditional_Expression(StringBuilder sb, ConditionalExpression exp)
		{
			sb.Append(FormatExpression(exp.TestExpression)).Append(" ? ")
				.Append(FormatExpression(exp.TrueExpression))
				.Append(" : ")
				.Append(FormatExpression(exp.FalseExpression));
		}

		private void Format_Type_Reference_Expression(StringBuilder sb, TypeReferenceExpression exp)
		{
			sb.Append(FormatterUtility.FormatDataType(exp.TypeReference, document, controller));
		}

		private void Format_IsTypeOf_Expression(StringBuilder sb, IsTypeOfExpression exp)
		{
			sb.Append(FormatExpression(exp.Expression)).Append(" is ").Append(
				FormatterUtility.GetDataTypeFromTypeReference(exp.TypeReference, document, controller).ToString());
		}

		private void Format_Parenthesized_Expression(StringBuilder sb, ParenthesizedExpression exp)
		{
			sb.Append("(").Append(FormatExpression(exp.Expression)).Append(")");
		}

		public static string Format_Literal_Expression(LiteralExpression literal)
		{
			switch (literal.LiteralType)
			{
				case LiteralType.True:
					return "true";
				case LiteralType.False:
					return "false";
				case LiteralType.Null:
					return "null";
				/*
			case LiteralType.DecimalInteger:
				break;
			case LiteralType.HexadecimalInteger:
				break;
			case LiteralType.OctalInteger:
				break;
			case LiteralType.Real:
				break;
			case LiteralType.Character:
				break;
			case LiteralType.String:
				break;
			case LiteralType.VerbatimString:
				break;
				 */
			}

			return literal.LiteralValue;
		}

		private void Format_TryCast_Expression(StringBuilder sb, TryCastExpression expression)
		{
			sb.Append(FormatExpression(expression.Expression)).Append(" as ").Append(
				FormatterUtility.FormatDataType(expression.ReturnType, document, controller));
		}

		private void Format_Argument_Expression(StringBuilder sb, ArgumentExpression expression)
		{
			string modifierText = string.Join(" ", FormatterUtility.GetModifiersFromEnum(expression.Modifiers).ToArray());
			if (string.IsNullOrEmpty(modifierText) == false) sb.Append(modifierText).Append(" ");

			sb.Append(FormatExpression(expression.Expression));
		}

		private void Format_TypeOf_Expression(StringBuilder sb, TypeOfExpression expression)
		{
			sb.Append("typeof(")
				.Append(FormatterUtility.FormatDataType(expression.TypeReference, document, controller))
				.Append(")");
		}

		private void Format_Anonymous_Method_Expression(StringBuilder sb, AnonymousMethodExpression expression)
		{
			sb.Append("delegate");
			List<string> parameters = new List<string>();
			foreach (IAstNode child in expression.ChildNodes)
			{
				if (child is ParameterDeclaration)
				{
					parameters.Add(FormatterUtility.FormatParameter((ParameterDeclaration)child, document, controller));
				}
			}

			if (parameters.Count > 0)
			{
				sb.Append("(");
				sb.Append(string.Join(", ", parameters.ToArray()));
				sb.Append(")");
			}
			indentLevel++;

			sb.Append(ProcessStatement(expression.BlockStatement).TrimEnd(Environment.NewLine.ToCharArray()));

			indentLevel--;
		}

		private void Format_Cast_Expression(StringBuilder sb, CastExpression expression)
		{
			sb.Append("(").Append(FormatterUtility.FormatDataType(expression.ReturnType, document, controller));
			sb.Append(") ").Append(FormatExpression(expression.Expression));
		}

		private void Format_Object_Collection_Initializer_Expression(StringBuilder sb,
																	 ObjectCollectionInitializerExpression ocie)
		{
			sb.Append("{");
			for (int i = 0; i < ocie.Initializers.Count; i++)
			{
				sb.Append(FormatExpression(ocie.Initializers[i] as Expression));
				if (ocie.Initializers.Count > 1 && i < ocie.Initializers.Count - 1)
					sb.Append(", ");
			}
			sb.Append("}");
		}

		private void Format_Object_Creation_Expression(StringBuilder sb, ObjectCreationExpression oce)
		{
			sb.Append("new ");
			if (oce.IsImplicitlyTyped == false)
			{
				sb.Append(FormatterUtility.FormatDataType(oce.ObjectType, document, controller));
			}

			// Figure out if this is a constructor call or array creation
			// If it is an array then it needs the square brackets. Constructor calls
			// with initialisers can omit the parentheses if there are no arguments.
			string openingBrace = "[";
			string closingBrace = "]";
			bool isArrayCreation = false;
			{
				IToken startToken = document.Tokens.GetTokenAtOffset(oce.StartOffset);
				TokenStream tokenStream = document.GetTokenStream(startToken);

				while (startToken != null && startToken.StartOffset < oce.EndOffset)
				{
					string tokenText = document.GetSubstring(startToken.TextRange);
					if (tokenText == "[")
					{
						isArrayCreation = true;
						break;
					}
					if (tokenText == "(")
						break;
					if (tokenText == "{")
						break; // If we get to the initialiser, we have gone too far.
					startToken = tokenStream.Read();
				}

				if (isArrayCreation == false)
				{
					openingBrace = "(";
					closingBrace = ")";
				}
			}

			Process_Generic_Type_Arguments(sb, oce.GenericTypeArguments);

			if (!isArrayCreation || (oce.Arguments != null && oce.Arguments.Count != 0) || oce.IsImplicitlyTyped)
			{
				sb.Append(openingBrace);
				if (oce.Arguments != null)
				{
					for (int i = 0; i < oce.Arguments.Count; i++)
					{
						sb.Append(FormatExpression(oce.Arguments[i] as Expression));
						if (oce.Arguments.Count > 1 && i < oce.Arguments.Count - 1)
							sb.Append(", ");
					}
				}
				sb.Append(closingBrace);
			}


			if (oce.Initializer != null)
				sb.Append(FormatExpression(oce.Initializer));

		}

		private void Format_Member_Access_Expression(StringBuilder sb, MemberAccess mac)
		{
			sb.Append(FormatExpression(mac.Expression)).Append(".").Append(mac.MemberName.Text);

			Process_Generic_Type_Arguments(sb, mac.GenericTypeArguments);
		}

		private void Format_Invocation_Expression(StringBuilder sb, InvocationExpression invoke)
		{
			sb.Append(FormatExpression(invoke.Expression));
			Process_Generic_Type_Arguments(sb, invoke.GenericTypeArguments);
			if (invoke.IsIndexerInvocation)
				sb.Append("[");
			else
				sb.Append("(");
			for (int i = 0; i < invoke.Arguments.Count; i++)
			{
				sb.Append(FormatExpression((Expression)invoke.Arguments[i]));
				if (invoke.Arguments.Count != 1 && i != invoke.Arguments.Count - 1)
					sb.Append(", ");
			}
			if (invoke.IsIndexerInvocation)
				sb.Append("]");
			else
				sb.Append(")");
		}

		private void Format_Assignment_Expression(StringBuilder sb, AssignmentExpression assign)
		{
			sb.Append(FormatExpression(assign.LeftExpression));
			sb.Append(" = ");
			sb.Append(FormatExpression(assign.RightExpression));
		}

		private void Format_Unary_Expression(StringBuilder sb, UnaryExpression unexp)
		{
			if (OperatorIsPrefix(unexp.OperatorType, true))
			{
				sb.Append(FormatterUtility.GetOperatorName(unexp.OperatorType));
				sb.Append(FormatExpression(unexp.Expression));
			}
			else
			{
				sb.Append(FormatExpression(unexp.Expression));
				sb.Append(FormatterUtility.GetOperatorName(unexp.OperatorType));
			}
		}

		private void Format_Binary_Expression(StringBuilder sb, BinaryExpression bin)
		{
			sb.Append(FormatExpression(bin.LeftExpression));
			sb.Append(" ").Append(FormatterUtility.GetOperatorName(bin.OperatorType)).Append(" ");
			sb.Append(FormatExpression(bin.RightExpression));
		}

		/// <summary>
		/// Returns true if the given OperatorType is a prefix operator.
		/// </summary>
		/// <param name="type">The OperatorType to inspect.</param>
		/// <param name="isUnary">Whether this is a unary operator</param>
		/// <returns>true if the given OperatorType is a prefix operator.</returns>
		private static bool OperatorIsPrefix(OperatorType type, bool isUnary)
		{
			if (isUnary && (type == OperatorType.Subtraction || type == OperatorType.Addition))
				return true;

			switch (type)
			{
				case OperatorType.AddressOf:
				case OperatorType.Negation:
				case OperatorType.PreDecrement:
				case OperatorType.PreIncrement:
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Processes a list of TypeReferences as generic type arguments. Appends &lt;types&gt; to the given StringBuilder.
		/// </summary>
		/// <param name="sb">The StringBuilder to append to.</param>
		/// <param name="genericArguments">The generic type nodes to append.</param>
		private void Process_Generic_Type_Arguments(StringBuilder sb, IAstNodeList genericArguments)
		{
			if (genericArguments.Count <= 0) return;

			sb.Append("<");
			for (int i = 0; i < genericArguments.Count; i++)
			{
				sb.Append(FormatterUtility.FormatDataType(genericArguments[i] as TypeReference, document, controller));
				if (genericArguments.Count != 1 && i != genericArguments.Count - 1)
					sb.Append(", ");
			}
			sb.Append(">");
		}

		/// <summary>
		/// Formats and appends the given IfStatement to the end of the StringBuilder.
		/// </summary>
		/// <param name="sb">The StringBuilder to append to.</param>
		/// <param name="ifStatement">The IfStatement to process.</param>
		private void Process_If_Statement(StringBuilder sb, IfStatement ifStatement)
		{
			sb.AppendFormat("{0}if ({1}) ", Indent, FormatExpression(ifStatement.Condition));

			Statement trueStatement = ifStatement.TrueStatement;
			Statement falseStatement = ifStatement.FalseStatement;

			if (trueStatement != null)
			{
				ProcessStatementBlock(sb, trueStatement);
			}

			if (falseStatement != null)
			{
				sb.AppendFormat("{0}else ", Indent);
				ProcessStatementBlock(sb, falseStatement);
			}
		}

		/// <summary>
		/// Use this method for formatting a Statement that should be treated as either a StatementBlock or as a 
		/// single statement, for instance the statement block for an IfStatement or ForStatement.
		/// </summary>
		/// <param name="sb"></param>
		/// <param name="statement"></param>
		private void ProcessStatementBlock(StringBuilder sb, Statement statement)
		{
			if (statement is BlockStatement)
			{
				Process_Block_Statement(sb, (BlockStatement)statement);
			}
			else
			{
				Process_Single_Statement_Block(sb, statement);
			}
		}

		/// <summary>
		/// Formats the given Statement as a single block and appends it to the end of the StringBuilder
		/// </summary>
		/// <param name="sb">The StringBuilder to append to.</param>
		/// <param name="statement">The Statement to process.</param>
		private void Process_Single_Statement_Block(StringBuilder sb, Statement statement)
		{
			bool commentsExist = false;

			if (formatSettings.SingleLineBlocksOnSameLineAsParent == false ||
				(formatSettings.AddBracesToSingleLineBlocks && formatSettings.PutBracesOnNewLines))
			{
				sb.Append(Environment.NewLine);
				indentLevel++;
			}

			if (statement.Comments != null && statement.Comments.Count > 0)
			{
				commentsExist = true;
			}

			if (formatSettings.AddBracesToSingleLineBlocks)
			{
				if (formatSettings.SingleLineBlocksOnSameLineAsParent == false || commentsExist)
				{
					indentLevel--;
					sb.Append(Indent);
					indentLevel++;
				}

				sb.Append("{");

				if (formatSettings.SingleLineBlocksOnSameLineAsParent == false || commentsExist)
				{
					sb.Append(Environment.NewLine).Append(Indent);
				}
			}
			else if (formatSettings.SingleLineBlocksOnSameLineAsParent == false || commentsExist)
			{
				sb.Append(Indent);
			}

			if (commentsExist)
			{
				foreach (Comment commentNode in statement.Comments)
				{
					sb.AppendLine(commentNode.Text.Trim()).Append(Indent);
				}
			}

			sb.AppendLine(ProcessStatement(statement));

			if (formatSettings.SingleLineBlocksOnSameLineAsParent == false || commentsExist ||
				(formatSettings.AddBracesToSingleLineBlocks && formatSettings.PutBracesOnNewLines))
			{
				indentLevel--;
			}
			if (formatSettings.AddBracesToSingleLineBlocks)
			{
				sb.Append(Indent);
				sb.AppendLine("}");
			}
		}

		/// <summary>
		/// Formats the given Statement as a single block and appends it to the end of the StringBuilder
		/// </summary>
		/// <param name="sb">The StringBuilder to append to.</param>
		/// <param name="statement">The Statement to process.</param>
		private void Process_Block_Statement(StringBuilder sb, BlockStatement statement)
		{
			sb.AppendLine();
			IAstNodeList comments = statement.Comments;
			sb.Append(ProcessBodyTextInternal(statement.Statements, comments, statement.StartOffset, statement.Length));
		}

		public void SetFormatSettings(CSharpFormatSettings settings)
		{
			if (settings == null) return;

			formatSettings.SetFrom(settings);
		}
	}
}
