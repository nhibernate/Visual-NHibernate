using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Common;
using log4net;
using Slyce.Common;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Workbench.IntelliMerge
{
	///<summary>
	/// Contains helper methods for analysing a set of files.
	///</summary>
	public class AnalysisHelper
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AnalysisHelper));

		private IController controller;

		private int numConflicts = 0;
		private int numExactCopy = 0;
		private int numResolved = 0;
		private int numAnalysisErrors = 0;
		private int numGenerationErrors = 0;
		private int numNewFiles = 0;

		/// <summary>
		/// Analyses the files in the given ProjectFileTree, reporting progress through the given ProgressHelper.
		/// </summary>
		/// <param name="progressHelper">The progress helper to use to report progress.</param>
		/// <param name="cont">The Controller to use.</param>
		/// <param name="tree">The project information to analyse.</param>
		public void StartAnalysis(IAnalysisProgressHelper progressHelper, IController cont, ProjectFileTree tree)
		{
			controller = cont;
			//output.BusyAnalysing = true;
			//output.ResetCounts();

			numConflicts = 0;
			numExactCopy = 0;
			numResolved = 0;
			numNewFiles = 0;

			log.Debug("Analysis started");

			if (progressHelper.IsCancellationPending())
			{
				progressHelper.Cancel();
				log.Debug("Analysis cancelled before it did anything.");
				return;
			}

			List<ProjectFileTreeNode> nodes = GetListOfNodesToProcess(tree.AllNodes, true);

			progressHelper.AddItemRangeToQueue(nodes);

			progressHelper.ReportProgress(0, progressHelper);
			progressHelper.ReportProgress(10, new AnalyseFilesProgress(nodes.Count, 0, 0, 0, 0, 0));

			log.Debug("Got all items to analyse, starting to process them.");

			while (progressHelper.HasItemsLeftToProcess())
			{
				if (progressHelper.IsCancellationPending())
				{
					progressHelper.Cancel();
					return;
				}

				ProjectFileTreeNode node = progressHelper.Dequeue();
				if (node == null)
					break;
				AnalyseFile(progressHelper, node);
				progressHelper.ReportProgress(10, new AnalyseFilesProgress(progressHelper.Count, numExactCopy, numResolved, numConflicts, numAnalysisErrors + numGenerationErrors, numNewFiles, node.Path, node.Status));
				Application.DoEvents();
			}
		}

		internal List<ProjectFileTreeNode> GetListOfNodesToProcess(IEnumerable<ProjectFileTreeNode> childNodes, bool onlyUnanalysedNodes)
		{
			List<ProjectFileTreeNode> nodes = new List<ProjectFileTreeNode>();

			foreach (ProjectFileTreeNode childNode in childNodes)
			{
				if (childNode.Status == ProjectFileStatusEnum.GenerationError)
				{
					numGenerationErrors++;
					continue;
				}

				if (childNode.Status != ProjectFileStatusEnum.Folder && childNode.AssociatedFile != null)
				{
					if (onlyUnanalysedNodes)
					{
						if (childNode.Status == ProjectFileStatusEnum.UnAnalysedFile || childNode.Status == ProjectFileStatusEnum.Busy)
							nodes.Add(childNode);
					}
					else
					{
						nodes.Add(childNode);
					}
				}
			}

			return nodes;
		}

		internal void AnalyseFile(IAnalysisProgressHelper progressHelper, ProjectFileTreeNode fileNode)
		{
			if (fileNode == null || fileNode.AssociatedFile == null)
				return;

			fileNode.Status = ProjectFileStatusEnum.Busy;
			progressHelper.ReportProgress(10, new AnalyseFilesProgress(progressHelper.Count, numExactCopy, numResolved, numConflicts, numAnalysisErrors + numGenerationErrors, numNewFiles, fileNode.Path, fileNode.Status));

			try
			{
				fileNode.AssociatedFile.LoadCustomMatches(controller.GetTempFilePathForComponent(ComponentKey.SlyceMerge_PrevGen));
				bool result = fileNode.AssociatedFile.PerformDiff();

				if (result == false)
				{
					fileNode.Status = ProjectFileStatusEnum.AnalysisError;
					numAnalysisErrors++;
					return;
				}
			}
			catch (DiffException e)
			{
				if (e.InnerException == null || e.InnerException is MergeException == false) throw;

				MergeException mergeEx = (MergeException)e.InnerException;
				// There was an error during the Merge. Display this to the user.
				fileNode.Status = ProjectFileStatusEnum.MergeError;
				fileNode.MergeError = new MergeError(mergeEx.BaseConstructName, mergeEx.BaseConstructType, mergeEx.Message);
				numAnalysisErrors++;
				return;
			}

			fileNode.Status = ProjectFileStatusEnum.AnalysedFile;

			switch (fileNode.AssociatedFile.CurrentDiffResult.DiffType)
			{
				case TypeOfDiff.Conflict:
					numConflicts++;
					break;
				case TypeOfDiff.ExactCopy:
					numExactCopy++;
					break;
				case TypeOfDiff.UserAndTemplateChange:
				case TypeOfDiff.UserChangeOnly:
				case TypeOfDiff.TemplateChangeOnly:
					numResolved++;
					break;
				case TypeOfDiff.Warning:
					// TODO: handle warnings
					break;
				case TypeOfDiff.NewFile:
					numNewFiles++;
					break;
				default:
					throw new NotImplementedException("Not coded yet: " + fileNode.AssociatedFile.CurrentDiffResult.DiffType);
			}
		}
	}
}
