using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.BLL;

namespace ArchAngel.Workbench.ContentItems
{
    #region Delegates
    public delegate void StatusChangedDelegate(ContentItems.ContentItem sender);
    #endregion

    public partial class ContentItem : UserControl
    {
        #region Events
        public event StatusChangedDelegate StatusChanged;
        #endregion

        public bool DisplayImage = true;
        public bool DisplayTopPanel = true;
        private bool _hasNext = false;
        private bool _hasPrev = false;
        private bool _hasFinish = false;
        private string _title = "Default Title";
        private string _pageHeader = "Default Page Header";
        private string _pageDescription = "Default description.";
        public Color ButtonForeColor = Color.Black;
        public string HelpFileName = "404.htm";
        public string NextText = "";
        public bool ValidatingControls = false;

        public enum ContentItemNames
        {
            SetupProject,
            SetupDatabase,
            EditModel,
            Options,
            Generation,
            Merge,
            Nothing,
            PreActions,
            PostActions
        }

        public enum OptionsItemNames
        {
            General,
            Formatting,
            Nothing
        }

        public ContentItem()
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }

        public ContentItem(string title, string pageHeader, string pageDescription)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            Title = title;
            PageHeader = pageHeader;
            PageDescription = pageDescription;
        }

        protected void RaiseStatusChangedEvent()
        {
            if (StatusChanged != null)
            {
                StatusChanged(this);
            }
        }

        public bool HasNext
        {
            get { return _hasNext; }
            set
            {
                _hasNext = value;
                RaiseStatusChangedEvent();
            }
        }

        public bool HasPrev
        {
            get { return _hasPrev; }
            set
            {
                _hasPrev = value;
                RaiseStatusChangedEvent();
            }
        }

        public bool HasFinish
        {
            get { return _hasFinish; }
            set
            {
                _hasFinish = value;
                RaiseStatusChangedEvent();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaiseStatusChangedEvent();
            }
        }

        public string PageHeader
        {
            get { return _pageHeader; }
            set
            {
                _pageHeader = value;
                RaiseStatusChangedEvent();
            }
        }

        public string PageDescription
        {
            get { return _pageDescription; }
            set
            {
                _pageDescription = value;
                RaiseStatusChangedEvent();
            }
        }

        /// <summary>
        /// Is called when the ContentItem is being displayed due to a user action (clicking Next, Back or menu item).
        /// </summary>
        public virtual void IsDisplaying()
        {
        }

        /// <summary>
        /// Name of next control type, from enum.
        /// </summary>
        /// <returns></returns>
        public virtual bool Next()
        {
            return true;// ContentItemNames.Nothing.ToString();
        }

        /// <summary>
        /// Previous control type, from enum
        /// </summary>
        /// <returns></returns>
        public virtual bool Back()
        {
            return true;// ContentItemNames.Nothing.ToString();
        }

        protected Object[] GetParameters(string methodName, Type type)
        {
            return GetParameters(methodName, type, null);
        }

        protected Object[] GetParameters(string methodName, Type type, object obj)
        {
            MethodInfo method = type.GetMethod(methodName);
            ParameterInfo[] parameters = method.GetParameters();
            System.Collections.ArrayList aryParameters = new System.Collections.ArrayList();

            foreach (ParameterInfo parameter in parameters)
            {
                Type paramType = parameter.ParameterType;

                if (paramType == typeof(ArchAngel.Providers.Database.Model.Database[]))
                {
                    aryParameters.Add(Controller.Instance.BllDatabase.Databases);
                }
                else if (paramType == typeof(Column) ||
                    paramType == typeof(ArchAngel.Providers.Database.Model.Database) ||
                    paramType == typeof(MapColumn) ||
                    paramType == typeof(Filter) ||
                    paramType == typeof(Index) ||
                    paramType == typeof(Key) ||
                    paramType == typeof(ManyToManyRelationship) ||
                    paramType == typeof(ManyToOneRelationship) ||
                    paramType == typeof(OneToManyRelationship) ||
                    paramType == typeof(OneToOneRelationship) ||
                    paramType == typeof(Relationship) ||
                    paramType == typeof(ScriptBase) ||
                    paramType == typeof(ScriptObject) ||
                    paramType == typeof(ArchAngel.Providers.Database.Model.StoredProcedure) ||
                    paramType == typeof(ArchAngel.Providers.Database.Model.Table) ||
                    paramType == typeof(ArchAngel.Providers.Database.Model.View))
                {
                    aryParameters.Add(obj);
                }
                else if (paramType == typeof(AppConfig))
                {
                    aryParameters.Add(Controller.Instance.AppConfig);
                }
                else
                {
                    throw new Exception("Parameter " + parameter.ParameterType + " not supported");
                }
            }
            return aryParameters.ToArray();
        }

        /// <summary>
        /// Saves current state.
        /// </summary>
        /// <returns>Returns true if save was successful, false otherwise.</returns>
        public virtual bool Save()
        {
            return true;
        }

        public virtual bool ValidateControls()
        {
            ValidatingControls = true;

            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control control = this.Controls[i];
                //control.Focus();

                if (!ValidateControl(control))
                {
                    return false;
                }
            }
            ValidatingControls = false;
            return true;
        }

        protected bool ValidateControl(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                Control childControl = control.Controls[i];
                //childControl.Focus();

                if (!ValidateControl(childControl))
                {
                    return false;
                }

                if (!Validate())
                {
                    return false;
                }
            }
            //control.Focus();
            return Validate();
        }

        public virtual void ClearErrorProvider()
        {

        }
    }
}
