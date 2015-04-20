using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Slyce.Common.Controls.Diagramming.Shapes
{
	public partial class ShapeCanvas : UserControl
	{
		public delegate void LineEndWithFocusChangedDelegate(LineEndWithFocus lineEndWithFocus);
		public event LineEndWithFocusChangedDelegate LineEndWithFocusChanged;
		private bool BusyPopulating = false;
		internal bool CtrlKeyDown = false;
		internal bool ShiftKeyDown = false;

		private enum Layouts
		{
			Star,
			ThreeLayerVertical,
			ThreeLayerHorizontal,
			SwimLanesVertical
		}
		public class LineEndWithFocus
		{
			public enum EndTypes
			{
				Start,
				End
			}
			public LinkLine Line;
			public EndTypes EndType;

			public LineEndWithFocus(LinkLine line, EndTypes endType)
			{
				Line = line;
				EndType = endType;
			}
		}
		public class SwimLaneStyle
		{
			public enum Styles
			{
				Fill,
				Line
			}
			public Color BackColor1 = Color.FromArgb(0, 0, 0);
			public Color BackColor2 = Color.FromArgb(255, 255, 255);
			public Color ForeColor = Color.FromArgb(255, 255, 255);
			public float GradientAngle = 90F;
			public string Heading = "Not set";
			public Styles Style = Styles.Fill;

			public SwimLaneStyle(Color backColor1, Color backColor2, Color foreColor, float gradientAngle, string heading, Styles style)
			{
				BackColor1 = backColor1;
				BackColor2 = backColor2;
				ForeColor = foreColor;
				GradientAngle = gradientAngle;
				Heading = heading;
				Style = style;
			}
		}

		private Layouts DiagramType = Layouts.Star;
		private RawShape MainShape;
		private List<RawShape> ChildEntities = new List<RawShape>();
		private List<RawShape> TopLevelShapes = new List<RawShape>();
		private List<RawShape> BottomLevelShapes = new List<RawShape>();
		private List<RawShape> RightAlignedShapes = new List<RawShape>();
		public int ChildShapeMaxWidth = 40;
		public int ChildShapeMaxHeight = 40;
		public int MainShapeMaxWidth = 60;
		public int MainShapeMaxHeight = 60;
		private bool _KeepMainShapeFull = true;
		public bool KeepMainShapeCentered = false;
		private LineEndWithFocus _FocusedLineEnd;
		private LineEndWithFocus TempFocusedLineEnd = null;
		public SwimLaneStyle SwimLane1 = new SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.FromArgb(40, 40, 40), Color.FromArgb(255, 255, 255), 0F, "Left", SwimLaneStyle.Styles.Line);
		public SwimLaneStyle SwimLane2 = new SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.FromArgb(40, 40, 40), Color.FromArgb(255, 255, 255), 0F, "Centre", SwimLaneStyle.Styles.Line);
		public SwimLaneStyle SwimLane3 = new SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.FromArgb(40, 40, 40), Color.FromArgb(255, 255, 255), 0F, "Right", SwimLaneStyle.Styles.Line);
		public SwimLaneStyle SwimLane4 = new SwimLaneStyle(Color.FromArgb(40, 40, 40), Color.FromArgb(40, 40, 40), Color.FromArgb(255, 255, 255), 0F, "Level4", SwimLaneStyle.Styles.Line);
		public int Gap = 20;

		public ShapeCanvas()
		{
			InitializeComponent();

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);
		}

		private LineEndWithFocus FocusedLineEnd
		{
			get { return _FocusedLineEnd; }
			set
			{
				if (_FocusedLineEnd != value)
				{
					_FocusedLineEnd = value;

					if (LineEndWithFocusChanged != null)
						LineEndWithFocusChanged(value);
				}
			}
		}

		public void ClearShapes()
		{
			this.Controls.Clear();
			MainShape = null;
			ChildEntities.Clear();
			TopLevelShapes.Clear();
			BottomLevelShapes.Clear();
		}

		public bool KeepMainShapeFull
		{
			get { return _KeepMainShapeFull; }
			set { _KeepMainShapeFull = value; }
		}

		private void ResizeMainShape(int width, int height)
		{
			int newWidth = width;
			int newHeight = height;

			if (height >= MainShapeMaxHeight)
				newHeight = MainShapeMaxHeight;

			if (width >= MainShapeMaxWidth)
				newWidth = MainShapeMaxWidth;

			if (KeepMainShapeFull)
			{
				// Make the main shape the same width/height as the summ of the linked shapes, so that all links are straight
				switch (DiagramType)
				{
					case Layouts.ThreeLayerVertical:
						if (TopLevelShapes.Count == 1)
							newWidth = Math.Max(newWidth, TopLevelShapes[0].Width);
						else if (TopLevelShapes.Count > 1)
							newWidth = Math.Max(newWidth, TopLevelShapes[TopLevelShapes.Count - 1].Right - TopLevelShapes[0].Left);

						if (BottomLevelShapes.Count == 1)
							newWidth = Math.Max(newWidth, BottomLevelShapes[0].Width);
						else if (BottomLevelShapes.Count > 1)
							newWidth = Math.Max(newWidth, BottomLevelShapes[BottomLevelShapes.Count - 1].Right - BottomLevelShapes[0].Left);

						break;
					case Layouts.ThreeLayerHorizontal:
						if (TopLevelShapes.Count == 1)
							newHeight = Math.Max(newHeight, TopLevelShapes[0].Height);
						else if (TopLevelShapes.Count > 1)
							newHeight = Math.Max(newHeight, TopLevelShapes[TopLevelShapes.Count - 1].Bottom - TopLevelShapes[0].Top);

						if (BottomLevelShapes.Count == 1)
							newHeight = Math.Max(newHeight, BottomLevelShapes[0].Width);
						else if (BottomLevelShapes.Count > 1)
							newHeight = Math.Max(newHeight, BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom - BottomLevelShapes[0].Top);

						break;
					default:
						throw new NotImplementedException("KeepMainShapeFull cannot be true for this type of diagram.");
				}
			}
			if (KeepMainShapeFull)
				MainShape.AutoSizeWidth = false;

			if (MainShape.Height != newHeight)
				MainShape.Height = newHeight;

			if (MainShape.Width != newWidth)
				MainShape.Width = newWidth;

			if (!KeepMainShapeFull)
			{
				using (Graphics g = Graphics.FromHwnd(this.Handle))
				{
					MainShape.ReCalculateSize(g);
					g.Dispose();
				}
			}
		}

		public void DrawStar(RawShape centreShape, List<RawShape> outerShapes)
		{
			Slyce.Common.Utility.SuspendPainting(this);
			KeepMainShapeFull = false;
			ClearShapes();
			MainShape = centreShape;
			DiagramType = Layouts.Star;
			ChildEntities = outerShapes;
			Draw();
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void DrawThreeLayer(RawShape centreShape, List<RawShape> topLevelShapes, List<RawShape> bottomLevelShapes, List<RawShape> rightAlignedShapes)
		{
			BusyPopulating = true;
			Slyce.Common.Utility.SuspendPainting(this);
			ClearShapes();

			if (rightAlignedShapes == null)
				rightAlignedShapes = new List<RawShape>();

			MainShape = centreShape;
			TopLevelShapes = topLevelShapes;
			BottomLevelShapes = bottomLevelShapes;
			RightAlignedShapes = rightAlignedShapes;
			Draw();
			Slyce.Common.Utility.ResumePainting(this);
			BusyPopulating = false;
		}

		public void DrawThreeLayerVertical(RawShape centreShape, List<RawShape> topLevelShapes, List<RawShape> bottomLevelShapes)
		{
			DrawThreeLayerVertical(centreShape, topLevelShapes, bottomLevelShapes, null);
		}

		public void DrawThreeLayerVertical(RawShape centreShape, List<RawShape> topLevelShapes, List<RawShape> bottomLevelShapes, List<RawShape> rightAlignedShapes)
		{
			if (topLevelShapes == null)
				topLevelShapes = new List<RawShape>();

			if (bottomLevelShapes == null)
				bottomLevelShapes = new List<RawShape>();

			DiagramType = Layouts.ThreeLayerVertical;
			DrawThreeLayer(centreShape, topLevelShapes, bottomLevelShapes, rightAlignedShapes);
		}

		public void DrawThreeLayerHorizontal(RawShape centreShape, List<RawShape> topLevelShapes, List<RawShape> bottomLevelShapes, bool keepMainShapeFull)
		{
			if (topLevelShapes == null)
				topLevelShapes = new List<RawShape>();

			if (bottomLevelShapes == null)
				bottomLevelShapes = new List<RawShape>();

			KeepMainShapeFull = keepMainShapeFull;
			DiagramType = Layouts.ThreeLayerHorizontal;
			DrawThreeLayer(centreShape, topLevelShapes, bottomLevelShapes, null);
		}

		public void DrawVerticalSwimLanes(RawShape centreShape, List<RawShape> topLevelShapes, List<RawShape> bottomLevelShapes, List<RawShape> level4Shapes, string headingLeft, string headingCentre, string headingRight, string headingLevel4)
		{
			SwimLane1.Heading = headingLeft;
			SwimLane2.Heading = headingCentre;
			SwimLane3.Heading = headingRight;
			SwimLane4.Heading = headingLevel4;
			DiagramType = Layouts.SwimLanesVertical;
			DrawThreeLayer(centreShape, topLevelShapes, bottomLevelShapes, level4Shapes);
		}

		private void Draw()
		{
			if (MainShape == null)
				return;

			int height = Math.Max(1, this.Width / 10);
			int width = height;

			switch (DiagramType)
			{
				case Layouts.ThreeLayerVertical:
					//ResizeEntityShapes(TopLevelShapes, height, width);
					//ResizeEntityShapes(BottomLevelShapes, height, width);
					LayoutShapesInLevelsVertical();
					//ResizeMainShape(width, height);
					//PositionMainShape();
					break;
				case Layouts.ThreeLayerHorizontal:
					LayoutShapesInLevelsHorizontal();
					break;
				case Layouts.Star:
					//ResizeEntityShapes(ChildEntities, height, width);
					LayoutChildrenInSurroundingCircle(ChildEntities);
					ResizeMainShape(width, height);
					PositionMainShape();
					break;
				case Layouts.SwimLanesVertical:
					LayoutShapesInVerticalSwimLanes();
					break;
				default:
					throw new NotImplementedException("Not handled yet");
			}
		}

		private void PositionMainShape()
		{
			//int gap = 10;

			switch (DiagramType)
			{
				case Layouts.Star:
					MainShape.Top = this.ClientSize.Height / 2 - MainShape.Height / 2;
					MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
					break;
				case Layouts.ThreeLayerHorizontal:
					if ((TopLevelShapes.Count > 0 && BottomLevelShapes.Count > 0) ||
						(TopLevelShapes.Count == 0 && BottomLevelShapes.Count == 0))
					{
						MainShape.Top = Math.Min(TopLevelShapes[0].Top, BottomLevelShapes[0].Top);// this.ClientSize.Height / 2 - MainShape.Height / 2;
						MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
					}
					else if (TopLevelShapes.Count > 0)
					{
						MainShape.Top = TopLevelShapes[0].Top;
						MainShape.Left = this.ClientSize.Width - MainShape.Width - Gap;
					}
					else if (BottomLevelShapes.Count > 0)
					{
						//if (BottomLevelShapes.Count == 1)
						MainShape.Top = BottomLevelShapes[0].Top;
						//MainShape.Top = this.ClientSize.Height / 2 - MainShape.Height / 2;
						MainShape.Left = Gap;
					}
					break;
				case Layouts.ThreeLayerVertical:
					if ((TopLevelShapes.Count > 0 && BottomLevelShapes.Count > 0) ||
						(TopLevelShapes.Count == 0 && BottomLevelShapes.Count == 0))
					{
						MainShape.Top = this.ClientSize.Height / 2 - MainShape.Height / 2;
						MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
					}
					else if (TopLevelShapes.Count > 0)
					{
						MainShape.Top = this.ClientSize.Height - MainShape.Height - Gap;
						MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
					}
					else if (BottomLevelShapes.Count > 0)
					{
						if (RightAlignedShapes.Count > 0)
							MainShape.Top = 30;
						else
							MainShape.Top = Gap;

						MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
					}
					break;
				default:
					throw new NotImplementedException("Not handled yet");
			}
		}

		bool Busy = false;

		private void LayoutShapesInLevelsVertical()
		{
			if (Busy) return;

			Busy = true;
			Slyce.Common.Utility.SuspendPainting(this.Parent);

			using (Graphics g = Graphics.FromHwnd(this.Handle))
			{
				int widthPerChild = 0;
				int width = 0;
				int leftPos = 0;
				int maxBottomTopLevelShapes = 0;
				int widthOfTopLevelShapes = Gap;
				int widthOfBottomLevelShapes = Gap;
				int maxHeightBottomLevelShapes = 0;

				#region Calculate required height of the canvas

				MainShape.ReCalculateSize(g);

				foreach (RawShape childShape in TopLevelShapes)
				{
					childShape.ReCalculateSize(g);
					//childShape.Top = Gap;
					maxBottomTopLevelShapes = Math.Max(maxBottomTopLevelShapes, childShape.Bottom);
				}
				foreach (RawShape childShape in BottomLevelShapes)
				{
					childShape.ReCalculateSize(g);
					//childShape.Top = this.ClientSize.Height - childShape.Height - Gap;
					//widthPerChild = Math.Max(widthPerChild, childShape.Width);
					maxHeightBottomLevelShapes = Math.Max(maxHeightBottomLevelShapes, childShape.Height);
				}
				foreach (var shape in TopLevelShapes)
					widthOfTopLevelShapes += shape.Width + Gap;

				foreach (var shape in BottomLevelShapes)
					widthOfBottomLevelShapes += shape.Width + Gap;

				int minHeight = maxBottomTopLevelShapes + MainShapeMaxHeight + maxHeightBottomLevelShapes + Gap * 5;
				int minWidth = Math.Max(widthOfBottomLevelShapes, widthOfTopLevelShapes);

				if (this.Height > this.Parent.Height - this.Top - 20)
					this.Height = this.Parent.Height - this.Top - 20;

				if (this.Width > this.Parent.Width - this.Left - 20)
					this.Width = this.Parent.Width - this.Left - 20;

				if (minHeight > this.ClientSize.Height)
					this.Height = minHeight;

				if (minWidth > this.ClientSize.Width)
					this.Width = minWidth;

				//int canvasHeight = MainShape.Bottom;

				//if (TopLevelShapes.Count > 0)
				//    canvasHeight = Math.Max(canvasHeight, TopLevelShapes[TopLevelShapes.Count - 1].Bottom);

				//if (BottomLevelShapes.Count > 0)
				//    canvasHeight = Math.Max(canvasHeight, BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom);

				////this.Height = height + 5;

				//if (canvasHeight + Gap < Parent.Height)
				//    this.Height = Parent.Height;
				//else
				//    this.Height = canvasHeight + Gap;

				//this.Height = Math.Max(this.Height, height + 5);
				#endregion

				#region Top Level Shapes
				if (TopLevelShapes.Count > 0)
				{
					widthPerChild = this.ClientSize.Width / TopLevelShapes.Count;
					width = Math.Min(widthPerChild - 4, ChildShapeMaxWidth);
					leftPos = 0;

					foreach (RawShape childShape in TopLevelShapes)
					{
						//childShape.ReCalculateSize(g);
						childShape.Left = leftPos + widthPerChild / 2 - childShape.Width / 2;
						childShape.Top = Gap;
						leftPos += widthPerChild;
						maxBottomTopLevelShapes = Math.Max(maxBottomTopLevelShapes, childShape.Bottom);
					}
				}
				#endregion

				#region Bottom Level Shapes

				if (BottomLevelShapes.Count > 0)
				{
					widthPerChild = this.ClientSize.Width / BottomLevelShapes.Count;

					foreach (RawShape childShape in BottomLevelShapes)
					{
						//childShape.ReCalculateSize(g);
						widthPerChild = Math.Max(widthPerChild, childShape.Width);
					}
					//this.Width = Math.Max(this.Width, widthPerChild * BottomLevelShapes.Count);
					leftPos = 0;

					foreach (RawShape childShape in BottomLevelShapes)
					{
						//childShape.ReCalculateWidth(g);
						childShape.Left = leftPos + widthPerChild / 2 - childShape.Width / 2;
						childShape.Top = this.ClientSize.Height - childShape.Height - Gap;
						leftPos += widthPerChild;

						if (childShape.Left < 0)
							throw new Exception("Less than zero");
					}
				}
				#endregion

				#region Right-Aligned Shapes

				if (RightAlignedShapes.Count > 0)
				{
					//widthPerChild = this.ClientSize.Width / BottomLevelShapes.Count;
					int topPos = string.IsNullOrEmpty(SwimLane4.Heading) ? Gap : 30;
					int rightOffset = string.IsNullOrEmpty(SwimLane4.Heading) ? Gap : 30;

					foreach (RawShape childShape in RightAlignedShapes)
					{
						childShape.ReCalculateSize(g);
						childShape.Left = this.ClientSize.Width - childShape.Width - rightOffset;
						childShape.Top = topPos;
						topPos += childShape.Height + Gap;

						//if (childShape.Top < 0)
						//    throw new Exception("Less than zero");
					}
				}
				#endregion

				#region MainShape
				//int height = Math.Max(1, this.Width / 10);
				//width = height;

				int newWidth;// = width;
				int newHeight;// = height;

				//if (height >= MainShapeMaxHeight)
				newHeight = MainShapeMaxHeight;

				//if (width >= MainShapeMaxWidth)
				newWidth = MainShapeMaxWidth;

				if (KeepMainShapeFull)
				{
					// Make the main shape the same width/height as the sum of the linked shapes, so that all links are straight
					if (TopLevelShapes.Count == 1)
						newWidth = Math.Max(newWidth, TopLevelShapes[0].Width);
					else if (TopLevelShapes.Count > 1)
						newWidth = Math.Max(newWidth, TopLevelShapes[TopLevelShapes.Count - 1].Right - TopLevelShapes[0].Left);

					if (BottomLevelShapes.Count == 1)
						newWidth = Math.Max(newWidth, BottomLevelShapes[0].Width);
					else if (BottomLevelShapes.Count > 1)
						newWidth = Math.Max(newWidth, BottomLevelShapes[BottomLevelShapes.Count - 1].Right - BottomLevelShapes[0].Left);
				}
				if (KeepMainShapeFull)
					MainShape.AutoSizeWidth = false;

				if (MainShape.Height != newHeight)
					MainShape.Height = newHeight;

				if (MainShape.Width != newWidth)
					MainShape.Width = newWidth;

				//if (!KeepMainShapeFull)
				MainShape.ReCalculateSize(g);

				#region Position MainShape
				if ((TopLevelShapes.Count > 0 && BottomLevelShapes.Count > 0) ||
					(TopLevelShapes.Count == 0 && BottomLevelShapes.Count == 0))
				{
					MainShape.Top = this.ClientSize.Height / 2 - MainShape.Height / 2;
					MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
				}
				else if (TopLevelShapes.Count > 0)
				{
					MainShape.Top = this.ClientSize.Height - MainShape.Height - Gap;
					MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
				}
				else if (BottomLevelShapes.Count > 0)
				{
					if (RightAlignedShapes.Count > 0)
						MainShape.Top = 30;
					else
						MainShape.Top = Gap;

					MainShape.Left = this.ClientSize.Width / 2 - MainShape.Width / 2;
				}
				#endregion

				#endregion

				g.Dispose();
			}
			if (BottomLevelShapes.Count > 0)
				this.Height = Math.Max(BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom, this.Height);

			Slyce.Common.Utility.ResumePainting(this.Parent);
			Busy = false;
		}

		private void LayoutShapesInLevelsHorizontal()
		{
			Slyce.Common.Utility.SuspendPainting(this);
			int height = 0;
			int topPos = 0;
			//int gap = 20;

			using (Graphics g = Graphics.FromHwnd(this.Handle))
			{
				#region Top Level Shapes

				if (TopLevelShapes.Count > 0)
				{
					topPos = string.IsNullOrEmpty(SwimLane1.Heading) ? Gap : 30;
					int rightOffset = string.IsNullOrEmpty(SwimLane1.Heading) ? Gap : 30;

					height = ChildShapeMaxHeight;

					int maxWidth = 0;

					foreach (RawShape childShape in TopLevelShapes)
					{
						childShape.AutoSizeWidth = false;
						maxWidth = Math.Max(maxWidth, childShape.ReCalculateSize(g));
					}
					foreach (RawShape childShape in TopLevelShapes)
					{
						childShape.Top = topPos;
						childShape.Left = Gap;
						childShape.Height = ChildShapeMaxHeight;
						childShape.Width = maxWidth;
						topPos = childShape.Bottom + Gap;
					}
				}
				#endregion

				#region Bottom Level Shapes

				if (BottomLevelShapes.Count > 0)
				{
					//topPos = 0;
					topPos = string.IsNullOrEmpty(SwimLane1.Heading) ? Gap : 30;
					int rightOffset = string.IsNullOrEmpty(SwimLane1.Heading) ? Gap : 30;

					int maxWidth = 0;

					foreach (RawShape childShape in BottomLevelShapes)
					{
						childShape.AutoSizeWidth = false;
						maxWidth = Math.Max(maxWidth, childShape.ReCalculateSize(g));
					}
					foreach (RawShape childShape in BottomLevelShapes)
					{
						//childShape.ReCalculateWidth(g);
						childShape.Width = maxWidth;
						childShape.Top = topPos;
						childShape.Left = this.ClientSize.Width - maxWidth - Gap;
						childShape.Height = ChildShapeMaxHeight;
						topPos = childShape.Bottom + Gap;

						//if (childShape.Left < 0)
						//    throw new Exception("Less than zero");
					}
				}
				#endregion

				#region MainShape
				topPos = Gap;

				if (TopLevelShapes.Count > 0)
					topPos = Math.Max(topPos, TopLevelShapes[0].Top);

				if (BottomLevelShapes.Count > 0)
					topPos = Math.Max(topPos, BottomLevelShapes[0].Top);

				MainShape.Height = ChildShapeMaxHeight;

				if (KeepMainShapeFull)
				{
					if (TopLevelShapes.Count > 0)
						MainShape.Height = Math.Max(MainShape.Height, TopLevelShapes[TopLevelShapes.Count - 1].Bottom - MainShape.Top);

					if (BottomLevelShapes.Count > 0)
						MainShape.Height = Math.Max(MainShape.Height, BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom - MainShape.Top);
				}
				MainShape.ReCalculateSize(g);

				if (TopLevelShapes.Count > 0 || KeepMainShapeCentered)
					MainShape.Left = Math.Max(200, this.Width / 2 - MainShape.Width / 2);// leftPos + 20;
				else
					MainShape.Left = 5;

				MainShape.Top = topPos;
				#endregion
			}

			#region Calculate required height of the canvas
			height = MainShape.Bottom;

			if (TopLevelShapes.Count > 0)
				height = Math.Max(height, TopLevelShapes[TopLevelShapes.Count - 1].Bottom);

			if (BottomLevelShapes.Count > 0)
				height = Math.Max(height, BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom);

			//this.Height = height + 5;

			if (height + Gap < Parent.Height)
				this.Height = Parent.Height;
			else
				this.Height = height + Gap;
			//this.Height = Math.Max(this.Height, height + 5);
			#endregion

			Slyce.Common.Utility.ResumePainting(this);
		}

		private void LayoutChildrenInSurroundingCircle(List<RawShape> childEntities)
		{
			if (childEntities.Count == 0)
				return;

			using (Graphics g = Graphics.FromHwnd(this.Handle))
			{
				Slyce.Common.Utility.SuspendPainting(this);
				//int gap = 5;
				int angle = 360 / childEntities.Count;
				int radius = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 2 - MainShape.Height / 2 - Gap;
				Point center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
				Point[] points = ShapeHelper.GetPointsOnCircle(center, radius, childEntities.Count);

				for (int i = 0; i < childEntities.Count; i++)
				{
					childEntities[i].ReCalculateSize(g);
					childEntities[i].Top = points[i].Y - childEntities[i].Height / 2;
					childEntities[i].Left = points[i].X - childEntities[i].Width / 2;
				}
				g.Dispose();
			}
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void LayoutShapesInVerticalSwimLanes()
		{
			Slyce.Common.Utility.SuspendPainting(this);
			int heightPerChild = 0;
			int height = 0;
			int topPos = 40;
			int leftPos = 0;

			using (Graphics g = Graphics.FromHwnd(this.Handle))
			{
				#region Top Level Shapes

				if (TopLevelShapes.Count > 0)
				{
					heightPerChild = this.ClientSize.Height / TopLevelShapes.Count;
					height = Math.Min(heightPerChild - 4, ChildShapeMaxHeight);
					//topPos = 10;

					foreach (RawShape childShape in TopLevelShapes)
					{
						childShape.ReCalculateSize(g);
						childShape.Top = topPos;
						childShape.Left = 10;
						topPos = childShape.Bottom + 10;
						leftPos = Math.Max(leftPos, childShape.Right);
					}
				}
				#endregion

				#region MainShape
				MainShape.ReCalculateSize(g);
				MainShape.Left = leftPos + 20;
				MainShape.Top = topPos;
				topPos = MainShape.Bottom + 10;
				//leftPos = MainShape.Right;
				#endregion

				#region Bottom Level Shapes

				if (BottomLevelShapes.Count > 0)
				{
					heightPerChild = this.ClientSize.Height / BottomLevelShapes.Count;
					//topPos += 10;

					foreach (RawShape childShape in BottomLevelShapes)
					{
						childShape.ReCalculateSize(g);
						childShape.Top = topPos;// +20;
						childShape.Left = MainShape.Right + 20;
						topPos = childShape.Bottom + 10;

						if (childShape.Left < 0)
							throw new Exception("Less than zero");
					}
				}
				#endregion

				#region Level 4 Shapes

				if (RightAlignedShapes.Count > 0)
				{
					topPos = 40;

					//heightPerChild = this.ClientSize.Height / Level4Shapes.Count;
					//topPos = 0;

					foreach (RawShape childShape in RightAlignedShapes)
					{
						childShape.ReCalculateSize(g);
						childShape.Top = topPos;
						childShape.Left = this.Width - childShape.Width - 10;
						topPos = childShape.Bottom + 10;

						if (childShape.Left < 0)
							throw new Exception("Less than zero");
					}
				}
				#endregion
			}
			this.Height = Math.Max(BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom, this.Height);
			Slyce.Common.Utility.ResumePainting(this);
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			DrawLines(e);
		}

		private void DrawLines(PaintEventArgs e)
		{
			if (DiagramType == Layouts.Star && ChildEntities.Count == 0)
				return;

			Graphics g = e.Graphics;

			if (MainShape == null)
			{
				g.Clear(Color.White);
				return;
			}
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			g.Clear(BackColor);
			//int gap = 5;
			int radius = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 2 - MainShape.Height / 2 - Gap;
			Point center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
			Rectangle entityShapeRect = new Rectangle(MainShape.Left - 5, MainShape.Top - 5, MainShape.Width + 10, MainShape.Height + 10);
			Point centerOfShapeEntity = new Point(MainShape.Left + MainShape.Width / 2, MainShape.Top + MainShape.Height / 2);
			int[] yVals;

			#region Draw SwimLanes

			if (DiagramType == Layouts.SwimLanesVertical)
			{
				#region Level 1
				Rectangle parentRect;

				if (SwimLane1.Style == SwimLaneStyle.Styles.Line)
					parentRect = new Rectangle(MainShape.Left - 10, 0, 1, (int)(this.Height * 0.7));
				else
					parentRect = new Rectangle(0, 0, MainShape.Left - 10, this.Height);

				Brush brush = new LinearGradientBrush(parentRect, SwimLane1.BackColor1, SwimLane1.BackColor2, SwimLane1.GradientAngle);
				g.FillRectangle(brush, parentRect);

				if (SwimLane1.Style != SwimLaneStyle.Styles.Fill)
					parentRect = new Rectangle(0, 0, MainShape.Left - 10, this.Height);

				#endregion

				#region Level 2
				Rectangle mainShapRect;

				if (SwimLane2.Style == SwimLaneStyle.Styles.Line)
					mainShapRect = new Rectangle(BottomLevelShapes[0].Left - 10, 0, 1, (int)(this.Height * 0.8));
				else
					mainShapRect = new Rectangle(parentRect.Right - 1, 0, BottomLevelShapes[0].Left - (MainShape.Left - 10) - 10, this.Height);

				brush = new LinearGradientBrush(mainShapRect, SwimLane2.BackColor1, SwimLane2.BackColor2, SwimLane2.GradientAngle);
				g.FillRectangle(brush, mainShapRect);

				if (SwimLane2.Style != SwimLaneStyle.Styles.Fill)
					mainShapRect = new Rectangle(parentRect.Right - 1, 0, BottomLevelShapes[0].Left - (MainShape.Left - 10) - 10, this.Height);

				#endregion

				#region Level 3
				Rectangle childrenRect;

				if (SwimLane3.Style == SwimLaneStyle.Styles.Line)
					childrenRect = new Rectangle(BottomLevelShapes[0].Right + 10, 0, 1, (int)(this.Height * 0.9));
				else
					childrenRect = new Rectangle(mainShapRect.Right - 1, 0, BottomLevelShapes[0].Width + 20, this.Height);

				brush = new LinearGradientBrush(childrenRect, SwimLane3.BackColor1, SwimLane3.BackColor2, SwimLane3.GradientAngle);
				g.FillRectangle(brush, childrenRect);

				if (SwimLane3.Style != SwimLaneStyle.Styles.Fill)
					childrenRect = new Rectangle(mainShapRect.Right - 1, 0, BottomLevelShapes[0].Width + 20, this.Height);

				#endregion

				#region Level 4
				Rectangle level4Rect = new Rectangle(childrenRect.Right - 1, 0, this.Width - childrenRect.Right, this.Height);

				if (SwimLane4.Style != SwimLaneStyle.Styles.Line)
				{
					brush = new LinearGradientBrush(level4Rect, SwimLane4.BackColor1, SwimLane4.BackColor2, SwimLane4.GradientAngle);
					g.FillRectangle(brush, level4Rect);
				}
				#endregion

				#region Headings
				brush = new SolidBrush(SwimLane1.ForeColor);
				Font headingFont = new Font(this.Font, FontStyle.Bold);
				SizeF textSize = g.MeasureString(SwimLane1.Heading, headingFont);
				g.DrawString(SwimLane1.Heading, headingFont, brush, (parentRect.Width - textSize.Width) / 2, 5);

				if (SwimLane2.ForeColor != SwimLane1.ForeColor)
					brush = new SolidBrush(SwimLane2.ForeColor);

				textSize = g.MeasureString(SwimLane2.Heading, headingFont);
				g.DrawString(SwimLane2.Heading, headingFont, brush, mainShapRect.Left + (mainShapRect.Width - textSize.Width) / 2, 5);

				if (SwimLane3.ForeColor != SwimLane2.ForeColor)
					brush = new SolidBrush(SwimLane3.ForeColor);

				textSize = g.MeasureString(SwimLane3.Heading, headingFont);
				g.DrawString(SwimLane3.Heading, headingFont, brush, childrenRect.Left + (childrenRect.Width - textSize.Width) / 2, 5);

				if (SwimLane4.ForeColor != SwimLane3.ForeColor)
					brush = new SolidBrush(SwimLane4.ForeColor);

				textSize = g.MeasureString(SwimLane4.Heading, headingFont);
				g.DrawString(SwimLane4.Heading, headingFont, brush, level4Rect.Left + (level4Rect.Width - textSize.Width) / 2, 5);
				#endregion
			}
			else if (DiagramType == Layouts.ThreeLayerVertical && RightAlignedShapes.Count > 0)
			{
				Rectangle level4Rect;
				int offset = this.ClientSize.Width - RightAlignedShapes[0].Right;

				if (SwimLane4.Style == SwimLaneStyle.Styles.Line)
					level4Rect = new Rectangle(RightAlignedShapes[0].Left - offset, 0, 1, Convert.ToInt32(this.Height * 0.6));
				else
					//throw new NotImplementedException("Not handled yet");
					level4Rect = new Rectangle(RightAlignedShapes[0].Left - offset, 0, this.ClientSize.Width - RightAlignedShapes[0].Left + offset, Convert.ToInt32(this.Height * 0.6));

				Brush brush = new LinearGradientBrush(level4Rect, SwimLane4.BackColor1, SwimLane4.BackColor2, SwimLane4.GradientAngle);
				g.FillRectangle(brush, level4Rect);

				if (SwimLane4.ForeColor != SwimLane3.ForeColor)
					brush = new SolidBrush(SwimLane4.ForeColor);

				Font headingFont = new Font(this.Font, FontStyle.Bold);
				SizeF textSize = g.MeasureString(SwimLane4.Heading, headingFont);
				g.DrawString(SwimLane4.Heading, headingFont, brush, level4Rect.Left + (level4Rect.Width - textSize.Width) / 2, 5);
			}
			else if (DiagramType == Layouts.ThreeLayerHorizontal)
			{
				if (TopLevelShapes.Count > 0)
				{
					Rectangle level4Rect;
					int offset = TopLevelShapes[0].Left;

					if (SwimLane1.Style == SwimLaneStyle.Styles.Line)
						level4Rect = new Rectangle(TopLevelShapes[0].Right + offset, 0, 1, this.Height);
					else
						level4Rect = new Rectangle(0, 0, TopLevelShapes[0].Right + offset, this.Height);

					Rectangle brushRect = level4Rect;
					brushRect.Width += 1;

					GraphicsPath ellipsePath = new GraphicsPath();
					//int ellipseWidth = Convert.ToInt32(TopLevelShapes[0].Right * 1.5);
					int ellipseWidth = 170;// Convert.ToInt32(this.Height / 4);
					int ellipseHeight = Convert.ToInt32(this.Height / 2);
					Rectangle rectEllipse = new Rectangle(-1 * ellipseWidth, -1 * ellipseHeight, 2 * ellipseWidth, 3 * ellipseHeight);
					ellipsePath.AddEllipse(rectEllipse);
					PathGradientBrush pathBrush = new PathGradientBrush(ellipsePath);
					pathBrush.CenterColor = SwimLane1.BackColor1;
					Color[] colors = { SwimLane1.BackColor2 };
					//Color[] colors = { Color.Red };
					pathBrush.SurroundColors = colors;
					g.FillEllipse(pathBrush, rectEllipse);

					Brush brush = new LinearGradientBrush(brushRect, SwimLane1.BackColor1, SwimLane1.BackColor2, SwimLane1.GradientAngle);
					//g.FillRectangle(brush, level4Rect);
					brush = new SolidBrush(SwimLane1.ForeColor);
					Font headingFont = new Font(this.Font, FontStyle.Bold);
					SizeF textSize = g.MeasureString(SwimLane1.Heading, headingFont);
					g.DrawString(SwimLane1.Heading, headingFont, brush, level4Rect.Left + (level4Rect.Width - textSize.Width) / 2, 5);
				}
				if (BottomLevelShapes.Count > 0)
				{
					Rectangle level4Rect;
					int offset = this.ClientSize.Width - BottomLevelShapes[0].Right;

					if (SwimLane3.Style == SwimLaneStyle.Styles.Line)
						level4Rect = new Rectangle(BottomLevelShapes[0].Left - offset, 0, 1, this.Height);
					else
						level4Rect = new Rectangle(BottomLevelShapes[0].Left - offset, 0, this.ClientSize.Width - BottomLevelShapes[0].Left + offset, this.Height);

					Rectangle brushRect = level4Rect;
					brushRect.Width += 1;
					brushRect.Offset(-1, 0);

					GraphicsPath ellipsePath = new GraphicsPath();
					int ellipseWidth = 170;// Convert.ToInt32((this.Width - BottomLevelShapes[0].Left) * 1.5);// Convert.ToInt32(this.Height * 0.8);
					int ellipseHeight = Convert.ToInt32(this.Height / 2);// * 1.2);
					Rectangle rectEllipse = new Rectangle(this.Width - ellipseWidth, -1 * ellipseHeight, 2 * ellipseWidth, 3 * ellipseHeight);
					ellipsePath.AddEllipse(rectEllipse);
					PathGradientBrush pathBrush = new PathGradientBrush(ellipsePath);
					pathBrush.CenterColor = SwimLane3.BackColor1;
					Color[] colors = { SwimLane3.BackColor2 };
					pathBrush.SurroundColors = colors;
					g.FillEllipse(pathBrush, rectEllipse);

					Brush brush = new LinearGradientBrush(brushRect, SwimLane3.BackColor1, SwimLane3.BackColor2, SwimLane3.GradientAngle);
					//g.FillRectangle(brush, level4Rect);
					brush = new SolidBrush(SwimLane3.ForeColor);

					Font headingFont = new Font(this.Font, FontStyle.Bold);
					SizeF textSize = g.MeasureString(SwimLane3.Heading, headingFont);
					g.DrawString(SwimLane3.Heading, headingFont, brush, level4Rect.Left + (level4Rect.Width - textSize.Width) / 2, 5);
				}
			}
			#endregion

			#region Draw Shapes
			MainShape.Draw(g);

			foreach (RawShape shape in TopLevelShapes)
				shape.Draw(g);

			foreach (RawShape shape in BottomLevelShapes)
				shape.Draw(g);

			foreach (RawShape shape in ChildEntities)
				shape.Draw(g);

			foreach (RawShape shape in RightAlignedShapes)
				shape.Draw(g);

			#endregion

			switch (DiagramType)
			{
				case Layouts.Star:
					Point[] points = ShapeHelper.GetPointsOnCircle(center, radius, ChildEntities.Count);

					for (int i = 0; i < ChildEntities.Count; i++)
					{
						if (ChildEntities[i].OriginatingLineStyle != null)
						{
							Point intersectionPoint = ShapeHelper.GetIntersectionPoints(entityShapeRect, centerOfShapeEntity, points[i], 0);
							Point childIntersectionPoint = ShapeHelper.GetIntersectionPoints(ChildEntities[i].Bounds, points[i], centerOfShapeEntity, 5);
							ChildEntities[i].OriginatingLineStyle.BackColor = BackColor;
							ChildEntities[i].OriginatingLineStyle.Draw(g, childIntersectionPoint, intersectionPoint);
						}
					}
					break;
				case Layouts.ThreeLayerVertical:
					int[] xVals = new int[0];

					if (TopLevelShapes.Count > 0)
					{
						xVals = ShapeHelper.GetEqualPointsAlongStraightLine(MainShape.Left, MainShape.Right, TopLevelShapes.Count);

						for (int i = 0; i < TopLevelShapes.Count; i++)
						{
							if (TopLevelShapes[i].OriginatingLineStyle != null)
							{
								Point connection = new Point(TopLevelShapes[i].Left + TopLevelShapes[i].Width / 2, TopLevelShapes[i].Bottom + Gap);
								Point mainShapeConnection;

								if (KeepMainShapeFull)
									mainShapeConnection = new Point(connection.X, MainShape.Top - Gap);
								else
									mainShapeConnection = new Point(xVals[i], MainShape.Top - Gap);

								TopLevelShapes[i].OriginatingLineStyle.BackColor = BackColor;
								TopLevelShapes[i].OriginatingLineStyle.Draw(g, mainShapeConnection, connection);
							}
						}
					}
					if (BottomLevelShapes.Count > 0)
					{
						xVals = ShapeHelper.GetEqualPointsAlongStraightLine(MainShape.Left, MainShape.Right, BottomLevelShapes.Count);

						for (int i = 0; i < BottomLevelShapes.Count; i++)
						{
							if (BottomLevelShapes[i].OriginatingLineStyle != null)
							{
								Point connection = new Point(BottomLevelShapes[i].Left + BottomLevelShapes[i].Width / 2, BottomLevelShapes[i].Top - Gap);
								Point mainShapeConnection;

								if (KeepMainShapeFull)
									mainShapeConnection = new Point(connection.X, MainShape.Bottom + Gap);
								else
									mainShapeConnection = new Point(xVals[i], MainShape.Bottom + Gap);

								BottomLevelShapes[i].OriginatingLineStyle.BackColor = BackColor;
								BottomLevelShapes[i].OriginatingLineStyle.Draw(g, connection, mainShapeConnection);
							}
						}
					}
					if (RightAlignedShapes.Count > 0)
					{
						yVals = ShapeHelper.GetEqualPointsAlongStraightLine(MainShape.Top, MainShape.Bottom, RightAlignedShapes.Count);

						for (int i = 0; i < RightAlignedShapes.Count; i++)
						{
							if (RightAlignedShapes[i].OriginatingLineStyle != null)
							{
								Point connection = new Point(RightAlignedShapes[i].Left - Gap, RightAlignedShapes[i].Top + RightAlignedShapes[i].Height / 2);
								Point mainShapeConnection;

								if (KeepMainShapeFull)
									mainShapeConnection = new Point(connection.Y, MainShape.Right + Gap);
								else
									mainShapeConnection = new Point(MainShape.Right + Gap, yVals[i]);

								RightAlignedShapes[i].OriginatingLineStyle.BackColor = BackColor;
								RightAlignedShapes[i].OriginatingLineStyle.Draw(g, connection, mainShapeConnection);
							}
						}

					}
					break;
				case Layouts.ThreeLayerHorizontal:
					if (TopLevelShapes.Count > 0)
					{
						if (MainShape.Top <= TopLevelShapes[0].Top && MainShape.Bottom >= TopLevelShapes[TopLevelShapes.Count - 1].Bottom)
						{
							// We can draw horizontal lines
							KeepMainShapeFull = true;
							yVals = new int[0];
						}
						else
						{
							int countOfShapesWithLines = 0;
							countOfShapesWithLines = TopLevelShapes.Where(s => s.OriginatingLineStyle != null).Count();
							yVals = ShapeHelper.GetEqualPointsAlongStraightLine(MainShape.Top, MainShape.Bottom, countOfShapesWithLines);
						}
					}
					else
						yVals = new int[0];

					for (int i = 0; i < TopLevelShapes.Count; i++)
					{
						if (TopLevelShapes[i].OriginatingLineStyle != null)
						{
							Point connection = new Point(TopLevelShapes[i].Right + Gap, TopLevelShapes[i].Top + TopLevelShapes[i].Height / 2);
							Point mainShapeConnection;

							if (KeepMainShapeFull)
								mainShapeConnection = new Point(MainShape.Left - Gap, connection.Y);
							else
								mainShapeConnection = new Point(MainShape.Left - Gap, yVals[i]);

							TopLevelShapes[i].OriginatingLineStyle.BackColor = BackColor;
							TopLevelShapes[i].OriginatingLineStyle.Draw(g, mainShapeConnection, connection);
						}
					}
					if (BottomLevelShapes.Count > 0)
					{
						if (MainShape.Top <= BottomLevelShapes[0].Top && MainShape.Bottom >= BottomLevelShapes[BottomLevelShapes.Count - 1].Bottom)
						{
							// We can draw horizontal lines
							KeepMainShapeFull = true;
						}
						else
						{
							int countOfShapesWithLines = BottomLevelShapes.Where(s => s.OriginatingLineStyle != null).Count();
							yVals = ShapeHelper.GetEqualPointsAlongStraightLine(MainShape.Top, MainShape.Bottom, countOfShapesWithLines);
						}
					}
					else
						yVals = new int[0];

					for (int i = 0; i < BottomLevelShapes.Count; i++)
					{
						if (BottomLevelShapes[i].OriginatingLineStyle != null)
						{
							Point connection = new Point(BottomLevelShapes[i].Left - Gap, BottomLevelShapes[i].Top + BottomLevelShapes[i].Height / 2);
							Point mainShapeConnection;

							if (KeepMainShapeFull)
								mainShapeConnection = new Point(MainShape.Right + Gap, connection.Y);
							else
								mainShapeConnection = new Point(MainShape.Right + Gap, yVals[i]);

							BottomLevelShapes[i].OriginatingLineStyle.BackColor = BackColor;
							BottomLevelShapes[i].OriginatingLineStyle.Draw(g, connection, mainShapeConnection);
						}
					}
					break;
				case Layouts.SwimLanesVertical:
					if (TopLevelShapes.Count > 1)
						throw new Exception("SwimLanesVertical can only handle one TopLevel shape.");

					if (TopLevelShapes.Count == 1)
					{
						if (TopLevelShapes[0].OriginatingLineStyle != null)
						{
							Point connection = new Point(TopLevelShapes[0].Left + TopLevelShapes[0].Width / 2, TopLevelShapes[0].Bottom + Gap);
							Point mainShapeConnection = new Point(MainShape.Left - Gap, MainShape.Top + MainShape.Height / 2);
							TopLevelShapes[0].OriginatingLineStyle.BackColor = BackColor;
							TopLevelShapes[0].OriginatingLineStyle.Draw(g, mainShapeConnection, connection);
						}
					}
					if (BottomLevelShapes.Count > 0)
						xVals = ShapeHelper.GetEqualPointsAlongStraightLine(MainShape.Left, MainShape.Right, BottomLevelShapes.Count);
					else
						xVals = new int[0];

					Array.Reverse(xVals);

					for (int i = 0; i < BottomLevelShapes.Count; i++)
					{
						if (BottomLevelShapes[i].OriginatingLineStyle != null)
						{
							Point connection = new Point(BottomLevelShapes[i].Left - Gap, BottomLevelShapes[i].Top + BottomLevelShapes[i].Height / 2);
							Point mainShapeConnection = new Point(xVals[i], MainShape.Bottom + Gap);
							BottomLevelShapes[i].OriginatingLineStyle.BackColor = BackColor;
							BottomLevelShapes[i].OriginatingLineStyle.Draw(g, connection, mainShapeConnection);
						}
					}
					break;
				default:
					throw new NotImplementedException("Not handled yet");
			}
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (MainShape == null ||
				MainShape.GPath == null ||
				BusyPopulating)
				return;

			TempFocusedLineEnd = null;
			bool mustRedraw = false;
			bool oneIsAlreadyFocused = false;
			RawShape shapeWithFocus = null;

			for (int i = 0; i < AllShapes.Count; i++)
			{
				RawShape shape = AllShapes[i];
				ShapeCanvas.LineEndWithFocus tempFocusedLineEnd = null;
				shape.ProcessMouseMove(e, ref mustRedraw, ref oneIsAlreadyFocused, ref tempFocusedLineEnd);

				if (tempFocusedLineEnd != null)
					TempFocusedLineEnd = tempFocusedLineEnd;

				if (shape.HasFocus)
					shapeWithFocus = shape;
			}
			if (mustRedraw)
				Refresh();

			if (shapeWithFocus != null)
			{
				if (Cursor != shapeWithFocus.Cursor)
					Cursor = shapeWithFocus.Cursor;
			}
			else
				Cursor = Cursors.Default;

			FocusedLineEnd = TempFocusedLineEnd;
		}

		//private void SetFocusOnShape(MouseEventArgs e, ref bool isFocused, ref bool mustRedraw, ref bool oneIsAlreadyFocused, ref bool shapeHasFocus, RawShape shape)
		//{
		//    if (shape.GPath == null)
		//        return;

		//    isFocused = false;

		//    shapeHasFocus = shape.GPath.IsVisible(e.X, e.Y);

		//    if (shape.HasFocus != shapeHasFocus)
		//    {
		//        shape.HasFocus = shapeHasFocus;
		//        mustRedraw = true;
		//    }
		//    if (oneIsAlreadyFocused)
		//        isFocused = false;
		//    else if (shape.OriginatingLineStyle != null)
		//        isFocused = shape.OriginatingLineStyle.GPath.IsVisible(e.X, e.Y);

		//    if (shape.OriginatingLineStyle != null)
		//    {
		//        if (shape.OriginatingLineStyle.IsFocused != isFocused)
		//        {
		//            shape.OriginatingLineStyle.IsFocused = isFocused;
		//            mustRedraw = true;

		//            if (shape.OriginatingLineStyle.IsFocused)
		//                shape.OriginatingLineStyle.RaiseMouseEnter(e);
		//            else
		//                shape.OriginatingLineStyle.RaiseMouseLeave(e);
		//        }
		//        if (shape.OriginatingLineStyle.GPathFinishEnd.IsVisible(e.X, e.Y))
		//        {
		//            shape.OriginatingLineStyle.RaiseMouseOverEnd1(e);
		//            TempFocusedLineEnd = new LineEndWithFocus(shape.OriginatingLineStyle, LineEndWithFocus.EndTypes.End);
		//        }
		//        else if (shape.OriginatingLineStyle.GPathStartEnd.IsVisible(e.X, e.Y))
		//        {
		//            shape.OriginatingLineStyle.RaiseMouseOverEnd2(e);
		//            TempFocusedLineEnd = new LineEndWithFocus(shape.OriginatingLineStyle, LineEndWithFocus.EndTypes.Start);
		//        }
		//        if (shape.OriginatingLineStyle.GPathMiddleImage.IsVisible(e.X, e.Y))
		//            shape.OriginatingLineStyle.RaiseMouseOverMiddleImage(e);
		//    }
		//    if (isFocused) oneIsAlreadyFocused = true;
		//}

		private IList<RawShape> AllShapes
		{
			get
			{
				List<RawShape> allShapes = new List<RawShape>();

				allShapes.Add(MainShape);
				allShapes.AddRange(TopLevelShapes);
				allShapes.AddRange(BottomLevelShapes);
				allShapes.AddRange(ChildEntities);
				return allShapes;
			}
		}

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			LinkLine focusedLinkLine = null;
			LinkLine tempLine = null;

			foreach (RawShape shape in AllShapes)
			{
				tempLine = shape.ProcessMouseClick(e);

				if (tempLine != null)
					focusedLinkLine = tempLine;
			}
			DefocusAllLines();

			if (focusedLinkLine != null)
				focusedLinkLine.IsFocused = true;

			Refresh();
		}

		private void DefocusAllLines()
		{
			foreach (RawShape shape in AllShapes)
				shape.DeFocusAllLines();
		}

		private void Canvas_SizeChanged(object sender, EventArgs e)
		{
			Draw();
			Refresh();
		}

		private void ShapeCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			foreach (RawShape shape in AllShapes)
				shape.ProcessDoubleClick(e);
		}

		private void ShapeCanvas_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
				CtrlKeyDown = true;
			else if (e.Shift)
			{
				ShiftKeyDown = true;
				// TODO: remove next line when Shift-selecting of properties is implemented
				CtrlKeyDown = true;
			}
		}

		private void ShapeCanvas_KeyUp(object sender, KeyEventArgs e)
		{
			CtrlKeyDown = false;
			ShiftKeyDown = false;
		}

	}
}
