using System.Windows;
using System.Windows.Media;

namespace SchemaDiagrammer.View.Shapes.ConnectorEndPoints
{
	public class OneConnectorEndPoint : ConnectorEndPointDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
			context.BeginFigure(startPoint, true, false);
			context.LineTo(endPoint, true, false);
		}
	}
}
