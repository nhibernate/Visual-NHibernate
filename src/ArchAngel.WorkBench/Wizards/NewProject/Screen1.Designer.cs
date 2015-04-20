namespace ArchAngel.Workbench.Wizards.NewProject
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("filename", 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen1));
			this.listViewRecentFiles = new System.Windows.Forms.ListView();
			this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.txtExistingProject = new System.Windows.Forms.TextBox();
			this.line3 = new Slyce.Common.Controls.Line();
			this.line1 = new Slyce.Common.Controls.Line();
			this.line2 = new Slyce.Common.Controls.Line();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.buttonCheckForUpdates = new DevComponents.DotNetBar.ButtonX();
			this.btnBrowse = new DevComponents.DotNetBar.ButtonX();
			this.btnOpen = new DevComponents.DotNetBar.ButtonX();
			this.btnNew = new DevComponents.DotNetBar.ButtonX();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listViewRecentFiles
			// 
			this.listViewRecentFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewRecentFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFile,
            this.columnHeader2});
			this.listViewRecentFiles.FullRowSelect = true;
			this.listViewRecentFiles.HideSelection = false;
			listViewItem2.ToolTipText = "full path";
			this.listViewRecentFiles.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
			this.listViewRecentFiles.Location = new System.Drawing.Point(40, 117);
			this.listViewRecentFiles.MultiSelect = false;
			this.listViewRecentFiles.Name = "listViewRecentFiles";
			this.listViewRecentFiles.Size = new System.Drawing.Size(272, 207);
			this.listViewRecentFiles.SmallImageList = this.imageList1;
			this.listViewRecentFiles.TabIndex = 1;
			this.listViewRecentFiles.UseCompatibleStateImageBehavior = false;
			this.listViewRecentFiles.View = System.Windows.Forms.View.Details;
			this.listViewRecentFiles.DoubleClick += new System.EventHandler(this.listViewRecentFiles_DoubleClick);
			this.listViewRecentFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewRecentFiles_KeyPress);
			// 
			// colFile
			// 
			this.colFile.Text = "File";
			this.colFile.Width = 115;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Path";
			this.columnHeader2.Width = 290;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "aaproj.ico");
			this.imageList1.Images.SetKeyName(1, "Workbench.ico");
			this.imageList1.Images.SetKeyName(2, "DataContainer_MoveNextHS.png");
			this.imageList1.Images.SetKeyName(3, "openfolderHS.png");
			// 
			// txtExistingProject
			// 
			this.txtExistingProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExistingProject.Location = new System.Drawing.Point(40, 381);
			this.txtExistingProject.Name = "txtExistingProject";
			this.txtExistingProject.Size = new System.Drawing.Size(240, 20);
			this.txtExistingProject.TabIndex = 3;
			// 
			// line3
			// 
			this.line3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.line3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.line3.Caption = "Open an existing project";
			this.line3.CaptionMarginSpace = 0;
			this.line3.CaptionPadding = 5;
			this.line3.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.line3.Location = new System.Drawing.Point(3, 359);
			this.line3.Name = "line3";
			this.line3.Size = new System.Drawing.Size(309, 16);
			this.line3.TabIndex = 11;
			// 
			// line1
			// 
			this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.line1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.line1.Caption = "New project";
			this.line1.CaptionMarginSpace = 0;
			this.line1.CaptionPadding = 5;
			this.line1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.line1.Location = new System.Drawing.Point(3, 26);
			this.line1.Name = "line1";
			this.line1.Size = new System.Drawing.Size(309, 16);
			this.line1.TabIndex = 5;
			// 
			// line2
			// 
			this.line2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.line2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.line2.Caption = "Open a recent project";
			this.line2.CaptionMarginSpace = 0;
			this.line2.CaptionPadding = 5;
			this.line2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.line2.Location = new System.Drawing.Point(3, 95);
			this.line2.Name = "line2";
			this.line2.Size = new System.Drawing.Size(309, 16);
			this.line2.TabIndex = 4;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.buttonCheckForUpdates);
			this.panelEx1.Controls.Add(this.line1);
			this.panelEx1.Controls.Add(this.txtExistingProject);
			this.panelEx1.Controls.Add(this.btnBrowse);
			this.panelEx1.Controls.Add(this.line3);
			this.panelEx1.Controls.Add(this.btnOpen);
			this.panelEx1.Controls.Add(this.btnNew);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(328, 416);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 12;
			this.panelEx1.Text = "panelEx1";
			// 
			// buttonCheckForUpdates
			// 
			this.buttonCheckForUpdates.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCheckForUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCheckForUpdates.AutoSize = true;
			this.buttonCheckForUpdates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonCheckForUpdates.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCheckForUpdates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonCheckForUpdates.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonCheckForUpdates.HoverImage")));
			this.buttonCheckForUpdates.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckForUpdates.Image")));
			this.buttonCheckForUpdates.Location = new System.Drawing.Point(287, 3);
			this.buttonCheckForUpdates.Name = "buttonCheckForUpdates";
			this.buttonCheckForUpdates.Size = new System.Drawing.Size(25, 22);
			this.buttonCheckForUpdates.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.buttonCheckForUpdates, new DevComponents.DotNetBar.SuperTooltipInfo("Updates", "Internet connection required", "Click to check for updates.", ((System.Drawing.Image)(resources.GetObject("buttonCheckForUpdates.SuperTooltip"))), ((System.Drawing.Image)(resources.GetObject("buttonCheckForUpdates.SuperTooltip1"))), DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonCheckForUpdates.TabIndex = 12;
			this.buttonCheckForUpdates.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonCheckForUpdates.Click += new System.EventHandler(this.buttonCheckForUpdates_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.AutoSize = true;
			this.btnBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
			this.btnBrowse.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.btnBrowse.Location = new System.Drawing.Point(288, 381);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(24, 22);
			this.btnBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
			this.btnOpen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
			this.btnOpen.Location = new System.Drawing.Point(237, 330);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(75, 23);
			this.btnOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "Load";
			this.btnOpen.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnNew
			// 
			this.btnNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNew.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnNew.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
			this.btnNew.Location = new System.Drawing.Point(40, 48);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(156, 31);
			this.btnNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "Create a new project...";
			this.btnNew.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// Screen1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.listViewRecentFiles);
			this.Controls.Add(this.line2);
			this.Controls.Add(this.panelEx1);
			this.ForeColor = System.Drawing.Color.White;
			this.MinimumSize = new System.Drawing.Size(315, 377);
			this.Name = "Screen1";
			this.Size = new System.Drawing.Size(328, 416);
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Slyce.Common.Controls.Line line2;
		private Slyce.Common.Controls.Line line1;
		private System.Windows.Forms.ListView listViewRecentFiles;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader colFile;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private Slyce.Common.Controls.Line line3;
		private System.Windows.Forms.TextBox txtExistingProject;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.ButtonX btnNew;
		private DevComponents.DotNetBar.ButtonX btnOpen;
		private DevComponents.DotNetBar.ButtonX btnBrowse;
		private DevComponents.DotNetBar.ButtonX buttonCheckForUpdates;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
	}
}
