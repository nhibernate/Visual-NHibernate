using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Diagrammer;
using SchemaDiagrammer.Controller;
using SchemaDiagrammer.Model;
using SchemaDiagrammer.View;
using SchemaDiagrammer.View.Shapes;
using Slyce.Common;
using Colors=System.Windows.Media.Colors;
using IController=SchemaDiagrammer.Controller.IController;

namespace ArchAngel.Providers.EntityModel
{
	public class SchemaController : ISchemaController
	{
		private MappingSet currentMappingSet;
		private IDatabase currentDatabase;
		private EntitySet currentEntitySet;
		public IEnumerable<DiagramShape> Shapes {  get { return Controller.Shapes; } }
		public IEnumerable<Connection> Connections { get { return Controller.Connections; } }

		private readonly Dictionary<DiagramShape, IModelObject> ShapeLookup = new Dictionary<DiagramShape, IModelObject>();
		private readonly Dictionary<Connection, IModelObject> ConnectionLookup = new Dictionary<Connection, IModelObject>();

		private readonly Dictionary<Guid, DiagramShape> EntityLookup = new Dictionary<Guid, DiagramShape>();
		private readonly Dictionary<Guid, Connection> RelationshipLookup = new Dictionary<Guid, Connection>();

		private readonly Dictionary<VirtualRelationship, Connection> VirtualConnectionLookup = new Dictionary<VirtualRelationship, Connection>();
		private DiagramShape selectedShape;

		private class VirtualRelationship
		{
			public readonly Guid SourceEntity;
			public readonly Guid TargetEntity;

			public VirtualRelationship(Guid sourceEntity, Guid targetEntity)
			{
				SourceEntity = sourceEntity;
				TargetEntity = targetEntity;
			}

			public bool Equals(VirtualRelationship other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return other.SourceEntity.Equals(SourceEntity) && other.TargetEntity.Equals(TargetEntity);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof (VirtualRelationship)) return false;
				return Equals((VirtualRelationship) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (SourceEntity.GetHashCode()*397) ^ TargetEntity.GetHashCode();
				}
			}
		}

		public EditModel EditModel { get; set; }

		public IController Controller { get; private set; }

		public SchemaController(IController controller)
		{
			Controller = controller;
			//Schema.OnConnectionAdded += Schema_OnRelationshipAdded;
			//Schema.OnShapeAdded += Schema_OnTableAdded;
			controller.OnSchemaCleared += Schema_OnSchemaCleared;
			//Schema.OnShapeRemoved += Schema_OnTableRemoved;
			//Schema.OnConnectionRemoved += Schema_OnRelationshipRemoved;
            
            Controller.OnObjectSelected += (s, e) => ObjectSelected(e.Object);
			Controller.OnShapeSelected += (s, e) => DiagramEntitySelected(e.Entity);
			Controller.OnShapeDeselected += (s, e) => DiagramEntityDeselected(e.Entity);
			Controller.OnConnectionSelected += (s, e) => DiagramEntitySelected(e.Entity);
			Controller.OnConnectionDeselected += (s, e) => DiagramEntityDeselected(e.Entity);

			Controller.OnShapeSetAsPrimary += (s, e) => ObjectSetAsPrimary(e.Object);
		}

		public void NullAllModels()
		{
			currentMappingSet = null;
			currentEntitySet = null;
			currentDatabase = null;
		}

		public void SetCurrentDatabase(IDatabase database)
		{
			NullAllModels();
			currentDatabase = database;
		}

		public void SetCurrentMappingSet(MappingSet mappingSet)
		{
			NullAllModels();
			currentMappingSet = mappingSet;
		}

		public void SetCurrentEntitySet(EntitySet entitySet)
		{
			NullAllModels();
			currentEntitySet = entitySet;
		}

		public bool IsShowingDatabase(IDatabase database)
		{
			return currentDatabase != null && currentDatabase == database;
		}

		public bool IsShowingMappingSet(MappingSet mappingSet)
		{
			return currentMappingSet != null && currentMappingSet == mappingSet;
		}

		public bool IsShowingEntities(EntitySet entities)
		{
			return currentEntitySet != null && currentEntitySet == entities;
		}

