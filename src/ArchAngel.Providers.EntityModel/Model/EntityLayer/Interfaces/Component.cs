using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface Component : IModelObject
	{
		string Name { get; set; }
        IList<string> OldNames { get; }
		Entity ParentEntity { get; set; }
		ComponentSpecification Specification { get; set; }
		ReadOnlyCollection<ComponentPropertyMarker> Properties { get; }

		void AddProperty(ComponentPropertyMarker property);
		void RemoveProperty(ComponentProperty property);
		ComponentPropertyMarker GetProperty(string name);
		
		void DeleteSelf();
		
	}
}