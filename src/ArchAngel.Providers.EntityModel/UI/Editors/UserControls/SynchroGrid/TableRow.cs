using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using System.Windows.Forms;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid
{
    public class TableRow
    {
        private int Gap = 5;
        private bool _IsFocused = false;
        public static Image GreenArrowLeftImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_left_light_green_16.png"));
        public static Image GreenArrowRightImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_right_light_green_16.png"));
        public static Image OrangeArrowLeftImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_left_orange_16.png"));
        public static Image OrangeArrowRightImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_right_orange_16.png"));
        public static Image DeleteImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.stop_16.png"));

        public static Image GreenArrowLeftImageLarge = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_left_light_green_24.png"));
        public static Image GreenArrowRightImageLarge = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_right_light_green_24.png"));
        public static Image OrangeArrowLeftImageLarge = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_left_orange_24.png"));
        public static Image OrangeArrowRightImageLarge = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid.Resources.arrow_right_orange_24.png"));

        public Brush TextBrush = new SolidBrush(Color.White);
        public GraphicsPath GPath;
        public GraphicsPath GPathModelDeleteImage;
        public GraphicsPath GPathModelTableName;
        public GraphicsPath GPathCentreImage;
        public GraphicsPath GPathLeftArrow;
        public GraphicsPath GPathRightArrow;

        public GraphicsPath GPathModelTableLabel;
        public GraphicsPath GPathDatabaseTableLabel;
        public GraphicsPath GPathModelColumnsLabel;
        public GraphicsPath GPathModelIndexesLabel;
        public GraphicsPath GPathModelKeysLabel;
        public GraphicsPath GPathModelRelationshipsLabel;

        public GraphicsPath GPathDbColumnsLabel;
        public GraphicsPath GPathDbIndexesLabel;
        public GraphicsPath GPathDbKeysLabel;
        public GraphicsPath GPathDbRelationshipsLabel;

        public GraphicsPath GPathDeleteModelTable;
        public GraphicsPath GPathDeleteDatabaseTable;

        public bool TableExpanded = false;
        private bool ColumnsExpanded = false;
        private bool KeysExpanded = false;
        private bool IndexesExpanded = false;
        private bool RelationshipsExpanded = false;

        private Table ModelTable { get; set; }
        private Table DatabaseTable { get; set; }
        public Font Font { get; set; }
        private IList<SynchroGrid.SynchroCanvas.ColumnMatch> ColumnMatches = new List<SynchroCanvas.ColumnMatch>();
        private IList<SynchroGrid.SynchroCanvas.KeyMatch> KeyMatches = new List<SynchroCanvas.KeyMatch>();
        private IList<SynchroGrid.SynchroCanvas.IndexMatch> IndexMatches = new List<SynchroCanvas.IndexMatch>();
        private IList<SynchroGrid.SynchroCanvas.RelationshipMatch> RelationshipMatches = new List<SynchroCanvas.RelationshipMatch>();

        public TableRow(Table modelTable, Table databaseTable, Font font)
        {
            ModelTable = modelTable;
            DatabaseTable = databaseTable;
            Font = font;
        }

        public TableRow(
            Table modelTable,
            Table databaseTable,
            Font font,
            IList<SynchroGrid.SynchroCanvas.ColumnMatch> columnMatches,
            IList<SynchroGrid.SynchroCanvas.KeyMatch> keyMatches,
            IList<SynchroGrid.SynchroCanvas.IndexMatch> indexMatches,
            IList<SynchroGrid.SynchroCanvas.RelationshipMatch> relationshipMatches)
            : this(modelTable, databaseTable, font)
        {
            ColumnMatches = columnMatches;
            KeyMatches = keyMatches;
            IndexMatches = indexMatches;
            RelationshipMatches = relationshipMatches;
        }

        private Color BackColor1
        {
            get
            {
                if (TableExpanded)
                    return Color.FromArgb(70, 70, 70);
                else if (IsFocused)
                    return Color.FromArgb(120, 120, 120);
                //return Color.FromArgb(100, 100, 100);
                else
                    return Color.FromArgb(30, 30, 30);
            }
        }

        private Color BackColor2
        {
            get
            {
                if (TableExpanded)
                    return Color.FromArgb(70, 70, 70);
                else if (IsFocused)
                    return Color.FromArgb(50, 50, 50);
                else
                    return Color.FromArgb(30, 30, 30);
                //return Color.FromArgb(100, 100, 100);
            }
        }

        public bool IsFocused
        {
            get { return _IsFocused; }
            set
            {
                if (_IsFocused != value)
                    _IsFocused = value;
            }
        }

        public Rectangle Draw(Graphics g, Point pos, int width)
        {
            Rectangle rectMain;
            SizeF textSize = new SizeF();

            if (DatabaseTable != null)
                textSize = g.MeasureString(DatabaseTable.Name, Font);
            else if (ModelTable != null)
                textSize = g.MeasureString(ModelTable.Name, Font);

            int fullHeight = GetFullHeight((int)textSize.Height);

            //if (IsExpanded)
            //{
            //    rectMain = new Rectangle(pos, new Size(width, fullHeight));
            //}
            //else
            //{
            rectMain = new Rectangle(pos, new Size(width, fullHeight));
            GPath = new GraphicsPath();
            GPath.AddRectangle(rectMain);

            Brush backBrush = new LinearGradientBrush(rectMain, BackColor1, BackColor2, 90F);
            g.FillPath(backBrush, GPath); // main rectangle background

            if (IsFocused)
            {
                Rectangle borderRect = rectMain;
                borderRect.Width -= 1;
                borderRect.Height -= 1;
                //borderRect.Inflate(-1, -1);
                //g.DrawPath(new Pen(Color.White, 1F), GPath); // border
                g.DrawRectangle(new Pen(Color.FromArgb(180, 180, 180), 1F), borderRect);
            }
            //else
            //{
            //    Rectangle borderRect = rectMain;
            //    //borderRect.Inflate(-1, -1);
            //    //g.DrawPath(new Pen(Color.White, 1F), GPath); // border
            //    g.DrawRectangle(new Pen(Color.Black, 1F), borderRect);
            //}
            //else
            //g.DrawPath(new Pen(Color.Black), GPath); // border

            //}
            if (ModelTable != null)
            {
                Point textPos = Point.Add(pos, new Size(35, 2));

                if (DatabaseTable == null)
                {
                    // TODO: draw delete image to left of table-name.
                }
                else
                {
                    SizeF modelTableTextSize = g.MeasureString(ModelTable.Name, Font);
                    Point trianglePoint = new Point(textPos.X - 10, textPos.Y + 6);
                    DrawTriangle(g, trianglePoint, TableExpanded, TextBrush);
                    GPathModelTableLabel = new GraphicsPath();
                    GPathModelTableLabel.AddRectangle(new Rectangle(textPos.X - 10, textPos.Y, (int)modelTableTextSize.Width + 10, (int)modelTableTextSize.Height));
                }
                g.DrawString(ModelTable.Name, Font, TextBrush, textPos);
            }
            if (DatabaseTable != null)
            {
                Point textPos = Point.Add(pos, new Size(rectMain.Size.Width - (int)textSize.Width - 100, 2));
                //textPos.Offset(-1 * (int)textSize.Width - 20, 2);

                if (ModelTable == null)
                {
                    // TODO: draw delete image to right of table-name.
                }
                else
                {
                    SizeF dbTableTextSize = g.MeasureString(DatabaseTable.Name, Font);
                    Point trianglePoint = new Point(textPos.X - 10, textPos.Y + 6);
                    DrawTriangle(g, trianglePoint, TableExpanded, TextBrush);
                    GPathDatabaseTableLabel = new GraphicsPath();
                    GPathDatabaseTableLabel.AddRectangle(new Rectangle(textPos.X - 10, textPos.Y, (int)dbTableTextSize.Width + 10, (int)dbTableTextSize.Height));
                }
                g.DrawString(DatabaseTable.Name, Font, TextBrush, textPos);
            }
            if (ModelTable != null &&
                DatabaseTable != null)
            {
                // TODO: draw two arrows
                Point imagePos = pos;
                imagePos.Offset(width / 2 - OrangeArrowRightImageLarge.Width * 5, 0);
                g.DrawImage(OrangeArrowRightImageLarge, new Rectangle(imagePos, new Size(OrangeArrowRightImageLarge.Width, OrangeArrowRightImageLarge.Height)));

                imagePos = pos;
                imagePos.Offset(width / 2 + OrangeArrowLeftImageLarge.Width * 4, 0);
                g.DrawImage(OrangeArrowLeftImageLarge, new Rectangle(imagePos, new Size(OrangeArrowLeftImageLarge.Width, OrangeArrowLeftImageLarge.Height)));
            }
            else if (ModelTable != null)
            {
                Point imagePos = pos;
                imagePos.Offset(1, 1);
                Rectangle deleteRect = new Rectangle(imagePos, new Size(DeleteImage.Width, DeleteImage.Height));
                GPathDeleteModelTable = new GraphicsPath();
                GPathDeleteModelTable.AddRectangle(deleteRect);
                g.DrawImage(DeleteImage, deleteRect);

                // TODO: draw centre arrow pointing right
                imagePos = pos;
                imagePos.Offset(width / 2 - GreenArrowRightImageLarge.Width / 2, 0);
                g.DrawImage(GreenArrowRightImageLarge, new Rectangle(imagePos, new Size(GreenArrowRightImageLarge.Width, GreenArrowRightImageLarge.Height)));

                // TODO: draw hyperlink to select Database table
            }
            else if (DatabaseTable != null)
            {
                Point imagePos = pos;
                imagePos.Offset(width - DeleteImage.Width - 1, 1);
                Rectangle deleteRect = new Rectangle(imagePos, new Size(DeleteImage.Width, DeleteImage.Height));
                GPathDeleteDatabaseTable = new GraphicsPath();
                GPathDeleteDatabaseTable.AddRectangle(deleteRect);
                g.DrawImage(DeleteImage, deleteRect);

                // TODO: draw centre arrow pointing left
                imagePos = pos;
                imagePos.Offset(width / 2 - GreenArrowLeftImageLarge.Width / 2, 0);
                g.DrawImage(GreenArrowLeftImageLarge, new Rectangle(imagePos, new Size(GreenArrowLeftImageLarge.Width, GreenArrowLeftImageLarge.Height)));

                // TODO: draw hyperlink to select Model table
            }
            if (TableExpanded)
            {
                if (ModelTable != null && DatabaseTable != null)
                {
                    Point detailsPoint = pos;
                    detailsPoint.Offset(30, (int)textSize.Height + 5);
                    DrawTableDetails(g, detailsPoint, width, true);

                    detailsPoint = Point.Add(pos, new Size(rectMain.Size.Width - (int)textSize.Width - 100, 2));
                    detailsPoint.Offset(0, (int)textSize.Height + 5);
                    DrawTableDetails(g, detailsPoint, width, false);
                }
            }
            return rectMain;
        }

        private int GetFullHeight(int textHeight)
        {
            if (TableExpanded)
            {
                return Gap + textHeight * 5 + Gap;
            }
            else
            {
                return Gap + textHeight + Gap;
            }
        }

        private int DrawTableDetails(Graphics g, Point pos, int width, bool isModel)
        {
            Point startPos = pos;

            startPos.Offset(0, DrawColumns(g, startPos, width, isModel));
            startPos.Offset(0, DrawKeys(g, startPos, width, isModel));
            startPos.Offset(0, DrawIndexes(g, startPos, width, isModel));
            startPos.Offset(0, DrawRelationships(g, startPos, width, isModel));

            return startPos.Y - pos.Y;
        }

        private int DrawColumns(Graphics g, Point pos, int width, bool isModel)
        {
            if (ColumnMatches.Count > 0)
            {
                Point trianglePoint = new Point(pos.X, pos.Y + 6);
                DrawTriangle(g, trianglePoint, ColumnsExpanded, TextBrush);
                g.DrawString("Columns", Font, TextBrush, pos.X + 10, pos.Y);
                SizeF textSize = g.MeasureString("Columns", Font);

                if (isModel)
                {
                    GPathModelColumnsLabel = new GraphicsPath();
                    GPathModelColumnsLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                else
                {
                    GPathDbColumnsLabel = new GraphicsPath();
                    GPathDbColumnsLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                foreach (var col in ColumnMatches)
                {
                    pos.Y += (int)textSize.Height;

                    if (isModel)
                    {
                        if (col.ModelColumn != null)
                            g.DrawString(col.ModelColumn.Name, Font, TextBrush, pos.X + 15, pos.Y);
                        //else
                        //kk;
                    }
                    else
                        if (col.DatabaseColumn != null)
                            g.DrawString(col.ModelColumn.Name, Font, TextBrush, pos.X + 10, pos.Y);
                }
                return (int)textSize.Height;
            }
            return pos.Y;
        }

        private int DrawKeys(Graphics g, Point pos, int width, bool isModel)
        {
            if (KeyMatches.Count > 0)
            {
                Point trianglePoint = new Point(pos.X, pos.Y + 6);
                DrawTriangle(g, trianglePoint, KeysExpanded, TextBrush);
                g.DrawString("Keys", Font, TextBrush, pos.X + 10, pos.Y);
                SizeF textSize = g.MeasureString("Keys", Font);

                if (isModel)
                {
                    GPathModelKeysLabel = new GraphicsPath();
                    GPathModelKeysLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                else
                {
                    GPathDbKeysLabel = new GraphicsPath();
                    GPathDbKeysLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                return (int)textSize.Height;
            }
            return pos.Y;
        }

        private int DrawIndexes(Graphics g, Point pos, int width, bool isModel)
        {
            if (IndexMatches.Count > 0)
            {
                Point trianglePoint = new Point(pos.X, pos.Y + 6);
                DrawTriangle(g, trianglePoint, IndexesExpanded, TextBrush);
                g.DrawString("Indexes", Font, TextBrush, pos.X + 10, pos.Y);
                SizeF textSize = g.MeasureString("Indexes", Font);

                if (isModel)
                {
                    GPathModelIndexesLabel = new GraphicsPath();
                    GPathModelIndexesLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                else
                {
                    GPathDbIndexesLabel = new GraphicsPath();
                    GPathDbIndexesLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                return (int)textSize.Height;
            }
            return pos.Y;
        }

        private int DrawRelationships(Graphics g, Point pos, int width, bool isModel)
        {
            if (RelationshipMatches.Count > 0)
            {
                Point trianglePoint = new Point(pos.X, pos.Y + 6);
                DrawTriangle(g, trianglePoint, RelationshipsExpanded, TextBrush);
                g.DrawString("Relationships", Font, TextBrush, pos.X + 10, pos.Y);
                SizeF textSize = g.MeasureString("Relationships", Font);

                if (isModel)
                {
                    GPathModelRelationshipsLabel = new GraphicsPath();
                    GPathModelRelationshipsLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                else
                {
                    GPathDbRelationshipsLabel = new GraphicsPath();
                    GPathDbRelationshipsLabel.AddRectangle(new Rectangle(pos.X, pos.Y, (int)textSize.Width + 10, (int)textSize.Height));
                }
                return (int)textSize.Height;
            }
            return pos.Y;
        }

        internal void ProcessMouseMove(MouseEventArgs e, ref bool mustRedraw, ref bool oneIsAlreadyFocused)
        {
            if (this.GPath == null)
                return;

            bool isFocused = false;
            bool shapeHasFocus = this.GPath.IsVisible(e.X, e.Y);

            if (this.IsFocused != shapeHasFocus)
            {
                this.IsFocused = shapeHasFocus;
                mustRedraw = true;
            }
            if (oneIsAlreadyFocused)
                isFocused = false;

            if (isFocused) oneIsAlreadyFocused = true;
        }

        internal void ProcessMouseClick(MouseEventArgs e, ref bool mustRedraw)
        {
            if (GPath == null || !GPath.IsVisible(e.X, e.Y))
                return;

            if (GPathModelTableLabel != null && GPathModelTableLabel.IsVisible(e.X, e.Y))
                TableExpanded = !TableExpanded;
            else if (GPathDatabaseTableLabel != null && GPathDatabaseTableLabel.IsVisible(e.X, e.Y))
                TableExpanded = !TableExpanded;
            else if (GPathModelColumnsLabel != null && GPathModelColumnsLabel.IsVisible(e.X, e.Y))
                ColumnsExpanded = !ColumnsExpanded;
            else if (GPathModelIndexesLabel != null && GPathModelIndexesLabel.IsVisible(e.X, e.Y))
                IndexesExpanded = !IndexesExpanded;
            else if (GPathModelKeysLabel != null && GPathModelKeysLabel.IsVisible(e.X, e.Y))
                KeysExpanded = !KeysExpanded;
            else if (GPathModelRelationshipsLabel != null && GPathModelRelationshipsLabel.IsVisible(e.X, e.Y))
                RelationshipsExpanded = !RelationshipsExpanded;
            else if (GPathDbColumnsLabel != null && GPathDbColumnsLabel.IsVisible(e.X, e.Y))
                ColumnsExpanded = !ColumnsExpanded;
            else if (GPathDbIndexesLabel != null && GPathDbIndexesLabel.IsVisible(e.X, e.Y))
                IndexesExpanded = !IndexesExpanded;
            else if (GPathDbKeysLabel != null && GPathDbKeysLabel.IsVisible(e.X, e.Y))
                KeysExpanded = !KeysExpanded;
            else if (GPathDbRelationshipsLabel != null && GPathDbRelationshipsLabel.IsVisible(e.X, e.Y))
                RelationshipsExpanded = !RelationshipsExpanded;
            else if (GPathDeleteDatabaseTable != null && GPathDeleteDatabaseTable.IsVisible(e.X, e.Y))
                MessageBox.Show("Delete DB table");
            else if (GPathDeleteModelTable != null && GPathDeleteModelTable.IsVisible(e.X, e.Y))
                MessageBox.Show("Delete model table");
        }

        internal static void DrawTriangle(Graphics g, Point point, bool isVertical, Brush textBrush)
        {
            int size = 3;
            GraphicsPath path = new GraphicsPath();

            if (isVertical)
            {
                Point point1 = new Point(point.X, point.Y - size);
                Point point2 = new Point(point.X + size * 2, point.Y - size);
                Point point3 = new Point(point.X + size, point.Y + size);
                path.AddPolygon(new Point[] { point1, point2, point3 });
            }
            else
            {
                Point point1 = new Point(point.X, point.Y - size);
                Point point2 = new Point(point.X + size * 2, point.Y);
                Point point3 = new Point(point.X, point.Y + size);
                path.AddPolygon(new Point[] { point1, point2, point3 });
            }
            g.FillPath(textBrush, path);
        }
    }
}
