// Decompiled with JetBrains decompiler
// Type: NHibernate.TemplateGen
// Assembly: NHibernate.AAT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 40840706-A565-4F76-8EF9-260A13165A0A
// Assembly location: C:\Projekte\OpenSource\VNH_Luedi\src\3rd_Party_Libs\NHibernate.AAT.DLL

using ArchAngel.Interfaces;
using ArchAngel.Interfaces.NHibernateEnums;
using ArchAngel.Interfaces.Wizards.NewProject;
using ArchAngel.NHibernateHelper;
using ArchAngel.NHibernateHelper.LoadProjectWizard;
using ArchAngel.Providers.EntityModel;
using Slyce.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;

namespace NHibernate
{
  [Serializable]
  public class TemplateGen
  {
    public bool SkipCurrentFile = false;
    public string CurrentFileName = "";
    public string GeneratedFileName = "";
    public Dictionary<string, object> TemplateCache = new Dictionary<string, object>();
    private Stack<StringBuilder> _SBStack = new Stack<StringBuilder>();
    private static List<string> m_assemblySearchPaths = new List<string>();

    public static List<string> AssemblySearchPaths
    {
      get
      {
        return TemplateGen.m_assemblySearchPaths;
      }
      set
      {
        TemplateGen.m_assemblySearchPaths = value;
      }
    }

    public void ClearTemplateCache()
    {
      this.TemplateCache.Clear();
      VirtualProperties.ClearCache();
    }

		public static string GetProjectInfoXml()
    {
			Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("NHibernate.options.xml");
			string docText = new StreamReader(s).ReadToEnd();
			return docText;
    }

		[Language("C#")]
    public static void CustomNewProjectScreens(out List<INewProjectScreen> screens)
    {
      screens = new List<INewProjectScreen>();
      screens.Add((INewProjectScreen) new LoadExistingProject());
      screens.Add((INewProjectScreen) new LoadExistingDatabase());
      screens.Add((INewProjectScreen) new SelectDatabaseObjects());
      screens.Add((INewProjectScreen) new Prefixes());
      screens.Add((INewProjectScreen) new SetNhConfig());
    }

    [Language("C#")]
    public static void LoadProjectOutput(string outputFolder, IEnumerable<ArchAngel.Interfaces.ProviderInfo> provider, TemplateData extraData)
    {
    }

    [Language("C#")]
    public static void PreGenerationModelInitialisation(ArchAngel.Interfaces.ProviderInfo provider, PreGenerationData data)
    {
      if (provider == null)
        return;
      if (provider is ArchAngel.Providers.EntityModel.ProviderInfo)
      {
        new EntityModelPreprocessor((IFileController) new FileController()).InitialiseEntityModel((ArchAngel.Providers.EntityModel.ProviderInfo)provider, data);
      }
      else
      {
        if (!(provider is ArchAngel.NHibernateHelper.ProviderInfo))
          return;
        new NHibernateProjectPreprocessor((IFileController) new FileController()).InitialiseNHibernateProject((ArchAngel.NHibernateHelper.ProviderInfo) provider, data);
      }
    }

    public StringBuilder GetCurrentStringBuilder()
    {
      return this._SBStack.Peek();
    }

    private void Write(object s)
    {
      if (s == null)
        return;
      StringBuilder stringBuilder = this._SBStack.Peek();
      stringBuilder.Insert(stringBuilder.Length, s.ToString());
    }

    private void WriteLine(object s)
    {
      if (s == null)
        return;
      StringBuilder stringBuilder = this._SBStack.Peek();
      stringBuilder.Insert(stringBuilder.Length, s.ToString() + Environment.NewLine);
    }

    private void WriteFormat(string format, params object[] args)
    {
      if (string.IsNullOrEmpty(format))
        return;
      StringBuilder stringBuilder = this._SBStack.Peek();
      stringBuilder.Insert(stringBuilder.Length, string.Format(format, args));
    }

    private void Write(bool val, object trueText)
    {
      if (!val || trueText == null)
        return;
      StringBuilder stringBuilder = this._SBStack.Peek();
      stringBuilder.Insert(stringBuilder.Length, trueText.ToString());
    }

    private void Write(bool val, object trueText, object falseText)
    {
      if (val && trueText != null)
      {
        StringBuilder stringBuilder = this._SBStack.Peek();
        stringBuilder.Insert(stringBuilder.Length, trueText.ToString());
      }
      else
      {
        if (val || falseText == null)
          return;
        StringBuilder stringBuilder = this._SBStack.Peek();
        stringBuilder.Insert(stringBuilder.Length, falseText.ToString());
      }
    }

