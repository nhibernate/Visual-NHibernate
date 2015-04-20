using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common.Controls.Diagramming.SlyceGrid;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    public partial class NamespaceEditor : UserControl
    {
        private enum SelectedTypes
        {
            Namespace,
            Implement,
            Attribute,
            Function,
            Property
        }
        private EntitySet EntitySet;
        private NamespaceInput Input = new NamespaceInput();
        private FormCodeInput FormCodeInput = new FormCodeInput();
        private string SelectedNamespace = "";
        private string SelectedImplement = "";
        private string SelectedAttribute = "";
        private string SelectedFunction = "";
        private string SelectedProperty = "";
        public bool ShowNamespaces = true;
        public bool ShowImplements = true;
        public bool ShowAttributes = true;
        public bool ShowFunctions = true;
        public bool ShowProperties = true;
        private SelectedTypes SelectedType = SelectedTypes.Namespace;

        public NamespaceEditor()
        {
            InitializeComponent();

            InitEntitiesGrid();

            //Input.Visible = false;
            //this.Controls.Add(Input);
            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void InitEntitiesGrid()
        {
            slyceGridEntities.InvalidColor = Color.FromArgb(100, 100, 100);
            slyceGridEntities.DisabledColor = Color.FromArgb(25, 25, 25);
            slyceGridEntities.BackColor = Color.Black;
            slyceGridEntities.AlternatingBackColor = Color.FromArgb(10, 10, 10);
            slyceGridEntities.AllowUserToAddRows = false;
            slyceGridEntities.CellValueChanged += new SlyceGrid.CellValueChangedDelegate(slyceGridEntities_CellValueChanged);
        }

        void slyceGridEntities_CellValueChanged(int row, int cell, int column, string columnHeader, ref object tag, object newValue)
        {
            if (slyceGridEntities.Columns[column].Tag is CustomNamespace)
            {
                CustomNamespace ns = (CustomNamespace)slyceGridEntities.Columns[column].Tag;
                Entity entity = (Entity)slyceGridEntities.Items[row].Tag;

                bool isChecked = (bool)newValue;

                if (isChecked)
                    ns.Entities.Add(entity);
                else
                    ns.Entities.Remove(entity);
            }
            else if (slyceGridEntities.Columns[column].Tag is CustomImplement)
            {
                CustomImplement ci = (CustomImplement)slyceGridEntities.Columns[column].Tag;
                Entity entity = (Entity)slyceGridEntities.Items[row].Tag;

                bool isChecked = (bool)newValue;

                if (isChecked)
                    ci.Entities.Add(entity);
                else
                    ci.Entities.Remove(entity);
            }
            else if (slyceGridEntities.Columns[column].Tag is CustomAttribute)
            {
                CustomAttribute ca = (CustomAttribute)slyceGridEntities.Columns[column].Tag;
                Entity entity = (Entity)slyceGridEntities.Items[row].Tag;

                bool isChecked = (bool)newValue;

                if (isChecked)
                    ca.Entities.Add(entity);
                else
                    ca.Entities.Remove(entity);
            }
            else if (slyceGridEntities.Columns[column].Tag is CustomProperty)
            {
                CustomProperty cp = (CustomProperty)slyceGridEntities.Columns[column].Tag;
                Entity entity = (Entity)slyceGridEntities.Items[row].Tag;

                bool isChecked = (bool)newValue;

                if (isChecked)
                    cp.Entities.Add(entity);
                else
                    cp.Entities.Remove(entity);
            }
            else if (slyceGridEntities.Columns[column].Tag is CustomFunction)
            {
                CustomFunction cm = (CustomFunction)slyceGridEntities.Columns[column].Tag;
                Entity entity = (Entity)slyceGridEntities.Items[row].Tag;

                bool isChecked = (bool)newValue;

                if (isChecked)
                    cm.Entities.Add(entity);
                else
                    cm.Entities.Remove(entity);
            }
        }

        bool slyceGridNamespaces_DeleteClicked(int row, object tag)
        {
            EntitySet.RemoveNamespace((CustomNamespace)tag);
            return true;
        }

        public void FillData(EntitySet entitySet)
        {
            EntitySet = entitySet;
            Populate();
        }

        private void Populate()
        {
            PopulateEntitiesGrid();
        }

        private void PopulateEntitiesGrid()
        {
            Slyce.Common.Utility.SuspendPainting(slyceGridEntities);
            slyceGridEntities.ShowDeleteColumn = false;
            slyceGridEntities.Clear();
            ColumnItem entityCol = new ColumnItem("Entity", ColumnItem.ColumnTypes.Textbox, "NewProp", "");
            entityCol.ColorScheme = ColumnItem.ColorSchemes.Black;
            entityCol.ReadOnly = true;
            slyceGridEntities.Columns.Add(entityCol);

            #region Add custom namespaces
            if (ShowNamespaces)
            {
                bool exists = false;

                foreach (CustomNamespace ns in EntitySet.CustomNamespaces.OrderBy(n => n.Value))
                {
                    exists = true;
                    ColumnItem col = new ColumnItem(ns.Value, ColumnItem.ColumnTypes.Checkbox, "NewProp", "Namespaces");
                    col.IsLink = true;
                    col.Clicked += new MouseEventHandler(colNamespace_Clicked);
                    col.Tag = ns;
                    col.ColorScheme = ColumnItem.ColorSchemes.Green;
                    slyceGridEntities.Columns.Add(col);
                }
                if (!exists)
                {
                    ColumnItem addNewColumn = new ColumnItem("", ColumnItem.ColumnTypes.None, null, "Namespaces");
                    addNewColumn.IsLink = false;
                    addNewColumn.ColorScheme = ColumnItem.ColorSchemes.Green;
                    slyceGridEntities.Columns.Add(addNewColumn);
                }
            }
            #endregion

            #region Add custom attributes
            if (ShowAttributes)
            {
                bool exists = false;

                foreach (CustomAttribute ca in EntitySet.CustomAttributes.OrderBy(a => a.RawName))
                {
                    exists = true;
                    ColumnItem col = new ColumnItem(ca.RawName, ColumnItem.ColumnTypes.Checkbox, "NewProp", "Attributes");
                    col.IsLink = true;
                    col.Clicked += new MouseEventHandler(colAttribute_Clicked);
                    col.Tag = ca;
                    col.ColorScheme = ColumnItem.ColorSchemes.Blue;
                    slyceGridEntities.Columns.Add(col);
                }
                if (!exists)
                {
                    ColumnItem addNewColumn = new ColumnItem("", ColumnItem.ColumnTypes.None, null, "Attributes");
                    addNewColumn.IsLink = false;
                    addNewColumn.ColorScheme = ColumnItem.ColorSchemes.Blue;
                    slyceGridEntities.Columns.Add(addNewColumn);
                }
            }
            #endregion

            #region Add custom base names
            if (ShowImplements)
            {
                bool exists = false;

                foreach (CustomImplement ci in EntitySet.CustomImplements.OrderBy(n => n.Value))
                {
                    exists = true;
                    ColumnItem col = new ColumnItem(ci.Value, ColumnItem.ColumnTypes.Checkbox, "NewProp", "Base Names");
                    col.IsLink = true;
                    col.Clicked += new MouseEventHandler(colImplement_Clicked);
                    col.Tag = ci;
                    col.ColorScheme = ColumnItem.ColorSchemes.Yellow;
                    slyceGridEntities.Columns.Add(col);
                }
                if (!exists)
                {
                    ColumnItem addNewColumn = new ColumnItem("", ColumnItem.ColumnTypes.None, null, "Base Names");
                    addNewColumn.IsLink = false;
                    addNewColumn.ColorScheme = ColumnItem.ColorSchemes.Yellow;
                    slyceGridEntities.Columns.Add(addNewColumn);
                }
            }
            #endregion

            #region Add custom properties
            if (ShowProperties)
            {
                bool exists = false;

                foreach (CustomProperty cp in EntitySet.CustomProperties.OrderBy(n => n.Name))
                {
                    exists = true;
                    ColumnItem col = new ColumnItem(cp.Name, ColumnItem.ColumnTypes.Checkbox, "NewProp", "Properties");
                    col.IsLink = true;
                    col.Clicked += new MouseEventHandler(colProperty_Clicked);
                    col.Tag = cp;
                    col.ColorScheme = ColumnItem.ColorSchemes.Red;
                    slyceGridEntities.Columns.Add(col);
                }
                if (!exists)
                {
                    ColumnItem addNewColumn = new ColumnItem("", ColumnItem.ColumnTypes.None, null, "Properties");
                    addNewColumn.IsLink = false;
                    addNewColumn.ColorScheme = ColumnItem.ColorSchemes.Red;
                    slyceGridEntities.Columns.Add(addNewColumn);
                }
            }
            #endregion

            #region Add custom functions
            if (ShowFunctions)
            {
                bool exists = false;

                foreach (CustomFunction cm in EntitySet.CustomFunctions.OrderBy(n => n.Name))
                {
                    exists = true;
                    ColumnItem col = new ColumnItem(cm.Name, ColumnItem.ColumnTypes.Checkbox, "NewProp", "Functions");
                    col.IsLink = true;
                    col.Clicked += new MouseEventHandler(colMethod_Clicked);
                    col.Tag = cm;
                    col.ColorScheme = ColumnItem.ColorSchemes.Orange;
                    slyceGridEntities.Columns.Add(col);
                }
                if (!exists)
                {
                    ColumnItem addNewColumn = new ColumnItem("", ColumnItem.ColumnTypes.None, null, "Functions");
                    addNewColumn.IsLink = false;
                    addNewColumn.ColorScheme = ColumnItem.ColorSchemes.Orange;
                    slyceGridEntities.Columns.Add(addNewColumn);
                }
            }
            #endregion

            foreach (Entity entity in EntitySet.Entities)
                AddEntityToEntitiesGrid(entity);

            slyceGridEntities.Populate();
            slyceGridEntities.FrozenColumnIndex = 1;
            Slyce.Common.Utility.ResumePainting(slyceGridEntities);
        }

        public void AddNewNamespace()
        {
            Input.Text = "";
            Input.AutoAddToEntities = true;
            Input.DisplayName = "Namespace:";

            if (Input.ShowDialog(this) == DialogResult.OK)
            {
                CustomNamespace cn = new CustomNamespace(Input.Text);
                cn.AutoAddToEntities = Input.AutoAddToEntities;
                EntitySet.AddNamespace(cn, true);
                PopulateEntitiesGrid();
            }
        }

        public void AddNewBaseName()
        {
            Input.Text = "";
            Input.AutoAddToEntities = true;
            Input.DisplayName = "Base name:";

            if (Input.ShowDialog(this) == DialogResult.OK)
            {
                CustomImplement ci = new CustomImplement(Input.Text);
                ci.AutoAddToEntities = Input.AutoAddToEntities;
                EntitySet.AddCustomImplement(ci, true);
                PopulateEntitiesGrid();
            }
        }

        public void AddNewProperty()
        {
            FormCodeInput.FillData(FormCodeInput.EditTypes.CustomProperty);

            if (FormCodeInput.ShowDialog(this) == DialogResult.OK)
            {
                EntitySet.AddCustomProperty(FormCodeInput.CustomProperty, true);
                PopulateEntitiesGrid();
            }
        }

        public void AddNewFunction()
        {
            FormCodeInput.FillData(FormCodeInput.EditTypes.CustomMethod);

            if (FormCodeInput.ShowDialog(this) == DialogResult.OK)
            {
                EntitySet.AddCustomFunction(FormCodeInput.CustomMethod, true);
                PopulateEntitiesGrid();
            }
        }

        public void AddNewAttribute()
        {
            FormAttributeEditor form = new FormAttributeEditor("", "", true);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                CustomAttribute ca = new CustomAttribute(form.RawName, form.RawArgumentString);
                ca.AutoAddToEntities = form.AutoAddToEntities;
                EntitySet.AddCustomAttribute(ca, true);
                PopulateEntitiesGrid();
            }
        }

        void colNamespace_Clicked(object sender, MouseEventArgs e)
        {
            SelectedType = SelectedTypes.Namespace;
            ColumnItem col = (ColumnItem)sender;
            CustomNamespace ns = EntitySet.CustomNamespaces.First(n => n.Value == col.Text);

            if (e.Button == MouseButtons.Left)
            {
                Input.Text = ns.Value;
                Input.AutoAddToEntities = ns.AutoAddToEntities;
                Input.DisplayName = "Namespace:";

                if (Input.ShowDialog(this) == DialogResult.OK)
                {
                    if (col.Text != Input.Text)
                    {
                        ns.Value = Input.Text;
                        ns.AutoAddToEntities = Input.AutoAddToEntities;
                        PopulateEntitiesGrid();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                SelectedNamespace = col.Text;
                contextMenuStrip1.Show(pt);
            }
        }

        void colImplement_Clicked(object sender, MouseEventArgs e)
        {
            SelectedType = SelectedTypes.Implement;
            ColumnItem col = (ColumnItem)sender;
            CustomImplement ci = EntitySet.CustomImplements.First(n => n.Value == col.Text);

            if (e.Button == MouseButtons.Left)
            {
                Input.Text = ci.Value;
                Input.AutoAddToEntities = ci.AutoAddToEntities;
                Input.DisplayName = "Base name:";

                if (Input.ShowDialog(this) == DialogResult.OK)
                {
                    if (col.Text != Input.Text)
                    {
                        ci.Value = Input.Text;
                        ci.AutoAddToEntities = Input.AutoAddToEntities;
                        PopulateEntitiesGrid();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                SelectedImplement = col.Text;
                contextMenuStrip1.Show(pt);
            }
        }

        void colProperty_Clicked(object sender, MouseEventArgs e)
        {
            SelectedType = SelectedTypes.Property;
            ColumnItem col = (ColumnItem)sender;
            CustomProperty cp = EntitySet.CustomProperties.First(n => n.Name == col.Text);

            if (e.Button == MouseButtons.Left)
            {
                FormCodeInput.FillData(cp);

                if (FormCodeInput.ShowDialog(this) == DialogResult.OK)
                    PopulateEntitiesGrid();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                SelectedProperty = col.Text;
                contextMenuStrip1.Show(pt);
            }
        }

        void colMethod_Clicked(object sender, MouseEventArgs e)
        {
            SelectedType = SelectedTypes.Function;
            ColumnItem col = (ColumnItem)sender;
            CustomFunction cm = EntitySet.CustomFunctions.First(n => n.Name == col.Text);

            if (e.Button == MouseButtons.Left)
            {
                FormCodeInput.FillData(cm);

                if (FormCodeInput.ShowDialog(this) == DialogResult.OK)
                    PopulateEntitiesGrid();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                SelectedFunction = col.Text;
                contextMenuStrip1.Show(pt);
            }
        }

        void colAttribute_Clicked(object sender, MouseEventArgs e)
        {
            SelectedType = SelectedTypes.Attribute;
            ColumnItem col = (ColumnItem)sender;
            CustomAttribute ca = EntitySet.CustomAttributes.First(a => a.RawName == col.Text);

            if (e.Button == MouseButtons.Left)
            {
                FormAttributeEditor form = new FormAttributeEditor(ca.RawName, ca.RawArgumentString, ca.AutoAddToEntities);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (col.Text != ca.RawName)
                    {
                        ca.RawName = form.RawName;
                        ca.RawArgumentString = form.RawArgumentString;
                        ca.AutoAddToEntities = form.AutoAddToEntities;
                        PopulateEntitiesGrid();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                SelectedAttribute = col.Text;
                contextMenuStrip1.Show(pt);
            }
        }

        private void AddEntityToEntitiesGrid(Entity entity)
        {
            SlyceTreeGridItem gridItem = new SlyceTreeGridItem();
            gridItem.Tag = entity;
            gridItem.SubItems.Add(new SlyceTreeGridCellItem(entity.Name));

            #region Namespaces
            if (ShowNamespaces)
            {
                bool exists = false;

                foreach (CustomNamespace ns in EntitySet.CustomNamespaces.OrderBy(n => n.Value))
                {
                    exists = true;
                    bool isChecked = ns.Entities.Contains(entity);
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(isChecked));
                }
                if (!exists)
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(null));
            }
            #endregion

            #region Attributes
            if (ShowAttributes)
            {
                bool exists = false;

                foreach (CustomAttribute ca in EntitySet.CustomAttributes.OrderBy(n => n.RawName))
                {
                    exists = true;
                    bool isChecked = ca.Entities.Contains(entity);
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(isChecked));
                }
                if (!exists)
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(null));
            }
            #endregion

            #region Implements
            if (ShowImplements)
            {
                bool exists = false;

                foreach (CustomImplement ci in EntitySet.CustomImplements.OrderBy(n => n.Value))
                {
                    exists = true;
                    bool isChecked = ci.Entities.Contains(entity);
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(isChecked));
                }
                if (!exists)
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(null));
            }
            #endregion

            #region Properties
            if (ShowProperties)
            {
                bool exists = false;

                foreach (CustomProperty cp in EntitySet.CustomProperties.OrderBy(n => n.Name))
                {
                    exists = true;
                    bool isChecked = cp.Entities.Contains(entity);
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(isChecked));
                }
                if (!exists)
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(null));
            }
            #endregion

            #region Functions
            if (ShowFunctions)
            {
                bool exists = false;

                foreach (CustomFunction cm in EntitySet.CustomFunctions.OrderBy(n => n.Name))
                {
                    exists = true;
                    bool isChecked = cm.Entities.Contains(entity);
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(isChecked));
                }
                if (!exists)
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(null));
            }
            #endregion

            slyceGridEntities.Items.Add(gridItem);
        }

        private void mnuDeleteNamespace_Click(object sender, EventArgs e)
        {
            switch (SelectedType)
            {
                case SelectedTypes.Namespace:
                    EntitySet.RemoveNamespace(EntitySet.CustomNamespaces.First(n => n.Value == SelectedNamespace));
                    break;
                case SelectedTypes.Implement:
                    EntitySet.RemoveCustomImplement(EntitySet.CustomImplements.First(n => n.Value == SelectedImplement));
                    break;
                case SelectedTypes.Attribute:
                    EntitySet.RemoveCustomAttribute(EntitySet.CustomAttributes.First(n => n.RawName == SelectedAttribute));
                    break;
                case SelectedTypes.Property:
                    EntitySet.RemoveCustomProperty(EntitySet.CustomProperties.First(n => n.Name == SelectedProperty));
                    break;
                case SelectedTypes.Function:
                    EntitySet.RemoveCustomMethod(EntitySet.CustomFunctions.First(n => n.Name == SelectedFunction));
                    break;
                default:
                    throw new NotImplementedException("Type not handled yet: " + SelectedType.ToString());
            }
            PopulateEntitiesGrid();
        }

        private void mnuEditNamespace_Click(object sender, EventArgs e)
        {
            switch (SelectedType)
            {
                case SelectedTypes.Namespace:
                    CustomNamespace ns = EntitySet.CustomNamespaces.First(n => n.Value == SelectedNamespace);
                    Input.Text = ns.Value;
                    Input.AutoAddToEntities = ns.AutoAddToEntities;

                    if (Input.ShowDialog(this) == DialogResult.OK)
                    {
                        if (SelectedNamespace != Input.Text)
                        {
                            ns.Value = Input.Text;
                            ns.AutoAddToEntities = Input.AutoAddToEntities;
                            PopulateEntitiesGrid();
                        }
                    }
                    break;
                case SelectedTypes.Implement:
                    CustomImplement ci = EntitySet.CustomImplements.First(n => n.Value == SelectedImplement);
                    Input.Text = ci.Value;
                    Input.AutoAddToEntities = ci.AutoAddToEntities;

                    if (Input.ShowDialog(this) == DialogResult.OK)
                    {
                        if (SelectedImplement != Input.Text)
                        {
                            ci.Value = Input.Text;
                            ci.AutoAddToEntities = Input.AutoAddToEntities;
                            PopulateEntitiesGrid();
                        }
                    }
                    break;
                case SelectedTypes.Attribute:
                    CustomAttribute ca = EntitySet.CustomAttributes.First(n => n.RawName == SelectedAttribute);

                    FormAttributeEditor form = new FormAttributeEditor(ca.RawName, ca.RawArgumentString, ca.AutoAddToEntities);

                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (ca.RawName != form.RawName ||
                            ca.RawArgumentString != form.RawArgumentString ||
                            ca.AutoAddToEntities != form.AutoAddToEntities)
                        {
                            ca.RawName = form.RawName;
                            ca.RawArgumentString = form.RawArgumentString;
                            ca.AutoAddToEntities = form.AutoAddToEntities;
                            PopulateEntitiesGrid();
                        }
                    }
                    break;
                case SelectedTypes.Property:
                    CustomProperty cp = EntitySet.CustomProperties.First(n => n.Name == SelectedProperty);

                    FormCodeInput.FillData(cp);

                    if (FormCodeInput.ShowDialog(this) == DialogResult.OK)
                        PopulateEntitiesGrid();

                    break;
                case SelectedTypes.Function:
                    CustomFunction cm = EntitySet.CustomFunctions.First(n => n.Name == SelectedFunction);

                    FormCodeInput.FillData(cm);

                    if (FormCodeInput.ShowDialog(this) == DialogResult.OK)
                        PopulateEntitiesGrid();

                    break;
                default:
                    throw new NotImplementedException("Type not handled yet: " + SelectedType.ToString());
            }
        }

        public void Save()
        {
            // We need to change current cell to ensure that CellValueChanged gets fired.
            slyceGridEntities.FinaliseEdits();
        }
    }
}
