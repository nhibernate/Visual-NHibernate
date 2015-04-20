using System;
using System.Collections.Generic;
using Slyce.Common;

namespace Slyce.IntelliMerge.Controller
{
	/// <summary>
	/// Contains methods useful for working with IFileInformation objects.
	/// </summary>
	public static class FileInformationUtility
	{
		private static readonly Dictionary<IntelliMergeType, string> intellimergeToDescription = new Dictionary<IntelliMergeType, string>();
		private static readonly Dictionary<string, IntelliMergeType> descriptionToIntellimerge = new Dictionary<string, IntelliMergeType>();

		/// <summary>
		/// Gets the description for the given IntelliMergeType enum value.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetIntelliMergeTypeDescription(IntelliMergeType type)
		{
			if (!intellimergeToDescription.ContainsKey(type))
			{
				intellimergeToDescription[type] = Utility.GetDescription(type);
				descriptionToIntellimerge[intellimergeToDescription[type]] = type;
			}
			return intellimergeToDescription[type];
		}

		/// <summary>
		/// Returns the IntelliMergeType with the given description. Throws an exception if the description does not exist.
		/// </summary>
		/// <param name="description"></param>
		/// <returns></returns>
		public static IntelliMergeType GetIntelliMergeType(string description)
		{
			if (!descriptionToIntellimerge.ContainsKey(description))
			{
				// If the enum is not already cached, we need to cache it. Unfortunately there is no good
				// way of doing this, so just cache the whole lot if this happens.
				foreach (string name in Enum.GetNames(typeof(IntelliMergeType)))
				{
					IntelliMergeType type = ((IntelliMergeType)Enum.Parse(typeof(IntelliMergeType), name, true));
					GetIntelliMergeTypeDescription(type);
				}
			}
			return descriptionToIntellimerge[description];
		}

		/// <summary>
		/// Gets the default IntelliMergeType for the given IFileInformation object.
		/// </summary>
		/// <param name="fileInfo">The file to get the default IntelliMergeType for.</param>
		/// <returns>The default IntelliMergeType that should be used if the IntelliMergeType is set to AutoDetect.</returns>
		public static IntelliMergeType GetDefaultIntelliMergeType(IFileInformation fileInfo)
		{
			return IntelliMergeType.Overwrite;

			/*
						if(fileInfo is BinaryFileInformation)
						{
							return IntelliMergeType.Overwrite;
						}
						if(fileInfo is TextFileInformation)
						{
							if (fileInfo.TemplateLanguage.HasValue == false)
							{
								// The template language was not set
								if (Path.GetExtension(fileInfo.RelativeFilePath).ToLower() == ".cs")
								{
									return IntelliMergeType.CSharp;
								}
					
								return IntelliMergeType.PlainText;
							}
				
							// The Template language was set, work off of that
							switch(fileInfo.TemplateLanguage.Value)
							{
								case Common.TemplateContentLanguage.CSharp:
									return IntelliMergeType.CSharp;
								default:
									return IntelliMergeType.PlainText;
							}
						}

						// Unsupported type. Throw an exception so this fails fast - someone has added a new type and needs to update this
						// code as well.
						throw new ArgumentException("The given file information object is unsupported by the FileInformationUtility.GetDefaultIntelliMergeType() method.");
			*/
		}
	}
}