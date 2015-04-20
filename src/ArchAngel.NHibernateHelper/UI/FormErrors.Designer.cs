namespace ArchAngel.NHibernateHelper.UI
{
	partial class FormErrors
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormErrors));
			this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
			this.buttonClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonCopy = new DevComponents.DotNetBar.ButtonX();
			this.ColumnMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.labelHeading = new DevComponents.DotNetBar.LabelX();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewX1
			// 
			this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMessage});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
			this.dataGridViewX1.Location = new System.Drawing.Point(12, 52);
			this.dataGridViewX1.Name = "dataGridViewX1";
			this.dataGridViewX1.RowHeadersVisible = false;
			this.dataGridViewX1.RowHeadersWidth = 25;
			this.dataGridViewX1.Size = new System.Drawing.Size(554, 207);
			this.dataGridViewX1.TabIndex = 0;
			// 
			// styleManager1
			// 
			this.styleManager1.ManagerColorTint = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Black;
			// 
			// buttonClose
			// 
			this.buttonClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(491, 268);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 35);
			this.buttonClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonCopy
			// 
			this.buttonCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCopy.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonCopy.HoverImage")));
			this.buttonCopy.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopy.Image")));
			this.buttonCopy.Location = new System.Drawing.Point(400, 268);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(75, 35);
			this.buttonCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCopy.TabIndex = 2;
			this.buttonCopy.Text = "Copy";
			this.buttonCopy.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// ColumnMessage
			// 
			this.ColumnMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ColumnMessage.HeaderText = "Error";
			this.ColumnMessage.Name = "ColumnMessage";
			// 
			// labelHeading
			// 
			// 
			// 
			// 
			this.labelHeading.BackgroundStyle.Class = "";
			this.labelHeading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelHeading.Location = new System.Drawing.Point(12, 12);
			this.labelHeading.Name = "labelHeading";
			this.labelHeading.Size = new System.Drawing.Size(554, 34);
			this.labelHeading.TabIndex = 3;
			this.labelHeading.Text = "labelX1";
			this.labelHeading.TextLineAlignment = System.Drawing.StringAlignment.Near;
			// 
			// FormErrors
			// 
			this.AcceptButton = this.buttonCopy;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(578, 312);
			this.Controls.Add(this.labelHeading);
			this.Controls.Add(this.buttonCopy);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.dataGridViewX1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormErrors";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Errors";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
		private DevComponents.DotNetBar.StyleManager styleManager1;
		private DevComponents.DotNetBar.ButtonX buttonClose;
		private DevComponents.DotNetBar.ButtonX buttonCopy;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMessage;
		private DevComponents.DotNetBar.LabelX labelHeading;
	}
}