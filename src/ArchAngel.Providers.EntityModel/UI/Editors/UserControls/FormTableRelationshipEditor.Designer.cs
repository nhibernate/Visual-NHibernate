namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormTableRelationshipEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTableRelationshipEditor));
            this.labelForeignTable = new System.Windows.Forms.Label();
            this.comboBoxForeignTable = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTableName = new System.Windows.Forms.Label();
            this.checkBoxForeignUnique = new System.Windows.Forms.CheckBox();
            this.checkBoxPrimaryUnique = new System.Windows.Forms.CheckBox();
            this.labelSelectForeignTable = new System.Windows.Forms.Label();
            this.buttonOk = new DevComponents.DotNetBar.ButtonX();
            this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonAddColumn = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelForeignTable
            // 
            this.labelForeignTable.AutoSize = true;
            this.labelForeignTable.BackColor = System.Drawing.Color.Transparent;
            this.labelForeignTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForeignTable.ForeColor = System.Drawing.Color.White;
            this.labelForeignTable.Location = new System.Drawing.Point(304, 28);
            this.labelForeignTable.Name = "labelForeignTable";
            this.labelForeignTable.Size = new System.Drawing.Size(101, 13);
            this.labelForeignTable.TabIndex = 9;
            this.labelForeignTable.Text = "Select foreign table:";
            this.labelForeignTable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxForeignTable
            // 
            this.comboBoxForeignTable.DisplayMember = "Text";
            this.comboBoxForeignTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxForeignTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxForeignTable.FormattingEnabled = true;
            this.comboBoxForeignTable.ItemHeight = 14;
            this.comboBoxForeignTable.Location = new System.Drawing.Point(171, 15);
            this.comboBoxForeignTable.Name = "comboBoxForeignTable";
            this.comboBoxForeignTable.Size = new System.Drawing.Size(170, 20);
            this.comboBoxForeignTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxForeignTable.TabIndex = 8;
            this.comboBoxForeignTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxForeignTable_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(439, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelTableName
            // 
            this.labelTableName.AutoSize = true;
            this.labelTableName.BackColor = System.Drawing.Color.Transparent;
            this.labelTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTableName.ForeColor = System.Drawing.Color.White;
            this.labelTableName.Location = new System.Drawing.Point(68, 28);
            this.labelTableName.Name = "labelTableName";
            this.labelTableName.Size = new System.Drawing.Size(41, 13);
            this.labelTableName.TabIndex = 13;
            this.labelTableName.Text = "TableA";
            this.labelTableName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // checkBoxForeignUnique
            // 
            this.checkBoxForeignUnique.AutoSize = true;
            this.checkBoxForeignUnique.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxForeignUnique.ForeColor = System.Drawing.Color.White;
            this.checkBoxForeignUnique.Location = new System.Drawing.Point(326, 219);
            this.checkBoxForeignUnique.Name = "checkBoxForeignUnique";
            this.checkBoxForeignUnique.Size = new System.Drawing.Size(60, 17);
            this.checkBoxForeignUnique.TabIndex = 19;
            this.checkBoxForeignUnique.Text = "Unique";
            this.checkBoxForeignUnique.UseVisualStyleBackColor = false;
            // 
            // checkBoxPrimaryUnique
            // 
            this.checkBoxPrimaryUnique.AutoSize = true;
            this.checkBoxPrimaryUnique.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPrimaryUnique.ForeColor = System.Drawing.Color.White;
            this.checkBoxPrimaryUnique.Location = new System.Drawing.Point(47, 226);
            this.checkBoxPrimaryUnique.Name = "checkBoxPrimaryUnique";
            this.checkBoxPrimaryUnique.Size = new System.Drawing.Size(60, 17);
            this.checkBoxPrimaryUnique.TabIndex = 20;
            this.checkBoxPrimaryUnique.Text = "Unique";
            this.checkBoxPrimaryUnique.UseVisualStyleBackColor = false;
            // 
            // labelSelectForeignTable
            // 
            this.labelSelectForeignTable.AutoSize = true;
            this.labelSelectForeignTable.BackColor = System.Drawing.Color.Transparent;
            this.labelSelectForeignTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectForeignTable.ForeColor = System.Drawing.Color.White;
            this.labelSelectForeignTable.Location = new System.Drawing.Point(12, 15);
            this.labelSelectForeignTable.Name = "labelSelectForeignTable";
            this.labelSelectForeignTable.Size = new System.Drawing.Size(97, 13);
            this.labelSelectForeignTable.TabIndex = 21;
            this.labelSelectForeignTable.Text = "Select table to link:";
            this.labelSelectForeignTable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOk.Location = new System.Drawing.Point(144, 267);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOk.TabIndex = 22;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonCancel.Location = new System.Drawing.Point(239, 267);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAddColumn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAddColumn.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddColumn.Image")));
            this.buttonAddColumn.Location = new System.Drawing.Point(184, 220);
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.buttonAddColumn.Size = new System.Drawing.Size(89, 23);
            this.buttonAddColumn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAddColumn.TabIndex = 24;
            this.buttonAddColumn.Text = "Add column";
            this.buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
            // 
            // FormTableRelationshipEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.buttonAddColumn);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelSelectForeignTable);
            this.Controls.Add(this.checkBoxPrimaryUnique);
            this.Controls.Add(this.checkBoxForeignUnique);
            this.Controls.Add(this.labelTableName);
            this.Controls.Add(this.labelForeignTable);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBoxForeignTable);
            this.Name = "FormTableRelationshipEditor";
            this.Size = new System.Drawing.Size(458, 296);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelForeignTable;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxForeignTable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTableName;
        private System.Windows.Forms.CheckBox checkBoxForeignUnique;
        private System.Windows.Forms.CheckBox checkBoxPrimaryUnique;
        private System.Windows.Forms.Label labelSelectForeignTable;
        private DevComponents.DotNetBar.ButtonX buttonOk;
        private DevComponents.DotNetBar.ButtonX buttonCancel;
        private DevComponents.DotNetBar.ButtonX buttonAddColumn;
    }
}
