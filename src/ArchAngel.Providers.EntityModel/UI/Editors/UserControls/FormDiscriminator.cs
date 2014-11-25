using System;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
//using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormDiscriminator : Form
	{
		private EntityImpl Entity;

		public FormDiscriminator(EntityImpl entity)
		{
			InitializeComponent();

			Entity = entity;
			Populate();
		}

		private void Populate()
		{
			comboBoxColumn.Items.Clear();
			comboBoxOperator.Items.Clear();
			comboBoxColumn.DisplayMember = "Name";
			comboBoxOperator.DisplayMember = "DisplayText";

			foreach (ITable table in Entity.MappedTables())
			{
				foreach (Column column in table.Columns)
				{
					comboBoxColumn.Items.Add(column);
				}
			}
			comboBoxColumn.Sorted = true;

			foreach (Operator op in Operator.BuiltInOperations)
			{
				comboBoxOperator.Items.Add(op);
			}
			comboBoxOperator.Sorted = true;

			if (Entity.Discriminator != null && Entity.Discriminator.RootGrouping != null)
			{
				Condition firstCondition = Entity.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

				if (firstCondition != null)
				{
					IColumn column = firstCondition.Column;
					Operator op = firstCondition.Operator;
					string exprText = firstCondition.ExpressionValue.Value;

					comboBoxColumn.Text = column.Name;
					comboBoxOperator.Text = op.DisplayText;
					textBoxValue.Text = exprText;
				}
			}
		}

		private void Save()
		{
			Grouping g = new AndGrouping();
			IColumn column = (IColumn)comboBoxColumn.SelectedItem;
			Operator op = (Operator)comboBoxOperator.SelectedItem;
			ExpressionValue value = new ExpressionValueImpl(textBoxValue.Text);
			if (column != null && op != null && value != null)
				g.AddCondition(new ConditionImpl(column, op, value));

			if (Entity.Discriminator == null)
				Entity.Discriminator = new DiscriminatorImpl();

			Entity.Discriminator.RootGrouping = g;
			//DiscriminatorChanged.RaiseEventEx(this);
		}

		private void FormDiscriminator_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void comboBoxEx2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void comboBoxEx1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			Save();
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

	}
}
