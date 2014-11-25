using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace ArchAngel.Licensing
{
	public class LicenseHelper
	{
		public enum KeySizes
		{
			Key_56 = 56,
			Key_128 = 128,
			Key_256 = 256,
			Key_512 = 512,
			Key_1024 = 1024,
			Key_2048 = 2048
		}

		public static string CreateSignature(string text, string my_private_key)
		{
			RSACryptoServiceProvider rsacp = new RSACryptoServiceProvider(2048);
			rsacp.FromXmlString(my_private_key);

			ASCIIEncoding ByteConverter = new ASCIIEncoding();
			byte[] sign_this = ByteConverter.GetBytes(text);
			byte[] signature = rsacp.SignData(sign_this, new SHA1CryptoServiceProvider());
			string base64_string = Convert.ToBase64String(signature);

			return base64_string;
		}

		public static bool VerifySignature(string text, string base64_encoded_signature, string my_public_key)
		{
			RSACryptoServiceProvider rsacp = new RSACryptoServiceProvider(2048);
			rsacp.FromXmlString(my_public_key);

			ASCIIEncoding ByteConverter = new ASCIIEncoding();
			byte[] verify_this = ByteConverter.GetBytes(text);
			byte[] signature = Convert.FromBase64String(base64_encoded_signature);
			bool ok = rsacp.VerifyData(verify_this, new SHA1CryptoServiceProvider(), signature);
			return ok;
		}

		public static string CreateRsaPrivateKey(KeySizes keySize)
		{
			RSACryptoServiceProvider rsacp = new RSACryptoServiceProvider((int)keySize);
			string my_key_pair_formatted_as_an_xml_string = rsacp.ToXmlString(true);
			return my_key_pair_formatted_as_an_xml_string;
		}

		/// <summary>
		/// Formats the provided XML so it's indented and humanly-readable.
		/// </summary>
		/// <param name="xml">The input XML to format.</param>
		/// <returns></returns>
		public static string FormatXml(string xml)
		{
			XmlDocument document = new XmlDocument();
			document.Load(new StringReader(xml));
			StringBuilder builder = new StringBuilder();

			using (XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder)))
			{
				writer.Formatting = Formatting.Indented;
				writer.Indentation = 1;
				writer.IndentChar = '\t';
				document.Save(writer);
			}
			return builder.ToString();
		}
	}

}
