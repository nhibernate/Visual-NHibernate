namespace ArchAngel.Providers.EntityModel.Model
{
    /// <summary>
    /// Marker interface. Indicates that this object is a container for other objects.
    /// Mainly used in the UI layer where we do some detection of container types for
    /// different displays.
    /// </summary>
    public interface IItemContainer : IModelObject
    {
    }
}