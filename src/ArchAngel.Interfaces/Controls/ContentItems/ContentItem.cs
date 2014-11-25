using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// References to ArchAngel specific libraries
//using ArchAngel.Providers.Database.Model;
//using ArchAngel.Providers.Database.BLL;

namespace ArchAngel.Interfaces.Controls.ContentItems
{
    #region Delegates
    public delegate void StatusChangedDelegate(ContentItems.ContentItem sender);
    public delegate void LeavingDelegate();
    public delegate void DisplayingDelegate();
    #endregion

    public partial class ContentItem : UserControl
    {
        #region Events
        public event StatusChangedDelegate StatusChanged;
        /// <summary>
        /// Occurs when the user navigates to this screen, and it is displayed to the user.
        /// </summary>
        public event DisplayingDelegate Displaying;
        #endregion

        public bool DisplayImage = true;
        public bool DisplayTopPanel = true;
        private bool _hasNext = true;
        private bool _hasPrev = true;
        private bool _hasFinish = false;
        private string _title = "Title not set";
        private string _pageHeader = "Default Page Header";
        private string _pageDescription = "Description not set";
        public Color ButtonForeColor = Color.Black;
        public string HelpPage = "404.htm";
        public string HelpFile = "ArchAngel Help.chm";
        public string NextText = "";
        public bool ValidatingControls = false;
        private Color _NavBarIconTransparentColor = Color.Empty;
        private Image _NavBarIcon;
        private bool _EnterKeyTriggersNext = true;

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

        /// <summary>
        /// The icon to be displayed in ArchAngel Workbench the navigation pane.
        /// </summary>
        [Browsable(true)]
        public Image NavBarIcon
        {
            get { return _NavBarIcon; }
            set { _NavBarIcon = value; }
        }

        /// <summary>
        /// The transparent color to be applied to the NavBarIcon.
        /// </summary>
        public Color NavBarIconTransparentColor
        {
            get { return _NavBarIconTransparentColor; }
            set { _NavBarIconTransparentColor = value; }
        }

        /// <summary>
        /// Gets raised when properties that are displayed by the parent container are changed. This gives the
        /// parent container the opportunity to listen to these events and apply the changes where appropriate.
        /// </summary>
        private void RaiseStatusChangedEvent()
        {
            if (StatusChanged != null)
            {
                StatusChanged(this);
            }
        }

        /// <summary>
        /// Raises the Displaying event.
        /// </summary>
        public virtual void OnDisplaying()
        {
            if (Displaying != null)
            {
                Displaying();
            }
        }

        /// <summary>
        /// Gets or sets whether the 'Enter' key triggers the 'Next' button-click.
        /// </summary>
        public bool EnterKeyTriggersNext 
        {
            get { return _EnterKeyTriggersNext; }
            set { _EnterKeyTriggersNext = value; }
        }

        /// <summary>
        /// Gets or sets whether this ContentItem has a 'Next' button.
        /// </summary>
        public bool HasNext
        {
            get { return _hasNext; }
            set
            {
                _hasNext = value;
                RaiseStatusChangedEvent();
            }
        }

        /// <summary>
        /// Gets or sets whether this ContentItem has a 'Back' button.
        /// </summary>
        public bool HasPrev
        {
            get { return _hasPrev; }
            set
            {
                _hasPrev = value;
                RaiseStatusChangedEvent();
            }
        }

        /// <summary>
        /// Gets or sets whether this ContentItem has a 'Finish' button.
        /// </summary>
        public bool HasFinish
        {
            get { return _hasFinish; }
            set
            {
                _hasFinish = value;
                RaiseStatusChangedEvent();
            }
        }

        /// <summary>
        /// Gets or sets the Title of this ContentItem which will be displayed as a heading.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaiseStatusChangedEvent();
            }
        }

        /// <summary>
        /// Gets or sets the heading text that will be displayed above this ContentItem.
        /// </summary>
        public string PageHeader
        {
            get { return _pageHeader; }
            set
            {
                _pageHeader = value;
                RaiseStatusChangedEvent();
            }
        }

        /// <summary>
        /// Gets or sets the description that will be displayed above this ContentItem, below the PageHeader text.
        /// </summary>
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
        /// Event handler that gets wired up inside ArchAngel Workbench, allowing you to subscribe to all data changes application-wide.
        /// </summary>
        /// <param name="type">The type of object that has been affected.</param>
        /// <param name="method">The name of the property that has been changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public virtual void OnDataChanged(Type type, System.Reflection.MethodInfo method, object oldValue, object newValue)
        {
        }

        /// <summary>
        /// Gets called when the user clicks the 'Next' button. Return false to cancel the action 
        /// and prevent the previous screen from being loaded.
        /// </summary>
        /// <returns></returns>
        public virtual bool Next()
        {
            return true;
        }

        /// <summary>
        /// Gets called when the user clicks the 'Back' button. Return false to cancel the action 
        /// and prevent the previous screen from being loaded.
        /// </summary>
        /// <returns></returns>
        public virtual bool Back()
        {
            return true;
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

                if (!ValidateControl(childControl))
                {
                    return false;
                }
                if (!Validate())
                {
                    return false;
                }
            }
            return Validate();
        }

        public virtual void Clear()
        {
        }

        public virtual void ClearErrorProvider()
        {

        }

        public virtual void ProjectLoadedEventHandler()
        {
        }

        public virtual void TemplateLoadedEventHandler()
        {
        }

    }
}
