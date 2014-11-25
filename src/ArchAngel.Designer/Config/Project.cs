using System;
using System.Xml;
using System.Reflection;
using System.Collections;
//using CodeSpew.Model.ITemplateConfig;
using Slyce.ITemplate;

namespace Slyce.ITemplate
{
	/// <summary>
	/// Summary description for Project.
	/// </summary>
	[Serializable]
	public class Project : Slyce.ITemplate.IProject
	{
		public IOption[] m_options;
		public IOutput[] m_outputs;
		public string m_name;
		public string m_description;
        ArrayList TempOutputs;

		public IOption[] Options
		{
			get {return m_options;}
			set {m_options = value;}
		}

		public IOutput[] Outputs
		{
			get {return m_outputs;}
			set {m_outputs = value;}
		}

		public string Name
		{
			get {return m_name;}
			set {m_name = value;}
		}

		public string Description
		{
			get {return m_description;}
			set {m_description = value;}
		}

		public Project()
		{
			Init();
		}

		private void Init()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("options.xml"));
			//doc.Load(@"C:\xxx.xml");
			this.Name			= doc.SelectSingleNode("ROOT/config/project/name").InnerText;
			this.Description	= doc.SelectSingleNode("ROOT/config/project/description").InnerText;
			XmlNodeList nodes	= doc.SelectNodes("ROOT/config/project/options/option");
			this.Options		= new Option[nodes.Count];

			for (int i = 0; i < nodes.Count; i++)
			{
				XmlNode subNode			= nodes[i];
				this.Options[i]			= new Option();
				Options[i].Description	= subNode.SelectSingleNode("description").InnerText;
				Options[i].Text			= subNode.SelectSingleNode("text").InnerText;
				Options[i].VariableName = subNode.SelectSingleNode("variablename").InnerText;
				Options[i].VarType		= subNode.SelectSingleNode("type").InnerText;
				Options[i].Category		= subNode.SelectSingleNode("category").InnerText;
				Options[i].DefaultValue	= subNode.SelectSingleNode("defaultvalue") != null ? subNode.SelectSingleNode("defaultvalue").InnerText : "";
				Options[i].IteratorName	= subNode.SelectSingleNode("iteratorname") != null ? subNode.SelectSingleNode("iteratorname").InnerText : "";

				XmlNodeList valueNodes	= subNode.SelectNodes("values/value");
				Options[i].Values		= new string[valueNodes.Count];

				for (int x = 0; x < valueNodes.Count; x++)
				{
					Options[i].Values[x] = valueNodes[x].InnerText;
				}
            }

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
           
            ///////////////////////////
            //nodes			= doc.SelectNodes("ROOT/project/outputs/output");
            //// Skip the output that stores orphan files
            //this.Outputs	= new Output[nodes.Count - 1];
            //int nextIndex = 0;

            //for (int i = 0; i < nodes.Count; i++)
            //{
            //    XmlNode subNode	= nodes[i];

            //    // Skip the output that stores orphan files
            //    if (subNode.Attributes["name"].Value != "orphan")
            //    {
            //        this.Outputs[nextIndex]	= new Output();
            //        Outputs[nextIndex].Name	= subNode.Attributes["name"].Value;

            //        XmlNode rootFolder			= subNode.SelectSingleNode("rootfolder");
            //        Outputs[nextIndex].RootFolder		= new Folder();
            //        Outputs[nextIndex].RootFolder.Name	= "Root";

            //        ProcessFolderNode(rootFolder, Outputs[nextIndex].RootFolder);
            //        nextIndex++;
            //    }
            //}
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
            XmlNodeList subFolderNodes	= folderNode.SelectNodes("folder");
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
	}
}