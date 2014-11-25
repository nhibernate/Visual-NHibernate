using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_ComponentPresenter
{
	[TestFixture]
	public class When_Constructing_The_Presenter
	{
		[Test]
		public void Show_Property_Grid_Called()
		{
			IComponentForm form = MockRepository.GenerateMock<IComponentForm>();
			Component obj = MockRepository.GenerateMock<Component>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

            //ComponentPresenter presenter = new ComponentPresenter(panel, form);
            //presenter.AttachToModel(obj);
            //presenter.Show();
			
			panel.AssertWasCalled(p => p.ShowPropertyGrid(form));
		}

		[Test]
		public void Property_Changed_Registered()
		{
			IComponentForm form = MockRepository.GenerateMock<IComponentForm>();
			Component obj = MockRepository.GenerateMock<Component>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			ComponentPresenter presenter = new ComponentPresenter(panel, form);
			presenter.AttachToModel(obj);

			obj.AssertWasCalled(o => o.PropertyChanged += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Events_Are_Attached_To()
		{
			IComponentForm form = MockRepository.GenerateMock<IComponentForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			new ComponentPresenter(panel, form);

			form.AssertWasCalled(f => f.PropertyMappingChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ComponentNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteComponent += null, c => c.IgnoreArguments());
		}

		[Test]
		public void Form_Is_Set_Up()
		{
			IComponentForm form = MockRepository.GenerateMock<IComponentForm>();
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
			Component obj = spec.CreateImplementedComponentFor(entity, "Street");
			mappingSet.EntitySet.AddComponentSpecification(spec);

			var mapping = new ComponentMappingImpl {ToComponent = obj, FromTable = table};
			mapping.AddPropertyAndColumn(obj.Properties[0], table.Columns[0]);
			mappingSet.AddMapping(mapping);

			form.Expect(f => f.SetMappings(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<ColumnComponentPropertyMapping>)action.Arguments[0]).Count(), Is.EqualTo(1)));

			form.Expect(f => f.SetPossibleColumns(null))
				.IgnoreArguments()
				.WhenCalled(action => Assert.That(((IEnumerable<IColumn>)action.Arguments[0]).Count(), Is.EqualTo(2)));

			ComponentPresenter presenter = new ComponentPresenter(panel, form);
			presenter.AttachToModel(obj);

			form.AssertWasCalled(f => f.Clear());

			form.AssertWasCalled(f => f.ComponentName = obj.Name);
			form.AssertWasCalled(f => f.SetParentEntity(obj.ParentEntity.Name));
			form.AssertWasCalled(f => f.SetVirtualProperties(obj.Ex));
			form.VerifyAllExpectations();
		}
	}
}
