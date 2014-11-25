namespace ArchAngel.Workbench.Wizards.NewProject
{
    partial class Screen2
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("One two three", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("filename", 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen2));
            this.listTemplates = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnBack = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tvProviders = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTemplateDescription = new System.Windows.Forms.Label();
            this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.SuspendLayout();
            // 
            // listTemplates
            // 
            this.listTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listTemplates.BackColor = System.Drawing.Color.White;
            listViewGroup1.Header = "One two three";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "XXXX";
            this.listTemplates.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listTemplates.HideSelection = false;
            listViewItem1.ToolTipText = "full path";
            this.listTemplates.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listTemplates.LargeImageList = this.imageList1;
            this.listTemplates.Location = new System.Drawing.Point(190, 27);
            this.listTemplates.MultiSelect = false;
            this.listTemplates.Name = "listTemplates";
            this.listTemplates.ShowItemToolTips = true;
            this.listTemplates.Size = new System.Drawing.Size(291, 222);
            this.listTemplates.SmallImageList = this.imageList1;
            this.listTemplates.TabIndex = 1;
            this.listTemplates.UseCompatibleStateImageBehavior = false;
            this.listTemplates.View = System.Windows.Forms.View.SmallIcon;
            this.listTemplates.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTemplates_MouseClick);
            this.listTemplates.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.listTemplates_ItemMouseHover);
            this.listTemplates.SelectedIndexChanged += new System.EventHandler(this.listTemplates_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "aal.ico");
            this.imageList1.Images.SetKeyName(1, "PostActions.ico");
            this.imageList1.Images.SetKeyName(2, "FindHS.png");
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.ForeColor = System.Drawing.Color.Black;
            this.btnBack.Location = new System.Drawing.Point(23, 340);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.ForeColor = System.Drawing.Color.Black;
            this.btnOpen.Location = new System.Drawing.Point(408, 340);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(73, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Next >";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tvProviders
            // 
            this.tvProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvProviders.FullRowSelect = true;
            this.tvProviders.HideSelection = false;
            this.tvProviders.Location = new System.Drawing.Point(21, 27);
            this.tvProviders.Name = "tvProviders";
            this.tvProviders.Size = new System.Drawing.Size(163, 222);
            this.tvProviders.TabIndex = 0;
            this.tvProviders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProviders_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Providers:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Templates:";
            // 
            // lblTemplateDescription
            // 
            this.lblTemplateDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemplateDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplateDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTemplateDescription.Location = new System.Drawing.Point(21, 252);
            this.lblTemplateDescription.Name = "lblTemplateDescription";
            this.lblTemplateDescription.Size = new System.Drawing.Size(460, 72);
            this.lblTemplateDescription.TabIndex = 21;
            // 
            // customValidator1
            // 
            this.customValidator1.ErrorMessage = "Your error message here.";
            this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // Screen2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.lblTemplateDescription);
            this.Controls.Add(this.listTemplates);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvProviders);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnBack);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(315, 377);
            this.Name = "Screen2";
            this.Size = new System.Drawing.Size(502, 377);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ListView listTemplates;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TreeView tvProviders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTemplateDescription;
		private System.Windows.Forms.ImageList imageList1;
		private DevComponents.DotNetBar.Validator.CustomValidator customValidator1;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
    }
}
