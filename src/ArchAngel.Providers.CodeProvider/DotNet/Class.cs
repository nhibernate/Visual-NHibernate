using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	/// <summary>
	/// Summary description for Class.
	/// </summary>
	[Serializable]
	public class Class : BaseConstruct, IVisibilityModifiers, IFields
	{
		protected List<Constant> constants = new List<Constant>();
		protected List<Field> fields = new List<Field>();
		protected List<Function> functions = new List<Function>();
		protected List<Property> properties = new List<Property>();
		protected List<Event> events = new List<Event>();
		protected Indexer classIndexer;
		protected List<Operator> operators = new List<Operator>();
		protected List<Constructor> constructors = new List<Constructor>();
		protected List<Destructor> destructors = new List<Destructor>();
		// TODO: Static Constructor
		protected List<Class> innerClasses = new List<Class>();
		protected List<Struct> structs = new List<Struct>();
		protected List<Enumeration> enums = new List<Enumeration>();
		protected List<Interface> interfaces = new List<Interface>();
		protected List<Delegate> delegates = new List<Delegate>();
		protected List<Region> regions = new List<Region>();
		public List<string> BaseNames = new List<string>();
		public List<string> GenericTypes = new List<string>();
		protected List<string> _Modifiers = new List<string>();
		public bool IsPartial = false;
		public string GenericConstraintClause;

		public Class(Controller controller, string name, List<string> modifiers, List<string> baseNames, BaseConstruct.CodeLanguage language, BaseConstruct parentObject, int nodeIndex)
			: base(controller)
		{
			Name = name;
			BaseNames = baseNames;
			Language = language;
			Modifiers = modifiers;
			ParentObject = parentObject;
			Index = nodeIndex;
		}

		public Class(Controller controller, string name, List<string> modifiers, List<string> baseNames, BaseConstruct.CodeLanguage language, BaseConstruct parentObject)
			: base(controller)
		{
			Name = name;
			BaseNames = baseNames;
			Language = language;
			Modifiers = modifiers;
			ParentObject = parentObject;
		}

		public Class(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		private Class(Class classToCopy)
			: base(null)
		{
			classToCopy.CloneInto(this);
			BaseNames = new List<string>(classToCopy.BaseNames);
			GenericTypes = new List<string>(classToCopy.GenericTypes);
			Modifiers = new List<string>(classToCopy.Modifiers);
			IsPartial = classToCopy.IsPartial;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Class(this);
		}


		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		public void RemoveField(Field field)
		{
			for (int i = 0; i < Fields.Count; i++)
			{
				if (fields[i].IsTheSame(field))
				{
					fields.RemoveAt(i);
					return;
				}
			}
		}

		public void AddField(Field field)
		{
			AddChildInternal(field);
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="child"></param>
		protected override void AddChildInternal(BaseConstruct child)
		{
			if (child == null)
				throw new InvalidOperationException("Cannot add null child");

			if (child is Constant)
				constants.Add(child as Constant);
			else if (child is Field)
				fields.Add(child as Field);
			else if (child is Function)
				functions.Add(child as Function);
			else if (child is Property)
				properties.Add(child as Property);
			else if (child is Event)
				events.Add(child as Event);
			else if (child is Indexer)
				classIndexer = child as Indexer;
			else if (child is Operator)
				operators.Add(child as Operator);
			else if (child is Constructor)
				constructors.Add(child as Constructor);
			else if (child is Destructor)
				destructors.Add(child as Destructor);
			else if (child is Class)
				innerClasses.Add(child as Class);
			else if (child is Struct)
				structs.Add(child as Struct);
			else if (child is Enumeration)
				enums.Add(child as Enumeration);
			else if (child is Interface)
				interfaces.Add(child as Interface);
			else if (child is Delegate)
				delegates.Add(child as Delegate);
			else if (child is Region)
				regions.Add(child as Region);
			else
				throw new InvalidOperationException("Cannot add child of type " + child.GetType());
		}

		/// <summary>
		/// This method should merge the minor features of a base construct, like modifiers or parameter names.
		/// </summary>
		/// <param name="user">The User's version of the object.</param>
		/// <param name="newgen">The Template's version of the object.</param>
		/// <param name="prevgen">The previously generated version of the object.</param>
		/// <returns>True if the merge was successful. If it returns false, the user will have to manually merge it. </returns>
		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Class userClass = (Class)user, newgenClass = (Class)newgen, prevgenClass = (Class)prevgen;

			/*
			public bool IsPartial = false;
			*/
			// Modifiers
			if (!Utility.MergeModifiers(this, userClass, newgenClass, prevgenClass))
				return false;

			// Base Names
			if (!Utility.MergeStringCollections(BaseNames, userClass.BaseNames, newgenClass.BaseNames, prevgenClass.BaseNames))
				return false;

			// Generic Types
			if (!Utility.MergeStringCollections(GenericTypes, userClass.GenericTypes, newgenClass.GenericTypes, prevgenClass.GenericTypes))
				return false;

			// IsPartial
			if (userClass.IsPartial == newgenClass.IsPartial) IsPartial = userClass.IsPartial;
			else if (userClass.IsPartial == prevgenClass.IsPartial) IsPartial = newgenClass.IsPartial;
			else if (newgenClass.IsPartial == prevgenClass.IsPartial) IsPartial = userClass.IsPartial;

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
				if (constants.Remove(child as Constant) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Field)
			{
				if (fields.Remove(child as Field) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Function)
			{
				if (functions.Remove(child as Function) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Property)
			{
				if (properties.Remove(child as Property) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Event)
			{
				if (events.Remove(child as Event) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Indexer)
			{
				if (classIndexer == child as Indexer)
					classIndexer = null;
				else
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Operator)
			{
				if (operators.Remove(child as Operator) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Constructor)
			{
				if (constructors.Remove(child as Constructor) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Class)
			{
				if (innerClasses.Remove(child as Class) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Struct)
			{
				if (structs.Remove(child as Struct) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Enumeration)
			{
				if (enums.Remove(child as Enumeration) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Interface)
			{
				if (interfaces.Remove(child as Interface) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Delegate)
			{
				if (delegates.Remove(child as Delegate) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is Region)
			{
				if (regions.Remove(child as Region) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else
				base.RemoveChildObjectInternal(child);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Class, depth);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Class);
		}

		private bool IsTheSame(Class comparisonClass)
		{
			return IsTheSame(comparisonClass, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Class comparisonClass, ComparisonDepth depth)
		{
			if (comparisonClass == null)
				return false;

			if (Name == comparisonClass.Name)
			{
				//if ((ParentObject == null && comparisonClass.ParentObject == null) ||
				//    (ParentObject.GetType() == comparisonClass.ParentObject.GetType() &&
				//    ParentObject.Name == comparisonClass.ParentObject.Name))
				{
					if (depth == ComparisonDepth.Signature)
						return true;

					if (!base.IsTheSame(comparisonClass, depth))
						return false;

					if (BaseNames != comparisonClass.BaseNames &&
						IsPartial != comparisonClass.IsPartial)
					{
						return false;
					}

					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonClass.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}
					if (!Utility.StringCollectionsAreTheSame(GenericTypes, comparisonClass.GenericTypes))
					{
						ComparisonDifference += GetType().Name + ".GenericTypes";
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public BaseConstruct[] SortedConstructs
		{
			get
			{
				ArrayList objects = new ArrayList();
				objects.AddRange(delegates);
				objects.AddRange(events);
				objects.AddRange(enums);
				objects.AddRange(structs);
				objects.AddRange(interfaces);
				objects.AddRange(constants);
				objects.AddRange(fields);
				objects.AddRange(constructors);
				objects.AddRange(destructors);
				objects.AddRange(regions);

				if (ClassIndexer != null) { objects.Add(classIndexer); }

				objects.AddRange(properties);
				objects.AddRange(functions);
				objects.AddRange(operators);
				objects.AddRange(innerClasses);
				objects.AddRange(EmptyPlaceholders);

				BaseConstruct[] baseObjects = (BaseConstruct[])objects.ToArray(typeof(BaseConstruct));
				int[] indexes = new int[baseObjects.Length];

				for (int i = 0; i < baseObjects.Length; i++)
				{
					if (baseObjects[i].Index < 0)
						throw new InvalidOperationException("Index has not been set in CSharpFormatter for type: " + baseObjects[i].GetType().Name);

					indexes[i] = baseObjects[i].Index;
				}
				Array.Sort(indexes, baseObjects);
				return baseObjects;
			}
		}

		public List<Constant> Constants
		{
			get { return constants; }
			set { constants = value; }
		}

		/// <summary>
		/// A collection of all functions in this class, including any inside regions.
		/// </summary>
		/// <remarks>To add/remove a function, use Add/RemoveChild()</remarks>
		public ReadOnlyCollection<Function> Functions
		{
			get
			{
				return RecursiveGetAllOfType(functions);
			}
		}

		/// <summary>
		/// A collection of all fields in this class, including any inside regions.
		/// </summary>
		/// <remarks>To add/remove a field, use Add/RemoveChild()</remarks>
		public ReadOnlyCollection<Field> Fields
		{
			get
			{
				return RecursiveGetAllOfType(fields);
			}
		}

		/// <summary>
		/// A collection of all inner classes in this class, including any inside regions.
		/// </summary>
		/// <remarks>To add/remove an inner class, use Add/RemoveChild()</remarks>
		public ReadOnlyCollection<Class> InnerClasses
		{
			get
			{
				return RecursiveGetAllOfType(innerClasses);
			}
		}

		private ReadOnlyCollection<T> RecursiveGetAllOfType<T>(IEnumerable<T> directObjects) where T : class
		{
			List<T> funcs = new List<T>();
			funcs.AddRange(directObjects);
			SearchAndAddFromRegions(funcs);
			return funcs.AsReadOnly();
		}

		private void SearchAndAddFromRegions<T>(List<T> list) where T : class
		{
			foreach (Region region in regions)
				list.AddRange(region.GetConstructsOfType<T>());
		}

		public List<Property> Properties
		{
			get { return properties; }
			set { properties = value; }
		}

		public List<Event> Events
		{
			get { return events; }
			set { events = value; }
		}

		public Indexer ClassIndexer
		{
			get { return classIndexer; }
			set { classIndexer = value; }
		}

		public List<Operator> Operators
		{
			get { return operators; }
			set { operators = value; }
		}

		public List<Constructor> Constructors
		{
			get { return constructors; }
			set { constructors = value; }
		}

		public List<Destructor> Destructors
		{
			get { return destructors; }
			set { destructors = value; }
		}

		public List<Struct> Structs
		{
			get { return structs; }
			set { structs = value; }
		}

		public List<Enumeration> Enums
		{
			get { return enums; }
			set { enums = value; }
		}

		public List<Interface> Interfaces
		{
			get { return interfaces; }
			set { interfaces = value; }
		}

		public List<Delegate> Delegates
		{
			get { return delegates; }
			set { delegates = value; }
		}

		public List<Region> Regions
		{
			get { return regions; }
			set { regions = value; }
		}

		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();

			if (Controller.Reorder == false)
			{
				children.AddRange(SortedConstructs);
				return children.AsReadOnly();
			}

			for (int i = 0; i < delegates.Count; i++)
				children.Add(delegates[i]);

			for (int i = 0; i < events.Count; i++)
				children.Add(events[i]);

			for (int i = 0; i < enums.Count; i++)
				children.Add(enums[i]);

			for (int i = 0; i < structs.Count; i++)
				children.Add(structs[i]);

			for (int i = 0; i < interfaces.Count; i++)
				children.Add(interfaces[i]);

			for (int i = 0; i < constants.Count; i++)
				children.Add(constants[i]);

			for (int i = 0; i < fields.Count; i++)
				children.Add(fields[i]);

			for (int i = 0; i < constructors.Count; i++)
				children.Add(constructors[i]);

			for (int i = 0; i < destructors.Count; i++)
				children.Add(destructors[i]);

			if (classIndexer != null)
				children.Add(classIndexer);

			for (int i = 0; i < properties.Count; i++)
				children.Add(properties[i]);

			for (int i = 0; i < functions.Count; i++)
				children.Add(functions[i]);

			for (int i = 0; i < operators.Count; i++)
				children.Add(operators[i]);

			for (int i = 0; i < innerClasses.Count; i++)
				children.Add(innerClasses[i]);

			for (int i = 0; i < regions.Count; i++)
				children.Add(regions[i]);

			return children.AsReadOnly();
		}
	}

	public interface IFields
	{
		ReadOnlyCollection<Field> Fields
		{
			get;
		}
	}
}