    internal class DynamicFilenames
    {
    }

    internal class DynamicFolderNames
    {
    }

    public static class UserOptions
    {
      private static Dictionary<string, object> UserOptionValues = new Dictionary<string, object>();
      private static bool _AutoImport = false;
      private static bool _AutoImport_Loaded = false;
      private static BytecodeGenerator _BytecodeGenerator = (BytecodeGenerator) 0;
      private static bool _BytecodeGenerator_Loaded = false;
      private static string _CacheProviderClass = (string) null;
      private static bool _CacheProviderClass_Loaded = false;
      private static string _CacheQueryCacheFactory = (string) null;
      private static bool _CacheQueryCacheFactory_Loaded = false;
      private static string _CacheRegionPrefix = (string) null;
      private static bool _CacheRegionPrefix_Loaded = false;
      private static bool? _CacheUserMinimalPuts = new bool?();
      private static bool _CacheUserMinimalPuts_Loaded = false;
      private static bool? _CacheUserQueryCache = new bool?();
      private static bool _CacheUserQueryCache_Loaded = false;
      private static TopLevelAccessTypes _DefaultAccess = (TopLevelAccessTypes) 0;
      private static bool _DefaultAccess_Loaded = false;
      private static TopLevelCascadeTypes _DefaultCascade = (TopLevelCascadeTypes) 0;
      private static bool _DefaultCascade_Loaded = false;
      private static TopLevelCollectionCascadeTypes _DefaultCollectionCascade = (TopLevelCollectionCascadeTypes) 0;
      private static bool _DefaultCollectionCascade_Loaded = false;
      private static bool _DefaultCollectionLazy = false;
      private static bool _DefaultCollectionLazy_Loaded = false;
      private static bool _DefaultInverse = false;
      private static bool _DefaultInverse_Loaded = false;
      private static bool _DefaultLazy = false;
      private static bool _DefaultLazy_Loaded = false;
      private static bool _GeneratePartialClasses = false;
      private static bool _GeneratePartialClasses_Loaded = false;
      private static bool? _GenerateStatistics = new bool?();
      private static bool _GenerateStatistics_Loaded = false;
      private static int? _MaxFetchDepth = new int?();
      private static bool _MaxFetchDepth_Loaded = false;
      private static NHibernateVersions _NHibernateVersion = (NHibernateVersions) 0;
      private static bool _NHibernateVersion_Loaded = false;
      private static string _ProjectGuid = (string) null;
      private static bool _ProjectGuid_Loaded = false;
      private static string _ProjectName = (string) null;
      private static bool _ProjectName_Loaded = false;
      private static string _ProjectNamespace = (string) null;
      private static bool _ProjectNamespace_Loaded = false;
      private static string _QuerySubstitutions = (string) null;
      private static bool _QuerySubstitutions_Loaded = false;
      private static SourceCodeMultiLineType _ReferenceTemplate = (SourceCodeMultiLineType) null;
      private static bool _ReferenceTemplate_Loaded = false;
      private static bool? _ShowSql = new bool?();
      private static bool _ShowSql_Loaded = false;
      private static string _TransactionFactoryClass = (string) null;
      private static bool _TransactionFactoryClass_Loaded = false;
      private static bool _UseFluentNHibernate = false;
      private static bool _UseFluentNHibernate_Loaded = false;
      private static bool? _UseOuterJoin = new bool?();
      private static bool _UseOuterJoin_Loaded = false;
      private static bool _UsePrivateSettersOnProperties = false;
      private static bool _UsePrivateSettersOnProperties_Loaded = false;
      private static bool? _UseProxyValidator = new bool?();
      private static bool _UseProxyValidator_Loaded = false;
      private static bool _UseSpatial = false;
      private static bool _UseSpatial_Loaded = false;
      private static bool _UseSubNamespaceForSchemas = false;
      private static bool _UseSubNamespaceForSchemas_Loaded = false;
      private static VisualStudioVersions _VisualStudioVersion = (VisualStudioVersions) 0;
      private static bool _VisualStudioVersion_Loaded = false;

      public static bool AutoImport
      {
        get
        {
          if (!TemplateGen.UserOptions._AutoImport_Loaded)
          {
            TemplateGen.UserOptions._AutoImport = TemplateGen.UserOptions.AutoImport_DefaultValue();
            TemplateGen.UserOptions._AutoImport_Loaded = true;
          }
          return TemplateGen.UserOptions._AutoImport;
        }
        set
        {
          TemplateGen.UserOptions._AutoImport = value;
          TemplateGen.UserOptions._AutoImport_Loaded = true;
        }
      }

