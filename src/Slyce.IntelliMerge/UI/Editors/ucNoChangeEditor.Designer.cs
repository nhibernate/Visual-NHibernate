namespace Slyce.IntelliMerge.UI.Editors
{
	partial class ucNoChangeEditor
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
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucNoChangeEditor));
			this.titleLabel = new System.Windows.Forms.Label();
			this.syntaxEditor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.panel1 = new System.Windows.Forms.Panel();
			this.warningLabel = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// titleLabel
			// 
			this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.titleLabel.Location = new System.Drawing.Point(0, 0);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(748, 20);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "This File Has No Changes";
			this.titleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// syntaxEditor
			// 
			this.syntaxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			document1.ReadOnly = true;
			this.syntaxEditor.Document = document1;
			this.syntaxEditor.Location = new System.Drawing.Point(0, 68);
			this.syntaxEditor.Name = "syntaxEditor";
			this.syntaxEditor.Size = new System.Drawing.Size(748, 431);
			this.syntaxEditor.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.warningLabel);
			this.panel1.Controls.Add(this.titleLabel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(748, 68);
			this.panel1.TabIndex = 2;
			// 
			// warningLabel
			// 
			this.warningLabel.AutoSize = true;
			this.warningLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.warningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.warningLabel.ForeColor = System.Drawing.Color.Red;
			this.warningLabel.Location = new System.Drawing.Point(0, 20);
			this.warningLabel.MaximumSize = new System.Drawing.Size(600, 0);
			this.warningLabel.Name = "warningLabel";
			this.warningLabel.Size = new System.Drawing.Size(589, 48);
			this.warningLabel.TabIndex = 1;
			this.warningLabel.Text = resources.GetString("warningLabel.Text");
			this.warningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ucNoChangeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.syntaxEditor);
			this.Controls.Add(this.panel1);
			this.Name = "ucNoChangeEditor";
			this.Size = new System.Drawing.Size(748, 499);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label titleLabel;
		private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label warningLabel;
	}
}