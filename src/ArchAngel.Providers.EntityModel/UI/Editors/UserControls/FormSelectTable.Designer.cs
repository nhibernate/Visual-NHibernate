namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormSelectTable
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("One");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Two");
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelColumns = new System.Windows.Forms.Label();
            this.listViewColumns = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxDeleteMappedEntity = new System.Windows.Forms.CheckBox();
            this.checkBoxAddReferences = new System.Windows.Forms.CheckBox();
            this.buttonOk = new DevComponents.DotNetBar.ButtonX();
            this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(169, 121);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDescription.Location = new System.Drawing.Point(9, 176);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(169, 43);
            this.labelDescription.TabIndex = 13;
            this.labelDescription.Text = "Only tables that are mapped 1:1 or m:1 to currently mapped tabels are available.";
            // 
            // labelColumns
            // 
            this.labelColumns.AutoSize = true;
            this.labelColumns.Location = new System.Drawing.Point(203, 15);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(225, 13);
            this.labelColumns.TabIndex = 15;
            this.labelColumns.Text = "Add new properties mapped to these columns:";
            // 
            // listViewColumns
            // 
            this.listViewColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listViewColumns.Border.Class = "ListViewBorder";
            this.listViewColumns.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewColumns.CheckBoxes = true;
            this.listViewColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader1});
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.listViewColumns.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewColumns.Location = new System.Drawing.Point(206, 31);
            this.listViewColumns.Name = "listViewColumns";
            this.listViewColumns.Size = new System.Drawing.Size(409, 129);
            this.listViewColumns.TabIndex = 16;
            this.listViewColumns.UseCompatibleStateImageBehavior = false;
            this.listViewColumns.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Column Name";
            this.columnHeader3.Width = 122;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Data Type";
            this.columnHeader4.Width = 79;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Primary Key";
            this.columnHeader1.Width = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Select table to map:";
            // 
            // checkBoxDeleteMappedEntity
            // 
            this.checkBoxDeleteMappedEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxDeleteMappedEntity.AutoSize = true;
            this.checkBoxDeleteMappedEntity.Checked = true;
            this.checkBoxDeleteMappedEntity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDeleteMappedEntity.Location = new System.Drawing.Point(236, 175);
            this.checkBoxDeleteMappedEntity.Name = "checkBoxDeleteMappedEntity";
            this.checkBoxDeleteMappedEntity.Size = new System.Drawing.Size(302, 17);
            this.checkBoxDeleteMappedEntity.TabIndex = 18;
            this.checkBoxDeleteMappedEntity.Text = "Remove this table\'s currently mapped entity from the model";
            this.checkBoxDeleteMappedEntity.UseVisualStyleBackColor = true;
            this.checkBoxDeleteMappedEntity.Visible = false;
            // 
            // checkBoxAddReferences
            // 
            this.checkBoxAddReferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAddReferences.AutoSize = true;
            this.checkBoxAddReferences.Checked = true;
            this.checkBoxAddReferences.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddReferences.Location = new System.Drawing.Point(236, 198);
            this.checkBoxAddReferences.Name = "checkBoxAddReferences";
            this.checkBoxAddReferences.Size = new System.Drawing.Size(251, 17);
            this.checkBoxAddReferences.TabIndex = 19;
            this.checkBoxAddReferences.Text = "Add the references from currently mapped entity";
            this.checkBoxAddReferences.UseVisualStyleBackColor = true;
            this.checkBoxAddReferences.Visible = false;
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOk.Location = new System.Drawing.Point(227, 234);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOk.TabIndex = 20;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(324, 234);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormSelectTable
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(627, 269);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxAddReferences);
            this.Controls.Add(this.checkBoxDeleteMappedEntity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewColumns);
            this.Controls.Add(this.labelColumns);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labelDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormSelectTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormSelectTable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelColumns;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewColumns;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxDeleteMappedEntity;
        private System.Windows.Forms.CheckBox checkBoxAddReferences;
        private DevComponents.DotNetBar.ButtonX buttonOk;
        private DevComponents.DotNetBar.ButtonX buttonCancel;
    }
}