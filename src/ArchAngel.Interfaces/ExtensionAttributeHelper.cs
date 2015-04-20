using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.Interfaces
{
	public static class ExtensionAttributeHelper
	{
		public static IEnumerable<Type> GetAllTemplateEnumTypes(IEnumerable<Assembly> assemblies)
		{
			var types = GetTypesFromAssemblies(assemblies).Where(t => t.IsEnum);
			foreach(var type in types)
			{
				if (HasTemplateEnumAttribute(type))
					yield return type;
			}
		}

		public static bool HasTemplateEnumAttribute(Type type)
		{
			return type.GetCustomAttributes(true).Any(a => a.GetType().FullName == TemplateEnumAttributeName);
		}

		public static object GetNullValueFromEnum(Type enumType)
		{
			foreach(var value in Enum.GetValues(enumType))
			{
				FieldInfo fieldInfo = enumType.GetField(value.ToString());

				if (HasNullValueAttribute(fieldInfo))
					return value;
			}
			return null;
		}

		public static bool HasNullValueAttribute(Type enumType, object enumValue)
		{
			FieldInfo fieldInfo = enumType.GetField(enumValue.ToString());
			return fieldInfo.GetCustomAttributes(false).Any(a => a.GetType().FullName == NullValueEnumAttributeName);
		}

		public static bool HasNullValueAttribute(FieldInfo fieldInfo)
		{
			return fieldInfo.GetCustomAttributes(false).Any(a => a.GetType().FullName == NullValueEnumAttributeName);
		}

		public static object GetDefaultEnumValue(Type type)
		{
			foreach(var enumValue in Enum.GetValues(type))
			{
				if (HasNullValueAttribute(type, enumValue))
					return enumValue;
			}

			return null;
		}

		private static IEnumerable<Type> GetTypesFromAssemblies(IEnumerable<Assembly> assemblies)
		{
			foreach(var assembly in assemblies)
			{
				foreach(var type in assembly.GetTypes())
				{
					yield return type;
				}
			}
		}

		public static IEnumerable<Type> GetAllExtendableTypes(IEnumerable<Assembly> assemblies)
		{
			var types = GetArchAngelEditorBrowsableTypes(assemblies).ToList();

			foreach(var type in types)
			{
				if (CanAddVirtualProperties(type))
					yield return type;
				else if(HasExtensionAttribute(type))
					yield return type;
			}
		}

		public static bool CanAddVirtualProperties(Type type)
		{
			if (type.IsClass == false && type.IsInterface == false) return false;

			ArchAngelEditorAttribute browserAttr = GetBrowserAttribute(type);

			return browserAttr != null && browserAttr.VirtualPropertiesAllowed;
		}

		private static ArchAngelEditorAttribute GetBrowserAttribute(Type type)
		{
			object[] attributes = type.GetCustomAttributes(false);
			object browserAttrObj = attributes.FirstOrDefault(a => a.GetType().FullName == ArchAngelEditorAttributeName);

			return browserAttrObj as ArchAngelEditorAttribute;
		}

		public static IEnumerable<Type> GetAlternativeForms(Type type)
		{
			var browserAttr = GetBrowserAttribute(type);
			if (browserAttr == null) return new Type[0];
			
			return browserAttr.AlternativeForms ?? new Type[0];
		}

		public static bool IsGeneratorIterator(Type type)
		{
			if (type.IsClass == false && type.IsInterface == false) return false;

			object[] attributes = type.GetCustomAttributes(false);
			object browserAttrObj = attributes.FirstOrDefault(a => a.GetType().FullName == ArchAngelEditorAttributeName);

			var browserAttr = browserAttrObj as ArchAngelEditorAttribute;

			return browserAttr != null && browserAttr.IsGeneratorIterator;
		}

		public static IEnumerable<Type> GetGeneratorIteratorTypes(IEnumerable<Assembly> assemblies)
		{
			foreach(var assembly in assemblies)
			{
				if (ProviderInfo.IsProvider(assembly) == false) continue;

				var types = assembly.GetTypes();

				foreach (var type in types)
				{
					if (IsGeneratorIterator(type))
						yield return type;
				}
			}
		}

        public static IEnumerable<Type> GetArchAngelEditorBrowsableTypes(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                if (!ProviderInfo.IsProvider(assembly)) continue;

                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (HasBrowserAttribute(type))
                        yield return type;
                }
            }
        }

		private static readonly string EditorAttributeFullName = typeof(ArchAngelEditorAttribute).FullName;
	    private static bool HasBrowserAttribute(ICustomAttributeProvider type)
	    {
            var allAttributes = type.GetCustomAttributes(false);
			return allAttributes.Any(a => a.GetType().FullName == EditorAttributeFullName);
	    }

	    public static IEnumerable<Type> GetApiExtensionTypes(IEnumerable<Assembly> assemblies)
		{
			foreach (var assembly in assemblies)
			{
			    if (!ProviderInfo.IsProvider(assembly)) continue;

			    var types = assembly.GetTypes();

			    foreach (var type in types)
			    {
			        if(HasExtensionAttribute(type))
			            yield return type;
			    }
			}
		}

		private static readonly Type ApiExtensionType = typeof(ApiExtensionAttribute);
	    public static bool HasExtensionAttribute(Type type)
		{
	        foreach (var method in type.GetMethods())
	        {
	            if (HasApiExtensionAttribute(method)) return true;
	        }
	        return false;
	    }

		public static bool HasApiExtensionAttribute(MethodInfo method)
		{
			var allAttributes = method.GetCustomAttributes(false);

			return allAttributes.Any(a => a.GetType().FullName == ApiExtensionType.FullName);
		}

		public static IEnumerable<MethodInfo> GetExtendableMethods(Type type)
		{
			foreach (var method in type.GetMethods())
			{
				if(HasApiExtensionAttribute(method))
					yield return method;
			}
		}

		public static string GetExtensionDescription(MethodInfo method)
		{
			object attribute = GetApiExtensionAttribute(method);

			Type type = attribute.GetType();
			return (string)type.InvokeMember("Description", BindingFlags.GetProperty, null, attribute, null);
		}

		private static object GetApiExtensionAttribute(MethodInfo method)
		{
			object attribute = null;
			foreach (var att in method.GetCustomAttributes(false))
			{
                if (att.GetType().FullName == ApiExtensionType.FullName)
				{
					attribute = att;
					break;
				}
			}

			if (attribute == null)
				throw new ArgumentException(String.Format("Method {0} does not have the ApiExtension attribute", method.Name), "method");

			return attribute;
		}

		public static string GetDefaultCode(MethodInfo method)
		{
			var attribute = GetApiExtensionAttribute(method);

			var type = attribute.GetType();
			return (string)type.InvokeMember("DefaultCode", BindingFlags.GetProperty, null, attribute, null);
		}

		public static readonly string ArchAngelEditorAttributeName = typeof(ArchAngelEditorAttribute).FullName;
		public static readonly string TemplateEnumAttributeName = typeof(TemplateEnumAttribute).FullName;
		public static readonly string NullValueEnumAttributeName = typeof(NullValueAttribute).FullName;
	}
}