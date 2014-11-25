using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class LinkLine
	{
		public delegate string GetTextDelegate(object dataObject);
		public delegate void MouseEndDelegate(object sender, MouseEventArgs e, Rectangle endBounds);
		public GetTextDelegate StartTextFunction;
		public GetTextDelegate MiddleTextFunction;
		public GetTextDelegate EndTextFunction;

		public event MouseEventHandler MouseClick;
		public event MouseEndDelegate MouseOverEnd1;
		public event MouseEndDelegate MouseOverEnd2;
		public event MouseEventHandler MouseEnter;
		public event MouseEventHandler MouseLeave;
		public event MouseEventHandler MiddleImageClick;
		public event MouseEndDelegate StartImageClick;
		public event MouseEndDelegate EndImageClick;
		public event MouseEndDelegate MouseOverMiddleImage;

		protected Pen Pen;
		private Color _BackColor = Color.Pink;
		private Brush BackBrush = new SolidBrush(Color.Pink);
		public Color LineColor = Color.DarkGray;
		private System.Drawing.Drawing2D.AdjustableArrowCap ArrowCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 5, true);
		public DashStyle LineStyle = DashStyle.Solid;
		private bool _IsFocused = false;
		public RawShape Parent;
		public System.Drawing.Drawing2D.GraphicsPath GPath;
		public System.Drawing.Drawing2D.GraphicsPath GPathMiddleImage;
		public System.Drawing.Drawing2D.GraphicsPath GPathFinishEnd;
		public System.Drawing.Drawing2D.GraphicsPath GPathStartEnd;
		public System.Drawing.Drawing2D.GraphicsPath GPathStartImage;
		public System.Drawing.Drawing2D.GraphicsPath GPathEndImage;
		public CustomLineCap StartCap = LineCaps.None;
		public CustomLineCap EndCap = LineCaps.None;
		private Color _ForeColor = Color.White;
		private Brush TextBrush = new SolidBrush(Color.White);
		private string _StartText;
		private string _EndText;
		private string _MiddleText;
		public bool ShowMiddleTextOnlyWhenFocused = true;
		public bool ShowEndTextOnlyWhenFocused = false;
		public Image MiddleImage = null;
		public Image MiddleImageFocused = null;
		private Brush WhiteBrush = new SolidBrush(Color.White);
		public object DataObject;
		public String StartTextDataMember = "";
		public String EndTextDataMember = "";
		public String MiddleTextDataMember = "";
		private Rectangle EndRectangle1;
		private Rectangle EndRectangle2;
		private Rectangle MiddleImageRectangle;
		private Rectangle StartImageRectangle;
		private Rectangle EndImageRectangle;
		public string DefaultEndText = "";
		public Image StartImage;
		public Image EndImage;

		public LinkLine()
		{
			this.LineColor = Color.DarkGray;
			LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			//Font = font;
			//StartText = "";
			//EndText = "";
			MiddleText = "";
			this.Font = new Font(this.Font.FontFamily.Name, 7F);
		}

		public LinkLine(Font font)
		{
			this.LineColor = Color.DarkGray;
			LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			Font = font;
			//StartText = "";
			//EndText = "";
			MiddleText = "";
			this.Font = new Font(this.Font.FontFamily.Name, 7F);
		}

		public LinkLine(Font font, DashStyle lineStyle, string startText, string middleText, string endText, CustomLineCap startCap, CustomLineCap endCap)
		{
			this.LineColor = Color.DarkGray;
			LineStyle = lineStyle;
			Font = font;
			//StartText = startText;
			//EndText = endText;
			MiddleText = middleText;
			StartCap = startCap;
			EndCap = endCap;
			this.Font = new Font(this.Font.FontFamily.Name, 7F);
		}

		public virtual Font Font { get; set; }

		public Color BackColor
		{
			get { return _BackColor; }
			set
			{
				if (_BackColor != value)
				{
					_BackColor = value;
					BackBrush = new SolidBrush(value);
				}
			}
		}

		public Color ForeColor
		{
			get { return _ForeColor; }
			set
			{
				if (_ForeColor != value)
				{
					_ForeColor = value;
					TextBrush = new SolidBrush(value);
				}
			}
		}

		protected virtual Pen ArrowPen
		{
			get
			{
				if (Pen == null)
				{
					Pen = new Pen(LineColor);
					Pen.Width = 1;
					Pen.CustomStartCap = StartCap;
					Pen.CustomEndCap = EndCap;
					Pen.DashStyle = LineStyle;
				}
				Pen.Color = IsFocused ? Color.White : LineColor;
				return Pen;
			}
		}

		public string StartText
		{
			get
			{
				if (StartTextFunction != null)
				{
					string val = StartTextFunction.Invoke(DataObject);

					if (string.IsNullOrEmpty(val))
						return DefaultEndText;

					return val;
				}
				if (!string.IsNullOrEmpty(StartTextDataMember))
				{
					string val = (string)DataObject.GetType().InvokeMember(StartTextDataMember, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, DataObject, null);

					if (string.IsNullOrEmpty(val))
						return DefaultEndText;

					return val;
				}
				if (string.IsNullOrEmpty(_StartText))
					_StartText = DefaultEndText;

				return _StartText;
			}
			set { _StartText = value; }
		}

		public string EndText
		{
			get
			{
				if (EndTextFunction != null)
				{
					string val = EndTextFunction.Invoke(DataObject);

					if (string.IsNullOrEmpty(val))
						return DefaultEndText;

					return val;
				}
				if (!string.IsNullOrEmpty(EndTextDataMember))
				{
					string val = (string)DataObject.GetType().InvokeMember(EndTextDataMember, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, DataObject, null);

					if (string.IsNullOrEmpty(val))
						return DefaultEndText;

					return val;
				}
				if (string.IsNullOrEmpty(_EndText))
					_EndText = DefaultEndText;

				return _EndText;
			}
			set { _EndText = value; }
		}

		private int MiddleTextLineCount = 1;

		public string MiddleText
		{
			get
			{
				if (MiddleTextFunction != null)
					return MiddleTextFunction.Invoke(DataObject);

				if (!string.IsNullOrEmpty(MiddleTextDataMember))
					return (string)DataObject.GetType().InvokeMember(MiddleTextDataMember, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, DataObject, null);

				return _MiddleText;
			}
			set
			{
				if (_MiddleText == value)
					return;

				_MiddleText = value;
				MiddleTextLineCount = _MiddleText.Split('\n').Length;
			}
		}

		public virtual void Draw(Graphics g, Point startPoint, Point endPoint)
		{
			if (startPoint == endPoint)
				return;

			MidPoint = ShapeHelper.GetLineMidPoint(startPoint, endPoint);
			GPath = new System.Drawing.Drawing2D.GraphicsPath();
			//GPath.AddLine(startPoint, endPoint);

			Point pointA = new Point(startPoint.X + 16, startPoint.Y);
			Point pointB = new Point(endPoint.X - 16, endPoint.Y);
			GPath.AddLine(pointA, pointB);

			// OutOfMemoryException if two points equal
			if (pointA != pointB)
				try
				{
					GPath.Widen(new Pen(Color.Red, 50F));
				}
				catch
				{
					// Do nothing
				}

			g.DrawLine(ArrowPen, startPoint, endPoint);
			bool isHorizontal = startPoint.Y == endPoint.Y;
			bool isVertical = startPoint.X == endPoint.X;
			GPathFinishEnd = new GraphicsPath();
			GPathStartEnd = new GraphicsPath();

			if ((IsFocused || !ShowEndTextOnlyWhenFocused) || (!string.IsNullOrEmpty(MiddleText) && !ShowMiddleTextOnlyWhenFocused))
			{
				int textHeight = (int)Math.Ceiling(g.MeasureString("A", Font).Height);
				double angle = ShapeHelper.GetAngleBetween2PointsInDegrees(startPoint, endPoint);

				if ((IsFocused || !ShowEndTextOnlyWhenFocused) && !String.IsNullOrEmpty(StartText))
				{
					SizeF textSize = g.MeasureString(StartText, Font);
					int textLength = (int)Math.Ceiling(textSize.Width);
					Point textPoint = ShapeHelper.GetPointAlongLine(startPoint, endPoint, 35);

					if (isHorizontal)
					{
						if (startPoint.X < endPoint.X)
						{
							textPoint.X = startPoint.X + 10;
							textPoint.Y -= textHeight + 2;
						}
						else
						{
							textPoint.X = startPoint.X - textLength - 5;
							textPoint.Y = startPoint.Y + 2;
						}
					}
					else if (isVertical)
					{
						if (startPoint.Y > endPoint.Y)
						{
							textPoint.X = startPoint.X - textLength / 2;
							textPoint.Y = startPoint.Y - textHeight * 2;
						}
						else
						{
							textPoint.X = startPoint.X - textLength / 2;
							textPoint.Y = startPoint.Y + textHeight;
						}
					}
					else
					{
						textPoint.X = textPoint.X - textLength / 2;
						textPoint.Y -= textHeight / 2;
					}
					EndRectangle1 = new Rectangle(textPoint, new Size(textLength, textHeight));

					if (!isHorizontal && !isVertical)
						g.FillRectangle(BackBrush, EndRectangle1);

					g.DrawString(StartText, Font, TextBrush, textPoint);
					GPathFinishEnd.AddRectangle(EndRectangle1);
					GPathStartImage = new GraphicsPath();

					if (IsFocused && StartImage != null)
					{
						//StartImageRectangle = new Rectangle(EndRectangle1.Left - StartImage.Width - 2, EndRectangle1.Top, StartImage.Width, StartImage.Height);
						StartImageRectangle = new Rectangle(EndRectangle1.Right + 2, EndRectangle1.Top, StartImage.Width, StartImage.Height);
						g.FillRectangle(BackBrush, StartImageRectangle);
						g.DrawImage(StartImage, StartImageRectangle);
						GPathStartImage.AddRectangle(StartImageRectangle);
					}
				}
				if ((IsFocused || !ShowEndTextOnlyWhenFocused) && !string.IsNullOrEmpty(EndText))
				{
					int textLength = (int)Math.Ceiling(g.MeasureString(EndText, Font).Width);
					Point textPoint = ShapeHelper.GetPointAlongLine(endPoint, startPoint, 35);

					if (isHorizontal)
					{
						if (startPoint.X < endPoint.X)
						{
							textPoint.X = endPoint.X - textLength - 5;
							textPoint.Y = endPoint.Y + 2;
						}
						else
						{
							textPoint.X = endPoint.X + 10;
							textPoint.Y -= textHeight + 2;
						}
					}
					else if (isVertical)
					{
						if (startPoint.Y > endPoint.Y)
						{
							textPoint.X = endPoint.X - textLength / 2;
							textPoint.Y = endPoint.Y + textHeight;
						}
						else
						{
							textPoint.X = endPoint.X - textLength / 2;
							textPoint.Y = endPoint.Y - textHeight * 2;
						}
					}
					else
					{
						textPoint.X = textPoint.X - textLength / 2;
						textPoint.Y -= textHeight / 2;
					}
					EndRectangle2 = new Rectangle(textPoint, new Size(textLength, textHeight));

					if (!isHorizontal && !isVertical)
						g.FillRectangle(BackBrush, EndRectangle2);

					g.DrawString(EndText, Font, TextBrush, textPoint);
					GPathStartEnd.AddRectangle(EndRectangle2);

					GPathEndImage = new GraphicsPath();

					if (IsFocused && StartImage != null)
					{
						//EndImageRectangle = new Rectangle(EndRectangle2.Right + 2, EndRectangle2.Bottom - EndImage.Height, EndImage.Width, EndImage.Height);
						EndImageRectangle = new Rectangle(EndRectangle2.Left - EndImage.Width - 2, EndRectangle2.Bottom - EndImage.Height, EndImage.Width, EndImage.Height);
						g.FillRectangle(BackBrush, EndImageRectangle);
						g.DrawImage(EndImage, EndImageRectangle);
						GPathEndImage.AddRectangle(EndImageRectangle);
					}
				}
				if (!string.IsNullOrEmpty(MiddleText) &&
					(IsFocused || !ShowMiddleTextOnlyWhenFocused))
				{
					int textLength = (int)Math.Ceiling(g.MeasureString(MiddleText, Font).Width);
					Point textPoint = MidPoint;

					if (isHorizontal)
					{
						textPoint.X = textPoint.X - textLength / 2;
						textPoint.Y -= textHeight + 2;

						if (MiddleImage != null)
							textPoint.Y -= MiddleImage.Height;
					}
					else
					{
						textPoint.X = textPoint.X - textLength / 2;
						textPoint.Y -= textHeight * MiddleTextLineCount + MiddleImage.Height / 2 + 2;
					}
					g.FillRectangle(BackBrush, new Rectangle(textPoint.X, textPoint.Y, textLength, textHeight * MiddleTextLineCount));
					g.DrawString(MiddleText, Font, TextBrush, textPoint);
				}
			}
			GPathMiddleImage = new System.Drawing.Drawing2D.GraphicsPath();
			Image image;

			if (!IsFocused)
				image = MiddleImage;
			else
				image = MiddleImageFocused != null ? MiddleImageFocused : MiddleImage;

			if (image != null)
			{
				Point imagePos = MidPoint;
				imagePos.Offset(-1 * image.Width / 2, -1 * image.Height / 2);
				MiddleImageRectangle = new Rectangle(imagePos, image.Size);

				g.FillRectangle(BackBrush, MiddleImageRectangle);
				g.DrawImage(image, MiddleImageRectangle);
				GPathMiddleImage.AddRectangle(MiddleImageRectangle);
			}
			//#region Add circles at each end
			//int radius = 20;
			//Point circleCentre = ShapeHelper.GetPointAlongLine(startPoint, endPoint, radius);
			//GPathEnd1.AddPath(ShapeHelper.GetCirclePath(circleCentre, radius), false);
			////g.DrawPath(new Pen(TextBrush), GPathEnd1);

			//circleCentre = ShapeHelper.GetPointAlongLine(endPoint, startPoint, radius);
			//GPathEnd2.AddPath(ShapeHelper.GetCirclePath(circleCentre, radius), false);
			////g.DrawPath(new Pen(TextBrush), GPathEnd2);
			//#endregion
		}

		public Point MidPoint { get; private set; }

		public virtual bool IsFocused
		{
			get { return _IsFocused; }
			set
			{
				if (_IsFocused != value)
				{
					_IsFocused = value;
				}
			}
		}

		internal void RaiseMouseClick(MouseEventArgs e)
		{
			if (MiddleImage != null && MouseClick != null)
				MouseClick(this, e);
		}

		internal void RaiseMouseOverEnd1(MouseEventArgs e)
		{
			if (MouseOverEnd1 != null)
				MouseOverEnd1(this, e, EndRectangle1);
		}

		internal void RaiseMouseOverEnd2(MouseEventArgs e)
		{
			if (MouseOverEnd2 != null)
				MouseOverEnd2(this, e, EndRectangle2);
		}

		internal void RaiseMouseOverMiddleImage(MouseEventArgs e)
		{
			if (MouseOverMiddleImage != null)
				MouseOverMiddleImage(this, e, MiddleImageRectangle);
		}

		internal void RaiseMouseEnter(MouseEventArgs e)
		{
			if (MouseEnter != null)
				MouseEnter(this, e);
		}

		internal void RaiseMouseLeave(MouseEventArgs e)
		{
			if (MouseLeave != null)
				MouseLeave(this, e);
		}

		internal void RaiseMiddleImageClick(MouseEventArgs e)
		{
			if (MiddleImageClick != null)
				MiddleImageClick(this, e);
		}

		internal void RaiseStartImageClick(MouseEventArgs e)
		{
			if (StartImageClick != null)
				StartImageClick(this, e, StartImageRectangle);
		}

		internal void RaiseEndImageClick(MouseEventArgs e)
		{
			if (EndImageClick != null)
				EndImageClick(this, e, EndImageRectangle);
		}
	}
}
