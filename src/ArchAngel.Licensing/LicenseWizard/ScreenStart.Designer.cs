namespace ArchAngel.Licensing.LicenseWizard
{
	partial class ScreenStart
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenStart));
			this.lblMessage = new System.Windows.Forms.Label();
			this.pnlRemainingDays = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.labelWarning = new System.Windows.Forms.Label();
			this.textBoxSerial = new System.Windows.Forms.TextBox();
			this.textBoxLicenseFile = new System.Windows.Forms.TextBox();
			this.buttonBrowse = new DevComponents.DotNetBar.ButtonX();
			this.panelMain = new DevComponents.DotNetBar.PanelEx();
			this.labelEmail = new DevComponents.DotNetBar.LabelX();
			this.textBoxEmail = new System.Windows.Forms.TextBox();
			this.buttonExtendTrial = new DevComponents.DotNetBar.ButtonX();
			this.buttonBuyNow = new DevComponents.DotNetBar.ButtonX();
			this.buttonInstallLicenseFile = new DevComponents.DotNetBar.ButtonX();
			this.buttonContinue = new DevComponents.DotNetBar.ButtonX();
			this.labelSerial = new DevComponents.DotNetBar.LabelX();
			this.buttonGetNewTrialLicenseManual = new DevComponents.DotNetBar.ButtonX();
			this.buttonCopyUrl = new DevComponents.DotNetBar.ButtonItem();
			this.buttonGetNewTrialLicenseAuto = new DevComponents.DotNetBar.ButtonX();
			this.labelDescription = new DevComponents.DotNetBar.LabelX();
			this.labelHeading = new DevComponents.DotNetBar.LabelX();
			this.panelContinueTrial = new DevComponents.DotNetBar.PanelEx();
			this.pictureBoxStartTrial = new System.Windows.Forms.PictureBox();
			this.panelSerial = new DevComponents.DotNetBar.PanelEx();
			this.pictureBoxSerial = new System.Windows.Forms.PictureBox();
			this.panelLicenseFile = new DevComponents.DotNetBar.PanelEx();
			this.pictureBoxLicense = new System.Windows.Forms.PictureBox();
			this.panelBuy = new DevComponents.DotNetBar.PanelEx();
			this.pictureBoxPurchase = new System.Windows.Forms.PictureBox();
			this.panelExtendTrial = new DevComponents.DotNetBar.PanelEx();
			this.pictureBoxExtend = new System.Windows.Forms.PictureBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.buttonRefreshLicense = new DevComponents.DotNetBar.ButtonX();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonRemoveLicense = new DevComponents.DotNetBar.ButtonX();
			this.panelMain.SuspendLayout();
			this.panelContinueTrial.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStartTrial)).BeginInit();
			this.panelSerial.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSerial)).BeginInit();
			this.panelLicenseFile.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLicense)).BeginInit();
			this.panelBuy.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPurchase)).BeginInit();
			this.panelExtendTrial.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExtend)).BeginInit();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.Location = new System.Drawing.Point(3, 302);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(885, 23);
			this.lblMessage.TabIndex = 11;
			this.lblMessage.Text = "Your 30-day trial period has xx days remaining";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// pnlRemainingDays
			// 
			this.pnlRemainingDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlRemainingDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlRemainingDays.Location = new System.Drawing.Point(25, 317);
			this.pnlRemainingDays.Margin = new System.Windows.Forms.Padding(2);
			this.pnlRemainingDays.Name = "pnlRemainingDays";
			this.pnlRemainingDays.Size = new System.Drawing.Size(840, 17);
			this.pnlRemainingDays.TabIndex = 19;
			this.pnlRemainingDays.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRemainingDays_Paint);
			// 
			// labelWarning
			// 
			this.labelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWarning.ForeColor = System.Drawing.Color.Red;
			this.labelWarning.Location = new System.Drawing.Point(6, 279);
			this.labelWarning.Name = "labelWarning";
			this.labelWarning.Size = new System.Drawing.Size(885, 23);
			this.labelWarning.TabIndex = 22;
			this.labelWarning.Text = "Warning text";
			this.labelWarning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textBoxSerial
			// 
			this.textBoxSerial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSerial.Location = new System.Drawing.Point(13, 184);
			this.textBoxSerial.Name = "textBoxSerial";
			this.textBoxSerial.Size = new System.Drawing.Size(547, 20);
			this.textBoxSerial.TabIndex = 23;
			this.textBoxSerial.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxLicenseFile
			// 
			this.textBoxLicenseFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLicenseFile.Location = new System.Drawing.Point(97, 184);
			this.textBoxLicenseFile.Name = "textBoxLicenseFile";
			this.textBoxLicenseFile.Size = new System.Drawing.Size(498, 20);
			this.textBoxLicenseFile.TabIndex = 25;
			this.textBoxLicenseFile.TextChanged += new System.EventHandler(this.textBoxLicenseFile_TextChanged);
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.Image")));
			this.buttonBrowse.Location = new System.Drawing.Point(601, 184);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(27, 20);
			this.buttonBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBrowse.TabIndex = 26;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// panelMain
			// 
			this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelMain.Controls.Add(this.labelEmail);
			this.panelMain.Controls.Add(this.textBoxEmail);
			this.panelMain.Controls.Add(this.buttonExtendTrial);
			this.panelMain.Controls.Add(this.buttonBuyNow);
			this.panelMain.Controls.Add(this.buttonInstallLicenseFile);
			this.panelMain.Controls.Add(this.buttonContinue);
			this.panelMain.Controls.Add(this.labelSerial);
			this.panelMain.Controls.Add(this.buttonGetNewTrialLicenseManual);
			this.panelMain.Controls.Add(this.buttonGetNewTrialLicenseAuto);
			this.panelMain.Controls.Add(this.labelDescription);
			this.panelMain.Controls.Add(this.labelHeading);
			this.panelMain.Controls.Add(this.textBoxSerial);
			this.panelMain.Controls.Add(this.buttonBrowse);
			this.panelMain.Controls.Add(this.textBoxLicenseFile);
			this.panelMain.Location = new System.Drawing.Point(191, 21);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(692, 221);
			this.panelMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelMain.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelMain.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelMain.Style.CornerDiameter = 5;
			this.panelMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelMain.Style.GradientAngle = 90;
			this.panelMain.TabIndex = 62;
			// 
			// labelEmail
			// 
			this.labelEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelEmail.AutoSize = true;
			// 
			// 
			// 
			this.labelEmail.BackgroundStyle.Class = "";
			this.labelEmail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelEmail.ForeColor = System.Drawing.Color.White;
			this.labelEmail.Location = new System.Drawing.Point(13, 147);
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size(33, 15);
			this.labelEmail.TabIndex = 57;
			this.labelEmail.Text = "Email:";
			this.labelEmail.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelEmail.WordWrap = true;
			// 
			// textBoxEmail
			// 
			this.textBoxEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxEmail.Location = new System.Drawing.Point(52, 144);
			this.textBoxEmail.Name = "textBoxEmail";
			this.textBoxEmail.Size = new System.Drawing.Size(576, 20);
			this.textBoxEmail.TabIndex = 56;
			// 
			// buttonExtendTrial
			// 
			this.buttonExtendTrial.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonExtendTrial.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonExtendTrial.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonExtendTrial.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonExtendTrial.HoverImage")));
			this.buttonExtendTrial.Image = ((System.Drawing.Image)(resources.GetObject("buttonExtendTrial.Image")));
			this.buttonExtendTrial.Location = new System.Drawing.Point(634, 168);
			this.buttonExtendTrial.Name = "buttonExtendTrial";
			this.buttonExtendTrial.Size = new System.Drawing.Size(103, 22);
			this.buttonExtendTrial.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonExtendTrial.TabIndex = 55;
			this.buttonExtendTrial.Text = "Create request";
			this.buttonExtendTrial.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonExtendTrial.Click += new System.EventHandler(this.buttonExtendTrial_Click);
			// 
			// buttonBuyNow
			// 
			this.buttonBuyNow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBuyNow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBuyNow.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonBuyNow.Image = ((System.Drawing.Image)(resources.GetObject("buttonBuyNow.Image")));
			this.buttonBuyNow.Location = new System.Drawing.Point(525, 168);
			this.buttonBuyNow.Name = "buttonBuyNow";
			this.buttonBuyNow.Size = new System.Drawing.Size(103, 22);
			this.buttonBuyNow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBuyNow.TabIndex = 54;
			this.buttonBuyNow.Text = "Buy now";
			this.buttonBuyNow.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonBuyNow.Click += new System.EventHandler(this.buttonBuyNow_Click);
			// 
			// buttonInstallLicenseFile
			// 
			this.buttonInstallLicenseFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonInstallLicenseFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonInstallLicenseFile.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonInstallLicenseFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonInstallLicenseFile.Image")));
			this.buttonInstallLicenseFile.Location = new System.Drawing.Point(416, 168);
			this.buttonInstallLicenseFile.Name = "buttonInstallLicenseFile";
			this.buttonInstallLicenseFile.Size = new System.Drawing.Size(103, 22);
			this.buttonInstallLicenseFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonInstallLicenseFile.TabIndex = 53;
			this.buttonInstallLicenseFile.Text = "Install";
			this.buttonInstallLicenseFile.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonInstallLicenseFile.Click += new System.EventHandler(this.buttonInstallLicenseFile_Click);
			// 
			// buttonContinue
			// 
			this.buttonContinue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonContinue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonContinue.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonContinue.Image = ((System.Drawing.Image)(resources.GetObject("buttonContinue.Image")));
			this.buttonContinue.Location = new System.Drawing.Point(292, 168);
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.Size = new System.Drawing.Size(103, 22);
			this.buttonContinue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonContinue.TabIndex = 52;
			this.buttonContinue.Text = "Continue";
			this.buttonContinue.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
			// 
			// labelSerial
			// 
			this.labelSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSerial.AutoSize = true;
			// 
			// 
			// 
			this.labelSerial.BackgroundStyle.Class = "";
			this.labelSerial.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelSerial.ForeColor = System.Drawing.Color.White;
			this.labelSerial.Location = new System.Drawing.Point(134, 164);
			this.labelSerial.Name = "labelSerial";
			this.labelSerial.Size = new System.Drawing.Size(34, 15);
			this.labelSerial.TabIndex = 51;
			this.labelSerial.Text = "Serial:";
			this.labelSerial.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelSerial.WordWrap = true;
			// 
			// buttonGetNewTrialLicenseManual
			// 
			this.buttonGetNewTrialLicenseManual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonGetNewTrialLicenseManual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonGetNewTrialLicenseManual.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonGetNewTrialLicenseManual.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetNewTrialLicenseManual.Image")));
			this.buttonGetNewTrialLicenseManual.Location = new System.Drawing.Point(183, 168);
			this.buttonGetNewTrialLicenseManual.Name = "buttonGetNewTrialLicenseManual";
			this.buttonGetNewTrialLicenseManual.Size = new System.Drawing.Size(103, 22);
			this.buttonGetNewTrialLicenseManual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonGetNewTrialLicenseManual.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonCopyUrl});
			this.buttonGetNewTrialLicenseManual.TabIndex = 50;
			this.buttonGetNewTrialLicenseManual.Text = "Visit website";
			this.buttonGetNewTrialLicenseManual.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonGetNewTrialLicenseManual.Click += new System.EventHandler(this.buttonGetNewTrialLicenseManual_Click);
			// 
			// buttonCopyUrl
			// 
			this.buttonCopyUrl.GlobalItem = false;
			this.buttonCopyUrl.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopyUrl.Image")));
			this.buttonCopyUrl.Name = "buttonCopyUrl";
			this.buttonCopyUrl.Text = "Copy URL";
			this.buttonCopyUrl.Click += new System.EventHandler(this.buttonCopyUrl_Click);
			// 
			// buttonGetNewTrialLicenseAuto
			// 
			this.buttonGetNewTrialLicenseAuto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonGetNewTrialLicenseAuto.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonGetNewTrialLicenseAuto.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonGetNewTrialLicenseAuto.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetNewTrialLicenseAuto.Image")));
			this.buttonGetNewTrialLicenseAuto.Location = new System.Drawing.Point(25, 168);
			this.buttonGetNewTrialLicenseAuto.Name = "buttonGetNewTrialLicenseAuto";
			this.buttonGetNewTrialLicenseAuto.Size = new System.Drawing.Size(103, 22);
			this.buttonGetNewTrialLicenseAuto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonGetNewTrialLicenseAuto.TabIndex = 49;
			this.buttonGetNewTrialLicenseAuto.Text = "Unlock";
			this.buttonGetNewTrialLicenseAuto.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonGetNewTrialLicenseAuto.Click += new System.EventHandler(this.buttonGetNewTrialLicenseAuto_Click);
			// 
			// labelDescription
			// 
			this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelDescription.BackgroundStyle.Class = "";
			this.labelDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelDescription.ForeColor = System.Drawing.Color.White;
			this.labelDescription.Location = new System.Drawing.Point(25, 48);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(641, 114);
			this.labelDescription.TabIndex = 48;
			this.labelDescription.Text = "labelDescription";
			this.labelDescription.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelDescription.WordWrap = true;
			// 
			// labelHeading
			// 
			this.labelHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelHeading.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelHeading.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelHeading.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelHeading.BackgroundStyle.BorderBottomWidth = 1;
			this.labelHeading.BackgroundStyle.Class = "";
			this.labelHeading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelHeading.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHeading.ForeColor = System.Drawing.Color.White;
			this.labelHeading.Location = new System.Drawing.Point(25, 9);
			this.labelHeading.Name = "labelHeading";
			this.labelHeading.SingleLineColor = System.Drawing.Color.White;
			this.labelHeading.Size = new System.Drawing.Size(641, 23);
			this.labelHeading.TabIndex = 47;
			this.labelHeading.Text = "Getting Started";
			// 
			// panelContinueTrial
			// 
			this.panelContinueTrial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelContinueTrial.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelContinueTrial.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelContinueTrial.Controls.Add(this.pictureBoxStartTrial);
			this.panelContinueTrial.Cursor = System.Windows.Forms.Cursors.Hand;
			this.panelContinueTrial.Location = new System.Drawing.Point(6, 21);
			this.panelContinueTrial.Name = "panelContinueTrial";
			this.panelContinueTrial.Size = new System.Drawing.Size(860, 41);
			this.panelContinueTrial.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelContinueTrial.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelContinueTrial.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelContinueTrial.Style.CornerDiameter = 5;
			this.panelContinueTrial.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelContinueTrial.Style.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelContinueTrial.Style.ForeColor.Color = System.Drawing.Color.White;
			this.panelContinueTrial.Style.GradientAngle = 90;
			this.panelContinueTrial.Style.MarginLeft = 40;
			this.panelContinueTrial.TabIndex = 60;
			this.panelContinueTrial.Text = "Start trial";
			this.panelContinueTrial.Click += new System.EventHandler(this.panelContinueTrial_Click);
			// 
			// pictureBoxStartTrial
			// 
			this.pictureBoxStartTrial.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStartTrial.Image")));
			this.pictureBoxStartTrial.Location = new System.Drawing.Point(8, 8);
			this.pictureBoxStartTrial.Name = "pictureBoxStartTrial";
			this.pictureBoxStartTrial.Size = new System.Drawing.Size(24, 24);
			this.pictureBoxStartTrial.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxStartTrial.TabIndex = 0;
			this.pictureBoxStartTrial.TabStop = false;
			// 
			// panelSerial
			// 
			this.panelSerial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelSerial.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelSerial.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelSerial.Controls.Add(this.pictureBoxSerial);
			this.panelSerial.Cursor = System.Windows.Forms.Cursors.Hand;
			this.panelSerial.Location = new System.Drawing.Point(6, 63);
			this.panelSerial.Name = "panelSerial";
			this.panelSerial.Size = new System.Drawing.Size(860, 41);
			this.panelSerial.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelSerial.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelSerial.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelSerial.Style.CornerDiameter = 5;
			this.panelSerial.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelSerial.Style.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelSerial.Style.ForeColor.Color = System.Drawing.Color.White;
			this.panelSerial.Style.GradientAngle = 90;
			this.panelSerial.Style.MarginLeft = 40;
			this.panelSerial.TabIndex = 61;
			this.panelSerial.Text = "Serial number";
			this.panelSerial.Click += new System.EventHandler(this.panelSerial_Click);
			// 
			// pictureBoxSerial
			// 
			this.pictureBoxSerial.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSerial.Image")));
			this.pictureBoxSerial.Location = new System.Drawing.Point(8, 8);
			this.pictureBoxSerial.Name = "pictureBoxSerial";
			this.pictureBoxSerial.Size = new System.Drawing.Size(24, 24);
			this.pictureBoxSerial.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxSerial.TabIndex = 1;
			this.pictureBoxSerial.TabStop = false;
			// 
			// panelLicenseFile
			// 
			this.panelLicenseFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelLicenseFile.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelLicenseFile.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelLicenseFile.Controls.Add(this.pictureBoxLicense);
			this.panelLicenseFile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.panelLicenseFile.Location = new System.Drawing.Point(6, 105);
			this.panelLicenseFile.Name = "panelLicenseFile";
			this.panelLicenseFile.Size = new System.Drawing.Size(860, 41);
			this.panelLicenseFile.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelLicenseFile.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelLicenseFile.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelLicenseFile.Style.CornerDiameter = 5;
			this.panelLicenseFile.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelLicenseFile.Style.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelLicenseFile.Style.ForeColor.Color = System.Drawing.Color.White;
			this.panelLicenseFile.Style.GradientAngle = 90;
			this.panelLicenseFile.Style.MarginLeft = 40;
			this.panelLicenseFile.TabIndex = 63;
			this.panelLicenseFile.Text = "License file";
			this.panelLicenseFile.Click += new System.EventHandler(this.panelLicenseFile_Click);
			// 
			// pictureBoxLicense
			// 
			this.pictureBoxLicense.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLicense.Image")));
			this.pictureBoxLicense.Location = new System.Drawing.Point(8, 8);
			this.pictureBoxLicense.Name = "pictureBoxLicense";
			this.pictureBoxLicense.Size = new System.Drawing.Size(24, 24);
			this.pictureBoxLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxLicense.TabIndex = 1;
			this.pictureBoxLicense.TabStop = false;
			// 
			// panelBuy
			// 
			this.panelBuy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelBuy.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelBuy.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelBuy.Controls.Add(this.pictureBoxPurchase);
			this.panelBuy.Cursor = System.Windows.Forms.Cursors.Hand;
			this.panelBuy.Location = new System.Drawing.Point(6, 147);
			this.panelBuy.Name = "panelBuy";
			this.panelBuy.Size = new System.Drawing.Size(860, 41);
			this.panelBuy.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelBuy.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelBuy.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelBuy.Style.CornerDiameter = 5;
			this.panelBuy.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelBuy.Style.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelBuy.Style.ForeColor.Color = System.Drawing.Color.White;
			this.panelBuy.Style.GradientAngle = 90;
			this.panelBuy.Style.MarginLeft = 40;
			this.panelBuy.TabIndex = 64;
			this.panelBuy.Text = "Purchase";
			this.panelBuy.Click += new System.EventHandler(this.panelBuy_Click);
			// 
			// pictureBoxPurchase
			// 
			this.pictureBoxPurchase.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPurchase.Image")));
			this.pictureBoxPurchase.Location = new System.Drawing.Point(8, 8);
			this.pictureBoxPurchase.Name = "pictureBoxPurchase";
			this.pictureBoxPurchase.Size = new System.Drawing.Size(24, 24);
			this.pictureBoxPurchase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxPurchase.TabIndex = 1;
			this.pictureBoxPurchase.TabStop = false;
			// 
			// panelExtendTrial
			// 
			this.panelExtendTrial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelExtendTrial.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelExtendTrial.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelExtendTrial.Controls.Add(this.pictureBoxExtend);
			this.panelExtendTrial.Cursor = System.Windows.Forms.Cursors.Hand;
			this.panelExtendTrial.Location = new System.Drawing.Point(6, 189);
			this.panelExtendTrial.Name = "panelExtendTrial";
			this.panelExtendTrial.Size = new System.Drawing.Size(860, 41);
			this.panelExtendTrial.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelExtendTrial.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.panelExtendTrial.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelExtendTrial.Style.CornerDiameter = 5;
			this.panelExtendTrial.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.panelExtendTrial.Style.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelExtendTrial.Style.ForeColor.Color = System.Drawing.Color.White;
			this.panelExtendTrial.Style.GradientAngle = 90;
			this.panelExtendTrial.Style.MarginLeft = 40;
			this.panelExtendTrial.TabIndex = 65;
			this.panelExtendTrial.Text = "Extend trial";
			this.panelExtendTrial.Click += new System.EventHandler(this.panelExtendTrial_Click);
			// 
			// pictureBoxExtend
			// 
			this.pictureBoxExtend.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxExtend.Image")));
			this.pictureBoxExtend.Location = new System.Drawing.Point(8, 8);
			this.pictureBoxExtend.Name = "pictureBoxExtend";
			this.pictureBoxExtend.Size = new System.Drawing.Size(24, 24);
			this.pictureBoxExtend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxExtend.TabIndex = 1;
			this.pictureBoxExtend.TabStop = false;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "audio_player_24.png");
			this.imageList1.Images.SetKeyName(1, "audio_player_24_d.png");
			this.imageList1.Images.SetKeyName(2, "change_uppercase_24.png");
			this.imageList1.Images.SetKeyName(3, "change_uppercase_24_d.png");
			this.imageList1.Images.SetKeyName(4, "doc_unlock_24.png");
			this.imageList1.Images.SetKeyName(5, "doc_unlock_24_d.png");
			this.imageList1.Images.SetKeyName(6, "currency_dollar_24.png");
			this.imageList1.Images.SetKeyName(7, "currency_dollar_24_d.png");
			this.imageList1.Images.SetKeyName(8, "history_24.png");
			this.imageList1.Images.SetKeyName(9, "history_24_d.png");
			// 
			// buttonRefreshLicense
			// 
			this.buttonRefreshLicense.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRefreshLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRefreshLicense.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRefreshLicense.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonRefreshLicense.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRefreshLicense.HoverImage")));
			this.buttonRefreshLicense.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshLicense.Image")));
			this.buttonRefreshLicense.Location = new System.Drawing.Point(770, 295);
			this.buttonRefreshLicense.Name = "buttonRefreshLicense";
			this.buttonRefreshLicense.Size = new System.Drawing.Size(68, 20);
			this.buttonRefreshLicense.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.buttonRefreshLicense, new DevComponents.DotNetBar.SuperTooltipInfo("Renew license", "", "If your trial has been extended, download the fresh version of your license.", ((System.Drawing.Image)(resources.GetObject("buttonRefreshLicense.SuperTooltip"))), null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonRefreshLicense.TabIndex = 66;
			this.buttonRefreshLicense.Text = "Renew";
			this.buttonRefreshLicense.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonRefreshLicense.Visible = false;
			this.buttonRefreshLicense.Click += new System.EventHandler(this.buttonRefreshLicense_Click);
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonRemoveLicense
			// 
			this.buttonRemoveLicense.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRemoveLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveLicense.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRemoveLicense.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonRemoveLicense.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRemoveLicense.HoverImage")));
			this.buttonRemoveLicense.Image = ((System.Drawing.Image)(resources.GetObject("buttonRemoveLicense.Image")));
			this.buttonRemoveLicense.Location = new System.Drawing.Point(841, 295);
			this.buttonRemoveLicense.Name = "buttonRemoveLicense";
			this.buttonRemoveLicense.Size = new System.Drawing.Size(24, 20);
			this.buttonRemoveLicense.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.buttonRemoveLicense, new DevComponents.DotNetBar.SuperTooltipInfo("Remove license", "", "Remove the existing license from your machine.", ((System.Drawing.Image)(resources.GetObject("buttonRemoveLicense.SuperTooltip"))), null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonRemoveLicense.TabIndex = 67;
			this.buttonRemoveLicense.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonRemoveLicense.Visible = false;
			this.buttonRemoveLicense.Click += new System.EventHandler(this.buttonRemoveLicense_Click);
			// 
			// ScreenStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.buttonRemoveLicense);
			this.Controls.Add(this.buttonRefreshLicense);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelContinueTrial);
			this.Controls.Add(this.panelSerial);
			this.Controls.Add(this.labelWarning);
			this.Controls.Add(this.pnlRemainingDays);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.panelLicenseFile);
			this.Controls.Add(this.panelBuy);
			this.Controls.Add(this.panelExtendTrial);
			this.Name = "ScreenStart";
			this.Size = new System.Drawing.Size(891, 344);
			this.panelMain.ResumeLayout(false);
			this.panelMain.PerformLayout();
			this.panelContinueTrial.ResumeLayout(false);
			this.panelContinueTrial.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxStartTrial)).EndInit();
			this.panelSerial.ResumeLayout(false);
			this.panelSerial.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxSerial)).EndInit();
			this.panelLicenseFile.ResumeLayout(false);
			this.panelLicenseFile.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLicense)).EndInit();
			this.panelBuy.ResumeLayout(false);
			this.panelBuy.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPurchase)).EndInit();
			this.panelExtendTrial.ResumeLayout(false);
			this.panelExtendTrial.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxExtend)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Panel pnlRemainingDays;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label labelWarning;
		private System.Windows.Forms.TextBox textBoxSerial;
		private System.Windows.Forms.TextBox textBoxLicenseFile;
		private DevComponents.DotNetBar.ButtonX buttonBrowse;
		private DevComponents.DotNetBar.PanelEx panelMain;
		private DevComponents.DotNetBar.LabelX labelDescription;
		private DevComponents.DotNetBar.LabelX labelHeading;
		private DevComponents.DotNetBar.PanelEx panelContinueTrial;
		private DevComponents.DotNetBar.PanelEx panelSerial;
		private DevComponents.DotNetBar.PanelEx panelLicenseFile;
		private DevComponents.DotNetBar.PanelEx panelBuy;
		private DevComponents.DotNetBar.PanelEx panelExtendTrial;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private DevComponents.DotNetBar.ButtonX buttonGetNewTrialLicenseAuto;
		private DevComponents.DotNetBar.ButtonX buttonGetNewTrialLicenseManual;
		private DevComponents.DotNetBar.LabelX labelSerial;
		private DevComponents.DotNetBar.ButtonX buttonContinue;
		private DevComponents.DotNetBar.ButtonItem buttonCopyUrl;
		private DevComponents.DotNetBar.ButtonX buttonInstallLicenseFile;
		private DevComponents.DotNetBar.ButtonX buttonBuyNow;
		private System.Windows.Forms.PictureBox pictureBoxStartTrial;
		private System.Windows.Forms.PictureBox pictureBoxSerial;
		private System.Windows.Forms.PictureBox pictureBoxLicense;
		private System.Windows.Forms.PictureBox pictureBoxPurchase;
		private System.Windows.Forms.PictureBox pictureBoxExtend;
		private System.Windows.Forms.ImageList imageList1;
		private DevComponents.DotNetBar.ButtonX buttonExtendTrial;
		private DevComponents.DotNetBar.ButtonX buttonRefreshLicense;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private DevComponents.DotNetBar.ButtonX buttonRemoveLicense;
		private DevComponents.DotNetBar.LabelX labelEmail;
		private System.Windows.Forms.TextBox textBoxEmail;



	}
}
