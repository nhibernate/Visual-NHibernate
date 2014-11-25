using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Algorithm.Diff;

namespace Slyce.IntelliMerge.UI.VisualDiff
{
	public class TwoWayDiff
	{
		public DiffResult PerformDiff(string leftText, string rightText)
		{
			List<string> leftInput =
				new List<string>(Common.Utility.StandardizeLineBreaks(leftText, Common.Utility.LineBreaks.Unix).Split('\n'));
			List<string> rightInput =
				new List<string>(Common.Utility.StandardizeLineBreaks(rightText, Common.Utility.LineBreaks.Unix).Split('\n'));

			// We need to add a blank line to the bottom of each file, or any lines that are added
			// at the bottom of the new files will be discarded by the diff/merge. I have spent far
			// too long trying to work out why, this work around works so I'm going with it. If someone
			// fixes the underlying problem, this should still work fine. We remove the last line of the
			// merged lines to compensate for this.
			AddHack(leftInput, rightInput);

			// Do the initial 2 way diff
			Diff diff = new Diff(leftInput.ToArray(), rightInput.ToArray(), false, true);

			List<string> leftResult = new List<string>();
			List<string> rightResult = new List<string>();
			List<string> mergedResult = new List<string>();

			List<Block> leftBlocks = new List<Block>();
			List<Block> rightBlocks = new List<Block>();
			List<Block> mergedBlocks = new List<Block>();
			List<int> conflictBlocks = new List<int>();

			foreach (Diff.Hunk h in diff)
			{
				if (h.Same)
				{
					// Create a new block.
					leftBlocks.Add(new Block(leftResult.Count, h.Original().Count));
					rightBlocks.Add(new Block(rightResult.Count, h.Original().Count));
					mergedBlocks.Add(new Block(mergedResult.Count, h.Original().Count));

					// Add each of the lines in the hunk to all of the result lists
					foreach (string line in h.Original())
					{
						leftResult.Add(line);
						rightResult.Add(line);
						mergedResult.Add(line);
					}
					continue;
				}

				// Create a new block
				leftBlocks.Add(new Block(leftResult.Count, h.Left.Count));
				rightBlocks.Add(new Block(rightResult.Count, h.Right.Count));
				mergedBlocks.Add(new Block(mergedResult.Count, 0));

				// Add a new conflict block indicator
				conflictBlocks.Add(leftBlocks.Count-1);

				// Add the lines from the left side of the hunk to the left result
				foreach(string line in h.Left)
				{
					leftResult.Add(line);
				}
				// Add the lines from the right side of the hunk to the right result
				foreach (string line in h.Right)
				{
					rightResult.Add(line);
				}
			}

			// Remove our hack lines
			RemoveHack(leftBlocks, leftResult, rightBlocks, rightResult, mergedBlocks);

			DiffResult result = new DiffResult();

			result.diffResults.Left = new SingleDiffResult(result, leftResult, leftBlocks, "Left");
			result.diffResults.Right = new SingleDiffResult(result, rightResult, rightBlocks, "Right");
			result.diffResults.Merged = new SingleDiffResult(result, mergedResult, mergedBlocks, "Merged");
			foreach(var cb in conflictBlocks)
				result.conflictBlocks.Add(cb);
			
			return result;
		}
		/// <summary>
		/// We need to add a blank line to the bottom of each file, or any lines that are added
		/// at the bottom of the new files will be discarded by the diff/merge. I have spent far
		/// too long trying to work out why, this work around works so I'm going with it. If someone
		/// fixes the underlying problem, this should still work fine. We remove the last line of the
		/// merged lines to compensate for this.
		/// </summary>
		private static void AddHack(ICollection<string> leftInput, ICollection<string> rightInput)
		{
			leftInput.Add("");
			rightInput.Add("");
		}

		private static void RemoveHack(IList<Block> leftBlocks, IList<string> leftResult, IList<Block> rightBlocks, IList<string> rightResult, IList<Block> mergedBlocks)
		{
			leftResult.RemoveAt(leftResult.Count - 1);
			rightResult.RemoveAt(rightResult.Count - 1);

			int lastBlockIndex = leftBlocks.Count - 1;

			leftBlocks[lastBlockIndex].Length--;
			rightBlocks[lastBlockIndex].Length--;
			mergedBlocks[lastBlockIndex].Length--;

			// If the last block is now empty, remove it.
			if (leftBlocks[lastBlockIndex].Length <= 0)
			{
				leftBlocks.RemoveAt(lastBlockIndex);
				rightBlocks.RemoveAt(lastBlockIndex);
				mergedBlocks.RemoveAt(lastBlockIndex);
			}
		}
	}


