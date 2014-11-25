namespace Slyce.IntelliMerge.UI.Editors
{
	partial class ucTextMergeEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTextMergeEditor));
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            this.imageListStatuses = new System.Windows.Forms.ImageList(this.components);
            this.imageListNodes = new System.Windows.Forms.ImageList(this.components);
            this.toolTipForButtons = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.editorOriginal = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.originalButtonPanel = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editorResolved = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.resolvedButtonPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonEnableEditing = new System.Windows.Forms.ToolStripButton();
            this.imageListArrows = new System.Windows.Forms.ImageList(this.components);
            this.panelWarning = new System.Windows.Forms.Panel();
            this.lbWarningText = new System.Windows.Forms.Label();
            this.toolStripButtonPrevConflict = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextConflict = new System.Windows.Forms.ToolStripButton();
            this.btnAccept = new System.Windows.Forms.ToolStripButton();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelWarning.SuspendLayout();
            this.SuspendLayout();
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
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 69);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.editorOriginal);
            this.splitContainer1.Panel1.Controls.Add(this.originalButtonPanel);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.editorResolved);
            this.splitContainer1.Panel2.Controls.Add(this.resolvedButtonPanel);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(833, 643);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // editorOriginal
            // 
            this.editorOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.ReadOnly = true;
            this.editorOriginal.Document = document1;
            this.editorOriginal.LineNumberMarginVisible = true;
            this.editorOriginal.Location = new System.Drawing.Point(0, 25);
            this.editorOriginal.Margin = new System.Windows.Forms.Padding(2);
            this.editorOriginal.Name = "editorOriginal";
            this.editorOriginal.Size = new System.Drawing.Size(354, 614);
            this.editorOriginal.TabIndex = 2;
            this.editorOriginal.UserMarginVisible = true;
            this.editorOriginal.ViewVerticalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.editorOriginal_ViewVerticalScroll);
            this.editorOriginal.Resize += new System.EventHandler(this.editorOriginal_Resize);
            this.editorOriginal.UserMarginPaint += new ActiproSoftware.SyntaxEditor.UserMarginPaintEventHandler(this.editorOriginal_UserMarginPaint);
            // 
            // originalButtonPanel
            // 
            this.originalButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.originalButtonPanel.Location = new System.Drawing.Point(354, 25);
            this.originalButtonPanel.MaximumSize = new System.Drawing.Size(50, 0);
            this.originalButtonPanel.MinimumSize = new System.Drawing.Size(50, 0);
            this.originalButtonPanel.Name = "originalButtonPanel";
            this.originalButtonPanel.Padding = new System.Windows.Forms.Padding(33, 0, 0, 0);
            this.originalButtonPanel.Size = new System.Drawing.Size(50, 0);
            this.originalButtonPanel.TabIndex = 5;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrevConflict,
            this.toolStripSeparator1,
            this.toolStripButtonNextConflict});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(404, 25);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // editorResolved
            // 
            this.editorResolved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorResolved.Document = document2;
            this.editorResolved.LineNumberMarginVisible = true;
            this.editorResolved.Location = new System.Drawing.Point(0, 25);
            this.editorResolved.Margin = new System.Windows.Forms.Padding(2);
            this.editorResolved.Name = "editorResolved";
            this.editorResolved.Size = new System.Drawing.Size(366, 614);
            this.editorResolved.TabIndex = 3;
            this.editorResolved.UserMarginVisible = true;
            this.editorResolved.ViewVerticalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.editorResolved_ViewVerticalScroll);
            this.editorResolved.Resize += new System.EventHandler(this.editorResolved_Resize);
            this.editorResolved.UserMarginPaint += new ActiproSoftware.SyntaxEditor.UserMarginPaintEventHandler(this.editorResolved_UserMarginPaint);
            // 
            // resolvedButtonPanel
            // 
            this.resolvedButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.resolvedButtonPanel.Location = new System.Drawing.Point(366, 25);
            this.resolvedButtonPanel.MaximumSize = new System.Drawing.Size(50, 0);
            this.resolvedButtonPanel.MinimumSize = new System.Drawing.Size(50, 0);
            this.resolvedButtonPanel.Name = "resolvedButtonPanel";
            this.resolvedButtonPanel.Size = new System.Drawing.Size(50, 0);
            this.resolvedButtonPanel.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEnableEditing,
            this.btnAccept});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(416, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonEnableEditing
            // 
            this.toolStripButtonEnableEditing.CheckOnClick = true;
            this.toolStripButtonEnableEditing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEnableEditing.Name = "toolStripButtonEnableEditing";
            this.toolStripButtonEnableEditing.Size = new System.Drawing.Size(89, 22);
            this.toolStripButtonEnableEditing.Text = " Enable Editing";
            this.toolStripButtonEnableEditing.ToolTipText = "Enable/Disable Editing";
            this.toolStripButtonEnableEditing.CheckedChanged += new System.EventHandler(this.toolStripButtonEnableEditing_CheckedChanged);
            // 
            // imageListArrows
            // 
            this.imageListArrows.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListArrows.ImageStream")));
            this.imageListArrows.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListArrows.Images.SetKeyName(0, "green_arrow.png");
            this.imageListArrows.Images.SetKeyName(1, "blue_arrow.png");
            this.imageListArrows.Images.SetKeyName(2, "error_16.png");
            // 
            // panelWarning
            // 
            this.panelWarning.Controls.Add(this.lbWarningText);
            this.panelWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWarning.Location = new System.Drawing.Point(0, 0);
            this.panelWarning.Name = "panelWarning";
            this.panelWarning.Size = new System.Drawing.Size(833, 69);
            this.panelWarning.TabIndex = 0;
            this.panelWarning.Visible = false;
            // 
            // lbWarningText
            // 
            this.lbWarningText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWarningText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWarningText.ForeColor = System.Drawing.Color.Red;
            this.lbWarningText.Location = new System.Drawing.Point(0, 0);
            this.lbWarningText.Name = "lbWarningText";
            this.lbWarningText.Size = new System.Drawing.Size(833, 69);
            this.lbWarningText.TabIndex = 0;
            this.lbWarningText.Text = "Warning Text";
            // 
            // toolStripButtonPrevConflict
            // 
            this.toolStripButtonPrevConflict.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrevConflict.Image")));
            this.toolStripButtonPrevConflict.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevConflict.Name = "toolStripButtonPrevConflict";
            this.toolStripButtonPrevConflict.Size = new System.Drawing.Size(95, 22);
            this.toolStripButtonPrevConflict.Text = "Prev Conflict";
            this.toolStripButtonPrevConflict.ToolTipText = "Goto previous conflict";
            this.toolStripButtonPrevConflict.Click += new System.EventHandler(this.toolStripButtonPrevConflict_Click);
            // 
            // toolStripButtonNextConflict
            // 
            this.toolStripButtonNextConflict.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextConflict.Image")));
            this.toolStripButtonNextConflict.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextConflict.Name = "toolStripButtonNextConflict";
            this.toolStripButtonNextConflict.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonNextConflict.Text = "Next Conflict";
            this.toolStripButtonNextConflict.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButtonNextConflict.ToolTipText = "Goto next conflict";
            this.toolStripButtonNextConflict.Click += new System.EventHandler(this.toolStripButtonNextConflict_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Enabled = false;
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(124, 22);
            this.btnAccept.Text = "Accept Text Below";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // ucTextMergeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelWarning);
            this.Name = "ucTextMergeEditor";
            this.Size = new System.Drawing.Size(833, 712);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelWarning.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imageListNodes;
		private System.Windows.Forms.ImageList imageListStatuses;
		private System.Windows.Forms.ToolTip toolTipForButtons;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnAccept;
		private System.Windows.Forms.ImageList imageListArrows;
		protected System.Windows.Forms.SplitContainer splitContainer1;
		protected ActiproSoftware.SyntaxEditor.SyntaxEditor editorResolved;
		protected ActiproSoftware.SyntaxEditor.SyntaxEditor editorOriginal;
		private System.Windows.Forms.Panel originalButtonPanel;
		private System.Windows.Forms.Panel resolvedButtonPanel;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton toolStripButtonPrevConflict;
		private System.Windows.Forms.ToolStripButton toolStripButtonNextConflict;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonEnableEditing;
		private System.Windows.Forms.Panel panelWarning;
		private System.Windows.Forms.Label lbWarningText;
        private System.Windows.Forms.FontDialog fontDialog1;

	}
}