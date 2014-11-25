using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class DataGridViewTextBoxColumnEx : DataGridViewTextBoxColumn
	{
		private Color _InvalidColor = Color.Orange;
		private Color _DisabledColor = Color.Gray;
		private static Color _BackColor = Color.Black;
		private DataGridViewContentAlignment _Alignment = DataGridViewContentAlignment.MiddleLeft;

		public DataGridViewTextBoxColumnEx()
		{
			this.CellTemplate = new DataGridViewTextBoxCellEx();
		}

		public DataGridViewContentAlignment Alignment
		{
			get
			{
				return _Alignment;
			}
			set
			{
				if (_Alignment != value)
				{
					_Alignment = value;
					this.CellTemplate.Style.Alignment = value;
				}
			}
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
					DataGridViewTextBoxCellEx.BackColor = value;
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
					//((DataGridViewTextBoxCellEx)CellTemplate).InvalidColor = value;
					DataGridViewTextBoxCellEx.InvalidColor = value;
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
					//((DataGridViewTextBoxCellEx)CellTemplate).DisabledColor = value;
					DataGridViewTextBoxCellEx.DisabledColor = value;
				}
			}
		}
	}
}
