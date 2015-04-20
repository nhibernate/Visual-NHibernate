using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class DataGridViewComboBoxColumnEx : DataGridViewComboBoxColumn
	{
		private Color _InvalidColor = Color.Orange;
		private Color _DisabledColor = Color.Gray;
		private Color _BackColor = Color.Black;

		public DataGridViewComboBoxColumnEx()
		{
			this.CellTemplate = new DataGridViewComboBoxCellEx();
		}

		public Color BackColor
		{
			get { return _BackColor; }
			set
			{
				if (_BackColor != value)
				{
					_BackColor = value;
					//((DataGridViewComboBoxCellEx)CellTemplate).InvalidColor = value;
					DataGridViewComboBoxCellEx.BackColor = value;
				}
			}
		}

		public Color InvalidColor
		{
			get { return _InvalidColor; }
			set
			{
				if (_InvalidColor != value)
				{
					_InvalidColor = value;
					//((DataGridViewComboBoxCellEx)CellTemplate).InvalidColor = value;
					DataGridViewComboBoxCellEx.InvalidColor = value;
				}
			}
		}

		public Color DisabledColor
		{
			get { return _DisabledColor; }
			set
			{
				if (_DisabledColor != value)
				{
					_DisabledColor = value;
					//((DataGridViewComboBoxCellEx)CellTemplate).DisabledColor = value;
					DataGridViewComboBoxCellEx.DisabledColor = value;
				}
			}
		}
	}
}
