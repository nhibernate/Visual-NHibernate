using System;
using System.IO;
using System.Collections.Generic;
using ArchAngel.Providers.Database.Model;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ArchAngel.Providers.Database
{
    [DotfuscatorDoNotRename]
    public class SerializerHelper
    {
        [DotfuscatorDoNotRename]
        public static bool UseOldWay = false;
        public static bool UseFastSerialization = false;
        /* File Version Change History
         * ===========================
         * Version 1: Baseline
         * Version 2: ScriptBase.UserOptions changed from List<Slyce.ITemplate.IUserOption> to List<Model.UserOption>
         * Version 3: ScriptObject.Schema now saved to/from serialized data
         * */
        [DotfuscatorDoNotRename]
        public static int FileVersionLatest = 3;
        [DotfuscatorDoNotRename]
        public static int FileVersionCurrent;
		internal static List<Model.Database> CachedDatabases;

        [Serializable]
        [DotfuscatorDoNotRename]
        public class Wrapper : ISerializable
        {
            [DotfuscatorDoNotRename]
            private int _Version;
            [DotfuscatorDoNotRename]
            public List<Model.Database> Databases;

            public Wrapper()
            {
            }

            public Wrapper(int version, List<Model.Database> databases)
            {
                Version = version;
                Databases = databases;
            }

            [DotfuscatorDoNotRename]
            public Wrapper(SerializationInfo serializationInfo, StreamingContext streamingContext)
            {
                //try
                //{
                //    UseFastSerialization = true;
                //    DateTime start = DateTime.Now;

                //    using (Slyce.Common.SerializationReader reader = new SerializationReader((byte[])serializationInfo.GetValue("d", typeof(byte[]))))
                //    {
                //        //byte[] oo = (byte[])serializationInfo.GetValue("d", typeof(byte[]));
                //        _Version = reader.ReadInt32();
                //        UseFastSerialization = true;
                //        Databases = (List<Model.Database>)reader.ReadObject();
                //        // TODO: Deserialize child objects
                //        DateTime end = DateTime.Now;
                //        TimeSpan ts = end - start;
                //        string dd = string.Format("{0},{1}", ts.Seconds, ts.Milliseconds);
                //        dd = "";
                //        return;
                //    }
                //}
                //catch (SerializationException ex)
                //{
                    // Do nothing
					UseFastSerialization = false;
                //}
            	System.Collections.IEnumerator enumerator = serializationInfo.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    SerializationEntry entry = (SerializationEntry)enumerator.Current;

                    switch (entry.ObjectType.Name)
                    {
                        case "Int32":
                            _Version = (int)entry.Value;
                            break;
                        case "List`1":
                            Databases = (List<Model.Database>)entry.Value;
                            break;
                        default:
							throw new NotImplementedException("Not coded yet: " + entry.ObjectType.Name);
                    }
                }
                FileVersionCurrent = Version;
            }

            public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
            {
#if FAST_SERIALIZATION
                using (SerializationWriter writer = new SerializationWriter())
                {
                    writer.Write(_Version);
                    writer.WriteObject(Databases);
                    info.AddValue("d", writer.ToArray());
                }
#else
				info.AddValue("Version", _Version);
                info.AddValue("Databases", Databases);
#endif
			}


            public int Version
            {
                get
                {
                    return _Version;
                }
                set
                {
                    _Version = value;
                    FileVersionCurrent = _Version;
                }
            }
        }

        [DotfuscatorDoNotRename]
        internal static void Serialize(string filename, List<Model.Database> databases)
        {
            Wrapper wrapper = new Wrapper(FileVersionLatest, databases);
            BinaryFormatter formatter = new BinaryFormatter();

        	using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, wrapper);
                stream.Close();
            }
        }

        [DotfuscatorDoNotRename]
        internal static List<Model.Database> Deserialize(string filename)
        {
            Wrapper wrapper;
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new BLL.Version2SerializationBinder();
			ScriptBase.Lookups.Clear();
        	
			if(CachedDatabases != null)
				CachedDatabases.Clear();
        	else
				CachedDatabases = new List<Model.Database>();
			
			// This makes sure we get rid of the old database model.
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                wrapper = (Wrapper)formatter.Deserialize(stream); // Expensive on large databases.
                stream.Close();
            }
			for (int outerCounter = 0; outerCounter < CachedDatabases.Count; outerCounter++)
			{
				for (int innerCounter = CachedDatabases.Count - 1; innerCounter > outerCounter; innerCounter--)
				{
					if (CachedDatabases[outerCounter].ConnectionString.IsTheSame(CachedDatabases[innerCounter].ConnectionString))
					{
						CachedDatabases.RemoveAt(innerCounter);
					}
				}
			}
            RunPostDeserializationWireups(wrapper);
            return wrapper.Databases;
        }

        private static void RunPostDeserializationWireups(Wrapper wrapper)
        {
            foreach (Model.Database database in wrapper.Databases)
            {
                foreach (Lookup lookup in database.Lookups)
                {
                    lookup.Database = database;
                }
            }
        }

    }
}
