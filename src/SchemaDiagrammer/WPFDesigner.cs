using System.Windows;

namespace SchemaDiagrammer
{
	public static class WPFDesigner
	{
        /// <summary>
        /// Does not work if the application was started as a WinForms app.
        /// </summary>
        /// <returns></returns>
		public static bool CurrentlyInDesignMode()
		{
			if (!_isInDesignMode.HasValue)
			{
				_isInDesignMode =
					(null == Application.Current) ||
					Application.Current.GetType() == typeof(Application);
			}
			return _isInDesignMode.Value;
		}

		/// <summary>
		/// Stores the computed InDesignMode value.
		/// </summary>
		private static bool? _isInDesignMode;
	}
		
}
