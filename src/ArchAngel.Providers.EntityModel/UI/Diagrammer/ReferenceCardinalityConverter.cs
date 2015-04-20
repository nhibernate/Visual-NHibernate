using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ArchAngel.Interfaces;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes.ConnectorEndPoints;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	internal class ReferenceCardinalityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var cardinality = value as Cardinality;

			if (cardinality == null)
				return new ReferenceOneConnectorEndPoint();

			if (cardinality.Start == cardinality.End && (cardinality.Start == 1 || cardinality.Start == 0))
				return new ReferenceOneConnectorEndPoint();

			if (cardinality.Start == 1 && cardinality.End == int.MaxValue)
				return new ReferenceManyConnectorEndPoint();

			return new ReferenceManyConnectorEndPoint();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new InvalidOperationException("Cannot convert a drawing strategy back to a cardinality.");
		}
	}

	internal class ReferenceOneConnectorEndPoint : ConnectorEndPointDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
			var spacing = ReferenceDrawingStrategy.Spacing;
			DrawingHelper.DrawParallelLines(context, startPoint, endPoint, spacing);

			Vector mainLine = startPoint.To(endPoint);
			mainLine.Normalize();

			Vector mainPerpendicularLine = DrawingHelper.GetPerpendicularLine(startPoint, endPoint);

			DrawingHelper.DrawTriangle(context, mainLine, mainPerpendicularLine, startPoint + mainPerpendicularLine*spacing, 4);
		}
	}

	internal class ReferenceManyConnectorEndPoint : ConnectorEndPointDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
			var spacing = ReferenceDrawingStrategy.Spacing;
			DrawingHelper.DrawParallelLines(context, startPoint, endPoint, spacing);

			Vector mainLine = startPoint.To(endPoint);
			mainLine.Normalize();

			Vector mainPerpendicularLine = DrawingHelper.GetPerpendicularLine(startPoint, endPoint);

			DrawingHelper.DrawTriangle(context, mainLine, mainPerpendicularLine, startPoint + mainPerpendicularLine * spacing, 8);
		}
	}
}