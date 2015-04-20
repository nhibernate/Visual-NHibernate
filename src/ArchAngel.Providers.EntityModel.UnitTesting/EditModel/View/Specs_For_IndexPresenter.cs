using System;
using System.ComponentModel;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using System.Collections.Generic;

namespace Specs_For_IndexPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Events_Are_Registered()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //new IndexPresenter(panel, form);

			form.AssertWasCalled(f => f.DatatypeChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DescriptionChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.IndexNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.SelectedColumnChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteColumn += null, c => c.IgnoreArguments());
		}
	}

	[TestFixture]
	public class When_Attaching_The_Index
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IIndex obj = MockRepository.GenerateMock<IIndex>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //IndexPresenter presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();

            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void The_Form_Is_Filled()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IIndex obj = MockRepository.GenerateMock<IIndex>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //IndexPresenter presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Datatype = obj.Datatype);
			form.AssertWasCalled(f => f.Description = obj.Description);
			form.AssertWasCalled(f => f.IndexName = obj.Name);
			form.AssertWasCalled(f => f.Columns = obj.Columns);
			form.AssertWasCalled(f => f.SetVirtualProperties(obj.Ex));
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IIndex obj = MockRepository.GenerateMock<IIndex>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Clear_Called()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IIndex obj = MockRepository.GenerateMock<IIndex>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());
		}

		[Test]
		public void Name_Changed_Called()
		{
			const string name = "Index2";

			Test_Property_Called(obj => obj.Name = name, f => f.IndexName = name);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.IndexName).Return(name),
				f => f.IndexNameChanged += null,
				x => x.IndexName = name,
				name, obj => obj.Name);
		}

		[Test]
		public void DataType_Changed_Called()
		{
			var dt = DatabaseIndexType.Check;
			Test_Property_Called(obj => obj.Datatype = dt, f => f.Datatype = dt);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Datatype).Return(dt),
				f => f.DatatypeChanged += null,
				x => x.Datatype = dt,
				dt,
				obj1 => obj1.Datatype);
		}

		[Test]
		public void Description_Changed_Called()
		{
			const string description = "Index2 description";

			Test_Property_Called(obj => obj.Description = description, f => f.Description = description);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Description).Return(description),
				f => f.DescriptionChanged += null,
				x => x.Description = description,
				description, obj1 => obj1.Description);
		}

		[Test]
		public void Columns_Changed_Called()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IIndex obj = MockRepository.GenerateStub<IIndex>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			List<IColumn> columns = new List<IColumn>();
			obj.Stub(o => o.Columns).Return(columns);

			// Raise the PropertyChanged event for the Columns collection on the Index
			IEventRaiser raiser = obj.GetEventRaiser(e => e.PropertyChanged += null);
			raiser.Raise(obj, new PropertyChangedEventArgs("Columns"));

			form.AssertWasCalled(f => f.Columns = columns);
		}

		[Test]
		public void Column_Selection_Changed_Called()
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			IIndex obj = MockRepository.GenerateStub<IIndex>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			Column toReturn = new Column("Column1");
			form.Stub(o => o.SelectedColumn).Return(toReturn);

			// Raise the PropertyChanged event for the Columns collection on the Key
			IEventRaiser raiser = form.GetEventRaiser(e => e.SelectedColumnChanged += null);
			raiser.Raise(obj, null);

			panel.AssertWasCalled(p => p.ShowObjectPropertyGrid(toReturn));
		}

		private void Test_Property_Called(Action<Index> setPropertyAction, Action<IIndexForm> action)
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			Index obj = new Index("Index1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			setPropertyAction(obj);

			form.AssertWasCalled(action);
		}

		private void Property_Changed_Called_After_Form_Updates(Action<IIndexForm> setFormPropertyReturnValue,
			Action<IIndexForm> @event, Action<IIndexForm> expectedCall, object newPropertyValue, Func<Index, object> getParameter)
		{
			IIndexForm form = MockRepository.GenerateMock<IIndexForm>();
			Index obj = new Index("Index1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new IndexPresenter(panel, form);
            //presenter.AttachToModel(obj);

			IEventRaiser raiser = form.GetEventRaiser(@event);
			setFormPropertyReturnValue(form);

			raiser.Raise(form, null);

			form.AssertWasCalled(expectedCall);
			Assert.That(getParameter(obj), Is.EqualTo(newPropertyValue));
		}
	}
}
