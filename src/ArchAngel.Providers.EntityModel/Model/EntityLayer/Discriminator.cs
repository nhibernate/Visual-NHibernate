using ArchAngel.Providers.EntityModel.Model.EntityLayer.Interfaces;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	public class Discriminator : IDiscriminator
	{
		public Discriminator()
		{
			Insert = true;
			DiscriminatorType = Enums.DiscriminatorTypes.Column;
		}

		public Enums.DiscriminatorTypes DiscriminatorType { get; set; }
		public string ColumnName { get; set; }

		/// <summary>
		/// Eg: "case when CLASS_TYPE in ('a', 'b', 'c') then 0 else 1 end"
		/// </summary>
		public string Formula { get; set; }
		public bool AllowNull { get; set; }

		/// <summary>
		///  "force" NHibernate to specify allowed discriminator values even when retrieving all instances of the root class.
		/// </summary>
		public bool Force { get; set; }

		/// <summary>
		/// set this to false if your discriminator column is also part of a mapped composite identifier.
		/// </summary>
		public bool Insert { get; set; }

	}
}
