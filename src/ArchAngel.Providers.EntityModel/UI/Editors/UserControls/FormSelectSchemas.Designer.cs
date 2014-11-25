namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class FormSelectSchemas
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectSchemas));
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOk = new DevComponents.DotNetBar.ButtonX();
			this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
			this.advTreeTables = new DevComponents.AdvTree.AdvTree();
			this.nodeNewTables = new DevComponents.AdvTree.Node();
			this.node3 = new DevComponents.AdvTree.Node();
			this.node4 = new DevComponents.AdvTree.Node();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.node6 = new DevComponents.AdvTree.Node();
			this.elementStyle5 = new DevComponents.DotNetBar.ElementStyle();
			this.nodeNewViews = new DevComponents.AdvTree.Node();
			this.nodeEmpty = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle4 = new DevComponents.DotNetBar.ElementStyle();
			((System.ComponentModel.ISupportInitialize)(this.advTreeTables)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(274, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select extra schemas you\'d like to fetch new tables from:";
			// 
			// buttonOk
			// 
			this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonOk.Location = new System.Drawing.Point(408, 335);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "OK";
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(489, 335);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
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
			this.advTreeTables.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.advTreeTables.Location = new System.Drawing.Point(12, 25);
			this.advTreeTables.Name = "advTreeTables";
			this.advTreeTables.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.nodeNewTables,
            this.nodeNewViews,
            this.nodeEmpty});
			this.advTreeTables.NodesConnector = this.nodeConnector1;
			this.advTreeTables.NodeStyle = this.elementStyle1;
			this.advTreeTables.PathSeparator = ";";
			this.advTreeTables.Size = new System.Drawing.Size(552, 294);
			this.advTreeTables.Styles.Add(this.elementStyle1);
			this.advTreeTables.Styles.Add(this.elementStyle2);
			this.advTreeTables.Styles.Add(this.elementStyle3);
			this.advTreeTables.Styles.Add(this.elementStyle4);
			this.advTreeTables.Styles.Add(this.elementStyle5);
			this.advTreeTables.TabIndex = 4;
			this.advTreeTables.Text = "Click \'Refresh\' to see changes in the database";
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
			// node6
			// 
			this.node6.CheckBoxVisible = true;
			this.node6.Expanded = true;
			this.node6.Name = "node6";
			this.node6.Text = "Change 1";
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
			// nodeEmpty
			// 
			this.nodeEmpty.Expanded = true;
			this.nodeEmpty.Image = ((System.Drawing.Image)(resources.GetObject("nodeEmpty.Image")));
			this.nodeEmpty.Name = "nodeEmpty";
			this.nodeEmpty.Text = "Click \'Refresh\' to see changes in the database";
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
			// FormSelectSchemas
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(576, 370);
			this.Controls.Add(this.advTreeTables);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.label1);
			this.Name = "FormSelectSchemas";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Other Schemas";
			((System.ComponentModel.ISupportInitialize)(this.advTreeTables)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private DevComponents.DotNetBar.ButtonX buttonOk;
		private DevComponents.DotNetBar.ButtonX buttonCancel;
		private DevComponents.AdvTree.AdvTree advTreeTables;
		private DevComponents.AdvTree.Node nodeNewTables;
		private DevComponents.AdvTree.Node node3;
		private DevComponents.AdvTree.Node node4;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
		private DevComponents.AdvTree.Node node6;
		private DevComponents.DotNetBar.ElementStyle elementStyle5;
		private DevComponents.AdvTree.Node nodeNewViews;
		private DevComponents.AdvTree.Node nodeEmpty;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.DotNetBar.ElementStyle elementStyle3;
		private DevComponents.DotNetBar.ElementStyle elementStyle4;

	}
}