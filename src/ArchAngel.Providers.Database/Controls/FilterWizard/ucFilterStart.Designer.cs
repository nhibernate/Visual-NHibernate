namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    partial class ucFilterStart
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
            this.grouper1 = new Slyce.Common.Controls.Grouper();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonSingleItem = new System.Windows.Forms.RadioButton();
            this.radioButtonCollection = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateStoredProcedure = new System.Windows.Forms.CheckBox();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientMode = Slyce.Common.Controls.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 0F;
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.radioButtonSingleItem);
            this.grouper1.Controls.Add(this.radioButtonCollection);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(6, 102);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(56)))), ((int)(((byte)(153)))));
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowControlForTitle = true;
            this.grouper1.ShadowThickness = 2;
            this.grouper1.Size = new System.Drawing.Size(368, 84);
            this.grouper1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "This filter will:";
            // 
            // radioButtonSingleItem
            // 
            this.radioButtonSingleItem.AutoSize = true;
            this.radioButtonSingleItem.Location = new System.Drawing.Point(26, 59);
            this.radioButtonSingleItem.Name = "radioButtonSingleItem";
            this.radioButtonSingleItem.Size = new System.Drawing.Size(118, 17);
            this.radioButtonSingleItem.TabIndex = 5;
            this.radioButtonSingleItem.Text = "Return a single item";
            this.radioButtonSingleItem.UseVisualStyleBackColor = true;
            // 
            // radioButtonCollection
            // 
            this.radioButtonCollection.AutoSize = true;
            this.radioButtonCollection.Checked = true;
            this.radioButtonCollection.Location = new System.Drawing.Point(26, 36);
            this.radioButtonCollection.Name = "radioButtonCollection";
            this.radioButtonCollection.Size = new System.Drawing.Size(153, 17);
            this.radioButtonCollection.TabIndex = 4;
            this.radioButtonCollection.TabStop = true;
            this.radioButtonCollection.Text = "Return a collection of items";
            this.radioButtonCollection.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(70, 8);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(301, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(-15, -15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateStoredProcedure
            // 
            this.checkBoxCreateStoredProcedure.AutoSize = true;
            this.checkBoxCreateStoredProcedure.Checked = true;
            this.checkBoxCreateStoredProcedure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateStoredProcedure.Location = new System.Drawing.Point(70, 86);
            this.checkBoxCreateStoredProcedure.Name = "checkBoxCreateStoredProcedure";
            this.checkBoxCreateStoredProcedure.Size = new System.Drawing.Size(150, 17);
            this.checkBoxCreateStoredProcedure.TabIndex = 2;
            this.checkBoxCreateStoredProcedure.Text = "Generate SQL for this filter";
            this.checkBoxCreateStoredProcedure.UseVisualStyleBackColor = true;
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlias.Location = new System.Drawing.Point(70, 34);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(301, 20);
            this.textBoxAlias.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Alias:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(70, 60);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(301, 20);
            this.textBoxDescription.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Description";
            // 
            // ucFilterStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxCreateStoredProcedure);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grouper1);
            this.Name = "ucFilterStart";
            this.Size = new System.Drawing.Size(689, 426);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Slyce.Common.Controls.Grouper grouper1;
        private System.Windows.Forms.RadioButton radioButtonSingleItem;
        private System.Windows.Forms.RadioButton radioButtonCollection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxCreateStoredProcedure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label4;
    }
}
