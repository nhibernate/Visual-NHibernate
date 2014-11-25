namespace ArchAngel.NHibernateHelper.UI
{
    partial class FormSelectClasses
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCsProjFile = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonFetch = new System.Windows.Forms.Button();
            this.advTree = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            ((System.ComponentModel.ISupportInitialize)(this.advTree)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Visual Studio project:";
            // 
            // textBoxCsProjFile
            // 
            this.textBoxCsProjFile.Location = new System.Drawing.Point(124, 14);
            this.textBoxCsProjFile.Name = "textBoxCsProjFile";
            this.textBoxCsProjFile.Size = new System.Drawing.Size(479, 20);
            this.textBoxCsProjFile.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(609, 12);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(36, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            // 
            // buttonFetch
            // 
            this.buttonFetch.Location = new System.Drawing.Point(304, 56);
            this.buttonFetch.Name = "buttonFetch";
            this.buttonFetch.Size = new System.Drawing.Size(75, 23);
            this.buttonFetch.TabIndex = 3;
            this.buttonFetch.Text = "Get Classes";
            this.buttonFetch.UseVisualStyleBackColor = true;
            this.buttonFetch.Click += new System.EventHandler(this.buttonFetch_Click);
            // 
            // advTree
            // 
            this.advTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTree.AllowDrop = true;
            this.advTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTree.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTree.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.advTree.Location = new System.Drawing.Point(18, 102);
            this.advTree.Name = "advTree";
            this.advTree.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.advTree.NodesConnector = this.nodeConnector1;
            this.advTree.NodeStyle = this.elementStyle1;
            this.advTree.PathSeparator = ";";
            this.advTree.Size = new System.Drawing.Size(618, 289);
            this.advTree.Styles.Add(this.elementStyle1);
            this.advTree.TabIndex = 4;
            this.advTree.Text = "advTree1";
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
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // FormSelectClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 403);
            this.Controls.Add(this.advTree);
            this.Controls.Add(this.buttonFetch);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxCsProjFile);
            this.Controls.Add(this.label1);
            this.Name = "FormSelectClasses";
            this.Text = "Select Classes";
            ((System.ComponentModel.ISupportInitialize)(this.advTree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCsProjFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonFetch;
        private DevComponents.AdvTree.AdvTree advTree;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}