		private void Schema_OnSchemaCleared(object sender, EventArgs e)
		{
			ClearInternalData();

			RaiseSchemaClearedEvent(new EventArgs());
		}

		public void AddEntity(ITable entity)
		{
			if (EntityLookup.ContainsKey(entity.InternalIdentifier))
				return;

			TableShape shape = new TableShape();
			AddAllItems(entity.Columns, shape.Columns);
			entity.PropertyChanged += dbEntity_PropertyChanged;
		    entity.ColumnsChanged += table_ColumnsChanged;

			AddEntity(shape, entity);
		}

		public void AddEntity(Entity entity)
		{
			if (EntityLookup.ContainsKey(entity.InternalIdentifier))
				return;

			EntityShape shape = new EntityShape();

			AddAllItems(entity.ConcreteProperties, shape.Properties);
			AddAllItems(entity.InheritedProperties, shape.InheritedProperties);

			shape.Background = new SolidColorBrush(Colors.Salmon);
			entity.PropertyChanged += entity_PropertyChanged;
            entity.PropertiesChanged += entity_PropertiesChanged;

			AddEntity(shape, entity);
		}


        void table_ColumnsChanged(object sender, CollectionChangeEvent<IColumn> e)
        {
            var table = sender as Table;
            if (table == null) return;

            var shape = GetShapeFor(table) as TableShape;
            if (shape == null) return;

            switch (e.ChangeType)
            {
                case CollectionChangeAction.Addition:
                    shape.Columns.Add(e.ChangedObject);
                    break;
                case CollectionChangeAction.Deletion:
                    shape.Columns.Remove(e.ChangedObject);
                    break;
            }
        }

        void entity_PropertiesChanged(object sender, CollectionChangeEvent<Property> e)
        {
            var entity = sender as Entity;
            if(entity== null) return;

            var shape = GetShapeFor(entity) as EntityShape;
            if(shape == null) return;

            switch(e.ChangeType)
            {
                case CollectionChangeAction.Addition:
                    if (e.ChangedObject.Entity.InternalIdentifier == entity.InternalIdentifier)
                    {
                        shape.Properties.Add(e.ChangedObject);
                    }
                    else
                    {
                        shape.InheritedProperties.Add(e.ChangedObject);
                    }
                    break;
                case CollectionChangeAction.Deletion:
                    if (e.ChangedObject.Entity.InternalIdentifier == entity.InternalIdentifier)
                    {
                        shape.Properties.Remove(e.ChangedObject);
                    }
                    else
                    {
                        shape.InheritedProperties.Remove(e.ChangedObject);
                    }
                    break;
            }
        }

		private void AddAllItems<T>(IEnumerable<T> items, ObservableCollection<T> collection)
		{
			collection.Clear();
			foreach(var prop in items)
				collection.Add(prop);
		}

		private void AddEntity(DiagramShape shape, IEntity entity)
		{
			IModelObject entityAsModelObj = entity as IModelObject;
			if (entityAsModelObj == null) throw new Exception("Expectation violated: new entity is not an IModelObject");

			if (EntityLookup.ContainsKey(entityAsModelObj.InternalIdentifier))	
				return;
		
			shape.EntityName = entity.EntityName;

			EntityLookup[entityAsModelObj.InternalIdentifier] = shape;
			ShapeLookup[shape] = entityAsModelObj;

			Controller.AddShape(shape);
		}

		public void AddRelationship(IRelationship relationship)
		{
			IModelObject relAsModelObj = relationship as IModelObject;
			if(relAsModelObj == null) throw new Exception("Expectation violated: new relationship is not an IModelObject");

			if (RelationshipLookup.ContainsKey(relAsModelObj.InternalIdentifier))
			    return;

			var newConn = new Connection(EntityLookup[((IModelObject)relationship.SourceEntity).InternalIdentifier],
			                             EntityLookup[((IModelObject)relationship.TargetEntity).InternalIdentifier]);

			newConn.SourceCardinality = relationship.SourceCardinality;
			newConn.TargetCardinality = relationship.TargetCardinality;

			newConn.ConnectionName = relationship.Name;

			if (relationship is Reference)
			{
				newConn.Stroke = new SolidColorBrush(Colors.Red);
				newConn.DrawingStrategy = new ReferenceDrawingStrategy();
			}
			else if (relationship is Mapping)
			{
				newConn.Stroke = new SolidColorBrush(Colors.BlueViolet);
			}

			newConn.Source.AddConnection(newConn);
			newConn.Target.AddConnection(newConn);

			RelationshipLookup[relAsModelObj.InternalIdentifier] = newConn;
			ConnectionLookup[newConn] = relAsModelObj;
			Controller.AddConnection(newConn);
		}

