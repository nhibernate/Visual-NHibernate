using System;
using System.Globalization;
using System.Windows.Data;
using ArchAngel.Interfaces;
using SchemaDiagrammer.View.Shapes.ConnectorEndPoints;

namespace SchemaDiagrammer.View.Converters
{
	public class CardinalityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var cardinality = value as Cardinality;

			if (cardinality == null)
				return new OneConnectorEndPoint();
			
			if(cardinality.Start == cardinality.End && (cardinality.Start == 1 ||cardinality.Start == 0))
				return new OneConnectorEndPoint();
			
			if (cardinality.Start == 1 && cardinality.End == int.MaxValue)
				return new OneToManyConnectorEndPoint();

			return new ManyConnectorEndPoint();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
