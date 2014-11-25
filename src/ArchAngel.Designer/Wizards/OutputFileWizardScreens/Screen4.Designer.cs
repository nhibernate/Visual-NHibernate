namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
    partial class Screen4
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
			this.ddlExistingFunctions = new System.Windows.Forms.ComboBox();
			this.optExistingFunction = new System.Windows.Forms.RadioButton();
			this.optNewFunction = new System.Windows.Forms.RadioButton();
			this.labelFunctionSelectionDescription = new System.Windows.Forms.Label();
			this.optNoSkip = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// ddlExistingFunctions
			// 
			this.ddlExistingFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ddlExistingFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlExistingFunctions.FormattingEnabled = true;
			this.ddlExistingFunctions.Location = new System.Drawing.Point(185, 39);
			this.ddlExistingFunctions.Name = "ddlExistingFunctions";
			this.ddlExistingFunctions.Size = new System.Drawing.Size(686, 21);
			this.ddlExistingFunctions.TabIndex = 7;
			this.ddlExistingFunctions.SelectedIndexChanged += new System.EventHandler(this.ddlExistingFunctions_SelectedIndexChanged);
			// 
			// optExistingFunction
			// 
			this.optExistingFunction.AutoSize = true;
			this.optExistingFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optExistingFunction.Location = new System.Drawing.Point(13, 40);
			this.optExistingFunction.Name = "optExistingFunction";
			this.optExistingFunction.Size = new System.Drawing.Size(166, 17);
			this.optExistingFunction.TabIndex = 6;
			this.optExistingFunction.Text = "Use an existing function:";
			this.optExistingFunction.UseVisualStyleBackColor = true;
			// 
			// optNewFunction
			// 
			this.optNewFunction.AutoSize = true;
			this.optNewFunction.Checked = true;
			this.optNewFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optNewFunction.Location = new System.Drawing.Point(13, 17);
			this.optNewFunction.Name = "optNewFunction";
			this.optNewFunction.Size = new System.Drawing.Size(150, 17);
			this.optNewFunction.TabIndex = 5;
			this.optNewFunction.TabStop = true;
			this.optNewFunction.Text = "Create a new function";
			this.optNewFunction.UseVisualStyleBackColor = true;
			// 
			// labelFunctionSelectionDescription
			// 
			this.labelFunctionSelectionDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelFunctionSelectionDescription.Location = new System.Drawing.Point(194, 63);
			this.labelFunctionSelectionDescription.Name = "labelFunctionSelectionDescription";
			this.labelFunctionSelectionDescription.Size = new System.Drawing.Size(677, 53);
			this.labelFunctionSelectionDescription.TabIndex = 8;
			this.labelFunctionSelectionDescription.Text = "Only functions with a single paramter of the following type are available for sel" +
				"ection: xxx";
			// 
			// optNoSkip
			// 
			this.optNoSkip.AutoSize = true;
			this.optNoSkip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optNoSkip.Location = new System.Drawing.Point(13, 63);
			this.optNoSkip.Name = "optNoSkip";
			this.optNoSkip.Size = new System.Drawing.Size(144, 17);
			this.optNoSkip.TabIndex = 10;
			this.optNoSkip.TabStop = true;
			this.optNoSkip.Text = "No Skip File function";
			this.optNoSkip.UseVisualStyleBackColor = true;
			this.optNoSkip.Visible = false;
			// 
			// Screen4
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.optNoSkip);
			this.Controls.Add(this.labelFunctionSelectionDescription);
			this.Controls.Add(this.ddlExistingFunctions);
			this.Controls.Add(this.optExistingFunction);
			this.Controls.Add(this.optNewFunction);
			this.Name = "Screen4";
			this.Size = new System.Drawing.Size(893, 434);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlExistingFunctions;
        private System.Windows.Forms.RadioButton optExistingFunction;
        private System.Windows.Forms.RadioButton optNewFunction;
        private System.Windows.Forms.Label labelFunctionSelectionDescription;
		private System.Windows.Forms.RadioButton optNoSkip;
    }
}
