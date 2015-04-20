using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public static class EntityExtensions
	{
		public static PropertyConstraintBuilder EnsureHasProperty(this Class entity, string propertyName, IList<string> propertyOldNames)
		{
			return new PropertyConstraintBuilder(entity, propertyName, propertyOldNames);
		}

		public static FieldConstraintBuilder EnsureHasField(this Class entity, string fieldName, IList<string> fieldOldNames)
		{
			return new FieldConstraintBuilder(entity, fieldName, fieldOldNames);
		}

		public static FunctionConstraintBuilder EnsureHasFunction(this Class entity, string functionName, IList<string> functionOldNames)
		{
			return new FunctionConstraintBuilder(entity, functionName, functionOldNames);
		}

		public static ClassConstraintBuilder EnsureHas(this Class @class)
		{
			return new ClassConstraintBuilder(@class);
		}

		public static PropertyConstraintBuilder EnsureDoesNotHaveProperty(this Class entity, string propertyName, IList<string> propertyOldNames)
		{
			PropertyConstraintBuilder cb = new PropertyConstraintBuilder(entity, propertyName, propertyOldNames);
			cb.RemoveProperty();
			return cb;
		}

		public static FunctionConstraintBuilder EnsureDoesNotHaveFunction(this Class entity, string functionName, string returnType, List<ArchAngel.Interfaces.Scripting.NHibernate.Model.ISourceParameter> parameterTypeNames)
		{
			FunctionConstraintBuilder cb = new FunctionConstraintBuilder(entity, functionName, new List<string>())
				.WithReturnType(returnType)
				.WithParameterTypes(parameterTypeNames)
				.RemoveFunction();
			return cb;
		}

		public static UsingStatementConstraintBuilder EnsureHasUsingNamespace(this Class entity, string namespaceName)
		{
			return new UsingStatementConstraintBuilder(entity, namespaceName);
		}

		public static UsingStatementConstraintBuilder EnsureDoesNotHaveUsingNamespace(this Class entity, string namespaceName)
		{
			UsingStatementConstraintBuilder cb = new UsingStatementConstraintBuilder(entity, namespaceName);
			cb.RemoveUsingStatement();
			return cb;
		}

		public static ImplementsConstraintBuilder EnsureHasImplement(this Class entity, string implementName)
		{
			return new ImplementsConstraintBuilder(entity, implementName);
		}

		public static ImplementsConstraintBuilder EnsureDoesNotHaveImplement(this Class entity, string implementName)
		{
			ImplementsConstraintBuilder cb = new ImplementsConstraintBuilder(entity, implementName);
			cb.RemoveCustomImplements();
			return cb;
		}

		public static AttributeConstraintBuilder<ClassConstraintBuilder> EnsureDoesNotHaveAttribute(this ClassConstraintBuilder builder, string attributeName)
		{
			AttributeConstraintBuilder<ClassConstraintBuilder> cb = new AttributeConstraintBuilder<ClassConstraintBuilder>(builder, attributeName);
			cb.RemoveAttribute();
			return cb;
		}
	}
}