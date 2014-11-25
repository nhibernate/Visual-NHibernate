namespace ArchAngel.Providers.Database.Controls
{
    partial class FormColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormColumn));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBoxDataType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.textBoxName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxAlias = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxAliasDisplay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.lblPrecision = new DevComponents.DotNetBar.LabelX();
            this.lblScale = new DevComponents.DotNetBar.LabelX();
            this.textBoxDefault = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxOrdinalPosition = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.numEditCharMaxLength = new DevComponents.Editors.IntegerInput();
            this.numEditPrecision = new DevComponents.Editors.IntegerInput();
            this.numEditScale = new DevComponents.Editors.IntegerInput();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.textBoxDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.checkBoxIsNullable = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.expandablePanelDetails = new DevComponents.DotNetBar.ExpandablePanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.ddlLookups = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEditCharMaxLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditScale)).BeginInit();
            this.expandablePanelDetails.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(428, 385);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(347, 385);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 19;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.DisplayMember = "Text";
            this.comboBoxDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDataType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.ItemHeight = 14;
            this.comboBoxDataType.Location = new System.Drawing.Point(112, 150);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(368, 20);
            this.comboBoxDataType.TabIndex = 30;
            this.comboBoxDataType.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxDataType_Validating);
            this.comboBoxDataType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataType_SelectedIndexChanged);
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(74, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(32, 15);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "Name";
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX6.AutoSize = true;
            this.labelX6.Location = new System.Drawing.Point(42, 75);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(64, 15);
            this.labelX6.TabIndex = 33;
            this.labelX6.Text = "Alias display";
            // 
            // labelX7
            // 
            this.labelX7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX7.AutoSize = true;
            this.labelX7.Location = new System.Drawing.Point(80, 49);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(26, 15);
            this.labelX7.TabIndex = 34;
            this.labelX7.Text = "Alias";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelX13, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelX1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDataType, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelX6, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelX7, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAlias, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAliasDisplay, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelX8, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelX9, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelX2, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelX10, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblPrecision, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblScale, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDefault, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBoxOrdinalPosition, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.numEditCharMaxLength, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.numEditPrecision, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.numEditScale, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.labelX11, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDescription, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxIsNullable, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelX3, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.ddlLookups, 2, 12);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(503, 329);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // labelX13
            // 
            this.labelX13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX13.AutoSize = true;
            this.labelX13.Location = new System.Drawing.Point(54, 121);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(52, 15);
            this.labelX13.TabIndex = 49;
            this.labelX13.Text = "Is nullable";
            // 
            // textBoxName
            // 
            // 
            // 
            // 
            this.textBoxName.Border.Class = "TextBoxBorder";
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(112, 23);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(368, 20);
            this.textBoxName.TabIndex = 35;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxAlias
            // 
            // 
            // 
            // 
            this.textBoxAlias.Border.Class = "TextBoxBorder";
            this.textBoxAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAlias.Location = new System.Drawing.Point(112, 49);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(368, 20);
            this.textBoxAlias.TabIndex = 36;
            this.textBoxAlias.TextChanged += new System.EventHandler(this.textBoxAlias_TextChanged);
            // 
            // textBoxAliasDisplay
            // 
            // 
            // 
            // 
            this.textBoxAliasDisplay.Border.Class = "TextBoxBorder";
            this.textBoxAliasDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAliasDisplay.Location = new System.Drawing.Point(112, 75);
            this.textBoxAliasDisplay.Name = "textBoxAliasDisplay";
            this.textBoxAliasDisplay.Size = new System.Drawing.Size(368, 20);
            this.textBoxAliasDisplay.TabIndex = 37;
            this.textBoxAliasDisplay.TextChanged += new System.EventHandler(this.textBoxAliasDisplay_TextChanged);
            // 
            // labelX8
            // 
            this.labelX8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX8.AutoSize = true;
            this.labelX8.Location = new System.Drawing.Point(56, 150);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(50, 15);
            this.labelX8.TabIndex = 38;
            this.labelX8.Text = "Data-type";
            // 
            // labelX9
            // 
            this.labelX9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX9.AutoSize = true;
            this.labelX9.Location = new System.Drawing.Point(39, 176);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(67, 15);
            this.labelX9.TabIndex = 39;
            this.labelX9.Text = "Default value";
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.AutoSize = true;
            this.labelX2.Location = new System.Drawing.Point(27, 202);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(79, 15);
            this.labelX2.TabIndex = 40;
            this.labelX2.Text = "Ordinal position";
            // 
            // labelX10
            // 
            this.labelX10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX10.AutoSize = true;
            this.labelX10.Location = new System.Drawing.Point(23, 228);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(83, 15);
            this.labelX10.TabIndex = 41;
            this.labelX10.Text = "Char max length";
            // 
            // lblPrecision
            // 
            this.lblPrecision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrecision.AutoSize = true;
            this.lblPrecision.Location = new System.Drawing.Point(58, 254);
            this.lblPrecision.Name = "lblPrecision";
            this.lblPrecision.Size = new System.Drawing.Size(48, 15);
            this.lblPrecision.TabIndex = 42;
            this.lblPrecision.Text = "Precision";
            // 
            // lblScale
            // 
            this.lblScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(76, 280);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(30, 15);
            this.lblScale.TabIndex = 43;
            this.lblScale.Text = "Scale";
            // 
            // textBoxDefault
            // 
            this.textBoxDefault.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.textBoxDefault.Border.Class = "TextBoxBorder";
            this.textBoxDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDefault.Location = new System.Drawing.Point(112, 176);
            this.textBoxDefault.Name = "textBoxDefault";
            this.textBoxDefault.Size = new System.Drawing.Size(368, 20);
            this.textBoxDefault.TabIndex = 44;
            // 
            // textBoxOrdinalPosition
            // 
            this.textBoxOrdinalPosition.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.textBoxOrdinalPosition.Border.Class = "TextBoxBorder";
            this.textBoxOrdinalPosition.Location = new System.Drawing.Point(112, 202);
            this.textBoxOrdinalPosition.Name = "textBoxOrdinalPosition";
            this.textBoxOrdinalPosition.Size = new System.Drawing.Size(53, 20);
            this.textBoxOrdinalPosition.TabIndex = 45;
            // 
            // numEditCharMaxLength
            // 
            // 
            // 
            // 
            this.numEditCharMaxLength.BackgroundStyle.Class = "DateTimeInputBackground";
            this.numEditCharMaxLength.Location = new System.Drawing.Point(112, 228);
            this.numEditCharMaxLength.Name = "numEditCharMaxLength";
            this.numEditCharMaxLength.ShowUpDown = true;
            this.numEditCharMaxLength.Size = new System.Drawing.Size(53, 20);
            this.numEditCharMaxLength.TabIndex = 46;
            // 
            // numEditPrecision
            // 
            // 
            // 
            // 
            this.numEditPrecision.BackgroundStyle.Class = "DateTimeInputBackground";
            this.numEditPrecision.Location = new System.Drawing.Point(112, 254);
            this.numEditPrecision.Name = "numEditPrecision";
            this.numEditPrecision.ShowUpDown = true;
            this.numEditPrecision.Size = new System.Drawing.Size(53, 20);
            this.numEditPrecision.TabIndex = 47;
            // 
            // numEditScale
            // 
            // 
            // 
            // 
            this.numEditScale.BackgroundStyle.Class = "DateTimeInputBackground";
            this.numEditScale.Location = new System.Drawing.Point(112, 280);
            this.numEditScale.Name = "numEditScale";
            this.numEditScale.ShowUpDown = true;
            this.numEditScale.Size = new System.Drawing.Size(53, 20);
            this.numEditScale.TabIndex = 48;
            // 
            // labelX11
            // 
            this.labelX11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX11.AutoSize = true;
            this.labelX11.Location = new System.Drawing.Point(48, 101);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(58, 15);
            this.labelX11.TabIndex = 50;
            this.labelX11.Text = "Description";
            // 
            // textBoxDescription
            // 
            // 
            // 
            // 
            this.textBoxDescription.Border.Class = "TextBoxBorder";
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Location = new System.Drawing.Point(112, 101);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(368, 20);
            this.textBoxDescription.TabIndex = 51;
            // 
            // checkBoxIsNullable
            // 
            this.checkBoxIsNullable.Location = new System.Drawing.Point(112, 121);
            this.checkBoxIsNullable.Name = "checkBoxIsNullable";
            this.checkBoxIsNullable.Size = new System.Drawing.Size(75, 23);
            this.checkBoxIsNullable.TabIndex = 52;
            // 
            // expandablePanelDetails
            // 
            this.expandablePanelDetails.AnimationTime = 0;
            this.expandablePanelDetails.AutoSize = true;
            this.expandablePanelDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.expandablePanelDetails.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanelDetails.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanelDetails.Controls.Add(this.tableLayoutPanel1);
            this.expandablePanelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanelDetails.ExpandOnTitleClick = true;
            this.expandablePanelDetails.Location = new System.Drawing.Point(3, 3);
            this.expandablePanelDetails.Name = "expandablePanelDetails";
            this.expandablePanelDetails.Size = new System.Drawing.Size(503, 355);
            this.expandablePanelDetails.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanelDetails.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelDetails.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelDetails.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanelDetails.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanelDetails.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanelDetails.Style.GradientAngle = 90;
            this.expandablePanelDetails.TabIndex = 36;
            this.expandablePanelDetails.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanelDetails.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanelDetails.TitleStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("expandablePanelDetails.TitleStyle.BackgroundImage")));
            this.expandablePanelDetails.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.CenterLeft;
            this.expandablePanelDetails.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanelDetails.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanelDetails.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expandablePanelDetails.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanelDetails.TitleStyle.GradientAngle = 90;
            this.expandablePanelDetails.TitleStyle.MarginLeft = 30;
            this.expandablePanelDetails.TitleText = "Details";
            // 
            // panelEx1
            // 
            this.panelEx1.AutoScroll = true;
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.tableLayoutPanel2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(509, 414);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 37;
            this.panelEx1.Text = "panelEx1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScrollMargin = new System.Drawing.Size(15, 0);
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.expandablePanelDetails, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(509, 414);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 379);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(509, 35);
            this.ucHeading1.TabIndex = 17;
            this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX3.AutoSize = true;
            this.labelX3.Location = new System.Drawing.Point(33, 306);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(73, 15);
            this.labelX3.TabIndex = 53;
            this.labelX3.Text = "Lookup values";
            // 
            // ddlLookups
            // 
            this.ddlLookups.DisplayMember = "Text";
            this.ddlLookups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlLookups.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlLookups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLookups.FormattingEnabled = true;
            this.ddlLookups.ItemHeight = 14;
            this.ddlLookups.Location = new System.Drawing.Point(112, 306);
            this.ddlLookups.Name = "ddlLookups";
            this.ddlLookups.Size = new System.Drawing.Size(368, 20);
            this.ddlLookups.TabIndex = 54;
            // 
            // FormColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(509, 414);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.ucHeading1);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormColumn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Column";
            this.Load += new System.EventHandler(this.FormColumn_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormColumn_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEditCharMaxLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEditScale)).EndInit();
            this.expandablePanelDetails.ResumeLayout(false);
            this.expandablePanelDetails.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxDataType;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxName;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxAlias;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxAliasDisplay;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX lblPrecision;
        private DevComponents.DotNetBar.LabelX lblScale;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxDefault;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxOrdinalPosition;
        private DevComponents.Editors.IntegerInput numEditCharMaxLength;
        private DevComponents.Editors.IntegerInput numEditPrecision;
        private DevComponents.Editors.IntegerInput numEditScale;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanelDetails;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxDescription;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxIsNullable;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ddlLookups;
    }
}