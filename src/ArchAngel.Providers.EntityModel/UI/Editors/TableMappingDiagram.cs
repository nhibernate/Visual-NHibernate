using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
//using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using Slyce.Common.Controls.Diagramming.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class TableMappingDiagram : UserControl
	{
		public delegate void EntityActionDelegate(Entity entity, Table table);
		public delegate void EntityCreatedDelegate(Table fromTable);
		public delegate void TableActionDelegate(Table nextTable);
		public delegate void RelationshipActionDelegate(Relationship relationship);
		public event EntityActionDelegate EntityAdded;
		public event EntityActionDelegate EntityRemoved;
		public event EntityCreatedDelegate EntityCreated;
		public event TableActionDelegate TableDeleted;
		public event TableActionDelegate TableSelected;
		public event EntityActionDelegate EntitySelected;
		public event RelationshipActionDelegate RelationshipDeleted;

		private Table _Table;
		private static Image FilterImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.filter16.png"));
		private static Image FilterImageFocused = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.filter16_h.png"));
		private static Image FilterImageDisabled = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.filter16_d.png"));
		private static Image EntityImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.square_light_blue_16.png"));
		private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
		private static Image ViewImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.print_preview_16.png"));
		private static Image DeleteImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.delete_x_16.png"));
		private static Image DeleteImageHover = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.delete_x_16_h.png"));
		private static Image AddImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.plus_16.png"));
		private static Image AddImageHover = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.plus_16_h.png"));
		private static Image InfoImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.about_16.png"));
		private static Image ExpandImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.split_video24.png"));
		private static Image CollapseImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.join_video24.png"));

		private LinkLine SelectedLinkLine = null;
		private Entity SelectedEntity = null;
		private Relationship SelectedRelationship = null;
		//private Slyce.Common.Controls.FloatingToolstrip TableToolstrip = new Slyce.Common.Controls.FloatingToolstrip();
		//private Slyce.Common.Controls.FloatingToolstrip EntityToolstrip = new Slyce.Common.Controls.FloatingToolstrip();
		public List<Entity> MappedEntities = new List<Entity>();
		private UserControls.FormRelationshipEditor _RelationshipEditorForm;
		private UserControls.FormEntityMappingEditor MappingEditor = new UserControls.FormEntityMappingEditor();
		private UserControls.FormTableRelationshipEditor _TableRelationshipEditor;
		public bool ShowMappedEntities = true;
		public bool ShowRelatedtables = true;
		private bool KeepMainShapeFull = true;
		private RawCategory CategoryColumns;
		private Font CategoryFont;
		private Font PropertyFont;
		private RawTable MainShape;
		private Point ScrollPosition = new Point(0, 0);
		private bool BusyPopulating = false;

		public TableMappingDiagram()
		{
			InitializeComponent();

			this.BackColor = Color.Black;

			CategoryFont = new Font(Font.FontFamily, 7.5F, FontStyle.Regular);
			PropertyFont = new Font(Font.FontFamily, 7F, FontStyle.Regular);
			CategoryColumns = new RawCategory("Columns", CategoryFont, PropertyFont, MainShape);
			CategoryColumns.IsExpanded = true;

			shapeCanvas1.MouseClick += new MouseEventHandler(shapeCanvas1_MouseClick);
			shapeCanvas1.LineEndWithFocusChanged += new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas.LineEndWithFocusChangedDelegate(this.shapeCanvas1_LineEndWithFocusChanged);
			shapeCanvas1.MouseMove += new MouseEventHandler(shapeCanvas1_MouseMove);

			//PopulateTableToolstrip();
			//PopulateEntityToolstrip();

			MappingEditor.Visible = false;
			MappingEditor.VisibleChanged += new EventHandler(MappingEditor_VisibleChanged);

			this.Controls.Add(MappingEditor);
		}

		private UserControls.FormTableRelationshipEditor TableRelationshipEditor
		{
			get
			{
				if (_TableRelationshipEditor == null)
				{
					_TableRelationshipEditor = new UserControls.FormTableRelationshipEditor();
					_TableRelationshipEditor.Visible = false;
					_TableRelationshipEditor.RelationshipAdded += new EventHandler(_TableRelationshipEditor_RelationshipAdded);
					Controls.Add(_TableRelationshipEditor);
					_TableRelationshipEditor.BringToFront();
				}
				return _TableRelationshipEditor;
			}
		}

		void _TableRelationshipEditor_RelationshipAdded(object sender, EventArgs e)
		{
			Populate();
		}

		private void shapeCanvas1_MouseClick(object sender, MouseEventArgs e)
		{
			if (RelationshipEditorForm.Visible && !RelationshipEditorForm.Bounds.Contains(e.Location))
			{
				Slyce.Common.Utility.SuspendPainting(this);
				RelationshipEditorForm.Visible = false;
				Slyce.Common.Utility.ResumePainting(this);
			}
			if (MappingEditor.Visible && !MappingEditor.Bounds.Contains(e.Location))
			{
				Slyce.Common.Utility.SuspendPainting(this);
				MappingEditor.Visible = false;
				Slyce.Common.Utility.ResumePainting(this);
			}
			//if (TableRelationshipEditor.Visible && !TableRelationshipEditor.Bounds.Contains(e.Location))
			//    TableRelationshipEditor.Visible = false;
		}

		private void MappingEditor_VisibleChanged(object sender, EventArgs e)
		{
			//Populate();
		}

		//private void HideAllFloatingToolstrips()
		//{
		//    TableToolstrip.Visible = false;
		//    EntityToolstrip.Visible = false;
		//}

		//private void PopulateTableToolstrip()
		//{
		//    Slyce.Common.Controls.FloatingToolstrip.MenuItem item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Delete", DeleteImage, DeleteImageHover, CenterTableDelete_Click, TableToolstrip);
		//    TableToolstrip.Items.Add(item);

		//    TableToolstrip.Visible = false;
		//    TableToolstrip.LayoutStyle = Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Vertical;
		//    TableToolstrip.BackColor = Color.Transparent;
		//    this.Controls.Add(TableToolstrip);
		//}

		//private void PopulateEntityToolstrip()
		//{
		//    Slyce.Common.Controls.FloatingToolstrip.MenuItem item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Delete this mapping", DeleteImage, DeleteImageHover, EntityUnmap_Click, EntityToolstrip);
		//    EntityToolstrip.Items.Add(item);

		//    EntityToolstrip.Visible = false;
		//    EntityToolstrip.LayoutStyle = Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal;
		//    EntityToolstrip.BackColor = Color.Transparent;
		//    EntityToolstrip.Offset = 1;
		//    this.Controls.Add(EntityToolstrip);
		//}

		//private void EntityUnmap_Click(object sender, EventArgs e)
		//{
		//    EntityToolstrip.Visible = false;

		//    if (EntityRemoved != null)
		//    {
		//        Slyce.Common.Controls.FloatingToolstrip.MenuItem menuItem = (Slyce.Common.Controls.FloatingToolstrip.MenuItem)sender;
		//        EntityRemoved((Entity)menuItem.Tag);
		//    }
		//}

		private void CenterTableDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("Delete this table [{0}]?", Table.Name), "Delete table", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			//TableToolstrip.Visible = false;
			Table nextTable = null;

			foreach (Table table in Table.Database.Tables)
			{
				if (table != Table)
				{
					nextTable = table;
					break;
				}
			}
			Table.DeleteSelf();

			if (TableDeleted != null)
				TableDeleted(nextTable);
		}

		public Table Table
		{
			get { return _Table; }
			set
			{
				_Table = value;

				if (_Table != null)
				{
					ScrollPosition = new Point(0, 0);
					Populate();
				}
			}
		}

		private void Populate()
		{
			if (Table == null || BusyPopulating)
				return;

			try
			{
				BusyPopulating = true;
				Slyce.Common.Utility.SuspendPainting(this);
				bool hasMultiSchemas = Table.Database.GetSchemas().Count() > 1;
				//HideAllFloatingToolstrips();
				Cursor = Cursors.WaitCursor;
				Font boldFont = new Font(Font, FontStyle.Bold);
				Font subTextFont = new Font(Font.FontFamily, Font.Size - 0.6F, FontStyle.Regular);
				MainShape = new RawTable(shapeCanvas1, Table.Name, boldFont, CategoryFont, PropertyFont, Table);

				if (hasMultiSchemas)
					MainShape.SubText = string.Format("  ({0}) ", Table.Schema);

				MainShape.FontSubText = subTextFont;
				MainShape.Icon = Table.IsView ? ViewImage : TableImage;
				//MainShape.Enter += new EventHandler(centreShape_Enter);
				//MainShape.Leave += new EventHandler(centreShape_Leave);
				MainShape.MouseClick += new MouseEventHandler(centreShape_MouseClick);
				CategoryColumns.Parent = MainShape;
				CategoryColumns.Properties.Clear();

				MainShape.Categories.Add(CategoryColumns);

				if (Table.Columns.Count > 0)
				{
					foreach (var column in Table.Columns)
					{
						RawProperty prop = new RawProperty(column.Name, column);

						if (column.InPrimaryKey)
						{
							prop.ImageColor = Color.OrangeRed;
							prop.ImageType = RawProperty.ImageTypes.Key;
						}
						prop.Click += new MouseEventHandler(Column_Click);
						prop.DoubleClick += new MouseEventHandler(Column_DoubleClick);
						CategoryColumns.Properties.Add(prop);
					}
				}

				List<RawShape> bottomLevelShapes = new List<RawShape>();
				List<RawShape> rightAlignedShapes = new List<RawShape>();
				Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);

				//TheEntity.Discriminator.RootGrouping.
				#region Mapped Entities

				if (ShowMappedEntities)
				{
					foreach (EntityImpl entity in MappedEntities.OrderBy(e => e.Name))
					{
						RawEntity entityShape = new RawEntity(shapeCanvas1, entity.Name, boldFont, entity);

						CustomLineCap startCap = LineCaps.None;//.SolidArrow;
						CustomLineCap endCap = LineCaps.None;
						entityShape.OriginatingLineStyle.LineStyle = DashStyle.Solid;
						entityShape.Cursor = Cursors.Hand;
						//entityShape.OriginatingLineStyle.MiddleText = GetDiscriminatorText(entityShape);
						entityShape.OriginatingLineStyle.StartCap = startCap;
						entityShape.OriginatingLineStyle.EndCap = endCap;
						//entityShape.BackColor1 = Color.Blue;
						//entityShape.BackColor2 = Color.DarkBlue;
						//entityShape.RoundedCorners = false;
						entityShape.Tag = entity;
						entityShape.Icon = EntityImage;
						string discriminatorText = GetDiscriminatorText(entity);
						entityShape.OriginatingLineStyle.MiddleText = discriminatorText;

						if (string.IsNullOrEmpty(discriminatorText))
						{
							entityShape.OriginatingLineStyle.MiddleImage = FilterImageDisabled;
							entityShape.OriginatingLineStyle.MiddleImageFocused = FilterImageDisabled;
						}
						else
						{
							entityShape.OriginatingLineStyle.MiddleImage = FilterImage;
							entityShape.OriginatingLineStyle.MiddleImageFocused = FilterImageFocused;
						}
						entityShape.OriginatingLineStyle.MouseClick += new MouseEventHandler(OriginatingLineStyle_MouseClick);
						entityShape.MouseClick += new MouseEventHandler(entityShape_MouseClick);
						entityShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(MapLine_MiddleImageClick);
						entityShape.OriginatingLineStyle.ForeColor = Color.White;
						entityShape.OriginatingLineStyle.DataObject = entity;
						entityShape.OriginatingLineStyle.ShowMiddleTextOnlyWhenFocused = true;
						//entityShape.Enter += new EventHandler(entityShape_Enter);
						//entityShape.Leave += new EventHandler(entityShape_Leave);
						entityShape.MouseDoubleClick += new MouseEventHandler(entityShape_MouseDoubleClick);

						//#region Setup discriminator
						//if (Table.Discriminator != null && Table.Discriminator.RootGrouping != null)
						//{
						//    ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Condition firstCondition = Table.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

						//    if (firstCondition != null)
						//    {
						//        IColumn column = firstCondition.Column;
						//        ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Operator op = firstCondition.Operator;
						//        string exprText = firstCondition.ExpressionValue.Value;
						//    }
						//}
						//#endregion

						//tableShape.OriginatingLineStyle.MiddleImageMouseOver += new EventHandler(OriginatingLineStyle_MiddleImageMouseOver);
						rightAlignedShapes.Add(entityShape);
					}
					#region Add empty entity
					Slyce.Common.Controls.Diagramming.Shapes.RawShape emptyEntity = new Slyce.Common.Controls.Diagramming.Shapes.RawShape(shapeCanvas1, "Map entity...", boldUnderlineFont)
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
						Tag = null,
						RoundedCorners = false,
						OriginatingLineStyle = null //new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow)
					};
					emptyEntity.MouseClick += new MouseEventHandler(emptyEntity_MouseClick);
					//emptyTable.OriginatingLineStyle.MouseClick += new MouseEventHandler(OriginatingLineStyle_MouseClick);
					rightAlignedShapes.Add(emptyEntity);
					#endregion
				}
				#endregion

				#region Relationships
				if (ShowRelatedtables)
				{
					foreach (Relationship relationship in Table.Relationships.OrderBy(r => r.PrimaryTable == Table ? r.ForeignTable.Name : r.PrimaryTable.Name))
					{
						RawShape relatedTableShape;
						CustomLineCap startCap;
						CustomLineCap endCap;

						if (relationship.PrimaryTable == Table)
						{
							relatedTableShape = new RawTable(shapeCanvas1, relationship.ForeignTable.Name, boldFont, relationship.ForeignTable);

							if (hasMultiSchemas)
								relatedTableShape.SubText = string.Format("  ({0}) ", relationship.ForeignTable.Schema);

							string end1 = relationship.PrimaryKey.IsUnique ? "1" : "m";
							endCap = relationship.ForeignKey.IsUnique ? LineCaps.One : LineCaps.Many;
							startCap = relationship.PrimaryKey.IsUnique ? LineCaps.One : LineCaps.Many;
							relatedTableShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(OriginatingLineStyle_MiddleImageClick2);
							relatedTableShape.Icon = relationship.ForeignTable.IsView ? ViewImage : TableImage;
						}
						else if (relationship.ForeignTable == Table)
						{
							relatedTableShape = new RawTable(shapeCanvas1, relationship.PrimaryTable.Name, boldFont, relationship.PrimaryTable);

							if (hasMultiSchemas)
								relatedTableShape.SubText = string.Format("  ({0}) ", relationship.PrimaryTable.Schema);

							string end1 = relationship.ForeignKey.IsUnique ? "1" : "m";
							endCap = relationship.PrimaryKey.IsUnique ? LineCaps.One : LineCaps.Many;
							startCap = relationship.ForeignKey.IsUnique ? LineCaps.One : LineCaps.Many;
							relatedTableShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(OriginatingLineStyle_MiddleImageClick2);
							relatedTableShape.Icon = relationship.PrimaryTable.IsView ? ViewImage : TableImage;
						}
						else
							throw new Exception("What the...??!");

						relatedTableShape.FontSubText = subTextFont;

						if (relationship.IsUserDefined)
						{
							relatedTableShape.OriginatingLineStyle.LineStyle = DashStyle.Dash;
							relatedTableShape.OriginatingLineStyle.LineColor = Color.Yellow;
						}
						else
						{
							relatedTableShape.OriginatingLineStyle.LineStyle = DashStyle.Solid;
						}
						relatedTableShape.Cursor = Cursors.Hand;
						relatedTableShape.OriginatingLineStyle.StartCap = startCap;
						relatedTableShape.OriginatingLineStyle.EndCap = endCap;
						relatedTableShape.OriginatingLineStyle.DataObject = relationship;
						relatedTableShape.OriginatingLineStyle.ForeColor = Color.White;
						relatedTableShape.OriginatingLineStyle.MiddleImage = InfoImage;
						relatedTableShape.MouseClick += new MouseEventHandler(referencedEntityShape_MouseClick);
						relatedTableShape.MouseDoubleClick += new MouseEventHandler(referencedEntityShape_MouseDoubleClick);
						bottomLevelShapes.Add(relatedTableShape);
					}
					// Add empty relationship
					RawShape emptyTable = new RawShape(shapeCanvas1, "Add relationship...", boldUnderlineFont)
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
						OriginatingLineStyle = null,
						Tag = null
					};
					emptyTable.MouseClick += new MouseEventHandler(emptyTable_MouseClick);
					bottomLevelShapes.Add(emptyTable);
				}
				#endregion

				shapeCanvas1.SwimLane1 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(79, 124, 205), Color.Black, Color.White, 0F, "Related Tables", ShapeCanvas.SwimLaneStyle.Styles.Fill);
				shapeCanvas1.SwimLane3 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(79, 124, 205), Color.Black, Color.White, 180F, "Mapped Entities", ShapeCanvas.SwimLaneStyle.Styles.Fill);
				shapeCanvas1.SwimLane4 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(79, 124, 205), Color.Black, Color.White, 90F, "Mapped Entities", ShapeCanvas.SwimLaneStyle.Styles.Fill);

				shapeCanvas1.BackColor = this.BackColor;
				//shapeCanvas1.DrawStar(centreShape, outerShapes);
				//shapeCanvas1.DrawThreeLayerHorizontal(centreShape, null, outerShapes, false);
				shapeCanvas1.Height = this.Height;
				//shapeCanvas1.DrawThreeLayerVertical(centreShape, null, bottomLevelShapes, rightAlignedShapes);
				shapeCanvas1.KeepMainShapeCentered = true;
				shapeCanvas1.DrawThreeLayerHorizontal(MainShape, bottomLevelShapes, rightAlignedShapes, KeepMainShapeFull);
				//shapeCanvas1.Focus();
			}
			finally
			{
				this.AutoScrollPosition = ScrollPosition;
				Slyce.Common.Utility.ResumePainting(this);
				Cursor = Cursors.Default;
				BusyPopulating = false;
				this.Focus();
				shapeCanvas1.Focus();
			}
		}

		void referencedEntityShape_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				SelectedRelationship = (Relationship)((RawShape)sender).OriginatingLineStyle.DataObject;
				HideAllContextMenuItems();
				mnuDeleteRelationship.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (TableSelected != null)
					TableSelected((Table)((RawShape)sender).Tag);
			}
		}

		void emptyTable_MouseClick(object sender, MouseEventArgs e)
		{
			List<ITable> unavailableTables = new List<ITable>();

			//foreach (Relationship relationship in Table.Relationships)
			//{
			//    if (relationship.PrimaryTable == Table)
			//        unavailableTables.Add(relationship.ForeignTable);
			//    else if (relationship.ForeignTable == Table)
			//        unavailableTables.Add(relationship.PrimaryTable);
			//}
			TableRelationshipEditor.Fill(null, Table, null, unavailableTables);

			#region Centre the editor
			if (TableRelationshipEditor.Height < this.Height)
				TableRelationshipEditor.Top = (this.Height - TableRelationshipEditor.Height) / 2;
			else
				TableRelationshipEditor.Top = 10;

			if (TableRelationshipEditor.Width < this.Width)
				TableRelationshipEditor.Left = (this.Width - TableRelationshipEditor.Width) / 2;
			else
				TableRelationshipEditor.Left = 10;
			#endregion

			TableRelationshipEditor.Visible = true;
			TableRelationshipEditor.Refresh();

			//if (form.SelectedTable != null)
			//{
			//    ITable selectedTable = form.SelectedTable;
			//    Relationship newRelationship = new RelationshipImpl()
			//        {
			//            PrimaryTable = Table,
			//            ForeignTable = form.SelectedTable,

			//        };

			//    newRelationship.Name = string.Format("FK_NH_{0}_{1}", Table.Name, form.SelectedTable.Name);
			//    newRelationship.AddThisTo(Table, form.SelectedTable);
			//    newRelationship.Database = Table.Database;
			//    //newRelationship.ForeignCardinality =
			//    //newRelationship.ForeignKey = 
			//    newRelationship.IsUserDefined = true;
			//    //newRelationship.PrimaryCardinality = 
			//    //newRelationship.PrimaryKey = 
			//    newRelationship.Schema = Table.Schema;
			//    //newRelationship.SetForeignEnd(
			//    //newRelationship.SetPrimaryEnd(
			//    //newRelationship.SourceCardinality = 
			//    //newRelationship.SourceEntity = 
			//    //newRelationship.TargetCardinality = 
			//    //newRelationship.TargetEntity = 
			//    Table.Database.AddRelationship(newRelationship);

			//    Populate();
			//}
		}

		private void Column_Click(object sender, MouseEventArgs e)
		{
			//ShowComponentEditor((RawProperty)sender, e.Location);
		}

		private void Column_DoubleClick(object sender, MouseEventArgs e)
		{
			//ShowComponentEditor((RawProperty)sender, e.Location);
		}

		void referencedEntityShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (TableSelected != null)
				TableSelected((Table)((RawShape)sender).Tag);
		}

		void entityShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (EntitySelected != null)
				EntitySelected((EntityImpl)((RawShape)sender).Tag, Table);
		}

		void shapeCanvas1_MouseMove(object sender, MouseEventArgs e)
		{
			//if (RelationshipEditorForm.Visible && !RelationshipEditorForm.Bounds.Contains(e.Location))
			//    RelationshipEditorForm.Visible = false;
		}

		void OriginatingLineStyle_MouseOverMiddleImage1(object sender, MouseEventArgs e, Rectangle endBounds)
		{
			LinkLine line = (LinkLine)sender;
			Relationship relationship = (Relationship)line.DataObject;
			ProcessMouseOverMiddleImage(sender, e.Location, relationship, true);
		}

		void OriginatingLineStyle_MouseOverMiddleImage2(object sender, MouseEventArgs e, Rectangle endBounds)
		{
			LinkLine line = (LinkLine)sender;
			Relationship relationship = (Relationship)line.DataObject;
			ProcessMouseOverMiddleImage(sender, e.Location, relationship, false);
		}

		private void ProcessMouseOverMiddleImage(object sender, Point pt, Relationship relationship, bool isEnd1)
		{
			RelationshipEditorForm.Fill(relationship, isEnd1);
			pt = ((LinkLine)sender).MidPoint;
			pt.Offset(-1 * RelationshipEditorForm.Width / 2, -1 * RelationshipEditorForm.Height / 2);
			//RelationshipEditorForm.Location = pt;

			//////////////////////////

			int y = pt.Y + RelationshipEditorForm.Height / 2 - ScrollPosition.Y;
			if (pt.Y + RelationshipEditorForm.Height / 2 - ScrollPosition.Y < this.Height)
			{
				pt.Offset(0, -1 * ScrollPosition.Y);
				RelationshipEditorForm.Location = pt;
			}
			else
			{
				//pt.Offset(0, -1 * RefEditorForm.Height - endRectangle.Height + offset * 2);
				pt.Y = this.Height - RelationshipEditorForm.Height - 2;
				RelationshipEditorForm.Location = pt;
			}


			RelationshipEditorForm.Visible = true;
			RelationshipEditorForm.Refresh();
		}

		void centreShape_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				HideAllContextMenuItems();
				//mnuStretch.Visible = true;
				//mnuStretch.Text = KeepMainShapeFull ? "Collapse" : "Expand";
				//mnuStretch.Image = KeepMainShapeFull ? CollapseImage : ExpandImage;
				mnuRemoveTable.Visible = true;
				mnuCreateTPHInheritance.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
		}

		private void HideAllContextMenuItems()
		{
			foreach (ToolStripItem item in contextMenuStrip1.Items)
				item.Visible = false;
		}

		//void entityShape_Leave(object sender, EventArgs e)
		//{
		//    //EntityToolstrip.Visible = false;
		//}

		//void entityShape_Enter(object sender, EventArgs e)
		//{
		//    if (EntityToolstrip.LayoutStyle == Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal)
		//    {
		//        //EntityToolstrip.Left = ((RawShape)sender).Left;
		//        //EntityToolstrip.Top = ((RawShape)sender).Top - TableToolstrip.Height;
		//        EntityToolstrip.StartPoint = ((RawShape)sender).Location;
		//    }
		//    else
		//    {
		//        EntityToolstrip.Left = ((RawShape)sender).Right + 1;
		//        EntityToolstrip.Top = ((RawShape)sender).Top;
		//    }
		//    EntityToolstrip.Tag = ((RawShape)sender).Tag;
		//    EntityToolstrip.BringToFront();
		//    EntityToolstrip.Show();
		//    EntityToolstrip.Visible = true;
		//    this.Refresh();
		//}

		//void centreShape_Leave(object sender, EventArgs e)
		//{
		//    TableToolstrip.Visible = false;
		//}

		//void centreShape_Enter(object sender, EventArgs e)
		//{
		//    if (TableToolstrip.LayoutStyle == Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal)
		//    {
		//        TableToolstrip.Left = ((RawShape)sender).Left;
		//        TableToolstrip.Top = ((RawShape)sender).Top - TableToolstrip.Height;
		//    }
		//    else
		//    {
		//        TableToolstrip.Left = ((RawShape)sender).Right + 1;
		//        TableToolstrip.Top = ((RawShape)sender).Top;
		//    }
		//    TableToolstrip.Tag = Table;
		//    TableToolstrip.BringToFront();
		//    TableToolstrip.Show();
		//    TableToolstrip.Visible = true;
		//    this.Refresh();
		//}

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

		void OriginatingLineStyle_MiddleImageClick1(object sender, MouseEventArgs e)
		{
			LinkLine line = (LinkLine)sender;
			Relationship relationship = (Relationship)line.DataObject;
			ProcessMouseOverMiddleImage(sender, e.Location, relationship, true);
		}

		void OriginatingLineStyle_MiddleImageClick2(object sender, MouseEventArgs e)
		{
			LinkLine line = (LinkLine)sender;
			Relationship relationship = (Relationship)line.DataObject;
			ProcessMouseOverMiddleImage(sender, e.Location, relationship, false);
		}

		void MapLine_MiddleImageClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				SelectedLinkLine = (LinkLine)sender;
				EntityImpl entity = (EntityImpl)SelectedLinkLine.DataObject;

				//UserControls.FormDiscriminator form = new UserControls.FormDiscriminator(entity);
				//form.StartPosition = FormStartPosition.CenterParent;//.Manual;
				//form.ShowDialog(this);

				//SelectedLinkLine.MiddleText = GetDiscriminatorText(entity);

				//if (string.IsNullOrEmpty(SelectedLinkLine.MiddleText))
				//{
				//    SelectedLinkLine.MiddleImage = FilterImageDisabled;
				//    SelectedLinkLine.MiddleImageFocused = FilterImageDisabled;
				//}
				//else
				//{
				//    SelectedLinkLine.MiddleImage = FilterImage;
				//    SelectedLinkLine.MiddleImageFocused = FilterImageFocused;
				//}
				Point pt = ((LinkLine)sender).MidPoint;
				pt.Offset(-1 * MappingEditor.Width / 2, -1 * MappingEditor.Height / 2);

				if (pt.Y < 2)
					pt.Offset(0, 5 - pt.Y);

				MappingEditor.EntityOnLeft = false;
				MappingEditor.Entity = entity;
				MappingEditor.Table = Table;
				MappingEditor.Location = pt;
				MappingEditor.BringToFront();
				MappingEditor.Visible = true;
				MappingEditor.Refresh();
			}
			//else
			//{
			//    ShowContextMenuForDiscriminator();
			//}
		}

		private string GetDiscriminatorText(EntityImpl entity)
		{
			if (entity.Parent != null)
			{
				if (entity.Parent.Discriminator != null &&
					EntityImpl.DetermineInheritanceTypeWithParent(entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
				{
					if (entity.Parent.Discriminator.DiscriminatorType == Enums.DiscriminatorTypes.Column)
						return string.Format("{0} = {1}", entity.Parent.Discriminator.ColumnName, entity.DiscriminatorValue);
					else
						return string.Format("Value: {0} Formula: {1}", entity.DiscriminatorValue, entity.Parent.Discriminator.Formula);
				}
			}
			//throw new NotImplementedException("TODO: ddiscriminator stuff");
			//if (entity.Discriminator != null && entity.Discriminator.RootGrouping != null)
			//{
			//    Condition firstCondition = entity.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

			//    if (firstCondition != null)
			//    {
			//        return string.Format("{0} {1} '{2}'", firstCondition.Column.Name, firstCondition.Operator.DisplayText, firstCondition.ExpressionValue.Value);
			//    }
			//}
			return "";
		}

		void entityShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedEntity = (Entity)((RawShape)sender).Tag;

			if (e.Button == MouseButtons.Right)
			{
				foreach (ToolStripItem item in contextMenuStrip1.Items)
				{
					item.Visible = false;
				}
				mnuUnmapEntity.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (EntitySelected != null)
					EntitySelected((EntityImpl)((RawShape)sender).Tag, Table);
			}
		}

		void emptyEntity_MouseClick(object sender, MouseEventArgs e)
		{
			List<Entity> unavailableEntities = new List<Entity>();
			unavailableEntities.AddRange(Table.MappedEntities());

			try
			{
				EntityForm2.BusyPopulating = true;
				UserControls.FormSelectExistingEntity form = new UserControls.FormSelectExistingEntity(Table, unavailableEntities, null, "Select entity to map", false);
				form.ShowDialog(this);

				if (form.SelectedEntity != null && EntityAdded != null)
				{
					EntityForm2.BusyPopulating = false;
					EntityAdded(form.SelectedEntity, Table);
				}
			}
			finally
			{
				EntityForm2.BusyPopulating = false;
			}
		}

		void OriginatingLineStyle_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedLinkLine = (LinkLine)sender;

			//if (e.Button == MouseButtons.Right)
			//    ShowContextMenuForDiscriminator();
		}

		//private void ShowContextMenuForDiscriminator()
		//{
		//    foreach (ToolStripItem item in contextMenuStrip1.Items)
		//    {
		//        item.Visible = false;
		//    }
		//    mnuEditDiscriminator.Visible = true;
		//    mnuRemoveDiscriminator.Visible = true;
		//    contextMenuStrip1.Show(Cursor.Position);
		//}

		//private void mnuEditDiscriminator_Click(object sender, EventArgs e)
		//{

		//}

		//private string GetDiscriminatorText(ITable table)
		//{
		//    if (Table.Discriminator != null && Table.Discriminator.RootGrouping != null)
		//    {
		//        Condition firstCondition = Table.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

		//        if (firstCondition != null && firstCondition.Column.Parent == table)
		//        {
		//            return string.Format("{0} {1} '{2}'", firstCondition.Column.Name, firstCondition.Operator.DisplayText, firstCondition.ExpressionValue.Value);
		//        }
		//    }
		//    return "";
		//}

		//private string GetDiscriminatorText()
		//{
		//    if (Table.Discriminator != null && Table.Discriminator.RootGrouping != null)
		//    {
		//        Condition firstCondition = Table.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

		//        if (firstCondition != null)
		//        {
		//            return string.Format("{0} {1} '{2}'", firstCondition.Column.Name, firstCondition.Operator.DisplayText, firstCondition.ExpressionValue.Value);
		//        }
		//    }
		//    return "";
		//}

		private void mnuRemoveDiscriminator_Click(object sender, EventArgs e)
		{

		}

		private void MappingEditor_SizeChanged(object sender, EventArgs e)
		{
			Populate();
		}

		private void MappingEditor_Resize(object sender, EventArgs e)
		{
			Populate();
		}

		private void shapeCanvas1_LineEndWithFocusChanged(ShapeCanvas.LineEndWithFocus lineEndWithFocus)
		{
			if (lineEndWithFocus == null || lineEndWithFocus.Line == null)
			{
				//RelationshipEditorForm.Visible = false;
				return;
			}
			return;
		}

		private void TableMappingDiagram_Resize(object sender, EventArgs e)
		{
			//if (shapeCanvas1.Height < this.Height)
			//    shapeCanvas1.Height = this.Height;

			shapeCanvas1.Width = this.ClientSize.Width;
		}

		private void mnuStretch_Click(object sender, EventArgs e)
		{
			KeepMainShapeFull = !KeepMainShapeFull;
			Populate();
		}

		private void mnuRemoveTable_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("Delete this table [{0}]?", Table.Name), "Delete table", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			Table nextTable = null;

			foreach (Table table in Table.Database.Tables)
			{
				if (table != Table)
				{
					nextTable = table;
					break;
				}
			}
			Table.DeleteSelf();

			if (TableDeleted != null)
				TableDeleted(nextTable);
		}

		private void mnuUnmapEntity_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("Unmap this entity [{0}]?", SelectedEntity.Name), "Unmap entity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			EntityRemoved(SelectedEntity, Table);
		}

		private void mnuDeleteRelationship_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("Delete this relationship [{0}]?", SelectedRelationship.Name), "Delete relationship", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			RelationshipDeleted(SelectedRelationship);
		}

		private void mnuCreateTPHInheritance_Click(object sender, EventArgs e)
		{
			UserControls.FormInheritanceHierarchy form = new UserControls.FormInheritanceHierarchy(Table);
			form.ShowDialog(this);
		}

		private void TableMappingDiagram_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
				ScrollPosition.Y = e.NewValue;
			else
				ScrollPosition.X = e.NewValue;
		}

		protected override Point ScrollToControl(Control activeControl)
		{
			Point pt = new Point(-1 * ScrollPosition.X, -1 * ScrollPosition.Y);
			return pt;
		}
	}
}
