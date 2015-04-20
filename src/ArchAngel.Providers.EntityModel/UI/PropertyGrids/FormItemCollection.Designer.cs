namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
    partial class FormItemCollection
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormItemCollection));
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.listBoxItems = new System.Windows.Forms.ListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
			this.buttonNewColumn = new DevComponents.DotNetBar.ButtonItem();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx1.SuspendLayout();
			this.panelEx3.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx1.Controls.Add(this.panelEx3);
			this.panelEx1.Controls.Add(this.ribbonBar1);
			this.panelEx1.Controls.Add(this.panelEx2);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(457, 440);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx3.Controls.Add(this.listBoxItems);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(0, 110);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Padding = new System.Windows.Forms.Padding(10);
			this.panelEx3.Size = new System.Drawing.Size(457, 330);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 3;
			this.panelEx3.Text = "panelEx3";
			// 
			// listBoxItems
			// 
			this.listBoxItems.ContextMenuStrip = this.contextMenuStrip1;
			this.listBoxItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxItems.FormattingEnabled = true;
			this.listBoxItems.Location = new System.Drawing.Point(10, 10);
			this.listBoxItems.Name = "listBoxItems";
			this.listBoxItems.Size = new System.Drawing.Size(437, 303);
			this.listBoxItems.TabIndex = 2;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addColumnToolStripMenuItem,
            this.removeColumnToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// addColumnToolStripMenuItem
			// 
			this.addColumnToolStripMenuItem.Name = "addColumnToolStripMenuItem";
			this.addColumnToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.addColumnToolStripMenuItem.Text = "Add {Item}";
			// 
			// removeColumnToolStripMenuItem
			// 
			this.removeColumnToolStripMenuItem.Name = "removeColumnToolStripMenuItem";
			this.removeColumnToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.removeColumnToolStripMenuItem.Text = "Remove {Item}";
			// 
			// ribbonBar1
			// 
			this.ribbonBar1.AutoOverflowEnabled = true;
			// 
			// 
			// 
			this.ribbonBar1.BackgroundMouseOverStyle.Class = "";
			// 
			// 
			// 
			this.ribbonBar1.BackgroundStyle.Class = "";
			this.ribbonBar1.ContainerControlProcessDialogKey = true;
			this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonNewColumn});
			this.ribbonBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.ribbonBar1.Location = new System.Drawing.Point(0, 30);
			this.ribbonBar1.Name = "ribbonBar1";
			this.ribbonBar1.Size = new System.Drawing.Size(457, 80);
			this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.ribbonBar1.TabIndex = 1;
			this.ribbonBar1.Text = "{Item}s";
			// 
			// 
			// 
			this.ribbonBar1.TitleStyle.Class = "";
			// 
			// 
			// 
			this.ribbonBar1.TitleStyleMouseOver.Class = "";
			// 
			// buttonNewColumn
			// 
			this.buttonNewColumn.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonNewColumn.HoverImage")));
			this.buttonNewColumn.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewColumn.Image")));
			this.buttonNewColumn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonNewColumn.Name = "buttonNewColumn";
			this.buttonNewColumn.SubItemsExpandWidth = 14;
			this.buttonNewColumn.Text = "New {Item}";
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx2.Location = new System.Drawing.Point(0, 0);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(457, 30);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx2.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 0;
			this.panelEx2.Text = "{Item}s";
			// 
			// FormItemCollection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelEx1);
			this.Name = "FormItemCollection";
			this.Size = new System.Drawing.Size(457, 440);
			this.panelEx1.ResumeLayout(false);
			this.panelEx3.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem buttonNewColumn;
		private System.Windows.Forms.ListBox listBoxItems;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addColumnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeColumnToolStripMenuItem;

    }
}
