using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Slyce.Common;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public class FormCollectionAdapter<T> : UserControl, ICollectionForm<T> where T : class
	{
		private readonly FormItemCollection internalForm;
		private readonly Func<T, string> displayAliasFunction;
		private List<T> items;

		public FormCollectionAdapter(Func<T, string> displayNameFunction)
		{
			this.displayAliasFunction = displayNameFunction;
			internalForm = new FormItemCollection();

			Initialise();
		}

		private void Initialise()
		{
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(internalForm);
			Name = "FormItemCollection";
			Size = new System.Drawing.Size(457, 440);
			internalForm.ResumeLayout(false);
			ResumeLayout(false);
		}

		public void Clear() { }

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		public event EventHandler SelectedItemChanged
		{
			add { internalForm.SelectedItemChanged += value; }
			remove { internalForm.SelectedItemChanged -= value; }
		}
		public event EventHandler AddItem
		{
			add { internalForm.AddItem += value; }
			remove { internalForm.AddItem -= value; }
		}
		public event EventHandler RemoveItem
		{
			add { internalForm.RemoveItem += value; }
			remove { internalForm.RemoveItem -= value; }
		}

		public T SelectedItem
		{
			get
			{
				return internalForm.SelectedIndex > -1 ? items[internalForm.SelectedIndex] : null;
			}
			set
			{
				internalForm.SelectedIndex = items.IndexOf(value);
			}
		}

		public void SetItemTypeName(string name)
		{
			if(internalForm != null)
				internalForm.SetItemName(name);
		}

		public IEnumerable<T> Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value.ToList();

				// Uses the function we were given to convert each item to
				// a display string.
				internalForm.Items = items.Select(displayAliasFunction);
			}
		}
	}

	public interface ICollectionForm<T> : IEditorForm
	{
		event EventHandler SelectedItemChanged;
		event EventHandler AddItem;
		event EventHandler RemoveItem;
        IEnumerable<T> Items { get; set; }
		T SelectedItem { get; set; }
		void SetItemTypeName(string name);
	}
}
