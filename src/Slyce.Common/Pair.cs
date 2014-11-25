
namespace Slyce.Common
{
	public class Pair<T>
	{
		public readonly T Left;
		public readonly T Right;

		public Pair(T left, T right)
		{
			Left = left;
			Right = right;
		}
	}
}
