namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormDatabaseRefreshResults
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
			this.tabControl1 = new DevComponents.DotNetBar.TabControl();
			this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.listBoxAdded = new System.Windows.Forms.CheckedListBox();
			this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.listBoxRemoved = new System.Windows.Forms.CheckedListBox();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonAcceptChanges = new System.Windows.Forms.Button();
			this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
			this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
			this.textBoxTextResults = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
			((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabControlPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			this.groupPanel2.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.tabControlPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.CanReorderTabs = true;
			this.tabControl1.Controls.Add(this.tabControlPanel2);
			this.tabControl1.Controls.Add(this.tabControlPanel1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.tabControl1.SelectedTabIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(607, 591);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
			this.tabControl1.Tabs.Add(this.tabItem2);
			this.tabControl1.Tabs.Add(this.tabItem1);
			this.tabControl1.Text = "tabControl1";
			this.tabControl1.SelectedTabChanging += new DevComponents.DotNetBar.TabStrip.SelectedTabChangingEventHandler(this.tabControl1_SelectedTabChanging);
			// 
			// tabControlPanel2
			// 
			this.tabControlPanel2.Controls.Add(this.splitContainer1);
			this.tabControlPanel2.Controls.Add(this.panelEx1);
			this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
			this.tabControlPanel2.Name = "tabControlPanel2";
			this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
			this.tabControlPanel2.Size = new System.Drawing.Size(607, 565);
			this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
			this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
			this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
			this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
						| DevComponents.DotNetBar.eBorderSide.Bottom)));
			this.tabControlPanel2.Style.GradientAngle = 90;
			this.tabControlPanel2.TabIndex = 2;
			this.tabControlPanel2.TabItem = this.tabItem2;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(1, 1);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupPanel2);
			this.splitContainer1.Size = new System.Drawing.Size(605, 526);
			this.splitContainer1.SplitterDistance = 302;
			this.splitContainer1.TabIndex = 0;
			// 
			// groupPanel1
			// 
			this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.groupPanel1.Controls.Add(this.listBoxAdded);
			this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupPanel1.Location = new System.Drawing.Point(0, 0);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.groupPanel1.Size = new System.Drawing.Size(302, 526);
			// 
			// 
			// 
			this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.groupPanel1.Style.BackColorGradientAngle = 90;
			this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderBottomWidth = 1;
			this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderLeftWidth = 1;
			this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderRightWidth = 1;
			this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderTopWidth = 1;
			this.groupPanel1.Style.Class = "";
			this.groupPanel1.Style.CornerDiameter = 4;
			this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseDown.Class = "";
			this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseOver.Class = "";
			this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupPanel1.TabIndex = 0;
			this.groupPanel1.Text = "Added Tables";
			// 
			// listBoxAdded
			// 
			this.listBoxAdded.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxAdded.FormattingEnabled = true;
			this.listBoxAdded.Location = new System.Drawing.Point(10, 10);
			this.listBoxAdded.MultiColumn = true;
			this.listBoxAdded.Name = "listBoxAdded";
			this.listBoxAdded.Size = new System.Drawing.Size(276, 485);
			this.listBoxAdded.TabIndex = 0;
			// 
			// groupPanel2
			// 
			this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
			this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.groupPanel2.Controls.Add(this.listBoxRemoved);
			this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupPanel2.Location = new System.Drawing.Point(0, 0);
			this.groupPanel2.Name = "groupPanel2";
			this.groupPanel2.Padding = new System.Windows.Forms.Padding(10);
			this.groupPanel2.Size = new System.Drawing.Size(299, 526);
			// 
			// 
			// 
			this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.groupPanel2.Style.BackColorGradientAngle = 90;
			this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel2.Style.BorderBottomWidth = 1;
			this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel2.Style.BorderLeftWidth = 1;
			this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel2.Style.BorderRightWidth = 1;
			this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel2.Style.BorderTopWidth = 1;
			this.groupPanel2.Style.Class = "";
			this.groupPanel2.Style.CornerDiameter = 4;
			this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.groupPanel2.StyleMouseDown.Class = "";
			this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanel2.StyleMouseOver.Class = "";
			this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupPanel2.TabIndex = 0;
			this.groupPanel2.Text = "Removed Tables";
			// 
			// listBoxRemoved
			// 
			this.listBoxRemoved.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxRemoved.FormattingEnabled = true;
			this.listBoxRemoved.Location = new System.Drawing.Point(10, 10);
			this.listBoxRemoved.Name = "listBoxRemoved";
			this.listBoxRemoved.Size = new System.Drawing.Size(273, 485);
			this.listBoxRemoved.TabIndex = 0;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx1.Controls.Add(this.buttonCancel);
			this.panelEx1.Controls.Add(this.buttonAcceptChanges);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelEx1.Location = new System.Drawing.Point(1, 527);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(605, 37);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 1;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(517, 6);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonAcceptChanges
			// 
			this.buttonAcceptChanges.Location = new System.Drawing.Point(407, 6);
			this.buttonAcceptChanges.Name = "buttonAcceptChanges";
			this.buttonAcceptChanges.Size = new System.Drawing.Size(104, 23);
			this.buttonAcceptChanges.TabIndex = 0;
			this.buttonAcceptChanges.Text = "Apply Changes";
			this.buttonAcceptChanges.UseVisualStyleBackColor = true;
			this.buttonAcceptChanges.Click += new System.EventHandler(this.buttonAcceptChanges_Click);
			// 
			// tabItem2
			// 
			this.tabItem2.AttachedControl = this.tabControlPanel2;
			this.tabItem2.Name = "tabItem2";
			this.tabItem2.Text = "Wizard";
			// 
			// tabControlPanel1
			// 
			this.tabControlPanel1.Controls.Add(this.textBoxTextResults);
			this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
			this.tabControlPanel1.Name = "tabControlPanel1";
			this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(15);
			this.tabControlPanel1.Size = new System.Drawing.Size(607, 565);
			this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
			this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
			this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
			this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
						| DevComponents.DotNetBar.eBorderSide.Bottom)));
			this.tabControlPanel1.Style.GradientAngle = 90;
			this.tabControlPanel1.TabIndex = 1;
			this.tabControlPanel1.TabItem = this.tabItem1;
			// 
			// textBoxTextResults
			// 
			// 
			// 
			// 
			this.textBoxTextResults.Border.Class = "TextBoxBorder";
			this.textBoxTextResults.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxTextResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxTextResults.Location = new System.Drawing.Point(15, 15);
			this.textBoxTextResults.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxTextResults.Multiline = true;
			this.textBoxTextResults.Name = "textBoxTextResults";
			this.textBoxTextResults.Size = new System.Drawing.Size(577, 535);
			this.textBoxTextResults.TabIndex = 0;
			this.textBoxTextResults.WordWrap = false;
			// 
			// tabItem1
			// 
			this.tabItem1.AttachedControl = this.tabControlPanel1;
			this.tabItem1.Name = "tabItem1";
			this.tabItem1.Text = "Detailed";
			// 
			// FormDatabaseRefreshResults
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "FormDatabaseRefreshResults";
			this.Size = new System.Drawing.Size(607, 591);
			((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabControlPanel2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel2.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.tabControlPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.TabControl tabControl1;
		private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
		private DevComponents.DotNetBar.TabItem tabItem1;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxTextResults;
		private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
		private DevComponents.DotNetBar.TabItem tabItem2;
		private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private System.Windows.Forms.Button buttonAcceptChanges;
		private System.Windows.Forms.CheckedListBox listBoxAdded;
		private System.Windows.Forms.CheckedListBox listBoxRemoved;
		private System.Windows.Forms.Button buttonCancel;

	}
}
