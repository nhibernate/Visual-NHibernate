namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormComponentMappingEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComponentMappingEditor));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.comboBoxComponents = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonOk = new DevComponents.DotNetBar.ButtonX();
            this.comboBoxExEntities = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textBoxComponentName = new System.Windows.Forms.TextBox();
            this.labelComponentNameInput = new System.Windows.Forms.Label();
            this.pictureEntity = new System.Windows.Forms.PictureBox();
            this.pictureComponent = new System.Windows.Forms.PictureBox();
            this.labelColumns = new System.Windows.Forms.Label();
            this.labelComponentName = new System.Windows.Forms.Label();
            this.labelSelect = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureComponent)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelSelect);
            this.panelEx1.Controls.Add(this.comboBoxComponents);
            this.panelEx1.Controls.Add(this.buttonOk);
            this.panelEx1.Controls.Add(this.comboBoxExEntities);
            this.panelEx1.Controls.Add(this.textBoxComponentName);
            this.panelEx1.Controls.Add(this.labelComponentNameInput);
            this.panelEx1.Controls.Add(this.pictureEntity);
            this.panelEx1.Controls.Add(this.pictureComponent);
            this.panelEx1.Controls.Add(this.labelColumns);
            this.panelEx1.Controls.Add(this.labelComponentName);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(370, 223);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            // 
            // comboBoxComponents
            // 
            this.comboBoxComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxComponents.DisplayMember = "Text";
            this.comboBoxComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComponents.FormattingEnabled = true;
            this.comboBoxComponents.ItemHeight = 14;
            this.comboBoxComponents.Location = new System.Drawing.Point(104, 35);
            this.comboBoxComponents.Name = "comboBoxComponents";
            this.comboBoxComponents.Size = new System.Drawing.Size(232, 20);
            this.comboBoxComponents.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxComponents.TabIndex = 55;
            this.comboBoxComponents.SelectedIndexChanged += new System.EventHandler(this.comboBoxComponents_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOk.Location = new System.Drawing.Point(152, 191);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(67, 20);
            this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOk.TabIndex = 54;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // comboBoxExEntities
            // 
            this.comboBoxExEntities.DisplayMember = "Text";
            this.comboBoxExEntities.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxExEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExEntities.FormattingEnabled = true;
            this.comboBoxExEntities.ItemHeight = 14;
            this.comboBoxExEntities.Location = new System.Drawing.Point(262, 74);
            this.comboBoxExEntities.Name = "comboBoxExEntities";
            this.comboBoxExEntities.Size = new System.Drawing.Size(121, 20);
            this.comboBoxExEntities.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxExEntities.TabIndex = 8;
            this.comboBoxExEntities.SelectedIndexChanged += new System.EventHandler(this.comboBoxExEntities_SelectedIndexChanged);
            // 
            // textBoxComponentName
            // 
            this.textBoxComponentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComponentName.Location = new System.Drawing.Point(104, 16);
            this.textBoxComponentName.Name = "textBoxComponentName";
            this.textBoxComponentName.Size = new System.Drawing.Size(230, 20);
            this.textBoxComponentName.TabIndex = 7;
            // 
            // labelComponentNameInput
            // 
            this.labelComponentNameInput.AutoSize = true;
            this.labelComponentNameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComponentNameInput.ForeColor = System.Drawing.Color.White;
            this.labelComponentNameInput.Location = new System.Drawing.Point(13, 19);
            this.labelComponentNameInput.Name = "labelComponentNameInput";
            this.labelComponentNameInput.Size = new System.Drawing.Size(93, 13);
            this.labelComponentNameInput.TabIndex = 6;
            this.labelComponentNameInput.Text = "Component name:";
            // 
            // pictureEntity
            // 
            this.pictureEntity.Image = ((System.Drawing.Image)(resources.GetObject("pictureEntity.Image")));
            this.pictureEntity.Location = new System.Drawing.Point(235, 58);
            this.pictureEntity.Name = "pictureEntity";
            this.pictureEntity.Size = new System.Drawing.Size(16, 16);
            this.pictureEntity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureEntity.TabIndex = 5;
            this.pictureEntity.TabStop = false;
            // 
            // pictureComponent
            // 
            this.pictureComponent.Image = ((System.Drawing.Image)(resources.GetObject("pictureComponent.Image")));
            this.pictureComponent.Location = new System.Drawing.Point(15, 58);
            this.pictureComponent.Name = "pictureComponent";
            this.pictureComponent.Size = new System.Drawing.Size(16, 16);
            this.pictureComponent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureComponent.TabIndex = 4;
            this.pictureComponent.TabStop = false;
            // 
            // labelColumns
            // 
            this.labelColumns.AutoSize = true;
            this.labelColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelColumns.ForeColor = System.Drawing.Color.White;
            this.labelColumns.Location = new System.Drawing.Point(265, 58);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(71, 13);
            this.labelColumns.TabIndex = 3;
            this.labelColumns.Text = "EntityName";
            // 
            // labelComponentName
            // 
            this.labelComponentName.AutoSize = true;
            this.labelComponentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComponentName.ForeColor = System.Drawing.Color.White;
            this.labelComponentName.Location = new System.Drawing.Point(45, 58);
            this.labelComponentName.Name = "labelComponentName";
            this.labelComponentName.Size = new System.Drawing.Size(102, 13);
            this.labelComponentName.TabIndex = 2;
            this.labelComponentName.Text = "ComponentName";
            // 
            // labelSelect
            // 
            this.labelSelect.AutoSize = true;
            this.labelSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelect.ForeColor = System.Drawing.Color.White;
            this.labelSelect.Location = new System.Drawing.Point(13, 39);
            this.labelSelect.Name = "labelSelect";
            this.labelSelect.Size = new System.Drawing.Size(40, 13);
            this.labelSelect.TabIndex = 56;
            this.labelSelect.Text = "Select:";
            // 
            // FormComponentMappingEditor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 223);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormComponentMappingEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Component mappings";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureComponent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label labelColumns;
        private System.Windows.Forms.Label labelComponentName;
        private System.Windows.Forms.PictureBox pictureEntity;
        private System.Windows.Forms.PictureBox pictureComponent;
        private System.Windows.Forms.TextBox textBoxComponentName;
        private System.Windows.Forms.Label labelComponentNameInput;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExEntities;
        private DevComponents.DotNetBar.ButtonX buttonOk;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxComponents;
        private System.Windows.Forms.Label labelSelect;
    }
}
