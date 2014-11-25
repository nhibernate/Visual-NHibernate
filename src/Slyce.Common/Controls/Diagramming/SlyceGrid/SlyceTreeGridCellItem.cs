
namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public class SlyceTreeGridCellItem
	{
		public bool Ignore = false;
		private bool _Enabled = true;

		public SlyceTreeGridCellItem(object value)
		{
			Value = value;
		}

		public SlyceTreeGridCellItem(object value, bool ignore)
		{
			Value = value;
			Ignore = ignore;
		}

		public SlyceTreeGridCellItem(object value, object[] allowedValues)
		{
			Value = value;
			AllowedValues = allowedValues;
			IsNullable = false;
			HasValue = false;
		}

		public SlyceTreeGridCellItem(object value, object[] allowedValues, bool isNullable, bool isNull)
		{
			Value = value;
			AllowedValues = allowedValues;
			IsNullable = isNullable;
			HasValue = isNull;
		}

		public SlyceTreeGridCellItem(object value, bool isNullable, bool isNull)
		{
			Value = value;
			IsNullable = isNullable;
			HasValue = isNull;
		}

		public object Value { get; set; }
		public bool IsNullable { get; set; }
		public bool HasValue { get; set; }
		public object[] AllowedValues { get; set; }

		public bool Enabled
		{
			get { return _Enabled; }
			set { _Enabled = value; }
		}
	}
}
