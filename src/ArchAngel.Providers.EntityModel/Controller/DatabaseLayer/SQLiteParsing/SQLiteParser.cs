using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using ArrayList = System.Collections.Generic.List<object>;
using List = System.Collections.IList;
using RewriteRuleITokenStream = Antlr.Runtime.Tree.RewriteRuleTokenStream;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.SQLiteParsing
{
	[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.3 Nov 30, 2010 12:45:30")]
	[System.CLSCompliant(false)]
	public partial class SQLiteParser : Antlr.Runtime.Parser
	{
		internal static readonly string[] tokenNames = new string[] {
		"<invalid>", "<EOR>", "<DOWN>", "<UP>", "ALIAS", "BIND", "BIND_NAME", "BLOB_LITERAL", "COLUMN_CONSTRAINT", "COLUMN_EXPRESSION", "COLUMNS", "CONSTRAINTS", "CREATE_INDEX", "CREATE_TABLE", "DROP_INDEX", "DROP_TABLE", "FLOAT_LITERAL", "FUNCTION_LITERAL", "FUNCTION_EXPRESSION", "ID_LITERAL", "IN_VALUES", "IN_TABLE", "INTEGER_LITERAL", "IS_NULL", "NOT_NULL", "OPTIONS", "ORDERING", "SELECT_CORE", "STRING_LITERAL", "TABLE_CONSTRAINT", "TYPE", "TYPE_PARAMS", "SEMI", "EXPLAIN", "QUERY", "PLAN", "DOT", "INDEXED", "BY", "NOT", "OR", "AND", "ESCAPE", "IN", "LPAREN", "COMMA", "RPAREN", "ISNULL", "NOTNULL", "IS", "NULL", "BETWEEN", "EQUALS", "EQUALS2", "NOT_EQUALS", "NOT_EQUALS2", "LIKE", "GLOB", "REGEXP", "MATCH", "LESS", "LESS_OR_EQ", "GREATER", "GREATER_OR_EQ", "SHIFT_LEFT", "SHIFT_RIGHT", "AMPERSAND", "PIPE", "PLUS", "MINUS", "ASTERISK", "SLASH", "PERCENT", "DOUBLE_PIPE", "TILDA", "COLLATE", "ID", "DISTINCT", "CAST", "AS", "CASE", "ELSE", "END", "WHEN", "THEN", "INTEGER", "FLOAT", "STRING", "BLOB", "CURRENT_TIME", "CURRENT_DATE", "CURRENT_TIMESTAMP", "QUESTION", "COLON", "AT", "RAISE", "IGNORE", "ROLLBACK", "ABORT", "FAIL", "PRAGMA", "ATTACH", "DATABASE", "DETACH", "ANALYZE", "REINDEX", "VACUUM", "REPLACE", "ASC", "DESC", "ORDER", "LIMIT", "OFFSET", "UNION", "ALL", "INTERSECT", "EXCEPT", "SELECT", "FROM", "WHERE", "GROUP", "HAVING", "NATURAL", "LEFT", "OUTER", "INNER", "CROSS", "JOIN", "ON", "USING", "INSERT", "INTO", "VALUES", "DEFAULT", "UPDATE", "SET", "DELETE", "BEGIN", "DEFERRED", "IMMEDIATE", "EXCLUSIVE", "TRANSACTION", "COMMIT", "TO", "SAVEPOINT", "RELEASE", "CONFLICT", "CREATE", "VIRTUAL", "TABLE", "TEMPORARY", "IF", "EXISTS", "CONSTRAINT", "PRIMARY", "KEY", "AUTOINCREMENT", "UNIQUE", "CHECK", "FOREIGN", "REFERENCES", "CASCADE", "RESTRICT", "DEFERRABLE", "INITIALLY", "DROP", "ALTER", "RENAME", "ADD", "COLUMN", "VIEW", "INDEX", "TRIGGER", "BEFORE", "AFTER", "INSTEAD", "OF", "FOR", "EACH", "ROW", "BACKSLASH", "DOLLAR", "QUOTE_DOUBLE", "QUOTE_SINGLE", "APOSTROPHE", "LPAREN_SQUARE", "RPAREN_SQUARE", "UNDERSCORE", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "STRING_ESCAPE_SINGLE", "STRING_ESCAPE_DOUBLE", "STRING_CORE", "STRING_CORE_SINGLE", "STRING_CORE_DOUBLE", "STRING_SINGLE", "STRING_DOUBLE", "ID_START", "ID_CORE", "ID_PLAIN", "ID_QUOTED_CORE", "ID_QUOTED_CORE_SQUARE", "ID_QUOTED_CORE_APOSTROPHE", "ID_QUOTED_SQUARE", "ID_QUOTED_APOSTROPHE", "ID_QUOTED", "FLOAT_EXP", "COMMENT", "LINE_COMMENT", "WS"
	};
		public const int EOF = -1;
		public const int ALIAS = 4;
		public const int BIND = 5;
		public const int BIND_NAME = 6;
		public const int BLOB_LITERAL = 7;
		public const int COLUMN_CONSTRAINT = 8;
		public const int COLUMN_EXPRESSION = 9;
		public const int COLUMNS = 10;
		public const int CONSTRAINTS = 11;
		public const int CREATE_INDEX = 12;
		public const int CREATE_TABLE = 13;
		public const int DROP_INDEX = 14;
		public const int DROP_TABLE = 15;
		public const int FLOAT_LITERAL = 16;
		public const int FUNCTION_LITERAL = 17;
		public const int FUNCTION_EXPRESSION = 18;
		public const int ID_LITERAL = 19;
		public const int IN_VALUES = 20;
		public const int IN_TABLE = 21;
		public const int INTEGER_LITERAL = 22;
		public const int IS_NULL = 23;
		public const int NOT_NULL = 24;
		public const int OPTIONS = 25;
		public const int ORDERING = 26;
		public const int SELECT_CORE = 27;
		public const int STRING_LITERAL = 28;
		public const int TABLE_CONSTRAINT = 29;
		public const int TYPE = 30;
		public const int TYPE_PARAMS = 31;
		public const int SEMI = 32;
		public const int EXPLAIN = 33;
		public const int QUERY = 34;
		public const int PLAN = 35;
		public const int DOT = 36;
		public const int INDEXED = 37;
		public const int BY = 38;
		public const int NOT = 39;
		public const int OR = 40;
		public const int AND = 41;
		public const int ESCAPE = 42;
		public const int IN = 43;
		public const int LPAREN = 44;
		public const int COMMA = 45;
		public const int RPAREN = 46;
		public const int ISNULL = 47;
		public const int NOTNULL = 48;
		public const int IS = 49;
		public const int NULL = 50;
		public const int BETWEEN = 51;
		public const int EQUALS = 52;
		public const int EQUALS2 = 53;
		public const int NOT_EQUALS = 54;
		public const int NOT_EQUALS2 = 55;
		public const int LIKE = 56;
		public const int GLOB = 57;
		public const int REGEXP = 58;
		public const int MATCH = 59;
		public const int LESS = 60;
		public const int LESS_OR_EQ = 61;
		public const int GREATER = 62;
		public const int GREATER_OR_EQ = 63;
		public const int SHIFT_LEFT = 64;
		public const int SHIFT_RIGHT = 65;
		public const int AMPERSAND = 66;
		public const int PIPE = 67;
		public const int PLUS = 68;
		public const int MINUS = 69;
		public const int ASTERISK = 70;
		public const int SLASH = 71;
		public const int PERCENT = 72;
		public const int DOUBLE_PIPE = 73;
		public const int TILDA = 74;
		public const int COLLATE = 75;
		public const int ID = 76;
		public const int DISTINCT = 77;
		public const int CAST = 78;
		public const int AS = 79;
		public const int CASE = 80;
		public const int ELSE = 81;
		public const int END = 82;
		public const int WHEN = 83;
		public const int THEN = 84;
		public const int INTEGER = 85;
		public const int FLOAT = 86;
		public const int STRING = 87;
		public const int BLOB = 88;
		public const int CURRENT_TIME = 89;
		public const int CURRENT_DATE = 90;
		public const int CURRENT_TIMESTAMP = 91;
		public const int QUESTION = 92;
		public const int COLON = 93;
		public const int AT = 94;
		public const int RAISE = 95;
		public const int IGNORE = 96;
		public const int ROLLBACK = 97;
		public const int ABORT = 98;
		public const int FAIL = 99;
		public const int PRAGMA = 100;
		public const int ATTACH = 101;
		public const int DATABASE = 102;
		public const int DETACH = 103;
		public const int ANALYZE = 104;
		public const int REINDEX = 105;
		public const int VACUUM = 106;
		public const int REPLACE = 107;
		public const int ASC = 108;
		public const int DESC = 109;
		public const int ORDER = 110;
		public const int LIMIT = 111;
		public const int OFFSET = 112;
		public const int UNION = 113;
		public const int ALL = 114;
		public const int INTERSECT = 115;
		public const int EXCEPT = 116;
		public const int SELECT = 117;
		public const int FROM = 118;
		public const int WHERE = 119;
		public const int GROUP = 120;
		public const int HAVING = 121;
		public const int NATURAL = 122;
		public const int LEFT = 123;
		public const int OUTER = 124;
		public const int INNER = 125;
		public const int CROSS = 126;
		public const int JOIN = 127;
		public const int ON = 128;
		public const int USING = 129;
		public const int INSERT = 130;
		public const int INTO = 131;
		public const int VALUES = 132;
		public const int DEFAULT = 133;
		public const int UPDATE = 134;
		public const int SET = 135;
		public const int DELETE = 136;
		public const int BEGIN = 137;
		public const int DEFERRED = 138;
		public const int IMMEDIATE = 139;
		public const int EXCLUSIVE = 140;
		public const int TRANSACTION = 141;
		public const int COMMIT = 142;
		public const int TO = 143;
		public const int SAVEPOINT = 144;
		public const int RELEASE = 145;
		public const int CONFLICT = 146;
		public const int CREATE = 147;
		public const int VIRTUAL = 148;
		public const int TABLE = 149;
		public const int TEMPORARY = 150;
		public const int IF = 151;
		public const int EXISTS = 152;
		public const int CONSTRAINT = 153;
		public const int PRIMARY = 154;
		public const int KEY = 155;
		public const int AUTOINCREMENT = 156;
		public const int UNIQUE = 157;
		public const int CHECK = 158;
		public const int FOREIGN = 159;
		public const int REFERENCES = 160;
		public const int CASCADE = 161;
		public const int RESTRICT = 162;
		public const int DEFERRABLE = 163;
		public const int INITIALLY = 164;
		public const int DROP = 165;
		public const int ALTER = 166;
		public const int RENAME = 167;
		public const int ADD = 168;
		public const int COLUMN = 169;
		public const int VIEW = 170;
		public const int INDEX = 171;
		public const int TRIGGER = 172;
		public const int BEFORE = 173;
		public const int AFTER = 174;
		public const int INSTEAD = 175;
		public const int OF = 176;
		public const int FOR = 177;
		public const int EACH = 178;
		public const int ROW = 179;
		public const int BACKSLASH = 180;
		public const int DOLLAR = 181;
		public const int QUOTE_DOUBLE = 182;
		public const int QUOTE_SINGLE = 183;
		public const int APOSTROPHE = 184;
		public const int LPAREN_SQUARE = 185;
		public const int RPAREN_SQUARE = 186;
		public const int UNDERSCORE = 187;
		public const int A = 188;
		public const int B = 189;
		public const int C = 190;
		public const int D = 191;
		public const int E = 192;
		public const int F = 193;
		public const int G = 194;
		public const int H = 195;
		public const int I = 196;
		public const int J = 197;
		public const int K = 198;
		public const int L = 199;
		public const int M = 200;
		public const int N = 201;
		public const int O = 202;
		public const int P = 203;
		public const int Q = 204;
		public const int R = 205;
		public const int S = 206;
		public const int T = 207;
		public const int U = 208;
		public const int V = 209;
		public const int W = 210;
		public const int X = 211;
		public const int Y = 212;
		public const int Z = 213;
		public const int STRING_ESCAPE_SINGLE = 214;
		public const int STRING_ESCAPE_DOUBLE = 215;
		public const int STRING_CORE = 216;
		public const int STRING_CORE_SINGLE = 217;
		public const int STRING_CORE_DOUBLE = 218;
		public const int STRING_SINGLE = 219;
		public const int STRING_DOUBLE = 220;
		public const int ID_START = 221;
		public const int ID_CORE = 222;
		public const int ID_PLAIN = 223;
		public const int ID_QUOTED_CORE = 224;
		public const int ID_QUOTED_CORE_SQUARE = 225;
		public const int ID_QUOTED_CORE_APOSTROPHE = 226;
		public const int ID_QUOTED_SQUARE = 227;
		public const int ID_QUOTED_APOSTROPHE = 228;
		public const int ID_QUOTED = 229;
		public const int FLOAT_EXP = 230;
		public const int COMMENT = 231;
		public const int LINE_COMMENT = 232;
		public const int WS = 233;

		// delegates
		// delegators

#if ANTLR_DEBUG
		private static readonly bool[] decisionCanBacktrack =
			new bool[]
			{
				false, // invalid decision
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false, false, false, false, false, false, false, false, false, 
				false, false
			};
#else
		private static readonly bool[] decisionCanBacktrack = new bool[0];
#endif
		public SQLiteParser(ITokenStream input)
			: this(input, new RecognizerSharedState())
		{
		}
		public SQLiteParser(ITokenStream input, RecognizerSharedState state)
			: base(input, state)
		{
			ITreeAdaptor treeAdaptor = null;
			CreateTreeAdaptor(ref treeAdaptor);
			TreeAdaptor = treeAdaptor ?? new CommonTreeAdaptor();

			OnCreated();
		}

		// Implement this function in your helper file to use a custom tree adaptor
		partial void CreateTreeAdaptor(ref ITreeAdaptor adaptor);

		private ITreeAdaptor adaptor;

		public ITreeAdaptor TreeAdaptor
		{
			get
			{
				return adaptor;
			}
			set
			{
				this.adaptor = value;
			}
		}

		public override string[] TokenNames { get { return SQLiteParser.tokenNames; } }
		public override string GrammarFileName { get { return "C:\\Users\\Gareth\\Desktop\\test.g"; } }



		// Disable error recovery.
		protected Object recoverFromMismatchedToken(IIntStream input, int ttype, BitSet follow)
		{
			throw new MismatchedTokenException(ttype, input);
		}

		// Delegate error reporting to caller.
		public void displayRecognitionError(String[] tokenNames, RecognitionException e)
		{
			/*final StringBuilder buffer = new StringBuilder();
			buffer.append("[").append(getErrorHeader(e)).append("] ");
			buffer.append(getErrorMessage(e, tokenNames));
			if(e.token!=null) {
			  final CharStream stream = e.token.getInputStream();
			  if(stream!=null) {
				int size = stream.size();
				if(size>0) {
				  buffer.append("\n").append(stream.substring(0, size-1));
				}
			  }
			}
			throw new SqlJetParserException(buffer.toString(), e);*/
			throw new Exception("TODO: (GFH) implement code for displayRecognitionError()");
		}

		// unquotes identifier
		private String unquoteId(String id)
		{
			if (id == null)
			{
				return null;
			}
			int len = id.Length;
			if (len == 0)
			{
				return "";
			}
			char first = id[0];
			char last = id[len - 1];
			switch (first)
			{
				case '[':
					first = ']';
					break;
				case '\'':
				case '"':
				case '`':
					if (first == last)
					{
						return id.Substring(1, len - 1);
					}
					break;
				default:
					return id;
			}
			return null;
		}



		partial void OnCreated();
		partial void EnterRule(string ruleName, int ruleIndex);
		partial void LeaveRule(string ruleName, int ruleIndex);

		#region Rules
		public class sql_stmt_list_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_sql_stmt_list();
		partial void Leave_sql_stmt_list();

		// $ANTLR start "sql_stmt_list"
		// C:\\Users\\Gareth\\Desktop\\test.g:145:1: sql_stmt_list : sql_stmt ( SEMI ( sql_stmt SEMI )* )? EOF ;
		[GrammarRule("sql_stmt_list")]
		public SQLiteParser.sql_stmt_list_return sql_stmt_list()
		{
			Enter_sql_stmt_list();
			EnterRule("sql_stmt_list", 1);
			TraceIn("sql_stmt_list", 1);
			SQLiteParser.sql_stmt_list_return retval = new SQLiteParser.sql_stmt_list_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken SEMI2 = null;
			CommonToken SEMI4 = null;
			CommonToken EOF5 = null;
			SQLiteParser.sql_stmt_return sql_stmt1 = default(SQLiteParser.sql_stmt_return);
			SQLiteParser.sql_stmt_return sql_stmt3 = default(SQLiteParser.sql_stmt_return);

			CommonTree SEMI2_tree = null;
			CommonTree SEMI4_tree = null;
			CommonTree EOF5_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "sql_stmt_list");
				DebugLocation(145, 56);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:145:14: ( sql_stmt ( SEMI ( sql_stmt SEMI )* )? EOF )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:145:16: sql_stmt ( SEMI ( sql_stmt SEMI )* )? EOF
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(145, 16);
						PushFollow(Follow._sql_stmt_in_sql_stmt_list212);
						sql_stmt1 = sql_stmt();
						PopFollow();

						adaptor.AddChild(root_0, sql_stmt1.Tree);
						DebugLocation(145, 25);
						// C:\\Users\\Gareth\\Desktop\\test.g:145:25: ( SEMI ( sql_stmt SEMI )* )?
						int alt2 = 2;
						try
						{
							DebugEnterSubRule(2);
							try
							{
								DebugEnterDecision(2, decisionCanBacktrack[2]);
								int LA2_0 = input.LA(1);

								if ((LA2_0 == SEMI))
								{
									alt2 = 1;
								}
							}
							finally { DebugExitDecision(2); }
							switch (alt2)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:145:26: SEMI ( sql_stmt SEMI )*
									{
										DebugLocation(145, 30);
										SEMI2 = (CommonToken)Match(input, SEMI, Follow._SEMI_in_sql_stmt_list215);
										DebugLocation(145, 32);
										// C:\\Users\\Gareth\\Desktop\\test.g:145:32: ( sql_stmt SEMI )*
										try
										{
											DebugEnterSubRule(1);
											while (true)
											{
												int alt1 = 2;
												try
												{
													DebugEnterDecision(1, decisionCanBacktrack[1]);
													try
													{
														alt1 = dfa1.Predict(input);
													}
													catch (NoViableAltException nvae)
													{
														DebugRecognitionException(nvae);
														throw;
													}
												}
												finally { DebugExitDecision(1); }
												switch (alt1)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:145:33: sql_stmt SEMI
														{
															DebugLocation(145, 33);
															PushFollow(Follow._sql_stmt_in_sql_stmt_list219);
															sql_stmt3 = sql_stmt();
															PopFollow();

															adaptor.AddChild(root_0, sql_stmt3.Tree);
															DebugLocation(145, 46);
															SEMI4 = (CommonToken)Match(input, SEMI, Follow._SEMI_in_sql_stmt_list221);

														}
														break;

													default:
														goto loop1;
												}
											}

										loop1:
											;

										}
										finally { DebugExitSubRule(1); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(2); }

						DebugLocation(145, 56);
						EOF5 = (CommonToken)Match(input, EOF, Follow._EOF_in_sql_stmt_list229);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("sql_stmt_list", 1);
					LeaveRule("sql_stmt_list", 1);
					Leave_sql_stmt_list();
				}
				DebugLocation(145, 56);
			}
			finally { DebugExitRule(GrammarFileName, "sql_stmt_list"); }
			return retval;

		}
		// $ANTLR end "sql_stmt_list"

		public class sql_stmt_itself_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_sql_stmt_itself();
		partial void Leave_sql_stmt_itself();

		// $ANTLR start "sql_stmt_itself"
		// C:\\Users\\Gareth\\Desktop\\test.g:147:1: sql_stmt_itself : sql_stmt ( SEMI )? EOF ;
		[GrammarRule("sql_stmt_itself")]
		private SQLiteParser.sql_stmt_itself_return sql_stmt_itself()
		{
			Enter_sql_stmt_itself();
			EnterRule("sql_stmt_itself", 2);
			TraceIn("sql_stmt_itself", 2);
			SQLiteParser.sql_stmt_itself_return retval = new SQLiteParser.sql_stmt_itself_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken SEMI7 = null;
			CommonToken EOF8 = null;
			SQLiteParser.sql_stmt_return sql_stmt6 = default(SQLiteParser.sql_stmt_return);

			CommonTree SEMI7_tree = null;
			CommonTree EOF8_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "sql_stmt_itself");
				DebugLocation(147, 39);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:147:16: ( sql_stmt ( SEMI )? EOF )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:147:18: sql_stmt ( SEMI )? EOF
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(147, 18);
						PushFollow(Follow._sql_stmt_in_sql_stmt_itself237);
						sql_stmt6 = sql_stmt();
						PopFollow();

						adaptor.AddChild(root_0, sql_stmt6.Tree);
						DebugLocation(147, 27);
						// C:\\Users\\Gareth\\Desktop\\test.g:147:27: ( SEMI )?
						int alt3 = 2;
						try
						{
							DebugEnterSubRule(3);
							try
							{
								DebugEnterDecision(3, decisionCanBacktrack[3]);
								int LA3_0 = input.LA(1);

								if ((LA3_0 == SEMI))
								{
									alt3 = 1;
								}
							}
							finally { DebugExitDecision(3); }
							switch (alt3)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:147:28: SEMI
									{
										DebugLocation(147, 32);
										SEMI7 = (CommonToken)Match(input, SEMI, Follow._SEMI_in_sql_stmt_itself240);

									}
									break;

							}
						}
						finally { DebugExitSubRule(3); }

						DebugLocation(147, 39);
						EOF8 = (CommonToken)Match(input, EOF, Follow._EOF_in_sql_stmt_itself245);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("sql_stmt_itself", 2);
					LeaveRule("sql_stmt_itself", 2);
					Leave_sql_stmt_itself();
				}
				DebugLocation(147, 39);
			}
			finally { DebugExitRule(GrammarFileName, "sql_stmt_itself"); }
			return retval;

		}
		// $ANTLR end "sql_stmt_itself"

		public class sql_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_sql_stmt();
		partial void Leave_sql_stmt();

		// $ANTLR start "sql_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:149:1: sql_stmt : ( EXPLAIN ( QUERY PLAN )? )? sql_stmt_core ;
		[GrammarRule("sql_stmt")]
		private SQLiteParser.sql_stmt_return sql_stmt()
		{
			Enter_sql_stmt();
			EnterRule("sql_stmt", 3);
			TraceIn("sql_stmt", 3);
			SQLiteParser.sql_stmt_return retval = new SQLiteParser.sql_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken EXPLAIN9 = null;
			CommonToken QUERY10 = null;
			CommonToken PLAN11 = null;
			SQLiteParser.sql_stmt_core_return sql_stmt_core12 = default(SQLiteParser.sql_stmt_core_return);

			CommonTree EXPLAIN9_tree = null;
			CommonTree QUERY10_tree = null;
			CommonTree PLAN11_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "sql_stmt");
				DebugLocation(149, 48);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:149:9: ( ( EXPLAIN ( QUERY PLAN )? )? sql_stmt_core )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:149:11: ( EXPLAIN ( QUERY PLAN )? )? sql_stmt_core
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(149, 11);
						// C:\\Users\\Gareth\\Desktop\\test.g:149:11: ( EXPLAIN ( QUERY PLAN )? )?
						int alt5 = 2;
						try
						{
							DebugEnterSubRule(5);
							try
							{
								DebugEnterDecision(5, decisionCanBacktrack[5]);
								try
								{
									alt5 = dfa5.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(5); }
							switch (alt5)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:149:12: EXPLAIN ( QUERY PLAN )?
									{
										DebugLocation(149, 12);
										EXPLAIN9 = (CommonToken)Match(input, EXPLAIN, Follow._EXPLAIN_in_sql_stmt254);
										EXPLAIN9_tree = (CommonTree)adaptor.Create(EXPLAIN9);
										adaptor.AddChild(root_0, EXPLAIN9_tree);

										DebugLocation(149, 20);
										// C:\\Users\\Gareth\\Desktop\\test.g:149:20: ( QUERY PLAN )?
										int alt4 = 2;
										try
										{
											DebugEnterSubRule(4);
											try
											{
												DebugEnterDecision(4, decisionCanBacktrack[4]);
												try
												{
													alt4 = dfa4.Predict(input);
												}
												catch (NoViableAltException nvae)
												{
													DebugRecognitionException(nvae);
													throw;
												}
											}
											finally { DebugExitDecision(4); }
											switch (alt4)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:149:21: QUERY PLAN
													{
														DebugLocation(149, 21);
														QUERY10 = (CommonToken)Match(input, QUERY, Follow._QUERY_in_sql_stmt257);
														QUERY10_tree = (CommonTree)adaptor.Create(QUERY10);
														adaptor.AddChild(root_0, QUERY10_tree);

														DebugLocation(149, 27);
														PLAN11 = (CommonToken)Match(input, PLAN, Follow._PLAN_in_sql_stmt259);
														PLAN11_tree = (CommonTree)adaptor.Create(PLAN11);
														adaptor.AddChild(root_0, PLAN11_tree);


													}
													break;

											}
										}
										finally { DebugExitSubRule(4); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(5); }

						DebugLocation(149, 36);
						PushFollow(Follow._sql_stmt_core_in_sql_stmt265);
						sql_stmt_core12 = sql_stmt_core();
						PopFollow();

						adaptor.AddChild(root_0, sql_stmt_core12.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("sql_stmt", 3);
					LeaveRule("sql_stmt", 3);
					Leave_sql_stmt();
				}
				DebugLocation(149, 48);
			}
			finally { DebugExitRule(GrammarFileName, "sql_stmt"); }
			return retval;

		}
		// $ANTLR end "sql_stmt"

		public class sql_stmt_core_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_sql_stmt_core();
		partial void Leave_sql_stmt_core();

		// $ANTLR start "sql_stmt_core"
		// C:\\Users\\Gareth\\Desktop\\test.g:151:1: sql_stmt_core : ( pragma_stmt | attach_stmt | detach_stmt | analyze_stmt | reindex_stmt | vacuum_stmt | select_stmt | insert_stmt | update_stmt | delete_stmt | begin_stmt | commit_stmt | rollback_stmt | savepoint_stmt | release_stmt | create_virtual_table_stmt | create_table_stmt | drop_table_stmt | alter_table_stmt | create_view_stmt | drop_view_stmt | create_index_stmt | drop_index_stmt | create_trigger_stmt | drop_trigger_stmt );
		[GrammarRule("sql_stmt_core")]
		private SQLiteParser.sql_stmt_core_return sql_stmt_core()
		{
			Enter_sql_stmt_core();
			EnterRule("sql_stmt_core", 4);
			TraceIn("sql_stmt_core", 4);
			SQLiteParser.sql_stmt_core_return retval = new SQLiteParser.sql_stmt_core_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.pragma_stmt_return pragma_stmt13 = default(SQLiteParser.pragma_stmt_return);
			SQLiteParser.attach_stmt_return attach_stmt14 = default(SQLiteParser.attach_stmt_return);
			SQLiteParser.detach_stmt_return detach_stmt15 = default(SQLiteParser.detach_stmt_return);
			SQLiteParser.analyze_stmt_return analyze_stmt16 = default(SQLiteParser.analyze_stmt_return);
			SQLiteParser.reindex_stmt_return reindex_stmt17 = default(SQLiteParser.reindex_stmt_return);
			SQLiteParser.vacuum_stmt_return vacuum_stmt18 = default(SQLiteParser.vacuum_stmt_return);
			SQLiteParser.select_stmt_return select_stmt19 = default(SQLiteParser.select_stmt_return);
			SQLiteParser.insert_stmt_return insert_stmt20 = default(SQLiteParser.insert_stmt_return);
			SQLiteParser.update_stmt_return update_stmt21 = default(SQLiteParser.update_stmt_return);
			SQLiteParser.delete_stmt_return delete_stmt22 = default(SQLiteParser.delete_stmt_return);
			SQLiteParser.begin_stmt_return begin_stmt23 = default(SQLiteParser.begin_stmt_return);
			SQLiteParser.commit_stmt_return commit_stmt24 = default(SQLiteParser.commit_stmt_return);
			SQLiteParser.rollback_stmt_return rollback_stmt25 = default(SQLiteParser.rollback_stmt_return);
			SQLiteParser.savepoint_stmt_return savepoint_stmt26 = default(SQLiteParser.savepoint_stmt_return);
			SQLiteParser.release_stmt_return release_stmt27 = default(SQLiteParser.release_stmt_return);
			SQLiteParser.create_virtual_table_stmt_return create_virtual_table_stmt28 = default(SQLiteParser.create_virtual_table_stmt_return);
			SQLiteParser.create_table_stmt_return create_table_stmt29 = default(SQLiteParser.create_table_stmt_return);
			SQLiteParser.drop_table_stmt_return drop_table_stmt30 = default(SQLiteParser.drop_table_stmt_return);
			SQLiteParser.alter_table_stmt_return alter_table_stmt31 = default(SQLiteParser.alter_table_stmt_return);
			SQLiteParser.create_view_stmt_return create_view_stmt32 = default(SQLiteParser.create_view_stmt_return);
			SQLiteParser.drop_view_stmt_return drop_view_stmt33 = default(SQLiteParser.drop_view_stmt_return);
			SQLiteParser.create_index_stmt_return create_index_stmt34 = default(SQLiteParser.create_index_stmt_return);
			SQLiteParser.drop_index_stmt_return drop_index_stmt35 = default(SQLiteParser.drop_index_stmt_return);
			SQLiteParser.create_trigger_stmt_return create_trigger_stmt36 = default(SQLiteParser.create_trigger_stmt_return);
			SQLiteParser.drop_trigger_stmt_return drop_trigger_stmt37 = default(SQLiteParser.drop_trigger_stmt_return);


			try
			{
				DebugEnterRule(GrammarFileName, "sql_stmt_core");
				DebugLocation(151, 2);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:152:3: ( pragma_stmt | attach_stmt | detach_stmt | analyze_stmt | reindex_stmt | vacuum_stmt | select_stmt | insert_stmt | update_stmt | delete_stmt | begin_stmt | commit_stmt | rollback_stmt | savepoint_stmt | release_stmt | create_virtual_table_stmt | create_table_stmt | drop_table_stmt | alter_table_stmt | create_view_stmt | drop_view_stmt | create_index_stmt | drop_index_stmt | create_trigger_stmt | drop_trigger_stmt )
					int alt6 = 25;
					try
					{
						DebugEnterDecision(6, decisionCanBacktrack[6]);
						try
						{
							alt6 = dfa6.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(6); }
					switch (alt6)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:152:5: pragma_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(152, 5);
								PushFollow(Follow._pragma_stmt_in_sql_stmt_core275);
								pragma_stmt13 = pragma_stmt();
								PopFollow();

								adaptor.AddChild(root_0, pragma_stmt13.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:153:5: attach_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(153, 5);
								PushFollow(Follow._attach_stmt_in_sql_stmt_core281);
								attach_stmt14 = attach_stmt();
								PopFollow();

								adaptor.AddChild(root_0, attach_stmt14.Tree);

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:154:5: detach_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(154, 5);
								PushFollow(Follow._detach_stmt_in_sql_stmt_core287);
								detach_stmt15 = detach_stmt();
								PopFollow();

								adaptor.AddChild(root_0, detach_stmt15.Tree);

							}
							break;
						case 4:
							DebugEnterAlt(4);
							// C:\\Users\\Gareth\\Desktop\\test.g:155:5: analyze_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(155, 5);
								PushFollow(Follow._analyze_stmt_in_sql_stmt_core293);
								analyze_stmt16 = analyze_stmt();
								PopFollow();

								adaptor.AddChild(root_0, analyze_stmt16.Tree);

							}
							break;
						case 5:
							DebugEnterAlt(5);
							// C:\\Users\\Gareth\\Desktop\\test.g:156:5: reindex_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(156, 5);
								PushFollow(Follow._reindex_stmt_in_sql_stmt_core299);
								reindex_stmt17 = reindex_stmt();
								PopFollow();

								adaptor.AddChild(root_0, reindex_stmt17.Tree);

							}
							break;
						case 6:
							DebugEnterAlt(6);
							// C:\\Users\\Gareth\\Desktop\\test.g:157:5: vacuum_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(157, 5);
								PushFollow(Follow._vacuum_stmt_in_sql_stmt_core305);
								vacuum_stmt18 = vacuum_stmt();
								PopFollow();

								adaptor.AddChild(root_0, vacuum_stmt18.Tree);

							}
							break;
						case 7:
							DebugEnterAlt(7);
							// C:\\Users\\Gareth\\Desktop\\test.g:159:5: select_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(159, 5);
								PushFollow(Follow._select_stmt_in_sql_stmt_core314);
								select_stmt19 = select_stmt();
								PopFollow();

								adaptor.AddChild(root_0, select_stmt19.Tree);

							}
							break;
						case 8:
							DebugEnterAlt(8);
							// C:\\Users\\Gareth\\Desktop\\test.g:160:5: insert_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(160, 5);
								PushFollow(Follow._insert_stmt_in_sql_stmt_core320);
								insert_stmt20 = insert_stmt();
								PopFollow();

								adaptor.AddChild(root_0, insert_stmt20.Tree);

							}
							break;
						case 9:
							DebugEnterAlt(9);
							// C:\\Users\\Gareth\\Desktop\\test.g:161:5: update_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(161, 5);
								PushFollow(Follow._update_stmt_in_sql_stmt_core326);
								update_stmt21 = update_stmt();
								PopFollow();

								adaptor.AddChild(root_0, update_stmt21.Tree);

							}
							break;
						case 10:
							DebugEnterAlt(10);
							// C:\\Users\\Gareth\\Desktop\\test.g:162:5: delete_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(162, 5);
								PushFollow(Follow._delete_stmt_in_sql_stmt_core332);
								delete_stmt22 = delete_stmt();
								PopFollow();

								adaptor.AddChild(root_0, delete_stmt22.Tree);

							}
							break;
						case 11:
							DebugEnterAlt(11);
							// C:\\Users\\Gareth\\Desktop\\test.g:163:5: begin_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(163, 5);
								PushFollow(Follow._begin_stmt_in_sql_stmt_core338);
								begin_stmt23 = begin_stmt();
								PopFollow();

								adaptor.AddChild(root_0, begin_stmt23.Tree);

							}
							break;
						case 12:
							DebugEnterAlt(12);
							// C:\\Users\\Gareth\\Desktop\\test.g:164:5: commit_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(164, 5);
								PushFollow(Follow._commit_stmt_in_sql_stmt_core344);
								commit_stmt24 = commit_stmt();
								PopFollow();

								adaptor.AddChild(root_0, commit_stmt24.Tree);

							}
							break;
						case 13:
							DebugEnterAlt(13);
							// C:\\Users\\Gareth\\Desktop\\test.g:165:5: rollback_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(165, 5);
								PushFollow(Follow._rollback_stmt_in_sql_stmt_core350);
								rollback_stmt25 = rollback_stmt();
								PopFollow();

								adaptor.AddChild(root_0, rollback_stmt25.Tree);

							}
							break;
						case 14:
							DebugEnterAlt(14);
							// C:\\Users\\Gareth\\Desktop\\test.g:166:5: savepoint_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(166, 5);
								PushFollow(Follow._savepoint_stmt_in_sql_stmt_core356);
								savepoint_stmt26 = savepoint_stmt();
								PopFollow();

								adaptor.AddChild(root_0, savepoint_stmt26.Tree);

							}
							break;
						case 15:
							DebugEnterAlt(15);
							// C:\\Users\\Gareth\\Desktop\\test.g:167:5: release_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(167, 5);
								PushFollow(Follow._release_stmt_in_sql_stmt_core362);
								release_stmt27 = release_stmt();
								PopFollow();

								adaptor.AddChild(root_0, release_stmt27.Tree);

							}
							break;
						case 16:
							DebugEnterAlt(16);
							// C:\\Users\\Gareth\\Desktop\\test.g:169:5: create_virtual_table_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(169, 5);
								PushFollow(Follow._create_virtual_table_stmt_in_sql_stmt_core371);
								create_virtual_table_stmt28 = create_virtual_table_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_virtual_table_stmt28.Tree);

							}
							break;
						case 17:
							DebugEnterAlt(17);
							// C:\\Users\\Gareth\\Desktop\\test.g:170:5: create_table_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(170, 5);
								PushFollow(Follow._create_table_stmt_in_sql_stmt_core377);
								create_table_stmt29 = create_table_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_table_stmt29.Tree);

							}
							break;
						case 18:
							DebugEnterAlt(18);
							// C:\\Users\\Gareth\\Desktop\\test.g:171:5: drop_table_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(171, 5);
								PushFollow(Follow._drop_table_stmt_in_sql_stmt_core383);
								drop_table_stmt30 = drop_table_stmt();
								PopFollow();

								adaptor.AddChild(root_0, drop_table_stmt30.Tree);

							}
							break;
						case 19:
							DebugEnterAlt(19);
							// C:\\Users\\Gareth\\Desktop\\test.g:172:5: alter_table_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(172, 5);
								PushFollow(Follow._alter_table_stmt_in_sql_stmt_core389);
								alter_table_stmt31 = alter_table_stmt();
								PopFollow();

								adaptor.AddChild(root_0, alter_table_stmt31.Tree);

							}
							break;
						case 20:
							DebugEnterAlt(20);
							// C:\\Users\\Gareth\\Desktop\\test.g:173:5: create_view_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(173, 5);
								PushFollow(Follow._create_view_stmt_in_sql_stmt_core395);
								create_view_stmt32 = create_view_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_view_stmt32.Tree);

							}
							break;
						case 21:
							DebugEnterAlt(21);
							// C:\\Users\\Gareth\\Desktop\\test.g:174:5: drop_view_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(174, 5);
								PushFollow(Follow._drop_view_stmt_in_sql_stmt_core401);
								drop_view_stmt33 = drop_view_stmt();
								PopFollow();

								adaptor.AddChild(root_0, drop_view_stmt33.Tree);

							}
							break;
						case 22:
							DebugEnterAlt(22);
							// C:\\Users\\Gareth\\Desktop\\test.g:175:5: create_index_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(175, 5);
								PushFollow(Follow._create_index_stmt_in_sql_stmt_core407);
								create_index_stmt34 = create_index_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_index_stmt34.Tree);

							}
							break;
						case 23:
							DebugEnterAlt(23);
							// C:\\Users\\Gareth\\Desktop\\test.g:176:5: drop_index_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(176, 5);
								PushFollow(Follow._drop_index_stmt_in_sql_stmt_core413);
								drop_index_stmt35 = drop_index_stmt();
								PopFollow();

								adaptor.AddChild(root_0, drop_index_stmt35.Tree);

							}
							break;
						case 24:
							DebugEnterAlt(24);
							// C:\\Users\\Gareth\\Desktop\\test.g:177:5: create_trigger_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(177, 5);
								PushFollow(Follow._create_trigger_stmt_in_sql_stmt_core419);
								create_trigger_stmt36 = create_trigger_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_trigger_stmt36.Tree);

							}
							break;
						case 25:
							DebugEnterAlt(25);
							// C:\\Users\\Gareth\\Desktop\\test.g:178:5: drop_trigger_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(178, 5);
								PushFollow(Follow._drop_trigger_stmt_in_sql_stmt_core425);
								drop_trigger_stmt37 = drop_trigger_stmt();
								PopFollow();

								adaptor.AddChild(root_0, drop_trigger_stmt37.Tree);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("sql_stmt_core", 4);
					LeaveRule("sql_stmt_core", 4);
					Leave_sql_stmt_core();
				}
				DebugLocation(179, 2);
			}
			finally { DebugExitRule(GrammarFileName, "sql_stmt_core"); }
			return retval;

		}
		// $ANTLR end "sql_stmt_core"

		public class schema_create_table_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_schema_create_table_stmt();
		partial void Leave_schema_create_table_stmt();

		// $ANTLR start "schema_create_table_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:181:1: schema_create_table_stmt : ( create_virtual_table_stmt | create_table_stmt );
		[GrammarRule("schema_create_table_stmt")]
		private SQLiteParser.schema_create_table_stmt_return schema_create_table_stmt()
		{
			Enter_schema_create_table_stmt();
			EnterRule("schema_create_table_stmt", 5);
			TraceIn("schema_create_table_stmt", 5);
			SQLiteParser.schema_create_table_stmt_return retval = new SQLiteParser.schema_create_table_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.create_virtual_table_stmt_return create_virtual_table_stmt38 = default(SQLiteParser.create_virtual_table_stmt_return);
			SQLiteParser.create_table_stmt_return create_table_stmt39 = default(SQLiteParser.create_table_stmt_return);


			try
			{
				DebugEnterRule(GrammarFileName, "schema_create_table_stmt");
				DebugLocation(181, 71);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:181:25: ( create_virtual_table_stmt | create_table_stmt )
					int alt7 = 2;
					try
					{
						DebugEnterDecision(7, decisionCanBacktrack[7]);
						int LA7_0 = input.LA(1);

						if ((LA7_0 == CREATE))
						{
							int LA7_1 = input.LA(2);

							if ((LA7_1 == VIRTUAL))
							{
								alt7 = 1;
							}
							else if (((LA7_1 >= TABLE && LA7_1 <= TEMPORARY)))
							{
								alt7 = 2;
							}
							else
							{
								NoViableAltException nvae = new NoViableAltException("", 7, 1, input);

								DebugRecognitionException(nvae);
								throw nvae;
							}
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 7, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(7); }
					switch (alt7)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:181:27: create_virtual_table_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(181, 27);
								PushFollow(Follow._create_virtual_table_stmt_in_schema_create_table_stmt435);
								create_virtual_table_stmt38 = create_virtual_table_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_virtual_table_stmt38.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:181:55: create_table_stmt
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(181, 55);
								PushFollow(Follow._create_table_stmt_in_schema_create_table_stmt439);
								create_table_stmt39 = create_table_stmt();
								PopFollow();

								adaptor.AddChild(root_0, create_table_stmt39.Tree);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("schema_create_table_stmt", 5);
					LeaveRule("schema_create_table_stmt", 5);
					Leave_schema_create_table_stmt();
				}
				DebugLocation(181, 71);
			}
			finally { DebugExitRule(GrammarFileName, "schema_create_table_stmt"); }
			return retval;

		}
		// $ANTLR end "schema_create_table_stmt"

		public class qualified_table_name_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_qualified_table_name();
		partial void Leave_qualified_table_name();

		// $ANTLR start "qualified_table_name"
		// C:\\Users\\Gareth\\Desktop\\test.g:183:1: qualified_table_name : (database_name= id DOT )? table_name= id ( INDEXED BY index_name= id | NOT INDEXED )? ;
		[GrammarRule("qualified_table_name")]
		private SQLiteParser.qualified_table_name_return qualified_table_name()
		{
			Enter_qualified_table_name();
			EnterRule("qualified_table_name", 6);
			TraceIn("qualified_table_name", 6);
			SQLiteParser.qualified_table_name_return retval = new SQLiteParser.qualified_table_name_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DOT40 = null;
			CommonToken INDEXED41 = null;
			CommonToken BY42 = null;
			CommonToken NOT43 = null;
			CommonToken INDEXED44 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return index_name = default(SQLiteParser.id_return);

			CommonTree DOT40_tree = null;
			CommonTree INDEXED41_tree = null;
			CommonTree BY42_tree = null;
			CommonTree NOT43_tree = null;
			CommonTree INDEXED44_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "qualified_table_name");
				DebugLocation(183, 101);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:183:21: ( (database_name= id DOT )? table_name= id ( INDEXED BY index_name= id | NOT INDEXED )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:183:23: (database_name= id DOT )? table_name= id ( INDEXED BY index_name= id | NOT INDEXED )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(183, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:183:23: (database_name= id DOT )?
						int alt8 = 2;
						try
						{
							DebugEnterSubRule(8);
							try
							{
								DebugEnterDecision(8, decisionCanBacktrack[8]);
								try
								{
									alt8 = dfa8.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(8); }
							switch (alt8)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:183:24: database_name= id DOT
									{
										DebugLocation(183, 37);
										PushFollow(Follow._id_in_qualified_table_name449);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(183, 41);
										DOT40 = (CommonToken)Match(input, DOT, Follow._DOT_in_qualified_table_name451);
										DOT40_tree = (CommonTree)adaptor.Create(DOT40);
										adaptor.AddChild(root_0, DOT40_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(8); }

						DebugLocation(183, 57);
						PushFollow(Follow._id_in_qualified_table_name457);
						table_name = id();
						PopFollow();

						adaptor.AddChild(root_0, table_name.Tree);
						DebugLocation(183, 61);
						// C:\\Users\\Gareth\\Desktop\\test.g:183:61: ( INDEXED BY index_name= id | NOT INDEXED )?
						int alt9 = 3;
						try
						{
							DebugEnterSubRule(9);
							try
							{
								DebugEnterDecision(9, decisionCanBacktrack[9]);
								int LA9_0 = input.LA(1);

								if ((LA9_0 == INDEXED))
								{
									alt9 = 1;
								}
								else if ((LA9_0 == NOT))
								{
									alt9 = 2;
								}
							}
							finally { DebugExitDecision(9); }
							switch (alt9)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:183:62: INDEXED BY index_name= id
									{
										DebugLocation(183, 62);
										INDEXED41 = (CommonToken)Match(input, INDEXED, Follow._INDEXED_in_qualified_table_name460);
										INDEXED41_tree = (CommonTree)adaptor.Create(INDEXED41);
										adaptor.AddChild(root_0, INDEXED41_tree);

										DebugLocation(183, 70);
										BY42 = (CommonToken)Match(input, BY, Follow._BY_in_qualified_table_name462);
										BY42_tree = (CommonTree)adaptor.Create(BY42);
										adaptor.AddChild(root_0, BY42_tree);

										DebugLocation(183, 83);
										PushFollow(Follow._id_in_qualified_table_name466);
										index_name = id();
										PopFollow();

										adaptor.AddChild(root_0, index_name.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:183:89: NOT INDEXED
									{
										DebugLocation(183, 89);
										NOT43 = (CommonToken)Match(input, NOT, Follow._NOT_in_qualified_table_name470);
										NOT43_tree = (CommonTree)adaptor.Create(NOT43);
										adaptor.AddChild(root_0, NOT43_tree);

										DebugLocation(183, 93);
										INDEXED44 = (CommonToken)Match(input, INDEXED, Follow._INDEXED_in_qualified_table_name472);
										INDEXED44_tree = (CommonTree)adaptor.Create(INDEXED44);
										adaptor.AddChild(root_0, INDEXED44_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(9); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("qualified_table_name", 6);
					LeaveRule("qualified_table_name", 6);
					Leave_qualified_table_name();
				}
				DebugLocation(183, 101);
			}
			finally { DebugExitRule(GrammarFileName, "qualified_table_name"); }
			return retval;

		}
		// $ANTLR end "qualified_table_name"

		public class expr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_expr();
		partial void Leave_expr();

		// $ANTLR start "expr"
		// C:\\Users\\Gareth\\Desktop\\test.g:185:1: expr : or_subexpr ( OR or_subexpr )* ;
		[GrammarRule("expr")]
		private SQLiteParser.expr_return expr()
		{
			Enter_expr();
			EnterRule("expr", 7);
			TraceIn("expr", 7);
			SQLiteParser.expr_return retval = new SQLiteParser.expr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken OR46 = null;
			SQLiteParser.or_subexpr_return or_subexpr45 = default(SQLiteParser.or_subexpr_return);
			SQLiteParser.or_subexpr_return or_subexpr47 = default(SQLiteParser.or_subexpr_return);

			CommonTree OR46_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "expr");
				DebugLocation(185, 34);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:185:5: ( or_subexpr ( OR or_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:185:7: or_subexpr ( OR or_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(185, 7);
						PushFollow(Follow._or_subexpr_in_expr481);
						or_subexpr45 = or_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, or_subexpr45.Tree);
						DebugLocation(185, 18);
						// C:\\Users\\Gareth\\Desktop\\test.g:185:18: ( OR or_subexpr )*
						try
						{
							DebugEnterSubRule(10);
							while (true)
							{
								int alt10 = 2;
								try
								{
									DebugEnterDecision(10, decisionCanBacktrack[10]);
									try
									{
										alt10 = dfa10.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(10); }
								switch (alt10)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:185:19: OR or_subexpr
										{
											DebugLocation(185, 21);
											OR46 = (CommonToken)Match(input, OR, Follow._OR_in_expr484);
											OR46_tree = (CommonTree)adaptor.Create(OR46);
											root_0 = (CommonTree)adaptor.BecomeRoot(OR46_tree, root_0);

											DebugLocation(185, 23);
											PushFollow(Follow._or_subexpr_in_expr487);
											or_subexpr47 = or_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, or_subexpr47.Tree);

										}
										break;

									default:
										goto loop10;
								}
							}

						loop10:
							;

						}
						finally { DebugExitSubRule(10); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("expr", 7);
					LeaveRule("expr", 7);
					Leave_expr();
				}
				DebugLocation(185, 34);
			}
			finally { DebugExitRule(GrammarFileName, "expr"); }
			return retval;

		}
		// $ANTLR end "expr"

		public class or_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_or_subexpr();
		partial void Leave_or_subexpr();

		// $ANTLR start "or_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:187:1: or_subexpr : and_subexpr ( AND and_subexpr )* ;
		[GrammarRule("or_subexpr")]
		private SQLiteParser.or_subexpr_return or_subexpr()
		{
			Enter_or_subexpr();
			EnterRule("or_subexpr", 8);
			TraceIn("or_subexpr", 8);
			SQLiteParser.or_subexpr_return retval = new SQLiteParser.or_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken AND49 = null;
			SQLiteParser.and_subexpr_return and_subexpr48 = default(SQLiteParser.and_subexpr_return);
			SQLiteParser.and_subexpr_return and_subexpr50 = default(SQLiteParser.and_subexpr_return);

			CommonTree AND49_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "or_subexpr");
				DebugLocation(187, 43);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:187:11: ( and_subexpr ( AND and_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:187:13: and_subexpr ( AND and_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(187, 13);
						PushFollow(Follow._and_subexpr_in_or_subexpr496);
						and_subexpr48 = and_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, and_subexpr48.Tree);
						DebugLocation(187, 25);
						// C:\\Users\\Gareth\\Desktop\\test.g:187:25: ( AND and_subexpr )*
						try
						{
							DebugEnterSubRule(11);
							while (true)
							{
								int alt11 = 2;
								try
								{
									DebugEnterDecision(11, decisionCanBacktrack[11]);
									try
									{
										alt11 = dfa11.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(11); }
								switch (alt11)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:187:26: AND and_subexpr
										{
											DebugLocation(187, 29);
											AND49 = (CommonToken)Match(input, AND, Follow._AND_in_or_subexpr499);
											AND49_tree = (CommonTree)adaptor.Create(AND49);
											root_0 = (CommonTree)adaptor.BecomeRoot(AND49_tree, root_0);

											DebugLocation(187, 31);
											PushFollow(Follow._and_subexpr_in_or_subexpr502);
											and_subexpr50 = and_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, and_subexpr50.Tree);

										}
										break;

									default:
										goto loop11;
								}
							}

						loop11:
							;

						}
						finally { DebugExitSubRule(11); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("or_subexpr", 8);
					LeaveRule("or_subexpr", 8);
					Leave_or_subexpr();
				}
				DebugLocation(187, 43);
			}
			finally { DebugExitRule(GrammarFileName, "or_subexpr"); }
			return retval;

		}
		// $ANTLR end "or_subexpr"

		public class and_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_and_subexpr();
		partial void Leave_and_subexpr();

		// $ANTLR start "and_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:189:1: and_subexpr : eq_subexpr ( cond_expr )? ;
		[GrammarRule("and_subexpr")]
		private SQLiteParser.and_subexpr_return and_subexpr()
		{
			Enter_and_subexpr();
			EnterRule("and_subexpr", 9);
			TraceIn("and_subexpr", 9);
			SQLiteParser.and_subexpr_return retval = new SQLiteParser.and_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.eq_subexpr_return eq_subexpr51 = default(SQLiteParser.eq_subexpr_return);
			SQLiteParser.cond_expr_return cond_expr52 = default(SQLiteParser.cond_expr_return);


			try
			{
				DebugEnterRule(GrammarFileName, "and_subexpr");
				DebugLocation(189, 35);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:189:12: ( eq_subexpr ( cond_expr )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:189:14: eq_subexpr ( cond_expr )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(189, 14);
						PushFollow(Follow._eq_subexpr_in_and_subexpr511);
						eq_subexpr51 = eq_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, eq_subexpr51.Tree);
						DebugLocation(189, 34);
						// C:\\Users\\Gareth\\Desktop\\test.g:189:34: ( cond_expr )?
						int alt12 = 2;
						try
						{
							DebugEnterSubRule(12);
							try
							{
								DebugEnterDecision(12, decisionCanBacktrack[12]);
								try
								{
									alt12 = dfa12.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(12); }
							switch (alt12)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:189:34: cond_expr
									{
										DebugLocation(189, 34);
										PushFollow(Follow._cond_expr_in_and_subexpr513);
										cond_expr52 = cond_expr();
										PopFollow();

										root_0 = (CommonTree)adaptor.BecomeRoot(cond_expr52.Tree, root_0);

									}
									break;

							}
						}
						finally { DebugExitSubRule(12); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("and_subexpr", 9);
					LeaveRule("and_subexpr", 9);
					Leave_and_subexpr();
				}
				DebugLocation(189, 35);
			}
			finally { DebugExitRule(GrammarFileName, "and_subexpr"); }
			return retval;

		}
		// $ANTLR end "and_subexpr"

		public class cond_expr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_cond_expr();
		partial void Leave_cond_expr();

		// $ANTLR start "cond_expr"
		// C:\\Users\\Gareth\\Desktop\\test.g:191:1: cond_expr : ( ( NOT )? match_op match_expr= eq_subexpr ( ESCAPE escape_expr= eq_subexpr )? -> ^( match_op $match_expr ( NOT )? ( ^( ESCAPE $escape_expr) )? ) | ( NOT )? IN LPAREN expr ( COMMA expr )* RPAREN -> ^( IN_VALUES ( NOT )? ^( IN ( expr )+ ) ) | ( NOT )? IN (database_name= id DOT )? table_name= id -> ^( IN_TABLE ( NOT )? ^( IN ^( $table_name ( $database_name)? ) ) ) | ( ISNULL -> IS_NULL | NOTNULL -> NOT_NULL | IS NULL -> IS_NULL | NOT NULL -> NOT_NULL | IS NOT NULL -> NOT_NULL ) | ( NOT )? BETWEEN e1= eq_subexpr AND e2= eq_subexpr -> ^( BETWEEN ( NOT )? ^( AND $e1 $e2) ) | ( ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr )+ );
		[GrammarRule("cond_expr")]
		private SQLiteParser.cond_expr_return cond_expr()
		{
			Enter_cond_expr();
			EnterRule("cond_expr", 10);
			TraceIn("cond_expr", 10);
			SQLiteParser.cond_expr_return retval = new SQLiteParser.cond_expr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken NOT53 = null;
			CommonToken ESCAPE55 = null;
			CommonToken NOT56 = null;
			CommonToken IN57 = null;
			CommonToken LPAREN58 = null;
			CommonToken COMMA60 = null;
			CommonToken RPAREN62 = null;
			CommonToken NOT63 = null;
			CommonToken IN64 = null;
			CommonToken DOT65 = null;
			CommonToken ISNULL66 = null;
			CommonToken NOTNULL67 = null;
			CommonToken IS68 = null;
			CommonToken NULL69 = null;
			CommonToken NOT70 = null;
			CommonToken NULL71 = null;
			CommonToken IS72 = null;
			CommonToken NOT73 = null;
			CommonToken NULL74 = null;
			CommonToken NOT75 = null;
			CommonToken BETWEEN76 = null;
			CommonToken AND77 = null;
			CommonToken set78 = null;
			SQLiteParser.eq_subexpr_return match_expr = default(SQLiteParser.eq_subexpr_return);
			SQLiteParser.eq_subexpr_return escape_expr = default(SQLiteParser.eq_subexpr_return);
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.eq_subexpr_return e1 = default(SQLiteParser.eq_subexpr_return);
			SQLiteParser.eq_subexpr_return e2 = default(SQLiteParser.eq_subexpr_return);
			SQLiteParser.match_op_return match_op54 = default(SQLiteParser.match_op_return);
			SQLiteParser.expr_return expr59 = default(SQLiteParser.expr_return);
			SQLiteParser.expr_return expr61 = default(SQLiteParser.expr_return);
			SQLiteParser.eq_subexpr_return eq_subexpr79 = default(SQLiteParser.eq_subexpr_return);

			CommonTree NOT53_tree = null;
			CommonTree ESCAPE55_tree = null;
			CommonTree NOT56_tree = null;
			CommonTree IN57_tree = null;
			CommonTree LPAREN58_tree = null;
			CommonTree COMMA60_tree = null;
			CommonTree RPAREN62_tree = null;
			CommonTree NOT63_tree = null;
			CommonTree IN64_tree = null;
			CommonTree DOT65_tree = null;
			CommonTree ISNULL66_tree = null;
			CommonTree NOTNULL67_tree = null;
			CommonTree IS68_tree = null;
			CommonTree NULL69_tree = null;
			CommonTree NOT70_tree = null;
			CommonTree NULL71_tree = null;
			CommonTree IS72_tree = null;
			CommonTree NOT73_tree = null;
			CommonTree NULL74_tree = null;
			CommonTree NOT75_tree = null;
			CommonTree BETWEEN76_tree = null;
			CommonTree AND77_tree = null;
			CommonTree set78_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_IN = new RewriteRuleITokenStream(adaptor, "token IN");
			RewriteRuleITokenStream stream_ESCAPE = new RewriteRuleITokenStream(adaptor, "token ESCAPE");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_IS = new RewriteRuleITokenStream(adaptor, "token IS");
			RewriteRuleITokenStream stream_NULL = new RewriteRuleITokenStream(adaptor, "token NULL");
			RewriteRuleITokenStream stream_ISNULL = new RewriteRuleITokenStream(adaptor, "token ISNULL");
			RewriteRuleITokenStream stream_NOT = new RewriteRuleITokenStream(adaptor, "token NOT");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_NOTNULL = new RewriteRuleITokenStream(adaptor, "token NOTNULL");
			RewriteRuleITokenStream stream_AND = new RewriteRuleITokenStream(adaptor, "token AND");
			RewriteRuleITokenStream stream_BETWEEN = new RewriteRuleITokenStream(adaptor, "token BETWEEN");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_expr = new RewriteRuleSubtreeStream(adaptor, "rule expr");
			RewriteRuleSubtreeStream stream_match_op = new RewriteRuleSubtreeStream(adaptor, "rule match_op");
			RewriteRuleSubtreeStream stream_eq_subexpr = new RewriteRuleSubtreeStream(adaptor, "rule eq_subexpr");
			try
			{
				DebugEnterRule(GrammarFileName, "cond_expr");
				DebugLocation(191, 2);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:192:3: ( ( NOT )? match_op match_expr= eq_subexpr ( ESCAPE escape_expr= eq_subexpr )? -> ^( match_op $match_expr ( NOT )? ( ^( ESCAPE $escape_expr) )? ) | ( NOT )? IN LPAREN expr ( COMMA expr )* RPAREN -> ^( IN_VALUES ( NOT )? ^( IN ( expr )+ ) ) | ( NOT )? IN (database_name= id DOT )? table_name= id -> ^( IN_TABLE ( NOT )? ^( IN ^( $table_name ( $database_name)? ) ) ) | ( ISNULL -> IS_NULL | NOTNULL -> NOT_NULL | IS NULL -> IS_NULL | NOT NULL -> NOT_NULL | IS NOT NULL -> NOT_NULL ) | ( NOT )? BETWEEN e1= eq_subexpr AND e2= eq_subexpr -> ^( BETWEEN ( NOT )? ^( AND $e1 $e2) ) | ( ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr )+ )
					int alt22 = 6;
					try
					{
						DebugEnterDecision(22, decisionCanBacktrack[22]);
						try
						{
							alt22 = dfa22.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(22); }
					switch (alt22)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:192:5: ( NOT )? match_op match_expr= eq_subexpr ( ESCAPE escape_expr= eq_subexpr )?
							{
								DebugLocation(192, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:192:5: ( NOT )?
								int alt13 = 2;
								try
								{
									DebugEnterSubRule(13);
									try
									{
										DebugEnterDecision(13, decisionCanBacktrack[13]);
										int LA13_0 = input.LA(1);

										if ((LA13_0 == NOT))
										{
											alt13 = 1;
										}
									}
									finally { DebugExitDecision(13); }
									switch (alt13)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:192:5: NOT
											{
												DebugLocation(192, 5);
												NOT53 = (CommonToken)Match(input, NOT, Follow._NOT_in_cond_expr525);
												stream_NOT.Add(NOT53);


											}
											break;

									}
								}
								finally { DebugExitSubRule(13); }

								DebugLocation(192, 10);
								PushFollow(Follow._match_op_in_cond_expr528);
								match_op54 = match_op();
								PopFollow();

								stream_match_op.Add(match_op54.Tree);
								DebugLocation(192, 29);
								PushFollow(Follow._eq_subexpr_in_cond_expr532);
								match_expr = eq_subexpr();
								PopFollow();

								stream_eq_subexpr.Add(match_expr.Tree);
								DebugLocation(192, 41);
								// C:\\Users\\Gareth\\Desktop\\test.g:192:41: ( ESCAPE escape_expr= eq_subexpr )?
								int alt14 = 2;
								try
								{
									DebugEnterSubRule(14);
									try
									{
										DebugEnterDecision(14, decisionCanBacktrack[14]);
										try
										{
											alt14 = dfa14.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(14); }
									switch (alt14)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:192:42: ESCAPE escape_expr= eq_subexpr
											{
												DebugLocation(192, 42);
												ESCAPE55 = (CommonToken)Match(input, ESCAPE, Follow._ESCAPE_in_cond_expr535);
												stream_ESCAPE.Add(ESCAPE55);

												DebugLocation(192, 60);
												PushFollow(Follow._eq_subexpr_in_cond_expr539);
												escape_expr = eq_subexpr();
												PopFollow();

												stream_eq_subexpr.Add(escape_expr.Tree);

											}
											break;

									}
								}
								finally { DebugExitSubRule(14); }



								{
									// AST REWRITE
									// elements: match_op, ESCAPE, match_expr, escape_expr, NOT
									// token labels: 
									// rule labels: retval, match_expr, escape_expr
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_match_expr = new RewriteRuleSubtreeStream(adaptor, "rule match_expr", match_expr != null ? match_expr.Tree : null);
									RewriteRuleSubtreeStream stream_escape_expr = new RewriteRuleSubtreeStream(adaptor, "rule escape_expr", escape_expr != null ? escape_expr.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 192:74: -> ^( match_op $match_expr ( NOT )? ( ^( ESCAPE $escape_expr) )? )
									{
										DebugLocation(192, 77);
										// C:\\Users\\Gareth\\Desktop\\test.g:192:77: ^( match_op $match_expr ( NOT )? ( ^( ESCAPE $escape_expr) )? )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(192, 79);
											root_1 = (CommonTree)adaptor.BecomeRoot(stream_match_op.NextNode(), root_1);

											DebugLocation(192, 88);
											adaptor.AddChild(root_1, stream_match_expr.NextTree());
											DebugLocation(192, 100);
											// C:\\Users\\Gareth\\Desktop\\test.g:192:100: ( NOT )?
											if (stream_NOT.HasNext)
											{
												DebugLocation(192, 100);
												adaptor.AddChild(root_1, stream_NOT.NextNode());

											}
											stream_NOT.Reset();
											DebugLocation(192, 105);
											// C:\\Users\\Gareth\\Desktop\\test.g:192:105: ( ^( ESCAPE $escape_expr) )?
											if (stream_ESCAPE.HasNext || stream_escape_expr.HasNext)
											{
												DebugLocation(192, 105);
												// C:\\Users\\Gareth\\Desktop\\test.g:192:105: ^( ESCAPE $escape_expr)
												{
													CommonTree root_2 = (CommonTree)adaptor.Nil();
													DebugLocation(192, 107);
													root_2 = (CommonTree)adaptor.BecomeRoot(stream_ESCAPE.NextNode(), root_2);

													DebugLocation(192, 114);
													adaptor.AddChild(root_2, stream_escape_expr.NextTree());

													adaptor.AddChild(root_1, root_2);
												}

											}
											stream_ESCAPE.Reset();
											stream_escape_expr.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:193:5: ( NOT )? IN LPAREN expr ( COMMA expr )* RPAREN
							{
								DebugLocation(193, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:193:5: ( NOT )?
								int alt15 = 2;
								try
								{
									DebugEnterSubRule(15);
									try
									{
										DebugEnterDecision(15, decisionCanBacktrack[15]);
										int LA15_0 = input.LA(1);

										if ((LA15_0 == NOT))
										{
											alt15 = 1;
										}
									}
									finally { DebugExitDecision(15); }
									switch (alt15)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:193:5: NOT
											{
												DebugLocation(193, 5);
												NOT56 = (CommonToken)Match(input, NOT, Follow._NOT_in_cond_expr567);
												stream_NOT.Add(NOT56);


											}
											break;

									}
								}
								finally { DebugExitSubRule(15); }

								DebugLocation(193, 10);
								IN57 = (CommonToken)Match(input, IN, Follow._IN_in_cond_expr570);
								stream_IN.Add(IN57);

								DebugLocation(193, 13);
								LPAREN58 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_cond_expr572);
								stream_LPAREN.Add(LPAREN58);

								DebugLocation(193, 20);
								PushFollow(Follow._expr_in_cond_expr574);
								expr59 = expr();
								PopFollow();

								stream_expr.Add(expr59.Tree);
								DebugLocation(193, 25);
								// C:\\Users\\Gareth\\Desktop\\test.g:193:25: ( COMMA expr )*
								try
								{
									DebugEnterSubRule(16);
									while (true)
									{
										int alt16 = 2;
										try
										{
											DebugEnterDecision(16, decisionCanBacktrack[16]);
											int LA16_0 = input.LA(1);

											if ((LA16_0 == COMMA))
											{
												alt16 = 1;
											}


										}
										finally { DebugExitDecision(16); }
										switch (alt16)
										{
											case 1:
												DebugEnterAlt(1);
												// C:\\Users\\Gareth\\Desktop\\test.g:193:26: COMMA expr
												{
													DebugLocation(193, 26);
													COMMA60 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_cond_expr577);
													stream_COMMA.Add(COMMA60);

													DebugLocation(193, 32);
													PushFollow(Follow._expr_in_cond_expr579);
													expr61 = expr();
													PopFollow();

													stream_expr.Add(expr61.Tree);

												}
												break;

											default:
												goto loop16;
										}
									}

								loop16:
									;

								}
								finally { DebugExitSubRule(16); }

								DebugLocation(193, 39);
								RPAREN62 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_cond_expr583);
								stream_RPAREN.Add(RPAREN62);



								{
									// AST REWRITE
									// elements: expr, NOT, IN
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 193:46: -> ^( IN_VALUES ( NOT )? ^( IN ( expr )+ ) )
									{
										DebugLocation(193, 49);
										// C:\\Users\\Gareth\\Desktop\\test.g:193:49: ^( IN_VALUES ( NOT )? ^( IN ( expr )+ ) )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(193, 51);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(IN_VALUES, "IN_VALUES"), root_1);

											DebugLocation(193, 61);
											// C:\\Users\\Gareth\\Desktop\\test.g:193:61: ( NOT )?
											if (stream_NOT.HasNext)
											{
												DebugLocation(193, 61);
												adaptor.AddChild(root_1, stream_NOT.NextNode());

											}
											stream_NOT.Reset();
											DebugLocation(193, 66);
											// C:\\Users\\Gareth\\Desktop\\test.g:193:66: ^( IN ( expr )+ )
											{
												CommonTree root_2 = (CommonTree)adaptor.Nil();
												DebugLocation(193, 68);
												root_2 = (CommonTree)adaptor.BecomeRoot(stream_IN.NextNode(), root_2);

												DebugLocation(193, 71);
												if (!(stream_expr.HasNext))
												{
													throw new RewriteEarlyExitException();
												}
												while (stream_expr.HasNext)
												{
													DebugLocation(193, 71);
													adaptor.AddChild(root_2, stream_expr.NextTree());

												}
												stream_expr.Reset();

												adaptor.AddChild(root_1, root_2);
											}

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:194:5: ( NOT )? IN (database_name= id DOT )? table_name= id
							{
								DebugLocation(194, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:194:5: ( NOT )?
								int alt17 = 2;
								try
								{
									DebugEnterSubRule(17);
									try
									{
										DebugEnterDecision(17, decisionCanBacktrack[17]);
										int LA17_0 = input.LA(1);

										if ((LA17_0 == NOT))
										{
											alt17 = 1;
										}
									}
									finally { DebugExitDecision(17); }
									switch (alt17)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:194:5: NOT
											{
												DebugLocation(194, 5);
												NOT63 = (CommonToken)Match(input, NOT, Follow._NOT_in_cond_expr605);
												stream_NOT.Add(NOT63);


											}
											break;

									}
								}
								finally { DebugExitSubRule(17); }

								DebugLocation(194, 10);
								IN64 = (CommonToken)Match(input, IN, Follow._IN_in_cond_expr608);
								stream_IN.Add(IN64);

								DebugLocation(194, 13);
								// C:\\Users\\Gareth\\Desktop\\test.g:194:13: (database_name= id DOT )?
								int alt18 = 2;
								try
								{
									DebugEnterSubRule(18);
									try
									{
										DebugEnterDecision(18, decisionCanBacktrack[18]);
										try
										{
											alt18 = dfa18.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(18); }
									switch (alt18)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:194:14: database_name= id DOT
											{
												DebugLocation(194, 27);
												PushFollow(Follow._id_in_cond_expr613);
												database_name = id();
												PopFollow();

												stream_id.Add(database_name.Tree);
												DebugLocation(194, 31);
												DOT65 = (CommonToken)Match(input, DOT, Follow._DOT_in_cond_expr615);
												stream_DOT.Add(DOT65);


											}
											break;

									}
								}
								finally { DebugExitSubRule(18); }

								DebugLocation(194, 47);
								PushFollow(Follow._id_in_cond_expr621);
								table_name = id();
								PopFollow();

								stream_id.Add(table_name.Tree);


								{
									// AST REWRITE
									// elements: table_name, NOT, database_name, IN
									// token labels: 
									// rule labels: database_name, retval, table_name
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 194:51: -> ^( IN_TABLE ( NOT )? ^( IN ^( $table_name ( $database_name)? ) ) )
									{
										DebugLocation(194, 54);
										// C:\\Users\\Gareth\\Desktop\\test.g:194:54: ^( IN_TABLE ( NOT )? ^( IN ^( $table_name ( $database_name)? ) ) )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(194, 56);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(IN_TABLE, "IN_TABLE"), root_1);

											DebugLocation(194, 65);
											// C:\\Users\\Gareth\\Desktop\\test.g:194:65: ( NOT )?
											if (stream_NOT.HasNext)
											{
												DebugLocation(194, 65);
												adaptor.AddChild(root_1, stream_NOT.NextNode());

											}
											stream_NOT.Reset();
											DebugLocation(194, 70);
											// C:\\Users\\Gareth\\Desktop\\test.g:194:70: ^( IN ^( $table_name ( $database_name)? ) )
											{
												CommonTree root_2 = (CommonTree)adaptor.Nil();
												DebugLocation(194, 72);
												root_2 = (CommonTree)adaptor.BecomeRoot(stream_IN.NextNode(), root_2);

												DebugLocation(194, 75);
												// C:\\Users\\Gareth\\Desktop\\test.g:194:75: ^( $table_name ( $database_name)? )
												{
													CommonTree root_3 = (CommonTree)adaptor.Nil();
													DebugLocation(194, 77);
													root_3 = (CommonTree)adaptor.BecomeRoot(stream_table_name.NextNode(), root_3);

													DebugLocation(194, 89);
													// C:\\Users\\Gareth\\Desktop\\test.g:194:89: ( $database_name)?
													if (stream_database_name.HasNext)
													{
														DebugLocation(194, 89);
														adaptor.AddChild(root_3, stream_database_name.NextTree());

													}
													stream_database_name.Reset();

													adaptor.AddChild(root_2, root_3);
												}

												adaptor.AddChild(root_1, root_2);
											}

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 4:
							DebugEnterAlt(4);
							// C:\\Users\\Gareth\\Desktop\\test.g:197:5: ( ISNULL -> IS_NULL | NOTNULL -> NOT_NULL | IS NULL -> IS_NULL | NOT NULL -> NOT_NULL | IS NOT NULL -> NOT_NULL )
							{
								DebugLocation(197, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:197:5: ( ISNULL -> IS_NULL | NOTNULL -> NOT_NULL | IS NULL -> IS_NULL | NOT NULL -> NOT_NULL | IS NOT NULL -> NOT_NULL )
								int alt19 = 5;
								try
								{
									DebugEnterSubRule(19);
									try
									{
										DebugEnterDecision(19, decisionCanBacktrack[19]);
										switch (input.LA(1))
										{
											case ISNULL:
												{
													alt19 = 1;
												}
												break;
											case NOTNULL:
												{
													alt19 = 2;
												}
												break;
											case IS:
												{
													int LA19_3 = input.LA(2);

													if ((LA19_3 == NULL))
													{
														alt19 = 3;
													}
													else if ((LA19_3 == NOT))
													{
														alt19 = 5;
													}
													else
													{
														NoViableAltException nvae = new NoViableAltException("", 19, 3, input);

														DebugRecognitionException(nvae);
														throw nvae;
													}
												}
												break;
											case NOT:
												{
													alt19 = 4;
												}
												break;
											default:
												{
													NoViableAltException nvae = new NoViableAltException("", 19, 0, input);

													DebugRecognitionException(nvae);
													throw nvae;
												}
										}

									}
									finally { DebugExitDecision(19); }
									switch (alt19)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:197:6: ISNULL
											{
												DebugLocation(197, 6);
												ISNULL66 = (CommonToken)Match(input, ISNULL, Follow._ISNULL_in_cond_expr652);
												stream_ISNULL.Add(ISNULL66);



												{
													// AST REWRITE
													// elements: 
													// token labels: 
													// rule labels: retval
													// token list labels: 
													// rule list labels: 
													// wildcard labels: 
													retval.Tree = root_0;
													RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

													root_0 = (CommonTree)adaptor.Nil();
													// 197:13: -> IS_NULL
													{
														DebugLocation(197, 16);
														adaptor.AddChild(root_0, (CommonTree)adaptor.Create(IS_NULL, "IS_NULL"));

													}

													retval.Tree = root_0;
												}

											}
											break;
										case 2:
											DebugEnterAlt(2);
											// C:\\Users\\Gareth\\Desktop\\test.g:197:26: NOTNULL
											{
												DebugLocation(197, 26);
												NOTNULL67 = (CommonToken)Match(input, NOTNULL, Follow._NOTNULL_in_cond_expr660);
												stream_NOTNULL.Add(NOTNULL67);



												{
													// AST REWRITE
													// elements: 
													// token labels: 
													// rule labels: retval
													// token list labels: 
													// rule list labels: 
													// wildcard labels: 
													retval.Tree = root_0;
													RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

													root_0 = (CommonTree)adaptor.Nil();
													// 197:34: -> NOT_NULL
													{
														DebugLocation(197, 37);
														adaptor.AddChild(root_0, (CommonTree)adaptor.Create(NOT_NULL, "NOT_NULL"));

													}

													retval.Tree = root_0;
												}

											}
											break;
										case 3:
											DebugEnterAlt(3);
											// C:\\Users\\Gareth\\Desktop\\test.g:197:48: IS NULL
											{
												DebugLocation(197, 48);
												IS68 = (CommonToken)Match(input, IS, Follow._IS_in_cond_expr668);
												stream_IS.Add(IS68);

												DebugLocation(197, 51);
												NULL69 = (CommonToken)Match(input, NULL, Follow._NULL_in_cond_expr670);
												stream_NULL.Add(NULL69);



												{
													// AST REWRITE
													// elements: 
													// token labels: 
													// rule labels: retval
													// token list labels: 
													// rule list labels: 
													// wildcard labels: 
													retval.Tree = root_0;
													RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

													root_0 = (CommonTree)adaptor.Nil();
													// 197:56: -> IS_NULL
													{
														DebugLocation(197, 59);
														adaptor.AddChild(root_0, (CommonTree)adaptor.Create(IS_NULL, "IS_NULL"));

													}

													retval.Tree = root_0;
												}

											}
											break;
										case 4:
											DebugEnterAlt(4);
											// C:\\Users\\Gareth\\Desktop\\test.g:197:69: NOT NULL
											{
												DebugLocation(197, 69);
												NOT70 = (CommonToken)Match(input, NOT, Follow._NOT_in_cond_expr678);
												stream_NOT.Add(NOT70);

												DebugLocation(197, 73);
												NULL71 = (CommonToken)Match(input, NULL, Follow._NULL_in_cond_expr680);
												stream_NULL.Add(NULL71);



												{
													// AST REWRITE
													// elements: 
													// token labels: 
													// rule labels: retval
													// token list labels: 
													// rule list labels: 
													// wildcard labels: 
													retval.Tree = root_0;
													RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

													root_0 = (CommonTree)adaptor.Nil();
													// 197:78: -> NOT_NULL
													{
														DebugLocation(197, 81);
														adaptor.AddChild(root_0, (CommonTree)adaptor.Create(NOT_NULL, "NOT_NULL"));

													}

													retval.Tree = root_0;
												}

											}
											break;
										case 5:
											DebugEnterAlt(5);
											// C:\\Users\\Gareth\\Desktop\\test.g:197:92: IS NOT NULL
											{
												DebugLocation(197, 92);
												IS72 = (CommonToken)Match(input, IS, Follow._IS_in_cond_expr688);
												stream_IS.Add(IS72);

												DebugLocation(197, 95);
												NOT73 = (CommonToken)Match(input, NOT, Follow._NOT_in_cond_expr690);
												stream_NOT.Add(NOT73);

												DebugLocation(197, 99);
												NULL74 = (CommonToken)Match(input, NULL, Follow._NULL_in_cond_expr692);
												stream_NULL.Add(NULL74);



												{
													// AST REWRITE
													// elements: 
													// token labels: 
													// rule labels: retval
													// token list labels: 
													// rule list labels: 
													// wildcard labels: 
													retval.Tree = root_0;
													RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

													root_0 = (CommonTree)adaptor.Nil();
													// 197:104: -> NOT_NULL
													{
														DebugLocation(197, 107);
														adaptor.AddChild(root_0, (CommonTree)adaptor.Create(NOT_NULL, "NOT_NULL"));

													}

													retval.Tree = root_0;
												}

											}
											break;

									}
								}
								finally { DebugExitSubRule(19); }


							}
							break;
						case 5:
							DebugEnterAlt(5);
							// C:\\Users\\Gareth\\Desktop\\test.g:198:5: ( NOT )? BETWEEN e1= eq_subexpr AND e2= eq_subexpr
							{
								DebugLocation(198, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:198:5: ( NOT )?
								int alt20 = 2;
								try
								{
									DebugEnterSubRule(20);
									try
									{
										DebugEnterDecision(20, decisionCanBacktrack[20]);
										int LA20_0 = input.LA(1);

										if ((LA20_0 == NOT))
										{
											alt20 = 1;
										}
									}
									finally { DebugExitDecision(20); }
									switch (alt20)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:198:5: NOT
											{
												DebugLocation(198, 5);
												NOT75 = (CommonToken)Match(input, NOT, Follow._NOT_in_cond_expr703);
												stream_NOT.Add(NOT75);


											}
											break;

									}
								}
								finally { DebugExitSubRule(20); }

								DebugLocation(198, 10);
								BETWEEN76 = (CommonToken)Match(input, BETWEEN, Follow._BETWEEN_in_cond_expr706);
								stream_BETWEEN.Add(BETWEEN76);

								DebugLocation(198, 20);
								PushFollow(Follow._eq_subexpr_in_cond_expr710);
								e1 = eq_subexpr();
								PopFollow();

								stream_eq_subexpr.Add(e1.Tree);
								DebugLocation(198, 32);
								AND77 = (CommonToken)Match(input, AND, Follow._AND_in_cond_expr712);
								stream_AND.Add(AND77);

								DebugLocation(198, 38);
								PushFollow(Follow._eq_subexpr_in_cond_expr716);
								e2 = eq_subexpr();
								PopFollow();

								stream_eq_subexpr.Add(e2.Tree);


								{
									// AST REWRITE
									// elements: BETWEEN, e2, AND, e1, NOT
									// token labels: 
									// rule labels: retval, e1, e2
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_e1 = new RewriteRuleSubtreeStream(adaptor, "rule e1", e1 != null ? e1.Tree : null);
									RewriteRuleSubtreeStream stream_e2 = new RewriteRuleSubtreeStream(adaptor, "rule e2", e2 != null ? e2.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 198:50: -> ^( BETWEEN ( NOT )? ^( AND $e1 $e2) )
									{
										DebugLocation(198, 53);
										// C:\\Users\\Gareth\\Desktop\\test.g:198:53: ^( BETWEEN ( NOT )? ^( AND $e1 $e2) )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(198, 55);
											root_1 = (CommonTree)adaptor.BecomeRoot(stream_BETWEEN.NextNode(), root_1);

											DebugLocation(198, 63);
											// C:\\Users\\Gareth\\Desktop\\test.g:198:63: ( NOT )?
											if (stream_NOT.HasNext)
											{
												DebugLocation(198, 63);
												adaptor.AddChild(root_1, stream_NOT.NextNode());

											}
											stream_NOT.Reset();
											DebugLocation(198, 68);
											// C:\\Users\\Gareth\\Desktop\\test.g:198:68: ^( AND $e1 $e2)
											{
												CommonTree root_2 = (CommonTree)adaptor.Nil();
												DebugLocation(198, 70);
												root_2 = (CommonTree)adaptor.BecomeRoot(stream_AND.NextNode(), root_2);

												DebugLocation(198, 74);
												adaptor.AddChild(root_2, stream_e1.NextTree());
												DebugLocation(198, 78);
												adaptor.AddChild(root_2, stream_e2.NextTree());

												adaptor.AddChild(root_1, root_2);
											}

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 6:
							DebugEnterAlt(6);
							// C:\\Users\\Gareth\\Desktop\\test.g:199:5: ( ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr )+
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(199, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:199:5: ( ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr )+
								int cnt21 = 0;
								try
								{
									DebugEnterSubRule(21);
									while (true)
									{
										int alt21 = 2;
										try
										{
											DebugEnterDecision(21, decisionCanBacktrack[21]);
											try
											{
												alt21 = dfa21.Predict(input);
											}
											catch (NoViableAltException nvae)
											{
												DebugRecognitionException(nvae);
												throw;
											}
										}
										finally { DebugExitDecision(21); }
										switch (alt21)
										{
											case 1:
												DebugEnterAlt(1);
												// C:\\Users\\Gareth\\Desktop\\test.g:199:6: ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr
												{
													DebugLocation(199, 6);
													set78 = (CommonToken)input.LT(1);
													set78 = (CommonToken)input.LT(1);
													if ((input.LA(1) >= EQUALS && input.LA(1) <= NOT_EQUALS2))
													{
														input.Consume();
														root_0 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(set78), root_0);
														state.errorRecovery = false;
													}
													else
													{
														MismatchedSetException mse = new MismatchedSetException(null, input);
														DebugRecognitionException(mse);
														throw mse;
													}

													DebugLocation(199, 53);
													PushFollow(Follow._eq_subexpr_in_cond_expr759);
													eq_subexpr79 = eq_subexpr();
													PopFollow();

													adaptor.AddChild(root_0, eq_subexpr79.Tree);

												}
												break;

											default:
												if (cnt21 >= 1)
													goto loop21;

												EarlyExitException eee21 = new EarlyExitException(21, input);
												DebugRecognitionException(eee21);
												throw eee21;
										}
										cnt21++;
									}
								loop21:
									;

								}
								finally { DebugExitSubRule(21); }


							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("cond_expr", 10);
					LeaveRule("cond_expr", 10);
					Leave_cond_expr();
				}
				DebugLocation(200, 2);
			}
			finally { DebugExitRule(GrammarFileName, "cond_expr"); }
			return retval;

		}
		// $ANTLR end "cond_expr"

		public class match_op_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_match_op();
		partial void Leave_match_op();

		// $ANTLR start "match_op"
		// C:\\Users\\Gareth\\Desktop\\test.g:202:1: match_op : ( LIKE | GLOB | REGEXP | MATCH );
		[GrammarRule("match_op")]
		private SQLiteParser.match_op_return match_op()
		{
			Enter_match_op();
			EnterRule("match_op", 11);
			TraceIn("match_op", 11);
			SQLiteParser.match_op_return retval = new SQLiteParser.match_op_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set80 = null;

			CommonTree set80_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "match_op");
				DebugLocation(202, 38);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:202:9: ( LIKE | GLOB | REGEXP | MATCH )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(202, 9);
						set80 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= LIKE && input.LA(1) <= MATCH))
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set80));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("match_op", 11);
					LeaveRule("match_op", 11);
					Leave_match_op();
				}
				DebugLocation(202, 38);
			}
			finally { DebugExitRule(GrammarFileName, "match_op"); }
			return retval;

		}
		// $ANTLR end "match_op"

		public class eq_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_eq_subexpr();
		partial void Leave_eq_subexpr();

		// $ANTLR start "eq_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:204:1: eq_subexpr : neq_subexpr ( ( LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ ) neq_subexpr )* ;
		[GrammarRule("eq_subexpr")]
		private SQLiteParser.eq_subexpr_return eq_subexpr()
		{
			Enter_eq_subexpr();
			EnterRule("eq_subexpr", 12);
			TraceIn("eq_subexpr", 12);
			SQLiteParser.eq_subexpr_return retval = new SQLiteParser.eq_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set82 = null;
			SQLiteParser.neq_subexpr_return neq_subexpr81 = default(SQLiteParser.neq_subexpr_return);
			SQLiteParser.neq_subexpr_return neq_subexpr83 = default(SQLiteParser.neq_subexpr_return);

			CommonTree set82_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "eq_subexpr");
				DebugLocation(204, 85);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:204:11: ( neq_subexpr ( ( LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ ) neq_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:204:13: neq_subexpr ( ( LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ ) neq_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(204, 13);
						PushFollow(Follow._neq_subexpr_in_eq_subexpr792);
						neq_subexpr81 = neq_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, neq_subexpr81.Tree);
						DebugLocation(204, 25);
						// C:\\Users\\Gareth\\Desktop\\test.g:204:25: ( ( LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ ) neq_subexpr )*
						try
						{
							DebugEnterSubRule(23);
							while (true)
							{
								int alt23 = 2;
								try
								{
									DebugEnterDecision(23, decisionCanBacktrack[23]);
									try
									{
										alt23 = dfa23.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(23); }
								switch (alt23)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:204:26: ( LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ ) neq_subexpr
										{
											DebugLocation(204, 26);
											set82 = (CommonToken)input.LT(1);
											set82 = (CommonToken)input.LT(1);
											if ((input.LA(1) >= LESS && input.LA(1) <= GREATER_OR_EQ))
											{
												input.Consume();
												root_0 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(set82), root_0);
												state.errorRecovery = false;
											}
											else
											{
												MismatchedSetException mse = new MismatchedSetException(null, input);
												DebugRecognitionException(mse);
												throw mse;
											}

											DebugLocation(204, 73);
											PushFollow(Follow._neq_subexpr_in_eq_subexpr812);
											neq_subexpr83 = neq_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, neq_subexpr83.Tree);

										}
										break;

									default:
										goto loop23;
								}
							}

						loop23:
							;

						}
						finally { DebugExitSubRule(23); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("eq_subexpr", 12);
					LeaveRule("eq_subexpr", 12);
					Leave_eq_subexpr();
				}
				DebugLocation(204, 85);
			}
			finally { DebugExitRule(GrammarFileName, "eq_subexpr"); }
			return retval;

		}
		// $ANTLR end "eq_subexpr"

		public class neq_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_neq_subexpr();
		partial void Leave_neq_subexpr();

		// $ANTLR start "neq_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:206:1: neq_subexpr : bit_subexpr ( ( SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE ) bit_subexpr )* ;
		[GrammarRule("neq_subexpr")]
		private SQLiteParser.neq_subexpr_return neq_subexpr()
		{
			Enter_neq_subexpr();
			EnterRule("neq_subexpr", 13);
			TraceIn("neq_subexpr", 13);
			SQLiteParser.neq_subexpr_return retval = new SQLiteParser.neq_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set85 = null;
			SQLiteParser.bit_subexpr_return bit_subexpr84 = default(SQLiteParser.bit_subexpr_return);
			SQLiteParser.bit_subexpr_return bit_subexpr86 = default(SQLiteParser.bit_subexpr_return);

			CommonTree set85_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "neq_subexpr");
				DebugLocation(206, 86);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:206:12: ( bit_subexpr ( ( SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE ) bit_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:206:14: bit_subexpr ( ( SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE ) bit_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(206, 14);
						PushFollow(Follow._bit_subexpr_in_neq_subexpr821);
						bit_subexpr84 = bit_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, bit_subexpr84.Tree);
						DebugLocation(206, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:206:26: ( ( SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE ) bit_subexpr )*
						try
						{
							DebugEnterSubRule(24);
							while (true)
							{
								int alt24 = 2;
								try
								{
									DebugEnterDecision(24, decisionCanBacktrack[24]);
									try
									{
										alt24 = dfa24.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(24); }
								switch (alt24)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:206:27: ( SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE ) bit_subexpr
										{
											DebugLocation(206, 27);
											set85 = (CommonToken)input.LT(1);
											set85 = (CommonToken)input.LT(1);
											if ((input.LA(1) >= SHIFT_LEFT && input.LA(1) <= PIPE))
											{
												input.Consume();
												root_0 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(set85), root_0);
												state.errorRecovery = false;
											}
											else
											{
												MismatchedSetException mse = new MismatchedSetException(null, input);
												DebugRecognitionException(mse);
												throw mse;
											}

											DebugLocation(206, 74);
											PushFollow(Follow._bit_subexpr_in_neq_subexpr841);
											bit_subexpr86 = bit_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, bit_subexpr86.Tree);

										}
										break;

									default:
										goto loop24;
								}
							}

						loop24:
							;

						}
						finally { DebugExitSubRule(24); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("neq_subexpr", 13);
					LeaveRule("neq_subexpr", 13);
					Leave_neq_subexpr();
				}
				DebugLocation(206, 86);
			}
			finally { DebugExitRule(GrammarFileName, "neq_subexpr"); }
			return retval;

		}
		// $ANTLR end "neq_subexpr"

		public class bit_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_bit_subexpr();
		partial void Leave_bit_subexpr();

		// $ANTLR start "bit_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:208:1: bit_subexpr : add_subexpr ( ( PLUS | MINUS ) add_subexpr )* ;
		[GrammarRule("bit_subexpr")]
		private SQLiteParser.bit_subexpr_return bit_subexpr()
		{
			Enter_bit_subexpr();
			EnterRule("bit_subexpr", 14);
			TraceIn("bit_subexpr", 14);
			SQLiteParser.bit_subexpr_return retval = new SQLiteParser.bit_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set88 = null;
			SQLiteParser.add_subexpr_return add_subexpr87 = default(SQLiteParser.add_subexpr_return);
			SQLiteParser.add_subexpr_return add_subexpr89 = default(SQLiteParser.add_subexpr_return);

			CommonTree set88_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "bit_subexpr");
				DebugLocation(208, 55);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:208:12: ( add_subexpr ( ( PLUS | MINUS ) add_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:208:14: add_subexpr ( ( PLUS | MINUS ) add_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(208, 14);
						PushFollow(Follow._add_subexpr_in_bit_subexpr850);
						add_subexpr87 = add_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, add_subexpr87.Tree);
						DebugLocation(208, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:208:26: ( ( PLUS | MINUS ) add_subexpr )*
						try
						{
							DebugEnterSubRule(25);
							while (true)
							{
								int alt25 = 2;
								try
								{
									DebugEnterDecision(25, decisionCanBacktrack[25]);
									try
									{
										alt25 = dfa25.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(25); }
								switch (alt25)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:208:27: ( PLUS | MINUS ) add_subexpr
										{
											DebugLocation(208, 27);
											set88 = (CommonToken)input.LT(1);
											set88 = (CommonToken)input.LT(1);
											if ((input.LA(1) >= PLUS && input.LA(1) <= MINUS))
											{
												input.Consume();
												root_0 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(set88), root_0);
												state.errorRecovery = false;
											}
											else
											{
												MismatchedSetException mse = new MismatchedSetException(null, input);
												DebugRecognitionException(mse);
												throw mse;
											}

											DebugLocation(208, 43);
											PushFollow(Follow._add_subexpr_in_bit_subexpr862);
											add_subexpr89 = add_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, add_subexpr89.Tree);

										}
										break;

									default:
										goto loop25;
								}
							}

						loop25:
							;

						}
						finally { DebugExitSubRule(25); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("bit_subexpr", 14);
					LeaveRule("bit_subexpr", 14);
					Leave_bit_subexpr();
				}
				DebugLocation(208, 55);
			}
			finally { DebugExitRule(GrammarFileName, "bit_subexpr"); }
			return retval;

		}
		// $ANTLR end "bit_subexpr"

		public class add_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_add_subexpr();
		partial void Leave_add_subexpr();

		// $ANTLR start "add_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:210:1: add_subexpr : mul_subexpr ( ( ASTERISK | SLASH | PERCENT ) mul_subexpr )* ;
		[GrammarRule("add_subexpr")]
		private SQLiteParser.add_subexpr_return add_subexpr()
		{
			Enter_add_subexpr();
			EnterRule("add_subexpr", 15);
			TraceIn("add_subexpr", 15);
			SQLiteParser.add_subexpr_return retval = new SQLiteParser.add_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set91 = null;
			SQLiteParser.mul_subexpr_return mul_subexpr90 = default(SQLiteParser.mul_subexpr_return);
			SQLiteParser.mul_subexpr_return mul_subexpr92 = default(SQLiteParser.mul_subexpr_return);

			CommonTree set91_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "add_subexpr");
				DebugLocation(210, 69);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:210:12: ( mul_subexpr ( ( ASTERISK | SLASH | PERCENT ) mul_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:210:14: mul_subexpr ( ( ASTERISK | SLASH | PERCENT ) mul_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(210, 14);
						PushFollow(Follow._mul_subexpr_in_add_subexpr871);
						mul_subexpr90 = mul_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, mul_subexpr90.Tree);
						DebugLocation(210, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:210:26: ( ( ASTERISK | SLASH | PERCENT ) mul_subexpr )*
						try
						{
							DebugEnterSubRule(26);
							while (true)
							{
								int alt26 = 2;
								try
								{
									DebugEnterDecision(26, decisionCanBacktrack[26]);
									try
									{
										alt26 = dfa26.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(26); }
								switch (alt26)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:210:27: ( ASTERISK | SLASH | PERCENT ) mul_subexpr
										{
											DebugLocation(210, 27);
											set91 = (CommonToken)input.LT(1);
											set91 = (CommonToken)input.LT(1);
											if ((input.LA(1) >= ASTERISK && input.LA(1) <= PERCENT))
											{
												input.Consume();
												root_0 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(set91), root_0);
												state.errorRecovery = false;
											}
											else
											{
												MismatchedSetException mse = new MismatchedSetException(null, input);
												DebugRecognitionException(mse);
												throw mse;
											}

											DebugLocation(210, 57);
											PushFollow(Follow._mul_subexpr_in_add_subexpr887);
											mul_subexpr92 = mul_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, mul_subexpr92.Tree);

										}
										break;

									default:
										goto loop26;
								}
							}

						loop26:
							;

						}
						finally { DebugExitSubRule(26); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("add_subexpr", 15);
					LeaveRule("add_subexpr", 15);
					Leave_add_subexpr();
				}
				DebugLocation(210, 69);
			}
			finally { DebugExitRule(GrammarFileName, "add_subexpr"); }
			return retval;

		}
		// $ANTLR end "add_subexpr"

		public class mul_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_mul_subexpr();
		partial void Leave_mul_subexpr();

		// $ANTLR start "mul_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:212:1: mul_subexpr : con_subexpr ( DOUBLE_PIPE con_subexpr )* ;
		[GrammarRule("mul_subexpr")]
		private SQLiteParser.mul_subexpr_return mul_subexpr()
		{
			Enter_mul_subexpr();
			EnterRule("mul_subexpr", 16);
			TraceIn("mul_subexpr", 16);
			SQLiteParser.mul_subexpr_return retval = new SQLiteParser.mul_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DOUBLE_PIPE94 = null;
			SQLiteParser.con_subexpr_return con_subexpr93 = default(SQLiteParser.con_subexpr_return);
			SQLiteParser.con_subexpr_return con_subexpr95 = default(SQLiteParser.con_subexpr_return);

			CommonTree DOUBLE_PIPE94_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "mul_subexpr");
				DebugLocation(212, 52);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:212:12: ( con_subexpr ( DOUBLE_PIPE con_subexpr )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:212:14: con_subexpr ( DOUBLE_PIPE con_subexpr )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(212, 14);
						PushFollow(Follow._con_subexpr_in_mul_subexpr896);
						con_subexpr93 = con_subexpr();
						PopFollow();

						adaptor.AddChild(root_0, con_subexpr93.Tree);
						DebugLocation(212, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:212:26: ( DOUBLE_PIPE con_subexpr )*
						try
						{
							DebugEnterSubRule(27);
							while (true)
							{
								int alt27 = 2;
								try
								{
									DebugEnterDecision(27, decisionCanBacktrack[27]);
									try
									{
										alt27 = dfa27.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(27); }
								switch (alt27)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:212:27: DOUBLE_PIPE con_subexpr
										{
											DebugLocation(212, 38);
											DOUBLE_PIPE94 = (CommonToken)Match(input, DOUBLE_PIPE, Follow._DOUBLE_PIPE_in_mul_subexpr899);
											DOUBLE_PIPE94_tree = (CommonTree)adaptor.Create(DOUBLE_PIPE94);
											root_0 = (CommonTree)adaptor.BecomeRoot(DOUBLE_PIPE94_tree, root_0);

											DebugLocation(212, 40);
											PushFollow(Follow._con_subexpr_in_mul_subexpr902);
											con_subexpr95 = con_subexpr();
											PopFollow();

											adaptor.AddChild(root_0, con_subexpr95.Tree);

										}
										break;

									default:
										goto loop27;
								}
							}

						loop27:
							;

						}
						finally { DebugExitSubRule(27); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("mul_subexpr", 16);
					LeaveRule("mul_subexpr", 16);
					Leave_mul_subexpr();
				}
				DebugLocation(212, 52);
			}
			finally { DebugExitRule(GrammarFileName, "mul_subexpr"); }
			return retval;

		}
		// $ANTLR end "mul_subexpr"

		public class con_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_con_subexpr();
		partial void Leave_con_subexpr();

		// $ANTLR start "con_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:214:1: con_subexpr : ( unary_subexpr | unary_op unary_subexpr -> ^( unary_op unary_subexpr ) );
		[GrammarRule("con_subexpr")]
		private SQLiteParser.con_subexpr_return con_subexpr()
		{
			Enter_con_subexpr();
			EnterRule("con_subexpr", 17);
			TraceIn("con_subexpr", 17);
			SQLiteParser.con_subexpr_return retval = new SQLiteParser.con_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.unary_subexpr_return unary_subexpr96 = default(SQLiteParser.unary_subexpr_return);
			SQLiteParser.unary_op_return unary_op97 = default(SQLiteParser.unary_op_return);
			SQLiteParser.unary_subexpr_return unary_subexpr98 = default(SQLiteParser.unary_subexpr_return);

			RewriteRuleSubtreeStream stream_unary_subexpr = new RewriteRuleSubtreeStream(adaptor, "rule unary_subexpr");
			RewriteRuleSubtreeStream stream_unary_op = new RewriteRuleSubtreeStream(adaptor, "rule unary_op");
			try
			{
				DebugEnterRule(GrammarFileName, "con_subexpr");
				DebugLocation(214, 80);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:214:12: ( unary_subexpr | unary_op unary_subexpr -> ^( unary_op unary_subexpr ) )
					int alt28 = 2;
					try
					{
						DebugEnterDecision(28, decisionCanBacktrack[28]);
						try
						{
							alt28 = dfa28.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(28); }
					switch (alt28)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:214:14: unary_subexpr
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(214, 14);
								PushFollow(Follow._unary_subexpr_in_con_subexpr911);
								unary_subexpr96 = unary_subexpr();
								PopFollow();

								adaptor.AddChild(root_0, unary_subexpr96.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:214:30: unary_op unary_subexpr
							{
								DebugLocation(214, 30);
								PushFollow(Follow._unary_op_in_con_subexpr915);
								unary_op97 = unary_op();
								PopFollow();

								stream_unary_op.Add(unary_op97.Tree);
								DebugLocation(214, 39);
								PushFollow(Follow._unary_subexpr_in_con_subexpr917);
								unary_subexpr98 = unary_subexpr();
								PopFollow();

								stream_unary_subexpr.Add(unary_subexpr98.Tree);


								{
									// AST REWRITE
									// elements: unary_op, unary_subexpr
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 214:53: -> ^( unary_op unary_subexpr )
									{
										DebugLocation(214, 56);
										// C:\\Users\\Gareth\\Desktop\\test.g:214:56: ^( unary_op unary_subexpr )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(214, 58);
											root_1 = (CommonTree)adaptor.BecomeRoot(stream_unary_op.NextNode(), root_1);

											DebugLocation(214, 67);
											adaptor.AddChild(root_1, stream_unary_subexpr.NextTree());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("con_subexpr", 17);
					LeaveRule("con_subexpr", 17);
					Leave_con_subexpr();
				}
				DebugLocation(214, 80);
			}
			finally { DebugExitRule(GrammarFileName, "con_subexpr"); }
			return retval;

		}
		// $ANTLR end "con_subexpr"

		public class unary_op_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_unary_op();
		partial void Leave_unary_op();

		// $ANTLR start "unary_op"
		// C:\\Users\\Gareth\\Desktop\\test.g:216:1: unary_op : ( PLUS | MINUS | TILDA | NOT );
		[GrammarRule("unary_op")]
		private SQLiteParser.unary_op_return unary_op()
		{
			Enter_unary_op();
			EnterRule("unary_op", 18);
			TraceIn("unary_op", 18);
			SQLiteParser.unary_op_return retval = new SQLiteParser.unary_op_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set99 = null;

			CommonTree set99_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "unary_op");
				DebugLocation(216, 36);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:216:9: ( PLUS | MINUS | TILDA | NOT )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(216, 9);
						set99 = (CommonToken)input.LT(1);
						if (input.LA(1) == NOT || (input.LA(1) >= PLUS && input.LA(1) <= MINUS) || input.LA(1) == TILDA)
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set99));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("unary_op", 18);
					LeaveRule("unary_op", 18);
					Leave_unary_op();
				}
				DebugLocation(216, 36);
			}
			finally { DebugExitRule(GrammarFileName, "unary_op"); }
			return retval;

		}
		// $ANTLR end "unary_op"

		public class unary_subexpr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_unary_subexpr();
		partial void Leave_unary_subexpr();

		// $ANTLR start "unary_subexpr"
		// C:\\Users\\Gareth\\Desktop\\test.g:218:1: unary_subexpr : atom_expr ( COLLATE collation_name= ID )? ;
		[GrammarRule("unary_subexpr")]
		private SQLiteParser.unary_subexpr_return unary_subexpr()
		{
			Enter_unary_subexpr();
			EnterRule("unary_subexpr", 19);
			TraceIn("unary_subexpr", 19);
			SQLiteParser.unary_subexpr_return retval = new SQLiteParser.unary_subexpr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken collation_name = null;
			CommonToken COLLATE101 = null;
			SQLiteParser.atom_expr_return atom_expr100 = default(SQLiteParser.atom_expr_return);

			CommonTree collation_name_tree = null;
			CommonTree COLLATE101_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "unary_subexpr");
				DebugLocation(218, 54);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:218:14: ( atom_expr ( COLLATE collation_name= ID )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:218:16: atom_expr ( COLLATE collation_name= ID )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(218, 16);
						PushFollow(Follow._atom_expr_in_unary_subexpr951);
						atom_expr100 = atom_expr();
						PopFollow();

						adaptor.AddChild(root_0, atom_expr100.Tree);
						DebugLocation(218, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:218:26: ( COLLATE collation_name= ID )?
						int alt29 = 2;
						try
						{
							DebugEnterSubRule(29);
							try
							{
								DebugEnterDecision(29, decisionCanBacktrack[29]);
								try
								{
									alt29 = dfa29.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(29); }
							switch (alt29)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:218:27: COLLATE collation_name= ID
									{
										DebugLocation(218, 34);
										COLLATE101 = (CommonToken)Match(input, COLLATE, Follow._COLLATE_in_unary_subexpr954);
										COLLATE101_tree = (CommonTree)adaptor.Create(COLLATE101);
										root_0 = (CommonTree)adaptor.BecomeRoot(COLLATE101_tree, root_0);

										DebugLocation(218, 50);
										collation_name = (CommonToken)Match(input, ID, Follow._ID_in_unary_subexpr959);
										collation_name_tree = (CommonTree)adaptor.Create(collation_name);
										adaptor.AddChild(root_0, collation_name_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(29); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("unary_subexpr", 19);
					LeaveRule("unary_subexpr", 19);
					Leave_unary_subexpr();
				}
				DebugLocation(218, 54);
			}
			finally { DebugExitRule(GrammarFileName, "unary_subexpr"); }
			return retval;

		}
		// $ANTLR end "unary_subexpr"

		public class atom_expr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_atom_expr();
		partial void Leave_atom_expr();

		// $ANTLR start "atom_expr"
		// C:\\Users\\Gareth\\Desktop\\test.g:220:1: atom_expr : ( literal_value | bind_parameter | ( (database_name= id DOT )? table_name= id DOT )? column_name= ID -> ^( COLUMN_EXPRESSION ^( $column_name ( ^( $table_name ( $database_name)? ) )? ) ) | name= ID LPAREN ( ( DISTINCT )? args+= expr ( COMMA args+= expr )* | ASTERISK )? RPAREN -> ^( FUNCTION_EXPRESSION $name ( DISTINCT )? ( $args)* ( ASTERISK )? ) | LPAREN expr RPAREN | CAST LPAREN expr AS type_name RPAREN | CASE (case_expr= expr )? ( when_expr )+ ( ELSE else_expr= expr )? END -> ^( CASE ( $case_expr)? ( when_expr )+ ( $else_expr)? ) | raise_function );
		[GrammarRule("atom_expr")]
		private SQLiteParser.atom_expr_return atom_expr()
		{
			Enter_atom_expr();
			EnterRule("atom_expr", 20);
			TraceIn("atom_expr", 20);
			SQLiteParser.atom_expr_return retval = new SQLiteParser.atom_expr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken column_name = null;
			CommonToken name = null;
			CommonToken DOT104 = null;
			CommonToken DOT105 = null;
			CommonToken LPAREN106 = null;
			CommonToken DISTINCT107 = null;
			CommonToken COMMA108 = null;
			CommonToken ASTERISK109 = null;
			CommonToken RPAREN110 = null;
			CommonToken LPAREN111 = null;
			CommonToken RPAREN113 = null;
			CommonToken CAST114 = null;
			CommonToken LPAREN115 = null;
			CommonToken AS117 = null;
			CommonToken RPAREN119 = null;
			CommonToken CASE120 = null;
			CommonToken ELSE122 = null;
			CommonToken END123 = null;
			List list_args = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.expr_return case_expr = default(SQLiteParser.expr_return);
			SQLiteParser.expr_return else_expr = default(SQLiteParser.expr_return);
			SQLiteParser.literal_value_return literal_value102 = default(SQLiteParser.literal_value_return);
			SQLiteParser.bind_parameter_return bind_parameter103 = default(SQLiteParser.bind_parameter_return);
			SQLiteParser.expr_return expr112 = default(SQLiteParser.expr_return);
			SQLiteParser.expr_return expr116 = default(SQLiteParser.expr_return);
			SQLiteParser.type_name_return type_name118 = default(SQLiteParser.type_name_return);
			SQLiteParser.when_expr_return when_expr121 = default(SQLiteParser.when_expr_return);
			SQLiteParser.raise_function_return raise_function124 = default(SQLiteParser.raise_function_return);
			SQLiteParser.expr_return args = default(SQLiteParser.expr_return);
			CommonTree column_name_tree = null;
			CommonTree name_tree = null;
			CommonTree DOT104_tree = null;
			CommonTree DOT105_tree = null;
			CommonTree LPAREN106_tree = null;
			CommonTree DISTINCT107_tree = null;
			CommonTree COMMA108_tree = null;
			CommonTree ASTERISK109_tree = null;
			CommonTree RPAREN110_tree = null;
			CommonTree LPAREN111_tree = null;
			CommonTree RPAREN113_tree = null;
			CommonTree CAST114_tree = null;
			CommonTree LPAREN115_tree = null;
			CommonTree AS117_tree = null;
			CommonTree RPAREN119_tree = null;
			CommonTree CASE120_tree = null;
			CommonTree ELSE122_tree = null;
			CommonTree END123_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_END = new RewriteRuleITokenStream(adaptor, "token END");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_ID = new RewriteRuleITokenStream(adaptor, "token ID");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_DISTINCT = new RewriteRuleITokenStream(adaptor, "token DISTINCT");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleITokenStream stream_ASTERISK = new RewriteRuleITokenStream(adaptor, "token ASTERISK");
			RewriteRuleITokenStream stream_ELSE = new RewriteRuleITokenStream(adaptor, "token ELSE");
			RewriteRuleITokenStream stream_CASE = new RewriteRuleITokenStream(adaptor, "token CASE");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_when_expr = new RewriteRuleSubtreeStream(adaptor, "rule when_expr");
			RewriteRuleSubtreeStream stream_expr = new RewriteRuleSubtreeStream(adaptor, "rule expr");
			try
			{
				DebugEnterRule(GrammarFileName, "atom_expr");
				DebugLocation(220, 2);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:221:3: ( literal_value | bind_parameter | ( (database_name= id DOT )? table_name= id DOT )? column_name= ID -> ^( COLUMN_EXPRESSION ^( $column_name ( ^( $table_name ( $database_name)? ) )? ) ) | name= ID LPAREN ( ( DISTINCT )? args+= expr ( COMMA args+= expr )* | ASTERISK )? RPAREN -> ^( FUNCTION_EXPRESSION $name ( DISTINCT )? ( $args)* ( ASTERISK )? ) | LPAREN expr RPAREN | CAST LPAREN expr AS type_name RPAREN | CASE (case_expr= expr )? ( when_expr )+ ( ELSE else_expr= expr )? END -> ^( CASE ( $case_expr)? ( when_expr )+ ( $else_expr)? ) | raise_function )
					int alt38 = 8;
					try
					{
						DebugEnterDecision(38, decisionCanBacktrack[38]);
						try
						{
							alt38 = dfa38.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(38); }
					switch (alt38)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:221:5: literal_value
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(221, 5);
								PushFollow(Follow._literal_value_in_atom_expr971);
								literal_value102 = literal_value();
								PopFollow();

								adaptor.AddChild(root_0, literal_value102.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:222:5: bind_parameter
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(222, 5);
								PushFollow(Follow._bind_parameter_in_atom_expr977);
								bind_parameter103 = bind_parameter();
								PopFollow();

								adaptor.AddChild(root_0, bind_parameter103.Tree);

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:223:5: ( (database_name= id DOT )? table_name= id DOT )? column_name= ID
							{
								DebugLocation(223, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:223:5: ( (database_name= id DOT )? table_name= id DOT )?
								int alt31 = 2;
								try
								{
									DebugEnterSubRule(31);
									try
									{
										DebugEnterDecision(31, decisionCanBacktrack[31]);
										try
										{
											alt31 = dfa31.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(31); }
									switch (alt31)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:223:6: (database_name= id DOT )? table_name= id DOT
											{
												DebugLocation(223, 6);
												// C:\\Users\\Gareth\\Desktop\\test.g:223:6: (database_name= id DOT )?
												int alt30 = 2;
												try
												{
													DebugEnterSubRule(30);
													try
													{
														DebugEnterDecision(30, decisionCanBacktrack[30]);
														try
														{
															alt30 = dfa30.Predict(input);
														}
														catch (NoViableAltException nvae)
														{
															DebugRecognitionException(nvae);
															throw;
														}
													}
													finally { DebugExitDecision(30); }
													switch (alt30)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:223:7: database_name= id DOT
															{
																DebugLocation(223, 20);
																PushFollow(Follow._id_in_atom_expr987);
																database_name = id();
																PopFollow();

																stream_id.Add(database_name.Tree);
																DebugLocation(223, 24);
																DOT104 = (CommonToken)Match(input, DOT, Follow._DOT_in_atom_expr989);
																stream_DOT.Add(DOT104);


															}
															break;

													}
												}
												finally { DebugExitSubRule(30); }

												DebugLocation(223, 40);
												PushFollow(Follow._id_in_atom_expr995);
												table_name = id();
												PopFollow();

												stream_id.Add(table_name.Tree);
												DebugLocation(223, 44);
												DOT105 = (CommonToken)Match(input, DOT, Follow._DOT_in_atom_expr997);
												stream_DOT.Add(DOT105);


											}
											break;

									}
								}
								finally { DebugExitSubRule(31); }

								DebugLocation(223, 61);
								column_name = (CommonToken)Match(input, ID, Follow._ID_in_atom_expr1003);
								stream_ID.Add(column_name);



								{
									// AST REWRITE
									// elements: table_name, column_name, database_name
									// token labels: column_name
									// rule labels: database_name, retval, table_name
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleITokenStream stream_column_name = new RewriteRuleITokenStream(adaptor, "token column_name", column_name);
									RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 223:65: -> ^( COLUMN_EXPRESSION ^( $column_name ( ^( $table_name ( $database_name)? ) )? ) )
									{
										DebugLocation(223, 68);
										// C:\\Users\\Gareth\\Desktop\\test.g:223:68: ^( COLUMN_EXPRESSION ^( $column_name ( ^( $table_name ( $database_name)? ) )? ) )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(223, 70);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMN_EXPRESSION, "COLUMN_EXPRESSION"), root_1);

											DebugLocation(223, 88);
											// C:\\Users\\Gareth\\Desktop\\test.g:223:88: ^( $column_name ( ^( $table_name ( $database_name)? ) )? )
											{
												CommonTree root_2 = (CommonTree)adaptor.Nil();
												DebugLocation(223, 90);
												root_2 = (CommonTree)adaptor.BecomeRoot(stream_column_name.NextNode(), root_2);

												DebugLocation(223, 103);
												// C:\\Users\\Gareth\\Desktop\\test.g:223:103: ( ^( $table_name ( $database_name)? ) )?
												if (stream_table_name.HasNext || stream_database_name.HasNext)
												{
													DebugLocation(223, 103);
													// C:\\Users\\Gareth\\Desktop\\test.g:223:103: ^( $table_name ( $database_name)? )
													{
														CommonTree root_3 = (CommonTree)adaptor.Nil();
														DebugLocation(223, 105);
														root_3 = (CommonTree)adaptor.BecomeRoot(stream_table_name.NextNode(), root_3);

														DebugLocation(223, 117);
														// C:\\Users\\Gareth\\Desktop\\test.g:223:117: ( $database_name)?
														if (stream_database_name.HasNext)
														{
															DebugLocation(223, 117);
															adaptor.AddChild(root_3, stream_database_name.NextTree());

														}
														stream_database_name.Reset();

														adaptor.AddChild(root_2, root_3);
													}

												}
												stream_table_name.Reset();
												stream_database_name.Reset();

												adaptor.AddChild(root_1, root_2);
											}

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 4:
							DebugEnterAlt(4);
							// C:\\Users\\Gareth\\Desktop\\test.g:224:5: name= ID LPAREN ( ( DISTINCT )? args+= expr ( COMMA args+= expr )* | ASTERISK )? RPAREN
							{
								DebugLocation(224, 9);
								name = (CommonToken)Match(input, ID, Follow._ID_in_atom_expr1032);
								stream_ID.Add(name);

								DebugLocation(224, 13);
								LPAREN106 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_atom_expr1034);
								stream_LPAREN.Add(LPAREN106);

								DebugLocation(224, 20);
								// C:\\Users\\Gareth\\Desktop\\test.g:224:20: ( ( DISTINCT )? args+= expr ( COMMA args+= expr )* | ASTERISK )?
								int alt34 = 3;
								try
								{
									DebugEnterSubRule(34);
									try
									{
										DebugEnterDecision(34, decisionCanBacktrack[34]);
										try
										{
											alt34 = dfa34.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(34); }
									switch (alt34)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:224:21: ( DISTINCT )? args+= expr ( COMMA args+= expr )*
											{
												DebugLocation(224, 21);
												// C:\\Users\\Gareth\\Desktop\\test.g:224:21: ( DISTINCT )?
												int alt32 = 2;
												try
												{
													DebugEnterSubRule(32);
													try
													{
														DebugEnterDecision(32, decisionCanBacktrack[32]);
														try
														{
															alt32 = dfa32.Predict(input);
														}
														catch (NoViableAltException nvae)
														{
															DebugRecognitionException(nvae);
															throw;
														}
													}
													finally { DebugExitDecision(32); }
													switch (alt32)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:224:21: DISTINCT
															{
																DebugLocation(224, 21);
																DISTINCT107 = (CommonToken)Match(input, DISTINCT, Follow._DISTINCT_in_atom_expr1037);
																stream_DISTINCT.Add(DISTINCT107);


															}
															break;

													}
												}
												finally { DebugExitSubRule(32); }

												DebugLocation(224, 35);
												PushFollow(Follow._expr_in_atom_expr1042);
												args = expr();
												PopFollow();

												stream_expr.Add(args.Tree);
												if (list_args == null) list_args = new ArrayList();
												list_args.Add(args.Tree);

												DebugLocation(224, 42);
												// C:\\Users\\Gareth\\Desktop\\test.g:224:42: ( COMMA args+= expr )*
												try
												{
													DebugEnterSubRule(33);
													while (true)
													{
														int alt33 = 2;
														try
														{
															DebugEnterDecision(33, decisionCanBacktrack[33]);
															int LA33_0 = input.LA(1);

															if ((LA33_0 == COMMA))
															{
																alt33 = 1;
															}


														}
														finally { DebugExitDecision(33); }
														switch (alt33)
														{
															case 1:
																DebugEnterAlt(1);
																// C:\\Users\\Gareth\\Desktop\\test.g:224:43: COMMA args+= expr
																{
																	DebugLocation(224, 43);
																	COMMA108 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_atom_expr1045);
																	stream_COMMA.Add(COMMA108);

																	DebugLocation(224, 53);
																	PushFollow(Follow._expr_in_atom_expr1049);
																	args = expr();
																	PopFollow();

																	stream_expr.Add(args.Tree);
																	if (list_args == null) list_args = new ArrayList();
																	list_args.Add(args.Tree);


																}
																break;

															default:
																goto loop33;
														}
													}

												loop33:
													;

												}
												finally { DebugExitSubRule(33); }


											}
											break;
										case 2:
											DebugEnterAlt(2);
											// C:\\Users\\Gareth\\Desktop\\test.g:224:64: ASTERISK
											{
												DebugLocation(224, 64);
												ASTERISK109 = (CommonToken)Match(input, ASTERISK, Follow._ASTERISK_in_atom_expr1055);
												stream_ASTERISK.Add(ASTERISK109);


											}
											break;

									}
								}
								finally { DebugExitSubRule(34); }

								DebugLocation(224, 75);
								RPAREN110 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_atom_expr1059);
								stream_RPAREN.Add(RPAREN110);



								{
									// AST REWRITE
									// elements: ASTERISK, args, DISTINCT, name
									// token labels: name
									// rule labels: retval
									// token list labels: 
									// rule list labels: args
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleITokenStream stream_name = new RewriteRuleITokenStream(adaptor, "token name", name);
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_args = new RewriteRuleSubtreeStream(adaptor, "token args", list_args);
									root_0 = (CommonTree)adaptor.Nil();
									// 224:82: -> ^( FUNCTION_EXPRESSION $name ( DISTINCT )? ( $args)* ( ASTERISK )? )
									{
										DebugLocation(224, 85);
										// C:\\Users\\Gareth\\Desktop\\test.g:224:85: ^( FUNCTION_EXPRESSION $name ( DISTINCT )? ( $args)* ( ASTERISK )? )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(224, 87);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FUNCTION_EXPRESSION, "FUNCTION_EXPRESSION"), root_1);

											DebugLocation(224, 107);
											adaptor.AddChild(root_1, stream_name.NextNode());
											DebugLocation(224, 113);
											// C:\\Users\\Gareth\\Desktop\\test.g:224:113: ( DISTINCT )?
											if (stream_DISTINCT.HasNext)
											{
												DebugLocation(224, 113);
												adaptor.AddChild(root_1, stream_DISTINCT.NextNode());

											}
											stream_DISTINCT.Reset();
											DebugLocation(224, 123);
											// C:\\Users\\Gareth\\Desktop\\test.g:224:123: ( $args)*
											while (stream_args.HasNext)
											{
												DebugLocation(224, 123);
												adaptor.AddChild(root_1, stream_args.NextTree());

											}
											stream_args.Reset();
											DebugLocation(224, 130);
											// C:\\Users\\Gareth\\Desktop\\test.g:224:130: ( ASTERISK )?
											if (stream_ASTERISK.HasNext)
											{
												DebugLocation(224, 130);
												adaptor.AddChild(root_1, stream_ASTERISK.NextNode());

											}
											stream_ASTERISK.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 5:
							DebugEnterAlt(5);
							// C:\\Users\\Gareth\\Desktop\\test.g:225:5: LPAREN expr RPAREN
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(225, 11);
								LPAREN111 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_atom_expr1084);
								DebugLocation(225, 13);
								PushFollow(Follow._expr_in_atom_expr1087);
								expr112 = expr();
								PopFollow();

								adaptor.AddChild(root_0, expr112.Tree);
								DebugLocation(225, 24);
								RPAREN113 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_atom_expr1089);

							}
							break;
						case 6:
							DebugEnterAlt(6);
							// C:\\Users\\Gareth\\Desktop\\test.g:226:5: CAST LPAREN expr AS type_name RPAREN
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(226, 9);
								CAST114 = (CommonToken)Match(input, CAST, Follow._CAST_in_atom_expr1096);
								CAST114_tree = (CommonTree)adaptor.Create(CAST114);
								root_0 = (CommonTree)adaptor.BecomeRoot(CAST114_tree, root_0);

								DebugLocation(226, 17);
								LPAREN115 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_atom_expr1099);
								DebugLocation(226, 19);
								PushFollow(Follow._expr_in_atom_expr1102);
								expr116 = expr();
								PopFollow();

								adaptor.AddChild(root_0, expr116.Tree);
								DebugLocation(226, 26);
								AS117 = (CommonToken)Match(input, AS, Follow._AS_in_atom_expr1104);
								DebugLocation(226, 28);
								PushFollow(Follow._type_name_in_atom_expr1107);
								type_name118 = type_name();
								PopFollow();

								adaptor.AddChild(root_0, type_name118.Tree);
								DebugLocation(226, 44);
								RPAREN119 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_atom_expr1109);

							}
							break;
						case 7:
							DebugEnterAlt(7);
							// C:\\Users\\Gareth\\Desktop\\test.g:229:5: CASE (case_expr= expr )? ( when_expr )+ ( ELSE else_expr= expr )? END
							{
								DebugLocation(229, 5);
								CASE120 = (CommonToken)Match(input, CASE, Follow._CASE_in_atom_expr1118);
								stream_CASE.Add(CASE120);

								DebugLocation(229, 10);
								// C:\\Users\\Gareth\\Desktop\\test.g:229:10: (case_expr= expr )?
								int alt35 = 2;
								try
								{
									DebugEnterSubRule(35);
									try
									{
										DebugEnterDecision(35, decisionCanBacktrack[35]);
										try
										{
											alt35 = dfa35.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(35); }
									switch (alt35)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:229:11: case_expr= expr
											{
												DebugLocation(229, 20);
												PushFollow(Follow._expr_in_atom_expr1123);
												case_expr = expr();
												PopFollow();

												stream_expr.Add(case_expr.Tree);

											}
											break;

									}
								}
								finally { DebugExitSubRule(35); }

								DebugLocation(229, 28);
								// C:\\Users\\Gareth\\Desktop\\test.g:229:28: ( when_expr )+
								int cnt36 = 0;
								try
								{
									DebugEnterSubRule(36);
									while (true)
									{
										int alt36 = 2;
										try
										{
											DebugEnterDecision(36, decisionCanBacktrack[36]);
											int LA36_0 = input.LA(1);

											if ((LA36_0 == WHEN))
											{
												alt36 = 1;
											}


										}
										finally { DebugExitDecision(36); }
										switch (alt36)
										{
											case 1:
												DebugEnterAlt(1);
												// C:\\Users\\Gareth\\Desktop\\test.g:229:28: when_expr
												{
													DebugLocation(229, 28);
													PushFollow(Follow._when_expr_in_atom_expr1127);
													when_expr121 = when_expr();
													PopFollow();

													stream_when_expr.Add(when_expr121.Tree);

												}
												break;

											default:
												if (cnt36 >= 1)
													goto loop36;

												EarlyExitException eee36 = new EarlyExitException(36, input);
												DebugRecognitionException(eee36);
												throw eee36;
										}
										cnt36++;
									}
								loop36:
									;

								}
								finally { DebugExitSubRule(36); }

								DebugLocation(229, 39);
								// C:\\Users\\Gareth\\Desktop\\test.g:229:39: ( ELSE else_expr= expr )?
								int alt37 = 2;
								try
								{
									DebugEnterSubRule(37);
									try
									{
										DebugEnterDecision(37, decisionCanBacktrack[37]);
										int LA37_0 = input.LA(1);

										if ((LA37_0 == ELSE))
										{
											alt37 = 1;
										}
									}
									finally { DebugExitDecision(37); }
									switch (alt37)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:229:40: ELSE else_expr= expr
											{
												DebugLocation(229, 40);
												ELSE122 = (CommonToken)Match(input, ELSE, Follow._ELSE_in_atom_expr1131);
												stream_ELSE.Add(ELSE122);

												DebugLocation(229, 54);
												PushFollow(Follow._expr_in_atom_expr1135);
												else_expr = expr();
												PopFollow();

												stream_expr.Add(else_expr.Tree);

											}
											break;

									}
								}
								finally { DebugExitSubRule(37); }

								DebugLocation(229, 62);
								END123 = (CommonToken)Match(input, END, Follow._END_in_atom_expr1139);
								stream_END.Add(END123);



								{
									// AST REWRITE
									// elements: when_expr, CASE, case_expr, else_expr
									// token labels: 
									// rule labels: retval, case_expr, else_expr
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_case_expr = new RewriteRuleSubtreeStream(adaptor, "rule case_expr", case_expr != null ? case_expr.Tree : null);
									RewriteRuleSubtreeStream stream_else_expr = new RewriteRuleSubtreeStream(adaptor, "rule else_expr", else_expr != null ? else_expr.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 229:66: -> ^( CASE ( $case_expr)? ( when_expr )+ ( $else_expr)? )
									{
										DebugLocation(229, 69);
										// C:\\Users\\Gareth\\Desktop\\test.g:229:69: ^( CASE ( $case_expr)? ( when_expr )+ ( $else_expr)? )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(229, 71);
											root_1 = (CommonTree)adaptor.BecomeRoot(stream_CASE.NextNode(), root_1);

											DebugLocation(229, 76);
											// C:\\Users\\Gareth\\Desktop\\test.g:229:76: ( $case_expr)?
											if (stream_case_expr.HasNext)
											{
												DebugLocation(229, 76);
												adaptor.AddChild(root_1, stream_case_expr.NextTree());

											}
											stream_case_expr.Reset();
											DebugLocation(229, 88);
											if (!(stream_when_expr.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_when_expr.HasNext)
											{
												DebugLocation(229, 88);
												adaptor.AddChild(root_1, stream_when_expr.NextTree());

											}
											stream_when_expr.Reset();
											DebugLocation(229, 99);
											// C:\\Users\\Gareth\\Desktop\\test.g:229:99: ( $else_expr)?
											if (stream_else_expr.HasNext)
											{
												DebugLocation(229, 99);
												adaptor.AddChild(root_1, stream_else_expr.NextTree());

											}
											stream_else_expr.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 8:
							DebugEnterAlt(8);
							// C:\\Users\\Gareth\\Desktop\\test.g:230:5: raise_function
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(230, 5);
								PushFollow(Follow._raise_function_in_atom_expr1162);
								raise_function124 = raise_function();
								PopFollow();

								adaptor.AddChild(root_0, raise_function124.Tree);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("atom_expr", 20);
					LeaveRule("atom_expr", 20);
					Leave_atom_expr();
				}
				DebugLocation(231, 2);
			}
			finally { DebugExitRule(GrammarFileName, "atom_expr"); }
			return retval;

		}
		// $ANTLR end "atom_expr"

		public class when_expr_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_when_expr();
		partial void Leave_when_expr();

		// $ANTLR start "when_expr"
		// C:\\Users\\Gareth\\Desktop\\test.g:233:1: when_expr : WHEN e1= expr THEN e2= expr -> ^( WHEN $e1 $e2) ;
		[GrammarRule("when_expr")]
		private SQLiteParser.when_expr_return when_expr()
		{
			Enter_when_expr();
			EnterRule("when_expr", 21);
			TraceIn("when_expr", 21);
			SQLiteParser.when_expr_return retval = new SQLiteParser.when_expr_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken WHEN125 = null;
			CommonToken THEN126 = null;
			SQLiteParser.expr_return e1 = default(SQLiteParser.expr_return);
			SQLiteParser.expr_return e2 = default(SQLiteParser.expr_return);

			CommonTree WHEN125_tree = null;
			CommonTree THEN126_tree = null;
			RewriteRuleITokenStream stream_THEN = new RewriteRuleITokenStream(adaptor, "token THEN");
			RewriteRuleITokenStream stream_WHEN = new RewriteRuleITokenStream(adaptor, "token WHEN");
			RewriteRuleSubtreeStream stream_expr = new RewriteRuleSubtreeStream(adaptor, "rule expr");
			try
			{
				DebugEnterRule(GrammarFileName, "when_expr");
				DebugLocation(233, 55);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:233:10: ( WHEN e1= expr THEN e2= expr -> ^( WHEN $e1 $e2) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:233:12: WHEN e1= expr THEN e2= expr
					{
						DebugLocation(233, 12);
						WHEN125 = (CommonToken)Match(input, WHEN, Follow._WHEN_in_when_expr1172);
						stream_WHEN.Add(WHEN125);

						DebugLocation(233, 19);
						PushFollow(Follow._expr_in_when_expr1176);
						e1 = expr();
						PopFollow();

						stream_expr.Add(e1.Tree);
						DebugLocation(233, 25);
						THEN126 = (CommonToken)Match(input, THEN, Follow._THEN_in_when_expr1178);
						stream_THEN.Add(THEN126);

						DebugLocation(233, 32);
						PushFollow(Follow._expr_in_when_expr1182);
						e2 = expr();
						PopFollow();

						stream_expr.Add(e2.Tree);


						{
							// AST REWRITE
							// elements: WHEN, e1, e2
							// token labels: 
							// rule labels: retval, e1, e2
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_e1 = new RewriteRuleSubtreeStream(adaptor, "rule e1", e1 != null ? e1.Tree : null);
							RewriteRuleSubtreeStream stream_e2 = new RewriteRuleSubtreeStream(adaptor, "rule e2", e2 != null ? e2.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 233:38: -> ^( WHEN $e1 $e2)
							{
								DebugLocation(233, 41);
								// C:\\Users\\Gareth\\Desktop\\test.g:233:41: ^( WHEN $e1 $e2)
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(233, 43);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_WHEN.NextNode(), root_1);

									DebugLocation(233, 48);
									adaptor.AddChild(root_1, stream_e1.NextTree());
									DebugLocation(233, 52);
									adaptor.AddChild(root_1, stream_e2.NextTree());

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("when_expr", 21);
					LeaveRule("when_expr", 21);
					Leave_when_expr();
				}
				DebugLocation(233, 55);
			}
			finally { DebugExitRule(GrammarFileName, "when_expr"); }
			return retval;

		}
		// $ANTLR end "when_expr"

		public class literal_value_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_literal_value();
		partial void Leave_literal_value();

		// $ANTLR start "literal_value"
		// C:\\Users\\Gareth\\Desktop\\test.g:235:1: literal_value : ( INTEGER -> ^( INTEGER_LITERAL INTEGER ) | FLOAT -> ^( FLOAT_LITERAL FLOAT ) | STRING -> ^( STRING_LITERAL STRING ) | BLOB -> ^( BLOB_LITERAL BLOB ) | NULL | CURRENT_TIME -> ^( FUNCTION_LITERAL CURRENT_TIME ) | CURRENT_DATE -> ^( FUNCTION_LITERAL CURRENT_DATE ) | CURRENT_TIMESTAMP -> ^( FUNCTION_LITERAL CURRENT_TIMESTAMP ) );
		[GrammarRule("literal_value")]
		private SQLiteParser.literal_value_return literal_value()
		{
			Enter_literal_value();
			EnterRule("literal_value", 22);
			TraceIn("literal_value", 22);
			SQLiteParser.literal_value_return retval = new SQLiteParser.literal_value_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken INTEGER127 = null;
			CommonToken FLOAT128 = null;
			CommonToken STRING129 = null;
			CommonToken BLOB130 = null;
			CommonToken NULL131 = null;
			CommonToken CURRENT_TIME132 = null;
			CommonToken CURRENT_DATE133 = null;
			CommonToken CURRENT_TIMESTAMP134 = null;

			CommonTree INTEGER127_tree = null;
			CommonTree FLOAT128_tree = null;
			CommonTree STRING129_tree = null;
			CommonTree BLOB130_tree = null;
			CommonTree NULL131_tree = null;
			CommonTree CURRENT_TIME132_tree = null;
			CommonTree CURRENT_DATE133_tree = null;
			CommonTree CURRENT_TIMESTAMP134_tree = null;
			RewriteRuleITokenStream stream_INTEGER = new RewriteRuleITokenStream(adaptor, "token INTEGER");
			RewriteRuleITokenStream stream_BLOB = new RewriteRuleITokenStream(adaptor, "token BLOB");
			RewriteRuleITokenStream stream_FLOAT = new RewriteRuleITokenStream(adaptor, "token FLOAT");
			RewriteRuleITokenStream stream_CURRENT_TIMESTAMP = new RewriteRuleITokenStream(adaptor, "token CURRENT_TIMESTAMP");
			RewriteRuleITokenStream stream_CURRENT_DATE = new RewriteRuleITokenStream(adaptor, "token CURRENT_DATE");
			RewriteRuleITokenStream stream_CURRENT_TIME = new RewriteRuleITokenStream(adaptor, "token CURRENT_TIME");
			RewriteRuleITokenStream stream_STRING = new RewriteRuleITokenStream(adaptor, "token STRING");

			try
			{
				DebugEnterRule(GrammarFileName, "literal_value");
				DebugLocation(235, 2);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:236:3: ( INTEGER -> ^( INTEGER_LITERAL INTEGER ) | FLOAT -> ^( FLOAT_LITERAL FLOAT ) | STRING -> ^( STRING_LITERAL STRING ) | BLOB -> ^( BLOB_LITERAL BLOB ) | NULL | CURRENT_TIME -> ^( FUNCTION_LITERAL CURRENT_TIME ) | CURRENT_DATE -> ^( FUNCTION_LITERAL CURRENT_DATE ) | CURRENT_TIMESTAMP -> ^( FUNCTION_LITERAL CURRENT_TIMESTAMP ) )
					int alt39 = 8;
					try
					{
						DebugEnterDecision(39, decisionCanBacktrack[39]);
						switch (input.LA(1))
						{
							case INTEGER:
								{
									alt39 = 1;
								}
								break;
							case FLOAT:
								{
									alt39 = 2;
								}
								break;
							case STRING:
								{
									alt39 = 3;
								}
								break;
							case BLOB:
								{
									alt39 = 4;
								}
								break;
							case NULL:
								{
									alt39 = 5;
								}
								break;
							case CURRENT_TIME:
								{
									alt39 = 6;
								}
								break;
							case CURRENT_DATE:
								{
									alt39 = 7;
								}
								break;
							case CURRENT_TIMESTAMP:
								{
									alt39 = 8;
								}
								break;
							default:
								{
									NoViableAltException nvae = new NoViableAltException("", 39, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
						}

					}
					finally { DebugExitDecision(39); }
					switch (alt39)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:236:5: INTEGER
							{
								DebugLocation(236, 5);
								INTEGER127 = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_literal_value1204);
								stream_INTEGER.Add(INTEGER127);



								{
									// AST REWRITE
									// elements: INTEGER
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 236:13: -> ^( INTEGER_LITERAL INTEGER )
									{
										DebugLocation(236, 16);
										// C:\\Users\\Gareth\\Desktop\\test.g:236:16: ^( INTEGER_LITERAL INTEGER )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(236, 18);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(INTEGER_LITERAL, "INTEGER_LITERAL"), root_1);

											DebugLocation(236, 34);
											adaptor.AddChild(root_1, stream_INTEGER.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:237:5: FLOAT
							{
								DebugLocation(237, 5);
								FLOAT128 = (CommonToken)Match(input, FLOAT, Follow._FLOAT_in_literal_value1218);
								stream_FLOAT.Add(FLOAT128);



								{
									// AST REWRITE
									// elements: FLOAT
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 237:11: -> ^( FLOAT_LITERAL FLOAT )
									{
										DebugLocation(237, 14);
										// C:\\Users\\Gareth\\Desktop\\test.g:237:14: ^( FLOAT_LITERAL FLOAT )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(237, 16);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FLOAT_LITERAL, "FLOAT_LITERAL"), root_1);

											DebugLocation(237, 30);
											adaptor.AddChild(root_1, stream_FLOAT.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:238:5: STRING
							{
								DebugLocation(238, 5);
								STRING129 = (CommonToken)Match(input, STRING, Follow._STRING_in_literal_value1232);
								stream_STRING.Add(STRING129);



								{
									// AST REWRITE
									// elements: STRING
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 238:12: -> ^( STRING_LITERAL STRING )
									{
										DebugLocation(238, 15);
										// C:\\Users\\Gareth\\Desktop\\test.g:238:15: ^( STRING_LITERAL STRING )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(238, 17);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(STRING_LITERAL, "STRING_LITERAL"), root_1);

											DebugLocation(238, 32);
											adaptor.AddChild(root_1, stream_STRING.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 4:
							DebugEnterAlt(4);
							// C:\\Users\\Gareth\\Desktop\\test.g:239:5: BLOB
							{
								DebugLocation(239, 5);
								BLOB130 = (CommonToken)Match(input, BLOB, Follow._BLOB_in_literal_value1246);
								stream_BLOB.Add(BLOB130);



								{
									// AST REWRITE
									// elements: BLOB
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 239:10: -> ^( BLOB_LITERAL BLOB )
									{
										DebugLocation(239, 13);
										// C:\\Users\\Gareth\\Desktop\\test.g:239:13: ^( BLOB_LITERAL BLOB )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(239, 15);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(BLOB_LITERAL, "BLOB_LITERAL"), root_1);

											DebugLocation(239, 28);
											adaptor.AddChild(root_1, stream_BLOB.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 5:
							DebugEnterAlt(5);
							// C:\\Users\\Gareth\\Desktop\\test.g:240:5: NULL
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(240, 5);
								NULL131 = (CommonToken)Match(input, NULL, Follow._NULL_in_literal_value1260);
								NULL131_tree = (CommonTree)adaptor.Create(NULL131);
								adaptor.AddChild(root_0, NULL131_tree);


							}
							break;
						case 6:
							DebugEnterAlt(6);
							// C:\\Users\\Gareth\\Desktop\\test.g:241:5: CURRENT_TIME
							{
								DebugLocation(241, 5);
								CURRENT_TIME132 = (CommonToken)Match(input, CURRENT_TIME, Follow._CURRENT_TIME_in_literal_value1266);
								stream_CURRENT_TIME.Add(CURRENT_TIME132);



								{
									// AST REWRITE
									// elements: CURRENT_TIME
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 241:18: -> ^( FUNCTION_LITERAL CURRENT_TIME )
									{
										DebugLocation(241, 21);
										// C:\\Users\\Gareth\\Desktop\\test.g:241:21: ^( FUNCTION_LITERAL CURRENT_TIME )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(241, 23);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FUNCTION_LITERAL, "FUNCTION_LITERAL"), root_1);

											DebugLocation(241, 40);
											adaptor.AddChild(root_1, stream_CURRENT_TIME.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 7:
							DebugEnterAlt(7);
							// C:\\Users\\Gareth\\Desktop\\test.g:242:5: CURRENT_DATE
							{
								DebugLocation(242, 5);
								CURRENT_DATE133 = (CommonToken)Match(input, CURRENT_DATE, Follow._CURRENT_DATE_in_literal_value1280);
								stream_CURRENT_DATE.Add(CURRENT_DATE133);



								{
									// AST REWRITE
									// elements: CURRENT_DATE
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 242:18: -> ^( FUNCTION_LITERAL CURRENT_DATE )
									{
										DebugLocation(242, 21);
										// C:\\Users\\Gareth\\Desktop\\test.g:242:21: ^( FUNCTION_LITERAL CURRENT_DATE )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(242, 23);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FUNCTION_LITERAL, "FUNCTION_LITERAL"), root_1);

											DebugLocation(242, 40);
											adaptor.AddChild(root_1, stream_CURRENT_DATE.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 8:
							DebugEnterAlt(8);
							// C:\\Users\\Gareth\\Desktop\\test.g:243:5: CURRENT_TIMESTAMP
							{
								DebugLocation(243, 5);
								CURRENT_TIMESTAMP134 = (CommonToken)Match(input, CURRENT_TIMESTAMP, Follow._CURRENT_TIMESTAMP_in_literal_value1294);
								stream_CURRENT_TIMESTAMP.Add(CURRENT_TIMESTAMP134);



								{
									// AST REWRITE
									// elements: CURRENT_TIMESTAMP
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 243:23: -> ^( FUNCTION_LITERAL CURRENT_TIMESTAMP )
									{
										DebugLocation(243, 26);
										// C:\\Users\\Gareth\\Desktop\\test.g:243:26: ^( FUNCTION_LITERAL CURRENT_TIMESTAMP )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(243, 28);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FUNCTION_LITERAL, "FUNCTION_LITERAL"), root_1);

											DebugLocation(243, 45);
											adaptor.AddChild(root_1, stream_CURRENT_TIMESTAMP.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("literal_value", 22);
					LeaveRule("literal_value", 22);
					Leave_literal_value();
				}
				DebugLocation(244, 2);
			}
			finally { DebugExitRule(GrammarFileName, "literal_value"); }
			return retval;

		}
		// $ANTLR end "literal_value"

		public class bind_parameter_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_bind_parameter();
		partial void Leave_bind_parameter();

		// $ANTLR start "bind_parameter"
		// C:\\Users\\Gareth\\Desktop\\test.g:246:1: bind_parameter : ( QUESTION -> BIND | QUESTION position= INTEGER -> ^( BIND $position) | COLON name= id -> ^( BIND_NAME $name) | AT name= id -> ^( BIND_NAME $name) );
		[GrammarRule("bind_parameter")]
		private SQLiteParser.bind_parameter_return bind_parameter()
		{
			Enter_bind_parameter();
			EnterRule("bind_parameter", 23);
			TraceIn("bind_parameter", 23);
			SQLiteParser.bind_parameter_return retval = new SQLiteParser.bind_parameter_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken position = null;
			CommonToken QUESTION135 = null;
			CommonToken QUESTION136 = null;
			CommonToken COLON137 = null;
			CommonToken AT138 = null;
			SQLiteParser.id_return name = default(SQLiteParser.id_return);

			CommonTree position_tree = null;
			CommonTree QUESTION135_tree = null;
			CommonTree QUESTION136_tree = null;
			CommonTree COLON137_tree = null;
			CommonTree AT138_tree = null;
			RewriteRuleITokenStream stream_AT = new RewriteRuleITokenStream(adaptor, "token AT");
			RewriteRuleITokenStream stream_COLON = new RewriteRuleITokenStream(adaptor, "token COLON");
			RewriteRuleITokenStream stream_INTEGER = new RewriteRuleITokenStream(adaptor, "token INTEGER");
			RewriteRuleITokenStream stream_QUESTION = new RewriteRuleITokenStream(adaptor, "token QUESTION");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			try
			{
				DebugEnterRule(GrammarFileName, "bind_parameter");
				DebugLocation(246, 2);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:247:3: ( QUESTION -> BIND | QUESTION position= INTEGER -> ^( BIND $position) | COLON name= id -> ^( BIND_NAME $name) | AT name= id -> ^( BIND_NAME $name) )
					int alt40 = 4;
					try
					{
						DebugEnterDecision(40, decisionCanBacktrack[40]);
						try
						{
							alt40 = dfa40.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(40); }
					switch (alt40)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:247:5: QUESTION
							{
								DebugLocation(247, 5);
								QUESTION135 = (CommonToken)Match(input, QUESTION, Follow._QUESTION_in_bind_parameter1315);
								stream_QUESTION.Add(QUESTION135);



								{
									// AST REWRITE
									// elements: 
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 247:14: -> BIND
									{
										DebugLocation(247, 17);
										adaptor.AddChild(root_0, (CommonTree)adaptor.Create(BIND, "BIND"));

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:248:5: QUESTION position= INTEGER
							{
								DebugLocation(248, 5);
								QUESTION136 = (CommonToken)Match(input, QUESTION, Follow._QUESTION_in_bind_parameter1325);
								stream_QUESTION.Add(QUESTION136);

								DebugLocation(248, 22);
								position = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_bind_parameter1329);
								stream_INTEGER.Add(position);



								{
									// AST REWRITE
									// elements: position
									// token labels: position
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleITokenStream stream_position = new RewriteRuleITokenStream(adaptor, "token position", position);
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 248:31: -> ^( BIND $position)
									{
										DebugLocation(248, 34);
										// C:\\Users\\Gareth\\Desktop\\test.g:248:34: ^( BIND $position)
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(248, 36);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(BIND, "BIND"), root_1);

											DebugLocation(248, 41);
											adaptor.AddChild(root_1, stream_position.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:249:5: COLON name= id
							{
								DebugLocation(249, 5);
								COLON137 = (CommonToken)Match(input, COLON, Follow._COLON_in_bind_parameter1344);
								stream_COLON.Add(COLON137);

								DebugLocation(249, 15);
								PushFollow(Follow._id_in_bind_parameter1348);
								name = id();
								PopFollow();

								stream_id.Add(name.Tree);


								{
									// AST REWRITE
									// elements: name
									// token labels: 
									// rule labels: retval, name
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_name = new RewriteRuleSubtreeStream(adaptor, "rule name", name != null ? name.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 249:19: -> ^( BIND_NAME $name)
									{
										DebugLocation(249, 22);
										// C:\\Users\\Gareth\\Desktop\\test.g:249:22: ^( BIND_NAME $name)
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(249, 24);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(BIND_NAME, "BIND_NAME"), root_1);

											DebugLocation(249, 34);
											adaptor.AddChild(root_1, stream_name.NextTree());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 4:
							DebugEnterAlt(4);
							// C:\\Users\\Gareth\\Desktop\\test.g:250:5: AT name= id
							{
								DebugLocation(250, 5);
								AT138 = (CommonToken)Match(input, AT, Follow._AT_in_bind_parameter1363);
								stream_AT.Add(AT138);

								DebugLocation(250, 12);
								PushFollow(Follow._id_in_bind_parameter1367);
								name = id();
								PopFollow();

								stream_id.Add(name.Tree);


								{
									// AST REWRITE
									// elements: name
									// token labels: 
									// rule labels: retval, name
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_name = new RewriteRuleSubtreeStream(adaptor, "rule name", name != null ? name.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 250:16: -> ^( BIND_NAME $name)
									{
										DebugLocation(250, 19);
										// C:\\Users\\Gareth\\Desktop\\test.g:250:19: ^( BIND_NAME $name)
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(250, 21);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(BIND_NAME, "BIND_NAME"), root_1);

											DebugLocation(250, 31);
											adaptor.AddChild(root_1, stream_name.NextTree());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("bind_parameter", 23);
					LeaveRule("bind_parameter", 23);
					Leave_bind_parameter();
				}
				DebugLocation(253, 2);
			}
			finally { DebugExitRule(GrammarFileName, "bind_parameter"); }
			return retval;

		}
		// $ANTLR end "bind_parameter"

		public class raise_function_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_raise_function();
		partial void Leave_raise_function();

		// $ANTLR start "raise_function"
		// C:\\Users\\Gareth\\Desktop\\test.g:255:1: raise_function : RAISE LPAREN ( IGNORE | ( ROLLBACK | ABORT | FAIL ) COMMA error_message= STRING ) RPAREN ;
		[GrammarRule("raise_function")]
		private SQLiteParser.raise_function_return raise_function()
		{
			Enter_raise_function();
			EnterRule("raise_function", 24);
			TraceIn("raise_function", 24);
			SQLiteParser.raise_function_return retval = new SQLiteParser.raise_function_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken error_message = null;
			CommonToken RAISE139 = null;
			CommonToken LPAREN140 = null;
			CommonToken IGNORE141 = null;
			CommonToken set142 = null;
			CommonToken COMMA143 = null;
			CommonToken RPAREN144 = null;

			CommonTree error_message_tree = null;
			CommonTree RAISE139_tree = null;
			CommonTree LPAREN140_tree = null;
			CommonTree IGNORE141_tree = null;
			CommonTree set142_tree = null;
			CommonTree COMMA143_tree = null;
			CommonTree RPAREN144_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "raise_function");
				DebugLocation(255, 103);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:255:15: ( RAISE LPAREN ( IGNORE | ( ROLLBACK | ABORT | FAIL ) COMMA error_message= STRING ) RPAREN )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:255:17: RAISE LPAREN ( IGNORE | ( ROLLBACK | ABORT | FAIL ) COMMA error_message= STRING ) RPAREN
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(255, 22);
						RAISE139 = (CommonToken)Match(input, RAISE, Follow._RAISE_in_raise_function1388);
						RAISE139_tree = (CommonTree)adaptor.Create(RAISE139);
						root_0 = (CommonTree)adaptor.BecomeRoot(RAISE139_tree, root_0);

						DebugLocation(255, 30);
						LPAREN140 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_raise_function1391);
						DebugLocation(255, 32);
						// C:\\Users\\Gareth\\Desktop\\test.g:255:32: ( IGNORE | ( ROLLBACK | ABORT | FAIL ) COMMA error_message= STRING )
						int alt41 = 2;
						try
						{
							DebugEnterSubRule(41);
							try
							{
								DebugEnterDecision(41, decisionCanBacktrack[41]);
								int LA41_0 = input.LA(1);

								if ((LA41_0 == IGNORE))
								{
									alt41 = 1;
								}
								else if (((LA41_0 >= ROLLBACK && LA41_0 <= FAIL)))
								{
									alt41 = 2;
								}
								else
								{
									NoViableAltException nvae = new NoViableAltException("", 41, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
							}
							finally { DebugExitDecision(41); }
							switch (alt41)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:255:33: IGNORE
									{
										DebugLocation(255, 33);
										IGNORE141 = (CommonToken)Match(input, IGNORE, Follow._IGNORE_in_raise_function1395);
										IGNORE141_tree = (CommonTree)adaptor.Create(IGNORE141);
										adaptor.AddChild(root_0, IGNORE141_tree);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:255:42: ( ROLLBACK | ABORT | FAIL ) COMMA error_message= STRING
									{
										DebugLocation(255, 42);
										set142 = (CommonToken)input.LT(1);
										if ((input.LA(1) >= ROLLBACK && input.LA(1) <= FAIL))
										{
											input.Consume();
											adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set142));
											state.errorRecovery = false;
										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											throw mse;
										}

										DebugLocation(255, 73);
										COMMA143 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_raise_function1411);
										DebugLocation(255, 88);
										error_message = (CommonToken)Match(input, STRING, Follow._STRING_in_raise_function1416);
										error_message_tree = (CommonTree)adaptor.Create(error_message);
										adaptor.AddChild(root_0, error_message_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(41); }

						DebugLocation(255, 103);
						RPAREN144 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_raise_function1419);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("raise_function", 24);
					LeaveRule("raise_function", 24);
					Leave_raise_function();
				}
				DebugLocation(255, 103);
			}
			finally { DebugExitRule(GrammarFileName, "raise_function"); }
			return retval;

		}
		// $ANTLR end "raise_function"

		public class type_name_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_type_name();
		partial void Leave_type_name();

		// $ANTLR start "type_name"
		// C:\\Users\\Gareth\\Desktop\\test.g:257:1: type_name : (names+= ID )+ ( LPAREN size1= signed_number ( COMMA size2= signed_number )? RPAREN )? -> ^( TYPE ^( TYPE_PARAMS ( $size1)? ( $size2)? ) ( $names)+ ) ;
		[GrammarRule("type_name")]
		private SQLiteParser.type_name_return type_name()
		{
			Enter_type_name();
			EnterRule("type_name", 25);
			TraceIn("type_name", 25);
			SQLiteParser.type_name_return retval = new SQLiteParser.type_name_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken LPAREN145 = null;
			CommonToken COMMA146 = null;
			CommonToken RPAREN147 = null;
			CommonToken names = null;
			List list_names = null;
			SQLiteParser.signed_number_return size1 = default(SQLiteParser.signed_number_return);
			SQLiteParser.signed_number_return size2 = default(SQLiteParser.signed_number_return);

			CommonTree LPAREN145_tree = null;
			CommonTree COMMA146_tree = null;
			CommonTree RPAREN147_tree = null;
			CommonTree names_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_ID = new RewriteRuleITokenStream(adaptor, "token ID");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_signed_number = new RewriteRuleSubtreeStream(adaptor, "rule signed_number");
			try
			{
				DebugEnterRule(GrammarFileName, "type_name");
				DebugLocation(257, 49);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:257:10: ( (names+= ID )+ ( LPAREN size1= signed_number ( COMMA size2= signed_number )? RPAREN )? -> ^( TYPE ^( TYPE_PARAMS ( $size1)? ( $size2)? ) ( $names)+ ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:257:12: (names+= ID )+ ( LPAREN size1= signed_number ( COMMA size2= signed_number )? RPAREN )?
					{
						DebugLocation(257, 17);
						// C:\\Users\\Gareth\\Desktop\\test.g:257:17: (names+= ID )+
						int cnt42 = 0;
						try
						{
							DebugEnterSubRule(42);
							while (true)
							{
								int alt42 = 2;
								try
								{
									DebugEnterDecision(42, decisionCanBacktrack[42]);
									try
									{
										alt42 = dfa42.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(42); }
								switch (alt42)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:257:17: names+= ID
										{
											DebugLocation(257, 17);
											names = (CommonToken)Match(input, ID, Follow._ID_in_type_name1429);
											stream_ID.Add(names);

											if (list_names == null) list_names = new ArrayList();
											list_names.Add(names);


										}
										break;

									default:
										if (cnt42 >= 1)
											goto loop42;

										EarlyExitException eee42 = new EarlyExitException(42, input);
										DebugRecognitionException(eee42);
										throw eee42;
								}
								cnt42++;
							}
						loop42:
							;

						}
						finally { DebugExitSubRule(42); }

						DebugLocation(257, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:257:23: ( LPAREN size1= signed_number ( COMMA size2= signed_number )? RPAREN )?
						int alt44 = 2;
						try
						{
							DebugEnterSubRule(44);
							try
							{
								DebugEnterDecision(44, decisionCanBacktrack[44]);
								try
								{
									alt44 = dfa44.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(44); }
							switch (alt44)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:257:24: LPAREN size1= signed_number ( COMMA size2= signed_number )? RPAREN
									{
										DebugLocation(257, 24);
										LPAREN145 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_type_name1433);
										stream_LPAREN.Add(LPAREN145);

										DebugLocation(257, 36);
										PushFollow(Follow._signed_number_in_type_name1437);
										size1 = signed_number();
										PopFollow();

										stream_signed_number.Add(size1.Tree);
										DebugLocation(257, 51);
										// C:\\Users\\Gareth\\Desktop\\test.g:257:51: ( COMMA size2= signed_number )?
										int alt43 = 2;
										try
										{
											DebugEnterSubRule(43);
											try
											{
												DebugEnterDecision(43, decisionCanBacktrack[43]);
												int LA43_0 = input.LA(1);

												if ((LA43_0 == COMMA))
												{
													alt43 = 1;
												}
											}
											finally { DebugExitDecision(43); }
											switch (alt43)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:257:52: COMMA size2= signed_number
													{
														DebugLocation(257, 52);
														COMMA146 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_type_name1440);
														stream_COMMA.Add(COMMA146);

														DebugLocation(257, 63);
														PushFollow(Follow._signed_number_in_type_name1444);
														size2 = signed_number();
														PopFollow();

														stream_signed_number.Add(size2.Tree);

													}
													break;

											}
										}
										finally { DebugExitSubRule(43); }

										DebugLocation(257, 80);
										RPAREN147 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_type_name1448);
										stream_RPAREN.Add(RPAREN147);


									}
									break;

							}
						}
						finally { DebugExitSubRule(44); }



						{
							// AST REWRITE
							// elements: names, size1, size2
							// token labels: 
							// rule labels: retval, size2, size1
							// token list labels: names
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleITokenStream stream_names = new RewriteRuleITokenStream(adaptor, "token names", list_names);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_size2 = new RewriteRuleSubtreeStream(adaptor, "rule size2", size2 != null ? size2.Tree : null);
							RewriteRuleSubtreeStream stream_size1 = new RewriteRuleSubtreeStream(adaptor, "rule size1", size1 != null ? size1.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 258:1: -> ^( TYPE ^( TYPE_PARAMS ( $size1)? ( $size2)? ) ( $names)+ )
							{
								DebugLocation(258, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:258:4: ^( TYPE ^( TYPE_PARAMS ( $size1)? ( $size2)? ) ( $names)+ )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(258, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(TYPE, "TYPE"), root_1);

									DebugLocation(258, 11);
									// C:\\Users\\Gareth\\Desktop\\test.g:258:11: ^( TYPE_PARAMS ( $size1)? ( $size2)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(258, 13);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(TYPE_PARAMS, "TYPE_PARAMS"), root_2);

										DebugLocation(258, 25);
										// C:\\Users\\Gareth\\Desktop\\test.g:258:25: ( $size1)?
										if (stream_size1.HasNext)
										{
											DebugLocation(258, 25);
											adaptor.AddChild(root_2, stream_size1.NextTree());

										}
										stream_size1.Reset();
										DebugLocation(258, 33);
										// C:\\Users\\Gareth\\Desktop\\test.g:258:33: ( $size2)?
										if (stream_size2.HasNext)
										{
											DebugLocation(258, 33);
											adaptor.AddChild(root_2, stream_size2.NextTree());

										}
										stream_size2.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(258, 42);
									if (!(stream_names.HasNext))
									{
										throw new RewriteEarlyExitException();
									}
									while (stream_names.HasNext)
									{
										DebugLocation(258, 42);
										adaptor.AddChild(root_1, stream_names.NextNode());

									}
									stream_names.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("type_name", 25);
					LeaveRule("type_name", 25);
					Leave_type_name();
				}
				DebugLocation(258, 49);
			}
			finally { DebugExitRule(GrammarFileName, "type_name"); }
			return retval;

		}
		// $ANTLR end "type_name"

		public class signed_number_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_signed_number();
		partial void Leave_signed_number();

		// $ANTLR start "signed_number"
		// C:\\Users\\Gareth\\Desktop\\test.g:260:1: signed_number : ( PLUS | MINUS )? ( INTEGER | FLOAT ) ;
		[GrammarRule("signed_number")]
		private SQLiteParser.signed_number_return signed_number()
		{
			Enter_signed_number();
			EnterRule("signed_number", 26);
			TraceIn("signed_number", 26);
			SQLiteParser.signed_number_return retval = new SQLiteParser.signed_number_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set148 = null;
			CommonToken set149 = null;

			CommonTree set148_tree = null;
			CommonTree set149_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "signed_number");
				DebugLocation(260, 48);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:260:14: ( ( PLUS | MINUS )? ( INTEGER | FLOAT ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:260:16: ( PLUS | MINUS )? ( INTEGER | FLOAT )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(260, 16);
						// C:\\Users\\Gareth\\Desktop\\test.g:260:16: ( PLUS | MINUS )?
						int alt45 = 2;
						try
						{
							DebugEnterSubRule(45);
							try
							{
								DebugEnterDecision(45, decisionCanBacktrack[45]);
								int LA45_0 = input.LA(1);

								if (((LA45_0 >= PLUS && LA45_0 <= MINUS)))
								{
									alt45 = 1;
								}
							}
							finally { DebugExitDecision(45); }
							switch (alt45)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:
									{
										DebugLocation(260, 16);
										set148 = (CommonToken)input.LT(1);
										if ((input.LA(1) >= PLUS && input.LA(1) <= MINUS))
										{
											input.Consume();
											adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set148));
											state.errorRecovery = false;
										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											throw mse;
										}


									}
									break;

							}
						}
						finally { DebugExitSubRule(45); }

						DebugLocation(260, 32);
						set149 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= INTEGER && input.LA(1) <= FLOAT))
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set149));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("signed_number", 26);
					LeaveRule("signed_number", 26);
					Leave_signed_number();
				}
				DebugLocation(260, 48);
			}
			finally { DebugExitRule(GrammarFileName, "signed_number"); }
			return retval;

		}
		// $ANTLR end "signed_number"

		public class pragma_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_pragma_stmt();
		partial void Leave_pragma_stmt();

		// $ANTLR start "pragma_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:263:1: pragma_stmt : PRAGMA (database_name= id DOT )? pragma_name= id ( EQUALS pragma_value | LPAREN pragma_value RPAREN )? -> ^( PRAGMA ^( $pragma_name ( $database_name)? ) ( pragma_value )? ) ;
		[GrammarRule("pragma_stmt")]
		private SQLiteParser.pragma_stmt_return pragma_stmt()
		{
			Enter_pragma_stmt();
			EnterRule("pragma_stmt", 27);
			TraceIn("pragma_stmt", 27);
			SQLiteParser.pragma_stmt_return retval = new SQLiteParser.pragma_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken PRAGMA150 = null;
			CommonToken DOT151 = null;
			CommonToken EQUALS152 = null;
			CommonToken LPAREN154 = null;
			CommonToken RPAREN156 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return pragma_name = default(SQLiteParser.id_return);
			SQLiteParser.pragma_value_return pragma_value153 = default(SQLiteParser.pragma_value_return);
			SQLiteParser.pragma_value_return pragma_value155 = default(SQLiteParser.pragma_value_return);

			CommonTree PRAGMA150_tree = null;
			CommonTree DOT151_tree = null;
			CommonTree EQUALS152_tree = null;
			CommonTree LPAREN154_tree = null;
			CommonTree RPAREN156_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_EQUALS = new RewriteRuleITokenStream(adaptor, "token EQUALS");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleITokenStream stream_PRAGMA = new RewriteRuleITokenStream(adaptor, "token PRAGMA");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_pragma_value = new RewriteRuleSubtreeStream(adaptor, "rule pragma_value");
			try
			{
				DebugEnterRule(GrammarFileName, "pragma_stmt");
				DebugLocation(263, 58);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:263:12: ( PRAGMA (database_name= id DOT )? pragma_name= id ( EQUALS pragma_value | LPAREN pragma_value RPAREN )? -> ^( PRAGMA ^( $pragma_name ( $database_name)? ) ( pragma_value )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:263:14: PRAGMA (database_name= id DOT )? pragma_name= id ( EQUALS pragma_value | LPAREN pragma_value RPAREN )?
					{
						DebugLocation(263, 14);
						PRAGMA150 = (CommonToken)Match(input, PRAGMA, Follow._PRAGMA_in_pragma_stmt1502);
						stream_PRAGMA.Add(PRAGMA150);

						DebugLocation(263, 21);
						// C:\\Users\\Gareth\\Desktop\\test.g:263:21: (database_name= id DOT )?
						int alt46 = 2;
						try
						{
							DebugEnterSubRule(46);
							try
							{
								DebugEnterDecision(46, decisionCanBacktrack[46]);
								try
								{
									alt46 = dfa46.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(46); }
							switch (alt46)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:263:22: database_name= id DOT
									{
										DebugLocation(263, 35);
										PushFollow(Follow._id_in_pragma_stmt1507);
										database_name = id();
										PopFollow();

										stream_id.Add(database_name.Tree);
										DebugLocation(263, 39);
										DOT151 = (CommonToken)Match(input, DOT, Follow._DOT_in_pragma_stmt1509);
										stream_DOT.Add(DOT151);


									}
									break;

							}
						}
						finally { DebugExitSubRule(46); }

						DebugLocation(263, 56);
						PushFollow(Follow._id_in_pragma_stmt1515);
						pragma_name = id();
						PopFollow();

						stream_id.Add(pragma_name.Tree);
						DebugLocation(263, 60);
						// C:\\Users\\Gareth\\Desktop\\test.g:263:60: ( EQUALS pragma_value | LPAREN pragma_value RPAREN )?
						int alt47 = 3;
						try
						{
							DebugEnterSubRule(47);
							try
							{
								DebugEnterDecision(47, decisionCanBacktrack[47]);
								int LA47_0 = input.LA(1);

								if ((LA47_0 == EQUALS))
								{
									alt47 = 1;
								}
								else if ((LA47_0 == LPAREN))
								{
									alt47 = 2;
								}
							}
							finally { DebugExitDecision(47); }
							switch (alt47)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:263:61: EQUALS pragma_value
									{
										DebugLocation(263, 61);
										EQUALS152 = (CommonToken)Match(input, EQUALS, Follow._EQUALS_in_pragma_stmt1518);
										stream_EQUALS.Add(EQUALS152);

										DebugLocation(263, 68);
										PushFollow(Follow._pragma_value_in_pragma_stmt1520);
										pragma_value153 = pragma_value();
										PopFollow();

										stream_pragma_value.Add(pragma_value153.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:263:83: LPAREN pragma_value RPAREN
									{
										DebugLocation(263, 83);
										LPAREN154 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_pragma_stmt1524);
										stream_LPAREN.Add(LPAREN154);

										DebugLocation(263, 90);
										PushFollow(Follow._pragma_value_in_pragma_stmt1526);
										pragma_value155 = pragma_value();
										PopFollow();

										stream_pragma_value.Add(pragma_value155.Tree);
										DebugLocation(263, 103);
										RPAREN156 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_pragma_stmt1528);
										stream_RPAREN.Add(RPAREN156);


									}
									break;

							}
						}
						finally { DebugExitSubRule(47); }



						{
							// AST REWRITE
							// elements: PRAGMA, database_name, pragma_value, pragma_name
							// token labels: 
							// rule labels: database_name, retval, pragma_name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_pragma_name = new RewriteRuleSubtreeStream(adaptor, "rule pragma_name", pragma_name != null ? pragma_name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 264:1: -> ^( PRAGMA ^( $pragma_name ( $database_name)? ) ( pragma_value )? )
							{
								DebugLocation(264, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:264:4: ^( PRAGMA ^( $pragma_name ( $database_name)? ) ( pragma_value )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(264, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_PRAGMA.NextNode(), root_1);

									DebugLocation(264, 13);
									// C:\\Users\\Gareth\\Desktop\\test.g:264:13: ^( $pragma_name ( $database_name)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(264, 15);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_pragma_name.NextNode(), root_2);

										DebugLocation(264, 28);
										// C:\\Users\\Gareth\\Desktop\\test.g:264:28: ( $database_name)?
										if (stream_database_name.HasNext)
										{
											DebugLocation(264, 28);
											adaptor.AddChild(root_2, stream_database_name.NextTree());

										}
										stream_database_name.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(264, 45);
									// C:\\Users\\Gareth\\Desktop\\test.g:264:45: ( pragma_value )?
									if (stream_pragma_value.HasNext)
									{
										DebugLocation(264, 45);
										adaptor.AddChild(root_1, stream_pragma_value.NextTree());

									}
									stream_pragma_value.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("pragma_stmt", 27);
					LeaveRule("pragma_stmt", 27);
					Leave_pragma_stmt();
				}
				DebugLocation(264, 58);
			}
			finally { DebugExitRule(GrammarFileName, "pragma_stmt"); }
			return retval;

		}
		// $ANTLR end "pragma_stmt"

		public class pragma_value_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_pragma_value();
		partial void Leave_pragma_value();

		// $ANTLR start "pragma_value"
		// C:\\Users\\Gareth\\Desktop\\test.g:266:1: pragma_value : ( signed_number -> ^( FLOAT_LITERAL signed_number ) | ID -> ^( ID_LITERAL ID ) | STRING -> ^( STRING_LITERAL STRING ) );
		[GrammarRule("pragma_value")]
		private SQLiteParser.pragma_value_return pragma_value()
		{
			Enter_pragma_value();
			EnterRule("pragma_value", 28);
			TraceIn("pragma_value", 28);
			SQLiteParser.pragma_value_return retval = new SQLiteParser.pragma_value_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ID158 = null;
			CommonToken STRING159 = null;
			SQLiteParser.signed_number_return signed_number157 = default(SQLiteParser.signed_number_return);

			CommonTree ID158_tree = null;
			CommonTree STRING159_tree = null;
			RewriteRuleITokenStream stream_ID = new RewriteRuleITokenStream(adaptor, "token ID");
			RewriteRuleITokenStream stream_STRING = new RewriteRuleITokenStream(adaptor, "token STRING");
			RewriteRuleSubtreeStream stream_signed_number = new RewriteRuleSubtreeStream(adaptor, "rule signed_number");
			try
			{
				DebugEnterRule(GrammarFileName, "pragma_value");
				DebugLocation(266, 1);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:267:2: ( signed_number -> ^( FLOAT_LITERAL signed_number ) | ID -> ^( ID_LITERAL ID ) | STRING -> ^( STRING_LITERAL STRING ) )
					int alt48 = 3;
					try
					{
						DebugEnterDecision(48, decisionCanBacktrack[48]);
						switch (input.LA(1))
						{
							case PLUS:
							case MINUS:
							case INTEGER:
							case FLOAT:
								{
									alt48 = 1;
								}
								break;
							case ID:
								{
									alt48 = 2;
								}
								break;
							case STRING:
								{
									alt48 = 3;
								}
								break;
							default:
								{
									NoViableAltException nvae = new NoViableAltException("", 48, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
						}

					}
					finally { DebugExitDecision(48); }
					switch (alt48)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:267:4: signed_number
							{
								DebugLocation(267, 4);
								PushFollow(Follow._signed_number_in_pragma_value1557);
								signed_number157 = signed_number();
								PopFollow();

								stream_signed_number.Add(signed_number157.Tree);


								{
									// AST REWRITE
									// elements: signed_number
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 267:18: -> ^( FLOAT_LITERAL signed_number )
									{
										DebugLocation(267, 21);
										// C:\\Users\\Gareth\\Desktop\\test.g:267:21: ^( FLOAT_LITERAL signed_number )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(267, 23);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FLOAT_LITERAL, "FLOAT_LITERAL"), root_1);

											DebugLocation(267, 37);
											adaptor.AddChild(root_1, stream_signed_number.NextTree());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:268:4: ID
							{
								DebugLocation(268, 4);
								ID158 = (CommonToken)Match(input, ID, Follow._ID_in_pragma_value1570);
								stream_ID.Add(ID158);



								{
									// AST REWRITE
									// elements: ID
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 268:7: -> ^( ID_LITERAL ID )
									{
										DebugLocation(268, 10);
										// C:\\Users\\Gareth\\Desktop\\test.g:268:10: ^( ID_LITERAL ID )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(268, 12);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(ID_LITERAL, "ID_LITERAL"), root_1);

											DebugLocation(268, 23);
											adaptor.AddChild(root_1, stream_ID.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:269:4: STRING
							{
								DebugLocation(269, 4);
								STRING159 = (CommonToken)Match(input, STRING, Follow._STRING_in_pragma_value1583);
								stream_STRING.Add(STRING159);



								{
									// AST REWRITE
									// elements: STRING
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 269:11: -> ^( STRING_LITERAL STRING )
									{
										DebugLocation(269, 14);
										// C:\\Users\\Gareth\\Desktop\\test.g:269:14: ^( STRING_LITERAL STRING )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(269, 16);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(STRING_LITERAL, "STRING_LITERAL"), root_1);

											DebugLocation(269, 31);
											adaptor.AddChild(root_1, stream_STRING.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("pragma_value", 28);
					LeaveRule("pragma_value", 28);
					Leave_pragma_value();
				}
				DebugLocation(270, 1);
			}
			finally { DebugExitRule(GrammarFileName, "pragma_value"); }
			return retval;

		}
		// $ANTLR end "pragma_value"

		public class attach_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_attach_stmt();
		partial void Leave_attach_stmt();

		// $ANTLR start "attach_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:273:1: attach_stmt : ATTACH ( DATABASE )? filename= id AS database_name= id ;
		[GrammarRule("attach_stmt")]
		private SQLiteParser.attach_stmt_return attach_stmt()
		{
			Enter_attach_stmt();
			EnterRule("attach_stmt", 29);
			TraceIn("attach_stmt", 29);
			SQLiteParser.attach_stmt_return retval = new SQLiteParser.attach_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ATTACH160 = null;
			CommonToken DATABASE161 = null;
			CommonToken AS162 = null;
			SQLiteParser.id_return filename = default(SQLiteParser.id_return);
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);

			CommonTree ATTACH160_tree = null;
			CommonTree DATABASE161_tree = null;
			CommonTree AS162_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "attach_stmt");
				DebugLocation(273, 63);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:273:12: ( ATTACH ( DATABASE )? filename= id AS database_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:273:14: ATTACH ( DATABASE )? filename= id AS database_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(273, 14);
						ATTACH160 = (CommonToken)Match(input, ATTACH, Follow._ATTACH_in_attach_stmt1601);
						ATTACH160_tree = (CommonTree)adaptor.Create(ATTACH160);
						adaptor.AddChild(root_0, ATTACH160_tree);

						DebugLocation(273, 21);
						// C:\\Users\\Gareth\\Desktop\\test.g:273:21: ( DATABASE )?
						int alt49 = 2;
						try
						{
							DebugEnterSubRule(49);
							try
							{
								DebugEnterDecision(49, decisionCanBacktrack[49]);
								try
								{
									alt49 = dfa49.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(49); }
							switch (alt49)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:273:22: DATABASE
									{
										DebugLocation(273, 22);
										DATABASE161 = (CommonToken)Match(input, DATABASE, Follow._DATABASE_in_attach_stmt1604);
										DATABASE161_tree = (CommonTree)adaptor.Create(DATABASE161);
										adaptor.AddChild(root_0, DATABASE161_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(49); }

						DebugLocation(273, 41);
						PushFollow(Follow._id_in_attach_stmt1610);
						filename = id();
						PopFollow();

						adaptor.AddChild(root_0, filename.Tree);
						DebugLocation(273, 45);
						AS162 = (CommonToken)Match(input, AS, Follow._AS_in_attach_stmt1612);
						AS162_tree = (CommonTree)adaptor.Create(AS162);
						adaptor.AddChild(root_0, AS162_tree);

						DebugLocation(273, 61);
						PushFollow(Follow._id_in_attach_stmt1616);
						database_name = id();
						PopFollow();

						adaptor.AddChild(root_0, database_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("attach_stmt", 29);
					LeaveRule("attach_stmt", 29);
					Leave_attach_stmt();
				}
				DebugLocation(273, 63);
			}
			finally { DebugExitRule(GrammarFileName, "attach_stmt"); }
			return retval;

		}
		// $ANTLR end "attach_stmt"

		public class detach_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_detach_stmt();
		partial void Leave_detach_stmt();

		// $ANTLR start "detach_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:276:1: detach_stmt : DETACH ( DATABASE )? database_name= id ;
		[GrammarRule("detach_stmt")]
		private SQLiteParser.detach_stmt_return detach_stmt()
		{
			Enter_detach_stmt();
			EnterRule("detach_stmt", 30);
			TraceIn("detach_stmt", 30);
			SQLiteParser.detach_stmt_return retval = new SQLiteParser.detach_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DETACH163 = null;
			CommonToken DATABASE164 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);

			CommonTree DETACH163_tree = null;
			CommonTree DATABASE164_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "detach_stmt");
				DebugLocation(276, 48);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:276:12: ( DETACH ( DATABASE )? database_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:276:14: DETACH ( DATABASE )? database_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(276, 14);
						DETACH163 = (CommonToken)Match(input, DETACH, Follow._DETACH_in_detach_stmt1624);
						DETACH163_tree = (CommonTree)adaptor.Create(DETACH163);
						adaptor.AddChild(root_0, DETACH163_tree);

						DebugLocation(276, 21);
						// C:\\Users\\Gareth\\Desktop\\test.g:276:21: ( DATABASE )?
						int alt50 = 2;
						try
						{
							DebugEnterSubRule(50);
							try
							{
								DebugEnterDecision(50, decisionCanBacktrack[50]);
								int LA50_0 = input.LA(1);

								if ((LA50_0 == DATABASE))
								{
									int LA50_1 = input.LA(2);

									if (((LA50_1 >= EXPLAIN && LA50_1 <= PLAN) || (LA50_1 >= INDEXED && LA50_1 <= BY) || (LA50_1 >= OR && LA50_1 <= ESCAPE) || (LA50_1 >= IS && LA50_1 <= BETWEEN) || (LA50_1 >= COLLATE && LA50_1 <= THEN) || LA50_1 == STRING || (LA50_1 >= CURRENT_TIME && LA50_1 <= CURRENT_TIMESTAMP) || (LA50_1 >= RAISE && LA50_1 <= ROW)))
									{
										alt50 = 1;
									}
								}
							}
							finally { DebugExitDecision(50); }
							switch (alt50)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:276:22: DATABASE
									{
										DebugLocation(276, 22);
										DATABASE164 = (CommonToken)Match(input, DATABASE, Follow._DATABASE_in_detach_stmt1627);
										DATABASE164_tree = (CommonTree)adaptor.Create(DATABASE164);
										adaptor.AddChild(root_0, DATABASE164_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(50); }

						DebugLocation(276, 46);
						PushFollow(Follow._id_in_detach_stmt1633);
						database_name = id();
						PopFollow();

						adaptor.AddChild(root_0, database_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("detach_stmt", 30);
					LeaveRule("detach_stmt", 30);
					Leave_detach_stmt();
				}
				DebugLocation(276, 48);
			}
			finally { DebugExitRule(GrammarFileName, "detach_stmt"); }
			return retval;

		}
		// $ANTLR end "detach_stmt"

		public class analyze_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_analyze_stmt();
		partial void Leave_analyze_stmt();

		// $ANTLR start "analyze_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:279:1: analyze_stmt : ANALYZE (database_or_table_name= id | database_name= id DOT table_name= id )? ;
		[GrammarRule("analyze_stmt")]
		private SQLiteParser.analyze_stmt_return analyze_stmt()
		{
			Enter_analyze_stmt();
			EnterRule("analyze_stmt", 31);
			TraceIn("analyze_stmt", 31);
			SQLiteParser.analyze_stmt_return retval = new SQLiteParser.analyze_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ANALYZE165 = null;
			CommonToken DOT166 = null;
			SQLiteParser.id_return database_or_table_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);

			CommonTree ANALYZE165_tree = null;
			CommonTree DOT166_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "analyze_stmt");
				DebugLocation(279, 87);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:279:13: ( ANALYZE (database_or_table_name= id | database_name= id DOT table_name= id )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:279:15: ANALYZE (database_or_table_name= id | database_name= id DOT table_name= id )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(279, 15);
						ANALYZE165 = (CommonToken)Match(input, ANALYZE, Follow._ANALYZE_in_analyze_stmt1641);
						ANALYZE165_tree = (CommonTree)adaptor.Create(ANALYZE165);
						adaptor.AddChild(root_0, ANALYZE165_tree);

						DebugLocation(279, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:279:23: (database_or_table_name= id | database_name= id DOT table_name= id )?
						int alt51 = 3;
						try
						{
							DebugEnterSubRule(51);
							try
							{
								DebugEnterDecision(51, decisionCanBacktrack[51]);
								try
								{
									alt51 = dfa51.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(51); }
							switch (alt51)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:279:24: database_or_table_name= id
									{
										DebugLocation(279, 46);
										PushFollow(Follow._id_in_analyze_stmt1646);
										database_or_table_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_or_table_name.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:279:52: database_name= id DOT table_name= id
									{
										DebugLocation(279, 65);
										PushFollow(Follow._id_in_analyze_stmt1652);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(279, 69);
										DOT166 = (CommonToken)Match(input, DOT, Follow._DOT_in_analyze_stmt1654);
										DOT166_tree = (CommonTree)adaptor.Create(DOT166);
										adaptor.AddChild(root_0, DOT166_tree);

										DebugLocation(279, 83);
										PushFollow(Follow._id_in_analyze_stmt1658);
										table_name = id();
										PopFollow();

										adaptor.AddChild(root_0, table_name.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(51); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("analyze_stmt", 31);
					LeaveRule("analyze_stmt", 31);
					Leave_analyze_stmt();
				}
				DebugLocation(279, 87);
			}
			finally { DebugExitRule(GrammarFileName, "analyze_stmt"); }
			return retval;

		}
		// $ANTLR end "analyze_stmt"

		public class reindex_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_reindex_stmt();
		partial void Leave_reindex_stmt();

		// $ANTLR start "reindex_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:282:1: reindex_stmt : REINDEX (database_name= id DOT )? collation_or_table_or_index_name= id ;
		[GrammarRule("reindex_stmt")]
		private SQLiteParser.reindex_stmt_return reindex_stmt()
		{
			Enter_reindex_stmt();
			EnterRule("reindex_stmt", 32);
			TraceIn("reindex_stmt", 32);
			SQLiteParser.reindex_stmt_return retval = new SQLiteParser.reindex_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken REINDEX167 = null;
			CommonToken DOT168 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return collation_or_table_or_index_name = default(SQLiteParser.id_return);

			CommonTree REINDEX167_tree = null;
			CommonTree DOT168_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "reindex_stmt");
				DebugLocation(282, 81);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:282:13: ( REINDEX (database_name= id DOT )? collation_or_table_or_index_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:282:15: REINDEX (database_name= id DOT )? collation_or_table_or_index_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(282, 15);
						REINDEX167 = (CommonToken)Match(input, REINDEX, Follow._REINDEX_in_reindex_stmt1668);
						REINDEX167_tree = (CommonTree)adaptor.Create(REINDEX167);
						adaptor.AddChild(root_0, REINDEX167_tree);

						DebugLocation(282, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:282:23: (database_name= id DOT )?
						int alt52 = 2;
						try
						{
							DebugEnterSubRule(52);
							try
							{
								DebugEnterDecision(52, decisionCanBacktrack[52]);
								int LA52_0 = input.LA(1);

								if ((LA52_0 == ID || LA52_0 == STRING))
								{
									int LA52_1 = input.LA(2);

									if ((LA52_1 == DOT))
									{
										alt52 = 1;
									}
								}
								else if (((LA52_0 >= EXPLAIN && LA52_0 <= PLAN) || (LA52_0 >= INDEXED && LA52_0 <= BY) || (LA52_0 >= OR && LA52_0 <= ESCAPE) || (LA52_0 >= IS && LA52_0 <= BETWEEN) || LA52_0 == COLLATE || (LA52_0 >= DISTINCT && LA52_0 <= THEN) || (LA52_0 >= CURRENT_TIME && LA52_0 <= CURRENT_TIMESTAMP) || (LA52_0 >= RAISE && LA52_0 <= ROW)))
								{
									int LA52_2 = input.LA(2);

									if ((LA52_2 == DOT))
									{
										alt52 = 1;
									}
								}
							}
							finally { DebugExitDecision(52); }
							switch (alt52)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:282:24: database_name= id DOT
									{
										DebugLocation(282, 37);
										PushFollow(Follow._id_in_reindex_stmt1673);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(282, 41);
										DOT168 = (CommonToken)Match(input, DOT, Follow._DOT_in_reindex_stmt1675);
										DOT168_tree = (CommonTree)adaptor.Create(DOT168);
										adaptor.AddChild(root_0, DOT168_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(52); }

						DebugLocation(282, 79);
						PushFollow(Follow._id_in_reindex_stmt1681);
						collation_or_table_or_index_name = id();
						PopFollow();

						adaptor.AddChild(root_0, collation_or_table_or_index_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("reindex_stmt", 32);
					LeaveRule("reindex_stmt", 32);
					Leave_reindex_stmt();
				}
				DebugLocation(282, 81);
			}
			finally { DebugExitRule(GrammarFileName, "reindex_stmt"); }
			return retval;

		}
		// $ANTLR end "reindex_stmt"

		public class vacuum_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_vacuum_stmt();
		partial void Leave_vacuum_stmt();

		// $ANTLR start "vacuum_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:285:1: vacuum_stmt : VACUUM ;
		[GrammarRule("vacuum_stmt")]
		private SQLiteParser.vacuum_stmt_return vacuum_stmt()
		{
			Enter_vacuum_stmt();
			EnterRule("vacuum_stmt", 33);
			TraceIn("vacuum_stmt", 33);
			SQLiteParser.vacuum_stmt_return retval = new SQLiteParser.vacuum_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken VACUUM169 = null;

			CommonTree VACUUM169_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "vacuum_stmt");
				DebugLocation(285, 19);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:285:12: ( VACUUM )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:285:14: VACUUM
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(285, 14);
						VACUUM169 = (CommonToken)Match(input, VACUUM, Follow._VACUUM_in_vacuum_stmt1689);
						VACUUM169_tree = (CommonTree)adaptor.Create(VACUUM169);
						adaptor.AddChild(root_0, VACUUM169_tree);


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("vacuum_stmt", 33);
					LeaveRule("vacuum_stmt", 33);
					Leave_vacuum_stmt();
				}
				DebugLocation(285, 19);
			}
			finally { DebugExitRule(GrammarFileName, "vacuum_stmt"); }
			return retval;

		}
		// $ANTLR end "vacuum_stmt"

		public class operation_conflict_clause_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_operation_conflict_clause();
		partial void Leave_operation_conflict_clause();

		// $ANTLR start "operation_conflict_clause"
		// C:\\Users\\Gareth\\Desktop\\test.g:291:1: operation_conflict_clause : OR ( ROLLBACK | ABORT | FAIL | IGNORE | REPLACE ) ;
		[GrammarRule("operation_conflict_clause")]
		private SQLiteParser.operation_conflict_clause_return operation_conflict_clause()
		{
			Enter_operation_conflict_clause();
			EnterRule("operation_conflict_clause", 34);
			TraceIn("operation_conflict_clause", 34);
			SQLiteParser.operation_conflict_clause_return retval = new SQLiteParser.operation_conflict_clause_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken OR170 = null;
			CommonToken set171 = null;

			CommonTree OR170_tree = null;
			CommonTree set171_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "operation_conflict_clause");
				DebugLocation(291, 74);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:291:26: ( OR ( ROLLBACK | ABORT | FAIL | IGNORE | REPLACE ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:291:28: OR ( ROLLBACK | ABORT | FAIL | IGNORE | REPLACE )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(291, 28);
						OR170 = (CommonToken)Match(input, OR, Follow._OR_in_operation_conflict_clause1700);
						OR170_tree = (CommonTree)adaptor.Create(OR170);
						adaptor.AddChild(root_0, OR170_tree);

						DebugLocation(291, 31);
						set171 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= IGNORE && input.LA(1) <= FAIL) || input.LA(1) == REPLACE)
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set171));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("operation_conflict_clause", 34);
					LeaveRule("operation_conflict_clause", 34);
					Leave_operation_conflict_clause();
				}
				DebugLocation(291, 74);
			}
			finally { DebugExitRule(GrammarFileName, "operation_conflict_clause"); }
			return retval;

		}
		// $ANTLR end "operation_conflict_clause"

		public class ordering_term_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_ordering_term();
		partial void Leave_ordering_term();

		// $ANTLR start "ordering_term"
		// C:\\Users\\Gareth\\Desktop\\test.g:293:1: ordering_term : expr ( ASC | DESC )? -> ^( ORDERING expr ( ASC )? ( DESC )? ) ;
		[GrammarRule("ordering_term")]
		private SQLiteParser.ordering_term_return ordering_term()
		{
			Enter_ordering_term();
			EnterRule("ordering_term", 35);
			TraceIn("ordering_term", 35);
			SQLiteParser.ordering_term_return retval = new SQLiteParser.ordering_term_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ASC173 = null;
			CommonToken DESC174 = null;
			SQLiteParser.expr_return expr172 = default(SQLiteParser.expr_return);

			CommonTree ASC173_tree = null;
			CommonTree DESC174_tree = null;
			RewriteRuleITokenStream stream_ASC = new RewriteRuleITokenStream(adaptor, "token ASC");
			RewriteRuleITokenStream stream_DESC = new RewriteRuleITokenStream(adaptor, "token DESC");
			RewriteRuleSubtreeStream stream_expr = new RewriteRuleSubtreeStream(adaptor, "rule expr");
			try
			{
				DebugEnterRule(GrammarFileName, "ordering_term");
				DebugLocation(293, 34);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:293:14: ( expr ( ASC | DESC )? -> ^( ORDERING expr ( ASC )? ( DESC )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:293:16: expr ( ASC | DESC )?
					{
						DebugLocation(293, 16);
						PushFollow(Follow._expr_in_ordering_term1727);
						expr172 = expr();
						PopFollow();

						stream_expr.Add(expr172.Tree);
						DebugLocation(293, 82);
						// C:\\Users\\Gareth\\Desktop\\test.g:293:82: ( ASC | DESC )?
						int alt53 = 3;
						try
						{
							DebugEnterSubRule(53);
							try
							{
								DebugEnterDecision(53, decisionCanBacktrack[53]);
								try
								{
									alt53 = dfa53.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(53); }
							switch (alt53)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:293:83: ASC
									{
										DebugLocation(293, 83);
										ASC173 = (CommonToken)Match(input, ASC, Follow._ASC_in_ordering_term1732);
										stream_ASC.Add(ASC173);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:293:89: DESC
									{
										DebugLocation(293, 89);
										DESC174 = (CommonToken)Match(input, DESC, Follow._DESC_in_ordering_term1736);
										stream_DESC.Add(DESC174);


									}
									break;

							}
						}
						finally { DebugExitSubRule(53); }



						{
							// AST REWRITE
							// elements: DESC, ASC, expr
							// token labels: 
							// rule labels: retval
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 294:1: -> ^( ORDERING expr ( ASC )? ( DESC )? )
							{
								DebugLocation(294, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:294:4: ^( ORDERING expr ( ASC )? ( DESC )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(294, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(ORDERING, "ORDERING"), root_1);

									DebugLocation(294, 15);
									adaptor.AddChild(root_1, stream_expr.NextTree());
									DebugLocation(294, 20);
									// C:\\Users\\Gareth\\Desktop\\test.g:294:20: ( ASC )?
									if (stream_ASC.HasNext)
									{
										DebugLocation(294, 21);
										adaptor.AddChild(root_1, stream_ASC.NextNode());

									}
									stream_ASC.Reset();
									DebugLocation(294, 27);
									// C:\\Users\\Gareth\\Desktop\\test.g:294:27: ( DESC )?
									if (stream_DESC.HasNext)
									{
										DebugLocation(294, 28);
										adaptor.AddChild(root_1, stream_DESC.NextNode());

									}
									stream_DESC.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("ordering_term", 35);
					LeaveRule("ordering_term", 35);
					Leave_ordering_term();
				}
				DebugLocation(294, 34);
			}
			finally { DebugExitRule(GrammarFileName, "ordering_term"); }
			return retval;

		}
		// $ANTLR end "ordering_term"

		public class operation_limited_clause_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_operation_limited_clause();
		partial void Leave_operation_limited_clause();

		// $ANTLR start "operation_limited_clause"
		// C:\\Users\\Gareth\\Desktop\\test.g:296:1: operation_limited_clause : ( ORDER BY ordering_term ( COMMA ordering_term )* )? LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )? ;
		[GrammarRule("operation_limited_clause")]
		private SQLiteParser.operation_limited_clause_return operation_limited_clause()
		{
			Enter_operation_limited_clause();
			EnterRule("operation_limited_clause", 36);
			TraceIn("operation_limited_clause", 36);
			SQLiteParser.operation_limited_clause_return retval = new SQLiteParser.operation_limited_clause_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken limit = null;
			CommonToken offset = null;
			CommonToken ORDER175 = null;
			CommonToken BY176 = null;
			CommonToken COMMA178 = null;
			CommonToken LIMIT180 = null;
			CommonToken set181 = null;
			SQLiteParser.ordering_term_return ordering_term177 = default(SQLiteParser.ordering_term_return);
			SQLiteParser.ordering_term_return ordering_term179 = default(SQLiteParser.ordering_term_return);

			CommonTree limit_tree = null;
			CommonTree offset_tree = null;
			CommonTree ORDER175_tree = null;
			CommonTree BY176_tree = null;
			CommonTree COMMA178_tree = null;
			CommonTree LIMIT180_tree = null;
			CommonTree set181_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "operation_limited_clause");
				DebugLocation(296, 56);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:296:25: ( ( ORDER BY ordering_term ( COMMA ordering_term )* )? LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:297:3: ( ORDER BY ordering_term ( COMMA ordering_term )* )? LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(297, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:297:3: ( ORDER BY ordering_term ( COMMA ordering_term )* )?
						int alt55 = 2;
						try
						{
							DebugEnterSubRule(55);
							try
							{
								DebugEnterDecision(55, decisionCanBacktrack[55]);
								int LA55_0 = input.LA(1);

								if ((LA55_0 == ORDER))
								{
									alt55 = 1;
								}
							}
							finally { DebugExitDecision(55); }
							switch (alt55)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:297:4: ORDER BY ordering_term ( COMMA ordering_term )*
									{
										DebugLocation(297, 4);
										ORDER175 = (CommonToken)Match(input, ORDER, Follow._ORDER_in_operation_limited_clause1766);
										ORDER175_tree = (CommonTree)adaptor.Create(ORDER175);
										adaptor.AddChild(root_0, ORDER175_tree);

										DebugLocation(297, 10);
										BY176 = (CommonToken)Match(input, BY, Follow._BY_in_operation_limited_clause1768);
										BY176_tree = (CommonTree)adaptor.Create(BY176);
										adaptor.AddChild(root_0, BY176_tree);

										DebugLocation(297, 13);
										PushFollow(Follow._ordering_term_in_operation_limited_clause1770);
										ordering_term177 = ordering_term();
										PopFollow();

										adaptor.AddChild(root_0, ordering_term177.Tree);
										DebugLocation(297, 27);
										// C:\\Users\\Gareth\\Desktop\\test.g:297:27: ( COMMA ordering_term )*
										try
										{
											DebugEnterSubRule(54);
											while (true)
											{
												int alt54 = 2;
												try
												{
													DebugEnterDecision(54, decisionCanBacktrack[54]);
													int LA54_0 = input.LA(1);

													if ((LA54_0 == COMMA))
													{
														alt54 = 1;
													}


												}
												finally { DebugExitDecision(54); }
												switch (alt54)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:297:28: COMMA ordering_term
														{
															DebugLocation(297, 28);
															COMMA178 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_operation_limited_clause1773);
															COMMA178_tree = (CommonTree)adaptor.Create(COMMA178);
															adaptor.AddChild(root_0, COMMA178_tree);

															DebugLocation(297, 34);
															PushFollow(Follow._ordering_term_in_operation_limited_clause1775);
															ordering_term179 = ordering_term();
															PopFollow();

															adaptor.AddChild(root_0, ordering_term179.Tree);

														}
														break;

													default:
														goto loop54;
												}
											}

										loop54:
											;

										}
										finally { DebugExitSubRule(54); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(55); }

						DebugLocation(298, 3);
						LIMIT180 = (CommonToken)Match(input, LIMIT, Follow._LIMIT_in_operation_limited_clause1783);
						LIMIT180_tree = (CommonTree)adaptor.Create(LIMIT180);
						adaptor.AddChild(root_0, LIMIT180_tree);

						DebugLocation(298, 14);
						limit = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_operation_limited_clause1787);
						limit_tree = (CommonTree)adaptor.Create(limit);
						adaptor.AddChild(root_0, limit_tree);

						DebugLocation(298, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:298:23: ( ( OFFSET | COMMA ) offset= INTEGER )?
						int alt56 = 2;
						try
						{
							DebugEnterSubRule(56);
							try
							{
								DebugEnterDecision(56, decisionCanBacktrack[56]);
								int LA56_0 = input.LA(1);

								if ((LA56_0 == COMMA || LA56_0 == OFFSET))
								{
									alt56 = 1;
								}
							}
							finally { DebugExitDecision(56); }
							switch (alt56)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:298:24: ( OFFSET | COMMA ) offset= INTEGER
									{
										DebugLocation(298, 24);
										set181 = (CommonToken)input.LT(1);
										if (input.LA(1) == COMMA || input.LA(1) == OFFSET)
										{
											input.Consume();
											adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set181));
											state.errorRecovery = false;
										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											throw mse;
										}

										DebugLocation(298, 47);
										offset = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_operation_limited_clause1800);
										offset_tree = (CommonTree)adaptor.Create(offset);
										adaptor.AddChild(root_0, offset_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(56); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("operation_limited_clause", 36);
					LeaveRule("operation_limited_clause", 36);
					Leave_operation_limited_clause();
				}
				DebugLocation(298, 56);
			}
			finally { DebugExitRule(GrammarFileName, "operation_limited_clause"); }
			return retval;

		}
		// $ANTLR end "operation_limited_clause"

		public class select_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_select_stmt();
		partial void Leave_select_stmt();

		// $ANTLR start "select_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:301:1: select_stmt : select_list ( ORDER BY ordering_term ( COMMA ordering_term )* )? ( LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )? )? -> ^( SELECT select_list ( ^( ORDER ( ordering_term )+ ) )? ( ^( LIMIT $limit ( $offset)? ) )? ) ;
		[GrammarRule("select_stmt")]
		private SQLiteParser.select_stmt_return select_stmt()
		{
			Enter_select_stmt();
			EnterRule("select_stmt", 37);
			TraceIn("select_stmt", 37);
			SQLiteParser.select_stmt_return retval = new SQLiteParser.select_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken limit = null;
			CommonToken offset = null;
			CommonToken ORDER183 = null;
			CommonToken BY184 = null;
			CommonToken COMMA186 = null;
			CommonToken LIMIT188 = null;
			CommonToken OFFSET189 = null;
			CommonToken COMMA190 = null;
			SQLiteParser.select_list_return select_list182 = default(SQLiteParser.select_list_return);
			SQLiteParser.ordering_term_return ordering_term185 = default(SQLiteParser.ordering_term_return);
			SQLiteParser.ordering_term_return ordering_term187 = default(SQLiteParser.ordering_term_return);

			CommonTree limit_tree = null;
			CommonTree offset_tree = null;
			CommonTree ORDER183_tree = null;
			CommonTree BY184_tree = null;
			CommonTree COMMA186_tree = null;
			CommonTree LIMIT188_tree = null;
			CommonTree OFFSET189_tree = null;
			CommonTree COMMA190_tree = null;
			RewriteRuleITokenStream stream_INTEGER = new RewriteRuleITokenStream(adaptor, "token INTEGER");
			RewriteRuleITokenStream stream_BY = new RewriteRuleITokenStream(adaptor, "token BY");
			RewriteRuleITokenStream stream_ORDER = new RewriteRuleITokenStream(adaptor, "token ORDER");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_LIMIT = new RewriteRuleITokenStream(adaptor, "token LIMIT");
			RewriteRuleITokenStream stream_OFFSET = new RewriteRuleITokenStream(adaptor, "token OFFSET");
			RewriteRuleSubtreeStream stream_select_list = new RewriteRuleSubtreeStream(adaptor, "rule select_list");
			RewriteRuleSubtreeStream stream_ordering_term = new RewriteRuleSubtreeStream(adaptor, "rule ordering_term");
			try
			{
				DebugEnterRule(GrammarFileName, "select_stmt");
				DebugLocation(301, 1);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:301:12: ( select_list ( ORDER BY ordering_term ( COMMA ordering_term )* )? ( LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )? )? -> ^( SELECT select_list ( ^( ORDER ( ordering_term )+ ) )? ( ^( LIMIT $limit ( $offset)? ) )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:301:14: select_list ( ORDER BY ordering_term ( COMMA ordering_term )* )? ( LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )? )?
					{
						DebugLocation(301, 14);
						PushFollow(Follow._select_list_in_select_stmt1810);
						select_list182 = select_list();
						PopFollow();

						stream_select_list.Add(select_list182.Tree);
						DebugLocation(302, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:302:3: ( ORDER BY ordering_term ( COMMA ordering_term )* )?
						int alt58 = 2;
						try
						{
							DebugEnterSubRule(58);
							try
							{
								DebugEnterDecision(58, decisionCanBacktrack[58]);
								int LA58_0 = input.LA(1);

								if ((LA58_0 == ORDER))
								{
									alt58 = 1;
								}
							}
							finally { DebugExitDecision(58); }
							switch (alt58)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:302:4: ORDER BY ordering_term ( COMMA ordering_term )*
									{
										DebugLocation(302, 4);
										ORDER183 = (CommonToken)Match(input, ORDER, Follow._ORDER_in_select_stmt1815);
										stream_ORDER.Add(ORDER183);

										DebugLocation(302, 10);
										BY184 = (CommonToken)Match(input, BY, Follow._BY_in_select_stmt1817);
										stream_BY.Add(BY184);

										DebugLocation(302, 13);
										PushFollow(Follow._ordering_term_in_select_stmt1819);
										ordering_term185 = ordering_term();
										PopFollow();

										stream_ordering_term.Add(ordering_term185.Tree);
										DebugLocation(302, 27);
										// C:\\Users\\Gareth\\Desktop\\test.g:302:27: ( COMMA ordering_term )*
										try
										{
											DebugEnterSubRule(57);
											while (true)
											{
												int alt57 = 2;
												try
												{
													DebugEnterDecision(57, decisionCanBacktrack[57]);
													int LA57_0 = input.LA(1);

													if ((LA57_0 == COMMA))
													{
														alt57 = 1;
													}


												}
												finally { DebugExitDecision(57); }
												switch (alt57)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:302:28: COMMA ordering_term
														{
															DebugLocation(302, 28);
															COMMA186 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_select_stmt1822);
															stream_COMMA.Add(COMMA186);

															DebugLocation(302, 34);
															PushFollow(Follow._ordering_term_in_select_stmt1824);
															ordering_term187 = ordering_term();
															PopFollow();

															stream_ordering_term.Add(ordering_term187.Tree);

														}
														break;

													default:
														goto loop57;
												}
											}

										loop57:
											;

										}
										finally { DebugExitSubRule(57); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(58); }

						DebugLocation(303, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:303:3: ( LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )? )?
						int alt61 = 2;
						try
						{
							DebugEnterSubRule(61);
							try
							{
								DebugEnterDecision(61, decisionCanBacktrack[61]);
								int LA61_0 = input.LA(1);

								if ((LA61_0 == LIMIT))
								{
									alt61 = 1;
								}
							}
							finally { DebugExitDecision(61); }
							switch (alt61)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:303:4: LIMIT limit= INTEGER ( ( OFFSET | COMMA ) offset= INTEGER )?
									{
										DebugLocation(303, 4);
										LIMIT188 = (CommonToken)Match(input, LIMIT, Follow._LIMIT_in_select_stmt1833);
										stream_LIMIT.Add(LIMIT188);

										DebugLocation(303, 15);
										limit = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_select_stmt1837);
										stream_INTEGER.Add(limit);

										DebugLocation(303, 24);
										// C:\\Users\\Gareth\\Desktop\\test.g:303:24: ( ( OFFSET | COMMA ) offset= INTEGER )?
										int alt60 = 2;
										try
										{
											DebugEnterSubRule(60);
											try
											{
												DebugEnterDecision(60, decisionCanBacktrack[60]);
												int LA60_0 = input.LA(1);

												if ((LA60_0 == COMMA || LA60_0 == OFFSET))
												{
													alt60 = 1;
												}
											}
											finally { DebugExitDecision(60); }
											switch (alt60)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:303:25: ( OFFSET | COMMA ) offset= INTEGER
													{
														DebugLocation(303, 25);
														// C:\\Users\\Gareth\\Desktop\\test.g:303:25: ( OFFSET | COMMA )
														int alt59 = 2;
														try
														{
															DebugEnterSubRule(59);
															try
															{
																DebugEnterDecision(59, decisionCanBacktrack[59]);
																int LA59_0 = input.LA(1);

																if ((LA59_0 == OFFSET))
																{
																	alt59 = 1;
																}
																else if ((LA59_0 == COMMA))
																{
																	alt59 = 2;
																}
																else
																{
																	NoViableAltException nvae = new NoViableAltException("", 59, 0, input);

																	DebugRecognitionException(nvae);
																	throw nvae;
																}
															}
															finally { DebugExitDecision(59); }
															switch (alt59)
															{
																case 1:
																	DebugEnterAlt(1);
																	// C:\\Users\\Gareth\\Desktop\\test.g:303:26: OFFSET
																	{
																		DebugLocation(303, 26);
																		OFFSET189 = (CommonToken)Match(input, OFFSET, Follow._OFFSET_in_select_stmt1841);
																		stream_OFFSET.Add(OFFSET189);


																	}
																	break;
																case 2:
																	DebugEnterAlt(2);
																	// C:\\Users\\Gareth\\Desktop\\test.g:303:35: COMMA
																	{
																		DebugLocation(303, 35);
																		COMMA190 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_select_stmt1845);
																		stream_COMMA.Add(COMMA190);


																	}
																	break;

															}
														}
														finally { DebugExitSubRule(59); }

														DebugLocation(303, 48);
														offset = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_select_stmt1850);
														stream_INTEGER.Add(offset);


													}
													break;

											}
										}
										finally { DebugExitSubRule(60); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(61); }



						{
							// AST REWRITE
							// elements: limit, ORDER, ordering_term, LIMIT, offset, select_list
							// token labels: limit, offset
							// rule labels: retval
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleITokenStream stream_limit = new RewriteRuleITokenStream(adaptor, "token limit", limit);
							RewriteRuleITokenStream stream_offset = new RewriteRuleITokenStream(adaptor, "token offset", offset);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 304:1: -> ^( SELECT select_list ( ^( ORDER ( ordering_term )+ ) )? ( ^( LIMIT $limit ( $offset)? ) )? )
							{
								DebugLocation(304, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:304:4: ^( SELECT select_list ( ^( ORDER ( ordering_term )+ ) )? ( ^( LIMIT $limit ( $offset)? ) )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(305, 3);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(SELECT, "SELECT"), root_1);

									DebugLocation(305, 10);
									adaptor.AddChild(root_1, stream_select_list.NextTree());
									DebugLocation(305, 22);
									// C:\\Users\\Gareth\\Desktop\\test.g:305:22: ( ^( ORDER ( ordering_term )+ ) )?
									if (stream_ORDER.HasNext || stream_ordering_term.HasNext)
									{
										DebugLocation(305, 22);
										// C:\\Users\\Gareth\\Desktop\\test.g:305:22: ^( ORDER ( ordering_term )+ )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(305, 24);
											root_2 = (CommonTree)adaptor.BecomeRoot(stream_ORDER.NextNode(), root_2);

											DebugLocation(305, 30);
											if (!(stream_ordering_term.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_ordering_term.HasNext)
											{
												DebugLocation(305, 30);
												adaptor.AddChild(root_2, stream_ordering_term.NextTree());

											}
											stream_ordering_term.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_ORDER.Reset();
									stream_ordering_term.Reset();
									DebugLocation(305, 47);
									// C:\\Users\\Gareth\\Desktop\\test.g:305:47: ( ^( LIMIT $limit ( $offset)? ) )?
									if (stream_limit.HasNext || stream_LIMIT.HasNext || stream_offset.HasNext)
									{
										DebugLocation(305, 47);
										// C:\\Users\\Gareth\\Desktop\\test.g:305:47: ^( LIMIT $limit ( $offset)? )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(305, 49);
											root_2 = (CommonTree)adaptor.BecomeRoot(stream_LIMIT.NextNode(), root_2);

											DebugLocation(305, 55);
											adaptor.AddChild(root_2, stream_limit.NextNode());
											DebugLocation(305, 62);
											// C:\\Users\\Gareth\\Desktop\\test.g:305:62: ( $offset)?
											if (stream_offset.HasNext)
											{
												DebugLocation(305, 62);
												adaptor.AddChild(root_2, stream_offset.NextNode());

											}
											stream_offset.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_limit.Reset();
									stream_LIMIT.Reset();
									stream_offset.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("select_stmt", 37);
					LeaveRule("select_stmt", 37);
					Leave_select_stmt();
				}
				DebugLocation(306, 1);
			}
			finally { DebugExitRule(GrammarFileName, "select_stmt"); }
			return retval;

		}
		// $ANTLR end "select_stmt"

		public class select_list_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_select_list();
		partial void Leave_select_list();

		// $ANTLR start "select_list"
		// C:\\Users\\Gareth\\Desktop\\test.g:308:1: select_list : select_core ( select_op select_core )* ;
		[GrammarRule("select_list")]
		private SQLiteParser.select_list_return select_list()
		{
			Enter_select_list();
			EnterRule("select_list", 38);
			TraceIn("select_list", 38);
			SQLiteParser.select_list_return retval = new SQLiteParser.select_list_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.select_core_return select_core191 = default(SQLiteParser.select_core_return);
			SQLiteParser.select_op_return select_op192 = default(SQLiteParser.select_op_return);
			SQLiteParser.select_core_return select_core193 = default(SQLiteParser.select_core_return);


			try
			{
				DebugEnterRule(GrammarFileName, "select_list");
				DebugLocation(308, 39);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:308:12: ( select_core ( select_op select_core )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:309:3: select_core ( select_op select_core )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(309, 3);
						PushFollow(Follow._select_core_in_select_list1895);
						select_core191 = select_core();
						PopFollow();

						adaptor.AddChild(root_0, select_core191.Tree);
						DebugLocation(309, 15);
						// C:\\Users\\Gareth\\Desktop\\test.g:309:15: ( select_op select_core )*
						try
						{
							DebugEnterSubRule(62);
							while (true)
							{
								int alt62 = 2;
								try
								{
									DebugEnterDecision(62, decisionCanBacktrack[62]);
									int LA62_0 = input.LA(1);

									if ((LA62_0 == UNION || (LA62_0 >= INTERSECT && LA62_0 <= EXCEPT)))
									{
										alt62 = 1;
									}


								}
								finally { DebugExitDecision(62); }
								switch (alt62)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:309:16: select_op select_core
										{
											DebugLocation(309, 25);
											PushFollow(Follow._select_op_in_select_list1898);
											select_op192 = select_op();
											PopFollow();

											root_0 = (CommonTree)adaptor.BecomeRoot(select_op192.Tree, root_0);
											DebugLocation(309, 27);
											PushFollow(Follow._select_core_in_select_list1901);
											select_core193 = select_core();
											PopFollow();

											adaptor.AddChild(root_0, select_core193.Tree);

										}
										break;

									default:
										goto loop62;
								}
							}

						loop62:
							;

						}
						finally { DebugExitSubRule(62); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("select_list", 38);
					LeaveRule("select_list", 38);
					Leave_select_list();
				}
				DebugLocation(309, 39);
			}
			finally { DebugExitRule(GrammarFileName, "select_list"); }
			return retval;

		}
		// $ANTLR end "select_list"

		public class select_op_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_select_op();
		partial void Leave_select_op();

		// $ANTLR start "select_op"
		// C:\\Users\\Gareth\\Desktop\\test.g:311:1: select_op : ( UNION ( ALL )? | INTERSECT | EXCEPT );
		[GrammarRule("select_op")]
		private SQLiteParser.select_op_return select_op()
		{
			Enter_select_op();
			EnterRule("select_op", 39);
			TraceIn("select_op", 39);
			SQLiteParser.select_op_return retval = new SQLiteParser.select_op_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken UNION194 = null;
			CommonToken ALL195 = null;
			CommonToken INTERSECT196 = null;
			CommonToken EXCEPT197 = null;

			CommonTree UNION194_tree = null;
			CommonTree ALL195_tree = null;
			CommonTree INTERSECT196_tree = null;
			CommonTree EXCEPT197_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "select_op");
				DebugLocation(311, 45);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:311:10: ( UNION ( ALL )? | INTERSECT | EXCEPT )
					int alt64 = 3;
					try
					{
						DebugEnterDecision(64, decisionCanBacktrack[64]);
						switch (input.LA(1))
						{
							case UNION:
								{
									alt64 = 1;
								}
								break;
							case INTERSECT:
								{
									alt64 = 2;
								}
								break;
							case EXCEPT:
								{
									alt64 = 3;
								}
								break;
							default:
								{
									NoViableAltException nvae = new NoViableAltException("", 64, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
						}

					}
					finally { DebugExitDecision(64); }
					switch (alt64)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:311:12: UNION ( ALL )?
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(311, 17);
								UNION194 = (CommonToken)Match(input, UNION, Follow._UNION_in_select_op1910);
								UNION194_tree = (CommonTree)adaptor.Create(UNION194);
								root_0 = (CommonTree)adaptor.BecomeRoot(UNION194_tree, root_0);

								DebugLocation(311, 19);
								// C:\\Users\\Gareth\\Desktop\\test.g:311:19: ( ALL )?
								int alt63 = 2;
								try
								{
									DebugEnterSubRule(63);
									try
									{
										DebugEnterDecision(63, decisionCanBacktrack[63]);
										int LA63_0 = input.LA(1);

										if ((LA63_0 == ALL))
										{
											alt63 = 1;
										}
									}
									finally { DebugExitDecision(63); }
									switch (alt63)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:311:20: ALL
											{
												DebugLocation(311, 20);
												ALL195 = (CommonToken)Match(input, ALL, Follow._ALL_in_select_op1914);
												ALL195_tree = (CommonTree)adaptor.Create(ALL195);
												adaptor.AddChild(root_0, ALL195_tree);


											}
											break;

									}
								}
								finally { DebugExitSubRule(63); }


							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:311:28: INTERSECT
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(311, 28);
								INTERSECT196 = (CommonToken)Match(input, INTERSECT, Follow._INTERSECT_in_select_op1920);
								INTERSECT196_tree = (CommonTree)adaptor.Create(INTERSECT196);
								adaptor.AddChild(root_0, INTERSECT196_tree);


							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:311:40: EXCEPT
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(311, 40);
								EXCEPT197 = (CommonToken)Match(input, EXCEPT, Follow._EXCEPT_in_select_op1924);
								EXCEPT197_tree = (CommonTree)adaptor.Create(EXCEPT197);
								adaptor.AddChild(root_0, EXCEPT197_tree);


							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("select_op", 39);
					LeaveRule("select_op", 39);
					Leave_select_op();
				}
				DebugLocation(311, 45);
			}
			finally { DebugExitRule(GrammarFileName, "select_op"); }
			return retval;

		}
		// $ANTLR end "select_op"

		public class select_core_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_select_core();
		partial void Leave_select_core();

		// $ANTLR start "select_core"
		// C:\\Users\\Gareth\\Desktop\\test.g:313:1: select_core : SELECT ( ALL | DISTINCT )? result_column ( COMMA result_column )* ( FROM join_source )? ( WHERE where_expr= expr )? ( GROUP BY ordering_term ( COMMA ordering_term )* ( HAVING having_expr= expr )? )? -> ^( SELECT_CORE ( DISTINCT )? ^( COLUMNS ( result_column )+ ) ( ^( FROM join_source ) )? ( ^( WHERE $where_expr) )? ( ^( GROUP ( ordering_term )+ ( ^( HAVING $having_expr) )? ) )? ) ;
		[GrammarRule("select_core")]
		private SQLiteParser.select_core_return select_core()
		{
			Enter_select_core();
			EnterRule("select_core", 40);
			TraceIn("select_core", 40);
			SQLiteParser.select_core_return retval = new SQLiteParser.select_core_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken SELECT198 = null;
			CommonToken ALL199 = null;
			CommonToken DISTINCT200 = null;
			CommonToken COMMA202 = null;
			CommonToken FROM204 = null;
			CommonToken WHERE206 = null;
			CommonToken GROUP207 = null;
			CommonToken BY208 = null;
			CommonToken COMMA210 = null;
			CommonToken HAVING212 = null;
			SQLiteParser.expr_return where_expr = default(SQLiteParser.expr_return);
			SQLiteParser.expr_return having_expr = default(SQLiteParser.expr_return);
			SQLiteParser.result_column_return result_column201 = default(SQLiteParser.result_column_return);
			SQLiteParser.result_column_return result_column203 = default(SQLiteParser.result_column_return);
			SQLiteParser.join_source_return join_source205 = default(SQLiteParser.join_source_return);
			SQLiteParser.ordering_term_return ordering_term209 = default(SQLiteParser.ordering_term_return);
			SQLiteParser.ordering_term_return ordering_term211 = default(SQLiteParser.ordering_term_return);

			CommonTree SELECT198_tree = null;
			CommonTree ALL199_tree = null;
			CommonTree DISTINCT200_tree = null;
			CommonTree COMMA202_tree = null;
			CommonTree FROM204_tree = null;
			CommonTree WHERE206_tree = null;
			CommonTree GROUP207_tree = null;
			CommonTree BY208_tree = null;
			CommonTree COMMA210_tree = null;
			CommonTree HAVING212_tree = null;
			RewriteRuleITokenStream stream_WHERE = new RewriteRuleITokenStream(adaptor, "token WHERE");
			RewriteRuleITokenStream stream_GROUP = new RewriteRuleITokenStream(adaptor, "token GROUP");
			RewriteRuleITokenStream stream_BY = new RewriteRuleITokenStream(adaptor, "token BY");
			RewriteRuleITokenStream stream_HAVING = new RewriteRuleITokenStream(adaptor, "token HAVING");
			RewriteRuleITokenStream stream_FROM = new RewriteRuleITokenStream(adaptor, "token FROM");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_SELECT = new RewriteRuleITokenStream(adaptor, "token SELECT");
			RewriteRuleITokenStream stream_DISTINCT = new RewriteRuleITokenStream(adaptor, "token DISTINCT");
			RewriteRuleITokenStream stream_ALL = new RewriteRuleITokenStream(adaptor, "token ALL");
			RewriteRuleSubtreeStream stream_ordering_term = new RewriteRuleSubtreeStream(adaptor, "rule ordering_term");
			RewriteRuleSubtreeStream stream_result_column = new RewriteRuleSubtreeStream(adaptor, "rule result_column");
			RewriteRuleSubtreeStream stream_expr = new RewriteRuleSubtreeStream(adaptor, "rule expr");
			RewriteRuleSubtreeStream stream_join_source = new RewriteRuleSubtreeStream(adaptor, "rule join_source");
			try
			{
				DebugEnterRule(GrammarFileName, "select_core");
				DebugLocation(313, 1);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:313:12: ( SELECT ( ALL | DISTINCT )? result_column ( COMMA result_column )* ( FROM join_source )? ( WHERE where_expr= expr )? ( GROUP BY ordering_term ( COMMA ordering_term )* ( HAVING having_expr= expr )? )? -> ^( SELECT_CORE ( DISTINCT )? ^( COLUMNS ( result_column )+ ) ( ^( FROM join_source ) )? ( ^( WHERE $where_expr) )? ( ^( GROUP ( ordering_term )+ ( ^( HAVING $having_expr) )? ) )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:314:3: SELECT ( ALL | DISTINCT )? result_column ( COMMA result_column )* ( FROM join_source )? ( WHERE where_expr= expr )? ( GROUP BY ordering_term ( COMMA ordering_term )* ( HAVING having_expr= expr )? )?
					{
						DebugLocation(314, 3);
						SELECT198 = (CommonToken)Match(input, SELECT, Follow._SELECT_in_select_core1933);
						stream_SELECT.Add(SELECT198);

						DebugLocation(314, 10);
						// C:\\Users\\Gareth\\Desktop\\test.g:314:10: ( ALL | DISTINCT )?
						int alt65 = 3;
						try
						{
							DebugEnterSubRule(65);
							try
							{
								DebugEnterDecision(65, decisionCanBacktrack[65]);
								try
								{
									alt65 = dfa65.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(65); }
							switch (alt65)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:314:11: ALL
									{
										DebugLocation(314, 11);
										ALL199 = (CommonToken)Match(input, ALL, Follow._ALL_in_select_core1936);
										stream_ALL.Add(ALL199);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:314:17: DISTINCT
									{
										DebugLocation(314, 17);
										DISTINCT200 = (CommonToken)Match(input, DISTINCT, Follow._DISTINCT_in_select_core1940);
										stream_DISTINCT.Add(DISTINCT200);


									}
									break;

							}
						}
						finally { DebugExitSubRule(65); }

						DebugLocation(314, 28);
						PushFollow(Follow._result_column_in_select_core1944);
						result_column201 = result_column();
						PopFollow();

						stream_result_column.Add(result_column201.Tree);
						DebugLocation(314, 42);
						// C:\\Users\\Gareth\\Desktop\\test.g:314:42: ( COMMA result_column )*
						try
						{
							DebugEnterSubRule(66);
							while (true)
							{
								int alt66 = 2;
								try
								{
									DebugEnterDecision(66, decisionCanBacktrack[66]);
									try
									{
										alt66 = dfa66.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(66); }
								switch (alt66)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:314:43: COMMA result_column
										{
											DebugLocation(314, 43);
											COMMA202 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_select_core1947);
											stream_COMMA.Add(COMMA202);

											DebugLocation(314, 49);
											PushFollow(Follow._result_column_in_select_core1949);
											result_column203 = result_column();
											PopFollow();

											stream_result_column.Add(result_column203.Tree);

										}
										break;

									default:
										goto loop66;
								}
							}

						loop66:
							;

						}
						finally { DebugExitSubRule(66); }

						DebugLocation(314, 65);
						// C:\\Users\\Gareth\\Desktop\\test.g:314:65: ( FROM join_source )?
						int alt67 = 2;
						try
						{
							DebugEnterSubRule(67);
							try
							{
								DebugEnterDecision(67, decisionCanBacktrack[67]);
								try
								{
									alt67 = dfa67.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(67); }
							switch (alt67)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:314:66: FROM join_source
									{
										DebugLocation(314, 66);
										FROM204 = (CommonToken)Match(input, FROM, Follow._FROM_in_select_core1954);
										stream_FROM.Add(FROM204);

										DebugLocation(314, 71);
										PushFollow(Follow._join_source_in_select_core1956);
										join_source205 = join_source();
										PopFollow();

										stream_join_source.Add(join_source205.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(67); }

						DebugLocation(314, 85);
						// C:\\Users\\Gareth\\Desktop\\test.g:314:85: ( WHERE where_expr= expr )?
						int alt68 = 2;
						try
						{
							DebugEnterSubRule(68);
							try
							{
								DebugEnterDecision(68, decisionCanBacktrack[68]);
								try
								{
									alt68 = dfa68.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(68); }
							switch (alt68)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:314:86: WHERE where_expr= expr
									{
										DebugLocation(314, 86);
										WHERE206 = (CommonToken)Match(input, WHERE, Follow._WHERE_in_select_core1961);
										stream_WHERE.Add(WHERE206);

										DebugLocation(314, 102);
										PushFollow(Follow._expr_in_select_core1965);
										where_expr = expr();
										PopFollow();

										stream_expr.Add(where_expr.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(68); }

						DebugLocation(315, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:315:3: ( GROUP BY ordering_term ( COMMA ordering_term )* ( HAVING having_expr= expr )? )?
						int alt71 = 2;
						try
						{
							DebugEnterSubRule(71);
							try
							{
								DebugEnterDecision(71, decisionCanBacktrack[71]);
								try
								{
									alt71 = dfa71.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(71); }
							switch (alt71)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:315:5: GROUP BY ordering_term ( COMMA ordering_term )* ( HAVING having_expr= expr )?
									{
										DebugLocation(315, 5);
										GROUP207 = (CommonToken)Match(input, GROUP, Follow._GROUP_in_select_core1973);
										stream_GROUP.Add(GROUP207);

										DebugLocation(315, 11);
										BY208 = (CommonToken)Match(input, BY, Follow._BY_in_select_core1975);
										stream_BY.Add(BY208);

										DebugLocation(315, 14);
										PushFollow(Follow._ordering_term_in_select_core1977);
										ordering_term209 = ordering_term();
										PopFollow();

										stream_ordering_term.Add(ordering_term209.Tree);
										DebugLocation(315, 28);
										// C:\\Users\\Gareth\\Desktop\\test.g:315:28: ( COMMA ordering_term )*
										try
										{
											DebugEnterSubRule(69);
											while (true)
											{
												int alt69 = 2;
												try
												{
													DebugEnterDecision(69, decisionCanBacktrack[69]);
													try
													{
														alt69 = dfa69.Predict(input);
													}
													catch (NoViableAltException nvae)
													{
														DebugRecognitionException(nvae);
														throw;
													}
												}
												finally { DebugExitDecision(69); }
												switch (alt69)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:315:29: COMMA ordering_term
														{
															DebugLocation(315, 29);
															COMMA210 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_select_core1980);
															stream_COMMA.Add(COMMA210);

															DebugLocation(315, 35);
															PushFollow(Follow._ordering_term_in_select_core1982);
															ordering_term211 = ordering_term();
															PopFollow();

															stream_ordering_term.Add(ordering_term211.Tree);

														}
														break;

													default:
														goto loop69;
												}
											}

										loop69:
											;

										}
										finally { DebugExitSubRule(69); }

										DebugLocation(315, 51);
										// C:\\Users\\Gareth\\Desktop\\test.g:315:51: ( HAVING having_expr= expr )?
										int alt70 = 2;
										try
										{
											DebugEnterSubRule(70);
											try
											{
												DebugEnterDecision(70, decisionCanBacktrack[70]);
												try
												{
													alt70 = dfa70.Predict(input);
												}
												catch (NoViableAltException nvae)
												{
													DebugRecognitionException(nvae);
													throw;
												}
											}
											finally { DebugExitDecision(70); }
											switch (alt70)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:315:52: HAVING having_expr= expr
													{
														DebugLocation(315, 52);
														HAVING212 = (CommonToken)Match(input, HAVING, Follow._HAVING_in_select_core1987);
														stream_HAVING.Add(HAVING212);

														DebugLocation(315, 70);
														PushFollow(Follow._expr_in_select_core1991);
														having_expr = expr();
														PopFollow();

														stream_expr.Add(having_expr.Tree);

													}
													break;

											}
										}
										finally { DebugExitSubRule(70); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(71); }



						{
							// AST REWRITE
							// elements: ordering_term, DISTINCT, where_expr, join_source, GROUP, WHERE, result_column, HAVING, having_expr, FROM
							// token labels: 
							// rule labels: retval, having_expr, where_expr
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_having_expr = new RewriteRuleSubtreeStream(adaptor, "rule having_expr", having_expr != null ? having_expr.Tree : null);
							RewriteRuleSubtreeStream stream_where_expr = new RewriteRuleSubtreeStream(adaptor, "rule where_expr", where_expr != null ? where_expr.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 316:1: -> ^( SELECT_CORE ( DISTINCT )? ^( COLUMNS ( result_column )+ ) ( ^( FROM join_source ) )? ( ^( WHERE $where_expr) )? ( ^( GROUP ( ordering_term )+ ( ^( HAVING $having_expr) )? ) )? )
							{
								DebugLocation(316, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:316:4: ^( SELECT_CORE ( DISTINCT )? ^( COLUMNS ( result_column )+ ) ( ^( FROM join_source ) )? ( ^( WHERE $where_expr) )? ( ^( GROUP ( ordering_term )+ ( ^( HAVING $having_expr) )? ) )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(317, 3);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(SELECT_CORE, "SELECT_CORE"), root_1);

									DebugLocation(317, 15);
									// C:\\Users\\Gareth\\Desktop\\test.g:317:15: ( DISTINCT )?
									if (stream_DISTINCT.HasNext)
									{
										DebugLocation(317, 16);
										adaptor.AddChild(root_1, stream_DISTINCT.NextNode());

									}
									stream_DISTINCT.Reset();
									DebugLocation(317, 27);
									// C:\\Users\\Gareth\\Desktop\\test.g:317:27: ^( COLUMNS ( result_column )+ )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(317, 29);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

										DebugLocation(317, 37);
										if (!(stream_result_column.HasNext))
										{
											throw new RewriteEarlyExitException();
										}
										while (stream_result_column.HasNext)
										{
											DebugLocation(317, 37);
											adaptor.AddChild(root_2, stream_result_column.NextTree());

										}
										stream_result_column.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(317, 53);
									// C:\\Users\\Gareth\\Desktop\\test.g:317:53: ( ^( FROM join_source ) )?
									if (stream_join_source.HasNext || stream_FROM.HasNext)
									{
										DebugLocation(317, 53);
										// C:\\Users\\Gareth\\Desktop\\test.g:317:53: ^( FROM join_source )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(317, 55);
											root_2 = (CommonTree)adaptor.BecomeRoot(stream_FROM.NextNode(), root_2);

											DebugLocation(317, 60);
											adaptor.AddChild(root_2, stream_join_source.NextTree());

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_join_source.Reset();
									stream_FROM.Reset();
									DebugLocation(317, 74);
									// C:\\Users\\Gareth\\Desktop\\test.g:317:74: ( ^( WHERE $where_expr) )?
									if (stream_where_expr.HasNext || stream_WHERE.HasNext)
									{
										DebugLocation(317, 74);
										// C:\\Users\\Gareth\\Desktop\\test.g:317:74: ^( WHERE $where_expr)
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(317, 76);
											root_2 = (CommonTree)adaptor.BecomeRoot(stream_WHERE.NextNode(), root_2);

											DebugLocation(317, 82);
											adaptor.AddChild(root_2, stream_where_expr.NextTree());

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_where_expr.Reset();
									stream_WHERE.Reset();
									DebugLocation(318, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:318:3: ( ^( GROUP ( ordering_term )+ ( ^( HAVING $having_expr) )? ) )?
									if (stream_ordering_term.HasNext || stream_GROUP.HasNext || stream_HAVING.HasNext || stream_having_expr.HasNext)
									{
										DebugLocation(318, 3);
										// C:\\Users\\Gareth\\Desktop\\test.g:318:3: ^( GROUP ( ordering_term )+ ( ^( HAVING $having_expr) )? )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(318, 5);
											root_2 = (CommonTree)adaptor.BecomeRoot(stream_GROUP.NextNode(), root_2);

											DebugLocation(318, 11);
											if (!(stream_ordering_term.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_ordering_term.HasNext)
											{
												DebugLocation(318, 11);
												adaptor.AddChild(root_2, stream_ordering_term.NextTree());

											}
											stream_ordering_term.Reset();
											DebugLocation(318, 26);
											// C:\\Users\\Gareth\\Desktop\\test.g:318:26: ( ^( HAVING $having_expr) )?
											if (stream_HAVING.HasNext || stream_having_expr.HasNext)
											{
												DebugLocation(318, 26);
												// C:\\Users\\Gareth\\Desktop\\test.g:318:26: ^( HAVING $having_expr)
												{
													CommonTree root_3 = (CommonTree)adaptor.Nil();
													DebugLocation(318, 28);
													root_3 = (CommonTree)adaptor.BecomeRoot(stream_HAVING.NextNode(), root_3);

													DebugLocation(318, 35);
													adaptor.AddChild(root_3, stream_having_expr.NextTree());

													adaptor.AddChild(root_2, root_3);
												}

											}
											stream_HAVING.Reset();
											stream_having_expr.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_ordering_term.Reset();
									stream_GROUP.Reset();
									stream_HAVING.Reset();
									stream_having_expr.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("select_core", 40);
					LeaveRule("select_core", 40);
					Leave_select_core();
				}
				DebugLocation(319, 1);
			}
			finally { DebugExitRule(GrammarFileName, "select_core"); }
			return retval;

		}
		// $ANTLR end "select_core"

		public class result_column_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_result_column();
		partial void Leave_result_column();

		// $ANTLR start "result_column"
		// C:\\Users\\Gareth\\Desktop\\test.g:321:1: result_column : ( ASTERISK | table_name= id DOT ASTERISK -> ^( ASTERISK $table_name) | expr ( ( AS )? column_alias= id )? -> ^( ALIAS expr ( $column_alias)? ) );
		[GrammarRule("result_column")]
		private SQLiteParser.result_column_return result_column()
		{
			Enter_result_column();
			EnterRule("result_column", 41);
			TraceIn("result_column", 41);
			SQLiteParser.result_column_return retval = new SQLiteParser.result_column_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ASTERISK213 = null;
			CommonToken DOT214 = null;
			CommonToken ASTERISK215 = null;
			CommonToken AS217 = null;
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return column_alias = default(SQLiteParser.id_return);
			SQLiteParser.expr_return expr216 = default(SQLiteParser.expr_return);

			CommonTree ASTERISK213_tree = null;
			CommonTree DOT214_tree = null;
			CommonTree ASTERISK215_tree = null;
			CommonTree AS217_tree = null;
			RewriteRuleITokenStream stream_AS = new RewriteRuleITokenStream(adaptor, "token AS");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_ASTERISK = new RewriteRuleITokenStream(adaptor, "token ASTERISK");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_expr = new RewriteRuleSubtreeStream(adaptor, "rule expr");
			try
			{
				DebugEnterRule(GrammarFileName, "result_column");
				DebugLocation(321, 65);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:322:3: ( ASTERISK | table_name= id DOT ASTERISK -> ^( ASTERISK $table_name) | expr ( ( AS )? column_alias= id )? -> ^( ALIAS expr ( $column_alias)? ) )
					int alt74 = 3;
					try
					{
						DebugEnterDecision(74, decisionCanBacktrack[74]);
						try
						{
							alt74 = dfa74.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(74); }
					switch (alt74)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:322:5: ASTERISK
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(322, 5);
								ASTERISK213 = (CommonToken)Match(input, ASTERISK, Follow._ASTERISK_in_result_column2061);
								ASTERISK213_tree = (CommonTree)adaptor.Create(ASTERISK213);
								adaptor.AddChild(root_0, ASTERISK213_tree);


							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:323:5: table_name= id DOT ASTERISK
							{
								DebugLocation(323, 15);
								PushFollow(Follow._id_in_result_column2069);
								table_name = id();
								PopFollow();

								stream_id.Add(table_name.Tree);
								DebugLocation(323, 19);
								DOT214 = (CommonToken)Match(input, DOT, Follow._DOT_in_result_column2071);
								stream_DOT.Add(DOT214);

								DebugLocation(323, 23);
								ASTERISK215 = (CommonToken)Match(input, ASTERISK, Follow._ASTERISK_in_result_column2073);
								stream_ASTERISK.Add(ASTERISK215);



								{
									// AST REWRITE
									// elements: table_name, ASTERISK
									// token labels: 
									// rule labels: retval, table_name
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 323:32: -> ^( ASTERISK $table_name)
									{
										DebugLocation(323, 35);
										// C:\\Users\\Gareth\\Desktop\\test.g:323:35: ^( ASTERISK $table_name)
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(323, 37);
											root_1 = (CommonTree)adaptor.BecomeRoot(stream_ASTERISK.NextNode(), root_1);

											DebugLocation(323, 46);
											adaptor.AddChild(root_1, stream_table_name.NextTree());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:324:5: expr ( ( AS )? column_alias= id )?
							{
								DebugLocation(324, 5);
								PushFollow(Follow._expr_in_result_column2088);
								expr216 = expr();
								PopFollow();

								stream_expr.Add(expr216.Tree);
								DebugLocation(324, 10);
								// C:\\Users\\Gareth\\Desktop\\test.g:324:10: ( ( AS )? column_alias= id )?
								int alt73 = 2;
								try
								{
									DebugEnterSubRule(73);
									try
									{
										DebugEnterDecision(73, decisionCanBacktrack[73]);
										try
										{
											alt73 = dfa73.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(73); }
									switch (alt73)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:324:11: ( AS )? column_alias= id
											{
												DebugLocation(324, 11);
												// C:\\Users\\Gareth\\Desktop\\test.g:324:11: ( AS )?
												int alt72 = 2;
												try
												{
													DebugEnterSubRule(72);
													try
													{
														DebugEnterDecision(72, decisionCanBacktrack[72]);
														try
														{
															alt72 = dfa72.Predict(input);
														}
														catch (NoViableAltException nvae)
														{
															DebugRecognitionException(nvae);
															throw;
														}
													}
													finally { DebugExitDecision(72); }
													switch (alt72)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:324:12: AS
															{
																DebugLocation(324, 12);
																AS217 = (CommonToken)Match(input, AS, Follow._AS_in_result_column2092);
																stream_AS.Add(AS217);


															}
															break;

													}
												}
												finally { DebugExitSubRule(72); }

												DebugLocation(324, 29);
												PushFollow(Follow._id_in_result_column2098);
												column_alias = id();
												PopFollow();

												stream_id.Add(column_alias.Tree);

											}
											break;

									}
								}
								finally { DebugExitSubRule(73); }



								{
									// AST REWRITE
									// elements: expr, column_alias
									// token labels: 
									// rule labels: retval, column_alias
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_column_alias = new RewriteRuleSubtreeStream(adaptor, "rule column_alias", column_alias != null ? column_alias.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 324:35: -> ^( ALIAS expr ( $column_alias)? )
									{
										DebugLocation(324, 38);
										// C:\\Users\\Gareth\\Desktop\\test.g:324:38: ^( ALIAS expr ( $column_alias)? )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(324, 40);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(ALIAS, "ALIAS"), root_1);

											DebugLocation(324, 46);
											adaptor.AddChild(root_1, stream_expr.NextTree());
											DebugLocation(324, 51);
											// C:\\Users\\Gareth\\Desktop\\test.g:324:51: ( $column_alias)?
											if (stream_column_alias.HasNext)
											{
												DebugLocation(324, 51);
												adaptor.AddChild(root_1, stream_column_alias.NextTree());

											}
											stream_column_alias.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("result_column", 41);
					LeaveRule("result_column", 41);
					Leave_result_column();
				}
				DebugLocation(324, 65);
			}
			finally { DebugExitRule(GrammarFileName, "result_column"); }
			return retval;

		}
		// $ANTLR end "result_column"

		public class join_source_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_join_source();
		partial void Leave_join_source();

		// $ANTLR start "join_source"
		// C:\\Users\\Gareth\\Desktop\\test.g:326:1: join_source : single_source ( join_op single_source ( join_constraint )? )* ;
		[GrammarRule("join_source")]
		private SQLiteParser.join_source_return join_source()
		{
			Enter_join_source();
			EnterRule("join_source", 42);
			TraceIn("join_source", 42);
			SQLiteParser.join_source_return retval = new SQLiteParser.join_source_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.single_source_return single_source218 = default(SQLiteParser.single_source_return);
			SQLiteParser.join_op_return join_op219 = default(SQLiteParser.join_op_return);
			SQLiteParser.single_source_return single_source220 = default(SQLiteParser.single_source_return);
			SQLiteParser.join_constraint_return join_constraint221 = default(SQLiteParser.join_constraint_return);


			try
			{
				DebugEnterRule(GrammarFileName, "join_source");
				DebugLocation(326, 71);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:326:12: ( single_source ( join_op single_source ( join_constraint )? )* )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:326:14: single_source ( join_op single_source ( join_constraint )? )*
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(326, 14);
						PushFollow(Follow._single_source_in_join_source2119);
						single_source218 = single_source();
						PopFollow();

						adaptor.AddChild(root_0, single_source218.Tree);
						DebugLocation(326, 28);
						// C:\\Users\\Gareth\\Desktop\\test.g:326:28: ( join_op single_source ( join_constraint )? )*
						try
						{
							DebugEnterSubRule(76);
							while (true)
							{
								int alt76 = 2;
								try
								{
									DebugEnterDecision(76, decisionCanBacktrack[76]);
									try
									{
										alt76 = dfa76.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(76); }
								switch (alt76)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:326:29: join_op single_source ( join_constraint )?
										{
											DebugLocation(326, 36);
											PushFollow(Follow._join_op_in_join_source2122);
											join_op219 = join_op();
											PopFollow();

											root_0 = (CommonTree)adaptor.BecomeRoot(join_op219.Tree, root_0);
											DebugLocation(326, 38);
											PushFollow(Follow._single_source_in_join_source2125);
											single_source220 = single_source();
											PopFollow();

											adaptor.AddChild(root_0, single_source220.Tree);
											DebugLocation(326, 52);
											// C:\\Users\\Gareth\\Desktop\\test.g:326:52: ( join_constraint )?
											int alt75 = 2;
											try
											{
												DebugEnterSubRule(75);
												try
												{
													DebugEnterDecision(75, decisionCanBacktrack[75]);
													try
													{
														alt75 = dfa75.Predict(input);
													}
													catch (NoViableAltException nvae)
													{
														DebugRecognitionException(nvae);
														throw;
													}
												}
												finally { DebugExitDecision(75); }
												switch (alt75)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:326:53: join_constraint
														{
															DebugLocation(326, 53);
															PushFollow(Follow._join_constraint_in_join_source2128);
															join_constraint221 = join_constraint();
															PopFollow();

															adaptor.AddChild(root_0, join_constraint221.Tree);

														}
														break;

												}
											}
											finally { DebugExitSubRule(75); }


										}
										break;

									default:
										goto loop76;
								}
							}

						loop76:
							;

						}
						finally { DebugExitSubRule(76); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("join_source", 42);
					LeaveRule("join_source", 42);
					Leave_join_source();
				}
				DebugLocation(326, 71);
			}
			finally { DebugExitRule(GrammarFileName, "join_source"); }
			return retval;

		}
		// $ANTLR end "join_source"

		public class single_source_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_single_source();
		partial void Leave_single_source();

		// $ANTLR start "single_source"
		// C:\\Users\\Gareth\\Desktop\\test.g:328:1: single_source : ( (database_name= id DOT )? table_name= ID ( ( AS )? table_alias= ID )? ( INDEXED BY index_name= id | NOT INDEXED )? -> ^( ALIAS ^( $table_name ( $database_name)? ) ( $table_alias)? ( ^( INDEXED ( NOT )? ( $index_name)? ) )? ) | LPAREN select_stmt RPAREN ( ( AS )? table_alias= ID )? -> ^( ALIAS select_stmt ( $table_alias)? ) | LPAREN join_source RPAREN );
		[GrammarRule("single_source")]
		private SQLiteParser.single_source_return single_source()
		{
			Enter_single_source();
			EnterRule("single_source", 43);
			TraceIn("single_source", 43);
			SQLiteParser.single_source_return retval = new SQLiteParser.single_source_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken table_name = null;
			CommonToken table_alias = null;
			CommonToken DOT222 = null;
			CommonToken AS223 = null;
			CommonToken INDEXED224 = null;
			CommonToken BY225 = null;
			CommonToken NOT226 = null;
			CommonToken INDEXED227 = null;
			CommonToken LPAREN228 = null;
			CommonToken RPAREN230 = null;
			CommonToken AS231 = null;
			CommonToken LPAREN232 = null;
			CommonToken RPAREN234 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return index_name = default(SQLiteParser.id_return);
			SQLiteParser.select_stmt_return select_stmt229 = default(SQLiteParser.select_stmt_return);
			SQLiteParser.join_source_return join_source233 = default(SQLiteParser.join_source_return);

			CommonTree table_name_tree = null;
			CommonTree table_alias_tree = null;
			CommonTree DOT222_tree = null;
			CommonTree AS223_tree = null;
			CommonTree INDEXED224_tree = null;
			CommonTree BY225_tree = null;
			CommonTree NOT226_tree = null;
			CommonTree INDEXED227_tree = null;
			CommonTree LPAREN228_tree = null;
			CommonTree RPAREN230_tree = null;
			CommonTree AS231_tree = null;
			CommonTree LPAREN232_tree = null;
			CommonTree RPAREN234_tree = null;
			RewriteRuleITokenStream stream_INDEXED = new RewriteRuleITokenStream(adaptor, "token INDEXED");
			RewriteRuleITokenStream stream_AS = new RewriteRuleITokenStream(adaptor, "token AS");
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_BY = new RewriteRuleITokenStream(adaptor, "token BY");
			RewriteRuleITokenStream stream_NOT = new RewriteRuleITokenStream(adaptor, "token NOT");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_ID = new RewriteRuleITokenStream(adaptor, "token ID");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_select_stmt = new RewriteRuleSubtreeStream(adaptor, "rule select_stmt");
			try
			{
				DebugEnterRule(GrammarFileName, "single_source");
				DebugLocation(328, 31);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:329:3: ( (database_name= id DOT )? table_name= ID ( ( AS )? table_alias= ID )? ( INDEXED BY index_name= id | NOT INDEXED )? -> ^( ALIAS ^( $table_name ( $database_name)? ) ( $table_alias)? ( ^( INDEXED ( NOT )? ( $index_name)? ) )? ) | LPAREN select_stmt RPAREN ( ( AS )? table_alias= ID )? -> ^( ALIAS select_stmt ( $table_alias)? ) | LPAREN join_source RPAREN )
					int alt83 = 3;
					try
					{
						DebugEnterDecision(83, decisionCanBacktrack[83]);
						try
						{
							alt83 = dfa83.Predict(input);
						}
						catch (NoViableAltException nvae)
						{
							DebugRecognitionException(nvae);
							throw;
						}
					}
					finally { DebugExitDecision(83); }
					switch (alt83)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:329:5: (database_name= id DOT )? table_name= ID ( ( AS )? table_alias= ID )? ( INDEXED BY index_name= id | NOT INDEXED )?
							{
								DebugLocation(329, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:329:5: (database_name= id DOT )?
								int alt77 = 2;
								try
								{
									DebugEnterSubRule(77);
									try
									{
										DebugEnterDecision(77, decisionCanBacktrack[77]);
										try
										{
											alt77 = dfa77.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(77); }
									switch (alt77)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:329:6: database_name= id DOT
											{
												DebugLocation(329, 19);
												PushFollow(Follow._id_in_single_source2145);
												database_name = id();
												PopFollow();

												stream_id.Add(database_name.Tree);
												DebugLocation(329, 23);
												DOT222 = (CommonToken)Match(input, DOT, Follow._DOT_in_single_source2147);
												stream_DOT.Add(DOT222);


											}
											break;

									}
								}
								finally { DebugExitSubRule(77); }

								DebugLocation(329, 39);
								table_name = (CommonToken)Match(input, ID, Follow._ID_in_single_source2153);
								stream_ID.Add(table_name);

								DebugLocation(329, 43);
								// C:\\Users\\Gareth\\Desktop\\test.g:329:43: ( ( AS )? table_alias= ID )?
								int alt79 = 2;
								try
								{
									DebugEnterSubRule(79);
									try
									{
										DebugEnterDecision(79, decisionCanBacktrack[79]);
										try
										{
											alt79 = dfa79.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(79); }
									switch (alt79)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:329:44: ( AS )? table_alias= ID
											{
												DebugLocation(329, 44);
												// C:\\Users\\Gareth\\Desktop\\test.g:329:44: ( AS )?
												int alt78 = 2;
												try
												{
													DebugEnterSubRule(78);
													try
													{
														DebugEnterDecision(78, decisionCanBacktrack[78]);
														int LA78_0 = input.LA(1);

														if ((LA78_0 == AS))
														{
															alt78 = 1;
														}
													}
													finally { DebugExitDecision(78); }
													switch (alt78)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:329:45: AS
															{
																DebugLocation(329, 45);
																AS223 = (CommonToken)Match(input, AS, Follow._AS_in_single_source2157);
																stream_AS.Add(AS223);


															}
															break;

													}
												}
												finally { DebugExitSubRule(78); }

												DebugLocation(329, 61);
												table_alias = (CommonToken)Match(input, ID, Follow._ID_in_single_source2163);
												stream_ID.Add(table_alias);


											}
											break;

									}
								}
								finally { DebugExitSubRule(79); }

								DebugLocation(329, 67);
								// C:\\Users\\Gareth\\Desktop\\test.g:329:67: ( INDEXED BY index_name= id | NOT INDEXED )?
								int alt80 = 3;
								try
								{
									DebugEnterSubRule(80);
									try
									{
										DebugEnterDecision(80, decisionCanBacktrack[80]);
										try
										{
											alt80 = dfa80.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(80); }
									switch (alt80)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:329:68: INDEXED BY index_name= id
											{
												DebugLocation(329, 68);
												INDEXED224 = (CommonToken)Match(input, INDEXED, Follow._INDEXED_in_single_source2168);
												stream_INDEXED.Add(INDEXED224);

												DebugLocation(329, 76);
												BY225 = (CommonToken)Match(input, BY, Follow._BY_in_single_source2170);
												stream_BY.Add(BY225);

												DebugLocation(329, 89);
												PushFollow(Follow._id_in_single_source2174);
												index_name = id();
												PopFollow();

												stream_id.Add(index_name.Tree);

											}
											break;
										case 2:
											DebugEnterAlt(2);
											// C:\\Users\\Gareth\\Desktop\\test.g:329:95: NOT INDEXED
											{
												DebugLocation(329, 95);
												NOT226 = (CommonToken)Match(input, NOT, Follow._NOT_in_single_source2178);
												stream_NOT.Add(NOT226);

												DebugLocation(329, 99);
												INDEXED227 = (CommonToken)Match(input, INDEXED, Follow._INDEXED_in_single_source2180);
												stream_INDEXED.Add(INDEXED227);


											}
											break;

									}
								}
								finally { DebugExitSubRule(80); }



								{
									// AST REWRITE
									// elements: NOT, INDEXED, table_name, database_name, index_name, table_alias
									// token labels: table_name, table_alias
									// rule labels: index_name, database_name, retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleITokenStream stream_table_name = new RewriteRuleITokenStream(adaptor, "token table_name", table_name);
									RewriteRuleITokenStream stream_table_alias = new RewriteRuleITokenStream(adaptor, "token table_alias", table_alias);
									RewriteRuleSubtreeStream stream_index_name = new RewriteRuleSubtreeStream(adaptor, "rule index_name", index_name != null ? index_name.Tree : null);
									RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 330:3: -> ^( ALIAS ^( $table_name ( $database_name)? ) ( $table_alias)? ( ^( INDEXED ( NOT )? ( $index_name)? ) )? )
									{
										DebugLocation(330, 6);
										// C:\\Users\\Gareth\\Desktop\\test.g:330:6: ^( ALIAS ^( $table_name ( $database_name)? ) ( $table_alias)? ( ^( INDEXED ( NOT )? ( $index_name)? ) )? )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(330, 8);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(ALIAS, "ALIAS"), root_1);

											DebugLocation(330, 14);
											// C:\\Users\\Gareth\\Desktop\\test.g:330:14: ^( $table_name ( $database_name)? )
											{
												CommonTree root_2 = (CommonTree)adaptor.Nil();
												DebugLocation(330, 16);
												root_2 = (CommonTree)adaptor.BecomeRoot(stream_table_name.NextNode(), root_2);

												DebugLocation(330, 28);
												// C:\\Users\\Gareth\\Desktop\\test.g:330:28: ( $database_name)?
												if (stream_database_name.HasNext)
												{
													DebugLocation(330, 28);
													adaptor.AddChild(root_2, stream_database_name.NextTree());

												}
												stream_database_name.Reset();

												adaptor.AddChild(root_1, root_2);
											}
											DebugLocation(330, 45);
											// C:\\Users\\Gareth\\Desktop\\test.g:330:45: ( $table_alias)?
											if (stream_table_alias.HasNext)
											{
												DebugLocation(330, 45);
												adaptor.AddChild(root_1, stream_table_alias.NextNode());

											}
											stream_table_alias.Reset();
											DebugLocation(330, 59);
											// C:\\Users\\Gareth\\Desktop\\test.g:330:59: ( ^( INDEXED ( NOT )? ( $index_name)? ) )?
											if (stream_NOT.HasNext || stream_INDEXED.HasNext || stream_index_name.HasNext)
											{
												DebugLocation(330, 59);
												// C:\\Users\\Gareth\\Desktop\\test.g:330:59: ^( INDEXED ( NOT )? ( $index_name)? )
												{
													CommonTree root_2 = (CommonTree)adaptor.Nil();
													DebugLocation(330, 61);
													root_2 = (CommonTree)adaptor.BecomeRoot(stream_INDEXED.NextNode(), root_2);

													DebugLocation(330, 69);
													// C:\\Users\\Gareth\\Desktop\\test.g:330:69: ( NOT )?
													if (stream_NOT.HasNext)
													{
														DebugLocation(330, 69);
														adaptor.AddChild(root_2, stream_NOT.NextNode());

													}
													stream_NOT.Reset();
													DebugLocation(330, 74);
													// C:\\Users\\Gareth\\Desktop\\test.g:330:74: ( $index_name)?
													if (stream_index_name.HasNext)
													{
														DebugLocation(330, 74);
														adaptor.AddChild(root_2, stream_index_name.NextTree());

													}
													stream_index_name.Reset();

													adaptor.AddChild(root_1, root_2);
												}

											}
											stream_NOT.Reset();
											stream_INDEXED.Reset();
											stream_index_name.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:331:5: LPAREN select_stmt RPAREN ( ( AS )? table_alias= ID )?
							{
								DebugLocation(331, 5);
								LPAREN228 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_single_source2221);
								stream_LPAREN.Add(LPAREN228);

								DebugLocation(331, 12);
								PushFollow(Follow._select_stmt_in_single_source2223);
								select_stmt229 = select_stmt();
								PopFollow();

								stream_select_stmt.Add(select_stmt229.Tree);
								DebugLocation(331, 24);
								RPAREN230 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_single_source2225);
								stream_RPAREN.Add(RPAREN230);

								DebugLocation(331, 31);
								// C:\\Users\\Gareth\\Desktop\\test.g:331:31: ( ( AS )? table_alias= ID )?
								int alt82 = 2;
								try
								{
									DebugEnterSubRule(82);
									try
									{
										DebugEnterDecision(82, decisionCanBacktrack[82]);
										try
										{
											alt82 = dfa82.Predict(input);
										}
										catch (NoViableAltException nvae)
										{
											DebugRecognitionException(nvae);
											throw;
										}
									}
									finally { DebugExitDecision(82); }
									switch (alt82)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:331:32: ( AS )? table_alias= ID
											{
												DebugLocation(331, 32);
												// C:\\Users\\Gareth\\Desktop\\test.g:331:32: ( AS )?
												int alt81 = 2;
												try
												{
													DebugEnterSubRule(81);
													try
													{
														DebugEnterDecision(81, decisionCanBacktrack[81]);
														int LA81_0 = input.LA(1);

														if ((LA81_0 == AS))
														{
															alt81 = 1;
														}
													}
													finally { DebugExitDecision(81); }
													switch (alt81)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:331:33: AS
															{
																DebugLocation(331, 33);
																AS231 = (CommonToken)Match(input, AS, Follow._AS_in_single_source2229);
																stream_AS.Add(AS231);


															}
															break;

													}
												}
												finally { DebugExitSubRule(81); }

												DebugLocation(331, 49);
												table_alias = (CommonToken)Match(input, ID, Follow._ID_in_single_source2235);
												stream_ID.Add(table_alias);


											}
											break;

									}
								}
								finally { DebugExitSubRule(82); }



								{
									// AST REWRITE
									// elements: table_alias, select_stmt
									// token labels: table_alias
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleITokenStream stream_table_alias = new RewriteRuleITokenStream(adaptor, "token table_alias", table_alias);
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 332:3: -> ^( ALIAS select_stmt ( $table_alias)? )
									{
										DebugLocation(332, 6);
										// C:\\Users\\Gareth\\Desktop\\test.g:332:6: ^( ALIAS select_stmt ( $table_alias)? )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(332, 8);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(ALIAS, "ALIAS"), root_1);

											DebugLocation(332, 14);
											adaptor.AddChild(root_1, stream_select_stmt.NextTree());
											DebugLocation(332, 26);
											// C:\\Users\\Gareth\\Desktop\\test.g:332:26: ( $table_alias)?
											if (stream_table_alias.HasNext)
											{
												DebugLocation(332, 26);
												adaptor.AddChild(root_1, stream_table_alias.NextNode());

											}
											stream_table_alias.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 3:
							DebugEnterAlt(3);
							// C:\\Users\\Gareth\\Desktop\\test.g:333:5: LPAREN join_source RPAREN
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(333, 11);
								LPAREN232 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_single_source2257);
								DebugLocation(333, 13);
								PushFollow(Follow._join_source_in_single_source2260);
								join_source233 = join_source();
								PopFollow();

								adaptor.AddChild(root_0, join_source233.Tree);
								DebugLocation(333, 31);
								RPAREN234 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_single_source2262);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("single_source", 43);
					LeaveRule("single_source", 43);
					Leave_single_source();
				}
				DebugLocation(333, 31);
			}
			finally { DebugExitRule(GrammarFileName, "single_source"); }
			return retval;

		}
		// $ANTLR end "single_source"

		public class join_op_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_join_op();
		partial void Leave_join_op();

		// $ANTLR start "join_op"
		// C:\\Users\\Gareth\\Desktop\\test.g:335:1: join_op : ( COMMA | ( NATURAL )? ( ( LEFT )? ( OUTER )? | INNER | CROSS ) JOIN );
		[GrammarRule("join_op")]
		private SQLiteParser.join_op_return join_op()
		{
			Enter_join_op();
			EnterRule("join_op", 44);
			TraceIn("join_op", 44);
			SQLiteParser.join_op_return retval = new SQLiteParser.join_op_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken COMMA235 = null;
			CommonToken NATURAL236 = null;
			CommonToken LEFT237 = null;
			CommonToken OUTER238 = null;
			CommonToken INNER239 = null;
			CommonToken CROSS240 = null;
			CommonToken JOIN241 = null;

			CommonTree COMMA235_tree = null;
			CommonTree NATURAL236_tree = null;
			CommonTree LEFT237_tree = null;
			CommonTree OUTER238_tree = null;
			CommonTree INNER239_tree = null;
			CommonTree CROSS240_tree = null;
			CommonTree JOIN241_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "join_op");
				DebugLocation(335, 55);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:336:3: ( COMMA | ( NATURAL )? ( ( LEFT )? ( OUTER )? | INNER | CROSS ) JOIN )
					int alt88 = 2;
					try
					{
						DebugEnterDecision(88, decisionCanBacktrack[88]);
						int LA88_0 = input.LA(1);

						if ((LA88_0 == COMMA))
						{
							alt88 = 1;
						}
						else if (((LA88_0 >= NATURAL && LA88_0 <= JOIN)))
						{
							alt88 = 2;
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 88, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(88); }
					switch (alt88)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:336:5: COMMA
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(336, 5);
								COMMA235 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_join_op2273);
								COMMA235_tree = (CommonTree)adaptor.Create(COMMA235);
								adaptor.AddChild(root_0, COMMA235_tree);


							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:337:5: ( NATURAL )? ( ( LEFT )? ( OUTER )? | INNER | CROSS ) JOIN
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(337, 5);
								// C:\\Users\\Gareth\\Desktop\\test.g:337:5: ( NATURAL )?
								int alt84 = 2;
								try
								{
									DebugEnterSubRule(84);
									try
									{
										DebugEnterDecision(84, decisionCanBacktrack[84]);
										int LA84_0 = input.LA(1);

										if ((LA84_0 == NATURAL))
										{
											alt84 = 1;
										}
									}
									finally { DebugExitDecision(84); }
									switch (alt84)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:337:6: NATURAL
											{
												DebugLocation(337, 6);
												NATURAL236 = (CommonToken)Match(input, NATURAL, Follow._NATURAL_in_join_op2280);
												NATURAL236_tree = (CommonTree)adaptor.Create(NATURAL236);
												adaptor.AddChild(root_0, NATURAL236_tree);


											}
											break;

									}
								}
								finally { DebugExitSubRule(84); }

								DebugLocation(337, 16);
								// C:\\Users\\Gareth\\Desktop\\test.g:337:16: ( ( LEFT )? ( OUTER )? | INNER | CROSS )
								int alt87 = 3;
								try
								{
									DebugEnterSubRule(87);
									try
									{
										DebugEnterDecision(87, decisionCanBacktrack[87]);
										switch (input.LA(1))
										{
											case LEFT:
											case OUTER:
											case JOIN:
												{
													alt87 = 1;
												}
												break;
											case INNER:
												{
													alt87 = 2;
												}
												break;
											case CROSS:
												{
													alt87 = 3;
												}
												break;
											default:
												{
													NoViableAltException nvae = new NoViableAltException("", 87, 0, input);

													DebugRecognitionException(nvae);
													throw nvae;
												}
										}

									}
									finally { DebugExitDecision(87); }
									switch (alt87)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:337:17: ( LEFT )? ( OUTER )?
											{
												DebugLocation(337, 17);
												// C:\\Users\\Gareth\\Desktop\\test.g:337:17: ( LEFT )?
												int alt85 = 2;
												try
												{
													DebugEnterSubRule(85);
													try
													{
														DebugEnterDecision(85, decisionCanBacktrack[85]);
														int LA85_0 = input.LA(1);

														if ((LA85_0 == LEFT))
														{
															alt85 = 1;
														}
													}
													finally { DebugExitDecision(85); }
													switch (alt85)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:337:18: LEFT
															{
																DebugLocation(337, 18);
																LEFT237 = (CommonToken)Match(input, LEFT, Follow._LEFT_in_join_op2286);
																LEFT237_tree = (CommonTree)adaptor.Create(LEFT237);
																adaptor.AddChild(root_0, LEFT237_tree);


															}
															break;

													}
												}
												finally { DebugExitSubRule(85); }

												DebugLocation(337, 25);
												// C:\\Users\\Gareth\\Desktop\\test.g:337:25: ( OUTER )?
												int alt86 = 2;
												try
												{
													DebugEnterSubRule(86);
													try
													{
														DebugEnterDecision(86, decisionCanBacktrack[86]);
														int LA86_0 = input.LA(1);

														if ((LA86_0 == OUTER))
														{
															alt86 = 1;
														}
													}
													finally { DebugExitDecision(86); }
													switch (alt86)
													{
														case 1:
															DebugEnterAlt(1);
															// C:\\Users\\Gareth\\Desktop\\test.g:337:26: OUTER
															{
																DebugLocation(337, 26);
																OUTER238 = (CommonToken)Match(input, OUTER, Follow._OUTER_in_join_op2291);
																OUTER238_tree = (CommonTree)adaptor.Create(OUTER238);
																adaptor.AddChild(root_0, OUTER238_tree);


															}
															break;

													}
												}
												finally { DebugExitSubRule(86); }


											}
											break;
										case 2:
											DebugEnterAlt(2);
											// C:\\Users\\Gareth\\Desktop\\test.g:337:36: INNER
											{
												DebugLocation(337, 36);
												INNER239 = (CommonToken)Match(input, INNER, Follow._INNER_in_join_op2297);
												INNER239_tree = (CommonTree)adaptor.Create(INNER239);
												adaptor.AddChild(root_0, INNER239_tree);


											}
											break;
										case 3:
											DebugEnterAlt(3);
											// C:\\Users\\Gareth\\Desktop\\test.g:337:44: CROSS
											{
												DebugLocation(337, 44);
												CROSS240 = (CommonToken)Match(input, CROSS, Follow._CROSS_in_join_op2301);
												CROSS240_tree = (CommonTree)adaptor.Create(CROSS240);
												adaptor.AddChild(root_0, CROSS240_tree);


											}
											break;

									}
								}
								finally { DebugExitSubRule(87); }

								DebugLocation(337, 55);
								JOIN241 = (CommonToken)Match(input, JOIN, Follow._JOIN_in_join_op2304);
								JOIN241_tree = (CommonTree)adaptor.Create(JOIN241);
								root_0 = (CommonTree)adaptor.BecomeRoot(JOIN241_tree, root_0);


							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("join_op", 44);
					LeaveRule("join_op", 44);
					Leave_join_op();
				}
				DebugLocation(337, 55);
			}
			finally { DebugExitRule(GrammarFileName, "join_op"); }
			return retval;

		}
		// $ANTLR end "join_op"

		public class join_constraint_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_join_constraint();
		partial void Leave_join_constraint();

		// $ANTLR start "join_constraint"
		// C:\\Users\\Gareth\\Desktop\\test.g:339:1: join_constraint : ( ON expr | USING LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN -> ^( USING ( $column_names)+ ) );
		[GrammarRule("join_constraint")]
		private SQLiteParser.join_constraint_return join_constraint()
		{
			Enter_join_constraint();
			EnterRule("join_constraint", 45);
			TraceIn("join_constraint", 45);
			SQLiteParser.join_constraint_return retval = new SQLiteParser.join_constraint_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ON242 = null;
			CommonToken USING244 = null;
			CommonToken LPAREN245 = null;
			CommonToken COMMA246 = null;
			CommonToken RPAREN247 = null;
			List list_column_names = null;
			SQLiteParser.expr_return expr243 = default(SQLiteParser.expr_return);
			SQLiteParser.id_return column_names = default(SQLiteParser.id_return);
			CommonTree ON242_tree = null;
			CommonTree USING244_tree = null;
			CommonTree LPAREN245_tree = null;
			CommonTree COMMA246_tree = null;
			CommonTree RPAREN247_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_USING = new RewriteRuleITokenStream(adaptor, "token USING");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			try
			{
				DebugEnterRule(GrammarFileName, "join_constraint");
				DebugLocation(339, 93);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:340:3: ( ON expr | USING LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN -> ^( USING ( $column_names)+ ) )
					int alt90 = 2;
					try
					{
						DebugEnterDecision(90, decisionCanBacktrack[90]);
						int LA90_0 = input.LA(1);

						if ((LA90_0 == ON))
						{
							alt90 = 1;
						}
						else if ((LA90_0 == USING))
						{
							alt90 = 2;
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 90, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(90); }
					switch (alt90)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:340:5: ON expr
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(340, 7);
								ON242 = (CommonToken)Match(input, ON, Follow._ON_in_join_constraint2315);
								ON242_tree = (CommonTree)adaptor.Create(ON242);
								root_0 = (CommonTree)adaptor.BecomeRoot(ON242_tree, root_0);

								DebugLocation(340, 9);
								PushFollow(Follow._expr_in_join_constraint2318);
								expr243 = expr();
								PopFollow();

								adaptor.AddChild(root_0, expr243.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:341:5: USING LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN
							{
								DebugLocation(341, 5);
								USING244 = (CommonToken)Match(input, USING, Follow._USING_in_join_constraint2324);
								stream_USING.Add(USING244);

								DebugLocation(341, 11);
								LPAREN245 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_join_constraint2326);
								stream_LPAREN.Add(LPAREN245);

								DebugLocation(341, 30);
								PushFollow(Follow._id_in_join_constraint2330);
								column_names = id();
								PopFollow();

								stream_id.Add(column_names.Tree);
								if (list_column_names == null) list_column_names = new ArrayList();
								list_column_names.Add(column_names.Tree);

								DebugLocation(341, 35);
								// C:\\Users\\Gareth\\Desktop\\test.g:341:35: ( COMMA column_names+= id )*
								try
								{
									DebugEnterSubRule(89);
									while (true)
									{
										int alt89 = 2;
										try
										{
											DebugEnterDecision(89, decisionCanBacktrack[89]);
											int LA89_0 = input.LA(1);

											if ((LA89_0 == COMMA))
											{
												alt89 = 1;
											}


										}
										finally { DebugExitDecision(89); }
										switch (alt89)
										{
											case 1:
												DebugEnterAlt(1);
												// C:\\Users\\Gareth\\Desktop\\test.g:341:36: COMMA column_names+= id
												{
													DebugLocation(341, 36);
													COMMA246 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_join_constraint2333);
													stream_COMMA.Add(COMMA246);

													DebugLocation(341, 54);
													PushFollow(Follow._id_in_join_constraint2337);
													column_names = id();
													PopFollow();

													stream_id.Add(column_names.Tree);
													if (list_column_names == null) list_column_names = new ArrayList();
													list_column_names.Add(column_names.Tree);


												}
												break;

											default:
												goto loop89;
										}
									}

								loop89:
									;

								}
								finally { DebugExitSubRule(89); }

								DebugLocation(341, 61);
								RPAREN247 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_join_constraint2341);
								stream_RPAREN.Add(RPAREN247);



								{
									// AST REWRITE
									// elements: column_names, USING
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: column_names
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
									RewriteRuleSubtreeStream stream_column_names = new RewriteRuleSubtreeStream(adaptor, "token column_names", list_column_names);
									root_0 = (CommonTree)adaptor.Nil();
									// 341:68: -> ^( USING ( $column_names)+ )
									{
										DebugLocation(341, 71);
										// C:\\Users\\Gareth\\Desktop\\test.g:341:71: ^( USING ( $column_names)+ )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(341, 73);
											root_1 = (CommonTree)adaptor.BecomeRoot(stream_USING.NextNode(), root_1);

											DebugLocation(341, 79);
											if (!(stream_column_names.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_column_names.HasNext)
											{
												DebugLocation(341, 79);
												adaptor.AddChild(root_1, stream_column_names.NextTree());

											}
											stream_column_names.Reset();

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("join_constraint", 45);
					LeaveRule("join_constraint", 45);
					Leave_join_constraint();
				}
				DebugLocation(341, 93);
			}
			finally { DebugExitRule(GrammarFileName, "join_constraint"); }
			return retval;

		}
		// $ANTLR end "join_constraint"

		public class insert_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_insert_stmt();
		partial void Leave_insert_stmt();

		// $ANTLR start "insert_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:344:1: insert_stmt : ( INSERT ( operation_conflict_clause )? | REPLACE ) INTO (database_name= id DOT )? table_name= id ( ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN | select_stmt ) | DEFAULT VALUES ) ;
		[GrammarRule("insert_stmt")]
		private SQLiteParser.insert_stmt_return insert_stmt()
		{
			Enter_insert_stmt();
			EnterRule("insert_stmt", 46);
			TraceIn("insert_stmt", 46);
			SQLiteParser.insert_stmt_return retval = new SQLiteParser.insert_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken INSERT248 = null;
			CommonToken REPLACE250 = null;
			CommonToken INTO251 = null;
			CommonToken DOT252 = null;
			CommonToken LPAREN253 = null;
			CommonToken COMMA254 = null;
			CommonToken RPAREN255 = null;
			CommonToken VALUES256 = null;
			CommonToken LPAREN257 = null;
			CommonToken COMMA258 = null;
			CommonToken RPAREN259 = null;
			CommonToken DEFAULT261 = null;
			CommonToken VALUES262 = null;
			List list_column_names = null;
			List list_values = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.operation_conflict_clause_return operation_conflict_clause249 = default(SQLiteParser.operation_conflict_clause_return);
			SQLiteParser.select_stmt_return select_stmt260 = default(SQLiteParser.select_stmt_return);
			SQLiteParser.id_return column_names = default(SQLiteParser.id_return);
			SQLiteParser.expr_return values = default(SQLiteParser.expr_return);
			CommonTree INSERT248_tree = null;
			CommonTree REPLACE250_tree = null;
			CommonTree INTO251_tree = null;
			CommonTree DOT252_tree = null;
			CommonTree LPAREN253_tree = null;
			CommonTree COMMA254_tree = null;
			CommonTree RPAREN255_tree = null;
			CommonTree VALUES256_tree = null;
			CommonTree LPAREN257_tree = null;
			CommonTree COMMA258_tree = null;
			CommonTree RPAREN259_tree = null;
			CommonTree DEFAULT261_tree = null;
			CommonTree VALUES262_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "insert_stmt");
				DebugLocation(344, 20);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:344:12: ( ( INSERT ( operation_conflict_clause )? | REPLACE ) INTO (database_name= id DOT )? table_name= id ( ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN | select_stmt ) | DEFAULT VALUES ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:344:14: ( INSERT ( operation_conflict_clause )? | REPLACE ) INTO (database_name= id DOT )? table_name= id ( ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN | select_stmt ) | DEFAULT VALUES )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(344, 14);
						// C:\\Users\\Gareth\\Desktop\\test.g:344:14: ( INSERT ( operation_conflict_clause )? | REPLACE )
						int alt92 = 2;
						try
						{
							DebugEnterSubRule(92);
							try
							{
								DebugEnterDecision(92, decisionCanBacktrack[92]);
								int LA92_0 = input.LA(1);

								if ((LA92_0 == INSERT))
								{
									alt92 = 1;
								}
								else if ((LA92_0 == REPLACE))
								{
									alt92 = 2;
								}
								else
								{
									NoViableAltException nvae = new NoViableAltException("", 92, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
							}
							finally { DebugExitDecision(92); }
							switch (alt92)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:344:15: INSERT ( operation_conflict_clause )?
									{
										DebugLocation(344, 15);
										INSERT248 = (CommonToken)Match(input, INSERT, Follow._INSERT_in_insert_stmt2360);
										INSERT248_tree = (CommonTree)adaptor.Create(INSERT248);
										adaptor.AddChild(root_0, INSERT248_tree);

										DebugLocation(344, 22);
										// C:\\Users\\Gareth\\Desktop\\test.g:344:22: ( operation_conflict_clause )?
										int alt91 = 2;
										try
										{
											DebugEnterSubRule(91);
											try
											{
												DebugEnterDecision(91, decisionCanBacktrack[91]);
												int LA91_0 = input.LA(1);

												if ((LA91_0 == OR))
												{
													alt91 = 1;
												}
											}
											finally { DebugExitDecision(91); }
											switch (alt91)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:344:23: operation_conflict_clause
													{
														DebugLocation(344, 23);
														PushFollow(Follow._operation_conflict_clause_in_insert_stmt2363);
														operation_conflict_clause249 = operation_conflict_clause();
														PopFollow();

														adaptor.AddChild(root_0, operation_conflict_clause249.Tree);

													}
													break;

											}
										}
										finally { DebugExitSubRule(91); }


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:344:53: REPLACE
									{
										DebugLocation(344, 53);
										REPLACE250 = (CommonToken)Match(input, REPLACE, Follow._REPLACE_in_insert_stmt2369);
										REPLACE250_tree = (CommonTree)adaptor.Create(REPLACE250);
										adaptor.AddChild(root_0, REPLACE250_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(92); }

						DebugLocation(344, 62);
						INTO251 = (CommonToken)Match(input, INTO, Follow._INTO_in_insert_stmt2372);
						INTO251_tree = (CommonTree)adaptor.Create(INTO251);
						adaptor.AddChild(root_0, INTO251_tree);

						DebugLocation(344, 67);
						// C:\\Users\\Gareth\\Desktop\\test.g:344:67: (database_name= id DOT )?
						int alt93 = 2;
						try
						{
							DebugEnterSubRule(93);
							try
							{
								DebugEnterDecision(93, decisionCanBacktrack[93]);
								try
								{
									alt93 = dfa93.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(93); }
							switch (alt93)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:344:68: database_name= id DOT
									{
										DebugLocation(344, 81);
										PushFollow(Follow._id_in_insert_stmt2377);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(344, 85);
										DOT252 = (CommonToken)Match(input, DOT, Follow._DOT_in_insert_stmt2379);
										DOT252_tree = (CommonTree)adaptor.Create(DOT252);
										adaptor.AddChild(root_0, DOT252_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(93); }

						DebugLocation(344, 101);
						PushFollow(Follow._id_in_insert_stmt2385);
						table_name = id();
						PopFollow();

						adaptor.AddChild(root_0, table_name.Tree);
						DebugLocation(345, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:345:3: ( ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN | select_stmt ) | DEFAULT VALUES )
						int alt98 = 2;
						try
						{
							DebugEnterSubRule(98);
							try
							{
								DebugEnterDecision(98, decisionCanBacktrack[98]);
								int LA98_0 = input.LA(1);

								if ((LA98_0 == LPAREN || LA98_0 == SELECT || LA98_0 == VALUES))
								{
									alt98 = 1;
								}
								else if ((LA98_0 == DEFAULT))
								{
									alt98 = 2;
								}
								else
								{
									NoViableAltException nvae = new NoViableAltException("", 98, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
							}
							finally { DebugExitDecision(98); }
							switch (alt98)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:345:5: ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN | select_stmt )
									{
										DebugLocation(345, 5);
										// C:\\Users\\Gareth\\Desktop\\test.g:345:5: ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )?
										int alt95 = 2;
										try
										{
											DebugEnterSubRule(95);
											try
											{
												DebugEnterDecision(95, decisionCanBacktrack[95]);
												int LA95_0 = input.LA(1);

												if ((LA95_0 == LPAREN))
												{
													alt95 = 1;
												}
											}
											finally { DebugExitDecision(95); }
											switch (alt95)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:345:6: LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN
													{
														DebugLocation(345, 6);
														LPAREN253 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_insert_stmt2392);
														LPAREN253_tree = (CommonTree)adaptor.Create(LPAREN253);
														adaptor.AddChild(root_0, LPAREN253_tree);

														DebugLocation(345, 25);
														PushFollow(Follow._id_in_insert_stmt2396);
														column_names = id();
														PopFollow();

														adaptor.AddChild(root_0, column_names.Tree);
														if (list_column_names == null) list_column_names = new ArrayList();
														list_column_names.Add(column_names.Tree);

														DebugLocation(345, 30);
														// C:\\Users\\Gareth\\Desktop\\test.g:345:30: ( COMMA column_names+= id )*
														try
														{
															DebugEnterSubRule(94);
															while (true)
															{
																int alt94 = 2;
																try
																{
																	DebugEnterDecision(94, decisionCanBacktrack[94]);
																	int LA94_0 = input.LA(1);

																	if ((LA94_0 == COMMA))
																	{
																		alt94 = 1;
																	}


																}
																finally { DebugExitDecision(94); }
																switch (alt94)
																{
																	case 1:
																		DebugEnterAlt(1);
																		// C:\\Users\\Gareth\\Desktop\\test.g:345:31: COMMA column_names+= id
																		{
																			DebugLocation(345, 31);
																			COMMA254 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_insert_stmt2399);
																			COMMA254_tree = (CommonTree)adaptor.Create(COMMA254);
																			adaptor.AddChild(root_0, COMMA254_tree);

																			DebugLocation(345, 49);
																			PushFollow(Follow._id_in_insert_stmt2403);
																			column_names = id();
																			PopFollow();

																			adaptor.AddChild(root_0, column_names.Tree);
																			if (list_column_names == null) list_column_names = new ArrayList();
																			list_column_names.Add(column_names.Tree);


																		}
																		break;

																	default:
																		goto loop94;
																}
															}

														loop94:
															;

														}
														finally { DebugExitSubRule(94); }

														DebugLocation(345, 56);
														RPAREN255 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_insert_stmt2407);
														RPAREN255_tree = (CommonTree)adaptor.Create(RPAREN255);
														adaptor.AddChild(root_0, RPAREN255_tree);


													}
													break;

											}
										}
										finally { DebugExitSubRule(95); }

										DebugLocation(346, 5);
										// C:\\Users\\Gareth\\Desktop\\test.g:346:5: ( VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN | select_stmt )
										int alt97 = 2;
										try
										{
											DebugEnterSubRule(97);
											try
											{
												DebugEnterDecision(97, decisionCanBacktrack[97]);
												int LA97_0 = input.LA(1);

												if ((LA97_0 == VALUES))
												{
													alt97 = 1;
												}
												else if ((LA97_0 == SELECT))
												{
													alt97 = 2;
												}
												else
												{
													NoViableAltException nvae = new NoViableAltException("", 97, 0, input);

													DebugRecognitionException(nvae);
													throw nvae;
												}
											}
											finally { DebugExitDecision(97); }
											switch (alt97)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:346:6: VALUES LPAREN values+= expr ( COMMA values+= expr )* RPAREN
													{
														DebugLocation(346, 6);
														VALUES256 = (CommonToken)Match(input, VALUES, Follow._VALUES_in_insert_stmt2416);
														VALUES256_tree = (CommonTree)adaptor.Create(VALUES256);
														adaptor.AddChild(root_0, VALUES256_tree);

														DebugLocation(346, 13);
														LPAREN257 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_insert_stmt2418);
														LPAREN257_tree = (CommonTree)adaptor.Create(LPAREN257);
														adaptor.AddChild(root_0, LPAREN257_tree);

														DebugLocation(346, 26);
														PushFollow(Follow._expr_in_insert_stmt2422);
														values = expr();
														PopFollow();

														adaptor.AddChild(root_0, values.Tree);
														if (list_values == null) list_values = new ArrayList();
														list_values.Add(values.Tree);

														DebugLocation(346, 33);
														// C:\\Users\\Gareth\\Desktop\\test.g:346:33: ( COMMA values+= expr )*
														try
														{
															DebugEnterSubRule(96);
															while (true)
															{
																int alt96 = 2;
																try
																{
																	DebugEnterDecision(96, decisionCanBacktrack[96]);
																	int LA96_0 = input.LA(1);

																	if ((LA96_0 == COMMA))
																	{
																		alt96 = 1;
																	}


																}
																finally { DebugExitDecision(96); }
																switch (alt96)
																{
																	case 1:
																		DebugEnterAlt(1);
																		// C:\\Users\\Gareth\\Desktop\\test.g:346:34: COMMA values+= expr
																		{
																			DebugLocation(346, 34);
																			COMMA258 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_insert_stmt2425);
																			COMMA258_tree = (CommonTree)adaptor.Create(COMMA258);
																			adaptor.AddChild(root_0, COMMA258_tree);

																			DebugLocation(346, 46);
																			PushFollow(Follow._expr_in_insert_stmt2429);
																			values = expr();
																			PopFollow();

																			adaptor.AddChild(root_0, values.Tree);
																			if (list_values == null) list_values = new ArrayList();
																			list_values.Add(values.Tree);


																		}
																		break;

																	default:
																		goto loop96;
																}
															}

														loop96:
															;

														}
														finally { DebugExitSubRule(96); }

														DebugLocation(346, 55);
														RPAREN259 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_insert_stmt2433);
														RPAREN259_tree = (CommonTree)adaptor.Create(RPAREN259);
														adaptor.AddChild(root_0, RPAREN259_tree);


													}
													break;
												case 2:
													DebugEnterAlt(2);
													// C:\\Users\\Gareth\\Desktop\\test.g:346:64: select_stmt
													{
														DebugLocation(346, 64);
														PushFollow(Follow._select_stmt_in_insert_stmt2437);
														select_stmt260 = select_stmt();
														PopFollow();

														adaptor.AddChild(root_0, select_stmt260.Tree);

													}
													break;

											}
										}
										finally { DebugExitSubRule(97); }


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:347:5: DEFAULT VALUES
									{
										DebugLocation(347, 5);
										DEFAULT261 = (CommonToken)Match(input, DEFAULT, Follow._DEFAULT_in_insert_stmt2444);
										DEFAULT261_tree = (CommonTree)adaptor.Create(DEFAULT261);
										adaptor.AddChild(root_0, DEFAULT261_tree);

										DebugLocation(347, 13);
										VALUES262 = (CommonToken)Match(input, VALUES, Follow._VALUES_in_insert_stmt2446);
										VALUES262_tree = (CommonTree)adaptor.Create(VALUES262);
										adaptor.AddChild(root_0, VALUES262_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(98); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("insert_stmt", 46);
					LeaveRule("insert_stmt", 46);
					Leave_insert_stmt();
				}
				DebugLocation(347, 20);
			}
			finally { DebugExitRule(GrammarFileName, "insert_stmt"); }
			return retval;

		}
		// $ANTLR end "insert_stmt"

		public class update_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_update_stmt();
		partial void Leave_update_stmt();

		// $ANTLR start "update_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:350:1: update_stmt : UPDATE ( operation_conflict_clause )? qualified_table_name SET values+= update_set ( COMMA values+= update_set )* ( WHERE expr )? ( operation_limited_clause )? ;
		[GrammarRule("update_stmt")]
		private SQLiteParser.update_stmt_return update_stmt()
		{
			Enter_update_stmt();
			EnterRule("update_stmt", 47);
			TraceIn("update_stmt", 47);
			SQLiteParser.update_stmt_return retval = new SQLiteParser.update_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken UPDATE263 = null;
			CommonToken SET266 = null;
			CommonToken COMMA267 = null;
			CommonToken WHERE268 = null;
			List list_values = null;
			SQLiteParser.operation_conflict_clause_return operation_conflict_clause264 = default(SQLiteParser.operation_conflict_clause_return);
			SQLiteParser.qualified_table_name_return qualified_table_name265 = default(SQLiteParser.qualified_table_name_return);
			SQLiteParser.expr_return expr269 = default(SQLiteParser.expr_return);
			SQLiteParser.operation_limited_clause_return operation_limited_clause270 = default(SQLiteParser.operation_limited_clause_return);
			SQLiteParser.update_set_return values = default(SQLiteParser.update_set_return);
			CommonTree UPDATE263_tree = null;
			CommonTree SET266_tree = null;
			CommonTree COMMA267_tree = null;
			CommonTree WHERE268_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "update_stmt");
				DebugLocation(350, 94);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:350:12: ( UPDATE ( operation_conflict_clause )? qualified_table_name SET values+= update_set ( COMMA values+= update_set )* ( WHERE expr )? ( operation_limited_clause )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:350:14: UPDATE ( operation_conflict_clause )? qualified_table_name SET values+= update_set ( COMMA values+= update_set )* ( WHERE expr )? ( operation_limited_clause )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(350, 14);
						UPDATE263 = (CommonToken)Match(input, UPDATE, Follow._UPDATE_in_update_stmt2456);
						UPDATE263_tree = (CommonTree)adaptor.Create(UPDATE263);
						adaptor.AddChild(root_0, UPDATE263_tree);

						DebugLocation(350, 21);
						// C:\\Users\\Gareth\\Desktop\\test.g:350:21: ( operation_conflict_clause )?
						int alt99 = 2;
						try
						{
							DebugEnterSubRule(99);
							try
							{
								DebugEnterDecision(99, decisionCanBacktrack[99]);
								int LA99_0 = input.LA(1);

								if ((LA99_0 == OR))
								{
									int LA99_1 = input.LA(2);

									if (((LA99_1 >= IGNORE && LA99_1 <= FAIL) || LA99_1 == REPLACE))
									{
										alt99 = 1;
									}
								}
							}
							finally { DebugExitDecision(99); }
							switch (alt99)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:350:22: operation_conflict_clause
									{
										DebugLocation(350, 22);
										PushFollow(Follow._operation_conflict_clause_in_update_stmt2459);
										operation_conflict_clause264 = operation_conflict_clause();
										PopFollow();

										adaptor.AddChild(root_0, operation_conflict_clause264.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(99); }

						DebugLocation(350, 50);
						PushFollow(Follow._qualified_table_name_in_update_stmt2463);
						qualified_table_name265 = qualified_table_name();
						PopFollow();

						adaptor.AddChild(root_0, qualified_table_name265.Tree);
						DebugLocation(351, 3);
						SET266 = (CommonToken)Match(input, SET, Follow._SET_in_update_stmt2467);
						SET266_tree = (CommonTree)adaptor.Create(SET266);
						adaptor.AddChild(root_0, SET266_tree);

						DebugLocation(351, 13);
						PushFollow(Follow._update_set_in_update_stmt2471);
						values = update_set();
						PopFollow();

						adaptor.AddChild(root_0, values.Tree);
						if (list_values == null) list_values = new ArrayList();
						list_values.Add(values.Tree);

						DebugLocation(351, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:351:26: ( COMMA values+= update_set )*
						try
						{
							DebugEnterSubRule(100);
							while (true)
							{
								int alt100 = 2;
								try
								{
									DebugEnterDecision(100, decisionCanBacktrack[100]);
									int LA100_0 = input.LA(1);

									if ((LA100_0 == COMMA))
									{
										alt100 = 1;
									}


								}
								finally { DebugExitDecision(100); }
								switch (alt100)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:351:27: COMMA values+= update_set
										{
											DebugLocation(351, 27);
											COMMA267 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_update_stmt2474);
											COMMA267_tree = (CommonTree)adaptor.Create(COMMA267);
											adaptor.AddChild(root_0, COMMA267_tree);

											DebugLocation(351, 39);
											PushFollow(Follow._update_set_in_update_stmt2478);
											values = update_set();
											PopFollow();

											adaptor.AddChild(root_0, values.Tree);
											if (list_values == null) list_values = new ArrayList();
											list_values.Add(values.Tree);


										}
										break;

									default:
										goto loop100;
								}
							}

						loop100:
							;

						}
						finally { DebugExitSubRule(100); }

						DebugLocation(351, 54);
						// C:\\Users\\Gareth\\Desktop\\test.g:351:54: ( WHERE expr )?
						int alt101 = 2;
						try
						{
							DebugEnterSubRule(101);
							try
							{
								DebugEnterDecision(101, decisionCanBacktrack[101]);
								int LA101_0 = input.LA(1);

								if ((LA101_0 == WHERE))
								{
									alt101 = 1;
								}
							}
							finally { DebugExitDecision(101); }
							switch (alt101)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:351:55: WHERE expr
									{
										DebugLocation(351, 55);
										WHERE268 = (CommonToken)Match(input, WHERE, Follow._WHERE_in_update_stmt2483);
										WHERE268_tree = (CommonTree)adaptor.Create(WHERE268);
										adaptor.AddChild(root_0, WHERE268_tree);

										DebugLocation(351, 61);
										PushFollow(Follow._expr_in_update_stmt2485);
										expr269 = expr();
										PopFollow();

										adaptor.AddChild(root_0, expr269.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(101); }

						DebugLocation(351, 68);
						// C:\\Users\\Gareth\\Desktop\\test.g:351:68: ( operation_limited_clause )?
						int alt102 = 2;
						try
						{
							DebugEnterSubRule(102);
							try
							{
								DebugEnterDecision(102, decisionCanBacktrack[102]);
								int LA102_0 = input.LA(1);

								if (((LA102_0 >= ORDER && LA102_0 <= LIMIT)))
								{
									alt102 = 1;
								}
							}
							finally { DebugExitDecision(102); }
							switch (alt102)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:351:69: operation_limited_clause
									{
										DebugLocation(351, 69);
										PushFollow(Follow._operation_limited_clause_in_update_stmt2490);
										operation_limited_clause270 = operation_limited_clause();
										PopFollow();

										adaptor.AddChild(root_0, operation_limited_clause270.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(102); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("update_stmt", 47);
					LeaveRule("update_stmt", 47);
					Leave_update_stmt();
				}
				DebugLocation(351, 94);
			}
			finally { DebugExitRule(GrammarFileName, "update_stmt"); }
			return retval;

		}
		// $ANTLR end "update_stmt"

		public class update_set_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_update_set();
		partial void Leave_update_set();

		// $ANTLR start "update_set"
		// C:\\Users\\Gareth\\Desktop\\test.g:353:1: update_set : column_name= id EQUALS expr ;
		[GrammarRule("update_set")]
		private SQLiteParser.update_set_return update_set()
		{
			Enter_update_set();
			EnterRule("update_set", 48);
			TraceIn("update_set", 48);
			SQLiteParser.update_set_return retval = new SQLiteParser.update_set_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken EQUALS271 = null;
			SQLiteParser.id_return column_name = default(SQLiteParser.id_return);
			SQLiteParser.expr_return expr272 = default(SQLiteParser.expr_return);

			CommonTree EQUALS271_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "update_set");
				DebugLocation(353, 38);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:353:11: (column_name= id EQUALS expr )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:353:13: column_name= id EQUALS expr
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(353, 24);
						PushFollow(Follow._id_in_update_set2501);
						column_name = id();
						PopFollow();

						adaptor.AddChild(root_0, column_name.Tree);
						DebugLocation(353, 28);
						EQUALS271 = (CommonToken)Match(input, EQUALS, Follow._EQUALS_in_update_set2503);
						EQUALS271_tree = (CommonTree)adaptor.Create(EQUALS271);
						adaptor.AddChild(root_0, EQUALS271_tree);

						DebugLocation(353, 35);
						PushFollow(Follow._expr_in_update_set2505);
						expr272 = expr();
						PopFollow();

						adaptor.AddChild(root_0, expr272.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("update_set", 48);
					LeaveRule("update_set", 48);
					Leave_update_set();
				}
				DebugLocation(353, 38);
			}
			finally { DebugExitRule(GrammarFileName, "update_set"); }
			return retval;

		}
		// $ANTLR end "update_set"

		public class delete_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_delete_stmt();
		partial void Leave_delete_stmt();

		// $ANTLR start "delete_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:356:1: delete_stmt : DELETE FROM qualified_table_name ( WHERE expr )? ( operation_limited_clause )? ;
		[GrammarRule("delete_stmt")]
		private SQLiteParser.delete_stmt_return delete_stmt()
		{
			Enter_delete_stmt();
			EnterRule("delete_stmt", 49);
			TraceIn("delete_stmt", 49);
			SQLiteParser.delete_stmt_return retval = new SQLiteParser.delete_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DELETE273 = null;
			CommonToken FROM274 = null;
			CommonToken WHERE276 = null;
			SQLiteParser.qualified_table_name_return qualified_table_name275 = default(SQLiteParser.qualified_table_name_return);
			SQLiteParser.expr_return expr277 = default(SQLiteParser.expr_return);
			SQLiteParser.operation_limited_clause_return operation_limited_clause278 = default(SQLiteParser.operation_limited_clause_return);

			CommonTree DELETE273_tree = null;
			CommonTree FROM274_tree = null;
			CommonTree WHERE276_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "delete_stmt");
				DebugLocation(356, 87);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:356:12: ( DELETE FROM qualified_table_name ( WHERE expr )? ( operation_limited_clause )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:356:14: DELETE FROM qualified_table_name ( WHERE expr )? ( operation_limited_clause )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(356, 14);
						DELETE273 = (CommonToken)Match(input, DELETE, Follow._DELETE_in_delete_stmt2513);
						DELETE273_tree = (CommonTree)adaptor.Create(DELETE273);
						adaptor.AddChild(root_0, DELETE273_tree);

						DebugLocation(356, 21);
						FROM274 = (CommonToken)Match(input, FROM, Follow._FROM_in_delete_stmt2515);
						FROM274_tree = (CommonTree)adaptor.Create(FROM274);
						adaptor.AddChild(root_0, FROM274_tree);

						DebugLocation(356, 26);
						PushFollow(Follow._qualified_table_name_in_delete_stmt2517);
						qualified_table_name275 = qualified_table_name();
						PopFollow();

						adaptor.AddChild(root_0, qualified_table_name275.Tree);
						DebugLocation(356, 47);
						// C:\\Users\\Gareth\\Desktop\\test.g:356:47: ( WHERE expr )?
						int alt103 = 2;
						try
						{
							DebugEnterSubRule(103);
							try
							{
								DebugEnterDecision(103, decisionCanBacktrack[103]);
								int LA103_0 = input.LA(1);

								if ((LA103_0 == WHERE))
								{
									alt103 = 1;
								}
							}
							finally { DebugExitDecision(103); }
							switch (alt103)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:356:48: WHERE expr
									{
										DebugLocation(356, 48);
										WHERE276 = (CommonToken)Match(input, WHERE, Follow._WHERE_in_delete_stmt2520);
										WHERE276_tree = (CommonTree)adaptor.Create(WHERE276);
										adaptor.AddChild(root_0, WHERE276_tree);

										DebugLocation(356, 54);
										PushFollow(Follow._expr_in_delete_stmt2522);
										expr277 = expr();
										PopFollow();

										adaptor.AddChild(root_0, expr277.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(103); }

						DebugLocation(356, 61);
						// C:\\Users\\Gareth\\Desktop\\test.g:356:61: ( operation_limited_clause )?
						int alt104 = 2;
						try
						{
							DebugEnterSubRule(104);
							try
							{
								DebugEnterDecision(104, decisionCanBacktrack[104]);
								int LA104_0 = input.LA(1);

								if (((LA104_0 >= ORDER && LA104_0 <= LIMIT)))
								{
									alt104 = 1;
								}
							}
							finally { DebugExitDecision(104); }
							switch (alt104)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:356:62: operation_limited_clause
									{
										DebugLocation(356, 62);
										PushFollow(Follow._operation_limited_clause_in_delete_stmt2527);
										operation_limited_clause278 = operation_limited_clause();
										PopFollow();

										adaptor.AddChild(root_0, operation_limited_clause278.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(104); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("delete_stmt", 49);
					LeaveRule("delete_stmt", 49);
					Leave_delete_stmt();
				}
				DebugLocation(356, 87);
			}
			finally { DebugExitRule(GrammarFileName, "delete_stmt"); }
			return retval;

		}
		// $ANTLR end "delete_stmt"

		public class begin_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_begin_stmt();
		partial void Leave_begin_stmt();

		// $ANTLR start "begin_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:359:1: begin_stmt : BEGIN ( DEFERRED | IMMEDIATE | EXCLUSIVE )? ( TRANSACTION )? ;
		[GrammarRule("begin_stmt")]
		private SQLiteParser.begin_stmt_return begin_stmt()
		{
			Enter_begin_stmt();
			EnterRule("begin_stmt", 50);
			TraceIn("begin_stmt", 50);
			SQLiteParser.begin_stmt_return retval = new SQLiteParser.begin_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken BEGIN279 = null;
			CommonToken set280 = null;
			CommonToken TRANSACTION281 = null;

			CommonTree BEGIN279_tree = null;
			CommonTree set280_tree = null;
			CommonTree TRANSACTION281_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "begin_stmt");
				DebugLocation(359, 68);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:359:11: ( BEGIN ( DEFERRED | IMMEDIATE | EXCLUSIVE )? ( TRANSACTION )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:359:13: BEGIN ( DEFERRED | IMMEDIATE | EXCLUSIVE )? ( TRANSACTION )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(359, 13);
						BEGIN279 = (CommonToken)Match(input, BEGIN, Follow._BEGIN_in_begin_stmt2537);
						BEGIN279_tree = (CommonTree)adaptor.Create(BEGIN279);
						adaptor.AddChild(root_0, BEGIN279_tree);

						DebugLocation(359, 19);
						// C:\\Users\\Gareth\\Desktop\\test.g:359:19: ( DEFERRED | IMMEDIATE | EXCLUSIVE )?
						int alt105 = 2;
						try
						{
							DebugEnterSubRule(105);
							try
							{
								DebugEnterDecision(105, decisionCanBacktrack[105]);
								int LA105_0 = input.LA(1);

								if (((LA105_0 >= DEFERRED && LA105_0 <= EXCLUSIVE)))
								{
									alt105 = 1;
								}
							}
							finally { DebugExitDecision(105); }
							switch (alt105)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:
									{
										DebugLocation(359, 19);
										set280 = (CommonToken)input.LT(1);
										if ((input.LA(1) >= DEFERRED && input.LA(1) <= EXCLUSIVE))
										{
											input.Consume();
											adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set280));
											state.errorRecovery = false;
										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											throw mse;
										}


									}
									break;

							}
						}
						finally { DebugExitSubRule(105); }

						DebugLocation(359, 55);
						// C:\\Users\\Gareth\\Desktop\\test.g:359:55: ( TRANSACTION )?
						int alt106 = 2;
						try
						{
							DebugEnterSubRule(106);
							try
							{
								DebugEnterDecision(106, decisionCanBacktrack[106]);
								int LA106_0 = input.LA(1);

								if ((LA106_0 == TRANSACTION))
								{
									alt106 = 1;
								}
							}
							finally { DebugExitDecision(106); }
							switch (alt106)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:359:56: TRANSACTION
									{
										DebugLocation(359, 56);
										TRANSACTION281 = (CommonToken)Match(input, TRANSACTION, Follow._TRANSACTION_in_begin_stmt2553);
										TRANSACTION281_tree = (CommonTree)adaptor.Create(TRANSACTION281);
										adaptor.AddChild(root_0, TRANSACTION281_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(106); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("begin_stmt", 50);
					LeaveRule("begin_stmt", 50);
					Leave_begin_stmt();
				}
				DebugLocation(359, 68);
			}
			finally { DebugExitRule(GrammarFileName, "begin_stmt"); }
			return retval;

		}
		// $ANTLR end "begin_stmt"

		public class commit_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_commit_stmt();
		partial void Leave_commit_stmt();

		// $ANTLR start "commit_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:362:1: commit_stmt : ( COMMIT | END ) ( TRANSACTION )? ;
		[GrammarRule("commit_stmt")]
		private SQLiteParser.commit_stmt_return commit_stmt()
		{
			Enter_commit_stmt();
			EnterRule("commit_stmt", 51);
			TraceIn("commit_stmt", 51);
			SQLiteParser.commit_stmt_return retval = new SQLiteParser.commit_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set282 = null;
			CommonToken TRANSACTION283 = null;

			CommonTree set282_tree = null;
			CommonTree TRANSACTION283_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "commit_stmt");
				DebugLocation(362, 42);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:362:12: ( ( COMMIT | END ) ( TRANSACTION )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:362:14: ( COMMIT | END ) ( TRANSACTION )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(362, 14);
						set282 = (CommonToken)input.LT(1);
						if (input.LA(1) == END || input.LA(1) == COMMIT)
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set282));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}

						DebugLocation(362, 29);
						// C:\\Users\\Gareth\\Desktop\\test.g:362:29: ( TRANSACTION )?
						int alt107 = 2;
						try
						{
							DebugEnterSubRule(107);
							try
							{
								DebugEnterDecision(107, decisionCanBacktrack[107]);
								int LA107_0 = input.LA(1);

								if ((LA107_0 == TRANSACTION))
								{
									alt107 = 1;
								}
							}
							finally { DebugExitDecision(107); }
							switch (alt107)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:362:30: TRANSACTION
									{
										DebugLocation(362, 30);
										TRANSACTION283 = (CommonToken)Match(input, TRANSACTION, Follow._TRANSACTION_in_commit_stmt2572);
										TRANSACTION283_tree = (CommonTree)adaptor.Create(TRANSACTION283);
										adaptor.AddChild(root_0, TRANSACTION283_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(107); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("commit_stmt", 51);
					LeaveRule("commit_stmt", 51);
					Leave_commit_stmt();
				}
				DebugLocation(362, 42);
			}
			finally { DebugExitRule(GrammarFileName, "commit_stmt"); }
			return retval;

		}
		// $ANTLR end "commit_stmt"

		public class rollback_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_rollback_stmt();
		partial void Leave_rollback_stmt();

		// $ANTLR start "rollback_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:365:1: rollback_stmt : ROLLBACK ( TRANSACTION )? ( TO ( SAVEPOINT )? savepoint_name= id )? ;
		[GrammarRule("rollback_stmt")]
		private SQLiteParser.rollback_stmt_return rollback_stmt()
		{
			Enter_rollback_stmt();
			EnterRule("rollback_stmt", 52);
			TraceIn("rollback_stmt", 52);
			SQLiteParser.rollback_stmt_return retval = new SQLiteParser.rollback_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ROLLBACK284 = null;
			CommonToken TRANSACTION285 = null;
			CommonToken TO286 = null;
			CommonToken SAVEPOINT287 = null;
			SQLiteParser.id_return savepoint_name = default(SQLiteParser.id_return);

			CommonTree ROLLBACK284_tree = null;
			CommonTree TRANSACTION285_tree = null;
			CommonTree TO286_tree = null;
			CommonTree SAVEPOINT287_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "rollback_stmt");
				DebugLocation(365, 75);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:365:14: ( ROLLBACK ( TRANSACTION )? ( TO ( SAVEPOINT )? savepoint_name= id )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:365:16: ROLLBACK ( TRANSACTION )? ( TO ( SAVEPOINT )? savepoint_name= id )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(365, 16);
						ROLLBACK284 = (CommonToken)Match(input, ROLLBACK, Follow._ROLLBACK_in_rollback_stmt2582);
						ROLLBACK284_tree = (CommonTree)adaptor.Create(ROLLBACK284);
						adaptor.AddChild(root_0, ROLLBACK284_tree);

						DebugLocation(365, 25);
						// C:\\Users\\Gareth\\Desktop\\test.g:365:25: ( TRANSACTION )?
						int alt108 = 2;
						try
						{
							DebugEnterSubRule(108);
							try
							{
								DebugEnterDecision(108, decisionCanBacktrack[108]);
								int LA108_0 = input.LA(1);

								if ((LA108_0 == TRANSACTION))
								{
									alt108 = 1;
								}
							}
							finally { DebugExitDecision(108); }
							switch (alt108)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:365:26: TRANSACTION
									{
										DebugLocation(365, 26);
										TRANSACTION285 = (CommonToken)Match(input, TRANSACTION, Follow._TRANSACTION_in_rollback_stmt2585);
										TRANSACTION285_tree = (CommonTree)adaptor.Create(TRANSACTION285);
										adaptor.AddChild(root_0, TRANSACTION285_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(108); }

						DebugLocation(365, 40);
						// C:\\Users\\Gareth\\Desktop\\test.g:365:40: ( TO ( SAVEPOINT )? savepoint_name= id )?
						int alt110 = 2;
						try
						{
							DebugEnterSubRule(110);
							try
							{
								DebugEnterDecision(110, decisionCanBacktrack[110]);
								int LA110_0 = input.LA(1);

								if ((LA110_0 == TO))
								{
									alt110 = 1;
								}
							}
							finally { DebugExitDecision(110); }
							switch (alt110)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:365:41: TO ( SAVEPOINT )? savepoint_name= id
									{
										DebugLocation(365, 41);
										TO286 = (CommonToken)Match(input, TO, Follow._TO_in_rollback_stmt2590);
										TO286_tree = (CommonTree)adaptor.Create(TO286);
										adaptor.AddChild(root_0, TO286_tree);

										DebugLocation(365, 44);
										// C:\\Users\\Gareth\\Desktop\\test.g:365:44: ( SAVEPOINT )?
										int alt109 = 2;
										try
										{
											DebugEnterSubRule(109);
											try
											{
												DebugEnterDecision(109, decisionCanBacktrack[109]);
												int LA109_0 = input.LA(1);

												if ((LA109_0 == SAVEPOINT))
												{
													int LA109_1 = input.LA(2);

													if (((LA109_1 >= EXPLAIN && LA109_1 <= PLAN) || (LA109_1 >= INDEXED && LA109_1 <= BY) || (LA109_1 >= OR && LA109_1 <= ESCAPE) || (LA109_1 >= IS && LA109_1 <= BETWEEN) || (LA109_1 >= COLLATE && LA109_1 <= THEN) || LA109_1 == STRING || (LA109_1 >= CURRENT_TIME && LA109_1 <= CURRENT_TIMESTAMP) || (LA109_1 >= RAISE && LA109_1 <= ROW)))
													{
														alt109 = 1;
													}
												}
											}
											finally { DebugExitDecision(109); }
											switch (alt109)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:365:45: SAVEPOINT
													{
														DebugLocation(365, 45);
														SAVEPOINT287 = (CommonToken)Match(input, SAVEPOINT, Follow._SAVEPOINT_in_rollback_stmt2593);
														SAVEPOINT287_tree = (CommonTree)adaptor.Create(SAVEPOINT287);
														adaptor.AddChild(root_0, SAVEPOINT287_tree);


													}
													break;

											}
										}
										finally { DebugExitSubRule(109); }

										DebugLocation(365, 71);
										PushFollow(Follow._id_in_rollback_stmt2599);
										savepoint_name = id();
										PopFollow();

										adaptor.AddChild(root_0, savepoint_name.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(110); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("rollback_stmt", 52);
					LeaveRule("rollback_stmt", 52);
					Leave_rollback_stmt();
				}
				DebugLocation(365, 75);
			}
			finally { DebugExitRule(GrammarFileName, "rollback_stmt"); }
			return retval;

		}
		// $ANTLR end "rollback_stmt"

		public class savepoint_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_savepoint_stmt();
		partial void Leave_savepoint_stmt();

		// $ANTLR start "savepoint_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:368:1: savepoint_stmt : SAVEPOINT savepoint_name= id ;
		[GrammarRule("savepoint_stmt")]
		private SQLiteParser.savepoint_stmt_return savepoint_stmt()
		{
			Enter_savepoint_stmt();
			EnterRule("savepoint_stmt", 53);
			TraceIn("savepoint_stmt", 53);
			SQLiteParser.savepoint_stmt_return retval = new SQLiteParser.savepoint_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken SAVEPOINT288 = null;
			SQLiteParser.id_return savepoint_name = default(SQLiteParser.id_return);

			CommonTree SAVEPOINT288_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "savepoint_stmt");
				DebugLocation(368, 43);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:368:15: ( SAVEPOINT savepoint_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:368:17: SAVEPOINT savepoint_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(368, 17);
						SAVEPOINT288 = (CommonToken)Match(input, SAVEPOINT, Follow._SAVEPOINT_in_savepoint_stmt2609);
						SAVEPOINT288_tree = (CommonTree)adaptor.Create(SAVEPOINT288);
						adaptor.AddChild(root_0, SAVEPOINT288_tree);

						DebugLocation(368, 41);
						PushFollow(Follow._id_in_savepoint_stmt2613);
						savepoint_name = id();
						PopFollow();

						adaptor.AddChild(root_0, savepoint_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("savepoint_stmt", 53);
					LeaveRule("savepoint_stmt", 53);
					Leave_savepoint_stmt();
				}
				DebugLocation(368, 43);
			}
			finally { DebugExitRule(GrammarFileName, "savepoint_stmt"); }
			return retval;

		}
		// $ANTLR end "savepoint_stmt"

		public class release_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_release_stmt();
		partial void Leave_release_stmt();

		// $ANTLR start "release_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:371:1: release_stmt : RELEASE ( SAVEPOINT )? savepoint_name= id ;
		[GrammarRule("release_stmt")]
		private SQLiteParser.release_stmt_return release_stmt()
		{
			Enter_release_stmt();
			EnterRule("release_stmt", 54);
			TraceIn("release_stmt", 54);
			SQLiteParser.release_stmt_return retval = new SQLiteParser.release_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken RELEASE289 = null;
			CommonToken SAVEPOINT290 = null;
			SQLiteParser.id_return savepoint_name = default(SQLiteParser.id_return);

			CommonTree RELEASE289_tree = null;
			CommonTree SAVEPOINT290_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "release_stmt");
				DebugLocation(371, 52);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:371:13: ( RELEASE ( SAVEPOINT )? savepoint_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:371:15: RELEASE ( SAVEPOINT )? savepoint_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(371, 15);
						RELEASE289 = (CommonToken)Match(input, RELEASE, Follow._RELEASE_in_release_stmt2621);
						RELEASE289_tree = (CommonTree)adaptor.Create(RELEASE289);
						adaptor.AddChild(root_0, RELEASE289_tree);

						DebugLocation(371, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:371:23: ( SAVEPOINT )?
						int alt111 = 2;
						try
						{
							DebugEnterSubRule(111);
							try
							{
								DebugEnterDecision(111, decisionCanBacktrack[111]);
								int LA111_0 = input.LA(1);

								if ((LA111_0 == SAVEPOINT))
								{
									int LA111_1 = input.LA(2);

									if (((LA111_1 >= EXPLAIN && LA111_1 <= PLAN) || (LA111_1 >= INDEXED && LA111_1 <= BY) || (LA111_1 >= OR && LA111_1 <= ESCAPE) || (LA111_1 >= IS && LA111_1 <= BETWEEN) || (LA111_1 >= COLLATE && LA111_1 <= THEN) || LA111_1 == STRING || (LA111_1 >= CURRENT_TIME && LA111_1 <= CURRENT_TIMESTAMP) || (LA111_1 >= RAISE && LA111_1 <= ROW)))
									{
										alt111 = 1;
									}
								}
							}
							finally { DebugExitDecision(111); }
							switch (alt111)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:371:24: SAVEPOINT
									{
										DebugLocation(371, 24);
										SAVEPOINT290 = (CommonToken)Match(input, SAVEPOINT, Follow._SAVEPOINT_in_release_stmt2624);
										SAVEPOINT290_tree = (CommonTree)adaptor.Create(SAVEPOINT290);
										adaptor.AddChild(root_0, SAVEPOINT290_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(111); }

						DebugLocation(371, 50);
						PushFollow(Follow._id_in_release_stmt2630);
						savepoint_name = id();
						PopFollow();

						adaptor.AddChild(root_0, savepoint_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("release_stmt", 54);
					LeaveRule("release_stmt", 54);
					Leave_release_stmt();
				}
				DebugLocation(371, 52);
			}
			finally { DebugExitRule(GrammarFileName, "release_stmt"); }
			return retval;

		}
		// $ANTLR end "release_stmt"

		public class table_conflict_clause_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_table_conflict_clause();
		partial void Leave_table_conflict_clause();

		// $ANTLR start "table_conflict_clause"
		// C:\\Users\\Gareth\\Desktop\\test.g:378:1: table_conflict_clause : ON CONFLICT ( ROLLBACK | ABORT | FAIL | IGNORE | REPLACE ) ;
		[GrammarRule("table_conflict_clause")]
		private SQLiteParser.table_conflict_clause_return table_conflict_clause()
		{
			Enter_table_conflict_clause();
			EnterRule("table_conflict_clause", 55);
			TraceIn("table_conflict_clause", 55);
			SQLiteParser.table_conflict_clause_return retval = new SQLiteParser.table_conflict_clause_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ON291 = null;
			CommonToken CONFLICT292 = null;
			CommonToken set293 = null;

			CommonTree ON291_tree = null;
			CommonTree CONFLICT292_tree = null;
			CommonTree set293_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "table_conflict_clause");
				DebugLocation(378, 81);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:378:22: ( ON CONFLICT ( ROLLBACK | ABORT | FAIL | IGNORE | REPLACE ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:378:24: ON CONFLICT ( ROLLBACK | ABORT | FAIL | IGNORE | REPLACE )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(378, 26);
						ON291 = (CommonToken)Match(input, ON, Follow._ON_in_table_conflict_clause2642);
						DebugLocation(378, 36);
						CONFLICT292 = (CommonToken)Match(input, CONFLICT, Follow._CONFLICT_in_table_conflict_clause2645);
						CONFLICT292_tree = (CommonTree)adaptor.Create(CONFLICT292);
						root_0 = (CommonTree)adaptor.BecomeRoot(CONFLICT292_tree, root_0);

						DebugLocation(378, 38);
						set293 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= IGNORE && input.LA(1) <= FAIL) || input.LA(1) == REPLACE)
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set293));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("table_conflict_clause", 55);
					LeaveRule("table_conflict_clause", 55);
					Leave_table_conflict_clause();
				}
				DebugLocation(378, 81);
			}
			finally { DebugExitRule(GrammarFileName, "table_conflict_clause"); }
			return retval;

		}
		// $ANTLR end "table_conflict_clause"

		public class create_virtual_table_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_create_virtual_table_stmt();
		partial void Leave_create_virtual_table_stmt();

		// $ANTLR start "create_virtual_table_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:382:1: create_virtual_table_stmt : CREATE VIRTUAL TABLE (database_name= id DOT )? table_name= id USING module_name= id ( LPAREN column_def ( COMMA column_def )* RPAREN )? -> ^( CREATE_TABLE ^( OPTIONS VIRTUAL ) ^( $table_name ( $database_name)? ) ^( $module_name) ( ^( COLUMNS ( column_def )+ ) )? ) ;
		[GrammarRule("create_virtual_table_stmt")]
		private SQLiteParser.create_virtual_table_stmt_return create_virtual_table_stmt()
		{
			Enter_create_virtual_table_stmt();
			EnterRule("create_virtual_table_stmt", 56);
			TraceIn("create_virtual_table_stmt", 56);
			SQLiteParser.create_virtual_table_stmt_return retval = new SQLiteParser.create_virtual_table_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CREATE294 = null;
			CommonToken VIRTUAL295 = null;
			CommonToken TABLE296 = null;
			CommonToken DOT297 = null;
			CommonToken USING298 = null;
			CommonToken LPAREN299 = null;
			CommonToken COMMA301 = null;
			CommonToken RPAREN303 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return module_name = default(SQLiteParser.id_return);
			SQLiteParser.column_def_return column_def300 = default(SQLiteParser.column_def_return);
			SQLiteParser.column_def_return column_def302 = default(SQLiteParser.column_def_return);

			CommonTree CREATE294_tree = null;
			CommonTree VIRTUAL295_tree = null;
			CommonTree TABLE296_tree = null;
			CommonTree DOT297_tree = null;
			CommonTree USING298_tree = null;
			CommonTree LPAREN299_tree = null;
			CommonTree COMMA301_tree = null;
			CommonTree RPAREN303_tree = null;
			RewriteRuleITokenStream stream_TABLE = new RewriteRuleITokenStream(adaptor, "token TABLE");
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_CREATE = new RewriteRuleITokenStream(adaptor, "token CREATE");
			RewriteRuleITokenStream stream_USING = new RewriteRuleITokenStream(adaptor, "token USING");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_VIRTUAL = new RewriteRuleITokenStream(adaptor, "token VIRTUAL");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_column_def = new RewriteRuleSubtreeStream(adaptor, "rule column_def");
			try
			{
				DebugEnterRule(GrammarFileName, "create_virtual_table_stmt");
				DebugLocation(382, 110);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:382:26: ( CREATE VIRTUAL TABLE (database_name= id DOT )? table_name= id USING module_name= id ( LPAREN column_def ( COMMA column_def )* RPAREN )? -> ^( CREATE_TABLE ^( OPTIONS VIRTUAL ) ^( $table_name ( $database_name)? ) ^( $module_name) ( ^( COLUMNS ( column_def )+ ) )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:382:28: CREATE VIRTUAL TABLE (database_name= id DOT )? table_name= id USING module_name= id ( LPAREN column_def ( COMMA column_def )* RPAREN )?
					{
						DebugLocation(382, 28);
						CREATE294 = (CommonToken)Match(input, CREATE, Follow._CREATE_in_create_virtual_table_stmt2675);
						stream_CREATE.Add(CREATE294);

						DebugLocation(382, 35);
						VIRTUAL295 = (CommonToken)Match(input, VIRTUAL, Follow._VIRTUAL_in_create_virtual_table_stmt2677);
						stream_VIRTUAL.Add(VIRTUAL295);

						DebugLocation(382, 43);
						TABLE296 = (CommonToken)Match(input, TABLE, Follow._TABLE_in_create_virtual_table_stmt2679);
						stream_TABLE.Add(TABLE296);

						DebugLocation(382, 49);
						// C:\\Users\\Gareth\\Desktop\\test.g:382:49: (database_name= id DOT )?
						int alt112 = 2;
						try
						{
							DebugEnterSubRule(112);
							try
							{
								DebugEnterDecision(112, decisionCanBacktrack[112]);
								int LA112_0 = input.LA(1);

								if ((LA112_0 == ID || LA112_0 == STRING))
								{
									int LA112_1 = input.LA(2);

									if ((LA112_1 == DOT))
									{
										alt112 = 1;
									}
								}
								else if (((LA112_0 >= EXPLAIN && LA112_0 <= PLAN) || (LA112_0 >= INDEXED && LA112_0 <= BY) || (LA112_0 >= OR && LA112_0 <= ESCAPE) || (LA112_0 >= IS && LA112_0 <= BETWEEN) || LA112_0 == COLLATE || (LA112_0 >= DISTINCT && LA112_0 <= THEN) || (LA112_0 >= CURRENT_TIME && LA112_0 <= CURRENT_TIMESTAMP) || (LA112_0 >= RAISE && LA112_0 <= ROW)))
								{
									int LA112_2 = input.LA(2);

									if ((LA112_2 == DOT))
									{
										alt112 = 1;
									}
								}
							}
							finally { DebugExitDecision(112); }
							switch (alt112)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:382:50: database_name= id DOT
									{
										DebugLocation(382, 63);
										PushFollow(Follow._id_in_create_virtual_table_stmt2684);
										database_name = id();
										PopFollow();

										stream_id.Add(database_name.Tree);
										DebugLocation(382, 67);
										DOT297 = (CommonToken)Match(input, DOT, Follow._DOT_in_create_virtual_table_stmt2686);
										stream_DOT.Add(DOT297);


									}
									break;

							}
						}
						finally { DebugExitSubRule(112); }

						DebugLocation(382, 83);
						PushFollow(Follow._id_in_create_virtual_table_stmt2692);
						table_name = id();
						PopFollow();

						stream_id.Add(table_name.Tree);
						DebugLocation(383, 3);
						USING298 = (CommonToken)Match(input, USING, Follow._USING_in_create_virtual_table_stmt2696);
						stream_USING.Add(USING298);

						DebugLocation(383, 20);
						PushFollow(Follow._id_in_create_virtual_table_stmt2700);
						module_name = id();
						PopFollow();

						stream_id.Add(module_name.Tree);
						DebugLocation(383, 24);
						// C:\\Users\\Gareth\\Desktop\\test.g:383:24: ( LPAREN column_def ( COMMA column_def )* RPAREN )?
						int alt114 = 2;
						try
						{
							DebugEnterSubRule(114);
							try
							{
								DebugEnterDecision(114, decisionCanBacktrack[114]);
								int LA114_0 = input.LA(1);

								if ((LA114_0 == LPAREN))
								{
									alt114 = 1;
								}
							}
							finally { DebugExitDecision(114); }
							switch (alt114)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:383:25: LPAREN column_def ( COMMA column_def )* RPAREN
									{
										DebugLocation(383, 25);
										LPAREN299 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_create_virtual_table_stmt2703);
										stream_LPAREN.Add(LPAREN299);

										DebugLocation(383, 32);
										PushFollow(Follow._column_def_in_create_virtual_table_stmt2705);
										column_def300 = column_def();
										PopFollow();

										stream_column_def.Add(column_def300.Tree);
										DebugLocation(383, 43);
										// C:\\Users\\Gareth\\Desktop\\test.g:383:43: ( COMMA column_def )*
										try
										{
											DebugEnterSubRule(113);
											while (true)
											{
												int alt113 = 2;
												try
												{
													DebugEnterDecision(113, decisionCanBacktrack[113]);
													int LA113_0 = input.LA(1);

													if ((LA113_0 == COMMA))
													{
														alt113 = 1;
													}


												}
												finally { DebugExitDecision(113); }
												switch (alt113)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:383:44: COMMA column_def
														{
															DebugLocation(383, 44);
															COMMA301 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_create_virtual_table_stmt2708);
															stream_COMMA.Add(COMMA301);

															DebugLocation(383, 50);
															PushFollow(Follow._column_def_in_create_virtual_table_stmt2710);
															column_def302 = column_def();
															PopFollow();

															stream_column_def.Add(column_def302.Tree);

														}
														break;

													default:
														goto loop113;
												}
											}

										loop113:
											;

										}
										finally { DebugExitSubRule(113); }

										DebugLocation(383, 63);
										RPAREN303 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_create_virtual_table_stmt2714);
										stream_RPAREN.Add(RPAREN303);


									}
									break;

							}
						}
						finally { DebugExitSubRule(114); }



						{
							// AST REWRITE
							// elements: VIRTUAL, module_name, database_name, column_def, table_name
							// token labels: 
							// rule labels: database_name, retval, table_name, module_name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);
							RewriteRuleSubtreeStream stream_module_name = new RewriteRuleSubtreeStream(adaptor, "rule module_name", module_name != null ? module_name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 384:3: -> ^( CREATE_TABLE ^( OPTIONS VIRTUAL ) ^( $table_name ( $database_name)? ) ^( $module_name) ( ^( COLUMNS ( column_def )+ ) )? )
							{
								DebugLocation(384, 6);
								// C:\\Users\\Gareth\\Desktop\\test.g:384:6: ^( CREATE_TABLE ^( OPTIONS VIRTUAL ) ^( $table_name ( $database_name)? ) ^( $module_name) ( ^( COLUMNS ( column_def )+ ) )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(384, 8);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(CREATE_TABLE, "CREATE_TABLE"), root_1);

									DebugLocation(384, 21);
									// C:\\Users\\Gareth\\Desktop\\test.g:384:21: ^( OPTIONS VIRTUAL )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(384, 23);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(OPTIONS, "OPTIONS"), root_2);

										DebugLocation(384, 31);
										adaptor.AddChild(root_2, stream_VIRTUAL.NextNode());

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(384, 40);
									// C:\\Users\\Gareth\\Desktop\\test.g:384:40: ^( $table_name ( $database_name)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(384, 42);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_table_name.NextNode(), root_2);

										DebugLocation(384, 54);
										// C:\\Users\\Gareth\\Desktop\\test.g:384:54: ( $database_name)?
										if (stream_database_name.HasNext)
										{
											DebugLocation(384, 54);
											adaptor.AddChild(root_2, stream_database_name.NextTree());

										}
										stream_database_name.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(384, 71);
									// C:\\Users\\Gareth\\Desktop\\test.g:384:71: ^( $module_name)
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(384, 73);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_module_name.NextNode(), root_2);

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(384, 87);
									// C:\\Users\\Gareth\\Desktop\\test.g:384:87: ( ^( COLUMNS ( column_def )+ ) )?
									if (stream_column_def.HasNext)
									{
										DebugLocation(384, 87);
										// C:\\Users\\Gareth\\Desktop\\test.g:384:87: ^( COLUMNS ( column_def )+ )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(384, 89);
											root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

											DebugLocation(384, 97);
											if (!(stream_column_def.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_column_def.HasNext)
											{
												DebugLocation(384, 97);
												adaptor.AddChild(root_2, stream_column_def.NextTree());

											}
											stream_column_def.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_column_def.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("create_virtual_table_stmt", 56);
					LeaveRule("create_virtual_table_stmt", 56);
					Leave_create_virtual_table_stmt();
				}
				DebugLocation(384, 110);
			}
			finally { DebugExitRule(GrammarFileName, "create_virtual_table_stmt"); }
			return retval;

		}
		// $ANTLR end "create_virtual_table_stmt"

		public class create_table_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_create_table_stmt();
		partial void Leave_create_table_stmt();

		// $ANTLR start "create_table_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:387:1: create_table_stmt : CREATE ( TEMPORARY )? TABLE ( IF NOT EXISTS )? (database_name= id DOT )? table_name= id ( LPAREN column_def ( COMMA column_def )* ( COMMA table_constraint )* RPAREN | AS select_stmt ) -> ^( CREATE_TABLE ^( OPTIONS ( TEMPORARY )? ( EXISTS )? ) ^( $table_name ( $database_name)? ) ( ^( COLUMNS ( column_def )+ ) )? ( ^( CONSTRAINTS ( table_constraint )* ) )? ( select_stmt )? ) ;
		[GrammarRule("create_table_stmt")]
		private SQLiteParser.create_table_stmt_return create_table_stmt()
		{
			Enter_create_table_stmt();
			EnterRule("create_table_stmt", 57);
			TraceIn("create_table_stmt", 57);
			SQLiteParser.create_table_stmt_return retval = new SQLiteParser.create_table_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CREATE304 = null;
			CommonToken TEMPORARY305 = null;
			CommonToken TABLE306 = null;
			CommonToken IF307 = null;
			CommonToken NOT308 = null;
			CommonToken EXISTS309 = null;
			CommonToken DOT310 = null;
			CommonToken LPAREN311 = null;
			CommonToken COMMA313 = null;
			CommonToken COMMA315 = null;
			CommonToken RPAREN317 = null;
			CommonToken AS318 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.column_def_return column_def312 = default(SQLiteParser.column_def_return);
			SQLiteParser.column_def_return column_def314 = default(SQLiteParser.column_def_return);
			SQLiteParser.table_constraint_return table_constraint316 = default(SQLiteParser.table_constraint_return);
			SQLiteParser.select_stmt_return select_stmt319 = default(SQLiteParser.select_stmt_return);

			CommonTree CREATE304_tree = null;
			CommonTree TEMPORARY305_tree = null;
			CommonTree TABLE306_tree = null;
			CommonTree IF307_tree = null;
			CommonTree NOT308_tree = null;
			CommonTree EXISTS309_tree = null;
			CommonTree DOT310_tree = null;
			CommonTree LPAREN311_tree = null;
			CommonTree COMMA313_tree = null;
			CommonTree COMMA315_tree = null;
			CommonTree RPAREN317_tree = null;
			CommonTree AS318_tree = null;
			RewriteRuleITokenStream stream_TABLE = new RewriteRuleITokenStream(adaptor, "token TABLE");
			RewriteRuleITokenStream stream_AS = new RewriteRuleITokenStream(adaptor, "token AS");
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_CREATE = new RewriteRuleITokenStream(adaptor, "token CREATE");
			RewriteRuleITokenStream stream_NOT = new RewriteRuleITokenStream(adaptor, "token NOT");
			RewriteRuleITokenStream stream_EXISTS = new RewriteRuleITokenStream(adaptor, "token EXISTS");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_TEMPORARY = new RewriteRuleITokenStream(adaptor, "token TEMPORARY");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleITokenStream stream_IF = new RewriteRuleITokenStream(adaptor, "token IF");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_select_stmt = new RewriteRuleSubtreeStream(adaptor, "rule select_stmt");
			RewriteRuleSubtreeStream stream_column_def = new RewriteRuleSubtreeStream(adaptor, "rule column_def");
			RewriteRuleSubtreeStream stream_table_constraint = new RewriteRuleSubtreeStream(adaptor, "rule table_constraint");
			try
			{
				DebugEnterRule(GrammarFileName, "create_table_stmt");
				DebugLocation(387, 73);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:387:18: ( CREATE ( TEMPORARY )? TABLE ( IF NOT EXISTS )? (database_name= id DOT )? table_name= id ( LPAREN column_def ( COMMA column_def )* ( COMMA table_constraint )* RPAREN | AS select_stmt ) -> ^( CREATE_TABLE ^( OPTIONS ( TEMPORARY )? ( EXISTS )? ) ^( $table_name ( $database_name)? ) ( ^( COLUMNS ( column_def )+ ) )? ( ^( CONSTRAINTS ( table_constraint )* ) )? ( select_stmt )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:387:20: CREATE ( TEMPORARY )? TABLE ( IF NOT EXISTS )? (database_name= id DOT )? table_name= id ( LPAREN column_def ( COMMA column_def )* ( COMMA table_constraint )* RPAREN | AS select_stmt )
					{
						DebugLocation(387, 20);
						CREATE304 = (CommonToken)Match(input, CREATE, Follow._CREATE_in_create_table_stmt2760);
						stream_CREATE.Add(CREATE304);

						DebugLocation(387, 27);
						// C:\\Users\\Gareth\\Desktop\\test.g:387:27: ( TEMPORARY )?
						int alt115 = 2;
						try
						{
							DebugEnterSubRule(115);
							try
							{
								DebugEnterDecision(115, decisionCanBacktrack[115]);
								int LA115_0 = input.LA(1);

								if ((LA115_0 == TEMPORARY))
								{
									alt115 = 1;
								}
							}
							finally { DebugExitDecision(115); }
							switch (alt115)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:387:27: TEMPORARY
									{
										DebugLocation(387, 27);
										TEMPORARY305 = (CommonToken)Match(input, TEMPORARY, Follow._TEMPORARY_in_create_table_stmt2762);
										stream_TEMPORARY.Add(TEMPORARY305);


									}
									break;

							}
						}
						finally { DebugExitSubRule(115); }

						DebugLocation(387, 38);
						TABLE306 = (CommonToken)Match(input, TABLE, Follow._TABLE_in_create_table_stmt2765);
						stream_TABLE.Add(TABLE306);

						DebugLocation(387, 44);
						// C:\\Users\\Gareth\\Desktop\\test.g:387:44: ( IF NOT EXISTS )?
						int alt116 = 2;
						try
						{
							DebugEnterSubRule(116);
							try
							{
								DebugEnterDecision(116, decisionCanBacktrack[116]);
								int LA116_0 = input.LA(1);

								if ((LA116_0 == IF))
								{
									int LA116_1 = input.LA(2);

									if ((LA116_1 == NOT))
									{
										alt116 = 1;
									}
								}
							}
							finally { DebugExitDecision(116); }
							switch (alt116)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:387:45: IF NOT EXISTS
									{
										DebugLocation(387, 45);
										IF307 = (CommonToken)Match(input, IF, Follow._IF_in_create_table_stmt2768);
										stream_IF.Add(IF307);

										DebugLocation(387, 48);
										NOT308 = (CommonToken)Match(input, NOT, Follow._NOT_in_create_table_stmt2770);
										stream_NOT.Add(NOT308);

										DebugLocation(387, 52);
										EXISTS309 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_create_table_stmt2772);
										stream_EXISTS.Add(EXISTS309);


									}
									break;

							}
						}
						finally { DebugExitSubRule(116); }

						DebugLocation(387, 61);
						// C:\\Users\\Gareth\\Desktop\\test.g:387:61: (database_name= id DOT )?
						int alt117 = 2;
						try
						{
							DebugEnterSubRule(117);
							try
							{
								DebugEnterDecision(117, decisionCanBacktrack[117]);
								int LA117_0 = input.LA(1);

								if ((LA117_0 == ID || LA117_0 == STRING))
								{
									int LA117_1 = input.LA(2);

									if ((LA117_1 == DOT))
									{
										alt117 = 1;
									}
								}
								else if (((LA117_0 >= EXPLAIN && LA117_0 <= PLAN) || (LA117_0 >= INDEXED && LA117_0 <= BY) || (LA117_0 >= OR && LA117_0 <= ESCAPE) || (LA117_0 >= IS && LA117_0 <= BETWEEN) || LA117_0 == COLLATE || (LA117_0 >= DISTINCT && LA117_0 <= THEN) || (LA117_0 >= CURRENT_TIME && LA117_0 <= CURRENT_TIMESTAMP) || (LA117_0 >= RAISE && LA117_0 <= ROW)))
								{
									int LA117_2 = input.LA(2);

									if ((LA117_2 == DOT))
									{
										alt117 = 1;
									}
								}
							}
							finally { DebugExitDecision(117); }
							switch (alt117)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:387:62: database_name= id DOT
									{
										DebugLocation(387, 75);
										PushFollow(Follow._id_in_create_table_stmt2779);
										database_name = id();
										PopFollow();

										stream_id.Add(database_name.Tree);
										DebugLocation(387, 79);
										DOT310 = (CommonToken)Match(input, DOT, Follow._DOT_in_create_table_stmt2781);
										stream_DOT.Add(DOT310);


									}
									break;

							}
						}
						finally { DebugExitSubRule(117); }

						DebugLocation(387, 95);
						PushFollow(Follow._id_in_create_table_stmt2787);
						table_name = id();
						PopFollow();

						stream_id.Add(table_name.Tree);
						DebugLocation(388, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:388:3: ( LPAREN column_def ( COMMA column_def )* ( COMMA table_constraint )* RPAREN | AS select_stmt )
						int alt120 = 2;
						try
						{
							DebugEnterSubRule(120);
							try
							{
								DebugEnterDecision(120, decisionCanBacktrack[120]);
								int LA120_0 = input.LA(1);

								if ((LA120_0 == LPAREN))
								{
									alt120 = 1;
								}
								else if ((LA120_0 == AS))
								{
									alt120 = 2;
								}
								else
								{
									NoViableAltException nvae = new NoViableAltException("", 120, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
							}
							finally { DebugExitDecision(120); }
							switch (alt120)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:388:5: LPAREN column_def ( COMMA column_def )* ( COMMA table_constraint )* RPAREN
									{
										DebugLocation(388, 5);
										LPAREN311 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_create_table_stmt2793);
										stream_LPAREN.Add(LPAREN311);

										DebugLocation(388, 12);
										PushFollow(Follow._column_def_in_create_table_stmt2795);
										column_def312 = column_def();
										PopFollow();

										stream_column_def.Add(column_def312.Tree);
										DebugLocation(388, 23);
										// C:\\Users\\Gareth\\Desktop\\test.g:388:23: ( COMMA column_def )*
										try
										{
											DebugEnterSubRule(118);
											while (true)
											{
												int alt118 = 2;
												try
												{
													DebugEnterDecision(118, decisionCanBacktrack[118]);
													try
													{
														alt118 = dfa118.Predict(input);
													}
													catch (NoViableAltException nvae)
													{
														DebugRecognitionException(nvae);
														throw;
													}
												}
												finally { DebugExitDecision(118); }
												switch (alt118)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:388:24: COMMA column_def
														{
															DebugLocation(388, 24);
															COMMA313 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_create_table_stmt2798);
															stream_COMMA.Add(COMMA313);

															DebugLocation(388, 30);
															PushFollow(Follow._column_def_in_create_table_stmt2800);
															column_def314 = column_def();
															PopFollow();

															stream_column_def.Add(column_def314.Tree);

														}
														break;

													default:
														goto loop118;
												}
											}

										loop118:
											;

										}
										finally { DebugExitSubRule(118); }

										DebugLocation(388, 43);
										// C:\\Users\\Gareth\\Desktop\\test.g:388:43: ( COMMA table_constraint )*
										try
										{
											DebugEnterSubRule(119);
											while (true)
											{
												int alt119 = 2;
												try
												{
													DebugEnterDecision(119, decisionCanBacktrack[119]);
													int LA119_0 = input.LA(1);

													if ((LA119_0 == COMMA))
													{
														alt119 = 1;
													}


												}
												finally { DebugExitDecision(119); }
												switch (alt119)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:388:44: COMMA table_constraint
														{
															DebugLocation(388, 44);
															COMMA315 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_create_table_stmt2805);
															stream_COMMA.Add(COMMA315);

															DebugLocation(388, 50);
															PushFollow(Follow._table_constraint_in_create_table_stmt2807);
															table_constraint316 = table_constraint();
															PopFollow();

															stream_table_constraint.Add(table_constraint316.Tree);

														}
														break;

													default:
														goto loop119;
												}
											}

										loop119:
											;

										}
										finally { DebugExitSubRule(119); }

										DebugLocation(388, 69);
										RPAREN317 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_create_table_stmt2811);
										stream_RPAREN.Add(RPAREN317);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:389:5: AS select_stmt
									{
										DebugLocation(389, 5);
										AS318 = (CommonToken)Match(input, AS, Follow._AS_in_create_table_stmt2817);
										stream_AS.Add(AS318);

										DebugLocation(389, 8);
										PushFollow(Follow._select_stmt_in_create_table_stmt2819);
										select_stmt319 = select_stmt();
										PopFollow();

										stream_select_stmt.Add(select_stmt319.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(120); }



						{
							// AST REWRITE
							// elements: EXISTS, table_name, select_stmt, TEMPORARY, table_constraint, column_def, database_name
							// token labels: 
							// rule labels: database_name, retval, table_name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 390:1: -> ^( CREATE_TABLE ^( OPTIONS ( TEMPORARY )? ( EXISTS )? ) ^( $table_name ( $database_name)? ) ( ^( COLUMNS ( column_def )+ ) )? ( ^( CONSTRAINTS ( table_constraint )* ) )? ( select_stmt )? )
							{
								DebugLocation(390, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:390:4: ^( CREATE_TABLE ^( OPTIONS ( TEMPORARY )? ( EXISTS )? ) ^( $table_name ( $database_name)? ) ( ^( COLUMNS ( column_def )+ ) )? ( ^( CONSTRAINTS ( table_constraint )* ) )? ( select_stmt )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(390, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(CREATE_TABLE, "CREATE_TABLE"), root_1);

									DebugLocation(390, 19);
									// C:\\Users\\Gareth\\Desktop\\test.g:390:19: ^( OPTIONS ( TEMPORARY )? ( EXISTS )? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(390, 21);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(OPTIONS, "OPTIONS"), root_2);

										DebugLocation(390, 29);
										// C:\\Users\\Gareth\\Desktop\\test.g:390:29: ( TEMPORARY )?
										if (stream_TEMPORARY.HasNext)
										{
											DebugLocation(390, 29);
											adaptor.AddChild(root_2, stream_TEMPORARY.NextNode());

										}
										stream_TEMPORARY.Reset();
										DebugLocation(390, 40);
										// C:\\Users\\Gareth\\Desktop\\test.g:390:40: ( EXISTS )?
										if (stream_EXISTS.HasNext)
										{
											DebugLocation(390, 40);
											adaptor.AddChild(root_2, stream_EXISTS.NextNode());

										}
										stream_EXISTS.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(390, 49);
									// C:\\Users\\Gareth\\Desktop\\test.g:390:49: ^( $table_name ( $database_name)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(390, 51);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_table_name.NextNode(), root_2);

										DebugLocation(390, 63);
										// C:\\Users\\Gareth\\Desktop\\test.g:390:63: ( $database_name)?
										if (stream_database_name.HasNext)
										{
											DebugLocation(390, 63);
											adaptor.AddChild(root_2, stream_database_name.NextTree());

										}
										stream_database_name.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(391, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:391:3: ( ^( COLUMNS ( column_def )+ ) )?
									if (stream_column_def.HasNext)
									{
										DebugLocation(391, 3);
										// C:\\Users\\Gareth\\Desktop\\test.g:391:3: ^( COLUMNS ( column_def )+ )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(391, 5);
											root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

											DebugLocation(391, 13);
											if (!(stream_column_def.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_column_def.HasNext)
											{
												DebugLocation(391, 13);
												adaptor.AddChild(root_2, stream_column_def.NextTree());

											}
											stream_column_def.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_column_def.Reset();
									DebugLocation(391, 27);
									// C:\\Users\\Gareth\\Desktop\\test.g:391:27: ( ^( CONSTRAINTS ( table_constraint )* ) )?
									if (stream_table_constraint.HasNext)
									{
										DebugLocation(391, 27);
										// C:\\Users\\Gareth\\Desktop\\test.g:391:27: ^( CONSTRAINTS ( table_constraint )* )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(391, 29);
											root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(CONSTRAINTS, "CONSTRAINTS"), root_2);

											DebugLocation(391, 41);
											// C:\\Users\\Gareth\\Desktop\\test.g:391:41: ( table_constraint )*
											while (stream_table_constraint.HasNext)
											{
												DebugLocation(391, 41);
												adaptor.AddChild(root_2, stream_table_constraint.NextTree());

											}
											stream_table_constraint.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_table_constraint.Reset();
									DebugLocation(391, 61);
									// C:\\Users\\Gareth\\Desktop\\test.g:391:61: ( select_stmt )?
									if (stream_select_stmt.HasNext)
									{
										DebugLocation(391, 61);
										adaptor.AddChild(root_1, stream_select_stmt.NextTree());

									}
									stream_select_stmt.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("create_table_stmt", 57);
					LeaveRule("create_table_stmt", 57);
					Leave_create_table_stmt();
				}
				DebugLocation(391, 73);
			}
			finally { DebugExitRule(GrammarFileName, "create_table_stmt"); }
			return retval;

		}
		// $ANTLR end "create_table_stmt"

		public class column_def_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_def();
		partial void Leave_column_def();

		// $ANTLR start "column_def"
		// C:\\Users\\Gareth\\Desktop\\test.g:393:1: column_def : name= id_column_def ( type_name )? ( column_constraint )* -> ^( $name ^( CONSTRAINTS ( column_constraint )* ) ( type_name )? ) ;
		[GrammarRule("column_def")]
		private SQLiteParser.column_def_return column_def()
		{
			Enter_column_def();
			EnterRule("column_def", 58);
			TraceIn("column_def", 58);
			SQLiteParser.column_def_return retval = new SQLiteParser.column_def_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.id_column_def_return name = default(SQLiteParser.id_column_def_return);
			SQLiteParser.type_name_return type_name320 = default(SQLiteParser.type_name_return);
			SQLiteParser.column_constraint_return column_constraint321 = default(SQLiteParser.column_constraint_return);

			RewriteRuleSubtreeStream stream_column_constraint = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint");
			RewriteRuleSubtreeStream stream_id_column_def = new RewriteRuleSubtreeStream(adaptor, "rule id_column_def");
			RewriteRuleSubtreeStream stream_type_name = new RewriteRuleSubtreeStream(adaptor, "rule type_name");
			try
			{
				DebugEnterRule(GrammarFileName, "column_def");
				DebugLocation(393, 56);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:393:11: (name= id_column_def ( type_name )? ( column_constraint )* -> ^( $name ^( CONSTRAINTS ( column_constraint )* ) ( type_name )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:393:13: name= id_column_def ( type_name )? ( column_constraint )*
					{
						DebugLocation(393, 17);
						PushFollow(Follow._id_column_def_in_column_def2875);
						name = id_column_def();
						PopFollow();

						stream_id_column_def.Add(name.Tree);
						DebugLocation(393, 32);
						// C:\\Users\\Gareth\\Desktop\\test.g:393:32: ( type_name )?
						int alt121 = 2;
						try
						{
							DebugEnterSubRule(121);
							try
							{
								DebugEnterDecision(121, decisionCanBacktrack[121]);
								try
								{
									alt121 = dfa121.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(121); }
							switch (alt121)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:393:32: type_name
									{
										DebugLocation(393, 32);
										PushFollow(Follow._type_name_in_column_def2877);
										type_name320 = type_name();
										PopFollow();

										stream_type_name.Add(type_name320.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(121); }

						DebugLocation(393, 43);
						// C:\\Users\\Gareth\\Desktop\\test.g:393:43: ( column_constraint )*
						try
						{
							DebugEnterSubRule(122);
							while (true)
							{
								int alt122 = 2;
								try
								{
									DebugEnterDecision(122, decisionCanBacktrack[122]);
									try
									{
										alt122 = dfa122.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(122); }
								switch (alt122)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:393:43: column_constraint
										{
											DebugLocation(393, 43);
											PushFollow(Follow._column_constraint_in_column_def2880);
											column_constraint321 = column_constraint();
											PopFollow();

											stream_column_constraint.Add(column_constraint321.Tree);

										}
										break;

									default:
										goto loop122;
								}
							}

						loop122:
							;

						}
						finally { DebugExitSubRule(122); }



						{
							// AST REWRITE
							// elements: type_name, name, column_constraint
							// token labels: 
							// rule labels: retval, name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_name = new RewriteRuleSubtreeStream(adaptor, "rule name", name != null ? name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 394:1: -> ^( $name ^( CONSTRAINTS ( column_constraint )* ) ( type_name )? )
							{
								DebugLocation(394, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:394:4: ^( $name ^( CONSTRAINTS ( column_constraint )* ) ( type_name )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(394, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_name.NextNode(), root_1);

									DebugLocation(394, 12);
									// C:\\Users\\Gareth\\Desktop\\test.g:394:12: ^( CONSTRAINTS ( column_constraint )* )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(394, 14);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(CONSTRAINTS, "CONSTRAINTS"), root_2);

										DebugLocation(394, 26);
										// C:\\Users\\Gareth\\Desktop\\test.g:394:26: ( column_constraint )*
										while (stream_column_constraint.HasNext)
										{
											DebugLocation(394, 26);
											adaptor.AddChild(root_2, stream_column_constraint.NextTree());

										}
										stream_column_constraint.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(394, 46);
									// C:\\Users\\Gareth\\Desktop\\test.g:394:46: ( type_name )?
									if (stream_type_name.HasNext)
									{
										DebugLocation(394, 46);
										adaptor.AddChild(root_1, stream_type_name.NextTree());

									}
									stream_type_name.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_def", 58);
					LeaveRule("column_def", 58);
					Leave_column_def();
				}
				DebugLocation(394, 56);
			}
			finally { DebugExitRule(GrammarFileName, "column_def"); }
			return retval;

		}
		// $ANTLR end "column_def"

		public class column_constraint_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint();
		partial void Leave_column_constraint();

		// $ANTLR start "column_constraint"
		// C:\\Users\\Gareth\\Desktop\\test.g:396:1: column_constraint : ( CONSTRAINT name= id )? ( column_constraint_pk | column_constraint_identity | column_constraint_not_null | column_constraint_null | column_constraint_unique | column_constraint_check | column_constraint_default | column_constraint_collate | fk_clause ) -> ^( COLUMN_CONSTRAINT ( column_constraint_pk )? ( column_constraint_identity )? ( column_constraint_not_null )? ( column_constraint_null )? ( column_constraint_unique )? ( column_constraint_check )? ( column_constraint_default )? ( column_constraint_collate )? ( fk_clause )? ( $name)? ) ;
		[GrammarRule("column_constraint")]
		private SQLiteParser.column_constraint_return column_constraint()
		{
			Enter_column_constraint();
			EnterRule("column_constraint", 59);
			TraceIn("column_constraint", 59);
			SQLiteParser.column_constraint_return retval = new SQLiteParser.column_constraint_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CONSTRAINT322 = null;
			SQLiteParser.id_return name = default(SQLiteParser.id_return);
			SQLiteParser.column_constraint_pk_return column_constraint_pk323 = default(SQLiteParser.column_constraint_pk_return);
			SQLiteParser.column_constraint_identity_return column_constraint_identity324 = default(SQLiteParser.column_constraint_identity_return);
			SQLiteParser.column_constraint_not_null_return column_constraint_not_null325 = default(SQLiteParser.column_constraint_not_null_return);
			SQLiteParser.column_constraint_null_return column_constraint_null326 = default(SQLiteParser.column_constraint_null_return);
			SQLiteParser.column_constraint_unique_return column_constraint_unique327 = default(SQLiteParser.column_constraint_unique_return);
			SQLiteParser.column_constraint_check_return column_constraint_check328 = default(SQLiteParser.column_constraint_check_return);
			SQLiteParser.column_constraint_default_return column_constraint_default329 = default(SQLiteParser.column_constraint_default_return);
			SQLiteParser.column_constraint_collate_return column_constraint_collate330 = default(SQLiteParser.column_constraint_collate_return);
			SQLiteParser.fk_clause_return fk_clause331 = default(SQLiteParser.fk_clause_return);

			CommonTree CONSTRAINT322_tree = null;
			RewriteRuleITokenStream stream_CONSTRAINT = new RewriteRuleITokenStream(adaptor, "token CONSTRAINT");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_column_constraint_default = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_default");
			RewriteRuleSubtreeStream stream_column_constraint_check = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_check");
			RewriteRuleSubtreeStream stream_column_constraint_pk = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_pk");
			RewriteRuleSubtreeStream stream_column_constraint_null = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_null");
			RewriteRuleSubtreeStream stream_column_constraint_identity = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_identity");
			RewriteRuleSubtreeStream stream_column_constraint_collate = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_collate");
			RewriteRuleSubtreeStream stream_column_constraint_unique = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_unique");
			RewriteRuleSubtreeStream stream_fk_clause = new RewriteRuleSubtreeStream(adaptor, "rule fk_clause");
			RewriteRuleSubtreeStream stream_column_constraint_not_null = new RewriteRuleSubtreeStream(adaptor, "rule column_constraint_not_null");
			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint");
				DebugLocation(396, 9);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:396:18: ( ( CONSTRAINT name= id )? ( column_constraint_pk | column_constraint_identity | column_constraint_not_null | column_constraint_null | column_constraint_unique | column_constraint_check | column_constraint_default | column_constraint_collate | fk_clause ) -> ^( COLUMN_CONSTRAINT ( column_constraint_pk )? ( column_constraint_identity )? ( column_constraint_not_null )? ( column_constraint_null )? ( column_constraint_unique )? ( column_constraint_check )? ( column_constraint_default )? ( column_constraint_collate )? ( fk_clause )? ( $name)? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:396:20: ( CONSTRAINT name= id )? ( column_constraint_pk | column_constraint_identity | column_constraint_not_null | column_constraint_null | column_constraint_unique | column_constraint_check | column_constraint_default | column_constraint_collate | fk_clause )
					{
						DebugLocation(396, 20);
						// C:\\Users\\Gareth\\Desktop\\test.g:396:20: ( CONSTRAINT name= id )?
						int alt123 = 2;
						try
						{
							DebugEnterSubRule(123);
							try
							{
								DebugEnterDecision(123, decisionCanBacktrack[123]);
								try
								{
									alt123 = dfa123.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(123); }
							switch (alt123)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:396:21: CONSTRAINT name= id
									{
										DebugLocation(396, 21);
										CONSTRAINT322 = (CommonToken)Match(input, CONSTRAINT, Follow._CONSTRAINT_in_column_constraint2906);
										stream_CONSTRAINT.Add(CONSTRAINT322);

										DebugLocation(396, 36);
										PushFollow(Follow._id_in_column_constraint2910);
										name = id();
										PopFollow();

										stream_id.Add(name.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(123); }

						DebugLocation(397, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:397:3: ( column_constraint_pk | column_constraint_identity | column_constraint_not_null | column_constraint_null | column_constraint_unique | column_constraint_check | column_constraint_default | column_constraint_collate | fk_clause )
						int alt124 = 9;
						try
						{
							DebugEnterSubRule(124);
							try
							{
								DebugEnterDecision(124, decisionCanBacktrack[124]);
								try
								{
									alt124 = dfa124.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(124); }
							switch (alt124)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:397:5: column_constraint_pk
									{
										DebugLocation(397, 5);
										PushFollow(Follow._column_constraint_pk_in_column_constraint2918);
										column_constraint_pk323 = column_constraint_pk();
										PopFollow();

										stream_column_constraint_pk.Add(column_constraint_pk323.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:398:5: column_constraint_identity
									{
										DebugLocation(398, 5);
										PushFollow(Follow._column_constraint_identity_in_column_constraint2924);
										column_constraint_identity324 = column_constraint_identity();
										PopFollow();

										stream_column_constraint_identity.Add(column_constraint_identity324.Tree);

									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:399:5: column_constraint_not_null
									{
										DebugLocation(399, 5);
										PushFollow(Follow._column_constraint_not_null_in_column_constraint2930);
										column_constraint_not_null325 = column_constraint_not_null();
										PopFollow();

										stream_column_constraint_not_null.Add(column_constraint_not_null325.Tree);

									}
									break;
								case 4:
									DebugEnterAlt(4);
									// C:\\Users\\Gareth\\Desktop\\test.g:400:5: column_constraint_null
									{
										DebugLocation(400, 5);
										PushFollow(Follow._column_constraint_null_in_column_constraint2936);
										column_constraint_null326 = column_constraint_null();
										PopFollow();

										stream_column_constraint_null.Add(column_constraint_null326.Tree);

									}
									break;
								case 5:
									DebugEnterAlt(5);
									// C:\\Users\\Gareth\\Desktop\\test.g:401:5: column_constraint_unique
									{
										DebugLocation(401, 5);
										PushFollow(Follow._column_constraint_unique_in_column_constraint2942);
										column_constraint_unique327 = column_constraint_unique();
										PopFollow();

										stream_column_constraint_unique.Add(column_constraint_unique327.Tree);

									}
									break;
								case 6:
									DebugEnterAlt(6);
									// C:\\Users\\Gareth\\Desktop\\test.g:402:5: column_constraint_check
									{
										DebugLocation(402, 5);
										PushFollow(Follow._column_constraint_check_in_column_constraint2948);
										column_constraint_check328 = column_constraint_check();
										PopFollow();

										stream_column_constraint_check.Add(column_constraint_check328.Tree);

									}
									break;
								case 7:
									DebugEnterAlt(7);
									// C:\\Users\\Gareth\\Desktop\\test.g:403:5: column_constraint_default
									{
										DebugLocation(403, 5);
										PushFollow(Follow._column_constraint_default_in_column_constraint2954);
										column_constraint_default329 = column_constraint_default();
										PopFollow();

										stream_column_constraint_default.Add(column_constraint_default329.Tree);

									}
									break;
								case 8:
									DebugEnterAlt(8);
									// C:\\Users\\Gareth\\Desktop\\test.g:404:5: column_constraint_collate
									{
										DebugLocation(404, 5);
										PushFollow(Follow._column_constraint_collate_in_column_constraint2960);
										column_constraint_collate330 = column_constraint_collate();
										PopFollow();

										stream_column_constraint_collate.Add(column_constraint_collate330.Tree);

									}
									break;
								case 9:
									DebugEnterAlt(9);
									// C:\\Users\\Gareth\\Desktop\\test.g:405:5: fk_clause
									{
										DebugLocation(405, 5);
										PushFollow(Follow._fk_clause_in_column_constraint2966);
										fk_clause331 = fk_clause();
										PopFollow();

										stream_fk_clause.Add(fk_clause331.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(124); }



						{
							// AST REWRITE
							// elements: column_constraint_collate, column_constraint_not_null, column_constraint_default, column_constraint_unique, column_constraint_check, name, column_constraint_null, column_constraint_identity, fk_clause, column_constraint_pk
							// token labels: 
							// rule labels: retval, name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_name = new RewriteRuleSubtreeStream(adaptor, "rule name", name != null ? name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 406:1: -> ^( COLUMN_CONSTRAINT ( column_constraint_pk )? ( column_constraint_identity )? ( column_constraint_not_null )? ( column_constraint_null )? ( column_constraint_unique )? ( column_constraint_check )? ( column_constraint_default )? ( column_constraint_collate )? ( fk_clause )? ( $name)? )
							{
								DebugLocation(406, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:406:4: ^( COLUMN_CONSTRAINT ( column_constraint_pk )? ( column_constraint_identity )? ( column_constraint_not_null )? ( column_constraint_null )? ( column_constraint_unique )? ( column_constraint_check )? ( column_constraint_default )? ( column_constraint_collate )? ( fk_clause )? ( $name)? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(406, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMN_CONSTRAINT, "COLUMN_CONSTRAINT"), root_1);

									DebugLocation(407, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:407:3: ( column_constraint_pk )?
									if (stream_column_constraint_pk.HasNext)
									{
										DebugLocation(407, 3);
										adaptor.AddChild(root_1, stream_column_constraint_pk.NextTree());

									}
									stream_column_constraint_pk.Reset();
									DebugLocation(408, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:408:3: ( column_constraint_identity )?
									if (stream_column_constraint_identity.HasNext)
									{
										DebugLocation(408, 3);
										adaptor.AddChild(root_1, stream_column_constraint_identity.NextTree());

									}
									stream_column_constraint_identity.Reset();
									DebugLocation(409, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:409:3: ( column_constraint_not_null )?
									if (stream_column_constraint_not_null.HasNext)
									{
										DebugLocation(409, 3);
										adaptor.AddChild(root_1, stream_column_constraint_not_null.NextTree());

									}
									stream_column_constraint_not_null.Reset();
									DebugLocation(410, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:410:3: ( column_constraint_null )?
									if (stream_column_constraint_null.HasNext)
									{
										DebugLocation(410, 3);
										adaptor.AddChild(root_1, stream_column_constraint_null.NextTree());

									}
									stream_column_constraint_null.Reset();
									DebugLocation(411, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:411:3: ( column_constraint_unique )?
									if (stream_column_constraint_unique.HasNext)
									{
										DebugLocation(411, 3);
										adaptor.AddChild(root_1, stream_column_constraint_unique.NextTree());

									}
									stream_column_constraint_unique.Reset();
									DebugLocation(412, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:412:3: ( column_constraint_check )?
									if (stream_column_constraint_check.HasNext)
									{
										DebugLocation(412, 3);
										adaptor.AddChild(root_1, stream_column_constraint_check.NextTree());

									}
									stream_column_constraint_check.Reset();
									DebugLocation(413, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:413:3: ( column_constraint_default )?
									if (stream_column_constraint_default.HasNext)
									{
										DebugLocation(413, 3);
										adaptor.AddChild(root_1, stream_column_constraint_default.NextTree());

									}
									stream_column_constraint_default.Reset();
									DebugLocation(414, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:414:3: ( column_constraint_collate )?
									if (stream_column_constraint_collate.HasNext)
									{
										DebugLocation(414, 3);
										adaptor.AddChild(root_1, stream_column_constraint_collate.NextTree());

									}
									stream_column_constraint_collate.Reset();
									DebugLocation(415, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:415:3: ( fk_clause )?
									if (stream_fk_clause.HasNext)
									{
										DebugLocation(415, 3);
										adaptor.AddChild(root_1, stream_fk_clause.NextTree());

									}
									stream_fk_clause.Reset();
									DebugLocation(416, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:416:3: ( $name)?
									if (stream_name.HasNext)
									{
										DebugLocation(416, 3);
										adaptor.AddChild(root_1, stream_name.NextTree());

									}
									stream_name.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint", 59);
					LeaveRule("column_constraint", 59);
					Leave_column_constraint();
				}
				DebugLocation(416, 9);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint"); }
			return retval;

		}
		// $ANTLR end "column_constraint"

		public class column_constraint_pk_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_pk();
		partial void Leave_column_constraint_pk();

		// $ANTLR start "column_constraint_pk"
		// C:\\Users\\Gareth\\Desktop\\test.g:418:1: column_constraint_pk : PRIMARY KEY ( ASC | DESC )? ( table_conflict_clause )? ;
		[GrammarRule("column_constraint_pk")]
		private SQLiteParser.column_constraint_pk_return column_constraint_pk()
		{
			Enter_column_constraint_pk();
			EnterRule("column_constraint_pk", 60);
			TraceIn("column_constraint_pk", 60);
			SQLiteParser.column_constraint_pk_return retval = new SQLiteParser.column_constraint_pk_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken PRIMARY332 = null;
			CommonToken KEY333 = null;
			CommonToken set334 = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause335 = default(SQLiteParser.table_conflict_clause_return);

			CommonTree PRIMARY332_tree = null;
			CommonTree KEY333_tree = null;
			CommonTree set334_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_pk");
				DebugLocation(418, 72);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:418:21: ( PRIMARY KEY ( ASC | DESC )? ( table_conflict_clause )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:418:23: PRIMARY KEY ( ASC | DESC )? ( table_conflict_clause )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(418, 30);
						PRIMARY332 = (CommonToken)Match(input, PRIMARY, Follow._PRIMARY_in_column_constraint_pk3031);
						PRIMARY332_tree = (CommonTree)adaptor.Create(PRIMARY332);
						root_0 = (CommonTree)adaptor.BecomeRoot(PRIMARY332_tree, root_0);

						DebugLocation(418, 35);
						KEY333 = (CommonToken)Match(input, KEY, Follow._KEY_in_column_constraint_pk3034);
						DebugLocation(418, 37);
						// C:\\Users\\Gareth\\Desktop\\test.g:418:37: ( ASC | DESC )?
						int alt125 = 2;
						try
						{
							DebugEnterSubRule(125);
							try
							{
								DebugEnterDecision(125, decisionCanBacktrack[125]);
								try
								{
									alt125 = dfa125.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(125); }
							switch (alt125)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:
									{
										DebugLocation(418, 37);
										set334 = (CommonToken)input.LT(1);
										if ((input.LA(1) >= ASC && input.LA(1) <= DESC))
										{
											input.Consume();
											adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set334));
											state.errorRecovery = false;
										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											throw mse;
										}


									}
									break;

							}
						}
						finally { DebugExitSubRule(125); }

						DebugLocation(418, 51);
						// C:\\Users\\Gareth\\Desktop\\test.g:418:51: ( table_conflict_clause )?
						int alt126 = 2;
						try
						{
							DebugEnterSubRule(126);
							try
							{
								DebugEnterDecision(126, decisionCanBacktrack[126]);
								try
								{
									alt126 = dfa126.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(126); }
							switch (alt126)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:418:51: table_conflict_clause
									{
										DebugLocation(418, 51);
										PushFollow(Follow._table_conflict_clause_in_column_constraint_pk3046);
										table_conflict_clause335 = table_conflict_clause();
										PopFollow();

										adaptor.AddChild(root_0, table_conflict_clause335.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(126); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_pk", 60);
					LeaveRule("column_constraint_pk", 60);
					Leave_column_constraint_pk();
				}
				DebugLocation(418, 72);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_pk"); }
			return retval;

		}
		// $ANTLR end "column_constraint_pk"

		public class column_constraint_identity_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_identity();
		partial void Leave_column_constraint_identity();

		// $ANTLR start "column_constraint_identity"
		// C:\\Users\\Gareth\\Desktop\\test.g:420:1: column_constraint_identity : AUTOINCREMENT ( table_conflict_clause )? ;
		[GrammarRule("column_constraint_identity")]
		private SQLiteParser.column_constraint_identity_return column_constraint_identity()
		{
			Enter_column_constraint_identity();
			EnterRule("column_constraint_identity", 61);
			TraceIn("column_constraint_identity", 61);
			SQLiteParser.column_constraint_identity_return retval = new SQLiteParser.column_constraint_identity_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken AUTOINCREMENT336 = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause337 = default(SQLiteParser.table_conflict_clause_return);

			CommonTree AUTOINCREMENT336_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_identity");
				DebugLocation(420, 64);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:420:27: ( AUTOINCREMENT ( table_conflict_clause )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:420:29: AUTOINCREMENT ( table_conflict_clause )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(420, 29);
						AUTOINCREMENT336 = (CommonToken)Match(input, AUTOINCREMENT, Follow._AUTOINCREMENT_in_column_constraint_identity3054);
						AUTOINCREMENT336_tree = (CommonTree)adaptor.Create(AUTOINCREMENT336);
						adaptor.AddChild(root_0, AUTOINCREMENT336_tree);

						DebugLocation(420, 43);
						// C:\\Users\\Gareth\\Desktop\\test.g:420:43: ( table_conflict_clause )?
						int alt127 = 2;
						try
						{
							DebugEnterSubRule(127);
							try
							{
								DebugEnterDecision(127, decisionCanBacktrack[127]);
								try
								{
									alt127 = dfa127.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(127); }
							switch (alt127)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:420:43: table_conflict_clause
									{
										DebugLocation(420, 43);
										PushFollow(Follow._table_conflict_clause_in_column_constraint_identity3056);
										table_conflict_clause337 = table_conflict_clause();
										PopFollow();

										adaptor.AddChild(root_0, table_conflict_clause337.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(127); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_identity", 61);
					LeaveRule("column_constraint_identity", 61);
					Leave_column_constraint_identity();
				}
				DebugLocation(420, 64);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_identity"); }
			return retval;

		}
		// $ANTLR end "column_constraint_identity"

		public class column_constraint_not_null_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_not_null();
		partial void Leave_column_constraint_not_null();

		// $ANTLR start "column_constraint_not_null"
		// C:\\Users\\Gareth\\Desktop\\test.g:422:1: column_constraint_not_null : NOT NULL ( table_conflict_clause )? -> ^( NOT_NULL ( table_conflict_clause )? ) ;
		[GrammarRule("column_constraint_not_null")]
		private SQLiteParser.column_constraint_not_null_return column_constraint_not_null()
		{
			Enter_column_constraint_not_null();
			EnterRule("column_constraint_not_null", 62);
			TraceIn("column_constraint_not_null", 62);
			SQLiteParser.column_constraint_not_null_return retval = new SQLiteParser.column_constraint_not_null_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken NOT338 = null;
			CommonToken NULL339 = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause340 = default(SQLiteParser.table_conflict_clause_return);

			CommonTree NOT338_tree = null;
			CommonTree NULL339_tree = null;
			RewriteRuleITokenStream stream_NOT = new RewriteRuleITokenStream(adaptor, "token NOT");
			RewriteRuleITokenStream stream_NULL = new RewriteRuleITokenStream(adaptor, "token NULL");
			RewriteRuleSubtreeStream stream_table_conflict_clause = new RewriteRuleSubtreeStream(adaptor, "rule table_conflict_clause");
			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_not_null");
				DebugLocation(422, 97);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:422:27: ( NOT NULL ( table_conflict_clause )? -> ^( NOT_NULL ( table_conflict_clause )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:422:29: NOT NULL ( table_conflict_clause )?
					{
						DebugLocation(422, 29);
						NOT338 = (CommonToken)Match(input, NOT, Follow._NOT_in_column_constraint_not_null3064);
						stream_NOT.Add(NOT338);

						DebugLocation(422, 33);
						NULL339 = (CommonToken)Match(input, NULL, Follow._NULL_in_column_constraint_not_null3066);
						stream_NULL.Add(NULL339);

						DebugLocation(422, 38);
						// C:\\Users\\Gareth\\Desktop\\test.g:422:38: ( table_conflict_clause )?
						int alt128 = 2;
						try
						{
							DebugEnterSubRule(128);
							try
							{
								DebugEnterDecision(128, decisionCanBacktrack[128]);
								try
								{
									alt128 = dfa128.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(128); }
							switch (alt128)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:422:38: table_conflict_clause
									{
										DebugLocation(422, 38);
										PushFollow(Follow._table_conflict_clause_in_column_constraint_not_null3068);
										table_conflict_clause340 = table_conflict_clause();
										PopFollow();

										stream_table_conflict_clause.Add(table_conflict_clause340.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(128); }



						{
							// AST REWRITE
							// elements: table_conflict_clause
							// token labels: 
							// rule labels: retval
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 422:61: -> ^( NOT_NULL ( table_conflict_clause )? )
							{
								DebugLocation(422, 64);
								// C:\\Users\\Gareth\\Desktop\\test.g:422:64: ^( NOT_NULL ( table_conflict_clause )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(422, 66);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(NOT_NULL, "NOT_NULL"), root_1);

									DebugLocation(422, 75);
									// C:\\Users\\Gareth\\Desktop\\test.g:422:75: ( table_conflict_clause )?
									if (stream_table_conflict_clause.HasNext)
									{
										DebugLocation(422, 75);
										adaptor.AddChild(root_1, stream_table_conflict_clause.NextTree());

									}
									stream_table_conflict_clause.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_not_null", 62);
					LeaveRule("column_constraint_not_null", 62);
					Leave_column_constraint_not_null();
				}
				DebugLocation(422, 97);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_not_null"); }
			return retval;

		}
		// $ANTLR end "column_constraint_not_null"

		public class column_constraint_null_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_null();
		partial void Leave_column_constraint_null();

		// $ANTLR start "column_constraint_null"
		// C:\\Users\\Gareth\\Desktop\\test.g:424:1: column_constraint_null : NULL ( table_conflict_clause )? -> ^( IS_NULL ( table_conflict_clause )? ) ;
		[GrammarRule("column_constraint_null")]
		private SQLiteParser.column_constraint_null_return column_constraint_null()
		{
			Enter_column_constraint_null();
			EnterRule("column_constraint_null", 63);
			TraceIn("column_constraint_null", 63);
			SQLiteParser.column_constraint_null_return retval = new SQLiteParser.column_constraint_null_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken NULL341 = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause342 = default(SQLiteParser.table_conflict_clause_return);

			CommonTree NULL341_tree = null;
			RewriteRuleITokenStream stream_NULL = new RewriteRuleITokenStream(adaptor, "token NULL");
			RewriteRuleSubtreeStream stream_table_conflict_clause = new RewriteRuleSubtreeStream(adaptor, "rule table_conflict_clause");
			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_null");
				DebugLocation(424, 88);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:424:23: ( NULL ( table_conflict_clause )? -> ^( IS_NULL ( table_conflict_clause )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:424:25: NULL ( table_conflict_clause )?
					{
						DebugLocation(424, 25);
						NULL341 = (CommonToken)Match(input, NULL, Follow._NULL_in_column_constraint_null3085);
						stream_NULL.Add(NULL341);

						DebugLocation(424, 30);
						// C:\\Users\\Gareth\\Desktop\\test.g:424:30: ( table_conflict_clause )?
						int alt129 = 2;
						try
						{
							DebugEnterSubRule(129);
							try
							{
								DebugEnterDecision(129, decisionCanBacktrack[129]);
								try
								{
									alt129 = dfa129.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(129); }
							switch (alt129)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:424:30: table_conflict_clause
									{
										DebugLocation(424, 30);
										PushFollow(Follow._table_conflict_clause_in_column_constraint_null3087);
										table_conflict_clause342 = table_conflict_clause();
										PopFollow();

										stream_table_conflict_clause.Add(table_conflict_clause342.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(129); }



						{
							// AST REWRITE
							// elements: table_conflict_clause
							// token labels: 
							// rule labels: retval
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 424:53: -> ^( IS_NULL ( table_conflict_clause )? )
							{
								DebugLocation(424, 56);
								// C:\\Users\\Gareth\\Desktop\\test.g:424:56: ^( IS_NULL ( table_conflict_clause )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(424, 58);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(IS_NULL, "IS_NULL"), root_1);

									DebugLocation(424, 66);
									// C:\\Users\\Gareth\\Desktop\\test.g:424:66: ( table_conflict_clause )?
									if (stream_table_conflict_clause.HasNext)
									{
										DebugLocation(424, 66);
										adaptor.AddChild(root_1, stream_table_conflict_clause.NextTree());

									}
									stream_table_conflict_clause.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_null", 63);
					LeaveRule("column_constraint_null", 63);
					Leave_column_constraint_null();
				}
				DebugLocation(424, 88);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_null"); }
			return retval;

		}
		// $ANTLR end "column_constraint_null"

		public class column_constraint_unique_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_unique();
		partial void Leave_column_constraint_unique();

		// $ANTLR start "column_constraint_unique"
		// C:\\Users\\Gareth\\Desktop\\test.g:426:1: column_constraint_unique : UNIQUE ( table_conflict_clause )? ;
		[GrammarRule("column_constraint_unique")]
		private SQLiteParser.column_constraint_unique_return column_constraint_unique()
		{
			Enter_column_constraint_unique();
			EnterRule("column_constraint_unique", 64);
			TraceIn("column_constraint_unique", 64);
			SQLiteParser.column_constraint_unique_return retval = new SQLiteParser.column_constraint_unique_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken UNIQUE343 = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause344 = default(SQLiteParser.table_conflict_clause_return);

			CommonTree UNIQUE343_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_unique");
				DebugLocation(426, 56);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:426:25: ( UNIQUE ( table_conflict_clause )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:426:27: UNIQUE ( table_conflict_clause )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(426, 33);
						UNIQUE343 = (CommonToken)Match(input, UNIQUE, Follow._UNIQUE_in_column_constraint_unique3104);
						UNIQUE343_tree = (CommonTree)adaptor.Create(UNIQUE343);
						root_0 = (CommonTree)adaptor.BecomeRoot(UNIQUE343_tree, root_0);

						DebugLocation(426, 35);
						// C:\\Users\\Gareth\\Desktop\\test.g:426:35: ( table_conflict_clause )?
						int alt130 = 2;
						try
						{
							DebugEnterSubRule(130);
							try
							{
								DebugEnterDecision(130, decisionCanBacktrack[130]);
								try
								{
									alt130 = dfa130.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(130); }
							switch (alt130)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:426:35: table_conflict_clause
									{
										DebugLocation(426, 35);
										PushFollow(Follow._table_conflict_clause_in_column_constraint_unique3107);
										table_conflict_clause344 = table_conflict_clause();
										PopFollow();

										adaptor.AddChild(root_0, table_conflict_clause344.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(130); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_unique", 64);
					LeaveRule("column_constraint_unique", 64);
					Leave_column_constraint_unique();
				}
				DebugLocation(426, 56);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_unique"); }
			return retval;

		}
		// $ANTLR end "column_constraint_unique"

		public class column_constraint_check_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_check();
		partial void Leave_column_constraint_check();

		// $ANTLR start "column_constraint_check"
		// C:\\Users\\Gareth\\Desktop\\test.g:428:1: column_constraint_check : CHECK LPAREN expr RPAREN ;
		[GrammarRule("column_constraint_check")]
		private SQLiteParser.column_constraint_check_return column_constraint_check()
		{
			Enter_column_constraint_check();
			EnterRule("column_constraint_check", 65);
			TraceIn("column_constraint_check", 65);
			SQLiteParser.column_constraint_check_return retval = new SQLiteParser.column_constraint_check_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CHECK345 = null;
			CommonToken LPAREN346 = null;
			CommonToken RPAREN348 = null;
			SQLiteParser.expr_return expr347 = default(SQLiteParser.expr_return);

			CommonTree CHECK345_tree = null;
			CommonTree LPAREN346_tree = null;
			CommonTree RPAREN348_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_check");
				DebugLocation(428, 52);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:428:24: ( CHECK LPAREN expr RPAREN )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:428:26: CHECK LPAREN expr RPAREN
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(428, 31);
						CHECK345 = (CommonToken)Match(input, CHECK, Follow._CHECK_in_column_constraint_check3115);
						CHECK345_tree = (CommonTree)adaptor.Create(CHECK345);
						root_0 = (CommonTree)adaptor.BecomeRoot(CHECK345_tree, root_0);

						DebugLocation(428, 39);
						LPAREN346 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_column_constraint_check3118);
						DebugLocation(428, 41);
						PushFollow(Follow._expr_in_column_constraint_check3121);
						expr347 = expr();
						PopFollow();

						adaptor.AddChild(root_0, expr347.Tree);
						DebugLocation(428, 52);
						RPAREN348 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_column_constraint_check3123);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_check", 65);
					LeaveRule("column_constraint_check", 65);
					Leave_column_constraint_check();
				}
				DebugLocation(428, 52);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_check"); }
			return retval;

		}
		// $ANTLR end "column_constraint_check"

		public class numeric_literal_value_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_numeric_literal_value();
		partial void Leave_numeric_literal_value();

		// $ANTLR start "numeric_literal_value"
		// C:\\Users\\Gareth\\Desktop\\test.g:430:1: numeric_literal_value : ( INTEGER -> ^( INTEGER_LITERAL INTEGER ) | FLOAT -> ^( FLOAT_LITERAL FLOAT ) );
		[GrammarRule("numeric_literal_value")]
		private SQLiteParser.numeric_literal_value_return numeric_literal_value()
		{
			Enter_numeric_literal_value();
			EnterRule("numeric_literal_value", 66);
			TraceIn("numeric_literal_value", 66);
			SQLiteParser.numeric_literal_value_return retval = new SQLiteParser.numeric_literal_value_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken INTEGER349 = null;
			CommonToken FLOAT350 = null;

			CommonTree INTEGER349_tree = null;
			CommonTree FLOAT350_tree = null;
			RewriteRuleITokenStream stream_INTEGER = new RewriteRuleITokenStream(adaptor, "token INTEGER");
			RewriteRuleITokenStream stream_FLOAT = new RewriteRuleITokenStream(adaptor, "token FLOAT");

			try
			{
				DebugEnterRule(GrammarFileName, "numeric_literal_value");
				DebugLocation(430, 2);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:431:3: ( INTEGER -> ^( INTEGER_LITERAL INTEGER ) | FLOAT -> ^( FLOAT_LITERAL FLOAT ) )
					int alt131 = 2;
					try
					{
						DebugEnterDecision(131, decisionCanBacktrack[131]);
						int LA131_0 = input.LA(1);

						if ((LA131_0 == INTEGER))
						{
							alt131 = 1;
						}
						else if ((LA131_0 == FLOAT))
						{
							alt131 = 2;
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 131, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(131); }
					switch (alt131)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:431:5: INTEGER
							{
								DebugLocation(431, 5);
								INTEGER349 = (CommonToken)Match(input, INTEGER, Follow._INTEGER_in_numeric_literal_value3134);
								stream_INTEGER.Add(INTEGER349);



								{
									// AST REWRITE
									// elements: INTEGER
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 431:13: -> ^( INTEGER_LITERAL INTEGER )
									{
										DebugLocation(431, 16);
										// C:\\Users\\Gareth\\Desktop\\test.g:431:16: ^( INTEGER_LITERAL INTEGER )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(431, 18);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(INTEGER_LITERAL, "INTEGER_LITERAL"), root_1);

											DebugLocation(431, 34);
											adaptor.AddChild(root_1, stream_INTEGER.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:432:5: FLOAT
							{
								DebugLocation(432, 5);
								FLOAT350 = (CommonToken)Match(input, FLOAT, Follow._FLOAT_in_numeric_literal_value3148);
								stream_FLOAT.Add(FLOAT350);



								{
									// AST REWRITE
									// elements: FLOAT
									// token labels: 
									// rule labels: retval
									// token list labels: 
									// rule list labels: 
									// wildcard labels: 
									retval.Tree = root_0;
									RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

									root_0 = (CommonTree)adaptor.Nil();
									// 432:11: -> ^( FLOAT_LITERAL FLOAT )
									{
										DebugLocation(432, 14);
										// C:\\Users\\Gareth\\Desktop\\test.g:432:14: ^( FLOAT_LITERAL FLOAT )
										{
											CommonTree root_1 = (CommonTree)adaptor.Nil();
											DebugLocation(432, 16);
											root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(FLOAT_LITERAL, "FLOAT_LITERAL"), root_1);

											DebugLocation(432, 30);
											adaptor.AddChild(root_1, stream_FLOAT.NextNode());

											adaptor.AddChild(root_0, root_1);
										}

									}

									retval.Tree = root_0;
								}

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("numeric_literal_value", 66);
					LeaveRule("numeric_literal_value", 66);
					Leave_numeric_literal_value();
				}
				DebugLocation(433, 2);
			}
			finally { DebugExitRule(GrammarFileName, "numeric_literal_value"); }
			return retval;

		}
		// $ANTLR end "numeric_literal_value"

		public class signed_default_number_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_signed_default_number();
		partial void Leave_signed_default_number();

		// $ANTLR start "signed_default_number"
		// C:\\Users\\Gareth\\Desktop\\test.g:435:1: signed_default_number : ( PLUS | MINUS ) numeric_literal_value ;
		[GrammarRule("signed_default_number")]
		private SQLiteParser.signed_default_number_return signed_default_number()
		{
			Enter_signed_default_number();
			EnterRule("signed_default_number", 67);
			TraceIn("signed_default_number", 67);
			SQLiteParser.signed_default_number_return retval = new SQLiteParser.signed_default_number_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set351 = null;
			SQLiteParser.numeric_literal_value_return numeric_literal_value352 = default(SQLiteParser.numeric_literal_value_return);

			CommonTree set351_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "signed_default_number");
				DebugLocation(435, 60);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:435:22: ( ( PLUS | MINUS ) numeric_literal_value )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:435:24: ( PLUS | MINUS ) numeric_literal_value
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(435, 24);
						set351 = (CommonToken)input.LT(1);
						set351 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= PLUS && input.LA(1) <= MINUS))
						{
							input.Consume();
							root_0 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(set351), root_0);
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}

						DebugLocation(435, 40);
						PushFollow(Follow._numeric_literal_value_in_signed_default_number3175);
						numeric_literal_value352 = numeric_literal_value();
						PopFollow();

						adaptor.AddChild(root_0, numeric_literal_value352.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("signed_default_number", 67);
					LeaveRule("signed_default_number", 67);
					Leave_signed_default_number();
				}
				DebugLocation(435, 60);
			}
			finally { DebugExitRule(GrammarFileName, "signed_default_number"); }
			return retval;

		}
		// $ANTLR end "signed_default_number"

		public class column_constraint_default_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_default();
		partial void Leave_column_constraint_default();

		// $ANTLR start "column_constraint_default"
		// C:\\Users\\Gareth\\Desktop\\test.g:438:1: column_constraint_default : DEFAULT ( signed_default_number | literal_value | LPAREN expr RPAREN ) ;
		[GrammarRule("column_constraint_default")]
		private SQLiteParser.column_constraint_default_return column_constraint_default()
		{
			Enter_column_constraint_default();
			EnterRule("column_constraint_default", 68);
			TraceIn("column_constraint_default", 68);
			SQLiteParser.column_constraint_default_return retval = new SQLiteParser.column_constraint_default_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DEFAULT353 = null;
			CommonToken LPAREN356 = null;
			CommonToken RPAREN358 = null;
			SQLiteParser.signed_default_number_return signed_default_number354 = default(SQLiteParser.signed_default_number_return);
			SQLiteParser.literal_value_return literal_value355 = default(SQLiteParser.literal_value_return);
			SQLiteParser.expr_return expr357 = default(SQLiteParser.expr_return);

			CommonTree DEFAULT353_tree = null;
			CommonTree LPAREN356_tree = null;
			CommonTree RPAREN358_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_default");
				DebugLocation(438, 98);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:438:26: ( DEFAULT ( signed_default_number | literal_value | LPAREN expr RPAREN ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:438:28: DEFAULT ( signed_default_number | literal_value | LPAREN expr RPAREN )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(438, 35);
						DEFAULT353 = (CommonToken)Match(input, DEFAULT, Follow._DEFAULT_in_column_constraint_default3183);
						DEFAULT353_tree = (CommonTree)adaptor.Create(DEFAULT353);
						root_0 = (CommonTree)adaptor.BecomeRoot(DEFAULT353_tree, root_0);

						DebugLocation(438, 37);
						// C:\\Users\\Gareth\\Desktop\\test.g:438:37: ( signed_default_number | literal_value | LPAREN expr RPAREN )
						int alt132 = 3;
						try
						{
							DebugEnterSubRule(132);
							try
							{
								DebugEnterDecision(132, decisionCanBacktrack[132]);
								try
								{
									alt132 = dfa132.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(132); }
							switch (alt132)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:438:38: signed_default_number
									{
										DebugLocation(438, 38);
										PushFollow(Follow._signed_default_number_in_column_constraint_default3187);
										signed_default_number354 = signed_default_number();
										PopFollow();

										adaptor.AddChild(root_0, signed_default_number354.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:438:62: literal_value
									{
										DebugLocation(438, 62);
										PushFollow(Follow._literal_value_in_column_constraint_default3191);
										literal_value355 = literal_value();
										PopFollow();

										adaptor.AddChild(root_0, literal_value355.Tree);

									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:438:78: LPAREN expr RPAREN
									{
										DebugLocation(438, 84);
										LPAREN356 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_column_constraint_default3195);
										DebugLocation(438, 86);
										PushFollow(Follow._expr_in_column_constraint_default3198);
										expr357 = expr();
										PopFollow();

										adaptor.AddChild(root_0, expr357.Tree);
										DebugLocation(438, 97);
										RPAREN358 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_column_constraint_default3200);

									}
									break;

							}
						}
						finally { DebugExitSubRule(132); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_default", 68);
					LeaveRule("column_constraint_default", 68);
					Leave_column_constraint_default();
				}
				DebugLocation(438, 98);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_default"); }
			return retval;

		}
		// $ANTLR end "column_constraint_default"

		public class column_constraint_collate_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_column_constraint_collate();
		partial void Leave_column_constraint_collate();

		// $ANTLR start "column_constraint_collate"
		// C:\\Users\\Gareth\\Desktop\\test.g:440:1: column_constraint_collate : COLLATE collation_name= id ;
		[GrammarRule("column_constraint_collate")]
		private SQLiteParser.column_constraint_collate_return column_constraint_collate()
		{
			Enter_column_constraint_collate();
			EnterRule("column_constraint_collate", 69);
			TraceIn("column_constraint_collate", 69);
			SQLiteParser.column_constraint_collate_return retval = new SQLiteParser.column_constraint_collate_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken COLLATE359 = null;
			SQLiteParser.id_return collation_name = default(SQLiteParser.id_return);

			CommonTree COLLATE359_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "column_constraint_collate");
				DebugLocation(440, 53);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:440:26: ( COLLATE collation_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:440:28: COLLATE collation_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(440, 35);
						COLLATE359 = (CommonToken)Match(input, COLLATE, Follow._COLLATE_in_column_constraint_collate3209);
						COLLATE359_tree = (CommonTree)adaptor.Create(COLLATE359);
						root_0 = (CommonTree)adaptor.BecomeRoot(COLLATE359_tree, root_0);

						DebugLocation(440, 51);
						PushFollow(Follow._id_in_column_constraint_collate3214);
						collation_name = id();
						PopFollow();

						adaptor.AddChild(root_0, collation_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("column_constraint_collate", 69);
					LeaveRule("column_constraint_collate", 69);
					Leave_column_constraint_collate();
				}
				DebugLocation(440, 53);
			}
			finally { DebugExitRule(GrammarFileName, "column_constraint_collate"); }
			return retval;

		}
		// $ANTLR end "column_constraint_collate"

		public class table_constraint_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_table_constraint();
		partial void Leave_table_constraint();

		// $ANTLR start "table_constraint"
		// C:\\Users\\Gareth\\Desktop\\test.g:442:1: table_constraint : ( CONSTRAINT name= id )? ( table_constraint_pk | table_constraint_unique | table_constraint_check | table_constraint_fk ) -> ^( TABLE_CONSTRAINT ( table_constraint_pk )? ( table_constraint_unique )? ( table_constraint_check )? ( table_constraint_fk )? ( $name)? ) ;
		[GrammarRule("table_constraint")]
		private SQLiteParser.table_constraint_return table_constraint()
		{
			Enter_table_constraint();
			EnterRule("table_constraint", 70);
			TraceIn("table_constraint", 70);
			SQLiteParser.table_constraint_return retval = new SQLiteParser.table_constraint_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CONSTRAINT360 = null;
			SQLiteParser.id_return name = default(SQLiteParser.id_return);
			SQLiteParser.table_constraint_pk_return table_constraint_pk361 = default(SQLiteParser.table_constraint_pk_return);
			SQLiteParser.table_constraint_unique_return table_constraint_unique362 = default(SQLiteParser.table_constraint_unique_return);
			SQLiteParser.table_constraint_check_return table_constraint_check363 = default(SQLiteParser.table_constraint_check_return);
			SQLiteParser.table_constraint_fk_return table_constraint_fk364 = default(SQLiteParser.table_constraint_fk_return);

			CommonTree CONSTRAINT360_tree = null;
			RewriteRuleITokenStream stream_CONSTRAINT = new RewriteRuleITokenStream(adaptor, "token CONSTRAINT");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_table_constraint_pk = new RewriteRuleSubtreeStream(adaptor, "rule table_constraint_pk");
			RewriteRuleSubtreeStream stream_table_constraint_fk = new RewriteRuleSubtreeStream(adaptor, "rule table_constraint_fk");
			RewriteRuleSubtreeStream stream_table_constraint_unique = new RewriteRuleSubtreeStream(adaptor, "rule table_constraint_unique");
			RewriteRuleSubtreeStream stream_table_constraint_check = new RewriteRuleSubtreeStream(adaptor, "rule table_constraint_check");
			try
			{
				DebugEnterRule(GrammarFileName, "table_constraint");
				DebugLocation(442, 9);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:442:17: ( ( CONSTRAINT name= id )? ( table_constraint_pk | table_constraint_unique | table_constraint_check | table_constraint_fk ) -> ^( TABLE_CONSTRAINT ( table_constraint_pk )? ( table_constraint_unique )? ( table_constraint_check )? ( table_constraint_fk )? ( $name)? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:442:19: ( CONSTRAINT name= id )? ( table_constraint_pk | table_constraint_unique | table_constraint_check | table_constraint_fk )
					{
						DebugLocation(442, 19);
						// C:\\Users\\Gareth\\Desktop\\test.g:442:19: ( CONSTRAINT name= id )?
						int alt133 = 2;
						try
						{
							DebugEnterSubRule(133);
							try
							{
								DebugEnterDecision(133, decisionCanBacktrack[133]);
								int LA133_0 = input.LA(1);

								if ((LA133_0 == CONSTRAINT))
								{
									alt133 = 1;
								}
							}
							finally { DebugExitDecision(133); }
							switch (alt133)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:442:20: CONSTRAINT name= id
									{
										DebugLocation(442, 20);
										CONSTRAINT360 = (CommonToken)Match(input, CONSTRAINT, Follow._CONSTRAINT_in_table_constraint3223);
										stream_CONSTRAINT.Add(CONSTRAINT360);

										DebugLocation(442, 35);
										PushFollow(Follow._id_in_table_constraint3227);
										name = id();
										PopFollow();

										stream_id.Add(name.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(133); }

						DebugLocation(443, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:443:3: ( table_constraint_pk | table_constraint_unique | table_constraint_check | table_constraint_fk )
						int alt134 = 4;
						try
						{
							DebugEnterSubRule(134);
							try
							{
								DebugEnterDecision(134, decisionCanBacktrack[134]);
								switch (input.LA(1))
								{
									case PRIMARY:
										{
											alt134 = 1;
										}
										break;
									case UNIQUE:
										{
											alt134 = 2;
										}
										break;
									case CHECK:
										{
											alt134 = 3;
										}
										break;
									case FOREIGN:
										{
											alt134 = 4;
										}
										break;
									default:
										{
											NoViableAltException nvae = new NoViableAltException("", 134, 0, input);

											DebugRecognitionException(nvae);
											throw nvae;
										}
								}

							}
							finally { DebugExitDecision(134); }
							switch (alt134)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:443:5: table_constraint_pk
									{
										DebugLocation(443, 5);
										PushFollow(Follow._table_constraint_pk_in_table_constraint3235);
										table_constraint_pk361 = table_constraint_pk();
										PopFollow();

										stream_table_constraint_pk.Add(table_constraint_pk361.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:444:5: table_constraint_unique
									{
										DebugLocation(444, 5);
										PushFollow(Follow._table_constraint_unique_in_table_constraint3241);
										table_constraint_unique362 = table_constraint_unique();
										PopFollow();

										stream_table_constraint_unique.Add(table_constraint_unique362.Tree);

									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:445:5: table_constraint_check
									{
										DebugLocation(445, 5);
										PushFollow(Follow._table_constraint_check_in_table_constraint3247);
										table_constraint_check363 = table_constraint_check();
										PopFollow();

										stream_table_constraint_check.Add(table_constraint_check363.Tree);

									}
									break;
								case 4:
									DebugEnterAlt(4);
									// C:\\Users\\Gareth\\Desktop\\test.g:446:5: table_constraint_fk
									{
										DebugLocation(446, 5);
										PushFollow(Follow._table_constraint_fk_in_table_constraint3253);
										table_constraint_fk364 = table_constraint_fk();
										PopFollow();

										stream_table_constraint_fk.Add(table_constraint_fk364.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(134); }



						{
							// AST REWRITE
							// elements: table_constraint_fk, table_constraint_check, table_constraint_unique, name, table_constraint_pk
							// token labels: 
							// rule labels: retval, name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_name = new RewriteRuleSubtreeStream(adaptor, "rule name", name != null ? name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 447:1: -> ^( TABLE_CONSTRAINT ( table_constraint_pk )? ( table_constraint_unique )? ( table_constraint_check )? ( table_constraint_fk )? ( $name)? )
							{
								DebugLocation(447, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:447:4: ^( TABLE_CONSTRAINT ( table_constraint_pk )? ( table_constraint_unique )? ( table_constraint_check )? ( table_constraint_fk )? ( $name)? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(447, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(TABLE_CONSTRAINT, "TABLE_CONSTRAINT"), root_1);

									DebugLocation(448, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:448:3: ( table_constraint_pk )?
									if (stream_table_constraint_pk.HasNext)
									{
										DebugLocation(448, 3);
										adaptor.AddChild(root_1, stream_table_constraint_pk.NextTree());

									}
									stream_table_constraint_pk.Reset();
									DebugLocation(449, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:449:3: ( table_constraint_unique )?
									if (stream_table_constraint_unique.HasNext)
									{
										DebugLocation(449, 3);
										adaptor.AddChild(root_1, stream_table_constraint_unique.NextTree());

									}
									stream_table_constraint_unique.Reset();
									DebugLocation(450, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:450:3: ( table_constraint_check )?
									if (stream_table_constraint_check.HasNext)
									{
										DebugLocation(450, 3);
										adaptor.AddChild(root_1, stream_table_constraint_check.NextTree());

									}
									stream_table_constraint_check.Reset();
									DebugLocation(451, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:451:3: ( table_constraint_fk )?
									if (stream_table_constraint_fk.HasNext)
									{
										DebugLocation(451, 3);
										adaptor.AddChild(root_1, stream_table_constraint_fk.NextTree());

									}
									stream_table_constraint_fk.Reset();
									DebugLocation(452, 3);
									// C:\\Users\\Gareth\\Desktop\\test.g:452:3: ( $name)?
									if (stream_name.HasNext)
									{
										DebugLocation(452, 3);
										adaptor.AddChild(root_1, stream_name.NextTree());

									}
									stream_name.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("table_constraint", 70);
					LeaveRule("table_constraint", 70);
					Leave_table_constraint();
				}
				DebugLocation(452, 9);
			}
			finally { DebugExitRule(GrammarFileName, "table_constraint"); }
			return retval;

		}
		// $ANTLR end "table_constraint"

		public class table_constraint_pk_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_table_constraint_pk();
		partial void Leave_table_constraint_pk();

		// $ANTLR start "table_constraint_pk"
		// C:\\Users\\Gareth\\Desktop\\test.g:454:1: table_constraint_pk : PRIMARY KEY LPAREN indexed_columns+= id ( ASC )? ( COMMA indexed_columns+= id ( ASC )? )* RPAREN ( table_conflict_clause )? -> ^( PRIMARY ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? ) ;
		[GrammarRule("table_constraint_pk")]
		private SQLiteParser.table_constraint_pk_return table_constraint_pk()
		{
			Enter_table_constraint_pk();
			EnterRule("table_constraint_pk", 71);
			TraceIn("table_constraint_pk", 71);
			SQLiteParser.table_constraint_pk_return retval = new SQLiteParser.table_constraint_pk_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken PRIMARY365 = null;
			CommonToken KEY366 = null;
			CommonToken LPAREN367 = null;
			CommonToken ASC368 = null;
			CommonToken COMMA369 = null;
			CommonToken ASC370 = null;
			CommonToken RPAREN371 = null;
			List list_indexed_columns = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause372 = default(SQLiteParser.table_conflict_clause_return);
			SQLiteParser.id_return indexed_columns = default(SQLiteParser.id_return);
			CommonTree PRIMARY365_tree = null;
			CommonTree KEY366_tree = null;
			CommonTree LPAREN367_tree = null;
			CommonTree ASC368_tree = null;
			CommonTree COMMA369_tree = null;
			CommonTree ASC370_tree = null;
			CommonTree RPAREN371_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_PRIMARY = new RewriteRuleITokenStream(adaptor, "token PRIMARY");
			RewriteRuleITokenStream stream_ASC = new RewriteRuleITokenStream(adaptor, "token ASC");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_KEY = new RewriteRuleITokenStream(adaptor, "token KEY");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_table_conflict_clause = new RewriteRuleSubtreeStream(adaptor, "rule table_conflict_clause");
			try
			{
				DebugEnterRule(GrammarFileName, "table_constraint_pk");
				DebugLocation(454, 67);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:454:20: ( PRIMARY KEY LPAREN indexed_columns+= id ( ASC )? ( COMMA indexed_columns+= id ( ASC )? )* RPAREN ( table_conflict_clause )? -> ^( PRIMARY ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:454:22: PRIMARY KEY LPAREN indexed_columns+= id ( ASC )? ( COMMA indexed_columns+= id ( ASC )? )* RPAREN ( table_conflict_clause )?
					{
						DebugLocation(454, 22);
						PRIMARY365 = (CommonToken)Match(input, PRIMARY, Follow._PRIMARY_in_table_constraint_pk3293);
						stream_PRIMARY.Add(PRIMARY365);

						DebugLocation(454, 30);
						KEY366 = (CommonToken)Match(input, KEY, Follow._KEY_in_table_constraint_pk3295);
						stream_KEY.Add(KEY366);

						DebugLocation(455, 3);
						LPAREN367 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_table_constraint_pk3299);
						stream_LPAREN.Add(LPAREN367);

						DebugLocation(455, 25);
						PushFollow(Follow._id_in_table_constraint_pk3303);
						indexed_columns = id();
						PopFollow();

						stream_id.Add(indexed_columns.Tree);
						if (list_indexed_columns == null) list_indexed_columns = new ArrayList();
						list_indexed_columns.Add(indexed_columns.Tree);

						DebugLocation(455, 30);
						// C:\\Users\\Gareth\\Desktop\\test.g:455:30: ( ASC )?
						int alt135 = 2;
						try
						{
							DebugEnterSubRule(135);
							try
							{
								DebugEnterDecision(135, decisionCanBacktrack[135]);
								int LA135_0 = input.LA(1);

								if ((LA135_0 == ASC))
								{
									alt135 = 1;
								}
							}
							finally { DebugExitDecision(135); }
							switch (alt135)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:455:31: ASC
									{
										DebugLocation(455, 31);
										ASC368 = (CommonToken)Match(input, ASC, Follow._ASC_in_table_constraint_pk3306);
										stream_ASC.Add(ASC368);


									}
									break;

							}
						}
						finally { DebugExitSubRule(135); }

						DebugLocation(455, 37);
						// C:\\Users\\Gareth\\Desktop\\test.g:455:37: ( COMMA indexed_columns+= id ( ASC )? )*
						try
						{
							DebugEnterSubRule(137);
							while (true)
							{
								int alt137 = 2;
								try
								{
									DebugEnterDecision(137, decisionCanBacktrack[137]);
									int LA137_0 = input.LA(1);

									if ((LA137_0 == COMMA))
									{
										alt137 = 1;
									}


								}
								finally { DebugExitDecision(137); }
								switch (alt137)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:455:38: COMMA indexed_columns+= id ( ASC )?
										{
											DebugLocation(455, 38);
											COMMA369 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_table_constraint_pk3311);
											stream_COMMA.Add(COMMA369);

											DebugLocation(455, 59);
											PushFollow(Follow._id_in_table_constraint_pk3315);
											indexed_columns = id();
											PopFollow();

											stream_id.Add(indexed_columns.Tree);
											if (list_indexed_columns == null) list_indexed_columns = new ArrayList();
											list_indexed_columns.Add(indexed_columns.Tree);

											DebugLocation(455, 64);
											// C:\\Users\\Gareth\\Desktop\\test.g:455:64: ( ASC )?
											int alt136 = 2;
											try
											{
												DebugEnterSubRule(136);
												try
												{
													DebugEnterDecision(136, decisionCanBacktrack[136]);
													int LA136_0 = input.LA(1);

													if ((LA136_0 == ASC))
													{
														alt136 = 1;
													}
												}
												finally { DebugExitDecision(136); }
												switch (alt136)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:455:65: ASC
														{
															DebugLocation(455, 65);
															ASC370 = (CommonToken)Match(input, ASC, Follow._ASC_in_table_constraint_pk3318);
															stream_ASC.Add(ASC370);


														}
														break;

												}
											}
											finally { DebugExitSubRule(136); }


										}
										break;

									default:
										goto loop137;
								}
							}

						loop137:
							;

						}
						finally { DebugExitSubRule(137); }

						DebugLocation(455, 73);
						RPAREN371 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_table_constraint_pk3324);
						stream_RPAREN.Add(RPAREN371);

						DebugLocation(455, 80);
						// C:\\Users\\Gareth\\Desktop\\test.g:455:80: ( table_conflict_clause )?
						int alt138 = 2;
						try
						{
							DebugEnterSubRule(138);
							try
							{
								DebugEnterDecision(138, decisionCanBacktrack[138]);
								int LA138_0 = input.LA(1);

								if ((LA138_0 == ON))
								{
									alt138 = 1;
								}
							}
							finally { DebugExitDecision(138); }
							switch (alt138)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:455:80: table_conflict_clause
									{
										DebugLocation(455, 80);
										PushFollow(Follow._table_conflict_clause_in_table_constraint_pk3326);
										table_conflict_clause372 = table_conflict_clause();
										PopFollow();

										stream_table_conflict_clause.Add(table_conflict_clause372.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(138); }



						{
							// AST REWRITE
							// elements: table_conflict_clause, PRIMARY, indexed_columns
							// token labels: 
							// rule labels: retval
							// token list labels: 
							// rule list labels: indexed_columns
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_indexed_columns = new RewriteRuleSubtreeStream(adaptor, "token indexed_columns", list_indexed_columns);
							root_0 = (CommonTree)adaptor.Nil();
							// 456:1: -> ^( PRIMARY ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? )
							{
								DebugLocation(456, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:456:4: ^( PRIMARY ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(456, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_PRIMARY.NextNode(), root_1);

									DebugLocation(456, 14);
									// C:\\Users\\Gareth\\Desktop\\test.g:456:14: ^( COLUMNS ( $indexed_columns)+ )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(456, 16);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

										DebugLocation(456, 24);
										if (!(stream_indexed_columns.HasNext))
										{
											throw new RewriteEarlyExitException();
										}
										while (stream_indexed_columns.HasNext)
										{
											DebugLocation(456, 25);
											adaptor.AddChild(root_2, stream_indexed_columns.NextTree());

										}
										stream_indexed_columns.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(456, 45);
									// C:\\Users\\Gareth\\Desktop\\test.g:456:45: ( table_conflict_clause )?
									if (stream_table_conflict_clause.HasNext)
									{
										DebugLocation(456, 45);
										adaptor.AddChild(root_1, stream_table_conflict_clause.NextTree());

									}
									stream_table_conflict_clause.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("table_constraint_pk", 71);
					LeaveRule("table_constraint_pk", 71);
					Leave_table_constraint_pk();
				}
				DebugLocation(456, 67);
			}
			finally { DebugExitRule(GrammarFileName, "table_constraint_pk"); }
			return retval;

		}
		// $ANTLR end "table_constraint_pk"

		public class table_constraint_unique_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_table_constraint_unique();
		partial void Leave_table_constraint_unique();

		// $ANTLR start "table_constraint_unique"
		// C:\\Users\\Gareth\\Desktop\\test.g:458:1: table_constraint_unique : UNIQUE LPAREN indexed_columns+= id ( COMMA indexed_columns+= id )* RPAREN ( table_conflict_clause )? -> ^( UNIQUE ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? ) ;
		[GrammarRule("table_constraint_unique")]
		private SQLiteParser.table_constraint_unique_return table_constraint_unique()
		{
			Enter_table_constraint_unique();
			EnterRule("table_constraint_unique", 72);
			TraceIn("table_constraint_unique", 72);
			SQLiteParser.table_constraint_unique_return retval = new SQLiteParser.table_constraint_unique_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken UNIQUE373 = null;
			CommonToken LPAREN374 = null;
			CommonToken COMMA375 = null;
			CommonToken RPAREN376 = null;
			List list_indexed_columns = null;
			SQLiteParser.table_conflict_clause_return table_conflict_clause377 = default(SQLiteParser.table_conflict_clause_return);
			SQLiteParser.id_return indexed_columns = default(SQLiteParser.id_return);
			CommonTree UNIQUE373_tree = null;
			CommonTree LPAREN374_tree = null;
			CommonTree COMMA375_tree = null;
			CommonTree RPAREN376_tree = null;
			RewriteRuleITokenStream stream_UNIQUE = new RewriteRuleITokenStream(adaptor, "token UNIQUE");
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_table_conflict_clause = new RewriteRuleSubtreeStream(adaptor, "rule table_conflict_clause");
			try
			{
				DebugEnterRule(GrammarFileName, "table_constraint_unique");
				DebugLocation(458, 64);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:458:24: ( UNIQUE LPAREN indexed_columns+= id ( COMMA indexed_columns+= id )* RPAREN ( table_conflict_clause )? -> ^( UNIQUE ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:458:26: UNIQUE LPAREN indexed_columns+= id ( COMMA indexed_columns+= id )* RPAREN ( table_conflict_clause )?
					{
						DebugLocation(458, 26);
						UNIQUE373 = (CommonToken)Match(input, UNIQUE, Follow._UNIQUE_in_table_constraint_unique3353);
						stream_UNIQUE.Add(UNIQUE373);

						DebugLocation(459, 3);
						LPAREN374 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_table_constraint_unique3357);
						stream_LPAREN.Add(LPAREN374);

						DebugLocation(459, 25);
						PushFollow(Follow._id_in_table_constraint_unique3361);
						indexed_columns = id();
						PopFollow();

						stream_id.Add(indexed_columns.Tree);
						if (list_indexed_columns == null) list_indexed_columns = new ArrayList();
						list_indexed_columns.Add(indexed_columns.Tree);

						DebugLocation(459, 30);
						// C:\\Users\\Gareth\\Desktop\\test.g:459:30: ( COMMA indexed_columns+= id )*
						try
						{
							DebugEnterSubRule(139);
							while (true)
							{
								int alt139 = 2;
								try
								{
									DebugEnterDecision(139, decisionCanBacktrack[139]);
									int LA139_0 = input.LA(1);

									if ((LA139_0 == COMMA))
									{
										alt139 = 1;
									}


								}
								finally { DebugExitDecision(139); }
								switch (alt139)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:459:31: COMMA indexed_columns+= id
										{
											DebugLocation(459, 31);
											COMMA375 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_table_constraint_unique3364);
											stream_COMMA.Add(COMMA375);

											DebugLocation(459, 52);
											PushFollow(Follow._id_in_table_constraint_unique3368);
											indexed_columns = id();
											PopFollow();

											stream_id.Add(indexed_columns.Tree);
											if (list_indexed_columns == null) list_indexed_columns = new ArrayList();
											list_indexed_columns.Add(indexed_columns.Tree);


										}
										break;

									default:
										goto loop139;
								}
							}

						loop139:
							;

						}
						finally { DebugExitSubRule(139); }

						DebugLocation(459, 59);
						RPAREN376 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_table_constraint_unique3372);
						stream_RPAREN.Add(RPAREN376);

						DebugLocation(459, 66);
						// C:\\Users\\Gareth\\Desktop\\test.g:459:66: ( table_conflict_clause )?
						int alt140 = 2;
						try
						{
							DebugEnterSubRule(140);
							try
							{
								DebugEnterDecision(140, decisionCanBacktrack[140]);
								int LA140_0 = input.LA(1);

								if ((LA140_0 == ON))
								{
									alt140 = 1;
								}
							}
							finally { DebugExitDecision(140); }
							switch (alt140)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:459:66: table_conflict_clause
									{
										DebugLocation(459, 66);
										PushFollow(Follow._table_conflict_clause_in_table_constraint_unique3374);
										table_conflict_clause377 = table_conflict_clause();
										PopFollow();

										stream_table_conflict_clause.Add(table_conflict_clause377.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(140); }



						{
							// AST REWRITE
							// elements: indexed_columns, UNIQUE, table_conflict_clause
							// token labels: 
							// rule labels: retval
							// token list labels: 
							// rule list labels: indexed_columns
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_indexed_columns = new RewriteRuleSubtreeStream(adaptor, "token indexed_columns", list_indexed_columns);
							root_0 = (CommonTree)adaptor.Nil();
							// 460:1: -> ^( UNIQUE ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? )
							{
								DebugLocation(460, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:460:4: ^( UNIQUE ^( COLUMNS ( $indexed_columns)+ ) ( table_conflict_clause )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(460, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_UNIQUE.NextNode(), root_1);

									DebugLocation(460, 13);
									// C:\\Users\\Gareth\\Desktop\\test.g:460:13: ^( COLUMNS ( $indexed_columns)+ )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(460, 15);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

										DebugLocation(460, 23);
										if (!(stream_indexed_columns.HasNext))
										{
											throw new RewriteEarlyExitException();
										}
										while (stream_indexed_columns.HasNext)
										{
											DebugLocation(460, 23);
											adaptor.AddChild(root_2, stream_indexed_columns.NextTree());

										}
										stream_indexed_columns.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(460, 42);
									// C:\\Users\\Gareth\\Desktop\\test.g:460:42: ( table_conflict_clause )?
									if (stream_table_conflict_clause.HasNext)
									{
										DebugLocation(460, 42);
										adaptor.AddChild(root_1, stream_table_conflict_clause.NextTree());

									}
									stream_table_conflict_clause.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("table_constraint_unique", 72);
					LeaveRule("table_constraint_unique", 72);
					Leave_table_constraint_unique();
				}
				DebugLocation(460, 64);
			}
			finally { DebugExitRule(GrammarFileName, "table_constraint_unique"); }
			return retval;

		}
		// $ANTLR end "table_constraint_unique"

		public class table_constraint_check_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_table_constraint_check();
		partial void Leave_table_constraint_check();

		// $ANTLR start "table_constraint_check"
		// C:\\Users\\Gareth\\Desktop\\test.g:462:1: table_constraint_check : CHECK LPAREN expr RPAREN ;
		[GrammarRule("table_constraint_check")]
		private SQLiteParser.table_constraint_check_return table_constraint_check()
		{
			Enter_table_constraint_check();
			EnterRule("table_constraint_check", 73);
			TraceIn("table_constraint_check", 73);
			SQLiteParser.table_constraint_check_return retval = new SQLiteParser.table_constraint_check_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CHECK378 = null;
			CommonToken LPAREN379 = null;
			CommonToken RPAREN381 = null;
			SQLiteParser.expr_return expr380 = default(SQLiteParser.expr_return);

			CommonTree CHECK378_tree = null;
			CommonTree LPAREN379_tree = null;
			CommonTree RPAREN381_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "table_constraint_check");
				DebugLocation(462, 51);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:462:23: ( CHECK LPAREN expr RPAREN )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:462:25: CHECK LPAREN expr RPAREN
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(462, 30);
						CHECK378 = (CommonToken)Match(input, CHECK, Follow._CHECK_in_table_constraint_check3399);
						CHECK378_tree = (CommonTree)adaptor.Create(CHECK378);
						root_0 = (CommonTree)adaptor.BecomeRoot(CHECK378_tree, root_0);

						DebugLocation(462, 38);
						LPAREN379 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_table_constraint_check3402);
						DebugLocation(462, 40);
						PushFollow(Follow._expr_in_table_constraint_check3405);
						expr380 = expr();
						PopFollow();

						adaptor.AddChild(root_0, expr380.Tree);
						DebugLocation(462, 51);
						RPAREN381 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_table_constraint_check3407);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("table_constraint_check", 73);
					LeaveRule("table_constraint_check", 73);
					Leave_table_constraint_check();
				}
				DebugLocation(462, 51);
			}
			finally { DebugExitRule(GrammarFileName, "table_constraint_check"); }
			return retval;

		}
		// $ANTLR end "table_constraint_check"

		public class table_constraint_fk_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_table_constraint_fk();
		partial void Leave_table_constraint_fk();

		// $ANTLR start "table_constraint_fk"
		// C:\\Users\\Gareth\\Desktop\\test.g:464:1: table_constraint_fk : FOREIGN KEY LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN fk_clause -> ^( FOREIGN ^( COLUMNS ( $column_names)+ ) fk_clause ) ;
		[GrammarRule("table_constraint_fk")]
		private SQLiteParser.table_constraint_fk_return table_constraint_fk()
		{
			Enter_table_constraint_fk();
			EnterRule("table_constraint_fk", 74);
			TraceIn("table_constraint_fk", 74);
			SQLiteParser.table_constraint_fk_return retval = new SQLiteParser.table_constraint_fk_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken FOREIGN382 = null;
			CommonToken KEY383 = null;
			CommonToken LPAREN384 = null;
			CommonToken COMMA385 = null;
			CommonToken RPAREN386 = null;
			List list_column_names = null;
			SQLiteParser.fk_clause_return fk_clause387 = default(SQLiteParser.fk_clause_return);
			SQLiteParser.id_return column_names = default(SQLiteParser.id_return);
			CommonTree FOREIGN382_tree = null;
			CommonTree KEY383_tree = null;
			CommonTree LPAREN384_tree = null;
			CommonTree COMMA385_tree = null;
			CommonTree RPAREN386_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_FOREIGN = new RewriteRuleITokenStream(adaptor, "token FOREIGN");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_KEY = new RewriteRuleITokenStream(adaptor, "token KEY");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_fk_clause = new RewriteRuleSubtreeStream(adaptor, "rule fk_clause");
			try
			{
				DebugEnterRule(GrammarFileName, "table_constraint_fk");
				DebugLocation(464, 49);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:464:20: ( FOREIGN KEY LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN fk_clause -> ^( FOREIGN ^( COLUMNS ( $column_names)+ ) fk_clause ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:464:22: FOREIGN KEY LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN fk_clause
					{
						DebugLocation(464, 22);
						FOREIGN382 = (CommonToken)Match(input, FOREIGN, Follow._FOREIGN_in_table_constraint_fk3415);
						stream_FOREIGN.Add(FOREIGN382);

						DebugLocation(464, 30);
						KEY383 = (CommonToken)Match(input, KEY, Follow._KEY_in_table_constraint_fk3417);
						stream_KEY.Add(KEY383);

						DebugLocation(464, 34);
						LPAREN384 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_table_constraint_fk3419);
						stream_LPAREN.Add(LPAREN384);

						DebugLocation(464, 53);
						PushFollow(Follow._id_in_table_constraint_fk3423);
						column_names = id();
						PopFollow();

						stream_id.Add(column_names.Tree);
						if (list_column_names == null) list_column_names = new ArrayList();
						list_column_names.Add(column_names.Tree);

						DebugLocation(464, 58);
						// C:\\Users\\Gareth\\Desktop\\test.g:464:58: ( COMMA column_names+= id )*
						try
						{
							DebugEnterSubRule(141);
							while (true)
							{
								int alt141 = 2;
								try
								{
									DebugEnterDecision(141, decisionCanBacktrack[141]);
									int LA141_0 = input.LA(1);

									if ((LA141_0 == COMMA))
									{
										alt141 = 1;
									}


								}
								finally { DebugExitDecision(141); }
								switch (alt141)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:464:59: COMMA column_names+= id
										{
											DebugLocation(464, 59);
											COMMA385 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_table_constraint_fk3426);
											stream_COMMA.Add(COMMA385);

											DebugLocation(464, 77);
											PushFollow(Follow._id_in_table_constraint_fk3430);
											column_names = id();
											PopFollow();

											stream_id.Add(column_names.Tree);
											if (list_column_names == null) list_column_names = new ArrayList();
											list_column_names.Add(column_names.Tree);


										}
										break;

									default:
										goto loop141;
								}
							}

						loop141:
							;

						}
						finally { DebugExitSubRule(141); }

						DebugLocation(464, 84);
						RPAREN386 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_table_constraint_fk3434);
						stream_RPAREN.Add(RPAREN386);

						DebugLocation(464, 91);
						PushFollow(Follow._fk_clause_in_table_constraint_fk3436);
						fk_clause387 = fk_clause();
						PopFollow();

						stream_fk_clause.Add(fk_clause387.Tree);


						{
							// AST REWRITE
							// elements: fk_clause, FOREIGN, column_names
							// token labels: 
							// rule labels: retval
							// token list labels: 
							// rule list labels: column_names
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_column_names = new RewriteRuleSubtreeStream(adaptor, "token column_names", list_column_names);
							root_0 = (CommonTree)adaptor.Nil();
							// 465:1: -> ^( FOREIGN ^( COLUMNS ( $column_names)+ ) fk_clause )
							{
								DebugLocation(465, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:465:4: ^( FOREIGN ^( COLUMNS ( $column_names)+ ) fk_clause )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(465, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_FOREIGN.NextNode(), root_1);

									DebugLocation(465, 14);
									// C:\\Users\\Gareth\\Desktop\\test.g:465:14: ^( COLUMNS ( $column_names)+ )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(465, 16);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

										DebugLocation(465, 24);
										if (!(stream_column_names.HasNext))
										{
											throw new RewriteEarlyExitException();
										}
										while (stream_column_names.HasNext)
										{
											DebugLocation(465, 24);
											adaptor.AddChild(root_2, stream_column_names.NextTree());

										}
										stream_column_names.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(465, 40);
									adaptor.AddChild(root_1, stream_fk_clause.NextTree());

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("table_constraint_fk", 74);
					LeaveRule("table_constraint_fk", 74);
					Leave_table_constraint_fk();
				}
				DebugLocation(465, 49);
			}
			finally { DebugExitRule(GrammarFileName, "table_constraint_fk"); }
			return retval;

		}
		// $ANTLR end "table_constraint_fk"

		public class fk_clause_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_fk_clause();
		partial void Leave_fk_clause();

		// $ANTLR start "fk_clause"
		// C:\\Users\\Gareth\\Desktop\\test.g:467:1: fk_clause : REFERENCES foreign_table= id ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( fk_clause_action )* ( fk_clause_deferrable )? -> ^( REFERENCES $foreign_table ^( COLUMNS ( $column_names)+ ) ( fk_clause_action )* ( fk_clause_deferrable )? ) ;
		[GrammarRule("fk_clause")]
		private SQLiteParser.fk_clause_return fk_clause()
		{
			Enter_fk_clause();
			EnterRule("fk_clause", 75);
			TraceIn("fk_clause", 75);
			SQLiteParser.fk_clause_return retval = new SQLiteParser.fk_clause_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken REFERENCES388 = null;
			CommonToken LPAREN389 = null;
			CommonToken COMMA390 = null;
			CommonToken RPAREN391 = null;
			List list_column_names = null;
			SQLiteParser.id_return foreign_table = default(SQLiteParser.id_return);
			SQLiteParser.fk_clause_action_return fk_clause_action392 = default(SQLiteParser.fk_clause_action_return);
			SQLiteParser.fk_clause_deferrable_return fk_clause_deferrable393 = default(SQLiteParser.fk_clause_deferrable_return);
			SQLiteParser.id_return column_names = default(SQLiteParser.id_return);
			CommonTree REFERENCES388_tree = null;
			CommonTree LPAREN389_tree = null;
			CommonTree COMMA390_tree = null;
			CommonTree RPAREN391_tree = null;
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_REFERENCES = new RewriteRuleITokenStream(adaptor, "token REFERENCES");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_fk_clause_action = new RewriteRuleSubtreeStream(adaptor, "rule fk_clause_action");
			RewriteRuleSubtreeStream stream_fk_clause_deferrable = new RewriteRuleSubtreeStream(adaptor, "rule fk_clause_deferrable");
			try
			{
				DebugEnterRule(GrammarFileName, "fk_clause");
				DebugLocation(467, 97);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:467:10: ( REFERENCES foreign_table= id ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( fk_clause_action )* ( fk_clause_deferrable )? -> ^( REFERENCES $foreign_table ^( COLUMNS ( $column_names)+ ) ( fk_clause_action )* ( fk_clause_deferrable )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:467:12: REFERENCES foreign_table= id ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )? ( fk_clause_action )* ( fk_clause_deferrable )?
					{
						DebugLocation(467, 12);
						REFERENCES388 = (CommonToken)Match(input, REFERENCES, Follow._REFERENCES_in_fk_clause3459);
						stream_REFERENCES.Add(REFERENCES388);

						DebugLocation(467, 36);
						PushFollow(Follow._id_in_fk_clause3463);
						foreign_table = id();
						PopFollow();

						stream_id.Add(foreign_table.Tree);
						DebugLocation(467, 40);
						// C:\\Users\\Gareth\\Desktop\\test.g:467:40: ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )?
						int alt143 = 2;
						try
						{
							DebugEnterSubRule(143);
							try
							{
								DebugEnterDecision(143, decisionCanBacktrack[143]);
								try
								{
									alt143 = dfa143.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(143); }
							switch (alt143)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:467:41: LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN
									{
										DebugLocation(467, 41);
										LPAREN389 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_fk_clause3466);
										stream_LPAREN.Add(LPAREN389);

										DebugLocation(467, 60);
										PushFollow(Follow._id_in_fk_clause3470);
										column_names = id();
										PopFollow();

										stream_id.Add(column_names.Tree);
										if (list_column_names == null) list_column_names = new ArrayList();
										list_column_names.Add(column_names.Tree);

										DebugLocation(467, 65);
										// C:\\Users\\Gareth\\Desktop\\test.g:467:65: ( COMMA column_names+= id )*
										try
										{
											DebugEnterSubRule(142);
											while (true)
											{
												int alt142 = 2;
												try
												{
													DebugEnterDecision(142, decisionCanBacktrack[142]);
													int LA142_0 = input.LA(1);

													if ((LA142_0 == COMMA))
													{
														alt142 = 1;
													}


												}
												finally { DebugExitDecision(142); }
												switch (alt142)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:467:66: COMMA column_names+= id
														{
															DebugLocation(467, 66);
															COMMA390 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_fk_clause3473);
															stream_COMMA.Add(COMMA390);

															DebugLocation(467, 84);
															PushFollow(Follow._id_in_fk_clause3477);
															column_names = id();
															PopFollow();

															stream_id.Add(column_names.Tree);
															if (list_column_names == null) list_column_names = new ArrayList();
															list_column_names.Add(column_names.Tree);


														}
														break;

													default:
														goto loop142;
												}
											}

										loop142:
											;

										}
										finally { DebugExitSubRule(142); }

										DebugLocation(467, 91);
										RPAREN391 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_fk_clause3481);
										stream_RPAREN.Add(RPAREN391);


									}
									break;

							}
						}
						finally { DebugExitSubRule(143); }

						DebugLocation(468, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:468:3: ( fk_clause_action )*
						try
						{
							DebugEnterSubRule(144);
							while (true)
							{
								int alt144 = 2;
								try
								{
									DebugEnterDecision(144, decisionCanBacktrack[144]);
									try
									{
										alt144 = dfa144.Predict(input);
									}
									catch (NoViableAltException nvae)
									{
										DebugRecognitionException(nvae);
										throw;
									}
								}
								finally { DebugExitDecision(144); }
								switch (alt144)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:468:3: fk_clause_action
										{
											DebugLocation(468, 3);
											PushFollow(Follow._fk_clause_action_in_fk_clause3487);
											fk_clause_action392 = fk_clause_action();
											PopFollow();

											stream_fk_clause_action.Add(fk_clause_action392.Tree);

										}
										break;

									default:
										goto loop144;
								}
							}

						loop144:
							;

						}
						finally { DebugExitSubRule(144); }

						DebugLocation(468, 21);
						// C:\\Users\\Gareth\\Desktop\\test.g:468:21: ( fk_clause_deferrable )?
						int alt145 = 2;
						try
						{
							DebugEnterSubRule(145);
							try
							{
								DebugEnterDecision(145, decisionCanBacktrack[145]);
								try
								{
									alt145 = dfa145.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(145); }
							switch (alt145)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:468:21: fk_clause_deferrable
									{
										DebugLocation(468, 21);
										PushFollow(Follow._fk_clause_deferrable_in_fk_clause3490);
										fk_clause_deferrable393 = fk_clause_deferrable();
										PopFollow();

										stream_fk_clause_deferrable.Add(fk_clause_deferrable393.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(145); }



						{
							// AST REWRITE
							// elements: REFERENCES, fk_clause_deferrable, foreign_table, fk_clause_action, column_names
							// token labels: 
							// rule labels: retval, foreign_table
							// token list labels: 
							// rule list labels: column_names
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_foreign_table = new RewriteRuleSubtreeStream(adaptor, "rule foreign_table", foreign_table != null ? foreign_table.Tree : null);
							RewriteRuleSubtreeStream stream_column_names = new RewriteRuleSubtreeStream(adaptor, "token column_names", list_column_names);
							root_0 = (CommonTree)adaptor.Nil();
							// 469:1: -> ^( REFERENCES $foreign_table ^( COLUMNS ( $column_names)+ ) ( fk_clause_action )* ( fk_clause_deferrable )? )
							{
								DebugLocation(469, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:469:4: ^( REFERENCES $foreign_table ^( COLUMNS ( $column_names)+ ) ( fk_clause_action )* ( fk_clause_deferrable )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(469, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_REFERENCES.NextNode(), root_1);

									DebugLocation(469, 17);
									adaptor.AddChild(root_1, stream_foreign_table.NextTree());
									DebugLocation(469, 32);
									// C:\\Users\\Gareth\\Desktop\\test.g:469:32: ^( COLUMNS ( $column_names)+ )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(469, 34);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

										DebugLocation(469, 42);
										if (!(stream_column_names.HasNext))
										{
											throw new RewriteEarlyExitException();
										}
										while (stream_column_names.HasNext)
										{
											DebugLocation(469, 42);
											adaptor.AddChild(root_2, stream_column_names.NextTree());

										}
										stream_column_names.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(469, 58);
									// C:\\Users\\Gareth\\Desktop\\test.g:469:58: ( fk_clause_action )*
									while (stream_fk_clause_action.HasNext)
									{
										DebugLocation(469, 58);
										adaptor.AddChild(root_1, stream_fk_clause_action.NextTree());

									}
									stream_fk_clause_action.Reset();
									DebugLocation(469, 76);
									// C:\\Users\\Gareth\\Desktop\\test.g:469:76: ( fk_clause_deferrable )?
									if (stream_fk_clause_deferrable.HasNext)
									{
										DebugLocation(469, 76);
										adaptor.AddChild(root_1, stream_fk_clause_deferrable.NextTree());

									}
									stream_fk_clause_deferrable.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("fk_clause", 75);
					LeaveRule("fk_clause", 75);
					Leave_fk_clause();
				}
				DebugLocation(469, 97);
			}
			finally { DebugExitRule(GrammarFileName, "fk_clause"); }
			return retval;

		}
		// $ANTLR end "fk_clause"

		public class fk_clause_action_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_fk_clause_action();
		partial void Leave_fk_clause_action();

		// $ANTLR start "fk_clause_action"
		// C:\\Users\\Gareth\\Desktop\\test.g:471:1: fk_clause_action : ( ON ( DELETE | UPDATE | INSERT ) ( SET NULL | SET DEFAULT | CASCADE | RESTRICT ) | MATCH id );
		[GrammarRule("fk_clause_action")]
		private SQLiteParser.fk_clause_action_return fk_clause_action()
		{
			Enter_fk_clause_action();
			EnterRule("fk_clause_action", 76);
			TraceIn("fk_clause_action", 76);
			SQLiteParser.fk_clause_action_return retval = new SQLiteParser.fk_clause_action_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ON394 = null;
			CommonToken set395 = null;
			CommonToken SET396 = null;
			CommonToken NULL397 = null;
			CommonToken SET398 = null;
			CommonToken DEFAULT399 = null;
			CommonToken CASCADE400 = null;
			CommonToken RESTRICT401 = null;
			CommonToken MATCH402 = null;
			SQLiteParser.id_return id403 = default(SQLiteParser.id_return);

			CommonTree ON394_tree = null;
			CommonTree set395_tree = null;
			CommonTree SET396_tree = null;
			CommonTree NULL397_tree = null;
			CommonTree SET398_tree = null;
			CommonTree DEFAULT399_tree = null;
			CommonTree CASCADE400_tree = null;
			CommonTree RESTRICT401_tree = null;
			CommonTree MATCH402_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "fk_clause_action");
				DebugLocation(471, 13);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:472:3: ( ON ( DELETE | UPDATE | INSERT ) ( SET NULL | SET DEFAULT | CASCADE | RESTRICT ) | MATCH id )
					int alt147 = 2;
					try
					{
						DebugEnterDecision(147, decisionCanBacktrack[147]);
						int LA147_0 = input.LA(1);

						if ((LA147_0 == ON))
						{
							alt147 = 1;
						}
						else if ((LA147_0 == MATCH))
						{
							alt147 = 2;
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 147, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(147); }
					switch (alt147)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:472:5: ON ( DELETE | UPDATE | INSERT ) ( SET NULL | SET DEFAULT | CASCADE | RESTRICT )
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(472, 7);
								ON394 = (CommonToken)Match(input, ON, Follow._ON_in_fk_clause_action3524);
								ON394_tree = (CommonTree)adaptor.Create(ON394);
								root_0 = (CommonTree)adaptor.BecomeRoot(ON394_tree, root_0);

								DebugLocation(472, 9);
								set395 = (CommonToken)input.LT(1);
								if (input.LA(1) == INSERT || input.LA(1) == UPDATE || input.LA(1) == DELETE)
								{
									input.Consume();
									adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set395));
									state.errorRecovery = false;
								}
								else
								{
									MismatchedSetException mse = new MismatchedSetException(null, input);
									DebugRecognitionException(mse);
									throw mse;
								}

								DebugLocation(472, 36);
								// C:\\Users\\Gareth\\Desktop\\test.g:472:36: ( SET NULL | SET DEFAULT | CASCADE | RESTRICT )
								int alt146 = 4;
								try
								{
									DebugEnterSubRule(146);
									try
									{
										DebugEnterDecision(146, decisionCanBacktrack[146]);
										switch (input.LA(1))
										{
											case SET:
												{
													int LA146_1 = input.LA(2);

													if ((LA146_1 == NULL))
													{
														alt146 = 1;
													}
													else if ((LA146_1 == DEFAULT))
													{
														alt146 = 2;
													}
													else
													{
														NoViableAltException nvae = new NoViableAltException("", 146, 1, input);

														DebugRecognitionException(nvae);
														throw nvae;
													}
												}
												break;
											case CASCADE:
												{
													alt146 = 3;
												}
												break;
											case RESTRICT:
												{
													alt146 = 4;
												}
												break;
											default:
												{
													NoViableAltException nvae = new NoViableAltException("", 146, 0, input);

													DebugRecognitionException(nvae);
													throw nvae;
												}
										}

									}
									finally { DebugExitDecision(146); }
									switch (alt146)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:472:37: SET NULL
											{
												DebugLocation(472, 40);
												SET396 = (CommonToken)Match(input, SET, Follow._SET_in_fk_clause_action3540);
												DebugLocation(472, 42);
												NULL397 = (CommonToken)Match(input, NULL, Follow._NULL_in_fk_clause_action3543);
												NULL397_tree = (CommonTree)adaptor.Create(NULL397);
												adaptor.AddChild(root_0, NULL397_tree);


											}
											break;
										case 2:
											DebugEnterAlt(2);
											// C:\\Users\\Gareth\\Desktop\\test.g:472:49: SET DEFAULT
											{
												DebugLocation(472, 52);
												SET398 = (CommonToken)Match(input, SET, Follow._SET_in_fk_clause_action3547);
												DebugLocation(472, 54);
												DEFAULT399 = (CommonToken)Match(input, DEFAULT, Follow._DEFAULT_in_fk_clause_action3550);
												DEFAULT399_tree = (CommonTree)adaptor.Create(DEFAULT399);
												adaptor.AddChild(root_0, DEFAULT399_tree);


											}
											break;
										case 3:
											DebugEnterAlt(3);
											// C:\\Users\\Gareth\\Desktop\\test.g:472:64: CASCADE
											{
												DebugLocation(472, 64);
												CASCADE400 = (CommonToken)Match(input, CASCADE, Follow._CASCADE_in_fk_clause_action3554);
												CASCADE400_tree = (CommonTree)adaptor.Create(CASCADE400);
												adaptor.AddChild(root_0, CASCADE400_tree);


											}
											break;
										case 4:
											DebugEnterAlt(4);
											// C:\\Users\\Gareth\\Desktop\\test.g:472:74: RESTRICT
											{
												DebugLocation(472, 74);
												RESTRICT401 = (CommonToken)Match(input, RESTRICT, Follow._RESTRICT_in_fk_clause_action3558);
												RESTRICT401_tree = (CommonTree)adaptor.Create(RESTRICT401);
												adaptor.AddChild(root_0, RESTRICT401_tree);


											}
											break;

									}
								}
								finally { DebugExitSubRule(146); }


							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:473:5: MATCH id
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(473, 10);
								MATCH402 = (CommonToken)Match(input, MATCH, Follow._MATCH_in_fk_clause_action3565);
								MATCH402_tree = (CommonTree)adaptor.Create(MATCH402);
								root_0 = (CommonTree)adaptor.BecomeRoot(MATCH402_tree, root_0);

								DebugLocation(473, 12);
								PushFollow(Follow._id_in_fk_clause_action3568);
								id403 = id();
								PopFollow();

								adaptor.AddChild(root_0, id403.Tree);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("fk_clause_action", 76);
					LeaveRule("fk_clause_action", 76);
					Leave_fk_clause_action();
				}
				DebugLocation(473, 13);
			}
			finally { DebugExitRule(GrammarFileName, "fk_clause_action"); }
			return retval;

		}
		// $ANTLR end "fk_clause_action"

		public class fk_clause_deferrable_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_fk_clause_deferrable();
		partial void Leave_fk_clause_deferrable();

		// $ANTLR start "fk_clause_deferrable"
		// C:\\Users\\Gareth\\Desktop\\test.g:475:1: fk_clause_deferrable : ( NOT )? DEFERRABLE ( INITIALLY DEFERRED | INITIALLY IMMEDIATE )? ;
		[GrammarRule("fk_clause_deferrable")]
		private SQLiteParser.fk_clause_deferrable_return fk_clause_deferrable()
		{
			Enter_fk_clause_deferrable();
			EnterRule("fk_clause_deferrable", 77);
			TraceIn("fk_clause_deferrable", 77);
			SQLiteParser.fk_clause_deferrable_return retval = new SQLiteParser.fk_clause_deferrable_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken NOT404 = null;
			CommonToken DEFERRABLE405 = null;
			CommonToken INITIALLY406 = null;
			CommonToken DEFERRED407 = null;
			CommonToken INITIALLY408 = null;
			CommonToken IMMEDIATE409 = null;

			CommonTree NOT404_tree = null;
			CommonTree DEFERRABLE405_tree = null;
			CommonTree INITIALLY406_tree = null;
			CommonTree DEFERRED407_tree = null;
			CommonTree INITIALLY408_tree = null;
			CommonTree IMMEDIATE409_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "fk_clause_deferrable");
				DebugLocation(475, 86);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:475:21: ( ( NOT )? DEFERRABLE ( INITIALLY DEFERRED | INITIALLY IMMEDIATE )? )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:475:23: ( NOT )? DEFERRABLE ( INITIALLY DEFERRED | INITIALLY IMMEDIATE )?
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(475, 23);
						// C:\\Users\\Gareth\\Desktop\\test.g:475:23: ( NOT )?
						int alt148 = 2;
						try
						{
							DebugEnterSubRule(148);
							try
							{
								DebugEnterDecision(148, decisionCanBacktrack[148]);
								int LA148_0 = input.LA(1);

								if ((LA148_0 == NOT))
								{
									alt148 = 1;
								}
							}
							finally { DebugExitDecision(148); }
							switch (alt148)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:475:24: NOT
									{
										DebugLocation(475, 24);
										NOT404 = (CommonToken)Match(input, NOT, Follow._NOT_in_fk_clause_deferrable3576);
										NOT404_tree = (CommonTree)adaptor.Create(NOT404);
										adaptor.AddChild(root_0, NOT404_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(148); }

						DebugLocation(475, 40);
						DEFERRABLE405 = (CommonToken)Match(input, DEFERRABLE, Follow._DEFERRABLE_in_fk_clause_deferrable3580);
						DEFERRABLE405_tree = (CommonTree)adaptor.Create(DEFERRABLE405);
						root_0 = (CommonTree)adaptor.BecomeRoot(DEFERRABLE405_tree, root_0);

						DebugLocation(475, 42);
						// C:\\Users\\Gareth\\Desktop\\test.g:475:42: ( INITIALLY DEFERRED | INITIALLY IMMEDIATE )?
						int alt149 = 3;
						try
						{
							DebugEnterSubRule(149);
							try
							{
								DebugEnterDecision(149, decisionCanBacktrack[149]);
								try
								{
									alt149 = dfa149.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(149); }
							switch (alt149)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:475:43: INITIALLY DEFERRED
									{
										DebugLocation(475, 52);
										INITIALLY406 = (CommonToken)Match(input, INITIALLY, Follow._INITIALLY_in_fk_clause_deferrable3584);
										DebugLocation(475, 54);
										DEFERRED407 = (CommonToken)Match(input, DEFERRED, Follow._DEFERRED_in_fk_clause_deferrable3587);
										DEFERRED407_tree = (CommonTree)adaptor.Create(DEFERRED407);
										adaptor.AddChild(root_0, DEFERRED407_tree);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:475:65: INITIALLY IMMEDIATE
									{
										DebugLocation(475, 74);
										INITIALLY408 = (CommonToken)Match(input, INITIALLY, Follow._INITIALLY_in_fk_clause_deferrable3591);
										DebugLocation(475, 76);
										IMMEDIATE409 = (CommonToken)Match(input, IMMEDIATE, Follow._IMMEDIATE_in_fk_clause_deferrable3594);
										IMMEDIATE409_tree = (CommonTree)adaptor.Create(IMMEDIATE409);
										adaptor.AddChild(root_0, IMMEDIATE409_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(149); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("fk_clause_deferrable", 77);
					LeaveRule("fk_clause_deferrable", 77);
					Leave_fk_clause_deferrable();
				}
				DebugLocation(475, 86);
			}
			finally { DebugExitRule(GrammarFileName, "fk_clause_deferrable"); }
			return retval;

		}
		// $ANTLR end "fk_clause_deferrable"

		public class drop_table_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_drop_table_stmt();
		partial void Leave_drop_table_stmt();

		// $ANTLR start "drop_table_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:478:1: drop_table_stmt : DROP TABLE ( IF EXISTS )? (database_name= id DOT )? table_name= id -> ^( DROP_TABLE ^( OPTIONS ( EXISTS )? ) ^( $table_name ( $database_name)? ) ) ;
		[GrammarRule("drop_table_stmt")]
		private SQLiteParser.drop_table_stmt_return drop_table_stmt()
		{
			Enter_drop_table_stmt();
			EnterRule("drop_table_stmt", 78);
			TraceIn("drop_table_stmt", 78);
			SQLiteParser.drop_table_stmt_return retval = new SQLiteParser.drop_table_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DROP410 = null;
			CommonToken TABLE411 = null;
			CommonToken IF412 = null;
			CommonToken EXISTS413 = null;
			CommonToken DOT414 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);

			CommonTree DROP410_tree = null;
			CommonTree TABLE411_tree = null;
			CommonTree IF412_tree = null;
			CommonTree EXISTS413_tree = null;
			CommonTree DOT414_tree = null;
			RewriteRuleITokenStream stream_TABLE = new RewriteRuleITokenStream(adaptor, "token TABLE");
			RewriteRuleITokenStream stream_EXISTS = new RewriteRuleITokenStream(adaptor, "token EXISTS");
			RewriteRuleITokenStream stream_DROP = new RewriteRuleITokenStream(adaptor, "token DROP");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_IF = new RewriteRuleITokenStream(adaptor, "token IF");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			try
			{
				DebugEnterRule(GrammarFileName, "drop_table_stmt");
				DebugLocation(478, 66);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:478:16: ( DROP TABLE ( IF EXISTS )? (database_name= id DOT )? table_name= id -> ^( DROP_TABLE ^( OPTIONS ( EXISTS )? ) ^( $table_name ( $database_name)? ) ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:478:18: DROP TABLE ( IF EXISTS )? (database_name= id DOT )? table_name= id
					{
						DebugLocation(478, 18);
						DROP410 = (CommonToken)Match(input, DROP, Follow._DROP_in_drop_table_stmt3604);
						stream_DROP.Add(DROP410);

						DebugLocation(478, 23);
						TABLE411 = (CommonToken)Match(input, TABLE, Follow._TABLE_in_drop_table_stmt3606);
						stream_TABLE.Add(TABLE411);

						DebugLocation(478, 29);
						// C:\\Users\\Gareth\\Desktop\\test.g:478:29: ( IF EXISTS )?
						int alt150 = 2;
						try
						{
							DebugEnterSubRule(150);
							try
							{
								DebugEnterDecision(150, decisionCanBacktrack[150]);
								int LA150_0 = input.LA(1);

								if ((LA150_0 == IF))
								{
									int LA150_1 = input.LA(2);

									if ((LA150_1 == EXISTS))
									{
										alt150 = 1;
									}
								}
							}
							finally { DebugExitDecision(150); }
							switch (alt150)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:478:30: IF EXISTS
									{
										DebugLocation(478, 30);
										IF412 = (CommonToken)Match(input, IF, Follow._IF_in_drop_table_stmt3609);
										stream_IF.Add(IF412);

										DebugLocation(478, 33);
										EXISTS413 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_drop_table_stmt3611);
										stream_EXISTS.Add(EXISTS413);


									}
									break;

							}
						}
						finally { DebugExitSubRule(150); }

						DebugLocation(478, 42);
						// C:\\Users\\Gareth\\Desktop\\test.g:478:42: (database_name= id DOT )?
						int alt151 = 2;
						try
						{
							DebugEnterSubRule(151);
							try
							{
								DebugEnterDecision(151, decisionCanBacktrack[151]);
								int LA151_0 = input.LA(1);

								if ((LA151_0 == ID || LA151_0 == STRING))
								{
									int LA151_1 = input.LA(2);

									if ((LA151_1 == DOT))
									{
										alt151 = 1;
									}
								}
								else if (((LA151_0 >= EXPLAIN && LA151_0 <= PLAN) || (LA151_0 >= INDEXED && LA151_0 <= BY) || (LA151_0 >= OR && LA151_0 <= ESCAPE) || (LA151_0 >= IS && LA151_0 <= BETWEEN) || LA151_0 == COLLATE || (LA151_0 >= DISTINCT && LA151_0 <= THEN) || (LA151_0 >= CURRENT_TIME && LA151_0 <= CURRENT_TIMESTAMP) || (LA151_0 >= RAISE && LA151_0 <= ROW)))
								{
									int LA151_2 = input.LA(2);

									if ((LA151_2 == DOT))
									{
										alt151 = 1;
									}
								}
							}
							finally { DebugExitDecision(151); }
							switch (alt151)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:478:43: database_name= id DOT
									{
										DebugLocation(478, 56);
										PushFollow(Follow._id_in_drop_table_stmt3618);
										database_name = id();
										PopFollow();

										stream_id.Add(database_name.Tree);
										DebugLocation(478, 60);
										DOT414 = (CommonToken)Match(input, DOT, Follow._DOT_in_drop_table_stmt3620);
										stream_DOT.Add(DOT414);


									}
									break;

							}
						}
						finally { DebugExitSubRule(151); }

						DebugLocation(478, 76);
						PushFollow(Follow._id_in_drop_table_stmt3626);
						table_name = id();
						PopFollow();

						stream_id.Add(table_name.Tree);


						{
							// AST REWRITE
							// elements: EXISTS, table_name, database_name
							// token labels: 
							// rule labels: database_name, retval, table_name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 479:1: -> ^( DROP_TABLE ^( OPTIONS ( EXISTS )? ) ^( $table_name ( $database_name)? ) )
							{
								DebugLocation(479, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:479:4: ^( DROP_TABLE ^( OPTIONS ( EXISTS )? ) ^( $table_name ( $database_name)? ) )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(479, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(DROP_TABLE, "DROP_TABLE"), root_1);

									DebugLocation(479, 17);
									// C:\\Users\\Gareth\\Desktop\\test.g:479:17: ^( OPTIONS ( EXISTS )? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(479, 19);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(OPTIONS, "OPTIONS"), root_2);

										DebugLocation(479, 27);
										// C:\\Users\\Gareth\\Desktop\\test.g:479:27: ( EXISTS )?
										if (stream_EXISTS.HasNext)
										{
											DebugLocation(479, 27);
											adaptor.AddChild(root_2, stream_EXISTS.NextNode());

										}
										stream_EXISTS.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(479, 36);
									// C:\\Users\\Gareth\\Desktop\\test.g:479:36: ^( $table_name ( $database_name)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(479, 38);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_table_name.NextNode(), root_2);

										DebugLocation(479, 50);
										// C:\\Users\\Gareth\\Desktop\\test.g:479:50: ( $database_name)?
										if (stream_database_name.HasNext)
										{
											DebugLocation(479, 50);
											adaptor.AddChild(root_2, stream_database_name.NextTree());

										}
										stream_database_name.Reset();

										adaptor.AddChild(root_1, root_2);
									}

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("drop_table_stmt", 78);
					LeaveRule("drop_table_stmt", 78);
					Leave_drop_table_stmt();
				}
				DebugLocation(479, 66);
			}
			finally { DebugExitRule(GrammarFileName, "drop_table_stmt"); }
			return retval;

		}
		// $ANTLR end "drop_table_stmt"

		public class alter_table_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_alter_table_stmt();
		partial void Leave_alter_table_stmt();

		// $ANTLR start "alter_table_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:482:1: alter_table_stmt : ALTER TABLE (database_name= id DOT )? table_name= id ( RENAME TO new_table_name= id | ADD ( COLUMN )? column_def ) ;
		[GrammarRule("alter_table_stmt")]
		private SQLiteParser.alter_table_stmt_return alter_table_stmt()
		{
			Enter_alter_table_stmt();
			EnterRule("alter_table_stmt", 79);
			TraceIn("alter_table_stmt", 79);
			SQLiteParser.alter_table_stmt_return retval = new SQLiteParser.alter_table_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken ALTER415 = null;
			CommonToken TABLE416 = null;
			CommonToken DOT417 = null;
			CommonToken RENAME418 = null;
			CommonToken TO419 = null;
			CommonToken ADD420 = null;
			CommonToken COLUMN421 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return new_table_name = default(SQLiteParser.id_return);
			SQLiteParser.column_def_return column_def422 = default(SQLiteParser.column_def_return);

			CommonTree ALTER415_tree = null;
			CommonTree TABLE416_tree = null;
			CommonTree DOT417_tree = null;
			CommonTree RENAME418_tree = null;
			CommonTree TO419_tree = null;
			CommonTree ADD420_tree = null;
			CommonTree COLUMN421_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "alter_table_stmt");
				DebugLocation(482, 124);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:482:17: ( ALTER TABLE (database_name= id DOT )? table_name= id ( RENAME TO new_table_name= id | ADD ( COLUMN )? column_def ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:482:19: ALTER TABLE (database_name= id DOT )? table_name= id ( RENAME TO new_table_name= id | ADD ( COLUMN )? column_def )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(482, 19);
						ALTER415 = (CommonToken)Match(input, ALTER, Follow._ALTER_in_alter_table_stmt3656);
						ALTER415_tree = (CommonTree)adaptor.Create(ALTER415);
						adaptor.AddChild(root_0, ALTER415_tree);

						DebugLocation(482, 25);
						TABLE416 = (CommonToken)Match(input, TABLE, Follow._TABLE_in_alter_table_stmt3658);
						TABLE416_tree = (CommonTree)adaptor.Create(TABLE416);
						adaptor.AddChild(root_0, TABLE416_tree);

						DebugLocation(482, 31);
						// C:\\Users\\Gareth\\Desktop\\test.g:482:31: (database_name= id DOT )?
						int alt152 = 2;
						try
						{
							DebugEnterSubRule(152);
							try
							{
								DebugEnterDecision(152, decisionCanBacktrack[152]);
								int LA152_0 = input.LA(1);

								if ((LA152_0 == ID || LA152_0 == STRING))
								{
									int LA152_1 = input.LA(2);

									if ((LA152_1 == DOT))
									{
										alt152 = 1;
									}
								}
								else if (((LA152_0 >= EXPLAIN && LA152_0 <= PLAN) || (LA152_0 >= INDEXED && LA152_0 <= BY) || (LA152_0 >= OR && LA152_0 <= ESCAPE) || (LA152_0 >= IS && LA152_0 <= BETWEEN) || LA152_0 == COLLATE || (LA152_0 >= DISTINCT && LA152_0 <= THEN) || (LA152_0 >= CURRENT_TIME && LA152_0 <= CURRENT_TIMESTAMP) || (LA152_0 >= RAISE && LA152_0 <= ROW)))
								{
									int LA152_2 = input.LA(2);

									if ((LA152_2 == DOT))
									{
										alt152 = 1;
									}
								}
							}
							finally { DebugExitDecision(152); }
							switch (alt152)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:482:32: database_name= id DOT
									{
										DebugLocation(482, 45);
										PushFollow(Follow._id_in_alter_table_stmt3663);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(482, 49);
										DOT417 = (CommonToken)Match(input, DOT, Follow._DOT_in_alter_table_stmt3665);
										DOT417_tree = (CommonTree)adaptor.Create(DOT417);
										adaptor.AddChild(root_0, DOT417_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(152); }

						DebugLocation(482, 65);
						PushFollow(Follow._id_in_alter_table_stmt3671);
						table_name = id();
						PopFollow();

						adaptor.AddChild(root_0, table_name.Tree);
						DebugLocation(482, 69);
						// C:\\Users\\Gareth\\Desktop\\test.g:482:69: ( RENAME TO new_table_name= id | ADD ( COLUMN )? column_def )
						int alt154 = 2;
						try
						{
							DebugEnterSubRule(154);
							try
							{
								DebugEnterDecision(154, decisionCanBacktrack[154]);
								int LA154_0 = input.LA(1);

								if ((LA154_0 == RENAME))
								{
									alt154 = 1;
								}
								else if ((LA154_0 == ADD))
								{
									alt154 = 2;
								}
								else
								{
									NoViableAltException nvae = new NoViableAltException("", 154, 0, input);

									DebugRecognitionException(nvae);
									throw nvae;
								}
							}
							finally { DebugExitDecision(154); }
							switch (alt154)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:482:70: RENAME TO new_table_name= id
									{
										DebugLocation(482, 70);
										RENAME418 = (CommonToken)Match(input, RENAME, Follow._RENAME_in_alter_table_stmt3674);
										RENAME418_tree = (CommonTree)adaptor.Create(RENAME418);
										adaptor.AddChild(root_0, RENAME418_tree);

										DebugLocation(482, 77);
										TO419 = (CommonToken)Match(input, TO, Follow._TO_in_alter_table_stmt3676);
										TO419_tree = (CommonTree)adaptor.Create(TO419);
										adaptor.AddChild(root_0, TO419_tree);

										DebugLocation(482, 94);
										PushFollow(Follow._id_in_alter_table_stmt3680);
										new_table_name = id();
										PopFollow();

										adaptor.AddChild(root_0, new_table_name.Tree);

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:482:100: ADD ( COLUMN )? column_def
									{
										DebugLocation(482, 100);
										ADD420 = (CommonToken)Match(input, ADD, Follow._ADD_in_alter_table_stmt3684);
										ADD420_tree = (CommonTree)adaptor.Create(ADD420);
										adaptor.AddChild(root_0, ADD420_tree);

										DebugLocation(482, 104);
										// C:\\Users\\Gareth\\Desktop\\test.g:482:104: ( COLUMN )?
										int alt153 = 2;
										try
										{
											DebugEnterSubRule(153);
											try
											{
												DebugEnterDecision(153, decisionCanBacktrack[153]);
												int LA153_0 = input.LA(1);

												if ((LA153_0 == COLUMN))
												{
													alt153 = 1;
												}
											}
											finally { DebugExitDecision(153); }
											switch (alt153)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:482:105: COLUMN
													{
														DebugLocation(482, 105);
														COLUMN421 = (CommonToken)Match(input, COLUMN, Follow._COLUMN_in_alter_table_stmt3687);
														COLUMN421_tree = (CommonTree)adaptor.Create(COLUMN421);
														adaptor.AddChild(root_0, COLUMN421_tree);


													}
													break;

											}
										}
										finally { DebugExitSubRule(153); }

										DebugLocation(482, 114);
										PushFollow(Follow._column_def_in_alter_table_stmt3691);
										column_def422 = column_def();
										PopFollow();

										adaptor.AddChild(root_0, column_def422.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(154); }


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("alter_table_stmt", 79);
					LeaveRule("alter_table_stmt", 79);
					Leave_alter_table_stmt();
				}
				DebugLocation(482, 124);
			}
			finally { DebugExitRule(GrammarFileName, "alter_table_stmt"); }
			return retval;

		}
		// $ANTLR end "alter_table_stmt"

		public class create_view_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_create_view_stmt();
		partial void Leave_create_view_stmt();

		// $ANTLR start "create_view_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:485:1: create_view_stmt : CREATE ( TEMPORARY )? VIEW ( IF NOT EXISTS )? (database_name= id DOT )? view_name= id AS select_stmt ;
		[GrammarRule("create_view_stmt")]
		private SQLiteParser.create_view_stmt_return create_view_stmt()
		{
			Enter_create_view_stmt();
			EnterRule("create_view_stmt", 80);
			TraceIn("create_view_stmt", 80);
			SQLiteParser.create_view_stmt_return retval = new SQLiteParser.create_view_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CREATE423 = null;
			CommonToken TEMPORARY424 = null;
			CommonToken VIEW425 = null;
			CommonToken IF426 = null;
			CommonToken NOT427 = null;
			CommonToken EXISTS428 = null;
			CommonToken DOT429 = null;
			CommonToken AS430 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return view_name = default(SQLiteParser.id_return);
			SQLiteParser.select_stmt_return select_stmt431 = default(SQLiteParser.select_stmt_return);

			CommonTree CREATE423_tree = null;
			CommonTree TEMPORARY424_tree = null;
			CommonTree VIEW425_tree = null;
			CommonTree IF426_tree = null;
			CommonTree NOT427_tree = null;
			CommonTree EXISTS428_tree = null;
			CommonTree DOT429_tree = null;
			CommonTree AS430_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "create_view_stmt");
				DebugLocation(485, 109);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:485:17: ( CREATE ( TEMPORARY )? VIEW ( IF NOT EXISTS )? (database_name= id DOT )? view_name= id AS select_stmt )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:485:19: CREATE ( TEMPORARY )? VIEW ( IF NOT EXISTS )? (database_name= id DOT )? view_name= id AS select_stmt
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(485, 19);
						CREATE423 = (CommonToken)Match(input, CREATE, Follow._CREATE_in_create_view_stmt3700);
						CREATE423_tree = (CommonTree)adaptor.Create(CREATE423);
						adaptor.AddChild(root_0, CREATE423_tree);

						DebugLocation(485, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:485:26: ( TEMPORARY )?
						int alt155 = 2;
						try
						{
							DebugEnterSubRule(155);
							try
							{
								DebugEnterDecision(155, decisionCanBacktrack[155]);
								int LA155_0 = input.LA(1);

								if ((LA155_0 == TEMPORARY))
								{
									alt155 = 1;
								}
							}
							finally { DebugExitDecision(155); }
							switch (alt155)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:485:26: TEMPORARY
									{
										DebugLocation(485, 26);
										TEMPORARY424 = (CommonToken)Match(input, TEMPORARY, Follow._TEMPORARY_in_create_view_stmt3702);
										TEMPORARY424_tree = (CommonTree)adaptor.Create(TEMPORARY424);
										adaptor.AddChild(root_0, TEMPORARY424_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(155); }

						DebugLocation(485, 37);
						VIEW425 = (CommonToken)Match(input, VIEW, Follow._VIEW_in_create_view_stmt3705);
						VIEW425_tree = (CommonTree)adaptor.Create(VIEW425);
						adaptor.AddChild(root_0, VIEW425_tree);

						DebugLocation(485, 42);
						// C:\\Users\\Gareth\\Desktop\\test.g:485:42: ( IF NOT EXISTS )?
						int alt156 = 2;
						try
						{
							DebugEnterSubRule(156);
							try
							{
								DebugEnterDecision(156, decisionCanBacktrack[156]);
								int LA156_0 = input.LA(1);

								if ((LA156_0 == IF))
								{
									int LA156_1 = input.LA(2);

									if ((LA156_1 == NOT))
									{
										alt156 = 1;
									}
								}
							}
							finally { DebugExitDecision(156); }
							switch (alt156)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:485:43: IF NOT EXISTS
									{
										DebugLocation(485, 43);
										IF426 = (CommonToken)Match(input, IF, Follow._IF_in_create_view_stmt3708);
										IF426_tree = (CommonTree)adaptor.Create(IF426);
										adaptor.AddChild(root_0, IF426_tree);

										DebugLocation(485, 46);
										NOT427 = (CommonToken)Match(input, NOT, Follow._NOT_in_create_view_stmt3710);
										NOT427_tree = (CommonTree)adaptor.Create(NOT427);
										adaptor.AddChild(root_0, NOT427_tree);

										DebugLocation(485, 50);
										EXISTS428 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_create_view_stmt3712);
										EXISTS428_tree = (CommonTree)adaptor.Create(EXISTS428);
										adaptor.AddChild(root_0, EXISTS428_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(156); }

						DebugLocation(485, 59);
						// C:\\Users\\Gareth\\Desktop\\test.g:485:59: (database_name= id DOT )?
						int alt157 = 2;
						try
						{
							DebugEnterSubRule(157);
							try
							{
								DebugEnterDecision(157, decisionCanBacktrack[157]);
								int LA157_0 = input.LA(1);

								if ((LA157_0 == ID || LA157_0 == STRING))
								{
									int LA157_1 = input.LA(2);

									if ((LA157_1 == DOT))
									{
										alt157 = 1;
									}
								}
								else if (((LA157_0 >= EXPLAIN && LA157_0 <= PLAN) || (LA157_0 >= INDEXED && LA157_0 <= BY) || (LA157_0 >= OR && LA157_0 <= ESCAPE) || (LA157_0 >= IS && LA157_0 <= BETWEEN) || LA157_0 == COLLATE || (LA157_0 >= DISTINCT && LA157_0 <= THEN) || (LA157_0 >= CURRENT_TIME && LA157_0 <= CURRENT_TIMESTAMP) || (LA157_0 >= RAISE && LA157_0 <= ROW)))
								{
									int LA157_2 = input.LA(2);

									if ((LA157_2 == DOT))
									{
										alt157 = 1;
									}
								}
							}
							finally { DebugExitDecision(157); }
							switch (alt157)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:485:60: database_name= id DOT
									{
										DebugLocation(485, 73);
										PushFollow(Follow._id_in_create_view_stmt3719);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(485, 77);
										DOT429 = (CommonToken)Match(input, DOT, Follow._DOT_in_create_view_stmt3721);
										DOT429_tree = (CommonTree)adaptor.Create(DOT429);
										adaptor.AddChild(root_0, DOT429_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(157); }

						DebugLocation(485, 92);
						PushFollow(Follow._id_in_create_view_stmt3727);
						view_name = id();
						PopFollow();

						adaptor.AddChild(root_0, view_name.Tree);
						DebugLocation(485, 96);
						AS430 = (CommonToken)Match(input, AS, Follow._AS_in_create_view_stmt3729);
						AS430_tree = (CommonTree)adaptor.Create(AS430);
						adaptor.AddChild(root_0, AS430_tree);

						DebugLocation(485, 99);
						PushFollow(Follow._select_stmt_in_create_view_stmt3731);
						select_stmt431 = select_stmt();
						PopFollow();

						adaptor.AddChild(root_0, select_stmt431.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("create_view_stmt", 80);
					LeaveRule("create_view_stmt", 80);
					Leave_create_view_stmt();
				}
				DebugLocation(485, 109);
			}
			finally { DebugExitRule(GrammarFileName, "create_view_stmt"); }
			return retval;

		}
		// $ANTLR end "create_view_stmt"

		public class drop_view_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_drop_view_stmt();
		partial void Leave_drop_view_stmt();

		// $ANTLR start "drop_view_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:488:1: drop_view_stmt : DROP VIEW ( IF EXISTS )? (database_name= id DOT )? view_name= id ;
		[GrammarRule("drop_view_stmt")]
		private SQLiteParser.drop_view_stmt_return drop_view_stmt()
		{
			Enter_drop_view_stmt();
			EnterRule("drop_view_stmt", 81);
			TraceIn("drop_view_stmt", 81);
			SQLiteParser.drop_view_stmt_return retval = new SQLiteParser.drop_view_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DROP432 = null;
			CommonToken VIEW433 = null;
			CommonToken IF434 = null;
			CommonToken EXISTS435 = null;
			CommonToken DOT436 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return view_name = default(SQLiteParser.id_return);

			CommonTree DROP432_tree = null;
			CommonTree VIEW433_tree = null;
			CommonTree IF434_tree = null;
			CommonTree EXISTS435_tree = null;
			CommonTree DOT436_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "drop_view_stmt");
				DebugLocation(488, 75);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:488:15: ( DROP VIEW ( IF EXISTS )? (database_name= id DOT )? view_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:488:17: DROP VIEW ( IF EXISTS )? (database_name= id DOT )? view_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(488, 17);
						DROP432 = (CommonToken)Match(input, DROP, Follow._DROP_in_drop_view_stmt3739);
						DROP432_tree = (CommonTree)adaptor.Create(DROP432);
						adaptor.AddChild(root_0, DROP432_tree);

						DebugLocation(488, 22);
						VIEW433 = (CommonToken)Match(input, VIEW, Follow._VIEW_in_drop_view_stmt3741);
						VIEW433_tree = (CommonTree)adaptor.Create(VIEW433);
						adaptor.AddChild(root_0, VIEW433_tree);

						DebugLocation(488, 27);
						// C:\\Users\\Gareth\\Desktop\\test.g:488:27: ( IF EXISTS )?
						int alt158 = 2;
						try
						{
							DebugEnterSubRule(158);
							try
							{
								DebugEnterDecision(158, decisionCanBacktrack[158]);
								int LA158_0 = input.LA(1);

								if ((LA158_0 == IF))
								{
									int LA158_1 = input.LA(2);

									if ((LA158_1 == EXISTS))
									{
										alt158 = 1;
									}
								}
							}
							finally { DebugExitDecision(158); }
							switch (alt158)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:488:28: IF EXISTS
									{
										DebugLocation(488, 28);
										IF434 = (CommonToken)Match(input, IF, Follow._IF_in_drop_view_stmt3744);
										IF434_tree = (CommonTree)adaptor.Create(IF434);
										adaptor.AddChild(root_0, IF434_tree);

										DebugLocation(488, 31);
										EXISTS435 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_drop_view_stmt3746);
										EXISTS435_tree = (CommonTree)adaptor.Create(EXISTS435);
										adaptor.AddChild(root_0, EXISTS435_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(158); }

						DebugLocation(488, 40);
						// C:\\Users\\Gareth\\Desktop\\test.g:488:40: (database_name= id DOT )?
						int alt159 = 2;
						try
						{
							DebugEnterSubRule(159);
							try
							{
								DebugEnterDecision(159, decisionCanBacktrack[159]);
								int LA159_0 = input.LA(1);

								if ((LA159_0 == ID || LA159_0 == STRING))
								{
									int LA159_1 = input.LA(2);

									if ((LA159_1 == DOT))
									{
										alt159 = 1;
									}
								}
								else if (((LA159_0 >= EXPLAIN && LA159_0 <= PLAN) || (LA159_0 >= INDEXED && LA159_0 <= BY) || (LA159_0 >= OR && LA159_0 <= ESCAPE) || (LA159_0 >= IS && LA159_0 <= BETWEEN) || LA159_0 == COLLATE || (LA159_0 >= DISTINCT && LA159_0 <= THEN) || (LA159_0 >= CURRENT_TIME && LA159_0 <= CURRENT_TIMESTAMP) || (LA159_0 >= RAISE && LA159_0 <= ROW)))
								{
									int LA159_2 = input.LA(2);

									if ((LA159_2 == DOT))
									{
										alt159 = 1;
									}
								}
							}
							finally { DebugExitDecision(159); }
							switch (alt159)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:488:41: database_name= id DOT
									{
										DebugLocation(488, 54);
										PushFollow(Follow._id_in_drop_view_stmt3753);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(488, 58);
										DOT436 = (CommonToken)Match(input, DOT, Follow._DOT_in_drop_view_stmt3755);
										DOT436_tree = (CommonTree)adaptor.Create(DOT436);
										adaptor.AddChild(root_0, DOT436_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(159); }

						DebugLocation(488, 73);
						PushFollow(Follow._id_in_drop_view_stmt3761);
						view_name = id();
						PopFollow();

						adaptor.AddChild(root_0, view_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("drop_view_stmt", 81);
					LeaveRule("drop_view_stmt", 81);
					Leave_drop_view_stmt();
				}
				DebugLocation(488, 75);
			}
			finally { DebugExitRule(GrammarFileName, "drop_view_stmt"); }
			return retval;

		}
		// $ANTLR end "drop_view_stmt"

		public class create_index_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_create_index_stmt();
		partial void Leave_create_index_stmt();

		// $ANTLR start "create_index_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:491:1: create_index_stmt : CREATE ( UNIQUE )? INDEX ( IF NOT EXISTS )? (database_name= id DOT )? index_name= id ON table_name= id LPAREN columns+= indexed_column ( COMMA columns+= indexed_column )* RPAREN -> ^( CREATE_INDEX ^( OPTIONS ( UNIQUE )? ( EXISTS )? ) ^( $index_name ( $database_name)? ) $table_name ( ^( COLUMNS ( $columns)+ ) )? ) ;
		[GrammarRule("create_index_stmt")]
		private SQLiteParser.create_index_stmt_return create_index_stmt()
		{
			Enter_create_index_stmt();
			EnterRule("create_index_stmt", 82);
			TraceIn("create_index_stmt", 82);
			SQLiteParser.create_index_stmt_return retval = new SQLiteParser.create_index_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CREATE437 = null;
			CommonToken UNIQUE438 = null;
			CommonToken INDEX439 = null;
			CommonToken IF440 = null;
			CommonToken NOT441 = null;
			CommonToken EXISTS442 = null;
			CommonToken DOT443 = null;
			CommonToken ON444 = null;
			CommonToken LPAREN445 = null;
			CommonToken COMMA446 = null;
			CommonToken RPAREN447 = null;
			List list_columns = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return index_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.indexed_column_return columns = default(SQLiteParser.indexed_column_return);
			CommonTree CREATE437_tree = null;
			CommonTree UNIQUE438_tree = null;
			CommonTree INDEX439_tree = null;
			CommonTree IF440_tree = null;
			CommonTree NOT441_tree = null;
			CommonTree EXISTS442_tree = null;
			CommonTree DOT443_tree = null;
			CommonTree ON444_tree = null;
			CommonTree LPAREN445_tree = null;
			CommonTree COMMA446_tree = null;
			CommonTree RPAREN447_tree = null;
			RewriteRuleITokenStream stream_INDEX = new RewriteRuleITokenStream(adaptor, "token INDEX");
			RewriteRuleITokenStream stream_ON = new RewriteRuleITokenStream(adaptor, "token ON");
			RewriteRuleITokenStream stream_UNIQUE = new RewriteRuleITokenStream(adaptor, "token UNIQUE");
			RewriteRuleITokenStream stream_RPAREN = new RewriteRuleITokenStream(adaptor, "token RPAREN");
			RewriteRuleITokenStream stream_CREATE = new RewriteRuleITokenStream(adaptor, "token CREATE");
			RewriteRuleITokenStream stream_NOT = new RewriteRuleITokenStream(adaptor, "token NOT");
			RewriteRuleITokenStream stream_EXISTS = new RewriteRuleITokenStream(adaptor, "token EXISTS");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_COMMA = new RewriteRuleITokenStream(adaptor, "token COMMA");
			RewriteRuleITokenStream stream_LPAREN = new RewriteRuleITokenStream(adaptor, "token LPAREN");
			RewriteRuleITokenStream stream_IF = new RewriteRuleITokenStream(adaptor, "token IF");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			RewriteRuleSubtreeStream stream_indexed_column = new RewriteRuleSubtreeStream(adaptor, "rule indexed_column");
			try
			{
				DebugEnterRule(GrammarFileName, "create_index_stmt");
				DebugLocation(491, 110);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:491:18: ( CREATE ( UNIQUE )? INDEX ( IF NOT EXISTS )? (database_name= id DOT )? index_name= id ON table_name= id LPAREN columns+= indexed_column ( COMMA columns+= indexed_column )* RPAREN -> ^( CREATE_INDEX ^( OPTIONS ( UNIQUE )? ( EXISTS )? ) ^( $index_name ( $database_name)? ) $table_name ( ^( COLUMNS ( $columns)+ ) )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:491:20: CREATE ( UNIQUE )? INDEX ( IF NOT EXISTS )? (database_name= id DOT )? index_name= id ON table_name= id LPAREN columns+= indexed_column ( COMMA columns+= indexed_column )* RPAREN
					{
						DebugLocation(491, 20);
						CREATE437 = (CommonToken)Match(input, CREATE, Follow._CREATE_in_create_index_stmt3769);
						stream_CREATE.Add(CREATE437);

						DebugLocation(491, 27);
						// C:\\Users\\Gareth\\Desktop\\test.g:491:27: ( UNIQUE )?
						int alt160 = 2;
						try
						{
							DebugEnterSubRule(160);
							try
							{
								DebugEnterDecision(160, decisionCanBacktrack[160]);
								int LA160_0 = input.LA(1);

								if ((LA160_0 == UNIQUE))
								{
									alt160 = 1;
								}
							}
							finally { DebugExitDecision(160); }
							switch (alt160)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:491:28: UNIQUE
									{
										DebugLocation(491, 28);
										UNIQUE438 = (CommonToken)Match(input, UNIQUE, Follow._UNIQUE_in_create_index_stmt3772);
										stream_UNIQUE.Add(UNIQUE438);


									}
									break;

							}
						}
						finally { DebugExitSubRule(160); }

						DebugLocation(491, 37);
						INDEX439 = (CommonToken)Match(input, INDEX, Follow._INDEX_in_create_index_stmt3776);
						stream_INDEX.Add(INDEX439);

						DebugLocation(491, 43);
						// C:\\Users\\Gareth\\Desktop\\test.g:491:43: ( IF NOT EXISTS )?
						int alt161 = 2;
						try
						{
							DebugEnterSubRule(161);
							try
							{
								DebugEnterDecision(161, decisionCanBacktrack[161]);
								int LA161_0 = input.LA(1);

								if ((LA161_0 == IF))
								{
									int LA161_1 = input.LA(2);

									if ((LA161_1 == NOT))
									{
										alt161 = 1;
									}
								}
							}
							finally { DebugExitDecision(161); }
							switch (alt161)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:491:44: IF NOT EXISTS
									{
										DebugLocation(491, 44);
										IF440 = (CommonToken)Match(input, IF, Follow._IF_in_create_index_stmt3779);
										stream_IF.Add(IF440);

										DebugLocation(491, 47);
										NOT441 = (CommonToken)Match(input, NOT, Follow._NOT_in_create_index_stmt3781);
										stream_NOT.Add(NOT441);

										DebugLocation(491, 51);
										EXISTS442 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_create_index_stmt3783);
										stream_EXISTS.Add(EXISTS442);


									}
									break;

							}
						}
						finally { DebugExitSubRule(161); }

						DebugLocation(491, 60);
						// C:\\Users\\Gareth\\Desktop\\test.g:491:60: (database_name= id DOT )?
						int alt162 = 2;
						try
						{
							DebugEnterSubRule(162);
							try
							{
								DebugEnterDecision(162, decisionCanBacktrack[162]);
								int LA162_0 = input.LA(1);

								if ((LA162_0 == ID || LA162_0 == STRING))
								{
									int LA162_1 = input.LA(2);

									if ((LA162_1 == DOT))
									{
										alt162 = 1;
									}
								}
								else if (((LA162_0 >= EXPLAIN && LA162_0 <= PLAN) || (LA162_0 >= INDEXED && LA162_0 <= BY) || (LA162_0 >= OR && LA162_0 <= ESCAPE) || (LA162_0 >= IS && LA162_0 <= BETWEEN) || LA162_0 == COLLATE || (LA162_0 >= DISTINCT && LA162_0 <= THEN) || (LA162_0 >= CURRENT_TIME && LA162_0 <= CURRENT_TIMESTAMP) || (LA162_0 >= RAISE && LA162_0 <= ROW)))
								{
									int LA162_2 = input.LA(2);

									if ((LA162_2 == DOT))
									{
										alt162 = 1;
									}
								}
							}
							finally { DebugExitDecision(162); }
							switch (alt162)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:491:61: database_name= id DOT
									{
										DebugLocation(491, 74);
										PushFollow(Follow._id_in_create_index_stmt3790);
										database_name = id();
										PopFollow();

										stream_id.Add(database_name.Tree);
										DebugLocation(491, 78);
										DOT443 = (CommonToken)Match(input, DOT, Follow._DOT_in_create_index_stmt3792);
										stream_DOT.Add(DOT443);


									}
									break;

							}
						}
						finally { DebugExitSubRule(162); }

						DebugLocation(491, 94);
						PushFollow(Follow._id_in_create_index_stmt3798);
						index_name = id();
						PopFollow();

						stream_id.Add(index_name.Tree);
						DebugLocation(492, 3);
						ON444 = (CommonToken)Match(input, ON, Follow._ON_in_create_index_stmt3802);
						stream_ON.Add(ON444);

						DebugLocation(492, 16);
						PushFollow(Follow._id_in_create_index_stmt3806);
						table_name = id();
						PopFollow();

						stream_id.Add(table_name.Tree);
						DebugLocation(492, 20);
						LPAREN445 = (CommonToken)Match(input, LPAREN, Follow._LPAREN_in_create_index_stmt3808);
						stream_LPAREN.Add(LPAREN445);

						DebugLocation(492, 34);
						PushFollow(Follow._indexed_column_in_create_index_stmt3812);
						columns = indexed_column();
						PopFollow();

						stream_indexed_column.Add(columns.Tree);
						if (list_columns == null) list_columns = new ArrayList();
						list_columns.Add(columns.Tree);

						DebugLocation(492, 51);
						// C:\\Users\\Gareth\\Desktop\\test.g:492:51: ( COMMA columns+= indexed_column )*
						try
						{
							DebugEnterSubRule(163);
							while (true)
							{
								int alt163 = 2;
								try
								{
									DebugEnterDecision(163, decisionCanBacktrack[163]);
									int LA163_0 = input.LA(1);

									if ((LA163_0 == COMMA))
									{
										alt163 = 1;
									}


								}
								finally { DebugExitDecision(163); }
								switch (alt163)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:492:52: COMMA columns+= indexed_column
										{
											DebugLocation(492, 52);
											COMMA446 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_create_index_stmt3815);
											stream_COMMA.Add(COMMA446);

											DebugLocation(492, 65);
											PushFollow(Follow._indexed_column_in_create_index_stmt3819);
											columns = indexed_column();
											PopFollow();

											stream_indexed_column.Add(columns.Tree);
											if (list_columns == null) list_columns = new ArrayList();
											list_columns.Add(columns.Tree);


										}
										break;

									default:
										goto loop163;
								}
							}

						loop163:
							;

						}
						finally { DebugExitSubRule(163); }

						DebugLocation(492, 84);
						RPAREN447 = (CommonToken)Match(input, RPAREN, Follow._RPAREN_in_create_index_stmt3823);
						stream_RPAREN.Add(RPAREN447);



						{
							// AST REWRITE
							// elements: database_name, table_name, UNIQUE, columns, index_name, EXISTS
							// token labels: 
							// rule labels: index_name, database_name, retval, table_name
							// token list labels: 
							// rule list labels: columns
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_index_name = new RewriteRuleSubtreeStream(adaptor, "rule index_name", index_name != null ? index_name.Tree : null);
							RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_table_name = new RewriteRuleSubtreeStream(adaptor, "rule table_name", table_name != null ? table_name.Tree : null);
							RewriteRuleSubtreeStream stream_columns = new RewriteRuleSubtreeStream(adaptor, "token columns", list_columns);
							root_0 = (CommonTree)adaptor.Nil();
							// 493:1: -> ^( CREATE_INDEX ^( OPTIONS ( UNIQUE )? ( EXISTS )? ) ^( $index_name ( $database_name)? ) $table_name ( ^( COLUMNS ( $columns)+ ) )? )
							{
								DebugLocation(493, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:493:4: ^( CREATE_INDEX ^( OPTIONS ( UNIQUE )? ( EXISTS )? ) ^( $index_name ( $database_name)? ) $table_name ( ^( COLUMNS ( $columns)+ ) )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(493, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(CREATE_INDEX, "CREATE_INDEX"), root_1);

									DebugLocation(493, 19);
									// C:\\Users\\Gareth\\Desktop\\test.g:493:19: ^( OPTIONS ( UNIQUE )? ( EXISTS )? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(493, 21);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(OPTIONS, "OPTIONS"), root_2);

										DebugLocation(493, 29);
										// C:\\Users\\Gareth\\Desktop\\test.g:493:29: ( UNIQUE )?
										if (stream_UNIQUE.HasNext)
										{
											DebugLocation(493, 29);
											adaptor.AddChild(root_2, stream_UNIQUE.NextNode());

										}
										stream_UNIQUE.Reset();
										DebugLocation(493, 37);
										// C:\\Users\\Gareth\\Desktop\\test.g:493:37: ( EXISTS )?
										if (stream_EXISTS.HasNext)
										{
											DebugLocation(493, 37);
											adaptor.AddChild(root_2, stream_EXISTS.NextNode());

										}
										stream_EXISTS.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(493, 46);
									// C:\\Users\\Gareth\\Desktop\\test.g:493:46: ^( $index_name ( $database_name)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(493, 48);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_index_name.NextNode(), root_2);

										DebugLocation(493, 60);
										// C:\\Users\\Gareth\\Desktop\\test.g:493:60: ( $database_name)?
										if (stream_database_name.HasNext)
										{
											DebugLocation(493, 60);
											adaptor.AddChild(root_2, stream_database_name.NextTree());

										}
										stream_database_name.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(493, 77);
									adaptor.AddChild(root_1, stream_table_name.NextTree());
									DebugLocation(493, 89);
									// C:\\Users\\Gareth\\Desktop\\test.g:493:89: ( ^( COLUMNS ( $columns)+ ) )?
									if (stream_columns.HasNext)
									{
										DebugLocation(493, 89);
										// C:\\Users\\Gareth\\Desktop\\test.g:493:89: ^( COLUMNS ( $columns)+ )
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(493, 91);
											root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(COLUMNS, "COLUMNS"), root_2);

											DebugLocation(493, 99);
											if (!(stream_columns.HasNext))
											{
												throw new RewriteEarlyExitException();
											}
											while (stream_columns.HasNext)
											{
												DebugLocation(493, 99);
												adaptor.AddChild(root_2, stream_columns.NextTree());

											}
											stream_columns.Reset();

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_columns.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("create_index_stmt", 82);
					LeaveRule("create_index_stmt", 82);
					Leave_create_index_stmt();
				}
				DebugLocation(493, 110);
			}
			finally { DebugExitRule(GrammarFileName, "create_index_stmt"); }
			return retval;

		}
		// $ANTLR end "create_index_stmt"

		public class indexed_column_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_indexed_column();
		partial void Leave_indexed_column();

		// $ANTLR start "indexed_column"
		// C:\\Users\\Gareth\\Desktop\\test.g:495:1: indexed_column : column_name= id ( COLLATE collation_name= id )? ( ASC | DESC )? -> ^( $column_name ( ^( COLLATE $collation_name) )? ( ASC )? ( DESC )? ) ;
		[GrammarRule("indexed_column")]
		private SQLiteParser.indexed_column_return indexed_column()
		{
			Enter_indexed_column();
			EnterRule("indexed_column", 83);
			TraceIn("indexed_column", 83);
			SQLiteParser.indexed_column_return retval = new SQLiteParser.indexed_column_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken COLLATE448 = null;
			CommonToken ASC449 = null;
			CommonToken DESC450 = null;
			SQLiteParser.id_return column_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return collation_name = default(SQLiteParser.id_return);

			CommonTree COLLATE448_tree = null;
			CommonTree ASC449_tree = null;
			CommonTree DESC450_tree = null;
			RewriteRuleITokenStream stream_ASC = new RewriteRuleITokenStream(adaptor, "token ASC");
			RewriteRuleITokenStream stream_DESC = new RewriteRuleITokenStream(adaptor, "token DESC");
			RewriteRuleITokenStream stream_COLLATE = new RewriteRuleITokenStream(adaptor, "token COLLATE");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			try
			{
				DebugEnterRule(GrammarFileName, "indexed_column");
				DebugLocation(495, 57);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:495:15: (column_name= id ( COLLATE collation_name= id )? ( ASC | DESC )? -> ^( $column_name ( ^( COLLATE $collation_name) )? ( ASC )? ( DESC )? ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:495:17: column_name= id ( COLLATE collation_name= id )? ( ASC | DESC )?
					{
						DebugLocation(495, 28);
						PushFollow(Follow._id_in_indexed_column3869);
						column_name = id();
						PopFollow();

						stream_id.Add(column_name.Tree);
						DebugLocation(495, 32);
						// C:\\Users\\Gareth\\Desktop\\test.g:495:32: ( COLLATE collation_name= id )?
						int alt164 = 2;
						try
						{
							DebugEnterSubRule(164);
							try
							{
								DebugEnterDecision(164, decisionCanBacktrack[164]);
								int LA164_0 = input.LA(1);

								if ((LA164_0 == COLLATE))
								{
									alt164 = 1;
								}
							}
							finally { DebugExitDecision(164); }
							switch (alt164)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:495:33: COLLATE collation_name= id
									{
										DebugLocation(495, 33);
										COLLATE448 = (CommonToken)Match(input, COLLATE, Follow._COLLATE_in_indexed_column3872);
										stream_COLLATE.Add(COLLATE448);

										DebugLocation(495, 55);
										PushFollow(Follow._id_in_indexed_column3876);
										collation_name = id();
										PopFollow();

										stream_id.Add(collation_name.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(164); }

						DebugLocation(495, 61);
						// C:\\Users\\Gareth\\Desktop\\test.g:495:61: ( ASC | DESC )?
						int alt165 = 3;
						try
						{
							DebugEnterSubRule(165);
							try
							{
								DebugEnterDecision(165, decisionCanBacktrack[165]);
								int LA165_0 = input.LA(1);

								if ((LA165_0 == ASC))
								{
									alt165 = 1;
								}
								else if ((LA165_0 == DESC))
								{
									alt165 = 2;
								}
							}
							finally { DebugExitDecision(165); }
							switch (alt165)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:495:62: ASC
									{
										DebugLocation(495, 62);
										ASC449 = (CommonToken)Match(input, ASC, Follow._ASC_in_indexed_column3881);
										stream_ASC.Add(ASC449);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:495:68: DESC
									{
										DebugLocation(495, 68);
										DESC450 = (CommonToken)Match(input, DESC, Follow._DESC_in_indexed_column3885);
										stream_DESC.Add(DESC450);


									}
									break;

							}
						}
						finally { DebugExitSubRule(165); }



						{
							// AST REWRITE
							// elements: COLLATE, DESC, column_name, ASC, collation_name
							// token labels: 
							// rule labels: retval, collation_name, column_name
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);
							RewriteRuleSubtreeStream stream_collation_name = new RewriteRuleSubtreeStream(adaptor, "rule collation_name", collation_name != null ? collation_name.Tree : null);
							RewriteRuleSubtreeStream stream_column_name = new RewriteRuleSubtreeStream(adaptor, "rule column_name", column_name != null ? column_name.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 496:1: -> ^( $column_name ( ^( COLLATE $collation_name) )? ( ASC )? ( DESC )? )
							{
								DebugLocation(496, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:496:4: ^( $column_name ( ^( COLLATE $collation_name) )? ( ASC )? ( DESC )? )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(496, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot(stream_column_name.NextNode(), root_1);

									DebugLocation(496, 19);
									// C:\\Users\\Gareth\\Desktop\\test.g:496:19: ( ^( COLLATE $collation_name) )?
									if (stream_COLLATE.HasNext || stream_collation_name.HasNext)
									{
										DebugLocation(496, 19);
										// C:\\Users\\Gareth\\Desktop\\test.g:496:19: ^( COLLATE $collation_name)
										{
											CommonTree root_2 = (CommonTree)adaptor.Nil();
											DebugLocation(496, 21);
											root_2 = (CommonTree)adaptor.BecomeRoot(stream_COLLATE.NextNode(), root_2);

											DebugLocation(496, 29);
											adaptor.AddChild(root_2, stream_collation_name.NextTree());

											adaptor.AddChild(root_1, root_2);
										}

									}
									stream_COLLATE.Reset();
									stream_collation_name.Reset();
									DebugLocation(496, 47);
									// C:\\Users\\Gareth\\Desktop\\test.g:496:47: ( ASC )?
									if (stream_ASC.HasNext)
									{
										DebugLocation(496, 47);
										adaptor.AddChild(root_1, stream_ASC.NextNode());

									}
									stream_ASC.Reset();
									DebugLocation(496, 52);
									// C:\\Users\\Gareth\\Desktop\\test.g:496:52: ( DESC )?
									if (stream_DESC.HasNext)
									{
										DebugLocation(496, 52);
										adaptor.AddChild(root_1, stream_DESC.NextNode());

									}
									stream_DESC.Reset();

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("indexed_column", 83);
					LeaveRule("indexed_column", 83);
					Leave_indexed_column();
				}
				DebugLocation(496, 57);
			}
			finally { DebugExitRule(GrammarFileName, "indexed_column"); }
			return retval;

		}
		// $ANTLR end "indexed_column"

		public class drop_index_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_drop_index_stmt();
		partial void Leave_drop_index_stmt();

		// $ANTLR start "drop_index_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:499:1: drop_index_stmt : DROP INDEX ( IF EXISTS )? (database_name= id DOT )? index_name= id -> ^( DROP_INDEX ^( OPTIONS ( EXISTS )? ) ^( $index_name ( $database_name)? ) ) ;
		[GrammarRule("drop_index_stmt")]
		private SQLiteParser.drop_index_stmt_return drop_index_stmt()
		{
			Enter_drop_index_stmt();
			EnterRule("drop_index_stmt", 84);
			TraceIn("drop_index_stmt", 84);
			SQLiteParser.drop_index_stmt_return retval = new SQLiteParser.drop_index_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DROP451 = null;
			CommonToken INDEX452 = null;
			CommonToken IF453 = null;
			CommonToken EXISTS454 = null;
			CommonToken DOT455 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return index_name = default(SQLiteParser.id_return);

			CommonTree DROP451_tree = null;
			CommonTree INDEX452_tree = null;
			CommonTree IF453_tree = null;
			CommonTree EXISTS454_tree = null;
			CommonTree DOT455_tree = null;
			RewriteRuleITokenStream stream_INDEX = new RewriteRuleITokenStream(adaptor, "token INDEX");
			RewriteRuleITokenStream stream_EXISTS = new RewriteRuleITokenStream(adaptor, "token EXISTS");
			RewriteRuleITokenStream stream_DROP = new RewriteRuleITokenStream(adaptor, "token DROP");
			RewriteRuleITokenStream stream_DOT = new RewriteRuleITokenStream(adaptor, "token DOT");
			RewriteRuleITokenStream stream_IF = new RewriteRuleITokenStream(adaptor, "token IF");
			RewriteRuleSubtreeStream stream_id = new RewriteRuleSubtreeStream(adaptor, "rule id");
			try
			{
				DebugEnterRule(GrammarFileName, "drop_index_stmt");
				DebugLocation(499, 66);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:499:16: ( DROP INDEX ( IF EXISTS )? (database_name= id DOT )? index_name= id -> ^( DROP_INDEX ^( OPTIONS ( EXISTS )? ) ^( $index_name ( $database_name)? ) ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:499:18: DROP INDEX ( IF EXISTS )? (database_name= id DOT )? index_name= id
					{
						DebugLocation(499, 18);
						DROP451 = (CommonToken)Match(input, DROP, Follow._DROP_in_drop_index_stmt3916);
						stream_DROP.Add(DROP451);

						DebugLocation(499, 23);
						INDEX452 = (CommonToken)Match(input, INDEX, Follow._INDEX_in_drop_index_stmt3918);
						stream_INDEX.Add(INDEX452);

						DebugLocation(499, 29);
						// C:\\Users\\Gareth\\Desktop\\test.g:499:29: ( IF EXISTS )?
						int alt166 = 2;
						try
						{
							DebugEnterSubRule(166);
							try
							{
								DebugEnterDecision(166, decisionCanBacktrack[166]);
								int LA166_0 = input.LA(1);

								if ((LA166_0 == IF))
								{
									int LA166_1 = input.LA(2);

									if ((LA166_1 == EXISTS))
									{
										alt166 = 1;
									}
								}
							}
							finally { DebugExitDecision(166); }
							switch (alt166)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:499:30: IF EXISTS
									{
										DebugLocation(499, 30);
										IF453 = (CommonToken)Match(input, IF, Follow._IF_in_drop_index_stmt3921);
										stream_IF.Add(IF453);

										DebugLocation(499, 33);
										EXISTS454 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_drop_index_stmt3923);
										stream_EXISTS.Add(EXISTS454);


									}
									break;

							}
						}
						finally { DebugExitSubRule(166); }

						DebugLocation(499, 42);
						// C:\\Users\\Gareth\\Desktop\\test.g:499:42: (database_name= id DOT )?
						int alt167 = 2;
						try
						{
							DebugEnterSubRule(167);
							try
							{
								DebugEnterDecision(167, decisionCanBacktrack[167]);
								int LA167_0 = input.LA(1);

								if ((LA167_0 == ID || LA167_0 == STRING))
								{
									int LA167_1 = input.LA(2);

									if ((LA167_1 == DOT))
									{
										alt167 = 1;
									}
								}
								else if (((LA167_0 >= EXPLAIN && LA167_0 <= PLAN) || (LA167_0 >= INDEXED && LA167_0 <= BY) || (LA167_0 >= OR && LA167_0 <= ESCAPE) || (LA167_0 >= IS && LA167_0 <= BETWEEN) || LA167_0 == COLLATE || (LA167_0 >= DISTINCT && LA167_0 <= THEN) || (LA167_0 >= CURRENT_TIME && LA167_0 <= CURRENT_TIMESTAMP) || (LA167_0 >= RAISE && LA167_0 <= ROW)))
								{
									int LA167_2 = input.LA(2);

									if ((LA167_2 == DOT))
									{
										alt167 = 1;
									}
								}
							}
							finally { DebugExitDecision(167); }
							switch (alt167)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:499:43: database_name= id DOT
									{
										DebugLocation(499, 56);
										PushFollow(Follow._id_in_drop_index_stmt3930);
										database_name = id();
										PopFollow();

										stream_id.Add(database_name.Tree);
										DebugLocation(499, 60);
										DOT455 = (CommonToken)Match(input, DOT, Follow._DOT_in_drop_index_stmt3932);
										stream_DOT.Add(DOT455);


									}
									break;

							}
						}
						finally { DebugExitSubRule(167); }

						DebugLocation(499, 76);
						PushFollow(Follow._id_in_drop_index_stmt3938);
						index_name = id();
						PopFollow();

						stream_id.Add(index_name.Tree);


						{
							// AST REWRITE
							// elements: index_name, database_name, EXISTS
							// token labels: 
							// rule labels: database_name, index_name, retval
							// token list labels: 
							// rule list labels: 
							// wildcard labels: 
							retval.Tree = root_0;
							RewriteRuleSubtreeStream stream_database_name = new RewriteRuleSubtreeStream(adaptor, "rule database_name", database_name != null ? database_name.Tree : null);
							RewriteRuleSubtreeStream stream_index_name = new RewriteRuleSubtreeStream(adaptor, "rule index_name", index_name != null ? index_name.Tree : null);
							RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval != null ? retval.Tree : null);

							root_0 = (CommonTree)adaptor.Nil();
							// 500:1: -> ^( DROP_INDEX ^( OPTIONS ( EXISTS )? ) ^( $index_name ( $database_name)? ) )
							{
								DebugLocation(500, 4);
								// C:\\Users\\Gareth\\Desktop\\test.g:500:4: ^( DROP_INDEX ^( OPTIONS ( EXISTS )? ) ^( $index_name ( $database_name)? ) )
								{
									CommonTree root_1 = (CommonTree)adaptor.Nil();
									DebugLocation(500, 6);
									root_1 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(DROP_INDEX, "DROP_INDEX"), root_1);

									DebugLocation(500, 17);
									// C:\\Users\\Gareth\\Desktop\\test.g:500:17: ^( OPTIONS ( EXISTS )? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(500, 19);
										root_2 = (CommonTree)adaptor.BecomeRoot((CommonTree)adaptor.Create(OPTIONS, "OPTIONS"), root_2);

										DebugLocation(500, 27);
										// C:\\Users\\Gareth\\Desktop\\test.g:500:27: ( EXISTS )?
										if (stream_EXISTS.HasNext)
										{
											DebugLocation(500, 27);
											adaptor.AddChild(root_2, stream_EXISTS.NextNode());

										}
										stream_EXISTS.Reset();

										adaptor.AddChild(root_1, root_2);
									}
									DebugLocation(500, 36);
									// C:\\Users\\Gareth\\Desktop\\test.g:500:36: ^( $index_name ( $database_name)? )
									{
										CommonTree root_2 = (CommonTree)adaptor.Nil();
										DebugLocation(500, 38);
										root_2 = (CommonTree)adaptor.BecomeRoot(stream_index_name.NextNode(), root_2);

										DebugLocation(500, 50);
										// C:\\Users\\Gareth\\Desktop\\test.g:500:50: ( $database_name)?
										if (stream_database_name.HasNext)
										{
											DebugLocation(500, 50);
											adaptor.AddChild(root_2, stream_database_name.NextTree());

										}
										stream_database_name.Reset();

										adaptor.AddChild(root_1, root_2);
									}

									adaptor.AddChild(root_0, root_1);
								}

							}

							retval.Tree = root_0;
						}

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("drop_index_stmt", 84);
					LeaveRule("drop_index_stmt", 84);
					Leave_drop_index_stmt();
				}
				DebugLocation(500, 66);
			}
			finally { DebugExitRule(GrammarFileName, "drop_index_stmt"); }
			return retval;

		}
		// $ANTLR end "drop_index_stmt"

		public class create_trigger_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_create_trigger_stmt();
		partial void Leave_create_trigger_stmt();

		// $ANTLR start "create_trigger_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:503:1: create_trigger_stmt : CREATE ( TEMPORARY )? TRIGGER ( IF NOT EXISTS )? (database_name= id DOT )? trigger_name= id ( BEFORE | AFTER | INSTEAD OF )? ( DELETE | INSERT | UPDATE ( OF column_names+= id ( COMMA column_names+= id )* )? ) ON table_name= id ( FOR EACH ROW )? ( WHEN expr )? BEGIN ( ( update_stmt | insert_stmt | delete_stmt | select_stmt ) SEMI )+ END ;
		[GrammarRule("create_trigger_stmt")]
		private SQLiteParser.create_trigger_stmt_return create_trigger_stmt()
		{
			Enter_create_trigger_stmt();
			EnterRule("create_trigger_stmt", 85);
			TraceIn("create_trigger_stmt", 85);
			SQLiteParser.create_trigger_stmt_return retval = new SQLiteParser.create_trigger_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken CREATE456 = null;
			CommonToken TEMPORARY457 = null;
			CommonToken TRIGGER458 = null;
			CommonToken IF459 = null;
			CommonToken NOT460 = null;
			CommonToken EXISTS461 = null;
			CommonToken DOT462 = null;
			CommonToken BEFORE463 = null;
			CommonToken AFTER464 = null;
			CommonToken INSTEAD465 = null;
			CommonToken OF466 = null;
			CommonToken DELETE467 = null;
			CommonToken INSERT468 = null;
			CommonToken UPDATE469 = null;
			CommonToken OF470 = null;
			CommonToken COMMA471 = null;
			CommonToken ON472 = null;
			CommonToken FOR473 = null;
			CommonToken EACH474 = null;
			CommonToken ROW475 = null;
			CommonToken WHEN476 = null;
			CommonToken BEGIN478 = null;
			CommonToken SEMI483 = null;
			CommonToken END484 = null;
			List list_column_names = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return trigger_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return table_name = default(SQLiteParser.id_return);
			SQLiteParser.expr_return expr477 = default(SQLiteParser.expr_return);
			SQLiteParser.update_stmt_return update_stmt479 = default(SQLiteParser.update_stmt_return);
			SQLiteParser.insert_stmt_return insert_stmt480 = default(SQLiteParser.insert_stmt_return);
			SQLiteParser.delete_stmt_return delete_stmt481 = default(SQLiteParser.delete_stmt_return);
			SQLiteParser.select_stmt_return select_stmt482 = default(SQLiteParser.select_stmt_return);
			SQLiteParser.id_return column_names = default(SQLiteParser.id_return);
			CommonTree CREATE456_tree = null;
			CommonTree TEMPORARY457_tree = null;
			CommonTree TRIGGER458_tree = null;
			CommonTree IF459_tree = null;
			CommonTree NOT460_tree = null;
			CommonTree EXISTS461_tree = null;
			CommonTree DOT462_tree = null;
			CommonTree BEFORE463_tree = null;
			CommonTree AFTER464_tree = null;
			CommonTree INSTEAD465_tree = null;
			CommonTree OF466_tree = null;
			CommonTree DELETE467_tree = null;
			CommonTree INSERT468_tree = null;
			CommonTree UPDATE469_tree = null;
			CommonTree OF470_tree = null;
			CommonTree COMMA471_tree = null;
			CommonTree ON472_tree = null;
			CommonTree FOR473_tree = null;
			CommonTree EACH474_tree = null;
			CommonTree ROW475_tree = null;
			CommonTree WHEN476_tree = null;
			CommonTree BEGIN478_tree = null;
			CommonTree SEMI483_tree = null;
			CommonTree END484_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "create_trigger_stmt");
				DebugLocation(503, 75);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:503:20: ( CREATE ( TEMPORARY )? TRIGGER ( IF NOT EXISTS )? (database_name= id DOT )? trigger_name= id ( BEFORE | AFTER | INSTEAD OF )? ( DELETE | INSERT | UPDATE ( OF column_names+= id ( COMMA column_names+= id )* )? ) ON table_name= id ( FOR EACH ROW )? ( WHEN expr )? BEGIN ( ( update_stmt | insert_stmt | delete_stmt | select_stmt ) SEMI )+ END )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:503:22: CREATE ( TEMPORARY )? TRIGGER ( IF NOT EXISTS )? (database_name= id DOT )? trigger_name= id ( BEFORE | AFTER | INSTEAD OF )? ( DELETE | INSERT | UPDATE ( OF column_names+= id ( COMMA column_names+= id )* )? ) ON table_name= id ( FOR EACH ROW )? ( WHEN expr )? BEGIN ( ( update_stmt | insert_stmt | delete_stmt | select_stmt ) SEMI )+ END
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(503, 22);
						CREATE456 = (CommonToken)Match(input, CREATE, Follow._CREATE_in_create_trigger_stmt3968);
						CREATE456_tree = (CommonTree)adaptor.Create(CREATE456);
						adaptor.AddChild(root_0, CREATE456_tree);

						DebugLocation(503, 29);
						// C:\\Users\\Gareth\\Desktop\\test.g:503:29: ( TEMPORARY )?
						int alt168 = 2;
						try
						{
							DebugEnterSubRule(168);
							try
							{
								DebugEnterDecision(168, decisionCanBacktrack[168]);
								int LA168_0 = input.LA(1);

								if ((LA168_0 == TEMPORARY))
								{
									alt168 = 1;
								}
							}
							finally { DebugExitDecision(168); }
							switch (alt168)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:503:29: TEMPORARY
									{
										DebugLocation(503, 29);
										TEMPORARY457 = (CommonToken)Match(input, TEMPORARY, Follow._TEMPORARY_in_create_trigger_stmt3970);
										TEMPORARY457_tree = (CommonTree)adaptor.Create(TEMPORARY457);
										adaptor.AddChild(root_0, TEMPORARY457_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(168); }

						DebugLocation(503, 40);
						TRIGGER458 = (CommonToken)Match(input, TRIGGER, Follow._TRIGGER_in_create_trigger_stmt3973);
						TRIGGER458_tree = (CommonTree)adaptor.Create(TRIGGER458);
						adaptor.AddChild(root_0, TRIGGER458_tree);

						DebugLocation(503, 48);
						// C:\\Users\\Gareth\\Desktop\\test.g:503:48: ( IF NOT EXISTS )?
						int alt169 = 2;
						try
						{
							DebugEnterSubRule(169);
							try
							{
								DebugEnterDecision(169, decisionCanBacktrack[169]);
								try
								{
									alt169 = dfa169.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(169); }
							switch (alt169)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:503:49: IF NOT EXISTS
									{
										DebugLocation(503, 49);
										IF459 = (CommonToken)Match(input, IF, Follow._IF_in_create_trigger_stmt3976);
										IF459_tree = (CommonTree)adaptor.Create(IF459);
										adaptor.AddChild(root_0, IF459_tree);

										DebugLocation(503, 52);
										NOT460 = (CommonToken)Match(input, NOT, Follow._NOT_in_create_trigger_stmt3978);
										NOT460_tree = (CommonTree)adaptor.Create(NOT460);
										adaptor.AddChild(root_0, NOT460_tree);

										DebugLocation(503, 56);
										EXISTS461 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_create_trigger_stmt3980);
										EXISTS461_tree = (CommonTree)adaptor.Create(EXISTS461);
										adaptor.AddChild(root_0, EXISTS461_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(169); }

						DebugLocation(503, 65);
						// C:\\Users\\Gareth\\Desktop\\test.g:503:65: (database_name= id DOT )?
						int alt170 = 2;
						try
						{
							DebugEnterSubRule(170);
							try
							{
								DebugEnterDecision(170, decisionCanBacktrack[170]);
								try
								{
									alt170 = dfa170.Predict(input);
								}
								catch (NoViableAltException nvae)
								{
									DebugRecognitionException(nvae);
									throw;
								}
							}
							finally { DebugExitDecision(170); }
							switch (alt170)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:503:66: database_name= id DOT
									{
										DebugLocation(503, 79);
										PushFollow(Follow._id_in_create_trigger_stmt3987);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(503, 83);
										DOT462 = (CommonToken)Match(input, DOT, Follow._DOT_in_create_trigger_stmt3989);
										DOT462_tree = (CommonTree)adaptor.Create(DOT462);
										adaptor.AddChild(root_0, DOT462_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(170); }

						DebugLocation(503, 101);
						PushFollow(Follow._id_in_create_trigger_stmt3995);
						trigger_name = id();
						PopFollow();

						adaptor.AddChild(root_0, trigger_name.Tree);
						DebugLocation(504, 3);
						// C:\\Users\\Gareth\\Desktop\\test.g:504:3: ( BEFORE | AFTER | INSTEAD OF )?
						int alt171 = 4;
						try
						{
							DebugEnterSubRule(171);
							try
							{
								DebugEnterDecision(171, decisionCanBacktrack[171]);
								switch (input.LA(1))
								{
									case BEFORE:
										{
											alt171 = 1;
										}
										break;
									case AFTER:
										{
											alt171 = 2;
										}
										break;
									case INSTEAD:
										{
											alt171 = 3;
										}
										break;
								}

							}
							finally { DebugExitDecision(171); }
							switch (alt171)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:504:4: BEFORE
									{
										DebugLocation(504, 4);
										BEFORE463 = (CommonToken)Match(input, BEFORE, Follow._BEFORE_in_create_trigger_stmt4000);
										BEFORE463_tree = (CommonTree)adaptor.Create(BEFORE463);
										adaptor.AddChild(root_0, BEFORE463_tree);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:504:13: AFTER
									{
										DebugLocation(504, 13);
										AFTER464 = (CommonToken)Match(input, AFTER, Follow._AFTER_in_create_trigger_stmt4004);
										AFTER464_tree = (CommonTree)adaptor.Create(AFTER464);
										adaptor.AddChild(root_0, AFTER464_tree);


									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:504:21: INSTEAD OF
									{
										DebugLocation(504, 21);
										INSTEAD465 = (CommonToken)Match(input, INSTEAD, Follow._INSTEAD_in_create_trigger_stmt4008);
										INSTEAD465_tree = (CommonTree)adaptor.Create(INSTEAD465);
										adaptor.AddChild(root_0, INSTEAD465_tree);

										DebugLocation(504, 29);
										OF466 = (CommonToken)Match(input, OF, Follow._OF_in_create_trigger_stmt4010);
										OF466_tree = (CommonTree)adaptor.Create(OF466);
										adaptor.AddChild(root_0, OF466_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(171); }

						DebugLocation(504, 34);
						// C:\\Users\\Gareth\\Desktop\\test.g:504:34: ( DELETE | INSERT | UPDATE ( OF column_names+= id ( COMMA column_names+= id )* )? )
						int alt174 = 3;
						try
						{
							DebugEnterSubRule(174);
							try
							{
								DebugEnterDecision(174, decisionCanBacktrack[174]);
								switch (input.LA(1))
								{
									case DELETE:
										{
											alt174 = 1;
										}
										break;
									case INSERT:
										{
											alt174 = 2;
										}
										break;
									case UPDATE:
										{
											alt174 = 3;
										}
										break;
									default:
										{
											NoViableAltException nvae = new NoViableAltException("", 174, 0, input);

											DebugRecognitionException(nvae);
											throw nvae;
										}
								}

							}
							finally { DebugExitDecision(174); }
							switch (alt174)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:504:35: DELETE
									{
										DebugLocation(504, 35);
										DELETE467 = (CommonToken)Match(input, DELETE, Follow._DELETE_in_create_trigger_stmt4015);
										DELETE467_tree = (CommonTree)adaptor.Create(DELETE467);
										adaptor.AddChild(root_0, DELETE467_tree);


									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:504:44: INSERT
									{
										DebugLocation(504, 44);
										INSERT468 = (CommonToken)Match(input, INSERT, Follow._INSERT_in_create_trigger_stmt4019);
										INSERT468_tree = (CommonTree)adaptor.Create(INSERT468);
										adaptor.AddChild(root_0, INSERT468_tree);


									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:504:53: UPDATE ( OF column_names+= id ( COMMA column_names+= id )* )?
									{
										DebugLocation(504, 53);
										UPDATE469 = (CommonToken)Match(input, UPDATE, Follow._UPDATE_in_create_trigger_stmt4023);
										UPDATE469_tree = (CommonTree)adaptor.Create(UPDATE469);
										adaptor.AddChild(root_0, UPDATE469_tree);

										DebugLocation(504, 60);
										// C:\\Users\\Gareth\\Desktop\\test.g:504:60: ( OF column_names+= id ( COMMA column_names+= id )* )?
										int alt173 = 2;
										try
										{
											DebugEnterSubRule(173);
											try
											{
												DebugEnterDecision(173, decisionCanBacktrack[173]);
												int LA173_0 = input.LA(1);

												if ((LA173_0 == OF))
												{
													alt173 = 1;
												}
											}
											finally { DebugExitDecision(173); }
											switch (alt173)
											{
												case 1:
													DebugEnterAlt(1);
													// C:\\Users\\Gareth\\Desktop\\test.g:504:61: OF column_names+= id ( COMMA column_names+= id )*
													{
														DebugLocation(504, 61);
														OF470 = (CommonToken)Match(input, OF, Follow._OF_in_create_trigger_stmt4026);
														OF470_tree = (CommonTree)adaptor.Create(OF470);
														adaptor.AddChild(root_0, OF470_tree);

														DebugLocation(504, 76);
														PushFollow(Follow._id_in_create_trigger_stmt4030);
														column_names = id();
														PopFollow();

														adaptor.AddChild(root_0, column_names.Tree);
														if (list_column_names == null) list_column_names = new ArrayList();
														list_column_names.Add(column_names.Tree);

														DebugLocation(504, 81);
														// C:\\Users\\Gareth\\Desktop\\test.g:504:81: ( COMMA column_names+= id )*
														try
														{
															DebugEnterSubRule(172);
															while (true)
															{
																int alt172 = 2;
																try
																{
																	DebugEnterDecision(172, decisionCanBacktrack[172]);
																	int LA172_0 = input.LA(1);

																	if ((LA172_0 == COMMA))
																	{
																		alt172 = 1;
																	}


																}
																finally { DebugExitDecision(172); }
																switch (alt172)
																{
																	case 1:
																		DebugEnterAlt(1);
																		// C:\\Users\\Gareth\\Desktop\\test.g:504:82: COMMA column_names+= id
																		{
																			DebugLocation(504, 82);
																			COMMA471 = (CommonToken)Match(input, COMMA, Follow._COMMA_in_create_trigger_stmt4033);
																			COMMA471_tree = (CommonTree)adaptor.Create(COMMA471);
																			adaptor.AddChild(root_0, COMMA471_tree);

																			DebugLocation(504, 100);
																			PushFollow(Follow._id_in_create_trigger_stmt4037);
																			column_names = id();
																			PopFollow();

																			adaptor.AddChild(root_0, column_names.Tree);
																			if (list_column_names == null) list_column_names = new ArrayList();
																			list_column_names.Add(column_names.Tree);


																		}
																		break;

																	default:
																		goto loop172;
																}
															}

														loop172:
															;

														}
														finally { DebugExitSubRule(172); }


													}
													break;

											}
										}
										finally { DebugExitSubRule(173); }


									}
									break;

							}
						}
						finally { DebugExitSubRule(174); }

						DebugLocation(505, 3);
						ON472 = (CommonToken)Match(input, ON, Follow._ON_in_create_trigger_stmt4046);
						ON472_tree = (CommonTree)adaptor.Create(ON472);
						adaptor.AddChild(root_0, ON472_tree);

						DebugLocation(505, 16);
						PushFollow(Follow._id_in_create_trigger_stmt4050);
						table_name = id();
						PopFollow();

						adaptor.AddChild(root_0, table_name.Tree);
						DebugLocation(505, 20);
						// C:\\Users\\Gareth\\Desktop\\test.g:505:20: ( FOR EACH ROW )?
						int alt175 = 2;
						try
						{
							DebugEnterSubRule(175);
							try
							{
								DebugEnterDecision(175, decisionCanBacktrack[175]);
								int LA175_0 = input.LA(1);

								if ((LA175_0 == FOR))
								{
									alt175 = 1;
								}
							}
							finally { DebugExitDecision(175); }
							switch (alt175)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:505:21: FOR EACH ROW
									{
										DebugLocation(505, 21);
										FOR473 = (CommonToken)Match(input, FOR, Follow._FOR_in_create_trigger_stmt4053);
										FOR473_tree = (CommonTree)adaptor.Create(FOR473);
										adaptor.AddChild(root_0, FOR473_tree);

										DebugLocation(505, 25);
										EACH474 = (CommonToken)Match(input, EACH, Follow._EACH_in_create_trigger_stmt4055);
										EACH474_tree = (CommonTree)adaptor.Create(EACH474);
										adaptor.AddChild(root_0, EACH474_tree);

										DebugLocation(505, 30);
										ROW475 = (CommonToken)Match(input, ROW, Follow._ROW_in_create_trigger_stmt4057);
										ROW475_tree = (CommonTree)adaptor.Create(ROW475);
										adaptor.AddChild(root_0, ROW475_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(175); }

						DebugLocation(505, 36);
						// C:\\Users\\Gareth\\Desktop\\test.g:505:36: ( WHEN expr )?
						int alt176 = 2;
						try
						{
							DebugEnterSubRule(176);
							try
							{
								DebugEnterDecision(176, decisionCanBacktrack[176]);
								int LA176_0 = input.LA(1);

								if ((LA176_0 == WHEN))
								{
									alt176 = 1;
								}
							}
							finally { DebugExitDecision(176); }
							switch (alt176)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:505:37: WHEN expr
									{
										DebugLocation(505, 37);
										WHEN476 = (CommonToken)Match(input, WHEN, Follow._WHEN_in_create_trigger_stmt4062);
										WHEN476_tree = (CommonTree)adaptor.Create(WHEN476);
										adaptor.AddChild(root_0, WHEN476_tree);

										DebugLocation(505, 42);
										PushFollow(Follow._expr_in_create_trigger_stmt4064);
										expr477 = expr();
										PopFollow();

										adaptor.AddChild(root_0, expr477.Tree);

									}
									break;

							}
						}
						finally { DebugExitSubRule(176); }

						DebugLocation(506, 3);
						BEGIN478 = (CommonToken)Match(input, BEGIN, Follow._BEGIN_in_create_trigger_stmt4070);
						BEGIN478_tree = (CommonTree)adaptor.Create(BEGIN478);
						adaptor.AddChild(root_0, BEGIN478_tree);

						DebugLocation(506, 9);
						// C:\\Users\\Gareth\\Desktop\\test.g:506:9: ( ( update_stmt | insert_stmt | delete_stmt | select_stmt ) SEMI )+
						int cnt178 = 0;
						try
						{
							DebugEnterSubRule(178);
							while (true)
							{
								int alt178 = 2;
								try
								{
									DebugEnterDecision(178, decisionCanBacktrack[178]);
									int LA178_0 = input.LA(1);

									if ((LA178_0 == REPLACE || LA178_0 == SELECT || LA178_0 == INSERT || LA178_0 == UPDATE || LA178_0 == DELETE))
									{
										alt178 = 1;
									}


								}
								finally { DebugExitDecision(178); }
								switch (alt178)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:506:10: ( update_stmt | insert_stmt | delete_stmt | select_stmt ) SEMI
										{
											DebugLocation(506, 10);
											// C:\\Users\\Gareth\\Desktop\\test.g:506:10: ( update_stmt | insert_stmt | delete_stmt | select_stmt )
											int alt177 = 4;
											try
											{
												DebugEnterSubRule(177);
												try
												{
													DebugEnterDecision(177, decisionCanBacktrack[177]);
													switch (input.LA(1))
													{
														case UPDATE:
															{
																alt177 = 1;
															}
															break;
														case REPLACE:
														case INSERT:
															{
																alt177 = 2;
															}
															break;
														case DELETE:
															{
																alt177 = 3;
															}
															break;
														case SELECT:
															{
																alt177 = 4;
															}
															break;
														default:
															{
																NoViableAltException nvae = new NoViableAltException("", 177, 0, input);

																DebugRecognitionException(nvae);
																throw nvae;
															}
													}

												}
												finally { DebugExitDecision(177); }
												switch (alt177)
												{
													case 1:
														DebugEnterAlt(1);
														// C:\\Users\\Gareth\\Desktop\\test.g:506:11: update_stmt
														{
															DebugLocation(506, 11);
															PushFollow(Follow._update_stmt_in_create_trigger_stmt4074);
															update_stmt479 = update_stmt();
															PopFollow();

															adaptor.AddChild(root_0, update_stmt479.Tree);

														}
														break;
													case 2:
														DebugEnterAlt(2);
														// C:\\Users\\Gareth\\Desktop\\test.g:506:25: insert_stmt
														{
															DebugLocation(506, 25);
															PushFollow(Follow._insert_stmt_in_create_trigger_stmt4078);
															insert_stmt480 = insert_stmt();
															PopFollow();

															adaptor.AddChild(root_0, insert_stmt480.Tree);

														}
														break;
													case 3:
														DebugEnterAlt(3);
														// C:\\Users\\Gareth\\Desktop\\test.g:506:39: delete_stmt
														{
															DebugLocation(506, 39);
															PushFollow(Follow._delete_stmt_in_create_trigger_stmt4082);
															delete_stmt481 = delete_stmt();
															PopFollow();

															adaptor.AddChild(root_0, delete_stmt481.Tree);

														}
														break;
													case 4:
														DebugEnterAlt(4);
														// C:\\Users\\Gareth\\Desktop\\test.g:506:53: select_stmt
														{
															DebugLocation(506, 53);
															PushFollow(Follow._select_stmt_in_create_trigger_stmt4086);
															select_stmt482 = select_stmt();
															PopFollow();

															adaptor.AddChild(root_0, select_stmt482.Tree);

														}
														break;

												}
											}
											finally { DebugExitSubRule(177); }

											DebugLocation(506, 66);
											SEMI483 = (CommonToken)Match(input, SEMI, Follow._SEMI_in_create_trigger_stmt4089);
											SEMI483_tree = (CommonTree)adaptor.Create(SEMI483);
											adaptor.AddChild(root_0, SEMI483_tree);


										}
										break;

									default:
										if (cnt178 >= 1)
											goto loop178;

										EarlyExitException eee178 = new EarlyExitException(178, input);
										DebugRecognitionException(eee178);
										throw eee178;
								}
								cnt178++;
							}
						loop178:
							;

						}
						finally { DebugExitSubRule(178); }

						DebugLocation(506, 73);
						END484 = (CommonToken)Match(input, END, Follow._END_in_create_trigger_stmt4093);
						END484_tree = (CommonTree)adaptor.Create(END484);
						adaptor.AddChild(root_0, END484_tree);


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("create_trigger_stmt", 85);
					LeaveRule("create_trigger_stmt", 85);
					Leave_create_trigger_stmt();
				}
				DebugLocation(506, 75);
			}
			finally { DebugExitRule(GrammarFileName, "create_trigger_stmt"); }
			return retval;

		}
		// $ANTLR end "create_trigger_stmt"

		public class drop_trigger_stmt_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_drop_trigger_stmt();
		partial void Leave_drop_trigger_stmt();

		// $ANTLR start "drop_trigger_stmt"
		// C:\\Users\\Gareth\\Desktop\\test.g:509:1: drop_trigger_stmt : DROP TRIGGER ( IF EXISTS )? (database_name= id DOT )? trigger_name= id ;
		[GrammarRule("drop_trigger_stmt")]
		private SQLiteParser.drop_trigger_stmt_return drop_trigger_stmt()
		{
			Enter_drop_trigger_stmt();
			EnterRule("drop_trigger_stmt", 86);
			TraceIn("drop_trigger_stmt", 86);
			SQLiteParser.drop_trigger_stmt_return retval = new SQLiteParser.drop_trigger_stmt_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken DROP485 = null;
			CommonToken TRIGGER486 = null;
			CommonToken IF487 = null;
			CommonToken EXISTS488 = null;
			CommonToken DOT489 = null;
			SQLiteParser.id_return database_name = default(SQLiteParser.id_return);
			SQLiteParser.id_return trigger_name = default(SQLiteParser.id_return);

			CommonTree DROP485_tree = null;
			CommonTree TRIGGER486_tree = null;
			CommonTree IF487_tree = null;
			CommonTree EXISTS488_tree = null;
			CommonTree DOT489_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "drop_trigger_stmt");
				DebugLocation(509, 84);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:509:18: ( DROP TRIGGER ( IF EXISTS )? (database_name= id DOT )? trigger_name= id )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:509:20: DROP TRIGGER ( IF EXISTS )? (database_name= id DOT )? trigger_name= id
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(509, 20);
						DROP485 = (CommonToken)Match(input, DROP, Follow._DROP_in_drop_trigger_stmt4101);
						DROP485_tree = (CommonTree)adaptor.Create(DROP485);
						adaptor.AddChild(root_0, DROP485_tree);

						DebugLocation(509, 25);
						TRIGGER486 = (CommonToken)Match(input, TRIGGER, Follow._TRIGGER_in_drop_trigger_stmt4103);
						TRIGGER486_tree = (CommonTree)adaptor.Create(TRIGGER486);
						adaptor.AddChild(root_0, TRIGGER486_tree);

						DebugLocation(509, 33);
						// C:\\Users\\Gareth\\Desktop\\test.g:509:33: ( IF EXISTS )?
						int alt179 = 2;
						try
						{
							DebugEnterSubRule(179);
							try
							{
								DebugEnterDecision(179, decisionCanBacktrack[179]);
								int LA179_0 = input.LA(1);

								if ((LA179_0 == IF))
								{
									int LA179_1 = input.LA(2);

									if ((LA179_1 == EXISTS))
									{
										alt179 = 1;
									}
								}
							}
							finally { DebugExitDecision(179); }
							switch (alt179)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:509:34: IF EXISTS
									{
										DebugLocation(509, 34);
										IF487 = (CommonToken)Match(input, IF, Follow._IF_in_drop_trigger_stmt4106);
										IF487_tree = (CommonTree)adaptor.Create(IF487);
										adaptor.AddChild(root_0, IF487_tree);

										DebugLocation(509, 37);
										EXISTS488 = (CommonToken)Match(input, EXISTS, Follow._EXISTS_in_drop_trigger_stmt4108);
										EXISTS488_tree = (CommonTree)adaptor.Create(EXISTS488);
										adaptor.AddChild(root_0, EXISTS488_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(179); }

						DebugLocation(509, 46);
						// C:\\Users\\Gareth\\Desktop\\test.g:509:46: (database_name= id DOT )?
						int alt180 = 2;
						try
						{
							DebugEnterSubRule(180);
							try
							{
								DebugEnterDecision(180, decisionCanBacktrack[180]);
								int LA180_0 = input.LA(1);

								if ((LA180_0 == ID || LA180_0 == STRING))
								{
									int LA180_1 = input.LA(2);

									if ((LA180_1 == DOT))
									{
										alt180 = 1;
									}
								}
								else if (((LA180_0 >= EXPLAIN && LA180_0 <= PLAN) || (LA180_0 >= INDEXED && LA180_0 <= BY) || (LA180_0 >= OR && LA180_0 <= ESCAPE) || (LA180_0 >= IS && LA180_0 <= BETWEEN) || LA180_0 == COLLATE || (LA180_0 >= DISTINCT && LA180_0 <= THEN) || (LA180_0 >= CURRENT_TIME && LA180_0 <= CURRENT_TIMESTAMP) || (LA180_0 >= RAISE && LA180_0 <= ROW)))
								{
									int LA180_2 = input.LA(2);

									if ((LA180_2 == DOT))
									{
										alt180 = 1;
									}
								}
							}
							finally { DebugExitDecision(180); }
							switch (alt180)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:509:47: database_name= id DOT
									{
										DebugLocation(509, 60);
										PushFollow(Follow._id_in_drop_trigger_stmt4115);
										database_name = id();
										PopFollow();

										adaptor.AddChild(root_0, database_name.Tree);
										DebugLocation(509, 64);
										DOT489 = (CommonToken)Match(input, DOT, Follow._DOT_in_drop_trigger_stmt4117);
										DOT489_tree = (CommonTree)adaptor.Create(DOT489);
										adaptor.AddChild(root_0, DOT489_tree);


									}
									break;

							}
						}
						finally { DebugExitSubRule(180); }

						DebugLocation(509, 82);
						PushFollow(Follow._id_in_drop_trigger_stmt4123);
						trigger_name = id();
						PopFollow();

						adaptor.AddChild(root_0, trigger_name.Tree);

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("drop_trigger_stmt", 86);
					LeaveRule("drop_trigger_stmt", 86);
					Leave_drop_trigger_stmt();
				}
				DebugLocation(509, 84);
			}
			finally { DebugExitRule(GrammarFileName, "drop_trigger_stmt"); }
			return retval;

		}
		// $ANTLR end "drop_trigger_stmt"

		public class id_core_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_id_core();
		partial void Leave_id_core();

		// $ANTLR start "id_core"
		// C:\\Users\\Gareth\\Desktop\\test.g:512:1: id_core : str= ( ID | STRING ) ;
		[GrammarRule("id_core")]
		private SQLiteParser.id_core_return id_core()
		{
			Enter_id_core();
			EnterRule("id_core", 87);
			TraceIn("id_core", 87);
			SQLiteParser.id_core_return retval = new SQLiteParser.id_core_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken str = null;

			CommonTree str_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "id_core");
				DebugLocation(512, 65);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:512:8: (str= ( ID | STRING ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:512:10: str= ( ID | STRING )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(512, 13);
						str = (CommonToken)input.LT(1);
						if (input.LA(1) == ID || input.LA(1) == STRING)
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(str));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}

						DebugLocation(512, 30);
						str.Text = unquoteId((str != null ? str.Text : null));

					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("id_core", 87);
					LeaveRule("id_core", 87);
					Leave_id_core();
				}
				DebugLocation(512, 65);
			}
			finally { DebugExitRule(GrammarFileName, "id_core"); }
			return retval;

		}
		// $ANTLR end "id_core"

		public class id_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_id();
		partial void Leave_id();

		// $ANTLR start "id"
		// C:\\Users\\Gareth\\Desktop\\test.g:516:1: id : ( id_core | keyword );
		[GrammarRule("id")]
		private SQLiteParser.id_return id()
		{
			Enter_id();
			EnterRule("id", 88);
			TraceIn("id", 88);
			SQLiteParser.id_return retval = new SQLiteParser.id_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.id_core_return id_core490 = default(SQLiteParser.id_core_return);
			SQLiteParser.keyword_return keyword491 = default(SQLiteParser.keyword_return);


			try
			{
				DebugEnterRule(GrammarFileName, "id");
				DebugLocation(516, 21);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:516:3: ( id_core | keyword )
					int alt181 = 2;
					try
					{
						DebugEnterDecision(181, decisionCanBacktrack[181]);
						int LA181_0 = input.LA(1);

						if ((LA181_0 == ID || LA181_0 == STRING))
						{
							alt181 = 1;
						}
						else if (((LA181_0 >= EXPLAIN && LA181_0 <= PLAN) || (LA181_0 >= INDEXED && LA181_0 <= BY) || (LA181_0 >= OR && LA181_0 <= ESCAPE) || (LA181_0 >= IS && LA181_0 <= BETWEEN) || LA181_0 == COLLATE || (LA181_0 >= DISTINCT && LA181_0 <= THEN) || (LA181_0 >= CURRENT_TIME && LA181_0 <= CURRENT_TIMESTAMP) || (LA181_0 >= RAISE && LA181_0 <= ROW)))
						{
							alt181 = 2;
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 181, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(181); }
					switch (alt181)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:516:5: id_core
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(516, 5);
								PushFollow(Follow._id_core_in_id4152);
								id_core490 = id_core();
								PopFollow();

								adaptor.AddChild(root_0, id_core490.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:516:15: keyword
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(516, 15);
								PushFollow(Follow._keyword_in_id4156);
								keyword491 = keyword();
								PopFollow();

								adaptor.AddChild(root_0, keyword491.Tree);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("id", 88);
					LeaveRule("id", 88);
					Leave_id();
				}
				DebugLocation(516, 21);
			}
			finally { DebugExitRule(GrammarFileName, "id"); }
			return retval;

		}
		// $ANTLR end "id"

		public class keyword_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_keyword();
		partial void Leave_keyword();

		// $ANTLR start "keyword"
		// C:\\Users\\Gareth\\Desktop\\test.g:518:1: keyword : ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GROUP | HAVING | IF | IGNORE | IMMEDIATE | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | JOIN | KEY | LEFT | LIMIT | NATURAL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE ) ;
		[GrammarRule("keyword")]
		private SQLiteParser.keyword_return keyword()
		{
			Enter_keyword();
			EnterRule("keyword", 89);
			TraceIn("keyword", 89);
			SQLiteParser.keyword_return retval = new SQLiteParser.keyword_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set492 = null;

			CommonTree set492_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "keyword");
				DebugLocation(518, 3);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:518:8: ( ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GROUP | HAVING | IF | IGNORE | IMMEDIATE | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | JOIN | KEY | LEFT | LIMIT | NATURAL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:518:10: ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GROUP | HAVING | IF | IGNORE | IMMEDIATE | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | JOIN | KEY | LEFT | LIMIT | NATURAL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(518, 10);
						set492 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= EXPLAIN && input.LA(1) <= PLAN) || (input.LA(1) >= INDEXED && input.LA(1) <= BY) || (input.LA(1) >= OR && input.LA(1) <= ESCAPE) || (input.LA(1) >= IS && input.LA(1) <= BETWEEN) || input.LA(1) == COLLATE || (input.LA(1) >= DISTINCT && input.LA(1) <= THEN) || (input.LA(1) >= CURRENT_TIME && input.LA(1) <= CURRENT_TIMESTAMP) || (input.LA(1) >= RAISE && input.LA(1) <= ROW))
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set492));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("keyword", 89);
					LeaveRule("keyword", 89);
					Leave_keyword();
				}
				DebugLocation(635, 3);
			}
			finally { DebugExitRule(GrammarFileName, "keyword"); }
			return retval;

		}
		// $ANTLR end "keyword"

		public class id_column_def_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_id_column_def();
		partial void Leave_id_column_def();

		// $ANTLR start "id_column_def"
		// C:\\Users\\Gareth\\Desktop\\test.g:637:1: id_column_def : ( id_core | keyword_column_def );
		[GrammarRule("id_column_def")]
		private SQLiteParser.id_column_def_return id_column_def()
		{
			Enter_id_column_def();
			EnterRule("id_column_def", 90);
			TraceIn("id_column_def", 90);
			SQLiteParser.id_column_def_return retval = new SQLiteParser.id_column_def_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			SQLiteParser.id_core_return id_core493 = default(SQLiteParser.id_core_return);
			SQLiteParser.keyword_column_def_return keyword_column_def494 = default(SQLiteParser.keyword_column_def_return);


			try
			{
				DebugEnterRule(GrammarFileName, "id_column_def");
				DebugLocation(637, 43);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:637:14: ( id_core | keyword_column_def )
					int alt182 = 2;
					try
					{
						DebugEnterDecision(182, decisionCanBacktrack[182]);
						int LA182_0 = input.LA(1);

						if ((LA182_0 == ID || LA182_0 == STRING))
						{
							alt182 = 1;
						}
						else if (((LA182_0 >= EXPLAIN && LA182_0 <= PLAN) || (LA182_0 >= INDEXED && LA182_0 <= IN) || (LA182_0 >= ISNULL && LA182_0 <= BETWEEN) || (LA182_0 >= LIKE && LA182_0 <= MATCH) || LA182_0 == COLLATE || (LA182_0 >= DISTINCT && LA182_0 <= THEN) || (LA182_0 >= CURRENT_TIME && LA182_0 <= CURRENT_TIMESTAMP) || (LA182_0 >= RAISE && LA182_0 <= EXISTS) || (LA182_0 >= PRIMARY && LA182_0 <= ADD) || (LA182_0 >= VIEW && LA182_0 <= ROW)))
						{
							alt182 = 2;
						}
						else
						{
							NoViableAltException nvae = new NoViableAltException("", 182, 0, input);

							DebugRecognitionException(nvae);
							throw nvae;
						}
					}
					finally { DebugExitDecision(182); }
					switch (alt182)
					{
						case 1:
							DebugEnterAlt(1);
							// C:\\Users\\Gareth\\Desktop\\test.g:637:16: id_core
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(637, 16);
								PushFollow(Follow._id_core_in_id_column_def4830);
								id_core493 = id_core();
								PopFollow();

								adaptor.AddChild(root_0, id_core493.Tree);

							}
							break;
						case 2:
							DebugEnterAlt(2);
							// C:\\Users\\Gareth\\Desktop\\test.g:637:26: keyword_column_def
							{
								root_0 = (CommonTree)adaptor.Nil();

								DebugLocation(637, 26);
								PushFollow(Follow._keyword_column_def_in_id_column_def4834);
								keyword_column_def494 = keyword_column_def();
								PopFollow();

								adaptor.AddChild(root_0, keyword_column_def494.Tree);

							}
							break;

					}
					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("id_column_def", 90);
					LeaveRule("id_column_def", 90);
					Leave_id_column_def();
				}
				DebugLocation(637, 43);
			}
			finally { DebugExitRule(GrammarFileName, "id_column_def"); }
			return retval;

		}
		// $ANTLR end "id_column_def"

		public class keyword_column_def_return : ParserRuleReturnScope<CommonToken>, IAstRuleReturnScope<CommonTree>
		{
			private CommonTree _tree;
			public CommonTree Tree { get { return _tree; } set { _tree = value; } }
		}

		partial void Enter_keyword_column_def();
		partial void Leave_keyword_column_def();

		// $ANTLR start "keyword_column_def"
		// C:\\Users\\Gareth\\Desktop\\test.g:639:1: keyword_column_def : ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COMMIT | CONFLICT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE ) ;
		[GrammarRule("keyword_column_def")]
		private SQLiteParser.keyword_column_def_return keyword_column_def()
		{
			Enter_keyword_column_def();
			EnterRule("keyword_column_def", 91);
			TraceIn("keyword_column_def", 91);
			SQLiteParser.keyword_column_def_return retval = new SQLiteParser.keyword_column_def_return();
			retval.Start = (CommonToken)input.LT(1);

			CommonTree root_0 = null;

			CommonToken set495 = null;

			CommonTree set495_tree = null;

			try
			{
				DebugEnterRule(GrammarFileName, "keyword_column_def");
				DebugLocation(639, 3);
				try
				{
					// C:\\Users\\Gareth\\Desktop\\test.g:639:19: ( ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COMMIT | CONFLICT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE ) )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:639:21: ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COMMIT | CONFLICT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE )
					{
						root_0 = (CommonTree)adaptor.Nil();

						DebugLocation(639, 21);
						set495 = (CommonToken)input.LT(1);
						if ((input.LA(1) >= EXPLAIN && input.LA(1) <= PLAN) || (input.LA(1) >= INDEXED && input.LA(1) <= IN) || (input.LA(1) >= ISNULL && input.LA(1) <= BETWEEN) || (input.LA(1) >= LIKE && input.LA(1) <= MATCH) || input.LA(1) == COLLATE || (input.LA(1) >= DISTINCT && input.LA(1) <= THEN) || (input.LA(1) >= CURRENT_TIME && input.LA(1) <= CURRENT_TIMESTAMP) || (input.LA(1) >= RAISE && input.LA(1) <= EXISTS) || (input.LA(1) >= PRIMARY && input.LA(1) <= ADD) || (input.LA(1) >= VIEW && input.LA(1) <= ROW))
						{
							input.Consume();
							adaptor.AddChild(root_0, (CommonTree)adaptor.Create(set495));
							state.errorRecovery = false;
						}
						else
						{
							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

					retval.Stop = (CommonToken)input.LT(-1);

					retval.Tree = (CommonTree)adaptor.RulePostProcessing(root_0);
					adaptor.SetTokenBoundaries(retval.Tree, retval.Start, retval.Stop);

				}
				catch (RecognitionException re)
				{
					ReportError(re);
					Recover(input, re);
					retval.Tree = (CommonTree)adaptor.ErrorNode(input, retval.Start, input.LT(-1), re);

				}
				finally
				{
					TraceOut("keyword_column_def", 91);
					LeaveRule("keyword_column_def", 91);
					Leave_keyword_column_def();
				}
				DebugLocation(756, 3);
			}
			finally { DebugExitRule(GrammarFileName, "keyword_column_def"); }
			return retval;

		}
		// $ANTLR end "keyword_column_def"
		#endregion Rules


		#region DFA
		DFA1 dfa1;
		DFA5 dfa5;
		DFA4 dfa4;
		DFA6 dfa6;
		DFA8 dfa8;
		DFA10 dfa10;
		DFA11 dfa11;
		DFA12 dfa12;
		DFA22 dfa22;
		DFA14 dfa14;
		DFA18 dfa18;
		DFA21 dfa21;
		DFA23 dfa23;
		DFA24 dfa24;
		DFA25 dfa25;
		DFA26 dfa26;
		DFA27 dfa27;
		DFA28 dfa28;
		DFA29 dfa29;
		DFA38 dfa38;
		DFA31 dfa31;
		DFA30 dfa30;
		DFA34 dfa34;
		DFA32 dfa32;
		DFA35 dfa35;
		DFA40 dfa40;
		DFA42 dfa42;
		DFA44 dfa44;
		DFA46 dfa46;
		DFA49 dfa49;
		DFA51 dfa51;
		DFA53 dfa53;
		DFA65 dfa65;
		DFA66 dfa66;
		DFA67 dfa67;
		DFA68 dfa68;
		DFA71 dfa71;
		DFA69 dfa69;
		DFA70 dfa70;
		DFA74 dfa74;
		DFA73 dfa73;
		DFA72 dfa72;
		DFA76 dfa76;
		DFA75 dfa75;
		DFA83 dfa83;
		DFA77 dfa77;
		DFA79 dfa79;
		DFA80 dfa80;
		DFA82 dfa82;
		DFA93 dfa93;
		DFA118 dfa118;
		DFA121 dfa121;
		DFA122 dfa122;
		DFA123 dfa123;
		DFA124 dfa124;
		DFA125 dfa125;
		DFA126 dfa126;
		DFA127 dfa127;
		DFA128 dfa128;
		DFA129 dfa129;
		DFA130 dfa130;
		DFA132 dfa132;
		DFA143 dfa143;
		DFA144 dfa144;
		DFA145 dfa145;
		DFA149 dfa149;
		DFA169 dfa169;
		DFA170 dfa170;

		protected override void InitDFAs()
		{
			base.InitDFAs();
			dfa1 = new DFA1(this);
			dfa5 = new DFA5(this);
			dfa4 = new DFA4(this);
			dfa6 = new DFA6(this);
			dfa8 = new DFA8(this);
			dfa10 = new DFA10(this);
			dfa11 = new DFA11(this);
			dfa12 = new DFA12(this);
			dfa22 = new DFA22(this);
			dfa14 = new DFA14(this);
			dfa18 = new DFA18(this);
			dfa21 = new DFA21(this);
			dfa23 = new DFA23(this);
			dfa24 = new DFA24(this);
			dfa25 = new DFA25(this);
			dfa26 = new DFA26(this);
			dfa27 = new DFA27(this);
			dfa28 = new DFA28(this);
			dfa29 = new DFA29(this);
			dfa38 = new DFA38(this);
			dfa31 = new DFA31(this);
			dfa30 = new DFA30(this);
			dfa34 = new DFA34(this);
			dfa32 = new DFA32(this);
			dfa35 = new DFA35(this);
			dfa40 = new DFA40(this);
			dfa42 = new DFA42(this);
			dfa44 = new DFA44(this);
			dfa46 = new DFA46(this);
			dfa49 = new DFA49(this);
			dfa51 = new DFA51(this);
			dfa53 = new DFA53(this);
			dfa65 = new DFA65(this);
			dfa66 = new DFA66(this);
			dfa67 = new DFA67(this);
			dfa68 = new DFA68(this);
			dfa71 = new DFA71(this);
			dfa69 = new DFA69(this);
			dfa70 = new DFA70(this);
			dfa74 = new DFA74(this);
			dfa73 = new DFA73(this);
			dfa72 = new DFA72(this);
			dfa76 = new DFA76(this);
			dfa75 = new DFA75(this);
			dfa83 = new DFA83(this);
			dfa77 = new DFA77(this);
			dfa79 = new DFA79(this);
			dfa80 = new DFA80(this);
			dfa82 = new DFA82(this);
			dfa93 = new DFA93(this);
			dfa118 = new DFA118(this);
			dfa121 = new DFA121(this);
			dfa122 = new DFA122(this);
			dfa123 = new DFA123(this);
			dfa124 = new DFA124(this);
			dfa125 = new DFA125(this);
			dfa126 = new DFA126(this);
			dfa127 = new DFA127(this);
			dfa128 = new DFA128(this);
			dfa129 = new DFA129(this);
			dfa130 = new DFA130(this);
			dfa132 = new DFA132(this);
			dfa143 = new DFA143(this);
			dfa144 = new DFA144(this);
			dfa145 = new DFA145(this);
			dfa149 = new DFA149(this);
			dfa169 = new DFA169(this);
			dfa170 = new DFA170(this);
		}

		private class DFA1 : DFA
		{
			private const string DFA1_eotS =
				"\x16\xFFFF";
			private const string DFA1_eofS =
				"\x1\x1\x15\xFFFF";
			private const string DFA1_minS =
				"\x1\x21\x15\xFFFF";
			private const string DFA1_maxS =
				"\x1\xA6\x15\xFFFF";
			private const string DFA1_acceptS =
				"\x1\xFFFF\x1\x2\x1\x1\x13\xFFFF";
			private const string DFA1_specialS =
				"\x16\xFFFF}>";
			private static readonly string[] DFA1_transitionS =
			{
				"\x1\x2\x30\xFFFF\x1\x2\xE\xFFFF\x1\x2\x2\xFFFF\x2\x2\x1\xFFFF\x5\x2"+
				"\x9\xFFFF\x1\x2\xC\xFFFF\x1\x2\x3\xFFFF\x1\x2\x1\xFFFF\x2\x2\x4\xFFFF"+
				"\x1\x2\x1\xFFFF\x2\x2\x1\xFFFF\x1\x2\x11\xFFFF\x2\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA1_eot = DFA.UnpackEncodedString(DFA1_eotS);
			private static readonly short[] DFA1_eof = DFA.UnpackEncodedString(DFA1_eofS);
			private static readonly char[] DFA1_min = DFA.UnpackEncodedStringToUnsignedChars(DFA1_minS);
			private static readonly char[] DFA1_max = DFA.UnpackEncodedStringToUnsignedChars(DFA1_maxS);
			private static readonly short[] DFA1_accept = DFA.UnpackEncodedString(DFA1_acceptS);
			private static readonly short[] DFA1_special = DFA.UnpackEncodedString(DFA1_specialS);
			private static readonly short[][] DFA1_transition;

			static DFA1()
			{
				int numStates = DFA1_transitionS.Length;
				DFA1_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA1_transition[i] = DFA.UnpackEncodedString(DFA1_transitionS[i]);
				}
			}

			public DFA1(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 1;
				this.eot = DFA1_eot;
				this.eof = DFA1_eof;
				this.min = DFA1_min;
				this.max = DFA1_max;
				this.accept = DFA1_accept;
				this.special = DFA1_special;
				this.transition = DFA1_transition;
			}

			public override string Description { get { return "()* loopback of 145:32: ( sql_stmt SEMI )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA5 : DFA
		{
			private const string DFA5_eotS =
				"\x15\xFFFF";
			private const string DFA5_eofS =
				"\x15\xFFFF";
			private const string DFA5_minS =
				"\x1\x21\x14\xFFFF";
			private const string DFA5_maxS =
				"\x1\xA6\x14\xFFFF";
			private const string DFA5_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x12\xFFFF";
			private const string DFA5_specialS =
				"\x15\xFFFF}>";
			private static readonly string[] DFA5_transitionS =
			{
				"\x1\x1\x30\xFFFF\x1\x2\xE\xFFFF\x1\x2\x2\xFFFF\x2\x2\x1\xFFFF\x5\x2"+
				"\x9\xFFFF\x1\x2\xC\xFFFF\x1\x2\x3\xFFFF\x1\x2\x1\xFFFF\x2\x2\x4\xFFFF"+
				"\x1\x2\x1\xFFFF\x2\x2\x1\xFFFF\x1\x2\x11\xFFFF\x2\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA5_eot = DFA.UnpackEncodedString(DFA5_eotS);
			private static readonly short[] DFA5_eof = DFA.UnpackEncodedString(DFA5_eofS);
			private static readonly char[] DFA5_min = DFA.UnpackEncodedStringToUnsignedChars(DFA5_minS);
			private static readonly char[] DFA5_max = DFA.UnpackEncodedStringToUnsignedChars(DFA5_maxS);
			private static readonly short[] DFA5_accept = DFA.UnpackEncodedString(DFA5_acceptS);
			private static readonly short[] DFA5_special = DFA.UnpackEncodedString(DFA5_specialS);
			private static readonly short[][] DFA5_transition;

			static DFA5()
			{
				int numStates = DFA5_transitionS.Length;
				DFA5_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA5_transition[i] = DFA.UnpackEncodedString(DFA5_transitionS[i]);
				}
			}

			public DFA5(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 5;
				this.eot = DFA5_eot;
				this.eof = DFA5_eof;
				this.min = DFA5_min;
				this.max = DFA5_max;
				this.accept = DFA5_accept;
				this.special = DFA5_special;
				this.transition = DFA5_transition;
			}

			public override string Description { get { return "149:11: ( EXPLAIN ( QUERY PLAN )? )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA4 : DFA
		{
			private const string DFA4_eotS =
				"\x15\xFFFF";
			private const string DFA4_eofS =
				"\x15\xFFFF";
			private const string DFA4_minS =
				"\x1\x22\x14\xFFFF";
			private const string DFA4_maxS =
				"\x1\xA6\x14\xFFFF";
			private const string DFA4_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x12\xFFFF";
			private const string DFA4_specialS =
				"\x15\xFFFF}>";
			private static readonly string[] DFA4_transitionS =
			{
				"\x1\x1\x2F\xFFFF\x1\x2\xE\xFFFF\x1\x2\x2\xFFFF\x2\x2\x1\xFFFF\x5\x2"+
				"\x9\xFFFF\x1\x2\xC\xFFFF\x1\x2\x3\xFFFF\x1\x2\x1\xFFFF\x2\x2\x4\xFFFF"+
				"\x1\x2\x1\xFFFF\x2\x2\x1\xFFFF\x1\x2\x11\xFFFF\x2\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA4_eot = DFA.UnpackEncodedString(DFA4_eotS);
			private static readonly short[] DFA4_eof = DFA.UnpackEncodedString(DFA4_eofS);
			private static readonly char[] DFA4_min = DFA.UnpackEncodedStringToUnsignedChars(DFA4_minS);
			private static readonly char[] DFA4_max = DFA.UnpackEncodedStringToUnsignedChars(DFA4_maxS);
			private static readonly short[] DFA4_accept = DFA.UnpackEncodedString(DFA4_acceptS);
			private static readonly short[] DFA4_special = DFA.UnpackEncodedString(DFA4_specialS);
			private static readonly short[][] DFA4_transition;

			static DFA4()
			{
				int numStates = DFA4_transitionS.Length;
				DFA4_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA4_transition[i] = DFA.UnpackEncodedString(DFA4_transitionS[i]);
				}
			}

			public DFA4(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 4;
				this.eot = DFA4_eot;
				this.eof = DFA4_eof;
				this.min = DFA4_min;
				this.max = DFA4_max;
				this.accept = DFA4_accept;
				this.special = DFA4_special;
				this.transition = DFA4_transition;
			}

			public override string Description { get { return "149:20: ( QUERY PLAN )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA6 : DFA
		{
			private const string DFA6_eotS =
				"\x22\xFFFF";
			private const string DFA6_eofS =
				"\x22\xFFFF";
			private const string DFA6_minS =
				"\x1\x52\x10\xFFFF\x1\x94\x1\x95\x2\xFFFF\x1\x95\xC\xFFFF";
			private const string DFA6_maxS =
				"\x1\xA6\x10\xFFFF\x2\xAC\x2\xFFFF\x1\xAC\xC\xFFFF";
			private const string DFA6_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x1\x3\x1\x4\x1\x5\x1\x6\x1\x7\x1\x8\x1\xFFFF\x1" +
				"\x9\x1\xA\x1\xB\x1\xC\x1\xD\x1\xE\x1\xF\x2\xFFFF\x1\x13\x1\x10\x1\xFFFF" +
				"\x1\x11\x1\x14\x1\x16\x1\xFFFF\x1\x18\x1\x12\x1\x15\x1\x17\x1\x19\x3" +
				"\xFFFF";
			private const string DFA6_specialS =
				"\x22\xFFFF}>";
			private static readonly string[] DFA6_transitionS =
			{
				"\x1\xD\xE\xFFFF\x1\xE\x2\xFFFF\x1\x1\x1\x2\x1\xFFFF\x1\x3\x1\x4\x1"+
				"\x5\x1\x6\x1\x8\x9\xFFFF\x1\x7\xC\xFFFF\x1\x8\x3\xFFFF\x1\xA\x1\xFFFF"+
				"\x1\xB\x1\xC\x4\xFFFF\x1\xD\x1\xFFFF\x1\xF\x1\x10\x1\xFFFF\x1\x11\x11"+
				"\xFFFF\x1\x12\x1\x13",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x1\x14\x1\x16\x1\x15\x6\xFFFF\x1\x18\xC\xFFFF\x1\x17\x1\x18\x1\x1A",
				"\x1\x1B\x14\xFFFF\x1\x1C\x1\x1D\x1\x1E",
				"",
				"",
				"\x1\x16\x14\xFFFF\x1\x17\x1\xFFFF\x1\x1A",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA6_eot = DFA.UnpackEncodedString(DFA6_eotS);
			private static readonly short[] DFA6_eof = DFA.UnpackEncodedString(DFA6_eofS);
			private static readonly char[] DFA6_min = DFA.UnpackEncodedStringToUnsignedChars(DFA6_minS);
			private static readonly char[] DFA6_max = DFA.UnpackEncodedStringToUnsignedChars(DFA6_maxS);
			private static readonly short[] DFA6_accept = DFA.UnpackEncodedString(DFA6_acceptS);
			private static readonly short[] DFA6_special = DFA.UnpackEncodedString(DFA6_specialS);
			private static readonly short[][] DFA6_transition;

			static DFA6()
			{
				int numStates = DFA6_transitionS.Length;
				DFA6_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA6_transition[i] = DFA.UnpackEncodedString(DFA6_transitionS[i]);
				}
			}

			public DFA6(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 6;
				this.eot = DFA6_eot;
				this.eof = DFA6_eof;
				this.min = DFA6_min;
				this.max = DFA6_max;
				this.accept = DFA6_accept;
				this.special = DFA6_special;
				this.transition = DFA6_transition;
			}

			public override string Description { get { return "151:1: sql_stmt_core : ( pragma_stmt | attach_stmt | detach_stmt | analyze_stmt | reindex_stmt | vacuum_stmt | select_stmt | insert_stmt | update_stmt | delete_stmt | begin_stmt | commit_stmt | rollback_stmt | savepoint_stmt | release_stmt | create_virtual_table_stmt | create_table_stmt | drop_table_stmt | alter_table_stmt | create_view_stmt | drop_view_stmt | create_index_stmt | drop_index_stmt | create_trigger_stmt | drop_trigger_stmt );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA8 : DFA
		{
			private const string DFA8_eotS =
				"\x15\xFFFF";
			private const string DFA8_eofS =
				"\x1\xFFFF\x2\x4\x12\xFFFF";
			private const string DFA8_minS =
				"\x1\x21\x2\x20\x12\xFFFF";
			private const string DFA8_maxS =
				"\x1\xB3\x2\x87\x12\xFFFF";
			private const string DFA8_acceptS =
				"\x3\xFFFF\x1\x1\x1\x2\x10\xFFFF";
			private const string DFA8_specialS =
				"\x15\xFFFF}>";
			private static readonly string[] DFA8_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x4\x3\xFFFF\x1\x3\x1\x4\x1\xFFFF\x1\x4\x46\xFFFF\x2\x4\x7\xFFFF"+
				"\x1\x4\xF\xFFFF\x1\x4",
				"\x1\x4\x3\xFFFF\x1\x3\x1\x4\x1\xFFFF\x1\x4\x46\xFFFF\x2\x4\x7\xFFFF"+
				"\x1\x4\xF\xFFFF\x1\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA8_eot = DFA.UnpackEncodedString(DFA8_eotS);
			private static readonly short[] DFA8_eof = DFA.UnpackEncodedString(DFA8_eofS);
			private static readonly char[] DFA8_min = DFA.UnpackEncodedStringToUnsignedChars(DFA8_minS);
			private static readonly char[] DFA8_max = DFA.UnpackEncodedStringToUnsignedChars(DFA8_maxS);
			private static readonly short[] DFA8_accept = DFA.UnpackEncodedString(DFA8_acceptS);
			private static readonly short[] DFA8_special = DFA.UnpackEncodedString(DFA8_specialS);
			private static readonly short[][] DFA8_transition;

			static DFA8()
			{
				int numStates = DFA8_transitionS.Length;
				DFA8_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA8_transition[i] = DFA.UnpackEncodedString(DFA8_transitionS[i]);
				}
			}

			public DFA8(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 8;
				this.eot = DFA8_eot;
				this.eof = DFA8_eof;
				this.min = DFA8_min;
				this.max = DFA8_max;
				this.accept = DFA8_accept;
				this.special = DFA8_special;
				this.transition = DFA8_transition;
			}

			public override string Description { get { return "183:23: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA10 : DFA
		{
			private const string DFA10_eotS =
				"\x62\xFFFF";
			private const string DFA10_eofS =
				"\x1\x1\x1C\xFFFF\x1\x1\x44\xFFFF";
			private const string DFA10_minS =
				"\x1\x20\x1C\xFFFF\x1\x20\x2\xFFFF\x2\x21\x6\x24\x3A\xFFFF";
			private const string DFA10_maxS =
				"\x1\xB3\x1C\xFFFF\x1\xB3\x2\xFFFF\x2\xB3\x1\x26\x3\x75\x1\x26\x1\x55" +
				"\x3A\xFFFF";
			private const string DFA10_acceptS =
				"\x1\xFFFF\x1\x2\x29\xFFFF\x1\x1\x36\xFFFF";
			private const string DFA10_specialS =
				"\x62\xFFFF}>";
			private static readonly string[] DFA10_transitionS =
			{
				"\x4\x1\x1\xFFFF\x2\x1\x1\xFFFF\x1\x1D\x2\x1\x2\xFFFF\x2\x1\x2\xFFFF"+
				"\x3\x1\x17\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x1\x1\x3\x2B\x1\xFFFF\x6\x2B\x1\xFFFF\x1\x2B\x2\x1\x2\xFFFF\x3\x2B"+
				"\x10\xFFFF\x2\x2B\x4\xFFFF\x24\x2B\x1\x26\x1\x27\x1\x2B\x1\x23\x1\x2B"+
				"\x1\x24\x1\x25\x1\x2B\x1\x20\x1\x21\x1\x22\x3B\x2B",
				"",
				"",
				"\x3\x1\x1\x2B\x2\x1\x1\xFFFF\x3\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x17"+
				"\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x3\x1\x1\x2B\x6\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x10\xFFFF\x2\x1"+
				"\x4\xFFFF\x6A\x1",
				"\x1\x2B\x1\xFFFF\x1\x1",
				"\x1\x2B\x4D\xFFFF\x1\x1\x2\xFFFF\x1\x1",
				"\x1\x2B\x50\xFFFF\x1\x1",
				"\x1\x2B\x50\xFFFF\x1\x1",
				"\x1\x2B\x1\xFFFF\x1\x1",
				"\x1\x2B\x30\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA10_eot = DFA.UnpackEncodedString(DFA10_eotS);
			private static readonly short[] DFA10_eof = DFA.UnpackEncodedString(DFA10_eofS);
			private static readonly char[] DFA10_min = DFA.UnpackEncodedStringToUnsignedChars(DFA10_minS);
			private static readonly char[] DFA10_max = DFA.UnpackEncodedStringToUnsignedChars(DFA10_maxS);
			private static readonly short[] DFA10_accept = DFA.UnpackEncodedString(DFA10_acceptS);
			private static readonly short[] DFA10_special = DFA.UnpackEncodedString(DFA10_specialS);
			private static readonly short[][] DFA10_transition;

			static DFA10()
			{
				int numStates = DFA10_transitionS.Length;
				DFA10_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA10_transition[i] = DFA.UnpackEncodedString(DFA10_transitionS[i]);
				}
			}

			public DFA10(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 10;
				this.eot = DFA10_eot;
				this.eof = DFA10_eof;
				this.min = DFA10_min;
				this.max = DFA10_max;
				this.accept = DFA10_accept;
				this.special = DFA10_special;
				this.transition = DFA10_transition;
			}

			public override string Description { get { return "()* loopback of 185:18: ( OR or_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA11 : DFA
		{
			private const string DFA11_eotS =
				"\x63\xFFFF";
			private const string DFA11_eofS =
				"\x1\x1\x1D\xFFFF\x1\x1\x44\xFFFF";
			private const string DFA11_minS =
				"\x1\x20\x1D\xFFFF\x1\x20\x2\xFFFF\x2\x21\x6\x24\x3A\xFFFF";
			private const string DFA11_maxS =
				"\x1\xB3\x1D\xFFFF\x1\xB3\x2\xFFFF\x2\xB3\x1\x26\x3\x75\x1\x26\x1\x55" +
				"\x3A\xFFFF";
			private const string DFA11_acceptS =
				"\x1\xFFFF\x1\x2\x2A\xFFFF\x1\x1\x36\xFFFF";
			private const string DFA11_specialS =
				"\x63\xFFFF}>";
			private static readonly string[] DFA11_transitionS =
			{
				"\x4\x1\x1\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\x1E\x1\x1\x2\xFFFF\x2\x1\x2"+
				"\xFFFF\x3\x1\x17\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF"+
				"\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x1\x1\x3\x2C\x1\xFFFF\x6\x2C\x1\xFFFF\x1\x2C\x2\x1\x2\xFFFF\x3\x2C"+
				"\x10\xFFFF\x2\x2C\x4\xFFFF\x24\x2C\x1\x27\x1\x28\x1\x2C\x1\x24\x1\x2C"+
				"\x1\x25\x1\x26\x1\x2C\x1\x21\x1\x22\x1\x23\x3B\x2C",
				"",
				"",
				"\x3\x1\x1\x2C\x2\x1\x1\xFFFF\x3\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x17"+
				"\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x3\x1\x1\x2C\x6\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x10\xFFFF\x2\x1"+
				"\x4\xFFFF\x6A\x1",
				"\x1\x2C\x1\xFFFF\x1\x1",
				"\x1\x2C\x4D\xFFFF\x1\x1\x2\xFFFF\x1\x1",
				"\x1\x2C\x50\xFFFF\x1\x1",
				"\x1\x2C\x50\xFFFF\x1\x1",
				"\x1\x2C\x1\xFFFF\x1\x1",
				"\x1\x2C\x30\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA11_eot = DFA.UnpackEncodedString(DFA11_eotS);
			private static readonly short[] DFA11_eof = DFA.UnpackEncodedString(DFA11_eofS);
			private static readonly char[] DFA11_min = DFA.UnpackEncodedStringToUnsignedChars(DFA11_minS);
			private static readonly char[] DFA11_max = DFA.UnpackEncodedStringToUnsignedChars(DFA11_maxS);
			private static readonly short[] DFA11_accept = DFA.UnpackEncodedString(DFA11_acceptS);
			private static readonly short[] DFA11_special = DFA.UnpackEncodedString(DFA11_specialS);
			private static readonly short[][] DFA11_transition;

			static DFA11()
			{
				int numStates = DFA11_transitionS.Length;
				DFA11_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA11_transition[i] = DFA.UnpackEncodedString(DFA11_transitionS[i]);
				}
			}

			public DFA11(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 11;
				this.eot = DFA11_eot;
				this.eof = DFA11_eof;
				this.min = DFA11_min;
				this.max = DFA11_max;
				this.accept = DFA11_accept;
				this.special = DFA11_special;
				this.transition = DFA11_transition;
			}

			public override string Description { get { return "()* loopback of 187:25: ( AND and_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA12 : DFA
		{
			private const string DFA12_eotS =
				"\x79\xFFFF";
			private const string DFA12_eofS =
				"\x1\x9\x5\xFFFF\x2\x9\x71\xFFFF";
			private const string DFA12_minS =
				"\x1\x20\x5\xFFFF\x2\x20\x3E\xFFFF\x1\x21\x2\xFFFF\x1\x21\x6\x24\x29" +
				"\xFFFF";
			private const string DFA12_maxS =
				"\x1\xB3\x5\xFFFF\x1\x78\x1\xB3\x3E\xFFFF\x1\xB3\x2\xFFFF\x1\xB3\x1\x26" +
				"\x3\x75\x1\x26\x1\x55\x29\xFFFF";
			private const string DFA12_acceptS =
				"\x1\xFFFF\x1\x1\x7\xFFFF\x1\x2\x6F\xFFFF";
			private const string DFA12_specialS =
				"\x79\xFFFF}>";
			private static readonly string[] DFA12_transitionS =
			{
				"\x4\x9\x1\xFFFF\x2\x9\x1\x1\x3\x9\x1\x1\x1\xFFFF\x2\x9\x2\x1\x1\x6"+
				"\x1\x9\x1\x7\x8\x1\xF\xFFFF\xA\x9\x2\xFFFF\x1\x9\x1\xFFFF\x3\x9\x3\xFFFF"+
				"\x55\x9",
				"",
				"",
				"",
				"",
				"",
				"\x1\x9\x6\xFFFF\x1\x1\x5\xFFFF\x2\x9\x3\xFFFF\x1\x1\x3B\xFFFF\x2\x9"+
				"\x1\xFFFF\x1\x9\x1\xFFFF\x2\x9\x1\xFFFF\x3\x9",
				"\x1\x9\x3\x1\x1\xFFFF\x6\x1\x1\xFFFF\x1\x1\x2\x9\x2\xFFFF\x3\x1\x10"+
				"\xFFFF\x2\x1\x4\xFFFF\x24\x1\x1\x4E\x1\x4F\x1\x1\x1\x4B\x1\x1\x1\x4C"+
				"\x1\x4D\x1\x1\x1\x46\x1\x49\x1\x4A\x3B\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x9\x1\x1\x2\x9\x1\xFFFF\x3\x9\x1\xFFFF\x1\x9\x4\xFFFF\x3\x9\x17"+
				"\xFFFF\xA\x9\x2\xFFFF\x1\x9\x1\xFFFF\x3\x9\x3\xFFFF\x55\x9",
				"",
				"",
				"\x3\x9\x1\x1\x6\x9\x1\xFFFF\x1\x9\x4\xFFFF\x3\x9\x10\xFFFF\x2\x9\x4"+
				"\xFFFF\x6A\x9",
				"\x1\x1\x1\xFFFF\x1\x9",
				"\x1\x1\x4D\xFFFF\x1\x9\x2\xFFFF\x1\x9",
				"\x1\x1\x50\xFFFF\x1\x9",
				"\x1\x1\x50\xFFFF\x1\x9",
				"\x1\x1\x1\xFFFF\x1\x9",
				"\x1\x1\x30\xFFFF\x1\x9",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA12_eot = DFA.UnpackEncodedString(DFA12_eotS);
			private static readonly short[] DFA12_eof = DFA.UnpackEncodedString(DFA12_eofS);
			private static readonly char[] DFA12_min = DFA.UnpackEncodedStringToUnsignedChars(DFA12_minS);
			private static readonly char[] DFA12_max = DFA.UnpackEncodedStringToUnsignedChars(DFA12_maxS);
			private static readonly short[] DFA12_accept = DFA.UnpackEncodedString(DFA12_acceptS);
			private static readonly short[] DFA12_special = DFA.UnpackEncodedString(DFA12_specialS);
			private static readonly short[][] DFA12_transition;

			static DFA12()
			{
				int numStates = DFA12_transitionS.Length;
				DFA12_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA12_transition[i] = DFA.UnpackEncodedString(DFA12_transitionS[i]);
				}
			}

			public DFA12(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 12;
				this.eot = DFA12_eot;
				this.eof = DFA12_eof;
				this.min = DFA12_min;
				this.max = DFA12_max;
				this.accept = DFA12_accept;
				this.special = DFA12_special;
				this.transition = DFA12_transition;
			}

			public override string Description { get { return "189:34: ( cond_expr )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA22 : DFA
		{
			private const string DFA22_eotS =
				"\x13\xFFFF";
			private const string DFA22_eofS =
				"\x13\xFFFF";
			private const string DFA22_minS =
				"\x1\x27\x1\x2B\x1\xFFFF\x1\x21\x7\xFFFF\x1\x21\x7\xFFFF";
			private const string DFA22_maxS =
				"\x2\x3B\x1\xFFFF\x1\xB3\x7\xFFFF\x1\xB3\x7\xFFFF";
			private const string DFA22_acceptS =
				"\x2\xFFFF\x1\x1\x1\xFFFF\x1\x4\x2\xFFFF\x1\x5\x1\x6\x4\xFFFF\x1\x2\x1" +
				"\x3\x4\xFFFF";
			private const string DFA22_specialS =
				"\x13\xFFFF}>";
			private static readonly string[] DFA22_transitionS =
			{
				"\x1\x1\x3\xFFFF\x1\x3\x3\xFFFF\x3\x4\x1\xFFFF\x1\x7\x4\x8\x4\x2",
				"\x1\xB\x6\xFFFF\x1\x4\x1\x7\x4\xFFFF\x4\x2",
				"",
				"\x3\xE\x1\xFFFF\x2\xE\x1\xFFFF\x3\xE\x1\xFFFF\x1\xD\x4\xFFFF\x3\xE"+
				"\x17\xFFFF\xA\xE\x2\xFFFF\x1\xE\x1\xFFFF\x3\xE\x3\xFFFF\x55\xE",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\xE\x1\xFFFF\x2\xE\x1\xFFFF\x3\xE\x1\xFFFF\x1\xD\x4\xFFFF\x3\xE"+
				"\x17\xFFFF\xA\xE\x2\xFFFF\x1\xE\x1\xFFFF\x3\xE\x3\xFFFF\x55\xE",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA22_eot = DFA.UnpackEncodedString(DFA22_eotS);
			private static readonly short[] DFA22_eof = DFA.UnpackEncodedString(DFA22_eofS);
			private static readonly char[] DFA22_min = DFA.UnpackEncodedStringToUnsignedChars(DFA22_minS);
			private static readonly char[] DFA22_max = DFA.UnpackEncodedStringToUnsignedChars(DFA22_maxS);
			private static readonly short[] DFA22_accept = DFA.UnpackEncodedString(DFA22_acceptS);
			private static readonly short[] DFA22_special = DFA.UnpackEncodedString(DFA22_specialS);
			private static readonly short[][] DFA22_transition;

			static DFA22()
			{
				int numStates = DFA22_transitionS.Length;
				DFA22_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA22_transition[i] = DFA.UnpackEncodedString(DFA22_transitionS[i]);
				}
			}

			public DFA22(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 22;
				this.eot = DFA22_eot;
				this.eof = DFA22_eof;
				this.min = DFA22_min;
				this.max = DFA22_max;
				this.accept = DFA22_accept;
				this.special = DFA22_special;
				this.transition = DFA22_transition;
			}

			public override string Description { get { return "191:1: cond_expr : ( ( NOT )? match_op match_expr= eq_subexpr ( ESCAPE escape_expr= eq_subexpr )? -> ^( match_op $match_expr ( NOT )? ( ^( ESCAPE $escape_expr) )? ) | ( NOT )? IN LPAREN expr ( COMMA expr )* RPAREN -> ^( IN_VALUES ( NOT )? ^( IN ( expr )+ ) ) | ( NOT )? IN (database_name= id DOT )? table_name= id -> ^( IN_TABLE ( NOT )? ^( IN ^( $table_name ( $database_name)? ) ) ) | ( ISNULL -> IS_NULL | NOTNULL -> NOT_NULL | IS NULL -> IS_NULL | NOT NULL -> NOT_NULL | IS NOT NULL -> NOT_NULL ) | ( NOT )? BETWEEN e1= eq_subexpr AND e2= eq_subexpr -> ^( BETWEEN ( NOT )? ^( AND $e1 $e2) ) | ( ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr )+ );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA14 : DFA
		{
			private const string DFA14_eotS =
				"\x64\xFFFF";
			private const string DFA14_eofS =
				"\x2\x2\x62\xFFFF";
			private const string DFA14_minS =
				"\x2\x20\x2F\xFFFF\x1\x21\x2\xFFFF\x1\x21\x6\x24\x29\xFFFF";
			private const string DFA14_maxS =
				"\x2\xB3\x2F\xFFFF\x1\xB3\x2\xFFFF\x1\xB3\x1\x26\x3\x75\x1\x26\x1\x55" +
				"\x29\xFFFF";
			private const string DFA14_acceptS =
				"\x2\xFFFF\x1\x2\x1E\xFFFF\x1\x1\x42\xFFFF";
			private const string DFA14_specialS =
				"\x64\xFFFF}>";
			private static readonly string[] DFA14_transitionS =
			{
				"\x4\x2\x1\xFFFF\x2\x2\x1\xFFFF\x2\x2\x1\x1\x2\xFFFF\x2\x2\x2\xFFFF"+
				"\x3\x2\x17\xFFFF\xA\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x2\x3\x21\x1\xFFFF\x6\x21\x1\xFFFF\x1\x21\x2\x2\x2\xFFFF\x3\x21"+
				"\x10\xFFFF\x2\x21\x4\xFFFF\x24\x21\x1\x39\x1\x3A\x1\x21\x1\x36\x1\x21"+
				"\x1\x37\x1\x38\x1\x21\x1\x31\x1\x34\x1\x35\x3B\x21",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x2\x1\x21\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2\x4\xFFFF\x3\x2\x17"+
				"\xFFFF\xA\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"",
				"",
				"\x3\x2\x1\x21\x6\x2\x1\xFFFF\x1\x2\x4\xFFFF\x3\x2\x10\xFFFF\x2\x2"+
				"\x4\xFFFF\x6A\x2",
				"\x1\x21\x1\xFFFF\x1\x2",
				"\x1\x21\x4D\xFFFF\x1\x2\x2\xFFFF\x1\x2",
				"\x1\x21\x50\xFFFF\x1\x2",
				"\x1\x21\x50\xFFFF\x1\x2",
				"\x1\x21\x1\xFFFF\x1\x2",
				"\x1\x21\x30\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA14_eot = DFA.UnpackEncodedString(DFA14_eotS);
			private static readonly short[] DFA14_eof = DFA.UnpackEncodedString(DFA14_eofS);
			private static readonly char[] DFA14_min = DFA.UnpackEncodedStringToUnsignedChars(DFA14_minS);
			private static readonly char[] DFA14_max = DFA.UnpackEncodedStringToUnsignedChars(DFA14_maxS);
			private static readonly short[] DFA14_accept = DFA.UnpackEncodedString(DFA14_acceptS);
			private static readonly short[] DFA14_special = DFA.UnpackEncodedString(DFA14_specialS);
			private static readonly short[][] DFA14_transition;

			static DFA14()
			{
				int numStates = DFA14_transitionS.Length;
				DFA14_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA14_transition[i] = DFA.UnpackEncodedString(DFA14_transitionS[i]);
				}
			}

			public DFA14(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 14;
				this.eot = DFA14_eot;
				this.eof = DFA14_eof;
				this.min = DFA14_min;
				this.max = DFA14_max;
				this.accept = DFA14_accept;
				this.special = DFA14_special;
				this.transition = DFA14_transition;
			}

			public override string Description { get { return "192:41: ( ESCAPE escape_expr= eq_subexpr )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA18 : DFA
		{
			private const string DFA18_eotS =
				"\x43\xFFFF";
			private const string DFA18_eofS =
				"\x1\xFFFF\x2\x4\x40\xFFFF";
			private const string DFA18_minS =
				"\x1\x21\x2\x20\x40\xFFFF";
			private const string DFA18_maxS =
				"\x3\xB3\x40\xFFFF";
			private const string DFA18_acceptS =
				"\x3\xFFFF\x1\x1\x1\x2\x3E\xFFFF";
			private const string DFA18_specialS =
				"\x43\xFFFF}>";
			private static readonly string[] DFA18_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x4\x4\x1\x3\x2\x4\x1\xFFFF\x3\x4\x2\xFFFF\x2\x4\x2\xFFFF\x3\x4\x17"+
				"\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x4\x4\x1\x3\x2\x4\x1\xFFFF\x3\x4\x2\xFFFF\x2\x4\x2\xFFFF\x3\x4\x17"+
				"\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA18_eot = DFA.UnpackEncodedString(DFA18_eotS);
			private static readonly short[] DFA18_eof = DFA.UnpackEncodedString(DFA18_eofS);
			private static readonly char[] DFA18_min = DFA.UnpackEncodedStringToUnsignedChars(DFA18_minS);
			private static readonly char[] DFA18_max = DFA.UnpackEncodedStringToUnsignedChars(DFA18_maxS);
			private static readonly short[] DFA18_accept = DFA.UnpackEncodedString(DFA18_acceptS);
			private static readonly short[] DFA18_special = DFA.UnpackEncodedString(DFA18_specialS);
			private static readonly short[][] DFA18_transition;

			static DFA18()
			{
				int numStates = DFA18_transitionS.Length;
				DFA18_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA18_transition[i] = DFA.UnpackEncodedString(DFA18_transitionS[i]);
				}
			}

			public DFA18(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 18;
				this.eot = DFA18_eot;
				this.eof = DFA18_eof;
				this.min = DFA18_min;
				this.max = DFA18_max;
				this.accept = DFA18_accept;
				this.special = DFA18_special;
				this.transition = DFA18_transition;
			}

			public override string Description { get { return "194:13: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA21 : DFA
		{
			private const string DFA21_eotS =
				"\x21\xFFFF";
			private const string DFA21_eofS =
				"\x1\x1\x20\xFFFF";
			private const string DFA21_minS =
				"\x1\x20\x20\xFFFF";
			private const string DFA21_maxS =
				"\x1\xB3\x20\xFFFF";
			private const string DFA21_acceptS =
				"\x1\xFFFF\x1\x2\x1E\xFFFF\x1\x1";
			private const string DFA21_specialS =
				"\x21\xFFFF}>";
			private static readonly string[] DFA21_transitionS =
			{
				"\x4\x1\x1\xFFFF\x2\x1\x1\xFFFF\x3\x1\x2\xFFFF\x2\x1\x2\xFFFF\x3\x1"+
				"\x4\x20\x13\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA21_eot = DFA.UnpackEncodedString(DFA21_eotS);
			private static readonly short[] DFA21_eof = DFA.UnpackEncodedString(DFA21_eofS);
			private static readonly char[] DFA21_min = DFA.UnpackEncodedStringToUnsignedChars(DFA21_minS);
			private static readonly char[] DFA21_max = DFA.UnpackEncodedStringToUnsignedChars(DFA21_maxS);
			private static readonly short[] DFA21_accept = DFA.UnpackEncodedString(DFA21_acceptS);
			private static readonly short[] DFA21_special = DFA.UnpackEncodedString(DFA21_specialS);
			private static readonly short[][] DFA21_transition;

			static DFA21()
			{
				int numStates = DFA21_transitionS.Length;
				DFA21_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA21_transition[i] = DFA.UnpackEncodedString(DFA21_transitionS[i]);
				}
			}

			public DFA21(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 21;
				this.eot = DFA21_eot;
				this.eof = DFA21_eof;
				this.min = DFA21_min;
				this.max = DFA21_max;
				this.accept = DFA21_accept;
				this.special = DFA21_special;
				this.transition = DFA21_transition;
			}

			public override string Description { get { return "()+ loopback of 199:5: ( ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 ) eq_subexpr )+"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA23 : DFA
		{
			private const string DFA23_eotS =
				"\x2A\xFFFF";
			private const string DFA23_eofS =
				"\x1\x1\x29\xFFFF";
			private const string DFA23_minS =
				"\x1\x20\x29\xFFFF";
			private const string DFA23_maxS =
				"\x1\xB3\x29\xFFFF";
			private const string DFA23_acceptS =
				"\x1\xFFFF\x1\x2\x27\xFFFF\x1\x1";
			private const string DFA23_specialS =
				"\x2A\xFFFF}>";
			private static readonly string[] DFA23_transitionS =
			{
				"\x4\x1\x1\xFFFF\x7\x1\x1\xFFFF\xF\x1\x4\x29\xB\xFFFF\xA\x1\x2\xFFFF"+
				"\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA23_eot = DFA.UnpackEncodedString(DFA23_eotS);
			private static readonly short[] DFA23_eof = DFA.UnpackEncodedString(DFA23_eofS);
			private static readonly char[] DFA23_min = DFA.UnpackEncodedStringToUnsignedChars(DFA23_minS);
			private static readonly char[] DFA23_max = DFA.UnpackEncodedStringToUnsignedChars(DFA23_maxS);
			private static readonly short[] DFA23_accept = DFA.UnpackEncodedString(DFA23_acceptS);
			private static readonly short[] DFA23_special = DFA.UnpackEncodedString(DFA23_specialS);
			private static readonly short[][] DFA23_transition;

			static DFA23()
			{
				int numStates = DFA23_transitionS.Length;
				DFA23_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA23_transition[i] = DFA.UnpackEncodedString(DFA23_transitionS[i]);
				}
			}

			public DFA23(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 23;
				this.eot = DFA23_eot;
				this.eof = DFA23_eof;
				this.min = DFA23_min;
				this.max = DFA23_max;
				this.accept = DFA23_accept;
				this.special = DFA23_special;
				this.transition = DFA23_transition;
			}

			public override string Description { get { return "()* loopback of 204:25: ( ( LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ ) neq_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA24 : DFA
		{
			private const string DFA24_eotS =
				"\x2B\xFFFF";
			private const string DFA24_eofS =
				"\x1\x1\x2A\xFFFF";
			private const string DFA24_minS =
				"\x1\x20\x2A\xFFFF";
			private const string DFA24_maxS =
				"\x1\xB3\x2A\xFFFF";
			private const string DFA24_acceptS =
				"\x1\xFFFF\x1\x2\x28\xFFFF\x1\x1";
			private const string DFA24_specialS =
				"\x2B\xFFFF}>";
			private static readonly string[] DFA24_transitionS =
			{
				"\x4\x1\x1\xFFFF\x7\x1\x1\xFFFF\x13\x1\x4\x2A\x7\xFFFF\xA\x1\x2\xFFFF"+
				"\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA24_eot = DFA.UnpackEncodedString(DFA24_eotS);
			private static readonly short[] DFA24_eof = DFA.UnpackEncodedString(DFA24_eofS);
			private static readonly char[] DFA24_min = DFA.UnpackEncodedStringToUnsignedChars(DFA24_minS);
			private static readonly char[] DFA24_max = DFA.UnpackEncodedStringToUnsignedChars(DFA24_maxS);
			private static readonly short[] DFA24_accept = DFA.UnpackEncodedString(DFA24_acceptS);
			private static readonly short[] DFA24_special = DFA.UnpackEncodedString(DFA24_specialS);
			private static readonly short[][] DFA24_transition;

			static DFA24()
			{
				int numStates = DFA24_transitionS.Length;
				DFA24_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA24_transition[i] = DFA.UnpackEncodedString(DFA24_transitionS[i]);
				}
			}

			public DFA24(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 24;
				this.eot = DFA24_eot;
				this.eof = DFA24_eof;
				this.min = DFA24_min;
				this.max = DFA24_max;
				this.accept = DFA24_accept;
				this.special = DFA24_special;
				this.transition = DFA24_transition;
			}

			public override string Description { get { return "()* loopback of 206:26: ( ( SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE ) bit_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA25 : DFA
		{
			private const string DFA25_eotS =
				"\x2C\xFFFF";
			private const string DFA25_eofS =
				"\x1\x1\x2B\xFFFF";
			private const string DFA25_minS =
				"\x1\x20\x2B\xFFFF";
			private const string DFA25_maxS =
				"\x1\xB3\x2B\xFFFF";
			private const string DFA25_acceptS =
				"\x1\xFFFF\x1\x2\x29\xFFFF\x1\x1";
			private const string DFA25_specialS =
				"\x2C\xFFFF}>";
			private static readonly string[] DFA25_transitionS =
			{
				"\x4\x1\x1\xFFFF\x7\x1\x1\xFFFF\x17\x1\x2\x2B\x5\xFFFF\xA\x1\x2\xFFFF"+
				"\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA25_eot = DFA.UnpackEncodedString(DFA25_eotS);
			private static readonly short[] DFA25_eof = DFA.UnpackEncodedString(DFA25_eofS);
			private static readonly char[] DFA25_min = DFA.UnpackEncodedStringToUnsignedChars(DFA25_minS);
			private static readonly char[] DFA25_max = DFA.UnpackEncodedStringToUnsignedChars(DFA25_maxS);
			private static readonly short[] DFA25_accept = DFA.UnpackEncodedString(DFA25_acceptS);
			private static readonly short[] DFA25_special = DFA.UnpackEncodedString(DFA25_specialS);
			private static readonly short[][] DFA25_transition;

			static DFA25()
			{
				int numStates = DFA25_transitionS.Length;
				DFA25_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA25_transition[i] = DFA.UnpackEncodedString(DFA25_transitionS[i]);
				}
			}

			public DFA25(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 25;
				this.eot = DFA25_eot;
				this.eof = DFA25_eof;
				this.min = DFA25_min;
				this.max = DFA25_max;
				this.accept = DFA25_accept;
				this.special = DFA25_special;
				this.transition = DFA25_transition;
			}

			public override string Description { get { return "()* loopback of 208:26: ( ( PLUS | MINUS ) add_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA26 : DFA
		{
			private const string DFA26_eotS =
				"\x2D\xFFFF";
			private const string DFA26_eofS =
				"\x1\x1\x2C\xFFFF";
			private const string DFA26_minS =
				"\x1\x20\x2C\xFFFF";
			private const string DFA26_maxS =
				"\x1\xB3\x2C\xFFFF";
			private const string DFA26_acceptS =
				"\x1\xFFFF\x1\x2\x2A\xFFFF\x1\x1";
			private const string DFA26_specialS =
				"\x2D\xFFFF}>";
			private static readonly string[] DFA26_transitionS =
			{
				"\x4\x1\x1\xFFFF\x7\x1\x1\xFFFF\x19\x1\x3\x2C\x2\xFFFF\xA\x1\x2\xFFFF"+
				"\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA26_eot = DFA.UnpackEncodedString(DFA26_eotS);
			private static readonly short[] DFA26_eof = DFA.UnpackEncodedString(DFA26_eofS);
			private static readonly char[] DFA26_min = DFA.UnpackEncodedStringToUnsignedChars(DFA26_minS);
			private static readonly char[] DFA26_max = DFA.UnpackEncodedStringToUnsignedChars(DFA26_maxS);
			private static readonly short[] DFA26_accept = DFA.UnpackEncodedString(DFA26_acceptS);
			private static readonly short[] DFA26_special = DFA.UnpackEncodedString(DFA26_specialS);
			private static readonly short[][] DFA26_transition;

			static DFA26()
			{
				int numStates = DFA26_transitionS.Length;
				DFA26_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA26_transition[i] = DFA.UnpackEncodedString(DFA26_transitionS[i]);
				}
			}

			public DFA26(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 26;
				this.eot = DFA26_eot;
				this.eof = DFA26_eof;
				this.min = DFA26_min;
				this.max = DFA26_max;
				this.accept = DFA26_accept;
				this.special = DFA26_special;
				this.transition = DFA26_transition;
			}

			public override string Description { get { return "()* loopback of 210:26: ( ( ASTERISK | SLASH | PERCENT ) mul_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA27 : DFA
		{
			private const string DFA27_eotS =
				"\x2E\xFFFF";
			private const string DFA27_eofS =
				"\x1\x1\x2D\xFFFF";
			private const string DFA27_minS =
				"\x1\x20\x2D\xFFFF";
			private const string DFA27_maxS =
				"\x1\xB3\x2D\xFFFF";
			private const string DFA27_acceptS =
				"\x1\xFFFF\x1\x2\x2B\xFFFF\x1\x1";
			private const string DFA27_specialS =
				"\x2E\xFFFF}>";
			private static readonly string[] DFA27_transitionS =
			{
				"\x4\x1\x1\xFFFF\x7\x1\x1\xFFFF\x1C\x1\x1\x2D\x1\xFFFF\xA\x1\x2\xFFFF"+
				"\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA27_eot = DFA.UnpackEncodedString(DFA27_eotS);
			private static readonly short[] DFA27_eof = DFA.UnpackEncodedString(DFA27_eofS);
			private static readonly char[] DFA27_min = DFA.UnpackEncodedStringToUnsignedChars(DFA27_minS);
			private static readonly char[] DFA27_max = DFA.UnpackEncodedStringToUnsignedChars(DFA27_maxS);
			private static readonly short[] DFA27_accept = DFA.UnpackEncodedString(DFA27_acceptS);
			private static readonly short[] DFA27_special = DFA.UnpackEncodedString(DFA27_specialS);
			private static readonly short[][] DFA27_transition;

			static DFA27()
			{
				int numStates = DFA27_transitionS.Length;
				DFA27_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA27_transition[i] = DFA.UnpackEncodedString(DFA27_transitionS[i]);
				}
			}

			public DFA27(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 27;
				this.eot = DFA27_eot;
				this.eof = DFA27_eof;
				this.min = DFA27_min;
				this.max = DFA27_max;
				this.accept = DFA27_accept;
				this.special = DFA27_special;
				this.transition = DFA27_transition;
			}

			public override string Description { get { return "()* loopback of 212:26: ( DOUBLE_PIPE con_subexpr )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA28 : DFA
		{
			private const string DFA28_eotS =
				"\x13\xFFFF";
			private const string DFA28_eofS =
				"\x13\xFFFF";
			private const string DFA28_minS =
				"\x1\x21\x12\xFFFF";
			private const string DFA28_maxS =
				"\x1\xB3\x12\xFFFF";
			private const string DFA28_acceptS =
				"\x1\xFFFF\x1\x1\x10\xFFFF\x1\x2";
			private const string DFA28_specialS =
				"\x13\xFFFF}>";
			private static readonly string[] DFA28_transitionS =
			{
				"\x3\x1\x1\xFFFF\x2\x1\x1\x12\x3\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x10"+
				"\xFFFF\x2\x12\x4\xFFFF\x1\x12\x69\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA28_eot = DFA.UnpackEncodedString(DFA28_eotS);
			private static readonly short[] DFA28_eof = DFA.UnpackEncodedString(DFA28_eofS);
			private static readonly char[] DFA28_min = DFA.UnpackEncodedStringToUnsignedChars(DFA28_minS);
			private static readonly char[] DFA28_max = DFA.UnpackEncodedStringToUnsignedChars(DFA28_maxS);
			private static readonly short[] DFA28_accept = DFA.UnpackEncodedString(DFA28_acceptS);
			private static readonly short[] DFA28_special = DFA.UnpackEncodedString(DFA28_specialS);
			private static readonly short[][] DFA28_transition;

			static DFA28()
			{
				int numStates = DFA28_transitionS.Length;
				DFA28_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA28_transition[i] = DFA.UnpackEncodedString(DFA28_transitionS[i]);
				}
			}

			public DFA28(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 28;
				this.eot = DFA28_eot;
				this.eof = DFA28_eof;
				this.min = DFA28_min;
				this.max = DFA28_max;
				this.accept = DFA28_accept;
				this.special = DFA28_special;
				this.transition = DFA28_transition;
			}

			public override string Description { get { return "214:1: con_subexpr : ( unary_subexpr | unary_op unary_subexpr -> ^( unary_op unary_subexpr ) );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA29 : DFA
		{
			private const string DFA29_eotS =
				"\x3C\xFFFF";
			private const string DFA29_eofS =
				"\x2\x2\x3A\xFFFF";
			private const string DFA29_minS =
				"\x2\x20\x3A\xFFFF";
			private const string DFA29_maxS =
				"\x1\xB3\x1\x78\x3A\xFFFF";
			private const string DFA29_acceptS =
				"\x2\xFFFF\x1\x2\x2C\xFFFF\x1\x1\xC\xFFFF";
			private const string DFA29_specialS =
				"\x3C\xFFFF}>";
			private static readonly string[] DFA29_transitionS =
			{
				"\x4\x2\x1\xFFFF\x7\x2\x1\xFFFF\x1D\x2\x1\xFFFF\x1\x1\x9\x2\x2\xFFFF"+
				"\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x2\xC\xFFFF\x2\x2\x1D\xFFFF\x1\x2F\x21\xFFFF\x2\x2\x1\xFFFF\x1"+
				"\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA29_eot = DFA.UnpackEncodedString(DFA29_eotS);
			private static readonly short[] DFA29_eof = DFA.UnpackEncodedString(DFA29_eofS);
			private static readonly char[] DFA29_min = DFA.UnpackEncodedStringToUnsignedChars(DFA29_minS);
			private static readonly char[] DFA29_max = DFA.UnpackEncodedStringToUnsignedChars(DFA29_maxS);
			private static readonly short[] DFA29_accept = DFA.UnpackEncodedString(DFA29_acceptS);
			private static readonly short[] DFA29_special = DFA.UnpackEncodedString(DFA29_specialS);
			private static readonly short[][] DFA29_transition;

			static DFA29()
			{
				int numStates = DFA29_transitionS.Length;
				DFA29_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA29_transition[i] = DFA.UnpackEncodedString(DFA29_transitionS[i]);
				}
			}

			public DFA29(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 29;
				this.eot = DFA29_eot;
				this.eof = DFA29_eof;
				this.min = DFA29_min;
				this.max = DFA29_max;
				this.accept = DFA29_accept;
				this.special = DFA29_special;
				this.transition = DFA29_transition;
			}

			public override string Description { get { return "218:26: ( COLLATE collation_name= ID )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA38 : DFA
		{
			private const string DFA38_eotS =
				"\x145\xFFFF";
			private const string DFA38_eofS =
				"\x3\xFFFF\x1\x1\x1\xFFFF\x4\x1\x3\xFFFF\x1\x11\x138\xFFFF";
			private const string DFA38_minS =
				"\x1\x21\x2\xFFFF\x1\x20\x1\xFFFF\x4\x20\x3\xFFFF\x1\x20\x1\x24\x1\xFFFF" +
				"\x1\x21\x1\x24\x134\xFFFF";
			private const string DFA38_maxS =
				"\x1\xB3\x2\xFFFF\x1\xB3\x1\xFFFF\x4\xB3\x3\xFFFF\x1\xB3\x1\x2C\x1\xFFFF" +
				"\x1\xB3\x1\x2C\x134\xFFFF";
			private const string DFA38_acceptS =
				"\x1\xFFFF\x1\x1\x7\xFFFF\x1\x2\x4\xFFFF\x1\x5\x2\xFFFF\x1\x3\xEB\xFFFF" +
				"\x1\x4\x2F\xFFFF\x1\x6\x2\xFFFF\x1\x7\x12\xFFFF\x1\x8\x1\xFFFF";
			private const string DFA38_specialS =
				"\x145\xFFFF}>";
			private static readonly string[] DFA38_transitionS =
			{
				"\x3\x11\x1\xFFFF\x2\x11\x1\xFFFF\x3\x11\x1\xFFFF\x1\xE\x4\xFFFF\x1"+
				"\x11\x1\x5\x1\x11\x17\xFFFF\x1\x11\x1\xC\x1\x11\x1\xD\x1\x11\x1\xF\x4"+
				"\x11\x2\x1\x1\x3\x1\x1\x1\x6\x1\x7\x1\x8\x3\x9\x1\x10\x54\x11",
				"",
				"",
				"\x4\x1\x1\x11\x7\x1\x1\xFFFF\x1D\x1\x1\xFFFF\xA\x1\x2\xFFFF\x1\x1"+
				"\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"\x4\x1\x1\x11\x7\x1\x1\xFFFF\x1D\x1\x1\xFFFF\xA\x1\x2\xFFFF\x1\x1"+
				"\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x4\x1\x1\x11\x7\x1\x1\xFFFF\x1D\x1\x1\xFFFF\xA\x1\x2\xFFFF\x1\x1"+
				"\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x4\x1\x1\x11\x7\x1\x1\xFFFF\x1D\x1\x1\xFFFF\xA\x1\x2\xFFFF\x1\x1"+
				"\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x4\x1\x1\x11\x7\x1\x1\xFFFF\x1D\x1\x1\xFFFF\xA\x1\x2\xFFFF\x1\x1"+
				"\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"\xC\x11\x1\xFD\x1D\x11\x1\xFFFF\xA\x11\x2\xFFFF\x1\x11\x1\xFFFF\x3"+
				"\x11\x3\xFFFF\x55\x11",
				"\x1\x11\x7\xFFFF\x1\x12D",
				"",
				"\x3\x130\x1\x11\x6\x130\x1\xFFFF\x1\x130\x4\xFFFF\x3\x130\x10\xFFFF"+
				"\x2\x130\x4\xFFFF\x6A\x130",
				"\x1\x11\x7\xFFFF\x1\x143",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA38_eot = DFA.UnpackEncodedString(DFA38_eotS);
			private static readonly short[] DFA38_eof = DFA.UnpackEncodedString(DFA38_eofS);
			private static readonly char[] DFA38_min = DFA.UnpackEncodedStringToUnsignedChars(DFA38_minS);
			private static readonly char[] DFA38_max = DFA.UnpackEncodedStringToUnsignedChars(DFA38_maxS);
			private static readonly short[] DFA38_accept = DFA.UnpackEncodedString(DFA38_acceptS);
			private static readonly short[] DFA38_special = DFA.UnpackEncodedString(DFA38_specialS);
			private static readonly short[][] DFA38_transition;

			static DFA38()
			{
				int numStates = DFA38_transitionS.Length;
				DFA38_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA38_transition[i] = DFA.UnpackEncodedString(DFA38_transitionS[i]);
				}
			}

			public DFA38(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 38;
				this.eot = DFA38_eot;
				this.eof = DFA38_eof;
				this.min = DFA38_min;
				this.max = DFA38_max;
				this.accept = DFA38_accept;
				this.special = DFA38_special;
				this.transition = DFA38_transition;
			}

			public override string Description { get { return "220:1: atom_expr : ( literal_value | bind_parameter | ( (database_name= id DOT )? table_name= id DOT )? column_name= ID -> ^( COLUMN_EXPRESSION ^( $column_name ( ^( $table_name ( $database_name)? ) )? ) ) | name= ID LPAREN ( ( DISTINCT )? args+= expr ( COMMA args+= expr )* | ASTERISK )? RPAREN -> ^( FUNCTION_EXPRESSION $name ( DISTINCT )? ( $args)* ( ASTERISK )? ) | LPAREN expr RPAREN | CAST LPAREN expr AS type_name RPAREN | CASE (case_expr= expr )? ( when_expr )+ ( ELSE else_expr= expr )? END -> ^( CASE ( $case_expr)? ( when_expr )+ ( $else_expr)? ) | raise_function );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA31 : DFA
		{
			private const string DFA31_eotS =
				"\x33\xFFFF";
			private const string DFA31_eofS =
				"\x1\xFFFF\x1\x5\x31\xFFFF";
			private const string DFA31_minS =
				"\x1\x21\x1\x20\x31\xFFFF";
			private const string DFA31_maxS =
				"\x2\xB3\x31\xFFFF";
			private const string DFA31_acceptS =
				"\x2\xFFFF\x1\x1\x2\xFFFF\x1\x2\x2D\xFFFF";
			private const string DFA31_specialS =
				"\x33\xFFFF}>";
			private static readonly string[] DFA31_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x4\x5\x1\x2\x7\x5\x1\xFFFF\x1D\x5\x1\xFFFF\xA\x5\x2\xFFFF\x1\x5\x1"+
				"\xFFFF\x3\x5\x3\xFFFF\x55\x5",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA31_eot = DFA.UnpackEncodedString(DFA31_eotS);
			private static readonly short[] DFA31_eof = DFA.UnpackEncodedString(DFA31_eofS);
			private static readonly char[] DFA31_min = DFA.UnpackEncodedStringToUnsignedChars(DFA31_minS);
			private static readonly char[] DFA31_max = DFA.UnpackEncodedStringToUnsignedChars(DFA31_maxS);
			private static readonly short[] DFA31_accept = DFA.UnpackEncodedString(DFA31_acceptS);
			private static readonly short[] DFA31_special = DFA.UnpackEncodedString(DFA31_specialS);
			private static readonly short[][] DFA31_transition;

			static DFA31()
			{
				int numStates = DFA31_transitionS.Length;
				DFA31_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA31_transition[i] = DFA.UnpackEncodedString(DFA31_transitionS[i]);
				}
			}

			public DFA31(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 31;
				this.eot = DFA31_eot;
				this.eof = DFA31_eof;
				this.min = DFA31_min;
				this.max = DFA31_max;
				this.accept = DFA31_accept;
				this.special = DFA31_special;
				this.transition = DFA31_transition;
			}

			public override string Description { get { return "223:5: ( (database_name= id DOT )? table_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA30 : DFA
		{
			private const string DFA30_eotS =
				"\x69\xFFFF";
			private const string DFA30_eofS =
				"\x5\xFFFF\x1\xC\x2\xFFFF\x1\xC\x60\xFFFF";
			private const string DFA30_minS =
				"\x1\x21\x2\x24\x2\x21\x1\x20\x2\xFFFF\x1\x20\x60\xFFFF";
			private const string DFA30_maxS =
				"\x1\xB3\x2\x24\x3\xB3\x2\xFFFF\x1\xB3\x60\xFFFF";
			private const string DFA30_acceptS =
				"\x6\xFFFF\x1\x1\x5\xFFFF\x1\x2\x5C\xFFFF";
			private const string DFA30_specialS =
				"\x69\xFFFF}>";
			private static readonly string[] DFA30_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x3",
				"\x1\x4",
				"\x3\x6\x1\xFFFF\x2\x6\x1\xFFFF\x3\x6\x6\xFFFF\x3\x6\x17\xFFFF\x1\x6"+
				"\x1\x5\x8\x6\x2\xFFFF\x1\x6\x1\xFFFF\x3\x6\x3\xFFFF\x55\x6",
				"\x3\x6\x1\xFFFF\x2\x6\x1\xFFFF\x3\x6\x6\xFFFF\x3\x6\x17\xFFFF\x1\x6"+
				"\x1\x8\x8\x6\x2\xFFFF\x1\x6\x1\xFFFF\x3\x6\x3\xFFFF\x55\x6",
				"\x4\xC\x1\x6\x7\xC\x1\xFFFF\x1D\xC\x1\xFFFF\xA\xC\x2\xFFFF\x1\xC\x1"+
				"\xFFFF\x3\xC\x3\xFFFF\x55\xC",
				"",
				"",
				"\x4\xC\x1\x6\x7\xC\x1\xFFFF\x1D\xC\x1\xFFFF\xA\xC\x2\xFFFF\x1\xC\x1"+
				"\xFFFF\x3\xC\x3\xFFFF\x55\xC",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA30_eot = DFA.UnpackEncodedString(DFA30_eotS);
			private static readonly short[] DFA30_eof = DFA.UnpackEncodedString(DFA30_eofS);
			private static readonly char[] DFA30_min = DFA.UnpackEncodedStringToUnsignedChars(DFA30_minS);
			private static readonly char[] DFA30_max = DFA.UnpackEncodedStringToUnsignedChars(DFA30_maxS);
			private static readonly short[] DFA30_accept = DFA.UnpackEncodedString(DFA30_acceptS);
			private static readonly short[] DFA30_special = DFA.UnpackEncodedString(DFA30_specialS);
			private static readonly short[][] DFA30_transition;

			static DFA30()
			{
				int numStates = DFA30_transitionS.Length;
				DFA30_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA30_transition[i] = DFA.UnpackEncodedString(DFA30_transitionS[i]);
				}
			}

			public DFA30(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 30;
				this.eot = DFA30_eot;
				this.eof = DFA30_eof;
				this.min = DFA30_min;
				this.max = DFA30_max;
				this.accept = DFA30_accept;
				this.special = DFA30_special;
				this.transition = DFA30_transition;
			}

			public override string Description { get { return "223:6: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA34 : DFA
		{
			private const string DFA34_eotS =
				"\x16\xFFFF";
			private const string DFA34_eofS =
				"\x16\xFFFF";
			private const string DFA34_minS =
				"\x1\x21\x15\xFFFF";
			private const string DFA34_maxS =
				"\x1\xB3\x15\xFFFF";
			private const string DFA34_acceptS =
				"\x1\xFFFF\x1\x1\x12\xFFFF\x1\x2\x1\x3";
			private const string DFA34_specialS =
				"\x16\xFFFF}>";
			private static readonly string[] DFA34_transitionS =
			{
				"\x3\x1\x1\xFFFF\x6\x1\x1\xFFFF\x1\x1\x1\xFFFF\x1\x15\x2\xFFFF\x3\x1"+
				"\x10\xFFFF\x2\x1\x1\x14\x3\xFFFF\x6A\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA34_eot = DFA.UnpackEncodedString(DFA34_eotS);
			private static readonly short[] DFA34_eof = DFA.UnpackEncodedString(DFA34_eofS);
			private static readonly char[] DFA34_min = DFA.UnpackEncodedStringToUnsignedChars(DFA34_minS);
			private static readonly char[] DFA34_max = DFA.UnpackEncodedStringToUnsignedChars(DFA34_maxS);
			private static readonly short[] DFA34_accept = DFA.UnpackEncodedString(DFA34_acceptS);
			private static readonly short[] DFA34_special = DFA.UnpackEncodedString(DFA34_specialS);
			private static readonly short[][] DFA34_transition;

			static DFA34()
			{
				int numStates = DFA34_transitionS.Length;
				DFA34_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA34_transition[i] = DFA.UnpackEncodedString(DFA34_transitionS[i]);
				}
			}

			public DFA34(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 34;
				this.eot = DFA34_eot;
				this.eof = DFA34_eof;
				this.min = DFA34_min;
				this.max = DFA34_max;
				this.accept = DFA34_accept;
				this.special = DFA34_special;
				this.transition = DFA34_transition;
			}

			public override string Description { get { return "224:20: ( ( DISTINCT )? args+= expr ( COMMA args+= expr )* | ASTERISK )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA32 : DFA
		{
			private const string DFA32_eotS =
				"\x27\xFFFF";
			private const string DFA32_eofS =
				"\x27\xFFFF";
			private const string DFA32_minS =
				"\x2\x21\x25\xFFFF";
			private const string DFA32_maxS =
				"\x2\xB3\x25\xFFFF";
			private const string DFA32_acceptS =
				"\x2\xFFFF\x1\x2\x11\xFFFF\x1\x1\x12\xFFFF";
			private const string DFA32_specialS =
				"\x27\xFFFF}>";
			private static readonly string[] DFA32_transitionS =
			{
				"\x3\x2\x1\xFFFF\x6\x2\x1\xFFFF\x1\x2\x4\xFFFF\x3\x2\x10\xFFFF\x2\x2"+
				"\x4\xFFFF\x3\x2\x1\x1\x66\x2",
				"\x3\x14\x1\x2\x6\x14\x1\xFFFF\x1\x14\x4\xFFFF\x3\x14\x10\xFFFF\x2"+
				"\x14\x4\xFFFF\x6A\x14",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA32_eot = DFA.UnpackEncodedString(DFA32_eotS);
			private static readonly short[] DFA32_eof = DFA.UnpackEncodedString(DFA32_eofS);
			private static readonly char[] DFA32_min = DFA.UnpackEncodedStringToUnsignedChars(DFA32_minS);
			private static readonly char[] DFA32_max = DFA.UnpackEncodedStringToUnsignedChars(DFA32_maxS);
			private static readonly short[] DFA32_accept = DFA.UnpackEncodedString(DFA32_acceptS);
			private static readonly short[] DFA32_special = DFA.UnpackEncodedString(DFA32_specialS);
			private static readonly short[][] DFA32_transition;

			static DFA32()
			{
				int numStates = DFA32_transitionS.Length;
				DFA32_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA32_transition[i] = DFA.UnpackEncodedString(DFA32_transitionS[i]);
				}
			}

			public DFA32(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 32;
				this.eot = DFA32_eot;
				this.eof = DFA32_eof;
				this.min = DFA32_min;
				this.max = DFA32_max;
				this.accept = DFA32_accept;
				this.special = DFA32_special;
				this.transition = DFA32_transition;
			}

			public override string Description { get { return "224:21: ( DISTINCT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA35 : DFA
		{
			private const string DFA35_eotS =
				"\x27\xFFFF";
			private const string DFA35_eofS =
				"\x27\xFFFF";
			private const string DFA35_minS =
				"\x1\x21\x10\xFFFF\x1\x21\x15\xFFFF";
			private const string DFA35_maxS =
				"\x1\xB3\x10\xFFFF\x1\xB3\x15\xFFFF";
			private const string DFA35_acceptS =
				"\x1\xFFFF\x1\x1\x13\xFFFF\x1\x2\x11\xFFFF";
			private const string DFA35_specialS =
				"\x27\xFFFF}>";
			private static readonly string[] DFA35_transitionS =
			{
				"\x3\x1\x1\xFFFF\x6\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x10\xFFFF\x2\x1"+
				"\x4\xFFFF\x9\x1\x1\x11\x60\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x15\x1\x1\x6\x15\x1\xFFFF\x1\x15\x4\xFFFF\x3\x15\x10\xFFFF\x2"+
				"\x15\x4\xFFFF\x6A\x15",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA35_eot = DFA.UnpackEncodedString(DFA35_eotS);
			private static readonly short[] DFA35_eof = DFA.UnpackEncodedString(DFA35_eofS);
			private static readonly char[] DFA35_min = DFA.UnpackEncodedStringToUnsignedChars(DFA35_minS);
			private static readonly char[] DFA35_max = DFA.UnpackEncodedStringToUnsignedChars(DFA35_maxS);
			private static readonly short[] DFA35_accept = DFA.UnpackEncodedString(DFA35_acceptS);
			private static readonly short[] DFA35_special = DFA.UnpackEncodedString(DFA35_specialS);
			private static readonly short[][] DFA35_transition;

			static DFA35()
			{
				int numStates = DFA35_transitionS.Length;
				DFA35_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA35_transition[i] = DFA.UnpackEncodedString(DFA35_transitionS[i]);
				}
			}

			public DFA35(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 35;
				this.eot = DFA35_eot;
				this.eof = DFA35_eof;
				this.min = DFA35_min;
				this.max = DFA35_max;
				this.accept = DFA35_accept;
				this.special = DFA35_special;
				this.transition = DFA35_transition;
			}

			public override string Description { get { return "229:10: (case_expr= expr )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA40 : DFA
		{
			private const string DFA40_eotS =
				"\x33\xFFFF";
			private const string DFA40_eofS =
				"\x1\xFFFF\x1\x5\x31\xFFFF";
			private const string DFA40_minS =
				"\x1\x5C\x1\x20\x31\xFFFF";
			private const string DFA40_maxS =
				"\x1\x5E\x1\xB3\x31\xFFFF";
			private const string DFA40_acceptS =
				"\x2\xFFFF\x1\x3\x1\x4\x1\x2\x1\x1\x2D\xFFFF";
			private const string DFA40_specialS =
				"\x33\xFFFF}>";
			private static readonly string[] DFA40_transitionS =
			{
				"\x1\x1\x1\x2\x1\x3",
				"\x4\x5\x1\xFFFF\x7\x5\x1\xFFFF\x1D\x5\x1\xFFFF\xA\x5\x1\x4\x1\xFFFF"+
				"\x1\x5\x1\xFFFF\x3\x5\x3\xFFFF\x55\x5",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA40_eot = DFA.UnpackEncodedString(DFA40_eotS);
			private static readonly short[] DFA40_eof = DFA.UnpackEncodedString(DFA40_eofS);
			private static readonly char[] DFA40_min = DFA.UnpackEncodedStringToUnsignedChars(DFA40_minS);
			private static readonly char[] DFA40_max = DFA.UnpackEncodedStringToUnsignedChars(DFA40_maxS);
			private static readonly short[] DFA40_accept = DFA.UnpackEncodedString(DFA40_acceptS);
			private static readonly short[] DFA40_special = DFA.UnpackEncodedString(DFA40_specialS);
			private static readonly short[][] DFA40_transition;

			static DFA40()
			{
				int numStates = DFA40_transitionS.Length;
				DFA40_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA40_transition[i] = DFA.UnpackEncodedString(DFA40_transitionS[i]);
				}
			}

			public DFA40(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 40;
				this.eot = DFA40_eot;
				this.eof = DFA40_eof;
				this.min = DFA40_min;
				this.max = DFA40_max;
				this.accept = DFA40_accept;
				this.special = DFA40_special;
				this.transition = DFA40_transition;
			}

			public override string Description { get { return "246:1: bind_parameter : ( QUESTION -> BIND | QUESTION position= INTEGER -> ^( BIND $position) | COLON name= id -> ^( BIND_NAME $name) | AT name= id -> ^( BIND_NAME $name) );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA42 : DFA
		{
			private const string DFA42_eotS =
				"\x11\xFFFF";
			private const string DFA42_eofS =
				"\x1\x1\x10\xFFFF";
			private const string DFA42_minS =
				"\x1\x20\x10\xFFFF";
			private const string DFA42_maxS =
				"\x1\xA0\x10\xFFFF";
			private const string DFA42_acceptS =
				"\x1\xFFFF\x1\x2\xE\xFFFF\x1\x1";
			private const string DFA42_specialS =
				"\x11\xFFFF}>";
			private static readonly string[] DFA42_transitionS =
			{
				"\x1\x1\x6\xFFFF\x1\x1\x4\xFFFF\x3\x1\x3\xFFFF\x1\x1\x18\xFFFF\x1\x1"+
				"\x1\x10\x38\xFFFF\x1\x1\x13\xFFFF\x2\x1\x1\xFFFF\x3\x1\x1\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA42_eot = DFA.UnpackEncodedString(DFA42_eotS);
			private static readonly short[] DFA42_eof = DFA.UnpackEncodedString(DFA42_eofS);
			private static readonly char[] DFA42_min = DFA.UnpackEncodedStringToUnsignedChars(DFA42_minS);
			private static readonly char[] DFA42_max = DFA.UnpackEncodedStringToUnsignedChars(DFA42_maxS);
			private static readonly short[] DFA42_accept = DFA.UnpackEncodedString(DFA42_acceptS);
			private static readonly short[] DFA42_special = DFA.UnpackEncodedString(DFA42_specialS);
			private static readonly short[][] DFA42_transition;

			static DFA42()
			{
				int numStates = DFA42_transitionS.Length;
				DFA42_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA42_transition[i] = DFA.UnpackEncodedString(DFA42_transitionS[i]);
				}
			}

			public DFA42(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 42;
				this.eot = DFA42_eot;
				this.eof = DFA42_eof;
				this.min = DFA42_min;
				this.max = DFA42_max;
				this.accept = DFA42_accept;
				this.special = DFA42_special;
				this.transition = DFA42_transition;
			}

			public override string Description { get { return "()+ loopback of 257:17: (names+= ID )+"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA44 : DFA
		{
			private const string DFA44_eotS =
				"\x10\xFFFF";
			private const string DFA44_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA44_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA44_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA44_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA44_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA44_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x4\xFFFF\x1\x1\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF"+
				"\x1\x2\x39\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA44_eot = DFA.UnpackEncodedString(DFA44_eotS);
			private static readonly short[] DFA44_eof = DFA.UnpackEncodedString(DFA44_eofS);
			private static readonly char[] DFA44_min = DFA.UnpackEncodedStringToUnsignedChars(DFA44_minS);
			private static readonly char[] DFA44_max = DFA.UnpackEncodedStringToUnsignedChars(DFA44_maxS);
			private static readonly short[] DFA44_accept = DFA.UnpackEncodedString(DFA44_acceptS);
			private static readonly short[] DFA44_special = DFA.UnpackEncodedString(DFA44_specialS);
			private static readonly short[][] DFA44_transition;

			static DFA44()
			{
				int numStates = DFA44_transitionS.Length;
				DFA44_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA44_transition[i] = DFA.UnpackEncodedString(DFA44_transitionS[i]);
				}
			}

			public DFA44(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 44;
				this.eot = DFA44_eot;
				this.eof = DFA44_eof;
				this.min = DFA44_min;
				this.max = DFA44_max;
				this.accept = DFA44_accept;
				this.special = DFA44_special;
				this.transition = DFA44_transition;
			}

			public override string Description { get { return "257:23: ( LPAREN size1= signed_number ( COMMA size2= signed_number )? RPAREN )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA46 : DFA
		{
			private const string DFA46_eotS =
				"\xD\xFFFF";
			private const string DFA46_eofS =
				"\x1\xFFFF\x2\x4\xA\xFFFF";
			private const string DFA46_minS =
				"\x1\x21\x2\x20\xA\xFFFF";
			private const string DFA46_maxS =
				"\x1\xB3\x2\x34\xA\xFFFF";
			private const string DFA46_acceptS =
				"\x3\xFFFF\x1\x1\x1\x2\x8\xFFFF";
			private const string DFA46_specialS =
				"\xD\xFFFF}>";
			private static readonly string[] DFA46_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x4\x3\xFFFF\x1\x3\x7\xFFFF\x1\x4\x7\xFFFF\x1\x4",
				"\x1\x4\x3\xFFFF\x1\x3\x7\xFFFF\x1\x4\x7\xFFFF\x1\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA46_eot = DFA.UnpackEncodedString(DFA46_eotS);
			private static readonly short[] DFA46_eof = DFA.UnpackEncodedString(DFA46_eofS);
			private static readonly char[] DFA46_min = DFA.UnpackEncodedStringToUnsignedChars(DFA46_minS);
			private static readonly char[] DFA46_max = DFA.UnpackEncodedStringToUnsignedChars(DFA46_maxS);
			private static readonly short[] DFA46_accept = DFA.UnpackEncodedString(DFA46_acceptS);
			private static readonly short[] DFA46_special = DFA.UnpackEncodedString(DFA46_specialS);
			private static readonly short[][] DFA46_transition;

			static DFA46()
			{
				int numStates = DFA46_transitionS.Length;
				DFA46_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA46_transition[i] = DFA.UnpackEncodedString(DFA46_transitionS[i]);
				}
			}

			public DFA46(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 46;
				this.eot = DFA46_eot;
				this.eof = DFA46_eof;
				this.min = DFA46_min;
				this.max = DFA46_max;
				this.accept = DFA46_accept;
				this.special = DFA46_special;
				this.transition = DFA46_transition;
			}

			public override string Description { get { return "263:21: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA49 : DFA
		{
			private const string DFA49_eotS =
				"\xE\xFFFF";
			private const string DFA49_eofS =
				"\x7\xFFFF\x1\x2\x6\xFFFF";
			private const string DFA49_minS =
				"\x2\x21\x3\xFFFF\x1\x21\x1\xFFFF\x1\x20\x6\xFFFF";
			private const string DFA49_maxS =
				"\x2\xB3\x3\xFFFF\x1\xB3\x1\xFFFF\x1\xB3\x6\xFFFF";
			private const string DFA49_acceptS =
				"\x2\xFFFF\x1\x2\x1\xFFFF\x1\x1\x9\xFFFF";
			private const string DFA49_specialS =
				"\xE\xFFFF}>";
			private static readonly string[] DFA49_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\xA\x2"+
				"\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x7\x2\x1\x1\x4D\x2",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x17\xFFFF\x4\x4"+
				"\x1\x5\x5\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x4\x2"+
				"\x1\x7\x5\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"",
				"\x1\x2\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x17\xFFFF"+
				"\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA49_eot = DFA.UnpackEncodedString(DFA49_eotS);
			private static readonly short[] DFA49_eof = DFA.UnpackEncodedString(DFA49_eofS);
			private static readonly char[] DFA49_min = DFA.UnpackEncodedStringToUnsignedChars(DFA49_minS);
			private static readonly char[] DFA49_max = DFA.UnpackEncodedStringToUnsignedChars(DFA49_maxS);
			private static readonly short[] DFA49_accept = DFA.UnpackEncodedString(DFA49_acceptS);
			private static readonly short[] DFA49_special = DFA.UnpackEncodedString(DFA49_specialS);
			private static readonly short[][] DFA49_transition;

			static DFA49()
			{
				int numStates = DFA49_transitionS.Length;
				DFA49_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA49_transition[i] = DFA.UnpackEncodedString(DFA49_transitionS[i]);
				}
			}

			public DFA49(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 49;
				this.eot = DFA49_eot;
				this.eof = DFA49_eof;
				this.min = DFA49_min;
				this.max = DFA49_max;
				this.accept = DFA49_accept;
				this.special = DFA49_special;
				this.transition = DFA49_transition;
			}

			public override string Description { get { return "273:21: ( DATABASE )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA51 : DFA
		{
			private const string DFA51_eotS =
				"\xB\xFFFF";
			private const string DFA51_eofS =
				"\x1\x3\x2\x5\x8\xFFFF";
			private const string DFA51_minS =
				"\x3\x20\x8\xFFFF";
			private const string DFA51_maxS =
				"\x1\xB3\x2\x24\x8\xFFFF";
			private const string DFA51_acceptS =
				"\x3\xFFFF\x1\x3\x1\xFFFF\x1\x1\x1\xFFFF\x1\x2\x3\xFFFF";
			private const string DFA51_specialS =
				"\xB\xFFFF}>";
			private static readonly string[] DFA51_transitionS =
			{
				"\x1\x3\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF"+
				"\x1\x2\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x5\x3\xFFFF\x1\x7",
				"\x1\x5\x3\xFFFF\x1\x7",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA51_eot = DFA.UnpackEncodedString(DFA51_eotS);
			private static readonly short[] DFA51_eof = DFA.UnpackEncodedString(DFA51_eofS);
			private static readonly char[] DFA51_min = DFA.UnpackEncodedStringToUnsignedChars(DFA51_minS);
			private static readonly char[] DFA51_max = DFA.UnpackEncodedStringToUnsignedChars(DFA51_maxS);
			private static readonly short[] DFA51_accept = DFA.UnpackEncodedString(DFA51_acceptS);
			private static readonly short[] DFA51_special = DFA.UnpackEncodedString(DFA51_specialS);
			private static readonly short[][] DFA51_transition;

			static DFA51()
			{
				int numStates = DFA51_transitionS.Length;
				DFA51_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA51_transition[i] = DFA.UnpackEncodedString(DFA51_transitionS[i]);
				}
			}

			public DFA51(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 51;
				this.eot = DFA51_eot;
				this.eof = DFA51_eof;
				this.min = DFA51_min;
				this.max = DFA51_max;
				this.accept = DFA51_accept;
				this.special = DFA51_special;
				this.transition = DFA51_transition;
			}

			public override string Description { get { return "279:23: (database_or_table_name= id | database_name= id DOT table_name= id )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA53 : DFA
		{
			private const string DFA53_eotS =
				"\xD\xFFFF";
			private const string DFA53_eofS =
				"\x1\x3\xC\xFFFF";
			private const string DFA53_minS =
				"\x1\x20\xC\xFFFF";
			private const string DFA53_maxS =
				"\x1\x79\xC\xFFFF";
			private const string DFA53_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x1\x3\x9\xFFFF";
			private const string DFA53_specialS =
				"\xD\xFFFF}>";
			private static readonly string[] DFA53_transitionS =
			{
				"\x1\x3\xC\xFFFF\x2\x3\x3D\xFFFF\x1\x1\x1\x2\x2\x3\x1\xFFFF\x1\x3\x1"+
				"\xFFFF\x2\x3\x4\xFFFF\x1\x3",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA53_eot = DFA.UnpackEncodedString(DFA53_eotS);
			private static readonly short[] DFA53_eof = DFA.UnpackEncodedString(DFA53_eofS);
			private static readonly char[] DFA53_min = DFA.UnpackEncodedStringToUnsignedChars(DFA53_minS);
			private static readonly char[] DFA53_max = DFA.UnpackEncodedStringToUnsignedChars(DFA53_maxS);
			private static readonly short[] DFA53_accept = DFA.UnpackEncodedString(DFA53_acceptS);
			private static readonly short[] DFA53_special = DFA.UnpackEncodedString(DFA53_specialS);
			private static readonly short[][] DFA53_transition;

			static DFA53()
			{
				int numStates = DFA53_transitionS.Length;
				DFA53_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA53_transition[i] = DFA.UnpackEncodedString(DFA53_transitionS[i]);
				}
			}

			public DFA53(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 53;
				this.eot = DFA53_eot;
				this.eof = DFA53_eof;
				this.min = DFA53_min;
				this.max = DFA53_max;
				this.accept = DFA53_accept;
				this.special = DFA53_special;
				this.transition = DFA53_transition;
			}

			public override string Description { get { return "293:82: ( ASC | DESC )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA65 : DFA
		{
			private const string DFA65_eotS =
				"\x3E\xFFFF";
			private const string DFA65_eofS =
				"\x3E\xFFFF";
			private const string DFA65_minS =
				"\x3\x21\x3B\xFFFF";
			private const string DFA65_maxS =
				"\x3\xB3\x3B\xFFFF";
			private const string DFA65_acceptS =
				"\x3\xFFFF\x1\x3\x12\xFFFF\x1\x1\x13\xFFFF\x1\x2\x13\xFFFF";
			private const string DFA65_specialS =
				"\x3E\xFFFF}>";
			private static readonly string[] DFA65_transitionS =
			{
				"\x3\x3\x1\xFFFF\x6\x3\x1\xFFFF\x1\x3\x4\xFFFF\x3\x3\x10\xFFFF\x3\x3"+
				"\x3\xFFFF\x3\x3\x1\x2\x24\x3\x1\x1\x41\x3",
				"\x3\x16\x1\x3\x6\x16\x1\xFFFF\x1\x16\x4\xFFFF\x3\x16\x10\xFFFF\x3"+
				"\x16\x3\xFFFF\x6A\x16",
				"\x3\x2A\x1\x3\x6\x2A\x1\xFFFF\x1\x2A\x4\xFFFF\x3\x2A\x10\xFFFF\x3"+
				"\x2A\x3\xFFFF\x6A\x2A",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA65_eot = DFA.UnpackEncodedString(DFA65_eotS);
			private static readonly short[] DFA65_eof = DFA.UnpackEncodedString(DFA65_eofS);
			private static readonly char[] DFA65_min = DFA.UnpackEncodedStringToUnsignedChars(DFA65_minS);
			private static readonly char[] DFA65_max = DFA.UnpackEncodedStringToUnsignedChars(DFA65_maxS);
			private static readonly short[] DFA65_accept = DFA.UnpackEncodedString(DFA65_acceptS);
			private static readonly short[] DFA65_special = DFA.UnpackEncodedString(DFA65_specialS);
			private static readonly short[][] DFA65_transition;

			static DFA65()
			{
				int numStates = DFA65_transitionS.Length;
				DFA65_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA65_transition[i] = DFA.UnpackEncodedString(DFA65_transitionS[i]);
				}
			}

			public DFA65(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 65;
				this.eot = DFA65_eot;
				this.eof = DFA65_eof;
				this.min = DFA65_min;
				this.max = DFA65_max;
				this.accept = DFA65_accept;
				this.special = DFA65_special;
				this.transition = DFA65_transition;
			}

			public override string Description { get { return "314:10: ( ALL | DISTINCT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA66 : DFA
		{
			private const string DFA66_eotS =
				"\xD\xFFFF";
			private const string DFA66_eofS =
				"\x1\x1\xC\xFFFF";
			private const string DFA66_minS =
				"\x1\x20\xC\xFFFF";
			private const string DFA66_maxS =
				"\x1\x78\xC\xFFFF";
			private const string DFA66_acceptS =
				"\x1\xFFFF\x1\x2\xA\xFFFF\x1\x1";
			private const string DFA66_specialS =
				"\xD\xFFFF}>";
			private static readonly string[] DFA66_transitionS =
			{
				"\x1\x1\xC\xFFFF\x1\xC\x1\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\xFFFF"+
				"\x2\x1\x1\xFFFF\x3\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA66_eot = DFA.UnpackEncodedString(DFA66_eotS);
			private static readonly short[] DFA66_eof = DFA.UnpackEncodedString(DFA66_eofS);
			private static readonly char[] DFA66_min = DFA.UnpackEncodedStringToUnsignedChars(DFA66_minS);
			private static readonly char[] DFA66_max = DFA.UnpackEncodedStringToUnsignedChars(DFA66_maxS);
			private static readonly short[] DFA66_accept = DFA.UnpackEncodedString(DFA66_acceptS);
			private static readonly short[] DFA66_special = DFA.UnpackEncodedString(DFA66_specialS);
			private static readonly short[][] DFA66_transition;

			static DFA66()
			{
				int numStates = DFA66_transitionS.Length;
				DFA66_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA66_transition[i] = DFA.UnpackEncodedString(DFA66_transitionS[i]);
				}
			}

			public DFA66(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 66;
				this.eot = DFA66_eot;
				this.eof = DFA66_eof;
				this.min = DFA66_min;
				this.max = DFA66_max;
				this.accept = DFA66_accept;
				this.special = DFA66_special;
				this.transition = DFA66_transition;
			}

			public override string Description { get { return "()* loopback of 314:42: ( COMMA result_column )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA67 : DFA
		{
			private const string DFA67_eotS =
				"\xC\xFFFF";
			private const string DFA67_eofS =
				"\x1\x2\xB\xFFFF";
			private const string DFA67_minS =
				"\x1\x20\xB\xFFFF";
			private const string DFA67_maxS =
				"\x1\x78\xB\xFFFF";
			private const string DFA67_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x9\xFFFF";
			private const string DFA67_specialS =
				"\xC\xFFFF}>";
			private static readonly string[] DFA67_transitionS =
			{
				"\x1\x2\xD\xFFFF\x1\x2\x3F\xFFFF\x2\x2\x1\xFFFF\x1\x2\x1\xFFFF\x2\x2"+
				"\x1\xFFFF\x1\x1\x2\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA67_eot = DFA.UnpackEncodedString(DFA67_eotS);
			private static readonly short[] DFA67_eof = DFA.UnpackEncodedString(DFA67_eofS);
			private static readonly char[] DFA67_min = DFA.UnpackEncodedStringToUnsignedChars(DFA67_minS);
			private static readonly char[] DFA67_max = DFA.UnpackEncodedStringToUnsignedChars(DFA67_maxS);
			private static readonly short[] DFA67_accept = DFA.UnpackEncodedString(DFA67_acceptS);
			private static readonly short[] DFA67_special = DFA.UnpackEncodedString(DFA67_specialS);
			private static readonly short[][] DFA67_transition;

			static DFA67()
			{
				int numStates = DFA67_transitionS.Length;
				DFA67_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA67_transition[i] = DFA.UnpackEncodedString(DFA67_transitionS[i]);
				}
			}

			public DFA67(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 67;
				this.eot = DFA67_eot;
				this.eof = DFA67_eof;
				this.min = DFA67_min;
				this.max = DFA67_max;
				this.accept = DFA67_accept;
				this.special = DFA67_special;
				this.transition = DFA67_transition;
			}

			public override string Description { get { return "314:65: ( FROM join_source )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA68 : DFA
		{
			private const string DFA68_eotS =
				"\xB\xFFFF";
			private const string DFA68_eofS =
				"\x1\x2\xA\xFFFF";
			private const string DFA68_minS =
				"\x1\x20\xA\xFFFF";
			private const string DFA68_maxS =
				"\x1\x78\xA\xFFFF";
			private const string DFA68_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x8\xFFFF";
			private const string DFA68_specialS =
				"\xB\xFFFF}>";
			private static readonly string[] DFA68_transitionS =
			{
				"\x1\x2\xD\xFFFF\x1\x2\x3F\xFFFF\x2\x2\x1\xFFFF\x1\x2\x1\xFFFF\x2\x2"+
				"\x2\xFFFF\x1\x1\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA68_eot = DFA.UnpackEncodedString(DFA68_eotS);
			private static readonly short[] DFA68_eof = DFA.UnpackEncodedString(DFA68_eofS);
			private static readonly char[] DFA68_min = DFA.UnpackEncodedStringToUnsignedChars(DFA68_minS);
			private static readonly char[] DFA68_max = DFA.UnpackEncodedStringToUnsignedChars(DFA68_maxS);
			private static readonly short[] DFA68_accept = DFA.UnpackEncodedString(DFA68_acceptS);
			private static readonly short[] DFA68_special = DFA.UnpackEncodedString(DFA68_specialS);
			private static readonly short[][] DFA68_transition;

			static DFA68()
			{
				int numStates = DFA68_transitionS.Length;
				DFA68_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA68_transition[i] = DFA.UnpackEncodedString(DFA68_transitionS[i]);
				}
			}

			public DFA68(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 68;
				this.eot = DFA68_eot;
				this.eof = DFA68_eof;
				this.min = DFA68_min;
				this.max = DFA68_max;
				this.accept = DFA68_accept;
				this.special = DFA68_special;
				this.transition = DFA68_transition;
			}

			public override string Description { get { return "314:85: ( WHERE where_expr= expr )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA71 : DFA
		{
			private const string DFA71_eotS =
				"\xA\xFFFF";
			private const string DFA71_eofS =
				"\x1\x2\x9\xFFFF";
			private const string DFA71_minS =
				"\x1\x20\x9\xFFFF";
			private const string DFA71_maxS =
				"\x1\x78\x9\xFFFF";
			private const string DFA71_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x7\xFFFF";
			private const string DFA71_specialS =
				"\xA\xFFFF}>";
			private static readonly string[] DFA71_transitionS =
			{
				"\x1\x2\xD\xFFFF\x1\x2\x3F\xFFFF\x2\x2\x1\xFFFF\x1\x2\x1\xFFFF\x2\x2"+
				"\x3\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA71_eot = DFA.UnpackEncodedString(DFA71_eotS);
			private static readonly short[] DFA71_eof = DFA.UnpackEncodedString(DFA71_eofS);
			private static readonly char[] DFA71_min = DFA.UnpackEncodedStringToUnsignedChars(DFA71_minS);
			private static readonly char[] DFA71_max = DFA.UnpackEncodedStringToUnsignedChars(DFA71_maxS);
			private static readonly short[] DFA71_accept = DFA.UnpackEncodedString(DFA71_acceptS);
			private static readonly short[] DFA71_special = DFA.UnpackEncodedString(DFA71_specialS);
			private static readonly short[][] DFA71_transition;

			static DFA71()
			{
				int numStates = DFA71_transitionS.Length;
				DFA71_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA71_transition[i] = DFA.UnpackEncodedString(DFA71_transitionS[i]);
				}
			}

			public DFA71(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 71;
				this.eot = DFA71_eot;
				this.eof = DFA71_eof;
				this.min = DFA71_min;
				this.max = DFA71_max;
				this.accept = DFA71_accept;
				this.special = DFA71_special;
				this.transition = DFA71_transition;
			}

			public override string Description { get { return "315:3: ( GROUP BY ordering_term ( COMMA ordering_term )* ( HAVING having_expr= expr )? )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA69 : DFA
		{
			private const string DFA69_eotS =
				"\xB\xFFFF";
			private const string DFA69_eofS =
				"\x1\x1\xA\xFFFF";
			private const string DFA69_minS =
				"\x1\x20\xA\xFFFF";
			private const string DFA69_maxS =
				"\x1\x79\xA\xFFFF";
			private const string DFA69_acceptS =
				"\x1\xFFFF\x1\x2\x8\xFFFF\x1\x1";
			private const string DFA69_specialS =
				"\xB\xFFFF}>";
			private static readonly string[] DFA69_transitionS =
			{
				"\x1\x1\xC\xFFFF\x1\xA\x1\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\xFFFF"+
				"\x2\x1\x4\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA69_eot = DFA.UnpackEncodedString(DFA69_eotS);
			private static readonly short[] DFA69_eof = DFA.UnpackEncodedString(DFA69_eofS);
			private static readonly char[] DFA69_min = DFA.UnpackEncodedStringToUnsignedChars(DFA69_minS);
			private static readonly char[] DFA69_max = DFA.UnpackEncodedStringToUnsignedChars(DFA69_maxS);
			private static readonly short[] DFA69_accept = DFA.UnpackEncodedString(DFA69_acceptS);
			private static readonly short[] DFA69_special = DFA.UnpackEncodedString(DFA69_specialS);
			private static readonly short[][] DFA69_transition;

			static DFA69()
			{
				int numStates = DFA69_transitionS.Length;
				DFA69_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA69_transition[i] = DFA.UnpackEncodedString(DFA69_transitionS[i]);
				}
			}

			public DFA69(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 69;
				this.eot = DFA69_eot;
				this.eof = DFA69_eof;
				this.min = DFA69_min;
				this.max = DFA69_max;
				this.accept = DFA69_accept;
				this.special = DFA69_special;
				this.transition = DFA69_transition;
			}

			public override string Description { get { return "()* loopback of 315:28: ( COMMA ordering_term )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA70 : DFA
		{
			private const string DFA70_eotS =
				"\xA\xFFFF";
			private const string DFA70_eofS =
				"\x1\x2\x9\xFFFF";
			private const string DFA70_minS =
				"\x1\x20\x9\xFFFF";
			private const string DFA70_maxS =
				"\x1\x79\x9\xFFFF";
			private const string DFA70_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x7\xFFFF";
			private const string DFA70_specialS =
				"\xA\xFFFF}>";
			private static readonly string[] DFA70_transitionS =
			{
				"\x1\x2\xD\xFFFF\x1\x2\x3F\xFFFF\x2\x2\x1\xFFFF\x1\x2\x1\xFFFF\x2\x2"+
				"\x4\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA70_eot = DFA.UnpackEncodedString(DFA70_eotS);
			private static readonly short[] DFA70_eof = DFA.UnpackEncodedString(DFA70_eofS);
			private static readonly char[] DFA70_min = DFA.UnpackEncodedStringToUnsignedChars(DFA70_minS);
			private static readonly char[] DFA70_max = DFA.UnpackEncodedStringToUnsignedChars(DFA70_maxS);
			private static readonly short[] DFA70_accept = DFA.UnpackEncodedString(DFA70_acceptS);
			private static readonly short[] DFA70_special = DFA.UnpackEncodedString(DFA70_specialS);
			private static readonly short[][] DFA70_transition;

			static DFA70()
			{
				int numStates = DFA70_transitionS.Length;
				DFA70_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA70_transition[i] = DFA.UnpackEncodedString(DFA70_transitionS[i]);
				}
			}

			public DFA70(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 70;
				this.eot = DFA70_eot;
				this.eof = DFA70_eof;
				this.min = DFA70_min;
				this.max = DFA70_max;
				this.accept = DFA70_accept;
				this.special = DFA70_special;
				this.transition = DFA70_transition;
			}

			public override string Description { get { return "315:51: ( HAVING having_expr= expr )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA74 : DFA
		{
			private const string DFA74_eotS =
				"\x116\xFFFF";
			private const string DFA74_eofS =
				"\x2\xFFFF\x2\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x10B\xFFFF";
			private const string DFA74_minS =
				"\x1\x21\x1\xFFFF\x2\x20\x2\xFFFF\x1\x20\x1\xFFFF\x3\x20\x1\x24\x4\xFFFF" +
				"\x1\x21\x2\x24\x1\xFFFF\x1\x21\x1F\xFFFF\x1\x21\x20\xFFFF\x1\x21\x1F" +
				"\xFFFF\x1\x21\x1F\xFFFF\x1\x21\x1F\xFFFF\x1\x21\x20\xFFFF\x2\x21\x14" +
				"\xFFFF\x2\x21\x28\xFFFF";
			private const string DFA74_maxS =
				"\x1\xB3\x1\xFFFF\x2\xB3\x2\xFFFF\x1\xB3\x1\xFFFF\x3\xB3\x1\x2C\x4\xFFFF" +
				"\x1\xB3\x1\x2C\x1\x24\x1\xFFFF\x1\xB3\x1F\xFFFF\x1\xB3\x20\xFFFF\x1\xB3" +
				"\x1F\xFFFF\x1\xB3\x1F\xFFFF\x1\xB3\x1F\xFFFF\x1\xB3\x20\xFFFF\x2\xB3" +
				"\x14\xFFFF\x2\xB3\x28\xFFFF";
			private const string DFA74_acceptS =
				"\x1\xFFFF\x1\x1\x2\xFFFF\x1\x3\xE9\xFFFF\x1\x2\x27\xFFFF";
			private const string DFA74_specialS =
				"\x116\xFFFF}>";
			private static readonly string[] DFA74_transitionS =
			{
				"\x3\x12\x1\xFFFF\x2\x12\x1\x4\x3\x12\x1\xFFFF\x1\x4\x4\xFFFF\x1\x12"+
				"\x1\x3\x1\x12\x10\xFFFF\x2\x4\x1\x1\x3\xFFFF\x1\x4\x1\x12\x1\x6\x1\x12"+
				"\x1\xB\x1\x12\x1\x10\x4\x12\x2\x4\x1\x2\x1\x4\x1\x8\x1\x9\x1\xA\x3\x4"+
				"\x1\x11\x54\x12",
				"",
				"\x4\x4\x1\x14\x7\x4\x1\xFFFF\x1D\x4\x1\xFFFF\xA\x4\x2\xFFFF\x1\x4"+
				"\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x4\x4\x1\x34\x7\x4\x1\xFFFF\x1D\x4\x1\xFFFF\xA\x4\x2\xFFFF\x1\x4"+
				"\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"\x4\x4\x1\x55\x25\x4\x1\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4"+
				"\x3\xFFFF\x55\x4",
				"",
				"\x4\x4\x1\x75\x7\x4\x1\xFFFF\x1D\x4\x1\xFFFF\xA\x4\x2\xFFFF\x1\x4"+
				"\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x4\x4\x1\x95\x7\x4\x1\xFFFF\x1D\x4\x1\xFFFF\xA\x4\x2\xFFFF\x1\x4"+
				"\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x4\x4\x1\xB5\x7\x4\x1\xFFFF\x1D\x4\x1\xFFFF\xA\x4\x2\xFFFF\x1\x4"+
				"\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x1\xD6\x7\xFFFF\x1\x4",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xD7\x6\x4\x1\xFFFF\x1\x4\x4\xFFFF\x3\x4\x10\xFFFF\x2\x4"+
				"\x4\xFFFF\x6A\x4",
				"\x1\xEC\x7\xFFFF\x1\x4",
				"\x1\xED",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x6\xFFFF\x3\x4\x12\xFFFF\x1\xEE"+
				"\x4\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA74_eot = DFA.UnpackEncodedString(DFA74_eotS);
			private static readonly short[] DFA74_eof = DFA.UnpackEncodedString(DFA74_eofS);
			private static readonly char[] DFA74_min = DFA.UnpackEncodedStringToUnsignedChars(DFA74_minS);
			private static readonly char[] DFA74_max = DFA.UnpackEncodedStringToUnsignedChars(DFA74_maxS);
			private static readonly short[] DFA74_accept = DFA.UnpackEncodedString(DFA74_acceptS);
			private static readonly short[] DFA74_special = DFA.UnpackEncodedString(DFA74_specialS);
			private static readonly short[][] DFA74_transition;

			static DFA74()
			{
				int numStates = DFA74_transitionS.Length;
				DFA74_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA74_transition[i] = DFA.UnpackEncodedString(DFA74_transitionS[i]);
				}
			}

			public DFA74(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 74;
				this.eot = DFA74_eot;
				this.eof = DFA74_eof;
				this.min = DFA74_min;
				this.max = DFA74_max;
				this.accept = DFA74_accept;
				this.special = DFA74_special;
				this.transition = DFA74_transition;
			}

			public override string Description { get { return "321:1: result_column : ( ASTERISK | table_name= id DOT ASTERISK -> ^( ASTERISK $table_name) | expr ( ( AS )? column_alias= id )? -> ^( ALIAS expr ( $column_alias)? ) );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA73 : DFA
		{
			private const string DFA73_eotS =
				"\xD7\xFFFF";
			private const string DFA73_eofS =
				"\x1\x4\x2\xFFFF\x1\x1\x1\xFFFF\x7\x1\xCB\xFFFF";
			private const string DFA73_minS =
				"\x1\x20\x2\xFFFF\x1\x20\x1\xFFFF\x7\x20\x5\xFFFF\x2\x21\x6\x24\x8\xFFFF" +
				"\x2\x21\x6\x24\xAE\xFFFF";
			private const string DFA73_maxS =
				"\x1\xB3\x2\xFFFF\x1\xB3\x1\xFFFF\x1\xB3\x6\x78\x5\xFFFF\x2\xB3\x1\x26" +
				"\x3\x75\x1\x26\x1\x55\x8\xFFFF\x2\xB3\x1\x26\x3\x75\x1\x26\x1\x55\xAE" +
				"\xFFFF";
			private const string DFA73_acceptS =
				"\x1\xFFFF\x1\x1\x2\xFFFF\x1\x2\xD2\xFFFF";
			private const string DFA73_specialS =
				"\xD7\xFFFF}>";
			private static readonly string[] DFA73_transitionS =
			{
				"\x1\x4\x3\x1\x1\xFFFF\x2\x1\x1\xFFFF\x3\x1\x2\xFFFF\x2\x4\x2\xFFFF"+
				"\x3\x1\x17\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\xF\x1"+
				"\x1\xA\x1\xB\x1\x1\x1\x7\x1\x1\x1\x8\x1\x9\x1\x1\x1\x3\x1\x5\x1\x6\x3B"+
				"\x1",
				"",
				"",
				"\x1\x1\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x1\xFFFF\x1\x4\x2\x1\x2"+
				"\xFFFF\x3\x4\x17\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF"+
				"\xF\x4\x1\x17\x1\x18\x1\x4\x1\x14\x1\x4\x1\x15\x1\x16\x1\x4\x1\x11\x1"+
				"\x12\x1\x13\x3B\x4",
				"",
				"\x1\x1\x3\x4\x1\xFFFF\x6\x4\x1\xFFFF\x1\x4\x2\x1\x2\xFFFF\x3\x4\x10"+
				"\xFFFF\x2\x4\x4\xFFFF\x24\x4\x1\x27\x1\x28\x1\x4\x1\x24\x1\x4\x1\x25"+
				"\x1\x26\x1\x4\x1\x21\x1\x22\x1\x23\x3B\x4",
				"\x1\x1\x5\xFFFF\x1\x4\x6\xFFFF\x2\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1"+
				"\x1\xFFFF\x2\x1\x1\xFFFF\x3\x1",
				"\x1\x1\xC\xFFFF\x2\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\x4\x2\x1\x1"+
				"\x4\x3\x1",
				"\x1\x1\xC\xFFFF\x2\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\xFFFF\x2\x1"+
				"\x1\x4\x3\x1",
				"\x1\x1\xC\xFFFF\x2\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\xFFFF\x2\x1"+
				"\x1\x4\x3\x1",
				"\x1\x1\x5\xFFFF\x1\x4\x6\xFFFF\x2\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1"+
				"\x1\xFFFF\x2\x1\x1\xFFFF\x3\x1",
				"\x1\x1\xC\xFFFF\x2\x1\x26\xFFFF\x1\x4\x18\xFFFF\x2\x1\x1\xFFFF\x1"+
				"\x1\x1\xFFFF\x2\x1\x1\xFFFF\x3\x1",
				"",
				"",
				"",
				"",
				"",
				"\x3\x1\x1\x4\x2\x1\x1\xFFFF\x3\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x17"+
				"\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x3\x1\x1\x4\x6\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x10\xFFFF\x2\x1\x4"+
				"\xFFFF\x6A\x1",
				"\x1\x4\x1\xFFFF\x1\x1",
				"\x1\x4\x4D\xFFFF\x1\x1\x2\xFFFF\x1\x1",
				"\x1\x4\x50\xFFFF\x1\x1",
				"\x1\x4\x50\xFFFF\x1\x1",
				"\x1\x4\x1\xFFFF\x1\x1",
				"\x1\x4\x30\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x1\x1\x4\x2\x1\x1\xFFFF\x3\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x17"+
				"\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"\x3\x1\x1\x4\x6\x1\x1\xFFFF\x1\x1\x4\xFFFF\x3\x1\x10\xFFFF\x2\x1\x4"+
				"\xFFFF\x6A\x1",
				"\x1\x4\x1\xFFFF\x1\x1",
				"\x1\x4\x4D\xFFFF\x1\x1\x2\xFFFF\x1\x1",
				"\x1\x4\x50\xFFFF\x1\x1",
				"\x1\x4\x50\xFFFF\x1\x1",
				"\x1\x4\x1\xFFFF\x1\x1",
				"\x1\x4\x30\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA73_eot = DFA.UnpackEncodedString(DFA73_eotS);
			private static readonly short[] DFA73_eof = DFA.UnpackEncodedString(DFA73_eofS);
			private static readonly char[] DFA73_min = DFA.UnpackEncodedStringToUnsignedChars(DFA73_minS);
			private static readonly char[] DFA73_max = DFA.UnpackEncodedStringToUnsignedChars(DFA73_maxS);
			private static readonly short[] DFA73_accept = DFA.UnpackEncodedString(DFA73_acceptS);
			private static readonly short[] DFA73_special = DFA.UnpackEncodedString(DFA73_specialS);
			private static readonly short[][] DFA73_transition;

			static DFA73()
			{
				int numStates = DFA73_transitionS.Length;
				DFA73_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA73_transition[i] = DFA.UnpackEncodedString(DFA73_transitionS[i]);
				}
			}

			public DFA73(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 73;
				this.eot = DFA73_eot;
				this.eof = DFA73_eof;
				this.min = DFA73_min;
				this.max = DFA73_max;
				this.accept = DFA73_accept;
				this.special = DFA73_special;
				this.transition = DFA73_transition;
			}

			public override string Description { get { return "324:10: ( ( AS )? column_alias= id )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA72 : DFA
		{
			private const string DFA72_eotS =
				"\xD9\xFFFF";
			private const string DFA72_eofS =
				"\x1\xFFFF\x1\x2\x3\xFFFF\x1\x4\x1\xFFFF\x7\x4\xCB\xFFFF";
			private const string DFA72_minS =
				"\x1\x21\x1\x20\x3\xFFFF\x1\x20\x1\xFFFF\x7\x20\x5\xFFFF\x2\x21\x6\x24" +
				"\x8\xFFFF\x2\x21\x6\x24\xAE\xFFFF";
			private const string DFA72_maxS =
				"\x2\xB3\x3\xFFFF\x1\xB3\x1\xFFFF\x1\xB3\x6\x78\x5\xFFFF\x2\xB3\x1\x26" +
				"\x3\x75\x1\x26\x1\x55\x8\xFFFF\x2\xB3\x1\x26\x3\x75\x1\x26\x1\x55\xAE" +
				"\xFFFF";
			private const string DFA72_acceptS =
				"\x2\xFFFF\x1\x2\x1\xFFFF\x1\x1\xD4\xFFFF";
			private const string DFA72_specialS =
				"\xD9\xFFFF}>";
			private static readonly string[] DFA72_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x4\x2"+
				"\x1\x1\x5\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x2\x3\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4\x2\xFFFF\x2\x2\x2\xFFFF"+
				"\x3\x4\x17\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\xF\x4"+
				"\x1\xC\x1\xD\x1\x4\x1\x9\x1\x4\x1\xA\x1\xB\x1\x4\x1\x5\x1\x7\x1\x8\x3B"+
				"\x4",
				"",
				"",
				"",
				"\x1\x4\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2\x2\x4\x2"+
				"\xFFFF\x3\x2\x17\xFFFF\xA\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF"+
				"\xF\x2\x1\x19\x1\x1A\x1\x2\x1\x16\x1\x2\x1\x17\x1\x18\x1\x2\x1\x13\x1"+
				"\x14\x1\x15\x3B\x2",
				"",
				"\x1\x4\x3\x2\x1\xFFFF\x6\x2\x1\xFFFF\x1\x2\x2\x4\x2\xFFFF\x3\x2\x10"+
				"\xFFFF\x2\x2\x4\xFFFF\x24\x2\x1\x29\x1\x2A\x1\x2\x1\x26\x1\x2\x1\x27"+
				"\x1\x28\x1\x2\x1\x23\x1\x24\x1\x25\x3B\x2",
				"\x1\x4\x5\xFFFF\x1\x2\x6\xFFFF\x2\x4\x3F\xFFFF\x2\x4\x1\xFFFF\x1\x4"+
				"\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4",
				"\x1\x4\xC\xFFFF\x2\x4\x3F\xFFFF\x2\x4\x1\xFFFF\x1\x4\x1\x2\x2\x4\x1"+
				"\x2\x3\x4",
				"\x1\x4\xC\xFFFF\x2\x4\x3F\xFFFF\x2\x4\x1\xFFFF\x1\x4\x1\xFFFF\x2\x4"+
				"\x1\x2\x3\x4",
				"\x1\x4\xC\xFFFF\x2\x4\x3F\xFFFF\x2\x4\x1\xFFFF\x1\x4\x1\xFFFF\x2\x4"+
				"\x1\x2\x3\x4",
				"\x1\x4\x5\xFFFF\x1\x2\x6\xFFFF\x2\x4\x3F\xFFFF\x2\x4\x1\xFFFF\x1\x4"+
				"\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4",
				"\x1\x4\xC\xFFFF\x2\x4\x26\xFFFF\x1\x2\x18\xFFFF\x2\x4\x1\xFFFF\x1"+
				"\x4\x1\xFFFF\x2\x4\x1\xFFFF\x3\x4",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\x2\x2\x4\x1\xFFFF\x3\x4\x1\xFFFF\x1\x4\x4\xFFFF\x3\x4\x17"+
				"\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x3\x4\x1\x2\x6\x4\x1\xFFFF\x1\x4\x4\xFFFF\x3\x4\x10\xFFFF\x2\x4\x4"+
				"\xFFFF\x6A\x4",
				"\x1\x2\x1\xFFFF\x1\x4",
				"\x1\x2\x4D\xFFFF\x1\x4\x2\xFFFF\x1\x4",
				"\x1\x2\x50\xFFFF\x1\x4",
				"\x1\x2\x50\xFFFF\x1\x4",
				"\x1\x2\x1\xFFFF\x1\x4",
				"\x1\x2\x30\xFFFF\x1\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x3\x4\x1\x2\x2\x4\x1\xFFFF\x3\x4\x1\xFFFF\x1\x4\x4\xFFFF\x3\x4\x17"+
				"\xFFFF\xA\x4\x2\xFFFF\x1\x4\x1\xFFFF\x3\x4\x3\xFFFF\x55\x4",
				"\x3\x4\x1\x2\x6\x4\x1\xFFFF\x1\x4\x4\xFFFF\x3\x4\x10\xFFFF\x2\x4\x4"+
				"\xFFFF\x6A\x4",
				"\x1\x2\x1\xFFFF\x1\x4",
				"\x1\x2\x4D\xFFFF\x1\x4\x2\xFFFF\x1\x4",
				"\x1\x2\x50\xFFFF\x1\x4",
				"\x1\x2\x50\xFFFF\x1\x4",
				"\x1\x2\x1\xFFFF\x1\x4",
				"\x1\x2\x30\xFFFF\x1\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA72_eot = DFA.UnpackEncodedString(DFA72_eotS);
			private static readonly short[] DFA72_eof = DFA.UnpackEncodedString(DFA72_eofS);
			private static readonly char[] DFA72_min = DFA.UnpackEncodedStringToUnsignedChars(DFA72_minS);
			private static readonly char[] DFA72_max = DFA.UnpackEncodedStringToUnsignedChars(DFA72_maxS);
			private static readonly short[] DFA72_accept = DFA.UnpackEncodedString(DFA72_acceptS);
			private static readonly short[] DFA72_special = DFA.UnpackEncodedString(DFA72_specialS);
			private static readonly short[][] DFA72_transition;

			static DFA72()
			{
				int numStates = DFA72_transitionS.Length;
				DFA72_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA72_transition[i] = DFA.UnpackEncodedString(DFA72_transitionS[i]);
				}
			}

			public DFA72(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 72;
				this.eot = DFA72_eot;
				this.eof = DFA72_eof;
				this.min = DFA72_min;
				this.max = DFA72_max;
				this.accept = DFA72_accept;
				this.special = DFA72_special;
				this.transition = DFA72_transition;
			}

			public override string Description { get { return "324:11: ( AS )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA76 : DFA
		{
			private const string DFA76_eotS =
				"\x12\xFFFF";
			private const string DFA76_eofS =
				"\x1\x1\x11\xFFFF";
			private const string DFA76_minS =
				"\x1\x20\x11\xFFFF";
			private const string DFA76_maxS =
				"\x1\x7F\x11\xFFFF";
			private const string DFA76_acceptS =
				"\x1\xFFFF\x1\x2\x9\xFFFF\x1\x1\x6\xFFFF";
			private const string DFA76_specialS =
				"\x12\xFFFF}>";
			private static readonly string[] DFA76_transitionS =
			{
				"\x1\x1\xC\xFFFF\x1\xB\x1\x1\x3F\xFFFF\x2\x1\x1\xFFFF\x1\x1\x1\xFFFF"+
				"\x2\x1\x2\xFFFF\x2\x1\x1\xFFFF\x6\xB",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA76_eot = DFA.UnpackEncodedString(DFA76_eotS);
			private static readonly short[] DFA76_eof = DFA.UnpackEncodedString(DFA76_eofS);
			private static readonly char[] DFA76_min = DFA.UnpackEncodedStringToUnsignedChars(DFA76_minS);
			private static readonly char[] DFA76_max = DFA.UnpackEncodedStringToUnsignedChars(DFA76_maxS);
			private static readonly short[] DFA76_accept = DFA.UnpackEncodedString(DFA76_acceptS);
			private static readonly short[] DFA76_special = DFA.UnpackEncodedString(DFA76_specialS);
			private static readonly short[][] DFA76_transition;

			static DFA76()
			{
				int numStates = DFA76_transitionS.Length;
				DFA76_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA76_transition[i] = DFA.UnpackEncodedString(DFA76_transitionS[i]);
				}
			}

			public DFA76(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 76;
				this.eot = DFA76_eot;
				this.eof = DFA76_eof;
				this.min = DFA76_min;
				this.max = DFA76_max;
				this.accept = DFA76_accept;
				this.special = DFA76_special;
				this.transition = DFA76_transition;
			}

			public override string Description { get { return "()* loopback of 326:28: ( join_op single_source ( join_constraint )? )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA75 : DFA
		{
			private const string DFA75_eotS =
				"\x14\xFFFF";
			private const string DFA75_eofS =
				"\x1\x3\x13\xFFFF";
			private const string DFA75_minS =
				"\x1\x20\x13\xFFFF";
			private const string DFA75_maxS =
				"\x1\x81\x13\xFFFF";
			private const string DFA75_acceptS =
				"\x1\xFFFF\x1\x1\x1\xFFFF\x1\x2\x10\xFFFF";
			private const string DFA75_specialS =
				"\x14\xFFFF}>";
			private static readonly string[] DFA75_transitionS =
			{
				"\x1\x3\xC\xFFFF\x2\x3\x3F\xFFFF\x2\x3\x1\xFFFF\x1\x3\x1\xFFFF\x2\x3"+
				"\x2\xFFFF\x2\x3\x1\xFFFF\x6\x3\x2\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA75_eot = DFA.UnpackEncodedString(DFA75_eotS);
			private static readonly short[] DFA75_eof = DFA.UnpackEncodedString(DFA75_eofS);
			private static readonly char[] DFA75_min = DFA.UnpackEncodedStringToUnsignedChars(DFA75_minS);
			private static readonly char[] DFA75_max = DFA.UnpackEncodedStringToUnsignedChars(DFA75_maxS);
			private static readonly short[] DFA75_accept = DFA.UnpackEncodedString(DFA75_acceptS);
			private static readonly short[] DFA75_special = DFA.UnpackEncodedString(DFA75_specialS);
			private static readonly short[][] DFA75_transition;

			static DFA75()
			{
				int numStates = DFA75_transitionS.Length;
				DFA75_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA75_transition[i] = DFA.UnpackEncodedString(DFA75_transitionS[i]);
				}
			}

			public DFA75(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 75;
				this.eot = DFA75_eot;
				this.eof = DFA75_eof;
				this.min = DFA75_min;
				this.max = DFA75_max;
				this.accept = DFA75_accept;
				this.special = DFA75_special;
				this.transition = DFA75_transition;
			}

			public override string Description { get { return "326:52: ( join_constraint )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA83 : DFA
		{
			private const string DFA83_eotS =
				"\x20\xFFFF";
			private const string DFA83_eofS =
				"\x20\xFFFF";
			private const string DFA83_minS =
				"\x1\x21\x3\xFFFF\x2\x21\x1A\xFFFF";
			private const string DFA83_maxS =
				"\x1\xB3\x3\xFFFF\x2\xB3\x1A\xFFFF";
			private const string DFA83_acceptS =
				"\x1\xFFFF\x1\x1\x4\xFFFF\x1\x3\x3\xFFFF\x1\x2\x15\xFFFF";
			private const string DFA83_specialS =
				"\x20\xFFFF}>";
			private static readonly string[] DFA83_transitionS =
			{
				"\x3\x1\x1\xFFFF\x2\x1\x1\xFFFF\x3\x1\x1\xFFFF\x1\x4\x4\xFFFF\x3\x1"+
				"\x17\xFFFF\xA\x1\x2\xFFFF\x1\x1\x1\xFFFF\x3\x1\x3\xFFFF\x55\x1",
				"",
				"",
				"",
				"\x3\x6\x1\xFFFF\x2\x6\x1\xFFFF\x3\x6\x1\xFFFF\x1\x6\x4\xFFFF\x3\x6"+
				"\x17\xFFFF\xA\x6\x2\xFFFF\x1\x6\x1\xFFFF\x3\x6\x3\xFFFF\x16\x6\x1\x5"+
				"\x3E\x6",
				"\x3\xA\x1\x6\x6\xA\x1\xFFFF\x1\xA\x4\xFFFF\x3\xA\x10\xFFFF\x3\xA\x3"+
				"\xFFFF\x6A\xA",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA83_eot = DFA.UnpackEncodedString(DFA83_eotS);
			private static readonly short[] DFA83_eof = DFA.UnpackEncodedString(DFA83_eofS);
			private static readonly char[] DFA83_min = DFA.UnpackEncodedStringToUnsignedChars(DFA83_minS);
			private static readonly char[] DFA83_max = DFA.UnpackEncodedStringToUnsignedChars(DFA83_maxS);
			private static readonly short[] DFA83_accept = DFA.UnpackEncodedString(DFA83_acceptS);
			private static readonly short[] DFA83_special = DFA.UnpackEncodedString(DFA83_specialS);
			private static readonly short[][] DFA83_transition;

			static DFA83()
			{
				int numStates = DFA83_transitionS.Length;
				DFA83_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA83_transition[i] = DFA.UnpackEncodedString(DFA83_transitionS[i]);
				}
			}

			public DFA83(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 83;
				this.eot = DFA83_eot;
				this.eof = DFA83_eof;
				this.min = DFA83_min;
				this.max = DFA83_max;
				this.accept = DFA83_accept;
				this.special = DFA83_special;
				this.transition = DFA83_transition;
			}

			public override string Description { get { return "328:1: single_source : ( (database_name= id DOT )? table_name= ID ( ( AS )? table_alias= ID )? ( INDEXED BY index_name= id | NOT INDEXED )? -> ^( ALIAS ^( $table_name ( $database_name)? ) ( $table_alias)? ( ^( INDEXED ( NOT )? ( $index_name)? ) )? ) | LPAREN select_stmt RPAREN ( ( AS )? table_alias= ID )? -> ^( ALIAS select_stmt ( $table_alias)? ) | LPAREN join_source RPAREN );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA77 : DFA
		{
			private const string DFA77_eotS =
				"\x1C\xFFFF";
			private const string DFA77_eofS =
				"\x1\xFFFF\x1\x5\x1A\xFFFF";
			private const string DFA77_minS =
				"\x1\x21\x1\x20\x1A\xFFFF";
			private const string DFA77_maxS =
				"\x1\xB3\x1\x81\x1A\xFFFF";
			private const string DFA77_acceptS =
				"\x2\xFFFF\x1\x1\x2\xFFFF\x1\x2\x16\xFFFF";
			private const string DFA77_specialS =
				"\x1C\xFFFF}>";
			private static readonly string[] DFA77_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x5\x3\xFFFF\x1\x2\x1\x5\x1\xFFFF\x1\x5\x5\xFFFF\x2\x5\x1D\xFFFF"+
				"\x1\x5\x2\xFFFF\x1\x5\x1E\xFFFF\x2\x5\x1\xFFFF\x1\x5\x1\xFFFF\x2\x5"+
				"\x2\xFFFF\x2\x5\x1\xFFFF\x8\x5",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA77_eot = DFA.UnpackEncodedString(DFA77_eotS);
			private static readonly short[] DFA77_eof = DFA.UnpackEncodedString(DFA77_eofS);
			private static readonly char[] DFA77_min = DFA.UnpackEncodedStringToUnsignedChars(DFA77_minS);
			private static readonly char[] DFA77_max = DFA.UnpackEncodedStringToUnsignedChars(DFA77_maxS);
			private static readonly short[] DFA77_accept = DFA.UnpackEncodedString(DFA77_acceptS);
			private static readonly short[] DFA77_special = DFA.UnpackEncodedString(DFA77_specialS);
			private static readonly short[][] DFA77_transition;

			static DFA77()
			{
				int numStates = DFA77_transitionS.Length;
				DFA77_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA77_transition[i] = DFA.UnpackEncodedString(DFA77_transitionS[i]);
				}
			}

			public DFA77(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 77;
				this.eot = DFA77_eot;
				this.eof = DFA77_eof;
				this.min = DFA77_min;
				this.max = DFA77_max;
				this.accept = DFA77_accept;
				this.special = DFA77_special;
				this.transition = DFA77_transition;
			}

			public override string Description { get { return "329:5: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA79 : DFA
		{
			private const string DFA79_eotS =
				"\x18\xFFFF";
			private const string DFA79_eofS =
				"\x1\x3\x17\xFFFF";
			private const string DFA79_minS =
				"\x1\x20\x17\xFFFF";
			private const string DFA79_maxS =
				"\x1\x81\x17\xFFFF";
			private const string DFA79_acceptS =
				"\x1\xFFFF\x1\x1\x1\xFFFF\x1\x2\x14\xFFFF";
			private const string DFA79_specialS =
				"\x18\xFFFF}>";
			private static readonly string[] DFA79_transitionS =
			{
				"\x1\x3\x4\xFFFF\x1\x3\x1\xFFFF\x1\x3\x5\xFFFF\x2\x3\x1D\xFFFF\x1\x1"+
				"\x2\xFFFF\x1\x1\x1E\xFFFF\x2\x3\x1\xFFFF\x1\x3\x1\xFFFF\x2\x3\x2\xFFFF"+
				"\x2\x3\x1\xFFFF\x8\x3",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA79_eot = DFA.UnpackEncodedString(DFA79_eotS);
			private static readonly short[] DFA79_eof = DFA.UnpackEncodedString(DFA79_eofS);
			private static readonly char[] DFA79_min = DFA.UnpackEncodedStringToUnsignedChars(DFA79_minS);
			private static readonly char[] DFA79_max = DFA.UnpackEncodedStringToUnsignedChars(DFA79_maxS);
			private static readonly short[] DFA79_accept = DFA.UnpackEncodedString(DFA79_acceptS);
			private static readonly short[] DFA79_special = DFA.UnpackEncodedString(DFA79_specialS);
			private static readonly short[][] DFA79_transition;

			static DFA79()
			{
				int numStates = DFA79_transitionS.Length;
				DFA79_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA79_transition[i] = DFA.UnpackEncodedString(DFA79_transitionS[i]);
				}
			}

			public DFA79(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 79;
				this.eot = DFA79_eot;
				this.eof = DFA79_eof;
				this.min = DFA79_min;
				this.max = DFA79_max;
				this.accept = DFA79_accept;
				this.special = DFA79_special;
				this.transition = DFA79_transition;
			}

			public override string Description { get { return "329:43: ( ( AS )? table_alias= ID )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA80 : DFA
		{
			private const string DFA80_eotS =
				"\x16\xFFFF";
			private const string DFA80_eofS =
				"\x1\x3\x15\xFFFF";
			private const string DFA80_minS =
				"\x1\x20\x15\xFFFF";
			private const string DFA80_maxS =
				"\x1\x81\x15\xFFFF";
			private const string DFA80_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x1\x3\x12\xFFFF";
			private const string DFA80_specialS =
				"\x16\xFFFF}>";
			private static readonly string[] DFA80_transitionS =
			{
				"\x1\x3\x4\xFFFF\x1\x1\x1\xFFFF\x1\x2\x5\xFFFF\x2\x3\x3F\xFFFF\x2\x3"+
				"\x1\xFFFF\x1\x3\x1\xFFFF\x2\x3\x2\xFFFF\x2\x3\x1\xFFFF\x8\x3",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA80_eot = DFA.UnpackEncodedString(DFA80_eotS);
			private static readonly short[] DFA80_eof = DFA.UnpackEncodedString(DFA80_eofS);
			private static readonly char[] DFA80_min = DFA.UnpackEncodedStringToUnsignedChars(DFA80_minS);
			private static readonly char[] DFA80_max = DFA.UnpackEncodedStringToUnsignedChars(DFA80_maxS);
			private static readonly short[] DFA80_accept = DFA.UnpackEncodedString(DFA80_acceptS);
			private static readonly short[] DFA80_special = DFA.UnpackEncodedString(DFA80_specialS);
			private static readonly short[][] DFA80_transition;

			static DFA80()
			{
				int numStates = DFA80_transitionS.Length;
				DFA80_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA80_transition[i] = DFA.UnpackEncodedString(DFA80_transitionS[i]);
				}
			}

			public DFA80(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 80;
				this.eot = DFA80_eot;
				this.eof = DFA80_eof;
				this.min = DFA80_min;
				this.max = DFA80_max;
				this.accept = DFA80_accept;
				this.special = DFA80_special;
				this.transition = DFA80_transition;
			}

			public override string Description { get { return "329:67: ( INDEXED BY index_name= id | NOT INDEXED )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA82 : DFA
		{
			private const string DFA82_eotS =
				"\x16\xFFFF";
			private const string DFA82_eofS =
				"\x1\x3\x15\xFFFF";
			private const string DFA82_minS =
				"\x1\x20\x15\xFFFF";
			private const string DFA82_maxS =
				"\x1\x81\x15\xFFFF";
			private const string DFA82_acceptS =
				"\x1\xFFFF\x1\x1\x1\xFFFF\x1\x2\x12\xFFFF";
			private const string DFA82_specialS =
				"\x16\xFFFF}>";
			private static readonly string[] DFA82_transitionS =
			{
				"\x1\x3\xC\xFFFF\x2\x3\x1D\xFFFF\x1\x1\x2\xFFFF\x1\x1\x1E\xFFFF\x2\x3"+
				"\x1\xFFFF\x1\x3\x1\xFFFF\x2\x3\x2\xFFFF\x2\x3\x1\xFFFF\x8\x3",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA82_eot = DFA.UnpackEncodedString(DFA82_eotS);
			private static readonly short[] DFA82_eof = DFA.UnpackEncodedString(DFA82_eofS);
			private static readonly char[] DFA82_min = DFA.UnpackEncodedStringToUnsignedChars(DFA82_minS);
			private static readonly char[] DFA82_max = DFA.UnpackEncodedStringToUnsignedChars(DFA82_maxS);
			private static readonly short[] DFA82_accept = DFA.UnpackEncodedString(DFA82_acceptS);
			private static readonly short[] DFA82_special = DFA.UnpackEncodedString(DFA82_specialS);
			private static readonly short[][] DFA82_transition;

			static DFA82()
			{
				int numStates = DFA82_transitionS.Length;
				DFA82_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA82_transition[i] = DFA.UnpackEncodedString(DFA82_transitionS[i]);
				}
			}

			public DFA82(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 82;
				this.eot = DFA82_eot;
				this.eof = DFA82_eof;
				this.min = DFA82_min;
				this.max = DFA82_max;
				this.accept = DFA82_accept;
				this.special = DFA82_special;
				this.transition = DFA82_transition;
			}

			public override string Description { get { return "331:31: ( ( AS )? table_alias= ID )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA93 : DFA
		{
			private const string DFA93_eotS =
				"\xD\xFFFF";
			private const string DFA93_eofS =
				"\xD\xFFFF";
			private const string DFA93_minS =
				"\x1\x21\x2\x24\xA\xFFFF";
			private const string DFA93_maxS =
				"\x1\xB3\x2\x85\xA\xFFFF";
			private const string DFA93_acceptS =
				"\x3\xFFFF\x1\x1\x1\x2\x8\xFFFF";
			private const string DFA93_specialS =
				"\xD\xFFFF}>";
			private static readonly string[] DFA93_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x3\x7\xFFFF\x1\x4\x48\xFFFF\x1\x4\xE\xFFFF\x2\x4",
				"\x1\x3\x7\xFFFF\x1\x4\x48\xFFFF\x1\x4\xE\xFFFF\x2\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA93_eot = DFA.UnpackEncodedString(DFA93_eotS);
			private static readonly short[] DFA93_eof = DFA.UnpackEncodedString(DFA93_eofS);
			private static readonly char[] DFA93_min = DFA.UnpackEncodedStringToUnsignedChars(DFA93_minS);
			private static readonly char[] DFA93_max = DFA.UnpackEncodedStringToUnsignedChars(DFA93_maxS);
			private static readonly short[] DFA93_accept = DFA.UnpackEncodedString(DFA93_acceptS);
			private static readonly short[] DFA93_special = DFA.UnpackEncodedString(DFA93_specialS);
			private static readonly short[][] DFA93_transition;

			static DFA93()
			{
				int numStates = DFA93_transitionS.Length;
				DFA93_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA93_transition[i] = DFA.UnpackEncodedString(DFA93_transitionS[i]);
				}
			}

			public DFA93(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 93;
				this.eot = DFA93_eot;
				this.eof = DFA93_eof;
				this.min = DFA93_min;
				this.max = DFA93_max;
				this.accept = DFA93_accept;
				this.special = DFA93_special;
				this.transition = DFA93_transition;
			}

			public override string Description { get { return "344:67: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA118 : DFA
		{
			private const string DFA118_eotS =
				"\x42\xFFFF";
			private const string DFA118_eofS =
				"\x42\xFFFF";
			private const string DFA118_minS =
				"\x1\x2D\x1\x21\x2\xFFFF\x4\x27\x3A\xFFFF";
			private const string DFA118_maxS =
				"\x1\x2E\x1\xB3\x2\xFFFF\x4\xA0\x3A\xFFFF";
			private const string DFA118_acceptS =
				"\x2\xFFFF\x1\x2\x5\xFFFF\x1\x1\x39\xFFFF";
			private const string DFA118_specialS =
				"\x42\xFFFF}>";
			private static readonly string[] DFA118_transitionS =
			{
				"\x1\x1\x1\x2",
				"\x3\x8\x1\xFFFF\x7\x8\x3\xFFFF\x5\x8\x4\xFFFF\x4\x8\xF\xFFFF\xA\x8"+
				"\x2\xFFFF\x1\x8\x1\xFFFF\x3\x8\x3\xFFFF\x3A\x8\x1\x2\x1\x4\x2\x8\x1"+
				"\x5\x1\x6\x1\x7\x9\x8\x1\xFFFF\xA\x8",
				"",
				"",
				"\x1\x8\x5\xFFFF\x2\x8\x3\xFFFF\x1\x8\x18\xFFFF\x2\x8\x38\xFFFF\x1"+
				"\x8\x13\xFFFF\x2\x8\x1\x2\x3\x8\x1\xFFFF\x1\x8",
				"\x1\x8\x4\xFFFF\x1\x2\x2\x8\x3\xFFFF\x1\x8\x18\xFFFF\x2\x8\x38\xFFFF"+
				"\x1\x8\x13\xFFFF\x2\x8\x1\xFFFF\x3\x8\x1\xFFFF\x1\x8",
				"\x1\x8\x4\xFFFF\x1\x2\x2\x8\x3\xFFFF\x1\x8\x18\xFFFF\x2\x8\x38\xFFFF"+
				"\x1\x8\x13\xFFFF\x2\x8\x1\xFFFF\x3\x8\x1\xFFFF\x1\x8",
				"\x1\x8\x5\xFFFF\x2\x8\x3\xFFFF\x1\x8\x18\xFFFF\x2\x8\x38\xFFFF\x1"+
				"\x8\x13\xFFFF\x2\x8\x1\x2\x3\x8\x1\xFFFF\x1\x8",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA118_eot = DFA.UnpackEncodedString(DFA118_eotS);
			private static readonly short[] DFA118_eof = DFA.UnpackEncodedString(DFA118_eofS);
			private static readonly char[] DFA118_min = DFA.UnpackEncodedStringToUnsignedChars(DFA118_minS);
			private static readonly char[] DFA118_max = DFA.UnpackEncodedStringToUnsignedChars(DFA118_maxS);
			private static readonly short[] DFA118_accept = DFA.UnpackEncodedString(DFA118_acceptS);
			private static readonly short[] DFA118_special = DFA.UnpackEncodedString(DFA118_specialS);
			private static readonly short[][] DFA118_transition;

			static DFA118()
			{
				int numStates = DFA118_transitionS.Length;
				DFA118_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA118_transition[i] = DFA.UnpackEncodedString(DFA118_transitionS[i]);
				}
			}

			public DFA118(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 118;
				this.eot = DFA118_eot;
				this.eof = DFA118_eof;
				this.min = DFA118_min;
				this.max = DFA118_max;
				this.accept = DFA118_accept;
				this.special = DFA118_special;
				this.transition = DFA118_transition;
			}

			public override string Description { get { return "()* loopback of 388:23: ( COMMA column_def )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA121 : DFA
		{
			private const string DFA121_eotS =
				"\x10\xFFFF";
			private const string DFA121_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA121_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA121_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA121_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA121_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA121_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x1\x1\x38\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA121_eot = DFA.UnpackEncodedString(DFA121_eotS);
			private static readonly short[] DFA121_eof = DFA.UnpackEncodedString(DFA121_eofS);
			private static readonly char[] DFA121_min = DFA.UnpackEncodedStringToUnsignedChars(DFA121_minS);
			private static readonly char[] DFA121_max = DFA.UnpackEncodedStringToUnsignedChars(DFA121_maxS);
			private static readonly short[] DFA121_accept = DFA.UnpackEncodedString(DFA121_acceptS);
			private static readonly short[] DFA121_special = DFA.UnpackEncodedString(DFA121_specialS);
			private static readonly short[][] DFA121_transition;

			static DFA121()
			{
				int numStates = DFA121_transitionS.Length;
				DFA121_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA121_transition[i] = DFA.UnpackEncodedString(DFA121_transitionS[i]);
				}
			}

			public DFA121(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 121;
				this.eot = DFA121_eot;
				this.eof = DFA121_eof;
				this.min = DFA121_min;
				this.max = DFA121_max;
				this.accept = DFA121_accept;
				this.special = DFA121_special;
				this.transition = DFA121_transition;
			}

			public override string Description { get { return "393:32: ( type_name )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA122 : DFA
		{
			private const string DFA122_eotS =
				"\xF\xFFFF";
			private const string DFA122_eofS =
				"\x1\x1\xE\xFFFF";
			private const string DFA122_minS =
				"\x1\x20\xE\xFFFF";
			private const string DFA122_maxS =
				"\x1\xA0\xE\xFFFF";
			private const string DFA122_acceptS =
				"\x1\xFFFF\x1\x2\x3\xFFFF\x1\x1\x9\xFFFF";
			private const string DFA122_specialS =
				"\xF\xFFFF}>";
			private static readonly string[] DFA122_transitionS =
			{
				"\x1\x1\x6\xFFFF\x1\x5\x5\xFFFF\x2\x1\x3\xFFFF\x1\x5\x18\xFFFF\x1\x5"+
				"\x39\xFFFF\x1\x5\x13\xFFFF\x2\x5\x1\xFFFF\x3\x5\x1\xFFFF\x1\x5",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA122_eot = DFA.UnpackEncodedString(DFA122_eotS);
			private static readonly short[] DFA122_eof = DFA.UnpackEncodedString(DFA122_eofS);
			private static readonly char[] DFA122_min = DFA.UnpackEncodedStringToUnsignedChars(DFA122_minS);
			private static readonly char[] DFA122_max = DFA.UnpackEncodedStringToUnsignedChars(DFA122_maxS);
			private static readonly short[] DFA122_accept = DFA.UnpackEncodedString(DFA122_acceptS);
			private static readonly short[] DFA122_special = DFA.UnpackEncodedString(DFA122_specialS);
			private static readonly short[][] DFA122_transition;

			static DFA122()
			{
				int numStates = DFA122_transitionS.Length;
				DFA122_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA122_transition[i] = DFA.UnpackEncodedString(DFA122_transitionS[i]);
				}
			}

			public DFA122(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 122;
				this.eot = DFA122_eot;
				this.eof = DFA122_eof;
				this.min = DFA122_min;
				this.max = DFA122_max;
				this.accept = DFA122_accept;
				this.special = DFA122_special;
				this.transition = DFA122_transition;
			}

			public override string Description { get { return "()* loopback of 393:43: ( column_constraint )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA123 : DFA
		{
			private const string DFA123_eotS =
				"\xB\xFFFF";
			private const string DFA123_eofS =
				"\xB\xFFFF";
			private const string DFA123_minS =
				"\x1\x27\xA\xFFFF";
			private const string DFA123_maxS =
				"\x1\xA0\xA\xFFFF";
			private const string DFA123_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x8\xFFFF";
			private const string DFA123_specialS =
				"\xB\xFFFF}>";
			private static readonly string[] DFA123_transitionS =
			{
				"\x1\x2\xA\xFFFF\x1\x2\x18\xFFFF\x1\x2\x39\xFFFF\x1\x2\x13\xFFFF\x1"+
				"\x1\x1\x2\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA123_eot = DFA.UnpackEncodedString(DFA123_eotS);
			private static readonly short[] DFA123_eof = DFA.UnpackEncodedString(DFA123_eofS);
			private static readonly char[] DFA123_min = DFA.UnpackEncodedStringToUnsignedChars(DFA123_minS);
			private static readonly char[] DFA123_max = DFA.UnpackEncodedStringToUnsignedChars(DFA123_maxS);
			private static readonly short[] DFA123_accept = DFA.UnpackEncodedString(DFA123_acceptS);
			private static readonly short[] DFA123_special = DFA.UnpackEncodedString(DFA123_specialS);
			private static readonly short[][] DFA123_transition;

			static DFA123()
			{
				int numStates = DFA123_transitionS.Length;
				DFA123_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA123_transition[i] = DFA.UnpackEncodedString(DFA123_transitionS[i]);
				}
			}

			public DFA123(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 123;
				this.eot = DFA123_eot;
				this.eof = DFA123_eof;
				this.min = DFA123_min;
				this.max = DFA123_max;
				this.accept = DFA123_accept;
				this.special = DFA123_special;
				this.transition = DFA123_transition;
			}

			public override string Description { get { return "396:20: ( CONSTRAINT name= id )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA124 : DFA
		{
			private const string DFA124_eotS =
				"\xA\xFFFF";
			private const string DFA124_eofS =
				"\xA\xFFFF";
			private const string DFA124_minS =
				"\x1\x27\x9\xFFFF";
			private const string DFA124_maxS =
				"\x1\xA0\x9\xFFFF";
			private const string DFA124_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x1\x3\x1\x4\x1\x5\x1\x6\x1\x7\x1\x8\x1\x9";
			private const string DFA124_specialS =
				"\xA\xFFFF}>";
			private static readonly string[] DFA124_transitionS =
			{
				"\x1\x3\xA\xFFFF\x1\x4\x18\xFFFF\x1\x8\x39\xFFFF\x1\x7\x14\xFFFF\x1"+
				"\x1\x1\xFFFF\x1\x2\x1\x5\x1\x6\x1\xFFFF\x1\x9",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA124_eot = DFA.UnpackEncodedString(DFA124_eotS);
			private static readonly short[] DFA124_eof = DFA.UnpackEncodedString(DFA124_eofS);
			private static readonly char[] DFA124_min = DFA.UnpackEncodedStringToUnsignedChars(DFA124_minS);
			private static readonly char[] DFA124_max = DFA.UnpackEncodedStringToUnsignedChars(DFA124_maxS);
			private static readonly short[] DFA124_accept = DFA.UnpackEncodedString(DFA124_acceptS);
			private static readonly short[] DFA124_special = DFA.UnpackEncodedString(DFA124_specialS);
			private static readonly short[][] DFA124_transition;

			static DFA124()
			{
				int numStates = DFA124_transitionS.Length;
				DFA124_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA124_transition[i] = DFA.UnpackEncodedString(DFA124_transitionS[i]);
				}
			}

			public DFA124(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 124;
				this.eot = DFA124_eot;
				this.eof = DFA124_eof;
				this.min = DFA124_min;
				this.max = DFA124_max;
				this.accept = DFA124_accept;
				this.special = DFA124_special;
				this.transition = DFA124_transition;
			}

			public override string Description { get { return "397:3: ( column_constraint_pk | column_constraint_identity | column_constraint_not_null | column_constraint_null | column_constraint_unique | column_constraint_check | column_constraint_default | column_constraint_collate | fk_clause )"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA125 : DFA
		{
			private const string DFA125_eotS =
				"\x11\xFFFF";
			private const string DFA125_eofS =
				"\x1\x2\x10\xFFFF";
			private const string DFA125_minS =
				"\x1\x20\x10\xFFFF";
			private const string DFA125_maxS =
				"\x1\xA0\x10\xFFFF";
			private const string DFA125_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xE\xFFFF";
			private const string DFA125_specialS =
				"\x11\xFFFF}>";
			private static readonly string[] DFA125_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x20\xFFFF\x2\x1\x12\xFFFF\x1\x2\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF"+
				"\x3\x2\x1\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA125_eot = DFA.UnpackEncodedString(DFA125_eotS);
			private static readonly short[] DFA125_eof = DFA.UnpackEncodedString(DFA125_eofS);
			private static readonly char[] DFA125_min = DFA.UnpackEncodedStringToUnsignedChars(DFA125_minS);
			private static readonly char[] DFA125_max = DFA.UnpackEncodedStringToUnsignedChars(DFA125_maxS);
			private static readonly short[] DFA125_accept = DFA.UnpackEncodedString(DFA125_acceptS);
			private static readonly short[] DFA125_special = DFA.UnpackEncodedString(DFA125_specialS);
			private static readonly short[][] DFA125_transition;

			static DFA125()
			{
				int numStates = DFA125_transitionS.Length;
				DFA125_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA125_transition[i] = DFA.UnpackEncodedString(DFA125_transitionS[i]);
				}
			}

			public DFA125(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 125;
				this.eot = DFA125_eot;
				this.eof = DFA125_eof;
				this.min = DFA125_min;
				this.max = DFA125_max;
				this.accept = DFA125_accept;
				this.special = DFA125_special;
				this.transition = DFA125_transition;
			}

			public override string Description { get { return "418:37: ( ASC | DESC )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA126 : DFA
		{
			private const string DFA126_eotS =
				"\x10\xFFFF";
			private const string DFA126_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA126_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA126_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA126_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA126_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA126_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x34\xFFFF\x1\x1\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF"+
				"\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA126_eot = DFA.UnpackEncodedString(DFA126_eotS);
			private static readonly short[] DFA126_eof = DFA.UnpackEncodedString(DFA126_eofS);
			private static readonly char[] DFA126_min = DFA.UnpackEncodedStringToUnsignedChars(DFA126_minS);
			private static readonly char[] DFA126_max = DFA.UnpackEncodedStringToUnsignedChars(DFA126_maxS);
			private static readonly short[] DFA126_accept = DFA.UnpackEncodedString(DFA126_acceptS);
			private static readonly short[] DFA126_special = DFA.UnpackEncodedString(DFA126_specialS);
			private static readonly short[][] DFA126_transition;

			static DFA126()
			{
				int numStates = DFA126_transitionS.Length;
				DFA126_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA126_transition[i] = DFA.UnpackEncodedString(DFA126_transitionS[i]);
				}
			}

			public DFA126(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 126;
				this.eot = DFA126_eot;
				this.eof = DFA126_eof;
				this.min = DFA126_min;
				this.max = DFA126_max;
				this.accept = DFA126_accept;
				this.special = DFA126_special;
				this.transition = DFA126_transition;
			}

			public override string Description { get { return "418:51: ( table_conflict_clause )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA127 : DFA
		{
			private const string DFA127_eotS =
				"\x10\xFFFF";
			private const string DFA127_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA127_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA127_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA127_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA127_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA127_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x34\xFFFF\x1\x1\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF"+
				"\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA127_eot = DFA.UnpackEncodedString(DFA127_eotS);
			private static readonly short[] DFA127_eof = DFA.UnpackEncodedString(DFA127_eofS);
			private static readonly char[] DFA127_min = DFA.UnpackEncodedStringToUnsignedChars(DFA127_minS);
			private static readonly char[] DFA127_max = DFA.UnpackEncodedStringToUnsignedChars(DFA127_maxS);
			private static readonly short[] DFA127_accept = DFA.UnpackEncodedString(DFA127_acceptS);
			private static readonly short[] DFA127_special = DFA.UnpackEncodedString(DFA127_specialS);
			private static readonly short[][] DFA127_transition;

			static DFA127()
			{
				int numStates = DFA127_transitionS.Length;
				DFA127_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA127_transition[i] = DFA.UnpackEncodedString(DFA127_transitionS[i]);
				}
			}

			public DFA127(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 127;
				this.eot = DFA127_eot;
				this.eof = DFA127_eof;
				this.min = DFA127_min;
				this.max = DFA127_max;
				this.accept = DFA127_accept;
				this.special = DFA127_special;
				this.transition = DFA127_transition;
			}

			public override string Description { get { return "420:43: ( table_conflict_clause )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA128 : DFA
		{
			private const string DFA128_eotS =
				"\x10\xFFFF";
			private const string DFA128_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA128_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA128_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA128_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA128_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA128_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x34\xFFFF\x1\x1\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF"+
				"\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA128_eot = DFA.UnpackEncodedString(DFA128_eotS);
			private static readonly short[] DFA128_eof = DFA.UnpackEncodedString(DFA128_eofS);
			private static readonly char[] DFA128_min = DFA.UnpackEncodedStringToUnsignedChars(DFA128_minS);
			private static readonly char[] DFA128_max = DFA.UnpackEncodedStringToUnsignedChars(DFA128_maxS);
			private static readonly short[] DFA128_accept = DFA.UnpackEncodedString(DFA128_acceptS);
			private static readonly short[] DFA128_special = DFA.UnpackEncodedString(DFA128_specialS);
			private static readonly short[][] DFA128_transition;

			static DFA128()
			{
				int numStates = DFA128_transitionS.Length;
				DFA128_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA128_transition[i] = DFA.UnpackEncodedString(DFA128_transitionS[i]);
				}
			}

			public DFA128(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 128;
				this.eot = DFA128_eot;
				this.eof = DFA128_eof;
				this.min = DFA128_min;
				this.max = DFA128_max;
				this.accept = DFA128_accept;
				this.special = DFA128_special;
				this.transition = DFA128_transition;
			}

			public override string Description { get { return "422:38: ( table_conflict_clause )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA129 : DFA
		{
			private const string DFA129_eotS =
				"\x10\xFFFF";
			private const string DFA129_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA129_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA129_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA129_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA129_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA129_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x34\xFFFF\x1\x1\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF"+
				"\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA129_eot = DFA.UnpackEncodedString(DFA129_eotS);
			private static readonly short[] DFA129_eof = DFA.UnpackEncodedString(DFA129_eofS);
			private static readonly char[] DFA129_min = DFA.UnpackEncodedStringToUnsignedChars(DFA129_minS);
			private static readonly char[] DFA129_max = DFA.UnpackEncodedStringToUnsignedChars(DFA129_maxS);
			private static readonly short[] DFA129_accept = DFA.UnpackEncodedString(DFA129_acceptS);
			private static readonly short[] DFA129_special = DFA.UnpackEncodedString(DFA129_specialS);
			private static readonly short[][] DFA129_transition;

			static DFA129()
			{
				int numStates = DFA129_transitionS.Length;
				DFA129_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA129_transition[i] = DFA.UnpackEncodedString(DFA129_transitionS[i]);
				}
			}

			public DFA129(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 129;
				this.eot = DFA129_eot;
				this.eof = DFA129_eof;
				this.min = DFA129_min;
				this.max = DFA129_max;
				this.accept = DFA129_accept;
				this.special = DFA129_special;
				this.transition = DFA129_transition;
			}

			public override string Description { get { return "424:30: ( table_conflict_clause )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA130 : DFA
		{
			private const string DFA130_eotS =
				"\x10\xFFFF";
			private const string DFA130_eofS =
				"\x1\x2\xF\xFFFF";
			private const string DFA130_minS =
				"\x1\x20\xF\xFFFF";
			private const string DFA130_maxS =
				"\x1\xA0\xF\xFFFF";
			private const string DFA130_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\xD\xFFFF";
			private const string DFA130_specialS =
				"\x10\xFFFF}>";
			private static readonly string[] DFA130_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x34\xFFFF\x1\x1\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF"+
				"\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA130_eot = DFA.UnpackEncodedString(DFA130_eotS);
			private static readonly short[] DFA130_eof = DFA.UnpackEncodedString(DFA130_eofS);
			private static readonly char[] DFA130_min = DFA.UnpackEncodedStringToUnsignedChars(DFA130_minS);
			private static readonly char[] DFA130_max = DFA.UnpackEncodedStringToUnsignedChars(DFA130_maxS);
			private static readonly short[] DFA130_accept = DFA.UnpackEncodedString(DFA130_acceptS);
			private static readonly short[] DFA130_special = DFA.UnpackEncodedString(DFA130_specialS);
			private static readonly short[][] DFA130_transition;

			static DFA130()
			{
				int numStates = DFA130_transitionS.Length;
				DFA130_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA130_transition[i] = DFA.UnpackEncodedString(DFA130_transitionS[i]);
				}
			}

			public DFA130(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 130;
				this.eot = DFA130_eot;
				this.eof = DFA130_eof;
				this.min = DFA130_min;
				this.max = DFA130_max;
				this.accept = DFA130_accept;
				this.special = DFA130_special;
				this.transition = DFA130_transition;
			}

			public override string Description { get { return "426:35: ( table_conflict_clause )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA132 : DFA
		{
			private const string DFA132_eotS =
				"\xB\xFFFF";
			private const string DFA132_eofS =
				"\xB\xFFFF";
			private const string DFA132_minS =
				"\x1\x2C\xA\xFFFF";
			private const string DFA132_maxS =
				"\x1\x5B\xA\xFFFF";
			private const string DFA132_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x7\xFFFF\x1\x3";
			private const string DFA132_specialS =
				"\xB\xFFFF}>";
			private static readonly string[] DFA132_transitionS =
			{
				"\x1\xA\x5\xFFFF\x1\x2\x11\xFFFF\x2\x1\xF\xFFFF\x7\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA132_eot = DFA.UnpackEncodedString(DFA132_eotS);
			private static readonly short[] DFA132_eof = DFA.UnpackEncodedString(DFA132_eofS);
			private static readonly char[] DFA132_min = DFA.UnpackEncodedStringToUnsignedChars(DFA132_minS);
			private static readonly char[] DFA132_max = DFA.UnpackEncodedStringToUnsignedChars(DFA132_maxS);
			private static readonly short[] DFA132_accept = DFA.UnpackEncodedString(DFA132_acceptS);
			private static readonly short[] DFA132_special = DFA.UnpackEncodedString(DFA132_specialS);
			private static readonly short[][] DFA132_transition;

			static DFA132()
			{
				int numStates = DFA132_transitionS.Length;
				DFA132_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA132_transition[i] = DFA.UnpackEncodedString(DFA132_transitionS[i]);
				}
			}

			public DFA132(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 132;
				this.eot = DFA132_eot;
				this.eof = DFA132_eof;
				this.min = DFA132_min;
				this.max = DFA132_max;
				this.accept = DFA132_accept;
				this.special = DFA132_special;
				this.transition = DFA132_transition;
			}

			public override string Description { get { return "438:37: ( signed_default_number | literal_value | LPAREN expr RPAREN )"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA143 : DFA
		{
			private const string DFA143_eotS =
				"\x13\xFFFF";
			private const string DFA143_eofS =
				"\x1\x2\x12\xFFFF";
			private const string DFA143_minS =
				"\x1\x20\x12\xFFFF";
			private const string DFA143_maxS =
				"\x1\xA3\x12\xFFFF";
			private const string DFA143_acceptS =
				"\x1\xFFFF\x1\x1\x1\x2\x10\xFFFF";
			private const string DFA143_specialS =
				"\x13\xFFFF}>";
			private static readonly string[] DFA143_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x4\xFFFF\x1\x1\x2\x2\x3\xFFFF\x1\x2\x8\xFFFF"+
				"\x1\x2\xF\xFFFF\x1\x2\x34\xFFFF\x1\x2\x4\xFFFF\x1\x2\x13\xFFFF\x2\x2"+
				"\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2\x2\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA143_eot = DFA.UnpackEncodedString(DFA143_eotS);
			private static readonly short[] DFA143_eof = DFA.UnpackEncodedString(DFA143_eofS);
			private static readonly char[] DFA143_min = DFA.UnpackEncodedStringToUnsignedChars(DFA143_minS);
			private static readonly char[] DFA143_max = DFA.UnpackEncodedStringToUnsignedChars(DFA143_maxS);
			private static readonly short[] DFA143_accept = DFA.UnpackEncodedString(DFA143_acceptS);
			private static readonly short[] DFA143_special = DFA.UnpackEncodedString(DFA143_specialS);
			private static readonly short[][] DFA143_transition;

			static DFA143()
			{
				int numStates = DFA143_transitionS.Length;
				DFA143_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA143_transition[i] = DFA.UnpackEncodedString(DFA143_transitionS[i]);
				}
			}

			public DFA143(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 143;
				this.eot = DFA143_eot;
				this.eof = DFA143_eof;
				this.min = DFA143_min;
				this.max = DFA143_max;
				this.accept = DFA143_accept;
				this.special = DFA143_special;
				this.transition = DFA143_transition;
			}

			public override string Description { get { return "467:40: ( LPAREN column_names+= id ( COMMA column_names+= id )* RPAREN )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA144 : DFA
		{
			private const string DFA144_eotS =
				"\x12\xFFFF";
			private const string DFA144_eofS =
				"\x1\x1\x11\xFFFF";
			private const string DFA144_minS =
				"\x1\x20\x11\xFFFF";
			private const string DFA144_maxS =
				"\x1\xA3\x11\xFFFF";
			private const string DFA144_acceptS =
				"\x1\xFFFF\x1\x2\xE\xFFFF\x1\x1\x1\xFFFF";
			private const string DFA144_specialS =
				"\x12\xFFFF}>";
			private static readonly string[] DFA144_transitionS =
			{
				"\x1\x1\x6\xFFFF\x1\x1\x5\xFFFF\x2\x1\x3\xFFFF\x1\x1\x8\xFFFF\x1\x10"+
				"\xF\xFFFF\x1\x1\x34\xFFFF\x1\x10\x4\xFFFF\x1\x1\x13\xFFFF\x2\x1\x1\xFFFF"+
				"\x3\x1\x1\xFFFF\x1\x1\x2\xFFFF\x1\x1",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA144_eot = DFA.UnpackEncodedString(DFA144_eotS);
			private static readonly short[] DFA144_eof = DFA.UnpackEncodedString(DFA144_eofS);
			private static readonly char[] DFA144_min = DFA.UnpackEncodedStringToUnsignedChars(DFA144_minS);
			private static readonly char[] DFA144_max = DFA.UnpackEncodedStringToUnsignedChars(DFA144_maxS);
			private static readonly short[] DFA144_accept = DFA.UnpackEncodedString(DFA144_acceptS);
			private static readonly short[] DFA144_special = DFA.UnpackEncodedString(DFA144_specialS);
			private static readonly short[][] DFA144_transition;

			static DFA144()
			{
				int numStates = DFA144_transitionS.Length;
				DFA144_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA144_transition[i] = DFA.UnpackEncodedString(DFA144_transitionS[i]);
				}
			}

			public DFA144(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 144;
				this.eot = DFA144_eot;
				this.eof = DFA144_eof;
				this.min = DFA144_min;
				this.max = DFA144_max;
				this.accept = DFA144_accept;
				this.special = DFA144_special;
				this.transition = DFA144_transition;
			}

			public override string Description { get { return "()* loopback of 468:3: ( fk_clause_action )*"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA145 : DFA
		{
			private const string DFA145_eotS =
				"\x12\xFFFF";
			private const string DFA145_eofS =
				"\x1\x3\x11\xFFFF";
			private const string DFA145_minS =
				"\x1\x20\x1\x32\x10\xFFFF";
			private const string DFA145_maxS =
				"\x2\xA3\x10\xFFFF";
			private const string DFA145_acceptS =
				"\x2\xFFFF\x1\x1\x1\x2\xE\xFFFF";
			private const string DFA145_specialS =
				"\x12\xFFFF}>";
			private static readonly string[] DFA145_transitionS =
			{
				"\x1\x3\x6\xFFFF\x1\x1\x5\xFFFF\x2\x3\x3\xFFFF\x1\x3\x18\xFFFF\x1\x3"+
				"\x39\xFFFF\x1\x3\x13\xFFFF\x2\x3\x1\xFFFF\x3\x3\x1\xFFFF\x1\x3\x2\xFFFF"+
				"\x1\x2",
				"\x1\x3\x70\xFFFF\x1\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA145_eot = DFA.UnpackEncodedString(DFA145_eotS);
			private static readonly short[] DFA145_eof = DFA.UnpackEncodedString(DFA145_eofS);
			private static readonly char[] DFA145_min = DFA.UnpackEncodedStringToUnsignedChars(DFA145_minS);
			private static readonly char[] DFA145_max = DFA.UnpackEncodedStringToUnsignedChars(DFA145_maxS);
			private static readonly short[] DFA145_accept = DFA.UnpackEncodedString(DFA145_acceptS);
			private static readonly short[] DFA145_special = DFA.UnpackEncodedString(DFA145_specialS);
			private static readonly short[][] DFA145_transition;

			static DFA145()
			{
				int numStates = DFA145_transitionS.Length;
				DFA145_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA145_transition[i] = DFA.UnpackEncodedString(DFA145_transitionS[i]);
				}
			}

			public DFA145(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 145;
				this.eot = DFA145_eot;
				this.eof = DFA145_eof;
				this.min = DFA145_min;
				this.max = DFA145_max;
				this.accept = DFA145_accept;
				this.special = DFA145_special;
				this.transition = DFA145_transition;
			}

			public override string Description { get { return "468:21: ( fk_clause_deferrable )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA149 : DFA
		{
			private const string DFA149_eotS =
				"\x12\xFFFF";
			private const string DFA149_eofS =
				"\x1\x2\x11\xFFFF";
			private const string DFA149_minS =
				"\x1\x20\x1\x8A\x10\xFFFF";
			private const string DFA149_maxS =
				"\x1\xA4\x1\x8B\x10\xFFFF";
			private const string DFA149_acceptS =
				"\x2\xFFFF\x1\x3\xD\xFFFF\x1\x1\x1\x2";
			private const string DFA149_specialS =
				"\x12\xFFFF}>";
			private static readonly string[] DFA149_transitionS =
			{
				"\x1\x2\x6\xFFFF\x1\x2\x5\xFFFF\x2\x2\x3\xFFFF\x1\x2\x18\xFFFF\x1\x2"+
				"\x39\xFFFF\x1\x2\x13\xFFFF\x2\x2\x1\xFFFF\x3\x2\x1\xFFFF\x1\x2\x3\xFFFF"+
				"\x1\x1",
				"\x1\x10\x1\x11",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA149_eot = DFA.UnpackEncodedString(DFA149_eotS);
			private static readonly short[] DFA149_eof = DFA.UnpackEncodedString(DFA149_eofS);
			private static readonly char[] DFA149_min = DFA.UnpackEncodedStringToUnsignedChars(DFA149_minS);
			private static readonly char[] DFA149_max = DFA.UnpackEncodedStringToUnsignedChars(DFA149_maxS);
			private static readonly short[] DFA149_accept = DFA.UnpackEncodedString(DFA149_acceptS);
			private static readonly short[] DFA149_special = DFA.UnpackEncodedString(DFA149_specialS);
			private static readonly short[][] DFA149_transition;

			static DFA149()
			{
				int numStates = DFA149_transitionS.Length;
				DFA149_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA149_transition[i] = DFA.UnpackEncodedString(DFA149_transitionS[i]);
				}
			}

			public DFA149(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 149;
				this.eot = DFA149_eot;
				this.eof = DFA149_eof;
				this.min = DFA149_min;
				this.max = DFA149_max;
				this.accept = DFA149_accept;
				this.special = DFA149_special;
				this.transition = DFA149_transition;
			}

			public override string Description { get { return "475:42: ( INITIALLY DEFERRED | INITIALLY IMMEDIATE )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA169 : DFA
		{
			private const string DFA169_eotS =
				"\xC\xFFFF";
			private const string DFA169_eofS =
				"\xC\xFFFF";
			private const string DFA169_minS =
				"\x1\x21\x1\x24\xA\xFFFF";
			private const string DFA169_maxS =
				"\x1\xB3\x1\xAF\xA\xFFFF";
			private const string DFA169_acceptS =
				"\x2\xFFFF\x1\x2\x1\xFFFF\x1\x1\x7\xFFFF";
			private const string DFA169_specialS =
				"\xC\xFFFF}>";
			private static readonly string[] DFA169_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\xA\x2"+
				"\x2\xFFFF\x1\x2\x1\xFFFF\x3\x2\x3\xFFFF\x38\x2\x1\x1\x1C\x2",
				"\x1\x2\x2\xFFFF\x1\x4\x5A\xFFFF\x1\x2\x3\xFFFF\x1\x2\x1\xFFFF\x1\x2"+
				"\x24\xFFFF\x3\x2",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA169_eot = DFA.UnpackEncodedString(DFA169_eotS);
			private static readonly short[] DFA169_eof = DFA.UnpackEncodedString(DFA169_eofS);
			private static readonly char[] DFA169_min = DFA.UnpackEncodedStringToUnsignedChars(DFA169_minS);
			private static readonly char[] DFA169_max = DFA.UnpackEncodedStringToUnsignedChars(DFA169_maxS);
			private static readonly short[] DFA169_accept = DFA.UnpackEncodedString(DFA169_acceptS);
			private static readonly short[] DFA169_special = DFA.UnpackEncodedString(DFA169_specialS);
			private static readonly short[][] DFA169_transition;

			static DFA169()
			{
				int numStates = DFA169_transitionS.Length;
				DFA169_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA169_transition[i] = DFA.UnpackEncodedString(DFA169_transitionS[i]);
				}
			}

			public DFA169(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 169;
				this.eot = DFA169_eot;
				this.eof = DFA169_eof;
				this.min = DFA169_min;
				this.max = DFA169_max;
				this.accept = DFA169_accept;
				this.special = DFA169_special;
				this.transition = DFA169_transition;
			}

			public override string Description { get { return "503:48: ( IF NOT EXISTS )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA170 : DFA
		{
			private const string DFA170_eotS =
				"\x11\xFFFF";
			private const string DFA170_eofS =
				"\x11\xFFFF";
			private const string DFA170_minS =
				"\x1\x21\x2\x24\xE\xFFFF";
			private const string DFA170_maxS =
				"\x1\xB3\x2\xAF\xE\xFFFF";
			private const string DFA170_acceptS =
				"\x3\xFFFF\x1\x1\x1\x2\xC\xFFFF";
			private const string DFA170_specialS =
				"\x11\xFFFF}>";
			private static readonly string[] DFA170_transitionS =
			{
				"\x3\x2\x1\xFFFF\x2\x2\x1\xFFFF\x3\x2\x6\xFFFF\x3\x2\x17\xFFFF\x1\x2"+
				"\x1\x1\x8\x2\x2\xFFFF\x1\x1\x1\xFFFF\x3\x2\x3\xFFFF\x55\x2",
				"\x1\x3\x5D\xFFFF\x1\x4\x3\xFFFF\x1\x4\x1\xFFFF\x1\x4\x24\xFFFF\x3"+
				"\x4",
				"\x1\x3\x5D\xFFFF\x1\x4\x3\xFFFF\x1\x4\x1\xFFFF\x1\x4\x24\xFFFF\x3"+
				"\x4",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				""
			};

			private static readonly short[] DFA170_eot = DFA.UnpackEncodedString(DFA170_eotS);
			private static readonly short[] DFA170_eof = DFA.UnpackEncodedString(DFA170_eofS);
			private static readonly char[] DFA170_min = DFA.UnpackEncodedStringToUnsignedChars(DFA170_minS);
			private static readonly char[] DFA170_max = DFA.UnpackEncodedStringToUnsignedChars(DFA170_maxS);
			private static readonly short[] DFA170_accept = DFA.UnpackEncodedString(DFA170_acceptS);
			private static readonly short[] DFA170_special = DFA.UnpackEncodedString(DFA170_specialS);
			private static readonly short[][] DFA170_transition;

			static DFA170()
			{
				int numStates = DFA170_transitionS.Length;
				DFA170_transition = new short[numStates][];
				for (int i = 0; i < numStates; i++)
				{
					DFA170_transition[i] = DFA.UnpackEncodedString(DFA170_transitionS[i]);
				}
			}

			public DFA170(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 170;
				this.eot = DFA170_eot;
				this.eof = DFA170_eof;
				this.min = DFA170_min;
				this.max = DFA170_max;
				this.accept = DFA170_accept;
				this.special = DFA170_special;
				this.transition = DFA170_transition;
			}

			public override string Description { get { return "503:65: (database_name= id DOT )?"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}


		#endregion DFA

		#region Follow sets
		private static class Follow
		{
			public static readonly BitSet _sql_stmt_in_sql_stmt_list212 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _SEMI_in_sql_stmt_list215 = new BitSet(new ulong[] { 0x200000000UL, 0x200FB200040000UL, 0x60000B4344UL });
			public static readonly BitSet _sql_stmt_in_sql_stmt_list219 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _SEMI_in_sql_stmt_list221 = new BitSet(new ulong[] { 0x200000000UL, 0x200FB200040000UL, 0x60000B4344UL });
			public static readonly BitSet _EOF_in_sql_stmt_list229 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _sql_stmt_in_sql_stmt_itself237 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _SEMI_in_sql_stmt_itself240 = new BitSet(new ulong[] { 0x0UL });
			public static readonly BitSet _EOF_in_sql_stmt_itself245 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _EXPLAIN_in_sql_stmt254 = new BitSet(new ulong[] { 0x600000000UL, 0x200FB200040000UL, 0x60000B4344UL });
			public static readonly BitSet _QUERY_in_sql_stmt257 = new BitSet(new ulong[] { 0x800000000UL });
			public static readonly BitSet _PLAN_in_sql_stmt259 = new BitSet(new ulong[] { 0x200000000UL, 0x200FB200040000UL, 0x60000B4344UL });
			public static readonly BitSet _sql_stmt_core_in_sql_stmt265 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _pragma_stmt_in_sql_stmt_core275 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _attach_stmt_in_sql_stmt_core281 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _detach_stmt_in_sql_stmt_core287 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _analyze_stmt_in_sql_stmt_core293 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _reindex_stmt_in_sql_stmt_core299 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _vacuum_stmt_in_sql_stmt_core305 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _select_stmt_in_sql_stmt_core314 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _insert_stmt_in_sql_stmt_core320 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _update_stmt_in_sql_stmt_core326 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _delete_stmt_in_sql_stmt_core332 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _begin_stmt_in_sql_stmt_core338 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _commit_stmt_in_sql_stmt_core344 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _rollback_stmt_in_sql_stmt_core350 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _savepoint_stmt_in_sql_stmt_core356 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _release_stmt_in_sql_stmt_core362 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_virtual_table_stmt_in_sql_stmt_core371 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_table_stmt_in_sql_stmt_core377 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _drop_table_stmt_in_sql_stmt_core383 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _alter_table_stmt_in_sql_stmt_core389 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_view_stmt_in_sql_stmt_core395 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _drop_view_stmt_in_sql_stmt_core401 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_index_stmt_in_sql_stmt_core407 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _drop_index_stmt_in_sql_stmt_core413 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_trigger_stmt_in_sql_stmt_core419 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _drop_trigger_stmt_in_sql_stmt_core425 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_virtual_table_stmt_in_schema_create_table_stmt435 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _create_table_stmt_in_schema_create_table_stmt439 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_in_qualified_table_name449 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_qualified_table_name451 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_qualified_table_name457 = new BitSet(new ulong[] { 0xA000000002UL });
			public static readonly BitSet _INDEXED_in_qualified_table_name460 = new BitSet(new ulong[] { 0x4000000000UL });
			public static readonly BitSet _BY_in_qualified_table_name462 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_qualified_table_name466 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_qualified_table_name470 = new BitSet(new ulong[] { 0x2000000000UL });
			public static readonly BitSet _INDEXED_in_qualified_table_name472 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _or_subexpr_in_expr481 = new BitSet(new ulong[] { 0x10000000002UL });
			public static readonly BitSet _OR_in_expr484 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _or_subexpr_in_expr487 = new BitSet(new ulong[] { 0x10000000002UL });
			public static readonly BitSet _and_subexpr_in_or_subexpr496 = new BitSet(new ulong[] { 0x20000000002UL });
			public static readonly BitSet _AND_in_or_subexpr499 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _and_subexpr_in_or_subexpr502 = new BitSet(new ulong[] { 0x20000000002UL });
			public static readonly BitSet _eq_subexpr_in_and_subexpr511 = new BitSet(new ulong[] { 0xFFB888000000002UL });
			public static readonly BitSet _cond_expr_in_and_subexpr513 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_cond_expr525 = new BitSet(new ulong[] { 0xF00008000000000UL });
			public static readonly BitSet _match_op_in_cond_expr528 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _eq_subexpr_in_cond_expr532 = new BitSet(new ulong[] { 0x40000000002UL });
			public static readonly BitSet _ESCAPE_in_cond_expr535 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _eq_subexpr_in_cond_expr539 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_cond_expr567 = new BitSet(new ulong[] { 0x80000000000UL });
			public static readonly BitSet _IN_in_cond_expr570 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_cond_expr572 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_cond_expr574 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_cond_expr577 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_cond_expr579 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_cond_expr583 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_cond_expr605 = new BitSet(new ulong[] { 0x80000000000UL });
			public static readonly BitSet _IN_in_cond_expr608 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_cond_expr613 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_cond_expr615 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_cond_expr621 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ISNULL_in_cond_expr652 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOTNULL_in_cond_expr660 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _IS_in_cond_expr668 = new BitSet(new ulong[] { 0x4000000000000UL });
			public static readonly BitSet _NULL_in_cond_expr670 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_cond_expr678 = new BitSet(new ulong[] { 0x4000000000000UL });
			public static readonly BitSet _NULL_in_cond_expr680 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _IS_in_cond_expr688 = new BitSet(new ulong[] { 0x8000000000UL });
			public static readonly BitSet _NOT_in_cond_expr690 = new BitSet(new ulong[] { 0x4000000000000UL });
			public static readonly BitSet _NULL_in_cond_expr692 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_cond_expr703 = new BitSet(new ulong[] { 0x8000000000000UL });
			public static readonly BitSet _BETWEEN_in_cond_expr706 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _eq_subexpr_in_cond_expr710 = new BitSet(new ulong[] { 0x20000000000UL });
			public static readonly BitSet _AND_in_cond_expr712 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _eq_subexpr_in_cond_expr716 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_cond_expr742 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _eq_subexpr_in_cond_expr759 = new BitSet(new ulong[] { 0xF0000000000002UL });
			public static readonly BitSet _set_in_match_op0 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _neq_subexpr_in_eq_subexpr792 = new BitSet(new ulong[] { 0xF000000000000002UL });
			public static readonly BitSet _set_in_eq_subexpr795 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _neq_subexpr_in_eq_subexpr812 = new BitSet(new ulong[] { 0xF000000000000002UL });
			public static readonly BitSet _bit_subexpr_in_neq_subexpr821 = new BitSet(new ulong[] { 0x2UL, 0xFUL });
			public static readonly BitSet _set_in_neq_subexpr824 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _bit_subexpr_in_neq_subexpr841 = new BitSet(new ulong[] { 0x2UL, 0xFUL });
			public static readonly BitSet _add_subexpr_in_bit_subexpr850 = new BitSet(new ulong[] { 0x2UL, 0x30UL });
			public static readonly BitSet _set_in_bit_subexpr853 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _add_subexpr_in_bit_subexpr862 = new BitSet(new ulong[] { 0x2UL, 0x30UL });
			public static readonly BitSet _mul_subexpr_in_add_subexpr871 = new BitSet(new ulong[] { 0x2UL, 0x1C0UL });
			public static readonly BitSet _set_in_add_subexpr874 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _mul_subexpr_in_add_subexpr887 = new BitSet(new ulong[] { 0x2UL, 0x1C0UL });
			public static readonly BitSet _con_subexpr_in_mul_subexpr896 = new BitSet(new ulong[] { 0x2UL, 0x200UL });
			public static readonly BitSet _DOUBLE_PIPE_in_mul_subexpr899 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _con_subexpr_in_mul_subexpr902 = new BitSet(new ulong[] { 0x2UL, 0x200UL });
			public static readonly BitSet _unary_subexpr_in_con_subexpr911 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _unary_op_in_con_subexpr915 = new BitSet(new ulong[] { 0xE176E00000000UL, 0xFFFFFFFFFFFFF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _unary_subexpr_in_con_subexpr917 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_unary_op0 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _atom_expr_in_unary_subexpr951 = new BitSet(new ulong[] { 0x2UL, 0x800UL });
			public static readonly BitSet _COLLATE_in_unary_subexpr954 = new BitSet(new ulong[] { 0x0UL, 0x1000UL });
			public static readonly BitSet _ID_in_unary_subexpr959 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _literal_value_in_atom_expr971 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _bind_parameter_in_atom_expr977 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_in_atom_expr987 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_atom_expr989 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_atom_expr995 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_atom_expr997 = new BitSet(new ulong[] { 0x0UL, 0x1000UL });
			public static readonly BitSet _ID_in_atom_expr1003 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ID_in_atom_expr1032 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_atom_expr1034 = new BitSet(new ulong[] { 0xE57EE00000000UL, 0xFFFFFFFFFFFFFC70UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _DISTINCT_in_atom_expr1037 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_atom_expr1042 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_atom_expr1045 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_atom_expr1049 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _ASTERISK_in_atom_expr1055 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_atom_expr1059 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _LPAREN_in_atom_expr1084 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_atom_expr1087 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_atom_expr1089 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CAST_in_atom_expr1096 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_atom_expr1099 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_atom_expr1102 = new BitSet(new ulong[] { 0x0UL, 0x8000UL });
			public static readonly BitSet _AS_in_atom_expr1104 = new BitSet(new ulong[] { 0x0UL, 0x1000UL });
			public static readonly BitSet _type_name_in_atom_expr1107 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_atom_expr1109 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CASE_in_atom_expr1118 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_atom_expr1123 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _when_expr_in_atom_expr1127 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ELSE_in_atom_expr1131 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_atom_expr1135 = new BitSet(new ulong[] { 0x0UL, 0x40000UL });
			public static readonly BitSet _END_in_atom_expr1139 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _raise_function_in_atom_expr1162 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _WHEN_in_when_expr1172 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_when_expr1176 = new BitSet(new ulong[] { 0x0UL, 0x100000UL });
			public static readonly BitSet _THEN_in_when_expr1178 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_when_expr1182 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _INTEGER_in_literal_value1204 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _FLOAT_in_literal_value1218 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _STRING_in_literal_value1232 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _BLOB_in_literal_value1246 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NULL_in_literal_value1260 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CURRENT_TIME_in_literal_value1266 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CURRENT_DATE_in_literal_value1280 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CURRENT_TIMESTAMP_in_literal_value1294 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _QUESTION_in_bind_parameter1315 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _QUESTION_in_bind_parameter1325 = new BitSet(new ulong[] { 0x0UL, 0x200000UL });
			public static readonly BitSet _INTEGER_in_bind_parameter1329 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _COLON_in_bind_parameter1344 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_bind_parameter1348 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _AT_in_bind_parameter1363 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_bind_parameter1367 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _RAISE_in_raise_function1388 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_raise_function1391 = new BitSet(new ulong[] { 0x0UL, 0xF00000000UL });
			public static readonly BitSet _IGNORE_in_raise_function1395 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _set_in_raise_function1399 = new BitSet(new ulong[] { 0x200000000000UL });
			public static readonly BitSet _COMMA_in_raise_function1411 = new BitSet(new ulong[] { 0x0UL, 0x800000UL });
			public static readonly BitSet _STRING_in_raise_function1416 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_raise_function1419 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ID_in_type_name1429 = new BitSet(new ulong[] { 0x100000000002UL, 0x1000UL });
			public static readonly BitSet _LPAREN_in_type_name1433 = new BitSet(new ulong[] { 0x0UL, 0x600030UL });
			public static readonly BitSet _signed_number_in_type_name1437 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_type_name1440 = new BitSet(new ulong[] { 0x0UL, 0x600030UL });
			public static readonly BitSet _signed_number_in_type_name1444 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_type_name1448 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_signed_number1479 = new BitSet(new ulong[] { 0x0UL, 0x600000UL });
			public static readonly BitSet _set_in_signed_number1488 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _PRAGMA_in_pragma_stmt1502 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_pragma_stmt1507 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_pragma_stmt1509 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_pragma_stmt1515 = new BitSet(new ulong[] { 0x10100000000002UL });
			public static readonly BitSet _EQUALS_in_pragma_stmt1518 = new BitSet(new ulong[] { 0x0UL, 0xE01030UL });
			public static readonly BitSet _pragma_value_in_pragma_stmt1520 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _LPAREN_in_pragma_stmt1524 = new BitSet(new ulong[] { 0x0UL, 0xE01030UL });
			public static readonly BitSet _pragma_value_in_pragma_stmt1526 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_pragma_stmt1528 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _signed_number_in_pragma_value1557 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ID_in_pragma_value1570 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _STRING_in_pragma_value1583 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ATTACH_in_attach_stmt1601 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _DATABASE_in_attach_stmt1604 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_attach_stmt1610 = new BitSet(new ulong[] { 0x0UL, 0x8000UL });
			public static readonly BitSet _AS_in_attach_stmt1612 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_attach_stmt1616 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DETACH_in_detach_stmt1624 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _DATABASE_in_detach_stmt1627 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_detach_stmt1633 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ANALYZE_in_analyze_stmt1641 = new BitSet(new ulong[] { 0xE076E00000002UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_analyze_stmt1646 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_in_analyze_stmt1652 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_analyze_stmt1654 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_analyze_stmt1658 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _REINDEX_in_reindex_stmt1668 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_reindex_stmt1673 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_reindex_stmt1675 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_reindex_stmt1681 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _VACUUM_in_vacuum_stmt1689 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _OR_in_operation_conflict_clause1700 = new BitSet(new ulong[] { 0x0UL, 0x80F00000000UL });
			public static readonly BitSet _set_in_operation_conflict_clause1702 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _expr_in_ordering_term1727 = new BitSet(new ulong[] { 0x2UL, 0x300000000000UL });
			public static readonly BitSet _ASC_in_ordering_term1732 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DESC_in_ordering_term1736 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ORDER_in_operation_limited_clause1766 = new BitSet(new ulong[] { 0x4000000000UL });
			public static readonly BitSet _BY_in_operation_limited_clause1768 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ordering_term_in_operation_limited_clause1770 = new BitSet(new ulong[] { 0x200000000000UL, 0x800000000000UL });
			public static readonly BitSet _COMMA_in_operation_limited_clause1773 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ordering_term_in_operation_limited_clause1775 = new BitSet(new ulong[] { 0x200000000000UL, 0x800000000000UL });
			public static readonly BitSet _LIMIT_in_operation_limited_clause1783 = new BitSet(new ulong[] { 0x0UL, 0x200000UL });
			public static readonly BitSet _INTEGER_in_operation_limited_clause1787 = new BitSet(new ulong[] { 0x200000000002UL, 0x1000000000000UL });
			public static readonly BitSet _set_in_operation_limited_clause1790 = new BitSet(new ulong[] { 0x0UL, 0x200000UL });
			public static readonly BitSet _INTEGER_in_operation_limited_clause1800 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _select_list_in_select_stmt1810 = new BitSet(new ulong[] { 0x2UL, 0xC00000000000UL });
			public static readonly BitSet _ORDER_in_select_stmt1815 = new BitSet(new ulong[] { 0x4000000000UL });
			public static readonly BitSet _BY_in_select_stmt1817 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ordering_term_in_select_stmt1819 = new BitSet(new ulong[] { 0x200000000002UL, 0x800000000000UL });
			public static readonly BitSet _COMMA_in_select_stmt1822 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ordering_term_in_select_stmt1824 = new BitSet(new ulong[] { 0x200000000002UL, 0x800000000000UL });
			public static readonly BitSet _LIMIT_in_select_stmt1833 = new BitSet(new ulong[] { 0x0UL, 0x200000UL });
			public static readonly BitSet _INTEGER_in_select_stmt1837 = new BitSet(new ulong[] { 0x200000000002UL, 0x1000000000000UL });
			public static readonly BitSet _OFFSET_in_select_stmt1841 = new BitSet(new ulong[] { 0x0UL, 0x200000UL });
			public static readonly BitSet _COMMA_in_select_stmt1845 = new BitSet(new ulong[] { 0x0UL, 0x200000UL });
			public static readonly BitSet _INTEGER_in_select_stmt1850 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _select_core_in_select_list1895 = new BitSet(new ulong[] { 0x2UL, 0x1A000000000000UL });
			public static readonly BitSet _select_op_in_select_list1898 = new BitSet(new ulong[] { 0x0UL, 0x20000000000000UL });
			public static readonly BitSet _select_core_in_select_list1901 = new BitSet(new ulong[] { 0x2UL, 0x1A000000000000UL });
			public static readonly BitSet _UNION_in_select_op1910 = new BitSet(new ulong[] { 0x2UL, 0x4000000000000UL });
			public static readonly BitSet _ALL_in_select_op1914 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _INTERSECT_in_select_op1920 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _EXCEPT_in_select_op1924 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _SELECT_in_select_core1933 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC70UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ALL_in_select_core1936 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC70UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _DISTINCT_in_select_core1940 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC70UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _result_column_in_select_core1944 = new BitSet(new ulong[] { 0x200000000002UL, 0x1C0000000000000UL });
			public static readonly BitSet _COMMA_in_select_core1947 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC70UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _result_column_in_select_core1949 = new BitSet(new ulong[] { 0x200000000002UL, 0x1C0000000000000UL });
			public static readonly BitSet _FROM_in_select_core1954 = new BitSet(new ulong[] { 0xE176E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _join_source_in_select_core1956 = new BitSet(new ulong[] { 0x2UL, 0x180000000000000UL });
			public static readonly BitSet _WHERE_in_select_core1961 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_select_core1965 = new BitSet(new ulong[] { 0x2UL, 0x100000000000000UL });
			public static readonly BitSet _GROUP_in_select_core1973 = new BitSet(new ulong[] { 0x4000000000UL });
			public static readonly BitSet _BY_in_select_core1975 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ordering_term_in_select_core1977 = new BitSet(new ulong[] { 0x200000000002UL, 0x200000000000000UL });
			public static readonly BitSet _COMMA_in_select_core1980 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _ordering_term_in_select_core1982 = new BitSet(new ulong[] { 0x200000000002UL, 0x200000000000000UL });
			public static readonly BitSet _HAVING_in_select_core1987 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_select_core1991 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ASTERISK_in_result_column2061 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_in_result_column2069 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_result_column2071 = new BitSet(new ulong[] { 0x0UL, 0x40UL });
			public static readonly BitSet _ASTERISK_in_result_column2073 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _expr_in_result_column2088 = new BitSet(new ulong[] { 0xE076E00000002UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _AS_in_result_column2092 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_result_column2098 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _single_source_in_join_source2119 = new BitSet(new ulong[] { 0x200000000002UL, 0xFC00000000000000UL });
			public static readonly BitSet _join_op_in_join_source2122 = new BitSet(new ulong[] { 0xE176E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _single_source_in_join_source2125 = new BitSet(new ulong[] { 0x200000000002UL, 0xFC00000000000000UL, 0x3UL });
			public static readonly BitSet _join_constraint_in_join_source2128 = new BitSet(new ulong[] { 0x200000000002UL, 0xFC00000000000000UL });
			public static readonly BitSet _id_in_single_source2145 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_single_source2147 = new BitSet(new ulong[] { 0x0UL, 0x1000UL });
			public static readonly BitSet _ID_in_single_source2153 = new BitSet(new ulong[] { 0xA000000002UL, 0x9000UL });
			public static readonly BitSet _AS_in_single_source2157 = new BitSet(new ulong[] { 0x0UL, 0x1000UL });
			public static readonly BitSet _ID_in_single_source2163 = new BitSet(new ulong[] { 0xA000000002UL });
			public static readonly BitSet _INDEXED_in_single_source2168 = new BitSet(new ulong[] { 0x4000000000UL });
			public static readonly BitSet _BY_in_single_source2170 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_single_source2174 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_single_source2178 = new BitSet(new ulong[] { 0x2000000000UL });
			public static readonly BitSet _INDEXED_in_single_source2180 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _LPAREN_in_single_source2221 = new BitSet(new ulong[] { 0x0UL, 0x20000000000000UL });
			public static readonly BitSet _select_stmt_in_single_source2223 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_single_source2225 = new BitSet(new ulong[] { 0x2UL, 0x9000UL });
			public static readonly BitSet _AS_in_single_source2229 = new BitSet(new ulong[] { 0x0UL, 0x1000UL });
			public static readonly BitSet _ID_in_single_source2235 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _LPAREN_in_single_source2257 = new BitSet(new ulong[] { 0xE176E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _join_source_in_single_source2260 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_single_source2262 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _COMMA_in_join_op2273 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NATURAL_in_join_op2280 = new BitSet(new ulong[] { 0x0UL, 0xF800000000000000UL });
			public static readonly BitSet _LEFT_in_join_op2286 = new BitSet(new ulong[] { 0x0UL, 0x9000000000000000UL });
			public static readonly BitSet _OUTER_in_join_op2291 = new BitSet(new ulong[] { 0x0UL, 0x8000000000000000UL });
			public static readonly BitSet _INNER_in_join_op2297 = new BitSet(new ulong[] { 0x0UL, 0x8000000000000000UL });
			public static readonly BitSet _CROSS_in_join_op2301 = new BitSet(new ulong[] { 0x0UL, 0x8000000000000000UL });
			public static readonly BitSet _JOIN_in_join_op2304 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ON_in_join_constraint2315 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_join_constraint2318 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _USING_in_join_constraint2324 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_join_constraint2326 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_join_constraint2330 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_join_constraint2333 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_join_constraint2337 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_join_constraint2341 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _INSERT_in_insert_stmt2360 = new BitSet(new ulong[] { 0x10000000000UL, 0x0UL, 0x8UL });
			public static readonly BitSet _operation_conflict_clause_in_insert_stmt2363 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8UL });
			public static readonly BitSet _REPLACE_in_insert_stmt2369 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8UL });
			public static readonly BitSet _INTO_in_insert_stmt2372 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_insert_stmt2377 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_insert_stmt2379 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_insert_stmt2385 = new BitSet(new ulong[] { 0x100000000000UL, 0x20000000000000UL, 0x30UL });
			public static readonly BitSet _LPAREN_in_insert_stmt2392 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_insert_stmt2396 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_insert_stmt2399 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_insert_stmt2403 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_insert_stmt2407 = new BitSet(new ulong[] { 0x0UL, 0x20000000000000UL, 0x10UL });
			public static readonly BitSet _VALUES_in_insert_stmt2416 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_insert_stmt2418 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_insert_stmt2422 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_insert_stmt2425 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_insert_stmt2429 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_insert_stmt2433 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _select_stmt_in_insert_stmt2437 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DEFAULT_in_insert_stmt2444 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x10UL });
			public static readonly BitSet _VALUES_in_insert_stmt2446 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _UPDATE_in_update_stmt2456 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _operation_conflict_clause_in_update_stmt2459 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _qualified_table_name_in_update_stmt2463 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x80UL });
			public static readonly BitSet _SET_in_update_stmt2467 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _update_set_in_update_stmt2471 = new BitSet(new ulong[] { 0x200000000002UL, 0x80C00000000000UL });
			public static readonly BitSet _COMMA_in_update_stmt2474 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _update_set_in_update_stmt2478 = new BitSet(new ulong[] { 0x200000000002UL, 0x80C00000000000UL });
			public static readonly BitSet _WHERE_in_update_stmt2483 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_update_stmt2485 = new BitSet(new ulong[] { 0x2UL, 0xC00000000000UL });
			public static readonly BitSet _operation_limited_clause_in_update_stmt2490 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_in_update_set2501 = new BitSet(new ulong[] { 0x10000000000000UL });
			public static readonly BitSet _EQUALS_in_update_set2503 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_update_set2505 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DELETE_in_delete_stmt2513 = new BitSet(new ulong[] { 0x0UL, 0x40000000000000UL });
			public static readonly BitSet _FROM_in_delete_stmt2515 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _qualified_table_name_in_delete_stmt2517 = new BitSet(new ulong[] { 0x2UL, 0x80C00000000000UL });
			public static readonly BitSet _WHERE_in_delete_stmt2520 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_delete_stmt2522 = new BitSet(new ulong[] { 0x2UL, 0xC00000000000UL });
			public static readonly BitSet _operation_limited_clause_in_delete_stmt2527 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _BEGIN_in_begin_stmt2537 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x3C00UL });
			public static readonly BitSet _set_in_begin_stmt2539 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x2000UL });
			public static readonly BitSet _TRANSACTION_in_begin_stmt2553 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_commit_stmt2563 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x2000UL });
			public static readonly BitSet _TRANSACTION_in_commit_stmt2572 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ROLLBACK_in_rollback_stmt2582 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0xA000UL });
			public static readonly BitSet _TRANSACTION_in_rollback_stmt2585 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x8000UL });
			public static readonly BitSet _TO_in_rollback_stmt2590 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _SAVEPOINT_in_rollback_stmt2593 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_rollback_stmt2599 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _SAVEPOINT_in_savepoint_stmt2609 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_savepoint_stmt2613 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _RELEASE_in_release_stmt2621 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _SAVEPOINT_in_release_stmt2624 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_release_stmt2630 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ON_in_table_conflict_clause2642 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x40000UL });
			public static readonly BitSet _CONFLICT_in_table_conflict_clause2645 = new BitSet(new ulong[] { 0x0UL, 0x80F00000000UL });
			public static readonly BitSet _set_in_table_conflict_clause2648 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CREATE_in_create_virtual_table_stmt2675 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x100000UL });
			public static readonly BitSet _VIRTUAL_in_create_virtual_table_stmt2677 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x200000UL });
			public static readonly BitSet _TABLE_in_create_virtual_table_stmt2679 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_virtual_table_stmt2684 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_create_virtual_table_stmt2686 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_virtual_table_stmt2692 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x2UL });
			public static readonly BitSet _USING_in_create_virtual_table_stmt2696 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_virtual_table_stmt2700 = new BitSet(new ulong[] { 0x100000000002UL });
			public static readonly BitSet _LPAREN_in_create_virtual_table_stmt2703 = new BitSet(new ulong[] { 0xF0F8FEE00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFDFFFDFFFFFFUL });
			public static readonly BitSet _column_def_in_create_virtual_table_stmt2705 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_create_virtual_table_stmt2708 = new BitSet(new ulong[] { 0xF0F8FEE00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFDFFFDFFFFFFUL });
			public static readonly BitSet _column_def_in_create_virtual_table_stmt2710 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_create_virtual_table_stmt2714 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CREATE_in_create_table_stmt2760 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x600000UL });
			public static readonly BitSet _TEMPORARY_in_create_table_stmt2762 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x200000UL });
			public static readonly BitSet _TABLE_in_create_table_stmt2765 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_create_table_stmt2768 = new BitSet(new ulong[] { 0x8000000000UL });
			public static readonly BitSet _NOT_in_create_table_stmt2770 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_create_table_stmt2772 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_table_stmt2779 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_create_table_stmt2781 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_table_stmt2787 = new BitSet(new ulong[] { 0x100000000000UL, 0x8000UL });
			public static readonly BitSet _LPAREN_in_create_table_stmt2793 = new BitSet(new ulong[] { 0xF0F8FEE00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFDFFFDFFFFFFUL });
			public static readonly BitSet _column_def_in_create_table_stmt2795 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_create_table_stmt2798 = new BitSet(new ulong[] { 0xF0F8FEE00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFDFFFDFFFFFFUL });
			public static readonly BitSet _column_def_in_create_table_stmt2800 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_create_table_stmt2805 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0xE6000000UL });
			public static readonly BitSet _table_constraint_in_create_table_stmt2807 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_create_table_stmt2811 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _AS_in_create_table_stmt2817 = new BitSet(new ulong[] { 0x0UL, 0x20000000000000UL });
			public static readonly BitSet _select_stmt_in_create_table_stmt2819 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_column_def_in_column_def2875 = new BitSet(new ulong[] { 0x4008000000002UL, 0x1800UL, 0x176000020UL });
			public static readonly BitSet _type_name_in_column_def2877 = new BitSet(new ulong[] { 0x4008000000002UL, 0x800UL, 0x176000020UL });
			public static readonly BitSet _column_constraint_in_column_def2880 = new BitSet(new ulong[] { 0x4008000000002UL, 0x800UL, 0x176000020UL });
			public static readonly BitSet _CONSTRAINT_in_column_constraint2906 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_column_constraint2910 = new BitSet(new ulong[] { 0x4008000000000UL, 0x800UL, 0x176000020UL });
			public static readonly BitSet _column_constraint_pk_in_column_constraint2918 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_identity_in_column_constraint2924 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_not_null_in_column_constraint2930 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_null_in_column_constraint2936 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_unique_in_column_constraint2942 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_check_in_column_constraint2948 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_default_in_column_constraint2954 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _column_constraint_collate_in_column_constraint2960 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _fk_clause_in_column_constraint2966 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _PRIMARY_in_column_constraint_pk3031 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8000000UL });
			public static readonly BitSet _KEY_in_column_constraint_pk3034 = new BitSet(new ulong[] { 0x2UL, 0x300000000000UL, 0x1UL });
			public static readonly BitSet _set_in_column_constraint_pk3037 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_column_constraint_pk3046 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _AUTOINCREMENT_in_column_constraint_identity3054 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_column_constraint_identity3056 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_column_constraint_not_null3064 = new BitSet(new ulong[] { 0x4000000000000UL });
			public static readonly BitSet _NULL_in_column_constraint_not_null3066 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_column_constraint_not_null3068 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NULL_in_column_constraint_null3085 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_column_constraint_null3087 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _UNIQUE_in_column_constraint_unique3104 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_column_constraint_unique3107 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CHECK_in_column_constraint_check3115 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_column_constraint_check3118 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_column_constraint_check3121 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_column_constraint_check3123 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _INTEGER_in_numeric_literal_value3134 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _FLOAT_in_numeric_literal_value3148 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_signed_default_number3166 = new BitSet(new ulong[] { 0x0UL, 0x600000UL });
			public static readonly BitSet _numeric_literal_value_in_signed_default_number3175 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DEFAULT_in_column_constraint_default3183 = new BitSet(new ulong[] { 0x4100000000000UL, 0xFE00030UL });
			public static readonly BitSet _signed_default_number_in_column_constraint_default3187 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _literal_value_in_column_constraint_default3191 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _LPAREN_in_column_constraint_default3195 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_column_constraint_default3198 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_column_constraint_default3200 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _COLLATE_in_column_constraint_collate3209 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_column_constraint_collate3214 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CONSTRAINT_in_table_constraint3223 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint3227 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0xE6000000UL });
			public static readonly BitSet _table_constraint_pk_in_table_constraint3235 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _table_constraint_unique_in_table_constraint3241 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _table_constraint_check_in_table_constraint3247 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _table_constraint_fk_in_table_constraint3253 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _PRIMARY_in_table_constraint_pk3293 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8000000UL });
			public static readonly BitSet _KEY_in_table_constraint_pk3295 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_table_constraint_pk3299 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint_pk3303 = new BitSet(new ulong[] { 0x600000000000UL, 0x100000000000UL });
			public static readonly BitSet _ASC_in_table_constraint_pk3306 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_table_constraint_pk3311 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint_pk3315 = new BitSet(new ulong[] { 0x600000000000UL, 0x100000000000UL });
			public static readonly BitSet _ASC_in_table_constraint_pk3318 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_table_constraint_pk3324 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_table_constraint_pk3326 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _UNIQUE_in_table_constraint_unique3353 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_table_constraint_unique3357 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint_unique3361 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_table_constraint_unique3364 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint_unique3368 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_table_constraint_unique3372 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1UL });
			public static readonly BitSet _table_conflict_clause_in_table_constraint_unique3374 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CHECK_in_table_constraint_check3399 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_table_constraint_check3402 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_table_constraint_check3405 = new BitSet(new ulong[] { 0x400000000000UL });
			public static readonly BitSet _RPAREN_in_table_constraint_check3407 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _FOREIGN_in_table_constraint_fk3415 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8000000UL });
			public static readonly BitSet _KEY_in_table_constraint_fk3417 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_table_constraint_fk3419 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint_fk3423 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_table_constraint_fk3426 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_table_constraint_fk3430 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_table_constraint_fk3434 = new BitSet(new ulong[] { 0x4008000000000UL, 0x800UL, 0x176000020UL });
			public static readonly BitSet _fk_clause_in_table_constraint_fk3436 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _REFERENCES_in_fk_clause3459 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_fk_clause3463 = new BitSet(new ulong[] { 0x800108000000002UL, 0x0UL, 0x800000001UL });
			public static readonly BitSet _LPAREN_in_fk_clause3466 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_fk_clause3470 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_fk_clause3473 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_fk_clause3477 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_fk_clause3481 = new BitSet(new ulong[] { 0x800008000000002UL, 0x0UL, 0x800000001UL });
			public static readonly BitSet _fk_clause_action_in_fk_clause3487 = new BitSet(new ulong[] { 0x800008000000002UL, 0x0UL, 0x800000001UL });
			public static readonly BitSet _fk_clause_deferrable_in_fk_clause3490 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ON_in_fk_clause_action3524 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x144UL });
			public static readonly BitSet _set_in_fk_clause_action3527 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x600000080UL });
			public static readonly BitSet _SET_in_fk_clause_action3540 = new BitSet(new ulong[] { 0x4000000000000UL });
			public static readonly BitSet _NULL_in_fk_clause_action3543 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _SET_in_fk_clause_action3547 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x20UL });
			public static readonly BitSet _DEFAULT_in_fk_clause_action3550 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CASCADE_in_fk_clause_action3554 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _RESTRICT_in_fk_clause_action3558 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _MATCH_in_fk_clause_action3565 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_fk_clause_action3568 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _NOT_in_fk_clause_deferrable3576 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x800000000UL });
			public static readonly BitSet _DEFERRABLE_in_fk_clause_deferrable3580 = new BitSet(new ulong[] { 0x2UL, 0x0UL, 0x1000000000UL });
			public static readonly BitSet _INITIALLY_in_fk_clause_deferrable3584 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x400UL });
			public static readonly BitSet _DEFERRED_in_fk_clause_deferrable3587 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _INITIALLY_in_fk_clause_deferrable3591 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x800UL });
			public static readonly BitSet _IMMEDIATE_in_fk_clause_deferrable3594 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DROP_in_drop_table_stmt3604 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x200000UL });
			public static readonly BitSet _TABLE_in_drop_table_stmt3606 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_drop_table_stmt3609 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_drop_table_stmt3611 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_table_stmt3618 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_drop_table_stmt3620 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_table_stmt3626 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ALTER_in_alter_table_stmt3656 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x200000UL });
			public static readonly BitSet _TABLE_in_alter_table_stmt3658 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_alter_table_stmt3663 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_alter_table_stmt3665 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_alter_table_stmt3671 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x18000000000UL });
			public static readonly BitSet _RENAME_in_alter_table_stmt3674 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8000UL });
			public static readonly BitSet _TO_in_alter_table_stmt3676 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_alter_table_stmt3680 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ADD_in_alter_table_stmt3684 = new BitSet(new ulong[] { 0xF0F8FEE00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFDFFFFFFUL });
			public static readonly BitSet _COLUMN_in_alter_table_stmt3687 = new BitSet(new ulong[] { 0xF0F8FEE00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFDFFFDFFFFFFUL });
			public static readonly BitSet _column_def_in_alter_table_stmt3691 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CREATE_in_create_view_stmt3700 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x40000400000UL });
			public static readonly BitSet _TEMPORARY_in_create_view_stmt3702 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x40000000000UL });
			public static readonly BitSet _VIEW_in_create_view_stmt3705 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_create_view_stmt3708 = new BitSet(new ulong[] { 0x8000000000UL });
			public static readonly BitSet _NOT_in_create_view_stmt3710 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_create_view_stmt3712 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_view_stmt3719 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_create_view_stmt3721 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_view_stmt3727 = new BitSet(new ulong[] { 0x0UL, 0x8000UL });
			public static readonly BitSet _AS_in_create_view_stmt3729 = new BitSet(new ulong[] { 0x0UL, 0x20000000000000UL });
			public static readonly BitSet _select_stmt_in_create_view_stmt3731 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DROP_in_drop_view_stmt3739 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x40000000000UL });
			public static readonly BitSet _VIEW_in_drop_view_stmt3741 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_drop_view_stmt3744 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_drop_view_stmt3746 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_view_stmt3753 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_drop_view_stmt3755 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_view_stmt3761 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CREATE_in_create_index_stmt3769 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x80020000000UL });
			public static readonly BitSet _UNIQUE_in_create_index_stmt3772 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x80000000000UL });
			public static readonly BitSet _INDEX_in_create_index_stmt3776 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_create_index_stmt3779 = new BitSet(new ulong[] { 0x8000000000UL });
			public static readonly BitSet _NOT_in_create_index_stmt3781 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_create_index_stmt3783 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_index_stmt3790 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_create_index_stmt3792 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_index_stmt3798 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1UL });
			public static readonly BitSet _ON_in_create_index_stmt3802 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_index_stmt3806 = new BitSet(new ulong[] { 0x100000000000UL });
			public static readonly BitSet _LPAREN_in_create_index_stmt3808 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _indexed_column_in_create_index_stmt3812 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _COMMA_in_create_index_stmt3815 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _indexed_column_in_create_index_stmt3819 = new BitSet(new ulong[] { 0x600000000000UL });
			public static readonly BitSet _RPAREN_in_create_index_stmt3823 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_in_indexed_column3869 = new BitSet(new ulong[] { 0x2UL, 0x300000000800UL });
			public static readonly BitSet _COLLATE_in_indexed_column3872 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_indexed_column3876 = new BitSet(new ulong[] { 0x2UL, 0x300000000000UL });
			public static readonly BitSet _ASC_in_indexed_column3881 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DESC_in_indexed_column3885 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DROP_in_drop_index_stmt3916 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x80000000000UL });
			public static readonly BitSet _INDEX_in_drop_index_stmt3918 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_drop_index_stmt3921 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_drop_index_stmt3923 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_index_stmt3930 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_drop_index_stmt3932 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_index_stmt3938 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _CREATE_in_create_trigger_stmt3968 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x100000400000UL });
			public static readonly BitSet _TEMPORARY_in_create_trigger_stmt3970 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x100000000000UL });
			public static readonly BitSet _TRIGGER_in_create_trigger_stmt3973 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_create_trigger_stmt3976 = new BitSet(new ulong[] { 0x8000000000UL });
			public static readonly BitSet _NOT_in_create_trigger_stmt3978 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_create_trigger_stmt3980 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_trigger_stmt3987 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_create_trigger_stmt3989 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_trigger_stmt3995 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0xE00000000144UL });
			public static readonly BitSet _BEFORE_in_create_trigger_stmt4000 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x144UL });
			public static readonly BitSet _AFTER_in_create_trigger_stmt4004 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x144UL });
			public static readonly BitSet _INSTEAD_in_create_trigger_stmt4008 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000000000UL });
			public static readonly BitSet _OF_in_create_trigger_stmt4010 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x144UL });
			public static readonly BitSet _DELETE_in_create_trigger_stmt4015 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1UL });
			public static readonly BitSet _INSERT_in_create_trigger_stmt4019 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1UL });
			public static readonly BitSet _UPDATE_in_create_trigger_stmt4023 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000000001UL });
			public static readonly BitSet _OF_in_create_trigger_stmt4026 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_trigger_stmt4030 = new BitSet(new ulong[] { 0x200000000000UL, 0x0UL, 0x1UL });
			public static readonly BitSet _COMMA_in_create_trigger_stmt4033 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_trigger_stmt4037 = new BitSet(new ulong[] { 0x200000000000UL, 0x0UL, 0x1UL });
			public static readonly BitSet _ON_in_create_trigger_stmt4046 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_create_trigger_stmt4050 = new BitSet(new ulong[] { 0x0UL, 0x80000UL, 0x2000000000200UL });
			public static readonly BitSet _FOR_in_create_trigger_stmt4053 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x4000000000000UL });
			public static readonly BitSet _EACH_in_create_trigger_stmt4055 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x8000000000000UL });
			public static readonly BitSet _ROW_in_create_trigger_stmt4057 = new BitSet(new ulong[] { 0x0UL, 0x80000UL, 0x200UL });
			public static readonly BitSet _WHEN_in_create_trigger_stmt4062 = new BitSet(new ulong[] { 0xE17EE00000000UL, 0xFFFFFFFFFFFFFC30UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _expr_in_create_trigger_stmt4064 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x200UL });
			public static readonly BitSet _BEGIN_in_create_trigger_stmt4070 = new BitSet(new ulong[] { 0x0UL, 0x20080000000000UL, 0x144UL });
			public static readonly BitSet _update_stmt_in_create_trigger_stmt4074 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _insert_stmt_in_create_trigger_stmt4078 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _delete_stmt_in_create_trigger_stmt4082 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _select_stmt_in_create_trigger_stmt4086 = new BitSet(new ulong[] { 0x100000000UL });
			public static readonly BitSet _SEMI_in_create_trigger_stmt4089 = new BitSet(new ulong[] { 0x0UL, 0x20080000040000UL, 0x144UL });
			public static readonly BitSet _END_in_create_trigger_stmt4093 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _DROP_in_drop_trigger_stmt4101 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x100000000000UL });
			public static readonly BitSet _TRIGGER_in_drop_trigger_stmt4103 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _IF_in_drop_trigger_stmt4106 = new BitSet(new ulong[] { 0x0UL, 0x0UL, 0x1000000UL });
			public static readonly BitSet _EXISTS_in_drop_trigger_stmt4108 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_trigger_stmt4115 = new BitSet(new ulong[] { 0x1000000000UL });
			public static readonly BitSet _DOT_in_drop_trigger_stmt4117 = new BitSet(new ulong[] { 0xE076E00000000UL, 0xFFFFFFFF8E9FF800UL, 0xFFFFFFFFFFFFFUL });
			public static readonly BitSet _id_in_drop_trigger_stmt4123 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_id_core4133 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_core_in_id4152 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _keyword_in_id4156 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_keyword4163 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _id_core_in_id_column_def4830 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _keyword_column_def_in_id_column_def4834 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_keyword_column_def4841 = new BitSet(new ulong[] { 0x2UL });

		}
		#endregion Follow sets
	}
}