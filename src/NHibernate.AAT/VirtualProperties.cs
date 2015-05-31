// Decompiled with JetBrains decompiler
// Type: NHibernate.VirtualProperties
// Assembly: NHibernate.AAT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 40840706-A565-4F76-8EF9-260A13165A0A
// Assembly location: C:\Projekte\OpenSource\VNH_Luedi\src\3rd_Party_Libs\NHibernate.AAT.DLL

using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.NHibernateEnums;
using ArchAngel.NHibernateHelper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NHibernate
{
  public static class VirtualProperties
  {
    private static Dictionary<object, Dictionary<string, object>> VirtualPropertyValues = new Dictionary<object, Dictionary<string, object>>();

    public static void ClearCache()
    {
      VirtualProperties.VirtualPropertyValues.Clear();
    }

    public static void ComponentProperty_UsePrivateSetter_EnsureInit(ComponentProperty obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("ComponentProperty_UsePrivateSetter"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "ComponentProperty_UsePrivateSetter")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("ComponentProperty_UsePrivateSetter", (object) (bool) (VirtualProperties.ComponentProperty_UsePrivateSetter_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("ComponentProperty_UsePrivateSetter", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "ComponentProperty_UsePrivateSetter")).Value ? true : false));
    }

    public static bool get_ComponentProperty_UsePrivateSetter(this ComponentProperty obj)
    {
      VirtualProperties.ComponentProperty_UsePrivateSetter_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["ComponentProperty_UsePrivateSetter"];
    }

    public static void set_ComponentProperty_UsePrivateSetter(this ComponentProperty obj, bool value)
    {
      VirtualProperties.ComponentProperty_UsePrivateSetter_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["ComponentProperty_UsePrivateSetter"] = (object) (bool) (value ? true : false);
    }

    public static bool ComponentProperty_UsePrivateSetter_DefaultValue(this ComponentProperty componentproperty)
    {
      return false;
    }

    public static bool ComponentProperty_UsePrivateSetter_DisplayToUser(this ComponentProperty componentproperty)
    {
      return true;
    }

    public static bool ComponentProperty_UsePrivateSetter_IsValid(this ComponentProperty componentproperty, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void ComponentSpec_MarkAsSerializable_EnsureInit(ComponentSpecification obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("ComponentSpec_MarkAsSerializable"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "ComponentSpec_MarkAsSerializable")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("ComponentSpec_MarkAsSerializable", (object) (bool) (VirtualProperties.ComponentSpec_MarkAsSerializable_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("ComponentSpec_MarkAsSerializable", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "ComponentSpec_MarkAsSerializable")).Value ? true : false));
    }

    public static bool get_ComponentSpec_MarkAsSerializable(this ComponentSpecification obj)
    {
      VirtualProperties.ComponentSpec_MarkAsSerializable_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["ComponentSpec_MarkAsSerializable"];
    }

    public static void set_ComponentSpec_MarkAsSerializable(this ComponentSpecification obj, bool value)
    {
      VirtualProperties.ComponentSpec_MarkAsSerializable_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["ComponentSpec_MarkAsSerializable"] = (object) (bool) (value ? true : false);
    }

    public static bool ComponentSpec_MarkAsSerializable_DefaultValue(this ComponentSpecification componentspecification)
    {
      return true;
    }

    public static bool ComponentSpec_MarkAsSerializable_DisplayToUser(this ComponentSpecification componentspecification)
    {
      return true;
    }

    public static bool ComponentSpec_MarkAsSerializable_IsValid(this ComponentSpecification componentspecification, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Component_UsePrivateSetter_EnsureInit(Component obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Component_UsePrivateSetter"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Component_UsePrivateSetter")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Component_UsePrivateSetter", (object) (bool) (VirtualProperties.Component_UsePrivateSetter_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Component_UsePrivateSetter", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Component_UsePrivateSetter")).Value ? true : false));
    }

    public static bool get_Component_UsePrivateSetter(this Component obj)
    {
      VirtualProperties.Component_UsePrivateSetter_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Component_UsePrivateSetter"];
    }

    public static void set_Component_UsePrivateSetter(this Component obj, bool value)
    {
      VirtualProperties.Component_UsePrivateSetter_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Component_UsePrivateSetter"] = (object) (bool) (value ? true : false);
    }

    public static bool Component_UsePrivateSetter_DefaultValue(this Component component)
    {
      return false;
    }

    public static bool Component_UsePrivateSetter_DisplayToUser(this Component component)
    {
      return true;
    }

    public static bool Component_UsePrivateSetter_IsValid(this Component component, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End1CollectionType_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End1CollectionType"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1CollectionType")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1CollectionType", (object) VirtualProperties.End1CollectionType_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1CollectionType", (object) (AssociationType) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1CollectionType")).Value);
    }

    public static AssociationType get_End1CollectionType(this Reference obj)
    {
      VirtualProperties.End1CollectionType_EnsureInit(obj);
      return (AssociationType) VirtualProperties.VirtualPropertyValues[(object) obj]["End1CollectionType"];
    }

    public static void set_End1CollectionType(this Reference obj, AssociationType value)
    {
      VirtualProperties.End1CollectionType_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End1CollectionType"] = (object) value;
    }

    public static AssociationType End1CollectionType_DefaultValue(this Reference reference)
    {
      return (AssociationType) 3;
    }

    public static bool End1CollectionType_DisplayToUser(this Reference reference)
    {
      return reference.Cardinality1 == Cardinality.Many;
    }

    public static bool End1CollectionType_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      if ((AssociationType) ((IScriptBaseObject) reference).GetUserOptionValue("End1CollectionType") != AssociationType.Set || reference.MappedTable() != null)
        return true;
      failReason = "'Set' collection types require a mapped table.";
      return false;
    }

    public static void End1IndexColumn_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End1IndexColumn"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1IndexColumn")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1IndexColumn", (object) VirtualProperties.End1IndexColumn_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1IndexColumn", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1IndexColumn")).Value);
    }

    public static string get_End1IndexColumn(this Reference obj)
    {
      VirtualProperties.End1IndexColumn_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["End1IndexColumn"];
    }

    public static void set_End1IndexColumn(this Reference obj, string value)
    {
      VirtualProperties.End1IndexColumn_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End1IndexColumn"] = (object) value;
    }

    public static string End1IndexColumn_DefaultValue(this Reference reference)
    {
      return (string) null;
    }

    public static bool End1IndexColumn_DisplayToUser(this Reference reference)
    {
      AssociationType associationType = (AssociationType) ((IScriptBaseObject) reference).GetUserOptionValue("End1CollectionType");
      return (reference.Cardinality1 == Cardinality.Many) && (associationType == AssociationType.Map || associationType == AssociationType.IDBag || associationType == AssociationType.List);
    }

    public static bool End1IndexColumn_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End1Inverse_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End1Inverse"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1Inverse")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1Inverse", (object) VirtualProperties.End1Inverse_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1Inverse", (object) (BooleanInheritedTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1Inverse")).Value);
    }

    public static BooleanInheritedTypes get_End1Inverse(this Reference obj)
    {
      VirtualProperties.End1Inverse_EnsureInit(obj);
      return (BooleanInheritedTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["End1Inverse"];
    }

    public static void set_End1Inverse(this Reference obj, BooleanInheritedTypes value)
    {
      VirtualProperties.End1Inverse_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End1Inverse"] = (object) value;
    }

    public static BooleanInheritedTypes End1Inverse_DefaultValue(this Reference reference)
    {
      if ((reference.Cardinality2 == Cardinality.One) && (reference.Cardinality1 == Cardinality.Many))
        return (BooleanInheritedTypes) 0;
      return (BooleanInheritedTypes) 2;
    }

    public static bool End1Inverse_DisplayToUser(this Reference reference)
    {
      return reference.Cardinality1 == Cardinality.Many;
    }

    public static bool End1Inverse_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End1SqlWhereClause_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End1SqlWhereClause"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1SqlWhereClause")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1SqlWhereClause", (object) VirtualProperties.End1SqlWhereClause_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1SqlWhereClause", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1SqlWhereClause")).Value);
    }

    public static string get_End1SqlWhereClause(this Reference obj)
    {
      VirtualProperties.End1SqlWhereClause_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["End1SqlWhereClause"];
    }

    public static void set_End1SqlWhereClause(this Reference obj, string value)
    {
      VirtualProperties.End1SqlWhereClause_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End1SqlWhereClause"] = (object) value;
    }

    public static string End1SqlWhereClause_DefaultValue(this Reference reference)
    {
      return (string) null;
    }

    public static bool End1SqlWhereClause_DisplayToUser(this Reference reference)
    {
      return reference.Cardinality1 == Cardinality.Many;
    }

    public static bool End1SqlWhereClause_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End1UsePrivateSetter_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End1UsePrivateSetter"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1UsePrivateSetter")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1UsePrivateSetter", (object) (bool) (VirtualProperties.End1UsePrivateSetter_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End1UsePrivateSetter", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End1UsePrivateSetter")).Value ? true : false));
    }

    public static bool get_End1UsePrivateSetter(this Reference obj)
    {
      VirtualProperties.End1UsePrivateSetter_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["End1UsePrivateSetter"];
    }

    public static void set_End1UsePrivateSetter(this Reference obj, bool value)
    {
      VirtualProperties.End1UsePrivateSetter_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End1UsePrivateSetter"] = (object) (bool) (value ? true : false);
    }

    public static bool End1UsePrivateSetter_DefaultValue(this Reference reference)
    {
      return false;
    }

    public static bool End1UsePrivateSetter_DisplayToUser(this Reference reference)
    {
      return true;
    }

    public static bool End1UsePrivateSetter_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End2CollectionType_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End2CollectionType"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2CollectionType")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2CollectionType", (object) VirtualProperties.End2CollectionType_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2CollectionType", (object) (AssociationType) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2CollectionType")).Value);
    }

    public static AssociationType get_End2CollectionType(this Reference obj)
    {
      VirtualProperties.End2CollectionType_EnsureInit(obj);
      return (AssociationType) VirtualProperties.VirtualPropertyValues[(object) obj]["End2CollectionType"];
    }

    public static void set_End2CollectionType(this Reference obj, AssociationType value)
    {
      VirtualProperties.End2CollectionType_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End2CollectionType"] = (object) value;
    }

    public static AssociationType End2CollectionType_DefaultValue(this Reference reference)
    {
      return (AssociationType) 3;
    }

    public static bool End2CollectionType_DisplayToUser(this Reference reference)
    {
      return reference.Cardinality2 == Cardinality.Many;
    }

    public static bool End2CollectionType_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End2IndexColumn_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End2IndexColumn"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2IndexColumn")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2IndexColumn", (object) VirtualProperties.End2IndexColumn_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2IndexColumn", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2IndexColumn")).Value);
    }

    public static string get_End2IndexColumn(this Reference obj)
    {
      VirtualProperties.End2IndexColumn_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["End2IndexColumn"];
    }

    public static void set_End2IndexColumn(this Reference obj, string value)
    {
      VirtualProperties.End2IndexColumn_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End2IndexColumn"] = (object) value;
    }

    public static string End2IndexColumn_DefaultValue(this Reference reference)
    {
      return (string) null;
    }

    public static bool End2IndexColumn_DisplayToUser(this Reference reference)
    {
      AssociationType associationType = (AssociationType) ((IScriptBaseObject) reference).GetUserOptionValue("End2CollectionType");
      return (reference.Cardinality2 == Cardinality.Many) && (associationType == AssociationType.Set || associationType == AssociationType.IDBag || associationType == AssociationType.List);
    }

    public static bool End2IndexColumn_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End2Inverse_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End2Inverse"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2Inverse")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2Inverse", (object) VirtualProperties.End2Inverse_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2Inverse", (object) (BooleanInheritedTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2Inverse")).Value);
    }

    public static BooleanInheritedTypes get_End2Inverse(this Reference obj)
    {
      VirtualProperties.End2Inverse_EnsureInit(obj);
      return (BooleanInheritedTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["End2Inverse"];
    }

    public static void set_End2Inverse(this Reference obj, BooleanInheritedTypes value)
    {
      VirtualProperties.End2Inverse_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End2Inverse"] = (object) value;
    }

    public static BooleanInheritedTypes End2Inverse_DefaultValue(this Reference reference)
    {
      if ((reference.Cardinality2 == Cardinality.Many) && (reference.Cardinality1 == Cardinality.One))
        return (BooleanInheritedTypes) 0;
      return (BooleanInheritedTypes) 2;
    }

    public static bool End2Inverse_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality2 == Cardinality.Many);
    }

    public static bool End2Inverse_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End2SqlWhereClause_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End2SqlWhereClause"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2SqlWhereClause")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2SqlWhereClause", (object) VirtualProperties.End2SqlWhereClause_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2SqlWhereClause", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2SqlWhereClause")).Value);
    }

    public static string get_End2SqlWhereClause(this Reference obj)
    {
      VirtualProperties.End2SqlWhereClause_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["End2SqlWhereClause"];
    }

    public static void set_End2SqlWhereClause(this Reference obj, string value)
    {
      VirtualProperties.End2SqlWhereClause_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End2SqlWhereClause"] = (object) value;
    }

    public static string End2SqlWhereClause_DefaultValue(this Reference reference)
    {
      return (string) null;
    }

    public static bool End2SqlWhereClause_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality2 ==  Cardinality.Many);
    }

    public static bool End2SqlWhereClause_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void End2UsePrivateSetter_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("End2UsePrivateSetter"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2UsePrivateSetter")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2UsePrivateSetter", (object) (bool) (VirtualProperties.End2UsePrivateSetter_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("End2UsePrivateSetter", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "End2UsePrivateSetter")).Value ? true : false));
    }

    public static bool get_End2UsePrivateSetter(this Reference obj)
    {
      VirtualProperties.End2UsePrivateSetter_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["End2UsePrivateSetter"];
    }

    public static void set_End2UsePrivateSetter(this Reference obj, bool value)
    {
      VirtualProperties.End2UsePrivateSetter_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["End2UsePrivateSetter"] = (object) (bool) (value ? true : false);
    }

    public static bool End2UsePrivateSetter_DefaultValue(this Reference reference)
    {
      return false;
    }

    public static bool End2UsePrivateSetter_DisplayToUser(this Reference reference)
    {
      return true;
    }

    public static bool End2UsePrivateSetter_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_BatchSize_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_BatchSize"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_BatchSize")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_BatchSize", (object) VirtualProperties.Entity_BatchSize_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_BatchSize", (object) (int) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_BatchSize")).Value);
    }

    public static int get_Entity_BatchSize(this Entity obj)
    {
      VirtualProperties.Entity_BatchSize_EnsureInit(obj);
      return (int) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_BatchSize"];
    }

    public static void set_Entity_BatchSize(this Entity obj, int value)
    {
      VirtualProperties.Entity_BatchSize_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_BatchSize"] = (object) value;
    }

    public static int Entity_BatchSize_DefaultValue(this Entity entity)
    {
      return 1;
    }

    public static bool Entity_BatchSize_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_BatchSize_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_DefaultAccess_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_DefaultAccess"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DefaultAccess")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DefaultAccess", (object) VirtualProperties.Entity_DefaultAccess_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DefaultAccess", (object) (PropertyAccessTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DefaultAccess")).Value);
    }

    public static PropertyAccessTypes get_Entity_DefaultAccess(this Entity obj)
    {
      VirtualProperties.Entity_DefaultAccess_EnsureInit(obj);
      return (PropertyAccessTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DefaultAccess"];
    }

    public static void set_Entity_DefaultAccess(this Entity obj, PropertyAccessTypes value)
    {
      VirtualProperties.Entity_DefaultAccess_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DefaultAccess"] = (object) value;
    }

    public static PropertyAccessTypes Entity_DefaultAccess_DefaultValue(this Entity entity)
    {
      return (PropertyAccessTypes) 0;
    }

    public static bool Entity_DefaultAccess_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_DefaultAccess_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_DefaultCascade_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_DefaultCascade"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DefaultCascade")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DefaultCascade", (object) VirtualProperties.Entity_DefaultCascade_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DefaultCascade", (object) (CascadeTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DefaultCascade")).Value);
    }

    public static CascadeTypes get_Entity_DefaultCascade(this Entity obj)
    {
      VirtualProperties.Entity_DefaultCascade_EnsureInit(obj);
      return (CascadeTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DefaultCascade"];
    }

    public static void set_Entity_DefaultCascade(this Entity obj, CascadeTypes value)
    {
      VirtualProperties.Entity_DefaultCascade_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DefaultCascade"] = (object) value;
    }

    public static CascadeTypes Entity_DefaultCascade_DefaultValue(this Entity entity)
    {
      return (CascadeTypes) 0;
    }

    public static bool Entity_DefaultCascade_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_DefaultCascade_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_DefaultCollectionLazy_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_DefaultCollectionLazy"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DefaultCollectionLazy")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DefaultCollectionLazy", (object) VirtualProperties.Entity_DefaultCollectionLazy_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DefaultCollectionLazy", (object) (CollectionLazyTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DefaultCollectionLazy")).Value);
    }

    public static CollectionLazyTypes get_Entity_DefaultCollectionLazy(this Entity obj)
    {
      VirtualProperties.Entity_DefaultCollectionLazy_EnsureInit(obj);
      return (CollectionLazyTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DefaultCollectionLazy"];
    }

    public static void set_Entity_DefaultCollectionLazy(this Entity obj, CollectionLazyTypes value)
    {
      VirtualProperties.Entity_DefaultCollectionLazy_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DefaultCollectionLazy"] = (object) value;
    }

    public static CollectionLazyTypes Entity_DefaultCollectionLazy_DefaultValue(this Entity entity)
    {
      return (CollectionLazyTypes) 0;
    }

    public static bool Entity_DefaultCollectionLazy_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_DefaultCollectionLazy_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_DynamicInsert_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_DynamicInsert"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DynamicInsert")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DynamicInsert", (object) (bool) (VirtualProperties.Entity_DynamicInsert_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DynamicInsert", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DynamicInsert")).Value ? true : false));
    }

    public static bool get_Entity_DynamicInsert(this Entity obj)
    {
      VirtualProperties.Entity_DynamicInsert_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DynamicInsert"];
    }

    public static void set_Entity_DynamicInsert(this Entity obj, bool value)
    {
      VirtualProperties.Entity_DynamicInsert_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DynamicInsert"] = (object) (bool) (value ? true : false);
    }

    public static bool Entity_DynamicInsert_DefaultValue(this Entity entity)
    {
      return false;
    }

    public static bool Entity_DynamicInsert_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_DynamicInsert_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_DynamicUpdate_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_DynamicUpdate"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DynamicUpdate")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DynamicUpdate", (object) (bool) (VirtualProperties.Entity_DynamicUpdate_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_DynamicUpdate", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_DynamicUpdate")).Value ? true : false));
    }

    public static bool get_Entity_DynamicUpdate(this Entity obj)
    {
      VirtualProperties.Entity_DynamicUpdate_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DynamicUpdate"];
    }

    public static void set_Entity_DynamicUpdate(this Entity obj, bool value)
    {
      VirtualProperties.Entity_DynamicUpdate_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_DynamicUpdate"] = (object) (bool) (value ? true : false);
    }

    public static bool Entity_DynamicUpdate_DefaultValue(this Entity entity)
    {
      return false;
    }

    public static bool Entity_DynamicUpdate_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_DynamicUpdate_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_Lazy_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_Lazy"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Lazy")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Lazy", (object) VirtualProperties.Entity_Lazy_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Lazy", (object) (EntityLazyTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Lazy")).Value);
    }

    public static EntityLazyTypes get_Entity_Lazy(this Entity obj)
    {
      VirtualProperties.Entity_Lazy_EnsureInit(obj);
      return (EntityLazyTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Lazy"];
    }

    public static void set_Entity_Lazy(this Entity obj, EntityLazyTypes value)
    {
      VirtualProperties.Entity_Lazy_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Lazy"] = (object) value;
    }

    public static EntityLazyTypes Entity_Lazy_DefaultValue(this Entity entity)
    {
      return (EntityLazyTypes) 0;
    }

    public static bool Entity_Lazy_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_Lazy_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_MarkAsSerializable_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_MarkAsSerializable"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_MarkAsSerializable")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_MarkAsSerializable", (object) (bool) (VirtualProperties.Entity_MarkAsSerializable_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_MarkAsSerializable", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_MarkAsSerializable")).Value ? true : false));
    }

    public static bool get_Entity_MarkAsSerializable(this Entity obj)
    {
      VirtualProperties.Entity_MarkAsSerializable_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_MarkAsSerializable"];
    }

    public static void set_Entity_MarkAsSerializable(this Entity obj, bool value)
    {
      VirtualProperties.Entity_MarkAsSerializable_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_MarkAsSerializable"] = (object) (bool) (value ? true : false);
    }

    public static bool Entity_MarkAsSerializable_DefaultValue(this Entity entity)
    {
      return true;
    }

    public static bool Entity_MarkAsSerializable_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_MarkAsSerializable_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_Mutable_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_Mutable"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Mutable")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Mutable", (object) (bool) (VirtualProperties.Entity_Mutable_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Mutable", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Mutable")).Value ? true : false));
    }

    public static bool get_Entity_Mutable(this Entity obj)
    {
      VirtualProperties.Entity_Mutable_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Mutable"];
    }

    public static void set_Entity_Mutable(this Entity obj, bool value)
    {
      VirtualProperties.Entity_Mutable_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Mutable"] = (object) (bool) (value ? true : false);
    }

    public static bool Entity_Mutable_DefaultValue(this Entity entity)
    {
      return true;
    }

    public static bool Entity_Mutable_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_Mutable_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_OptimisticLock_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_OptimisticLock"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_OptimisticLock")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_OptimisticLock", (object) VirtualProperties.Entity_OptimisticLock_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_OptimisticLock", (object) (OptimisticLockModes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_OptimisticLock")).Value);
    }

    public static OptimisticLockModes get_Entity_OptimisticLock(this Entity obj)
    {
      VirtualProperties.Entity_OptimisticLock_EnsureInit(obj);
      return (OptimisticLockModes) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_OptimisticLock"];
    }

    public static void set_Entity_OptimisticLock(this Entity obj, OptimisticLockModes value)
    {
      VirtualProperties.Entity_OptimisticLock_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_OptimisticLock"] = (object) value;
    }

    public static OptimisticLockModes Entity_OptimisticLock_DefaultValue(this Entity entity)
    {
      return (OptimisticLockModes) 0;
    }

    public static bool Entity_OptimisticLock_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_OptimisticLock_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_Persister_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_Persister"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Persister")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Persister", (object) VirtualProperties.Entity_Persister_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Persister", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Persister")).Value);
    }

    public static string get_Entity_Persister(this Entity obj)
    {
      VirtualProperties.Entity_Persister_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Persister"];
    }

    public static void set_Entity_Persister(this Entity obj, string value)
    {
      VirtualProperties.Entity_Persister_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Persister"] = (object) value;
    }

    public static string Entity_Persister_DefaultValue(this Entity entity)
    {
      return "";
    }

    public static bool Entity_Persister_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_Persister_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_Proxy_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_Proxy"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Proxy")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Proxy", (object) VirtualProperties.Entity_Proxy_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_Proxy", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_Proxy")).Value);
    }

    public static string get_Entity_Proxy(this Entity obj)
    {
      VirtualProperties.Entity_Proxy_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Proxy"];
    }

    public static void set_Entity_Proxy(this Entity obj, string value)
    {
      VirtualProperties.Entity_Proxy_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_Proxy"] = (object) value;
    }

    public static string Entity_Proxy_DefaultValue(this Entity entity)
    {
      return "";
    }

    public static bool Entity_Proxy_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_Proxy_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_SelectBeforeUpdate_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_SelectBeforeUpdate"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_SelectBeforeUpdate")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_SelectBeforeUpdate", (object) (bool) (VirtualProperties.Entity_SelectBeforeUpdate_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_SelectBeforeUpdate", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_SelectBeforeUpdate")).Value ? true : false));
    }

    public static bool get_Entity_SelectBeforeUpdate(this Entity obj)
    {
      VirtualProperties.Entity_SelectBeforeUpdate_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_SelectBeforeUpdate"];
    }

    public static void set_Entity_SelectBeforeUpdate(this Entity obj, bool value)
    {
      VirtualProperties.Entity_SelectBeforeUpdate_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_SelectBeforeUpdate"] = (object) (bool) (value ? true : false);
    }

    public static bool Entity_SelectBeforeUpdate_DefaultValue(this Entity entity)
    {
      return false;
    }

    public static bool Entity_SelectBeforeUpdate_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_SelectBeforeUpdate_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Entity_SqlWhereClause_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Entity_SqlWhereClause"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_SqlWhereClause")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_SqlWhereClause", (object) VirtualProperties.Entity_SqlWhereClause_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Entity_SqlWhereClause", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Entity_SqlWhereClause")).Value);
    }

    public static string get_Entity_SqlWhereClause(this Entity obj)
    {
      VirtualProperties.Entity_SqlWhereClause_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_SqlWhereClause"];
    }

    public static void set_Entity_SqlWhereClause(this Entity obj, string value)
    {
      VirtualProperties.Entity_SqlWhereClause_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Entity_SqlWhereClause"] = (object) value;
    }

    public static string Entity_SqlWhereClause_DefaultValue(this Entity entity)
    {
      return "";
    }

    public static bool Entity_SqlWhereClause_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool Entity_SqlWhereClause_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void ImplementEqualityMembers_EnsureInit(Entity obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("ImplementEqualityMembers"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "ImplementEqualityMembers")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("ImplementEqualityMembers", (object) (bool) (VirtualProperties.ImplementEqualityMembers_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("ImplementEqualityMembers", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "ImplementEqualityMembers")).Value ? true : false));
    }

    public static bool get_ImplementEqualityMembers(this Entity obj)
    {
      VirtualProperties.ImplementEqualityMembers_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["ImplementEqualityMembers"];
    }

    public static void set_ImplementEqualityMembers(this Entity obj, bool value)
    {
      VirtualProperties.ImplementEqualityMembers_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["ImplementEqualityMembers"] = (object) (bool) (value ? true : false);
    }

    public static bool ImplementEqualityMembers_DefaultValue(this Entity entity)
    {
      return true;
    }

    public static bool ImplementEqualityMembers_DisplayToUser(this Entity entity)
    {
      return true;
    }

    public static bool ImplementEqualityMembers_IsValid(this Entity entity, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_Access_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_Access"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Access")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Access", (object) VirtualProperties.Property_Access_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Access", (object) (PropertyAccessTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Access")).Value);
    }

    public static PropertyAccessTypes get_Property_Access(this Property obj)
    {
      VirtualProperties.Property_Access_EnsureInit(obj);
      return (PropertyAccessTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Access"];
    }

    public static void set_Property_Access(this Property obj, PropertyAccessTypes value)
    {
      VirtualProperties.Property_Access_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Access"] = (object) value;
    }

    public static PropertyAccessTypes Property_Access_DefaultValue(this Property property)
    {
      return (PropertyAccessTypes) 0;
    }

    public static bool Property_Access_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_Access_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_Formula_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_Formula"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Formula")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Formula", (object) VirtualProperties.Property_Formula_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Formula", (object) (string) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Formula")).Value);
    }

    public static string get_Property_Formula(this Property obj)
    {
      VirtualProperties.Property_Formula_EnsureInit(obj);
      return (string) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Formula"];
    }

    public static void set_Property_Formula(this Property obj, string value)
    {
      VirtualProperties.Property_Formula_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Formula"] = (object) value;
    }

    public static string Property_Formula_DefaultValue(this Property property)
    {
      return "";
    }

    public static bool Property_Formula_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_Formula_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_Generation_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_Generation"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Generation")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Generation", (object) VirtualProperties.Property_Generation_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Generation", (object) (PropertyGeneratedTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Generation")).Value);
    }

    public static PropertyGeneratedTypes get_Property_Generation(this Property obj)
    {
      VirtualProperties.Property_Generation_EnsureInit(obj);
      return (PropertyGeneratedTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Generation"];
    }

    public static void set_Property_Generation(this Property obj, PropertyGeneratedTypes value)
    {
      VirtualProperties.Property_Generation_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Generation"] = (object) value;
    }

    public static PropertyGeneratedTypes Property_Generation_DefaultValue(this Property property)
    {
      return (PropertyGeneratedTypes) 0;
    }

    public static bool Property_Generation_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_Generation_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_Insert_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_Insert"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Insert")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Insert", (object) (bool) (VirtualProperties.Property_Insert_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Insert", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Insert")).Value ? true : false));
    }

    public static bool get_Property_Insert(this Property obj)
    {
      VirtualProperties.Property_Insert_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Insert"];
    }

    public static void set_Property_Insert(this Property obj, bool value)
    {
      VirtualProperties.Property_Insert_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Insert"] = (object) (bool) (value ? true : false);
    }

    public static bool Property_Insert_DefaultValue(this Property property)
    {
      return true;
    }

    public static bool Property_Insert_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_Insert_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_IsLazy_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_IsLazy"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_IsLazy")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_IsLazy", (object) (bool) (VirtualProperties.Property_IsLazy_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_IsLazy", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_IsLazy")).Value ? true : false));
    }

    public static bool get_Property_IsLazy(this Property obj)
    {
      VirtualProperties.Property_IsLazy_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_IsLazy"];
    }

    public static void set_Property_IsLazy(this Property obj, bool value)
    {
      VirtualProperties.Property_IsLazy_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_IsLazy"] = (object) (bool) (value ? true : false);
    }

    public static bool Property_IsLazy_DefaultValue(this Property property)
    {
      return false;
    }

    public static bool Property_IsLazy_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_IsLazy_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_IsVersion_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_IsVersion"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_IsVersion")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_IsVersion", (object) (bool) (VirtualProperties.Property_IsVersion_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_IsVersion", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_IsVersion")).Value ? true : false));
    }

    public static bool get_Property_IsVersion(this Property obj)
    {
      VirtualProperties.Property_IsVersion_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_IsVersion"];
    }

    public static void set_Property_IsVersion(this Property obj, bool value)
    {
      VirtualProperties.Property_IsVersion_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_IsVersion"] = (object) (bool) (value ? true : false);
    }

    public static bool Property_IsVersion_DefaultValue(this Property property)
    {
      return false;
    }

    public static bool Property_IsVersion_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_IsVersion_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_OptimisticLock_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_OptimisticLock"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_OptimisticLock")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_OptimisticLock", (object) (bool) (VirtualProperties.Property_OptimisticLock_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_OptimisticLock", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_OptimisticLock")).Value ? true : false));
    }

    public static bool get_Property_OptimisticLock(this Property obj)
    {
      VirtualProperties.Property_OptimisticLock_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_OptimisticLock"];
    }

    public static void set_Property_OptimisticLock(this Property obj, bool value)
    {
      VirtualProperties.Property_OptimisticLock_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_OptimisticLock"] = (object) (bool) (value ? true : false);
    }

    public static bool Property_OptimisticLock_DefaultValue(this Property property)
    {
      return true;
    }

    public static bool Property_OptimisticLock_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_OptimisticLock_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_Update_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_Update"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Update")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Update", (object) (bool) (VirtualProperties.Property_Update_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_Update", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_Update")).Value ? true : false));
    }

    public static bool get_Property_Update(this Property obj)
    {
      VirtualProperties.Property_Update_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Update"];
    }

    public static void set_Property_Update(this Property obj, bool value)
    {
      VirtualProperties.Property_Update_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_Update"] = (object) (bool) (value ? true : false);
    }

    public static bool Property_Update_DefaultValue(this Property property)
    {
      return true;
    }

    public static bool Property_Update_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_Update_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Property_UsePrivateSetter_EnsureInit(Property obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Property_UsePrivateSetter"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_UsePrivateSetter")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_UsePrivateSetter", (object) (bool) (VirtualProperties.Property_UsePrivateSetter_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Property_UsePrivateSetter", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Property_UsePrivateSetter")).Value ? true : false));
    }

    public static bool get_Property_UsePrivateSetter(this Property obj)
    {
      VirtualProperties.Property_UsePrivateSetter_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Property_UsePrivateSetter"];
    }

    public static void set_Property_UsePrivateSetter(this Property obj, bool value)
    {
      VirtualProperties.Property_UsePrivateSetter_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Property_UsePrivateSetter"] = (object) (bool) (value ? true : false);
    }

    public static bool Property_UsePrivateSetter_DefaultValue(this Property property)
    {
      return false;
    }

    public static bool Property_UsePrivateSetter_DisplayToUser(this Property property)
    {
      return true;
    }

    public static bool Property_UsePrivateSetter_IsValid(this Property property, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1Cascade_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1Cascade"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Cascade")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Cascade", (object) VirtualProperties.Reference_End1Cascade_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Cascade", (object) (CascadeTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Cascade")).Value);
    }

    public static CascadeTypes get_Reference_End1Cascade(this Reference obj)
    {
      VirtualProperties.Reference_End1Cascade_EnsureInit(obj);
      return (CascadeTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Cascade"];
    }

    public static void set_Reference_End1Cascade(this Reference obj, CascadeTypes value)
    {
      VirtualProperties.Reference_End1Cascade_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Cascade"] = (object) value;
    }

    public static CascadeTypes Reference_End1Cascade_DefaultValue(this Reference reference)
    {
      return (CascadeTypes) 0;
    }

    public static bool Reference_End1Cascade_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality1);
    }

    public static bool Reference_End1Cascade_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1CollectionCascade_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1CollectionCascade"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1CollectionCascade")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1CollectionCascade", (object) VirtualProperties.Reference_End1CollectionCascade_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1CollectionCascade", (object) (CollectionCascadeTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1CollectionCascade")).Value);
    }

    public static CollectionCascadeTypes get_Reference_End1CollectionCascade(this Reference obj)
    {
      VirtualProperties.Reference_End1CollectionCascade_EnsureInit(obj);
      return (CollectionCascadeTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1CollectionCascade"];
    }

    public static void set_Reference_End1CollectionCascade(this Reference obj, CollectionCascadeTypes value)
    {
      VirtualProperties.Reference_End1CollectionCascade_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1CollectionCascade"] = (object) value;
    }

    public static CollectionCascadeTypes Reference_End1CollectionCascade_DefaultValue(this Reference reference)
    {
      return (CollectionCascadeTypes) 0;
    }

    public static bool Reference_End1CollectionCascade_DisplayToUser(this Reference reference)
    {
      return Cardinality.Many.Equals(reference.Cardinality1);
    }

    public static bool Reference_End1CollectionCascade_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1CollectionFetchMode_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1CollectionFetchMode"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1CollectionFetchMode")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1CollectionFetchMode", (object) VirtualProperties.Reference_End1CollectionFetchMode_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1CollectionFetchMode", (object) (CollectionFetchModes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1CollectionFetchMode")).Value);
    }

    public static CollectionFetchModes get_Reference_End1CollectionFetchMode(this Reference obj)
    {
      VirtualProperties.Reference_End1CollectionFetchMode_EnsureInit(obj);
      return (CollectionFetchModes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1CollectionFetchMode"];
    }

    public static void set_Reference_End1CollectionFetchMode(this Reference obj, CollectionFetchModes value)
    {
      VirtualProperties.Reference_End1CollectionFetchMode_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1CollectionFetchMode"] = (object) value;
    }

    public static CollectionFetchModes Reference_End1CollectionFetchMode_DefaultValue(this Reference reference)
    {
      return (CollectionFetchModes) 0;
    }

    public static bool Reference_End1CollectionFetchMode_DisplayToUser(this Reference reference)
    {
      return Cardinality.Many.Equals(reference.Cardinality1);
    }

    public static bool Reference_End1CollectionFetchMode_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1FetchMode_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1FetchMode"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1FetchMode")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1FetchMode", (object) VirtualProperties.Reference_End1FetchMode_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1FetchMode", (object) (FetchModes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1FetchMode")).Value);
    }

    public static FetchModes get_Reference_End1FetchMode(this Reference obj)
    {
      VirtualProperties.Reference_End1FetchMode_EnsureInit(obj);
      return (FetchModes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1FetchMode"];
    }

    public static void set_Reference_End1FetchMode(this Reference obj, FetchModes value)
    {
      VirtualProperties.Reference_End1FetchMode_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1FetchMode"] = (object) value;
    }

    public static FetchModes Reference_End1FetchMode_DefaultValue(this Reference reference)
    {
      return (FetchModes) 0;
    }

    public static bool Reference_End1FetchMode_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality1);
    }

    public static bool Reference_End1FetchMode_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1Insert_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1Insert"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Insert")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Insert", (object) (bool) (VirtualProperties.Reference_End1Insert_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Insert", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Insert")).Value ? true : false));
    }

    public static bool get_Reference_End1Insert(this Reference obj)
    {
      VirtualProperties.Reference_End1Insert_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Insert"];
    }

    public static void set_Reference_End1Insert(this Reference obj, bool value)
    {
      VirtualProperties.Reference_End1Insert_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Insert"] = (object) (bool) (value ? true : false);
    }

    public static bool Reference_End1Insert_DefaultValue(this Reference reference)
    {
      return true;
    }

    public static bool Reference_End1Insert_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality1) && Cardinality.Many.Equals(reference.Cardinality2);
    }

    public static bool Reference_End1Insert_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1Lazy_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1Lazy"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Lazy")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Lazy", (object) VirtualProperties.Reference_End1Lazy_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Lazy", (object) (CollectionLazyTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Lazy")).Value);
    }

    public static CollectionLazyTypes get_Reference_End1Lazy(this Reference obj)
    {
      VirtualProperties.Reference_End1Lazy_EnsureInit(obj);
      return (CollectionLazyTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Lazy"];
    }

    public static void set_Reference_End1Lazy(this Reference obj, CollectionLazyTypes value)
    {
      VirtualProperties.Reference_End1Lazy_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Lazy"] = (object) value;
    }

    public static CollectionLazyTypes Reference_End1Lazy_DefaultValue(this Reference reference)
    {
      return (CollectionLazyTypes) 0;
    }

    public static bool Reference_End1Lazy_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality1 == Cardinality.Many);
    }

    public static bool Reference_End1Lazy_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1OrderByIsAsc_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1OrderByIsAsc"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1OrderByIsAsc")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1OrderByIsAsc", (object) (bool) (VirtualProperties.Reference_End1OrderByIsAsc_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1OrderByIsAsc", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1OrderByIsAsc")).Value ? true : false));
    }

    public static bool get_Reference_End1OrderByIsAsc(this Reference obj)
    {
      VirtualProperties.Reference_End1OrderByIsAsc_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1OrderByIsAsc"];
    }

    public static void set_Reference_End1OrderByIsAsc(this Reference obj, bool value)
    {
      VirtualProperties.Reference_End1OrderByIsAsc_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1OrderByIsAsc"] = (object) (bool) (value ? true : false);
    }

    public static bool Reference_End1OrderByIsAsc_DefaultValue(this Reference reference)
    {
      return true;
    }

    public static bool Reference_End1OrderByIsAsc_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality1 == Cardinality.Many);
    }

    public static bool Reference_End1OrderByIsAsc_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1OrderByProperty_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1OrderByProperty"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1OrderByProperty")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1OrderByProperty", (object) VirtualProperties.Reference_End1OrderByProperty_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1OrderByProperty", (object) (PropertiesForThisEntity) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1OrderByProperty")).Value);
    }

    public static PropertiesForThisEntity get_Reference_End1OrderByProperty(this Reference obj)
    {
      VirtualProperties.Reference_End1OrderByProperty_EnsureInit(obj);
      return (PropertiesForThisEntity) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1OrderByProperty"];
    }

    public static void set_Reference_End1OrderByProperty(this Reference obj, PropertiesForThisEntity value)
    {
      VirtualProperties.Reference_End1OrderByProperty_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1OrderByProperty"] = (object) value;
    }

    public static PropertiesForThisEntity Reference_End1OrderByProperty_DefaultValue(this Reference reference)
    {
      return (PropertiesForThisEntity) 0;
    }

    public static bool Reference_End1OrderByProperty_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality1 == Cardinality.Many);
    }

    public static bool Reference_End1OrderByProperty_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End1Update_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End1Update"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Update")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Update", (object) (bool) (VirtualProperties.Reference_End1Update_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End1Update", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End1Update")).Value ? true : false));
    }

    public static bool get_Reference_End1Update(this Reference obj)
    {
      VirtualProperties.Reference_End1Update_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Update"];
    }

    public static void set_Reference_End1Update(this Reference obj, bool value)
    {
      VirtualProperties.Reference_End1Update_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End1Update"] = (object) (bool) (value ? true : false);
    }

    public static bool Reference_End1Update_DefaultValue(this Reference reference)
    {
      return true;
    }

    public static bool Reference_End1Update_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality1) && Cardinality.Many.Equals(reference.Cardinality2);
    }

    public static bool Reference_End1Update_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2Cascade_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2Cascade"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Cascade")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Cascade", (object) VirtualProperties.Reference_End2Cascade_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Cascade", (object) (CascadeTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Cascade")).Value);
    }

    public static CascadeTypes get_Reference_End2Cascade(this Reference obj)
    {
      VirtualProperties.Reference_End2Cascade_EnsureInit(obj);
      return (CascadeTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Cascade"];
    }

    public static void set_Reference_End2Cascade(this Reference obj, CascadeTypes value)
    {
      VirtualProperties.Reference_End2Cascade_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Cascade"] = (object) value;
    }

    public static CascadeTypes Reference_End2Cascade_DefaultValue(this Reference reference)
    {
      return (CascadeTypes) 0;
    }

    public static bool Reference_End2Cascade_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality2);
    }

    public static bool Reference_End2Cascade_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2CollectionCascade_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2CollectionCascade"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2CollectionCascade")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2CollectionCascade", (object) VirtualProperties.Reference_End2CollectionCascade_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2CollectionCascade", (object) (CollectionCascadeTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2CollectionCascade")).Value);
    }

    public static CollectionCascadeTypes get_Reference_End2CollectionCascade(this Reference obj)
    {
      VirtualProperties.Reference_End2CollectionCascade_EnsureInit(obj);
      return (CollectionCascadeTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2CollectionCascade"];
    }

    public static void set_Reference_End2CollectionCascade(this Reference obj, CollectionCascadeTypes value)
    {
      VirtualProperties.Reference_End2CollectionCascade_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2CollectionCascade"] = (object) value;
    }

    public static CollectionCascadeTypes Reference_End2CollectionCascade_DefaultValue(this Reference reference)
    {
      return (CollectionCascadeTypes) 0;
    }

    public static bool Reference_End2CollectionCascade_DisplayToUser(this Reference reference)
    {
      return Cardinality.Many.Equals(reference.Cardinality2);
    }

    public static bool Reference_End2CollectionCascade_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2CollectionFetchMode_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2CollectionFetchMode"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2CollectionFetchMode")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2CollectionFetchMode", (object) VirtualProperties.Reference_End2CollectionFetchMode_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2CollectionFetchMode", (object) (CollectionFetchModes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2CollectionFetchMode")).Value);
    }

    public static CollectionFetchModes get_Reference_End2CollectionFetchMode(this Reference obj)
    {
      VirtualProperties.Reference_End2CollectionFetchMode_EnsureInit(obj);
      return (CollectionFetchModes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2CollectionFetchMode"];
    }

    public static void set_Reference_End2CollectionFetchMode(this Reference obj, CollectionFetchModes value)
    {
      VirtualProperties.Reference_End2CollectionFetchMode_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2CollectionFetchMode"] = (object) value;
    }

    public static CollectionFetchModes Reference_End2CollectionFetchMode_DefaultValue(this Reference reference)
    {
      return (CollectionFetchModes) 0;
    }

    public static bool Reference_End2CollectionFetchMode_DisplayToUser(this Reference reference)
    {
      return Cardinality.Many.Equals(reference.Cardinality2);
    }

    public static bool Reference_End2CollectionFetchMode_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2FetchMode_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2FetchMode"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2FetchMode")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2FetchMode", (object) VirtualProperties.Reference_End2FetchMode_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2FetchMode", (object) (FetchModes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2FetchMode")).Value);
    }

    public static FetchModes get_Reference_End2FetchMode(this Reference obj)
    {
      VirtualProperties.Reference_End2FetchMode_EnsureInit(obj);
      return (FetchModes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2FetchMode"];
    }

    public static void set_Reference_End2FetchMode(this Reference obj, FetchModes value)
    {
      VirtualProperties.Reference_End2FetchMode_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2FetchMode"] = (object) value;
    }

    public static FetchModes Reference_End2FetchMode_DefaultValue(this Reference reference)
    {
      return (FetchModes) 0;
    }

    public static bool Reference_End2FetchMode_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality2);
    }

    public static bool Reference_End2FetchMode_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2Insert_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2Insert"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Insert")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Insert", (object) (bool) (VirtualProperties.Reference_End2Insert_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Insert", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Insert")).Value ? true : false));
    }

    public static bool get_Reference_End2Insert(this Reference obj)
    {
      VirtualProperties.Reference_End2Insert_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Insert"];
    }

    public static void set_Reference_End2Insert(this Reference obj, bool value)
    {
      VirtualProperties.Reference_End2Insert_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Insert"] = (object) (bool) (value ? true : false);
    }

    public static bool Reference_End2Insert_DefaultValue(this Reference reference)
    {
      return true;
    }

    public static bool Reference_End2Insert_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality2) && Cardinality.Many.Equals(reference.Cardinality1);
    }

    public static bool Reference_End2Insert_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2Lazy_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2Lazy"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Lazy")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Lazy", (object) VirtualProperties.Reference_End2Lazy_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Lazy", (object) (CollectionLazyTypes) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Lazy")).Value);
    }

    public static CollectionLazyTypes get_Reference_End2Lazy(this Reference obj)
    {
      VirtualProperties.Reference_End2Lazy_EnsureInit(obj);
      return (CollectionLazyTypes) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Lazy"];
    }

    public static void set_Reference_End2Lazy(this Reference obj, CollectionLazyTypes value)
    {
      VirtualProperties.Reference_End2Lazy_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Lazy"] = (object) value;
    }

    public static CollectionLazyTypes Reference_End2Lazy_DefaultValue(this Reference reference)
    {
      return (CollectionLazyTypes) 0;
    }

    public static bool Reference_End2Lazy_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality2 == Cardinality.Many);
    }

    public static bool Reference_End2Lazy_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2OrderByIsAsc_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2OrderByIsAsc"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2OrderByIsAsc")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2OrderByIsAsc", (object) (bool) (VirtualProperties.Reference_End2OrderByIsAsc_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2OrderByIsAsc", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2OrderByIsAsc")).Value ? true : false));
    }

    public static bool get_Reference_End2OrderByIsAsc(this Reference obj)
    {
      VirtualProperties.Reference_End2OrderByIsAsc_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2OrderByIsAsc"];
    }

    public static void set_Reference_End2OrderByIsAsc(this Reference obj, bool value)
    {
      VirtualProperties.Reference_End2OrderByIsAsc_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2OrderByIsAsc"] = (object) (bool) (value ? true : false);
    }

    public static bool Reference_End2OrderByIsAsc_DefaultValue(this Reference reference)
    {
      return true;
    }

    public static bool Reference_End2OrderByIsAsc_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality2 == Cardinality.Many);
    }

    public static bool Reference_End2OrderByIsAsc_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2OrderByProperty_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2OrderByProperty"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2OrderByProperty")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2OrderByProperty", (object) VirtualProperties.Reference_End2OrderByProperty_DefaultValue(obj));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2OrderByProperty", (object) (PropertiesForThisEntity) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2OrderByProperty")).Value);
    }

    public static PropertiesForThisEntity get_Reference_End2OrderByProperty(this Reference obj)
    {
      VirtualProperties.Reference_End2OrderByProperty_EnsureInit(obj);
      return (PropertiesForThisEntity) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2OrderByProperty"];
    }

    public static void set_Reference_End2OrderByProperty(this Reference obj, PropertiesForThisEntity value)
    {
      VirtualProperties.Reference_End2OrderByProperty_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2OrderByProperty"] = (object) value;
    }

    public static PropertiesForThisEntity Reference_End2OrderByProperty_DefaultValue(this Reference reference)
    {
      return (PropertiesForThisEntity) 0;
    }

    public static bool Reference_End2OrderByProperty_DisplayToUser(this Reference reference)
    {
      return (reference.Cardinality2 == Cardinality.Many);
    }

    public static bool Reference_End2OrderByProperty_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }

    public static void Reference_End2Update_EnsureInit(Reference obj)
    {
      if (!VirtualProperties.VirtualPropertyValues.ContainsKey((object) obj))
        VirtualProperties.VirtualPropertyValues.Add((object) obj, new Dictionary<string, object>());
      if (VirtualProperties.VirtualPropertyValues[(object) obj].ContainsKey("Reference_End2Update"))
        return;
      if (!Enumerable.Any<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Update")))
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Update", (object) (bool) (VirtualProperties.Reference_End2Update_DefaultValue(obj) ? true : false));
      else
        VirtualProperties.VirtualPropertyValues[(object) obj].Add("Reference_End2Update", (object) (bool) ((bool) Enumerable.First<IUserOption>((IEnumerable<IUserOption>) ((IScriptBaseObject) obj).Ex, (Func<IUserOption, bool>) (uo => uo.Name == "Reference_End2Update")).Value ? true : false));
    }

    public static bool get_Reference_End2Update(this Reference obj)
    {
      VirtualProperties.Reference_End2Update_EnsureInit(obj);
      return (bool) VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Update"];
    }

    public static void set_Reference_End2Update(this Reference obj, bool value)
    {
      VirtualProperties.Reference_End2Update_EnsureInit(obj);
      VirtualProperties.VirtualPropertyValues[(object) obj]["Reference_End2Update"] = (object) (bool) (value ? true : false);
    }

    public static bool Reference_End2Update_DefaultValue(this Reference reference)
    {
      return true;
    }

    public static bool Reference_End2Update_DisplayToUser(this Reference reference)
    {
      return Cardinality.One.Equals(reference.Cardinality2) && Cardinality.Many.Equals(reference.Cardinality1);
    }

    public static bool Reference_End2Update_IsValid(this Reference reference, out string failReason)
    {
      failReason = "";
      return true;
    }
  }
}
