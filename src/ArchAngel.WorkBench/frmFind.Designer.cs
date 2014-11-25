namespace ArchAngel.Workbench
{
	partial class frmFind
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblReplace = new System.Windows.Forms.Label();
			this.ddlFind = new System.Windows.Forms.ComboBox();
			this.ddlReplace = new System.Windows.Forms.ComboBox();
			this.chkFindMatchWholeWord = new System.Windows.Forms.CheckBox();
			this.chkFindMatchCase = new System.Windows.Forms.CheckBox();
			this.chkScopeScript = new System.Windows.Forms.RadioButton();
			this.chkScopeBoth = new System.Windows.Forms.RadioButton();
			this.chkScopeOutput = new System.Windows.Forms.RadioButton();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.buttonReplaceAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonReplace = new DevComponents.DotNetBar.ButtonX();
			this.buttonFindAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonFind = new DevComponents.DotNetBar.ButtonX();
			this.radioButtonCurrentFile = new System.Windows.Forms.RadioButton();
			this.radioButtonAllFiles = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panelOptions = new DevComponents.DotNetBar.PanelEx();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panelOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 15);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Find what:";
			// 
			// lblReplace
			// 
			this.lblReplace.AutoSize = true;
			this.lblReplace.Location = new System.Drawing.Point(15, 53);
			this.lblReplace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblReplace.Name = "lblReplace";
			this.lblReplace.Size = new System.Drawing.Size(72, 13);
			this.lblReplace.TabIndex = 20;
			this.lblReplace.Text = "Replace with:";
			// 
			// ddlFind
			// 
			this.ddlFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ddlFind.FormattingEnabled = true;
			this.ddlFind.Location = new System.Drawing.Point(18, 30);
			this.ddlFind.Margin = new System.Windows.Forms.Padding(2);
			this.ddlFind.Name = "ddlFind";
			this.ddlFind.Size = new System.Drawing.Size(180, 21);
			this.ddlFind.TabIndex = 0;
			// 
			// ddlReplace
			// 
			this.ddlReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ddlReplace.FormattingEnabled = true;
			this.ddlReplace.Location = new System.Drawing.Point(18, 68);
			this.ddlReplace.Margin = new System.Windows.Forms.Padding(2);
			this.ddlReplace.Name = "ddlReplace";
			this.ddlReplace.Size = new System.Drawing.Size(180, 21);
			this.ddlReplace.TabIndex = 1;
			// 
			// chkFindMatchWholeWord
			// 
			this.chkFindMatchWholeWord.AutoSize = true;
			this.chkFindMatchWholeWord.Location = new System.Drawing.Point(15, 42);
			this.chkFindMatchWholeWord.Margin = new System.Windows.Forms.Padding(2);
			this.chkFindMatchWholeWord.Name = "chkFindMatchWholeWord";
			this.chkFindMatchWholeWord.Size = new System.Drawing.Size(113, 17);
			this.chkFindMatchWholeWord.TabIndex = 7;
			this.chkFindMatchWholeWord.Text = "Match whole word";
			this.chkFindMatchWholeWord.UseVisualStyleBackColor = true;
			// 
			// chkFindMatchCase
			// 
			this.chkFindMatchCase.AutoSize = true;
			this.chkFindMatchCase.Location = new System.Drawing.Point(15, 20);
			this.chkFindMatchCase.Margin = new System.Windows.Forms.Padding(2);
			this.chkFindMatchCase.Name = "chkFindMatchCase";
			this.chkFindMatchCase.Size = new System.Drawing.Size(82, 17);
			this.chkFindMatchCase.TabIndex = 6;
			this.chkFindMatchCase.Text = "Match case";
			this.chkFindMatchCase.UseVisualStyleBackColor = true;
			// 
			// chkScopeScript
			// 
			this.chkScopeScript.AutoSize = true;
			this.chkScopeScript.Location = new System.Drawing.Point(11, 20);
			this.chkScopeScript.Margin = new System.Windows.Forms.Padding(2);
			this.chkScopeScript.Name = "chkScopeScript";
			this.chkScopeScript.Size = new System.Drawing.Size(74, 17);
			this.chkScopeScript.TabIndex = 3;
			this.chkScopeScript.TabStop = true;
			this.chkScopeScript.Text = "Script only";
			this.chkScopeScript.UseVisualStyleBackColor = true;
			// 
			// chkScopeBoth
			// 
			this.chkScopeBoth.AutoSize = true;
			this.chkScopeBoth.Checked = true;
			this.chkScopeBoth.Location = new System.Drawing.Point(11, 54);
			this.chkScopeBoth.Margin = new System.Windows.Forms.Padding(2);
			this.chkScopeBoth.Name = "chkScopeBoth";
			this.chkScopeBoth.Size = new System.Drawing.Size(47, 17);
			this.chkScopeBoth.TabIndex = 5;
			this.chkScopeBoth.TabStop = true;
			this.chkScopeBoth.Text = "Both";
			this.chkScopeBoth.UseVisualStyleBackColor = true;
			// 
			// chkScopeOutput
			// 
			this.chkScopeOutput.AutoSize = true;
			this.chkScopeOutput.Location = new System.Drawing.Point(11, 37);
			this.chkScopeOutput.Margin = new System.Windows.Forms.Padding(2);
			this.chkScopeOutput.Name = "chkScopeOutput";
			this.chkScopeOutput.Size = new System.Drawing.Size(79, 17);
			this.chkScopeOutput.TabIndex = 4;
			this.chkScopeOutput.TabStop = true;
			this.chkScopeOutput.Text = "Output only";
			this.chkScopeOutput.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(155, 2);
			this.btnClose.Margin = new System.Windows.Forms.Padding(2);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(56, 19);
			this.btnClose.TabIndex = 20;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.chkScopeOutput);
			this.groupBox1.Controls.Add(this.chkScopeScript);
			this.groupBox1.Controls.Add(this.chkScopeBoth);
			this.groupBox1.Location = new System.Drawing.Point(3, 49);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(173, 79);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Scope";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.chkFindMatchWholeWord);
			this.groupBox2.Controls.Add(this.chkFindMatchCase);
			this.groupBox2.Location = new System.Drawing.Point(3, 134);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(173, 71);
			this.groupBox2.TabIndex = 21;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Find options";
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.panelOptions);
			this.panelEx1.Controls.Add(this.ddlReplace);
			this.panelEx1.Controls.Add(this.label1);
			this.panelEx1.Controls.Add(this.lblReplace);
			this.panelEx1.Controls.Add(this.ddlFind);
			this.panelEx1.Controls.Add(this.btnClose);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(215, 375);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 22;
			// 
			// buttonReplaceAll
			// 
			this.buttonReplaceAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReplaceAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonReplaceAll.Location = new System.Drawing.Point(101, 240);
			this.buttonReplaceAll.Name = "buttonReplaceAll";
			this.buttonReplaceAll.Size = new System.Drawing.Size(75, 23);
			this.buttonReplaceAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonReplaceAll.TabIndex = 25;
			this.buttonReplaceAll.Text = "Replace All";
			this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
			// 
			// buttonReplace
			// 
			this.buttonReplace.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReplace.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonReplace.Location = new System.Drawing.Point(101, 211);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.Size = new System.Drawing.Size(75, 23);
			this.buttonReplace.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonReplace.TabIndex = 24;
			this.buttonReplace.Text = "&Replace";
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// buttonFindAll
			// 
			this.buttonFindAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonFindAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFindAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonFindAll.Location = new System.Drawing.Point(5, 240);
			this.buttonFindAll.Name = "buttonFindAll";
			this.buttonFindAll.Size = new System.Drawing.Size(75, 23);
			this.buttonFindAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonFindAll.TabIndex = 23;
			this.buttonFindAll.Text = "Find &All";
			this.buttonFindAll.Click += new System.EventHandler(this.buttonFindAll_Click);
			// 
			// buttonFind
			// 
			this.buttonFind.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFind.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonFind.Location = new System.Drawing.Point(5, 211);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.Size = new System.Drawing.Size(75, 23);
			this.buttonFind.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonFind.TabIndex = 22;
			this.buttonFind.Text = "&Find Next";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// radioButtonCurrentFile
			// 
			this.radioButtonCurrentFile.AutoSize = true;
			this.radioButtonCurrentFile.Checked = true;
			this.radioButtonCurrentFile.Location = new System.Drawing.Point(11, 15);
			this.radioButtonCurrentFile.Margin = new System.Windows.Forms.Padding(2);
			this.radioButtonCurrentFile.Name = "radioButtonCurrentFile";
			this.radioButtonCurrentFile.Size = new System.Drawing.Size(75, 17);
			this.radioButtonCurrentFile.TabIndex = 26;
			this.radioButtonCurrentFile.TabStop = true;
			this.radioButtonCurrentFile.Text = "Current file";
			this.radioButtonCurrentFile.UseVisualStyleBackColor = true;
			// 
			// radioButtonAllFiles
			// 
			this.radioButtonAllFiles.AutoSize = true;
			this.radioButtonAllFiles.Location = new System.Drawing.Point(98, 15);
			this.radioButtonAllFiles.Margin = new System.Windows.Forms.Padding(2);
			this.radioButtonAllFiles.Name = "radioButtonAllFiles";
			this.radioButtonAllFiles.Size = new System.Drawing.Size(57, 17);
			this.radioButtonAllFiles.TabIndex = 27;
			this.radioButtonAllFiles.Text = "All files";
			this.radioButtonAllFiles.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.radioButtonCurrentFile);
			this.groupBox3.Controls.Add(this.radioButtonAllFiles);
			this.groupBox3.Location = new System.Drawing.Point(3, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(173, 40);
			this.groupBox3.TabIndex = 28;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Look in";
			// 
			// panelOptions
			// 
			this.panelOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelOptions.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelOptions.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelOptions.Controls.Add(this.groupBox3);
			this.panelOptions.Controls.Add(this.buttonReplaceAll);
			this.panelOptions.Controls.Add(this.groupBox1);
			this.panelOptions.Controls.Add(this.buttonReplace);
			this.panelOptions.Controls.Add(this.groupBox2);
			this.panelOptions.Controls.Add(this.buttonFindAll);
			this.panelOptions.Controls.Add(this.buttonFind);
			this.panelOptions.Location = new System.Drawing.Point(18, 98);
			this.panelOptions.Name = "panelOptions";
			this.panelOptions.Size = new System.Drawing.Size(180, 270);
			this.panelOptions.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelOptions.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelOptions.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelOptions.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelOptions.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelOptions.Style.GradientAngle = 90;
			this.panelOptions.TabIndex = 29;
			// 
			// frmFind
			// 
			this.AcceptButton = this.buttonFind;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(215, 375);
			this.Controls.Add(this.panelEx1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmFind";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFind_FormClosed);
			this.Load += new System.EventHandler(this.frmFind_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmFind_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFind_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.panelOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblReplace;
		private System.Windows.Forms.ComboBox ddlFind;
		private System.Windows.Forms.ComboBox ddlReplace;
		private System.Windows.Forms.CheckBox chkFindMatchWholeWord;
		private System.Windows.Forms.CheckBox chkFindMatchCase;
		private System.Windows.Forms.RadioButton chkScopeScript;
		private System.Windows.Forms.RadioButton chkScopeBoth;
		private System.Windows.Forms.RadioButton chkScopeOutput;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.ButtonX buttonReplaceAll;
		private DevComponents.DotNetBar.ButtonX buttonReplace;
		private DevComponents.DotNetBar.ButtonX buttonFindAll;
		private DevComponents.DotNetBar.ButtonX buttonFind;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioButtonCurrentFile;
		private System.Windows.Forms.RadioButton radioButtonAllFiles;
		private DevComponents.DotNetBar.PanelEx panelOptions;
	}
}