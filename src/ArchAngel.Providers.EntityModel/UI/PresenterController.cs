using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using log4net;

namespace ArchAngel.Providers.EntityModel.UI
{
	internal class CollectionPlaceholder : ModelObject
	{
		public readonly IItemContainer Entity;
	    public readonly Type ItemType;

	    public CollectionPlaceholder(IItemContainer entity, Type itemType)
		{
		    Entity = entity;
		    ItemType = itemType;
		}

		public override string DisplayName
		{
			get { return "Collection"; }
		}
	}

	internal class PresenterController
	{
		private static readonly ILog log = LogManager.GetLogger(typeof (PresenterController));

		private readonly Dictionary<Type, PresenterBase> Presenters = new Dictionary<Type, PresenterBase>();

		private PresenterBase currentPresenter;

		public PresenterController(IMainPanel panel)
		{
			Presenters.Add(typeof(ITable), new DatabaseTablePresenter(panel, new FormDatabaseTable()));
			Presenters.Add(typeof(IColumn), new ColumnPresenter(panel, new FormColumn()));
			Presenters.Add(typeof(IKey), new KeyPresenter(panel, new FormKey()));
			//Presenters.Add(typeof(IIndex), new IndexPresenter(panel, new FormIndex()));
			Presenters.Add(typeof(IDatabase), new DatabasePresenter(panel, new FormDatabase()));
			//Presenters.Add(typeof(ITableContainer), new TableCollectionPresenter(panel, new FormCollectionAdapter<ITable>(c => c.Name)));
			//Presenters.Add(typeof(IIndexContainer), new IndexCollectionPresenter(panel, new FormCollectionAdapter<IIndex>(c => c.Name)));
			//Presenters.Add(typeof(IKeyContainer), new KeyCollectionPresenter(panel, new FormCollectionAdapter<IKey>(c => c.Name)));
			//Presenters.Add(typeof(IColumnContainer), new ColumnCollectionPresenter(panel, new FormCollectionAdapter<IColumn>(c => c.Name)));
			Presenters.Add(typeof(Relationship), new RelationshipPresenter(panel, new FormRelationship()));
			Presenters.Add(typeof(Reference), new ReferencePresenter(panel, new FormReference()));
			Presenters.Add(typeof(Entity), new EntityPresenter(panel, new FormEntity()));
			Presenters.Add(typeof(Property), new PropertyPresenter(panel, new FormProperty()));
			Presenters.Add(typeof(EntityKey), new EntityKeyPresenter(panel, new FormEntityKey()));
			Presenters.Add(typeof(Component), new ComponentPresenter(panel, new FormComponent()));
			Presenters.Add(typeof(ComponentSpecification), new ComponentSpecificationPresenter(panel, new FormComponentSpecification()));
			Presenters.Add(typeof(ComponentProperty), new ComponentPropertyPresenter(panel, new FormProperty()));
		}

		public T GetPresenter<T>() where T : PresenterBase
		{
			return (T) Presenters.Values.First(p => p is T);
		}

		public void ShowPresenter(PresenterBase presenter)
		{
			currentPresenter = presenter;
			currentPresenter.Show();
		}

		public void ShowPresenter(IModelObject obj)
		{
			currentPresenter = null;
			if (obj == null)
				return;

			// Find the first type in the list of types the presenter handles that the obj is an instance of.
			// This means that if an object implements two displayable interfaces, the first one we find will
			// be used.
		    object o = obj;
		    Type first = Presenters.Keys.FirstOrDefault(k => k.IsInstanceOfType(o));
            
            //if (obj is CollectionPlaceholder)
            //{
            //    CollectionPlaceholder placeholder = (CollectionPlaceholder) obj;
            //    if (placeholder.ItemType == typeof(ITable))
            //        first = typeof (ITableContainer);
            //    else if (placeholder.ItemType == typeof(IKey))
            //        first = typeof(IKeyContainer);
            //    else if (placeholder.ItemType == typeof(IIndex))
            //        first = typeof(IIndexContainer);
            //    else if (placeholder.ItemType == typeof(IColumn))
            //        first = typeof(IColumnContainer);
            //    obj = placeholder.Entity;
            //}

			if(first == null)
			{
				log.InfoFormat("We are not handling model objects of type {0} in PresenterController.ShowPresenter()", obj.GetType());
				return;
			}

			currentPresenter = Presenters[first];

			currentPresenter.DetachFromModel();
			currentPresenter.AttachToModel(obj);

			currentPresenter.Show();
		}

		public void ClearCurrentPresenter()
		{
			if(currentPresenter != null)
			{
				currentPresenter.DetachFromModel();
			}
		}

	    public PresenterBase GetCurrentPresenter()
	    {
	        return currentPresenter;
	    }
	}
}