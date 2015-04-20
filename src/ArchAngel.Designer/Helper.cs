using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms; // Required for some bad design code below!

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for Helper.
	/// </summary>
	public class Helper
	{
		/// <summary>
		/// Generates a code file to be used as a blank code template. Returns count
		/// of valid functions.
		/// </summary>
		/// <param name="targetFile"></param>
		/// <param name="outputFile"></param>
		/// <returns>Count of valid functions found.</returns>
		public static int GenerateTemplateFile(string targetFile, string outputFile)
		{
			if (File.Exists(outputFile))
			{
				// TODO: we shouldn't be using a MessageBox from within a non-WinForm class!
				if (MessageBox.Show(Controller.Instance.MainForm, "Output file already exists. Do you want to overwrite?", "File exists", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
				{
					return 0;
				}

				File.Delete(outputFile);
			}
			int validCount = 0;
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFile(targetFile);
			}
			catch
			{
				throw new Exception("Not a valid file.");
			}
			MethodInfo[] methods;
			MethodInfo method;
			Type scriptAttrib = assembly.GetType("ScriptLib.ScriptPermissionAttribute");

			if (scriptAttrib == null)
			{
				MessageBox.Show(Controller.Instance.MainForm, "ScriptPermissionAttribute attribute not found in this file (assembly).", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return 0;
			}
			Type[] types = assembly.GetTypes();
			StringBuilder sb = new StringBuilder();

			foreach (Type type in types)
			{
				object[] attribs = type.GetCustomAttributes(scriptAttrib, true);

				if (attribs.Length > 0 &&
					type.IsInterface)
				{
					validCount++;
					sb.Append(string.Format("Imports System\nImports System.Collections\nImports {0}\n\n", type.Namespace));
					sb.Append(string.Format("Namespace {0}\n\n", type.Namespace));
					sb.Append(string.Format("\tPublic Class {0} Inherits {1}\n\n", type.Name.Substring(1), type.Name));
					methods = type.GetMethods();

					for (int methodCounter = 0; methodCounter < methods.Length; methodCounter++)
					{
						method = methods[methodCounter];

						sb.Append(method.ReturnType.Name == "Void" ? "\t\tPublic Sub " : "\t\tPublic Function ");
						sb.Append(method.IsStatic ? "Static " : "");
						sb.Append(string.Format("{0} (", method.Name));

						ParameterInfo[] parameters = method.GetParameters();

						for (int pCounter = 0; pCounter < parameters.Length; pCounter++)
						{
							ParameterInfo param = parameters[pCounter];

							if (pCounter < parameters.Length - 1)
							{
								sb.Append(string.Format("{2}{0} As {1}, ", param.Name, param.ParameterType.Name.Replace("&", ""), param.IsOut ? "ByRef " : ""));
							}
							else
							{
								sb.Append(string.Format("{2}{0} As {1}", param.Name, param.ParameterType.Name.Replace("&", ""), param.IsOut ? "ByRef " : ""));
							}
						}
						sb.Append(")\n");
						sb.Append(method.ReturnType.Name == "Void" ? "\t\tEnd Sub\n\n" : "\t\tEnd Function\n\n");

					}
					sb.Append("\tEnd Class\n");
				}
			}
			sb.Append("End Namespace\n");
			string body = sb.ToString();
			TextWriter tw = null;

			try
			{
				tw = new StreamWriter(outputFile);
				tw.Write(body);
			}
			finally
			{
				if (tw != null)
				{
					tw.Flush();
					tw.Close();
				}
			}
			return validCount;
		}

	}
}
