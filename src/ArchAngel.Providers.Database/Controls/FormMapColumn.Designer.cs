namespace ArchAngel.Providers.Database.Controls
{
    partial class FormMapColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMapColumn));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxRelationship = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMapColumn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxScriptObject = new System.Windows.Forms.TextBox();
            this.textBoxAliasDisplay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.chkNullable = new System.Windows.Forms.CheckBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(269, 252);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(188, 252);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(13, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Relationship";
            // 
            // comboBoxRelationship
            // 
            this.comboBoxRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRelationship.FormattingEnabled = true;
            this.comboBoxRelationship.Location = new System.Drawing.Point(84, 21);
            this.comboBoxRelationship.Name = "comboBoxRelationship";
            this.comboBoxRelationship.Size = new System.Drawing.Size(243, 21);
            this.comboBoxRelationship.TabIndex = 1;
            this.comboBoxRelationship.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxRelationship_Validating);
            this.comboBoxRelationship.SelectedIndexChanged += new System.EventHandler(this.comboBoxScriptObject_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Map Column";
            // 
            // comboBoxMapColumn
            // 
            this.comboBoxMapColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMapColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMapColumn.FormattingEnabled = true;
            this.comboBoxMapColumn.Location = new System.Drawing.Point(84, 74);
            this.comboBoxMapColumn.Name = "comboBoxMapColumn";
            this.comboBoxMapColumn.Size = new System.Drawing.Size(243, 21);
            this.comboBoxMapColumn.TabIndex = 5;
            this.comboBoxMapColumn.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxMapColumn_Validating);
            this.comboBoxMapColumn.SelectedIndexChanged += new System.EventHandler(this.comboBoxMapColumn_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Table";
            // 
            // textBoxScriptObject
            // 
            this.textBoxScriptObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScriptObject.Location = new System.Drawing.Point(84, 48);
            this.textBoxScriptObject.Name = "textBoxScriptObject";
            this.textBoxScriptObject.ReadOnly = true;
            this.textBoxScriptObject.Size = new System.Drawing.Size(243, 20);
            this.textBoxScriptObject.TabIndex = 3;
            // 
            // textBoxAliasDisplay
            // 
            this.textBoxAliasDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAliasDisplay.Location = new System.Drawing.Point(84, 153);
            this.textBoxAliasDisplay.Name = "textBoxAliasDisplay";
            this.textBoxAliasDisplay.Size = new System.Drawing.Size(243, 20);
            this.textBoxAliasDisplay.TabIndex = 11;
            this.textBoxAliasDisplay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAliasDisplay_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(14, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Alias Display";
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlias.Location = new System.Drawing.Point(84, 127);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(243, 20);
            this.textBoxAlias.TabIndex = 9;
            this.textBoxAlias.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAlias_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(14, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Alias";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(84, 101);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(243, 20);
            this.textBoxName.TabIndex = 7;
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(14, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Name";
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 246);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(349, 34);
            this.ucHeading1.TabIndex = 14;
            this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkNullable
            // 
            this.chkNullable.AutoSize = true;
            this.chkNullable.Location = new System.Drawing.Point(84, 210);
            this.chkNullable.Name = "chkNullable";
            this.chkNullable.Size = new System.Drawing.Size(64, 17);
            this.chkNullable.TabIndex = 15;
            this.chkNullable.Text = "Nullable";
            this.chkNullable.UseVisualStyleBackColor = true;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(84, 179);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(243, 20);
            this.textBoxDescription.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(14, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Description";
            // 
            // FormMapColumn
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(349, 280);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkNullable);
            this.Controls.Add(this.textBoxAliasDisplay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxScriptObject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMapColumn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxRelationship);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.ucHeading1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMapColumn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Column";
            this.Load += new System.EventHandler(this.FormMapColumn_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMapColumn_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMapColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxRelationship;
        private System.Windows.Forms.TextBox textBoxScriptObject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAliasDisplay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label6;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.CheckBox chkNullable;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label7;
    }
}