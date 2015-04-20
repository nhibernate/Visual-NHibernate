namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormDiscriminator
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
            this.comboBoxColumn = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboBoxOperator = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textBoxValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.DisplayMember = "Text";
            this.comboBoxColumn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.ItemHeight = 14;
            this.comboBoxColumn.Location = new System.Drawing.Point(14, 12);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(141, 20);
            this.comboBoxColumn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxColumn.TabIndex = 5;
            this.comboBoxColumn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxEx2_KeyDown);
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.DisplayMember = "Text";
            this.comboBoxOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.ItemHeight = 14;
            this.comboBoxOperator.Location = new System.Drawing.Point(161, 12);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(55, 20);
            this.comboBoxOperator.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxOperator.TabIndex = 4;
            this.comboBoxOperator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxEx1_KeyDown);
            // 
            // textBoxValue
            // 
            // 
            // 
            // 
            this.textBoxValue.Border.Class = "TextBoxBorder";
            this.textBoxValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxValue.Location = new System.Drawing.Point(222, 12);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(184, 20);
            this.textBoxValue.TabIndex = 3;
            this.textBoxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxX1_KeyDown);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(132, 65);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(222, 65);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormDiscriminator
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(421, 104);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.comboBoxOperator);
            this.Controls.Add(this.textBoxValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormDiscriminator";
            this.Text = "Discriminator";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDiscriminator_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxColumn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxOperator;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxValue;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}