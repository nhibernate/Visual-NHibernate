using System;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	//[Serializable]
	[DotfuscatorDoNotRename]
	public class DefaultValueFunction : /*ISerializable,*/ IDefaultValueFunction
	{
		[DotfuscatorDoNotRename]
		private Type _objectType;
		[DotfuscatorDoNotRename]
		private string _propertyName;
		[DotfuscatorDoNotRename]
		private bool _useCustomCode;
		[DotfuscatorDoNotRename]
		private FunctionTypes _functionType;

		public DefaultValueFunction()
		{
		}

		public DefaultValueFunction(Type objectType, string propertyName, bool useCustomCode, FunctionTypes functionType)
		{
			ObjectType = objectType;
			PropertyName = propertyName;
			UseCustomCode = useCustomCode;
			FunctionType = functionType;
		}

		public Type ObjectType
		{
			get { return _objectType; }
			set
			{
				//ArchAngel.Interfaces.Events.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _objectType, value);
				_objectType = value;
			}
		}

		public string PropertyName
		{
			get { return _propertyName; }
			set
			{
				//ArchAngel.Interfaces.Events.RaiseDataChangedEvent(this.GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _dataType, value);
				_propertyName = value;
			}
		}

		public string FunctionName
		{
			get
			{
				switch (FunctionType)
				{
					case FunctionTypes.DefaultValue:
						return string.Format("DefaultValue_{0}_{1}", ObjectType.Name, PropertyName);
					case FunctionTypes.Validate:
						return string.Format("Validate_{0}_{1}", ObjectType.Name, PropertyName);
					case FunctionTypes.HelperOverride:
						string functionName = ObjectType.FullName.Replace("ArchAngel.Providers.Database.", "") + "." + PropertyName;
						return functionName.Replace(".", "_");
					default:
						throw new NotImplementedException("Not coded yet.");
				}
				//return string.Format("DefaultValue_{0}_{1}", ObjectType.Name, PropertyName); 
			}
		}

		public bool UseCustomCode
		{
			get { return _useCustomCode; }
			set { _useCustomCode = value; }
		}

		public FunctionTypes FunctionType
		{
			get { return _functionType; }
			set { _functionType = value; }
		}

	}
}
