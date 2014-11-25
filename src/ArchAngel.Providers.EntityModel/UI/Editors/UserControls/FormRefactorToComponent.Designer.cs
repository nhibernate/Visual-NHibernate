namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormRefactorToComponent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRefactorToComponent));
            this.comboBoxComponents = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textBoxNewComponentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureEntity = new System.Windows.Forms.PictureBox();
            this.pictureComponent = new System.Windows.Forms.PictureBox();
            this.labelEntityName = new System.Windows.Forms.Label();
            this.labelComponentName = new System.Windows.Forms.Label();
            this.buttonOk = new DevComponents.DotNetBar.ButtonX();
            this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxComponentEntityName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureComponent)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxComponents
            // 
            this.comboBoxComponents.DisplayMember = "Text";
            this.comboBoxComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComponents.FormattingEnabled = true;
            this.comboBoxComponents.ItemHeight = 14;
            this.comboBoxComponents.Location = new System.Drawing.Point(31, 37);
            this.comboBoxComponents.Name = "comboBoxComponents";
            this.comboBoxComponents.Size = new System.Drawing.Size(200, 20);
            this.comboBoxComponents.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxComponents.TabIndex = 0;
            this.comboBoxComponents.SelectedIndexChanged += new System.EventHandler(this.comboBoxComponents_SelectedIndexChanged);
            // 
            // textBoxNewComponentName
            // 
            this.textBoxNewComponentName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewComponentName.Location = new System.Drawing.Point(249, 37);
            this.textBoxNewComponentName.Name = "textBoxNewComponentName";
            this.textBoxNewComponentName.Size = new System.Drawing.Size(222, 20);
            this.textBoxNewComponentName.TabIndex = 42;
            this.textBoxNewComponentName.Text = "New component name...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Select component";
            // 
            // pictureEntity
            // 
            this.pictureEntity.Image = ((System.Drawing.Image)(resources.GetObject("pictureEntity.Image")));
            this.pictureEntity.Location = new System.Drawing.Point(32, 98);
            this.pictureEntity.Name = "pictureEntity";
            this.pictureEntity.Size = new System.Drawing.Size(16, 16);
            this.pictureEntity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureEntity.TabIndex = 51;
            this.pictureEntity.TabStop = false;
            // 
            // pictureComponent
            // 
            this.pictureComponent.Image = ((System.Drawing.Image)(resources.GetObject("pictureComponent.Image")));
            this.pictureComponent.Location = new System.Drawing.Point(286, 98);
            this.pictureComponent.Name = "pictureComponent";
            this.pictureComponent.Size = new System.Drawing.Size(16, 16);
            this.pictureComponent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureComponent.TabIndex = 50;
            this.pictureComponent.TabStop = false;
            // 
            // labelEntityName
            // 
            this.labelEntityName.AutoSize = true;
            this.labelEntityName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEntityName.ForeColor = System.Drawing.Color.White;
            this.labelEntityName.Location = new System.Drawing.Point(62, 98);
            this.labelEntityName.Name = "labelEntityName";
            this.labelEntityName.Size = new System.Drawing.Size(71, 13);
            this.labelEntityName.TabIndex = 49;
            this.labelEntityName.Text = "EntityName";
            // 
            // labelComponentName
            // 
            this.labelComponentName.AutoSize = true;
            this.labelComponentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComponentName.ForeColor = System.Drawing.Color.White;
            this.labelComponentName.Location = new System.Drawing.Point(305, 79);
            this.labelComponentName.Name = "labelComponentName";
            this.labelComponentName.Size = new System.Drawing.Size(153, 13);
            this.labelComponentName.TabIndex = 48;
            this.labelComponentName.Text = "Component name in entity";
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOk.Location = new System.Drawing.Point(171, 203);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(67, 20);
            this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOk.TabIndex = 52;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCancel.Location = new System.Drawing.Point(266, 203);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(67, 20);
            this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonCancel.TabIndex = 53;
            this.buttonCancel.Text = "Cancel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(246, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Component type name";
            // 
            // textBoxComponentEntityName
            // 
            this.textBoxComponentEntityName.Location = new System.Drawing.Point(308, 95);
            this.textBoxComponentEntityName.Name = "textBoxComponentEntityName";
            this.textBoxComponentEntityName.Size = new System.Drawing.Size(163, 20);
            this.textBoxComponentEntityName.TabIndex = 55;
            this.textBoxComponentEntityName.Text = "NewComponentProperty";
            // 
            // FormRefactorToComponent
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(505, 235);
            this.Controls.Add(this.textBoxComponentEntityName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.pictureEntity);
            this.Controls.Add(this.pictureComponent);
            this.Controls.Add(this.labelEntityName);
            this.Controls.Add(this.labelComponentName);
            this.Controls.Add(this.textBoxNewComponentName);
            this.Controls.Add(this.comboBoxComponents);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormRefactorToComponent";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Refactor to component";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureComponent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxComponents;
        private System.Windows.Forms.TextBox textBoxNewComponentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureEntity;
        private System.Windows.Forms.PictureBox pictureComponent;
        private System.Windows.Forms.Label labelEntityName;
        private System.Windows.Forms.Label labelComponentName;
        private DevComponents.DotNetBar.ButtonX buttonOk;
        private DevComponents.DotNetBar.ButtonX buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxComponentEntityName;
    }
}