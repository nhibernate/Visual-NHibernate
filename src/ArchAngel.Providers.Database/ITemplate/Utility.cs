using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ITemplate
{
    public delegate void DataChangedEventDelegate(Type type, MethodInfo method, object oldValue, object newValue);

    public class Utility
    {
        public static event DataChangedEventDelegate DataChangedEvent;

        /// <summary>
        /// Callthis to raise an event to signify that data has changed.
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public static void RaiseDataChangedEvent(Type type, MethodInfo method, object oldValue, object newValue)
        {
            // Don't raise event if the data hasn't actually changed
            if (DataChangedEvent != null && (oldValue != newValue))
            {
                DataChangedEvent(type, method, oldValue, newValue);
            }
        }
    }
}
