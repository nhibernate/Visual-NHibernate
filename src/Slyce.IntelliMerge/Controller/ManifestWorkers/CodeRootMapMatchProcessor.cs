using System.Collections.Generic;
using System.Xml;
using Con = Slyce.IntelliMerge.Controller.ManifestWorkers.ManifestConstants;

namespace Slyce.IntelliMerge.Controller.ManifestWorkers
{
    /// <summary>
    /// The class provides methods that load and save custom matching of ICodeRoot elements.
    /// The LoadCustomMappings method is used to load custom mappings from an XmlDocument and 
    /// apply them to the given CodeRootMap. The SaveCustomMappings method will work through a
    /// CodeRootMap, extract information about the custom mappings that have been applied and
    /// 
    /// </summary>
    public class CodeRootMapMatchProcessor
    {
        

        /// <summary>
        /// Loads custom mappings from the XmlDocument and applies them to the CodeRootMap.
        /// Returns a list of mappings that could not be applied.
        /// </summary>
        /// <param name="doc">The XmlDocument to load from.</param>
        /// <param name="map">The CodeRootMap to apply the mappings to.</param>
        /// <param name="filenameForMap">The filename to use when looking for mappings for this CodeRootMap</param>
        public void LoadCustomMappings(XmlDocument doc, CodeRootMap map, string filenameForMap)
        {
            string xpathQuery = string.Format("{0}/{1}/{2}/{3}[@filename='{4}']", Con.ManifestElement, Con.MappingsElement, Con.CodeRootMappingsElement, Con.CodeRootMappingElement, filenameForMap);

            XmlNodeList nodes = doc.SelectNodes(xpathQuery);
            if (nodes == null) return;
            
            foreach(XmlNode node in nodes)
            {
                XmlNode userNode = node.SelectSingleNode(Con.UserObjectElement);
                XmlNode newgNode = node.SelectSingleNode(Con.NewGenObjectElement);
                XmlNode prevNode = node.SelectSingleNode(Con.PrevGenObjectElement);

                map.MatchConstructs(string.IsNullOrEmpty(userNode.InnerText) ? null : userNode.InnerText,
                                    string.IsNullOrEmpty(newgNode.InnerText) ? null : newgNode.InnerText,
                                    string.IsNullOrEmpty(prevNode.InnerText) ? null : prevNode.InnerText);
            }
        }

        /// <summary>
        /// Saves the custom mappings in the given CodeRootMap to the XmlDocument.
        /// </summary>
        /// <param name="doc">The XmlDocument to save the mappings to.</param>
        /// <param name="map">The CodeRootMap to inspect for custom mappings.</param>
        /// <param name="filenameForMap">The filename to use when saving mappings for this CodeRootMap</param>
        public void SaveCustomMappings(XmlDocument doc, CodeRootMap map, string filenameForMap)
        {
            // Get each of the nodes marked as a custom match.
			// Note that it is vitally important that these are in order.
			// The parent nodes should come before the child nodes.
			IList<CodeRootMapNode> customNodes = map.GetCustomMatchedNodes();

			// Create or find the node that we need to add to.
        	XmlNode root = doc.SelectSingleNode(Con.ManifestElement);
			if(root == null)
			{
				root = doc.CreateElement(Con.ManifestElement);
				doc.AppendChild(root);
			}

			XmlNode mappingsNode = root.SelectSingleNode(Con.MappingsElement);
			if(mappingsNode == null)
			{
				mappingsNode = doc.CreateElement(Con.MappingsElement);
				root.AppendChild(mappingsNode);
			}
			
			XmlNode crMappingsNode = root.SelectSingleNode(Con.CodeRootMappingsElement);
			if(crMappingsNode == null)
			{
				crMappingsNode = doc.CreateElement(Con.CodeRootMappingsElement);
				mappingsNode.AppendChild(crMappingsNode);
			}

			// Clear the old custom mappings for this file.
			XmlNodeList oldMappings = crMappingsNode.SelectNodes(string.Format("{0}[@{1}='{2}']", 
				Con.CodeRootMappingElement, Con.CodeRootMappingFilenameAttribute, filenameForMap));
			if(oldMappings != null)
			{
				foreach(XmlNode node in oldMappings)
				{
					node.ParentNode.RemoveChild(node);
				}
			}

			// Add each of them to the custom map document
			foreach(CodeRootMapNode node in customNodes)
			{
				XmlNode crmnMappingNode = doc.CreateElement(Con.CodeRootMappingElement);
				XmlAttribute attr = doc.CreateAttribute(Con.CodeRootMappingFilenameAttribute);
				attr.Value = filenameForMap;
				crmnMappingNode.Attributes.Append(attr);

				XmlNode userNode = doc.CreateElement(ManifestConstants.UserObjectElement);
				userNode.InnerText = node.UserObj == null ? "" : node.UserObj.FullyQualifiedIdentifer;

				XmlNode newgNode = doc.CreateElement(ManifestConstants.NewGenObjectElement);
				newgNode.InnerText = node.NewGenObj == null ? "" : node.NewGenObj.FullyQualifiedIdentifer;

				XmlNode prevNode = doc.CreateElement(ManifestConstants.PrevGenObjectElement);
				prevNode.InnerText = node.PrevGenObj == null ? "" : node.PrevGenObj.FullyQualifiedIdentifer;

				crmnMappingNode.AppendChild(userNode);
				crmnMappingNode.AppendChild(newgNode);
				crmnMappingNode.AppendChild(prevNode);

				crMappingsNode.AppendChild(crmnMappingNode);
			}
        }
    }
}