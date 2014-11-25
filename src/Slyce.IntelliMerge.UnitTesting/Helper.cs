using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Slyce.IntelliMerge.UnitTesting
{
	public static class Helper
	{
		/// <summary>
		/// Read resource file and return it as a string
		/// </summary>
		/// <param name="resourceName">full name of resource (eg SI.WorkflowEngine.Sabre.Resources.CommandTemplateRQ.xml)</param>
		/// <returns>contents of resource file</returns>
		public static string GetResource(string resourceName)
		{
			using (
				StreamReader reader =
					new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
