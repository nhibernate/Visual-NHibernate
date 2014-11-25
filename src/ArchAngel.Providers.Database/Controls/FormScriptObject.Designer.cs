namespace ArchAngel.Providers.Database.Controls
{
    partial class FormScriptObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScriptObject));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAliasPlural = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkBoxRenameRelatedObjects = new System.Windows.Forms.CheckBox();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.listViewLookups = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panelLookup = new DevComponents.DotNetBar.ExpandablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.expandablePanel1.SuspendLayout();
            this.panelLookup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(17, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(58, 43);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(250, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAlias.Location = new System.Drawing.Point(58, 69);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(250, 20);
            this.textBoxAlias.TabIndex = 3;
            this.textBoxAlias.TextChanged += new System.EventHandler(this.textBoxAlias_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(17, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alias";
            // 
            // textBoxAliasPlural
            // 
            this.textBoxAliasPlural.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAliasPlural.Location = new System.Drawing.Point(58, 95);
            this.textBoxAliasPlural.Name = "textBoxAliasPlural";
            this.textBoxAliasPlural.Size = new System.Drawing.Size(250, 20);
            this.textBoxAliasPlural.TabIndex = 5;
            this.textBoxAliasPlural.TextChanged += new System.EventHandler(this.textBoxAliasPlural_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(17, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Plural";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(185, 393);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(266, 393);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // checkBoxRenameRelatedObjects
            // 
            this.checkBoxRenameRelatedObjects.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRenameRelatedObjects.Location = new System.Drawing.Point(58, 144);
            this.checkBoxRenameRelatedObjects.Name = "checkBoxRenameRelatedObjects";
            this.checkBoxRenameRelatedObjects.Size = new System.Drawing.Size(176, 30);
            this.checkBoxRenameRelatedObjects.TabIndex = 6;
            this.checkBoxRenameRelatedObjects.Text = "Rename Related Objects";
            this.checkBoxRenameRelatedObjects.UseVisualStyleBackColor = false;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 387);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(347, 35);
            this.ucHeading1.TabIndex = 9;
            this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(58, 121);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(250, 20);
            this.textBoxDescription.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(17, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Desc.";
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel1.Controls.Add(this.listViewLookups);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 183);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(347, 204);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 31;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("expandablePanel1.TitleStyle.BackgroundImage")));
            this.expandablePanel1.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.CenterLeft;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleStyle.MarginLeft = 30;
            this.expandablePanel1.TitleText = "Attached Lookups";
            // 
            // listViewLookups
            // 
            this.listViewLookups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.listViewLookups.Border.Class = "ListViewBorder";
            this.listViewLookups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewLookups.Location = new System.Drawing.Point(12, 35);
            this.listViewLookups.Name = "listViewLookups";
            this.listViewLookups.Size = new System.Drawing.Size(319, 153);
            this.listViewLookups.TabIndex = 1;
            this.listViewLookups.UseCompatibleStateImageBehavior = false;
            this.listViewLookups.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Lookups";
            this.columnHeader1.Width = 238;
            // 
            // panelLookup
            // 
            this.panelLookup.AnimationTime = 0;
            this.panelLookup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelLookup.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelLookup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelLookup.Controls.Add(this.label1);
            this.panelLookup.Controls.Add(this.textBoxName);
            this.panelLookup.Controls.Add(this.textBoxDescription);
            this.panelLookup.Controls.Add(this.label2);
            this.panelLookup.Controls.Add(this.label4);
            this.panelLookup.Controls.Add(this.textBoxAlias);
            this.panelLookup.Controls.Add(this.checkBoxRenameRelatedObjects);
            this.panelLookup.Controls.Add(this.label3);
            this.panelLookup.Controls.Add(this.textBoxAliasPlural);
            this.panelLookup.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLookup.ExpandButtonVisible = false;
            this.panelLookup.Location = new System.Drawing.Point(0, 0);
            this.panelLookup.Name = "panelLookup";
            this.panelLookup.Size = new System.Drawing.Size(347, 183);
            this.panelLookup.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelLookup.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelLookup.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelLookup.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelLookup.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelLookup.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelLookup.Style.GradientAngle = 90;
            this.panelLookup.TabIndex = 32;
            this.panelLookup.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelLookup.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelLookup.TitleStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLookup.TitleStyle.BackgroundImage")));
            this.panelLookup.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.CenterLeft;
            this.panelLookup.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.panelLookup.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelLookup.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLookup.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelLookup.TitleStyle.GradientAngle = 90;
            this.panelLookup.TitleStyle.MarginLeft = 30;
            this.panelLookup.TitleText = "Details";
            // 
            // FormScriptObject
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(347, 422);
            this.Controls.Add(this.expandablePanel1);
            this.Controls.Add(this.panelLookup);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.ucHeading1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(353, 178);
            this.Name = "FormScriptObject";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ScriptObject";
            this.Load += new System.EventHandler(this.FormScriptObject_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormScriptObject_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.expandablePanel1.ResumeLayout(false);
            this.panelLookup.ResumeLayout(false);
            this.panelLookup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAliasPlural;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox checkBoxRenameRelatedObjects;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewLookups;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private DevComponents.DotNetBar.ExpandablePanel panelLookup;
    }
}