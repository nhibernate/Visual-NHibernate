using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Slyce.Common;

namespace ArchAngel.Providers.Database.Model
{
	[Serializable]
	[DotfuscatorDoNotRename]
	public class Association : ISerializable
	{
		[DotfuscatorDoNotRename]
		protected static bool SerializationVersionExists = true;
		[DotfuscatorDoNotRename]
		protected string _AssociationKind = "";
		[DotfuscatorDoNotRename]
		protected ScriptObject _AssociatedObject;
		private readonly string _AssociatedObjectId;
		[DotfuscatorDoNotRename]
		protected ScriptObject _PrimaryObject;
		[DotfuscatorDoNotRename]
		protected List<Mapping> _Mappings = new List<Mapping>();
		[DotfuscatorDoNotRename]
		protected string _Name = "NewAssociation";
		[DotfuscatorDoNotRename]
		protected bool _Enabled;
		[DotfuscatorDoNotRename]
		private string _UniqueId = string.Empty;

		public Association(ScriptObject primaryObject)
		{
			PrimaryObject = primaryObject;
		}

		public Association(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (SerializerHelper.UseFastSerialization)
			{
				using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
				{
					_UniqueId = reader.ReadString();
					_AssociatedObjectId = reader.ReadString();
					_AssociationKind = reader.ReadString();
					_Mappings = (List<Mapping>)reader.ReadObject();
					_Name = reader.ReadString();
					_Enabled = reader.ReadBoolean();
				}
			}
			else
			{
				if (SerializationVersionExists)
				{
					try
					{
					}
					catch (SerializationException)
					{
						// ignore
						SerializationVersionExists = false;
					}
				}
				_AssociatedObject = (ScriptObject)serializationInfo.GetValue("AssociatedObject", ModelTypes.ScriptObject);
				_PrimaryObject = (ScriptObject)serializationInfo.GetValue("PrimaryObject", ModelTypes.ScriptObject);
				_AssociationKind = serializationInfo.GetString("AssociationKind");
				_Mappings = (List<Mapping>)serializationInfo.GetValue("Mappings", typeof(List<Mapping>));
				_Name = serializationInfo.GetString("Name");
				_Enabled = serializationInfo.GetBoolean("Enabled");
			}
		}

		/// <exclude/>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
#if FAST_SERIALIZATION
			using (SerializationWriter writer = new SerializationWriter())
			{
				writer.Write(this.UniqueId);

				foreach (ScriptObject so in _PrimaryObject.Database.AllScriptObjects)
				{
					if (so.Name == _AssociatedObject.Name && so.UniqueId != _AssociatedObject.UniqueId)
					{
						_AssociatedObject.UniqueId = so.UniqueId;
					}
				}
				writer.Write(_AssociatedObject.UniqueId);
				writer.Write(_AssociationKind);
				writer.WriteObject(_Mappings);
				writer.Write(_Name);
				writer.Write(_Enabled);
				info.AddValue("d", writer.ToArray());
			}
#else
            info.AddValue("SerializationVersion", ScriptBase.SerializationVersion);
            info.AddValue("AssociatedObject", _AssociatedObject);
            info.AddValue("PrimaryObject", _PrimaryObject);
            info.AddValue("AssociationKind", _AssociationKind);
            info.AddValue("Mappings", _Mappings);
            info.AddValue("Name", _Name);
            info.AddValue("Enabled", _Enabled);
#endif
		}

		public string UniqueId
		{
			get
			{
				if (_UniqueId.Length == 0)
				{
					_UniqueId = Guid.NewGuid().ToString();
				}
				return _UniqueId;
			}
		}

