using System;
using System.Collections.Generic;
using System.Text;
//using Slyce.Loader;
using System.Reflection;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
    public class Helper
    {
        public delegate void DataChangedEventDelegate(Type type, MethodInfo method, object oldValue, object newValue);
        public delegate void RefreshApplicationEventDelegate();

        public static event DataChangedEventDelegate DataChangedEvent;
        public static event RefreshApplicationEventDelegate RefreshApplicationEvent;

        /// <summary>
        /// Call this to raise an event to signify that data has changed.
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

        public static void RaiseRefreshApplicationEvent()
        {
            // Don't raise event if the data hasn't actually changed
            if (RefreshApplicationEvent != null)
            {
                RefreshApplicationEvent();
            }
        }




    }
}
