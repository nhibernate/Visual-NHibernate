namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	partial class FormCustomType
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCustomType));
			this.buttonOk = new System.Windows.Forms.Button();
			this.labelDatabaseType = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.comboBoxCSharpTypes = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.checkBoxExisting = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxNew = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.textBoxDotNetType = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.textBoxCSharpType = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelQuestion = new DevComponents.DotNetBar.LabelX();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonOk.Location = new System.Drawing.Point(137, 246);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// labelDatabaseType
			// 
			this.labelDatabaseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelDatabaseType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDatabaseType.Location = new System.Drawing.Point(87, 33);
			this.labelDatabaseType.Name = "labelDatabaseType";
			this.labelDatabaseType.Size = new System.Drawing.Size(237, 36);
			this.labelDatabaseType.TabIndex = 1;
			this.labelDatabaseType.Text = "Database type";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(66, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(258, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "A new type has been discovered in the database:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(36, 36);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// labelX1
			// 
			this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.Location = new System.Drawing.Point(25, 217);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(312, 23);
			this.labelX1.TabIndex = 7;
			this.labelX1.Text = "<b>Note:</b> Manage types via <i>File &nbsp;&nbsp;&gt; Options &nbsp;&nbsp;&gt; T" +
				"ype&nbsp;&nbsp;Mappings</i>";
			// 
			// comboBoxCSharpTypes
			// 
			this.comboBoxCSharpTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxCSharpTypes.DisplayMember = "Text";
			this.comboBoxCSharpTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxCSharpTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCSharpTypes.FormattingEnabled = true;
			this.comboBoxCSharpTypes.ItemHeight = 14;
			this.comboBoxCSharpTypes.Location = new System.Drawing.Point(131, 92);
			this.comboBoxCSharpTypes.Name = "comboBoxCSharpTypes";
			this.comboBoxCSharpTypes.Size = new System.Drawing.Size(193, 20);
			this.comboBoxCSharpTypes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxCSharpTypes.TabIndex = 8;
			// 
			// checkBoxExisting
			// 
			// 
			// 
			// 
			this.checkBoxExisting.BackgroundStyle.Class = "";
			this.checkBoxExisting.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxExisting.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.checkBoxExisting.Checked = true;
			this.checkBoxExisting.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxExisting.CheckValue = "Y";
			this.checkBoxExisting.Location = new System.Drawing.Point(25, 89);
			this.checkBoxExisting.Name = "checkBoxExisting";
			this.checkBoxExisting.Size = new System.Drawing.Size(100, 23);
			this.checkBoxExisting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxExisting.TabIndex = 9;
			this.checkBoxExisting.Text = " Select existing";
			// 
			// checkBoxNew
			// 
			// 
			// 
			// 
			this.checkBoxNew.BackgroundStyle.Class = "";
			this.checkBoxNew.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxNew.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.checkBoxNew.Location = new System.Drawing.Point(25, 109);
			this.checkBoxNew.Name = "checkBoxNew";
			this.checkBoxNew.Size = new System.Drawing.Size(100, 23);
			this.checkBoxNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxNew.TabIndex = 10;
			this.checkBoxNew.Text = " Create new";
			this.checkBoxNew.CheckedChanged += new System.EventHandler(this.checkBoxNew_CheckedChanged);
			// 
			// labelX2
			// 
			this.labelX2.AutoSize = true;
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.ForeColor = System.Drawing.Color.Black;
			this.labelX2.Location = new System.Drawing.Point(10, 10);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(46, 15);
			this.labelX2.TabIndex = 11;
			this.labelX2.Text = ".Net type";
			// 
			// textBoxDotNetType
			// 
			this.textBoxDotNetType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.textBoxDotNetType.Border.Class = "TextBoxBorder";
			this.textBoxDotNetType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxDotNetType.Location = new System.Drawing.Point(62, 8);
			this.textBoxDotNetType.Name = "textBoxDotNetType";
			this.textBoxDotNetType.Size = new System.Drawing.Size(187, 20);
			this.textBoxDotNetType.TabIndex = 12;
			// 
			// groupPanel1
			// 
			this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.groupPanel1.Controls.Add(this.labelX3);
			this.groupPanel1.Controls.Add(this.textBoxCSharpType);
			this.groupPanel1.Controls.Add(this.labelX2);
			this.groupPanel1.Controls.Add(this.textBoxDotNetType);
			this.groupPanel1.Enabled = false;
			this.groupPanel1.Location = new System.Drawing.Point(55, 138);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(269, 72);
			// 
			// 
			// 
			this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.groupPanel1.Style.BackColorGradientAngle = 90;
			this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderBottomWidth = 1;
			this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderLeftWidth = 1;
			this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderRightWidth = 1;
			this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.groupPanel1.Style.BorderTopWidth = 1;
			this.groupPanel1.Style.Class = "";
			this.groupPanel1.Style.CornerDiameter = 4;
			this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseDown.Class = "";
			this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseOver.Class = "";
			this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupPanel1.TabIndex = 13;
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			this.labelX3.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.ForeColor = System.Drawing.Color.Black;
			this.labelX3.Location = new System.Drawing.Point(11, 31);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(40, 15);
			this.labelX3.TabIndex = 13;
			this.labelX3.Text = "C# type";
			// 
			// textBoxCSharpType
			// 
			this.textBoxCSharpType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.textBoxCSharpType.Border.Class = "TextBoxBorder";
			this.textBoxCSharpType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxCSharpType.Location = new System.Drawing.Point(62, 30);
			this.textBoxCSharpType.Name = "textBoxCSharpType";
			this.textBoxCSharpType.Size = new System.Drawing.Size(187, 20);
			this.textBoxCSharpType.TabIndex = 14;
			// 
			// labelQuestion
			// 
			this.labelQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelQuestion.BackgroundStyle.Class = "";
			this.labelQuestion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelQuestion.Location = new System.Drawing.Point(25, 63);
			this.labelQuestion.Name = "labelQuestion";
			this.labelQuestion.Size = new System.Drawing.Size(319, 23);
			this.labelQuestion.TabIndex = 15;
			this.labelQuestion.Text = "What .Net types do you want to map <b>{0}</b> to?";
			// 
			// FormCustomType
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(349, 282);
			this.ControlBox = false;
			this.Controls.Add(this.labelQuestion);
			this.Controls.Add(this.groupPanel1);
			this.Controls.Add(this.checkBoxNew);
			this.Controls.Add(this.comboBoxCSharpTypes);
			this.Controls.Add(this.checkBoxExisting);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.labelDatabaseType);
			this.Controls.Add(this.buttonOk);
			this.Name = "FormCustomType";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Database Type";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Label labelDatabaseType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCSharpTypes;
		private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxExisting;
		private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxNew;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxDotNetType;
		private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxCSharpType;
		private DevComponents.DotNetBar.LabelX labelQuestion;
	}
}