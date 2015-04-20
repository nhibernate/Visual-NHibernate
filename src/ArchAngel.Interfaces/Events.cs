using System;
using System.Reflection;

namespace ArchAngel.Interfaces
{
    public class Events
    {
        public delegate void IsDirtyDelegate();
        public static event IsDirtyDelegate IsDirtyEvent;
        public delegate void ShadeMainFormDelegate();
        public static event ShadeMainFormDelegate ShadeMainFormEvent;
        public static event ShadeMainFormDelegate UnShadeMainFormEvent;
        public delegate void SetCursorDelegate(System.Windows.Forms.Cursor cursor);
        public static event SetCursorDelegate SetCursorEvent;
        public delegate void ReportErrorDelegate(Exception ex);
        public static event ReportErrorDelegate ReportErrorEvent;
        public delegate void BusyPopulatingDelegate(bool busyPopulating);
        public static event BusyPopulatingDelegate SetBusyPopulatingEvent;
        public delegate void RefreshApplicationEventDelegate();
        public static event RefreshApplicationEventDelegate RefreshApplicationEvent;
        public static event DataChangedEventDelegate DataChangedEvent;
        public delegate void DataChangedEventDelegate(Type type, MethodInfo method, object oldValue, object newValue);
        public delegate void CancelGenerationDelegate();
        public static event CancelGenerationDelegate CancelGenerationEvent;
		public delegate void ProcessingObjectDelegate(string objectName, string description);
		public static event ProcessingObjectDelegate ObjectBeingProcessed;

        public static void RaiseCancelGenerationEvent()
        {
            if (CancelGenerationEvent != null)
            {
                CancelGenerationEvent();
            }
        }

        public static void RaiseIsDirtyEvent()
        {
            if (IsDirtyEvent != null)
            {
                IsDirtyEvent();
            }
        }

        public static void ShadeMainForm()
        {
            if (ShadeMainFormEvent != null)
            {
                ShadeMainFormEvent();
            }
        }

        public static void UnShadeMainForm()
        {
            if (UnShadeMainFormEvent != null)
            {
                UnShadeMainFormEvent();
            }
        }

        public static void SetCursor(System.Windows.Forms.Cursor cursor)
        {
            if (SetCursorEvent != null)
            {
                SetCursorEvent(cursor);
            }
        }

        public static void ReportError(Exception ex)
        {
            if (ReportErrorEvent != null)
            {
                ReportErrorEvent(ex);
            }
        }

        public static void SetBusyPopulating(bool isBusyPopulating)
        {
            if (SetBusyPopulatingEvent != null)
            {
                SetBusyPopulatingEvent(isBusyPopulating);
            }
        }

        /// <summary>
        /// Call this to raise an event to signify that data has changed.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
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

        /// <summary>
        /// Cause ArchAngel Workbench to Refresh it's display.
        /// </summary>
        public static void RaiseRefreshApplicationEvent()
        {
            // Don't raise event if the data hasn't actually changed
            if (RefreshApplicationEvent != null)
            {
                RefreshApplicationEvent();
            }
        }

		public static void RaiseObjectBeingProcessedEvent(string objectName, string description)
		{
			if (ObjectBeingProcessed != null)
			{
				ObjectBeingProcessed(objectName, description);
			}
		}
    }
}
