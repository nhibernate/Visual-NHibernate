using System.Collections.ObjectModel;
using System.Windows;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using SchemaDiagrammer.View.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	public class TableShape : DiagramShape
	{
		public static readonly DependencyProperty ColumnsProperty =
			DependencyProperty.Register("Columns", typeof(ObservableCollection<IColumn>), typeof(TableShape), new FrameworkPropertyMetadata(new ObservableCollection<IColumn>()));
        public static readonly DependencyProperty IsColumnListVisibleProperty =
            DependencyProperty.Register("IsColumnListVisible", typeof(bool), typeof(DiagramShape), new FrameworkPropertyMetadata(false));

		static TableShape()
        {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TableShape), new FrameworkPropertyMetadata(typeof(TableShape)));
        }

		public TableShape()
		{
			Columns = new ObservableCollection<IColumn>();
		}

		public ObservableCollection<IColumn> Columns
		{
			get { return (ObservableCollection<IColumn>)GetValue(ColumnsProperty); }
			set { SetValue(ColumnsProperty, value); }
		}

        public bool IsColumnListVisible
        {
            get { return (bool)GetValue(IsColumnListVisibleProperty); }
            set { SetValue(IsColumnListVisibleProperty, value); }
        }
	}
}
