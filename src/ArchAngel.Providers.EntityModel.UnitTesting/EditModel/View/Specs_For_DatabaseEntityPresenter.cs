using System;
using System.Collections.ObjectModel;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace ArchAngel.Providers.EntityModel.UnitTesting.EditModel.View
{
	[TestFixture]
	public class Specs_For_DatabaseEntityPresenter
	{
        [Test]
        public void Form_Is_Setup_Correctly()
        {
            ITableForm form = MockRepository.GenerateMock<ITableForm>();
			ITable obj = MockRepository.GenerateMock<ITable>();
            IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //DatabaseTablePresenter presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);

            form.AssertWasCalled(f => f.Description = obj.Description);
            form.AssertWasCalled(f => f.EntityName = obj.EntityName);
            form.AssertWasCalled(f => f.SetColumns(obj.Columns));
			form.AssertWasCalled(f => f.SetVirtualProperties(obj.Ex));
			form.AssertWasCalled(f => f.SetKeys(obj.Keys));
        }

		[Test]
		public void Show_Property_Grid_Called()
		{
			ITableForm form = MockRepository.GenerateMock<ITableForm>();
			ITable obj = MockRepository.GenerateMock<ITable>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //DatabaseTablePresenter presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();

            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Clear_Called()
		{
			ITableForm form = MockRepository.GenerateMock<ITableForm>();
			ITable obj = MockRepository.GenerateMock<ITable>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());
		}

		[Test]
		public void Events_Registered()
		{
			ITableForm form = MockRepository.GenerateMock<ITableForm>();
			ITable obj = MockRepository.GenerateMock<ITable>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);

            obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
            
            form.AssertWasCalled(f => f.AddNewColumn += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteColumn += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.EditColumn += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.AddNewKey += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteKey += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.EditKey += null, c => c.IgnoreArguments());
            form.AssertWasCalled(f => f.DescriptionChanged += null, c => c.IgnoreArguments());
            form.AssertWasCalled(f => f.EntityNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteEntity += null, c => c.IgnoreArguments());
		}

        [Test]
        public void Triggering_AddColumn_Event_Adds_New_Column()
        {
            ITableForm form = MockRepository.GenerateMock<ITableForm>();
            IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();
			ITable obj = MockRepository.GenerateMock<ITable>();
            obj.Stub(o => o.Columns).Return(new ReadOnlyCollection<IColumn>(new IColumn[0]));

            //var presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);

            form.Raise(f => f.AddNewColumn += null, form, new EventArgs());

            obj.AssertWasCalled(o => o.AddColumn(Arg<IColumn>.Is.NotNull));
        }

		[Test]
		public void Name_Changed_Called()
		{
			const string name = "Index2";

			Test_Property_Called(obj => obj.Name = name, f => f.EntityName = name);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.EntityName).Return(name),
				f => f.EntityNameChanged += null,
				x => x.EntityName = name,
				name, obj => obj.Name);
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

		private void Test_Property_Called(Action<ITable> setPropertyAction, Action<ITableForm> action)
		{
			ITableForm form = MockRepository.GenerateMock<ITableForm>();
			Table obj = new Table("Index1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);

			setPropertyAction(obj);

			form.AssertWasCalled(action);
		}

		private void Property_Changed_Called_After_Form_Updates(Action<ITableForm> setFormPropertyReturnValue,
			Action<ITableForm> @event, Action<ITableForm> expectedCall, object newPropertyValue, Func<ITable, object> getParameter)
		{
			ITableForm form = MockRepository.GenerateMock<ITableForm>();
			Table obj = new Table("Index1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new DatabaseTablePresenter(panel, form);
            //presenter.AttachToModel(obj);

			IEventRaiser raiser = form.GetEventRaiser(@event);
			setFormPropertyReturnValue(form);

			raiser.Raise(form, null);

			form.AssertWasCalled(expectedCall);
			Assert.That(getParameter(obj), Is.EqualTo(newPropertyValue));
		}
	}
}
