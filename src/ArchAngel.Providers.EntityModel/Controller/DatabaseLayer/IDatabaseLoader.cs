using System.Collections.Generic;

using System.ComponentModel;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public interface IDatabaseLoader : INotifyPropertyChanged
	{
		void TestConnection();
		/// <summary>
		/// Attempts to read the Database Schema from the database represented by the DatabaseConnector.
		/// </summary>
		/// <returns>The Database that has been read.</returns>
		/// <exception cref="DatabaseLoaderException">Thrown if there was an issue in the execution of this request.</exception>
		Database LoadDatabase(List<SchemaData> databaseObjectsToFetch, List<string> allowedSchemas);

		IDatabaseConnector DatabaseConnector { get; set; }

		List<SchemaData> GetSchemaObjects();

		List<Table> GetTables(List<SchemaData> tablesToFetch);

		List<Table> GetViews(List<SchemaData> viewsToFetch);

		List<SchemaData> DatabaseObjectsToFetch { get; set; }
	}
}
