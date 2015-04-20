
namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class ICache
	{
		private CacheIncludeTypes _Include = CacheIncludeTypes.All;

		public CacheUsageTypes Usage { get; set; }

		public string Region { get; set; }

		public CacheIncludeTypes Include { get; set; }
	}
}
