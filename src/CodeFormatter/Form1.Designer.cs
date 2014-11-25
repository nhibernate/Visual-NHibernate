namespace CodeFormatter
{
	partial class Form1
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
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
			this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.syntaxEditor2 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.button1 = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// syntaxEditor1
			// 
			this.syntaxEditor1.CurrentLineHighlightingVisible = true;
			this.syntaxEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.syntaxEditor1.Document = document1;
			this.syntaxEditor1.Location = new System.Drawing.Point(0, 0);
			this.syntaxEditor1.Name = "syntaxEditor1";
			this.syntaxEditor1.Size = new System.Drawing.Size(392, 500);
			this.syntaxEditor1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 23);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.syntaxEditor1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.syntaxEditor2);
			this.splitContainer1.Size = new System.Drawing.Size(766, 500);
			this.splitContainer1.SplitterDistance = 392;
			this.splitContainer1.TabIndex = 1;
			// 
			// syntaxEditor2
			// 
			this.syntaxEditor2.Dock = System.Windows.Forms.DockStyle.Fill;
			document2.ReadOnly = true;
			this.syntaxEditor2.Document = document2;
			this.syntaxEditor2.Location = new System.Drawing.Point(0, 0);
			this.syntaxEditor2.Name = "syntaxEditor2";
			this.syntaxEditor2.Size = new System.Drawing.Size(370, 500);
			this.syntaxEditor2.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Top;
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(766, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Format";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(766, 523);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor2;
		private System.Windows.Forms.Button button1;
	}
}

