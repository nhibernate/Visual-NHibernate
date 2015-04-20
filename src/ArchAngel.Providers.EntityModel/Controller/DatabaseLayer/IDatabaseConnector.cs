using System;
using System.Collections.Generic;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IDatabaseConnector : IDisposable
	{
		string DatabaseName { get; set; }
		void TestConnection();
		void Open();
		void Close();
		ArchAngel.Providers.EntityModel.Helper.ConnectionStringHelper ConnectionInformation { get; set; }
		List<string> GetTableNames();
		List<string> GetViewNames();
		string SchemaFilterCSV { get; set; }
	}
}