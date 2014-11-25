namespace ArchAngel.Designer
{
    partial class frmTestFunction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestFunction));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.tabStrip1 = new ActiproSoftware.UIStudio.TabStrip.TabStrip();
            this.tabOutput = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tabFormatted = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.syntaxEditorFormatted = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.ucHeading2 = new Slyce.Common.Controls.ucHeading();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblPageDescription = new System.Windows.Forms.Label();
            this.lblPageHeader = new System.Windows.Forms.Label();
            this.ucHeading3 = new Slyce.Common.Controls.ucHeading();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).BeginInit();
            this.tabStrip1.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabFormatted.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(59)))), ((int)(((byte)(150)))));
            this.splitContainer1.Location = new System.Drawing.Point(-1, 45);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.ucHeading1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.ucHeading2);
            this.splitContainer1.Size = new System.Drawing.Size(872, 382);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 362);
            this.panel1.TabIndex = 1;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeading1.Location = new System.Drawing.Point(0, 0);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(209, 20);
            this.ucHeading1.TabIndex = 0;
            // 
            // tabStrip1
            // 
            this.tabStrip1.AutoSetFocusOnClick = true;
            this.tabStrip1.Controls.Add(this.tabOutput);
            this.tabStrip1.Controls.Add(this.tabFormatted);
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStrip1.Location = new System.Drawing.Point(0, 20);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Size = new System.Drawing.Size(660, 362);
            this.tabStrip1.TabIndex = 15;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // tabOutput
            // 
            this.tabOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabOutput.Controls.Add(this.syntaxEditor1);
            this.tabOutput.Key = "TabStripPage";
            this.tabOutput.Location = new System.Drawing.Point(0, 21);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Size = new System.Drawing.Size(660, 341);
            this.tabOutput.TabIndex = 0;
            this.tabOutput.Text = "Raw Output";
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxEditor1.Document = document1;
            this.syntaxEditor1.LineNumberMarginVisible = true;
            this.syntaxEditor1.Location = new System.Drawing.Point(0, 0);
            this.syntaxEditor1.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(660, 341);
            this.syntaxEditor1.TabIndex = 14;
            // 
            // tabFormatted
            // 
            this.tabFormatted.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabFormatted.Controls.Add(this.syntaxEditorFormatted);
            this.tabFormatted.Key = "TabStripPage";
            this.tabFormatted.Location = new System.Drawing.Point(0, 21);
            this.tabFormatted.Name = "tabFormatted";
            this.tabFormatted.Size = new System.Drawing.Size(660, 341);
            this.tabFormatted.TabIndex = 1;
            this.tabFormatted.Text = "Formatted Output";
            // 
            // syntaxEditorFormatted
            // 
            this.syntaxEditorFormatted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxEditorFormatted.Document = document2;
            this.syntaxEditorFormatted.LineNumberMarginVisible = true;
            this.syntaxEditorFormatted.Location = new System.Drawing.Point(0, 0);
            this.syntaxEditorFormatted.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxEditorFormatted.Name = "syntaxEditorFormatted";
            this.syntaxEditorFormatted.Size = new System.Drawing.Size(660, 341);
            this.syntaxEditorFormatted.TabIndex = 15;
            // 
            // ucHeading2
            // 
            this.ucHeading2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeading2.Location = new System.Drawing.Point(0, 0);
            this.ucHeading2.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading2.Name = "ucHeading2";
            this.ucHeading2.Size = new System.Drawing.Size(660, 20);
            this.ucHeading2.TabIndex = 14;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(742, 432);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(48, 24);
            this.btnRun.TabIndex = 14;
            this.btnRun.Text = "    &Run";
            this.btnRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(804, 432);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 24);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Controls.Add(this.lblPageDescription);
            this.pnlTop.Controls.Add(this.lblPageHeader);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(871, 46);
            this.pnlTop.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblPageDescription
            // 
            this.lblPageDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageDescription.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPageDescription.Location = new System.Drawing.Point(173, 22);
            this.lblPageDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPageDescription.Name = "lblPageDescription";
            this.lblPageDescription.Size = new System.Drawing.Size(698, 24);
            this.lblPageDescription.TabIndex = 1;
            this.lblPageDescription.Text = "lblPageDescription";
            // 
            // lblPageHeader
            // 
            this.lblPageHeader.AutoSize = true;
            this.lblPageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageHeader.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPageHeader.Location = new System.Drawing.Point(154, 7);
            this.lblPageHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPageHeader.Name = "lblPageHeader";
            this.lblPageHeader.Size = new System.Drawing.Size(103, 15);
            this.lblPageHeader.TabIndex = 0;
            this.lblPageHeader.Text = "lblPageHeader";
            // 
            // ucHeading3
            // 
            this.ucHeading3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucHeading3.Location = new System.Drawing.Point(0, 426);
            this.ucHeading3.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading3.Name = "ucHeading3";
            this.ucHeading3.Size = new System.Drawing.Size(871, 34);
            this.ucHeading3.TabIndex = 17;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmTestFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(871, 460);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ucHeading3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(671, 404);
            this.Name = "frmTestFunction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test Function";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTestFunction_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmTestFunction_Paint);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTestFunction_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).EndInit();
            this.tabStrip1.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabFormatted.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private Slyce.Common.Controls.ucHeading ucHeading2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPageDescription;
        private System.Windows.Forms.Label lblPageHeader;
        private Slyce.Common.Controls.ucHeading ucHeading3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ActiproSoftware.UIStudio.TabStrip.TabStrip tabStrip1;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabOutput;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabFormatted;
        internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
        internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorFormatted;
    }
}