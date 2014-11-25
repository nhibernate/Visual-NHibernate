using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public enum KeyTypes
	{
		Component,
		Properties,
		Empty
	}

	public class IEntityKey
	{
		public IEntityKey()
		{
			Properties = new List<IProperty>();
		}

		public KeyTypes KeyType { get; set; }
		public List<IProperty> Properties { get; set; }
		public IEntity Parent { get; set; }
	}
}
