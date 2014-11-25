using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	public partial class FloatingToolstrip : UserControl
	{
		public enum LayoutStyles
		{
			Horizontal,
			Vertical
		}
		public class MenuItem
		{
			public event EventHandler Click;
			public Image Image;
			public Image HoverImage;
			public string TooltipText;
			public bool Selected = false;
			public FloatingToolstrip Parent;

			public MenuItem(string tooltipText, Image image, Image hoverImage, EventHandler clickHandler, FloatingToolstrip parent)
			{
				TooltipText = tooltipText;
				Image = image;
				HoverImage = hoverImage;
				Click = clickHandler;
				Parent = parent;
			}

			internal void RaiseClick(object tag)
			{
				if (Click != null)
				{
					Tag = tag;
					Click(this, null);
				}
			}

			public object Tag { get; set; }
		}
		public int Offset = 3;
		private int Gap = 2;
		private LayoutStyles _LayoutStyle = LayoutStyles.Horizontal;
		private ToolTip tooltip = new ToolTip();
		private Timer TooltipTimer = new Timer();
		public Point StartPoint;

		public FloatingToolstrip()
		{
			InitializeComponent();

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);

			Items = new List<MenuItem>();
			ItemRectangles = new List<Rectangle>();

			TooltipTimer.Interval = 200;
			TooltipTimer.Tick += new EventHandler(TooltipTimer_Tick);
		}

		public object Tag { get; set; }

		void TooltipTimer_Tick(object sender, EventArgs e)
		{
			TooltipTimer.Stop();

			MenuItem item = (MenuItem)TooltipTimer.Tag;

			if (item.Selected)
			{
				for (int i = 0; i < Items.Count; i++)
				{
					if (Items[i] == item)
					{
						tooltip.Show(item.TooltipText, this, new Point(ItemRectangles[i].Right, ItemRectangles[i].Bottom));
					}
				}
			}
		}

		public List<MenuItem> Items { get; set; }
		public List<Rectangle> ItemRectangles { get; set; }

		public LayoutStyles LayoutStyle
		{
			get { return _LayoutStyle; }
			set { _LayoutStyle = value; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			ItemRectangles.Clear();
			Graphics g = e.Graphics;

			if (LayoutStyle == LayoutStyles.Vertical)
			{
				Point pt = new Point(Gap, 0);
				Rectangle itemBackgroundRectangle = new Rectangle(pt, new Size(16 + Offset * 2, 16 + Offset * 2));
				Rectangle itemImageRectangle = new Rectangle(pt.X + Offset, pt.Y + Offset, 16, 16);
				Brush bgBrushSelected = new SolidBrush(Color.FromArgb(80, 80, 80));

				for (int i = 0; i < Items.Count; i++)
				{
					MenuItem item = Items[i];
					ItemRectangles.Add(itemBackgroundRectangle);

					if (item.Selected)
					{
						g.FillRectangle(bgBrushSelected, itemBackgroundRectangle);
						g.DrawImage(item.HoverImage, itemImageRectangle);
					}
					else
					{
						//g.FillRectangle(bgBrush, itemBackgroundRectangle);
						g.DrawImage(item.Image, itemImageRectangle);
					}
					itemBackgroundRectangle.Y += itemBackgroundRectangle.Height;
					itemImageRectangle.Y = itemBackgroundRectangle.Y + 2;
				}
				this.Height = ItemRectangles[ItemRectangles.Count - 1].Bottom + Gap * 2;
				this.Width = ItemRectangles[0].Width + Gap * 2;
			}
			else if (LayoutStyle == LayoutStyles.Horizontal)
			{
				Point pt = new Point(0, 0);
				Rectangle itemBackgroundRectangle = new Rectangle(pt, new Size(16 + Offset * 2, 16 + Offset * 2));
				Rectangle itemImageRectangle = new Rectangle(pt.X + Offset, pt.Y + Offset, 16, 16);
				Brush bgBrushSelected = new SolidBrush(Color.FromArgb(80, 80, 80));

				for (int i = 0; i < Items.Count; i++)
				{
					MenuItem item = Items[i];
					ItemRectangles.Add(itemBackgroundRectangle);

					if (item.Selected)
					{
						g.FillRectangle(bgBrushSelected, itemBackgroundRectangle);
						g.DrawImage(item.HoverImage, itemImageRectangle);
					}
					else
					{
						//g.FillRectangle(bgBrush, itemBackgroundRectangle);
						g.DrawImage(item.Image, itemImageRectangle);
					}
					itemBackgroundRectangle.X += itemBackgroundRectangle.Width;
					itemImageRectangle.X = itemBackgroundRectangle.X + 2;
				}
				this.Height = ItemRectangles[0].Height + Gap * 2;
				this.Width = ItemRectangles[ItemRectangles.Count - 1].Right + Gap * 2;
				//this.Top = StartPoint.Y - this.Height;
				//this.Left = StartPoint.X;
			}
		}

		private void FloatingToolstrip_MouseMove(object sender, MouseEventArgs e)
		{
			bool oneIsSelected = false;
			bool selected = false;
			bool selectionChanged = false;

			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].Selected && !ItemRectangles[i].Contains(e.Location))
				{
					TooltipTimer.Stop();
					tooltip.Hide(this);
				}
			}
			for (int i = 0; i < ItemRectangles.Count; i++)
			{
				selected = ItemRectangles[i].Contains(e.Location);

				if (selected)
					oneIsSelected = true;

				if (Items[i].Selected != selected)
				{
					Items[i].Selected = selected;
					selectionChanged = true;

					if (selected)
					{
						TooltipTimer.Tag = Items[i];
						TooltipTimer.Start();
					}
				}
			}
			if (!oneIsSelected)
				tooltip.Hide(this);

			if (selectionChanged)
				this.Refresh();
		}

		private void FloatingToolstrip_MouseClick(object sender, MouseEventArgs e)
		{
			for (int i = 0; i < ItemRectangles.Count; i++)
			{
				if (ItemRectangles[i].Contains(e.Location))
				{
					Items[i].RaiseClick(Tag);
					return;
				}
			}
		}

		private void FloatingToolstrip_MouseLeave(object sender, EventArgs e)
		{
			tooltip.Hide(this);

			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].Selected)
				{
					Items[i].Selected = false;
					this.Refresh();
				}
			}

		}
	}
}
