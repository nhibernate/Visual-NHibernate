using System;
using System.Collections;
using System.IO;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for SyntaxReader.
	/// </summary>
	public class SyntaxReader
	{
		public enum SyntaxType
		{
			CSharp,
			VBNet
		}
		private string TheFile;
		private ArrayList Keywords = new ArrayList();
		private ArrayList Functions = new ArrayList();
		private ArrayList Comments = new ArrayList();

		public SyntaxReader(SyntaxType syntax)
		{
			string fileResouce = "";

			switch (syntax)
			{
				case SyntaxType.CSharp:
					fileResouce = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".Resources.cs.syntax";
					break;
				case SyntaxType.VBNet:
					fileResouce = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".Resources.vb.syntax";
					break;
				default:
					throw new Exception("Syntax not catered for.");
			}
			using (Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(fileResouce))
			{
				TextReader tr = new StreamReader(s);
				TheFile = tr.ReadToEnd();
			}
			FillArrays();
		}

		public void FillArrays()
		{
			StringReader sr = new StringReader(TheFile);
			string nextLine;

			nextLine = sr.ReadLine();
			nextLine = nextLine.Trim();

			// Find functions header
			while (nextLine != null)
			{
				if (nextLine == "[FUNCTIONS]")
				{
					// Read all of the functions into the arraylist
					nextLine = sr.ReadLine();
					if (nextLine != null) { nextLine = nextLine.Trim(); }

					while (nextLine != null && nextLine[0] != '[')
					{
						Functions.Add(nextLine);
						nextLine = "";

						while (nextLine != null && nextLine.Length == 0)
						{
							nextLine = sr.ReadLine();
							if (nextLine != null) { nextLine = nextLine.Trim(); }
						}
					}
				}
				if (nextLine == "[KEYWORDS]")
				{
					// Read all of the keywords into the arraylist
					nextLine = sr.ReadLine();

					if (nextLine != null) { nextLine = nextLine.Trim(); }

					while (nextLine != null && nextLine[0] != '[')
					{
						Keywords.Add(nextLine);
						nextLine = "";

						while (nextLine != null && nextLine == "")
						{
							nextLine = sr.ReadLine();

							if (nextLine != null) { nextLine = nextLine.Trim(); }
						}
					}
				}
				if (nextLine == "[COMMENTS]")
				{
					// Read all of the comment sentinels into the arraylist
					nextLine = sr.ReadLine();

					if (nextLine != null) { nextLine = nextLine.Trim(); }

					while (nextLine != null && nextLine[0] != '[')
					{
						Comments.Add(nextLine);
						nextLine = "";

						while (nextLine != null && nextLine == "")
						{
							nextLine = sr.ReadLine();
							if (nextLine != null) { nextLine = nextLine.Trim(); }
						}
					}
				}
				if (nextLine != null && nextLine.Length > 0 && nextLine[0] == '[')
				{
				}
				else
				{
					nextLine = sr.ReadLine();
					if (nextLine != null) { nextLine = nextLine.Trim(); }
				}
			}
			Keywords.Sort();
			Functions.Sort();
			Comments.Sort();
		}

		public bool IsKeyword(string s)
		{
			return (Keywords.BinarySearch(s) >= 0);
		}

		public bool IsFunction(string s)
		{
			return (Functions.BinarySearch(s) >= 0);
		}

		public bool IsComment(string s)
		{
			Slyce.Common.Utility.CheckForNulls(new object[] { s }, new string[] { "s" });
			for (int i = 0; i < Comments.Count; i++)
			{
				// Does the line start with a comment?
				if (s.IndexOf(Comments[i].ToString()) == 0)
				{
					return true;
				}
			}
			return false;
			//return (Comments.BinarySearch(s) >= 0);
		}


	}
}
