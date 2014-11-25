using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class DataGridViewTextBoxCellEx : DataGridViewTextBoxCell
	{
		private bool _Enabled = true;
		private SolidBrush _InvalidBrush;
		private SolidBrush _InvalidBrushSelected;
		private SolidBrush _DisabledBrush;
		private SolidBrush _DisabledBrushSelected;
		private static Color _InvalidColor = Color.Orange;
		private static Color _DisabledColor = Color.Gray;
		private static Color _BackColor = Color.Black;
		private static Color InvalidColorSelected = Slyce.Common.Colors.ChangeBrightness(Color.Orange, Slyce.Common.Colors.GetBrightness(Color.Orange) + 0.1);
		private static Color DisabledColorSelected = Slyce.Common.Colors.ChangeBrightness(Color.Gray, Slyce.Common.Colors.GetBrightness(Color.Gray) + 0.1);
		private static Brush BackBrush = new SolidBrush(Color.Black);

		public DataGridViewTextBoxCellEx()
		{
		}

		public static Color BackColor
		{
			get { return _BackColor; }
			set
			{
				if (_BackColor != value)
				{
					_BackColor = value;
					BackBrush = new SolidBrush(value);
				}
			}
		}

		public static Color InvalidColor
		{
			get { return _InvalidColor; }
			set
			{
				if (_InvalidColor != value)
				{
					_InvalidColor = value;

					double brightness = Slyce.Common.Colors.GetBrightness(value) - 0.1;

					if (brightness < 0)
						brightness = 0.2;

					InvalidColorSelected = Slyce.Common.Colors.ChangeBrightness(value, brightness);
				}
			}
		}

		public static Color DisabledColor
		{
			get { return _DisabledColor; }
			set
			{
				if (_DisabledColor != value)
				{
					_DisabledColor = value;

					double brightness = Slyce.Common.Colors.GetBrightness(value) + 0.05;

					if (brightness > 1)
						brightness = 0.9;

					DisabledColorSelected = Slyce.Common.Colors.ChangeBrightness(value, brightness);
				}
			}
		}

		public bool Enabled
		{
			get { return _Enabled; }
			set
			{
				if (_Enabled != value)
				{
					_Enabled = value;
					ReadOnly = !value;
				}
			}
		}

		private bool HasValue
		{
			get
			{
				if (RowIndex < 0)
					return false;

				return this.Value != null && !string.IsNullOrEmpty(this.Value.ToString());
			}
		}

		private SolidBrush InvalidBrush
		{
			get
			{
				if (Selected)
				{
					if (_InvalidBrushSelected == null ||
						_InvalidBrushSelected.Color != InvalidColorSelected)
					{
						_InvalidBrushSelected = new SolidBrush(InvalidColorSelected);
					}
					return _InvalidBrushSelected;
				}
				else
				{
					if (_InvalidBrush == null ||
						_InvalidBrush.Color != InvalidColor)
					{
						_InvalidBrush = new SolidBrush(InvalidColor);
					}
					return _InvalidBrush;
				}
			}
		}

		private SolidBrush DisabledBrush
		{
			get
			{
				if (Selected)
				{
					if (_DisabledBrushSelected == null ||
						_DisabledBrushSelected.Color != DisabledColorSelected)
					{
						_DisabledBrushSelected = new SolidBrush(DisabledColorSelected);
					}
					return _DisabledBrushSelected;
				}
				else
				{
					if (_DisabledBrush == null ||
						_DisabledBrush.Color != DisabledColor)
					{
						_DisabledBrush = new SolidBrush(DisabledColor);
					}
					return _DisabledBrush;
				}
			}
		}

		// Override the Clone method so that the Enabled property is copied.
		public override object Clone()
		{
			DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)base.Clone();
			return cell;
		}

		protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (!Enabled && !this.OwningRow.IsNewRow)
			{
				//graphics.FillRectangle(DisabledBrush, cellBounds);


				//// Draw the cell borders, if specified.
				//if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
				//{
				//    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
				//}
				//base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
				//graphics.FillEllipse(DisabledBrush, new Rectangle(cellBounds.X + cellBounds.Width / 2 - 2, cellBounds.Y + cellBounds.Height / 2 - 2, 4, 4));
				graphics.FillRectangle(DisabledBrush, cellBounds);

				if (Selected)
				{
					//graphics.FillRectangle(DisabledBrush, cellBounds);
					//graphics.FillRectangle(BackBrush, new Rectangle(cellBounds.X + cellBounds.Width / 2 - 2, cellBounds.Y + cellBounds.Height / 2 - 2, 4, 4));
					graphics.DrawImage(SlyceGrid.NoEntryImage, new Rectangle(cellBounds.X + cellBounds.Width / 2 - 8, cellBounds.Y + cellBounds.Height / 2 - 8, 16, 16));
				}
				//else
				//{
				//    graphics.FillRectangle(BackBrush, cellBounds);
				//    graphics.FillRectangle(DisabledBrush, new Rectangle(cellBounds.X + cellBounds.Width / 2 - 2, cellBounds.Y + cellBounds.Height / 2 - 2, 4, 4));
				//}
				// Draw the cell borders, if specified.
				if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
				{
					PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
				}
			}
			else if (rowIndex < 0)
			{
				// Do nothing
			}
			else if (!HasValue && !this.OwningRow.IsNewRow)
			{
				graphics.FillRectangle(BackBrush, cellBounds);

				// Draw the cell borders, if specified.
				if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
				{
					PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
				}
			}
			else
				base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

			//if (!Enabled)
			//    graphics.FillEllipse(DotBrush, new Rectangle(cellBounds.X + cellBounds.Width / 2 - 2, cellBounds.Y + cellBounds.Height / 2 - 2, 2, 2));
		}

	}
}
