using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ArchAngel.Providers.Database.Model
{
    /// <summary>
    /// Summary description for Compare
    /// </summary>
    public class SortComparer<T> : IComparer<T>
    {
        private readonly ListSortDescriptionCollection _listSortDescriptionCollection;
        private readonly PropertyDescriptor _propertyDescriptor;
        private readonly ListSortDirection _listSortDirection;

        public SortComparer(string fieldName, ListSortDirection listSortDirection)
        {
            _propertyDescriptor = TypeDescriptor.GetProperties(typeof(T))[fieldName];
            _listSortDirection = listSortDirection;
        }

        public SortComparer(PropertyDescriptor propertyDescriptor, ListSortDirection listSortDirection)
        {
            _propertyDescriptor = propertyDescriptor;
            _listSortDirection = listSortDirection;
        }

        public SortComparer(string[] fieldNames, ListSortDirection listSortDirection)
        {
            List<ListSortDescription> listSortDescriptions = new List<ListSortDescription>();
            foreach (string fieldName in fieldNames)
            {
                PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(T))[fieldName];
                ListSortDescription listSortDescription = new ListSortDescription(propertyDescriptor, listSortDirection);
                listSortDescriptions.Add(listSortDescription);
            }

            _listSortDescriptionCollection = new ListSortDescriptionCollection(listSortDescriptions.ToArray());

        }

        public SortComparer(ListSortDescriptionCollection listSortDescriptionCollection)
        {
            _listSortDescriptionCollection = listSortDescriptionCollection;
        }

        int IComparer<T>.Compare(T x, T y)
        {
        	if (_propertyDescriptor != null) // Simple sort
            {
                object xValue = _propertyDescriptor.GetValue(x);
                object yValue = _propertyDescriptor.GetValue(y);
                return CompareValues(xValue, yValue, _listSortDirection);
            }
        	if (_listSortDescriptionCollection != null && _listSortDescriptionCollection.Count > 0)
        	{
        		return RecursiveCompareInternal(x, y, 0);
        	}
        	return 0;
        }

    	private static int CompareValues(object xValue, object yValue, ListSortDirection listSortDirection)
        {
            int retValue = 0;

            if (xValue == null && yValue == null)
            {
                return retValue;
            }

            if (xValue is IComparable) // Can ask the x value
            {
                retValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (yValue is IComparable) //Can ask the y value
            {
                retValue = ((IComparable)yValue).CompareTo(xValue);
            }
            else if (!xValue.Equals(yValue))    // not comparable, compare String representations
            {
                retValue = xValue.ToString().CompareTo(yValue.ToString());
            }

            if (listSortDirection == ListSortDirection.Ascending)
            {
                return retValue;
            }
    		return retValue * -1;
        }

        private int RecursiveCompareInternal(T x, T y, int index)
        {
            if (index >= _listSortDescriptionCollection.Count)
            {
                return 0; // termination condition
            }

            ListSortDescription listSortDesc = _listSortDescriptionCollection[index];
            object xValue = listSortDesc.PropertyDescriptor.GetValue(x);
            object yValue = listSortDesc.PropertyDescriptor.GetValue(y);

            int retValue = CompareValues(xValue, yValue, listSortDesc.SortDirection);
            if (retValue == 0)
            {
                return RecursiveCompareInternal(x, y, ++index);
            }
        	return retValue;
        }
    }
}
