using System;
using System.Reflection;
using System.Windows.Forms;

namespace Slyce.Common
{
	public class CrossThreadHelper
	{
		#region Delegate Definitions
		// delegates used to call MainForm functions from worker thread
		public delegate void DelegateSetProperty(object obj, string propertyName, object val);
		public delegate object DelegateGetProperty(object obj, string propertyName);
		public delegate object DelegateCallMethod(object obj, string methodName, object[] parameters);
		#endregion

		#region Delegate Instances
		// Delegate instances used to call user interface functions 
		// from worker thread:
		private DelegateSetProperty _delegateSetProperty;
		private DelegateGetProperty _delegateGetProperty;
		private DelegateCallMethod _delegateCallMethod;
		#endregion

		private Control TheControl;

		public static bool ApplicationClosing = false;

		public CrossThreadHelper(Control form)
		{
			TheControl = form;
			_delegateSetProperty = new DelegateSetProperty(this.SetObjectProperty);
			_delegateGetProperty = new DelegateGetProperty(this.GetObjectProperty);
			_delegateCallMethod = new DelegateCallMethod(this.CallObjectMethod);
		}

		private void SetObjectProperty(object obj, string propertyName, object val)
		{
			System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(propertyName);
			pi.SetValue(obj, val, null);
		}

		private object GetObjectProperty(object obj, string propertyName)
		{
			System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(propertyName);
			return pi.GetValue(obj, null); // TODO: if we need to access an array, we need to change this from null
		}

		public void SetVisibility(Control control, bool visible)
		{
			if (TheControl.InvokeRequired)
			{
				SetCrossThreadProperty(control, "Visible", visible);
			}
			else
			{
				control.Visible = visible;
			}
		}


		private object CallObjectMethod(object obj, string methodName, object[] parameters)
		{
			Type type = obj.GetType();

			if (methodName.IndexOf(".") > 0)
			{
				string[] parts = methodName.Split('.');
				PropertyInfo pi = type.GetProperty(parts[0]);

				if (pi != null)
				{
					obj = pi.GetValue(obj, null);
					type = obj.GetType();
					methodName = parts[1];
				}
			}
			Type[] paramTypes = new Type[0];
			bool hasNullParams = false;

			if (parameters != null)
			{
				paramTypes = new Type[parameters.Length];

				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i] != null)
					{
						paramTypes[i] = parameters[i].GetType();
					}
					else
					{
						hasNullParams = true;
						paramTypes[i] = null;
					}
				}
			}
			System.Reflection.MethodInfo mi = null;

			if (!hasNullParams)
			{
				mi = type.GetMethod(methodName, paramTypes);
			}
			else
			{
				// We need to find the matching method by looping through all methods and finding the one with the best match.
				foreach (MethodInfo method in type.GetMethods())
				{
					if (method.Name == methodName)
					{
						bool found = true;
						ParameterInfo[] methodParameters = method.GetParameters();

						if (methodParameters.Length != paramTypes.Length)
						{
							continue;
						}

						for (int i = 0; i < methodParameters.Length; i++)
						{
							if (paramTypes[i] != null && methodParameters[i].ParameterType != typeof(System.Object) && methodParameters[i].ParameterType != paramTypes[i])
							{
								found = false;
								break;
							}
						}
						if (found)
						{
							mi = method;
							break;
						}
					}
				}
			}
			return mi.Invoke(obj, parameters);
		}

		public object CallCrossThreadMethod(object obj, string methodName, object[] parameters)
		{
			return CallCrossThreadMethod(obj, methodName, parameters, 10000);
		}

		/// <summary>
		/// Calls methodName
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <param name="timeoutMilliseconds"></param>
		/// <returns></returns>
		public object CallCrossThreadMethod(object obj, string methodName, object[] parameters, int timeoutMilliseconds)
		{
			try
			{
				IAsyncResult result = TheControl.BeginInvoke(_delegateCallMethod, obj, methodName, parameters);
				while (result.IsCompleted == false && ApplicationClosing == false)
				{
					result.AsyncWaitHandle.WaitOne(timeoutMilliseconds, false);
				}
				return TheControl.EndInvoke(result);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error calling CallCrossThreadMethod (Object: {0}, methodName: {1})", (obj != null ? obj.ToString() : "??"), methodName), ex);
			}
		}

		public void SetCrossThreadProperty(object obj, string propertyName, object val)
		{
			try
			{
				CallCrossThreadMethod(obj, "set_" + propertyName, new object[] { val });
				//TheControl.Invoke(_delegateSetProperty, obj, propertyName, val);
			}
			catch (Exception)
			{
				string message = string.Format("Object is null = {0}\nPropertyName = {1}\nValue = {2}\n", obj == null, propertyName, val);
				if (obj != null)
				{
					PropertyInfo propParseErrors = obj.GetType().GetProperty("ParseErrors");
					MethodInfo method_getParseErrors = obj.GetType().GetMethod("get_ParseErrors");
					MethodInfo method_setParseErrors = obj.GetType().GetMethod("set_ParseErrors");

					message += "\nParseErrors = " + (propParseErrors == null ? "null" : "found");
					message += "\nget_ParseErrors = " + (method_getParseErrors == null ? "null" : "found");
					message += "\nset_ParseErrors = " + (method_setParseErrors == null ? "null" : "found");
				}

				//foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
				//{
				//    message += string.Format("\n{0}", prop.Name);
				//}
				MessageBox.Show(message, "SetCrossThreadProperty failed");
				throw;
			}
		}

		public object GetCrossThreadProperty(object obj, string propertyName)
		{
			return TheControl.Invoke(_delegateGetProperty, obj, propertyName);
		}

	}
}
