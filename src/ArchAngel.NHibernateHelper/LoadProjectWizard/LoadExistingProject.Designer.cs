namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	partial class LoadExistingProject
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadExistingProject));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
			this.btnNewBlankProject = new DevComponents.DotNetBar.ButtonX();
			this.buttonExistingDatabase = new DevComponents.DotNetBar.ButtonX();
			this.buttonVisualStudioProject = new DevComponents.DotNetBar.ButtonX();
			this.btnBack = new DevComponents.DotNetBar.ButtonX();
			this.buttonCreateProject = new DevComponents.DotNetBar.ButtonX();
			this.rbLoadFromDatabase = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.rbStartNewProject = new System.Windows.Forms.RadioButton();
			this.rbUseExistingProject = new System.Windows.Forms.RadioButton();
			this.labelBlank = new System.Windows.Forms.Label();
			this.panelBlank = new DevComponents.DotNetBar.PanelEx();
			this.labelExistingProject = new System.Windows.Forms.Label();
			this.labelDatabase = new System.Windows.Forms.Label();
			this.panelDatabase = new DevComponents.DotNetBar.PanelEx();
			this.panelExistingProject = new DevComponents.DotNetBar.PanelEx();
			this.panelBlank.SuspendLayout();
			this.panelDatabase.SuspendLayout();
			this.panelExistingProject.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "VSProject_CSCodefile.bmp");
			// 
			// highlighter1
			// 
			this.highlighter1.ContainerControl = this;
			this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// btnNewBlankProject
			// 
			this.btnNewBlankProject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNewBlankProject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnNewBlankProject.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNewBlankProject.Image = ((System.Drawing.Image)(resources.GetObject("btnNewBlankProject.Image")));
			this.btnNewBlankProject.Location = new System.Drawing.Point(27, 27);
			this.btnNewBlankProject.Name = "btnNewBlankProject";
			this.btnNewBlankProject.Size = new System.Drawing.Size(179, 31);
			this.btnNewBlankProject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnNewBlankProject.TabIndex = 40;
			this.btnNewBlankProject.Text = "New Blank Project >";
			this.btnNewBlankProject.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.btnNewBlankProject.Click += new System.EventHandler(this.btnNewBlankProject_Click);
			this.btnNewBlankProject.MouseEnter += new System.EventHandler(this.btnNewBlankProject_MouseEnter);
			// 
			// buttonExistingDatabase
			// 
			this.buttonExistingDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonExistingDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonExistingDatabase.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonExistingDatabase.Image = ((System.Drawing.Image)(resources.GetObject("buttonExistingDatabase.Image")));
			this.buttonExistingDatabase.Location = new System.Drawing.Point(9, 12);
			this.buttonExistingDatabase.Name = "buttonExistingDatabase";
			this.buttonExistingDatabase.Size = new System.Drawing.Size(179, 31);
			this.buttonExistingDatabase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonExistingDatabase.TabIndex = 41;
			this.buttonExistingDatabase.Text = "From existing database >";
			this.buttonExistingDatabase.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonExistingDatabase.Click += new System.EventHandler(this.buttonExistingDatabase_Click);
			this.buttonExistingDatabase.MouseEnter += new System.EventHandler(this.buttonExistingDatabase_MouseEnter);
			// 
			// buttonVisualStudioProject
			// 
			this.buttonVisualStudioProject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonVisualStudioProject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonVisualStudioProject.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonVisualStudioProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonVisualStudioProject.Image")));
			this.buttonVisualStudioProject.Location = new System.Drawing.Point(9, 12);
			this.buttonVisualStudioProject.Name = "buttonVisualStudioProject";
			this.buttonVisualStudioProject.Size = new System.Drawing.Size(179, 31);
			this.buttonVisualStudioProject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonVisualStudioProject.TabIndex = 42;
			this.buttonVisualStudioProject.Text = "Import Visual Studio project >";
			this.buttonVisualStudioProject.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonVisualStudioProject.Click += new System.EventHandler(this.buttonVisualStudioProject_Click);
			this.buttonVisualStudioProject.MouseEnter += new System.EventHandler(this.buttonVisualStudioProject_MouseEnter);
			// 
			// btnBack
			// 
			this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnBack.Location = new System.Drawing.Point(26, 293);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(71, 23);
			this.btnBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnBack.TabIndex = 44;
			this.btnBack.Text = "< Back";
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// buttonCreateProject
			// 
			this.buttonCreateProject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCreateProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCreateProject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCreateProject.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonCreateProject.Location = new System.Drawing.Point(410, 293);
			this.buttonCreateProject.Name = "buttonCreateProject";
			this.buttonCreateProject.Size = new System.Drawing.Size(103, 23);
			this.buttonCreateProject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCreateProject.TabIndex = 45;
			this.buttonCreateProject.Text = "Create project >";
			this.buttonCreateProject.Visible = false;
			this.buttonCreateProject.Click += new System.EventHandler(this.buttonCreateProject_Click);
			// 
			// rbLoadFromDatabase
			// 
			this.rbLoadFromDatabase.AutoSize = true;
			this.rbLoadFromDatabase.Location = new System.Drawing.Point(188, 310);
			this.rbLoadFromDatabase.Name = "rbLoadFromDatabase";
			this.rbLoadFromDatabase.Size = new System.Drawing.Size(163, 17);
			this.rbLoadFromDatabase.TabIndex = 36;
			this.rbLoadFromDatabase.TabStop = true;
			this.rbLoadFromDatabase.Text = "Load From Existing Database";
			this.rbLoadFromDatabase.UseVisualStyleBackColor = true;
			this.rbLoadFromDatabase.Visible = false;
			this.rbLoadFromDatabase.Click += new System.EventHandler(this.rbLoadFromDatabase_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(115, 304);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Project Location:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Visible = false;
			// 
			// rbStartNewProject
			// 
			this.rbStartNewProject.AutoSize = true;
			this.rbStartNewProject.Location = new System.Drawing.Point(188, 287);
			this.rbStartNewProject.Name = "rbStartNewProject";
			this.rbStartNewProject.Size = new System.Drawing.Size(138, 17);
			this.rbStartNewProject.TabIndex = 36;
			this.rbStartNewProject.TabStop = true;
			this.rbStartNewProject.Text = "Start With Blank Project";
			this.rbStartNewProject.UseVisualStyleBackColor = true;
			this.rbStartNewProject.Visible = false;
			this.rbStartNewProject.CheckedChanged += new System.EventHandler(this.rbStartNewProject_CheckedChanged);
			// 
			// rbUseExistingProject
			// 
			this.rbUseExistingProject.AutoSize = true;
			this.rbUseExistingProject.Location = new System.Drawing.Point(129, 264);
			this.rbUseExistingProject.Name = "rbUseExistingProject";
			this.rbUseExistingProject.Size = new System.Drawing.Size(272, 17);
			this.rbUseExistingProject.TabIndex = 36;
			this.rbUseExistingProject.TabStop = true;
			this.rbUseExistingProject.Text = "Load from existing Visual Studio (NHibernate) project";
			this.rbUseExistingProject.UseVisualStyleBackColor = true;
			this.rbUseExistingProject.Visible = false;
			this.rbUseExistingProject.CheckedChanged += new System.EventHandler(this.rbUseExistingProject_CheckedChanged);
			// 
			// labelBlank
			// 
			this.labelBlank.Location = new System.Drawing.Point(211, 12);
			this.labelBlank.Name = "labelBlank";
			this.labelBlank.Size = new System.Drawing.Size(229, 23);
			this.labelBlank.TabIndex = 46;
			this.labelBlank.Text = "Create a new empty project.";
			this.labelBlank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelBlank.Visible = false;
			// 
			// panelBlank
			// 
			this.panelBlank.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelBlank.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelBlank.Controls.Add(this.labelBlank);
			this.panelBlank.Location = new System.Drawing.Point(18, 15);
			this.panelBlank.Name = "panelBlank";
			this.panelBlank.Size = new System.Drawing.Size(480, 56);
			this.panelBlank.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelBlank.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
			this.panelBlank.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelBlank.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelBlank.Style.CornerDiameter = 5;
			this.panelBlank.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelBlank.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelBlank.Style.GradientAngle = 90;
			this.panelBlank.TabIndex = 48;
			this.panelBlank.MouseEnter += new System.EventHandler(this.panelBlank_MouseEnter);
			this.panelBlank.MouseLeave += new System.EventHandler(this.panelBlank_MouseLeave);
			// 
			// labelExistingProject
			// 
			this.labelExistingProject.Location = new System.Drawing.Point(210, 6);
			this.labelExistingProject.Name = "labelExistingProject";
			this.labelExistingProject.Size = new System.Drawing.Size(261, 40);
			this.labelExistingProject.TabIndex = 49;
			this.labelExistingProject.Text = "Visualize and edit an existing NHibernate project.\r\n    Fluent or HBM XML";
			this.labelExistingProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelExistingProject.Visible = false;
			// 
			// labelDatabase
			// 
			this.labelDatabase.Location = new System.Drawing.Point(211, 12);
			this.labelDatabase.Name = "labelDatabase";
			this.labelDatabase.Size = new System.Drawing.Size(260, 35);
			this.labelDatabase.TabIndex = 50;
			this.labelDatabase.Text = "Create a new NHibernate project from an existing database.";
			this.labelDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelDatabase.Visible = false;
			// 
			// panelDatabase
			// 
			this.panelDatabase.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelDatabase.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelDatabase.Controls.Add(this.labelDatabase);
			this.panelDatabase.Controls.Add(this.buttonExistingDatabase);
			this.panelDatabase.Location = new System.Drawing.Point(18, 77);
			this.panelDatabase.Name = "panelDatabase";
			this.panelDatabase.Size = new System.Drawing.Size(480, 56);
			this.panelDatabase.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelDatabase.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
			this.panelDatabase.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelDatabase.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelDatabase.Style.CornerDiameter = 5;
			this.panelDatabase.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelDatabase.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelDatabase.Style.GradientAngle = 90;
			this.panelDatabase.TabIndex = 49;
			this.panelDatabase.MouseEnter += new System.EventHandler(this.panelDatabase_MouseEnter);
			this.panelDatabase.MouseLeave += new System.EventHandler(this.panelDatabase_MouseLeave);
			// 
			// panelExistingProject
			// 
			this.panelExistingProject.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExistingProject.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExistingProject.Controls.Add(this.labelExistingProject);
			this.panelExistingProject.Controls.Add(this.buttonVisualStudioProject);
			this.panelExistingProject.Location = new System.Drawing.Point(18, 140);
			this.panelExistingProject.Name = "panelExistingProject";
			this.panelExistingProject.Size = new System.Drawing.Size(480, 56);
			this.panelExistingProject.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelExistingProject.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
			this.panelExistingProject.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelExistingProject.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelExistingProject.Style.CornerDiameter = 5;
			this.panelExistingProject.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelExistingProject.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelExistingProject.Style.GradientAngle = 90;
			this.panelExistingProject.TabIndex = 49;
			this.panelExistingProject.MouseEnter += new System.EventHandler(this.panelExistingProject_MouseEnter);
			this.panelExistingProject.MouseLeave += new System.EventHandler(this.panelExistingProject_MouseLeave);
			// 
			// LoadExistingProject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.buttonCreateProject);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.btnNewBlankProject);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rbUseExistingProject);
			this.Controls.Add(this.rbLoadFromDatabase);
			this.Controls.Add(this.rbStartNewProject);
			this.Controls.Add(this.panelBlank);
			this.Controls.Add(this.panelDatabase);
			this.Controls.Add(this.panelExistingProject);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "LoadExistingProject";
			this.Size = new System.Drawing.Size(537, 340);
			this.panelBlank.ResumeLayout(false);
			this.panelDatabase.ResumeLayout(false);
			this.panelExistingProject.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.ButtonX btnNewBlankProject;
        private DevComponents.DotNetBar.ButtonX buttonExistingDatabase;
		private DevComponents.DotNetBar.ButtonX buttonVisualStudioProject;
        private DevComponents.DotNetBar.ButtonX btnBack;
        private DevComponents.DotNetBar.ButtonX buttonCreateProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbUseExistingProject;
        private System.Windows.Forms.RadioButton rbLoadFromDatabase;
		private System.Windows.Forms.RadioButton rbStartNewProject;
		private System.Windows.Forms.Label labelBlank;
		private DevComponents.DotNetBar.PanelEx panelBlank;
		private System.Windows.Forms.Label labelDatabase;
		private System.Windows.Forms.Label labelExistingProject;
		private DevComponents.DotNetBar.PanelEx panelDatabase;
		private DevComponents.DotNetBar.PanelEx panelExistingProject;
	}
}