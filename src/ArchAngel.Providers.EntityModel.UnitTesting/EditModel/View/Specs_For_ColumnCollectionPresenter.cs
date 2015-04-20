using System;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace EntityProvider.Presenters.Specs_For_ColumnCollectionPresenter
{
	[TestFixture]
	public class Specs_For_ColumnCollectionPresenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = MockRepository.GenerateMock<IColumnContainer>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();
			
            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void New_Column_Added_Correctly()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = MockRepository.GenerateMock<IColumnContainer>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();


            //var presenter = new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();

			var raiser = form.GetEventRaiser(f => f.AddItem += null);
			raiser.Raise(form, new EventArgs());
			
			panel.AssertWasCalled(p => p.ShowObjectPropertyGrid(Arg<Column>.Matches(c => c != null && c.Name == "NewColumn")));
			//panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
			form.AssertWasCalled(f => f.Clear());
			obj.AssertWasCalled(o => o.AddColumn(Arg<Column>.Matches(c => c != null && c.Name == "NewColumn")));
		}

		[Test]
		public void Column_Removed_Correctly()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = MockRepository.GenerateMock<IColumnContainer>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = (ItemCollectionPresenter<IColumn>) new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();

			IColumn selectedColumn = new Column("Column1");
			form.Stub(f => f.SelectedItem).Return(selectedColumn);

			var raiser = form.GetEventRaiser(f => f.RemoveItem += null);
			raiser.Raise(form, new EventArgs());

			obj.AssertWasCalled(o => o.RemoveColumn(selectedColumn));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Exception_Thrown_If_Selected_Column_Is_Null()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = MockRepository.GenerateMock<IColumnContainer>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = (ItemCollectionPresenter<IColumn>) new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();

			form.Stub(f => f.SelectedItem).Return(null);

			var raiser = form.GetEventRaiser(f => f.RemoveItem += null);
			raiser.Raise(form, new EventArgs());
		}

		[Test]
		public void Property_Changed_Registered()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = MockRepository.GenerateMock<IColumnContainer>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = (ItemCollectionPresenter<IColumn>) new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
			Assert.That(presenter.Detached, Is.False);

			presenter.DetachFromModel();
			
			Assert.That(presenter.Detached, Is.True);
			obj.AssertWasCalled(o => o.PropertyChanged -= null, c => c.IgnoreArguments());
		}

		[Test]
		public void Re_Attaching_First_Detaches()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = MockRepository.GenerateMock<IColumnContainer>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = (ItemCollectionPresenter<IColumn>) new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
            //Assert.That(presenter.Detached, Is.False);

            //presenter.AttachToModel(obj);

            //Assert.That(presenter.Detached, Is.False);
			obj.AssertWasCalled(o => o.PropertyChanged -= null, c => c.IgnoreArguments());
			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments().Repeat.Twice());
		}

		private void Test_Event_Registered(Action<ICollectionForm<IColumn>> action)
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = new Table("Table1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = (ItemCollectionPresenter<IColumn>) new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(action, c => c.IgnoreArguments());
		}

		[Test]
		public void AddColumnRegistered()
		{
			Test_Event_Registered(x => x.AddItem += null);
		}

		[Test]
		public void RemoveColumnRegistered()
		{
			Test_Event_Registered(x => x.RemoveItem += null);
		}

		[Test]
		public void ColumnsUpdatedAfterCall()
		{
			ICollectionForm<IColumn> form = MockRepository.GenerateMock<ICollectionForm<IColumn>>();
			IColumnContainer obj = new Table("Table1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = (ItemCollectionPresenter<IColumn>) new ColumnCollectionPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Items = obj.Columns);

			obj.AddColumn(new Column("asdsad"));

			form.AssertWasCalled(f => f.Items = obj.Columns, c => c.Repeat.Twice());
		}
	}
}
