using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using SchemaDiagrammer.View.Converters;

namespace SchemaDiagrammer.View.Shapes
{
    public class ConnectionPoint : Shape, IDiagramEntity
    {
        public string UID { get; set; }
        public DiagramSurface Surface { get; set; }
    	public DiagramShape ParentShape { get; set; }
		public bool Visible { get { return ParentShape.Visible; } set{} }

        private readonly RectangleGeometry recGeo = new RectangleGeometry();

		public static readonly DependencyProperty LocationProperty = DependencyProperty.Register("Location", typeof(Point), typeof(ConnectionPoint), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));
		public static readonly DependencyProperty SideOfShapeProperty = DependencyProperty.Register("SideOfShape", typeof(SideOfShape), typeof(ConnectionPoint), new FrameworkPropertyMetadata(SideOfShape.Top, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));
		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(double), typeof(ConnectionPoint), new FrameworkPropertyMetadata(0.5, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));


		static ConnectionPoint()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ConnectionPoint), new FrameworkPropertyMetadata(typeof(ConnectionPoint)));
		}

    	public ConnectionPoint(DiagramShape parent, SideOfShape side, double position)
        {
    		UID = Guid.NewGuid().ToString();
    		Side = side;
    		Position = position;

            ParentShape = parent;

            SetBinding(LocationProperty,
                       CreateLeftTopWidthHeightBinding(parent));

			// The Fill property must be set to a Brush in order for the shape to be considered solid.
			// Otherwise only the edges are considered part of it.
			Fill = Brushes.Transparent;

			MouseEnter += ConnectionPoint_MouseEnter;
			MouseLeave += ConnectionPoint_MouseLeave;
        }

		private MultiBinding CreateLeftTopWidthHeightBinding(DiagramShape connectable)
		{
			// Create a multibinding collection and assign an appropriate converter to it
			var multiBinding = new MultiBinding { Converter = new ConnectorBindingConverter() };

			// Create binding #1 to IConnectable to handle Left
			var binding = new Binding { Source = connectable, Path = new PropertyPath(Canvas.LeftProperty) };
			multiBinding.Bindings.Add(binding);

			// Create binding #2 to IConnectable to handle Top
			binding = new Binding { Source = connectable, Path = new PropertyPath(Canvas.TopProperty) };
			multiBinding.Bindings.Add(binding);

			// Create binding #3 to IConnectable to handle ActualWidth
			binding = new Binding { Source = connectable, Path = new PropertyPath(ActualWidthProperty) };
			multiBinding.Bindings.Add(binding);

			// Create binding #4 to IConnectable to handle ActualHeight
			binding = new Binding { Source = connectable, Path = new PropertyPath(ActualHeightProperty) };
			multiBinding.Bindings.Add(binding);
			
			// Create binding #5 to ConnectionPoint to handle SideOfShape
			binding = new Binding { Source = this, Path = new PropertyPath(SideOfShapeProperty) };
			multiBinding.Bindings.Add(binding);

			// Create binding #6 to ConnectionPoint to handle Position
			binding = new Binding { Source = this, Path = new PropertyPath(PositionProperty) };
			multiBinding.Bindings.Add(binding);

			return multiBinding;
		}

		public Point LineAwayFromThisTo(Point point, double length)
		{
			switch (Side)
			{
				case SideOfShape.Top:
					return point + new Vector(0, -length);
				case SideOfShape.Bottom:
					return point + new Vector(0, length);
				case SideOfShape.Left:
					return point + new Vector(-length, 0);
				case SideOfShape.Right:
					return point + new Vector(length, 0);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void ConnectionPoint_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Stroke = Brushes.Red;
		}

		private void ConnectionPoint_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Stroke = Brushes.Black;
		}

        public Point Location
        {
            get { return (Point)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

		public SideOfShape Side
		{
			get { return (SideOfShape)GetValue(SideOfShapeProperty); }
			set { SetValue(SideOfShapeProperty, value); }
		}

		public double Position
		{
			get { return (double)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

        protected override Geometry DefiningGeometry
        {
            get
            {
				if (Visible == false)
					return Geometry.Empty;

            	recGeo.Rect = new Rect(new Point(Location.X - 3, Location.Y - 3), new Size(6, 6));
                return recGeo;
            }
        }

		public void AddToSurface(DiagramSurface surface)
		{
			if (surface == null) return;

			surface.Children.Add(this);
			Surface = surface;
		}

		public void RemoveFromSurface(DiagramSurface surface)
		{
			if (surface == null) return;

			surface.Children.Remove(this);
			Surface = null;
		}
    }
}