namespace ArchAngel.Workbench.UserControls
{
	partial class NamingEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ActiproSoftware.SyntaxEditor.Document document11 = new ActiproSoftware.SyntaxEditor.Document();
			ActiproSoftware.SyntaxEditor.Document document12 = new ActiproSoftware.SyntaxEditor.Document();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.syntaxEditorOffscreen = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.syntaxEditorScriptHeader = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.backgroundWorkerAddReferences = new System.ComponentModel.BackgroundWorker();
			this.labelHeader = new DevComponents.DotNetBar.LabelX();
			this.labelDescription = new DevComponents.DotNetBar.LabelX();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 600;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// syntaxEditorOffscreen
			// 
			this.syntaxEditorOffscreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.syntaxEditorOffscreen.CausesValidation = false;
			this.syntaxEditorOffscreen.Document = document11;
			this.syntaxEditorOffscreen.IndicatorMarginVisible = false;
			this.syntaxEditorOffscreen.Location = new System.Drawing.Point(48, 61);
			this.syntaxEditorOffscreen.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorOffscreen.Name = "syntaxEditorOffscreen";
			this.syntaxEditorOffscreen.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.None;
			this.syntaxEditorOffscreen.Size = new System.Drawing.Size(172, 51);
			this.syntaxEditorOffscreen.TabIndex = 22;
			this.syntaxEditorOffscreen.Visible = false;
			// 
			// syntaxEditorScriptHeader
			// 
			this.syntaxEditorScriptHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.syntaxEditorScriptHeader.Document = document12;
			this.syntaxEditorScriptHeader.LineNumberMarginVisible = true;
			this.syntaxEditorScriptHeader.Location = new System.Drawing.Point(0, 46);
			this.syntaxEditorScriptHeader.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorScriptHeader.Name = "syntaxEditorScriptHeader";
			this.syntaxEditorScriptHeader.Size = new System.Drawing.Size(269, 104);
			this.syntaxEditorScriptHeader.TabIndex = 21;
			this.syntaxEditorScriptHeader.TriggerActivated += new ActiproSoftware.SyntaxEditor.TriggerEventHandler(this.syntaxEditorScriptHeader_TriggerActivated);
			this.syntaxEditorScriptHeader.TextChanged += new System.EventHandler(this.syntaxEditorScriptHeader_TextChanged);
			this.syntaxEditorScriptHeader.KeyDown += new System.Windows.Forms.KeyEventHandler(this.syntaxEditorScriptHeader_KeyDown);
			// 
			// backgroundWorkerAddReferences
			// 
			this.backgroundWorkerAddReferences.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAddReferences_DoWork);
			// 
			// labelHeader
			// 
			this.labelHeader.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelHeader.BackgroundStyle.BackColor = System.Drawing.Color.YellowGreen;
			this.labelHeader.BackgroundStyle.BackColor2 = System.Drawing.Color.Green;
			this.labelHeader.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelHeader.BackgroundStyle.Class = "";
			this.labelHeader.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelHeader.ForeColor = System.Drawing.Color.White;
			this.labelHeader.Location = new System.Drawing.Point(0, 0);
			this.labelHeader.Name = "labelHeader";
			this.labelHeader.Size = new System.Drawing.Size(269, 23);
			this.labelHeader.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.labelHeader.TabIndex = 23;
			this.labelHeader.Text = "labelX1";
			this.labelHeader.TextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// labelDescription
			// 
			// 
			// 
			// 
			this.labelDescription.BackgroundStyle.Class = "";
			this.labelDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelDescription.ForeColor = System.Drawing.Color.White;
			this.labelDescription.Location = new System.Drawing.Point(0, 23);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(269, 23);
			this.labelDescription.TabIndex = 24;
			this.labelDescription.Text = "labelX1";
			// 
			// NamingEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.syntaxEditorOffscreen);
			this.Controls.Add(this.syntaxEditorScriptHeader);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.labelHeader);
			this.Name = "NamingEditor";
			this.Size = new System.Drawing.Size(269, 150);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timer1;
		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorOffscreen;
		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorScriptHeader;
		private System.ComponentModel.BackgroundWorker backgroundWorkerAddReferences;
		private DevComponents.DotNetBar.LabelX labelHeader;
		private DevComponents.DotNetBar.LabelX labelDescription;
	}
}
