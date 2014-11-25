using System;
using System.ComponentModel;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class ColumnPresenter : PresenterBase
	{
		public IColumn Column { get; private set; }

		private readonly IColumnForm form;

		public ColumnPresenter(IMainPanel panel, IColumnForm form)
			: base(panel)
		{
			this.form = form;
			SetupForm();

			form.ColumnNameChanged +=		(sender, e) => { if (!Detached) Column.Name			= form.ColumnName; };
			form.ColumnScaleChanged +=		(sender, e) => { if (!Detached) Column.Scale		= form.ColumnScale; };
			form.DatatypeChanged +=			(sender, e) => { if (!Detached) Column.Datatype		= form.Datatype; };
			form.DefaultChanged +=			(sender, e) => { if (!Detached) Column.Default		= form.Default; };
			form.DescriptionChanged +=		(sender, e) => { if (!Detached) Column.Description	= form.Description; };
			form.IsNullableChanged +=		(sender, e) => { if (!Detached) Column.IsNullable	= form.IsNullable; };
			form.OrdinalPositionChanged +=	(sender, e) => { if (!Detached) Column.OrdinalPosition = form.OrdinalPosition; };
			form.PrecisionChanged +=		(sender, e) => { if (!Detached) Column.Precision	= form.Precision; };

			form.ColumnSizeChanged += Form_SizeChanged;
			form.ColumnSizeIsMaxChanged += Form_SizeChanged;
			form.DeleteColumn += Form_DeleteColumn;
		}

		private void Form_SizeChanged(object sender, EventArgs e)
		{
			if (!Detached)
			{
				Column.SizeIsMax = form.ColumnSizeIsMax;
				Column.Size = form.ColumnSize;
			}
		}

		private void Form_DeleteColumn(object sender, EventArgs e)
		{
			if (Detached) return;

			Column.DeleteSelf();
			mainPanel.ShowPropertyGrid(null);
		}

		private void SetupForm()
		{
			if (Detached)
			{
				mainPanel.ShowPropertyGrid(null);
				return;
			}

			form.Clear();
			form.ColumnName = Column.Name;
			form.ColumnScale = Column.Scale;
			form.ColumnSize = Column.SizeIsMax ? 0 : Column.Size;
			form.ColumnSizeIsMax = Column.SizeIsMax;
			form.Datatype = Column.Datatype;
			form.Default = Column.Default;
			form.Description = Column.Description;
			form.IsNullable = Column.IsNullable;
			form.OrdinalPosition = Column.OrdinalPosition;
			form.Precision = Column.Precision;
			form.SetVirtualProperties(Column.Ex);
		}

		private void Column_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            form.RefreshVirtualProperties();

			switch (e.PropertyName)
			{
				case "Name":
					form.ColumnName = Column.Name;
					break;
				case "Description":
					form.Description = Column.Description;
					break;
				case "IsNullable":
					form.IsNullable = Column.IsNullable;
					break;
				case "Datatype":
					form.Datatype = Column.Datatype;
					break;
				case "Default":
					form.Default = Column.Default;
					break;
				case "OrdinalPosition":
					form.OrdinalPosition = Column.OrdinalPosition;
					break;
				case "Size":
					form.ColumnSize = Column.Size;
					form.ColumnSizeIsMax = Column.SizeIsMax;
					break;
				case "Precision":
					form.Precision = Column.Precision;
					break;
				case "Scale":
					form.ColumnScale = Column.Scale;
					break;
			}
		}

		public void AttachToModel(IColumn column)
		{
			if (!Detached) DetachFromModel();

			Column = column;
			Column.PropertyChanged += Column_PropertyChanged;
			Detached = false;

			SetupForm();
		}

		public override void DetachFromModel()
		{
			if (Detached || Column == null) return;

			Column.PropertyChanged -= Column_PropertyChanged;
			Column = null;
			Detached = true;
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is IColumn == false)
				throw new ArgumentException("Model must be an IColumn");
			AttachToModel((IColumn)obj);
		}

		public override void Show()
		{
			ShowPropertyGrid(form);
		}
	}
}