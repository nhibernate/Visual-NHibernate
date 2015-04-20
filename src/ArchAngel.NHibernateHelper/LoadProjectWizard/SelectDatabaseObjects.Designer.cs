namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
    partial class SelectDatabaseObjects
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.buttonFinish = new DevComponents.DotNetBar.ButtonX();
			this.buttonBack = new DevComponents.DotNetBar.ButtonX();
			this.cell1 = new DevComponents.AdvTree.Cell();
			this.cell2 = new DevComponents.AdvTree.Cell();
			this.cell3 = new DevComponents.AdvTree.Cell();
			this.cell4 = new DevComponents.AdvTree.Cell();
			this.cell5 = new DevComponents.AdvTree.Cell();
			this.cell6 = new DevComponents.AdvTree.Cell();
			this.advTree1 = new DevComponents.AdvTree.AdvTree();
			this.node1 = new DevComponents.AdvTree.Node();
			this.node2 = new DevComponents.AdvTree.Node();
			this.node3 = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			((System.ComponentModel.ISupportInitialize)(this.advTree1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonFinish
			// 
			this.buttonFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonFinish.Location = new System.Drawing.Point(394, 455);
			this.buttonFinish.Name = "buttonFinish";
			this.buttonFinish.Size = new System.Drawing.Size(76, 23);
			this.buttonFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonFinish.TabIndex = 48;
			this.buttonFinish.Text = "Next >";
			this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
			// 
			// buttonBack
			// 
			this.buttonBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBack.Location = new System.Drawing.Point(17, 455);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(71, 23);
			this.buttonBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBack.TabIndex = 47;
			this.buttonBack.Text = "< Back";
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// cell1
			// 
			this.cell1.Name = "cell1";
			this.cell1.StyleMouseOver = null;
			// 
			// cell2
			// 
			this.cell2.Name = "cell2";
			this.cell2.StyleMouseOver = null;
			// 
			// cell3
			// 
			this.cell3.Name = "cell3";
			this.cell3.StyleMouseOver = null;
			this.cell3.Text = "cell3";
			// 
			// cell4
			// 
			this.cell4.Name = "cell4";
			this.cell4.StyleMouseOver = null;
			this.cell4.Text = "cell4";
			// 
			// cell5
			// 
			this.cell5.Name = "cell5";
			this.cell5.StyleMouseOver = null;
			this.cell5.Text = "clee5";
			// 
			// cell6
			// 
			this.cell6.Name = "cell6";
			this.cell6.StyleMouseOver = null;
			this.cell6.Text = "cell6";
			// 
			// advTree1
			// 
			this.advTree1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.advTree1.AllowDrop = true;
			this.advTree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.advTree1.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
			this.advTree1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.advTree1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.advTree1.Location = new System.Drawing.Point(17, 38);
			this.advTree1.MultiSelect = true;
			this.advTree1.Name = "advTree1";
			this.advTree1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1,
            this.node2,
            this.node3});
			this.advTree1.NodesConnector = this.nodeConnector1;
			this.advTree1.NodeStyle = this.elementStyle1;
			this.advTree1.PathSeparator = ";";
			this.advTree1.Size = new System.Drawing.Size(453, 399);
			this.advTree1.Styles.Add(this.elementStyle1);
			this.advTree1.Styles.Add(this.elementStyle2);
			this.advTree1.TabIndex = 49;
			this.advTree1.Text = "advTree1";
			this.advTree1.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.advTree1_AfterCheck);
			this.advTree1.BeforeNodeSelect += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.advTree1_BeforeNodeSelect);
			this.advTree1.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.advTree1_AfterNodeSelect);
			// 
			// node1
			// 
			this.node1.Expanded = true;
			this.node1.Name = "node1";
			this.node1.Text = "node1";
			// 
			// node2
			// 
			this.node2.Expanded = true;
			this.node2.Name = "node2";
			this.node2.Text = "node2";
			// 
			// node3
			// 
			this.node3.Expanded = true;
			this.node3.Name = "node3";
			this.node3.Text = "node3";
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Class = "";
			this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle2
			// 
			this.elementStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
			this.elementStyle2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
			this.elementStyle2.BackColorGradientAngle = 90;
			this.elementStyle2.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderBottomWidth = 1;
			this.elementStyle2.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle2.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderLeftWidth = 1;
			this.elementStyle2.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderRightWidth = 1;
			this.elementStyle2.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderTopWidth = 1;
			this.elementStyle2.Class = "";
			this.elementStyle2.CornerDiameter = 4;
			this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle2.Description = "Blue";
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.PaddingBottom = 1;
			this.elementStyle2.PaddingLeft = 1;
			this.elementStyle2.PaddingRight = 1;
			this.elementStyle2.PaddingTop = 1;
			this.elementStyle2.TextColor = System.Drawing.Color.Black;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(17, 9);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(91, 23);
			this.labelX1.TabIndex = 51;
			this.labelX1.Text = "Select tables:";
			// 
			// SelectDatabaseObjects
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.advTree1);
			this.Controls.Add(this.buttonFinish);
			this.Controls.Add(this.buttonBack);
			this.Name = "SelectDatabaseObjects";
			this.Size = new System.Drawing.Size(486, 491);
			((System.ComponentModel.ISupportInitialize)(this.advTree1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonFinish;
        private DevComponents.DotNetBar.ButtonX buttonBack;
        private DevComponents.AdvTree.Cell cell1;
        private DevComponents.AdvTree.Cell cell2;
        private DevComponents.AdvTree.Cell cell3;
        private DevComponents.AdvTree.Cell cell4;
        private DevComponents.AdvTree.Cell cell5;
        private DevComponents.AdvTree.Cell cell6;
        private DevComponents.AdvTree.AdvTree advTree1;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.AdvTree.Node node3;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}
