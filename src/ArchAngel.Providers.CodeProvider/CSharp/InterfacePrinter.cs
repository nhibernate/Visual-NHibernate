using System;
using System.Collections;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Interface.
	/// </summary>
	[Serializable]
	public class InterfacePrinter : BasePrinter<Interface>
	{
		private Interface obj;

		public InterfacePrinter(Interface obj)
			: base(obj)
		{
			this.obj = obj;
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

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			StringBuilder sb = new StringBuilder();
			GetBodyText(sb);
			return sb.ToString();
		}

		private void GetBodyText(StringBuilder sb)
		{
			obj.Controller.IndentLevel++;

			if (!obj.Controller.Reorder)
			{
				ArrayList objects = new ArrayList();
				objects.AddRange(obj.Events);
				objects.AddRange(obj.Properties);
				objects.AddRange(obj.Methods);
				objects.AddRange(obj.Indexers);

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
				Type prevType = null;
				Type fieldType = typeof(Field);
				bool firstObject = true;

				foreach (BaseConstruct baseObject in baseObjects)
				{
					if (prevType == fieldType && !fieldType.IsInstanceOfType(baseObject))
					{
						// Make sure to insert a line after a field declaration, but not if it preceeded by another field
						sb.AppendLine();
					}
					else
					{
						prevType = baseObject.GetType();
					}
					if (!baseObject.Omit)
					{
						if (!firstObject)
						{
							//sb.AppendLine(baseObject.ToString());
							sb.Append(baseObject.ToString());
						}
						else
						{
							firstObject = false;
							string text = baseObject.ToString();

							// Remove the first linebreak
							while (text.Length > 0 && (text[0] == '\r' || text[0] == '\n'))
							{
								text = text.Remove(0, 1);
							}
							sb.Append(text);
						}
					}
				}
			}
			else
			{
				#region Write Events

				if (obj.Events.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Events");
					for (int i = 0; i < obj.Events.Count; i++)
					{
						sb.AppendLine(obj.Events[i].ToString());
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
					sb.AppendLine();
				}
				#endregion

				#region Write Properties

				if (obj.Properties.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Properties");

					for (int i = 0; i < obj.Properties.Count; i++)
					{
						sb.AppendLine(obj.Properties[i].ToString());
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
					sb.AppendLine();
				}
				#endregion

				#region Write Methods

				if (obj.Methods.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Methods");

					for (int i = 0; i < obj.Methods.Count; i++)
					{
						sb.AppendLine(obj.Methods[i].ToString());
						sb.AppendLine();
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
					sb.AppendLine();
				}
				#endregion

				#region Write Indexers

				if (obj.Indexers.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Indexers");

					for (int i = 0; i < obj.Indexers.Count; i++)
					{
						sb.AppendLine(obj.Indexers[i].ToString());
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
					sb.AppendLine();
				}
				#endregion
			}
			obj.Controller.IndentLevel--;
		}

		public string ToString(bool includeBody)
		{
			StringBuilder sb = new StringBuilder(10000);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.AppendFormat("interface {0}", obj.Name);

			if (obj.InterfaceBase.Length > 0)
			{
				sb.Append(" : " + obj.InterfaceBase);
			}
			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			sb.AppendLine("");
			sb.AppendLine(obj.Controller.Indent + "{");

			if (includeBody)
			{
				GetBodyText(sb);
			}
			sb.AppendLine();
			sb.AppendLine(obj.Controller.Indent + "}");
			return sb.ToString();
		}

		public override string Print()
		{
			return ToString(true);
		}
	}
}
