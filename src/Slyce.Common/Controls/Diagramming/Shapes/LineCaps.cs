using System.Drawing;
using System.Drawing.Drawing2D;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class LineCaps
	{
		private static CustomLineCap _SolidCircle;
		private static CustomLineCap _HollowCircle;
		private static CustomLineCap _HollowDiamond;
		private static CustomLineCap _SolidDiamond;
		private static CustomLineCap _Many;
		private static CustomLineCap _One;
		private static CustomLineCap _Zero;
		private static CustomLineCap _ZeroOrOne;
		private static CustomLineCap _SolidArrow;
		private static CustomLineCap _HollowArrow;
		private static CustomLineCap _None;

		public static CustomLineCap None
		{
			get
			{
				if (_None == null)
				{
					_None = new CustomLineCap(null, GetNonePath());
				}
				return _None;
			}
		}

		public static CustomLineCap SolidCircle
		{
			get
			{
				if (_SolidCircle == null)
				{
					int radius = 2;
					_SolidCircle = new CustomLineCap(GetCirclePath(radius), null);
					_SolidCircle.BaseInset = (float)radius;
				}
				return _SolidCircle;
			}
		}

		public static CustomLineCap HollowCircle
		{
			get
			{
				if (_HollowCircle == null)
				{
					int radius = 2;
					_HollowCircle = new CustomLineCap(null, GetCirclePath(radius));
					_HollowCircle.BaseInset = (float)radius * 2;
				}
				return _HollowCircle;
			}
		}

		public static CustomLineCap HollowDiamond
		{
			get
			{
				if (_HollowDiamond == null)
				{
					int radius = 2;
					_HollowDiamond = new CustomLineCap(null, GetDiamondPath(radius));
					_HollowDiamond.BaseInset = (float)radius * 2;
				}
				return _HollowDiamond;
			}
		}

		public static CustomLineCap SolidDiamond
		{
			get
			{
				if (_SolidDiamond == null)
				{
					int radius = 3;
					_SolidDiamond = new CustomLineCap(GetDiamondPath(radius), null);
					_SolidDiamond.BaseInset = (float)radius * 2 + radius + 2;
				}
				return _SolidDiamond;
			}
		}

		public static CustomLineCap Many
		{
			get
			{
				if (_Many == null)
				{
					int length = 5;
					_Many = new CustomLineCap(null, GetManyPath(length));
					_Many.BaseInset = (float)length;
				}
				return _Many;
			}
		}

		public static CustomLineCap One
		{
			get
			{
				if (_One == null)
				{
					int length = 5;
					_One = new CustomLineCap(null, GetOnePath(length));
					//_One.BaseInset = (float)length;
				}
				return _One;
			}
		}

		public static CustomLineCap Zero
		{
			get
			{
				if (_Zero == null)
				{
					int radius = 4;
					int gap = 3;
					_Zero = new CustomLineCap(null, GetZeroPath(radius));
					_Zero.BaseInset = (float)radius * 2 + gap;
				}
				return _Zero;
			}
		}

		public static CustomLineCap ZeroOrOne
		{
			get
			{
				if (_ZeroOrOne == null)
				{
					int radius = 2;
					int gap = 2;
					_ZeroOrOne = new CustomLineCap(null, GetZeroOrOnePath(radius));
					_ZeroOrOne.BaseInset = (float)radius * 2 + gap + 1;
				}
				return _ZeroOrOne;
			}
		}

		public static CustomLineCap SolidArrow
		{
			get
			{
				if (_SolidArrow == null)
				{
					int size = 2;
					_SolidArrow = new CustomLineCap(GetArrowPath(size), null);
					_SolidArrow.BaseInset = (float)size * 3;
				}
				return _SolidArrow;
			}
		}

		public static CustomLineCap HollowArrow
		{
			get
			{
				if (_SolidArrow == null)
				{
					int size = 2;
					_SolidArrow = new CustomLineCap(null, GetArrowPath(size));
					_SolidArrow.BaseInset = (float)size * 3;
				}
				return _SolidArrow;
			}
		}

		#region Helper Methods

		private static GraphicsPath GetNonePath()
		{
			return new GraphicsPath();
		}

		private static GraphicsPath GetArrowPath(int size)
		{
			GraphicsPath hPath = new GraphicsPath();
			Point top = new Point(0, 0);
			Point leftCorner = new Point(-1 * size, -3 * size);
			Point rightCorner = new Point(1 * size, -3 * size);

			hPath.AddLine(top, leftCorner);
			hPath.AddLine(leftCorner, rightCorner);
			hPath.AddLine(rightCorner, top);

			return hPath;
		}

		private static GraphicsPath GetOnePath(int length)
		{
			GraphicsPath hPath = new GraphicsPath();
			//Point top = new Point(0, -1 * length);
			//Point bottom = new Point(0, 0);
			Point left = new Point(-1 * length, -1 * length);
			Point right = new Point(length, -1 * length);

			//hPath.AddLine(top, bottom);
			hPath.AddLine(left, right);

			return hPath;
		}

		private static GraphicsPath GetManyPath(int length)
		{
			GraphicsPath hPath = new GraphicsPath();
			//Point top = new Point(0, -1 * length);
			//Point leftBottom = new Point(-1 * length, 0);
			//Point centreBottom = new Point(0, 0);
			//Point rightBottom = new Point(length, 0);

			Point top = new Point(0, -1 * (int)(length * 1.5));
			Point leftBottom = new Point(-1 * length, 0);
			Point centreBottom = new Point(0, 0);
			Point rightBottom = new Point(length, 0);

			hPath.AddLine(top, leftBottom);
			hPath.AddLine(top, centreBottom);
			hPath.AddLine(top, rightBottom);

			return hPath;
		}

		private static GraphicsPath GetDiamondPath(int radius)
		{
			GraphicsPath hPath = new GraphicsPath();
			Point top = new Point(0, -1 * radius * 2);
			Point left = new Point(-1 * radius, -1 * radius);
			Point bottom = new Point(0, 0);
			Point right = new Point(radius, -1 * radius);

			hPath.AddLine(top, left);
			hPath.AddLine(left, bottom);
			hPath.AddLine(bottom, right);
			hPath.AddLine(right, top);

			return hPath;
		}

		private static GraphicsPath GetCirclePath(int radius)
		{
			GraphicsPath hPath = new GraphicsPath();
			hPath.AddEllipse(-1 * radius, -1 * radius * 2, radius * 2, radius * 2);
			return hPath;
		}

		private static GraphicsPath GetZeroPath(int radius)
		{
			int gap = 2;
			GraphicsPath hPath = new GraphicsPath();
			hPath.AddEllipse(-1 * radius, -1 * (radius * 2 + gap), radius * 2, radius * 2);

			Point top = new Point(0, -1 * gap);
			Point bottom = new Point(0, 0);
			hPath.AddLine(top, bottom);

			return hPath;
		}

		private static GraphicsPath GetZeroOrOnePath(int radius)
		{
			int gap = 3;
			GraphicsPath hPath = new GraphicsPath();
			hPath.AddEllipse(-1 * radius, -1 * (radius * 2 + gap), radius * 2, radius * 2);

			Point top = new Point(0, -1 * gap);
			Point bottom = new Point(0, 0);
			Point middle = new Point(0, -1 * (gap - 1));
			hPath.AddLine(top, bottom);
			hPath.AddLine(bottom, middle);

			Point left = new Point(-1 * radius - 1, -1 * (gap - 1));
			Point right = new Point(radius + 1, -1 * (gap - 1));

			//hPath.AddLine(left, right);
			hPath.AddLine(middle, left);
			hPath.AddLine(left, middle);
			hPath.AddLine(middle, right);

			return hPath;
		}

		#endregion
	}
}
