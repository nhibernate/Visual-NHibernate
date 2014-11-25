using System;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_RelationshipPresenter
{
	[TestFixture]
	public class When_Constructing_A_RelationshipPresenter
	{
		[Test]
		public void The_Presenter_Hooks_Up_To_The_Right_Events()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IRelationshipForm form = MockRepository.GenerateMock<IRelationshipForm>();

            //new RelationshipPresenter(mainPanel, form);

			form.AssertWasCalled(f => f.RelationshipNameChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.PrimaryKeyChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.ForeignKeyChanged += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.DeleteRelationship += null, c => c.IgnoreArguments());
		}
	}

	internal class TestHelper
	{
		public static Relationship GetRelationship()
		{
			Relationship relationship = new RelationshipImpl();
			relationship.Name = "Name";

			var table1 = new Table("Table1");
			table1.AddColumn(new Column("ColumnX"));
			relationship.PrimaryKey = new Key("", DatabaseKeyType.Primary) { Parent = table1 };
			relationship.PrimaryTable = table1;
			relationship.PrimaryKey.AddColumn("ColumnX");

			var table2 = new Table("Table2");
			table2.AddColumn(new Column("Column1"));
			relationship.ForeignKey = new Key("", DatabaseKeyType.Foreign) { Parent = table2 };
			relationship.ForeignTable = table2;
			relationship.ForeignKey.AddColumn("Column1");

			var database = new Database("");
			database.AddTable(table1);
			database.AddTable(table2);
			relationship.Database = database;
			return relationship;
		}
	}

	[TestFixture]
	public class When_Attaching_A_Relationship
	{
		[Test]
		public void The_Presenter_Fills_In_The_Form()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IRelationshipForm form = MockRepository.GenerateMock<IRelationshipForm>();

			Relationship relationship = TestHelper.GetRelationship();

			var presenter = new RelationshipPresenter(mainPanel, form);
			presenter.AttachToModel(relationship);

			form.AssertWasCalled(f => f.Clear());
			form.AssertWasCalled(f => f.RelationshipName = "Name");
			form.AssertWasCalled(f => f.PrimaryKey = relationship.PrimaryKey);
			form.AssertWasCalled(f => f.ForeignKey = relationship.ForeignKey);
			form.AssertWasCalled(f => f.SetPossiblePrimaryKeys(null), c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.SetPossibleForeignKeys(null), c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.SetVirtualProperties(relationship.Ex));
		}
	}

	[TestFixture]
	public class When_Changing_The_Name_On_The_Form
	{
		[Test]
		public void The_Presenter_Changes_The_Name_On_The_Relationship()
		{
			IMainPanel mainPanel = MockRepository.GenerateStub<IMainPanel>();
			IRelationshipForm form = MockRepository.GenerateStub<IRelationshipForm>();
			Relationship relationship = TestHelper.GetRelationship();

			var presenter = new RelationshipPresenter(mainPanel, form);
			presenter.AttachToModel(relationship);

			form.RelationshipName = "NewName";
			form.GetEventRaiser(f => f.RelationshipNameChanged += null).Raise(form, new EventArgs());

			Assert.That(relationship.Name, Is.EqualTo("NewName"));
		}
	}
}

