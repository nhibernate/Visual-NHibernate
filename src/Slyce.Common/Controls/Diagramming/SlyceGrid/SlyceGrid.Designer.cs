namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
    partial class SlyceGrid
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridViewX1 = new Slyce.Common.Controls.Diagramming.SlyceGrid.DataGridViewGFH();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewX1
			// 
			this.dataGridViewX1.AllowUserToResizeRows = false;
			this.dataGridViewX1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridViewX1.CausesValidation = false;
			this.dataGridViewX1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dataGridViewX1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewX1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewX1.ColumnHeadersHeight = 60;
			this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewX1.EnableHeadersVisualStyles = false;
			this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.dataGridViewX1.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewX1.Name = "dataGridViewX1";
			this.dataGridViewX1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewX1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewX1.RowHeadersWidth = 20;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			this.dataGridViewX1.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewX1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
			this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewX1.ShowEditingIcon = false;
			this.dataGridViewX1.Size = new System.Drawing.Size(293, 166);
			this.dataGridViewX1.TabIndex = 1;
			this.dataGridViewX1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
			this.dataGridViewX1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellEndEdit);
			this.dataGridViewX1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellEnter);
			this.dataGridViewX1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewX1_CellPainting);
			this.dataGridViewX1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewX1_CellValidating);
			this.dataGridViewX1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewX1_ColumnWidthChanged);
			this.dataGridViewX1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewX1_DataError);
			this.dataGridViewX1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewX1_RowsAdded);
			this.dataGridViewX1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewX1_Scroll);
			this.dataGridViewX1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewX1_MouseClick);
			this.dataGridViewX1.Resize += new System.EventHandler(this.dataGridViewX1_Resize);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			this.Column1.Width = 72;
			// 
			// Column2
			// 
			this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
			this.Column2.HeaderText = "";
			this.Column2.Name = "Column2";
			this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column2.Width = 20;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Column3";
			this.Column3.Name = "Column3";
			this.Column3.Width = 72;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Column4";
			this.Column4.Name = "Column4";
			this.Column4.Width = 72;
			// 
			// SlyceGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewX1);
			this.Name = "SlyceGrid";
			this.Size = new System.Drawing.Size(293, 166);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DataGridViewGFH dataGridViewX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}
