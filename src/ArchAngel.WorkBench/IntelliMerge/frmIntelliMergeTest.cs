using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TypeOfDiff=Slyce.IntelliMerge.TypeOfDiff;

namespace ArchAngel.Workbench.IntelliMerge
{
    public partial class frmIntelliMergeTest : Form
    {
        public frmIntelliMergeTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Slyce.IntelliMerge.SlyceMerge.LineSpan[] lines1;
            Slyce.IntelliMerge.SlyceMerge.LineSpan[] lines2;
            string combined;
            TypeOfDiff typeOfDiff = Slyce.IntelliMerge.SlyceMerge.PerformTwoWayDiff(checkBox1.Checked, textBox1.Text, textBox2.Text, out lines1, out lines2, out combined);
            label1.Text = typeOfDiff.ToString();
            syntaxEditor1.Text = combined;

            for (int i = 0; i < lines1.Length; i++)
            {
                for (int lineCounter = lines1[i].StartLine; lineCounter <= lines1[i].EndLine; lineCounter++)
                {
                    syntaxEditor1.Document.Lines[lineCounter].BackColor = Color.PeachPuff;
                }
            }
            for (int i = 0; i < lines2.Length; i++)
            {
                for (int lineCounter = lines2[i].StartLine; lineCounter <= lines2[i].EndLine; lineCounter++)
                {
                    syntaxEditor1.Document.Lines[lineCounter].BackColor = Color.YellowGreen;
                }
            }
            
            
        }
    }
}