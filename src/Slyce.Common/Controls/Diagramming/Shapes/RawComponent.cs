using System.Drawing;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class RawComponent : RawShape
	{
		public RawComponent(ShapeCanvas canvas, string name, Font font, object tag)
			: base(canvas, name, font)
		{
			RoundedCorners = true;
			//BackColor1 = Color.Green;
			//BackColor2 = Color.YellowGreen;
			//ForeColor = Color.White;
			SetColors();
			Tag = tag;
		}

		public RawComponent(ShapeCanvas canvas, string name, Font font, Font categoryFont, Font propertyFont, object tag)
			: base(canvas, name, font, categoryFont, propertyFont)
		{
			RoundedCorners = true;
			//BackColor1 = Color.Green;
			//BackColor2 = Color.YellowGreen;
			//ForeColor = Color.White;
			SetColors();
			Tag = tag;
		}

		private void SetColors()
		{
			BackColor1 = Color.FromArgb(15, 113, 168);//.Blue;
			BackColor2 = Color.FromArgb(123, 182, 212);//.LightBlue;
			BorderColor = Color.FromArgb(15, 113, 168);//
			ForeColor = Color.White;

			FocusBackColor1 = Color.FromArgb(15, 113, 168);
			FocusBackColor2 = Color.FromArgb(15, 113, 168);
			FocusForeColor = Color.White;
			FocusBorderColor = Color.FromArgb(15, 113, 168);//
		}

	}
}
