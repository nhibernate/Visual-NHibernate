namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class FormInheritanceHierarchy
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
			this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonOk = new DevComponents.DotNetBar.ButtonX();
			this.InheritanceHierarchy1 = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.InheritanceHierarchy();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(327, 436);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCancel.TabIndex = 18;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonOk.Location = new System.Drawing.Point(246, 436);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonOk.TabIndex = 17;
			this.buttonOk.Text = "Ok";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// InheritanceHierarchy1
			// 
			this.InheritanceHierarchy1.Location = new System.Drawing.Point(12, 12);
			this.InheritanceHierarchy1.MinimumSize = new System.Drawing.Size(490, 380);
			this.InheritanceHierarchy1.Name = "InheritanceHierarchy1";
			this.InheritanceHierarchy1.Size = new System.Drawing.Size(625, 408);
			this.InheritanceHierarchy1.TabIndex = 19;
			this.InheritanceHierarchy1.Table = null;
			// 
			// FormInheritanceHierarchy
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(649, 471);
			this.Controls.Add(this.InheritanceHierarchy1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Name = "FormInheritanceHierarchy";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Inheritance - Table Per Hierarchy";
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonCancel;
		private DevComponents.DotNetBar.ButtonX buttonOk;
		private InheritanceHierarchy InheritanceHierarchy1;
	}
}