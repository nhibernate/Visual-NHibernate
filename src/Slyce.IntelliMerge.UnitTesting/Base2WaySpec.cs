using System;
using System.Collections.Generic;
using System.Text;

namespace Slyce.IntelliMerge.UnitTesting
{
	public class Base2WaySpec
	{
		// outputs
		protected SlyceMerge.LineSpan[] leftConflictLines = null;
		protected SlyceMerge.LineSpan[] rightConflictLines = null;
		protected string mergedText = string.Empty;

		// constants
		protected const string resourcePrefix = "Slyce.IntelliMerge.UnitTesting.Resources.";
		protected const bool identifyConflictsOnly = true;
		protected const bool identifyConflictsAndRegions = false;
	}
}
