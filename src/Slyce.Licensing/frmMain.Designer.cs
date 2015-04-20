namespace Slyce.Licensing
{
	partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.licenseStatus1 = new Slyce.Licensing.LicenseStatus();
            this.SuspendLayout();
            // 
            // licenseStatus1
            // 
            this.licenseStatus1.BackColor = System.Drawing.Color.Transparent;
            this.licenseStatus1.Location = new System.Drawing.Point(4, 1);
            this.licenseStatus1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.licenseStatus1.Name = "licenseStatus1";
            this.licenseStatus1.Size = new System.Drawing.Size(396, 101);
            this.licenseStatus1.TabIndex = 0;
            this.licenseStatus1.Resize += new System.EventHandler(this.licenseStatus1_Resize);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(388, 279);
            this.Controls.Add(this.licenseStatus1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ArchAngel Trial";
            this.ResumeLayout(false);

		}

		#endregion

        private LicenseStatus licenseStatus1;

    }
}