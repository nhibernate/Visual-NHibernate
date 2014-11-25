using System;
using System.Reflection;
using ArchAngel.Interfaces.ITemplate;
using System.Diagnostics;

namespace ArchAngel.Interfaces
{
	[Serializable]
	[DotfuscatorDoNotRename]
	[DebuggerDisplay("Name = {Name}, Value = {Value}")]
	public class UserOption : IUserOption
#if FAST_SERIALIZATION
 ,ISerializable
#endif
	{
		[DotfuscatorDoNotRename]
		private string _name;
		[DotfuscatorDoNotRename]
		private Type _dataType;
		[DotfuscatorDoNotRename]
		private object _value;
		[DotfuscatorDoNotRename]
		private IScriptBaseObject _owner;
		//[DotfuscatorDoNotRename]
		//[NonSerialized]
		//private System.Collections.Hashtable HashMemberInfos = new System.Collections.Hashtable();
		[NonSerialized]
		private IOption _CorrespondingOption = null;

		public UserOption()
		{
		}

		public UserOption(string name, Type dataType, object value)
		{
			Name = name;
			DataType = dataType;
			Value = value;
		}

#if FAST_SERIALIZATION
		/// <exclude/>
		public UserOption(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base()
		{
			try
			{
				using (Slyce.Common.SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					_name = reader.ReadString();
					_dataType = (Type)reader.ReadObject();
					_value = reader.ReadObject();
				}
			}
			catch
			{
				this._name = serializationInfo.GetString("_name");
				this._dataType = (Type)serializationInfo.GetValue("_dataType", typeof(Type));
				this._value = serializationInfo.GetValue("_value", typeof(object));
			}
		}
#endif

#if FAST_SERIALIZATION
		/// <exclude/>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			using (SerializationWriter writer = new SerializationWriter())
			{
				writer.Write(_name);
				writer.WriteObject(_dataType);
				writer.WriteObject(_value);
				info.AddValue("d", writer.ToArray());
			}

		}
#endif

		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _name, value);
					_name = value;
				}
			}
		}

		public Type DataType
		{
			get { return _dataType; }
			set
			{
				if (_dataType != value)
				{
					Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _dataType, value);
					_dataType = value;
				}
			}
		}

		public object Value
		{
			get
			{
				if (_value == null)
					_value = DefaultValue;

				return _value;
			}
			set
			{
				if (_value != value)
				{
					Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _value, value);
					_value = value;
				}
			}
		}

		public IScriptBaseObject Owner
		{
			get { return _owner; }
			set
			{
				if (_owner != value)
				{
					Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _owner, value);
					_owner = value;
				}
			}
		}

		// This right here makes it very hard to unit test these objects. We should really
		// set the Corresponding Option in the Workbench project loading code. That would
		// take a bit of work, and because of other dependencies in the system (mostly the
		// monolithic process of loading a workbench project, which i haven't yet fixed) I can't
		// get adequate unit tests around everything to ensure I don't break anything.
		private IOption CorrespondingOption
		{
			get
			{
				if (_CorrespondingOption == null)
				{
					foreach (IOption option in SharedData.CurrentProject.Options)
					{
						if (Name == option.VariableName)
						{
							_CorrespondingOption = option;
							break;
						}
					}
				}
				return _CorrespondingOption;
			}
		}

		/// <summary>
		/// Calls the DefaultValue function in the Template assembly and returns it's value.
		/// </summary>
		public object DefaultValue
		{
			// Another thing that needs changing. We shouldn't be relying on SharedData.CurrentProject, but the
			// only other way to do it would be to create a TemplateLoader property on this object and set that.
			// Not the most terrible idea in the world, but it would be a bit more difficult in the short term.
			get
			{
				if (Owner != null)
					return SharedData.CurrentProject.TemplateLoader.CallDefaultValueFunction(CorrespondingOption, new object[] { Owner });

				throw new Exception("This UserOption (" + Name + ") does not have a parent, so is not a virtual property and this function, DefaultValue, should not be called.");
			}
		}

		public string Description
		{
			get
			{
				if (Owner != null)
					return CorrespondingOption.Description;

				throw new Exception("This UserOption (" + Name + ") does not have a parent, so is not a virtual property and this function, Description, should not be called.");
			}
		}

		public string[] EnumValues
		{
			get
			{
				if (Owner != null)
					return CorrespondingOption.EnumValues;

				throw new Exception("This UserOption (" + Name + ") does not have a parent, so is not a virtual property and this function, EnumValues, should not be called.");
			}
		}

		public string Text
		{
			get
			{
				if (Owner != null)
					return CorrespondingOption.Text;

				throw new Exception("This UserOption (" + Name + ") does not have a parent, so is not a virtual property and this function, Text, should not be called.");
			}
		}

		public bool IsValid(bool deepCheck, out string failReason)
		{
			if (CorrespondingOption.IsValidValue.HasValue)
			{
				failReason = "";
				return CorrespondingOption.IsValidValue.Value;
			}
			if (Owner != null)
				return SharedData.CurrentProject.IsValid(CorrespondingOption.ValidatorFunction, Owner, out failReason);

			throw new Exception("This UserOption does not have a parent, so is not a virtual property and this function, IsValid, should not be called.");
		}

		public bool DisplayToUser
		{
			get
			{
				if (Owner != null)
					return SharedData.CurrentProject.TemplateLoader.CallDisplayToUserFunction(CorrespondingOption, new object[] { Owner });

				throw new Exception("This UserOption (" + Name + ") does not have a parent, so is not a virtual property and this function, DisplayToUser, should not be called.");

			}
		}
	}
}
