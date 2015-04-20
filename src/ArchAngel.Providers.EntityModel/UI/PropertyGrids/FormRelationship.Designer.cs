namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormRelationship
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxPrimaryKey = new System.Windows.Forms.ComboBox();
			this.comboBoxForeignKey = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelTableFK = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.labelTablePK = new System.Windows.Forms.Label();
			this.labelPrimaryKeyColumns = new DevComponents.DotNetBar.LabelX();
			this.labelForeignKeyColumns = new DevComponents.DotNetBar.LabelX();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.panelTitle = new DevComponents.DotNetBar.PanelEx();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeColumnMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx3.SuspendLayout();
			this.expandablePanel1.SuspendLayout();
			this.panelEx4.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.comboBoxPrimaryKey, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxForeignKey, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.labelTableFK, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbName, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelTablePK, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.labelPrimaryKeyColumns, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.labelForeignKeyColumns, 1, 6);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(660, 258);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// comboBoxPrimaryKey
			// 
			this.comboBoxPrimaryKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPrimaryKey.FormattingEnabled = true;
			this.comboBoxPrimaryKey.Location = new System.Drawing.Point(333, 103);
			this.comboBoxPrimaryKey.Name = "comboBoxPrimaryKey";
			this.comboBoxPrimaryKey.Size = new System.Drawing.Size(314, 21);
			this.comboBoxPrimaryKey.TabIndex = 2;
			this.comboBoxPrimaryKey.SelectedIndexChanged += new System.EventHandler(this.comboBoxKey1_SelectedIndexChanged);
			// 
			// comboBoxForeignKey
			// 
			this.comboBoxForeignKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxForeignKey.FormattingEnabled = true;
			this.comboBoxForeignKey.Location = new System.Drawing.Point(13, 103);
			this.comboBoxForeignKey.Name = "comboBoxForeignKey";
			this.comboBoxForeignKey.Size = new System.Drawing.Size(314, 21);
			this.comboBoxForeignKey.TabIndex = 3;
			this.comboBoxForeignKey.SelectedIndexChanged += new System.EventHandler(this.comboBoxKey2_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(333, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(314, 26);
			this.label3.TabIndex = 9;
			this.label3.Text = "ForeignKey";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(13, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(314, 26);
			this.label2.TabIndex = 8;
			this.label2.Text = "PrimaryKey";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// labelTableFK
			// 
			this.labelTableFK.AutoSize = true;
			this.labelTableFK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTableFK.Location = new System.Drawing.Point(333, 49);
			this.labelTableFK.Name = "labelTableFK";
			this.labelTableFK.Size = new System.Drawing.Size(314, 25);
			this.labelTableFK.TabIndex = 5;
			this.labelTableFK.Text = "Table2";
			this.labelTableFK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(634, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbName
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tbName, 2);
			this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbName.Location = new System.Drawing.Point(13, 26);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(634, 20);
			this.tbName.TabIndex = 1;
			this.tbName.Text = "Name";
			// 
			// labelTablePK
			// 
			this.labelTablePK.AutoSize = true;
			this.labelTablePK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTablePK.Location = new System.Drawing.Point(13, 49);
			this.labelTablePK.Name = "labelTablePK";
			this.labelTablePK.Size = new System.Drawing.Size(314, 25);
			this.labelTablePK.TabIndex = 4;
			this.labelTablePK.Text = "Table1";
			this.labelTablePK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// labelPrimaryKeyColumns
			// 
			// 
			// 
			// 
			this.labelPrimaryKeyColumns.BackgroundStyle.Class = "";
			this.labelPrimaryKeyColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPrimaryKeyColumns.Location = new System.Drawing.Point(13, 124);
			this.labelPrimaryKeyColumns.Name = "labelPrimaryKeyColumns";
			this.labelPrimaryKeyColumns.Size = new System.Drawing.Size(314, 121);
			this.labelPrimaryKeyColumns.TabIndex = 10;
			this.labelPrimaryKeyColumns.TextLineAlignment = System.Drawing.StringAlignment.Near;
			// 
			// labelForeignKeyColumns
			// 
			// 
			// 
			// 
			this.labelForeignKeyColumns.BackgroundStyle.Class = "";
			this.labelForeignKeyColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelForeignKeyColumns.Location = new System.Drawing.Point(333, 124);
			this.labelForeignKeyColumns.Name = "labelForeignKeyColumns";
			this.labelForeignKeyColumns.Size = new System.Drawing.Size(314, 121);
			this.labelForeignKeyColumns.TabIndex = 11;
			this.labelForeignKeyColumns.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelForeignKeyColumns.WordWrap = true;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx1.Controls.Add(this.panelEx3);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 56);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(660, 566);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 3;
			this.panelEx1.Text = "panelEx1";
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx3.Controls.Add(this.virtualPropertyGrid1);
			this.panelEx3.Controls.Add(this.expandablePanel1);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(0, 0);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(660, 566);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 3;
			// 
			// virtualPropertyGrid1
			// 
			this.virtualPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.virtualPropertyGrid1.Location = new System.Drawing.Point(0, 284);
			this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
			this.virtualPropertyGrid1.Size = new System.Drawing.Size(660, 168);
			this.virtualPropertyGrid1.TabIndex = 4;
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.expandablePanel1.Controls.Add(this.panelEx4);
			this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(660, 284);
			this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel1.Style.GradientAngle = 90;
			this.expandablePanel1.TabIndex = 3;
			this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel1.TitleStyle.GradientAngle = 90;
			this.expandablePanel1.TitleText = "Details";
			// 
			// panelEx4
			// 
			this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx4.Controls.Add(this.tableLayoutPanel1);
			this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx4.Location = new System.Drawing.Point(0, 26);
			this.panelEx4.Name = "panelEx4";
			this.panelEx4.Size = new System.Drawing.Size(660, 258);
			this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx4.Style.GradientAngle = 90;
			this.panelEx4.TabIndex = 3;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Class = "";
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// panelTitle
			// 
			this.panelTitle.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelTitle.Location = new System.Drawing.Point(0, 0);
			this.panelTitle.Name = "panelTitle";
			this.panelTitle.Size = new System.Drawing.Size(660, 32);
			this.panelTitle.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelTitle.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelTitle.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelTitle.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelTitle.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelTitle.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelTitle.Style.GradientAngle = 90;
			this.panelTitle.TabIndex = 4;
			this.panelTitle.Text = "Relationship Details";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeColumnMappingToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(215, 26);
			// 
			// removeColumnMappingToolStripMenuItem
			// 
			this.removeColumnMappingToolStripMenuItem.Name = "removeColumnMappingToolStripMenuItem";
			this.removeColumnMappingToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.removeColumnMappingToolStripMenuItem.Text = "Remove Column Mapping";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 32);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(660, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Image = global::ArchAngel.Providers.EntityModel.Properties.Resources.delete_a_16;
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// FormRelationship
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelEx1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.panelTitle);
			this.Name = "FormRelationship";
			this.Size = new System.Drawing.Size(660, 622);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panelEx1.ResumeLayout(false);
			this.panelEx3.ResumeLayout(false);
			this.expandablePanel1.ResumeLayout(false);
			this.panelEx4.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbName;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.PanelEx panelTitle;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem removeColumnMappingToolStripMenuItem;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
		private DevComponents.DotNetBar.PanelEx panelEx4;
		private VirtualPropertyGrid virtualPropertyGrid1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ComboBox comboBoxPrimaryKey;
		private System.Windows.Forms.ComboBox comboBoxForeignKey;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelTableFK;
		private System.Windows.Forms.Label labelTablePK;
		private DevComponents.DotNetBar.LabelX labelPrimaryKeyColumns;
		private DevComponents.DotNetBar.LabelX labelForeignKeyColumns;
	}
}