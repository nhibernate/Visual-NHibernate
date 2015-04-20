using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using SchemaDiagrammer.View.Shapes;
using SchemaDiagrammer.View.Shapes.ConnectorEndPoints;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	public class InheritanceDrawingStrategy : IConnectionDrawingStrategy
	{
		public void Draw(StreamGeometryContext context, Connection connection)
		{
			Point startPoint = connection.SourceEndPoint.EndPoint;
			Point endPoint = connection.TargetEndPoint.EndPoint;

			context.BeginFigure(startPoint, true, false);
			context.LineTo(endPoint, true, true);
		}

		public bool OverrideConnectorEndPointSetup
		{
			get
			{
				return true;
			}
		}

		public void CustomSetupConnectorEndPoint(Connection connection)
		{
			connection.SourceCardinalityConverter = new InheritanceCardinalityConverter();
		}	
	}

	public class BlankCardinalityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return new BlankEndPoint();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class BlankEndPoint : ConnectorEndPointDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
		}
	}
}