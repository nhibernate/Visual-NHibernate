namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class FormInheritanceInfo
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
			this.buttonOk = new DevComponents.DotNetBar.ButtonX();
			this.labelEntity1 = new System.Windows.Forms.Label();
			this.labelKey2 = new System.Windows.Forms.Label();
			this.labelKey1 = new System.Windows.Forms.Label();
			this.labelEntity2 = new System.Windows.Forms.Label();
			this.labelTypeOfInheritance = new System.Windows.Forms.Label();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonOk.Location = new System.Drawing.Point(105, 131);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "Ok";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// labelEntity1
			// 
			this.labelEntity1.AutoSize = true;
			this.labelEntity1.ForeColor = System.Drawing.Color.White;
			this.labelEntity1.Location = new System.Drawing.Point(19, 43);
			this.labelEntity1.Name = "labelEntity1";
			this.labelEntity1.Size = new System.Drawing.Size(35, 13);
			this.labelEntity1.TabIndex = 1;
			this.labelEntity1.Text = "label1";
			// 
			// labelKey2
			// 
			this.labelKey2.AutoSize = true;
			this.labelKey2.ForeColor = System.Drawing.Color.White;
			this.labelKey2.Location = new System.Drawing.Point(200, 75);
			this.labelKey2.Name = "labelKey2";
			this.labelKey2.Size = new System.Drawing.Size(35, 13);
			this.labelKey2.TabIndex = 2;
			this.labelKey2.Text = "label1";
			// 
			// labelKey1
			// 
			this.labelKey1.AutoSize = true;
			this.labelKey1.ForeColor = System.Drawing.Color.White;
			this.labelKey1.Location = new System.Drawing.Point(19, 75);
			this.labelKey1.Name = "labelKey1";
			this.labelKey1.Size = new System.Drawing.Size(35, 13);
			this.labelKey1.TabIndex = 3;
			this.labelKey1.Text = "label1";
			// 
			// labelEntity2
			// 
			this.labelEntity2.AutoSize = true;
			this.labelEntity2.ForeColor = System.Drawing.Color.White;
			this.labelEntity2.Location = new System.Drawing.Point(200, 43);
			this.labelEntity2.Name = "labelEntity2";
			this.labelEntity2.Size = new System.Drawing.Size(35, 13);
			this.labelEntity2.TabIndex = 4;
			this.labelEntity2.Text = "label1";
			// 
			// labelTypeOfInheritance
			// 
			this.labelTypeOfInheritance.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelTypeOfInheritance.AutoSize = true;
			this.labelTypeOfInheritance.ForeColor = System.Drawing.Color.White;
			this.labelTypeOfInheritance.Location = new System.Drawing.Point(125, 11);
			this.labelTypeOfInheritance.Name = "labelTypeOfInheritance";
			this.labelTypeOfInheritance.Size = new System.Drawing.Size(35, 13);
			this.labelTypeOfInheritance.TabIndex = 5;
			this.labelTypeOfInheritance.Text = "label1";
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.labelTypeOfInheritance);
			this.panelEx1.Controls.Add(this.labelEntity2);
			this.panelEx1.Controls.Add(this.buttonOk);
			this.panelEx1.Controls.Add(this.labelKey1);
			this.panelEx1.Controls.Add(this.labelEntity1);
			this.panelEx1.Controls.Add(this.labelKey2);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(284, 166);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 6;
			// 
			// FormInheritanceInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 166);
			this.Controls.Add(this.panelEx1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormInheritanceInfo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Inheritance Info";
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonOk;
		private System.Windows.Forms.Label labelEntity1;
		private System.Windows.Forms.Label labelKey2;
		private System.Windows.Forms.Label labelKey1;
		private System.Windows.Forms.Label labelEntity2;
		private System.Windows.Forms.Label labelTypeOfInheritance;
		private DevComponents.DotNetBar.PanelEx panelEx1;
	}
}