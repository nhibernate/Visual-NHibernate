using System;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Slyce.Common.StringExtensions
{
	public static class StringExtensionMethods
	{
		public static string AddTabs(this string text, int count)
		{
			if (text == null)
				text = "";

			string tabs = new string('\t', count);

			if (!text.StartsWith("\n") && !text.StartsWith("\r"))
				return tabs + text.Replace("\n", "\n" + tabs);

			return text.Replace("\n", "\n" + tabs);
		}

		public static string RemoveTabs(this string text, int count)
		{
			string tabs = new string('\t', count);
			return text.Replace("\n" + tabs, "\n");
		}

		public static T? As<T>(this string s) where T : struct, IConvertible
		{
			if (string.IsNullOrEmpty(s))
				return null;

			try
			{
				Type type = typeof(T);
				bool isEnum = typeof(Enum).IsAssignableFrom(type);
				return (T)(isEnum
					? Enum.Parse(type, s, true)
					: Convert.ChangeType(s, type, CultureInfo.InvariantCulture));
			}
			catch
			{
				return default(T);
			}
		}

		public static object As(this string s, Type type)
		{
			if (string.IsNullOrEmpty(s))
				return null;

			try
			{
				bool isEnum = typeof(Enum).IsAssignableFrom(type);
				return (isEnum
					? Enum.Parse(type, s, true)
					: Convert.ChangeType(s, type));
			}
			catch
			{
				if (type.BaseType == typeof(Enum))
				{
					// Set default value
					return 0;
				}
				return null;
			}
		}

		public static string BackTick(this string name)
		{
			return "`" + name + "`";
		}

		public static string UnBackTick(this string name)
		{
			return name == null ? "" : name.Trim('`');
		}

		public static string ToCamelCase(this string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return "";
			}
			bool toLower = true;
			bool toUpper = false;
			char[] letters = name.ToCharArray();

			for (int i = 0; i < letters.Length; i++)
			{
				string letter = letters[i].ToString();
				string nextLetter = null;

				if (i < letters.Length - 2)
				{
					nextLetter = letters[i + 1].ToString();
				}
				if (toLower && nextLetter != null && nextLetter == nextLetter.ToLower() && i > 0)
				{
					toUpper = true;
					toLower = false;
				}
				if (toLower)
				{
					letters[i] = letter.ToLower().ToCharArray()[0];
				}
				if (toUpper)
				{
					if (i > 1)
					{
						letters[i] = letter.ToUpper().ToCharArray()[0];
					}
					break;
				}
			}
			name = "";

			foreach (char letter in letters)
			{
				name += letter.ToString();
			}
			return name;
		}

		public static string ToSingleWord(this string multiWords)
		{
			multiWords = multiWords.Trim();

			if (string.IsNullOrEmpty(multiWords))
			{
				return "";
			}
			/*Replace multi-spaces with single spaces*/
			string[] words = System.Text.RegularExpressions.Regex.Replace(multiWords, @"\s\s", "").Split(' ');
			StringBuilder sb = new StringBuilder(50);

			for (int i = 0; i < words.Length; i++)
			{
				/*Capitalise first letter*/
				sb.Append(words[i].Substring(0, 1).ToUpper());

				if (words[i].Length > 1)
				{
					sb.Append(words[i].Substring(1));
				}
			}
			return sb.ToString();
		}

		public static string ToTitleCase(this string name)
		{
			if (name == null)
				return "";

			name = name.Replace("_", " ");
			string returnString = "";
			string[] ary = name.Split(' ');

			foreach (string str in ary)
			{
				string word = str.Trim();

				if (word.Length == 1)
					returnString += word.Substring(0, 1).ToUpper();
				else if (word.Length > 1)
				{
					char[] buffer = word.ToCharArray();
					buffer[0] = char.ToUpper(buffer[0]);

					for (int i = 1; i < buffer.Length; i++)
					{
						if (char.IsLetter(word[i]))
						{
							if (!char.IsLetter(word[i - 1]) ||
								(char.IsLower(word[i - 1]) && char.IsUpper(word[i])))
								buffer[i] = char.ToUpper(word[i]);
							else
								buffer[i] = char.ToLower(word[i]);
						}
						else
							buffer[i] = word[i];
					}

					returnString += new string(buffer);
				}
			}
			return returnString;
		}

		#region Encryption methods
		private const DataProtectionScope Scope = DataProtectionScope.LocalMachine;

		/// <summary>
		/// Encrypts a given password and returns the encrypted data
		/// as a base64 string.
		/// </summary>
		/// <param name="plainText">An unencrypted string that needs
		/// to be secured.</param>
		/// <returns>A base64 encoded string that represents the encrypted
		/// binary data.
		/// </returns>
		/// <remarks>This solution is not really secure as we are
		/// keeping strings in memory. If runtime protection is essential,
		/// <see cref="SecureString"/> should be used.</remarks>
		/// <exception cref="ArgumentNullException">If <paramref name="plainText"/>
		/// is a null reference.</exception>
		public static string Encrypt(this string plainText)
		{
			if (plainText == null) throw new ArgumentNullException("plainText");

			//encrypt data
			var data = Encoding.Unicode.GetBytes(plainText);
			byte[] encrypted = ProtectedData.Protect(data, null, Scope);

			//return as base64 string
			return Convert.ToBase64String(encrypted);
		}

		/// <summary>
		/// Decrypts a given string.
		/// </summary>
		/// <param name="cipher">A base64 encoded string that was created
		/// through the <see cref="Encrypt(string)"/> or
		/// <see cref="Encrypt(SecureString)"/> extension methods.</param>
		/// <returns>The decrypted string.</returns>
		/// <remarks>Keep in mind that the decrypted string remains in memory
		/// and makes your application vulnerable per se. If runtime protection
		/// is essential, <see cref="SecureString"/> should be used.</remarks>
		/// <exception cref="ArgumentNullException">If <paramref name="cipher"/>
		/// is a null reference.</exception>
		public static string Decrypt(this string cipher)
		{
			if (cipher == null) throw new ArgumentNullException("cipher");

			//parse base64 string
			byte[] data = Convert.FromBase64String(cipher);

			//decrypt data
			byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);
			return Encoding.Unicode.GetString(decrypted);
		}

		#endregion


	}
}
