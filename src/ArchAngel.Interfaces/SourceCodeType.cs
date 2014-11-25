
namespace ArchAngel.Interfaces
{
	public class SourceCodeType
	{
		public SourceCodeType(string value)
		{
			Value = value;
		}

		public string Value { get; set; }

		public override string ToString()
		{
			return Value;
		}
	}
}
