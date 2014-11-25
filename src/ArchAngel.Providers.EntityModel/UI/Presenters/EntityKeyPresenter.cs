using System;
using System.ComponentModel;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.UI.Wizards;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class EntityKeyPresenter : PresenterBase
	{
		private EntityKey key;
		private readonly IEntityKeyForm form;

		public EntityKeyPresenter(IMainPanel panel, IEntityKeyForm form)
			: base(panel)
		{
			this.form = form;

			form.AddNewProperty += form_AddNewProperty;
			form.ComponentChanged += form_ComponentChanged;
			form.KeyTypeChanged += form_KeyTypeChanged;
			form.RemoveProperty += form_RemoveProperty;
			form.RunKeyConversionWizard += form_RunKeyConversionWizard;
		}

		void form_RunKeyConversionWizard(object sender, EventArgs e)
		{
			var modelInformation = new ConvertKeyToComponentModelInformation(key.GetMappingSet(), key);
			var wizard = new FormConvertKeyToComponent(modelInformation);
			var dialogResult = mainPanel.ShowDialog(wizard);

			if(dialogResult == DialogResult.OK)
			{
				var result = wizard.GetConversionResult();
				result.ApplyChanges(key.GetMappingSet(), key);
			}
		}

		void form_RemoveProperty(object sender, GenericEventArgs<Property> e)
		{
			if(Detached) return;

			key.RemoveProperty(e.Object);
		}

		void form_KeyTypeChanged(object sender, EventArgs e)
		{
			if(Detached) return;

			key.KeyType = form.KeyType;
		}

		void form_ComponentChanged(object sender, EventArgs e)
		{
			if (Detached) return;

			key.Component = form.Component;
		}

		void form_AddNewProperty(object sender, GenericEventArgs<Property> e)
		{
			if(Detached) return;

			key.AddProperty(e.Object);
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.SetProperties(key.Properties);
			form.KeyType = key.KeyType;
			form.SetParentEntityName(key.Parent.Name);
			form.SetPossibleProperties(key.Parent.Properties);
			form.SetPossibleComponents(key.Parent.Components);
			form.Component = key.Component;
			
			form.SetVirtualProperties(key.Ex);
		}

		private void key_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "Component")
			{
				form.Component = key.Component;
				form.KeyType = key.KeyType;
			}
		}
		
		private void key_PropertiesChanged(object sender, CollectionChangeEvent<Property> args)
		{
			form.SetProperties(key.Properties);
			form.KeyType = key.KeyType;
		}

		public override void DetachFromModel()
		{
			if (Detached) return;

			key.PropertyChanged -= key_PropertyChanged;
			key.PropertiesChanged -= key_PropertiesChanged;
			key.Parent.PropertyChanged -= Entity_PropertyChanged;
			key = null;
			Detached = true;
		}

		void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(Detached) return;

			switch(e.PropertyName)
			{
				case "Components":
					form.SetPossibleComponents(key.Parent.Components);
					break;
				case "Properties":
					form.SetPossibleProperties(key.Parent.Properties);
					break;
			}
		}

		public void AttachToModel(EntityKey obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			form.Clear();
			key = obj;
			key.PropertyChanged += key_PropertyChanged;
			key.PropertiesChanged += key_PropertiesChanged;
			key.Parent.PropertyChanged += Entity_PropertyChanged;

			Detached = false;

			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is EntityKey == false)
				throw new ArgumentException("Model must be an EntityKey");
			AttachToModel((EntityKey)obj);
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}