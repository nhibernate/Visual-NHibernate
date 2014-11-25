using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Collections;
using System.Threading;

namespace ArchAngel.Providers
{
	public delegate void ProcedureFormattedEventHandler(string name, string formattedText, string originalText);
	public delegate void ErrorEventHandler(string fileName, string procedureName, string description, string originalText, int lineNumber, int startPos, int length);
	public delegate void FinishedEventHandler();

	/// <summary>
	/// All nodes - if needing to be on a newline - must add
	/// a newline at the BEGINNING, not at the end.
	/// </summary>
	public class SqlFormatter
	{
		#region Format Properties
		public bool MaintainCharacterCount	= false;
		public bool UseFullKeywords			= false;
		public string FormattedText			= "";
		public event ProcedureFormattedEventHandler FormatFinished;
		public event ErrorEventHandler RaiseError;
		public event FinishedEventHandler Finished;
		#endregion

		#region Class Variables
		public ArrayList FunctionTexts		= new ArrayList();
		public ArrayList FunctionNames		= new ArrayList();
		public ArrayList FunctionNamesLower	= new ArrayList();
		bool ProcessingElseIf				= false;
		private bool IsProcessingFunction	= false;
		private StringBuilder sb;
		int indentNum						= 0;//7;
		public bool IgnoreError				= true;
		string OpenBrace					= "{";
		string CloseBrace					= "}";
		private char Quote					= '"';
		private bool WriteToTemp			= false;
		private StringBuilder Temp;
		private ArrayList VariableNames		= new ArrayList();
		private bool MustInlineAccessorBody = false;
		private bool PutCommentOnNewLine	= true;
		private int lastCommentProcessed	= 0;
		private string CodeFile				= "";
		private string GrammarFile			= "";
		string TempFile						= System.IO.Path.GetTempFileName();
		private string CurrentProcedureName = "";
		private bool CurrentProcHasError	= false;
		private string CurrentOrigText		= "";
        public string GrammarFilePath = Path.Combine(Path.GetTempPath(), "TSQL_SP_Splitter.GMR");
		#endregion

		#region Constructors
		public SqlFormatter(string grammarFile, string codeFile)
		{
            throw new NotImplementedException("SQL Parser has not been fully tested yet.");
            CreateGrammarFile();
			parser = new PGMRX120Lib.PgmrClass();
			parser.SetGrammar(grammarFile);
			parser.SetInputFilename(codeFile);
			CodeFile	= codeFile;
			GrammarFile = grammarFile;
		}

		public SqlFormatter(string grammarFile)
		{
            throw new NotImplementedException("SQL Parser has not been fully tested yet.");
            CreateGrammarFile();
			parser = new PGMRX120Lib.PgmrClass();
			parser.SetGrammar(grammarFile);
		}

		~SqlFormatter()
		{
            //System.IO.Slyce.Common.Utility.DeleteFileBrute(TempFile);
		}

		#endregion

