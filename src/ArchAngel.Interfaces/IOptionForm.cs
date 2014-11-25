using System.Collections.Generic;

namespace ArchAngel.Interfaces
{
	public interface IOptionForm
	{
		string Text { get; set; }
		void Fill(List<ProviderInfo> providers);
		void Save();
		void FinaliseEdits();
	}
}
