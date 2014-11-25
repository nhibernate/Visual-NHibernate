using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SchemaDiagrammer.View.Shapes;

namespace SchemaDiagrammer.View.Converters
{
	public enum SideOfShape { Top, Bottom, Left, Right }

	public static class SideOfShapeHelper
	{
		public static Vector LinePerpendicularTo(this SideOfShape side)
		{
			switch (side)
			{
				case SideOfShape.Top:
					return new Vector(0, -1);
				case SideOfShape.Bottom:
					return new Vector(0, 1);
				case SideOfShape.Left:
					return new Vector(-1, 0);
				case SideOfShape.Right:
					return new Vector(1, 0);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Returns a normalized Vector representing a line parallel to
		/// an imaginary wall.
		///		 _____
		///		|  .  |
		///		|_____|
		///	The vectors for each side are determined by the line that points
		/// clockwise in this diagram.
		/// </summary>
		/// <param name="side"></param>
		/// <returns></returns>
		public static Vector LineParallelTo(this SideOfShape side)
		{
			switch (side)
			{
				case SideOfShape.Top:
					return new Vector(1, 0);
				case SideOfShape.Bottom:
					return new Vector(-1, 0);
				case SideOfShape.Left:
					return new Vector(0, -1);
				case SideOfShape.Right:
					return new Vector(0, 1);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
	
	public class ConnectorBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double left = System.Convert.ToDouble(values[0]);
            double top = System.Convert.ToDouble(values[1]);
            double width = System.Convert.ToDouble(values[2]);
            double height = System.Convert.ToDouble(values[3]);
        	SideOfShape side = (SideOfShape) values[4];
        	double position = System.Convert.ToDouble(values[5]);
            //return bindingStrategy.Convert(left, top, actualWidth, actualHeight);

			return Convert(left, top, width, height, side, position);
        }

		public Point Convert(DiagramShape shape, SideOfShape side, double position)
		{
			return Convert(Canvas.GetLeft(shape), Canvas.GetTop(shape), shape.ActualWidth, shape.ActualHeight,
						   side, position);
		}

		public Point Convert(DiagramShape shape, ConnectionPoint connectionPoint)
		{
			return Convert(Canvas.GetLeft(shape), Canvas.GetTop(shape), shape.ActualWidth, shape.ActualHeight,
			               connectionPoint.Side, connectionPoint.Position);
		}

    	public Point Convert(double left, double top, double width, double height, SideOfShape side, double position)
    	{
    		switch (side)
    		{
    			case SideOfShape.Top:
    				var convert = new Point(left + width * position, top);
    				return convert;
    			case SideOfShape.Bottom:
    				return new Point(left + width * position, top + height);
    			case SideOfShape.Left:
    				return new Point(left, top + height * position);
    			case SideOfShape.Right:
    				return new Point(left + width, top + height * position);
    			default:
    				throw new ArgumentOutOfRangeException();
    		}
    	}

    	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}