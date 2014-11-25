using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Slyce.Loader;
using DevExpress.XtraTreeList.Nodes;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.BLL;

namespace ArchAngel.Workbench.ContentItems
{
    public partial class EditModel : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {

        #region Delegate Definitions

        // delegates used to call MainForm functions from worker thread
        public delegate void Generation_DelegateSetProperty(object obj, string propertyName, object val);
        public delegate object Generation_DelegateCallMethod(object obj, string methodName, object[] parameters);

        #endregion

        #region Delegate Instances

        // Delegate instances used to call user interface functions 
        // from worker thread:
        private Generation_DelegateSetProperty _delegateSetProperty;
        private Generation_DelegateCallMethod _delegateCallMethod;

        #endregion

        #region Enums
        enum Images
        {
            Unchecked = 0,
            Checked = 1,
            PartiallyChecked = 2,
            Database = 3,
            Table = 4,
            Column = 5,
            Key = 6,
            Index = 7,
            Filter = 8,
            Option = 9,
            Relationship = 10,
            StoredProcedure = 11,
            GreenBullet = 12,
            OrangeBullet = 13,
            RedBullet = 14,
            Red_Database = 15,
            Red_Table = 16,
            Red_Column = 17,
            Red_Key = 18,
            Red_Index = 19,
            Red_Filter = 20,
            Red_Option = 21,
            Red_Relationship = 22,
            Red_StoredProcedure = 23
        }
        enum BackGroundWorkerSteps
        {
            Step1,
            Step2,
            Step3
        }
        #endregion

       private bool _disableCheckTreeListNode;
        private Thread _validationThread;
        private Slyce.TemplateInfo.Project _project = null;
        private List<Slyce.ITemplate.IOption> _optionsForTable = null;
        private List<Slyce.ITemplate.IOption> _optionsForViews = null;
        private List<Slyce.ITemplate.IOption> _optionsForStoredProcedures = null;
        private List<Slyce.ITemplate.IOption> _optionsForScriptObjects = null;
        private List<Slyce.ITemplate.IOption> _optionsForColumns = null;
        private List<Slyce.ITemplate.IOption> _optionsForOneToOneRelationships = null;
        private List<Slyce.ITemplate.IOption> _optionsForOneToManyRelationship = null;
        private List<Slyce.ITemplate.IOption> _optionsForManyToOneRelationship = null;
        private List<Slyce.ITemplate.IOption> _optionsForManyToManyRelationship = null;
        private List<Slyce.ITemplate.IOption> _optionsForFilters = null;
        private List<Slyce.ITemplate.IOption> _optionsForIndexes = null;
        private List<Slyce.ITemplate.IOption> _optionsForKeys = null;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private bool MustSetDefaultValues = false;
        private BackGroundWorkerSteps CurrentBackgroundWorkerStep;
        Type ScriptBaseType = typeof(ArchAngel.Providers.Database.Model.ScriptBase);
        Type ScriptObjectType = typeof(ArchAngel.Providers.Database.Model.ScriptObject);
        Type RelationshipType = typeof(ArchAngel.Providers.Database.Model.Relationship);
        private const int RedBulletValue = (int)Images.RedBullet;
        private const int GreenBulletValue = (int)Images.GreenBullet;
        private int UncheckedImageValue = (int)Images.Unchecked;

        private int RedColumnValue = (int)Images.Red_Column;
        private int RedDatabaseValue = (int)Images.Red_Database;
        private int RedFilterValue = (int)Images.Red_Filter;
        private int RedIndexValue = (int)Images.Red_Index;
        private int RedKeyValue = (int)Images.Red_Key;
        private int RedOptionValue = (int)Images.Red_Option;
        private int RedRelationshipValue = (int)Images.Red_Relationship;
        private int RedStoredProcedureValue = (int)Images.Red_StoredProcedure;
        private int RedTableValue = (int)Images.Red_Table;

        Type DatabaseType = typeof(ArchAngel.Providers.Database.Model.Database);

        public EditModel()
        {
            Populate();
            Project.OnTemplateLoaded += new Project.TemplateLoadedDelegate(Project_OnTemplateLoaded);
        }

        public EditModel(string title, string pageHeader, string pageDescription) : base (title, pageHeader, pageDescription)
        {
            Populate();
            Project.OnTemplateLoaded += new Project.TemplateLoadedDelegate(Project_OnTemplateLoaded);
        }

        private void Project_OnTemplateLoaded()
        {
            LoadTreeView();
            //SetPerObjectUserOptions();
        }

        private void Populate()
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            if (Slyce.Common.Utility.InDesignMode)
            {
                return;
            }

            HasPrev = true;
            HasNext = true;
            HelpFileName = "TaskEditModel.htm";
            _delegateSetProperty = new Generation_DelegateSetProperty(this.SetObjectProperty);
            _delegateCallMethod = new Generation_DelegateCallMethod(this.CallObjectMethod);
            Controller.Instance.BllDatabase.ObjectBeingProcessed += new ProcessingObjectDelegate(BllDatabase_ObjectBeingProcessed);
            AddEditorsToTreeview();

            if (System.IO.File.Exists(Controller.Instance.AppConfig.TemplateFileName))
            {
                // Settings
                LoadTreeView();
            }
            else
            {
                // No project file has been specified. There is probably no project file.
                treeList.ClearNodes();
            }
#if USE_SMO
            buttonReloadTreeListNode.Visible = true;
#endif
        }

        void BllDatabase_ObjectBeingProcessed(string objectName, string description)
        {
            if (labelDatabaseFeedback.InvokeRequired)
            {
                SetCrossThreadProperty(labelDatabaseFeedback, "Text", string.Format("Processing: {1} - {0}", objectName, description));
                CallCrossThreadMethod(labelDatabaseFeedback, "Refresh", null);
            }
            else
            {
                labelDatabaseFeedback.Text = string.Format("Processing: {1} - {0}", objectName, description);
                labelDatabaseFeedback.Refresh();
            }
        }

        public override bool Back()
		{
            return true;// ContentItemNames.SetupDatabase.ToString();
		}

        public override bool Next()
		{
            return true;// ContentItemNames.Options.ToString();
		}

        #region Edit Model

        
        [DoNotObfuscate]
        public void LoadTreeView()
        {
            //ResetAssembly();
            FillOptions();
            Slyce.Common.Utility.SuspendPainting(this.Handle);
            _disableCheckTreeListNode = true;
            treeList.BeginUpdate();
            treeList.ClearNodes();

            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                database.SnapshotMode = true;
            }

            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                TreeListNode rootNode = treeList.AppendNode(new object[] { database.Name, null, null }, null);
                rootNode.Tag = database;
                rootNode.ImageIndex = rootNode.SelectImageIndex = (int)Images.Database;
                
                // Tables
                TreeListNode tableNodes = treeList.AppendNode(new object[] { "Tables" }, rootNode);
                tableNodes.Tag = database.Tables;
                tableNodes.ImageIndex = tableNodes.SelectImageIndex = (int)Images.Table;

                foreach (ArchAngel.Providers.Database.Model.Table table in database.Tables)
                {
                    TreeListNode tableNode = AddScriptObjectNode(tableNodes, table);
                    tableNode.ImageIndex = tableNode.SelectImageIndex = (int)Images.GreenBullet;
                }

                // Views
                TreeListNode viewNodes = treeList.AppendNode(new object[] { "Views" }, rootNode);
                viewNodes.Tag = database.Views;
                viewNodes.ImageIndex = viewNodes.SelectImageIndex = (int)Images.Table;

                foreach (ArchAngel.Providers.Database.Model.View view in database.Views)
                {
                    TreeListNode viewNode = AddScriptObjectNode(viewNodes, view);
                    viewNode.ImageIndex = viewNode.SelectImageIndex = (int)Images.GreenBullet;
                }

                // StoredProcedures
                TreeListNode storedProcedureNodes = treeList.AppendNode(new object[] { "StoredProcedures" }, rootNode);
                storedProcedureNodes.Tag = database.StoredProcedures;
                storedProcedureNodes.ImageIndex = storedProcedureNodes.SelectImageIndex = (int)Images.StoredProcedure;

