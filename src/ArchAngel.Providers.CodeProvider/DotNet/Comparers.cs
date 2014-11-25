using System;
//using System.Collections;
using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public class Comparers
	{
		public enum SortDirection
		{
			Ascending,
			Descending
		}

		#region BaseConstruct Comparer
		public class BaseConstructComparer : IComparer<BaseConstruct>
		{
			private SortDirection _direction = SortDirection.Ascending;
			private static Type type;

			public BaseConstructComparer()
				: base()
			{
			}

			public static void ResetType()
			{
				type = null;
			}

			public BaseConstructComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(BaseConstruct baseConstructX, BaseConstruct baseConstructY)
			{
				//return Compare(baseConstructX, baseConstructY);
				if (type == null)
				{
					type = baseConstructX.GetType();
				}
				switch (type.Name)
				{
					case "Attribute":
						return Compare((Attribute)baseConstructX, (Attribute)baseConstructY);
					case "Class":
						return Compare((Class)baseConstructX, (Class)baseConstructY);
					case "Constant":
						return Compare((Constant)baseConstructX, (Constant)baseConstructY);
					case "Constructor":
						return Compare((Constructor)baseConstructX, (Constructor)baseConstructY);
					case "Event":
						return Compare((Event)baseConstructX, (Event)baseConstructY);
					case "Field":
						return Compare((Field)baseConstructX, (Field)baseConstructY);
					case "Function":
						return Compare((Function)baseConstructX, (Function)baseConstructY);
					case "Namespace":
						return Compare((Namespace)baseConstructX, (Namespace)baseConstructY);
					case "Property":
						return Compare((Property)baseConstructX, (Property)baseConstructY);
					case "Struct":
						return Compare((Struct)baseConstructX, (Struct)baseConstructY);
					default:
						throw new NotImplementedException("Not coded yet");
				}
			}

			private int Compare(Class classX, Class classY)
			{
				if (classX == null && classY == null)
				{
					return 0;
				}
				else if (classX == null && classY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (classX != null && classY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ?
					 classX.Name.CompareTo(classY.Name) :
					 classY.Name.CompareTo(classX.Name);
				}
			}

			private int Compare(Attribute attributeX, Attribute attributeY)
			{
				if (attributeX == null && attributeY == null)
				{
					return 0;
				}
				else if (attributeX == null && attributeY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (attributeX != null && attributeY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? attributeX.Name.CompareTo(attributeY.Name) : attributeY.Name.CompareTo(attributeX.Name);
				}
			}

			private int Compare(Constant constantX, Constant constantY)
			{
				if (constantX == null && constantY == null)
				{
					return 0;
				}
				else if (constantX == null && constantY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (constantX != null && constantY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? constantX.Name.CompareTo(constantY.Name) : constantY.Name.CompareTo(constantX.Name);
				}
			}

			private int Compare(Constructor constructorX, Constructor constructorY)
			{
				if (constructorX == null && constructorY == null)
				{
					return 0;
				}
				else if (constructorX == null && constructorY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (constructorX != null && constructorY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (constructorX.Name != constructorY.Name)
					{
						return (this._direction == SortDirection.Ascending) ? constructorX.Name.CompareTo(constructorY.Name) : constructorY.Name.CompareTo(constructorX.Name);
					}
					else // The names are equal, so need to check parameters as well
					{
						if (constructorX.Parameters.Count != constructorY.Parameters.Count)
						{
							return (this._direction == SortDirection.Ascending) ? constructorX.Parameters.Count.CompareTo(constructorY.Parameters.Count) : constructorY.Parameters.Count.CompareTo(constructorX.Parameters.Count);
						}
						else // Same number of parameters, so need to order by names
						{
							bool mismatchExists = false;

							for (int i = 0; i < constructorX.Parameters.Count; i++)
							{
								if (constructorX.Parameters[i].Name != constructorY.Parameters[i].Name)
								{
									mismatchExists = true;
									return (this._direction == SortDirection.Ascending) ? constructorX.Parameters[i].Name.CompareTo(constructorY.Parameters[i].Name) : constructorY.Parameters[i].Name.CompareTo(constructorX.Parameters[i].Name);
								}
							}
							if (mismatchExists)
							{
								// All param names are the same, so need to check data-types
								for (int i = 0; i < constructorX.Parameters.Count; i++)
								{
									if (constructorX.Parameters[i].DataType != constructorY.Parameters[i].DataType)
									{
										mismatchExists = true;
										return (this._direction == SortDirection.Ascending) ? constructorX.Parameters[i].DataType.CompareTo(constructorY.Parameters[i].DataType) : constructorY.Parameters[i].DataType.CompareTo(constructorX.Parameters[i].DataType);
									}
								}
							}
							// The functions are the same
							return 0;
						}

					}
				}
			}

			private int Compare(Event eventX, Event eventY)
			{
				if (eventX == null && eventY == null)
				{
					return 0;
				}
				else if (eventX == null && eventY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (eventX != null && eventY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? eventX.Name.CompareTo(eventY.Name) : eventY.Name.CompareTo(eventX.Name);
				}
			}

			private int Compare(Field fieldX, Field fieldY)
			{
				if (fieldX == null && fieldY == null)
				{
					return 0;
				}
				else if (fieldX == null && fieldY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (fieldX != null && fieldY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (fieldX.Name != fieldY.Name)
					{
						// The names are not the same, so no need to check parameters
						return (this._direction == SortDirection.Ascending) ? fieldX.Name.CompareTo(fieldY.Name) : fieldY.Name.CompareTo(fieldX.Name);
					}
					else // The names are equal, so need to check data-type as well
					{
						return (this._direction == SortDirection.Ascending) ? fieldX.DataType.Name.CompareTo(fieldY.DataType.Name) : fieldY.DataType.Name.CompareTo(fieldX.DataType.Name);
					}
				}
			}

			private int Compare(Function functionX, Function functionY)
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
										return (this._direction == SortDirection.Ascending) ? functionX.Parameters[i].DataType.CompareTo(functionY.Parameters[i].DataType) : functionY.Parameters[i].DataType.CompareTo(functionX.Parameters[i].DataType);
									}
								}
							}
							// The functions are the same
							return 0;
						}
					}
				}
			}

			private int Compare(Namespace namespaceX, Namespace namespaceY)
			{
				if (namespaceX == null && namespaceY == null)
				{
					return 0;
				}
				else if (namespaceX == null && namespaceY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (namespaceX != null && namespaceY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? namespaceX.Name.CompareTo(namespaceY.Name) : namespaceY.Name.CompareTo(namespaceX.Name);
				}
			}

			private int Compare(Property propertyX, Property propertyY)
			{
				if (propertyX == null && propertyY == null)
				{
					return 0;
				}
				else if (propertyX == null && propertyY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (propertyX != null && propertyY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (propertyX.Name != propertyY.Name)
					{
						// The names are not the same, so no need to check parameters
						return (this._direction == SortDirection.Ascending) ? propertyX.Name.CompareTo(propertyY.Name) : propertyY.Name.CompareTo(propertyX.Name);
					}
					else // The names are equal, so need to check data-type as well
					{
						return (this._direction == SortDirection.Ascending) ? propertyX.DataType.Name.CompareTo(propertyY.DataType.Name) : propertyY.DataType.Name.CompareTo(propertyX.DataType.Name);
					}
				}
			}

			private int Compare(Struct structX, Struct structY)
			{
				if (structX == null && structY == null)
				{
					return 0;
				}
				else if (structX == null && structY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (structX != null && structY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ?
					 structX.Name.CompareTo(structY.Name) :
					 structY.Name.CompareTo(structX.Name);
				}
			}

		}
		#endregion

		#region Attribute Comparer
		public class AttributeComparer : IComparer<Attribute>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public AttributeComparer()
				: base()
			{
			}

			public AttributeComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Attribute attributeX, Attribute attributeY)
			{
				if (attributeX == null && attributeY == null)
				{
					return 0;
				}
				else if (attributeX == null && attributeY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (attributeX != null && attributeY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? attributeX.Name.CompareTo(attributeY.Name) : attributeY.Name.CompareTo(attributeX.Name);
				}
			}
		}
		#endregion

		#region Constant Comparer
		public class ConstantComparer : IComparer<Constant>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public ConstantComparer()
				: base()
			{
			}

			public ConstantComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Constant constantX, Constant constantY)
			{
				if (constantX == null && constantY == null)
				{
					return 0;
				}
				else if (constantX == null && constantY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (constantX != null && constantY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? constantX.Name.CompareTo(constantY.Name) : constantY.Name.CompareTo(constantX.Name);
				}
			}
		}
		#endregion

		#region Class Comparer
		public class ClassComparer : IComparer<Class>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public ClassComparer()
				: base()
			{
			}

			public ClassComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Class classX, Class classY)
			{
				if (classX == null && classY == null)
				{
					return 0;
				}
				else if (classX == null && classY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (classX != null && classY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ?
					 classX.Name.CompareTo(classY.Name) :
					 classY.Name.CompareTo(classX.Name);
				}
			}
		}
		#endregion

		#region Struct Comparer
		public class StructComparer : IComparer<Struct>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public StructComparer()
				: base()
			{
			}

			public StructComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Struct structX, Struct structY)
			{
				if (structX == null && structY == null)
				{
					return 0;
				}
				else if (structX == null && structY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (structX != null && structY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ?
					 structX.Name.CompareTo(structY.Name) :
					 structY.Name.CompareTo(structX.Name);
				}
			}
		}
		#endregion

		#region Function Comparer
		public class FunctionComparer : IComparer<Function>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public FunctionComparer()
				: base()
			{
			}

			public FunctionComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Function functionX, Function functionY)
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
										return (this._direction == SortDirection.Ascending) ? functionX.Parameters[i].DataType.CompareTo(functionY.Parameters[i].DataType) : functionY.Parameters[i].DataType.CompareTo(functionX.Parameters[i].DataType);
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

		#region Property Comparer
		public class PropertyComparer : IComparer<Property>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public PropertyComparer()
				: base()
			{
			}

			public PropertyComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Property propertyX, Property propertyY)
			{
				if (propertyX == null && propertyY == null)
				{
					return 0;
				}
				else if (propertyX == null && propertyY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (propertyX != null && propertyY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (propertyX.Name != propertyY.Name)
					{
						// The names are not the same, so no need to check parameters
						return (this._direction == SortDirection.Ascending) ? propertyX.Name.CompareTo(propertyY.Name) : propertyY.Name.CompareTo(propertyX.Name);
					}
					else // The names are equal, so need to check data-type as well
					{
						return (this._direction == SortDirection.Ascending) ? propertyX.DataType.Name.CompareTo(propertyY.DataType.Name) : propertyY.DataType.Name.CompareTo(propertyX.DataType.Name);
					}
				}
			}
		}
		#endregion

		#region Field Comparer
		public class FieldComparer : IComparer<Field>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public FieldComparer()
				: base()
			{
			}

			public FieldComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Field fieldX, Field fieldY)
			{
				if (fieldX == null && fieldY == null)
				{
					return 0;
				}
				else if (fieldX == null && fieldY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (fieldX != null && fieldY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (fieldX.Name != fieldY.Name)
					{
						// The names are not the same, so no need to check parameters
						return (this._direction == SortDirection.Ascending) ? fieldX.Name.CompareTo(fieldY.Name) : fieldY.Name.CompareTo(fieldX.Name);
					}
					else // The names are equal, so need to check data-type as well
					{
						return (this._direction == SortDirection.Ascending) ? fieldX.DataType.Name.CompareTo(fieldY.DataType.Name) : fieldY.DataType.Name.CompareTo(fieldX.DataType.Name);
					}
				}
			}
		}
		#endregion

		#region Namespace Comparer
		public class NamespaceComparer : IComparer<Namespace>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public NamespaceComparer()
				: base()
			{
			}

			public NamespaceComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Namespace namespaceX, Namespace namespaceY)
			{
				if (namespaceX == null && namespaceY == null)
				{
					return 0;
				}
				else if (namespaceX == null && namespaceY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (namespaceX != null && namespaceY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? namespaceX.Name.CompareTo(namespaceY.Name) : namespaceY.Name.CompareTo(namespaceX.Name);
				}
			}
		}
		#endregion

		#region Event Comparer
		public class EventComparer : IComparer<Event>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public EventComparer()
				: base()
			{
			}

			public EventComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Event eventX, Event eventY)
			{
				if (eventX == null && eventY == null)
				{
					return 0;
				}
				else if (eventX == null && eventY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (eventX != null && eventY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ? eventX.Name.CompareTo(eventY.Name) : eventY.Name.CompareTo(eventX.Name);
				}
			}
		}
		#endregion

		#region Constructor Comparer
		public class ConstructorComparer : IComparer<Constructor>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public ConstructorComparer()
				: base()
			{
			}

			public ConstructorComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(Constructor constructorX, Constructor constructorY)
			{
				if (constructorX == null && constructorY == null)
				{
					return 0;
				}
				else if (constructorX == null && constructorY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (constructorX != null && constructorY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					if (constructorX.Name != constructorY.Name)
					{
						return (this._direction == SortDirection.Ascending) ? constructorX.Name.CompareTo(constructorY.Name) : constructorY.Name.CompareTo(constructorX.Name);
					}
					else // The names are equal, so need to check parameters as well
					{
						if (constructorX.Parameters.Count != constructorY.Parameters.Count)
						{
							return (this._direction == SortDirection.Ascending) ? constructorX.Parameters.Count.CompareTo(constructorY.Parameters.Count) : constructorY.Parameters.Count.CompareTo(constructorX.Parameters.Count);
						}
						else // Same number of parameters, so need to order by names
						{
							bool mismatchExists = false;

							for (int i = 0; i < constructorX.Parameters.Count; i++)
							{
								if (constructorX.Parameters[i].Name != constructorY.Parameters[i].Name)
								{
									mismatchExists = true;
									return (this._direction == SortDirection.Ascending) ? constructorX.Parameters[i].Name.CompareTo(constructorY.Parameters[i].Name) : constructorY.Parameters[i].Name.CompareTo(constructorX.Parameters[i].Name);
								}
							}
							if (mismatchExists)
							{
								// All param names are the same, so need to check data-types
								for (int i = 0; i < constructorX.Parameters.Count; i++)
								{
									if (constructorX.Parameters[i].DataType != constructorY.Parameters[i].DataType)
									{
										mismatchExists = true;
										return (this._direction == SortDirection.Ascending) ? constructorX.Parameters[i].DataType.CompareTo(constructorY.Parameters[i].DataType) : constructorY.Parameters[i].DataType.CompareTo(constructorX.Parameters[i].DataType);
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

		#region UsingStatement Comparer
		public class UsingStatementComparer : IComparer<UsingStatement>
		{
			private SortDirection _direction = SortDirection.Ascending;

			public UsingStatementComparer()
				: base()
			{
			}

			public UsingStatementComparer(SortDirection sortDirection)
			{
				_direction = sortDirection;
			}

			public int Compare(UsingStatement usingX, UsingStatement usingY)
			{
				if (usingX == null && usingY == null)
				{
					return 0;
				}
				else if (usingX == null && usingY != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (usingX != null && usingY == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					return (this._direction == SortDirection.Ascending) ?
					 usingX.Value.CompareTo(usingY.Value) :
					 usingY.Value.CompareTo(usingX.Value);
				}
			}
		}
		#endregion


	}
}
