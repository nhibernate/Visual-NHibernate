namespace ArchAngel.Providers.EntityModel.UI
{
	partial class FormConvertKeyToComponent
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConvertKeyToComponent));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.wizard1 = new DevComponents.DotNetBar.Wizard();
			this.wizardPageCreateNewOrChooseExisting = new DevComponents.DotNetBar.WizardPage();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.radioButtonUseExisting = new System.Windows.Forms.RadioButton();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.radioButtonCreateNewComponent = new System.Windows.Forms.RadioButton();
			this.wizardPageCreateNew = new DevComponents.DotNetBar.WizardPage();
			this.checkBoxCreateNewCompDeleteProperties = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.textBoxNewComponentName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.textBoxNewComponentDefName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.wizardPageChooseExisting = new DevComponents.DotNetBar.WizardPage();
			this.comboBoxExistingComponentDefs = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelX6 = new DevComponents.DotNetBar.LabelX();
			this.wizardPageMapExisting = new DevComponents.DotNetBar.WizardPage();
			this.textBoxUseExistingNameOfComponent = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX7 = new DevComponents.DotNetBar.LabelX();
			this.dataGridViewPropertyMappings = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.ComponentPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ExistingKeyProperty = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.wizardPageFinish = new DevComponents.DotNetBar.WizardPage();
			this.labelFinishedChanges = new DevComponents.DotNetBar.LabelX();
			this.labelX8 = new DevComponents.DotNetBar.LabelX();
			this.checkBoxUseExistingDeleteExistingProperties = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.wizard1.SuspendLayout();
			this.wizardPageCreateNewOrChooseExisting.SuspendLayout();
			this.wizardPageCreateNew.SuspendLayout();
			this.wizardPageChooseExisting.SuspendLayout();
			this.wizardPageMapExisting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPropertyMappings)).BeginInit();
			this.wizardPageFinish.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizard1
			// 
			this.wizard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(229)))), ((int)(((byte)(253)))));
			this.wizard1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("wizard1.BackgroundImage")));
			this.wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
			this.wizard1.Cursor = System.Windows.Forms.Cursors.Default;
			this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wizard1.FinishButtonTabIndex = 3;
			// 
			// 
			// 
			this.wizard1.FooterStyle.BackColor = System.Drawing.Color.Transparent;
			this.wizard1.FooterStyle.Class = "";
			this.wizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
			this.wizard1.HeaderCaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.wizard1.HeaderHeight = 90;
			this.wizard1.HeaderImageVisible = false;
			// 
			// 
			// 
			this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.Transparent;
			this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
			this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
			this.wizard1.HeaderStyle.BorderBottomWidth = 1;
			this.wizard1.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.wizard1.HeaderStyle.BorderLeftWidth = 1;
			this.wizard1.HeaderStyle.BorderRightWidth = 1;
			this.wizard1.HeaderStyle.BorderTopWidth = 1;
			this.wizard1.HeaderStyle.Class = "";
			this.wizard1.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.wizard1.HelpButtonVisible = false;
			this.wizard1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.wizard1.Location = new System.Drawing.Point(0, 0);
			this.wizard1.Name = "wizard1";
			this.wizard1.Size = new System.Drawing.Size(613, 360);
			this.wizard1.TabIndex = 0;
			this.wizard1.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPageCreateNewOrChooseExisting,
            this.wizardPageCreateNew,
            this.wizardPageChooseExisting,
            this.wizardPageMapExisting,
            this.wizardPageFinish});
			this.wizard1.CancelButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_CancelButtonClick);
			this.wizard1.WizardPageChanging += new DevComponents.DotNetBar.WizardCancelPageChangeEventHandler(this.wizard1_WizardPageChanging);
			this.wizard1.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_FinishButtonClick);
			// 
			// wizardPageCreateNewOrChooseExisting
			// 
			this.wizardPageCreateNewOrChooseExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.wizardPageCreateNewOrChooseExisting.AntiAlias = false;
			this.wizardPageCreateNewOrChooseExisting.BackColor = System.Drawing.Color.Transparent;
			this.wizardPageCreateNewOrChooseExisting.Controls.Add(this.labelX2);
			this.wizardPageCreateNewOrChooseExisting.Controls.Add(this.radioButtonUseExisting);
			this.wizardPageCreateNewOrChooseExisting.Controls.Add(this.labelX1);
			this.wizardPageCreateNewOrChooseExisting.Controls.Add(this.radioButtonCreateNewComponent);
			this.wizardPageCreateNewOrChooseExisting.Location = new System.Drawing.Point(7, 102);
			this.wizardPageCreateNewOrChooseExisting.Name = "wizardPageCreateNewOrChooseExisting";
			this.wizardPageCreateNewOrChooseExisting.PageDescription = "Choose whether to create a new Component Definition for your key, or reuse an old" +
				" one";
			this.wizardPageCreateNewOrChooseExisting.PageTitle = "Create New Component Definition Or Use Existing Definition";
			this.wizardPageCreateNewOrChooseExisting.Size = new System.Drawing.Size(599, 200);
			// 
			// 
			// 
			this.wizardPageCreateNewOrChooseExisting.Style.Class = "";
			// 
			// 
			// 
			this.wizardPageCreateNewOrChooseExisting.StyleMouseDown.Class = "";
			// 
			// 
			// 
			this.wizardPageCreateNewOrChooseExisting.StyleMouseOver.Class = "";
			this.wizardPageCreateNewOrChooseExisting.TabIndex = 7;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.Location = new System.Drawing.Point(15, 88);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(405, 23);
			this.labelX2.TabIndex = 3;
			this.labelX2.Text = "Choose an existing Component Definition to use as the Key";
			// 
			// radioButtonUseExisting
			// 
			this.radioButtonUseExisting.AutoSize = true;
			this.radioButtonUseExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonUseExisting.Location = new System.Drawing.Point(15, 65);
			this.radioButtonUseExisting.Name = "radioButtonUseExisting";
			this.radioButtonUseExisting.Size = new System.Drawing.Size(220, 17);
			this.radioButtonUseExisting.TabIndex = 2;
			this.radioButtonUseExisting.Text = "Use Existing Component Definition";
			this.radioButtonUseExisting.UseVisualStyleBackColor = true;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.Location = new System.Drawing.Point(15, 27);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(405, 23);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "Creates a new Component Definition from the properties in the existing Key";
			// 
			// radioButtonCreateNewComponent
			// 
			this.radioButtonCreateNewComponent.AutoSize = true;
			this.radioButtonCreateNewComponent.Checked = true;
			this.radioButtonCreateNewComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonCreateNewComponent.Location = new System.Drawing.Point(15, 3);
			this.radioButtonCreateNewComponent.Name = "radioButtonCreateNewComponent";
			this.radioButtonCreateNewComponent.Size = new System.Drawing.Size(216, 17);
			this.radioButtonCreateNewComponent.TabIndex = 0;
			this.radioButtonCreateNewComponent.TabStop = true;
			this.radioButtonCreateNewComponent.Text = "Create New Component Definition";
			this.radioButtonCreateNewComponent.UseVisualStyleBackColor = true;
			// 
			// wizardPageCreateNew
			// 
			this.wizardPageCreateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.wizardPageCreateNew.AntiAlias = false;
			this.wizardPageCreateNew.BackColor = System.Drawing.Color.Transparent;
			this.wizardPageCreateNew.Controls.Add(this.checkBoxCreateNewCompDeleteProperties);
			this.wizardPageCreateNew.Controls.Add(this.labelX5);
			this.wizardPageCreateNew.Controls.Add(this.textBoxNewComponentName);
			this.wizardPageCreateNew.Controls.Add(this.labelX4);
			this.wizardPageCreateNew.Controls.Add(this.textBoxNewComponentDefName);
			this.wizardPageCreateNew.Controls.Add(this.labelX3);
			this.wizardPageCreateNew.Location = new System.Drawing.Point(7, 102);
			this.wizardPageCreateNew.Name = "wizardPageCreateNew";
			this.wizardPageCreateNew.PageDescription = "Setup the new Component Definition to be created from your current Composite Key";
			this.wizardPageCreateNew.PageTitle = "Create New Component Definition";
			this.wizardPageCreateNew.Size = new System.Drawing.Size(599, 200);
			// 
			// 
			// 
			this.wizardPageCreateNew.Style.Class = "";
			// 
			// 
			// 
			this.wizardPageCreateNew.StyleMouseDown.Class = "";
			// 
			// 
			// 
			this.wizardPageCreateNew.StyleMouseOver.Class = "";
			this.wizardPageCreateNew.TabIndex = 8;
			// 
			// checkBoxCreateNewCompDeleteProperties
			// 
			// 
			// 
			// 
			this.checkBoxCreateNewCompDeleteProperties.BackgroundStyle.Class = "";
			this.checkBoxCreateNewCompDeleteProperties.Location = new System.Drawing.Point(164, 86);
			this.checkBoxCreateNewCompDeleteProperties.Name = "checkBoxCreateNewCompDeleteProperties";
			this.checkBoxCreateNewCompDeleteProperties.Size = new System.Drawing.Size(38, 23);
			this.checkBoxCreateNewCompDeleteProperties.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxCreateNewCompDeleteProperties.TabIndex = 5;
			this.checkBoxCreateNewCompDeleteProperties.Text = "checkBoxX1";
			this.checkBoxCreateNewCompDeleteProperties.TextVisible = false;
			// 
			// labelX5
			// 
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.Class = "";
			this.labelX5.Location = new System.Drawing.Point(5, 86);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(152, 23);
			this.labelX5.TabIndex = 4;
			this.labelX5.Text = "Delete Old Properties?";
			// 
			// textBoxNewComponentName
			// 
			// 
			// 
			// 
			this.textBoxNewComponentName.Border.Class = "TextBoxBorder";
			this.textBoxNewComponentName.Location = new System.Drawing.Point(164, 59);
			this.textBoxNewComponentName.Name = "textBoxNewComponentName";
			this.textBoxNewComponentName.Size = new System.Drawing.Size(215, 20);
			this.textBoxNewComponentName.TabIndex = 3;
			// 
			// labelX4
			// 
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.Class = "";
			this.labelX4.Location = new System.Drawing.Point(5, 57);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(152, 23);
			this.labelX4.TabIndex = 2;
			this.labelX4.Text = "Component Name:";
			// 
			// textBoxNewComponentDefName
			// 
			// 
			// 
			// 
			this.textBoxNewComponentDefName.Border.Class = "TextBoxBorder";
			this.textBoxNewComponentDefName.Location = new System.Drawing.Point(164, 30);
			this.textBoxNewComponentDefName.Name = "textBoxNewComponentDefName";
			this.textBoxNewComponentDefName.Size = new System.Drawing.Size(215, 20);
			this.textBoxNewComponentDefName.TabIndex = 1;
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.Location = new System.Drawing.Point(5, 28);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(152, 23);
			this.labelX3.TabIndex = 0;
			this.labelX3.Text = "Component Definition Name:";
			// 
			// wizardPageChooseExisting
			// 
			this.wizardPageChooseExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.wizardPageChooseExisting.AntiAlias = false;
			this.wizardPageChooseExisting.BackColor = System.Drawing.Color.Transparent;
			this.wizardPageChooseExisting.Controls.Add(this.comboBoxExistingComponentDefs);
			this.wizardPageChooseExisting.Controls.Add(this.labelX6);
			this.wizardPageChooseExisting.Location = new System.Drawing.Point(7, 102);
			this.wizardPageChooseExisting.Name = "wizardPageChooseExisting";
			this.wizardPageChooseExisting.PageTitle = "Choose an Existing Component Definition";
			this.wizardPageChooseExisting.Size = new System.Drawing.Size(599, 200);
			// 
			// 
			// 
			this.wizardPageChooseExisting.Style.Class = "";
			// 
			// 
			// 
			this.wizardPageChooseExisting.StyleMouseDown.Class = "";
			// 
			// 
			// 
			this.wizardPageChooseExisting.StyleMouseOver.Class = "";
			this.wizardPageChooseExisting.TabIndex = 9;
			// 
			// comboBoxExistingComponentDefs
			// 
			this.comboBoxExistingComponentDefs.DisplayMember = "Text";
			this.comboBoxExistingComponentDefs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxExistingComponentDefs.FormattingEnabled = true;
			this.comboBoxExistingComponentDefs.ItemHeight = 14;
			this.comboBoxExistingComponentDefs.Location = new System.Drawing.Point(188, 5);
			this.comboBoxExistingComponentDefs.Name = "comboBoxExistingComponentDefs";
			this.comboBoxExistingComponentDefs.Size = new System.Drawing.Size(251, 20);
			this.comboBoxExistingComponentDefs.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxExistingComponentDefs.TabIndex = 1;
			// 
			// labelX6
			// 
			// 
			// 
			// 
			this.labelX6.BackgroundStyle.Class = "";
			this.labelX6.Location = new System.Drawing.Point(6, 4);
			this.labelX6.Name = "labelX6";
			this.labelX6.Size = new System.Drawing.Size(175, 23);
			this.labelX6.TabIndex = 0;
			this.labelX6.Text = "Existing Component Definitions:";
			// 
			// wizardPageMapExisting
			// 
			this.wizardPageMapExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.wizardPageMapExisting.AntiAlias = false;
			this.wizardPageMapExisting.BackColor = System.Drawing.Color.Transparent;
			this.wizardPageMapExisting.Controls.Add(this.checkBoxUseExistingDeleteExistingProperties);
			this.wizardPageMapExisting.Controls.Add(this.labelX8);
			this.wizardPageMapExisting.Controls.Add(this.textBoxUseExistingNameOfComponent);
			this.wizardPageMapExisting.Controls.Add(this.labelX7);
			this.wizardPageMapExisting.Controls.Add(this.dataGridViewPropertyMappings);
			this.wizardPageMapExisting.Location = new System.Drawing.Point(7, 102);
			this.wizardPageMapExisting.Name = "wizardPageMapExisting";
			this.wizardPageMapExisting.PageDescription = "Map the Properties on the Component to the Properties in currently in the Key";
			this.wizardPageMapExisting.PageTitle = "Map Existing Component Properties";
			this.wizardPageMapExisting.Size = new System.Drawing.Size(599, 200);
			// 
			// 
			// 
			this.wizardPageMapExisting.Style.Class = "";
			// 
			// 
			// 
			this.wizardPageMapExisting.StyleMouseDown.Class = "";
			// 
			// 
			// 
			this.wizardPageMapExisting.StyleMouseOver.Class = "";
			this.wizardPageMapExisting.TabIndex = 10;
			// 
			// textBoxUseExistingNameOfComponent
			// 
			// 
			// 
			// 
			this.textBoxUseExistingNameOfComponent.Border.Class = "TextBoxBorder";
			this.textBoxUseExistingNameOfComponent.Location = new System.Drawing.Point(156, 1);
			this.textBoxUseExistingNameOfComponent.Name = "textBoxUseExistingNameOfComponent";
			this.textBoxUseExistingNameOfComponent.Size = new System.Drawing.Size(225, 20);
			this.textBoxUseExistingNameOfComponent.TabIndex = 2;
			// 
			// labelX7
			// 
			// 
			// 
			// 
			this.labelX7.BackgroundStyle.Class = "";
			this.labelX7.Location = new System.Drawing.Point(6, 0);
			this.labelX7.Name = "labelX7";
			this.labelX7.Size = new System.Drawing.Size(144, 20);
			this.labelX7.TabIndex = 1;
			this.labelX7.Text = "Name Of Key:";
			// 
			// dataGridViewPropertyMappings
			// 
			this.dataGridViewPropertyMappings.AllowUserToAddRows = false;
			this.dataGridViewPropertyMappings.AllowUserToDeleteRows = false;
			this.dataGridViewPropertyMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewPropertyMappings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewPropertyMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPropertyMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ComponentPropertyName,
            this.ExistingKeyProperty});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewPropertyMappings.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewPropertyMappings.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.dataGridViewPropertyMappings.Location = new System.Drawing.Point(5, 51);
			this.dataGridViewPropertyMappings.Name = "dataGridViewPropertyMappings";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewPropertyMappings.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewPropertyMappings.Size = new System.Drawing.Size(589, 149);
			this.dataGridViewPropertyMappings.TabIndex = 0;
			// 
			// ComponentPropertyName
			// 
			this.ComponentPropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ComponentPropertyName.HeaderText = "Component Property";
			this.ComponentPropertyName.Name = "ComponentPropertyName";
			this.ComponentPropertyName.ReadOnly = true;
			// 
			// ExistingKeyProperty
			// 
			this.ExistingKeyProperty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ExistingKeyProperty.HeaderText = "Existing Key Property";
			this.ExistingKeyProperty.Name = "ExistingKeyProperty";
			// 
			// wizardPageFinish
			// 
			this.wizardPageFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.wizardPageFinish.AntiAlias = false;
			this.wizardPageFinish.BackColor = System.Drawing.Color.Transparent;
			this.wizardPageFinish.Controls.Add(this.labelFinishedChanges);
			this.wizardPageFinish.Location = new System.Drawing.Point(7, 102);
			this.wizardPageFinish.Name = "wizardPageFinish";
			this.wizardPageFinish.PageDescription = "Click finish to apply the your changes";
			this.wizardPageFinish.PageTitle = "Finished!";
			this.wizardPageFinish.Size = new System.Drawing.Size(599, 200);
			// 
			// 
			// 
			this.wizardPageFinish.Style.Class = "";
			// 
			// 
			// 
			this.wizardPageFinish.StyleMouseDown.Class = "";
			// 
			// 
			// 
			this.wizardPageFinish.StyleMouseOver.Class = "";
			this.wizardPageFinish.TabIndex = 11;
			// 
			// labelFinishedChanges
			// 
			// 
			// 
			// 
			this.labelFinishedChanges.BackgroundStyle.Class = "";
			this.labelFinishedChanges.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelFinishedChanges.Location = new System.Drawing.Point(0, 0);
			this.labelFinishedChanges.Name = "labelFinishedChanges";
			this.labelFinishedChanges.PaddingBottom = 30;
			this.labelFinishedChanges.PaddingLeft = 30;
			this.labelFinishedChanges.PaddingRight = 30;
			this.labelFinishedChanges.PaddingTop = 30;
			this.labelFinishedChanges.Size = new System.Drawing.Size(599, 200);
			this.labelFinishedChanges.TabIndex = 0;
			this.labelFinishedChanges.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelFinishedChanges.WordWrap = true;
			// 
			// labelX8
			// 
			// 
			// 
			// 
			this.labelX8.BackgroundStyle.Class = "";
			this.labelX8.Location = new System.Drawing.Point(6, 26);
			this.labelX8.Name = "labelX8";
			this.labelX8.Size = new System.Drawing.Size(144, 20);
			this.labelX8.TabIndex = 3;
			this.labelX8.Text = "Delete Existing Properties:";
			// 
			// checkBoxUseExistingDeleteExistingProperties
			// 
			// 
			// 
			// 
			this.checkBoxUseExistingDeleteExistingProperties.BackgroundStyle.Class = "";
			this.checkBoxUseExistingDeleteExistingProperties.Location = new System.Drawing.Point(157, 22);
			this.checkBoxUseExistingDeleteExistingProperties.Name = "checkBoxUseExistingDeleteExistingProperties";
			this.checkBoxUseExistingDeleteExistingProperties.Size = new System.Drawing.Size(97, 23);
			this.checkBoxUseExistingDeleteExistingProperties.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxUseExistingDeleteExistingProperties.TabIndex = 4;
			this.checkBoxUseExistingDeleteExistingProperties.Text = "checkBoxX1";
			this.checkBoxUseExistingDeleteExistingProperties.TextVisible = false;
			// 
			// FormConvertKeyToComponent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(613, 360);
			this.Controls.Add(this.wizard1);
			this.Name = "FormConvertKeyToComponent";
			this.Text = "FormConvertKeyToComponent";
			this.wizard1.ResumeLayout(false);
			this.wizardPageCreateNewOrChooseExisting.ResumeLayout(false);
			this.wizardPageCreateNewOrChooseExisting.PerformLayout();
			this.wizardPageCreateNew.ResumeLayout(false);
			this.wizardPageChooseExisting.ResumeLayout(false);
			this.wizardPageMapExisting.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPropertyMappings)).EndInit();
			this.wizardPageFinish.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Wizard wizard1;
		private DevComponents.DotNetBar.WizardPage wizardPageCreateNewOrChooseExisting;
		private DevComponents.DotNetBar.WizardPage wizardPageCreateNew;
		private DevComponents.DotNetBar.LabelX labelX1;
		private System.Windows.Forms.RadioButton radioButtonCreateNewComponent;
		private DevComponents.DotNetBar.LabelX labelX2;
		private System.Windows.Forms.RadioButton radioButtonUseExisting;
		private DevComponents.DotNetBar.WizardPage wizardPageChooseExisting;
		private DevComponents.DotNetBar.WizardPage wizardPageMapExisting;
		private DevComponents.DotNetBar.WizardPage wizardPageFinish;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.LabelX labelX5;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxNewComponentName;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxNewComponentDefName;
		private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxCreateNewCompDeleteProperties;
		private DevComponents.DotNetBar.LabelX labelX6;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxExistingComponentDefs;
		private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewPropertyMappings;
		private DevComponents.DotNetBar.LabelX labelX7;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxUseExistingNameOfComponent;
		private DevComponents.DotNetBar.LabelX labelFinishedChanges;
		private System.Windows.Forms.DataGridViewTextBoxColumn ComponentPropertyName;
		private System.Windows.Forms.DataGridViewComboBoxColumn ExistingKeyProperty;
		private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxUseExistingDeleteExistingProperties;
		private DevComponents.DotNetBar.LabelX labelX8;
	}
}