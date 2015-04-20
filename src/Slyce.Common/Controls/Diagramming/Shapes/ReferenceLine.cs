using System;
using System.Drawing;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class ReferenceLine : LinkLine
	{
		private Brush TextBrush = new SolidBrush(Color.Black);
		private string _StartText;
		private string _EndText;
		private string _MiddleText;

		public ReferenceLine(Font font)
		{
			this.LineColor = Color.DarkGray;
			LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			Font = font;
			StartText = "startetxt";
			EndText = "endtext";
			MiddleText = "1:n";
			this.Font = new Font(this.Font, FontStyle.Bold);
		}

		public string StartText
		{
			get { return _StartText; }
			set { _StartText = value; }
		}

		public string EndText
		{
			get { return _EndText; }
			set { _EndText = value; }
		}

		public string MiddleText
		{
			get { return _MiddleText; }
			set { _MiddleText = value; }
		}

		public override void Draw(Graphics g, Point startPoint, Point endPoint)
		{
			GPath = new System.Drawing.Drawing2D.GraphicsPath();
			GPath.AddLine(startPoint, endPoint);

			// OutOfMemoryException if two points equal
			if (startPoint != endPoint)
				try
				{
					GPath.Widen(new Pen(Color.Red, 50F));
				}
				catch
				{
					// Do nothing
				}

			g.DrawLine(ArrowPen, startPoint, endPoint);

			if (IsFocused || !string.IsNullOrEmpty(MiddleText))
			{
				int textHeight = (int)Math.Ceiling(g.MeasureString("A", Font).Height);
				double angle = ShapeHelper.GetAngleBetween2PointsInDegrees(startPoint, endPoint);

				if (IsFocused && !String.IsNullOrEmpty(StartText))
				{
					int textLength = (int)Math.Ceiling(g.MeasureString(StartText, Font).Width);
					Point textPoint = ShapeHelper.GetPointOnCircle(startPoint, 40, angle);
					textPoint.X = textPoint.X - textLength / 2;
					textPoint.Y -= textHeight / 2;
					g.FillRectangle(new SolidBrush(Color.White), new Rectangle(textPoint.X, textPoint.Y, textLength, textHeight));
					g.DrawString(StartText, Font, TextBrush, textPoint);
				}
				if (IsFocused && !string.IsNullOrEmpty(EndText))
				{
					int textLength = (int)Math.Ceiling(g.MeasureString(EndText, Font).Width);
					Point textPoint = ShapeHelper.GetPointOnCircle(endPoint, -40, angle);
					textPoint.X = textPoint.X - textLength / 2;
					textPoint.Y -= textHeight / 2;
					g.FillRectangle(new SolidBrush(Color.White), new Rectangle(textPoint.X, textPoint.Y, textLength, textHeight));
					g.DrawString(EndText, Font, TextBrush, textPoint);
				}
				if (!string.IsNullOrEmpty(MiddleText))
				{
					int textLength = (int)Math.Ceiling(g.MeasureString(MiddleText, Font).Width);
					double lineLength = ShapeHelper.GetLineLength(startPoint, endPoint);
					Point textPoint = ShapeHelper.GetPointOnCircle(startPoint, (int)(lineLength / 2), angle);
					textPoint.X = textPoint.X - textLength / 2;
					textPoint.Y -= textHeight / 2;
					g.FillRectangle(new SolidBrush(Color.White), new Rectangle(textPoint.X, textPoint.Y, textLength, textHeight));
					g.DrawString(MiddleText, Font, TextBrush, textPoint);
				}
			}

		}
	}
}
