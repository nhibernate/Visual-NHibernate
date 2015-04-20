using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
//using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using Slyce.Common.Controls.Diagramming.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class ComponentMappingDiagram : UserControl
	{
		public delegate void EntityActionDelegate(Entity entity);
		public event EntityActionDelegate EntitySelected;

		private static Image EntityImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.square_light_blue_16.png"));
		private static Image InfoImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.about_16.png"));
		private ComponentSpecification _ComponentSpecification;
		private bool KeepMainShapeFull = true;
		//UserControls.FormComponentMappingEditor MappingEditor;
		private RawComponent MainShape;

		public ComponentMappingDiagram()
		{
			InitializeComponent();

			this.BackColor = Color.Black;
			//MappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormComponentMappingEditor();
			//MappingEditor.Visible = false;
			//MappingEditor.VisibleChanged += new EventHandler(MappingEditor_VisibleChanged);
			//this.Controls.Add(MappingEditor);
			shapeCanvas1.MouseClick += new MouseEventHandler(shapeCanvas1_MouseClick);
		}

		void shapeCanvas1_MouseClick(object sender, MouseEventArgs e)
		{
			//if (MappingEditor.Visible && !MappingEditor.Bounds.Contains(e.Location))
			//    MappingEditor.Visible = false;
		}

		void MappingEditor_VisibleChanged(object sender, EventArgs e)
		{
			Populate();
		}

		public ComponentSpecification ComponentSpecification
		{
			get { return _ComponentSpecification; }
			set
			{
				_ComponentSpecification = value;

				if (_ComponentSpecification != null)
				{
					Populate();
				}
			}
		}

		private void Populate()
		{
			Draw();
		}

		private void Draw()
		{
			if (ComponentSpecification == null)
				return;

			Cursor = Cursors.WaitCursor;
			Font boldFont = new Font(Font, FontStyle.Bold);
			MainShape = new RawComponent(shapeCanvas1, ComponentSpecification.Name, boldFont, ComponentSpecification);
			MainShape.Categories.Clear();

			RawCategory cat = new RawCategory("Properties", Font, MainShape);
			MainShape.Categories.Add(cat);

			foreach (var x in ComponentSpecification.Properties)
			{
				//RawProperty prop = new RawProperty(string.Format("{0} [{1}]", x.Name, x.Type), x);
				RawProperty prop = new RawProperty(x.Name, x);
				cat.Properties.Add(prop);
			}

			//centreShape.Icon = EntityImage;
			//centreShape.Enter += new EventHandler(centreShape_Enter);
			//centreShape.Leave += new EventHandler(centreShape_Leave);
			Font boldUnderlineFont = new Font(Font, FontStyle.Bold | FontStyle.Underline);
			List<RawShape> topLevelShapes = new List<RawShape>();
			List<RawShape> rightAlignedShapes = new List<RawShape>();

			#region References

			foreach (ComponentImpl component in ComponentSpecification.ImplementedComponents)
			{
				RawEntity referencedEntityShape;
				CustomLineCap startCap;
				CustomLineCap endCap;

				referencedEntityShape = new RawEntity(shapeCanvas1, component.ParentEntity.Name, boldFont, component.ParentEntity);
				//string end1 = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2) ? "1" : "m";
				//startCap = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality2) ? LineCaps.One : LineCaps.Many;
				//endCap = ArchAngel.Interfaces.Cardinality.One.Equals(reference.Cardinality1) ? LineCaps.One : LineCaps.Many;
				referencedEntityShape.OriginatingLineStyle.EndTextDataMember = "Name";
				//referencedEntityShape.OriginatingLineStyle.EndTextDataMember = "End2Name";
				//referencedEntityShape.OriginatingLineStyle.StartImageClick += new LinkLine.MouseEndDelegate(OriginatingLineStyle_StartImageClick);
				//referencedEntityShape.OriginatingLineStyle.EndImageClick += new LinkLine.MouseEndDelegate(OriginatingLineStyle_EndImageClick);
				//referencedEntityShape.OriginatingLineStyle.MouseOverEnd1 += new LinkLine.MouseOverEndDelegate(OriginatingLineStyle_MouseOverEnd2);
				//referencedEntityShape.OriginatingLineStyle.MouseOverEnd2 += new LinkLine.MouseOverEndDelegate(OriginatingLineStyle_MouseOverEnd1);


				referencedEntityShape.Icon = EntityImage;
				referencedEntityShape.OriginatingLineStyle.LineStyle = DashStyle.Solid;
				//referencedEntityShape.OriginatingLineStyle.StartCap = startCap;
				//referencedEntityShape.OriginatingLineStyle.EndCap = endCap;
				referencedEntityShape.OriginatingLineStyle.DataObject = component;
				referencedEntityShape.OriginatingLineStyle.ForeColor = Color.White;
				referencedEntityShape.OriginatingLineStyle.DefaultEndText = "";
				//referencedEntityShape.OriginatingLineStyle.StartImage = EditImage;
				//referencedEntityShape.OriginatingLineStyle.EndImage = EditImage;
				referencedEntityShape.OriginatingLineStyle.MiddleImage = InfoImage;
				referencedEntityShape.OriginatingLineStyle.MiddleImageClick += new MouseEventHandler(OriginatingLineStyle_MiddleImageClick);
				referencedEntityShape.MouseDoubleClick += new MouseEventHandler(referencedEntityShape_MouseDoubleClick);
				referencedEntityShape.MouseClick += new MouseEventHandler(referencedEntityShape_MouseClick);
				referencedEntityShape.Cursor = Cursors.Hand;
				topLevelShapes.Add(referencedEntityShape);
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
			topLevelShapes.Add(emptyReference);
			#endregion

			shapeCanvas1.SwimLane1 = new ShapeCanvas.SwimLaneStyle(Color.Gray, Color.Black, Color.White, 180F, "Used By (Entities)", ShapeCanvas.SwimLaneStyle.Styles.Fill);
			//shapeCanvas1.SwimLane3 = new ShapeCanvas.SwimLaneStyle(Color.FromArgb(79, 124, 205), Color.Black, Color.White, 0F, "Mapped Tables", ShapeCanvas.SwimLaneStyle.Styles.Fill);

			shapeCanvas1.BackColor = this.BackColor;
			shapeCanvas1.Height = this.Height;
			shapeCanvas1.KeepMainShapeCentered = true;
			shapeCanvas1.DrawThreeLayerHorizontal(MainShape, topLevelShapes, rightAlignedShapes, KeepMainShapeFull);
			Cursor = Cursors.Default;
		}

		void referencedEntityShape_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (EntitySelected != null)
					EntitySelected((Entity)((RawShape)sender).Tag);
			}
		}

		private void referencedEntityShape_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (EntitySelected != null)
				EntitySelected((Entity)((RawShape)sender).Tag);
		}

		private void OriginatingLineStyle_MiddleImageClick(object sender, MouseEventArgs e)
		{
			LinkLine line = (LinkLine)sender;
			ComponentImpl component = (ComponentImpl)line.DataObject;
			List<Entity> unavailableEntities = new List<Entity>();
			UserControls.FormComponentMappingEditor MappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormComponentMappingEditor();
			MappingEditor.Fill(ComponentSpecification, component.ParentEntity);
			Point pt = line.MidPoint;
			pt.Offset(-1 * MappingEditor.Width / 2, -1 * MappingEditor.Height / 2);

			if (pt.Y < 2)
				pt.Offset(0, 5 - pt.Y);

			MappingEditor.Location = pt;
			//MappingEditor.BringToFront();
			//MappingEditor.Visible = true;
			//MappingEditor.Refresh();

			if (MappingEditor.ShowDialog(this) == DialogResult.OK)
			{
				//Populate();
			}
			Populate();
		}

		void emptyReference_MouseClick(object sender, MouseEventArgs e)
		{
			if (ComponentSpecification.Properties.Count == 0)
			{
				MessageBox.Show("Add some properties to this components before adding a usage. Use the Properties grid below.", "No properties defined", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			List<Entity> unavailableEntities = new List<Entity>();

			UserControls.FormComponentMappingEditor MappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.FormComponentMappingEditor();
			MappingEditor.Fill(ComponentSpecification, null);
			MappingEditor.Location = e.Location;
			//MappingEditor.BringToFront();
			//MappingEditor.Visible = true;

			if (MappingEditor.ShowDialog(this) == DialogResult.OK)
			{
				//Populate();
			}
			Populate();
		}

	}
}
