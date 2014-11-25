using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Slyce.Common;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    public partial class FormCodeInput : Form
    {
        public enum EditTypes
        {
            CustomProperty,
            CustomMethod
        }
        public CustomProperty CustomProperty;
        public CustomFunction CustomMethod;
        private EditTypes EditType = EditTypes.CustomProperty;

        public FormCodeInput()
        {
            InitializeComponent();

            SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditor1, TemplateContentLanguage.CSharp, SyntaxEditorHelper.ScriptLanguageTypes.CSharp);
            ActiproSoftware.SyntaxEditor.KeyPressTrigger t = new ActiproSoftware.SyntaxEditor.KeyPressTrigger("MemberListTrigger2", true, '#');
            t.ValidLexicalStates.Add(syntaxEditor1.Document.Language.DefaultLexicalState);
            syntaxEditor1.Document.Language.Triggers.Add(t);
        }

        public void FillData(CustomProperty customProperty)
        {
            this.DialogResult = DialogResult.Cancel;
            EditType = EditTypes.CustomProperty;
            CustomProperty = customProperty;

            if (CustomProperty != null)
            {
                checkBoxAutoAddToEntities.Checked = customProperty.AutoAddToEntities;
                syntaxEditor1.Text = customProperty.UserString;
            }
            else
                throw new Exception("customProperty cannot be null.");
        }

        public void FillData(EditTypes editType)
        {
            this.DialogResult = DialogResult.Cancel;
            CustomProperty = null;
            CustomMethod = null;
            EditType = editType;
            checkBoxAutoAddToEntities.Checked = true;

            switch (EditType)
            {
                case EditTypes.CustomMethod:
                    syntaxEditor1.Text = "public void NewFunction()\n{\n}";
                    break;
                case EditTypes.CustomProperty:
                    syntaxEditor1.Text = "public string NewProperty { get; set; }";
                    break;
                default:
                    throw new NotImplementedException("Not handled yet: " + EditType.ToString());
            }
        }

        public void FillData(CustomFunction customMethod)
        {
            this.DialogResult = DialogResult.Cancel;
            EditType = EditTypes.CustomMethod;
            CustomMethod = customMethod;

            if (CustomMethod != null)
            {
                checkBoxAutoAddToEntities.Checked = customMethod.AutoAddToEntities;
                syntaxEditor1.Text = customMethod.UserString;
            }
            else
                throw new Exception("customMethod cannot be null.");
        }

        public bool AutoAddToEntities
        {
            get { return checkBoxAutoAddToEntities.Checked; }
            set { checkBoxAutoAddToEntities.Checked = value; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string name;

            switch (EditType)
            {
                case EditTypes.CustomMethod:
                    ArchAngel.Providers.CodeProvider.DotNet.Function codeMethod;

                    try
                    {
                        if (!CustomFunction.CodeMethodIsValid(syntaxEditor1.Text, out name, out codeMethod))
                        {
                            MessageBox.Show(this, "The code is not valid. It has an error.", "Invalid code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "The code is not valid. It has an error.", "Invalid code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (CustomMethod == null)
                        CustomMethod = new CustomFunction(name, syntaxEditor1.Text);
                    else
                    {
                        CustomMethod.Name = name;
                        CustomMethod.UserString = syntaxEditor1.Text;
                    }
                    CustomMethod.CodeFunction = codeMethod;
                    CustomMethod.AutoAddToEntities = checkBoxAutoAddToEntities.Checked;
                    break;
                case EditTypes.CustomProperty:

                    ArchAngel.Providers.CodeProvider.DotNet.Property codeProperty;

                    try
                    {
                        if (!CustomProperty.CodePropertyIsValid(syntaxEditor1.Text, out name, out codeProperty))
                        {
                            MessageBox.Show(this, "The code is not valid. It has an error.", "Invalid code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "The code is not valid. It has an error.", "Invalid code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (CustomProperty == null)
                        CustomProperty = new CustomProperty(name, syntaxEditor1.Text);
                    else
                    {
                        CustomProperty.Name = name;
                        CustomProperty.UserString = syntaxEditor1.Text;
                    }
                    CustomProperty.CodeProperty = codeProperty;
                    CustomProperty.AutoAddToEntities = checkBoxAutoAddToEntities.Checked;
                    break;
                default:
                    throw new NotImplementedException("EditType not handled yet in buttonOk_Click(): " + EditType.ToString());
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddItemToMemberList(string displayName, string tooltipText, ActiproSoftware.Products.SyntaxEditor.IconResource icon)
        {
            bool alreadyExists = false;

            for (int i = 0; i < syntaxEditor1.IntelliPrompt.MemberList.Count; i++)
            {
                if (syntaxEditor1.IntelliPrompt.MemberList[i].Text == displayName &&
                    syntaxEditor1.IntelliPrompt.MemberList[i].ImageIndex == (int)icon)
                {
                    alreadyExists = true;
                    break;
                }
            }
            if (!alreadyExists)
            {
                syntaxEditor1.IntelliPrompt.MemberList.Add(new ActiproSoftware.SyntaxEditor.IntelliPromptMemberListItem(displayName, (int)icon, tooltipText));
            }
        }

        private void syntaxEditor1_TriggerActivated(object sender, ActiproSoftware.SyntaxEditor.TriggerEventArgs e)
        {
            syntaxEditor1.IntelliPrompt.MemberList.Clear();
            int originalCaretPos = syntaxEditor1.SelectedView.Selection.StartOffset - 1;
            string triggerChar = syntaxEditor1.Document.Text.Substring(originalCaretPos, 1);

            if (triggerChar == ".")
            {
                string word = syntaxEditor1.Document.GetWordText(originalCaretPos - 1);

                if (word == "entity" &&
                    syntaxEditor1.Document.Text.Substring(originalCaretPos - 1 - "entity".Length, 1) == "#")
                {
                    AddItemToMemberList("Name#", "The name property of the entity.", ActiproSoftware.Products.SyntaxEditor.IconResource.PublicClass);
                    //AddItemToMemberList("Type#", "The data-type of the Entity.", ActiproSoftware.Products.SyntaxEditor.IconResource.PublicClass);
                }
            }
            else if (triggerChar == "#")
                AddItemToMemberList("entity", "The entity that is being processed.", ActiproSoftware.Products.SyntaxEditor.IconResource.PublicClass);

            if (syntaxEditor1.IntelliPrompt.MemberList.Count > 0)
                syntaxEditor1.IntelliPrompt.MemberList.Show();
        }
    }
}
