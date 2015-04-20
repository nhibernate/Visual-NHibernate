using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    public partial class ComponentDetailsEditor : UserControl
    {
        private ComponentSpecification _ComponentSpecification;
        private DetailsEditorHelper Helper;

        public ComponentDetailsEditor()
        {
            InitializeComponent();

            BackColor = Color.Black;
            ForeColor = Color.White;
            Helper = new DetailsEditorHelper(this, BackColor, ForeColor, superTooltip1);
            virtualPropertyGrid1.BackColor = BackColor;
        }

        public ComponentSpecification ComponentSpecification
        {
            get { return _ComponentSpecification; }
            set
            {
                _ComponentSpecification = value;
                Populate();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                superTooltip1.SetSuperTooltip(this.Controls[i], null);
            }
            this.Controls.Clear();
            Helper.Clear();
        }

        private void Populate()
        {
            if (ComponentSpecification == null)
                return;

            Clear();
            int maxLabelWidth = 0;

            LabelX label = new LabelX();
            Graphics g = Graphics.FromHwnd(label.Handle);
            maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString("Name", label.Font).Width));

            for (int i = 0; i < ComponentSpecification.Ex.Count; i++)
            {
                maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString(ComponentSpecification.Ex[i].Name, label.Font).Width));
            }
            //maxLabelWidth += 5;
            int top = 5;
            TextBox tb = new TextBox();
            //tb.BackColor = this.BackColor;
            //tb.ForeColor = this.ForeColor;
            tb.Top = top;
            tb.Left = maxLabelWidth + 5 + 5;
            tb.Text = ComponentSpecification.Name;
            this.Controls.Add(tb);
            tb.TextChanged += new EventHandler(tb_TextChanged);
            top = Helper.AddLabel("Name", top, maxLabelWidth);

            this.Controls.Add(virtualPropertyGrid1);
            virtualPropertyGrid1.BackColor = this.BackColor;
            virtualPropertyGrid1.ForeColor = this.ForeColor;
            virtualPropertyGrid1.Top = top;
            virtualPropertyGrid1.Left = 0;
            virtualPropertyGrid1.Width = Width;
            virtualPropertyGrid1.Height = Height - virtualPropertyGrid1.Top;
            virtualPropertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;// | AnchorStyles.Bottom;
            virtualPropertyGrid1.SetVirtualProperties(ComponentSpecification.Ex);

            this.Height = virtualPropertyGrid1.Bottom + 50;
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            ComponentSpecification.Name = ((TextBox)sender).Text;
        }
    }
}
