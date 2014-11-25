using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.DotNetBar;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormReferenceEditor2 : UserControl
	{
		private enum CardinalityTypes
		{
			Zero,
			One,
			Many
		}
		internal event EventHandler CardinalityChanged;
		private bool IsEnd1 = true;
		private readonly List<IUserOption> virtualProperties = new List<IUserOption>();
		private Dictionary<IUserOption, Control> UserOptionLookups = new Dictionary<IUserOption, Control>();
		private Dictionary<Control, IUserOption> ControlLookups = new Dictionary<Control, IUserOption>();
		public bool PropertiesAffected = false;

		public FormReferenceEditor2()
		{
			InitializeComponent();

			this.Font = new Font(this.Font.Name, 7F, FontStyle.Regular);
			//panelEx1.Style.BackColor1 = Color.FromArgb(20, 20, 20);
			//panelEx1.Style.BackColor2 = Color.FromArgb(100, 100, 100);
			//BackColor = Color.Yellow;
			ForeColor = Color.White;//.Black;

			comboBoxCardinality.Items.Add("zero");
			comboBoxCardinality.Items.Add("one");
			comboBoxCardinality.Items.Add("many");
		}

		public void Fill(ReferenceImpl reference, bool isEnd1)
		{
			IsEnd1 = isEnd1;
			Reference = reference;
			PropertiesAffected = false;
			Populate();
		}

		private ReferenceImpl Reference { get; set; }

		private CardinalityTypes CardinalityType
		{
			get
			{
				if (IsEnd1)
					if (!Reference.End1Enabled)
						return CardinalityTypes.Zero;
					else
						return Cardinality.One.Equals(Reference.Cardinality1) ? CardinalityTypes.One : CardinalityTypes.Many;
				else
					if (!Reference.End2Enabled)
						return CardinalityTypes.Zero;
					else
						return Cardinality.One.Equals(Reference.Cardinality2) ? CardinalityTypes.One : CardinalityTypes.Many;
			}
		}

		private void Populate()
		{
			Clear();
			int maxLabelWidth = 0;
			//this.Height = 1000;

			Label label = new Label();
			Graphics g = Graphics.FromHwnd(label.Handle);
			maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString("Name", label.Font).Width));

			for (int i = 0; i < Reference.Ex.Count; i++)
				maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString(Reference.Ex[i].Name, label.Font).Width));

			if (IsEnd1)
			{
				textBoxEndName.Text = Reference.End1Name;
				labelName.Text = string.Format("One {0} has", Reference.Entity1.Name);
				textBoxEndName.Text = Reference.End1Name;
				comboBoxCardinality.SelectedItem = CardinalityType.ToString().ToLower();
			}
			else
			{
				textBoxEndName.Text = Reference.End2Name;
				labelName.Text = string.Format("One {0} has", Reference.Entity2.Name);
				textBoxEndName.Text = Reference.End2Name;
				comboBoxCardinality.SelectedItem = CardinalityType.ToString().ToLower();
			}
			PopulateRelationships();
			PopulateAssociationTables();

			if (Reference.MappedTable() != null)
			{
				ITable mappedTable = Reference.MappedTable();

				foreach (ComboBoxItem item in comboBoxMappedTable.Items)
				{
					if (item.Tag == mappedTable)
					{
						comboBoxMappedTable.SelectedItem = item;
						break;
					}
				}
			}
			if (Reference.MappedRelationship() != null)
				comboBoxMappedRelationship.SelectedItem = Reference.MappedRelationship();

			labelName.Width = Convert.ToInt32(g.MeasureString(labelName.Text, labelName.Font).Width);
			this.Width = labelName.Right + comboBoxCardinality.Width + textBoxEndName.Width + 30 + pictureBox1.Width;
			int top;// = checkBoxIncludeForeignKey.Bottom + 5;// comboBoxMappedRelationship.Bottom + 5;
			checkBoxIncludeForeignKey.Checked = Reference.IncludeForeignKey;

			if (CardinalityType == CardinalityTypes.One)
			{
				checkBoxIncludeForeignKey.Visible = true;
				top = checkBoxIncludeForeignKey.Bottom + 5;
			}
			else
			{
				checkBoxIncludeForeignKey.Visible = false;
				top = comboBoxMappedRelationship.Bottom + 5;
			}

			virtualPropertyGrid1.Top = top;
			virtualPropertyGrid1.Left = 0;
			virtualPropertyGrid1.Width = Width;
			//virtualPropertyGrid1.Height = Height - virtualPropertyGrid1.Top;
			virtualPropertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;// | AnchorStyles.Bottom;
			virtualPropertyGrid1.BackColor = Color.Transparent;// this.BackColor;
			virtualPropertyGrid1.ForeColor = this.ForeColor;
			virtualPropertyGrid1.Reference = Reference;
			virtualPropertyGrid1.IsEnd1 = IsEnd1;

			List<IUserOption> options = new List<IUserOption>();

			if (IsEnd1)
			{
				foreach (IUserOption opt in Reference.Ex)
					if (!opt.Text.StartsWith("End2:"))
						options.Add(opt);
			}
			else
			{
				foreach (IUserOption opt in Reference.Ex)
					if (!opt.Text.StartsWith("End1:"))
						options.Add(opt);
			}
			virtualPropertyGrid1.Font = this.Font;
			virtualPropertyGrid1.SetVirtualProperties(options);
			this.Height = virtualPropertyGrid1.Bottom + 5;

			foreach (Control control in this.Controls)
			{
				control.Font = this.Font;
			}
			virtualPropertyGrid1.Refresh();
			this.Refresh();
		}

		private void PopulateRelationships()
		{
			comboBoxMappedRelationship.Items.Clear();
			comboBoxMappedRelationship.DisplayMember = "Name";

			foreach (var rel in Reference.PossibleMappedRelationships())
				comboBoxMappedRelationship.Items.Add(rel);
		}

		private void PopulateAssociationTables()
		{
			comboBoxMappedTable.Items.Clear();
			comboBoxMappedTable.DisplayMember = "Name";
			//comboBoxMappedTable.Items.Add("");

			foreach (var table in Reference.PossibleMappedTables())
			{
				ComboBoxItem item = new ComboBoxItem(string.Format("{0} ({1})", table.Name, table.Schema));
				item.Tag = table;
				comboBoxMappedTable.Items.Add(item);
			}
		}

		public void Clear()
		{
			for (int i = 0; i < this.Controls.Count; i += 2)
				superTooltip1.SetSuperTooltip(this.Controls[i], null);

			virtualProperties.Clear();
			UserOptionLookups.Clear();
			ControlLookups.Clear();
		}

		private void Save()
		{
		}

		private void FormReferenceEditor2_MouseLeave(object sender, EventArgs e)
		{
			Point pt = PointToClient(Cursor.Position);

			if (pt.X <= 0 || pt.X >= this.Width ||
				pt.Y <= 0 || pt.Y >= this.Height)
			{
				Save();
				Visible = false;
			}
		}

		private void textBoxEndName_TextChanged(object sender, EventArgs e)
		{
			if (IsEnd1)
				Reference.End1Name = textBoxEndName.Text;
			else
				Reference.End2Name = textBoxEndName.Text;
		}

		private void comboBoxCardinality_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsEnd1)
			{
				switch (comboBoxCardinality.Text)
				{
					case "zero":
						Reference.End1Enabled = false;
						Reference.Cardinality1 = new Cardinality(0);
						break;
					case "one":
						Reference.End1Enabled = true;
						Reference.Cardinality1 = new Cardinality(1);
						break;
					case "many":
						Reference.End1Enabled = true;
						Reference.Cardinality1 = new Cardinality(int.MaxValue);
						break;
					default:
						throw new NotImplementedException("Not handled yet");
				}
			}
			else
			{
				switch (comboBoxCardinality.Text)
				{
					case "zero":
						Reference.End2Enabled = false;
						Reference.Cardinality2 = new Cardinality(0);
						break;
					case "one":
						Reference.End2Enabled = true;
						Reference.Cardinality2 = new Cardinality(1);
						break;
					case "many":
						Reference.End2Enabled = true;
						Reference.Cardinality2 = new Cardinality(int.MaxValue);
						break;
					default:
						throw new NotImplementedException("Not handled yet");
				}
			}
			SetComboBoxVisibility();
			virtualPropertyGrid1.LayoutOptions();
			this.Height = virtualPropertyGrid1.Bottom + 5;

			if (CardinalityChanged != null)
				CardinalityChanged(this, null);
		}

		private void SetComboBoxVisibility()
		{
			// Many-to-many references must have a Mapped Table (association table),
			// while all other references must have a Mapped Relationship.
			//if (!ArchAngel.Interfaces.Cardinality.One.Equals(Reference.Cardinality1) &&
			//    !ArchAngel.Interfaces.Cardinality.One.Equals(Reference.Cardinality2))
			//{
			//    comboBoxMappedRelationship.Enabled = false;
			//    comboBoxMappedTable.Enabled = true;
			//}
			//else
			//{
			//    comboBoxMappedRelationship.Enabled = true;
			//    comboBoxMappedTable.Enabled = false;
			//}
		}

		private void FormReferenceEditor2_VisibleChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			Visible = false;
		}

		private void comboBoxMappedTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			Reference.SetMappedTable((ITable)((ComboBoxItem)comboBoxMappedTable.SelectedItem).Tag);
		}

		private void comboBoxMappedRelationship_SelectedIndexChanged(object sender, EventArgs e)
		{
			Reference.SetMappedRelationship((Relationship)comboBoxMappedRelationship.SelectedItem);
		}

		private void checkBoxIncludeForeignKey_CheckedChanged(object sender, EventArgs e)
		{
			if (CardinalityType == CardinalityTypes.One &&
				Reference.IncludeForeignKey != checkBoxIncludeForeignKey.Checked)
			{
				Reference.IncludeForeignKey = checkBoxIncludeForeignKey.Checked;
				PropertiesAffected = true;
			}
		}

		private void virtualPropertyGrid1_Resize(object sender, EventArgs e)
		{
			this.Height = virtualPropertyGrid1.Bottom + 10;
		}
	}
}
