using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.Controls.Diagramming.Shapes;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    public partial class EntityReferenceDiagram : UserControl
    {
        private static Image ExpandImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.split_video24.png"));
        private static Image CollapseImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.join_video24.png"));
        private EntityImpl _Entity;
        UserControls.FormReferenceEditor2 _RefEditorForm;
        private Rectangle CurrentEndRectangle;
        private bool KeepMainShapeFull = false;

        public EntityReferenceDiagram()
        {
            InitializeComponent();

            this.BackColor = Color.Black;
            shapeCanvas1.LineEndWithFocusChanged += new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas.LineEndWithFocusChangedDelegate(this.shapeCanvas1_LineEndWithFocusChanged);
        }

        public EntityImpl Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;

                if (_Entity != null)
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
            if (Entity == null)
                return;

            Cursor = Cursors.WaitCursor;
            Font boldFont = new Font(Font, FontStyle.Bold);
            Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);

            RawShape centreShape = new RawEntity(Entity.Name, boldFont, Entity);
            centreShape.MouseClick += new MouseEventHandler(centreShape_MouseClick);

            List<RawShape> topLevelShapes = new List<RawShape>();
            List<RawShape> bottomLevelShapes = new List<RawShape>();

            foreach (ReferenceImpl reference in Entity.References)
            {
                RawShape referencedEntityShape;
                CustomLineCap startCap;
                CustomLineCap endCap;

                if (reference.Entity1 == Entity)
                {
                    referencedEntityShape = new RawEntity(reference.Entity2.Name, boldFont, reference.Entity2);
                    string end1 = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1) ? "1" : "m";
                    startCap = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1) ? LineCaps.One : LineCaps.Many;
                    endCap = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2) ? LineCaps.One : LineCaps.Many;
                    referencedEntityShape.OriginatingLineStyle.StartTextDataMember = "End2Name";
                    referencedEntityShape.OriginatingLineStyle.EndTextDataMember = "End1Name";
                    referencedEntityShape.OriginatingLineStyle.MouseOverEnd1 += new LinkLine.MouseEndDelegate(OriginatingLineStyle_MouseOverEnd2);
                    referencedEntityShape.OriginatingLineStyle.MouseOverEnd2 += new LinkLine.MouseEndDelegate(OriginatingLineStyle_MouseOverEnd1); 
                }
                else if (reference.Entity2 == Entity)
                {
                    referencedEntityShape = new RawEntity(reference.Entity1.Name, boldFont, reference.Entity1);
                    string end1 = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2) ? "1" : "m";
                    startCap = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2) ? LineCaps.One : LineCaps.Many;
                    endCap = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1) ? LineCaps.One : LineCaps.Many;
                    referencedEntityShape.OriginatingLineStyle.StartTextDataMember = "End1Name";
                    referencedEntityShape.OriginatingLineStyle.EndTextDataMember = "End2Name";
                    referencedEntityShape.OriginatingLineStyle.MouseOverEnd1 += new LinkLine.MouseEndDelegate(OriginatingLineStyle_MouseOverEnd1);
                    referencedEntityShape.OriginatingLineStyle.MouseOverEnd2 += new LinkLine.MouseEndDelegate(OriginatingLineStyle_MouseOverEnd2);
                }
                else
                    throw new Exception("What the...??!");

                referencedEntityShape.OriginatingLineStyle.LineStyle = DashStyle.Solid;
                referencedEntityShape.OriginatingLineStyle.StartCap = startCap;
                referencedEntityShape.OriginatingLineStyle.EndCap = endCap;
                referencedEntityShape.OriginatingLineStyle.DataObject = reference;
                referencedEntityShape.OriginatingLineStyle.ForeColor = Color.White;
                referencedEntityShape.OriginatingLineStyle.DefaultEndText = "not set";
                bottomLevelShapes.Add(referencedEntityShape);
            }
            // Add empty reference
            RawShape emptyReference = new RawShape("Add reference...", boldUnderlineFont)
            {
                BackColor1 = Color.White,
                BackColor2 = Color.White,
                BorderColor = Color.Gray,
                ForeColor = Color.Gray,
                FocusForeColor = Color.Blue,
                FocusBackColor1 = Color.WhiteSmoke,
                FocusBackColor2 = Color.White,
                FocusBorderColor = Color.DarkGray,
                Cursor = Cursors.Hand,
                OriginatingLineStyle = null,// new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow),
                Tag = null
            };
            //emptyReference.OriginatingLineStyle.ForeColor = Color.White;
            emptyReference.MouseClick += new MouseEventHandler(emptyReference_MouseClick);
            bottomLevelShapes.Add(emptyReference);

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
            List<Entity> unavailableEntities = new List<Entity>();

            foreach (ReferenceImpl reference in Entity.References)
            {
                if (reference.Entity1 == Entity)
                    unavailableEntities.Add(reference.Entity2);
                else if (reference.Entity2 == Entity)
                    unavailableEntities.Add(reference.Entity1);
            }
            UserControls.FormSelectEntity form = new UserControls.FormSelectEntity(Entity, unavailableEntities, null, "Select parent entity");
            form.ShowDialog();

            if (form.SelectedEntity != null)
            {
                Entity.AddReference(new ReferenceImpl(Entity, form.SelectedEntity));
                Populate();
            }
        }

        private void OriginatingLineStyle_MouseOverEnd1(object sender, MouseEventArgs e, Rectangle endRectangle)
        {
            ProcessMouseOverEndOfLine(sender, e, endRectangle, true);
        }

        private void OriginatingLineStyle_MouseOverEnd2(object sender, MouseEventArgs e, Rectangle endRectangle)
        {
            ProcessMouseOverEndOfLine(sender, e, endRectangle, false);
        }

        private void ProcessMouseOverEndOfLine(object sender, MouseEventArgs e, Rectangle endRectangle, bool isEnd1)
        {
            int offset = 0;
            Point pt = new Point(endRectangle.X, endRectangle.Y + endRectangle.Height);// PointToClient(Cursor.Position);// e.Location;
            pt.Offset(offset, offset);

            if (endRectangle != CurrentEndRectangle || !RefEditorForm.Visible)
            {
                CurrentEndRectangle = endRectangle;
                LinkLine line = (LinkLine)sender;
                ReferenceImpl reference = (ReferenceImpl)line.DataObject;
                RefEditorForm.Fill(reference, isEnd1);
                //RefEditorForm.Fill((ReferenceImpl)lineEndWithFocus.Line.DataObject, lineEndWithFocus.EndType == ShapeCanvas.LineEndWithFocus.EndTypes.Start);

                if (pt.X + RefEditorForm.Width < this.Width)
                    RefEditorForm.Location = pt;
                else
                {
                    pt.Offset(-1 * RefEditorForm.Width + endRectangle.Width + offset * 2, 0);
                    RefEditorForm.Location = pt;
                }
                if (pt.Y + RefEditorForm.Height < this.Height)
                    RefEditorForm.Location = pt;
                else
                {
                    pt.Offset(0, -1 * RefEditorForm.Height - endRectangle.Height + offset * 2);
                    RefEditorForm.Location = pt;
                }
                RefEditorForm.Visible = true;
                RefEditorForm.Refresh();
            }
        }

        UserControls.FormReferenceEditor2 RefEditorForm
        {
            get
            {
                if (_RefEditorForm == null)
                {
                    _RefEditorForm = new UserControls.FormReferenceEditor2();
                    _RefEditorForm.Visible = false;
                    Controls.Add(_RefEditorForm);
                    _RefEditorForm.BringToFront();
                }
                return _RefEditorForm;
            }
        }

        private string GetCardinalityString(object referenceObject)
        {
            Reference reference = (Reference)referenceObject;
            string end2 = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1) ? "1" : "m";
            string end1 = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2) ? "1" : end2 == "1" ? "m" : "n";
            return string.Format("{0}:{1}", end1, end2);
        }

        private void shapeCanvas1_Load(object sender, EventArgs e)
        {

        }

        private void shapeCanvas1_LineEndWithFocusChanged(ShapeCanvas.LineEndWithFocus lineEndWithFocus)
        {
            if (lineEndWithFocus == null || lineEndWithFocus.Line == null)
            {
                RefEditorForm.Visible = false;
                return;
            }
            return;
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
