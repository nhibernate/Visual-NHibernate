namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class FormAvailableInheritance
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAvailableInheritance));
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.buttonTablePerSubClass = new DevComponents.DotNetBar.ButtonX();
			this.panelBlank = new DevComponents.DotNetBar.PanelEx();
			this.labelDescription = new System.Windows.Forms.Label();
			this.buttonTablePerConcreteClass = new DevComponents.DotNetBar.ButtonX();
			this.buttonTablePerHierarchy = new DevComponents.DotNetBar.ButtonX();
			this.linkCreateFromHierarchy = new System.Windows.Forms.LinkLabel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelBlank.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelX1
			// 
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.BackColor = System.Drawing.Color.Black;
			this.labelX1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.labelX1.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(0, 0);
			this.labelX1.Margin = new System.Windows.Forms.Padding(0);
			this.labelX1.Name = "labelX1";
			this.labelX1.PaddingTop = 7;
			this.labelX1.SingleLineColor = System.Drawing.Color.Black;
			this.labelX1.Size = new System.Drawing.Size(388, 60);
			this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.labelX1.TabIndex = 0;
			this.labelX1.Text = "Possible inheritance schemes";
			this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
			this.labelX1.TextLineAlignment = System.Drawing.StringAlignment.Near;
			// 
			// buttonTablePerSubClass
			// 
			this.buttonTablePerSubClass.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonTablePerSubClass.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonTablePerSubClass.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonTablePerSubClass.Image = ((System.Drawing.Image)(resources.GetObject("buttonTablePerSubClass.Image")));
			this.buttonTablePerSubClass.Location = new System.Drawing.Point(14, 29);
			this.buttonTablePerSubClass.Name = "buttonTablePerSubClass";
			this.buttonTablePerSubClass.Size = new System.Drawing.Size(179, 31);
			this.buttonTablePerSubClass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonTablePerSubClass.TabIndex = 50;
			this.buttonTablePerSubClass.Text = "Table per sub-class";
			this.buttonTablePerSubClass.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonTablePerSubClass.MouseEnter += new System.EventHandler(this.buttonTablePerSubClass_MouseEnter);
			this.buttonTablePerSubClass.MouseLeave += new System.EventHandler(this.buttonTablePerSubClass_MouseLeave);
			// 
			// panelBlank
			// 
			this.panelBlank.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelBlank.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelBlank.Controls.Add(this.linkCreateFromHierarchy);
			this.panelBlank.Controls.Add(this.labelDescription);
			this.panelBlank.Location = new System.Drawing.Point(199, 29);
			this.panelBlank.Name = "panelBlank";
			this.panelBlank.Size = new System.Drawing.Size(179, 105);
			this.panelBlank.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelBlank.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
			this.panelBlank.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelBlank.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelBlank.Style.CornerDiameter = 5;
			this.panelBlank.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelBlank.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelBlank.Style.GradientAngle = 90;
			this.panelBlank.TabIndex = 51;
			// 
			// labelDescription
			// 
			this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelDescription.ForeColor = System.Drawing.Color.White;
			this.labelDescription.Location = new System.Drawing.Point(13, 9);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(163, 78);
			this.labelDescription.TabIndex = 0;
			this.labelDescription.Text = "Hover over options to see descriptions.";
			// 
			// buttonTablePerConcreteClass
			// 
			this.buttonTablePerConcreteClass.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonTablePerConcreteClass.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonTablePerConcreteClass.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonTablePerConcreteClass.Image = ((System.Drawing.Image)(resources.GetObject("buttonTablePerConcreteClass.Image")));
			this.buttonTablePerConcreteClass.Location = new System.Drawing.Point(14, 66);
			this.buttonTablePerConcreteClass.Name = "buttonTablePerConcreteClass";
			this.buttonTablePerConcreteClass.Size = new System.Drawing.Size(179, 31);
			this.buttonTablePerConcreteClass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonTablePerConcreteClass.TabIndex = 41;
			this.buttonTablePerConcreteClass.Text = "Table per concrete class";
			this.buttonTablePerConcreteClass.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonTablePerConcreteClass.Click += new System.EventHandler(this.buttonTablePerConcreteClass_Click);
			this.buttonTablePerConcreteClass.MouseEnter += new System.EventHandler(this.buttonTablePerConcreteClass_MouseEnter);
			this.buttonTablePerConcreteClass.MouseLeave += new System.EventHandler(this.buttonTablePerConcreteClass_MouseLeave);
			// 
			// buttonTablePerHierarchy
			// 
			this.buttonTablePerHierarchy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonTablePerHierarchy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonTablePerHierarchy.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonTablePerHierarchy.Image = ((System.Drawing.Image)(resources.GetObject("buttonTablePerHierarchy.Image")));
			this.buttonTablePerHierarchy.Location = new System.Drawing.Point(14, 103);
			this.buttonTablePerHierarchy.Name = "buttonTablePerHierarchy";
			this.buttonTablePerHierarchy.Size = new System.Drawing.Size(179, 31);
			this.buttonTablePerHierarchy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonTablePerHierarchy.TabIndex = 42;
			this.buttonTablePerHierarchy.Text = "Table per hierarchy";
			this.buttonTablePerHierarchy.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonTablePerHierarchy.Click += new System.EventHandler(this.buttonTablePerHierarchy_Click);
			this.buttonTablePerHierarchy.MouseEnter += new System.EventHandler(this.buttonTablePerHierarchy_MouseEnter);
			this.buttonTablePerHierarchy.MouseLeave += new System.EventHandler(this.buttonTablePerHierarchy_MouseLeave);
			// 
			// linkCreateFromHierarchy
			// 
			this.linkCreateFromHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.linkCreateFromHierarchy.LinkColor = System.Drawing.Color.Yellow;
			this.linkCreateFromHierarchy.Location = new System.Drawing.Point(13, 87);
			this.linkCreateFromHierarchy.Name = "linkCreateFromHierarchy";
			this.linkCreateFromHierarchy.Size = new System.Drawing.Size(163, 23);
			this.linkCreateFromHierarchy.TabIndex = 52;
			this.linkCreateFromHierarchy.TabStop = true;
			this.linkCreateFromHierarchy.Text = "Click to create...";
			this.linkCreateFromHierarchy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCreateFromHierarchy_LinkClicked);
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 60);
			this.panelEx1.Margin = new System.Windows.Forms.Padding(0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(388, 88);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.Black;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.BorderWidth = 0;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 52;
			// 
			// FormAvailableInheritance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.buttonTablePerHierarchy);
			this.Controls.Add(this.buttonTablePerConcreteClass);
			this.Controls.Add(this.buttonTablePerSubClass);
			this.Controls.Add(this.panelBlank);
			this.Controls.Add(this.panelEx1);
			this.Controls.Add(this.labelX1);
			this.Name = "FormAvailableInheritance";
			this.Size = new System.Drawing.Size(388, 148);
			this.panelBlank.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.ButtonX buttonTablePerSubClass;
		private DevComponents.DotNetBar.PanelEx panelBlank;
		private DevComponents.DotNetBar.ButtonX buttonTablePerConcreteClass;
		private DevComponents.DotNetBar.ButtonX buttonTablePerHierarchy;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.LinkLabel linkCreateFromHierarchy;
		private DevComponents.DotNetBar.PanelEx panelEx1;
	}
}
