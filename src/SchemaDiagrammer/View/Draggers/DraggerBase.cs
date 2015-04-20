using System;
using System.Windows;
using System.Windows.Input;

namespace SchemaDiagrammer.View.Draggers
{
	public abstract class DraggerBase
	{
		protected Point _StartPoint;  // Where the mouse started off from
		protected Point _OriginalPosition; // Where the element started from
		protected bool _IsDragging; // Are we actually dragging the shape around?

	    protected bool _HasMoved = false;

		protected readonly DiagramSurface _Surface;
		protected readonly MainWindow _Window;

		protected DraggerBase(DiagramSurface surface, MainWindow window, Point startPoint)
		{
			_Surface = surface;
			_Window = window;
			_StartPoint = startPoint;
		}

		protected void SetupMouse()
		{
			_Window.PreviewKeyDown += Window_PreviewKeyDown;
			_Window.PreviewMouseLeftButtonUp += DiagramSurface_PreviewMouseLeftButtonUp;
			_Window.PreviewMouseMove += DiagramSurface_PreviewMouseMove;

			Mouse.Capture(_Surface, CaptureMode.SubTree);

		    _HasMoved = false;
		}

		protected abstract void MoveOriginalShapeToOverlay();
		protected abstract void CreateOverlay();
		protected abstract void MoveOverlay(Point mousePosition, double left, double top);
		protected abstract Point CalculateOriginalPosition();
		protected abstract void RemoveOverlay();
		protected abstract void OperationCancelled();
		protected abstract bool ShouldHighlightObject(IDiagramEntity obj);
		protected abstract void OnDragFinished();
		protected abstract void OperationSuccessful();
		protected abstract bool CancelOperation();

		protected void DragFinished(bool canceled)
		{
			Mouse.Capture(null);
			if (_IsDragging)
			{
				// Figure out how far the mouse has moved
				Vector distance = Mouse.GetPosition(_Surface) - _StartPoint;
				if(Math.Abs(distance.X) < SystemParameters.MinimumHorizontalDragDistance
				   && Math.Abs(distance.Y) < SystemParameters.MinimumVerticalDragDistance)
					canceled = true;

				canceled |= CancelOperation();
			    canceled |= !_HasMoved;

				if (!canceled)
				{
					MoveOriginalShapeToOverlay();
					OperationSuccessful();
				}
				else
				{
					OperationCancelled();
				}
				RemoveOverlay();
			}

			_IsDragging = false;

			_Window.PreviewKeyDown -= Window_PreviewKeyDown;
			_Window.PreviewMouseLeftButtonUp -= DiagramSurface_PreviewMouseLeftButtonUp;
			_Window.PreviewMouseMove -= DiagramSurface_PreviewMouseMove;

			_Window.DragFinished();
			OnDragFinished();
		}



		private void DragStarted()
		{
			_IsDragging = true;

			_OriginalPosition = CalculateOriginalPosition();

			CreateOverlay();
		}

		private void DragMoved()
		{
		    _HasMoved = true;
			var currentPosition = Mouse.GetPosition(_Surface);

			double elementLeft = (currentPosition.X - _StartPoint.X) + _OriginalPosition.X;
			double elementTop = (currentPosition.Y - _StartPoint.Y) + _OriginalPosition.Y;

			MoveOverlay(currentPosition, elementLeft, elementTop);
		}

		private void DiagramSurface_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			DragFinished(false);
		}

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			// This is handled at the window level, because neither Canvas nor
			// its children ever get keyboard focus.
			if (e.Key == Key.Escape)
				DragFinished(true);
		}

		protected void DiagramSurface_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			//bool xMovementIsSignificant = Math.Abs(e.GetPosition(_Surface).X - _StartPoint.X) > SystemParameters.MinimumHorizontalDragDistance;
			//bool yMovementIsSignificant = Math.Abs(e.GetPosition(_Surface).Y - _StartPoint.Y) > SystemParameters.MinimumVerticalDragDistance;

			if (_IsDragging)
				DragMoved();
			if (!_IsDragging) //&& xMovementIsSignificant && yMovementIsSignificant)
			{
				DragStarted();
			}
			e.Handled = true;
		}
	}
}