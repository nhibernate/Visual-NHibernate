using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	[DotfuscatorDoNotRename]
	[Serializable]
	public partial class DistinctStatus : Panel
	{
		private ArrayList DescriptionLabels = new ArrayList();
		private ArrayList StatusLabels = new ArrayList();
		private ArrayList ProgressBars = new ArrayList();
		public ArrayList CheckBoxes = new ArrayList();
		private Panel Panel1 = new Panel();
		private Slyce.Common.CrossThreadHelper CrossThreadHelper;

		public DistinctStatus()
		{
			CrossThreadHelper = new CrossThreadHelper(this);
			//InitializeComponent();
			EnableDoubleBuffering();
			Panel1.Dock = DockStyle.Fill;
			Panel1.AutoScroll = true;
			Panel1.HorizontalScroll.Visible = false;
			Panel1.Parent = this;
			this.Controls.Add(Panel1);
			LayoutControls();
			Panel1.HorizontalScroll.Visible = false;
		}

		private void EnableDoubleBuffering()
		{
			// Set the value of the double-buffering style bits to true.
			this.SetStyle(ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint,
				true);
			this.UpdateStyles();
		}

		[DotfuscatorDoNotRename]
		public void Reset()
		{
			for (int i = 0; i < ProgressBars.Count; i++)
			{
				((ProgressBar)ProgressBars[i]).Value = 0;
				((Label)StatusLabels[i]).Visible = false;
				((Label)DescriptionLabels[i]).Visible = false;
			}
		}

		[DotfuscatorDoNotRename]
		public void SetCheckboxVisibility(string name, bool val)
		{
			name = name.Replace("_", " ");

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				CheckBox checkbox = (CheckBox)CheckBoxes[i];

				if ((string)checkbox.Tag == name)
				{
					checkbox.Enabled = val;
					break;
				}
			}
		}

		[DotfuscatorDoNotRename]
		public void SetCheckState(string name, bool checkState)
		{
			name = name.Replace("_", " ");

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				if ((string)((CheckBox)CheckBoxes[i]).Tag == name)
				{
					((CheckBox)CheckBoxes[i]).Checked = checkState;
					break;
				}
			}
		}

		[DotfuscatorDoNotRename]
		public void SetProgressBarMax(string name, int val)
		{
			name = name.Replace("_", " ");

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				if ((string)((CheckBox)CheckBoxes[i]).Tag == name)
				{
					((ProgressBar)ProgressBars[i]).Maximum = val;
					break;
				}
			}
		}

		/// <summary>
		/// Clears all controls and data.
		/// </summary>
		[DotfuscatorDoNotRename]
		public void Clear()
		{
			Panel1.Controls.Clear();

			foreach (Control control in DescriptionLabels)
			{
				control.Dispose();
			}
			foreach (Control control in StatusLabels)
			{
				control.Dispose();
			}
			foreach (Control control in ProgressBars)
			{
				control.Dispose();
			}
			foreach (Control control in CheckBoxes)
			{
				control.Dispose();
			}
			DescriptionLabels.Clear();
			StatusLabels.Clear();
			ProgressBars.Clear();
			CheckBoxes.Clear();

			//for (int i = 0; i < CheckBoxes.Count; i++)
			//{
			//    CheckBox chk = (CheckBox)CheckBoxes[i];
			//    ProgressBar progBar = (ProgressBar)ProgressBars[i];
			//    Label descLabel = (Label)DescriptionLabels[i];
			//    Label statusLabel = (Label)StatusLabels[i];
			//    statusLabel.Visible = false;
			//    progBar.Visible = false;
			//    descLabel.Visible = false;
			//    descLabel.Text = "";
			//    this.Refresh();
			//}
		}

		[DotfuscatorDoNotRename]
		public void LayoutControls()
		{
			int maxWidth = GetWidestCheckbox() + 5;

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				CheckBox chk = (CheckBox)CheckBoxes[i];
				ProgressBar progBar = (ProgressBar)ProgressBars[i];
				Label descLabel = (Label)DescriptionLabels[i];
				Label statusLabel = (Label)StatusLabels[i];
				progBar.Left = maxWidth;
				descLabel.Left = progBar.Right + 5;
				statusLabel.Left = maxWidth;
				statusLabel.Visible = false;
				statusLabel.Width = Panel1.Width - statusLabel.Left - 5;
				descLabel.Width = Panel1.Width - descLabel.Left - 5;
				//progBar.Visible = chk.Checked;
				descLabel.Visible = chk.Checked;
				this.Refresh();
			}
			this.Invalidate();
		}

		private int GetWidestCheckbox()
		{
			int max = 0;

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				max = ((CheckBox)CheckBoxes[i]).Width > max ? ((CheckBox)CheckBoxes[i]).Width : max;
			}
			return max;
		}

		[DotfuscatorDoNotRename]
		public void Add(string name, int max, bool isSelected)
		{
			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				if (((CheckBox)CheckBoxes[i]).Text == name)
				{
					return;
				}
			}
			Font boldFont = new Font(this.Font, FontStyle.Bold);

			// ProgressBar
			ProgressBar progressBar = new ProgressBar();
			progressBar.Minimum = 0;
			progressBar.Maximum = max;
			progressBar.Step = 1;
			progressBar.Width = 100;
			progressBar.Left = 100;
			progressBar.Style = ProgressBarStyle.Continuous;
			progressBar.Visible = false;

			// Status Label
			Label statusLabel = new Label();
			statusLabel.Font = boldFont;
			statusLabel.Text = "";
			statusLabel.AutoSize = false;
			statusLabel.Left = 100;
			statusLabel.Width = Panel1.Width - statusLabel.Left - 5;

			// CheckBox
			CheckBox checkBox = new CheckBox();
			checkBox.AutoSize = true;
			checkBox.Left = 0;
			checkBox.Text = name;
			checkBox.Tag = name;
			checkBox.Checked = isSelected;

			// Description Label
			Label descriptionLabel = new Label();
			descriptionLabel.Text = "";
			descriptionLabel.AutoSize = false;
			descriptionLabel.Left = 100;
			descriptionLabel.Width = Panel1.Width - descriptionLabel.Left - 100;

			if (CheckBoxes.Count > 0)
			{
				Label prevLabel = (Label)DescriptionLabels[DescriptionLabels.Count - 1];
				checkBox.Top = prevLabel.Top + prevLabel.Height;
			}
			else
			{
				checkBox.Top = 0;
			}
			progressBar.Top = checkBox.Top;
			statusLabel.Top = progressBar.Top;
			descriptionLabel.Top = checkBox.Top;//.Bottom + 5;
			Panel1.Controls.Add(checkBox);
			Panel1.Controls.Add(progressBar);
			Panel1.Controls.Add(descriptionLabel);
			Panel1.Controls.Add(statusLabel);
			DescriptionLabels.Add(descriptionLabel);
			StatusLabels.Add(statusLabel);
			ProgressBars.Add(progressBar);
			CheckBoxes.Add(checkBox);
			//Panel1.Height = descriptionLabel.Bottom + 5;
			this.Refresh();
		}

		protected override void OnResize(EventArgs eventargs)
		{
			try
			{
				Slyce.Common.Utility.SuspendPainting(this);
				PerformLayout();
				base.OnResize(eventargs);
				LayoutControls();
			}
			finally
			{
				Slyce.Common.Utility.ResumePainting(this);
			}
		}

		[DotfuscatorDoNotRename]
		public void IncrementProgress(string name)
		{
			name = name.Replace("_", " ");

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				if ((string)((CheckBox)CheckBoxes[i]).Tag == name)
				{
					IncrementProgress(i);
					break;
				}
			}
		}

		[DotfuscatorDoNotRename]
		public void IncrementProgress(int i)
		{
			ProgressBar progBar = (ProgressBar)ProgressBars[i];
			Label statusLabel = (Label)StatusLabels[i];
			Label descriptionLabel = (Label)DescriptionLabels[i];

			if (progBar.InvokeRequired)
			{
				if (progBar.Value >= progBar.Minimum)
				{
					CrossThreadHelper.SetCrossThreadProperty(progBar, "Visible", true);
					CrossThreadHelper.SetCrossThreadProperty(statusLabel, "Visible", false);
					CrossThreadHelper.SetCrossThreadProperty(descriptionLabel, "Visible", false);
				}
				CrossThreadHelper.CallCrossThreadMethod(progBar, "Increment", new object[] { 1 });

				if (progBar.Value == progBar.Maximum)
				{
					CrossThreadHelper.SetCrossThreadProperty(progBar, "Visible", false);
					CrossThreadHelper.SetCrossThreadProperty(descriptionLabel, "Visible", false);

					if (progBar.Value != 1)
					{
						CrossThreadHelper.SetCrossThreadProperty(statusLabel, "Text", string.Format("Complete - {0} files.", progBar.Value));
					}
					else
					{
						CrossThreadHelper.SetCrossThreadProperty(statusLabel, "Text", "Complete - 1 file.");
					}
					CrossThreadHelper.SetCrossThreadProperty(statusLabel, "Visible", true);
				}
			}
			else
			{
				if (progBar.Value == progBar.Minimum)
				{

					progBar.Visible = true;
					statusLabel.Visible = false;
					descriptionLabel.Visible = true;
				}
				progBar.Increment(1);

				if (progBar.Value == progBar.Maximum)
				{
					progBar.Visible = false;
					descriptionLabel.Visible = false;

					if (progBar.Value != 1)
					{
						statusLabel.Text = string.Format("Complete - {0} files.", progBar.Value);
					}
					else
					{
						statusLabel.Text = "Complete - 1 file.";
					}
					statusLabel.Visible = true;
				}
			}
		}

		[DotfuscatorDoNotRename]
		public bool IsOutputEnabled(string name)
		{
			name = name.Replace("_", " ");

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				CheckBox chk = (CheckBox)CheckBoxes[i];

				if ((string)chk.Tag == name)
				{
					return chk.Checked;
				}
			}
			throw new Exception("Output checkbox not found: " + name);
		}

		[DotfuscatorDoNotRename]
		public void SetDescription(string name, string val)
		{
			name = name.Replace("_", " ");

			for (int i = 0; i < CheckBoxes.Count; i++)
			{
				if ((string)((CheckBox)CheckBoxes[i]).Tag == name)
				{
					Label label = (Label)DescriptionLabels[i];
					ProgressBar progBar = (ProgressBar)ProgressBars[i];
					label.Text = string.Format("({0}/{1}) {2}", progBar.Value, progBar.Maximum, val);
					//label.Visible = true;
					label.Refresh();
					break;
				}
			}
		}

	}
}
