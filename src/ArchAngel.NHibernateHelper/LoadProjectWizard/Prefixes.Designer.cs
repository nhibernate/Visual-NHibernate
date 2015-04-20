namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
    partial class Prefixes
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prefixes));
			this.buttonFinish = new DevComponents.DotNetBar.ButtonX();
			this.buttonBack = new DevComponents.DotNetBar.ButtonX();
			this.label1 = new System.Windows.Forms.Label();
			this.formPrefixes1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.FormPrefixes();
			this.SuspendLayout();
			// 
			// buttonFinish
			// 
			this.buttonFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonFinish.Location = new System.Drawing.Point(413, 436);
			this.buttonFinish.Name = "buttonFinish";
			this.buttonFinish.Size = new System.Drawing.Size(76, 23);
			this.buttonFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonFinish.TabIndex = 50;
			this.buttonFinish.Text = "Finish >";
			this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
			// 
			// buttonBack
			// 
			this.buttonBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBack.Location = new System.Drawing.Point(19, 436);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(71, 23);
			this.buttonBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBack.TabIndex = 49;
			this.buttonBack.Text = "< Back";
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(37, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(316, 17);
			this.label1.TabIndex = 51;
			this.label1.Text = "Prefixes & suffixes to remove when naming entities";
			// 
			// formPrefixes1
			// 
			this.formPrefixes1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.formPrefixes1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.formPrefixes1.Location = new System.Drawing.Point(19, 93);
			this.formPrefixes1.Name = "formPrefixes1";
			this.formPrefixes1.Size = new System.Drawing.Size(470, 327);
			this.formPrefixes1.TabIndex = 0;
			// 
			// Prefixes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonFinish);
			this.Controls.Add(this.buttonBack);
			this.Controls.Add(this.formPrefixes1);
			this.Name = "Prefixes";
			this.Size = new System.Drawing.Size(511, 474);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ArchAngel.Providers.EntityModel.UI.PropertyGrids.FormPrefixes formPrefixes1;
        private DevComponents.DotNetBar.ButtonX buttonFinish;
        private DevComponents.DotNetBar.ButtonX buttonBack;
        private System.Windows.Forms.Label label1;
    }
}
