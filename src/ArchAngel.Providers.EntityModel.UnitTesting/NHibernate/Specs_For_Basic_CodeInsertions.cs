using System.IO;
using ArchAngel.NHibernateHelper.CodeInsertions;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;

namespace ArchAngel.Providers.EntityModel.UnitTesting.NHibernate
{
	[TestFixture]
	public class Specs_For_Basic_CodeInsertions
	{
		[Test]
		public void Test()
		{
			string codeText = File.ReadAllText("Resources\\BasicClass.txt");

			var parser = new CSharpParser();
			parser.ParseCode(codeText);

			var codeRoot = (CodeRoot)parser.CreatedCodeRoot;
			//Actions actions = Test2(codeRoot);//Test1(codeRoot);

			Actions actions = new Actions();

			Entity entity = new EntityImpl("BasicClass");
			entity.AddProperty(new PropertyImpl { Name = "Property5", Type = "Entity" });

			Entity entity1 = new EntityImpl("Class1");
			entity1.AddProperty(new PropertyImpl { Name = "Property5", Type = "Entity" });

			entity.MappedClass = codeRoot.Namespaces[0].Classes[0];
			entity1.MappedClass = codeRoot.Namespaces[0].Classes[1];

			CheckEntity(entity, actions);

			codeText = actions.RunActions(codeText, codeRoot, true);
			actions = new Actions();

			actions.AddAction(new AddAttributeToPropertyAction(entity.MappedClass.Properties[0], new Attribute(codeRoot.Controller){Name = "Attr"}));
			CheckEntity(entity1, actions);

			var output = actions.RunActions(codeText, codeRoot, false);
		}

		private static void CheckEntity(Entity entity, Actions actions)
		{
			foreach (var property in entity.Properties)
			{
                entity.MappedClass.EnsureHasProperty(property.Name.GetCSharpFriendlyIdentifier(), property.OldNames)
					.WithType(property.Type)
					.WithModifiers("public")
					.WithModifiers(property.IsInherited ? "override" : "virtual")
					.WithGetAccessorBody(";")
					.WithSetAccessorBody(";")
					.ApplyTo(actions);
			}
		}
	}
}