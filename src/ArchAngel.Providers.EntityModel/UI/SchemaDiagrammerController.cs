using System;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI.Filters;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using SchemaDiagrammer;

namespace ArchAngel.Providers.EntityModel.UI
{
	public class SchemaDiagrammerController
	{
		private readonly MainWindow window;
		private readonly SchemaController controller;
		private DiagramViewFilter currentFilter;
		private bool showMappedTables;

		public SchemaDiagrammerController(MainWindow window, EditModel editModelScreen)
		{
			this.window = window;
			controller = new SchemaController(window.Controller);
			controller.EditModel = editModelScreen;
			window.LayoutFinished += window_LayoutFinished;
		}

		public bool ShowMappedTables
		{
			get { return showMappedTables; }
			set 
			{
				showMappedTables = value;
				UpdateShowMappedTables();
			}
		}

	    public void DisableDiagramRefresh()
	    {
            if(currentFilter != null)
            {
                currentFilter.Disconnect();
            }
	    }

        public void EnableDiagramRefresh()
        {
            if (currentFilter != null && currentFilter.CanRun())
            {
                currentFilter.Connect();
                currentFilter.RunFilter();

                RunLayout();
            }
            else
            {
                ClearDiagram();
            }
        }

	    private void UpdateShowMappedTables()
		{
			if(currentFilter is RelatedEntitiesFilter)
			{
				var filter = (RelatedEntitiesFilter) currentFilter;
				filter.ShowTables = showMappedTables;
				filter.RunFilter();
				RunLayout();
				return;
			}
		}

		public void RunLayout()
		{
			window.ResetPanAndZoom();
			window.Layout();
		}

		void window_LayoutFinished(object sender, EventArgs e)
		{
			controller.ShowVisibleObjects();
		}

		public void ShowTag(IModelObject tag)
		{
            if(tag is CollectionPlaceholder)
            {
                var placeholder = (CollectionPlaceholder) tag;
                tag = placeholder.Entity;
            }

			IDatabase database;
			if (tag is ITable)
				ShowRelatedTables((ITable)tag, 1, false);
			else if (tag is Entity)
				ShowRelatedEntities(tag as Entity, 1, showMappedTables);
			else if (tag is IColumn)
				ShowRelatedTables(((Column)tag).Parent, 0, false);
			else if (tag is IKey)
				ShowRelatedTables(((IKey)tag).Parent, 0, false);
			else if (tag is IIndex)
				ShowRelatedTables(((IIndex)tag).Parent, 0, false);
			else if (tag is Property)
				ShowRelatedEntities(((Property)tag).Entity, 0, showMappedTables);
			else if (tag is Reference)
				ShowReference((Reference) tag);
			else if (tag is EntitySet)
				ShowEntities(tag as EntitySet);
			else if (GetSelectedDatabase(out database, tag))
				ShowTables(database);
			else if (tag is MappingSet)
				ShowEntitiesAndMapping(tag as MappingSet);
			else
				ShowNothing();
		}

		public void ClearDiagram()
        {
            controller.ClearSchema();
        }

		private bool GetSelectedDatabase(out IDatabase database, object tag)
		{
			database = null;

			if (tag is IDatabase)
			{
				database = tag as IDatabase;
				return true;
			}
			if (tag is IScriptBase)
			{
				IScriptBase entity = tag as IScriptBase;
				database = entity.Database;
				return true;
			}
			return false;
		}

		private void ShowReference(Reference reference)
		{
			if(reference == null)
			{
				controller.ClearSchema();
				return;
			}

			SetCurrentFilter(new ReferenceFilter(this, controller, reference));
			currentFilter.RunFilter();

			RunLayout();
		}

		public void ShowEntitiesAndMapping(MappingSet mappingSet)
		{
			if (mappingSet == null)
			{
				controller.ClearSchema();
				return;
			}

			SetCurrentFilter(new MappingSetShowAllFilter(this, controller, mappingSet));
			currentFilter.RunFilter();

			RunLayout();
		}

		public void ShowTables(IDatabase database)
		{
			if(database == null)
			{
				controller.ClearSchema();
				return;
			}

			SetCurrentFilter(new DatabaseShowAllFilter(this, controller, database));
			currentFilter.RunFilter();

			RunLayout();
		}

