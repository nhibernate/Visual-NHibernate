using System;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class TableCollectionPresenter : ItemCollectionPresenter<ITable>
	{
		public TableCollectionPresenter(IMainPanel panel, ICollectionForm<ITable> form) : base(panel, form)
		{
		}

		protected override string ItemPropertyName
		{
			get { return "Tables"; }
		}

		protected override string ItemName
		{
			get { return "Table"; }
		}

		protected override ITable AddItemInternal()
		{
			ITableContainer container = itemContainer as ITableContainer;
			if (container != null)
			{
				var table = new Table("NewTable");
				container.AddTable(table);
				return table;
			}

			throw new InvalidOperationException("No Entity attached to TableCollectionPresenter");
		}

		protected override void RemoveItemInternal(ITable obj)
		{
			ITableContainer container = itemContainer as ITableContainer;
			if (container != null)
			{
				container.RemoveTable(obj);
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to TableCollectionPresenter");
			}
		}

		protected override void SetItemsOnForm()
		{
			ITableContainer container = itemContainer as ITableContainer;
			if (container != null)
			{
				form.Items = container.Tables;
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to TableCollectionPresenter");
			}
		}

		protected override bool CheckEntityType(IItemContainer obj)
		{
			return obj is ITableContainer;
		}
	}
}