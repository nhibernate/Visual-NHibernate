using System;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class DataGridViewGFH : DataGridView
	{
		public double HorizontalScrollingOffsetValue
		{
			get
			{
				double scrollRatio = Convert.ToDouble(HorizontalScrollBar.Value) / HorizontalScrollBar.Maximum;
				return scrollRatio;
			}
		}

		public bool VerticalScrollbarVisible
		{
			get { return VerticalScrollBar.Visible; }
		}

		public int VerticalScrollbarWidth
		{
			get { return VerticalScrollBar.Width; }
		}
	}
}
