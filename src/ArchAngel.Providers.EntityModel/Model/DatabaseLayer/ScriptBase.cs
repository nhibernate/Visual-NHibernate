using System;
using System.Collections.Generic;
using ArchAngel.Interfaces;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	public abstract class ScriptBase : ModelObject, IScriptBase
	{
		private string description;
		private bool enabled = true;
		private bool isUserDefined;
		private string name;
		private string schema;
		private Guid uid;

		public IDatabase Database { get; set; }

		public string Description
		{
			get { return description; }
			set
			{
				if (description != value)
				{
					description = value;
					RaisePropertyChanged("Description");
				}
			}
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled != value)
				{
					enabled = value;
					RaisePropertyChanged("Enabled");
				}
			}
		}

		public bool IsUserDefined
		{
			get { return isUserDefined; }
			set
			{
				if (isUserDefined != value)
				{
					isUserDefined = value;
					RaisePropertyChanged("IsUserDefined");
				}
			}
		}

		public string Name
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					name = value;
					RaisePropertyChanged("Name");
				}
			}
		}

		public string Schema
		{
			get { return schema; }
			set
			{
				if (schema != value)
				{
					schema = value;
					RaisePropertyChanged("Schema");
				}
			}
		}

		public virtual void ResetDefaults()
		{
		}

		[ArchAngel.Interfaces.Attributes.ApiExtension]
		public virtual bool NameValidate(out string failReason)
		{
			failReason = "";
			object retVal;
			if (ApiExtensionHelper.RunIfExtended(GetType(), "NameValidate", out retVal, failReason))
				return (bool)retVal;

			/*Don't check items that are not enabled*/
			if (!Enabled)
			{
				return true;
			}

			return ValidateSpacesAndEmptyness(Name, out failReason, "Name");
		}

		public static bool ValidateSpacesAndEmptyness(string plural, out string failReason, string propertyName)
		{
			failReason = "";
			if (string.IsNullOrEmpty(plural))
			{
				failReason = propertyName + " cannot be zero-length.";
				return false;
			}
			if (plural.IndexOf(" ") >= 0)
			{
				failReason = propertyName + " cannot have spaces.";
				return false;
			}
			return true;
		}

		public virtual bool ValidateObject(List<ValidationFailure> failures)
		{
			string tempFailureReason;
			if (NameValidate(out tempFailureReason) == false)
			{
				failures.Add(new ValidationFailure(tempFailureReason, "Name"));
			}

			return failures.Count == 0;
		}

		public bool Equals(IScriptBase obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;


			return Equals(obj.Name, Name);

			//return Equals(obj.UID, UID);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((ScriptBase)obj);
		}

		/// <summary>
		/// Performs a shallow clone of this object's properties.
		/// Does not inclue virtual properties, columns, relationships,
		/// or parent objects.
		/// </summary>
		/// <param name="obj"></param>
		public virtual void CopyInto(IScriptBase obj)
		{
			obj.Name = Name;
			//obj.UID = UID;
		}

		public override int GetHashCode()
		{
			return
				uid != Guid.Empty ? uid.GetHashCode() :
				name != null ? name.GetHashCode() :
				0;
		}

		public override string ToString()
		{
			return name;
		}

		public bool HasChanges(IScriptBase value)
		{
			return false;
		}
	}

	public class ValidationFailure
	{
		public readonly string Reason;
		public readonly string Property;

		public ValidationFailure(string reason, string property)
		{
			Reason = reason;
			Property = property;
		}
	}
}