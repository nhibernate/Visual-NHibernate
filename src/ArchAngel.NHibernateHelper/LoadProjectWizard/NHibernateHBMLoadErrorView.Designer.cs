namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	partial class NHibernateHBMLoadErrorView
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.buttonClipboard = new DevComponents.DotNetBar.ButtonX();
			this.buttonClose = new DevComponents.DotNetBar.ButtonX();
			this.label1 = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx1.Controls.Add(this.buttonClipboard);
			this.panelEx1.Controls.Add(this.buttonClose);
			this.panelEx1.Controls.Add(this.label1);
			this.panelEx1.Controls.Add(this.labelX1);
			this.panelEx1.Controls.Add(this.listBox1);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(581, 474);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// buttonClipboard
			// 
			this.buttonClipboard.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonClipboard.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonClipboard.Location = new System.Drawing.Point(383, 438);
			this.buttonClipboard.Name = "buttonClipboard";
			this.buttonClipboard.Size = new System.Drawing.Size(104, 23);
			this.buttonClipboard.TabIndex = 5;
			this.buttonClipboard.Text = "Copy To Clipboard";
			this.buttonClipboard.Click += new System.EventHandler(this.buttonClipboard_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonClose.Location = new System.Drawing.Point(493, 439);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(556, 44);
			this.label1.TabIndex = 3;
			this.label1.WordWrap = true;
			// 
			// labelX1
			// 
			this.labelX1.Location = new System.Drawing.Point(16, 63);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(227, 23);
			this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.labelX1.TabIndex = 2;
			this.labelX1.Text = "(Line Number : Column) - Error Message";
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(12, 92);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(557, 342);
			this.listBox1.TabIndex = 0;
			// 
			// NHibernateHBMLoadErrorView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(581, 474);
			this.Controls.Add(this.panelEx1);
			this.Name = "NHibernateHBMLoadErrorView";
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.PanelEx panelEx1;
		private System.Windows.Forms.ListBox listBox1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.ButtonX buttonClose;
		private DevComponents.DotNetBar.LabelX label1;
		private DevComponents.DotNetBar.ButtonX buttonClipboard;
	}
}