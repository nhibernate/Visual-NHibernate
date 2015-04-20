using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.BLL;

namespace ArchAngel.Workbench
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

        ScriptObject _parent;
        Filter _filter;
        ScriptObject[] _scriptObjects;
        //private Dictionary<ScreenNames, ContentItems.ContentItem> _contentItems = new Dictionary<ScreenNames, ContentItems.ContentItem>();
        private static Dictionary<ScreenNames, ArchAngel.Interfaces.Controls.ContentItems.ContentItem> _contentItems = new Dictionary<ScreenNames, ArchAngel.Interfaces.Controls.ContentItems.ContentItem>();
        private static List<ArchAngel.Interfaces.Controls.ContentItems.ContentItem> _orderedContentItems = new List<ArchAngel.Interfaces.Controls.ContentItems.ContentItem>();
        private ArchAngel.Interfaces.Controls.ContentItems.ContentItem _currentContentItem;
        private bool FinishClicked = false;
        public bool IsAddingNewFilter = false;

        public FormFilter2(Form parentForm, ScriptObject parent, ScriptObject[] scriptObjects)
        {
            this.Owner = parentForm;
            //this.Owner.ParentForm = parenForm;
            this.Owner.Cursor = Cursors.WaitCursor;
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading1.Text = "";
            this.Text = "Add New Filter";
            IsAddingNewFilter = true;
            _parent = parent;
            _scriptObjects = scriptObjects;
            Controller.ShadeMainForm();
            ResizeControls();
            InitContentItems();
            Populate();
            this.Owner.Cursor = Cursors.Default;
        }

        public FormFilter2(Form parentForm, Filter filter, ScriptObject[] scriptObjects)
        {
            this.Owner = parentForm;
            this.Owner.Cursor = Cursors.WaitCursor;
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;

            _parent = filter.Parent;
            _filter = filter;
            _scriptObjects = scriptObjects;
            ucHeading1.Text = "";
            this.Text = "Edit Filter " + _filter.Name;
            IsAddingNewFilter = false;
            Controller.ShadeMainForm();
            InitContentItems();
            Populate();
            this.Owner.Cursor = Cursors.Default;
        }

        public Filter Filter
        {
            get
            {
                if (_filter == null)
                {
                    if (_parent == null)
                    {
                        throw new InvalidOperationException("_parent shouldn't be null for a new filter.");
                    }
                    _filter = new Filter("NewFilter", true, _parent, true, true, false, "", null);
                    _filter.Alias = "NewFilter";
                }
                return _filter;
            }
        }

        private void Populate()
        {
            LoadWizardPage(ScreenNames.Columns, false);
        }

        private void FormFilter2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private void InitContentItems()
        {
            _contentItems.Clear();
            _orderedContentItems.Clear();

            ArchAngel.Interfaces.Controls.ContentItems.ContentItem newContentItem;
            int stepNum = 1;

            // Columns
            newContentItem = new FilterWizard.ucFilterColumns(this);
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
            Cursor = Cursors.WaitCursor;
            Slyce.Common.Utility.SuspendPainting(panelContent.Handle);
            panelContent.Controls.Clear();
            ArchAngel.Interfaces.Controls.ContentItems.ContentItem nextContentItem = _contentItems[screenName];

            //switch (screenName)
            //{
            //    case ScreenNames.Columns:
            //        if (!calledFromClickEvent)
            //        {
            //            sequentialNavBar.ClickButton("Project Details");
            //        }
            //        pictureHeading.Image = imageListHeading.Images[0];
            //        break;
            //    case ContentItems.ContentItem.ContentItemNames.SetupDatabase:
            //        ShowTaskHelp(TaskHelpTypes.Database);

            //        if (!calledFromClickEvent)
            //        {
            //            sequentialNavBar.ClickButton("Database");
            //        }
            //        pictureHeading.Image = imageListHeading.Images[1];
            //        break;
            //    case ContentItems.ContentItem.ContentItemNames.EditModel:
            //        ShowTaskHelp(TaskHelpTypes.Model);

            //        if (!calledFromClickEvent)
            //        {
            //            sequentialNavBar.ClickButton("Object Model");
            //        }
            //        pictureHeading.Image = imageListHeading.Images[2];
            //        break;
            //    case ContentItems.ContentItem.ContentItemNames.Options:
            //        ShowTaskHelp(TaskHelpTypes.Options);

            //        if (!calledFromClickEvent)
            //        {
            //            sequentialNavBar.ClickButton("Options");
            //        }
            //        pictureHeading.Image = imageListHeading.Images[3];
            //        break;
            //    case ContentItems.ContentItem.ContentItemNames.Generation:
            //        ShowTaskHelp(TaskHelpTypes.Generation);

            //        if (!calledFromClickEvent)
            //        {
            //            sequentialNavBar.ClickButton("Analysis");
            //        }
            //        pictureHeading.Image = imageListHeading.Images[4];
            //        break;
            //    case ContentItems.ContentItem.ContentItemNames.Merge:
            //        ShowTaskHelp(TaskHelpTypes.Merge);

            //        if (!calledFromClickEvent)
            //        {
            //            sequentialNavBar.ClickButton("Generation");
            //        }
            //        pictureHeading.Image = imageListHeading.Images[4];
            //        break;
            //    default:
            //        throw new NotImplementedException("Not coded yet: " + contentItemName.ToString());
            //}
            _currentContentItem = nextContentItem;
            Slyce.Common.Utility.SuspendPainting(_currentContentItem.Handle);

            if (_currentContentItem == null)
            {
                return;
            }
            _currentContentItem.IsDisplaying();
            panelContent.Controls.Clear();
            panelContent.Controls.Add(_currentContentItem);
            _currentContentItem.Dock = DockStyle.Fill;
            //this.BackColor = _currentContentItem.BackColor;
            //pnlImageBackground.Visible = _currentContentItem.DisplayImage;
            panelTop.Visible = _currentContentItem.DisplayTopPanel;
            //headingContentTitle.Visible = !_currentContentItem.DisplayTopPanel;
            buttonBack.Visible = _currentContentItem.HasPrev;
            buttonBack.BringToFront();
            buttonNext.Enabled = _currentContentItem.HasNext || _currentContentItem.HasFinish;
            //headingContentTitle.Text = _currentContentItem.Title;
            //headingContentTitle.Refresh();
            labelPageHeader.Text = _currentContentItem.PageHeader;
            labelPageDescription.Text = _currentContentItem.PageDescription;
            buttonNext.Text = _currentContentItem.HasFinish ? "&Finish" : "&Next >";

            if (!string.IsNullOrEmpty(_currentContentItem.NextText))
            {
                buttonNext.Text = _currentContentItem.NextText;
            }
            ResizeControls();
            Slyce.Common.Utility.ResumePainting();
            Cursor = Cursors.Default;
        }

        private void FormFilter2_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void ResizeControls()
        {
            panelContent.Left = 0;
            panelContent.Top = panelTop.Bottom;
            panelContent.Width = this.ClientSize.Width;
            panelContent.Height = ucHeading1.Top - panelTop.Bottom;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            ArchAngel.Interfaces.Controls.ContentItems.ContentItem contentItem = (ArchAngel.Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (contentItem.HasFinish)
            {
                FinishClicked = true;
                this.Close();
                return;
            }

            if (!contentItem.Next())
            {
                return;
            }
            int indexOfCurrentItem = -1;

            for (int i = 0; i < _orderedContentItems.Count; i++)
            {
                if (_orderedContentItems[i] == contentItem)
                {
                    // If we're at the last item, do nothing
                    if (i < _orderedContentItems.Count - 1)
                    {
                        string name;

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
            ArchAngel.Interfaces.Controls.ContentItems.ContentItem contentItem = (ArchAngel.Interfaces.Controls.ContentItems.ContentItem)panelContent.Controls[0];

            if (!contentItem.Back())
            {
                return;
            }

            int indexOfCurrentItem = -1;

            for (int i = 0; i < _orderedContentItems.Count; i++)
            {
                if (_orderedContentItems[i] == contentItem)
                {
                    // If we're at the first item, do nothing
                    if (i > 0)
                    {
                        string name;

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
            //ContentItems.ContentItem contentItem = (ContentItems.ContentItem)panelContent.Controls[0];
            //ScreenNames newContentItemName = (ScreenNames)Enum.Parse(typeof(ScreenNames), contentItem.Back());

            //if (newContentItemName != ScreenNames.Nothing)
            //{
            //    if (contentItem.ValidateControls())
            //    {
            //        //panelContent.Controls.Clear();
            //        LoadWizardPage(newContentItemName);
            //    }
            //}
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

    }
}