using System;

namespace ArchAngel.Interfaces
{
	public class ScriptFunctionWrapper
	{
		//object RunScriptFunction(string functionName, ref object[] parameters);
		//bool RunApiExtensionFunction(string functionName, out object result, ref object[] parameters);

		public virtual object RunScriptFunction(string functionName, ref object[] parameters)
		{
			throw new Exception("This base method should never get called. It is supposed to be overridden in the Template.");
		}

		/// <summary>
		/// If the function is API function has been overridden in the template, executes the function and returns true. Returns false if the function hasn't been overridden.
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="result"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual bool RunApiExtensionFunction(string functionName, out object result, ref object[] parameters)
		{
			throw new Exception("This base method should never get called. It is supposed to be overridden in the Template.");
		}
	}
}
