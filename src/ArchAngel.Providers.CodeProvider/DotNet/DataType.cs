using System;
using System.Collections.Generic;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class DataType : IEquatable<DataType>, IPrintable
	{

		private string _Name = "";
		private string _Alias = "";
		private List<DataType> _GenericParameters = new List<DataType>();
		private Controller controller;
		//public DataType() { }

		public DataType(Controller controller, string name)
		{
			_Name = name;
			this.controller = controller;
		}

		public bool IsGeneric
		{
			get { return GenericParameters.Count > 0; }
		}

		public List<DataType> GenericParameters
		{
			get { return _GenericParameters; }
			set { _GenericParameters = value; }
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public DataType Clone()
		{
			DataType dt = new DataType(controller, Name);
			dt.Alias = Alias;
			foreach (DataType dat in GenericParameters)
			{
				dt.GenericParameters.Add(dat.Clone());
			}

			return dt;
		}


		public string Alias
		{
			get { return _Alias; }
			set { _Alias = value; }
		}

		public static bool operator ==(DataType d1, DataType d2)
		{
			if (ReferenceEquals(d1, null) && ReferenceEquals(d2, null))
			{
				return true;
			}
			if (ReferenceEquals(d1, null) || ReferenceEquals(d2, null))
			{
				return false;
			}

			if (d1.Alias == d2.Alias &&
				d1.Name == d2.Name &&
				d1.IsGeneric == d2.IsGeneric &&
				d1.GenericParameters.Count == d2.GenericParameters.Count)
			{
				for (int i = 0; i < d1.GenericParameters.Count; i++)
				{
					if (d1.GenericParameters[i] != d2.GenericParameters[i])
					{
						return false;
					}
				}
				return true;
			}

			return false;
		}

		public static bool operator !=(DataType d1, DataType d2)
		{
			return !(d1 == d2);
		}

		public bool Equals(DataType obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!Equals(obj._Name, _Name) || !Equals(obj._Alias, _Alias)) return false;
			if (obj._GenericParameters.Count != _GenericParameters.Count) return false;
			for (int i = 0; i < _GenericParameters.Count; i++)
			{
				if (obj._GenericParameters[i].Equals(_GenericParameters[i]) == false) return false;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(DataType)) return false;
			return Equals((DataType)obj);
		}

		public override int GetHashCode()
		{
			int result = (_Name != null ? _Name.GetHashCode() : 0);
			result = (result * 397) ^ (_Alias != null ? _Alias.GetHashCode() : 0);
			result = (result * 397) ^ (_GenericParameters != null ? _GenericParameters.GetHashCode() : 0);
			return result;
		}

		public override string ToString()
		{
			return controller.GetPrinterFor(this).Print();
		}
	}
}
