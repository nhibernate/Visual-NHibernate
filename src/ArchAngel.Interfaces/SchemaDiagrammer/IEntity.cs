using System.Collections.Generic;
using System.ComponentModel;

namespace ArchAngel.Interfaces.SchemaDiagrammer
{
	public interface IEntity : INotifyPropertyChanged
	{
		string EntityName { get; set; }
		IEnumerable<IRelationship> Relationships { get; }
	}
}