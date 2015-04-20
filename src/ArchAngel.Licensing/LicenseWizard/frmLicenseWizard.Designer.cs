namespace ArchAngel.Licensing.LicenseWizard
{
	partial class frmLicenseWizard
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicenseWizard));
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.panelContent = new DevComponents.DotNetBar.PanelEx();
			this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
			this.SuspendLayout();
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 353);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(469, 35);
			this.ucHeading1.TabIndex = 23;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// panelContent
			// 
			this.panelContent.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelContent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(0, 0);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(469, 353);
			this.panelContent.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelContent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelContent.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
			this.panelContent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelContent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelContent.Style.GradientAngle = 90;
			this.panelContent.TabIndex = 27;
			// 
			// styleManager1
			// 
			this.styleManager1.ManagerColorTint = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Black;
			// 
			// frmLicenseWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(469, 388);
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.ucHeading1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLicenseWizard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Visual NHibernate";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLicenseWizard_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private Slyce.Common.Controls.ucHeading ucHeading1;
		private DevComponents.DotNetBar.PanelEx panelContent;
		private DevComponents.DotNetBar.StyleManager styleManager1;
	}
}