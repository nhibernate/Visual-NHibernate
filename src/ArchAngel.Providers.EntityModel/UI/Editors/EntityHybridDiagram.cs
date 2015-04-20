using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.Controls.Diagramming.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class EntityHybridDiagram : UserControl
	{
		private EntityImpl _Entity;
		//private LinkLine SelectedLinkLine = null;
		private RawShape SelectedParent = null;
		private RawShape SelectedChild = null;

		public EntityHybridDiagram()
		{
			InitializeComponent();

			this.BackColor = Color.Black;
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
			Font boldFont = new Font(Font, FontStyle.Bold);
			Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);
			RawShape centreShape = new RawEntity(shapeCanvas1, Entity.Name, boldFont, Entity);

			List<RawShape> level1Shapes = new List<RawShape>();
			List<RawShape> level3Shapes = new List<RawShape>();
			List<RawShape> rightAlignedShapes = new List<RawShape>();

			if (Entity.Parent == null)
			{
				RawShape emptyParent = new RawShape(shapeCanvas1, "Add parent...", boldUnderlineFont)
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
					OriginatingLineStyle = null,//new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow),
					Tag = null
				};
				emptyParent.MouseClick += new MouseEventHandler(emptyParent_MouseClick);
				level1Shapes.Add(emptyParent);
			}
			else
			{
				RawEntity parentShape = new RawEntity(shapeCanvas1, Entity.Parent.Name, boldFont, Entity.Parent);
				parentShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
				parentShape.MouseClick += new MouseEventHandler(parentShape_MouseClick);
				level1Shapes.Add(parentShape);

				foreach (var table in Entity.Parent.MappedTables())
				{
					RawTable tableShape = new RawTable(shapeCanvas1, table.Name, boldFont, table);
					tableShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
					tableShape.MouseClick += new MouseEventHandler(tableShape_MouseClick);
					rightAlignedShapes.Add(tableShape);
				}
			}
			foreach (var table in Entity.MappedTables())
			{
				RawTable tableShape = new RawTable(shapeCanvas1, table.Name, boldFont, table);
				tableShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
				tableShape.MouseClick += new MouseEventHandler(tableShape_MouseClick);
				rightAlignedShapes.Add(tableShape);
			}
			foreach (Entity child in Entity.Children)
			{
				RawEntity childShape = new RawEntity(shapeCanvas1, child.Name, boldFont, child);
				childShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
				childShape.MouseClick += new MouseEventHandler(childShape_MouseClick);
				level3Shapes.Add(childShape);

				foreach (var table in child.MappedTables())
				{
					RawTable tableShape = new RawTable(shapeCanvas1, table.Name, boldFont, table);
					tableShape.OriginatingLineStyle = new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow);
					tableShape.MouseClick += new MouseEventHandler(tableShape_MouseClick);
					rightAlignedShapes.Add(tableShape);
				}
			}
			#region Add empty child
			RawShape emptyChild = new RawShape(shapeCanvas1, "Add child...", boldUnderlineFont)
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
				OriginatingLineStyle = null//new LinkLine(boldFont, DashStyle.Dot, "", "", "", LineCaps.None, LineCaps.SolidArrow)
			};
			emptyChild.MouseClick += new MouseEventHandler(emptyChild_MouseClick);
			//emptyChild.OriginatingLineStyle.MouseClick += new MouseEventHandler(OriginatingLineStyle_MouseClick);
			level3Shapes.Add(emptyChild);
			#endregion

			shapeCanvas1.BackColor = this.BackColor;
			shapeCanvas1.SwimLane1 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.Black, Color.White, 90F, "Parent", ShapeCanvas.SwimLaneStyle.Styles.Line);
			shapeCanvas1.SwimLane2 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.Black, Color.White, 90F, "Entity", ShapeCanvas.SwimLaneStyle.Styles.Line);
			shapeCanvas1.SwimLane3 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.Black, Color.White, 90F, "Children", ShapeCanvas.SwimLaneStyle.Styles.Line);
			shapeCanvas1.SwimLane4 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(100, 100, 100), Color.Black, Color.White, 90F, "Mapped Tables", ShapeCanvas.SwimLaneStyle.Styles.Line);

			shapeCanvas1.BackColor = this.BackColor;
			shapeCanvas1.DrawVerticalSwimLanes(centreShape, level1Shapes, level3Shapes, rightAlignedShapes, "Parent", "Entity", "Children", "Mapped Tables");
		}

		void emptyChild_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedParent = null;
			SelectedChild = null;

			List<Entity> unavailableEntities = new List<Entity>();
			unavailableEntities.AddRange(Entity.Children);
			unavailableEntities.Add(Entity.Parent);

			UserControls.FormSelectEntityForInheritance form = new UserControls.FormSelectEntityForInheritance(Entity, unavailableEntities, null, "Select child entity", false, UserControls.FormSelectEntityForInheritance.RequestorTypes.Entity_Select_Child);
			form.ShowDialog();

			if (form.SelectedEntity != null)
			{
				Entity.AddChild(form.SelectedEntity);
				Populate();
			}
		}

		void childShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedChild = (RawShape)sender;
			SelectedParent = null;

			if (e.Button == MouseButtons.Right)
			{
				mnuRemove.Text = "Remove child";
				contextMenuStrip1.Show(Cursor.Position);
			}
		}

		void emptyParent_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedParent = null;
			SelectedChild = null;

			List<Entity> unavailableEntities = new List<Entity>();
			unavailableEntities.AddRange(Entity.Children);
			unavailableEntities.Add(Entity.Parent);

			UserControls.FormSelectEntityForInheritance form = new UserControls.FormSelectEntityForInheritance(Entity, unavailableEntities, null, "Select or create parent entity", false, UserControls.FormSelectEntityForInheritance.RequestorTypes.Entity_Select_Parent);
			form.ShowDialog();

			if (form.SelectedEntity != null)
			{
				Entity.Parent = form.SelectedEntity;
				Populate();
			}
		}

		void parentShape_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedParent = (RawShape)sender;
			SelectedChild = null;

			if (e.Button == MouseButtons.Right)
			{
				mnuRemove.Text = "Remove parent";
				contextMenuStrip1.Show(Cursor.Position);
			}
		}

		void tableShape_MouseClick(object sender, MouseEventArgs e)
		{
			//SelectedParent = (RawShape)sender;
			//SelectedChild = null;

			//if (e.Button == MouseButtons.Right)
			//{
			//    mnuRemove.Text = "Remove parent";
			//    contextMenuStrip1.Show(Cursor.Position);
			//}
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (SelectedChild == null && SelectedParent == null)
				e.Cancel = true;
		}

		private void mnuRemove_Click(object sender, EventArgs e)
		{
			if (SelectedChild != null)
				Entity.RemoveChild((Entity)SelectedChild.Tag);
			else if (SelectedParent != null)
				Entity.RemoveParent();

			Populate();
		}

		void OriginatingLineStyle_MouseClick(object sender, MouseEventArgs e)
		{
			//SelectedLinkLine = (LinkLine)sender;

			//if (e.Button == MouseButtons.Right)
			//    contextMenuStrip1.Show(Cursor.Position);
		}

	}
}
