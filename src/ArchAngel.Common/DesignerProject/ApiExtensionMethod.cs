using System;
using System.Linq;
using System.Reflection;
using System.Text;
using ArchAngel.Interfaces;
using ArchAngel.Providers.CodeProvider;
using Slyce.Common;

namespace ArchAngel.Designer.DesignerProject
{
    public class ApiExtensionMethod
    {
        private string description;
        private string defaultCode;

        private MethodInfo extendedMethod;

        /// <summary>
        /// The code that the user has overridden the method with.
        /// </summary>
        public string OverridingFunctionBody { get; set; }

        public ApiExtensionMethod(MethodInfo extendedMethod)
        {
            ExtendedMethod = extendedMethod;
        }

        /// <summary>
        /// The method to be overridden.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Must have the ApiExtension attribute on it
        /// or this will throw an ArgumentException when set.
        /// </exception>
        public MethodInfo ExtendedMethod
        {
            get
            {
                return extendedMethod;
            }
            set
            {
                if(ExtensionAttributeHelper.HasApiExtensionAttribute(value) == false)
                {
                    throw new ArgumentException(string.Format("Method {0} does not have the ApiExtension attribute", value.Name), "value");
                }
                extendedMethod = value;
            }
        }

        /// <summary>
        /// The documentation that the Provider author gave for the method.
        /// </summary>
        public string Description
        {
            get
            {
                if (ExtendedMethod == null)
                    return "";

                if(description == null)
                    description = ExtensionAttributeHelper.GetExtensionDescription(ExtendedMethod);

                return description;
            }
        }

        /// <summary>
        /// The code that exists in the Provider before overriding.
        /// </summary>
        public string DefaultCode
        {
            get
            {
                return defaultCode;
            }
			set
			{
				defaultCode = value;
			}
        }

        private FunctionInfo functionInfo;
        public bool HasOverride { get { return string.IsNullOrEmpty(OverridingFunctionBody) == false; } }

        public FunctionInfo FunctionInfo
        {
            get
            {
                if(functionInfo == null)
                {
                    functionInfo = CreateFunctionInfoFrom(this);
                }
                if (HasOverride)
                    functionInfo.Body = OverridingFunctionBody;
                return functionInfo;
            }
        }

        public static FunctionInfo CreateFunctionInfoFrom(ApiExtensionMethod extMethod)
        {
            MethodInfo method = extMethod.ExtendedMethod;

            string formattedCode = extMethod.OverridingFunctionBody;

			if (extMethod.HasOverride == false && string.IsNullOrEmpty(extMethod.DefaultCode) == false)
            {
                CSharpParser parser = new CSharpParser();
                parser.FormatSettings.MaintainWhitespace = false;

                string methodText = string.Format("public void {0} () {{ {1} }}", method.Name, extMethod.DefaultCode);

                var bc = parser.ParseSingleConstruct(methodText, BaseConstructType.MethodDeclaration);

                // Remove the start and end braces
                formattedCode = Utility.StandardizeLineBreaks(bc.ToString(), "\n");
                // + 1 to get past the {, +1 to remove the first line break.
                formattedCode = formattedCode.Substring(formattedCode.IndexOf('{')+2);
                formattedCode = formattedCode.Substring(0, formattedCode.LastIndexOf('}')-1);

                formattedCode = RemoveTabs(formattedCode);
            }

            FunctionInfo fi = new FunctionInfo(
                method.Name,
                method.ReturnType,
                formattedCode, 
                true,
                SyntaxEditorHelper.ScriptLanguageTypes.CSharp,
                extMethod.Description, 
                "C#",
                "Extension Methods");

            return fi;
        }

        private static string RemoveTabs(string formattedCode)
        {
            StringBuilder sb = new StringBuilder();
            int numTabs = -1;
            foreach(var line in formattedCode.Split('\n'))
            {
                if(numTabs == -1)
                {
                    numTabs = line.TakeWhile(c => c == '\t').Count();
                }

                sb.AppendLine(line.Remove(0, numTabs));
            }

            formattedCode = sb.ToString();
            return formattedCode;
        }
    }
}