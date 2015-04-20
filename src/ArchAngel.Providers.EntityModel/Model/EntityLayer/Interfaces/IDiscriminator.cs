
namespace ArchAngel.Providers.EntityModel.Model.EntityLayer.Interfaces
{
	public interface IDiscriminator
	{
		Enums.DiscriminatorTypes DiscriminatorType { get; set; }
		string ColumnName { get; set; }
		string Formula { get; set; }
		bool AllowNull { get; set; }
		bool Force { get; set; }
		bool Insert { get; set; }
	}
}
