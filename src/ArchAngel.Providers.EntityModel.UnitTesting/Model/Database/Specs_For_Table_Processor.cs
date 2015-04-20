using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Table_Procesor
{
    [TestFixture]
    public class When_Adding_A_Table_At_The_Start_Of_The_List
    {
        [Test]
        public void The_Tables_Still_Order_Correctly()
        {
            TableProcessor processor = new TableProcessor();

            Table leftTable = new Table("Table1");
            Table rightTable = new Table("Table1");

            var leftTables = new List<ITable> {leftTable};
            var rightTables = new List<ITable> { rightTable, new Table("aaaTable2") };
            
            var tableSet = processor.GetEqualTables(leftTables, rightTables);
            
            Assert.That(tableSet.Count(), Is.EqualTo(1));
            Assert.That(tableSet.First().Key, Is.SameAs(leftTable));
            Assert.That(tableSet.First().Value, Is.SameAs(rightTable));
        }
    }
}