		public void AddParentChildRelationship(IModelObject parent, IModelObject child)
		{
			var newConn = new Connection(EntityLookup[parent.InternalIdentifier], EntityLookup[child.InternalIdentifier]);

			newConn.SourceCardinality = Cardinality.One;
			newConn.TargetCardinality = Cardinality.One;
			newConn.ConnectionName = "Parent";

			newConn.StrokeDashArray = new DoubleCollection {2, 2};
			newConn.StrokeDashCap = PenLineCap.Round;
			newConn.StrokeThickness = 2;
			newConn.Stroke = new SolidColorBrush(Colors.OrangeRed);
			newConn.IsVirtual = true;
			newConn.DrawingStrategy = new InheritanceDrawingStrategy();

			VirtualConnectionLookup.Add(new VirtualRelationship(parent.InternalIdentifier, child.InternalIdentifier), newConn);
			Controller.AddConnection(newConn);
		}

		public void RemoveParentChildRelationship(IModelObject parent, IModelObject child)
		{
			VirtualRelationship vr = GetVirtualRelationshipId(parent, child);

			if(VirtualConnectionLookup.ContainsKey(vr))
			{
				var connection = VirtualConnectionLookup[vr];
				Controller.RemoveConnection(connection);
			}
		}

		private VirtualRelationship GetVirtualRelationshipId(IModelObject parent, IModelObject child)
		{
			return new VirtualRelationship(parent.InternalIdentifier, child.InternalIdentifier);
		}

		public void ClearSchema()
		{
            ClearVisibleSet();
			Controller.ClearSchema();
			ClearInternalData();
		}

		private void ClearInternalData()
		{
			currentDatabase = null;
			currentEntitySet = null;
			currentMappingSet = null;
			ShapeLookup.Clear();
			ConnectionLookup.Clear();
			EntityLookup.Clear();
			RelationshipLookup.Clear();
			VirtualConnectionLookup.Clear();
		}

        //public void DiagramEntityDeselected(IDiagramEntity obj)
        //{
        //    if(EditModel != null) EditModel.ShowPropertyGrid(null);
        //}

		public void SetEntitiesAndRelationshipsToVisible(HashSet<IEntity> relatedObjectsSet)
		{
			foreach (var t in relatedObjectsSet)
			{
				SetVisibility(t, Visibility.Visible);
				foreach (var r in t.Relationships)
					if (relatedObjectsSet.Contains(r.SourceEntity) && relatedObjectsSet.Contains(r.TargetEntity))
						SetVisibility(r, Visibility.Visible);
				
			}
		}

		public void SetEntityAsSelected(IEntity entity)
		{
			var modelEntity = entity as Entity;
			if (modelEntity == null) return;

			ClearPrimaryShape();

			var shape = EntityLookup[modelEntity.InternalIdentifier];

			SetSelectedShape(shape);
		}

		private void SetSelectedShape(DiagramShape shape)
		{
			selectedShape = shape;
			shape.IsSelected = true;
		}

		private void ClearPrimaryShape()
		{
			if(selectedShape != null) 
				selectedShape.IsSelected = false;
			selectedShape = null;
		}

		public void DiagramEntitySelected(IDiagramEntity obj)
		{
			if (EditModel == null) return;

			if(obj is DiagramShape)
			{
				var shape = (DiagramShape) obj;
				IModelObject entity;
				if (ShapeLookup.TryGetValue(shape, out entity))
				{
					EditModel.ShowObjectPropertyGrid(entity);
					if(entity is ModelObject) EditModel.SyncCurrentlySelectedObject(entity as ModelObject);
				}
			}
			else if (obj is Connection)
			{
				var connection = (Connection) obj;
				IModelObject rel;
				if (ConnectionLookup.TryGetValue(connection, out rel))
				{
					EditModel.ShowObjectPropertyGrid(rel);
					if (rel is ModelObject) EditModel.SyncCurrentlySelectedObject(rel as ModelObject);
				}
			}
		}

