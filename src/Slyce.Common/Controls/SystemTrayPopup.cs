using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	internal partial class SystemTrayPopup : Form
	{
		#region Delegate Instances
		public event System.Windows.Forms.ControlEventHandler CloseButtonClicked;
		public event System.Windows.Forms.ControlEventHandler HeadingClicked;
		public event System.Windows.Forms.ControlEventHandler TextClicked;
		public event System.Windows.Forms.ControlEventHandler Clicked;
		#endregion

		int ScreenBottom = SystemInformation.WorkingArea.Height;
		public int DisplayTime = 1000;
		public int FadeAwayTime = 2000;
		public int FadeInTime = 1000;
		private DateTime LastActivatedTime = DateTime.Now;
		private bool FullyDisplayed = false;
		public Color LightColor = Color.White;
		public Color DarkColor = Color.DodgerBlue;
		private Graphics g = null;
		private Bitmap localBitmap = null;
		public string HeadingText = "";
		private Rectangle CloseButtonRectangle;
		private RectangleF HeadingRect;
		private RectangleF TextRectangle;
		private RectangleF IconRectangle;
		private Graphics realGraphics;
		public new bool AutoSize = true;
		public int MinHeight = 100;
		public double OpacitySlide = 0.1;
		public double OpacityFinal = 0.9;
		public double OpacityFocused = 1.0;
		private bool MouseIsOverText = false;
		private bool MouseIsOverHeading = false;
		private bool MouseIsOverCloseButton = false;
		private bool TextIsUnderlined = false;
		private bool HeadingIsUnderlined = false;
		private Image _icon;
		private string _iconFile;
		private bool IsClosing = false;
		private Slyce.Common.CrossThreadHelper CrossThreadHelper;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="headingText"></param>
		/// <param name="text"></param>
		public SystemTrayPopup(string headingText, string text)//, double slideOpacity, double finalOpacity, double focusedOpacity)
		{
			CrossThreadHelper = new CrossThreadHelper(this);
			this.Top = ScreenBottom;
			this.Left = SystemInformation.WorkingArea.Width - this.Width - 10;
			this.Visible = false;
			InitializeComponent();
			//SetOpacities(slideOpacity, finalOpacity, focusedOpacity);
			//this.Opacity = OpacitySlide;
			//CrossThreadHelper.SetCrossThreadProperty(this, "Opacity", 0.1);
			this.BackColor = DarkColor;
			HeadingText = headingText;
			this.Text = text;
			//this.Height = Math.Min(SystemInformation.WorkingArea.Height, Math.Max(height, MinHeight));
			//Display();
		}

		public string IconFile
		{
			get { return _iconFile; }
			set { _iconFile = value; }
		}

		private new Image Icon
		{
			get
			{
				if (_icon == null)
				{
					if (!string.IsNullOrEmpty(_iconFile))
					{
						if (Slyce.Common.Utility.StringsAreEqual(Path.GetExtension(_iconFile), ".ico", false))
						{
							_icon = new Icon(_iconFile).ToBitmap();
						}
						else
						{
							_icon = new Bitmap(_iconFile);
						}
					}
				}
				return _icon;
			}
		}

		public void Display()
		{
			this.Opacity = OpacitySlide;
			this.Show();
			Draw(false);
			backgroundWorker1.RunWorkerAsync();
			backgroundWorkerShow.RunWorkerAsync();
		}

		/// <summary>
		/// Sets the opacity values.
		/// </summary>
		/// <param name="slide">Opacity during 'slide'.</param>
		/// <param name="final">Opacity at final display.</param>
		/// <param name="focused"></param>
		public void SetOpacities(double slide, double final, double focused)
		{
			if (slide < 0 || slide > 1 ||
				final < 0 || final > 1 ||
				focused < 0 || focused > 1)
			{
				throw new InvalidOperationException("Invalid Opacity value. Must be between 0.0 and 1.0");
			}
			OpacitySlide = slide;
			OpacityFinal = final;
			OpacityFocused = focused;
		}

		private void Draw(bool forceRedraw)
		{
			//create our offscreen bitmap
			if (localBitmap == null || forceRedraw)
			{
				CloseButtonRectangle = new Rectangle(this.Width - 20, 13, 10, 10);

				if (Icon != null)
				{
					IconRectangle = new RectangleF(5, 23, Icon.Width, Icon.Height);
					//IconRectangle = new RectangleF(5, 23, 48, 48);
				}
				HeadingRect = new RectangleF(IconRectangle.Right + 5, 23, this.Width - IconRectangle.Right - 5, 20);
				TextRectangle = new RectangleF(IconRectangle.Right + 5, HeadingRect.Bottom + 5, this.Width - IconRectangle.Right - 5, this.Height - HeadingRect.Bottom - 10);

				localBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
				g = Graphics.FromImage(localBitmap);

				SolidBrush textBrush = new SolidBrush(Color.Black);
				Font headingFont;
				Font textFont;

				if (HeadingIsUnderlined) { headingFont = new Font(this.Font, FontStyle.Bold | FontStyle.Underline); }
				else { headingFont = new Font(this.Font, FontStyle.Bold); }

				if (TextIsUnderlined) { textFont = new Font(this.Font, FontStyle.Underline); }
				else { textFont = new Font(this.Font, FontStyle.Regular); }

				SizeF textRectSize = g.MeasureString(this.Text, textFont, (int)TextRectangle.Width);
				SizeF headingRectSize = g.MeasureString(HeadingText, headingFont, (int)HeadingRect.Width);

				if (AutoSize)
				{
					HeadingRect.Height = Math.Min(SystemInformation.WorkingArea.Height, headingRectSize.Height);
					TextRectangle.Y = HeadingRect.Bottom + 5;
					TextRectangle.Height = textRectSize.Height;
					this.Height = Math.Max(MinHeight, (int)TextRectangle.Bottom + 5);
				}
				// If the text is only on a single line, resize the width of the string so that the mouseover looks
				// correct. If we don't do this, the the text will get underlined even when we are way off to the right.
				if (textRectSize.Height == g.MeasureString(".", this.Font, (int)TextRectangle.Width).Height)
				{
					TextRectangle.Width = textRectSize.Width + 2;
				}
				if (headingRectSize.Height == g.MeasureString(".", headingFont, (int)HeadingRect.Width).Height)
				{
					HeadingRect.Width = headingRectSize.Width + 2;
				}
				localBitmap.Dispose();
				g.Dispose();
				localBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
				g = Graphics.FromImage(localBitmap);

				// Draw Background
				LinearGradientBrush backgroundBrush = new LinearGradientBrush(this.ClientRectangle, LightColor, DarkColor, LinearGradientMode.Vertical);
				g.FillRectangle(backgroundBrush, this.ClientRectangle);

				Pen pen = new Pen(DarkColor, 2);
				Pen closeButtonPen;

				if (MouseIsOverCloseButton)
				{
					closeButtonPen = new Pen(Color.Black, 3);
				}
				else
				{
					closeButtonPen = new Pen(Color.Black, 2);
				}

				Point topLeftCorner = new Point(0, 0);
				Point topRightCorner = new Point(this.Width - 1, 0);
				Point bottomLeftCorner = new Point(0, this.Height - 1);
				Point bottomRightCorner = new Point(this.Width - 1, this.Height - 1);

				// Draw 'X' close button
				g.DrawLine(closeButtonPen, CloseButtonRectangle.Left, CloseButtonRectangle.Top, CloseButtonRectangle.Right, CloseButtonRectangle.Bottom);
				g.DrawLine(closeButtonPen, CloseButtonRectangle.Left, CloseButtonRectangle.Bottom, CloseButtonRectangle.Right, CloseButtonRectangle.Top);

				// Draw Icon
				if (Icon != null)
				{
					//g.DrawIcon(Icon, (int)IconRectangle.X, (int)IconRectangle.Y);
					g.DrawImage(Icon, IconRectangle);
				}

				Rectangle rect = new Rectangle(topLeftCorner, new Size(this.Width, 12));
				LinearGradientBrush brush = new LinearGradientBrush(rect, LightColor, DarkColor, LinearGradientMode.Vertical);
				g.FillRectangle(brush, rect);

				g.DrawString(HeadingText, headingFont, textBrush, HeadingRect, StringFormat.GenericDefault);
				g.DrawString(this.Text, textFont, textBrush, TextRectangle, StringFormat.GenericDefault);

				// Draw Border
				Pen borderPen = new Pen(Color.Black, 1);
				g.DrawLine(borderPen, topLeftCorner, topRightCorner);
				g.DrawLine(borderPen, topLeftCorner, bottomLeftCorner);
				g.DrawLine(borderPen, bottomLeftCorner, bottomRightCorner);
				g.DrawLine(borderPen, topRightCorner, bottomRightCorner);

				g.Dispose();
				pen.Dispose();
				brush.Dispose();
				borderPen.Dispose();
				closeButtonPen.Dispose();
				textBrush.Dispose();
				headingFont.Dispose();
				textFont.Dispose();
				//realGraphics = Graphics.FromHwnd(this.Handle);
			}
			//push our bitmap forward to the screen
			if (realGraphics != null)
			{
				realGraphics.Dispose();
			}
			realGraphics = Graphics.FromHwnd(this.Handle);
			realGraphics.DrawImage(localBitmap, 0, 0);
			//localBitmap.Dispose();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.AboveNormal;

			while (!FullyDisplayed)
			{
				System.Threading.Thread.Sleep(100);
			}
			System.Threading.Thread.Sleep(DisplayTime);
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				// Ensure that we get this reported to us.
				throw e.Error;
			}
			double opacity;
			double fadeDelta = OpacityFinal / (FadeAwayTime / 10);

			while (this.Opacity > 0)
			{
				if (IsClosing) { return; }
				if (DateTime.Now.Subtract(LastActivatedTime) < TimeSpan.FromMilliseconds(DisplayTime))
				{
					this.Opacity = OpacityFocused;
					break;
				}
				else
				{
					opacity = this.Opacity - fadeDelta;
					this.Opacity = opacity;
					Application.DoEvents();
					System.Threading.Thread.Sleep(10);
				}
			}
			if (this.Opacity == 0)
			{
				this.Close();
			}
			else
			{
				backgroundWorker1.RunWorkerAsync();
			}
		}

		//private void button1_Click(object sender, EventArgs e)
		//{
		//    this.Close();
		//}

		private void SystemTrayPopup_Activated(object sender, EventArgs e)
		{
			LastActivatedTime = DateTime.Now;
			//this.Opacity = OpacityFocused;
		}

		private void SystemTrayPopup_MouseMove(object sender, MouseEventArgs e)
		{
			LastActivatedTime = DateTime.Now;
			this.Opacity = OpacityFocused;

			//if (CloseButtonRectangle.Contains(e.Location) && CloseButtonClicked != null)
			//{
			//    CloseButtonClicked(this, null);
			//    this.Close();
			//    return;
			//}
			//if (HeadingRect.Contains(e.Location) && HeadingClicked != null)
			//{
			//    HeadingClicked(this, null);
			//    clickHandled = true;
			//}

			// Only do this switching if the calling code is handling the TextClicked event
			if (TextClicked != null)
			{
				if (!MouseIsOverText && TextRectangle.Contains(e.Location))
				{
					MouseIsOverText = true;
					TextIsUnderlined = true;
					Cursor = Cursors.Hand;
					Draw(true);
				}
				else if (MouseIsOverText && !TextRectangle.Contains(e.Location))
				{
					MouseIsOverText = false;
					TextIsUnderlined = false;
					Cursor = Cursors.Default;
					Draw(true);
				}
			}
			// Only do this switching if the calling code is handling the HeadingClicked event
			if (HeadingClicked != null)
			{
				if (!MouseIsOverHeading && HeadingRect.Contains(e.Location))
				{
					MouseIsOverHeading = true;
					HeadingIsUnderlined = true;
					Cursor = Cursors.Hand;
					Draw(true);
				}
				else if (MouseIsOverHeading && !HeadingRect.Contains(e.Location))
				{
					MouseIsOverHeading = false;
					HeadingIsUnderlined = false;
					Cursor = Cursors.Default;
					Draw(true);
				}
			}
			if (!MouseIsOverCloseButton && CloseButtonRectangle.Contains(e.Location))
			{
				MouseIsOverCloseButton = true;
				Cursor = Cursors.Hand;
				Draw(true);
			}
			else if (MouseIsOverCloseButton && !CloseButtonRectangle.Contains(e.Location))
			{
				MouseIsOverCloseButton = false;
				Cursor = Cursors.Default;
				Draw(true);
			}
		}

		private void SystemTrayPopup_MouseEnter(object sender, EventArgs e)
		{
			LastActivatedTime = DateTime.Now;
			this.Opacity = OpacityFocused;
		}

		private void SystemTrayPopup_MouseClick(object sender, MouseEventArgs e)
		{
			LastActivatedTime = DateTime.Now;
			this.Opacity = OpacityFocused;

			if (MouseIsOverCloseButton)
			{
				this.Close();
			}
		}

		private void backgroundWorkerShow_DoWork(object sender, DoWorkEventArgs e)
		{
			System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.AboveNormal;
			CrossThreadHelper.SetCrossThreadProperty(this, "Visible", true);
			CrossThreadHelper.CallCrossThreadMethod(this, "Show", null);
			CrossThreadHelper.SetCrossThreadProperty(this, "Top", ScreenBottom);
			CrossThreadHelper.SetCrossThreadProperty(this, "Left", SystemInformation.WorkingArea.Width - this.Width - 10);
			int delta = this.Height / 100;
			double opacity = this.Opacity;

			while (this.Top > ScreenBottom - this.Height)
			{
				int top = this.Top - delta;
				CrossThreadHelper.SetCrossThreadProperty(this, "Top", top);
				this.Invalidate();
				Application.DoEvents();
				System.Threading.Thread.Sleep(5);
			}
			double fadeDelta = OpacityFinal / (FadeInTime / 10);

			while (this.Opacity < OpacityFinal)
			{
				opacity += fadeDelta;// 0.01;
				CrossThreadHelper.SetCrossThreadProperty(this, "Opacity", opacity);
				CrossThreadHelper.CallCrossThreadMethod(this, "Invalidate", null);
				System.Threading.Thread.Sleep(10);
			}
			LastActivatedTime = DateTime.Now;
			FullyDisplayed = true;
			this.Invalidate();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			//base.OnPaintBackground(e);
		}
		private void SystemTrayPopup_Paint(object sender, PaintEventArgs e)
		{
			Draw(false);
		}

		private void SystemTrayPopup_FormClosing(object sender, FormClosingEventArgs e)
		{
			IsClosing = true;

			if (localBitmap != null) { localBitmap.Dispose(); }
			if (realGraphics != null) { realGraphics.Dispose(); }
		}

		private void SystemTrayPopup_MouseDown(object sender, MouseEventArgs e)
		{
			bool clickHandled = false;

			if (CloseButtonRectangle.Contains(e.Location) && CloseButtonClicked != null)
			{
				CloseButtonClicked(this, null);
				this.Close();
				return;
			}
			if (HeadingRect.Contains(e.Location) && HeadingClicked != null)
			{
				HeadingClicked(this, null);
				clickHandled = true;
			}
			if (TextRectangle.Contains(e.Location) && TextClicked != null)
			{
				TextClicked(this, null);
				clickHandled = true;
			}
			if (!clickHandled && Clicked != null)
			{
				Clicked(this, null);
			}

		}
	}

	public class SystemTrayAlert
	{
		public event System.Windows.Forms.ControlEventHandler CloseButtonClicked;
		public event System.Windows.Forms.ControlEventHandler HeadingClicked;
		public event System.Windows.Forms.ControlEventHandler TextClicked;
		public event System.Windows.Forms.ControlEventHandler Clicked;

		private static System.Threading.Timer timer;
		public int DisplayTime = 1000;
		public int FadeAwayTime = 2000;
		public int FadeInTime = 1000;
		public Color LightColor = Color.White;
		public Color DarkColor = Color.DodgerBlue;
		public string HeadingText = "";
		public string Text = "";
		public bool AutoSize = true;
		public int MinHeight = 100;
		public double OpacitySlide = 0.1;
		public double OpacityFinal = 0.9;
		public double OpacityFocused = 1.0;
		public string IconFile;
		public int Width = 400;
		private static bool HasStarted = false;

		public void Start()
		{
			HasStarted = false;
			System.Threading.TimerCallback callback = new System.Threading.TimerCallback(this.Begin);
			timer = new System.Threading.Timer(callback, null, 1, 1000);
			return;
		}

		private void Begin(object state)
		{
			if (HasStarted)
			{
				timer.Dispose();
				return;
			}
			HasStarted = true;
			//timer.Dispose();
			SystemTrayPopup form = new SystemTrayPopup(HeadingText, Text);
			form.CloseButtonClicked += new ControlEventHandler(form_CloseButtonClicked);
			form.TextClicked += new ControlEventHandler(form_TextClicked);
			form.HeadingClicked += new ControlEventHandler(form_HeadingClicked);
			form.Clicked += new ControlEventHandler(form_Clicked);
			form.DisplayTime = DisplayTime;
			form.FadeAwayTime = FadeAwayTime;
			form.FadeInTime = FadeInTime;
			form.LightColor = LightColor;
			form.DarkColor = DarkColor;
			form.HeadingText = HeadingText;
			form.Text = Text;
			form.AutoSize = AutoSize;
			form.MinHeight = MinHeight;
			form.OpacitySlide = OpacitySlide;
			form.OpacityFinal = OpacityFinal;
			form.OpacityFocused = OpacityFocused;
			form.IconFile = IconFile;
			form.Width = Width;
			form.Display();
			Application.Run(form);
		}

		void form_Clicked(object sender, ControlEventArgs e)
		{
			if (Clicked != null)
			{
				Clicked(sender, e);
			}
		}

		void form_HeadingClicked(object sender, ControlEventArgs e)
		{
			if (HeadingClicked != null)
			{
				HeadingClicked(sender, e);
			}
			else if (Clicked != null)
			{
				Clicked(sender, e);
			}
		}

		void form_TextClicked(object sender, ControlEventArgs e)
		{
			if (TextClicked != null)
			{
				TextClicked(sender, e);
			}
			else if (Clicked != null)
			{
				Clicked(sender, e);
			}
		}

		void form_CloseButtonClicked(object sender, ControlEventArgs e)
		{
			if (CloseButtonClicked != null)
			{
				CloseButtonClicked(sender, e);
			}
		}
	}

}