namespace ArchAngel.Designer
{
	partial class frmConstantsDataEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConstantsDataEntry));
            this.ddlConstantFunctions = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddConstant = new System.Windows.Forms.Button();
            this.ddlConstType = new System.Windows.Forms.ComboBox();
            this.txtConstName = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.SuspendLayout();
            // 
            // ddlConstantFunctions
            // 
            this.ddlConstantFunctions.FormattingEnabled = true;
            this.ddlConstantFunctions.Location = new System.Drawing.Point(63, 143);
            this.ddlConstantFunctions.Margin = new System.Windows.Forms.Padding(2);
            this.ddlConstantFunctions.Name = "ddlConstantFunctions";
            this.ddlConstantFunctions.Size = new System.Drawing.Size(164, 21);
            this.ddlConstantFunctions.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(61, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 73);
            this.label4.TabIndex = 8;
            this.label4.Text = "Static values such as \"hello\", 45, 2.3. Also functions that will be called once a" +
                "t runtime, such as DateTime.Now, or any of your parameterless functions.";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(207, 181);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddConstant
            // 
            this.btnAddConstant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddConstant.Location = new System.Drawing.Point(147, 181);
            this.btnAddConstant.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddConstant.Name = "btnAddConstant";
            this.btnAddConstant.Size = new System.Drawing.Size(56, 24);
            this.btnAddConstant.TabIndex = 4;
            this.btnAddConstant.Text = "OK";
            this.btnAddConstant.UseVisualStyleBackColor = true;
            this.btnAddConstant.Click += new System.EventHandler(this.btnAddConstant_Click);
            // 
            // ddlConstType
            // 
            this.ddlConstType.FormattingEnabled = true;
            this.ddlConstType.Items.AddRange(new object[] {
            "string",
            "int",
            "bool",
            "Runtime"});
            this.ddlConstType.Location = new System.Drawing.Point(63, 33);
            this.ddlConstType.Margin = new System.Windows.Forms.Padding(2);
            this.ddlConstType.Name = "ddlConstType";
            this.ddlConstType.Size = new System.Drawing.Size(164, 21);
            this.ddlConstType.TabIndex = 1;
            this.ddlConstType.SelectedIndexChanged += new System.EventHandler(this.ddlConstType_SelectedIndexChanged);
            // 
            // txtConstName
            // 
            this.txtConstName.Location = new System.Drawing.Point(63, 10);
            this.txtConstName.Margin = new System.Windows.Forms.Padding(2);
            this.txtConstName.Name = "txtConstName";
            this.txtConstName.Size = new System.Drawing.Size(164, 20);
            this.txtConstName.TabIndex = 0;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(63, 57);
            this.txtValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(164, 20);
            this.txtValue.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 186);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(269, 35);
            this.ucHeading1.TabIndex = 10;
            // 
            // frmConstantsDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(269, 221);
            this.Controls.Add(this.ddlConstantFunctions);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConstName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddConstant);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlConstType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(273, 246);
            this.Name = "frmConstantsDataEntry";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Constant Data";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmConstantsDataEntry_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmConstantsDataEntry_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox ddlConstantFunctions;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAddConstant;
		private System.Windows.Forms.ComboBox ddlConstType;
		private System.Windows.Forms.TextBox txtConstName;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
        private Slyce.Common.Controls.ucHeading ucHeading1;
	}
}