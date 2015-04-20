using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs_For_DetailedDatabaseMergeResultPresenter
{
	[TestFixture]
	public class When_Creating_A_New_Presenter
	{
		[Test]
		public void Defaults_To_Text_View()
		{
			IDBMergeResultProcessor results = MockRepository.GenerateStub<IDBMergeResultProcessor>();
			results.Stub(r => r.GetTextResults()).Return("results");

			IDetailedDBMergeResultForm form = MockRepository.GenerateMock<IDetailedDBMergeResultForm>();
			var presenter = new DetailedDBMergeResultPresenter(form, results);

			presenter.Show();

			form.AssertWasCalled(f => f.TextResults = "results");
		}

		[Test]
		public void Attaches_To_The_GetTextResults_Event_Listener_And_Performs_That_Action()
		{
			IDBMergeResultProcessor results = MockRepository.GenerateStub<IDBMergeResultProcessor>();
			results.Stub(r => r.GetTextResults()).Return("results");

			IDetailedDBMergeResultForm form = MockRepository.GenerateMock<IDetailedDBMergeResultForm>();
			new DetailedDBMergeResultPresenter(form, results);

			form.Raise(f => f.GetTextResults += null, form, null); // raise the GetTextResults event

			form.AssertWasCalled(f => f.GetTextResults += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.TextResults = "results");
		}

		[Test]
		public void Attaches_To_The_GetXmlResults_Event_Listener_And_Performs_That_Action()
		{
			IDBMergeResultProcessor results = MockRepository.GenerateStub<IDBMergeResultProcessor>();
			results.Stub(r => r.GetXmlResults()).Return("results");

			IDetailedDBMergeResultForm form = MockRepository.GenerateMock<IDetailedDBMergeResultForm>();
			new DetailedDBMergeResultPresenter(form, results);

			form.Raise(f => f.GetXmlResults += null, form, null); // raise the GetTextResults event

			form.AssertWasCalled(f => f.GetXmlResults += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.XmlResults = "results");
		}

		[Test]
		public void Attaches_To_The_GetHtmlResults_Event_Listener_And_Performs_That_Action()
		{
			IDBMergeResultProcessor results = MockRepository.GenerateStub<IDBMergeResultProcessor>();
			results.Stub(r => r.GetHtmlResults()).Return("results");

			IDetailedDBMergeResultForm form = MockRepository.GenerateMock<IDetailedDBMergeResultForm>();
			new DetailedDBMergeResultPresenter(form, results);

			form.Raise(f => f.GetHtmlResults += null, form, null); // raise the GetTextResults event

			form.AssertWasCalled(f => f.GetHtmlResults += null, c => c.IgnoreArguments());
			form.AssertWasCalled(f => f.HtmlResults = "results");
		}
	}
}
