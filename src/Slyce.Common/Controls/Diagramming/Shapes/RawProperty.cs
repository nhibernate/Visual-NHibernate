using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class RawProperty
	{
		public enum ImageTypes
		{
			None,
			Circle,
			Key
		}
		public event MouseEventHandler Click;
		public event MouseEventHandler DoubleClick;
		public event EventHandler Enter;
		public event EventHandler Leave;
		public event EventHandler ExpandedChanged;

		public string Text;
		public List<RawProperty> Properties = new List<RawProperty>();
		public Rectangle Bounds;
		private bool _IsFocused = false;
		public Color BackColor = Color.White;
		public Color BackColorFocused = Color.LightGreen;
		private Brush _BackBrush = null;
		private Brush _ImageBrush = null;
		private Pen _ImagePen = null;
		private Brush _SelectedBrush = null;
		public object Tag;
		public ImageTypes ImageType = ImageTypes.None;
		public Color ImageColor = Color.Black;
		public Color SelectedImageColor = Color.Orange;
		public List<RawProperty> SubProperties = new List<RawProperty>();
		private bool _IsExpanded = false;
		public bool AllowSelection = false;

		public RawProperty(string text, object tag)
		{
			Text = text;
			Tag = tag;
		}

		private bool _IsSelected = false;

		public bool IsSelected
		{
			get { return _IsSelected; }
			set
			{
				if (_IsSelected != value)
				{
					_IsSelected = value;
					_BackBrush = null;
				}
			}
		}

		public bool IsFocused
		{
			get { return _IsFocused; }
			set
			{
				if (_IsFocused != value)
				{
					_IsFocused = value;
					_BackBrush = null;

					if (_IsFocused && Enter != null)
						Enter(this, null);
					else if (!_IsFocused && Leave != null)
						Leave(this, null);
				}
			}
		}

		private Brush BackBrush
		{
			get
			{
				if (_BackBrush == null)
				{
					if (IsFocused)
						_BackBrush = new SolidBrush(BackColorFocused);
					else if (IsSelected && AllowSelection)
						_BackBrush = SelectedBrush;
					else
						_BackBrush = new SolidBrush(BackColor);
				}
				return _BackBrush;
			}
		}

		private Brush ImageBrush
		{
			get
			{
				if (_ImageBrush == null)
				{
					_ImageBrush = new SolidBrush(ImageColor);
				}
				return _ImageBrush;
			}
		}

		private Pen ImagePen
		{
			get
			{
				if (_ImagePen == null)
				{
					_ImagePen = new Pen(ImageColor, 1.7F);
				}
				return _ImagePen;
			}
		}

		private Brush SelectedBrush
		{
			get
			{
				if (_SelectedBrush == null)
				{
					_SelectedBrush = new SolidBrush(SelectedImageColor);
				}
				return _SelectedBrush;
			}
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

		public int NumberOfVisibleSubProperties
		{
			get
			{
				if (!IsExpanded)
					return 0;

				int count = SubProperties.Count;

				foreach (RawProperty subProp in SubProperties)
					count += subProp.NumberOfVisibleSubProperties;

				return count;
			}
		}

		public int NumberOfSubProperties
		{
			get
			{
				if (!IsExpanded)
					return 0;

				int count = SubProperties.Count;

				foreach (RawProperty subProp in SubProperties)
					count += subProp.SubProperties.Count;

				return count;
			}
		}

		public void Draw(Graphics g, Point pos, Font font, Brush textBrush, int textHeight)
		{
			g.FillRectangle(BackBrush, Bounds);

			if (SubProperties.Count > 0)
			{
				Point trianglePoint = new Point(pos.X - 8, pos.Y + 6);
				RawCategory.DrawTriangle(g, trianglePoint, IsExpanded, textBrush);
			}
			//if (IsSelected)
			//{
			//    Point selectedPoint = new Point(Bounds.X + 8, pos.Y + 4);
			//    //Point selectedPoint = new Point(Bounds.X + 8, Bounds.Y);
			//    g.FillEllipse(SelectedBrush, selectedPoint.X, selectedPoint.Y, 5, 5);
			//    //DrawImage(g, selectedPoint, ImageTypes.Circle, SelectedBrush);
			//}
			g.DrawString(Text, font, textBrush, pos);
			DrawImage(g, pos, ImageType, ImageBrush);

			if (IsExpanded)
			{
				Point startPos = pos;
				pos.Offset(8, textHeight + 1);

				foreach (RawProperty prop in SubProperties)
				{
					prop.Bounds = new Rectangle(Bounds.X, pos.Y, Bounds.Width, Bounds.Height - (pos.Y - startPos.Y));
					prop.Draw(g, pos, font, textBrush, textHeight);
					pos.Offset(0, textHeight);
				}
				//this.Bounds.Height = textHeight + 1;
			}
		}

		private void DrawImage(Graphics g, Point pos, ImageTypes imageType, Brush brush)
		{
			int radius = 2;

			switch (imageType)
			{
				case ImageTypes.None:
					break;
				case ImageTypes.Circle:
					g.FillEllipse(brush, pos.X - radius * 2 - 3, Bounds.Top + Convert.ToInt32((Bounds.Bottom - Bounds.Top) / 2) - radius, radius * 2, radius * 2);
					break;
				case ImageTypes.Key:
					Rectangle circleBounds = new Rectangle(pos.X - radius * 8, Bounds.Top + Convert.ToInt32((Bounds.Bottom - Bounds.Top) / 2) - radius, radius * 2, radius * 2);
					g.DrawEllipse(ImagePen, circleBounds);

					Point startPoint = new Point(circleBounds.Right, circleBounds.Bottom - circleBounds.Height / 2);
					Point endPoint = new Point(circleBounds.Right + 6, circleBounds.Bottom - circleBounds.Height / 2);
					g.DrawLine(ImagePen, startPoint, endPoint);

					startPoint.Offset(3, 0);
					endPoint = new Point(startPoint.X, startPoint.Y + 3);
					g.DrawLine(ImagePen, startPoint, endPoint);

					startPoint.Offset(3, 0);
					endPoint = new Point(startPoint.X, startPoint.Y + 3);
					g.DrawLine(ImagePen, startPoint, endPoint);
					break;
				default:
					throw new NotImplementedException("Not handled yet");
			}
		}

		internal void ProcessMouseMove(MouseEventArgs e, ref bool mustRedraw)
		{
			bool isFocused;

			if (IsExpanded)
			{
				// Check all sub-properties
				for (int i = SubProperties.Count - 1; i >= 0; i--)
				{
					SubProperties[i].ProcessMouseMove(e, ref mustRedraw);

					if (SubProperties[i].IsFocused)
					{
						foreach (var sibling in SubProperties)
						{
							if (sibling != SubProperties[i])
								sibling.IsFocused = false;
						}
						if (IsFocused)
						{
							IsFocused = false;
							mustRedraw = true;
						}
						return;
					}
				}
			}
			isFocused = Bounds.Contains(e.Location);

			if (IsFocused != isFocused)
			{
				IsFocused = isFocused;
				mustRedraw = true;
			}
		}

		internal void ProcessMouseClick(MouseEventArgs e)
		{
			if (SubProperties.Count == 0)
			{
				if (e.Button == MouseButtons.Left)
					IsSelected = !IsSelected;

				if (Click != null)
					Click(this, e);
			}
			else
			{
				for (int i = SubProperties.Count - 1; i >= 0; i--)
				{
					if (SubProperties[i].Bounds.Contains(e.Location))
					{
						SubProperties[i].ProcessMouseClick(e);
						return;
					}
				}
				if (e.Button == MouseButtons.Left)
					IsExpanded = !IsExpanded;

				if (Click != null)
					Click(this, e);
			}
		}

		internal void ProcessMouseDoubleClick(MouseEventArgs e)
		{
			if (DoubleClick != null)
				DoubleClick(this, e);
		}
	}
}
