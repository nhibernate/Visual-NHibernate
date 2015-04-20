
namespace ArchAngel.Interfaces
{
	public class SourceCodeMultiLineType
	{
		public SourceCodeMultiLineType(string value)
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
