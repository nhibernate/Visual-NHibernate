namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	public interface IPropertyContainer : IItemContainer
	{
		void AddProperty(Property property);
		void RemoveProperty(Property property);
		Property GetProperty(string propertyName);
	}
}