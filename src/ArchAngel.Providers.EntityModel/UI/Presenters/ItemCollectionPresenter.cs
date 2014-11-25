using System;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public abstract class ItemCollectionPresenter<T> : PresenterBase where T : class, IModelObject
	{
		protected readonly ICollectionForm<T> form;
		protected abstract string ItemPropertyName { get; }
		protected IItemContainer itemContainer;
		protected abstract string ItemName { get; }

		protected ItemCollectionPresenter(IMainPanel panel, ICollectionForm<T> form) : base(panel)
		{
			this.form = form;

			SetupForm();
		}

		protected abstract T AddItemInternal();
		protected abstract void RemoveItemInternal(T obj);
		protected abstract void SetItemsOnForm();
		protected abstract bool CheckEntityType(IItemContainer obj);

		private void SetupForm()
		{
			form.AddItem += form_AddItem;
			form.RemoveItem += form_RemoveItem;
			form.SetItemTypeName(ItemName);
		}

		private void form_AddItem(object sender, EventArgs e)
		{
			T item = AddItemInternal();

			mainPanel.ShowObjectPropertyGrid(item);
			mainPanel.SyncCurrentlySelectedObject(item);
		}

		protected void form_RemoveItem(object sender, EventArgs e)
		{
			T item = form.SelectedItem;
			if(item == null) throw new InvalidOperationException("Cannot remove a item if one is not selected");

			RemoveItemInternal(item);
		}

		public void AttachToModel(IItemContainer obj)
		{
			if (!Detached) DetachFromModel();
			if (CheckEntityType(obj) == false) throw new ArgumentException("obj must be of type " + typeof (T) + "Container");
			itemContainer = obj;
			itemContainer.PropertyChanged += Entity_PropertyChanged;
			Detached = false;
			SetItemsOnForm();
		}

		internal override void AttachToModel(object obj)
		{
			if(obj is IItemContainer == false)
				throw new ArgumentException("Model must be an IItemContainer");
			AttachToModel((IItemContainer)obj);
		}

		public override void DetachFromModel()
		{
			if (Detached || itemContainer == null) return;

			itemContainer.PropertyChanged -= Entity_PropertyChanged;
			itemContainer = null;
			Detached = true;
		}

		private void Entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName == ItemPropertyName)
			{
				SetItemsOnForm();
			}
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}
