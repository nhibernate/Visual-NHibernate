using System;

namespace ArchAngel.Providers.EntityModel
{
	public class SettingsEngine
	{
		public static Controller.DatabaseLayer.DatabaseTypes LastDatabaseTypeUsed
		{
			get
			{
				try
				{
					string db = Settings1.Default.LastDatabaseTypeUsed;

					if (db == "SQLServer2000")
						db = "SQLServer2005";

					if (string.IsNullOrEmpty(db))
						return ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.Unknown;

					return (Controller.DatabaseLayer.DatabaseTypes)Enum.Parse(typeof(Controller.DatabaseLayer.DatabaseTypes), db, true);
				}
				catch
				{
					return ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.Unknown;
				}
			}
			set
			{
				Settings1.Default.LastDatabaseTypeUsed = value.ToString();
				Settings1.Default.Save();
			}
		}
	}
}
