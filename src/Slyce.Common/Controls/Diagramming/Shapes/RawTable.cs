using System.Drawing;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class RawTable : RawShape
	{
		public RawTable(ShapeCanvas canvas, string name, Font font, object tag)
			: base(canvas, name, font)
		{
			RoundedCorners = false;
			//BackColor1 = Color.Blue;
			//BackColor2 = Color.DarkBlue;
			//ForeColor = Color.White; 
			SetColors();
			Tag = tag;
		}

		public RawTable(ShapeCanvas canvas, string name, Font font, Font categoryFont, Font propertyFont, object tag)
			: base(canvas, name, font, categoryFont, propertyFont)
		{
			RoundedCorners = false;
			//BackColor1 = Color.Blue;
			//BackColor2 = Color.DarkBlue;
			//ForeColor = Color.White;
			SetColors();
			Tag = tag;
		}

		private void SetColors()
		{
			BackColor1 = Color.Green;
			BackColor2 = Color.YellowGreen;
			BorderColor = Color.YellowGreen;
			ForeColor = Color.White;

			FocusBackColor1 = Color.Green;
			FocusBackColor2 = Color.Green;
			FocusForeColor = Color.White;
			FocusBorderColor = Color.YellowGreen;
		}

	}
}