      public static BytecodeGenerator BytecodeGenerator
      {
        get
        {
          if (!TemplateGen.UserOptions._BytecodeGenerator_Loaded)
          {
            TemplateGen.UserOptions._BytecodeGenerator = TemplateGen.UserOptions.BytecodeGenerator_DefaultValue();
            TemplateGen.UserOptions._BytecodeGenerator_Loaded = true;
          }
          return TemplateGen.UserOptions._BytecodeGenerator;
        }
        set
        {
          TemplateGen.UserOptions._BytecodeGenerator = value;
          TemplateGen.UserOptions._BytecodeGenerator_Loaded = true;
        }
      }

      public static string CacheProviderClass
      {
        get
        {
          if (!TemplateGen.UserOptions._CacheProviderClass_Loaded)
          {
            TemplateGen.UserOptions._CacheProviderClass = TemplateGen.UserOptions.CacheProviderClass_DefaultValue();
            TemplateGen.UserOptions._CacheProviderClass_Loaded = true;
          }
          return TemplateGen.UserOptions._CacheProviderClass;
        }
        set
        {
          TemplateGen.UserOptions._CacheProviderClass = value;
          TemplateGen.UserOptions._CacheProviderClass_Loaded = true;
        }
      }

      public static string CacheQueryCacheFactory
      {
        get
        {
          if (!TemplateGen.UserOptions._CacheQueryCacheFactory_Loaded)
          {
            TemplateGen.UserOptions._CacheQueryCacheFactory = TemplateGen.UserOptions.CacheQueryCacheFactory_DefaultValue();
            TemplateGen.UserOptions._CacheQueryCacheFactory_Loaded = true;
          }
          return TemplateGen.UserOptions._CacheQueryCacheFactory;
        }
        set
        {
          TemplateGen.UserOptions._CacheQueryCacheFactory = value;
          TemplateGen.UserOptions._CacheQueryCacheFactory_Loaded = true;
        }
      }

      public static string CacheRegionPrefix
      {
        get
        {
          if (!TemplateGen.UserOptions._CacheRegionPrefix_Loaded)
          {
            TemplateGen.UserOptions._CacheRegionPrefix = TemplateGen.UserOptions.CacheRegionPrefix_DefaultValue();
            TemplateGen.UserOptions._CacheRegionPrefix_Loaded = true;
          }
          return TemplateGen.UserOptions._CacheRegionPrefix;
        }
        set
        {
          TemplateGen.UserOptions._CacheRegionPrefix = value;
          TemplateGen.UserOptions._CacheRegionPrefix_Loaded = true;
        }
      }

      public static bool? CacheUserMinimalPuts
      {
        get
        {
          if (!TemplateGen.UserOptions._CacheUserMinimalPuts_Loaded)
          {
            TemplateGen.UserOptions._CacheUserMinimalPuts = TemplateGen.UserOptions.CacheUserMinimalPuts_DefaultValue();
            TemplateGen.UserOptions._CacheUserMinimalPuts_Loaded = true;
          }
          return TemplateGen.UserOptions._CacheUserMinimalPuts;
        }
        set
        {
          TemplateGen.UserOptions._CacheUserMinimalPuts = value;
          TemplateGen.UserOptions._CacheUserMinimalPuts_Loaded = true;
        }
      }

      public static bool? CacheUserQueryCache
      {
        get
        {
          if (!TemplateGen.UserOptions._CacheUserQueryCache_Loaded)
          {
            TemplateGen.UserOptions._CacheUserQueryCache = TemplateGen.UserOptions.CacheUserQueryCache_DefaultValue();
            TemplateGen.UserOptions._CacheUserQueryCache_Loaded = true;
          }
          return TemplateGen.UserOptions._CacheUserQueryCache;
        }
        set
        {
          TemplateGen.UserOptions._CacheUserQueryCache = value;
          TemplateGen.UserOptions._CacheUserQueryCache_Loaded = true;
        }
      }

      public static TopLevelAccessTypes DefaultAccess
      {
        get
        {
          if (!TemplateGen.UserOptions._DefaultAccess_Loaded)
          {
            TemplateGen.UserOptions._DefaultAccess = TemplateGen.UserOptions.DefaultAccess_DefaultValue();
            TemplateGen.UserOptions._DefaultAccess_Loaded = true;
          }
          return TemplateGen.UserOptions._DefaultAccess;
        }
        set
        {
          TemplateGen.UserOptions._DefaultAccess = value;
          TemplateGen.UserOptions._DefaultAccess_Loaded = true;
        }
      }

