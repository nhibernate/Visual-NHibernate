using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_ComponentSpecificationPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IComponentSpecificationForm form = MockRepository.GenerateMock<IComponentSpecificationForm>();
			ComponentSpecification obj = MockRepository.GenerateMock<ComponentSpecification>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			obj.Stub(o => o.ImplementedComponents).Return(new List<Component>().AsReadOnly());
			obj.Stub(o => o.EntitySet).Return(new EntitySetImpl());

			ComponentSpecificationPresenter presenter = new ComponentSpecificationPresenter(panel, form);
			presenter.AttachToModel(obj);
			presenter.Show();
			
			panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IComponentSpecificationForm form = MockRepository.GenerateMock<IComponentSpecificationForm>();
			ComponentSpecification obj = MockRepository.GenerateMock<ComponentSpecification>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			obj.Stub(o => o.ImplementedComponents).Return(new List<Component>().AsReadOnly());
			obj.Stub(o => o.EntitySet).Return(new EntitySetImpl());

			ComponentSpecificationPresenter presenter = new ComponentSpecificationPresenter(panel, form);
			presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
			obj.AssertWasCalled(o => o.ImplementedComponentsChanged += null, c => c.IgnoreArguments());
			obj.AssertWasCalled(o => o.PropertiesChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Events_Are_Attached_To()
		{
			IComponentSpecificationForm form = MockRepository.GenerateMock<IComponentSpecificationForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			new ComponentSpecificationPresenter(panel, form);

			form.AssertWasCalled(f => f.CreateNewProperty += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteProperty += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.EditProperty += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.SpecNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.AddNewUsage += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.NavigateToUsage += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.PropertyNameChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Is_Set_Up()
		{
			IComponentSpecificationForm form = MockRepository.GenerateMock<IComponentSpecificationForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var mappingSet = new MappingSetImpl();
			var entity = new EntityImpl("Entity1");
			entity.AddProperty(new PropertyImpl("Property1"));
			var table = new Table("Table1");
			table.AddColumn(new Column("Column1"));
			table.AddColumn(new Column("Street"));
			mappingSet.EntitySet.AddEntity(entity);

			mappingSet.ChangeMappedColumnFor(entity.Properties.First()).To(table.Columns[0]);

			ComponentSpecification spec = new ComponentSpecificationImpl("Address");
			spec.AddProperty(new ComponentPropertyImpl("Street"));
			Component component = spec.CreateImplementedComponentFor(entity, "Street");
			mappingSet.EntitySet.AddComponentSpecification(spec);

			var mapping = new ComponentMappingImpl {ToComponent = component, FromTable = table};
			mapping.AddPropertyAndColumn(component.Properties[0], table.Columns[0]);
			mappingSet.AddMapping(mapping);

			form.Expect(f => f.SetProperties(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<ComponentProperty>)action.Arguments[0]).Count(), Is.EqualTo(1)));

			form.Expect(f => f.SetUsages(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<Entity>)action.Arguments[0]).Count(), Is.EqualTo(1)));

			form.Expect(f => f.SetFullEntityList(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<Entity>)action.Arguments[0]).Count(), Is.EqualTo(1)));

			ComponentSpecificationPresenter presenter = new ComponentSpecificationPresenter(panel, form);
			presenter.AttachToModel(spec);

			form.AssertWasCalled(f => f.Clear());
			form.AssertWasCalled(f => f.SpecName = spec.Name);
			form.AssertWasCalled(f => f.SetVirtualProperties(spec.Ex));

			form.VerifyAllExpectations();
		}
	}
}
