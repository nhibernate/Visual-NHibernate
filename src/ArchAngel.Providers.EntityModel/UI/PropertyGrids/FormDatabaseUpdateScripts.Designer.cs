namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormDatabaseUpdateScripts
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseUpdateScripts));
			ActiproSoftware.SyntaxEditor.Document document3 = new ActiproSoftware.SyntaxEditor.Document();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.buttonMultiSave = new DevComponents.DotNetBar.ButtonItem();
			this.buttonRunScript = new DevComponents.DotNetBar.ButtonX();
			this.buttonCopy = new DevComponents.DotNetBar.ButtonX();
			this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabItem7 = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabItem8 = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabItem9 = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabItem10 = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabItem11 = new DevComponents.DotNetBar.SuperTabItem();
			this.syntaxEditorScript = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.comboBoxDatabases = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboItem1 = new DevComponents.Editors.ComboItem();
			this.comboItem2 = new DevComponents.Editors.ComboItem();
			this.comboItem3 = new DevComponents.Editors.ComboItem();
			this.comboItem4 = new DevComponents.Editors.ComboItem();
			this.comboItem5 = new DevComponents.Editors.ComboItem();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.comboBoxTables = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboItem6 = new DevComponents.Editors.ComboItem();
			this.comboItem7 = new DevComponents.Editors.ComboItem();
			this.comboItem8 = new DevComponents.Editors.ComboItem();
			this.comboItem9 = new DevComponents.Editors.ComboItem();
			this.comboItem10 = new DevComponents.Editors.ComboItem();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.buttonX1);
			this.panel1.Controls.Add(this.buttonRunScript);
			this.panel1.Controls.Add(this.buttonCopy);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 479);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(801, 57);
			this.panel1.TabIndex = 3;
			// 
			// buttonClose
			// 
			this.buttonClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonClose.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonClose.HoverImage")));
			this.buttonClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonClose.Image")));
			this.buttonClose.Location = new System.Drawing.Point(687, 11);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(100, 34);
			this.buttonClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "  Close";
			this.buttonClose.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonX1.HoverImage")));
			this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
			this.buttonX1.Location = new System.Drawing.Point(461, 11);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(100, 34);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonMultiSave});
			this.buttonX1.TabIndex = 2;
			this.buttonX1.Text = "  Save";
			this.buttonX1.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
			// 
			// buttonMultiSave
			// 
			this.buttonMultiSave.GlobalItem = false;
			this.buttonMultiSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonMultiSave.Image")));
			this.buttonMultiSave.Name = "buttonMultiSave";
			this.buttonMultiSave.Text = "Save a file per table...";
			// 
			// buttonRunScript
			// 
			this.buttonRunScript.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRunScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRunScript.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRunScript.Enabled = false;
			this.buttonRunScript.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRunScript.HoverImage")));
			this.buttonRunScript.Image = ((System.Drawing.Image)(resources.GetObject("buttonRunScript.Image")));
			this.buttonRunScript.Location = new System.Drawing.Point(574, 11);
			this.buttonRunScript.Name = "buttonRunScript";
			this.buttonRunScript.Size = new System.Drawing.Size(100, 34);
			this.buttonRunScript.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonRunScript.TabIndex = 1;
			this.buttonRunScript.Text = "  Run script";
			this.buttonRunScript.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonRunScript.Click += new System.EventHandler(this.buttonRunScript_Click);
			// 
			// buttonCopy
			// 
			this.buttonCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCopy.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonCopy.HoverImage")));
			this.buttonCopy.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopy.Image")));
			this.buttonCopy.Location = new System.Drawing.Point(348, 11);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(100, 34);
			this.buttonCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCopy.TabIndex = 0;
			this.buttonCopy.Text = "  Copy";
			this.buttonCopy.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// superTabItem1
			// 
			this.superTabItem1.GlobalItem = false;
			this.superTabItem1.Name = "superTabItem1";
			this.superTabItem1.TabStripItem = null;
			this.superTabItem1.Text = "Database";
			// 
			// superTabItem7
			// 
			this.superTabItem7.GlobalItem = false;
			this.superTabItem7.Name = "superTabItem7";
			this.superTabItem7.TabStripItem = null;
			this.superTabItem7.Text = "Table";
			// 
			// superTabItem8
			// 
			this.superTabItem8.GlobalItem = false;
			this.superTabItem8.Name = "superTabItem8";
			this.superTabItem8.TabStripItem = null;
			this.superTabItem8.Text = "Column";
			// 
			// superTabItem9
			// 
			this.superTabItem9.GlobalItem = false;
			this.superTabItem9.Name = "superTabItem9";
			this.superTabItem9.TabStripItem = null;
			this.superTabItem9.Text = "Key";
			// 
			// superTabItem10
			// 
			this.superTabItem10.GlobalItem = false;
			this.superTabItem10.Name = "superTabItem10";
			this.superTabItem10.TabStripItem = null;
			this.superTabItem10.Text = "Index";
			// 
			// superTabItem11
			// 
			this.superTabItem11.GlobalItem = false;
			this.superTabItem11.Name = "superTabItem11";
			this.superTabItem11.TabStripItem = null;
			this.superTabItem11.Text = "Relationship";
			// 
			// syntaxEditorScript
			// 
			this.syntaxEditorScript.Document = document3;
			this.syntaxEditorScript.LineNumberMarginVisible = true;
			this.syntaxEditorScript.Location = new System.Drawing.Point(0, 81);
			this.syntaxEditorScript.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorScript.Name = "syntaxEditorScript";
			this.syntaxEditorScript.Size = new System.Drawing.Size(801, 394);
			this.syntaxEditorScript.TabIndex = 19;
			// 
			// comboBoxDatabases
			// 
			this.comboBoxDatabases.DisplayMember = "Text";
			this.comboBoxDatabases.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDatabases.FormattingEnabled = true;
			this.comboBoxDatabases.ItemHeight = 14;
			this.comboBoxDatabases.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5});
			this.comboBoxDatabases.Location = new System.Drawing.Point(12, 41);
			this.comboBoxDatabases.Name = "comboBoxDatabases";
			this.comboBoxDatabases.Size = new System.Drawing.Size(121, 20);
			this.comboBoxDatabases.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxDatabases.TabIndex = 20;
			this.comboBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabases_SelectedIndexChanged);
			// 
			// comboItem1
			// 
			this.comboItem1.Text = "SQL Server 2005/2008/Azure";
			// 
			// comboItem2
			// 
			this.comboItem2.Text = "Oracle";
			// 
			// comboItem3
			// 
			this.comboItem3.Text = "MySQL";
			// 
			// comboItem4
			// 
			this.comboItem4.Text = "PostgreSQL";
			// 
			// comboItem5
			// 
			this.comboItem5.Text = "Firebird";
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(12, 20);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(75, 23);
			this.labelX1.TabIndex = 21;
			this.labelX1.Text = "Database";
			// 
			// comboBoxTables
			// 
			this.comboBoxTables.DisplayMember = "Text";
			this.comboBoxTables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTables.FormattingEnabled = true;
			this.comboBoxTables.ItemHeight = 14;
			this.comboBoxTables.Items.AddRange(new object[] {
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10});
			this.comboBoxTables.Location = new System.Drawing.Point(152, 41);
			this.comboBoxTables.Name = "comboBoxTables";
			this.comboBoxTables.Size = new System.Drawing.Size(121, 20);
			this.comboBoxTables.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxTables.TabIndex = 22;
			this.comboBoxTables.SelectedIndexChanged += new System.EventHandler(this.comboBoxTables_SelectedIndexChanged);
			// 
			// comboItem6
			// 
			this.comboItem6.Text = "SQL Server 2005/2008/Azure";
			// 
			// comboItem7
			// 
			this.comboItem7.Text = "Oracle";
			// 
			// comboItem8
			// 
			this.comboItem8.Text = "MySQL";
			// 
			// comboItem9
			// 
			this.comboItem9.Text = "PostgreSQL";
			// 
			// comboItem10
			// 
			this.comboItem10.Text = "Firebird";
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.ForeColor = System.Drawing.Color.White;
			this.labelX2.Location = new System.Drawing.Point(152, 20);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(75, 23);
			this.labelX2.TabIndex = 23;
			this.labelX2.Text = "Table";
			// 
			// FormDatabaseUpdateScripts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(801, 536);
			this.Controls.Add(this.comboBoxTables);
			this.Controls.Add(this.labelX2);
			this.Controls.Add(this.comboBoxDatabases);
			this.Controls.Add(this.syntaxEditorScript);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.labelX1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormDatabaseUpdateScripts";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Update Scripts";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX buttonCopy;
		private DevComponents.DotNetBar.ButtonX buttonRunScript;
		private DevComponents.DotNetBar.SuperTabItem superTabItem1;
		private DevComponents.DotNetBar.SuperTabItem superTabItem7;
		private DevComponents.DotNetBar.SuperTabItem superTabItem8;
		private DevComponents.DotNetBar.SuperTabItem superTabItem9;
		private DevComponents.DotNetBar.SuperTabItem superTabItem10;
		private DevComponents.DotNetBar.SuperTabItem superTabItem11;
		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorScript;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxDatabases;
		private DevComponents.Editors.ComboItem comboItem1;
		private DevComponents.Editors.ComboItem comboItem2;
		private DevComponents.Editors.ComboItem comboItem3;
		private DevComponents.Editors.ComboItem comboItem4;
		private DevComponents.Editors.ComboItem comboItem5;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxTables;
		private DevComponents.Editors.ComboItem comboItem6;
		private DevComponents.Editors.ComboItem comboItem7;
		private DevComponents.Editors.ComboItem comboItem8;
		private DevComponents.Editors.ComboItem comboItem9;
		private DevComponents.Editors.ComboItem comboItem10;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.ButtonX buttonX1;
		private DevComponents.DotNetBar.ButtonX buttonClose;
		private DevComponents.DotNetBar.ButtonItem buttonMultiSave;
	}
}