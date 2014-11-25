using System;
using System.IO;
using System.Windows.Forms;
using log4net.Config;

namespace Provider.Test
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var file = new FileInfo("log4net.config");
			if (!file.Exists)
				throw new Exception("Could not load log4net config");
			XmlConfigurator.Configure(file);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
