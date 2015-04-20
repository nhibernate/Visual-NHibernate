using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormItemCollection : UserControl, IEventSender
    {
		private string itemName;

		public bool EventRaisingDisabled { get; set; }
		public event EventHandler SelectedItemChanged;
		public event EventHandler AddItem;
		public event EventHandler RemoveItem;

		public FormItemCollection()
		{
			InitializeComponent();

			buttonNewColumn.Click += (sender, e) => AddItem.RaiseEventEx(this);
			addColumnToolStripMenuItem.Click += (sender, e) => AddItem.RaiseEventEx(this);
			removeColumnToolStripMenuItem.Click += (sender, e) => RemoveItem.RaiseEventEx(this);
			listBoxItems.SelectedIndexChanged += (sender, e) => SelectedItemChanged.RaiseEventEx(this);
		}

		public void SetItemName(string name)
		{
			itemName = name;

			panelEx2.Text = ReplaceItem(panelEx2.Text);
			ribbonBar1.Text = ReplaceItem(ribbonBar1.Text);
			buttonNewColumn.Text = ReplaceItem(buttonNewColumn.Text);
			addColumnToolStripMenuItem.Text = ReplaceItem(addColumnToolStripMenuItem.Text);
			removeColumnToolStripMenuItem.Text = ReplaceItem(removeColumnToolStripMenuItem.Text);
		}

		private string ReplaceItem(string text)
		{
			return text.Replace("{Item}", itemName);
		}

		public int SelectedIndex
		{
			get
			{
				return listBoxItems.SelectedIndex;
			}
			set
			{
				listBoxItems.SelectedIndex = value;
			}
		}

		public IEnumerable<string> Items
		{
			get
			{
				return listBoxItems.Items.Cast<string>();
			}
			set
			{
				listBoxItems.Items.Clear();
				foreach (var item in value)
					listBoxItems.Items.Add(item);
			}
		}

		private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			removeColumnToolStripMenuItem.Enabled = listBoxItems.SelectedIndex >= 0;
		}
    }
}
