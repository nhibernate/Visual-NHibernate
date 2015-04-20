namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    partial class EntityDetailsEditor
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
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.comboBoxIdGenerator = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.textBoxGeneratorParam1 = new System.Windows.Forms.TextBox();
			this.labelGeneratorParam1 = new DevComponents.DotNetBar.LabelX();
			this.textBoxGeneratorParam2 = new System.Windows.Forms.TextBox();
			this.labelGeneratorParam2 = new DevComponents.DotNetBar.LabelX();
			this.textBoxGeneratorParam3 = new System.Windows.Forms.TextBox();
			this.labelGeneratorParam3 = new DevComponents.DotNetBar.LabelX();
			this.comboBoxCacheUsage = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboBoxCacheInclude = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelCacheUsage = new DevComponents.DotNetBar.LabelX();
			this.labelCacheInclude = new DevComponents.DotNetBar.LabelX();
			this.labelCacheRegion = new DevComponents.DotNetBar.LabelX();
			this.textBoxCacheRegion = new System.Windows.Forms.TextBox();
			this.groupBoxCache = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.groupPanelIdGenerator = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
			this.checkBoxIsAbstract = new System.Windows.Forms.CheckBox();
			this.groupBoxCache.SuspendLayout();
			this.groupPanelIdGenerator.SuspendLayout();
			this.SuspendLayout();
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(119, 11);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(100, 20);
			this.textBoxName.TabIndex = 3;
			this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
			// 
			// comboBoxIdGenerator
			// 
			this.comboBoxIdGenerator.DisplayMember = "Text";
			this.comboBoxIdGenerator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxIdGenerator.FormattingEnabled = true;
			this.comboBoxIdGenerator.ItemHeight = 14;
			this.comboBoxIdGenerator.Location = new System.Drawing.Point(7, 18);
			this.comboBoxIdGenerator.Name = "comboBoxIdGenerator";
			this.comboBoxIdGenerator.Size = new System.Drawing.Size(121, 20);
			this.comboBoxIdGenerator.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxIdGenerator.TabIndex = 4;
			this.comboBoxIdGenerator.SelectedIndexChanged += new System.EventHandler(this.comboBoxIdGenerator_SelectedIndexChanged);
			this.comboBoxIdGenerator.TextChanged += new System.EventHandler(this.comboBoxIdGenerator_TextChanged);
			// 
			// textBoxGeneratorParam1
			// 
			this.textBoxGeneratorParam1.Location = new System.Drawing.Point(143, 18);
			this.textBoxGeneratorParam1.Name = "textBoxGeneratorParam1";
			this.textBoxGeneratorParam1.Size = new System.Drawing.Size(80, 20);
			this.textBoxGeneratorParam1.TabIndex = 5;
			this.textBoxGeneratorParam1.TextChanged += new System.EventHandler(this.textBoxGeneratorParam1_TextChanged);
			// 
			// labelGeneratorParam1
			// 
			this.labelGeneratorParam1.AutoSize = true;
			// 
			// 
			// 
			this.labelGeneratorParam1.BackgroundStyle.Class = "";
			this.labelGeneratorParam1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelGeneratorParam1.ForeColor = System.Drawing.Color.White;
			this.labelGeneratorParam1.Location = new System.Drawing.Point(143, 3);
			this.labelGeneratorParam1.Name = "labelGeneratorParam1";
			this.labelGeneratorParam1.Size = new System.Drawing.Size(44, 15);
			this.labelGeneratorParam1.TabIndex = 6;
			this.labelGeneratorParam1.Text = "Param1:";
			// 
			// textBoxGeneratorParam2
			// 
			this.textBoxGeneratorParam2.Location = new System.Drawing.Point(229, 18);
			this.textBoxGeneratorParam2.Name = "textBoxGeneratorParam2";
			this.textBoxGeneratorParam2.Size = new System.Drawing.Size(80, 20);
			this.textBoxGeneratorParam2.TabIndex = 7;
			this.textBoxGeneratorParam2.TextChanged += new System.EventHandler(this.textBoxGeneratorParam2_TextChanged);
			// 
			// labelGeneratorParam2
			// 
			this.labelGeneratorParam2.AutoSize = true;
			// 
			// 
			// 
			this.labelGeneratorParam2.BackgroundStyle.Class = "";
			this.labelGeneratorParam2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelGeneratorParam2.ForeColor = System.Drawing.Color.White;
			this.labelGeneratorParam2.Location = new System.Drawing.Point(229, 3);
			this.labelGeneratorParam2.Name = "labelGeneratorParam2";
			this.labelGeneratorParam2.Size = new System.Drawing.Size(44, 15);
			this.labelGeneratorParam2.TabIndex = 8;
			this.labelGeneratorParam2.Text = "Param2:";
			// 
			// textBoxGeneratorParam3
			// 
			this.textBoxGeneratorParam3.Location = new System.Drawing.Point(315, 18);
			this.textBoxGeneratorParam3.Name = "textBoxGeneratorParam3";
			this.textBoxGeneratorParam3.Size = new System.Drawing.Size(80, 20);
			this.textBoxGeneratorParam3.TabIndex = 9;
			this.textBoxGeneratorParam3.TextChanged += new System.EventHandler(this.textBoxGeneratorParam3_TextChanged);
			// 
			// labelGeneratorParam3
			// 
			this.labelGeneratorParam3.AutoSize = true;
			// 
			// 
			// 
			this.labelGeneratorParam3.BackgroundStyle.Class = "";
			this.labelGeneratorParam3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelGeneratorParam3.ForeColor = System.Drawing.Color.White;
			this.labelGeneratorParam3.Location = new System.Drawing.Point(315, 3);
			this.labelGeneratorParam3.Name = "labelGeneratorParam3";
			this.labelGeneratorParam3.Size = new System.Drawing.Size(44, 15);
			this.labelGeneratorParam3.TabIndex = 10;
			this.labelGeneratorParam3.Text = "Param3:";
			// 
			// comboBoxCacheUsage
			// 
			this.comboBoxCacheUsage.DisplayMember = "Text";
			this.comboBoxCacheUsage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxCacheUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCacheUsage.FormattingEnabled = true;
			this.comboBoxCacheUsage.ItemHeight = 14;
			this.comboBoxCacheUsage.Location = new System.Drawing.Point(7, 20);
			this.comboBoxCacheUsage.Name = "comboBoxCacheUsage";
			this.comboBoxCacheUsage.Size = new System.Drawing.Size(121, 20);
			this.comboBoxCacheUsage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxCacheUsage.TabIndex = 11;
			this.comboBoxCacheUsage.SelectedIndexChanged += new System.EventHandler(this.comboBoxCacheUsage_SelectedIndexChanged);
			// 
			// comboBoxCacheInclude
			// 
			this.comboBoxCacheInclude.DisplayMember = "Text";
			this.comboBoxCacheInclude.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxCacheInclude.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCacheInclude.FormattingEnabled = true;
			this.comboBoxCacheInclude.ItemHeight = 14;
			this.comboBoxCacheInclude.Location = new System.Drawing.Point(143, 20);
			this.comboBoxCacheInclude.Name = "comboBoxCacheInclude";
			this.comboBoxCacheInclude.Size = new System.Drawing.Size(121, 20);
			this.comboBoxCacheInclude.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxCacheInclude.TabIndex = 12;
			this.comboBoxCacheInclude.SelectedIndexChanged += new System.EventHandler(this.comboBoxCacheInclude_SelectedIndexChanged);
			// 
			// labelCacheUsage
			// 
			this.labelCacheUsage.AutoSize = true;
			this.labelCacheUsage.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelCacheUsage.BackgroundStyle.Class = "";
			this.labelCacheUsage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelCacheUsage.ForeColor = System.Drawing.Color.White;
			this.labelCacheUsage.Location = new System.Drawing.Point(7, 3);
			this.labelCacheUsage.Name = "labelCacheUsage";
			this.labelCacheUsage.Size = new System.Drawing.Size(37, 15);
			this.labelCacheUsage.TabIndex = 13;
			this.labelCacheUsage.Text = "Usage:";
			// 
			// labelCacheInclude
			// 
			this.labelCacheInclude.AutoSize = true;
			this.labelCacheInclude.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelCacheInclude.BackgroundStyle.Class = "";
			this.labelCacheInclude.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelCacheInclude.ForeColor = System.Drawing.Color.White;
			this.labelCacheInclude.Location = new System.Drawing.Point(143, 3);
			this.labelCacheInclude.Name = "labelCacheInclude";
			this.labelCacheInclude.Size = new System.Drawing.Size(41, 15);
			this.labelCacheInclude.TabIndex = 14;
			this.labelCacheInclude.Text = "Include:";
			// 
			// labelCacheRegion
			// 
			this.labelCacheRegion.AutoSize = true;
			this.labelCacheRegion.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelCacheRegion.BackgroundStyle.Class = "";
			this.labelCacheRegion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelCacheRegion.ForeColor = System.Drawing.Color.White;
			this.labelCacheRegion.Location = new System.Drawing.Point(281, 3);
			this.labelCacheRegion.Name = "labelCacheRegion";
			this.labelCacheRegion.Size = new System.Drawing.Size(40, 15);
			this.labelCacheRegion.TabIndex = 15;
			this.labelCacheRegion.Text = "Region:";
			// 
			// textBoxCacheRegion
			// 
			this.textBoxCacheRegion.Location = new System.Drawing.Point(281, 20);
			this.textBoxCacheRegion.Name = "textBoxCacheRegion";
			this.textBoxCacheRegion.Size = new System.Drawing.Size(114, 20);
			this.textBoxCacheRegion.TabIndex = 16;
			this.textBoxCacheRegion.TextChanged += new System.EventHandler(this.textBoxCacheRegion_TextChanged);
			// 
			// groupBoxCache
			// 
			this.groupBoxCache.BackColor = System.Drawing.Color.Transparent;
			this.groupBoxCache.CanvasColor = System.Drawing.Color.Transparent;
			this.groupBoxCache.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.groupBoxCache.Controls.Add(this.labelCacheRegion);
			this.groupBoxCache.Controls.Add(this.comboBoxCacheInclude);
			this.groupBoxCache.Controls.Add(this.textBoxCacheRegion);
			this.groupBoxCache.Controls.Add(this.labelCacheUsage);
			this.groupBoxCache.Controls.Add(this.comboBoxCacheUsage);
			this.groupBoxCache.Controls.Add(this.labelCacheInclude);
			this.groupBoxCache.Location = new System.Drawing.Point(53, 97);
			this.groupBoxCache.Name = "groupBoxCache";
			this.groupBoxCache.Size = new System.Drawing.Size(410, 53);
			// 
			// 
			// 
			this.groupBoxCache.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.groupBoxCache.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.groupBoxCache.Style.BackColorGradientAngle = 90;
			this.groupBoxCache.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupBoxCache.Style.BorderBottomWidth = 1;
			this.groupBoxCache.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.groupBoxCache.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupBoxCache.Style.BorderLeftWidth = 1;
			this.groupBoxCache.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupBoxCache.Style.BorderRightWidth = 1;
			this.groupBoxCache.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupBoxCache.Style.BorderTopWidth = 1;
			this.groupBoxCache.Style.Class = "";
			this.groupBoxCache.Style.CornerDiameter = 4;
			this.groupBoxCache.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.groupBoxCache.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.groupBoxCache.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.groupBoxCache.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.groupBoxCache.StyleMouseDown.Class = "";
			this.groupBoxCache.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupBoxCache.StyleMouseOver.Class = "";
			this.groupBoxCache.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupBoxCache.TabIndex = 18;
			// 
			// groupPanelIdGenerator
			// 
			this.groupPanelIdGenerator.BackColor = System.Drawing.Color.Transparent;
			this.groupPanelIdGenerator.CanvasColor = System.Drawing.Color.Transparent;
			this.groupPanelIdGenerator.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.groupPanelIdGenerator.Controls.Add(this.labelX1);
			this.groupPanelIdGenerator.Controls.Add(this.comboBoxIdGenerator);
			this.groupPanelIdGenerator.Controls.Add(this.labelGeneratorParam2);
			this.groupPanelIdGenerator.Controls.Add(this.textBoxGeneratorParam3);
			this.groupPanelIdGenerator.Controls.Add(this.labelGeneratorParam1);
			this.groupPanelIdGenerator.Controls.Add(this.labelGeneratorParam3);
			this.groupPanelIdGenerator.Controls.Add(this.textBoxGeneratorParam1);
			this.groupPanelIdGenerator.Controls.Add(this.textBoxGeneratorParam2);
			this.groupPanelIdGenerator.Location = new System.Drawing.Point(53, 178);
			this.groupPanelIdGenerator.Name = "groupPanelIdGenerator";
			this.groupPanelIdGenerator.Size = new System.Drawing.Size(410, 51);
			// 
			// 
			// 
			this.groupPanelIdGenerator.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.groupPanelIdGenerator.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
			this.groupPanelIdGenerator.Style.BackColorGradientAngle = 90;
			this.groupPanelIdGenerator.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanelIdGenerator.Style.BorderBottomWidth = 1;
			this.groupPanelIdGenerator.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.groupPanelIdGenerator.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanelIdGenerator.Style.BorderLeftWidth = 1;
			this.groupPanelIdGenerator.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanelIdGenerator.Style.BorderRightWidth = 1;
			this.groupPanelIdGenerator.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanelIdGenerator.Style.BorderTopWidth = 1;
			this.groupPanelIdGenerator.Style.Class = "";
			this.groupPanelIdGenerator.Style.CornerDiameter = 4;
			this.groupPanelIdGenerator.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.groupPanelIdGenerator.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.groupPanelIdGenerator.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.groupPanelIdGenerator.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.groupPanelIdGenerator.StyleMouseDown.Class = "";
			this.groupPanelIdGenerator.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanelIdGenerator.StyleMouseOver.Class = "";
			this.groupPanelIdGenerator.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupPanelIdGenerator.TabIndex = 19;
			// 
			// labelX1
			// 
			this.labelX1.AutoSize = true;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(7, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(55, 15);
			this.labelX1.TabIndex = 11;
			this.labelX1.Text = "Generator:";
			// 
			// virtualPropertyGrid1
			// 
			this.virtualPropertyGrid1.AutoSize = true;
			this.virtualPropertyGrid1.BackColor = System.Drawing.SystemColors.Control;
			this.virtualPropertyGrid1.Location = new System.Drawing.Point(18, 40);
			this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
			this.virtualPropertyGrid1.Size = new System.Drawing.Size(85, 23);
			this.virtualPropertyGrid1.TabIndex = 0;
			// 
			// checkBoxIsAbstract
			// 
			this.checkBoxIsAbstract.AutoSize = true;
			this.checkBoxIsAbstract.Location = new System.Drawing.Point(53, 46);
			this.checkBoxIsAbstract.Name = "checkBoxIsAbstract";
			this.checkBoxIsAbstract.Size = new System.Drawing.Size(15, 14);
			this.checkBoxIsAbstract.TabIndex = 20;
			this.checkBoxIsAbstract.UseVisualStyleBackColor = true;
			this.checkBoxIsAbstract.CheckedChanged += new System.EventHandler(this.checkBoxIsAbstract_CheckedChanged);
			// 
			// EntityDetailsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.checkBoxIsAbstract);
			this.Controls.Add(this.groupPanelIdGenerator);
			this.Controls.Add(this.groupBoxCache);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.virtualPropertyGrid1);
			this.Name = "EntityDetailsEditor";
			this.Size = new System.Drawing.Size(608, 257);
			this.groupBoxCache.ResumeLayout(false);
			this.groupBoxCache.PerformLayout();
			this.groupPanelIdGenerator.ResumeLayout(false);
			this.groupPanelIdGenerator.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid virtualPropertyGrid1;
        private System.Windows.Forms.TextBox textBoxName;
        private DevComponents.DotNetBar.LabelX labelGeneratorParam1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxIdGenerator;
        private System.Windows.Forms.TextBox textBoxGeneratorParam1;
        private System.Windows.Forms.TextBox textBoxGeneratorParam2;
        private DevComponents.DotNetBar.LabelX labelGeneratorParam2;
        private System.Windows.Forms.TextBox textBoxGeneratorParam3;
        private DevComponents.DotNetBar.LabelX labelGeneratorParam3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCacheUsage;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCacheInclude;
        private DevComponents.DotNetBar.LabelX labelCacheUsage;
        private DevComponents.DotNetBar.LabelX labelCacheInclude;
        private DevComponents.DotNetBar.LabelX labelCacheRegion;
        private System.Windows.Forms.TextBox textBoxCacheRegion;
        private DevComponents.DotNetBar.Controls.GroupPanel groupBoxCache;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanelIdGenerator;
		private System.Windows.Forms.CheckBox checkBoxIsAbstract;

    }
}
