using System;
using System.Globalization;
using System.Xml;

namespace ArchAngel.Licensing
{
	public class License
	{
		public enum LicenseTypes
		{
			Full,
			Trial
		}

		public License()
		{
		}

		public License(string xml)
		{
			CultureInfo englishProvider = CultureInfo.CreateSpecificCulture("en-US");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlNode root = doc.SelectSingleNode("slyce-license");
			XmlNode licenseNode = root.SelectSingleNode("license");
			XmlNode orderNode = root.SelectSingleNode("order");
			XmlNode purchaserNode = orderNode.SelectSingleNode("purchaser");
			XmlNode addressNode = purchaserNode.SelectSingleNode("address");

			this.Version = int.Parse(root.Attributes["version"].Value);
			this.Product = root.SelectSingleNode("product").InnerText;
			this.Serial = licenseNode.Attributes["serial"].Value;
			this.Type = (LicenseTypes)Enum.Parse(typeof(LicenseTypes), licenseNode.Attributes["type"].Value);
			this.HardwareId = licenseNode.Attributes["hardware-id"].Value;
			this.ExpiryDate = DateTime.ParseExact(licenseNode.Attributes["expiry-date"].Value, "d MMM yyyy", englishProvider);
			this.Quantity = int.Parse(licenseNode.Attributes["quantity"].Value);
			this.OrderReference = orderNode.Attributes["reference"].Value;
			this.OrderDate = DateTime.ParseExact(orderNode.Attributes["date"].Value, "d MMM yyyy", englishProvider);
			this.Company = purchaserNode.Attributes["company"].Value;
			this.Name = purchaserNode.Attributes["name"].Value;
			this.Email = purchaserNode.Attributes["email"].Value;
			this.Phone = purchaserNode.Attributes["phone"].Value;
			this.AddressLine1 = addressNode.Attributes["line1"].Value;
			this.AddressLine2 = addressNode.Attributes["line2"].Value;
			this.AddressCity = addressNode.Attributes["city"].Value;
			this.AddressCountry = addressNode.Attributes["country"].Value;
		}

		public int Version { get; set; }
		public LicenseTypes Type { get; set; }
		public string Product { get; set; }
		public string Serial { get; set; }
		public string HardwareId { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		public int Quantity { get; set; }
		public string OrderReference { get; set; }
		public string Company { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressCity { get; set; }
		public string AddressCountry { get; set; }

		public string ToXml(string privateKey)
		{
			string xml = string.Format(@"<slyce-license version=""{0}"">
								<product>{1}</product>
								<license serial=""{2}"" type=""{3}"" hardware-id=""{4}"" expiry-date=""{5}"" quantity=""{6}"" />
								<order reference=""{7}"" date=""{8}"">
									<purchaser company=""{9}"" name=""{10}"" email=""{11}"" phone=""{12}"">
										<address line1=""{13}"" line2=""{14}"" city=""{15}"" country=""{16}"" />
									</purchaser>
								</order>
								<signature />
							</slyce-license>",
								Version,
								Product,
								Serial,
								Type,
								HardwareId,
								ExpiryDate.ToString("d MMM yyyy"),
								Quantity,
								OrderReference,
								OrderDate,
								Company,
								Name,
								Email,
								Phone,
								AddressLine1,
								AddressLine2,
								AddressCity,
								AddressCountry);

			xml = LicenseHelper.FormatXml(xml);
			string signature = LicenseHelper.CreateSignature(xml.Substring(xml.IndexOf(@"?>") + 2).Replace("<signature />", "").Replace("  ", " ").Replace("\r", "").Replace("\n", "").Replace("\t", ""), privateKey);
			xml = xml.Replace("<signature />", "<signature>" + signature + "</signature>");
			return LicenseHelper.FormatXml(xml);
		}

		public static bool IsValidLicense(string licenseXml, string publicKey)
		{
			licenseXml = LicenseHelper.FormatXml(licenseXml);
			int start = licenseXml.IndexOf("<signature>");
			int end = licenseXml.IndexOf("</signature>") + "</signature>".Length;

			int sigValueStart = start + "<signature>".Length;
			int sigValueEnd = end - "</signature>".Length;
			string signature = licenseXml.Substring(sigValueStart, sigValueEnd - sigValueStart);
			licenseXml = licenseXml.Substring(0, start) + "<signature />" + licenseXml.Substring(end);
			licenseXml = LicenseHelper.FormatXml(licenseXml);

			return LicenseHelper.VerifySignature(licenseXml, signature, publicKey);
		}
	}

}
