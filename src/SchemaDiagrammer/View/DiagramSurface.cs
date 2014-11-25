using System;
using System.Windows;
using System.Windows.Controls;
using SchemaDiagrammer.Controller;
using SchemaDiagrammer.Model;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes;
using Slyce.Common.EventExtensions;
using Point=System.Windows.Point;

namespace SchemaDiagrammer.View
{
	public class DiagramSurface : Canvas
	{
        public static readonly RoutedEvent ObjectSelectedEvent = EventManager.RegisterRoutedEvent("ObjectSelected", RoutingStrategy.Bubble, typeof(ObjectSelectedHandler), typeof(DiagramSurface));
        public static void AddObjectSelectedHandler(DependencyObject d, ObjectSelectedHandler handler)
        {
            UIElement uie = d as UIElement;
            if (uie != null)
            {
                uie.AddHandler(ObjectSelectedEvent, handler);

            }
        }

        public static void RemoveObjectSelectedHandler(DependencyObject d, ObjectSelectedHandler handler)
        {
            UIElement uie = d as UIElement;
            if (uie != null)
            {
                uie.RemoveHandler(ObjectSelectedEvent, handler);
            }
        }


		private Connection _SelectedConnection;
		private DiagramShape _SelectedShape;

		/// <summary>
		/// Gets or sets the controller.
		/// </summary>
		/// <value>The controller.</value>
		public IController Controller { get; private set; }

		public DiagramSurface()
		{
			SetController(new ControllerImpl());
		}

		public DiagramShape SelectedShape
		{
			get { return _SelectedShape; }
			set
			{
                if (_SelectedShape == value) return;

				DeselectConnection(_SelectedConnection);
				_SelectedConnection = null;

				DeSelectShape(_SelectedShape);
				_SelectedShape = value;
				SelectShape(_SelectedShape);
			}
		}

		public Connection SelectedConnection
		{
			get { return _SelectedConnection; }
			set
			{
                if (_SelectedConnection == value) return;

                DeSelectShape(_SelectedShape);
				_SelectedShape = null;

				DeselectConnection(_SelectedConnection);
				_SelectedConnection = value;
				SelectConnection(value);
			}
		}

		public MainWindow Window { get; set; }

		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeAdded;
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeRemoved;

		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionAdded;
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionRemoved;

		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionSelected;
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionDeselected;

		public event EventHandler<SchemaEventArgs<DiagramShape>> OnEntitySelected;
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnEntityDeselected;

		public void SetController(IController controller)
		{
			Controller = controller;
			Controller.OnShapeAdded += Controller_OnShapeAdded;
			Controller.OnShapeRemoved += Controller_OnTableRemoved;
			Controller.OnConnectionAdded += Controller_OnConnectionAdded;
			Controller.OnConnectionRemoved += Controller_OnRelationshipRemoved;
			Controller.OnSchemaCleared += Controller_OnCleared;
		}

		private void SelectShape(DiagramShape shape)
		{
			if (shape == null) return;
			shape.IsSelected = true;

			RaiseEvent(shape, OnEntitySelected);
		}

		private void DeSelectShape(DiagramShape shape)
		{
			if (shape == null) return;
			shape.IsSelected = false;

			RaiseEvent(shape, OnEntityDeselected);
		}

		private void SelectConnection(Connection connection)
		{
			if (connection == null) return;
			connection.IsSelected = true;

			RaiseEvent(connection, OnConnectionSelected);
		}

		private void DeselectConnection(Connection connection)
		{
			if (connection == null) return;
			connection.IsSelected = false;

			RaiseEvent(connection, OnConnectionDeselected);
		}

		private void Controller_OnTableRemoved(object sender, SchemaEventArgs<DiagramShape> e)
		{
			e.Entity.RemoveFromSurface(this);
			RaiseEvent(e.Entity, OnShapeRemoved);
		}

		private void Controller_OnRelationshipRemoved(object sender, SchemaEventArgs<Connection> e)
		{
			Connection connection = e.Entity;

			connection.RemoveFromSurface(this);
			OnConnectionRemoved.RaiseEvent(this, new SchemaEventArgs<Connection>(connection));
		}

		private void Controller_OnCleared(object sender, EventArgs e)
		{
			Children.Clear();
		}

		private void Controller_OnConnectionAdded(object sender, SchemaEventArgs<Connection> e)
		{
			var connection = e.Entity;

			ConnectionPointUtility.TidyAllConnections(Controller);

			connection.AddToSurface(this);
			OnConnectionAdded.RaiseEvent(this, new SchemaEventArgs<Connection>(connection));
		}

		private void Controller_OnShapeAdded(object sender, SchemaEventArgs<DiagramShape> e)
		{
			e.Entity.AddToSurface(this);

			RaiseEvent(e.Entity, OnShapeAdded);
		}

		/// <summary>
		/// Adds a connection between the two shapes.
		/// </summary>
		/// <param name="source">The source shape.</param>
		/// <param name="target">The target shape.</param>
		/// <returns>The new connection shape.</returns>
		public Connection AddConnection(DiagramShape source, DiagramShape target)
		{
			throw new NotImplementedException();
			//return Controller.AddConnection(source, target);
		}

		/// <summary>
		/// Removed a connection.
		/// </summary>
		/// <param name="connection">The connection to remove from the Schema.</param>
		public void RemoveConnection(Connection connection)
		{
			throw new NotImplementedException();
			//Controller.RemoveConnection(connection);
		}

		/// <summary>
		/// Raises the given event
		/// </summary>
		/// <param name="obj">The object that changed</param>
		/// <param name="handler">The event to raise</param>
		protected virtual void RaiseEvent<T>(T obj, EventHandler<SchemaEventArgs<T>> handler)
		{
			var e = new SchemaEventArgs<T>(obj);
			if (handler != null)
			{
				handler(this, e);
			}
		}

		/// <summary>
		/// Zooms into the given shape on the canvas, so that the zoom level is 
		/// </summary>
		/// <param name="shape"></param>
		public void ZoomInto(DiagramShape shape)
		{
			Window.ResetPanAndZoom();
			Window.CenterOn(shape.CenterLocation);
			Window.ZoomIntoPointAtZoomLevel(CenterLocation, 1.4);
		}

		internal Point CenterLocation
		{
			get { return new Point(ActualWidth/2, ActualHeight/2); }
		}

		/// <summary>
		/// Shifts all connections to the closest connection points between their shapes.
		/// </summary>
		public void TidyConnections()
		{
			ConnectionPointUtility.TidyAllConnections(Controller);
		}
	}
}