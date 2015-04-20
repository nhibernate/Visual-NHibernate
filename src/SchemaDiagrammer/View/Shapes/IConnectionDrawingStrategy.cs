using System.Windows;
using System.Windows.Media;

namespace SchemaDiagrammer.View.Shapes
{
	public interface IConnectionDrawingStrategy
	{
		void Draw(StreamGeometryContext context, Connection connection);
		bool OverrideConnectorEndPointSetup { get; }
		void CustomSetupConnectorEndPoint(Connection connection);
	}

	public abstract class ConnectionDrawingStrategyBase : IConnectionDrawingStrategy
	{
		public abstract void Draw(StreamGeometryContext context, Connection connection);

		public virtual bool OverrideConnectorEndPointSetup
		{
			get
			{
				return false;
			}
		}

		public virtual void CustomSetupConnectorEndPoint(Connection connection)
		{
			throw new System.InvalidOperationException("This class does not implement a custom Connector Endpoint Setup routine");
		}
	}

	public class DefaultConnectionDrawingStrategy : ConnectionDrawingStrategyBase
	{
		public override void Draw(StreamGeometryContext context, Connection connection)
		{
			if (connection.SourceConnectionPoint == null || connection.TargetConnectionPoint == null)
			{
				context.BeginFigure(connection.StartPoint, true, false);
				context.LineTo(connection.EndPoint, true, true);
			}
			else if(connection.Source == connection.Target)
			{
				Point startPoint = connection.SourceEndPoint.EndPoint;
				Point midPoint = connection.SourceConnectionPoint.LineAwayFromThisTo(startPoint, 50);

				context.BeginFigure(startPoint, true, true);
				context.ArcTo(midPoint, new Size(50, 50), 180, false, SweepDirection.Clockwise, true, true);
				context.ArcTo(startPoint, new Size(50, 50), 180, false, SweepDirection.Clockwise, true, true);
			}
			else
			{
				Point startPoint = connection.SourceEndPoint.EndPoint;
				Point endPoint = connection.TargetEndPoint.EndPoint;

				context.BeginFigure(startPoint, true, false);
				context.LineTo(endPoint, true, true);
			}
		}
	}
}
