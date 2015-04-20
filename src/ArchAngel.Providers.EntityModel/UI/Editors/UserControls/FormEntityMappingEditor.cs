using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
//using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormEntityMappingEditor : UserControl
	{
		private struct LineWidth
		{
			public float NameWidth;
			public float TypeWidth;

			public LineWidth(float nameWidth, float typeWidth)
			{
				NameWidth = nameWidth;
				TypeWidth = typeWidth;
			}

			public float TotalWidth
			{
				get { return NameWidth + TypeWidth; }
			}
		}
		public enum LayoutStyles
		{
			EntityTable,
			TableEntity
		}
		private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
		private static Image EntityImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.square_light_blue_16.png"));

		private Entity _Entity;
		public Table Table = null;
		private int Gap = 5;
		private Font FontHeader;
		private Font FontItem;
		private Font FontType;
		private Brush TextBrush = new SolidBrush(Color.White);
		private Brush TypeBrush = new SolidBrush(Color.LightGray);
		private Brush ComponentBoxBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
		private Dictionary<object, LineWidth> LineWidths = new Dictionary<object, LineWidth>();
		private Pen _LinePen = null;
		public bool EntityOnLeft = true;

		public FormEntityMappingEditor()
		{
			InitializeComponent();

			FontHeader = new Font(Font.FontFamily.Name, 8.5F, FontStyle.Bold);
			FontItem = new Font(Font.FontFamily.Name, 8F, FontStyle.Regular);
			FontType = new Font(Font.FontFamily.Name, 7F, FontStyle.Regular);

			SetStyle(
		  ControlStyles.UserPaint |
		  ControlStyles.AllPaintingInWmPaint |
		  ControlStyles.OptimizedDoubleBuffer, true);
		}

		private Pen LinePen
		{
			get
			{
				if (_LinePen == null)
				{
					_LinePen = new Pen(Color.White);
					//_LinePen.EndCap = 
				}
				return _LinePen;
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		public Entity Entity
		{
			get { return _Entity; }
			set
			{
				if (_Entity != value)
				{
					_Entity = value;
				}
				if (_Entity != null)
				{
					LineWidths = new Dictionary<object, LineWidth>();

					using (Graphics g = Graphics.FromHwnd(panelEx1.Handle))
					{
						int width = (int)(Gap + Math.Max(GetWidestPropertyLine(g), GetWidestComponentPropertyLine(g)) + Gap * 10 + GetWidestColumnLine(g) + Gap);
						this.Width = Math.Max(width, 400);
					}
					//throw new NotImplementedException("TODO: ddiscriminator stuff");
					PopulateDiscriminator();
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = Graphics.FromHwnd(panelEx1.Handle);
			float widestPropertyLine = Math.Max(GetWidestPropertyLine(g), GetWidestComponentPropertyLine(g));
			float widestColumnLine = GetWidestColumnLine(g);
			Point leftPoint;
			Point rightPoint;

			if (EntityOnLeft)
			{
				leftPoint = new Point(Gap, pictureBox1.Bottom + Gap);
				//rightPoint = new Point((int)widestPropertyLine + Gap * 10, pictureBox1.Bottom + Gap);
				rightPoint = new Point(this.Width - (int)widestColumnLine - Gap, pictureBox1.Bottom + Gap);
			}
			else
			{
				//leftPoint = new Point((int)widestPropertyLine + Gap * 10, pictureBox1.Bottom + Gap);
				leftPoint = new Point(this.Width - (int)widestPropertyLine - Gap, pictureBox1.Bottom + Gap);
				rightPoint = new Point(Gap, pictureBox1.Bottom + Gap);
			}
			SizeF textSize = g.MeasureString(Entity.Name, FontHeader);
			g.DrawImage(EntityImage, new Rectangle(leftPoint, new Size(EntityImage.Width, EntityImage.Height)));
			g.DrawString(Entity.Name, FontHeader, TextBrush, leftPoint.X + EntityImage.Width + 1, leftPoint.Y);
			textSize = g.MeasureString("ABC", FontItem);
			leftPoint.Offset(0, (int)textSize.Height + 5);

			foreach (var table in Entity.MappedTables())
			{
				if (Table != null && table != Table)
					continue;

				g.DrawImage(TableImage, new Rectangle(rightPoint, new Size(TableImage.Width, TableImage.Height)));
				g.DrawString(table.Name, FontHeader, TextBrush, rightPoint.X + TableImage.Width + 1, rightPoint.Y);
				rightPoint.Offset(0, (int)textSize.Height + 5);

				foreach (var property in Entity.Properties)
				{
					IColumn mappedColumn = property.MappedColumn();

					if (mappedColumn == null)
						continue;

					if (mappedColumn.Parent == table)
					{
						if (EntityOnLeft)
							g.DrawLine(LinePen, leftPoint.X + widestPropertyLine + 4, leftPoint.Y + textSize.Height / 2, rightPoint.X - 4, rightPoint.Y + textSize.Height / 2);
						else
							g.DrawLine(LinePen, rightPoint.X + widestColumnLine + 4, rightPoint.Y + textSize.Height / 2, leftPoint.X - 4, leftPoint.Y + textSize.Height / 2);

						textSize = g.MeasureString(property.Name, FontItem);
						LineWidth lineWidth = GetWidthOfPropertyLine(g, property);

						if (EntityOnLeft)
						{
							g.DrawString(string.Format("{0}", property.NHibernateType), FontType, TypeBrush, leftPoint.X, leftPoint.Y);
							g.DrawString(property.Name, FontItem, TextBrush, leftPoint.X + widestPropertyLine - lineWidth.NameWidth, leftPoint.Y);
						}
						else
						{
							g.DrawString(property.Name, FontItem, TextBrush, leftPoint.X, leftPoint.Y);
							g.DrawString(string.Format("{0}", property.NHibernateType), FontType, TypeBrush, leftPoint.X + widestPropertyLine - lineWidth.TypeWidth, leftPoint.Y);
						}
						leftPoint.Offset(0, (int)textSize.Height + 1);

						textSize = g.MeasureString(mappedColumn.Name, FontItem);
						lineWidth = GetWidthOfColumnLine(g, mappedColumn);

						if (EntityOnLeft)
						{
							g.DrawString(mappedColumn.Name, FontItem, TextBrush, rightPoint);
							g.DrawString(string.Format("{0}", mappedColumn.OriginalDataType), FontType, TypeBrush, rightPoint.X + widestColumnLine - lineWidth.TypeWidth, rightPoint.Y);
						}
						else
						{
							g.DrawString(string.Format("{0}", mappedColumn.OriginalDataType), FontType, TypeBrush, rightPoint);
							g.DrawString(mappedColumn.Name, FontItem, TextBrush, rightPoint.X + widestColumnLine - lineWidth.NameWidth, rightPoint.Y);

						}
						rightPoint.Offset(0, (int)textSize.Height + 1);
					}
				}
				foreach (var component in Entity.Components)
				{
					if (component.MappedTable() == table)
					{
						leftPoint.Offset(0, 3);
						rightPoint.Offset(0, 3);

						Size componentBoxSize = new Size((int)widestPropertyLine, 3 + (1 + component.Properties.Count) * ((int)textSize.Height + 1));

						g.FillRectangle(ComponentBoxBrush, new Rectangle(leftPoint, componentBoxSize));
						float typeNameWidth = g.MeasureString(component.Name, FontItem).Width;
						g.DrawString(string.Format("{0}  ", component.Specification.Name), FontType, TypeBrush, leftPoint.X, leftPoint.Y);
						g.DrawString(component.Name, FontItem, TextBrush, leftPoint.X + widestPropertyLine - typeNameWidth, leftPoint.Y);
						leftPoint.Offset(0, (int)textSize.Height + 3);
						rightPoint.Offset(0, (int)textSize.Height + 3);

						foreach (var componentProperty in component.Properties)
						{
							IColumn mappedColumn = componentProperty.MappedColumn();

							if (mappedColumn == null)
								continue;

							g.DrawLine(LinePen, leftPoint.X + widestPropertyLine + 4, leftPoint.Y + textSize.Height / 2, rightPoint.X - 4, rightPoint.Y + textSize.Height / 2);

							textSize = g.MeasureString(componentProperty.PropertyName, FontItem);
							g.DrawString(string.Format("{0}  ", componentProperty.RepresentedProperty.Type), FontType, TypeBrush, leftPoint.X, leftPoint.Y);
							LineWidth lineWidth = GetWidthOfComponentPropertyLine(g, componentProperty);
							g.DrawString(componentProperty.PropertyName, FontItem, TextBrush, leftPoint.X + widestPropertyLine - lineWidth.NameWidth, leftPoint.Y);
							leftPoint.Offset(0, (int)textSize.Height + 1);

							textSize = g.MeasureString(mappedColumn.Name, FontItem);
							g.DrawString(mappedColumn.Name, FontItem, TextBrush, rightPoint);
							lineWidth = GetWidthOfColumnLine(g, mappedColumn);
							g.DrawString(string.Format("  {0}", mappedColumn.OriginalDataType), FontType, TypeBrush, this.Width - Gap * 2 - lineWidth.TypeWidth, rightPoint.Y);
							rightPoint.Offset(0, (int)textSize.Height + 1);
						}
					}
				}
				leftPoint.Offset(0, 8);
				rightPoint.Offset(0, 8);
			}
			if (string.IsNullOrEmpty(labelDiscriminator.Text))
			{
				if (this.Height != leftPoint.Y + Gap)
					this.Height = leftPoint.Y + Gap;
			}
			else
			{
				if (labelDiscriminator.Top != leftPoint.Y)
				{
					labelDiscriminator.Top = leftPoint.Y;
					labelDiscriminator.Width = this.Width - labelDiscriminator.Left - 5;
				}
				if (this.Height != labelDiscriminator.Bottom + Gap)
					this.Height = labelDiscriminator.Bottom + Gap;
			}
		}

		private float GetWidestPropertyLine(Graphics g)
		{
			float maxWidth = 0;

			foreach (var table in Entity.MappedTables())
				foreach (var property in Entity.Properties)
					maxWidth = Math.Max(maxWidth, GetWidthOfPropertyLine(g, property).TotalWidth);

			maxWidth = Math.Max(maxWidth, g.MeasureString(Entity.Name, FontHeader).Width + EntityImage.Width + 1);
			return maxWidth;
		}

		private LineWidth GetWidthOfPropertyLine(Graphics g, Property property)
		{
			if (!LineWidths.ContainsKey(property))
				LineWidths.Add(
					property,
					new LineWidth(
						g.MeasureString(property.Name, FontItem).Width,
						g.MeasureString(property.NHibernateType, FontType).Width
						)
					);

			return LineWidths[property];
		}

		private float GetWidestColumnLine(Graphics g)
		{
			float maxWidth = 0;

			foreach (var table in Entity.MappedTables())
			{
				foreach (var property in Entity.Properties)
				{
					IColumn mappedColumn = property.MappedColumn();

					if (mappedColumn == null)
						continue;

					maxWidth = Math.Max(maxWidth, GetWidthOfColumnLine(g, mappedColumn).TotalWidth);
				}
				maxWidth = Math.Max(maxWidth, g.MeasureString(table.Name, FontHeader).Width + EntityImage.Width + 1);
			}
			return maxWidth;
		}

		private LineWidth GetWidthOfColumnLine(Graphics g, IColumn column)
		{
			if (column == null)
				return new LineWidth(0, 0);

			if (!LineWidths.ContainsKey(column))
				LineWidths.Add(
					column,
					new LineWidth(
						g.MeasureString(column.Name, FontItem).Width,
						g.MeasureString(column.OriginalDataType.ToString(), FontType).Width
						)
					);

			return LineWidths[column];
		}

		private float GetWidestComponentPropertyLine(Graphics g)
		{
			float maxWidth = 0;

			foreach (var table in Entity.MappedTables())
				foreach (var component in Entity.Components)
				{
					maxWidth = Math.Max(maxWidth, g.MeasureString(component.Name, FontItem).Width + g.MeasureString(component.Specification.Name + "  ", FontType).Width);

					foreach (var componentProperty in component.Properties)
						maxWidth = Math.Max(maxWidth, GetWidthOfComponentPropertyLine(g, componentProperty).TotalWidth);
				}
			return maxWidth;
		}

		private LineWidth GetWidthOfComponentPropertyLine(Graphics g, ComponentPropertyMarker property)
		{
			if (!LineWidths.ContainsKey(property))
				LineWidths.Add(
					property,
					new LineWidth(
						g.MeasureString(property.PropertyName, FontItem).Width,
						g.MeasureString(property.RepresentedProperty.Type, FontType).Width
						)
					);

			return LineWidths[property];
		}

		private void PopulateDiscriminator()
		{
			labelDiscriminator.Text = "";

			if (Entity.Parent != null)
			{
				if (Entity.Parent.Discriminator != null &&
					EntityImpl.DetermineInheritanceTypeWithParent(Entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
				{
					if (Entity.Parent.Discriminator.DiscriminatorType == Enums.DiscriminatorTypes.Column)
						labelDiscriminator.Text = string.Format("{0} = {1}", Entity.Parent.Discriminator.ColumnName, Entity.DiscriminatorValue);
					else
						labelDiscriminator.Text = string.Format("Value: {0} Formula: {1}", Entity.DiscriminatorValue, Entity.Parent.Discriminator.Formula);
				}
			}
		}

		private void Save()
		{
			//throw new NotImplementedException("TODO: ddiscriminator stuff");
			//if (comboBoxColumn.Text == "")
			//{
			//    Entity.Discriminator = null;
			//}
			//else
			//{
			//    Grouping g = new AndGrouping();
			//    IColumn column = (IColumn)comboBoxColumn.SelectedItem;
			//    Operator op = (Operator)comboBoxOperator.SelectedItem;
			//    ExpressionValue value = new ExpressionValueImpl(textBoxValue.Text);
			//    if (column != null && op != null && value != null)
			//        g.AddCondition(new ConditionImpl(column, op, value));

			//    if (Entity.Discriminator == null)
			//        Entity.Discriminator = new DiscriminatorImpl();

			//    Entity.Discriminator.RootGrouping = g;
			//    //DiscriminatorChanged.RaiseEventEx(this);
			//}
			//Entity = null;
		}

		private void FormEntityMappingEditor_VisibleChanged(object sender, EventArgs e)
		{
			if (!this.Visible && Entity != null)
				Save();
		}

	}
}
