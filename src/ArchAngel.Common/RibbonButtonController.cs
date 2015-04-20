using System;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using DevComponents.DotNetBar;
using Slyce.Common;
using RibbonBar = ArchAngel.Interfaces.RibbonBar;

namespace ArchAngel.Common
{
	public class RibbonBarControllerBase : RibbonBarController
	{
		private readonly DoubleLookup<ClickableItem, ButtonItem> Buttons = new DoubleLookup<ClickableItem, ButtonItem>();
		private readonly DoubleLookup<RibbonBarContentItem, RibbonBarBuilder> Builders = new DoubleLookup<RibbonBarContentItem, RibbonBarBuilder>();

		public override void RefreshButtonStatus()
		{
			if (Buttons.Count == 0) return;

			var control = ((Control)Buttons.Pairs.ElementAt(0).Value.ContainerControl);
			if (control.InvokeRequired)
			{
				control.Invoke(new MethodInvoker(RefreshButtonStatus));
				return;
			}

			foreach (var buttonPair in Buttons.Pairs)
			{
				RefreshButton(buttonPair.Key, buttonPair.Value);
			}
		}

		public override void RefreshButtonStatus(RibbonBarContentItem page)
		{
			Control control;
			if (page is Control)
			{
				control = (Control)page;
			}
			else if (Buttons.Count > 0)
			{
				control = ((Control)Buttons.Pairs.ElementAt(0).Value.ContainerControl);
			}
			else return;

			if (control.InvokeRequired)
			{
				control.Invoke(new MethodInvoker(RefreshButtonStatus));
				return;
			}
			try
			{
				var builder = Builders[page];

				foreach (var button in builder.GetAllButtons())
					RefreshButton(button, Buttons[button]);
			}
			catch
			{
				// Do nothing
				string gfh = "";
			}
		}

		public override void RefreshButtonStatus(ClickableItem button)
		{
			var control = ((Control)Buttons[button].ContainerControl);
			if (control.InvokeRequired)
			{
				control.Invoke(new MethodInvoker(RefreshButtonStatus));
				return;
			}

			RefreshButton(button, Buttons[button]);
		}

		private void RefreshButton(ClickableItem button, ButtonItem buttonItem)
		{
			if (button.IsEnabledFunction != null)
			{
				buttonItem.Enabled = button.IsEnabledFunction();
			}
		}

		public override void Clear()
		{
			base.Clear();

			foreach (var pair in Buttons.Pairs)
			{
				pair.Value.Click -= pair.Key.ClickEventHandler;
				pair.Value.CheckedChanged -= pair.Key.CheckedEventHandler;

				var button = pair.Key as RibbonButton;
				if (button != null)
				{
					for (int i = 0; i < button.DropDownItems.Count; i++)
					{
						var subItem = (ButtonItem)pair.Value.SubItems[i];
						subItem.Click -= button.DropDownItems[i].ClickEventHandler;
						subItem.CheckedChanged -= button.DropDownItems[i].CheckedEventHandler;
					}
				}
			}

			Buttons.Clear();
			Builders.Clear();
		}

		public void ProcessRibbonBarButtons(RibbonBarContentItem item, RibbonPanel panel)
		{
			if (item == null) return;

			RibbonBarBuilder builder = new RibbonBarBuilder(this);
			item.AddRibbonBarButtons(builder);

			Builders.Add(item, builder);

			foreach (RibbonBar obj in builder.RibbonBars)
			{
				var group = ProcessGroup(obj);
				panel.Controls.Add(group);
			}
		}

		private DevComponents.DotNetBar.RibbonBar ProcessGroup(RibbonBar bar)
		{
			DevComponents.DotNetBar.RibbonBar groupPanel = new DevComponents.DotNetBar.RibbonBar();
			groupPanel.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			groupPanel.Text = bar.Name;
			groupPanel.LayoutOrientation = bar.Orientation == Orientation.Horizontal ? eOrientation.Horizontal : eOrientation.Vertical;

			foreach (var button in bar.Buttons)
			{
				var bi = ProcessButton(button);
				groupPanel.Items.Add(bi);
			}

			return groupPanel;
		}

		private ButtonItem ProcessButton(RibbonButton button)
		{
			ButtonItem buttonItem = new ButtonItem();
			buttonItem.Name = buttonItem.Text = button.Text;
			if (button.Image != null)
				buttonItem.Image = button.Image;

			buttonItem.Tooltip = button.ToolTip;

			buttonItem.ButtonStyle = GetButtonStyle(button.DisplayType);
			buttonItem.ImagePosition = GetImageLocation(button.ImageLocation);
			buttonItem.AutoCheckOnClick = button.IsCheckable;
			if (button.IsCheckable)
				buttonItem.Checked = button.CheckedState;

			buttonItem.CheckedChanged += button.CheckedEventHandler;
			buttonItem.Click += button.ClickEventHandler;

			foreach (var dropDownItem in button.DropDownItems)
			{
				ButtonItem dropDownButton = new ButtonItem();
				dropDownButton.Text = dropDownItem.Text;
				dropDownButton.Tooltip = dropDownItem.ToolTip;

				dropDownButton.Click += dropDownItem.ClickEventHandler;
				dropDownButton.CheckedChanged += button.CheckedEventHandler;

				dropDownButton.AutoCheckOnClick = button.IsCheckable;
				if (button.IsCheckable)
					dropDownButton.Checked = button.CheckedState;

				buttonItem.SubItems.Add(dropDownButton);
			}

			Buttons[button] = buttonItem;

			return buttonItem;
		}

		private eImagePosition GetImageLocation(ImageDisplayLocation location)
		{
			switch (location)
			{
				case ImageDisplayLocation.Top:
					return eImagePosition.Top;
				case ImageDisplayLocation.Bottom:
					return eImagePosition.Bottom;
				case ImageDisplayLocation.Left:
					return eImagePosition.Left;
				case ImageDisplayLocation.Right:
					return eImagePosition.Right;
				default:
					throw new ArgumentOutOfRangeException("location");
			}
		}

		private eButtonStyle GetButtonStyle(ButtonDisplayType type)
		{
			switch (type)
			{
				case ButtonDisplayType.Text:
					return eButtonStyle.TextOnlyAlways;
				case ButtonDisplayType.Image:
					return eButtonStyle.Default;
				case ButtonDisplayType.TextAndImage:
					return eButtonStyle.ImageAndText;
				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}
	}
}