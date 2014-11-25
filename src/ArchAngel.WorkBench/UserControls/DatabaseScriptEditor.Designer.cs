namespace ArchAngel.Workbench.UserControls
{
	partial class DatabaseScriptEditor
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
			ActiproSoftware.SyntaxEditor.Document document3 = new ActiproSoftware.SyntaxEditor.Document();
			ActiproSoftware.SyntaxEditor.Document document4 = new ActiproSoftware.SyntaxEditor.Document();
			this.syntaxEditorScriptHeader = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.syntaxEditorOffscreen = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.backgroundWorkerAddReferences = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// syntaxEditorScriptHeader
			// 
			this.syntaxEditorScriptHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.syntaxEditorScriptHeader.Document = document3;
			this.syntaxEditorScriptHeader.LineNumberMarginVisible = true;
			this.syntaxEditorScriptHeader.Location = new System.Drawing.Point(0, 0);
			this.syntaxEditorScriptHeader.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorScriptHeader.Name = "syntaxEditorScriptHeader";
			this.syntaxEditorScriptHeader.Size = new System.Drawing.Size(319, 125);
			this.syntaxEditorScriptHeader.TabIndex = 19;
			this.syntaxEditorScriptHeader.TriggerActivated += new ActiproSoftware.SyntaxEditor.TriggerEventHandler(this.syntaxEditorScriptHeader_TriggerActivated);
			this.syntaxEditorScriptHeader.TextChanged += new System.EventHandler(this.syntaxEditorScriptHeader_TextChanged);
			this.syntaxEditorScriptHeader.KeyDown += new System.Windows.Forms.KeyEventHandler(this.syntaxEditorScriptHeader_KeyDown);
			// 
			// syntaxEditorOffscreen
			// 
			this.syntaxEditorOffscreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.syntaxEditorOffscreen.CausesValidation = false;
			this.syntaxEditorOffscreen.Document = document4;
			this.syntaxEditorOffscreen.IndicatorMarginVisible = false;
			this.syntaxEditorOffscreen.Location = new System.Drawing.Point(88, 17);
			this.syntaxEditorOffscreen.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorOffscreen.Name = "syntaxEditorOffscreen";
			this.syntaxEditorOffscreen.ScrollBarType = ActiproSoftware.SyntaxEditor.ScrollBarType.None;
			this.syntaxEditorOffscreen.Size = new System.Drawing.Size(170, 51);
			this.syntaxEditorOffscreen.TabIndex = 20;
			this.syntaxEditorOffscreen.Visible = false;
			// 
			// timer1
			// 
			this.timer1.Interval = 600;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// backgroundWorkerAddReferences
			// 
			this.backgroundWorkerAddReferences.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAddReferences_DoWork);
			// 
			// DatabaseScriptEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.syntaxEditorOffscreen);
			this.Controls.Add(this.syntaxEditorScriptHeader);
			this.Name = "DatabaseScriptEditor";
			this.Size = new System.Drawing.Size(319, 125);
			this.ResumeLayout(false);

		}

		#endregion

		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorScriptHeader;
		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorOffscreen;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.BackgroundWorker backgroundWorkerAddReferences;
	}
}
