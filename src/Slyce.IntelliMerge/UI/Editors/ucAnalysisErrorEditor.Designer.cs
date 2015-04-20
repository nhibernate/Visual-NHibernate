namespace Slyce.IntelliMerge.UI.Editors
{
	partial class ucAnalysisErrorEditor
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
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAnalysisErrorEditor));
			this.editor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.dockManager1 = new ActiproSoftware.UIStudio.Dock.DockManager(this.components);
			this.errorToolWindow = new ActiproSoftware.UIStudio.Dock.ToolWindow();
			this.errorTreeList = new DevComponents.AdvTree.AdvTree();
			this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
			this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
			this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.autoHideTabStripPanel1 = new ActiproSoftware.UIStudio.Dock.AutoHideTabStripPanel();
			this.autoHideContainer1 = new ActiproSoftware.UIStudio.Dock.AutoHideContainer();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.errorToolWindow.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorTreeList)).BeginInit();
			this.autoHideContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// editor
			// 
			this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editor.Document = document1;
			this.editor.LineNumberMarginVisible = true;
			this.editor.Location = new System.Drawing.Point(0, 25);
			this.editor.Name = "editor";
			this.editor.Size = new System.Drawing.Size(562, 380);
			this.editor.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(562, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(93, 22);
			this.toolStripButton1.Text = "Reparse Text";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(138, 22);
			this.toolStripButton2.Text = "Save And Re-Analyse";
			this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// dockManager1
			// 
			this.dockManager1.HostContainerControl = this;
			// 
			// errorToolWindow
			// 
			this.errorToolWindow.ContextImage = global::Slyce.IntelliMerge.Properties.Resources.error_16;
			this.errorToolWindow.Controls.Add(this.errorTreeList);
			this.errorToolWindow.Cursor = System.Windows.Forms.Cursors.Default;
			this.errorToolWindow.Dock = System.Windows.Forms.DockStyle.Top;
			this.errorToolWindow.DockedSize = new System.Drawing.Size(200, 197);
			this.errorToolWindow.DockManager = this.dockManager1;
			this.errorToolWindow.Location = new System.Drawing.Point(0, 24);
			this.errorToolWindow.Name = "errorToolWindow";
			this.errorToolWindow.Size = new System.Drawing.Size(562, 200);
			this.errorToolWindow.State = ActiproSoftware.UIStudio.Dock.ToolWindowState.AutoHide;
			this.errorToolWindow.TabIndex = 0;
			this.errorToolWindow.Text = "Errors";
			// 
			// errorTreeList
			// 
			this.errorTreeList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.errorTreeList.AllowDrop = true;
			this.errorTreeList.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.errorTreeList.BackgroundStyle.Class = "TreeBorderKey";
			this.errorTreeList.Columns.Add(this.columnHeader1);
			this.errorTreeList.Columns.Add(this.columnHeader2);
			this.errorTreeList.Columns.Add(this.columnHeader3);
			this.errorTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorTreeList.GridLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.errorTreeList.GridRowLines = true;
			this.errorTreeList.HotTracking = true;
			this.errorTreeList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.errorTreeList.Location = new System.Drawing.Point(0, 0);
			this.errorTreeList.Name = "errorTreeList";
			this.errorTreeList.NodesConnector = this.nodeConnector1;
			this.errorTreeList.NodeStyle = this.elementStyle1;
			this.errorTreeList.PathSeparator = ";";
			this.errorTreeList.Size = new System.Drawing.Size(562, 200);
			this.errorTreeList.Styles.Add(this.elementStyle1);
			this.errorTreeList.SuspendPaint = false;
			this.errorTreeList.TabIndex = 4;
			this.errorTreeList.Text = "advTree1";
			this.errorTreeList.DoubleClick += new System.EventHandler(this.errorTreeList_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Name = "columnHeader1";
			this.columnHeader1.Text = "Line";
			this.columnHeader1.Width.Relative = 35;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Name = "columnHeader2";
			this.columnHeader2.Text = "Column";
			this.columnHeader2.Width.Relative = 15;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Name = "columnHeader3";
			this.columnHeader3.Text = "Description";
			this.columnHeader3.Width.Relative = 50;
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// autoHideTabStripPanel1
			// 
			this.autoHideTabStripPanel1.AllowDrop = true;
			this.autoHideTabStripPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.autoHideTabStripPanel1.DockManager = this.dockManager1;
			this.autoHideTabStripPanel1.Location = new System.Drawing.Point(0, 405);
			this.autoHideTabStripPanel1.Name = "autoHideTabStripPanel1";
			this.autoHideTabStripPanel1.Size = new System.Drawing.Size(562, 23);
			this.autoHideTabStripPanel1.TabIndex = 2;
			// 
			// autoHideContainer1
			// 
			this.autoHideContainer1.AutoHideTabStripPanel = this.autoHideTabStripPanel1;
			this.autoHideContainer1.Controls.Add(this.errorToolWindow);
			this.autoHideContainer1.DockManager = this.dockManager1;
			this.autoHideContainer1.Location = new System.Drawing.Point(0, 205);
			this.autoHideContainer1.Name = "autoHideContainer1";
			this.autoHideContainer1.Size = new System.Drawing.Size(562, 200);
			this.autoHideContainer1.TabIndex = 3;
			// 
			// ucAnalysisErrorEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.autoHideContainer1);
			this.Controls.Add(this.editor);
			this.Controls.Add(this.autoHideTabStripPanel1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "ucAnalysisErrorEditor";
			this.Size = new System.Drawing.Size(562, 428);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.errorToolWindow.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorTreeList)).EndInit();
			this.autoHideContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ActiproSoftware.SyntaxEditor.SyntaxEditor editor;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ActiproSoftware.UIStudio.Dock.DockManager dockManager1;
		private ActiproSoftware.UIStudio.Dock.ToolWindow errorToolWindow;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private ActiproSoftware.UIStudio.Dock.AutoHideContainer autoHideContainer1;
		private ActiproSoftware.UIStudio.Dock.AutoHideTabStripPanel autoHideTabStripPanel1;
		private DevComponents.AdvTree.AdvTree errorTreeList;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.AdvTree.ColumnHeader columnHeader1;
		private DevComponents.AdvTree.ColumnHeader columnHeader2;
		private DevComponents.AdvTree.ColumnHeader columnHeader3;
	}
}