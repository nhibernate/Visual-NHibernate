// Decompiled with JetBrains decompiler
// Type: NHibernate.ScriptFunctionWrapper
// Assembly: NHibernate.AAT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 40840706-A565-4F76-8EF9-260A13165A0A
// Assembly location: C:\Projekte\OpenSource\VNH_Luedi\src\3rd_Party_Libs\NHibernate.AAT.DLL

using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Wizards.NewProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHibernate
{
  public class ScriptFunctionWrapper : ArchAngel.Interfaces.ScriptFunctionWrapper
  {
    private static TemplateGen instance = new TemplateGen();

    public ScriptFunctionWrapper() 
    {
    }

    private string GetParamTypeString(object[] parameters)
    {
      StringBuilder stringBuilder = new StringBuilder(100);
      foreach (object obj in parameters)
        stringBuilder.Append(obj.GetType().FullName + ",");
      return stringBuilder.ToString().TrimEnd(',').Replace("+", ".");
    }

    public virtual object RunScriptFunction(string functionName, ref object[] parameters)
    {
      object obj;
      switch (functionName)
      {
        case "CustomNewProjectScreens":
          List<INewProjectScreen> screens = (List<INewProjectScreen>) parameters[0];
          TemplateGen.CustomNewProjectScreens(out screens);
          obj = (object) null;
          parameters[0] = (object) screens;
          break;
        case "LoadProjectOutput":
          string outputFolder = (string) parameters[0];
          IEnumerable<ProviderInfo> provider = (IEnumerable<ProviderInfo>) parameters[1];
          TemplateData extraData = (TemplateData) parameters[2];
          TemplateGen.LoadProjectOutput(outputFolder, provider, extraData);
          obj = (object) null;
          parameters[0] = (object) outputFolder;
          parameters[1] = (object) provider;
          parameters[2] = (object) extraData;
          break;
        case "PreGenerationModelInitialisation":
          ProviderInfo providerInfo = (ProviderInfo) parameters[0];
          PreGenerationData data = (PreGenerationData) parameters[1];
          TemplateGen.PreGenerationModelInitialisation(providerInfo, data);
          obj = (object) null;
          parameters[0] = (object) providerInfo;
          parameters[1] = (object) data;
          break;
        case "InternalFunctions.MustSkipCurrentFile":
          return (object) (int) (ScriptFunctionWrapper.instance.SkipCurrentFile ? 1 : 0);
        case "InternalFunctions.ResetSkipCurrentFile":
          ScriptFunctionWrapper.instance.SkipCurrentFile = false;
          return (object) null;
        case "InternalFunctions.GetCurrentFileName":
          return (object) ScriptFunctionWrapper.instance.CurrentFileName;
        case "InternalFunctions.ResetCurrentFileName":
          ScriptFunctionWrapper.instance.CurrentFileName = "";
          return (object) null;
        case "InternalFunctions.SetGeneratedFileName":
          ScriptFunctionWrapper.instance.GeneratedFileName = (string) parameters[0];
          return (object) null;
        case "InternalFunctions.ClearTemplateCache":
          ScriptFunctionWrapper.instance.ClearTemplateCache();
          return (object) null;
        default:
          throw new Exception("Function not handled in RunScriptFunction:" + functionName);
      }
      return obj;
    }

    public virtual bool RunApiExtensionFunction(string functionName, out object result, ref object[] parameters)
    {
      result = (object) null;
      functionName = functionName.Replace(".", "_");
      return false;
    }
  }
}
