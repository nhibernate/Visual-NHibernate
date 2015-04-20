using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.CSharp;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;
using CommentObject = ArchAngel.Providers.CodeProvider.DotNet.CommentObject;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;
using Document = ActiproSoftware.SyntaxEditor.Document;
using InterfaceAccessor = ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.InterfaceAccessor;
using UsingStatement = ArchAngel.Providers.CodeProvider.DotNet.UsingStatement;

namespace ArchAngel.Providers.CodeProvider
{
	///<summary>
	/// Used to create a CSharp CodeRoot from C# source code.
	///</summary>
	public class CSharpParser : ISemanticParseDataTarget, IParser
	{
		private readonly Stack<BaseConstruct> objectStack = new Stack<BaseConstruct>();
		private readonly ManualResetEvent parseWaitHandle = new ManualResetEvent(false);
		private readonly SortedList<int, Comment> comments = new SortedList<int, Comment>();
		private readonly SortedList<int, BaseConstruct> baseConstructs = new SortedList<int, BaseConstruct>();
		private readonly SortedList<int, Region> unhandledRegions = new SortedList<int, Region>();
		private readonly List<ParserSyntaxError> syntaxErrors = new List<ParserSyntaxError>();
		private readonly CSharpFormatSettings formatSettings = new CSharpFormatSettings();
		private string currentFilename = "";
		private CSharpController controller = new CSharpController();
		private Document document;
		//private string currentCode = "";
		private Guid parserGuid = Guid.NewGuid();
		private CSharpCodeFormatter formatter;
		private bool parseFinished;
		private ParserException exceptionThrown;

		///<summary>
		/// True if any errors occurred during parsing/formatting. The error descriptions
		/// can be retrieved from the SyntaxErrors property.
		///</summary>
		public bool ErrorOccurred
		{
			get { return syntaxErrors.Count > 0 || exceptionThrown != null; }
		}

		public Document ActiproDocument { get { return document; } }

		/// <summary>
		/// Contains the syntax errors that occured during parsing/formatting. Empty if there were none.
		/// </summary>
		public ReadOnlyCollection<ParserSyntaxError> SyntaxErrors
		{
			get { return syntaxErrors.AsReadOnly(); }
		}

		/// <summary>
		/// The formatter settings that will be used when formatting the code.
		/// </summary>
		public CSharpFormatSettings FormatSettings
		{
			get { return formatSettings; }
		}

		/// <summary>
		/// Gets the CodeRoot that was created by the ParseCode method.
		/// </summary>
		public ICodeRoot CreatedCodeRoot
		{
			get
			{
				return ParseFinished ? controller.Root : null;
			}
		}

		private bool ParseFinished
		{
			get { return parseFinished; }
		}

		#region ISemanticParseDataTarget Members

		/// <summary>
		/// Gets a unique GUID that identifies the object.   
		/// </summary>
		/// <value>
		/// A unique GUID that identifies the object.
		/// </value>
		string ISemanticParseDataTarget.Guid
		{
			get { return parserGuid.ToString(); }
		}

		/// <summary>
		/// Occurs when a semantic parse request is completed.            
		/// </summary>
		/// <param name="request">A <see cref="T:ActiproSoftware.SyntaxEditor.SemanticParserServiceRequest" /> 
		/// that contains the semantic parse request information and the parse data result.</param>
		public void NotifySemanticParseComplete(SemanticParserServiceRequest request)
		{
			// Call this on a new thread so we don't block the ActiproParserService thread while we do our processing.
			Thread thread = new Thread(new ThreadStart(delegate
			{
				try
				{
					CreateCodeRoot(request);
				}
				catch (ParserException e)
				{
					ExceptionThrown = e;
				}
				finally
				{
					parseFinished = true;
					parseWaitHandle.Set();
				}
			}));
			thread.Start();
		}

		public ParserException ExceptionThrown
		{
			get { return exceptionThrown; }
			private set { exceptionThrown = value; }
		}

		#endregion

		/// <summary>
		/// Removes all state from this object so it can be reused for a different parse.
		/// </summary>
		public void Reset()
		{
			controller = new CSharpController();
			parseWaitHandle.Reset();
			parseFinished = false;
			//currentCode = "";
			objectStack.Clear();
			comments.Clear();
			baseConstructs.Clear();
			syntaxErrors.Clear();
			if (formatter != null)
				formatter.SetFormatSettings(formatSettings);
		}

		/// <summary>
		/// Parses the given code. Uses the default filename of DefaultFilename.cs
		/// </summary>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		public void ParseCode(string code)
		{
			ParseCode("DefaultFilename.cs", code);
		}

		/// <summary>
		/// Parses the given code and creates a C# CodeRoot from it.
		/// </summary>
		/// <param name="filename">The name of the file being parsed. Informational use only.</param>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		public void ParseCode(string filename, string code)
		{
			currentFilename = filename;
			//ParseCodeAsync(filename, code).WaitOne(5000);
			WaitHandle waitHandle = ParseCodeAsync(filename, code);
			waitHandle.WaitOne();
		}

		/// <summary>
		/// Parses the given code asynchronously. Uses the default filename of DefaultFilename.cs
		/// </summary>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		public WaitHandle ParseCodeAsync(string code)
		{
			return ParseCodeAsync("DefaultFilename.cs", code);
		}

