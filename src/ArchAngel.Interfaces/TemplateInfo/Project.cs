using System;
using System.Xml;
using System.Reflection;
using System.Collections;
using ArchAngel.Interfaces.ITemplate;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.TemplateInfo
{
    /// <summary>
    /// Summary description for Project.
    /// </summary>
    [Serializable]
    public class Project : ArchAngel.Interfaces.ITemplate.IProject
    {
        private IOption[] m_options;
        private IOutput[] m_outputs;
        private List<IDefaultValueFunction> m_defaultValueFunctions = new List<IDefaultValueFunction>();
        private string m_name;
        private string m_description;
        private ArrayList TempOutputs;
        private Function[] Functions;
        private List<System.Reflection.Assembly> m_referencedAssemblies = new List<System.Reflection.Assembly>();
        private string[] ReferencedAssemblyPaths;
        private string[] AssemblySearchPaths;
        private List<ArchAngel.Interfaces.BaseAction> m_actions = new List<ArchAngel.Interfaces.BaseAction>();

        public IOption[] Options
        {
            get { return m_options; }
            set { m_options = value; }
        }

        public IOutput[] Outputs
        {
            get { return m_outputs; }
            set { m_outputs = value; }
        }

        public List<ArchAngel.Interfaces.BaseAction> Actions
        {
            get { return m_actions; }
            set { m_actions = value; }
        }

        public List<IDefaultValueFunction> DefaultValueFunctions
        {
            get { return m_defaultValueFunctions; }
            set { m_defaultValueFunctions = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public Project()
        {
            //Init();
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        public IOption FindOption(string name)
        {
            foreach (IOption option in this.Options)
            {
                if (option.VariableName == name)
                {
                    return option;
                }
            }
            return null;
        }

        public List<ArchAngel.Interfaces.BaseAction> PreActions
        {
            get
            {
                List<ArchAngel.Interfaces.BaseAction> preActions = new List<ArchAngel.Interfaces.BaseAction>();
                Type generationType = typeof(ArchAngel.Actions.GenerationStepAction);

                for (int i = 0; i < Actions.Count; i++)
                {
                    ArchAngel.Interfaces.BaseAction action = Actions[i];

                    if (action.GetType() == generationType)
                    {
                        break;
                    }
                    preActions.Add(action);
                }
                return preActions;
            }
        }

        public List<ArchAngel.Interfaces.BaseAction> PostActions
        {
            get
            {
                List<ArchAngel.Interfaces.BaseAction> postActions = new List<ArchAngel.Interfaces.BaseAction>();
                Type generationType = typeof(ArchAngel.Actions.GenerationStepAction);

                bool inPreActions = true;

                for (int i = 0; i < Actions.Count; i++)
                {
                    ArchAngel.Interfaces.BaseAction action = Actions[i];

                    if (action.GetType() == generationType)
                    {
                        inPreActions = false;
                        continue;
                    }
                    if (!inPreActions)
                    {
                        postActions.Add(action);
                    }
                }
                return postActions;
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string filenameWithoutExt = args.Name.Substring(0, args.Name.IndexOf(','));

            foreach (string resolvePath in AssemblySearchPaths)
            {
                // Check for AAL files
                string file = args.Name.Substring(0, args.Name.IndexOf(',')) + ".aal";
                string filename = System.IO.Path.Combine(resolvePath, filenameWithoutExt + ".aal");

                if (System.IO.File.Exists(filename))
                {
                    return Assembly.LoadFrom(filename);
                }
                // Check for DLL files
                filename = System.IO.Path.Combine(resolvePath, filenameWithoutExt + ".dll");

                if (System.IO.File.Exists(filename))
                {
                    return Assembly.LoadFrom(filename);
                }
            }
            throw new System.IO.FileNotFoundException("(ITemplate.CurrentDomain_AssemblyResolve) Assembly could not be found: " + filenameWithoutExt);
        }


        public void PopulateFunctions(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("ROOT/function");
            Functions = new Function[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                Functions[i] = new Function();
                Functions[i].Name = nodes[i].Attributes["name"].InnerText;
                Functions[i].ParameterTypeName = nodes[i].Attributes["parametertypename"].InnerText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="assemblySearchFolders">Folders to search for referenced assemblies.</param>
        public void Init(XmlDocument doc, string[] assemblySearchFolders)
        {
            #region Referenced Assemblies
            AssemblySearchPaths = assemblySearchFolders;
            bool asssemblyFound = false;
            ReferencedAssemblyPaths = doc.SelectSingleNode("ROOT/referencedfiles").InnerText.Split(',');

            for (int i = 0; i < ReferencedAssemblyPaths.Length; i++)
            {
                if (string.IsNullOrEmpty(ReferencedAssemblyPaths[i]))
                {
                    continue;
                }
                ReferencedAssemblyPaths[i] = ReferencedAssemblyPaths[i].Split('|')[0];
                ReferencedAssemblyPaths[i] = ReferencedAssemblyPaths[i].Substring(ReferencedAssemblyPaths[i].LastIndexOf(@"\") + 1);
                asssemblyFound = false;
                string pathsSearched = "";

                foreach (string searchPath in assemblySearchFolders)
                {
                    pathsSearched += pathsSearched.Length > 0 ? ", " + searchPath : searchPath;
                    string filePath = System.IO.Path.Combine(searchPath, ReferencedAssemblyPaths[i]);

                    if (System.IO.File.Exists(filePath))
                    {
                        asssemblyFound = true;
                        ReferencedAssemblyPaths[i] = filePath;
                        break;
                    }
                }
                if (!asssemblyFound)
                {
                    throw new System.IO.FileNotFoundException(string.Format("Referenced assembly not found: {0}\n\nPaths searched: {1}", ReferencedAssemblyPaths[i], pathsSearched));
                }
            }
            #endregion

            this.Name = doc.SelectSingleNode("ROOT/config/project/name").InnerText;
            this.Description = doc.SelectSingleNode("ROOT/config/project/description").InnerText;

            #region User Options
            XmlNodeList nodes = doc.SelectNodes("ROOT/config/project/options/option");
            this.Options = new Option[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode subNode = nodes[i];
                this.Options[i] = new Option();
                Options[i].Description = subNode.SelectSingleNode("description").InnerText;
                Options[i].Text = subNode.SelectSingleNode("text").InnerText;
                Options[i].VariableName = subNode.SelectSingleNode("variablename").InnerText;
                Options[i].VarType = GetTypeFromReferencedAssemblies(subNode.SelectSingleNode("type").InnerText, true);
                Options[i].Category = subNode.SelectSingleNode("category").InnerText;
                Options[i].DefaultValue = subNode.SelectSingleNode("defaultvalue") != null ? subNode.SelectSingleNode("defaultvalue").InnerText : "";
                Options[i].DefaultValueIsFunction = subNode.SelectSingleNode("defaultvalueisfunction") != null ? bool.Parse(subNode.SelectSingleNode("defaultvalueisfunction").InnerText) : false;
                Options[i].IteratorName = subNode.SelectSingleNode("iteratorname") != null ? subNode.SelectSingleNode("iteratorname").InnerText : "";
                Options[i].DisplayToUser = subNode.SelectSingleNode("displaytouser").InnerText != null ? subNode.SelectSingleNode("displaytouser").InnerText : "true";
                Options[i].DisplayToUserIsFunction = subNode.SelectSingleNode("displaytouserisfunction").InnerText != null ? bool.Parse(subNode.SelectSingleNode("displaytouserisfunction").InnerText) : false;
                Options[i].ValidatorFunction = subNode.SelectSingleNode("validatorfunction") != null ? subNode.SelectSingleNode("validatorfunction").InnerText : "";

                XmlNodeList valueNodes = subNode.SelectNodes("values/value");
                Options[i].Values = new string[valueNodes.Count];

                for (int x = 0; x < valueNodes.Count; x++)
                {
                    Options[i].Values[x] = valueNodes[x].InnerText;
                }
            }
            #endregion

            #region Default Value Functions
            nodes = doc.SelectNodes("ROOT/config/project/defaultvaluefunctions/defaultvaluefunction");
            this.DefaultValueFunctions = new List<IDefaultValueFunction>();

            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode subNode = nodes[i];
                ArchAngel.Providers.Database.Model.DefaultValueFunction defaultValueFunction = new ArchAngel.Providers.Database.Model.DefaultValueFunction();
                defaultValueFunction.ObjectType = GetTypeFromReferencedAssemblies(subNode.SelectSingleNode("objecttype").InnerText, true);
                defaultValueFunction.PropertyName = subNode.SelectSingleNode("propertyname").InnerText;
                defaultValueFunction.UseCustomCode = bool.Parse(subNode.SelectSingleNode("usecustomcode").InnerText);
                defaultValueFunction.FunctionType = (FunctionTypes) Enum.Parse(typeof(FunctionTypes), subNode.SelectSingleNode("functiontype").InnerText);
                DefaultValueFunctions.Add(defaultValueFunction);
            }
            #endregion

            #region Outputs
            TempOutputs = new ArrayList();
            XmlNode rootFolderNode = doc.SelectSingleNode("ROOT/config/project/rootoutput/rootfolder");

            // Get list of discrete output names
            XmlNodeList outputNamesOfFolders = doc.SelectNodes("ROOT/config/project/rootoutput//folder");
            XmlNodeList outputNamesOfScriptFiles = doc.SelectNodes("ROOT/config/project/rootoutput//script");
            XmlNodeList outputNamesOfFiles = doc.SelectNodes("ROOT/config/project/rootoutput//file");
            string[] outputNames = GetOutputNames(doc);
            this.Outputs = new Output[outputNames.Length];

            for (int outputCounter = 0; outputCounter < outputNames.Length; outputCounter++)
            {
                this.Outputs[outputCounter] = new Output();
                Outputs[outputCounter].RootFolder = new Folder();
                Outputs[outputCounter].RootFolder.Name = "Root";
                Outputs[outputCounter].Name = outputNames[outputCounter];
                ProcessFolderNode(rootFolderNode, Outputs[outputCounter].RootFolder, outputNames[outputCounter]);
            }
            #endregion

            #region Actions
            System.Reflection.Assembly actionAssembly = System.Reflection.Assembly.GetAssembly(typeof(ArchAngel.Actions.ActionSet));
            XmlNodeList actionNodes = doc.SelectNodes("ROOT/config/project/actions/action");

            foreach (XmlNode actionNode in actionNodes)
            {
                string actionTypeName = actionNode.SelectSingleNode("@typename").Value;
                ArchAngel.Actions.BaseAction action = (ArchAngel.Actions.BaseAction)actionAssembly.CreateInstance(actionTypeName);
                action.ReadFromXml(actionNode.InnerXml);
                this.Actions.Add(action);
            }
            #endregion
        }

        private string[] GetOutputNames(XmlDocument doc)
        {
            // Get list of discrete output names
            XmlNodeList outputNamesOfFolders = doc.SelectNodes("ROOT/config/project/rootoutput//folder");
            XmlNodeList outputNamesOfScriptFiles = doc.SelectNodes("ROOT/config/project/rootoutput//script");
            XmlNodeList outputNamesOfFiles = doc.SelectNodes("ROOT/config/project/rootoutput//file");

            ArrayList outputNames = new ArrayList();
            GetOutputNamesFromAttributes(outputNamesOfFolders, ref outputNames);
            GetOutputNamesFromAttributes(outputNamesOfScriptFiles, ref outputNames);
            GetOutputNamesFromAttributes(outputNamesOfFiles, ref outputNames);
            return (string[])outputNames.ToArray(typeof(string));
        }

        private void GetOutputNamesFromAttributes(XmlNodeList nodes, ref ArrayList nameArray)
        {
            foreach (XmlNode nameNode in nodes)
            {
                string[] names = nameNode.Attributes["outputs"].Value.Split(',');

                foreach (string n in names)
                {
                    if (nameArray.BinarySearch(n) < 0)
                    {
                        nameArray.Add(n);
                        nameArray.Sort();
                    }
                }
            }
        }

        private void ProcessFolderNode(XmlNode folderNode, IFolder parentFolder, string outputName)
        {
            #region Process sub-folders
            XmlNodeList subFolderNodes = folderNode.SelectNodes("folder");
            ArrayList actualSubFolders = new ArrayList();

            for (int i = 0; i < subFolderNodes.Count; i++)
            {
                XmlNode subFolderNode = subFolderNodes[i];

                // Only process if the correct output
                if (HasDesiredOutput(subFolderNode, outputName))
                {
                    Folder tempFolder = new Folder();
                    tempFolder.Name = subFolderNode.Attributes["name"].Value;
                    ProcessFolderNode(subFolderNode, tempFolder, outputName);
                    actualSubFolders.Add(tempFolder);
                }
            }
            Folder[] subFolders = (Folder[])actualSubFolders.ToArray(typeof(Folder));
            #endregion

            #region Process scripts
            XmlNodeList scriptNodes = folderNode.SelectNodes("script");
            ArrayList actualScripts = new ArrayList();

            for (int i = 0; i < scriptNodes.Count; i++)
            {
                XmlNode scriptNode = scriptNodes[i];

                // Only process if the correct output
                if (HasDesiredOutput(scriptNode, outputName))
                {
                    Script tempScript = new Script();
                    tempScript.FileName = scriptNode.Attributes["filename"].Value;
                    tempScript.ScriptName = scriptNode.Attributes["scriptname"].Value;
                    tempScript.IteratorName = scriptNode.Attributes["iteratorname"].Value;
                    actualScripts.Add(tempScript);
                }
            }
            Script[] scripts = (Script[])actualScripts.ToArray(typeof(Script));
            #endregion

            #region Process files
            XmlNodeList fileNodes = folderNode.SelectNodes("file");
            ArrayList actualFiles = new ArrayList();

            for (int i = 0; i < fileNodes.Count; i++)
            {
                XmlNode fileNode = fileNodes[i];

                // Only process if the correct output
                if (HasDesiredOutput(fileNode, outputName))
                {
                    File tempFile = new File();
                    tempFile.Name = fileNode.Attributes["name"].Value;
                    actualFiles.Add(tempFile);
                }
            }
            File[] files = (File[])actualFiles.ToArray(typeof(File));
            #endregion

            parentFolder.Files = files;
            parentFolder.SubFolders = subFolders;
            parentFolder.Scripts = scripts;
        }

        private bool HasDesiredOutput(XmlNode node, string outputName)
        {
            string[] outputs = node.Attributes["outputs"].Value.Split(',');

            for (int x = 0; x < outputs.Length; x++)
            {
                // Only process if the correct output
                if (outputs[x] == outputName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets an array of assemblies that are referenced by this project
        /// </summary>
        public List<System.Reflection.Assembly> ReferencedAssemblies
        {
            get
            {
                if (m_referencedAssemblies == null || m_referencedAssemblies.Count == 0)
                {
                    foreach (string referencedAssembly in ReferencedAssemblyPaths)
                    {
                        System.Windows.Forms.MessageBox.Show("TemplateInfo.Project is attempting to load: " + referencedAssembly);
                        m_referencedAssemblies.Add(System.Reflection.Assembly.LoadFrom(referencedAssembly));
                    }
                }
                return m_referencedAssemblies;
            }
        }

        /// <summary>
        /// Searches the running assembly as well as all referenced assemblies for the given type.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public Type GetTypeFromReferencedAssemblies(string typeName, bool throwOnError)
        {
            // TODO: This switch statement is only required until all templates have been compiled with the new strongly-typed system
            switch (typeName.ToLower())
            {
                case "string":
                    return typeof(string);
                    break;
                case "bool":
                case "boolean":
                    return typeof(bool);
                    break;
                case "int":
                case "int32":
                    return typeof(int);
                    break;
                case "double":
                    return typeof(double);
                    break;
                case "table":
                    typeName = "ArchAngel.Providers.Database.Model.Table";
                    break;
                case "view":
                    typeName = "ArchAngel.Providers.Database.Model.View";
                    break;
                case "storedprocedure":
                    typeName = "ArchAngel.Providers.Database.Model.View";
                    break;
                case "scriptobject":
                    typeName = "ArchAngel.Providers.Database.Model.ScriptObject";
                    break;
                case "database":
                    typeName = "ArchAngel.Providers.Database.Model.Database";
                    break;
                case "column":
                    typeName = "ArchAngel.Providers.Database.Model.Column";
                    break;
                case "mapcolumn":
                    typeName = "ArchAngel.Providers.Database.Model.MapColumn";
                    break;
                case "database[]":
                    typeName = "ArchAngel.Providers.Database.Model.Database[]";
                    break;
                case "column[]":
                    typeName = "ArchAngel.Providers.Database.Model.Column[]";
                    break;
                case "enumeration":
                    return typeof(Enum);
                    break;
                //case "color":
                 //   return typeof(System.Drawing.Color);
                   // break;
            }
            Type type = Type.GetType(typeName);

            if (type != null)
            {
                return type;
            }
            foreach (System.Reflection.Assembly assembly in ReferencedAssemblies)
            {
                type = assembly.GetType(typeName, false);

                if (type != null)
                {
                    break;
                }
            }
            if (type == null && throwOnError)
            {
                throw new Exception("Type not found: " + typeName);
            }
            return type;
        }


    }
}