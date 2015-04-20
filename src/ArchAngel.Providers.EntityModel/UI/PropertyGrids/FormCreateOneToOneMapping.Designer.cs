namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class FormCreateOneToOneMapping
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonSelectUnmapped = new System.Windows.Forms.Button();
            this.buttonSelectNone = new System.Windows.Forms.Button();
            this.listViewTables = new System.Windows.Forms.ListView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.buttonSelectAll);
            this.panelEx1.Controls.Add(this.buttonSelectUnmapped);
            this.panelEx1.Controls.Add(this.buttonSelectNone);
            this.panelEx1.Controls.Add(this.listViewTables);
            this.panelEx1.Controls.Add(this.buttonCancel);
            this.panelEx1.Controls.Add(this.buttonCreate);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(447, 416);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectAll.Location = new System.Drawing.Point(216, 38);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(65, 23);
            this.buttonSelectAll.TabIndex = 1;
            this.buttonSelectAll.Text = "All";
            this.toolTip1.SetToolTip(this.buttonSelectAll, "Check all tables");
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.selectAll_Click);
            // 
            // buttonSelectUnmapped
            // 
            this.buttonSelectUnmapped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectUnmapped.Location = new System.Drawing.Point(287, 38);
            this.buttonSelectUnmapped.Name = "buttonSelectUnmapped";
            this.buttonSelectUnmapped.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectUnmapped.TabIndex = 2;
            this.buttonSelectUnmapped.Text = "Unmapped";
            this.toolTip1.SetToolTip(this.buttonSelectUnmapped, "Check tables that don\'t have a mapped Entity");
            this.buttonSelectUnmapped.UseVisualStyleBackColor = true;
            this.buttonSelectUnmapped.Click += new System.EventHandler(this.selectUnmapped_Click);
            // 
            // buttonSelectNone
            // 
            this.buttonSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectNone.Location = new System.Drawing.Point(368, 38);
            this.buttonSelectNone.Name = "buttonSelectNone";
            this.buttonSelectNone.Size = new System.Drawing.Size(65, 23);
            this.buttonSelectNone.TabIndex = 3;
            this.buttonSelectNone.Text = "None";
            this.toolTip1.SetToolTip(this.buttonSelectNone, "Uncheck all tables");
            this.buttonSelectNone.UseVisualStyleBackColor = true;
            this.buttonSelectNone.Click += new System.EventHandler(this.selectNone_Click);
            // 
            // listViewTables
            // 
            this.listViewTables.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTables.CheckBoxes = true;
            this.listViewTables.FullRowSelect = true;
            this.listViewTables.Location = new System.Drawing.Point(16, 67);
            this.listViewTables.Name = "listViewTables";
            this.listViewTables.ShowItemToolTips = true;
            this.listViewTables.Size = new System.Drawing.Size(417, 309);
            this.listViewTables.TabIndex = 4;
            this.listViewTables.UseCompatibleStateImageBehavior = false;
            this.listViewTables.View = System.Windows.Forms.View.List;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(358, 382);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.buttonCancel, "Cancel this operation");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreate.Location = new System.Drawing.Point(277, 382);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 5;
            this.buttonCreate.Text = "Create";
            this.toolTip1.SetToolTip(this.buttonCreate, "Create the new Entities from the selected Tables");
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(4, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(440, 31);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Create One To One Mappings Of The Following Tables:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // FormCreateOneToOneMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 416);
            this.Controls.Add(this.panelEx1);
            this.Name = "FormCreateOneToOneMapping";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private System.Windows.Forms.Button buttonCreate;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ListView listViewTables;
		private System.Windows.Forms.Button buttonSelectUnmapped;
		private System.Windows.Forms.Button buttonSelectNone;
		private System.Windows.Forms.Button buttonSelectAll;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
