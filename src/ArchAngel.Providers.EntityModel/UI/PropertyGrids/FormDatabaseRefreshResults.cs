using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormDatabaseRefreshResults : UserControl, IDetailedDBMergeResultForm, ISimpleDBMergeResultForm
	{
		private readonly Dictionary<string, IMergeOperation<ITable>> addedTableOperations;
		private readonly Dictionary<string, IMergeOperation<ITable>> removedTableOperations;
		public event EventHandler ChangesAccepted;
		public event EventHandler ChangesCancelled;

		public FormDatabaseRefreshResults()
		{
			InitializeComponent();
			addedTableOperations = new Dictionary<string, IMergeOperation<ITable>>();
			removedTableOperations = new Dictionary<string, IMergeOperation<ITable>>();
		}

		public IEnumerable<IMergeOperation<ITable>> AddedTableOperations
		{
			get { return addedTableOperations.Values; }
			set 
			{
				addedTableOperations.Clear();
				foreach (var operation in value)
				{
					addedTableOperations[operation.DisplayName] = operation;
				}

				listBoxAdded.Items.Clear();
				listBoxAdded.Items.AddRange(addedTableOperations.Keys.ToArray());
			}
		}

		public IEnumerable<IMergeOperation<ITable>> RemovedTableOperations
		{
			get { return removedTableOperations.Values; }
			set
			{
				removedTableOperations.Clear();
				foreach(var operation in value)
				{
					removedTableOperations[operation.DisplayName] = operation;
				}
				
				listBoxRemoved.Items.Clear();
				listBoxRemoved.Items.AddRange(removedTableOperations.Keys.ToArray());
			}
		}

		public IEnumerable<IMergeOperation<ITable>> SelectedAddedTableOperations
		{
			get
			{
				foreach (string tableName in listBoxAdded.CheckedItems)
				{
					yield return addedTableOperations[tableName];
				}
			}
		}

		public IEnumerable<IMergeOperation<ITable>> SelectedRemovedTableOperations
		{
			get
			{
				foreach (string tableName in listBoxRemoved.CheckedItems)
				{
					yield return removedTableOperations[tableName];
				}
			}
		}
	    
		public string TextResults
		{
			get { return textBoxTextResults.Text; }
			set { textBoxTextResults.Text = value; }
		}

		public string HtmlResults
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public string XmlResults
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public event EventHandler GetTextResults;
		public event EventHandler GetHtmlResults;
		public event EventHandler GetXmlResults;

		private void tabControl1_SelectedTabChanging(object sender, DevComponents.DotNetBar.TabStripTabChangingEventArgs e)
		{
			GetTextResults.RaiseEvent(this);
		}

		private void buttonAcceptChanges_Click(object sender, EventArgs e)
		{
			ChangesAccepted.RaiseEvent(this);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			ChangesCancelled.RaiseEvent(this);
		}
	}

	public interface ISimpleDBMergeResultForm
	{
		IEnumerable<IMergeOperation<ITable>> AddedTableOperations { get; set; }
		IEnumerable<IMergeOperation<ITable>> RemovedTableOperations { get; set; }
		IEnumerable<IMergeOperation<ITable>> SelectedAddedTableOperations { get; }
		IEnumerable<IMergeOperation<ITable>> SelectedRemovedTableOperations { get; }
		event EventHandler ChangesCancelled;
		event EventHandler ChangesAccepted;
	}

	public interface IDetailedDBMergeResultForm
	{
		string TextResults { get; set; }
		string HtmlResults { get; set; }
		string XmlResults { get; set; }
		event EventHandler GetTextResults;
		event EventHandler GetHtmlResults;
		event EventHandler GetXmlResults;
	}
}
