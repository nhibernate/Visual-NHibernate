using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.UI.Wizards
{
	public interface IConvertKeyToComponentModelInformation
	{
		IEnumerable<ComponentSpecification> GetComponentSpecifications();
		IEnumerable<KeyValuePair<string, string>> AutoMapComponentAndKey(ComponentSpecification spec);
		IEnumerable<string> GetKeyProperties();
	}

	public class ConvertKeyToComponentModelInformation : IConvertKeyToComponentModelInformation
	{
		private readonly MappingSet mappingSet;
		private readonly EntityKey _key;

		public ConvertKeyToComponentModelInformation(MappingSet mappingSet, EntityKey key)
		{
			this.mappingSet = mappingSet;
			_key = key;
		}

		public IEnumerable<ComponentSpecification> GetComponentSpecifications()
		{
			return mappingSet.EntitySet.ComponentSpecifications;
		}

		public IEnumerable<KeyValuePair<string, string>> AutoMapComponentAndKey(ComponentSpecification spec)
		{
			foreach(var property in spec.Properties)
			{
				var propertyName = property.Name;
				// Try find something by name
				var possibleProperty = _key.Properties.FirstOrDefault(p => p.Name == propertyName);
				if(possibleProperty != null)
				{
					yield return new KeyValuePair<string, string>(propertyName, possibleProperty.Name);
					continue;
				}

				// Try find by type. This is not the best way of doing it, but might find some matches.
				string propertyType = property.Type;
				possibleProperty = _key.Properties.FirstOrDefault(p => p.Type == propertyType);
				if (possibleProperty != null)
				{
					yield return new KeyValuePair<string, string>(propertyName, possibleProperty.Name);
					continue;
				}

				yield return new KeyValuePair<string, string>(propertyName, "");
			}
		}

		public IEnumerable<string> GetKeyProperties()
		{
			return _key.Properties.Select(p => p.Name);
		}
	}
}