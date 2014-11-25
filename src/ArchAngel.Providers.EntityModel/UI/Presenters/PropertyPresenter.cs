using System;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class PropertyPresenter : PresenterBase
	{
		private readonly IPropertyForm form;
		private Property property;

		public PropertyPresenter(IMainPanel panel, IPropertyForm form) : base(panel)
		{
			this.form = form;

			form.DatatypeChanged += (s, e) => { if (!Detached) property.Type = form.Datatype; };
			form.PropertyNameChanged += (s, e) => { if (!Detached) property.Name = form.PropertyName; };
			form.ReadOnlyChanged += (s, e) => { if (!Detached) property.ReadOnly = form.ReadOnly; };
			form.IsKeyChanged += (s, e) => { if (!Detached) property.IsKeyProperty = form.IsKeyProperty; };
			form.RemoveProperty += form_RemoveProperty;

			form.ShouldShowNullable = true;
			form.ShouldShowReadOnly = true;
			form.ShouldShowIsKeyProperty = true;

			SetupForm();
		}

		void form_RemoveProperty(object sender, EventArgs e)
		{
			if(!Detached)
			{
				property.DeleteSelf();
				mainPanel.ShowObjectPropertyGrid(property.Entity);
				DetachFromModel();
			}
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.Clear();

			form.Datatype = property.Type;
			form.PropertyName = property.Name;
			form.ReadOnly = property.ReadOnly;
			form.IsKeyProperty = property.IsKeyProperty;
			form.IsOveridden = property.IsOverridden;
			form.SetVirtualProperties(property.Ex);
			form.SetValidationOptions(property.ValidationOptions);
		}

		internal override void AttachToModel(object obj)
		{
			if(obj is Property == false)
				throw new ArgumentException("obj must be of type Property to be attached to. This one is a " + obj.GetType());
			AttachToModel(obj as Property);
		}

		public void AttachToModel(Property obj)
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
				case "IsKeyProperty":
					form.IsKeyProperty = property.IsKeyProperty;
					break;
				case "Name":
					form.PropertyName = property.Name;
					break;
				case "ReadOnly":
					form.ReadOnly = property.ReadOnly;
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