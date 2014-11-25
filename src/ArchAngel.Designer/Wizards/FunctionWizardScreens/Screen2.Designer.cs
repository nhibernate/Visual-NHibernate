namespace ArchAngel.Designer.Wizards.FunctionWizardScreens
{
    partial class Screen2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen2));
            this.label1 = new System.Windows.Forms.Label();
            this.ddlReturnType = new System.Windows.Forms.ComboBox();
            this.grouper1 = new Slyce.Common.Controls.Grouper();
            this.buttonEditParameter = new System.Windows.Forms.Button();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.btnDeleteParameter = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.lstParameters = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(848, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "What is this function returning?";
            // 
            // ddlReturnType
            // 
            this.ddlReturnType.FormattingEnabled = true;
            this.ddlReturnType.Location = new System.Drawing.Point(16, 21);
            this.ddlReturnType.Name = "ddlReturnType";
            this.ddlReturnType.Size = new System.Drawing.Size(247, 21);
            this.ddlReturnType.Sorted = true;
            this.ddlReturnType.TabIndex = 2;
            this.ddlReturnType.SelectedIndexChanged += new System.EventHandler(this.ddlReturnType_SelectedIndexChanged);
            // 
            // grouper1
            // 
            this.grouper1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grouper1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientMode = Slyce.Common.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 0F;
            this.grouper1.Controls.Add(this.buttonEditParameter);
            this.grouper1.Controls.Add(this.btnAddParameter);
            this.grouper1.Controls.Add(this.btnDeleteParameter);
            this.grouper1.Controls.Add(this.btnMoveUp);
            this.grouper1.Controls.Add(this.btnMoveDown);
            this.grouper1.Controls.Add(this.lstParameters);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Parameters";
            this.grouper1.Location = new System.Drawing.Point(16, 65);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(56)))), ((int)(((byte)(153)))));
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowControlForTitle = true;
            this.grouper1.ShadowThickness = 2;
            this.grouper1.Size = new System.Drawing.Size(845, 356);
            this.grouper1.TabIndex = 23;
            // 
            // buttonEditParameter
            // 
            this.buttonEditParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditParameter.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditParameter.Image")));
            this.buttonEditParameter.Location = new System.Drawing.Point(812, 61);
            this.buttonEditParameter.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEditParameter.Name = "buttonEditParameter";
            this.buttonEditParameter.Size = new System.Drawing.Size(24, 24);
            this.buttonEditParameter.TabIndex = 23;
            this.buttonEditParameter.UseVisualStyleBackColor = true;
            this.buttonEditParameter.Click += new System.EventHandler(this.buttonEditParameter_Click);
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParameter.Image = ((System.Drawing.Image)(resources.GetObject("btnAddParameter.Image")));
            this.btnAddParameter.Location = new System.Drawing.Point(812, 33);
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
            this.btnDeleteParameter.Location = new System.Drawing.Point(812, 89);
            this.btnDeleteParameter.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteParameter.Name = "btnDeleteParameter";
            this.btnDeleteParameter.Size = new System.Drawing.Size(24, 24);
            this.btnDeleteParameter.TabIndex = 18;
            this.btnDeleteParameter.UseVisualStyleBackColor = true;
            this.btnDeleteParameter.Click += new System.EventHandler(this.btnDeleteParameter_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(816, 161);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(17, 19);
            this.btnMoveUp.TabIndex = 11;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Visible = false;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(816, 184);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(17, 19);
            this.btnMoveDown.TabIndex = 10;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Visible = false;
            // 
            // lstParameters
            // 
            this.lstParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
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
            this.lstParameters.Size = new System.Drawing.Size(790, 305);
            this.lstParameters.TabIndex = 21;
            this.lstParameters.UseCompatibleStateImageBehavior = false;
            this.lstParameters.View = System.Windows.Forms.View.Details;
            this.lstParameters.SelectedIndexChanged += new System.EventHandler(this.lstParameters_SelectedIndexChanged);
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
            // Screen2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.ddlReturnType);
            this.Controls.Add(this.label1);
            this.Name = "Screen2";
            this.Size = new System.Drawing.Size(876, 437);
            this.grouper1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlReturnType;
        private Slyce.Common.Controls.Grouper grouper1;
        private System.Windows.Forms.Button btnAddParameter;
        private System.Windows.Forms.Button btnDeleteParameter;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.ListView lstParameters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonEditParameter;
    }
}
