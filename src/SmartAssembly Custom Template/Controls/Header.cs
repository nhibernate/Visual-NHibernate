using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartAssembly.SmartExceptionsWithAdvancedUI.Controls
{
	public partial class Header : UserControl
	{
		public Header()
		{
			InitializeComponent();
		}

		[Browsable(true)]
		public override string Text
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
	}
}
