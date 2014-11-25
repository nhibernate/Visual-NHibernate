namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormCreateNewRelationship
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
			this.comboBoxPK = new System.Windows.Forms.ComboBox();
			this.comboBoxFK = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelTableFK = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.labelTablePK = new System.Windows.Forms.Label();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonCreate = new DevComponents.DotNetBar.ButtonX();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeColumnMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx3.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.comboBoxPK, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFK, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.labelTableFK, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbName, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelTablePK, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 137);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// comboBoxPK
			// 
			this.comboBoxPK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPK.FormattingEnabled = true;
			this.comboBoxPK.Location = new System.Drawing.Point(8, 98);
			this.comboBoxPK.Name = "comboBoxPK";
			this.comboBoxPK.Size = new System.Drawing.Size(306, 21);
			this.comboBoxPK.TabIndex = 2;
			this.comboBoxPK.SelectedIndexChanged += new System.EventHandler(this.comboBoxKey1_SelectedIndexChanged);
			// 
			// comboBoxFK
			// 
			this.comboBoxFK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFK.FormattingEnabled = true;
			this.comboBoxFK.Location = new System.Drawing.Point(320, 98);
			this.comboBoxFK.Name = "comboBoxFK";
			this.comboBoxFK.Size = new System.Drawing.Size(306, 21);
			this.comboBoxFK.TabIndex = 3;
			this.comboBoxFK.SelectedIndexChanged += new System.EventHandler(this.comboBoxKey2_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(320, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(306, 26);
			this.label3.TabIndex = 9;
			this.label3.Text = "ForeignKey";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(8, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(306, 26);
			this.label2.TabIndex = 8;
			this.label2.Text = "PrimaryKey";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// labelTableFK
			// 
			this.labelTableFK.AutoSize = true;
			this.labelTableFK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTableFK.Location = new System.Drawing.Point(320, 44);
			this.labelTableFK.Name = "labelTableFK";
			this.labelTableFK.Size = new System.Drawing.Size(306, 25);
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
			this.label1.Location = new System.Drawing.Point(8, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(618, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbName
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tbName, 2);
			this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbName.Location = new System.Drawing.Point(8, 21);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(618, 20);
			this.tbName.TabIndex = 1;
			this.tbName.Text = "Name";
			// 
			// labelTablePK
			// 
			this.labelTablePK.AutoSize = true;
			this.labelTablePK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTablePK.Location = new System.Drawing.Point(8, 44);
			this.labelTablePK.Name = "labelTablePK";
			this.labelTablePK.Size = new System.Drawing.Size(306, 25);
			this.labelTablePK.TabIndex = 4;
			this.labelTablePK.Text = "Table1";
			this.labelTablePK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx1.Controls.Add(this.panelEx3);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(644, 182);
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
			this.panelEx3.Controls.Add(this.buttonCancel);
			this.panelEx3.Controls.Add(this.buttonCreate);
			this.panelEx3.Controls.Add(this.tableLayoutPanel1);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(0, 0);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(644, 182);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 3;
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCancel.Location = new System.Drawing.Point(556, 150);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonCreate
			// 
			this.buttonCreate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCreate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCreate.Location = new System.Drawing.Point(475, 150);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(75, 23);
			this.buttonCreate.TabIndex = 4;
			this.buttonCreate.Text = "Create";
			this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
			// 
			// elementStyle1
			// 
			this.elementStyle1.Class = "";
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
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
			// FormCreateNewRelationship
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 182);
			this.Controls.Add(this.panelEx1);
			this.Name = "FormCreateNewRelationship";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panelEx1.ResumeLayout(false);
			this.panelEx3.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbName;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem removeColumnMappingToolStripMenuItem;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private System.Windows.Forms.ComboBox comboBoxPK;
		private System.Windows.Forms.ComboBox comboBoxFK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelTableFK;
		private System.Windows.Forms.Label labelTablePK;
		private DevComponents.DotNetBar.ButtonX buttonCancel;
		private DevComponents.DotNetBar.ButtonX buttonCreate;
	}
}