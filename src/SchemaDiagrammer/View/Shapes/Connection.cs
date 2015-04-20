using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using ArchAngel.Interfaces;
using SchemaDiagrammer.View.Converters;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes.ConnectorEndPoints;

namespace SchemaDiagrammer.View.Shapes
{
    public class Connection : Shape, IDiagramEntity
	{
		#region Dependency Properties
		public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point), typeof(Connection), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point), typeof(Connection), new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(Connection), new FrameworkPropertyMetadata(false));
		public static readonly DependencyProperty SourceEndPointShapeProperty = DependencyProperty.Register("SourceEndPointDrawingStrategyShape", typeof(IConnectorEndPointDrawingStrategy), typeof(Connection), new FrameworkPropertyMetadata(new OneConnectorEndPoint(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty TargetEndPointShapeProperty = DependencyProperty.Register("TargetEndPointDrawingStrategyShape", typeof(IConnectorEndPointDrawingStrategy), typeof(Connection), new FrameworkPropertyMetadata(new OneConnectorEndPoint(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty SourceConnectionPointProperty = DependencyProperty.Register("SourceConnectionPoint", typeof(ConnectionPoint), typeof(Connection), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty TargetConnectionPointProperty = DependencyProperty.Register("TargetConnectionPoint", typeof(ConnectionPoint), typeof(Connection), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty DrawingStrategyProperty = DependencyProperty.Register("DrawingStrategy", typeof(IConnectionDrawingStrategy), typeof(Connection), new FrameworkPropertyMetadata(new DefaultConnectionDrawingStrategy(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty NameVisibleProperty = DependencyProperty.Register("NameVisible", typeof(bool), typeof(Connection), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty ConnectionNameProperty = DependencyProperty.Register("ConnectionName", typeof(string), typeof(Connection), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty SourceCardinalityProperty = DependencyProperty.Register("SourceCardinality", typeof(Cardinality), typeof(Connection), new FrameworkPropertyMetadata(Cardinality.One, FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty TargetCardinalityProperty = DependencyProperty.Register("TargetCardinality", typeof(Cardinality), typeof(Connection), new FrameworkPropertyMetadata(Cardinality.One, FrameworkPropertyMetadataOptions.AffectsRender));

		#endregion

    	public DiagramSurface Surface { get; set; }
    	public string UID { get; set; }
		public bool IsVirtual { get; set; }

		public IValueConverter SourceCardinalityConverter { get; set; }
		public IValueConverter TargetCardinalityConverter { get; set; }

		public ConnectorEndPoint SourceEndPoint { get; private set; }
		public ConnectorEndPoint TargetEndPoint { get; private set; }

		private bool visible = true;
		private DiagramShape source;
		private DiagramShape target;

    	static Connection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Connection), new FrameworkPropertyMetadata(typeof(Connection)));
        }

        public Connection()
        {
            UID = Guid.NewGuid().ToString();

			SourceCardinalityConverter = new CardinalityConverter();
			TargetCardinalityConverter = new CardinalityConverter();
        }

		public Connection(ConnectionPoint sourceConnectionPoint) : this()
		{
			SourceConnectionPoint = sourceConnectionPoint;
		}

        public Connection(DiagramShape source, DiagramShape target) : this()
        {
            Source = source;
            Target = target;
        }

		public DiagramShape Source
		{
			get
			{
				return source;
			}
			set
			{
				if(source != null)
					source.RemoveConnection(this);
				
				source = value;
				
				if (source != null)
					source.AddConnection(this);
				
				if(source != null && target != null)
					ConnectionPointUtility.TidyAllConnections(source);
			}
		}
		public DiagramShape Target
		{
			get
			{
				return target;
			}
			set
			{
				if(target != null)
					target.RemoveConnection(this);
				
				target = value;

				if(target != null)
					target.AddConnection(this);
				
				if (source != null && target != null)
					ConnectionPointUtility.TidyAllConnections(target);
			}
		}

		public bool Visible
		{
			get { return visible; }
			set
			{
				Visibility = value ? Visibility.Visible : Visibility.Collapsed;
				
				SourceEndPoint.Visibility = Visibility;
				TargetEndPoint.Visibility = Visibility;

                if (value == false)
                    IsSelected = false;

				visible = value;
			}
		}

       protected override Geometry DefiningGeometry
        {
            get
            {
				if (Visible == false)
					return Geometry.Empty;

                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open())
                {
                	DrawingStrategy.Draw(context, this);
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();

                return geometry;
            }
        }

		public void RemoveFromSurface(DiagramSurface surface)
		{
			surface.Children.Remove(this);
			surface.Children.Remove(SourceEndPoint);
			surface.Children.Remove(TargetEndPoint);

			Surface = null;
		}
        
		public void AddToSurface(DiagramSurface surface)
		{
			Surface = surface;
			Surface.Children.Add(this);

			SourceEndPoint = new ConnectorEndPoint(this);
			TargetEndPoint = new ConnectorEndPoint(this);

			// Setup ConnectorEndPoints
			if (DrawingStrategy != null && DrawingStrategy.OverrideConnectorEndPointSetup)
			{
				DrawingStrategy.CustomSetupConnectorEndPoint(this);
			}
			
			SetupSourceConnectorEndPoint();
			SetupTargetConnectorEndPoint();
			
			SourceEndPoint.AddToSurface(surface);
			TargetEndPoint.AddToSurface(surface);

			// Setup Adorners
			AdornerLayer layer = AdornerLayer.GetAdornerLayer(this);
			if (layer == null) return;

			SetupRelationshipNameAdorner(layer);
			
			//SetupSourceCardinalityAdorner(layer);
			//SetupTargetCardinalityAdorner(layer);
		}

    	private void SetupSourceConnectorEndPoint()
    	{
    		SourceEndPoint.SetBinding(ConnectorEndPoint.ConnectionPointProperty,
    		                    new Binding {Source = this, Path = new PropertyPath(SourceConnectionPointProperty)});

			SourceEndPoint.SetBinding(ConnectorEndPoint.StartPointProperty,
				new Binding { Source = this, Path = new PropertyPath(StartPointProperty) });

			// Creates a binding that converts between the SourceCardinality property on
			// this connection and an EndPointDrawingStrategy
			SourceEndPoint.SetBinding(ConnectorEndPoint.DrawingStrategyProperty,
				new Binding
				{
					Source = this,
					Path = new PropertyPath(SourceCardinalityProperty),
					Converter = SourceCardinalityConverter
				});		
    	}

		private void SetupTargetConnectorEndPoint()
		{
			TargetEndPoint.SetBinding(ConnectorEndPoint.ConnectionPointProperty,
					new Binding { Source = this, Path = new PropertyPath(TargetConnectionPointProperty) });

			TargetEndPoint.SetBinding(ConnectorEndPoint.StartPointProperty,
				new Binding { Source = this, Path = new PropertyPath(EndPointProperty) });

			TargetEndPoint.SetBinding(ConnectorEndPoint.DrawingStrategyProperty,
				new Binding
				{
					Source = this,
					Path = new PropertyPath(TargetCardinalityProperty),
					Converter = TargetCardinalityConverter
				});
		}

    	private void SetupRelationshipNameAdorner(AdornerLayer layer)
    	{
    		TextShapeDecoration decor = new TextShapeDecoration(this);
    		decor.CentreText = true;

    		var multiBinding = new MultiBinding();
    		multiBinding.Bindings.Add(new Binding{ Source = this, Path = new PropertyPath(StartPointProperty) });
    		multiBinding.Bindings.Add(new Binding{ Source = this, Path = new PropertyPath(EndPointProperty) });
    		multiBinding.Converter = new MiddleOfLineConverter();

    		decor.SetBinding(TextShapeDecoration.AnchorProperty, multiBinding);

    		decor.SetBinding(TextShapeDecoration.TextProperty,
    		                 new Binding {Source = this, Path = new PropertyPath(ConnectionNameProperty)});
			decor.SetBinding(VisibilityProperty,
							 new Binding { Source = this, Path = new PropertyPath(NameVisibleProperty), Converter = new VisibilityConverter() });

    		layer.Add(decor);
    	}

    	public static void SetConnectionPointBinding(Connection connection, ConnectionPoint sourceConnPoint, ConnectionPoint targetConnPoint)
    	{
    		SetConnectionStartPointBinding(connection, sourceConnPoint);
    		SetConnectionEndPointBinding(connection, targetConnPoint);           
    	}

    	public static void SetConnectionEndPointBinding(Connection connection, ConnectionPoint connPoint)
    	{
			if (connPoint == null) throw new ArgumentNullException("connPoint");

    		connection.SetBinding(EndPointProperty, new Binding { Source = connPoint, Path = new PropertyPath(ConnectionPoint.LocationProperty)});
    		connection.TargetConnectionPoint = connPoint;
    	}

    	public static void SetConnectionStartPointBinding(Connection connection, ConnectionPoint connPoint)
    	{
			if (connPoint == null) throw new ArgumentNullException("connPoint");

			connection.SetBinding(StartPointProperty, new Binding { Source = connPoint, Path = new PropertyPath(ConnectionPoint.LocationProperty) });
    		connection.SourceConnectionPoint = connPoint;
    	}

		public IConnectionDrawingStrategy DrawingStrategy
		{
			get { return (IConnectionDrawingStrategy)GetValue(DrawingStrategyProperty); }
			set
			{
				SetValue(DrawingStrategyProperty, value);
				if (SourceEndPoint == null || TargetEndPoint == null)
					return; // If the end points haven't been set up yet then the custom
							// setup will happen later when it gets added to the surface.

				if (DrawingStrategy != null && DrawingStrategy.OverrideConnectorEndPointSetup)
				{
					DrawingStrategy.CustomSetupConnectorEndPoint(this);
				}
			}
		}

    	public bool NameVisible
    	{
			get { return (bool)GetValue(NameVisibleProperty); }
			set { SetValue(NameVisibleProperty, value); }
    	}

		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		public Point StartPoint
		{
			get { return (Point)GetValue(StartPointProperty); }
			set { SetValue(StartPointProperty, value); }
		}

		public Point EndPoint
		{
			get { return (Point)GetValue(EndPointProperty); }
			set { SetValue(EndPointProperty, value); }
		}

		public IConnectorEndPointDrawingStrategy SourceEndPointDrawingStrategyShape
		{
			get { return (IConnectorEndPointDrawingStrategy)GetValue(SourceEndPointShapeProperty); }
			set { SetValue(SourceEndPointShapeProperty, value); }
		}

		public IConnectorEndPointDrawingStrategy TargetEndPointDrawingStrategyShape
		{
			get { return (IConnectorEndPointDrawingStrategy)GetValue(TargetEndPointShapeProperty); }
			set { SetValue(TargetEndPointShapeProperty, value); }
		}

		public ConnectionPoint SourceConnectionPoint
		{
			get { return (ConnectionPoint)GetValue(SourceConnectionPointProperty); }
			set { SetValue(SourceConnectionPointProperty, value); }
		}
		public ConnectionPoint TargetConnectionPoint
		{
			get { return (ConnectionPoint)GetValue(TargetConnectionPointProperty); }
			set { SetValue(TargetConnectionPointProperty, value); }
		}

		public Cardinality SourceCardinality
		{
			get { return (Cardinality)GetValue(SourceCardinalityProperty); }
			set { SetValue(SourceCardinalityProperty, value); }
		}

		public Cardinality TargetCardinality
		{
			get { return (Cardinality)GetValue(TargetCardinalityProperty); }
			set { SetValue(TargetCardinalityProperty, value); }
		}

		public string ConnectionName
		{
			get { return (string)GetValue(ConnectionNameProperty); }
			set { SetValue(ConnectionNameProperty, value); }
		}
	}
}