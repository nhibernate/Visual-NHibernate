using System.Windows;
using System.Windows.Media;

namespace SchemaDiagrammer.View.Shapes.ConnectorEndPoints
{
	public interface IConnectorEndPointDrawingStrategy
	{
		void Draw(StreamGeometryContext context, Point startPoint, Point endPoint);
		void SetUpConnectorEndPoint(ConnectorEndPoint endPoint);
	}

	public abstract class ConnectorEndPointDrawingStrategyBase : IConnectorEndPointDrawingStrategy
	{
		public abstract void Draw(StreamGeometryContext context, Point startPoint, Point endPoint);

		public virtual void SetUpConnectorEndPoint(ConnectorEndPoint endPoint)
		{
		}
	}

	public class OneToManyConnectorEndPoint : ManyConnectorEndPoint
	{
		public override void Draw(StreamGeometryContext context, Point startPoint, Point endPoint)
		{
			base.Draw(context, startPoint, endPoint);

			Vector line = endPoint - startPoint;
			Vector perpendicularLine = new Vector(line.Y, -line.X);
			perpendicularLine.Normalize();

			double halfLength = line.Length / 2;
			Point leftPoint = endPoint - (perpendicularLine * halfLength);
			Point rightPoint = endPoint + (perpendicularLine * halfLength);

			context.BeginFigure(leftPoint, true, false);
			context.LineTo(rightPoint, true, false);
		}
	}
}