namespace ArchAngel.Workbench
{
    partial class FormColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColumn));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxAliasDisplay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxIsNullable = new System.Windows.Forms.CheckBox();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCharacterMaximumLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOrdinalPosition = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDefault = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(265, 165);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(184, 165);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 15;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Location = new System.Drawing.Point(53, 32);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(250, 20);
            this.textBoxAlias.TabIndex = 3;
            this.textBoxAlias.TextChanged += new System.EventHandler(this.textBoxAlias_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alias";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(53, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(250, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // textBoxAliasDisplay
            // 
            this.textBoxAliasDisplay.Location = new System.Drawing.Point(53, 58);
            this.textBoxAliasDisplay.Name = "textBoxAliasDisplay";
            this.textBoxAliasDisplay.Size = new System.Drawing.Size(250, 20);
            this.textBoxAliasDisplay.TabIndex = 5;
            this.textBoxAliasDisplay.TextChanged += new System.EventHandler(this.textBoxAliasDisplay_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Alias\r\nDisplay";
            // 
            // checkBoxIsNullable
            // 
            this.checkBoxIsNullable.AutoSize = true;
            this.checkBoxIsNullable.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxIsNullable.Location = new System.Drawing.Point(53, 84);
            this.checkBoxIsNullable.Name = "checkBoxIsNullable";
            this.checkBoxIsNullable.Size = new System.Drawing.Size(75, 17);
            this.checkBoxIsNullable.TabIndex = 6;
            this.checkBoxIsNullable.Text = "Is Nullable";
            this.checkBoxIsNullable.UseVisualStyleBackColor = false;
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Location = new System.Drawing.Point(53, 107);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(104, 21);
            this.comboBoxDataType.TabIndex = 10;
            this.comboBoxDataType.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDataType_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Data\r\nType";
            // 
            // textBoxCharacterMaximumLength
            // 
            this.textBoxCharacterMaximumLength.Location = new System.Drawing.Point(257, 107);
            this.textBoxCharacterMaximumLength.Name = "textBoxCharacterMaximumLength";
            this.textBoxCharacterMaximumLength.Size = new System.Drawing.Size(46, 20);
            this.textBoxCharacterMaximumLength.TabIndex = 12;
            this.textBoxCharacterMaximumLength.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterMaximumLength_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(163, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Char Max Length";
            // 
            // textBoxOrdinalPosition
            // 
            this.textBoxOrdinalPosition.Location = new System.Drawing.Point(257, 82);
            this.textBoxOrdinalPosition.Name = "textBoxOrdinalPosition";
            this.textBoxOrdinalPosition.ReadOnly = true;
            this.textBoxOrdinalPosition.Size = new System.Drawing.Size(46, 20);
            this.textBoxOrdinalPosition.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(163, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ordinal Position";
            // 
            // textBoxDefault
            // 
            this.textBoxDefault.Location = new System.Drawing.Point(53, 134);
            this.textBoxDefault.Name = "textBoxDefault";
            this.textBoxDefault.ReadOnly = true;
            this.textBoxDefault.Size = new System.Drawing.Size(104, 20);
            this.textBoxDefault.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(12, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Default";
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 159);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(346, 35);
            this.ucHeading1.TabIndex = 17;
            // 
            // FormColumn
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(346, 194);
            this.Controls.Add(this.textBoxDefault);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxOrdinalPosition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxCharacterMaximumLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxDataType);
            this.Controls.Add(this.checkBoxIsNullable);
            this.Controls.Add(this.textBoxAliasDisplay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucHeading1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormColumn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Column";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormColumn_FormClosed);
            this.Load += new System.EventHandler(this.FormColumn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox textBoxAliasDisplay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxIsNullable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.TextBox textBoxCharacterMaximumLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOrdinalPosition;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDefault;
        private System.Windows.Forms.Label label7;
        private Slyce.Common.Controls.ucHeading ucHeading1;
    }
}