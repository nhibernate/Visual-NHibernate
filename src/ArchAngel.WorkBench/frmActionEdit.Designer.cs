namespace ArchAngel.Workbench
{
    partial class FormAction
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAction));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.CausesValidation = false;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(963, 395);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(100, 28);
			this.buttonCancel.TabIndex = 19;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(855, 395);
			this.buttonOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(100, 28);
			this.buttonOk.TabIndex = 18;
			this.buttonOk.Text = "&OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1069, 387);
			this.panel1.TabIndex = 21;
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 387);
			this.ucHeading1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(1069, 43);
			this.ucHeading1.TabIndex = 20;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// FormAction
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1069, 430);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.ucHeading1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "FormAction";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Action";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormAction_Paint);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.Panel panel1;
    }
}