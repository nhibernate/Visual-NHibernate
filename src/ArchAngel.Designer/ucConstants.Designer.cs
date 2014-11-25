namespace ArchAngel.Designer
{
	partial class ucConstants
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucConstants));
            this.btnNew = new System.Windows.Forms.Button();
            this.btnRemoveConstant = new System.Windows.Forms.Button();
            this.lstConstants = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.ucLabel1 = new Slyce.Common.Controls.ucHeading();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(536, 30);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(78, 24);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "      &New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnRemoveConstant
            // 
            this.btnRemoveConstant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveConstant.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveConstant.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveConstant.Image")));
            this.btnRemoveConstant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveConstant.Location = new System.Drawing.Point(536, 87);
            this.btnRemoveConstant.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveConstant.Name = "btnRemoveConstant";
            this.btnRemoveConstant.Size = new System.Drawing.Size(78, 24);
            this.btnRemoveConstant.TabIndex = 10;
            this.btnRemoveConstant.Text = "      &Remove";
            this.btnRemoveConstant.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveConstant.UseVisualStyleBackColor = true;
            this.btnRemoveConstant.Click += new System.EventHandler(this.btnRemoveConstant_Click);
            // 
            // lstConstants
            // 
            this.lstConstants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConstants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstConstants.FullRowSelect = true;
            this.lstConstants.GridLines = true;
            this.lstConstants.HideSelection = false;
            this.lstConstants.Location = new System.Drawing.Point(14, 30);
            this.lstConstants.Margin = new System.Windows.Forms.Padding(2);
            this.lstConstants.Name = "lstConstants";
            this.lstConstants.Size = new System.Drawing.Size(518, 514);
            this.lstConstants.TabIndex = 11;
            this.lstConstants.UseCompatibleStateImageBehavior = false;
            this.lstConstants.View = System.Windows.Forms.View.Details;
            this.lstConstants.DoubleClick += new System.EventHandler(this.lstConstants_DoubleClick);
            this.lstConstants.SelectedIndexChanged += new System.EventHandler(this.lstConstants_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 126;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Value";
            this.columnHeader3.Width = 363;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Runtime";
            this.columnHeader4.Width = 78;
            // 
            // ucLabel1
            // 
            this.ucLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLabel1.Location = new System.Drawing.Point(0, 0);
            this.ucLabel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.Size = new System.Drawing.Size(625, 25);
            this.ucLabel1.TabIndex = 5;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(536, 58);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(78, 24);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "      &Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // ucConstants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lstConstants);
            this.Controls.Add(this.btnRemoveConstant);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.ucLabel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucConstants";
            this.Size = new System.Drawing.Size(625, 556);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucConstants_Paint);
            this.ResumeLayout(false);

		}

		#endregion

        private Slyce.Common.Controls.ucHeading ucLabel1;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnRemoveConstant;
        private System.Windows.Forms.ListView lstConstants;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button btnEdit;

	}
}
