namespace ArchAngel.Providers.Database.Controls
{
    partial class FormFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilter));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkBoxCreateStoredProcedure = new System.Windows.Forms.CheckBox();
            this.checkBoxUseCustomWhereClause = new System.Windows.Forms.CheckBox();
            this.radioButtonReturnCollection = new System.Windows.Forms.RadioButton();
            this.radioButtonReturnSpecificItem = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCustomWhereClause = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxScriptObject = new System.Windows.Forms.ComboBox();
            this.comboBoxColumn = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFilterColumnAlias = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxCompareOperator = new System.Windows.Forms.ComboBox();
            this.buttonAddFilter = new System.Windows.Forms.Button();
            this.buttonAddOrder = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonFilterOr = new System.Windows.Forms.RadioButton();
            this.radioButtonFilterAnd = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonOrderDescending = new System.Windows.Forms.RadioButton();
            this.radioButtonOrderAscending = new System.Windows.Forms.RadioButton();
            this.listViewOrderByColumn = new System.Windows.Forms.ListView();
            this.columnHeaderOrderByScriptObjectName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderOrderByColumnName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderOrderBySortDirection = new System.Windows.Forms.ColumnHeader();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listViewColumn = new System.Windows.Forms.ListView();
            this.columnHeaderColumnOperator = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumnScriptObjectName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumnColumnName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumnAlias = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumnCompareOperator = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemFilterDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabStripFilter = new ActiproSoftware.UIStudio.TabStrip.TabStrip();
            this.tabStripPageColumn = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.tabStripPageOrderByColumn = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.tabStripPageCustomWhereClause = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStripFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabStripFilter)).BeginInit();
            this.tabStripFilter.SuspendLayout();
            this.tabStripPageColumn.SuspendLayout();
            this.tabStripPageOrderByColumn.SuspendLayout();
            this.tabStripPageCustomWhereClause.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 463);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(186, 463);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlias.Location = new System.Drawing.Point(49, 103);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(277, 20);
            this.textBoxAlias.TabIndex = 6;
            this.textBoxAlias.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAlias_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(10, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Alias";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(49, 77);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(277, 20);
            this.textBoxName.TabIndex = 4;
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(10, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // checkBoxCreateStoredProcedure
            // 
            this.checkBoxCreateStoredProcedure.AutoSize = true;
            this.checkBoxCreateStoredProcedure.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxCreateStoredProcedure.Checked = true;
            this.checkBoxCreateStoredProcedure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateStoredProcedure.Location = new System.Drawing.Point(12, 12);
            this.checkBoxCreateStoredProcedure.Name = "checkBoxCreateStoredProcedure";
            this.checkBoxCreateStoredProcedure.Size = new System.Drawing.Size(143, 17);
            this.checkBoxCreateStoredProcedure.TabIndex = 0;
            this.checkBoxCreateStoredProcedure.Text = "Create Stored Procedure";
            this.checkBoxCreateStoredProcedure.UseVisualStyleBackColor = false;
            // 
            // checkBoxUseCustomWhereClause
            // 
            this.checkBoxUseCustomWhereClause.AutoSize = true;
            this.checkBoxUseCustomWhereClause.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxUseCustomWhereClause.Location = new System.Drawing.Point(173, 12);
            this.checkBoxUseCustomWhereClause.Name = "checkBoxUseCustomWhereClause";
            this.checkBoxUseCustomWhereClause.Size = new System.Drawing.Size(153, 17);
            this.checkBoxUseCustomWhereClause.TabIndex = 1;
            this.checkBoxUseCustomWhereClause.Text = "Use Custom Where Clause";
            this.checkBoxUseCustomWhereClause.UseVisualStyleBackColor = false;
            this.checkBoxUseCustomWhereClause.CheckedChanged += new System.EventHandler(this.checkBoxUseCustomWhereClause_CheckedChanged);
            // 
            // radioButtonReturnCollection
            // 
            this.radioButtonReturnCollection.AutoSize = true;
            this.radioButtonReturnCollection.Checked = true;
            this.radioButtonReturnCollection.Location = new System.Drawing.Point(6, 9);
            this.radioButtonReturnCollection.Name = "radioButtonReturnCollection";
            this.radioButtonReturnCollection.Size = new System.Drawing.Size(111, 17);
            this.radioButtonReturnCollection.TabIndex = 0;
            this.radioButtonReturnCollection.TabStop = true;
            this.radioButtonReturnCollection.Text = "Returns Collection";
            this.radioButtonReturnCollection.UseVisualStyleBackColor = true;
            this.radioButtonReturnCollection.CheckedChanged += new System.EventHandler(this.radioButtonReturnCollection_CheckedChanged);
            // 
            // radioButtonReturnSpecificItem
            // 
            this.radioButtonReturnSpecificItem.AutoSize = true;
            this.radioButtonReturnSpecificItem.Location = new System.Drawing.Point(168, 9);
            this.radioButtonReturnSpecificItem.Name = "radioButtonReturnSpecificItem";
            this.radioButtonReturnSpecificItem.Size = new System.Drawing.Size(121, 17);
            this.radioButtonReturnSpecificItem.TabIndex = 1;
            this.radioButtonReturnSpecificItem.Text = "Return Specific Item";
            this.radioButtonReturnSpecificItem.UseVisualStyleBackColor = true;
            this.radioButtonReturnSpecificItem.CheckedChanged += new System.EventHandler(this.radioButtonReturnSpecificItem_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.radioButtonReturnCollection);
            this.groupBox1.Controls.Add(this.radioButtonReturnSpecificItem);
            this.groupBox1.Location = new System.Drawing.Point(5, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 31);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textBoxCustomWhereClause
            // 
            this.textBoxCustomWhereClause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCustomWhereClause.Location = new System.Drawing.Point(0, 0);
            this.textBoxCustomWhereClause.Multiline = true;
            this.textBoxCustomWhereClause.Name = "textBoxCustomWhereClause";
            this.textBoxCustomWhereClause.Size = new System.Drawing.Size(354, 220);
            this.textBoxCustomWhereClause.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(10, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Script\r\nObject";
            // 
            // comboBoxScriptObject
            // 
            this.comboBoxScriptObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxScriptObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScriptObject.FormattingEnabled = true;
            this.comboBoxScriptObject.Location = new System.Drawing.Point(49, 153);
            this.comboBoxScriptObject.Name = "comboBoxScriptObject";
            this.comboBoxScriptObject.Size = new System.Drawing.Size(292, 21);
            this.comboBoxScriptObject.TabIndex = 8;
            this.comboBoxScriptObject.SelectedIndexChanged += new System.EventHandler(this.comboBoxScriptObject_SelectedIndexChanged);
            // 
            // comboBoxColumn
            // 
            this.comboBoxColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.Location = new System.Drawing.Point(49, 180);
            this.comboBoxColumn.Name = "comboBoxColumn";
            this.comboBoxColumn.Size = new System.Drawing.Size(292, 21);
            this.comboBoxColumn.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(10, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Column";
            // 
            // textBoxFilterColumnAlias
            // 
            this.textBoxFilterColumnAlias.Location = new System.Drawing.Point(14, 24);
            this.textBoxFilterColumnAlias.Name = "textBoxFilterColumnAlias";
            this.textBoxFilterColumnAlias.Size = new System.Drawing.Size(123, 20);
            this.textBoxFilterColumnAlias.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(11, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Column Alias";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(140, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Compare Operator";
            // 
            // comboBoxCompareOperator
            // 
            this.comboBoxCompareOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompareOperator.FormattingEnabled = true;
            this.comboBoxCompareOperator.Location = new System.Drawing.Point(143, 24);
            this.comboBoxCompareOperator.Name = "comboBoxCompareOperator";
            this.comboBoxCompareOperator.Size = new System.Drawing.Size(90, 21);
            this.comboBoxCompareOperator.TabIndex = 3;
            // 
            // buttonAddFilter
            // 
            this.buttonAddFilter.Location = new System.Drawing.Point(251, 51);
            this.buttonAddFilter.Name = "buttonAddFilter";
            this.buttonAddFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFilter.TabIndex = 5;
            this.buttonAddFilter.Text = "Add &Filter";
            this.buttonAddFilter.UseVisualStyleBackColor = true;
            this.buttonAddFilter.Click += new System.EventHandler(this.buttonAddFilter_Click);
            // 
            // buttonAddOrder
            // 
            this.buttonAddOrder.Location = new System.Drawing.Point(251, 51);
            this.buttonAddOrder.Name = "buttonAddOrder";
            this.buttonAddOrder.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOrder.TabIndex = 1;
            this.buttonAddOrder.Text = "Add &Order";
            this.buttonAddOrder.UseVisualStyleBackColor = true;
            this.buttonAddOrder.Click += new System.EventHandler(this.buttonAddOrder_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.radioButtonFilterOr);
            this.groupBox2.Controls.Add(this.radioButtonFilterAnd);
            this.groupBox2.Location = new System.Drawing.Point(239, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(90, 29);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // radioButtonFilterOr
            // 
            this.radioButtonFilterOr.AutoSize = true;
            this.radioButtonFilterOr.Enabled = false;
            this.radioButtonFilterOr.Location = new System.Drawing.Point(51, 9);
            this.radioButtonFilterOr.Name = "radioButtonFilterOr";
            this.radioButtonFilterOr.Size = new System.Drawing.Size(36, 17);
            this.radioButtonFilterOr.TabIndex = 1;
            this.radioButtonFilterOr.Text = "Or";
            this.radioButtonFilterOr.UseVisualStyleBackColor = true;
            // 
            // radioButtonFilterAnd
            // 
            this.radioButtonFilterAnd.AutoSize = true;
            this.radioButtonFilterAnd.Enabled = false;
            this.radioButtonFilterAnd.Location = new System.Drawing.Point(6, 9);
            this.radioButtonFilterAnd.Name = "radioButtonFilterAnd";
            this.radioButtonFilterAnd.Size = new System.Drawing.Size(44, 17);
            this.radioButtonFilterAnd.TabIndex = 0;
            this.radioButtonFilterAnd.Text = "And";
            this.radioButtonFilterAnd.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.radioButtonOrderDescending);
            this.groupBox3.Controls.Add(this.radioButtonOrderAscending);
            this.groupBox3.Location = new System.Drawing.Point(220, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(106, 29);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // radioButtonOrderDescending
            // 
            this.radioButtonOrderDescending.AutoSize = true;
            this.radioButtonOrderDescending.Location = new System.Drawing.Point(55, 9);
            this.radioButtonOrderDescending.Name = "radioButtonOrderDescending";
            this.radioButtonOrderDescending.Size = new System.Drawing.Size(50, 17);
            this.radioButtonOrderDescending.TabIndex = 1;
            this.radioButtonOrderDescending.Text = "Desc";
            this.radioButtonOrderDescending.UseVisualStyleBackColor = true;
            // 
            // radioButtonOrderAscending
            // 
            this.radioButtonOrderAscending.AutoSize = true;
            this.radioButtonOrderAscending.Checked = true;
            this.radioButtonOrderAscending.Location = new System.Drawing.Point(10, 9);
            this.radioButtonOrderAscending.Name = "radioButtonOrderAscending";
            this.radioButtonOrderAscending.Size = new System.Drawing.Size(43, 17);
            this.radioButtonOrderAscending.TabIndex = 0;
            this.radioButtonOrderAscending.TabStop = true;
            this.radioButtonOrderAscending.Text = "Asc";
            this.radioButtonOrderAscending.UseVisualStyleBackColor = true;
            // 
            // listViewOrderByColumn
            // 
            this.listViewOrderByColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewOrderByColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderOrderByScriptObjectName,
            this.columnHeaderOrderByColumnName,
            this.columnHeaderOrderBySortDirection});
            this.listViewOrderByColumn.FullRowSelect = true;
            this.listViewOrderByColumn.Location = new System.Drawing.Point(0, 80);
            this.listViewOrderByColumn.Name = "listViewOrderByColumn";
            this.listViewOrderByColumn.Size = new System.Drawing.Size(354, 140);
            this.listViewOrderByColumn.TabIndex = 3;
            this.listViewOrderByColumn.UseCompatibleStateImageBehavior = false;
            this.listViewOrderByColumn.View = System.Windows.Forms.View.Details;
            this.listViewOrderByColumn.Resize += new System.EventHandler(this.listViewOrderByColumn_Resize);
            this.listViewOrderByColumn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewOrderBy_MouseUp);
            // 
            // columnHeaderOrderByScriptObjectName
            // 
            this.columnHeaderOrderByScriptObjectName.Text = "Object";
            // 
            // columnHeaderOrderByColumnName
            // 
            this.columnHeaderOrderByColumnName.Text = "Column";
            // 
            // columnHeaderOrderBySortDirection
            // 
            this.columnHeaderOrderBySortDirection.Text = "Sort";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(3, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Columns";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(3, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Order By";
            // 
            // listViewColumn
            // 
            this.listViewColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderColumnOperator,
            this.columnHeaderColumnScriptObjectName,
            this.columnHeaderColumnColumnName,
            this.columnHeaderColumnAlias,
            this.columnHeaderColumnCompareOperator});
            this.listViewColumn.FullRowSelect = true;
            this.listViewColumn.Location = new System.Drawing.Point(14, 80);
            this.listViewColumn.Name = "listViewColumn";
            this.listViewColumn.Size = new System.Drawing.Size(328, 140);
            this.listViewColumn.TabIndex = 7;
            this.listViewColumn.UseCompatibleStateImageBehavior = false;
            this.listViewColumn.View = System.Windows.Forms.View.Details;
            this.listViewColumn.Resize += new System.EventHandler(this.listViewColumn_Resize);
            this.listViewColumn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewColumn_MouseUp);
            // 
            // columnHeaderColumnOperator
            // 
            this.columnHeaderColumnOperator.Text = "Operator";
            // 
            // columnHeaderColumnScriptObjectName
            // 
            this.columnHeaderColumnScriptObjectName.Text = "Object";
            // 
            // columnHeaderColumnColumnName
            // 
            this.columnHeaderColumnColumnName.Text = "Column";
            // 
            // columnHeaderColumnAlias
            // 
            this.columnHeaderColumnAlias.Text = "Alias";
            // 
            // columnHeaderColumnCompareOperator
            // 
            this.columnHeaderColumnCompareOperator.Text = "C/OP";
            // 
            // contextMenuStripFilter
            // 
            this.contextMenuStripFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFilterDelete});
            this.contextMenuStripFilter.Name = "contextMenuStripFilter";
            this.contextMenuStripFilter.Size = new System.Drawing.Size(117, 26);
            // 
            // toolStripMenuItemFilterDelete
            // 
            this.toolStripMenuItemFilterDelete.Name = "toolStripMenuItemFilterDelete";
            this.toolStripMenuItemFilterDelete.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemFilterDelete.Text = "&Delete";
            this.toolStripMenuItemFilterDelete.Click += new System.EventHandler(this.toolStripMenuItemFilterDelete_Click);
            // 
            // tabStripFilter
            // 
            this.tabStripFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabStripFilter.AutoSetFocusOnClick = true;
            this.tabStripFilter.Controls.Add(this.tabStripPageColumn);
            this.tabStripFilter.Controls.Add(this.tabStripPageOrderByColumn);
            this.tabStripFilter.Controls.Add(this.tabStripPageCustomWhereClause);
            this.tabStripFilter.Location = new System.Drawing.Point(-1, 216);
            this.tabStripFilter.Name = "tabStripFilter";
            this.tabStripFilter.Size = new System.Drawing.Size(354, 241);
            this.tabStripFilter.TabIndex = 11;
            this.tabStripFilter.Text = "tabStripFilter";
            // 
            // tabStripPageColumn
            // 
            this.tabStripPageColumn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageColumn.Controls.Add(this.buttonAddFilter);
            this.tabStripPageColumn.Controls.Add(this.groupBox2);
            this.tabStripPageColumn.Controls.Add(this.comboBoxCompareOperator);
            this.tabStripPageColumn.Controls.Add(this.listViewColumn);
            this.tabStripPageColumn.Controls.Add(this.label7);
            this.tabStripPageColumn.Controls.Add(this.textBoxFilterColumnAlias);
            this.tabStripPageColumn.Controls.Add(this.label8);
            this.tabStripPageColumn.Controls.Add(this.label6);
            this.tabStripPageColumn.Key = "TabStripPage";
            this.tabStripPageColumn.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageColumn.Name = "tabStripPageColumn";
            this.tabStripPageColumn.Size = new System.Drawing.Size(354, 220);
            this.tabStripPageColumn.TabIndex = 0;
            this.tabStripPageColumn.Text = "Columns";
            // 
            // tabStripPageOrderByColumn
            // 
            this.tabStripPageOrderByColumn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageOrderByColumn.Controls.Add(this.buttonAddOrder);
            this.tabStripPageOrderByColumn.Controls.Add(this.groupBox3);
            this.tabStripPageOrderByColumn.Controls.Add(this.label9);
            this.tabStripPageOrderByColumn.Controls.Add(this.listViewOrderByColumn);
            this.tabStripPageOrderByColumn.Key = "TabStripPage";
            this.tabStripPageOrderByColumn.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageOrderByColumn.Name = "tabStripPageOrderByColumn";
            this.tabStripPageOrderByColumn.Size = new System.Drawing.Size(354, 220);
            this.tabStripPageOrderByColumn.TabIndex = 1;
            this.tabStripPageOrderByColumn.Text = "Order By";
            // 
            // tabStripPageCustomWhereClause
            // 
            this.tabStripPageCustomWhereClause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageCustomWhereClause.Controls.Add(this.textBoxCustomWhereClause);
            this.tabStripPageCustomWhereClause.Key = "TabStripPage";
            this.tabStripPageCustomWhereClause.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageCustomWhereClause.Name = "tabStripPageCustomWhereClause";
            this.tabStripPageCustomWhereClause.Size = new System.Drawing.Size(354, 220);
            this.tabStripPageCustomWhereClause.TabIndex = 2;
            this.tabStripPageCustomWhereClause.Text = "Custom Where Clause";
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 453);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(353, 39);
            this.ucHeading1.TabIndex = 14;
            this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(48, 127);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(277, 20);
            this.textBoxDescription.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Desc.";
            // 
            // FormFilter
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(353, 492);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabStripFilter);
            this.Controls.Add(this.comboBoxScriptObject);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxUseCustomWhereClause);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxCreateStoredProcedure);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFilter";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.FormFilter_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFilter_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.contextMenuStripFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabStripFilter)).EndInit();
            this.tabStripFilter.ResumeLayout(false);
            this.tabStripPageColumn.ResumeLayout(false);
            this.tabStripPageColumn.PerformLayout();
            this.tabStripPageOrderByColumn.ResumeLayout(false);
            this.tabStripPageOrderByColumn.PerformLayout();
            this.tabStripPageCustomWhereClause.ResumeLayout(false);
            this.tabStripPageCustomWhereClause.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox checkBoxUseCustomWhereClause;
        private System.Windows.Forms.CheckBox checkBoxCreateStoredProcedure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonReturnCollection;
        private System.Windows.Forms.RadioButton radioButtonReturnSpecificItem;
        private System.Windows.Forms.TextBox textBoxCustomWhereClause;
        private System.Windows.Forms.ComboBox comboBoxScriptObject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCompareOperator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxFilterColumnAlias;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxColumn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAddOrder;
        private System.Windows.Forms.Button buttonAddFilter;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonOrderDescending;
        private System.Windows.Forms.RadioButton radioButtonOrderAscending;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonFilterOr;
        private System.Windows.Forms.RadioButton radioButtonFilterAnd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView listViewColumn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listViewOrderByColumn;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnOperator;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnColumnName;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnScriptObjectName;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnCompareOperator;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnAlias;
        private System.Windows.Forms.ColumnHeader columnHeaderOrderByColumnName;
        private System.Windows.Forms.ColumnHeader columnHeaderOrderByScriptObjectName;
        private System.Windows.Forms.ColumnHeader columnHeaderOrderBySortDirection;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFilter;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFilterDelete;
        private ActiproSoftware.UIStudio.TabStrip.TabStrip tabStripFilter;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageColumn;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageOrderByColumn;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageCustomWhereClause;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label3;
    }
}