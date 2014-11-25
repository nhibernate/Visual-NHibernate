using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using log4net;

namespace ArchAngel.Providers.EntityModel.UI.Wizards
{
	public interface IConvertKeyToComponentModelResults
	{
		void ApplyChanges(MappingSet mappingSet, EntityKey key);
		string GetTextualDescription();
	}

	public class ConvertKeyToExistingComponentResults : IConvertKeyToComponentModelResults
	{
		private readonly ComponentSpecification existingSpecToUse;
		private readonly string newComponentName;
		private readonly bool deleteExistingProperties;
		private readonly List<KeyValuePair<string, string>> propertyMappings;

		private static readonly ILog log = LogManager.GetLogger(typeof(ConvertKeyToExistingComponentResults));

		public ConvertKeyToExistingComponentResults(ComponentSpecification existingSpecToUse, string newComponentName, bool deleteExistingProperties, IEnumerable<KeyValuePair<string, string>> propertyMappings)
		{
			this.existingSpecToUse = existingSpecToUse;
			this.newComponentName = newComponentName;
			this.deleteExistingProperties = deleteExistingProperties;
			this.propertyMappings = propertyMappings.ToList();
		}


		public void ApplyChanges(MappingSet mappingSet, EntityKey key)
		{
			// Create new Component
			var component = existingSpecToUse.CreateImplementedComponentFor(key.Parent, newComponentName);

			// Set mapped column on the new Component's properties.
			foreach(var pair in propertyMappings)
			{
				var newProperty = component.GetProperty(pair.Key);
				var oldProperty = key.Properties.FirstOrDefault(p => p.Name == pair.Value);

				if(oldProperty == null)
				{
					// Something went horribly wrong. We have a Property that has been mapped,
					// but it doesn't actually exist. I am chosing to ignore this here, but log it.
					log.ErrorFormat("Property {0} was mapped in the ConvertKeyToComponent wizard, but doesn't actually exist in the model.", pair.Value);
					continue;
				}

				newProperty.SetMappedColumn(oldProperty.MappedColumn());
			}

			// Delete existing properties if needed.
			if(deleteExistingProperties)
			{
				foreach(var property in key.Properties.ToList())
				{
					property.DeleteSelf();
				}
			}

			key.Component = component;
		}

		public string GetTextualDescription()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("Use the existing Component Definition called {0}.", existingSpecToUse.Name).AppendLine().AppendLine();
			sb.AppendFormat(
				"Add a new usage of this Definition called {0} to the Entity. This is the name of the property that will be generated on the Entity class for the Key.", newComponentName)
				.AppendLine().AppendLine();
			sb.Append("Take the columns mapped to the previous properties in the key and map them to the new Component's properties.").AppendLine().AppendLine();

			if (deleteExistingProperties)
				sb.Append("Delete the properties that made up the original Key.");

			return sb.ToString();
		}
	}

	public class ConvertKeyToNewComponentResults : IConvertKeyToComponentModelResults
	{
		private readonly string newComponentSpecificationName;
		private readonly string newComponentName;
		private readonly bool deleteExistingProperties;

		public ConvertKeyToNewComponentResults(string newComponentSpecificationName, string newComponentName, bool deleteExistingProperties)
		{
			this.newComponentSpecificationName = newComponentSpecificationName;
			this.newComponentName = newComponentName;
			this.deleteExistingProperties = deleteExistingProperties;
		}

		public void ApplyChanges(MappingSet mappingSet, EntityKey key)
		{
			// Create the new Spec
			ComponentSpecification spec = new ComponentSpecificationImpl(newComponentSpecificationName);

			var properties = key.Properties.ToList();
			foreach (var property in properties)
			{
				var newProperty = ComponentPropertyImpl.CreateFromProperty(property);
				spec.AddProperty(newProperty);
			}

			mappingSet.EntitySet.AddComponentSpecification(spec);

			// Create the new Component
			var component = spec.CreateImplementedComponentFor(key.Parent, newComponentName);

			// Map the old property's columns to the new component
			for (int i = 0; i < properties.Count; i++)
			{
				var property = properties[i];
				var newProperty = spec.Properties[i];

				var componentProperty = component.GetProperty(newProperty.Name);
				mappingSet.ChangeMappingFor(componentProperty).To(property.MappedColumn());
			}

			// Delete old properties
			if(deleteExistingProperties)
			{
				foreach(var property in properties)
				{
					property.DeleteSelf();
				}
			}

			// Set the Key to use the new component.
			key.Component = component;
		}

		public string GetTextualDescription()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat(
				"Create a new Component Defintion called {0}. This is the name of the class that will be generated for this Component.", newComponentSpecificationName)
				.AppendLine().AppendLine();
			sb.AppendFormat(
				"Create a new Component from that Definition called {0}. This is the name of the property that will be generated on the Entity class for the Key.", newComponentName)
				.AppendLine().AppendLine();
			sb.Append(
				"Take the columns mapped to the previous properties in the key and map them to the new Component's properties.")
				.AppendLine().AppendLine();
			if (deleteExistingProperties)
				sb.Append("Delete the properties that made up the original Key.");

			return sb.ToString();
		}
	}
}