      public static TopLevelCascadeTypes DefaultCascade
      {
        get
        {
          if (!TemplateGen.UserOptions._DefaultCascade_Loaded)
          {
            TemplateGen.UserOptions._DefaultCascade = TemplateGen.UserOptions.DefaultCascade_DefaultValue();
            TemplateGen.UserOptions._DefaultCascade_Loaded = true;
          }
          return TemplateGen.UserOptions._DefaultCascade;
        }
        set
        {
          TemplateGen.UserOptions._DefaultCascade = value;
          TemplateGen.UserOptions._DefaultCascade_Loaded = true;
        }
      }

      public static TopLevelCollectionCascadeTypes DefaultCollectionCascade
      {
        get
        {
          if (!TemplateGen.UserOptions._DefaultCollectionCascade_Loaded)
          {
            TemplateGen.UserOptions._DefaultCollectionCascade = TemplateGen.UserOptions.DefaultCollectionCascade_DefaultValue();
            TemplateGen.UserOptions._DefaultCollectionCascade_Loaded = true;
          }
          return TemplateGen.UserOptions._DefaultCollectionCascade;
        }
        set
        {
          TemplateGen.UserOptions._DefaultCollectionCascade = value;
          TemplateGen.UserOptions._DefaultCollectionCascade_Loaded = true;
        }
      }

      public static bool DefaultCollectionLazy
      {
        get
        {
          if (!TemplateGen.UserOptions._DefaultCollectionLazy_Loaded)
          {
            TemplateGen.UserOptions._DefaultCollectionLazy = TemplateGen.UserOptions.DefaultCollectionLazy_DefaultValue();
            TemplateGen.UserOptions._DefaultCollectionLazy_Loaded = true;
          }
          return TemplateGen.UserOptions._DefaultCollectionLazy;
        }
        set
        {
          TemplateGen.UserOptions._DefaultCollectionLazy = value;
          TemplateGen.UserOptions._DefaultCollectionLazy_Loaded = true;
        }
      }

      public static bool DefaultInverse
      {
        get
        {
          if (!TemplateGen.UserOptions._DefaultInverse_Loaded)
          {
            TemplateGen.UserOptions._DefaultInverse = TemplateGen.UserOptions.DefaultInverse_DefaultValue();
            TemplateGen.UserOptions._DefaultInverse_Loaded = true;
          }
          return TemplateGen.UserOptions._DefaultInverse;
        }
        set
        {
          TemplateGen.UserOptions._DefaultInverse = value;
          TemplateGen.UserOptions._DefaultInverse_Loaded = true;
        }
      }

      public static bool DefaultLazy
      {
        get
        {
          if (!TemplateGen.UserOptions._DefaultLazy_Loaded)
          {
            TemplateGen.UserOptions._DefaultLazy = TemplateGen.UserOptions.DefaultLazy_DefaultValue();
            TemplateGen.UserOptions._DefaultLazy_Loaded = true;
          }
          return TemplateGen.UserOptions._DefaultLazy;
        }
        set
        {
          TemplateGen.UserOptions._DefaultLazy = value;
          TemplateGen.UserOptions._DefaultLazy_Loaded = true;
        }
      }

      public static bool GeneratePartialClasses
      {
        get
        {
          if (!TemplateGen.UserOptions._GeneratePartialClasses_Loaded)
          {
            TemplateGen.UserOptions._GeneratePartialClasses = TemplateGen.UserOptions.GeneratePartialClasses_DefaultValue();
            TemplateGen.UserOptions._GeneratePartialClasses_Loaded = true;
          }
          return TemplateGen.UserOptions._GeneratePartialClasses;
        }
        set
        {
          TemplateGen.UserOptions._GeneratePartialClasses = value;
          TemplateGen.UserOptions._GeneratePartialClasses_Loaded = true;
        }
      }

      public static bool? GenerateStatistics
      {
        get
        {
          if (!TemplateGen.UserOptions._GenerateStatistics_Loaded)
          {
            TemplateGen.UserOptions._GenerateStatistics = TemplateGen.UserOptions.GenerateStatistics_DefaultValue();
            TemplateGen.UserOptions._GenerateStatistics_Loaded = true;
          }
          return TemplateGen.UserOptions._GenerateStatistics;
        }
        set
        {
          TemplateGen.UserOptions._GenerateStatistics = value;
          TemplateGen.UserOptions._GenerateStatistics_Loaded = true;
        }
      }

