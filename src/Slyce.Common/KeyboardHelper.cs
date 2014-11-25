using System.Windows.Forms;

namespace Slyce.Common
{
	/// <summary>
	/// Helper methods for determining Keyboard state.
	/// </summary>
	public static class KeyboardHelper
	{
		/// <summary>
		/// Returns true if either of the + keys on the keyboard are down.
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		public static bool IsAddKeyDown(Keys keyData)
		{
			return
				((keyData & Keys.KeyCode) == Keys.Oemplus)
				|| ((keyData & Keys.KeyCode) == Keys.Add);
		}

		/// <summary>
		/// Returns true if either of the - keys on the keyboard are down.
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		public static bool IsMinusKeyDown(Keys keyData)
		{
			return
				((keyData & Keys.KeyCode) == Keys.OemMinus)
				|| ((keyData & Keys.KeyCode) == Keys.Subtract);
		}

		/// <summary>
		/// Returns true if the Tab key on the keyboard is down.
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		public static bool IsTabKeyDown(Keys keyData)
		{
			return ((keyData & Keys.KeyCode) == Keys.Tab);
		}
	}
}
