namespace ArchAngel.Workbench.UserControls
{
	partial class FileActionPanel
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
			this.buttonNotSet = new System.Windows.Forms.Button();
			this.buttonCSIntelliMerge = new System.Windows.Forms.Button();
			this.buttonPlainTextMerge = new System.Windows.Forms.Button();
			this.buttonOverwrite = new System.Windows.Forms.Button();
			this.buttonCreateOnly = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonNotSet
			// 
			this.buttonNotSet.AutoSize = true;
			this.buttonNotSet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonNotSet.Image = global::ArchAngel.Workbench.Properties.Resources.cross_32;
			this.buttonNotSet.Location = new System.Drawing.Point(4, 5);
			this.buttonNotSet.Name = "buttonNotSet";
			this.buttonNotSet.Size = new System.Drawing.Size(38, 38);
			this.buttonNotSet.TabIndex = 5;
			this.toolTip1.SetToolTip(this.buttonNotSet, "Not Set");
			this.buttonNotSet.UseVisualStyleBackColor = true;
			this.buttonNotSet.Click += new System.EventHandler(this.buttonNotSet_Click);
			// 
			// buttonCSIntelliMerge
			// 
			this.buttonCSIntelliMerge.AutoSize = true;
			this.buttonCSIntelliMerge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonCSIntelliMerge.Image = global::ArchAngel.Workbench.Properties.Resources.insert_32;
			this.buttonCSIntelliMerge.Location = new System.Drawing.Point(4, 49);
			this.buttonCSIntelliMerge.Name = "buttonCSIntelliMerge";
			this.buttonCSIntelliMerge.Size = new System.Drawing.Size(38, 38);
			this.buttonCSIntelliMerge.TabIndex = 6;
			this.toolTip1.SetToolTip(this.buttonCSIntelliMerge, "C# IntelliMerge");
			this.buttonCSIntelliMerge.UseVisualStyleBackColor = true;
			this.buttonCSIntelliMerge.Click += new System.EventHandler(this.buttonCSIntelliMerge_Click);
			// 
			// buttonPlainTextMerge
			// 
			this.buttonPlainTextMerge.AutoSize = true;
			this.buttonPlainTextMerge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonPlainTextMerge.Image = global::ArchAngel.Workbench.Properties.Resources.TextCenter;
			this.buttonPlainTextMerge.Location = new System.Drawing.Point(4, 94);
			this.buttonPlainTextMerge.Name = "buttonPlainTextMerge";
			this.buttonPlainTextMerge.Size = new System.Drawing.Size(38, 38);
			this.buttonPlainTextMerge.TabIndex = 7;
			this.toolTip1.SetToolTip(this.buttonPlainTextMerge, "Plain Text Merge");
			this.buttonPlainTextMerge.UseVisualStyleBackColor = true;
			this.buttonPlainTextMerge.Click += new System.EventHandler(this.buttonPlainTextMerge_Click);
			// 
			// buttonOverwrite
			// 
			this.buttonOverwrite.AutoSize = true;
			this.buttonOverwrite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonOverwrite.Image = global::ArchAngel.Workbench.Properties.Resources.reset_32;
			this.buttonOverwrite.Location = new System.Drawing.Point(4, 138);
			this.buttonOverwrite.Name = "buttonOverwrite";
			this.buttonOverwrite.Size = new System.Drawing.Size(38, 38);
			this.buttonOverwrite.TabIndex = 8;
			this.toolTip1.SetToolTip(this.buttonOverwrite, "Overwrite");
			this.buttonOverwrite.UseVisualStyleBackColor = true;
			this.buttonOverwrite.Click += new System.EventHandler(this.buttonOverwrite_Click);
			// 
			// buttonCreateOnly
			// 
			this.buttonCreateOnly.AutoSize = true;
			this.buttonCreateOnly.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonCreateOnly.Image = global::ArchAngel.Workbench.Properties.Resources.plus_32;
			this.buttonCreateOnly.Location = new System.Drawing.Point(4, 182);
			this.buttonCreateOnly.Name = "buttonCreateOnly";
			this.buttonCreateOnly.Size = new System.Drawing.Size(38, 38);
			this.buttonCreateOnly.TabIndex = 9;
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
			this.Controls.Add(this.buttonPlainTextMerge);
			this.Controls.Add(this.buttonCSIntelliMerge);
			this.Controls.Add(this.buttonNotSet);
			this.Name = "FileActionPanel";
			this.Size = new System.Drawing.Size(45, 223);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonNotSet;
		private System.Windows.Forms.Button buttonCSIntelliMerge;
		private System.Windows.Forms.Button buttonPlainTextMerge;
		private System.Windows.Forms.Button buttonOverwrite;
		private System.Windows.Forms.Button buttonCreateOnly;
	}
}
