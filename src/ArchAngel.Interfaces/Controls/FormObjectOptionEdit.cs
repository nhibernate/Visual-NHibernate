using System;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using Slyce.Common.Controls;

namespace ArchAngel.Interfaces.Controls
{
	public partial class FormVirtualPropertyEdit : Form
	{
		public IUserOption VirtualProperty;
		private Control InputControl;
		private object OriginalValue;

		public FormVirtualPropertyEdit(IUserOption virtualProperty, IScriptBaseObject iteratorObject)
		{
			InitializeComponent();
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
			ucHeading1.Text = "";
			VirtualProperty = virtualProperty;
			OriginalValue = VirtualProperty.Value;
			Interfaces.Events.ShadeMainForm();
			Populate();
		}

		private void Populate()
		{
			this.Text = "Option Edit - " + VirtualProperty.Text;
			lblText.Text = VirtualProperty.Text.Length == 0 ? "No text available. Edit template to add text." : VirtualProperty.Text;
			lblDescription.Text = VirtualProperty.Description.Length == 0 ? "No Description available. Edit template to add a description." : VirtualProperty.Description;
			lblText.Visible = true;
			lblDescription.Visible = true;
			Button btnColorBrowse = null;
			Button btnFileBrowse = null;
			Button btnDirectoryBrowse = null;

			switch (VirtualProperty.DataType.FullName)
			{
				case "System.String":
					InputControl = new TextBox();
					InputControl.Text = (string)VirtualProperty.Value;
					break;
				case "System.Int32":
					InputControl = new NumEdit();
					((NumEdit)InputControl).InputType = NumEdit.NumEditType.Integer;
					InputControl.Text = ((int)VirtualProperty.Value).ToString();
					break;
				case "System.Double":
					InputControl = new NumEdit();
					((NumEdit)InputControl).InputType = NumEdit.NumEditType.Double;
					InputControl.Text = ((double)VirtualProperty.Value).ToString();
					break;
				case "System.Drawing.Color":
					InputControl = new Label();
					int colorVal = (int)VirtualProperty.Value;
					InputControl.BackColor = Color.FromArgb(colorVal);
					btnColorBrowse = new Button();
					btnColorBrowse.Name = "btnColorBrowse";
					btnColorBrowse.Text = "...";
					btnColorBrowse.Width = 24;
					btnColorBrowse.Click += btnColorBrowse_Click;
					break;
				case "System.Boolean":
					InputControl = new CheckBox();
					((CheckBox)InputControl).Checked = (bool)VirtualProperty.Value;
					break;
				case "System.IO.FileInfo":
					InputControl = new TextBox();
					InputControl.Text = (string)VirtualProperty.Value;
					btnFileBrowse = new Button();
					btnFileBrowse.Name = "btnFileBrowse";
					btnFileBrowse.Text = "...";
					btnFileBrowse.Width = 24;
					btnFileBrowse.Click += btnFileBrowse_Click;
					break;
				case "System.IO.DirectoryInfo":
					InputControl = new TextBox();
					InputControl.Text = (string)VirtualProperty.Value;
					btnDirectoryBrowse = new Button();
					btnDirectoryBrowse.Name = "btnDirectoryBrowse";
					btnDirectoryBrowse.Text = "...";
					btnDirectoryBrowse.Width = 24;
					btnDirectoryBrowse.Click += btnDirectoryBrowse_Click;
					break;
				default:
					// Do some additional checking
					if (VirtualProperty.DataType.IsEnum)
					{
						InputControl = new ComboBox();
						((ComboBox)InputControl).Items.AddRange(Enum.GetNames(VirtualProperty.DataType));
						int index = ((ComboBox)InputControl).FindStringExact((string)VirtualProperty.Value);
						((ComboBox)InputControl).DropDownStyle = ComboBoxStyle.DropDownList;
						((ComboBox)InputControl).SelectedIndex = index;
						break;
					}
					else
						throw new NotImplementedException("UserOption type not handled yet: " + VirtualProperty.DataType.FullName);
			}
			InputControl.Top = lblValue.Top;
			InputControl.Left = lblValue.Right + 5;
			this.Controls.Add(InputControl);

			if (btnColorBrowse != null)
			{
				btnColorBrowse.Left = InputControl.Right + 5;
				btnColorBrowse.Top = InputControl.Top;
				this.Controls.Add(btnColorBrowse);
			}
			if (btnFileBrowse != null)
			{
				btnFileBrowse.Left = InputControl.Right + 5;
				btnFileBrowse.Top = InputControl.Top;
				this.Controls.Add(btnFileBrowse);
			}
			if (btnDirectoryBrowse != null)
			{
				btnDirectoryBrowse.Left = InputControl.Right + 5;
				btnDirectoryBrowse.Top = InputControl.Top;
				this.Controls.Add(btnDirectoryBrowse);
			}
			Height = (Height - ClientSize.Height) + InputControl.Bottom + ucHeading1.Height + 10;
		}

