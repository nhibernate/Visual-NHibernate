using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid
{
    public partial class SynchroCanvas : UserControl
    {
        public class TableMatch
        {
            public Table ModelTable { get; set; }
            public Table DatabaseTable { get; set; }

            public TableMatch(Table modelTable, Table databaseTable)
            {
                ModelTable = modelTable;
                DatabaseTable = databaseTable;
            }
        }
        public class ColumnMatch
        {
            public IColumn ModelColumn { get; set; }
            public IColumn DatabaseColumn { get; set; }

            public ColumnMatch(IColumn modelColumn, IColumn databaseColumn)
            {
                ModelColumn = modelColumn;
                DatabaseColumn = databaseColumn;
            }
        }
        public class KeyMatch
        {
            public IKey ModelKey { get; set; }
            public IKey DatabaseKey { get; set; }

            public KeyMatch(IKey modelKey, IKey databaseKey)
            {
                ModelKey = modelKey;
                DatabaseKey = databaseKey;
            }
        }
        public class IndexMatch
        {
            public IIndex ModelIndex { get; set; }
            public IIndex DatabaseIndex { get; set; }

            public IndexMatch(IIndex modelIndex, IIndex databaseIndex)
            {
                ModelIndex = modelIndex;
                DatabaseIndex = databaseIndex;
            }
        }
        public class RelationshipMatch
        {
            public Relationship ModelRelationship { get; set; }
            public Relationship DatabaseRelationship { get; set; }

            public RelationshipMatch(Relationship modelRelationship, Relationship databaseRelationship)
            {
                ModelRelationship = modelRelationship;
                DatabaseRelationship = databaseRelationship;
            }
        }
        private IList<TableMatch> Matches = new List<TableMatch>();
        private IList<TableRow> TableRows = new List<TableRow>();

        public SynchroCanvas()
        {
            InitializeComponent();

            SetStyle(
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint |
                        ControlStyles.OptimizedDoubleBuffer, true);

            this.BackColor = Color.Black;
        }

        public void Fill(IList<TableMatch> matches)
        {
            Matches.Clear();
            Matches = matches;
            TableRows.Clear();

            foreach (TableMatch match in Matches)
                TableRows.Add(new TableRow(match.ModelTable, match.DatabaseTable, this.Font));

            //TableRows[2].TableExpanded = true;
        }

        public void FillChangedTables(
            IList<TableMatch> matches,
            DatabaseMergeResult results)
        {
            Matches.Clear();
            Matches = matches;
            TableRows.Clear();

            foreach (TableMatch match in Matches)
            {
                #region Columns
                IEnumerable<IColumn> removedColumns = results.ColumnOperations.Where(r => r is ColumnRemovalOperation && r.Object.Parent == match.DatabaseTable).Select(r => r.Object);
                IEnumerable<IColumn> addedColumns = results.ColumnOperations.Where(r => r is ColumnAdditionOperation && r.Object.Parent == match.ModelTable).Select(r => r.Object);
                IEnumerable<IColumn> changedColumns = results.ColumnOperations.Where(r => r is ColumnChangeOperation).Select(r => r.Object);
                List<ColumnMatch> columnMatches = new List<ColumnMatch>();

                foreach (var col in removedColumns)
                    columnMatches.Add(new ColumnMatch(null, col));

                foreach (var col in addedColumns)
                    columnMatches.Add(new ColumnMatch(col, null));

                //foreach (var col in changedColumns)
                //    columnMatches.Add(new ColumnMatch(col, null));
                #endregion

                #region Keys
                IEnumerable<IKey> removedKeys = results.KeyOperations.Where(r => r is KeyRemovalOperation && r.Object.Parent == match.DatabaseTable).Select(r => r.Object);
                IEnumerable<IKey> addedKeys = results.KeyOperations.Where(r => r is KeyAdditionOperation && r.Object.Parent == match.ModelTable).Select(r => r.Object);
                IEnumerable<IKey> changedKeys = results.KeyOperations.Where(r => r is KeyChangeOperation).Select(r => r.Object);
                List<KeyMatch> keyMatches = new List<KeyMatch>();

                foreach (var key in removedKeys)
                    keyMatches.Add(new KeyMatch(null, key));

                foreach (var key in addedKeys)
                    keyMatches.Add(new KeyMatch(key, null));

                //foreach (var col in changedColumns)
                //    columnMatches.Add(new ColumnMatch(col, null));
                #endregion

                #region Indexes
                //IEnumerable<IKey> removedKeys = results.KeyOperations.Where(r => r is KeyRemovalOperation && r.Object.Parent == match.DatabaseTable).Select(r => r.Object);
                //IEnumerable<IKey> addedKeys = results.KeyOperations.Where(r => r is KeyAdditionOperation && r.Object.Parent == match.ModelTable).Select(r => r.Object);
                //IEnumerable<IKey> changedKeys = results.KeyOperations.Where(r => r is KeyChangeOperation).Select(r => r.Object);
                List<IndexMatch> indexMatches = new List<IndexMatch>();

                //foreach (var key in removedKeys)
                //    keyMatches.Add(new KeyMatch(null, key));

                //foreach (var key in addedKeys)
                //    keyMatches.Add(new KeyMatch(key, null));

                //foreach (var col in changedColumns)
                //    columnMatches.Add(new ColumnMatch(col, null));
                #endregion

                #region Relationships
                //IEnumerable<IKey> removedKeys = results.KeyOperations.Where(r => r is KeyRemovalOperation && r.Object.Parent == match.DatabaseTable).Select(r => r.Object);
                //IEnumerable<IKey> addedKeys = results.KeyOperations.Where(r => r is KeyAdditionOperation && r.Object.Parent == match.ModelTable).Select(r => r.Object);
                //IEnumerable<IKey> changedKeys = results.KeyOperations.Where(r => r is KeyChangeOperation).Select(r => r.Object);
                List<RelationshipMatch> relationshipMatches = new List<RelationshipMatch>();

                //foreach (var key in removedKeys)
                //    keyMatches.Add(new KeyMatch(null, key));

                //foreach (var key in addedKeys)
                //    keyMatches.Add(new KeyMatch(key, null));

                //foreach (var col in changedColumns)
                //    columnMatches.Add(new ColumnMatch(col, null));
                #endregion

                TableRows.Add(new TableRow(match.ModelTable, match.DatabaseTable, this.Font, columnMatches, keyMatches, indexMatches, relationshipMatches));
            }
            //TableRows[2].TableExpanded = true;
        }

        private void SynchroCanvas_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            Point pos = new Point(0, 0);

            foreach (TableRow row in TableRows)
            {
                pos.Offset(0, row.Draw(g, pos, this.Width).Height);
            }
            if (pos.Y > 0)
                this.Height = pos.Y;
        }

        private void SynchroCanvas_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void SynchroCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            bool mustRedraw = false;
            bool oneIsAlreadyFocused = false;
            //RawShape shapeWithFocus = null;

            foreach (TableRow row in TableRows)
            {
                row.ProcessMouseMove(e, ref mustRedraw, ref oneIsAlreadyFocused);
            }
            if (mustRedraw)
                Refresh();
        }

        private void SynchroCanvas_MouseLeave(object sender, EventArgs e)
        {
            MouseEventArgs ee = new MouseEventArgs(MouseButtons.Left, 0, -100, -100, 0);
            bool mustRedraw = false;
            bool oneIsAlreadyFocused = false;
            //RawShape shapeWithFocus = null;

            foreach (TableRow row in TableRows)
                row.ProcessMouseMove(ee, ref mustRedraw, ref oneIsAlreadyFocused);

            if (mustRedraw)
                Refresh();
        }

        private void SynchroCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            bool mustRedraw = false;

            foreach (TableRow row in TableRows)
                row.ProcessMouseClick(e, ref mustRedraw);

            Refresh();
        }

    }
}
