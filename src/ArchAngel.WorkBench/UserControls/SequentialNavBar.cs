using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevComponents.DotNetBar;

namespace ArchAngel.Workbench.UserControls
{
    public delegate void HoverButtonEventDelegate(object sender, string name);

    public partial class SequentialNavBar : UserControl
    {
        public event HoverButtonEventDelegate ButtonClickedEvent;
        public List<HoverButton> Buttons = new List<HoverButton>();
        public int SelectedIndex = -1;
        public TextBox HiddenLabel = new TextBox();
        private PanelEx panel = new PanelEx();
        public int ButtonHeight = 20;
        HoverButton SelectedButton = null;
        public Color BorderColorHover = Color.DarkBlue;
        public int ButtonBorderSize = 0;

        public SequentialNavBar()
        {
            InitializeComponent();
            if (Slyce.Common.Utility.InDesignMode) { return; }
            HiddenLabel.Top = -100;
            HiddenLabel.Left = -100;
            this.Controls.Add(HiddenLabel);
            panel.BackColor = Color.Transparent;
            panel.Height = 0;
            panel.Left = 0;
            panel.Width = this.ClientSize.Width;
            panel.AutoScroll = true;
            panel.Height = this.Height;
            this.Controls.Add(panel);
            PerformLayout();
        }

        public int GetButtonIndex(string text)
        {
            text = text.Trim().ToLower();

            for (int i = 0; i < Buttons.Count; i++)
            {
                string navName = Buttons[i].Text;

                if (navName.IndexOf('.') >= 0)
                {
                    navName = navName.Substring(navName.IndexOf('.') + 1).Trim().ToLower();
                }
                else
                {
                    navName = navName.Trim().ToLower();
                }
                if (navName == text)
                {
                    return i;
                }
            }
            return -1;
        }

        public void ClickButton(string buttonText)
        {
            ClickButton(GetButtonIndex(buttonText));
        }

        public void ClickButton(int index)
        {
            HoverButton button = Buttons[index];

            if (SelectedButton != null)
            {
                SelectedButton.Unselect();
            }
            button.Select();
        }

        public void Clear()
        {
            for (int i = panel.Controls.Count - 1; i >= 0; i--)
            {
                if (panel.Controls[i].GetType() == typeof(HoverButton))
                {
                    panel.Controls.RemoveAt(i);
                }
            }
            Buttons.Clear();
            SelectedIndex = -1;
        }

        public static void CheckForNulls(object[] args, string[] names)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    throw new ArgumentNullException(names[i]);
                }
            }
        }

        public void Add(HoverButton button)
        {
            SequentialNavBar.CheckForNulls(new object[] { button }, new string[] { "button" });
            button.Height = ButtonHeight;
            button.Width = this.ClientSize.Width;
            button.Parent = this;
            button.Left = 0;
            button.SetColors(BorderColorHover, BackgroundColorHover, BackgroundColorSelected);
            button.FlatAppearance.BorderSize = ButtonBorderSize;

            if (Buttons.Count > 0)
            {
                HoverButton prevButton = Buttons[Buttons.Count - 1];
                button.Top = prevButton.Top + prevButton.Height;
            }
            else
            {
                button.Top = 0;
            }
            panel.Controls.Add(button);
            Buttons.Add(button);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PerformLayout();
        }

        private new void PerformLayout()
        {
            int clientWidth = this.ClientSize.Width;
            int buttonWidth = clientWidth;
            panel.Width = buttonWidth;
            panel.Height = this.Height;

            for (int x = this.Controls.Count - 1; x >= 0; x--)
            {
                if (this.Controls[x].GetType() == typeof(Panel))
                {
                    Panel tempPanel = (Panel)this.Controls[x];

                    for (int i = tempPanel.Controls.Count - 1; i >= 0; i--)
                    {
                        if (tempPanel.Controls[i].GetType() == typeof(HoverButton))
                        {
                            tempPanel.Controls[i].Width = buttonWidth;
                        }
                    }
                    break;
                }
            }
        }

        internal void ButtonSelected(HoverButton button, bool raiseButtonClickEvent)
        {
            if (SelectedButton != null)
            {
                SelectedButton.Unselect();
            }
            SelectedButton = button;

            if (ButtonClickedEvent != null && raiseButtonClickEvent)
            {
                ButtonClickedEvent(null, button.Text.Trim());
            }
            //for (int i = 0; i < panel.Controls.Count; i++)
            //{
            //   Control ctl = panel.Controls[i];

            //   if (ctl != button && 
            //      panel.Controls[i].GetType() == typeof(HoverButton))
            //   {
            //      ((HoverButton)panel.Controls[i]).Unselect();
            //   }
            //}
        }

        private Color BackgroundColorHover
        {
            get
            {
                return Slyce.Common.Colors.GetBaseColorVariant(0.5);
            }
        }

        private Color BackgroundColorSelected
        {
            get
            {
                return Slyce.Common.Colors.GetBaseColorVariant(0.7);
            }
        }


    }

    public class HoverButton : Button
    {
        private bool m_selected = false;
        internal SequentialNavBar m_parent = null;
        private Color BorderColorHover = Color.DarkBlue;
        private Color BackColorHover = Color.FromArgb(225, 230, 232);
        private Color BackColorSelected = Color.FromArgb(152, 181, 226);

        public HoverButton()
        {
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.FlatStyle = FlatStyle.Flat;
            this.Height = 27;
            this.FlatAppearance.BorderSize = 1;
            this.FlatAppearance.BorderColor = this.BackColor;
            this.AutoEllipsis = true;
            //this.Padding.All = 0;
            Font font = new Font(this.Font, FontStyle.Bold);
            this.Font = font;
        }

        internal void SetColors(Color borderColorHover, Color backColorHover, Color backColorSelected)
        {
            BorderColorHover = borderColorHover;
            BackColorHover = backColorHover;
            BackColorSelected = backColorSelected;
        }

        internal new SequentialNavBar Parent
        {
            get { return m_parent; }
            set
            {
                m_parent = value;
                this.BackColor = m_parent.BackColor;
                this.FlatAppearance.BorderColor = m_parent.BackColor;
            }
        }

        private bool Selected
        {
            get { return m_selected; }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                SequentialNavBar.CheckForNulls(new object[] { value }, new string[] { "value" });
                base.Text = "         " + value.Trim();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!Selected)
            {
                this.BackColor = BackColorHover;
                this.FlatAppearance.BorderColor = BorderColorHover;
                this.ForeColor = Slyce.Common.Colors.IdealTextColor(this.BackColor);
            }
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!Selected)
            {
                this.BackColor = Parent.BackColor;
                this.FlatAppearance.BorderColor = this.BackColor;
                this.ForeColor = Slyce.Common.Colors.IdealTextColor(this.BackColor);
            }
            base.OnMouseLeave(e);
        }

        internal void Unselect()
        {
            m_selected = false;
            this.BackColor = Parent.BackColor;
            this.FlatAppearance.BorderColor = this.BackColor;
            this.ForeColor = Slyce.Common.Colors.IdealTextColor(this.BackColor);
            this.Refresh();
        }

        internal new void Select()
        {
            OnGotFocus(null);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.Parent.HiddenLabel.Focus();
            // If e = null then this function was called by code, not user action. In this
            // case we mustn't call ButtonSelected because we'll cause the control to
            // get loaded twice.
            this.Parent.ButtonSelected(this, e != null);
            m_selected = true;
            this.BackColor = BackColorSelected;
            this.ForeColor = Slyce.Common.Colors.IdealTextColor(this.BackColor);
            this.FlatAppearance.BorderColor = BorderColorHover;
        }


    }
}
