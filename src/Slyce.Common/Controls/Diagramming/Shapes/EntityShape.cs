using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
    public partial class EntityShape : Shape
    {
        private bool _IsEmpty = false;
        private string _Text;

        public EntityShape(string name)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            _Text = name;
            labelX1.Text = name;
            SetStyle();
        }

        public EntityShape(string name, bool isEmpty)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            _Text = name;
            labelX1.Text = name;
            IsEmpty = isEmpty;
            SetStyle();
        }

        private void SetStyle()
        {
            if (IsEmpty)
            {
                labelX1.BackColor = Color.FromArgb(253, 253, 253);
                labelX1.BackgroundStyle.BackColor = labelX1.BackColor;
                labelX1.BackgroundStyle.BackColor2 = labelX1.BackColor;
                labelX1.BackgroundStyle.BorderColor = Color.DarkGray;
            }
            else
            {
                labelX1.BackgroundStyle.BackColor = Color.Blue;
                labelX1.BackgroundStyle.BackColor2 = Color.AliceBlue;
                labelX1.BackgroundStyle.BorderColor = labelX1.BackgroundStyle.BackColor;
            }
            
            labelX1.BackgroundStyle.BackColorGradientAngle = 90;
            labelX1.BackgroundStyle.Border = DevComponents.DotNetBar.eStyleBorderType.Solid;
            labelX1.BackgroundStyle.BorderWidth = 1;
            
            labelX1.BackgroundStyle.CornerDiameter = 3;
            labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            labelX1.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            labelX1.ForeColor = Color.White;
            labelX1.TextAlignment = StringAlignment.Center;
            labelX1.TextLineAlignment = StringAlignment.Near;
        }

        public bool IsEmpty
        {
            get { return _IsEmpty; }
            set
            {
                if (_IsEmpty != value)
                {
                    _IsEmpty = value;
                    SetStyle();
                    labelX1.Text = _IsEmpty ? string.Format(@"<span align=""center""><a>{0}</a></span>", _Text) : _Text;
                }
            }
        }
    }
}
