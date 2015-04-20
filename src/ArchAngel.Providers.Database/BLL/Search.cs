using System;
using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.BLL
{
    public static class Search
    {
    	//public static ScriptObject GetScriptObject(ScriptObject[] scriptObjects, string name, bool throwException)
		public static ScriptObject GetScriptObject(ScriptObject[] scriptObjects, ScriptObject scriptObjectToFind, Type type, bool throwException)
        {
            foreach (ScriptObject scriptObject in scriptObjects)
            {
                if (scriptObject.Name == scriptObjectToFind.Name &&
					scriptObject.Schema == scriptObjectToFind.Schema && 
					scriptObject.GetType() == type)
                {
                    return scriptObject;
                }
            }

            if (throwException)
            {
				throw new Exception(string.Format("Cannot find script object {0}.{1}", scriptObjectToFind.Schema, scriptObjectToFind.Name));
            }
        	return null;
        }

        //public static ScriptObject GetScriptObject(ScriptObject[] scriptObjects, string name)
		public static ScriptObject GetScriptObject(ScriptObject[] scriptObjects, ScriptObject scriptObjectToFind, Type type)
        {
			return GetScriptObject(scriptObjects, scriptObjectToFind, type, true);
        }

		public static Model.Table GetTable(IList<Model.Table> tables, Model.Table tableToFind, bool throwException)
        {
            foreach (Model.Table table in tables)
            {
                if (table.Name == tableToFind.Name &&
					table.Schema == tableToFind.Schema)
                {
                    return table;
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find table {0}.{1}", tableToFind.Schema, tableToFind.Name));
            }
        	return null;
        }

		public static Model.Table GetTable(IList<Model.Table> tables, Model.Table tableToFind)
        {
			return GetTable(tables, tableToFind, true);
        }

		public static Model.View GetView(IList<Model.View> views, Model.View viewToFind, bool throwException)
        {
            foreach (Model.View view in views)
            {
                if (view.Name == viewToFind.Name &&
					view.Schema == viewToFind.Schema)
                {
                    return view;
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find view {0}.{1}", viewToFind.Schema, viewToFind.Name));
            }
        	return null;
        }

		public static Model.View GetView(IList<Model.View> views, Model.View viewToFind)
        {
			return GetView(views, viewToFind, true);
        }

		public static Model.StoredProcedure GetStoredProcedure(IList<Model.StoredProcedure> storedProcedures, Model.StoredProcedure storedProcedureToFind, bool throwException)
        {
            foreach (Model.StoredProcedure storedProcedure in storedProcedures)
            {
                if (storedProcedure.Name == storedProcedureToFind.Name &&
					storedProcedure.Schema == storedProcedureToFind.Schema)
                {
                    return storedProcedure;
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find storedProcedure {0}.{1}", storedProcedureToFind.Schema, storedProcedureToFind.Name));
            }
        	return null;
        }

		public static Model.StoredProcedure GetStoredProcedure(IList<Model.StoredProcedure> storedProcedures, Model.StoredProcedure storedProcedureToFind)
        {
            return GetStoredProcedure(storedProcedures, storedProcedureToFind, true);
        }

		public static Column GetColumn(IList<Model.Table> tables, Model.Table tableToFind, string columnName, bool throwException)
        {
            foreach (Model.Table table in tables)
            {
                if (table.Name == tableToFind.Name &&
					table.Schema == tableToFind.Schema)
                {
                    foreach (Column column in table.Columns)
                    {
                        if (column.Name == columnName)
                        {
                            return column;
                        }
                    }
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find column {0} of table {1}.{2}", columnName, tableToFind.Schema, tableToFind.Name));
            }
        	return null;
        }

		public static Column GetColumn(IList<Model.Table> tables, Model.Table tableToFind, string columnName)
        {
			return GetColumn(tables, tableToFind, columnName, true);
        }

        public static Column GetColumn(Column[] columns, string name, bool throwException)
        {
            foreach (Column column in columns)
            {
                if (column.Name == name)
                {
                    return column;
                }
            }

            if (throwException)
            {
                throw new Exception("Cannot find column " + name);
            }
        	return null;
        }

        /// <summary>
        /// Lookup for MapColumns - need to compare Alias as well, because multiple MapColumns can link to a single foreign table.
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public static Column GetColumn(Column[] columns, string name, string alias, bool throwException)
        {
            foreach (Column column in columns)
            {
                if (column.Name == name &&
                    column.Alias == alias)
                {
                    return column;
                }
            }

            if (throwException)
            {
                throw new Exception("Cannot find column " + name);
            }
            return null;
        }


        public static Column GetColumn(Column[] columns, string name)
        {
            return GetColumn(columns, name, true);
        }

        /// <summary>
        /// Lookup for MapColumns - need to compare Alias as well, because multiple MapColumns can link to a single foreign table.
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static Column GetColumn(Column[] columns, string name, string alias)
        {
            return GetColumn(columns, name, alias, true);
        }

		public static Filter GetFilter(IList<Model.Table> tables, Model.Table tableToFind, string filterName, bool throwException)
        {
            foreach (Model.Table table in tables)
            {
                if (table.Name == tableToFind.Name &&
					table.Schema == tableToFind.Schema)
                {
                    foreach (Filter filter in table.Filters)
                    {
                        if (filter.Name == filterName)
                        {
                            return filter;
                        }
                    }
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find filter {0} of table {1}.{2}", filterName, tableToFind.Schema, tableToFind.Name));
            }
        	return null;
        }

		public static Filter GetFilter(IList<Model.Table> tables, Model.Table tableToFind, string filterName)
        {
			return GetFilter(tables, tableToFind, filterName, true);
        }

        public static Filter GetFilter(Filter[] filters, string name, bool throwException)
        {
            foreach (Filter filter in filters)
            {
                if (filter.Name == name)
                {
                    return filter;
                }
            }

            if (throwException)
            {
                throw new Exception("Cannot find filter " + name);
            }
        	return null;
        }

        public static Filter GetFilter(Filter[] filters, string name)
        {
            return GetFilter(filters, name, true);
        }

        public static Filter GetFilter(Filter[] filters, Filter filterToFind)
        {
            foreach (Filter filter in filters)
            {
            	if (filter.FilterColumns.Length == filterToFind.FilterColumns.Length &&
                    filter.OrderByColumns.Length == filterToFind.OrderByColumns.Length)
                {
                    bool found = true;

                    foreach (Filter.FilterColumn column in filter.FilterColumns)
                    {
                        found = false;

                        foreach (Filter.FilterColumn columnToFind in filterToFind.FilterColumns)
                        {
                            if (column.Column.Name == columnToFind.Column.Name)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            break;
                        }
                    }
                    foreach (Filter.OrderByColumn column in filter.OrderByColumns)
                    {
                        found = false;

                        foreach (Filter.OrderByColumn columnToFind in filterToFind.OrderByColumns)
                        {
                            if (column.Column.Name == columnToFind.Column.Name)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            break;
                        }
                    }
                    if (found)
                    {
                        return filter;
                    }
                }
            }
            return null;
        }

        public static Model.StoredProcedure.Parameter GetParameter(IList<Model.StoredProcedure> storedProcedures, Model.StoredProcedure storedProcedureToFind, string parameterName, bool throwException)
        {
            foreach (Model.StoredProcedure storedProcedure in storedProcedures)
            {
                if (storedProcedure.Name == storedProcedureToFind.Name &&
					storedProcedure.Schema == storedProcedureToFind.Schema)
                {
                    foreach (Model.StoredProcedure.Parameter parameter in storedProcedure.Parameters)
                    {
                        if (parameter.Name == parameterName)
                        {
                            return parameter;
                        }
                    }
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find column {0} of stored procedure {1}.{2}", parameterName, storedProcedureToFind.Schema, storedProcedureToFind.Name));
            }
        	return null;
        }

		public static Model.StoredProcedure.Parameter GetParameter(IList<Model.StoredProcedure> storedProcedures, Model.StoredProcedure storedProcedureToFind, string parameterName)
        {
			return GetParameter(storedProcedures, storedProcedureToFind, parameterName, true);
        }

        public static Model.StoredProcedure.Parameter GetParameter(Model.StoredProcedure.Parameter[] parameters, string name, bool throwException)
        {
            name = name.Replace("@", "");

            foreach (Model.StoredProcedure.Parameter parameter in parameters)
            {
                if (parameter.Name == name)
                {
                    return parameter;
                }
            }
            if (throwException)
            {
                throw new Exception("Cannot find parameter " + name);
            }
        	return null;
        }

        public static Model.StoredProcedure.Parameter GetParameter(Model.StoredProcedure.Parameter[] parameters, string name)
        {
            return GetParameter(parameters, name, true);
        }

		public static Relationship GetRelationship(IList<Model.Table> tables, Model.Table tableToFind, string relationshipName, bool throwException)
        {
            foreach (Model.Table table in tables)
            {
                if (table.Name == tableToFind.Name &&
					table.Schema == tableToFind.Schema)
                {
                    foreach (Relationship relationship in table.Relationships)
                    {
                        if (relationship.Name == relationshipName)
                        {
                            return relationship;
                        }
                    }
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find relationship {0} of table {1}.{2}", relationshipName, tableToFind.Schema, tableToFind.Name));
            }
        	return null;
        }

		public static Relationship GetRelationship(IList<Model.Table> tables, Model.Table tableToFind, string relationshipName)
        {
			return GetRelationship(tables, tableToFind, relationshipName, true);
        }

        public static Relationship GetRelationship(Relationship[] relationships, string name, bool throwException)
        {
            foreach (Relationship relationship in relationships)
            {
                if (relationship.Name == name)
                {
                    return relationship;
                }
            }

            if (throwException)
            {
                throw new Exception("Cannot find relationship " + name);
            }
        	return null;
        }

        public static Relationship GetRelationship(Relationship[] relationships, string name)
        {
            return GetRelationship(relationships, name, true);
        }

		public static Key GetKey(IList<Model.Table> tables, Model.Table tableToFind, string keyName, bool throwException)
        {
            foreach (Model.Table table in tables)
            {
                if (table.Name == tableToFind.Name &&
					table.Schema == tableToFind.Schema)
                {
                    foreach (Key key in table.Keys)
                    {
                        if (key.Name == keyName)
                        {
                            return key;
                        }
                    }
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find key {0} of table {1}.{2}", keyName, tableToFind.Schema, tableToFind.Name));
                //return null;
            }
        	return null;
        }

		public static Key GetKey(IList<Model.Table> tables, Model.Table tableToFind, string keyName)
        {
			return GetKey(tables, tableToFind, keyName, true);
        }

        public static Key GetKey(IList<Key> keys, string keyName, bool throwException)
        {
            foreach (Key key in keys)
            {
                if (key.Name == keyName)
                {
                    return key;
                }
            }

            if (throwException)
            {
                throw new Exception("Cannot find key " + keyName);
            }
        	return null;
        }

        public static Key GetKey(IList<Key> keys, string keyName)
        {
            return GetKey(keys, keyName, true);
        }

		public static Index GetIndex(IList<Model.Table> tables, Model.Table tableToFind, string indexName, bool throwException)
        {
            foreach (Model.Table table in tables)
            {
                if (table.Name == tableToFind.Name &&
					table.Schema == tableToFind.Schema)
                {
                    foreach (Index index in table.Indexes)
                    {
                        if (index.Name == indexName)
                        {
                            return index;
                        }
                    }
                }
            }

            if (throwException)
            {
                throw new Exception(string.Format("Cannot find index {0} of table {1}.{2}", indexName, tableToFind.Schema, tableToFind.Name));
            }
        	return null;
        }

		public static Index GetIndex(IList<Model.Table> tables, Model.Table tableToFind, string indexName)
        {
			return GetIndex(tables, tableToFind, indexName, true);
        }

        public static Index GetIndex(IList<Index> indexes, string indexName, bool throwException)
        {
            foreach (Index index in indexes)
            {
                if (index.Name == indexName)
                {
                    return index;
                }
            }

            if (throwException)
            {
                throw new Exception("Cannot find index " + indexName);
            }
        	return null;
        }

        public static Index GetIndex(IList<Index> indexes, string indexName)
        {
            return GetIndex(indexes, indexName, true);
        }

		//private static string RemoveOwnerName(string fullTableName)
		//{
		//    if (fullTableName == null)
		//        return null;

		//    int start = fullTableName.IndexOf("].[");

		//    if (start == -1)
		//    {
		//        return fullTableName;
		//    }
		//    fullTableName = fullTableName.Substring(start + 3, fullTableName.Length - start - 3);
		//    fullTableName = fullTableName.Replace("]", "");
		//    return fullTableName;
		//}
    }
}