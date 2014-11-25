using System;

namespace ArchAngel.Interfaces
{
	public class ActionAttribute : Attribute
	{
		private string _DisplayName;
		private Type _OwnerType;
		private string _Description;
		private string _EditControlName;

		public ActionAttribute(string displayName, Type ownerType, string description, string editControlName)
		{
			DisplayName = displayName;
			OwnerType = ownerType;
			Description = description;
			EditControlName = editControlName;
		}

		public string DisplayName
		{
			get { return _DisplayName; }
			set { _DisplayName = value; }
		}

		public Type OwnerType
		{
			get { return _OwnerType; }
			set { _OwnerType = value; }
		}

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		public string EditControlName
		{
			get { return _EditControlName; }
			set { _EditControlName = value; }
		}
	}
}
