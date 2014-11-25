using System;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class KeyCollectionPresenter : ItemCollectionPresenter<IKey>
	{
		public KeyCollectionPresenter(IMainPanel panel, ICollectionForm<IKey> form)
			: base(panel, form)
		{
		}

		protected override string ItemPropertyName
		{
			get { return "Keys"; }
		}

		protected override string ItemName
		{
			get { return "Key"; }
		}

		protected override IKey AddItemInternal()
		{
			IKeyContainer container = itemContainer as IKeyContainer;
			if (container != null)
			{
				return container.AddKey(new Key("NewKey"));
			}

			throw new InvalidOperationException("No Entity attached to KeyCollectionPresenter");
		}

		protected override void RemoveItemInternal(IKey obj)
		{
			IKeyContainer container = itemContainer as IKeyContainer;
			if (container != null)
			{
				container.RemoveKey(obj);
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to KeyCollectionPresenter");
			}
		}

		protected override void SetItemsOnForm()
		{
			IKeyContainer container = itemContainer as IKeyContainer;
			if (container != null)
			{
				form.Items = container.Keys;
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to KeyCollectionPresenter");
			}
		}

		protected override bool CheckEntityType(IItemContainer obj)
		{
			return obj is IKeyContainer;
		}
	}
}