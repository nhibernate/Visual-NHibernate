namespace Slyce.IntelliMerge.UI.Editors
{
    partial class ucSimpleDiffEditor
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSimpleDiffEditor));
            this.editorOriginal = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.editorNew = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.scrollBar1 = new ActiproSoftware.WinUICore.ScrollBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelHeading = new DevComponents.DotNetBar.LabelX();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSingleEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDoubleEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPrevChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextChange = new System.Windows.Forms.ToolStripButton();
            this.labelFilePath = new DevComponents.DotNetBar.LabelX();
            this.panelHeader = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.scrollBar1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorOriginal
            // 
            this.editorOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.ReadOnly = true;
            this.editorOriginal.Document = document1;
            this.editorOriginal.IndicatorMarginVisible = false;
            this.editorOriginal.LineNumberMarginVisible = true;
            this.editorOriginal.Location = new System.Drawing.Point(0, 23);
            this.editorOriginal.Margin = new System.Windows.Forms.Padding(2);
            this.editorOriginal.Name = "editorOriginal";
            this.editorOriginal.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.ForcedHorizontal;
            this.editorOriginal.Size = new System.Drawing.Size(327, 384);
            this.editorOriginal.TabIndex = 3;
            this.editorOriginal.ViewVerticalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.editorOriginal_ViewVerticalScroll);
            this.editorOriginal.ViewHorizontalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.editorOriginal_ViewHorizontalScroll);
            // 
            // editorNew
            // 
            this.editorNew.Dock = System.Windows.Forms.DockStyle.Fill;
            document2.ReadOnly = true;
            this.editorNew.Document = document2;
            this.editorNew.IndicatorMarginVisible = false;
            this.editorNew.LineNumberMarginVisible = true;
            this.editorNew.Location = new System.Drawing.Point(0, 23);
            this.editorNew.Margin = new System.Windows.Forms.Padding(2);
            this.editorNew.Name = "editorNew";
            this.editorNew.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.ForcedHorizontal;
            this.editorNew.Size = new System.Drawing.Size(315, 384);
            this.editorNew.TabIndex = 3;
            this.editorNew.ViewVerticalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.editorNew_ViewVerticalScroll);
            this.editorNew.ViewHorizontalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.editorNew_ViewHorizontalScroll);
            // 
            // scrollBar1
            // 
            this.scrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.scrollBar1.Location = new System.Drawing.Point(646, 0);
            this.scrollBar1.Name = "scrollBar1";
            this.scrollBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.scrollBar1.Size = new System.Drawing.Size(17, 407);
            this.scrollBar1.TabIndex = 4;
            this.scrollBar1.Text = "scrollBar1";
            this.scrollBar1.ValueChanged += new System.EventHandler(this.scrollBar1_ValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.editorOriginal);
            this.splitContainer1.Panel1.Controls.Add(this.labelX1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.editorNew);
            this.splitContainer1.Panel2.Controls.Add(this.labelX2);
            this.splitContainer1.Size = new System.Drawing.Size(646, 407);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground;
            this.labelX1.BackgroundStyle.BackColorGradientAngle = 90;
            this.labelX1.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(0, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(327, 23);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "Existing file";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground;
            this.labelX2.BackgroundStyle.BackColorGradientAngle = 90;
            this.labelX2.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.White;
            this.labelX2.Location = new System.Drawing.Point(0, 0);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(315, 23);
            this.labelX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "New file";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.scrollBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 407);
            this.panel1.TabIndex = 6;
            // 
            // labelHeading
            // 
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelHeading.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground;
            this.labelHeading.BackgroundStyle.BackColorGradientAngle = 90;
            this.labelHeading.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.labelHeading.BackgroundStyle.Class = "";
            this.labelHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(0, 0);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(663, 23);
            this.labelHeading.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelHeading.TabIndex = 9;
            this.labelHeading.Text = "labelHeading";
            this.labelHeading.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSingleEditor,
            this.toolStripButtonDoubleEditor,
            this.toolStripSeparator1,
            this.toolStripButtonPrevChange,
            this.toolStripButtonNextChange});
            this.toolStrip1.Location = new System.Drawing.Point(0, 23);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(663, 31);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSingleEditor
            // 
            this.toolStripButtonSingleEditor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripButtonSingleEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSingleEditor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSingleEditor.Image")));
            this.toolStripButtonSingleEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSingleEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSingleEditor.Name = "toolStripButtonSingleEditor";
            this.toolStripButtonSingleEditor.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonSingleEditor.Text = "toolStripButton1";
            this.toolStripButtonSingleEditor.ToolTipText = "View text in a single editor";
            this.toolStripButtonSingleEditor.Click += new System.EventHandler(this.toolStripButtonSingleEditor_Click);
            // 
            // toolStripButtonDoubleEditor
            // 
            this.toolStripButtonDoubleEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDoubleEditor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDoubleEditor.Image")));
            this.toolStripButtonDoubleEditor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDoubleEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDoubleEditor.Name = "toolStripButtonDoubleEditor";
            this.toolStripButtonDoubleEditor.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonDoubleEditor.Text = "toolStripButton1";
            this.toolStripButtonDoubleEditor.ToolTipText = "View text in two side-by-side editors";
            this.toolStripButtonDoubleEditor.Click += new System.EventHandler(this.toolStripButtonDoubleEditor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonPrevChange
            // 
            this.toolStripButtonPrevChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevChange.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrevChange.Image")));
            this.toolStripButtonPrevChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevChange.Name = "toolStripButtonPrevChange";
            this.toolStripButtonPrevChange.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPrevChange.Text = "Move to previous change";
            this.toolStripButtonPrevChange.Click += new System.EventHandler(this.toolStripButtonPrevChange_Click);
            // 
            // toolStripButtonNextChange
            // 
            this.toolStripButtonNextChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextChange.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextChange.Image")));
            this.toolStripButtonNextChange.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonNextChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextChange.Name = "toolStripButtonNextChange";
            this.toolStripButtonNextChange.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonNextChange.Text = "toolStripButton2";
            this.toolStripButtonNextChange.ToolTipText = "Move to next change";
            this.toolStripButtonNextChange.Click += new System.EventHandler(this.toolStripButtonNextChange_Click);
            // 
            // labelFilePath
            // 
            this.labelFilePath.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelFilePath.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground;
            this.labelFilePath.BackgroundStyle.BackColorGradientAngle = 90;
            this.labelFilePath.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.labelFilePath.BackgroundStyle.Class = "";
            this.labelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilePath.ForeColor = System.Drawing.Color.White;
            this.labelFilePath.Location = new System.Drawing.Point(0, 0);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(104, 23);
            this.labelFilePath.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.labelFilePath.TabIndex = 11;
            this.labelFilePath.Text = "labelFilePath";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.labelFilePath);
            this.panelHeader.Controls.Add(this.labelHeading);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(663, 23);
            this.panelHeader.TabIndex = 12;
            // 
            // ucSimpleDiffEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelHeader);
            this.Name = "ucSimpleDiffEditor";
            this.Size = new System.Drawing.Size(663, 461);
            this.Resize += new System.EventHandler(this.ucSimpleDiffEditor_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.scrollBar1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected ActiproSoftware.SyntaxEditor.SyntaxEditor editorOriginal;
        protected ActiproSoftware.SyntaxEditor.SyntaxEditor editorNew;
        private ActiproSoftware.WinUICore.ScrollBar scrollBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
		  private System.Windows.Forms.Panel panel1;
		  private DevComponents.DotNetBar.LabelX labelHeading;
		  private DevComponents.DotNetBar.LabelX labelX1;
		  private DevComponents.DotNetBar.LabelX labelX2;
		  private System.Windows.Forms.ToolStrip toolStrip1;
		  private System.Windows.Forms.ToolStripButton toolStripButtonSingleEditor;
		  private System.Windows.Forms.ToolStripButton toolStripButtonDoubleEditor;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		  private System.Windows.Forms.ToolStripButton toolStripButtonPrevChange;
		  private System.Windows.Forms.ToolStripButton toolStripButtonNextChange;
		  private DevComponents.DotNetBar.LabelX labelFilePath;
		  private System.Windows.Forms.Panel panelHeader;
    }
}
