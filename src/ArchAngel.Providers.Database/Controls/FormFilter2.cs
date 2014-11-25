using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.Controls.ContentItems;
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Controls
{
    public partial class FormFilter2 : Form
    {
        public enum ScreenNames
        {
            Start,
            Columns,
            ReturnOrder,
            WhereClause,
            Summary,
            Nothing
        }
        public enum ReturnTypes
        {
            Collection,
            Single,
            Any
        }

        private ScriptObject _parent;
        private static Filter _existingFilter;
        private static Filter _newFilter;
    	private static readonly Dictionary<ScreenNames, ContentItem> _contentItems = new Dictionary<ScreenNames, ContentItem>();
        private static readonly List<ContentItem> _orderedContentItems = new List<ContentItem>();
        private ContentItem _currentContentItem;
        private bool FinishClicked = false;
        public bool IsAddingNewFilter = false;
        internal ReturnTypes ReturnType = ReturnTypes.Any;
        internal List<Column> RequiredFilterColumns = new List<Column>();

        /// <summary>
        /// Constructor for when called from Relationship wizard.
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="parent"></param>
        /// <param name="returnType"></param>
        /// <param name="requiredFilterColumns"></param>
        public FormFilter2(Form parentForm, ScriptObject parent, ReturnTypes returnType, List<Column> requiredFilterColumns)
        {
            Init(parentForm, parent, returnType, requiredFilterColumns);
        }

        /// <summary>
        /// Constructor for creating a new filter.
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="parent"></param>
        public FormFilter2(Form parentForm, ScriptObject parent)
        {
            Init(parentForm, parent, ReturnTypes.Any, new List<Column>());
        }

        /// <summary>
        /// Only gets called when adding new filters. Initialises variables and screens.
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="parent"></param>
        /// <param name="returnType"></param>
        /// <param name="requiredFilterColumns"></param>
        private void Init(Form parentForm, ScriptObject parent, ReturnTypes returnType, List<Column> requiredFilterColumns)
        {
            // Reset the filters because they are static
            _existingFilter = _newFilter = null;
            Owner = parentForm;
            //this.Owner.ParentForm = parenForm;
            Owner.Cursor = Cursors.WaitCursor;
            InitializeComponent();
            RequiredFilterColumns = requiredFilterColumns;
            ReturnType = returnType;
            BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading1.Text = "";
            Text = "Add New Filter";
            IsAddingNewFilter = true;
            _parent = parent;
        	Interfaces.Events.ShadeMainForm();
            ResizeControls();

            if (requiredFilterColumns.Count > 0)
            {
                foreach (Column column in requiredFilterColumns)
                {
                    Filter.FilterColumn filterColumn = new Filter.FilterColumn(column, "And", "=", column.Alias);
                    TheFilter.AddFilterColumn(filterColumn);
                }
                SetNewFilterName();
            }
            InitContentItems();
            Populate();
            Owner.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Constructor for editing an existing filter.
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="filter"></param>
        public FormFilter2(Form parentForm, Filter filter)
        {
            // Reset the filters because they are static
            _existingFilter = _newFilter =null;
            Owner = parentForm;
            Owner.Cursor = Cursors.WaitCursor;
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = filter.Parent;
            _existingFilter = filter;
        	ucHeading1.Text = "";
            Text = "Edit Filter " + _existingFilter.Name;
            IsAddingNewFilter = false;
            Interfaces.Events.ShadeMainForm();
            InitContentItems();
            Populate();
            Owner.Cursor = Cursors.Default;
        }

        /// <summary>
        /// The final version of the filter. This should only be called from the calling form, not anywhere inside the wizard.
        /// </summary>
        public static Filter Filter
        {
            get
            {
                if (_existingFilter != null)
                {
                    return _existingFilter;
                }
                return _newFilter;
            }
        }

        internal Filter TheFilter
        {
            get
            {
                if (_existingFilter != null)
                {
                    return _existingFilter;
                }
            	if (_newFilter == null)
            	{
            		if (_parent == null)
            		{
            			throw new InvalidOperationException("_parent shouldn't be null for a new filter.");
            		}
            		_newFilter = new Filter("NewFilter", true, _parent, true, true, false, "", null);
            		_newFilter.Alias = "NewFilter";
            	}
            	return _newFilter;
            }
        }

        private void SetNewFilterName()
        {
            string filterName = "NewFilter";

            if (ReturnType != ReturnTypes.Any)
            {
                // This has been called from the Relationship editor
                if (ReturnType == ReturnTypes.Collection)
                {
                    filterName = string.Format("Get{0}By", _parent.AliasPlural);
                }
                else
                {
                    filterName = string.Format("Get{0}By", _parent.Alias);
                }
            	for (int i = 0; i < RequiredFilterColumns.Count; i++)
                {
                    if (i > 0)
                    {
                        filterName += "And";
                    }
                    filterName += RequiredFilterColumns[i].Alias;
                }
            }
            TheFilter.Name = TheFilter.Alias = filterName;
        }

        private void Populate()
        {
            LoadWizardPage(ScreenNames.Columns, false);
        }

        private void FormFilter2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Interfaces.Events.UnShadeMainForm();
        }

        private void InitContentItems()
        {
            _contentItems.Clear();
            _orderedContentItems.Clear();

        	// Columns
            ContentItem newContentItem = new FilterWizard.ucFilterColumns(this);
            _contentItems.Add(ScreenNames.Columns, newContentItem);
            _orderedContentItems.Add(newContentItem);

            // ReturnOrder
            newContentItem = new FilterWizard.ucFilterReturnOrder(this);
            _contentItems.Add(ScreenNames.ReturnOrder, newContentItem);
            _orderedContentItems.Add(newContentItem);

            // WhereClause
            newContentItem = new FilterWizard.ucFilterWhere(this);
            _contentItems.Add(ScreenNames.WhereClause, newContentItem);
            _orderedContentItems.Add(newContentItem);

            // Start
            newContentItem = new FilterWizard.ucFilterStart(this);
            _contentItems.Add(ScreenNames.Start, newContentItem);
            _orderedContentItems.Add(newContentItem);

            // Summary
            newContentItem = new FilterWizard.ucFilterSummary(this);
            _contentItems.Add(ScreenNames.Summary, newContentItem);
            _orderedContentItems.Add(newContentItem);

            //// Add event handling code for each ContentItem
            //foreach (ContentItems.ContentItem.ContentItemNames key in _contentItems.Keys)
            //{
            //    _contentItems[key].StatusChanged += new ContentItems.StatusChangedDelegate(ContentItem_StatusChanged);
            //}

            //_contentItems.Add(ScreenNames.Columns, new FilterWizard.ucFilterColumns(this));//  ContentItems.Options("Options", "Step 4 - Options", "Specify what options you want the generation engine to use."));
            //_contentItems.Add(ScreenNames.ReturnOrder, new FilterWizard.ucFilterReturnOrder(this));
            //_contentItems.Add(ScreenNames.Start, new FilterWizard.ucFilterStart(this));
            //_contentItems.Add(ScreenNames.Summary, new FilterWizard.ucFilterSummary(this));
            //_contentItems.Add(ScreenNames.WhereClause, new FilterWizard.ucFilterWhere(this));
        }

        public void LoadWizardPage(ScreenNames screenName)
        {
            LoadWizardPage(screenName, false);
        }

        public void LoadWizardPage(ScreenNames screenName, bool calledFromClickEvent)
        {
			try
			{
				Cursor = Cursors.WaitCursor;
				Slyce.Common.Utility.SuspendPainting(panelContent);
				panelContent.Controls.Clear();
				ArchAngel.Interfaces.Controls.ContentItems.ContentItem nextContentItem = _contentItems[screenName];

				_currentContentItem = nextContentItem;
				Slyce.Common.Utility.SuspendPainting(_currentContentItem);

				if (_currentContentItem == null)
				{
					Slyce.Common.Utility.ResumePainting(panelContent);
					Slyce.Common.Utility.ResumePainting(_currentContentItem);
					return;
				}
				_currentContentItem.OnDisplaying();
				panelContent.Controls.Clear();
				panelContent.Controls.Add(_currentContentItem);
				_currentContentItem.Dock = DockStyle.Fill;
				panelTop.Visible = _currentContentItem.DisplayTopPanel;
				buttonBack.Visible = _currentContentItem.HasPrev;
				buttonBack.BringToFront();
				buttonNext.Enabled = _currentContentItem.HasNext || _currentContentItem.HasFinish;
				labelPageHeader.Text = _currentContentItem.PageHeader;
				labelPageDescription.Text = _currentContentItem.PageDescription;
				buttonNext.Text = _currentContentItem.HasFinish ? "&Finish" : "&Next >";

				if (!string.IsNullOrEmpty(_currentContentItem.NextText))
				{
					buttonNext.Text = _currentContentItem.NextText;
				}
				ResizeControls();
				_currentContentItem.Focus();
			}
			finally
			{
				Slyce.Common.Utility.ResumePainting(panelContent);
				Slyce.Common.Utility.ResumePainting(_currentContentItem);
				Cursor = Cursors.Default;
			}
        }

        private void FormFilter2_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void ResizeControls()
        {
            panelContent.Left = 0;
            panelContent.Top = panelTop.Bottom;
            panelContent.Width = ClientSize.Width;
            panelContent.Height = ucHeading1.Top - panelTop.Bottom;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            ContentItem contentItem = (ContentItem)panelContent.Controls[0];

            if (contentItem.HasFinish)
            {
                FinishClicked = true;
                Close();
                return;
            }
            if (!contentItem.Next())
            {
                return;
            }
            for (int i = 0; i < _orderedContentItems.Count; i++)
            {
                if (_orderedContentItems[i] == contentItem)
                {
                    // If we're at the last item, do nothing
                    if (i < _orderedContentItems.Count - 1)
                    {
                        foreach (ScreenNames key in _contentItems.Keys)
                        {
                            if (_contentItems[key] == _orderedContentItems[i + 1])
                            {
                                if (contentItem.ValidateControls())
                                {
                                    panelContent.Controls.Clear();
                                    LoadWizardPage(key);
                                }
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            //ContentItems.ContentItem contentItem = (ContentItems.ContentItem)panelContent.Controls[0];

            //if (contentItem.HasFinish)
            //{
            //    FinishClicked = true;
            //    this.Close();
            //    return;
            //}
            //ScreenNames newContentItemName = (ScreenNames)Enum.Parse(typeof(ScreenNames), contentItem.Next());

            //if (newContentItemName != ScreenNames.Nothing)
            //{
            //    //if (contentItem.ValidateControls())
            //    //{
            //        //panelContent.Controls.Clear();
            //        LoadWizardPage(newContentItemName);
            //    //}
            //}
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ContentItem contentItem = (ContentItem)panelContent.Controls[0];

            if (!contentItem.Back())
            {
                return;
            }
            for (int i = 0; i < _orderedContentItems.Count; i++)
            {
                if (_orderedContentItems[i] == contentItem)
                {
                    // If we're at the first item, do nothing
                    if (i > 0)
                    {
                        foreach (ScreenNames key in _contentItems.Keys)
                        {
                            if (_contentItems[key] == _orderedContentItems[i - 1])
                            {
                                if (contentItem.ValidateControls())
                                {
                                    panelContent.Controls.Clear();
                                    LoadWizardPage(key);
                                }
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void FormFilter2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = FinishClicked ? DialogResult.OK : DialogResult.Cancel;

            if (FinishClicked)
            {
                foreach (ScreenNames key in _contentItems.Keys)
                {
                    ArchAngel.Interfaces.Controls.ContentItems.ContentItem item = _contentItems[key];

                    if (!item.Save())
                    {
                        MessageBox.Show("Problem saving data.");
                        e.Cancel = true;
                        return;
                    }
                }
            }
            //else
            //{
            //    // The user didn't click finish. They just clicked close, so they don't want to save what they have done, so revert to the original filter.
            //    _filter = _realFilter;
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}