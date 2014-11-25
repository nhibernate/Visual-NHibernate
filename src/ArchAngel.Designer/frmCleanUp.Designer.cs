namespace ArchAngel.Designer
{
	partial class frmCleanUp
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCleanUp));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panelTop = new System.Windows.Forms.Panel();
			this.labelPageDescription = new System.Windows.Forms.Label();
			this.labelPageHeader = new System.Windows.Forms.Label();
			this.pictureHeading = new System.Windows.Forms.PictureBox();
			this.treeListUserOptions = new DevComponents.AdvTree.AdvTree();
			this.node1 = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.treeListFunctions = new DevComponents.AdvTree.AdvTree();
			this.node2 = new DevComponents.AdvTree.Node();
			this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListUserOptions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListFunctions)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(694, 367);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.Location = new System.Drawing.Point(613, 367);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 7;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 362);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(777, 31);
			this.ucHeading1.TabIndex = 6;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Unused User Options";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 199);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Unused Functions";
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.White;
			this.panelTop.Controls.Add(this.labelPageDescription);
			this.panelTop.Controls.Add(this.labelPageHeader);
			this.panelTop.Controls.Add(this.pictureHeading);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Margin = new System.Windows.Forms.Padding(2);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(777, 46);
			this.panelTop.TabIndex = 21;
			// 
			// labelPageDescription
			// 
			this.labelPageDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelPageDescription.ForeColor = System.Drawing.Color.MidnightBlue;
			this.labelPageDescription.Location = new System.Drawing.Point(168, 17);
			this.labelPageDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelPageDescription.Name = "labelPageDescription";
			this.labelPageDescription.Size = new System.Drawing.Size(607, 29);
			this.labelPageDescription.TabIndex = 1;
			this.labelPageDescription.Text = "Functions and UserOptions that are no longer used in the project are displayed. Y" +
				"ou can delete those which you no longer require.";
			// 
			// labelPageHeader
			// 
			this.labelPageHeader.AutoSize = true;
			this.labelPageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPageHeader.ForeColor = System.Drawing.Color.MidnightBlue;
			this.labelPageHeader.Location = new System.Drawing.Point(154, 2);
			this.labelPageHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelPageHeader.Name = "labelPageHeader";
			this.labelPageHeader.Size = new System.Drawing.Size(283, 15);
			this.labelPageHeader.TabIndex = 0;
			this.labelPageHeader.Text = "Remove unused functions and UserOptions";
			// 
			// pictureHeading
			// 
			this.pictureHeading.Image = ((System.Drawing.Image)(resources.GetObject("pictureHeading.Image")));
			this.pictureHeading.Location = new System.Drawing.Point(0, 0);
			this.pictureHeading.Margin = new System.Windows.Forms.Padding(2);
			this.pictureHeading.Name = "pictureHeading";
			this.pictureHeading.Size = new System.Drawing.Size(150, 57);
			this.pictureHeading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureHeading.TabIndex = 2;
			this.pictureHeading.TabStop = false;
			// 
			// treeListUserOptions
			// 
			this.treeListUserOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeListUserOptions.AllowDrop = true;
			this.treeListUserOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeListUserOptions.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.treeListUserOptions.BackgroundStyle.Class = "TreeBorderKey";
			this.treeListUserOptions.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeListUserOptions.Location = new System.Drawing.Point(15, 76);
			this.treeListUserOptions.Name = "treeListUserOptions";
			this.treeListUserOptions.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
			this.treeListUserOptions.NodesConnector = this.nodeConnector1;
			this.treeListUserOptions.NodeStyle = this.elementStyle1;
			this.treeListUserOptions.PathSeparator = ";";
			this.treeListUserOptions.Size = new System.Drawing.Size(750, 120);
			this.treeListUserOptions.Styles.Add(this.elementStyle1);
			this.treeListUserOptions.TabIndex = 22;
			this.treeListUserOptions.Text = "advTree1";
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
			// treeListFunctions
			// 
			this.treeListFunctions.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeListFunctions.AllowDrop = true;
			this.treeListFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeListFunctions.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.treeListFunctions.BackgroundStyle.Class = "TreeBorderKey";
			this.treeListFunctions.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeListFunctions.Location = new System.Drawing.Point(15, 216);
			this.treeListFunctions.Name = "treeListFunctions";
			this.treeListFunctions.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node2});
			this.treeListFunctions.NodesConnector = this.nodeConnector2;
			this.treeListFunctions.NodeStyle = this.elementStyle2;
			this.treeListFunctions.PathSeparator = ";";
			this.treeListFunctions.Size = new System.Drawing.Size(750, 140);
			this.treeListFunctions.Styles.Add(this.elementStyle2);
			this.treeListFunctions.TabIndex = 23;
			this.treeListFunctions.Text = "advTree1";
			// 
			// node2
			// 
			this.node2.Expanded = true;
			this.node2.Name = "node2";
			this.node2.Text = "node2";
			// 
			// nodeConnector2
			// 
			this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle2
			// 
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// frmCleanUp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(777, 393);
			this.Controls.Add(this.treeListFunctions);
			this.Controls.Add(this.treeListUserOptions);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.ucHeading1);
			this.MinimumSize = new System.Drawing.Size(785, 427);
			this.Name = "frmCleanUp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Project Clean-Up";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCleanUp_FormClosing);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListUserOptions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListFunctions)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private Slyce.Common.Controls.ucHeading ucHeading1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Label labelPageDescription;
		private System.Windows.Forms.Label labelPageHeader;
		private System.Windows.Forms.PictureBox pictureHeading;
		private DevComponents.AdvTree.AdvTree treeListUserOptions;
		private DevComponents.AdvTree.Node node1;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.AdvTree.AdvTree treeListFunctions;
		private DevComponents.AdvTree.Node node2;
		private DevComponents.AdvTree.NodeConnector nodeConnector2;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
	}
}