      public static int? MaxFetchDepth
      {
        get
        {
          if (!TemplateGen.UserOptions._MaxFetchDepth_Loaded)
          {
            TemplateGen.UserOptions._MaxFetchDepth = TemplateGen.UserOptions.MaxFetchDepth_DefaultValue();
            TemplateGen.UserOptions._MaxFetchDepth_Loaded = true;
          }
          return TemplateGen.UserOptions._MaxFetchDepth;
        }
        set
        {
          TemplateGen.UserOptions._MaxFetchDepth = value;
          TemplateGen.UserOptions._MaxFetchDepth_Loaded = true;
        }
      }

      public static NHibernateVersions NHibernateVersion
      {
        get
        {
          if (!TemplateGen.UserOptions._NHibernateVersion_Loaded)
          {
            TemplateGen.UserOptions._NHibernateVersion = TemplateGen.UserOptions.NHibernateVersion_DefaultValue();
            TemplateGen.UserOptions._NHibernateVersion_Loaded = true;
          }
          return TemplateGen.UserOptions._NHibernateVersion;
        }
        set
        {
          TemplateGen.UserOptions._NHibernateVersion = value;
          TemplateGen.UserOptions._NHibernateVersion_Loaded = true;
        }
      }

      public static string ProjectGuid
      {
        get
        {
          if (!TemplateGen.UserOptions._ProjectGuid_Loaded)
          {
            TemplateGen.UserOptions._ProjectGuid = TemplateGen.UserOptions.ProjectGuid_DefaultValue();
            TemplateGen.UserOptions._ProjectGuid_Loaded = true;
          }
          return TemplateGen.UserOptions._ProjectGuid;
        }
        set
        {
          TemplateGen.UserOptions._ProjectGuid = value;
          TemplateGen.UserOptions._ProjectGuid_Loaded = true;
        }
      }

      public static string ProjectName
      {
        get
        {
          if (!TemplateGen.UserOptions._ProjectName_Loaded)
          {
            TemplateGen.UserOptions._ProjectName = TemplateGen.UserOptions.ProjectName_DefaultValue();
            TemplateGen.UserOptions._ProjectName_Loaded = true;
          }
          return TemplateGen.UserOptions._ProjectName;
        }
        set
        {
          TemplateGen.UserOptions._ProjectName = value;
          TemplateGen.UserOptions._ProjectName_Loaded = true;
        }
      }

      public static string ProjectNamespace
      {
        get
        {
          if (!TemplateGen.UserOptions._ProjectNamespace_Loaded)
          {
            TemplateGen.UserOptions._ProjectNamespace = TemplateGen.UserOptions.ProjectNamespace_DefaultValue();
            TemplateGen.UserOptions._ProjectNamespace_Loaded = true;
          }
          return TemplateGen.UserOptions._ProjectNamespace;
        }
        set
        {
          TemplateGen.UserOptions._ProjectNamespace = value;
          TemplateGen.UserOptions._ProjectNamespace_Loaded = true;
        }
      }

      public static string QuerySubstitutions
      {
        get
        {
          if (!TemplateGen.UserOptions._QuerySubstitutions_Loaded)
          {
            TemplateGen.UserOptions._QuerySubstitutions = TemplateGen.UserOptions.QuerySubstitutions_DefaultValue();
            TemplateGen.UserOptions._QuerySubstitutions_Loaded = true;
          }
          return TemplateGen.UserOptions._QuerySubstitutions;
        }
        set
        {
          TemplateGen.UserOptions._QuerySubstitutions = value;
          TemplateGen.UserOptions._QuerySubstitutions_Loaded = true;
        }
      }

      public static SourceCodeMultiLineType ReferenceTemplate
      {
        get
        {
          if (!TemplateGen.UserOptions._ReferenceTemplate_Loaded)
          {
            TemplateGen.UserOptions._ReferenceTemplate = TemplateGen.UserOptions.ReferenceTemplate_DefaultValue();
            TemplateGen.UserOptions._ReferenceTemplate_Loaded = true;
          }
          return TemplateGen.UserOptions._ReferenceTemplate;
        }
        set
        {
          TemplateGen.UserOptions._ReferenceTemplate = value;
          TemplateGen.UserOptions._ReferenceTemplate_Loaded = true;
        }
      }

