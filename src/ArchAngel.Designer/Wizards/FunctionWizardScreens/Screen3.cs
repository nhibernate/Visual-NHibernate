using System.Collections.Generic;
using System.Text;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer.Wizards.FunctionWizardScreens
{
	public partial class Screen3 : Interfaces.Controls.ContentItems.ContentItem
	{
		public Screen3()
		{
			InitializeComponent();
			PageHeader = "Summary";
			PageDescription = "Summary of changes.";
			HasNext = false;
			HasPrev = true;
			NextText = "OK";
			HasFinish = true;
			chkCreateSampleCode.Visible = frmFunctionWizard.IsNewFunction;
		}

		public override bool Save()
		{
			if (frmFunctionWizard.IsNewFunction && chkCreateSampleCode.Checked)
			{
				frmFunctionWizard.CurrentFunction.Body = "SAMPLE";
			}
			return true;
		}

		public override void OnDisplaying()
		{
			var files = Project.Instance.FindFilesUsingFunction(frmFunctionWizard.CurrentFunction.Name);
			var sbUsage = new StringBuilder();

			if (files.Count > 0)
			{
				if (files.Count == 1)
				{
					sbUsage.Append("<h2>1 file is using this function:</h2><ul>");
				}
				else
				{
					sbUsage.AppendFormat("<h2>{0} files are using this function:</h2><ul>", files.Count);
				}
			}
			else
			{
				sbUsage.AppendFormat("<h2>No files are using this function.</h2>");
			}
			foreach (OutputFile file in files)
			{
				sbUsage.AppendFormat("<li>{0}</li>", file.Name);
			}
			if (files.Count > 0)
			{
				sbUsage.Append("</ul>");
			}
			sbUsage.Append("<br/>&nbsp;");
			List<FunctionInfo> foundLocations = Project.Instance.FindFunctionsUsing(frmFunctionWizard.CurrentFunction.Name, false);

			var sbLocations = new StringBuilder();

            foreach (FunctionInfo foundLocation in foundLocations)
			{
				sbLocations.AppendFormat("<li>{0}</li>", foundLocation.Name);
			}
			if (foundLocations.Count > 0)
			{
				if (foundLocations.Count == 1)
				{
					sbLocations.Insert(0, "<h2>1 other function is using this function:</h2><ul>");
				}
				else
				{
					sbLocations.Insert(0, string.Format("<h2>{0} other functions use this function:</h2><ul>", foundLocations.Count));
				}
				sbLocations.Insert(sbLocations.Length, "</ul>");
			}
			else
			{
				sbLocations.Insert(0, string.Format("<h2>No other functions use this function.</h2>"));
			}

			string parameterString = "";

            for (int i = 0; i < frmFunctionWizard.TempFunction.Parameters.Count; i++)
			{
				if (i > 0)
				{
					parameterString += ", ";
				}
				parameterString += string.Format(@"<span class='kwrd'>{0}</span> {1}", Slyce.Common.Utility.GetDemangledGenericTypeName(frmFunctionWizard.TempFunction.Parameters[i].DataType, Project.Instance.Namespaces).Replace("<", "&lt;").Replace(">", "&gt;"), frmFunctionWizard.TempFunction.Parameters[i].Name);
			}
			string returnTypeName = frmFunctionWizard.TempFunction.ReturnType == null ? "void" : Slyce.Common.Utility.GetDemangledGenericTypeName(frmFunctionWizard.TempFunction.ReturnType, Project.Instance.Namespaces);
			var sb = new StringBuilder(1000);
			// Code formatting, see: http://manoli.net/csharpformat/format.aspx
			sb.Append(string.Format(@"
            <html>
            <style>
                *
                {{
                    font-family:Verdana,Arial;
                    font-size:xx-small;
                    background-color:AliceBlue;
                    padding:0;
                    margin:0;
                }}
                h2
                {{
                    font-size:x-small;
                    background-color:LightSteelBlue;
                }}
                .csharpcode, .csharpcode pre
                {{
	                font-size: x-small;
	                color: black;
	                font-family: Consolas, 'Courier New', Courier, Monospace;
	                background-color: #ffffff;
                }}
                .csharpcode pre {{ margin: 0em; }}
                .csharpcode .rem {{ color: #008000; }}
                .csharpcode .kwrd {{ color: #0000ff; }}
                .csharpcode .str {{ color: #006080; }}
                .csharpcode .op {{ color: #0000c0; }}
                .csharpcode .preproc {{ color: #cc6633; }}
                .csharpcode .asp {{ background-color: #ffff00; }}
                .csharpcode .html {{ color: #800000; }}
                .csharpcode .attr {{ color: #ff0000; }}
                .csharpcode .alt 
                {{
	                background-color: #f4f4f4;
	                width: 100%;
	                margin: 0em;
                }}
                .csharpcode .lnum {{ color: #606060; }}
            </style>
            <body>
<pre class='csharpcode'>
<span class='kwrd'>public</span> <span class='kwrd'>{0}</span> {1}({4})
{2}
{3}
</pre>
{5}
{6}
            </body>
            </html>", returnTypeName, frmFunctionWizard.TempFunction.Name, "{", "}", parameterString, sbUsage, sbLocations));
			webBrowser1.DocumentText = sb.ToString();

			base.OnDisplaying();
		}

	}
}
