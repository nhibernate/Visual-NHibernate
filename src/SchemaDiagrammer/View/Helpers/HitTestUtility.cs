using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using SchemaDiagrammer.View.Shapes;
using SchemaDiagrammer.View.Shapes.ConnectorEndPoints;

namespace SchemaDiagrammer.View.Helpers
{
    public static class HitTestUtility
    {
    	private static readonly Func<List<DiagramShape>, Point, HitTestResultCallback> SimpleShapeLambda;
    	private static readonly Func<List<Connection>, Point, HitTestResultCallback> ConnectionLambda;
    	private static readonly Func<List<ConnectionPoint>, Point, HitTestResultCallback> ConnectionPointLambda;
		private static readonly Func<List<ConnectorEndPoint>, Point, HitTestResultCallback> ConnectorEndPointLambda;

		static HitTestUtility()
		{
    		SimpleShapeLambda = ((shapesHit, p) => (hitTestResult =>
   			{
   				var visualHit = hitTestResult.VisualHit as FrameworkElement;
   				if(visualHit == null)
   					return HitTestResultBehavior.Continue;

   				if (visualHit.TemplatedParent is DiagramShape)
   				{
   					shapesHit.Add(visualHit.TemplatedParent as DiagramShape);
   				}
   				return HitTestResultBehavior.Continue;
   			}));

			ConnectionLambda = (shapesHit, startPoint) => (hitTestResult =>
			{
				if (hitTestResult.VisualHit is Connection)
				{
					shapesHit.Add(hitTestResult.VisualHit as Connection);
				}
				return HitTestResultBehavior.Continue;
			});

			ConnectionPointLambda = (shapesHit, p) => (hitTestResult =>
            {
                if (hitTestResult.VisualHit is ConnectionPoint)
                {
                    shapesHit.Add(hitTestResult.VisualHit as ConnectionPoint);
                }
                return HitTestResultBehavior.Continue;
            });

			ConnectorEndPointLambda = (shapesHit, p) => (hitTestResult =>
			{
				if (hitTestResult.VisualHit is ConnectorEndPoint)
				{
					shapesHit.Add(hitTestResult.VisualHit as ConnectorEndPoint);
				}
				return HitTestResultBehavior.Continue;
			});
		}

		public static List<IDiagramEntity> GetImportantObjectsAt(this DiagramSurface surface, Point point)
		{
			var objects = new List<IDiagramEntity>();

			surface.GetShapesAt(point).ForEach(objects.Add);
			surface.GetConnectionsAt(point).ForEach(objects.Add);
			surface.GetConnectionPointsAt(point).ForEach(objects.Add);
			surface.GetConnectorEndPointsAt(point).ForEach(objects.Add);

			return objects;
		}

		public static ConnectorEndPoint GetConnectorEndPointAt(this DiagramSurface surface, Point point)
		{
			var shapesHit = GetConnectorEndPointsAt(surface, point);

			shapesHit.Sort((s1, s2) =>
			{
				var distance1 = s1.StartPoint.AbsoluteDistanceTo(point);
				var distance2 = s2.StartPoint.AbsoluteDistanceTo(point);

				return distance1.CompareTo(distance2);
			});

			return shapesHit.Count > 0 ? shapesHit[0] : null;
		}

		public static List<ConnectorEndPoint> GetConnectorEndPointsAt(this DiagramSurface surface, Point point)
		{
			return GetShapesHit(surface, point, ConnectorEndPointLambda);
		}

    	public static DiagramShape GetShapeAt(this DiagramSurface surface, Point p)
        {
			var shapesHit = GetShapesAt(surface, p);

			shapesHit.Sort((s1, s2) =>
			{
				var distance1 = s1.Location.AbsoluteDistanceTo(p);
				var distance2 = s2.Location.AbsoluteDistanceTo(p);

				return distance1.CompareTo(distance2);
			});

			return shapesHit.Count > 0 ? shapesHit[0] : null;
        }

		public static List<DiagramShape> GetShapesAt(this DiagramSurface surface, Point p)
		{
			return GetShapesHit(surface, p, SimpleShapeLambda);
		}

        public static Connection GetConnectionAt(this DiagramSurface surface, Point p)
        {
			var shapesHit = GetConnectionsAt(surface, p);

			shapesHit.Sort((s1, s2) =>
			{
				var distance1 = Math.Min(s1.StartPoint.AbsoluteDistanceTo(p), s1.EndPoint.AbsoluteDistanceTo(p));
				var distance2 = Math.Min(s2.StartPoint.AbsoluteDistanceTo(p), s2.EndPoint.AbsoluteDistanceTo(p));

				return distance1.CompareTo(distance2);
			});

			return shapesHit.Count > 0 ? shapesHit[0] : null;
        }

		public static List<Connection> GetConnectionsAt(this DiagramSurface surface, Point p)
		{
			return GetShapesHit(surface, p, ConnectionLambda);
		}

        public static ConnectionPoint GetConnectionPointAt(this DiagramSurface surface, Point p)
        {
			var shapesHit = GetConnectionPointsAt(surface, p);
			
			shapesHit.Sort((s1, s2) =>
               	{
               		var distance1 = s1.Location.AbsoluteDistanceTo(p);
               		var distance2 = s2.Location.AbsoluteDistanceTo(p);

               		return distance1.CompareTo(distance2);
               	});

        	return shapesHit.Count > 0 ? shapesHit[0] : null;
        }

		public static List<ConnectionPoint> GetConnectionPointsAt(this DiagramSurface surface, Point p)
		{
			return GetShapesHit(surface, p, ConnectionPointLambda);
		}

    	private static List<T> GetShapesHit<T>(Visual surface, Point p, Func<List<T>, Point, HitTestResultCallback> target)
    	{
    		var shapesHit = new List<T>();

    		var callback = new HitTestResultCallback(target(shapesHit, p));
    		// Specify that the hit test should search for every shape within 2 pixels of the mouse click.
    		var rect = new Rect(new Point(p.X - 2, p.Y - 2), new Size(5, 5));
    		var parameters = new GeometryHitTestParameters(new RectangleGeometry(rect));

    		// Perform the hit test
    		VisualTreeHelper.HitTest(surface, null, callback, parameters);
    		return shapesHit;
    	}
    }
}