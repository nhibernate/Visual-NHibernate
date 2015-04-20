using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class ColumnItem
	{
		public enum ColumnTypes
		{
			Textbox,
			Checkbox,
			ComboBox,
			NullableCheckBox,
			NullableTextBox,
			Image,
			NullableIntegerInput,
			Link,
			None,
			IntegerInput
		}
		public enum ColorSchemes
		{
			Blue,
			Green,
			Yellow,
			Red,
			Orange,
			Custom,
			Black
		}
		public event System.Windows.Forms.MouseEventHandler Clicked;
		private bool _IsNullable = false;
		private ColumnTypes _ControlType;
		public Dictionary<string, object> ComboItems = new Dictionary<string, object>();
		public object DefaultValue = null;
		public string CategoryName;
		public bool ReadOnly = false;
		public bool IsLink = false;
		public object Tag;
		public Color CategoryBackColor = Color.GreenYellow;
		public Color CategoryBackColor2 = Color.Green;
		public Color CategoryForeColor = Color.White;
		ColorSchemes _ColorScheme = ColorSchemes.Blue;

		public ColumnItem(string text, ColumnTypes controlType, object defaultValue, string categoryName)
		{
			Text = text;
			ControlType = controlType;
			DefaultValue = defaultValue;
			CategoryName = categoryName;
		}

		public ColorSchemes ColorScheme
		{
			get { return _ColorScheme; }
			set
			{
				_ColorScheme = value;

				switch (value)
				{
					case ColorSchemes.Blue:
						CategoryBackColor = Color.FromArgb(82, 128, 208);
						CategoryBackColor2 = Color.Navy;
						CategoryForeColor = Color.White;
						break;
					case ColorSchemes.Custom:
						break;
					case ColorSchemes.Green:
						CategoryBackColor = Color.GreenYellow;
						CategoryBackColor2 = Color.Green;
						CategoryForeColor = Color.White;
						break;
					case ColorSchemes.Orange:
						CategoryBackColor = Color.FromArgb(253, 222, 79);
						CategoryBackColor2 = Color.FromArgb(204, 102, 0);
						CategoryForeColor = Color.White;
						break;
					case ColorSchemes.Red:
						CategoryBackColor = Color.FromArgb(244, 179, 179);
						CategoryBackColor2 = Color.FromArgb(153, 0, 0);
						CategoryForeColor = Color.White;
						break;
					case ColorSchemes.Yellow:
						CategoryBackColor = Color.White;
						CategoryBackColor2 = Color.FromArgb(249, 211, 17);
						CategoryForeColor = Color.Black;
						break;
					case ColorSchemes.Black:
						CategoryBackColor = Color.Black;
						CategoryBackColor2 = Color.Black;
						CategoryForeColor = Color.White;
						break;
				}
			}
		}

		public string Text { get; set; }

		public Image Image { get; set; }

		public ColumnTypes ControlType
		{
			get { return _ControlType; }
			set
			{
				_ControlType = value;

				IsNullable = _ControlType == ColumnTypes.NullableCheckBox || _ControlType == ColumnTypes.NullableTextBox || _ControlType == ColumnTypes.NullableIntegerInput;
			}
		}

		public bool IsNullable { get; set; }

		internal void RaiseClick(MouseEventArgs e)
		{
			if (Clicked != null)
				Clicked(this, e);
		}
	}
}
