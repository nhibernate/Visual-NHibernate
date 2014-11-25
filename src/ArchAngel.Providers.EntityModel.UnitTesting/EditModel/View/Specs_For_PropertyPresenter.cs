using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs_For_PropertyPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			Property obj = MockRepository.GenerateMock<Property>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //PropertyPresenter presenter = new PropertyPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();
			
            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			Property obj = MockRepository.GenerateMock<Property>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //PropertyPresenter presenter = new PropertyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Events_Are_Attached_To()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			Property obj = MockRepository.GenerateMock<Property>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			//new PropertyPresenter(panel, form);

			form.AssertWasCalled(f => f.DatatypeChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.IsKeyChanged += null, c => c.IgnoreArguments());
			//form.AssertWasCalled(f => f.IsVirtualChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.PropertyNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ReadOnlyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Is_Set_Up()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			Property obj = MockRepository.GenerateMock<Property>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //PropertyPresenter presenter = new PropertyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());

			form.AssertWasCalled(f => f.ShouldShowIsKeyProperty = true);
			form.AssertWasCalled(f => f.ShouldShowReadOnly = true);
			form.AssertWasCalled(f => f.ShouldShowNullable = true);

			form.AssertWasCalled(f => f.Datatype = obj.Type);
			form.AssertWasCalled(f => f.PropertyName = obj.Name);
			form.AssertWasCalled(f => f.ReadOnly = obj.ReadOnly);
			form.AssertWasCalled(f => f.IsKeyProperty = obj.IsKeyProperty);
			//form.AssertWasCalled(f => f.IsVirtual = obj.IsVirtual);
			form.AssertWasCalled(f => f.IsOveridden = obj.IsInherited);
			form.AssertWasCalled(f => f.SetVirtualProperties(obj.Ex));
			form.AssertWasCalled(f => f.SetValidationOptions(obj.ValidationOptions));
		}
	}
}
