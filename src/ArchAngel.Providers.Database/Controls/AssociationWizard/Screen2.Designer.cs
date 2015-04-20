namespace ArchAngel.Providers.Database.Controls.AssociationWizard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPrimaryObjectName = new System.Windows.Forms.Label();
            this.lblAssociatedObjectName = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelSentence1 = new System.Windows.Forms.Label();
            this.buttonAutoMap = new System.Windows.Forms.Button();
            this.gridMappings = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLinkedColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colLinkedParameter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrimaryObjectName
            // 
            this.lblPrimaryObjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(222)))), ((int)(((byte)(254)))));
            this.lblPrimaryObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaryObjectName.Location = new System.Drawing.Point(21, 64);
            this.lblPrimaryObjectName.Name = "lblPrimaryObjectName";
            this.lblPrimaryObjectName.Size = new System.Drawing.Size(64, 13);
            this.lblPrimaryObjectName.TabIndex = 1;
            this.lblPrimaryObjectName.Text = "label1";
            // 
            // lblAssociatedObjectName
            // 
            this.lblAssociatedObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAssociatedObjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(222)))), ((int)(((byte)(254)))));
            this.lblAssociatedObjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssociatedObjectName.Location = new System.Drawing.Point(530, 64);
            this.lblAssociatedObjectName.Name = "lblAssociatedObjectName";
            this.lblAssociatedObjectName.Size = new System.Drawing.Size(41, 13);
            this.lblAssociatedObjectName.TabIndex = 2;
            this.lblAssociatedObjectName.Text = "label1";
            this.lblAssociatedObjectName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.labelSentence1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(555, 37);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // labelSentence1
            // 
            this.labelSentence1.AutoSize = true;
            this.labelSentence1.Location = new System.Drawing.Point(3, 0);
            this.labelSentence1.Name = "labelSentence1";
            this.labelSentence1.Size = new System.Drawing.Size(214, 13);
            this.labelSentence1.TabIndex = 0;
            this.labelSentence1.Text = "Specify how the columns of XXX map to the";
            // 
            // buttonAutoMap
            // 
            this.buttonAutoMap.Location = new System.Drawing.Point(10, 38);
            this.buttonAutoMap.Name = "buttonAutoMap";
            this.buttonAutoMap.Size = new System.Drawing.Size(75, 23);
            this.buttonAutoMap.TabIndex = 4;
            this.buttonAutoMap.Text = "AutoMap";
            this.buttonAutoMap.UseVisualStyleBackColor = true;
            this.buttonAutoMap.Click += new System.EventHandler(this.buttonAutoMap_Click);
            // 
            // gridMappings
            // 
            this.gridMappings.AllowUserToAddRows = false;
            this.gridMappings.AllowUserToDeleteRows = false;
            this.gridMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColumns,
            this.colLinkedColumn,
            this.colLinkedParameter});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridMappings.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridMappings.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gridMappings.Location = new System.Drawing.Point(14, 80);
            this.gridMappings.Name = "gridMappings";
            this.gridMappings.Size = new System.Drawing.Size(557, 328);
            this.gridMappings.TabIndex = 5;
            this.gridMappings.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMappings_CellEnter);
            // 
            // colColumns
            // 
            this.colColumns.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colColumns.HeaderText = "This column...";
            this.colColumns.Name = "colColumns";
            this.colColumns.ReadOnly = true;
            // 
            // colLinkedColumn
            // 
            this.colLinkedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLinkedColumn.HeaderText = "...maps to this column";
            this.colLinkedColumn.Name = "colLinkedColumn";
            this.colLinkedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLinkedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colLinkedColumn.Width = 92;
            // 
            // colLinkedParameter
            // 
            this.colLinkedParameter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLinkedParameter.HeaderText = "...maps to this parameter";
            this.colLinkedParameter.Name = "colLinkedParameter";
            this.colLinkedParameter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLinkedParameter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colLinkedParameter.Width = 134;
            // 
            // Screen2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMappings);
            this.Controls.Add(this.buttonAutoMap);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblAssociatedObjectName);
            this.Controls.Add(this.lblPrimaryObjectName);
            this.Name = "Screen2";
            this.Size = new System.Drawing.Size(589, 423);
            this.Resize += new System.EventHandler(this.Screen2_Resize);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPrimaryObjectName;
        private System.Windows.Forms.Label lblAssociatedObjectName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelSentence1;
        private System.Windows.Forms.Button buttonAutoMap;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridMappings;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumns;
        private System.Windows.Forms.DataGridViewComboBoxColumn colLinkedColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn colLinkedParameter;
    }
}
