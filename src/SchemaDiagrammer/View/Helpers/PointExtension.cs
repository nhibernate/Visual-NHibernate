using System;
using System.Windows;

namespace SchemaDiagrammer.View.Helpers
{
    public static class PointExtension
    {
        public static double AbsoluteDistanceTo(this Point point1, Point point2)
        {
            return Math.Abs((point1 - point2).Length);
        }

		public static Vector To(this Point startPoint, Point endPoint)
		{
			return endPoint - startPoint;
		}
    }
}