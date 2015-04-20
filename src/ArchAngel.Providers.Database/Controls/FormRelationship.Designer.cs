namespace ArchAngel.Providers.Database.Controls
{
    partial class FormRelationship
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelationship));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.radioButtonManyToOne = new System.Windows.Forms.RadioButton();
            this.radioButtonManyToMany = new System.Windows.Forms.RadioButton();
            this.radioButtonOneToMany = new System.Windows.Forms.RadioButton();
            this.radioButtonOneToOne = new System.Windows.Forms.RadioButton();
            this.comboBoxForeignColumn = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listViewPrimaryColumn = new System.Windows.Forms.ListView();
            this.columnHeaderColumnColumnName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumnAlias = new System.Windows.Forms.ColumnHeader();
            this.listViewForeignColumn = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxForeignScriptObject = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.contextMenuStripRelationship = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemRelationshipDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonForeignColumnAdd = new System.Windows.Forms.Button();
            this.tabStripRelationship = new ActiproSoftware.UIStudio.TabStrip.TabStrip();
            this.tabStripPagePrimaryTable = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.buttonAddPrimaryFilter = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxPrimaryFilter = new System.Windows.Forms.ComboBox();
            this.comboBoxPrimaryScriptObject = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonPrimaryColumnAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxPrimaryColumn = new System.Windows.Forms.ComboBox();
            this.tabStripPageRelationships = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.buttonAddIntermediatePrimaryRelationship = new System.Windows.Forms.Button();
            this.buttonAddIntermediateForeignRelationship = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxIntermediateForeignRelationship = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxIntermediatePrimaryRelationship = new System.Windows.Forms.ComboBox();
            this.tabStripPageForeignTable = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.buttonAddForeignFilter = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxForeignFilter = new System.Windows.Forms.ComboBox();
            this.checkBoxIsBase = new System.Windows.Forms.CheckBox();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBoxType.SuspendLayout();
            this.contextMenuStripRelationship.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabStripRelationship)).BeginInit();
            this.tabStripRelationship.SuspendLayout();
            this.tabStripPagePrimaryTable.SuspendLayout();
            this.tabStripPageRelationships.SuspendLayout();
            this.tabStripPageForeignTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(289, 468);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(208, 468);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 9;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlias.Location = new System.Drawing.Point(53, 32);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(311, 20);
            this.textBoxAlias.TabIndex = 3;
            this.textBoxAlias.TextChanged += new System.EventHandler(this.textBoxAlias_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alias";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(53, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(311, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(53, 58);
            this.textBoxPath.Multiline = true;
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(311, 51);
            this.textBoxPath.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Path";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBoxType
            // 
            this.groupBoxType.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxType.Controls.Add(this.radioButtonManyToOne);
            this.groupBoxType.Controls.Add(this.radioButtonManyToMany);
            this.groupBoxType.Controls.Add(this.radioButtonOneToMany);
            this.groupBoxType.Controls.Add(this.radioButtonOneToOne);
            this.groupBoxType.Location = new System.Drawing.Point(53, 149);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(206, 71);
            this.groupBoxType.TabIndex = 6;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "Type";
            // 
            // radioButtonManyToOne
            // 
            this.radioButtonManyToOne.AutoSize = true;
            this.radioButtonManyToOne.Location = new System.Drawing.Point(6, 42);
            this.radioButtonManyToOne.Name = "radioButtonManyToOne";
            this.radioButtonManyToOne.Size = new System.Drawing.Size(86, 17);
            this.radioButtonManyToOne.TabIndex = 2;
            this.radioButtonManyToOne.TabStop = true;
            this.radioButtonManyToOne.Text = "Many to One";
            this.radioButtonManyToOne.UseVisualStyleBackColor = true;
            // 
            // radioButtonManyToMany
            // 
            this.radioButtonManyToMany.AutoSize = true;
            this.radioButtonManyToMany.Location = new System.Drawing.Point(98, 42);
            this.radioButtonManyToMany.Name = "radioButtonManyToMany";
            this.radioButtonManyToMany.Size = new System.Drawing.Size(92, 17);
            this.radioButtonManyToMany.TabIndex = 3;
            this.radioButtonManyToMany.TabStop = true;
            this.radioButtonManyToMany.Text = "Many to Many";
            this.radioButtonManyToMany.UseVisualStyleBackColor = true;
            // 
            // radioButtonOneToMany
            // 
            this.radioButtonOneToMany.AutoSize = true;
            this.radioButtonOneToMany.Location = new System.Drawing.Point(98, 19);
            this.radioButtonOneToMany.Name = "radioButtonOneToMany";
            this.radioButtonOneToMany.Size = new System.Drawing.Size(86, 17);
            this.radioButtonOneToMany.TabIndex = 1;
            this.radioButtonOneToMany.TabStop = true;
            this.radioButtonOneToMany.Text = "One to Many";
            this.radioButtonOneToMany.UseVisualStyleBackColor = true;
            // 
            // radioButtonOneToOne
            // 
            this.radioButtonOneToOne.AutoSize = true;
            this.radioButtonOneToOne.Location = new System.Drawing.Point(6, 19);
            this.radioButtonOneToOne.Name = "radioButtonOneToOne";
            this.radioButtonOneToOne.Size = new System.Drawing.Size(80, 17);
            this.radioButtonOneToOne.TabIndex = 0;
            this.radioButtonOneToOne.TabStop = true;
            this.radioButtonOneToOne.Text = "One to One";
            this.radioButtonOneToOne.UseVisualStyleBackColor = true;
            // 
            // comboBoxForeignColumn
            // 
            this.comboBoxForeignColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxForeignColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignColumn.FormattingEnabled = true;
            this.comboBoxForeignColumn.Location = new System.Drawing.Point(69, 30);
            this.comboBoxForeignColumn.Name = "comboBoxForeignColumn";
            this.comboBoxForeignColumn.Size = new System.Drawing.Size(266, 21);
            this.comboBoxForeignColumn.Sorted = true;
            this.comboBoxForeignColumn.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(0, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Column";
            // 
            // listViewPrimaryColumn
            // 
            this.listViewPrimaryColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPrimaryColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderColumnColumnName,
            this.columnHeaderColumnAlias});
            this.listViewPrimaryColumn.FullRowSelect = true;
            this.listViewPrimaryColumn.Location = new System.Drawing.Point(69, 57);
            this.listViewPrimaryColumn.Name = "listViewPrimaryColumn";
            this.listViewPrimaryColumn.Size = new System.Drawing.Size(296, 128);
            this.listViewPrimaryColumn.TabIndex = 6;
            this.listViewPrimaryColumn.UseCompatibleStateImageBehavior = false;
            this.listViewPrimaryColumn.View = System.Windows.Forms.View.Details;
            this.listViewPrimaryColumn.Validating += new System.ComponentModel.CancelEventHandler(this.listViewPrimaryColumn_Validating);
            this.listViewPrimaryColumn.Resize += new System.EventHandler(this.listViewPrimaryColumn_Resize);
            this.listViewPrimaryColumn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewPrimaryColumn_MouseUp);
            // 
            // columnHeaderColumnColumnName
            // 
            this.columnHeaderColumnColumnName.Text = "Column Alias";
            // 
            // columnHeaderColumnAlias
            // 
            this.columnHeaderColumnAlias.Text = "Name";
            // 
            // listViewForeignColumn
            // 
            this.listViewForeignColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewForeignColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewForeignColumn.FullRowSelect = true;
            this.listViewForeignColumn.Location = new System.Drawing.Point(69, 57);
            this.listViewForeignColumn.Name = "listViewForeignColumn";
            this.listViewForeignColumn.Size = new System.Drawing.Size(296, 128);
            this.listViewForeignColumn.TabIndex = 6;
            this.listViewForeignColumn.UseCompatibleStateImageBehavior = false;
            this.listViewForeignColumn.View = System.Windows.Forms.View.Details;
            this.listViewForeignColumn.Validating += new System.ComponentModel.CancelEventHandler(this.listViewForeignColumn_Validating);
            this.listViewForeignColumn.Resize += new System.EventHandler(this.listViewForeignColumn_Resize);
            this.listViewForeignColumn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewForeignColumn_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column Alias";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(0, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Columns";
            // 
            // comboBoxForeignScriptObject
            // 
            this.comboBoxForeignScriptObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxForeignScriptObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignScriptObject.FormattingEnabled = true;
            this.comboBoxForeignScriptObject.Location = new System.Drawing.Point(69, 3);
            this.comboBoxForeignScriptObject.Name = "comboBoxForeignScriptObject";
            this.comboBoxForeignScriptObject.Size = new System.Drawing.Size(296, 21);
            this.comboBoxForeignScriptObject.Sorted = true;
            this.comboBoxForeignScriptObject.TabIndex = 1;
            this.comboBoxForeignScriptObject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxForeignScriptObject_Validating);
            this.comboBoxForeignScriptObject.SelectedIndexChanged += new System.EventHandler(this.comboBoxForeignScriptObject_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(0, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Script Object";
            // 
            // contextMenuStripRelationship
            // 
            this.contextMenuStripRelationship.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRelationshipDelete});
            this.contextMenuStripRelationship.Name = "contextMenuStripRelationship";
            this.contextMenuStripRelationship.Size = new System.Drawing.Size(117, 26);
            // 
            // toolStripMenuItemRelationshipDelete
            // 
            this.toolStripMenuItemRelationshipDelete.Name = "toolStripMenuItemRelationshipDelete";
            this.toolStripMenuItemRelationshipDelete.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemRelationshipDelete.Text = "&Delete";
            this.toolStripMenuItemRelationshipDelete.Click += new System.EventHandler(this.toolStripMenuItemRelationshipDelete_Click);
            // 
            // buttonForeignColumnAdd
            // 
            this.buttonForeignColumnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonForeignColumnAdd.Location = new System.Drawing.Point(341, 28);
            this.buttonForeignColumnAdd.Name = "buttonForeignColumnAdd";
            this.buttonForeignColumnAdd.Size = new System.Drawing.Size(24, 23);
            this.buttonForeignColumnAdd.TabIndex = 4;
            this.buttonForeignColumnAdd.Text = "+";
            this.buttonForeignColumnAdd.UseVisualStyleBackColor = true;
            this.buttonForeignColumnAdd.Click += new System.EventHandler(this.buttonForeignColumnAdd_Click);
            // 
            // tabStripRelationship
            // 
            this.tabStripRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabStripRelationship.AutoSetFocusOnClick = true;
            this.tabStripRelationship.Controls.Add(this.tabStripPagePrimaryTable);
            this.tabStripRelationship.Controls.Add(this.tabStripPageRelationships);
            this.tabStripRelationship.Controls.Add(this.tabStripPageForeignTable);
            this.tabStripRelationship.Location = new System.Drawing.Point(-1, 226);
            this.tabStripRelationship.Name = "tabStripRelationship";
            this.tabStripRelationship.Size = new System.Drawing.Size(377, 236);
            this.tabStripRelationship.TabIndex = 8;
            this.tabStripRelationship.Text = "tabStripRelationship";
            // 
            // tabStripPagePrimaryTable
            // 
            this.tabStripPagePrimaryTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPagePrimaryTable.Controls.Add(this.buttonAddPrimaryFilter);
            this.tabStripPagePrimaryTable.Controls.Add(this.label13);
            this.tabStripPagePrimaryTable.Controls.Add(this.comboBoxPrimaryFilter);
            this.tabStripPagePrimaryTable.Controls.Add(this.comboBoxPrimaryScriptObject);
            this.tabStripPagePrimaryTable.Controls.Add(this.label6);
            this.tabStripPagePrimaryTable.Controls.Add(this.buttonPrimaryColumnAdd);
            this.tabStripPagePrimaryTable.Controls.Add(this.label8);
            this.tabStripPagePrimaryTable.Controls.Add(this.listViewPrimaryColumn);
            this.tabStripPagePrimaryTable.Controls.Add(this.label10);
            this.tabStripPagePrimaryTable.Controls.Add(this.comboBoxPrimaryColumn);
            this.tabStripPagePrimaryTable.Key = "TabStripPage";
            this.tabStripPagePrimaryTable.Location = new System.Drawing.Point(0, 21);
            this.tabStripPagePrimaryTable.Name = "tabStripPagePrimaryTable";
            this.tabStripPagePrimaryTable.Size = new System.Drawing.Size(377, 215);
            this.tabStripPagePrimaryTable.TabIndex = 0;
            this.tabStripPagePrimaryTable.Text = "Primary Table";
            // 
            // buttonAddPrimaryFilter
            // 
            this.buttonAddPrimaryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPrimaryFilter.Location = new System.Drawing.Point(341, 189);
            this.buttonAddPrimaryFilter.Name = "buttonAddPrimaryFilter";
            this.buttonAddPrimaryFilter.Size = new System.Drawing.Size(24, 23);
            this.buttonAddPrimaryFilter.TabIndex = 9;
            this.buttonAddPrimaryFilter.Text = "...";
            this.buttonAddPrimaryFilter.UseVisualStyleBackColor = true;
            this.buttonAddPrimaryFilter.Click += new System.EventHandler(this.buttonAddPrimaryFilter_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(0, 194);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Filter";
            // 
            // comboBoxPrimaryFilter
            // 
            this.comboBoxPrimaryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPrimaryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrimaryFilter.FormattingEnabled = true;
            this.comboBoxPrimaryFilter.Location = new System.Drawing.Point(69, 191);
            this.comboBoxPrimaryFilter.Name = "comboBoxPrimaryFilter";
            this.comboBoxPrimaryFilter.Size = new System.Drawing.Size(266, 21);
            this.comboBoxPrimaryFilter.Sorted = true;
            this.comboBoxPrimaryFilter.TabIndex = 8;
            this.comboBoxPrimaryFilter.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxPrimaryFilter_Validating);
            // 
            // comboBoxPrimaryScriptObject
            // 
            this.comboBoxPrimaryScriptObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPrimaryScriptObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrimaryScriptObject.Enabled = false;
            this.comboBoxPrimaryScriptObject.FormattingEnabled = true;
            this.comboBoxPrimaryScriptObject.Location = new System.Drawing.Point(69, 3);
            this.comboBoxPrimaryScriptObject.Name = "comboBoxPrimaryScriptObject";
            this.comboBoxPrimaryScriptObject.Size = new System.Drawing.Size(296, 21);
            this.comboBoxPrimaryScriptObject.Sorted = true;
            this.comboBoxPrimaryScriptObject.TabIndex = 1;
            this.comboBoxPrimaryScriptObject.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxPrimaryScriptObject_Validating);
            this.comboBoxPrimaryScriptObject.SelectedIndexChanged += new System.EventHandler(this.comboBoxPrimaryScriptObject_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(0, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Script Object";
            // 
            // buttonPrimaryColumnAdd
            // 
            this.buttonPrimaryColumnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrimaryColumnAdd.Location = new System.Drawing.Point(341, 28);
            this.buttonPrimaryColumnAdd.Name = "buttonPrimaryColumnAdd";
            this.buttonPrimaryColumnAdd.Size = new System.Drawing.Size(24, 23);
            this.buttonPrimaryColumnAdd.TabIndex = 4;
            this.buttonPrimaryColumnAdd.Text = "+";
            this.buttonPrimaryColumnAdd.UseVisualStyleBackColor = true;
            this.buttonPrimaryColumnAdd.Click += new System.EventHandler(this.buttonPrimaryColumnAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(0, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Columns";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(0, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Column";
            // 
            // comboBoxPrimaryColumn
            // 
            this.comboBoxPrimaryColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPrimaryColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrimaryColumn.FormattingEnabled = true;
            this.comboBoxPrimaryColumn.Location = new System.Drawing.Point(69, 30);
            this.comboBoxPrimaryColumn.Name = "comboBoxPrimaryColumn";
            this.comboBoxPrimaryColumn.Size = new System.Drawing.Size(266, 21);
            this.comboBoxPrimaryColumn.Sorted = true;
            this.comboBoxPrimaryColumn.TabIndex = 3;
            // 
            // tabStripPageRelationships
            // 
            this.tabStripPageRelationships.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageRelationships.Controls.Add(this.buttonAddIntermediatePrimaryRelationship);
            this.tabStripPageRelationships.Controls.Add(this.buttonAddIntermediateForeignRelationship);
            this.tabStripPageRelationships.Controls.Add(this.label11);
            this.tabStripPageRelationships.Controls.Add(this.comboBoxIntermediateForeignRelationship);
            this.tabStripPageRelationships.Controls.Add(this.label3);
            this.tabStripPageRelationships.Controls.Add(this.comboBoxIntermediatePrimaryRelationship);
            this.tabStripPageRelationships.Key = "TabStripPage";
            this.tabStripPageRelationships.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageRelationships.Name = "tabStripPageRelationships";
            this.tabStripPageRelationships.Size = new System.Drawing.Size(377, 215);
            this.tabStripPageRelationships.TabIndex = 1;
            this.tabStripPageRelationships.Text = "Relationships";
            // 
            // buttonAddIntermediatePrimaryRelationship
            // 
            this.buttonAddIntermediatePrimaryRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddIntermediatePrimaryRelationship.CausesValidation = false;
            this.buttonAddIntermediatePrimaryRelationship.Location = new System.Drawing.Point(341, 1);
            this.buttonAddIntermediatePrimaryRelationship.Name = "buttonAddIntermediatePrimaryRelationship";
            this.buttonAddIntermediatePrimaryRelationship.Size = new System.Drawing.Size(24, 23);
            this.buttonAddIntermediatePrimaryRelationship.TabIndex = 2;
            this.buttonAddIntermediatePrimaryRelationship.Text = "...";
            this.buttonAddIntermediatePrimaryRelationship.UseVisualStyleBackColor = true;
            this.buttonAddIntermediatePrimaryRelationship.Click += new System.EventHandler(this.buttonAddIntermediatePrimaryRelationship_Click);
            // 
            // buttonAddIntermediateForeignRelationship
            // 
            this.buttonAddIntermediateForeignRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddIntermediateForeignRelationship.CausesValidation = false;
            this.buttonAddIntermediateForeignRelationship.Location = new System.Drawing.Point(341, 28);
            this.buttonAddIntermediateForeignRelationship.Name = "buttonAddIntermediateForeignRelationship";
            this.buttonAddIntermediateForeignRelationship.Size = new System.Drawing.Size(24, 23);
            this.buttonAddIntermediateForeignRelationship.TabIndex = 5;
            this.buttonAddIntermediateForeignRelationship.Text = "...";
            this.buttonAddIntermediateForeignRelationship.UseVisualStyleBackColor = true;
            this.buttonAddIntermediateForeignRelationship.Click += new System.EventHandler(this.buttonAddIntermediateForeignRelationship_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(0, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Foreign";
            // 
            // comboBoxIntermediateForeignRelationship
            // 
            this.comboBoxIntermediateForeignRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxIntermediateForeignRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntermediateForeignRelationship.FormattingEnabled = true;
            this.comboBoxIntermediateForeignRelationship.Location = new System.Drawing.Point(69, 30);
            this.comboBoxIntermediateForeignRelationship.Name = "comboBoxIntermediateForeignRelationship";
            this.comboBoxIntermediateForeignRelationship.Size = new System.Drawing.Size(266, 21);
            this.comboBoxIntermediateForeignRelationship.Sorted = true;
            this.comboBoxIntermediateForeignRelationship.TabIndex = 4;
            this.comboBoxIntermediateForeignRelationship.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxIntermediateForeignRelationship_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(0, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Primary";
            // 
            // comboBoxIntermediatePrimaryRelationship
            // 
            this.comboBoxIntermediatePrimaryRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxIntermediatePrimaryRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntermediatePrimaryRelationship.FormattingEnabled = true;
            this.comboBoxIntermediatePrimaryRelationship.Location = new System.Drawing.Point(69, 3);
            this.comboBoxIntermediatePrimaryRelationship.Name = "comboBoxIntermediatePrimaryRelationship";
            this.comboBoxIntermediatePrimaryRelationship.Size = new System.Drawing.Size(266, 21);
            this.comboBoxIntermediatePrimaryRelationship.Sorted = true;
            this.comboBoxIntermediatePrimaryRelationship.TabIndex = 1;
            this.comboBoxIntermediatePrimaryRelationship.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxIntermediatePrimaryRelationship_Validating);
            this.comboBoxIntermediatePrimaryRelationship.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntermediatePrimaryRelationship_SelectedIndexChanged);
            // 
            // tabStripPageForeignTable
            // 
            this.tabStripPageForeignTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageForeignTable.Controls.Add(this.buttonAddForeignFilter);
            this.tabStripPageForeignTable.Controls.Add(this.label12);
            this.tabStripPageForeignTable.Controls.Add(this.comboBoxForeignFilter);
            this.tabStripPageForeignTable.Controls.Add(this.label9);
            this.tabStripPageForeignTable.Controls.Add(this.label5);
            this.tabStripPageForeignTable.Controls.Add(this.buttonForeignColumnAdd);
            this.tabStripPageForeignTable.Controls.Add(this.comboBoxForeignColumn);
            this.tabStripPageForeignTable.Controls.Add(this.label7);
            this.tabStripPageForeignTable.Controls.Add(this.listViewForeignColumn);
            this.tabStripPageForeignTable.Controls.Add(this.comboBoxForeignScriptObject);
            this.tabStripPageForeignTable.Key = "TabStripPage";
            this.tabStripPageForeignTable.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageForeignTable.Name = "tabStripPageForeignTable";
            this.tabStripPageForeignTable.Size = new System.Drawing.Size(377, 215);
            this.tabStripPageForeignTable.TabIndex = 2;
            this.tabStripPageForeignTable.Text = "Foreign Table";
            // 
            // buttonAddForeignFilter
            // 
            this.buttonAddForeignFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddForeignFilter.Location = new System.Drawing.Point(341, 189);
            this.buttonAddForeignFilter.Name = "buttonAddForeignFilter";
            this.buttonAddForeignFilter.Size = new System.Drawing.Size(24, 23);
            this.buttonAddForeignFilter.TabIndex = 9;
            this.buttonAddForeignFilter.Text = "...";
            this.buttonAddForeignFilter.UseVisualStyleBackColor = true;
            this.buttonAddForeignFilter.Click += new System.EventHandler(this.buttonAddForeignFilter_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(0, 194);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Filter";
            // 
            // comboBoxForeignFilter
            // 
            this.comboBoxForeignFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxForeignFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForeignFilter.FormattingEnabled = true;
            this.comboBoxForeignFilter.Location = new System.Drawing.Point(69, 191);
            this.comboBoxForeignFilter.Name = "comboBoxForeignFilter";
            this.comboBoxForeignFilter.Size = new System.Drawing.Size(266, 21);
            this.comboBoxForeignFilter.Sorted = true;
            this.comboBoxForeignFilter.TabIndex = 8;
            this.comboBoxForeignFilter.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxForeignFilter_Validating);
            // 
            // checkBoxIsBase
            // 
            this.checkBoxIsBase.AutoSize = true;
            this.checkBoxIsBase.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxIsBase.Enabled = false;
            this.checkBoxIsBase.Location = new System.Drawing.Point(265, 158);
            this.checkBoxIsBase.Name = "checkBoxIsBase";
            this.checkBoxIsBase.Size = new System.Drawing.Size(61, 17);
            this.checkBoxIsBase.TabIndex = 7;
            this.checkBoxIsBase.Text = "Is Base";
            this.checkBoxIsBase.UseVisualStyleBackColor = false;
            this.checkBoxIsBase.Visible = false;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 464);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(376, 29);
            this.ucHeading1.TabIndex = 11;
            this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(53, 115);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(311, 20);
            this.textBoxDescription.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(12, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Desc.";
            // 
            // FormRelationship
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(376, 493);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.checkBoxIsBase);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBoxType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabStripRelationship);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRelationship";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relationship";
            this.Load += new System.EventHandler(this.FormRelationship_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRelationship_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.contextMenuStripRelationship.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabStripRelationship)).EndInit();
            this.tabStripRelationship.ResumeLayout(false);
            this.tabStripPagePrimaryTable.ResumeLayout(false);
            this.tabStripPagePrimaryTable.PerformLayout();
            this.tabStripPageRelationships.ResumeLayout(false);
            this.tabStripPageRelationships.PerformLayout();
            this.tabStripPageForeignTable.ResumeLayout(false);
            this.tabStripPageForeignTable.PerformLayout();
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
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.RadioButton radioButtonManyToOne;
        private System.Windows.Forms.RadioButton radioButtonManyToMany;
        private System.Windows.Forms.RadioButton radioButtonOneToMany;
        private System.Windows.Forms.RadioButton radioButtonOneToOne;
        private System.Windows.Forms.ComboBox comboBoxForeignColumn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listViewPrimaryColumn;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnColumnName;
        private System.Windows.Forms.ColumnHeader columnHeaderColumnAlias;
        private System.Windows.Forms.ListView listViewForeignColumn;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxForeignScriptObject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRelationship;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRelationshipDelete;
        private System.Windows.Forms.Button buttonForeignColumnAdd;
        private ActiproSoftware.UIStudio.TabStrip.TabStrip tabStripRelationship;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPagePrimaryTable;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageRelationships;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageForeignTable;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxIntermediateForeignRelationship;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxIntermediatePrimaryRelationship;
        private System.Windows.Forms.CheckBox checkBoxIsBase;
        private System.Windows.Forms.ComboBox comboBoxPrimaryScriptObject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonPrimaryColumnAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxPrimaryColumn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxPrimaryFilter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxForeignFilter;
        private System.Windows.Forms.Button buttonAddPrimaryFilter;
        private System.Windows.Forms.Button buttonAddIntermediatePrimaryRelationship;
        private System.Windows.Forms.Button buttonAddIntermediateForeignRelationship;
        private System.Windows.Forms.Button buttonAddForeignFilter;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label14;
    }
}