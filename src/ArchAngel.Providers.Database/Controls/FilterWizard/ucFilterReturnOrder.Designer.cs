namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    partial class ucFilterReturnOrder
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFilterReturnOrder));
			this.imageListState = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.buttonDown = new System.Windows.Forms.Button();
			this.buttonUp = new System.Windows.Forms.Button();
			this.treeList1 = new DevComponents.AdvTree.AdvTree();
			this.node1 = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageListState
			// 
			this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
			this.imageListState.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListState.Images.SetKeyName(0, "");
			this.imageListState.Images.SetKeyName(1, "");
			this.imageListState.Images.SetKeyName(2, "");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Select return order:";
			// 
			// buttonDown
			// 
			this.buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonDown.Image")));
			this.buttonDown.Location = new System.Drawing.Point(570, 82);
			this.buttonDown.Name = "buttonDown";
			this.buttonDown.Size = new System.Drawing.Size(22, 23);
			this.buttonDown.TabIndex = 4;
			this.buttonDown.UseVisualStyleBackColor = true;
			this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
			// 
			// buttonUp
			// 
			this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.Image")));
			this.buttonUp.Location = new System.Drawing.Point(570, 53);
			this.buttonUp.Name = "buttonUp";
			this.buttonUp.Size = new System.Drawing.Size(22, 23);
			this.buttonUp.TabIndex = 5;
			this.buttonUp.UseVisualStyleBackColor = true;
			this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
			// 
			// treeList1
			// 
			this.treeList1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeList1.AllowDrop = true;
			this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeList1.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.treeList1.BackgroundStyle.Class = "TreeBorderKey";
			this.treeList1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeList1.Location = new System.Drawing.Point(4, 25);
			this.treeList1.Name = "treeList1";
			this.treeList1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
			this.treeList1.NodesConnector = this.nodeConnector1;
			this.treeList1.NodeStyle = this.elementStyle1;
			this.treeList1.PathSeparator = ";";
			this.treeList1.Size = new System.Drawing.Size(560, 341);
			this.treeList1.Styles.Add(this.elementStyle1);
			this.treeList1.TabIndex = 6;
			this.treeList1.Text = "advTree1";
			// 
			// node1
			// 
			this.node1.Expanded = true;
			this.node1.Name = "node1";
			this.node1.Text = "node1";
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// ucFilterReturnOrder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeList1);
			this.Controls.Add(this.buttonUp);
			this.Controls.Add(this.buttonDown);
			this.Controls.Add(this.label1);
			this.Name = "ucFilterReturnOrder";
			this.Size = new System.Drawing.Size(598, 369);
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.ImageList imageListState;
		private DevComponents.AdvTree.AdvTree treeList1;
		private DevComponents.AdvTree.Node node1;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}
