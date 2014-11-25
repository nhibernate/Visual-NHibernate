using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SchemaDiagrammer.View.Converters
{
	public class VisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool val = (bool) value;

			return val ? Visibility.Visible : Visibility.Hidden;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility vis = (Visibility) value;

			return vis == Visibility.Visible ? true : false;
		}
	}
}
