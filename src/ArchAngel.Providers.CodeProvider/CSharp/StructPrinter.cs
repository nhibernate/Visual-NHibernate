using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
    /// <summary>
    /// Summary description for Struct.
    /// </summary>
    [Serializable]
    public class StructPrinter : BasePrinter<Struct>
    {
        private Struct obj;

        public StructPrinter(Struct obj) : base(obj)
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
        /// <returns>The outer text of the node.</returns>
        public override string GetOuterText()
        {
            return ToString(false);
        }

        private void GetBodyText(StringBuilder sb)
        {
           obj.Controller.IndentLevel++;

           if (!obj.Controller.Reorder)
            {
                Type prevType = null;
                Type fieldType = typeof(Field);
                bool firstObject = true;

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

                #region Write Constants

                if (obj.Constants.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Constants");

                    for (int i = 0; i < obj.Constants.Count; i++)
                    {
                        sb.AppendLine(obj.Constants[i].ToString());
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
                }
                #endregion

                #region Write Fields

                if (obj.Fields.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Fields");

                    for (int i = 0; i < obj.Fields.Count; i++)
                    {
                        sb.AppendLine(obj.Fields[i].ToString());
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
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
                }
                #endregion

                #region Write Indexer
                if (obj.StructIndexer != null)
                {
                    sb.AppendLine(obj.StructIndexer.ToString());
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
                }
                #endregion

                #region Write Functions

                if (obj.Functions.Count > 0)
                {
                    Utility.WriteRegionStart(obj.Controller, sb, "Functions");

                    for (int i = 0; i < obj.Functions.Count; i++)
                    {
                        if (!obj.Functions[i].Omit)
                        {
                            sb.AppendLine(obj.Functions[i].ToString());
                        }
                    }
                    Utility.WriteRegionEnd(obj.Controller, sb);
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
                }
                #endregion

                #region Classes
                for (int i = 0; i < obj.Classes.Count; i++)
                {
                    sb.AppendLine(obj.Classes[i].ToString());
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

            sb.AppendFormat("struct {0}", obj.Name);

            if (obj.GenericTypes.Count != 0)
            {
                sb.Append("<");
                foreach (string generic in obj.GenericTypes)
                {
                    sb.AppendFormat("{0}, ", generic);
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(">");
            }
            if (obj.BaseNames.Count > 0)
            {
                sb.Append(" : ");

                sb.Append(string.Join(", ", obj.BaseNames.ToArray()));

            }

            if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
            {
                sb.Append(" " + obj.Comments.TrailingComment);
            }
            sb.AppendLine("");
            sb.AppendLine("{");

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
