using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class NamespacePrinter : BasePrinter<Namespace>
	{
		private Namespace obj;

		public NamespacePrinter(Namespace obj)
			: base(obj)
		{
			this.obj = obj;
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

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public override string GetOuterText()
		{
			return ToString(false);
		}

		public void GetBodyText(StringBuilder sb)
		{
			obj.Controller.IndentLevel++;

			if (!obj.Controller.Reorder)
			{
				Type prevType = null;
				Type fieldType = typeof(Field);
				bool firstObject = true;

				foreach (UsingStatement us in obj.UsingStatements)
				{
					sb.AppendLine(us.ToString());
				}

				foreach (BaseConstruct baseObject in obj.SortedConstructs)
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
				#region Write Using Statements
				foreach (UsingStatement us in obj.UsingStatements)
				{
					sb.AppendLine(us.ToString());
				}
				#endregion

				#region Write Delegates

				if (obj.Delegates.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Delegates");

					for (int i = 0; i < obj.Delegates.Count; i++)
					{
						if (!obj.Delegates[i].Omit)
						{
							sb.AppendLine(obj.Delegates[i].ToString());
						}
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
				}
				#endregion

				#region Write Events

				if (obj.Events.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Events");

					for (int i = 0; i < obj.Events.Count; i++)
					{
						if (!obj.Events[i].Omit)
						{
							sb.AppendLine(obj.Events[i].ToString());
						}
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
				}
				#endregion

				#region Write Enums

				if (obj.Enums.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Enums");

					for (int i = 0; i < obj.Enums.Count; i++)
					{
						if (!obj.Enums[i].Omit)
						{
							sb.AppendLine(obj.Enums[i].ToString());
						}
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
				}
				#endregion

				#region Write Structs

				if (obj.Structs.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Structs");

					for (int i = 0; i < obj.Structs.Count; i++)
					{
						if (!obj.Structs[i].Omit)
						{
							sb.AppendLine(obj.Structs[i].ToString());
						}
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
				}
				#endregion

				#region Write Interfaces

				if (obj.Interfaces.Count > 0)
				{
					Utility.WriteRegionStart(obj.Controller, sb, "Interfaces");

					for (int i = 0; i < obj.Interfaces.Count; i++)
					{
						if (!obj.Interfaces[i].Omit)
						{
							sb.AppendLine(obj.Interfaces[i].ToString());
						}
					}
					Utility.WriteRegionEnd(obj.Controller, sb);
				}
				#endregion

				#region Classes
				for (int i = 0; i < obj.Classes.Count; i++)
				{
					sb.AppendLine(obj.Classes[i].ToString());
				}
				#endregion

				#region Regions
				for (int i = 0; i < obj.Regions.Count; i++)
				{
					sb.AppendLine(obj.Regions[i].ToString());
				}
				#endregion

				#region Namespaces
				for (int i = 0; i < obj.InnerNamespaces.Count; i++)
				{
					sb.AppendLine(obj.InnerNamespaces[i].ToString());
				}
				#endregion
			}
			obj.Controller.IndentLevel--;
		}

		public string ToString(bool includeBody)
		{
			StringBuilder sb = new StringBuilder(10000);

			sb.Append(obj.Controller.Indent + "namespace " + obj.Name);

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			sb.Append(Environment.NewLine + obj.Controller.Indent + "{" + Environment.NewLine);

			if (includeBody)
			{
				GetBodyText(sb);
			}
			sb.AppendLine(obj.Controller.Indent + "}");
			return sb.ToString();
		}

		public override string Print()
		{
			return ToString(true);
		}

	}
}
