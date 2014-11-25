using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Namespace : BaseConstruct
	{
		public List<Class> Classes = new List<Class>();
		public List<Struct> Structs = new List<Struct>();
		public List<Enumeration> Enums = new List<Enumeration>();
		public List<Interface> Interfaces = new List<Interface>();
		public List<Delegate> Delegates = new List<Delegate>();
		public List<Event> Events = new List<Event>();
		public List<Region> Regions = new List<Region>();
		public List<Namespace> InnerNamespaces = new List<Namespace>();
		public List<UsingStatement> UsingStatements = new List<UsingStatement>();
		public CodeRoot ParentCodeRoot;

		public Namespace(Controller controller)
			: base(controller)
		{
			PreceedingBlankLines = -1;
		}

		public Namespace(Controller controller, BaseConstruct parentObject, string name, CodeRoot parentCodeRoot, CodeLanguage language, int nodeIndex)
			: this(controller)
		{
			ParentObject = parentObject;
			Name = name;
			ParentCodeRoot = parentCodeRoot;
			Language = language;
			Index = nodeIndex;
		}

		public Namespace(Namespace nsToCopyFrom)
			: base(null)
		{
			PreceedingBlankLines = -1;
			nsToCopyFrom.CloneInto(this);
			//foreach(UsingStatement us in nsToCopyFrom.UsingStatements)
			//{
			//    UsingStatements.Add((UsingStatement)us.Clone());
			//}
		}

		public Namespace(Controller controller, string name)
			: this(controller)
		{
			Name = name;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			Namespace ns = new Namespace(this);
			foreach (UsingStatement us in UsingStatements)
			{
				ns.AddChild(us.Clone());
			}
			return ns;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			return true;
		}

		/// <summary>
		/// Removes the given child object from this object.
		/// </summary>
		/// <param name="child"></param>
		/// <exception cref="InvalidOperationException">If the child type is unsupported (the parent doesn't have any of those objects) then an
		/// InvalidOperationExeception is thrown.</exception>
		/// <exception cref="ArgumentException">If the child's parent is not this object, then an ArgumentException is thrown.</exception>
		protected override void RemoveChildObjectInternal(BaseConstruct child)
		{
			if (child is Event)
			{
				if (Events.Remove(child as Event) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Class)
			{
				if (Classes.Remove(child as Class) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Struct)
			{
				if (Structs.Remove(child as Struct) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Enumeration)
			{
				if (Enums.Remove(child as Enumeration) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Interface)
			{
				if (Interfaces.Remove(child as Interface) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Delegate)
			{
				if (Delegates.Remove(child as Delegate) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Region)
			{
				if (Regions.Remove(child as Region) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Namespace)
			{
				if (InnerNamespaces.Remove(child as Namespace) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is UsingStatement)
			{
				if (UsingStatements.Remove(child as UsingStatement) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else
				base.RemoveChildObjectInternal(child);
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="child">The child object to add.</param>
		protected override void AddChildInternal(BaseConstruct child)
		{
			if (child == null)
				throw new InvalidOperationException("Cannot add null child");

			if (child is Event)
			{
				Events.Add(child as Event);
			}
			else if (child is Class)
			{
				Classes.Add(child as Class);
			}
			else if (child is Struct)
			{
				Structs.Add(child as Struct);
			}
			else if (child is Enumeration)
			{
				Enums.Add(child as Enumeration);
			}
			else if (child is Interface)
			{
				Interfaces.Add(child as Interface);
			}
			else if (child is Delegate)
			{
				Delegates.Add(child as Delegate);
			}
			else if (child is Region)
			{
				Regions.Add(child as Region);
			}
			else if (child is Namespace)
			{
				InnerNamespaces.Add(child as Namespace);
			}
			else if (child is UsingStatement)
			{
				UsingStatements.Add(child as UsingStatement);
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + child.GetType());
		}

		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();

			if (Controller.Reorder == false)
			{
				children.AddRange(SortedConstructs);
				return children.AsReadOnly();
			}

			ArrayList objects = new ArrayList();
			objects.AddRange(Delegates);
			objects.AddRange(Events);
			objects.AddRange(Enums);
			objects.AddRange(Structs);
			objects.AddRange(Interfaces);
			objects.AddRange(Classes);
			objects.AddRange(Regions);
			objects.AddRange(InnerNamespaces);

			BaseConstruct[] baseObjects = (BaseConstruct[])objects.ToArray(typeof(BaseConstruct));

			children.AddRange(baseObjects);
			return children.AsReadOnly();
		}

		public override bool IsTheSame(BaseConstruct comparisonNamespace)
		{
			return IsTheSame(comparisonNamespace as Namespace);
		}

		public override bool IsTheSame(BaseConstruct comparisonNamespace, ComparisonDepth depth)
		{
			return IsTheSame(comparisonNamespace as Namespace, depth);
		}

		private bool IsTheSame(Namespace comparisonNamespace)
		{
			return IsTheSame(comparisonNamespace, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Namespace comparisonNamespace, ComparisonDepth depth)
		{
			if (comparisonNamespace == null)
				return false;

			if (Name == comparisonNamespace.Name)
			{
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (!base.IsTheSame(comparisonNamespace, depth))
				{
					return false;
				}

				if (depth == ComparisonDepth.Outer)
				{
					return true;
				}

				if (!Utility.CollectionsAreTheSame(UsingStatements, comparisonNamespace.UsingStatements, new Comparers.UsingStatementComparer()))
				{
					ComparisonDifference += GetType().Name + ".UsingStatements";
					return false;
				}

				Comparers.BaseConstructComparer comparer = new Comparers.BaseConstructComparer();

				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Classes.ToArray(), comparisonNamespace.Classes.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Classes";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Structs.ToArray(), comparisonNamespace.Structs.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Structs";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Interfaces.ToArray(), comparisonNamespace.Interfaces.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Interfaces";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Enums.ToArray(), comparisonNamespace.Enums.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Enums";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Delegates.ToArray(), comparisonNamespace.Delegates.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Delegates";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(InnerNamespaces.ToArray(), comparisonNamespace.InnerNamespaces.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Namespaces";
					return false;
				}
				return true;
			}
			return false;
		}

		public BaseConstruct[] SortedConstructs
		{
			get
			{
				ArrayList objects = new ArrayList();
				objects.AddRange(Delegates);
				objects.AddRange(Events);
				objects.AddRange(Enums);
				objects.AddRange(Structs);
				objects.AddRange(Interfaces);
				objects.AddRange(Classes);
				objects.AddRange(Regions);
				objects.AddRange(InnerNamespaces);

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
	}
}
