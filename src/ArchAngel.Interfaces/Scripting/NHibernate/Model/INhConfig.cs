
namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class INhConfig
	{
		public string Driver { get; set; }
		public string Dialect { get; set; }
		public string DialectSpatial { get; set; }
		public string ConnectionString { get; set; }
		public bool FileExists { get; set; }
		public string ExistingFilePath { get; set; }
	}
}
