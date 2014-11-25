using System.Drawing;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public class RawEntity : RawShape
	{
		public RawEntity(ShapeCanvas canvas, string name, Font font, object tag)
			: base(canvas, name, font)
		{
			RoundedCorners = true;
			//BackColor1 = Color.FromArgb(130, 130, 130);
			//BackColor2 = Color.FromArgb(40, 40, 40);
			//ForeColor = Color.White;
			SetColors();
			Tag = tag;
		}

		public RawEntity(ShapeCanvas canvas, string name, Font font, Font categoryFont, Font propertyFont, object tag)
			: base(canvas, name, font, categoryFont, propertyFont)
		{
			RoundedCorners = true;
			//BackColor1 = Color.FromArgb(130, 130, 130);
			//BackColor2 = Color.FromArgb(40, 40, 40);
			//ForeColor = Color.White;
			SetColors();
			Tag = tag;
		}

		private void SetColors()
		{
			BackColor1 = Color.Orange;
			BackColor2 = Color.Yellow;//.FromArgb(255, 223, 0); // Color.Yellow;
			BorderColor = Color.Orange;
			ForeColor = Color.Black;//.White;

			FocusBackColor1 = Color.Orange;
			FocusBackColor2 = Color.Orange;
			FocusBorderColor = Color.Orange;
			FocusForeColor = Color.Black;//.White;
		}

	}
}
