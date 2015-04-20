using System;
using System.Collections.Generic;

namespace ArchAngel.Providers.Database
{
    public enum DatabaseTypes
    {
        SQLServer2000,
        SQLServer2005,
        SQLServerExpress
    }

    public class ModelTypes
    {
        public static Type Column = typeof(Model.Column);
        public static Type Filter = typeof(Model.Filter);
        public static Type Index = typeof(Model.Index);
        public static Type Key = typeof(Model.Key);
        public static Type KeyList = typeof(List<Model.Key>);
        public static Type ManyToManyRelationship = typeof(Model.ManyToManyRelationship);
        public static Type ManyToOneRelationship = typeof(Model.ManyToOneRelationship);
        public static Type MapColumn = typeof(Model.MapColumn);
        public static Type OneToManyRelationship = typeof(Model.OneToManyRelationship);
        public static Type OneToOneRelationship = typeof(Model.OneToOneRelationship);
        public static Type Relationship = typeof(Model.Relationship);
        public static Type ScriptBase = typeof(Model.ScriptBase);
        public static Type ScriptObject = typeof(Model.ScriptObject);
        public static Type StoredProcedure = typeof(Model.StoredProcedure);
        public static Type Table = typeof(Model.Table);
        public static Type View = typeof(Model.View);
        public static Type UserOptionList = typeof(List<Interfaces.ITemplate.IUserOption>);
        public static Type TableList = typeof(List<Model.Table>);
        public static Type StoredProcedureList = typeof(List<Model.StoredProcedure>);
        public static Type ViewList = typeof(List<Model.View>);
        public static Type ConnectionStringHelper = typeof(BLL.ConnectionStringHelper);
        public static Type DatabaseTypes = typeof(DatabaseTypes);
        public static Type OrderByColumnList = typeof(List<Model.Filter.OrderByColumn>);
        public static Type FilterColumnList = typeof(List<Model.Filter.FilterColumn>);
        public static Type Object = typeof(object);
        public static Type Database = typeof(Model.Database);
        public static Type ColumnList = typeof(List<Model.Column>);
        public static Type FilterList = typeof(List<Model.Filter>);
        public static Type RelationshipArray = typeof(Model.Relationship[]);
        public static Type ManyToManyRelationshipList = typeof(List<Model.ManyToManyRelationship>);
        public static Type ManyToOneRelationshipList = typeof(List<Model.ManyToOneRelationship>);
        public static Type OneToManyRelationshipList = typeof(List<Model.OneToManyRelationship>);
        public static Type OneToOneRelationshipList = typeof(List<Model.OneToOneRelationship>);
        public static Type ParameterList = typeof(List<Model.StoredProcedure.Parameter>);
        public static Type IndexList = typeof(List<Model.Index>);
        public static Type Lookup = typeof(Model.Lookup);
        public static Type LookupList = typeof(List<Model.Lookup>);
        public static Type LookupValueList = typeof(List<Model.LookupValue>);
    }
}
