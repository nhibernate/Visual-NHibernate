using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
    /// <summary>
    /// Summary description for InterfaceProperty.
    /// </summary>
    [Serializable]
    public class InterfacePropertyPrinter : BasePrinter<InterfaceProperty>
    {
        private readonly InterfaceProperty obj;
        public InterfacePropertyPrinter(InterfaceProperty obj) : base(obj)
        {
            this.obj = obj;
        }

        public override string FullyQualifiedName
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string Print()
        {
            StringBuilder sb = new StringBuilder(500);

            sb.Append(obj.Controller.Indent);

            if (obj.HasNewKeyword)
            {
                sb.Append("new ");
            }
            sb.AppendFormat("{0} {1}", obj.DataType, obj.Name);
            sb.Append(" { ");

            #region Write Accessors
            if (obj.GetAccessor != null)
            {
                sb.Append(obj.GetAccessor.ToString());
            }
            if (obj.SetAccessor != null)
            {
                sb.Append(obj.SetAccessor.ToString());
            }
            #endregion

            sb.AppendLine(" }");

            if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
            {
                sb.Append(" " + obj.Comments.TrailingComment);
            }
            return sb.ToString();
        }
    }
}
