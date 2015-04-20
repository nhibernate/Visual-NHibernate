using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes.ConnectorEndPoints;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	internal class InheritanceCardinalityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return new InheritanceEndPoint();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new InvalidOperationException("Cannot convert a drawing strategy back to a cardinality.");
		}
	}

	internal class InheritanceEndPoint : ConnectorEndPointDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
			Vector mainLine = startPoint.To(endPoint);
			mainLine.Normalize();

			Vector mainPerpendicularLine = DrawingHelper.GetPerpendicularLine(startPoint, endPoint);
			DrawingHelper.DrawTriangle(context, mainLine, mainPerpendicularLine, startPoint, 10, false);
		}

		public override void SetUpConnectorEndPoint(ConnectorEndPoint endPoint)
		{
			endPoint.StrokeDashArray = null;
		}
	}
}