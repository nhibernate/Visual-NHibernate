namespace ArchAngel.Workbench
{
	partial class ucFindResults
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.listResults = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.colFunction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listResults
			// 
			// 
			// 
			// 
			this.listResults.Border.Class = "ListViewBorder";
			this.listResults.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.listResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFunction,
            this.columnHeader1,
            this.columnHeader2,
            this.colText});
			this.listResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listResults.FullRowSelect = true;
			this.listResults.GridLines = true;
			this.listResults.Location = new System.Drawing.Point(0, 0);
			this.listResults.Name = "listResults";
			this.listResults.Size = new System.Drawing.Size(790, 380);
			this.listResults.TabIndex = 0;
			this.listResults.UseCompatibleStateImageBehavior = false;
			this.listResults.View = System.Windows.Forms.View.Details;
			this.listResults.SelectedIndexChanged += new System.EventHandler(this.listResults_SelectedIndexChanged);
			this.listResults.Resize += new System.EventHandler(this.listResults_Resize);
			// 
			// colFunction
			// 
			this.colFunction.Text = "File";
			// 
			// colText
			// 
			this.colText.Text = "Text";
			this.colText.Width = 300;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.Controls.Add(this.listResults);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(790, 380);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 1;
			this.panelEx1.Text = "panelEx1";
			// 
			// columnHeader1
			// 
			this.columnHeader1.DisplayIndex = 2;
			// 
			// columnHeader2
			// 
			this.columnHeader2.DisplayIndex = 3;
			// 
			// ucFindResults
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelEx1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "ucFindResults";
			this.Size = new System.Drawing.Size(790, 380);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucFindResults_Paint);
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private DevComponents.DotNetBar.Controls.ListViewEx listResults;
		private System.Windows.Forms.ColumnHeader colFunction;
		private System.Windows.Forms.ColumnHeader colText;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}
