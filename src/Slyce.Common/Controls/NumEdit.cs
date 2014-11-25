using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

// See: http://www.codeproject.com/cs/miscctrl/numedit.asp
namespace Slyce.Common.Controls
{
	/// <summary>
	/// Numeric data entry control
	/// </summary>
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public class NumEdit : TextBox
	{
		private NumEditType m_inpType;
		//		private double m_min;
		//		private double m_max;
		//		private bool m_minSet;
		//		private bool m_maxSet;

		public enum NumEditType
		{
			Currency,
			Decimal,
			Single,
			Double,
			SmallInteger,
			Integer,
			LargeInteger
		}

		public NumEdit()
		{
			// set min/max
			//			m_minSet = false;
			//			m_maxSet = false;

			// set default input type
			this.InputType = NumEditType.Integer;

			// NOTE: Existing context menu allows Paste command, with no known
			//	method to intercept. Only option is to reset to empty.
			//	(setting to Null doesn't work)
			this.ContextMenu = new ContextMenu();
		}

		[Description("Sets the numeric type allowed"), Category("Behavior")]
		public NumEditType InputType
		{
			get { return m_inpType; }
			set
			{
				m_inpType = value;
				//				if(!m_minSet)
				//					this.Min = Double.MinValue;
				//				if(!m_maxSet)
				//					this.Max = Double.MaxValue;
			}
		}

		//		[Description("Sets the Minimum allowed value"), Category("Behavior")]
		//		public double Min
		//		{
		//			get{return m_min;}
		//			set{m_min = CheckMin(value);}
		//		}

		//		[Description("Sets the Maximum allowed value"), Category("Behavior")]
		//		public double Max
		//		{
		//			get{return m_max;}
		//			set{m_max = CheckMax(value);}
		//		}

		public override string Text
		{
			get { return base.Text; }
			set
			{
				if (IsValid(value, true))
					base.Text = value;
			}
		}

		private bool IsValid(string val, bool user)
		{
			// this method validates the ENTIRE string
			//	not each character
			// Rev 1: Added bool user param. This bypasses preliminary checks
			//	that allow -, this is used by the OnLeave event
			//	to prevent
			bool ret = true;

			if (val == null ||
				val.Equals("") ||
				val.Equals(String.Empty))
			{
				return ret;
			}
			if (user)
			{
				// allow first char == '-'
				if (val.Equals("-"))
					return ret;
			}

			//			if(Min < 0 && val.Equals("-"))
			//				return ret;

			// parse into dataType, errors indicate invalid value
			// NOTE: parsing also validates data type min/max
			try
			{
				switch (m_inpType)
				{
					case NumEditType.Currency:
						decimal dec = decimal.Parse(val);
						int pos = val.IndexOf(".");
						if (pos != -1)
							ret = val.Substring(pos).Length <= 3;	// 2 decimals + "."
						//ret &= Min <= (double)dec && (double)dec <= Max;
						break;
					case NumEditType.Single:
						float flt;// = float.Parse(val);
						return float.TryParse(val, out flt);
					//ret &= Min <= flt && flt <= Max;
					case NumEditType.Double:
						double dbl;// = double.Parse(val);
						return double.TryParse(val, out dbl);
					//ret &= Min <= dbl && dbl <= Max;
					case NumEditType.Decimal:
						decimal dec2;// = decimal.Parse(val);
						return decimal.TryParse(val, out dec2);
					//ret &= Min <= (double)dec2 && (double)dec2 <= Max;
					case NumEditType.SmallInteger:
						short s;// = short.Parse(val);
						return short.TryParse(val, out s);
					//ret &= Min <= s && s <= Max;
					case NumEditType.Integer:
						int i;// = int.Parse(val);
						return int.TryParse(val, out i);
					//ret &= Min <= i && i <= Max;
					case NumEditType.LargeInteger:
						long l;// = long.Parse(val);
						return long.TryParse(val, out l);
					//ret &= Min <= l && l <= Max;
					default:
						throw new ApplicationException();
				}
			}
			catch
			{
				ret = false;
			}
			return ret;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// trap Ctrl-V paste and prevent invalid values
			//	return false to allow further processing
			if (keyData == (Keys)Shortcut.CtrlV || keyData == (Keys)Shortcut.ShiftIns)
			{
				IDataObject iData = Clipboard.GetDataObject();

				// assemble new string and check IsValid
				string newText;
				newText = base.Text.Substring(0, base.SelectionStart)
					+ (string)iData.GetData(DataFormats.Text)
					+ base.Text.Substring(base.SelectionStart + base.SelectionLength);

				// check if data to be pasted is convertable to inputType
				if (!IsValid(newText, true))
					return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override void OnLeave(EventArgs e)
		{
			// handle - and leading zeros input since KeyPress handler must allow this
			if (base.Text != "")
			{
				if (!IsValid(base.Text, false))
					base.Text = "";
				else if (Double.Parse(base.Text) == 0)	// this used for -0, 000 and other strings
					base.Text = "0";
			}
			base.OnLeave(e);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			// assemble new text with new KeyStroke
			//	and pass to validation routine.

			// NOTES;
			//	1) Delete key is NOT passed here
			//	2) control passed here after ProcessCmdKey() is run

			char c = e.KeyChar;
			if (!Char.IsControl(c))	// not sure about this?? nothing in docs about what is Control char??
			{
				// prevent spaces
				if (c.ToString() == " ")
				{
					e.Handled = true;
					return;
				}

				string newText = base.Text.Substring(0, base.SelectionStart)
					+ c.ToString() + base.Text.Substring(base.SelectionStart + base.SelectionLength);

				if (!IsValid(newText, true))
					e.Handled = true;
			}
			base.OnKeyPress(e);
		}

		//		private double CheckMin(double Min)
		//		{
		//			double ret;
		//			switch(m_inpType)
		//			{
		//				case NumEditType.Currency:
		//					ret = (double)Decimal.MinValue <= Min ? Min : (double)Decimal.MinValue;
		//					break;
		//				case NumEditType.Single:
		//					ret = Single.MinValue <= Min ? Min : Single.MinValue;
		//					break;
		//				case NumEditType.Double:
		//					ret = Double.MinValue <= Min ? Min : Double.MinValue;
		//					break;
		//				case NumEditType.Decimal:
		//					ret = (double)Decimal.MinValue <= Min ? Min : (double)Decimal.MinValue;
		//					break;
		//				case NumEditType.SmallInteger:
		//					ret = Int16.MinValue <= Min ? Min : Int16.MinValue;
		//					break;
		//				case NumEditType.Integer:
		//					ret = Int32.MinValue <= Min ? Min : Int32.MinValue;
		//					break;
		//				case NumEditType.LargeInteger:
		//					ret = Int64.MinValue <= Min ? Min : Int64.MinValue;
		//					break;
		//				default:
		//					throw new ApplicationException();
		//			}
		//			return ret;
		//		}

		//		private double CheckMax(double Max)
		//		{
		//			double ret;
		//			switch(m_inpType)
		//			{
		//				case NumEditType.Currency:
		//					ret = (double)Decimal.MaxValue >= Max ? Max : (double)Decimal.MaxValue;
		//					break;
		//				case NumEditType.Single:
		//					ret = Single.MinValue >= Max ? Max : Single.MaxValue;
		//					break;
		//				case NumEditType.Double:
		//					ret = Double.MinValue >= Max ? Max : Double.MaxValue;
		//					break;
		//				case NumEditType.Decimal:
		//					ret = (double)Decimal.MinValue >= Max ? Max : (double)Decimal.MaxValue;
		//					break;
		//				case NumEditType.SmallInteger:
		//					ret = Int16.MinValue >= Max ? Max : Int16.MaxValue;
		//					break;
		//				case NumEditType.Integer:
		//					ret = Int32.MinValue >= Max ? Max : Int32.MaxValue;
		//					break;
		//				case NumEditType.LargeInteger:
		//					ret = Int64.MinValue >= Max ? Max : Int64.MaxValue;
		//					break;
		//				default:
		//					throw new ApplicationException();
		//			}
		//			return ret;
		//		}
	}
}
