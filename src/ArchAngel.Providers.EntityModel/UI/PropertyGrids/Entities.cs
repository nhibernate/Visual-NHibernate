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

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
    public partial class Entities : UserControl
    {
        private EntitySet EntitySet;
        private Color[] HighlightYellowColors = new Color[] { Color.FromArgb(172, 255, 227, 65), Color.FromArgb(255, 227, 65), Color.FromArgb(249, 211, 17) };

        public Entities()
        {
            InitializeComponent();

            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);

            labelNamespaces.BackColor =
                labelAttributes.BackColor =
                labelInterfaces.BackColor =
                labelFunctions.BackColor =
                labelProperties.BackColor =
                Color.Transparent;

            labelNamespaces.BackgroundStyle.BackColor =
                labelAttributes.BackgroundStyle.BackColor =
                labelInterfaces.BackgroundStyle.BackColor =
                labelFunctions.BackgroundStyle.BackColor =
                labelProperties.BackgroundStyle.BackColor =
                Color.FromArgb(110, 110, 110);

            labelNamespaces.BackgroundStyle.BackColor2 =
                labelAttributes.BackgroundStyle.BackColor2 =
                labelInterfaces.BackgroundStyle.BackColor2 =
                labelFunctions.BackgroundStyle.BackColor2 =
                labelProperties.BackgroundStyle.BackColor2 =
                Color.FromArgb(40, 40, 40);

            labelNamespaces.BackgroundStyle.BackColorGradientAngle =
                labelAttributes.BackgroundStyle.BackColorGradientAngle =
                labelInterfaces.BackgroundStyle.BackColorGradientAngle =
                labelFunctions.BackgroundStyle.BackColorGradientAngle =
                labelProperties.BackgroundStyle.BackColorGradientAngle =
                90;

            highlighter1.SetHighlightColor(labelAttributes, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
            highlighter1.SetHighlightColor(labelInterfaces, DevComponents.DotNetBar.Validator.eHighlightColor.Custom);
            highlighter1.CustomHighlightColors = HighlightYellowColors;
            highlighter1.SetHighlightColor(labelFunctions, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
            highlighter1.SetHighlightColor(labelNamespaces, DevComponents.DotNetBar.Validator.eHighlightColor.Green);
            highlighter1.SetHighlightColor(labelProperties, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
        }

        public void FillData(EntitySet entitySet)
        {
            EntitySet = entitySet;
            Populate();
        }

        private void Populate()
        {
            namespaceEditor1.FillData(EntitySet);
        }

        private void labelNamespaces_Click(object sender, EventArgs e)
        {
            namespaceEditor1.ShowNamespaces = highlighter1.GetHighlightColor(labelNamespaces) == DevComponents.DotNetBar.Validator.eHighlightColor.None;

            if (namespaceEditor1.ShowNamespaces)
                highlighter1.SetHighlightColor(labelNamespaces, DevComponents.DotNetBar.Validator.eHighlightColor.Green);
            else
                highlighter1.SetHighlightColor(labelNamespaces, DevComponents.DotNetBar.Validator.eHighlightColor.None);

            Populate();
        }

        private void labelAttributes_Click(object sender, EventArgs e)
        {
            namespaceEditor1.ShowAttributes = highlighter1.GetHighlightColor(labelAttributes) == DevComponents.DotNetBar.Validator.eHighlightColor.None;

            if (namespaceEditor1.ShowAttributes)
                highlighter1.SetHighlightColor(labelAttributes, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
            else
                highlighter1.SetHighlightColor(labelAttributes, DevComponents.DotNetBar.Validator.eHighlightColor.None);

            Populate();
        }

        private void labelMethods_Click(object sender, EventArgs e)
        {
            namespaceEditor1.ShowFunctions = highlighter1.GetHighlightColor(labelFunctions) == DevComponents.DotNetBar.Validator.eHighlightColor.None;

            if (namespaceEditor1.ShowFunctions)
                highlighter1.SetHighlightColor(labelFunctions, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
            else
                highlighter1.SetHighlightColor(labelFunctions, DevComponents.DotNetBar.Validator.eHighlightColor.None);

            Populate();
        }

        private void labelProperties_Click(object sender, EventArgs e)
        {
            namespaceEditor1.ShowProperties = highlighter1.GetHighlightColor(labelProperties) == DevComponents.DotNetBar.Validator.eHighlightColor.None;

            if (namespaceEditor1.ShowProperties)
                highlighter1.SetHighlightColor(labelProperties, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            else
                highlighter1.SetHighlightColor(labelProperties, DevComponents.DotNetBar.Validator.eHighlightColor.None);

            Populate();
        }

        private void labelInterfaces_Click(object sender, EventArgs e)
        {
            namespaceEditor1.ShowImplements = highlighter1.GetHighlightColor(labelInterfaces) == DevComponents.DotNetBar.Validator.eHighlightColor.None;

            if (namespaceEditor1.ShowImplements)
                highlighter1.SetHighlightColor(labelInterfaces, DevComponents.DotNetBar.Validator.eHighlightColor.Custom);
            else
                highlighter1.SetHighlightColor(labelInterfaces, DevComponents.DotNetBar.Validator.eHighlightColor.None);

            Populate();
        }

        private void buttonAddNamespace_Click(object sender, EventArgs e)
        {
            namespaceEditor1.AddNewNamespace();
        }

        private void buttonAddAttribute_Click(object sender, EventArgs e)
        {
            namespaceEditor1.AddNewAttribute();
        }

        private void buttonAddBaseName_Click(object sender, EventArgs e)
        {
            namespaceEditor1.AddNewBaseName();
        }

        private void buttonAddFunction_Click(object sender, EventArgs e)
        {
            namespaceEditor1.AddNewFunction();
        }

        private void buttonAddProperty_Click(object sender, EventArgs e)
        {
            namespaceEditor1.AddNewProperty();
        }

        public void Save()
        {
            namespaceEditor1.Save();
        }
    }
}
