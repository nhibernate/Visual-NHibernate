namespace ArchAngel.Licensing.LicenseWizard
{
    partial class ScreenActivate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenActivate));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelMain = new DevComponents.DotNetBar.PanelEx();
			this.labelDescription = new DevComponents.DotNetBar.LabelX();
			this.labelHeading = new DevComponents.DotNetBar.LabelX();
			this.panelAuto = new DevComponents.DotNetBar.PanelEx();
			this.btnAuto = new DevComponents.DotNetBar.ButtonX();
			this.panelManual = new DevComponents.DotNetBar.PanelEx();
			this.buttonManual = new DevComponents.DotNetBar.ButtonX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.panelMain.SuspendLayout();
			this.panelAuto.SuspendLayout();
			this.panelManual.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelMain.Controls.Add(this.labelDescription);
			this.panelMain.Controls.Add(this.labelHeading);
			this.panelMain.Location = new System.Drawing.Point(188, 42);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(223, 303);
			this.panelMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelMain.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelMain.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelMain.Style.CornerDiameter = 5;
			this.panelMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelMain.Style.GradientAngle = 90;
			this.panelMain.TabIndex = 59;
			// 
			// labelDescription
			// 
			this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelDescription.BackgroundStyle.Class = "";
			this.labelDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelDescription.ForeColor = System.Drawing.Color.White;
			this.labelDescription.Location = new System.Drawing.Point(25, 48);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(172, 238);
			this.labelDescription.TabIndex = 48;
			this.labelDescription.Text = "labelX1";
			this.labelDescription.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelDescription.WordWrap = true;
			// 
			// labelHeading
			// 
			this.labelHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelHeading.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelHeading.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelHeading.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelHeading.BackgroundStyle.BorderBottomWidth = 1;
			this.labelHeading.BackgroundStyle.Class = "";
			this.labelHeading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelHeading.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHeading.ForeColor = System.Drawing.Color.White;
			this.labelHeading.Location = new System.Drawing.Point(25, 9);
			this.labelHeading.Name = "labelHeading";
			this.labelHeading.SingleLineColor = System.Drawing.Color.White;
			this.labelHeading.Size = new System.Drawing.Size(172, 23);
			this.labelHeading.TabIndex = 47;
			this.labelHeading.Text = "Getting Started";
			// 
			// panelAuto
			// 
			this.panelAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelAuto.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelAuto.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelAuto.Controls.Add(this.btnAuto);
			this.panelAuto.Location = new System.Drawing.Point(3, 42);
			this.panelAuto.Name = "panelAuto";
			this.panelAuto.Size = new System.Drawing.Size(408, 56);
			this.panelAuto.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelAuto.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelAuto.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelAuto.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelAuto.Style.CornerDiameter = 5;
			this.panelAuto.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelAuto.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelAuto.Style.GradientAngle = 90;
			this.panelAuto.TabIndex = 55;
			this.panelAuto.MouseEnter += new System.EventHandler(this.panelAuto_MouseEnter);
			// 
			// btnAuto
			// 
			this.btnAuto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnAuto.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnAuto.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnAuto.Image = ((System.Drawing.Image)(resources.GetObject("btnAuto.Image")));
			this.btnAuto.Location = new System.Drawing.Point(9, 12);
			this.btnAuto.Name = "btnAuto";
			this.btnAuto.Size = new System.Drawing.Size(153, 31);
			this.btnAuto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnAuto.TabIndex = 47;
			this.btnAuto.Text = "Auto  (recommended)";
			this.btnAuto.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
			this.btnAuto.MouseEnter += new System.EventHandler(this.btnAuto_MouseEnter);
			// 
			// panelManual
			// 
			this.panelManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelManual.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelManual.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelManual.Controls.Add(this.buttonManual);
			this.panelManual.Location = new System.Drawing.Point(3, 104);
			this.panelManual.Name = "panelManual";
			this.panelManual.Size = new System.Drawing.Size(408, 56);
			this.panelManual.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelManual.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelManual.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelManual.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelManual.Style.CornerDiameter = 5;
			this.panelManual.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelManual.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelManual.Style.GradientAngle = 90;
			this.panelManual.TabIndex = 57;
			this.panelManual.MouseEnter += new System.EventHandler(this.panelManual_MouseEnter);
			// 
			// buttonManual
			// 
			this.buttonManual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonManual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonManual.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonManual.Image = ((System.Drawing.Image)(resources.GetObject("buttonManual.Image")));
			this.buttonManual.Location = new System.Drawing.Point(9, 12);
			this.buttonManual.Name = "buttonManual";
			this.buttonManual.Size = new System.Drawing.Size(153, 31);
			this.buttonManual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonManual.TabIndex = 41;
			this.buttonManual.Text = "Manual";
			this.buttonManual.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonManual.Click += new System.EventHandler(this.buttonManual_Click);
			this.buttonManual.MouseEnter += new System.EventHandler(this.buttonManual_MouseEnter);
			// 
			// labelX2
			// 
			this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelX2.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelX2.BackgroundStyle.BorderBottomWidth = 1;
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX2.ForeColor = System.Drawing.Color.DimGray;
			this.labelX2.Location = new System.Drawing.Point(12, 3);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(399, 23);
			this.labelX2.TabIndex = 54;
			this.labelX2.Text = "Install License";
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// ScreenActivate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelAuto);
			this.Controls.Add(this.panelManual);
			this.Controls.Add(this.labelX2);
			this.Name = "ScreenActivate";
			this.Size = new System.Drawing.Size(426, 360);
			this.panelMain.ResumeLayout(false);
			this.panelAuto.ResumeLayout(false);
			this.panelManual.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private DevComponents.DotNetBar.PanelEx panelMain;
		private DevComponents.DotNetBar.LabelX labelDescription;
		private DevComponents.DotNetBar.LabelX labelHeading;
		private DevComponents.DotNetBar.PanelEx panelAuto;
		private DevComponents.DotNetBar.ButtonX btnAuto;
		private DevComponents.DotNetBar.PanelEx panelManual;
		private DevComponents.DotNetBar.ButtonX buttonManual;
		private DevComponents.DotNetBar.LabelX labelX2;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;

    }
}
