using System.Collections.Generic;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class SlyceTreeGridItem
	{
		List<SlyceTreeGridCellItem> _SubItems = new List<SlyceTreeGridCellItem>();
		public object Tag = null;

		public List<SlyceTreeGridCellItem> SubItems
		{
			get { return _SubItems; }
		}
	}
}
