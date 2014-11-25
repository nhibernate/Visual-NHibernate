using System;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class ComponentSpecificationPresenter : PresenterBase
	{
		private readonly IComponentSpecificationForm form;
		private ComponentSpecification spec;

		public ComponentSpecificationPresenter(IMainPanel panel, IComponentSpecificationForm form)
			: base(panel)
		{
			this.form = form;

			form.CreateNewProperty += form_CreateNewProperty;
			form.DeleteProperty += form_DeleteProperty;
			form.EditProperty += form_EditProperty;
			form.SpecNameChanged += form_NameChanged;
			form.PropertyNameChanged += form_PropertyNameChanged;
			form.NavigateToUsage += form_NavigateToUsage;
			form.AddNewUsage += form_AddNewUsage;
			form.DeleteSpec += form_DeleteSpec;

			SetupForm();
		}

		void form_DeleteSpec(object sender, EventArgs e)
		{
			if (Detached) return;

			spec.DeleteSelf();
		}

		void form_NavigateToUsage(object sender, Slyce.Common.GenericEventArgs<Entity> e)
		{
			if (Detached) return;

			mainPanel.ShowObjectPropertyGrid(e.Object);
		}

		void form_AddNewUsage(object sender, Slyce.Common.GenericEventArgs<Entity> e)
		{
			if (Detached) return;
			if (e.Object == null) throw new ArgumentNullException("e");

			spec.CreateImplementedComponentFor(e.Object, spec.Name + "_New");
		}

		void form_PropertyNameChanged(object sender, Slyce.Common.GenericEventArgs<ComponentProperty> e)
		{
			if (Detached) return;
			if (e.Object == null) return;

			e.Object.Name = form.GetPropertyName(e.Object);
		}

		void form_NameChanged(object sender, EventArgs e)
		{
			if (Detached) return;

			spec.Name = form.SpecName;
		}

		void form_EditProperty(object sender, Slyce.Common.GenericEventArgs<ComponentProperty> e)
		{
			mainPanel.ShowObjectPropertyGrid(e.Object);
		}

		void form_DeleteProperty(object sender, Slyce.Common.GenericEventArgs<ComponentProperty> e)
		{
			e.Object.DeleteSelf();
		}

		void form_CreateNewProperty(object sender, EventArgs e)
		{
			spec.AddProperty(new ComponentPropertyImpl("New_Property"));
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.Clear();

			form.SpecName = spec.Name;
			form.SetProperties(spec.Properties);
			form.SetUsages(spec.ImplementedComponents.Select(c => c.ParentEntity).Distinct());
			form.SetFullEntityList(spec.EntitySet.Entities);

			form.SetVirtualProperties(spec.Ex);
		}

		void spec_PropertiesChanged(object sender, Slyce.Common.CollectionChangeEvent<ComponentProperty> e)
		{
			form.SetProperties(spec.Properties);
		}

		private void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			form.RefreshVirtualProperties();

			switch (e.PropertyName)
			{
				case "Name":
					form.SpecName = spec.Name;
					break;
			}
		}

		void spec_ImplementedComponentsChanged(object sender, Slyce.Common.CollectionChangeEvent<Component> e)
		{
			form.SetUsages(spec.ImplementedComponents.Select(c => c.ParentEntity).Distinct());
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is ComponentSpecification == false)
				throw new ArgumentException("obj must be of type ComponentSpecification to be attached to. This one is a " + obj.GetType());
			AttachToModel(obj as ComponentSpecification);
		}

		public void AttachToModel(ComponentSpecification obj)
		{
			if(!Detached) DetachFromModel();

			spec = obj;
			Detached = false;
			spec.PropertyChanged += property_PropertyChanged;
			spec.PropertiesChanged += spec_PropertiesChanged;
			spec.ImplementedComponentsChanged += spec_ImplementedComponentsChanged;

			SetupForm();
		}

		public override void DetachFromModel()
		{
			if (Detached || spec == null) return;

			spec.PropertyChanged -= property_PropertyChanged;
			spec.PropertiesChanged -= spec_PropertiesChanged;
			spec.ImplementedComponentsChanged -= spec_ImplementedComponentsChanged;
			spec = null;
			Detached = true;
			form.Clear();
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}