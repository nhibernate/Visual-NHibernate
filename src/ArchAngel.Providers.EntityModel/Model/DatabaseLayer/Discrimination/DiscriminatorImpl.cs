using System.Collections.Generic;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination
{
	public class DiscriminatorImpl : Discriminator
	{
		public Grouping RootGrouping { get; set; }

		public IEnumerable<ILeaf> Children { get { yield return RootGrouping; } }

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.ProcessDiscriminator(this);
		}
	}
}