namespace SlyceScripter
{
	partial class frmCompile
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
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnSelectScriptFile = new System.Windows.Forms.Button();
			this.txtOutputFile = new System.Windows.Forms.TextBox();
			this.btnOutputFile = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(246, 62);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 1;
			this.btnCreate.Text = "Compile";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(338, 62);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSelectScriptFile
			// 
			this.btnSelectScriptFile.Location = new System.Drawing.Point(501, 62);
			this.btnSelectScriptFile.Name = "btnSelectScriptFile";
			this.btnSelectScriptFile.Size = new System.Drawing.Size(75, 23);
			this.btnSelectScriptFile.TabIndex = 3;
			this.btnSelectScriptFile.Text = "Add";
			this.btnSelectScriptFile.UseVisualStyleBackColor = true;
			this.btnSelectScriptFile.Visible = false;
			this.btnSelectScriptFile.Click += new System.EventHandler(this.btnSelectScriptFile_Click);
			// 
			// txtOutputFile
			// 
			this.txtOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputFile.Location = new System.Drawing.Point(5, 23);
			this.txtOutputFile.Name = "txtOutputFile";
			this.txtOutputFile.Size = new System.Drawing.Size(588, 22);
			this.txtOutputFile.TabIndex = 4;
			this.txtOutputFile.TextChanged += new System.EventHandler(this.txtOutputFile_TextChanged);
			// 
			// btnOutputFile
			// 
			this.btnOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOutputFile.Location = new System.Drawing.Point(599, 23);
			this.btnOutputFile.Name = "btnOutputFile";
			this.btnOutputFile.Size = new System.Drawing.Size(34, 23);
			this.btnOutputFile.TabIndex = 5;
			this.btnOutputFile.Text = "....";
			this.btnOutputFile.UseVisualStyleBackColor = true;
			this.btnOutputFile.Click += new System.EventHandler(this.btnOutputFile_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// frmCompile
			// 
			this.AcceptButton = this.btnCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(648, 116);
			this.Controls.Add(this.btnOutputFile);
			this.Controls.Add(this.txtOutputFile);
			this.Controls.Add(this.btnSelectScriptFile);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCreate);
			this.Name = "frmCompile";
			this.ShowInTaskbar = false;
			this.Text = "Compile";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmCompile_Paint);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCompile_FormClosing);
			this.Load += new System.EventHandler(this.frmCompile_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnSelectScriptFile;
		private System.Windows.Forms.TextBox txtOutputFile;
		private System.Windows.Forms.Button btnOutputFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}