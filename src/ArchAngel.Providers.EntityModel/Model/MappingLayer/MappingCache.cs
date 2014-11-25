using System;
using System.Collections.Generic;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	/// <summary>
	/// Use to cache mapping relationships.
	/// </summary>
	/// <typeparam name="T">The single side of the mapping. Each T object will map to only one U object</typeparam>
	/// <typeparam name="U">The multiple side of the mapping. Each U object may map to multiple T objects.</typeparam>
	internal class MappingCache<T, U>
		where T : class, IModelObject
		where U : class, IModelObject
	{
		private readonly Dictionary<Guid, T> tLookup = new Dictionary<Guid, T>();
		private readonly LookupList<Guid, U> uLookup = new LookupList<Guid, U>();

		public void Clear()
		{
			tLookup.Clear();
			uLookup.Clear();
		}

		public void AddMapping(T tObj, U uObj)
		{
			// TODO: remove this code once the error is resolved.
			if (tObj == null)
				throw new Exception("tObj is null.");

			if (uObj == null)
				throw new Exception("uObj is null.");

			if (tLookup == null)
				throw new Exception("tLookup is null.");

			if (uLookup == null)
				throw new Exception("uLookup is null.");

			if (uObj.InternalIdentifier == null)
				throw new Exception("uObj.InternalIdentifier is null.");

			if (tObj.InternalIdentifier == null)
				throw new Exception("tObj.InternalIdentifier is null.");

			tLookup[uObj.InternalIdentifier] = tObj;
			uLookup.Add(tObj.InternalIdentifier, uObj);
		}

		public T GetMappedObject(U uObj)
		{
			T tObj;
			if (tLookup.TryGetValue(uObj.InternalIdentifier, out tObj))
			{
				return tObj;
			}
			return null;
		}

		public IEnumerable<U> GetMappedObjects(T tObj)
		{
			if (uLookup.ContainsKey(tObj.InternalIdentifier))
			{
				return uLookup[tObj.InternalIdentifier];
			}
			return new List<U>();
		}

		public bool ContainsObject(T tObj)
		{
			return tLookup.ContainsKey(tObj.InternalIdentifier);
		}

		public bool ContainsObject(U uObj)
		{
			return uLookup.ContainsKey(uObj.InternalIdentifier);
		}
	}
}
