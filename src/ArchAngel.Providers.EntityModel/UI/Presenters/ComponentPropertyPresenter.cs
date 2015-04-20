using System;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class ComponentPropertyPresenter : PresenterBase
	{
		private readonly IPropertyForm form;
		private ComponentProperty property;

		public ComponentPropertyPresenter(IMainPanel panel, IPropertyForm form)
			: base(panel)
		{
			this.form = form;

			form.ShouldShowNullable = false;
			form.ShouldShowReadOnly = false;
			form.ShouldShowIsKeyProperty = false;

			form.DatatypeChanged += (s, e) => { if (!Detached) property.Type = form.Datatype; };
			form.PropertyNameChanged += (s, e) => { if (!Detached) property.Name = form.PropertyName; };
			form.RemoveProperty += form_RemoveProperty;
			SetupForm();
		}

		void form_RemoveProperty(object sender, EventArgs e)
		{
			if(!Detached)
			{
				
				mainPanel.ShowObjectPropertyGrid(property.Specification);
				property.DeleteSelf();
				DetachFromModel();
			}
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.Clear();

			form.Datatype = property.Type;
			form.PropertyName = property.Name;
			form.SetVirtualProperties(property.Ex);
			form.SetValidationOptions(property.ValidationOptions);
		}

		internal override void AttachToModel(object obj)
		{
			if(obj is ComponentProperty == false)
				throw new ArgumentException("obj must be of type Property to be attached to. This one is a " + obj.GetType());
			AttachToModel(obj as ComponentProperty);
		}

		public void AttachToModel(ComponentProperty obj)
		{
			if(!Detached) DetachFromModel();

			property = obj;
			Detached = false;
			property.PropertyChanged += property_PropertyChanged;

			SetupForm();
		}

		private void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			switch(e.PropertyName)
			{
				case "Type":
					form.Datatype = property.Type;
					break;
				case "Name":
					form.PropertyName = property.Name;
					break;
			}
		}

		public override void DetachFromModel()
		{
			if (Detached || property == null) return;

			property.PropertyChanged -= property_PropertyChanged;
			property = null;
			Detached = true;
			form.Clear();
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}