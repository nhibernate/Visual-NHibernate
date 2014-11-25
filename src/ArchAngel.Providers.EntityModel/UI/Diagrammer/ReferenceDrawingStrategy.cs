using System.Windows;
using System.Windows.Media;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	public class ReferenceDrawingStrategy : IConnectionDrawingStrategy
	{
		public const int Spacing = 7;

		public void Draw(StreamGeometryContext context, Connection connection)
		{
			if (connection.SourceConnectionPoint == null || connection.TargetConnectionPoint == null)
			{
				context.BeginFigure(connection.StartPoint, true, false);
				context.LineTo(connection.EndPoint, true, true);
			}
			else
			{
				Point startPoint = connection.SourceEndPoint.EndPoint;
				Point endPoint = connection.TargetEndPoint.EndPoint;

				var perpendicularSource = DrawingHelper.GetPerpendicularLine(connection.SourceEndPoint.AsLine);
				var perpendicularTarget = DrawingHelper.GetPerpendicularLine(connection.TargetEndPoint.AsLine);

				context.BeginFigure(startPoint + (perpendicularSource * Spacing), true, false);
				context.LineTo(endPoint - (perpendicularTarget * Spacing), true, true);

				context.BeginFigure(startPoint - (perpendicularSource * Spacing), true, false);
				context.LineTo(endPoint + (perpendicularTarget * Spacing), true, true);
			}
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
			connection.SourceCardinalityConverter = new ReferenceCardinalityConverter();
			connection.TargetCardinalityConverter = new ReferenceCardinalityConverter();
		}	
	}
}