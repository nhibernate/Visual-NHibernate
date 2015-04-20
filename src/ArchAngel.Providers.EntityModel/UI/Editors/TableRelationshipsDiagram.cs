using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.Controls.Diagramming.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    public partial class TableRelationshipsDiagram : UserControl
    {
        private static Image ExpandImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.split_video24.png"));
        private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
        private static Image CollapseImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.join_video24.png"));
        private static Image InfoImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.about_16.png"));
        UserControls.FormRelationshipEditor _RelationshipEditorForm;
        private Rectangle CurrentEndRectangle;
        private Table _Table;
        private bool KeepMainShapeFull = false;

        public TableRelationshipsDiagram()
        {
            InitializeComponent();

            this.BackColor = Color.Black;
            shapeCanvas1.LineEndWithFocusChanged += new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas.LineEndWithFocusChangedDelegate(this.shapeCanvas1_LineEndWithFocusChanged);
            shapeCanvas1.Click += new EventHandler(shapeCanvas1_Click);
        }

        void shapeCanvas1_Click(object sender, EventArgs e)
        {
            if (RelationshipEditorForm.Visible)
                RelationshipEditorForm.Visible = false;
        }

        public Table Table
        {
            get { return _Table; }
            set
            {
                _Table = value;

                if (_Table != null)
                {
                    Populate();
                }
            }
        }

        private void Populate()
        {
            DrawThreeLayers();
        }

        private void DrawThreeLayers()
        {
            if (Table == null)
                return;

            Cursor = Cursors.WaitCursor;
            Font boldFont = new Font(Font, FontStyle.Bold);
            Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);

            RawTable centreShape = new RawTable(Table.Name, boldFont, Table);
            centreShape.Icon = TableImage;
            centreShape.MouseClick += new MouseEventHandler(centreShape_MouseClick);

            List<RawShape> topLevelShapes = new List<RawShape>();
            List<RawShape> bottomLevelShapes = new List<RawShape>();

            foreach (Relationship relationship in Table.Relationships)
            {
                RawShape referencedEntityShape;
                CustomLineCap startCap;
                CustomLineCap endCap;

                if (relationship.PrimaryTable == Table)
                {
                    referencedEntityShape = new RawTable(relationship.ForeignTable.Name, boldFont, relationship.ForeignTable);
                    string end1 = relationship.PrimaryKey.IsUnique ? "1" : "m";
                    startCap = relationship.ForeignKey.IsUnique ? LineCaps.One : LineCaps.Many;
                    endCap = relationship.PrimaryKey.IsUnique ? LineCaps.One : LineCaps.Many;
                }
                else if (relationship.ForeignTable == Table)
                {
                    referencedEntityShape = new RawTable(relationship.PrimaryTable.Name, boldFont, relationship.PrimaryTable);
                    string end1 = relationship.ForeignKey.IsUnique ? "1" : "m";
                    startCap = relationship.PrimaryKey.IsUnique ? LineCaps.One : LineCaps.Many;
                    endCap = relationship.ForeignKey.IsUnique ? LineCaps.One : LineCaps.Many;
                }
                else
                    throw new Exception("What the...??!");

                referencedEntityShape.Icon = TableImage;
                referencedEntityShape.OriginatingLineStyle.LineStyle = DashStyle.Solid;
                referencedEntityShape.OriginatingLineStyle.StartCap = startCap;
                referencedEntityShape.OriginatingLineStyle.EndCap = endCap;
                referencedEntityShape.OriginatingLineStyle.DataObject = relationship;
                referencedEntityShape.OriginatingLineStyle.ForeColor = Color.White;
                referencedEntityShape.OriginatingLineStyle.MiddleImage = InfoImage;
                referencedEntityShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(OriginatingLineStyle_MiddleImageClick);
                bottomLevelShapes.Add(referencedEntityShape);
            }
            //// Add empty reference
            //RawShape emptyReference = new RawShape("Add relationship...", boldUnderlineFont)
            //{
            //    BackColor1 = Color.White,
            //    BackColor2 = Color.White,
            //    BorderColor = Color.Gray,
            //    ForeColor = Color.Gray,
            //    FocusForeColor = Color.Blue,
            //    FocusBackColor1 = Color.WhiteSmoke,
            //    FocusBackColor2 = Color.White,
            //    FocusBorderColor = Color.DarkGray,
            //    Cursor = Cursors.Hand,
            //    OriginatingLineStyle = null,// new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow),
            //    Tag = null
            //};
            //emptyReference.MouseClick += new MouseEventHandler(emptyReference_MouseClick);
            //bottomLevelShapes.Add(emptyReference);

            //if (vertical)
            //  canvas1.DrawThreeLayerVertical(centreShape, topLevelShapes, bottomLevelShapes);
            //else
            shapeCanvas1.BackColor = this.BackColor;
            //shapeCanvas1.DrawThreeLayerHorizontal(centreShape, topLevelShapes, bottomLevelShapes, true);
            shapeCanvas1.Height = this.Height;
            //shapeCanvas1.DrawStar(centreShape, bottomLevelShapes);
            shapeCanvas1.KeepMainShapeFull = KeepMainShapeFull;
            shapeCanvas1.DrawThreeLayerVertical(centreShape, null, bottomLevelShapes);

            Cursor = Cursors.Default;
        }

        void OriginatingLineStyle_MiddleImageClick(object sender, MouseEventArgs e)
        {
            int offset = 0;
            Point pt = e.Location;
            pt.Offset(offset, offset);

            //if (endRectangle != CurrentEndRectangle || !RelationshipEditorForm.Visible)
            //{
            //CurrentEndRectangle = endRectangle;
            LinkLine line = (LinkLine)sender;
            Relationship relationship = (Relationship)line.DataObject;
            RelationshipEditorForm.Fill(relationship);
            //RelationshipEditorForm.Fill((ReferenceImpl)lineEndWithFocus.Line.DataObject, lineEndWithFocus.EndType == ShapeCanvas.LineEndWithFocus.EndTypes.Start);

            //if (pt.X + RelationshipEditorForm.Width < this.Width)
            //    RelationshipEditorForm.Location = pt;
            //else
            //{
            //    pt.Offset(-1 * RelationshipEditorForm.Width + endRectangle.Width + offset * 2, 0);
            //    RelationshipEditorForm.Location = pt;
            //}
            //if (pt.Y + RelationshipEditorForm.Height < this.Height)
            //    RelationshipEditorForm.Location = pt;
            //else
            //{
            //    pt.Offset(0, -1 * RelationshipEditorForm.Height - endRectangle.Height + offset * 2);
            //    RelationshipEditorForm.Location = pt;
            //}
            pt = ((LinkLine)sender).MidPoint;
            pt.Offset(-1 * RelationshipEditorForm.Width / 2, -1 * RelationshipEditorForm.Height / 2);
            RelationshipEditorForm.Location = pt;
            RelationshipEditorForm.Visible = true;
            RelationshipEditorForm.Refresh();
            //}
        }

        void OriginatingLineStyle_MouseLeave(object sender, MouseEventArgs e)
        {
            if (RelationshipEditorForm.Visible)
                RelationshipEditorForm.Visible = false;
        }

        void centreShape_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HideAllContextMenuItems();
                mnuExpand.Visible = true;
                mnuExpand.Text = KeepMainShapeFull ? "Collapse" : "Expand";
                mnuExpand.Image = KeepMainShapeFull ? CollapseImage : ExpandImage;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void HideAllContextMenuItems()
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Visible = false;
        }

        void emptyReference_MouseClick(object sender, MouseEventArgs e)
        {
            List<ITable> unavailableEntities = new List<ITable>();

            foreach (Relationship relationship in Table.Relationships)
            {
                if (relationship.PrimaryTable == Table)
                    unavailableEntities.Add(relationship.ForeignTable);
                else if (relationship.ForeignTable == Table)
                    unavailableEntities.Add(relationship.PrimaryTable);
            }
            UserControls.FormSelectTable form = new UserControls.FormSelectTable(Table, unavailableEntities, null, "Select parent entity");
            form.ShowDialog();

            if (form.SelectedTable != null)
            {
                Table.AddRelationship(new RelationshipImpl()
                {
                    PrimaryTable = Table,
                    ForeignTable = form.SelectedTable
                });
                Populate();
            }
        }

        private UserControls.FormRelationshipEditor RelationshipEditorForm
        {
            get
            {
                if (_RelationshipEditorForm == null)
                {
                    _RelationshipEditorForm = new UserControls.FormRelationshipEditor();
                    _RelationshipEditorForm.Visible = false;
                    Controls.Add(_RelationshipEditorForm);
                    _RelationshipEditorForm.BringToFront();
                }
                return _RelationshipEditorForm;
            }
        }

        private string GetCardinalityString(object referenceObject)
        {
            Relationship relationship = (Relationship)referenceObject;
            string end2 = ArchAngel.Interfaces.Cardinality.One.Equals(relationship.PrimaryCardinality) ? "1" : "m";
            string end1 = ArchAngel.Interfaces.Cardinality.One.Equals(relationship.ForeignCardinality) ? "1" : end2 == "1" ? "m" : "n";
            return string.Format("{0}:{1}", end1, end2);
        }

        private void shapeCanvas1_LineEndWithFocusChanged(ShapeCanvas.LineEndWithFocus lineEndWithFocus)
        {
            if (lineEndWithFocus == null || lineEndWithFocus.Line == null)
            {
                RelationshipEditorForm.Visible = false;
                return;
            }
            return;
            int offset = 40;
            Point pt = PointToClient(Cursor.Position);// e.Location;
            pt.Offset(-offset, -offset);

            if (!RelationshipEditorForm.Visible)
            {
                RelationshipEditorForm.Fill((Relationship)lineEndWithFocus.Line.DataObject);

                if (pt.X + RelationshipEditorForm.Width < this.Width)
                    RelationshipEditorForm.Location = pt;
                else
                {
                    pt.Offset(-1 * RelationshipEditorForm.Width + offset * 2, 0);
                    RelationshipEditorForm.Location = pt;
                }
                if (pt.Y + RelationshipEditorForm.Height < this.Height)
                    RelationshipEditorForm.Location = pt;
                else
                {
                    pt.Offset(0, -1 * RelationshipEditorForm.Height + offset * 2);
                    RelationshipEditorForm.Location = pt;
                }
                RelationshipEditorForm.Visible = true;
                RelationshipEditorForm.Refresh();
            }
        }

        private void ReferenceEditor_Resize(object sender, EventArgs e)
        {
            Populate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void mnuExpand_Click(object sender, EventArgs e)
        {
            KeepMainShapeFull = !KeepMainShapeFull;
            Populate();
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {

        }

    }
}
