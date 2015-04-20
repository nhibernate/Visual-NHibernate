
namespace ArchAngel.Interfaces.Scripting.DatabaseChanges
{
	public class IChangedColumn
	{
		public string OldName { get; set; }
		public string NewName { get; set; }
		public string OldType { get; set; }
		public string NewType { get; set; }
		public long NewLength { get; set; }
	}
}
