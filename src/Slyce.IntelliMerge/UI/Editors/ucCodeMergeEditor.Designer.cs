namespace Slyce.IntelliMerge.UI.Editors
{
	partial class ucCodeMergeEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCodeMergeEditor));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeListObjects = new DevComponents.AdvTree.AdvTree();
			this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
			this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
			this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
			this.columnHeader4 = new DevComponents.AdvTree.ColumnHeader();
			this.imageListNodes = new System.Windows.Forms.ImageList(this.components);
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyleUnchanged = new DevComponents.DotNetBar.ElementStyle();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnSelectMatch = new System.Windows.Forms.Button();
			this.panelWarning = new System.Windows.Forms.Panel();
			this.lbWarningText = new System.Windows.Forms.Label();
			this.ucTextMergeEditor = new Slyce.IntelliMerge.UI.Editors.ucTextMergeEditor();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectMatchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListStatuses = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListObjects)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.panelWarning.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			this.splitContainer1.Panel1.Controls.Add(this.panelWarning);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ucTextMergeEditor);
			this.splitContainer1.Size = new System.Drawing.Size(638, 470);
			this.splitContainer1.SplitterDistance = 148;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.treeListObjects);
			this.panel1.Controls.Add(this.flowLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 44);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(638, 104);
			this.panel1.TabIndex = 2;
			// 
			// treeListObjects
			// 
			this.treeListObjects.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeListObjects.AllowDrop = true;
			this.treeListObjects.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.treeListObjects.BackgroundStyle.Class = "TreeBorderKey";
			this.treeListObjects.Columns.Add(this.columnHeader1);
			this.treeListObjects.Columns.Add(this.columnHeader2);
			this.treeListObjects.Columns.Add(this.columnHeader3);
			this.treeListObjects.Columns.Add(this.columnHeader4);
			this.treeListObjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListObjects.GridLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.treeListObjects.GridRowLines = true;
			this.treeListObjects.ImageList = this.imageListNodes;
			this.treeListObjects.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeListObjects.Location = new System.Drawing.Point(0, 30);
			this.treeListObjects.Name = "treeListObjects";
			this.treeListObjects.NodesConnector = this.nodeConnector1;
			this.treeListObjects.NodeStyle = this.elementStyle1;
			this.treeListObjects.PathSeparator = ";";
			this.treeListObjects.SelectionBoxStyle = DevComponents.AdvTree.eSelectionStyle.FullRowSelect;
			this.treeListObjects.Size = new System.Drawing.Size(638, 74);
			this.treeListObjects.Styles.Add(this.elementStyle1);
			this.treeListObjects.Styles.Add(this.elementStyleUnchanged);
			this.treeListObjects.SuspendPaint = false;
			this.treeListObjects.TabIndex = 2;
			this.treeListObjects.Text = "advTree1";
			this.treeListObjects.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.treeListObjects_AfterNodeSelect);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Name = "columnHeader1";
			this.columnHeader1.Width.Relative = 40;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Name = "columnHeader2";
			this.columnHeader2.Text = "User Version";
			this.columnHeader2.Width.Relative = 20;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Name = "columnHeader3";
			this.columnHeader3.Text = "Template Version";
			this.columnHeader3.Width.Relative = 20;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Name = "columnHeader4";
			this.columnHeader4.Text = "Previously Generated";
			this.columnHeader4.Width.Relative = 20;
			// 
			// imageListNodes
			// 
			this.imageListNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListNodes.ImageStream")));
			this.imageListNodes.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListNodes.Images.SetKeyName(0, "");
			this.imageListNodes.Images.SetKeyName(1, "");
			this.imageListNodes.Images.SetKeyName(2, "");
			this.imageListNodes.Images.SetKeyName(3, "");
			this.imageListNodes.Images.SetKeyName(4, "");
			this.imageListNodes.Images.SetKeyName(5, "");
			this.imageListNodes.Images.SetKeyName(6, "");
			this.imageListNodes.Images.SetKeyName(7, "");
			this.imageListNodes.Images.SetKeyName(8, "");
			this.imageListNodes.Images.SetKeyName(9, "");
			this.imageListNodes.Images.SetKeyName(10, "");
			this.imageListNodes.Images.SetKeyName(11, "");
			this.imageListNodes.Images.SetKeyName(12, "");
			this.imageListNodes.Images.SetKeyName(13, "");
			this.imageListNodes.Images.SetKeyName(14, "");
			this.imageListNodes.Images.SetKeyName(15, "VSObject_Event.bmp");
			this.imageListNodes.Images.SetKeyName(16, "VSObject_Constant.bmp");
			this.imageListNodes.Images.SetKeyName(17, "VSObject_Operator.bmp");
			this.imageListNodes.Images.SetKeyName(18, "VSObject_Structure.bmp");
			this.imageListNodes.Images.SetKeyName(19, "VSObject_Interface.bmp");
			this.imageListNodes.Images.SetKeyName(20, "VSObject_Enum.bmp");
			this.imageListNodes.Images.SetKeyName(21, "VSObject_Delegate.bmp");
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle1
			// 
			this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.elementStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			this.elementStyle1.TextTrimming = DevComponents.DotNetBar.eStyleTextTrimming.EllipsisPath;
			// 
			// elementStyleUnchanged
			// 
			this.elementStyleUnchanged.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.elementStyleUnchanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.elementStyleUnchanged.Name = "elementStyleUnchanged";
			this.elementStyleUnchanged.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemDisabledText;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.checkBox1);
			this.flowLayoutPanel1.Controls.Add(this.btnSelectMatch);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(0, 30);
			this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 30);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(638, 30);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.checkBox1.Location = new System.Drawing.Point(3, 3);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(140, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Show Unchanged Items";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// btnSelectMatch
			// 
			this.btnSelectMatch.Enabled = false;
			this.btnSelectMatch.Location = new System.Drawing.Point(149, 3);
			this.btnSelectMatch.Name = "btnSelectMatch";
			this.btnSelectMatch.Size = new System.Drawing.Size(90, 23);
			this.btnSelectMatch.TabIndex = 1;
			this.btnSelectMatch.Text = "Select Match";
			this.btnSelectMatch.UseVisualStyleBackColor = true;
			this.btnSelectMatch.Click += new System.EventHandler(this.selectMatchToolStripMenuItem_Click);
			// 
			// panelWarning
			// 
			this.panelWarning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelWarning.Controls.Add(this.lbWarningText);
			this.panelWarning.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelWarning.ForeColor = System.Drawing.Color.Red;
			this.panelWarning.Location = new System.Drawing.Point(0, 0);
			this.panelWarning.Name = "panelWarning";
			this.panelWarning.Size = new System.Drawing.Size(638, 44);
			this.panelWarning.TabIndex = 2;
			this.panelWarning.Visible = false;
			// 
			// lbWarningText
			// 
			this.lbWarningText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbWarningText.Location = new System.Drawing.Point(0, 0);
			this.lbWarningText.Name = "lbWarningText";
			this.lbWarningText.Size = new System.Drawing.Size(638, 44);
			this.lbWarningText.TabIndex = 2;
			this.lbWarningText.Text = "Warning Text";
			// 
			// ucTextMergeEditor
			// 
			this.ucTextMergeEditor.ConflictColour = System.Drawing.Color.Red;
			this.ucTextMergeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucTextMergeEditor.EditingEnabled = false;
			this.ucTextMergeEditor.FileInformation = null;
			this.ucTextMergeEditor.HasUnsavedChanges = false;
			this.ucTextMergeEditor.Location = new System.Drawing.Point(0, 0);
			this.ucTextMergeEditor.Name = "ucTextMergeEditor";
			this.ucTextMergeEditor.Size = new System.Drawing.Size(638, 318);
			this.ucTextMergeEditor.TabIndex = 0;
			this.ucTextMergeEditor.TemplateChangeColour = System.Drawing.Color.LightGreen;
			this.ucTextMergeEditor.UserAndTemplateChangeColour = System.Drawing.Color.LightCyan;
			this.ucTextMergeEditor.UserChangeColour = System.Drawing.Color.LightBlue;
			this.ucTextMergeEditor.VirtualLineColour = System.Drawing.Color.LightGray;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMatchMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(147, 26);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// selectMatchMenuItem
			// 
			this.selectMatchMenuItem.Name = "selectMatchMenuItem";
			this.selectMatchMenuItem.Size = new System.Drawing.Size(146, 22);
			this.selectMatchMenuItem.Text = "Select Match";
			this.selectMatchMenuItem.Click += new System.EventHandler(this.selectMatchToolStripMenuItem_Click);
			// 
			// imageListStatuses
			// 
			this.imageListStatuses.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatuses.ImageStream")));
			this.imageListStatuses.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListStatuses.Images.SetKeyName(0, "");
			this.imageListStatuses.Images.SetKeyName(1, "");
			this.imageListStatuses.Images.SetKeyName(2, "");
			this.imageListStatuses.Images.SetKeyName(3, "");
			this.imageListStatuses.Images.SetKeyName(4, "");
			this.imageListStatuses.Images.SetKeyName(5, "");
			// 
			// ucCodeMergeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ucCodeMergeEditor";
			this.Size = new System.Drawing.Size(638, 470);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListObjects)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.panelWarning.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private ucTextMergeEditor ucTextMergeEditor;
		private System.Windows.Forms.ImageList imageListNodes;
		private System.Windows.Forms.ImageList imageListStatuses;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem selectMatchMenuItem;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnSelectMatch;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelWarning;
		private System.Windows.Forms.Label lbWarningText;
		private DevComponents.AdvTree.AdvTree treeListObjects;
		private DevComponents.AdvTree.ColumnHeader columnHeader1;
		private DevComponents.AdvTree.ColumnHeader columnHeader2;
		private DevComponents.AdvTree.ColumnHeader columnHeader3;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.AdvTree.ColumnHeader columnHeader4;
		private DevComponents.DotNetBar.ElementStyle elementStyleUnchanged;
	}
}