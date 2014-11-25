using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public abstract class BasePrinter<T> : IPrinter where T : BaseConstruct
	{
		private readonly T obj;

		protected BasePrinter(T obj)
		{
			this.obj = obj;
		}

		public virtual string FullyQualifiedName
		{
			get { return (obj.ParentObject != null) ? obj.ParentObject.FullyQualifiedName + "." + obj.Name : obj.Name; }
		}

		public abstract string Print();

		/// <summary>
		/// Gets the inner text of the node, if it has any. If it doesn't, it returns string.Empty.
		/// </summary>
		/// <returns>The inner text of the node, if it has any. If it doesn't, it returns string.Empty.</returns>
		public virtual string GetInnerText()
		{
			return string.Empty;
		}

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public virtual string GetOuterText()
		{
			return obj.ToString();
		}
	}
}
