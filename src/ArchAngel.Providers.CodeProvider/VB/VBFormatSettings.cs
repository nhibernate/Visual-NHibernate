
namespace ArchAngel.Providers.CodeProvider.VB
{
	public class VBFormatSettings
	{
		/// <summary>
		/// If true, does not output empty statements (;)
		/// </summary>
		public bool OmitEmptyStatements = true;

		/// <summary>
		/// We set Controller.Reorder to this value.
		/// </summary>
		public bool ReorderBaseConstructs = false;

		/// <summary>
		/// If true, will make sure that line breaks present in the previous file will be present in this one.
		/// if two functions were defined line so:
		/// public void Function1() { }
		/// 
		/// 
		/// public void Function2() {}
		/// 
		/// then they would be output line this as well.
		/// </summary>
		public bool MaintainWhitespace = true;
	}
}
