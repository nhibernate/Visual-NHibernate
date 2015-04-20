using System.Reflection;
using System.Text;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.NHibernateHelper
{
	public class PropertyTemplate
	{
		public static string ProcessPropertyTemplate(Entity entity, Property property)
		{
			System.Text.StringBuilder sb = new StringBuilder(((SourceCodeMultiLineType)SharedData.CurrentProject.GetUserOption("PropertyTemplate")).Value);

			sb.Replace("#entity.Name#", entity.Name);
			sb.Replace("#property.Name#", property.Name);
			sb.Replace("#property.Type#", property.Type.Replace(SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString() + ".Model.", ""));

			ProcessIfStatement(sb, "property.IsInherited", property.IsInherited);
			ProcessIfStatement(sb, "property.SetterIsPrivate", (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") || (bool)property.GetUserOptionValue("Property_UsePrivateSetter"));

			return RemoveTrailingLineBreaks(sb.ToString());
		}

		public static string ProcessReferenceTemplate(Entity entity, DirectedReference reference)
		{
			System.Text.StringBuilder sb = new StringBuilder(((SourceCodeMultiLineType)SharedData.CurrentProject.GetUserOption("ReferenceTemplate")).Value);

			string typeName;

			if (reference.FromEndCardinality == ArchAngel.Interfaces.Cardinality.Many)
				typeName = NHCollections.GetCollectionType(reference).Replace(SharedData.CurrentProject.GetUserOption("ProjectNamespace").ToString() + ".Model.", "");
			else
				typeName = reference.ToEntity.Name;

			sb.Replace("#entity.Name#", entity.Name);
			sb.Replace("#reference.Name#", reference.FromName);
			sb.Replace("#reference.Type#", typeName);

			bool usePrivateSetter = (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") ||
									(reference.Entity1IsFromEnd ? (bool)reference.Reference.GetUserOptionValue("End1UsePrivateSetter") : (bool)reference.Reference.GetUserOptionValue("End2UsePrivateSetter"));

			ProcessIfStatement(sb, "reference.SetterIsPrivate", usePrivateSetter);

			return RemoveTrailingLineBreaks(sb.ToString());
		}

		public static string ProcessComponentTemplate(Entity entity, ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component)
		{
			System.Text.StringBuilder sb = new StringBuilder(((SourceCodeMultiLineType)SharedData.CurrentProject.GetUserOption("ComponentTemplate")).Value);

			string specName = component.Specification.Name;
			string componentName = component.Name;

			sb.Replace("#entity.Name#", entity.Name);
			sb.Replace("#component.Name#", component.Name);
			sb.Replace("#component.Type#", specName);

			bool usePrivateSetter = (bool)SharedData.CurrentProject.GetUserOption("UsePrivateSettersOnProperties") ||
									(bool)component.GetUserOptionValue("Component_UsePrivateSetter");

			ProcessIfStatement(sb, "component.SetterIsPrivate", usePrivateSetter);

			return RemoveTrailingLineBreaks(sb.ToString());
		}

		private static MethodInfo mystaticMethodInfo;
		private static int CodeHash = 0;

		public static string ProcessEntityTemplate(Entity entity)
		{
			return "TODO";
			//            Scripting.Model.Entity scriptEntity = new ArchAngel.NHibernateHelper.Scripting.Model.Entity(entity);
			//            string code = ((SourceCodeMultiLineType)SharedData.CurrentProject.GetUserOption("EntityFileTemplate")).Value;
			//            StringBuilder sb = new StringBuilder();
			//            sb.AppendLine(@"
			//            using System;
			//            using System.Linq;
			//            using System.Text;
			//            using System.Collections.Generic;
			//
			//            public class Test
			//            {
			//                private static Stack<StringBuilder> _SBStack = new Stack<StringBuilder>();
			//
			//                public static class Project
			//                {");

			//            sb.AppendLine("        public static string Guid { get { return \"" + (string)SharedData.CurrentProject.GetUserOption("ProjectGuid") + "\"; } }");
			//            sb.AppendLine("        public static string Name { get { return \"" + (string)SharedData.CurrentProject.GetUserOption("ProjectName") + "\"; } }");
			//            sb.AppendLine("        public static string Namespace { get { return \"" + (string)SharedData.CurrentProject.GetUserOption("ProjectNamespace") + "\"; } }");
			//            sb.AppendLine(@"   }
			//
			//                public static string RunEntityFileTemplate(ArchAngel.NHibernateHelper.Scripting.Model.Entity entity)
			//                {
			//                    ");
			//            sb.AppendLine(Slyce.Common.Scripter.FormatFunctionBody("ProcessEntityFileTemplateFunction", code));
			//            sb.AppendLine(@"
			//                }");
			//            Slyce.Common.Scripter.InsertWriteCalls(sb);
			//            sb.Append(@"
			//            }");

			//            if (code.GetHashCode() != CodeHash)
			//            {
			//                CodeHash = code.GetHashCode();
			//                List<string> referencedAssemblies = new List<string>();
			//                referencedAssemblies.Add(typeof(ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity).Assembly.CodeBase.Replace(@"file:///", ""));
			//                referencedAssemblies.Add(typeof(ArchAngel.NHibernateHelper.EntityLoader).Assembly.CodeBase.Replace(@"file:///", ""));
			//                System.Reflection.Assembly ass = Slyce.Common.Scripter.CompileCode(sb.ToString(), referencedAssemblies);
			//                mystaticMethodInfo = ass.GetType("Test").GetMethod("RunEntityFileTemplate");
			//            }
			//            object[] parms = new object[] { scriptEntity };
			//string result = (string)mystaticMethodInfo.Invoke(null, parms);
			//return result;
		}

		private static void ProcessIfStatement(StringBuilder sb, string ifStatement, bool value)
		{
			int ifIndex = 0;
			ifIndex = sb.ToString().IndexOf("#" + ifStatement, ifIndex) + 1;

			while (ifIndex >= 1)
			{
				if (ifIndex >= 1)
				{
					int start = sb.ToString().IndexOf("?", ifIndex) + 1;
					int end = sb.ToString().IndexOf("#", ifIndex);

					string payload = sb.ToString().Substring(start, end - start);
					bool hasFalsePart = sb.ToString().IndexOf(":", start) >= 0 && sb.ToString().IndexOf(":", start) < end;

					string truePart = "";
					string falsePart = "";

					if (hasFalsePart)
					{
						string[] parts = payload.Split(':');
						truePart = parts[0].Trim(' ').Trim('"');
						falsePart = parts[1].Trim(' ').Trim('"');
					}
					else
						truePart = payload.Trim(' ').Trim('"');

					if (value)
					{
						sb.Remove(ifIndex - 1, end - ifIndex + 2);
						sb.Insert(ifIndex - 1, truePart);
					}
					else
					{
						sb.Remove(ifIndex - 1, end - ifIndex + 2);
						sb.Insert(ifIndex - 1, falsePart);
					}
				}
				ifIndex = sb.ToString().IndexOf("#" + ifStatement, ifIndex) + 1;
			}
		}

		private static string RemoveTrailingLineBreaks(string text)
		{
			return text.Trim('\r', '\n').Replace("\n", "\n\t\t") + "\n";
		}
	}
}
