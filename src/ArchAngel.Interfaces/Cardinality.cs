
namespace ArchAngel.Interfaces
{
	public class Cardinality
	{
		public Cardinality(int start, int end)
		{
			Start = start;
			End = end;
		}

		public Cardinality(int count)
		{
			Start = End = count;
		}

		public int Start
		{
			get;
			private set;
		}

		public int End
		{
			get;
			private set;
		}

		public static Cardinality One
		{
			get { return new Cardinality(1); }
		}

		public static Cardinality Many
		{
			get { return new Cardinality(int.MaxValue); }
		}

		public static Cardinality Zero
		{
			get { return new Cardinality(0); }
		}

		public override string ToString()
		{
			if (Start >= End)
			{
				return Start.ToString();
			}

			var startString = Start == int.MaxValue ? "*" : Start.ToString();
			var endString = End == int.MaxValue ? "*" : End.ToString();

			return startString + " ... " + endString;
		}

		public static bool operator ==(Cardinality obj1, Cardinality obj2)
		{
			return Equals(obj1, obj2);
		}

		public static bool operator !=(Cardinality obj1, Cardinality obj2)
		{
			return !(obj1 == obj2);
		}

		public bool Equals(Cardinality obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.Start == Start && obj.End == End;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Cardinality)) return false;
			return Equals((Cardinality)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Start * 397) ^ End;
			}
		}

		public static bool IsOneToMany(Cardinality cardinality1, Cardinality cardinality2)
		{
			if (cardinality1 == One && cardinality2 == Many)
				return true;
			if (cardinality2 == One && cardinality1 == Many)
				return true;

			return false;
		}

		public static bool IsOneToOne(Cardinality cardinality1, Cardinality cardinality2)
		{
			if (cardinality1 == One && cardinality2 == One)
				return true;

			return false;
		}

		public static bool IsManyToMany(Cardinality cardinality1, Cardinality cardinality2)
		{
			if (cardinality1 == Many && cardinality2 == Many)
				return true;

			return false;
		}
	}
}