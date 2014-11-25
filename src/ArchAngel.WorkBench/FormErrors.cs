using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Common;

namespace ArchAngel.Workbench
{
    public partial class FormErrors : Form
    {
        public FormErrors(string titleText)
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading1.Text = "";
            Controller.Instance.ShadeMainForm();
            this.Text = titleText;
        }

        public void ShowErrors(List<GenerationError> errors)
        {
            StringBuilder sb = new StringBuilder(10000);
            sb.Append(@"
                    <html>
                    <style>
                    * {
	                    font-family:Verdana;
	                    font-size:xx-small;
                    }

                    td {
	                    vertical-align:top;
                    }

                    td.filename {
	                    background-color:lightsteelblue;
	                    font-weight:bold;
                    }
                    .error {
                        color:Red;
                    }
                    </style>

                    <script>
                    function showDiv(name)
                    {
                        var div = eval('document.all.' + name);
                        var link = eval('document.all.link' + name);
                    	

                        if (div.style.display == 'none')
                        {
                            div.style.display = 'block';
                            link.innerText = link.innerText.replace('+', '-');
                        }
                        else
                        {
                            div.style.display = 'none';
                            link.innerText = link.innerText.replace('-', '+');
                        }
                    }
                    </script>

                    <body>
                    <table>");
            sb.Append(@"
                    <tr>
                        <td width='20'>&nbsp;</td>
                        <td width='*'>&nbsp;</td>
                    </tr>");

            foreach (GenerationError err in errors)
            {
                sb.Append(string.Format(@"
                    <tr>
                        <td colspan='2' class='filename'>{0}</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>{1}</td>
                    </tr>
                    <tr>
                        <td colspan='2'>&nbsp;</td>
                    </tr>", err.FileName, err.ErrorDescription));
            }
            sb.Append("</table></body></html>");
            webBrowserErrors.DocumentText = sb.ToString();
            //richTextBoxErrors.Text = text;
            this.ShowDialog(this.ParentForm);
        }

        private void FormErrors_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.Instance.UnshadeMainForm();
        }
    }
}