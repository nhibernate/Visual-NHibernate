using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormRelationshipEditor : UserControl
	{
		private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
		private Brush TextBrush = new SolidBrush(Color.White);
		private float MaxWidthPrimary;
		private float MaxWidthForeign;
		private int gap = 5;
		private int middleColumnWidth = 20;
		private Font TableNameFont;
		private Font ColumnNameFont;
		private Pen LinePen = new Pen(Color.White, 1F);
		private bool IsEnd1 = true;

		public FormRelationshipEditor()
		{
			InitializeComponent();

			this.Font = new Font(this.Font.Name, 9F, FontStyle.Regular);
			TableNameFont = new Font(this.Font.Name, 9F, FontStyle.Bold);
			ColumnNameFont = new Font(this.Font.Name, 7F, FontStyle.Regular);
			ForeColor = Color.White;
			labelName.ForeColor = this.ForeColor;
			LinePen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
			LinePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
		}

		private Relationship Relationship { get; set; }

		public void Fill(Relationship relationship, bool isEnd1)
		{
			Relationship = relationship;
			IsEnd1 = isEnd1;
			Populate();
		}

		private void Populate()
		{
			ITable table1 = IsEnd1 ? Relationship.PrimaryTable : Relationship.ForeignTable;
			ITable table2 = IsEnd1 ? Relationship.ForeignTable : Relationship.PrimaryTable;

			IKey key1 = IsEnd1 ? Relationship.PrimaryKey : Relationship.ForeignKey;
			IKey key2 = IsEnd1 ? Relationship.ForeignKey : Relationship.PrimaryKey;

			Graphics g = Graphics.FromHwnd(panelEx1.Handle);

			SizeF tableTextSize = g.MeasureString(table1.Name, TableNameFont);
			SizeF columnTextSize = g.MeasureString(table1.Name, ColumnNameFont);

			if (Relationship.IsUserDefined)
				labelName.Text = "(virtual) " + Relationship.Name;
			else
				labelName.Text = Relationship.Name;

			SizeF labelNameSize = g.MeasureString(labelName.Text, labelName.Font);

			int imageWidth = 16;
			MaxWidthPrimary = Math.Max(GetWidestColumn(key1, g, TableNameFont), tableTextSize.Width + imageWidth);

			tableTextSize = g.MeasureString(table2.Name, TableNameFont);
			MaxWidthForeign = Math.Max(GetWidestColumn(key2, g, TableNameFont), tableTextSize.Width + imageWidth);
			int widthOfTitle = Convert.ToInt32(g.MeasureString(Relationship.Name, TableNameFont).Width) + 4 * pictureBox1.Width;
			this.Width = Math.Max(Math.Max(widthOfTitle, Convert.ToInt32(MaxWidthPrimary + MaxWidthForeign) + gap * 2 + middleColumnWidth), (int)labelNameSize.Width + imageWidth * 2 + 20);
			this.Height = labelName.Bottom + Convert.ToInt32(tableTextSize.Height) + gap + Math.Max(key1.Columns.Count, key2.Columns.Count) * (Convert.ToInt32(columnTextSize.Height) + gap + 10);
		}

		private void Draw()
		{
			List<IColumn> leftColumns = IsEnd1 ? Relationship.PrimaryKey.Columns.ToList() : Relationship.ForeignKey.Columns.ToList();
			List<IColumn> rightColumns = IsEnd1 ? Relationship.ForeignKey.Columns.ToList() : Relationship.PrimaryKey.Columns.ToList();
			string leftTableName = IsEnd1 ? Relationship.PrimaryTable.Name : Relationship.ForeignTable.Name;
			string rightTableName = IsEnd1 ? Relationship.ForeignTable.Name : Relationship.PrimaryTable.Name;

			Graphics g = Graphics.FromHwnd(panelEx1.Handle);
			SizeF tableTextSize = g.MeasureString(leftTableName, TableNameFont);
			SizeF columnTextSize = g.MeasureString(leftTableName, ColumnNameFont);

			//if (Relationship.IsUserDefined)
			//{
			//    SizeF s = g.MeasureString("(virtual)", labelName.Font);
			//    g.DrawString("(virtual)", labelName.Font, TextBrush, (labelName.Right - labelName.Width / 2)
			//}

			// Primary side
			Point textPos = new Point(gap, labelName.Bottom + gap);
			g.DrawImage(TableImage, new Rectangle(textPos, new Size(TableImage.Width, TableImage.Height)));
			g.DrawString(leftTableName, TableNameFont, TextBrush, textPos.X + TableImage.Width, textPos.Y);
			textPos.Y += Convert.ToInt32(tableTextSize.Height) + gap;
			Point lineStart = new Point(gap + Convert.ToInt32(MaxWidthPrimary), 0);
			Point lineEnd = new Point(this.Width - gap - Convert.ToInt32(MaxWidthForeign) - 2, lineStart.Y);
			int halfTextHeight = Convert.ToInt32(columnTextSize.Height) / 2;

			foreach (Column column in leftColumns)
			{
				float offset = MaxWidthPrimary - g.MeasureString(column.Name, ColumnNameFont).Width;
				g.DrawString(column.Name, ColumnNameFont, TextBrush, textPos.X + offset, textPos.Y);
				lineStart.Y = textPos.Y + halfTextHeight;
				lineEnd.Y = lineStart.Y;
				g.DrawLine(LinePen, lineStart, lineEnd);
				textPos.Y += Convert.ToInt32(columnTextSize.Height) + 2;
			}

			// Foreign side
			textPos = new Point(this.Width - gap - Convert.ToInt32(MaxWidthForeign), labelName.Bottom + gap);
			g.DrawImage(TableImage, new Rectangle(textPos, new Size(TableImage.Width, TableImage.Height)));
			//textPos.Offset(TableImage.Width, 0);
			g.DrawString(rightTableName, TableNameFont, TextBrush, textPos.X + TableImage.Width, textPos.Y);
			textPos.Y += Convert.ToInt32(tableTextSize.Height) + gap;

			foreach (Column column in rightColumns)
			{
				g.DrawString(column.Name, ColumnNameFont, TextBrush, textPos.X, textPos.Y);
				textPos.Y += Convert.ToInt32(columnTextSize.Height) + 2;
			}
		}

		private float GetWidestColumn(IKey key, Graphics g, Font font)
		{
			float maxWidth = 0;

			foreach (Column column in key.Columns)
				maxWidth = Math.Max(maxWidth, g.MeasureString(column.Name, font).Width);

			return maxWidth;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Draw();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void FormRelationshipEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Visible = false;
		}

		private void labelX2_Click(object sender, EventArgs e)
		{

		}

	}
}
