namespace ArchAngel.Workbench
{
    partial class FormMergeEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMergeEditor));
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxFileType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonSwitchView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPrevConflict = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextConflict = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAcceptUserChanges = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAcceptTemplateChanges = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.syntaxEditor2 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.ucBinaryFileViewer1 = new ArchAngel.IntelliMerge.ucBinaryFileViewer();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.syntaxEditor1.Document = document1;
            this.syntaxEditor1.LineNumberMarginVisible = true;
            this.syntaxEditor1.Location = new System.Drawing.Point(0, 51);
            this.syntaxEditor1.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(814, 326);
            this.syntaxEditor1.TabIndex = 0;
            this.syntaxEditor1.UserMarginVisible = true;
            this.syntaxEditor1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.syntaxEditor1_MouseClick);
            this.syntaxEditor1.DocumentTextChanged += new ActiproSoftware.SyntaxEditor.DocumentModificationEventHandler(this.syntaxEditor1_DocumentTextChanged);
            this.syntaxEditor1.SelectionChanged += new ActiproSoftware.SyntaxEditor.SelectionEventHandler(this.syntaxEditor1_SelectionChanged);
            this.syntaxEditor1.ContextMenuRequested += new ActiproSoftware.SyntaxEditor.ContextMenuRequestEventHandler(this.syntaxEditor1_ContextMenuRequested);
            this.syntaxEditor1.UserMarginPaint += new ActiproSoftware.SyntaxEditor.UserMarginPaintEventHandler(this.syntaxEditor1_UserMarginPaint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(813, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxFileType,
            this.toolStripButtonSwitchView,
            this.toolStripSeparator2,
            this.toolStripButtonPrevConflict,
            this.toolStripButtonNextConflict,
            this.toolStripSeparator1,
            this.toolStripButtonAcceptUserChanges,
            this.toolStripButtonAcceptTemplateChanges,
            this.toolStripButtonHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBoxFileType
            // 
            this.toolStripComboBoxFileType.Items.AddRange(new object[] {
            "Batch file",
            "C#",
            "CSS",
            "HTML",
            "INI file",
            "Java",
            "JScript",
            "Perl",
            "PHP",
            "Plain Text",
            "Python",
            "T-SQL",
            "VB.net",
            "VBScript",
            "XML"});
            this.toolStripComboBoxFileType.Name = "toolStripComboBoxFileType";
            this.toolStripComboBoxFileType.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBoxFileType.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxFileType_SelectedIndexChanged);
            this.toolStripComboBoxFileType.Click += new System.EventHandler(this.toolStripComboBoxFileType_Click);
            // 
            // toolStripButtonSwitchView
            // 
            this.toolStripButtonSwitchView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSwitchView.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSwitchView.Image")));
            this.toolStripButtonSwitchView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSwitchView.Name = "toolStripButtonSwitchView";
            this.toolStripButtonSwitchView.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSwitchView.Text = "toolStripButton1";
            this.toolStripButtonSwitchView.ToolTipText = "Switch view style";
            this.toolStripButtonSwitchView.Click += new System.EventHandler(this.toolStripButtonSwitchView_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPrevConflict
            // 
            this.toolStripButtonPrevConflict.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevConflict.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrevConflict.Image")));
            this.toolStripButtonPrevConflict.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevConflict.Name = "toolStripButtonPrevConflict";
            this.toolStripButtonPrevConflict.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPrevConflict.Text = "toolStripButton1";
            this.toolStripButtonPrevConflict.ToolTipText = "Goto previous conflict";
            this.toolStripButtonPrevConflict.Click += new System.EventHandler(this.toolStripButtonPrevConflict_Click);
            // 
            // toolStripButtonNextConflict
            // 
            this.toolStripButtonNextConflict.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextConflict.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextConflict.Image")));
            this.toolStripButtonNextConflict.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextConflict.Name = "toolStripButtonNextConflict";
            this.toolStripButtonNextConflict.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNextConflict.Text = "toolStripButton1";
            this.toolStripButtonNextConflict.ToolTipText = "Goto next conflict";
            this.toolStripButtonNextConflict.Click += new System.EventHandler(this.toolStripButtonNextConflict_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAcceptUserChanges
            // 
            this.toolStripButtonAcceptUserChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAcceptUserChanges.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAcceptUserChanges.Image")));
            this.toolStripButtonAcceptUserChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAcceptUserChanges.Name = "toolStripButtonAcceptUserChanges";
            this.toolStripButtonAcceptUserChanges.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAcceptUserChanges.Text = "toolStripButton1";
            this.toolStripButtonAcceptUserChanges.ToolTipText = "Accept all user changes, ignore all template changes";
            this.toolStripButtonAcceptUserChanges.Click += new System.EventHandler(this.toolStripButtonAcceptUserChanges_Click);
            // 
            // toolStripButtonAcceptTemplateChanges
            // 
            this.toolStripButtonAcceptTemplateChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAcceptTemplateChanges.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAcceptTemplateChanges.Image")));
            this.toolStripButtonAcceptTemplateChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAcceptTemplateChanges.Name = "toolStripButtonAcceptTemplateChanges";
            this.toolStripButtonAcceptTemplateChanges.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAcceptTemplateChanges.Text = "toolStripButton1";
            this.toolStripButtonAcceptTemplateChanges.ToolTipText = "Accept all template changes, ignore all user changes";
            this.toolStripButtonAcceptTemplateChanges.Click += new System.EventHandler(this.toolStripButtonAcceptTemplateChanges_Click);
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHelp.Image")));
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonHelp.Text = "toolStripButton1";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(743, 407);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(59, 20);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "&Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(676, 407);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(59, 20);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "&OK";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAccept,
            this.mnuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // mnuAccept
            // 
            this.mnuAccept.Image = ((System.Drawing.Image)(resources.GetObject("mnuAccept.Image")));
            this.mnuAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAccept.Name = "mnuAccept";
            this.mnuAccept.Size = new System.Drawing.Size(118, 22);
            this.mnuAccept.Text = "&Accept";
            this.mnuAccept.Click += new System.EventHandler(this.mnuAccept_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuDelete.Image")));
            this.mnuDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(118, 22);
            this.mnuDelete.Text = "&Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // syntaxEditor2
            // 
            this.syntaxEditor2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.syntaxEditor2.Document = document2;
            this.syntaxEditor2.LineNumberMarginVisible = true;
            this.syntaxEditor2.Location = new System.Drawing.Point(488, 40);
            this.syntaxEditor2.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxEditor2.Name = "syntaxEditor2";
            this.syntaxEditor2.Size = new System.Drawing.Size(120, 52);
            this.syntaxEditor2.TabIndex = 7;
            this.syntaxEditor2.UserMarginVisible = true;
            this.syntaxEditor2.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "application.png");
            this.imageList1.Images.SetKeyName(1, "application_tile_horizontal.png");
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 404);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(813, 28);
            this.ucHeading1.TabIndex = 8;
            // 
            // ucBinaryFileViewer1
            // 
            this.ucBinaryFileViewer1.Location = new System.Drawing.Point(0, 35);
            this.ucBinaryFileViewer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucBinaryFileViewer1.Name = "ucBinaryFileViewer1";
            this.ucBinaryFileViewer1.Size = new System.Drawing.Size(108, 20);
            this.ucBinaryFileViewer1.TabIndex = 5;
            // 
            // FormMergeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(813, 432);
            this.Controls.Add(this.syntaxEditor2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.syntaxEditor1);
            this.Controls.Add(this.ucBinaryFileViewer1);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMergeEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxFileType;
        private ArchAngel.IntelliMerge.ucBinaryFileViewer ucBinaryFileViewer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuAccept;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor2;
        private System.Windows.Forms.ToolStripButton toolStripButtonSwitchView;
        private System.Windows.Forms.ImageList imageList1;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextConflict;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrevConflict;
        private System.Windows.Forms.ToolStripButton toolStripButtonAcceptUserChanges;
        private System.Windows.Forms.ToolStripButton toolStripButtonAcceptTemplateChanges;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
    }
}