		#region Helper Functions
        public void CreateGrammarFile()
        {
            if (!File.Exists(GrammarFilePath))
            {
                using (System.IO.Stream normalStream = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.CodeProvider.Resources.TSQL_SP_Splitter.GMR")).BaseStream)
                {
                    if (normalStream == null)
                    {
                        throw new FileNotFoundException("SQL Grammar file cannot be read from embedded resources.");
                    }
                    FileStream streamWriter = File.Create(GrammarFilePath);
                    int size = 2048;
                    byte[] data = new byte[2048];

                    while (true)
                    {
                        size = normalStream.Read(data, 0, data.Length);

                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else { break; }
                    }
                    streamWriter.Close();
                    normalStream.Close();
                    File.SetLastWriteTime(GrammarFilePath, DateTime.Now);
                }
            }
        }

		/// <summary>
		/// Writes string out to storage: fi WriteToTemp = true then writes to
		/// sb, otherwise writes to Temp.
		/// </summary>
		/// <param name="text"></param>
		private void WriteString(string text)
		{
			if (!WriteToTemp) {sb.Append(text);}
			else {Temp.Append(text);}
		}

		/// <summary>
		/// Gets the full text from the current storage, either Temp or sb,
		/// depending on whether WriteToTemp is true or not.
		/// </summary>
		/// <returns></returns>
		private string ReadString()
		{
			if (WriteToTemp) {return Temp.ToString();}
			else {return sb.ToString();}
		}

		public void SetText(string text)
		{
			parser.SetInputString(text);
		}

		private string Indent
		{
			get
			{
				string retVal = "";

				for (int i = 0; i < indentNum; i++)
				{
					retVal += "\t";
				}
				return retVal;
			}
		}

		public void GetFormattedCode() 
		{
			Thread oThread = new Thread(new ThreadStart(GetFormattedCodeThreaded));
			oThread.Start();
		}

		private void GetFormattedCodeThreaded()
		{
            throw new NotImplementedException("TSQL grammar has not been saved as an embedded resource yet.");
			PGMRX120Lib.PgmrClass outerParser = new PGMRX120Lib.PgmrClass();
            outerParser.SetGrammar(GrammarFilePath);// (@"C:\Projects\CSFormatter\CSFormatter\SQLFormat\TSQL_SP_Splitter\TSQL_SP_Splitter.GMR");
			outerParser.SetInputFilename(CodeFile);

			// Get all individual SQL statements
			//int iiii = outerParser.GetInputLength();
//			StringBuilder sbTemp = new StringBuilder(outerParser.GetInputLength() + 100);
//
//			int pos = 0;
//			int testLength = outerParser.GetInputLength();
//
//			while (pos < testLength - 5000)
//			{
//				sbTemp.Append(outerParser.GetInputBuffer(pos, pos + 5000));
//				pos += 5000;
//			}
//			sbTemp.Append(outerParser.GetInputBuffer(pos, testLength - pos));

			//string originalText = sbTemp.ToString();// outerParser.GetInputBuffer(0, outerParser.GetInputLength() - 1);
			StringBuilder sbAll = new StringBuilder(outerParser.GetInputLength() + 100);
			IgnoreError			= false;
			PGMRX120Lib.PGStatus parseStatus = outerParser.Parse();

			switch (parseStatus)
			{
				case PGMRX120Lib.PGStatus.pgStatusBreak:
					break;
				case PGMRX120Lib.PGStatus.pgStatusComplete:
					break;
				case PGMRX120Lib.PGStatus.pgStatusError:
					string err = "";

					for (short i = 0; i < outerParser.GetNumErrors(); i++)
					{
						int error_code		= outerParser.GetErrorCode(i);
						err					+= outerParser.GetErrorDescription(i);
						string errDetails	= outerParser.GetErrorDetails(i);
					}
					MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case PGMRX120Lib.PGStatus.pgStatusExternal:
					break;
				case PGMRX120Lib.PGStatus.pgStatusUnknown:
					break;
			}
			// Get all function and procedure names
			int funcNameSearchId	= outerParser.StartSearch("start.sql_statement", 0);
			int funcNameIndex		= outerParser.FindNext(funcNameSearchId);
			
			while (funcNameIndex > 0)
			{
				string origProcText = outerParser.GetValue(funcNameIndex);
				string newProcText = GetFormattedCode(origProcText);

				if (!CurrentProcHasError)
				{
					FormatFinished(CurrentProcedureName, newProcText, origProcText);
				}
				sbAll.Append(newProcText);
				funcNameIndex		= outerParser.FindNext(funcNameSearchId);
//				FunctionNames.Add(parser.GetValue(funcNameIndex));
//				FunctionNamesLower.Add(parser.GetValue(funcNameIndex).ToLower());
//				funcNameIndex = parser.FindNext(funcNameSearchId);
			}
//			funcNameSearchId	= parser.StartSearch("program.ws_block.ws_compound_statement.statement.simple_statement.decl_section.proc_func_decl_part.proc_decl.proc_heading.pascal_identifier", 0);
//			funcNameIndex		= parser.FindNext(funcNameSearchId);
//			
//			while (funcNameIndex > 0)
//			{
//				FunctionNames.Add(parser.GetValue(funcNameIndex));
//				FunctionNamesLower.Add(parser.GetValue(funcNameIndex).ToLower());
//				funcNameIndex = parser.FindNext(funcNameSearchId);
//			}
//			FunctionNames.Sort();
//			FunctionNamesLower.Sort();
			Finished();
			FormattedText = sbAll.ToString();
			//return sbAll.ToString();
		}

		public string GetFormattedCode(string textToParse)
		{
			CurrentOrigText = textToParse;
				parser = new PGMRX120Lib.PgmrClass();
				parser.SetGrammar(GrammarFile);
				//parser.SetInputString(textToParse);
			
				using (System.IO.TextWriter tw = new System.IO.StreamWriter(TempFile, false))
				{
					tw.Write(textToParse);
					tw.Flush();
					tw.Close();
				}
				parser.SetInputFilename(TempFile);
			
				IgnoreError = false;
				PGMRX120Lib.PGStatus parseStatus = parser.Parse();
                //Slyce.Common.Utility.DeleteFileBrute(tempFile);

//			if (parser.GetNumErrors() > 0)
//			{
//				string err = "";
//
//				for (short i = 0; i < parser.GetNumErrors(); i++)
//				{
//					int error_code = parser.GetErrorCode(i);
//					err += parser.GetErrorDescription(i);
//					string errDetails = parser.GetErrorDetails(i);
//				}
//				RaiseError(CurrentProcedureName, "Parse Error", CurrentOrigText, parser.getin.GetErrorDetail(). lineNum, startPos, numChars);
//				return "";
//			}

				switch (parseStatus)
				{
					case PGMRX120Lib.PGStatus.pgStatusBreak:
						break;
					case PGMRX120Lib.PGStatus.pgStatusComplete:
						break;
					case PGMRX120Lib.PGStatus.pgStatusError:
						string err = "";

						for (short i = 0; i < parser.GetNumErrors(); i++)
						{
							int error_code = parser.GetErrorCode(i);
							err += parser.GetErrorDescription(i);
							string errDetails = parser.GetErrorDetails(i);
						}
						MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					case PGMRX120Lib.PGStatus.pgStatusExternal:
						break;
					case PGMRX120Lib.PGStatus.pgStatusUnknown:
						break;
				}
				//			// Get all function and procedure names
				//			int funcNameSearchId	= parser.StartSearch("program.ws_block.ws_compound_statement.statement.simple_statement.decl_section.proc_func_decl_part.func_decl.func_heading.pascal_identifier", 0);
				//			int funcNameIndex		= parser.FindNext(funcNameSearchId);
				//
				//			while (funcNameIndex > 0)
				//			{
				//				FunctionNames.Add(parser.GetValue(funcNameIndex));
				//				FunctionNamesLower.Add(parser.GetValue(funcNameIndex).ToLower());
				//				funcNameIndex = parser.FindNext(funcNameSearchId);
				//			}
				//			funcNameSearchId	= parser.StartSearch("program.ws_block.ws_compound_statement.statement.simple_statement.decl_section.proc_func_decl_part.proc_decl.proc_heading.pascal_identifier", 0);
				//			funcNameIndex		= parser.FindNext(funcNameSearchId);
				//
				//			while (funcNameIndex > 0)
				//			{
				//				FunctionNames.Add(parser.GetValue(funcNameIndex));
				//				FunctionNamesLower.Add(parser.GetValue(funcNameIndex).ToLower());
				//				funcNameIndex = parser.FindNext(funcNameSearchId);
				//			}
				//			FunctionNames.Sort();
				//			FunctionNamesLower.Sort();

			// Get the name of the procedure
			int funcNameSearchId	= parser.StartSearch("start.sql_statement.create_procedure.procedure_name", 0);
			int funcNameIndex		= parser.FindNext(funcNameSearchId);

			if (funcNameIndex > 0)
			{
				CurrentProcedureName = parser.GetValue(funcNameIndex);
			}
			else
			{
				int endPos = Math.Max(0, textToParse.IndexOf("\n"));
				
				if (endPos == 0) {endPos = textToParse.Length;}

				CurrentProcedureName = "Missing. ("+ textToParse.Substring(0, endPos) +")";
			}
				int codeLength		= parser.GetInputLength();
				sb					= new StringBuilder(codeLength + 100);
				int searchId		= parser.StartSearch("start", 0);
				int nodeIndex		= parser.FindNext(searchId);
			
				try
				{
					ProcessNode(nodeIndex);
					nodeIndex = parser.GetNextSibling(nodeIndex);

					while (nodeIndex > 0)
					{
						ProcessNode(nodeIndex);
						nodeIndex = parser.GetNextSibling(nodeIndex);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					//Singleton.Instance.AddError("(Parser error) "+ ex.Message);
				}
				for (int i = 0; i < parser.GetNumErrors(); i++)
				{
					//Singleton.Instance.AddError("(Syntax Error) "+ parser.GetErrorDescription((short)i)); 
				}
				return sb.ToString();
		}

		private void ProcessChildNodes(int nodeId)
		{
			int numChildren = parser.GetNumChildren(nodeId);

			for (int i = 0; i < numChildren; i++)
			{
				ProcessNode(parser.GetChild(nodeId, i));
			}
		}

		/// <summary>
		/// Gets the child nodes which are not expected and have not been catered for yet.
		/// </summary>
		/// <param name="nodeId"></param>
		/// <param name="expectedNodes"></param>
		/// <returns></returns>
		private string[] GetUnexpectedNodes(int nodeId, string[] expectedNodes)
		{
			if (ChildNodeExists(nodeId, "space_symbol"))
			{
				ProcessChildNode(nodeId, "space_symbol");
				string[] temp = new string[expectedNodes.Length + 1];
				Array.Copy(expectedNodes, 0, temp, 0, expectedNodes.Length);
				expectedNodes = temp;
				expectedNodes[expectedNodes.Length - 1] = "space_symbol";
			}
			//			if (ChildNodeExists(nodeId, "region_start_directive"))
			//			{
			//				ProcessChildNode(nodeId, "region_start_directive");
			//				string[] temp = new string[expectedNodes.Length + 1];
			//				Array.Copy(expectedNodes, 0, temp, 0, expectedNodes.Length);
			//				expectedNodes = temp;
			//				expectedNodes[expectedNodes.Length - 1] = "region_start_directive";
			//			}
			//			if (ChildNodeExists(nodeId, "region_end_directive"))
			//			{
			//				ProcessChildNode(nodeId, "region_end_directive");
			//				string[] temp = new string[expectedNodes.Length + 1];
			//				Array.Copy(expectedNodes, 0, temp, 0, expectedNodes.Length);
			//				expectedNodes = temp;
			//				expectedNodes[expectedNodes.Length - 1] = "region_end_directive";
			//			}
			int numChildren = parser.GetNumChildren(nodeId);

			//			if (expectedNodes.Length > numChildren)
			//			{
			//				return new string[0];
			//			}
			ArrayList unexpectedNodes = new ArrayList();
			Array.Sort(expectedNodes);
			int pos = 0;
			
			for (int i = 0; i < numChildren; i++)
			{
				string name = parser.GetLabel(parser.GetChild(nodeId, i));

				if (Array.BinarySearch(expectedNodes, name) < 0)
				{
					unexpectedNodes.Add(name);
					unexpectedNodes.Sort();
					pos++;
				}
			}
			if (unexpectedNodes.Count > 0)
			{
				string allNames = "";

				for (int i = 0; i < unexpectedNodes.Count; i++)
				{
					allNames += (string)unexpectedNodes[i] +", ";
				}
				//MessageBox.Show("Unexpected nodes for "+ parser.GetLabel(nodeId) +": "+ allNames.Substring(0, allNames.Length - 2));
				throw new Exception("Unexpected nodes for "+ parser.GetLabel(nodeId) +": "+ allNames.Substring(0, allNames.Length - 2));
			}
			return (string[])unexpectedNodes.ToArray(typeof(string));
		}

		private int ProcessChildNode(int parentNodeId, string childNodeName)
		{
			return ProcessChildNode(parentNodeId, childNodeName, -1);
		}

		/// <summary>
		/// Processes the next child token.
		/// </summary>
		/// <param name="parentNodeId"></param>
		/// <param name="childNodeName"></param>
		/// <returns>Returns the search index if a match was found, -1 otherwise.</returns>
		private int ProcessChildNode(int parentNodeId, string childNodeName, int searchIndex)
		{
			int childSearchId = -1;
			int childNodeIndex;

			if (searchIndex < 0)
			{
				childSearchId	= parser.StartSearch("."+ childNodeName, parentNodeId);
				childNodeIndex	= parser.FindNext(childSearchId);
			}
			else
			{
				throw new Exception("oops");
			}
			if (childSearchId > 0)
			{
				ProcessNode(childNodeIndex);
				return childSearchId;
			}
			return -1;
		}

		private bool ChildNodeExists(int parentNodeId, string childNodeName)
		{
			int index;
			return ChildNodeExists(parentNodeId, childNodeName, out index);
		}

		private bool ChildNodeExists(int parentNodeId, string childNodeName, out int index)
		{
			index = parser.FindNext(parser.StartSearch("."+ childNodeName, parentNodeId));
			return index > 0;
		}
	
		/// <summary>
		/// Gets the label of the previous sibling that is not a space_symbol.
		/// </summary>
		/// <param name="nodeIndex"></param>
		/// <returns></returns>
		private string GetPrevNonSpaceLabel(int nodeIndex)
		{
			int prevSibling		= parser.GetPrevSibling(nodeIndex);
			string prevLabel	= parser.GetLabel(prevSibling);

			while (prevSibling > 0 &&
				prevLabel == "space_symbol")
			{
				prevSibling = parser.GetPrevSibling(prevSibling);
				prevLabel	= parser.GetLabel(prevSibling);
			}
			if (prevLabel == "space_symbol")
			{
				prevLabel = "";
			}
			return prevLabel;
		}

		/// <summary>
		/// Gets the label of the previous sibling that is not a space_symbol.
		/// </summary>
		/// <param name="nodeIndex"></param>
		/// <returns></returns>
		private string GetNextNonSpaceLabel(int nodeIndex)
		{
			int nextSibling		= parser.GetNextSibling(nodeIndex);
			string nextLabel	= parser.GetLabel(nextSibling);

			while (nextSibling > 0 &&
				nextLabel == "space_symbol")
			{
				nextSibling = parser.GetNextSibling(nextSibling);
				nextLabel	= parser.GetLabel(nextSibling);
			}
			if (nextLabel == "space_symbol")
			{
				nextLabel = "";
			}
			return nextLabel;
		}

		#endregion

		/// <summary>
		/// Processes the token node.
		/// </summary>
		/// <param name="nodeIndex"></param>
		public void ProcessNode(int nodeIndex)
		{
			string nodeName = parser.GetLabel(nodeIndex);
			int siblingIndex = 0;

			if (nodeName != "space_symbol")
			{
				siblingIndex = parser.GetPrevSibling(nodeIndex);

				if (siblingIndex > lastCommentProcessed &&
					parser.GetLabel(siblingIndex) == "space_symbol")
				{
					ProcessNode(siblingIndex);
					lastCommentProcessed = siblingIndex;
				}
			}
			switch (nodeName)
			{
				case "start":
					ProcessChildNodes(nodeIndex);
					break;
				case "sql_statement":
					Process_sql_statement(nodeIndex);
					break;
				case "create_procedure":
					Process_create_procedure(nodeIndex);
					break;
				case "proc_keyword":
					Process_proc_keyword(nodeIndex);
					break;
				case "procedure_name":
					Process_procedure_name(nodeIndex);
					break;
				case "number":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "open_parenthesis":
					WriteString("(");
					break;
				case "close_parenthesis":
					WriteString(")");
					break;
				case "procedure_option":
					Process_procedure_option(nodeIndex);
					break;
				case "procedure_body":
					Process_procedure_body(nodeIndex);
					break;
				case "procedure_param_list":
					Process_procedure_param_list(nodeIndex);
					break;
				case "procedure_param":
					Process_procedure_param(nodeIndex);
					break;
				case "parameter":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "data_type":
					Process_data_type(nodeIndex);
					break;
				case "varying_tag":
					WriteString("VARYING");
					break;
				case "param_init_value":
					ProcessChildNodes(nodeIndex);
					break;
				case "output_tag":
					Process_output_tag(nodeIndex);
					break;
				case "literal":
					WriteString(parser.GetValue(nodeIndex));
					// Might need to further refine the processing of this token
					//Process_literal(nodeIndex);
					break;
				case "null_tag":
					WriteString("NULL");
					break;
				case "default_name":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "name":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "numeric":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "select":
					ProcessChildNodes(nodeIndex);
					break;
				case "query_expression":
					Process_query_expression(nodeIndex);
					break;
				case "query_specification":
					Process_query_specification(nodeIndex);
					break;
				case "select_list":
					Process_select_list(nodeIndex);
					break;
				case "select_item":
					Process_select_item(nodeIndex);
					break;
				case "as_keyword":
					WriteString(" AS ");
					break;
				case "equal_keyword":
					WriteString(" = ");
					break;
				case "column_alias":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "into_keyword":
					WriteString(" INTO ");
					break;
				case "all_or_distinct":
					WriteString(parser.GetValue(nodeIndex).ToUpper());
					break;
				case "expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "scalar_exp":
					ProcessChildNodes(nodeIndex);
					break;
				case "scalar_term":
					ProcessChildNodes(nodeIndex);
					break;
				case "scalar_factor":
					ProcessChildNodes(nodeIndex);
					break;
				case "scalar_factor_op":
					ProcessChildNodes(nodeIndex);
					break;
				case "column_ref":
					Process_column_ref(nodeIndex);
					break;
				case "period_keyword":
					WriteString(".");
					break;
				case "identity_col_keyword":
					WriteString("IDENTITYCOL");
					break;
				case "wildcard":
					WriteString("*");
					break;
				case "table_exp":
					Process_table_exp(nodeIndex);
					break;
				case "table_source":
					Process_table_source(nodeIndex);
					break;
				case "table":
					ProcessChildNodes(nodeIndex);
					break;
				case "qualified_name":
					Process_qualified_name(nodeIndex);
					break;
				case "with_keyword":
					WriteString(" WITH ");
					break;
				case "table_hint":
					WriteString(parser.GetValue(nodeIndex).ToUpper());
					break;
				case "where_clause":
					Process_where_clause(nodeIndex);
					break;
				case "search_condition":
					Process_search_condition(nodeIndex);
					break;
				case "search_term":
					Process_search_term(nodeIndex);
					break;
				case "search_factor":
					ProcessChildNodes(nodeIndex);
					break;
				case "not_tag":
					WriteString("NOT");
					break;
				case "predicate":
					ProcessChildNodes(nodeIndex);
					break;
				case "relational_exp":
					ProcessChildNodes(nodeIndex);
					break;
				case "relational_value":
					ProcessChildNodes(nodeIndex);
					break;
				case "comparison_operator":
					WriteString(" "+ parser.GetValue(nodeIndex) +" ");
					break;
				case "atom":
					Process_atom(nodeIndex);
					break;
				case "variable":
					WriteString(" "+ parser.GetValue(nodeIndex) +" ");
					break;
				case "create_table":
					Process_create_table(nodeIndex);
					break;
				case "default_keyword":
					WriteString(" DEFAULT");
					break;
				case "on_keyword":
					WriteString(" ON");
					break;
				case "textimage_on_keyword":
					WriteString(" TEXTIMAGE_ON");
					break;
				case "table_type_definition":
					Process_table_type_definition(nodeIndex);
					break;
				case "table_type_definition_term":
					ProcessChildNodes(nodeIndex);
					break;
				case "column_definition":
					Process_column_definition(nodeIndex);
					break;
				case "timestamp_keyword":
					WriteString("TIMESTAMP");
					break;
				case "column_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "column_def_qualifier":
					Process_column_def_qualifier(nodeIndex);
					break;
				case "values_keyword":
					WriteString(" VALUES");
					break;
				case "seed":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "increment":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "column_constraint":
					Process_column_constraint(nodeIndex);
					break;
				case "primary_key_keyword":
					WriteString("PRIMARY KEY");
					break;
				case "foreign_key_keyword":
					WriteString("FOREIGN KEY");
					break;
				case "fillfactor_keyword":
					WriteString("FILLFACTOR");
					break;
				case "check_keyword":
					WriteString("CHECK");
					break;
				case "references_keyword":
					WriteString("REFERENCES");
					break;
				case "unique_keyword":
					WriteString("UNIQUE");
					break;
				case "clustered_keyword":
					WriteString("CLUSTERED");
					break;
				case "non_clustered_keyword":
					WriteString("NONCLUSTERED");
					break;
				case "set":
					Process_set(nodeIndex);
					break;
				case "tsql_identifier":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "on_off_flag":
					WriteString(parser.GetValue(nodeIndex).ToUpper());
					break;
				case "go":
					WriteString(string.Format("GO", Environment.NewLine, Indent));
					break;
				case "case_function":
					Process_case_function(nodeIndex);
					break;
				case "when_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "result_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "else_result_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "string_expr":
					Process_string_expr(nodeIndex);
					break;
				case "unary_op":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "order_by_clause":
					Process_order_by_clause(nodeIndex);
					break;
				case "order_by_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "column_position":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "ascending_or_descending":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "function_ref":
					Process_function_ref(nodeIndex);
					break;
				case "sql_function_name":
					WriteString(parser.GetValue(nodeIndex).ToUpper());
					break;
				case "statement_block":
					Process_statement_block(nodeIndex);
					break;
				case "declare":
					Process_declare(nodeIndex);
					break;
				case "local_variable":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "print":
					Process_print(nodeIndex);
					break;
				case "printable_value":
					ProcessChildNodes(nodeIndex);
					break;
				case "scalar_term_op":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "__cast_function":
					Process_cast_function(nodeIndex);
					break;
				case "function_name":
					Process_function_name(nodeIndex);
					break;
				case "select_variable":
					Process_select_variable(nodeIndex);
					break;
				case "new_table":
					ProcessChildNodes(nodeIndex);
					break;
				case "relational_predicate":
					ProcessChildNodes(nodeIndex);
					break;
				case "between_predicate":
					Process_between_predicate(nodeIndex);
					break;
				case "scalar1":
					ProcessChildNodes(nodeIndex);
					break;
				case "scalar2":
					ProcessChildNodes(nodeIndex);
					break;
				case "like_predicate":
					Process_like_predicate(nodeIndex);
					break;
				case "escape":
					Process_escape(nodeIndex);
					break;
				case "in_predicate":
					Process_in_predicate(nodeIndex);
					break;
				case "atom_list":
					Process_atom_list(nodeIndex);
					break;
				case "while":
					Process_while(nodeIndex);
					break;
				case "boolean_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "if_else_statement":
					Process_if_else_statement(nodeIndex);
					break;
				case "truncate_table":
					Process_truncate_table(nodeIndex);
					break;
				case "insert":
					Process_insert(nodeIndex);
					break;
				case "column":
					ProcessChildNodes(nodeIndex);
					break;
				case "derived_table":
					ProcessChildNodes(nodeIndex);
					break;
				case "table_alias":
					ProcessChildNodes(nodeIndex);
					break;
				case "execute":
					Process_execute(nodeIndex);
					break;
				case "delete":
					Process_delete(nodeIndex);
					break;
				case "update":
					Process_update(nodeIndex);
					break;
				case "statement_selection_clause":
					Process_statement_selection_clause(nodeIndex);
					break;
				case "is_keyword":
					WriteString("IS");
					break;
				case "declare_cursor":
					Process_declare_cursor(nodeIndex);
					break;
				case "cursor_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "scroll_keyword":
					WriteString("SCROLL");
					break;
				case "insensitive_keyword":
					WriteString("INSENSITIVE");
					break;
				case "read_only_keyword":
					WriteString("READ ONLY");
					break;
				case "update_keyword":
					WriteString("UPDATE");
					break;
				case "cursor_keyword":
					WriteString("CURSOR");
					break;
				case "open":
					Process_open(nodeIndex);
					break;
				case "fetch":
					Process_fetch(nodeIndex);
					break;
				case "variable_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "at_variable":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "next_keyword":
					WriteString("NEXT");
					break;
				case "prior_keyword":
					WriteString("PRIOR");
					break;
				case "first_keyword":
					WriteString("FIRST");
					break;
				case "last_keyword":
					WriteString("LAST");
					break;
				case "fetch_record_spec":
					Process_fetch_record_spec(nodeIndex);
					break;
				case "from_keyword":
					WriteString("FROM");
					break;
				case "close":
					Process_close(nodeIndex);
					break;
				case "deallocate":
					Process_deallocate(nodeIndex);
					break;
				case "return":
					Process_return(nodeIndex);
					break;
				case "integer_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "test_for_null":
					Process_test_for_null(nodeIndex);
					break;
				case "drop_table":
					Process_drop_table(nodeIndex);
					break;
				case "select_top_clause":
					Process_select_top_clause(nodeIndex);
					break;
				case "integer":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "subquery":
					ProcessChildNodes(nodeIndex);
					break;
				case "break":
					WriteString("BREAK");
					break;
				case "set_transaction_level":
					Process_set_transaction_level(nodeIndex);
					break;
				case "transaction_levels":
					Process_transaction_levels(nodeIndex);
					break;
				case "__cursor_decl_spec":
					Process_cursor_decl_spec(nodeIndex);
					break;
				case "type_warning_keyword":
					WriteString("TYPE_WARNING");
					break;
				case "optimistic_keyword":
					WriteString("OPTIMISTIC");
					break;
				case "scroll_locks_keyword":
					WriteString("SCROLL_LOCKS");
					break;
				case "fast_forward_keyword":
					WriteString("FAST_FORWARD");
					break;
				case "static_keyword":
					WriteString("STATIC");
					break;
				case "keyset_keyword":
					WriteString("KEYSET");
					break;
				case "dynamic_keyword":
					WriteString("DYNAMIC");
					break;
				case "local_keyword":
					WriteString("LOCAL");
					break;
				case "forward_only_keyword":
					WriteString("FORWARD_ONLY");
					break;
				case "group_by_keyword":
					WriteString("GROUP BY");
					break;
				case "group_by_expression":
					ProcessChildNodes(nodeIndex);
					break;
				case "create_index":
					Process_create_index(nodeIndex);
					break;
				case "index_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "filegroup":
					Process_filegroup(nodeIndex);
					break;
				case "filegroup_name":
					Process_filegroup_name(nodeIndex);
					break;
				case "set_statistics_profile":
					Process_set_statistics_profile(nodeIndex);
					break;
				case "existence_test":
					Process_existence_test(nodeIndex);
					break;
				case "grant":
					Process_grant(nodeIndex);
					break;
				case "permission":
					WriteString(parser.GetValue(nodeIndex).ToUpper());
					break;
				case "to_keyword":
					WriteString("TO");
					break;
				case "with_grant_option_keyword":
					WriteString("WITH GRANT OPTION");
					break;
				case "privileges_keyword":
					WriteString("PRIVILEGES");
					break;
				case "security_account":
					Process_security_account(nodeIndex);
					break;
				case "distinct_literal":
					WriteString("DISTINCT");
					break;
				case "having_clause":
					Process_having_clause(nodeIndex);
					break;
				case "begin_transaction":
					Process_begin_transaction(nodeIndex);
					break;
				case "commit_transaction":
					Process_commit_transaction(nodeIndex);
					break;
				case "rollback_transaction":
					Process_rollback_transaction(nodeIndex);
					break;
				case "transaction_keyword":
					Process_transaction_keyword(nodeIndex);
					break;
				case "transaction_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "tran_name_variable":
					WriteString("@");
					ProcessChildNodes(nodeIndex);
					break;
				case "label":
					ProcessChildNodes(nodeIndex);
					WriteString(":");
					break;
				case "label_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "goto":
					WriteString("GOTO ");
					ProcessChildNodes(nodeIndex);
					break;
				case "raiserror":
					Process_raiserror(nodeIndex);
					break;
				case "savepoint_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "savepoint_variable":
					ProcessChildNodes(nodeIndex);
					break;
				case "identifier":
					WriteString(parser.GetValue(nodeIndex));
					break;
				case "return_status":
					ProcessChildNodes(nodeIndex);
					break;
				case "alter_table":
					Process_alter_table(nodeIndex);
					break;
				case "comma_keyword":
					WriteString(",");
					break;
				case "add_keyword":
					WriteString("ADD");
					break;
				case "drop_keyword":
					WriteString("DROP");
					break;
				case "constraint_name":
					ProcessChildNodes(nodeIndex);
					break;
				case "column_keyword":
					WriteString("COLUMN");
					break;
				case "space_symbol":
					Process_space_symbol(nodeIndex);
					break;
				case "sql_comment":
					Process_sql_comment(nodeIndex);
					break;
				case "c_comment":
					Process_c_comment(nodeIndex);
					break;













				default:
					if (!IgnoreError && nodeName.Length > 0)
					{
						if (nodeName == "Error")
						{
							CurrentProcHasError = true;
							int lineNum = -1;
							int startPos = -1;
							int numChars = -1;
							int childIndex;
 
							if (ChildNodeExists(nodeIndex, "LineNumber", out childIndex))
							{
								lineNum	= int.Parse(parser.GetValue(childIndex));
							}
							if (ChildNodeExists(nodeIndex, "StartPos", out childIndex))
							{
								startPos = int.Parse(parser.GetValue(childIndex));
							}
							if (ChildNodeExists(nodeIndex, "NumChars", out childIndex))
							{
								numChars = int.Parse(parser.GetValue(childIndex));
							}
							RaiseError(CodeFile, CurrentProcedureName, "Parse Error", CurrentOrigText, lineNum, startPos, numChars);
						}
						else
						{
							CurrentProcHasError = true;
                            RaiseError(CodeFile, CurrentProcedureName, "Not implemented yet: " + nodeName, "", 1, 1, 1);
							//throw new Exception("Not implemented yet: "+ nodeName);
						}
					}
					break;
			}
//			siblingIndex = parser.GetNextSibling(nodeIndex);
//
//			if (siblingIndex > lastCommentProcessed &&
//				parser.GetLabel(siblingIndex) == "space_symbol")
//			{
//				ProcessNode(siblingIndex);
//			}

		}

		public void Process_space_symbol(int nodeIndex)
		{
			if (nodeIndex > lastCommentProcessed)
			{
				lastCommentProcessed = nodeIndex;
				ProcessChildNodes(nodeIndex);
			}
		}

		public void Process_create_procedure(int nodeIndex)
		{
			WriteString("CREATE ");
			ProcessChildNode(nodeIndex, "proc_keyword");
			WriteString(" ");
			ProcessChildNode(nodeIndex, "procedure_name");

			if (ChildNodeExists(nodeIndex, "number"))
			{
				WriteString(";");
				ProcessChildNode(nodeIndex, "number");
			}
			WriteString(Environment.NewLine + Indent);

			if (MaintainCharacterCount)
			{
				ProcessChildNode(nodeIndex, "open_parenthesis");
			}
			else
			{
				WriteString("(");
			}
			if (ChildNodeExists(nodeIndex, "procedure_param_list"))
			{
				indentNum++;
				WriteString(Environment.NewLine + Indent);
				ProcessChildNode(nodeIndex, "procedure_param_list");
				indentNum--;
				WriteString(Environment.NewLine + Indent);
			}
			if (MaintainCharacterCount)
			{
				ProcessChildNode(nodeIndex, "close_parenthesis");
			}
			else
			{
				WriteString(")");
			}
			ProcessChildNode(nodeIndex, "procedure_option");
			WriteString(string.Format("{0}{1}AS", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "procedure_body");
		}

		public void Process_proc_keyword(int nodeIndex)
		{
			if (MaintainCharacterCount)
			{
				WriteString(parser.GetValue(nodeIndex));
			}
			else if (UseFullKeywords)
			{
				WriteString("PROCEDURE");
			}
			else
			{
				WriteString("PROC");
			}
		}

		public void Process_output_tag(int nodeIndex)
		{
			if (MaintainCharacterCount)
			{
				WriteString(parser.GetValue(nodeIndex));
			}
			else if (UseFullKeywords)
			{
				WriteString("OUTPUT");
			}
			else
			{
				WriteString("OUT");
			}
		}

		public void Process_procedure_name(int nodeIndex)
		{
			WriteString(parser.GetValue(nodeIndex));
		}

		public void Process_procedure_option(int nodeIndex)
		{
			//WriteString(parser.GetValue(nodeIndex));
		}

		public void Process_procedure_body(int nodeIndex)
		{
			if (parser.GetNumChildren(nodeIndex) > 0 &&
				parser.GetLabel(parser.GetChild(nodeIndex, 0)) == "sql_statement")
			{
				WriteString(Environment.NewLine + Indent);
			}
			ProcessChildNodes(nodeIndex);
		}

		public void Process_procedure_param_list(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".procedure_param", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(", " + Environment.NewLine + Indent);
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
		}

		public void Process_procedure_param(int nodeIndex)
		{
			ProcessChildNode(nodeIndex, "parameter");
			WriteString(" ");

			if (ChildNodeExists(nodeIndex, "as_keyword"))
			{
				WriteString("AS ");
			}
			ProcessChildNode(nodeIndex, "data_type");
			WriteString(" ");
			ProcessChildNode(nodeIndex, "varying_tag");

			if (ChildNodeExists(nodeIndex, "param_init_value"))
			{
				WriteString("= ");
				ProcessChildNode(nodeIndex, "param_init_value");
			}
			ProcessChildNode(nodeIndex, "output_tag");
		}

		public void Process_data_type(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "cursor_keyword"))
			{
				WriteString("CURSOR ");
			}
			else
			{
				int childIndex;

				if (ChildNodeExists(nodeIndex, "name", out childIndex))
				{
					string val = parser.GetValue(childIndex);

					switch (val.ToUpper())
					{
						case "INT":
						case "INTEGER":
							if (MaintainCharacterCount)
							{
								WriteString(val.ToUpper());
							}
							else
							{
								WriteString("INTEGER");
							}
							break;
						case "BIGINT":
						case "SMALLINT":
						case "TINYINT":
						case "BIT":
						case "DECIMAL":
						case "NUMERIC":
						case "MONEY":
						case "SMALLMONEY":
						case "FLOAT":
						case "REAL":
						case "DATETIME":
						case "SMALLDATETIME":
						case "CHAR":
						case "VARCHAR":
						case "TEXT":
						case "NCHAR":
						case "NVARCHAR":
						case "NTEXT":
						case "BINARY":
						case "VARBINARY":
						case "IMAGE":
							WriteString(val.ToUpper());
							break;
						default:
							ProcessNode(childIndex);
							break;
					}
				}
				//ProcessChildNode(nodeIndex, "name");

				if (ChildNodeExists(nodeIndex, "numeric"))
				{
					WriteString("(");
					int newSearchId	= parser.StartSearch(".numeric", nodeIndex);
					int newIndex	= parser.FindNext(newSearchId);
					int counter		= 0;

					while (newIndex > 0)
					{
						if (counter > 0)
						{
							WriteString(", ");
						}
						ProcessNode(newIndex);
						newIndex = parser.FindNext(newSearchId);
						counter++;
					}
					WriteString(")");
				}
			}
		}

		public void Process_select_list(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".select_item", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;
			int numChildren = parser.GetNumChildren(nodeIndex);
			indentNum++;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(", ");
				}
				if (numChildren > 1)
				{
					WriteString(Environment.NewLine + Indent);
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
			indentNum--;
		}

		public void Process_select_item(int nodeIndex)
		{
			ProcessChildNodes(nodeIndex);
//			if (ChildNodeExists(nodeIndex, "asterisk_keyword"))
//			{
//				ProcessChildNode(nodeIndex, "asterisk_keyword");
//			}
//			else if (ChildNodeExists(nodeIndex, "rowguidcol_keyword"))
//			{
//				WriteString("ROWGUIDCOL");
//
//				if (ChildNodeExists(nodeIndex, "equal_keyword"))
//				{
//					WriteString("=");
//					ProcessChildNode(nodeIndex, "expression");
//					ProcessChildNode(nodeIndex, "as_keyword");
//					ProcessChildNode(nodeIndex, "column_alias");
//				}
//			}
//			else // There could be two expression tokens, one on each side of an equal sign
//			{
//				int newSearchId	= parser.StartSearch(".expression", nodeIndex);
//				int newIndex	= parser.FindNext(newSearchId);
//				int counter		= 0;
//
//				while (newIndex > 0)
//				{
//					if (counter > 0)
//					{
//						WriteString(" = ");
//					}
//					ProcessNode(newIndex);
//					newIndex = parser.FindNext(newSearchId);
//					counter++;
//				}
//			}
			
		}

		public void Process_query_specification(int nodeIndex)
		{
			if (parser.GetLabel(parser.GetPrevSibling(parser.GetParent(parser.GetParent(parser.GetParent(nodeIndex))))) == "sql_statement")
			{
				WriteString(Environment.NewLine + Indent);
			}
			WriteString("SELECT ");
			ProcessChildNodes(nodeIndex);
		}

		public void Process_column_ref(int nodeIndex)
		{
			ProcessChildNodes(nodeIndex);
//			if (ChildNodeExists(nodeIndex, "name"))
//			{
//				if (ChildNodeExists(nodeIndex, "period_keyword"))
//				{
//					WriteString(".");
//				}
//				else
//				{
//					WriteString(" ");
//				}
//			}
		}

		public void Process_table_exp(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".table_source", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;

			while (newIndex > 0)
			{
				if (counter == 0)
				{
					WriteString(string.Format("{0}{1}FROM ", Environment.NewLine, Indent));
				}
				if (counter > 0)
				{
					WriteString(", ");
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
			ProcessChildNode(nodeIndex, "where_clause");

			if (ChildNodeExists(nodeIndex, "group_by_keyword"))
			{
				ProcessChildNode(nodeIndex, "group_by_keyword");
				ProcessChildNode(nodeIndex, "all_keyword");
			
				newSearchId	= parser.StartSearch(".group_by_expression", nodeIndex);
				newIndex	= parser.FindNext(newSearchId);
				counter		= 0;

				while (newIndex > 0)
				{
					if (counter > 0)
					{
						WriteString(", ");
					}
					ProcessNode(newIndex);
					newIndex = parser.FindNext(newSearchId);
					counter++;
				}
				if (ChildNodeExists(nodeIndex, "cube_rollup_keyword"))
				{
					WriteString(" WITH ");
					ProcessChildNode(nodeIndex, "cube_rollup_keyword");
				}
			}
			ProcessChildNode(nodeIndex, "having_clause");
		}

		public void Process_table_source(int nodeIndex)
		{
			int tableIndex = 0;

			if (ChildNodeExists(nodeIndex, "table", out tableIndex))
			{
				ProcessChildNode(nodeIndex, "table");
				int nextSibling = parser.GetNextSibling(tableIndex);

				if (parser.GetLabel(nextSibling) == "table_hint")
				{
					WriteString("(");
					ProcessNode(nextSibling);
					WriteString(")");
				}
				ProcessChildNode(nodeIndex, "as_keyword");
				ProcessChildNode(nodeIndex, "table_alias");
				ProcessChildNode(nodeIndex, "with_keyword");

				nextSibling		= parser.GetNextSibling(nextSibling);
				int hintCounter = 0;

				while (nextSibling > 0)
				{
					if (parser.GetLabel(nextSibling) == "table_hint")
					{
						if (hintCounter == 0)
						{
							WriteString("(");
						}
						else
						{
							WriteString(", ");
						}
					
						ProcessNode(nextSibling);
					}
					hintCounter++;
					nextSibling = parser.GetNextSibling(nextSibling);
				}
				if (hintCounter > 0)
				{
					WriteString(")");
				}
			}
			else if (ChildNodeExists(nodeIndex, "rowset_function", out tableIndex))
			{
				ProcessChildNode(nodeIndex, "rowset_function");
				ProcessChildNode(nodeIndex, "as_keyword");
				ProcessChildNode(nodeIndex, "table_alias");
			}
			else if (ChildNodeExists(nodeIndex, "derived_table", out tableIndex))
			{
				ProcessChildNode(nodeIndex, "derived_table");
				ProcessChildNode(nodeIndex, "as_keyword");
				ProcessChildNode(nodeIndex, "table_alias");

				int newSearchId	= parser.StartSearch(".column_alias", nodeIndex);
				int newIndex	= parser.FindNext(newSearchId);
				int counter		= 0;

				while (newIndex > 0)
				{
					if (counter > 0)
					{
						WriteString(", ");
					}
					ProcessNode(newIndex);
					newIndex = parser.FindNext(newSearchId);
					counter++;
				}
			}
			else if (ChildNodeExists(nodeIndex, "table_source", out tableIndex))
			{
				WriteString("(");
				ProcessChildNode(nodeIndex, "table_source");
				WriteString("(");
			}
			else // joined tables
			{
				int newSearchId	= parser.StartSearch(".joined_table", nodeIndex);
				int newIndex	= parser.FindNext(newSearchId);
				int counter		= 0;

				while (newIndex > 0)
				{
					ProcessNode(newIndex);
					newIndex = parser.FindNext(newSearchId);
					counter++;
				}
			}
		}

		public void Process_qualified_name(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".name", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(".");
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
		}

		public void Process_where_clause(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}WHERE ", Environment.NewLine, Indent));
			ProcessChildNodes(nodeIndex);
		}
		
		public void Process_search_condition(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".search_term", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(" OR ");
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
		}

		public void Process_search_term(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".search_factor", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;
			indentNum++;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(" AND ");
					WriteString(Environment.NewLine + Indent);
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
			indentNum--;
		}

		public void Process_atom(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "variable"))
			{
				ProcessChildNode(nodeIndex, "variable");
			}
			else if (ChildNodeExists(nodeIndex, "string_expr"))
			{
				ProcessChildNode(nodeIndex, "string_expr");
			}
			else
			{
				WriteString(" user");
			}
		}

