using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SchemaDiagrammer.Controller;
using SchemaDiagrammer.View.Converters;
using SchemaDiagrammer.View.Shapes;

namespace SchemaDiagrammer.View.Helpers
{
	public static class ConnectionPointUtility
	{
		public static void TidyAllConnections(IController controller)
		{
			foreach(var shape in controller.Schema.Shapes)
			{
				TidyAllConnections(shape);
			}
		}

		public static void TidyAllConnections_AndConnectedShapes(DiagramShape shape)
		{
			TidyAllConnections(shape);
			foreach(var s in shape.ConnectedShapes)
			{
				TidyAllConnections(s);
			}
		}

		public static void TidyAllConnections(DiagramShape shape)
		{
			Dictionary<SideOfShape, List<Connection>> connectionsBySide = GetConnectionsBySide(shape);
			HashSet<ConnectionPoint> newlyCreatedConnectionPoints = new HashSet<ConnectionPoint>();

			// Iterate through the groups one at a time
			foreach(var connectionsPair in connectionsBySide)
			{
				var connections = connectionsPair.Value;
				var side = connectionsPair.Key;
				var centreOfSide = GetCenterOfSide(shape, side);
				List<Pair<double, Connection>> angles = GetConnectionAngles(shape, connections, centreOfSide);

				// Order the connections by their angle
				Func<Pair<double, Connection>, double> selector;
				if(side == SideOfShape.Top || side == SideOfShape.Right) 
					selector = pair => pair.Value1;
				else 
					selector = pair => -pair.Value1;
				var sortedAngles = angles.OrderBy(selector).ToList();

				// Create new connection points for the connections
				var newConnectionPoints = CreateNewConnectionPointsOn(shape, side, connections.Count);
					
				// HashSet doesn't have an AddRange method, so I'm using ForEach to make this shorter.
				newConnectionPoints.ForEach(p => newlyCreatedConnectionPoints.Add(p));
					
				// Attach the Connections to the new Connection Points.
				ConnectToConnectionPoints(shape, sortedAngles.Select(pair => pair.Value2).ToList(), newConnectionPoints);
			}

			// Clear all unused connection points from the shape
			shape.RemoveUnusedConnectionPoints();
		}

		private static void ConnectToConnectionPoints(DiagramShape shape, IList<Connection> connections, IList<ConnectionPoint> points)
		{
			int count = connections.Count();

			for(int i = 0; i < count; i++)
			{
				var connection = connections[i];
				var connectionPoint = points[i];

				if(connection.Source == connection.Target)
				{
					Connection.SetConnectionStartPointBinding(connection, connectionPoint);
					Connection.SetConnectionEndPointBinding(connection, connectionPoint);
				}
				else if(connection.Source == shape)
				{
					Connection.SetConnectionStartPointBinding(connection, connectionPoint);
				}
				else
				{
					Connection.SetConnectionEndPointBinding(connection, connectionPoint);
				}
			}
		}

		private static List<ConnectionPoint> CreateNewConnectionPointsOn(DiagramShape shape, SideOfShape side, int numConnections)
		{
			var list = new List<ConnectionPoint>();

			double spacing = 1.0 / (numConnections + 1);

			for (int i = 1; i <= numConnections; i++ )
			{
				var cp = shape.AddAnotherConnectionPointAt(side, spacing*i);
				list.Add(cp);
			}

			return list;
		}

		private static Dictionary<SideOfShape, List<Connection>> GetConnectionsBySide(DiagramShape shape)
		{
			Dictionary<SideOfShape, List<Connection>> connectionsBySide = new Dictionary<SideOfShape, List<Connection>>();
			foreach(SideOfShape side in Enum.GetValues(typeof(SideOfShape)))
				connectionsBySide[side] = new List<Connection>();

			// Split connections into groups based on the side they are closest to.
			foreach(var connection in shape.Connections)
			{
				DiagramShape otherShape = connection.Source == shape ? connection.Target : connection.Source;

				SideOfShape closestSide = GetClosestSideTo(shape, otherShape);
				connectionsBySide[closestSide].Add(connection);
			}
			return connectionsBySide;
		}

		private static List<Pair<double, Connection>> GetConnectionAngles(DiagramShape shape, IEnumerable<Connection> connections, Point centreOfSide)
		{
			var angles = new List<Pair<double, Connection>>();

			// Calculate the angle each connection hits the current shape.
			foreach(var connection in connections)
			{
				var otherShape = connection.Source == shape ? connection.Target : connection.Source;

				double angle = CalculateAngle(centreOfSide, otherShape.CenterLocation);
				angles.Add(new Pair<double, Connection>(angle, connection));
			}
			return angles;
		}

		private static readonly ConnectorBindingConverter converter = new ConnectorBindingConverter();

		private static Point GetCenterOfSide(DiagramShape shape, SideOfShape side)
		{
			return converter.Convert(shape, side, 0.5);
		}

		private static SideOfShape GetClosestSideTo(DiagramShape shape, DiagramShape otherShape)
		{
			var sides = new SortedList<double, SideOfShape>();

			foreach (SideOfShape side in Enum.GetValues(typeof(SideOfShape)))
			{
				var distance = converter.Convert(shape, side, 0.5).AbsoluteDistanceTo(otherShape.CenterLocation);
				sides[distance] = side;
			}

			return sides.First().Value;
		}

		public class Pair<Q, T>
		{
			public Q Value1 { get; set; }
			public T Value2 { get; set; }

			public Pair(Q value1, T value2)
			{
				Value1 = value1;
				Value2 = value2;
			}
		}

		private static double CalculateAngle(Point point, Point location)
		{
			double radians = Math.Atan2((point.Y - location.Y), (point.X - location.X));
			if(radians < 0) radians += 2 * Math.PI;
			var angle = radians*(180/Math.PI);
			return angle;
		}
	}
}
