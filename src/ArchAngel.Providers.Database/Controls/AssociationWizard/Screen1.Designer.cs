namespace ArchAngel.Providers.Database.Controls.AssociationWizard
{
    partial class Screen1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelSentence1 = new System.Windows.Forms.Label();
            this.comboBoxActionTypes = new System.Windows.Forms.ComboBox();
            this.comboBoxObjectTypes = new System.Windows.Forms.ComboBox();
            this.lblTreeviewHeading = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelSentence4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labelSentence2 = new System.Windows.Forms.Label();
            this.labelSentence3 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridListObjects = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Alias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridListObjects)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSentence1
            // 
            this.labelSentence1.AutoSize = true;
            this.labelSentence1.Location = new System.Drawing.Point(3, 0);
            this.labelSentence1.Name = "labelSentence1";
            this.labelSentence1.Size = new System.Drawing.Size(90, 13);
            this.labelSentence1.TabIndex = 0;
            this.labelSentence1.Text = "I want to create a";
            this.labelSentence1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxActionTypes
            // 
            this.comboBoxActionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxActionTypes.FormattingEnabled = true;
            this.comboBoxActionTypes.Location = new System.Drawing.Point(99, 3);
            this.comboBoxActionTypes.Name = "comboBoxActionTypes";
            this.comboBoxActionTypes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxActionTypes.Sorted = true;
            this.comboBoxActionTypes.TabIndex = 1;
            // 
            // comboBoxObjectTypes
            // 
            this.comboBoxObjectTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxObjectTypes.FormattingEnabled = true;
            this.comboBoxObjectTypes.Location = new System.Drawing.Point(480, 3);
            this.comboBoxObjectTypes.Name = "comboBoxObjectTypes";
            this.comboBoxObjectTypes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxObjectTypes.Sorted = true;
            this.comboBoxObjectTypes.TabIndex = 3;
            this.comboBoxObjectTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxObjectTypes_SelectedIndexChanged);
            // 
            // lblTreeviewHeading
            // 
            this.lblTreeviewHeading.AutoSize = true;
            this.lblTreeviewHeading.Location = new System.Drawing.Point(6, 64);
            this.lblTreeviewHeading.Name = "lblTreeviewHeading";
            this.lblTreeviewHeading.Size = new System.Drawing.Size(32, 13);
            this.lblTreeviewHeading.TabIndex = 5;
            this.lblTreeviewHeading.Text = "Filter:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.labelSentence1);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxActionTypes);
            this.flowLayoutPanel1.Controls.Add(this.labelSentence4);
            this.flowLayoutPanel1.Controls.Add(this.txtName);
            this.flowLayoutPanel1.Controls.Add(this.labelSentence2);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxObjectTypes);
            this.flowLayoutPanel1.Controls.Add(this.labelSentence3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(710, 52);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // labelSentence4
            // 
            this.labelSentence4.AutoSize = true;
            this.labelSentence4.Location = new System.Drawing.Point(226, 0);
            this.labelSentence4.Name = "labelSentence4";
            this.labelSentence4.Size = new System.Drawing.Size(92, 13);
            this.labelSentence4.TabIndex = 6;
            this.labelSentence4.Text = "Association called";
            this.labelSentence4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(324, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 5;
            // 
            // labelSentence2
            // 
            this.labelSentence2.AutoSize = true;
            this.labelSentence2.Location = new System.Drawing.Point(430, 0);
            this.labelSentence2.Name = "labelSentence2";
            this.labelSentence2.Size = new System.Drawing.Size(44, 13);
            this.labelSentence2.TabIndex = 2;
            this.labelSentence2.Text = "with the";
            // 
            // labelSentence3
            // 
            this.labelSentence3.AutoSize = true;
            this.labelSentence3.Location = new System.Drawing.Point(607, 0);
            this.labelSentence3.Name = "labelSentence3";
            this.labelSentence3.Size = new System.Drawing.Size(81, 13);
            this.labelSentence3.TabIndex = 4;
            this.labelSentence3.Text = "selected below:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(44, 61);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(173, 20);
            this.txtFilter.TabIndex = 7;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "You can use wildcards eg: sp*address*";
            // 
            // gridListObjects
            // 
            this.gridListObjects.AllowUserToAddRows = false;
            this.gridListObjects.AllowUserToDeleteRows = false;
            this.gridListObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridListObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Alias});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridListObjects.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridListObjects.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gridListObjects.Location = new System.Drawing.Point(9, 104);
            this.gridListObjects.MultiSelect = false;
            this.gridListObjects.Name = "gridListObjects";
            this.gridListObjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridListObjects.Size = new System.Drawing.Size(696, 283);
            this.gridListObjects.TabIndex = 9;
            // 
            // Alias
            // 
            this.Alias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Alias.HeaderText = "Alias";
            this.Alias.Name = "Alias";
            this.Alias.ReadOnly = true;
            // 
            // Screen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridListObjects);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblTreeviewHeading);
            this.Controls.Add(this.txtFilter);
            this.Name = "Screen1";
            this.Size = new System.Drawing.Size(716, 402);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridListObjects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSentence1;
        private System.Windows.Forms.ComboBox comboBoxActionTypes;
        private System.Windows.Forms.ComboBox comboBoxObjectTypes;
        private System.Windows.Forms.Label lblTreeviewHeading;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelSentence2;
        private System.Windows.Forms.Label labelSentence3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSentence4;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridListObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alias;
    }
}