	public class DiffResult
	{
		internal readonly ObjectList<SingleDiffResult> diffResults = new ObjectList<SingleDiffResult>();
		internal readonly HashSet<int> conflictBlocks = new HashSet<int>();
		internal readonly HashSet<int> conflictBlocksHandled = new HashSet<int>();

		public ReadOnlyCollection<ObjectList<Block>> ConflictBlocks
		{
			get
			{
				List<ObjectList<Block>> list = new List<ObjectList<Block>>();
				foreach(int index in conflictBlocks)
				{
					list.Add(GetBlock(index));
				}

				return list.AsReadOnly();
			}
		}

		public bool MergedSuccessfully
		{
			get { return conflictBlocksHandled.Count - conflictBlocks.Count == 0; }
		}

		public ReadOnlyCollection<ObjectList<Block>> GetBlocks()
		{
			List<ObjectList<Block>> list = new List<ObjectList<Block>>();
			for (int i = 0; i < BlockCount; i++)
			{
				list.Add(GetBlock(i));
			}

			return list.AsReadOnly();
		}

		private ObjectList<Block> GetBlock(int i)
		{
			ObjectList<Block> item = new ObjectList<Block>();
			item.Left = diffResults.Left.Blocks[i];
			item.Right = diffResults.Right.Blocks[i];
			item.Merged = diffResults.Merged.Blocks[i];
			return item;
		}

		public int BlockCount
		{
			get { return diffResults.Left.Blocks.Count; }
		}

		public SingleDiffResult Merged
		{
			get { return diffResults.Merged; }
		}

		public void SelectVersionOfConflict(int blockNumber, ObjectVersion left)
		{
			if(left == ObjectVersion.Merged)
				throw  new ArgumentException("Cannot select the merged version, as it doesn't make sense to set it to itself. This indicates you are doing something wrong.");
			
			ObjectList<Block> conflictBlock = GetBlock(blockNumber);

			diffResults.Merged.SetText(blockNumber, conflictBlock[left].Text);
			conflictBlock.Merged.BlockTypeText = diffResults[left].DisplayText;

			if(conflictBlocks.Contains(blockNumber))
				conflictBlocksHandled.Add(blockNumber);
		}

		public void ChangeLine(int lineNumberInMerged, string newLineText)
		{
			diffResults.Merged.SetLineText(lineNumberInMerged, newLineText);
			int blockNumber = diffResults.Merged.GetBlockNumber(lineNumberInMerged);

			DiffBlock(blockNumber);
		}

		public void InsertLine(int block, int lineIndex, string text)
		{
			diffResults.Merged.InsertLine(block, lineIndex, text);
			DiffBlock(block);
		}

		private void DiffBlock(int blockNumber)
		{
			Block left = diffResults.Left.Blocks[blockNumber];
			Block right = diffResults.Right.Blocks[blockNumber];
			Block merged = diffResults.Merged.Blocks[blockNumber];

			if (merged.Length != right.Length && merged.Length != right.Length)
			{
				conflictBlocksHandled.Remove(blockNumber);
				conflictBlocks.Add(blockNumber);
				return;
			}

			string[] leftText = left.Text;
			string[] rightText = right.Text;
			string[] mergedText = merged.Text;

			for(int i = 0; i < merged.Length; i++)
			{
				if (leftText[i] != mergedText[i] && rightText[i] != mergedText[i])
				{
					conflictBlocksHandled.Remove(blockNumber);
					conflictBlocks.Add(blockNumber);
					return;
				}
			}

			// The blocks are equal, so remove them from the conflict blocks list.
			conflictBlocksHandled.Add(blockNumber);
			conflictBlocks.Add(blockNumber);
		}

		public void RemoveLine(int blockNumber, int lineNumber)
		{
			diffResults.Merged.RemoveLine(blockNumber, lineNumber);
			DiffBlock(blockNumber);
		}
	}

	public class SingleDiffResult
	{
		private readonly List<string> lines = new List<string>();
		private readonly List<Block> blocks = new List<Block>();
		private readonly string displayText;
		private DiffResult parentDiffResult;

		public SingleDiffResult(DiffResult parentDiffResult, IEnumerable<string> lines, IEnumerable<Block> blocks, string displayText)
		{
			this.parentDiffResult = parentDiffResult;
			this.displayText = displayText;
			this.lines.AddRange(lines);
			this.blocks.AddRange(blocks);

			foreach(Block b in blocks)
			{
				b.ParentResult = this;
			}
		}

		public string DisplayText
		{
			[DebuggerStepThrough]
			get { return displayText; }
		}

		public ReadOnlyCollection<string> Lines
		{
			get { return lines.AsReadOnly(); }
		}

