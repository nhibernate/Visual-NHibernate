using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.TemplateInfo;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.DotNet;
using Slyce.Common;
using File=ArchAngel.Interfaces.TemplateInfo.File;
using Function=ArchAngel.Providers.CodeProvider.DotNet.Function;

namespace ArchAngel.Designer.DesignerProject
{
    [Serializable]
    public class ParamInfo
    {
        public string Name;
        public Type DataType;
        public string Modifiers = "";

        public ParamInfo(string name, Type dataType)
        {
            Name = name;
            DataType = dataType;
        }

        public ParamInfo(string name, Type dataType, string modifiers) : this(name, dataType)
        {
            Modifiers = modifiers;
        }
    }

	public class OverriddenFunctionInformation
	{
		public Type Type { get; private set; }
		public string MethodName { get; private set; }
		public string XmlComments { get; private set; }
		public string BodyText { get; private set; }
		public IEnumerable<Type> Parameters { get; private set; }

		public OverriddenFunctionInformation(Type type, string methodName, IEnumerable<Type> parameters, string xmlComments, string bodyText)
		{
			Type = type;
			MethodName = methodName;
			Parameters = parameters;
			XmlComments = xmlComments;
			BodyText = bodyText;
		}
	}

	[Serializable]
	public class IncludedFile
	{
		public string FullFilePath { get; set; }
		public string DisplayName { get; set; }

		public IncludedFile(string fullFilePath, string displayName)
		{
			FullFilePath = fullFilePath;
			DisplayName = displayName;
		}

		public IncludedFile(string fullFilePath)
		{
			FullFilePath = fullFilePath;
			DisplayName = Path.GetFileName(fullFilePath);
		}
	}

    [Serializable]
    public class OutputFolder
    {
        private string _Id;
        public string Name;
        public Type IteratorType;
        public readonly List<OutputFile> Files;
        public readonly List<OutputFolder> Folders;

        public OutputFolder(string name) : this(name, Guid.NewGuid().ToString())
        {
        }

        public OutputFolder(string name, string id)
        {
            _Id = id.Replace("-", "");
            Name = name;
            Files = new List<OutputFile>();
            Folders = new List<OutputFolder>();
        }

        public string Id
        {
            get
            {
                if (String.IsNullOrEmpty(_Id))
                {
                    _Id = Guid.NewGuid().ToString().Replace("-", "");
                }
                return _Id;
            }
        }

        public void AddFile(OutputFile file)
        {
            foreach (OutputFile exitingFile in Files)
            {
                if (exitingFile.Id == file.Id)
                {
                    return;
                }
            }
            Files.Add(file);
            //Files.Sort();
        }

        public void RemoveFile(OutputFile file)
        {
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].Id == file.Id)
                {
                    Files.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveFolder(OutputFolder folder)
        {
            for (int i = 0; i < Folders.Count; i++)
            {
                if (Folders[i].Id == folder.Id)
                {
                    Folders.RemoveAt(i);
                    break;
                }
            }
        }

    	public IOutput ToCombinedOutput()
    	{
    		Output output = new Output();
    		output.Name = Name;
    	    output.RootFolder = ToIFolder();
    		return output;
    	}

    	public IFolder ToIFolder()
    	{
    		Folder folder = new Folder();
    		folder.Name = Name;
    		folder.Id = Id;

    		var files =		from f in Files
    						where f.FileType == OutputFileTypes.File
							select f.ToIFile();
			var scripts =	from f in Files
    						where f.FileType == OutputFileTypes.Script
							select f.ToIScript();
    		folder.Files = files.ToArray();
			folder.Scripts = scripts.ToArray();
    		folder.SubFolders = Folders.Select(f => f.ToIFolder()).ToArray();

    		return folder;
    	}
    }

    [Serializable]
    public class OutputFile
    {
        public OutputFileTypes FileType;
        public string Name { get; set; }
        public string StaticFileName { get; set; }
		public string StaticFileSkipFunctionName { get; set; }
    	public Type StaticFileIterator { get; set; }
        public string ScriptName = "";
        public FunctionInfo IteratorFunction { get; set; }
        private string _Id;

