using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Event : BaseConstruct, IVisibilityModifiers
	{
		public DataType DataType;
		private List<string> _Modifiers = new List<string>();
		public string AddAccessorText = "";
		public string RemoveAccessorText = "";
		public string InitialValue = "";

		public Event(Controller controller)
			: base(controller)
		{
		}

		public Event(Controller controller, DataType type, string name, string modifier)
			: base(controller)
		{
			Name = name;
			DataType = type;
			_Modifiers.Add(modifier);
		}

		public Event(Controller controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		private Event(Event eventToCopyFrom)
			: base(null)
		{
			eventToCopyFrom.CloneInto(this);
			DataType = eventToCopyFrom.DataType.Clone();
			foreach (string mod in eventToCopyFrom.Modifiers)
			{
				Modifiers.Add(mod);
			}
			AddAccessorText = eventToCopyFrom.AddAccessorText;
			RemoveAccessorText = eventToCopyFrom.RemoveAccessorText;
			InitialValue = eventToCopyFrom.InitialValue;
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Event(this);
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Event userBC = (Event)user, newgenBC = (Event)newgen, prevgenBC = (Event)prevgen;

			// DataType;
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeDataType(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// Modifiers
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			// AddAccessorText
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeSingleItem(ref AddAccessorText, userBC.AddAccessorText, newgenBC.AddAccessorText, prevgenBC.AddAccessorText))
				return false;
			// RemoveAccessorText
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeSingleItem(ref RemoveAccessorText, userBC.RemoveAccessorText, newgenBC.RemoveAccessorText, prevgenBC.RemoveAccessorText))
				return false;
			// InitialValue
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeSingleItem(ref InitialValue, userBC.InitialValue, newgenBC.InitialValue, prevgenBC.InitialValue))
				return false;
			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Event);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Event, depth);
		}

		private bool IsTheSame(Event comparisonEvent)
		{
			return IsTheSame(comparisonEvent, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Event comparisonEvent, ComparisonDepth depth)
		{
			if (comparisonEvent == null)
			{
				return false;
			}

			if (Name == comparisonEvent.Name)
			{
				// Function names are the same, so now compare the class names
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}
					if (DataType != comparisonEvent.DataType)
					{
						return false;
					}
					if (!base.IsTheSame(comparisonEvent, depth))
					{
						return false;
					}
					if (InitialValue != comparisonEvent.InitialValue)
					{
						ComparisonDifference += GetType().Name + ".InitialValue";
						return false;
					}
					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonEvent.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}
					// Why are these checks on parents here?
					//if (!ParentObject.IsTheSame(comparisonEvent.ParentObject))
					//{

					//}
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			return children.AsReadOnly();
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}

	}
}
