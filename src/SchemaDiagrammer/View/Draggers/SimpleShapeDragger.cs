using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SchemaDiagrammer.View.Helpers;
using SchemaDiagrammer.View.Shapes;

namespace SchemaDiagrammer.View.Draggers
{
	public class SimpleShapeDragger : DraggerBase
	{
		private readonly DiagramShape _OriginalShape; // What is it that we're dragging?
		private DiagramShape _OverlayShape; // What is it that we're using to show where the shape will end up?

		public SimpleShapeDragger(DiagramSurface surface, MainWindow window, Point startPoint, DiagramShape originalShape)
			: base(surface, window, startPoint)
		{
			_OriginalShape = originalShape;

			SetupMouse();
		}

		protected override void MoveOriginalShapeToOverlay()
		{
			Canvas.SetLeft(_OriginalShape, Canvas.GetLeft(_OverlayShape));
			Canvas.SetTop(_OriginalShape, Canvas.GetTop(_OverlayShape));
		}

		protected override void CreateOverlay()
		{
			// Add a rectangle to indicate where we'll end up.
			// We'll just use an alpha-blended visual brush.
			var brush = new VisualBrush(_OriginalShape) { Opacity = 0.2 };

			_OverlayShape = new DiagramShape
			                	{
			                		Width = _OriginalShape.RenderSize.Width,
			                		Height = _OriginalShape.RenderSize.Height,
			                		Background = brush,
			                		Content = _OriginalShape.Content
			                	};

			Canvas.SetLeft(_OverlayShape, -1000);
			Canvas.SetTop(_OverlayShape, -1000);

			_Surface.Children.Add(_OverlayShape);
		}

		protected override void MoveOverlay(Point mousePosition, double left, double top)
		{
			Canvas.SetLeft(_OverlayShape, left);
			Canvas.SetTop(_OverlayShape, top);
		}

		protected override Point CalculateOriginalPosition()
		{
			return new Point(Canvas.GetLeft(_OriginalShape), Canvas.GetTop(_OriginalShape));
		}

		protected override void RemoveOverlay()
		{
			_Surface.Children.Remove(_OverlayShape);
			_OverlayShape = null;
		}

		protected override void OperationCancelled()
		{
			// Do nothing
		}

		protected override bool ShouldHighlightObject(IDiagramEntity obj)
		{
			return false;
		}

		protected override void OnDragFinished()
		{
			ConnectionPointUtility.TidyAllConnections_AndConnectedShapes(_OriginalShape);
		}

		protected override void OperationSuccessful()
		{
			// Do Nothing
		}

		protected override bool CancelOperation()
		{
			return false;
		}
	}
}