using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Interfaces
{
	public interface RibbonBarContentItem
	{
		void AddRibbonBarButtons(RibbonBarBuilder builder);
	}

	public delegate void CheckedStateHandler(object sender, bool NewCheckState);

	public class RibbonBarBuilder
	{
		private readonly List<RibbonBar> objects = new List<RibbonBar>();
		public RibbonBarController Controller { get; private set; }

		public RibbonBarBuilder(RibbonBarController controller)
		{
			Controller = controller;
		}

		public IEnumerable<RibbonBar> RibbonBars
		{
			get { return objects; }
		}

        public RibbonBarBuilder AddRibbonBar(RibbonBar obj)
		{
			objects.Add(obj);
            return this;
		}

        public RibbonBarBuilder InsertRibbonBar(int index, RibbonBar obj)
		{
			objects.Insert(index, obj);
            return this;
		}

        public RibbonButton CreateButton()
        {
            return new RibbonButton(Controller);
        }

		public RibbonBar CreateBar()
        {
            return new RibbonBar();
        }

		public DropDownItem CreateDropDownItem()
		{
			return new DropDownItem(Controller);
		}

		public IEnumerable<RibbonButton> GetAllButtons()
		{
			foreach(var bar in RibbonBars)
			{
				foreach (var button in bar.Buttons)
					yield return button;
			}
		}
	}

	public class DropDownItem : ClickableItem
	{
		public DropDownItem(RibbonBarController controller) : base(controller)
		{
			controller.AddClickableItemToWatch(this);
		}

		public DropDownItem AddCheckEventHandler(CheckedStateHandler handler)
		{
			CheckedEventHandlers.Add(handler); return this;
		}

		public DropDownItem AddEventHandler(EventHandler handler)
		{
			ClickEventHandlers.Add(handler); return this;
		}

		public DropDownItem SetIsCheckable(bool isCheckable) { IsCheckable = isCheckable; return this; }
		public DropDownItem SetIsChecked(bool isChecked) { CheckedState = isChecked; return this; }
		public DropDownItem SetText(string text) { Text = text; return this; }
		public DropDownItem SetToolTip(string text) { ToolTip = text; return this; }
		public DropDownItem SetIsEnabledHandler(Func<bool> handler) { IsEnabledFunction = handler; return this; }
	}

	public class ClickableItem
	{
		protected readonly RibbonBarController controller;
		protected readonly List<EventHandler> ClickEventHandlers = new List<EventHandler>();
		protected readonly List<CheckedStateHandler> CheckedEventHandlers = new List<CheckedStateHandler>();

		internal ClickableItem(RibbonBarController controller)
		{
			this.controller = controller;
		}

		public string Text { get; set; }
		public string ToolTip { get; set; }
		public bool IsCheckable { get; set; }
		public bool CheckedState { get; set; }
		public Func<bool> IsEnabledFunction { get; set; }

		public void ClickEventHandler(object sender, EventArgs e)
		{
			foreach (var handler in ClickEventHandlers)
				handler(this, e);
			controller.RefreshButtonStatus();
		}

		public void CheckedEventHandler(object sender, EventArgs e)
		{
			CheckedState = !CheckedState;

			foreach (var handler in CheckedEventHandlers)
				handler(this, CheckedState);
			controller.RefreshButtonStatus();
		}
	}

	public class RibbonButton : ClickableItem
	{		
		public Image Image { get; set; }
		public ButtonDisplayType DisplayType { get; set; }
		public ImageDisplayLocation ImageLocation { get; set; }
		public List<ClickableItem> DropDownItems { get; set; }

		internal RibbonButton(RibbonBarController controller) : base(controller)
		{
			controller.AddClickableItemToWatch(this);
			DropDownItems = new List<ClickableItem>();

			DisplayType = ButtonDisplayType.TextAndImage;
			ImageLocation = ImageDisplayLocation.Top;
		}

		public RibbonButton AddClickEventHandler(EventHandler handler)
		{
			ClickEventHandlers.Add(handler); return this;
		}

		public RibbonButton AddCheckEventHandler(CheckedStateHandler handler)
		{
			CheckedEventHandlers.Add(handler); return this;
		}

		public RibbonButton SetIsCheckable(bool isCheckable) { IsCheckable = isCheckable; return this; }
		public RibbonButton SetIsChecked(bool isChecked) { CheckedState = isChecked; return this; }
		public RibbonButton SetText(string text) { Text = text; return this; }
		public RibbonButton SetImage(Image image) { Image = image; return this; }
		public RibbonButton SetToolTip(string toolTip) { ToolTip = toolTip; return this; }
		public RibbonButton SetIsEnabledHandler(Func<bool> handler) { IsEnabledFunction = handler; return this; }
		public RibbonButton SetDisplayType(ButtonDisplayType type) { DisplayType = type; return this; }
		public RibbonButton SetImageLocation(ImageDisplayLocation location) { ImageLocation = location; return this; }
		public RibbonButton AddDropDownItem(ClickableItem item) { DropDownItems.Add(item); return this; }
	}

	public enum ButtonDisplayType
	{
		Text, Image, TextAndImage
	}

	public enum ImageDisplayLocation
	{
		Top, Bottom, Left, Right
	}

	public class RibbonBar
	{
		public string Name { get; set; }
		public Orientation Orientation { get; set; }
		public List<RibbonButton> Buttons { get; private set; }

		internal RibbonBar()
		{
			Buttons = new List<RibbonButton>();
			Orientation = Orientation.Horizontal;
		}

        public RibbonBar SetName(string name) { Name = name; return this; }
        public RibbonBar SetOrientation(Orientation orientation) { Orientation = orientation; return this; }
        public RibbonBar AddButton(RibbonButton button) { Buttons.Add(button); return this; }
	}

	public abstract class RibbonBarController
	{
		private readonly List<ClickableItem> buttons = new List<ClickableItem>();
		public void AddClickableItemToWatch(ClickableItem button) { buttons.Add(button); }
		public virtual void Clear()
		{
			buttons.Clear();
		}

		public abstract void RefreshButtonStatus();
		public abstract void RefreshButtonStatus(ClickableItem button);
		public abstract void RefreshButtonStatus(RibbonBarContentItem page);
	}
}