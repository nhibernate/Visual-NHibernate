namespace ArchAngel.Workbench.UserControls
{
	partial class DbTypeMapEditor
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
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.syntaxEditorSqlServer = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.labelScriptHeader = new DevComponents.DotNetBar.LabelX();
			this.dataGridViewSqlServer = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.ColumnSQLServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnUniType = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSqlServer)).BeginInit();
			this.SuspendLayout();
			// 
			// expandableSplitter1
			// 
			this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(166)))), ((int)(((byte)(72)))));
			this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
			this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.Location = new System.Drawing.Point(306, 23);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new System.Drawing.Size(6, 424);
			this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter1.TabIndex = 20;
			this.expandableSplitter1.TabStop = false;
			// 
			// syntaxEditorSqlServer
			// 
			this.syntaxEditorSqlServer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.syntaxEditorSqlServer.Document = document1;
			this.syntaxEditorSqlServer.LineNumberMarginVisible = true;
			this.syntaxEditorSqlServer.Location = new System.Drawing.Point(306, 23);
			this.syntaxEditorSqlServer.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorSqlServer.Name = "syntaxEditorSqlServer";
			this.syntaxEditorSqlServer.Size = new System.Drawing.Size(423, 424);
			this.syntaxEditorSqlServer.TabIndex = 21;
			// 
			// labelScriptHeader
			// 
			// 
			// 
			// 
			this.labelScriptHeader.BackgroundStyle.Class = "";
			this.labelScriptHeader.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelScriptHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelScriptHeader.ForeColor = System.Drawing.Color.White;
			this.labelScriptHeader.Location = new System.Drawing.Point(306, 0);
			this.labelScriptHeader.Name = "labelScriptHeader";
			this.labelScriptHeader.Size = new System.Drawing.Size(423, 23);
			this.labelScriptHeader.TabIndex = 19;
			this.labelScriptHeader.Text = "SQL Server -> C#";
			this.labelScriptHeader.TextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// dataGridViewSqlServer
			// 
			this.dataGridViewSqlServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewSqlServer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSQLServer,
            this.ColumnUniType});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewSqlServer.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewSqlServer.Dock = System.Windows.Forms.DockStyle.Left;
			this.dataGridViewSqlServer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridViewSqlServer.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
			this.dataGridViewSqlServer.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewSqlServer.Name = "dataGridViewSqlServer";
			this.dataGridViewSqlServer.Size = new System.Drawing.Size(306, 447);
			this.dataGridViewSqlServer.TabIndex = 18;
			this.dataGridViewSqlServer.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSqlServer_CellValueChanged);
			this.dataGridViewSqlServer.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewSqlServer_RowsAdded);
			// 
			// ColumnSQLServer
			// 
			this.ColumnSQLServer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ColumnSQLServer.HeaderText = "SQL Server Types";
			this.ColumnSQLServer.Name = "ColumnSQLServer";
			this.ColumnSQLServer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnSQLServer.Width = 109;
			// 
			// ColumnUniType
			// 
			this.ColumnUniType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ColumnUniType.DisplayMember = "Text";
			this.ColumnUniType.DropDownHeight = 106;
			this.ColumnUniType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ColumnUniType.DropDownWidth = 121;
			this.ColumnUniType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ColumnUniType.HeaderText = "C# Types";
			this.ColumnUniType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ColumnUniType.IntegralHeight = false;
			this.ColumnUniType.ItemHeight = 13;
			this.ColumnUniType.Name = "ColumnUniType";
			this.ColumnUniType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ColumnUniType.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ColumnUniType.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.ColumnUniType.Width = 121;
			// 
			// DbTypeMapEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.expandableSplitter1);
			this.Controls.Add(this.syntaxEditorSqlServer);
			this.Controls.Add(this.labelScriptHeader);
			this.Controls.Add(this.dataGridViewSqlServer);
			this.Name = "DbTypeMapEditor";
			this.Size = new System.Drawing.Size(729, 447);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSqlServer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorSqlServer;
		private DevComponents.DotNetBar.LabelX labelScriptHeader;
		private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewSqlServer;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSQLServer;
		private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn ColumnUniType;
	}
}
