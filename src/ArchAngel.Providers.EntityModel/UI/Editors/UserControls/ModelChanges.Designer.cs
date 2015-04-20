namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class ModelChanges
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelChanges));
			ActiproSoftware.SyntaxEditor.Document document3 = new ActiproSoftware.SyntaxEditor.Document();
			this.advTreeTables = new DevComponents.AdvTree.AdvTree();
			this.nodeNewEntities = new DevComponents.AdvTree.Node();
			this.node3 = new DevComponents.AdvTree.Node();
			this.node4 = new DevComponents.AdvTree.Node();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.nodeModifiedEntities = new DevComponents.AdvTree.Node();
			this.node5 = new DevComponents.AdvTree.Node();
			this.node6 = new DevComponents.AdvTree.Node();
			this.nodeRemovedEntities = new DevComponents.AdvTree.Node();
			this.node7 = new DevComponents.AdvTree.Node();
			this.elementStyle5 = new DevComponents.DotNetBar.ElementStyle();
			this.nodeEmpty = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle4 = new DevComponents.DotNetBar.ElementStyle();
			this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.syntaxEditorScript = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.panel3 = new System.Windows.Forms.Panel();
			this.comboBoxDatabaseTypes = new System.Windows.Forms.ComboBox();
			this.buttonRunScript = new DevComponents.DotNetBar.ButtonX();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.buttonCopy = new DevComponents.DotNetBar.ButtonX();
			this.buttonCreateChangeScript = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.advTreeTables)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// advTreeTables
			// 
			this.advTreeTables.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.advTreeTables.AllowDrop = true;
			this.advTreeTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
			// 
			// 
			// 
			this.advTreeTables.BackgroundStyle.Class = "TreeBorderKey";
			this.advTreeTables.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.advTreeTables.ColumnsVisible = false;
			this.advTreeTables.Dock = System.Windows.Forms.DockStyle.Left;
			this.advTreeTables.ForeColor = System.Drawing.Color.White;
			this.advTreeTables.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.advTreeTables.Location = new System.Drawing.Point(0, 0);
			this.advTreeTables.Name = "advTreeTables";
			this.advTreeTables.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.nodeNewEntities,
            this.nodeModifiedEntities,
            this.nodeRemovedEntities,
            this.nodeEmpty});
			this.advTreeTables.NodesConnector = this.nodeConnector1;
			this.advTreeTables.NodeStyle = this.elementStyle1;
			this.advTreeTables.PathSeparator = ";";
			this.advTreeTables.Size = new System.Drawing.Size(233, 373);
			this.advTreeTables.Styles.Add(this.elementStyle1);
			this.advTreeTables.Styles.Add(this.elementStyle2);
			this.advTreeTables.Styles.Add(this.elementStyle3);
			this.advTreeTables.Styles.Add(this.elementStyle4);
			this.advTreeTables.Styles.Add(this.elementStyle5);
			this.advTreeTables.TabIndex = 1;
			this.advTreeTables.Text = "Click \'Refresh\' to see changes in the database";
			// 
			// nodeNewEntities
			// 
			this.nodeNewEntities.CheckBoxThreeState = true;
			this.nodeNewEntities.CheckBoxVisible = true;
			this.nodeNewEntities.Expanded = true;
			this.nodeNewEntities.FullRowBackground = true;
			this.nodeNewEntities.Name = "nodeNewEntities";
			this.nodeNewEntities.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node3,
            this.node4});
			this.nodeNewEntities.Style = this.elementStyle2;
			this.nodeNewEntities.Text = "Add these tables to database";
			// 
			// node3
			// 
			this.node3.CheckBoxVisible = true;
			this.node3.Name = "node3";
			this.node3.Text = "Table1";
			// 
			// node4
			// 
			this.node4.CheckBoxVisible = true;
			this.node4.Expanded = true;
			this.node4.Name = "node4";
			this.node4.Text = "Table2";
			// 
			// elementStyle2
			// 
			this.elementStyle2.BackColor = System.Drawing.Color.GreenYellow;
			this.elementStyle2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
			this.elementStyle2.BorderBottomWidth = 1;
			this.elementStyle2.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle2.BorderLeftWidth = 1;
			this.elementStyle2.BorderRightWidth = 1;
			this.elementStyle2.BorderTopWidth = 1;
			this.elementStyle2.Class = "";
			this.elementStyle2.CornerDiameter = 4;
			this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle2.Description = "Blue";
			this.elementStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.PaddingBottom = 1;
			this.elementStyle2.PaddingLeft = 1;
			this.elementStyle2.PaddingRight = 1;
			this.elementStyle2.PaddingTop = 1;
			this.elementStyle2.TextColor = System.Drawing.Color.Black;
			// 
			// nodeModifiedEntities
			// 
			this.nodeModifiedEntities.CheckBoxThreeState = true;
			this.nodeModifiedEntities.CheckBoxVisible = true;
			this.nodeModifiedEntities.Expanded = true;
			this.nodeModifiedEntities.FullRowBackground = true;
			this.nodeModifiedEntities.Name = "nodeModifiedEntities";
			this.nodeModifiedEntities.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node5});
			this.nodeModifiedEntities.Style = this.elementStyle2;
			this.nodeModifiedEntities.Text = "Tables with changes";
			// 
			// node5
			// 
			this.node5.CheckBoxVisible = true;
			this.node5.Expanded = true;
			this.node5.Name = "node5";
			this.node5.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node6});
			this.node5.Text = "Table3";
			// 
			// node6
			// 
			this.node6.CheckBoxVisible = true;
			this.node6.Expanded = true;
			this.node6.Name = "node6";
			this.node6.Text = "Change 1";
			// 
			// nodeRemovedEntities
			// 
			this.nodeRemovedEntities.CheckBoxThreeState = true;
			this.nodeRemovedEntities.CheckBoxVisible = true;
			this.nodeRemovedEntities.Expanded = true;
			this.nodeRemovedEntities.FullRowBackground = true;
			this.nodeRemovedEntities.Name = "nodeRemovedEntities";
			this.nodeRemovedEntities.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node7});
			this.nodeRemovedEntities.Style = this.elementStyle5;
			this.nodeRemovedEntities.Text = "Remove these tables from database";
			// 
			// node7
			// 
			this.node7.CheckBoxVisible = true;
			this.node7.Expanded = true;
			this.node7.Name = "node7";
			this.node7.Style = this.elementStyle5;
			this.node7.Text = "Table3";
			// 
			// elementStyle5
			// 
			this.elementStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(224)))), ((int)(((byte)(123)))));
			this.elementStyle5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
			this.elementStyle5.BorderBottomWidth = 1;
			this.elementStyle5.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle5.BorderLeftWidth = 1;
			this.elementStyle5.BorderRightWidth = 1;
			this.elementStyle5.BorderTopWidth = 1;
			this.elementStyle5.Class = "";
			this.elementStyle5.CornerDiameter = 4;
			this.elementStyle5.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle5.Description = "Orange";
			this.elementStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.elementStyle5.Name = "elementStyle5";
			this.elementStyle5.PaddingBottom = 1;
			this.elementStyle5.PaddingLeft = 1;
			this.elementStyle5.PaddingRight = 1;
			this.elementStyle5.PaddingTop = 1;
			this.elementStyle5.TextColor = System.Drawing.Color.Black;
			// 
			// nodeEmpty
			// 
			this.nodeEmpty.Expanded = true;
			this.nodeEmpty.Image = ((System.Drawing.Image)(resources.GetObject("nodeEmpty.Image")));
			this.nodeEmpty.Name = "nodeEmpty";
			this.nodeEmpty.Text = "Click \'Refresh\' to see changes in the model";
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
			this.nodeConnector1.LineWidth = 0;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Class = "";
			this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.Color.White;
			// 
			// elementStyle3
			// 
			this.elementStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(215)))));
			this.elementStyle3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(111)))));
			this.elementStyle3.BackColorGradientAngle = 90;
			this.elementStyle3.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderBottomWidth = 1;
			this.elementStyle3.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle3.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderLeftWidth = 1;
			this.elementStyle3.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderRightWidth = 1;
			this.elementStyle3.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderTopWidth = 1;
			this.elementStyle3.Class = "";
			this.elementStyle3.CornerDiameter = 4;
			this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle3.Description = "Lemon";
			this.elementStyle3.Name = "elementStyle3";
			this.elementStyle3.PaddingBottom = 1;
			this.elementStyle3.PaddingLeft = 1;
			this.elementStyle3.PaddingRight = 1;
			this.elementStyle3.PaddingTop = 1;
			this.elementStyle3.TextColor = System.Drawing.Color.Black;
			// 
			// elementStyle4
			// 
			this.elementStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(201)))));
			this.elementStyle4.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(210)))), ((int)(((byte)(132)))));
			this.elementStyle4.BackColorGradientAngle = 90;
			this.elementStyle4.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderBottomWidth = 1;
			this.elementStyle4.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle4.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderLeftWidth = 1;
			this.elementStyle4.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderRightWidth = 1;
			this.elementStyle4.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle4.BorderTopWidth = 1;
			this.elementStyle4.Class = "";
			this.elementStyle4.CornerDiameter = 4;
			this.elementStyle4.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle4.Description = "OrangeLight";
			this.elementStyle4.Name = "elementStyle4";
			this.elementStyle4.PaddingBottom = 1;
			this.elementStyle4.PaddingLeft = 1;
			this.elementStyle4.PaddingRight = 1;
			this.elementStyle4.PaddingTop = 1;
			this.elementStyle4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(83)))), ((int)(((byte)(2)))));
			// 
			// expandableSplitter1
			// 
			this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(166)))), ((int)(((byte)(72)))));
			this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
			this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.Location = new System.Drawing.Point(233, 0);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new System.Drawing.Size(6, 373);
			this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter1.TabIndex = 2;
			this.expandableSplitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.syntaxEditorScript);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(239, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(431, 373);
			this.panel1.TabIndex = 3;
			// 
			// syntaxEditorScript
			// 
			this.syntaxEditorScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.syntaxEditorScript.Document = document3;
			this.syntaxEditorScript.LineNumberMarginVisible = true;
			this.syntaxEditorScript.Location = new System.Drawing.Point(0, 84);
			this.syntaxEditorScript.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorScript.Name = "syntaxEditorScript";
			this.syntaxEditorScript.Size = new System.Drawing.Size(431, 289);
			this.syntaxEditorScript.TabIndex = 20;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.buttonCopy);
			this.panel3.Controls.Add(this.buttonX1);
			this.panel3.Controls.Add(this.buttonCreateChangeScript);
			this.panel3.Controls.Add(this.comboBoxDatabaseTypes);
			this.panel3.Controls.Add(this.buttonRunScript);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(431, 84);
			this.panel3.TabIndex = 5;
			// 
			// comboBoxDatabaseTypes
			// 
			this.comboBoxDatabaseTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDatabaseTypes.FormattingEnabled = true;
			this.comboBoxDatabaseTypes.Location = new System.Drawing.Point(9, 10);
			this.comboBoxDatabaseTypes.Margin = new System.Windows.Forms.Padding(4);
			this.comboBoxDatabaseTypes.Name = "comboBoxDatabaseTypes";
			this.comboBoxDatabaseTypes.Size = new System.Drawing.Size(173, 21);
			this.comboBoxDatabaseTypes.TabIndex = 56;
			// 
			// buttonRunScript
			// 
			this.buttonRunScript.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRunScript.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRunScript.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRunScript.HoverImage")));
			this.buttonRunScript.Image = ((System.Drawing.Image)(resources.GetObject("buttonRunScript.Image")));
			this.buttonRunScript.Location = new System.Drawing.Point(189, 10);
			this.buttonRunScript.Name = "buttonRunScript";
			this.buttonRunScript.Size = new System.Drawing.Size(139, 30);
			this.buttonRunScript.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonRunScript.TabIndex = 1;
			this.buttonRunScript.Text = "  Full creation script";
			this.buttonRunScript.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonRunScript.Click += new System.EventHandler(this.buttonRunScript_Click);
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonX1.HoverImage")));
			this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
			this.buttonX1.Location = new System.Drawing.Point(319, 46);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(100, 30);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX1.TabIndex = 2;
			this.buttonX1.Text = "  Save";
			this.buttonX1.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
			// 
			// buttonCopy
			// 
			this.buttonCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCopy.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonCopy.HoverImage")));
			this.buttonCopy.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopy.Image")));
			this.buttonCopy.Location = new System.Drawing.Point(319, 10);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(100, 30);
			this.buttonCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCopy.TabIndex = 0;
			this.buttonCopy.Text = "  Copy";
			this.buttonCopy.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// buttonCreateChangeScript
			// 
			this.buttonCreateChangeScript.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCreateChangeScript.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCreateChangeScript.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonCreateChangeScript.HoverImage")));
			this.buttonCreateChangeScript.Image = ((System.Drawing.Image)(resources.GetObject("buttonCreateChangeScript.Image")));
			this.buttonCreateChangeScript.Location = new System.Drawing.Point(189, 46);
			this.buttonCreateChangeScript.Name = "buttonCreateChangeScript";
			this.buttonCreateChangeScript.Size = new System.Drawing.Size(139, 30);
			this.buttonCreateChangeScript.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCreateChangeScript.TabIndex = 57;
			this.buttonCreateChangeScript.Text = "  Changes only";
			this.buttonCreateChangeScript.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonCreateChangeScript.Click += new System.EventHandler(this.buttonCreateChangeScript_Click);
			// 
			// ModelChanges
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.expandableSplitter1);
			this.Controls.Add(this.advTreeTables);
			this.Name = "ModelChanges";
			this.Size = new System.Drawing.Size(670, 373);
			((System.ComponentModel.ISupportInitialize)(this.advTreeTables)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.AdvTree.AdvTree advTreeTables;
		private DevComponents.AdvTree.Node nodeNewEntities;
		private DevComponents.AdvTree.Node node3;
		private DevComponents.AdvTree.Node node4;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
		private DevComponents.AdvTree.Node nodeModifiedEntities;
		private DevComponents.AdvTree.Node node5;
		private DevComponents.AdvTree.Node node6;
		private DevComponents.AdvTree.Node nodeRemovedEntities;
		private DevComponents.AdvTree.Node node7;
		private DevComponents.DotNetBar.ElementStyle elementStyle5;
		private DevComponents.AdvTree.Node nodeEmpty;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.DotNetBar.ElementStyle elementStyle3;
		private DevComponents.DotNetBar.ElementStyle elementStyle4;
		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX buttonX1;
		private DevComponents.DotNetBar.ButtonX buttonRunScript;
		private DevComponents.DotNetBar.ButtonX buttonCopy;
		internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorScript;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ComboBox comboBoxDatabaseTypes;
		private DevComponents.DotNetBar.ButtonX buttonCreateChangeScript;
	}
}
