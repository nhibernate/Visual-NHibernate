using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Filters
{
	public class RelatedEntityObjectsFilter : DatabaseViewFilter
	{
		public readonly ITable Table;
		public readonly int DegreeOfRelationshipsToShow;

		public RelatedEntityObjectsFilter(SchemaDiagrammerController diagramController, SchemaController schemaController, ITable table, int degreeOfRelationshipsToShow) : base(diagramController, schemaController, table.Database)
		{
			DegreeOfRelationshipsToShow = degreeOfRelationshipsToShow;
			Table = table;
		}

		protected override void RunFilterImpl()
		{
			schemaController.ClearVisibleSet();

			IEnumerable<ITable> relatedObjects = Table.Database.GetRelatedEntities(Table, DegreeOfRelationshipsToShow);

			schemaController.SetEntitiesAndRelationshipsToVisible(new HashSet<IEntity>(relatedObjects.Cast<IEntity>()));
		}

	    public override bool CanRun()
	    {
	        return Table != null && Table.Database != null;
	    }

	    public string ToXml()
		{
			var sb = new StringBuilder();
			var settings = new XmlWriterSettings();
			settings.OmitXmlDeclaration = true;
			var xml = XmlWriter.Create(new StringWriter(sb), settings);

			// ReSharper disable PossibleNullReferenceException
			xml.WriteStartElement("Filter");
			xml.WriteAttributeString("type", "RelatedEntityObjects");
			
			xml.WriteStartElement("Entity");
			xml.WriteAttributeString("database", Table.Database.Name);
			xml.WriteAttributeString("name", Table.Name);
			xml.WriteEndElement();

			xml.WriteStartElement("DegreeOfRelationshipsToShow");
			xml.WriteAttributeString("value", DegreeOfRelationshipsToShow.ToString());
			xml.WriteEndElement();

			xml.WriteEndElement();
			// ReSharper restore PossibleNullReferenceException
			xml.Flush();
			xml.Close();
			return sb.ToString();
		}
	}
}