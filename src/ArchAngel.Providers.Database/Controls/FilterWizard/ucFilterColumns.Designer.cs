namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    partial class ucFilterColumns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFilterColumns));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.imageListState = new System.Windows.Forms.ImageList(this.components);
            this.gridFilterColumns = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colLogical = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridFilterColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select columns to filter by:";
            // 
            // imageListState
            // 
            this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
            this.imageListState.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListState.Images.SetKeyName(0, "");
            this.imageListState.Images.SetKeyName(1, "");
            this.imageListState.Images.SetKeyName(2, "");
            // 
            // gridFilterColumns
            // 
            this.gridFilterColumns.AllowUserToAddRows = false;
            this.gridFilterColumns.AllowUserToDeleteRows = false;
            this.gridFilterColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFilterColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFilterColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected,
            this.colTable,
            this.colColumn,
            this.colAlias,
            this.colOperator,
            this.colLogical});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridFilterColumns.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridFilterColumns.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gridFilterColumns.Location = new System.Drawing.Point(6, 31);
            this.gridFilterColumns.Name = "gridFilterColumns";
            this.gridFilterColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFilterColumns.Size = new System.Drawing.Size(867, 381);
            this.gridFilterColumns.TabIndex = 2;
            this.gridFilterColumns.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFilterColumns_CellEndEdit);
            this.gridFilterColumns.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridFilterColumns_DataError);
            this.gridFilterColumns.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFilterColumns_CellEnter);
            // 
            // colSelected
            // 
            this.colSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSelected.HeaderText = "";
            this.colSelected.Name = "colSelected";
            this.colSelected.Width = 5;
            // 
            // colTable
            // 
            this.colTable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTable.HeaderText = "Table";
            this.colTable.Name = "colTable";
            this.colTable.ReadOnly = true;
            this.colTable.Width = 59;
            // 
            // colColumn
            // 
            this.colColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colColumn.HeaderText = "Column";
            this.colColumn.Name = "colColumn";
            this.colColumn.ReadOnly = true;
            this.colColumn.Width = 67;
            // 
            // colAlias
            // 
            this.colAlias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAlias.HeaderText = "Alias";
            this.colAlias.Name = "colAlias";
            this.colAlias.ReadOnly = true;
            this.colAlias.Width = 54;
            // 
            // colOperator
            // 
            this.colOperator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colOperator.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colOperator.HeaderText = "Operator";
            this.colOperator.Items.AddRange(new object[] {
            "=",
            "<",
            ">",
            "<=",
            ">=",
            "<>"});
            this.colOperator.Name = "colOperator";
            this.colOperator.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colOperator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colOperator.Width = 73;
            // 
            // colLogical
            // 
            this.colLogical.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLogical.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colLogical.HeaderText = "And / Or";
            this.colLogical.Items.AddRange(new object[] {
            "And",
            "Or",
            " "});
            this.colLogical.Name = "colLogical";
            this.colLogical.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLogical.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colLogical.Width = 73;
            // 
            // ucFilterColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridFilterColumns);
            this.Controls.Add(this.label1);
            this.Name = "ucFilterColumns";
            this.Size = new System.Drawing.Size(886, 427);
            ((System.ComponentModel.ISupportInitialize)(this.gridFilterColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageListState;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridFilterColumns;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlias;
        private System.Windows.Forms.DataGridViewComboBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewComboBoxColumn colLogical;
    }
}
