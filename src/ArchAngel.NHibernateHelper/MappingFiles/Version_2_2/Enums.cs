using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2
{
	public enum ReferenceType
	{
		OneToOne, ManyToOne, ManyToMany, Unsupported
	}

	[TemplateEnum]
	public enum CascadeTypes
	{
		None,
		Save_Update,
		Delete,
		All,
		All_Delete_Orphan,
		Delete_Orphan
	}

	public enum AccessTypes
	{
		field,
		property,
		nosetter,
		ClassName
	}

	/// <summary>
	/// See: http://nhforge.org/blogs/nhibernate/archive/2009/02/09/nh2-1-0-new-generators.aspx
	/// </summary>
	public enum GeneratorTypes
	{
		increment,
		identity,
		sequence,
		hilo,
		seqhilo,
		uuid_hex,
		uuid_string,
		guid,
		guid_comb,
		guid_native,
		select,
		sequence_identity,
		trigger_identity,
		counter,
		native,
		assigned,
		foreign
	}

	public static class Enums
	{
		#region Extension Methods
		public static string XmlValue(this CascadeTypes cascadeType)
		{
			return cascadeType.ToString().Replace("_", "-").ToLower();
		}

		public static string ToString(this AccessTypes accessType)
		{
			return accessType.ToString();
		}

		public static string ToString(this GeneratorTypes genType)
		{
			return genType.ToString().Replace('_', '.');
		}
		#endregion

		/// <summary>
		/// Gets a list of CascadeTypes from a comma-separated string - the way they are stored in the 'cascade' 
		/// element - because it supports multi-values.
		/// </summary>
		/// <param name="cascadeString"></param>
		/// <returns></returns>
		public static IList<CascadeTypes> GetCascadeTypes(string cascadeString)
		{
			Type enumType = typeof(CascadeTypes);
			List<CascadeTypes> cascadeTypes = new List<CascadeTypes>();

			foreach (var part in cascadeString.Split(','))
				cascadeTypes.Add((CascadeTypes)Enum.Parse(enumType, part, true));

			return cascadeTypes;
		}

		/// <summary>
		/// Gets a comma-separated string of CascadeTypes. The 'cascade' element requires it in this 
		/// format, as it supports multi-values.
		/// </summary>
		/// <param name="cascadeTypes"></param>
		/// <returns></returns>
		public static string CreateCascadeTypeString(IList<CascadeTypes> cascadeTypes)
		{
			Type enumType = typeof(CascadeTypes);
			var sb = new StringBuilder();

			foreach (var cascadeType in cascadeTypes)
				sb.Append(Enum.GetName(enumType, cascadeType) + ",");

			return sb.ToString().TrimEnd(',');
		}
	}
}
