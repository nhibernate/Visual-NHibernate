using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Interfaces.Controls.ContentItems
{
    public class Controller
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

        /// <summary>
        /// Saves the page/object(s) state to file eg: xml, binary serialization etc
        /// </summary>
        /// <param name="filename"></param>
        public virtual void Save(string filename)
        {
            throw new InvalidOperationException("This base method should not be called. It is supposed to be overridden.");
        }

        /// <summary>
        /// Reads the page/object(s) state from file eg: xml, binary serialization etc
        /// </summary>
        /// <param name="filename"></param>
        public virtual void Open(string filename)
        {
            throw new InvalidOperationException("This base method should not be called. It is supposed to be overridden.");
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

    }
}