      public static bool? ShowSql
      {
        get
        {
          if (!TemplateGen.UserOptions._ShowSql_Loaded)
          {
            TemplateGen.UserOptions._ShowSql = TemplateGen.UserOptions.ShowSql_DefaultValue();
            TemplateGen.UserOptions._ShowSql_Loaded = true;
          }
          return TemplateGen.UserOptions._ShowSql;
        }
        set
        {
          TemplateGen.UserOptions._ShowSql = value;
          TemplateGen.UserOptions._ShowSql_Loaded = true;
        }
      }

      public static string TransactionFactoryClass
      {
        get
        {
          if (!TemplateGen.UserOptions._TransactionFactoryClass_Loaded)
          {
            TemplateGen.UserOptions._TransactionFactoryClass = TemplateGen.UserOptions.TransactionFactoryClass_DefaultValue();
            TemplateGen.UserOptions._TransactionFactoryClass_Loaded = true;
          }
          return TemplateGen.UserOptions._TransactionFactoryClass;
        }
        set
        {
          TemplateGen.UserOptions._TransactionFactoryClass = value;
          TemplateGen.UserOptions._TransactionFactoryClass_Loaded = true;
        }
      }

      public static bool UseFluentNHibernate
      {
        get
        {
          if (!TemplateGen.UserOptions._UseFluentNHibernate_Loaded)
          {
            TemplateGen.UserOptions._UseFluentNHibernate = TemplateGen.UserOptions.UseFluentNHibernate_DefaultValue();
            TemplateGen.UserOptions._UseFluentNHibernate_Loaded = true;
          }
          return TemplateGen.UserOptions._UseFluentNHibernate;
        }
        set
        {
          TemplateGen.UserOptions._UseFluentNHibernate = value;
          TemplateGen.UserOptions._UseFluentNHibernate_Loaded = true;
        }
      }

      public static bool? UseOuterJoin
      {
        get
        {
          if (!TemplateGen.UserOptions._UseOuterJoin_Loaded)
          {
            TemplateGen.UserOptions._UseOuterJoin = TemplateGen.UserOptions.UseOuterJoin_DefaultValue();
            TemplateGen.UserOptions._UseOuterJoin_Loaded = true;
          }
          return TemplateGen.UserOptions._UseOuterJoin;
        }
        set
        {
          TemplateGen.UserOptions._UseOuterJoin = value;
          TemplateGen.UserOptions._UseOuterJoin_Loaded = true;
        }
      }

      public static bool UsePrivateSettersOnProperties
      {
        get
        {
          if (!TemplateGen.UserOptions._UsePrivateSettersOnProperties_Loaded)
          {
            TemplateGen.UserOptions._UsePrivateSettersOnProperties = TemplateGen.UserOptions.UsePrivateSettersOnProperties_DefaultValue();
            TemplateGen.UserOptions._UsePrivateSettersOnProperties_Loaded = true;
          }
          return TemplateGen.UserOptions._UsePrivateSettersOnProperties;
        }
        set
        {
          TemplateGen.UserOptions._UsePrivateSettersOnProperties = value;
          TemplateGen.UserOptions._UsePrivateSettersOnProperties_Loaded = true;
        }
      }

      public static bool? UseProxyValidator
      {
        get
        {
          if (!TemplateGen.UserOptions._UseProxyValidator_Loaded)
          {
            TemplateGen.UserOptions._UseProxyValidator = TemplateGen.UserOptions.UseProxyValidator_DefaultValue();
            TemplateGen.UserOptions._UseProxyValidator_Loaded = true;
          }
          return TemplateGen.UserOptions._UseProxyValidator;
        }
        set
        {
          TemplateGen.UserOptions._UseProxyValidator = value;
          TemplateGen.UserOptions._UseProxyValidator_Loaded = true;
        }
      }

      public static bool UseSpatial
      {
        get
        {
          if (!TemplateGen.UserOptions._UseSpatial_Loaded)
          {
            TemplateGen.UserOptions._UseSpatial = TemplateGen.UserOptions.UseSpatial_DefaultValue();
            TemplateGen.UserOptions._UseSpatial_Loaded = true;
          }
          return TemplateGen.UserOptions._UseSpatial;
        }
        set
        {
          TemplateGen.UserOptions._UseSpatial = value;
          TemplateGen.UserOptions._UseSpatial_Loaded = true;
        }
      }