		/// <summary>
		/// Parses the given code asynchronously and creates a C# CodeRoot from it.
		/// </summary>
		/// <param name="filename">The name of the file being parsed. Informational use only.</param>
		/// <param name="code">The code to parse</param>
		/// <returns>A WaitHandle that will be signalled when the code is parsed and 
		/// the CodeRoot is ready for use.</returns>
		public WaitHandle ParseCodeAsync(string filename, string code)
		{
			Reset();
			parseWaitHandle.Reset();
			parseFinished = false;
			parserGuid = Guid.NewGuid();

			ISemanticParserServiceProcessor language = new CSharpSyntaxLanguage();
			// This is needed because the Actipro parser calculates the text offsets of parsed elements
			// by using /r/n for line breaks (on my windows machine) even if the original text had a /r or /n.
			// This manifests itself as a bunch of wierd Expressions, all type names, some variable names, and
			// various other elements will have completely the wrong text. The number of characters in the final
			// output is correct though.
			document = new Document();
			document.Text = Helper.StandardizeLineBreaks(code, Helper.LineBreaks.Windows);

			if (SemanticParserService.IsRunning == false)
				SemanticParserService.Start();

			// GFH: There seems to be a problem when the service is busy doing IntelliSense
			int ii = SemanticParserService.PendingRequestCount;

			if (SemanticParserService.IsBusy || SemanticParserService.IsRunning)
			{
				try
				{
					SemanticParserService.Stop();
				}
				catch
				{
					// Do nothing. Complains about one thread stopping another thread.
				}
				SemanticParserService.Start();
			}
			// Make a request to the parser service (runs in a separate thread).
			SemanticParserServiceRequest request = new SemanticParserServiceRequest(
				SemanticParserServiceRequest.MediumPriority,
				document,
				new ActiproSoftware.SyntaxEditor.TextRange(0, document.Length),
				SemanticParseFlags.None,
				language,
				this
				);
			SemanticParserService.Parse(request);

			return parseWaitHandle;
		}

		/// <summary>
		/// Creates a CSharp CodeRoot from the result of the semantic parse.
		/// </summary>
		/// <param name="request">The <see cref="SemanticParserServiceRequest"/> that contains the parse results.</param>
		private void CreateCodeRoot(SemanticParserServiceRequest request)
		{
			try
			{
				if (request == null) throw new ArgumentNullException("request");

				// Reset the formatter.
				formatter = new CSharpCodeFormatter(document, formatSettings, controller);

				// Load the document outline 
				CompilationUnit compilationUnit = request.SemanticParseData as CompilationUnit;
				if (compilationUnit == null)
				{
					throw new InvalidOperationException(
						"The Actipro parser did not return a CompilationUnit object. Unable to process this file.");
				}

				foreach (object obj in compilationUnit.SyntaxErrors)
				{
					SyntaxError error = (SyntaxError)obj;
					int lineNumber = document.OffsetToPosition(error.TextRange.StartOffset).Line;

					ParserSyntaxError pse = new ParserSyntaxError(error.Message, error.TextRange.StartOffset,
																  error.TextRange.Length, lineNumber, currentFilename, document.GetSubstring(error.TextRange));
					syntaxErrors.Add(pse);
				}
				// Create region objects before handling AST.
				CreateRegions(compilationUnit);

				ProcessNode(compilationUnit, compilationUnit);

				PostProcessComments();

				FixParentReferences();
			}
			catch (ParserException e)
			{
				exceptionThrown = e;
			}
			catch (Exception e)
			{
				exceptionThrown = new ParserException("An exception as thrown during parsing", e);
			}
		}

		private void FixParentReferences()
		{
			foreach (var child in controller.Root.WalkChildren())
			{
				child.Parent = null;
			}
		}

		private void CreateRegions(CompilationUnit compilationUnit)
		{
			if (compilationUnit.RegionTextRanges == null)
				return;

			foreach (ActiproSoftware.SyntaxEditor.TextRange regionRange in compilationUnit.RegionTextRanges)
			{
				IToken regionStartToken = document.Tokens.GetTokenAtOffset(regionRange.StartOffset);

				// Now that we have the start token, we need to figure out what the name of the region is.
				StringBuilder regionName = new StringBuilder();
				TokenStream tokens = document.GetTokenStream(regionStartToken);
				if (document.GetTokenText(regionStartToken) == "#")
					tokens.Seek(2);
				if (document.GetTokenText(regionStartToken) == "region")
					tokens.Seek(1);
				regionStartToken = tokens.Read();
				while (regionStartToken != null)
				{
					string tokenText = document.GetTokenText(regionStartToken);
					if (regionStartToken.ID == CSharpTokenID.Whitespace && tokenText == "\n")
					{
						break;
					}

					regionName.Append(tokenText);
					regionStartToken = tokens.Read();
				}

				Region region = new Region(controller, regionName.ToString().TrimStart(' '), regionRange.StartOffset);
				region.EndOffset = regionRange.EndOffset;
				region.TextRange = new TextRange(regionRange.StartOffset, regionRange.EndOffset);
				unhandledRegions.Add(regionRange.StartOffset, region);
			}
		}

		private void PostProcessComments()
		{
			if (baseConstructs.Count == 0)
				return;

			int currentPosition = 0; // The current position in the baseConstruct list 
			// stored so we don't have to keep starting from the beginning
			foreach (KeyValuePair<int, Comment> comment in comments)
			{
				if (formatter.HandledComments.ContainsKey(comment.Key))
					continue; // Skip any comments that we have handled in the formatter.

				// Find the baseConstruct that comes right after this comment.
				int commentStart = comment.Key;
				BaseConstruct nextBaseConstruct = null;
				for (int i = currentPosition; i < baseConstructs.Count; i++)
				{
					// If this base construct starts after the comment.
					if (baseConstructs.Keys[i] > commentStart)
					{
						nextBaseConstruct = baseConstructs.Values[i];
						currentPosition = i;
						break;
					}
				}

				// if the comment comes after the last base construct, it is a trailing comment.
				if (nextBaseConstruct == null)
				{
					SetTrailingComment(comment, baseConstructs.Values[baseConstructs.Count - 1].Comments);
					continue;
				}

				BaseConstruct prevBaseConstruct = currentPosition > 0 ? baseConstructs.Values[currentPosition - 1] : nextBaseConstruct;

				// Determine if the base construct and the comment are on the same line
				int commentLineNum = document.OffsetToPosition(comment.Value.StartOffset).Line;
				int prevConstructLineNum = document.OffsetToPosition(prevBaseConstruct.Index).Line;

				// If the comment is at the end of the same line as the previous object, set it as a trailing comment.
				if (commentLineNum == prevConstructLineNum && commentStart > prevBaseConstruct.Index)
				{
					SetTrailingComment(comment, prevBaseConstruct.Comments);
				}
				else // Add it as a preceeding comment on the next baseConstruct.
				{
					string commentValue = comment.Value.Text.Trim();
					commentValue = Helper.StandardizeLineBreaks(commentValue, Environment.NewLine);
					nextBaseConstruct.Comments.PreceedingComments.Add(commentValue);
				}
			}
		}

