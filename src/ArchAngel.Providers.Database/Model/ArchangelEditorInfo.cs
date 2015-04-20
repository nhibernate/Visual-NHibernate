using System;
using System.Collections.Generic;

/// <exclude/>
[Serializable]
public class ArchangelEditorInfo
{
    private static Type[] _parameterTypes;

    public static Type[] ParameterTypes
    {
        get
        {
            if (_parameterTypes == null)
            {
                List<Type> types = new List<Type>();

                // These will be displayed to the user in the order you add them.
                // Make sure that you order them from most used to least used, or alphabetically.
                types.Add(typeof(ArchAngel.Providers.Database.Model.Table));
                types.Add(typeof(ArchAngel.Providers.Database.Model.View));
                types.Add(typeof(ArchAngel.Providers.Database.Model.StoredProcedure));
                types.Add(typeof(ArchAngel.Providers.Database.Model.Database));
                types.Add(typeof(ArchAngel.Providers.Database.Model.Column));
                types.Add(typeof(ArchAngel.Providers.Database.Model.MapColumn));
                types.Add(typeof(ArchAngel.Providers.Database.Model.ScriptObject));
                types.Add(typeof(ArchAngel.Providers.Database.Model.OneToOneRelationship));
                types.Add(typeof(ArchAngel.Providers.Database.Model.OneToManyRelationship));
                types.Add(typeof(ArchAngel.Providers.Database.Model.ManyToOneRelationship));
                types.Add(typeof(ArchAngel.Providers.Database.Model.ManyToManyRelationship));

                _parameterTypes = types.ToArray();
            }
            return _parameterTypes;
        }
    }
}