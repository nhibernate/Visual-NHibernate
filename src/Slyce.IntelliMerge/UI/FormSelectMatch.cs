using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ArchAngel.Providers.CodeProvider;
using Slyce.IntelliMerge.Controller;

namespace Slyce.IntelliMerge.UI
{
    /// <summary>
    /// A form that allows the user to graphically match constucts in a diff.
    /// </summary>
    public partial class FormSelectMatch : Form
    {
        private readonly MissingObject missingObjects;
        private readonly List<IBaseConstruct> prevgenOptions = new List<IBaseConstruct>();
        private readonly List<IBaseConstruct> templateOptions = new List<IBaseConstruct>();
        private readonly List<IBaseConstruct> userOptions = new List<IBaseConstruct>();
        private readonly TextFileInformation fileInformation;
        private readonly CodeRootMapNode currentNode;

    	/// <summary>
        /// Constructs the form using the given map info as the source of constructs to match, 
        /// and the given file information as a source of constructs to use as matches
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="node"></param>
        public FormSelectMatch(TextFileInformation fileInfo, CodeRootMapNode node)
        {
            InitializeComponent();
			FormSelectMatchHeading.Text = "";
			ArchAngel.Interfaces.Events.ShadeMainForm();
            fileInformation = fileInfo;
            currentNode = node;

            if(fileInformation.CodeRootMap == null)
            {
                throw new ArgumentException("Cannot match constructs if the diff has not or cannot be performed.");
            }

            if (node.ContainsBaseConstruct == false)
            {
                throw new ArgumentException(
                    "Cannot match constructs when there are no constucts set in the MapInfoType given. " 
                    + "Please set at least one of these constructs before starting the Select Match Form.");
            }

            missingObjects = node.DetermineMissingConstructs();
            FillOptionsForConstructs();
            FillComboBoxes();

			SetDefaultSelectedConstruct();
        }

		public void SetSyntaxLanguage(SyntaxLanguage syntaxLanguage)
		{
			prevgenSyntaxEditor.Document.Language = syntaxLanguage;
			templateSyntaxEditor.Document.Language = syntaxLanguage;
			userSyntaxEditor.Document.Language = syntaxLanguage;
		}

    	private void SetDefaultSelectedConstruct()
    	{
    		if ((missingObjects & MissingObject.User) == 0)
    		{
    			userComboBox.SelectedIndex = 1;
    		}
    		if ((missingObjects & MissingObject.NewGen) == 0)
    		{
    			templateComboBox.SelectedIndex = 1;
    		}
    		if ((missingObjects & MissingObject.PrevGen) == 0)
    		{
    			prevgenComboBox.SelectedIndex = 1;
    		}
    	}

    	private void FillComboBoxes()
        {
            userComboBox.Items.Clear();
            templateComboBox.Items.Clear();
            prevgenComboBox.Items.Clear();

            userComboBox.Items.Add("");
            templateComboBox.Items.Add("");
            prevgenComboBox.Items.Add("");

            foreach(IBaseConstruct bc in userOptions)
            {
                userComboBox.Items.Add(bc.FullyQualifiedDisplayName);
            }

            foreach (IBaseConstruct bc in templateOptions)
            {
				templateComboBox.Items.Add(bc.FullyQualifiedDisplayName);
            }

            foreach (IBaseConstruct bc in prevgenOptions)
            {
				prevgenComboBox.Items.Add(bc.FullyQualifiedDisplayName);
            }
        }

        private void SetDefaultChoices()
        {
            if ((missingObjects & MissingObject.User) == 0)
            {
                userOptions.Add(currentNode.UserObj);
            }
            if ((missingObjects & MissingObject.NewGen) == 0)
            {
                templateOptions.Add(currentNode.NewGenObj);
            }
            if ((missingObjects & MissingObject.PrevGen) == 0)
            {
                prevgenOptions.Add(currentNode.PrevGenObj);
            }
        }

        private void FillOptionsForConstructs()
        {
            IBaseConstruct construct = currentNode.GetFirstValidBaseConstruct();
            if(construct == null && missingObjects != MissingObject.All)
            {
                throw new InvalidOperationException(
                    "The MapInfoType we are trying to match on is invalid - " 
                    + "it has at least one object mapped but none of them is a BaseConstruct. This is an invalid state.");
            }

            userOptions.Clear();
            templateOptions.Clear();
            prevgenOptions.Clear();

			SetDefaultChoices();

            //if((missingObjects & MissingObject.User) != 0)
            {
                userOptions.AddRange(currentNode.GetSiblingsOfSameType(Controller.Version.User));       
            }
            //if ((missingObjects & MissingObject.NewGen) != 0)
            {
                templateOptions.AddRange(currentNode.GetSiblingsOfSameType(Controller.Version.NewGen));
            }
            //if ((missingObjects & MissingObject.PrevGen) != 0)
            {
                prevgenOptions.AddRange(currentNode.GetSiblingsOfSameType(Controller.Version.PrevGen));
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if(box == null)
                return;
            
            List<IBaseConstruct> constructs;
            SyntaxEditor editor;

            if(sender == userComboBox)
            {
                constructs = userOptions;
                editor = userSyntaxEditor;
            }
            else if(sender == templateComboBox)
            {
                constructs = templateOptions;
                editor = templateSyntaxEditor;
            }
            else
            {
                constructs = prevgenOptions;
                editor = prevgenSyntaxEditor;
            }

            if (box.SelectedIndex == 0)
                editor.Text = "";
            else
            {
                IBaseConstruct construct = constructs[box.SelectedIndex - 1];
                editor.Text = construct.GetFullText().TrimStart();
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            IBaseConstruct userObj    = GetSelectedBaseConstruct(userComboBox, userOptions);
            IBaseConstruct newGenObj  = GetSelectedBaseConstruct(templateComboBox, templateOptions);
            IBaseConstruct prevGenObj = GetSelectedBaseConstruct(prevgenComboBox, prevgenOptions);

        	CodeRootMapNode node = currentNode.ParentTree.GetExactNode(userObj);
			bool userWarned = false;

			if (node != null && node != currentNode && (node.NewGenObj != null || node.PrevGenObj != null))
			{
				if (WarnUser() == false) return;
				userWarned = true;
			}

			node = currentNode.ParentTree.GetExactNode(newGenObj);
			if (node != null && node != currentNode && (node.UserObj != null || node.PrevGenObj != null))
			{
				if (userWarned == false && WarnUser() == false) return;
				userWarned = true;
			}
			
			node = currentNode.ParentTree.GetExactNode(prevGenObj);
			if (node != null && node != currentNode && (node.NewGenObj != null || node.UserObj != null))
				if (userWarned == false && WarnUser() == false) return;

            currentNode.ParentTree.MatchConstructs(currentNode.ParentNode, userObj, newGenObj, prevGenObj); 

            Close();
        }

    	private bool WarnUser()
    	{
    		DialogResult result = MessageBox.Show(this, "You are about to reassign a previously matched item." +
				" This may result in other items being marked with changes, although this is probably what you want." +
				"If you want to undo this change, simply match the item back up to its original matches.", "Reassign items?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

    		return result == DialogResult.Yes;
    	}

    	private static IBaseConstruct GetSelectedBaseConstruct(ListControl comboBox, IList<IBaseConstruct> options)
        {
            return comboBox.SelectedIndex > 0 ? options[comboBox.SelectedIndex - 1] : null;
        }

		private void FormSelectMatch_FormClosing(object sender, FormClosingEventArgs e)
		{
			ArchAngel.Interfaces.Events.UnShadeMainForm();
		}
    }
}
