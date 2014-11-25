using System;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Specs_For_ColumnPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			IColumn obj = MockRepository.GenerateMock<IColumn>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //ColumnPresenter presenter = new ColumnPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();
			
            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Form_Events_Are_Attached_To()
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			//new ColumnPresenter(panel, form);

			form.AssertWasCalled(f => f.ColumnNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ColumnScaleChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ColumnSizeChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DatatypeChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DefaultChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DescriptionChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.IsNullableChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.OrdinalPositionChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.PrecisionChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Is_Set_Up()
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			IColumn obj = MockRepository.GenerateMock<IColumn>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //ColumnPresenter presenter = new ColumnPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());

			form.AssertWasCalled(f => f.ColumnName = obj.Name);
			form.AssertWasCalled(f => f.ColumnScale = obj.Scale);
			form.AssertWasCalled(f => f.ColumnSize = obj.Size);
			form.AssertWasCalled(f => f.Datatype = obj.Datatype);
			form.AssertWasCalled(f => f.Default = obj.Default);
			form.AssertWasCalled(f => f.Description = obj.Description);
			form.AssertWasCalled(f => f.IsNullable = obj.IsNullable);
			form.AssertWasCalled(f => f.OrdinalPosition = obj.OrdinalPosition);
			form.AssertWasCalled(f => f.Precision = obj.Precision);
			form.AssertWasCalled(f => f.SetVirtualProperties(obj.Ex));
		}
	}

	[TestFixture]
	public class When_Attaching_The_Model
	{
		[Test]
		public void Property_Changed_Registered()
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			IColumn obj = MockRepository.GenerateMock<IColumn>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();
			
            //var presenter = new ColumnPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Clear_Called()
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			IColumn obj = MockRepository.GenerateMock<IColumn>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new ColumnPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());
		}

		private void Test_Property_Called(Action<Column> setPropertyAction, Action<IColumnForm> action)
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			Column obj = new Column("Column1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new ColumnPresenter(panel, form);
            //presenter.AttachToModel(obj);

			setPropertyAction(obj);

			form.AssertWasCalled(action);
		}


		private void Property_Changed_Called_After_Form_Updates(Action<IColumnForm> setFormPropertyReturnValue,
			Action<IColumnForm> @event, Action<IColumnForm> expectedCall, object newPropertyValue, Func<Column, object> getParameter)
		{
			IColumnForm form = MockRepository.GenerateMock<IColumnForm>();
			Column obj = new Column("Column1");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //var presenter = new ColumnPresenter(panel, form);
            //presenter.AttachToModel(obj);

			IEventRaiser raiser = form.GetEventRaiser(@event);
			setFormPropertyReturnValue(form);

			raiser.Raise(form, null);

			form.AssertWasCalled(expectedCall);
			Assert.That(getParameter(obj), Is.EqualTo(newPropertyValue));
		}

		[Test]
		public void Name_Changed_Called()
		{
			Test_Property_Called(obj => obj.Name = "Column2", f => f.ColumnName = "Column2");
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.ColumnName).Return("Column2"),
				f => f.ColumnNameChanged += null,
				x => x.ColumnName = "Column2",
				"Column2", obj => obj.Name);
		}

		[Test]
		public void Default_Changed_Called()
		{
			Test_Property_Called(obj => obj.Default = "12345", f => f.Default = "12345");
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Default).Return("12345"),
				f => f.DefaultChanged += null,
				x => x.Default = "12345",
				"12345", 
				obj1 => obj1.Default);
		}

		[Test]
		public void DataType_Changed_Called()
		{
			Test_Property_Called(obj => obj.Datatype = "varchar(10)", f => f.Datatype = "varchar(10)");
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Datatype).Return("varchar(10)"),
				f => f.DatatypeChanged += null,
				x => x.Datatype = "varchar(10)",
				"varchar(10)",
				obj1 => obj1.Datatype);
		}

		[Test]
		public void Description_Changed_Called()
		{
			const string description = "Column2 description";

			Test_Property_Called(obj => obj.Description = description, f => f.Description = description);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Description).Return(description),
				f => f.DescriptionChanged += null,
				x => x.Description = description,
				description, obj1 => obj1.Description);
		}

		[Test]
		public void IsNullable_Changed_Called()
		{
			Test_Property_Called(obj => obj.IsNullable = true, f => f.IsNullable = true);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.IsNullable).Return(true),
				f => f.IsNullableChanged += null,
				x => x.IsNullable = true,
				true, obj1 => obj1.IsNullable);
		}

		[Test]
		public void OrdinalPosition_Changed_Called()
		{
			Test_Property_Called(obj => obj.OrdinalPosition = 2, f => f.OrdinalPosition = 2);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.OrdinalPosition).Return(2),
				f => f.OrdinalPositionChanged += null,
				x => x.OrdinalPosition = 2,
				2, obj1 => obj1.OrdinalPosition);
		}

		[Test]
		public void Precision_Changed_Called()
		{
			Test_Property_Called(obj => obj.Precision = 2, f => f.Precision = 2);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.Precision).Return(6),
				f => f.PrecisionChanged += null,
				x => x.Precision = 6,
				6, obj1 => obj1.Precision);
		}

		[Test]
		public void Scale_Changed_Called()
		{
			Test_Property_Called(obj => obj.Scale = 2, f => f.ColumnScale = 2);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.ColumnScale).Return(2),
				f => f.ColumnScaleChanged += null,
				x => x.ColumnScale = 2,
				2, obj1 => obj1.Scale);
		}

		[Test]
		public void Size_Changed_Called()
		{
			Test_Property_Called(obj => obj.Size = 2, f => f.ColumnSize = 2);
			Property_Changed_Called_After_Form_Updates(
				form => form.Stub(f => f.ColumnSize).Return(6),
				f => f.ColumnSizeChanged += null,
				x => x.ColumnSize = 6,
				6, obj1 => obj1.Size);
		}
	}
}
