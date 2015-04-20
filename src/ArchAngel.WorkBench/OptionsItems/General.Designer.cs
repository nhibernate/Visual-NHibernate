namespace ArchAngel.Workbench.OptionsItems
{
    partial class General
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(General));
            this.textBoxTemplatePath = new System.Windows.Forms.TextBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.buttonTemplatePath = new System.Windows.Forms.Button();
            this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
            this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTemplatePath
            // 
            this.textBoxTemplatePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplatePath.Location = new System.Drawing.Point(16, 16);
            this.textBoxTemplatePath.Name = "textBoxTemplatePath";
            this.textBoxTemplatePath.Size = new System.Drawing.Size(328, 20);
            this.textBoxTemplatePath.TabIndex = 4;
            this.superValidator1.SetValidator1(this.textBoxTemplatePath, this.customValidator1);
            this.textBoxTemplatePath.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxTemplateFileName_Validating);
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplate.Location = new System.Drawing.Point(3, 0);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(76, 13);
            this.lblTemplate.TabIndex = 3;
            this.lblTemplate.Text = "Template Path";
            // 
            // buttonTemplatePath
            // 
            this.buttonTemplatePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTemplatePath.Image = ((System.Drawing.Image)(resources.GetObject("buttonTemplatePath.Image")));
            this.buttonTemplatePath.Location = new System.Drawing.Point(350, 14);
            this.buttonTemplatePath.Name = "buttonTemplatePath";
            this.buttonTemplatePath.Size = new System.Drawing.Size(25, 23);
            this.buttonTemplatePath.TabIndex = 5;
            this.buttonTemplatePath.UseVisualStyleBackColor = true;
            this.buttonTemplatePath.Click += new System.EventHandler(this.buttonTemplatePath_Click);
            // 
            // superValidator1
            // 
            this.superValidator1.ContainerControl = this;
            this.superValidator1.ErrorProvider = this.errorProvider1;
            this.superValidator1.Highlighter = this.highlighter1;
            this.superValidator1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // customValidator1
            // 
            this.customValidator1.ErrorMessage = "Your error message here.";
            this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            // 
            // highlighter1
            // 
            this.highlighter1.ContainerControl = this;
            this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.textBoxTemplatePath);
            this.Controls.Add(this.lblTemplate);
            this.Controls.Add(this.buttonTemplatePath);
            this.Name = "General";
            this.Size = new System.Drawing.Size(378, 245);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		internal System.Windows.Forms.TextBox textBoxTemplatePath;
		private System.Windows.Forms.Label lblTemplate;
		private System.Windows.Forms.Button buttonTemplatePath;
		private DevComponents.DotNetBar.Validator.SuperValidator superValidator1;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
		private DevComponents.DotNetBar.Validator.CustomValidator customValidator1;
    }
}
