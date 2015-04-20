namespace ArchAngel.Workbench.UserControls
{
	partial class TextFileActionPanel
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonOverwrite = new System.Windows.Forms.Button();
			this.buttonCreateOnly = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonOverwrite
			// 
			this.buttonOverwrite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonOverwrite.Image = global::ArchAngel.Workbench.Properties.Resources.reset_32;
			this.buttonOverwrite.Location = new System.Drawing.Point(4, 3);
			this.buttonOverwrite.Name = "buttonOverwrite";
			this.buttonOverwrite.Size = new System.Drawing.Size(72, 55);
			this.buttonOverwrite.TabIndex = 8;
			this.buttonOverwrite.Text = "Overwrite";
			this.buttonOverwrite.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonOverwrite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolTip1.SetToolTip(this.buttonOverwrite, "Overwrite");
			this.buttonOverwrite.UseVisualStyleBackColor = true;
			this.buttonOverwrite.Click += new System.EventHandler(this.buttonOverwrite_Click);
			// 
			// buttonCreateOnly
			// 
			this.buttonCreateOnly.AutoSize = true;
			this.buttonCreateOnly.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonCreateOnly.Image = global::ArchAngel.Workbench.Properties.Resources.plus_32;
			this.buttonCreateOnly.Location = new System.Drawing.Point(4, 64);
			this.buttonCreateOnly.Name = "buttonCreateOnly";
			this.buttonCreateOnly.Size = new System.Drawing.Size(72, 55);
			this.buttonCreateOnly.TabIndex = 9;
			this.buttonCreateOnly.Text = "Create Only";
			this.buttonCreateOnly.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.buttonCreateOnly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolTip1.SetToolTip(this.buttonCreateOnly, "Create Only");
			this.buttonCreateOnly.UseVisualStyleBackColor = true;
			this.buttonCreateOnly.Click += new System.EventHandler(this.buttonCreateOnly_Click);
			// 
			// FileActionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.buttonCreateOnly);
			this.Controls.Add(this.buttonOverwrite);
			this.Name = "FileActionPanel";
			this.Size = new System.Drawing.Size(79, 122);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonOverwrite;
		private System.Windows.Forms.Button buttonCreateOnly;
	}
}
