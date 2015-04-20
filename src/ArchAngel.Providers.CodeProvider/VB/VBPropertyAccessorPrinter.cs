using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBPropertyAccessorPrinter : IPrinter
	{
		private PropertyAccessor obj;

		public VBPropertyAccessorPrinter(PropertyAccessor obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedIdentifer
		{
			get { throw new System.NotImplementedException(); }
		}

		public string DisplayName
		{
			get { throw new System.NotImplementedException(); }
		}

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public string GetOuterText()
		{
			throw new System.NotImplementedException();
		}

		public string GetInnerText()
		{
			throw new System.NotImplementedException();
		}

		public string ToString(bool includeBody)
		{
			throw new System.NotImplementedException();
		}

		public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Print()
		{
			throw new System.NotImplementedException();
		}
	}
}
