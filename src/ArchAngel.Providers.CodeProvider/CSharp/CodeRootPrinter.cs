using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class CodeRootPrinter : IPrinter
	{
		private CodeRoot obj;

		public CodeRootPrinter(CodeRoot obj)
		{
			this.obj = obj;
		}

		public override string ToString()
		{
			//CurrentCodeRoot = this;
			StringBuilder sb = new StringBuilder(10000);
			obj.Controller.IndentLevel = 0;

			if (!obj.Controller.Reorder)
			{
				Type prevType = null;
				Type fieldType = typeof(Field);
				Type usingStatementType = typeof(UsingStatement);

				foreach (BaseConstruct baseObject in obj.SortedConstructs)
				{
					if (prevType == fieldType && !fieldType.IsInstanceOfType(baseObject))
					{
						// Make sure to insert a line after a field declaration, but not if it preceeded by another field.
						sb.AppendLine();
						prevType = baseObject.GetType();
					}
					else if (prevType == usingStatementType && !usingStatementType.IsInstanceOfType(baseObject))
					{
						// Make sure to insert a line after a using statement, but not if it preceeded by another using statement
						sb.AppendLine();
						prevType = baseObject.GetType();
					}
					else
					{
						prevType = baseObject.GetType();
					}
					if (!baseObject.Omit)
					{
						sb.AppendLine(baseObject.ToString());
					}
				}
			}
			else
			{
				#region Using Statements
				//sb.AppendLine(GetUsingTextBlock());
				sb.AppendLine(obj.UsingStatementsTextBlock);
				#endregion

				#region Attributes
				for (int i = 0; i < obj.Attributes.Count; i++)
				{
					sb.AppendLine(obj.Attributes[i].ToString());
				}
				#endregion
				#region Namespaces
				for (int i = 0; i < obj.Namespaces.Count; i++)
				{
					if (!obj.Namespaces[i].Omit)
					{
						sb.AppendLine(obj.Namespaces[i].ToString());
					}
				}
				#endregion
				//Controller.IndentLevel++;

				#region Classes
				for (int i = 0; i < obj.Classes.Count; i++)
				{
					if (!obj.Classes[i].Omit)
					{
						sb.AppendLine(obj.Classes[i].ToString());
					}
				}
				#endregion
			}
			// Remove any linebreaks from the end
			while (sb.Length > 0 && (sb[sb.Length - 1] == '\n' || sb[sb.Length - 1] == '\r'))
			{
				sb.Remove(sb.Length - 1, 1);
			}
			//			CurrentCodeRoot = null;
			return Helper.StandardizeLineBreaks(sb.ToString(), Environment.NewLine);
		}

		public string GetUsingTextBlock(Controller tempController)
		{
			StringBuilder sb = new StringBuilder(300);

			for (int i = 0; i < obj.UsingStatements.Count; i++)
			{
				sb.AppendLine(string.Format("{0}{1}", tempController.Indent, obj.UsingStatements[i]));
			}
			return sb.ToString();
		}

		#region ICodeRoot Members

		#endregion

		public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Print()
		{
			throw new System.NotImplementedException();
		}

		public string GetOuterText()
		{
			throw new InvalidOperationException("Cannot call GetOuterText on a Code Root");
		}

		public string GetInnerText()
		{
			throw new InvalidOperationException("Cannot call GetInnerText on a Code Root");
		}
	}
}
