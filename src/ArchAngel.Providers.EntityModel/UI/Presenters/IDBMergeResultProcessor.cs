using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public interface IDBMergeResultProcessor
	{
		string GetTextResults();
		string GetXmlResults();
		string GetHtmlResults();
	}

	public class DBMergeResultProcessor : IDBMergeResultProcessor
	{
		private readonly DatabaseMergeResult results;

		public DBMergeResultProcessor(DatabaseMergeResult results)
		{
			this.results = results;
		}

		public string GetTextResults()
		{
			StringBuilder sb = new StringBuilder();

			ProcessOperations(sb, "** Table Operations **", results.TableOperations);
			ProcessOperations(sb, "** Column Operations **", results.ColumnOperations);
			ProcessOperations(sb, "** Key Operations **", results.KeyOperations);
			ProcessOperations(sb, "** Index Operations **", results.IndexOperations);
            ProcessOperations(sb, "** Relationship Operations **", results.RelationshipOperations);

			return sb.ToString();
		}

		private void ProcessOperations<T>(StringBuilder sb, string operationHeader, IEnumerable<IMergeOperation<T>> operations) where T : class
		{
			if (operations.Count() > 0)
			{
				sb.AppendLine(operationHeader);
				foreach (var op in operations)
				{
					sb.AppendLine(op.ToString());
				}
			}
		}

		public string GetXmlResults()
		{
			throw new System.NotImplementedException();
		}

		public string GetHtmlResults()
		{
			throw new System.NotImplementedException();
		}
	}
}