using System.Collections.Generic;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public abstract class ParameterBase : BaseConstruct
	{
		private readonly List<Parameter> parameters = new List<Parameter>();

		protected ParameterBase(Controller controller)
			: base(controller)
		{
		}

		public List<Parameter> Parameters
		{
			get { return parameters; }
		}

		public override string FullyQualifiedDisplayName
		{
			get
			{
				return string.Format("{0} ({1})", FullyQualifiedName, GetParameterTypes(false));
			}
		}

		public string FullyQualifiedDisplayNameWithBCLTypes
		{
			get
			{
				return string.Format("{0} ({1})", FullyQualifiedName, GetParameterTypes(true));
			}
		}

		protected internal bool ParametersAreTheSame(ParameterBase comparisonFunction, ComparisonDepth depth)
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
				Parameter clone;
				if (templateChange && userChange)
				{
					return false;
				}
				if (userChange)
				{
					clone = (Parameter)userBC.Parameters[i].Clone();
				}
				else if (templateChange)
				{
					clone = (Parameter)newgenBC.Parameters[i].Clone();
				}
				else
				{
					clone = (Parameter)userBC.Parameters[i].Clone();
				}
				clone.ParentObject = this;
				Parameters.Add(clone);
			}

			return CustomMergeStepParameterInternal(user, newgen, prevgen);
		}

		protected abstract bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen,
																 BaseConstruct prevgen);

		protected void CloneParameters(ParameterBase iiToCopyFrom)
		{
			foreach (Parameter p in iiToCopyFrom.Parameters)
			{
				var clone = (Parameter)p.Clone();
				clone.ParentObject = this;
				Parameters.Add(clone);
			}
		}


		public string[] GetParameterTypeStrings()
		{
			string[] parameterStrings = new string[Parameters.Count];
			for (int i = 0; i < parameterStrings.Length; i++)
				parameterStrings[i] = Parameters[i].DataType;
			return parameterStrings;
		}

		public override string FullyQualifiedIdentifer
		{
			get
			{
				if (ParentObject != null && ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return string.Format("{0}{1}{2} ({3})", ParentObject.FullyQualifiedIdentifer, BaseConstructConstants.FullyQualifiedIdentifierSeparator, Name, GetParameterTypes(false));

				return string.Format("{0} ({1})", Name, string.Join(", ", GetParameterTypeStrings()));
			}
		}

		/// <summary>
		/// Gets a comma-separated list of parameter types
		/// </summary>
		/// <param name="useBclTypes">Forces type names to proper BCL type if set to true (eg: bool -> Boolean), uses type as defined in code if false.</param>
		/// <returns></returns>
		internal string GetParameterTypes(bool useBclTypes)
		{
			string parameterNames = "";

			for (int i = 0; i < Parameters.Count; i++)
			{
				if (i > 0)
				{
					parameterNames += ", ";
				}
				foreach (string modifier in Parameters[i].Modifiers)
				{
					parameterNames += modifier + " ";
				}
				parameterNames += useBclTypes ? GetBCLTypeName(Parameters[i].DataType) : Parameters[i].DataType;
			}
			return parameterNames;
		}

		/// <summary>
		/// Gets the BCL type name for a C# type.
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		private string GetBCLTypeName(string typeName)
		{
			switch (typeName)
			{
				case "string":
					return "String";
				case "bool":
					return "Boolean";
				case "int":
					return "Int32";
				case "double":
					return "Double";
				default:
					return typeName;
			}
		}

	}
}
