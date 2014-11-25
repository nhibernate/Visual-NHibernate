using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public static class ShapeHelper
	{
		public static double GetAngleBetween2PointsInDegrees(Point point1, Point point2)
		{
			double px1 = point1.X;
			double py1 = point1.Y;
			double px2 = point2.X;
			double py2 = point2.Y;

			// Negate X and Y values
			double pxRes = px2 - px1;
			double pyRes = py2 - py1;
			double angle = 0.0;

			// Calculate the angle
			if (pxRes == 0.0)
			{
				if (pyRes == 0.0)
					angle = 0.0;
				else if (pyRes > 0.0)
					angle = System.Math.PI / 2.0;
				else
					angle = System.Math.PI * 3.0 / 2.0;
			}
			else if (pyRes == 0.0)
			{
				if (pxRes > 0.0)
					angle = 0.0;
				else
					angle = System.Math.PI;
			}
			else
			{
				if (pxRes < 0.0)
					angle = System.Math.Atan(pyRes / pxRes) + System.Math.PI;
				else if (pyRes < 0.0)
					angle = System.Math.Atan(pyRes / pxRes) + (2 * System.Math.PI);
				else
					angle = System.Math.Atan(pyRes / pxRes);
			}
			// Convert to degrees
			angle = angle * 180 / System.Math.PI;
			return angle;
		}

		public static Point[] GetPointsOnCircle(Point center, int radius, int n)
		{
			double alpha = Math.PI * 2 / n;
			Point[] points = new Point[n];
			int i = -1;
			double radians270degrees = 270 * Math.PI / 180;

			while (++i < n)
			{
				double theta = alpha * i + radians270degrees;
				Point pointOnCircle = new Point(Convert.ToInt32(Math.Cos(theta) * radius), Convert.ToInt32(Math.Sin(theta) * radius));
				points[i] = new Point(center.X + pointOnCircle.X, center.Y + pointOnCircle.Y);
			}
			return points;
		}

		public static double GetLineLength(Point startPoint, Point endPoint)
		{
			return Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
		}

		public static Point GetPointOnCircle(Point center, int radius, double angle)
		{
			double thetaRadians = angle * Math.PI / 180;
			Point pointOnCircle = new Point(Convert.ToInt32(Math.Cos(thetaRadians) * radius), Convert.ToInt32(Math.Sin(thetaRadians) * radius));
			Point result = new Point(center.X + pointOnCircle.X, center.Y + pointOnCircle.Y);
			return result;
		}

		public static int[] GetEqualPointsAlongStraightLine(int leftX, int rightX, int n)
		{
			int[] points = new int[n];
			int width = (rightX - leftX) / n;
			int runningTotal = leftX - width / 2;

			for (int i = 0; i < points.Length; i++)
			{
				points[i] = runningTotal + width;
				runningTotal += width;
			}
			return points;
		}

		public static Point GetLineMidPoint(Point startPoint, Point endPoint)
		{
			double lineLength = GetLineLength(startPoint, endPoint);
			return GetPointAlongLine(startPoint, endPoint, (int)(lineLength / 2));
		}

		public static Point GetPointAlongLine(Point startPoint, Point endPoint, int distance)
		{
			double angle = GetAngleBetween2PointsInDegrees(startPoint, endPoint);
			return GetPointOnCircle(startPoint, distance, angle);
		}

		public static GraphicsPath GetCirclePath(Point centrePoint, int radius)
		{
			GraphicsPath path = new GraphicsPath();
			Point rectanglePoint = centrePoint;
			rectanglePoint.Offset(-1 * radius, -1 * radius);
			path.AddEllipse(rectanglePoint.X, rectanglePoint.Y, radius * 2, radius * 2);
			return path;
		}

		public static Point GetIntersectionPoints(Rectangle rect, Point lineEnd1, Point lineEnd2, int offset)
		{
			Point centre = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
			double ratio = 0;
			int xDelta = lineEnd2.X - lineEnd1.X;
			int yDelta = lineEnd2.Y - lineEnd1.Y;

			if (yDelta != 0)
				ratio = (double)yDelta / xDelta;

			int newX;
			int newY;

			if (xDelta >= 0 && yDelta >= 0)
			{
				// Bottom right quadrant
				if (Math.Abs(xDelta) >= Math.Abs(yDelta))
				{
					newX = xDelta == 0 ? 0 : rect.Width / 2 + offset;
					newY = yDelta == 0 ? 0 : Convert.ToInt32(ratio * newX);
					return new Point(centre.X + newX, centre.Y + newY);
				}
				else
				{
					newY = yDelta == 0 ? 0 : rect.Height / 2 + offset;
					newX = ratio == 0 ? rect.Width / 2 + offset : Convert.ToInt32(Math.Ceiling(newY / ratio));
					return new Point(centre.X + newX, centre.Y + newY);
				}
			}
			else if (xDelta >= 0 && yDelta < 0)
			{
				//// Top right quadrant
				//if (Math.Abs(xDelta) >= Math.Abs(yDelta))
				//{
				//    newX = xDelta == 0 ? 0 : rect.Width / 2 + offset;
				//    newY = yDelta == 0 ? 0 : Convert.ToInt32(ratio * newX);
				//    return new Point(centre.X + newX, centre.Y + newY);
				//}
				//else
				//{
				newY = yDelta == 0 ? 0 : rect.Height / 2 + offset;
				//newX = xDelta == 0 ? 0 / 2 + offset : Convert.ToInt32(Math.Ceiling(newY / ratio));
				newX = xDelta == 0 ? 0 : Convert.ToInt32(Math.Ceiling(newY / ratio));
				return new Point(centre.X - newX, centre.Y - newY);
				//}
			}
			else if (xDelta < 0 && yDelta > 0)
			{
				// Bottom left
				if (Math.Abs(xDelta) >= Math.Abs(yDelta))
				{
					newX = xDelta == 0 ? 0 : rect.Width / 2 + offset;
					newY = yDelta == 0 ? 0 : Convert.ToInt32(ratio * newX);
					return new Point(centre.X - newX, centre.Y - newY);
				}
				else
				{
					newY = yDelta == 0 ? 0 : rect.Height / 2 + offset;
					newX = ratio == 0 ? rect.Width / 2 + offset : Convert.ToInt32(Math.Ceiling(newY / ratio));
					return new Point(centre.X + newX, centre.Y + newY);
				}
			}
			else
			{
				//// Bottom left quadrant
				//if (Math.Abs(xDelta) >= Math.Abs(yDelta))
				//{
				//    newX = xDelta == 0 ? 0 : rect.Width / 2 + offset;
				//    newY = yDelta == 0 ? 0 : Convert.ToInt32(ratio * newX);
				//    return new Point(centre.X - newX, centre.Y - newY);
				//    //return new Point(0, 0);
				//}
				//else
				//{
				newY = yDelta == 0 ? 0 : rect.Height / 2 + offset;
				newX = ratio == 0 ? rect.Width / 2 + offset : Convert.ToInt32(Math.Ceiling(newY / ratio));
				return new Point(centre.X - newX, centre.Y - newY);
				//}
			}
		}
	}


}
