using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs_For_EntityKeyPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Events_Are_Registered()
		{
			IEntityKeyForm form = MockRepository.GenerateMock<IEntityKeyForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //new EntityKeyPresenter(panel, form);

            //form.AssertWasCalled(x => x.AddNewProperty += null, c => c.IgnoreArguments());
            //form.AssertWasCalled(x => x.RemoveProperty += null, c => c.IgnoreArguments());
            //form.AssertWasCalled(x => x.ComponentChanged += null, c => c.IgnoreArguments());
            //form.AssertWasCalled(x => x.KeyTypeChanged += null, c => c.IgnoreArguments());
            //form.AssertWasCalled(x => x.RunKeyConversionWizard += null, c => c.IgnoreArguments());
		}
	}

	[TestFixture]
	public class When_Attaching_The_Key
	{
		[Test]
		public void The_Form_Is_Set_Up()
		{
			IEntityKeyForm form = MockRepository.GenerateMock<IEntityKeyForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			EntityKey obj = new EntityKeyImpl();
			obj.Parent = new EntityImpl("Parent");

			EntityKeyPresenter presenter = new EntityKeyPresenter(panel, form);
			presenter.AttachToModel(obj);

			form.AssertWasCalled(x => x.SetProperties(obj.Properties));
			form.AssertWasCalled(x => x.KeyType = obj.KeyType);
			form.AssertWasCalled(x => x.Component = obj.Component);
			form.AssertWasCalled(x => x.SetParentEntityName(obj.Parent.Name));
			form.AssertWasCalled(x => x.SetPossibleComponents(obj.Parent.Components));
			form.AssertWasCalled(x => x.SetVirtualProperties(obj.Ex));
		}
	}
}
