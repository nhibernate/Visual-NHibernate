using System;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IDiscriminator
	{
		public enum DiscriminatorTypes
		{
			Column,
			Formula
		}
		public IColumn Column { get; set; }
		public string Formula { get; set; }
		public string CSharpType { get; set; }
		public DiscriminatorTypes DiscriminatorType { get; set; }
		//public string Operator { get; set; }
		//public string Value { get; set; }
	}
}
