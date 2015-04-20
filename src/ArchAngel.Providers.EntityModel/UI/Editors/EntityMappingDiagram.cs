using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
//using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;
using Slyce.Common.Controls.Diagramming.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class EntityMappingDiagram : UserControl
	{
		public delegate void TableActionDelegate(ITable table);
		public delegate void EntityActionDelegate(EntityImpl entity);
		public event TableActionDelegate TableAdded;
		public event TableActionDelegate TableRemoved;
		public event EntityActionDelegate EntityDeleted;
		public event TableActionDelegate TableSelected;
		public event EntityActionDelegate EntitySelected;
		public event EntityActionDelegate EntityUpdated;

		private EntityImpl _Entity;
		private static Image FilterImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.filter16.png"));
		private static Image FilterImageDisabled = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.filter16_d.png"));
		private static Image EntityImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.square_light_blue_16.png"));
		private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
		private static Image ViewImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.print_preview_16.png"));
		//private static Image DeleteImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.delete_x_16.png"));
		//private static Image DeleteImageHover = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.delete_x_16_h.png"));
		private static Image ExpandImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.split_video24.png"));
		private static Image CollapseImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.join_video24.png"));
		private static Image EditImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.edit_16.png"));
		private static Image AddComponentImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.insert_16.png"));
		private static Image AddComponentImageHover = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.insert_16_h.png"));
		private LinkLine SelectedLinkLine = null;
		private Table SelectedTable = null;
		private Reference SelectedReference = null;
		private ArchAngel.Providers.EntityModel.Model.EntityLayer.Component SelectedComponent = null;
		public List<ITable> MappedTables = new List<ITable>();
		private bool KeepMainShapeFull = true;
		public bool ShowMappedTables = true;
		public bool ShowReferencedEntities = true;
		public bool ShowInheritance = true;
		private UserControls.FormReferenceEditor2 _RefEditorForm;
		//private UserControls.FormComponentMappingEditor ComponentMappingEditor = new UserControls.FormComponentMappingEditor();
		private UserControls.FormEntityMappingEditor MappingEditor = new UserControls.FormEntityMappingEditor();
		private Rectangle CurrentEndRectangle;
		private Slyce.Common.Controls.FloatingToolstrip TableToolstrip = new Slyce.Common.Controls.FloatingToolstrip();
		private Slyce.Common.Controls.FloatingToolstrip EntityToolstrip = new Slyce.Common.Controls.FloatingToolstrip();
		private Slyce.Common.Controls.FloatingToolstrip ReferencedEntityToolstrip = new Slyce.Common.Controls.FloatingToolstrip();
		private Slyce.Common.Controls.FloatingToolstrip ComponentToolstrip = new Slyce.Common.Controls.FloatingToolstrip();
		private RawEntity MainShape;
		//private RawCategory CategoryComponents;
		private RawCategory CategoryProperties;
		private RawCategory CategoryInheritedProperties;
		private RawCategory CategoryNavigation;
		private Font CategoryFont;
		private Font PropertyFont;
		private bool BusyPopulating = false;
		private Point ScrollPosition = new Point(0, 0);

		public EntityMappingDiagram()
		{
			InitializeComponent();

			this.BackColor = Color.Black;

			CategoryFont = new Font(Font.FontFamily, 7.5F, FontStyle.Regular);
			PropertyFont = new Font(Font.FontFamily, 7F, FontStyle.Regular);
			CategoryProperties = new RawCategory("Properties", CategoryFont, PropertyFont, MainShape);
			CategoryInheritedProperties = new RawCategory("Inherited Properties", CategoryFont, PropertyFont, MainShape);
			//CategoryComponents = new RawCategory("Components", CategoryFont, PropertyFont, MainShape);
			CategoryNavigation = new RawCategory("Navigation", CategoryFont, PropertyFont, MainShape);

			//PopulateTableToolstrip();
			//PopulateEntityToolstrip();
			//PopulateReferencedEntityToolstrip();
			//PopulateComponentToolstrip();

			//shapeCanvas1.LineEndWithFocusChanged += new ShapeCanvas.LineEndWithFocusChangedDelegate(shapeCanvas1_LineEndWithFocusChanged);
			shapeCanvas1.MouseClick += new MouseEventHandler(shapeCanvas1_MouseClick);
			shapeCanvas1.MouseMove += new MouseEventHandler(shapeCanvas1_MouseMove);
			CategoryProperties.MultiPropertiesClicked += new RawCategory.MultiPropertiesClickedDelegate(CategoryProperties_MultiPropertiesClicked);

			RefEditorForm.VisibleChanged += new EventHandler(RefEditorForm_VisibleChanged);
			//ComponentMappingEditor.Visible = false;
			//ComponentMappingEditor.VisibleChanged += new EventHandler(ComponentMappingEditor_VisibleChanged);
			//this.Controls.Add(ComponentMappingEditor);

			MappingEditor.Visible = false;
			MappingEditor.VisibleChanged += new EventHandler(MappingEditor_VisibleChanged);
			this.Controls.Add(MappingEditor);
		}

		private void MappingEditor_VisibleChanged(object sender, EventArgs e)
		{
			Populate();
		}

		private void HideAllFloatingToolstrips()
		{
			TableToolstrip.Visible = false;
			EntityToolstrip.Visible = false;
			ReferencedEntityToolstrip.Visible = false;
			ComponentToolstrip.Visible = false;
		}

		void RefEditorForm_VisibleChanged(object sender, EventArgs e)
		{
			if (RefEditorForm.Visible == false)
			{
				if (RefEditorForm.PropertiesAffected &&
					EntityUpdated != null)
				{
					EntityUpdated(TheEntity);
				}
			}
		}

		private void ComponentMappingEditor_VisibleChanged(object sender, EventArgs e)
		{
			Populate();
		}

		private void shapeCanvas1_MouseClick(object sender, MouseEventArgs e)
		{
			if (RefEditorForm.Visible && !CurrentEndRectangle.Contains(e.Location))
			{
				Slyce.Common.Utility.SuspendPainting(this);
				RefEditorForm.Visible = false;
				Slyce.Common.Utility.ResumePainting(this);
			}
			//if (ComponentMappingEditor.Visible && !ComponentMappingEditor.Bounds.Contains(e.Location))
			//    ComponentMappingEditor.Visible = false;

			if (MappingEditor.Visible && !MappingEditor.Bounds.Contains(e.Location))
			{
				Slyce.Common.Utility.SuspendPainting(this);
				MappingEditor.Visible = false;
				Slyce.Common.Utility.ResumePainting(this);
			}
		}

		//void shapeCanvas1_LineEndWithFocusChanged(ShapeCanvas.LineEndWithFocus lineEndWithFocus)
		//{
		//    if (lineEndWithFocus == null || lineEndWithFocus.Line == null)
		//    {
		//        RefEditorForm.Visible = false;
		//        return;
		//    }
		//    return;
		//}

		//private void PopulateTableToolstrip()
		//{
		//    Slyce.Common.Controls.FloatingToolstrip.MenuItem item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Delete this mapping", DeleteImage, DeleteImageHover, TableUnmap_Click, TableToolstrip);
		//    TableToolstrip.Items.Add(item);

		//    TableToolstrip.Visible = false;
		//    TableToolstrip.LayoutStyle = Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal;
		//    TableToolstrip.BackColor = Color.Transparent;
		//    this.Controls.Add(TableToolstrip);
		//}

		//private void PopulateComponentToolstrip()
		//{
		//    Slyce.Common.Controls.FloatingToolstrip.MenuItem item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Edit this component", EditImage, EditImage, ComponentEdit_Click, ComponentToolstrip);
		//    ComponentToolstrip.Items.Add(item);

		//    item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Remove this component", DeleteImage, DeleteImageHover, ComponentRemove_Click, ComponentToolstrip);
		//    ComponentToolstrip.Items.Add(item);

		//    ComponentToolstrip.Visible = false;
		//    ComponentToolstrip.LayoutStyle = Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal;
		//    ComponentToolstrip.BackColor = Color.Transparent;
		//    ComponentToolstrip.Offset = 1;
		//    this.Controls.Add(ComponentToolstrip);
		//}

		private void ComponentEdit_Click(object sender, EventArgs e)
		{
			Slyce.Common.Controls.FloatingToolstrip.MenuItem item = (Slyce.Common.Controls.FloatingToolstrip.MenuItem)sender;
			ComponentToolstrip.Visible = false;
			ShowComponentEditor(item, ComponentToolstrip.Location);
		}

		private void ComponentRemove_Click(object sender, EventArgs e)
		{
			ComponentToolstrip.Visible = false;

			if (MessageBox.Show("Delete this component?", "Delete component", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Slyce.Common.Controls.FloatingToolstrip.MenuItem menuItem = (Slyce.Common.Controls.FloatingToolstrip.MenuItem)sender;
				ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Component)menuItem.Parent.Tag;
				TheEntity.RemoveComponent(component);
				Populate();
			}
		}

		private void ExpandCentreShape_Click(object sender, EventArgs e)
		{
			EntityToolstrip.Visible = false;
			Slyce.Common.Controls.FloatingToolstrip.MenuItem item = (Slyce.Common.Controls.FloatingToolstrip.MenuItem)sender;
			KeepMainShapeFull = !KeepMainShapeFull;
			item.TooltipText = KeepMainShapeFull ? "Collapse" : "Expand";
			item.Image = item.HoverImage = KeepMainShapeFull ? CollapseImage : ExpandImage;
			Populate();
		}

		//private void PopulateEntityToolstrip()
		//{
		//    Slyce.Common.Controls.FloatingToolstrip.MenuItem item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Delete", DeleteImage, DeleteImageHover, CenterEntityDelete_Click, EntityToolstrip);
		//    EntityToolstrip.Items.Add(item);

		//    //item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Stretch", ExpandImage, ExpandImage, ExpandCentreShape_Click, EntityToolstrip);
		//    //EntityToolstrip.Items.Add(item);

		//    //item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Add component", AddComponentImage, AddComponentImageHover, AddComponent_Click, EntityToolstrip);
		//    //EntityToolstrip.Items.Add(item);

		//    EntityToolstrip.Visible = false;
		//    EntityToolstrip.LayoutStyle = Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Vertical;
		//    EntityToolstrip.BackColor = Color.Transparent;
		//    EntityToolstrip.Offset = 1;
		//    this.Controls.Add(EntityToolstrip);
		//}

		//private void PopulateReferencedEntityToolstrip()
		//{
		//    Slyce.Common.Controls.FloatingToolstrip.MenuItem item = new Slyce.Common.Controls.FloatingToolstrip.MenuItem("Delete reference", DeleteImage, DeleteImageHover, ReferencedEntityDelete_Click, ReferencedEntityToolstrip);
		//    ReferencedEntityToolstrip.Items.Add(item);

		//    ReferencedEntityToolstrip.Visible = false;
		//    ReferencedEntityToolstrip.LayoutStyle = Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Vertical;
		//    ReferencedEntityToolstrip.BackColor = Color.Transparent;
		//    ReferencedEntityToolstrip.Offset = 1;
		//    this.Controls.Add(ReferencedEntityToolstrip);
		//}

		private void TableUnmap_Click(object sender, EventArgs e)
		{
			TableToolstrip.Visible = false;

			if (TableRemoved != null)
			{
				Slyce.Common.Controls.FloatingToolstrip.MenuItem menuItem = (Slyce.Common.Controls.FloatingToolstrip.MenuItem)sender;
				TableRemoved((Table)menuItem.Tag);
			}
		}

		private void AddComponent_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Add component clicked");
		}

		private void CenterEntityDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("Delete this entity [{0}]?", TheEntity.Name), "Delete entity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			EntityToolstrip.Visible = false;
			Entity nextEntity = null;

			foreach (Entity entity in TheEntity.EntitySet.Entities)
			{
				if (entity != TheEntity)
				{
					nextEntity = entity;
					break;
				}
			}
			TheEntity.DeleteSelf();

			if (EntityDeleted != null)
				EntityDeleted((EntityImpl)nextEntity);
		}

		private void ReferencedEntityDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Delete this reference?", "Delete reference", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			Slyce.Common.Controls.FloatingToolstrip.MenuItem menuItem = (Slyce.Common.Controls.FloatingToolstrip.MenuItem)sender;
			Reference reference = (Reference)menuItem.Tag;

			ReferencedEntityToolstrip.Visible = false;

			TheEntity.RemoveReference(reference);
			Populate();
		}

		public EntityImpl TheEntity
		{
			get { return _Entity; }
			set
			{
				_Entity = value;

				if (_Entity != null)
				{
					ScrollPosition = new Point(0, 0);
					Populate();
				}
			}
		}

		private void Populate()
		{
			if (TheEntity == null || BusyPopulating)
				return;

			BusyPopulating = true;

			try
			{
				Slyce.Common.Utility.SuspendPainting(this);
				HideAllFloatingToolstrips();
				Cursor = Cursors.WaitCursor;
				Font boldFont = new Font(Font, FontStyle.Bold);
				MainShape = new RawEntity(shapeCanvas1, TheEntity.Name, boldFont, TheEntity);
				MainShape.AutoSizeWidth = true;
				MainShape.Icon = EntityImage;
				MainShape.Enter += new EventHandler(centreShape_Enter);
				MainShape.Leave += new EventHandler(centreShape_Leave);
				MainShape.MouseClick += new MouseEventHandler(MainShape_MouseClick);
				//CategoryComponents.Parent = MainShape;
				//CategoryComponents.Properties.Clear();

				CategoryProperties.Parent = MainShape;
				CategoryProperties.IsExpanded = true;
				CategoryProperties.Properties.Clear();

				CategoryNavigation.Parent = MainShape;
				CategoryNavigation.Properties.Clear();

				MainShape.Categories.Add(CategoryProperties);

				if (TheEntity.InheritedProperties.Count() > 0)
				{
					CategoryInheritedProperties.Parent = MainShape;
					CategoryInheritedProperties.IsExpanded = true;
					CategoryInheritedProperties.Properties.Clear();
					MainShape.Categories.Add(CategoryInheritedProperties);
				}
				//MainShape.Categories.Add(CategoryComponents);
				MainShape.Categories.Add(CategoryNavigation);

				List<IColumn> columnsInComponents = new List<IColumn>();

				if (TheEntity.Components.Count > 0)
				{
					foreach (var component in TheEntity.Components)
					{
						RawProperty cat = new RawProperty(component.Name, component);
						cat.Enter += new EventHandler(Component_Enter);
						cat.Leave += new EventHandler(Component_Leave);
						cat.Click += new MouseEventHandler(Component_Click);
						CategoryProperties.Properties.Add(cat);

						foreach (var componentProperty in component.Properties)
						{
							columnsInComponents.Add(componentProperty.MappedColumn());

							RawProperty prop = new RawProperty(componentProperty.PropertyName, componentProperty);
							//prop.Click += new MouseEventHandler(Component_Click);
							//prop.DoubleClick += new MouseEventHandler(Component_DoubleClick);
							//prop.Enter += new EventHandler(Component_Enter);
							//prop.Leave += new EventHandler(Component_Leave);
							cat.SubProperties.Add(prop);
						}
					}
				}
				int propCount = TheEntity.Properties.Count();

				if (TheEntity.Properties.Count() > 0)
				{
					//foreach (var property in TheEntity.Properties.Except(TheEntity.ForeignKeyPropertiesToExclude))
					foreach (var property in TheEntity.Properties.Except(TheEntity.ForeignKeyPropertiesToExclude).Except(TheEntity.InheritedProperties))
					{
						if (!columnsInComponents.Contains(property.MappedColumn()))
						{
							RawProperty prop = new RawProperty(property.Name, property);
							prop.BackColorFocused = Color.FromArgb(245, 208, 18);
							prop.AllowSelection = true;
							prop.Click += new MouseEventHandler(Property_Click);
							CategoryProperties.Properties.Add(prop);
						}
					}
					foreach (var property in TheEntity.InheritedProperties)
					{
						if (!columnsInComponents.Contains(property.MappedColumn()))
						{
							RawProperty prop = new RawProperty(property.Name, property);
							prop.BackColorFocused = Color.FromArgb(245, 208, 18);
							prop.AllowSelection = true;
							prop.Click += new MouseEventHandler(Property_Click);
							CategoryInheritedProperties.Properties.Add(prop);
						}
					}
				}
				Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);
				Font subTextFont = new Font(Font.FontFamily, Font.Size - 0.6F, FontStyle.Regular);
				List<RawShape> bottomLevelShapes = new List<RawShape>();
				List<RawShape> rightAlignedShapes = new List<RawShape>();

				#region MappedTables

				if (ShowMappedTables)
				{
					bool hasMultiSchemas = false;

					if (TheEntity.EntitySet.MappingSet.Database != null)
						hasMultiSchemas = TheEntity.EntitySet.MappingSet.Database.GetSchemas().Count() > 1;

					foreach (ITable table in MappedTables.OrderBy(t => t.Name))
					{
						RawTable tableShape = new RawTable(shapeCanvas1, table.Name, boldFont, table);

						if (hasMultiSchemas)
						{
							tableShape.SubText = string.Format("  ({0}) ", table.Schema);
							tableShape.FontSubText = subTextFont;
						}
						CustomLineCap startCap = LineCaps.None;
						CustomLineCap endCap = LineCaps.None;
						tableShape.OriginatingLineStyle.LineStyle = DashStyle.Dot;
						//tableShape.OriginatingLineStyle.StartText = "";
						//tableShape.OriginatingLineStyle.EndText = "";
						tableShape.OriginatingLineStyle.MiddleText = GetDiscriminatorText(table);// "LastName = 'Anderson'";
						tableShape.OriginatingLineStyle.StartCap = startCap;
						tableShape.OriginatingLineStyle.EndCap = endCap;
						//tableShape.BackColor1 = Color.Blue;
						//tableShape.BackColor2 = Color.DarkBlue;
						tableShape.RoundedCorners = false;
						tableShape.Cursor = Cursors.Hand;
						tableShape.Tag = table;
						tableShape.Icon = table.IsView ? ViewImage : TableImage;

						if (string.IsNullOrEmpty(tableShape.OriginatingLineStyle.MiddleText))
							tableShape.OriginatingLineStyle.MiddleImage = FilterImageDisabled;
						else
							tableShape.OriginatingLineStyle.MiddleImage = FilterImage;

						tableShape.OriginatingLineStyle.MiddleImageFocused = EditImage;
						tableShape.OriginatingLineStyle.MouseClick += new MouseEventHandler(OriginatingLineStyle_MouseClick);
						tableShape.MouseClick += new MouseEventHandler(tableShape_MouseClick);
						tableShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(OriginatingLineStyle_MiddleImageClick);
						tableShape.OriginatingLineStyle.DataObject = table;
						tableShape.OriginatingLineStyle.ForeColor = Color.White;
						tableShape.MouseDoubleClick += new MouseEventHandler(tableShape_MouseDoubleClick);

						#region Setup discriminator
						//throw new NotImplementedException("TODO: ddiscriminator stuff");
						//if (TheEntity.Discriminator != null && TheEntity.Discriminator.RootGrouping != null)
						//{
						//    ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Condition firstCondition = TheEntity.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

						//    if (firstCondition != null)
						//    {
						//        IColumn column = firstCondition.Column;
						//        ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination.Operator op = firstCondition.Operator;
						//        string exprText = firstCondition.ExpressionValue.Value;
						//    }
						//}
						#endregion

						//tableShape.OriginatingLineStyle.MiddleImageMouseOver += new EventHandler(OriginatingLineStyle_MiddleImageMouseOver);
						rightAlignedShapes.Add(tableShape);
					}
					#region Add empty table
					Slyce.Common.Controls.Diagramming.Shapes.RawShape emptyTable = new Slyce.Common.Controls.Diagramming.Shapes.RawShape(shapeCanvas1, "Add table...", boldUnderlineFont)
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
					emptyTable.MouseClick += new MouseEventHandler(emptyTable_MouseClick);
					emptyTable.Cursor = Cursors.Hand;
					//emptyTable.OriginatingLineStyle.MouseClick += new MouseEventHandler(OriginatingLineStyle_MouseClick);
					rightAlignedShapes.Add(emptyTable);
					#endregion
				}
				#endregion

				#region References
				if (ShowReferencedEntities)
				{
					foreach (ReferenceImpl reference in TheEntity.References.OrderBy(r => r.Entity1 == TheEntity ? r.Entity2.Name : r.Entity1.Name))
					{
						RawEntity referencedEntityShape;
						CustomLineCap startCap;
						CustomLineCap endCap;

						if (reference.Entity1 == TheEntity)
						{
							referencedEntityShape = new RawEntity(shapeCanvas1, reference.Entity2.Name, boldFont, reference.Entity2);
							referencedEntityShape.OriginatingLineStyle.DataObject = reference;

							if (ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2))
								startCap = LineCaps.One;
							else if (ArchAngel.Interfaces.Cardinality.Many.Equals(reference.Cardinality2))
								startCap = LineCaps.Many;
							else if (ArchAngel.Interfaces.Cardinality.Zero.Equals(reference.Cardinality2))
								startCap = LineCaps.Zero;
							else
								throw new NotImplementedException("Unexpected cardinality.");

							if (ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1))
								endCap = LineCaps.One;
							else if (ArchAngel.Interfaces.Cardinality.Many.Equals(reference.Cardinality1))
								endCap = LineCaps.Many;
							else if (ArchAngel.Interfaces.Cardinality.Zero.Equals(reference.Cardinality1))
								endCap = LineCaps.Zero;
							else
								throw new NotImplementedException("Unexpected cardinality.");

							referencedEntityShape.OriginatingLineStyle.StartTextDataMember = "End1Name";
							referencedEntityShape.OriginatingLineStyle.EndTextDataMember = "End2Name";
							referencedEntityShape.OriginatingLineStyle.StartImageClick += new LinkLine.MouseEndDelegate(OriginatingLineStyle_StartImageClick);
							referencedEntityShape.OriginatingLineStyle.EndImageClick += new LinkLine.MouseEndDelegate(OriginatingLineStyle_EndImageClick);
							referencedEntityShape.MouseClick += new MouseEventHandler(referencedEntityShape_MouseClick);
							//referencedEntityShape.OriginatingLineStyle.MouseOverEnd1 += new LinkLine.MouseOverEndDelegate(OriginatingLineStyle_MouseOverEnd2);
							//referencedEntityShape.OriginatingLineStyle.MouseOverEnd2 += new LinkLine.MouseOverEndDelegate(OriginatingLineStyle_MouseOverEnd1);
							//reference..GetMappingSet().ReferenceMappings[0].


							//RawCategory categoryNav = new RawCategory("Navigation", CategoryFont, referencedEntityShape);
							//RawProperty navProp = new RawProperty(referencedEntityShape.OriginatingLineStyle.EndText, reference);
							//navProp.Click += new MouseEventHandler(NavigationProperty_Click);
							//categoryNav.Properties.Add(navProp);
							//referencedEntityShape.Categories.Add(categoryNav);

							RawProperty prop = new RawProperty(referencedEntityShape.OriginatingLineStyle.StartText, reference);
							prop.DoubleClick += new MouseEventHandler(prop1_DoubleClick);
							CategoryNavigation.Properties.Add(prop);
						}
						else if (reference.Entity2 == TheEntity)
						{
							referencedEntityShape = new RawEntity(shapeCanvas1, reference.Entity1.Name, boldFont, reference.Entity1);
							referencedEntityShape.OriginatingLineStyle.DataObject = reference;

							if (ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1))
								startCap = LineCaps.One;
							else if (ArchAngel.Interfaces.Cardinality.Many.Equals(reference.Cardinality1))
								startCap = LineCaps.Many;
							else if (ArchAngel.Interfaces.Cardinality.Zero.Equals(reference.Cardinality1))
								startCap = LineCaps.Zero;
							else
								throw new NotImplementedException("Unexpected cardinality.");

							if (ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2))
								endCap = LineCaps.One;
							else if (ArchAngel.Interfaces.Cardinality.Many.Equals(reference.Cardinality2))
								endCap = LineCaps.Many;
							else if (ArchAngel.Interfaces.Cardinality.Zero.Equals(reference.Cardinality2))
								endCap = LineCaps.Zero;
							else
								throw new NotImplementedException("Unexpected cardinality.");

							referencedEntityShape.OriginatingLineStyle.StartTextDataMember = "End2Name";
							referencedEntityShape.OriginatingLineStyle.EndTextDataMember = "End1Name";
							referencedEntityShape.OriginatingLineStyle.StartImageClick += new LinkLine.MouseEndDelegate(OriginatingLineStyle_EndImageClick);
							referencedEntityShape.OriginatingLineStyle.EndImageClick += new LinkLine.MouseEndDelegate(OriginatingLineStyle_StartImageClick);
							referencedEntityShape.MouseClick += new MouseEventHandler(referencedEntityShape_MouseClick);
							//referencedEntityShape.OriginatingLineStyle.MouseOverEnd1 += new LinkLine.MouseOverEndDelegate(OriginatingLineStyle_MouseOverEnd1);
							//referencedEntityShape.OriginatingLineStyle.MouseOverEnd2 += new LinkLine.MouseOverEndDelegate(OriginatingLineStyle_MouseOverEnd2);

							//RawCategory categoryNav = new RawCategory("Navigation", CategoryFont, referencedEntityShape);
							//RawProperty navProp = new RawProperty(referencedEntityShape.OriginatingLineStyle.EndText, reference);
							//navProp.Click += new MouseEventHandler(NavigationProperty_Click);
							//categoryNav.Properties.Add(navProp);
							//referencedEntityShape.Categories.Add(categoryNav);

							RawProperty prop = new RawProperty(referencedEntityShape.OriginatingLineStyle.StartText, reference);
							prop.DoubleClick += new MouseEventHandler(prop2_DoubleClick);
							CategoryNavigation.Properties.Add(prop);
						}
						else
							throw new Exception("What the...??!");

						referencedEntityShape.Cursor = Cursors.Hand;
						referencedEntityShape.Icon = EntityImage;
						referencedEntityShape.OriginatingLineStyle.LineStyle = DashStyle.Solid;
						referencedEntityShape.OriginatingLineStyle.StartCap = startCap;
						referencedEntityShape.OriginatingLineStyle.EndCap = endCap;

						referencedEntityShape.OriginatingLineStyle.ForeColor = Color.White;
						referencedEntityShape.OriginatingLineStyle.DefaultEndText = "not set";
						referencedEntityShape.OriginatingLineStyle.StartImage = EditImage;
						referencedEntityShape.OriginatingLineStyle.EndImage = EditImage;
						referencedEntityShape.MouseDoubleClick += new MouseEventHandler(referencedEntityShape_MouseDoubleClick);
						referencedEntityShape.Enter += new EventHandler(referencedEntityShape_Enter);
						referencedEntityShape.Leave += new EventHandler(referencedEntityShape_Leave);
						//referencedEntityShape.AutoSizeWidth = true;
						bottomLevelShapes.Add(referencedEntityShape);
					}
					// Add empty reference
					RawShape emptyReference = new RawShape(shapeCanvas1, "Add entity...", boldUnderlineFont)
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
					emptyReference.Cursor = Cursors.Hand;
					bottomLevelShapes.Add(emptyReference);
				}
				#endregion

				//shapeCanvas1.BackColor = this.BackColor;
				////shapeCanvas1.DrawStar(centreShape, outerShapes);
				////shapeCanvas1.DrawThreeLayerHorizontal(centreShape, null, outerShapes, false);
				//shapeCanvas1.Height = this.Height;
				//shapeCanvas1.DrawThreeLayerVertical(centreShape, null, outerShapes);
				//Cursor = Cursors.Default;

				shapeCanvas1.SwimLane1 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(79, 124, 205), Color.Black, Color.White, 180F, "Referenced Entities", ShapeCanvas.SwimLaneStyle.Styles.Fill);
				shapeCanvas1.SwimLane3 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(79, 124, 205), Color.Black, Color.White, 0F, "Mapped Tables", ShapeCanvas.SwimLaneStyle.Styles.Fill);

				shapeCanvas1.BackColor = this.BackColor;
				//shapeCanvas1.DrawStar(centreShape, outerShapes);
				//shapeCanvas1.DrawThreeLayerHorizontal(centreShape, null, outerShapes, false);
				shapeCanvas1.Height = this.Height;
				//shapeCanvas1.DrawThreeLayerVertical(centreShape, null, bottomLevelShapes, rightAlignedShapes);
				shapeCanvas1.KeepMainShapeCentered = true;
				MainShape.RecalcSizeRequired = true;
				shapeCanvas1.DrawThreeLayerHorizontal(MainShape, bottomLevelShapes, rightAlignedShapes, KeepMainShapeFull);
				this.Focus();
				shapeCanvas1.Focus();
			}
			finally
			{
				this.AutoScrollPosition = ScrollPosition;
				Slyce.Common.Utility.ResumePainting(this);
				Cursor = Cursors.Default;
				BusyPopulating = false;
			}
		}

		private List<Property> SelectedProperties = new List<Property>();
		private RawCategory SelectedCategory = null;

		private void CategoryProperties_MultiPropertiesClicked(RawCategory category, List<RawProperty> selectedProperties)
		{
			SelectedProperties.Clear();

			if (selectedProperties.Count <= 1)
				return;

			foreach (var rawProp in selectedProperties)
				SelectedProperties.Add((Property)rawProp.Tag);

			if (selectedProperties.Count <= 1)
				return;

			foreach (ToolStripItem item in contextMenuStrip1.Items)
				item.Visible = false;

			mnuAddComponent.Visible = true;
			mnuRefactorToExistingComponent.Visible = true;
			contextMenuStrip1.Show(Cursor.Position);
			SelectedCategory = category;
		}

		private void MainShape_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				foreach (ToolStripItem item in contextMenuStrip1.Items)
					item.Visible = false;

				mnuDeleteEntity.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
		}

		private void referencedEntityShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedReference = (Reference)((RawShape)sender).OriginatingLineStyle.DataObject;

			if (e.Button == MouseButtons.Right)
			{
				foreach (ToolStripItem item in contextMenuStrip1.Items)
					item.Visible = false;

				if (ArchAngel.Interfaces.Cardinality.IsOneToOne(SelectedReference.Cardinality1, SelectedReference.Cardinality2) &&
					!SelectedReference.SelfReference)
				{
					mnuConvertToChild.Visible = true;
					mnuConvertToChild.Text = "Convert to child (inheritance)";
				}
				if (!SelectedReference.SelfReference &&
					new DirectedReference(TheEntity, SelectedReference).FromEndCardinality == ArchAngel.Interfaces.Cardinality.One)
				{
					mnuMergeEntity.Visible = true;
					mnuMergeEntity.Text = "Merge into " + TheEntity.Name;
				}
				mnuRemoveReference.Visible = true;
				//mnuEditNearSide.Visible = true;
				//mnuEditFarSide.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (EntitySelected != null)
					EntitySelected((EntityImpl)((RawShape)sender).Tag);
			}
		}

		private void Component_Enter(object sender, EventArgs e)
		{
			//RawProperty prop = (RawProperty)sender;
			//SelectedComponent = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Component)prop.Tag;
			//ComponentToolstrip.Tag = prop.Tag;
			//ComponentToolstrip.Left = prop.Bounds.Right;
			//ComponentToolstrip.Top = prop.Bounds.Top;
			//ComponentToolstrip.BringToFront();
			//ComponentToolstrip.Show();
		}

		private void Component_Leave(object sender, EventArgs e)
		{
			ComponentToolstrip.Visible = false;
		}

		private void referencedEntityShape_Leave(object sender, EventArgs e)
		{
			ReferencedEntityToolstrip.Visible = false;
		}

		private void referencedEntityShape_Enter(object sender, EventArgs e)
		{
			//if (ComponentMappingEditor.Visible)
			//    return;

			if (ReferencedEntityToolstrip.LayoutStyle == Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal)
			{
				ReferencedEntityToolstrip.Left = ((RawShape)sender).Left;
				ReferencedEntityToolstrip.Top = ((RawShape)sender).Top - ReferencedEntityToolstrip.Height;
			}
			else
			{
				ReferencedEntityToolstrip.Left = ((RawShape)sender).Right + 1;
				ReferencedEntityToolstrip.Top = ((RawShape)sender).Top;
			}
			RawShape shape = (RawShape)sender;

			ReferencedEntityToolstrip.Tag = shape.OriginatingLineStyle.DataObject;
			ReferencedEntityToolstrip.BringToFront();
			ReferencedEntityToolstrip.Show();
			ReferencedEntityToolstrip.Visible = true;
			//ReferencedEntityToolstrip.Refresh();
			this.Refresh();
		}

		private void prop1_DoubleClick(object sender, MouseEventArgs e)
		{
			RawProperty prop = (RawProperty)sender;
			ReferenceImpl reference = (ReferenceImpl)prop.Tag;
			ProcessMouseOverEndOfLine(reference, e, prop.Bounds, true);
		}

		private void prop2_DoubleClick(object sender, MouseEventArgs e)
		{
			RawProperty prop = (RawProperty)sender;
			ReferenceImpl reference = (ReferenceImpl)prop.Tag;
			ProcessMouseOverEndOfLine(reference, e, prop.Bounds, false);
		}

		private void Component_DoubleClick(object sender, MouseEventArgs e)
		{
			//ShowComponentEditor((RawProperty)sender, e.Location);
		}

		private void Property_Click(object sender, MouseEventArgs e)
		{
			//SelectedComponent = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Component)((RawProperty)sender).Tag;

			//if (e.Button == MouseButtons.Right)
			//{
			//    foreach (ToolStripItem item in contextMenuStrip1.Items)
			//    {
			//        item.Visible = false;
			//    }
			//    mnuRemoveComponent.Visible = true;
			//    contextMenuStrip1.Show(Cursor.Position);
			//}
		}

		private void NavigationProperty_Click(object sender, MouseEventArgs e)
		{
			MessageBox.Show("Navigation property clicked");
		}

		private void Component_Click(object sender, MouseEventArgs e)
		{
			SelectedComponent = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Component)((RawProperty)sender).Tag;

			if (e.Button == MouseButtons.Right)
			{
				foreach (ToolStripItem item in contextMenuStrip1.Items)
					item.Visible = false;

				mnuRemoveComponent.Visible = true;
				mnuEditComponent.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
		}

		private void ShowComponentEditor(Slyce.Common.Controls.FloatingToolstrip.MenuItem menuItem, Point location)
		{
			ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Component)menuItem.Parent.Tag;

			UserControls.FormComponentMappingEditor ComponentMappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormComponentMappingEditor();
			ComponentMappingEditor.Fill(component.Specification, TheEntity);
			Point pt = location;
			pt.Offset(-1 * ComponentMappingEditor.Width / 2, -1 * ComponentMappingEditor.Height / 2);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);

			EntityToolstrip.Visible = false;
			this.Refresh();
			ComponentMappingEditor.Location = pt;
			//ComponentMappingEditor.BringToFront();
			//ComponentMappingEditor.Visible = true;
			//ComponentMappingEditor.Refresh();
			ComponentMappingEditor.ShowDialog(this);
			Populate();
		}

		private void referencedEntityShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (EntitySelected != null)
				EntitySelected((EntityImpl)((RawShape)sender).Tag);
		}

		private void tableShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (TableSelected != null)
				TableSelected((ITable)((RawShape)sender).Tag);
		}

		private void OriginatingLineStyle_StartImageClick(object sender, MouseEventArgs e, Rectangle endBounds)
		{
			LinkLine line = (LinkLine)sender;
			ReferenceImpl reference = (ReferenceImpl)line.DataObject;
			ProcessMouseOverEndOfLine(reference, e, endBounds, true);
		}

		private void OriginatingLineStyle_EndImageClick(object sender, MouseEventArgs e, Rectangle endBounds)
		{
			LinkLine line = (LinkLine)sender;
			ReferenceImpl reference = (ReferenceImpl)line.DataObject;
			ProcessMouseOverEndOfLine(reference, e, endBounds, false);
		}

		private void centreShape_Leave(object sender, EventArgs e)
		{
			EntityToolstrip.Visible = false;
			ComponentToolstrip.Visible = false;
		}

		private void centreShape_Enter(object sender, EventArgs e)
		{
			//if (ComponentMappingEditor.Visible)
			//    return;

			if (EntityToolstrip.LayoutStyle == Slyce.Common.Controls.FloatingToolstrip.LayoutStyles.Horizontal)
			{
				EntityToolstrip.Left = ((RawShape)sender).Left;
				EntityToolstrip.Top = ((RawShape)sender).Top - EntityToolstrip.Height;
			}
			else
			{
				EntityToolstrip.Left = ((RawShape)sender).Right + 1;
				EntityToolstrip.Top = ((RawShape)sender).Top;
			}
			EntityToolstrip.Tag = TheEntity;
			EntityToolstrip.BringToFront();
			EntityToolstrip.Show();
			EntityToolstrip.Visible = true;
			this.Refresh();
		}

		private void shapeCanvas1_MouseMove(object sender, MouseEventArgs e)
		{
			//if (RefEditorForm.Visible && !RefEditorForm.Bounds.Contains(e.Location))
			//    RefEditorForm.Visible = false;
		}

		private void emptyReference_MouseClick(object sender, MouseEventArgs e)
		{
			CreateNewReference(TheEntity);
			Populate();
		}

		public static void CreateNewReference(Entity theEntity)
		{
			List<Entity> unavailableEntities = new List<Entity>();

			foreach (ReferenceImpl reference in theEntity.References)
			{
				if (reference.Entity1 == theEntity)
					unavailableEntities.Add(reference.Entity2);
				else if (reference.Entity2 == theEntity)
					unavailableEntities.Add(reference.Entity1);
			}
			UserControls.FormSelectExistingEntity form = new UserControls.FormSelectExistingEntity(theEntity, unavailableEntities, null, "Select entity to reference", false, UserControls.FormSelectExistingEntity.RequestorTypes.Entity_Select_Existing);
			form.ShowDialog();

			if (form.SelectedEntity != null)
			{
				Entity selectedEntity = form.SelectedEntity;
				ReferenceImpl newReference = new ReferenceImpl(theEntity, selectedEntity);
				newReference.Name = "New Reference";
				newReference.EntitySet = selectedEntity.EntitySet;
				newReference.Cardinality1 = form.CardinalityPrimary;
				newReference.Cardinality2 = form.CardinalityForeign;
				newReference.End1Name = newReference.Cardinality1 == ArchAngel.Interfaces.Cardinality.Many ? newReference.Entity2.Name.Pluralize() : newReference.Entity2.Name;
				newReference.End2Name = newReference.Cardinality2 == ArchAngel.Interfaces.Cardinality.Many ? newReference.Entity1.Name.Pluralize() : newReference.Entity1.Name;
				newReference.End1Enabled = true;
				newReference.End2Enabled = true;
				theEntity.AddReference(newReference);
				selectedEntity.AddReference(newReference);

				if (form.SelectedRelationship != null)
					newReference.SetMappedRelationship(form.SelectedRelationship);
				else if (form.AssociationTable != null)
					newReference.SetMappedTable(form.AssociationTable);
				else if (selectedEntity.MappedTables().Count() == 1)
					newReference.SetMappedTable(selectedEntity.MappedTables().ElementAt(0));

				theEntity.EntitySet.AddReference(newReference);
			}
		}

		private void OriginatingLineStyle_MiddleImageClick(object sender, MouseEventArgs e)
		{
			Table table = (Table)((LinkLine)sender).DataObject;

			Point pt = ((LinkLine)sender).MidPoint;
			pt.Offset(-1 * MappingEditor.Width / 2, -1 * MappingEditor.Height / 2);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);

			MappingEditor.Entity = TheEntity;
			MappingEditor.Table = table;
			MappingEditor.Location = pt;
			MappingEditor.BringToFront();
			MappingEditor.Visible = true;
			MappingEditor.Refresh();

			//if (e.Button == MouseButtons.Left)
			//{
			//    SelectedLinkLine = (LinkLine)sender;

			//    UserControls.FormDiscriminator form = new UserControls.FormDiscriminator(TheEntity);
			//    form.StartPosition = FormStartPosition.CenterParent;//.Manual;
			//    form.ShowDialog(this);

			//    SelectedLinkLine.MiddleText = GetDiscriminatorText();

			//    if (string.IsNullOrEmpty(SelectedLinkLine.MiddleText))
			//        SelectedLinkLine.MiddleImage = FilterImageDisabled;
			//    else
			//        SelectedLinkLine.MiddleImage = FilterImage;
			//}
			//else
			//{
			//    ShowContextMenuForDiscriminator();
			//}
		}

		private void tableShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedTable = (Table)((RawShape)sender).Tag;

			if (e.Button == MouseButtons.Right)
			{
				foreach (ToolStripItem item in contextMenuStrip1.Items)
				{
					item.Visible = false;
				}
				mnuRemoveTable.Visible = true;
				contextMenuStrip1.Show(Cursor.Position);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (TableSelected != null)
					TableSelected((ITable)((RawShape)sender).Tag);
			}
		}

		private void emptyTable_MouseClick(object sender, MouseEventArgs e)
		{
			BusyPopulating = true;

			try
			{
				List<ITable> unavailableTables = new List<ITable>();
				unavailableTables.AddRange(TheEntity.MappedTables());

				UserControls.FormSelectTable form = new UserControls.FormSelectTable(TheEntity, unavailableTables, null, "Select table to map");
				form.ShowDialog();

				if (form.SelectedTable != null && TableAdded != null)
					TableAdded(form.SelectedTable);
			}
			finally
			{
				BusyPopulating = false;
				Populate();
			}
		}

		private void OriginatingLineStyle_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedLinkLine = (LinkLine)sender;

			//if (e.Button == MouseButtons.Right)
			//    ShowContextMenuForDiscriminator();
		}

		private void ShowContextMenuForDiscriminator()
		{
			foreach (ToolStripItem item in contextMenuStrip1.Items)
			{
				item.Visible = false;
			}
			mnuEditDiscriminator.Visible = true;
			mnuRemoveDiscriminator.Visible = true;
			contextMenuStrip1.Show(Cursor.Position);
		}

		private void mnuEditDiscriminator_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException("TODO: ddiscriminator stuff");
			//UserControls.FormDiscriminator form = new UserControls.FormDiscriminator(TheEntity);
			//form.StartPosition = FormStartPosition.CenterParent;//.Manual;
			//form.ShowDialog(this);

			//SelectedLinkLine.MiddleText = GetDiscriminatorText();

			//if (string.IsNullOrEmpty(SelectedLinkLine.MiddleText))
			//    SelectedLinkLine.MiddleImage = FilterImageDisabled;
			//else
			//    SelectedLinkLine.MiddleImage = FilterImage;
		}

		private string GetDiscriminatorText(ITable table)
		{
			if (TheEntity.Parent != null &&
				TheEntity.Parent.Discriminator != null &&
				EntityImpl.DetermineInheritanceTypeWithParent(TheEntity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
			{
				if (TheEntity.Parent.Discriminator.DiscriminatorType == Enums.DiscriminatorTypes.Column)
					return string.Format("{0} = {1}", TheEntity.Parent.Discriminator.ColumnName, TheEntity.DiscriminatorValue);
				else
					return string.Format("Value: {0} Formula: {1}", TheEntity.DiscriminatorValue, TheEntity.Parent.Discriminator.Formula);
			}
			return "";
		}

		private string GetDiscriminatorText()
		{
			throw new NotImplementedException("TODO: ddiscriminator stuff");
			//if (TheEntity.Discriminator != null && TheEntity.Discriminator.RootGrouping != null)
			//{
			//    Condition firstCondition = TheEntity.Discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);

			//    if (firstCondition != null)
			//    {
			//        return string.Format("{0} {1} '{2}'", firstCondition.Column.Name, firstCondition.Operator.DisplayText, firstCondition.ExpressionValue.Value);
			//    }
			//}
			return "";
		}

		private void mnuRemoveDiscriminator_Click(object sender, EventArgs e)
		{
			//if (TheEntity.Discriminator == null)
			//    TheEntity.Discriminator = new DiscriminatorImpl();

			//TheEntity.Discriminator.RootGrouping = new AndGrouping();
			//SelectedLinkLine.MiddleText = "";
			//SelectedLinkLine.MiddleImage = FilterImageDisabled;
		}

		private void mnuRemoveTable_Click(object sender, EventArgs e)
		{
			if (TableRemoved != null)
				TableRemoved(SelectedTable);
		}

		private void MappingEditor_SizeChanged(object sender, EventArgs e)
		{
			//Populate();
		}

		private void MappingEditor_Resize(object sender, EventArgs e)
		{
			//Populate();
			shapeCanvas1.Width = this.ClientSize.Width;
		}

		//void OriginatingLineStyle_MiddleImageMouseOver(object sender, EventArgs e)
		//{
		//    LinkLine line = (LinkLine)sender;
		//    //MessageBox.Show(string.Format("Show discriminator editor at: {0},{1}", line.MidPoint.X, line.MidPoint.Y));

		//    UserControls.DiscriminatorEditor editor = new WindowsFormsApplication1.Editors.UserControls.DiscriminatorEditor();
		//    editor.Location = line.MidPoint;
		//    this.Controls.Add(editor);
		//    editor.BringToFront();
		//    editor.Visible = true;
		//}

		private void OriginatingLineStyle_MouseOverEnd1(object sender, MouseEventArgs e, Rectangle endRectangle)
		{
			LinkLine line = (LinkLine)sender;
			ReferenceImpl reference = (ReferenceImpl)line.DataObject;
			ProcessMouseOverEndOfLine(reference, e, endRectangle, true);
		}

		private void OriginatingLineStyle_MouseOverEnd2(object sender, MouseEventArgs e, Rectangle endRectangle)
		{
			LinkLine line = (LinkLine)sender;
			ReferenceImpl reference = (ReferenceImpl)line.DataObject;
			ProcessMouseOverEndOfLine(reference, e, endRectangle, false);
		}

		private void ProcessMouseOverEndOfLine(ReferenceImpl reference, MouseEventArgs e, Rectangle endRectangle, bool isEnd1)
		{
			RefEditorForm.Visible = false;
			int offset = 0;
			Point pt = new Point(endRectangle.X, endRectangle.Y + endRectangle.Height);// PointToClient(Cursor.Position);// e.Location;
			pt.Offset(offset, offset);

			if (endRectangle != CurrentEndRectangle || !RefEditorForm.Visible)
			{
				CurrentEndRectangle = endRectangle;
				RefEditorForm.Fill(reference, isEnd1);
				//RefEditorForm.Fill((ReferenceImpl)lineEndWithFocus.Line.DataObject, lineEndWithFocus.EndType == ShapeCanvas.LineEndWithFocus.EndTypes.Start);

				if (pt.X + RefEditorForm.Width < this.Width)
					RefEditorForm.Location = pt;
				else
				{
					pt.Offset(-1 * RefEditorForm.Width + endRectangle.Width + offset * 2, 0);
					RefEditorForm.Location = pt;
				}
				if (pt.Y + RefEditorForm.Height - ScrollPosition.Y < this.Height)
				{
					pt.Offset(0, -1 * ScrollPosition.Y);
					RefEditorForm.Location = pt;
				}
				else
				{
					//pt.Offset(0, -1 * RefEditorForm.Height - endRectangle.Height + offset * 2);
					pt.Y = this.Height - RefEditorForm.Height - 2;
					RefEditorForm.Location = pt;
				}
				RefEditorForm.Visible = true;
				RefEditorForm.Refresh();
			}
		}

		private UserControls.FormReferenceEditor2 RefEditorForm
		{
			get
			{
				if (_RefEditorForm == null)
				{
					_RefEditorForm = new UserControls.FormReferenceEditor2();
					_RefEditorForm.Visible = false;
					Controls.Add(_RefEditorForm);
					_RefEditorForm.BringToFront();
					_RefEditorForm.CardinalityChanged += new EventHandler(_RefEditorForm_CardinalityChanged);
				}
				return _RefEditorForm;
			}
		}

		void _RefEditorForm_CardinalityChanged(object sender, EventArgs e)
		{
			Populate();
		}

		private void mnuDeleteEntity_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("Delete this entity [{0}]?", TheEntity.Name), "Delete entity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			EntityToolstrip.Visible = false;
			Entity nextEntity = null;

			foreach (Entity entity in TheEntity.EntitySet.Entities)
			{
				if (entity != TheEntity)
				{
					nextEntity = entity;
					break;
				}
			}
			TheEntity.DeleteSelf();

			if (EntityDeleted != null)
				EntityDeleted((EntityImpl)nextEntity);
		}

		private void mnuRemoveReference_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Delete this reference?", "Delete reference", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			Entity entity1 = SelectedReference.Entity1;
			Entity entity2 = SelectedReference.Entity2;

			entity1.RemoveReference(SelectedReference);
			entity2.RemoveReference(SelectedReference);
			TheEntity.EntitySet.DeleteReference(SelectedReference);
			Populate();
		}

		private void mnuRemoveComponent_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Delete this component?", "Delete component", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				TheEntity.RemoveComponent(SelectedComponent);
				Populate();
			}
		}

		private void mnuAddComponent_Click(object sender, EventArgs e)
		{
			UserControls.FormRefactorToComponent form = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormRefactorToComponent(TheEntity, SelectedProperties);
			form.ShowDialog(this);
			SelectedCategory.SelectedProperties.Clear();
			Populate();
		}

		private void mnuEditComponent_Click(object sender, EventArgs e)
		{
			UserControls.FormComponentMappingEditor MappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormComponentMappingEditor();
			MappingEditor.Fill(SelectedComponent.Specification, SelectedComponent.ParentEntity);
			Point pt = new Point(MappingEditor.Width / 2, MappingEditor.Height / 2);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);

			MappingEditor.ShowDialog(this);
			Populate();
		}

		private void mnuRefactorToExistingComponent_Click(object sender, EventArgs e)
		{
			UserControls.FormComponentMappingEditor MappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormComponentMappingEditor();
			MappingEditor.Fill(null, TheEntity);
			Point pt = new Point(MappingEditor.Width / 2, MappingEditor.Height / 2);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);

			MappingEditor.ShowDialog(this);
			Populate();
		}

		private void mnuConvertToChild_Click(object sender, EventArgs e)
		{
			if (SelectedReference.Entity1 == SelectedReference.Entity2)
				throw new Exception("Cannot make an entity a child of itself.");

			Entity childEntity = SelectedReference.Entity1 == TheEntity ? SelectedReference.Entity2 : SelectedReference.Entity1;

			TheEntity.AddChild(childEntity);
			childEntity.Parent = TheEntity;

			foreach (var directedReference in TheEntity.DirectedReferences)
			{
				if (directedReference.ToEntity == childEntity)
					TheEntity.RemoveReference(directedReference.Reference);
			}
			foreach (var directedReference in childEntity.DirectedReferences)
			{
				if (directedReference.ToEntity == TheEntity)
					childEntity.RemoveReference(directedReference.Reference);
			}
			// Remove the ID property from the child entity because it should share the parent entity's ID instead
			for (int i = childEntity.Key.Properties.ToList().Count - 1; i >= 0; i--)
				childEntity.RemoveProperty(childEntity.Key.Properties.ElementAt(i));

			Populate();
		}

		private void mnuMergeEntity_Click(object sender, EventArgs e)
		{
			BusyPopulating = true;

			try
			{
				DirectedReference dr = new DirectedReference(TheEntity, SelectedReference);
				Entity entityToMerge = dr.ToEntity;
				List<ITable> mappedTables = entityToMerge.MappedTables().ToList();

				foreach (var table in mappedTables)
					foreach (var column in table.Columns)
					{
						Property newProperty = Controller.MappingLayer.OneToOneEntityProcessor.CreatePropertyFromColumn(column);
						newProperty.Name = newProperty.Name.GetNextName(TheEntity.Properties.Select(p => p.Name));
						TheEntity.AddProperty(newProperty);
						newProperty.SetMappedColumn(column);
					}
				entityToMerge.DeleteSelf();
			}
			finally
			{
				BusyPopulating = false;
				Populate();
			}
		}

		private void EntityMappingDiagram_Scroll(object sender, ScrollEventArgs e)
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
