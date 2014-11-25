using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Is=NUnit.Framework.SyntaxHelpers.Is;

namespace Specs_For_DatabasePresenter
{
	[TestFixture]
	public class When_Constructing_A_Database_Loader
	{
		[Test]
		public void A_SQLCEDatabaseLoader_Is_Returned()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			DatabasePresenter presenter = new DatabasePresenter(panel, form);

			form.Stub(t => t.SelectedDatabaseType).Return(DatabaseTypes.SQLCE);
			form.Stub(t => t.SelectedDatabase).Return("1Table3Columns.sdf");

			IDatabaseLoader loader = presenter.CreateDatabaseLoader();
			Assert.That(loader, Is.Not.Null);
			Assert.That(loader, Is.TypeOf(typeof(SQLCEDatabaseLoader)));
			
			// Will throw an error if the database connection could not be established.
			loader.TestConnection();

			IDatabase db = loader.LoadDatabase();

			Assert.That(db.Name, Is.EqualTo("1Table3Columns"));
			Assert.That(db.Tables, Has.Count(1)); // Basic check to see if we got the correct database back.
		}

		[Test]
		public void A_SQLServer2005DatabaseLoader_Is_Returned()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			DatabasePresenter presenter = new DatabasePresenter(panel, form);

		    form.Stub(t => t.SelectedDatabaseType).Return(DatabaseTypes.SQLServer2005);
			form.Stub(t => t.ConnectionStringHelper).Return(new ConnectionStringHelper
			                                                	{
			                                                        CurrentDbType = DatabaseTypes.SQLServer2005,
                                                                    DatabaseName = "TestDatabase",
                                                                    Password = "p@ssword",
                                                                    ServerName = ".",
                                                                    UseFileName = false,
                                                                    UseIntegratedSecurity = false,
                                                                    UserName = "sa"
			                                                    });

