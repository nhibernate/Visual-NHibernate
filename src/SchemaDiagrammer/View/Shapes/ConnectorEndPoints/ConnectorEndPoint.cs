using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using SchemaDiagrammer.View.Helpers;

namespace SchemaDiagrammer.View.Shapes.ConnectorEndPoints
{
	public class ConnectorEndPoint : Shape, IDiagramEntity
	{
		internal const int Size = 15;
		public Connection Connection { get; set; }
		public string UID { get; set; }
		public DiagramSurface Surface { get; set; }
		public bool Visible { get { return Connection.Visible; } set{} }

		public ConnectorEndPoint(Connection con)
		{
			Connection = con;
			Surface = con.Surface;
			UID = Guid.NewGuid().ToString();

			SetupConnectionBindings();
		}

		private void SetupConnectionBindings()
		{
			var binding = new Binding();
			binding.Source = Connection;
			binding.Path = new PropertyPath(StrokeProperty);
			SetBinding(StrokeProperty, binding);

			binding = new Binding();
			binding.Source = Connection;
			binding.Path = new PropertyPath(StrokeThicknessProperty);
			SetBinding(StrokeThicknessProperty, binding);

			binding = new Binding();
			binding.Source = Connection;
			binding.Path = new PropertyPath(StrokeDashArrayProperty);
			SetBinding(StrokeDashArrayProperty, binding);

			binding = new Binding();
			binding.Source = Connection;
			binding.Path = new PropertyPath(StrokeDashCapProperty);
			SetBinding(StrokeDashCapProperty, binding);
		}

		public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(ConnectorEndPoint), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty DrawingStrategyProperty
			= DependencyProperty.Register("DrawingStrategy", 
			                              typeof(IConnectorEndPointDrawingStrategy),
			                              typeof(ConnectorEndPoint),
			                              new FrameworkPropertyMetadata(new OneConnectorEndPoint(), 
			                                                            FrameworkPropertyMetadataOptions.AffectsMeasure |
			                                                            FrameworkPropertyMetadataOptions.AffectsRender, drawingStrategy_Changed));

		public static readonly DependencyProperty ConnectionPointProperty
			= DependencyProperty.Register("ConnectionPoint",
			                              typeof(ConnectionPoint),
			                              typeof(ConnectorEndPoint),
			                              new FrameworkPropertyMetadata(null,
			                                                            FrameworkPropertyMetadataOptions.AffectsMeasure |
			                                                            FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(ConnectorEndPoint), new FrameworkPropertyMetadata(false));

		private static void drawingStrategy_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var endPoint = d as ConnectorEndPoint;
			var drawingStrategy = e.NewValue as IConnectorEndPointDrawingStrategy;
			if (endPoint == null) return;
			if(drawingStrategy == null) return;
			
			endPoint.SetupConnectionBindings();
			drawingStrategy.SetUpConnectorEndPoint(endPoint);
		}

		public Point StartPoint
		{
			get { return (Point) GetValue(StartPointProperty); }
			set { SetValue(StartPointProperty, value);}
		}

		public IConnectorEndPointDrawingStrategy DrawingStrategy
		{
			get { return (IConnectorEndPointDrawingStrategy)GetValue(DrawingStrategyProperty); }
			set { SetValue(DrawingStrategyProperty, value); }
		}

		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		public ConnectionPoint ConnectionPoint
		{
			get { return (ConnectionPoint)GetValue(ConnectionPointProperty); }
			set { SetValue(ConnectionPointProperty, value); }
		}

		public Point EndPoint
		{
			get 
			{
				if (ConnectionPoint == null)
					return StartPoint;
				return ConnectionPoint.LineAwayFromThisTo(StartPoint, Size); 
			}
		}

		public Vector AsLine { 
			get
			{
				return StartPoint.To(ConnectionPoint.LineAwayFromThisTo(StartPoint, Size));
			}
		}

		protected override Geometry DefiningGeometry
		{
			get 
			{
				if (Connection.Visible == false)
					return Geometry.Empty;

				// Create a StreamGeometry for describing the shape
				StreamGeometry geometry = new StreamGeometry();
				geometry.FillRule = FillRule.EvenOdd;

				using (StreamGeometryContext context = geometry.Open())
				{
					DrawingStrategy.Draw(context, StartPoint, EndPoint);
				}

				geometry.Freeze();

				return geometry;
			}
		}

		public void RemoveFromSurface(DiagramSurface surface)
		{
			surface.Children.Remove(this);
			Surface = null;
		}

		public void AddToSurface(DiagramSurface surface)
		{
			surface.Children.Add(this);
			Surface = surface;
		}
	}
}