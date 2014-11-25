namespace ArchAngel.Designer
{
    partial class frmFunctionEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFunctionEdit));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStripOverloads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteOverloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblReturnType = new System.Windows.Forms.Label();
            this.ddlReturnType = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.chkTemplateFunction = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlScriptLanguage = new System.Windows.Forms.ComboBox();
            this.btnDeleteFunction = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ddlCategory = new System.Windows.Forms.ComboBox();
            this.contextMenuStripParameters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.grouper1 = new Slyce.Common.Controls.Grouper();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.btnDeleteParameter = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.lstParameters = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.contextMenuStripOverloads.SuspendLayout();
            this.contextMenuStripParameters.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "delete.bmp");
            this.imageList1.Images.SetKeyName(1, "AddTableHS.png");
            // 
            // contextMenuStripOverloads
            // 
            this.contextMenuStripOverloads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteOverloadToolStripMenuItem});
            this.contextMenuStripOverloads.Name = "contextMenuStripOverloads";
            this.contextMenuStripOverloads.Size = new System.Drawing.Size(162, 26);
            // 
            // deleteOverloadToolStripMenuItem
            // 
            this.deleteOverloadToolStripMenuItem.Name = "deleteOverloadToolStripMenuItem";
            this.deleteOverloadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteOverloadToolStripMenuItem.Text = "&Delete overload";
            // 
            // lblReturnType
            // 
            this.lblReturnType.AutoSize = true;
            this.lblReturnType.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnType.Location = new System.Drawing.Point(296, 13);
            this.lblReturnType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Size = new System.Drawing.Size(77, 13);
            this.lblReturnType.TabIndex = 33;
            this.lblReturnType.Text = "Return Type";
            // 
            // ddlReturnType
            // 
            this.ddlReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlReturnType.FormattingEnabled = true;
            this.ddlReturnType.Items.AddRange(new object[] {
            "C#",
            "VB.net",
            "SQL",
            "HTML",
            "CSS",
            "INI File",
            "JScript",
            "Python",
            "VbSscript",
            "XML",
            "Plain Text"});
            this.ddlReturnType.Location = new System.Drawing.Point(299, 27);
            this.ddlReturnType.Margin = new System.Windows.Forms.Padding(2);
            this.ddlReturnType.Name = "ddlReturnType";
            this.ddlReturnType.Size = new System.Drawing.Size(92, 21);
            this.ddlReturnType.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ddlReturnType, "The type to return from this function. Template functions always return text, so " +
                    "you can specify what type this text will be, allowing syntax highlighting");
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(9, 11);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 29;
            this.lblName.Text = "Name";
            // 
            // chkTemplateFunction
            // 
            this.chkTemplateFunction.AutoSize = true;
            this.chkTemplateFunction.BackColor = System.Drawing.Color.Transparent;
            this.chkTemplateFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTemplateFunction.Location = new System.Drawing.Point(517, 29);
            this.chkTemplateFunction.Margin = new System.Windows.Forms.Padding(2);
            this.chkTemplateFunction.Name = "chkTemplateFunction";
            this.chkTemplateFunction.Size = new System.Drawing.Size(135, 17);
            this.chkTemplateFunction.TabIndex = 3;
            this.chkTemplateFunction.Text = "Template function?";
            this.toolTip1.SetToolTip(this.chkTemplateFunction, "Template functions can only return text. Only template functions can be used to w" +
                    "rite output to files.");
            this.chkTemplateFunction.UseVisualStyleBackColor = false;
            this.chkTemplateFunction.CheckedChanged += new System.EventHandler(this.chkTemplateFunction_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(589, 254);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "Discard changes and close");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(11, 27);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(284, 20);
            this.txtName.TabIndex = 0;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(529, 254);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 24);
            this.btnOk.TabIndex = 28;
            this.btnOk.Text = "OK";
            this.toolTip1.SetToolTip(this.btnOk, "Save changes and close");
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(392, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Script language:";
            // 
            // ddlScriptLanguage
            // 
            this.ddlScriptLanguage.FormattingEnabled = true;
            this.ddlScriptLanguage.Location = new System.Drawing.Point(395, 27);
            this.ddlScriptLanguage.Margin = new System.Windows.Forms.Padding(2);
            this.ddlScriptLanguage.Name = "ddlScriptLanguage";
            this.ddlScriptLanguage.Size = new System.Drawing.Size(107, 21);
            this.ddlScriptLanguage.TabIndex = 2;
            this.toolTip1.SetToolTip(this.ddlScriptLanguage, "The language that will be used to write the template scripting code");
            // 
            // btnDeleteFunction
            // 
            this.btnDeleteFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFunction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteFunction.ImageIndex = 0;
            this.btnDeleteFunction.ImageList = this.imageList1;
            this.btnDeleteFunction.Location = new System.Drawing.Point(434, 254);
            this.btnDeleteFunction.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteFunction.Name = "btnDeleteFunction";
            this.btnDeleteFunction.Size = new System.Drawing.Size(69, 24);
            this.btnDeleteFunction.TabIndex = 37;
            this.btnDeleteFunction.Text = "      &Delete";
            this.btnDeleteFunction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnDeleteFunction, "Delete this function");
            this.btnDeleteFunction.UseVisualStyleBackColor = true;
            this.btnDeleteFunction.Click += new System.EventHandler(this.btnDeleteFunction_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(395, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.AcceptsTab = true;
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(398, 122);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(247, 100);
            this.txtDescription.TabIndex = 5;
            // 
            // ddlCategory
            // 
            this.ddlCategory.FormattingEnabled = true;
            this.ddlCategory.Location = new System.Drawing.Point(398, 77);
            this.ddlCategory.Margin = new System.Windows.Forms.Padding(2);
            this.ddlCategory.Name = "ddlCategory";
            this.ddlCategory.Size = new System.Drawing.Size(92, 21);
            this.ddlCategory.TabIndex = 4;
            this.toolTip1.SetToolTip(this.ddlCategory, "The language that will be used to write the template scripting code");
            // 
            // contextMenuStripParameters
            // 
            this.contextMenuStripParameters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteParameterToolStripMenuItem});
            this.contextMenuStripParameters.Name = "contextMenuStripParameters";
            this.contextMenuStripParameters.Size = new System.Drawing.Size(170, 26);
            // 
            // deleteParameterToolStripMenuItem
            // 
            this.deleteParameterToolStripMenuItem.Name = "deleteParameterToolStripMenuItem";
            this.deleteParameterToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deleteParameterToolStripMenuItem.Text = "&Delete parameter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(395, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Category";
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientMode = Slyce.Common.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 0F;
            this.grouper1.Controls.Add(this.btnAddParameter);
            this.grouper1.Controls.Add(this.btnDeleteParameter);
            this.grouper1.Controls.Add(this.btnMoveUp);
            this.grouper1.Controls.Add(this.btnMoveDown);
            this.grouper1.Controls.Add(this.lstParameters);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Parameters";
            this.grouper1.Location = new System.Drawing.Point(12, 60);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(56)))), ((int)(((byte)(153)))));
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowControlForTitle = true;
            this.grouper1.ShadowThickness = 2;
            this.grouper1.Size = new System.Drawing.Size(354, 183);
            this.grouper1.TabIndex = 22;
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParameter.Image = ((System.Drawing.Image)(resources.GetObject("btnAddParameter.Image")));
            this.btnAddParameter.Location = new System.Drawing.Point(321, 33);
            this.btnAddParameter.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(24, 24);
            this.btnAddParameter.TabIndex = 22;
            this.btnAddParameter.UseVisualStyleBackColor = true;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // btnDeleteParameter
            // 
            this.btnDeleteParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteParameter.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteParameter.Image")));
            this.btnDeleteParameter.Location = new System.Drawing.Point(321, 62);
            this.btnDeleteParameter.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteParameter.Name = "btnDeleteParameter";
            this.btnDeleteParameter.Size = new System.Drawing.Size(24, 24);
            this.btnDeleteParameter.TabIndex = 18;
            this.btnDeleteParameter.UseVisualStyleBackColor = true;
            this.btnDeleteParameter.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(324, 90);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(17, 19);
            this.btnMoveUp.TabIndex = 11;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Visible = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(323, 114);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(17, 19);
            this.btnMoveDown.TabIndex = 10;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Visible = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // lstParameters
            // 
            this.lstParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstParameters.FullRowSelect = true;
            this.lstParameters.GridLines = true;
            this.lstParameters.Location = new System.Drawing.Point(17, 33);
            this.lstParameters.MultiSelect = false;
            this.lstParameters.Name = "lstParameters";
            this.lstParameters.Size = new System.Drawing.Size(299, 136);
            this.lstParameters.TabIndex = 21;
            this.lstParameters.UseCompatibleStateImageBehavior = false;
            this.lstParameters.View = System.Windows.Forms.View.Details;
            this.lstParameters.DoubleClick += new System.EventHandler(this.lstParameters_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 116;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data Type";
            this.columnHeader2.Width = 89;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Default Value";
            this.columnHeader3.Width = 108;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 257);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(654, 36);
            this.ucHeading1.TabIndex = 40;
            // 
            // frmFunctionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(654, 293);
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.ddlCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblReturnType);
            this.Controls.Add(this.ddlReturnType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.chkTemplateFunction);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddlScriptLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDeleteFunction);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(658, 318);
            this.Name = "frmFunctionEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Function Definition";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFunctionEdit_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmFunctionEdit_Paint);
            this.contextMenuStripOverloads.ResumeLayout(false);
            this.contextMenuStripParameters.ResumeLayout(false);
            this.grouper1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteParameter;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Label lblReturnType;
        private System.Windows.Forms.ComboBox ddlReturnType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox chkTemplateFunction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlScriptLanguage;
        private System.Windows.Forms.Button btnDeleteFunction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOverloads;
        private System.Windows.Forms.ToolStripMenuItem deleteOverloadToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripParameters;
        private System.Windows.Forms.ToolStripMenuItem deleteParameterToolStripMenuItem;
        private System.Windows.Forms.ListView lstParameters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlCategory;
        private Slyce.Common.Controls.Grouper grouper1;
        private System.Windows.Forms.Button btnAddParameter;
    }
}