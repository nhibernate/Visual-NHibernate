using System;

namespace Slyce.Common
{
	public class GenericEventArgs<T> : EventArgs
	{
		private readonly T obj;

		public GenericEventArgs(T obj)
		{
			this.obj = obj;
		}

		public T Object
		{
			get
			{
				return obj;
			}
		}
	}
}
