namespace ArchAngel.Workbench.ContentItems
{
    partial class SetupDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDatabase));
            this.buttonStoredProcedurePrefixAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonViewPrefixAdd = new System.Windows.Forms.Button();
            this.buttonTablePrefixAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAddDatabase = new System.Windows.Forms.Button();
            this.listBoxDatabase = new System.Windows.Forms.ListBox();
            this.textBoxTablePrefix = new System.Windows.Forms.TextBox();
            this.listBoxTablePrefix = new System.Windows.Forms.ListBox();
            this.listBoxStoredProcedurePrefix = new System.Windows.Forms.ListBox();
            this.textBoxViewPrefix = new System.Windows.Forms.TextBox();
            this.textBoxStoredProcedurePrefix = new System.Windows.Forms.TextBox();
            this.listBoxViewPrefix = new System.Windows.Forms.ListBox();
            this.contextMenuStripPrefix = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeletePrefix = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDatabase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDatabaseDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.buttonTablePrefixDelete = new System.Windows.Forms.Button();
            this.buttonViewPrefixDelete = new System.Windows.Forms.Button();
            this.buttonStoredProcedurePrefixDelete = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStripPrefix.SuspendLayout();
            this.contextMenuStripDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStoredProcedurePrefixAdd
            // 
            this.buttonStoredProcedurePrefixAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStoredProcedurePrefixAdd.ImageIndex = 0;
            this.buttonStoredProcedurePrefixAdd.ImageList = this.imageList1;
            this.buttonStoredProcedurePrefixAdd.Location = new System.Drawing.Point(131, 17);
            this.buttonStoredProcedurePrefixAdd.Name = "buttonStoredProcedurePrefixAdd";
            this.buttonStoredProcedurePrefixAdd.Size = new System.Drawing.Size(24, 24);
            this.buttonStoredProcedurePrefixAdd.TabIndex = 15;
            this.buttonStoredProcedurePrefixAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonStoredProcedurePrefixAdd, "Add prefix to the list");
            this.buttonStoredProcedurePrefixAdd.UseVisualStyleBackColor = true;
            this.buttonStoredProcedurePrefixAdd.Click += new System.EventHandler(this.buttonStoredProcedurePrefixAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "AddTableHS.png");
            this.imageList1.Images.SetKeyName(1, "EditInformationHS.png");
            this.imageList1.Images.SetKeyName(2, "delete.bmp");
            // 
            // buttonViewPrefixAdd
            // 
            this.buttonViewPrefixAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonViewPrefixAdd.ImageIndex = 0;
            this.buttonViewPrefixAdd.ImageList = this.imageList1;
            this.buttonViewPrefixAdd.Location = new System.Drawing.Point(131, 17);
            this.buttonViewPrefixAdd.Name = "buttonViewPrefixAdd";
            this.buttonViewPrefixAdd.Size = new System.Drawing.Size(24, 24);
            this.buttonViewPrefixAdd.TabIndex = 11;
            this.buttonViewPrefixAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonViewPrefixAdd, "Add prefix to the list");
            this.buttonViewPrefixAdd.UseVisualStyleBackColor = true;
            this.buttonViewPrefixAdd.Click += new System.EventHandler(this.buttonViewPrefixAdd_Click);
            // 
            // buttonTablePrefixAdd
            // 
            this.buttonTablePrefixAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTablePrefixAdd.ImageIndex = 0;
            this.buttonTablePrefixAdd.ImageList = this.imageList1;
            this.buttonTablePrefixAdd.Location = new System.Drawing.Point(128, 17);
            this.buttonTablePrefixAdd.Name = "buttonTablePrefixAdd";
            this.buttonTablePrefixAdd.Size = new System.Drawing.Size(24, 24);
            this.buttonTablePrefixAdd.TabIndex = 7;
            this.buttonTablePrefixAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonTablePrefixAdd, "Add prefix to the list");
            this.buttonTablePrefixAdd.UseVisualStyleBackColor = true;
            this.buttonTablePrefixAdd.Click += new System.EventHandler(this.buttonTablePrefixAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEdit.ImageIndex = 1;
            this.buttonEdit.ImageList = this.imageList1;
            this.buttonEdit.Location = new System.Drawing.Point(128, 91);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(24, 24);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonEdit, "Edit");
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.ImageIndex = 2;
            this.buttonRemove.ImageList = this.imageList1;
            this.buttonRemove.Location = new System.Drawing.Point(128, 122);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(24, 24);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonRemove, "Remove");
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAddDatabase
            // 
            this.buttonAddDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddDatabase.ImageIndex = 0;
            this.buttonAddDatabase.ImageList = this.imageList1;
            this.buttonAddDatabase.Location = new System.Drawing.Point(128, 60);
            this.buttonAddDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddDatabase.Name = "buttonAddDatabase";
            this.buttonAddDatabase.Size = new System.Drawing.Size(24, 24);
            this.buttonAddDatabase.TabIndex = 4;
            this.buttonAddDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonAddDatabase, "Add another database to the list");
            this.buttonAddDatabase.UseVisualStyleBackColor = true;
            this.buttonAddDatabase.Click += new System.EventHandler(this.buttonAddDatabase_Click);
            // 
            // listBoxDatabase
            // 
            this.listBoxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDatabase.FormattingEnabled = true;
            this.listBoxDatabase.Location = new System.Drawing.Point(3, 19);
            this.listBoxDatabase.Name = "listBoxDatabase";
            this.listBoxDatabase.Size = new System.Drawing.Size(120, 134);
            this.listBoxDatabase.TabIndex = 2;
            this.listBoxDatabase.Validating += new System.ComponentModel.CancelEventHandler(this.listBoxDatabase_Validating);
            this.listBoxDatabase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxDatabase_MouseUp);
            // 
            // textBoxTablePrefix
            // 
            this.textBoxTablePrefix.Location = new System.Drawing.Point(3, 18);
            this.textBoxTablePrefix.Name = "textBoxTablePrefix";
            this.textBoxTablePrefix.Size = new System.Drawing.Size(120, 20);
            this.textBoxTablePrefix.TabIndex = 6;
            // 
            // listBoxTablePrefix
            // 
            this.listBoxTablePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTablePrefix.FormattingEnabled = true;
            this.listBoxTablePrefix.Location = new System.Drawing.Point(3, 44);
            this.listBoxTablePrefix.Name = "listBoxTablePrefix";
            this.listBoxTablePrefix.Size = new System.Drawing.Size(120, 108);
            this.listBoxTablePrefix.TabIndex = 8;
            this.listBoxTablePrefix.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxTablePrefix_MouseUp);
            // 
            // listBoxStoredProcedurePrefix
            // 
            this.listBoxStoredProcedurePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxStoredProcedurePrefix.FormattingEnabled = true;
            this.listBoxStoredProcedurePrefix.Location = new System.Drawing.Point(5, 44);
            this.listBoxStoredProcedurePrefix.Name = "listBoxStoredProcedurePrefix";
            this.listBoxStoredProcedurePrefix.Size = new System.Drawing.Size(120, 108);
            this.listBoxStoredProcedurePrefix.TabIndex = 16;
            this.listBoxStoredProcedurePrefix.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxStoredProcedurePrefix_MouseUp);
            // 
            // textBoxViewPrefix
            // 
            this.textBoxViewPrefix.Location = new System.Drawing.Point(5, 18);
            this.textBoxViewPrefix.Name = "textBoxViewPrefix";
            this.textBoxViewPrefix.Size = new System.Drawing.Size(120, 20);
            this.textBoxViewPrefix.TabIndex = 10;
            // 
            // textBoxStoredProcedurePrefix
            // 
            this.textBoxStoredProcedurePrefix.Location = new System.Drawing.Point(5, 18);
            this.textBoxStoredProcedurePrefix.Name = "textBoxStoredProcedurePrefix";
            this.textBoxStoredProcedurePrefix.Size = new System.Drawing.Size(120, 20);
            this.textBoxStoredProcedurePrefix.TabIndex = 14;
            // 
            // listBoxViewPrefix
            // 
            this.listBoxViewPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxViewPrefix.FormattingEnabled = true;
            this.listBoxViewPrefix.Location = new System.Drawing.Point(5, 44);
            this.listBoxViewPrefix.Name = "listBoxViewPrefix";
            this.listBoxViewPrefix.Size = new System.Drawing.Size(120, 108);
            this.listBoxViewPrefix.TabIndex = 12;
            this.listBoxViewPrefix.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxViewPrefix_MouseUp);
            // 
            // contextMenuStripPrefix
            // 
            this.contextMenuStripPrefix.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeletePrefix});
            this.contextMenuStripPrefix.Name = "contextMenuStripPrefix";
            this.contextMenuStripPrefix.Size = new System.Drawing.Size(117, 26);
            // 
            // toolStripMenuItemDeletePrefix
            // 
            this.toolStripMenuItemDeletePrefix.Name = "toolStripMenuItemDeletePrefix";
            this.toolStripMenuItemDeletePrefix.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemDeletePrefix.Text = "&Delete";
            this.toolStripMenuItemDeletePrefix.Click += new System.EventHandler(this.toolStripMenuItemDeletePrefix_Click);
            // 
            // contextMenuStripDatabase
            // 
            this.contextMenuStripDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatabaseDelete});
            this.contextMenuStripDatabase.Name = "contextMenuStripDatabase";
            this.contextMenuStripDatabase.Size = new System.Drawing.Size(117, 26);
            // 
            // toolStripMenuItemDatabaseDelete
            // 
            this.toolStripMenuItemDatabaseDelete.Name = "toolStripMenuItemDatabaseDelete";
            this.toolStripMenuItemDatabaseDelete.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemDatabaseDelete.Text = "&Delete";
            this.toolStripMenuItemDatabaseDelete.Click += new System.EventHandler(this.toolStripMenuItemDatabaseDelete_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(209, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(408, 117);
            this.label4.TabIndex = 17;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // buttonTablePrefixDelete
            // 
            this.buttonTablePrefixDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTablePrefixDelete.ImageIndex = 2;
            this.buttonTablePrefixDelete.ImageList = this.imageList1;
            this.buttonTablePrefixDelete.Location = new System.Drawing.Point(128, 50);
            this.buttonTablePrefixDelete.Name = "buttonTablePrefixDelete";
            this.buttonTablePrefixDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonTablePrefixDelete.TabIndex = 18;
            this.buttonTablePrefixDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonTablePrefixDelete, "Remove");
            this.buttonTablePrefixDelete.UseVisualStyleBackColor = true;
            this.buttonTablePrefixDelete.Click += new System.EventHandler(this.buttonTablePrefixDelete_Click);
            // 
            // buttonViewPrefixDelete
            // 
            this.buttonViewPrefixDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonViewPrefixDelete.ImageIndex = 2;
            this.buttonViewPrefixDelete.ImageList = this.imageList1;
            this.buttonViewPrefixDelete.Location = new System.Drawing.Point(131, 50);
            this.buttonViewPrefixDelete.Name = "buttonViewPrefixDelete";
            this.buttonViewPrefixDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonViewPrefixDelete.TabIndex = 19;
            this.buttonViewPrefixDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonViewPrefixDelete, "Remove");
            this.buttonViewPrefixDelete.UseVisualStyleBackColor = true;
            this.buttonViewPrefixDelete.Click += new System.EventHandler(this.buttonViewPrefixDelete_Click);
            // 
            // buttonStoredProcedurePrefixDelete
            // 
            this.buttonStoredProcedurePrefixDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStoredProcedurePrefixDelete.ImageIndex = 2;
            this.buttonStoredProcedurePrefixDelete.ImageList = this.imageList1;
            this.buttonStoredProcedurePrefixDelete.Location = new System.Drawing.Point(131, 50);
            this.buttonStoredProcedurePrefixDelete.Name = "buttonStoredProcedurePrefixDelete";
            this.buttonStoredProcedurePrefixDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonStoredProcedurePrefixDelete.TabIndex = 20;
            this.buttonStoredProcedurePrefixDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonStoredProcedurePrefixDelete, "Remove");
            this.buttonStoredProcedurePrefixDelete.UseVisualStyleBackColor = true;
            this.buttonStoredProcedurePrefixDelete.Click += new System.EventHandler(this.buttonStoredProcedurePrefixDelete_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.buttonTablePrefixAdd);
            this.groupBox2.Controls.Add(this.listBoxTablePrefix);
            this.groupBox2.Controls.Add(this.textBoxTablePrefix);
            this.groupBox2.Controls.Add(this.buttonTablePrefixDelete);
            this.groupBox2.Location = new System.Drawing.Point(6, 170);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(202, 158);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Table Prefixes";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.textBoxViewPrefix);
            this.groupBox3.Controls.Add(this.listBoxViewPrefix);
            this.groupBox3.Controls.Add(this.buttonViewPrefixAdd);
            this.groupBox3.Controls.Add(this.buttonViewPrefixDelete);
            this.groupBox3.Location = new System.Drawing.Point(212, 170);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(202, 158);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View Prefixes";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.textBoxStoredProcedurePrefix);
            this.groupBox4.Controls.Add(this.buttonStoredProcedurePrefixAdd);
            this.groupBox4.Controls.Add(this.listBoxStoredProcedurePrefix);
            this.groupBox4.Controls.Add(this.buttonStoredProcedurePrefixDelete);
            this.groupBox4.Location = new System.Drawing.Point(418, 170);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(202, 158);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Stored Procedure Prefixes";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAddDatabase);
            this.groupBox1.Controls.Add(this.listBoxDatabase);
            this.groupBox1.Controls.Add(this.buttonRemove);
            this.groupBox1.Controls.Add(this.buttonEdit);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 158);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Databases";
            // 
            // SetupDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SetupDatabase";
            this.Size = new System.Drawing.Size(623, 340);
            this.contextMenuStripPrefix.ResumeLayout(false);
            this.contextMenuStripDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStoredProcedurePrefixAdd;
        private System.Windows.Forms.Button buttonViewPrefixAdd;
        private System.Windows.Forms.Button buttonTablePrefixAdd;
        private System.Windows.Forms.ListBox listBoxDatabase;
        private System.Windows.Forms.TextBox textBoxTablePrefix;
        private System.Windows.Forms.ListBox listBoxTablePrefix;
        private System.Windows.Forms.ListBox listBoxStoredProcedurePrefix;
        private System.Windows.Forms.TextBox textBoxViewPrefix;
        private System.Windows.Forms.TextBox textBoxStoredProcedurePrefix;
        private System.Windows.Forms.ListBox listBoxViewPrefix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPrefix;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeletePrefix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDatabase;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabaseDelete;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonAddDatabase;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonTablePrefixDelete;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonStoredProcedurePrefixDelete;
        private System.Windows.Forms.Button buttonViewPrefixDelete;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
