namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
    partial class FormKey
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKey));
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.textBoxName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.textBoxDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.listBoxColumn = new System.Windows.Forms.ListBox();
			this.comboBoxKeytype = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.expandablePanelDetails = new DevComponents.DotNetBar.ExpandablePanel();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.buttonDeleteColumn = new DevComponents.DotNetBar.ButtonX();
			this.buttonAddColumn = new DevComponents.DotNetBar.ButtonX();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.expandablePanelDetails.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// labelX1
			// 
			this.labelX1.AutoSize = true;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.Location = new System.Drawing.Point(16, 20);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(32, 15);
			this.labelX1.TabIndex = 13;
			this.labelX1.Text = "Name";
			// 
			// labelX5
			// 
			this.labelX5.AutoSize = true;
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.Class = "";
			this.labelX5.Location = new System.Drawing.Point(16, 72);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(31, 15);
			this.labelX5.TabIndex = 17;
			this.labelX5.Text = "Desc.";
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.Location = new System.Drawing.Point(16, 46);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(27, 15);
			this.labelX3.TabIndex = 15;
			this.labelX3.Text = "Type";
			// 
			// labelX4
			// 
			this.labelX4.AutoSize = true;
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.Class = "";
			this.labelX4.Location = new System.Drawing.Point(16, 125);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(46, 15);
			this.labelX4.TabIndex = 16;
			this.labelX4.Text = "Columns";
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.textBoxName.Border.Class = "TextBoxBorder";
			this.textBoxName.Location = new System.Drawing.Point(68, 15);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(410, 20);
			this.textBoxName.TabIndex = 1;
			// 
			// textBoxDescription
			// 
			this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.textBoxDescription.Border.Class = "TextBoxBorder";
			this.textBoxDescription.Location = new System.Drawing.Point(68, 67);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(410, 20);
			this.textBoxDescription.TabIndex = 3;
			// 
			// listBoxColumn
			// 
			this.listBoxColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxColumn.FormattingEnabled = true;
			this.listBoxColumn.Location = new System.Drawing.Point(68, 125);
			this.listBoxColumn.Name = "listBoxColumn";
			this.listBoxColumn.Size = new System.Drawing.Size(410, 147);
			this.listBoxColumn.TabIndex = 6;
			// 
			// comboBoxKeytype
			// 
			this.comboBoxKeytype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxKeytype.DisplayMember = "Text";
			this.comboBoxKeytype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxKeytype.FormattingEnabled = true;
			this.comboBoxKeytype.ItemHeight = 14;
			this.comboBoxKeytype.Location = new System.Drawing.Point(68, 41);
			this.comboBoxKeytype.Name = "comboBoxKeytype";
			this.comboBoxKeytype.Size = new System.Drawing.Size(410, 20);
			this.comboBoxKeytype.TabIndex = 2;
			// 
			// expandablePanelDetails
			// 
			this.expandablePanelDetails.AnimationTime = 0;
			this.expandablePanelDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.expandablePanelDetails.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanelDetails.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.expandablePanelDetails.Controls.Add(this.panelEx2);
			this.expandablePanelDetails.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandablePanelDetails.ExpandOnTitleClick = true;
			this.expandablePanelDetails.Location = new System.Drawing.Point(0, 55);
			this.expandablePanelDetails.Name = "expandablePanelDetails";
			this.expandablePanelDetails.Size = new System.Drawing.Size(500, 351);
			this.expandablePanelDetails.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanelDetails.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanelDetails.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanelDetails.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanelDetails.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanelDetails.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanelDetails.Style.GradientAngle = 90;
			this.expandablePanelDetails.TabIndex = 37;
			this.expandablePanelDetails.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanelDetails.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanelDetails.TitleStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("expandablePanelDetails.TitleStyle.BackgroundImage")));
			this.expandablePanelDetails.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.CenterLeft;
			this.expandablePanelDetails.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanelDetails.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanelDetails.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.expandablePanelDetails.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanelDetails.TitleStyle.GradientAngle = 90;
			this.expandablePanelDetails.TitleStyle.MarginLeft = 30;
			this.expandablePanelDetails.TitleText = "Details";
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx2.Controls.Add(this.buttonDeleteColumn);
			this.panelEx2.Controls.Add(this.buttonAddColumn);
			this.panelEx2.Controls.Add(this.listBoxColumn);
			this.panelEx2.Controls.Add(this.labelX4);
			this.panelEx2.Controls.Add(this.textBoxDescription);
			this.panelEx2.Controls.Add(this.labelX1);
			this.panelEx2.Controls.Add(this.labelX5);
			this.panelEx2.Controls.Add(this.labelX3);
			this.panelEx2.Controls.Add(this.textBoxName);
			this.panelEx2.Controls.Add(this.comboBoxKeytype);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(0, 26);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(500, 325);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 36;
			// 
			// buttonDeleteColumn
			// 
			this.buttonDeleteColumn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonDeleteColumn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonDeleteColumn.Image = global::ArchAngel.Providers.EntityModel.Properties.Resources.delete_a_16;
			this.buttonDeleteColumn.Location = new System.Drawing.Point(101, 96);
			this.buttonDeleteColumn.Name = "buttonDeleteColumn";
			this.buttonDeleteColumn.Size = new System.Drawing.Size(30, 23);
			this.buttonDeleteColumn.TabIndex = 5;
			this.buttonDeleteColumn.Click += new System.EventHandler(this.buttonDeleteColumn_Click);
			// 
			// buttonAddColumn
			// 
			this.buttonAddColumn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonAddColumn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonAddColumn.Image = global::ArchAngel.Providers.EntityModel.Properties.Resources.field_insert_b_16;
			this.buttonAddColumn.Location = new System.Drawing.Point(68, 96);
			this.buttonAddColumn.Name = "buttonAddColumn";
			this.buttonAddColumn.Size = new System.Drawing.Size(27, 23);
			this.buttonAddColumn.TabIndex = 4;
			this.buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx3.Location = new System.Drawing.Point(0, 0);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(500, 31);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 21;
			this.panelEx3.Text = "Key";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 31);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(500, 24);
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
			// virtualPropertyGrid1
			// 
			this.virtualPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.virtualPropertyGrid1.Location = new System.Drawing.Point(0, 406);
			this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
			this.virtualPropertyGrid1.Size = new System.Drawing.Size(500, 126);
			this.virtualPropertyGrid1.TabIndex = 38;
			// 
			// FormKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
			this.Controls.Add(this.virtualPropertyGrid1);
			this.Controls.Add(this.expandablePanelDetails);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.panelEx3);
			this.Name = "FormKey";
			this.Size = new System.Drawing.Size(500, 683);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.expandablePanelDetails.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.panelEx2.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
		private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxName;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxDescription;
        private System.Windows.Forms.ListBox listBoxColumn;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanelDetails;
		private DevComponents.DotNetBar.PanelEx panelEx2;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxKeytype;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private VirtualPropertyGrid virtualPropertyGrid1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private DevComponents.DotNetBar.ButtonX buttonDeleteColumn;
		private DevComponents.DotNetBar.ButtonX buttonAddColumn;
    }
}