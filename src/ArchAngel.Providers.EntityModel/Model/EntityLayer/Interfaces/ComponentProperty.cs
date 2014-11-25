namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public interface ComponentProperty : IModelObject
	{
		ComponentSpecification Specification { get; set; }
		string Name { get; set; }
		string Type { get; set; }
		ValidationOptions ValidationOptions { get; set; }
		void DeleteSelf();
	}
}
