using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class RawCategory
	{
		public delegate void MultiPropertiesClickedDelegate(RawCategory category, List<RawProperty> selectedProperties);

		public event MultiPropertiesClickedDelegate MultiPropertiesClicked;
		public event EventHandler ExpandedChanged;

		public RawShape Parent;
		public string Text;
		private Color _ForeColor = Color.Black;
		private Color _BackColor = Color.White;
		private Color _BorderColor = Color.Black;
		private Pen _BorderPen = null;
		private Pen PlusPen = new Pen(Color.Black, 1.6F);
		private Brush _BackgroundBrush = null;
		private Brush _HeaderBackgroundBrush = null;
		private Brush _TextBrush = null;
		public Font Font = null;
		public Font FontProperty = null;
		public List<RawProperty> Properties = new List<RawProperty>();
		private Rectangle CategoryRectangle;
		internal GraphicsPath GPathHeader = new GraphicsPath();
		public Rectangle Bounds;
		private bool _HeaderIsFocused = false;
		public Color _HeaderColor = Color.LightGray;
		public Color _HeaderColorFocused = Color.LightGoldenrodYellow;
		private bool _IsExpanded = false;
		public List<RawProperty> SelectedProperties = new List<RawProperty>();

		public RawCategory(string text, Font font, RawShape parent)
		{
			Text = text;
			Font = font;
			FontProperty = Font;
			Parent = parent;
		}

		public RawCategory(string text, Font font, Font fontProperty, RawShape parent)
		{
			Text = text;
			Font = font;
			FontProperty = fontProperty;
			Parent = parent;
		}

		public bool IsExpanded
		{
			get { return _IsExpanded; }
			set
			{
				if (_IsExpanded != value)
				{
					_IsExpanded = value;

					if (ExpandedChanged != null)
						ExpandedChanged(this, null);
				}
			}
		}

		private Pen BorderPen
		{
			get
			{
				if (_BorderPen == null)
				{
					_BorderPen = new Pen(BorderColor);
				}
				return _BorderPen;
			}
		}

		public Color BorderColor
		{
			get { return _BorderColor; }
			set
			{
				if (_BorderColor != value)
				{
					_BorderColor = value;
					_BorderPen = null;
				}
			}
		}

		public Color HeaderColor
		{
			get { return _HeaderColor; }
			set
			{
				if (_HeaderColor != value)
				{
					_HeaderColor = value;
					_HeaderBackgroundBrush = null;
				}
			}
		}

		public bool HeaderIsFocused
		{
			get { return _HeaderIsFocused; }
			set
			{
				if (_HeaderIsFocused != value)
				{
					_HeaderIsFocused = value;
					_HeaderBackgroundBrush = null;
				}
			}
		}

		public Color HeaderColorFocused
		{
			get { return _HeaderColorFocused; }
			set
			{
				if (_HeaderColorFocused != value)
				{
					_HeaderColorFocused = value;
					_HeaderBackgroundBrush = null;
				}
			}
		}

		public Color BackColor
		{
			get { return _BackColor; }
			set
			{
				if (_BackColor != value)
				{
					_BackColor = value;
					_BackgroundBrush = null;
				}
			}
		}

		public Brush BackgroundBrush
		{
			get
			{
				if (_BackgroundBrush == null)
				{
					_BackgroundBrush = new SolidBrush(BackColor);
				}
				return _BackgroundBrush;
			}
		}

		public Brush HeaderBackgroundBrush
		{
			get
			{
				if (_HeaderBackgroundBrush == null)
				{
					if (HeaderIsFocused)
						_HeaderBackgroundBrush = new SolidBrush(HeaderColorFocused);
					else
						_HeaderBackgroundBrush = new SolidBrush(HeaderColor);
				}
				return _HeaderBackgroundBrush;
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
					_TextBrush = null;
				}
			}
		}

		public Brush TextBrush
		{
			get
			{
				if (_TextBrush == null)
				{
					_TextBrush = new SolidBrush(ForeColor);
				}
				return _TextBrush;
			}
		}

		public Size Draw(Graphics g, Point location, int width)
		{
			Point pos = location;
			Point startPos = pos;
			int propertyOffset = 24;
			int textHeight = Convert.ToInt32(g.MeasureString("AAA", Font).Height);
			int propertyTextHeight = Convert.ToInt32(g.MeasureString("AAA", FontProperty).Height);
			int maxPropertyTextWidth = propertyOffset + GetMaxPropertyTextWidth(g, Properties.Select(p => p.Text).ToList(), FontProperty);
			maxPropertyTextWidth = Math.Max(maxPropertyTextWidth, Convert.ToInt32(g.MeasureString(this.Text, Font).Width));

			if (maxPropertyTextWidth > width)
				width = maxPropertyTextWidth + 1;

			Rectangle rectProperties = new Rectangle();

			if (IsExpanded)
			{
				int rectHeight = textHeight + 10;

				foreach (var prop in Properties)
					rectHeight += (propertyTextHeight + 2) * (1 + prop.NumberOfSubProperties);

				rectProperties = new Rectangle(pos, new Size(width, rectHeight));
				g.FillRectangle(BackgroundBrush, rectProperties);
				//g.DrawRectangle(BorderPen, rectProperties);
			}
			CategoryRectangle = new Rectangle(pos, new Size(width, textHeight + 2));
			GPathHeader.ClearMarkers();
			GPathHeader.AddRectangle(CategoryRectangle);
			g.FillRectangle(HeaderBackgroundBrush, CategoryRectangle);
			//g.DrawRectangle(BorderPen, rect);
			pos.Offset(2, 0);

			Point trianglePoint = new Point(pos.X, pos.Y + 6);
			DrawTriangle(g, trianglePoint, IsExpanded, TextBrush);

			pos.Offset(9, 0);
			g.DrawString(Text, Font, TextBrush, pos);

			if (IsExpanded)
			{
				pos.Offset(0, textHeight);
				pos.Offset(13, 2);

				foreach (RawProperty prop in Properties)
				{
					prop.Bounds = new Rectangle(startPos.X, pos.Y, CategoryRectangle.Width, textHeight * (1 + prop.NumberOfVisibleSubProperties));
					prop.Draw(g, pos, FontProperty, TextBrush, textHeight);
					pos.Offset(0, prop.Bounds.Height);
				}
			}
			Bounds = IsExpanded ? rectProperties : CategoryRectangle;
			Size size = new Size(width, IsExpanded ? rectProperties.Height : CategoryRectangle.Height);
			return size;
		}

		private int GetMaxPropertyTextWidth(Graphics g, List<string> texts, Font font)
		{
			float max = 0;

			foreach (string text in texts)
			{
				max = Math.Max(max, g.MeasureString(text, font).Width);
			}
			return Convert.ToInt32(max);
		}

		internal static void DrawTriangle(Graphics g, Point point, bool isVertical, Brush textBrush)
		{
			int size = 4;
			GraphicsPath path = new GraphicsPath();

			if (isVertical)
			{
				Point point1 = new Point(point.X, point.Y - size);
				Point point2 = new Point(point.X + size * 2, point.Y - size);
				Point point3 = new Point(point.X + size, point.Y + size);
				path.AddPolygon(new Point[] { point1, point2, point3 });
			}
			else
			{
				Point point1 = new Point(point.X, point.Y - size);
				Point point2 = new Point(point.X + size * 2, point.Y);
				Point point3 = new Point(point.X, point.Y + size);
				path.AddPolygon(new Point[] { point1, point2, point3 });
			}
			g.FillPath(textBrush, path);
		}

		internal void ProcessMouseMove(MouseEventArgs e, ref bool mustRedraw)
		{
			bool headerIsFocused = CategoryRectangle.Contains(e.Location);

			if (HeaderIsFocused != headerIsFocused)
			{
				HeaderIsFocused = headerIsFocused;
				mustRedraw = true;
			}
			foreach (RawProperty prop in Properties)
			{
				//if (prop.Bounds.Contains(e.Location))
				prop.ProcessMouseMove(e, ref mustRedraw);
			}
		}

		internal void RaiseMultiPropertiesClicked()
		{
			if (MultiPropertiesClicked != null)
				MultiPropertiesClicked(this, SelectedProperties);
		}

		internal void ProcessMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left &&
				CategoryRectangle.Contains(e.Location))
			{
				IsExpanded = !IsExpanded;
				Parent.RecalcSizeRequired = true;
			}
			//if (IsExpanded)
			//{
			//    for (int i = Properties.Count - 1; i >= 0; i--)
			//    {
			//        RawProperty prop = Properties[i];

			//        if (prop.Bounds.Contains(e.Location))
			//        {
			//            prop.ProcessMouseClick(e);
			//            Parent.RecalcSizeRequired = true;
			//        }
			//    }
			//}
		}

		internal void ProcessMouseDoubleClick(MouseEventArgs e)
		{
			if (CategoryRectangle.Contains(e.Location))
			{
				IsExpanded = !IsExpanded;
				Parent.RecalcSizeRequired = true;
			}
			if (IsExpanded)
			{
				for (int i = Properties.Count - 1; i >= 0; i--)
				{
					RawProperty prop = Properties[i];

					if (prop.Bounds.Contains(e.Location))
						prop.ProcessMouseDoubleClick(e);
				}
			}
		}

	}
}
