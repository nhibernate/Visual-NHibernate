using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SchemaDiagrammer.View.Shapes
{
	internal class MiddleOfLineConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			Point startPoint = (Point)values[0];
			Point endPoint = (Point)values[1];

			var vec = startPoint - endPoint;
			vec /= 2;
			Point convert = startPoint - vec;
			return convert;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}
}