		private void SetTrailingComment(KeyValuePair<int, Comment> comment, CommentObject commentObject)
		{
			string newComment = Helper.StandardizeLineBreaks(comment.Value.Text.Trim(), Environment.NewLine);

			if (formatSettings.CommentLinesAsCommentBlock)
			{
				newComment = "/* " + newComment.Replace("/*", "").Replace("*/", "").Replace("//", "").Trim() + " */";
			}

			string currentComment = commentObject.TrailingComment;
			if (string.IsNullOrEmpty(currentComment) == false)
			{
				commentObject.TrailingComment +=
					Environment.NewLine + newComment;
			}
			else
			{
				commentObject.TrailingComment = newComment;
			}
		}

		/// <summary>
		/// Processes the specified <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="compilationUnit">The parent <see cref="ICompilationUnit"/>.</param>
		/// <param name="node">The <see cref="IAstNode"/> to examine.</param>
		private void ProcessDotNetNode(CompilationUnit compilationUnit, AstNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			if (compilationUnit == null) throw new ArgumentNullException("compilationUnit");

			// Check for regions
			CheckRegions(node);

			// Process a node start
			switch (node.NodeType)
			{
				case DotNetNodeType.CompilationUnit:
					Process_Compilation_Unit();
					break;
				case DotNetNodeType.Comment:
					Process_Comment(node as Comment);
					break;
				case DotNetNodeType.ClassDeclaration:
					Process_Class_Declaration(node as ClassDeclaration);
					break;
				case DotNetNodeType.EnumerationDeclaration:
					Process_Enum_Declaration(node as EnumerationDeclaration);
					break;
				case DotNetNodeType.EnumerationMemberDeclaration:
					Process_Enum_Member_Declaration(node as EnumerationMemberDeclaration);
					break;
				case DotNetNodeType.InterfaceDeclaration:
					Process_Interface_Declaration(node as InterfaceDeclaration);
					break;
				case DotNetNodeType.NamespaceDeclaration:
					Process_Namespace_Declaration(node as NamespaceDeclaration);
					break;
				case DotNetNodeType.StructureDeclaration:
					Process_Structure_Declaration(node as StructureDeclaration);
					break;
				case DotNetNodeType.ConstructorDeclaration:
					Process_Constructor_Declaration(node as ConstructorDeclaration);
					break;
				case DotNetNodeType.DelegateDeclaration:
					Process_Delegate_Declaration(node as DelegateDeclaration);
					break;
				case DotNetNodeType.FieldDeclaration:
					Process_Field_Declaration(node as FieldDeclaration);
					break;
				case DotNetNodeType.DestructorDeclaration:
					//throw new InvalidOperationException("We do not current support destructors");
					Process_Destructor(node as DestructorDeclaration);
					break;
				case DotNetNodeType.EventDeclaration:
					Process_Event_Declaration(node as EventDeclaration);
					break;
				case DotNetNodeType.InterfaceEventDeclaration:
					Process_Interace_Event_Declaration(node as InterfaceEventDeclaration);
					break;
				case DotNetNodeType.InterfaceMethodDeclaration:
					Process_Interace_Method_Declaration(node as InterfaceMethodDeclaration);
					break;
				case DotNetNodeType.InterfacePropertyDeclaration:
					Process_Interace_Property_Declaration(node as InterfacePropertyDeclaration);
					break;
				case DotNetNodeType.InterfaceAccessor:
					Process_Interace_Accessor_Declaration(node as InterfaceAccessor);
					break;
				case DotNetNodeType.MethodDeclaration:
					Process_Method_Declaration(node as MethodDeclaration);
					break;
				case DotNetNodeType.OperatorDeclaration:
					Process_Operator_Declaration(node as OperatorDeclaration);
					break;
				case DotNetNodeType.PropertyDeclaration:
					Process_Property_Declaration(node as PropertyDeclaration);
					break;
				case DotNetNodeType.UsingDirective:
					Process_Using_Directive(node as UsingDirective);
					break;
				case DotNetNodeType.AttributeSection:
					Process_Attribute_Section(node as ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.AttributeSection);
					break;
				default:
					break;
			}

			// Process child nodes
			ProcessChildNodes(compilationUnit, node);

			// Process a node end
			switch (node.NodeType)
			{
				case DotNetNodeType.CompilationUnit:
				case DotNetNodeType.ClassDeclaration:
				case DotNetNodeType.EnumerationDeclaration:
				case DotNetNodeType.EnumerationMemberDeclaration:
				case DotNetNodeType.InterfaceDeclaration:
				case DotNetNodeType.NamespaceDeclaration:
				case DotNetNodeType.StructureDeclaration:
				case DotNetNodeType.ConstructorDeclaration:
				case DotNetNodeType.DestructorDeclaration:
				case DotNetNodeType.DelegateDeclaration:
				//case DotNetNodeType.FieldDeclaration: // FieldDecls handle stack popping themselves.
				case DotNetNodeType.EventDeclaration:
				case DotNetNodeType.InterfaceEventDeclaration:
				case DotNetNodeType.InterfaceMethodDeclaration:
				case DotNetNodeType.InterfacePropertyDeclaration:
				case DotNetNodeType.InterfaceAccessor:
				case DotNetNodeType.MethodDeclaration:
				case DotNetNodeType.OperatorDeclaration:
				case DotNetNodeType.PropertyDeclaration:
				case DotNetNodeType.UsingDirective:
					objectStack.Pop();
					break;
			}
		}

		private void CheckRegions(AstNode node)
		{
			// Only handle these node types.
			switch (node.NodeType)
			{
				case DotNetNodeType.CompilationUnit:
				case DotNetNodeType.ClassDeclaration:
				case DotNetNodeType.EnumerationDeclaration:
				case DotNetNodeType.EnumerationMemberDeclaration:
				case DotNetNodeType.InterfaceDeclaration:
				case DotNetNodeType.NamespaceDeclaration:
				case DotNetNodeType.StructureDeclaration:
				case DotNetNodeType.ConstructorDeclaration:
				case DotNetNodeType.DestructorDeclaration:
				case DotNetNodeType.DelegateDeclaration:
				case DotNetNodeType.FieldDeclaration:
				case DotNetNodeType.EventDeclaration:
				case DotNetNodeType.InterfaceEventDeclaration:
				case DotNetNodeType.InterfaceMethodDeclaration:
				case DotNetNodeType.InterfacePropertyDeclaration:
				case DotNetNodeType.InterfaceAccessor:
				case DotNetNodeType.MethodDeclaration:
				case DotNetNodeType.OperatorDeclaration:
				case DotNetNodeType.PropertyDeclaration:
				case DotNetNodeType.UsingDirective:
					CheckRegionsStart(node);
					CheckRegionsEnd(node);
					break;
			}
		}

