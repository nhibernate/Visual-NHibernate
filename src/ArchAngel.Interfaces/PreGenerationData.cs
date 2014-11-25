using System.Collections.Generic;

namespace ArchAngel.Interfaces
{
	public class PreGenerationData
	{
		public string OutputFolder { get; set; }
		public Dictionary<string, object> UserOptions { get; private set; }
		public bool OverwriteFiles = false;

		/// <summary>
		/// Gets/sets a list of ProviderInfo objects of all the other Providers in the project.
		/// </summary>
		public List<ProviderInfo> OtherProviderInfos { get; set; }

		public PreGenerationData()
		{
			UserOptions = new Dictionary<string, object>();
		}
	}
}