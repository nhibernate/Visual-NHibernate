namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class DatabaseChanges
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseChanges));
			this.advTreeTables = new DevComponents.AdvTree.AdvTree();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.nodeNewTables = new DevComponents.AdvTree.Node();
			this.node3 = new DevComponents.AdvTree.Node();
			this.node4 = new DevComponents.AdvTree.Node();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.nodeModifiedTables = new DevComponents.AdvTree.Node();
			this.node5 = new DevComponents.AdvTree.Node();
			this.node6 = new DevComponents.AdvTree.Node();
			this.nodeRemovedTables = new DevComponents.AdvTree.Node();
			this.node7 = new DevComponents.AdvTree.Node();
			this.elementStyle5 = new DevComponents.DotNetBar.ElementStyle();
			this.nodeNewViews = new DevComponents.AdvTree.Node();
			this.nodeModifiedViews = new DevComponents.AdvTree.Node();
			this.nodeRemovedViews = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle4 = new DevComponents.DotNetBar.ElementStyle();
			this.node16 = new DevComponents.AdvTree.Node();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.checkBoxCreateEntities = new System.Windows.Forms.CheckBox();
			this.buttonAddSchemas = new DevComponents.DotNetBar.ButtonX();
			this.nodeEmpty = new DevComponents.AdvTree.Node();
			((System.ComponentModel.ISupportInitialize)(this.advTreeTables)).BeginInit();
			this.SuspendLayout();
			// 
			// advTreeTables
			// 
			this.advTreeTables.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.advTreeTables.AllowDrop = true;
			this.advTreeTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.advTreeTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
			// 
			// 
			// 
			this.advTreeTables.BackgroundStyle.Class = "TreeBorderKey";
			this.advTreeTables.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.advTreeTables.ColumnsVisible = false;
			this.advTreeTables.ForeColor = System.Drawing.Color.White;
			this.advTreeTables.ImageList = this.imageList1;
			this.advTreeTables.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.advTreeTables.Location = new System.Drawing.Point(26, 27);
			this.advTreeTables.Name = "advTreeTables";
			this.advTreeTables.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.nodeNewTables,
            this.nodeModifiedTables,
            this.nodeRemovedTables,
            this.nodeNewViews,
            this.nodeModifiedViews,
            this.nodeRemovedViews,
            this.nodeEmpty});
			this.advTreeTables.NodesConnector = this.nodeConnector1;
			this.advTreeTables.NodeStyle = this.elementStyle1;
			this.advTreeTables.PathSeparator = ";";
			this.advTreeTables.Size = new System.Drawing.Size(610, 284);
			this.advTreeTables.Styles.Add(this.elementStyle1);
			this.advTreeTables.Styles.Add(this.elementStyle2);
			this.advTreeTables.Styles.Add(this.elementStyle3);
			this.advTreeTables.Styles.Add(this.elementStyle4);
			this.advTreeTables.Styles.Add(this.elementStyle5);
			this.advTreeTables.TabIndex = 0;
			this.advTreeTables.Text = "Click \'Refresh\' to see changes in the database";
			this.advTreeTables.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.advTreeTables_AfterCheck);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "TableHS.png");
			this.imageList1.Images.SetKeyName(1, "field_insert_b_16.png");
			this.imageList1.Images.SetKeyName(2, "edit_table_structure16.png");
			this.imageList1.Images.SetKeyName(3, "field_drop_16.png");
			// 
			// nodeNewTables
			// 
			this.nodeNewTables.CheckBoxThreeState = true;
			this.nodeNewTables.Expanded = true;
			this.nodeNewTables.FullRowBackground = true;
			this.nodeNewTables.Name = "nodeNewTables";
			this.nodeNewTables.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node3,
            this.node4});
			this.nodeNewTables.Style = this.elementStyle2;
			this.nodeNewTables.Text = "New database tables";
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
			// nodeModifiedTables
			// 
			this.nodeModifiedTables.CheckBoxThreeState = true;
			this.nodeModifiedTables.Expanded = true;
			this.nodeModifiedTables.FullRowBackground = true;
			this.nodeModifiedTables.Name = "nodeModifiedTables";
			this.nodeModifiedTables.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node5});
			this.nodeModifiedTables.Style = this.elementStyle2;
			this.nodeModifiedTables.Text = "Tables with changes";
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
			// nodeRemovedTables
			// 
			this.nodeRemovedTables.CheckBoxThreeState = true;
			this.nodeRemovedTables.Expanded = true;
			this.nodeRemovedTables.FullRowBackground = true;
			this.nodeRemovedTables.Name = "nodeRemovedTables";
			this.nodeRemovedTables.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node7});
			this.nodeRemovedTables.Style = this.elementStyle5;
			this.nodeRemovedTables.Text = "Removed database tables";
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
			// nodeNewViews
			// 
			this.nodeNewViews.CheckBoxThreeState = true;
			this.nodeNewViews.Expanded = true;
			this.nodeNewViews.FullRowBackground = true;
			this.nodeNewViews.Name = "nodeNewViews";
			this.nodeNewViews.Style = this.elementStyle2;
			this.nodeNewViews.Text = "New database views";
			// 
			// nodeModifiedViews
			// 
			this.nodeModifiedViews.CheckBoxThreeState = true;
			this.nodeModifiedViews.Expanded = true;
			this.nodeModifiedViews.FullRowBackground = true;
			this.nodeModifiedViews.Name = "nodeModifiedViews";
			this.nodeModifiedViews.Style = this.elementStyle2;
			this.nodeModifiedViews.Text = "Views with changes";
			// 
			// nodeRemovedViews
			// 
			this.nodeRemovedViews.CheckBoxThreeState = true;
			this.nodeRemovedViews.Expanded = true;
			this.nodeRemovedViews.FullRowBackground = true;
			this.nodeRemovedViews.Name = "nodeRemovedViews";
			this.nodeRemovedViews.Style = this.elementStyle5;
			this.nodeRemovedViews.Text = "Removed database views";
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
			// node16
			// 
			this.node16.CheckBoxVisible = true;
			this.node16.Expanded = true;
			this.node16.Name = "node16";
			this.node16.Text = "Change 1";
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// checkBoxCreateEntities
			// 
			this.checkBoxCreateEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxCreateEntities.AutoSize = true;
			this.checkBoxCreateEntities.Checked = true;
			this.checkBoxCreateEntities.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxCreateEntities.ForeColor = System.Drawing.Color.White;
			this.checkBoxCreateEntities.Location = new System.Drawing.Point(38, 326);
			this.checkBoxCreateEntities.Name = "checkBoxCreateEntities";
			this.checkBoxCreateEntities.Size = new System.Drawing.Size(194, 17);
			this.checkBoxCreateEntities.TabIndex = 8;
			this.checkBoxCreateEntities.Text = "Create entities for new tables/views";
			this.checkBoxCreateEntities.UseVisualStyleBackColor = true;
			this.checkBoxCreateEntities.Visible = false;
			// 
			// buttonAddSchemas
			// 
			this.buttonAddSchemas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonAddSchemas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddSchemas.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonAddSchemas.Location = new System.Drawing.Point(38, 349);
			this.buttonAddSchemas.Name = "buttonAddSchemas";
			this.buttonAddSchemas.Size = new System.Drawing.Size(194, 23);
			this.buttonAddSchemas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonAddSchemas.TabIndex = 0;
			this.buttonAddSchemas.Text = "Add tables from other schemas...";
			this.buttonAddSchemas.Visible = false;
			this.buttonAddSchemas.Click += new System.EventHandler(this.buttonAddSchemas_Click);
			// 
			// nodeEmpty
			// 
			this.nodeEmpty.Expanded = true;
			this.nodeEmpty.Image = ((System.Drawing.Image)(resources.GetObject("nodeEmpty.Image")));
			this.nodeEmpty.Name = "nodeEmpty";
			this.nodeEmpty.Text = "Click \'Refresh\' to see changes in the database";
			// 
			// DatabaseChanges
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.Controls.Add(this.buttonAddSchemas);
			this.Controls.Add(this.advTreeTables);
			this.Controls.Add(this.checkBoxCreateEntities);
			this.Name = "DatabaseChanges";
			this.Size = new System.Drawing.Size(664, 430);
			((System.ComponentModel.ISupportInitialize)(this.advTreeTables)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.AdvTree.AdvTree advTreeTables;
		private DevComponents.AdvTree.Node nodeNewTables;
		private DevComponents.AdvTree.Node nodeNewViews;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.AdvTree.Node node3;
		private DevComponents.AdvTree.Node node4;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
		private DevComponents.AdvTree.Node nodeModifiedTables;
		private DevComponents.AdvTree.Node nodeModifiedViews;
		private DevComponents.AdvTree.Node node5;
		private DevComponents.AdvTree.Node node6;
		private DevComponents.AdvTree.Node nodeRemovedTables;
		private DevComponents.AdvTree.Node nodeRemovedViews;
		private DevComponents.DotNetBar.ElementStyle elementStyle5;
		private DevComponents.DotNetBar.ElementStyle elementStyle3;
		private DevComponents.DotNetBar.ElementStyle elementStyle4;
		private DevComponents.AdvTree.Node node16;
		private DevComponents.AdvTree.Node node7;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private System.Windows.Forms.CheckBox checkBoxCreateEntities;
		private DevComponents.AdvTree.Node nodeEmpty;
		private System.Windows.Forms.ImageList imageList1;
		private DevComponents.DotNetBar.ButtonX buttonAddSchemas;
	}
}
