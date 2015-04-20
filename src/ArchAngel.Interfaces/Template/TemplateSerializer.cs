using System;
using System.Text;
using System.Xml;

namespace ArchAngel.Interfaces.Template
{
	public class TemplateSerializer
	{
		public static string Serialise(TemplateProject project)
		{
			return Serialise(writer => SerialiseTemplateProject(project, writer));
		}

		private static string Serialise(Action<XmlWriter> writeAction)
		{
			var sb = new StringBuilder();
			// If OmitXmlDecl is not set to true, an <?xml> node will be placed at the start
			// of the snippet, which is not what we want.
			XmlWriter writer = GetWriter(sb, new XmlWriterSettings { OmitXmlDeclaration = true });

			writeAction(writer);

			writer.Close();
			return sb.ToString();
		}

		private static XmlWriter GetWriter(StringBuilder sb, XmlWriterSettings settings)
		{
			settings.Indent = true;
			settings.IndentChars = "\t";
			settings.OmitXmlDeclaration = false;
			XmlWriter writer = XmlWriter.Create(sb, settings);
			if (writer == null)
				throw new InvalidOperationException("Couldn't create an XML Writer. ");
			return writer;
		}

		private static void SerialiseTemplateProject(TemplateProject project, XmlWriter writer)
		{
			writer.WriteStartElement("visual-nhibernate-template");
			writer.WriteAttributeString("version", 1.ToString());
			writer.WriteAttributeString("delimiter-style", project.Delimiter.ToString());

			foreach (var subFolder in project.OutputFolder.Folders)
				SerialiseFolder(subFolder, writer);

			foreach (var file in project.OutputFolder.Files)
				SerialiseFile(file, writer);

			foreach (var file in project.OutputFolder.StaticFiles)
				SerialiseStaticFile(file, writer);

			writer.WriteEndElement();
		}

		private static void SerialiseFolder(Folder folder, XmlWriter writer)
		{
			writer.WriteStartElement("folder");
			writer.WriteAttributeString("name", folder.Name);
			writer.WriteAttributeString("iterator", folder.Iterator.ToString());

			foreach (var subFolder in folder.Folders)
				SerialiseFolder(subFolder, writer);

			foreach (var file in folder.Files)
				SerialiseFile(file, writer);

			foreach (var file in folder.StaticFiles)
				SerialiseStaticFile(file, writer);

			writer.WriteEndElement();
		}

		private static void SerialiseFile(File file, XmlWriter writer)
		{
			writer.WriteStartElement("file");
			writer.WriteAttributeString("id", file.Id.ToString());
			writer.WriteAttributeString("name", file.Name);
			writer.WriteAttributeString("iterator", file.Iterator.ToString());
			writer.WriteAttributeString("syntax", file.Script.Syntax.ToString());
			writer.WriteAttributeString("encoding", file.Encoding.EncodingName);
			//SerialiseScript(file.Script, writer);
			writer.WriteEndElement();
		}

		private static void SerialiseStaticFile(StaticFile file, XmlWriter writer)
		{
			writer.WriteStartElement("static-file");
			writer.WriteAttributeString("id", file.Id.ToString());
			writer.WriteAttributeString("name", file.Name);
			writer.WriteAttributeString("iterator", file.Iterator.ToString());
			writer.WriteAttributeString("resource-name", file.ResourceName);
			writer.WriteEndElement();
		}

		//private static void SerialiseScript(Script script, XmlWriter writer)
		//{
		//    writer.WriteStartElement("script");
		//    writer.WriteAttributeString("syntax", script.Syntax.ToString());
		//    writer.WriteCData(script.Body);
		//    writer.WriteEndElement();
		//}
	}
}
