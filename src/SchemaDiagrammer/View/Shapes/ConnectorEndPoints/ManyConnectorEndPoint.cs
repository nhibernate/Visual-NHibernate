using System.Windows;
using System.Windows.Media;

namespace SchemaDiagrammer.View.Shapes.ConnectorEndPoints
{
	public class ManyConnectorEndPoint : ConnectorEndPointDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
			Vector line = endPoint - startPoint;
			Vector perpendicularLine = new Vector(line.Y, -line.X);
			perpendicularLine.Normalize();

			double halfLength = line.Length/2;
			Point leftPoint = startPoint - (perpendicularLine*halfLength);
			Point rightPoint = startPoint + (perpendicularLine * halfLength);

			var norLine = new Vector(line.X, line.Y);
			norLine.Normalize();
			Point shortEndPoint = endPoint - (norLine * 4);

			context.BeginFigure(startPoint, true, false);
			context.LineTo(shortEndPoint, true, false);

			context.LineTo(leftPoint, false, false);
			context.LineTo(shortEndPoint, true, false);

			context.LineTo(rightPoint, false, false);
			context.LineTo(shortEndPoint, true, false);
			
			context.LineTo(endPoint, true, false);
		}
	}
}