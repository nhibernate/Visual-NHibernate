using ArchAngel.Interfaces;
using ArchAngel.Providers.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Providers.Database.Helper
{
    [Interfaces.Attributes.ArchAngelEditor(false, false, "Alias")]
	public static class Script
	{
		#region Functions
		public static bool StringsAreEqual(string string1, string string2, bool caseSensitive)	
		{
			return Slyce.Common.Utility.StringsAreEqual(string1, string2, caseSensitive);
		}


		[Interfaces.Attributes.ApiExtension]
		public static string GetPlural(string singularName)		
		{
			if (singularName == "")
			{
				return "";
			}
			string name = singularName;
			
			if (name.LastIndexOf("y") == name.Length - 1)
			{
				name =  name.Substring(0, name.Length - 1) + "ies";
			}
			else if (name.LastIndexOf("s") == name.Length - 1)
			{
				name +=  "es";
			}
			else 
			{
				name +=  "s";
			}
			return name;
		}


		[Interfaces.Attributes.ApiExtension]
		public static string GetSingular(string pluralName)		
		{
			string name = pluralName;
			
			if (name.Length > 3 && name.LastIndexOf("ies") == name.Length - 3)
			{
				name =  name.Substring(0, name.Length - 3) + "y";
			}
			if (name.Length > 1 && name.LastIndexOf("s") == name.Length - 1 && name.LastIndexOf("ss") != name.Length - 2)
			{
				name =  name.Substring(0, name.Length - 1);
			}
			return name;
		}


		[Interfaces.Attributes.ApiExtension]
		public static string GetCamelCase(string name)		
		{
			if (string.IsNullOrEmpty(name))
			{
				return "";
			}
			bool toLower = true;
			bool toUpper = false;
			char[] letters = name.ToCharArray();
			
			for (int i = 0; i < letters.Length; i++)
			{
				string letter = letters[i].ToString();
				string nextLetter = null;
				
				if (i < letters.Length - 2)
				{
					nextLetter =  letters[i + 1].ToString();
				}
				if (toLower && nextLetter != null && nextLetter == nextLetter.ToLower() && i > 0)
				{
					toUpper =  true;
					toLower =  false;
				}
				if (toLower)
				{
					letters[i] =  letter.ToLower().ToCharArray()[0];
				}
				if (toUpper)
				{
					if (i > 1)
					{
						letters[i] =  letter.ToUpper().ToCharArray()[0];
					}
					break;
				}
			}
			name =  "";
			
			foreach (char letter in letters)
			{
				name +=  letter.ToString();
			}
			return name;
		}


		[Interfaces.Attributes.ApiExtension]
		public static string GetSingleWord(string multiWords)		
		{
			multiWords =  multiWords.Trim();
			
			if (string.IsNullOrEmpty(multiWords))
			{
				return "";
			}
			/*Replace multi-spaces with single spaces*/
			string[] words = System.Text.RegularExpressions.Regex.Replace(multiWords, @"\s\s", "").Split(' ');
			StringBuilder sb = new StringBuilder(50);
			
			for (int i = 0; i < words.Length; i++)
			{
				/*Capitalise first letter*/
				sb.Append(words[i].Substring(0, 1).ToUpper());
				
				if (words[i].Length > 1)
				{
					sb.Append(words[i].Substring(1));
				}
			}
			return sb.ToString();
		}


		[Interfaces.Attributes.ApiExtension]
		public static string GetTitleCase(string name)		
		{
			string returnString = "";
			string[] ary = name.Split(' ');
			
			foreach (string str in ary)
			{
				string word = str.Trim();
				returnString +=  word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1);
			}
			return returnString;
		}

		[Interfaces.Attributes.ApiExtension]
		public static Column GetColumn(Column[] columns, string name)	
		{
			foreach (Column column in columns)
			{
				if (column.Name == name)
				{
					return column;
				}
			}
			return null;
		}

		[Interfaces.Attributes.ApiExtension]
		public static Column GetColumnByColumnAlias(Column[] columns, string alias)	
		{
			foreach (Column column in columns)
			{
				if (column.Alias == alias)
				{
					return column;
				}
			}
			return null;
		}

		[Interfaces.Attributes.ApiExtension]
		public static MapColumn GetMapColumn(Column column, ScriptObject scriptObject)	
		{
			foreach (MapColumn mapColumn in scriptObject.MapColumns)
			{
				if (mapColumn.PrimaryColumns[0] == column)
				{
					return mapColumn;
				}
			}
			return null;
		}

		[Interfaces.Attributes.ApiExtension]
		public static Relationship GetRelationship(Relationship[] relationships, string name)	
		{
			foreach (Relationship relationship in relationships)
			{
				if (relationship.Name == name)
				{
					return relationship;
				}
			}
			return null;
		}

		[Interfaces.Attributes.ApiExtension]
		public static Relationship GetRelationshipByRelationshipAlias(Relationship[] relationships, string alias)	
		{
			foreach (Relationship relationship in relationships)
			{
				if (relationship.Alias == alias)
				{
					return relationship;
				}
			}
			return null;
		}

		[Interfaces.Attributes.ApiExtension]
		public static OneToOneRelationship[] GetInheritedOneToOneRelationships(ScriptObject scriptObject)	
		{
			List<OneToOneRelationship> oneToOneRelationships = new List<OneToOneRelationship>();
            OneToOneRelationship[] mapOneToOneRelationships = Script.GetDerivedOneToOneRelationships(scriptObject);
			
			if (mapOneToOneRelationships.Length == 0)
			{
				return oneToOneRelationships.ToArray();
			}
			if (mapOneToOneRelationships.Length > 1)
			{
				
				throw new Exception("Template does not support multiple inheritance");
			}
			OneToOneRelationship mapOneToOneRelationship = null;
			
			if (mapOneToOneRelationships.Length == 1)
			{
				mapOneToOneRelationship =  mapOneToOneRelationships[0];
				oneToOneRelationships.Add(mapOneToOneRelationship);
			}
			if (mapOneToOneRelationship.ForeignRelationship.Parent != null)
			{
                oneToOneRelationships.AddRange(Script.GetInheritedOneToOneRelationships(mapOneToOneRelationship.ForeignRelationship.Parent));
			}
			return oneToOneRelationships.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static OneToOneRelationship[] GetBaseOneToOneRelationships(ScriptObject scriptObject)	
		{
			List<OneToOneRelationship> oneToOneRelationships = new List<OneToOneRelationship>();
			
			foreach (OneToOneRelationship oneToOneRelationship in scriptObject.OneToOneRelationships)
			{
				if (oneToOneRelationship.IsBase)
				{
					oneToOneRelationships.Add(oneToOneRelationship);
				}
			}
			return oneToOneRelationships.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static OneToOneRelationship[] GetDerivedOneToOneRelationships(ScriptObject scriptObject)	
		{
			List<OneToOneRelationship> oneToOneRelationships = new List<OneToOneRelationship>();
			
			foreach (OneToOneRelationship oneToOneRelationship in scriptObject.OneToOneRelationships)
			{
				if (oneToOneRelationship.IsDerived)
				{
					oneToOneRelationships.Add(oneToOneRelationship);
				}
			}
			return oneToOneRelationships.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static MapColumn[] GetMapColumns(ScriptObject scriptObject)
		{
			if (ModelTypes.Table.IsInstanceOfType(scriptObject))
			{
				Table table = (Table)scriptObject;
				return table.MapColumns;
			}
			return new MapColumn[0];
		}

		[Interfaces.Attributes.ApiExtension]
    	public static Column[] GetColumnsAndInheritedColumns(ScriptObject scriptObject, OneToOneRelationship[] oneToOneRelationships, bool skipForeignColumns, bool replaceForeignColumns)	
		{
			List<Column> columns = new List<Column>();
			columns.AddRange(scriptObject.Columns);
            columns.AddRange(Script.GetInheritedColumns(oneToOneRelationships, skipForeignColumns, replaceForeignColumns));
			return columns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static Column[] GetUpdateableColumnsAndInheritedUpdateableColumns(ScriptObject scriptObject, OneToOneRelationship[] oneToOneRelationships, bool skipForeignColumns, bool replaceForeignColumns)	
		{
			List<Column> columns = new List<Column>();
			columns.AddRange(scriptObject.UpdateableColumns);
            columns.AddRange(Script.GetInheritedUpdateableColumns(oneToOneRelationships, skipForeignColumns, replaceForeignColumns));
			return columns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static MapColumn[] GetMapColumnsAndInheritedMapColumns(ScriptObject scriptObject, OneToOneRelationship[] oneToOneRelationships)	
		{
			List<MapColumn> mapColumns = new List<MapColumn>();
            mapColumns.AddRange(Script.GetMapColumns(scriptObject));
            mapColumns.AddRange(Script.GetInheritedMapColumns(oneToOneRelationships));
			return mapColumns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static MapColumn[] GetInheritedMapColumns(OneToOneRelationship[] oneToOneRelationships)	
		{
			List<MapColumn> mapColumns = new List<MapColumn>();
			
			foreach (OneToOneRelationship oneToOneRelationship in oneToOneRelationships)
			{
                mapColumns.AddRange(Script.GetMapColumns(oneToOneRelationship.ForeignRelationship.Parent));
			}
			return mapColumns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static Column[] GetInheritedColumns(OneToOneRelationship[] oneToOneRelationships, bool skipForeignColumns, bool replaceForeignColumns)	
		{
			List<Column> columns = new List<Column>();
			
			if (oneToOneRelationships.Length == 0)
			{
				return columns.ToArray();
			}
			if (replaceForeignColumns)
			{
				columns.AddRange(oneToOneRelationships[0].PrimaryColumns);
			}
			
			foreach (OneToOneRelationship oneToOneRelationship in oneToOneRelationships)
			{
				Column[] foreignColumns = oneToOneRelationship.ForeignRelationship.Parent.Columns;
				
				foreach (Column foreignColumn in foreignColumns)
				{
                    Column tempColumn = Script.GetColumn(oneToOneRelationship.ForeignColumns, foreignColumn.Name);
					
					if ((skipForeignColumns || replaceForeignColumns) && tempColumn != null)
					{
						
						continue;
					}
					columns.Add(foreignColumn);
				}
			}
			return columns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static Column[] GetInheritedUpdateableColumns(OneToOneRelationship[] oneToOneRelationships, bool skipForeignColumns, bool replaceForeignColumns)	
		{
			List<Column> columns = new List<Column>();
			
			if (oneToOneRelationships.Length == 0)
			{
				return columns.ToArray();
			}
			if (replaceForeignColumns)
			{
				columns.AddRange(oneToOneRelationships[0].PrimaryColumns);
			}
			
			foreach (OneToOneRelationship oneToOneRelationship in oneToOneRelationships)
			{
				Column[] foreignColumns = oneToOneRelationship.ForeignRelationship.Parent.Columns;
				
				foreach (Column foreignColumn in foreignColumns)
				{
					if (foreignColumn.ReadOnly)
					{
						
						continue;
					}
                    Column tempColumn = Script.GetColumn(oneToOneRelationship.ForeignColumns, foreignColumn.Name);
					
					if ((skipForeignColumns || replaceForeignColumns) && tempColumn != null)
					{
						
						continue;
					}
					columns.Add(foreignColumn);
				}
			}
			return columns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static Column[] GetAllInheritedColumns(OneToOneRelationship[] oneToOneRelationships, bool skipForeignColumns, bool replaceForeignColumns)	
		{
			List<Column> columns = new List<Column>();
			
			if (oneToOneRelationships.Length == 0)
			{
				return columns.ToArray();
			}
			if (replaceForeignColumns)
			{
				columns.AddRange(oneToOneRelationships[0].PrimaryColumns);
			}
			
			foreach (OneToOneRelationship oneToOneRelationship in oneToOneRelationships)
			{
				
				foreach (Column foreignColumn in oneToOneRelationship.ForeignRelationship.Parent.Columns)
				{
                    Column tempColumn = Script.GetColumn(oneToOneRelationship.ForeignColumns, foreignColumn.Name);
					
					if ((skipForeignColumns || replaceForeignColumns) && tempColumn != null)
					{
						
						continue;
					}
					columns.Add(foreignColumn);
				}
			}
			return columns.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static Table[] GetUpdateTables(Table table)	
		{
            return Script.GetUpdateTables(table, false);
		}

		[Interfaces.Attributes.ApiExtension]
		public static Table[] GetUpdateTables(Table table, bool reverseOrder)	
		{
			List<Table> tables = new List<Table>();
			tables.Add(table);
			OneToOneRelationship[] oneToOneRelationships = GetInheritedOneToOneRelationships(table);

            if (oneToOneRelationships.Length > 0 && ModelTypes.Table.IsInstanceOfType(oneToOneRelationships[0].ForeignRelationship.Parent))
			{
				tables.Add((Table)oneToOneRelationships[0].ForeignRelationship.Parent);
			}
			
			foreach (Column column in table.Columns)
			{
                if (ModelTypes.Table.IsInstanceOfType(column.Parent))
				{
					if (!tables.Contains((Table)column.Parent))
					{
						tables.Add((Table)column.Parent);
					}
				}
			}
			
			if (reverseOrder)
			{
				tables.Reverse();
			}
			return tables.ToArray();
		}

		[Interfaces.Attributes.ApiExtension]
		public static int GetAliasCount(Column[] columns, string alias)	
		{
			int count = 0;
			
			foreach (Column column in columns)
			{
				if (column.Alias == alias)
				{
					count++;
				}
			}
			return count;
		}

		[Interfaces.Attributes.ApiExtension]
		public static int GetAliasCount(Relationship[] relationships, string alias)	
		{
			int count = 0;
			
			foreach (Relationship relationship in relationships)
			{
				if (relationship.Alias == alias)
				{
					count++;
				}
			}
			return count;
		}

		[Interfaces.Attributes.ApiExtension]
		public static int GetAliasCount(Filter[] filters, string alias)	
		{
			int count = 0;
			
			foreach (Filter filter in filters)
			{
				if (filter.Alias == alias)
				{
					count++;
				}
			}
			return count;
		}

		[Interfaces.Attributes.ApiExtension]
		public static int GetAliasCount(Index[] indexes, string alias)	
		{
			int count = 0;
			
			foreach (Index index in indexes)
			{
				if (index.Alias == alias)
				{
					count++;
				}
			}
			return count;
		}

		[Interfaces.Attributes.ApiExtension]
		public static int GetAliasCount(Key[] keys, string alias)	
		{
			int count = 0;
			
			foreach (Key key in keys)
			{
				if (key.Alias == alias)
				{
					count++;
				}
			}
			return count;
		}
	

		#endregion

	}

}

