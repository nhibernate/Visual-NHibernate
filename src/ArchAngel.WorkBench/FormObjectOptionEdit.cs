using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Slyce.Common.Controls;
using Slyce.Loader;

namespace ArchAngel.Workbench
{
    public partial class FormObjectOptionEdit : Form
    {
        public ArchAngel.Interfaces.ITemplate.IOption CurrentOption;
        public ArchAngel.Interfaces.ITemplate.IUserOption UserOption;
        private Control InputControl;
        private ArchAngel.Interfaces.IScriptBaseObject IteratorObject;
        private object OriginalValue;

        public FormObjectOptionEdit(ArchAngel.Interfaces.ITemplate.IOption option, ArchAngel.Interfaces.ITemplate.IUserOption userOption, ArchAngel.Interfaces.IScriptBaseObject iteratorObject)
        {
            InitializeComponent();
            IteratorObject = iteratorObject;
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading1.Text = "";
            CurrentOption = option;
            UserOption = userOption;
            OriginalValue = UserOption.Value;
            Controller.ShadeMainForm();
            Populate();
        }

        private void Populate()
        {
            this.Text = "Option Edit - " + CurrentOption.VariableName;
            lblText.Text = CurrentOption.Text.Length == 0 ? "No text available. Edit template to add text." : CurrentOption.Text;
            lblDescription.Text = CurrentOption.Description.Length == 0 ? "No Description available. Edit template to add a description." : CurrentOption.Description;
            lblText.Visible = true;
            lblDescription.Visible = true;
            Button btnColorBrowse = null;
            Button btnFileBrowse = null;
            Button btnDirectoryBrowse = null;

            switch (CurrentOption.VarType.FullName)
            {
                case "System.String":
                    InputControl = new TextBox();
                    ((TextBox)InputControl).Text = (string)UserOption.Value;
                    break;
                case "System.Int32":
                    InputControl = new NumEdit();
                    ((NumEdit)InputControl).InputType = NumEdit.NumEditType.Integer;
                    ((NumEdit)InputControl).Text = ((int)UserOption.Value).ToString();
                    break;
                case "System.Double":
                    InputControl = new NumEdit();
                    ((NumEdit)InputControl).InputType = NumEdit.NumEditType.Double;
                    ((NumEdit)InputControl).Text = ((double)UserOption.Value).ToString();
                    break;
                case "System.Drawing.Color":
                    InputControl = new Label();
                    int colorVal = (int)UserOption.Value;
                    ((Label)InputControl).BackColor = Color.FromArgb(colorVal);
                    btnColorBrowse = new Button();
                    btnColorBrowse.Name = "btnColorBrowse";
                    btnColorBrowse.Text = "...";
                    btnColorBrowse.Width = 24;
                    btnColorBrowse.Click += new EventHandler(btnColorBrowse_Click);
                    break;
                case "System.Boolean":
                    InputControl = new CheckBox();
                    ((CheckBox)InputControl).Checked = (bool)UserOption.Value;
                    break;
                case "System.Enumeration":
                    InputControl = new ComboBox();
                    ((ComboBox)InputControl).Items.AddRange(CurrentOption.Values);
                    int index = ((ComboBox)InputControl).FindStringExact((string)UserOption.Value);
                    ((ComboBox)InputControl).SelectedIndex = index;
                    break;
                case "System.IO.FileInfo":
                    InputControl = new TextBox();
                    ((TextBox)InputControl).Text = (string)UserOption.Value;
                    btnFileBrowse = new Button();
                    btnFileBrowse.Name = "btnFileBrowse";
                    btnFileBrowse.Text = "...";
                    btnFileBrowse.Width = 24;
                    btnFileBrowse.Click += new EventHandler(btnFileBrowse_Click);
                    break;
                case "System.IO.DirectoryInfo":
                    InputControl = new TextBox();
                    ((TextBox)InputControl).Text = (string)UserOption.Value;
                    btnDirectoryBrowse = new Button();
                    btnDirectoryBrowse.Name = "btnDirectoryBrowse";
                    btnDirectoryBrowse.Text = "...";
                    btnDirectoryBrowse.Width = 24;
                    btnDirectoryBrowse.Click += new EventHandler(btnDirectoryBrowse_Click);
                    break;
                default:
                    throw new NotImplementedException("UserOption type not handled yet: " + CurrentOption.VarType.FullName);
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
            this.Height = (this.Height - this.ClientSize.Height) + InputControl.Bottom + ucHeading1.Height + 10;
        }

        void btnDirectoryBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog directoryBrowser = new FolderBrowserDialog();
            directoryBrowser.SelectedPath = ((TextBox)InputControl).Text;
            Controller.ShadeMainForm();

            if (directoryBrowser.ShowDialog(this) == DialogResult.OK)
            {
                ((TextBox)InputControl).Text = directoryBrowser.SelectedPath;
            }
            Controller.UnshadeMainForm();
        }

