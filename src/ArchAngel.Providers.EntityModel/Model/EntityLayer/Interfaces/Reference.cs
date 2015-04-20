using System;
using System.Collections.Generic;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Attributes;
using ArchAngel.Interfaces.SchemaDiagrammer;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	/// <summary>
	/// Represents a reference between two Entities.
	/// An example:
	/// Using this reference
	/// Reference 
	///	{
	///		Entity1 = "Entity1"
	///		Entity2 = "Entity2"
	///		Cardinality1 = 1
	///		Cardinality2 = 0..*
	///		End1Enabled = true
	///		End2Enabled = true
	///		End1Name = "ParentEntity1"
	///		End2Name = "Entity2Collection"
	/// }
	/// 
	/// We might generate the following two entities:
	/// 
	/// public class Entity1
	/// {
	///		public List&lt;Entity2&gt; Entity2Collection;
	/// 
	///		...
	/// }
	/// public class Entity2
	/// {
	///		public Entity1 ParentEntity1;
	/// 
	///		...
	/// }
	/// 
	/// </summary>
	[ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface Reference : IModelObject, IRelationship
	{
		/// <summary>
		/// Used to identify the Reference when deserialising it.
		/// </summary>
		Guid Identifier { get; set; }

		EntitySet EntitySet { get; set; }
		Entity Entity1 { get; set; }
		Entity Entity2 { get; set; }
		/// <summary>
		/// The number of Entity1 objects that are participating in this reference
		/// </summary>
		Cardinality Cardinality1 { get; set; }
		/// <summary>
		/// The number of Entity2 objects that are participating in this reference
		/// </summary>
		Cardinality Cardinality2 { get; set; }
		/// <summary>
		/// If true, then we should create a way to reference Entity1 from Entity2
		/// </summary>
		bool End1Enabled { get; set; }
		/// <summary>
		/// If true, then we should create a way to reference Entity2 from Entity1
		/// </summary>
		bool End2Enabled { get; set; }
		/// <summary>
		/// The name of the reference to Entity1 in Entity2
		/// </summary>
		string End1Name { get; set; }
		/// <summary>
		/// The name of the reference to Entity2 in Entity1
		/// </summary>
		string End2Name { get; set; }
		/// <summary>
		/// The old names of the reference to Entity1 in Entity2
		/// </summary>
		IList<string> OldEnd1Names { get; }
		/// <summary>
		/// The old names of the reference to Entity2 in Entity1
		/// </summary>
		IList<string> OldEnd2Names { get; }
		/// <summary>
		/// True if Entity1 is the same Entity as Entity2
		/// </summary>
		bool SelfReference { get; }

		/// <remarks>
		/// Requires the Parent EntitySet to have been set to something, otherwise it will not be added to
		/// its collection.
		/// </remarks>
		/// <param name="entity_1"></param>
		/// <param name="entity_2"></param>
		void AddThisTo(Entity entity_1, Entity entity_2);

		void DeleteSelf();

		/// <summary>
		/// Gets or sets whether the properties in the foreign key should be included in the entity's property list.
		/// </summary>
		bool IncludeForeignKey { get; set; }
	}
}