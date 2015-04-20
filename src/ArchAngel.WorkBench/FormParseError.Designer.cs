namespace ArchAngel.Workbench
{
    partial class FormParseError
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParseError));
            this.textBoxSetupModelTemplateFileName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonSetupModelTemplateFileName = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.ucHeading2 = new Slyce.Common.Controls.ucHeading();
            this.buttonClose2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSetupModelTemplateFileName
            // 
            this.textBoxSetupModelTemplateFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSetupModelTemplateFileName.Location = new System.Drawing.Point(10, 45);
            this.textBoxSetupModelTemplateFileName.Name = "textBoxSetupModelTemplateFileName";
            this.textBoxSetupModelTemplateFileName.ReadOnly = true;
            this.textBoxSetupModelTemplateFileName.Size = new System.Drawing.Size(330, 20);
            this.textBoxSetupModelTemplateFileName.TabIndex = 10;
            this.toolTip1.SetToolTip(this.textBoxSetupModelTemplateFileName, "The template used for determining how databases will be interpreted. You should u" +
                    "se the default if you are unsure about this option.");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(10, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(178, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Database Schema Update Template";
            // 
            // buttonSetupModelTemplateFileName
            // 
            this.buttonSetupModelTemplateFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetupModelTemplateFileName.Location = new System.Drawing.Point(346, 43);
            this.buttonSetupModelTemplateFileName.Name = "buttonSetupModelTemplateFileName";
            this.buttonSetupModelTemplateFileName.Size = new System.Drawing.Size(27, 23);
            this.buttonSetupModelTemplateFileName.TabIndex = 11;
            this.buttonSetupModelTemplateFileName.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(529, 303);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(66, 26);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 298);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(601, 35);
            this.ucHeading1.TabIndex = 13;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.syntaxEditor1.Document = document1;
            this.syntaxEditor1.LineNumberMarginVisible = true;
            this.syntaxEditor1.Location = new System.Drawing.Point(229, 62);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(753, 346);
            this.syntaxEditor1.TabIndex = 0;
            // 
            // ucHeading2
            // 
            this.ucHeading2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading2.Location = new System.Drawing.Point(0, 413);
            this.ucHeading2.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading2.Name = "ucHeading2";
            this.ucHeading2.Size = new System.Drawing.Size(988, 31);
            this.ucHeading2.TabIndex = 1;
            // 
            // buttonClose2
            // 
            this.buttonClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose2.Location = new System.Drawing.Point(907, 417);
            this.buttonClose2.Name = "buttonClose2";
            this.buttonClose2.Size = new System.Drawing.Size(75, 23);
            this.buttonClose2.TabIndex = 3;
            this.buttonClose2.Text = "Close";
            this.buttonClose2.UseVisualStyleBackColor = true;
            this.buttonClose2.Click += new System.EventHandler(this.buttonClose2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(226, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(756, 50);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(5, 31);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(215, 368);
            this.listBoxFiles.TabIndex = 5;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Click file to view error";
            // 
            // FormParseError
            // 
            this.CancelButton = this.buttonClose2;
            this.ClientSize = new System.Drawing.Size(988, 444);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClose2);
            this.Controls.Add(this.ucHeading2);
            this.Controls.Add(this.syntaxEditor1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormParseError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parse Error";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParseError_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSetupModelTemplateFileName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonSetupModelTemplateFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Button buttonClose2;
        private Slyce.Common.Controls.ucHeading ucHeading2;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Label label2;
    }
}
