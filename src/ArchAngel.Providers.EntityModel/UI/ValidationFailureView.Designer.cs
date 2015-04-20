namespace ArchAngel.Providers.EntityModel.UI
{
	partial class ValidationFailureView
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidationFailureView));
			this.gridResultsView = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.LevelColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ObjectColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.buttonReCheck = new DevComponents.DotNetBar.ButtonX();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gridResultsView)).BeginInit();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridResultsView
			// 
			this.gridResultsView.AllowUserToAddRows = false;
			this.gridResultsView.AllowUserToDeleteRows = false;
			this.gridResultsView.AllowUserToResizeRows = false;
			this.gridResultsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridResultsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LevelColumn,
            this.DescriptionColumn,
            this.ObjectColumn});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridResultsView.DefaultCellStyle = dataGridViewCellStyle1;
			this.gridResultsView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridResultsView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.gridResultsView.Location = new System.Drawing.Point(0, 29);
			this.gridResultsView.Name = "gridResultsView";
			this.gridResultsView.ReadOnly = true;
			this.gridResultsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridResultsView.Size = new System.Drawing.Size(863, 203);
			this.gridResultsView.TabIndex = 0;
			this.gridResultsView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridResultsView_CellContentClick);
			this.gridResultsView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridResultsView_CellContentDoubleClick);
			this.gridResultsView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridResultsView_RowEnter);
			// 
			// LevelColumn
			// 
			this.LevelColumn.HeaderText = "Level";
			this.LevelColumn.Name = "LevelColumn";
			this.LevelColumn.ReadOnly = true;
			this.LevelColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.LevelColumn.Width = 40;
			// 
			// DescriptionColumn
			// 
			this.DescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.DescriptionColumn.HeaderText = "Description";
			this.DescriptionColumn.Name = "DescriptionColumn";
			this.DescriptionColumn.ReadOnly = true;
			// 
			// ObjectColumn
			// 
			this.ObjectColumn.HeaderText = "Object";
			this.ObjectColumn.Name = "ObjectColumn";
			this.ObjectColumn.ReadOnly = true;
			this.ObjectColumn.Width = 250;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.BackColor = System.Drawing.Color.Red;
			this.labelX1.BackgroundStyle.BackColor2 = System.Drawing.Color.Brown;
			this.labelX1.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.BackgroundStyle.TextColor = System.Drawing.Color.White;
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(0, 0);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(863, 29);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "Validation Issues";
			this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Fatal");
			this.imageList1.Images.SetKeyName(1, "Error");
			this.imageList1.Images.SetKeyName(2, "Warning");
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.buttonReCheck);
			this.panelEx1.Controls.Add(this.buttonX1);
			this.panelEx1.Controls.Add(this.labelX1);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(863, 29);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 2;
			// 
			// buttonReCheck
			// 
			this.buttonReCheck.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonReCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReCheck.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonReCheck.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonReCheck.HoverImage")));
			this.buttonReCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonReCheck.Image")));
			this.buttonReCheck.Location = new System.Drawing.Point(707, 0);
			this.buttonReCheck.Name = "buttonReCheck";
			this.buttonReCheck.Size = new System.Drawing.Size(91, 29);
			this.buttonReCheck.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonReCheck.TabIndex = 3;
			this.buttonReCheck.Text = "Re-Check";
			this.buttonReCheck.Click += new System.EventHandler(this.buttonReCheck_Click);
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
			this.buttonX1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
			this.buttonX1.Location = new System.Drawing.Point(815, 0);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(35, 29);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX1.TabIndex = 2;
			this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
			// 
			// ValidationFailureView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridResultsView);
			this.Controls.Add(this.panelEx1);
			this.Name = "ValidationFailureView";
			this.Size = new System.Drawing.Size(863, 232);
			((System.ComponentModel.ISupportInitialize)(this.gridResultsView)).EndInit();
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.DataGridViewX gridResultsView;
		private DevComponents.DotNetBar.LabelX labelX1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.DataGridViewImageColumn LevelColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
		private System.Windows.Forms.DataGridViewLinkColumn ObjectColumn;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.ButtonX buttonX1;
		private DevComponents.DotNetBar.ButtonX buttonReCheck;
	}
}
