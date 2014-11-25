using System;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class ColumnCollectionPresenter : ItemCollectionPresenter<IColumn>
	{
		public ColumnCollectionPresenter(IMainPanel panel, ICollectionForm<IColumn> form)
			: base(panel, form)
		{
			
		}

		protected override string ItemPropertyName
		{
			get { return "Columns"; }
		}

		protected override string ItemName
		{
			get { return "Column"; }
		}

		protected override IColumn AddItemInternal()
		{
			IColumnContainer container = itemContainer as IColumnContainer;
			if (container != null)
			{
				var column = new Column("NewColumn");
				container.AddColumn(column);
				return column;
			}

			throw new InvalidOperationException("No Entity attached to ColumnCollectionPresenter");
		}

		protected override void RemoveItemInternal(IColumn obj)
		{
			IColumnContainer container = itemContainer as IColumnContainer;
			if (container != null)
			{
				container.RemoveColumn(obj);
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to ColumnCollectionPresenter");
			}
		}

		protected override void SetItemsOnForm()
		{
			form.Clear();
			IColumnContainer container = itemContainer as IColumnContainer;
			if (container != null)
			{
				form.Items = container.Columns;
			}
			else
			{
				throw new InvalidOperationException("No Entity attached to ColumnCollectionPresenter");
			}
		}

		protected override bool CheckEntityType(IItemContainer obj)
		{
			return obj is IColumnContainer;
		}
	}
}