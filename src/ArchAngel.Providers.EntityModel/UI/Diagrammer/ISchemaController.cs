using System;
using System.Collections.Generic;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using SchemaDiagrammer.Controller;
using SchemaDiagrammer.Model;
using SchemaDiagrammer.View;
using SchemaDiagrammer.View.Shapes;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	public interface ISchemaController
	{
		IEnumerable<DiagramShape> Shapes { get; }
		IEnumerable<Connection> Connections { get; }
		void ClearSchema();
		void AddEntity(ITable entity);
		void AddEntity(Entity entity);
		void AddRelationship(IRelationship relationship);
		DiagramShape GetShapeFor(IEntity entity);
		Connection GetConnectionFor(IRelationship relationship);
		void AddAllToVisibleSet();
		void ClearVisibleSet();
		void SetVisibility(IEntity t, Visibility vis);
		void SetVisibility(IRelationship t, Visibility vis);
		void DiagramEntitySelected(IDiagramEntity obj);
		void DiagramEntityDeselected(IDiagramEntity obj);
		void SetEntitiesAndRelationshipsToVisible(HashSet<IEntity> entities);
		void SetEntityAsSelected(IEntity entity);

		/// <summary>
		/// Occurs when a Table has been added
		/// </summary>
		event EventHandler<SchemaEventArgs<DiagramShape>> OnTableAdded;

		/// <summary>
		/// Occurs when a Table is removed
		/// </summary>
		event EventHandler<SchemaEventArgs<DiagramShape>> OnTableRemoved;

		/// <summary>
		/// Occurs when a Relationship has been added
		/// </summary>
		event EventHandler<SchemaEventArgs<Connection>> OnRelationshipAdded;

		/// <summary>
		/// Occurs when a Relationship is removed
		/// </summary>
		event EventHandler<SchemaEventArgs<Connection>> OnRelationshipRemoved;

		/// <summary>
		/// Occurs when a Relationship is changed
		/// </summary>
		event EventHandler OnSchemaCleared;

		bool CanConnect(DiagramShape shape, DiagramShape targetShape);
	}
}