namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormReferenceEditor2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReferenceEditor2));
			this.comboBoxMappedTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboBoxMappedRelationship = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.textBoxEndName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.comboBoxCardinality = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.checkBoxIncludeForeignKey = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelName = new System.Windows.Forms.Label();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
			this.panelEx1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxMappedTable
			// 
			this.comboBoxMappedTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMappedTable.DisplayMember = "Text";
			this.comboBoxMappedTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxMappedTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMappedTable.FormattingEnabled = true;
			this.comboBoxMappedTable.ItemHeight = 14;
			this.comboBoxMappedTable.Location = new System.Drawing.Point(123, 45);
			this.comboBoxMappedTable.Name = "comboBoxMappedTable";
			this.comboBoxMappedTable.Size = new System.Drawing.Size(204, 20);
			this.comboBoxMappedTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxMappedTable.TabIndex = 7;
			this.comboBoxMappedTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxMappedTable_SelectedIndexChanged);
			// 
			// comboBoxMappedRelationship
			// 
			this.comboBoxMappedRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMappedRelationship.DisplayMember = "Text";
			this.comboBoxMappedRelationship.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxMappedRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMappedRelationship.FormattingEnabled = true;
			this.comboBoxMappedRelationship.ItemHeight = 14;
			this.comboBoxMappedRelationship.Location = new System.Drawing.Point(123, 71);
			this.comboBoxMappedRelationship.Name = "comboBoxMappedRelationship";
			this.comboBoxMappedRelationship.Size = new System.Drawing.Size(204, 20);
			this.comboBoxMappedRelationship.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxMappedRelationship.TabIndex = 9;
			this.comboBoxMappedRelationship.SelectedIndexChanged += new System.EventHandler(this.comboBoxMappedRelationship_SelectedIndexChanged);
			// 
			// textBoxEndName
			// 
			// 
			// 
			// 
			this.textBoxEndName.Border.Class = "TextBoxBorder";
			this.textBoxEndName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxEndName.Location = new System.Drawing.Point(147, 3);
			this.textBoxEndName.Name = "textBoxEndName";
			this.textBoxEndName.Size = new System.Drawing.Size(102, 20);
			this.textBoxEndName.TabIndex = 0;
			this.textBoxEndName.TextChanged += new System.EventHandler(this.textBoxEndName_TextChanged);
			// 
			// comboBoxCardinality
			// 
			this.comboBoxCardinality.DisplayMember = "Text";
			this.comboBoxCardinality.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxCardinality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCardinality.FormattingEnabled = true;
			this.comboBoxCardinality.ItemHeight = 14;
			this.comboBoxCardinality.Location = new System.Drawing.Point(81, 3);
			this.comboBoxCardinality.Name = "comboBoxCardinality";
			this.comboBoxCardinality.Size = new System.Drawing.Size(60, 20);
			this.comboBoxCardinality.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxCardinality.TabIndex = 5;
			this.comboBoxCardinality.SelectedIndexChanged += new System.EventHandler(this.comboBoxCardinality_SelectedIndexChanged);
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.checkBoxIncludeForeignKey);
			this.panelEx1.Controls.Add(this.label1);
			this.panelEx1.Controls.Add(this.label2);
			this.panelEx1.Controls.Add(this.virtualPropertyGrid1);
			this.panelEx1.Controls.Add(this.comboBoxMappedRelationship);
			this.panelEx1.Controls.Add(this.pictureBox1);
			this.panelEx1.Controls.Add(this.comboBoxMappedTable);
			this.panelEx1.Controls.Add(this.flowLayoutPanel1);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(351, 302);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 1;
			// 
			// checkBoxIncludeForeignKey
			// 
			this.checkBoxIncludeForeignKey.AutoSize = true;
			this.checkBoxIncludeForeignKey.ForeColor = System.Drawing.Color.White;
			this.checkBoxIncludeForeignKey.Location = new System.Drawing.Point(123, 97);
			this.checkBoxIncludeForeignKey.Name = "checkBoxIncludeForeignKey";
			this.checkBoxIncludeForeignKey.Size = new System.Drawing.Size(159, 17);
			this.checkBoxIncludeForeignKey.TabIndex = 13;
			this.checkBoxIncludeForeignKey.Text = "Include foreign key column?";
			this.checkBoxIncludeForeignKey.UseVisualStyleBackColor = true;
			this.checkBoxIncludeForeignKey.CheckedChanged += new System.EventHandler(this.checkBoxIncludeForeignKey_CheckedChanged);
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(7, 71);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 20);
			this.label1.TabIndex = 12;
			this.label1.Text = "Mapped relationship";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(16, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 20);
			this.label2.TabIndex = 11;
			this.label2.Text = "Association table";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(332, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.labelName);
			this.flowLayoutPanel1.Controls.Add(this.comboBoxCardinality);
			this.flowLayoutPanel1.Controls.Add(this.textBoxEndName);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 9);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(326, 26);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(20, 6);
			this.labelName.Margin = new System.Windows.Forms.Padding(20, 6, 3, 0);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(55, 13);
			this.labelName.TabIndex = 6;
			this.labelName.Text = "One x has";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// virtualPropertyGrid1
			// 
			this.virtualPropertyGrid1.Location = new System.Drawing.Point(43, 157);
			this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
			this.virtualPropertyGrid1.Size = new System.Drawing.Size(228, 98);
			this.virtualPropertyGrid1.TabIndex = 10;
			this.virtualPropertyGrid1.Resize += new System.EventHandler(this.virtualPropertyGrid1_Resize);
			// 
			// FormReferenceEditor2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelEx1);
			this.Name = "FormReferenceEditor2";
			this.Size = new System.Drawing.Size(351, 302);
			this.VisibleChanged += new System.EventHandler(this.FormReferenceEditor2_VisibleChanged);
			this.MouseLeave += new System.EventHandler(this.FormReferenceEditor2_MouseLeave);
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private DevComponents.DotNetBar.Controls.TextBoxX textBoxEndName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCardinality;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxMappedTable;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxMappedRelationship;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid virtualPropertyGrid1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxIncludeForeignKey;
    }
}
