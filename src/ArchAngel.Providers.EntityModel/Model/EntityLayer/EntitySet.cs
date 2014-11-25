using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false, PreviewDisplayProperty = "")]
	public class EntitySetImpl : ModelObject, EntitySet
	{
		public event CollectionChangeHandler<Entity> EntitiesChanged;
		public event CollectionChangeHandler<Reference> ReferencesChanged;
		public event CollectionChangeHandler<ComponentSpecification> ComponentSpecsChanged;

		private readonly List<Entity> entities = new List<Entity>();
		private readonly List<Reference> references = new List<Reference>();
		private readonly List<ComponentSpecification> components = new List<ComponentSpecification>();

		public override string DisplayName
		{
			get { return "Entities"; }
		}

		public ReadOnlyCollection<Entity> Entities
		{
			get
			{
				entities.Sort((x, y) => string.Compare(x.Name, y.Name));
				return entities.AsReadOnly();
			}
		}

		public ReadOnlyCollection<string> Schemas
		{
			get
			{
				List<string> schemas = new List<string>();

				foreach (var entity in entities)
				{
					if (!schemas.Contains(entity.Schema))
						schemas.Add(entity.Schema);
				}
				return schemas.AsReadOnly();
			}
		}

		public ReadOnlyCollection<ComponentSpecification> ComponentSpecifications
		{
			get { return components.AsReadOnly(); }
		}

		public ReadOnlyCollection<Reference> References
		{
			get { return references.AsReadOnly(); }
		}

		private MappingSet _MappingSet;

		public MappingSet MappingSet
		{
			get { return _MappingSet; }
			set { _MappingSet = value; }
		}

		public bool IsEmpty
		{
			get
			{
				return entities.Count == 0;
			}
		}

		public virtual void AddEntity(Entity entity)
		{
			if (entities.Contains(entity))
				return;

			entities.Add(entity);
			entity.EntitySet = this;

			RaisePropertyChanged("Entities");
			EntitiesChanged.RaiseAdditionEventEx(this, entity);
		}

		public virtual void AddReference(Reference reference)
		{
			if (references.Contains(reference))
				return;

			references.Add(reference);
			reference.EntitySet = this;
			RaisePropertyChanged("References");
			ReferencesChanged.RaiseAdditionEventEx(this, reference);
		}

		public void AddComponentSpecification(ComponentSpecification componentSpec)
		{
			if (components.Contains(componentSpec))
				return;

			components.Add(componentSpec);
			componentSpec.EntitySet = this;
			RaisePropertyChanged("ComponentSpecifications");
			ComponentSpecsChanged.RaiseAdditionEventEx(this, componentSpec);
		}

		public virtual void DeleteEntity(Entity entity)
		{
			foreach (var reference in entity.DirectedReferences)
			{
				reference.ToEntity.RemoveReference(reference.Reference);
				RemoveReference(reference.Reference);
			}
			entity.ClearReferences();

			if (MappingSet != null)
				MappingSet.DeleteEntity(entity);

			if (entity.Parent != null)
				entity.Parent.RemoveChild(entity);

			for (int childCounter = entity.Children.Count - 1; childCounter >= 0; childCounter--)
			{
				Entity childEntity = entity.Children[childCounter];
				entity.RemoveChild(childEntity);

				// Add the ID property back to the child entity
				for (int i = childEntity.Key.Properties.ToList().Count - 1; i >= 0; i--)
				{
					if (childEntity.Properties.Count(p => p == childEntity.Key.Properties.ElementAt(i)) == 0)
						childEntity.AddProperty(childEntity.Key.Properties.ElementAt(i));
				}
				// Add any hidden properties back to the child
				foreach (var hiddenProperty in childEntity.PropertiesHiddenByAbstractParent)
					hiddenProperty.IsHiddenByAbstractParent = false;
			}
			RemoveEntity(entity);
			EntitiesChanged.RaiseDeletionEventEx(this, entity);
			RaisePropertyChanged("Entities");
		}

		public void RemoveEntity(Entity entity)
		{
			entities.Remove(entity);
			entity.EntitySet = null;

			EntitiesChanged.RaiseDeletionEventEx(this, entity);
			RaisePropertyChanged("Entities");
		}

		public void RemoveReference(Reference reference)
		{
			references.Remove(reference);
			reference.EntitySet = null;

			ReferencesChanged.RaiseDeletionEventEx(this, reference);
			RaisePropertyChanged("References");
		}

		public void RemoveComponentSpecification(ComponentSpecification spec)
		{
			components.Remove(spec);
			spec.EntitySet = null;
			ComponentSpecsChanged.RaiseDeletionEventEx(this, spec);
			RaisePropertyChanged("ComponentSpecifications");
		}

		public void DeleteReference(Reference impl)
		{
			RemoveReference(impl);
			if (MappingSet != null)
				MappingSet.DeleteReference(impl);
		}

		public void DeleteProperty(Property property)
		{
			if (MappingSet != null)
				MappingSet.RemoveMappingsContaining(property);
		}

		public Entity GetEntity(string name)
		{
			return entities.Find(e => e.Name == name);
		}

		public ComponentSpecification GetComponentSpecification(string name)
		{
			return components.Find(c => c.Name == name);
		}

		public IEnumerable<Entity> GetRelatedEntities(Entity entity, int degree)
		{
			var related = new HashSet<Entity>();

			related.Add(entity);

			if (degree == 0)
				return related;

			var relatedTables = new List<Entity>();
			foreach (var reference in entity.References)
			{
				Entity item = reference.Entity1 == entity ?
					reference.Entity2 : reference.Entity1;
				relatedTables.Add(item);
				related.Add(item);
			}

			if (entity.Parent != null)
			{
				related.Add(entity.Parent);
				relatedTables.Add(entity.Parent);
			}

			foreach (var ent in entity.Children)
			{
				related.Add(ent);
				relatedTables.Add(ent);
			}

			if (degree > 1)
			{
				foreach (var relatedTable in relatedTables)
				{
					foreach (var re in GetRelatedEntities(relatedTable, degree - 1))
						related.Add(re);
				}
			}

			return related;
		}

		public Entity this[int i]
		{
			get { return entities[i]; }
		}

		public int Count
		{
			get { return entities.Count; }
		}
	}
}