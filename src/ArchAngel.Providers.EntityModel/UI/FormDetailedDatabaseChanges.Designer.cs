namespace ArchAngel.Providers.EntityModel.UI
{
	partial class FormDetailedDatabaseChanges
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
            this.rtbChanges = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbChanges
            // 
            this.rtbChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChanges.Location = new System.Drawing.Point(0, 0);
            this.rtbChanges.Name = "rtbChanges";
            this.rtbChanges.Size = new System.Drawing.Size(536, 494);
            this.rtbChanges.TabIndex = 0;
            this.rtbChanges.Text = "";
            // 
            // FormDetailedDatabaseChanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 494);
            this.Controls.Add(this.rtbChanges);
            this.Name = "FormDetailedDatabaseChanges";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Changes";
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox rtbChanges;
	}
}