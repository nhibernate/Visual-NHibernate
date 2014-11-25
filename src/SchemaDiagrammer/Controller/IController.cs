using System;
using System.Collections.Generic;
using SchemaDiagrammer.Model;
using SchemaDiagrammer.View;
using SchemaDiagrammer.View.Shapes;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace SchemaDiagrammer.Controller
{
	public interface IController
	{
		/// <summary>
		/// The model that this object controls.
		/// </summary>
		Schema Schema { get; }

		IEnumerable<DiagramShape> Shapes { get; }

		IEnumerable<Connection> Connections { get; }

		/// <summary>
		/// Removes all connections and shapes from the underlying Schema, and updates the View.
		/// </summary>
		void ClearSchema();

        /// <summary>
        /// Occurs when an Object has been selected that is not a connection or shape.
        /// This is a custom selection event that Custom shapes will raise for selection
        /// of things that are attached to the shapes or connections.
        /// </summary>
        event EventHandler<GenericEventArgs<Object>> OnObjectSelected;

		/// <summary>
		/// Occurs when a Shape has been selected and the diagram wants it to become the primary
		/// focus. 
		/// </summary>
		/// <remarks>
		/// This generally means you should update some filter conditions so that
		/// the current content of the diagram focuses on the given object. The object
		/// should also be considered selected.
		/// </remarks>
		event EventHandler<GenericEventArgs<DiagramShape>> OnShapeSetAsPrimary;

		/// <summary>
		/// Occurs when a Shape has been added
		/// </summary>
		event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeAdded;

		/// <summary>
		/// Occurs when a Shape has been removed
		/// </summary>
		event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeRemoved;

		/// <summary>
		/// Occurs when a Connection has been added
		/// </summary>
		event EventHandler<SchemaEventArgs<Connection>> OnConnectionAdded;

		/// <summary>
		/// Occurs when a Connection has been removed
		/// </summary>
		event EventHandler<SchemaEventArgs<Connection>> OnConnectionRemoved;

		/// <summary>
		/// Occurs when a Shape has been selected
		/// </summary>
		event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeSelected;

		/// <summary>
		/// Occurs when a Connection has been deselected
		/// </summary>
		event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeDeselected;

		/// <summary>
		/// Occurs when a Connection has been selected
		/// </summary>
		event EventHandler<SchemaEventArgs<Connection>> OnConnectionSelected;

		/// <summary>
		/// Occurs when a Connection has been deselected
		/// </summary>
		event EventHandler<SchemaEventArgs<Connection>> OnConnectionDeselected;

		/// <summary>
		/// Occurs when the underlying schema is cleared.
		/// </summary>
		event EventHandler OnSchemaCleared;

		void ShapeSelected(DiagramShape entity);
		void ShapeDeselected(DiagramShape entity);
		void ConnectionSelected(Connection entity);
		void ConnectionDeselected(Connection entity);
		void SetAllToVisible(Visibility visibility);
		void SetVisibility(DiagramShape shape, Visibility visibility);
		void SetVisibility(Connection connection, Visibility visibility);
		bool CanConnect(DiagramShape shape, DiagramShape sourceShape);

		void AddShape(DiagramShape shape);
		void AddConnection(Connection connection);

		void RemoveShape(DiagramShape shape);
		void RemoveConnection(Connection connection);

		IEnumerable<DiagramShape> GetVisibleShapes();
		IEnumerable<Connection> GetVisibleConnections();

		void ShowVisibleObjects();
		void HideAllObjects();

		void LayoutFinished();
	    void ObjectSelected(object o);
		void ShapeSetAsPrimary(DiagramShape shape);
	}

	public enum Visibility { Visible, Hidden }

	public class ControllerImpl : IController
	{
		public ControllerImpl()
		{
			Schema = new Schema();
			Schema.OnConnectionAdded += (s, e) => OnConnectionAdded.RaiseEvent(this, e);
			Schema.OnConnectionRemoved += (s, e) => OnConnectionRemoved.RaiseEvent(this, e);
			Schema.OnShapeAdded += (s, e) => OnShapeAdded.RaiseEvent(this, e);
			Schema.OnShapeRemoved += (s, e) => OnShapeRemoved.RaiseEvent(this, e);
			Schema.OnSchemaCleared += (s, e) => OnSchemaCleared.RaiseEvent(this);
		}

		private readonly HashSet<DiagramShape> visibleShapes = new HashSet<DiagramShape>();
		private readonly HashSet<Connection> visibleConnections = new HashSet<Connection>();

		public Schema Schema
		{
			get; private set;
		}

		public IEnumerable<DiagramShape> Shapes
		{
			get
			{
				return Schema.Shapes;
			}
		}

		public IEnumerable<Connection> Connections
		{
			get
			{
				return Schema.Connections;
			}
		}

		public IEnumerable<DiagramShape> GetVisibleShapes()
		{
			return visibleShapes;
		}

		public IEnumerable<Connection> GetVisibleConnections()
		{
			return visibleConnections;
		}

		public void LayoutFinished()
		{
			ShowVisibleObjects();
		}

	    public void ObjectSelected(object o)
	    {
	        OnObjectSelected.RaiseEvent(this, new GenericEventArgs<object>(o));
	    }

		public void ShapeSetAsPrimary(DiagramShape shape)
		{
			OnShapeSetAsPrimary.RaiseEvent(this, new GenericEventArgs<DiagramShape>(shape));
		}

		public void ShowVisibleObjects()
		{
			HideAllObjects();

			foreach(var shape in visibleShapes)
				shape.Visible = true;
			foreach (var conn in visibleConnections)
				conn.Visible = true;
		}

		/// <summary>
		/// Temporarily hides all of the objects, but leaves them in the visible objects set so they can be
		/// shown again with a call to ShowVisibbleObjects
		/// </summary>
		public void HideAllObjects()
		{
			foreach (var shape in Shapes)
				shape.Visible = false;
			foreach (var conn in Connections)
				conn.Visible = false;
		}

		public void ClearSchema()
		{
			Schema.Clear();
		}

		public void ShapeSelected(DiagramShape entity)
		{
			OnShapeSelected.RaiseEvent(this, new SchemaEventArgs<DiagramShape>(entity));
		}

		public void ShapeDeselected(DiagramShape entity)
		{
			OnShapeDeselected.RaiseEvent(this, new SchemaEventArgs<DiagramShape>(entity));
		}

		public void ConnectionSelected(Connection entity)
		{
			OnConnectionSelected.RaiseEvent(this, new SchemaEventArgs<Connection>(entity));
		}

		public void ConnectionDeselected(Connection entity)
		{
			OnConnectionDeselected.RaiseEvent(this, new SchemaEventArgs<Connection>(entity));
		}

		public void SetAllToVisible(Visibility hidden)
		{
			bool visible = hidden == Visibility.Visible;

			visibleShapes.Clear();
			visibleConnections.Clear();

			if(visible)
			{
				foreach (var shape in Shapes)
					visibleShapes.Add(shape);
				foreach (var conn in Connections)
					visibleConnections.Add(conn);
			}	
		}

		public void SetVisibility(DiagramShape shape, Visibility visibility)
		{
			visibleShapes.Remove(shape);

			if (visibility == Visibility.Visible)
				visibleShapes.Add(shape);
		}

		public void SetVisibility(Connection connection, Visibility visibility)
		{
			visibleConnections.Remove(connection);

			if (visibility == Visibility.Visible)
				visibleConnections.Add(connection);
		}

		public bool CanConnect(DiagramShape shape, DiagramShape sourceShape)
		{
			return false;
		}

		public void AddShape(DiagramShape shape)
		{
			Schema.AddShape(shape);
			shape.Visible = false;
		}

		public void AddConnection(Connection connection)
		{
			Schema.AddConnection(connection);
			connection.Visible = false;

		}

		public void RemoveShape(DiagramShape shape)
		{
			Schema.RemoveShape(shape);
			shape.Visible = false;
		}

		public void RemoveConnection(Connection connection)
		{
			Schema.RemoveConnection(connection);
			connection.Visible = false;
		}

		public event EventHandler<GenericEventArgs<Object>> OnObjectSelected;
		public event EventHandler<GenericEventArgs<DiagramShape>> OnShapeSetAsPrimary;
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeAdded;
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeRemoved;
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionAdded;
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionRemoved;
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeSelected;
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeDeselected;
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionSelected;
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionDeselected;
		public event EventHandler OnSchemaCleared;
	}
}
