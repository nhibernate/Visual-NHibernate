namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormMapping
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMapping));
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbEntity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.cbTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonRemoveMapping = new System.Windows.Forms.ToolStripButton();
			this.gridControl = new ArchAngel.Providers.EntityModel.UI.ComboTreeGridControl();
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.expandablePanel1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx3.Location = new System.Drawing.Point(0, 0);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(775, 31);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 22;
			this.panelEx3.Text = "Mapping";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.06623F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.93377F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.cbEntity, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelX2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelX1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbTable, 1, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 14);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(741, 70);
			this.tableLayoutPanel1.TabIndex = 23;
			// 
			// cbEntity
			// 
			this.cbEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbEntity.DisplayMember = "Text";
			this.cbEntity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbEntity.FormattingEnabled = true;
			this.cbEntity.ItemHeight = 14;
			this.cbEntity.Location = new System.Drawing.Point(3, 32);
			this.cbEntity.Name = "cbEntity";
			this.cbEntity.Size = new System.Drawing.Size(364, 20);
			this.cbEntity.TabIndex = 1;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelX2.Location = new System.Drawing.Point(3, 3);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(364, 23);
			this.labelX2.TabIndex = 1;
			this.labelX2.Text = "Entity";
			this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelX1.Location = new System.Drawing.Point(373, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(365, 23);
			this.labelX1.TabIndex = 0;
			this.labelX1.Text = "Database Object";
			this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// cbTable
			// 
			this.cbTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbTable.DisplayMember = "Text";
			this.cbTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbTable.FormattingEnabled = true;
			this.cbTable.ItemHeight = 14;
			this.cbTable.Location = new System.Drawing.Point(373, 32);
			this.cbTable.Name = "cbTable";
			this.cbTable.Size = new System.Drawing.Size(365, 20);
			this.cbTable.TabIndex = 2;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRemoveMapping});
			this.toolStrip1.Location = new System.Drawing.Point(0, 31);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(775, 25);
			this.toolStrip1.TabIndex = 25;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// buttonRemoveMapping
			// 
			this.buttonRemoveMapping.Image = ((System.Drawing.Image)(resources.GetObject("buttonRemoveMapping.Image")));
			this.buttonRemoveMapping.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonRemoveMapping.Name = "buttonRemoveMapping";
			this.buttonRemoveMapping.Size = new System.Drawing.Size(111, 22);
			this.buttonRemoveMapping.Text = "Delete Mapping";
			this.buttonRemoveMapping.ToolTipText = "Remove Mapping";
			// 
			// gridControl
			// 
			this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gridControl.LeftTitle = "Property";
			this.gridControl.Location = new System.Drawing.Point(16, 90);
			this.gridControl.Name = "gridControl";
			this.gridControl.RightTitle = "Column";
			this.gridControl.Size = new System.Drawing.Size(741, 313);
			this.gridControl.TabIndex = 24;
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.expandablePanel1.Controls.Add(this.panelEx2);
			this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandablePanel1.Location = new System.Drawing.Point(0, 56);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(775, 453);
			this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel1.Style.GradientAngle = 90;
			this.expandablePanel1.TabIndex = 26;
			this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel1.TitleStyle.GradientAngle = 90;
			this.expandablePanel1.TitleText = "Details";
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx2.Controls.Add(this.gridControl);
			this.panelEx2.Controls.Add(this.tableLayoutPanel1);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(0, 26);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(775, 427);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 1;
			this.panelEx2.Text = "panelEx2";
			// 
			// virtualPropertyGrid1
			// 
			this.virtualPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.virtualPropertyGrid1.Location = new System.Drawing.Point(0, 509);
			this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
			this.virtualPropertyGrid1.Size = new System.Drawing.Size(775, 208);
			this.virtualPropertyGrid1.TabIndex = 27;
			// 
			// FormMapping
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.virtualPropertyGrid1);
			this.Controls.Add(this.expandablePanel1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.panelEx3);
			this.Name = "FormMapping";
			this.Size = new System.Drawing.Size(775, 750);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.expandablePanel1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.PanelEx panelEx3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbEntity;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbTable;
		private ComboTreeGridControl gridControl;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton buttonRemoveMapping;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
		private DevComponents.DotNetBar.PanelEx panelEx2;
		private VirtualPropertyGrid virtualPropertyGrid1;
	}
}
