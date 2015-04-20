using System.Collections.Generic;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
{
	public class Comparers
	{
		public enum SortDirection
		{
			Ascending,
			Descending
		}

		#region Function Comparer
		public class FunctionComparer : IComparer<FunctionInfo>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public FunctionComparer()
			{
			}

			public FunctionComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(FunctionInfo functionX, FunctionInfo functionY)
			{
				if (functionX == null && functionY == null)
				{
					return 0;
				}
				else if (functionX == null && functionY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (functionX != null && functionY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (functionX.Name != functionY.Name)
					{
						// The names are not the same, so no need to check parameters
						return (this._direction == SortDirection.Ascending) ? functionX.Name.CompareTo(functionY.Name) : functionY.Name.CompareTo(functionX.Name);
					}
					else // The names are equal, so need to check parameters as well
					{
						if (functionX.Parameters.Count != functionY.Parameters.Count)
						{
							return (this._direction == SortDirection.Ascending) ? functionX.Parameters.Count.CompareTo(functionY.Parameters.Count) : functionY.Parameters.Count.CompareTo(functionX.Parameters.Count);
						}
						else // Same number of parameters, so need to order by names
						{
							bool mismatchExists = false;

							for (int i = 0; i < functionX.Parameters.Count; i++)
							{
								if (functionX.Parameters[i].Name != functionY.Parameters[i].Name)
								{
									mismatchExists = true;
									return (this._direction == SortDirection.Ascending) ? functionX.Parameters[i].Name.CompareTo(functionY.Parameters[i].Name) : functionY.Parameters[i].Name.CompareTo(functionX.Parameters[i].Name);
								}
							}
							if (mismatchExists)
							{
								// All param names are the same, so need to check data-types
								for (int i = 0; i < functionX.Parameters.Count; i++)
								{
									if (functionX.Parameters[i].DataType != functionY.Parameters[i].DataType)
									{
										mismatchExists = true;
										return (this._direction == SortDirection.Ascending) ? functionX.Parameters[i].DataType.Name.CompareTo(functionY.Parameters[i].DataType.Name) : functionY.Parameters[i].DataType.Name.CompareTo(functionX.Parameters[i].DataType.Name);
									}
								}
							}
							// The functions are the same
							return 0;
						}
					}
				}
			}
		}
		#endregion

		#region UserOption Comparer
		public class UserOptionComparer : IComparer<UserOption>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public UserOptionComparer()
				: base()
			{
			}

			public UserOptionComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(UserOption userOptionX, UserOption userOptionY)
			{
				if (userOptionX == null && userOptionY == null)
				{
					return 0;
				}
				else if (userOptionX == null && userOptionY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (userOptionX != null && userOptionY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					//if (userOptionX.VariableName != userOptionY.VariableName)
					//{
					// The names are not the same, so no need to check parameters
					return (this._direction == SortDirection.Ascending) ? userOptionX.VariableName.CompareTo(userOptionY.VariableName) : userOptionY.VariableName.CompareTo(userOptionX.VariableName);
					//}
					//else // The names are equal, so need to check parameters as well
					//{
					//    throw new InvalidOperationException("UserOptions cannot have duplicate names.");
					//}
				}
			}
		}
		#endregion

	}
}
