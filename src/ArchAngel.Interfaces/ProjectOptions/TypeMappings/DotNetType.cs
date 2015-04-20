
namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	public class DotNetType
	{
		public string Name;
		public string OriginalName;
		public string CSharpName;
		public string OriginalCSharpName;
		public string VbName;
		public string OriginalVbName;
		public string SqlServerName;
		public string OriginalSqlServerName;
		public string OracleName;
		public string OriginalOracleName;
		public string MySqlName;
		public string OriginalMySqlName;
		public string PostgreSqlName;
		public string OriginalPostgreSqlName;
		public string FirebirdName;
		public string OriginalFirebirdName;
		public string SQLiteName;
		public string OriginalSQLiteName;

		public DotNetType()
		{
		}

		public DotNetType(string name, string csharpName, string vbName, string sqlServerName, string oracleName, string mySqlName, string postgreSqlName, string firebirdName, string sqliteName)
		{
			Name = name;
			OriginalName = Name;
			CSharpName = csharpName;
			OriginalCSharpName = CSharpName;
			VbName = vbName;
			OriginalVbName = VbName;

			SqlServerName = sqlServerName;
			OriginalSqlServerName = sqlServerName;

			OracleName = oracleName;
			OriginalOracleName = OracleName;

			MySqlName = mySqlName;
			OriginalMySqlName = MySqlName;

			PostgreSqlName = postgreSqlName;
			OriginalPostgreSqlName = PostgreSqlName;

			FirebirdName = firebirdName;
			OriginalFirebirdName = FirebirdName;

			SQLiteName = sqliteName;
			OriginalSQLiteName = SQLiteName;
		}

		public bool IsModified
		{
			get
			{
				return
					Name != OriginalName ||
					CSharpName != OriginalCSharpName ||
					VbName != OriginalVbName ||
					SqlServerName != OriginalSqlServerName ||
					OracleName != OriginalOracleName ||
					MySqlName != OriginalMySqlName ||
					PostgreSqlName != OriginalPostgreSqlName ||
					FirebirdName != OriginalFirebirdName ||
					SQLiteName != OriginalSQLiteName;
			}
		}
	}
}
