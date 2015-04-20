using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Interfaces.SchemaDiagrammer;
using SchemaDiagrammer.View.Shapes;

namespace SchemaDiagrammer.Model
{
	public class Schema
	{
		public Schema()
		{
			_Tables = new List<DiagramShape>();
			_Relationships = new List<Connection>();
		}

		public void AddShape(DiagramShape entity)
		{
			_Tables.Add(entity);
			RaiseEvent(entity, OnShapeAdded);
		}

		public void AddConnection(Connection rel)
		{
			_Relationships.Add(rel);
			RaiseEvent(rel, OnConnectionAdded);
		}

		public void RemoveShape(DiagramShape entity)
		{
			_Tables.Remove(entity);
			RaiseEvent(entity, OnShapeRemoved);
		}

		public void RemoveConnection(Connection relationship)
		{
			_Relationships.Remove(relationship);
			RaiseEvent(relationship, OnConnectionRemoved);
		}

		public void Clear()
		{
			_Tables.Clear();
			_Relationships.Clear();

			RaiseSchemaClearedEvent(new EventArgs());
		}

		#region Properties

		private readonly List<DiagramShape> _Tables;
		private readonly List<Connection> _Relationships;

		/// <summary>
		/// The Shapes in this schema
		/// </summary>
		public IEnumerable<DiagramShape> Shapes
		{
			get { return _Tables; }
		}

		/// <summary>
		/// The Connections in this schema
		/// </summary>
		public IEnumerable<Connection> Connections
		{
			get { return _Relationships; }
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when a Table has been added
		/// </summary>
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeAdded;
		/// <summary>
		/// Occurs when a Table is removed
		/// </summary>
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnShapeRemoved;
		/// <summary>
		/// Occurs when a Relationship has been added
		/// </summary>
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionAdded;
		/// <summary>
		/// Occurs when a Relationship is removed
		/// </summary>
		public event EventHandler<SchemaEventArgs<Connection>> OnConnectionRemoved;
		/// <summary>
		/// Occurs when a Relationship is changed
		/// </summary>
		public event EventHandler OnSchemaCleared;

		/// <summary>
		/// Raises the <see cref="OnSchemaCleared"/> event
		/// </summary>
		/// <param name="e">Event argument</param>
		protected virtual void RaiseSchemaClearedEvent(EventArgs e)
		{
			EventHandler handler = OnSchemaCleared;
			if (handler != null)
			{
				handler(this, e);
			}
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

		#endregion
	}

	public class SchemaEventArgs<T> : EventArgs
	{
		public T Entity { get; set; }
        
		public SchemaEventArgs(T entity)
		{
			Entity = entity;
		}
	}
}