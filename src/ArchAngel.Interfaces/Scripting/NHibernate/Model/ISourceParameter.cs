
namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class ISourceParameter
	{
		public ISourceParameter()
		{
		}

		public ISourceParameter(string name, string dataType)
		{
			Name = name;
			DataType = dataType;
		}

		public string Name { get; set; }
		public string DataType { get; set; }
	}
}
