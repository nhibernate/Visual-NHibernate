using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class PropertyAccessor : BaseConstruct, IBody
	{
		public AccessorTypes AccessorType = AccessorTypes.Get;
		protected string Text = "";
		public string Modifier = "";

		public PropertyAccessor(Controller controller)
			: base(controller)
		{
		}

		public PropertyAccessor(Controller controller, AccessorTypes accessorType)
			: base(controller)
		{
			AccessorType = accessorType;
		}

		public PropertyAccessor(Controller controller, AccessorTypes accessorType, string name)
			: base(controller)
		{
			AccessorType = accessorType;
			Name = name;
		}

		public PropertyAccessor(Controller controller, BaseConstruct parentObject, AccessorTypes accessorType, string text, CodeLanguage language)
			: base(controller)
		{
			ParentObject = parentObject;
			AccessorType = accessorType;
			Text = text;
			Language = language;
		}

		private PropertyAccessor(PropertyAccessor propToCopyFrom)
			: base(null)
		{
			propToCopyFrom.CloneInto(this);
			AccessorType = propToCopyFrom.AccessorType;
			Language = propToCopyFrom.Language;
			Modifier = propToCopyFrom.Modifier;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new PropertyAccessor(this);
		}

		public override string FullyQualifiedIdentifer
		{
			get
			{
				if (ParentObject != null && ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return ParentObject.FullyQualifiedIdentifer + BaseConstructConstants.FullyQualifiedIdentifierSeparator + (AccessorType == AccessorTypes.Get ? "get" : "set");

				return AccessorType == AccessorTypes.Get ? "get" : "set";
			}
		}

		public override string DisplayName
		{
			get { return AccessorType == AccessorTypes.Get ? "Get Accessor" : "Set Accessor"; }
		}

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public override string GetOuterText()
		{
			return ToString(false);
		}

		public string ToString(bool includeBody)
		{
			StringBuilder sb = new StringBuilder(100);

			string accessorName = AccessorType == AccessorTypes.Get ? "get" : "set";
			sb.Append(Controller.Indent);

			if (!string.IsNullOrEmpty(Modifier))
			{
				sb.Append(Modifier + " ");
			}
			sb.Append(accessorName);

			if (includeBody)
			{
				if (string.IsNullOrEmpty(BodyText))
				{
					BodyText = ";";
				}

				BodyText = Helper.RemoveTrailingLineBreaks(BodyText);
				BodyText = Helper.StandardizeLineBreaks(BodyText, Helper.LineBreaks.Unix);

				if (BodyText.IndexOf("\n") >= 0)
				{
					//Controller.IndentLevel--;
					BodyText = ArchAngel.Providers.CodeProvider.CSharp.Utility.ResetIndents(BodyText, Controller.Indent);
					//Controller.IndentLevel++;
				}
				else
				{
					int tabIndex = 0;

					while (BodyText[tabIndex] == '\t')
					{
						tabIndex++;
					}
					BodyText = BodyText.Substring(tabIndex);
				}

				sb.AppendLine(BodyText);
			}
			return sb.ToString();
		}

		public enum AccessorTypes
		{
			Get,
			Set
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}

		/// <summary>
		/// The body text of the construct. Does not include child nodes, and should only be
		/// used on leaf nodes.
		/// </summary>
		public string BodyText
		{
			get { return Text; }
			set { Text = value; }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			return ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeSingleItem(ref Modifier, ((PropertyAccessor)user).Modifier,
										   ((PropertyAccessor)newgen).Modifier, ((PropertyAccessor)prevgen).Modifier);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as PropertyAccessor);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as PropertyAccessor, depth);
		}

		private bool IsTheSame(PropertyAccessor comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(PropertyAccessor comparisonInterfaceAccessor, ComparisonDepth depth)
		{
			if (comparisonInterfaceAccessor == null)
				return false;

			if (AccessorType == comparisonInterfaceAccessor.AccessorType)
			{
				// Function names are the same, so now compare the class names
				// TODO: Parent comparison
				//if (ParentObject.IsTheSame(comparisonInterfaceAccessor.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonInterfaceAccessor, depth))
					{
						return false;
					}

					if (depth == ComparisonDepth.Outer)
					{
						return true;
					}

					if (Text != comparisonInterfaceAccessor.Text)
					{
						ComparisonDifference += GetType().Name + ".Text";
						return false;
					}
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
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to a PropertyAccessor");
		}

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			return Text;
		}

	}
}