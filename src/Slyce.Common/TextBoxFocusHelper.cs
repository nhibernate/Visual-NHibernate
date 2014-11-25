using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Slyce.Common
{
	public class TextBoxFocusHelper
	{
		DateTime LastTimeEntered = DateTime.Now;
		private Dictionary<Control, bool> AlreadyFocused = new Dictionary<Control, bool>();

		public TextBoxFocusHelper(Control[] textboxes)
		{
			foreach (Control tb in textboxes)
			{
				tb.Enter += new EventHandler(tb_Enter);
				tb.Leave += new EventHandler(tb_Leave);
				tb.MouseUp += new MouseEventHandler(tb_MouseUp);
				AlreadyFocused.Add(tb, false);
			}
		}

		public void Add(Control textBox)
		{
			if (!AlreadyFocused.ContainsKey(textBox))
			{
				textBox.Enter += new EventHandler(tb_Enter);
				textBox.Leave += new EventHandler(tb_Leave);
				textBox.MouseUp += new MouseEventHandler(tb_MouseUp);
				AlreadyFocused.Add(textBox, false);
			}
		}

		public void Remove(Control textBox)
		{
			if (AlreadyFocused.ContainsKey(textBox))
			{
				textBox.Enter -= tb_Enter;
				textBox.Leave -= tb_Leave;
				textBox.MouseUp -= tb_MouseUp;
				AlreadyFocused.Remove(textBox);
			}
		}

		void tb_MouseUp(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;
			double diff = DateTime.Now.Subtract(LastTimeEntered).TotalMilliseconds;

			if (control is TextBox)
			{
				TextBox textBox = (TextBox)control;

				if (diff < 200 && textBox.SelectionLength == 0)
				{
					AlreadyFocused[textBox] = true;
					textBox.SelectAll();
				}
			}
			else if (control is ActiproSoftware.SyntaxEditor.SyntaxEditor)
			{
				ActiproSoftware.SyntaxEditor.SyntaxEditor editor = (ActiproSoftware.SyntaxEditor.SyntaxEditor)control;

				if (diff < 200 && editor.SelectedView.SelectedText.Length == 0)
				{
					AlreadyFocused[editor] = true;
					editor.SelectedView.Selection.SelectAll();
				}
			}
			else
				throw new NotImplementedException("Control-type not handled yet: " + control.GetType().FullName);
		}

		void tb_Leave(object sender, EventArgs e)
		{
			AlreadyFocused[(Control)sender] = false;
		}

		void tb_Enter(object sender, EventArgs e)
		{
			// Select all text only if the mouse isn't down.
			// This makes tabbing to the textbox give focus.
			if (!AlreadyFocused[(Control)sender])
				LastTimeEntered = DateTime.Now;
		}
	}
}
