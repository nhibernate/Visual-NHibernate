using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class FunctionConstraintBuilder
	{
		private readonly Class _class;
		private readonly string _name;
		private readonly IList<string> _oldNames = new List<string>();
		private string _returnTypeName;
		private readonly List<string> _Modifiers = new List<string>();
		private List<ArchAngel.Interfaces.Scripting.NHibernate.Model.ISourceParameter> _ParameterTypes = new List<ArchAngel.Interfaces.Scripting.NHibernate.Model.ISourceParameter>();
		private string _body;
		private bool _mustRemove = false;

		private readonly List<AttributeConstraintBuilder<FunctionConstraintBuilder>> attributes = new List<AttributeConstraintBuilder<FunctionConstraintBuilder>>();

		public FunctionConstraintBuilder(Class @class, string name, IList<string> oldNames)
		{
			_class = @class;
			_name = name;
			_ParameterTypes = new List<ArchAngel.Interfaces.Scripting.NHibernate.Model.ISourceParameter>();

			foreach (string oldName in oldNames)
				_oldNames.Add(oldName);
		}

		public FunctionConstraintBuilder RemoveFunction()
		{
			_mustRemove = true; return this;
		}

		public FunctionConstraintBuilder WithReturnType(string returnTypeName)
		{
			_returnTypeName = returnTypeName; return this;
		}

		public FunctionConstraintBuilder WithParameterTypes(List<ArchAngel.Interfaces.Scripting.NHibernate.Model.ISourceParameter> parameterTypeNames)
		{
			_ParameterTypes = parameterTypeNames; return this;
		}

		public FunctionConstraintBuilder WithModifiers(params string[] modifiers)
		{
			_Modifiers.AddRange(modifiers); return this;
		}

		public FunctionConstraintBuilder WithBody(string body)
		{
			_body = body; return this;
		}

		public AttributeConstraintBuilder<FunctionConstraintBuilder> WithAttribute(string attributeName)
		{
			var attribute = new AttributeConstraintBuilder<FunctionConstraintBuilder>(this, attributeName);
			attributes.Add(attribute);
			return attribute;
		}

		public void ApplyTo(Actions actions)
		{
			var function = Construct(_class);

			// Try find the function in the existing class using the old name
			Function existingFunction = null;// _class.Functions.FirstOrDefault(p => p.Name == _name);

			foreach (var func in _class.Functions.Where(p => p.Name == _name && (_ParameterTypes != null && p.Parameters.Count == _ParameterTypes.Count)))
			{
				bool found = true;

				for (int i = 0; i < func.Parameters.Count; i++)
				{
					if (func.Parameters[i].DataType != _ParameterTypes[i].DataType)
					{
						found = false;
						break;
					}
				}
				if (found)
				{
					existingFunction = func;
					break;
				}
			}
			if (existingFunction == null)
			{
				for (int i = _oldNames.Count - 1; i >= 0; i--)
				{
					// No property found with the new name, so try the old name
					//existingFunction = _class.Functions.FirstOrDefault(p => p.Name == _oldNames[i]);

					//if (existingFunction != null)
					//    break;
					foreach (var func in _class.Functions.Where(p => p.Name == _oldNames[i] && p.Parameters.Count == _ParameterTypes.Count))
					{
						bool found = true;

						for (int pCounter = 0; pCounter < func.Parameters.Count; pCounter++)
						{
							if (func.Parameters[pCounter].DataType != _ParameterTypes[pCounter].DataType)
							{
								found = false;
								break;
							}
						}
						if (found)
						{
							existingFunction = func;
							break;
						}
					}
					if (existingFunction != null)
						break;
				}
			}
			//if (existingFunction == null)
			//{
			//    // As a last ditch effort to find the property, ignore case when searching
			//    existingFunction = _class.Functions.FirstOrDefault(p => p.Name.ToLowerInvariant() == _name.ToLowerInvariant());
			//}
			if (existingFunction != null)
			{
				if (_mustRemove)
					ApplyFunctionDeletion(actions, existingFunction);
				else
				{
					// Make sure the property matches the given property
					ApplyFunctionChanges(actions, existingFunction, function);
				}
			}
			else if (_mustRemove)
			{
				// No existing property found, so we don't need to do anything, it's already removed.
				// Don't add any actions
				return;
			}
			else
			{
				// Add the property to the class.
				actions.AddAction(new AddFunctionToClassAction(function, AdditionPoint.EndOfParent, _class));
			}
			if (!_mustRemove)
			{
				var functionToAddTo = existingFunction ?? function;

				//foreach (var attrBuilder in attributes)
				//{
				//    var attribute = attrBuilder.Build(functionToAddTo.Controller);

				//    if (functionToAddTo.Attributes.Any(a => a.Name == attribute.Name) ||
				//        functionToAddTo.AttributeSections.SelectMany(section => section.Attributes).Any(a => a.Name == attribute.Name))
				//    {
				//        // if there are any attributes on the property currently with this name, then we should skip it.
				//        continue;
				//    }
				//    actions.AddAction(new AddAttributeToMethodAction(functionToAddTo, attribute));
				//}
			}
		}

		private static void ApplyFunctionChanges(Actions actions, Function existingFunction, Function exampleFunction)
		{
			ApplyHeaderChanges(existingFunction, exampleFunction, actions);
			ApplyBodyChanges(existingFunction, exampleFunction, actions);
		}

		private static void ApplyFunctionDeletion(Actions actions, Function existingFunction)
		{
			actions.AddAction(new RemoveMethodFromClassAction(existingFunction));
		}

		/// <summary>
		/// Changes the entire header, including modifiers, return-type, name and parameters.
		/// </summary>
		/// <param name="existingFunction"></param>
		/// <param name="exampleFunction"></param>
		/// <param name="actions"></param>
		private static void ApplyHeaderChanges(Function existingFunction, Function exampleFunction, Actions actions)
		{
			if (existingFunction.Name != exampleFunction.Name)
			{
				// Change datatype
				actions.AddAction(new ChangeMethodHeaderAction(existingFunction, exampleFunction));
			}
		}

		private static void ApplyBodyChanges(Function existingFunction, Function exampleFunction, Actions actions)
		{
			actions.AddAction(new ChangeMethodBodyAction(existingFunction, exampleFunction));
		}

		private Function Construct(Class @class)
		{
			Function func = new Function(@class.Controller);
			func.Name = _name;
			func.Modifiers.AddRange(_Modifiers);
			func.ReturnType = new DataType(@class.Controller, _returnTypeName);
			func.BodyText = _body;

			foreach (var p in _ParameterTypes)
				func.Parameters.Add(new Parameter(@class.Controller, func, p.Name, p.DataType, BaseConstruct.CodeLanguage.CSharp));

			return func;
		}
	}
}
