using System.Text;
using System.Windows.Forms;

namespace Slyce.IntelliMerge.UI.Editors
{
	public partial class ucGenerateErrorEditor : UserControl
	{
		public ucGenerateErrorEditor()
		{
			InitializeComponent();
		}

		public ucGenerateErrorEditor(string fileName, string description) : this()
		{
			StringBuilder sb = new StringBuilder(10000);
			sb.Append(@"
                    <html>
                    <style>
                    * {
	                    font-family:Verdana;
	                    font-size:xx-small;
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

                    <body>");

			sb.Append(string.Format(@"
                    <h1 class='filename'>{0}</h1>
                    <p>{1}</p>", fileName, description));
			sb.Append("</body></html>");
			webBrowser.DocumentText = sb.ToString();
		}
	}
}