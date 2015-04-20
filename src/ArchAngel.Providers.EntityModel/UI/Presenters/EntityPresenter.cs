using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class EntityPresenter : PresenterBase
	{
		private readonly IEntityForm form;
		private Entity entity;
		private bool ignoreMappingSetChanges;

		public EntityPresenter(IMainPanel mainPanel, IEntityForm form)
			: base(mainPanel)
		{
			this.form = form;

			form.NameChanged += (sender, e) => { if (!Detached) entity.Name = form.EntityName; };
			form.AddNewProperty += form_AddNewProperty;
			form.PropertyNameChanged += form_PropertyNameChanged;
			form.CreateNewTableFromEntity += form_CreateNewTableFromEntity;
			form.RemoveEntity += form_RemoveEntity;
			form.RemoveProperty += form_RemoveProperty;
            form.EditProperty += form_EditProperty;
			form.MappingsChanged += form_MappingsChanged;
			form.NewMappingAdded += form_NewMappingAdded;
			form.CopyProperty += form_CopyProperty;
			form.MappingRemoved += form_MappingRemoved;
			form.SingleMappingChanged += form_SingleMappingChanged;
			form.DiscriminatorChanged += form_DiscriminatorChanged;
			form.ParentEntityChanged += form_ParentEntityChanged;
			form.ChildEntityAdded += form_ChildEntityAdded;
			form.ChildEntityRemoved += form_ChildEntityRemoved;

			SetupForm();
		}

	    private void form_EditProperty(object sender, GenericEventArgs<Property> e)
	    {
	        mainPanel.ShowObjectPropertyGrid(e.Object);
	    }

	    private void form_CopyProperty(object sender, GenericEventArgs<Property> e)
		{
			entity.CopyPropertyFromParent(e.Object);
			form.SetProperties(entity.ConcreteProperties);
		}

		void form_RemoveProperty(object sender, GenericEventArgs<Property> e)
		{
			e.Object.DeleteSelf();
		}

		private void form_PropertyNameChanged(object sender, PropertyNameChangeEventArgs e)
		{
			e.ChangedProperty.Name = e.NewName;
		}

		void form_ChildEntityRemoved(object sender, GenericEventArgs<Entity> e)
		{
			entity.RemoveChild(e.Object);
		}

		void form_ChildEntityAdded(object sender, GenericEventArgs<Entity> e)
		{
			entity.AddChild(e.Object);
		}

		void form_ParentEntityChanged(object sender, EventArgs e)
		{
			Entity parent = form.ParentEntity;

			if(parent == null)
			{
				entity.RemoveParent();
				return;
			}
			
			form.ParentEntity.AddChild(entity);
		}

		void form_DiscriminatorChanged(object sender, EventArgs e)
		{
			entity.Discriminator = form.Discriminator;
		}

		void form_SingleMappingChanged(object sender, GenericEventArgs<Property> e)
		{
			ignoreMappingSetChanges = true;
			e.Object.SetMappedColumn(form.GetMappedColumnFor(e.Object));
			ignoreMappingSetChanges = false;
		}

		void form_MappingRemoved(object sender, MappingEventArgs e)
		{
			entity.EntitySet.MappingSet.RemoveMapping(e.Mapping);
		}

		void form_NewMappingAdded(object sender, MappingEventArgs e)
		{
			entity.EntitySet.MappingSet.AddMapping(e.Mapping);
		}

		void form_MappingsChanged(object sender, EventArgs e)
		{
			entity.EntitySet.MappingSet.DeleteEntity(entity);

			foreach(var mapping in form.Mappings)
				entity.EntitySet.MappingSet.AddMapping(mapping);
		}

		void form_RemoveEntity(object sender, EventArgs e)
		{
			if (entity.EntitySet == null)
				return;

		    var tempEntity = entity;
		    var entitySet = entity.EntitySet;
		    DetachFromModel();

            //mainPanel.DisableDiagramRefresh();
            tempEntity.DeleteSelf();
            //mainPanel.ShowPropertyGrid(null);
            //mainPanel.ShowOnDiagram(entitySet);
            //mainPanel.EnableDiagramRefresh();
		}

		void form_CreateNewTableFromEntity(object sender, EventArgs e)
		{
			if(AnyContainersNull(entity))
			{
				throw new InvalidOperationException("Cannot create a new table because one of the parent containers is missing.");
			}

			var processor = new OneToOneEntityProcessor();
			var table = processor.CreateTable(entity);

			entity.EntitySet.MappingSet.Database.AddEntity(table);
		}

		private bool AnyContainersNull(Entity e)
		{
			return e.EntitySet == null || e.EntitySet.MappingSet == null;
		}

		void form_AddNewProperty(object sender, EventArgs e)
		{
			string name = "New_Property".GetNextName(entity.Properties.Select(p => p.Name));
			var property = new PropertyImpl(name);
			entity.AddProperty(property);
			
			form.SetProperties(entity.ConcreteProperties);
			form.Mappings = entity.Mappings();
			form.SetSelectedPropertyName(property);
		}

	    private void SetupForm()
		{
			if (Detached) return;

			bool containersNull = AnyContainersNull(entity);

			if (containersNull || entity.EntitySet.MappingSet.Database == null)
				form.SetAvailableTables(new List<ITable>());
			else
				form.SetAvailableTables(entity.EntitySet.MappingSet.Database.Tables);

			if(!containersNull)
                form.SetAvailableEntities(entity.EntitySet.Entities.Where(en => en != entity));

			form.SetProperties(entity.ConcreteProperties);

			form.EntityName = entity.Name;
			form.Mappings = entity.Mappings();
			form.Discriminator = entity.Discriminator;
			form.ParentEntity = entity.Parent;
			form.SetChildEntities(entity.Children);
			form.SetVirtualProperties(entity.Ex);
		}

		public override void DetachFromModel()
		{
			if (Detached) return;

			entity.PropertyChanged -= entity_PropertyChanged;
			if (AnyContainersNull(entity) == false)
				entity.EntitySet.MappingSet.PropertyChanged -= MappingSet_PropertyChanged;
			entity = null;
			Detached = true;
		}

		public void AttachToModel(Entity obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			form.Clear();
			entity = obj;
			entity.PropertyChanged += entity_PropertyChanged;
            entity.PropertiesChanged += entity_PropertiesChanged;

			if (AnyContainersNull(entity) == false)
				entity.EntitySet.MappingSet.PropertyChanged += MappingSet_PropertyChanged;
			Detached = false;

			SetupForm();
		}

        void entity_PropertiesChanged(object sender, CollectionChangeEvent<Property> e)
        {
            form.StartBulkUpdate();
            form.SetProperties(entity.ConcreteProperties);
            form.RefreshVirtualProperties();
            form.EndBulkUpdate();
        }

		void MappingSet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (ignoreMappingSetChanges) return;
			form.Mappings = entity.Mappings();
		}

		void entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();
			switch(e.PropertyName)
			{
				case "Name":
					form.EntityName = entity.Name;
					break;
				case "Discriminator":
					form.Discriminator = entity.Discriminator;
					break;
			}
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is Entity == false)
				throw new ArgumentException("Model must be an Entity");
			AttachToModel((Entity)obj);
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}
