using System;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class IndexCollectionPresenter : ItemCollectionPresenter<IIndex>
	{
		public IndexCollectionPresenter(IMainPanel panel, ICollectionForm<IIndex> form)
			: base(panel, form)
		{
		}

		protected override string ItemPropertyName
		{
			get { return "Indexes"; }
		}

		protected override string ItemName
		{
			get { return "Index"; }
		}

		protected override IIndex AddItemInternal()
		{
			IIndexContainer container = itemContainer as IIndexContainer;
			if (container != null)
			{
				return container.AddIndex(new Index("NewIndex"));
			}

			throw new InvalidOperationException("No Entity attached to IndexCollectionPresenter");
		}

		protected override void RemoveItemInternal(IIndex obj)
		{
			IIndexContainer container = itemContainer as IIndexContainer;
			if (container != null)
			{
				container.RemoveIndex(obj);
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to IndexCollectionPresenter");
			}
		}

		protected override void SetItemsOnForm()
		{
			IIndexContainer container = itemContainer as IIndexContainer;
			if (container != null)
			{
				form.Items = container.Indexes;
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to IndexCollectionPresenter");
			}
		}

		protected override bool CheckEntityType(IItemContainer obj)
		{
			return obj is IIndexContainer;
		}
	}
}