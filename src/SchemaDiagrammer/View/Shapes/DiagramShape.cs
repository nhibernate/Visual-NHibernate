using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SchemaDiagrammer.View.Converters;
using Slyce.Common.EventExtensions;

namespace SchemaDiagrammer.View.Shapes
{
	/// <summary>
    /// Simple shape for representing a table.
    /// </summary>
    [DebuggerDisplay("Shape: {EntityName}")]
    public class DiagramShape : ContentControl, IDiagramEntity, INotifyPropertyChanged
    {
        private readonly List<ConnectionPoint> _ConnectionPoints = new List<ConnectionPoint>();

		public static readonly DependencyProperty IsSelectedProperty = 
			DependencyProperty.Register("IsSelected", typeof(bool), typeof(DiagramShape), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty EntityNameProperty =
			DependencyProperty.Register("EntityName", typeof(string), typeof(DiagramShape), new FrameworkPropertyMetadata(""));

		private readonly List<Connection> _Connections = new List<Connection>();
		private bool visible = true;

		static DiagramShape()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramShape), new FrameworkPropertyMetadata(typeof(DiagramShape)));
        }

		public DiagramShape()
		{
			UID = Guid.NewGuid().ToString();

			_ConnectionPoints.Add(new ConnectionPoint(this, SideOfShape.Top, 0.5));
			_ConnectionPoints.Add(new ConnectionPoint(this, SideOfShape.Bottom, 0.5));
			_ConnectionPoints.Add(new ConnectionPoint(this, SideOfShape.Left, 0.5));
			_ConnectionPoints.Add(new ConnectionPoint(this, SideOfShape.Right, 0.5));
		}

		protected void NotifyOfPropertyChanged(string property)
		{
			PropertyChanged.RaiseEvent(this, property);
		}

        public string EntityName
        {
            get { return (string)GetValue(EntityNameProperty); }
            set { SetValue(EntityNameProperty, value); }
        }

		public ConnectionPoint AddAnotherConnectionPointAt(SideOfShape side, double position)
		{
			var item = new ConnectionPoint(this, side, position);
			_ConnectionPoints.Add(item);
			item.AddToSurface(Surface);
			return item;
		}

    	/// <summary>
        /// Gets or sets the surface this shape is attached to.
        /// </summary>
        /// <value>The surface this shape is attached to, or null.</value>
        public DiagramSurface Surface { get; set; }

    	public void RemoveFromSurface(DiagramSurface surface)
    	{
    		surface.Children.Remove(this);

			foreach(var cp in ConnectionPoints)
				cp.RemoveFromSurface(surface);

			Surface = null;
    	}

    	public void AddToSurface(DiagramSurface surface)
    	{
			surface.Children.Add(this);

			foreach (var cp in ConnectionPoints)
				cp.AddToSurface(surface);

			Surface = surface;
    	}

    	public bool Visible 
		{
			get { return visible; }
			set
			{
				Visibility = value ? Visibility.Visible : Visibility.Collapsed;
				foreach (var cp in ConnectionPoints)
				{
				    cp.Visibility = Visibility;
				}

                if(value == false)
                    IsSelected = false;

				visible = value;
			} 
		}

    	/// <summary>
        /// Gets or sets the unique identifier inside the document.
        /// </summary>
        /// <value>The UID.</value>
        public string UID { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public Point Location
        {
            get { return new Point(Canvas.GetLeft(this), Canvas.GetTop(this)); }
            set
            {
                Canvas.SetLeft(this, value.X); Canvas.SetTop(this, value.Y);
            	PropertyChanged.RaiseEvent(this, "Location");
            }
        }

    	public Point CenterLocation
    	{
    		get
    		{
				return new Point(Location.X + ActualWidth / 2, Location.Y + ActualHeight / 2);
    		}
    	}

		public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

		public void AddConnection(Connection connection)
		{
			if(_Connections.Contains(connection) == false)
				_Connections.Add(connection);
		}

		public void RemoveConnection(Connection connection)
		{
			_Connections.Remove(connection);
		}

		public IEnumerable<Connection> Connections
		{
			get { return _Connections; }
		}

        public IEnumerable<ConnectionPoint> ConnectionPoints
        {
            get { return _ConnectionPoints; }
        }

		public IEnumerable<DiagramShape> ConnectedShapes
		{
			get
			{
				foreach(var connection in _Connections)
				{
					yield return connection.Source == this ? connection.Target : connection.Source;
				}
			}
		}

    	public void RemoveConnectionPoint(ConnectionPoint p)
    	{
    		p.RemoveFromSurface(Surface);
    		_ConnectionPoints.Remove(p);
    	}

		public void RemoveUnusedConnectionPoints()
		{
			var cpsToRemove = new List<ConnectionPoint>();

			foreach (var cp in _ConnectionPoints)
			{
				ConnectionPoint cp1 = cp;
				if (_Connections.Any(c => c.SourceConnectionPoint == cp1 || c.TargetConnectionPoint == cp1))
					continue;

				cpsToRemove.Add(cp);
			}

			// Remove all of the connection points identified as free
			cpsToRemove.ForEach(RemoveConnectionPoint);
		}

		public event PropertyChangedEventHandler PropertyChanged;
    }
}