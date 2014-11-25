using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class ISourceClass
	{
		//private ArchAngel.Providers.CodeProvider.DotNet.Class BaseClass;
		private string _FilePath;
		public List<ISourceAttribute> SourceAttributesThatMustExist = new List<ISourceAttribute>();
		public List<ISourceProperty> SourcePropertiesThatMustExist = new List<ISourceProperty>();
		public List<ISourceProperty> SourcePropertiesThatMustNotExist = new List<ISourceProperty>();
		public List<ISourceField> SourceFieldsThatMustExist = new List<ISourceField>();
		public List<ISourceFunction> SourceFunctionsThatMustExist = new List<ISourceFunction>();
		public List<ISourceFunction> SourceFunctionsThatMustNotExist = new List<ISourceFunction>();

		public string FilePath
		{
			get { return _FilePath; }
			set { _FilePath = value; }
		}

		public void EnsureAttributeExists(ISourceAttribute attribute)
		{
			SourceAttributesThatMustExist.Add(attribute);
		}

		public void EnsurePropertyExists(ISourceProperty property)
		{
			SourcePropertiesThatMustExist.Add(property);
		}

		public void EnsurePropertyDoesNotExist(ISourceProperty property)
		{
			SourcePropertiesThatMustNotExist.Add(property);
		}

		public void EnsureFieldExists(ISourceField field)
		{
			SourceFieldsThatMustExist.Add(field);
		}

		public void EnsureFunctionExists(ISourceFunction function)
		{
			SourceFunctionsThatMustExist.Add(function);
		}

		public void EnsureFunctionDoesNotExist(ISourceFunction function)
		{
			SourceFunctionsThatMustNotExist.Add(function);
		}
	}
}
