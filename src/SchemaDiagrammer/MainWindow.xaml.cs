using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using log4net;
using SchemaDiagrammer.Controller;
using SchemaDiagrammer.Layout;
using SchemaDiagrammer.Model;
using SchemaDiagrammer.View;
using SchemaDiagrammer.View.Draggers;
using SchemaDiagrammer.View.Shapes;
using Slyce.Common.EventExtensions;
using Point=System.Windows.Point;

namespace SchemaDiagrammer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private DraggerBase _Dragger;

		private static readonly ILog log = LogManager.GetLogger(typeof (MainWindow));
		private const double DefaultZoomFactor = 1.4;

		private Point ScreenStartPoint;
		private Point startOffset;

		private readonly TranslateTransform pan = new TranslateTransform();
		private readonly ScaleTransform zoom = new ScaleTransform(1, 1);
		private readonly TransformGroup transformGroup = new TransformGroup();

		public static readonly DependencyProperty MaximumZoomProperty = DependencyProperty.Register("MaximumZoom", typeof(double), typeof(MainWindow), new FrameworkPropertyMetadata(2d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty MinimumZoomProperty = DependencyProperty.Register("MinimumZoom", typeof(double), typeof(MainWindow), new FrameworkPropertyMetadata(0.2d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty DiagramBackgroundBrushProperty = DependencyProperty.Register("DiagramBackgroundBrush", typeof(SolidColorBrush), typeof(MainWindow), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.AffectsRender));
		private bool ignoreScaleChange;

		public event EventHandler LayoutFinished;

		public MainWindow()
		{
			InitializeComponent();
			ApplyTemplate();
			Surface.Window = this;

			transformGroup.Children.Add(zoom);
			transformGroup.Children.Add(pan);
			Surface.RenderTransform = transformGroup;

			MouseDown += Surface_MouseDown;
			MouseUp += MainWindow_MouseUp;
			MouseMove += MainWindow_MouseMove;
			MouseWheel += MainWindow_MouseWheel;

			Surface.OnShapeAdded += Surface_OnShapeAdded;
			Surface.OnShapeRemoved += Surface_OnShapeRemoved;
			Surface.OnConnectionAdded += Surface_OnConnectionAdded;
			Surface.OnConnectionRemoved += Surface_OnConnectionRemoved;

			Surface.OnConnectionSelected += Surface_OnConnectionSelected;
			Surface.OnEntitySelected += Surface_OnEntitySelected;
			Surface.OnConnectionDeselected += Surface_OnConnectionDeselected;
			Surface.OnEntityDeselected += Surface_OnEntityDeselected;

			//var visualBrush = new VisualBrush();
			//visualBrush.Visual = Surface;
			//Map.Fill = visualBrush;
			//Map.Height = MapHeight;

			//var multibinding = new MultiBinding();
			//multibinding.Bindings.Add(new Binding() { Source = Surface, Path = new PropertyPath(ActualWidthProperty) });
			//multibinding.Bindings.Add(new Binding() { Source = Surface, Path = new PropertyPath(ActualHeightProperty) });
			//multibinding.Converter = new AspectRatioConverter();

			//Map.SetBinding(WidthProperty, multibinding);
		}

		/*
		private class AspectRatioConverter : IMultiValueConverter
		{
			public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
			{
				var actualWidth = (double) values[0]; var actualHeight = (double)values[1];

				var aspectRatio = actualWidth / actualHeight;
				return MapHeight * aspectRatio;
			}

			public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) { throw new System.NotImplementedException(); }
		}
		*/
		public void SetResourceDictionary(Stream stream)
		{
			Resources = (ResourceDictionary)XamlReader.Load(stream);
		}

		public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
			"Zoom", typeof (double), typeof (MainWindow), new PropertyMetadata(1.0));

		public double Zoom
		{
			get
			{
				return (double) GetValue(ZoomProperty);
			}
			set
			{
				SetValue(ZoomProperty, value);
                SetZoom(value);
			}
		}

		public double MaximumZoom
		{
			get { return (double)GetValue(MaximumZoomProperty); }
			set { SetValue(MaximumZoomProperty, value); }
		}

		public double MinimumZoom
		{
			get { return (double)GetValue(MinimumZoomProperty); }
			set { SetValue(MinimumZoomProperty, value); }
		}

		public SolidColorBrush DiagramBackgroundBrush
		{
			get { return (SolidColorBrush)GetValue(DiagramBackgroundBrushProperty); }
			set { SetValue(DiagramBackgroundBrushProperty, value); }
		}

		public Color DiagramBackgroundColour
		{
			get { return DiagramBackgroundBrush.Color; }
			set { DiagramBackgroundBrush = new SolidColorBrush(value); }
		}

		public IController Controller
		{
			get { return Surface.Controller; }
		}

		public void ZoomIntoPointBy(Point point, double steps)
		{
			if (steps == 0d) return;

			ZoomIntoPointAtZoomLevel(point, zoom.ScaleX + steps);
		}

		public void ZoomIntoPointAtZoomLevel(Point point, double level)
		{
            if (level > MaximumZoom)
                level = MaximumZoom;
            if (level < MinimumZoom)
                level = MinimumZoom;
			
			SetPan(point, level);
			Zoom = level;

			InvalidateArrange();
		}

		public void CenterOn(Point location)
		{
			pan.X = Surface.ActualWidth / 2 - location.X;
			pan.Y = Surface.ActualHeight / 2 - location.Y;
		}

		public void Layout()
		{
			if (Controller == null || Controller.GetVisibleShapes().Any() == false)
            {
                // No visible shapes.
                LayoutFinished.RaiseEvent(this);
                return; 
            }

			Cursor = Cursors.Wait;

			// The UpdateLayout() call is important, as it causes the sizes of the
			// shapes to be calculated. These sizes are needed for the layout algorithm.
			UpdateLayout();

			InvalidateVisual();

			var layout = new DiagramLayout();
			layout.CalculateLayout(Controller.GetVisibleShapes(), Controller.GetVisibleConnections(), ActualWidth, ActualHeight);
			layout.LayoutGraph();
			Surface.TidyConnections();

		    CalculateZoom();

		    Cursor = Cursors.Arrow;
			LayoutFinished.RaiseEvent(this);
		}

	    private void CalculateZoom()
	    {
			var shapes = Controller.GetVisibleShapes().ToList();
			if(shapes.Count == 0)
			{
				ResetPanAndZoom();
				return;
			}
	    	if(shapes.Count == 1)
			{
				var firstShape = shapes.First();
				CenterOn(firstShape.Location);
				SetZoom(1);
	    		return;
	    	}

	    	double leftMostPoint = Double.MaxValue;
	        double topMostPoint = Double.MaxValue;
	        double rightMostPoint = Double.MinValue;
	        double bottomMostPoint = Double.MinValue;

	    	foreach(var shape in shapes)
	        {
	            leftMostPoint = Math.Min(shape.Location.X, leftMostPoint);
	            topMostPoint = Math.Min(shape.Location.Y, topMostPoint);
	            rightMostPoint = Math.Max(shape.Location.X + shape.ActualWidth, rightMostPoint);
	            bottomMostPoint = Math.Max(shape.Location.Y + shape.ActualHeight, bottomMostPoint);
	        }

	        double requiredWidth = rightMostPoint - leftMostPoint;
	        double requiredHeight = bottomMostPoint - topMostPoint;

	    	double moveLeftBy = (ActualWidth / 2) - (leftMostPoint + (requiredWidth / 2));
			double moveUpBy = (ActualHeight / 2) - (bottomMostPoint - (requiredHeight / 2));

			// The -20 is to correct for a small variation in the sizes that I can't figure out.
			Vector moveBy = new Vector(moveLeftBy, moveUpBy-20);

			foreach(var shape in shapes)
			{
				shape.Location += moveBy;
			}

	        double requiredExtraZoom = 1 / Math.Max(requiredWidth / ActualWidth, requiredHeight / ActualHeight);
            // gives us extra padding around the edges
            requiredExtraZoom -= 0.1;
            ZoomIntoPointAtZoomLevel(Surface.CenterLocation, requiredExtraZoom);
            
	    }

	    #region Event Handlers

/*
		private void LayoutAlgorithms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			Layout();
		}
*/

		private void MainWindow_MouseMove(object sender, MouseEventArgs e)
		{
            if (IsMouseCaptured)
            {
                var physicalPoint = e.GetPosition(this);
				
				pan.X = physicalPoint.X - ScreenStartPoint.X + startOffset.X;
				pan.Y = physicalPoint.Y - ScreenStartPoint.Y + startOffset.Y;

				InvalidateArrange();
            }
		}

		private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseCaptured)
			{
				// we're done.  reset the cursor and release the mouse pointer
				Cursor = Cursors.Arrow;
				ReleaseMouseCapture();
			}
		}

		private void Surface_OnShapeRemoved(object sender, SchemaEventArgs<DiagramShape> e)
		{
			e.Entity.MouseDown -= SimpleShape_MouseDown;
            e.Entity.MouseDoubleClick -= SimpleShape_MouseDoubleClick;
            e.Entity.SizeChanged -= SimpleShape_SizeChanged;
		}

		private void Surface_OnShapeAdded(object sender, SchemaEventArgs<DiagramShape> e)
		{
			DiagramShape shape = e.Entity;

			shape.MouseDown += SimpleShape_MouseDown;
			shape.MouseDoubleClick += SimpleShape_MouseDoubleClick;
            shape.SizeChanged += SimpleShape_SizeChanged;

			foreach(ConnectionPoint point in shape.ConnectionPoints)
				point.MouseDown += ConnectionPoint_MouseDown;
		}

        void SimpleShape_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var shape = sender as DiagramShape;
            if(shape == null) return;

            if(shape.Visible && shape.ActualWidth != double.NaN && shape.ActualHeight != double.NaN)
            {
                LayoutIfSmall();
            }
        }

	    private void LayoutIfSmall()
	    {
	        int count = Controller.GetVisibleShapes().Count();
	        if(count > 0 && count < 6)
	        {
	            Layout();
	        }
	    }

	    private void Surface_OnConnectionAdded(object sender, SchemaEventArgs<Connection> e)
		{
			e.Entity.MouseDown += Connection_MouseDown;
			e.Entity.SourceEndPoint.MouseDown += EndPoint_MouseDown;
			e.Entity.TargetEndPoint.MouseDown += EndPoint_MouseDown;
		}

		private void Surface_OnConnectionRemoved(object sender, SchemaEventArgs<Connection> e)
		{
			e.Entity.MouseDown -= Connection_MouseDown;
			e.Entity.SourceEndPoint.MouseDown -= EndPoint_MouseDown;
			e.Entity.TargetEndPoint.MouseDown -= EndPoint_MouseDown;
		}

		void EndPoint_MouseDown(object sender, MouseButtonEventArgs e)
		{
			return;

			// Connection dragging is not implemented at a SchemaController level yet.

			//var startPosition = e.GetPosition(Surface);
			//var endPoint = e.Source as ConnectorEndPoint;
			//if (endPoint == null) return;

			//var isStartOfConnection = false;
			//if (endPoint.Connection.SourceEndPoint == endPoint)
			//    isStartOfConnection = true;

			//_Dragger = new ConnectionDragger(Surface, this, startPosition, endPoint.Connection, isStartOfConnection);
			//Surface.SelectedConnection = endPoint.Connection;
			//e.Handled = true;
		}

        private void ObjectSelected(object sender, ObjectSelectedEventArgs e)
        {
            Controller.ObjectSelected(e.SelectedObject);
        }

		void Surface_OnEntitySelected(object sender, SchemaEventArgs<DiagramShape> e)
		{
			Controller.ShapeSelected(e.Entity);
		}

		void Surface_OnEntityDeselected(object sender, SchemaEventArgs<DiagramShape> e)
		{
			Controller.ShapeDeselected(e.Entity);
		}

		void Surface_OnConnectionDeselected(object sender, SchemaEventArgs<Connection> e)
		{
			Controller.ConnectionDeselected(e.Entity);
		}

		void Surface_OnConnectionSelected(object sender, SchemaEventArgs<Connection> e)
		{
			Controller.ConnectionSelected(e.Entity);
		}

		private void SimpleShape_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var shape = sender as DiagramShape;
			if (shape == null) return;
		    shape.IsSelected = true;
            Controller.ShapeSetAsPrimary(shape);

		    e.Handled = true;
		}

		private void Connection_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Released)
				return;
			Surface.SelectedConnection = sender as Connection;
		}

		void ConnectionPoint_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//Focus();
			//if(e.ButtonState == MouseButtonState.Released)
			//    return;

			//if (ConnectionToolButton.IsChecked == false)
			//    return;

			//var connectionPoint = sender as ConnectionPoint;

			//if(connectionPoint == null)
			//    return;

			//// Create a new Connection Dragger, with the start connection point set to this one.
			//_Dragger = new NewConnectionDragger(Surface, this, e.GetPosition(Surface), connectionPoint);

			//e.Handled = true;
		}

		private void SimpleShape_MouseDown(object sender, MouseButtonEventArgs e)
		{
			log.Debug("DiagramShape MouseDown");
			log.DebugFormat("Shape has {0} ConnectionPoints", ((DiagramShape)e.Source).ConnectionPoints.Count());

			if (e.ButtonState == MouseButtonState.Released)
				return;

			if (e.ChangedButton == MouseButton.Left) // Drag and Drop
			{
                var simpleShape = e.Source as DiagramShape;
                Point startPoint = e.GetPosition(Surface);

                _Dragger = new SimpleShapeDragger(Surface, this, startPoint, simpleShape);
                Surface.SelectedShape = simpleShape;
                e.Handled = true;
			}
			// Select shape and create a new connection
			else if (e.ChangedButton == MouseButton.Middle)
			{
				if (Surface.SelectedShape == null)
					Surface.SelectedShape = sender as DiagramShape;
				else
				{
					var targetShape = (DiagramShape)sender;
					if (targetShape == Surface.SelectedShape)
					{
						Surface.SelectedShape = null;
						return;
					}

					// Not allowing creation of connections in the diagrammer yet.
					//if(Surface.Controller.CanConnect(Surface.SelectedShape, targetShape) == false)
					//{
					//    Surface.AddConnection(Surface.SelectedShape, targetShape);
					//    Surface.SelectedShape = null;	
					//}
				}
				e.Handled = true;
			}
			
		}

		private void Surface_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Released)
				return;

			log.Debug("Surface MouseDown");

			if (e.ChangedButton == MouseButton.Right)
			{
				//Surface.AddShape("New Shape", e.GetPosition(Surface));
				e.Handled = true;
			}
			else if (e.ChangedButton == MouseButton.Left)
			{
				if(_Dragger != null)
					return;
				
				if(e.Source is Connection)
				{
					Surface.SelectedConnection = e.Source as Connection;
					e.Handled = true;
				}
				else if(e.Source == this) // Is the user clicking on the canvas itself.
				{
					ScreenStartPoint = e.GetPosition(this);
					startOffset = new Point(pan.X, pan.Y);
					CaptureMouse();
					Cursor = Cursors.ScrollAll;
					e.Handled = true;	
				}
			}
		}

		void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (IsMouseCaptured)
				return; // Can't zoom while panning.

			var physicalPosition = e.GetPosition(this);

			// zoom into the content.  Calculate the zoom factor based on the direction of the mouse wheel.
			MouseZoom(physicalPosition, e.Delta, DefaultZoomFactor);
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (Surface == null) return;
			if (ignoreScaleChange) return;

			ZoomIntoPointBy(new Point(Surface.ActualWidth/2, Surface.ActualHeight/2), e.NewValue-e.OldValue);
		}

		internal void Reorder_Click(object sender, RoutedEventArgs e)
		{
			Layout();
		}

		private void ClearScreen_Click(object sender, RoutedEventArgs e)
		{
			Surface.Controller.SetAllToVisible(SchemaDiagrammer.Controller.Visibility.Hidden);
		}

		#endregion

		private void MouseZoom(Point physicalPosition, int mouseDelta, double zoomFactor)
		{
			if (mouseDelta <= 0) zoomFactor = 1.0 / DefaultZoomFactor;

			double currentZoom = zoom.ScaleX * zoomFactor;

			ZoomIntoPointAtZoomLevel(physicalPosition, currentZoom);
		}

		private void SetPan(Point point, double currentZoom)
		{
			var transformedPoint = transformGroup.Inverse.Transform(point);
			pan.X = -1 * (transformedPoint.X * currentZoom - point.X);
			pan.Y = -1 * (transformedPoint.Y * currentZoom - point.Y);
		}

		private void SetZoom(double currentZoom)
		{
			zoom.ScaleX = zoom.ScaleY = currentZoom;

			ignoreScaleChange = true;
			ZoomSlider.Value = currentZoom;
			ignoreScaleChange = false;
		}

		public void DragFinished()
		{
			_Dragger = null;
		}

		public void ResetPanAndZoom()
		{
			Zoom = 1;
			pan.X = 0;
			pan.Y = 0;
			InvalidateArrange();
		}
	}
}