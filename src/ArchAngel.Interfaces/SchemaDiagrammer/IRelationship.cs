using System.ComponentModel;

namespace ArchAngel.Interfaces.SchemaDiagrammer
{
	public interface IRelationship : INotifyPropertyChanged
	{
		IEntity SourceEntity { get; set; }
		IEntity TargetEntity { get; set; }
		Cardinality SourceCardinality { get; set; }
		Cardinality TargetCardinality { get; set; }
		string Name { get; set; }
	}
}