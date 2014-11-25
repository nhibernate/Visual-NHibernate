namespace ArchAngel.Workbench.ContentItems
{
    partial class Options
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
			this.components = new System.ComponentModel.Container();
			DevComponents.DotNetBar.Rendering.SuperTabItemColorTable superTabItemColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabItemColorTable();
			DevComponents.DotNetBar.Rendering.SuperTabColorStates superTabColorStates1 = new DevComponents.DotNetBar.Rendering.SuperTabColorStates();
			DevComponents.DotNetBar.Rendering.SuperTabItemStateColorTable superTabItemStateColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabItemStateColorTable();
			DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable superTabLinearGradientColorTable1 = new DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
			this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonResetAllDefaults = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
			this.superTabControl1.SuspendLayout();
			this.superTabControlPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 200;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 100;
			// 
			// highlighter1
			// 
			this.highlighter1.ContainerControl = this;
			this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// superTabControl1
			// 
			// 
			// 
			// 
			// 
			// 
			// 
			this.superTabControl1.ControlBox.CloseBox.Name = "";
			// 
			// 
			// 
			this.superTabControl1.ControlBox.MenuBox.Name = "";
			this.superTabControl1.ControlBox.Name = "";
			this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
			this.superTabControl1.Controls.Add(this.superTabControlPanel1);
			this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControl1.Location = new System.Drawing.Point(0, 54);
			this.superTabControl1.Name = "superTabControl1";
			this.superTabControl1.ReorderTabsEnabled = false;
			this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.superTabControl1.SelectedTabIndex = 0;
			this.superTabControl1.Size = new System.Drawing.Size(695, 217);
			this.superTabControl1.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
			this.superTabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superTabControl1.TabIndex = 5;
			this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
			this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.superTabControl1.Text = "superTabControl1";
			// 
			// superTabControlPanel1
			// 
			this.superTabControlPanel1.CanvasColor = System.Drawing.Color.Transparent;
			this.superTabControlPanel1.Controls.Add(this.textBox1);
			this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel1.Location = new System.Drawing.Point(99, 0);
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.Size = new System.Drawing.Size(596, 217);
			this.superTabControlPanel1.TabIndex = 1;
			this.superTabControlPanel1.TabItem = this.superTabItem1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(79, 22);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 0;
			// 
			// superTabItem1
			// 
			this.superTabItem1.AttachedControl = this.superTabControlPanel1;
			this.superTabItem1.GlobalItem = false;
			this.superTabItem1.Name = "superTabItem1";
			superTabLinearGradientColorTable1.AdaptiveGradient = false;
			superTabLinearGradientColorTable1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))))};
			superTabItemStateColorTable1.Background = superTabLinearGradientColorTable1;
			superTabItemStateColorTable1.CloseMarker = System.Drawing.Color.Red;
			superTabItemStateColorTable1.InnerBorder = System.Drawing.Color.Green;
			superTabItemStateColorTable1.OuterBorder = System.Drawing.Color.Yellow;
			superTabColorStates1.Selected = superTabItemStateColorTable1;
			superTabItemColorTable1.Left = superTabColorStates1;
			this.superTabItem1.TabColor = superTabItemColorTable1;
			this.superTabItem1.Text = "superTabItem1";
			// 
			// labelX1
			// 
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.labelX1.BackgroundStyle.BackColor2 = System.Drawing.Color.Black;
			this.labelX1.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX1.Location = new System.Drawing.Point(0, 0);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(695, 54);
			this.labelX1.TabIndex = 7;
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonResetAllDefaults
			// 
			this.buttonResetAllDefaults.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonResetAllDefaults.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonResetAllDefaults.DisabledImage = ((System.Drawing.Image)(resources.GetObject("buttonResetAllDefaults.DisabledImage")));
			this.buttonResetAllDefaults.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonResetAllDefaults.HoverImage")));
			this.buttonResetAllDefaults.Image = ((System.Drawing.Image)(resources.GetObject("buttonResetAllDefaults.Image")));
			this.buttonResetAllDefaults.Location = new System.Drawing.Point(12, 10);
			this.buttonResetAllDefaults.Name = "buttonResetAllDefaults";
			this.buttonResetAllDefaults.Size = new System.Drawing.Size(133, 33);
			this.buttonResetAllDefaults.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.buttonResetAllDefaults, new DevComponents.DotNetBar.SuperTooltipInfo("Re-generate the files", "<b>Note:</b> No files will be written to your disk until you click the \'Write fil" +
						"es to disk\' button.", "Re-generate the files based on your model.", ((System.Drawing.Image)(resources.GetObject("buttonResetAllDefaults.SuperTooltip"))), ((System.Drawing.Image)(resources.GetObject("buttonResetAllDefaults.SuperTooltip1"))), DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonResetAllDefaults.TabIndex = 69;
			this.buttonResetAllDefaults.Text = " Reset all defaults";
			this.buttonResetAllDefaults.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonResetAllDefaults.Click += new System.EventHandler(this.buttonResetAllDefaults_Click);
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.Controls.Add(this.buttonResetAllDefaults);
			this.Controls.Add(this.superTabControl1);
			this.Controls.Add(this.labelX1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Options";
			this.NavBarIcon = ((System.Drawing.Image)(resources.GetObject("$this.NavBarIcon")));
			this.Size = new System.Drawing.Size(695, 271);
			this.VisibleChanged += new System.EventHandler(this.Options_VisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
			this.superTabControl1.ResumeLayout(false);
			this.superTabControlPanel1.ResumeLayout(false);
			this.superTabControlPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
		private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private System.Windows.Forms.TextBox textBox1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private DevComponents.DotNetBar.ButtonX buttonResetAllDefaults;
    }
}