        /// <summary>
        /// Creates an Output file.
        /// </summary>
        /// <param name="name">The display name of the file.</param>
        /// <param name="type">The type of file</param>
        /// <param name="filename">If type is File, this is the StaticFileName. Otherwise this is the ScriptName.</param>
        /// <param name="id"></param>
        public OutputFile(string name, OutputFileTypes type, string filename, string id)
        {
        	StaticFileName = "";
            _Id = id.Replace("-", "");
            FileType = type;
            Name = name;
            if (type == OutputFileTypes.File)
                StaticFileName = filename;
            else
                ScriptName = filename;
        }

        public string Id
        {
            get
            {
                if (String.IsNullOrEmpty(_Id))
                {
                    _Id = Guid.NewGuid().ToString().Replace("-", "");
                }
                return _Id;
            }
			set
			{
				_Id = value;
			}
        }

        public string IteratorTypes
        {
            get
            {
                string iterators = "";

                if (IteratorFunction != null)
                {
                    for (int i = 0; i < IteratorFunction.Parameters.Count; i++)
                    {
                        if (i > 0)
                        {
                            iterators += ", ";
                        }
                        iterators += Utility.GetDemangledGenericTypeName(IteratorFunction.Parameters[i].DataType);
                    }
                }
                return iterators;
            }
        }

    	public IFile ToIFile()
    	{
    		File file = new File
    		            	{
    		            		Id = Id,
    		            		StaticFileSkipFunction = StaticFileSkipFunctionName,
    		            		IteratorName = StaticFileIterator == null
    		            		               	? ""
    		            		               	: Utility.GetDemangledGenericTypeName(StaticFileIterator.FullName),
    		            		Name = Name,
    		            		StaticFileName = StaticFileName
    		            	};

    		return file;
    	}

    	public IScript ToIScript()
    	{
    		Script script = new Script();
    		script.Id = Id;
    		script.FileName = Name;
    		script.ScriptName = ScriptName;
    		script.IteratorName = IteratorTypes;
    		return script;
    	}
    }

    [Serializable]
    public class DefaultValueFunction
    {
        [DotfuscatorDoNotRename]
        public enum FunctionTypes
        {
            //[DotfuscatorDoNotRename]
            //DefaultValue,
            [DotfuscatorDoNotRename]
            HelperOverride,
            //[DotfuscatorDoNotRename]
            //Validate,
            //[DotfuscatorDoNotRename]
            //DisplayToUser
        }
        public Type ObjectType;
        public string PropertyName;
        private bool _UseCustomCode;
        public FunctionTypes FunctionType;
        public List<ParamInfo> ParameterTypes = new List<ParamInfo>();
        public bool IsForUserOption;

        public DefaultValueFunction(Type objectType, string propertyName, bool useCustomCode, FunctionTypes functionType, bool isForUserOption)
        {
            ObjectType = objectType;
            PropertyName = propertyName;
            UseCustomCode = useCustomCode;
            FunctionType = functionType;
            IsForUserOption = isForUserOption;
        }

        public bool UseCustomCode
        {
            get
            {
                if (FunctionType == FunctionTypes.HelperOverride)
                {
                    return _UseCustomCode;
                }
                // All other types can ONLY use Custom Code!
                return true;
            }
            set { _UseCustomCode = value; }
        }

        public string FunctionName
        {
            get
            {
                if (FunctionType != FunctionTypes.HelperOverride)
                {
                    return PropertyName;
                }
                string functionName = ObjectType.FullName.Replace("+", ".") + "." + PropertyName;
                functionName = functionName.Replace(".", "_");

                switch (FunctionType)
                {
                        //case FunctionTypes.DefaultValue:
                        //    functionName += "Default";
                        //    break;
                        //case FunctionTypes.Validate:
                        //    functionName += "Validate";
                        //    break;
                        //case FunctionTypes.DisplayToUser:
                        //    functionName += "DisplayToUser";
                        //    break;
                    case FunctionTypes.HelperOverride:
                        throw new NotImplementedException("HelperOverride has not been implemented yet.");
                    default:
                        throw new NotImplementedException("This type of function has not been catered for yet.");
                }
                //return functionName;
            }
        }

