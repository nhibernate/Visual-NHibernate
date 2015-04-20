using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public class CodeRoot : ICodeRoot, IPrintable
	{
		protected string _usingStatementsTextBlock = "";
		protected bool _usingStatementsAreSorted;
		public List<Namespace> Namespaces = new List<Namespace>();
		public List<Class> Classes = new List<Class>();
		public List<Struct> Structs = new List<Struct>();
		public List<Interface> Interfaces = new List<Interface>();
		public List<Enumeration> Enums = new List<Enumeration>();
		public List<Delegate> Delegates = new List<Delegate>();
		public List<UsingStatement> UsingStatements = new List<UsingStatement>();
		public List<AttributeSection> Attributes = new List<AttributeSection>();

		public override string ToString()
		{
			return controller.GetPrinterFor(this).ToString();
		}

		public static bool IncludeExtraLines;
		public bool IgnoreAllOmits;
		protected Controller controller;

		public CodeRoot(Controller controller)
		{
			this.controller = controller;
		}

		public Controller Controller
		{
			get { return controller; }
		}

		public string UsingStatementsTextBlock
		{
			get
			{
				if (!_usingStatementsAreSorted)
				{
					if (Controller.Reorder)
					{
						Comparers.UsingStatementComparer comparer = new Comparers.UsingStatementComparer();
						UsingStatements.Sort(comparer);
					}
					StringBuilder sb = new StringBuilder(100);

					for (int i = 0; i < UsingStatements.Count; i++)
					{
						sb.AppendLine(UsingStatements[i].ToString());
					}
					_usingStatementsTextBlock = sb.ToString();
					_usingStatementsAreSorted = true;
				}
				return _usingStatementsTextBlock;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					CSharpParser csf = new CSharpParser();
					csf.ParseCode(value);
					UsingStatements = ((CodeRoot)csf.CreatedCodeRoot).UsingStatements;
					_usingStatementsAreSorted = false;
				}
				else
				{
					UsingStatements = new List<UsingStatement>();
					_usingStatementsAreSorted = true;
				}
			}
		}

		#region ICodeRoot Members

		/// <summary>
		/// Returns a new instance of this type of ICodeRoot
		/// </summary>
		/// <returns>A new instance of this type of ICodeRoot</returns>
		public ICodeRoot NewInstance()
		{
			CSharpController tempController = new CSharpController();
			tempController.Reorder = controller.Reorder;
			tempController.MaintainWhitespace = controller.MaintainWhitespace;
			tempController.Root = new CodeRoot(tempController);
			return tempController.Root;
		}

		#endregion

		#region ICodeRoot Members

		public IBaseConstruct CreateBaseConstruct(string code, IBaseConstruct originalConstruct)
		{
			CSharpParser csFormatter = new CSharpParser();

			return csFormatter.ParseSingleConstruct(code, originalConstruct as BaseConstruct);
		}

		#endregion

		public BaseConstruct[] SortedConstructs
		{
			get
			{
				ArrayList objects = new ArrayList();
				objects.AddRange(Namespaces);
				objects.AddRange(Classes);
				objects.AddRange(Structs);
				objects.AddRange(Delegates);
				objects.AddRange(Interfaces);
				objects.AddRange(Enums);
				objects.AddRange(Attributes);
				objects.AddRange(UsingStatements);

				BaseConstruct[] baseObjects = (BaseConstruct[])objects.ToArray(typeof(BaseConstruct));
				int[] indexes = new int[baseObjects.Length];

				for (int i = 0; i < baseObjects.Length; i++)
				{
					if (baseObjects[i].Index < 0)
					{
						throw new InvalidOperationException("Index has not been set in CSharpFormatter for type: " + baseObjects[i].GetType().Name);
					}
					indexes[i] = baseObjects[i].Index;
				}
				Array.Sort(indexes, baseObjects);
				return baseObjects;
			}
		}

		/// <summary>
		/// Copies top level items from this ICodeRoot into the given ICodeRoot
		/// </summary>
		/// <param name="codeRoot">The ICodeRoot to copy items into.</param>
		public void ShallowCloneInto(ICodeRoot codeRoot)
		{
			if ((codeRoot is CodeRoot) == false) return;
		}

		/// <summary>
		/// Searches for a child with the given unique id.
		/// </summary>
		/// <param name="uid"></param>
		/// <returns>The child with that id, or null if the uid doesn't exist as a child of this construct.</returns>
		public IBaseConstruct FindChild(string uid)
		{
			foreach (BaseConstruct bc in WalkChildren())
			{
				if (bc.DisplayName == uid)
					return bc;
			}

			return null;
		}

		/// <summary>
		/// Determines if two code roots are the same.
		/// </summary>
		/// <param name="comparisonCodeRoot">The CodeRoot to compare against.</param>
		/// <param name="depth">The depth to which we should compare. Signature will always
		/// return true, as CodeRoots have no signature. Outer will return true if the UsingStatements
		/// are the same, and complete will compare all children.</param>
		/// <returns>true if the given CodeRoot matches this one to the specified depth.</returns>
		public bool IsTheSame(ICodeRoot comparisonCodeRoot, ComparisonDepth depth)
		{
			return IsTheSame(comparisonCodeRoot as CodeRoot, depth);
		}

		/// <summary>
		/// Adds the give base construct as a child of this code root.
		/// </summary>
		/// <param name="child">The chidl object to add.</param>
		public void AddChild(IBaseConstruct child)
		{
			if (child == null)
				throw new ArgumentNullException("child");

			if (child is Namespace)
			{
				Namespaces.Add(child as Namespace);
			}
			else if (child is Struct)
			{
				Structs.Add(child as Struct);
			}
			else if (child is Interface)
			{
				Interfaces.Add(child as Interface);
			}
			else if (child is Enumeration)
			{
				Enums.Add(child as Enumeration);
			}
			else if (child is Delegate)
			{
				Delegates.Add(child as Delegate);
			}
			else if (child is UsingStatement)
			{
				UsingStatements.Add(child as UsingStatement);
				_usingStatementsAreSorted = false;
			}
			else if (child is AttributeSection)
			{
				Attributes.Add(child as AttributeSection);
			}
			else if (child is Class)
			{
				Classes.Add(child as Class);
			}
			else
			{
				throw new InvalidOperationException("Could not add child of type " + child.GetType());
			}

			if (child is BaseConstruct)
			{
				((BaseConstruct)child).Controller = controller;
			}
		}

		/// <summary>
		/// Determines if two code roots are the same.
		/// </summary>
		/// <param name="comparisonCodeRoot">The CodeRoot to compare against.</param>
		/// <param name="depth">The depth to which we should compare. Signature will always
		/// return true, as CodeRoots have no signature. Outer will return true if the UsingStatements
		/// are the same, and complete will compare all children.</param>
		/// <returns>true if the given CodeRoot matches this one to the specified depth.</returns>
		private bool IsTheSame(CodeRoot comparisonCodeRoot, ComparisonDepth depth)
		{
			if (comparisonCodeRoot == null)
				return false;

			//Comparers.BaseConstructComparer comparer = new Comparers.BaseConstructComparer();

			if (UsingStatementsTextBlock != comparisonCodeRoot.UsingStatementsTextBlock)
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".UsingStatements";
				return false;
			}

			if (depth == ComparisonDepth.Signature || depth == ComparisonDepth.Outer)
			{
				return true;
			}

			if (!Utility.BaseContructCollectionsAreTheSame(Classes.ToArray(), comparisonCodeRoot.Classes.ToArray()))
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".Classes";
				return false;
			}
			if (
				!Utility.BaseContructCollectionsAreTheSame(Namespaces.ToArray(), comparisonCodeRoot.Namespaces.ToArray()))
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".Namespaces";
				return false;
			}
			if (!Utility.BaseContructCollectionsAreTheSame(Structs.ToArray(), comparisonCodeRoot.Structs.ToArray()))
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".Structs";
				return false;
			}
			if (
				!Utility.BaseContructCollectionsAreTheSame(Interfaces.ToArray(), comparisonCodeRoot.Interfaces.ToArray()))
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".Interfaces";
				return false;
			}
			if (!Utility.BaseContructCollectionsAreTheSame(Enums.ToArray(), comparisonCodeRoot.Enums.ToArray()))
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".Enums";
				return false;
			}
			if (!Utility.BaseContructCollectionsAreTheSame(UsingStatements.ToArray(), comparisonCodeRoot.UsingStatements.ToArray()))
			{
				BaseConstruct.ComparisonDifference += GetType().Name + ".UsingStatements";
				return false;
			}
			return true;
		}

		public void AddUsingStatement(UsingStatement statement)
		{
			UsingStatements.Add(statement);
			_usingStatementsAreSorted = false;
		}

		/// <summary>
		/// Returns all of the IBaseConstructs in this tree in no particular order.
		/// </summary>
		/// <returns>All of the IBaseConstructs in this tree in no particular order.</returns>
		public IEnumerable<IBaseConstruct> WalkTree()
		{
			foreach (IBaseConstruct child in WalkChildren())
			{
				yield return child;
				foreach (var baseConstruct in child.WalkTree())
					yield return baseConstruct;
			}
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		public ReadOnlyCollection<IBaseConstruct> WalkChildren()
		{
			ArrayList objects = new ArrayList();
			objects.AddRange(Namespaces);
			objects.AddRange(Classes);
			objects.AddRange(Structs);
			objects.AddRange(Delegates);
			objects.AddRange(Interfaces);
			objects.AddRange(Enums);
			objects.AddRange(Attributes);
			objects.AddRange(UsingStatements);

			BaseConstruct[] baseObjects = (BaseConstruct[])objects.ToArray(typeof(BaseConstruct));
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			children.AddRange(baseObjects);
			return children.AsReadOnly();
		}
	}
}