                foreach (ArchAngel.Providers.Database.Model.StoredProcedure storedProcedure in database.StoredProcedures)
                {
                    TreeListNode storedProcNode = AddScriptObjectNode(storedProcedureNodes, storedProcedure);
                    storedProcNode.ImageIndex = storedProcNode.SelectImageIndex = (int)Images.GreenBullet;
                }
                rootNode.Expanded = true;
            }

            //TreeListHelper.TreeListViewValidate(treeList);
            ProcessTreeValidity(treeList.Nodes);

            _disableCheckTreeListNode = false;
            treeList.EndUpdate();
            SetPerObjectUserOptions();
            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                database.SnapshotMode = false;
            }
            Slyce.Common.Utility.ResumePainting();
        }

        private void LoadTreeListNode(ScriptObject scriptObject)
        {
            foreach (TreeListNode treeListNode in treeList.Nodes)
            {
                LoadTreeListNode(treeListNode, scriptObject);
            }
        }

        private void LoadTreeListNode(TreeListNode parentTreeListNode, ScriptObject scriptObject)
        {
            foreach (TreeListNode childTreeListNode in parentTreeListNode.Nodes)
            {
                if (childTreeListNode.Tag == scriptObject)
                {
                    childTreeListNode.Nodes.Clear();
                    UpdateScriptObjectNode(childTreeListNode, scriptObject);
                    return;
                }

                LoadTreeListNode(childTreeListNode, scriptObject);
            }
        }

        private string[] GetTreeListNodeText(ScriptBase scriptBase)
        {
            if (scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.Table) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.View) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.Column) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.Filter) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.Index) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.Key))
            {
                return new string[] { scriptBase.Alias, scriptBase.Name != scriptBase.Alias ? scriptBase.Name : null, null };
            }

            if (scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.MapColumn))
            {
                MapColumn mapColumn = (MapColumn)scriptBase;
                return new string[] { scriptBase.Alias, scriptBase.Name != scriptBase.Alias ? scriptBase.Name : null, mapColumn.ForeignColumn.Parent.Alias + "." + mapColumn.ForeignColumn.Alias };
            }

            if (scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.OneToOneRelationship) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.OneToManyRelationship) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.ManyToOneRelationship) ||
                scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.ManyToManyRelationship))
            {
                Relationship relationship = (Relationship)scriptBase;
                return new string[] { scriptBase.Alias, scriptBase.Name != scriptBase.Alias ? scriptBase.Name : null, relationship.Path };
            }

            throw new Exception("ScriptBase Type " + scriptBase.GetType().Name + " not found");
        }

        private void UpdateTreeListNodeText(TreeListNode treeListNode, ScriptBase scriptBase)
        {
            string[] columnTexts = GetTreeListNodeText(scriptBase);
            for (int i = 0; i < columnTexts.Length; i++)
            {
                treeListNode.SetValue(i, columnTexts[i]);
            }
            treeListNode.Tag = scriptBase;
        }

        private TreeListNode AddScriptObjectNode(TreeListNode scriptObjectNodes, ScriptObject scriptObject)
        {
            TreeListNode scriptObjectNode = treeList.AppendNode(null, scriptObjectNodes);
            UpdateScriptObjectNode(scriptObjectNode, scriptObject);
            return scriptObjectNode;
        }

        private void UpdateScriptObjectNode(TreeListNode scriptObjectNode, ScriptObject scriptObject)
        {
            scriptObjectNode.SetValue(0, scriptObject.Alias);
            scriptObjectNode.SetValue(1, scriptObject.Name != scriptObject.Alias ? scriptObject.Name : null);
            scriptObjectNode.Tag = scriptObject;
            AddOptionNodes(scriptObjectNode, scriptObject);

            TreeListNode columnNodes = treeList.AppendNode(new object[] { "Columns", null, null }, scriptObjectNode);
            columnNodes.Tag = scriptObject.Columns;
            columnNodes.ImageIndex = columnNodes.SelectImageIndex = (int)Images.Column;

            foreach (Column column in scriptObject.Columns)
            {
                TreeListNode columnNode = treeList.AppendNode(GetTreeListNodeText(column), columnNodes);
                columnNode.Tag = column;
                columnNode.ImageIndex = columnNode.SelectImageIndex = (int)Images.GreenBullet;
                AddOptionNodes(columnNode, (ScriptBase)column);
            }

            TreeListNode relationshipNodes = treeList.AppendNode(new object[] { "Relationships" }, scriptObjectNode);
            relationshipNodes.Tag = scriptObject.Relationships;
            relationshipNodes.ImageIndex = relationshipNodes.SelectImageIndex = (int)Images.Relationship;

            TreeListNode oneToOneRelationshipNodes = treeList.AppendNode(new object[] { "One to One" }, relationshipNodes);
            oneToOneRelationshipNodes.Tag = scriptObject.OneToOneRelationships;
            oneToOneRelationshipNodes.ImageIndex = oneToOneRelationshipNodes.SelectImageIndex = (int)Images.Relationship;

            foreach (OneToOneRelationship oneToOneRelationship in scriptObject.OneToOneRelationships)
            {
                TreeListNode oneToOneRelationshipNode = treeList.AppendNode(GetTreeListNodeText(oneToOneRelationship), oneToOneRelationshipNodes);
                oneToOneRelationshipNode.Tag = oneToOneRelationship;
                oneToOneRelationshipNode.ImageIndex = oneToOneRelationshipNode.SelectImageIndex = (int)Images.GreenBullet;
                AddOptionNodes(oneToOneRelationshipNode, (ScriptBase)oneToOneRelationship);
            }

            TreeListNode oneToManyRelationshipNodes = treeList.AppendNode(new object[] { "One to Many" }, relationshipNodes);
            oneToManyRelationshipNodes.Tag = scriptObject.OneToManyRelationships;
            oneToManyRelationshipNodes.ImageIndex = oneToManyRelationshipNodes.SelectImageIndex = (int)Images.Relationship;

            foreach (OneToManyRelationship oneToManyRelationship in scriptObject.OneToManyRelationships)
            {
                TreeListNode oneToManyRelationshipNode = treeList.AppendNode(GetTreeListNodeText(oneToManyRelationship), oneToManyRelationshipNodes);
                oneToManyRelationshipNode.Tag = oneToManyRelationship;
                oneToManyRelationshipNode.ImageIndex = oneToManyRelationshipNode.SelectImageIndex = (int)Images.GreenBullet;
                AddOptionNodes(oneToManyRelationshipNode, (ScriptBase)oneToManyRelationship);
            }

            TreeListNode manyToOneRelationshipNodes = treeList.AppendNode(new object[] { "Many to One" }, relationshipNodes);
            manyToOneRelationshipNodes.Tag = scriptObject.ManyToOneRelationships;
            manyToOneRelationshipNodes.ImageIndex = manyToOneRelationshipNodes.SelectImageIndex = (int)Images.Relationship;

            foreach (ManyToOneRelationship manyToOneRelationship in scriptObject.ManyToOneRelationships)
            {
                TreeListNode manyToOneRelationshipNode = treeList.AppendNode(GetTreeListNodeText(manyToOneRelationship), manyToOneRelationshipNodes);
                manyToOneRelationshipNode.Tag = manyToOneRelationship;
                manyToOneRelationshipNode.ImageIndex = manyToOneRelationshipNode.SelectImageIndex = (int)Images.GreenBullet;
                AddOptionNodes(manyToOneRelationshipNode, (ScriptBase)manyToOneRelationship);
            }

            TreeListNode manyToManyRelationshipNodes = treeList.AppendNode(new object[] { "Many to Many" }, relationshipNodes);
            manyToManyRelationshipNodes.Tag = scriptObject.ManyToManyRelationships;
            manyToManyRelationshipNodes.ImageIndex = manyToManyRelationshipNodes.SelectImageIndex = (int)Images.Relationship;

            foreach (ManyToManyRelationship manyToManyRelationship in scriptObject.ManyToManyRelationships)
            {
                TreeListNode manyToManyRelationshipNode = treeList.AppendNode(GetTreeListNodeText(manyToManyRelationship), manyToManyRelationshipNodes);
                manyToManyRelationshipNode.Tag = manyToManyRelationship;
                manyToManyRelationshipNode.ImageIndex = manyToManyRelationshipNode.SelectImageIndex = (int)Images.GreenBullet;
                AddOptionNodes(manyToManyRelationshipNode, (ScriptBase)manyToManyRelationship);
            }

            TreeListNode filterNodes = treeList.AppendNode(new object[] { "Filters" }, scriptObjectNode);
            filterNodes.Tag = scriptObject.Filters;
            filterNodes.ImageIndex = filterNodes.SelectImageIndex = (int)Images.Filter;

            foreach (Filter filter in scriptObject.Filters)
            {
                TreeListNode filterNode = treeList.AppendNode(GetTreeListNodeText(filter), filterNodes);
                filterNode.Tag = filter;
                filterNode.ImageIndex = filterNode.SelectImageIndex = (int)Images.GreenBullet;
                AddOptionNodes(filterNode, (ScriptBase)filter);
            }

            if (scriptObject.GetType() == typeof(ArchAngel.Providers.Database.Model.Table))
            {
                ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)scriptObject;
                TreeListNode indexNodes = treeList.AppendNode(new object[] { "Indexes" }, scriptObjectNode);
                indexNodes.Tag = table.Indexes;
                indexNodes.ImageIndex = indexNodes.SelectImageIndex = (int)Images.Index;

                foreach (Index index in table.Indexes)
                {
                    TreeListNode indexNode = treeList.AppendNode(GetTreeListNodeText(index), indexNodes);
                    indexNode.Tag = index;
                    indexNode.ImageIndex = indexNode.SelectImageIndex = (int)Images.GreenBullet;
                }

                TreeListNode keyNodes = treeList.AppendNode(new object[] { "Keys" }, scriptObjectNode);
                keyNodes.Tag = table.Keys;
                keyNodes.ImageIndex = keyNodes.SelectImageIndex = (int)Images.Key;

                foreach (Key key in table.Keys)
                {
                    TreeListNode keyNode = treeList.AppendNode(GetTreeListNodeText(key), keyNodes);
                    keyNode.Tag = key;
                    keyNode.ImageIndex = keyNode.SelectImageIndex = (int)Images.GreenBullet;
                }
            }

            if (scriptObject.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
            {
                ArchAngel.Providers.Database.Model.StoredProcedure storedProcedure = (ArchAngel.Providers.Database.Model.StoredProcedure)scriptObject;
                TreeListNode parameterNodes = treeList.AppendNode(new object[] { "Parameters" }, scriptObjectNode);
                parameterNodes.Tag = storedProcedure.Parameters;
                parameterNodes.ImageIndex = parameterNodes.SelectImageIndex = (int)Images.StoredProcedure;

                foreach (ArchAngel.Providers.Database.Model.StoredProcedure.Parameter parameter in storedProcedure.Parameters)
                {
                    TreeListNode parameterNode = treeList.AppendNode(new object[] { parameter.Name, null, null }, parameterNodes);
                    parameterNode.Tag = parameter;
                    parameterNode.ImageIndex = parameterNode.SelectImageIndex = (int)Images.GreenBullet;
                }
            }
        }

        /// <summary>
        /// Adds per-object options to ScriptObject node.
        /// </summary>
        /// <param name="scriptObjectNode"></param>
        /// <param name="scriptObject"></param>
        private void AddOptionNodes(TreeListNode scriptObjectNode, ScriptBase scriptObject)
        {
            string typeName = scriptObject.GetType().ToString();

            switch (typeName)
            {
                case "ArchAngel.Providers.Database.Model.MapColumn":
                case "ArchAngel.Providers.Database.Model.Column":
                    AddIndividualOptions(scriptObjectNode, _optionsForColumns, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.Filter":
                    AddIndividualOptions(scriptObjectNode, _optionsForFilters, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.Index":
                    AddIndividualOptions(scriptObjectNode, _optionsForIndexes, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.Key":
                    AddIndividualOptions(scriptObjectNode, _optionsForKeys, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.ManyToManyRelationship":
                    AddIndividualOptions(scriptObjectNode, _optionsForManyToManyRelationship, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.ManyToOneRelationship":
                    AddIndividualOptions(scriptObjectNode, _optionsForManyToOneRelationship, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.OneToManyRelationship":
                    AddIndividualOptions(scriptObjectNode, _optionsForOneToManyRelationship, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.OneToOneRelationship":
                    AddIndividualOptions(scriptObjectNode, _optionsForOneToOneRelationships, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.ScriptObject":
                    AddIndividualOptions(scriptObjectNode, _optionsForScriptObjects, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.StoredProcedure":
                    AddIndividualOptions(scriptObjectNode, _optionsForStoredProcedures, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.Table":
                    AddIndividualOptions(scriptObjectNode, _optionsForTable, scriptObject);
                    break;
                case "ArchAngel.Providers.Database.Model.View":
                    AddIndividualOptions(scriptObjectNode, _optionsForViews, scriptObject);
                    break;
                default:
                    throw new NotImplementedException("Not coded yet:" + typeName);
            }
        }

        private void AddIndividualOptions(TreeListNode node, List<Slyce.ITemplate.IOption> options, ScriptBase iteratorObject)
        {
            if (options.Count > 0)
            {
                TreeListNode optionsNode = treeList.AppendNode(new object[] { "Options", null, null }, node);
                optionsNode.ImageIndex = optionsNode.SelectImageIndex = (int)Images.Option;
                optionsNode.Tag = options;

                foreach (Slyce.ITemplate.IOption option in options)
                {
                    object defaultValue = null;
                    bool found = false;

                    foreach (UserOption opt in iteratorObject.UserOptions)
                    {
                        if (opt.Name == option.VariableName)
                        {
                            defaultValue = opt.Value;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        //if (option.DefaultValueIsFunction)
                        //{
                            defaultValue = GetDefaultOptionValueFromFunction(option.DefaultValue, iteratorObject);
                        //}
                        //else
                        //{
                        //    defaultValue = option.DefaultValue;
                        //}
                        iteratorObject.AddUserOption(new ArchAngel.Providers.Database.Model.UserOption(option.VariableName, option.VarType, defaultValue));
                    }
                    if (Project.MustDisplayOptionToUser(option, iteratorObject))
                    {
                        TreeListNode optionNode = treeList.AppendNode(new object[] { option.Text, defaultValue.ToString(), null }, optionsNode);
                        optionNode.Tag = option;
                        optionNode.ImageIndex = optionNode.SelectImageIndex = (int)Images.GreenBullet;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the per-object user options of the actual objects that are going to be used for generation.
        /// This ensures all values are set on the corredsponding objects, ready for generation. This function
        /// should get called just before the generation process begins, to update all tables, views, columns etc with
        /// the user-option values selected by the user.
        /// </summary>
        public void SetPerObjectUserOptions()
        {
            StringBuilder codeText = new StringBuilder(2000);

            // Get the code to compile the UserOptions
            foreach (TreeListNode childNode in treeList.Nodes)
            {
                SetPerObjectUserOptionsForNodeAndChildren(childNode);
            }
        }

        private void SetPerObjectUserOptionsForNodeAndChildren(TreeListNode node)
        {
            if (node.Tag.GetType() == typeof(ArchAngel.Providers.Database.Model.Database))
            {
                foreach (TreeListNode childNode in node.Nodes)
                {
                    SetPerObjectUserOptionsForNodeAndChildren(childNode);
                }
            }
            //List<ArchAngel.Providers.Database.Model.ScriptObject> scriptObjects = new List<ScriptObject>();
            ScriptBase scriptBase;
            Type type = node.Tag.GetType();
            List<Slyce.ITemplate.IOption> hiddenOptions = new List<Slyce.ITemplate.IOption>();

            // We need to handle any options that are not visible to the user ie: not in the treelist
            if (type.BaseType == typeof(ScriptBase))
            {
                scriptBase = (ScriptBase)node.Tag;

                foreach (Slyce.ITemplate.IUserOption userOption in scriptBase.UserOptions)
                {
                    // Find the matching Option for the UserOption
                    foreach (Slyce.ITemplate.IOption opt in Project.CurrentProject.Options)
                    {
                        if (opt.VariableName == userOption.Name)
                        {
                            if (!Project.MustDisplayOptionToUser(opt, scriptBase))
                            {
                                object defaultValue = null;

                                if (opt.DefaultValueIsFunction)
                                {
                                    defaultValue = GetDefaultOptionValueFromFunction(opt.DefaultValue, scriptBase);
                                }
                                else
                                {
                                    defaultValue = opt.DefaultValue;
                                }
                                userOption.Value = defaultValue;
                            }
                            break;
                        }
                    }
                }
            }
            MethodInfo method = type.GetMethod("AddUserOption");

            //foreach (TreeListNode childNode in node.Nodes)
            //{
            //    // Check for any options that are not displayed to the user, because we won't find them in the treelist!
            //    if (childNode.GetValue(0) != null && childNode.GetValue(0).ToString() == "Options")
            //    {
            //        // Does the object support UserOptions?
            //        if (method != null)
            //        {
            //            //scriptObjects.Add((ArchAngel.Providers.Database.Model.ScriptObject)node.Tag);

            //            // Loop through all the option nodes, setting the value on the object for each one
            //            foreach (TreeListNode optionNode in childNode.Nodes)
            //            {
            //                Slyce.ITemplate.IOption option = (Slyce.ITemplate.IOption)optionNode.Tag;
            //                UserOption userOption = new UserOption(option.VariableName, option.VarType, option.DefaultValue);
            //                method.Invoke(node.Tag, new object[] { userOption });
            //            }
            //        }
            //    }
            //    else
            //    {
            //        SetPerObjectUserOptionsForNodeAndChildren(childNode);
            //    }
            //}
        }

        /// <summary>
        /// Gets the default value from the function that has been specified as the DefaultValueFunction.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="iteratorObject"></param>
        /// <returns></returns>
        private object GetDefaultOptionValueFromFunction(string functionName, object iteratorObject)
        {
            try
            {
                object[] parameters = new object[] { iteratorObject };
                return Loader.Instance.CallTemplateFunction(functionName, ref parameters);
            }
            catch (System.MissingMethodException ex)
            {
                object[] parameters = new object[0];
                return Loader.Instance.CallTemplateFunction(functionName, ref parameters);
            }
        }

        #region Check and Colour nodes

        private CheckState GetNodeCheckState(TreeListNode treeListNode)
        {
            //Type type = BLL.Helper.GetBaseType(treeListNode.Tag);
            string typeName = ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag).Name;

            //if (typeName == "TempDeserializerBase")
            //{
            //    Type baseType = treeListNode.Tag.GetType();

            //    while (baseType.BaseType != typeof(ArchAngel.Providers.Database.TempDeserializerBase))
            //    {
            //        baseType = baseType.BaseType;
            //    }
            //    typeName = baseType.Name;
            //}
            switch (typeName)
            {
                case "Database":
                    ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)treeListNode.Tag;
                    return CheckState.Checked;

                case "ScriptBase":
                    ScriptBase scriptBase = (ScriptBase)treeListNode.Tag;
                    if (scriptBase.Enabled)
                    {
                        return CheckState.Checked;
                    }
                    else
                    {
                        return CheckState.Unchecked;
                    }

                case "Array":
                    int enabledCount = 0;
                    int disabledCount = 0;
                    ScriptBase[] scriptBases = (ScriptBase[])treeListNode.Tag;

                    foreach (ScriptBase scriptBase1 in scriptBases)
                    {
                        if (scriptBase1.Enabled)
                        {
                            enabledCount++;
                        }
                        else
                        {
                            disabledCount++;
                        }
                    }

                    if (disabledCount == scriptBases.Length)
                    {
                        return CheckState.Unchecked;
                    }
                    else if (enabledCount == scriptBases.Length)
                    {
                        return CheckState.Checked;
                    }
                    else
                    {
                        return CheckState.Indeterminate;
                    }

                case "List`1": // Generic List
                    string fullTypeName = ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag).FullName;

                    if (fullTypeName.IndexOf("Slyce.ITemplate.IOption") >= 0)
                    {
                        return CheckState.Checked;
                    }
                    else
                    {
                        throw new Exception("Unexpected generic list type. TypeName = " + fullTypeName);
                    }

                case "Option":
                    Slyce.TemplateInfo.Option option = (Slyce.TemplateInfo.Option)treeListNode.Tag;

                    if (option.Enabled)
                    {
                        return CheckState.Checked;
                    }
                    else
                    {
                        return CheckState.Unchecked;
                    }

                default:
                    throw new Exception("Cannot determine check state. TypeName = " + typeName);
            }
        }

        private void ToggleNodeCheckState(TreeListNode treeListNode)
        {
            treeList.BeginUpdate();
            CheckState checkState = GetNodeCheckState(treeListNode);

            if (checkState == CheckState.Indeterminate || checkState == CheckState.Unchecked)
            {
                checkState = CheckState.Checked;

                if (treeListNode.ImageIndex == (int)Images.OrangeBullet)
                {
                    treeListNode.ImageIndex = treeListNode.SelectImageIndex = (int)Images.GreenBullet;
                }
            }
            else
            {
                checkState = CheckState.Unchecked;

                if (treeListNode.ImageIndex == (int)Images.GreenBullet)
                {
                    treeListNode.ImageIndex = treeListNode.SelectImageIndex = (int)Images.OrangeBullet;
                }
            }

            SetNodeCheckState(treeListNode, checkState);

            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                database.SnapshotMode = true;
            }
            ProcessTreeValidity(treeList.Nodes);

            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                database.SnapshotMode = false;
            }
            treeList.EndUpdate();
        }

        private void SetNodeCheckState(TreeListNode treeListNode, CheckState checkState)
        {
            switch (checkState)
            {
                case CheckState.Checked:
                    treeListNode.StateImageIndex = (int)Images.Checked;
                    break;
                case CheckState.Unchecked:
                    treeListNode.StateImageIndex = (int)Images.Unchecked;
                    break;
            }
            Type type = ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag);

            if (type == typeof(ScriptBase))
            {
                ScriptBase scriptBase = (ScriptBase)treeListNode.Tag;

                if (checkState == CheckState.Checked)
                {
                    scriptBase.Enabled = true;
                }
                else
                {
                    scriptBase.Enabled = false;
                }

                //if (scriptBase.GetType() == typeof(OneToOneRelationship) ||
                //    scriptBase.GetType() == typeof(OneToManyRelationship) ||
                //    scriptBase.GetType() == typeof(ManyToOneRelationship) ||
                //    scriptBase.GetType() == typeof(ManyToManyRelationship))
                //{
                //    Relationship relationship = (Relationship)scriptBase;
                //    relationship.ForeignRelationship.Enabled = scriptBase.Enabled;
                //}

                if (scriptBase.GetType() == typeof(ManyToOneRelationship))
                {
                    // TODO: Multiple columns can be mapped on a single relationship. All should be disabled, not just one.
                    ManyToOneRelationship relationship = (ManyToOneRelationship)scriptBase;
                    MapColumn mapColumn = (MapColumn)relationship.Parent.GetColumn(relationship.Name);

                    // If a MapColumn exists for this relationship, disable it.
                    if (mapColumn != null)
                    {
                        mapColumn.Enabled = scriptBase.Enabled;
                    }
                }
            }
            else if (type == typeof(Slyce.TemplateInfo.Option))
            {
                Slyce.TemplateInfo.Option option = (Slyce.TemplateInfo.Option)treeListNode.Tag;

                if (checkState == CheckState.Checked)
                {
                    option.Enabled = true;
                }
                else
                {
                    option.Enabled = false;
                }
            }
            foreach (TreeListNode childTreeListNode in treeListNode.Nodes)
            {
                SetNodeCheckState(childTreeListNode, checkState);
            }
        }

        private void treeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (_disableCheckTreeListNode)
            {
                return;
            }

            CheckState checkState = GetNodeCheckState(e.Node);
            switch (checkState)
            {
                case CheckState.Unchecked:
                    e.NodeImageIndex = 0;
                    break;

                case CheckState.Checked:
                    e.NodeImageIndex = 1;
                    break;

                case CheckState.Indeterminate:
                    e.NodeImageIndex= 2;
                    break;

                default:
                    break;
            }
        }

        private void treeList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DevExpress.XtraTreeList.TreeListHitInfo treeListHit = treeList.CalcHitInfo(new Point(e.X, e.Y));
                if (treeListHit.HitInfoType == DevExpress.XtraTreeList.HitInfoType.StateImage)
                {
                    ToggleNodeCheckState(treeListHit.Node);
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                HideAllMenuItems(contextMenuStripTreeView);
                Point point = new Point(e.X, e.Y);

                DevExpress.XtraTreeList.TreeListHitInfo treeListHit = treeList.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode treeListNode = treeListHit.Node;
                treeList.SetFocusedNode(treeListNode);

                if (treeListNode == null ||
                    treeListNode.Tag == null ||
                    treeListNode.Tag.GetType() == typeof(ArchAngel.Providers.Database.Model.Database) ||
                    treeListNode.GetDisplayText(0) == "Relationships" ||
                    treeListNode.GetDisplayText(0) == "Parameters" ||
                    treeListNode.ParentNode.GetDisplayText(0) == "Parameters")
                {
                    return;
                }

                if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag) == typeof(System.Array))
                {
                    toolStripMenuItemTreeViewAdd.Visible = true;
                    toolStripMenuItemTreeViewEdit.Visible = true;
                    toolStripMenuItemTreeViewDelete.Visible = true;

                    toolStripMenuItemTreeViewAdd.Enabled = true;
                    toolStripMenuItemTreeViewEdit.Enabled = false;
                    toolStripMenuItemTreeViewDelete.Enabled = false;
                }

                if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag) == typeof(ScriptBase))
                {
                    toolStripMenuItemTreeViewAdd.Visible = true;
                    toolStripMenuItemTreeViewEdit.Visible = true;
                    toolStripMenuItemTreeViewDelete.Visible = true;

                    toolStripMenuItemTreeViewAdd.Enabled = false;
                    toolStripMenuItemTreeViewEdit.Enabled = true;

                    ScriptBase scriptBase = (ScriptBase)treeListNode.Tag;

                    if (scriptBase.GetType() == typeof(MapColumn))
                    {
                        toolStripMenuItemTreeViewDelete.Enabled = true;
                    }
                    else
                    {
                        toolStripMenuItemTreeViewDelete.Enabled = scriptBase.IsUserDefined;
                    }
                }

                // Map Columns
                if (treeListNode.Tag.GetType() == typeof(Column[]))
                {
                    toolStripMenuItemTreeViewAddMapColumn.Visible = true;
                }
                //else
                //{
                //    toolStripMenuItemTreeViewAddMapColumn.Visible = false;
                //}

                // Stored Procedure Execute
                if (treeListNode.Tag.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
                {
                    // GFH commented out
                    toolStripMenuItemTreeViewExecuteStoredProcedure.Visible = true;
                }
                //else
                //{
                //    // GFH commented out
                //    toolStripMenuItemTreeViewExecuteStoredProcedure.Visible = false;
                //}
                if (treeListNode.Tag.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
                {
                    // GFH commented out
                    toolStripMenuItemTreeViewExecuteStoredProcedure.Visible = true;
                }
                if (treeListNode.Tag.GetType() == typeof(Slyce.TemplateInfo.Option))
                {
                    toolStripMenuItemTreeViewEdit.Visible = true;
                }

                contextMenuStripTreeView.Show(treeList, point);
            }
        }

        private void HideAllMenuItems(ContextMenuStrip menu)
        {
            foreach (ToolStripItem item in menu.Items)
            {
                item.Visible = false;
            }
        }

        private void treeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeListNode treeListNode = treeList.FocusedNode;

            if (treeListNode == null)
            {
                return;
            }

            if (treeListNode.Tag == null)
            {
                return;
            }

            if (treeListNode[0].ToString() == "Relationships")
            {
                return;
            }

            if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag) == typeof(Array))
            {
                toolStripMenuItemTreeViewAdd_Click(null, null);
            }

            if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag) == typeof(ScriptBase))
            {
                toolStripMenuItemTreeViewEdit_Click(null, null);
            }
        }

        private void treeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            Color color = Color.Black;
            int nodeImageIndex = e.Node.ImageIndex;

            if (nodeImageIndex == RedBulletValue ||
                nodeImageIndex == RedColumnValue ||
                nodeImageIndex == RedDatabaseValue ||
                nodeImageIndex == RedFilterValue ||
                nodeImageIndex == RedIndexValue ||
                nodeImageIndex == RedKeyValue ||
                nodeImageIndex == RedOptionValue ||
                nodeImageIndex == RedRelationshipValue ||
                nodeImageIndex == RedStoredProcedureValue ||
                nodeImageIndex == RedTableValue)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
                return;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Black;
            }
            if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(e.Node.Tag) == typeof(ScriptBase))
            {
                ScriptBase scriptBase = (ScriptBase)e.Node.Tag;
                if (!scriptBase.Enabled)
                {
                    e.Appearance.ForeColor = Color.Gray;
                    return;
                }

                if (scriptBase.GetType() == typeof(MapColumn))
                {
                    e.Appearance.ForeColor = Color.Green;
                    return;
                }
                else
                {
                    if (scriptBase.IsUserDefined)
                    {
                        e.Appearance.ForeColor = Color.Blue;
                        return;
                    }
                }

                //TreeListHelper.TreeListNodeError(e);
            }
        }

        #endregion

        private void treeList_Resize(object sender, EventArgs e)
        {
            double treeViewWidth = System.Convert.ToDouble(treeList.Width);
            treeListColumnAlias.Width = (int)(treeViewWidth * 0.5);
            treeListColumnName.Width = (int)(treeList.Width * 0.25);
            treeListColumnNote.Width = (int)(treeList.Width * 0.25);
        }

        private void buttonReloadTreeListView_Click(object sender, EventArgs e)
        {
            LoadTreeView();
        }

        private void ValidateTreeView_OLD()
        {
            /*TreeViewHelper treeViewHelper = new TreeViewHelper(treeViewDatabaseProviderModel);
            //validateTreeView.
            if (_validationThread != null)
            {
                _validationThread.Abort();
                while (_validationThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(20);
                }
            }

            Slyce.Common.Utility.SuspendPainting(treeViewDatabaseProviderModel.Handle);

            _validationThread = new Thread(new ThreadStart(treeViewHelper.TreeViewValidate));
            _validationThread.Start();*/

            /*while (_validationThread.IsAlive)
            {
                Application.DoEvents();
            }
            
            _validationThread.Abort();
            _validationThread = null;
            Slyce.Common.Utility.ResumePainting();*/
        }

        #endregion

        #region TreeView

        private List<Slyce.ITemplate.IOption> GetSpecificOptions(Type type)
        {
            string typeName = type.Name;
            List<Slyce.ITemplate.IOption> options = new List<Slyce.ITemplate.IOption>();

            foreach (Slyce.ITemplate.IOption option in Project.CurrentProject.Options)
            {
                if (option.IteratorName == typeName)
                {
                    options.Add(option);
                }
            }
            return options;
        }

        private void FillOptions()
        {
            _optionsForTable = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.Table));
            _optionsForViews = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.View));
            _optionsForStoredProcedures = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.StoredProcedure));
            _optionsForScriptObjects = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.ScriptObject));
            _optionsForColumns = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.Column));
            _optionsForOneToOneRelationships = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.OneToOneRelationship));
            _optionsForOneToManyRelationship = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.OneToManyRelationship));
            _optionsForManyToOneRelationship = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.ManyToOneRelationship));
            _optionsForManyToManyRelationship = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.ManyToManyRelationship));
            _optionsForFilters = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.Filter));
            _optionsForIndexes = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.Index));
            _optionsForKeys = GetSpecificOptions(typeof(ArchAngel.Providers.Database.Model.Key));
        }

        private void toolStripMenuItemTreeViewAdd_Click(object sender, EventArgs e)
        {
            treeList.InvalidateNodes();

            treeList.BeginUpdate();

            TreeListNode treeListNode = treeList.FocusedNode;

            Type type = treeListNode.Tag.GetType();

            if (type == typeof(ArchAngel.Providers.Database.Model.Table[]) ||
                type == typeof(ArchAngel.Providers.Database.Model.View[]) ||
                type == typeof(ArchAngel.Providers.Database.Model.StoredProcedure[]))
            {
                ArchAngel.Providers.Database.Model.Database parent = (ArchAngel.Providers.Database.Model.Database)treeListNode.ParentNode.Tag;
                FormScriptObject form = new FormScriptObject((ScriptObject[])treeListNode.Tag, parent);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    parent.AddScriptObject(form.ScriptObject);
                    
                    TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.ScriptObject), treeListNode);
                    newTreeListNode.Tag = form.ScriptObject;
                    newTreeListNode.ImageIndex = newTreeListNode.SelectImageIndex = (int)Images.GreenBullet;

                    SetupNewTreeListNode(newTreeListNode, form.ScriptObject);

                    if (type == typeof(ArchAngel.Providers.Database.Model.Table[]))
                    {
                        treeListNode.Tag = parent.Tables;
                    }

                    if (type == typeof(ArchAngel.Providers.Database.Model.View[]))
                    {
                        treeListNode.Tag = parent.Views;
                    }

                    if (type == typeof(ArchAngel.Providers.Database.Model.StoredProcedure[]))
                    {
                        treeListNode.Tag = parent.StoredProcedures;
                    }
                    //TreeListHelper.TreeListNodeValidate(newTreeListNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                }
            }
            else if (type == typeof(Column[]))
            {
                ScriptObject parent = (ScriptObject)treeListNode.ParentNode.Tag;
                FormColumn form = new FormColumn(parent);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    parent.AddColumn(form.Column);
   
                    TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.Column), treeListNode);
                    newTreeListNode.Tag = form.Column;
                    newTreeListNode.ImageIndex = newTreeListNode.SelectImageIndex = (int)Images.GreenBullet;

                    treeListNode.Tag = parent.Columns;

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                }
            }
            else if (type == typeof(OneToOneRelationship[]) ||
                type == typeof(OneToManyRelationship[]) ||
                type == typeof(ManyToOneRelationship[]) ||
                type == typeof(ManyToManyRelationship[]))
            {
                ScriptObject parent = (ScriptObject)treeListNode.ParentNode.ParentNode.Tag;

                Type relationshipType = null;
                if (type == typeof(OneToOneRelationship[]))
                {
                    relationshipType = typeof(OneToOneRelationship);
                }

                if (type == typeof(ManyToOneRelationship[]))
                {
                    relationshipType = typeof(ManyToOneRelationship);
                }

                if (type == typeof(OneToManyRelationship[]))
                {
                    relationshipType = typeof(OneToManyRelationship);
                }

                if (type == typeof(ManyToManyRelationship[]))
                {
                    relationshipType = typeof(ManyToManyRelationship);
                }

                ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)treeListNode.ParentNode.ParentNode.ParentNode.ParentNode.Tag;

                FormRelationship form = new FormRelationship(relationshipType, (ScriptObject)treeListNode.ParentNode.ParentNode.Tag, Controller.Instance.BllDatabase.EnabledScriptObjects);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    ScriptObject primaryScriptObject = form.PrimaryRelationship.Parent;
                    primaryScriptObject.AddRelationship(form.PrimaryRelationship);

                    ScriptObject foreignScriptObject = form.ForeignRelationship.Parent;
                    foreignScriptObject.AddRelationship(form.ForeignRelationship);
                    
                    TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.PrimaryRelationship), treeListNode);
                    newTreeListNode.Tag = form.PrimaryRelationship;
                    newTreeListNode.ImageIndex = newTreeListNode.SelectImageIndex = (int)Images.GreenBullet;

                    if (type == typeof(OneToOneRelationship[]))
                    {
                        treeListNode.Tag = parent.OneToOneRelationships;
                    }

                    if (type == typeof(ManyToOneRelationship[]))
                    {
                        treeListNode.Tag = parent.ManyToOneRelationships;
                    }

                    if (type == typeof(OneToManyRelationship[]))
                    {
                        treeListNode.Tag = parent.OneToManyRelationships;
                    }

                    if (type == typeof(ManyToManyRelationship[]))
                    {
                        treeListNode.Tag = parent.ManyToManyRelationships;
                    }

                    LoadTreeListNode(foreignScriptObject);
                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                }
            }
            else if (type == typeof(Filter[]))
            {
                ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)treeListNode.ParentNode.ParentNode.ParentNode.Tag;
                ScriptObject parent = (ScriptObject)treeListNode.ParentNode.Tag;
                
                FormFilter2 form;
                // Stored Procedure Filters can only be created from other Stored Procedures (Enabled or Disabled)
                if (parent.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
                {
                    form = new FormFilter2(this.ParentForm, parent, database.StoredProcedures);
                }
                else
                {
                    form = new FormFilter2(this.ParentForm, parent, database.EnabledScriptObjects);
                }
                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    parent.AddFilter(form.Filter);

                    TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.Filter), treeListNode);
                    newTreeListNode.Tag = form.Filter;
                    newTreeListNode.ImageIndex = newTreeListNode.SelectImageIndex = (int)Images.GreenBullet;

                    treeListNode.Tag = parent.Filters;

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                }
            }
            else if (type == typeof(Index[]))
            {
                ArchAngel.Providers.Database.Model.Table parent = (ArchAngel.Providers.Database.Model.Table)treeListNode.ParentNode.Tag;
                FormIndex form = new FormIndex((ArchAngel.Providers.Database.Model.Table)treeListNode.ParentNode.Tag);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    parent.AddIndex(form.Index);

                    TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.Index), treeListNode);
                    newTreeListNode.Tag = form.Index;
                    newTreeListNode.ImageIndex = newTreeListNode.SelectImageIndex = (int)Images.GreenBullet;

                    treeListNode.Tag = parent.Indexes;

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                }
            }
            else if (type == typeof(Key[]))
            {
                ArchAngel.Providers.Database.Model.Table parent = (ArchAngel.Providers.Database.Model.Table)treeListNode.ParentNode.Tag;
                FormKey form = new FormKey((ArchAngel.Providers.Database.Model.Table)treeListNode.ParentNode.Tag);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    parent.AddKey(form.Key);

                    TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.Key), treeListNode);
                    newTreeListNode.Tag = form.Key;
                    newTreeListNode.ImageIndex = newTreeListNode.SelectImageIndex = (int)Images.GreenBullet;

                    treeListNode.Tag = parent.Keys;

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                }
            }
            treeList.EndUpdate();
        }

        private void toolStripMenuItemTreeViewEdit_Click(object sender, EventArgs e)
        {
            TreeListNode treeListNode = treeList.FocusedNode;
            Type type = treeListNode.Tag.GetType();

            if (type == typeof(Slyce.TemplateInfo.Option))
            {
                Slyce.ITemplate.IOption option = (Slyce.ITemplate.IOption)treeListNode.Tag;
                Slyce.ITemplate.IUserOption userOption = null;
                object parentObject = treeListNode.ParentNode.ParentNode.Tag;
                Type parentType = parentObject.GetType();

                if (parentType == typeof(ArchAngel.Providers.Database.Model.Table) ||
                parentType == typeof(ArchAngel.Providers.Database.Model.View) ||
                parentType == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
                {
                    ArchAngel.Providers.Database.Model.ScriptObject parentScriptObject = (ArchAngel.Providers.Database.Model.ScriptObject)parentObject;

                    for (int i = 0; i < parentScriptObject.UserOptions.Count; i++)
                    {
                        if (parentScriptObject.UserOptions[i].Name == option.VariableName)
                        {
                            userOption = parentScriptObject.UserOptions[i];
                            FormObjectOptionEdit form = new FormObjectOptionEdit(option, userOption, parentScriptObject);

                            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                            {
                                Controller.MainForm.Cursor = Cursors.WaitCursor;
                                parentScriptObject.UserOptions[i].Value = form.UserOption.Value;
                                treeList.Selection[0].Tag = form.CurrentOption;
                                treeList.Selection[0].SetValue(1, form.UserOption.Value);
                                ProcessTreeValidity(treeList.Nodes);
                                Controller.MainForm.Cursor = Cursors.Default;
                            }

                            //((ArchAngel.Providers.Database.Model.ScriptObject)treeList.Selection[0].ParentNode.ParentNode.Tag).UserOptions[i].Value = Convert.ChangeType(form.CurrentOption.DefaultValue, form.CurrentOption.VarType);
                            //parentScriptObject.UserOptions[i].Value = Convert.ChangeType(form.CurrentOption.DefaultValue, form.CurrentOption.VarType);
                            break;
                        }
                    }
                    return;
                }
                else if (parentType == typeof(ArchAngel.Providers.Database.Model.Column))
                {
                    ArchAngel.Providers.Database.Model.Column parentColumn = (ArchAngel.Providers.Database.Model.Column)parentObject;

                    for (int i = 0; i < parentColumn.UserOptions.Count; i++)
                    {
                        if (parentColumn.UserOptions[i].Name == option.VariableName)
                        {
                            userOption = parentColumn.UserOptions[i];
                            FormObjectOptionEdit form = new FormObjectOptionEdit(option, userOption, parentColumn);

                            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                            {
                                Controller.MainForm.Cursor = Cursors.WaitCursor;
                                parentColumn.UserOptions[i].Value = form.UserOption.Value;
                                treeList.Selection[0].Tag = form.CurrentOption;
                                treeList.Selection[0].SetValue(1, form.UserOption.Value);
                                ProcessTreeValidity(treeList.Nodes);
                                Controller.MainForm.Cursor = Cursors.Default;
                            }
                            break;
                        }
                    }
                    return;
                }
            }
            ScriptBase scriptBase = (ScriptBase)treeListNode.Tag;
            
            if (type == typeof(ArchAngel.Providers.Database.Model.Table) ||
                type == typeof(ArchAngel.Providers.Database.Model.View) ||
                type == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
            {
                FormScriptObject form = new FormScriptObject((ScriptObject)scriptBase, (ScriptObject[])treeListNode.ParentNode.Tag);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    UpdateTreeListNodeText(treeListNode, form.ScriptObject);

                    //TreeListHelper.TreeListNodeValidate(treeListNode);
                    foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
                    {
                        database.SnapshotMode = true;
                    }
                    treeList.BeginUpdate();
                    //ValidateNode(treeListNode);
                    ProcessTreeValidity(treeList.Nodes);
                    treeList.EndUpdate();

                    foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
                    {
                        database.SnapshotMode = false;
                    }
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (type == typeof(Column))
            {
                FormColumn form = new FormColumn((Column)scriptBase);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    UpdateTreeListNodeText(treeListNode, form.Column);

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (type == typeof(MapColumn))
            {
                FormMapColumn form = new FormMapColumn((ScriptObject)treeListNode.ParentNode.ParentNode.Tag, (MapColumn)scriptBase);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    MapColumn mapColumn = (MapColumn)scriptBase;

                    UpdateTreeListNodeText(treeListNode, form.MapColumn);

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (type.BaseType == typeof(Relationship))
            {
                ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)treeListNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.Tag;
                FormRelationship form = new FormRelationship((Relationship)scriptBase, Controller.Instance.BllDatabase.ScriptObjects);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    UpdateTreeListNodeText(treeListNode, form.PrimaryRelationship);

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (type == typeof(Filter))
            {
                ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)treeListNode.ParentNode.ParentNode.ParentNode.ParentNode.Tag;
                Filter filter = (Filter)scriptBase;

                FormFilter2 form;
                // Stored Procedure Filters can only be created from other Stored Procedures (Enabled or Disabled)
                if (filter.Parent.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
                {
                    form = new FormFilter2(this.ParentForm, filter, database.StoredProcedures);
                }
                else
                {
                    form = new FormFilter2(this.ParentForm, filter, database.EnabledScriptObjects);
                }
                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    UpdateTreeListNodeText(treeListNode, form.Filter);

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (type == typeof(Index))
            {
                FormIndex form = new FormIndex((Index)scriptBase);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    UpdateTreeListNodeText(treeListNode, form.Index);

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
            else if (type == typeof(Key))
            {
                FormKey form = new FormKey((Key)scriptBase);

                if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    Controller.MainForm.Cursor = Cursors.WaitCursor;
                    UpdateTreeListNodeText(treeListNode, form.Key);

                    //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                    ProcessTreeValidity(treeList.Nodes);
                    Controller.MainForm.Cursor = Cursors.Default;
                    return;
                }
            }
        }

        private void toolStripMenuItemTreeViewDelete_Click(object sender, EventArgs e)
        {
            Controller.MainForm.Cursor = Cursors.WaitCursor;
            // TODO: ensure these objects are not used else where
            TreeListNode treeListNode = treeList.FocusedNode;
            ScriptBase scriptBase = (ScriptBase)treeListNode.Tag;

            Type type = treeListNode.Tag.GetType();

            if (type == typeof(ArchAngel.Providers.Database.Model.Table) ||
                type == typeof(ArchAngel.Providers.Database.Model.View) ||
                type == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
            {
                ScriptObject scriptObject = (ScriptObject)treeListNode.Tag;
                ArchAngel.Providers.Database.Model.Database database = scriptObject.Database;
                database.RemoveScriptObject(scriptObject);
            }
            else if (type == typeof(Column))
            {
                Column column = (Column)treeListNode.Tag;
                ScriptObject scriptObject = column.Parent;
                scriptObject.RemoveColumn(column);
            }
            else if (type == typeof(MapColumn))
            {
                MapColumn mapColumn = (MapColumn)treeListNode.Tag;
                ScriptObject scriptObject = mapColumn.Parent;
                scriptObject.RemoveColumn(mapColumn);
            }
            else if (type.BaseType == typeof(Relationship))
            {
                Relationship relationship = (Relationship)treeListNode.Tag;
                ScriptObject primaryScriptObject = relationship.Parent;
                primaryScriptObject.RemoveRelationship(relationship);

                ScriptObject foreignScriptObject = relationship.ForeignRelationship.Parent;
                foreignScriptObject.RemoveRelationship(relationship.ForeignRelationship);

                LoadTreeListNode(foreignScriptObject);
            }
            else if (type == typeof(Filter))
            {
                Filter filter = (Filter)treeListNode.Tag;
                ScriptObject scriptObject = filter.Parent;
                scriptObject.RemoveFilter(filter);
            }
            else if (type == typeof(Index))
            {
                Index index = (Index)treeListNode.Tag;
                ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)index.Parent;
                table.RemoveIndex(index);
            }
            else if (type == typeof(Key))
            {
                Key key = (Key)treeListNode.Tag;
                ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)key.Parent;
                table.RemoveKey(key);
            }
            treeList.DeleteNode(treeListNode);
            ProcessTreeValidity(treeList.Nodes);
            Controller.MainForm.Cursor = Cursors.Default;
        }

        private void toolStripMenuItemTreeViewExecuteStoredProcedure_Click(object sender, EventArgs e)
        {
            treeList.BeginUpdate();

            TreeListNode treeListNode = treeList.FocusedNode;

            ArchAngel.Providers.Database.Model.StoredProcedure storedProcedure = (ArchAngel.Providers.Database.Model.StoredProcedure)treeListNode.Tag;

            Controller.Instance.BllDatabase.FillStoredProcedureColumns(storedProcedure);


            /*parent.AddColumn(column);

            TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.MapColumn), treeListNode);
            newTreeListNode.Tag = form.MapColumn;

            treeListNode.Tag = parent.Columns;

            TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);*/

            treeList.EndUpdate();

            LoadTreeView();
        }

        private void toolStripMenuItemTreeViewAddMapColumn_Click(object sender, EventArgs e)
        {
            treeList.BeginUpdate();

            TreeListNode treeListNode = treeList.FocusedNode;

            ScriptObject parent = (ScriptObject)treeListNode.ParentNode.Tag;
            FormMapColumn form = new FormMapColumn(parent);

            if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            {
                parent.AddColumn(form.MapColumn);

                TreeListNode newTreeListNode = treeList.AppendNode(GetTreeListNodeText(form.MapColumn), treeListNode);
                newTreeListNode.Tag = form.MapColumn;

                treeListNode.Tag = parent.Columns;

                //TreeListHelper.TreeListNodeValidate(treeListNode.ParentNode);
                ValidateNode(treeListNode.ParentNode);
            }
            treeList.EndUpdate();
        }

        private void SetupNewTreeListNode(TreeListNode treeListNode, ScriptObject scriptObject)
        {
            TreeListNode columnNodes = treeList.AppendNode(new object[] { "Columns", null, null }, treeListNode);
            columnNodes.Tag = scriptObject.Columns;

            TreeListNode relationshipNodes = treeList.AppendNode(new object[] { "Relationships", null, null }, treeListNode);
            relationshipNodes.Tag = scriptObject.Relationships;

            TreeListNode oneToOneRelationshipNodes = treeList.AppendNode(new object[] { "One to One", null, null }, relationshipNodes);
            oneToOneRelationshipNodes.Tag = scriptObject.OneToOneRelationships;

            TreeListNode oneToManyRelationshipNodes = treeList.AppendNode(new object[] { "One to Many", null, null }, relationshipNodes);
            oneToManyRelationshipNodes.Tag = scriptObject.OneToManyRelationships;

            TreeListNode manyToOneRelationshipNodes = treeList.AppendNode(new object[] { "Many to One", null, null }, relationshipNodes);
            manyToOneRelationshipNodes.Tag = scriptObject.ManyToOneRelationships;

            TreeListNode manyToManyRelationshipNodes = treeList.AppendNode(new object[] { "Many to Many", null, null }, relationshipNodes);
            manyToManyRelationshipNodes.Tag = scriptObject.ManyToManyRelationships;

            TreeListNode filterNodes = treeList.AppendNode(new object[] { "Filters", null, null }, treeListNode);
            filterNodes.Tag = scriptObject.Filters;

            if (scriptObject.GetType() == typeof(ArchAngel.Providers.Database.Model.Table))
            {
                ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)scriptObject;

                TreeListNode indexNodes = treeList.AppendNode(new object[] { "Indexes", null, null }, treeListNode);
                indexNodes.Tag = table.Indexes;

                TreeListNode keyNodes = treeList.AppendNode(new object[] { "Keys", null, null }, treeListNode);
                keyNodes.Tag = table.Keys;
            }
        }

        //private void buttonSetupModel_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;

        //    try
        //    {
        //        Assembly assembly = Assembly.LoadFrom(Controller.Instance.AppConfig.SetupTemplateFileName);
        //        Slyce.ITemplate.IProject project = (Slyce.ITemplate.IProject)assembly.CreateInstance("Slyce.ITemplate.Project");
        //        object template = assembly.CreateInstance(project.Name + ".TemplateGen");

        //        //AppConfig.ActiveProjectPath = textBoxProjectPath.Text;

        //        Type type = template.GetType();
        //        MethodInfo[] methods = type.GetMethods();
        //        foreach (MethodInfo method in methods)
        //        {
        //            ParameterInfo[] parameters = method.GetParameters();

        //            if (parameters.Length == 1)
        //            {
        //                ParameterInfo parameter = parameters[0];
        //                Type paramType = parameter.ParameterType;

        //                foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
        //                {
        //                    if (paramType == typeof(ArchAngel.Providers.Database.Model.Database))
        //                    {
        //                        type.InvokeMember(method.Name, BindingFlags.InvokeMethod, null, null, GetParameters(method.Name, type, database));
        //                    }
        //                    foreach (ScriptObject scriptObject in database.ScriptObjects)
        //                    {
        //                        if (paramType == typeof(ScriptObject))
        //                        {
        //                            type.InvokeMember(method.Name, BindingFlags.InvokeMethod, null, null, GetParameters(method.Name, type, scriptObject));
        //                        }
        //                    }
        //                    foreach (ArchAngel.Providers.Database.Model.Table table in database.Tables)
        //                    {
        //                        if (paramType == typeof(ArchAngel.Providers.Database.Model.Table))
        //                        {
        //                            type.InvokeMember(method.Name, BindingFlags.InvokeMethod, null, null, GetParameters(method.Name, type, table));
        //                        }
        //                    }

        //                    foreach (ArchAngel.Providers.Database.Model.View view in database.Views)
        //                    {
        //                        if (paramType == typeof(ArchAngel.Providers.Database.Model.View))
        //                        {
        //                            type.InvokeMember(method.Name, BindingFlags.InvokeMethod, null, null, GetParameters(method.Name, type, view));
        //                        }
        //                    }

        //                    foreach (ArchAngel.Providers.Database.Model.StoredProcedure storedProcedure in database.StoredProcedures)
        //                    {
        //                        if (paramType == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
        //                        {
        //                            type.InvokeMember(method.Name, BindingFlags.InvokeMethod, null, null, GetParameters(method.Name, type, storedProcedure));
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        labelDatabaseFeedback.Text = "Setup Model Complete";
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message + "\n" + ex.InnerException);
        //        labelDatabaseFeedback.Text = ex.Message + "\n" + ex.InnerException;
        //    }
        //    Cursor = Cursors.Default;
        //}

        /// <summary>
        /// Refreshes the database structure and keeps user-changes if specified.
        /// </summary>
        /// <returns>Returns a description of any errors that occurred or alerts that the user needs to be aware of.</returns>
        private void ReloadDatabase()
        {
            try
            {
                StringBuilder sb = new StringBuilder(1000);
                MustSetDefaultValues = false;
                CallCrossThreadMethod(treeList, "ClearNodes", null);
                treeList.ClearNodes();
                Application.DoEvents();

                if (checkBoxKeepChanges.Checked)
                {
                    for (int i = 0; i < Controller.Instance.BllDatabase.Databases.Length; i++)
                    {
                        ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)Controller.Instance.BllDatabase.Databases[i];
                        bool isNewDatabase = (database.Tables.Length + database.Views.Length + database.StoredProcedures.Length) == 0;
                        string errors = Controller.Instance.BllDatabase.RefreshDatabase(database, i,
                            FormMain.ContentItemDatabaseSetup.GetListItems(FormMain.ContentItemDatabaseSetup.ListBoxTablePrefix),
                            FormMain.ContentItemDatabaseSetup.GetListItems(FormMain.ContentItemDatabaseSetup.ListBoxViewPrefix),
                            FormMain.ContentItemDatabaseSetup.GetListItems(FormMain.ContentItemDatabaseSetup.ListBoxStoredProcedurePrefix));

                        if (errors.Length > 0)
                        {
                            sb.Append(errors);
                        }

                        if (isNewDatabase)
                        {
                            MustSetDefaultValues = true;
                        }
                    }
                    //CallCrossThreadMethod(FormMain.ContentItemModelEditTreeView, "LoadTreeView", null);
                    //SetCrossThreadProperty(labelDatabaseFeedback, "Text", "Refresh Database Complete");
                    //FormMain.ContentItemModelEditTreeView.LoadTreeView();
                    //labelDatabaseFeedback.Text = "Refresh Database Complete";
                }
                else // Overwrite all changes by doing a fresh reload of the database
                {
                    for (int i = 0; i < Controller.Instance.BllDatabase.Databases.Length; i++)
                    {
                        ArchAngel.Providers.Database.Model.Database database = (ArchAngel.Providers.Database.Model.Database)Controller.Instance.BllDatabase.Databases[i];
                        Controller.Instance.BllDatabase.LoadNewDatabase(i, database.Name, database.DatabaseType, database.ConnectionString,
                            FormMain.ContentItemDatabaseSetup.GetListItems(FormMain.ContentItemDatabaseSetup.ListBoxTablePrefix),
                            FormMain.ContentItemDatabaseSetup.GetListItems(FormMain.ContentItemDatabaseSetup.ListBoxViewPrefix),
                            FormMain.ContentItemDatabaseSetup.GetListItems(FormMain.ContentItemDatabaseSetup.ListBoxStoredProcedurePrefix));

                        MustSetDefaultValues = true;
                    }
                    CallCrossThreadMethod(FormMain.ContentItemModelEditTreeView, "LoadTreeView", null);
                    SetCrossThreadProperty(labelDatabaseFeedback, "Text", "Load Database Complete");
                    //FormMain.ContentItemModelEditTreeView.LoadTreeView();
                    //labelDatabaseFeedback.Text = "Load Database Complete";
                }
                if (sb.Length > 0)
                {
                    MessageBox.Show(sb.ToString(), "Issues while refreshing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += Environment.NewLine + ex.InnerException.Message;
                }
                Controller.ReportError(ex);
                SetCrossThreadProperty(labelDatabaseFeedback, "Text", message);
            }
        }

        private void buttonRefreshDatabase_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step1;
                Controller.BusyPopulating = true;
                Cursor = Cursors.WaitCursor;
                labelDatabaseFeedback.Text = "Connecting to database...";
                CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step1;
                backgroundWorker1.RunWorkerAsync();
            }
            //Cursor = Cursors.Default;
        }

        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (CurrentBackgroundWorkerStep)
            {
                case BackGroundWorkerSteps.Step1:
                    ReloadDatabase();
                    break;
                case BackGroundWorkerSteps.Step2:
                    break;
                case BackGroundWorkerSteps.Step3:
                    CallCrossThreadMethod(FormMain.ContentItemModelEditTreeView, "LoadTreeView", null);
                    SetCrossThreadProperty(labelDatabaseFeedback, "Text", "Refresh Database Complete");
                    break;
                default:
                    throw new NotImplementedException("Not handled yet.");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Controller.ReportError(e.Error);
            }
            switch (CurrentBackgroundWorkerStep)
            {
                case BackGroundWorkerSteps.Step1:
                    if (MustSetDefaultValues)
                    {
                        SetCrossThreadProperty(labelDatabaseFeedback, "Text", "Setting default values...");

                        foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
                        {
                            Project.SetDefaultValues(database);
                        }
                    }
                    CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step3;
                    backgroundWorker1.RunWorkerAsync();
                    break;
                //case BackGroundWorkerSteps.Step2:
                //    CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step2;
                //    backgroundWorker1.RunWorkerAsync();
                //    break;
                case BackGroundWorkerSteps.Step3:
                    // Do nothing
                    break;
                default:
                    throw new NotImplementedException("Not handled yet.");
            }
            labelDatabaseFeedback.Text = "Finished";
            Controller.BusyPopulating = false;
            Controller.IsDirty = true;
            Cursor = Cursors.Default;
        }

        #region Cross Threading Methods

        private void SetObjectProperty(object obj, string propertyName, object val)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            pi.SetValue(obj, val, null);
        }

        private object CallObjectMethod(object obj, string methodName, object[] parameters)
        {
            Type type = obj.GetType();

            if (methodName.IndexOf(".") > 0)
            {
                string[] parts = methodName.Split('.');
                System.Reflection.PropertyInfo pi = type.GetProperty(parts[0]);

                if (pi != null)
                {
                    obj = pi.GetValue(obj, null);
                    type = obj.GetType();
                    methodName = parts[1];
                }
            }
            Type[] paramTypes = new Type[0];

            if (parameters != null)
            {
                paramTypes = new Type[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    paramTypes[i] = parameters[i].GetType();
                }
            }
            System.Reflection.MethodInfo mi = type.GetMethod(methodName, paramTypes);
            return mi.Invoke(obj, parameters);
        }

        private object CallCrossThreadMethod(object obj, string methodName, object[] parameters)
        {
            try
            {
                return this.Invoke(_delegateCallMethod, obj, methodName, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error calling CallCrossThreadMethod (Object: {0}, methodName: {1})", (obj != null ? obj.ToString() : "??"), methodName), ex);
            }
        }

        private void SetCrossThreadProperty(object obj, string propertyName, object val)
        {
            try
            {
                this.Invoke(_delegateSetProperty, obj, propertyName, val);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error calling SetCrossThreadProperty (Object: {0}, methodName: {1})", (obj != null ? obj.ToString() : "??"), propertyName), ex);
            }
        }

        #endregion

        private void AddEditorsToTreeview()
        {
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();

            repositoryItemButtonEdit1.Click += new EventHandler(repositoryItemButtonEdit1_Click);

            treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1, repositoryItemComboBox1, repositoryItemButtonEdit1 });
        }

        void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraEditors.ButtonEdit editButton = (DevExpress.XtraEditors.ButtonEdit)sender;
            //Slyce.ITemplate.IOption option = (Slyce.ITemplate.IOption)treeList.Selection[0].Tag;
            //FormObjectOptionEdit form = new FormObjectOptionEdit(option);

            //if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
            //{
            //    ArchAngel.Providers.Database.Model.ScriptObject parentScriptObject = (ArchAngel.Providers.Database.Model.ScriptObject)treeList.Selection[0].ParentNode.ParentNode.Tag;

            //    for (int i = 0; i < parentScriptObject.UserOptions.Count; i++)
            //    {
            //        if (parentScriptObject.UserOptions[i].Name == form.CurrentOption.VariableName)
            //        {
            //            ((ArchAngel.Providers.Database.Model.ScriptObject)treeList.Selection[0].ParentNode.ParentNode.Tag).UserOptions[i].Value = Convert.ChangeType(form.CurrentOption.DefaultValue, form.CurrentOption.VarType);
            //            //parentScriptObject.UserOptions[i].Value = Convert.ChangeType(form.CurrentOption.DefaultValue, form.CurrentOption.VarType);
            //            break;
            //        }
            //    }
            //    treeList.Selection[0].Tag = form.CurrentOption;
            //    treeList.Selection[0].SetValue(1, form.CurrentOption.DefaultValue);
            //}
        }

        private void treeList_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Node.Tag.GetType().Name.IndexOf("Option") >= 0 && e.Column.VisibleIndex == 1)
            {
                Slyce.ITemplate.IOption option = (Slyce.ITemplate.IOption)e.Node.Tag;
                e.RepositoryItem = repositoryItemButtonEdit1;

                //object obj = e.Node.GetValue(0);

                //switch (option.VarType.ToLower())
                //{
                //    case "string":
                //        e.RepositoryItem = repo
                //}
                //if (obj != null)
                //{
                //    switch (obj.ToString())
                //    {
                //        case "Category":
                //            e.RepositoryItem = repositoryImageComboBox1;
                //            break;
                //        case "Supplier":
                //            e.RepositoryItem = repositoryItemComboBox1;
                //            break;
                //        case "Unit Price":
                //            e.RepositoryItem = repositoryItemCalcEdit1;
                //            break;
                //        case "Units in Stock":
                //            e.RepositoryItem = repositoryItemSpinEdit1;
                //            break;
                //        case "Discontinued":
                //            e.RepositoryItem = repositoryItemCheckEdit1;
                //            break;
                //        case "Last Order":
                //            e.RepositoryItem = repositoryItemDateEdit1;
                //            break;
                //        case "Relevance":
                //            e.RepositoryItem = repositoryItemProgressBar1;
                //            break;
                //        case "Phone":
                //            e.RepositoryItem = repositoryItemTextEdit1;
                //            break;
                //    }
                //}
            }

        }

        private void btnResetDefaultValues2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset ALL default values and discard any changes you have made?", "Reset Default Values", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                btnResetDefaultValues.Enabled = false;
                Controller.MainForm.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
                {
                    SetCrossThreadProperty(labelDatabaseFeedback, "Text", "Setting default values...");
                    Project.SetDefaultValues(database);
                }
                LoadTreeView();
                SetCrossThreadProperty(labelDatabaseFeedback, "Text", "");
                btnResetDefaultValues.Enabled = true;
                Controller.MainForm.Cursor = Cursors.Default;
                MessageBox.Show("Finished");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder(100);
            //bool isInvalid = false;

            //foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            //{
            //    string tempString;

            //    if (!database.IsValid(true, out tempString))
            //    {
            //        isInvalid = true;
            //        sb.AppendLine(tempString);
            //    }
            //}
            Controller.MainForm.Cursor = Cursors.WaitCursor;
            treeList.BeginUpdate();
            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                database.SnapshotMode = true;
            }
            ProcessTreeValidity(treeList.Nodes);
            foreach (ArchAngel.Providers.Database.Model.Database database in Controller.Instance.BllDatabase.Databases)
            {
                database.SnapshotMode = false;
            }
            treeList.EndUpdate();
            Controller.MainForm.Cursor = Cursors.Default;
            //if (isInvalid)
            //{
            //    MessageBox.Show(sb.ToString(), "Invalid Data");
            //}
            //else
            //{
            //    MessageBox.Show("All valid");
            //}
        }

        private void ProcessTreeValidity(TreeListNodes nodesToProcess)
        {
            foreach (TreeListNode node in nodesToProcess)
            {
                if (GetNodeCheckState(node) == CheckState.Unchecked && node.ImageIndex == GreenBulletValue)
                {
                    continue;
                }
                ValidateNode(node);
                ProcessTreeValidity(node.Nodes);
            }
        }

        private void ValidateNode(TreeListNode node)
        {
            string failReason = null;
            int imageIndex = node.ImageIndex;

            if (imageIndex == GreenBulletValue ||
                imageIndex == RedBulletValue)
            {
                node.ImageIndex = node.SelectImageIndex = GreenBulletValue;
            }

            if (node.Tag.GetType().BaseType == ScriptBaseType)
            {
                if (!((ScriptBase)node.Tag).IsValid(false, out failReason))
                {
                    if (node.ImageIndex != RedBulletValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = RedBulletValue;
                    }
                    EnsureParentNodesAreRed(node);
                }
                else
                {
                    if (node.ImageIndex != GreenBulletValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = GreenBulletValue;
                    }
                }
            }
            else if (node.Tag.GetType().BaseType == ScriptObjectType)
            {
                if (!((ScriptObject)node.Tag).IsValid(false, out failReason))
                {
                    if (node.ImageIndex != RedBulletValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = RedBulletValue;
                    }
                    EnsureParentNodesAreRed(node);
                }
                else
                {
                    if (node.ImageIndex != GreenBulletValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = GreenBulletValue;
                    }
                }
            }
            else if (node.Tag.GetType().BaseType == RelationshipType)
            {
                if (!((Relationship)node.Tag).IsValid(false, out failReason))
                {
                    if (node.ImageIndex != RedBulletValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = RedBulletValue;
                    }
                    EnsureParentNodesAreRed(node);
                }
                else
                {
                    if (node.ImageIndex != GreenBulletValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = GreenBulletValue;
                    }
                }
            }
            else if (node.Tag.GetType() == DatabaseType)
            {
                if (!((ArchAngel.Providers.Database.Model.Database)node.Tag).IsValid(false, out failReason))
                {
                    if (node.ImageIndex != RedDatabaseValue)
                    {
                        node.ImageIndex = node.SelectImageIndex = RedDatabaseValue;
                    }
                    EnsureParentNodesAreRed(node);
                }
                else
                {
                    if (node.ImageIndex != (int)Images.Database)
                    {
                        node.ImageIndex = node.SelectImageIndex = (int)Images.Database;
                    }
                }
            }
            else if (node.Tag.GetType().Name == "Column[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Column;
            }
            else if (node.Tag.GetType().Name == "Filter[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Filter;
            }
            else if (node.Tag.GetType().Name == "Index[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Index;
            }
            else if (node.Tag.GetType().Name == "Key[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Key;
            }
            else if (node.Tag.GetType().Name == "View[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Table;
            }
            else if (node.Tag.GetType().Name == "StoredProcedure[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.StoredProcedure;
            }
            else if (node.Tag.GetType().Name == "Relationship[]" ||
           node.Tag.GetType().Name == "OneToOneRelationship[]" ||
           node.Tag.GetType().Name == "OneToManyRelationship[]" ||
           node.Tag.GetType().Name == "ManyToManyRelationship[]" ||
           node.Tag.GetType().Name == "ManyToOneRelationship[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Relationship;
            }
            else if (node.Tag.GetType().Name == "Table[]")
            {
                node.ImageIndex = node.SelectImageIndex = (int)Images.Table;
            }
            else
            {
                string gg = "";
            }
            if (!string.IsNullOrEmpty(failReason))
            {
                node.SetValue(2, failReason);
            }
        }

        private void EnsureParentNodesAreRed(TreeListNode node)
        {
            TreeListNode parentNode = node.ParentNode;

            while (parentNode != null)
            {
                //if (parentNode.ImageIndex != RedBulletValue)
                //if (parentNode.ImageIndex == GreenBulletValue)
                //{
                //    parentNode.ImageIndex = node.SelectImageIndex = RedBulletValue;
                //}
                switch (parentNode.ImageIndex)
                {
                    case GreenBulletValue:
                        parentNode.ImageIndex = node.SelectImageIndex = RedBulletValue;
                        break;
                    case (int)Images.Column:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Column;
                        break;
                    case (int)Images.Database:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Database;
                        break;
                    case (int)Images.Filter:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Filter;
                        break;
                    case (int)Images.Index:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Index;
                        break;
                    case (int)Images.Key:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Key;
                        break;
                    case (int)Images.Option:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Option;
                        break;
                    case (int)Images.Relationship:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Relationship;
                        break;
                    case (int)Images.StoredProcedure:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_StoredProcedure;
                        break;
                    case (int)Images.Table:
                        parentNode.ImageIndex = node.SelectImageIndex = (int)Images.Red_Table;
                        break;
                }
                parentNode = parentNode.ParentNode;
            }
        }


    }
}
