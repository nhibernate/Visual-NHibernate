using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_DatabaseMergeResults
{
	[TestFixture]
	public class When_Merging_Two_Results
	{
		[Test]
		public void All_Of_The_Operations_Are_Put_Into_The_Result()
		{
			var columnOp = MockRepository.GenerateMock<MergeOperation<IColumn>>();
			var tableOp = MockRepository.GenerateMock<MergeOperation<ITable>>();
			var keyOp = MockRepository.GenerateMock<MergeOperation<IKey>>();
			var indexOp = MockRepository.GenerateMock<MergeOperation<IIndex>>();
			
			DatabaseMergeResult result1 = new DatabaseMergeResult();
			result1.AddColumnOperation(columnOp);
			result1.AddTableOperation(tableOp);
			result1.AddKeyOperation(keyOp);
			result1.AddIndexOperation(indexOp);

			DatabaseMergeResult result2 = new DatabaseMergeResult();
			result2.CopyFrom(result1);

			Assert.That(result1.ColumnOperations, Has.Count(1));
			Assert.That(result1.TableOperations, Has.Count(1));
			Assert.That(result1.KeyOperations, Has.Count(1));
			Assert.That(result1.IndexOperations, Has.Count(1));

			Assert.That(result1.ColumnOperations.ElementAt(0), Is.SameAs(columnOp));
			Assert.That(result1.TableOperations.ElementAt(0), Is.SameAs(tableOp));
			Assert.That(result1.KeyOperations.ElementAt(0), Is.SameAs(keyOp));
			Assert.That(result1.IndexOperations.ElementAt(0), Is.SameAs(indexOp));
		}
	}
}
