using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ArchAngel.Interfaces.NHibernateEnums;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using hibernatemapping = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.hibernatemapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2
{
	/// <summary>
	/// Contains methods useful for dealing with NHibernate mapping configuration files.
	/// </summary>
	public class Utility
	{
		private static XmlSerializer _HibernateXmlSerializer;

		/// <summary>
		/// 
		/// </summary>
		internal static XmlSerializer HibernateXmlSerializer
		{
			get
			{
				if (_HibernateXmlSerializer == null)
					_HibernateXmlSerializer = new XmlSerializer(typeof(hibernatemapping));

				return _HibernateXmlSerializer;
			}
		}

		/// <summary>
		/// Deserializes a hibernate mapping configuration file (*.hbm.xml)
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static hibernatemapping Open(string file)
		{
			using (TextReader tr = new StreamReader(Slyce.Common.Utility.RemoveXmlEncodingHeader(file)))
				return (hibernatemapping)HibernateXmlSerializer.Deserialize(tr);
		}

		/// <summary>
		/// Serializes a hibernate mapping to disk (typically '*.hbm.xml')
		/// </summary>
		/// <param name="nHibernateMapping"></param>
		/// <param name="file"></param>
		public static void Save(hibernatemapping nHibernateMapping, string file)
		{
			using (TextWriter tw = new StreamWriter(file, false, Encoding.UTF8))
			{
				using (var xtw = new XmlTextWriter(tw))
				{
					xtw.Formatting = Formatting.Indented;
					xtw.Indentation = 1;
					xtw.IndentChar = '\t';
					HibernateXmlSerializer.Serialize(xtw, nHibernateMapping, new XmlSerializerNamespaces(), "utf-8");
					tw.Flush();
				}
			}
		}

		public static string UpdateNHibernateMappingFile(hibernatemapping hm, EntitySetImpl entitySet)
		{
			EntityMapper mapper = new EntityMapper();

			foreach (var entity in entitySet.Entities)
			{
				var newClass = mapper.ProcessEntity(entity);
				hm.AddClass(newClass);
			}
			return hm.ToXml();
		}

		public static bool IsEntityMappedToTables(Entity entity)
		{
			if (entity.MappedTables().Count() > 0)
				return true;

			// Skip if the entity has no mapped tables;
			if (!entity.HasParent && !entity.HasChildren)
				return false;

			if (entity.HasParent)
			{
				if (EntityImpl.DetermineInheritanceTypeWithParent(entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
					return entity.Parent.MappedTables().Count() > 0;
				else
					return false;
			}
			else if (entity.IsAbstract && entity.HasChildren)
				return true;

			return false;
			// Entity has children
			//if (entity.IsAbstract)
		}

		private static bool SkipEntity_CheckParent(Entity entity)
		{
			EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithChildren(entity);

			switch (inheritanceType)
			{
				case EntityImpl.InheritanceType.None:
				case EntityImpl.InheritanceType.TablePerSubClass:
				case EntityImpl.InheritanceType.TablePerClassHierarchy:
					return false;
				case EntityImpl.InheritanceType.TablePerConcreteClass:
				case EntityImpl.InheritanceType.Unsupported:
					return true;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static bool SkipEntity_CheckChild(Entity entity)
		{
			EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithChildren(entity);

			switch (inheritanceType)
			{
				case EntityImpl.InheritanceType.None:
				case EntityImpl.InheritanceType.TablePerConcreteClass:
					return false;
				case EntityImpl.InheritanceType.TablePerClassHierarchy:
				case EntityImpl.InheritanceType.TablePerSubClass:
				case EntityImpl.InheritanceType.Unsupported:
					return true;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static string CreateMappingXMLFrom(
													Entity entity,
													string assemblyName,
													string entityNamespace,
													bool autoImport,
													TopLevelAccessTypes defaultAccess,
													TopLevelCascadeTypes defaultCascade,
													bool defaultLazy)
		{
			hibernatemapping hm = new hibernatemapping
									{
										@namespace = entityNamespace,
										assembly = assemblyName,
										autoimport = autoImport
									};

			if (defaultAccess != TopLevelAccessTypes.property)
				hm.defaultaccess = defaultAccess.ToString();

			if (defaultCascade != TopLevelCascadeTypes.none)
				hm.defaultcascade = defaultCascade.ToString().Replace("_", "-");

			if (!defaultLazy)
				hm.defaultlazy = defaultLazy;

			EntityMapper mapper = new EntityMapper();
			var newClass = mapper.ProcessEntity(entity);
			hm.AddClass(newClass);
			return hm.ToXml().Replace(@"xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" ", "").Replace(@"xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" ", "");
		}
	}
}
