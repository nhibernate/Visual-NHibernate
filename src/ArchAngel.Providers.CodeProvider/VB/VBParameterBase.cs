using System;
using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	public abstract class ParameterBase : BaseConstruct
	{
		protected ParameterBase(Controller controller)
			: base(controller)
		{
		}

		private readonly List<Parameter> parameters = new List<Parameter>();

		public List<Parameter> Parameters
		{
			get { return parameters; }
		}

		protected bool ParametersAreTheSame(ParameterBase comparisonFunction, ComparisonDepth depth)
		{
			if (Parameters.Count != comparisonFunction.Parameters.Count) return false;

			for (int i = 0; i < Parameters.Count; i++)
			{
				if (depth != ComparisonDepth.Signature && Parameters[i].Name != comparisonFunction.Parameters[i].Name)
					return false;

				if (Parameters[i].DataType != comparisonFunction.Parameters[i].DataType)
				{
					return false;
				}
			}
			return true;
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			// Handle Parameters in here, leave other customisations to CustomMergeStepParameterInternal
			ParameterBase userBC = (ParameterBase)user, newgenBC = (ParameterBase)newgen, prevgenBC = (ParameterBase)prevgen;

			if (userBC.Parameters.Count != prevgenBC.Parameters.Count &&
				newgenBC.Parameters.Count != prevgenBC.Parameters.Count)
			{
				// We can't merge if there are additional parameters in user and newgen.
				return false;
			}
			if (userBC.Parameters.Count != prevgenBC.Parameters.Count)
			{
				// the user has a different number of parameters. Check to see if the
				// newgen has changed, if it has then we can't merge.
				if (prevgenBC.ParametersAreTheSame(newgenBC, ComparisonDepth.Complete) == false)
					return false;

				// User has changed, newgen hasn't.
				CloneParameters(userBC);
				return true;
			}
			if (newgenBC.Parameters.Count != prevgenBC.Parameters.Count)
			{
				// the newgen has a different number of parameters. Check to see if the
				// user has changed, if it has then we can't merge.
				if (prevgenBC.ParametersAreTheSame(userBC, ComparisonDepth.Complete) == false)
					return false;
				// Template has changed, newgen hasn't.
				CloneParameters(newgenBC);
				return true;
			}

			Parameters.Clear();
			// Check each parameter individually
			for (int i = 0; i < prevgenBC.Parameters.Count; i++)
			{
				bool userChange = prevgenBC.Parameters[i].IsTheSame(userBC.Parameters[i], ComparisonDepth.Complete) == false;
				bool templateChange = prevgenBC.Parameters[i].IsTheSame(newgenBC.Parameters[i], ComparisonDepth.Complete) == false;

				if (userBC.Parameters[i].IsTheSame(newgenBC.Parameters[i], ComparisonDepth.Complete))
				{
					userChange = true;
					templateChange = false;
				}

				if (templateChange && userChange)
				{
					return false;
				}
				if (userChange)
				{
					Parameters.Add((Parameter)userBC.Parameters[i].Clone());
				}
				else if (templateChange)
				{
					Parameters.Add((Parameter)newgenBC.Parameters[i].Clone());
				}
				else
				{
					Parameters.Add((Parameter)userBC.Parameters[i].Clone());
				}
			}

			return CustomMergeStepParameterInternal(user, newgen, prevgen);
		}

		protected abstract bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen,
																 BaseConstruct prevgen);

		protected void CloneParameters(ParameterBase iiToCopyFrom)
		{
			foreach (Parameter p in iiToCopyFrom.Parameters)
			{
				Parameters.Add((Parameter)p.Clone());
			}
		}


		protected string[] GetParameterTypeStrings()
		{
			string[] parameterStrings = new string[Parameters.Count];
			for (int i = 0; i < parameterStrings.Length; i++)
				parameterStrings[i] = Parameters[i].DataType;
			return parameterStrings;
		}

		public override string FullyQualifiedDisplayNameExtended
		{
			get
			{
				throw new NotImplementedException("FullyQualifiedDisplayNameExtended not yet implemented");
			}
		}

		public override string FullyQualifiedDisplayName
		{
			get
			{
				throw new NotImplementedException("FullyQualifiedDisplayName not yet implemented");
			}
		}

		public override string FullyQualifiedIdentifer
		{
			get
			{
				throw new NotImplementedException("FullyQualifiedIdentifer not yet implemented");
			}
		}

		public override string DisplayName
		{
			get { throw new NotImplementedException("DisplayName not yet implemented"); }
		}

		public override string DisplayNameExtended
		{
			get
			{
				throw new NotImplementedException("DisplayNameExtended not yet implemented");
			}
		}


		private string GetParameterTypes()
		{
			throw new NotImplementedException("GetParameterTypes not yet implemented");
		}
	}
}