			IDatabaseLoader loader = presenter.CreateDatabaseLoader();
			Assert.That(loader, Is.Not.Null);
			Assert.That(loader, Is.TypeOf(typeof(SQLServer2005DatabaseLoader)));
		}
	}

	[TestFixture]
	public class When_The_Test_Connection_Event_Is_Raised
	{
		[Test(Description = "This test will fail if the CreateDatabaseLoader test fails for"
			+ "SQLCE. I haven't figured out how to stub that part out yet.")]
		public void The_Presenter_Tests_The_Connection_Successfully()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			form.Expect(f => f.SetDatabaseOperationResults(Arg<DatabaseOperationResults>.Matches(pc => pc.Succeeded)));

			new DatabasePresenter(panel, form);

			form.Stub(t => t.SelectedDatabaseType).Return(DatabaseTypes.SQLCE);
			form.Stub(t => t.SelectedDatabase).Return("1Table3Columns.sdf");

			form.GetEventRaiser(t => t.TestConnection += null).Raise(form, null);

			form.VerifyAllExpectations();
		}

		[Test(Description = "This test will fail if the CreateDatabaseLoader test fails for"
	+ "SQLCE. I haven't figured out how to stub that part out yet.")]
		public void The_Presenter_Tests_The_Connection_Unsuccessfully()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			form.Expect(f => f.SetDatabaseOperationResults(Arg<DatabaseOperationResults>.Matches(pc => pc.Succeeded == false)));

			new DatabasePresenter(panel, form);

			form.Stub(t => t.SelectedDatabaseType).Return(DatabaseTypes.SQLCE);
			form.Stub(t => t.SelectedDatabase).Return("1Table3Columnsaaa.sdf");

			form.GetEventRaiser(t => t.TestConnection += null).Raise(form, null);

			form.VerifyAllExpectations();
		}
	}

	[TestFixture]
	public class When_The_Refresh_Schema_Event_Is_Raised
	{
		[Test(Description = "This test will fail if the CreateDatabaseLoader test fails for"
			+ "SQLCE. I haven't figured out how to stub that part out yet.")]
		public void The_Presenter_Refreshes_The_Database_And_Attempts_To_Show_The_Results()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();
			IDatabase db = MockRepository.GenerateStub<IDatabase>();

			db.Stub(d => d.Tables).Return(new ReadOnlyCollection<ITable>(new List<ITable>()));

			var presenter = new DatabasePresenter(panel, form);
			presenter.AttachToModel(db);

			AutoResetEvent arEvent = new AutoResetEvent(false);
			presenter.SchemaRefreshed += (sender, e) => arEvent.Set();

			form.Stub(t => t.SelectedDatabaseType).Return(DatabaseTypes.SQLCE);
			form.Stub(t => t.SelectedDatabase).Return("1Table3Columns.sdf");

			var raiser = form.GetEventRaiser(t => t.RefreshSchema += null);
			raiser.Raise(form, null);

			Assert.That(arEvent.WaitOne(10000, true), "The test timed out waiting for the SchemaRefreshed event to be triggered");

			panel.AssertWasCalled(p => p.ShowDatabaseRefreshResults(
				Arg<DatabaseMergeResult>.Matches(t => t.TableOperations.Count() == 1)));
		}

		[Test(Description = "This test will fail if the CreateDatabaseLoader test fails for"
	+ "SQLCE. I haven't figured out how to stub that part out yet.")]
		public void Creates_A_New_Database_If_No_Database_Has_Been_Previously_Loaded()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			DatabasePresenter presenter = new DatabasePresenter(panel, form);

			AutoResetEvent arEvent = new AutoResetEvent(false);
			presenter.NewDatabaseCreated += (sender, e) => arEvent.Set();

			Assert.That(presenter.Database, Is.Null);

			form.Stub(t => t.SelectedDatabaseType).Return(DatabaseTypes.SQLCE);
			form.Stub(t => t.SelectedDatabase).Return("1Table3Columns.sdf");

			var raiser = form.GetEventRaiser(t => t.RefreshSchema += null);
			raiser.Raise(form, null);

			Assert.That(arEvent.WaitOne(10000, true), "The test timed out waiting for the NewDatabaseCreated event to be triggered");

			Assert.That(presenter.Database, Is.Not.Null);
			Assert.That(presenter.Database.Name, Is.EqualTo("1Table3Columns"));
		}
	}

	[TestFixture]
	public class When_Constructing_A_Database_Presenter_From_An_Existing_SQLCE_Database
	{
		[Test]
		public void The_DatabaseName_Is_Wired_Up_Correctly()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			Database obj = new Database("Database1");
			obj.Loader = DatabaseLoaderFacade.GetSQLCELoader("asdfsdf.sdf");
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new DatabasePresenter(panel, form);
			presenter.AttachToModel(obj);

			form.AssertWasCalled(a => a.SelectedDatabaseType = DatabaseTypes.SQLCE);
			form.AssertWasCalled(a => a.SetDatabaseFilename("asdfsdf.sdf"));
			form.AssertWasCalled(a => a.DatabaseHelper = Arg<IServerAndDatabaseHelper>.Is.NotNull);
		}
	}

	[TestFixture]
	public class When_Constructing_A_Database_Presenter_With_No_Model
	{
		[Test]
		public void The_DatabaseHelper_Is_Filled_In()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			new DatabasePresenter(panel, form);

			form.AssertWasCalled(a => a.DatabaseHelper = Arg<IServerAndDatabaseHelper>.Is.NotNull);
		}
	}

	[TestFixture]
	public class When_Constructing_A_Database_Presenter_From_An_Existing_SQLServer2005_Database
	{
		[Test]
		public void The_DatabaseName_Is_Wired_Up_Correctly_With_DatabaseName()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			Database obj = new Database("Database1");

			ISQLServer2005DatabaseConnector connector = MockRepository.GenerateStub<ISQLServer2005DatabaseConnector>();
			
			connector.ConnectionInformation = new ConnectionStringHelper
			                                  	{
			                                  		UserName = "username",
			                                  		Password = "password",
			                                  		ServerName = "Server",
			                                  		UseFileName = true
			                                  	};
			connector.DatabaseName = "DatabaseName";
			obj.Loader = new SQLServer2005DatabaseLoader(connector);

			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new DatabasePresenter(panel, form);
			presenter.AttachToModel(obj);

			form.AssertWasCalled(a => a.SelectedDatabaseType = DatabaseTypes.SQLServer2005);
			form.AssertWasCalled(a => a.SetDatabaseFilename("DatabaseName"));
			form.AssertWasCalled(a => a.Username = "username");
			form.AssertWasCalled(a => a.Password = "password");
			form.AssertWasCalled(a => a.SelectedServerName = "Server");
			form.AssertWasCalled(a => a.DatabaseHelper = Arg<IServerAndDatabaseHelper>.Is.NotNull);
		}

		[Test]
		public void The_DatabaseName_Is_Wired_Up_Correctly_With_Database()
		{
			IDatabaseForm form = MockRepository.GenerateMock<IDatabaseForm>();
			Database obj = new Database("Database1");

			ISQLServer2005DatabaseConnector connector = MockRepository.GenerateStub<ISQLServer2005DatabaseConnector>();

			connector.ConnectionInformation = new ConnectionStringHelper
			                                  	{
			                                  		UserName = "username",
			                                  		Password = "password",
			                                  		ServerName = "Server",
			                                  		UseFileName = false
			                                  	};
			connector.DatabaseName = "DatabaseName";
			obj.Loader = new SQLServer2005DatabaseLoader(connector);

			IMainPanel panel = MockRepository.GenerateMock<IMainPanel>();

			var presenter = new DatabasePresenter(panel, form);
			presenter.AttachToModel(obj);

			form.AssertWasCalled(a => a.SelectedDatabaseType = DatabaseTypes.SQLServer2005);
			form.AssertWasCalled(a => a.SetDatabase("DatabaseName"));
			form.AssertWasCalled(a => a.Username = "username");
			form.AssertWasCalled(a => a.Password = "password");
			form.AssertWasCalled(a => a.SelectedServerName = "Server");
			form.AssertWasCalled(a => a.DatabaseHelper = Arg<IServerAndDatabaseHelper>.Is.NotNull);
		}
	}
}
