// Decompiled with JetBrains decompiler
// Type: NHibernate.LanguageAttribute
// Assembly: NHibernate.AAT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 40840706-A565-4F76-8EF9-260A13165A0A
// Assembly location: C:\Projekte\OpenSource\VNH_Luedi\src\3rd_Party_Libs\NHibernate.AAT.DLL

using System;

namespace NHibernate
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public class LanguageAttribute : Attribute
  {
    protected string language;

    public string Language
    {
      get
      {
        return this.language;
      }
    }

    public LanguageAttribute(string language)
    {
      this.language = language;
    }

    public override string ToString()
    {
      return this.language;
    }
  }
}
