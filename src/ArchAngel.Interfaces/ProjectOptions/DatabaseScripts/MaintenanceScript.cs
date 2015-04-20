
using System;
namespace ArchAngel.Interfaces.ProjectOptions.DatabaseScripts
{
	public class MaintenanceScript
	{
		private string _Header = "";
		private string _Create = "";
		private string _Update = "";
		private string _Delete = "";

		private string OriginalHeader;
		private string OriginalCreate;
		private string OriginalUpdate;
		private string OriginalDelete;

		public MaintenanceScript(string header, string createScript, string updateScript, string deleteScript)
		{
			Header = header;
			Create = createScript;
			Update = updateScript;
			Delete = deleteScript;

			OriginalHeader = header;
			OriginalCreate = createScript;
			OriginalUpdate = updateScript;
			OriginalDelete = deleteScript;
		}

		public string Header
		{
			get { return _Header; }
			set
			{
				if (_Header != value)
				{
					_Header = value;
					Utility.TimeOfLastScriptChange = DateTime.Now;
				}
			}
		}

		public string Create
		{
			get { return _Create; }
			set
			{
				if (_Create != value)
				{
					_Create = value;
					Utility.TimeOfLastScriptChange = DateTime.Now;
				}
			}
		}

		public string Update
		{
			get { return _Update; }
			set
			{
				if (_Update != value)
				{
					_Update = value;
					Utility.TimeOfLastScriptChange = DateTime.Now;
				}
			}
		}

		public string Delete
		{
			get { return _Delete; }
			set
			{
				if (_Delete != value)
				{
					_Delete = value;
					Utility.TimeOfLastScriptChange = DateTime.Now;
				}
			}
		}

		public bool IsModified
		{
			get
			{
				return
					Header != OriginalHeader ||
					Create != OriginalCreate ||
					Update != OriginalUpdate ||
					Delete != OriginalDelete;
			}
		}
	}
}
