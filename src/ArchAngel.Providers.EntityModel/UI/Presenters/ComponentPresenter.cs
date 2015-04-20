using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class ComponentPresenter : PresenterBase
	{
		private readonly IComponentForm form;
		private Component component;
		private bool ignoreMappingSetChanges;

		public ComponentPresenter(IMainPanel panel, IComponentForm form)
			: base(panel)
		{
			this.form = form;

			form.PropertyMappingChanged += form_PropertyMappingChanged;
			form.ComponentNameChanged += form_ComponentNameChanged;
			form.DeleteComponent += form_DeleteComponent;

			SetupForm();
		}

		private void form_DeleteComponent(object sender, EventArgs e)
		{
			if (Detached) return;

			mainPanel.ShowPropertyGrid(null);
			component.DeleteSelf();
		}

		private void form_ComponentNameChanged(object sender, EventArgs e)
		{
			component.Name = form.ComponentName;
		}

		private void form_PropertyMappingChanged(object sender, GenericEventArgs<ComponentPropertyMarker> e)
		{
			ignoreMappingSetChanges = true;
			
			var newColumn = form.GetMappedColumnFor(e.Object);
			
			var ms = component.GetMappingSet();
			ms.ChangeMappingFor(e.Object).To(newColumn);

			ignoreMappingSetChanges = false;
		}

		void MappingSet_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (ignoreMappingSetChanges) return;

			ComponentMapping mapping = component.Mapping();
			if (mapping != null)
				form.SetMappings(mapping.Mappings);
			else
				form.SetMappings(new List<ColumnComponentPropertyMapping>());
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.Clear();

			form.SetProperties(component.Properties);
			form.ComponentName = component.Name;

			if(component.ParentEntity != null)
			{
				form.SetParentEntity(component.ParentEntity.Name);
				form.SetPossibleColumns(component.ParentEntity.MappedTables().SelectMany(t => t.Columns));
			}

			ComponentMapping mapping = component.Mapping();
			if (mapping != null)
				form.SetMappings(mapping.Mappings);

			form.SetVirtualProperties(component.Ex);
		}

		internal override void AttachToModel(object obj)
		{
			if(obj is Component == false)
				throw new ArgumentException("obj must be of type Component to be attached to. This one is a " + obj.GetType());
			AttachToModel(obj as Component);
		}

		public void AttachToModel(Component obj)
		{
			if(!Detached) DetachFromModel();

			component = obj;
			Detached = false;
			component.PropertyChanged += property_PropertyChanged;

			var ms = component.GetMappingSet();
			if(ms != null)
				ms.PropertyChanged += MappingSet_PropertyChanged;
			
			SetupForm();
		}

		private void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			switch(e.PropertyName)
			{

			}
		}

		public override void DetachFromModel()
		{
			if (Detached || component == null) return;

			component.PropertyChanged -= property_PropertyChanged;
			var ms = component.GetMappingSet();
			if (ms != null)
				ms.PropertyChanged -= MappingSet_PropertyChanged;
			
			component = null;
			Detached = true;
			form.Clear();

			
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}