namespace ApiExtender
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
			this.button1 = new System.Windows.Forms.Button();
			this.txtDll = new System.Windows.Forms.TextBox();
			this.txtCsprojFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseDll = new System.Windows.Forms.Button();
			this.btnBrowseProjectFile = new System.Windows.Forms.Button();
			this.rtfOutput = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtKeyName = new System.Windows.Forms.TextBox();
			this.chkSignAssembly = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(383, 253);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Go";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtDll
			// 
			this.txtDll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDll.Location = new System.Drawing.Point(117, 43);
			this.txtDll.Name = "txtDll";
			this.txtDll.Size = new System.Drawing.Size(314, 20);
			this.txtDll.TabIndex = 1;
			this.txtDll.Text = "C:\\Projects\\SVN\\ArchAngel\\trunk\\ArchAngel.Providers.EntityModel\\bin\\Release\\ArchA" +
				"ngel.Providers.EntityModel.dll";
			// 
			// txtCsprojFile
			// 
			this.txtCsprojFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCsprojFile.Location = new System.Drawing.Point(117, 69);
			this.txtCsprojFile.Name = "txtCsprojFile";
			this.txtCsprojFile.Size = new System.Drawing.Size(314, 20);
			this.txtCsprojFile.TabIndex = 2;
			this.txtCsprojFile.Text = "C:\\Projects\\SVN\\ArchAngel\\trunk\\ArchAngel.Providers.EntityModel\\ArchAngel.Provide" +
				"rs.EntityModel.csproj";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Project file";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(27, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Compiled DLL";
			// 
			// btnBrowseDll
			// 
			this.btnBrowseDll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseDll.Location = new System.Drawing.Point(437, 41);
			this.btnBrowseDll.Name = "btnBrowseDll";
			this.btnBrowseDll.Size = new System.Drawing.Size(32, 23);
			this.btnBrowseDll.TabIndex = 5;
			this.btnBrowseDll.Text = "...";
			this.btnBrowseDll.UseVisualStyleBackColor = true;
			this.btnBrowseDll.Click += new System.EventHandler(this.btnBrowseDll_Click);
			// 
			// btnBrowseProjectFile
			// 
			this.btnBrowseProjectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseProjectFile.Location = new System.Drawing.Point(437, 67);
			this.btnBrowseProjectFile.Name = "btnBrowseProjectFile";
			this.btnBrowseProjectFile.Size = new System.Drawing.Size(32, 23);
			this.btnBrowseProjectFile.TabIndex = 6;
			this.btnBrowseProjectFile.Text = "...";
			this.btnBrowseProjectFile.UseVisualStyleBackColor = true;
			this.btnBrowseProjectFile.Click += new System.EventHandler(this.btnBrowseProjectFile_Click);
			// 
			// rtfOutput
			// 
			this.rtfOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtfOutput.Location = new System.Drawing.Point(30, 146);
			this.rtfOutput.Name = "rtfOutput";
			this.rtfOutput.Size = new System.Drawing.Size(418, 88);
			this.rtfOutput.TabIndex = 7;
			this.rtfOutput.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(27, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Key File";
			// 
			// txtKeyName
			// 
			this.txtKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtKeyName.Location = new System.Drawing.Point(117, 95);
			this.txtKeyName.Name = "txtKeyName";
			this.txtKeyName.Size = new System.Drawing.Size(314, 20);
			this.txtKeyName.TabIndex = 9;
			this.txtKeyName.Text = "C:\\Projects\\SVN\\ArchAngel\\trunk\\ArchAngel.Providers.EntityModel\\slyce_strong_name" +
				"_key.snk";
			// 
			// chkSignAssembly
			// 
			this.chkSignAssembly.AutoSize = true;
			this.chkSignAssembly.Location = new System.Drawing.Point(96, 96);
			this.chkSignAssembly.Name = "chkSignAssembly";
			this.chkSignAssembly.Size = new System.Drawing.Size(15, 14);
			this.chkSignAssembly.TabIndex = 10;
			this.chkSignAssembly.UseVisualStyleBackColor = true;
			this.chkSignAssembly.CheckedChanged += new System.EventHandler(this.chkSignAssembly_CheckedChanged);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(437, 93);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(32, 23);
			this.button2.TabIndex = 11;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 288);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.chkSignAssembly);
			this.Controls.Add(this.txtKeyName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.rtfOutput);
			this.Controls.Add(this.btnBrowseProjectFile);
			this.Controls.Add(this.btnBrowseDll);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtCsprojFile);
			this.Controls.Add(this.txtDll);
			this.Controls.Add(this.button1);
			this.MinimumSize = new System.Drawing.Size(489, 220);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "API Extension Tool";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtDll;
		private System.Windows.Forms.TextBox txtCsprojFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseDll;
		private System.Windows.Forms.Button btnBrowseProjectFile;
		private System.Windows.Forms.RichTextBox rtfOutput;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtKeyName;
		private System.Windows.Forms.CheckBox chkSignAssembly;
		private System.Windows.Forms.Button button2;
    }
}

