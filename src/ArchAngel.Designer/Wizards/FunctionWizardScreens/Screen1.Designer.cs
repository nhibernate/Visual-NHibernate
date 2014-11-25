namespace ArchAngel.Designer.Wizards.FunctionWizardScreens
{
    partial class Screen1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.ddlCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.optIsTemplateFunction = new System.Windows.Forms.RadioButton();
            this.optNotTemplateFunction = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlScriptLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelNotTemplateFunction = new System.Windows.Forms.Label();
            this.labelIsTemplateFunction = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Function name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(104, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(257, 20);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(104, 44);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(535, 44);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // ddlCategory
            // 
            this.ddlCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlCategory.FormattingEnabled = true;
            this.ddlCategory.Location = new System.Drawing.Point(32, 150);
            this.ddlCategory.Name = "ddlCategory";
            this.ddlCategory.Size = new System.Drawing.Size(589, 21);
            this.ddlCategory.TabIndex = 6;
            this.ddlCategory.TextChanged += new System.EventHandler(this.ddlCategory_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(637, 34);
            this.label3.TabIndex = 7;
            this.label3.Text = "What category do you want this function to appear under on the navigation pane? (" +
                "type to add a new category)";
            // 
            // optIsTemplateFunction
            // 
            this.optIsTemplateFunction.AutoSize = true;
            this.optIsTemplateFunction.Checked = true;
            this.optIsTemplateFunction.Location = new System.Drawing.Point(3, 3);
            this.optIsTemplateFunction.Name = "optIsTemplateFunction";
            this.optIsTemplateFunction.Size = new System.Drawing.Size(14, 13);
            this.optIsTemplateFunction.TabIndex = 8;
            this.optIsTemplateFunction.TabStop = true;
            this.optIsTemplateFunction.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.optIsTemplateFunction.UseVisualStyleBackColor = true;
            this.optIsTemplateFunction.CheckedChanged += new System.EventHandler(this.optIsTemplateFunction_CheckedChanged);
            // 
            // optNotTemplateFunction
            // 
            this.optNotTemplateFunction.AutoSize = true;
            this.optNotTemplateFunction.Location = new System.Drawing.Point(3, 39);
            this.optNotTemplateFunction.Name = "optNotTemplateFunction";
            this.optNotTemplateFunction.Size = new System.Drawing.Size(14, 13);
            this.optNotTemplateFunction.TabIndex = 9;
            this.optNotTemplateFunction.UseVisualStyleBackColor = true;
            this.optNotTemplateFunction.CheckedChanged += new System.EventHandler(this.optNotTemplateFunction_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(637, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Is this function pure code, or text output with embedded code?";
            // 
            // ddlScriptLanguage
            // 
            this.ddlScriptLanguage.FormattingEnabled = true;
            this.ddlScriptLanguage.Location = new System.Drawing.Point(391, 11);
            this.ddlScriptLanguage.Margin = new System.Windows.Forms.Padding(2);
            this.ddlScriptLanguage.Name = "ddlScriptLanguage";
            this.ddlScriptLanguage.Size = new System.Drawing.Size(107, 21);
            this.ddlScriptLanguage.TabIndex = 11;
            this.ddlScriptLanguage.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.892368F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.10764F));
            this.tableLayoutPanel1.Controls.Add(this.labelNotTemplateFunction, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.optIsTemplateFunction, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.optNotTemplateFunction, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelIsTemplateFunction, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 201);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(607, 72);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // labelNotTemplateFunction
            // 
            this.labelNotTemplateFunction.AutoSize = true;
            this.labelNotTemplateFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNotTemplateFunction.Location = new System.Drawing.Point(32, 36);
            this.labelNotTemplateFunction.Name = "labelNotTemplateFunction";
            this.labelNotTemplateFunction.Size = new System.Drawing.Size(572, 36);
            this.labelNotTemplateFunction.TabIndex = 11;
            this.labelNotTemplateFunction.Text = "Standard function (code only)";
            this.labelNotTemplateFunction.Click += new System.EventHandler(this.labelNotTemplateFunction_Click);
            // 
            // labelIsTemplateFunction
            // 
            this.labelIsTemplateFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelIsTemplateFunction.Location = new System.Drawing.Point(32, 0);
            this.labelIsTemplateFunction.Name = "labelIsTemplateFunction";
            this.labelIsTemplateFunction.Size = new System.Drawing.Size(572, 36);
            this.labelIsTemplateFunction.TabIndex = 10;
            this.labelIsTemplateFunction.Text = "Template function: Function body is text output, and can have executable snippets" +
                " inside delimiters <% %> in the text. Usually used to generate the body of a fil" +
                "e.";
            this.labelIsTemplateFunction.Click += new System.EventHandler(this.labelIsTemplateFunction_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.Location = new System.Drawing.Point(564, 8);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 13;
            this.buttonDelete.Text = "     Delete";
            this.buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // Screen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ddlScriptLanguage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddlCategory);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "Screen1";
            this.Size = new System.Drawing.Size(655, 332);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox ddlCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton optIsTemplateFunction;
        private System.Windows.Forms.RadioButton optNotTemplateFunction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlScriptLanguage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelNotTemplateFunction;
        private System.Windows.Forms.Label labelIsTemplateFunction;
		private System.Windows.Forms.Button buttonDelete;
    }
}
