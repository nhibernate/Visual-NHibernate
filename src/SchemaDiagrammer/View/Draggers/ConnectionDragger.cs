using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes;

namespace SchemaDiagrammer.View.Draggers
{
	public class ConnectionDragger : DraggerBase
	{
		protected readonly Connection _OriginalConnection;
		protected readonly bool _IsStartOfConnection;
		private readonly ConnectionPoint _OriginalConnectionPoint;
		private readonly Brush _OriginalConnectionBrush;

		public ConnectionDragger(DiagramSurface surface, MainWindow window, Point startPoint, Connection originalConnection, bool isStartOfConnection) : base(surface, window, startPoint)
		{
			_OriginalConnection = originalConnection;
			_IsStartOfConnection = isStartOfConnection;
			_OriginalConnectionPoint = isStartOfConnection
			                           	? originalConnection.SourceConnectionPoint
			                           	: originalConnection.TargetConnectionPoint;
			_OriginalConnectionBrush = _OriginalConnection.Stroke;

			SetupMouse();
		}

		protected override void MoveOriginalShapeToOverlay()
		{
			// If we are close enough to a connection, attach to it.
			ConnectionPoint connectionPoint = GetConnectionPoint();

			if(connectionPoint == null)
			{
				// Remove the connection
				_Surface.RemoveConnection(_OriginalConnection);
				return;
			}

			if(_IsStartOfConnection)
			{
				_OriginalConnection.Source = connectionPoint.ParentShape;
				Connection.SetConnectionStartPointBinding(_OriginalConnection, connectionPoint);
			}
			else
			{
				_OriginalConnection.Target = connectionPoint.ParentShape;
				Connection.SetConnectionEndPointBinding(_OriginalConnection, connectionPoint);
			}
		}

		private ConnectionPoint GetConnectionPoint()
		{
			var point = Mouse.GetPosition(_Surface);
			return _Surface.GetConnectionPointAt(point);
		}

		protected override void CreateOverlay()
		{
			// Add a line to indicate where we'll end up.
			// We'll just use an alpha-blended visual brush.
			var brush = _OriginalConnection.Stroke != null ? _OriginalConnection.Stroke.Clone() : Brushes.Black.Clone();
			brush.Opacity = 0.5;

			_OriginalConnection.Stroke = brush;

			_OriginalConnection.SourceEndPoint.RemoveFromSurface(_Surface);
			_OriginalConnection.TargetEndPoint.RemoveFromSurface(_Surface);

			if (_IsStartOfConnection)
			{
				_OriginalConnection.SourceConnectionPoint = null;
			}
			else
			{
				_OriginalConnection.TargetConnectionPoint = null;
			}

			
		}

		protected override void MoveOverlay(Point mousePosition, double left, double top)
		{
			Vector vec = new Vector(5, 5);

			if(_IsStartOfConnection)
				_OriginalConnection.StartPoint = mousePosition + vec;
			else
				_OriginalConnection.EndPoint = mousePosition + vec;
		}

		protected override Point CalculateOriginalPosition()
		{
			return _OriginalConnectionPoint != null ? _OriginalConnectionPoint.Location : Mouse.GetPosition(_Surface);
		}

		protected override void RemoveOverlay()
		{
			_OriginalConnection.Stroke = _OriginalConnectionBrush;
		}

		protected override void OperationCancelled()
		{
			if(_IsStartOfConnection)
				Connection.SetConnectionStartPointBinding(_OriginalConnection, _OriginalConnectionPoint);
			else
				Connection.SetConnectionEndPointBinding(_OriginalConnection, _OriginalConnectionPoint);
		}

		protected override bool ShouldHighlightObject(IDiagramEntity obj)
		{
			return obj is ConnectionPoint && CanConnect(obj as ConnectionPoint);
		}

		protected override void OnDragFinished()
		{
			// Do Nothing
		}

		protected override void OperationSuccessful()
		{
			_OriginalConnection.SourceEndPoint.AddToSurface(_Surface);
			_OriginalConnection.TargetEndPoint.AddToSurface(_Surface);
		}

		protected override bool CancelOperation()
		{
			var connectionPoint = GetConnectionPoint();
			if (connectionPoint == null) return false;
			if(_IsStartOfConnection && connectionPoint == _OriginalConnection.TargetConnectionPoint)
				return true;
			if (!_IsStartOfConnection && connectionPoint == _OriginalConnection.SourceConnectionPoint)
				return true;

			bool canConnect = CanConnect(connectionPoint);
			return !canConnect;
		}

		protected bool CanConnect(ConnectionPoint connectionPoint)
		{
			DiagramShape sourceShape = _IsStartOfConnection ? _OriginalConnection.Target : _OriginalConnection.Source;

			return _Surface.Controller.CanConnect(connectionPoint.ParentShape, sourceShape);
		}
	}

	public class NewConnectionDragger : ConnectionDragger
	{
		public NewConnectionDragger(DiagramSurface surface, MainWindow window, Point startPoint, ConnectionPoint startConnectionPoint) : base(surface, window, startPoint, new Connection(startConnectionPoint), false)
		{
			_OriginalConnection.AddToSurface(surface);
			_OriginalConnection.Source = startConnectionPoint.ParentShape;
			Connection.SetConnectionStartPointBinding(_OriginalConnection, startConnectionPoint);

			_OriginalConnection.EndPoint = Mouse.GetPosition(_Surface);
		}

		protected override void OperationCancelled()
		{
			// Stop the ConnectionDragger from trying to reset the 
		}

		protected override void OperationSuccessful()
		{
			base.OperationSuccessful();

			// This still gets called if the connection point was dropped in the middle of nowhere. 
			// Check to make sure that the target connection point was set.
			if(_OriginalConnection.TargetConnectionPoint != null)
			{
				var newConnection = _Surface.AddConnection(_OriginalConnection.Source, _OriginalConnection.TargetConnectionPoint.ParentShape);
				Connection.SetConnectionStartPointBinding(newConnection, _OriginalConnection.SourceConnectionPoint);
				Connection.SetConnectionEndPointBinding(newConnection, _OriginalConnection.TargetConnectionPoint);
			}

			_OriginalConnection.RemoveFromSurface(_Surface);
		}
	}
}