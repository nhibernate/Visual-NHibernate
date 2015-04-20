using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using log4net;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	public interface IDatabaseSerialisationScheme
	{
		/// <summary>
		/// Returns an XML string containing the serialised database.
		/// </summary>
		/// <param name="database">The database to serialise.</param>
		/// <returns>The XML representing the database.</returns>
		string Serialise(IDatabase database);

		/// <summary>
		/// Returns an XML snippet containing the serialised Table.
		/// </summary>
		/// <param name="table">The Table to serialise.</param>
		/// <returns>The XML snippet representing the Table.</returns>
		string SerialiseTable(ITable table);

		/// <summary>
		/// Returns an XML snippet containing the serialised Key.
		/// </summary>
		/// <param name="key">The Key to serialise.</param>
		/// <returns>The XML snippet representing the Key.</returns>
		string SerialiseKey(IKey key);

		/// <summary>
		/// Returns an XML snippet containing the serialised Index.
		/// </summary>
		/// <param name="index">The Index to serialise.</param>
		/// <returns>The XML snippet representing the Index.</returns>
		string SerialiseIndex(IIndex index);

		string SerialiseRelationship(Relationship rel);
	}

	public class DatabaseSerialisationScheme : IDatabaseSerialisationScheme
	{
		public readonly static IDatabaseSerialisationScheme LatestVersion = new DatabaseSerialisationScheme();
		private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseDeserialisationScheme));

		/*
		 * Current Performance Results (18th December 2008):
		 * Small DB (20 Tables, 5 Columns each): 2ms
		 * Medium DB (100 Tables, 10 Columns each): 20ms
		 * Large DB (1000 Tables, 20 Columns each): 200ms
		 */

		public string Serialise(IDatabase database)
		{
			log.InfoFormat("Serialising database to XML (DB Name = {0})", database.Name);
			var sb = new StringBuilder();
			XmlWriter writer = GetWriter(sb);

			writer.WriteStartDocument();

			writer.WriteStartElement("LogicalSchema");
			writer.WriteAttributeString("Version", "1");

			if (database.Loader != null)
			{
				SerialiseConnectionInformationInternal(database.Loader.DatabaseConnector, writer);
			}

			writer.WriteElementString("DatabaseName", database.Name);
			writer.WriteElementString("DatabaseType", database.DatabaseType.ToString());

			foreach (var table in database.Tables)
				SerialiseTableInternal(table, writer);

			foreach (var view in database.Views)
				SerialiseViewInternal(view, writer);

			foreach (var rel in database.Relationships)
				SerialiseRelationshipInternal(rel, writer);

			ProcessScriptBase(database, writer);

			writer.WriteEndElement();


			writer.WriteEndDocument();

			writer.Close();

			return sb.ToString();
		}

		private string Serialise(Action<XmlWriter> writeStatement)
		{
			var sb = new StringBuilder();

			XmlWriter writer = GetWriter(sb);

			writeStatement(writer);

			writer.Close();
			return sb.ToString();
		}

		public string SerialiseConnectionInformation(IDatabaseConnector connector)
		{
			return Serialise(writer => SerialiseConnectionInformationInternal(connector, writer));
		}

		public string SerialiseRelationship(Relationship rel)
		{
			return Serialise(writer => SerialiseRelationshipInternal(rel, writer));
		}

		public string SerialiseTable(ITable table)
		{
			return Serialise(writer => SerialiseTableInternal(table, writer));
		}

		public string SerialiseKey(IKey key)
		{
			return Serialise(writer => SerialiseKeyInternal(key, writer));
		}

		public string SerialiseIndex(IIndex index)
		{
			return Serialise(writer => SerialiseIndexInternal(index, writer));
		}

		public string SerialiseColumn(IColumn column)
		{
			return Serialise(writer => SerialiseColumnInternal(column, writer));
		}

		private void SerialiseConnectionInformationInternal(IDatabaseConnector connector, XmlWriter writer)
		{
			if (connector is ISQLServer2005DatabaseConnector)
			{
				new SQLServer2005ConnectorSerialiser().Serialise(connector as ISQLServer2005DatabaseConnector, writer);
			}
			else if (connector is ISQLCEDatabaseConnector)
			{
				new SQLCEConnectorSerialiser().Serialise(connector as ISQLCEDatabaseConnector, writer);
			}
			else if (connector is IMySQLDatabaseConnector)
			{
				new MySQLConnectorSerialiser().Serialise(connector as IMySQLDatabaseConnector, writer);
			}
			else if (connector is IOracleDatabaseConnector)
			{
				new OracleConnectorSerialiser().Serialise(connector as IOracleDatabaseConnector, writer);
			}
			else if (connector is IPostgreSQLDatabaseConnector)
			{
				new PostgreSQLConnectorSerialiser().Serialise(connector as IPostgreSQLDatabaseConnector, writer);
			}
			else if (connector is ISQLServerExpressDatabaseConnector)
			{
				new SQLServerExpressConnectorSerialiser().Serialise(connector as ISQLServerExpressDatabaseConnector, writer);
			}
			else if (connector is IFirebirdDatabaseConnector)
			{
				new FirebirdConnectorSerialiser().Serialise(connector as IFirebirdDatabaseConnector, writer);
			}
			else if (connector is ISQLiteDatabaseConnector)
			{
				new SQLiteConnectorSerialiser().Serialise(connector as ISQLiteDatabaseConnector, writer);
			}
			else
			{
				throw new NotImplementedException("Not coded for yet in SerialiseConnectionInformationInternal(): " + connector.GetType().Name);
			}
		}

		public class SQLCEConnectorSerialiser
		{
			public void Serialise(ISQLCEDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				if (string.IsNullOrEmpty(connector.Filename) == false)
				{
					writer.WriteStartElement("ConnectionInformation");

					writer.WriteAttributeString("DatabaseConnector", "SqlCE");

					writer.WriteElementString("FileName", connector.Filename);

					writer.WriteEndElement();
				}

			}
		}

		public class SQLServer2005ConnectorSerialiser
		{
			public void Serialise(ISQLServer2005DatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "SqlServer2005");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				if (info.UseFileName)
					writer.WriteElementString("FileName", info.FileName);
				else
					writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteEndElement();
			}
		}

		public class SQLServerExpressConnectorSerialiser
		{
			public void Serialise(ISQLServerExpressDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "SqlServerExpress");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				if (info.UseFileName)
					writer.WriteElementString("FileName", info.FileName);
				else
					writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteEndElement();
			}
		}

		public class FirebirdConnectorSerialiser
		{
			public void Serialise(IFirebirdDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "Firebird");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				//if (info.UseFileName)
				writer.WriteElementString("FileName", info.FileName);
				//else
				writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteEndElement();
			}
		}

		public class SQLiteConnectorSerialiser
		{
			public void Serialise(ISQLiteDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "SQLite");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				//if (info.UseFileName)
				writer.WriteElementString("FileName", info.FileName);
				//else
				writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteEndElement();
			}
		}

		public class MySQLConnectorSerialiser
		{
			public void Serialise(IMySQLDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "MySQL");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				if (info.UseFileName)
					writer.WriteElementString("FileName", info.FileName);
				else
					writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteEndElement();
			}
		}

		public class OracleConnectorSerialiser
		{
			public void Serialise(IOracleDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "Oracle");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				if (info.UseFileName)
					writer.WriteElementString("FileName", info.FileName);
				else
					writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteElementString("ServiceName", info.ServiceName);
				writer.WriteElementString("UseDirectConnection", info.UseDirectConnection.ToString());
				writer.WriteEndElement();
			}
		}

		public class PostgreSQLConnectorSerialiser
		{
			public void Serialise(IPostgreSQLDatabaseConnector connector, XmlWriter writer)
			{
				if (connector == null) return;
				if (writer == null) return;

				writer.WriteStartElement("ConnectionInformation");

				writer.WriteAttributeString("DatabaseConnector", "PostgreSQL");

				var info = connector.ConnectionInformation;
				writer.WriteElementString("ServerName", info.ServerName);

				if (info.UseFileName)
					writer.WriteElementString("FileName", info.FileName);
				else
					writer.WriteElementString("DatabaseName", info.DatabaseName);

				if (info.UseIntegratedSecurity)
					writer.WriteElementString("UseIntegratedSecurity", "True");
				else
				{
					writer.WriteElementString("UserName", info.UserName);

					if (info.Password != null)
						writer.WriteElementString("Password", info.Password.Encrypt());
				}
				writer.WriteElementString("Port", info.Port.ToString());
				writer.WriteElementString("ServiceName", info.ServiceName);
				writer.WriteEndElement();
			}
		}

		private void SerialiseRelationshipInternal(Relationship rel, XmlWriter writer)
		{
			if (rel.PrimaryTable == null)
				throw new ArgumentException("Cannot serialise a relationship with no PrimaryTable");
			if (rel.ForeignTable == null)
				throw new ArgumentException("Cannot serialise a relationship with no ForeignTable");

			if (rel.PrimaryKey == null)
				throw new ArgumentException("Cannot serialise a relationship with no PrimaryKey: between tables {0} and {1}.");

			writer.WriteStartElement("Relationship");

			writer.WriteAttributeString("identifier", rel.Identifier.ToString());
			writer.WriteElementString("PrimaryTable", rel.PrimaryTable.Name);
			writer.WriteElementString("PrimarySchema", rel.PrimaryTable.Schema);
			writer.WriteElementString("ForeignTable", rel.ForeignTable.Name);
			writer.WriteElementString("ForeignSchema", rel.ForeignTable.Schema);
			writer.WriteElementString("PrimaryKey", rel.PrimaryKey.Name);
			writer.WriteElementString("ForeignKey", rel.ForeignKey.Name);
			writer.WriteElementString("Name", rel.Name);

			ProcessScriptBase(rel, writer);

			writer.WriteEndElement();
		}

		private void SerialiseIndexInternal(IIndex index, XmlWriter writer)
		{
			writer.WriteStartElement("Index");
			SerialiseScriptBaseInternal(index, writer);
			writer.WriteElementString("Datatype", index.IndexType.ToString());
			writer.WriteElementString("IsUnique", index.IsUnique.ToString());
			writer.WriteElementString("IsClustered", index.IsClustered.ToString());
			WriteColumnReferences(writer, index.Columns);
			writer.WriteEndElement();
		}

		private void SerialiseTableInternal(ITable table, XmlWriter writer)
		{
			writer.WriteStartElement("Table");
			SerialiseScriptBaseInternal(table, writer);

			if (table.Keys.Count > 0)
			{
				writer.WriteStartElement("Keys");
				foreach (var key in table.Keys)
					SerialiseKeyInternal(key, writer);
				writer.WriteEndElement();
			}
			if (table.Indexes.Count > 0)
			{
				writer.WriteStartElement("Indexes");
				foreach (var index in table.Indexes)
					SerialiseIndexInternal(index, writer);
				writer.WriteEndElement();
			}
			if (table.Columns.Count > 0)
			{
				writer.WriteStartElement("Columns");
				foreach (var column in table.Columns)
					SerialiseColumnInternal(column, writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private void SerialiseViewInternal(ITable view, XmlWriter writer)
		{
			writer.WriteStartElement("View");
			SerialiseScriptBaseInternal(view, writer);

			if (view.Keys.Count > 0)
			{
				writer.WriteStartElement("Keys");
				foreach (var key in view.Keys)
					SerialiseKeyInternal(key, writer);
				writer.WriteEndElement();
			}
			if (view.Indexes.Count > 0)
			{
				writer.WriteStartElement("Indexes");
				foreach (var index in view.Indexes)
					SerialiseIndexInternal(index, writer);
				writer.WriteEndElement();
			}
			if (view.Columns.Count > 0)
			{
				writer.WriteStartElement("Columns");
				foreach (var column in view.Columns)
					SerialiseColumnInternal(column, writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private void SerialiseColumnInternal(IColumn column, XmlWriter writer)
		{
			writer.WriteStartElement("Column");
			SerialiseScriptBaseInternal(column, writer);
			writer.WriteElementString("Datatype", string.IsNullOrEmpty(column.OriginalDataType) ? "" : column.OriginalDataType);
			writer.WriteElementString("Default", string.IsNullOrEmpty(column.Default) ? "" : column.Default);
			writer.WriteElementString("InPrimaryKey", column.InPrimaryKey.ToString());
			writer.WriteElementString("IsCalculated", column.IsCalculated.ToString());
			writer.WriteElementString("IsComputed", column.IsComputed.ToString());
			writer.WriteElementString("IsIdentity", column.IsIdentity.ToString());
			writer.WriteElementString("IsNullable", column.IsNullable.ToString());
			writer.WriteElementString("IsReadOnly", column.IsReadOnly.ToString());
			writer.WriteElementString("IsUnique", column.IsUnique.ToString());
			writer.WriteElementString("OrdinalPosition", column.OrdinalPosition.ToString());
			writer.WriteElementString("Precision", column.Precision.ToString());
			writer.WriteElementString("Scale", column.Scale.ToString());
			writer.WriteElementString("Size", column.Size.ToString());
			writer.WriteEndElement();
		}

		private void SerialiseKeyInternal(IKey key, XmlWriter writer)
		{
			writer.WriteStartElement("Key");
			{
				SerialiseScriptBaseInternal(key, writer);
				writer.WriteElementString("Keytype", key.Keytype.ToString());
				writer.WriteElementString("IsUnique", key.IsUnique.ToString());

				if (key.ReferencedKey != null && key.ReferencedKey.Parent != null)
				{
					writer.WriteStartElement("ReferencedKey");
					writer.WriteElementString("KeyName", key.ReferencedKey.Name);

					//if (key.ReferencedKey.Parent == null)
					//    throw new Exception(string.Format("Key [{0}] has no parent table.", key.ReferencedKey.Name));

					writer.WriteElementString("TableName", key.ReferencedKey.Parent.Name);
					writer.WriteElementString("Schema", key.ReferencedKey.Parent.Schema);
					writer.WriteEndElement();
				}
				WriteColumnReferences(writer, key.Columns);
			}
			writer.WriteEndElement();
		}

		private void WriteColumnReferences(XmlWriter writer, ICollection<IColumn> columns)
		{
			if (columns != null && columns.Count > 0)
			{
				writer.WriteStartElement("Columns");

				foreach (var column in columns)
					writer.WriteElementString("ColumnName", column.Name);

				writer.WriteEndElement();
			}
		}

		private void SerialiseScriptBaseInternal(IScriptBase scriptBase, XmlWriter writer)
		{
			writer.WriteElementString("Description", scriptBase.Description);
			writer.WriteElementString("Enabled", scriptBase.Enabled.ToString());
			writer.WriteElementString("IsUserDefined", scriptBase.IsUserDefined.ToString());
			writer.WriteElementString("Name", scriptBase.Name);
			writer.WriteElementString("Schema", scriptBase.Schema);

			ProcessScriptBase(scriptBase, writer);
		}

		public void ProcessScriptBase(IScriptBaseObject scriptBase, XmlWriter writer)
		{
			if (scriptBase.Ex != null && scriptBase.Ex.Count > 0)
			{
				var serialiser = new VirtualPropertySerialiser();
				serialiser.SerialiseVirtualProperties(scriptBase.Ex, writer);
			}
		}

		private static XmlWriter GetWriter(StringBuilder sb)
		{
			// If OmitXmlDecl is not set to true, an <?xml> node will be placed at the start
			// of the snippet, which is not what we want.
			return GetWriter(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
		}

		private static XmlWriter GetWriter(StringBuilder sb, XmlWriterSettings settings)
		{
			settings.Indent = true;
			settings.IndentChars = "\t";
			XmlWriter writer = XmlWriter.Create(sb, settings);
			if (writer == null)
				throw new InvalidOperationException("Couldn't create an XML Writer. ");
			return writer;
		}
	}
}