        public string GetFormattedDefaultCode()
        {
            bool isMethod = false;
            MethodInfo method = null;

            bool hasApiExt = false;
            string defaultCode = "";

            foreach (MethodInfo meth in ObjectType.GetMethods())
            {
                if (meth.Name == PropertyName)
                {
                    ParameterInfo[] realParams = meth.GetParameters();

                    if (realParams.Length == ParameterTypes.Count)
                    {
                        bool paramsMatch = true;

                        for (int paramCounter = 0; paramCounter < realParams.Length; paramCounter++)
                        {
                            if (realParams[paramCounter].ParameterType.FullName != ParameterTypes[paramCounter].DataType.FullName)
                            {
                                paramsMatch = false;
                                break;
                            }
                        }
                        if (paramsMatch)
                        {
                            object[] allAttributes = meth.GetCustomAttributes(false);

                            foreach (object att in allAttributes)
                            {
                                Type attType = att.GetType();

                                if (Utility.StringsAreEqual(attType.Name, "ApiExtensionAttribute", true))
                                {
                                    hasApiExt = true;
                                    method = meth;
                                    defaultCode = (string)attType.InvokeMember("DefaultCode", BindingFlags.GetProperty, null, att, null);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            Type returnType = null;

            if (method != null)
            {
                isMethod = true;
                returnType = method.ReturnType;
            }
            else
            {
                PropertyInfo property = ObjectType.GetProperty(PropertyName);
                object[] allAttributes = property.GetCustomAttributes(false);

                foreach (object att in allAttributes)
                {
                    Type attType = att.GetType();

                    if (Utility.StringsAreEqual(attType.Name, "ApiExtensionAttribute", true))
                    {
                        hasApiExt = true;
                        defaultCode = (string)attType.InvokeMember("DefaultCode", BindingFlags.GetProperty, null, att, null);
                        break;
                    }
                }
                returnType = property.PropertyType;
            }
            if (!hasApiExt)// attributes != null && attributes.Length == 0)
            {
                throw new NotImplementedException(String.Format("DefaultCodeAttribute not implemented for {0}.{1} yet.", ObjectType.FullName, PropertyName));
            }
            string code = defaultCode;// attributes[0].DefaultCode;
            CSharpParser csf = new CSharpParser();

            if (isMethod)
            {
                Function f = (Function)csf.ParseSingleConstruct("public void Method1(){ " + code + " }", BaseConstructType.MethodDeclaration);
                code = f.BodyText;
            }
            else
            {
                Property p = (Property)csf.ParseSingleConstruct("public int Property1 { " + code + " }", BaseConstructType.PropertyDeclaration);
                if (p.GetAccessor != null)
                    code = p.GetAccessor.BodyText;
                else
                    code = p.SetAccessor.BodyText;
            }
            int secondLineBreak = code.IndexOf("\r\n", code.IndexOf("{")) + 2;
            int lastLineBreak = code.LastIndexOf("\r\n", code.LastIndexOf("}"));
            if (lastLineBreak > secondLineBreak)
            {
                code = code.Substring(secondLineBreak, lastLineBreak - secondLineBreak);
            }
            else
            {
                code = "";
            }
            string[] lines = Utility.StandardizeLineBreaks(code, Utility.LineBreaks.Unix).Split('\n');

            StringBuilder sb = new StringBuilder(lines.Length * 100);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 1)
                {
                    sb.AppendLine(lines[i].Substring(1));
                }
                else
                {
                    sb.AppendLine(lines[i]);
                }
            }
            return sb.ToString();
        }
    }

    [Serializable]
    public class ReferencedFile
    {
        public string FileName = "";
    	public string AssemblyName { get { return Path.GetFileNameWithoutExtension(FileName); } }
        public bool MergeWithAssembly;
        public bool UseInWorkbench;
        private bool? _IsProvider;

        public ReferencedFile(string fileName, bool mergeWithAssembly, bool useInWorkbench)
        {
            FileName = fileName;
            MergeWithAssembly = mergeWithAssembly;
            UseInWorkbench = useInWorkbench;
        }

        public bool Equals(ReferencedFile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FileName, FileName) && other.MergeWithAssembly.Equals(MergeWithAssembly) && other.UseInWorkbench.Equals(UseInWorkbench);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ReferencedFile)) return false;
            return Equals((ReferencedFile) obj);
        }

        public bool IsProvider
        {
            get
            {
                if(_IsProvider.HasValue == false)
                {
                    if (UseInWorkbench)
                    {
                        _IsProvider = true;
                    }
                    else
                    {
                        try
                        {
                            Assembly assembly = Assembly.Load(AssemblyName);
                            IsProvider = ProviderInfo.IsProvider(assembly);
                        }
                        catch(Exception)
                        {
                            IsProvider = false;
                        }
                    }
                }

                return _IsProvider.Value;
            }
            set { _IsProvider = value; }
        }
    }
}