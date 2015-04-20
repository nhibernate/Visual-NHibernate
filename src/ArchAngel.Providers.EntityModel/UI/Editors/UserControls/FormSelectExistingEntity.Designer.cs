namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class FormSelectExistingEntity
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Test");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test2");
			this.checkBoxCreateNewEntity = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxSelectExistingEntity = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.labelSelectText = new DevComponents.DotNetBar.LabelX();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonOk = new DevComponents.DotNetBar.ButtonX();
			this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
			this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBoxCreateNewEntity
			// 
			this.checkBoxCreateNewEntity.AutoSize = true;
			// 
			// 
			// 
			this.checkBoxCreateNewEntity.BackgroundStyle.Class = "";
			this.checkBoxCreateNewEntity.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxCreateNewEntity.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.checkBoxCreateNewEntity.Location = new System.Drawing.Point(12, 12);
			this.checkBoxCreateNewEntity.Name = "checkBoxCreateNewEntity";
			this.checkBoxCreateNewEntity.Size = new System.Drawing.Size(208, 15);
			this.checkBoxCreateNewEntity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.checkBoxCreateNewEntity, new DevComponents.DotNetBar.SuperTooltipInfo("Create new entity", "", "Create a new entity based on this table, with a property for each column in the t" +
						"able.", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.checkBoxCreateNewEntity.TabIndex = 12;
			this.checkBoxCreateNewEntity.Text = "  Create a new abstract entity (TPCC):";
			this.checkBoxCreateNewEntity.TextColor = System.Drawing.Color.White;
			this.checkBoxCreateNewEntity.CheckedChanged += new System.EventHandler(this.checkBoxCreateNewEntity_CheckedChanged);
			// 
			// checkBoxSelectExistingEntity
			// 
			this.checkBoxSelectExistingEntity.AutoSize = true;
			// 
			// 
			// 
			this.checkBoxSelectExistingEntity.BackgroundStyle.Class = "";
			this.checkBoxSelectExistingEntity.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxSelectExistingEntity.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.checkBoxSelectExistingEntity.Checked = true;
			this.checkBoxSelectExistingEntity.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxSelectExistingEntity.CheckValue = "Y";
			this.checkBoxSelectExistingEntity.Location = new System.Drawing.Point(12, 33);
			this.checkBoxSelectExistingEntity.Name = "checkBoxSelectExistingEntity";
			this.checkBoxSelectExistingEntity.Size = new System.Drawing.Size(147, 15);
			this.checkBoxSelectExistingEntity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.checkBoxSelectExistingEntity, new DevComponents.DotNetBar.SuperTooltipInfo("Map to existing entity", "", "Map this table to an existing entity.", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.checkBoxSelectExistingEntity.TabIndex = 13;
			this.checkBoxSelectExistingEntity.Text = "  Select an existing entity:";
			this.checkBoxSelectExistingEntity.TextColor = System.Drawing.Color.White;
			this.checkBoxSelectExistingEntity.CheckedChanged += new System.EventHandler(this.checkBoxSelectExistingEntity_CheckedChanged);
			// 
			// labelSelectText
			// 
			this.labelSelectText.AutoSize = true;
			// 
			// 
			// 
			this.labelSelectText.BackgroundStyle.Class = "";
			this.labelSelectText.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelSelectText.ForeColor = System.Drawing.Color.White;
			this.labelSelectText.Location = new System.Drawing.Point(12, 57);
			this.labelSelectText.Name = "labelSelectText";
			this.labelSelectText.Size = new System.Drawing.Size(114, 15);
			this.labelSelectText.TabIndex = 14;
			this.labelSelectText.Text = "Select existing entities:";
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonOk
			// 
			this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonOk.Location = new System.Drawing.Point(121, 276);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonOk.TabIndex = 15;
			this.buttonOk.Text = "Ok";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(202, 276);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCancel.TabIndex = 16;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// listViewEx1
			// 
			this.listViewEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.listViewEx1.Border.Class = "ListViewBorder";
			this.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.listViewEx1.CheckBoxes = true;
			this.listViewEx1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listViewEx1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			this.listViewEx1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listViewEx1.Location = new System.Drawing.Point(12, 78);
			this.listViewEx1.Name = "listViewEx1";
			this.listViewEx1.Size = new System.Drawing.Size(375, 182);
			this.listViewEx1.TabIndex = 17;
			this.listViewEx1.UseCompatibleStateImageBehavior = false;
			this.listViewEx1.View = System.Windows.Forms.View.Details;
			this.listViewEx1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewEx1_ItemChecked);
			this.listViewEx1.SelectedIndexChanged += new System.EventHandler(this.listViewEx1_SelectedIndexChanged);
			this.listViewEx1.SizeChanged += new System.EventHandler(this.listViewEx1_SizeChanged);
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.textBoxName);
			this.panelEx1.Controls.Add(this.listViewEx1);
			this.panelEx1.Controls.Add(this.checkBoxCreateNewEntity);
			this.panelEx1.Controls.Add(this.buttonCancel);
			this.panelEx1.Controls.Add(this.checkBoxSelectExistingEntity);
			this.panelEx1.Controls.Add(this.buttonOk);
			this.panelEx1.Controls.Add(this.labelSelectText);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(399, 311);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 18;
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(223, 12);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(164, 20);
			this.textBoxName.TabIndex = 18;
			this.textBoxName.Text = "NewName";
			// 
			// FormSelectEntity
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(399, 311);
			this.Controls.Add(this.panelEx1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormSelectEntity";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FormSelectEntity";
			this.Load += new System.EventHandler(this.FormSelectEntity_Load);
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxCreateNewEntity;
		private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxSelectExistingEntity;
		private DevComponents.DotNetBar.LabelX labelSelectText;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private DevComponents.DotNetBar.ButtonX buttonOk;
		private DevComponents.DotNetBar.ButtonX buttonCancel;
		private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.TextBox textBoxName;
	}
}