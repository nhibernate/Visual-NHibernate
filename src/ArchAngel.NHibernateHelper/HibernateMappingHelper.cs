using System;
using System.IO;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;

namespace ArchAngel.NHibernateHelper
{
	public static class HibernateMappingHelper
	{
		public static string GetRelativeFilenameForEntityCSharpFile(ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity entity, string csprojFullFilePath)
		{
			return ArchAngel.NHibernateHelper.HibernateMappingHelper.GetRelativeFilenameForEntityCSharpFile((Entity)entity.ScriptObject, csprojFullFilePath);
		}

		public static string GetRelativeFilenameForEntityCSharpFile(Entity entity, string csprojFullFilePath)
		{
			if (entity.MappedClass != null)
			{
				var filename = entity.EntitySet.MappingSet.CodeParseResults.GetFilenameForParsedClass(entity.MappedClass);

				// filename is absolute, reduce it to relative to the .csproj file path
				return RelativePaths.GetRelativePath(Path.GetDirectoryName(csprojFullFilePath), filename, false);
			}

			return Path.Combine("Model", entity.Name + ".cs");
		}

		public static string ResolveShortClassName(@class hClass, hibernatemapping hm)
		{
			// remove everything after the comma if there is one.
			int indexOfComma = Math.Max(hClass.name.IndexOf(","), hClass.name.Length);
			string className = hClass.name.Substring(0, indexOfComma);

			// If the classname is not fully qualified, return it.
			if (className.Contains(".") == false) return className;

			return className.Substring(className.LastIndexOf(".") + 1);
		}

		public static string ResolveFullClassName(@class hClass, hibernatemapping hm)
		{
			// remove everything after the comma if there is one.
			int indexOfComma = Math.Max(hClass.name.IndexOf(","), hClass.name.Length);
			string className = hClass.name.Substring(0, indexOfComma);

			// If there is no default namespace specified, return the classname
			if (string.IsNullOrEmpty(hm.@namespace))
			{
				return className;
			}

			// If the classname is already fully qualified, return it.
			if (className.Contains(".")) return className;

			// Otherwise prepend the default namespace
			return hm.@namespace + "." + className;
		}
	}
}