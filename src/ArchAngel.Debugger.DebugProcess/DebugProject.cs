using System;
using System.Xml;
using System.Collections;
using ArchAngel.Interfaces.ITemplate;
using System.Collections.Generic;
using ArchAngel.Interfaces.TemplateInfo;
using Slyce.Loader;

namespace ArchAngel.Debugger.DebugProcess
{
    /// <summary>
    /// Summary description for Project.
    /// </summary>
    [Serializable]
    public class DebugProject : Interfaces.ProjectHelper
    {
        public override bool DisplayOptionToUser(IOption option, Interfaces.IScriptBaseObject iteratorObject)
        {
            try
            {
                if (option.DisplayToUserValue.HasValue)
                {
                    return option.DisplayToUserValue.Value;
                }
                if (string.IsNullOrEmpty(option.DisplayToUserFunction))
                {
                    return true;
                }
                if (iteratorObject == null)
                {
                    object[] parameters = new object[0];
                    return (bool)Loader.Instance.CallTemplateFunction(option.DisplayToUserFunction, ref parameters);
                }
                else
                {
                    object[] parameters = new object[] { iteratorObject };
                    return (bool)Loader.Instance.CallTemplateFunction(option.DisplayToUserFunction, ref parameters);
                }
            }
            catch (MissingMethodException)
            {
                object[] parameters = new object[] { iteratorObject };
                return (bool)Loader.Instance.CallTemplateFunction(option.DisplayToUserFunction, ref parameters);
            }
        }

        public override bool IsValid(string validationFunctionName, object objectToCheck, out string failReason)
        {
            if (typeof(IOption).IsInstanceOfType(objectToCheck))
            {
                IOption opt = (IOption)objectToCheck;

                if (opt.IsValidValue.HasValue)
                {
                    failReason = "";
                    return opt.IsValidValue.Value;
                }
            }
            failReason = "";
            object[] objs = new[] { objectToCheck, failReason };
            bool isValid = (bool)Loader.Instance.CallTemplateFunction(validationFunctionName, ref objs);
            failReason = (string)objs[1];
            return isValid;
        }

        public override object CallTemplateFunction(string functionName, ref object[] parameters)
        {
            return Loader.Instance.CallTemplateFunction(functionName, ref parameters);
        }

        /// <summary>
        /// Gets the default value from the function that has been specified as the DefaultValueFunction.
        /// </summary>
        /// <param name="virtualPropertyName"></param>
        /// <param name="iteratorObject"></param>
        /// <returns></returns>        
        public override object GetVirtualPropertyDefaultValue(string virtualPropertyName, object iteratorObject)
        {
            string functionName = string.Format("{0}_{1}Default", iteratorObject.GetType().FullName.Replace("+", ".").Replace(".", "_"), virtualPropertyName);
            //string functionName = virtualPropertyName;

            try
            {
                object[] parameters = new[] { iteratorObject };
                return CallTemplateFunction(functionName, ref parameters);
            }
            catch (MissingMethodException)
            {
                object[] parameters = new object[0];
                return CallTemplateFunction(functionName, ref parameters);
            }
        }

    }
}