		public void Process_create_table(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}CREATE TABLE ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "table");
			WriteString(" ");
			ProcessChildNode(nodeIndex, "table_type_definition");
		}

		public void Process_table_type_definition(int nodeIndex)
		{
			WriteString("(");

			int newSearchId	= parser.StartSearch(".table_type_definition_term", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;
			indentNum++;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(", ");
					WriteString(Environment.NewLine + Indent);
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
			indentNum--;

			WriteString(")");
		}

		public void Process_column_def_qualifier(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "identity_keyword"))
			{
				WriteString("IDENTITY ");
				
				if (ChildNodeExists(nodeIndex, "seed"))
				{
					WriteString("(");
					ProcessChildNode(nodeIndex, "seed");
					WriteString(", ");
					ProcessChildNode(nodeIndex, "increment");
					WriteString(")");
					ProcessChildNode(nodeIndex, "not_for_replication_option");
				}
			}
			else
			{
				ProcessChildNodes(nodeIndex);
			}
		}

		public void Process_column_constraint(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "constraint_name"))
			{
				WriteString("CONSTRAINT ");
				ProcessChildNode(nodeIndex, "constraint_name");
			}
		}

		public void Process_set(int nodeIndex)
		{
			WriteString("SET ");

			if (ChildNodeExists(nodeIndex, "local_variable") &&
				!ChildNodeExists(nodeIndex, "tsql_identifier"))
			{
				ProcessChildNode(nodeIndex, "local_variable");
				WriteString(" = ");

				if (ChildNodeExists(nodeIndex, "__cursor_decl_spec"))
				{
					ProcessChildNode(nodeIndex, "__cursor_decl_spec");
				}
				else if (ChildNodeExists(nodeIndex, "null_tag"))
				{
					ProcessChildNode(nodeIndex, "null_tag");
				}
				else
				{
					ProcessChildNode(nodeIndex, "expression");
				}

			}
			else if (ChildNodeExists(nodeIndex, "qualified_name"))
			{
				WriteString("IDENTITY_INSERT ");
				ProcessChildNode(nodeIndex, "qualified_name");
				WriteString(" ");
				ProcessChildNode(nodeIndex, "on_off_flag");
			}
			else
			{
				int newSearchId	= parser.StartSearch(".tsql_identifier", nodeIndex);
				int newIndex	= parser.FindNext(newSearchId);
				int counter		= 0;

				while (newIndex > 0)
				{
					if (counter > 0)
					{
						WriteString(", ");
					}
					ProcessNode(newIndex);
					newIndex = parser.FindNext(newSearchId);
					counter++;
				}
				if (counter > 1)
				{
					ProcessChildNode(nodeIndex, "on_off_flag");
				}
				else
				{
					WriteString(" = ");

					if (ChildNodeExists(nodeIndex, "literal"))
					{
						ProcessChildNode(nodeIndex, "literal");
					}
					else if (ChildNodeExists(nodeIndex, "on_off_flag"))
					{
						ProcessChildNode(nodeIndex, "on_off_flag");
					}
					else if (ChildNodeExists(nodeIndex, "local_variable"))
					{
						ProcessChildNode(nodeIndex, "local_variable");
					}
					else
					{
						ProcessChildNode(nodeIndex, "identifier");
					}
				}
			}
			GetUnexpectedNodes(nodeIndex, new string[] {"local_variable", "__cursor_decl_spec", "null_tag", "expression", "qualified_name", "on_off_flag", "tsql_identifier", "literal", "identifier"});
		}

		public void Process_case_function(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}CASE ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "expression");

			int newSearchId	= parser.StartSearch(".when_expression", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;
			indentNum++;

			while (newIndex > 0)
			{
				WriteString(string.Format("{0}{1}WHEN ", Environment.NewLine, Indent));
				ProcessNode(newIndex);
				indentNum++;
				WriteString(string.Format("{0}{1}THEN ", Environment.NewLine, Indent));
				int sibling = parser.GetNextSibling(newIndex);

				if (parser.GetLabel(sibling) == "result_expression")
				{
					ProcessNode(sibling);
				}
				indentNum--;
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
			if (ChildNodeExists(nodeIndex, "else_result_expression"))
			{
				WriteString(string.Format("{0}{1}ELSE ", Environment.NewLine, Indent));
				ProcessChildNode(nodeIndex, "else_result_expression");
			}
			indentNum--;
			WriteString(string.Format("{0}{1}END", Environment.NewLine, Indent));
			GetUnexpectedNodes(nodeIndex, new string[] {"expression", "when_expression", "result_expression", "else_result_expression"});
		}

		public void Process_string_expr(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "function_name"))
			{
				ProcessChildNode(nodeIndex, "function_name");
				WriteString("(");
				int newSearchId	= parser.StartSearch(".expression", nodeIndex);
				int newIndex	= parser.FindNext(newSearchId);
				int counter		= 0;

				while (newIndex > 0)
				{
					if (counter > 0)
					{
						WriteString(", ");
					}
					ProcessNode(newIndex);
					newIndex = parser.FindNext(newSearchId);
					counter++;
				}
				WriteString(")");
			}
			else
			{
				ProcessChildNodes(nodeIndex);
			}
			//WriteString(";");
		}

		public void Process_order_by_clause(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}ORDER BY ", Environment.NewLine, Indent));
	
			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				if (i > 0)
				{
					WriteString(", ");
				}
				int childIndex = parser.GetChild(nodeIndex, i);
				
				if (parser.GetLabel(childIndex) == "order_by_expression")
				{
					ProcessNode(childIndex);
					WriteString(" ");
				}
				if (parser.GetLabel(childIndex) == "column_position")
				{
					ProcessNode(childIndex);
					WriteString(" ");
				}
				if (parser.GetLabel(childIndex) == "ascending_or_descending")
				{
					ProcessNode(childIndex);
				}
			}
			GetUnexpectedNodes(nodeIndex, new string[] {"order_by_expression", "column_position", "ascending_or_descending"});
		}

		public void Process_function_ref(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "__cast_function"))
			{
				ProcessChildNodes(nodeIndex);
			}
			else if (ChildNodeExists(nodeIndex, "function_name"))
			{
				if (ChildNodeExists(nodeIndex, "scope_operator"))
				{
					ProcessChildNode(nodeIndex, "scope_operator");
					WriteString(" ");
				}
				ProcessChildNode(nodeIndex, "function_name");
				WriteString(" (");

				if (ChildNodeExists(nodeIndex, "wildcard"))
				{
					ProcessChildNode(nodeIndex, "wildcard");
				}
				else
				{
					int counter = 0;

					for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
					{
						if (counter > 0)
						{
							WriteString(", ");
						}
						int childIndex = parser.GetChild(nodeIndex, i);

						if (parser.GetLabel(childIndex) == "default_keyword" ||
							parser.GetLabel(childIndex) == "expression")
						{
							ProcessNode(childIndex);
							counter++;
						}
					}
				}
				WriteString(")");
			}
			else
			{
				ProcessChildNode(nodeIndex, "sql_function_name");
				WriteString("(");
				ProcessChildNode(nodeIndex, "wildcard");
				ProcessChildNode(nodeIndex, "distinct_literal");
				ProcessChildNode(nodeIndex, "identity_col_keyword");

				if (ChildNodeExists(nodeIndex, "all_or_distinct"))
				{
					ProcessChildNode(nodeIndex, "all_or_distinct");
					WriteString(" ");
				}
				ProcessChildNode(nodeIndex, "scalar_exp");
				WriteString(")");
			}
		}

		public void Process_statement_block(int nodeIndex)
		{
			//WriteString(string.Format("{0}{1}BEGIN{0}", Environment.NewLine, Indent));
			WriteString("BEGIN");
			indentNum++;
			ProcessChildNodes(nodeIndex);
			indentNum--;
			WriteString(string.Format("{0}{1}END", Environment.NewLine, Indent));
		}

		public void Process_declare(int nodeIndex)
		{
			WriteString("DECLARE ");

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				int childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "local_variable":
						ProcessNode(childIndex);
						break;
					case "as_keyword":
						WriteString(" ");
						ProcessNode(childIndex);
						break;
					case "data_type":
						WriteString(" ");
						ProcessNode(childIndex);
						
						if (parser.GetNextSibling(childIndex) > 0)
						{
							//WriteString(", ");
							WriteString(", " + Environment.NewLine + Indent);
						}
						break;
					case "cursor_variable_name":
						ProcessNode(childIndex);
						break;
					case "cursor_keyword":
						WriteString(" ");
						ProcessNode(childIndex);

						if (parser.GetNextSibling(childIndex) > 0)
						{
							//WriteString(", ");
							WriteString(", " + Environment.NewLine + Indent);
						}
						break;
					case "table_type_definition":
						WriteString(" TABLE ");
						ProcessNode(childIndex);

						if (parser.GetNextSibling(childIndex) > 0)
						{
							//WriteString(", ");
							WriteString(", " + Environment.NewLine + Indent);
						}
						break;
				}
			}
		}

		public void Process_print(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}PRINT ", Environment.NewLine, Indent));
			ProcessChildNodes(nodeIndex);
		}

		public void Process_cast_function(int nodeIndex)
		{
			WriteString("CAST (");
			ProcessChildNode(nodeIndex, "expression");
			WriteString(" AS ");
			ProcessChildNode(nodeIndex, "data_type");
			WriteString(")");
		}

		public void Process_function_name(int nodeIndex)
		{
			switch (parser.GetValue(nodeIndex).ToUpper())
			{
				case "COLUMNS_UPDATED":
					WriteString("COLUMNS_UPDATED");
					break;
				case "UPDATE":
					WriteString("UPDATE");
					break;
				case "REPLACE":
					WriteString("REPLACE");
					break;
				default:
					WriteString(parser.GetValue(nodeIndex));
					break;
			}
		}

		public void Process_select_variable(int nodeIndex)
		{
			WriteString("SELECT ");

			int newSearchId	= parser.StartSearch(".local_variable", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(", ");
				}
				ProcessNode(newIndex); // Process local_variable
				WriteString(" = ");
				newIndex = parser.GetNextSibling(newIndex);
				//newSearchId	= parser.StartSearch(".expression", newIndex);
				//newIndex	= parser.FindNext(newSearchId);
				ProcessNode(newIndex); // Process expression
				//newSearchId	= parser.StartSearch(".local_variable", newIndex);
				//newIndex	= parser.FindNext(newSearchId);
				newIndex = parser.GetNextSibling(newIndex);
				counter++;
			}
		}

		public void Process_between_predicate(int nodeIndex)
		{
			WriteString("BETWEEN ");
			ProcessChildNode(nodeIndex, "scalar1");
			WriteString(" AND ");
			ProcessChildNode(nodeIndex, "scalar2");
		}

		public void Process_like_predicate(int nodeIndex)
		{
			WriteString("LIKE ");
			ProcessChildNode(nodeIndex, "expression");

			if (ChildNodeExists(nodeIndex, "escape"))
			{
				WriteString(" ");
				ProcessChildNode(nodeIndex, "escape");
			}
		}

		public void Process_escape(int nodeIndex)
		{
			WriteString("ESCAPE ");
			ProcessChildNodes(nodeIndex);
		}

		public void Process_in_predicate(int nodeIndex)
		{
			WriteString("IN ");

			if (ChildNodeExists(nodeIndex, "atom_list"))
			{
				WriteString("(");
				ProcessChildNode(nodeIndex, "atom_list");
				WriteString(")");
			}
			else
			{
				ProcessChildNode(nodeIndex, "subquery");
			}
		}

		public void Process_atom_list(int nodeIndex)
		{
			int newSearchId	= parser.StartSearch(".atom", nodeIndex);
			int newIndex	= parser.FindNext(newSearchId);
			int counter		= 0;

			while (newIndex > 0)
			{
				if (counter > 0)
				{
					WriteString(", ");
				}
				ProcessNode(newIndex);
				newIndex = parser.FindNext(newSearchId);
				counter++;
			}
		}

		public void Process_while(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}WHILE ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "boolean_expression");
			indentNum++;
			ProcessChildNode(nodeIndex, "sql_statement");
			indentNum--;
		}

		public void Process_if_else_statement(int nodeIndex)
		{
			if (parser.GetPrevSibling(parser.GetParent(nodeIndex)) > 0)// || parser.GetLabel(parser.GetParent(parser.GetParent(nodeIndex))) != "statement_block")
			{
				WriteString(Environment.NewLine + Indent);
			}
			WriteString("IF ");
			ProcessChildNode(nodeIndex, "boolean_expression");
			indentNum++;
			bool elseHasBeenHit = false;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				int childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "sql_statement":
						ProcessNode(childIndex);
						break;
					case "else_keyword":
						indentNum--;
						elseHasBeenHit = true;
						WriteString(string.Format("{0}{1}ELSE", Environment.NewLine, Indent));
						indentNum++;
						break;
				}
			}
			indentNum--;
		}

		public void Process_truncate_table(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}TRUNCATE TABLE", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "name");
		}

		public void Process_insert(int nodeIndex)
		{
			WriteString("INSERT ");

			if (ChildNodeExists(nodeIndex, "into_keyword"))
			{
				WriteString("INTO ");
			}
			if (ChildNodeExists(nodeIndex, "table"))
			{
				ProcessChildNode(nodeIndex, "table");

				if (ChildNodeExists(nodeIndex, "with_keyword"))
				{
					WriteString("WITH (");
					int counter = 0;
					
					for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
					{
						int childIndex = parser.GetChild(nodeIndex, i);


						switch (parser.GetLabel(childIndex))
						{
							case "table_hint_limited":
								if (counter > 1)
								{
									WriteString(", ");
								}
								ProcessNode(childIndex);
								counter++;
								break;
						}
					}
					WriteString(")");

				}
			}
			else if (ChildNodeExists(nodeIndex, "rowset_function_limited"))
			{
				ProcessChildNode(nodeIndex, "rowset_function_limited");
			}
			if (ChildNodeExists(nodeIndex, "column"))
			{
				WriteString("(");

				int counter = 0;
					
				for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
				{
					int childIndex = parser.GetChild(nodeIndex, i);


					switch (parser.GetLabel(childIndex))
					{
						case "column":
							if (counter > 1)
							{
								WriteString(", ");
							}
							ProcessNode(childIndex);
							counter++;
							break;
					}
				}
				WriteString(")");
			}
			if (ChildNodeExists(nodeIndex, "values_keyword"))
			{
				WriteString("VALUES (");
				int counter  = 0;

				for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
				{
					int childIndex = parser.GetChild(nodeIndex, i);

					if (counter > 1)
					{
						WriteString(", ");
					}
					switch (parser.GetLabel(childIndex))
					{
						case "default_keyword":
						case "null_tag":
						case "expression":
							ProcessNode(childIndex);
							break;
					}
					counter++;
				}
				WriteString(")");
			}
			else
			{
				ProcessChildNode(nodeIndex, "derived_table");
				ProcessChildNode(nodeIndex, "execute");
				ProcessChildNode(nodeIndex, "default_keyword");
				ProcessChildNode(nodeIndex, "values_keyword2");
			}
		}

		public void Process_execute(int nodeIndex)
		{
			if (ChildNodeExists(nodeIndex, "execute_keyword"))
			{
				if (MaintainCharacterCount)
				{
					int childIndex;

					if (ChildNodeExists(nodeIndex, "execute_keyword", out childIndex))
					{
						WriteString(parser.GetValue(childIndex) + " ");
					}
					else if (UseFullKeywords)
					{
						WriteString("EXECUTE ");
					}
					else
					{
						WriteString("EXEC ");
					}
				}
			}
			if (ChildNodeExists(nodeIndex, "return_status"))
			{
				ProcessChildNode(nodeIndex, "return_status");
				WriteString(" = ");
			}
			if (ChildNodeExists(nodeIndex, "procedure_name"))
			{
				ProcessChildNode(nodeIndex, "procedure_name");
				
				if (ChildNodeExists(nodeIndex, "number"))
				{
					WriteString(";");
					ProcessChildNode(nodeIndex, "number");
				}
				WriteString(" ");
			}
			else if (ChildNodeExists(nodeIndex, "procedure_name_var"))
			{
				ProcessChildNode(nodeIndex, "procedure_name_var");
			}
			int counter  = 0;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				int childIndex = parser.GetChild(nodeIndex, i);
				string label = parser.GetLabel(childIndex);

				if (label == "space_symbol")
				{
					continue;
				}
				//if (counter > 1)
				//{
				//	WriteString(", ");
				//}
				bool checkForComma = false;

				switch (label)
				{
					case "parameter":
						ProcessNode(childIndex);
						WriteString(" = ");
						break;
					case "literal":
					case "name":
					case "local_variable":
					case "default_keyword":
						checkForComma = true;
						ProcessNode(childIndex);
						break;
					case "output_tag":
						checkForComma = true;
						WriteString(" ");
						ProcessNode(childIndex);
						break;
				}
				if (checkForComma &&
					parser.GetNextSibling(childIndex) > 0 &&
					GetNextNonSpaceLabel(childIndex) != "with_keyword" &&
					GetNextNonSpaceLabel(childIndex) != "output_tag")
				{
					WriteString(", ");
				}
				counter++;
			}
			if (ChildNodeExists(nodeIndex, "with_keyword"))
			{
				WriteString(" WITH RECOMPILE");
			}
			GetUnexpectedNodes(nodeIndex, new string[] {"execute_keyword", "return_status", "procedure_name", "number", "procedure_name_var", "parameter", "literal", "name", "local_variable", "output_tag", "default_keyword", "with_keyword"});
		}

		public void Process_delete(int nodeIndex)
		{
			WriteString("DELETE ");

			if (ChildNodeExists(nodeIndex, "from_keyword"))
			{
				WriteString("FROM ");
			}
			if (ChildNodeExists(nodeIndex, "table"))
			{
				ProcessChildNode(nodeIndex, "table");

				if (ChildNodeExists(nodeIndex, "table_hint_limited"))
				{
					WriteString("WITH (");
					int counter = 0;

					for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
					{
						int childIndex = parser.GetChild(nodeIndex, i);

						switch (parser.GetLabel(childIndex))
						{
							case "table_hint_limited":
								if (counter > 1)
								{
									WriteString(", ");
								}
								ProcessNode(childIndex);
								break;
						}
						counter++;
					}
					WriteString(")");
				}
			}
			else
			{
				ProcessChildNode(nodeIndex, "rowset_function_limited");
			}
			ProcessChildNode(nodeIndex, "statement_selection_clause");
			GetUnexpectedNodes(nodeIndex, new string[] {"from_keyword", "table", "table_hint_limited", "rowset_function_limited", "statement_selection_clause"});
		}
		
		public void Process_update(int nodeIndex)
		{
			int counter = 0;
			WriteString("UPDATE ");

			if (ChildNodeExists(nodeIndex, "table"))
			{
				ProcessChildNode(nodeIndex, "table");

				if (ChildNodeExists(nodeIndex, "table_hint_limited"))
				{
					WriteString("WITH (");
					counter = 0;

					for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
					{
						int childIndex = parser.GetChild(nodeIndex, i);

						switch (parser.GetLabel(childIndex))
						{
							case "table_hint_limited":
								if (counter > 1)
								{
									WriteString(", ");
								}
								ProcessNode(childIndex);
								break;
						}
						counter++;
					}
					WriteString(")");
				}
			}
			else
			{
				ProcessChildNode(nodeIndex, "rowset_function_limited");
			}
			WriteString("SET ");
			counter = 0;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				int childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "column_name":
					case "variable":
						ProcessNode(childIndex);
						WriteString(" = ");
						break;
					case "expression":
					case "default_keyword":
					case "null_tag":
						ProcessNode(childIndex);
						int siblingIndex = parser.GetNextSibling(childIndex);

						if (parser.GetLabel(siblingIndex) != "statement_selection_clause")
						{
							WriteString(", ");
						}
						break;
				}
				counter++;
			}
			ProcessChildNode(nodeIndex, "statement_selection_clause");
			GetUnexpectedNodes(nodeIndex, new string[] {"table", "table_hint_limited", "rowset_function_limited", "column_name", "expression", "default_keyword", "null_tag", "variable", "column", "statement_selection_clause"});
		}

		public void Process_statement_selection_clause(int nodeIndex)
		{
			int counter;
			WriteString(string.Format("{0}{1}", Environment.NewLine, Indent));

			if (ChildNodeExists(nodeIndex, "table_source"))
			{
				WriteString("FROM ");
				counter = 0;

				for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
				{
					int childIndex = parser.GetChild(nodeIndex, i);

					switch (parser.GetLabel(childIndex))
					{
						case "table_source":
							if (counter > 1)
							{
								WriteString(", ");
							}
							ProcessNode(childIndex);
							counter++;
							break;
					}
				}
			}
			if (ChildNodeExists(nodeIndex, "current_keyword"))
			{
				WriteString("WHERE CURRENT OF ");
				counter = 0;

				for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
				{
					int childIndex = parser.GetChild(nodeIndex, i);

					switch (parser.GetLabel(childIndex))
					{
						case "global_keyword":
							ProcessNode(childIndex);
							counter++;
							break;
						case "cursor_name":
						case "cursor_variable_name":
							if (counter > 1)
							{
								WriteString(", ");
							}
							ProcessNode(childIndex);
							counter++;
							break;
					}
				}
			}
			else
			{
				WriteString("WHERE ");
				ProcessChildNode(nodeIndex, "search_condition");
			}
			ProcessChildNode(nodeIndex, "option_query_hints_clause");
			GetUnexpectedNodes(nodeIndex, new string[] {"table_source", "current_keyword", "global_keyword", "cursor_name", "cursor_variable_name", "search_condition", "option_query_hints_clause"});
		}

		public void Process_declare_cursor(int nodeIndex)
		{
			//WriteString(string.Format("{0}{1}DECLARE ", Environment.NewLine, Indent));
			WriteString("DECLARE ");
			int childIndex;
			int counter;

			if (ChildNodeExists(nodeIndex, "cursor_name", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "insensitive_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "scroll_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "cursor_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			WriteString("FOR ");
			ProcessChildNode(nodeIndex, "select");

			if (ChildNodeExists(nodeIndex, "read_only_keyword") ||
				ChildNodeExists(nodeIndex, "update_keyword"))
			{
				WriteString("FOR ");

				ProcessChildNode(nodeIndex, "read_only_keyword");
				ProcessChildNode(nodeIndex, "update_keyword");

				if (ChildNodeExists(nodeIndex, "column_name"))
				{
					WriteString("OF ");

					counter = 0;

					for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
					{
						childIndex = parser.GetChild(nodeIndex, i);

						switch (parser.GetLabel(childIndex))
						{
							case "column_name":
								if (counter > 1)
								{
									WriteString(", ");
								}
								ProcessNode(childIndex);
								counter++;
								break;
						}
					}

				}
			}
			ProcessChildNode(nodeIndex, "__cursor_decl_spec");
			GetUnexpectedNodes(nodeIndex, new string[] {"cursor_name", "insensitive_keyword", "scroll_keyword", "cursor_keyword", "select", "read_only_keyword", "update_keyword", "column_name", "__cursor_decl_spec"});
		}

		public void Process_open(int nodeIndex)
		{
			int childIndex;

			WriteString(string.Format("{0}{1}OPEN ", Environment.NewLine, Indent));

			if (ChildNodeExists(nodeIndex, "global_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			ProcessChildNode(nodeIndex, "cursor_name");
			ProcessChildNode(nodeIndex, "cursor_variable_name");
		}

		public void Process_fetch(int nodeIndex)
		{
			int childIndex;
			WriteString(string.Format("{0}{1}OPEN ", Environment.NewLine, Indent));

			if (ChildNodeExists(nodeIndex, "fetch_record_spec", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "from_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "global_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "cursor_name", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "cursor_variable_name", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "variable_name"))
			{
				WriteString("INTO ");
				int counter = 0;

				for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
				{
					childIndex = parser.GetChild(nodeIndex, i);

					switch (parser.GetLabel(childIndex))
					{
						case "variable_name":
							if (counter > 1)
							{
								WriteString(", ");
							}
							ProcessNode(childIndex);
							counter++;
							break;
					}
				}

			}
		}

		public void Process_fetch_record_spec(int nodeIndex)
		{
			int childIndex;

			if (ChildNodeExists(nodeIndex, "fetch_absolute_param", out childIndex))
			{
				WriteString("ABSOLUTE ");
				ProcessNode(childIndex);
			}
			else if (ChildNodeExists(nodeIndex, "fetch_relative_param", out childIndex))
			{
				WriteString("RELATIVE ");
				ProcessNode(childIndex);
			}
			else
			{
				ProcessChildNodes(nodeIndex);
			}
		}

		public void Process_close(int nodeIndex)
		{
			int childIndex;

			WriteString(string.Format("{0}{1}CLOSE ", Environment.NewLine, Indent));

			if (ChildNodeExists(nodeIndex, "global_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			ProcessChildNode(nodeIndex, "cursor_name");
			ProcessChildNode(nodeIndex, "cursor_variable_name");
		}

		public void Process_deallocate(int nodeIndex)
		{
			int childIndex;

			WriteString(string.Format("{0}{1}DEALLOCATE ", Environment.NewLine, Indent));

			if (ChildNodeExists(nodeIndex, "global_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			ProcessChildNode(nodeIndex, "cursor_name");
			ProcessChildNode(nodeIndex, "cursor_variable_name");
		}

		public void Process_return(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}RETURN", Environment.NewLine, Indent));
			ProcessChildNodes(nodeIndex);
		}

		public void Process_test_for_null(int nodeIndex)
		{
			ProcessChildNode(nodeIndex, "column_ref");
			WriteString(" IS ");
			int childIndex;

			if (ChildNodeExists(nodeIndex, "not_tag", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			ProcessChildNode(nodeIndex, "null_tag");
		}

		public void Process_drop_table(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}DROP TABLE ", Environment.NewLine, Indent));
			ProcessChildNodes(nodeIndex);
		}

		public void Process_select_top_clause(int nodeIndex)
		{
			int childIndex;
			WriteString("TOP ");
			ProcessChildNode(nodeIndex, "integer");

			if (ChildNodeExists(nodeIndex, "percent_tag", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "with_ties_tag", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
		}

		public void Process_set_transaction_level(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}SET TRANSACTION ISOLATION LEVEL ", Environment.NewLine, Indent));
			ProcessChildNodes(nodeIndex);
		}

		public void Process_transaction_levels(int nodeIndex)
		{
			string text = parser.GetValue(nodeIndex).ToUpper();
			text = text.Replace("  ", " ");
		}

		public void Process_cursor_decl_spec(int nodeIndex)
		{
			int colNamesProcessed = 0;
			int childIndex;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "local_keyword":
					case "global_keyword":
					case "forward_only_keyword":
					case "scroll_keyword":
					case "static_keyword":
					case "keyset_keyword":
					case "dynamic_keyword":
					case "fast_forward_keyword":
					case "read_only_keyword":
					case "scroll_locks_keyword":
					case "optimistic_keyword":
					case "type_warning_keyword":
						ProcessNode(childIndex);
						WriteString(" ");
						break;
					case "select":
						WriteString("FOR ");
						ProcessNode(childIndex);
						break;
					case "update_keyword":
						WriteString("FOR ");
						ProcessNode(childIndex);
						break;
					case "column_name":
						if (colNamesProcessed == 0)
						{
							WriteString("OF ");
						}
						else
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);
						break;
				}
			}
		}

		public void Process_create_index(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}CREATE ", Environment.NewLine, Indent));

			int childIndex;

			if (ChildNodeExists(nodeIndex, "unique_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "clustered_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "non_clustered_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			WriteString("INDEX ");
			ProcessChildNode(nodeIndex, "index_name");
			WriteString(" ON ");
			ProcessChildNode(nodeIndex, "table");
			WriteString("(");

			int counter = 0;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "column":
						if (counter > 1)
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);
						counter++;
						break;
				}
			}
			WriteString(")");

			if (ChildNodeExists(nodeIndex, "create_index_option_list", out childIndex))
			{
				WriteString(" WITH ");
				ProcessNode(childIndex);
			}
			if (ChildNodeExists(nodeIndex, "filegroup", out childIndex))
			{
				WriteString(" ON ");
				ProcessNode(childIndex);
			}
		}

		public void Process_filegroup(int nodeIndex)
		{
			WriteString("FILEGROUP ");
			ProcessChildNode(nodeIndex, "filegroup_name");

			int counter = 0;
			int childIndex;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "filespec":
						if (counter > 1)
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);
						counter++;
						break;
				}
				if (ChildNodeExists(nodeIndex, "primary_keyword"))
				{
					WriteString(" [PRIMARY]");
				}
			}

		}

		public void Process_filegroup_name(int nodeIndex)
		{
			ProcessChildNode(nodeIndex, "name");

			if (ChildNodeExists(nodeIndex, "primary_keyword"))
			{
				WriteString(" [PRIMARY]");
			}
		}

		public void Process_set_statistics_profile(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}SET STATISTICS PROFILE ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "on_off_flag");
		}

		public void Process_existence_test(int nodeIndex)
		{
			WriteString("EXISTS ");
			ProcessChildNode(nodeIndex, "subquery");
		}

		public void Process_grant(int nodeIndex)
		{
			int childIndex;
			WriteString(string.Format("{0}{1}GRANT ", Environment.NewLine, Indent));

			int grantStatementCounter	= 0;
			int securityAccountCounter	= 0;
			int permissionCounter		= 0;
			int columnCounter			= 0;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "grant_statement":
						if (grantStatementCounter == 0)
						{
							WriteString("(");
						}
						else
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);

						if (parser.GetLabel(parser.GetNextSibling(childIndex)) != "grant_statement")
						{
							WriteString(") ");
						}
						grantStatementCounter++;
						break;
					case "security_account":
						if (securityAccountCounter == 0)
						{
							WriteString("(");
						}
						else
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);

						if (parser.GetLabel(parser.GetNextSibling(childIndex)) != "security_account")
						{
							WriteString(") ");
						}
						securityAccountCounter++;
						break;
					case "permission":
						if (permissionCounter == 0)
						{
							WriteString("(");
						}
						else
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);

						if (parser.GetLabel(parser.GetNextSibling(childIndex)) != "permission")
						{
							WriteString(") ");
						}
						permissionCounter++;
						break;
					case "column":
						if (columnCounter == 0)
						{
							WriteString("(");
						}
						else
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);

						if (parser.GetLabel(parser.GetNextSibling(childIndex)) != "column")
						{
							WriteString(") ");
						}
						columnCounter++;
						break;
					case "all_keyword":
					case "to_keyword":
					case "privileges_keyword":
					case "on_keyword":
					case "table":
					case "stored_procedure":
					case "extended_procedure":
					case "with_grant_option_keyword":
					case "group":
					case "role":
					case "as_keyword":
						ProcessNode(childIndex);
						WriteString(" ");
						break;
				}
			}
		}

		public void Process_security_account(int nodeIndex)
		{
			if (parser.GetNumChildren(nodeIndex) == 1)
			{
				ProcessChildNodes(nodeIndex);
			}
			else
			{
				WriteString("[");

				for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
				{
					if (i > 0)
					{
						WriteString("\\");
					}
					ProcessNode(parser.GetChild(nodeIndex, i));
				}
				WriteString("]");

			}
		}

		public void Process_having_clause(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}HAVING ", Environment.NewLine, Indent));
			ProcessChildNodes(nodeIndex);
		}

		public void Process_begin_transaction(int nodeIndex)
		{
			int childIndex;
			//WriteString(string.Format("{0}{1}BEGIN ", Environment.NewLine, Indent));
			WriteString("BEGIN ");
			ProcessChildNode(nodeIndex, "transaction_keyword");
			WriteString(" ");

			if (ChildNodeExists(nodeIndex, "transaction_name", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "tran_name_variable", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "with_keyword", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" MARK ");

				if (ChildNodeExists(nodeIndex, "description", out childIndex))
				{
					ProcessNode(childIndex);
				}
			}
		}

		public void Process_transaction_keyword(int nodeIndex)
		{
			if (MaintainCharacterCount)
			{
				WriteString(parser.GetValue(nodeIndex));
			}
			else if (UseFullKeywords)
			{
				WriteString("TRANSACTION");
			}
			else
			{
				WriteString("TRAN");
			}
		}

		public void Process_commit_transaction(int nodeIndex)
		{
			int childIndex;
			WriteString(string.Format("{0}{1}COMMIT ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "transaction_keyword");
			WriteString(" ");

			if (ChildNodeExists(nodeIndex, "transaction_name", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
			if (ChildNodeExists(nodeIndex, "tran_name_variable", out childIndex))
			{
				ProcessNode(childIndex);
				WriteString(" ");
			}
		}

		public void Process_raiserror(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}RAISERROR ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "open_parenthesis");
			int childIndex;
			int counter = 0;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "expression":
						if (counter > 1)
						{
							WriteString(", ");
						}
						ProcessNode(childIndex);
						counter++;
						break;
				}
			}
			ProcessChildNode(nodeIndex, "close_parenthesis");

			if (ChildNodeExists(nodeIndex, "with_keyword"))
			{
				WriteString(" WITH SETERROR");
			}
		}

		public void Process_rollback_transaction(int nodeIndex)
		{
			int childIndex;
			WriteString(string.Format("{0}{1}ROLLBACK ", Environment.NewLine, Indent));
			ProcessChildNode(nodeIndex, "transaction_keyword");
			WriteString(" ");
			ProcessChildNode(nodeIndex, "execute");
			ProcessChildNode(nodeIndex, "transaction_name");
			ProcessChildNode(nodeIndex, "tran_name_variable");
			ProcessChildNode(nodeIndex, "savepoint_name");
			ProcessChildNode(nodeIndex, "savepoint_variable");

		}

		public void Process_sql_statement(int nodeIndex)
		{
			//if (parser.GetLabel(parser.GetPrevSibling(nodeIndex)) == "sql_statement")
			//{
				WriteString(Environment.NewLine + Indent);
			//}
			ProcessChildNodes(nodeIndex);
		}

		public void Process_query_expression(int nodeIndex)
		{
			int childIndex;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "query_specification":
					case "query_expression":
					case "open_parenthesis":
					case "close_parenthesis":
						ProcessNode(childIndex);
						break;
					case "union_keyword":
						if (parser.GetLabel(parser.GetNextSibling(childIndex)) == "all_keyword")
						{
							WriteString(string.Format("{0}{0}{1}UNION ALL{0}{0}{1}", Environment.NewLine, Indent));
						}
						else
						{
							WriteString(string.Format("{0}{0}{1}UNION{0}{0}{1}", Environment.NewLine, Indent));
						}
						break;
				}
			}
		}

		public void Process_alter_table(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}ALTER TABLE ", Environment.NewLine, Indent));
			//ProcessChildNode(nodeIndex, "table");
			//indentNum++;
			//WriteString(Environment.NewLine + Indent);
			//ProcessChildNodes(nodeIndex);

			int childIndex;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				switch (parser.GetLabel(childIndex))
				{
					case "comma_keyword":
						WriteString(string.Format(",{0}{1}", Environment.NewLine, Indent));
						break;
					case "add_keyword":
						WriteString(string.Format("ADD{0}{1}", Environment.NewLine, Indent));
						break;
					case "drop_keyword":
						WriteString(string.Format("DROP{0}{1}", Environment.NewLine, Indent));
						break;
					case "table":
						ProcessNode(childIndex);
						indentNum++;
						WriteString(string.Format("{0}{1}", Environment.NewLine, Indent));
						break;
					default:
						//WriteString(" ");
						ProcessNode(childIndex);

						if (parser.GetNextSibling(childIndex) > 0 &&
							parser.GetLabel(parser.GetNextSibling(childIndex)) != "comma_keyword")
						{
							WriteString(" ");
						}
						break;
				}
			}
			indentNum--;
		}

		public void Process_sql_comment(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}{2}", Environment.NewLine, Indent, parser.GetValue(nodeIndex)));
		}

		public void Process_c_comment(int nodeIndex)
		{
			WriteString(string.Format("{0}{1}{2}", Environment.NewLine, Indent, parser.GetValue(nodeIndex)));
		}

		public void Process_column_definition(int nodeIndex)
		{
			int childIndex;

			for (int i = 0; i < parser.GetNumChildren(nodeIndex); i++)
			{
				childIndex = parser.GetChild(nodeIndex, i);

				if (i > 0 && parser.GetLabel(childIndex) != "space_symbol")
				{
					WriteString(" ");
				}
				ProcessNode(childIndex);
			}

		}






	}
}