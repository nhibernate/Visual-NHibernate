using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Interfaces.Controls.ContentItems;
using ArchAngel.Workbench.OptionsItems;

namespace ArchAngel.Workbench
{
    public partial class FormOptions : Form
    {
		private readonly Dictionary<ContentItem.OptionsItemNames, OptionScreen> _contentItems 
			= new Dictionary<ContentItem.OptionsItemNames, OptionScreen>();
        private OptionScreen _currentContentItem;

        public FormOptions()
        {
            InitializeComponent();
            BackColor = Slyce.Common.Colors.BackgroundColor;
            Populate();
            ucHeading1.Text = "";
            headingContentBottom.Text = "";
            DoubleBuffered = true;
            Controller.Instance.MainForm.Refresh();
            Controller.Instance.ShadeMainForm();
        }

        private void Populate()
        {
            splitContainer1.BackColor = Slyce.Common.Colors.BackgroundColorDark;
            splitContainer1.Panel1.BackColor = Slyce.Common.Colors.BackgroundColor;
            InitContentItems();

            sequentialNavBar1.BorderColorHover = Color.Navy;
            sequentialNavBar1.ButtonBorderSize = 1;
            sequentialNavBar1.ButtonHeight = 33;
            sequentialNavBar1.ButtonClickedEvent += sequentialNavBar1_ButtonClickedEvent;

            UserControls.HoverButton hoverButton = new UserControls.HoverButton();
            hoverButton.Text = "General";
            sequentialNavBar1.Add(hoverButton);

            hoverButton = new UserControls.HoverButton();
            hoverButton.Text = "Formatting";
            sequentialNavBar1.Add(hoverButton);
            LoadWizardPage(ContentItem.OptionsItemNames.General, false);
        }

        void sequentialNavBar1_ButtonClickedEvent(object sender, string name)
        {
            string navName;

            if (name.IndexOf('.') >= 0)
            {
                navName = name.Substring(name.IndexOf('.') + 1).Trim().ToLower();
            }
            else
            {
                navName = name.Trim().ToLower();
            }

            switch (navName)
            {
                case "general":
                    LoadWizardPage(ContentItem.OptionsItemNames.General, true);
                    break;
                case "formatting":
                    LoadWizardPage(ContentItem.OptionsItemNames.Formatting, true);
                    break;
                default:
                    throw new NotImplementedException("Not coded yet in: sequentialNavBar1_ButtonClickedEvent");
            }
        }

        [DotfuscatorDoNotRename]
        public void LoadWizardPage(ContentItem.OptionsItemNames contentItemName, bool calledFromClickEvent)
        {
			try
			{
				Cursor = Cursors.WaitCursor;
				Slyce.Common.Utility.SuspendPainting(panelContent);
				panelContent.Controls.Clear();
				OptionScreen nextContentItem = _contentItems[contentItemName];

				//webBrowserHelp.Navigate(HelpDirectory + @"\" + nextContentItem.HelpFileName);

				switch (contentItemName)
				{
					case ContentItem.OptionsItemNames.General:
						//ShowTaskHelp(TaskHelpTypes.ProjectDetails);

						if (!calledFromClickEvent)
						{
							sequentialNavBar1.ClickButton("General");
						}

						pictureHeading.Image = imageListHeading.Images[0];
						break;
					case ContentItem.OptionsItemNames.Formatting:
						//ShowTaskHelp(TaskHelpTypes.ProjectDetails);

						if (!calledFromClickEvent)
						{
							sequentialNavBar1.ClickButton("Formatting");
						}

						pictureHeading.Image = imageListHeading.Images[0];
						break;
					default:
						throw new NotImplementedException("Not coded yet: " + contentItemName.ToString());
				}
				_currentContentItem = nextContentItem;
				Slyce.Common.Utility.SuspendPainting(_currentContentItem);

				if (_currentContentItem == null)
				{
					return;
				}
				_currentContentItem.OnDisplaying();

				panelContent.Controls.Clear();
				panelContent.Controls.Add(_currentContentItem);
				_currentContentItem.Dock = DockStyle.Fill;
				//this.BackColor = _currentContentItem.BackColor;
				//pnlImageBackground.Visible = _currentContentItem.DisplayImage;
				panelTop.Visible = true;// _currentContentItem.DisplayTopPanel;
				headingContentTitle.Visible = !_currentContentItem.DisplayTopPanel;
				buttonBack.Visible = true;// _currentContentItem.HasPrev;
				buttonBack.BringToFront();
				buttonNext.Enabled = true;// _currentContentItem.HasNext || _currentContentItem.HasFinish;
				headingContentTitle.Text = _currentContentItem.Title;
				headingContentTitle.Refresh();
				labelPageHeader.Text = _currentContentItem.PageHeader;
				labelPageDescription.Text = _currentContentItem.PageDescription;

				if (!string.IsNullOrEmpty(_currentContentItem.NextText))
				{
					buttonNext.Text = _currentContentItem.NextText;
				}
				ResizeControls();
			}
			finally
			{
				Slyce.Common.Utility.ResumePainting(panelContent);
				Slyce.Common.Utility.ResumePainting(_currentContentItem);
				Cursor = Cursors.Default;
			}
        }

        private void InitContentItems()
        {
        	_contentItems.Clear();
            _contentItems.Add(ContentItem.OptionsItemNames.General, new General());
            _contentItems.Add(ContentItem.OptionsItemNames.Formatting, new Formatting());
            //panelContent.Controls.Add(_contentItems[(int)ContentItems.ContentItem.ContentItemNames.Merge]);
            //panelContent.Controls.Remove(_contentItems[(int)ContentItems.ContentItem.ContentItemNames.Merge]);

            // Add event handling code for each ContentItem
            foreach (ContentItem.OptionsItemNames key in _contentItems.Keys)
            {
                _contentItems[key].StatusChanged += ContentItem_StatusChanged;
            }
        }

        void ContentItem_StatusChanged(ContentItem sender)
        {
            buttonNext.Enabled = sender.HasNext || sender.HasFinish;
            headingContentTitle.Text = sender.Title;
            headingContentTitle.Refresh();
            labelPageHeader.Text = sender.PageHeader;
            labelPageDescription.Text = sender.PageDescription;
        }

        private void ResizeControls()
        {
            panelTop.Top = headingContentTitle.Visible ? headingContentTitle.Bottom + 1 : 0;
            panelTop.Left = 0;
            panelTop.Width = splitContainer1.Panel2.ClientSize.Width;

            panelContent.Left = 0;
            panelContent.Width = splitContainer1.Panel2.ClientSize.Width;
            int top = 0;

            if (headingContentTitle.Visible)
            {
                top += headingContentTitle.Height;
            }
            if (_currentContentItem.DisplayTopPanel)//(panelTop.Visible)
            {
                top += panelTop.Height;
            }

            panelContent.Top = top;
            panelContent.Height = splitContainer1.Panel2.ClientSize.Height - top - headingContentBottom.Height;
        }


    	private void splitContainer1_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
			foreach(var screen in _contentItems.Values)
			{
				screen.OnSave();
			}
        	Close();
        }

        private void FormOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void FormOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.Instance.UnshadeMainForm();
        }

		private void buttonBack_Click(object sender, EventArgs e)
		{
			Close();
		}
    }
}
