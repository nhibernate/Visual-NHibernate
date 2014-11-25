using System;
using System.Collections.Generic;
using System.ComponentModel;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Specs_For_KeyPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Events_Are_Registered()
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //new KeyPresenter(panel, form);

			form.AssertWasCalled(x => x.KeyNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(x => x.DescriptionChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(x => x.KeytypeChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(x => x.EditColumn += null, c => c.IgnoreArguments());
			form.AssertWasCalled(x => x.AddNewColumn += null, c => c.IgnoreArguments());
			form.AssertWasCalled(x => x.RemoveColumn += null, c => c.IgnoreArguments());
            form.AssertWasCalled(x => x.DeleteKey += null, c => c.IgnoreArguments());
		}
	}

	[TestFixture]
	public class When_Attaching_The_Key
	{
		[Test]
		public void The_Form_Is_Set_Up()
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IKey obj = MockRepository.GenerateMock<IKey>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //KeyPresenter presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(x => x.KeyName = obj.Name);
			form.AssertWasCalled(x => x.Description = obj.Description);
			form.AssertWasCalled(x => x.Keytype = obj.Keytype);
			form.AssertWasCalled(x => x.Columns = obj.Columns);
			form.AssertWasCalled(x => x.SetVirtualProperties(obj.Ex));
			form.AssertWasCalled(x => x.SetAvailableColumns(null), c => c.IgnoreArguments());
		}

		[Test]
		public void Show_Property_Grid_Called()
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IKey obj = MockRepository.GenerateMock<IKey>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //KeyPresenter presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();

            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IKey obj = MockRepository.GenerateMock<IKey>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Clear_Called()
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IKey obj = MockRepository.GenerateMock<IKey>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());
		}

		[Test]
		public void Name_Changed_Called()
		{
			const string name = "ForeignKey";

			Test_Property_Called(obj => obj.Name = name, f => f.KeyName = name);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.KeyName).Return(name),
				f => f.KeyNameChanged += null,
				x => x.KeyName = name,
				name, obj => obj.Name);
		}

		[Test]
		public void KeyType_Changed_Called()
		{
			var dt = DatabaseKeyType.None;
			Test_Property_Called(obj => obj.Keytype = dt, f => f.Keytype = dt);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Keytype).Return(dt),
				f => f.KeytypeChanged += null,
				x => x.Keytype = dt,
				dt,
				obj1 => obj1.Keytype);
		}

		[Test]
		public void Description_Changed_Called()
		{
			const string description = "ForeignKey description";

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
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IKey obj = MockRepository.GenerateStub<IKey>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			List<IColumn> columns = new List<IColumn>();
			obj.Stub(o => o.Columns).Return(columns.AsReadOnly());

			// Raise the PropertyChanged event for the Columns collection on the Key
			IEventRaiser raiser = obj.GetEventRaiser(e => e.PropertyChanged += null);
			raiser.Raise(obj, new PropertyChangedEventArgs("Columns"));

			form.AssertWasCalled(f => f.Columns = columns);
		}

		[Test]
		public void Column_Selection_Changed_Called()
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			IKey obj = MockRepository.GenerateStub<IKey>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			Column toReturn = new Column("Column1");
			form.Stub(o => o.SelectedColumn).Return(toReturn);

			// Raise the PropertyChanged event for the Columns collection on the Key
			IEventRaiser raiser = form.GetEventRaiser(e => e.EditColumn += null);
			raiser.Raise(obj, null);

			panel.AssertWasCalled(p => p.ShowObjectPropertyGrid(toReturn));
		}

		private void Test_Property_Called(Action<Key> setPropertyAction, Action<IKeyForm> action)
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			Key obj = new Key("PrimaryKey");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			setPropertyAction(obj);

			form.AssertWasCalled(action);
		}


		private void Property_Changed_Called_After_Form_Updates(Action<IKeyForm> setFormPropertyReturnValue,
			Action<IKeyForm> @event, Action<IKeyForm> expectedCall, object newPropertyValue, Func<Key, object> getParameter)
		{
			IKeyForm form = MockRepository.GenerateMock<IKeyForm>();
			Key obj = new Key("PrimaryKey");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new KeyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			IEventRaiser raiser = form.GetEventRaiser(@event);
			setFormPropertyReturnValue(form);

			raiser.Raise(form, null);

			form.AssertWasCalled(expectedCall);
			Assert.That(getParameter(obj), Is.EqualTo(newPropertyValue));
		}
	}
}