		public bool Enabled
		{
			get { return _Enabled; }
			set { _Enabled = value; }
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public List<Mapping> Mappings
		{
			get { return _Mappings; }
			set { _Mappings = value; }
		}

		public ScriptObject AssociatedObject
		{
			get
			{
				if (_AssociatedObject == null && _AssociatedObjectId != null)
				{
					_AssociatedObject = (ScriptObject)ScriptBase.Lookups[_AssociatedObjectId];
				}
				return _AssociatedObject;
			}
			set { _AssociatedObject = value; }
		}

		public ScriptObject PrimaryObject
		{
			get { return _PrimaryObject; }
			set { _PrimaryObject = value; }
		}

		public string AssociationKind
		{
			get { return _AssociationKind; }
			set { _AssociationKind = value; }
		}

		public Mapping FindMappingByPrimaryColumn(Column primaryColumn)
		{
			foreach (Mapping mapping in Mappings)
			{
				if (mapping.PrimaryColumn == primaryColumn)
				{
					return mapping;
				}
			}
			return null;
		}

		public Mapping FindMappingByAssociatedColumn(Column associatedColumn)
		{
			foreach (Mapping mapping in Mappings)
			{
				if (mapping.AssociatedColumn == associatedColumn)
				{
					return mapping;
				}
			}
			return null;
		}

		public Mapping FindMappingByAssociatedColumn(string associatedColumnAlias)
		{
			foreach (Mapping mapping in Mappings)
			{
				if (Utility.StringsAreEqual(mapping.AssociatedColumn.Alias, associatedColumnAlias, false))
				{
					return mapping;
				}
			}
			return null;
		}

		public Mapping FindMappingByAssociatedParameter(StoredProcedure.Parameter associatedParameter)
		{
			foreach (Mapping mapping in Mappings)
			{
				if (mapping.AssociatedParameter == associatedParameter)
				{
					return mapping;
				}
			}
			return null;
		}

		public Mapping FindMappingByAssociatedParameter(string associatedParameterAlias)
		{
			foreach (Mapping mapping in Mappings)
			{
				if (Utility.StringsAreEqual(mapping.AssociatedParameter.Alias, associatedParameterAlias, false))
				{
					return mapping;
				}
			}
			return null;
		}

		[Serializable]
		[DotfuscatorDoNotRename]
		public class Mapping : ISerializable
		{
			[DotfuscatorDoNotRename]
			private Column _PrimaryColumn;
			[DotfuscatorDoNotRename]
			private Column _AssociatedColumn;
			private readonly string _AssociatedColumnId;
			[DotfuscatorDoNotRename]
			private StoredProcedure.Parameter _AssociatedParameter;
			private readonly string _AssociatedParameterId;
			[DotfuscatorDoNotRename]
			protected static bool SerializationVersionExists = true;

			public Mapping(Column primaryColumn, Column associatedColumn, StoredProcedure.Parameter associatedParameter)
			{
				PrimaryColumn = primaryColumn;
				AssociatedColumn = associatedColumn;
				AssociatedParameter = associatedParameter;
			}

			public Mapping(SerializationInfo serializationInfo, StreamingContext streamingContext)
			{
				if (SerializerHelper.UseFastSerialization)
				{
					using (SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
					{
						_PrimaryColumn = (Column)ScriptBase.Lookups[reader.ReadString()];
						_AssociatedColumnId = reader.ReadString();
						_AssociatedParameterId = reader.ReadString();
					}
				}
				else
				{
					if (SerializationVersionExists)
					{
						try
						{
						}
						catch (SerializationException)
						{
							// ignore
							SerializationVersionExists = false;
						}
					}
					_PrimaryColumn = (Column)serializationInfo.GetValue("PrimaryColumn", typeof(Column));
					_AssociatedColumn = (Column)serializationInfo.GetValue("AssociatedColumn", typeof(Column));
					_AssociatedParameter = (StoredProcedure.Parameter)serializationInfo.GetValue("AssociatedParameter", typeof(StoredProcedure.Parameter));
				}
			}

			/// <exclude/>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
			public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
			{
#if FAST_SERIALIZATION
				using (SerializationWriter writer = new SerializationWriter())
				{
					writer.Write(PrimaryColumn.UniqueId);

					if (AssociatedColumn == null)
					{
						writer.Write(string.Empty);
					}
					else
					{
						writer.Write(AssociatedColumn.UniqueId);
					}
					if (AssociatedParameter == null)
					{
						writer.Write(string.Empty);
					}
					else
					{
						writer.Write(AssociatedParameter.UniqueId);
					}
					info.AddValue("d", writer.ToArray());
				}
#else
				info.AddValue("SerializationVersion", ScriptBase.SerializationVersion);
				info.AddValue("PrimaryColumn", _PrimaryColumn);
				info.AddValue("AssociatedColumn", _AssociatedColumn);
				info.AddValue("AssociatedParameter", _AssociatedParameter);
#endif
			}

			public StoredProcedure.Parameter AssociatedParameter
			{
				get 
				{
					if (_AssociatedParameter == null && !string.IsNullOrEmpty(_AssociatedParameterId))
					{
						_AssociatedParameter = (StoredProcedure.Parameter)ScriptBase.Lookups[_AssociatedParameterId];
					}
					return _AssociatedParameter; 
				}
				set { _AssociatedParameter = value; }
			}

			public Column AssociatedColumn
			{
				get 
				{
					if (_AssociatedColumn == null && !string.IsNullOrEmpty(_AssociatedColumnId))
					{
						_AssociatedColumn = (Column)ScriptBase.Lookups[_AssociatedColumnId];
					}
					return _AssociatedColumn; 
				}
				set { _AssociatedColumn = value; }
			}

			public Column PrimaryColumn
			{
				get { return _PrimaryColumn; }
				set { _PrimaryColumn = value; }
			}
		}
	}
}