        private void ObjectSelected(object o)
        {
            if (EditModel == null) return;
            if (o is ModelObject == false) return;

            EditModel.ShowObjectPropertyGrid(o as ModelObject);
            EditModel.SyncCurrentlySelectedObject(o as ModelObject);
        }

		private void ObjectSetAsPrimary(DiagramShape shape)
		{
			if (EditModel == null) return;
			if (shape == null) return;

			IModelObject entity;
			if (ShapeLookup.TryGetValue(shape, out entity))
			{
				EditModel.ShowObjectWithPrimaryFocus(entity);
			}
		}

		/// <summary>
		/// Occurs when a Table has been added
		/// </summary>
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnTableAdded;
		/// <summary>
		/// Occurs when a Table is removed
		/// </summary>
		public event EventHandler<SchemaEventArgs<DiagramShape>> OnTableRemoved;
		/// <summary>
		/// Occurs when a Relationship has been added
		/// </summary>
		public event EventHandler<SchemaEventArgs<Connection>> OnRelationshipAdded;
		/// <summary>
		/// Occurs when a Relationship is removed
		/// </summary>
		public event EventHandler<SchemaEventArgs<Connection>> OnRelationshipRemoved;

		/// <summary>
		/// Occurs when a Relationship is changed
		/// </summary>
		public event EventHandler OnSchemaCleared;

		public bool CanConnect(DiagramShape shape, DiagramShape targetShape)
		{
			//if(_ShapeToTable[shape] is ITable && _ShapeToTable[targetShape] is ITable)
			//{
			//    return true;
			//}

			return false;
		}

		public void AddAllToVisibleSet()
		{
			Controller.SetAllToVisible(Visibility.Visible);
		}

		public void ClearVisibleSet()
		{
			Controller.SetAllToVisible(Visibility.Hidden);
		}

		public void SetVisibility(IEntity t, Visibility vis)
		{
			if (t is IModelObject == false) return;
			Controller.SetVisibility(EntityLookup[((IModelObject)t).InternalIdentifier], vis);
		}

		public void SetVisibility(IRelationship t, Visibility vis)
		{
			if (t is IModelObject == false) return;

            if (!RelationshipLookup.ContainsKey(((IModelObject)t).InternalIdentifier))
                return; // GFH added this

			Controller.SetVisibility(RelationshipLookup[((IModelObject)t).InternalIdentifier], vis);
		}

		public void SetVisibilityOfVirtualRelationship(IModelObject source, IModelObject target, Visibility vis)
		{
			var vr = GetVirtualRelationshipId(source, target);
			if(VirtualConnectionLookup.ContainsKey(vr))
			{
				Controller.SetVisibility(VirtualConnectionLookup[vr], vis);
			}
		}

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

	    public DiagramShape GetShapeFor(IEntity entity)
		{
			if (entity is ModelObject == false) return null;
			return EntityLookup[((ModelObject)entity).InternalIdentifier];
		}

		public Connection GetConnectionFor(IRelationship relationship)
		{
			if (relationship is ModelObject == false) return null;
			return RelationshipLookup[((ModelObject)relationship).InternalIdentifier];
		}

		public void ShowVisibleObjects()
		{
			Controller.ShowVisibleObjects();
		}

		void entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			Entity entity = sender as Entity;
			if (entity == null) return;

            if (!EntityLookup.ContainsKey(entity.InternalIdentifier))
                return;

			EntityShape shape = EntityLookup[entity.InternalIdentifier] as EntityShape;
			if (shape == null) return;

			switch (e.PropertyName)
			{
				case "Name":
					shape.EntityName = entity.Name;
					break;
			}
		}

		void dbEntity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ITable entity = sender as ITable;
			if (entity == null) return;

			TableShape shape = EntityLookup[entity.InternalIdentifier] as TableShape;
			if (shape == null) return;

			switch (e.PropertyName)
			{
				case "Name":
					shape.EntityName = entity.Name;
					break;
			}
		}
	}
}