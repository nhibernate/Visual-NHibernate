using System;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_Mapping
{
	[TestFixture]
	public class When_Deleting_A_Mapping_And_It_Is_Part_Of_A_MappingSet
	{
		[Test]
		public void It_Is_Deleted()
		{
			MappingSet ms = new MappingSetImpl();
			Mapping m = new MappingImpl();
			ms.AddMapping(m);

			Assert.That(ms.Mappings.Contains(m));

			m.Delete();

			Assert.That(ms.Mappings.Contains(m) == false);
		}
	}

	[TestFixture]
	public class When_Deleting_A_Mapping_And_It_Is_Not_Part_Of_A_MappingSet
	{
		[Test]
		public void It_Does_Not_Throw_An_Exception()
		{
			Mapping m = new MappingImpl();
			m.Delete();
		}
	}
}