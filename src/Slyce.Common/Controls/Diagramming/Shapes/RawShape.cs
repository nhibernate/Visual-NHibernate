using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class RawShape
	{
		public event MouseEventHandler MouseClick;
		public event EventHandler Enter;
		public event EventHandler Leave;
		public event MouseEventHandler MouseDoubleClick;

		internal ShapeCanvas Canvas;
		private int Gap = 3;
		private int SubTextGap = 1;
		private int TextWidth = 0;
		private int TextHeight = 0;
		private int TextOnlyHeight = 0;
		private int SubTextHeight = 0;
		private int TextHeightCategory = 0;
		private int TextHeightProperty = 0;
		public LinkLine OriginatingLineStyle;
		private int _Width = 80;
		public int Height = 30;
		private int _Left = 0;
		public int Top = 0;
		public System.Drawing.Drawing2D.GraphicsPath GPath;
		private string _Text;
		private string _SubText;
		public Font Font;
		public Font FontSubText;
		public Font FontCategory;
		public Font FontProperty;
		private SolidBrush _TextBrush = new SolidBrush(Color.White);
		private Color _BackColor1 = Color.FromArgb(130, 130, 130);
		private Color _BackColor2 = Color.FromArgb(13, 13, 13);
		public Color BorderColor = Color.FromArgb(130, 130, 130);
		public Color ForeColor = Color.White;
		public int BorderWidth = 1;
		private bool _AutoSizeWidth = true;
		public int MinimumWidth = 90;
		private bool _IsEmpty = false;
		private bool _HasFocus = false;
		public Color FocusBackColor1 = Color.Orange;
		public Color FocusBackColor2 = Color.Yellow;
		public Color FocusForeColor = Color.White;
		public Color FocusBorderColor = Color.Orange;
		//public static Image DeleteIcon = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Diagrammer.Resources.delete_x_16.png"));
		public Cursor Cursor = Cursors.Default;
		public bool RoundedCorners = true;
		public object Tag;
		public bool RecalcSizeRequired = true;
		public Image Icon = null;
		public List<RawCategory> Categories = new List<RawCategory>();
		public List<RawShape> InnerShapes = new List<RawShape>();

		public RawShape(ShapeCanvas canvas, string name, Font font)
		{
			OriginatingLineStyle = new LinkLine(font);
			OriginatingLineStyle.Parent = this;
			Text = name;
			Font = font;
			FontCategory = Font;
			FontProperty = Font;
			Canvas = canvas;
		}

		public RawShape(ShapeCanvas canvas, string name, Font font, Font fontCategory, Font fontProperty)
		{
			OriginatingLineStyle = new LinkLine(font);
			OriginatingLineStyle.Parent = this;
			Text = name;
			Font = font;
			FontCategory = fontCategory;
			FontProperty = fontProperty;
			Canvas = canvas;
		}

		public Color BackColor1
		{
			get { return _BackColor1; }
			set
			{
				_BackColor1 = value;
			}
		}

		public Color BackColor2
		{
			get { return _BackColor2; }
			set
			{
				_BackColor2 = value;
				BorderColor = value;
			}
		}

		public bool AutoSizeWidth
		{
			get { return _AutoSizeWidth; }
			set
			{
				if (_AutoSizeWidth != value)
				{
					_AutoSizeWidth = value;

					if (!_AutoSizeWidth)
						RecalcSizeRequired = false;
					else
						RecalcSizeRequired = true;
				}
			}
		}

		public Point Location
		{
			get { return new Point(this.Left, this.Top); }
		}

		public string Text
		{
			get { return _Text; }
			set
			{
				if (_Text != value)
				{
					_Text = value;
					//RecalculateWidth();

					if (!AutoSizeWidth)
						RecalcSizeRequired = true;
				}
			}
		}

		public string SubText
		{
			get { return _SubText; }
			set
			{
				if (_SubText != value)
				{
					_SubText = value;
					//RecalculateWidth();

					if (!AutoSizeWidth)
						RecalcSizeRequired = true;
				}
			}
		}

		public int Width
		{
			get { return _Width; }
			set
			{
				if (!AutoSizeWidth)
					_Width = value;
			}
		}

		public int Left
		{
			get { return _Left; }
			set { _Left = value; }
		}

		public int Right
		{
			get { return Left + Width; }
		}

		public int Bottom
		{
			get { return Top + Height; }
		}

		public Rectangle Bounds
		{
			get { return new Rectangle(Left, Top, Width, Height); }
		}

		private Brush TextBrush
		{
			get
			{
				if (!HasFocus && _TextBrush.Color != ForeColor)
				{
					_TextBrush = new SolidBrush(ForeColor);
				}
				else if (HasFocus && _TextBrush.Color != FocusForeColor)
				{
					_TextBrush = new SolidBrush(FocusForeColor);
				}
				return _TextBrush;
			}
		}


		public bool IsEmpty
		{
			get { return _IsEmpty; }
			set
			{
				if (_IsEmpty != value)
				{
					_IsEmpty = value;
					Text = value ? string.Format(@"<span align=""center""><a>{0}</a></span>", Text) : Text;
				}
			}
		}

		internal int ReCalculateSize(Graphics g)
		{
			TextWidth = (int)g.MeasureString(Text, Font).Width + 3;
			TextOnlyHeight = (int)g.MeasureString(Text, Font).Height;
			TextHeight = TextOnlyHeight;

			if (!string.IsNullOrEmpty(SubText))
			{
				SubTextHeight = (int)g.MeasureString(SubText, FontSubText).Height + SubTextGap;
				TextWidth = Math.Max(TextWidth, (int)g.MeasureString(SubText, FontSubText).Width + 3);
				TextHeight += SubTextHeight;
			}
			TextHeightCategory = (int)g.MeasureString(Text, FontCategory).Height;
			TextHeightProperty = (int)g.MeasureString(Text, FontProperty).Height;

			if (Icon != null)
				TextWidth += Icon.Width + Gap * 3;

			int newWidth = Math.Max(TextWidth + Gap * 2, MinimumWidth);
			int newHeight = 5 + TextHeight + 20;
			//Height = 5 + TextHeight + 20;

			foreach (RawCategory cat in Categories)
			{
				Size catSize = cat.Draw(g, new Point(-1000, -1000), newWidth);
				newHeight += catSize.Height;
				newWidth = Math.Max(newWidth, catSize.Width + 2);
			}
			if (AutoSizeWidth)
				_Width = newWidth;

			if (newHeight > Height)
				Height = newHeight;

			RecalcSizeRequired = false;
			return newWidth;
		}

		public virtual void Draw(Graphics g)
		{
			if (RecalcSizeRequired)
				ReCalculateSize(g);

			Draw(g, new Point(Left, Top), Width, Height);
		}

		private void Draw(Graphics g, Point pos, int width, int height)
		{
			Point startPos = pos;

			if (HasFocus)
			{
				Pen pen = new Pen(FocusBorderColor, BorderWidth);
				DrawRect(g, pen, pos.X, pos.Y, width, height, 3);
			}
			else
			{
				Pen pen = new Pen(BorderColor, BorderWidth);
				DrawRect(g, pen, pos.X, pos.Y, width, height, 3);
			}

			pos.Offset(0, 5);

			if (Icon == null)
			{
				//pos.Offset(Convert.ToInt32((width - TextWidth) / 2), Convert.ToInt32((height - TextHeight) / 2));
				pos.Offset(Convert.ToInt32((width - TextWidth) / 2), 0);
			}
			else
			{
				pos.Offset(5, 0);
				Point imagePos = pos;
				g.DrawImage(Icon, new Rectangle(imagePos, new Size(Icon.Width, Icon.Height)));
				pos.Offset(Icon.Width + 5, 0);
			}
			g.DrawString(Text, Font, TextBrush, pos);

			if (!string.IsNullOrEmpty(SubText))
			{
				Point subTextPos = pos;
				subTextPos.Offset(0, TextOnlyHeight + SubTextGap);
				g.DrawString(SubText, FontSubText, TextBrush, subTextPos);
			}
			int y = pos.Y;
			pos = startPos;
			pos.Offset(1, y - pos.Y + TextHeight + 10);

			foreach (RawCategory category in Categories)
			{
				category.BorderColor = this.BackColor1;
				int categoryHeight = category.Draw(g, pos, this.Width - 2).Height;
				pos.Offset(0, categoryHeight);
			}
			pos.Offset(2, 0);

			foreach (RawShape innerShape in InnerShapes)
			{
				innerShape.Draw(g, pos, this.Width - 4, 25);
			}
		}

		private void DrawRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
		{
			Point top = new Point((int)X, (int)Y);
			Point bottom = new Point((int)X, (int)(Y + height));
			GPath = new GraphicsPath();

			if (RoundedCorners)
			{
				GPath.AddLine(X + radius, Y, X + width - (radius * 2), Y);
				GPath.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
				GPath.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
				GPath.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
				GPath.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
				GPath.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
				GPath.AddLine(X, Y + height - (radius * 2), X, Y + radius);
				GPath.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
				GPath.CloseFigure();
			}
			else
			{
				GPath.AddRectangle(new Rectangle(top, new Size((int)width, (int)height)));
			}
			if (HasFocus)
				g.FillPath(new LinearGradientBrush(top, bottom, FocusBackColor1, FocusBackColor2), GPath);
			else
				g.FillPath(new LinearGradientBrush(top, bottom, BackColor1, BackColor2), GPath);

			g.DrawPath(p, GPath);

			// Increase size of GPath for hit-testing purposes
			GPath = new GraphicsPath();
			top.Offset(-2, -2);
			GPath.AddRectangle(new Rectangle(top, new Size((int)width + 4, (int)height + 4)));
		}

		public bool HasFocus
		{
			get { return _HasFocus; }
			set
			{
				if (_HasFocus != value)
				{
					_HasFocus = value;

					if (_HasFocus && Enter != null)
						Enter(this, null);
					else if (!_HasFocus && Leave != null)
						Leave(this, null);
				}
			}
		}

		internal void RaiseMouseClick(MouseEventArgs e)
		{
			if (MouseClick != null)
				MouseClick(this, e);
		}

		private void RaiseDoubleClick(MouseEventArgs e)
		{
			if (MouseDoubleClick != null)
				MouseDoubleClick(this, e);
		}

		internal void ProcessDoubleClick(MouseEventArgs e)
		{
			if (this.GPath != null &&
				this.GPath.IsVisible(e.X, e.Y))
			{
				this.HasFocus = true;
				this.RaiseDoubleClick(e);
			}
			else
				this.HasFocus = false;

			foreach (RawCategory category in Categories)
			{
				if (category.Bounds.Contains(e.Location))
				{
					category.ProcessMouseDoubleClick(e);
				}
			}
		}

		internal LinkLine ProcessMouseClick(MouseEventArgs e)
		{
			LinkLine focusedLinkLine = null;

			if (this.GPath != null &&
				this.GPath.IsVisible(e.X, e.Y))
			{
				bool subShapeHasClick = false;

				foreach (RawCategory cat in this.Categories)
				{
					//if (cat.Bounds.Contains(e.Location))
					//{
					//    cat.ProcessMouseClick(e);
					//    subShapeHasClick = true;
					//    break;
					//}
					if (!Canvas.CtrlKeyDown && e.Button == MouseButtons.Left)
						cat.SelectedProperties.Clear();

					foreach (RawProperty prop in cat.Properties)
					{
						if (prop.Bounds.Contains(e.Location))
						{
							if (e.Button == MouseButtons.Left)
							{
								if (prop.IsSelected)
									cat.SelectedProperties.Remove(prop);
								else
								{

									cat.SelectedProperties.Add(prop);
									prop.ProcessMouseClick(e);
									subShapeHasClick = true;
								}
							}
							else if (e.Button == MouseButtons.Right)
							{
								prop.ProcessMouseClick(e);
								subShapeHasClick = true;
							}
							// Break if ctrl key down, because we don't need to 'de-select' the other properties
							if (Canvas.CtrlKeyDown && e.Button == MouseButtons.Left)
								break;
						}
						else if (!Canvas.CtrlKeyDown &&
								 !Canvas.ShiftKeyDown &&
								 e.Button == MouseButtons.Left)
						{
							prop.IsSelected = false;
						}
					}
					if (e.Button == MouseButtons.Right &&
						cat.SelectedProperties.Count > 0 &&
						cat.Bounds.Contains(e.Location))
					{
						if (!cat.GPathHeader.IsVisible(e.X, e.Y))
							cat.RaiseMultiPropertiesClicked();

						subShapeHasClick = true;
					}
					else if (e.Button == MouseButtons.Right &&
						cat.Bounds.Contains(e.Location))
					{
						cat.ProcessMouseClick(e);
					}
				}
				this.HasFocus = true;

				if (!subShapeHasClick)
					this.RaiseMouseClick(e);
			}
			else
				this.HasFocus = false;

			if (this.OriginatingLineStyle != null)
			{
				if (this.OriginatingLineStyle.GPathMiddleImage != null &&
					this.OriginatingLineStyle.GPathMiddleImage.IsVisible(e.X, e.Y))
				{
					this.OriginatingLineStyle.IsFocused = true;
					this.OriginatingLineStyle.RaiseMiddleImageClick(e);
					focusedLinkLine = this.OriginatingLineStyle;
				}
				else if (this.OriginatingLineStyle.GPathStartImage != null &&
					this.OriginatingLineStyle.GPathStartImage.IsVisible(e.X, e.Y))
				{
					this.OriginatingLineStyle.IsFocused = true;
					this.OriginatingLineStyle.RaiseStartImageClick(e);
					focusedLinkLine = this.OriginatingLineStyle;
				}
				else if (this.OriginatingLineStyle.GPathEndImage != null &&
					this.OriginatingLineStyle.GPathEndImage.IsVisible(e.X, e.Y))
				{
					this.OriginatingLineStyle.IsFocused = true;
					this.OriginatingLineStyle.RaiseEndImageClick(e);
					focusedLinkLine = this.OriginatingLineStyle;
				}
				else if (this.OriginatingLineStyle.GPath != null &&
					this.OriginatingLineStyle.GPath.IsVisible(e.X, e.Y))
				{
					this.OriginatingLineStyle.IsFocused = true;
					this.OriginatingLineStyle.RaiseMouseClick(e);
					focusedLinkLine = this.OriginatingLineStyle;
				}
				else
					this.OriginatingLineStyle.IsFocused = false;
			}
			foreach (RawCategory category in Categories)
			{
				if (category.Bounds.Contains(e.Location))
				{
					category.ProcessMouseClick(e);
				}
			}
			return focusedLinkLine;
		}

		internal void DeFocusAllLines()
		{
			if (this.OriginatingLineStyle != null)
				this.OriginatingLineStyle.IsFocused = false;
		}

		internal void ProcessMouseMove(MouseEventArgs e, ref bool mustRedraw, ref bool oneIsAlreadyFocused, ref ShapeCanvas.LineEndWithFocus TempFocusedLineEnd)
		{
			if (this.GPath == null)
				return;

			bool isFocused = false;
			bool shapeHasFocus = this.GPath.IsVisible(e.X, e.Y);

			if (this.HasFocus != shapeHasFocus)
			{
				this.HasFocus = shapeHasFocus;
				mustRedraw = true;
			}
			if (oneIsAlreadyFocused)
				isFocused = false;
			else if (this.OriginatingLineStyle != null &&
				this.OriginatingLineStyle.GPath != null)
				isFocused = this.OriginatingLineStyle.GPath.IsVisible(e.X, e.Y);

			if (this.OriginatingLineStyle != null)
			{
				if (this.OriginatingLineStyle.IsFocused != isFocused)
				{
					this.OriginatingLineStyle.IsFocused = isFocused;
					mustRedraw = true;

					if (this.OriginatingLineStyle.IsFocused)
						this.OriginatingLineStyle.RaiseMouseEnter(e);
					else
						this.OriginatingLineStyle.RaiseMouseLeave(e);
				}
				if (this.OriginatingLineStyle.GPathFinishEnd != null &&
					this.OriginatingLineStyle.GPathFinishEnd.IsVisible(e.X, e.Y))
				{
					this.OriginatingLineStyle.RaiseMouseOverEnd1(e);
					TempFocusedLineEnd = new ShapeCanvas.LineEndWithFocus(this.OriginatingLineStyle, ShapeCanvas.LineEndWithFocus.EndTypes.End);
				}
				else if (this.OriginatingLineStyle.GPathStartEnd != null &&
					this.OriginatingLineStyle.GPathStartEnd.IsVisible(e.X, e.Y))
				{
					this.OriginatingLineStyle.RaiseMouseOverEnd2(e);
					TempFocusedLineEnd = new ShapeCanvas.LineEndWithFocus(this.OriginatingLineStyle, ShapeCanvas.LineEndWithFocus.EndTypes.Start);
				}
				if (this.OriginatingLineStyle.GPathMiddleImage != null &&
					this.OriginatingLineStyle.GPathMiddleImage.IsVisible(e.X, e.Y))
					this.OriginatingLineStyle.RaiseMouseOverMiddleImage(e);
			}
			if (isFocused) oneIsAlreadyFocused = true;

			foreach (RawCategory category in Categories)
				category.ProcessMouseMove(e, ref mustRedraw);
		}

	}
}
