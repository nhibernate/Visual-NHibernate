using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using SchemaDiagrammer.Controller;

namespace ArchAngel.Providers.EntityModel.UI.Filters
{
	public class MappingSetShowAllFilter : MappingSetViewFilter
	{
		public MappingSetShowAllFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, MappingSet mappingSet) : base(diagramController, schemaController, mappingSet)
		{
		}

		protected override void RunFilterImpl()
		{
			schemaController.AddAllToVisibleSet();
		}

	    public override bool CanRun()
	    {
	        return true;
	    }
	}

	public class DatabaseShowAllFilter : DatabaseViewFilter
	{
		public DatabaseShowAllFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, IDatabase database)
			: base(diagramController, schemaController, database)
		{
		}

		protected override void RunFilterImpl()
		{
			schemaController.AddAllToVisibleSet();
		}

	    public override bool CanRun()
	    {
	        return true;
	    }

	    protected override void OnTableAdded(ITable table)
	    {
            base.OnTableAdded(table);
            schemaController.AddEntity(table);
            schemaController.SetVisibility(table, Visibility.Visible);
	    }

	    protected override void OnTableRemoved(ITable table)
	    {
            base.OnTableRemoved(table);

            schemaController.SetVisibility(table, Visibility.Hidden);
	    }

	    protected override void OnRelationshipAdded(Relationship relationship)
	    {
            base.OnRelationshipAdded(relationship);

            schemaController.SetVisibility(relationship, Visibility.Visible);
	    }

	    protected override void OnRelationshipRemoved(Relationship relationship)
	    {
            base.OnRelationshipRemoved(relationship);

            schemaController.SetVisibility(relationship, Visibility.Hidden);
	    }
	}

	public class EntitySetShowAllFilter : MappingSetViewFilter
	{
		private readonly EntitySet entitySet;

		public EntitySetShowAllFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, EntitySet entitySet)
			: base(diagramController, schemaController, entitySet.MappingSet)
		{
			this.entitySet = entitySet;
		}

		protected override void OnChildAdded(Entity child, Entity newParent)
		{
			base.OnChildAdded(child, newParent);

			schemaController.SetVisibilityOfVirtualRelationship(newParent, child, Visibility.Visible);
		}

		protected override void OnEntityAdded(Entity entity)
		{
			base.OnEntityAdded(entity);

			schemaController.SetVisibility(entity, Visibility.Visible);
		}

		protected override void OnReferenceAdded(Reference reference)
		{
			base.OnReferenceAdded(reference);

			schemaController.SetVisibility(reference, Visibility.Visible);
		}

		protected override void RunFilterImpl()
		{
			schemaController.SetEntitiesAndRelationshipsToVisible(new HashSet<IEntity>(entitySet.Entities.Cast<IEntity>()));
			foreach(var entity in entitySet.Entities)
			{
				if(entity.Parent != null)
				{
					schemaController.SetVisibilityOfVirtualRelationship(entity.Parent, entity, Visibility.Visible);
				}
			}
		}

	    public override bool CanRun()
	    {
	        return entitySet != null;
	    }
	}
}