		void btnDirectoryBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog directoryBrowser = new FolderBrowserDialog();
			directoryBrowser.SelectedPath = InputControl.Text;
			Interfaces.Events.ShadeMainForm();

			if (directoryBrowser.ShowDialog(this) == DialogResult.OK)
			{
				InputControl.Text = directoryBrowser.SelectedPath;
			}
			Interfaces.Events.UnShadeMainForm();
		}

		void btnFileBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog fileBrowser = new OpenFileDialog();
			fileBrowser.FileName = InputControl.Text;
			Interfaces.Events.ShadeMainForm();

			if (fileBrowser.ShowDialog(this) == DialogResult.OK)
			{
				InputControl.Text = fileBrowser.FileName;
			}
			Interfaces.Events.UnShadeMainForm();
		}

		void btnColorBrowse_Click(object sender, EventArgs e)
		{
			ColorDialog colorBrowser = new ColorDialog();
			colorBrowser.Color = InputControl.BackColor;
			Interfaces.Events.ShadeMainForm();

			if (colorBrowser.ShowDialog(this) == DialogResult.OK)
			{
				InputControl.BackColor = colorBrowser.Color;
			}
			Interfaces.Events.UnShadeMainForm();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			switch (VirtualProperty.DataType.FullName)
			{
				case "System.String":
					VirtualProperty.Value = InputControl.Text;
					break;
				case "System.Int32":
					VirtualProperty.Value = int.Parse(InputControl.Text);
					break;
				case "System.Double":
					VirtualProperty.Value = double.Parse(InputControl.Text);
					break;
				case "System.Drawing.Color":
					VirtualProperty.Value = InputControl.BackColor.ToArgb();
					break;
				case "System.Boolean":
					VirtualProperty.Value = ((CheckBox)InputControl).Checked;
					break;
				default:
					if (VirtualProperty.DataType.IsEnum)
					{
						VirtualProperty.Value = ((ComboBox)InputControl).SelectedItem;
						break;
					}
					throw new NotImplementedException("UserOption type not handled yet: " + VirtualProperty.DataType.FullName);
			}
			string failReason;

			if (!IsValid(out failReason))
			{
				MessageBox.Show(string.Format("{0}", failReason), "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				VirtualProperty.Value = OriginalValue;
				return;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormObjectOptionEdit_FormClosed(object sender, FormClosedEventArgs e)
		{
			Interfaces.Events.UnShadeMainForm();
		}

		private bool IsValid(out string failReason)
		{
			return VirtualProperty.IsValid(false, out failReason);
		}

		private void btnResetDefaultValue_Click(object sender, EventArgs e)
		{
			object defaultValue = VirtualProperty.DefaultValue;

			switch (VirtualProperty.DataType.FullName)
			{
				case "System.String":
					InputControl.Text = (string)defaultValue;
					break;
				case "System.Int32":
					InputControl.Text = ((int)defaultValue).ToString();
					break;
				case "System.Double":
					InputControl.Text = ((double)defaultValue).ToString();
					break;
				case "System.Drawing.Color":
					InputControl.BackColor = Color.FromArgb((int)defaultValue);
					break;
				case "System.Boolean":
					((CheckBox)InputControl).Checked = (bool)defaultValue;
					break;
				case "System.IO.FileInfo":
					InputControl.Text = (string)defaultValue;
					break;
				case "System.IO.DirectoryInfo":
					InputControl.Text = (string)defaultValue;
					break;
				default:
					if (VirtualProperty.DataType.IsEnum)
					{
						int index = ((ComboBox)InputControl).FindStringExact((string)defaultValue);
						((ComboBox)InputControl).SelectedIndex = index;
						break;
					}
					throw new NotImplementedException("UserOption type not handled yet: " + VirtualProperty.DataType.FullName);
			}
		}


	}
}