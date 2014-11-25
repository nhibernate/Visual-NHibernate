using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	[Serializable]
	public class ScriptBaseObject : IScriptBaseObject
	{
		[DotfuscatorDoNotRename]
		protected List<IUserOption> UserOptions = new List<IUserOption>();
		[DotfuscatorDoNotRename]
		protected object ExposedUserOptions;
		[DotfuscatorDoNotRename]
		protected static bool SerializationVersionExists = true;
		[NonSerialized]
		private bool _VirtualPropertiesAreSet = false;
		[NonSerialized]
		private bool _SettingVirtualProperties = false;
		[NonSerialized]
		private readonly object _LockObject = new object();

		public List<IUserOption> Ex
		{
			get
			{
				lock (_LockObject)
				{
					if (_VirtualPropertiesAreSet == false && _SettingVirtualProperties == false)
					{
						_SettingVirtualProperties = true;

						FillVirtualProperties();

						_SettingVirtualProperties = false;
						_VirtualPropertiesAreSet = true;
					}

					return UserOptions;
				}
			}
			set
			{
				Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), UserOptions, value);
				UserOptions = value;
				foreach (var val in value)
				{
					val.Owner = this;
				}
			}
		}

		public void AddUserOption(IUserOption userOption)
		{
			Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), null, UserOptions);
			UserOptions.Add(userOption);
			userOption.Owner = this;
			_VirtualPropertiesAreSet = true;
		}

		public object GetUserOptionValue(string name)
		{
			foreach (IUserOption userOption in Ex)
			{
				if (userOption.Name == name)
				{
					return userOption.Value;
				}
			}
			// Virtual Property not found, so this object was probably created and added during the
			// current session, because Virtual Properties are bulk-populated when the project is 
			// initially opened, in Project.FillVirtualProperties(). So now we will populate this object
			// individually.
			FillVirtualProperties();

			foreach (IUserOption userOption in Ex)
			{
				if (userOption.Name == name)
				{
					return userOption.Value;
				}
			}


			throw new MissingMemberException("UserOption not found: " + name);
		}

		public T GetUserOptionValue<T>(string name)
		{
			object value = GetUserOptionValue(name);
			if (value is T)
				return (T)value;
			return default(T);
		}

		public bool HasUserOption(string name)
		{
			if (Ex.Any(uo => uo.Name == name))
				return true;

			FillVirtualProperties();

			return Ex.Any(uo => uo.Name == name);
		}

		private void FillVirtualProperties()
		{
			if (SharedData.CurrentProject != null)
			{
				SharedData.CurrentProject.PopulateVirtualProperties(this);
			}
		}
	}
}
