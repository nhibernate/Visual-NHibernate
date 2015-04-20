using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormPrefixes : UserControl, IOptionForm
	{
		private MappingSet MappingSet;
		private string _Text;

		public FormPrefixes()
		{
			InitializeComponent();

			Text = "Database Prefixes & Suffixes";

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);
		}

		public string Text
		{
			get { return _Text; }
			set { _Text = value; }
		}

		public void FinaliseEdits()
		{
			MappingSet.TablePrefixes.Clear();
			MappingSet.ColumnPrefixes.Clear();
			MappingSet.TableSuffixes.Clear();
			MappingSet.ColumnSuffixes.Clear();

			foreach (string colPrefix in textBoxColumnPrefixes.Text.Split(','))
				MappingSet.ColumnPrefixes.Add(colPrefix.Trim());

			foreach (string tablePrefix in textBoxTablePrefixes.Text.Split(','))
				MappingSet.TablePrefixes.Add(tablePrefix.Trim());

			foreach (string colSuffix in textBoxColumnSuffixes.Text.Split(','))
				MappingSet.ColumnSuffixes.Add(colSuffix.Trim());

			foreach (string tableSuffix in textBoxTableSuffixes.Text.Split(','))
				MappingSet.TableSuffixes.Add(tableSuffix.Trim());
		}

		public void Fill(List<ArchAngel.Interfaces.ProviderInfo> providers)
		{
			if (providers == null)
				MappingSet = new MappingSetImpl();
			else
				foreach (var provider in providers)
					if (provider is ArchAngel.Providers.EntityModel.ProviderInfo)
					{
						MappingSet = ((ArchAngel.Providers.EntityModel.ProviderInfo)provider).MappingSet;
						break;
					}

			Populate();
		}

		private void Populate()
		{
			PopulateTablePrefixGrid();
			PopulateColumnPrefixGrid();
			PopulateTableSuffixGrid();
			PopulateColumnSuffixGrid();
		}

		private void PopulateTablePrefixGrid()
		{
			string tablePrefixes = "";

			foreach (string prefix in MappingSet.TablePrefixes)
				tablePrefixes += prefix + ",";

			tablePrefixes = tablePrefixes.TrimEnd(',');
			textBoxTablePrefixes.Text = tablePrefixes;
		}


		private void PopulateColumnPrefixGrid()
		{
			string columnPrefixes = "";

			foreach (string prefix in MappingSet.ColumnPrefixes)
				columnPrefixes += prefix + ",";

			columnPrefixes = columnPrefixes.TrimEnd(',');
			textBoxColumnPrefixes.Text = columnPrefixes;
		}

		private void PopulateTableSuffixGrid()
		{
			string tableSuffixes = "";

			foreach (string prefix in MappingSet.TableSuffixes)
				tableSuffixes += prefix + ",";

			tableSuffixes = tableSuffixes.TrimEnd(',');
			textBoxTableSuffixes.Text = tableSuffixes;
		}


		private void PopulateColumnSuffixGrid()
		{
			string columnSuffixes = "";

			foreach (string prefix in MappingSet.ColumnSuffixes)
				columnSuffixes += prefix + ",";

			columnSuffixes = columnSuffixes.TrimEnd(',');
			textBoxColumnSuffixes.Text = columnSuffixes;
		}

		public void Save()
		{
		}

		public List<string> TablePrefixes
		{
			get
			{
				if (DesignMode)
					return new List<string>();

				FinaliseEdits();
				return MappingSet.TablePrefixes.ToList();
			}
			set
			{
				string prefixes = "";

				if (value != null)
					foreach (string prefix in value)
						prefixes += prefix + ",";

				prefixes = prefixes.TrimEnd(',');
				textBoxTablePrefixes.Text = prefixes;
			}
		}

		public List<string> ColumnPrefixes
		{
			get
			{
				FinaliseEdits();
				return MappingSet.ColumnPrefixes.ToList();
			}
			set
			{
				string prefixes = "";

				if (value != null)
					foreach (string prefix in value)
						prefixes += prefix + ",";

				prefixes = prefixes.TrimEnd(',');
				textBoxColumnPrefixes.Text = prefixes;
			}
		}

		public List<string> TableSuffixes
		{
			get
			{
				if (DesignMode)
					return new List<string>();

				FinaliseEdits();
				return MappingSet.TableSuffixes.ToList();
			}
			set
			{
				string suffixes = "";

				if (value != null)
					foreach (string suffix in value)
						suffixes += suffix + ",";

				suffixes = suffixes.TrimEnd(',');
				textBoxTableSuffixes.Text = suffixes;
			}
		}

		public List<string> ColumnSuffixes
		{
			get
			{
				FinaliseEdits();
				return MappingSet.ColumnSuffixes.ToList();
			}
			set
			{
				string suffixes = "";

				if (value != null)
					foreach (string suffix in value)
						suffixes += suffix + ",";

				suffixes = suffixes.TrimEnd(',');
				textBoxColumnSuffixes.Text = suffixes;
			}
		}

	}
}
