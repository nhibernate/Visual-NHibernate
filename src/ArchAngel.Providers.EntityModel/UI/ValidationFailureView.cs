using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Rules;
using ArchAngel.Providers.EntityModel.Model;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class ValidationFailureView : UserControl
	{
		public event EventHandler CloseView;
		public event EventHandler ReValidateRequested;
		public event EventHandler<GenericEventArgs<IModelObject>> NavigateToObject;

		public ValidationFailureView()
		{
			InitializeComponent();
		}

		public void SetValidationResults(IEnumerable<IValidationResult> results)
		{
			ClearResultsView();

			foreach (var result in results)
			{
				AddResultToView(result);
			}
		}

		private void AddResultToView(IValidationResult result)
		{
			if (result == null)
				return;

			foreach (var issue in result.Issues)
			{
				string objectName = issue.Object == null ? "" : issue.Object.DisplayName;
				gridResultsView.Rows.Add(imageList1.Images[LevelToImageIndex(issue.ErrorLevel)], issue.Description, objectName);
				var rowIndex = gridResultsView.Rows.Count - 1;
				var row = gridResultsView.Rows[rowIndex];

				row.Tag = issue;
			}
		}

		private static int LevelToImageIndex(ValidationErrorLevel level)
		{
			switch (level)
			{
				case ValidationErrorLevel.Warning:
					return 2;
				case ValidationErrorLevel.Error:
					return 1;
				case ValidationErrorLevel.Fatal:
					return 0;
				default:
					throw new ArgumentOutOfRangeException("level");
			}
		}

		public void ClearResultsView()
		{
			gridResultsView.Rows.Clear();
		}

		private void gridResultsView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= gridResultsView.Rows.Count)
				return;

			var row = gridResultsView.Rows[e.RowIndex];
			var issue = (ValidationIssue)row.Tag;
			NavigateToObject.RaiseEvent(this, new GenericEventArgs<IModelObject>(issue.Object));
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			CloseView.RaiseEvent(this);
		}

		private void gridResultsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var row = gridResultsView.Rows[e.RowIndex];
				var issue = (ValidationIssue)row.Tag;
				NavigateToObject.RaiseEvent(this, new GenericEventArgs<IModelObject>(issue.Object));
			}
		}

		private void buttonReCheck_Click(object sender, EventArgs e)
		{
			if (ReValidateRequested != null)
				ReValidateRequested(null, null);
		}

		private void gridResultsView_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var row = gridResultsView.Rows[e.RowIndex];

				if (row.Tag != null)
				{
					var issue = (ValidationIssue)row.Tag;

					if (issue.Object != null)
						NavigateToObject.RaiseEvent(this, new GenericEventArgs<IModelObject>(issue.Object));
				}
			}
		}
	}
}
