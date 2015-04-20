using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormRefactorToComponent : Form
	{
		private struct LineWidth
		{
			public float NameWidth;
			public float TypeWidth;

			public LineWidth(float nameWidth, float typeWidth)
			{
				NameWidth = nameWidth;
				TypeWidth = typeWidth;
			}

			public float TotalWidth
			{
				get { return NameWidth + TypeWidth; }
			}
		}
		private static Image TableImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.TableHS.png"));
		private static Image EntityImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.square_light_blue_16.png"));

		private Font FontHeader;
		private Font FontItem;
		private Font FontType;
		private Brush TextBrush = new SolidBrush(Color.White);
		private Brush TypeBrush = new SolidBrush(Color.LightGray);
		private List<Property> Properties;
		private Entity Entity;
		private int Gap = 5;
		private Dictionary<object, LineWidth> LineWidths = new Dictionary<object, LineWidth>();
		private string NewComponentText = "New component name...";
		private ComponentSpecificationImpl NewComponent;
		private Slyce.Common.TextBoxFocusHelper FocusHelper;
		private ArchAngel.Providers.EntityModel.Model.EntityLayer.Component ComponentToEdit;

		public FormRefactorToComponent(EntityImpl entity, List<Property> properties)
		{
			InitializeComponent();

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);

			FontItem = new Font(Font.FontFamily.Name, 8F, FontStyle.Regular);
			FontType = new Font(Font.FontFamily.Name, 7F, FontStyle.Regular);

			FocusHelper = new Slyce.Common.TextBoxFocusHelper(new TextBox[] { textBoxComponentEntityName, textBoxNewComponentName });
			NewComponent = new ComponentSpecificationImpl(NewComponentText);

			foreach (Property prop in properties)
				NewComponent.AddProperty(ComponentPropertyImpl.CreateFromProperty(prop));

			textBoxNewComponentName.Text = NewComponentText;
			Entity = entity;
			Properties = properties;
			int maxHeight = Populate();
			ResizeForm(maxHeight);
		}

		public FormRefactorToComponent(ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component)
		{
			InitializeComponent();

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);

			FontItem = new Font(Font.FontFamily.Name, 8F, FontStyle.Regular);
			FontType = new Font(Font.FontFamily.Name, 7F, FontStyle.Regular);

			FocusHelper = new Slyce.Common.TextBoxFocusHelper(new TextBox[] { textBoxComponentEntityName, textBoxNewComponentName });
			ComponentToEdit = component;

			//Entity = entity;
			//Properties = properties;
			int maxHeight = Populate();
			ResizeForm(maxHeight);
		}

		private List<Label> PropertyLabels = new List<Label>();

		private int Populate()
		{
			if (Entity == null)
			{
				labelComponentName.Top = 10;

				pictureEntity.Top = labelComponentName.Bottom + 5;
				pictureComponent.Top = pictureEntity.Top;
				labelEntityName.Top = pictureEntity.Top;
				textBoxComponentEntityName.Top = pictureEntity.Top;
			}
			PropertyLabels.Clear();
			Point pos = pictureEntity.Location;
			pos.Offset(0, pictureEntity.Height + Gap * 3);

			if (Entity != null)
			{
				labelEntityName.Text = Entity.Name;

				foreach (var prop in Properties)
				{
					Label label = new Label();
					label.AutoSize = true;
					label.ForeColor = Color.White;
					label.Location = pos;
					label.Text = string.Format("{0}  ({1})", prop.Name, prop.NHibernateType);
					this.Controls.Add(label);
					PropertyLabels.Add(label);
					pos.Offset(0, (int)label.Height + Gap * 3);
				}
			}
			else
			{
				comboBoxComponents.Visible = false;
				label1.Visible = false;
				label2.Visible = false;
				textBoxNewComponentName.Visible = false;
				labelEntityName.Text = ComponentToEdit.ParentEntity.Name;

				foreach (var prop in ComponentToEdit.Properties)
				{
					Label label = new Label();
					label.AutoSize = true;
					label.ForeColor = Color.White;
					label.Location = pos;
					label.Text = string.Format("{0}  ({1})", prop.PropertyName, prop.RepresentedProperty.Type);
					this.Controls.Add(label);
					PropertyLabels.Add(label);
					pos.Offset(0, (int)label.Height + Gap * 3);
				}
			}
			pictureComponent.Left = GetWidestProperty() + Gap * 2 + 100;

			if (Entity != null)
			{
				comboBoxComponents.Items.Add("Create new component...");

				foreach (var component in Entity.EntitySet.ComponentSpecifications)
					comboBoxComponents.Items.Add(component.Name);

				comboBoxComponents.SelectedIndex = 0;
			}
			else
			{
				foreach (var component in ComponentToEdit.ParentEntity.EntitySet.ComponentSpecifications)
					comboBoxComponents.Items.Add(component.Name);

				comboBoxComponents.Text = ComponentToEdit.Specification.Name;
			}
			return Math.Max(pos.Y, PopulateComponentSide());
		}

		private List<Control> ComponentControls = new List<Control>();

		private int PopulateComponentSide()
		{
			int maxHeight = 0;

			foreach (Control control in ComponentControls)
			{
				if (control is TextBox)
					FocusHelper.Remove((TextBox)control);

				this.Controls.Remove(control);
			}
			ComponentControls.Clear();
			Point pos = pictureComponent.Location;
			labelComponentName.Left = pictureComponent.Right + Gap;
			textBoxComponentEntityName.Left = labelComponentName.Left;
			pos.Offset(0, pictureEntity.Height + Gap);
			List<string> allComponentProperties = new List<string>();

			if (ComponentToEdit != null)
			{
				textBoxComponentEntityName.Text = ComponentToEdit.Name;

				foreach (var prop in ComponentToEdit.Properties)
					allComponentProperties.Add(string.Format("{0}  ({1})", prop.PropertyName, prop.RepresentedProperty.Type));

				for (int i = 0; i < ComponentToEdit.Properties.Count; i++)
				{
					pos.Y = PropertyLabels[i].Top;
					var prop = ComponentToEdit.Properties[i];
					ComboBox combo = new ComboBox();
					combo.Location = pos;
					combo.Items.AddRange(allComponentProperties.ToArray());
					combo.Text = string.Format("{0}  ({1})", prop.PropertyName, prop.RepresentedProperty.Type);
					//label.Text = string.Format("{0}  ({1})", prop.Name, prop.Type);
					this.Controls.Add(combo);
					pos.Offset(0, (int)combo.Height + Gap + 2);
					ComponentControls.Add(combo);
				}
			}
			else
			{
				labelComponentName.Text = "New Component";
				string[] types = new string[] { "string", "int", "bool", "double", "DateTime" };

				// Textboxes to allow user to specify new component properties
				for (int i = 0; i < NewComponent.Properties.Count; i++)
				{
					var prop = NewComponent.Properties[i];
					pos.Y = PropertyLabels[i].Top;
					TextBox tbName = new TextBox();
					tbName.Text = prop.Name;
					tbName.Location = pos;
					tbName.Width = 100;
					ComponentControls.Add(tbName);
					this.Controls.Add(tbName);
					FocusHelper.Add(tbName);

					Point posType = new Point(pos.X + tbName.Width + Gap, pos.Y);
					ComboBox comboTypes = new ComboBox();
					comboTypes.Items.AddRange(types);

					if (!types.Contains(prop.Type))
						comboTypes.Items.Add(prop.Type);

					comboTypes.Text = prop.Type;
					comboTypes.Location = posType;
					comboTypes.Width = 60;
					ComponentControls.Add(comboTypes);
					this.Controls.Add(comboTypes);
					pos.Offset(0, (int)tbName.Height + Gap + 2);
				}
			}
			return pos.Y;
		}

		private void ResizeForm(int maxHeight)
		{
			this.Height = Math.Max(maxHeight + buttonOk.Height + Gap * 15, 280);
		}

		private int GetWidestProperty()
		{
			int maxWidth = 0;
			Graphics g = Graphics.FromHwnd(this.Handle);

			if (Entity != null)
				foreach (var prop in Properties)
				{
					string text = string.Format("{0}  ({1})", prop.Name, prop.NHibernateType);
					maxWidth = Math.Max(maxWidth, (int)g.MeasureString(text, FontItem).Width);
				}
			else
				foreach (var prop in ComponentToEdit.Properties)
				{
					string text = string.Format("{0}  ({1})", prop.PropertyName, prop.RepresentedProperty.Type);
					maxWidth = Math.Max(maxWidth, (int)g.MeasureString(text, FontItem).Width);
				}

			return maxWidth;
		}

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//    base.OnPaint(e);

		//    Graphics g = e.Graphics;
		//    Point pos = pictureEntity.Location;
		//    Point typePos;
		//    pos.Offset(0, pictureEntity.Height + Gap);

		//    foreach (var prop in Properties)
		//    {
		//        SizeF textSize = g.MeasureString(prop.Name, FontItem);
		//        g.DrawString(prop.Name, FontItem, TextBrush, pos);
		//        typePos = new Point(pos.X + (int)textSize.Width + 5, pos.Y);
		//        g.DrawString(string.Format("({0})", prop.Type), FontType, TypeBrush, typePos);
		//        pos.Offset(0, (int)textSize.Height + Gap);
		//    }
		//}

		private void comboBoxComponents_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Entity != null &&
				comboBoxComponents.SelectedIndex == 0)
			{
				textBoxNewComponentName.Visible = true;
				label2.Visible = true;
			}
			else
			{
				textBoxNewComponentName.Visible = false;
				label2.Visible = false;
			}
			PopulateComponentSide();
		}

		private ComponentSpecificationImpl SelectedComponent
		{
			get
			{
				foreach (ComponentSpecificationImpl component in Entity.EntitySet.ComponentSpecifications)
					if (component.Name == comboBoxComponents.Text)
						return component;

				return null;
			}
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (Entity != null &&
				(textBoxNewComponentName.Text == NewComponentText ||
				string.IsNullOrEmpty(textBoxNewComponentName.Text)))
			{
				MessageBox.Show(this, "Please enter a valid name for the new component.", "Invalid name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (Entity != null)
			{
				NewComponent.Name = textBoxNewComponentName.Text;

				for (int i = 0; i < NewComponent.Properties.Count; i++)
				{
					int nameIndex = 2 * i;
					int typeIndex = nameIndex + 1;
					NewComponent.Properties[i].Name = ((TextBox)ComponentControls[nameIndex]).Text;
					NewComponent.Properties[i].Type = ((ComboBox)ComponentControls[typeIndex]).Text;
				}
				Entity.EntitySet.AddComponentSpecification(NewComponent);

				NewComponent.CreateImplementedComponentFor(Entity, textBoxComponentEntityName.Text);
				ArchAngel.Providers.EntityModel.Model.EntityLayer.Component component = NewComponent.GetImplementationFor(Entity);
				component.Name = textBoxComponentEntityName.Text;

				for (int i = 0; i < component.Properties.Count; i++)
				{
					IColumn col = Properties[i].MappedColumn();
					component.Properties[i].SetMappedColumn(col);
				}
			}
			else
			{
				ComponentToEdit.Name = textBoxComponentEntityName.Text;

				for (int i = 0; i < ComponentToEdit.Properties.Count; i++)
				{
					int nameIndex = 2 * i;
					int typeIndex = nameIndex + 1;
					//ComponentToEdit.Properties[i].PropertyName = ((TextBox)ComponentControls[nameIndex]).Text;
					//ComponentToEdit.Properties[i].Type = ((ComboBox)ComponentControls[typeIndex]).Text;
				}
			}
			Close();
		}
	}
}
