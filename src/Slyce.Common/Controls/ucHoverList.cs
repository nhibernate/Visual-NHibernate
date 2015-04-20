using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	public delegate void HoverButtonEventDelegate(object sender, string name, object tag);

	public partial class ucHoverList : UserControl
	{
		public event HoverButtonEventDelegate ButtonClickedEvent;
		internal ArrayList Buttons = new ArrayList();
		public int SelectedIndex = -1;
		public TextBox HiddenLabel = new TextBox();
		private Panel panel = new Panel();
		private int ButtonHeight = 0;
		HoverButton SelectedButton = null;

		public ucHoverList()
		{
			InitializeComponent();
			//if (Controller.InDesignMode) { return; }

			EnableDoubleBuffering();
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

		private void EnableDoubleBuffering()
		{
			// Set the value of the double-buffering style bits to true.
			this.SetStyle(ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint,
				true);
			this.UpdateStyles();
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

		public void Add(HoverButton button)
		{
			Slyce.Common.Utility.CheckForNulls(new object[] { button }, new string[] { "button" });
			ButtonHeight = button.Height;
			button.Width = this.ClientSize.Width;
			button.ParentList = this;
			button.Left = 0;

			if (Buttons.Count > 0)
			{
				HoverButton prevButton = (HoverButton)Buttons[Buttons.Count - 1];
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

		private void ShowChildren(HoverButton button)
		{
			if (button.IsHeading)
			{
				// Hide the children of this heading
				int currentBottom = button.Bottom;
				bool haveReachedNextHeading = false;

				for (int i = button.Index + 1; i < Buttons.Count; i++)
				{
					HoverButton nextButton = (HoverButton)Buttons[i];

					if (nextButton.IsHeading)
					{
						haveReachedNextHeading = true;
					}
					if (!haveReachedNextHeading && !nextButton.Visible)
					{
						nextButton.Visible = true;
						nextButton.Top = currentBottom;
						currentBottom = nextButton.Bottom;
					}
					else if (haveReachedNextHeading && nextButton.Visible)
					{
						nextButton.Top = currentBottom;
						currentBottom = nextButton.Bottom;
					}
				}
				button.ChildrenAreHidden = false;
			}
			else
			{
				throw new InvalidOperationException("HideChildren can only be called by Heading buttons.");
			}
		}

		private void HideChildren(HoverButton button)
		{
			if (button.IsHeading)
			{
				// Hide the children of this heading
				int currentBottom = button.Bottom;
				bool haveReachedNextHeading = false;

				for (int i = button.Index + 1; i < Buttons.Count; i++)
				{
					HoverButton nextButton = (HoverButton)Buttons[i];

					if (nextButton.IsHeading)
					{
						haveReachedNextHeading = true;
					}
					if (!haveReachedNextHeading)
					{
						nextButton.Visible = false;
					}
					else
					{
						nextButton.Top = currentBottom;
						currentBottom += nextButton.Height;
					}
				}
				button.ChildrenAreHidden = true;
			}
			else
			{
				throw new InvalidOperationException("HideChildren can only be called by Heading buttons.");
			}
		}

		internal void ButtonSelected(HoverButton button)
		{
			// If a heading is clicked, hide/show all buttons below
			if (button.IsHeading)
			{
				if (!button.ChildrenAreHidden)
				{
					HideChildren(button);
				}
				else
				{
					ShowChildren(button);
				}
				return;
			}
			if (SelectedButton != null)
			{
				SelectedButton.Unselect();
			}
			SelectedButton = button;

			if (ButtonClickedEvent != null)
			{
				ButtonClickedEvent(null, button.Text.Trim(), button.Tag);
			}
		}
	}

	public class HoverButton : Button
	{
		private bool m_selected = false;
		internal ucHoverList m_parent = null;
		private bool _isHeading = false;
		public int Index = -1;
		public bool ChildrenAreHidden = false;

		public HoverButton()
		{
			this.TextAlign = ContentAlignment.MiddleLeft;
			this.ImageAlign = ContentAlignment.MiddleLeft;
			this.FlatStyle = FlatStyle.Flat;
			this.Height = 27;
			this.FlatAppearance.BorderSize = 1;
			this.FlatAppearance.BorderColor = BackgroundColor;
			this.AutoEllipsis = true;
			this.BackColorChanged += new EventHandler(HoverButton_BackColorChanged);
			this.Paint += new PaintEventHandler(HoverButton_Paint);
		}

		void HoverButton_Paint(object sender, PaintEventArgs e)
		{
			Repaint(e.Graphics);
		}

		private void Repaint(Graphics graphics)
		{
			if (IsHeading && this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
			{
				LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Slyce.Common.Colors.FadingTitleLightColor, Slyce.Common.Colors.FadingTitleDarkColor, LinearGradientMode.Vertical);
				brush.SetSigmaBellShape(0.5f, 1.0f);
				graphics.FillRectangle(brush, this.ClientRectangle);
				Brush textBrush = new SolidBrush(this.ForeColor);
				SizeF textSize = graphics.MeasureString(this.Text, this.Font);
				graphics.DrawString(Text, this.Font, textBrush, 5, (this.Height / 2) - (System.Convert.ToInt32(textSize.Height) / 2));
				Pen pen = new Pen(textBrush, 2.0f);

				if (ChildrenAreHidden)
				{
					graphics.DrawLine(pen, new Point(10, this.Height / 2 + 4), new Point(10, this.Height / 2 - 4));
				}
				graphics.DrawLine(pen, new Point(10 - 4, this.Height / 2), new Point(10 + 4, this.Height / 2));
				brush.Dispose();
				textBrush.Dispose();
			}
		}


		void HoverButton_BackColorChanged(object sender, EventArgs e)
		{
			this.ForeColor = Slyce.Common.Colors.IdealTextColor(this.BackColor);
		}

		internal ucHoverList ParentList
		{
			get { return m_parent; }
			set
			{
				m_parent = value;
				this.BackColor = BackgroundColor;
				this.FlatAppearance.BorderColor = BackgroundColor;
				this.Index = ParentList.Buttons.Count;

				if (IsHeading)
				{
					this.Font = new Font(this.Font, FontStyle.Bold);
				}
			}
		}

		public bool IsHeading
		{
			get { return _isHeading; }
			set
			{
				_isHeading = value;

				if (_isHeading)
				{
					this.Text = this.Text.Trim();
				}
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
				Slyce.Common.Utility.CheckForNulls(new object[] { value }, new string[] { "value" });

				if (IsHeading)
				{
					base.Text = "     " + value.Trim();
				}
				else
				{
					base.Text = "       " + value.Trim();
				}
			}
		}

		private Color BackgroundColor
		{
			get
			{
				if (IsHeading || ParentList == null)
				{
					double brightness = IsHeading ? 0.45 : 0.85;
					return Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BaseColor, brightness);
				}
				else
				{
					return ParentList.BackColor;
				}
			}
		}

		private Color BackgroundColorHover
		{
			get
			{
				double brightness = IsHeading ? 0.45 : 0.85;
				return Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BaseColor, brightness);
			}
		}

		private Color BackgroundColorSelected
		{
			get
			{
				double brightness = IsHeading ? 0.45 : 0.70;
				return Slyce.Common.Colors.ChangeBrightness(Slyce.Common.Colors.BaseColor, brightness);
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			if (!Selected && !IsHeading)
			{
				this.BackColor = BackgroundColorHover;
				this.FlatAppearance.BorderColor = Color.DarkBlue;
			}
			Cursor = Cursors.Hand;
			base.OnMouseHover(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (!Selected && !IsHeading)
			{
				this.BackColor = BackgroundColor;
				this.FlatAppearance.BorderColor = this.BackColor;
			}
			Cursor = Cursors.Default;
			base.OnMouseLeave(e);
		}

		protected override void OnClick(EventArgs e)
		{

		}

		internal void Unselect()
		{
			m_selected = false;
			this.BackColor = BackgroundColor;
			this.FlatAppearance.BorderColor = this.BackColor;
			this.Refresh();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			//if (!IsHeading)
			//{
			this.ParentList.HiddenLabel.Focus();
			this.ParentList.ButtonSelected(this);
			m_selected = true;
			this.BackColor = BackgroundColorSelected;
			//}
		}

	}
}
