namespace ArchAngel.Workbench.UserControls
{
	partial class BaseTypeMapEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseTypeMapEditor));
			this.dataGridViewCSharp = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.labelUsedBy = new DevComponents.DotNetBar.LabelX();
			this.ColumnDotNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnCSharp = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnVB = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnSQLServer1 = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			this.ColumnOracle = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			this.ColumnMySQL = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			this.ColumnPostgreSQL = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			this.ColumnFirebird = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			this.ColumnSQLite = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			this.ColumnDelete = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCSharp)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewCSharp
			// 
			this.dataGridViewCSharp.AllowUserToDeleteRows = false;
			this.dataGridViewCSharp.AllowUserToResizeRows = false;
			this.dataGridViewCSharp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCSharp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDotNet,
            this.ColumnCSharp,
            this.ColumnVB,
            this.ColumnSQLServer1,
            this.ColumnOracle,
            this.ColumnMySQL,
            this.ColumnPostgreSQL,
            this.ColumnFirebird,
            this.ColumnSQLite,
            this.ColumnDelete});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewCSharp.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewCSharp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewCSharp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridViewCSharp.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.dataGridViewCSharp.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewCSharp.Name = "dataGridViewCSharp";
			this.dataGridViewCSharp.RowHeadersVisible = false;
			this.dataGridViewCSharp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewCSharp.Size = new System.Drawing.Size(551, 469);
			this.dataGridViewCSharp.TabIndex = 6;
			this.dataGridViewCSharp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCSharp_CellContentClick);
			this.dataGridViewCSharp.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCSharp_CellEnter);
			this.dataGridViewCSharp.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCSharp_CellValueChanged);
			this.dataGridViewCSharp.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewCSharp_RowsAdded);
			// 
			// expandableSplitter2
			// 
			this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter2.Dock = System.Windows.Forms.DockStyle.Right;
			this.expandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter2.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
			this.expandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
			this.expandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
			this.expandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter2.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
			this.expandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
			this.expandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter2.Location = new System.Drawing.Point(551, 0);
			this.expandableSplitter2.Name = "expandableSplitter2";
			this.expandableSplitter2.Size = new System.Drawing.Size(6, 469);
			this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter2.TabIndex = 7;
			this.expandableSplitter2.TabStop = false;
			// 
			// labelUsedBy
			// 
			// 
			// 
			// 
			this.labelUsedBy.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.labelUsedBy.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.labelUsedBy.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelUsedBy.BackgroundStyle.Class = "";
			this.labelUsedBy.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelUsedBy.Dock = System.Windows.Forms.DockStyle.Right;
			this.labelUsedBy.ForeColor = System.Drawing.Color.White;
			this.labelUsedBy.Location = new System.Drawing.Point(557, 0);
			this.labelUsedBy.Name = "labelUsedBy";
			this.labelUsedBy.Size = new System.Drawing.Size(151, 469);
			this.labelUsedBy.TabIndex = 8;
			this.labelUsedBy.Text = "<b>labelX3</b>\r\n<br/>\r\n  one";
			this.labelUsedBy.TextLineAlignment = System.Drawing.StringAlignment.Near;
			// 
			// ColumnDotNet
			// 
			this.ColumnDotNet.Frozen = true;
			this.ColumnDotNet.HeaderText = ".Net";
			this.ColumnDotNet.Name = "ColumnDotNet";
			// 
			// ColumnCSharp
			// 
			this.ColumnCSharp.HeaderText = "C#";
			this.ColumnCSharp.Name = "ColumnCSharp";
			// 
			// ColumnVB
			// 
			this.ColumnVB.HeaderText = "VB.Net";
			this.ColumnVB.Name = "ColumnVB";
			// 
			// ColumnSQLServer1
			// 
			this.ColumnSQLServer1.DisplayMember = "Text";
			this.ColumnSQLServer1.DropDownHeight = 106;
			this.ColumnSQLServer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ColumnSQLServer1.DropDownWidth = 121;
			this.ColumnSQLServer1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnSQLServer1.HeaderText = "SQL Server";
			this.ColumnSQLServer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnSQLServer1.IntegralHeight = false;
			this.ColumnSQLServer1.ItemHeight = 13;
			this.ColumnSQLServer1.Name = "ColumnSQLServer1";
			this.ColumnSQLServer1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnSQLServer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// ColumnOracle
			// 
			this.ColumnOracle.DisplayMember = "Text";
			this.ColumnOracle.DropDownHeight = 106;
			this.ColumnOracle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ColumnOracle.DropDownWidth = 121;
			this.ColumnOracle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnOracle.HeaderText = "Oracle";
			this.ColumnOracle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnOracle.IntegralHeight = false;
			this.ColumnOracle.ItemHeight = 13;
			this.ColumnOracle.Name = "ColumnOracle";
			this.ColumnOracle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnOracle.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// ColumnMySQL
			// 
			this.ColumnMySQL.DisplayMember = "Text";
			this.ColumnMySQL.DropDownHeight = 106;
			this.ColumnMySQL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ColumnMySQL.DropDownWidth = 121;
			this.ColumnMySQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnMySQL.HeaderText = "MySQL";
			this.ColumnMySQL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnMySQL.IntegralHeight = false;
			this.ColumnMySQL.ItemHeight = 13;
			this.ColumnMySQL.Name = "ColumnMySQL";
			this.ColumnMySQL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnMySQL.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// ColumnPostgreSQL
			// 
			this.ColumnPostgreSQL.DisplayMember = "Text";
			this.ColumnPostgreSQL.DropDownHeight = 106;
			this.ColumnPostgreSQL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ColumnPostgreSQL.DropDownWidth = 121;
			this.ColumnPostgreSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnPostgreSQL.HeaderText = "PostgreSQL";
			this.ColumnPostgreSQL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnPostgreSQL.IntegralHeight = false;
			this.ColumnPostgreSQL.ItemHeight = 13;
			this.ColumnPostgreSQL.Name = "ColumnPostgreSQL";
			this.ColumnPostgreSQL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnPostgreSQL.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// ColumnFirebird
			// 
			this.ColumnFirebird.DisplayMember = "Text";
			this.ColumnFirebird.DropDownHeight = 106;
			this.ColumnFirebird.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ColumnFirebird.DropDownWidth = 121;
			this.ColumnFirebird.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnFirebird.HeaderText = "Firebird";
			this.ColumnFirebird.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnFirebird.IntegralHeight = false;
			this.ColumnFirebird.ItemHeight = 13;
			this.ColumnFirebird.Name = "ColumnFirebird";
			this.ColumnFirebird.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnFirebird.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// ColumnSQLite
			// 
			this.ColumnSQLite.DropDownHeight = 106;
			this.ColumnSQLite.DropDownWidth = 121;
			this.ColumnSQLite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnSQLite.HeaderText = "SQLite";
			this.ColumnSQLite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnSQLite.ItemHeight = 15;
			this.ColumnSQLite.Name = "ColumnSQLite";
			this.ColumnSQLite.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// ColumnDelete
			// 
			this.ColumnDelete.HeaderText = "";
			this.ColumnDelete.HoverImage = ((System.Drawing.Image)(resources.GetObject("ColumnDelete.HoverImage")));
			this.ColumnDelete.Image = ((System.Drawing.Image)(resources.GetObject("ColumnDelete.Image")));
			this.ColumnDelete.Name = "ColumnDelete";
			this.ColumnDelete.Text = null;
			this.ColumnDelete.Width = 20;
			// 
			// BaseTypeMapEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridViewCSharp);
			this.Controls.Add(this.expandableSplitter2);
			this.Controls.Add(this.labelUsedBy);
			this.Name = "BaseTypeMapEditor";
			this.Size = new System.Drawing.Size(708, 469);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCSharp)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewCSharp;
		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
		private DevComponents.DotNetBar.LabelX labelUsedBy;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDotNet;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCSharp;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVB;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnSQLServer1;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnOracle;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnMySQL;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnPostgreSQL;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnFirebird;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnSQLite;
		private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn ColumnDelete;
	}
}