		public ReadOnlyCollection<Block> Blocks
		{
			get { return blocks.AsReadOnly(); }
		}

		public void AddLine(string line)
		{
			lines.Add(line);
		}

		public void SetLineText(int lineNumber, string text)
		{
			lines[lineNumber] = text;
		}

		public void SetText(int blockNumber, string[] text)
		{
			Block blockToModify = blocks[blockNumber];

			int oldLength = blockToModify.Length;
			int newLength = text.Length;

			// Remove any existing text from the block
			lines.RemoveRange(blockToModify.StartOffset, blockToModify.Length);
		
			// Add the new text
			lines.InsertRange(blockToModify.StartOffset, text);
			
			// Set the new length on the conflict block
			blockToModify.Length = newLength;

			// Fix the offsets in the subsequent blocks
			int offsetDelta = newLength - oldLength;

			for (int i = blockNumber + 1; i < blocks.Count; i++)
			{
				blocks[i].StartOffset += offsetDelta;
			}
		}

		/// <summary>
		/// Gets the number of the block that the given line is in.
		/// </summary>
		/// <param name="lineNumber">The line to search for.</param>
		/// <returns>The index of the block that contains that line.</returns>
		public int GetBlockNumber(int lineNumber)
		{
			if(lineNumber < 0 || lineNumber >= lines.Count)
				throw new ArgumentOutOfRangeException("lineNumber");

			for (int i = 0; i < blocks.Count; i++)
			{
				var block = blocks[i];
				if (block.StartOffset <= lineNumber && block.StartOffset + block.Length > lineNumber)
					return i;
			}

			return -1;
		}

		public void InsertLine(int blockIndex, int lineIndex, string text)
		{
			Block block = blocks[blockIndex];
			List<string> newText = new List<string>(block.Text);
			newText.Insert(lineIndex, text);
			SetText(blockIndex, newText.ToArray());
		}

		public void RemoveLine(int blockNumber, int lineNumber)
		{
			Block block = blocks[blockNumber];
			List<string> newText = new List<string>(block.Text);
			newText.RemoveAt(lineNumber);
			SetText(blockNumber, newText.ToArray());
		}
	}

	public class Block
	{
		private SingleDiffResult parentResult;
		private int startOffset;
		private int length;
		private string customBlockTypeText = "";

		public Block(int startOffset, int length)
		{
			this.length = length;
			this.startOffset = startOffset;
		}

		public string BlockTypeText
		{
			get
			{
				return string.IsNullOrEmpty(customBlockTypeText) ? parentResult.DisplayText : customBlockTypeText;
			}
			set
			{
				customBlockTypeText = value;	
			}
		}

		internal SingleDiffResult ParentResult
		{
			[DebuggerStepThrough]
			get { return parentResult; }
			[DebuggerStepThrough]
			set { parentResult = value; }
		}

		public string[] Text
		{
			get
			{
				string[] text = new string[length];
				ReadOnlyCollection<string> lines = ParentResult.Lines;

				for (int i = 0; i < length; i++)
					text[i] = lines[startOffset + i];

				return text;
			}
		}

		internal int StartOffset
		{
			[DebuggerStepThrough]
			get { return startOffset; }
			[DebuggerStepThrough]
			set { startOffset = value; }
		}

		internal int Length
		{
			[DebuggerStepThrough]
			get { return length; }
			[DebuggerStepThrough]
			set { length = value; }
		}
	}

	/// <summary>
	/// Do not change these numbers!
	/// </summary>
	public enum ObjectVersion
	{
		Base = 0, Left = 1, Right = 2, Merged = 3
	}

	public class ObjectList<T> where T : class
	{
		private readonly T[] objects = new T[4];

		public T Base
		{
			[DebuggerStepThrough]
			get { return objects[(int)ObjectVersion.Base]; }
			[DebuggerStepThrough]
			set { objects[(int)ObjectVersion.Base] = value; }
		}

		public T Left
		{
			[DebuggerStepThrough]
			get { return objects[(int)ObjectVersion.Left]; }
			[DebuggerStepThrough]
			set { objects[(int)ObjectVersion.Left] = value; }
		}

		public T Right
		{
			[DebuggerStepThrough]
			get { return objects[(int)ObjectVersion.Right]; }
			[DebuggerStepThrough]
			set { objects[(int)ObjectVersion.Right] = value; }
		}

		public T Merged
		{
			[DebuggerStepThrough]
			get { return objects[(int)ObjectVersion.Merged]; }
			[DebuggerStepThrough]
			set { objects[(int)ObjectVersion.Merged] = value; }
		}

		public T this[ObjectVersion version]
		{
			get
			{
				return objects[(int)version];
			}
			set
			{
				objects[(int) version] = value;
			}
		}
	}
}
