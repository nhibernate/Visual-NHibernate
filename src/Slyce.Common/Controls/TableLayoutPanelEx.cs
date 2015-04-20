using System.ComponentModel;

namespace Slyce.Common.Controls
{
	public partial class TableLayoutPanelEx : System.Windows.Forms.TableLayoutPanel
	{
		public TableLayoutPanelEx()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
		}

		public TableLayoutPanelEx(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
			this.DoubleBuffered = true;
		}
	}
}