      public static bool UseSubNamespaceForSchemas
      {
        get
        {
          if (!TemplateGen.UserOptions._UseSubNamespaceForSchemas_Loaded)
          {
            TemplateGen.UserOptions._UseSubNamespaceForSchemas = TemplateGen.UserOptions.UseSubNamespaceForSchemas_DefaultValue();
            TemplateGen.UserOptions._UseSubNamespaceForSchemas_Loaded = true;
          }
          return TemplateGen.UserOptions._UseSubNamespaceForSchemas;
        }
        set
        {
          TemplateGen.UserOptions._UseSubNamespaceForSchemas = value;
          TemplateGen.UserOptions._UseSubNamespaceForSchemas_Loaded = true;
        }
      }

      public static VisualStudioVersions VisualStudioVersion
      {
        get
        {
          if (!TemplateGen.UserOptions._VisualStudioVersion_Loaded)
          {
            TemplateGen.UserOptions._VisualStudioVersion = TemplateGen.UserOptions.VisualStudioVersion_DefaultValue();
            TemplateGen.UserOptions._VisualStudioVersion_Loaded = true;
          }
          return TemplateGen.UserOptions._VisualStudioVersion;
        }
        set
        {
          TemplateGen.UserOptions._VisualStudioVersion = value;
          TemplateGen.UserOptions._VisualStudioVersion_Loaded = true;
        }
      }

      public static bool AutoImport_DefaultValue()
      {
        return true;
      }

      public static bool AutoImport_DisplayToUser()
      {
        return true;
      }

      public static bool AutoImport_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static BytecodeGenerator BytecodeGenerator_DefaultValue()
      {
        return (BytecodeGenerator) 0;
      }

      public static bool BytecodeGenerator_DisplayToUser()
      {
        return true;
      }

