using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
    /// <summary>
    /// Summary description for Class.
    /// </summary>
    [Serializable]
    public class ClassPrinter : BasePrinter<Class>
    {
        private readonly Class obj;
        public ClassPrinter(Class obj) : base(obj)
        {
            this.obj = obj;
        }

        /// <summary>
        /// Returns the body text of the class.
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

        private void GetBodyText(StringBuilder sb)
        {
            if (!obj.Controller.Reorder)
            {
                Type prevType = null;
                Type fieldType = typeof(Field);
                bool firstObject = true;

                foreach (BaseConstruct baseObject in obj.SortedConstructs)
                {
                    if (prevType == fieldType && !fieldType.IsInstanceOfType(baseObject))
                    {
                        if (!obj.Controller.MaintainWhitespace)
                        {
                            // Make sure to insert a line after a field declaration, but not if it preceeded by another field
                            sb.AppendLine();
                        }
                        prevType = baseObject.GetType();
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

                            if (text.Length > 1 && (text[0] == '\r' && text[1] == '\n'))
                            {
                                text = text.Remove(0, 2);
                            }
                            else if (text.Length > 0 && (text[0] == '\n' || text[0] == '\r'))
                            {
                                text = text.Remove(0, 1);
                            }
                            //// Remove the first linebreak
                            //while (text.Length > 0 && (text[0] == '\r' || text[0] == '\n'))
                            //{
                            //    text = text.Remove(0, 1);
                            //}
                            sb.Append(text);
                        }
                    }
                }
            }
            else
            {
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
                    sb.AppendLine();
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
                    sb.AppendLine();
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
                    sb.AppendLine();
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
                    sb.AppendLine();
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
                    sb.AppendLine();
                }
                #endregion

                #region Write Constants

                if (obj.Constants.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Constants");

                    for (int i = 0; i < obj.Constants.Count; i++)
                    {
                        sb.AppendLine(obj.Constants[i].ToString());
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
                    sb.AppendLine();
                }
                #endregion

                #region Write Fields

                if (obj.Fields.Count > 0)
                {
                    bool regionAdded = false;

                    for (int i = 0; i < obj.Fields.Count; i++)
                    {
                        if (!regionAdded)
                        {
                            Utility.WriteRegionStart(obj.Controller, sb, "Fields");
                            regionAdded = true;
                        }
                        sb.AppendLine(obj.Fields[i].ToString());
                    }
                    if (regionAdded)
                    {
                        Utility.WriteRegionEnd(obj.Controller, sb);
                        sb.AppendLine();
                    }
                }
                #endregion

                #region Write Constructors

                if (obj.Constructors.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Constructors");

                    for (int i = 0; i < obj.Constructors.Count; i++)
                    {
                        sb.AppendLine(obj.Constructors[i].ToString());
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
                    sb.AppendLine();
                }
                #endregion

				#region Write Destructors

                if (obj.Destructors.Count > 0)
				{
                    Utility.WriteRegionStart(obj.Controller, sb, "Destructors");

                    for (int i = 0; i < obj.Destructors.Count; i++)
					{
                        sb.AppendLine(obj.Destructors[i].ToString());
					}
                    Utility.WriteRegionEnd(obj.Controller, sb);
					sb.AppendLine();
				}
				#endregion

                #region Write Indexer
                if (obj.ClassIndexer != null)
                {
                    sb.AppendLine(obj.ClassIndexer.ToString());
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

                #region Write Functions

                if (obj.Functions.Count > 0)
                {
                    bool regionAdded = false;

                    for (int i = 0; i < obj.Functions.Count; i++)
                    {
                        if (!obj.Functions[i].Omit)
                        {
                            if (!regionAdded)
                            {
                                Utility.WriteRegionStart(obj.Controller, sb, "Functions");
                                regionAdded = true;
                            }
                            sb.AppendLine(obj.Functions[i].ToString());
                        }
                    }
                    if (regionAdded)
                    {
                        Utility.WriteRegionEnd(obj.Controller, sb);
                        sb.AppendLine();
                    }
                }
                #endregion

                #region Write Operators

                if (obj.Operators.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Operators");

                    for (int i = 0; i < obj.Operators.Count; i++)
                    {
                        if (!obj.Operators[i].Omit)
                        {
                            sb.AppendLine(obj.Operators[i].ToString());
                        }
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
                    sb.AppendLine();
                }
                #endregion

				#region Write Regions

                if (obj.Regions.Count > 0)
				{
                    for (int i = 0; i < obj.Regions.Count; i++)
					{
                        if (!obj.Regions[i].Omit)
						{
                            sb.AppendLine(obj.Regions[i].ToString());
						}
					}
					sb.AppendLine();
				}
				#endregion

                #region Write Inner Classes

                if (obj.InnerClasses.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Inner Classes");

                    for (int i = 0; i < obj.InnerClasses.Count; i++)
                    {
                        if (!obj.InnerClasses[i].Omit)
                        {
                            sb.AppendLine(obj.InnerClasses[i].ToString());
                        }
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
                    sb.AppendLine();
                }
                #endregion
            }
            return;// sb.ToString();
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

            if (obj.IsPartial)
            {
                sb.Append("partial ");
            }

            #region Generic Types
            string genericTypeString = "";

            for (int i = 0; i < obj.GenericTypes.Count; i++)
            {
                genericTypeString += obj.GenericTypes[i];

                if (i < obj.GenericTypes.Count - 1) { genericTypeString += ", "; }
            }
            if (!String.IsNullOrEmpty(genericTypeString))
            {
                genericTypeString = "<" + genericTypeString + ">";
            }
            #endregion

            sb.AppendFormat("class {0}{1}", obj.Name, genericTypeString);

            if (obj.BaseNames.Count > 0)
            {
                sb.Append(" : ");

                for (int i = 0; i < obj.BaseNames.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(obj.BaseNames[i]);
                }
            }
            if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
            {
                sb.Append(" " + obj.Comments.TrailingComment);
            }
            sb.AppendLine();
            sb.AppendLine(obj.Controller.Indent + "{");

            if (includeBody)
            {
                obj.Controller.IndentLevel++;
                GetBodyText(sb);
                obj.Controller.IndentLevel--;
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
