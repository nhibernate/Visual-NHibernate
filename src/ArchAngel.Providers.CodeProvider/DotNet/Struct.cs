using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Struct : BaseConstruct, IVisibilityModifiers, IFields
	{
		protected List<Field> fields = new List<Field>();
		public List<Function> Functions = new List<Function>();
		public List<Property> Properties = new List<Property>();
		public List<Event> Events = new List<Event>();
		public Indexer StructIndexer;
		public List<Constant> Constants = new List<Constant>();
		public List<Constructor> Constructors = new List<Constructor>();
		public List<Operator> Operators = new List<Operator>();
		public List<Struct> Structs = new List<Struct>();
		public List<Enumeration> Enums = new List<Enumeration>();
		public List<Interface> Interfaces = new List<Interface>();
		public List<Delegate> Delegates = new List<Delegate>();
		public List<Class> Classes = new List<Class>();
		public List<string> BaseNames = new List<string>();
		public List<string> GenericTypes = new List<string>();
		private List<string> _Modifiers = new List<string>();

		public Struct(Controller controller)
			: base(controller)
		{
		}

		public Struct(Controller controller, string name, string baseName, string genericType, string modifier)
			: base(controller)
		{
			Name = name;
			BaseNames.Add(baseName);
			GenericTypes.Add(genericType);
			_Modifiers.Add(modifier);
		}

		public Struct(Controller controller, BaseConstruct parentObject, string name, List<string> modifiers, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Name = name;
			Language = language;
			Modifiers = modifiers;
			Index = nodeIndex;
		}

		private Struct(Struct structToCopyFrom)
			: base(null)
		{
			structToCopyFrom.CloneInto(this);
			Modifiers = new List<string>(structToCopyFrom.Modifiers);
			BaseNames = new List<string>(structToCopyFrom.BaseNames);
			GenericTypes = new List<string>(structToCopyFrom.GenericTypes);
		}

		public Struct(Controller controller, string name)
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
			return new Struct(this);
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
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
				objects.AddRange(Constants);
				objects.AddRange(fields);
				objects.AddRange(Constructors);

				if (StructIndexer != null) { objects.Add(StructIndexer); }

				objects.AddRange(Properties);
				objects.AddRange(Functions);
				objects.AddRange(Operators);
				objects.AddRange(Classes);

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

		public ReadOnlyCollection<Field> Fields
		{
			get { return fields.AsReadOnly(); }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Struct userBC = (Struct)user, newgenBC = (Struct)newgen, prevgenBC = (Struct)prevgen;

			// BaseNames
			if (!Utility.MergeStringCollections(BaseNames, userBC.BaseNames, newgenBC.BaseNames, prevgenBC.BaseNames))
				return false;
			// GenericTypes
			if (!Utility.MergeStringCollections(GenericTypes, userBC.GenericTypes, newgenBC.GenericTypes, prevgenBC.GenericTypes))
				return false;
			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
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
			/*
			Constant
			Field
			Function
			Property
			Event
			Indexer
			Operator
			Constructor
			Class
			Struct
			Enumeration
			Interface
			Delegate
			RegionStart
			*/

			if (child is Constant)
			{
				if (Constants.Remove(child as Constant) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Field)
			{
				if (fields.Remove(child as Field) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Function)
			{
				if (Functions.Remove(child as Function) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Property)
			{
				if (Properties.Remove(child as Property) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Event)
			{
				if (Events.Remove(child as Event) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Indexer)
			{
				if (StructIndexer == child as Indexer)
					StructIndexer = null;
				else
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Operator)
			{
				if (Operators.Remove(child as Operator) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Constructor)
			{
				if (Constructors.Remove(child as Constructor) == false)
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

			if (child is Constant)
			{
				Constants.Add(child as Constant);
			}
			else if (child is Field)
			{
				fields.Add(child as Field);
			}
			else if (child is Function)
			{
				Functions.Add(child as Function);
			}
			else if (child is Property)
			{
				Properties.Add(child as Property);
			}
			else if (child is Event)
			{
				Events.Add(child as Event);
			}
			else if (child is Indexer)
			{
				StructIndexer = child as Indexer;
			}
			else if (child is Operator)
			{
				Operators.Add(child as Operator);
			}
			else if (child is Constructor)
			{
				Constructors.Add(child as Constructor);
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

			for (int i = 0; i < Delegates.Count; i++)
			{
				children.Add(Delegates[i]);
			}

			for (int i = 0; i < Events.Count; i++)
			{
				children.Add(Events[i]);
			}

			for (int i = 0; i < Enums.Count; i++)
			{
				children.Add(Enums[i]);
			}

			for (int i = 0; i < Structs.Count; i++)
			{
				children.Add(Structs[i]);
			}

			for (int i = 0; i < Structs.Count; i++)
			{
				children.Add(Structs[i]);
			}

			for (int i = 0; i < Interfaces.Count; i++)
			{
				children.Add(Interfaces[i]);
			}

			for (int i = 0; i < Constants.Count; i++)
			{
				children.Add(Constants[i]);
			}

			for (int i = 0; i < fields.Count; i++)
			{
				children.Add(fields[i]);
			}

			for (int i = 0; i < Constructors.Count; i++)
			{
				children.Add(Constructors[i]);
			}

			if (StructIndexer != null)
			{
				children.Add(StructIndexer);
			}

			for (int i = 0; i < Properties.Count; i++)
			{
				children.Add(Properties[i]);
			}

			for (int i = 0; i < Functions.Count; i++)
			{
				children.Add(Functions[i]);
			}

			for (int i = 0; i < Operators.Count; i++)
			{
				children.Add(Operators[i]);
			}

			return children.AsReadOnly();
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Struct);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Struct, depth);
		}

		private bool IsTheSame(Struct comparisonStruct)
		{
			return IsTheSame(comparisonStruct, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Struct comparisonStruct, ComparisonDepth depth)
		{
			if (comparisonStruct == null)
				return false;

			if (Name == comparisonStruct.Name)
			{
				// Function names are the same, so now compare the class names
				//Class thisParentClass = (Class)this.ParentObject;
				//Class comparisonParentClass = (Class)comparisonStruct.ParentObject;

				//if (thisParentClass.IsTheSame(comparisonParentClass))
				if ((ParentObject == null && comparisonStruct.ParentObject == null) ||
					(ParentObject.GetType() == comparisonStruct.ParentObject.GetType() &&
					 ParentObject.Name == comparisonStruct.ParentObject.Name))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonStruct, depth))
					{
						return false;
					}
					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonStruct.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}

					return true;
				}
			}
			return false;
		}
	}
}