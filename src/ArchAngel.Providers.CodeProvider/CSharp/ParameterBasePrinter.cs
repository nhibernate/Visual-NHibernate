using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public abstract class ParameterBasePrinter : BasePrinter<ParameterBase>
	{
		private ParameterBase obj;

		public ParameterBasePrinter(ParameterBase obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public virtual string FullyQualifiedDisplayNameExtended
		{
			get
			{
				string parameterNames = "";

				for (int i = 0; i < obj.Parameters.Count; i++)
				{
					if (i > 0)
					{
						parameterNames += ", ";
					}
					foreach (string modifier in obj.Parameters[i].Modifiers)
					{
						parameterNames += modifier + " ";
					}
					parameterNames += string.Format("{0} {1}", obj.Parameters[i].DataType,
													obj.Parameters[i].Name);
				}
				return string.Format("{0} ({1})", FullyQualifiedName, parameterNames);
			}
		}

		public virtual string FullyQualifiedDisplayName
		{
			get
			{
				return string.Format("{0} ({1})", FullyQualifiedName, obj.GetParameterTypes(false));
			}
		}

		public virtual string DisplayName
		{
			get { return string.Format("{0} ({1})", obj.Name, obj.Parameters.Count); }
		}

		public virtual string DisplayNameExtended
		{
			get
			{
				string parameterNames = "";

				for (int i = 0; i < obj.Parameters.Count; i++)
				{
					if (i > 0)
					{
						parameterNames += ", ";
					}
					parameterNames += string.Format("{0} {1}", obj.Parameters[i].DataType, obj.Parameters[i].Name);
				}
				return string.Format("{0} ({1})", obj.Name, parameterNames);
			}
		}

		public virtual string FullyQualifiedIdentifer
		{
			get
			{
				if (obj.ParentObject != null && obj.ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return string.Format("{0}{1}{2} ({3})", obj.ParentObject.FullyQualifiedIdentifer, BaseConstructConstants.FullyQualifiedIdentifierSeparator, obj.Name, obj.GetParameterTypes(false));

				return string.Format("{0} ({1})", obj.Name, string.Join(", ", obj.GetParameterTypeStrings()));
			}
		}

		public override string Print()
		{
			throw new System.NotImplementedException();
		}
	}
}