        void btnFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.FileName = ((TextBox)InputControl).Text;
            Controller.ShadeMainForm();

            if (fileBrowser.ShowDialog(this) == DialogResult.OK)
            {
                ((TextBox)InputControl).Text = fileBrowser.FileName;
            }
            Controller.UnshadeMainForm();
        }

        void btnColorBrowse_Click(object sender, EventArgs e)
        {
            ColorDialog colorBrowser = new ColorDialog();
            colorBrowser.Color = ((Label)InputControl).BackColor;
            Controller.ShadeMainForm();

            if (colorBrowser.ShowDialog(this) == DialogResult.OK)
            {
                ((Label)InputControl).BackColor = colorBrowser.Color;
            }
            Controller.UnshadeMainForm();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            switch (CurrentOption.VarType.FullName)
            {
                case "System.String":
                    UserOption.Value = ((TextBox)InputControl).Text;
                    break;
                case "System.Int32":
                    UserOption.Value = int.Parse(((NumEdit)InputControl).Text);
                    break;
                case "System.Double":
                    UserOption.Value = double.Parse(((NumEdit)InputControl).Text);
                    break;
                case "System.Drawing.Color":
                    UserOption.Value = ((Label)InputControl).BackColor.ToArgb();
                    break;
                case "System.Boolean":
                    UserOption.Value = ((CheckBox)InputControl).Checked;
                    break;
                case "System.Enumeration":
                    UserOption.Value = ((ComboBox)InputControl).Text;
                    break;
                default:
                    throw new NotImplementedException("UserOption type not handled yet: " + CurrentOption.VarType.FullName);
            }
            string failReason;

            if (!IsValid(out failReason))
            {
                MessageBox.Show(string.Format("{0}", failReason), "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UserOption.Value = OriginalValue;
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormObjectOptionEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.UnshadeMainForm();
        }

        private bool IsValid(out string failReason)
        {
            failReason = "";
            object[] objs = new object[] { IteratorObject, failReason };
            bool isValid = (bool)Loader.Instance.CallTemplateFunction(CurrentOption.ValidatorFunction, ref objs);
            failReason = (string)objs[1];
            return isValid;
        }

        private void btnResetDefaultValue_Click(object sender, EventArgs e)
        {
            object defaultValue = GetDefaultOptionValueFromFunction(CurrentOption.DefaultValue, IteratorObject);

            switch (CurrentOption.VarType.FullName)
            {
                case "System.String":
                    ((TextBox)InputControl).Text = (string)defaultValue;
                    break;
                case "System.Int32":
                    ((NumEdit)InputControl).Text = ((int)defaultValue).ToString();
                    break;
                case "System.Double":
                    ((NumEdit)InputControl).Text = ((double)defaultValue).ToString();
                    break;
                case "System.Drawing.Color":
                    ((Label)InputControl).BackColor = Color.FromArgb((int)defaultValue);
                    break;
                case "System.Boolean":
                    ((CheckBox)InputControl).Checked = (bool)defaultValue;
                    break;
                case "System.Enumeration":
                    int index = ((ComboBox)InputControl).FindStringExact((string)defaultValue);
                    ((ComboBox)InputControl).SelectedIndex = index;
                    break;
                case "System.IO.FileInfo":
                    ((TextBox)InputControl).Text = (string)defaultValue;
                    break;
                case "System.IO.DirectoryInfo":
                    ((TextBox)InputControl).Text = (string)defaultValue;
                    break;
                default:
                    throw new NotImplementedException("UserOption type not handled yet: " + CurrentOption.VarType.FullName);
            }
        }

        /// <summary>
        /// Gets the default value from the function that has been specified as the DefaultValueFunction.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="iteratorObject"></param>
        /// <returns></returns>
        private object GetDefaultOptionValueFromFunction(string functionName, object iteratorObject)
        {
            try
            {
                object[] parameters = new object[] { iteratorObject };
                return Loader.Instance.CallTemplateFunction(functionName, ref parameters);
            }
            catch (System.MissingMethodException ex)
            {
                object[] parameters = new object[0];
                return Loader.Instance.CallTemplateFunction(functionName, ref parameters);
            }
        }

    }
}