using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
    public class Shape : UserControl
    {
        public LinkLine OriginatingLineStyle = new LinkLine();

        public Shape()
        {
            //OriginatingLineStyle.Parent = this;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (OriginatingLineStyle.GPath.IsVisible(Cursor.Position.X, Cursor.Position.Y))
            {
                MessageBox.Show("MouseOver");
            }
            base.OnMouseHover(e);
        }
    }
}
