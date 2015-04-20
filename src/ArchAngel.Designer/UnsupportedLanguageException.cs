using System;
using System.Runtime.Serialization;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for UnsupportedLanguageExecption.
	/// </summary>
	[Serializable]
	public class UnsupportedLanguageExecption : Exception, ISerializable
	{
		private string extension;
		
		public UnsupportedLanguageExecption(SerializationInfo info, StreamingContext ctxt)
			: base(info, ctxt)
		{
            Slyce.Common.Utility.CheckForNulls(new object[] { info, ctxt }, new string[] { "info", "ctxt" });
			extension = info.GetString("extension");
		}

		public UnsupportedLanguageExecption(string extension)
		{
			this.extension = extension;
		}
	
		public string Extension
		{
			get
			{
				return extension;
			}
			set
			{
				extension = value;
			}
		}

		#region Implementation of ISerializable
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
            Slyce.Common.Utility.CheckForNulls(new object[] { info, context }, new string[] { "info", "context" });

			base.GetObjectData(info, context);
			info.AddValue("extension", extension);
		}
		#endregion

	}
}
