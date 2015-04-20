using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormComponentMappingEditor : Form
	{
		private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
		private Brush TextBrush = new SolidBrush(Color.White);
		private float maxWidthPrimary;
		private float maxWidthForeign;
		private int Gap = 5;
		private int middleColumnWidth = 20;
		private Font TableNameFont;
		private Font ColumnNameFont;
		private Pen LinePen = new Pen(Color.White, 1F);
		private List<LabelX> Labels = new List<LabelX>();
		private List<ComboBoxEx> ComboBoxes = new List<ComboBoxEx>();

		public FormComponentMappingEditor()
		{
			InitializeComponent();

			this.Font = new Font(this.Font.Name, 9F, FontStyle.Regular);
			TableNameFont = new Font(this.Font.Name, 9F, FontStyle.Bold);
			ColumnNameFont = new Font(this.Font.Name, 7F, FontStyle.Regular);
			ForeColor = Color.White;
			LinePen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
			LinePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
		}

		private ComponentSpecification ComponentSpecification { get; set; }

		private Entity Entity { get; set; }

		public void Fill(ComponentSpecification componentSpecification, Entity entity)
		{
			ComponentSpecification = componentSpecification;
			Entity = entity;
			Populate();
		}

		private void Populate()
		{
			foreach (LabelX label in Labels)
				panelEx1.Controls.Remove(label);

			foreach (ComboBoxEx combo in ComboBoxes)
				panelEx1.Controls.Remove(combo);

			Labels.Clear();
			ComboBoxes.Clear();
			textBoxComponentName.Text = "";

			//labelComponentName.Text = string.Format("{0} fields", ComponentSpecification.Name);
			labelComponentName.Text = "Component fields";

			if (Entity == null)
			{
				pictureComponent.Visible = false;
				labelComponentName.Visible = false;
				pictureEntity.Visible = false;
				//labelColumns.Visible = false;
				labelColumns.Left = Gap;
				labelColumns.Text = "Select entity:";
				comboBoxExEntities.Left = labelColumns.Right + Gap;
				comboBoxExEntities.Visible = true;
				comboBoxExEntities.DisplayMember = "Name";
				comboBoxExEntities.Items.Clear();
				comboBoxExEntities.Items.AddRange(ComponentSpecification.EntitySet.Entities.ToArray());
				comboBoxExEntities.Top = Gap;// pictureBox1.Bottom + Gap;// labelColumns.Top;
				labelColumns.Top = comboBoxExEntities.Top;
				textBoxComponentName.Text = "";
				textBoxComponentName.Visible = false;
				labelComponentNameInput.Visible = false;
				int newWidth = comboBoxExEntities.Right + Gap;

				if (this.Width != newWidth)
					this.Left = this.Left - (this.Width - newWidth);

				this.Width = newWidth;
			}
			else
			{
				textBoxComponentName.Visible = true;
				labelComponentNameInput.Visible = true;
				pictureComponent.Visible = true;
				labelComponentName.Visible = true;
				pictureEntity.Visible = true;
				labelColumns.Top = pictureEntity.Top;
				labelColumns.Visible = true;
				comboBoxExEntities.Visible = false;
				labelColumns.Text = "Mapped columns";

				//if (string.IsNullOrEmpty(textBoxComponentName.Text))
				//    textBoxComponentName.Text = "Not set";
			}
			if (ComponentSpecification != null)
			{
				comboBoxComponents.Visible = false;
				labelSelect.Visible = false;
				textBoxComponentName.Top = Gap;
				labelComponentNameInput.Top = Gap;

				ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component = ComponentSpecification.GetImplementationFor(Entity);

				if (component != null)
					textBoxComponentName.Text = component.Name;
			}
			else
			{
				comboBoxComponents.DisplayMember = "Name";

				foreach (var comp in Entity.EntitySet.ComponentSpecifications)
					comboBoxComponents.Items.Add(comp);

				//if (comboBoxComponents.Items.Count > 0)
				//    comboBoxComponents.SelectedIndex = 0;

				comboBoxComponents.Top = Gap;
				labelSelect.Top = Gap;
				textBoxComponentName.Top = comboBoxComponents.Bottom + Gap * 3;
				labelComponentNameInput.Top = textBoxComponentName.Top;
			}

			int top = labelComponentName.Bottom + Gap * 2;

			if (Entity != null)
			{
				if (ComponentSpecification != null)
				{
					foreach (var prop in ComponentSpecification.Properties)
						Labels.Add(new LabelX() { Text = string.Format("{0}  [{1}]", prop.Name, prop.Type) });

					ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component = ComponentSpecification.GetImplementationFor(Entity);
					int numTables = Entity.MappedTables().Count();

					if (component != null)
					{
						foreach (var prop in component.Properties)
						{
							List<Column> columns = GetColumnsOfType(Entity, prop.RepresentedProperty.Type);
							ComboBoxEx combo = new ComboBoxEx();
							combo.Width = 200;

							foreach (Column column in columns)
							{
								if (numTables > 1)
									combo.Items.Add(string.Format("{0}.{1}  [{2}]", column.Parent.Name, column.Name, column.OriginalDataType));
								else
									combo.Items.Add(string.Format("{0}  [{1}]", column.Name, column.OriginalDataType));
							}
							combo.DropDownStyle = ComboBoxStyle.DropDownList;
							IColumn col = prop.MappedColumn();

							if (col != null)
							{
								if (numTables > 1)
									combo.SelectedItem = string.Format("{0}.{1}  [{2}]", col.Parent.Name, col.Name, col.OriginalDataType);
								else
									combo.SelectedItem = string.Format("{0}  [{1}]", col.Name, col.OriginalDataType);
							}
							ComboBoxes.Add(combo);
						}
					}
					else
					{
						// Get the properties of the component specification
						foreach (var prop in ComponentSpecification.Properties)
						{
							List<Column> columns = GetColumnsOfType(Entity, prop.Type);
							ComboBoxEx combo = new ComboBoxEx();
							combo.Width = 200;

							foreach (Column column in columns)
							{
								if (numTables > 1)
									combo.Items.Add(string.Format("{0}.{1}  [{2}]", column.Parent.Name, column.Name, column.OriginalDataType));
								else
									combo.Items.Add(string.Format("{0}  [{1}]", column.Name, column.OriginalDataType));
							}
							combo.DropDownStyle = ComboBoxStyle.DropDownList;
							ComboBoxes.Add(combo);
						}
					}
				}

				int maxLeftColumnWidth = Math.Max(GetWidestLabel(Labels), Gap + pictureComponent.Width + labelComponentName.Width);
				int maxRightColumnWidth = Math.Max(GetWidestComboBox(ComboBoxes), pictureEntity.Width + labelColumns.Width + Gap);
				Graphics labelGraphics = null;

				if (ComponentSpecification != null)
				{
					for (int i = 0; i < ComponentSpecification.Properties.Count; i++)
					{
						LabelX label = Labels[i];
						panelEx1.Controls.Add(label);
						//label.BackColor = Color.Transparent;
						//label.ForeColor = Color.White;
						if (labelGraphics == null)
							labelGraphics = Graphics.FromHwnd(label.Handle);

						SizeF size = Graphics.FromHwnd(label.Handle).MeasureString(label.Text, label.Font);
						label.Height = Convert.ToInt32(size.Height) + 1;
						label.Width = Convert.ToInt32(size.Width) + 1;
						label.Top = top;
						label.Left = Math.Max(Gap, Gap + maxLeftColumnWidth - label.Width);
						label.BringToFront();

						if (Entity != null)
						{
							ComboBoxEx comboBox = ComboBoxes[i];
							panelEx1.Controls.Add(comboBox);
							comboBox.Top = top;
							comboBox.Left = Gap * 10 + maxLeftColumnWidth + 60;
							comboBox.Width = maxRightColumnWidth;
							comboBox.BringToFront();

							label.Top = top + comboBox.Height / 2 - label.Height / 2;
							top += comboBox.Height + 2;
						}
						else
							top += label.Height + 2;
					}
				}

				pictureComponent.Left = Gap;
				labelComponentName.Left = pictureComponent.Right;
				pictureEntity.Left = Gap * 10 + maxLeftColumnWidth + 60;
				labelColumns.Left = pictureEntity.Right;
				comboBoxExEntities.Left = pictureEntity.Right;
				comboBoxExEntities.Top = labelColumns.Top;
				int newWidth = Gap * 10 + maxLeftColumnWidth + 60 + maxRightColumnWidth + Gap * 3;

				if (this.Width != newWidth)
					this.Left = this.Left - (this.Width - newWidth);

				this.Width = newWidth;
			}
			this.Height = top + Gap * 15 + buttonOk.Height;
		}

		private int GetWidestLabel(List<LabelX> labels)
		{
			int maxWidth = 0;

			foreach (LabelX label in labels)
				maxWidth = Math.Max(maxWidth, label.Width);

			return maxWidth;
		}

		private int GetWidestComboBox(List<ComboBoxEx> comboBoxes)
		{
			int maxWidth = 0;

			foreach (ComboBoxEx combo in comboBoxes)
				maxWidth = Math.Max(maxWidth, combo.Width);

			return maxWidth;
		}

		private List<Column> GetColumnsOfType(Entity entity, string type)
		{
			List<Column> results = new List<Column>();

			foreach (Table table in Entity.MappedTables())
			{
				foreach (Column column in table.Columns)
				{
					//if (column.Type == type)
					results.Add(column);
				}
			}
			return results;
		}

		private void Draw()
		{
			if (Labels.Count > 0 && ComboBoxes.Count > 0)
			{
				Graphics g = Graphics.FromHwnd(panelEx1.Handle);

				for (int i = 0; i < Labels.Count; i++)
				{
					Point startPoint = new Point(Labels[i].Right + 2, Labels[i].Top + Convert.ToInt32(Labels[i].Height / 2));
					Point endPoint = new Point(ComboBoxes[i].Left - 2, Labels[i].Top + Convert.ToInt32(Labels[i].Height / 2));
					g.DrawLine(LinePen, startPoint, endPoint);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Draw();
		}

		private void comboBoxExEntities_SelectedIndexChanged(object sender, EventArgs e)
		{
			Entity = (Entity)comboBoxExEntities.SelectedItem;
			ComponentSpecification.CreateImplementedComponentFor(Entity, textBoxComponentName.Text);
			Populate();
		}

		private bool Save()
		{
			if (Entity == null)
				return false;

			if (ComponentSpecification == null)
			{
				MessageBox.Show(this, "No component selected.", "Component missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component = ComponentSpecification.GetImplementationFor(Entity);

			if (component == null)
			{
				// Create a new mapping
				component = ComponentSpecification.CreateImplementedComponentFor(Entity, textBoxComponentName.Text);
			}
			component.Name = textBoxComponentName.Text;
			List<Column> columns = GetColumnsOfType(Entity, "xxx");
			int numTables = Entity.MappedTables().Count();

			for (int i = 0; i < ComponentSpecification.Properties.Count; i++)
			{
				if (ComboBoxes[i].SelectedItem != null)
				{
					Column col = null;
					string selectedName = ComboBoxes[i].SelectedItem.ToString();

					if (numTables > 1)
					{
						foreach (Column c in columns)
						{
							if (string.Format("{0}.{1}  [{2}]", c.Parent.Name, c.Name, c.OriginalDataType) == selectedName)
							{
								col = c;
								break;
							}
						}
					}
					else
					{
						foreach (Column c in columns)
						{
							if (string.Format("{0}  [{1}]", c.Name, c.OriginalDataType) == selectedName)
							{
								col = c;
								break;
							}
						}
					}
					if (col == null)
						throw new Exception("Column shouldn't be null");

					component.Properties[i].SetMappedColumn(col);
				}
				else
				{
					component.Properties[i].SetMappedColumn(null);
				}
			}
			return true;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (Save())
				Close();
		}

		private void comboBoxComponents_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComponentSpecification = (ComponentSpecificationImpl)comboBoxComponents.SelectedItem;
			Populate();
		}

	}
}
