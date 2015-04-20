using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench
{
    public partial class FormAction : Form
    {
        private ArchAngel.Interfaces.BaseAction CurrentAction;

        public FormAction(ArchAngel.Interfaces.BaseAction action)
        {
            InitializeComponent();
            ucHeading1.Text = "";

            CurrentAction = action;
            Populate();
        }

        private void Populate()
        {
            Type currentActionType = CurrentAction.GetType();
            object[] customAttributes = currentActionType.GetCustomAttributes(typeof(ArchAngel.Interfaces.ActionAttribute), false);
            ArchAngel.Interfaces.ActionAttribute att = (ArchAngel.Interfaces.ActionAttribute)customAttributes[0];
            System.Reflection.Assembly actionAssembly = System.Reflection.Assembly.GetAssembly(currentActionType);

            Control control = (Control)actionAssembly.CreateInstance(att.EditControlName);
            panel1.Controls.Add(control);

        }

        private void FormAction_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
        }
    }
}