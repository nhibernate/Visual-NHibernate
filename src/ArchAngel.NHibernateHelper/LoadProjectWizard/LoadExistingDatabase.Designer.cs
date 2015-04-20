namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	partial class LoadExistingDatabase
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
			this.label1 = new System.Windows.Forms.Label();
			this.buttonBack = new DevComponents.DotNetBar.ButtonX();
			this.buttonFinish = new DevComponents.DotNetBar.ButtonX();
			this.ucDatabaseInformation1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(531, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Database Details";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonBack
			// 
			this.buttonBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBack.Location = new System.Drawing.Point(27, 398);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(71, 23);
			this.buttonBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBack.TabIndex = 45;
			this.buttonBack.Text = "< Back";
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// buttonFinish
			// 
			this.buttonFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonFinish.Location = new System.Drawing.Point(428, 398);
			this.buttonFinish.Name = "buttonFinish";
			this.buttonFinish.Size = new System.Drawing.Size(71, 23);
			this.buttonFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonFinish.TabIndex = 46;
			this.buttonFinish.Text = "Next >";
			this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
			// 
			// ucDatabaseInformation1
			// 
			this.ucDatabaseInformation1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ucDatabaseInformation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ucDatabaseInformation1.DatabaseHelper = null;
			this.ucDatabaseInformation1.EventRaisingDisabled = false;
			this.ucDatabaseInformation1.ForeColor = System.Drawing.Color.White;
			this.ucDatabaseInformation1.Location = new System.Drawing.Point(0, 40);
			this.ucDatabaseInformation1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ucDatabaseInformation1.Name = "ucDatabaseInformation1";
			this.ucDatabaseInformation1.Password = "";
			this.ucDatabaseInformation1.Port = 1433;
			this.ucDatabaseInformation1.SelectedDatabaseType = ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.Unknown;
			this.ucDatabaseInformation1.SelectedServerName = ".";
			this.ucDatabaseInformation1.ServiceName = "";
			this.ucDatabaseInformation1.Size = new System.Drawing.Size(531, 325);
			this.ucDatabaseInformation1.TabIndex = 0;
			this.ucDatabaseInformation1.UseIntegratedSecurity = true;
			this.ucDatabaseInformation1.Username = "sa";
			this.ucDatabaseInformation1.UsingDatabaseFile = false;
			// 
			// LoadExistingDatabase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.buttonFinish);
			this.Controls.Add(this.buttonBack);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucDatabaseInformation1);
			this.Name = "LoadExistingDatabase";
			this.Size = new System.Drawing.Size(531, 435);
			this.ResumeLayout(false);

		}

		#endregion

		private ArchAngel.Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation ucDatabaseInformation1;
		private System.Windows.Forms.Label label1;
		private DevComponents.DotNetBar.ButtonX buttonBack;
		private DevComponents.DotNetBar.ButtonX buttonFinish;
	}
}
