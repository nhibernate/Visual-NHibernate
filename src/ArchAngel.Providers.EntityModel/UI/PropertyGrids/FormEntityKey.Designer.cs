namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormEntityKey
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
			this.panelHeader = new DevComponents.DotNetBar.PanelEx();
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.buttonDeleteProperty = new DevComponents.DotNetBar.ButtonX();
			this.buttonAddProperty = new DevComponents.DotNetBar.ButtonX();
			this.listBoxProperties = new System.Windows.Forms.ListBox();
			this.radioButtonProperties = new System.Windows.Forms.RadioButton();
			this.comboBoxComponents = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.radioButtonComponent = new System.Windows.Forms.RadioButton();
			this.radioButtonNoKey = new System.Windows.Forms.RadioButton();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.labelEntityName = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.convertKeyToComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.expandablePanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelHeader
			// 
			this.panelHeader.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelHeader.Location = new System.Drawing.Point(0, 0);
			this.panelHeader.Name = "panelHeader";
			this.panelHeader.Size = new System.Drawing.Size(418, 31);
			this.panelHeader.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelHeader.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelHeader.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelHeader.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelHeader.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelHeader.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelHeader.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelHeader.Style.GradientAngle = 90;
			this.panelHeader.TabIndex = 2;
			this.panelHeader.Text = "Entity Key";
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.expandablePanel1.Controls.Add(this.buttonDeleteProperty);
			this.expandablePanel1.Controls.Add(this.buttonAddProperty);
			this.expandablePanel1.Controls.Add(this.listBoxProperties);
			this.expandablePanel1.Controls.Add(this.radioButtonProperties);
			this.expandablePanel1.Controls.Add(this.comboBoxComponents);
			this.expandablePanel1.Controls.Add(this.radioButtonComponent);
			this.expandablePanel1.Controls.Add(this.radioButtonNoKey);
			this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandablePanel1.Location = new System.Drawing.Point(0, 63);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(401, 302);
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
			// buttonDeleteProperty
			// 
			this.buttonDeleteProperty.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonDeleteProperty.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonDeleteProperty.Image = global::ArchAngel.Providers.EntityModel.Properties.Resources.delete_a_16;
			this.buttonDeleteProperty.Location = new System.Drawing.Point(132, 100);
			this.buttonDeleteProperty.Name = "buttonDeleteProperty";
			this.buttonDeleteProperty.Size = new System.Drawing.Size(24, 20);
			this.buttonDeleteProperty.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonDeleteProperty.TabIndex = 7;
			this.buttonDeleteProperty.Tooltip = "Add Property";
			this.buttonDeleteProperty.Click += new System.EventHandler(this.buttonDeleteProperty_Click);
			// 
			// buttonAddProperty
			// 
			this.buttonAddProperty.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonAddProperty.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonAddProperty.Image = global::ArchAngel.Providers.EntityModel.Properties.Resources.field_insert_b_16;
			this.buttonAddProperty.Location = new System.Drawing.Point(102, 100);
			this.buttonAddProperty.Name = "buttonAddProperty";
			this.buttonAddProperty.Size = new System.Drawing.Size(24, 20);
			this.buttonAddProperty.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonAddProperty.TabIndex = 6;
			this.buttonAddProperty.Tooltip = "Add Property";
			this.buttonAddProperty.Click += new System.EventHandler(this.buttonAddProperty_Click);
			// 
			// listBoxProperties
			// 
			this.listBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxProperties.FormattingEnabled = true;
			this.listBoxProperties.Location = new System.Drawing.Point(17, 123);
			this.listBoxProperties.Name = "listBoxProperties";
			this.listBoxProperties.Size = new System.Drawing.Size(376, 160);
			this.listBoxProperties.TabIndex = 5;
			this.listBoxProperties.SelectedIndexChanged += new System.EventHandler(this.listBoxProperties_SelectedIndexChanged);
			// 
			// radioButtonProperties
			// 
			this.radioButtonProperties.AutoSize = true;
			this.radioButtonProperties.Location = new System.Drawing.Point(17, 100);
			this.radioButtonProperties.Name = "radioButtonProperties";
			this.radioButtonProperties.Size = new System.Drawing.Size(72, 17);
			this.radioButtonProperties.TabIndex = 4;
			this.radioButtonProperties.TabStop = true;
			this.radioButtonProperties.Text = "Properties";
			this.radioButtonProperties.UseVisualStyleBackColor = true;
			this.radioButtonProperties.CheckedChanged += new System.EventHandler(this.radioButtonProperties_CheckedChanged);
			// 
			// comboBoxComponents
			// 
			this.comboBoxComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxComponents.DisplayMember = "Text";
			this.comboBoxComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxComponents.FormattingEnabled = true;
			this.comboBoxComponents.ItemHeight = 14;
			this.comboBoxComponents.Location = new System.Drawing.Point(102, 67);
			this.comboBoxComponents.Name = "comboBoxComponents";
			this.comboBoxComponents.Size = new System.Drawing.Size(291, 20);
			this.comboBoxComponents.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxComponents.TabIndex = 3;
			this.comboBoxComponents.SelectedIndexChanged += new System.EventHandler(this.comboBoxComponents_SelectedIndexChanged);
			// 
			// radioButtonComponent
			// 
			this.radioButtonComponent.AutoSize = true;
			this.radioButtonComponent.Location = new System.Drawing.Point(17, 70);
			this.radioButtonComponent.Name = "radioButtonComponent";
			this.radioButtonComponent.Size = new System.Drawing.Size(79, 17);
			this.radioButtonComponent.TabIndex = 2;
			this.radioButtonComponent.TabStop = true;
			this.radioButtonComponent.Text = "Component";
			this.radioButtonComponent.UseVisualStyleBackColor = true;
			this.radioButtonComponent.CheckedChanged += new System.EventHandler(this.radioButtonComponent_CheckedChanged);
			// 
			// radioButtonNoKey
			// 
			this.radioButtonNoKey.AutoSize = true;
			this.radioButtonNoKey.Location = new System.Drawing.Point(17, 39);
			this.radioButtonNoKey.Name = "radioButtonNoKey";
			this.radioButtonNoKey.Size = new System.Drawing.Size(60, 17);
			this.radioButtonNoKey.TabIndex = 1;
			this.radioButtonNoKey.TabStop = true;
			this.radioButtonNoKey.Text = "No Key";
			this.radioButtonNoKey.UseVisualStyleBackColor = true;
			this.radioButtonNoKey.CheckedChanged += new System.EventHandler(this.radioButtonNoKey_CheckedChanged);
			// 
			// panelEx1
			// 
			this.panelEx1.AutoScroll = true;
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.virtualPropertyGrid1);
			this.panelEx1.Controls.Add(this.expandablePanel1);
			this.panelEx1.Controls.Add(this.tableLayoutPanel1);
			this.panelEx1.Controls.Add(this.menuStrip1);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 31);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(418, 527);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 5;
			// 
			// virtualPropertyGrid1
			// 
			this.virtualPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.virtualPropertyGrid1.Location = new System.Drawing.Point(0, 365);
			this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
			this.virtualPropertyGrid1.Size = new System.Drawing.Size(401, 221);
			this.virtualPropertyGrid1.TabIndex = 5;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.labelEntityName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelX1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(401, 39);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// labelEntityName
			// 
			this.labelEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelEntityName.BackgroundStyle.Class = "";
			this.labelEntityName.Location = new System.Drawing.Point(58, 8);
			this.labelEntityName.Name = "labelEntityName";
			this.labelEntityName.Size = new System.Drawing.Size(335, 23);
			this.labelEntityName.TabIndex = 2;
			this.labelEntityName.Text = "Entity Name";
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.Location = new System.Drawing.Point(8, 8);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(41, 23);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "Entity:";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertKeyToComponentToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(401, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// convertKeyToComponentToolStripMenuItem
			// 
			this.convertKeyToComponentToolStripMenuItem.Name = "convertKeyToComponentToolStripMenuItem";
			this.convertKeyToComponentToolStripMenuItem.Size = new System.Drawing.Size(167, 20);
			this.convertKeyToComponentToolStripMenuItem.Text = "Convert Key To Component";
			this.convertKeyToComponentToolStripMenuItem.ToolTipText = "Creates a Component from the Key\'s Properties";
			this.convertKeyToComponentToolStripMenuItem.Click += new System.EventHandler(this.convertKeyToComponentToolStripMenuItem_Click);
			// 
			// FormEntityKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelEx1);
			this.Controls.Add(this.panelHeader);
			this.Name = "FormEntityKey";
			this.Size = new System.Drawing.Size(418, 558);
			this.expandablePanel1.ResumeLayout(false);
			this.expandablePanel1.PerformLayout();
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.PanelEx panelHeader;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
		private DevComponents.DotNetBar.LabelX labelEntityName;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private VirtualPropertyGrid virtualPropertyGrid1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevComponents.DotNetBar.ButtonX buttonAddProperty;
		private System.Windows.Forms.ListBox listBoxProperties;
		private System.Windows.Forms.RadioButton radioButtonProperties;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxComponents;
		private System.Windows.Forms.RadioButton radioButtonComponent;
		private System.Windows.Forms.RadioButton radioButtonNoKey;
		private DevComponents.DotNetBar.ButtonX buttonDeleteProperty;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem convertKeyToComponentToolStripMenuItem;
	}
}