		public void ShowEntities(EntitySet entities)
		{
			if (entities == null)
			{
				controller.ClearSchema();
				return;
			}

			SetCurrentFilter(new EntitySetShowAllFilter(this, controller, entities));
			currentFilter.RunFilter();

			RunLayout();
		}

		public void ShowRelatedEntities(Entity entity, int degreeOfRelationshipsToShow, bool showTables)
		{
			if (entity == null) return;

			var filter = new RelatedEntitiesFilter(this, controller, entity);
			filter.DegreeOfRelationshipsToShow = degreeOfRelationshipsToShow;
			filter.ShowTables = showTables;
			filter.RunFilter();

			SetCurrentFilter(filter);

			RunLayout();

			controller.SetEntityAsSelected(entity);
		}

		public void ShowRelatedTables(ITable table, int degreeOfRelationshipsToShow, bool showMappings)
		{
			if (table == null) return;

			var filter = new RelatedEntityObjectsFilter(this, controller, table, degreeOfRelationshipsToShow);
			SetCurrentFilter(filter); 
			
			currentFilter.RunFilter();

			RunLayout();

			controller.SetEntityAsSelected(table);
		}


		private void SetCurrentFilter(DiagramViewFilter filter)
		{
			if (currentFilter != null) currentFilter.Disconnect();

			currentFilter = filter;

			if (currentFilter != null) currentFilter.Connect();
		}

		/// <summary>
		/// Workaround to ensure that each Shape has its Width and Height property set.
		/// This should be far enough off screen that the user cannot see them, yet they are still on
		/// the canvas so WPF will set their width and Height
		/// </summary>
		public void HideShapesOffScreen()
		{
			foreach (var shape in controller.Shapes)
			{
				shape.Visible = true;
				shape.Location = new System.Windows.Point(-10000, -10000);
			}

			window.UpdateLayout();
		}

		public void RefreshSchemaDiagrammer(IDatabase database)
		{
			controller.ClearSchema();
			controller.SetCurrentDatabase(database);

			AddEntitiesAndRelationships(database);
		}

		public void RefreshSchemaDiagrammer(MappingSet mappingSet)
		{
			controller.ClearSchema();
			controller.SetCurrentMappingSet(mappingSet);

			AddEntitiesAndRelationships(mappingSet.Database);
			AddEntitiesAndRelationships(mappingSet.EntitySet);
			AddMappings(mappingSet);
		}

		public void RefreshSchemaDiagrammer(EntitySet entitySet)
		{
			controller.ClearSchema();
			controller.SetCurrentEntitySet(entitySet);

			AddEntitiesAndRelationships(entitySet);
		}


		private void AddMappings(MappingSet set)
		{
			foreach(var mapping in set.Mappings)
			{
				if (mapping.FromTable == set.Database.GetNullEntityObject())
					continue;
				controller.AddRelationship(mapping);
			}
		}

		private void AddEntitiesAndRelationships(ITableContainer database)
		{
			foreach (var entity in database.Tables.OrderBy(t => t.Name))
			{
				controller.AddEntity(entity);
			}

			// Have to add all entities before adding relationships
			foreach (var entity in database.Tables.OrderBy(t => t.Name))
			{
				foreach (var rel in entity.Relationships.OrderBy(r => r.PrimaryTable.Name))
					controller.AddRelationship(rel);
			}
		}

		private void AddEntitiesAndRelationships(EntitySet entitySet)
		{
			foreach(var entity in entitySet.Entities.OrderBy(e => e.Name))
			{
				controller.AddEntity(entity);
			}

			// Have to add all entities before adding relationships
			foreach (Entity entity in entitySet.Entities.OrderBy(e => e.Name))
			{
				foreach (var rel in entity.References.OrderBy(r => r.End1Name))
					controller.AddRelationship(rel);
			}

			// Add all inheritance relationships
			foreach(var entity in entitySet.Entities.OrderBy(e => e.Name))
			{
				if(entity.Parent != null)
				{
					controller.AddParentChildRelationship(entity.Parent, entity);
				}
			}
		}

		public void ShowNothing()
		{
			controller.ClearVisibleSet();
			controller.ShowVisibleObjects();
		}
	}
}