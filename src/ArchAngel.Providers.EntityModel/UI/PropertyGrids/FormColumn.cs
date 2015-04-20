using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormColumn : UserControl, IColumnForm, IEventSender
	{
		public FormColumn()
		{
			InitializeComponent();

			textBoxName.TextChanged +=					(sender, e) => ColumnNameChanged.RaiseEventEx(this);
			numEditScale.ValueChanged +=				(sender, e) => ColumnScaleChanged.RaiseEventEx(this);
			numEditCharMaxLength.ValueChanged +=		(sender, e) => ColumnSizeChanged.RaiseEventEx(this);
			textboxDataType.TextChanged +=				(sender, e) => DatatypeChanged.RaiseEventEx(this);
			textBoxDefault.TextChanged +=				(sender, e) => DefaultChanged.RaiseEventEx(this);
			textBoxDescription.TextChanged +=			(sender, e) => DescriptionChanged.RaiseEventEx(this);
			checkBoxIsNullable.CheckedChanged +=		(sender, e) => IsNullableChanged.RaiseEventEx(this);
			integerInputOrdinalPosition.ValueChanged +=	(sender, e) => OrdinalPositionChanged.RaiseEventEx(this);
			numEditPrecision.ValueChanged +=			(sender, e) => PrecisionChanged.RaiseEventEx(this);
		}

		public event EventHandler ColumnNameChanged;
		public event EventHandler ColumnScaleChanged;
		public event EventHandler ColumnSizeChanged;
		public event EventHandler ColumnSizeIsMaxChanged;
		public event EventHandler DatatypeChanged;
		public event EventHandler DefaultChanged;
		public event EventHandler DescriptionChanged;
		public event EventHandler IsNullableChanged;
		public event EventHandler OrdinalPositionChanged;
		public event EventHandler PrecisionChanged;
		public event EventHandler DeleteColumn;

		public string ColumnName
		{
			get { return textBoxName.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxName.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public string Description
		{
			get { return textBoxDescription.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxDescription.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public bool IsNullable
		{
			get { return checkBoxIsNullable.Checked; }
			set
			{
				EventRaisingDisabled = true;
				checkBoxIsNullable.Checked = value;
				EventRaisingDisabled = false;
			}
		}

		public string Datatype
		{
			get { return textboxDataType.Text; }
			set
			{
				EventRaisingDisabled = true;
				textboxDataType.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public string Default
		{
			get { return textBoxDefault.Text; }
			set
			{
				EventRaisingDisabled = true;
				textBoxDefault.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public int OrdinalPosition
		{
			get { return integerInputOrdinalPosition.Value; }
			set
			{
				EventRaisingDisabled = true;
				integerInputOrdinalPosition.Value = value;
				EventRaisingDisabled = false;
			}
		}

		public int ColumnSize
		{
			get { return numEditCharMaxLength.Value; }
			set
			{
				EventRaisingDisabled = true;
				numEditCharMaxLength.Value = value;
				EventRaisingDisabled = false;
			}
		}

		public bool ColumnSizeIsMax
		{
			get { return checkBoxSizeIsMax.Checked; }
			set
			{
				EventRaisingDisabled = true;
				checkBoxSizeIsMax.Checked = value;
				EventRaisingDisabled = false;
			}
		}

		public int Precision
		{
			get { return numEditPrecision.Value; }
			set
			{
				EventRaisingDisabled = true;
				numEditPrecision.Value = value;
				EventRaisingDisabled = false;
			}
		}

		public int ColumnScale
		{
			get { return numEditScale.Value; }
			set
			{
				EventRaisingDisabled = true;
				numEditScale.Value = value;
				EventRaisingDisabled = false;
			}
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

        public void RefreshVirtualProperties()
        {
            virtualPropertyGrid1.RefreshVisibilities();
        }

		public void Clear() { }

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		#region Implementation of IEventSender

		public bool EventRaisingDisabled { get; set; }

		#endregion

		private void deleteColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteColumn.RaiseEventEx(this);
		}

		private void checkBoxSizeIsMax_CheckedChanged(object sender, EventArgs e)
		{
			numEditCharMaxLength.Enabled = !checkBoxSizeIsMax.Checked;
			ColumnSizeIsMaxChanged.RaiseEventEx(this);
		}
	}
}