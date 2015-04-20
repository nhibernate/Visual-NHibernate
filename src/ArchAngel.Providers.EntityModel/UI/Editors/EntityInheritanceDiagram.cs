using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.Controls.Diagramming.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class EntityInheritanceDiagram : UserControl
	{
		public event EventHandler InheritanceUpdated;
		public event EntityMappingDiagram.EntityActionDelegate EntitySelected;
		public event EntityMappingDiagram.EntityActionDelegate EntityDeleted;

		private static Image InfoImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.about_16.png"));
		private EntityImpl _Entity;
		private LinkLine SelectedLinkLine = null;
		private RawShape SelectedParent = null;
		private RawShape SelectedChild = null;
		private RawShape CentreShape = null;
		private bool HasOneToOne = false;
		private Point ScrollPosition = new Point(0, 0);
		private bool BusyPopulating = false;

		public EntityInheritanceDiagram()
		{
			InitializeComponent();

			this.BackColor = Color.Black;
		}

		public EntityImpl Entity
		{
			get { return _Entity; }
			set
			{
				if (_Entity != value)
				{
					_Entity = value;
					shapeCanvasInheritance.Left = 0;
					shapeCanvasInheritance.Top = 0;
					shapeCanvasInheritance.Width = this.ClientSize.Width;
					shapeCanvasInheritance.Height = this.ClientSize.Height;
					ScrollPosition = new Point(0, 0);

					if (_Entity != null)
						Populate();
				}
			}
		}

		private string GetInheritanceDisplayText(Entity entity)
		{
			var inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(entity);
			string inheritanceName = EntityImpl.GetDisplayText(EntityImpl.DetermineInheritanceTypeWithParent(entity));

			if (inheritanceType == EntityImpl.InheritanceType.TablePerClassHierarchy)
				return string.Format("{0}\nDiscriminatorValue = {1}", inheritanceName, entity.DiscriminatorValue);

			return inheritanceName;
		}

		private void Populate()
		{
			if (Entity == null || BusyPopulating)
				return;

			BusyPopulating = true;

			try
			{
				Slyce.Common.Utility.SuspendPainting(this);
				Cursor = Cursors.WaitCursor;

				#region Check for Table Per Sub-Class
				HasOneToOne = false;

				foreach (var directedRef in Entity.DirectedReferences)
					if (ArchAngel.Interfaces.Cardinality.IsOneToOne(directedRef.FromEndCardinality, directedRef.ToEndCardinality))
					{
						HasOneToOne = true;
						break;
					}
				#endregion

				//shapeCanvasInheritance.main
				Font boldFont = new Font(Font, FontStyle.Bold);
				Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);
				CentreShape = new RawEntity(shapeCanvasInheritance, Entity.Name, boldFont, Entity);
				//centreShape.Cursor = Cursors.Hand;
				//centreShape.Click += new EventHandler(centreShape_OnClick);
				CentreShape.MouseClick += new MouseEventHandler(centreShape_MouseClick);

				List<RawShape> topLevelShapes = new List<RawShape>();
				List<RawShape> bottomLevelShapes = new List<RawShape>();

				if (Entity.Parent == null)
				{
					//if (HasOneToOne)
					//{
					RawShape emptyParent = new RawShape(shapeCanvasInheritance, "Add parent...", boldUnderlineFont)
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
						OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow),
						Tag = null
					};
					emptyParent.OriginatingLineStyle.ForeColor = Color.White;
					emptyParent.MouseClick += new MouseEventHandler(emptyParent_MouseClick);
					topLevelShapes.Add(emptyParent);
					//}
				}
				else
				{
					RawEntity parentShape = new RawEntity(shapeCanvasInheritance, Entity.Parent.Name, boldFont, Entity.Parent);
					parentShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
					parentShape.OriginatingLineStyle.MiddleImage = InfoImage;
					//parentShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(ParentOriginatingLineStyle_MiddleImageClick);
					parentShape.OriginatingLineStyle.ForeColor = Color.White;
					parentShape.OriginatingLineStyle.Parent = parentShape;
					parentShape.OriginatingLineStyle.MiddleText = GetInheritanceDisplayText(Entity);
					parentShape.Tag = Entity.Parent;
					parentShape.MouseClick += new MouseEventHandler(parentShape_MouseClick);
					parentShape.MouseDoubleClick += new MouseEventHandler(parentShape_MouseDoubleClick);
					parentShape.Cursor = Cursors.Hand;
					topLevelShapes.Add(parentShape);
				}

				#region Add empty child
				Slyce.Common.Controls.Diagramming.Shapes.RawShape emptyChild = new Slyce.Common.Controls.Diagramming.Shapes.RawShape(shapeCanvasInheritance, "Add child...", boldUnderlineFont)
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
					OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow)
				};
				emptyChild.OriginatingLineStyle.ForeColor = Color.White;
				emptyChild.MouseClick += new MouseEventHandler(emptyChild_MouseClick);
				emptyChild.OriginatingLineStyle.MouseClick += new MouseEventHandler(OriginatingLineStyle_MouseClick);
				bottomLevelShapes.Add(emptyChild);
				#endregion

				foreach (Entity child in Entity.Children)
				{
					RawEntity childShape = new RawEntity(shapeCanvasInheritance, child.Name, boldFont, child);
					childShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
					childShape.OriginatingLineStyle.MiddleImage = InfoImage;
					//childShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(ChildOriginatingLineStyle_MiddleImageClick);
					childShape.OriginatingLineStyle.ForeColor = Color.White;
					childShape.OriginatingLineStyle.Parent = childShape;
					childShape.OriginatingLineStyle.MiddleText = GetInheritanceDisplayText(child);
					childShape.Tag = child;
					childShape.MouseClick += new MouseEventHandler(childShape_MouseClick);
					childShape.MouseDoubleClick += new MouseEventHandler(childShape_MouseDoubleClick);
					childShape.Cursor = Cursors.Hand;
					bottomLevelShapes.Add(childShape);
				}
				shapeCanvasInheritance.BackColor = this.BackColor;
				shapeCanvasInheritance.Height = this.Height;
				shapeCanvasInheritance.DrawThreeLayerVertical(CentreShape, topLevelShapes, bottomLevelShapes);

				var inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(Entity);
				mnuSetDiscriminatorForSubClass.Visible = inheritanceType == EntityImpl.InheritanceType.TablePerSubClass || inheritanceType == EntityImpl.InheritanceType.TablePerSubClassWithDiscriminator;
			}
			finally
			{
				this.AutoScrollPosition = ScrollPosition;
				Slyce.Common.Utility.ResumePainting(this);
				Cursor = Cursors.Default;
				BusyPopulating = false;
			}
		}

		void childShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (EntitySelected != null)
				EntitySelected((EntityImpl)((RawShape)sender).Tag);
		}

		void parentShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (EntitySelected != null)
				EntitySelected((EntityImpl)((RawShape)sender).Tag);
		}

		void centreShape_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				HideAllContextMenuItems();

				EntityImpl.InheritanceType inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(Entity);

				if ((inheritanceType == EntityImpl.InheritanceType.None && (Entity.Children.Count == 0 || EntityImpl.DetermineInheritanceTypeWithChildren(Entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)) ||
					inheritanceType == EntityImpl.InheritanceType.TablePerClassHierarchy)
				{
					if (Entity.Children.Count == 0 && Entity.Parent == null)
						mnuCreateInheritanceHierarchy.Text = "Create inheritance hierarchy";
					else
						mnuCreateInheritanceHierarchy.Text = "Edit inheritance hierarchy";

					mnuCreateInheritanceHierarchy.Visible = true;
					contextMenuStrip1.BringToFront();
					contextMenuStrip1.Show(Cursor.Position);
				}
				else
				{
					var childInheritance = EntityImpl.DetermineInheritanceTypeWithChildren(Entity);

					if (childInheritance == EntityImpl.InheritanceType.TablePerSubClass ||
					childInheritance == EntityImpl.InheritanceType.TablePerSubClassWithDiscriminator)
					{
						mnuSetDiscriminatorForSubClass.Visible = true;
						contextMenuStrip1.BringToFront();
						contextMenuStrip1.Show(Cursor.Position);
					}
				}
			}
		}

		private void HideAllContextMenuItems()
		{
			foreach (ToolStripItem item in contextMenuStrip1.Items)
				item.Visible = false;
		}

		void ParentOriginatingLineStyle_MiddleImageClick(object sender, MouseEventArgs e)
		{
			LinkLine line = (LinkLine)sender;

			UserControls.FormInheritanceInfo form = new UserControls.FormInheritanceInfo((Entity)line.Parent.Tag, Entity);
			Point pt = line.MidPoint;
			pt.Offset(-1 * form.Width / 2, -1 * form.Height / 2);
			pt = this.PointToScreen(pt);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);

			form.Location = pt;

			//if (form.ShowDialog(this) == DialogResult.OK)
			//{
			//    //Populate();
			//}
			Populate();
		}

		void ChildOriginatingLineStyle_MiddleImageClick(object sender, MouseEventArgs e)
		{
			LinkLine line = (LinkLine)sender;

			UserControls.FormInheritanceInfo form = new UserControls.FormInheritanceInfo(Entity, (Entity)line.Parent.Tag);
			Point pt = line.MidPoint;
			pt.Offset(-1 * form.Width / 2, -1 * form.Height / 2);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);
			pt.Offset(this.Parent.Parent.Parent.Left, this.Parent.Top);
			form.Location = pt;
			//MappingEditor.BringToFront();
			//MappingEditor.Visible = true;
			//MappingEditor.Refresh();

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				//Populate();
			}
			Populate();
		}

		void childShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedChild = (RawShape)sender;
			SelectedParent = null;

			if (e.Button == MouseButtons.Right)
			{
				HideAllContextMenuItems();

				Entity childEntity = (Entity)SelectedChild.Tag;

				if (EntityImpl.DetermineInheritanceTypeWithParent(childEntity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
				{
					mnuCreateInheritanceHierarchy.Text = "Edit inheritance hierarchy";
					mnuCreateInheritanceHierarchy.Visible = true;
				}
				mnuRemove.Visible = true;
				mnuRemove.Text = "Remove child";
				var inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(Entity);
				mnuSetDiscriminatorForSubClass.Visible = inheritanceType == EntityImpl.InheritanceType.TablePerSubClass || inheritanceType == EntityImpl.InheritanceType.TablePerSubClassWithDiscriminator;
				contextMenuStrip1.Show(Cursor.Position);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (EntitySelected != null)
					EntitySelected((EntityImpl)((RawShape)sender).Tag);
			}
		}

		void emptyChild_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left)
				return;

			EntityImpl.InheritanceType inheritanceTypeWithChildren = EntityImpl.DetermineInheritanceTypeWithChildren(Entity);

			if (inheritanceTypeWithChildren == EntityImpl.InheritanceType.TablePerClassHierarchy)
			{
				ShowHierarchyEditor();
				return;
			}
			//if ((Entity.Children.Count > 0 && inheritanceTypeWithChildren == EntityImpl.InheritanceType.TablePerSubClass) ||
			//    inheritanceTypeWithChildren == EntityImpl.InheritanceType.TablePerConcreteClass ||
			//    inheritanceTypeWithChildren == EntityImpl.InheritanceType.TablePerClassHierarchy)// ||
			//{
			SelectedParent = null;
			SelectedChild = null;

			List<Entity> unavailableEntities = new List<Entity>();
			unavailableEntities.AddRange(Entity.Children);

			if (Entity.Parent != null)
				unavailableEntities.Add(Entity.Parent);

			UserControls.FormSelectEntityForInheritance form = new UserControls.FormSelectEntityForInheritance(Entity, unavailableEntities, null, "Select child entities", true, UserControls.FormSelectEntityForInheritance.RequestorTypes.Entity_Select_Child);
			form.EntityDeleted += new EventHandler(form_EntityDeleted);
			form.ShowDialog();

			if (form.SelectedEntities != null && form.SelectedEntities.Count > 0)
			{
				foreach (var entity in form.SelectedEntities)
				{
					Entity.AddChild(entity);
					entity.Parent = Entity;

					foreach (var directedReference in Entity.DirectedReferences)
					{
						if (directedReference.ToEntity == entity)
						{
							Entity.RemoveReference(directedReference.Reference);
							directedReference.Reference.DeleteSelf();
						}
					}
					foreach (var directedReference in entity.DirectedReferences)
					{
						if (directedReference.ToEntity == Entity)
						{
							entity.RemoveReference(directedReference.Reference);
							directedReference.Reference.DeleteSelf();
						}
					}
					// Remove the ID property from the child entity because it should share the parent entity's ID instead
					for (int i = entity.Key.Properties.ToList().Count - 1; i >= 0; i--)
					{
						if (Entity.IsAbstract)
							entity.Key.Properties.ElementAt(i).IsHiddenByAbstractParent = true;
						else
							entity.Key.Properties.ElementAt(i).IsPartOfHiddenKey = true;
					}
					if (Entity.IsAbstract)
					{
						foreach (Property parentProp in Entity.Properties)
						{
							Property childProp = entity.ConcreteProperties.SingleOrDefault(p => p.Name == parentProp.Name);

							if (childProp != null)
								childProp.IsHiddenByAbstractParent = true;
						}
					}
				}
				form.EntityDeleted -= form_EntityDeleted;
				Populate();

				RaiseInheritanceUpdatedEvent();
			}
			//}
			//else if (!HasOneToOne)
			//{
			//UserControls.FormInheritanceHierarchy form = new UserControls.FormInheritanceHierarchy(Entity);
			//form.ShowDialog(this);
			//}
		}

		void form_EntityDeleted(object sender, EventArgs e)
		{
			if (EntityDeleted != null)
				EntityDeleted(null);
		}

		private void RaiseInheritanceUpdatedEvent()
		{
			if (InheritanceUpdated != null)
				InheritanceUpdated(null, null);
		}

		void emptyParent_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left)
				return;

			//if (EntityImpl.DetermineInheritanceTypeWithChildren(Entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
			//{
			//    ShowHierarchyEditor();
			//    return;
			//}
			ShowSelectParent(false);
		}

		private void ShowSelectParent(bool onlyCreateAbstractParent)
		{
			SelectedParent = null;
			SelectedChild = null;

			List<Entity> unavailableEntities = new List<Entity>();
			unavailableEntities.AddRange(Entity.Children);
			unavailableEntities.Add(Entity.Parent);

			UserControls.FormSelectEntityForInheritance.RequestorTypes requestType = UserControls.FormSelectEntityForInheritance.RequestorTypes.Entity_Select_Parent;

			if (onlyCreateAbstractParent)
				requestType = UserControls.FormSelectEntityForInheritance.RequestorTypes.Entity_Create_Abstract_Parent;

			EntityImpl originalEntity = Entity;
			UserControls.FormSelectEntityForInheritance form = new UserControls.FormSelectEntityForInheritance(Entity, unavailableEntities, null, "Select or create parent entity", false, requestType);
			form.EntityDeleted += new EventHandler(form_EntityDeleted);
			form.ShowDialog();

			Entity = originalEntity;

			if (form.SelectedEntity != null)
			{
				Entity.Parent = form.SelectedEntity;
				form.SelectedEntity.AddChild(Entity);

				foreach (var directedReference in Entity.DirectedReferences)
				{
					if (directedReference.ToEntity == Entity.Parent)
					{
						Entity.RemoveReference(directedReference.Reference);
						directedReference.Reference.DeleteSelf();
					}
				}
				foreach (var directedReference in Entity.Parent.DirectedReferences)
				{
					if (directedReference.ToEntity == Entity)
					{
						Entity.Parent.RemoveReference(directedReference.Reference);
						directedReference.Reference.DeleteSelf();
					}
				}
				// Remove the ID property from the child entity because it should share the parent entity's ID instead
				if (!form.SelectedEntity.IsAbstract)
					for (int i = Entity.Key.Properties.ToList().Count - 1; i >= 0; i--)
						Entity.Key.Properties.ElementAt(i).IsPartOfHiddenKey = true;

				Populate();
				this.Refresh();
				RaiseInheritanceUpdatedEvent();
			}
			form.EntityDeleted -= form_EntityDeleted;
			Populate();
		}

		void parentShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedParent = (RawShape)sender;
			SelectedChild = null;

			if (e.Button == MouseButtons.Right)
			{
				HideAllContextMenuItems();

				Entity parentEntity = (Entity)SelectedParent.Tag;

				if (EntityImpl.DetermineInheritanceTypeWithParent(Entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
				{
					mnuCreateInheritanceHierarchy.Text = "Edit inheritance hierarchy";
					mnuCreateInheritanceHierarchy.Visible = true;
				}
				mnuRemove.Visible = true;
				mnuRemove.Text = "Remove parent";
				var inheritanceType = EntityImpl.DetermineInheritanceTypeWithParent(Entity);
				mnuSetDiscriminatorForSubClass.Visible = inheritanceType == EntityImpl.InheritanceType.TablePerSubClass || inheritanceType == EntityImpl.InheritanceType.TablePerSubClassWithDiscriminator;
				contextMenuStrip1.Show(Cursor.Position);
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (EntitySelected != null)
					EntitySelected((EntityImpl)((RawShape)sender).Tag);
			}
		}

		void OriginatingLineStyle_MouseClick(object sender, MouseEventArgs e)
		{
			//SelectedLinkLine = (LinkLine)sender;

			//if (e.Button == MouseButtons.Right)
			//    contextMenuStrip1.Show(Cursor.Position);
		}

		private void mnuRemove_Click(object sender, EventArgs e)
		{
			if (SelectedChild != null)
			{
				Entity childEntity = (Entity)SelectedChild.Tag;

				if (EntityImpl.DetermineInheritanceType(childEntity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
				{
					childEntity.DeleteSelf();

					if (EntityDeleted != null)
						EntityDeleted((EntityImpl)childEntity);
				}
				else
				{
					Entity.RemoveChild(childEntity);
				}
			}
			else if (SelectedParent != null)
			{
				Entity.RemoveParent();
			}
			Populate();
			RaiseInheritanceUpdatedEvent();
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			//if (SelectedChild == null && SelectedParent == null)
			//    e.Cancel = true;
		}

		int CurrentWidth = 0;
		int CurrentHeight = 0;

		private void EntityInheritanceDiagram_Resize(object sender, EventArgs e)
		{
			if (this.Width != CurrentWidth ||
				this.Height != CurrentHeight)
			{
				CurrentWidth = this.Width;
				CurrentHeight = this.Height;
				shapeCanvasInheritance.Size = this.Size;
			}
			this.Refresh();
			//shapeCanvasInheritance.Size = this.Size;
		}

		private void mnuCreateInheritanceHierarchy_Click(object sender, EventArgs e)
		{
			ShowHierarchyEditor();
		}

		private void ShowHierarchyEditor()
		{
			List<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable> mappedTables = Entity.MappedTables().ToList();

			if (mappedTables.Count == 0)
			{
				MessageBox.Show(this, "No tables are mapped to this entity. 'Table per Hierarchy' inheritance requires exactly one mapped table.", "No mapped table", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			else if (mappedTables.Count > 1)
			{
				MessageBox.Show(this, "Multiple tables are mapped to this entity. 'Table per Hierarchy' inheritance requires exactly one mapped table.", "Too many mapped tables", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			UserControls.FormInheritanceHierarchy form = new UserControls.FormInheritanceHierarchy(mappedTables[0]);
			form.ShowDialog(this);
			Populate();
		}

		private void EntityInheritanceDiagram_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
				ScrollPosition.Y = e.NewValue;
			else
				ScrollPosition.X = e.NewValue;
		}

		private void mnuSetDiscriminatorForSubClass_Click(object sender, EventArgs e)
		{
			ShowDiscriminatorForm();
		}

		private void ShowDiscriminatorForm()
		{
			var inheritanceType = EntityImpl.DetermineInheritanceTypeWithChildren(Entity);

			if (inheritanceType != EntityImpl.InheritanceType.TablePerSubClass &&
				inheritanceType != EntityImpl.InheritanceType.TablePerSubClassWithDiscriminator)
			{
				MessageBox.Show(this, "Invalid: only valid for Table Per Sub-Class inheritance.");
				return;
			}
			List<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable> mappedTables = Entity.MappedTables().ToList();

			if (mappedTables.Count == 0)
			{
				MessageBox.Show(this, "No tables are mapped to this entity. 'Table per Hierarchy' inheritance requires exactly one mapped table.", "No mapped table", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			else if (mappedTables.Count > 1)
			{
				MessageBox.Show(this, "Multiple tables are mapped to this entity. 'Table per Hierarchy' inheritance requires exactly one mapped table.", "Too many mapped tables", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			UserControls.FormSubClassDiscriminator form = new UserControls.FormSubClassDiscriminator(Entity);
			form.ShowDialog(this);
			Populate();
		}
	}
}
