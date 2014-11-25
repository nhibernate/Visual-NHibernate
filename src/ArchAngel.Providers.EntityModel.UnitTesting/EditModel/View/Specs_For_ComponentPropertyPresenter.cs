using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs_For_ComponentPropertyPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			ComponentProperty obj = MockRepository.GenerateMock<ComponentProperty>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //ComponentPropertyPresenter presenter = new ComponentPropertyPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();
			
            //panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			ComponentProperty obj = MockRepository.GenerateMock<ComponentProperty>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //ComponentPropertyPresenter presenter = new ComponentPropertyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Events_Are_Attached_To()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			ComponentProperty obj = MockRepository.GenerateMock<ComponentProperty>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			//new ComponentPropertyPresenter(panel, form);

			form.AssertWasCalled(f => f.DatatypeChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.PropertyNameChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Is_Set_Up()
		{
			IPropertyForm form = MockRepository.GenerateMock<IPropertyForm>();
			ComponentProperty obj = MockRepository.GenerateMock<ComponentProperty>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //ComponentPropertyPresenter presenter = new ComponentPropertyPresenter(panel, form);
            //presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());

			form.AssertWasCalled(f => f.ShouldShowIsKeyProperty = false);
			form.AssertWasCalled(f => f.ShouldShowNullable = false);
			form.AssertWasCalled(f => f.ShouldShowReadOnly = false);

			form.AssertWasCalled(f => f.Datatype = obj.Type);
			form.AssertWasCalled(f => f.PropertyName = obj.Name);
			form.AssertWasCalled(f => f.SetVirtualProperties(obj.Ex));
			form.AssertWasCalled(f => f.SetValidationOptions(obj.ValidationOptions));
		}
	}
}
