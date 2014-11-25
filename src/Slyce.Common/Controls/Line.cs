using System;
using System.ComponentModel;
using System.Drawing;

namespace Slyce.Common.Controls
{
	/// <summary>
	/// "NiceLine" draws a shaded line separator. Can have an aligned text caption.
	/// </summary>
	[DotfuscatorDoNotRename]
	[DefaultProperty("Caption")]
	[ToolboxBitmap(typeof(System.Windows.Forms.GroupBox))]
	public class Line : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.Container components = null;
		private string _Caption = "";
		private int _CaptionMarginSpace = 16;
		private int _CaptionPadding = 2;
		private LineVerticalAlign _LineVerticalAlign = LineVerticalAlign.Middle;
		private CaptionOrizontalAlign _CaptionOrizontalAlign = CaptionOrizontalAlign.Left;

		public Line()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// The caption text displayed on the line. 
		/// If the caption is "" (the default) the line is not broken
		/// </summary>
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The caption text displayed on the line. If the caption is \"\" (the default) the line is not broken")]
		public string Caption
		{
			get { return _Caption; }
			set
			{
				_Caption = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// The distance in pixels form the control margin to caption text
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(16)]
		[Description("The distance in pixels form the control margin to caption text")]
		public int CaptionMarginSpace
		{
			get { return _CaptionMarginSpace; }
			set
			{
				_CaptionMarginSpace = value;
				Invalidate();
			}
		}

		/// <summary>
		/// The space in pixels arrownd text caption
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(2)]
		[Description("The space in pixels arrownd text caption")]
		public int CaptionPadding
		{
			get { return _CaptionPadding; }
			set
			{
				_CaptionPadding = value;
				Invalidate();
			}
		}

		/// <summary>
		/// The vertical alignement of the line within the space of the control
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(LineVerticalAlign.Middle)]
		[Description("The vertical alignement of the line within the space of the control")]
		public LineVerticalAlign LineVerticalAlign
		{
			get { return _LineVerticalAlign; }
			set
			{
				_LineVerticalAlign = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Tell where the text caption is aligned in the control
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(CaptionOrizontalAlign.Left)]
		[Description("Tell where the text caption is aligned in the control")]
		public CaptionOrizontalAlign CaptionOrizontalAlign
		{
			get { return _CaptionOrizontalAlign; }
			set
			{
				_CaptionOrizontalAlign = value;
				Invalidate();
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// NiceLine
			// 
			this.Name = "NiceLine";
			this.Size = new System.Drawing.Size(100, this.Font.Height);
		}
		#endregion

		//		protected override CreateParams CreateParams
		//		{
		//			get
		//			{
		//				CreateParams cp = base.CreateParams;
		//				cp.ExStyle |= 0x20;
		//				return cp;
		//			}
		//		}
		//
		//		protected override void OnMove(EventArgs e)
		//		{
		//			RecreateHandle();
		//		}
		//
		//		protected override void OnPaintBackground(PaintEventArgs e)
		//		{
		//			// do nothing
		//		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			int ym;
			switch (LineVerticalAlign)
			{
				case LineVerticalAlign.Top:
					ym = 0;
					break;
				case LineVerticalAlign.Middle:
					ym = Convert.ToInt32(Math.Ceiling((double)(Size.Height / 2))) - 1;
					break;
				case LineVerticalAlign.Bottom:
					ym = Size.Height - 2;
					break;
				default:
					ym = 0;
					break;
			}

			SizeF captionSizeF = e.Graphics.MeasureString(Caption, this.Font, this.Width - CaptionMarginSpace * 2, StringFormat.GenericDefault);
			int captionLength = Convert.ToInt32(captionSizeF.Width);

			int beforeCaption;
			int afterCaption;

			if (Caption == "")
			{
				beforeCaption = CaptionMarginSpace;
				afterCaption = CaptionMarginSpace;
			}
			else
			{
				switch (CaptionOrizontalAlign)
				{
					case CaptionOrizontalAlign.Left:
						beforeCaption = CaptionMarginSpace;
						afterCaption = CaptionMarginSpace + CaptionPadding * 2 + captionLength;
						break;
					case CaptionOrizontalAlign.Center:
						beforeCaption = (Width - captionLength) / 2 - CaptionPadding;
						afterCaption = (Width - captionLength) / 2 + captionLength + CaptionPadding;
						break;
					case CaptionOrizontalAlign.Right:
						beforeCaption = Width - CaptionMarginSpace * 2 - captionLength;
						afterCaption = Width - CaptionMarginSpace;
						break;
					default:
						beforeCaption = CaptionMarginSpace;
						afterCaption = CaptionMarginSpace;
						break;
				}
			}

			// ------- 
			// |      ...caption...
			if (beforeCaption > 0)
			{
				e.Graphics.DrawLines(new Pen(Color.DimGray, 1),
					new Point[] { 
								new Point(0, ym + 1), 
								new Point(0, ym), 
								new Point(beforeCaption, ym)
							}
					);
			}

			//                  -------
			//	      ...caption... 
			e.Graphics.DrawLines(new Pen(Color.DimGray, 1),
				new Point[] { 
								new Point(afterCaption, ym), 
								new Point(this.Width, ym)
							}
				);

			//        ...caption...
			// -------
			e.Graphics.DrawLines(new Pen(Color.White, 1),
				new Point[] { 
								new Point(0, ym + 1), 
								new Point(beforeCaption, ym + 1)
							}
				);

			//        ...caption...       |
			//                  -------
			e.Graphics.DrawLines(new Pen(Color.White, 1),
				new Point[] { 
								new Point(afterCaption, ym + 1), 
								new Point(this.Width, ym + 1), 
								new Point(this.Width, ym) 
							}
				);

			//        ...caption...
			if (Caption != "")
			{
				e.Graphics.DrawString(Caption, this.Font, new SolidBrush(this.ForeColor), beforeCaption + CaptionPadding, 1);
			}

			//			e.Graphics.DrawLines(new Pen(Color.Red, 1), 
			//				new Point[] { 
			//								new Point(0, 0), 
			//								new Point(this.Width-1, 0), 
			//								new Point(this.Width-1, this.Height-1),
			//								new Point(0, this.Height-1),
			//								new Point(0, 0)
			//							} 
			//				);
		}

		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			this.Height = this.Font.Height + 2;
			this.Invalidate();
		}

		protected override void OnFontChanged(System.EventArgs e)
		{
			this.OnResize(e);
			base.OnFontChanged(e);
		}
	}

	public enum LineVerticalAlign
	{
		Top,
		Middle,
		Bottom
	}

	public enum CaptionOrizontalAlign
	{
		Left,
		Center,
		Right
	}
}
