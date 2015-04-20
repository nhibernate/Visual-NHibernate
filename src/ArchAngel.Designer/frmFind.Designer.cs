namespace ArchAngel.Designer
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
            this.ddlScope = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkFindMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.chkFindUse = new System.Windows.Forms.CheckBox();
            this.chkFindSearchUp = new System.Windows.Forms.CheckBox();
            this.chkFindMatchCase = new System.Windows.Forms.CheckBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.chkScopeScript = new System.Windows.Forms.RadioButton();
            this.chkScopeBoth = new System.Windows.Forms.RadioButton();
            this.chkScopeOutput = new System.Windows.Forms.RadioButton();
            this.btnFindAll = new System.Windows.Forms.Button();
            this.grouper1 = new Slyce.Common.Controls.Grouper();
            this.grouper2 = new Slyce.Common.Controls.Grouper();
            this.grouper1.SuspendLayout();
            this.grouper2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Find what:";
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(7, 46);
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
            this.ddlFind.Location = new System.Drawing.Point(9, 24);
            this.ddlFind.Margin = new System.Windows.Forms.Padding(2);
            this.ddlFind.Name = "ddlFind";
            this.ddlFind.Size = new System.Drawing.Size(242, 21);
            this.ddlFind.TabIndex = 0;
            this.ddlFind.SelectedIndexChanged += new System.EventHandler(this.ddlFind_SelectedIndexChanged);
            this.ddlFind.TextChanged += new System.EventHandler(this.ddlFind_TextChanged);
            // 
            // ddlReplace
            // 
            this.ddlReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlReplace.FormattingEnabled = true;
            this.ddlReplace.Location = new System.Drawing.Point(9, 62);
            this.ddlReplace.Margin = new System.Windows.Forms.Padding(2);
            this.ddlReplace.Name = "ddlReplace";
            this.ddlReplace.Size = new System.Drawing.Size(242, 21);
            this.ddlReplace.TabIndex = 1;
            this.ddlReplace.SelectedIndexChanged += new System.EventHandler(this.ddlReplace_SelectedIndexChanged);
            this.ddlReplace.TextChanged += new System.EventHandler(this.ddlReplace_TextChanged);
            // 
            // ddlScope
            // 
            this.ddlScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlScope.FormattingEnabled = true;
            this.ddlScope.Items.AddRange(new object[] {
            "Current function",
            "All open functions",
            "All functions"});
            this.ddlScope.Location = new System.Drawing.Point(9, 100);
            this.ddlScope.Margin = new System.Windows.Forms.Padding(2);
            this.ddlScope.Name = "ddlScope";
            this.ddlScope.Size = new System.Drawing.Size(242, 21);
            this.ddlScope.TabIndex = 2;
            this.ddlScope.SelectedIndexChanged += new System.EventHandler(this.ddlScope_SelectedIndexChanged);
            this.ddlScope.TextChanged += new System.EventHandler(this.ddlScope_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Look in:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(199, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 19);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSearch.Location = new System.Drawing.Point(9, 346);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 26);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "&Find Next";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkFindMatchWholeWord
            // 
            this.chkFindMatchWholeWord.AutoSize = true;
            this.chkFindMatchWholeWord.Location = new System.Drawing.Point(9, 50);
            this.chkFindMatchWholeWord.Margin = new System.Windows.Forms.Padding(2);
            this.chkFindMatchWholeWord.Name = "chkFindMatchWholeWord";
            this.chkFindMatchWholeWord.Size = new System.Drawing.Size(113, 17);
            this.chkFindMatchWholeWord.TabIndex = 7;
            this.chkFindMatchWholeWord.Text = "Match whole word";
            this.chkFindMatchWholeWord.UseVisualStyleBackColor = true;
            this.chkFindMatchWholeWord.CheckedChanged += new System.EventHandler(this.chkFindMatchWholeWord_CheckedChanged);
            // 
            // chkFindUse
            // 
            this.chkFindUse.AutoSize = true;
            this.chkFindUse.Location = new System.Drawing.Point(9, 94);
            this.chkFindUse.Margin = new System.Windows.Forms.Padding(2);
            this.chkFindUse.Name = "chkFindUse";
            this.chkFindUse.Size = new System.Drawing.Size(48, 17);
            this.chkFindUse.TabIndex = 9;
            this.chkFindUse.Text = "Use:";
            this.chkFindUse.UseVisualStyleBackColor = true;
            this.chkFindUse.CheckedChanged += new System.EventHandler(this.chkFindUse_CheckedChanged);
            // 
            // chkFindSearchUp
            // 
            this.chkFindSearchUp.AutoSize = true;
            this.chkFindSearchUp.Location = new System.Drawing.Point(9, 72);
            this.chkFindSearchUp.Margin = new System.Windows.Forms.Padding(2);
            this.chkFindSearchUp.Name = "chkFindSearchUp";
            this.chkFindSearchUp.Size = new System.Drawing.Size(75, 17);
            this.chkFindSearchUp.TabIndex = 8;
            this.chkFindSearchUp.Text = "Search up";
            this.chkFindSearchUp.UseVisualStyleBackColor = true;
            this.chkFindSearchUp.CheckedChanged += new System.EventHandler(this.chkFindSearchUp_CheckedChanged);
            // 
            // chkFindMatchCase
            // 
            this.chkFindMatchCase.AutoSize = true;
            this.chkFindMatchCase.Location = new System.Drawing.Point(9, 28);
            this.chkFindMatchCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkFindMatchCase.Name = "chkFindMatchCase";
            this.chkFindMatchCase.Size = new System.Drawing.Size(82, 17);
            this.chkFindMatchCase.TabIndex = 6;
            this.chkFindMatchCase.Text = "Match case";
            this.chkFindMatchCase.UseVisualStyleBackColor = true;
            this.chkFindMatchCase.CheckedChanged += new System.EventHandler(this.chkFindMatchCase_CheckedChanged);
            // 
            // btnReplace
            // 
            this.btnReplace.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReplace.Location = new System.Drawing.Point(133, 346);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(92, 26);
            this.btnReplace.TabIndex = 12;
            this.btnReplace.Text = "&Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReplaceAll.Location = new System.Drawing.Point(133, 375);
            this.btnReplaceAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(92, 26);
            this.btnReplaceAll.TabIndex = 13;
            this.btnReplaceAll.Text = "Replace &All";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // chkScopeScript
            // 
            this.chkScopeScript.AutoSize = true;
            this.chkScopeScript.Location = new System.Drawing.Point(9, 29);
            this.chkScopeScript.Margin = new System.Windows.Forms.Padding(2);
            this.chkScopeScript.Name = "chkScopeScript";
            this.chkScopeScript.Size = new System.Drawing.Size(74, 17);
            this.chkScopeScript.TabIndex = 3;
            this.chkScopeScript.TabStop = true;
            this.chkScopeScript.Text = "Script only";
            this.chkScopeScript.UseVisualStyleBackColor = true;
            this.chkScopeScript.CheckedChanged += new System.EventHandler(this.chkScopeScript_CheckedChanged);
            // 
            // chkScopeBoth
            // 
            this.chkScopeBoth.AutoSize = true;
            this.chkScopeBoth.Checked = true;
            this.chkScopeBoth.Location = new System.Drawing.Point(9, 63);
            this.chkScopeBoth.Margin = new System.Windows.Forms.Padding(2);
            this.chkScopeBoth.Name = "chkScopeBoth";
            this.chkScopeBoth.Size = new System.Drawing.Size(47, 17);
            this.chkScopeBoth.TabIndex = 5;
            this.chkScopeBoth.TabStop = true;
            this.chkScopeBoth.Text = "Both";
            this.chkScopeBoth.UseVisualStyleBackColor = true;
            this.chkScopeBoth.CheckedChanged += new System.EventHandler(this.chkScopeBoth_CheckedChanged);
            // 
            // chkScopeOutput
            // 
            this.chkScopeOutput.AutoSize = true;
            this.chkScopeOutput.Location = new System.Drawing.Point(9, 46);
            this.chkScopeOutput.Margin = new System.Windows.Forms.Padding(2);
            this.chkScopeOutput.Name = "chkScopeOutput";
            this.chkScopeOutput.Size = new System.Drawing.Size(79, 17);
            this.chkScopeOutput.TabIndex = 4;
            this.chkScopeOutput.TabStop = true;
            this.chkScopeOutput.Text = "Output only";
            this.chkScopeOutput.UseVisualStyleBackColor = true;
            this.chkScopeOutput.CheckedChanged += new System.EventHandler(this.chkScopeOutput_CheckedChanged);
            // 
            // btnFindAll
            // 
            this.btnFindAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFindAll.Location = new System.Drawing.Point(9, 375);
            this.btnFindAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Size = new System.Drawing.Size(92, 26);
            this.btnFindAll.TabIndex = 11;
            this.btnFindAll.Text = "Find &All";
            this.btnFindAll.UseVisualStyleBackColor = true;
            this.btnFindAll.Click += new System.EventHandler(this.btnFindAll_Click);
            // 
            // grouper1
            // 
            this.grouper1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grouper1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientMode = Slyce.Common.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 0F;
            this.grouper1.Controls.Add(this.chkFindMatchWholeWord);
            this.grouper1.Controls.Add(this.chkFindMatchCase);
            this.grouper1.Controls.Add(this.chkFindUse);
            this.grouper1.Controls.Add(this.chkFindSearchUp);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Find Options";
            this.grouper1.Location = new System.Drawing.Point(10, 220);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(56)))), ((int)(((byte)(153)))));
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowControlForTitle = true;
            this.grouper1.ShadowThickness = 2;
            this.grouper1.Size = new System.Drawing.Size(241, 121);
            this.grouper1.TabIndex = 17;
            // 
            // grouper2
            // 
            this.grouper2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grouper2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper2.BackgroundGradientMode = Slyce.Common.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 0F;
            this.grouper2.Controls.Add(this.chkScopeOutput);
            this.grouper2.Controls.Add(this.chkScopeScript);
            this.grouper2.Controls.Add(this.chkScopeBoth);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Scope";
            this.grouper2.Location = new System.Drawing.Point(9, 126);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 10;
            this.grouper2.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(56)))), ((int)(((byte)(153)))));
            this.grouper2.ShadowControl = true;
            this.grouper2.ShadowControlForTitle = true;
            this.grouper2.ShadowThickness = 2;
            this.grouper2.Size = new System.Drawing.Size(242, 89);
            this.grouper2.TabIndex = 20;
            // 
            // frmFind
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(258, 430);
            this.Controls.Add(this.grouper2);
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.btnFindAll);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ddlScope);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddlReplace);
            this.Controls.Add(this.ddlFind);
            this.Controls.Add(this.lblReplace);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmFind";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFind_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmFind_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFind_KeyDown);
            this.Load += new System.EventHandler(this.frmFind_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.grouper2.ResumeLayout(false);
            this.grouper2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblReplace;
		private System.Windows.Forms.ComboBox ddlFind;
		private System.Windows.Forms.ComboBox ddlReplace;
		private System.Windows.Forms.ComboBox ddlScope;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.CheckBox chkFindMatchWholeWord;
		private System.Windows.Forms.CheckBox chkFindUse;
		private System.Windows.Forms.CheckBox chkFindSearchUp;
		private System.Windows.Forms.CheckBox chkFindMatchCase;
		private System.Windows.Forms.Button btnReplace;
		private System.Windows.Forms.Button btnReplaceAll;
		private System.Windows.Forms.RadioButton chkScopeScript;
		private System.Windows.Forms.RadioButton chkScopeBoth;
        private System.Windows.Forms.RadioButton chkScopeOutput;
        private System.Windows.Forms.Button btnFindAll;
        private Slyce.Common.Controls.Grouper grouper1;
        private Slyce.Common.Controls.Grouper grouper2;
	}
}