namespace ArchAngel.Providers.EntityModel.UI
{
	partial class LoadDatabaseForm
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
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.ucDatabaseInformation1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.Controls.Add(this.buttonOk);
			this.panelEx1.Controls.Add(this.buttonCancel);
			this.panelEx1.Controls.Add(this.ucDatabaseInformation1);
			this.panelEx1.Controls.Add(this.labelX1);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(524, 442);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 1;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.Location = new System.Drawing.Point(356, 407);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(437, 407);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// ucDatabaseInformation1
			// 
			this.ucDatabaseInformation1.BackColor = System.Drawing.Color.Black;
			this.ucDatabaseInformation1.DatabaseHelper = null;
			this.ucDatabaseInformation1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDatabaseInformation1.EventRaisingDisabled = false;
			this.ucDatabaseInformation1.ForeColor = System.Drawing.Color.White;
			this.ucDatabaseInformation1.Location = new System.Drawing.Point(0, 23);
			this.ucDatabaseInformation1.Margin = new System.Windows.Forms.Padding(4);
			this.ucDatabaseInformation1.Name = "ucDatabaseInformation1";
			this.ucDatabaseInformation1.Password = "";
			this.ucDatabaseInformation1.Port = 1433;
			this.ucDatabaseInformation1.SelectedDatabaseType = ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005;
			this.ucDatabaseInformation1.SelectedServerName = "";
			this.ucDatabaseInformation1.ServiceName = "";
			this.ucDatabaseInformation1.Size = new System.Drawing.Size(524, 419);
			this.ucDatabaseInformation1.TabIndex = 0;
			this.ucDatabaseInformation1.UseIntegratedSecurity = false;
			this.ucDatabaseInformation1.Username = "sa";
			this.ucDatabaseInformation1.UsingDatabaseFile = false;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(0, 0);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(524, 23);
			this.labelX1.TabIndex = 1;
			// 
			// LoadDatabaseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 442);
			this.Controls.Add(this.panelEx1);
			this.Name = "LoadDatabaseForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Settings";
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ArchAngel.Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation ucDatabaseInformation1;
		private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
	}
}