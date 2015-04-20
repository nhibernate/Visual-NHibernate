using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArchAngel.Workbench
{
	public partial class FormSplash : Form
	{
		private TimeSpan _MinimumDuration = TimeSpan.FromSeconds(0);
		private bool _MinimumDurationCompleted;
		private bool _WaitingForTimer;
		private System.Windows.Forms.Timer _Timer;

		public FormSplash()
		{
			InitializeComponent();
			labelVersion.Text = string.Format("Version {0}  ", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

			if (!string.IsNullOrEmpty(Branding.SplashImageFile) && File.Exists(Branding.SplashImageFile))
			{
				labelVersion.Dock = DockStyle.Top;
				pictureBox1.Image = new Bitmap(Branding.SplashImageFile);
				pictureBox1.Dock = DockStyle.None;
				pictureBox1.Left = 0;
				pictureBox1.Top = labelVersion.Bottom + 1;
				Label linkCopyright = new Label();
				linkCopyright.Text = "Copyright © 2004-2010 Slyce Software Limited";
				linkCopyright.Font = new Font(new FontFamily("Verdana"), 8, FontStyle.Italic);
				this.Width = pictureBox1.Width;
				this.Height = pictureBox1.Height + labelVersion.Height + linkCopyright.Height;
				this.Controls.Add(linkCopyright);
				linkCopyright.Dock = DockStyle.Bottom;
				linkCopyright.TextAlign = ContentAlignment.MiddleRight;
			}
			else
			{
				pictureBox1.Width = pictureBox1.Image.Width;
				pictureBox1.Height = pictureBox1.Image.Height;
				this.Width = pictureBox1.Width;
				this.Height = pictureBox1.Height;
			}
		}

		public TimeSpan MinimumDuration
		{
			get { return _MinimumDuration; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", value, "value must be greater than or equal to TimeSpan.Zero");

				_MinimumDuration = value;
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (_Timer != null)
					{
						_Timer.Dispose();
						_Timer.Tick -= new EventHandler(OnTimerTick);
						_Timer = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (Visible && _MinimumDuration.TotalMilliseconds > 0)
			{
				_MinimumDurationCompleted = false;
				_WaitingForTimer = false;

				_Timer = new System.Windows.Forms.Timer();
				_Timer.Tick += new EventHandler(OnTimerTick);
				_Timer.Interval = (int)_MinimumDuration.TotalMilliseconds;
				_Timer.Start();
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (_MinimumDuration > TimeSpan.Zero && !_MinimumDurationCompleted)
			{
				// Waiting for the timer to finish
				_WaitingForTimer = true;
				e.Cancel = true;
			}
			else
			{
				base.OnClosing(e);
			}
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			_Timer.Stop();
			_MinimumDurationCompleted = true;

			if (_WaitingForTimer)
			{
				Close();
			}
		}

		private void buttonHideMe_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}
	}
}