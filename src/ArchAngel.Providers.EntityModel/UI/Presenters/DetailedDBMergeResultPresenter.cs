using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class DetailedDBMergeResultPresenter
	{
		private readonly IDetailedDBMergeResultForm form;
		private readonly IDBMergeResultProcessor results;

		public DetailedDBMergeResultPresenter(IDetailedDBMergeResultForm form, IDBMergeResultProcessor results)
		{
			this.form = form;
			this.results = results;

			form.GetTextResults += (sender, e) => SetTextResults();
			form.GetHtmlResults += (sender, e) => SetHtmlResults();
			form.GetXmlResults += (sender, e) => SetXmlResults();
		}

		private void SetXmlResults()
		{
			form.XmlResults = results.GetXmlResults();
		}

		private void SetHtmlResults()
		{
			form.HtmlResults = results.GetHtmlResults();
		}

		private void SetTextResults()
		{
			form.TextResults = results.GetTextResults();
		}

		public void Show()
		{
			SetTextResults();
		}
	}
}
