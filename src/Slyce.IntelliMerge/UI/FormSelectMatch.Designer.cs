namespace Slyce.IntelliMerge.UI
{
    partial class FormSelectMatch
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
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
			ActiproSoftware.SyntaxEditor.Document document3 = new ActiproSoftware.SyntaxEditor.Document();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.FormSelectMatchHeading = new Slyce.Common.Controls.ucHeading();
			this.userComboBox = new System.Windows.Forms.ComboBox();
			this.templateComboBox = new System.Windows.Forms.ComboBox();
			this.prevgenComboBox = new System.Windows.Forms.ComboBox();
			this.userSyntaxEditor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.templateSyntaxEditor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.prevgenSyntaxEditor = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.acceptButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// FormSelectMatchHeading
			// 
			this.FormSelectMatchHeading.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.FormSelectMatchHeading.Location = new System.Drawing.Point(0, 477);
			this.FormSelectMatchHeading.Margin = new System.Windows.Forms.Padding(2);
			this.FormSelectMatchHeading.Name = "FormSelectMatchHeading";
			this.FormSelectMatchHeading.Size = new System.Drawing.Size(1342, 34);
			this.FormSelectMatchHeading.TabIndex = 29;
			this.FormSelectMatchHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// userComboBox
			// 
			this.userComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.userComboBox.FormattingEnabled = true;
			this.userComboBox.Location = new System.Drawing.Point(895, 25);
			this.userComboBox.Name = "userComboBox";
			this.userComboBox.Size = new System.Drawing.Size(442, 21);
			this.userComboBox.TabIndex = 28;
			this.userComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// templateComboBox
			// 
			this.templateComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.templateComboBox.FormattingEnabled = true;
			this.templateComboBox.Location = new System.Drawing.Point(450, 25);
			this.templateComboBox.Name = "templateComboBox";
			this.templateComboBox.Size = new System.Drawing.Size(439, 21);
			this.templateComboBox.TabIndex = 27;
			this.templateComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// prevgenComboBox
			// 
			this.prevgenComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.prevgenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.prevgenComboBox.FormattingEnabled = true;
			this.prevgenComboBox.Location = new System.Drawing.Point(5, 25);
			this.prevgenComboBox.Name = "prevgenComboBox";
			this.prevgenComboBox.Size = new System.Drawing.Size(439, 21);
			this.prevgenComboBox.TabIndex = 26;
			this.prevgenComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// userSyntaxEditor
			// 
			this.userSyntaxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userSyntaxEditor.Document = document1;
			this.userSyntaxEditor.LineNumberMarginVisible = true;
			this.userSyntaxEditor.Location = new System.Drawing.Point(895, 51);
			this.userSyntaxEditor.Name = "userSyntaxEditor";
			this.userSyntaxEditor.Size = new System.Drawing.Size(442, 421);
			this.userSyntaxEditor.TabIndex = 25;
			// 
			// templateSyntaxEditor
			// 
			this.templateSyntaxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.templateSyntaxEditor.Document = document2;
			this.templateSyntaxEditor.LineNumberMarginVisible = true;
			this.templateSyntaxEditor.Location = new System.Drawing.Point(5, 51);
			this.templateSyntaxEditor.Name = "templateSyntaxEditor";
			this.templateSyntaxEditor.Size = new System.Drawing.Size(439, 421);
			this.templateSyntaxEditor.TabIndex = 24;
			// 
			// prevgenSyntaxEditor
			// 
			this.prevgenSyntaxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.prevgenSyntaxEditor.Document = document3;
			this.prevgenSyntaxEditor.LineNumberMarginVisible = true;
			this.prevgenSyntaxEditor.Location = new System.Drawing.Point(450, 51);
			this.prevgenSyntaxEditor.Name = "prevgenSyntaxEditor";
			this.prevgenSyntaxEditor.Size = new System.Drawing.Size(439, 421);
			this.prevgenSyntaxEditor.TabIndex = 23;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
			this.tableLayoutPanel.Controls.Add(this.label6, 2, 0);
			this.tableLayoutPanel.Controls.Add(this.label5, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.templateSyntaxEditor, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.prevgenSyntaxEditor, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.userSyntaxEditor, 2, 2);
			this.tableLayoutPanel.Controls.Add(this.userComboBox, 2, 1);
			this.tableLayoutPanel.Controls.Add(this.prevgenComboBox, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.templateComboBox, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel.RowCount = 3;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(1342, 477);
			this.tableLayoutPanel.TabIndex = 36;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(895, 2);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(442, 20);
			this.label6.TabIndex = 31;
			this.label6.Text = "User";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(450, 2);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(439, 20);
			this.label5.TabIndex = 30;
			this.label5.Text = "Newly Generated";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(5, 2);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(439, 20);
			this.label4.TabIndex = 29;
			this.label4.Text = "Previously Generated";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(1267, 482);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// acceptButton
			// 
			this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.acceptButton.Location = new System.Drawing.Point(1186, 482);
			this.acceptButton.Name = "acceptButton";
			this.acceptButton.Size = new System.Drawing.Size(75, 23);
			this.acceptButton.TabIndex = 0;
			this.acceptButton.Text = "OK";
			this.acceptButton.UseVisualStyleBackColor = true;
			this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
			// 
			// FormSelectMatch
			// 
			this.AcceptButton = this.acceptButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(1342, 511);
			this.Controls.Add(this.acceptButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.FormSelectMatchHeading);
			this.Name = "FormSelectMatch";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Matching Constructs";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSelectMatch_FormClosing);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private Slyce.Common.Controls.ucHeading FormSelectMatchHeading;
        private System.Windows.Forms.ComboBox userComboBox;
        private System.Windows.Forms.ComboBox templateComboBox;
        private System.Windows.Forms.ComboBox prevgenComboBox;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor userSyntaxEditor;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor templateSyntaxEditor;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor prevgenSyntaxEditor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;

    }
}