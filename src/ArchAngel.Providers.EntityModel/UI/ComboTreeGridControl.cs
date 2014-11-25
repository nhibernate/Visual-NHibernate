using System;
using System.ComponentModel;
using System.Windows.Forms;
using log4net;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI
{
	/// <summary>
	/// This class assumes a bunch of things. 
	/// * ComboTrees will be added to it.
	/// * The user of this control won't rearrange the controls after they have
	/// been added to this one.
	/// * The nodes in the ComboTrees will have the item they represent in their tags.
	/// </summary>
	public partial class ComboTreeGridControl : UserControl
	{
		public enum Side { Left, Right }

		private static readonly ILog log = LogManager.GetLogger(typeof (ComboTreeGridControl));

		private const int ControlHeight = 25;
		private const int RowGap = 5;
		private const int SidePadding = 5;

		public bool UseButtonPanel = true;

		public ComboTreeGridControl()
		{
			InitializeComponent();

			panelLeftColumns.Scroll += panelLeftColumns_Scroll;
			panelRightColumns.Scroll += panelRightColumns_Scroll;
		}

		private bool codeScrolling;

		void panelRightColumns_Scroll(object sender, ScrollEventArgs e)
		{
			if (codeScrolling) return;
			
			codeScrolling = true;
			panelLeftColumns.VerticalScroll.Value = panelRightColumns.VerticalScroll.Value;
			codeScrolling = false;
		}

		void panelLeftColumns_Scroll(object sender, ScrollEventArgs e)
		{
			if (codeScrolling) return;

			codeScrolling = true;
			panelRightColumns.VerticalScroll.Value = panelLeftColumns.VerticalScroll.Value;
			codeScrolling = false;
		}

		public ComboTreeGridControl(string leftTitleText, string rightTitleText) : this()
		{
			headerLeft.Text = leftTitleText;
			headerRight.Text = rightTitleText;
		}

		[EditorBrowsable]
		public string LeftTitle
		{
			get { return headerLeft.Text; }
			set { headerLeft.Text = value;}
		}

		[EditorBrowsable]
		public string RightTitle
		{
			get { return headerRight.Text; }
			set { headerRight.Text = value; }
		}

		private static int CalculateUsedSpace(int rowCount)
		{
			return (ControlHeight + RowGap) * rowCount + RowGap;
		}

		public void AddRow(Control control1, Control control2)
		{
			UseButtonPanel = false;
			AddRow(control1, control2, null);
		}

		public void AddRow(Control control1, Control control2, Button removeButton)
		{
			int currentStartingHeight = CalculateUsedSpace(ItemCount);
			
			panelLeftColumns.Controls.Add(control1);
			panelRightColumns.Controls.Add(control2);
			panelButtons.Controls.Add(removeButton);

			if(UseButtonPanel)
				AddRemoveButton(removeButton, currentStartingHeight);

			control1.Top = control2.Top = currentStartingHeight;
			control1.Left = control2.Left = SidePadding;
			control1.Height = control2.Height = ControlHeight;

			ResizeControlWidths(control1, control2, removeButton);

			control1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			control2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
		}

		private void AddRemoveButton(Button removeButton, int currentStartingHeight)
		{
			removeButton.Top = currentStartingHeight + splitContainer2.Height;
			removeButton.Left = SidePadding;
			removeButton.Height = ControlHeight;
		}

		private void ResizeControlWidths(Control control1, Control control2, Control removeButton)
		{
			control1.Width = panelLeftColumns.ClientSize.Width - (2 * SidePadding);
			control2.Width = panelRightColumns.ClientSize.Width - (2 * SidePadding);
			if(UseButtonPanel)
				removeButton.Width = panelButtons.ClientSize.Width - (2 * SidePadding);
		}

		public Button GetButtonFrom(int row)
		{
			if(UseButtonPanel)
				return null;

			return (Button)panelButtons.Controls[row];
		}

		public T GetControlFrom<T>(Side side, int rowIndex) where T : Control
		{
			T box;
			if (side == Side.Left)
				box = panelLeftColumns.Controls[rowIndex] as T;
			else
				box = panelRightColumns.Controls[rowIndex] as T;
			return box;
		}

		public Pair<T> GetControlFrom<T>(int rowIndex) where T : Control
		{
			return new Pair<T>(GetControlFrom<T>(Side.Left, rowIndex), GetControlFrom<T>(Side.Right, rowIndex));
		}

		public void Clear()
		{
			panelLeftColumns.Controls.Clear();
			panelRightColumns.Controls.Clear();
            panelButtons.Controls.Clear();
		}

		public int ItemCount
		{
			get
			{
				return panelLeftColumns.Controls.Count; 
			}
		}

		public int IndexOf(Button button)
		{
			if(UseButtonPanel == false)
				throw new InvalidOperationException("Not using button panel");

			for (int i = 0; i < ItemCount; i++)
			{
				if (panelButtons.Controls[i] == button) return i;
			}

			throw new ArgumentException("Button not part of this Grid.");
		}

		public int IndexOf(Control box)
		{
			for (int i = 0; i < ItemCount; i++ )
			{
				if (panelLeftColumns.Controls[i]	== box) return i;
				if (panelRightColumns.Controls[i]	== box) return i;
			}

			throw new ArgumentException("Control not part of this Grid.");
		}

		public void RemoveRow(int index)
		{
			panelLeftColumns.Controls.RemoveAt(index);
			panelRightColumns.Controls.RemoveAt(index);
			
			if(UseButtonPanel)
				panelButtons.Controls.RemoveAt(index);

			for(int i = index; i < ItemCount; i++)
			{
				panelLeftColumns.Controls[i].Top -= (ControlHeight + RowGap);
				panelRightColumns.Controls[i].Top -= (ControlHeight + RowGap);
				if(UseButtonPanel)
					panelButtons.Controls[i].Top -= (ControlHeight + RowGap);
			}
		}
	}
}
