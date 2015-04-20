using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class EntityCodeInsertions
	{
		public string Process(ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity ientity, Dictionary<string, object> TemplateCache, out string CurrentFileName)
		{
			Entity entity = (Entity)ientity.ScriptObject;
			Actions actions = new Actions();

			foreach (var att in ientity.MappedClass.SourceAttributesThatMustExist)
			{
				entity.MappedClass.EnsureHas()
						.AttributeNamed(att.Name)
						.Finish()
					.ApplyTo(actions);
			}
			foreach (var prop in ientity.MappedClass.SourcePropertiesThatMustExist)
			{
				string[] modifiers = prop.Modifiers == null ? new string[0] : prop.Modifiers.ToArray();

				PropertyConstraintBuilder propertyConstraint = null;
				propertyConstraint =
					entity.MappedClass.EnsureHasProperty(prop.Name, prop.PreviousNames ?? new List<string>())
						.WithType(prop.Type)
						.WithModifiers(modifiers)
						.WithGetAccessorBody(prop.GetAccessor.Body)
						.WithSetAccessorBody(prop.SetAccessor.Body);

				if (!string.IsNullOrEmpty(prop.SetAccessor.Modifier))
					propertyConstraint.WithSetAccessorAccessibility(prop.SetAccessor.Modifier);

				propertyConstraint.ApplyTo(actions);
			}
			foreach (var prop in ientity.MappedClass.SourcePropertiesThatMustNotExist)
			{
				PropertyConstraintBuilder propertyConstraint = null;
				propertyConstraint.ApplyTo(actions);
			}
			foreach (var field in ientity.MappedClass.SourceFieldsThatMustExist)
			{
				string[] modifiers = field.Modifiers == null ? new string[0] : field.Modifiers.ToArray();

				FieldConstraintBuilder fieldConstraint = null;
				fieldConstraint =
					entity.MappedClass.EnsureHasField(field.Name, field.PreviousNames ?? new List<string>())
						.WithType(field.Type)
						.WithModifiers(modifiers)
						.WithInitialValue(field.InitialValue);

				fieldConstraint.ApplyTo(actions);
			}
			foreach (var function in ientity.MappedClass.SourceFunctionsThatMustExist)
			{
				string[] modifiers = function.Modifiers == null ? new string[0] : function.Modifiers.ToArray();

				FunctionConstraintBuilder functionConstraint = null;
				functionConstraint =
					entity.MappedClass.EnsureHasFunction(function.Name, function.PreviousNames ?? new List<string>())
					.WithBody(function.Body)
					.WithModifiers(modifiers)
					.WithParameterTypes(function.Parameters)
					.WithReturnType(function.ReturnType);

				functionConstraint.ApplyTo(actions);
			}
			foreach (var function in ientity.MappedClass.SourceFunctionsThatMustNotExist)
			{
				FunctionConstraintBuilder functionConstraint = null;
				functionConstraint.ApplyTo(actions);
			}
			// Load the existing C# file up
			var filename = entity.EntitySet.MappingSet.CodeParseResults.GetFilenameForParsedClass(entity.MappedClass);

			CurrentFileName = filename;

			string outputText;
			if (TemplateCache.ContainsKey(filename) ||
				!System.IO.File.Exists(filename))
				outputText = actions.RunActions((string)TemplateCache[filename], entity.MappedClass.Controller.Root, false);
			else
				outputText = actions.RunActions(System.IO.File.ReadAllText(filename), entity.MappedClass.Controller.Root, true);

			TemplateCache[filename] = outputText;

			return outputText;
		}

		public string Process(Entity entity, Dictionary<string, object> TemplateCache, bool globalUsePrivateSetters, string globalProjectNamespace, out string CurrentFileName)
		{
			Actions actions = new Actions();

			if (entity.HasUserOption("Entity_MarkAsSerializable") && entity.GetUserOptionValue<bool>("Entity_MarkAsSerializable"))
			{
				entity.MappedClass.EnsureHas()
						.AttributeNamed("Serializable")
						.Finish()
					.ApplyTo(actions);
			}
			List<string> propertyNamesFromReferences = entity.DirectedReferences.Where(r => r.FromEndEnabled).Select(d => d.FromName).ToList();
			List<string> propertyNamesFromComponents = entity.Components.Select(c => c.Name).ToList();

			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			foreach (var property in entity.Properties)
				sb.AppendLine(PropertyTemplate.ProcessPropertyTemplate(entity, property));

			List<ArchAngel.Providers.CodeProvider.DotNet.Property> codeProps = ProcessTemplateProperties(sb.ToString());
			Dictionary<ArchAngel.Providers.EntityModel.Model.EntityLayer.Property, ArchAngel.Providers.CodeProvider.DotNet.Property> propMap = new Dictionary<Property, ArchAngel.Providers.CodeProvider.DotNet.Property>();

			for (int i = 0; i < entity.Properties.Count(); i++)
				propMap.Add(entity.Properties.ElementAt(i), codeProps[i]);

			foreach (var property in entity.Properties)
			{
				//sb.AppendLine(PropertyTemplate.ProcessPropertyTemplate(entity, property));
				PropertyConstraintBuilder propertyConstraint = null;

				if (entity.ForeignKeyPropertiesToExclude.Contains(property))
				{
					if (propertyNamesFromReferences.Contains(property.Name) ||
						propertyNamesFromComponents.Contains(property.Name))
					{
						// Don't mark this property for removal, because it's actually going to be added by a reference or component further down.
						continue;
					}
					propertyConstraint = entity.MappedClass.EnsureDoesNotHaveProperty(propMap[property].Name, property.OldNames);
				}
				else
				{
					// Add property's current name to its old name list
					if (property.OldNames.Count == 0 || !property.OldNames.Contains(propMap[property].Name))
						property.OldNames.Add(propMap[property].Name);

					propertyConstraint =
						entity.MappedClass.EnsureHasProperty(propMap[property].Name, property.OldNames)
							.WithType(propMap[property].DataType.Name.Replace(string.Format("{0}.Model.", globalProjectNamespace), ""))
							.WithModifiers(propMap[property].Modifiers.ToArray())
							.WithGetAccessorBody(propMap[property].GetAccessor.BodyText)
							.WithSetAccessorBody(propMap[property].SetAccessor.BodyText);


					//bool propertyUsePrivateSetter = property.HasUserOption("Property_UsePrivateSetter") && property.GetUserOptionValue<bool>("Property_UsePrivateSetter");

					//if (propertyUsePrivateSetter || globalUsePrivateSetters)
					if (propMap[property].SetAccessor.Modifier == "private")
						propertyConstraint.WithSetAccessorAccessibility("private");

					// Code insertions stuff for NHibernate Validator Attributes. Replaced with XML specs.
					//if(property.MaximumLength.HasValue && property.MaximumLength > 0 && property.MaximumLength < int.MaxValue)
					//{
					//    propertyConstraint.WithAttribute("Length")
					//        .WithParameter(property.MaximumLength.Value.ToString());
					//}
				}
				propertyConstraint.ApplyTo(actions);
			}

			#region DirectedReferences

			sb = new System.Text.StringBuilder();

			foreach (var reference in entity.DirectedReferences)
				sb.AppendLine(PropertyTemplate.ProcessReferenceTemplate(entity, reference));

			List<ArchAngel.Providers.CodeProvider.DotNet.Property> codeRefProps = ProcessTemplateProperties(sb.ToString());
			Dictionary<ArchAngel.Providers.EntityModel.Model.EntityLayer.DirectedReference, ArchAngel.Providers.CodeProvider.DotNet.Property> refPropMap = new Dictionary<DirectedReference, ArchAngel.Providers.CodeProvider.DotNet.Property>();

			for (int i = 0; i < entity.DirectedReferences.Count(); i++)
				refPropMap.Add(entity.DirectedReferences.ElementAt(i), codeRefProps[i]);

			foreach (var reference in entity.DirectedReferences.Where(r => r.FromEndEnabled).OrderBy(r => r.FromName))
			{
				// Add reference's current name to its old name list
				if (reference.OldFromNames.Count == 0 || !reference.OldFromNames.Contains(refPropMap[reference].Name))
					reference.OldFromNames.Add(refPropMap[reference].Name);

				var propertyConstraint = entity.MappedClass.EnsureHasProperty(refPropMap[reference].Name, reference.OldFromNames)
											.WithType(refPropMap[reference].DataType.Name.Replace(string.Format("{0}.Model.", globalProjectNamespace), ""))
											.WithModifiers(refPropMap[reference].Modifiers.ToArray())
											.WithGetAccessorBody(refPropMap[reference].GetAccessor.BodyText)
											.WithSetAccessorBody(refPropMap[reference].SetAccessor.BodyText);

				if (refPropMap[reference].SetAccessor.Modifier == "private")
					propertyConstraint.WithSetAccessorAccessibility("private");

				propertyConstraint.ApplyTo(actions);
			}
			#endregion

			#region Component Definitions

			sb = new System.Text.StringBuilder();

			foreach (var component in entity.Components)
				sb.AppendLine(PropertyTemplate.ProcessComponentTemplate(entity, component));

			List<ArchAngel.Providers.CodeProvider.DotNet.Property> codeComponentProps = ProcessTemplateProperties(sb.ToString());
			Dictionary<ArchAngel.Providers.EntityModel.Model.EntityLayer.Component, ArchAngel.Providers.CodeProvider.DotNet.Property> componentPropMap = new Dictionary<Component, ArchAngel.Providers.CodeProvider.DotNet.Property>();

			for (int i = 0; i < entity.Components.Count(); i++)
				componentPropMap.Add(entity.Components.ElementAt(i), codeComponentProps[i]);

			foreach (var component in entity.Components.OrderBy(c => c.Name))
			{
				// Add component's current name to its old name list
				if (component.OldNames.Count == 0 || !component.OldNames.Contains(componentPropMap[component].Name))
					component.OldNames.Add(componentPropMap[component].Name);

				var propertyConstraint = entity.MappedClass.EnsureHasProperty(componentPropMap[component].Name, component.OldNames)
							.WithType(componentPropMap[component].DataType.Name.Replace(string.Format("{0}.Model.", globalProjectNamespace), ""))
							.WithModifiers(componentPropMap[component].Modifiers.ToArray())
							.WithGetAccessorBody(componentPropMap[component].GetAccessor.BodyText)
							.WithSetAccessorBody(componentPropMap[component].SetAccessor.BodyText);

				if (componentPropMap[component].SetAccessor.Modifier == "private")
					propertyConstraint.WithSetAccessorAccessibility("private");

				propertyConstraint.ApplyTo(actions);
			}

			#endregion

			// Load the existing C# file up
			var filename = entity.EntitySet.MappingSet.CodeParseResults.GetFilenameForParsedClass(entity.MappedClass);

			CurrentFileName = filename;

			string outputText;
			if (TemplateCache.ContainsKey(filename) ||
				!System.IO.File.Exists(filename))
				outputText = actions.RunActions((string)TemplateCache[filename], entity.MappedClass.Controller.Root, false);
			else
				outputText = actions.RunActions(System.IO.File.ReadAllText(filename), entity.MappedClass.Controller.Root, true);

			TemplateCache[filename] = outputText;

			return outputText;
		}

		private List<ArchAngel.Providers.CodeProvider.DotNet.Property> ProcessTemplateProperties(string code)
		{
			List<ArchAngel.Providers.CodeProvider.DotNet.Property> codeProperties = new List<ArchAngel.Providers.CodeProvider.DotNet.Property>();

			string rawPropertyCode = string.Format(@"public class Dummy {{{0}}}", code);
			//string cleanPropertyCode = rawPropertyCode
			//    .Replace("#entity.Name#", "E_NAME")
			//    .Replace("#entity.Type#", "E_TYPE");

			var parseResults = ArchAngel.Providers.CodeProvider.ParseResults.ParseCSharpCode(rawPropertyCode);

			if (parseResults.Classes.Count != 1)
				throw new Exception("No class found.");

			return parseResults.Classes[0].Properties;
		}
	}
}
