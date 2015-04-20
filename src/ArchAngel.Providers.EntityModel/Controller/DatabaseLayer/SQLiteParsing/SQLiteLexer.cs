using System;
using Antlr.Runtime;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.SQLiteParsing
{
	[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.3 Nov 30, 2010 12:45:30")]
	[System.CLSCompliant(false)]
	public partial class SQLiteLexer : Antlr.Runtime.Lexer
	{
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


		public void displayRecognitionError(String[] tokenNames, RecognitionException e)
		{
			/*final StringBuilder buffer = new StringBuilder();
			buffer.append("[").append(getErrorHeader(e)).append("] ");
			buffer.append(getErrorMessage(e, tokenNames));
			if(e.input!=null && e.input instanceof CharStream) {
			   final CharStream stream = (CharStream) e.input;
				 int size = stream.size();
				 if(size>0) {
					buffer.append("\n").append(stream.substring(0, size-1));
				 }
			  }
			throw new SqlJetParserException(buffer.toString(), e);*/
			throw new Exception("TODO: (GFH) implement code for displayRecognitionError()");
		}



		// delegates
		// delegators

		public SQLiteLexer()
		{
			OnCreated();
		}

		public SQLiteLexer(ICharStream input)
			: this(input, new RecognizerSharedState())
		{
		}

		public SQLiteLexer(ICharStream input, RecognizerSharedState state)
			: base(input, state)
		{


			OnCreated();
		}
		public override string GrammarFileName { get { return "G:\\Users\\Gareth\\Desktop\\test.g"; } }

		private static readonly bool[] decisionCanBacktrack = new bool[0];


		partial void OnCreated();
		partial void EnterRule(string ruleName, int ruleIndex);
		partial void LeaveRule(string ruleName, int ruleIndex);

		partial void Enter_EQUALS();
		partial void Leave_EQUALS();

		// $ANTLR start "EQUALS"
		[GrammarRule("EQUALS")]
		private void mEQUALS()
		{
			Enter_EQUALS();
			EnterRule("EQUALS", 1);
			TraceIn("EQUALS", 1);
			try
			{
				int _type = EQUALS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:762:7: ( '=' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:762:16: '='
				{
					DebugLocation(762, 16);
					Match('=');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EQUALS", 1);
				LeaveRule("EQUALS", 1);
				Leave_EQUALS();
			}
		}
		// $ANTLR end "EQUALS"

		partial void Enter_EQUALS2();
		partial void Leave_EQUALS2();

		// $ANTLR start "EQUALS2"
		[GrammarRule("EQUALS2")]
		private void mEQUALS2()
		{
			Enter_EQUALS2();
			EnterRule("EQUALS2", 2);
			TraceIn("EQUALS2", 2);
			try
			{
				int _type = EQUALS2;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:763:8: ( '==' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:763:16: '=='
				{
					DebugLocation(763, 16);
					Match("==");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EQUALS2", 2);
				LeaveRule("EQUALS2", 2);
				Leave_EQUALS2();
			}
		}
		// $ANTLR end "EQUALS2"

		partial void Enter_NOT_EQUALS();
		partial void Leave_NOT_EQUALS();

		// $ANTLR start "NOT_EQUALS"
		[GrammarRule("NOT_EQUALS")]
		private void mNOT_EQUALS()
		{
			Enter_NOT_EQUALS();
			EnterRule("NOT_EQUALS", 3);
			TraceIn("NOT_EQUALS", 3);
			try
			{
				int _type = NOT_EQUALS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:764:11: ( '!=' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:764:16: '!='
				{
					DebugLocation(764, 16);
					Match("!=");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("NOT_EQUALS", 3);
				LeaveRule("NOT_EQUALS", 3);
				Leave_NOT_EQUALS();
			}
		}
		// $ANTLR end "NOT_EQUALS"

		partial void Enter_NOT_EQUALS2();
		partial void Leave_NOT_EQUALS2();

		// $ANTLR start "NOT_EQUALS2"
		[GrammarRule("NOT_EQUALS2")]
		private void mNOT_EQUALS2()
		{
			Enter_NOT_EQUALS2();
			EnterRule("NOT_EQUALS2", 4);
			TraceIn("NOT_EQUALS2", 4);
			try
			{
				int _type = NOT_EQUALS2;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:765:12: ( '<>' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:765:16: '<>'
				{
					DebugLocation(765, 16);
					Match("<>");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("NOT_EQUALS2", 4);
				LeaveRule("NOT_EQUALS2", 4);
				Leave_NOT_EQUALS2();
			}
		}
		// $ANTLR end "NOT_EQUALS2"

		partial void Enter_LESS();
		partial void Leave_LESS();

		// $ANTLR start "LESS"
		[GrammarRule("LESS")]
		private void mLESS()
		{
			Enter_LESS();
			EnterRule("LESS", 5);
			TraceIn("LESS", 5);
			try
			{
				int _type = LESS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:766:5: ( '<' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:766:16: '<'
				{
					DebugLocation(766, 16);
					Match('<');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LESS", 5);
				LeaveRule("LESS", 5);
				Leave_LESS();
			}
		}
		// $ANTLR end "LESS"

		partial void Enter_LESS_OR_EQ();
		partial void Leave_LESS_OR_EQ();

		// $ANTLR start "LESS_OR_EQ"
		[GrammarRule("LESS_OR_EQ")]
		private void mLESS_OR_EQ()
		{
			Enter_LESS_OR_EQ();
			EnterRule("LESS_OR_EQ", 6);
			TraceIn("LESS_OR_EQ", 6);
			try
			{
				int _type = LESS_OR_EQ;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:767:11: ( '<=' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:767:16: '<='
				{
					DebugLocation(767, 16);
					Match("<=");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LESS_OR_EQ", 6);
				LeaveRule("LESS_OR_EQ", 6);
				Leave_LESS_OR_EQ();
			}
		}
		// $ANTLR end "LESS_OR_EQ"

		partial void Enter_GREATER();
		partial void Leave_GREATER();

		// $ANTLR start "GREATER"
		[GrammarRule("GREATER")]
		private void mGREATER()
		{
			Enter_GREATER();
			EnterRule("GREATER", 7);
			TraceIn("GREATER", 7);
			try
			{
				int _type = GREATER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:768:8: ( '>' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:768:16: '>'
				{
					DebugLocation(768, 16);
					Match('>');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("GREATER", 7);
				LeaveRule("GREATER", 7);
				Leave_GREATER();
			}
		}
		// $ANTLR end "GREATER"

		partial void Enter_GREATER_OR_EQ();
		partial void Leave_GREATER_OR_EQ();

		// $ANTLR start "GREATER_OR_EQ"
		[GrammarRule("GREATER_OR_EQ")]
		private void mGREATER_OR_EQ()
		{
			Enter_GREATER_OR_EQ();
			EnterRule("GREATER_OR_EQ", 8);
			TraceIn("GREATER_OR_EQ", 8);
			try
			{
				int _type = GREATER_OR_EQ;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:769:14: ( '>=' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:769:16: '>='
				{
					DebugLocation(769, 16);
					Match(">=");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("GREATER_OR_EQ", 8);
				LeaveRule("GREATER_OR_EQ", 8);
				Leave_GREATER_OR_EQ();
			}
		}
		// $ANTLR end "GREATER_OR_EQ"

		partial void Enter_SHIFT_LEFT();
		partial void Leave_SHIFT_LEFT();

		// $ANTLR start "SHIFT_LEFT"
		[GrammarRule("SHIFT_LEFT")]
		private void mSHIFT_LEFT()
		{
			Enter_SHIFT_LEFT();
			EnterRule("SHIFT_LEFT", 9);
			TraceIn("SHIFT_LEFT", 9);
			try
			{
				int _type = SHIFT_LEFT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:770:11: ( '<<' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:770:16: '<<'
				{
					DebugLocation(770, 16);
					Match("<<");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SHIFT_LEFT", 9);
				LeaveRule("SHIFT_LEFT", 9);
				Leave_SHIFT_LEFT();
			}
		}
		// $ANTLR end "SHIFT_LEFT"

		partial void Enter_SHIFT_RIGHT();
		partial void Leave_SHIFT_RIGHT();

		// $ANTLR start "SHIFT_RIGHT"
		[GrammarRule("SHIFT_RIGHT")]
		private void mSHIFT_RIGHT()
		{
			Enter_SHIFT_RIGHT();
			EnterRule("SHIFT_RIGHT", 10);
			TraceIn("SHIFT_RIGHT", 10);
			try
			{
				int _type = SHIFT_RIGHT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:771:12: ( '>>' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:771:16: '>>'
				{
					DebugLocation(771, 16);
					Match(">>");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SHIFT_RIGHT", 10);
				LeaveRule("SHIFT_RIGHT", 10);
				Leave_SHIFT_RIGHT();
			}
		}
		// $ANTLR end "SHIFT_RIGHT"

		partial void Enter_AMPERSAND();
		partial void Leave_AMPERSAND();

		// $ANTLR start "AMPERSAND"
		[GrammarRule("AMPERSAND")]
		private void mAMPERSAND()
		{
			Enter_AMPERSAND();
			EnterRule("AMPERSAND", 11);
			TraceIn("AMPERSAND", 11);
			try
			{
				int _type = AMPERSAND;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:772:10: ( '&' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:772:16: '&'
				{
					DebugLocation(772, 16);
					Match('&');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("AMPERSAND", 11);
				LeaveRule("AMPERSAND", 11);
				Leave_AMPERSAND();
			}
		}
		// $ANTLR end "AMPERSAND"

		partial void Enter_PIPE();
		partial void Leave_PIPE();

		// $ANTLR start "PIPE"
		[GrammarRule("PIPE")]
		private void mPIPE()
		{
			Enter_PIPE();
			EnterRule("PIPE", 12);
			TraceIn("PIPE", 12);
			try
			{
				int _type = PIPE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:773:5: ( '|' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:773:16: '|'
				{
					DebugLocation(773, 16);
					Match('|');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("PIPE", 12);
				LeaveRule("PIPE", 12);
				Leave_PIPE();
			}
		}
		// $ANTLR end "PIPE"

		partial void Enter_DOUBLE_PIPE();
		partial void Leave_DOUBLE_PIPE();

		// $ANTLR start "DOUBLE_PIPE"
		[GrammarRule("DOUBLE_PIPE")]
		private void mDOUBLE_PIPE()
		{
			Enter_DOUBLE_PIPE();
			EnterRule("DOUBLE_PIPE", 13);
			TraceIn("DOUBLE_PIPE", 13);
			try
			{
				int _type = DOUBLE_PIPE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:774:12: ( '||' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:774:16: '||'
				{
					DebugLocation(774, 16);
					Match("||");


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DOUBLE_PIPE", 13);
				LeaveRule("DOUBLE_PIPE", 13);
				Leave_DOUBLE_PIPE();
			}
		}
		// $ANTLR end "DOUBLE_PIPE"

		partial void Enter_PLUS();
		partial void Leave_PLUS();

		// $ANTLR start "PLUS"
		[GrammarRule("PLUS")]
		private void mPLUS()
		{
			Enter_PLUS();
			EnterRule("PLUS", 14);
			TraceIn("PLUS", 14);
			try
			{
				int _type = PLUS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:775:5: ( '+' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:775:16: '+'
				{
					DebugLocation(775, 16);
					Match('+');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("PLUS", 14);
				LeaveRule("PLUS", 14);
				Leave_PLUS();
			}
		}
		// $ANTLR end "PLUS"

		partial void Enter_MINUS();
		partial void Leave_MINUS();

		// $ANTLR start "MINUS"
		[GrammarRule("MINUS")]
		private void mMINUS()
		{
			Enter_MINUS();
			EnterRule("MINUS", 15);
			TraceIn("MINUS", 15);
			try
			{
				int _type = MINUS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:776:6: ( '-' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:776:16: '-'
				{
					DebugLocation(776, 16);
					Match('-');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("MINUS", 15);
				LeaveRule("MINUS", 15);
				Leave_MINUS();
			}
		}
		// $ANTLR end "MINUS"

		partial void Enter_TILDA();
		partial void Leave_TILDA();

		// $ANTLR start "TILDA"
		[GrammarRule("TILDA")]
		private void mTILDA()
		{
			Enter_TILDA();
			EnterRule("TILDA", 16);
			TraceIn("TILDA", 16);
			try
			{
				int _type = TILDA;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:777:6: ( '~' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:777:16: '~'
				{
					DebugLocation(777, 16);
					Match('~');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("TILDA", 16);
				LeaveRule("TILDA", 16);
				Leave_TILDA();
			}
		}
		// $ANTLR end "TILDA"

		partial void Enter_ASTERISK();
		partial void Leave_ASTERISK();

		// $ANTLR start "ASTERISK"
		[GrammarRule("ASTERISK")]
		private void mASTERISK()
		{
			Enter_ASTERISK();
			EnterRule("ASTERISK", 17);
			TraceIn("ASTERISK", 17);
			try
			{
				int _type = ASTERISK;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:778:9: ( '*' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:778:16: '*'
				{
					DebugLocation(778, 16);
					Match('*');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ASTERISK", 17);
				LeaveRule("ASTERISK", 17);
				Leave_ASTERISK();
			}
		}
		// $ANTLR end "ASTERISK"

		partial void Enter_SLASH();
		partial void Leave_SLASH();

		// $ANTLR start "SLASH"
		[GrammarRule("SLASH")]
		private void mSLASH()
		{
			Enter_SLASH();
			EnterRule("SLASH", 18);
			TraceIn("SLASH", 18);
			try
			{
				int _type = SLASH;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:779:6: ( '/' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:779:16: '/'
				{
					DebugLocation(779, 16);
					Match('/');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SLASH", 18);
				LeaveRule("SLASH", 18);
				Leave_SLASH();
			}
		}
		// $ANTLR end "SLASH"

		partial void Enter_BACKSLASH();
		partial void Leave_BACKSLASH();

		// $ANTLR start "BACKSLASH"
		[GrammarRule("BACKSLASH")]
		private void mBACKSLASH()
		{
			Enter_BACKSLASH();
			EnterRule("BACKSLASH", 19);
			TraceIn("BACKSLASH", 19);
			try
			{
				int _type = BACKSLASH;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:780:10: ( '\\\\' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:780:16: '\\\\'
				{
					DebugLocation(780, 16);
					Match('\\');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("BACKSLASH", 19);
				LeaveRule("BACKSLASH", 19);
				Leave_BACKSLASH();
			}
		}
		// $ANTLR end "BACKSLASH"

		partial void Enter_PERCENT();
		partial void Leave_PERCENT();

		// $ANTLR start "PERCENT"
		[GrammarRule("PERCENT")]
		private void mPERCENT()
		{
			Enter_PERCENT();
			EnterRule("PERCENT", 20);
			TraceIn("PERCENT", 20);
			try
			{
				int _type = PERCENT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:781:8: ( '%' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:781:16: '%'
				{
					DebugLocation(781, 16);
					Match('%');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("PERCENT", 20);
				LeaveRule("PERCENT", 20);
				Leave_PERCENT();
			}
		}
		// $ANTLR end "PERCENT"

		partial void Enter_SEMI();
		partial void Leave_SEMI();

		// $ANTLR start "SEMI"
		[GrammarRule("SEMI")]
		private void mSEMI()
		{
			Enter_SEMI();
			EnterRule("SEMI", 21);
			TraceIn("SEMI", 21);
			try
			{
				int _type = SEMI;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:782:5: ( ';' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:782:16: ';'
				{
					DebugLocation(782, 16);
					Match(';');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SEMI", 21);
				LeaveRule("SEMI", 21);
				Leave_SEMI();
			}
		}
		// $ANTLR end "SEMI"

		partial void Enter_DOT();
		partial void Leave_DOT();

		// $ANTLR start "DOT"
		[GrammarRule("DOT")]
		private void mDOT()
		{
			Enter_DOT();
			EnterRule("DOT", 22);
			TraceIn("DOT", 22);
			try
			{
				int _type = DOT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:783:4: ( '.' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:783:16: '.'
				{
					DebugLocation(783, 16);
					Match('.');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DOT", 22);
				LeaveRule("DOT", 22);
				Leave_DOT();
			}
		}
		// $ANTLR end "DOT"

		partial void Enter_COMMA();
		partial void Leave_COMMA();

		// $ANTLR start "COMMA"
		[GrammarRule("COMMA")]
		private void mCOMMA()
		{
			Enter_COMMA();
			EnterRule("COMMA", 23);
			TraceIn("COMMA", 23);
			try
			{
				int _type = COMMA;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:784:6: ( ',' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:784:16: ','
				{
					DebugLocation(784, 16);
					Match(',');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("COMMA", 23);
				LeaveRule("COMMA", 23);
				Leave_COMMA();
			}
		}
		// $ANTLR end "COMMA"

		partial void Enter_LPAREN();
		partial void Leave_LPAREN();

		// $ANTLR start "LPAREN"
		[GrammarRule("LPAREN")]
		private void mLPAREN()
		{
			Enter_LPAREN();
			EnterRule("LPAREN", 24);
			TraceIn("LPAREN", 24);
			try
			{
				int _type = LPAREN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:785:7: ( '(' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:785:16: '('
				{
					DebugLocation(785, 16);
					Match('(');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LPAREN", 24);
				LeaveRule("LPAREN", 24);
				Leave_LPAREN();
			}
		}
		// $ANTLR end "LPAREN"

		partial void Enter_RPAREN();
		partial void Leave_RPAREN();

		// $ANTLR start "RPAREN"
		[GrammarRule("RPAREN")]
		private void mRPAREN()
		{
			Enter_RPAREN();
			EnterRule("RPAREN", 25);
			TraceIn("RPAREN", 25);
			try
			{
				int _type = RPAREN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:786:7: ( ')' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:786:16: ')'
				{
					DebugLocation(786, 16);
					Match(')');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("RPAREN", 25);
				LeaveRule("RPAREN", 25);
				Leave_RPAREN();
			}
		}
		// $ANTLR end "RPAREN"

		partial void Enter_QUESTION();
		partial void Leave_QUESTION();

		// $ANTLR start "QUESTION"
		[GrammarRule("QUESTION")]
		private void mQUESTION()
		{
			Enter_QUESTION();
			EnterRule("QUESTION", 26);
			TraceIn("QUESTION", 26);
			try
			{
				int _type = QUESTION;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:787:9: ( '?' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:787:16: '?'
				{
					DebugLocation(787, 16);
					Match('?');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("QUESTION", 26);
				LeaveRule("QUESTION", 26);
				Leave_QUESTION();
			}
		}
		// $ANTLR end "QUESTION"

		partial void Enter_COLON();
		partial void Leave_COLON();

		// $ANTLR start "COLON"
		[GrammarRule("COLON")]
		private void mCOLON()
		{
			Enter_COLON();
			EnterRule("COLON", 27);
			TraceIn("COLON", 27);
			try
			{
				int _type = COLON;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:788:6: ( ':' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:788:16: ':'
				{
					DebugLocation(788, 16);
					Match(':');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("COLON", 27);
				LeaveRule("COLON", 27);
				Leave_COLON();
			}
		}
		// $ANTLR end "COLON"

		partial void Enter_AT();
		partial void Leave_AT();

		// $ANTLR start "AT"
		[GrammarRule("AT")]
		private void mAT()
		{
			Enter_AT();
			EnterRule("AT", 28);
			TraceIn("AT", 28);
			try
			{
				int _type = AT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:789:3: ( '@' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:789:16: '@'
				{
					DebugLocation(789, 16);
					Match('@');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("AT", 28);
				LeaveRule("AT", 28);
				Leave_AT();
			}
		}
		// $ANTLR end "AT"

		partial void Enter_DOLLAR();
		partial void Leave_DOLLAR();

		// $ANTLR start "DOLLAR"
		[GrammarRule("DOLLAR")]
		private void mDOLLAR()
		{
			Enter_DOLLAR();
			EnterRule("DOLLAR", 29);
			TraceIn("DOLLAR", 29);
			try
			{
				int _type = DOLLAR;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:790:7: ( '$' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:790:16: '$'
				{
					DebugLocation(790, 16);
					Match('$');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DOLLAR", 29);
				LeaveRule("DOLLAR", 29);
				Leave_DOLLAR();
			}
		}
		// $ANTLR end "DOLLAR"

		partial void Enter_QUOTE_DOUBLE();
		partial void Leave_QUOTE_DOUBLE();

		// $ANTLR start "QUOTE_DOUBLE"
		[GrammarRule("QUOTE_DOUBLE")]
		private void mQUOTE_DOUBLE()
		{
			Enter_QUOTE_DOUBLE();
			EnterRule("QUOTE_DOUBLE", 30);
			TraceIn("QUOTE_DOUBLE", 30);
			try
			{
				int _type = QUOTE_DOUBLE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:791:13: ( '\"' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:791:16: '\"'
				{
					DebugLocation(791, 16);
					Match('\"');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("QUOTE_DOUBLE", 30);
				LeaveRule("QUOTE_DOUBLE", 30);
				Leave_QUOTE_DOUBLE();
			}
		}
		// $ANTLR end "QUOTE_DOUBLE"

		partial void Enter_QUOTE_SINGLE();
		partial void Leave_QUOTE_SINGLE();

		// $ANTLR start "QUOTE_SINGLE"
		[GrammarRule("QUOTE_SINGLE")]
		private void mQUOTE_SINGLE()
		{
			Enter_QUOTE_SINGLE();
			EnterRule("QUOTE_SINGLE", 31);
			TraceIn("QUOTE_SINGLE", 31);
			try
			{
				int _type = QUOTE_SINGLE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:792:13: ( '\\'' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:792:16: '\\''
				{
					DebugLocation(792, 16);
					Match('\'');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("QUOTE_SINGLE", 31);
				LeaveRule("QUOTE_SINGLE", 31);
				Leave_QUOTE_SINGLE();
			}
		}
		// $ANTLR end "QUOTE_SINGLE"

		partial void Enter_APOSTROPHE();
		partial void Leave_APOSTROPHE();

		// $ANTLR start "APOSTROPHE"
		[GrammarRule("APOSTROPHE")]
		private void mAPOSTROPHE()
		{
			Enter_APOSTROPHE();
			EnterRule("APOSTROPHE", 32);
			TraceIn("APOSTROPHE", 32);
			try
			{
				int _type = APOSTROPHE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:793:11: ( '`' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:793:16: '`'
				{
					DebugLocation(793, 16);
					Match('`');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("APOSTROPHE", 32);
				LeaveRule("APOSTROPHE", 32);
				Leave_APOSTROPHE();
			}
		}
		// $ANTLR end "APOSTROPHE"

		partial void Enter_LPAREN_SQUARE();
		partial void Leave_LPAREN_SQUARE();

		// $ANTLR start "LPAREN_SQUARE"
		[GrammarRule("LPAREN_SQUARE")]
		private void mLPAREN_SQUARE()
		{
			Enter_LPAREN_SQUARE();
			EnterRule("LPAREN_SQUARE", 33);
			TraceIn("LPAREN_SQUARE", 33);
			try
			{
				int _type = LPAREN_SQUARE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:794:14: ( '[' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:794:16: '['
				{
					DebugLocation(794, 16);
					Match('[');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LPAREN_SQUARE", 33);
				LeaveRule("LPAREN_SQUARE", 33);
				Leave_LPAREN_SQUARE();
			}
		}
		// $ANTLR end "LPAREN_SQUARE"

		partial void Enter_RPAREN_SQUARE();
		partial void Leave_RPAREN_SQUARE();

		// $ANTLR start "RPAREN_SQUARE"
		[GrammarRule("RPAREN_SQUARE")]
		private void mRPAREN_SQUARE()
		{
			Enter_RPAREN_SQUARE();
			EnterRule("RPAREN_SQUARE", 34);
			TraceIn("RPAREN_SQUARE", 34);
			try
			{
				int _type = RPAREN_SQUARE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:795:14: ( ']' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:795:16: ']'
				{
					DebugLocation(795, 16);
					Match(']');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("RPAREN_SQUARE", 34);
				LeaveRule("RPAREN_SQUARE", 34);
				Leave_RPAREN_SQUARE();
			}
		}
		// $ANTLR end "RPAREN_SQUARE"

		partial void Enter_UNDERSCORE();
		partial void Leave_UNDERSCORE();

		// $ANTLR start "UNDERSCORE"
		[GrammarRule("UNDERSCORE")]
		private void mUNDERSCORE()
		{
			Enter_UNDERSCORE();
			EnterRule("UNDERSCORE", 35);
			TraceIn("UNDERSCORE", 35);
			try
			{
				int _type = UNDERSCORE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:796:11: ( '_' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:796:16: '_'
				{
					DebugLocation(796, 16);
					Match('_');

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("UNDERSCORE", 35);
				LeaveRule("UNDERSCORE", 35);
				Leave_UNDERSCORE();
			}
		}
		// $ANTLR end "UNDERSCORE"

		partial void Enter_A();
		partial void Leave_A();

		// $ANTLR start "A"
		[GrammarRule("A")]
		private void mA()
		{
			Enter_A();
			EnterRule("A", 36);
			TraceIn("A", 36);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:799:11: ( ( 'a' | 'A' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:799:12: ( 'a' | 'A' )
				{
					DebugLocation(799, 12);
					if (input.LA(1) == 'A' || input.LA(1) == 'a')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("A", 36);
				LeaveRule("A", 36);
				Leave_A();
			}
		}
		// $ANTLR end "A"

		partial void Enter_B();
		partial void Leave_B();

		// $ANTLR start "B"
		[GrammarRule("B")]
		private void mB()
		{
			Enter_B();
			EnterRule("B", 37);
			TraceIn("B", 37);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:800:11: ( ( 'b' | 'B' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:800:12: ( 'b' | 'B' )
				{
					DebugLocation(800, 12);
					if (input.LA(1) == 'B' || input.LA(1) == 'b')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("B", 37);
				LeaveRule("B", 37);
				Leave_B();
			}
		}
		// $ANTLR end "B"

		partial void Enter_C();
		partial void Leave_C();

		// $ANTLR start "C"
		[GrammarRule("C")]
		private void mC()
		{
			Enter_C();
			EnterRule("C", 38);
			TraceIn("C", 38);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:801:11: ( ( 'c' | 'C' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:801:12: ( 'c' | 'C' )
				{
					DebugLocation(801, 12);
					if (input.LA(1) == 'C' || input.LA(1) == 'c')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("C", 38);
				LeaveRule("C", 38);
				Leave_C();
			}
		}
		// $ANTLR end "C"

		partial void Enter_D();
		partial void Leave_D();

		// $ANTLR start "D"
		[GrammarRule("D")]
		private void mD()
		{
			Enter_D();
			EnterRule("D", 39);
			TraceIn("D", 39);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:802:11: ( ( 'd' | 'D' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:802:12: ( 'd' | 'D' )
				{
					DebugLocation(802, 12);
					if (input.LA(1) == 'D' || input.LA(1) == 'd')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("D", 39);
				LeaveRule("D", 39);
				Leave_D();
			}
		}
		// $ANTLR end "D"

		partial void Enter_E();
		partial void Leave_E();

		// $ANTLR start "E"
		[GrammarRule("E")]
		private void mE()
		{
			Enter_E();
			EnterRule("E", 40);
			TraceIn("E", 40);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:803:11: ( ( 'e' | 'E' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:803:12: ( 'e' | 'E' )
				{
					DebugLocation(803, 12);
					if (input.LA(1) == 'E' || input.LA(1) == 'e')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("E", 40);
				LeaveRule("E", 40);
				Leave_E();
			}
		}
		// $ANTLR end "E"

		partial void Enter_F();
		partial void Leave_F();

		// $ANTLR start "F"
		[GrammarRule("F")]
		private void mF()
		{
			Enter_F();
			EnterRule("F", 41);
			TraceIn("F", 41);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:804:11: ( ( 'f' | 'F' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:804:12: ( 'f' | 'F' )
				{
					DebugLocation(804, 12);
					if (input.LA(1) == 'F' || input.LA(1) == 'f')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("F", 41);
				LeaveRule("F", 41);
				Leave_F();
			}
		}
		// $ANTLR end "F"

		partial void Enter_G();
		partial void Leave_G();

		// $ANTLR start "G"
		[GrammarRule("G")]
		private void mG()
		{
			Enter_G();
			EnterRule("G", 42);
			TraceIn("G", 42);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:805:11: ( ( 'g' | 'G' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:805:12: ( 'g' | 'G' )
				{
					DebugLocation(805, 12);
					if (input.LA(1) == 'G' || input.LA(1) == 'g')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("G", 42);
				LeaveRule("G", 42);
				Leave_G();
			}
		}
		// $ANTLR end "G"

		partial void Enter_H();
		partial void Leave_H();

		// $ANTLR start "H"
		[GrammarRule("H")]
		private void mH()
		{
			Enter_H();
			EnterRule("H", 43);
			TraceIn("H", 43);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:806:11: ( ( 'h' | 'H' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:806:12: ( 'h' | 'H' )
				{
					DebugLocation(806, 12);
					if (input.LA(1) == 'H' || input.LA(1) == 'h')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("H", 43);
				LeaveRule("H", 43);
				Leave_H();
			}
		}
		// $ANTLR end "H"

		partial void Enter_I();
		partial void Leave_I();

		// $ANTLR start "I"
		[GrammarRule("I")]
		private void mI()
		{
			Enter_I();
			EnterRule("I", 44);
			TraceIn("I", 44);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:807:11: ( ( 'i' | 'I' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:807:12: ( 'i' | 'I' )
				{
					DebugLocation(807, 12);
					if (input.LA(1) == 'I' || input.LA(1) == 'i')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("I", 44);
				LeaveRule("I", 44);
				Leave_I();
			}
		}
		// $ANTLR end "I"

		partial void Enter_J();
		partial void Leave_J();

		// $ANTLR start "J"
		[GrammarRule("J")]
		private void mJ()
		{
			Enter_J();
			EnterRule("J", 45);
			TraceIn("J", 45);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:808:11: ( ( 'j' | 'J' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:808:12: ( 'j' | 'J' )
				{
					DebugLocation(808, 12);
					if (input.LA(1) == 'J' || input.LA(1) == 'j')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("J", 45);
				LeaveRule("J", 45);
				Leave_J();
			}
		}
		// $ANTLR end "J"

		partial void Enter_K();
		partial void Leave_K();

		// $ANTLR start "K"
		[GrammarRule("K")]
		private void mK()
		{
			Enter_K();
			EnterRule("K", 46);
			TraceIn("K", 46);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:809:11: ( ( 'k' | 'K' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:809:12: ( 'k' | 'K' )
				{
					DebugLocation(809, 12);
					if (input.LA(1) == 'K' || input.LA(1) == 'k')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("K", 46);
				LeaveRule("K", 46);
				Leave_K();
			}
		}
		// $ANTLR end "K"

		partial void Enter_L();
		partial void Leave_L();

		// $ANTLR start "L"
		[GrammarRule("L")]
		private void mL()
		{
			Enter_L();
			EnterRule("L", 47);
			TraceIn("L", 47);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:810:11: ( ( 'l' | 'L' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:810:12: ( 'l' | 'L' )
				{
					DebugLocation(810, 12);
					if (input.LA(1) == 'L' || input.LA(1) == 'l')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("L", 47);
				LeaveRule("L", 47);
				Leave_L();
			}
		}
		// $ANTLR end "L"

		partial void Enter_M();
		partial void Leave_M();

		// $ANTLR start "M"
		[GrammarRule("M")]
		private void mM()
		{
			Enter_M();
			EnterRule("M", 48);
			TraceIn("M", 48);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:811:11: ( ( 'm' | 'M' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:811:12: ( 'm' | 'M' )
				{
					DebugLocation(811, 12);
					if (input.LA(1) == 'M' || input.LA(1) == 'm')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("M", 48);
				LeaveRule("M", 48);
				Leave_M();
			}
		}
		// $ANTLR end "M"

		partial void Enter_N();
		partial void Leave_N();

		// $ANTLR start "N"
		[GrammarRule("N")]
		private void mN()
		{
			Enter_N();
			EnterRule("N", 49);
			TraceIn("N", 49);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:812:11: ( ( 'n' | 'N' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:812:12: ( 'n' | 'N' )
				{
					DebugLocation(812, 12);
					if (input.LA(1) == 'N' || input.LA(1) == 'n')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("N", 49);
				LeaveRule("N", 49);
				Leave_N();
			}
		}
		// $ANTLR end "N"

		partial void Enter_O();
		partial void Leave_O();

		// $ANTLR start "O"
		[GrammarRule("O")]
		private void mO()
		{
			Enter_O();
			EnterRule("O", 50);
			TraceIn("O", 50);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:813:11: ( ( 'o' | 'O' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:813:12: ( 'o' | 'O' )
				{
					DebugLocation(813, 12);
					if (input.LA(1) == 'O' || input.LA(1) == 'o')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("O", 50);
				LeaveRule("O", 50);
				Leave_O();
			}
		}
		// $ANTLR end "O"

		partial void Enter_P();
		partial void Leave_P();

		// $ANTLR start "P"
		[GrammarRule("P")]
		private void mP()
		{
			Enter_P();
			EnterRule("P", 51);
			TraceIn("P", 51);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:814:11: ( ( 'p' | 'P' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:814:12: ( 'p' | 'P' )
				{
					DebugLocation(814, 12);
					if (input.LA(1) == 'P' || input.LA(1) == 'p')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("P", 51);
				LeaveRule("P", 51);
				Leave_P();
			}
		}
		// $ANTLR end "P"

		partial void Enter_Q();
		partial void Leave_Q();

		// $ANTLR start "Q"
		[GrammarRule("Q")]
		private void mQ()
		{
			Enter_Q();
			EnterRule("Q", 52);
			TraceIn("Q", 52);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:815:11: ( ( 'q' | 'Q' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:815:12: ( 'q' | 'Q' )
				{
					DebugLocation(815, 12);
					if (input.LA(1) == 'Q' || input.LA(1) == 'q')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("Q", 52);
				LeaveRule("Q", 52);
				Leave_Q();
			}
		}
		// $ANTLR end "Q"

		partial void Enter_R();
		partial void Leave_R();

		// $ANTLR start "R"
		[GrammarRule("R")]
		private void mR()
		{
			Enter_R();
			EnterRule("R", 53);
			TraceIn("R", 53);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:816:11: ( ( 'r' | 'R' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:816:12: ( 'r' | 'R' )
				{
					DebugLocation(816, 12);
					if (input.LA(1) == 'R' || input.LA(1) == 'r')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("R", 53);
				LeaveRule("R", 53);
				Leave_R();
			}
		}
		// $ANTLR end "R"

		partial void Enter_S();
		partial void Leave_S();

		// $ANTLR start "S"
		[GrammarRule("S")]
		private void mS()
		{
			Enter_S();
			EnterRule("S", 54);
			TraceIn("S", 54);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:817:11: ( ( 's' | 'S' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:817:12: ( 's' | 'S' )
				{
					DebugLocation(817, 12);
					if (input.LA(1) == 'S' || input.LA(1) == 's')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("S", 54);
				LeaveRule("S", 54);
				Leave_S();
			}
		}
		// $ANTLR end "S"

		partial void Enter_T();
		partial void Leave_T();

		// $ANTLR start "T"
		[GrammarRule("T")]
		private void mT()
		{
			Enter_T();
			EnterRule("T", 55);
			TraceIn("T", 55);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:818:11: ( ( 't' | 'T' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:818:12: ( 't' | 'T' )
				{
					DebugLocation(818, 12);
					if (input.LA(1) == 'T' || input.LA(1) == 't')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("T", 55);
				LeaveRule("T", 55);
				Leave_T();
			}
		}
		// $ANTLR end "T"

		partial void Enter_U();
		partial void Leave_U();

		// $ANTLR start "U"
		[GrammarRule("U")]
		private void mU()
		{
			Enter_U();
			EnterRule("U", 56);
			TraceIn("U", 56);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:819:11: ( ( 'u' | 'U' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:819:12: ( 'u' | 'U' )
				{
					DebugLocation(819, 12);
					if (input.LA(1) == 'U' || input.LA(1) == 'u')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("U", 56);
				LeaveRule("U", 56);
				Leave_U();
			}
		}
		// $ANTLR end "U"

		partial void Enter_V();
		partial void Leave_V();

		// $ANTLR start "V"
		[GrammarRule("V")]
		private void mV()
		{
			Enter_V();
			EnterRule("V", 57);
			TraceIn("V", 57);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:820:11: ( ( 'v' | 'V' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:820:12: ( 'v' | 'V' )
				{
					DebugLocation(820, 12);
					if (input.LA(1) == 'V' || input.LA(1) == 'v')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("V", 57);
				LeaveRule("V", 57);
				Leave_V();
			}
		}
		// $ANTLR end "V"

		partial void Enter_W();
		partial void Leave_W();

		// $ANTLR start "W"
		[GrammarRule("W")]
		private void mW()
		{
			Enter_W();
			EnterRule("W", 58);
			TraceIn("W", 58);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:821:11: ( ( 'w' | 'W' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:821:12: ( 'w' | 'W' )
				{
					DebugLocation(821, 12);
					if (input.LA(1) == 'W' || input.LA(1) == 'w')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("W", 58);
				LeaveRule("W", 58);
				Leave_W();
			}
		}
		// $ANTLR end "W"

		partial void Enter_X();
		partial void Leave_X();

		// $ANTLR start "X"
		[GrammarRule("X")]
		private void mX()
		{
			Enter_X();
			EnterRule("X", 59);
			TraceIn("X", 59);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:822:11: ( ( 'x' | 'X' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:822:12: ( 'x' | 'X' )
				{
					DebugLocation(822, 12);
					if (input.LA(1) == 'X' || input.LA(1) == 'x')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("X", 59);
				LeaveRule("X", 59);
				Leave_X();
			}
		}
		// $ANTLR end "X"

		partial void Enter_Y();
		partial void Leave_Y();

		// $ANTLR start "Y"
		[GrammarRule("Y")]
		private void mY()
		{
			Enter_Y();
			EnterRule("Y", 60);
			TraceIn("Y", 60);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:823:11: ( ( 'y' | 'Y' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:823:12: ( 'y' | 'Y' )
				{
					DebugLocation(823, 12);
					if (input.LA(1) == 'Y' || input.LA(1) == 'y')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("Y", 60);
				LeaveRule("Y", 60);
				Leave_Y();
			}
		}
		// $ANTLR end "Y"

		partial void Enter_Z();
		partial void Leave_Z();

		// $ANTLR start "Z"
		[GrammarRule("Z")]
		private void mZ()
		{
			Enter_Z();
			EnterRule("Z", 61);
			TraceIn("Z", 61);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:824:11: ( ( 'z' | 'Z' ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:824:12: ( 'z' | 'Z' )
				{
					DebugLocation(824, 12);
					if (input.LA(1) == 'Z' || input.LA(1) == 'z')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("Z", 61);
				LeaveRule("Z", 61);
				Leave_Z();
			}
		}
		// $ANTLR end "Z"

		partial void Enter_ABORT();
		partial void Leave_ABORT();

		// $ANTLR start "ABORT"
		[GrammarRule("ABORT")]
		private void mABORT()
		{
			Enter_ABORT();
			EnterRule("ABORT", 62);
			TraceIn("ABORT", 62);
			try
			{
				int _type = ABORT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:826:6: ( A B O R T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:826:8: A B O R T
				{
					DebugLocation(826, 8);
					mA();
					DebugLocation(826, 10);
					mB();
					DebugLocation(826, 12);
					mO();
					DebugLocation(826, 14);
					mR();
					DebugLocation(826, 16);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ABORT", 62);
				LeaveRule("ABORT", 62);
				Leave_ABORT();
			}
		}
		// $ANTLR end "ABORT"

		partial void Enter_ADD();
		partial void Leave_ADD();

		// $ANTLR start "ADD"
		[GrammarRule("ADD")]
		private void mADD()
		{
			Enter_ADD();
			EnterRule("ADD", 63);
			TraceIn("ADD", 63);
			try
			{
				int _type = ADD;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:827:4: ( A D D )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:827:6: A D D
				{
					DebugLocation(827, 6);
					mA();
					DebugLocation(827, 8);
					mD();
					DebugLocation(827, 10);
					mD();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ADD", 63);
				LeaveRule("ADD", 63);
				Leave_ADD();
			}
		}
		// $ANTLR end "ADD"

		partial void Enter_AFTER();
		partial void Leave_AFTER();

		// $ANTLR start "AFTER"
		[GrammarRule("AFTER")]
		private void mAFTER()
		{
			Enter_AFTER();
			EnterRule("AFTER", 64);
			TraceIn("AFTER", 64);
			try
			{
				int _type = AFTER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:828:6: ( A F T E R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:828:8: A F T E R
				{
					DebugLocation(828, 8);
					mA();
					DebugLocation(828, 10);
					mF();
					DebugLocation(828, 12);
					mT();
					DebugLocation(828, 14);
					mE();
					DebugLocation(828, 16);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("AFTER", 64);
				LeaveRule("AFTER", 64);
				Leave_AFTER();
			}
		}
		// $ANTLR end "AFTER"

		partial void Enter_ALL();
		partial void Leave_ALL();

		// $ANTLR start "ALL"
		[GrammarRule("ALL")]
		private void mALL()
		{
			Enter_ALL();
			EnterRule("ALL", 65);
			TraceIn("ALL", 65);
			try
			{
				int _type = ALL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:829:4: ( A L L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:829:6: A L L
				{
					DebugLocation(829, 6);
					mA();
					DebugLocation(829, 8);
					mL();
					DebugLocation(829, 10);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ALL", 65);
				LeaveRule("ALL", 65);
				Leave_ALL();
			}
		}
		// $ANTLR end "ALL"

		partial void Enter_ALTER();
		partial void Leave_ALTER();

		// $ANTLR start "ALTER"
		[GrammarRule("ALTER")]
		private void mALTER()
		{
			Enter_ALTER();
			EnterRule("ALTER", 66);
			TraceIn("ALTER", 66);
			try
			{
				int _type = ALTER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:830:6: ( A L T E R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:830:8: A L T E R
				{
					DebugLocation(830, 8);
					mA();
					DebugLocation(830, 10);
					mL();
					DebugLocation(830, 12);
					mT();
					DebugLocation(830, 14);
					mE();
					DebugLocation(830, 16);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ALTER", 66);
				LeaveRule("ALTER", 66);
				Leave_ALTER();
			}
		}
		// $ANTLR end "ALTER"

		partial void Enter_ANALYZE();
		partial void Leave_ANALYZE();

		// $ANTLR start "ANALYZE"
		[GrammarRule("ANALYZE")]
		private void mANALYZE()
		{
			Enter_ANALYZE();
			EnterRule("ANALYZE", 67);
			TraceIn("ANALYZE", 67);
			try
			{
				int _type = ANALYZE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:831:8: ( A N A L Y Z E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:831:10: A N A L Y Z E
				{
					DebugLocation(831, 10);
					mA();
					DebugLocation(831, 12);
					mN();
					DebugLocation(831, 14);
					mA();
					DebugLocation(831, 16);
					mL();
					DebugLocation(831, 18);
					mY();
					DebugLocation(831, 20);
					mZ();
					DebugLocation(831, 22);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ANALYZE", 67);
				LeaveRule("ANALYZE", 67);
				Leave_ANALYZE();
			}
		}
		// $ANTLR end "ANALYZE"

		partial void Enter_AND();
		partial void Leave_AND();

		// $ANTLR start "AND"
		[GrammarRule("AND")]
		private void mAND()
		{
			Enter_AND();
			EnterRule("AND", 68);
			TraceIn("AND", 68);
			try
			{
				int _type = AND;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:832:4: ( A N D )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:832:6: A N D
				{
					DebugLocation(832, 6);
					mA();
					DebugLocation(832, 8);
					mN();
					DebugLocation(832, 10);
					mD();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("AND", 68);
				LeaveRule("AND", 68);
				Leave_AND();
			}
		}
		// $ANTLR end "AND"

		partial void Enter_AS();
		partial void Leave_AS();

		// $ANTLR start "AS"
		[GrammarRule("AS")]
		private void mAS()
		{
			Enter_AS();
			EnterRule("AS", 69);
			TraceIn("AS", 69);
			try
			{
				int _type = AS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:833:3: ( A S )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:833:5: A S
				{
					DebugLocation(833, 5);
					mA();
					DebugLocation(833, 7);
					mS();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("AS", 69);
				LeaveRule("AS", 69);
				Leave_AS();
			}
		}
		// $ANTLR end "AS"

		partial void Enter_ASC();
		partial void Leave_ASC();

		// $ANTLR start "ASC"
		[GrammarRule("ASC")]
		private void mASC()
		{
			Enter_ASC();
			EnterRule("ASC", 70);
			TraceIn("ASC", 70);
			try
			{
				int _type = ASC;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:834:4: ( A S C )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:834:6: A S C
				{
					DebugLocation(834, 6);
					mA();
					DebugLocation(834, 8);
					mS();
					DebugLocation(834, 10);
					mC();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ASC", 70);
				LeaveRule("ASC", 70);
				Leave_ASC();
			}
		}
		// $ANTLR end "ASC"

		partial void Enter_ATTACH();
		partial void Leave_ATTACH();

		// $ANTLR start "ATTACH"
		[GrammarRule("ATTACH")]
		private void mATTACH()
		{
			Enter_ATTACH();
			EnterRule("ATTACH", 71);
			TraceIn("ATTACH", 71);
			try
			{
				int _type = ATTACH;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:835:7: ( A T T A C H )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:835:9: A T T A C H
				{
					DebugLocation(835, 9);
					mA();
					DebugLocation(835, 11);
					mT();
					DebugLocation(835, 13);
					mT();
					DebugLocation(835, 15);
					mA();
					DebugLocation(835, 17);
					mC();
					DebugLocation(835, 19);
					mH();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ATTACH", 71);
				LeaveRule("ATTACH", 71);
				Leave_ATTACH();
			}
		}
		// $ANTLR end "ATTACH"

		partial void Enter_AUTOINCREMENT();
		partial void Leave_AUTOINCREMENT();

		// $ANTLR start "AUTOINCREMENT"
		[GrammarRule("AUTOINCREMENT")]
		private void mAUTOINCREMENT()
		{
			Enter_AUTOINCREMENT();
			EnterRule("AUTOINCREMENT", 72);
			TraceIn("AUTOINCREMENT", 72);
			try
			{
				int _type = AUTOINCREMENT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:836:14: ( A U T O I N C R E M E N T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:836:16: A U T O I N C R E M E N T
				{
					DebugLocation(836, 16);
					mA();
					DebugLocation(836, 18);
					mU();
					DebugLocation(836, 20);
					mT();
					DebugLocation(836, 22);
					mO();
					DebugLocation(836, 24);
					mI();
					DebugLocation(836, 26);
					mN();
					DebugLocation(836, 28);
					mC();
					DebugLocation(836, 30);
					mR();
					DebugLocation(836, 32);
					mE();
					DebugLocation(836, 34);
					mM();
					DebugLocation(836, 36);
					mE();
					DebugLocation(836, 38);
					mN();
					DebugLocation(836, 40);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("AUTOINCREMENT", 72);
				LeaveRule("AUTOINCREMENT", 72);
				Leave_AUTOINCREMENT();
			}
		}
		// $ANTLR end "AUTOINCREMENT"

		partial void Enter_BEFORE();
		partial void Leave_BEFORE();

		// $ANTLR start "BEFORE"
		[GrammarRule("BEFORE")]
		private void mBEFORE()
		{
			Enter_BEFORE();
			EnterRule("BEFORE", 73);
			TraceIn("BEFORE", 73);
			try
			{
				int _type = BEFORE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:837:7: ( B E F O R E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:837:9: B E F O R E
				{
					DebugLocation(837, 9);
					mB();
					DebugLocation(837, 11);
					mE();
					DebugLocation(837, 13);
					mF();
					DebugLocation(837, 15);
					mO();
					DebugLocation(837, 17);
					mR();
					DebugLocation(837, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("BEFORE", 73);
				LeaveRule("BEFORE", 73);
				Leave_BEFORE();
			}
		}
		// $ANTLR end "BEFORE"

		partial void Enter_BEGIN();
		partial void Leave_BEGIN();

		// $ANTLR start "BEGIN"
		[GrammarRule("BEGIN")]
		private void mBEGIN()
		{
			Enter_BEGIN();
			EnterRule("BEGIN", 74);
			TraceIn("BEGIN", 74);
			try
			{
				int _type = BEGIN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:838:6: ( B E G I N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:838:8: B E G I N
				{
					DebugLocation(838, 8);
					mB();
					DebugLocation(838, 10);
					mE();
					DebugLocation(838, 12);
					mG();
					DebugLocation(838, 14);
					mI();
					DebugLocation(838, 16);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("BEGIN", 74);
				LeaveRule("BEGIN", 74);
				Leave_BEGIN();
			}
		}
		// $ANTLR end "BEGIN"

		partial void Enter_BETWEEN();
		partial void Leave_BETWEEN();

		// $ANTLR start "BETWEEN"
		[GrammarRule("BETWEEN")]
		private void mBETWEEN()
		{
			Enter_BETWEEN();
			EnterRule("BETWEEN", 75);
			TraceIn("BETWEEN", 75);
			try
			{
				int _type = BETWEEN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:839:8: ( B E T W E E N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:839:10: B E T W E E N
				{
					DebugLocation(839, 10);
					mB();
					DebugLocation(839, 12);
					mE();
					DebugLocation(839, 14);
					mT();
					DebugLocation(839, 16);
					mW();
					DebugLocation(839, 18);
					mE();
					DebugLocation(839, 20);
					mE();
					DebugLocation(839, 22);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("BETWEEN", 75);
				LeaveRule("BETWEEN", 75);
				Leave_BETWEEN();
			}
		}
		// $ANTLR end "BETWEEN"

		partial void Enter_BY();
		partial void Leave_BY();

		// $ANTLR start "BY"
		[GrammarRule("BY")]
		private void mBY()
		{
			Enter_BY();
			EnterRule("BY", 76);
			TraceIn("BY", 76);
			try
			{
				int _type = BY;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:840:3: ( B Y )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:840:5: B Y
				{
					DebugLocation(840, 5);
					mB();
					DebugLocation(840, 7);
					mY();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("BY", 76);
				LeaveRule("BY", 76);
				Leave_BY();
			}
		}
		// $ANTLR end "BY"

		partial void Enter_CASCADE();
		partial void Leave_CASCADE();

		// $ANTLR start "CASCADE"
		[GrammarRule("CASCADE")]
		private void mCASCADE()
		{
			Enter_CASCADE();
			EnterRule("CASCADE", 77);
			TraceIn("CASCADE", 77);
			try
			{
				int _type = CASCADE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:841:8: ( C A S C A D E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:841:10: C A S C A D E
				{
					DebugLocation(841, 10);
					mC();
					DebugLocation(841, 12);
					mA();
					DebugLocation(841, 14);
					mS();
					DebugLocation(841, 16);
					mC();
					DebugLocation(841, 18);
					mA();
					DebugLocation(841, 20);
					mD();
					DebugLocation(841, 22);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CASCADE", 77);
				LeaveRule("CASCADE", 77);
				Leave_CASCADE();
			}
		}
		// $ANTLR end "CASCADE"

		partial void Enter_CASE();
		partial void Leave_CASE();

		// $ANTLR start "CASE"
		[GrammarRule("CASE")]
		private void mCASE()
		{
			Enter_CASE();
			EnterRule("CASE", 78);
			TraceIn("CASE", 78);
			try
			{
				int _type = CASE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:842:5: ( C A S E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:842:7: C A S E
				{
					DebugLocation(842, 7);
					mC();
					DebugLocation(842, 9);
					mA();
					DebugLocation(842, 11);
					mS();
					DebugLocation(842, 13);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CASE", 78);
				LeaveRule("CASE", 78);
				Leave_CASE();
			}
		}
		// $ANTLR end "CASE"

		partial void Enter_CAST();
		partial void Leave_CAST();

		// $ANTLR start "CAST"
		[GrammarRule("CAST")]
		private void mCAST()
		{
			Enter_CAST();
			EnterRule("CAST", 79);
			TraceIn("CAST", 79);
			try
			{
				int _type = CAST;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:843:5: ( C A S T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:843:7: C A S T
				{
					DebugLocation(843, 7);
					mC();
					DebugLocation(843, 9);
					mA();
					DebugLocation(843, 11);
					mS();
					DebugLocation(843, 13);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CAST", 79);
				LeaveRule("CAST", 79);
				Leave_CAST();
			}
		}
		// $ANTLR end "CAST"

		partial void Enter_CHECK();
		partial void Leave_CHECK();

		// $ANTLR start "CHECK"
		[GrammarRule("CHECK")]
		private void mCHECK()
		{
			Enter_CHECK();
			EnterRule("CHECK", 80);
			TraceIn("CHECK", 80);
			try
			{
				int _type = CHECK;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:844:6: ( C H E C K )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:844:8: C H E C K
				{
					DebugLocation(844, 8);
					mC();
					DebugLocation(844, 10);
					mH();
					DebugLocation(844, 12);
					mE();
					DebugLocation(844, 14);
					mC();
					DebugLocation(844, 16);
					mK();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CHECK", 80);
				LeaveRule("CHECK", 80);
				Leave_CHECK();
			}
		}
		// $ANTLR end "CHECK"

		partial void Enter_COLLATE();
		partial void Leave_COLLATE();

		// $ANTLR start "COLLATE"
		[GrammarRule("COLLATE")]
		private void mCOLLATE()
		{
			Enter_COLLATE();
			EnterRule("COLLATE", 81);
			TraceIn("COLLATE", 81);
			try
			{
				int _type = COLLATE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:845:8: ( C O L L A T E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:845:10: C O L L A T E
				{
					DebugLocation(845, 10);
					mC();
					DebugLocation(845, 12);
					mO();
					DebugLocation(845, 14);
					mL();
					DebugLocation(845, 16);
					mL();
					DebugLocation(845, 18);
					mA();
					DebugLocation(845, 20);
					mT();
					DebugLocation(845, 22);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("COLLATE", 81);
				LeaveRule("COLLATE", 81);
				Leave_COLLATE();
			}
		}
		// $ANTLR end "COLLATE"

		partial void Enter_COLUMN();
		partial void Leave_COLUMN();

		// $ANTLR start "COLUMN"
		[GrammarRule("COLUMN")]
		private void mCOLUMN()
		{
			Enter_COLUMN();
			EnterRule("COLUMN", 82);
			TraceIn("COLUMN", 82);
			try
			{
				int _type = COLUMN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:846:7: ( C O L U M N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:846:9: C O L U M N
				{
					DebugLocation(846, 9);
					mC();
					DebugLocation(846, 11);
					mO();
					DebugLocation(846, 13);
					mL();
					DebugLocation(846, 15);
					mU();
					DebugLocation(846, 17);
					mM();
					DebugLocation(846, 19);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("COLUMN", 82);
				LeaveRule("COLUMN", 82);
				Leave_COLUMN();
			}
		}
		// $ANTLR end "COLUMN"

		partial void Enter_COMMIT();
		partial void Leave_COMMIT();

		// $ANTLR start "COMMIT"
		[GrammarRule("COMMIT")]
		private void mCOMMIT()
		{
			Enter_COMMIT();
			EnterRule("COMMIT", 83);
			TraceIn("COMMIT", 83);
			try
			{
				int _type = COMMIT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:847:7: ( C O M M I T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:847:9: C O M M I T
				{
					DebugLocation(847, 9);
					mC();
					DebugLocation(847, 11);
					mO();
					DebugLocation(847, 13);
					mM();
					DebugLocation(847, 15);
					mM();
					DebugLocation(847, 17);
					mI();
					DebugLocation(847, 19);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("COMMIT", 83);
				LeaveRule("COMMIT", 83);
				Leave_COMMIT();
			}
		}
		// $ANTLR end "COMMIT"

		partial void Enter_CONFLICT();
		partial void Leave_CONFLICT();

		// $ANTLR start "CONFLICT"
		[GrammarRule("CONFLICT")]
		private void mCONFLICT()
		{
			Enter_CONFLICT();
			EnterRule("CONFLICT", 84);
			TraceIn("CONFLICT", 84);
			try
			{
				int _type = CONFLICT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:848:9: ( C O N F L I C T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:848:11: C O N F L I C T
				{
					DebugLocation(848, 11);
					mC();
					DebugLocation(848, 13);
					mO();
					DebugLocation(848, 15);
					mN();
					DebugLocation(848, 17);
					mF();
					DebugLocation(848, 19);
					mL();
					DebugLocation(848, 21);
					mI();
					DebugLocation(848, 23);
					mC();
					DebugLocation(848, 25);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CONFLICT", 84);
				LeaveRule("CONFLICT", 84);
				Leave_CONFLICT();
			}
		}
		// $ANTLR end "CONFLICT"

		partial void Enter_CONSTRAINT();
		partial void Leave_CONSTRAINT();

		// $ANTLR start "CONSTRAINT"
		[GrammarRule("CONSTRAINT")]
		private void mCONSTRAINT()
		{
			Enter_CONSTRAINT();
			EnterRule("CONSTRAINT", 85);
			TraceIn("CONSTRAINT", 85);
			try
			{
				int _type = CONSTRAINT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:849:11: ( C O N S T R A I N T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:849:13: C O N S T R A I N T
				{
					DebugLocation(849, 13);
					mC();
					DebugLocation(849, 15);
					mO();
					DebugLocation(849, 17);
					mN();
					DebugLocation(849, 19);
					mS();
					DebugLocation(849, 21);
					mT();
					DebugLocation(849, 23);
					mR();
					DebugLocation(849, 25);
					mA();
					DebugLocation(849, 27);
					mI();
					DebugLocation(849, 29);
					mN();
					DebugLocation(849, 31);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CONSTRAINT", 85);
				LeaveRule("CONSTRAINT", 85);
				Leave_CONSTRAINT();
			}
		}
		// $ANTLR end "CONSTRAINT"

		partial void Enter_CREATE();
		partial void Leave_CREATE();

		// $ANTLR start "CREATE"
		[GrammarRule("CREATE")]
		private void mCREATE()
		{
			Enter_CREATE();
			EnterRule("CREATE", 86);
			TraceIn("CREATE", 86);
			try
			{
				int _type = CREATE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:850:7: ( C R E A T E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:850:9: C R E A T E
				{
					DebugLocation(850, 9);
					mC();
					DebugLocation(850, 11);
					mR();
					DebugLocation(850, 13);
					mE();
					DebugLocation(850, 15);
					mA();
					DebugLocation(850, 17);
					mT();
					DebugLocation(850, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CREATE", 86);
				LeaveRule("CREATE", 86);
				Leave_CREATE();
			}
		}
		// $ANTLR end "CREATE"

		partial void Enter_CROSS();
		partial void Leave_CROSS();

		// $ANTLR start "CROSS"
		[GrammarRule("CROSS")]
		private void mCROSS()
		{
			Enter_CROSS();
			EnterRule("CROSS", 87);
			TraceIn("CROSS", 87);
			try
			{
				int _type = CROSS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:851:6: ( C R O S S )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:851:8: C R O S S
				{
					DebugLocation(851, 8);
					mC();
					DebugLocation(851, 10);
					mR();
					DebugLocation(851, 12);
					mO();
					DebugLocation(851, 14);
					mS();
					DebugLocation(851, 16);
					mS();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CROSS", 87);
				LeaveRule("CROSS", 87);
				Leave_CROSS();
			}
		}
		// $ANTLR end "CROSS"

		partial void Enter_CURRENT_TIME();
		partial void Leave_CURRENT_TIME();

		// $ANTLR start "CURRENT_TIME"
		[GrammarRule("CURRENT_TIME")]
		private void mCURRENT_TIME()
		{
			Enter_CURRENT_TIME();
			EnterRule("CURRENT_TIME", 88);
			TraceIn("CURRENT_TIME", 88);
			try
			{
				int _type = CURRENT_TIME;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:852:13: ( C U R R E N T '_' T I M E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:852:15: C U R R E N T '_' T I M E
				{
					DebugLocation(852, 15);
					mC();
					DebugLocation(852, 17);
					mU();
					DebugLocation(852, 19);
					mR();
					DebugLocation(852, 21);
					mR();
					DebugLocation(852, 23);
					mE();
					DebugLocation(852, 25);
					mN();
					DebugLocation(852, 27);
					mT();
					DebugLocation(852, 29);
					Match('_');
					DebugLocation(852, 33);
					mT();
					DebugLocation(852, 35);
					mI();
					DebugLocation(852, 37);
					mM();
					DebugLocation(852, 39);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CURRENT_TIME", 88);
				LeaveRule("CURRENT_TIME", 88);
				Leave_CURRENT_TIME();
			}
		}
		// $ANTLR end "CURRENT_TIME"

		partial void Enter_CURRENT_DATE();
		partial void Leave_CURRENT_DATE();

		// $ANTLR start "CURRENT_DATE"
		[GrammarRule("CURRENT_DATE")]
		private void mCURRENT_DATE()
		{
			Enter_CURRENT_DATE();
			EnterRule("CURRENT_DATE", 89);
			TraceIn("CURRENT_DATE", 89);
			try
			{
				int _type = CURRENT_DATE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:853:13: ( C U R R E N T '_' D A T E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:853:15: C U R R E N T '_' D A T E
				{
					DebugLocation(853, 15);
					mC();
					DebugLocation(853, 17);
					mU();
					DebugLocation(853, 19);
					mR();
					DebugLocation(853, 21);
					mR();
					DebugLocation(853, 23);
					mE();
					DebugLocation(853, 25);
					mN();
					DebugLocation(853, 27);
					mT();
					DebugLocation(853, 29);
					Match('_');
					DebugLocation(853, 33);
					mD();
					DebugLocation(853, 35);
					mA();
					DebugLocation(853, 37);
					mT();
					DebugLocation(853, 39);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CURRENT_DATE", 89);
				LeaveRule("CURRENT_DATE", 89);
				Leave_CURRENT_DATE();
			}
		}
		// $ANTLR end "CURRENT_DATE"

		partial void Enter_CURRENT_TIMESTAMP();
		partial void Leave_CURRENT_TIMESTAMP();

		// $ANTLR start "CURRENT_TIMESTAMP"
		[GrammarRule("CURRENT_TIMESTAMP")]
		private void mCURRENT_TIMESTAMP()
		{
			Enter_CURRENT_TIMESTAMP();
			EnterRule("CURRENT_TIMESTAMP", 90);
			TraceIn("CURRENT_TIMESTAMP", 90);
			try
			{
				int _type = CURRENT_TIMESTAMP;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:854:18: ( C U R R E N T '_' T I M E S T A M P )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:854:20: C U R R E N T '_' T I M E S T A M P
				{
					DebugLocation(854, 20);
					mC();
					DebugLocation(854, 22);
					mU();
					DebugLocation(854, 24);
					mR();
					DebugLocation(854, 26);
					mR();
					DebugLocation(854, 28);
					mE();
					DebugLocation(854, 30);
					mN();
					DebugLocation(854, 32);
					mT();
					DebugLocation(854, 34);
					Match('_');
					DebugLocation(854, 38);
					mT();
					DebugLocation(854, 40);
					mI();
					DebugLocation(854, 42);
					mM();
					DebugLocation(854, 44);
					mE();
					DebugLocation(854, 46);
					mS();
					DebugLocation(854, 48);
					mT();
					DebugLocation(854, 50);
					mA();
					DebugLocation(854, 52);
					mM();
					DebugLocation(854, 54);
					mP();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("CURRENT_TIMESTAMP", 90);
				LeaveRule("CURRENT_TIMESTAMP", 90);
				Leave_CURRENT_TIMESTAMP();
			}
		}
		// $ANTLR end "CURRENT_TIMESTAMP"

		partial void Enter_DATABASE();
		partial void Leave_DATABASE();

		// $ANTLR start "DATABASE"
		[GrammarRule("DATABASE")]
		private void mDATABASE()
		{
			Enter_DATABASE();
			EnterRule("DATABASE", 91);
			TraceIn("DATABASE", 91);
			try
			{
				int _type = DATABASE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:855:9: ( D A T A B A S E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:855:11: D A T A B A S E
				{
					DebugLocation(855, 11);
					mD();
					DebugLocation(855, 13);
					mA();
					DebugLocation(855, 15);
					mT();
					DebugLocation(855, 17);
					mA();
					DebugLocation(855, 19);
					mB();
					DebugLocation(855, 21);
					mA();
					DebugLocation(855, 23);
					mS();
					DebugLocation(855, 25);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DATABASE", 91);
				LeaveRule("DATABASE", 91);
				Leave_DATABASE();
			}
		}
		// $ANTLR end "DATABASE"

		partial void Enter_DEFAULT();
		partial void Leave_DEFAULT();

		// $ANTLR start "DEFAULT"
		[GrammarRule("DEFAULT")]
		private void mDEFAULT()
		{
			Enter_DEFAULT();
			EnterRule("DEFAULT", 92);
			TraceIn("DEFAULT", 92);
			try
			{
				int _type = DEFAULT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:856:8: ( D E F A U L T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:856:10: D E F A U L T
				{
					DebugLocation(856, 10);
					mD();
					DebugLocation(856, 12);
					mE();
					DebugLocation(856, 14);
					mF();
					DebugLocation(856, 16);
					mA();
					DebugLocation(856, 18);
					mU();
					DebugLocation(856, 20);
					mL();
					DebugLocation(856, 22);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DEFAULT", 92);
				LeaveRule("DEFAULT", 92);
				Leave_DEFAULT();
			}
		}
		// $ANTLR end "DEFAULT"

		partial void Enter_DEFERRABLE();
		partial void Leave_DEFERRABLE();

		// $ANTLR start "DEFERRABLE"
		[GrammarRule("DEFERRABLE")]
		private void mDEFERRABLE()
		{
			Enter_DEFERRABLE();
			EnterRule("DEFERRABLE", 93);
			TraceIn("DEFERRABLE", 93);
			try
			{
				int _type = DEFERRABLE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:857:11: ( D E F E R R A B L E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:857:13: D E F E R R A B L E
				{
					DebugLocation(857, 13);
					mD();
					DebugLocation(857, 15);
					mE();
					DebugLocation(857, 17);
					mF();
					DebugLocation(857, 19);
					mE();
					DebugLocation(857, 21);
					mR();
					DebugLocation(857, 23);
					mR();
					DebugLocation(857, 25);
					mA();
					DebugLocation(857, 27);
					mB();
					DebugLocation(857, 29);
					mL();
					DebugLocation(857, 31);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DEFERRABLE", 93);
				LeaveRule("DEFERRABLE", 93);
				Leave_DEFERRABLE();
			}
		}
		// $ANTLR end "DEFERRABLE"

		partial void Enter_DEFERRED();
		partial void Leave_DEFERRED();

		// $ANTLR start "DEFERRED"
		[GrammarRule("DEFERRED")]
		private void mDEFERRED()
		{
			Enter_DEFERRED();
			EnterRule("DEFERRED", 94);
			TraceIn("DEFERRED", 94);
			try
			{
				int _type = DEFERRED;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:858:9: ( D E F E R R E D )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:858:11: D E F E R R E D
				{
					DebugLocation(858, 11);
					mD();
					DebugLocation(858, 13);
					mE();
					DebugLocation(858, 15);
					mF();
					DebugLocation(858, 17);
					mE();
					DebugLocation(858, 19);
					mR();
					DebugLocation(858, 21);
					mR();
					DebugLocation(858, 23);
					mE();
					DebugLocation(858, 25);
					mD();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DEFERRED", 94);
				LeaveRule("DEFERRED", 94);
				Leave_DEFERRED();
			}
		}
		// $ANTLR end "DEFERRED"

		partial void Enter_DELETE();
		partial void Leave_DELETE();

		// $ANTLR start "DELETE"
		[GrammarRule("DELETE")]
		private void mDELETE()
		{
			Enter_DELETE();
			EnterRule("DELETE", 95);
			TraceIn("DELETE", 95);
			try
			{
				int _type = DELETE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:859:7: ( D E L E T E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:859:9: D E L E T E
				{
					DebugLocation(859, 9);
					mD();
					DebugLocation(859, 11);
					mE();
					DebugLocation(859, 13);
					mL();
					DebugLocation(859, 15);
					mE();
					DebugLocation(859, 17);
					mT();
					DebugLocation(859, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DELETE", 95);
				LeaveRule("DELETE", 95);
				Leave_DELETE();
			}
		}
		// $ANTLR end "DELETE"

		partial void Enter_DESC();
		partial void Leave_DESC();

		// $ANTLR start "DESC"
		[GrammarRule("DESC")]
		private void mDESC()
		{
			Enter_DESC();
			EnterRule("DESC", 96);
			TraceIn("DESC", 96);
			try
			{
				int _type = DESC;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:860:5: ( D E S C )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:860:7: D E S C
				{
					DebugLocation(860, 7);
					mD();
					DebugLocation(860, 9);
					mE();
					DebugLocation(860, 11);
					mS();
					DebugLocation(860, 13);
					mC();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DESC", 96);
				LeaveRule("DESC", 96);
				Leave_DESC();
			}
		}
		// $ANTLR end "DESC"

		partial void Enter_DETACH();
		partial void Leave_DETACH();

		// $ANTLR start "DETACH"
		[GrammarRule("DETACH")]
		private void mDETACH()
		{
			Enter_DETACH();
			EnterRule("DETACH", 97);
			TraceIn("DETACH", 97);
			try
			{
				int _type = DETACH;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:861:7: ( D E T A C H )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:861:9: D E T A C H
				{
					DebugLocation(861, 9);
					mD();
					DebugLocation(861, 11);
					mE();
					DebugLocation(861, 13);
					mT();
					DebugLocation(861, 15);
					mA();
					DebugLocation(861, 17);
					mC();
					DebugLocation(861, 19);
					mH();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DETACH", 97);
				LeaveRule("DETACH", 97);
				Leave_DETACH();
			}
		}
		// $ANTLR end "DETACH"

		partial void Enter_DISTINCT();
		partial void Leave_DISTINCT();

		// $ANTLR start "DISTINCT"
		[GrammarRule("DISTINCT")]
		private void mDISTINCT()
		{
			Enter_DISTINCT();
			EnterRule("DISTINCT", 98);
			TraceIn("DISTINCT", 98);
			try
			{
				int _type = DISTINCT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:862:9: ( D I S T I N C T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:862:11: D I S T I N C T
				{
					DebugLocation(862, 11);
					mD();
					DebugLocation(862, 13);
					mI();
					DebugLocation(862, 15);
					mS();
					DebugLocation(862, 17);
					mT();
					DebugLocation(862, 19);
					mI();
					DebugLocation(862, 21);
					mN();
					DebugLocation(862, 23);
					mC();
					DebugLocation(862, 25);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DISTINCT", 98);
				LeaveRule("DISTINCT", 98);
				Leave_DISTINCT();
			}
		}
		// $ANTLR end "DISTINCT"

		partial void Enter_DROP();
		partial void Leave_DROP();

		// $ANTLR start "DROP"
		[GrammarRule("DROP")]
		private void mDROP()
		{
			Enter_DROP();
			EnterRule("DROP", 99);
			TraceIn("DROP", 99);
			try
			{
				int _type = DROP;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:863:5: ( D R O P )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:863:7: D R O P
				{
					DebugLocation(863, 7);
					mD();
					DebugLocation(863, 9);
					mR();
					DebugLocation(863, 11);
					mO();
					DebugLocation(863, 13);
					mP();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("DROP", 99);
				LeaveRule("DROP", 99);
				Leave_DROP();
			}
		}
		// $ANTLR end "DROP"

		partial void Enter_EACH();
		partial void Leave_EACH();

		// $ANTLR start "EACH"
		[GrammarRule("EACH")]
		private void mEACH()
		{
			Enter_EACH();
			EnterRule("EACH", 100);
			TraceIn("EACH", 100);
			try
			{
				int _type = EACH;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:864:5: ( E A C H )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:864:7: E A C H
				{
					DebugLocation(864, 7);
					mE();
					DebugLocation(864, 9);
					mA();
					DebugLocation(864, 11);
					mC();
					DebugLocation(864, 13);
					mH();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EACH", 100);
				LeaveRule("EACH", 100);
				Leave_EACH();
			}
		}
		// $ANTLR end "EACH"

		partial void Enter_ELSE();
		partial void Leave_ELSE();

		// $ANTLR start "ELSE"
		[GrammarRule("ELSE")]
		private void mELSE()
		{
			Enter_ELSE();
			EnterRule("ELSE", 101);
			TraceIn("ELSE", 101);
			try
			{
				int _type = ELSE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:865:5: ( E L S E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:865:7: E L S E
				{
					DebugLocation(865, 7);
					mE();
					DebugLocation(865, 9);
					mL();
					DebugLocation(865, 11);
					mS();
					DebugLocation(865, 13);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ELSE", 101);
				LeaveRule("ELSE", 101);
				Leave_ELSE();
			}
		}
		// $ANTLR end "ELSE"

		partial void Enter_END();
		partial void Leave_END();

		// $ANTLR start "END"
		[GrammarRule("END")]
		private void mEND()
		{
			Enter_END();
			EnterRule("END", 102);
			TraceIn("END", 102);
			try
			{
				int _type = END;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:866:4: ( E N D )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:866:6: E N D
				{
					DebugLocation(866, 6);
					mE();
					DebugLocation(866, 8);
					mN();
					DebugLocation(866, 10);
					mD();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("END", 102);
				LeaveRule("END", 102);
				Leave_END();
			}
		}
		// $ANTLR end "END"

		partial void Enter_ESCAPE();
		partial void Leave_ESCAPE();

		// $ANTLR start "ESCAPE"
		[GrammarRule("ESCAPE")]
		private void mESCAPE()
		{
			Enter_ESCAPE();
			EnterRule("ESCAPE", 103);
			TraceIn("ESCAPE", 103);
			try
			{
				int _type = ESCAPE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:867:7: ( E S C A P E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:867:9: E S C A P E
				{
					DebugLocation(867, 9);
					mE();
					DebugLocation(867, 11);
					mS();
					DebugLocation(867, 13);
					mC();
					DebugLocation(867, 15);
					mA();
					DebugLocation(867, 17);
					mP();
					DebugLocation(867, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ESCAPE", 103);
				LeaveRule("ESCAPE", 103);
				Leave_ESCAPE();
			}
		}
		// $ANTLR end "ESCAPE"

		partial void Enter_EXCEPT();
		partial void Leave_EXCEPT();

		// $ANTLR start "EXCEPT"
		[GrammarRule("EXCEPT")]
		private void mEXCEPT()
		{
			Enter_EXCEPT();
			EnterRule("EXCEPT", 104);
			TraceIn("EXCEPT", 104);
			try
			{
				int _type = EXCEPT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:868:7: ( E X C E P T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:868:9: E X C E P T
				{
					DebugLocation(868, 9);
					mE();
					DebugLocation(868, 11);
					mX();
					DebugLocation(868, 13);
					mC();
					DebugLocation(868, 15);
					mE();
					DebugLocation(868, 17);
					mP();
					DebugLocation(868, 19);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EXCEPT", 104);
				LeaveRule("EXCEPT", 104);
				Leave_EXCEPT();
			}
		}
		// $ANTLR end "EXCEPT"

		partial void Enter_EXCLUSIVE();
		partial void Leave_EXCLUSIVE();

		// $ANTLR start "EXCLUSIVE"
		[GrammarRule("EXCLUSIVE")]
		private void mEXCLUSIVE()
		{
			Enter_EXCLUSIVE();
			EnterRule("EXCLUSIVE", 105);
			TraceIn("EXCLUSIVE", 105);
			try
			{
				int _type = EXCLUSIVE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:869:10: ( E X C L U S I V E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:869:12: E X C L U S I V E
				{
					DebugLocation(869, 12);
					mE();
					DebugLocation(869, 14);
					mX();
					DebugLocation(869, 16);
					mC();
					DebugLocation(869, 18);
					mL();
					DebugLocation(869, 20);
					mU();
					DebugLocation(869, 22);
					mS();
					DebugLocation(869, 24);
					mI();
					DebugLocation(869, 26);
					mV();
					DebugLocation(869, 28);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EXCLUSIVE", 105);
				LeaveRule("EXCLUSIVE", 105);
				Leave_EXCLUSIVE();
			}
		}
		// $ANTLR end "EXCLUSIVE"

		partial void Enter_EXISTS();
		partial void Leave_EXISTS();

		// $ANTLR start "EXISTS"
		[GrammarRule("EXISTS")]
		private void mEXISTS()
		{
			Enter_EXISTS();
			EnterRule("EXISTS", 106);
			TraceIn("EXISTS", 106);
			try
			{
				int _type = EXISTS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:870:7: ( E X I S T S )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:870:9: E X I S T S
				{
					DebugLocation(870, 9);
					mE();
					DebugLocation(870, 11);
					mX();
					DebugLocation(870, 13);
					mI();
					DebugLocation(870, 15);
					mS();
					DebugLocation(870, 17);
					mT();
					DebugLocation(870, 19);
					mS();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EXISTS", 106);
				LeaveRule("EXISTS", 106);
				Leave_EXISTS();
			}
		}
		// $ANTLR end "EXISTS"

		partial void Enter_EXPLAIN();
		partial void Leave_EXPLAIN();

		// $ANTLR start "EXPLAIN"
		[GrammarRule("EXPLAIN")]
		private void mEXPLAIN()
		{
			Enter_EXPLAIN();
			EnterRule("EXPLAIN", 107);
			TraceIn("EXPLAIN", 107);
			try
			{
				int _type = EXPLAIN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:871:8: ( E X P L A I N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:871:10: E X P L A I N
				{
					DebugLocation(871, 10);
					mE();
					DebugLocation(871, 12);
					mX();
					DebugLocation(871, 14);
					mP();
					DebugLocation(871, 16);
					mL();
					DebugLocation(871, 18);
					mA();
					DebugLocation(871, 20);
					mI();
					DebugLocation(871, 22);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("EXPLAIN", 107);
				LeaveRule("EXPLAIN", 107);
				Leave_EXPLAIN();
			}
		}
		// $ANTLR end "EXPLAIN"

		partial void Enter_FAIL();
		partial void Leave_FAIL();

		// $ANTLR start "FAIL"
		[GrammarRule("FAIL")]
		private void mFAIL()
		{
			Enter_FAIL();
			EnterRule("FAIL", 108);
			TraceIn("FAIL", 108);
			try
			{
				int _type = FAIL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:872:5: ( F A I L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:872:7: F A I L
				{
					DebugLocation(872, 7);
					mF();
					DebugLocation(872, 9);
					mA();
					DebugLocation(872, 11);
					mI();
					DebugLocation(872, 13);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("FAIL", 108);
				LeaveRule("FAIL", 108);
				Leave_FAIL();
			}
		}
		// $ANTLR end "FAIL"

		partial void Enter_FOR();
		partial void Leave_FOR();

		// $ANTLR start "FOR"
		[GrammarRule("FOR")]
		private void mFOR()
		{
			Enter_FOR();
			EnterRule("FOR", 109);
			TraceIn("FOR", 109);
			try
			{
				int _type = FOR;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:873:4: ( F O R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:873:6: F O R
				{
					DebugLocation(873, 6);
					mF();
					DebugLocation(873, 8);
					mO();
					DebugLocation(873, 10);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("FOR", 109);
				LeaveRule("FOR", 109);
				Leave_FOR();
			}
		}
		// $ANTLR end "FOR"

		partial void Enter_FOREIGN();
		partial void Leave_FOREIGN();

		// $ANTLR start "FOREIGN"
		[GrammarRule("FOREIGN")]
		private void mFOREIGN()
		{
			Enter_FOREIGN();
			EnterRule("FOREIGN", 110);
			TraceIn("FOREIGN", 110);
			try
			{
				int _type = FOREIGN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:874:8: ( F O R E I G N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:874:10: F O R E I G N
				{
					DebugLocation(874, 10);
					mF();
					DebugLocation(874, 12);
					mO();
					DebugLocation(874, 14);
					mR();
					DebugLocation(874, 16);
					mE();
					DebugLocation(874, 18);
					mI();
					DebugLocation(874, 20);
					mG();
					DebugLocation(874, 22);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("FOREIGN", 110);
				LeaveRule("FOREIGN", 110);
				Leave_FOREIGN();
			}
		}
		// $ANTLR end "FOREIGN"

		partial void Enter_FROM();
		partial void Leave_FROM();

		// $ANTLR start "FROM"
		[GrammarRule("FROM")]
		private void mFROM()
		{
			Enter_FROM();
			EnterRule("FROM", 111);
			TraceIn("FROM", 111);
			try
			{
				int _type = FROM;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:875:5: ( F R O M )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:875:7: F R O M
				{
					DebugLocation(875, 7);
					mF();
					DebugLocation(875, 9);
					mR();
					DebugLocation(875, 11);
					mO();
					DebugLocation(875, 13);
					mM();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("FROM", 111);
				LeaveRule("FROM", 111);
				Leave_FROM();
			}
		}
		// $ANTLR end "FROM"

		partial void Enter_GLOB();
		partial void Leave_GLOB();

		// $ANTLR start "GLOB"
		[GrammarRule("GLOB")]
		private void mGLOB()
		{
			Enter_GLOB();
			EnterRule("GLOB", 112);
			TraceIn("GLOB", 112);
			try
			{
				int _type = GLOB;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:876:5: ( G L O B )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:876:7: G L O B
				{
					DebugLocation(876, 7);
					mG();
					DebugLocation(876, 9);
					mL();
					DebugLocation(876, 11);
					mO();
					DebugLocation(876, 13);
					mB();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("GLOB", 112);
				LeaveRule("GLOB", 112);
				Leave_GLOB();
			}
		}
		// $ANTLR end "GLOB"

		partial void Enter_GROUP();
		partial void Leave_GROUP();

		// $ANTLR start "GROUP"
		[GrammarRule("GROUP")]
		private void mGROUP()
		{
			Enter_GROUP();
			EnterRule("GROUP", 113);
			TraceIn("GROUP", 113);
			try
			{
				int _type = GROUP;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:877:6: ( G R O U P )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:877:8: G R O U P
				{
					DebugLocation(877, 8);
					mG();
					DebugLocation(877, 10);
					mR();
					DebugLocation(877, 12);
					mO();
					DebugLocation(877, 14);
					mU();
					DebugLocation(877, 16);
					mP();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("GROUP", 113);
				LeaveRule("GROUP", 113);
				Leave_GROUP();
			}
		}
		// $ANTLR end "GROUP"

		partial void Enter_HAVING();
		partial void Leave_HAVING();

		// $ANTLR start "HAVING"
		[GrammarRule("HAVING")]
		private void mHAVING()
		{
			Enter_HAVING();
			EnterRule("HAVING", 114);
			TraceIn("HAVING", 114);
			try
			{
				int _type = HAVING;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:878:7: ( H A V I N G )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:878:9: H A V I N G
				{
					DebugLocation(878, 9);
					mH();
					DebugLocation(878, 11);
					mA();
					DebugLocation(878, 13);
					mV();
					DebugLocation(878, 15);
					mI();
					DebugLocation(878, 17);
					mN();
					DebugLocation(878, 19);
					mG();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("HAVING", 114);
				LeaveRule("HAVING", 114);
				Leave_HAVING();
			}
		}
		// $ANTLR end "HAVING"

		partial void Enter_IF();
		partial void Leave_IF();

		// $ANTLR start "IF"
		[GrammarRule("IF")]
		private void mIF()
		{
			Enter_IF();
			EnterRule("IF", 115);
			TraceIn("IF", 115);
			try
			{
				int _type = IF;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:879:3: ( I F )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:879:5: I F
				{
					DebugLocation(879, 5);
					mI();
					DebugLocation(879, 7);
					mF();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("IF", 115);
				LeaveRule("IF", 115);
				Leave_IF();
			}
		}
		// $ANTLR end "IF"

		partial void Enter_IGNORE();
		partial void Leave_IGNORE();

		// $ANTLR start "IGNORE"
		[GrammarRule("IGNORE")]
		private void mIGNORE()
		{
			Enter_IGNORE();
			EnterRule("IGNORE", 116);
			TraceIn("IGNORE", 116);
			try
			{
				int _type = IGNORE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:880:7: ( I G N O R E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:880:9: I G N O R E
				{
					DebugLocation(880, 9);
					mI();
					DebugLocation(880, 11);
					mG();
					DebugLocation(880, 13);
					mN();
					DebugLocation(880, 15);
					mO();
					DebugLocation(880, 17);
					mR();
					DebugLocation(880, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("IGNORE", 116);
				LeaveRule("IGNORE", 116);
				Leave_IGNORE();
			}
		}
		// $ANTLR end "IGNORE"

		partial void Enter_IMMEDIATE();
		partial void Leave_IMMEDIATE();

		// $ANTLR start "IMMEDIATE"
		[GrammarRule("IMMEDIATE")]
		private void mIMMEDIATE()
		{
			Enter_IMMEDIATE();
			EnterRule("IMMEDIATE", 117);
			TraceIn("IMMEDIATE", 117);
			try
			{
				int _type = IMMEDIATE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:881:10: ( I M M E D I A T E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:881:12: I M M E D I A T E
				{
					DebugLocation(881, 12);
					mI();
					DebugLocation(881, 14);
					mM();
					DebugLocation(881, 16);
					mM();
					DebugLocation(881, 18);
					mE();
					DebugLocation(881, 20);
					mD();
					DebugLocation(881, 22);
					mI();
					DebugLocation(881, 24);
					mA();
					DebugLocation(881, 26);
					mT();
					DebugLocation(881, 28);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("IMMEDIATE", 117);
				LeaveRule("IMMEDIATE", 117);
				Leave_IMMEDIATE();
			}
		}
		// $ANTLR end "IMMEDIATE"

		partial void Enter_IN();
		partial void Leave_IN();

		// $ANTLR start "IN"
		[GrammarRule("IN")]
		private void mIN()
		{
			Enter_IN();
			EnterRule("IN", 118);
			TraceIn("IN", 118);
			try
			{
				int _type = IN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:882:3: ( I N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:882:5: I N
				{
					DebugLocation(882, 5);
					mI();
					DebugLocation(882, 7);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("IN", 118);
				LeaveRule("IN", 118);
				Leave_IN();
			}
		}
		// $ANTLR end "IN"

		partial void Enter_INDEX();
		partial void Leave_INDEX();

		// $ANTLR start "INDEX"
		[GrammarRule("INDEX")]
		private void mINDEX()
		{
			Enter_INDEX();
			EnterRule("INDEX", 119);
			TraceIn("INDEX", 119);
			try
			{
				int _type = INDEX;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:883:6: ( I N D E X )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:883:8: I N D E X
				{
					DebugLocation(883, 8);
					mI();
					DebugLocation(883, 10);
					mN();
					DebugLocation(883, 12);
					mD();
					DebugLocation(883, 14);
					mE();
					DebugLocation(883, 16);
					mX();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INDEX", 119);
				LeaveRule("INDEX", 119);
				Leave_INDEX();
			}
		}
		// $ANTLR end "INDEX"

		partial void Enter_INDEXED();
		partial void Leave_INDEXED();

		// $ANTLR start "INDEXED"
		[GrammarRule("INDEXED")]
		private void mINDEXED()
		{
			Enter_INDEXED();
			EnterRule("INDEXED", 120);
			TraceIn("INDEXED", 120);
			try
			{
				int _type = INDEXED;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:884:8: ( I N D E X E D )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:884:10: I N D E X E D
				{
					DebugLocation(884, 10);
					mI();
					DebugLocation(884, 12);
					mN();
					DebugLocation(884, 14);
					mD();
					DebugLocation(884, 16);
					mE();
					DebugLocation(884, 18);
					mX();
					DebugLocation(884, 20);
					mE();
					DebugLocation(884, 22);
					mD();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INDEXED", 120);
				LeaveRule("INDEXED", 120);
				Leave_INDEXED();
			}
		}
		// $ANTLR end "INDEXED"

		partial void Enter_INITIALLY();
		partial void Leave_INITIALLY();

		// $ANTLR start "INITIALLY"
		[GrammarRule("INITIALLY")]
		private void mINITIALLY()
		{
			Enter_INITIALLY();
			EnterRule("INITIALLY", 121);
			TraceIn("INITIALLY", 121);
			try
			{
				int _type = INITIALLY;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:885:10: ( I N I T I A L L Y )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:885:12: I N I T I A L L Y
				{
					DebugLocation(885, 12);
					mI();
					DebugLocation(885, 14);
					mN();
					DebugLocation(885, 16);
					mI();
					DebugLocation(885, 18);
					mT();
					DebugLocation(885, 20);
					mI();
					DebugLocation(885, 22);
					mA();
					DebugLocation(885, 24);
					mL();
					DebugLocation(885, 26);
					mL();
					DebugLocation(885, 28);
					mY();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INITIALLY", 121);
				LeaveRule("INITIALLY", 121);
				Leave_INITIALLY();
			}
		}
		// $ANTLR end "INITIALLY"

		partial void Enter_INNER();
		partial void Leave_INNER();

		// $ANTLR start "INNER"
		[GrammarRule("INNER")]
		private void mINNER()
		{
			Enter_INNER();
			EnterRule("INNER", 122);
			TraceIn("INNER", 122);
			try
			{
				int _type = INNER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:886:6: ( I N N E R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:886:8: I N N E R
				{
					DebugLocation(886, 8);
					mI();
					DebugLocation(886, 10);
					mN();
					DebugLocation(886, 12);
					mN();
					DebugLocation(886, 14);
					mE();
					DebugLocation(886, 16);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INNER", 122);
				LeaveRule("INNER", 122);
				Leave_INNER();
			}
		}
		// $ANTLR end "INNER"

		partial void Enter_INSERT();
		partial void Leave_INSERT();

		// $ANTLR start "INSERT"
		[GrammarRule("INSERT")]
		private void mINSERT()
		{
			Enter_INSERT();
			EnterRule("INSERT", 123);
			TraceIn("INSERT", 123);
			try
			{
				int _type = INSERT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:887:7: ( I N S E R T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:887:9: I N S E R T
				{
					DebugLocation(887, 9);
					mI();
					DebugLocation(887, 11);
					mN();
					DebugLocation(887, 13);
					mS();
					DebugLocation(887, 15);
					mE();
					DebugLocation(887, 17);
					mR();
					DebugLocation(887, 19);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INSERT", 123);
				LeaveRule("INSERT", 123);
				Leave_INSERT();
			}
		}
		// $ANTLR end "INSERT"

		partial void Enter_INSTEAD();
		partial void Leave_INSTEAD();

		// $ANTLR start "INSTEAD"
		[GrammarRule("INSTEAD")]
		private void mINSTEAD()
		{
			Enter_INSTEAD();
			EnterRule("INSTEAD", 124);
			TraceIn("INSTEAD", 124);
			try
			{
				int _type = INSTEAD;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:888:8: ( I N S T E A D )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:888:10: I N S T E A D
				{
					DebugLocation(888, 10);
					mI();
					DebugLocation(888, 12);
					mN();
					DebugLocation(888, 14);
					mS();
					DebugLocation(888, 16);
					mT();
					DebugLocation(888, 18);
					mE();
					DebugLocation(888, 20);
					mA();
					DebugLocation(888, 22);
					mD();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INSTEAD", 124);
				LeaveRule("INSTEAD", 124);
				Leave_INSTEAD();
			}
		}
		// $ANTLR end "INSTEAD"

		partial void Enter_INTERSECT();
		partial void Leave_INTERSECT();

		// $ANTLR start "INTERSECT"
		[GrammarRule("INTERSECT")]
		private void mINTERSECT()
		{
			Enter_INTERSECT();
			EnterRule("INTERSECT", 125);
			TraceIn("INTERSECT", 125);
			try
			{
				int _type = INTERSECT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:889:10: ( I N T E R S E C T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:889:12: I N T E R S E C T
				{
					DebugLocation(889, 12);
					mI();
					DebugLocation(889, 14);
					mN();
					DebugLocation(889, 16);
					mT();
					DebugLocation(889, 18);
					mE();
					DebugLocation(889, 20);
					mR();
					DebugLocation(889, 22);
					mS();
					DebugLocation(889, 24);
					mE();
					DebugLocation(889, 26);
					mC();
					DebugLocation(889, 28);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INTERSECT", 125);
				LeaveRule("INTERSECT", 125);
				Leave_INTERSECT();
			}
		}
		// $ANTLR end "INTERSECT"

		partial void Enter_INTO();
		partial void Leave_INTO();

		// $ANTLR start "INTO"
		[GrammarRule("INTO")]
		private void mINTO()
		{
			Enter_INTO();
			EnterRule("INTO", 126);
			TraceIn("INTO", 126);
			try
			{
				int _type = INTO;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:890:5: ( I N T O )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:890:7: I N T O
				{
					DebugLocation(890, 7);
					mI();
					DebugLocation(890, 9);
					mN();
					DebugLocation(890, 11);
					mT();
					DebugLocation(890, 13);
					mO();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INTO", 126);
				LeaveRule("INTO", 126);
				Leave_INTO();
			}
		}
		// $ANTLR end "INTO"

		partial void Enter_IS();
		partial void Leave_IS();

		// $ANTLR start "IS"
		[GrammarRule("IS")]
		private void mIS()
		{
			Enter_IS();
			EnterRule("IS", 127);
			TraceIn("IS", 127);
			try
			{
				int _type = IS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:891:3: ( I S )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:891:5: I S
				{
					DebugLocation(891, 5);
					mI();
					DebugLocation(891, 7);
					mS();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("IS", 127);
				LeaveRule("IS", 127);
				Leave_IS();
			}
		}
		// $ANTLR end "IS"

		partial void Enter_ISNULL();
		partial void Leave_ISNULL();

		// $ANTLR start "ISNULL"
		[GrammarRule("ISNULL")]
		private void mISNULL()
		{
			Enter_ISNULL();
			EnterRule("ISNULL", 128);
			TraceIn("ISNULL", 128);
			try
			{
				int _type = ISNULL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:892:7: ( I S N U L L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:892:9: I S N U L L
				{
					DebugLocation(892, 9);
					mI();
					DebugLocation(892, 11);
					mS();
					DebugLocation(892, 13);
					mN();
					DebugLocation(892, 15);
					mU();
					DebugLocation(892, 17);
					mL();
					DebugLocation(892, 19);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ISNULL", 128);
				LeaveRule("ISNULL", 128);
				Leave_ISNULL();
			}
		}
		// $ANTLR end "ISNULL"

		partial void Enter_JOIN();
		partial void Leave_JOIN();

		// $ANTLR start "JOIN"
		[GrammarRule("JOIN")]
		private void mJOIN()
		{
			Enter_JOIN();
			EnterRule("JOIN", 129);
			TraceIn("JOIN", 129);
			try
			{
				int _type = JOIN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:893:5: ( J O I N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:893:7: J O I N
				{
					DebugLocation(893, 7);
					mJ();
					DebugLocation(893, 9);
					mO();
					DebugLocation(893, 11);
					mI();
					DebugLocation(893, 13);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("JOIN", 129);
				LeaveRule("JOIN", 129);
				Leave_JOIN();
			}
		}
		// $ANTLR end "JOIN"

		partial void Enter_KEY();
		partial void Leave_KEY();

		// $ANTLR start "KEY"
		[GrammarRule("KEY")]
		private void mKEY()
		{
			Enter_KEY();
			EnterRule("KEY", 130);
			TraceIn("KEY", 130);
			try
			{
				int _type = KEY;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:894:4: ( K E Y )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:894:6: K E Y
				{
					DebugLocation(894, 6);
					mK();
					DebugLocation(894, 8);
					mE();
					DebugLocation(894, 10);
					mY();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("KEY", 130);
				LeaveRule("KEY", 130);
				Leave_KEY();
			}
		}
		// $ANTLR end "KEY"

		partial void Enter_LEFT();
		partial void Leave_LEFT();

		// $ANTLR start "LEFT"
		[GrammarRule("LEFT")]
		private void mLEFT()
		{
			Enter_LEFT();
			EnterRule("LEFT", 131);
			TraceIn("LEFT", 131);
			try
			{
				int _type = LEFT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:895:5: ( L E F T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:895:7: L E F T
				{
					DebugLocation(895, 7);
					mL();
					DebugLocation(895, 9);
					mE();
					DebugLocation(895, 11);
					mF();
					DebugLocation(895, 13);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LEFT", 131);
				LeaveRule("LEFT", 131);
				Leave_LEFT();
			}
		}
		// $ANTLR end "LEFT"

		partial void Enter_LIKE();
		partial void Leave_LIKE();

		// $ANTLR start "LIKE"
		[GrammarRule("LIKE")]
		private void mLIKE()
		{
			Enter_LIKE();
			EnterRule("LIKE", 132);
			TraceIn("LIKE", 132);
			try
			{
				int _type = LIKE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:896:5: ( L I K E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:896:7: L I K E
				{
					DebugLocation(896, 7);
					mL();
					DebugLocation(896, 9);
					mI();
					DebugLocation(896, 11);
					mK();
					DebugLocation(896, 13);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LIKE", 132);
				LeaveRule("LIKE", 132);
				Leave_LIKE();
			}
		}
		// $ANTLR end "LIKE"

		partial void Enter_LIMIT();
		partial void Leave_LIMIT();

		// $ANTLR start "LIMIT"
		[GrammarRule("LIMIT")]
		private void mLIMIT()
		{
			Enter_LIMIT();
			EnterRule("LIMIT", 133);
			TraceIn("LIMIT", 133);
			try
			{
				int _type = LIMIT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:897:6: ( L I M I T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:897:8: L I M I T
				{
					DebugLocation(897, 8);
					mL();
					DebugLocation(897, 10);
					mI();
					DebugLocation(897, 12);
					mM();
					DebugLocation(897, 14);
					mI();
					DebugLocation(897, 16);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("LIMIT", 133);
				LeaveRule("LIMIT", 133);
				Leave_LIMIT();
			}
		}
		// $ANTLR end "LIMIT"

		partial void Enter_MATCH();
		partial void Leave_MATCH();

		// $ANTLR start "MATCH"
		[GrammarRule("MATCH")]
		private void mMATCH()
		{
			Enter_MATCH();
			EnterRule("MATCH", 134);
			TraceIn("MATCH", 134);
			try
			{
				int _type = MATCH;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:898:6: ( M A T C H )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:898:8: M A T C H
				{
					DebugLocation(898, 8);
					mM();
					DebugLocation(898, 10);
					mA();
					DebugLocation(898, 12);
					mT();
					DebugLocation(898, 14);
					mC();
					DebugLocation(898, 16);
					mH();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("MATCH", 134);
				LeaveRule("MATCH", 134);
				Leave_MATCH();
			}
		}
		// $ANTLR end "MATCH"

		partial void Enter_NATURAL();
		partial void Leave_NATURAL();

		// $ANTLR start "NATURAL"
		[GrammarRule("NATURAL")]
		private void mNATURAL()
		{
			Enter_NATURAL();
			EnterRule("NATURAL", 135);
			TraceIn("NATURAL", 135);
			try
			{
				int _type = NATURAL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:899:8: ( N A T U R A L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:899:10: N A T U R A L
				{
					DebugLocation(899, 10);
					mN();
					DebugLocation(899, 12);
					mA();
					DebugLocation(899, 14);
					mT();
					DebugLocation(899, 16);
					mU();
					DebugLocation(899, 18);
					mR();
					DebugLocation(899, 20);
					mA();
					DebugLocation(899, 22);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("NATURAL", 135);
				LeaveRule("NATURAL", 135);
				Leave_NATURAL();
			}
		}
		// $ANTLR end "NATURAL"

		partial void Enter_NOT();
		partial void Leave_NOT();

		// $ANTLR start "NOT"
		[GrammarRule("NOT")]
		private void mNOT()
		{
			Enter_NOT();
			EnterRule("NOT", 136);
			TraceIn("NOT", 136);
			try
			{
				int _type = NOT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:900:4: ( N O T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:900:6: N O T
				{
					DebugLocation(900, 6);
					mN();
					DebugLocation(900, 8);
					mO();
					DebugLocation(900, 10);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("NOT", 136);
				LeaveRule("NOT", 136);
				Leave_NOT();
			}
		}
		// $ANTLR end "NOT"

		partial void Enter_NOTNULL();
		partial void Leave_NOTNULL();

		// $ANTLR start "NOTNULL"
		[GrammarRule("NOTNULL")]
		private void mNOTNULL()
		{
			Enter_NOTNULL();
			EnterRule("NOTNULL", 137);
			TraceIn("NOTNULL", 137);
			try
			{
				int _type = NOTNULL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:901:8: ( N O T N U L L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:901:10: N O T N U L L
				{
					DebugLocation(901, 10);
					mN();
					DebugLocation(901, 12);
					mO();
					DebugLocation(901, 14);
					mT();
					DebugLocation(901, 16);
					mN();
					DebugLocation(901, 18);
					mU();
					DebugLocation(901, 20);
					mL();
					DebugLocation(901, 22);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("NOTNULL", 137);
				LeaveRule("NOTNULL", 137);
				Leave_NOTNULL();
			}
		}
		// $ANTLR end "NOTNULL"

		partial void Enter_NULL();
		partial void Leave_NULL();

		// $ANTLR start "NULL"
		[GrammarRule("NULL")]
		private void mNULL()
		{
			Enter_NULL();
			EnterRule("NULL", 138);
			TraceIn("NULL", 138);
			try
			{
				int _type = NULL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:902:5: ( N U L L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:902:7: N U L L
				{
					DebugLocation(902, 7);
					mN();
					DebugLocation(902, 9);
					mU();
					DebugLocation(902, 11);
					mL();
					DebugLocation(902, 13);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("NULL", 138);
				LeaveRule("NULL", 138);
				Leave_NULL();
			}
		}
		// $ANTLR end "NULL"

		partial void Enter_OF();
		partial void Leave_OF();

		// $ANTLR start "OF"
		[GrammarRule("OF")]
		private void mOF()
		{
			Enter_OF();
			EnterRule("OF", 139);
			TraceIn("OF", 139);
			try
			{
				int _type = OF;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:903:3: ( O F )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:903:5: O F
				{
					DebugLocation(903, 5);
					mO();
					DebugLocation(903, 7);
					mF();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("OF", 139);
				LeaveRule("OF", 139);
				Leave_OF();
			}
		}
		// $ANTLR end "OF"

		partial void Enter_OFFSET();
		partial void Leave_OFFSET();

		// $ANTLR start "OFFSET"
		[GrammarRule("OFFSET")]
		private void mOFFSET()
		{
			Enter_OFFSET();
			EnterRule("OFFSET", 140);
			TraceIn("OFFSET", 140);
			try
			{
				int _type = OFFSET;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:904:7: ( O F F S E T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:904:9: O F F S E T
				{
					DebugLocation(904, 9);
					mO();
					DebugLocation(904, 11);
					mF();
					DebugLocation(904, 13);
					mF();
					DebugLocation(904, 15);
					mS();
					DebugLocation(904, 17);
					mE();
					DebugLocation(904, 19);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("OFFSET", 140);
				LeaveRule("OFFSET", 140);
				Leave_OFFSET();
			}
		}
		// $ANTLR end "OFFSET"

		partial void Enter_ON();
		partial void Leave_ON();

		// $ANTLR start "ON"
		[GrammarRule("ON")]
		private void mON()
		{
			Enter_ON();
			EnterRule("ON", 141);
			TraceIn("ON", 141);
			try
			{
				int _type = ON;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:905:3: ( O N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:905:5: O N
				{
					DebugLocation(905, 5);
					mO();
					DebugLocation(905, 7);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ON", 141);
				LeaveRule("ON", 141);
				Leave_ON();
			}
		}
		// $ANTLR end "ON"

		partial void Enter_OR();
		partial void Leave_OR();

		// $ANTLR start "OR"
		[GrammarRule("OR")]
		private void mOR()
		{
			Enter_OR();
			EnterRule("OR", 142);
			TraceIn("OR", 142);
			try
			{
				int _type = OR;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:906:3: ( O R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:906:5: O R
				{
					DebugLocation(906, 5);
					mO();
					DebugLocation(906, 7);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("OR", 142);
				LeaveRule("OR", 142);
				Leave_OR();
			}
		}
		// $ANTLR end "OR"

		partial void Enter_ORDER();
		partial void Leave_ORDER();

		// $ANTLR start "ORDER"
		[GrammarRule("ORDER")]
		private void mORDER()
		{
			Enter_ORDER();
			EnterRule("ORDER", 143);
			TraceIn("ORDER", 143);
			try
			{
				int _type = ORDER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:907:6: ( O R D E R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:907:8: O R D E R
				{
					DebugLocation(907, 8);
					mO();
					DebugLocation(907, 10);
					mR();
					DebugLocation(907, 12);
					mD();
					DebugLocation(907, 14);
					mE();
					DebugLocation(907, 16);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ORDER", 143);
				LeaveRule("ORDER", 143);
				Leave_ORDER();
			}
		}
		// $ANTLR end "ORDER"

		partial void Enter_OUTER();
		partial void Leave_OUTER();

		// $ANTLR start "OUTER"
		[GrammarRule("OUTER")]
		private void mOUTER()
		{
			Enter_OUTER();
			EnterRule("OUTER", 144);
			TraceIn("OUTER", 144);
			try
			{
				int _type = OUTER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:908:6: ( O U T E R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:908:8: O U T E R
				{
					DebugLocation(908, 8);
					mO();
					DebugLocation(908, 10);
					mU();
					DebugLocation(908, 12);
					mT();
					DebugLocation(908, 14);
					mE();
					DebugLocation(908, 16);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("OUTER", 144);
				LeaveRule("OUTER", 144);
				Leave_OUTER();
			}
		}
		// $ANTLR end "OUTER"

		partial void Enter_PLAN();
		partial void Leave_PLAN();

		// $ANTLR start "PLAN"
		[GrammarRule("PLAN")]
		private void mPLAN()
		{
			Enter_PLAN();
			EnterRule("PLAN", 145);
			TraceIn("PLAN", 145);
			try
			{
				int _type = PLAN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:909:5: ( P L A N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:909:7: P L A N
				{
					DebugLocation(909, 7);
					mP();
					DebugLocation(909, 9);
					mL();
					DebugLocation(909, 11);
					mA();
					DebugLocation(909, 13);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("PLAN", 145);
				LeaveRule("PLAN", 145);
				Leave_PLAN();
			}
		}
		// $ANTLR end "PLAN"

		partial void Enter_PRAGMA();
		partial void Leave_PRAGMA();

		// $ANTLR start "PRAGMA"
		[GrammarRule("PRAGMA")]
		private void mPRAGMA()
		{
			Enter_PRAGMA();
			EnterRule("PRAGMA", 146);
			TraceIn("PRAGMA", 146);
			try
			{
				int _type = PRAGMA;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:910:7: ( P R A G M A )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:910:9: P R A G M A
				{
					DebugLocation(910, 9);
					mP();
					DebugLocation(910, 11);
					mR();
					DebugLocation(910, 13);
					mA();
					DebugLocation(910, 15);
					mG();
					DebugLocation(910, 17);
					mM();
					DebugLocation(910, 19);
					mA();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("PRAGMA", 146);
				LeaveRule("PRAGMA", 146);
				Leave_PRAGMA();
			}
		}
		// $ANTLR end "PRAGMA"

		partial void Enter_PRIMARY();
		partial void Leave_PRIMARY();

		// $ANTLR start "PRIMARY"
		[GrammarRule("PRIMARY")]
		private void mPRIMARY()
		{
			Enter_PRIMARY();
			EnterRule("PRIMARY", 147);
			TraceIn("PRIMARY", 147);
			try
			{
				int _type = PRIMARY;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:911:8: ( P R I M A R Y )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:911:10: P R I M A R Y
				{
					DebugLocation(911, 10);
					mP();
					DebugLocation(911, 12);
					mR();
					DebugLocation(911, 14);
					mI();
					DebugLocation(911, 16);
					mM();
					DebugLocation(911, 18);
					mA();
					DebugLocation(911, 20);
					mR();
					DebugLocation(911, 22);
					mY();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("PRIMARY", 147);
				LeaveRule("PRIMARY", 147);
				Leave_PRIMARY();
			}
		}
		// $ANTLR end "PRIMARY"

		partial void Enter_QUERY();
		partial void Leave_QUERY();

		// $ANTLR start "QUERY"
		[GrammarRule("QUERY")]
		private void mQUERY()
		{
			Enter_QUERY();
			EnterRule("QUERY", 148);
			TraceIn("QUERY", 148);
			try
			{
				int _type = QUERY;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:912:6: ( Q U E R Y )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:912:8: Q U E R Y
				{
					DebugLocation(912, 8);
					mQ();
					DebugLocation(912, 10);
					mU();
					DebugLocation(912, 12);
					mE();
					DebugLocation(912, 14);
					mR();
					DebugLocation(912, 16);
					mY();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("QUERY", 148);
				LeaveRule("QUERY", 148);
				Leave_QUERY();
			}
		}
		// $ANTLR end "QUERY"

		partial void Enter_RAISE();
		partial void Leave_RAISE();

		// $ANTLR start "RAISE"
		[GrammarRule("RAISE")]
		private void mRAISE()
		{
			Enter_RAISE();
			EnterRule("RAISE", 149);
			TraceIn("RAISE", 149);
			try
			{
				int _type = RAISE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:913:6: ( R A I S E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:913:8: R A I S E
				{
					DebugLocation(913, 8);
					mR();
					DebugLocation(913, 10);
					mA();
					DebugLocation(913, 12);
					mI();
					DebugLocation(913, 14);
					mS();
					DebugLocation(913, 16);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("RAISE", 149);
				LeaveRule("RAISE", 149);
				Leave_RAISE();
			}
		}
		// $ANTLR end "RAISE"

		partial void Enter_REFERENCES();
		partial void Leave_REFERENCES();

		// $ANTLR start "REFERENCES"
		[GrammarRule("REFERENCES")]
		private void mREFERENCES()
		{
			Enter_REFERENCES();
			EnterRule("REFERENCES", 150);
			TraceIn("REFERENCES", 150);
			try
			{
				int _type = REFERENCES;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:914:11: ( R E F E R E N C E S )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:914:13: R E F E R E N C E S
				{
					DebugLocation(914, 13);
					mR();
					DebugLocation(914, 15);
					mE();
					DebugLocation(914, 17);
					mF();
					DebugLocation(914, 19);
					mE();
					DebugLocation(914, 21);
					mR();
					DebugLocation(914, 23);
					mE();
					DebugLocation(914, 25);
					mN();
					DebugLocation(914, 27);
					mC();
					DebugLocation(914, 29);
					mE();
					DebugLocation(914, 31);
					mS();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("REFERENCES", 150);
				LeaveRule("REFERENCES", 150);
				Leave_REFERENCES();
			}
		}
		// $ANTLR end "REFERENCES"

		partial void Enter_REGEXP();
		partial void Leave_REGEXP();

		// $ANTLR start "REGEXP"
		[GrammarRule("REGEXP")]
		private void mREGEXP()
		{
			Enter_REGEXP();
			EnterRule("REGEXP", 151);
			TraceIn("REGEXP", 151);
			try
			{
				int _type = REGEXP;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:915:7: ( R E G E X P )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:915:9: R E G E X P
				{
					DebugLocation(915, 9);
					mR();
					DebugLocation(915, 11);
					mE();
					DebugLocation(915, 13);
					mG();
					DebugLocation(915, 15);
					mE();
					DebugLocation(915, 17);
					mX();
					DebugLocation(915, 19);
					mP();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("REGEXP", 151);
				LeaveRule("REGEXP", 151);
				Leave_REGEXP();
			}
		}
		// $ANTLR end "REGEXP"

		partial void Enter_REINDEX();
		partial void Leave_REINDEX();

		// $ANTLR start "REINDEX"
		[GrammarRule("REINDEX")]
		private void mREINDEX()
		{
			Enter_REINDEX();
			EnterRule("REINDEX", 152);
			TraceIn("REINDEX", 152);
			try
			{
				int _type = REINDEX;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:916:8: ( R E I N D E X )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:916:10: R E I N D E X
				{
					DebugLocation(916, 10);
					mR();
					DebugLocation(916, 12);
					mE();
					DebugLocation(916, 14);
					mI();
					DebugLocation(916, 16);
					mN();
					DebugLocation(916, 18);
					mD();
					DebugLocation(916, 20);
					mE();
					DebugLocation(916, 22);
					mX();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("REINDEX", 152);
				LeaveRule("REINDEX", 152);
				Leave_REINDEX();
			}
		}
		// $ANTLR end "REINDEX"

		partial void Enter_RELEASE();
		partial void Leave_RELEASE();

		// $ANTLR start "RELEASE"
		[GrammarRule("RELEASE")]
		private void mRELEASE()
		{
			Enter_RELEASE();
			EnterRule("RELEASE", 153);
			TraceIn("RELEASE", 153);
			try
			{
				int _type = RELEASE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:917:8: ( R E L E A S E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:917:10: R E L E A S E
				{
					DebugLocation(917, 10);
					mR();
					DebugLocation(917, 12);
					mE();
					DebugLocation(917, 14);
					mL();
					DebugLocation(917, 16);
					mE();
					DebugLocation(917, 18);
					mA();
					DebugLocation(917, 20);
					mS();
					DebugLocation(917, 22);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("RELEASE", 153);
				LeaveRule("RELEASE", 153);
				Leave_RELEASE();
			}
		}
		// $ANTLR end "RELEASE"

		partial void Enter_RENAME();
		partial void Leave_RENAME();

		// $ANTLR start "RENAME"
		[GrammarRule("RENAME")]
		private void mRENAME()
		{
			Enter_RENAME();
			EnterRule("RENAME", 154);
			TraceIn("RENAME", 154);
			try
			{
				int _type = RENAME;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:918:7: ( R E N A M E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:918:9: R E N A M E
				{
					DebugLocation(918, 9);
					mR();
					DebugLocation(918, 11);
					mE();
					DebugLocation(918, 13);
					mN();
					DebugLocation(918, 15);
					mA();
					DebugLocation(918, 17);
					mM();
					DebugLocation(918, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("RENAME", 154);
				LeaveRule("RENAME", 154);
				Leave_RENAME();
			}
		}
		// $ANTLR end "RENAME"

		partial void Enter_REPLACE();
		partial void Leave_REPLACE();

		// $ANTLR start "REPLACE"
		[GrammarRule("REPLACE")]
		private void mREPLACE()
		{
			Enter_REPLACE();
			EnterRule("REPLACE", 155);
			TraceIn("REPLACE", 155);
			try
			{
				int _type = REPLACE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:919:8: ( R E P L A C E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:919:10: R E P L A C E
				{
					DebugLocation(919, 10);
					mR();
					DebugLocation(919, 12);
					mE();
					DebugLocation(919, 14);
					mP();
					DebugLocation(919, 16);
					mL();
					DebugLocation(919, 18);
					mA();
					DebugLocation(919, 20);
					mC();
					DebugLocation(919, 22);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("REPLACE", 155);
				LeaveRule("REPLACE", 155);
				Leave_REPLACE();
			}
		}
		// $ANTLR end "REPLACE"

		partial void Enter_RESTRICT();
		partial void Leave_RESTRICT();

		// $ANTLR start "RESTRICT"
		[GrammarRule("RESTRICT")]
		private void mRESTRICT()
		{
			Enter_RESTRICT();
			EnterRule("RESTRICT", 156);
			TraceIn("RESTRICT", 156);
			try
			{
				int _type = RESTRICT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:920:9: ( R E S T R I C T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:920:11: R E S T R I C T
				{
					DebugLocation(920, 11);
					mR();
					DebugLocation(920, 13);
					mE();
					DebugLocation(920, 15);
					mS();
					DebugLocation(920, 17);
					mT();
					DebugLocation(920, 19);
					mR();
					DebugLocation(920, 21);
					mI();
					DebugLocation(920, 23);
					mC();
					DebugLocation(920, 25);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("RESTRICT", 156);
				LeaveRule("RESTRICT", 156);
				Leave_RESTRICT();
			}
		}
		// $ANTLR end "RESTRICT"

		partial void Enter_ROLLBACK();
		partial void Leave_ROLLBACK();

		// $ANTLR start "ROLLBACK"
		[GrammarRule("ROLLBACK")]
		private void mROLLBACK()
		{
			Enter_ROLLBACK();
			EnterRule("ROLLBACK", 157);
			TraceIn("ROLLBACK", 157);
			try
			{
				int _type = ROLLBACK;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:921:9: ( R O L L B A C K )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:921:11: R O L L B A C K
				{
					DebugLocation(921, 11);
					mR();
					DebugLocation(921, 13);
					mO();
					DebugLocation(921, 15);
					mL();
					DebugLocation(921, 17);
					mL();
					DebugLocation(921, 19);
					mB();
					DebugLocation(921, 21);
					mA();
					DebugLocation(921, 23);
					mC();
					DebugLocation(921, 25);
					mK();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ROLLBACK", 157);
				LeaveRule("ROLLBACK", 157);
				Leave_ROLLBACK();
			}
		}
		// $ANTLR end "ROLLBACK"

		partial void Enter_ROW();
		partial void Leave_ROW();

		// $ANTLR start "ROW"
		[GrammarRule("ROW")]
		private void mROW()
		{
			Enter_ROW();
			EnterRule("ROW", 158);
			TraceIn("ROW", 158);
			try
			{
				int _type = ROW;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:922:4: ( R O W )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:922:6: R O W
				{
					DebugLocation(922, 6);
					mR();
					DebugLocation(922, 8);
					mO();
					DebugLocation(922, 10);
					mW();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ROW", 158);
				LeaveRule("ROW", 158);
				Leave_ROW();
			}
		}
		// $ANTLR end "ROW"

		partial void Enter_SAVEPOINT();
		partial void Leave_SAVEPOINT();

		// $ANTLR start "SAVEPOINT"
		[GrammarRule("SAVEPOINT")]
		private void mSAVEPOINT()
		{
			Enter_SAVEPOINT();
			EnterRule("SAVEPOINT", 159);
			TraceIn("SAVEPOINT", 159);
			try
			{
				int _type = SAVEPOINT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:923:10: ( S A V E P O I N T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:923:12: S A V E P O I N T
				{
					DebugLocation(923, 12);
					mS();
					DebugLocation(923, 14);
					mA();
					DebugLocation(923, 16);
					mV();
					DebugLocation(923, 18);
					mE();
					DebugLocation(923, 20);
					mP();
					DebugLocation(923, 22);
					mO();
					DebugLocation(923, 24);
					mI();
					DebugLocation(923, 26);
					mN();
					DebugLocation(923, 28);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SAVEPOINT", 159);
				LeaveRule("SAVEPOINT", 159);
				Leave_SAVEPOINT();
			}
		}
		// $ANTLR end "SAVEPOINT"

		partial void Enter_SELECT();
		partial void Leave_SELECT();

		// $ANTLR start "SELECT"
		[GrammarRule("SELECT")]
		private void mSELECT()
		{
			Enter_SELECT();
			EnterRule("SELECT", 160);
			TraceIn("SELECT", 160);
			try
			{
				int _type = SELECT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:924:7: ( S E L E C T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:924:9: S E L E C T
				{
					DebugLocation(924, 9);
					mS();
					DebugLocation(924, 11);
					mE();
					DebugLocation(924, 13);
					mL();
					DebugLocation(924, 15);
					mE();
					DebugLocation(924, 17);
					mC();
					DebugLocation(924, 19);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SELECT", 160);
				LeaveRule("SELECT", 160);
				Leave_SELECT();
			}
		}
		// $ANTLR end "SELECT"

		partial void Enter_SET();
		partial void Leave_SET();

		// $ANTLR start "SET"
		[GrammarRule("SET")]
		private void mSET()
		{
			Enter_SET();
			EnterRule("SET", 161);
			TraceIn("SET", 161);
			try
			{
				int _type = SET;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:925:4: ( S E T )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:925:6: S E T
				{
					DebugLocation(925, 6);
					mS();
					DebugLocation(925, 8);
					mE();
					DebugLocation(925, 10);
					mT();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("SET", 161);
				LeaveRule("SET", 161);
				Leave_SET();
			}
		}
		// $ANTLR end "SET"

		partial void Enter_TABLE();
		partial void Leave_TABLE();

		// $ANTLR start "TABLE"
		[GrammarRule("TABLE")]
		private void mTABLE()
		{
			Enter_TABLE();
			EnterRule("TABLE", 162);
			TraceIn("TABLE", 162);
			try
			{
				int _type = TABLE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:926:6: ( T A B L E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:926:8: T A B L E
				{
					DebugLocation(926, 8);
					mT();
					DebugLocation(926, 10);
					mA();
					DebugLocation(926, 12);
					mB();
					DebugLocation(926, 14);
					mL();
					DebugLocation(926, 16);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("TABLE", 162);
				LeaveRule("TABLE", 162);
				Leave_TABLE();
			}
		}
		// $ANTLR end "TABLE"

		partial void Enter_TEMPORARY();
		partial void Leave_TEMPORARY();

		// $ANTLR start "TEMPORARY"
		[GrammarRule("TEMPORARY")]
		private void mTEMPORARY()
		{
			Enter_TEMPORARY();
			EnterRule("TEMPORARY", 163);
			TraceIn("TEMPORARY", 163);
			try
			{
				int _type = TEMPORARY;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:927:10: ( T E M P ( O R A R Y )? )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:927:12: T E M P ( O R A R Y )?
				{
					DebugLocation(927, 12);
					mT();
					DebugLocation(927, 14);
					mE();
					DebugLocation(927, 16);
					mM();
					DebugLocation(927, 18);
					mP();
					DebugLocation(927, 20);
					// C:\\Users\\Gareth\\Desktop\\test.g:927:20: ( O R A R Y )?
					int alt1 = 2;
					try
					{
						DebugEnterSubRule(1);
						try
						{
							DebugEnterDecision(1, decisionCanBacktrack[1]);
							int LA1_0 = input.LA(1);

							if ((LA1_0 == 'O' || LA1_0 == 'o'))
							{
								alt1 = 1;
							}
						}
						finally { DebugExitDecision(1); }
						switch (alt1)
						{
							case 1:
								DebugEnterAlt(1);
								// C:\\Users\\Gareth\\Desktop\\test.g:927:22: O R A R Y
								{
									DebugLocation(927, 22);
									mO();
									DebugLocation(927, 24);
									mR();
									DebugLocation(927, 26);
									mA();
									DebugLocation(927, 28);
									mR();
									DebugLocation(927, 30);
									mY();

								}
								break;

						}
					}
					finally { DebugExitSubRule(1); }


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("TEMPORARY", 163);
				LeaveRule("TEMPORARY", 163);
				Leave_TEMPORARY();
			}
		}
		// $ANTLR end "TEMPORARY"

		partial void Enter_THEN();
		partial void Leave_THEN();

		// $ANTLR start "THEN"
		[GrammarRule("THEN")]
		private void mTHEN()
		{
			Enter_THEN();
			EnterRule("THEN", 164);
			TraceIn("THEN", 164);
			try
			{
				int _type = THEN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:928:5: ( T H E N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:928:7: T H E N
				{
					DebugLocation(928, 7);
					mT();
					DebugLocation(928, 9);
					mH();
					DebugLocation(928, 11);
					mE();
					DebugLocation(928, 13);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("THEN", 164);
				LeaveRule("THEN", 164);
				Leave_THEN();
			}
		}
		// $ANTLR end "THEN"

		partial void Enter_TO();
		partial void Leave_TO();

		// $ANTLR start "TO"
		[GrammarRule("TO")]
		private void mTO()
		{
			Enter_TO();
			EnterRule("TO", 165);
			TraceIn("TO", 165);
			try
			{
				int _type = TO;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:929:3: ( T O )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:929:5: T O
				{
					DebugLocation(929, 5);
					mT();
					DebugLocation(929, 7);
					mO();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("TO", 165);
				LeaveRule("TO", 165);
				Leave_TO();
			}
		}
		// $ANTLR end "TO"

		partial void Enter_TRANSACTION();
		partial void Leave_TRANSACTION();

		// $ANTLR start "TRANSACTION"
		[GrammarRule("TRANSACTION")]
		private void mTRANSACTION()
		{
			Enter_TRANSACTION();
			EnterRule("TRANSACTION", 166);
			TraceIn("TRANSACTION", 166);
			try
			{
				int _type = TRANSACTION;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:930:12: ( T R A N S A C T I O N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:930:14: T R A N S A C T I O N
				{
					DebugLocation(930, 14);
					mT();
					DebugLocation(930, 16);
					mR();
					DebugLocation(930, 18);
					mA();
					DebugLocation(930, 20);
					mN();
					DebugLocation(930, 22);
					mS();
					DebugLocation(930, 24);
					mA();
					DebugLocation(930, 26);
					mC();
					DebugLocation(930, 28);
					mT();
					DebugLocation(930, 30);
					mI();
					DebugLocation(930, 32);
					mO();
					DebugLocation(930, 34);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("TRANSACTION", 166);
				LeaveRule("TRANSACTION", 166);
				Leave_TRANSACTION();
			}
		}
		// $ANTLR end "TRANSACTION"

		partial void Enter_TRIGGER();
		partial void Leave_TRIGGER();

		// $ANTLR start "TRIGGER"
		[GrammarRule("TRIGGER")]
		private void mTRIGGER()
		{
			Enter_TRIGGER();
			EnterRule("TRIGGER", 167);
			TraceIn("TRIGGER", 167);
			try
			{
				int _type = TRIGGER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:931:8: ( T R I G G E R )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:931:10: T R I G G E R
				{
					DebugLocation(931, 10);
					mT();
					DebugLocation(931, 12);
					mR();
					DebugLocation(931, 14);
					mI();
					DebugLocation(931, 16);
					mG();
					DebugLocation(931, 18);
					mG();
					DebugLocation(931, 20);
					mE();
					DebugLocation(931, 22);
					mR();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("TRIGGER", 167);
				LeaveRule("TRIGGER", 167);
				Leave_TRIGGER();
			}
		}
		// $ANTLR end "TRIGGER"

		partial void Enter_UNION();
		partial void Leave_UNION();

		// $ANTLR start "UNION"
		[GrammarRule("UNION")]
		private void mUNION()
		{
			Enter_UNION();
			EnterRule("UNION", 168);
			TraceIn("UNION", 168);
			try
			{
				int _type = UNION;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:932:6: ( U N I O N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:932:8: U N I O N
				{
					DebugLocation(932, 8);
					mU();
					DebugLocation(932, 10);
					mN();
					DebugLocation(932, 12);
					mI();
					DebugLocation(932, 14);
					mO();
					DebugLocation(932, 16);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("UNION", 168);
				LeaveRule("UNION", 168);
				Leave_UNION();
			}
		}
		// $ANTLR end "UNION"

		partial void Enter_UNIQUE();
		partial void Leave_UNIQUE();

		// $ANTLR start "UNIQUE"
		[GrammarRule("UNIQUE")]
		private void mUNIQUE()
		{
			Enter_UNIQUE();
			EnterRule("UNIQUE", 169);
			TraceIn("UNIQUE", 169);
			try
			{
				int _type = UNIQUE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:933:7: ( U N I Q U E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:933:9: U N I Q U E
				{
					DebugLocation(933, 9);
					mU();
					DebugLocation(933, 11);
					mN();
					DebugLocation(933, 13);
					mI();
					DebugLocation(933, 15);
					mQ();
					DebugLocation(933, 17);
					mU();
					DebugLocation(933, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("UNIQUE", 169);
				LeaveRule("UNIQUE", 169);
				Leave_UNIQUE();
			}
		}
		// $ANTLR end "UNIQUE"

		partial void Enter_UPDATE();
		partial void Leave_UPDATE();

		// $ANTLR start "UPDATE"
		[GrammarRule("UPDATE")]
		private void mUPDATE()
		{
			Enter_UPDATE();
			EnterRule("UPDATE", 170);
			TraceIn("UPDATE", 170);
			try
			{
				int _type = UPDATE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:934:7: ( U P D A T E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:934:9: U P D A T E
				{
					DebugLocation(934, 9);
					mU();
					DebugLocation(934, 11);
					mP();
					DebugLocation(934, 13);
					mD();
					DebugLocation(934, 15);
					mA();
					DebugLocation(934, 17);
					mT();
					DebugLocation(934, 19);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("UPDATE", 170);
				LeaveRule("UPDATE", 170);
				Leave_UPDATE();
			}
		}
		// $ANTLR end "UPDATE"

		partial void Enter_USING();
		partial void Leave_USING();

		// $ANTLR start "USING"
		[GrammarRule("USING")]
		private void mUSING()
		{
			Enter_USING();
			EnterRule("USING", 171);
			TraceIn("USING", 171);
			try
			{
				int _type = USING;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:935:6: ( U S I N G )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:935:8: U S I N G
				{
					DebugLocation(935, 8);
					mU();
					DebugLocation(935, 10);
					mS();
					DebugLocation(935, 12);
					mI();
					DebugLocation(935, 14);
					mN();
					DebugLocation(935, 16);
					mG();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("USING", 171);
				LeaveRule("USING", 171);
				Leave_USING();
			}
		}
		// $ANTLR end "USING"

		partial void Enter_VACUUM();
		partial void Leave_VACUUM();

		// $ANTLR start "VACUUM"
		[GrammarRule("VACUUM")]
		private void mVACUUM()
		{
			Enter_VACUUM();
			EnterRule("VACUUM", 172);
			TraceIn("VACUUM", 172);
			try
			{
				int _type = VACUUM;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:936:7: ( V A C U U M )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:936:9: V A C U U M
				{
					DebugLocation(936, 9);
					mV();
					DebugLocation(936, 11);
					mA();
					DebugLocation(936, 13);
					mC();
					DebugLocation(936, 15);
					mU();
					DebugLocation(936, 17);
					mU();
					DebugLocation(936, 19);
					mM();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("VACUUM", 172);
				LeaveRule("VACUUM", 172);
				Leave_VACUUM();
			}
		}
		// $ANTLR end "VACUUM"

		partial void Enter_VALUES();
		partial void Leave_VALUES();

		// $ANTLR start "VALUES"
		[GrammarRule("VALUES")]
		private void mVALUES()
		{
			Enter_VALUES();
			EnterRule("VALUES", 173);
			TraceIn("VALUES", 173);
			try
			{
				int _type = VALUES;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:937:7: ( V A L U E S )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:937:9: V A L U E S
				{
					DebugLocation(937, 9);
					mV();
					DebugLocation(937, 11);
					mA();
					DebugLocation(937, 13);
					mL();
					DebugLocation(937, 15);
					mU();
					DebugLocation(937, 17);
					mE();
					DebugLocation(937, 19);
					mS();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("VALUES", 173);
				LeaveRule("VALUES", 173);
				Leave_VALUES();
			}
		}
		// $ANTLR end "VALUES"

		partial void Enter_VIEW();
		partial void Leave_VIEW();

		// $ANTLR start "VIEW"
		[GrammarRule("VIEW")]
		private void mVIEW()
		{
			Enter_VIEW();
			EnterRule("VIEW", 174);
			TraceIn("VIEW", 174);
			try
			{
				int _type = VIEW;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:938:5: ( V I E W )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:938:7: V I E W
				{
					DebugLocation(938, 7);
					mV();
					DebugLocation(938, 9);
					mI();
					DebugLocation(938, 11);
					mE();
					DebugLocation(938, 13);
					mW();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("VIEW", 174);
				LeaveRule("VIEW", 174);
				Leave_VIEW();
			}
		}
		// $ANTLR end "VIEW"

		partial void Enter_VIRTUAL();
		partial void Leave_VIRTUAL();

		// $ANTLR start "VIRTUAL"
		[GrammarRule("VIRTUAL")]
		private void mVIRTUAL()
		{
			Enter_VIRTUAL();
			EnterRule("VIRTUAL", 175);
			TraceIn("VIRTUAL", 175);
			try
			{
				int _type = VIRTUAL;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:939:8: ( V I R T U A L )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:939:10: V I R T U A L
				{
					DebugLocation(939, 10);
					mV();
					DebugLocation(939, 12);
					mI();
					DebugLocation(939, 14);
					mR();
					DebugLocation(939, 16);
					mT();
					DebugLocation(939, 18);
					mU();
					DebugLocation(939, 20);
					mA();
					DebugLocation(939, 22);
					mL();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("VIRTUAL", 175);
				LeaveRule("VIRTUAL", 175);
				Leave_VIRTUAL();
			}
		}
		// $ANTLR end "VIRTUAL"

		partial void Enter_WHEN();
		partial void Leave_WHEN();

		// $ANTLR start "WHEN"
		[GrammarRule("WHEN")]
		private void mWHEN()
		{
			Enter_WHEN();
			EnterRule("WHEN", 176);
			TraceIn("WHEN", 176);
			try
			{
				int _type = WHEN;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:940:5: ( W H E N )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:940:7: W H E N
				{
					DebugLocation(940, 7);
					mW();
					DebugLocation(940, 9);
					mH();
					DebugLocation(940, 11);
					mE();
					DebugLocation(940, 13);
					mN();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("WHEN", 176);
				LeaveRule("WHEN", 176);
				Leave_WHEN();
			}
		}
		// $ANTLR end "WHEN"

		partial void Enter_WHERE();
		partial void Leave_WHERE();

		// $ANTLR start "WHERE"
		[GrammarRule("WHERE")]
		private void mWHERE()
		{
			Enter_WHERE();
			EnterRule("WHERE", 177);
			TraceIn("WHERE", 177);
			try
			{
				int _type = WHERE;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:941:6: ( W H E R E )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:941:8: W H E R E
				{
					DebugLocation(941, 8);
					mW();
					DebugLocation(941, 10);
					mH();
					DebugLocation(941, 12);
					mE();
					DebugLocation(941, 14);
					mR();
					DebugLocation(941, 16);
					mE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("WHERE", 177);
				LeaveRule("WHERE", 177);
				Leave_WHERE();
			}
		}
		// $ANTLR end "WHERE"

		partial void Enter_STRING_ESCAPE_SINGLE();
		partial void Leave_STRING_ESCAPE_SINGLE();

		// $ANTLR start "STRING_ESCAPE_SINGLE"
		[GrammarRule("STRING_ESCAPE_SINGLE")]
		private void mSTRING_ESCAPE_SINGLE()
		{
			Enter_STRING_ESCAPE_SINGLE();
			EnterRule("STRING_ESCAPE_SINGLE", 178);
			TraceIn("STRING_ESCAPE_SINGLE", 178);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:943:30: ( ( BACKSLASH QUOTE_SINGLE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:943:32: ( BACKSLASH QUOTE_SINGLE )
				{
					DebugLocation(943, 32);
					// C:\\Users\\Gareth\\Desktop\\test.g:943:32: ( BACKSLASH QUOTE_SINGLE )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:943:33: BACKSLASH QUOTE_SINGLE
					{
						DebugLocation(943, 33);
						mBACKSLASH();
						DebugLocation(943, 43);
						mQUOTE_SINGLE();

					}


				}

			}
			finally
			{
				TraceOut("STRING_ESCAPE_SINGLE", 178);
				LeaveRule("STRING_ESCAPE_SINGLE", 178);
				Leave_STRING_ESCAPE_SINGLE();
			}
		}
		// $ANTLR end "STRING_ESCAPE_SINGLE"

		partial void Enter_STRING_ESCAPE_DOUBLE();
		partial void Leave_STRING_ESCAPE_DOUBLE();

		// $ANTLR start "STRING_ESCAPE_DOUBLE"
		[GrammarRule("STRING_ESCAPE_DOUBLE")]
		private void mSTRING_ESCAPE_DOUBLE()
		{
			Enter_STRING_ESCAPE_DOUBLE();
			EnterRule("STRING_ESCAPE_DOUBLE", 179);
			TraceIn("STRING_ESCAPE_DOUBLE", 179);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:944:30: ( ( BACKSLASH QUOTE_DOUBLE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:944:32: ( BACKSLASH QUOTE_DOUBLE )
				{
					DebugLocation(944, 32);
					// C:\\Users\\Gareth\\Desktop\\test.g:944:32: ( BACKSLASH QUOTE_DOUBLE )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:944:33: BACKSLASH QUOTE_DOUBLE
					{
						DebugLocation(944, 33);
						mBACKSLASH();
						DebugLocation(944, 43);
						mQUOTE_DOUBLE();

					}


				}

			}
			finally
			{
				TraceOut("STRING_ESCAPE_DOUBLE", 179);
				LeaveRule("STRING_ESCAPE_DOUBLE", 179);
				Leave_STRING_ESCAPE_DOUBLE();
			}
		}
		// $ANTLR end "STRING_ESCAPE_DOUBLE"

		partial void Enter_STRING_CORE();
		partial void Leave_STRING_CORE();

		// $ANTLR start "STRING_CORE"
		[GrammarRule("STRING_CORE")]
		private void mSTRING_CORE()
		{
			Enter_STRING_CORE();
			EnterRule("STRING_CORE", 180);
			TraceIn("STRING_CORE", 180);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:945:21: (~ ( QUOTE_SINGLE | QUOTE_DOUBLE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:945:23: ~ ( QUOTE_SINGLE | QUOTE_DOUBLE )
				{
					DebugLocation(945, 23);
					if ((input.LA(1) >= '\u0000' && input.LA(1) <= '!') || (input.LA(1) >= '#' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '\uFFFF'))
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("STRING_CORE", 180);
				LeaveRule("STRING_CORE", 180);
				Leave_STRING_CORE();
			}
		}
		// $ANTLR end "STRING_CORE"

		partial void Enter_STRING_CORE_SINGLE();
		partial void Leave_STRING_CORE_SINGLE();

		// $ANTLR start "STRING_CORE_SINGLE"
		[GrammarRule("STRING_CORE_SINGLE")]
		private void mSTRING_CORE_SINGLE()
		{
			Enter_STRING_CORE_SINGLE();
			EnterRule("STRING_CORE_SINGLE", 181);
			TraceIn("STRING_CORE_SINGLE", 181);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:946:28: ( ( STRING_CORE | QUOTE_DOUBLE | STRING_ESCAPE_SINGLE )* )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:946:30: ( STRING_CORE | QUOTE_DOUBLE | STRING_ESCAPE_SINGLE )*
				{
					DebugLocation(946, 30);
					// C:\\Users\\Gareth\\Desktop\\test.g:946:30: ( STRING_CORE | QUOTE_DOUBLE | STRING_ESCAPE_SINGLE )*
					try
					{
						DebugEnterSubRule(2);
						while (true)
						{
							int alt2 = 4;
							try
							{
								DebugEnterDecision(2, decisionCanBacktrack[2]);
								int LA2_0 = input.LA(1);

								if ((LA2_0 == '\\'))
								{
									int LA2_2 = input.LA(2);

									if ((LA2_2 == '\''))
									{
										alt2 = 3;
									}

									else
									{
										alt2 = 1;
									}

								}
								else if ((LA2_0 == '\"'))
								{
									alt2 = 2;
								}
								else if (((LA2_0 >= '\u0000' && LA2_0 <= '!') || (LA2_0 >= '#' && LA2_0 <= '&') || (LA2_0 >= '(' && LA2_0 <= '[') || (LA2_0 >= ']' && LA2_0 <= '\uFFFF')))
								{
									alt2 = 1;
								}


							}
							finally { DebugExitDecision(2); }
							switch (alt2)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:946:32: STRING_CORE
									{
										DebugLocation(946, 32);
										mSTRING_CORE();

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:946:46: QUOTE_DOUBLE
									{
										DebugLocation(946, 46);
										mQUOTE_DOUBLE();

									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:946:61: STRING_ESCAPE_SINGLE
									{
										DebugLocation(946, 61);
										mSTRING_ESCAPE_SINGLE();

									}
									break;

								default:
									goto loop2;
							}
						}

					loop2:
						;

					}
					finally { DebugExitSubRule(2); }


				}

			}
			finally
			{
				TraceOut("STRING_CORE_SINGLE", 181);
				LeaveRule("STRING_CORE_SINGLE", 181);
				Leave_STRING_CORE_SINGLE();
			}
		}
		// $ANTLR end "STRING_CORE_SINGLE"

		partial void Enter_STRING_CORE_DOUBLE();
		partial void Leave_STRING_CORE_DOUBLE();

		// $ANTLR start "STRING_CORE_DOUBLE"
		[GrammarRule("STRING_CORE_DOUBLE")]
		private void mSTRING_CORE_DOUBLE()
		{
			Enter_STRING_CORE_DOUBLE();
			EnterRule("STRING_CORE_DOUBLE", 182);
			TraceIn("STRING_CORE_DOUBLE", 182);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:947:28: ( ( STRING_CORE | QUOTE_SINGLE | STRING_ESCAPE_DOUBLE )* )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:947:30: ( STRING_CORE | QUOTE_SINGLE | STRING_ESCAPE_DOUBLE )*
				{
					DebugLocation(947, 30);
					// C:\\Users\\Gareth\\Desktop\\test.g:947:30: ( STRING_CORE | QUOTE_SINGLE | STRING_ESCAPE_DOUBLE )*
					try
					{
						DebugEnterSubRule(3);
						while (true)
						{
							int alt3 = 4;
							try
							{
								DebugEnterDecision(3, decisionCanBacktrack[3]);
								int LA3_0 = input.LA(1);

								if ((LA3_0 == '\\'))
								{
									int LA3_2 = input.LA(2);

									if ((LA3_2 == '\"'))
									{
										alt3 = 3;
									}

									else
									{
										alt3 = 1;
									}

								}
								else if ((LA3_0 == '\''))
								{
									alt3 = 2;
								}
								else if (((LA3_0 >= '\u0000' && LA3_0 <= '!') || (LA3_0 >= '#' && LA3_0 <= '&') || (LA3_0 >= '(' && LA3_0 <= '[') || (LA3_0 >= ']' && LA3_0 <= '\uFFFF')))
								{
									alt3 = 1;
								}


							}
							finally { DebugExitDecision(3); }
							switch (alt3)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:947:32: STRING_CORE
									{
										DebugLocation(947, 32);
										mSTRING_CORE();

									}
									break;
								case 2:
									DebugEnterAlt(2);
									// C:\\Users\\Gareth\\Desktop\\test.g:947:46: QUOTE_SINGLE
									{
										DebugLocation(947, 46);
										mQUOTE_SINGLE();

									}
									break;
								case 3:
									DebugEnterAlt(3);
									// C:\\Users\\Gareth\\Desktop\\test.g:947:61: STRING_ESCAPE_DOUBLE
									{
										DebugLocation(947, 61);
										mSTRING_ESCAPE_DOUBLE();

									}
									break;

								default:
									goto loop3;
							}
						}

					loop3:
						;

					}
					finally { DebugExitSubRule(3); }


				}

			}
			finally
			{
				TraceOut("STRING_CORE_DOUBLE", 182);
				LeaveRule("STRING_CORE_DOUBLE", 182);
				Leave_STRING_CORE_DOUBLE();
			}
		}
		// $ANTLR end "STRING_CORE_DOUBLE"

		partial void Enter_STRING_SINGLE();
		partial void Leave_STRING_SINGLE();

		// $ANTLR start "STRING_SINGLE"
		[GrammarRule("STRING_SINGLE")]
		private void mSTRING_SINGLE()
		{
			Enter_STRING_SINGLE();
			EnterRule("STRING_SINGLE", 183);
			TraceIn("STRING_SINGLE", 183);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:948:23: ( ( ( QUOTE_SINGLE )+ STRING_CORE_SINGLE ( QUOTE_SINGLE )+ ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:948:25: ( ( QUOTE_SINGLE )+ STRING_CORE_SINGLE ( QUOTE_SINGLE )+ )
				{
					DebugLocation(948, 25);
					// C:\\Users\\Gareth\\Desktop\\test.g:948:25: ( ( QUOTE_SINGLE )+ STRING_CORE_SINGLE ( QUOTE_SINGLE )+ )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:948:26: ( QUOTE_SINGLE )+ STRING_CORE_SINGLE ( QUOTE_SINGLE )+
					{
						DebugLocation(948, 26);
						// C:\\Users\\Gareth\\Desktop\\test.g:948:26: ( QUOTE_SINGLE )+
						int cnt4 = 0;
						try
						{
							DebugEnterSubRule(4);
							while (true)
							{
								int alt4 = 2;
								try
								{
									DebugEnterDecision(4, decisionCanBacktrack[4]);
									int LA4_0 = input.LA(1);

									if ((LA4_0 == '\''))
									{
										alt4 = 1;
									}


								}
								finally { DebugExitDecision(4); }
								switch (alt4)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:948:26: QUOTE_SINGLE
										{
											DebugLocation(948, 26);
											mQUOTE_SINGLE();

										}
										break;

									default:
										if (cnt4 >= 1)
											goto loop4;

										EarlyExitException eee4 = new EarlyExitException(4, input);
										DebugRecognitionException(eee4);
										throw eee4;
								}
								cnt4++;
							}
						loop4:
							;

						}
						finally { DebugExitSubRule(4); }

						DebugLocation(948, 40);
						mSTRING_CORE_SINGLE();
						DebugLocation(948, 59);
						// C:\\Users\\Gareth\\Desktop\\test.g:948:59: ( QUOTE_SINGLE )+
						int cnt5 = 0;
						try
						{
							DebugEnterSubRule(5);
							while (true)
							{
								int alt5 = 2;
								try
								{
									DebugEnterDecision(5, decisionCanBacktrack[5]);
									int LA5_0 = input.LA(1);

									if ((LA5_0 == '\''))
									{
										alt5 = 1;
									}


								}
								finally { DebugExitDecision(5); }
								switch (alt5)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:948:59: QUOTE_SINGLE
										{
											DebugLocation(948, 59);
											mQUOTE_SINGLE();

										}
										break;

									default:
										if (cnt5 >= 1)
											goto loop5;

										EarlyExitException eee5 = new EarlyExitException(5, input);
										DebugRecognitionException(eee5);
										throw eee5;
								}
								cnt5++;
							}
						loop5:
							;

						}
						finally { DebugExitSubRule(5); }


					}


				}

			}
			finally
			{
				TraceOut("STRING_SINGLE", 183);
				LeaveRule("STRING_SINGLE", 183);
				Leave_STRING_SINGLE();
			}
		}
		// $ANTLR end "STRING_SINGLE"

		partial void Enter_STRING_DOUBLE();
		partial void Leave_STRING_DOUBLE();

		// $ANTLR start "STRING_DOUBLE"
		[GrammarRule("STRING_DOUBLE")]
		private void mSTRING_DOUBLE()
		{
			Enter_STRING_DOUBLE();
			EnterRule("STRING_DOUBLE", 184);
			TraceIn("STRING_DOUBLE", 184);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:949:23: ( ( QUOTE_DOUBLE STRING_CORE_DOUBLE QUOTE_DOUBLE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:949:25: ( QUOTE_DOUBLE STRING_CORE_DOUBLE QUOTE_DOUBLE )
				{
					DebugLocation(949, 25);
					// C:\\Users\\Gareth\\Desktop\\test.g:949:25: ( QUOTE_DOUBLE STRING_CORE_DOUBLE QUOTE_DOUBLE )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:949:26: QUOTE_DOUBLE STRING_CORE_DOUBLE QUOTE_DOUBLE
					{
						DebugLocation(949, 26);
						mQUOTE_DOUBLE();
						DebugLocation(949, 39);
						mSTRING_CORE_DOUBLE();
						DebugLocation(949, 58);
						mQUOTE_DOUBLE();

					}


				}

			}
			finally
			{
				TraceOut("STRING_DOUBLE", 184);
				LeaveRule("STRING_DOUBLE", 184);
				Leave_STRING_DOUBLE();
			}
		}
		// $ANTLR end "STRING_DOUBLE"

		partial void Enter_STRING();
		partial void Leave_STRING();

		// $ANTLR start "STRING"
		[GrammarRule("STRING")]
		private void mSTRING()
		{
			Enter_STRING();
			EnterRule("STRING", 185);
			TraceIn("STRING", 185);
			try
			{
				int _type = STRING;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:950:7: ( ( STRING_SINGLE | STRING_DOUBLE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:950:9: ( STRING_SINGLE | STRING_DOUBLE )
				{
					DebugLocation(950, 9);
					// C:\\Users\\Gareth\\Desktop\\test.g:950:9: ( STRING_SINGLE | STRING_DOUBLE )
					int alt6 = 2;
					try
					{
						DebugEnterSubRule(6);
						try
						{
							DebugEnterDecision(6, decisionCanBacktrack[6]);
							int LA6_0 = input.LA(1);

							if ((LA6_0 == '\''))
							{
								alt6 = 1;
							}
							else if ((LA6_0 == '\"'))
							{
								alt6 = 2;
							}
							else
							{
								NoViableAltException nvae = new NoViableAltException("", 6, 0, input);

								DebugRecognitionException(nvae);
								throw nvae;
							}
						}
						finally { DebugExitDecision(6); }
						switch (alt6)
						{
							case 1:
								DebugEnterAlt(1);
								// C:\\Users\\Gareth\\Desktop\\test.g:950:10: STRING_SINGLE
								{
									DebugLocation(950, 10);
									mSTRING_SINGLE();

								}
								break;
							case 2:
								DebugEnterAlt(2);
								// C:\\Users\\Gareth\\Desktop\\test.g:950:26: STRING_DOUBLE
								{
									DebugLocation(950, 26);
									mSTRING_DOUBLE();

								}
								break;

						}
					}
					finally { DebugExitSubRule(6); }


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("STRING", 185);
				LeaveRule("STRING", 185);
				Leave_STRING();
			}
		}
		// $ANTLR end "STRING"

		partial void Enter_ID_START();
		partial void Leave_ID_START();

		// $ANTLR start "ID_START"
		[GrammarRule("ID_START")]
		private void mID_START()
		{
			Enter_ID_START();
			EnterRule("ID_START", 186);
			TraceIn("ID_START", 186);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:952:18: ( ( 'a' .. 'z' | 'A' .. 'Z' | UNDERSCORE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:952:20: ( 'a' .. 'z' | 'A' .. 'Z' | UNDERSCORE )
				{
					DebugLocation(952, 20);
					if ((input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z'))
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("ID_START", 186);
				LeaveRule("ID_START", 186);
				Leave_ID_START();
			}
		}
		// $ANTLR end "ID_START"

		partial void Enter_ID_CORE();
		partial void Leave_ID_CORE();

		// $ANTLR start "ID_CORE"
		[GrammarRule("ID_CORE")]
		private void mID_CORE()
		{
			Enter_ID_CORE();
			EnterRule("ID_CORE", 187);
			TraceIn("ID_CORE", 187);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:953:17: ( ( ID_START | '0' .. '9' | DOLLAR ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:953:19: ( ID_START | '0' .. '9' | DOLLAR )
				{
					DebugLocation(953, 19);
					if (input.LA(1) == '$' || (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z'))
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("ID_CORE", 187);
				LeaveRule("ID_CORE", 187);
				Leave_ID_CORE();
			}
		}
		// $ANTLR end "ID_CORE"

		partial void Enter_ID_PLAIN();
		partial void Leave_ID_PLAIN();

		// $ANTLR start "ID_PLAIN"
		[GrammarRule("ID_PLAIN")]
		private void mID_PLAIN()
		{
			Enter_ID_PLAIN();
			EnterRule("ID_PLAIN", 188);
			TraceIn("ID_PLAIN", 188);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:954:18: ( ID_START ( ID_CORE )* )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:954:20: ID_START ( ID_CORE )*
				{
					DebugLocation(954, 20);
					mID_START();
					DebugLocation(954, 29);
					// C:\\Users\\Gareth\\Desktop\\test.g:954:29: ( ID_CORE )*
					try
					{
						DebugEnterSubRule(7);
						while (true)
						{
							int alt7 = 2;
							try
							{
								DebugEnterDecision(7, decisionCanBacktrack[7]);
								int LA7_0 = input.LA(1);

								if ((LA7_0 == '$' || (LA7_0 >= '0' && LA7_0 <= '9') || (LA7_0 >= 'A' && LA7_0 <= 'Z') || LA7_0 == '_' || (LA7_0 >= 'a' && LA7_0 <= 'z')))
								{
									alt7 = 1;
								}


							}
							finally { DebugExitDecision(7); }
							switch (alt7)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:954:30: ID_CORE
									{
										DebugLocation(954, 30);
										mID_CORE();

									}
									break;

								default:
									goto loop7;
							}
						}

					loop7:
						;

					}
					finally { DebugExitSubRule(7); }


				}

			}
			finally
			{
				TraceOut("ID_PLAIN", 188);
				LeaveRule("ID_PLAIN", 188);
				Leave_ID_PLAIN();
			}
		}
		// $ANTLR end "ID_PLAIN"

		partial void Enter_ID_QUOTED_CORE();
		partial void Leave_ID_QUOTED_CORE();

		// $ANTLR start "ID_QUOTED_CORE"
		[GrammarRule("ID_QUOTED_CORE")]
		private void mID_QUOTED_CORE()
		{
			Enter_ID_QUOTED_CORE();
			EnterRule("ID_QUOTED_CORE", 189);
			TraceIn("ID_QUOTED_CORE", 189);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:956:24: (~ ( APOSTROPHE | LPAREN_SQUARE | RPAREN_SQUARE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:956:26: ~ ( APOSTROPHE | LPAREN_SQUARE | RPAREN_SQUARE )
				{
					DebugLocation(956, 26);
					if ((input.LA(1) >= '\u0000' && input.LA(1) <= 'Z') || input.LA(1) == '\\' || (input.LA(1) >= '^' && input.LA(1) <= '_') || (input.LA(1) >= 'a' && input.LA(1) <= '\uFFFF'))
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}


				}

			}
			finally
			{
				TraceOut("ID_QUOTED_CORE", 189);
				LeaveRule("ID_QUOTED_CORE", 189);
				Leave_ID_QUOTED_CORE();
			}
		}
		// $ANTLR end "ID_QUOTED_CORE"

		partial void Enter_ID_QUOTED_CORE_SQUARE();
		partial void Leave_ID_QUOTED_CORE_SQUARE();

		// $ANTLR start "ID_QUOTED_CORE_SQUARE"
		[GrammarRule("ID_QUOTED_CORE_SQUARE")]
		private void mID_QUOTED_CORE_SQUARE()
		{
			Enter_ID_QUOTED_CORE_SQUARE();
			EnterRule("ID_QUOTED_CORE_SQUARE", 190);
			TraceIn("ID_QUOTED_CORE_SQUARE", 190);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:957:31: ( ( ID_QUOTED_CORE | APOSTROPHE )* )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:957:33: ( ID_QUOTED_CORE | APOSTROPHE )*
				{
					DebugLocation(957, 33);
					// C:\\Users\\Gareth\\Desktop\\test.g:957:33: ( ID_QUOTED_CORE | APOSTROPHE )*
					try
					{
						DebugEnterSubRule(8);
						while (true)
						{
							int alt8 = 2;
							try
							{
								DebugEnterDecision(8, decisionCanBacktrack[8]);
								int LA8_0 = input.LA(1);

								if (((LA8_0 >= '\u0000' && LA8_0 <= 'Z') || LA8_0 == '\\' || (LA8_0 >= '^' && LA8_0 <= '\uFFFF')))
								{
									alt8 = 1;
								}


							}
							finally { DebugExitDecision(8); }
							switch (alt8)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:
									{
										DebugLocation(957, 33);
										if ((input.LA(1) >= '\u0000' && input.LA(1) <= 'Z') || input.LA(1) == '\\' || (input.LA(1) >= '^' && input.LA(1) <= '\uFFFF'))
										{
											input.Consume();

										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											Recover(mse);
											throw mse;
										}


									}
									break;

								default:
									goto loop8;
							}
						}

					loop8:
						;

					}
					finally { DebugExitSubRule(8); }


				}

			}
			finally
			{
				TraceOut("ID_QUOTED_CORE_SQUARE", 190);
				LeaveRule("ID_QUOTED_CORE_SQUARE", 190);
				Leave_ID_QUOTED_CORE_SQUARE();
			}
		}
		// $ANTLR end "ID_QUOTED_CORE_SQUARE"

		partial void Enter_ID_QUOTED_CORE_APOSTROPHE();
		partial void Leave_ID_QUOTED_CORE_APOSTROPHE();

		// $ANTLR start "ID_QUOTED_CORE_APOSTROPHE"
		[GrammarRule("ID_QUOTED_CORE_APOSTROPHE")]
		private void mID_QUOTED_CORE_APOSTROPHE()
		{
			Enter_ID_QUOTED_CORE_APOSTROPHE();
			EnterRule("ID_QUOTED_CORE_APOSTROPHE", 191);
			TraceIn("ID_QUOTED_CORE_APOSTROPHE", 191);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:958:35: ( ( ID_QUOTED_CORE | LPAREN_SQUARE | RPAREN_SQUARE )* )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:958:37: ( ID_QUOTED_CORE | LPAREN_SQUARE | RPAREN_SQUARE )*
				{
					DebugLocation(958, 37);
					// C:\\Users\\Gareth\\Desktop\\test.g:958:37: ( ID_QUOTED_CORE | LPAREN_SQUARE | RPAREN_SQUARE )*
					try
					{
						DebugEnterSubRule(9);
						while (true)
						{
							int alt9 = 2;
							try
							{
								DebugEnterDecision(9, decisionCanBacktrack[9]);
								int LA9_0 = input.LA(1);

								if (((LA9_0 >= '\u0000' && LA9_0 <= '_') || (LA9_0 >= 'a' && LA9_0 <= '\uFFFF')))
								{
									alt9 = 1;
								}


							}
							finally { DebugExitDecision(9); }
							switch (alt9)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:
									{
										DebugLocation(958, 37);
										if ((input.LA(1) >= '\u0000' && input.LA(1) <= '_') || (input.LA(1) >= 'a' && input.LA(1) <= '\uFFFF'))
										{
											input.Consume();

										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											Recover(mse);
											throw mse;
										}


									}
									break;

								default:
									goto loop9;
							}
						}

					loop9:
						;

					}
					finally { DebugExitSubRule(9); }


				}

			}
			finally
			{
				TraceOut("ID_QUOTED_CORE_APOSTROPHE", 191);
				LeaveRule("ID_QUOTED_CORE_APOSTROPHE", 191);
				Leave_ID_QUOTED_CORE_APOSTROPHE();
			}
		}
		// $ANTLR end "ID_QUOTED_CORE_APOSTROPHE"

		partial void Enter_ID_QUOTED_SQUARE();
		partial void Leave_ID_QUOTED_SQUARE();

		// $ANTLR start "ID_QUOTED_SQUARE"
		[GrammarRule("ID_QUOTED_SQUARE")]
		private void mID_QUOTED_SQUARE()
		{
			Enter_ID_QUOTED_SQUARE();
			EnterRule("ID_QUOTED_SQUARE", 192);
			TraceIn("ID_QUOTED_SQUARE", 192);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:959:26: ( ( LPAREN_SQUARE ID_QUOTED_CORE_SQUARE RPAREN_SQUARE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:959:28: ( LPAREN_SQUARE ID_QUOTED_CORE_SQUARE RPAREN_SQUARE )
				{
					DebugLocation(959, 28);
					// C:\\Users\\Gareth\\Desktop\\test.g:959:28: ( LPAREN_SQUARE ID_QUOTED_CORE_SQUARE RPAREN_SQUARE )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:959:29: LPAREN_SQUARE ID_QUOTED_CORE_SQUARE RPAREN_SQUARE
					{
						DebugLocation(959, 29);
						mLPAREN_SQUARE();
						DebugLocation(959, 43);
						mID_QUOTED_CORE_SQUARE();
						DebugLocation(959, 65);
						mRPAREN_SQUARE();

					}


				}

			}
			finally
			{
				TraceOut("ID_QUOTED_SQUARE", 192);
				LeaveRule("ID_QUOTED_SQUARE", 192);
				Leave_ID_QUOTED_SQUARE();
			}
		}
		// $ANTLR end "ID_QUOTED_SQUARE"

		partial void Enter_ID_QUOTED_APOSTROPHE();
		partial void Leave_ID_QUOTED_APOSTROPHE();

		// $ANTLR start "ID_QUOTED_APOSTROPHE"
		[GrammarRule("ID_QUOTED_APOSTROPHE")]
		private void mID_QUOTED_APOSTROPHE()
		{
			Enter_ID_QUOTED_APOSTROPHE();
			EnterRule("ID_QUOTED_APOSTROPHE", 193);
			TraceIn("ID_QUOTED_APOSTROPHE", 193);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:960:30: ( ( APOSTROPHE ID_QUOTED_CORE_APOSTROPHE APOSTROPHE ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:960:32: ( APOSTROPHE ID_QUOTED_CORE_APOSTROPHE APOSTROPHE )
				{
					DebugLocation(960, 32);
					// C:\\Users\\Gareth\\Desktop\\test.g:960:32: ( APOSTROPHE ID_QUOTED_CORE_APOSTROPHE APOSTROPHE )
					DebugEnterAlt(1);
					// C:\\Users\\Gareth\\Desktop\\test.g:960:33: APOSTROPHE ID_QUOTED_CORE_APOSTROPHE APOSTROPHE
					{
						DebugLocation(960, 33);
						mAPOSTROPHE();
						DebugLocation(960, 44);
						mID_QUOTED_CORE_APOSTROPHE();
						DebugLocation(960, 70);
						mAPOSTROPHE();

					}


				}

			}
			finally
			{
				TraceOut("ID_QUOTED_APOSTROPHE", 193);
				LeaveRule("ID_QUOTED_APOSTROPHE", 193);
				Leave_ID_QUOTED_APOSTROPHE();
			}
		}
		// $ANTLR end "ID_QUOTED_APOSTROPHE"

		partial void Enter_ID_QUOTED();
		partial void Leave_ID_QUOTED();

		// $ANTLR start "ID_QUOTED"
		[GrammarRule("ID_QUOTED")]
		private void mID_QUOTED()
		{
			Enter_ID_QUOTED();
			EnterRule("ID_QUOTED", 194);
			TraceIn("ID_QUOTED", 194);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:961:19: ( ID_QUOTED_SQUARE | ID_QUOTED_APOSTROPHE )
				int alt10 = 2;
				try
				{
					DebugEnterDecision(10, decisionCanBacktrack[10]);
					int LA10_0 = input.LA(1);

					if ((LA10_0 == '['))
					{
						alt10 = 1;
					}
					else if ((LA10_0 == '`'))
					{
						alt10 = 2;
					}
					else
					{
						NoViableAltException nvae = new NoViableAltException("", 10, 0, input);

						DebugRecognitionException(nvae);
						throw nvae;
					}
				}
				finally { DebugExitDecision(10); }
				switch (alt10)
				{
					case 1:
						DebugEnterAlt(1);
						// C:\\Users\\Gareth\\Desktop\\test.g:961:21: ID_QUOTED_SQUARE
						{
							DebugLocation(961, 21);
							mID_QUOTED_SQUARE();

						}
						break;
					case 2:
						DebugEnterAlt(2);
						// C:\\Users\\Gareth\\Desktop\\test.g:961:40: ID_QUOTED_APOSTROPHE
						{
							DebugLocation(961, 40);
							mID_QUOTED_APOSTROPHE();

						}
						break;

				}
			}
			finally
			{
				TraceOut("ID_QUOTED", 194);
				LeaveRule("ID_QUOTED", 194);
				Leave_ID_QUOTED();
			}
		}
		// $ANTLR end "ID_QUOTED"

		partial void Enter_ID();
		partial void Leave_ID();

		// $ANTLR start "ID"
		[GrammarRule("ID")]
		private void mID()
		{
			Enter_ID();
			EnterRule("ID", 195);
			TraceIn("ID", 195);
			try
			{
				int _type = ID;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:963:3: ( ID_PLAIN | ID_QUOTED )
				int alt11 = 2;
				try
				{
					DebugEnterDecision(11, decisionCanBacktrack[11]);
					int LA11_0 = input.LA(1);

					if (((LA11_0 >= 'A' && LA11_0 <= 'Z') || LA11_0 == '_' || (LA11_0 >= 'a' && LA11_0 <= 'z')))
					{
						alt11 = 1;
					}
					else if ((LA11_0 == '[' || LA11_0 == '`'))
					{
						alt11 = 2;
					}
					else
					{
						NoViableAltException nvae = new NoViableAltException("", 11, 0, input);

						DebugRecognitionException(nvae);
						throw nvae;
					}
				}
				finally { DebugExitDecision(11); }
				switch (alt11)
				{
					case 1:
						DebugEnterAlt(1);
						// C:\\Users\\Gareth\\Desktop\\test.g:963:5: ID_PLAIN
						{
							DebugLocation(963, 5);
							mID_PLAIN();

						}
						break;
					case 2:
						DebugEnterAlt(2);
						// C:\\Users\\Gareth\\Desktop\\test.g:963:16: ID_QUOTED
						{
							DebugLocation(963, 16);
							mID_QUOTED();

						}
						break;

				}
				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("ID", 195);
				LeaveRule("ID", 195);
				Leave_ID();
			}
		}
		// $ANTLR end "ID"

		partial void Enter_INTEGER();
		partial void Leave_INTEGER();

		// $ANTLR start "INTEGER"
		[GrammarRule("INTEGER")]
		private void mINTEGER()
		{
			Enter_INTEGER();
			EnterRule("INTEGER", 196);
			TraceIn("INTEGER", 196);
			try
			{
				int _type = INTEGER;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:967:8: ( ( '0' .. '9' )+ )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:967:10: ( '0' .. '9' )+
				{
					DebugLocation(967, 10);
					// C:\\Users\\Gareth\\Desktop\\test.g:967:10: ( '0' .. '9' )+
					int cnt12 = 0;
					try
					{
						DebugEnterSubRule(12);
						while (true)
						{
							int alt12 = 2;
							try
							{
								DebugEnterDecision(12, decisionCanBacktrack[12]);
								int LA12_0 = input.LA(1);

								if (((LA12_0 >= '0' && LA12_0 <= '9')))
								{
									alt12 = 1;
								}


							}
							finally { DebugExitDecision(12); }
							switch (alt12)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:967:11: '0' .. '9'
									{
										DebugLocation(967, 11);
										MatchRange('0', '9');

									}
									break;

								default:
									if (cnt12 >= 1)
										goto loop12;

									EarlyExitException eee12 = new EarlyExitException(12, input);
									DebugRecognitionException(eee12);
									throw eee12;
							}
							cnt12++;
						}
					loop12:
						;

					}
					finally { DebugExitSubRule(12); }


				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("INTEGER", 196);
				LeaveRule("INTEGER", 196);
				Leave_INTEGER();
			}
		}
		// $ANTLR end "INTEGER"

		partial void Enter_FLOAT_EXP();
		partial void Leave_FLOAT_EXP();

		// $ANTLR start "FLOAT_EXP"
		[GrammarRule("FLOAT_EXP")]
		private void mFLOAT_EXP()
		{
			Enter_FLOAT_EXP();
			EnterRule("FLOAT_EXP", 197);
			TraceIn("FLOAT_EXP", 197);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:968:20: ( ( 'e' | 'E' ) ( '+' | '-' )? ( '0' .. '9' )+ )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:968:22: ( 'e' | 'E' ) ( '+' | '-' )? ( '0' .. '9' )+
				{
					DebugLocation(968, 22);
					if (input.LA(1) == 'E' || input.LA(1) == 'e')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}

					DebugLocation(968, 32);
					// C:\\Users\\Gareth\\Desktop\\test.g:968:32: ( '+' | '-' )?
					int alt13 = 2;
					try
					{
						DebugEnterSubRule(13);
						try
						{
							DebugEnterDecision(13, decisionCanBacktrack[13]);
							int LA13_0 = input.LA(1);

							if ((LA13_0 == '+' || LA13_0 == '-'))
							{
								alt13 = 1;
							}
						}
						finally { DebugExitDecision(13); }
						switch (alt13)
						{
							case 1:
								DebugEnterAlt(1);
								// C:\\Users\\Gareth\\Desktop\\test.g:
								{
									DebugLocation(968, 32);
									if (input.LA(1) == '+' || input.LA(1) == '-')
									{
										input.Consume();

									}
									else
									{
										MismatchedSetException mse = new MismatchedSetException(null, input);
										DebugRecognitionException(mse);
										Recover(mse);
										throw mse;
									}


								}
								break;

						}
					}
					finally { DebugExitSubRule(13); }

					DebugLocation(968, 43);
					// C:\\Users\\Gareth\\Desktop\\test.g:968:43: ( '0' .. '9' )+
					int cnt14 = 0;
					try
					{
						DebugEnterSubRule(14);
						while (true)
						{
							int alt14 = 2;
							try
							{
								DebugEnterDecision(14, decisionCanBacktrack[14]);
								int LA14_0 = input.LA(1);

								if (((LA14_0 >= '0' && LA14_0 <= '9')))
								{
									alt14 = 1;
								}


							}
							finally { DebugExitDecision(14); }
							switch (alt14)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:968:44: '0' .. '9'
									{
										DebugLocation(968, 44);
										MatchRange('0', '9');

									}
									break;

								default:
									if (cnt14 >= 1)
										goto loop14;

									EarlyExitException eee14 = new EarlyExitException(14, input);
									DebugRecognitionException(eee14);
									throw eee14;
							}
							cnt14++;
						}
					loop14:
						;

					}
					finally { DebugExitSubRule(14); }


				}

			}
			finally
			{
				TraceOut("FLOAT_EXP", 197);
				LeaveRule("FLOAT_EXP", 197);
				Leave_FLOAT_EXP();
			}
		}
		// $ANTLR end "FLOAT_EXP"

		partial void Enter_FLOAT();
		partial void Leave_FLOAT();

		// $ANTLR start "FLOAT"
		[GrammarRule("FLOAT")]
		private void mFLOAT()
		{
			Enter_FLOAT();
			EnterRule("FLOAT", 198);
			TraceIn("FLOAT", 198);
			try
			{
				int _type = FLOAT;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:970:5: ( ( '0' .. '9' )+ DOT ( '0' .. '9' )* ( FLOAT_EXP )? | DOT ( '0' .. '9' )+ ( FLOAT_EXP )? | ( '0' .. '9' )+ FLOAT_EXP )
				int alt21 = 3;
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
						// C:\\Users\\Gareth\\Desktop\\test.g:970:9: ( '0' .. '9' )+ DOT ( '0' .. '9' )* ( FLOAT_EXP )?
						{
							DebugLocation(970, 9);
							// C:\\Users\\Gareth\\Desktop\\test.g:970:9: ( '0' .. '9' )+
							int cnt15 = 0;
							try
							{
								DebugEnterSubRule(15);
								while (true)
								{
									int alt15 = 2;
									try
									{
										DebugEnterDecision(15, decisionCanBacktrack[15]);
										int LA15_0 = input.LA(1);

										if (((LA15_0 >= '0' && LA15_0 <= '9')))
										{
											alt15 = 1;
										}


									}
									finally { DebugExitDecision(15); }
									switch (alt15)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:970:10: '0' .. '9'
											{
												DebugLocation(970, 10);
												MatchRange('0', '9');

											}
											break;

										default:
											if (cnt15 >= 1)
												goto loop15;

											EarlyExitException eee15 = new EarlyExitException(15, input);
											DebugRecognitionException(eee15);
											throw eee15;
									}
									cnt15++;
								}
							loop15:
								;

							}
							finally { DebugExitSubRule(15); }

							DebugLocation(970, 21);
							mDOT();
							DebugLocation(970, 25);
							// C:\\Users\\Gareth\\Desktop\\test.g:970:25: ( '0' .. '9' )*
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

										if (((LA16_0 >= '0' && LA16_0 <= '9')))
										{
											alt16 = 1;
										}


									}
									finally { DebugExitDecision(16); }
									switch (alt16)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:970:26: '0' .. '9'
											{
												DebugLocation(970, 26);
												MatchRange('0', '9');

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

							DebugLocation(970, 37);
							// C:\\Users\\Gareth\\Desktop\\test.g:970:37: ( FLOAT_EXP )?
							int alt17 = 2;
							try
							{
								DebugEnterSubRule(17);
								try
								{
									DebugEnterDecision(17, decisionCanBacktrack[17]);
									int LA17_0 = input.LA(1);

									if ((LA17_0 == 'E' || LA17_0 == 'e'))
									{
										alt17 = 1;
									}
								}
								finally { DebugExitDecision(17); }
								switch (alt17)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:970:37: FLOAT_EXP
										{
											DebugLocation(970, 37);
											mFLOAT_EXP();

										}
										break;

								}
							}
							finally { DebugExitSubRule(17); }


						}
						break;
					case 2:
						DebugEnterAlt(2);
						// C:\\Users\\Gareth\\Desktop\\test.g:971:9: DOT ( '0' .. '9' )+ ( FLOAT_EXP )?
						{
							DebugLocation(971, 9);
							mDOT();
							DebugLocation(971, 13);
							// C:\\Users\\Gareth\\Desktop\\test.g:971:13: ( '0' .. '9' )+
							int cnt18 = 0;
							try
							{
								DebugEnterSubRule(18);
								while (true)
								{
									int alt18 = 2;
									try
									{
										DebugEnterDecision(18, decisionCanBacktrack[18]);
										int LA18_0 = input.LA(1);

										if (((LA18_0 >= '0' && LA18_0 <= '9')))
										{
											alt18 = 1;
										}


									}
									finally { DebugExitDecision(18); }
									switch (alt18)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:971:14: '0' .. '9'
											{
												DebugLocation(971, 14);
												MatchRange('0', '9');

											}
											break;

										default:
											if (cnt18 >= 1)
												goto loop18;

											EarlyExitException eee18 = new EarlyExitException(18, input);
											DebugRecognitionException(eee18);
											throw eee18;
									}
									cnt18++;
								}
							loop18:
								;

							}
							finally { DebugExitSubRule(18); }

							DebugLocation(971, 25);
							// C:\\Users\\Gareth\\Desktop\\test.g:971:25: ( FLOAT_EXP )?
							int alt19 = 2;
							try
							{
								DebugEnterSubRule(19);
								try
								{
									DebugEnterDecision(19, decisionCanBacktrack[19]);
									int LA19_0 = input.LA(1);

									if ((LA19_0 == 'E' || LA19_0 == 'e'))
									{
										alt19 = 1;
									}
								}
								finally { DebugExitDecision(19); }
								switch (alt19)
								{
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\Gareth\\Desktop\\test.g:971:25: FLOAT_EXP
										{
											DebugLocation(971, 25);
											mFLOAT_EXP();

										}
										break;

								}
							}
							finally { DebugExitSubRule(19); }


						}
						break;
					case 3:
						DebugEnterAlt(3);
						// C:\\Users\\Gareth\\Desktop\\test.g:972:9: ( '0' .. '9' )+ FLOAT_EXP
						{
							DebugLocation(972, 9);
							// C:\\Users\\Gareth\\Desktop\\test.g:972:9: ( '0' .. '9' )+
							int cnt20 = 0;
							try
							{
								DebugEnterSubRule(20);
								while (true)
								{
									int alt20 = 2;
									try
									{
										DebugEnterDecision(20, decisionCanBacktrack[20]);
										int LA20_0 = input.LA(1);

										if (((LA20_0 >= '0' && LA20_0 <= '9')))
										{
											alt20 = 1;
										}


									}
									finally { DebugExitDecision(20); }
									switch (alt20)
									{
										case 1:
											DebugEnterAlt(1);
											// C:\\Users\\Gareth\\Desktop\\test.g:972:10: '0' .. '9'
											{
												DebugLocation(972, 10);
												MatchRange('0', '9');

											}
											break;

										default:
											if (cnt20 >= 1)
												goto loop20;

											EarlyExitException eee20 = new EarlyExitException(20, input);
											DebugRecognitionException(eee20);
											throw eee20;
									}
									cnt20++;
								}
							loop20:
								;

							}
							finally { DebugExitSubRule(20); }

							DebugLocation(972, 21);
							mFLOAT_EXP();

						}
						break;

				}
				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("FLOAT", 198);
				LeaveRule("FLOAT", 198);
				Leave_FLOAT();
			}
		}
		// $ANTLR end "FLOAT"

		partial void Enter_BLOB();
		partial void Leave_BLOB();

		// $ANTLR start "BLOB"
		[GrammarRule("BLOB")]
		private void mBLOB()
		{
			Enter_BLOB();
			EnterRule("BLOB", 199);
			TraceIn("BLOB", 199);
			try
			{
				int _type = BLOB;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:974:5: ( ( 'x' | 'X' ) QUOTE_SINGLE ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+ QUOTE_SINGLE )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:974:7: ( 'x' | 'X' ) QUOTE_SINGLE ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+ QUOTE_SINGLE
				{
					DebugLocation(974, 7);
					if (input.LA(1) == 'X' || input.LA(1) == 'x')
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null, input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;
					}

					DebugLocation(974, 17);
					mQUOTE_SINGLE();
					DebugLocation(974, 30);
					// C:\\Users\\Gareth\\Desktop\\test.g:974:30: ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+
					int cnt22 = 0;
					try
					{
						DebugEnterSubRule(22);
						while (true)
						{
							int alt22 = 2;
							try
							{
								DebugEnterDecision(22, decisionCanBacktrack[22]);
								int LA22_0 = input.LA(1);

								if (((LA22_0 >= '0' && LA22_0 <= '9') || (LA22_0 >= 'A' && LA22_0 <= 'F') || (LA22_0 >= 'a' && LA22_0 <= 'f')))
								{
									alt22 = 1;
								}


							}
							finally { DebugExitDecision(22); }
							switch (alt22)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:
									{
										DebugLocation(974, 30);
										if ((input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'F') || (input.LA(1) >= 'a' && input.LA(1) <= 'f'))
										{
											input.Consume();

										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											Recover(mse);
											throw mse;
										}


									}
									break;

								default:
									if (cnt22 >= 1)
										goto loop22;

									EarlyExitException eee22 = new EarlyExitException(22, input);
									DebugRecognitionException(eee22);
									throw eee22;
							}
							cnt22++;
						}
					loop22:
						;

					}
					finally { DebugExitSubRule(22); }

					DebugLocation(974, 60);
					mQUOTE_SINGLE();

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("BLOB", 199);
				LeaveRule("BLOB", 199);
				Leave_BLOB();
			}
		}
		// $ANTLR end "BLOB"

		partial void Enter_COMMENT();
		partial void Leave_COMMENT();

		// $ANTLR start "COMMENT"
		[GrammarRule("COMMENT")]
		private void mCOMMENT()
		{
			Enter_COMMENT();
			EnterRule("COMMENT", 200);
			TraceIn("COMMENT", 200);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:976:17: ( '/*' ( options {greedy=false; } : . )* '*/' )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:976:19: '/*' ( options {greedy=false; } : . )* '*/'
				{
					DebugLocation(976, 19);
					Match("/*");

					DebugLocation(976, 24);
					// C:\\Users\\Gareth\\Desktop\\test.g:976:24: ( options {greedy=false; } : . )*
					try
					{
						DebugEnterSubRule(23);
						while (true)
						{
							int alt23 = 2;
							try
							{
								DebugEnterDecision(23, decisionCanBacktrack[23]);
								int LA23_0 = input.LA(1);

								if ((LA23_0 == '*'))
								{
									int LA23_1 = input.LA(2);

									if ((LA23_1 == '/'))
									{
										alt23 = 2;
									}
									else if (((LA23_1 >= '\u0000' && LA23_1 <= '.') || (LA23_1 >= '0' && LA23_1 <= '\uFFFF')))
									{
										alt23 = 1;
									}


								}
								else if (((LA23_0 >= '\u0000' && LA23_0 <= ')') || (LA23_0 >= '+' && LA23_0 <= '\uFFFF')))
								{
									alt23 = 1;
								}


							}
							finally { DebugExitDecision(23); }
							switch (alt23)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:976:52: .
									{
										DebugLocation(976, 52);
										MatchAny();

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

					DebugLocation(976, 57);
					Match("*/");


				}

			}
			finally
			{
				TraceOut("COMMENT", 200);
				LeaveRule("COMMENT", 200);
				Leave_COMMENT();
			}
		}
		// $ANTLR end "COMMENT"

		partial void Enter_LINE_COMMENT();
		partial void Leave_LINE_COMMENT();

		// $ANTLR start "LINE_COMMENT"
		[GrammarRule("LINE_COMMENT")]
		private void mLINE_COMMENT()
		{
			Enter_LINE_COMMENT();
			EnterRule("LINE_COMMENT", 201);
			TraceIn("LINE_COMMENT", 201);
			try
			{
				// C:\\Users\\Gareth\\Desktop\\test.g:977:22: ( '--' (~ ( '\\n' | '\\r' ) )* ( ( '\\r' )? '\\n' | EOF ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:977:24: '--' (~ ( '\\n' | '\\r' ) )* ( ( '\\r' )? '\\n' | EOF )
				{
					DebugLocation(977, 24);
					Match("--");

					DebugLocation(977, 29);
					// C:\\Users\\Gareth\\Desktop\\test.g:977:29: (~ ( '\\n' | '\\r' ) )*
					try
					{
						DebugEnterSubRule(24);
						while (true)
						{
							int alt24 = 2;
							try
							{
								DebugEnterDecision(24, decisionCanBacktrack[24]);
								int LA24_0 = input.LA(1);

								if (((LA24_0 >= '\u0000' && LA24_0 <= '\t') || (LA24_0 >= '\u000B' && LA24_0 <= '\f') || (LA24_0 >= '\u000E' && LA24_0 <= '\uFFFF')))
								{
									alt24 = 1;
								}


							}
							finally { DebugExitDecision(24); }
							switch (alt24)
							{
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\Gareth\\Desktop\\test.g:977:29: ~ ( '\\n' | '\\r' )
									{
										DebugLocation(977, 29);
										if ((input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\uFFFF'))
										{
											input.Consume();

										}
										else
										{
											MismatchedSetException mse = new MismatchedSetException(null, input);
											DebugRecognitionException(mse);
											Recover(mse);
											throw mse;
										}


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

					DebugLocation(977, 43);
					// C:\\Users\\Gareth\\Desktop\\test.g:977:43: ( ( '\\r' )? '\\n' | EOF )
					int alt26 = 2;
					try
					{
						DebugEnterSubRule(26);
						try
						{
							DebugEnterDecision(26, decisionCanBacktrack[26]);
							int LA26_0 = input.LA(1);

							if ((LA26_0 == '\n' || LA26_0 == '\r'))
							{
								alt26 = 1;
							}
							else
							{
								alt26 = 2;
							}
						}
						finally { DebugExitDecision(26); }
						switch (alt26)
						{
							case 1:
								DebugEnterAlt(1);
								// C:\\Users\\Gareth\\Desktop\\test.g:977:44: ( '\\r' )? '\\n'
								{
									DebugLocation(977, 44);
									// C:\\Users\\Gareth\\Desktop\\test.g:977:44: ( '\\r' )?
									int alt25 = 2;
									try
									{
										DebugEnterSubRule(25);
										try
										{
											DebugEnterDecision(25, decisionCanBacktrack[25]);
											int LA25_0 = input.LA(1);

											if ((LA25_0 == '\r'))
											{
												alt25 = 1;
											}
										}
										finally { DebugExitDecision(25); }
										switch (alt25)
										{
											case 1:
												DebugEnterAlt(1);
												// C:\\Users\\Gareth\\Desktop\\test.g:977:44: '\\r'
												{
													DebugLocation(977, 44);
													Match('\r');

												}
												break;

										}
									}
									finally { DebugExitSubRule(25); }

									DebugLocation(977, 50);
									Match('\n');

								}
								break;
							case 2:
								DebugEnterAlt(2);
								// C:\\Users\\Gareth\\Desktop\\test.g:977:55: EOF
								{
									DebugLocation(977, 55);
									Match(EOF);

								}
								break;

						}
					}
					finally { DebugExitSubRule(26); }


				}

			}
			finally
			{
				TraceOut("LINE_COMMENT", 201);
				LeaveRule("LINE_COMMENT", 201);
				Leave_LINE_COMMENT();
			}
		}
		// $ANTLR end "LINE_COMMENT"

		partial void Enter_WS();
		partial void Leave_WS();

		// $ANTLR start "WS"
		[GrammarRule("WS")]
		private void mWS()
		{
			Enter_WS();
			EnterRule("WS", 202);
			TraceIn("WS", 202);
			try
			{
				int _type = WS;
				int _channel = DefaultTokenChannel;
				// C:\\Users\\Gareth\\Desktop\\test.g:978:3: ( ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | COMMENT | LINE_COMMENT ) )
				DebugEnterAlt(1);
				// C:\\Users\\Gareth\\Desktop\\test.g:978:5: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | COMMENT | LINE_COMMENT )
				{
					DebugLocation(978, 5);
					// C:\\Users\\Gareth\\Desktop\\test.g:978:5: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | COMMENT | LINE_COMMENT )
					int alt27 = 7;
					try
					{
						DebugEnterSubRule(27);
						try
						{
							DebugEnterDecision(27, decisionCanBacktrack[27]);
							switch (input.LA(1))
							{
								case ' ':
									{
										alt27 = 1;
									}
									break;
								case '\r':
									{
										alt27 = 2;
									}
									break;
								case '\t':
									{
										alt27 = 3;
									}
									break;
								case '\f':
									{
										alt27 = 4;
									}
									break;
								case '\n':
									{
										alt27 = 5;
									}
									break;
								case '/':
									{
										alt27 = 6;
									}
									break;
								case '-':
									{
										alt27 = 7;
									}
									break;
								default:
									{
										NoViableAltException nvae = new NoViableAltException("", 27, 0, input);

										DebugRecognitionException(nvae);
										throw nvae;
									}
							}

						}
						finally { DebugExitDecision(27); }
						switch (alt27)
						{
							case 1:
								DebugEnterAlt(1);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:6: ' '
								{
									DebugLocation(978, 6);
									Match(' ');

								}
								break;
							case 2:
								DebugEnterAlt(2);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:10: '\\r'
								{
									DebugLocation(978, 10);
									Match('\r');

								}
								break;
							case 3:
								DebugEnterAlt(3);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:15: '\\t'
								{
									DebugLocation(978, 15);
									Match('\t');

								}
								break;
							case 4:
								DebugEnterAlt(4);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:20: '\\u000C'
								{
									DebugLocation(978, 20);
									Match('\f');

								}
								break;
							case 5:
								DebugEnterAlt(5);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:29: '\\n'
								{
									DebugLocation(978, 29);
									Match('\n');

								}
								break;
							case 6:
								DebugEnterAlt(6);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:34: COMMENT
								{
									DebugLocation(978, 34);
									mCOMMENT();

								}
								break;
							case 7:
								DebugEnterAlt(7);
								// C:\\Users\\Gareth\\Desktop\\test.g:978:42: LINE_COMMENT
								{
									DebugLocation(978, 42);
									mLINE_COMMENT();

								}
								break;

						}
					}
					finally { DebugExitSubRule(27); }

					DebugLocation(978, 56);
					_channel = Hidden;

				}

				state.type = _type;
				state.channel = _channel;
			}
			finally
			{
				TraceOut("WS", 202);
				LeaveRule("WS", 202);
				Leave_WS();
			}
		}
		// $ANTLR end "WS"

		public override void mTokens()
		{
			// C:\\Users\\Gareth\\Desktop\\test.g:1:8: ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | BACKSLASH | PERCENT | SEMI | DOT | COMMA | LPAREN | RPAREN | QUESTION | COLON | AT | DOLLAR | QUOTE_DOUBLE | QUOTE_SINGLE | APOSTROPHE | LPAREN_SQUARE | RPAREN_SQUARE | UNDERSCORE | ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE | STRING | ID | INTEGER | FLOAT | BLOB | WS )
			int alt28 = 157;
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
					// C:\\Users\\Gareth\\Desktop\\test.g:1:10: EQUALS
					{
						DebugLocation(1, 10);
						mEQUALS();

					}
					break;
				case 2:
					DebugEnterAlt(2);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:17: EQUALS2
					{
						DebugLocation(1, 17);
						mEQUALS2();

					}
					break;
				case 3:
					DebugEnterAlt(3);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:25: NOT_EQUALS
					{
						DebugLocation(1, 25);
						mNOT_EQUALS();

					}
					break;
				case 4:
					DebugEnterAlt(4);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:36: NOT_EQUALS2
					{
						DebugLocation(1, 36);
						mNOT_EQUALS2();

					}
					break;
				case 5:
					DebugEnterAlt(5);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:48: LESS
					{
						DebugLocation(1, 48);
						mLESS();

					}
					break;
				case 6:
					DebugEnterAlt(6);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:53: LESS_OR_EQ
					{
						DebugLocation(1, 53);
						mLESS_OR_EQ();

					}
					break;
				case 7:
					DebugEnterAlt(7);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:64: GREATER
					{
						DebugLocation(1, 64);
						mGREATER();

					}
					break;
				case 8:
					DebugEnterAlt(8);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:72: GREATER_OR_EQ
					{
						DebugLocation(1, 72);
						mGREATER_OR_EQ();

					}
					break;
				case 9:
					DebugEnterAlt(9);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:86: SHIFT_LEFT
					{
						DebugLocation(1, 86);
						mSHIFT_LEFT();

					}
					break;
				case 10:
					DebugEnterAlt(10);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:97: SHIFT_RIGHT
					{
						DebugLocation(1, 97);
						mSHIFT_RIGHT();

					}
					break;
				case 11:
					DebugEnterAlt(11);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:109: AMPERSAND
					{
						DebugLocation(1, 109);
						mAMPERSAND();

					}
					break;
				case 12:
					DebugEnterAlt(12);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:119: PIPE
					{
						DebugLocation(1, 119);
						mPIPE();

					}
					break;
				case 13:
					DebugEnterAlt(13);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:124: DOUBLE_PIPE
					{
						DebugLocation(1, 124);
						mDOUBLE_PIPE();

					}
					break;
				case 14:
					DebugEnterAlt(14);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:136: PLUS
					{
						DebugLocation(1, 136);
						mPLUS();

					}
					break;
				case 15:
					DebugEnterAlt(15);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:141: MINUS
					{
						DebugLocation(1, 141);
						mMINUS();

					}
					break;
				case 16:
					DebugEnterAlt(16);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:147: TILDA
					{
						DebugLocation(1, 147);
						mTILDA();

					}
					break;
				case 17:
					DebugEnterAlt(17);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:153: ASTERISK
					{
						DebugLocation(1, 153);
						mASTERISK();

					}
					break;
				case 18:
					DebugEnterAlt(18);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:162: SLASH
					{
						DebugLocation(1, 162);
						mSLASH();

					}
					break;
				case 19:
					DebugEnterAlt(19);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:168: BACKSLASH
					{
						DebugLocation(1, 168);
						mBACKSLASH();

					}
					break;
				case 20:
					DebugEnterAlt(20);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:178: PERCENT
					{
						DebugLocation(1, 178);
						mPERCENT();

					}
					break;
				case 21:
					DebugEnterAlt(21);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:186: SEMI
					{
						DebugLocation(1, 186);
						mSEMI();

					}
					break;
				case 22:
					DebugEnterAlt(22);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:191: DOT
					{
						DebugLocation(1, 191);
						mDOT();

					}
					break;
				case 23:
					DebugEnterAlt(23);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:195: COMMA
					{
						DebugLocation(1, 195);
						mCOMMA();

					}
					break;
				case 24:
					DebugEnterAlt(24);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:201: LPAREN
					{
						DebugLocation(1, 201);
						mLPAREN();

					}
					break;
				case 25:
					DebugEnterAlt(25);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:208: RPAREN
					{
						DebugLocation(1, 208);
						mRPAREN();

					}
					break;
				case 26:
					DebugEnterAlt(26);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:215: QUESTION
					{
						DebugLocation(1, 215);
						mQUESTION();

					}
					break;
				case 27:
					DebugEnterAlt(27);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:224: COLON
					{
						DebugLocation(1, 224);
						mCOLON();

					}
					break;
				case 28:
					DebugEnterAlt(28);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:230: AT
					{
						DebugLocation(1, 230);
						mAT();

					}
					break;
				case 29:
					DebugEnterAlt(29);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:233: DOLLAR
					{
						DebugLocation(1, 233);
						mDOLLAR();

					}
					break;
				case 30:
					DebugEnterAlt(30);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:240: QUOTE_DOUBLE
					{
						DebugLocation(1, 240);
						mQUOTE_DOUBLE();

					}
					break;
				case 31:
					DebugEnterAlt(31);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:253: QUOTE_SINGLE
					{
						DebugLocation(1, 253);
						mQUOTE_SINGLE();

					}
					break;
				case 32:
					DebugEnterAlt(32);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:266: APOSTROPHE
					{
						DebugLocation(1, 266);
						mAPOSTROPHE();

					}
					break;
				case 33:
					DebugEnterAlt(33);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:277: LPAREN_SQUARE
					{
						DebugLocation(1, 277);
						mLPAREN_SQUARE();

					}
					break;
				case 34:
					DebugEnterAlt(34);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:291: RPAREN_SQUARE
					{
						DebugLocation(1, 291);
						mRPAREN_SQUARE();

					}
					break;
				case 35:
					DebugEnterAlt(35);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:305: UNDERSCORE
					{
						DebugLocation(1, 305);
						mUNDERSCORE();

					}
					break;
				case 36:
					DebugEnterAlt(36);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:316: ABORT
					{
						DebugLocation(1, 316);
						mABORT();

					}
					break;
				case 37:
					DebugEnterAlt(37);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:322: ADD
					{
						DebugLocation(1, 322);
						mADD();

					}
					break;
				case 38:
					DebugEnterAlt(38);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:326: AFTER
					{
						DebugLocation(1, 326);
						mAFTER();

					}
					break;
				case 39:
					DebugEnterAlt(39);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:332: ALL
					{
						DebugLocation(1, 332);
						mALL();

					}
					break;
				case 40:
					DebugEnterAlt(40);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:336: ALTER
					{
						DebugLocation(1, 336);
						mALTER();

					}
					break;
				case 41:
					DebugEnterAlt(41);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:342: ANALYZE
					{
						DebugLocation(1, 342);
						mANALYZE();

					}
					break;
				case 42:
					DebugEnterAlt(42);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:350: AND
					{
						DebugLocation(1, 350);
						mAND();

					}
					break;
				case 43:
					DebugEnterAlt(43);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:354: AS
					{
						DebugLocation(1, 354);
						mAS();

					}
					break;
				case 44:
					DebugEnterAlt(44);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:357: ASC
					{
						DebugLocation(1, 357);
						mASC();

					}
					break;
				case 45:
					DebugEnterAlt(45);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:361: ATTACH
					{
						DebugLocation(1, 361);
						mATTACH();

					}
					break;
				case 46:
					DebugEnterAlt(46);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:368: AUTOINCREMENT
					{
						DebugLocation(1, 368);
						mAUTOINCREMENT();

					}
					break;
				case 47:
					DebugEnterAlt(47);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:382: BEFORE
					{
						DebugLocation(1, 382);
						mBEFORE();

					}
					break;
				case 48:
					DebugEnterAlt(48);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:389: BEGIN
					{
						DebugLocation(1, 389);
						mBEGIN();

					}
					break;
				case 49:
					DebugEnterAlt(49);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:395: BETWEEN
					{
						DebugLocation(1, 395);
						mBETWEEN();

					}
					break;
				case 50:
					DebugEnterAlt(50);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:403: BY
					{
						DebugLocation(1, 403);
						mBY();

					}
					break;
				case 51:
					DebugEnterAlt(51);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:406: CASCADE
					{
						DebugLocation(1, 406);
						mCASCADE();

					}
					break;
				case 52:
					DebugEnterAlt(52);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:414: CASE
					{
						DebugLocation(1, 414);
						mCASE();

					}
					break;
				case 53:
					DebugEnterAlt(53);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:419: CAST
					{
						DebugLocation(1, 419);
						mCAST();

					}
					break;
				case 54:
					DebugEnterAlt(54);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:424: CHECK
					{
						DebugLocation(1, 424);
						mCHECK();

					}
					break;
				case 55:
					DebugEnterAlt(55);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:430: COLLATE
					{
						DebugLocation(1, 430);
						mCOLLATE();

					}
					break;
				case 56:
					DebugEnterAlt(56);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:438: COLUMN
					{
						DebugLocation(1, 438);
						mCOLUMN();

					}
					break;
				case 57:
					DebugEnterAlt(57);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:445: COMMIT
					{
						DebugLocation(1, 445);
						mCOMMIT();

					}
					break;
				case 58:
					DebugEnterAlt(58);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:452: CONFLICT
					{
						DebugLocation(1, 452);
						mCONFLICT();

					}
					break;
				case 59:
					DebugEnterAlt(59);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:461: CONSTRAINT
					{
						DebugLocation(1, 461);
						mCONSTRAINT();

					}
					break;
				case 60:
					DebugEnterAlt(60);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:472: CREATE
					{
						DebugLocation(1, 472);
						mCREATE();

					}
					break;
				case 61:
					DebugEnterAlt(61);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:479: CROSS
					{
						DebugLocation(1, 479);
						mCROSS();

					}
					break;
				case 62:
					DebugEnterAlt(62);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:485: CURRENT_TIME
					{
						DebugLocation(1, 485);
						mCURRENT_TIME();

					}
					break;
				case 63:
					DebugEnterAlt(63);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:498: CURRENT_DATE
					{
						DebugLocation(1, 498);
						mCURRENT_DATE();

					}
					break;
				case 64:
					DebugEnterAlt(64);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:511: CURRENT_TIMESTAMP
					{
						DebugLocation(1, 511);
						mCURRENT_TIMESTAMP();

					}
					break;
				case 65:
					DebugEnterAlt(65);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:529: DATABASE
					{
						DebugLocation(1, 529);
						mDATABASE();

					}
					break;
				case 66:
					DebugEnterAlt(66);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:538: DEFAULT
					{
						DebugLocation(1, 538);
						mDEFAULT();

					}
					break;
				case 67:
					DebugEnterAlt(67);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:546: DEFERRABLE
					{
						DebugLocation(1, 546);
						mDEFERRABLE();

					}
					break;
				case 68:
					DebugEnterAlt(68);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:557: DEFERRED
					{
						DebugLocation(1, 557);
						mDEFERRED();

					}
					break;
				case 69:
					DebugEnterAlt(69);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:566: DELETE
					{
						DebugLocation(1, 566);
						mDELETE();

					}
					break;
				case 70:
					DebugEnterAlt(70);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:573: DESC
					{
						DebugLocation(1, 573);
						mDESC();

					}
					break;
				case 71:
					DebugEnterAlt(71);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:578: DETACH
					{
						DebugLocation(1, 578);
						mDETACH();

					}
					break;
				case 72:
					DebugEnterAlt(72);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:585: DISTINCT
					{
						DebugLocation(1, 585);
						mDISTINCT();

					}
					break;
				case 73:
					DebugEnterAlt(73);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:594: DROP
					{
						DebugLocation(1, 594);
						mDROP();

					}
					break;
				case 74:
					DebugEnterAlt(74);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:599: EACH
					{
						DebugLocation(1, 599);
						mEACH();

					}
					break;
				case 75:
					DebugEnterAlt(75);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:604: ELSE
					{
						DebugLocation(1, 604);
						mELSE();

					}
					break;
				case 76:
					DebugEnterAlt(76);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:609: END
					{
						DebugLocation(1, 609);
						mEND();

					}
					break;
				case 77:
					DebugEnterAlt(77);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:613: ESCAPE
					{
						DebugLocation(1, 613);
						mESCAPE();

					}
					break;
				case 78:
					DebugEnterAlt(78);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:620: EXCEPT
					{
						DebugLocation(1, 620);
						mEXCEPT();

					}
					break;
				case 79:
					DebugEnterAlt(79);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:627: EXCLUSIVE
					{
						DebugLocation(1, 627);
						mEXCLUSIVE();

					}
					break;
				case 80:
					DebugEnterAlt(80);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:637: EXISTS
					{
						DebugLocation(1, 637);
						mEXISTS();

					}
					break;
				case 81:
					DebugEnterAlt(81);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:644: EXPLAIN
					{
						DebugLocation(1, 644);
						mEXPLAIN();

					}
					break;
				case 82:
					DebugEnterAlt(82);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:652: FAIL
					{
						DebugLocation(1, 652);
						mFAIL();

					}
					break;
				case 83:
					DebugEnterAlt(83);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:657: FOR
					{
						DebugLocation(1, 657);
						mFOR();

					}
					break;
				case 84:
					DebugEnterAlt(84);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:661: FOREIGN
					{
						DebugLocation(1, 661);
						mFOREIGN();

					}
					break;
				case 85:
					DebugEnterAlt(85);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:669: FROM
					{
						DebugLocation(1, 669);
						mFROM();

					}
					break;
				case 86:
					DebugEnterAlt(86);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:674: GLOB
					{
						DebugLocation(1, 674);
						mGLOB();

					}
					break;
				case 87:
					DebugEnterAlt(87);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:679: GROUP
					{
						DebugLocation(1, 679);
						mGROUP();

					}
					break;
				case 88:
					DebugEnterAlt(88);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:685: HAVING
					{
						DebugLocation(1, 685);
						mHAVING();

					}
					break;
				case 89:
					DebugEnterAlt(89);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:692: IF
					{
						DebugLocation(1, 692);
						mIF();

					}
					break;
				case 90:
					DebugEnterAlt(90);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:695: IGNORE
					{
						DebugLocation(1, 695);
						mIGNORE();

					}
					break;
				case 91:
					DebugEnterAlt(91);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:702: IMMEDIATE
					{
						DebugLocation(1, 702);
						mIMMEDIATE();

					}
					break;
				case 92:
					DebugEnterAlt(92);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:712: IN
					{
						DebugLocation(1, 712);
						mIN();

					}
					break;
				case 93:
					DebugEnterAlt(93);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:715: INDEX
					{
						DebugLocation(1, 715);
						mINDEX();

					}
					break;
				case 94:
					DebugEnterAlt(94);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:721: INDEXED
					{
						DebugLocation(1, 721);
						mINDEXED();

					}
					break;
				case 95:
					DebugEnterAlt(95);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:729: INITIALLY
					{
						DebugLocation(1, 729);
						mINITIALLY();

					}
					break;
				case 96:
					DebugEnterAlt(96);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:739: INNER
					{
						DebugLocation(1, 739);
						mINNER();

					}
					break;
				case 97:
					DebugEnterAlt(97);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:745: INSERT
					{
						DebugLocation(1, 745);
						mINSERT();

					}
					break;
				case 98:
					DebugEnterAlt(98);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:752: INSTEAD
					{
						DebugLocation(1, 752);
						mINSTEAD();

					}
					break;
				case 99:
					DebugEnterAlt(99);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:760: INTERSECT
					{
						DebugLocation(1, 760);
						mINTERSECT();

					}
					break;
				case 100:
					DebugEnterAlt(100);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:770: INTO
					{
						DebugLocation(1, 770);
						mINTO();

					}
					break;
				case 101:
					DebugEnterAlt(101);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:775: IS
					{
						DebugLocation(1, 775);
						mIS();

					}
					break;
				case 102:
					DebugEnterAlt(102);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:778: ISNULL
					{
						DebugLocation(1, 778);
						mISNULL();

					}
					break;
				case 103:
					DebugEnterAlt(103);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:785: JOIN
					{
						DebugLocation(1, 785);
						mJOIN();

					}
					break;
				case 104:
					DebugEnterAlt(104);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:790: KEY
					{
						DebugLocation(1, 790);
						mKEY();

					}
					break;
				case 105:
					DebugEnterAlt(105);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:794: LEFT
					{
						DebugLocation(1, 794);
						mLEFT();

					}
					break;
				case 106:
					DebugEnterAlt(106);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:799: LIKE
					{
						DebugLocation(1, 799);
						mLIKE();

					}
					break;
				case 107:
					DebugEnterAlt(107);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:804: LIMIT
					{
						DebugLocation(1, 804);
						mLIMIT();

					}
					break;
				case 108:
					DebugEnterAlt(108);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:810: MATCH
					{
						DebugLocation(1, 810);
						mMATCH();

					}
					break;
				case 109:
					DebugEnterAlt(109);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:816: NATURAL
					{
						DebugLocation(1, 816);
						mNATURAL();

					}
					break;
				case 110:
					DebugEnterAlt(110);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:824: NOT
					{
						DebugLocation(1, 824);
						mNOT();

					}
					break;
				case 111:
					DebugEnterAlt(111);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:828: NOTNULL
					{
						DebugLocation(1, 828);
						mNOTNULL();

					}
					break;
				case 112:
					DebugEnterAlt(112);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:836: NULL
					{
						DebugLocation(1, 836);
						mNULL();

					}
					break;
				case 113:
					DebugEnterAlt(113);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:841: OF
					{
						DebugLocation(1, 841);
						mOF();

					}
					break;
				case 114:
					DebugEnterAlt(114);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:844: OFFSET
					{
						DebugLocation(1, 844);
						mOFFSET();

					}
					break;
				case 115:
					DebugEnterAlt(115);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:851: ON
					{
						DebugLocation(1, 851);
						mON();

					}
					break;
				case 116:
					DebugEnterAlt(116);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:854: OR
					{
						DebugLocation(1, 854);
						mOR();

					}
					break;
				case 117:
					DebugEnterAlt(117);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:857: ORDER
					{
						DebugLocation(1, 857);
						mORDER();

					}
					break;
				case 118:
					DebugEnterAlt(118);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:863: OUTER
					{
						DebugLocation(1, 863);
						mOUTER();

					}
					break;
				case 119:
					DebugEnterAlt(119);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:869: PLAN
					{
						DebugLocation(1, 869);
						mPLAN();

					}
					break;
				case 120:
					DebugEnterAlt(120);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:874: PRAGMA
					{
						DebugLocation(1, 874);
						mPRAGMA();

					}
					break;
				case 121:
					DebugEnterAlt(121);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:881: PRIMARY
					{
						DebugLocation(1, 881);
						mPRIMARY();

					}
					break;
				case 122:
					DebugEnterAlt(122);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:889: QUERY
					{
						DebugLocation(1, 889);
						mQUERY();

					}
					break;
				case 123:
					DebugEnterAlt(123);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:895: RAISE
					{
						DebugLocation(1, 895);
						mRAISE();

					}
					break;
				case 124:
					DebugEnterAlt(124);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:901: REFERENCES
					{
						DebugLocation(1, 901);
						mREFERENCES();

					}
					break;
				case 125:
					DebugEnterAlt(125);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:912: REGEXP
					{
						DebugLocation(1, 912);
						mREGEXP();

					}
					break;
				case 126:
					DebugEnterAlt(126);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:919: REINDEX
					{
						DebugLocation(1, 919);
						mREINDEX();

					}
					break;
				case 127:
					DebugEnterAlt(127);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:927: RELEASE
					{
						DebugLocation(1, 927);
						mRELEASE();

					}
					break;
				case 128:
					DebugEnterAlt(128);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:935: RENAME
					{
						DebugLocation(1, 935);
						mRENAME();

					}
					break;
				case 129:
					DebugEnterAlt(129);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:942: REPLACE
					{
						DebugLocation(1, 942);
						mREPLACE();

					}
					break;
				case 130:
					DebugEnterAlt(130);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:950: RESTRICT
					{
						DebugLocation(1, 950);
						mRESTRICT();

					}
					break;
				case 131:
					DebugEnterAlt(131);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:959: ROLLBACK
					{
						DebugLocation(1, 959);
						mROLLBACK();

					}
					break;
				case 132:
					DebugEnterAlt(132);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:968: ROW
					{
						DebugLocation(1, 968);
						mROW();

					}
					break;
				case 133:
					DebugEnterAlt(133);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:972: SAVEPOINT
					{
						DebugLocation(1, 972);
						mSAVEPOINT();

					}
					break;
				case 134:
					DebugEnterAlt(134);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:982: SELECT
					{
						DebugLocation(1, 982);
						mSELECT();

					}
					break;
				case 135:
					DebugEnterAlt(135);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:989: SET
					{
						DebugLocation(1, 989);
						mSET();

					}
					break;
				case 136:
					DebugEnterAlt(136);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:993: TABLE
					{
						DebugLocation(1, 993);
						mTABLE();

					}
					break;
				case 137:
					DebugEnterAlt(137);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:999: TEMPORARY
					{
						DebugLocation(1, 999);
						mTEMPORARY();

					}
					break;
				case 138:
					DebugEnterAlt(138);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1009: THEN
					{
						DebugLocation(1, 1009);
						mTHEN();

					}
					break;
				case 139:
					DebugEnterAlt(139);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1014: TO
					{
						DebugLocation(1, 1014);
						mTO();

					}
					break;
				case 140:
					DebugEnterAlt(140);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1017: TRANSACTION
					{
						DebugLocation(1, 1017);
						mTRANSACTION();

					}
					break;
				case 141:
					DebugEnterAlt(141);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1029: TRIGGER
					{
						DebugLocation(1, 1029);
						mTRIGGER();

					}
					break;
				case 142:
					DebugEnterAlt(142);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1037: UNION
					{
						DebugLocation(1, 1037);
						mUNION();

					}
					break;
				case 143:
					DebugEnterAlt(143);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1043: UNIQUE
					{
						DebugLocation(1, 1043);
						mUNIQUE();

					}
					break;
				case 144:
					DebugEnterAlt(144);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1050: UPDATE
					{
						DebugLocation(1, 1050);
						mUPDATE();

					}
					break;
				case 145:
					DebugEnterAlt(145);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1057: USING
					{
						DebugLocation(1, 1057);
						mUSING();

					}
					break;
				case 146:
					DebugEnterAlt(146);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1063: VACUUM
					{
						DebugLocation(1, 1063);
						mVACUUM();

					}
					break;
				case 147:
					DebugEnterAlt(147);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1070: VALUES
					{
						DebugLocation(1, 1070);
						mVALUES();

					}
					break;
				case 148:
					DebugEnterAlt(148);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1077: VIEW
					{
						DebugLocation(1, 1077);
						mVIEW();

					}
					break;
				case 149:
					DebugEnterAlt(149);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1082: VIRTUAL
					{
						DebugLocation(1, 1082);
						mVIRTUAL();

					}
					break;
				case 150:
					DebugEnterAlt(150);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1090: WHEN
					{
						DebugLocation(1, 1090);
						mWHEN();

					}
					break;
				case 151:
					DebugEnterAlt(151);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1095: WHERE
					{
						DebugLocation(1, 1095);
						mWHERE();

					}
					break;
				case 152:
					DebugEnterAlt(152);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1101: STRING
					{
						DebugLocation(1, 1101);
						mSTRING();

					}
					break;
				case 153:
					DebugEnterAlt(153);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1108: ID
					{
						DebugLocation(1, 1108);
						mID();

					}
					break;
				case 154:
					DebugEnterAlt(154);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1111: INTEGER
					{
						DebugLocation(1, 1111);
						mINTEGER();

					}
					break;
				case 155:
					DebugEnterAlt(155);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1119: FLOAT
					{
						DebugLocation(1, 1119);
						mFLOAT();

					}
					break;
				case 156:
					DebugEnterAlt(156);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1125: BLOB
					{
						DebugLocation(1, 1125);
						mBLOB();

					}
					break;
				case 157:
					DebugEnterAlt(157);
					// C:\\Users\\Gareth\\Desktop\\test.g:1:1130: WS
					{
						DebugLocation(1, 1130);
						mWS();

					}
					break;

			}

		}


		#region DFA
		DFA21 dfa21;
		DFA28 dfa28;

		protected override void InitDFAs()
		{
			base.InitDFAs();
			dfa21 = new DFA21(this);
			dfa28 = new DFA28(this, SpecialStateTransition28);
		}

		private class DFA21 : DFA
		{
			private const string DFA21_eotS =
				"\x5\xFFFF";
			private const string DFA21_eofS =
				"\x5\xFFFF";
			private const string DFA21_minS =
				"\x2\x2E\x3\xFFFF";
			private const string DFA21_maxS =
				"\x1\x39\x1\x65\x3\xFFFF";
			private const string DFA21_acceptS =
				"\x2\xFFFF\x1\x2\x1\x1\x1\x3";
			private const string DFA21_specialS =
				"\x5\xFFFF}>";
			private static readonly string[] DFA21_transitionS =
			{
				"\x1\x2\x1\xFFFF\xA\x1",
				"\x1\x3\x1\xFFFF\xA\x1\xB\xFFFF\x1\x4\x1F\xFFFF\x1\x4",
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

			public override string Description { get { return "969:1: FLOAT : ( ( '0' .. '9' )+ DOT ( '0' .. '9' )* ( FLOAT_EXP )? | DOT ( '0' .. '9' )+ ( FLOAT_EXP )? | ( '0' .. '9' )+ FLOAT_EXP );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private class DFA28 : DFA
		{
			private const string DFA28_eotS =
				"\x1\xFFFF\x1\x39\x1\xFFFF\x1\x3D\x1\x40\x1\xFFFF\x1\x42\x1\xFFFF\x1" +
				"\x43\x2\xFFFF\x1\x44\x3\xFFFF\x1\x45\x7\xFFFF\x1\x47\x1\x49\x1\x4A\x1" +
				"\x4B\x1\xFFFF\x1\x4C\x18\x36\x1\x90\x17\xFFFF\x5\x36\x1\x98\x3\x36\x1" +
				"\x9F\x14\x36\x1\xBC\x2\x36\x1\xBF\x1\xC5\x8\x36\x1\xD0\x1\xD2\x1\xD3" +
				"\xC\x36\x1\xEA\x7\x36\x2\xFFFF\x1\x36\x1\xF6\x1\x36\x1\xF8\x2\x36\x1" +
				"\xFB\x1\xFFFF\x1\xFC\x5\x36\x1\xFFFF\x11\x36\x1\x118\x5\x36\x1\x11F\x4" +
				"\x36\x1\xFFFF\x2\x36\x1\xFFFF\x5\x36\x1\xFFFF\x2\x36\x1\x130\x5\x36\x1" +
				"\x136\x1\x36\x1\xFFFF\x1\x36\x2\xFFFF\xF\x36\x1\x149\x2\x36\x1\x14C\x3" +
				"\x36\x1\xFFFF\xB\x36\x1\xFFFF\x1\x36\x1\xFFFF\x2\x36\x2\xFFFF\x6\x36" +
				"\x1\x166\x1\x167\xD\x36\x1\x175\x2\x36\x1\x178\x1\x179\x1\x17A\x1\xFFFF" +
				"\x5\x36\x1\x180\x1\xFFFF\x1\x36\x1\x182\x1\x183\xA\x36\x1\x18E\x1\x36" +
				"\x1\x190\x1\xFFFF\x1\x191\x1\x192\x3\x36\x1\xFFFF\x1\x36\x1\x197\x3\x36" +
				"\x1\x19B\xC\x36\x1\xFFFF\x2\x36\x1\xFFFF\x1\x36\x1\x1AB\x1\x1AD\x8\x36" +
				"\x1\x1B6\x1\x36\x1\x1B8\x1\x36\x1\x1BA\x1\x1BB\x1\x1BC\x4\x36\x1\x1C1" +
				"\x2\x36\x2\xFFFF\x1\x1C4\x6\x36\x1\x1CB\x5\x36\x1\xFFFF\x2\x36\x3\xFFFF" +
				"\x5\x36\x1\xFFFF\x1\x36\x2\xFFFF\x1\x1D9\x3\x36\x1\x1DD\x1\x36\x1\x1E0" +
				"\x3\x36\x1\xFFFF\x1\x36\x3\xFFFF\x1\x1E5\x1\x1E6\x2\x36\x1\xFFFF\x1\x36" +
				"\x1\x1EA\x1\x1EB\x1\xFFFF\x2\x36\x1\x1EE\x1\x1EF\xA\x36\x1\x1FA\x1\xFFFF" +
				"\x1\x36\x1\xFFFF\x2\x36\x1\x1FE\x2\x36\x1\x201\x2\x36\x1\xFFFF\x1\x36" +
				"\x1\xFFFF\x1\x205\x3\xFFFF\x1\x36\x1\x207\x1\x36\x1\x209\x1\xFFFF\x2" +
				"\x36\x1\xFFFF\x1\x36\x1\x20D\x1\x20E\x2\x36\x1\x211\x1\xFFFF\x4\x36\x1" +
				"\x217\x1\x218\x1\x36\x1\x21A\x1\x21B\x1\x36\x1\x21D\x2\x36\x1\xFFFF\x1" +
				"\x220\x1\x221\x1\x36\x1\xFFFF\x2\x36\x1\xFFFF\x1\x225\x2\x36\x1\x228" +
				"\x2\xFFFF\x2\x36\x1\x22B\x2\xFFFF\x1\x22C\x1\x36\x2\xFFFF\x1\x36\x1\x22F" +
				"\x2\x36\x1\x232\x4\x36\x1\x237\x1\xFFFF\x3\x36\x1\xFFFF\x1\x23B\x1\x23C" +
				"\x1\xFFFF\x1\x23D\x1\x23E\x1\x36\x1\xFFFF\x1\x240\x1\xFFFF\x1\x36\x1" +
				"\xFFFF\x1\x242\x1\x243\x1\x244\x2\xFFFF\x2\x36\x1\xFFFF\x2\x36\x1\x249" +
				"\x2\x36\x2\xFFFF\x1\x36\x2\xFFFF\x1\x36\x1\xFFFF\x1\x24E\x1\x24F\x2\xFFFF" +
				"\x1\x36\x1\x251\x1\x36\x1\xFFFF\x1\x253\x1\x36\x1\xFFFF\x1\x255\x1\x256" +
				"\x2\xFFFF\x1\x257\x1\x36\x1\xFFFF\x1\x259\x1\x25A\x1\xFFFF\x1\x25B\x3" +
				"\x36\x1\xFFFF\x2\x36\x1\x261\x4\xFFFF\x1\x262\x1\xFFFF\x1\x36\x3\xFFFF" +
				"\x1\x264\x2\x36\x1\x268\x1\xFFFF\x1\x36\x1\x26A\x1\x26B\x1\x36\x2\xFFFF" +
				"\x1\x36\x1\xFFFF\x1\x36\x1\xFFFF\x1\x36\x3\xFFFF\x1\x36\x3\xFFFF\x1\x271" +
				"\x1\x272\x3\x36\x2\xFFFF\x1\x36\x1\xFFFF\x3\x36\x1\xFFFF\x1\x36\x2\xFFFF" +
				"\x1\x27B\x1\x27C\x1\x27D\x1\x27E\x1\x36\x2\xFFFF\x1\x280\x1\x1AB\x2\x36" +
				"\x1\x283\x2\x36\x1\x286\x4\xFFFF\x1\x287\x1\xFFFF\x2\x36\x1\xFFFF\x2" +
				"\x36\x2\xFFFF\x1\x28C\x1\x36\x1\x28E\x1\x290\x1\xFFFF\x1\x291\x1\xFFFF" +
				"\x1\x36\x2\xFFFF\x3\x36\x1\x296\x1\xFFFF";
			private const string DFA28_eofS =
				"\x297\xFFFF";
			private const string DFA28_minS =
				"\x1\x9\x1\x3D\x1\xFFFF\x1\x3C\x1\x3D\x1\xFFFF\x1\x7C\x1\xFFFF\x1\x2D" +
				"\x2\xFFFF\x1\x2A\x3\xFFFF\x1\x30\x7\xFFFF\x4\x0\x1\xFFFF\x1\x24\x1\x42" +
				"\x1\x45\x4\x41\x1\x4C\x1\x41\x1\x46\x1\x4F\x2\x45\x2\x41\x1\x46\x1\x4C" +
				"\x1\x55\x3\x41\x1\x4E\x1\x41\x1\x48\x1\x27\x1\x2E\x17\xFFFF\x1\x4F\x1" +
				"\x44\x1\x54\x1\x4C\x1\x41\x1\x24\x2\x54\x1\x46\x1\x24\x1\x53\x1\x45\x1" +
				"\x4C\x1\x45\x1\x52\x1\x54\x1\x46\x1\x53\x1\x4F\x1\x43\x1\x53\x1\x44\x2" +
				"\x43\x1\x49\x1\x52\x3\x4F\x1\x56\x1\x24\x1\x4E\x1\x4D\x2\x24\x1\x49\x1" +
				"\x59\x1\x46\x1\x4B\x3\x54\x1\x4C\x3\x24\x1\x54\x2\x41\x1\x45\x1\x49\x1" +
				"\x46\x1\x4C\x1\x56\x1\x4C\x1\x42\x1\x4D\x1\x45\x1\x24\x1\x41\x1\x49\x1" +
				"\x44\x1\x49\x1\x43\x2\x45\x2\xFFFF\x1\x52\x1\x24\x1\x45\x1\x24\x1\x45" +
				"\x1\x4C\x1\x24\x1\xFFFF\x1\x24\x1\x41\x2\x4F\x1\x49\x1\x57\x1\xFFFF\x2" +
				"\x43\x1\x4C\x1\x4D\x1\x46\x1\x41\x1\x53\x1\x52\x2\x41\x1\x45\x1\x43\x1" +
				"\x41\x1\x54\x1\x50\x1\x48\x1\x45\x1\x24\x1\x41\x1\x45\x1\x53\x2\x4C\x1" +
				"\x24\x1\x4D\x1\x42\x1\x55\x1\x49\x1\xFFFF\x1\x4F\x1\x45\x1\xFFFF\x1\x45" +
				"\x1\x54\x3\x45\x1\xFFFF\x1\x55\x1\x4E\x1\x24\x1\x54\x1\x45\x1\x49\x1" +
				"\x43\x1\x55\x1\x24\x1\x4C\x1\xFFFF\x1\x53\x2\xFFFF\x2\x45\x1\x4E\x1\x47" +
				"\x1\x4D\x1\x52\x1\x53\x2\x45\x1\x4E\x1\x45\x1\x41\x1\x4C\x1\x54\x1\x4C" +
				"\x1\x24\x2\x45\x1\x24\x1\x4C\x1\x50\x1\x4E\x1\xFFFF\x1\x4E\x1\x47\x1" +
				"\x4F\x1\x41\x1\x4E\x2\x55\x1\x57\x1\x54\x1\x4E\x1\x54\x1\xFFFF\x1\x52" +
				"\x1\xFFFF\x1\x52\x1\x59\x2\xFFFF\x1\x43\x1\x49\x1\x52\x1\x4E\x1\x45\x1" +
				"\x41\x2\x24\x1\x4B\x1\x41\x1\x4D\x1\x49\x1\x4C\x2\x54\x1\x53\x1\x45\x1" +
				"\x42\x1\x55\x1\x52\x1\x54\x1\x24\x1\x43\x1\x49\x3\x24\x1\xFFFF\x2\x50" +
				"\x1\x55\x1\x54\x1\x41\x1\x24\x1\xFFFF\x1\x49\x2\x24\x1\x50\x1\x4E\x1" +
				"\x52\x1\x44\x1\x58\x1\x49\x2\x52\x1\x45\x1\x52\x1\x24\x1\x4C\x1\x24\x1" +
				"\xFFFF\x2\x24\x1\x54\x1\x48\x1\x52\x1\xFFFF\x1\x55\x1\x24\x1\x45\x2\x52" +
				"\x1\x24\x1\x4D\x1\x41\x1\x59\x1\x45\x1\x52\x1\x58\x1\x44\x1\x41\x1\x4D" +
				"\x1\x41\x1\x52\x1\x42\x1\xFFFF\x1\x50\x1\x43\x1\xFFFF\x1\x45\x2\x24\x1" +
				"\x53\x1\x47\x1\x4E\x1\x55\x1\x54\x1\x47\x1\x55\x1\x45\x1\x24\x1\x55\x1" +
				"\x24\x1\x45\x3\x24\x1\x5A\x1\x48\x1\x4E\x1\x45\x1\x24\x1\x45\x1\x44\x2" +
				"\xFFFF\x1\x24\x1\x54\x1\x4E\x1\x54\x1\x49\x1\x52\x1\x45\x1\x24\x1\x4E" +
				"\x1\x41\x1\x4C\x1\x52\x1\x45\x1\xFFFF\x1\x48\x1\x4E\x3\xFFFF\x1\x45\x1" +
				"\x54\x2\x53\x1\x49\x1\xFFFF\x1\x47\x2\xFFFF\x1\x24\x1\x47\x1\x45\x1\x49" +
				"\x1\x24\x1\x41\x1\x24\x1\x54\x1\x41\x1\x53\x1\xFFFF\x1\x4C\x3\xFFFF\x2" +
				"\x24\x1\x41\x1\x4C\x1\xFFFF\x1\x54\x2\x24\x1\xFFFF\x1\x41\x1\x52\x2\x24" +
				"\x1\x45\x1\x50\x1\x45\x1\x53\x1\x45\x1\x43\x1\x49\x1\x41\x1\x4F\x1\x54" +
				"\x1\x24\x1\xFFFF\x1\x52\x1\xFFFF\x1\x41\x1\x45\x1\x24\x2\x45\x1\x24\x1" +
				"\x4D\x1\x53\x1\xFFFF\x1\x41\x1\xFFFF\x1\x24\x3\xFFFF\x1\x45\x1\x24\x1" +
				"\x43\x1\x24\x1\xFFFF\x1\x4E\x1\x45\x1\xFFFF\x1\x45\x2\x24\x1\x43\x1\x41" +
				"\x1\x24\x1\xFFFF\x1\x54\x1\x53\x1\x54\x1\x41\x2\x24\x1\x43\x2\x24\x1" +
				"\x49\x1\x24\x2\x4E\x1\xFFFF\x2\x24\x1\x41\x1\xFFFF\x1\x44\x1\x4C\x1\xFFFF" +
				"\x1\x24\x1\x44\x1\x45\x1\x24\x2\xFFFF\x2\x4C\x1\x24\x2\xFFFF\x1\x24\x1" +
				"\x59\x2\xFFFF\x1\x4E\x1\x24\x1\x58\x1\x45\x1\x24\x1\x45\x2\x43\x1\x49" +
				"\x1\x24\x1\xFFFF\x1\x41\x1\x43\x1\x52\x1\xFFFF\x2\x24\x1\xFFFF\x2\x24" +
				"\x1\x4C\x1\xFFFF\x1\x24\x1\xFFFF\x1\x52\x1\xFFFF\x3\x24\x2\xFFFF\x1\x54" +
				"\x1\x49\x1\xFFFF\x1\x5F\x1\x45\x1\x24\x1\x42\x1\x44\x2\xFFFF\x1\x54\x2" +
				"\xFFFF\x1\x56\x1\xFFFF\x2\x24\x2\xFFFF\x1\x54\x1\x24\x1\x4C\x1\xFFFF" +
				"\x1\x24\x1\x43\x1\xFFFF\x2\x24\x2\xFFFF\x1\x24\x1\x43\x1\xFFFF\x2\x24" +
				"\x1\xFFFF\x1\x24\x1\x54\x1\x4B\x1\x4E\x1\xFFFF\x1\x52\x1\x54\x1\x24\x4" +
				"\xFFFF\x1\x24\x1\xFFFF\x1\x45\x3\xFFFF\x1\x24\x1\x4E\x1\x44\x1\x24\x1" +
				"\xFFFF\x1\x4C\x2\x24\x1\x45\x2\xFFFF\x1\x45\x1\xFFFF\x1\x59\x1\xFFFF" +
				"\x1\x54\x3\xFFFF\x1\x45\x3\xFFFF\x2\x24\x1\x54\x1\x59\x1\x49\x2\xFFFF" +
				"\x1\x4D\x1\xFFFF\x1\x54\x1\x49\x1\x41\x1\xFFFF\x1\x45\x2\xFFFF\x4\x24" +
				"\x1\x53\x2\xFFFF\x2\x24\x1\x4F\x1\x45\x1\x24\x1\x4D\x1\x54\x1\x24\x4" +
				"\xFFFF\x1\x24\x1\xFFFF\x2\x4E\x1\xFFFF\x2\x45\x2\xFFFF\x1\x24\x1\x54" +
				"\x2\x24\x1\xFFFF\x1\x24\x1\xFFFF\x1\x54\x2\xFFFF\x1\x41\x1\x4D\x1\x50" +
				"\x1\x24\x1\xFFFF";
			private const string DFA28_maxS =
				"\x1\x7E\x1\x3D\x1\xFFFF\x2\x3E\x1\xFFFF\x1\x7C\x1\xFFFF\x1\x2D\x2\xFFFF" +
				"\x1\x2A\x3\xFFFF\x1\x39\x7\xFFFF\x4\xFFFF\x1\xFFFF\x1\x7A\x1\x75\x1\x79" +
				"\x1\x75\x1\x72\x1\x78\x2\x72\x1\x61\x1\x73\x1\x6F\x1\x65\x1\x69\x1\x61" +
				"\x2\x75\x1\x72\x1\x75\x1\x6F\x1\x65\x1\x72\x1\x73\x1\x69\x1\x68\x1\x27" +
				"\x1\x65\x17\xFFFF\x1\x6F\x1\x64\x2\x74\x1\x64\x1\x7A\x3\x74\x1\x7A\x1" +
				"\x73\x1\x65\x1\x6E\x1\x6F\x1\x72\x2\x74\x1\x73\x1\x6F\x1\x63\x1\x73\x1" +
				"\x64\x1\x63\x1\x70\x1\x69\x1\x72\x3\x6F\x1\x76\x1\x7A\x1\x6E\x1\x6D\x2" +
				"\x7A\x1\x69\x1\x79\x1\x66\x1\x6D\x3\x74\x1\x6C\x3\x7A\x1\x74\x1\x61\x1" +
				"\x69\x1\x65\x1\x69\x1\x73\x1\x77\x1\x76\x1\x74\x1\x62\x1\x6D\x1\x65\x1" +
				"\x7A\x2\x69\x1\x64\x1\x69\x1\x6C\x1\x72\x1\x65\x2\xFFFF\x1\x72\x1\x7A" +
				"\x1\x65\x1\x7A\x1\x65\x1\x6C\x1\x7A\x1\xFFFF\x1\x7A\x1\x61\x2\x6F\x1" +
				"\x69\x1\x77\x1\xFFFF\x1\x74\x1\x63\x1\x75\x1\x6D\x1\x73\x1\x61\x1\x73" +
				"\x1\x72\x1\x61\x2\x65\x1\x63\x1\x61\x1\x74\x1\x70\x1\x68\x1\x65\x1\x7A" +
				"\x1\x61\x1\x6C\x1\x73\x2\x6C\x1\x7A\x1\x6D\x1\x62\x1\x75\x1\x69\x1\xFFFF" +
				"\x1\x6F\x1\x65\x1\xFFFF\x1\x65\x1\x74\x1\x65\x1\x74\x1\x6F\x1\xFFFF\x1" +
				"\x75\x1\x6E\x1\x7A\x1\x74\x1\x65\x1\x69\x1\x63\x1\x75\x1\x7A\x1\x6C\x1" +
				"\xFFFF\x1\x73\x2\xFFFF\x2\x65\x1\x6E\x1\x67\x1\x6D\x1\x72\x1\x73\x2\x65" +
				"\x1\x6E\x1\x65\x1\x61\x1\x6C\x1\x74\x1\x6C\x1\x7A\x2\x65\x1\x7A\x1\x6C" +
				"\x1\x70\x1\x6E\x1\xFFFF\x1\x6E\x1\x67\x1\x71\x1\x61\x1\x6E\x2\x75\x1" +
				"\x77\x1\x74\x1\x72\x1\x74\x1\xFFFF\x1\x72\x1\xFFFF\x1\x72\x1\x79\x2\xFFFF" +
				"\x1\x63\x1\x69\x1\x72\x1\x6E\x1\x65\x1\x61\x2\x7A\x1\x6B\x1\x61\x1\x6D" +
				"\x1\x69\x1\x6C\x2\x74\x1\x73\x1\x65\x1\x62\x1\x75\x1\x72\x1\x74\x1\x7A" +
				"\x1\x63\x1\x69\x3\x7A\x1\xFFFF\x2\x70\x1\x75\x1\x74\x1\x61\x1\x7A\x1" +
				"\xFFFF\x1\x69\x2\x7A\x1\x70\x1\x6E\x1\x72\x1\x64\x1\x78\x1\x69\x2\x72" +
				"\x1\x65\x1\x72\x1\x7A\x1\x6C\x1\x7A\x1\xFFFF\x2\x7A\x1\x74\x1\x68\x1" +
				"\x72\x1\xFFFF\x1\x75\x1\x7A\x1\x65\x2\x72\x1\x7A\x1\x6D\x1\x61\x1\x79" +
				"\x1\x65\x1\x72\x1\x78\x1\x64\x1\x61\x1\x6D\x1\x61\x1\x72\x1\x62\x1\xFFFF" +
				"\x1\x70\x1\x63\x1\xFFFF\x1\x65\x2\x7A\x1\x73\x1\x67\x1\x6E\x1\x75\x1" +
				"\x74\x1\x67\x1\x75\x1\x65\x1\x7A\x1\x75\x1\x7A\x1\x65\x4\x7A\x1\x68\x1" +
				"\x6E\x1\x65\x1\x7A\x1\x65\x1\x64\x2\xFFFF\x1\x7A\x1\x74\x1\x6E\x1\x74" +
				"\x1\x69\x1\x72\x1\x65\x1\x7A\x1\x6E\x1\x61\x1\x6C\x1\x72\x1\x65\x1\xFFFF" +
				"\x1\x68\x1\x6E\x3\xFFFF\x1\x65\x1\x74\x2\x73\x1\x69\x1\xFFFF\x1\x67\x2" +
				"\xFFFF\x1\x7A\x1\x67\x1\x65\x1\x69\x1\x7A\x1\x61\x1\x7A\x1\x74\x1\x61" +
				"\x1\x73\x1\xFFFF\x1\x6C\x3\xFFFF\x2\x7A\x1\x61\x1\x6C\x1\xFFFF\x1\x74" +
				"\x2\x7A\x1\xFFFF\x1\x61\x1\x72\x2\x7A\x1\x65\x1\x70\x1\x65\x1\x73\x1" +
				"\x65\x1\x63\x1\x69\x1\x61\x1\x6F\x1\x74\x1\x7A\x1\xFFFF\x1\x72\x1\xFFFF" +
				"\x1\x61\x1\x65\x1\x7A\x2\x65\x1\x7A\x1\x6D\x1\x73\x1\xFFFF\x1\x61\x1" +
				"\xFFFF\x1\x7A\x3\xFFFF\x1\x65\x1\x7A\x1\x63\x1\x7A\x1\xFFFF\x1\x6E\x1" +
				"\x65\x1\xFFFF\x1\x65\x2\x7A\x1\x63\x1\x61\x1\x7A\x1\xFFFF\x1\x74\x1\x73" +
				"\x1\x74\x1\x65\x2\x7A\x1\x63\x2\x7A\x1\x69\x1\x7A\x2\x6E\x1\xFFFF\x2" +
				"\x7A\x1\x61\x1\xFFFF\x1\x64\x1\x6C\x1\xFFFF\x1\x7A\x1\x64\x1\x65\x1\x7A" +
				"\x2\xFFFF\x2\x6C\x1\x7A\x2\xFFFF\x1\x7A\x1\x79\x2\xFFFF\x1\x6E\x1\x7A" +
				"\x1\x78\x1\x65\x1\x7A\x1\x65\x2\x63\x1\x69\x1\x7A\x1\xFFFF\x1\x61\x1" +
				"\x63\x1\x72\x1\xFFFF\x2\x7A\x1\xFFFF\x2\x7A\x1\x6C\x1\xFFFF\x1\x7A\x1" +
				"\xFFFF\x1\x72\x1\xFFFF\x3\x7A\x2\xFFFF\x1\x74\x1\x69\x1\xFFFF\x1\x5F" +
				"\x1\x65\x1\x7A\x1\x62\x1\x64\x2\xFFFF\x1\x74\x2\xFFFF\x1\x76\x1\xFFFF" +
				"\x2\x7A\x2\xFFFF\x1\x74\x1\x7A\x1\x6C\x1\xFFFF\x1\x7A\x1\x63\x1\xFFFF" +
				"\x2\x7A\x2\xFFFF\x1\x7A\x1\x63\x1\xFFFF\x2\x7A\x1\xFFFF\x1\x7A\x1\x74" +
				"\x1\x6B\x1\x6E\x1\xFFFF\x1\x72\x1\x74\x1\x7A\x4\xFFFF\x1\x7A\x1\xFFFF" +
				"\x1\x65\x3\xFFFF\x1\x7A\x1\x6E\x1\x74\x1\x7A\x1\xFFFF\x1\x6C\x2\x7A\x1" +
				"\x65\x2\xFFFF\x1\x65\x1\xFFFF\x1\x79\x1\xFFFF\x1\x74\x3\xFFFF\x1\x65" +
				"\x3\xFFFF\x2\x7A\x1\x74\x1\x79\x1\x69\x2\xFFFF\x1\x6D\x1\xFFFF\x1\x74" +
				"\x1\x69\x1\x61\x1\xFFFF\x1\x65\x2\xFFFF\x4\x7A\x1\x73\x2\xFFFF\x2\x7A" +
				"\x1\x6F\x1\x65\x1\x7A\x1\x6D\x1\x74\x1\x7A\x4\xFFFF\x1\x7A\x1\xFFFF\x2" +
				"\x6E\x1\xFFFF\x2\x65\x2\xFFFF\x1\x7A\x1\x74\x2\x7A\x1\xFFFF\x1\x7A\x1" +
				"\xFFFF\x1\x74\x2\xFFFF\x1\x61\x1\x6D\x1\x70\x1\x7A\x1\xFFFF";
			private const string DFA28_acceptS =
				"\x2\xFFFF\x1\x3\x2\xFFFF\x1\xB\x1\xFFFF\x1\xE\x1\xFFFF\x1\x10\x1\x11" +
				"\x1\xFFFF\x1\x13\x1\x14\x1\x15\x1\xFFFF\x1\x17\x1\x18\x1\x19\x1\x1A\x1" +
				"\x1B\x1\x1C\x1\x1D\x4\xFFFF\x1\x22\x1A\xFFFF\x1\x99\x1\x9D\x1\x2\x1\x1" +
				"\x1\x4\x1\x6\x1\x9\x1\x5\x1\x8\x1\xA\x1\x7\x1\xD\x1\xC\x1\xF\x1\x12\x1" +
				"\x16\x1\x9B\x1\x1E\x1\x98\x1\x1F\x1\x20\x1\x21\x1\x23\x42\xFFFF\x1\x9C" +
				"\x1\x9A\x7\xFFFF\x1\x2B\x6\xFFFF\x1\x32\x1C\xFFFF\x1\x59\x2\xFFFF\x1" +
				"\x5C\x5\xFFFF\x1\x65\xA\xFFFF\x1\x71\x1\xFFFF\x1\x73\x1\x74\x16\xFFFF" +
				"\x1\x8B\xB\xFFFF\x1\x25\x1\xFFFF\x1\x27\x2\xFFFF\x1\x2A\x1\x2C\x1B\xFFFF" +
				"\x1\x4C\x6\xFFFF\x1\x53\x10\xFFFF\x1\x68\x5\xFFFF\x1\x6E\x12\xFFFF\x1" +
				"\x84\x2\xFFFF\x1\x87\x19\xFFFF\x1\x34\x1\x35\xD\xFFFF\x1\x46\x2\xFFFF" +
				"\x1\x49\x1\x4A\x1\x4B\x5\xFFFF\x1\x52\x1\xFFFF\x1\x55\x1\x56\xA\xFFFF" +
				"\x1\x64\x1\xFFFF\x1\x67\x1\x69\x1\x6A\x4\xFFFF\x1\x70\x3\xFFFF\x1\x77" +
				"\xF\xFFFF\x1\x89\x1\xFFFF\x1\x8A\x8\xFFFF\x1\x94\x1\xFFFF\x1\x96\x1\xFFFF" +
				"\x1\x24\x1\x26\x1\x28\x4\xFFFF\x1\x30\x2\xFFFF\x1\x36\x6\xFFFF\x1\x3D" +
				"\xD\xFFFF\x1\x57\x3\xFFFF\x1\x5D\x2\xFFFF\x1\x60\x4\xFFFF\x1\x6B\x1\x6C" +
				"\x3\xFFFF\x1\x75\x1\x76\x2\xFFFF\x1\x7A\x1\x7B\xA\xFFFF\x1\x88\x3\xFFFF" +
				"\x1\x8E\x2\xFFFF\x1\x91\x3\xFFFF\x1\x97\x1\xFFFF\x1\x2D\x1\xFFFF\x1\x2F" +
				"\x3\xFFFF\x1\x38\x1\x39\x2\xFFFF\x1\x3C\x5\xFFFF\x1\x45\x1\x47\x1\xFFFF" +
				"\x1\x4D\x1\x4E\x1\xFFFF\x1\x50\x2\xFFFF\x1\x58\x1\x5A\x3\xFFFF\x1\x61" +
				"\x2\xFFFF\x1\x66\x2\xFFFF\x1\x72\x1\x78\x2\xFFFF\x1\x7D\x2\xFFFF\x1\x80" +
				"\x4\xFFFF\x1\x86\x3\xFFFF\x1\x8F\x1\x90\x1\x92\x1\x93\x1\xFFFF\x1\x29" +
				"\x1\xFFFF\x1\x31\x1\x33\x1\x37\x4\xFFFF\x1\x42\x4\xFFFF\x1\x51\x1\x54" +
				"\x1\xFFFF\x1\x5E\x1\xFFFF\x1\x62\x1\xFFFF\x1\x6D\x1\x6F\x1\x79\x1\xFFFF" +
				"\x1\x7E\x1\x7F\x1\x81\x5\xFFFF\x1\x8D\x1\x95\x1\xFFFF\x1\x3A\x3\xFFFF" +
				"\x1\x41\x1\xFFFF\x1\x44\x1\x48\x5\xFFFF\x1\x82\x1\x83\x8\xFFFF\x1\x4F" +
				"\x1\x5B\x1\x5F\x1\x63\x1\xFFFF\x1\x85\x2\xFFFF\x1\x3B\x2\xFFFF\x1\x43" +
				"\x1\x7C\x4\xFFFF\x1\x8C\x1\xFFFF\x1\x3E\x1\xFFFF\x1\x3F\x1\x2E\x4\xFFFF" +
				"\x1\x40";
			private const string DFA28_specialS =
				"\x17\xFFFF\x1\x3\x1\x2\x1\x1\x1\x0\x27C\xFFFF}>";
			private static readonly string[] DFA28_transitionS =
			{
				"\x2\x37\x1\xFFFF\x2\x37\x12\xFFFF\x1\x37\x1\x2\x1\x17\x1\xFFFF\x1\x16"+
				"\x1\xD\x1\x5\x1\x18\x1\x11\x1\x12\x1\xA\x1\x7\x1\x10\x1\x8\x1\xF\x1"+
				"\xB\xA\x35\x1\x14\x1\xE\x1\x3\x1\x1\x1\x4\x1\x13\x1\x15\x1\x1D\x1\x1E"+
				"\x1\x1F\x1\x20\x1\x21\x1\x22\x1\x23\x1\x24\x1\x25\x1\x26\x1\x27\x1\x28"+
				"\x1\x29\x1\x2A\x1\x2B\x1\x2C\x1\x2D\x1\x2E\x1\x2F\x1\x30\x1\x31\x1\x32"+
				"\x1\x33\x1\x34\x2\x36\x1\x1A\x1\xC\x1\x1B\x1\xFFFF\x1\x1C\x1\x19\x1"+
				"\x1D\x1\x1E\x1\x1F\x1\x20\x1\x21\x1\x22\x1\x23\x1\x24\x1\x25\x1\x26"+
				"\x1\x27\x1\x28\x1\x29\x1\x2A\x1\x2B\x1\x2C\x1\x2D\x1\x2E\x1\x2F\x1\x30"+
				"\x1\x31\x1\x32\x1\x33\x1\x34\x2\x36\x1\xFFFF\x1\x6\x1\xFFFF\x1\x9",
				"\x1\x38",
				"",
				"\x1\x3C\x1\x3B\x1\x3A",
				"\x1\x3E\x1\x3F",
				"",
				"\x1\x41",
				"",
				"\x1\x37",
				"",
				"",
				"\x1\x37",
				"",
				"",
				"",
				"\xA\x46",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x0\x48",
				"\x0\x48",
				"\x0\x36",
				"\x5B\x36\x1\xFFFF\xFFA4\x36",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x4D\x1\xFFFF\x1\x4E\x1\xFFFF\x1\x4F\x5\xFFFF\x1\x50\x1\xFFFF\x1"+
				"\x51\x4\xFFFF\x1\x52\x1\x53\x1\x54\xC\xFFFF\x1\x4D\x1\xFFFF\x1\x4E\x1"+
				"\xFFFF\x1\x4F\x5\xFFFF\x1\x50\x1\xFFFF\x1\x51\x4\xFFFF\x1\x52\x1\x53"+
				"\x1\x54",
				"\x1\x55\x13\xFFFF\x1\x56\xB\xFFFF\x1\x55\x13\xFFFF\x1\x56",
				"\x1\x57\x6\xFFFF\x1\x58\x6\xFFFF\x1\x59\x2\xFFFF\x1\x5A\x2\xFFFF\x1"+
				"\x5B\xB\xFFFF\x1\x57\x6\xFFFF\x1\x58\x6\xFFFF\x1\x59\x2\xFFFF\x1\x5A"+
				"\x2\xFFFF\x1\x5B",
				"\x1\x5C\x3\xFFFF\x1\x5D\x3\xFFFF\x1\x5E\x8\xFFFF\x1\x5F\xE\xFFFF\x1"+
				"\x5C\x3\xFFFF\x1\x5D\x3\xFFFF\x1\x5E\x8\xFFFF\x1\x5F",
				"\x1\x60\xA\xFFFF\x1\x61\x1\xFFFF\x1\x62\x4\xFFFF\x1\x63\x4\xFFFF\x1"+
				"\x64\x8\xFFFF\x1\x60\xA\xFFFF\x1\x61\x1\xFFFF\x1\x62\x4\xFFFF\x1\x63"+
				"\x4\xFFFF\x1\x64",
				"\x1\x65\xD\xFFFF\x1\x66\x2\xFFFF\x1\x67\xE\xFFFF\x1\x65\xD\xFFFF\x1"+
				"\x66\x2\xFFFF\x1\x67",
				"\x1\x68\x5\xFFFF\x1\x69\x19\xFFFF\x1\x68\x5\xFFFF\x1\x69",
				"\x1\x6A\x1F\xFFFF\x1\x6A",
				"\x1\x6B\x1\x6C\x5\xFFFF\x1\x6D\x1\x6E\x4\xFFFF\x1\x6F\x12\xFFFF\x1"+
				"\x6B\x1\x6C\x5\xFFFF\x1\x6D\x1\x6E\x4\xFFFF\x1\x6F",
				"\x1\x70\x1F\xFFFF\x1\x70",
				"\x1\x71\x1F\xFFFF\x1\x71",
				"\x1\x72\x3\xFFFF\x1\x73\x1B\xFFFF\x1\x72\x3\xFFFF\x1\x73",
				"\x1\x74\x1F\xFFFF\x1\x74",
				"\x1\x75\xD\xFFFF\x1\x76\x5\xFFFF\x1\x77\xB\xFFFF\x1\x75\xD\xFFFF\x1"+
				"\x76\x5\xFFFF\x1\x77",
				"\x1\x78\x7\xFFFF\x1\x79\x3\xFFFF\x1\x7A\x2\xFFFF\x1\x7B\x10\xFFFF"+
				"\x1\x78\x7\xFFFF\x1\x79\x3\xFFFF\x1\x7A\x2\xFFFF\x1\x7B",
				"\x1\x7C\x5\xFFFF\x1\x7D\x19\xFFFF\x1\x7C\x5\xFFFF\x1\x7D",
				"\x1\x7E\x1F\xFFFF\x1\x7E",
				"\x1\x7F\x3\xFFFF\x1\x80\x9\xFFFF\x1\x81\x11\xFFFF\x1\x7F\x3\xFFFF"+
				"\x1\x80\x9\xFFFF\x1\x81",
				"\x1\x82\x3\xFFFF\x1\x83\x1B\xFFFF\x1\x82\x3\xFFFF\x1\x83",
				"\x1\x84\x3\xFFFF\x1\x85\x2\xFFFF\x1\x86\x6\xFFFF\x1\x87\x2\xFFFF\x1"+
				"\x88\xE\xFFFF\x1\x84\x3\xFFFF\x1\x85\x2\xFFFF\x1\x86\x6\xFFFF\x1\x87"+
				"\x2\xFFFF\x1\x88",
				"\x1\x89\x1\xFFFF\x1\x8A\x2\xFFFF\x1\x8B\x1A\xFFFF\x1\x89\x1\xFFFF"+
				"\x1\x8A\x2\xFFFF\x1\x8B",
				"\x1\x8C\x7\xFFFF\x1\x8D\x17\xFFFF\x1\x8C\x7\xFFFF\x1\x8D",
				"\x1\x8E\x1F\xFFFF\x1\x8E",
				"\x1\x8F",
				"\x1\x46\x1\xFFFF\xA\x35\xB\xFFFF\x1\x46\x1F\xFFFF\x1\x46",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"\x1\x91\x1F\xFFFF\x1\x91",
				"\x1\x92\x1F\xFFFF\x1\x92",
				"\x1\x93\x1F\xFFFF\x1\x93",
				"\x1\x94\x7\xFFFF\x1\x95\x17\xFFFF\x1\x94\x7\xFFFF\x1\x95",
				"\x1\x96\x2\xFFFF\x1\x97\x1C\xFFFF\x1\x96\x2\xFFFF\x1\x97",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x2\x36\x1\x99\x17\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\x2\x36\x1\x99\x17\x36",
				"\x1\x9A\x1F\xFFFF\x1\x9A",
				"\x1\x9B\x1F\xFFFF\x1\x9B",
				"\x1\x9C\x1\x9D\xC\xFFFF\x1\x9E\x11\xFFFF\x1\x9C\x1\x9D\xC\xFFFF\x1"+
				"\x9E",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\xA0\x1F\xFFFF\x1\xA0",
				"\x1\xA1\x1F\xFFFF\x1\xA1",
				"\x1\xA2\x1\xA3\x1\xA4\x1D\xFFFF\x1\xA2\x1\xA3\x1\xA4",
				"\x1\xA5\x9\xFFFF\x1\xA6\x15\xFFFF\x1\xA5\x9\xFFFF\x1\xA6",
				"\x1\xA7\x1F\xFFFF\x1\xA7",
				"\x1\xA8\x1F\xFFFF\x1\xA8",
				"\x1\xA9\x5\xFFFF\x1\xAA\x6\xFFFF\x1\xAB\x1\xAC\x11\xFFFF\x1\xA9\x5"+
				"\xFFFF\x1\xAA\x6\xFFFF\x1\xAB\x1\xAC",
				"\x1\xAD\x1F\xFFFF\x1\xAD",
				"\x1\xAE\x1F\xFFFF\x1\xAE",
				"\x1\xAF\x1F\xFFFF\x1\xAF",
				"\x1\xB0\x1F\xFFFF\x1\xB0",
				"\x1\xB1\x1F\xFFFF\x1\xB1",
				"\x1\xB2\x1F\xFFFF\x1\xB2",
				"\x1\xB3\x5\xFFFF\x1\xB4\x6\xFFFF\x1\xB5\x12\xFFFF\x1\xB3\x5\xFFFF"+
				"\x1\xB4\x6\xFFFF\x1\xB5",
				"\x1\xB6\x1F\xFFFF\x1\xB6",
				"\x1\xB7\x1F\xFFFF\x1\xB7",
				"\x1\xB8\x1F\xFFFF\x1\xB8",
				"\x1\xB9\x1F\xFFFF\x1\xB9",
				"\x1\xBA\x1F\xFFFF\x1\xBA",
				"\x1\xBB\x1F\xFFFF\x1\xBB",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\xBD\x1F\xFFFF\x1\xBD",
				"\x1\xBE\x1F\xFFFF\x1\xBE",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x3\x36\x1\xC0\x4\x36\x1\xC1\x4\x36"+
				"\x1\xC2\x4\x36\x1\xC3\x1\xC4\x6\x36\x4\xFFFF\x1\x36\x1\xFFFF\x3\x36"+
				"\x1\xC0\x4\x36\x1\xC1\x4\x36\x1\xC2\x4\x36\x1\xC3\x1\xC4\x6\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\xD\x36\x1\xC6\xC\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\xD\x36\x1\xC6\xC\x36",
				"\x1\xC7\x1F\xFFFF\x1\xC7",
				"\x1\xC8\x1F\xFFFF\x1\xC8",
				"\x1\xC9\x1F\xFFFF\x1\xC9",
				"\x1\xCA\x1\xFFFF\x1\xCB\x1D\xFFFF\x1\xCA\x1\xFFFF\x1\xCB",
				"\x1\xCC\x1F\xFFFF\x1\xCC",
				"\x1\xCD\x1F\xFFFF\x1\xCD",
				"\x1\xCE\x1F\xFFFF\x1\xCE",
				"\x1\xCF\x1F\xFFFF\x1\xCF",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x5\x36\x1\xD1\x14\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\x5\x36\x1\xD1\x14\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x3\x36\x1\xD4\x16\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\x3\x36\x1\xD4\x16\x36",
				"\x1\xD5\x1F\xFFFF\x1\xD5",
				"\x1\xD6\x1F\xFFFF\x1\xD6",
				"\x1\xD7\x7\xFFFF\x1\xD8\x17\xFFFF\x1\xD7\x7\xFFFF\x1\xD8",
				"\x1\xD9\x1F\xFFFF\x1\xD9",
				"\x1\xDA\x1F\xFFFF\x1\xDA",
				"\x1\xDB\x1\xDC\x1\xFFFF\x1\xDD\x2\xFFFF\x1\xDE\x1\xFFFF\x1\xDF\x1"+
				"\xFFFF\x1\xE0\x2\xFFFF\x1\xE1\x12\xFFFF\x1\xDB\x1\xDC\x1\xFFFF\x1\xDD"+
				"\x2\xFFFF\x1\xDE\x1\xFFFF\x1\xDF\x1\xFFFF\x1\xE0\x2\xFFFF\x1\xE1",
				"\x1\xE2\xA\xFFFF\x1\xE3\x14\xFFFF\x1\xE2\xA\xFFFF\x1\xE3",
				"\x1\xE4\x1F\xFFFF\x1\xE4",
				"\x1\xE5\x7\xFFFF\x1\xE6\x17\xFFFF\x1\xE5\x7\xFFFF\x1\xE6",
				"\x1\xE7\x1F\xFFFF\x1\xE7",
				"\x1\xE8\x1F\xFFFF\x1\xE8",
				"\x1\xE9\x1F\xFFFF\x1\xE9",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\xEB\x7\xFFFF\x1\xEC\x17\xFFFF\x1\xEB\x7\xFFFF\x1\xEC",
				"\x1\xED\x1F\xFFFF\x1\xED",
				"\x1\xEE\x1F\xFFFF\x1\xEE",
				"\x1\xEF\x1F\xFFFF\x1\xEF",
				"\x1\xF0\x8\xFFFF\x1\xF1\x16\xFFFF\x1\xF0\x8\xFFFF\x1\xF1",
				"\x1\xF2\xC\xFFFF\x1\xF3\x12\xFFFF\x1\xF2\xC\xFFFF\x1\xF3",
				"\x1\xF4\x1F\xFFFF\x1\xF4",
				"",
				"",
				"\x1\xF5\x1F\xFFFF\x1\xF5",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\xF7\x1F\xFFFF\x1\xF7",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\xF9\x1F\xFFFF\x1\xF9",
				"\x1\xFA\x1F\xFFFF\x1\xFA",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\xFD\x1F\xFFFF\x1\xFD",
				"\x1\xFE\x1F\xFFFF\x1\xFE",
				"\x1\xFF\x1F\xFFFF\x1\xFF",
				"\x1\x100\x1F\xFFFF\x1\x100",
				"\x1\x101\x1F\xFFFF\x1\x101",
				"",
				"\x1\x102\x1\xFFFF\x1\x103\xE\xFFFF\x1\x104\xE\xFFFF\x1\x102\x1\xFFFF"+
				"\x1\x103\xE\xFFFF\x1\x104",
				"\x1\x105\x1F\xFFFF\x1\x105",
				"\x1\x106\x8\xFFFF\x1\x107\x16\xFFFF\x1\x106\x8\xFFFF\x1\x107",
				"\x1\x108\x1F\xFFFF\x1\x108",
				"\x1\x109\xC\xFFFF\x1\x10A\x12\xFFFF\x1\x109\xC\xFFFF\x1\x10A",
				"\x1\x10B\x1F\xFFFF\x1\x10B",
				"\x1\x10C\x1F\xFFFF\x1\x10C",
				"\x1\x10D\x1F\xFFFF\x1\x10D",
				"\x1\x10E\x1F\xFFFF\x1\x10E",
				"\x1\x10F\x3\xFFFF\x1\x110\x1B\xFFFF\x1\x10F\x3\xFFFF\x1\x110",
				"\x1\x111\x1F\xFFFF\x1\x111",
				"\x1\x112\x1F\xFFFF\x1\x112",
				"\x1\x113\x1F\xFFFF\x1\x113",
				"\x1\x114\x1F\xFFFF\x1\x114",
				"\x1\x115\x1F\xFFFF\x1\x115",
				"\x1\x116\x1F\xFFFF\x1\x116",
				"\x1\x117\x1F\xFFFF\x1\x117",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x119\x1F\xFFFF\x1\x119",
				"\x1\x11A\x6\xFFFF\x1\x11B\x18\xFFFF\x1\x11A\x6\xFFFF\x1\x11B",
				"\x1\x11C\x1F\xFFFF\x1\x11C",
				"\x1\x11D\x1F\xFFFF\x1\x11D",
				"\x1\x11E\x1F\xFFFF\x1\x11E",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x4\x36\x1\x120\x15\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\x4\x36\x1\x120\x15\x36",
				"\x1\x121\x1F\xFFFF\x1\x121",
				"\x1\x122\x1F\xFFFF\x1\x122",
				"\x1\x123\x1F\xFFFF\x1\x123",
				"\x1\x124\x1F\xFFFF\x1\x124",
				"",
				"\x1\x125\x1F\xFFFF\x1\x125",
				"\x1\x126\x1F\xFFFF\x1\x126",
				"",
				"\x1\x127\x1F\xFFFF\x1\x127",
				"\x1\x128\x1F\xFFFF\x1\x128",
				"\x1\x129\x1F\xFFFF\x1\x129",
				"\x1\x12A\xE\xFFFF\x1\x12B\x10\xFFFF\x1\x12A\xE\xFFFF\x1\x12B",
				"\x1\x12C\x9\xFFFF\x1\x12D\x15\xFFFF\x1\x12C\x9\xFFFF\x1\x12D",
				"",
				"\x1\x12E\x1F\xFFFF\x1\x12E",
				"\x1\x12F\x1F\xFFFF\x1\x12F",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x131\x1F\xFFFF\x1\x131",
				"\x1\x132\x1F\xFFFF\x1\x132",
				"\x1\x133\x1F\xFFFF\x1\x133",
				"\x1\x134\x1F\xFFFF\x1\x134",
				"\x1\x135\x1F\xFFFF\x1\x135",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\xD\x36\x1\x137\xC\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\xD\x36\x1\x137\xC\x36",
				"\x1\x138\x1F\xFFFF\x1\x138",
				"",
				"\x1\x139\x1F\xFFFF\x1\x139",
				"",
				"",
				"\x1\x13A\x1F\xFFFF\x1\x13A",
				"\x1\x13B\x1F\xFFFF\x1\x13B",
				"\x1\x13C\x1F\xFFFF\x1\x13C",
				"\x1\x13D\x1F\xFFFF\x1\x13D",
				"\x1\x13E\x1F\xFFFF\x1\x13E",
				"\x1\x13F\x1F\xFFFF\x1\x13F",
				"\x1\x140\x1F\xFFFF\x1\x140",
				"\x1\x141\x1F\xFFFF\x1\x141",
				"\x1\x142\x1F\xFFFF\x1\x142",
				"\x1\x143\x1F\xFFFF\x1\x143",
				"\x1\x144\x1F\xFFFF\x1\x144",
				"\x1\x145\x1F\xFFFF\x1\x145",
				"\x1\x146\x1F\xFFFF\x1\x146",
				"\x1\x147\x1F\xFFFF\x1\x147",
				"\x1\x148\x1F\xFFFF\x1\x148",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x14A\x1F\xFFFF\x1\x14A",
				"\x1\x14B\x1F\xFFFF\x1\x14B",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x14D\x1F\xFFFF\x1\x14D",
				"\x1\x14E\x1F\xFFFF\x1\x14E",
				"\x1\x14F\x1F\xFFFF\x1\x14F",
				"",
				"\x1\x150\x1F\xFFFF\x1\x150",
				"\x1\x151\x1F\xFFFF\x1\x151",
				"\x1\x152\x1\xFFFF\x1\x153\x1D\xFFFF\x1\x152\x1\xFFFF\x1\x153",
				"\x1\x154\x1F\xFFFF\x1\x154",
				"\x1\x155\x1F\xFFFF\x1\x155",
				"\x1\x156\x1F\xFFFF\x1\x156",
				"\x1\x157\x1F\xFFFF\x1\x157",
				"\x1\x158\x1F\xFFFF\x1\x158",
				"\x1\x159\x1F\xFFFF\x1\x159",
				"\x1\x15A\x3\xFFFF\x1\x15B\x1B\xFFFF\x1\x15A\x3\xFFFF\x1\x15B",
				"\x1\x15C\x1F\xFFFF\x1\x15C",
				"",
				"\x1\x15D\x1F\xFFFF\x1\x15D",
				"",
				"\x1\x15E\x1F\xFFFF\x1\x15E",
				"\x1\x15F\x1F\xFFFF\x1\x15F",
				"",
				"",
				"\x1\x160\x1F\xFFFF\x1\x160",
				"\x1\x161\x1F\xFFFF\x1\x161",
				"\x1\x162\x1F\xFFFF\x1\x162",
				"\x1\x163\x1F\xFFFF\x1\x163",
				"\x1\x164\x1F\xFFFF\x1\x164",
				"\x1\x165\x1F\xFFFF\x1\x165",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x168\x1F\xFFFF\x1\x168",
				"\x1\x169\x1F\xFFFF\x1\x169",
				"\x1\x16A\x1F\xFFFF\x1\x16A",
				"\x1\x16B\x1F\xFFFF\x1\x16B",
				"\x1\x16C\x1F\xFFFF\x1\x16C",
				"\x1\x16D\x1F\xFFFF\x1\x16D",
				"\x1\x16E\x1F\xFFFF\x1\x16E",
				"\x1\x16F\x1F\xFFFF\x1\x16F",
				"\x1\x170\x1F\xFFFF\x1\x170",
				"\x1\x171\x1F\xFFFF\x1\x171",
				"\x1\x172\x1F\xFFFF\x1\x172",
				"\x1\x173\x1F\xFFFF\x1\x173",
				"\x1\x174\x1F\xFFFF\x1\x174",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x176\x1F\xFFFF\x1\x176",
				"\x1\x177\x1F\xFFFF\x1\x177",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x17B\x1F\xFFFF\x1\x17B",
				"\x1\x17C\x1F\xFFFF\x1\x17C",
				"\x1\x17D\x1F\xFFFF\x1\x17D",
				"\x1\x17E\x1F\xFFFF\x1\x17E",
				"\x1\x17F\x1F\xFFFF\x1\x17F",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x181\x1F\xFFFF\x1\x181",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x184\x1F\xFFFF\x1\x184",
				"\x1\x185\x1F\xFFFF\x1\x185",
				"\x1\x186\x1F\xFFFF\x1\x186",
				"\x1\x187\x1F\xFFFF\x1\x187",
				"\x1\x188\x1F\xFFFF\x1\x188",
				"\x1\x189\x1F\xFFFF\x1\x189",
				"\x1\x18A\x1F\xFFFF\x1\x18A",
				"\x1\x18B\x1F\xFFFF\x1\x18B",
				"\x1\x18C\x1F\xFFFF\x1\x18C",
				"\x1\x18D\x1F\xFFFF\x1\x18D",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x18F\x1F\xFFFF\x1\x18F",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x193\x1F\xFFFF\x1\x193",
				"\x1\x194\x1F\xFFFF\x1\x194",
				"\x1\x195\x1F\xFFFF\x1\x195",
				"",
				"\x1\x196\x1F\xFFFF\x1\x196",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x198\x1F\xFFFF\x1\x198",
				"\x1\x199\x1F\xFFFF\x1\x199",
				"\x1\x19A\x1F\xFFFF\x1\x19A",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x19C\x1F\xFFFF\x1\x19C",
				"\x1\x19D\x1F\xFFFF\x1\x19D",
				"\x1\x19E\x1F\xFFFF\x1\x19E",
				"\x1\x19F\x1F\xFFFF\x1\x19F",
				"\x1\x1A0\x1F\xFFFF\x1\x1A0",
				"\x1\x1A1\x1F\xFFFF\x1\x1A1",
				"\x1\x1A2\x1F\xFFFF\x1\x1A2",
				"\x1\x1A3\x1F\xFFFF\x1\x1A3",
				"\x1\x1A4\x1F\xFFFF\x1\x1A4",
				"\x1\x1A5\x1F\xFFFF\x1\x1A5",
				"\x1\x1A6\x1F\xFFFF\x1\x1A6",
				"\x1\x1A7\x1F\xFFFF\x1\x1A7",
				"",
				"\x1\x1A8\x1F\xFFFF\x1\x1A8",
				"\x1\x1A9\x1F\xFFFF\x1\x1A9",
				"",
				"\x1\x1AA\x1F\xFFFF\x1\x1AA",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\xE\x36\x1\x1AC\xB\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\xE\x36\x1\x1AC\xB\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1AE\x1F\xFFFF\x1\x1AE",
				"\x1\x1AF\x1F\xFFFF\x1\x1AF",
				"\x1\x1B0\x1F\xFFFF\x1\x1B0",
				"\x1\x1B1\x1F\xFFFF\x1\x1B1",
				"\x1\x1B2\x1F\xFFFF\x1\x1B2",
				"\x1\x1B3\x1F\xFFFF\x1\x1B3",
				"\x1\x1B4\x1F\xFFFF\x1\x1B4",
				"\x1\x1B5\x1F\xFFFF\x1\x1B5",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1B7\x1F\xFFFF\x1\x1B7",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1B9\x1F\xFFFF\x1\x1B9",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1BD\x1F\xFFFF\x1\x1BD",
				"\x1\x1BE\x1F\xFFFF\x1\x1BE",
				"\x1\x1BF\x1F\xFFFF\x1\x1BF",
				"\x1\x1C0\x1F\xFFFF\x1\x1C0",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1C2\x1F\xFFFF\x1\x1C2",
				"\x1\x1C3\x1F\xFFFF\x1\x1C3",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1C5\x1F\xFFFF\x1\x1C5",
				"\x1\x1C6\x1F\xFFFF\x1\x1C6",
				"\x1\x1C7\x1F\xFFFF\x1\x1C7",
				"\x1\x1C8\x1F\xFFFF\x1\x1C8",
				"\x1\x1C9\x1F\xFFFF\x1\x1C9",
				"\x1\x1CA\x1F\xFFFF\x1\x1CA",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1CC\x1F\xFFFF\x1\x1CC",
				"\x1\x1CD\x1F\xFFFF\x1\x1CD",
				"\x1\x1CE\x1F\xFFFF\x1\x1CE",
				"\x1\x1CF\x1F\xFFFF\x1\x1CF",
				"\x1\x1D0\x1F\xFFFF\x1\x1D0",
				"",
				"\x1\x1D1\x1F\xFFFF\x1\x1D1",
				"\x1\x1D2\x1F\xFFFF\x1\x1D2",
				"",
				"",
				"",
				"\x1\x1D3\x1F\xFFFF\x1\x1D3",
				"\x1\x1D4\x1F\xFFFF\x1\x1D4",
				"\x1\x1D5\x1F\xFFFF\x1\x1D5",
				"\x1\x1D6\x1F\xFFFF\x1\x1D6",
				"\x1\x1D7\x1F\xFFFF\x1\x1D7",
				"",
				"\x1\x1D8\x1F\xFFFF\x1\x1D8",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1DA\x1F\xFFFF\x1\x1DA",
				"\x1\x1DB\x1F\xFFFF\x1\x1DB",
				"\x1\x1DC\x1F\xFFFF\x1\x1DC",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x4\x36\x1\x1DE\x15\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\x4\x36\x1\x1DE\x15\x36",
				"\x1\x1DF\x1F\xFFFF\x1\x1DF",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1E1\x1F\xFFFF\x1\x1E1",
				"\x1\x1E2\x1F\xFFFF\x1\x1E2",
				"\x1\x1E3\x1F\xFFFF\x1\x1E3",
				"",
				"\x1\x1E4\x1F\xFFFF\x1\x1E4",
				"",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1E7\x1F\xFFFF\x1\x1E7",
				"\x1\x1E8\x1F\xFFFF\x1\x1E8",
				"",
				"\x1\x1E9\x1F\xFFFF\x1\x1E9",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x1EC\x1F\xFFFF\x1\x1EC",
				"\x1\x1ED\x1F\xFFFF\x1\x1ED",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1F0\x1F\xFFFF\x1\x1F0",
				"\x1\x1F1\x1F\xFFFF\x1\x1F1",
				"\x1\x1F2\x1F\xFFFF\x1\x1F2",
				"\x1\x1F3\x1F\xFFFF\x1\x1F3",
				"\x1\x1F4\x1F\xFFFF\x1\x1F4",
				"\x1\x1F5\x1F\xFFFF\x1\x1F5",
				"\x1\x1F6\x1F\xFFFF\x1\x1F6",
				"\x1\x1F7\x1F\xFFFF\x1\x1F7",
				"\x1\x1F8\x1F\xFFFF\x1\x1F8",
				"\x1\x1F9\x1F\xFFFF\x1\x1F9",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x1FB\x1F\xFFFF\x1\x1FB",
				"",
				"\x1\x1FC\x1F\xFFFF\x1\x1FC",
				"\x1\x1FD\x1F\xFFFF\x1\x1FD",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x1FF\x1F\xFFFF\x1\x1FF",
				"\x1\x200\x1F\xFFFF\x1\x200",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x202\x1F\xFFFF\x1\x202",
				"\x1\x203\x1F\xFFFF\x1\x203",
				"",
				"\x1\x204\x1F\xFFFF\x1\x204",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"",
				"\x1\x206\x1F\xFFFF\x1\x206",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x208\x1F\xFFFF\x1\x208",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x20A\x1F\xFFFF\x1\x20A",
				"\x1\x20B\x1F\xFFFF\x1\x20B",
				"",
				"\x1\x20C\x1F\xFFFF\x1\x20C",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x20F\x1F\xFFFF\x1\x20F",
				"\x1\x210\x1F\xFFFF\x1\x210",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x212\x1F\xFFFF\x1\x212",
				"\x1\x213\x1F\xFFFF\x1\x213",
				"\x1\x214\x1F\xFFFF\x1\x214",
				"\x1\x215\x3\xFFFF\x1\x216\x1B\xFFFF\x1\x215\x3\xFFFF\x1\x216",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x219\x1F\xFFFF\x1\x219",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x21C\x1F\xFFFF\x1\x21C",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x21E\x1F\xFFFF\x1\x21E",
				"\x1\x21F\x1F\xFFFF\x1\x21F",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x222\x1F\xFFFF\x1\x222",
				"",
				"\x1\x223\x1F\xFFFF\x1\x223",
				"\x1\x224\x1F\xFFFF\x1\x224",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x226\x1F\xFFFF\x1\x226",
				"\x1\x227\x1F\xFFFF\x1\x227",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"\x1\x229\x1F\xFFFF\x1\x229",
				"\x1\x22A\x1F\xFFFF\x1\x22A",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x22D\x1F\xFFFF\x1\x22D",
				"",
				"",
				"\x1\x22E\x1F\xFFFF\x1\x22E",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x230\x1F\xFFFF\x1\x230",
				"\x1\x231\x1F\xFFFF\x1\x231",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x233\x1F\xFFFF\x1\x233",
				"\x1\x234\x1F\xFFFF\x1\x234",
				"\x1\x235\x1F\xFFFF\x1\x235",
				"\x1\x236\x1F\xFFFF\x1\x236",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x238\x1F\xFFFF\x1\x238",
				"\x1\x239\x1F\xFFFF\x1\x239",
				"\x1\x23A\x1F\xFFFF\x1\x23A",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x23F\x1F\xFFFF\x1\x23F",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x241\x1F\xFFFF\x1\x241",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"\x1\x245\x1F\xFFFF\x1\x245",
				"\x1\x246\x1F\xFFFF\x1\x246",
				"",
				"\x1\x247",
				"\x1\x248\x1F\xFFFF\x1\x248",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x24A\x1F\xFFFF\x1\x24A",
				"\x1\x24B\x1F\xFFFF\x1\x24B",
				"",
				"",
				"\x1\x24C\x1F\xFFFF\x1\x24C",
				"",
				"",
				"\x1\x24D\x1F\xFFFF\x1\x24D",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"\x1\x250\x1F\xFFFF\x1\x250",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x252\x1F\xFFFF\x1\x252",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x254\x1F\xFFFF\x1\x254",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x258\x1F\xFFFF\x1\x258",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x25C\x1F\xFFFF\x1\x25C",
				"\x1\x25D\x1F\xFFFF\x1\x25D",
				"\x1\x25E\x1F\xFFFF\x1\x25E",
				"",
				"\x1\x25F\x1F\xFFFF\x1\x25F",
				"\x1\x260\x1F\xFFFF\x1\x260",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x263\x1F\xFFFF\x1\x263",
				"",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x265\x1F\xFFFF\x1\x265",
				"\x1\x267\xF\xFFFF\x1\x266\xF\xFFFF\x1\x267\xF\xFFFF\x1\x266",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x269\x1F\xFFFF\x1\x269",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x26C\x1F\xFFFF\x1\x26C",
				"",
				"",
				"\x1\x26D\x1F\xFFFF\x1\x26D",
				"",
				"\x1\x26E\x1F\xFFFF\x1\x26E",
				"",
				"\x1\x26F\x1F\xFFFF\x1\x26F",
				"",
				"",
				"",
				"\x1\x270\x1F\xFFFF\x1\x270",
				"",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x273\x1F\xFFFF\x1\x273",
				"\x1\x274\x1F\xFFFF\x1\x274",
				"\x1\x275\x1F\xFFFF\x1\x275",
				"",
				"",
				"\x1\x276\x1F\xFFFF\x1\x276",
				"",
				"\x1\x277\x1F\xFFFF\x1\x277",
				"\x1\x278\x1F\xFFFF\x1\x278",
				"\x1\x279\x1F\xFFFF\x1\x279",
				"",
				"\x1\x27A\x1F\xFFFF\x1\x27A",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x27F\x1F\xFFFF\x1\x27F",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x281\x1F\xFFFF\x1\x281",
				"\x1\x282\x1F\xFFFF\x1\x282",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x284\x1F\xFFFF\x1\x284",
				"\x1\x285\x1F\xFFFF\x1\x285",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x288\x1F\xFFFF\x1\x288",
				"\x1\x289\x1F\xFFFF\x1\x289",
				"",
				"\x1\x28A\x1F\xFFFF\x1\x28A",
				"\x1\x28B\x1F\xFFFF\x1\x28B",
				"",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"\x1\x28D\x1F\xFFFF\x1\x28D",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x12\x36\x1\x28F\x7\x36\x4\xFFFF\x1"+
				"\x36\x1\xFFFF\x12\x36\x1\x28F\x7\x36",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
				"",
				"\x1\x292\x1F\xFFFF\x1\x292",
				"",
				"",
				"\x1\x293\x1F\xFFFF\x1\x293",
				"\x1\x294\x1F\xFFFF\x1\x294",
				"\x1\x295\x1F\xFFFF\x1\x295",
				"\x1\x36\xB\xFFFF\xA\x36\x7\xFFFF\x1A\x36\x4\xFFFF\x1\x36\x1\xFFFF"+
				"\x1A\x36",
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

			public DFA28(BaseRecognizer recognizer, SpecialStateTransitionHandler specialStateTransition)
				: base(specialStateTransition)
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

			public override string Description { get { return "1:1: Tokens : ( EQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | BACKSLASH | PERCENT | SEMI | DOT | COMMA | LPAREN | RPAREN | QUESTION | COLON | AT | DOLLAR | QUOTE_DOUBLE | QUOTE_SINGLE | APOSTROPHE | LPAREN_SQUARE | RPAREN_SQUARE | UNDERSCORE | ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE | STRING | ID | INTEGER | FLOAT | BLOB | WS );"; } }

			public override void Error(NoViableAltException nvae)
			{
				DebugRecognitionException(nvae);
			}
		}

		private int SpecialStateTransition28(DFA dfa, int s, IIntStream _input)
		{
			IIntStream input = _input;
			int _s = s;
			switch (s)
			{
				case 0:
					int LA28_26 = input.LA(1);

					s = -1;
					if (((LA28_26 >= '\u0000' && LA28_26 <= 'Z') || (LA28_26 >= '\\' && LA28_26 <= '\uFFFF'))) { s = 54; }

					else s = 75;

					if (s >= 0) return s;
					break;
				case 1:
					int LA28_25 = input.LA(1);

					s = -1;
					if (((LA28_25 >= '\u0000' && LA28_25 <= '\uFFFF'))) { s = 54; }

					else s = 74;

					if (s >= 0) return s;
					break;
				case 2:
					int LA28_24 = input.LA(1);

					s = -1;
					if (((LA28_24 >= '\u0000' && LA28_24 <= '\uFFFF'))) { s = 72; }

					else s = 73;

					if (s >= 0) return s;
					break;
				case 3:
					int LA28_23 = input.LA(1);

					s = -1;
					if (((LA28_23 >= '\u0000' && LA28_23 <= '\uFFFF'))) { s = 72; }

					else s = 71;

					if (s >= 0) return s;
					break;
			}
			NoViableAltException nvae = new NoViableAltException(dfa.Description, 28, _s, input);
			dfa.Error(nvae);
			throw nvae;
		}

		#endregion

	}
}