      public static bool BytecodeGenerator_IsValid(BytecodeGenerator value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string CacheProviderClass_DefaultValue()
      {
        return (string) null;
      }

      public static bool CacheProviderClass_DisplayToUser()
      {
        return true;
      }

      public static bool CacheProviderClass_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string CacheQueryCacheFactory_DefaultValue()
      {
        return (string) null;
      }

      public static bool CacheQueryCacheFactory_DisplayToUser()
      {
        return true;
      }

      public static bool CacheQueryCacheFactory_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string CacheRegionPrefix_DefaultValue()
      {
        return (string) null;
      }

      public static bool CacheRegionPrefix_DisplayToUser()
      {
        return true;
      }

      public static bool CacheRegionPrefix_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool? CacheUserMinimalPuts_DefaultValue()
      {
        return new bool?(false);
      }

      public static bool CacheUserMinimalPuts_DisplayToUser()
      {
        return true;
      }

      public static bool CacheUserMinimalPuts_IsValid(bool? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool? CacheUserQueryCache_DefaultValue()
      {
        return new bool?();
      }

      public static bool CacheUserQueryCache_DisplayToUser()
      {
        return true;
      }

      public static bool CacheUserQueryCache_IsValid(bool? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static TopLevelAccessTypes DefaultAccess_DefaultValue()
      {
        return (TopLevelAccessTypes) 0;
      }

      public static bool DefaultAccess_DisplayToUser()
      {
        return true;
      }

      public static bool DefaultAccess_IsValid(TopLevelAccessTypes value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static TopLevelCascadeTypes DefaultCascade_DefaultValue()
      {
        return (TopLevelCascadeTypes) 0;
      }

      public static bool DefaultCascade_DisplayToUser()
      {
        return true;
      }

      public static bool DefaultCascade_IsValid(TopLevelCascadeTypes value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static TopLevelCollectionCascadeTypes DefaultCollectionCascade_DefaultValue()
      {
        return (TopLevelCollectionCascadeTypes) 0;
      }

      public static bool DefaultCollectionCascade_DisplayToUser()
      {
        return true;
      }

      public static bool DefaultCollectionCascade_IsValid(TopLevelCollectionCascadeTypes value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool DefaultCollectionLazy_DefaultValue()
      {
        return true;
      }

      public static bool DefaultCollectionLazy_DisplayToUser()
      {
        return true;
      }

      public static bool DefaultCollectionLazy_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool DefaultInverse_DefaultValue()
      {
        return true;
      }

      public static bool DefaultInverse_DisplayToUser()
      {
        return true;
      }

      public static bool DefaultInverse_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool DefaultLazy_DefaultValue()
      {
        return true;
      }

      public static bool DefaultLazy_DisplayToUser()
      {
        return true;
      }

      public static bool DefaultLazy_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool GeneratePartialClasses_DefaultValue()
      {
        return true;
      }

      public static bool GeneratePartialClasses_DisplayToUser()
      {
        return true;
      }

      public static bool GeneratePartialClasses_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool? GenerateStatistics_DefaultValue()
      {
        return new bool?();
      }

      public static bool GenerateStatistics_DisplayToUser()
      {
        return true;
      }

      public static bool GenerateStatistics_IsValid(bool? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static int? MaxFetchDepth_DefaultValue()
      {
        return new int?();
      }

      public static bool MaxFetchDepth_DisplayToUser()
      {
        return true;
      }

      public static bool MaxFetchDepth_IsValid(int? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static NHibernateVersions NHibernateVersion_DefaultValue()
      {
        return (NHibernateVersions) 0;
      }

      public static bool NHibernateVersion_DisplayToUser()
      {
        return true;
      }

      public static bool NHibernateVersion_IsValid(NHibernateVersions value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string ProjectGuid_DefaultValue()
      {
        return Guid.NewGuid().ToString();
      }

      public static bool ProjectGuid_DisplayToUser()
      {
        return true;
      }

      public static bool ProjectGuid_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string ProjectName_DefaultValue()
      {
        return "Project";
      }

      public static bool ProjectName_DisplayToUser()
      {
        return true;
      }

      public static bool ProjectName_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string ProjectNamespace_DefaultValue()
      {
        return "Project";
      }

      public static bool ProjectNamespace_DisplayToUser()
      {
        return true;
      }

      public static bool ProjectNamespace_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string QuerySubstitutions_DefaultValue()
      {
        return (string) null;
      }

      public static bool QuerySubstitutions_DisplayToUser()
      {
        return true;
      }

      public static bool QuerySubstitutions_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static SourceCodeMultiLineType ReferenceTemplate_DefaultValue()
      {
        return new SourceCodeMultiLineType("public virtual #reference.Type# #reference.Name#\n{\n\tget;\n\t#reference.SetterIsPrivate ? \"private \"#set;\n}");
      }

      public static bool ReferenceTemplate_DisplayToUser()
      {
        return true;
      }

      public static bool ReferenceTemplate_IsValid(SourceCodeMultiLineType value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool? ShowSql_DefaultValue()
      {
        return new bool?();
      }

      public static bool ShowSql_DisplayToUser()
      {
        return true;
      }

      public static bool ShowSql_IsValid(bool? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static string TransactionFactoryClass_DefaultValue()
      {
        return (string) null;
      }

      public static bool TransactionFactoryClass_DisplayToUser()
      {
        return true;
      }

      public static bool TransactionFactoryClass_IsValid(string value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool UseFluentNHibernate_DefaultValue()
      {
        return false;
      }

      public static bool UseFluentNHibernate_DisplayToUser()
      {
        return true;
      }

      public static bool UseFluentNHibernate_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool? UseOuterJoin_DefaultValue()
      {
        return new bool?(false);
      }

      public static bool UseOuterJoin_DisplayToUser()
      {
        return true;
      }

      public static bool UseOuterJoin_IsValid(bool? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool UsePrivateSettersOnProperties_DefaultValue()
      {
        return false;
      }

      public static bool UsePrivateSettersOnProperties_DisplayToUser()
      {
        return true;
      }

      public static bool UsePrivateSettersOnProperties_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool? UseProxyValidator_DefaultValue()
      {
        return new bool?();
      }

      public static bool UseProxyValidator_DisplayToUser()
      {
        return true;
      }

      public static bool UseProxyValidator_IsValid(bool? value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool UseSpatial_DefaultValue()
      {
        return false;
      }

      public static bool UseSpatial_DisplayToUser()
      {
        return true;
      }

      public static bool UseSpatial_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static bool UseSubNamespaceForSchemas_DefaultValue()
      {
        return true;
      }

      public static bool UseSubNamespaceForSchemas_DisplayToUser()
      {
        return true;
      }

      public static bool UseSubNamespaceForSchemas_IsValid(bool value, out string failReason)
      {
        failReason = "";
        return true;
      }

      public static VisualStudioVersions VisualStudioVersion_DefaultValue()
      {
        return (VisualStudioVersions) 0;
      }

      public static bool VisualStudioVersion_DisplayToUser()
      {
        return true;
      }

      public static bool VisualStudioVersion_IsValid(VisualStudioVersions value, out string failReason)
      {
        failReason = "";
        return true;
      }
    }
  }
}
