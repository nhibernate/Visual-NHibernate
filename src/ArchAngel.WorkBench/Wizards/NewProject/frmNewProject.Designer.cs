namespace ArchAngel.Workbench.Wizards.NewProject
{
	partial class frmNewProject
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewProject));
			this.panelContent = new DevComponents.DotNetBar.PanelEx();
			this.SuspendLayout();
			// 
			// panelContent
			// 
			this.panelContent.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelContent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(0, 0);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(605, 540);
			this.panelContent.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelContent.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.panelContent.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.panelContent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelContent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelContent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelContent.Style.GradientAngle = 90;
			this.panelContent.TabIndex = 0;
			// 
			// frmNewProject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(605, 540);
			this.Controls.Add(this.panelContent);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(613, 434);
			this.Name = "frmNewProject";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New / Open Project";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewProject_FormClosing);
			this.Load += new System.EventHandler(this.frmNewProject_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelContent;
    }
}