		private void CheckRegionsStart(IAstNode node)
		{
			while (unhandledRegions.Count > 0 && unhandledRegions.Keys[0] < node.StartOffset)
			{
				int key = unhandledRegions.Keys[0];
				// The next region is before this node.
				AddToParentAndStack(unhandledRegions[key]);
				unhandledRegions.RemoveAt(0);
			}
		}

		private void CheckRegionsEnd(IAstNode node)
		{
			if (objectStack.Count == 0) return;
			if (!(objectStack.Peek() is Region)) return;

			Region region = (Region)objectStack.Peek();
			if (node.StartOffset > region.EndOffset)
				objectStack.Pop();
		}

		#region Node Processing Methods

		private void Process_Attribute_Section(ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.AttributeSection section)
		{
			if (objectStack.Count == 1)
			{
				// This is a global attribute.
				AttributeSection attrSec = GetAttributeSectionFromNode(section);
				attrSec.Index = section.StartOffset;
				attrSec.TextRange = new TextRange(section.TextRange.StartOffset, section.TextRange.EndOffset);
				controller.Root.Attributes.Add(attrSec);
				baseConstructs.Add(section.StartOffset, attrSec);
			}
		}

		private void Process_Using_Directive(UsingDirective node)
		{
			if (node == null) throw new ArgumentNullException("node");

			UsingStatement usingDirective = new UsingStatement(controller);
			usingDirective.Alias = node.Alias ?? "";
			usingDirective.Value = node.NamespaceName.Text;

			SetupBaseConstruct(node.StartOffset, node.EndOffset, "", usingDirective, null);
		}

