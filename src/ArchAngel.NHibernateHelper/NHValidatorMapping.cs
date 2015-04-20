using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.NHibernateHelper
{
	public class NHValidatorMapping
	{
		public static string CreateXmlForEntity(Entity entity, string assembly, string @namespace)
		{
			using (var ms = new MemoryStream())
			using (var writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = Encoding.UTF8 }))
			{
				writer.WriteStartDocument();
				writer.WriteStartElement("nhv-mapping", "urn:nhibernate-validator-1.0");
				writer.WriteAttributeString("assembly", assembly);
				writer.WriteAttributeString("namespace", @namespace);
				writer.WriteStartElement("class", "urn:nhibernate-validator-1.0");
				writer.WriteAttributeString("name", entity.Name);

				foreach (var property in entity.Properties.Except(entity.ForeignKeyPropertiesToExclude).OrderBy(p => p.Name))
				{
					var constraintActions = new List<Action<XmlWriter, ValidationOptions>>();
					var options = property.ValidationOptions;
					var optsToUse = ValidationOptions.GetApplicableValidationOptionsForType(property.Type);
					constraintActions.AddRange(GetObjectConstraints(options, optsToUse));
					constraintActions.AddRange(GetStringConstraints(options, optsToUse));
					constraintActions.AddRange(GetIntegerConstraints(options, optsToUse));
					constraintActions.AddRange(GetDateConstraints(options, optsToUse));

					// If no nodes were created, don't both creating a property node
					if (constraintActions.Any() == false) continue;

					writer.WriteStartElement("property", "urn:nhibernate-validator-1.0");
					writer.WriteAttributeString("name", property.Name);

					foreach (var action in constraintActions)
						action(writer, property.ValidationOptions);

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Flush();
				writer.Close();
				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}

		private static IEnumerable<Action<XmlWriter, ValidationOptions>> GetObjectConstraints(ValidationOptions options, ApplicableOptions optsToUse)
		{
			if (optsToUse.IsSet(ApplicableOptions.Nullable) && options.Nullable.HasValue && options.Nullable == false)
				yield return (w, p) => w.WriteElementString("not-null", "urn:nhibernate-validator-1.0", "");

			if (optsToUse.IsSet(ApplicableOptions.Validate) && options.Validate)
				yield return (w, p) => w.WriteElementString("valid", "urn:nhibernate-validator-1.0", "");
		}

		private static IEnumerable<Action<XmlWriter, ValidationOptions>> GetDateConstraints(ValidationOptions options, ApplicableOptions optsToUse)
		{
			if (!optsToUse.IsSet(ApplicableOptions.Date))
				yield break;

			if (options.PastDate.HasValue && options.PastDate == true)
				yield return (w, p) => w.WriteElementString("past", "urn:nhibernate-validator-1.0", "");

			if (options.FutureDate.HasValue && options.FutureDate == true)
				yield return (w, p) => w.WriteElementString("future", "urn:nhibernate-validator-1.0", "");
		}

		private static IEnumerable<Action<XmlWriter, ValidationOptions>> GetStringConstraints(ValidationOptions options, ApplicableOptions optsToUse)
		{
			var maxLengthDefined = options.MaximumLength.HasValue && options.MaximumLength > 0 && options.MaximumLength < int.MaxValue;
			var minLengthDefined = options.MinimumLength.HasValue && options.MinimumLength > 0 && options.MinimumLength < int.MaxValue;
			var notEmptyDefined = options.NotEmpty.HasValue && options.NotEmpty.Value;
			var regexDefined = !string.IsNullOrEmpty(options.RegexPattern);

			if (optsToUse.IsSet(ApplicableOptions.NotEmpty) && notEmptyDefined)
				yield return (w, p) => w.WriteElementString("not-empty", "urn:nhibernate-validator-1.0", "");

			if (optsToUse.IsSet(ApplicableOptions.RegexPattern) && regexDefined)
			{
				yield return (w, p) =>
								{
									w.WriteStartElement("pattern", "urn:nhibernate-validator-1.0");
									w.WriteAttributeString("regex", options.RegexPattern);
									w.WriteEndElement();
								};
			}

			if (optsToUse.IsSet(ApplicableOptions.Value) && (maxLengthDefined || minLengthDefined))
			{
				yield return (w, p) =>
					{
						w.WriteStartElement("length", "urn:nhibernate-validator-1.0");
						if (maxLengthDefined)
							w.WriteAttributeString("max", p.MaximumLength.ToString());
						if (minLengthDefined)
							w.WriteAttributeString("min", p.MinimumLength.ToString());
						w.WriteEndElement();
					};
			}
		}

		private static IEnumerable<Action<XmlWriter, ValidationOptions>> GetIntegerConstraints(ValidationOptions options, ApplicableOptions optsToUse)
		{
			var integerDigitsDefined = HasValue(options.IntegerDigits);
			var fractionalDigitsDefined = HasValue(options.FractionalDigits);
			var minValueDefined = HasValue(options.MinimumValue);
			var maxValueDefined = HasValue(options.MaximumValue);

			if (optsToUse.IsSet(ApplicableOptions.Value) && (minValueDefined || maxValueDefined))
			{
				yield return (w, p) =>
						{
							w.WriteStartElement("range", "urn:nhibernate-validator-1.0");
							if (minValueDefined)
								w.WriteAttributeString("min", p.MinimumValue.Value.ToString(CultureInfo.InvariantCulture));
							if (maxValueDefined)
								w.WriteAttributeString("max", p.MaximumValue.Value.ToString(CultureInfo.InvariantCulture));

							w.WriteEndElement();
						};
			}

			if (optsToUse.IsSet(ApplicableOptions.Digits) && (integerDigitsDefined || fractionalDigitsDefined))
			{
				yield return (w, p) =>
				{
					w.WriteStartElement("digits", "urn:nhibernate-validator-1.0");
					if (integerDigitsDefined)
						w.WriteAttributeString("integerDigits", p.IntegerDigits.Value.ToString(CultureInfo.InvariantCulture));
					if (fractionalDigitsDefined)
						w.WriteAttributeString("fractionalDigits", p.FractionalDigits.Value.ToString(CultureInfo.InvariantCulture));

					w.WriteEndElement();
				};
			}
		}

		private static bool HasValue(int? nullable)
		{
			return nullable.HasValue && nullable > 0 && nullable < int.MaxValue;
		}
	}
}
