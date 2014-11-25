using System;
using System.Windows;
using System.Windows.Controls;

namespace SchemaDiagrammer.View
{
    // TODO: Set up and event handler that catches tunneling SelectObject events.

    public class ObjectSelectedEventArgs : RoutedEventArgs
    {
        public ObjectSelectedEventArgs(object selectedObject, object source)
            : base(DiagramSurface.ObjectSelectedEvent, source)
        {
            SelectedObject = selectedObject;
            Source = source;
        }

        public object SelectedObject { get; set; }
    }

    public delegate void ObjectSelectedHandler(object sender, ObjectSelectedEventArgs e);

    public class SelectableObjectPanel : StackPanel
    {
        public static readonly DependencyProperty ObjectProperty = DependencyProperty.Register("Object", typeof (object),
                                                                                   typeof (SelectableObjectPanel));

        public object Object
        {
            get { return GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        public SelectableObjectPanel()
        {
            AddHandler(MouseLeftButtonDownEvent, new RoutedEventHandler(ClickEventHandler));
        }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new ObjectSelectedEventArgs(Object, this));
        }
    }

    public class SelectableObjectListBox : ListBox
    {
        public SelectableObjectListBox()
        {
            AddHandler(SelectionChangedEvent, new RoutedEventHandler(SelectionChangedHandler));
			AddHandler(LostFocusEvent, new RoutedEventHandler(LostFocusEventHandler));
        }

    	private void LostFocusEventHandler(object sender, RoutedEventArgs e)
    	{
    		SelectedValue = null;
    	}

    	private void SelectionChangedHandler(object sender, RoutedEventArgs e)
        {
			if (SelectedValue == null) return;

            var obj = SelectedValue;
            RaiseEvent(new ObjectSelectedEventArgs(obj, this));
        }
    }
}
