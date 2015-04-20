using System;
using System.Collections.Generic;
using System.Text;
using Slyce.Common;

namespace ArchAngel.Designer.DesignerProject
{
	/// <summary>
	/// This class holds information about user defined functions. It is persisted in
	/// Project.SaveToXml and Project.ReadFromXml.
	/// </summary>
	[Serializable]
	public class FunctionInfo
	{
		public readonly List<ParamInfo> Parameters;
		public string Name;
		public Type ReturnType;
		private string m_body;
		public bool IsTemplateFunction;
		/// <summary>
		/// If this is true, the function represents an extension method.
		/// </summary>
		public bool IsExtensionMethod;
		/// <summary>
		/// If this function is an extension method, this is the type it extends.
		/// </summary>
		public string ExtendedType = "";

		public string TemplateReturnLanguage;
		public SyntaxEditorHelper.ScriptLanguageTypes ScriptLanguage;
		public string Description;
		public string Category;

		public FunctionInfo(string name, Type returnType, string body, bool isTemplateFunction, SyntaxEditorHelper.ScriptLanguageTypes scriptLanguage, string description, string templateReturnLanguage, string category)
		{
			Name = name;
			ReturnType = returnType;
			Parameters = new List<ParamInfo>();
			Body = body;
			IsTemplateFunction = isTemplateFunction;
			ScriptLanguage = scriptLanguage;
			Description = description;
			TemplateReturnLanguage = templateReturnLanguage;
			Category = category;
		}

		public FunctionInfo(bool isExtensionMethod, string extendedType)
		{
			IsExtensionMethod = isExtensionMethod;
			ExtendedType = extendedType;
			Body = "";
			Description = "";
			Parameters = new List<ParamInfo>();
		}

		public FunctionInfo(string name, Type returnType, string body, bool isTemplateFunction, SyntaxEditorHelper.ScriptLanguageTypes scriptLanguage, string description, string templateReturnLanguage, string category, bool isExtensionMethod, string extendedType)
			: this(name, returnType, body, isTemplateFunction, scriptLanguage, description, templateReturnLanguage, category)
		{
			IsExtensionMethod = isExtensionMethod;
			ExtendedType = extendedType;
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="o"></param>
		public FunctionInfo(FunctionInfo o)
		{
			Name = o.Name;
			ReturnType = o.ReturnType;
			Parameters = o.Parameters;
			Body = o.Body;
			IsTemplateFunction = o.IsTemplateFunction;
			ScriptLanguage = o.ScriptLanguage;
			Description = o.Description;
			TemplateReturnLanguage = o.TemplateReturnLanguage;
			Category = o.Category;
			IsExtensionMethod = o.IsExtensionMethod;
			ExtendedType = o.ExtendedType;
		}

		/// <summary>
		/// Returns the body of the function, with new lines standardised to \n
		/// </summary>
		public string Body
		{
			get { return m_body; }
			set
			{
				if (value != null)
					m_body = Utility.StandardizeLineBreaks(value, "\n");
				else
					m_body = value;

			}
		}

		public void AddParameter(ParamInfo parm)
		{
			bool found = false;

			for (int i = 0; i < Parameters.Count; i++)
			{
				if (Parameters[i].Name == parm.Name)
				{
					found = true;
					break;
				}
			}
			if (!found)
			{
				Parameters.Add(parm);
			}
		}

		/// <summary>
		/// Gets whether this function's parameters are the same as the parameter array passed as an argument.
		/// </summary>
		/// <param name="paramsToMatch">The paramter array to match against.</param>
		/// <returns>True if they are the same.</returns>
		public bool ParametersAreEqual(List<ParamInfo> paramsToMatch)
		{
			if (Parameters.Count != paramsToMatch.Count)
			{
				return false;
			}
			for (int i = 0; i < Parameters.Count; i++)
			{
				if (!Utility.StringsAreEqual(Parameters[i].Name, paramsToMatch[i].Name, false) ||
					!Utility.StringsAreEqual(Parameters[i].Modifiers, paramsToMatch[i].Modifiers, false) ||
					!Utility.StringsAreEqual(Parameters[i].DataType.FullName, paramsToMatch[i].DataType.FullName, false))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Creates a nicely formatted version of the function's signature
		/// </summary>
		public string DisplayName
		{
			get
			{
				StringBuilder sb = new StringBuilder(100);

				foreach (var param in Parameters)
				{
					sb.Append(param.DataType.FullName + ",");
				}
				return String.Format("{0}({1})", Name, sb.ToString().TrimEnd(',').Replace("+", "."));
			}
		}
	}
}