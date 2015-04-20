using System.Windows;
using System.Windows.Media;

namespace SchemaDiagrammer.View.Helpers
{
	public static class DrawingHelper
	{
		public static void DrawParallelLines(StreamGeometryContext context, Point startPoint, Point endPoint, int spacing)
		{
			Vector perpendicularLine = GetPerpendicularLine(startPoint, endPoint);

			// Draw 1->2 line
			context.BeginFigure(startPoint + (perpendicularLine * spacing), true, false);
			context.LineTo(endPoint + (perpendicularLine * spacing), true, true);

			// Draw 2->1 line
			context.BeginFigure(startPoint - (perpendicularLine * spacing), true, false);
			context.LineTo(endPoint - (perpendicularLine * spacing), true, true);
		}

		public static Vector GetPerpendicularLine(Point startPoint, Point endPoint)
		{
			Vector line = endPoint - startPoint;
			return GetPerpendicularLine(line);
		}

		public static Vector GetPerpendicularLine(Vector line)
		{
			Vector perpendicularLine = new Vector(line.Y, -line.X);
			perpendicularLine.Normalize();
			return perpendicularLine;
		}

		public static void DrawTriangle(StreamGeometryContext context, Vector mainLine, Vector mainPerpendicularLine, Point point1, int size)
		{
			DrawTriangle(context, mainLine, mainPerpendicularLine, point1, size, true);
		}

		public static void DrawTriangle(StreamGeometryContext context, Vector mainLine, Vector mainPerpendicularLine, Point point1, int size, bool isFilled)
		{
			int halfSize = size / 2;
			context.BeginFigure(point1, isFilled, true);
			var point2 = point1 + (mainPerpendicularLine * halfSize) + (mainLine * size);
			var point3 = point1 - (mainPerpendicularLine * halfSize) + (mainLine * size);
			context.LineTo(point2, true, true);
			context.LineTo(point3, true, true);
		}
	}
}
