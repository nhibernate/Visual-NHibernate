namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class FormCodeInput
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonOk = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAutoAddToEntities = new System.Windows.Forms.CheckBox();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.syntaxEditor1.Document = document1;
            this.syntaxEditor1.Location = new System.Drawing.Point(16, 45);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(540, 177);
            this.syntaxEditor1.TabIndex = 0;
            this.syntaxEditor1.TriggerActivated += new ActiproSoftware.SyntaxEditor.TriggerEventHandler(this.syntaxEditor1_TriggerActivated);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.buttonCancel);
            this.panelEx1.Controls.Add(this.buttonOk);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.checkBoxAutoAddToEntities);
            this.panelEx1.Controls.Add(this.syntaxEditor1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(573, 279);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(301, 246);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonOk.Location = new System.Drawing.Point(197, 246);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonOk.TabIndex = 18;
            this.buttonOk.Text = "Ok";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(335, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Variables: #entity.Name#";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Auto-add to entities:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxAutoAddToEntities
            // 
            this.checkBoxAutoAddToEntities.AutoSize = true;
            this.checkBoxAutoAddToEntities.Location = new System.Drawing.Point(132, 9);
            this.checkBoxAutoAddToEntities.Name = "checkBoxAutoAddToEntities";
            this.checkBoxAutoAddToEntities.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAutoAddToEntities.TabIndex = 15;
            this.checkBoxAutoAddToEntities.UseVisualStyleBackColor = true;
            // 
            // FormCodeInput
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(573, 279);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormCodeInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Property";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAutoAddToEntities;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX buttonCancel;
        private DevComponents.DotNetBar.ButtonX buttonOk;
    }
}