		private void Process_Property_Declaration(PropertyDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			if (node.IsIndexer)
			{
				Indexer inter = new Indexer(controller);
				inter.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);

				foreach (ParameterDeclaration paramNode in node.Parameters)
				{
					inter.Parameters.Add(GetParameterFromParameterDeclaration(document, controller, paramNode));
				}

				SetupBaseConstruct(node, inter);

				if (node.GetAccessor != null)
				{
					Process_Property_Accessor(node, PropertyAccessor.AccessorTypes.Get);
				}
				if (node.SetAccessor != null)
				{
					Process_Property_Accessor(node, PropertyAccessor.AccessorTypes.Set);
				}
			}
			else
			{
				Property inter = new Property(controller);
				inter.Name = node.Name.Text;
				inter.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);
				inter.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));

				SetupBaseConstruct(node, inter);

				if (node.GetAccessor != null)
				{
					Process_Property_Accessor(node, PropertyAccessor.AccessorTypes.Get);
				}
				if (node.SetAccessor != null)
				{
					Process_Property_Accessor(node, PropertyAccessor.AccessorTypes.Set);
				}
			}
		}

		private void Process_Property_Accessor(PropertyDeclaration node, PropertyAccessor.AccessorTypes accessorType)
		{
			PropertyAccessor accessor = new PropertyAccessor(controller);
			accessor.AccessorType = accessorType;

			AccessorDeclaration accessorDec = accessorType == PropertyAccessor.AccessorTypes.Get
												  ? node.GetAccessor
												  : node.SetAccessor;
			if ((node.Modifiers & Modifiers.Abstract) != 0 || accessorDec.BlockStatement == null)
			{ // BlockStatement will be null in abstract classes.
				accessor.BodyText = ";";
			}
			else
				ProcessBodyText(node, accessor, accessorDec.BlockStatement.Statements, node.Comments);
			SetupBaseConstruct(accessorDec.StartOffset, accessorDec.EndOffset, "", accessor, accessorDec.AttributeSections);
			objectStack.Pop(); // This is here on purpose. This method is not handled as part of the giant
			// switch statement, so we need to clean up after ourselves.
		}

		private void Process_Operator_Declaration(OperatorDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			Operator o = new Operator(controller);
			o.Name = FormatterUtility.GetOperatorName(node.OperatorType);
			o.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));
			o.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);

			foreach (IAstNode param in node.Parameters)
			{
				Parameter p = GetParameterFromParameterDeclaration(document, controller, param as ParameterDeclaration);
				o.Parameters.Add(p);
				p.ParentObject = o;
			}
			ProcessBodyText(node, o, node.Statements, node.Comments);


			SetupBaseConstruct(node, o);
		}

		private void Process_Method_Declaration(MethodDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			// Hack to check for implicitly created methods. Actipro put
			// this in if there is a delegate defined in the type.
			if (node.Name.StartOffset == -1 && node.Name.EndOffset == -2)
			{
				// push nothing onto the stack, means we don't have to do anything tricky outside this method.
				objectStack.Push(null);
				return;
			}

			Function function = new Function(controller);
			function.Name = node.Name.Text;
			function.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));
			function.ReturnType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);

			foreach (IAstNode param in node.Parameters)
			{
				Parameter p = GetParameterFromParameterDeclaration(document, controller, param as ParameterDeclaration);
				function.Parameters.Add(p);
				p.ParentObject = function;
			}


			if (node.IsGenericMethodDefinition)
			{
				List<string> genericTypeReferences = new List<string>();
				List<string> genericParameterContraints = new List<string>();

				foreach (TypeReference gtp in node.GenericTypeArguments)
				{
					Process_Generic_Type_Argument(gtp, genericTypeReferences, genericParameterContraints);
				}

				function.GenericParameters.AddRange(genericTypeReferences);

				if (genericParameterContraints.Count > 0)
				{
					function.GenericConstraintClause = string.Format("where {0}", string.Join(", ", genericParameterContraints.ToArray()));
				}
			}
			ProcessBodyText(node, function, node.Statements, node.Comments);
			SetupBaseConstruct(node, function);
		}

		private void Process_Generic_Type_Argument(TypeReference gtp, List<string> genericTypeReferences, List<string> genericParameterContraints)
		{
			var gtName = FormatterUtility.FormatDataType(gtp, document, controller);
			genericTypeReferences.Add(gtName);
			if (gtp.HasGenericParameterReferenceTypeConstraint)
				genericParameterContraints.Add(string.Format("{0} : class", gtName));
			if (gtp.HasGenericParameterDefaultConstructorConstraint)
				genericParameterContraints.Add(string.Format("{0} : new", gtName));
			if (gtp.HasGenericParameterNotNullableValueTypeConstraint)
				genericParameterContraints.Add(string.Format("{0} : struct", gtName));
			foreach (TypeReference constraint in gtp.GenericTypeParameterConstraints)
			{
				var dataType = FormatterUtility.FormatDataType(constraint, document, controller);

				if (gtp.HasGenericParameterNotNullableValueTypeConstraint && dataType == "System.ValueType")
					// For some reason using struct as a constraint adds a regular generic type contraint
					// as well as setting HasGenericParameterNotNullableValueTypeConstraint too true;
					continue;

				genericParameterContraints.Add(string.Format("{0} : {1}", gtName,
															 dataType));
			}
		}

		private void ProcessBodyText(IBlockAstNode node, IBody function, IAstNodeList statements, IAstNodeList commentsWithinBlock)
		{
			formatter.UnhandledRegions.Clear();
			foreach (KeyValuePair<int, Region> r in unhandledRegions)
			{
				if (r.Key > node.BlockStartOffset && r.Value.EndOffset < node.BlockEndOffset)
				{
					formatter.UnhandledRegions.Add(r.Key, r.Value);
				}
			}
			foreach (KeyValuePair<int, Region> r in formatter.UnhandledRegions)
				unhandledRegions.Remove(r.Key);

			function.BodyText =
				formatter.ProcessBodyText(statements, commentsWithinBlock, node.BlockStartOffset, node.BlockEndOffset - node.BlockStartOffset);
		}

		private void Process_Interace_Accessor_Declaration(InterfaceAccessor node)
		{
			if (node == null) throw new ArgumentNullException("node");

			DotNet.InterfaceAccessor inter = new DotNet.InterfaceAccessor(controller);
			inter.AccessorType =
				node.AccessorType == InterfaceAccessorType.Get ?
				DotNet.InterfaceAccessor.AccessorTypes.Get :
				DotNet.InterfaceAccessor.AccessorTypes.Set;

			SetupBaseConstruct(node.StartOffset, node.EndOffset, "", inter, node.AttributeSections);
		}

		private void Process_Interace_Property_Declaration(InterfacePropertyDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			if (node.IsIndexer)
			{
				InterfaceIndexer inter = new InterfaceIndexer(controller);
				inter.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);
				foreach (ParameterDeclaration paramNode in node.Parameters)
				{
					inter.Parameters.Add(GetParameterFromParameterDeclaration(document, controller, paramNode));
				}

				SetupBaseConstruct(node, inter);
			}
			else
			{
				InterfaceProperty inter = new InterfaceProperty(controller, node.Name.Text);
				inter.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);

				SetupBaseConstruct(node, inter);
			}
		}

		private void Process_Interace_Method_Declaration(InterfaceMethodDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			InterfaceMethod inter = new InterfaceMethod(controller, node.Name.Text);
			inter.ReturnType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);
			foreach (ParameterDeclaration paramNode in node.Parameters)
			{
				inter.Parameters.Add(GetParameterFromParameterDeclaration(document, controller, paramNode));
			}

			if (node.IsGenericMethodDefinition)
			{
				List<string> genericTypeReferences = new List<string>();
				List<string> genericParameterContraints = new List<string>();

				foreach (TypeReference gtp in node.GenericTypeArguments)
				{
					Process_Generic_Type_Argument(gtp, genericTypeReferences, genericParameterContraints);
				}

				inter.GenericParameters.AddRange(genericTypeReferences);

				if (genericParameterContraints.Count > 0)
				{
					inter.GenericConstraintClause = string.Format("where {0}", string.Join(", ", genericParameterContraints.ToArray()));
				}
			}

			SetupBaseConstruct(node, inter);
		}

		private void Process_Interace_Event_Declaration(InterfaceEventDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			InterfaceEvent inter = new InterfaceEvent(controller);
			inter.Name = node.Name.Text;
			inter.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.EventType, document, controller);

			SetupBaseConstruct(node, inter);
		}

		private void Process_Event_Declaration(EventDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");
			Event ev = new Event(controller);
			ev.Name = node.Name.Text;
			ev.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));
			ev.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.EventType, document, controller);
			if (node.AddAccessor != null || node.RemoveAccessor != null || node.RaiseEventAccessor != null)
			{
				throw new Exception(
					"The formatter does not currently support add/remove accessors on events. Please contact Slyce Support if you require this functionality.");
			}

			SetupBaseConstruct(node, ev);
		}

		private void Process_Field_Declaration(FieldDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			foreach (IAstNode comment in node.Comments)
				comments.Add(comment.StartOffset, (Comment)comment);

			int counter = 0;

			for (int i = 0; i < node.Variables.Count; i++)
			{
				IAstNode fieldNode = node.Variables[i];
				VariableDeclarator v = fieldNode as VariableDeclarator;

				if (v != null)
				{
					Field field = new Field(controller);
					field.Name = v.Name.Text;
					field.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(v.Modifiers));
					if (v.IsConstant)
					{
						field.Modifiers.Add("const");
					}
					field.InitialValue = formatter.FormatExpression(v.Initializer);
					field.DataType = FormatterUtility.GetDataTypeFromTypeReference(v.ReturnType, document, controller);

					if (counter == 0)
						SetupBaseConstruct(node.TextRange.StartOffset, v.EndOffset, v.DocumentationProvider.Documentation, field, node.AttributeSections);
					else
						SetupBaseConstruct(v.StartOffset, v.EndOffset, v.DocumentationProvider.Documentation, field, node.AttributeSections);

					counter++;
					// Pop the stack here as there may be multiple Field declarations.
					objectStack.Pop();
				}
			}
		}

		private void Process_Enum_Member_Declaration(EnumerationMemberDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			Enumeration.EnumMember enumMember = new Enumeration.EnumMember(controller, node.Name.Text);
			if (node.Initializer != null)
				enumMember.Value = formatter.FormatExpression(node.Initializer);

			SetupBaseConstruct(node, enumMember);
		}

		private void Process_Delegate_Declaration(DelegateDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			var dele = new Delegate(controller);
			dele.Name = node.Name.Text;
			dele.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));
			dele.ReturnType = FormatterUtility.GetDataTypeFromTypeReference(node.ReturnType, document, controller);

			foreach (ParameterDeclaration paramNode in node.Parameters)
			{
				Parameter param = GetParameterFromParameterDeclaration(document, controller, paramNode);
				param.ParentObject = dele;
				dele.Parameters.Add(param);
			}

			SetupBaseConstruct(node, dele);
		}

		private void Process_Constructor_Declaration(ConstructorDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			Constructor constructor = new Constructor(controller);
			constructor.Name = node.Name.Text;
			constructor.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));

			foreach (IAstNode param in node.Parameters)
			{
				Parameter p = GetParameterFromParameterDeclaration(document, controller, param as ParameterDeclaration);
				constructor.Parameters.Add(p);
				p.ParentObject = constructor;
			}

			ProcessBodyText(node, constructor, node.Statements, node.Comments);

			SetupBaseConstruct(node, constructor);
		}

		private void Process_Destructor(DestructorDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			Destructor destructor = new Destructor(controller);
			destructor.Name = node.Name.Text;
			destructor.IsExtern = (node.Modifiers & Modifiers.Extern) != 0;

			ProcessBodyText(node, destructor, node.Statements, node.Comments);
			destructor.BodyText = formatter.ProcessBodyText(node.Statements, node.Comments, node.BlockStartOffset, node.BlockEndOffset - node.BlockStartOffset);

			SetupBaseConstruct(node, destructor);
		}


		private void Process_Structure_Declaration(TypeMemberDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");

			Struct str = new Struct(controller);
			str.Name = node.Name.Text;
			str.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));

			SetupBaseConstruct(node, str);
		}

		private void Process_Namespace_Declaration(NamespaceDeclaration node)
		{
			if (node == null) throw new ArgumentNullException("node");
			Namespace ns = new Namespace(controller);
			ns.Name = node.Name.Text;

			SetupBaseConstruct(node.StartOffset, node.EndOffset, "", ns, null);
		}

		private void Process_Interface_Declaration(TypeMemberDeclaration node)
		{
			if (node == null)
				throw new ArgumentNullException("node");
			Interface inter = new Interface(controller, node.Name.Text);
			inter.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));

			SetupBaseConstruct(node, inter);
		}

		private void Process_Enum_Declaration(TypeMemberDeclaration node)
		{
			if (node == null)
				throw new ArgumentNullException("node");
			Enumeration enu = new Enumeration(controller);
			enu.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));
			enu.Name = node.Name.Text;

			SetupBaseConstruct(node, enu);
		}

		private void Process_Class_Declaration(ClassDeclaration node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			Class clazz = new Class(controller, "");

			string className = node.Name.Text;
			List<string> genericParameterContraints = new List<string>();
			List<string> genericTypeReferences = new List<string>();

			IDomTypeReference domTypeReference = node;
			if (domTypeReference.IsGenericTypeDefinition)
			{
				foreach (TypeReference gtp in domTypeReference.GenericTypeArguments)
				{
					Process_Generic_Type_Argument(gtp, genericTypeReferences, genericParameterContraints);
				}
				className += "<";
				className += string.Join(", ", genericTypeReferences.ToArray());
				className += ">";

				if (genericParameterContraints.Count > 0)
				{
					clazz.GenericConstraintClause = string.Format("where {0}", string.Join(", ", genericParameterContraints.ToArray()));
				}
			}

			clazz.Name = className;
			clazz.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnumCheckInternal(node, document));

			foreach (TypeReference baseType in node.BaseTypes)
			{
				clazz.BaseNames.Add(FormatterUtility.FormatDataType(baseType, document, controller));
			}

			SetupBaseConstruct(node, clazz);
		}

		#endregion

		/// <summary>
		/// Processes the child nodes of the specified <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="compilationUnit">The parent <see cref="ICompilationUnit"/>.</param>
		/// <param name="node">The <see cref="IAstNode"/> to examine.</param>
		private void ProcessChildNodes(CompilationUnit compilationUnit, IAstNode node)
		{
			// Recurse
			if (node.ChildNodeCount > 0)
			{
				if (node is CompilationUnit)
					compilationUnit = (CompilationUnit)node;
				foreach (IAstNode childNode in node.ChildNodes)
					ProcessNode(compilationUnit, childNode);
			}
		}

		/// <summary>
		/// Processes the specified <see cref="IAstNode"/>.
		/// </summary>
		/// <param name="compilationUnit">The parent <see cref="ICompilationUnit"/>.</param>
		/// <param name="node">The <see cref="IAstNode"/> to examine.</param>
		private void ProcessNode(CompilationUnit compilationUnit, IAstNode node)
		{
			if (node is AstNode)
			{
				// Process .NET node
				ProcessDotNetNode(compilationUnit, (AstNode)node);
			}
		}

		#region Helper Methods

		private void AddToParentAndStack(BaseConstruct child)
		{
			BaseConstruct parent = objectStack.Peek();

			while (parent is Region)
				parent = (BaseConstruct)parent.Parent;

			parent.AddChild(child);

			if (parent is CodeRootBaseConstructAdapter == false)
				child.Parent = parent;

			objectStack.Push(child);
		}

		private void Process_Comment(Comment comment)
		{
			comments.Add(comment.StartOffset, comment);
		}

		private void Process_Compilation_Unit()
		{
			controller.Reorder = formatSettings.ReorderBaseConstructs;
			CodeRootBaseConstructAdapter adapter = new CodeRootBaseConstructAdapter(controller.Root);
			objectStack.Push(adapter);
		}

		internal static Parameter GetParameterFromParameterDeclaration(Document document, Controller controller, ParameterDeclaration node)
		{
			Parameter param = new Parameter(controller);
			param.Name = node.Name;
			param.Modifiers.AddRange(FormatterUtility.GetModifiersFromEnum(node.Modifiers));
			param.DataType = FormatterUtility.GetDataTypeFromTypeReference(node.ParameterType, document, controller).ToString();
			param.Index = node.StartOffset;

			return param;
		}

		private static void SetDocumentation(BaseConstruct bc, string xmlDocumentation)
		{
			if (string.IsNullOrEmpty(xmlDocumentation)) return;

			string[] commentLines = xmlDocumentation.Split('\n');
			// Ignore the last line, it is blank
			for (int i = 0; i < commentLines.Length - 1; i++)
			{
				bc.XmlComments.Add(commentLines[i]);
			}
		}

		private void SetupBaseConstruct(TypeMemberDeclaration node, BaseConstruct inter)
		{
			inter.Index = node.StartOffset;
			inter.TextRange = new TextRange(node.TextRange.StartOffset, node.TextRange.EndOffset);
			SetDocumentation(inter, node.DocumentationProvider.Documentation);
			AddToParentAndStack(inter);
			SetAttributes(inter, node.AttributeSections);
			baseConstructs.Add(node.StartOffset, inter);
			//inter.PreceedingBlankLines = FindPreviousBlankLines(node.StartOffset);
		}

		private void SetupBaseConstruct(int startOffset, int endOffset, string documentation, BaseConstruct inter, IAstNodeList attributeSections)
		{
			inter.Index = startOffset;
			inter.TextRange = new TextRange(startOffset, endOffset);
			SetDocumentation(inter, documentation);
			AddToParentAndStack(inter);
			if (attributeSections != null)
			{
				SetAttributes(inter, attributeSections);
			}

			baseConstructs.Add(startOffset, inter);
			if (inter is UsingStatement) return;

			//inter.PreceedingBlankLines = FindPreviousBlankLines(startOffset);
		}

		private void SetAttributes(BaseConstruct inter, IAstNodeList AttributeSections)
		{
			foreach (ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.AttributeSection attrSec in AttributeSections)
			{
				inter.AddAttributeSection(GetAttributeSectionFromNode(attrSec));
			}
		}

		private AttributeSection GetAttributeSectionFromNode(ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.AttributeSection sec)
		{
			AttributeSection attrSec = new AttributeSection(controller);
			foreach (IAstNode node in sec.Attributes)
			{
				attrSec.AddAttribute(GetAttributeFromNode(node));
			}
			// Subtract 1 from start and add 1 to end to account for '[]'
			attrSec.TextRange = new TextRange(sec.StartOffset - 1, sec.EndOffset + 1);
			return attrSec;
		}

		private Attribute GetAttributeFromNode(IAstNode node)
		{
			Attribute attr = new Attribute(controller);

			//DotNet.Ast.Attribute
			ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute attrNode = node as ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute;
			if (attrNode == null)
				throw new ArgumentException("node is not an Attribute");

			string attrName = "";
			if (string.IsNullOrEmpty(attrNode.Target) == false)
			{
				// Add the target onto the attribute name.
				attrName = attrNode.Target + ":";
			}
			attrName += FormatterUtility.GetDataTypeFromTypeReference(attrNode.AttributeType, document, controller).ToString();
			attr.Name = attrName;

			foreach (IAstNode child in attrNode.Arguments)
			{
				//DotNet.Ast.AttributeArgument
				AttributeArgument argument = (AttributeArgument)child;
				string argumentValue = document.GetSubstring(argument.Expression.TextRange);
				if (string.IsNullOrEmpty(argument.Name))
				{
					attr.PositionalArguments.Add(argumentValue);
				}
				else
				{
					attr.NamedArguments.Add(new Attribute.NamedArgument(argument.Name, argumentValue));
				}
			}
			// Subtract 1 from start and add 1 to end to account for '[]'
			attr.TextRange = new TextRange(node.StartOffset - 1, node.EndOffset + 1);
			return attr;
		}

		#endregion

		#region Nested type: CodeRootBaseConstructAdapter

		internal class CodeRootBaseConstructAdapter : BaseConstruct
		{
			private readonly CodeRoot wrappedCodeRoot;

			public CodeRootBaseConstructAdapter(CodeRoot wrappedCodeRoot)
				: base(wrappedCodeRoot.Controller)
			{
				this.wrappedCodeRoot = wrappedCodeRoot;
			}

			protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
			{
				throw new NotImplementedException();
			}

			/// <summary>
			/// Returns all of the child IBaseConstructs in this node in no particular order.
			/// </summary>
			/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
			protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
			{
				return wrappedCodeRoot.WalkChildren();
			}

			/// <summary>
			/// Adds a new child to this BaseConstruct.
			/// </summary>
			/// <param name="childBC">The child object to add</param>
			protected override void AddChildInternal(BaseConstruct childBC)
			{
				wrappedCodeRoot.AddChild(childBC);
			}

			protected string ToStringInternal()
			{
				return wrappedCodeRoot.ToString();
			}

			/// <summary>
			/// Returns a shallow copy of the construct. Does not copy children.
			/// </summary>
			/// <returns>A shallow copy of the construct. Does not copy children.</returns>
			public override IBaseConstruct Clone()
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		private const string MemberDeclarationStart = "public class Class1 { ";
		private const string MemberDeclarationEnd = "\r\n } ";
		private const string InterfaceMemberDeclarationStart = "public interface Interface1 { ";
		private const string InterfaceMemberDeclarationEnd = "\r\n } ";
		private const string EnumMemberStart = "public enum Enum1 { ";
		private const string EnumMemberEnd = "\r\n } ";

		public BaseConstruct ParseSingleConstruct(string code, BaseConstruct existingConstruct)
		{
			if (existingConstruct is Namespace)
			{
				return ParseSingleConstruct(code, BaseConstructType.NamespaceDeclaration);
			}
			if (existingConstruct is Interface)
			{
				return ParseSingleConstruct(code, BaseConstructType.InterfaceDeclaration);
			}
			if (existingConstruct is Class)
			{
				return ParseSingleConstruct(code, BaseConstructType.ClassDeclaration);
			}
			if (existingConstruct is UsingStatement)
			{
				return ParseSingleConstruct(code, BaseConstructType.UsingDirective);
			}
			if (existingConstruct is Struct)
			{
				return ParseSingleConstruct(code, BaseConstructType.StructureDeclaration);
			}
			if (existingConstruct is Constructor)
			{
				return ParseSingleConstruct(code, BaseConstructType.ConstructorDeclaration);
			}
			if (existingConstruct is Destructor)
			{
				return ParseSingleConstruct(code, BaseConstructType.DestructorDeclaration);
			}
			if (existingConstruct is Delegate)
			{
				return ParseSingleConstruct(code, BaseConstructType.DelegateDeclaration);
			}
			if (existingConstruct is Enumeration)
			{
				return ParseSingleConstruct(code, BaseConstructType.EnumerationDeclaration);
			}
			if (existingConstruct is Event)
			{
				return ParseSingleConstruct(code, BaseConstructType.EventDeclaration);
			}
			if (existingConstruct is Field)
			{
				return ParseSingleConstruct(code, BaseConstructType.FieldDeclaration);
			}
			if (existingConstruct is Function)
			{
				return ParseSingleConstruct(code, BaseConstructType.MethodDeclaration);
			}
			if (existingConstruct is Operator)
			{
				return ParseSingleConstruct(code, BaseConstructType.OperatorDeclaration);
			}
			if (existingConstruct is Property)
			{
				return ParseSingleConstruct(code, BaseConstructType.PropertyDeclaration);
			}
			if (existingConstruct is Enumeration.EnumMember)
			{
				return ParseSingleConstruct(code, BaseConstructType.EnumerationMemberDeclaration);
			}
			if (existingConstruct is InterfaceEvent)
			{
				return ParseSingleConstruct(code, BaseConstructType.InterfaceEventDeclaration);
			}
			if (existingConstruct is InterfaceMethod)
			{
				return ParseSingleConstruct(code, BaseConstructType.InterfaceMethodDeclaration);
			}
			if (existingConstruct is InterfaceProperty)
			{
				return ParseSingleConstruct(code, BaseConstructType.InterfacePropertyDeclaration);
			}

			throw new ArgumentException("Unsupported type: " + existingConstruct.GetType());
		}

		public BaseConstruct ParseSingleConstruct(string code, BaseConstructType declaration)
		{
			try
			{

				switch (declaration)
				{
					case BaseConstructType.NamespaceDeclaration:
					case BaseConstructType.InterfaceDeclaration:
					case BaseConstructType.ClassDeclaration:
					case BaseConstructType.UsingDirective:
					case BaseConstructType.StructureDeclaration:
						ParseCode(code);
						return (BaseConstruct)CreatedCodeRoot.WalkChildren()[0];
					case BaseConstructType.ConstructorDeclaration:
					case BaseConstructType.DestructorDeclaration:
					case BaseConstructType.DelegateDeclaration:
					case BaseConstructType.EnumerationDeclaration:
					case BaseConstructType.EventDeclaration:
					case BaseConstructType.FieldDeclaration:
					case BaseConstructType.MethodDeclaration:
					case BaseConstructType.OperatorDeclaration:
					case BaseConstructType.PropertyDeclaration:
						ParseCode(MemberDeclarationStart + code + MemberDeclarationEnd);
						return (BaseConstruct)CreatedCodeRoot.WalkChildren()[0].WalkChildren()[0];
					case BaseConstructType.EnumerationMemberDeclaration:
						ParseCode(EnumMemberStart + code + EnumMemberEnd);
						return (BaseConstruct)CreatedCodeRoot.WalkChildren()[0].WalkChildren()[0];
					case BaseConstructType.InterfaceEventDeclaration:
					case BaseConstructType.InterfaceMethodDeclaration:
					case BaseConstructType.InterfacePropertyDeclaration:
						ParseCode(InterfaceMemberDeclarationStart + code + InterfaceMemberDeclarationEnd);
						return (BaseConstruct)CreatedCodeRoot.WalkChildren()[0].WalkChildren()[0];
					default:
						throw new ParserException(
							"There was an error parsing the given code. Inspect the SyntaxErrors for more information.");
				}
			}
			catch (ParserException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ParserException("An exception occurred while processing the parsed code", e);
			}
			finally
			{
				if (ErrorOccurred)
				{
					throw new ParserException(
							"There was one or more errors while parsing the given code:" + Environment.NewLine + GetFormattedErrors(), SyntaxErrors);
				}
			}
		}

		public string GetFormattedErrors()
		{
			StringBuilder sb = new StringBuilder();
			foreach (ParserSyntaxError error in SyntaxErrors)
			{
				sb.AppendLine(error.ToString());
			}
			if (ExceptionThrown != null)
			{
				if (exceptionThrown.InnerException != null)
				{
					sb.Append(exceptionThrown.InnerException.Message);
					sb.Append(exceptionThrown.InnerException.StackTrace);
				}
				else
				{
					sb.Append(ExceptionThrown.Message);
					sb.Append(exceptionThrown.StackTrace);
				}
			}
			return sb.ToString();
		}
	}

	public class ParserException : Exception
	{
		private ReadOnlyCollection<ParserSyntaxError> _SyntaxErrors = null;

		public ParserException(string s, Exception e)
			: base(s, e)
		{
		}

		public ParserException(string s)
			: base(s)
		{
		}

		public ParserException(string s, Exception e, ReadOnlyCollection<ParserSyntaxError> syntaxErrors)
			: base(s, e)
		{
			_SyntaxErrors = syntaxErrors;
		}

		public ParserException(string s, ReadOnlyCollection<ParserSyntaxError> syntaxErrors)
			: base(s)
		{
			_SyntaxErrors = syntaxErrors;
		}

		public ReadOnlyCollection<ParserSyntaxError> SyntaxErrors
		{
			get { return _SyntaxErrors; }
		}
	}


	public enum BaseConstructType
	{
		ConstructorDeclaration,
		DelegateDeclaration,
		DestructorDeclaration,
		FieldDeclaration,
		EventDeclaration,
		MethodDeclaration,
		OperatorDeclaration,
		PropertyDeclaration,
		ClassDeclaration,
		EnumerationDeclaration,
		EnumerationMemberDeclaration,
		InterfaceDeclaration,
		NamespaceDeclaration,
		StructureDeclaration,
		UsingDirective,
		InterfaceEventDeclaration,
		InterfaceMethodDeclaration,
		InterfacePropertyDeclaration
	}
}
