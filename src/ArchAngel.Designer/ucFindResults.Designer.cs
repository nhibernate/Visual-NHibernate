namespace ArchAngel.Designer
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
			this.colFunction = new System.Windows.Forms.ColumnHeader();
			this.colText = new System.Windows.Forms.ColumnHeader();
			this.colStart = new System.Windows.Forms.ColumnHeader();
			this.colLength = new System.Windows.Forms.ColumnHeader();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listResults
			// 
			// 
			// 
			// 
			this.listResults.Border.Class = "ListViewBorder";
			this.listResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFunction,
            this.colText,
            this.colStart,
            this.colLength});
			this.listResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listResults.FullRowSelect = true;
			this.listResults.GridLines = true;
			this.listResults.Location = new System.Drawing.Point(0, 0);
			this.listResults.Name = "listResults";
			this.listResults.Size = new System.Drawing.Size(790, 380);
			this.listResults.TabIndex = 0;
			this.listResults.UseCompatibleStateImageBehavior = false;
			this.listResults.View = System.Windows.Forms.View.Details;
			this.listResults.Resize += new System.EventHandler(this.listResults_Resize);
			this.listResults.DoubleClick += new System.EventHandler(this.listResults_DoubleClick);
			// 
			// colFunction
			// 
			this.colFunction.Text = "Function";
			// 
			// colText
			// 
			this.colText.Text = "Text";
			// 
			// colStart
			// 
			this.colStart.Text = "Start";
			// 
			// colLength
			// 
			this.colLength.Text = "Length";
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
		private System.Windows.Forms.ColumnHeader colStart;
		private System.Windows.Forms.ColumnHeader colLength;
		private DevComponents.DotNetBar.PanelEx panelEx1;
	}
}
