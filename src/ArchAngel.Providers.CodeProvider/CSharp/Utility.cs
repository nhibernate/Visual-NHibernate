using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public class Utility
	{
		internal static void WriteRegionStart(Controller controller, StringBuilder sb, string regionName)
		{
			if (controller.AddRegions)
			{
				sb.AppendLine(Environment.NewLine + controller.Indent + "#region " + regionName);
			}
		}

		internal static void WriteRegionEnd(Controller controller, StringBuilder sb)
		{
			if (controller.AddRegions)
			{
				sb.AppendLine(Environment.NewLine + controller.Indent + "#endregion");
			}
		}

		/*
				public static void UpdateProperties(BaseConstruct targetToUpdate, BaseConstruct sourceOfPropertyValues)
				{
					Type type = targetToUpdate.GetType();

					if (type == typeof(Class) ||
						type == typeof(Interface) ||
						type == typeof(Namespace) ||
						type == typeof(Struct))
					{
						targetToUpdate.Attributes = sourceOfPropertyValues.Attributes;

						//foreach (AttributeSection attributeSection in targetToUpdate.Attributes)
						//{
						//    attributeSection.ParentObject = targetToUpdate;
						//}
						targetToUpdate.Comments = sourceOfPropertyValues.Comments;
						targetToUpdate.Name = sourceOfPropertyValues.Name;
						targetToUpdate.Omit = sourceOfPropertyValues.Omit;
						targetToUpdate.XmlComments = sourceOfPropertyValues.XmlComments;

						if (type == typeof(Class))
						{
							((Class)targetToUpdate).BaseNames = ((Class)sourceOfPropertyValues).BaseNames;
							((Class)targetToUpdate).IsPartial = ((Class)sourceOfPropertyValues).IsPartial;
							//((Class)targetToUpdate).Functions = ((Class)sourceOfPropertyValues).Functions;

							//foreach (Function func in ((Class)targetToUpdate).Functions)
							//{
							//    func.ParentObject = targetToUpdate;
							//}
						}
						if (typeof(IVisibilityModifiers).IsInstanceOfType(targetToUpdate))
						{
							((IVisibilityModifiers)targetToUpdate).Modifiers = ((IVisibilityModifiers)sourceOfPropertyValues).Modifiers;
						}
					}
					else
					{
						sourceOfPropertyValues.ParentObject = targetToUpdate.ParentObject;
						//targetToUpdate = sourceOfPropertyValues;
					}
				}
		*/

		/*
				public static TypeOfDiff ModifyTypeOfDiff(TypeOfDiff currentTypeOfDiff, TypeOfDiff newTypeOfDiff)
				{
					switch (currentTypeOfDiff)
					{
						case TypeOfDiff.ExactCopy:
							return newTypeOfDiff;
						case TypeOfDiff.Conflict:
							return TypeOfDiff.Conflict;
						case TypeOfDiff.TemplateChangeOnly:
							switch (newTypeOfDiff)
							{
								case TypeOfDiff.Conflict:
								case TypeOfDiff.TemplateChangeOnly:
								case TypeOfDiff.UserAndTemplateChange:
								case TypeOfDiff.Warning:
									return newTypeOfDiff;
								case TypeOfDiff.ExactCopy:
									return currentTypeOfDiff;
								case TypeOfDiff.UserChangeOnly:
									return TypeOfDiff.UserAndTemplateChange;
								default:
									throw new NotImplementedException("Type of diff not handled yet.");
							}

						case TypeOfDiff.UserAndTemplateChange:
							switch (newTypeOfDiff)
							{
								case TypeOfDiff.Conflict:
									return newTypeOfDiff;
								case TypeOfDiff.ExactCopy:
								case TypeOfDiff.TemplateChangeOnly:
								case TypeOfDiff.UserChangeOnly:
								case TypeOfDiff.UserAndTemplateChange:
								case TypeOfDiff.Warning:
									return TypeOfDiff.UserAndTemplateChange;
								default:
									throw new NotImplementedException("Type of diff not handled yet.");
							}

						case TypeOfDiff.UserChangeOnly:
							switch (newTypeOfDiff)
							{
								case TypeOfDiff.Conflict:
								case TypeOfDiff.UserAndTemplateChange:
									return newTypeOfDiff;
								case TypeOfDiff.Warning:
								case TypeOfDiff.ExactCopy:
								case TypeOfDiff.UserChangeOnly:
									return TypeOfDiff.UserChangeOnly;
								case TypeOfDiff.TemplateChangeOnly:
									return TypeOfDiff.UserAndTemplateChange;
								default:
									throw new NotImplementedException("Type of diff not handled yet.");
							}

						case TypeOfDiff.Warning:
							switch (newTypeOfDiff)
							{
								case TypeOfDiff.Conflict:
								case TypeOfDiff.UserAndTemplateChange:
								case TypeOfDiff.UserChangeOnly:
								case TypeOfDiff.Warning:
								case TypeOfDiff.TemplateChangeOnly:
									return newTypeOfDiff;
								case TypeOfDiff.ExactCopy:
									return TypeOfDiff.Warning;
								default:
									throw new NotImplementedException("Type of diff not handled yet.");
							}
					}
					return TypeOfDiff.ExactCopy;
				}
		*/

		/*
				/// <summary>
				/// Gets the number of linebreaks in the supplied text.
				/// </summary>
				/// <param name="text">Text to check.</param>
				/// <param name="lineBreakCharacter">The string that has been for linebreaks ie: "\n", "\r" or "\r\n"</param>
				/// <returns></returns>
				public static int GetNumLines(string text, string lineBreakCharacter)
				{
					if (text.Length == 0)
					{
						return 0;
					}
					int index = -1;
					int numLines = 0;

					while (index < text.Length - 1)
					{
						index = text.IndexOf(lineBreakCharacter, index + 1);

						if (index > 0)
						{
							numLines++;
						}
						else
						{
							break;
						}
					}
					return numLines;
				}
		*/
		/// <summary>
		/// Returns whether the collections are the same. Uses the default comparer.
		/// </summary>
		/// <param name="list1">First collection.</param>
		/// <param name="list2">Second collection.</param>
		/// <returns>True if the collections are the same, false otherwise.</returns>
		public static bool BaseContructCollectionsAreTheSame(BaseConstruct[] list1, BaseConstruct[] list2)
		{
			return BaseContructCollectionsAreTheSame(list1, list2, null);
		}

		/// <summary>
		/// Returns whether the collections are the same.
		/// </summary>
		/// <param name="list1">First collection.</param>
		/// <param name="list2">Second collection.</param>
		/// <param name="comparer">Comparer to use when comparing objects.</param>
		/// <returns>True if the collections are the same, false otherwise.</returns>
		public static bool BaseContructCollectionsAreTheSame(BaseConstruct[] list1, BaseConstruct[] list2, IComparer<BaseConstruct> comparer)
		{
			return BaseContructCollectionsAreTheSame(list1, list2, comparer, ComparisonDepth.Complete);
		}

		/// <summary>
		/// Returns whether the collections are the same.
		/// </summary>
		/// <param name="list1">First collection.</param>
		/// <param name="list2">Second collection.</param>
		/// <param name="comparer">Comparer to use when comparing objects.</param>
		/// <param name="depth">The depth to which the comparison should be performed.</param>
		/// <returns>True if the collections are the same, false otherwise.</returns>
		public static bool BaseContructCollectionsAreTheSame(BaseConstruct[] list1, BaseConstruct[] list2, IComparer<BaseConstruct> comparer, ComparisonDepth depth)
		{
			Comparers.BaseConstructComparer.ResetType();

			if (list1.Length != list2.Length)
			{
				return false;
			}
			if (comparer != null)
			{
				Array.Sort(list1, comparer);
				Array.Sort(list2, comparer);
			}
			else
			{
				Array.Sort(list1);
				Array.Sort(list2);
			}

			for (int i = 0; i < list1.Length; i++)
			{
				if (!list1[i].IsTheSame(list2[i], depth))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns whether the collections are the same.
		/// </summary>
		/// <param name="list1">First collection.</param>
		/// <param name="list2">Second collection.</param>
		/// <param name="comparer">Comparer to use when comparing objects.</param>
		/// <returns>True if the collections are the same, false otherwise.</returns>
		public static bool CollectionsAreTheSame<T>(List<T> list1, List<T> list2, IComparer<T> comparer)
		{
			Comparers.BaseConstructComparer.ResetType();

			if (list1.Count != list2.Count)
			{
				return false;
			}
			if (comparer != null)
			{
				list1.Sort(comparer);
				list2.Sort(comparer);
			}
			else
			{
				list1.Sort();
				list2.Sort();
			}

			for (int i = 0; i < list1.Count; i++)
			{
				if (!list1[i].Equals(list2[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Gets whether two string collections are the same. Will not modify the Lists.
		/// </summary>
		/// <param name="list1"></param>
		/// <param name="list2"></param>
		/// <returns>True if both lists are the same, false otherwise.</returns>
		public static bool StringCollectionsAreTheSame(List<string> list1, List<string> list2)
		{
			return StringCollectionsAreTheSame(list1.ToArray(), list2.ToArray());
		}

		/// <summary>
		/// Gets whether two string collections are the same.
		/// </summary>
		/// <param name="list1"></param>
		/// <param name="list2"></param>
		/// <returns>True if both lists are the same, false otherwise.</returns>
		public static bool StringCollectionsAreTheSame(string[] list1, string[] list2)
		{
			if (list1 == null && list2 == null)
				return true;

			if (list1 == null || list2 == null)
				return false;

			// Modifiers
			if (list1.Length != list2.Length)
			{
				return false;
			}
			Array.Sort(list1);
			Array.Sort(list2);

			for (int i = 0; i < list1.Length; i++)
			{
				if (list1[i] != list2[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Adjusts the indents (tabs) of the text to the required level based on the
		/// supplied requiredStartIndent.
		/// </summary>
		/// <returns>The reformatted text with correct indents (tabs).</returns>
		/// <param name="text">The text to process.</param>
		/// <param name="requiredStartIndent">The indent string ("\t") to begin with.</param>
		public static string ResetIndents(string text, string requiredStartIndent)
		{
			int startPos = 0;
			// Skip to the first real line
			while (startPos < text.Length && text[startPos] == '\n')
			{
				startPos++;
			}

			int i = startPos;
			while (i < text.Length && text[i] == '\t')
			{
				i++;
			}
			//string currentIndent = text.Substring(startPos, i - 1);
			string currentIndent = text.Substring(startPos, i - startPos);
			int diff = currentIndent.Length - requiredStartIndent.Length;

			//if (currentIndent.Length > 0 && currentIndent.Length != requiredStartIndent.Length)
			if (currentIndent.Length != requiredStartIndent.Length)
			{
				string[] lines = text.Split('\n');

				for (int lineNum = 0; lineNum < lines.Length; lineNum++)
				{
					if (diff > 0)
					{
						i = 0;

						while (i < diff && i < lines[lineNum].Length && lines[lineNum][i] == '\t')
						{
							i++;
						}
						//i = i - requiredStartIndent.Length;

						if (i > 0)
						{
							lines[lineNum] = lines[lineNum].Substring(i);
						}
					}
					else if (diff < 0)
					{
						lines[lineNum] = new string('\t', diff * -1) + lines[lineNum];
					}
				}
				StringBuilder sb = new StringBuilder(lines.Length * 50);

				for (int lineNum = 0; lineNum < lines.Length; lineNum++)
				{
					sb.Append(lines[lineNum]);

					if (lineNum < lines.Length - 1)
					{
						sb.Append('\n');
					}
				}
				return sb.ToString();
			}
			return text;
		}

		/*
				/// <summary>
				/// Gets all sibling baseConstructs that appear at the same level (have the same
				/// parent) and are the same type as the supplied baseConstruct.
				/// </summary>
				/// <returns>List of BaseConstructs that have the same parent.</returns>
				/// <param name="baseConstruct">The object whose siblings to return.</param>
				public static List<BaseConstruct> GetSiblingsOfSameType(BaseConstruct baseConstruct)
				{
					Type type = baseConstruct.GetType();
					List<BaseConstruct> allSiblings = new List<BaseConstruct>();
					Type propertyType = baseConstruct.GetType();
					System.Reflection.PropertyInfo[] parentsProperties;
					System.Reflection.FieldInfo[] parentsFields;

					Type parentType = baseConstruct.ParentObject.GetType();
					parentsProperties = parentType.GetProperties();
					parentsFields = parentType.GetFields();
					string propertyName = "";
					string fieldName = "";

					foreach (System.Reflection.PropertyInfo siblingProp in parentsProperties)
					{
						if (siblingProp.PropertyType == propertyType && siblingProp.PropertyType.IsArray)
						{
							propertyName = siblingProp.Name;
							break;
						}
						if (siblingProp.PropertyType.IsGenericType && siblingProp.PropertyType.GetGenericTypeDefinition().Name == "List`1" && siblingProp.PropertyType.GetGenericArguments()[0] == type)
						{
							propertyName = siblingProp.Name;
							break;
						}
					}
					foreach (System.Reflection.FieldInfo siblingField in parentsFields)
					{
						if (siblingField.FieldType == propertyType && siblingField.FieldType.IsArray)
						{
							fieldName = siblingField.Name;
							break;
						}
				
						if (siblingField.FieldType.IsGenericType && siblingField.FieldType.GetGenericTypeDefinition().Name == "List`1" && siblingField.FieldType.GetGenericArguments()[0] == type)
						{
							fieldName = siblingField.Name;
							break;
						}
					}
					System.Collections.IEnumerable siblings = new BaseConstruct[0];

					if (!string.IsNullOrEmpty(propertyName))
					{
						siblings = (System.Collections.IEnumerable)parentType.GetProperty(fieldName).GetValue(baseConstruct.ParentObject, null);
					}
					else if (!string.IsNullOrEmpty(fieldName))
					{
						siblings = (System.Collections.IEnumerable)parentType.GetField(fieldName).GetValue(baseConstruct.ParentObject);
					}
					foreach (object sibling in siblings)
					{
						//if(sibling.Equals(baseConstruct) == false)
						//{
							allSiblings.Add((BaseConstruct)sibling);    
						//}
				
					}
					return allSiblings;
				}
		*/

		/// <summary>
		/// Merges a single item that can be compared using Equals(). Assigns the changed one to
		/// merged. If the type parameter is not a string or value type, this method will cause
		/// pain down the track, as it will copy the reference over. Strings are immutable, so
		/// cannot be changed, and value types are copied implicitly. Anything else could be dangerous!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="merged"></param>
		/// <param name="user"></param>
		/// <param name="newgen"></param>
		/// <param name="prevgen"></param>
		/// <returns>True if the items were merged successfully.</returns>
		internal static bool MergeSingleItem<T>(ref T merged, T user, T newgen, T prevgen)
		{
			bool templateChange = newgen.Equals(prevgen) == false;
			bool userChange = user.Equals(prevgen) == false;
			if (user.Equals(newgen))
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
				merged = user;
			}
			else if (templateChange)
			{
				merged = newgen;
			}
			else
			{
				merged = user;
			}

			return true;
		}

		/// <summary>
		/// Determines the DataType to Clone and assigns that to merged.
		/// </summary>
		/// <param name="merged">The target DataType.</param>
		/// <param name="user">The User's version of the DataType.</param>
		/// <param name="newgen">The Template's version of the DataType.</param>
		/// <param name="prevgen">The previously generated version of the DataType.</param>
		/// <returns>True if the items were merged successfully.</returns>
		internal static bool MergeDataType(ref DotNet.DataType merged, DotNet.DataType user, DotNet.DataType newgen, DotNet.DataType prevgen)
		{
			bool templateChange = newgen.Equals(prevgen) == false;
			bool userChange = user.Equals(prevgen) == false;

			if (user.Equals(newgen))
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
				merged = user.Clone();
			}
			else if (templateChange)
			{
				merged = newgen.Clone();
			}

			return true;
		}

		internal static bool MergeModifiers(IVisibilityModifiers merged, IVisibilityModifiers user, IVisibilityModifiers newgen, IVisibilityModifiers prevgen)
		{
			bool userChange = false, templateChange = false;

			if (StringCollectionsAreTheSame(user.Modifiers.ToArray(), prevgen.Modifiers.ToArray()) == false)
			{
				userChange = true;
			}
			if (StringCollectionsAreTheSame(newgen.Modifiers.ToArray(), prevgen.Modifiers.ToArray()) == false)
			{
				templateChange = true;
			}

			if (StringCollectionsAreTheSame(user.Modifiers.ToArray(), newgen.Modifiers.ToArray()))
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
				merged.Modifiers = new List<string>(user.Modifiers);
			}
			else if (templateChange)
			{
				merged.Modifiers = new List<string>(newgen.Modifiers);
			}
			else
			{
				merged.Modifiers = new List<string>(user.Modifiers);
			}

			return true;
		}

		internal static bool MergeStringCollections(List<string> merged, List<string> user, List<string> newgen, List<string> prevgen)
		{
			bool userChange = false, templateChange = false;
			// Comments
			if (StringCollectionsAreTheSame(user.ToArray(), prevgen.ToArray()) == false)
				userChange = true;
			if (StringCollectionsAreTheSame(newgen.ToArray(), prevgen.ToArray()) == false)
				templateChange = true;
			if (StringCollectionsAreTheSame(newgen.ToArray(), user.ToArray()))
			{
				userChange = true;
				templateChange = false;
			}

			merged.Clear();

			if (templateChange && userChange)
			{
				return false;
			}
			if (userChange)
			{
				merged.AddRange(user);
			}
			else if (templateChange)
			{
				merged.AddRange(newgen);
			}
			else
			{
				// All are the same
				merged.AddRange(user);
			}
			return true;
		}
	}
}
