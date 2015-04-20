using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Algorithm.Diff;
using System.Collections;
using Slyce.IntelliMerge;
using Slyce;
using DevExpress.XtraTreeList.Nodes;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.IntelliMerge;
using DiffCodeClass=Slyce.IntelliMerge.Controller.DiffItems.CodeFiles.DiffCodeClass;
using DiffFile=Slyce.IntelliMerge.Controller.DiffItems.DiffFile;

namespace ArchAngel.Workbench
{
    public partial class FormMergeEditor : Form
    {
        public enum AutoMergeTypes
        {
            None,
            KeepUserChanges,
            KeepTemplateChanges
        }
        public enum OriginTypes
        {
            MapInfo,
            DiffFile,
            UsingStatement
        }
        private SlyceMerge.LineSpan OrigianlLineSpan = null;
        public DiffFile CurrentDiffFile;
        private TypeOfDiff CurrentDiffType;
        private ArchAngel.Providers.CodeProvider.CSharp.MapInfoType CurrentMapInfo;
        private bool BusyPopulatingEditor = false;
        private int CurrentLine = 0;
        private bool CancelClicked = false;
        private OriginTypes OriginType;
        private string MarginTextUser = "User";
        private bool DisplayingTextMessage = false;

        public FormMergeEditor()
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            ucHeading1.Text = "";
            Controller.Instance.ShadeMainForm();
        }

        private TypeOfDiff GetCurrentDiffStatus()
        {
            if (ConflictsStillExist())
            {
                return TypeOfDiff.Conflict;
            }
            else
            {
                return TypeOfDiff.ExactCopy;
            }
        }

        public TypeOfDiff DisplayNodeFiles(TreeListNode node, DiffFile currentDiffFile)
        {
            OriginType = OriginTypes.DiffFile;
            return DisplayNodeFiles(node, currentDiffFile, AutoMergeTypes.None, "", "", "", "", currentDiffFile.DiffType, OriginType);
        }

        public TypeOfDiff DisplayNodeFiles(TreeListNode node, DiffFile currentDiffFile, ArchAngel.Providers.CodeProvider.CSharp.MapInfoType mapInfo, AutoMergeTypes autoMergeType, OriginTypes originType)
        {
            CurrentDiffFile = currentDiffFile;
            CurrentMapInfo = mapInfo;
            OriginType = originType;
            Type type = null;

            if (mapInfo.UserObject != null)
            {
                type = mapInfo.UserObject.GetType();
            }
            else if (mapInfo.TemplateObject != null)
            {
                type = mapInfo.TemplateObject.GetType();
            }
            else if (mapInfo.PrevGenObject != null)
            {
                type = mapInfo.PrevGenObject.GetType();
            }
            string userText = null;
            string templateText = null;
            string prevGenText = null;
            string mergedText = null;

            if (type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Class) ||
                type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Interface) ||
                type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Namespace) ||
                type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Struct))
            {
                userText = mapInfo.UserObject == null ? null : ((ArchAngel.Providers.CodeProvider.CSharp.Class)mapInfo.UserObject).ToString(false);
                templateText = mapInfo.TemplateObject == null ? null : ((ArchAngel.Providers.CodeProvider.CSharp.Class)mapInfo.TemplateObject).ToString(false);
                prevGenText = mapInfo.PrevGenObject == null ? null : ((ArchAngel.Providers.CodeProvider.CSharp.Class)mapInfo.PrevGenObject).ToString(false);
            }
            else
            {
                userText = mapInfo.UserObject == null ? null : mapInfo.UserObject.ToString();
                templateText = mapInfo.TemplateObject == null ? null : mapInfo.TemplateObject.ToString();
                prevGenText = mapInfo.PrevGenObject == null ? null : mapInfo.PrevGenObject.ToString();
            }
            if (mapInfo.Parent != null)
            {
                mergedText = mapInfo.Parent.ToString();
            }
            else if (mapInfo.MergedObject != null)
            {
                mergedText = mapInfo.MergedObject.ToString();
            }
            return DisplayNodeFiles(node, CurrentDiffFile, autoMergeType, userText, templateText, prevGenText, mergedText, mapInfo.DiffType, OriginType);
        }

        public TypeOfDiff DisplayNodeFiles(TreeListNode node, DiffFile currentDiffFile, AutoMergeTypes autoMergeType, string userText, string templateText, string parentText, string mergedText, TypeOfDiff currentDiffType, OriginTypes originType)
        {
            OriginType = originType;
            CurrentDiffFile = currentDiffFile;
            CurrentDiffType = currentDiffType;
            BusyPopulatingEditor = true;

            if (node.Tag.GetType() == typeof(DiffFile) &&
                !((DiffFile)node.Tag).IsText)
            {
                ucBinaryFileViewer1.RootParent = SlyceMergeWorker.PreviousGenerationFolder;
                ucBinaryFileViewer1.RootTemplate = Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator);
                ucBinaryFileViewer1.RootUser = Controller.Instance.ProjectSettings.ProjectPath;
                ucBinaryFileViewer1.DiffFile = (DiffFile)node.Tag;
                // Binary file
                ucBinaryFileViewer1.Visible = true;
                ucBinaryFileViewer1.Left = this.Left;
                ucBinaryFileViewer1.Width = this.ClientSize.Width;
                ucBinaryFileViewer1.Top = this.Top;
                ucBinaryFileViewer1.Height = this.ClientSize.Height;
                SetNavigationButtons();
                ShowBinaryFiles(node);
                return GetCurrentDiffStatus();
            }
            else
            {
                ucBinaryFileViewer1.Visible = false;
            }
            OrigianlLineSpan = null;
            bool filesTheSame = true;
            syntaxEditor1.ResetText();

            // Perform a 3-way diff on the file
            string parentFile = "";
            string userFile = "";
            string templateFile = "";
            string mergedFile = "";
            string fileBodyParent = null;
            string fileBodyUser = null;
            string fileBodyGenerated = null;

            if (OriginType == OriginTypes.DiffFile)
            {
                parentFile = Path.Combine(SlyceMergeWorker.PreviousGenerationFolder, ((DiffFile)node.Tag).RelativePath);
                userFile = Path.Combine(Controller.Instance.ProjectSettings.ProjectPath, ((DiffFile)node.Tag).RelativePath);
                templateFile = Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), ((DiffFile)node.Tag).RelativePath);
                mergedFile = Path.Combine(SlyceMergeWorker.StagingFolder, ((DiffFile)node.Tag).RelativePath + ".merged");

                fileBodyParent = File.Exists(parentFile) ? IOUtility.GetTextFileBody(parentFile) : "";
                fileBodyUser = File.Exists(userFile) ? IOUtility.GetTextFileBody(userFile) : "";
                fileBodyGenerated = File.Exists(templateFile) ? IOUtility.GetTextFileBody(templateFile) : "";
            }
            else
            {
                fileBodyParent = parentText;
                fileBodyUser = userText;
                fileBodyGenerated = templateText;
            }
            fileBodyParent = Slyce.Common.Utility.StandardizeLineBreaks(fileBodyParent, Slyce.Common.Utility.LineBreaks.Unix);
            fileBodyUser = Slyce.Common.Utility.StandardizeLineBreaks(fileBodyUser, Slyce.Common.Utility.LineBreaks.Unix);
            fileBodyGenerated = Slyce.Common.Utility.StandardizeLineBreaks(fileBodyGenerated, Slyce.Common.Utility.LineBreaks.Unix);
            DetermineFileType(node);

            if (CurrentDiffType == TypeOfDiff.UserChangeOnly)
            {
                MarginTextUser = "User";
                Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInSingleEditor(syntaxEditor1, ref fileBodyUser, ref fileBodyParent, true);
                syntaxEditor1.Document.ReadOnly = true;
                toolStripButtonAcceptTemplateChanges.Enabled = false;
                toolStripButtonAcceptUserChanges.Enabled = false;
                this.Text = "View only - No conflicts";
                buttonClose.Text = "&Close";
                buttonSave.Visible = false;
                SetNavigationButtons();
                this.ShowDialog(Controller.Instance.MainForm);
                return CurrentDiffType;
            }
            else if (CurrentDiffType == TypeOfDiff.TemplateChangeOnly)
            {
                MarginTextUser = "Template";

                if (fileBodyGenerated == null)
                {
                    // This is marked as a TemplateChange because the template DELETED the corresponding entity
                    DisplayingTextMessage = true;
                    toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.PlainText);
                    syntaxEditor1.WordWrap = ActiproSoftware.SyntaxEditor.WordWrapType.Word;
                    syntaxEditor1.Text = "This is a TemplateChange because it looks like the Template has not created the corresponding entity. In effect, the template has DELETED the corresponding entity. Alternatively, the corresponding entity has been RENAMED, or it's namespace has been renamed. If this is the case, click 'Select Match' on the popup menu to locate a renamed version of this entity if the template has renamed it (or its namespace).";
                }
                else
                {
                    Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInSingleEditor(syntaxEditor1, ref fileBodyGenerated, ref fileBodyParent, true);
                }
                syntaxEditor1.Document.ReadOnly = true;
                toolStripButtonAcceptTemplateChanges.Enabled = false;
                toolStripButtonAcceptUserChanges.Enabled = false;
                this.Text = "View only - No conflicts";
                buttonClose.Text = "&Close";
                buttonSave.Visible = false;
                SetNavigationButtons();
                this.ShowDialog(Controller.Instance.MainForm);
                return CurrentDiffType;
            }
            else if (CurrentDiffType == TypeOfDiff.UserAndTemplateChange)
            {
                if (OriginType == OriginTypes.DiffFile)
                {
                    string mergedFileName = mergedFile.Replace(".merged", ".copy");

                    if (!File.Exists(mergedFileName))
                    {
                        throw new Exception("Merged file with user and template changes not found.");
                    }
                    mergedText = IOUtility.GetTextFileBody(mergedFileName);
                    mergedText = Slyce.Common.Utility.StandardizeLineBreaks(mergedText, Slyce.Common.Utility.LineBreaks.Unix);
                }
                SlyceMerge.LineSpan[] userLines;
                SlyceMerge.LineSpan[] templateLines;
                SlyceMerge.LineSpan[] rightLinesUser;
                SlyceMerge.LineSpan[] rightLinesTemplate;
                string combinedText;
                SlyceMerge.PerformTwoWayDiff(false, mergedText, fileBodyUser, out userLines, out rightLinesUser, out combinedText);
                SlyceMerge.PerformTwoWayDiff(false, mergedText, fileBodyGenerated, out templateLines, out rightLinesTemplate, out combinedText);
                syntaxEditor1.Text = mergedText;

                for (int i = 0; i < userLines.Length; i++)
                {
                    #region Get offset
                    int offsetUser = 0;

                    for (int counterRightLineUser = 0; counterRightLineUser < rightLinesUser.Length; counterRightLineUser++)
                    {
                        if (rightLinesUser[counterRightLineUser].StartLine < userLines[i].StartLine)
                        {
                            offsetUser += (rightLinesUser[counterRightLineUser].EndLine - rightLinesUser[counterRightLineUser].StartLine + 1);
                        }
                        else
                        {
                            break;
                        }

                    }
                    #endregion

                    for (int lineCounter = userLines[i].StartLine; lineCounter <= userLines[i].EndLine; lineCounter++)
                    {
                        syntaxEditor1.Document.Lines[lineCounter - offsetUser].BackColor = Slyce.IntelliMerge.UI.Utility.ColourNewGen;
                    }
                }
                for (int i = 0; i < templateLines.Length; i++)
                {
                    #region Get offset
                    int offsetTemplate = 0;

                    for (int counterRightLineTemplate = 0; counterRightLineTemplate < rightLinesTemplate.Length; counterRightLineTemplate++)
                    {
                        if (rightLinesTemplate[counterRightLineTemplate].StartLine < templateLines[i].StartLine)
                        {
                            offsetTemplate += (rightLinesTemplate[counterRightLineTemplate].EndLine - rightLinesTemplate[counterRightLineTemplate].StartLine + 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    for (int lineCounter = templateLines[i].StartLine; lineCounter <= templateLines[i].EndLine; lineCounter++)
                    {
                        syntaxEditor1.Document.Lines[lineCounter - offsetTemplate].BackColor = Slyce.IntelliMerge.UI.Utility.ColourUser;
                    }
                }
                syntaxEditor1.Document.ReadOnly = true;
                toolStripButtonAcceptTemplateChanges.Enabled = false;
                toolStripButtonAcceptUserChanges.Enabled = false;
                this.Text = "View only - No conflicts";
                buttonClose.Text = "&Close";
                buttonSave.Visible = false;
                SetNavigationButtons();
                this.ShowDialog(Controller.Instance.MainForm);
                return currentDiffFile.DiffType;
            }
            syntaxEditor1.SuspendPainting();

            if (File.Exists(mergedFile))
            {
                using (TextReader tr = new StreamReader(mergedFile))
                {
                    string line = "";
                    int lineCounter = 0;
                    int pipeIndex = 0;
                    int backColor = 0;

                    while ((line = tr.ReadLine()) != null)
                    {
                        pipeIndex = line.IndexOf("|");
                        backColor = int.Parse(line.Substring(0, pipeIndex));
                        syntaxEditor1.Document.AppendText(line.Substring(pipeIndex + 1) + Environment.NewLine);
                        syntaxEditor1.Document.Lines[lineCounter].BackColor = Color.FromArgb(backColor);
                        lineCounter++;
                    }
                }
            }
            else // A merged file hasn't been created yet, so conflicts still exist
            {
                if (fileBodyParent != null &&
                          fileBodyUser != null &&
                          fileBodyGenerated != null)
                {
                    // Perform 3-way diff
                    string output;
                    SlyceMerge slyceMerge = SlyceMerge.Perform3wayDiff(fileBodyUser, fileBodyParent, fileBodyGenerated, out output);
                    int lineCounter = 0;
                    StringBuilder sb = new StringBuilder(Math.Max(fileBodyUser.Length, Math.Max(fileBodyParent.Length, fileBodyGenerated.Length)) + 1000);
                    System.Collections.ArrayList colouredLines = new System.Collections.ArrayList();

                    foreach (SlyceMerge.LineText line in slyceMerge.Lines)
                    {
                        int charPos = 0;
                        int numLineBreaks = 0;
                        charPos = line.Text.IndexOf("\r", 0);

                        if (charPos < 0) { numLineBreaks++; }

                        while (charPos >= 0)
                        {
                            numLineBreaks++;
                            charPos = line.Text.IndexOf("\r", charPos + 1);
                        }
                        sb.AppendLine(line.Text.Replace("\r", ""));
                        colouredLines.Add(new SlyceMerge.LineSpan(lineCounter, lineCounter + numLineBreaks, line.Colour));
                        lineCounter += numLineBreaks;
                    }
                    int linesToRemove = 0;
                    string text = sb.ToString();
                    string lastChar = text.Substring(text.Length - linesToRemove - 1);

                    while (lastChar.Length == 0 ||
                        lastChar == "\n" ||
                        lastChar == "\r" &&
                        text.Length >= linesToRemove + 1)
                    {
                        linesToRemove++;
                        lastChar = text.Substring(text.Length - linesToRemove - 1, 1);
                    }
                    syntaxEditor1.Document.Text = text.Substring(0, text.Length - linesToRemove);

                    for (int i = 0; i < colouredLines.Count; i++)
                    {
                        SlyceMerge.LineSpan ls = (SlyceMerge.LineSpan)colouredLines[i];

                        if (ls.OriginalColor != Color.White) { filesTheSame = false; }

                        for (int x = ls.StartLine; x <= ls.EndLine; x++)
                        {
                            if (x >= syntaxEditor1.Document.Lines.Count)
                            {
                                break;
                            }
                            syntaxEditor1.Document.Lines[x].BackColor = ls.OriginalColor;
                        }
                    }
                    if (filesTheSame)
                    {
                        this.Text = "No Changes";
                    }
                }
                else if (fileBodyParent != null &&
                          fileBodyUser != null &&
                          fileBodyGenerated == null)
                {
                    // No template file, just use the user file
                    syntaxEditor1.Text = fileBodyUser;
                    this.Text = "No Conflicts";
                }
                else if (fileBodyParent != null &&
         fileBodyUser == null &&
    fileBodyGenerated != null)
                {
                    // No user file, just use the template file
                    Slyce.IntelliMerge.UI.Utility.Perform2WayDiffInSingleEditor(syntaxEditor1, ref fileBodyGenerated, ref fileBodyParent, true);
                }
                else if (fileBodyParent == null &&
                       fileBodyUser != null &&
                  fileBodyGenerated != null)
                {
                    // No parent file, make sure the user merges the template and user files
                    string combinedText;
                    Slyce.IntelliMerge.SlyceMerge.LineSpan[] userLines;
                    Slyce.IntelliMerge.SlyceMerge.LineSpan[] templateLines;
                    SlyceMerge.PerformTwoWayDiff(false, fileBodyUser, fileBodyGenerated, out userLines, out templateLines, out combinedText);
                    Slyce.IntelliMerge.UI.Utility.PopulateSyntaxEditor(syntaxEditor1, combinedText, userLines, templateLines);
                    this.Text = "User changes vs generated";
                }
                else
                {
                    if (fileBodyParent != null)
                    {
                        if (File.Exists(userFile) || File.Exists(templateFile))
                        {
                            throw new Exception("More than one file exists. Shouldn't be here.");
                        }
                        syntaxEditor1.Text = fileBodyParent;
                        this.Text = "Unchanged file";
                    }
                    else if (fileBodyUser != null)
                    {
                        if (File.Exists(parentFile) || File.Exists(templateFile))
                        {
                            throw new Exception("More than one file exists. Shouldn't be here.");
                        }
                        syntaxEditor1.Text = fileBodyUser;
                        this.Text = "User-only file";
                    }
                    else if (fileBodyGenerated != null)
                    {
                        if (File.Exists(parentFile) || File.Exists(userFile))
                        {
                            throw new Exception("More than one file exists. Shouldn't be here.");
                        }
                        syntaxEditor1.Text = fileBodyGenerated;
                        this.Text = "Newly Generated file";
                    }
                }
            }
            syntaxEditor1.Refresh();

            if (syntaxEditor1.Document.Text.Length > 0)
            {
                syntaxEditor1.SelectedView.Selection.StartOffset = 0;
                syntaxEditor1.SelectedView.Selection.EndOffset = 0;
            }
            syntaxEditor1.Document.Modified = false;
            syntaxEditor1.ResumePainting();
            BusyPopulatingEditor = false;

            switch (autoMergeType)
            {
                case AutoMergeTypes.None:
                    GotoNextConflictLine(0);
                    this.ShowDialog(Controller.Instance.MainForm);
                    break;
                case AutoMergeTypes.KeepUserChanges:
                    AcceptAllUserChanges();

                    if (!SaveCurrentFile(false))
                    {
                        MessageBox.Show("There was a problem processing this file automatically. Please resolve these changes manually.", "Auto Processing Problem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        GotoNextConflictLine(0);
                        this.ShowDialog(Controller.Instance.MainForm);
                    }
                    break;
                case AutoMergeTypes.KeepTemplateChanges:
                    AcceptAllTemplateChanges();

                    if (!SaveCurrentFile(false))
                    {
                        MessageBox.Show("There was a problem processing this file automatically. Please resolve these changes manually.", "Auto Processing Problem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        GotoNextConflictLine(0);
                        this.ShowDialog(Controller.Instance.MainForm);
                    }
                    break;
            }
            SetNavigationButtons();
            if (CancelClicked) { return TypeOfDiff.Conflict; }
            else { return GetCurrentDiffStatus(); }
        }

        private void DetermineFileType(TreeListNode node)
        {
            string extension = "";
            Type type = null;

            if (node.Tag.GetType() == typeof(DiffCodeClass))
            {
                extension = ((DiffCodeClass)node.Tag).Language.ToString();
            }
            else if (node.Tag.GetType() == typeof(ArchAngel.Providers.CodeProvider.CSharp.MapInfoType))
            {
                ArchAngel.Providers.CodeProvider.CSharp.MapInfoType mapInfo = (ArchAngel.Providers.CodeProvider.CSharp.MapInfoType)node.Tag;

                if (mapInfo.UserObject != null)
                {
                    type = mapInfo.UserObject.GetType();

                    if (type.BaseType == typeof(ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct))
                    {
                        extension = ((ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct)mapInfo.UserObject).Language.ToString();
                    }
                    else if (type == typeof(string))
                    {
                        // TODO: language is hard-coded to C# now. Probably 
                        // need to figure a way of defining Language at CodeRoot level.
                        extension = ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct.CodeLanguage.CSharp.ToString();
                    }
                    else
                    {
                        throw new NotImplementedException(string.Format("FormMergeEditor.DetermineFileType doesn't cater for {0} types yet.", type.Name));
                    }
                }
                else if (mapInfo.TemplateObject != null)
                {
                    type = mapInfo.TemplateObject.GetType();

                    if (type.BaseType == typeof(ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct))
                    {
                        extension = ((ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct)mapInfo.TemplateObject).Language.ToString();
                    }
                    else if (type == typeof(string))
                    {
                        // TODO: language is hard-coded to C# now. Probably 
                        // need to figure a way of defining Language at CodeRoot level.
                        extension = ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct.CodeLanguage.CSharp.ToString();
                    }
                    else
                    {
                        throw new NotImplementedException(string.Format("FormMergeEditor.DetermineFileType doesn't cater for {0} types yet.", type.Name));
                    }
                }
                else if (mapInfo.PrevGenObject != null)
                {
                    type = mapInfo.PrevGenObject.GetType();

                    if (type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Function))
                    {
                        extension = ((ArchAngel.Providers.CodeProvider.CSharp.Function)mapInfo.PrevGenObject).Language.ToString();
                    }
                    else if (type == typeof(string))
                    {
                        // TODO: language is hard-coded to C# now. Probably 
                        // need to figure a way of defining Language at CodeRoot level.
                        extension = ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct.CodeLanguage.CSharp.ToString();
                    }
                    else
                    {
                        throw new NotImplementedException(string.Format("FormMergeEditor.DetermineFileType doesn't cater for {0} types yet.", type.Name));
                    }
                }
            }
            else if (node.Tag.GetType() == typeof(DiffFile))
            {
                extension = Path.GetExtension(((DiffFile)node.Tag).Path.ToLower());
            }
            else
            {
                throw new NotImplementedException(node.Tag.GetType().Name.ToString() + " has not been coded for yet.");
            }
            if (string.IsNullOrEmpty(extension))
            {
                throw new NotImplementedException("Correct file extension could not be determined.");
            }
                switch (extension.ToLower())
                {
                    case ".bat":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.BatchFile);
                        break;
                    case ".cs":
                    case "csharp":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.CSharp);
                        break;
                    case ".vb":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.VbNet);
                        break;
                    case ".html":
                    case ".htm":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Html);
                        break;
                    case ".css":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Css);
                        break;
                    case ".ini":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.IniFile);
                        break;
                    case ".jar":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Java);
                        break;
                    case ".js":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.JScript);
                        break;
                    case ".php":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.PHP);
                        break;
                    case ".pl":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Perl);
                        break;
                    case ".py":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Python);
                        break;
                    case ".sql":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Sql);
                        break;
                    case ".vbs":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.VbScript);
                        break;
                    case ".xml":
                    case ".xslt":
                    case ".config":
                    case ".csproj":
                    case ".vbproj":
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.Xml);
                        break;
                    default:
                        toolStripComboBoxFileType.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(Slyce.Common.SyntaxEditorHelper.Languages.PlainText);
                        break;
                }
        }

        private void SetSyntaxLanguage()
        {
            string syntaxLanguage = toolStripComboBoxFileType.Text;

            if (Slyce.Common.Utility.StringsAreEqual(syntaxLanguage, "t-sql", false))
            {
                syntaxLanguage = "sql";
            }
            Slyce.Common.SyntaxEditorHelper.Languages language = Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(syntaxLanguage);
            string normalFilePath = Slyce.Common.SyntaxEditorHelper.GetLanguageFileName(language);
            normalFilePath = Path.Combine(Path.GetTempPath(), normalFilePath);

            if (!File.Exists(normalFilePath))
            {
                MessageBox.Show("Xml language syntax file could not be loaded: " + normalFilePath);
                return;
            }
            syntaxEditor1.Document.LoadLanguageFromXml(normalFilePath, 0);
            syntaxEditor2.Document.LoadLanguageFromXml(normalFilePath, 0);
        }

        private void toolStripComboBoxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSyntaxLanguage();
        }

        private void ShowBinaryFiles(TreeListNode node)
        {
            //MessageBox.Show("Should display binary files here.");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            CancelClicked = true;
            this.Close();
        }

        /// <summary>
        /// TODO: Must call this before
        /// </summary>
        /// <param name="forceSave"></param>
        private bool SaveCurrentFile(bool forceSave)
        {
            if (!forceSave && ConflictsStillExist())
            {
                MessageBox.Show("Can't save. Conflicts still exist.", "Conflicts Still Exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (OriginType == OriginTypes.DiffFile)
            {
                // Save the old files with a new extension
                string parentFile = Path.Combine(SlyceMergeWorker.PreviousGenerationFolder, CurrentDiffFile.RelativePath);
                string userFile = Path.Combine(Controller.Instance.ProjectSettings.ProjectPath, CurrentDiffFile.RelativePath);
                string templateFile = Path.Combine(Controller.Instance.GetTempFilePathForComponent(ComponentKey.WorkbenchFileGenerator), CurrentDiffFile.RelativePath);
                string stagingFile = Path.Combine(SlyceMergeWorker.StagingFolder, CurrentDiffFile.RelativePath);
                DeselectLines();

                if (File.Exists(stagingFile + ".copy"))
                {
                    Slyce.Common.Utility.DeleteFileBrute(stagingFile + ".copy");
                }
                if (File.Exists(stagingFile + ".merged"))
                {
                    Slyce.Common.Utility.DeleteFileBrute(stagingFile + ".merged");
                }
                using (TextWriter tw = new StreamWriter(stagingFile + ".merged"))
                {
                    for (int i = 0; i < syntaxEditor1.Document.Lines.Count; i++)
                    {
                        if (i < syntaxEditor1.Document.Lines.Count - 1 ||
                            syntaxEditor1.Document.Lines[i].Text.Length > 0)
                        {
                            tw.WriteLine(syntaxEditor1.Document.Lines[i].BackColor.ToArgb().ToString() + "|" + syntaxEditor1.Document.Lines[i].Text);
                        }
                    }
                    tw.Close();
                }
                return true;
            }
            else if (CurrentMapInfo.Key == "UsingStatements")
            {
                CurrentMapInfo.MergedObject = syntaxEditor1.Text;
                CurrentDiffFile.CodeRootAll.UsingStatementsTextBlock = syntaxEditor1.Text;
                CurrentMapInfo.DiffType = TypeOfDiff.ExactCopy;
                CurrentDiffFile.DiffType = CurrentDiffFile.CodeRootAll.ResetDiffType();
                return true;
            }
            else // OriginType == OriginTypes.MapInfo
            {
                ArchAngel.Providers.CSharpFormatter csFormatter = new ArchAngel.Providers.CSharpFormatter(0);
                csFormatter.RaiseError += new ArchAngel.Providers.ErrorEventHandler(csFormatter_RaiseError);
                csFormatter.CreateObjectModel = true;
                csFormatter.ObjectStack.Clear();
                ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct clonedParent = (ArchAngel.Providers.CodeProvider.CSharp.BaseConstruct)Slyce.Common.Utility.CloneObject(CurrentMapInfo.Parent.ParentObject);
                csFormatter.ObjectStack.Add(new ArchAngel.Providers.CSharpFormatter.StackObject(clonedParent, 0));

                Type objectType = CurrentMapInfo.Parent.GetType();
                Type type = CurrentMapInfo.Parent.GetType();
                string parserElementName = "";

                if (type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Class) ||
                    type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Struct) ||
                    type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Interface) ||
                    type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Enumeration) ||
                    type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Delegate))
                {
                    parserElementName = "type_declaration";
                }
                else if (type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Constructor) ||
                    type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Attribute) ||
                    type == typeof(ArchAngel.Providers.CodeProvider.CSharp.Constant))
                {
                    parserElementName = "class_member_declaration";
                }
                else
                {
                    throw new NotImplementedException("Type not handled yet: "+ type.Name);
                }
                string formatted = csFormatter.ParseCode(Slyce.Common.Utility.StandardizeLineBreaks(syntaxEditor1.Text, Slyce.Common.Utility.LineBreaks.Windows), parserElementName);
                ArchAngel.Providers.CodeProvider.CSharp.Utility.UpdateProperties(CurrentMapInfo.Parent, csFormatter.ObjectStack[csFormatter.ObjectStack.Count - 1].TheObject);

                // Reset DiffType of all ancestors
                CurrentMapInfo.Parent.DiffType = TypeOfDiff.ExactCopy;
                CurrentDiffFile.DiffType = CurrentDiffFile.CodeRootAll.ResetDiffType();
                return true;
            }
        }

        void csFormatter_RaiseError(string fileName, string procedureName, string description, string originalText, int lineNumber, int startPos, int length)
        {
            MarkErrorWord(syntaxEditor1, lineNumber, startPos, description);
        }

        private void MarkErrorWord(ActiproSoftware.SyntaxEditor.SyntaxEditor editor, int lineNumber, int characterPos, string message)
        {
            ActiproSoftware.SyntaxEditor.DocumentPosition position = new ActiproSoftware.SyntaxEditor.DocumentPosition(lineNumber, characterPos);
            int offset = editor.Document.PositionToOffset(position);
            DynamicToken token = (DynamicToken)editor.Document.Tokens.GetTokenAtOffset(offset);
            ActiproSoftware.SyntaxEditor.SpanIndicator indicator = new ActiproSoftware.SyntaxEditor.WaveLineSpanIndicator("ErrorIndicator", Color.Red);
            indicator.Tag = message;
            ActiproSoftware.SyntaxEditor.SpanIndicatorLayer indicatorLayer = new ActiproSoftware.SyntaxEditor.SpanIndicatorLayer("kk", 1);
            editor.Document.SpanIndicatorLayers.Add(indicatorLayer);
            int startOffset = Math.Min(token.StartOffset, indicatorLayer.Document.Length - 1);
            int length = Math.Max(token.Length, 1);
            indicatorLayer.Add(indicator, startOffset, length);

            syntaxEditor1.Document.Lines[lineNumber].BackColor = Slyce.Common.Colors.BackgroundColor;
            syntaxEditor1.SelectedView.GoToLine(lineNumber, (lineNumber > 2) ? 2 : 0); // Allow 2 blank lines above selection
        }


        /// <summary>
        /// Removes highlighting of special regions.
        /// </summary>
        private void DeselectLines()
        {
            DeselectLines(true);
        }

        /// <summary>
        /// Removes highlighting of special regions.
        /// </summary>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        private void DeselectLines(int startLine, int endLine)
        {
            DeselectLines(true, startLine, endLine);
        }

        /// <summary>
        /// Removes highlighting of special regions.
        /// </summary>
        /// <param name="restoreOriginalColor"></param>
        private void DeselectLines(bool restoreOriginalColor)
        {
            if (OrigianlLineSpan != null)
            {
                DeselectLines(restoreOriginalColor, OrigianlLineSpan.StartLine, OrigianlLineSpan.EndLine);
            }
        }

        /// <summary>
        /// Removes highlighting of special regions.
        /// </summary>
        private void DeselectLines(bool restoreOriginalColor, int startLine, int endLine)
        {
            Color color = restoreOriginalColor ? OrigianlLineSpan.OriginalColor : Color.White;

            for (int i = startLine; i <= endLine; i++)
            {
                syntaxEditor1.Document.Lines[i].BackColor = color;
            }
            OrigianlLineSpan = null;
        }

        private void syntaxEditor1_ContextMenuRequested(object sender, ActiproSoftware.SyntaxEditor.ContextMenuRequestEventArgs e)
        {
            if (DisplayingTextMessage)
            {
                return;
            }
            if (CurrentDiffType != TypeOfDiff.Conflict)
            {
                syntaxEditor1.DefaultContextMenuEnabled = false;
                return;
            }
            int lineNumber = syntaxEditor1.Views[0].LocationToPosition(e.MenuLocation, ActiproSoftware.SyntaxEditor.LocationToPositionAlgorithm.Block).Line;

            if (syntaxEditor1.Document.Lines[lineNumber].BackColor != Color.White)
            {
                SelectLines(e.MenuLocation);
                syntaxEditor1.DefaultContextMenuEnabled = false;
                contextMenuStrip1.Show(syntaxEditor1, e.MenuLocation);
            }
            else
            {
                DeselectLines();
                syntaxEditor1.DefaultContextMenuEnabled = false;
            }
        }

        private void syntaxEditor1_DocumentTextChanged(object sender, ActiproSoftware.SyntaxEditor.DocumentModificationEventArgs e)
        {
            // Colour lines correctly when new lines inserted or existing lines deleted
            if (e.Modification.LinesInserted + e.Modification.LinesDeleted > 0)
            {
                int lineNum = e.Modification.StartLineIndex;// -1;

                if (e.Modification.LinesInserted > 0 && syntaxEditor1.Document.Lines[lineNum].BackColor == Color.Red &&
                    syntaxEditor1.Document.Lines[lineNum + 1].BackColor != Color.Red)
                {
                    DeselectLines();
                }
                if (e.Modification.LinesDeleted > 0 && OrigianlLineSpan != null)
                {
                    if (OrigianlLineSpan.EndLine - OrigianlLineSpan.StartLine > 0)
                    {
                        OrigianlLineSpan.EndLine -= 1;
                    }
                    if (OrigianlLineSpan.EndLine == OrigianlLineSpan.StartLine)
                    {
                        DeselectLines(true, OrigianlLineSpan.StartLine, OrigianlLineSpan.EndLine);
                        OrigianlLineSpan = null;
                    }
                    if (lineNum + 1 < syntaxEditor1.Document.Lines.Count &&
                        (syntaxEditor1.Document.Lines[lineNum].BackColor == Color.White &&
                            syntaxEditor1.Document.Lines[lineNum + 1].BackColor == Color.Red))
                    {
                        DeselectLines();
                    }
                }
            }
        }

        private bool ConflictsStillExist()
        {
            // Check whether there are any outstanding changes
            int lineCount = syntaxEditor1.Document.Lines.Count;
            bool conflictsExist = false;

            for (int i = 1; i < syntaxEditor1.Document.Lines.Count; i++)
            {
                if (syntaxEditor1.Document.Lines[i].BackColor != Color.White &&
                    !syntaxEditor1.Document.Lines[i].BackColor.IsEmpty)
                {
                    conflictsExist = true;
                    break;
                }
            }
            return conflictsExist;
        }

        private void syntaxEditor1_MouseClick(object sender, MouseEventArgs e)
        {
            if (DisplayingTextMessage)
            {
                return;
            }
            // Check whether the line clicked is part of a coloured region. If
            // it is, then highlight the region in the 'highlight' colour.
            if (syntaxEditor1.SelectedView.Selection.Length > 0)
            {
            }
            int lineNumber = syntaxEditor1.Views[0].LocationToPosition(e.Location, ActiproSoftware.SyntaxEditor.LocationToPositionAlgorithm.Block).Line;

            if (syntaxEditor1.Document.Lines[lineNumber].BackColor != Color.White &&
                syntaxEditor1.Document.Lines[lineNumber].BackColor.ToArgb() != 0)
            {
                SelectLines(e.Location);
            }
            else
            {
                DeselectLines();
            }
        }

        private void syntaxEditor1_UserMarginPaint(object sender, ActiproSoftware.SyntaxEditor.UserMarginPaintEventArgs e)
        {
            if (DisplayingTextMessage)
            {
                return;
            }
            // Add text to the user margin to indicate what kind of line it is.
            if (syntaxEditor1.Document.Lines[e.DisplayLineIndex].BackColor != Color.White)
            {
                // Custom draw a word wrap continuation marker
                Size size = new Size(8, 8);
                int x = e.DisplayLineBounds.Left;
                int y = e.DisplayLineBounds.Top;
                Font font = new Font("Verdana", 6);
                SolidBrush brush = new SolidBrush(Color.DarkBlue);
                Color backColor = syntaxEditor1.Document.Lines[e.DisplayLineIndex].BackColor;
                string colorString = string.Format("{0}, {1}, {2}", backColor.R, backColor.G, backColor.B);
                int newX;

                switch (colorString)
                {
					case Slyce.IntelliMerge.UI.Utility.ColourUserString:
                        newX = e.DisplayLineBounds.Left + (int)((float)(e.DisplayLineBounds.Width - e.Graphics.MeasureString(MarginTextUser, font).Width) / 2);
                        e.Graphics.DrawString(MarginTextUser, font, brush, new Point(newX, y));
                        break;
					case Slyce.IntelliMerge.UI.Utility.ColourNewGenString:
                        newX = e.DisplayLineBounds.Left + (int)((float)(e.DisplayLineBounds.Width - e.Graphics.MeasureString("Template", font).Width) / 2);
                        e.Graphics.DrawString("Template", font, brush, new Point(newX, y));
                        break;
                }
            }
        }

        /// <summary>
        /// Determines whether the location is part of a special region, and highlights the special region
        /// if it is.
        /// </summary>
        /// <param name="location"></param>
        private void SelectLines(Point location)
        {
            int lineNumber = syntaxEditor1.Views[0].LocationToPosition(location, ActiproSoftware.SyntaxEditor.LocationToPositionAlgorithm.Block).Line;
            SelectLines(lineNumber);
        }

        /// <summary>
        /// Determines whether the location is part of a special region, and highlights the special region
        /// if it is.
        /// </summary>
        /// <param name="location"></param>
        private void SelectLines(int lineNumber)
        {
            DeselectLines();
            int spanStartLine = lineNumber;
            int spanEndLine = lineNumber;
            Color selectedColor = syntaxEditor1.Document.Lines[lineNumber].BackColor;
            bool newColourFound = false;

            for (int i = lineNumber; i > 0; i--)
            {
                if (syntaxEditor1.Document.Lines[i].BackColor != selectedColor &&
                    syntaxEditor1.Document.Lines[i].BackColor.ToArgb() != 0)
                {
                    newColourFound = true;
                    break;
                }
                spanStartLine = i;
            }
            if (!newColourFound)
            {
                spanStartLine = lineNumber;
            }
            newColourFound = false;
            for (int i = lineNumber; i < syntaxEditor1.Document.Lines.Count; i++)
            {
                if (syntaxEditor1.Document.Lines[i].BackColor != selectedColor &&
                    syntaxEditor1.Document.Lines[i].BackColor.ToArgb() != 0)
                {
                    newColourFound = true;
                    break;
                }
                spanEndLine = i;
            }
            if (!newColourFound)
            {
                spanEndLine = lineNumber;
            }
            if (newColourFound)
            {
                for (int i = spanStartLine; i <= spanEndLine; i++)
                {
                    syntaxEditor1.Document.Lines[i].BackColor = Color.Red;
                }
            }
            OrigianlLineSpan = new SlyceMerge.LineSpan(spanStartLine, spanEndLine, selectedColor);
        }

        private void mnuAccept_Click(object sender, EventArgs e)
        {
            DeselectLines(false);
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            if (OrigianlLineSpan != null)
            {
                syntaxEditor1.SelectedView.Selection.StartOffset = syntaxEditor1.Document.Lines[OrigianlLineSpan.StartLine].StartOffset;
                syntaxEditor1.SelectedView.Selection.EndOffset = syntaxEditor1.Document.Lines[OrigianlLineSpan.EndLine].EndOffset + 1;

                if (OrigianlLineSpan.EndLine < syntaxEditor1.Document.Lines.Count - 1)
                {
                    OrigianlLineSpan.OriginalColor = syntaxEditor1.Document.Lines[OrigianlLineSpan.EndLine + 1].BackColor;
                }
                else
                {
                    OrigianlLineSpan.OriginalColor = Color.White;
                }
                syntaxEditor1.SelectedView.Delete();
                DeselectLines();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (SaveCurrentFile(false))
            {
                this.Close();
            }
        }

        private void toolStripButtonSwitchView_Click(object sender, EventArgs e)
        {
            ArrayList linesMine = new ArrayList();
            ArrayList linesYours = new ArrayList();
            int mineStart = -1;
            int mineEnd = -1;
            int yourStart = -1;
            int yourEnd = -1;

            for (int i = 0; i < syntaxEditor1.Document.Lines.Count; i++)
            {
                if (syntaxEditor1.Document.Lines[i].BackColor == Slyce.IntelliMerge.UI.Utility.ColourNewGen)
                {
                    if (mineStart >= 0)
                    {
                        linesMine.Add(new SlyceMerge.LineSpan(mineStart, i - 1, Color.Black));
                        mineStart = mineEnd = -1;
                    }
                    if (yourStart < 0)
                    {
                        yourStart = i;
                    }
                }
                else if (syntaxEditor1.Document.Lines[i].BackColor == Slyce.IntelliMerge.UI.Utility.ColourUser)
                {
                    if (yourStart >= 0)
                    {
                        linesYours.Add(new SlyceMerge.LineSpan(yourStart, i - 1, Color.Black));
                        yourStart = yourEnd = -1;
                    }
                    if (mineStart < 0)
                    {
                        mineStart = i;
                    }
                }
                else if (syntaxEditor1.Document.Lines[i].BackColor == Color.White ||
               syntaxEditor1.Document.Lines[i].BackColor == Color.Empty)
                {
                    if (mineStart >= 0)
                    {
                        linesMine.Add(new SlyceMerge.LineSpan(mineStart, i - 1, Color.Black));
                        mineStart = mineEnd = -1;
                    }
                    if (yourStart >= 0)
                    {
                        linesYours.Add(new SlyceMerge.LineSpan(yourStart, i - 1, Color.Black));
                        yourStart = yourEnd = -1;
                    }
                    mineStart = mineEnd = -1;
                    yourStart = yourEnd = -1;
                }
                else
                {
                    throw new NotImplementedException("Shouldn't be here");
                }
            }
            if (yourStart >= 0)
            {
                linesYours.Add(new SlyceMerge.LineSpan(yourStart, syntaxEditor1.Document.Lines.Count - 1, Color.Black));
            }
            if (mineStart >= 0)
            {
                linesMine.Add(new SlyceMerge.LineSpan(mineStart, syntaxEditor1.Document.Lines.Count - 1, Color.Black));
            }
            Slyce.IntelliMerge.UI.Utility.PopulateSyntaxEditors(syntaxEditor1, syntaxEditor2, syntaxEditor1.Text, (SlyceMerge.LineSpan[])linesMine.ToArray(typeof(SlyceMerge.LineSpan)), (SlyceMerge.LineSpan[])linesYours.ToArray(typeof(SlyceMerge.LineSpan)));

            syntaxEditor1.Width = this.ClientSize.Width / 2 - 5;
            syntaxEditor2.Left = this.ClientSize.Width / 2 + 5;
            syntaxEditor2.Width = this.ClientSize.Width - syntaxEditor2.Left - 5;
            syntaxEditor2.Top = syntaxEditor1.Top;
            syntaxEditor2.Height = syntaxEditor1.Height;
            syntaxEditor2.Visible = true;
        }

        private void toolStripButtonNextConflict_Click(object sender, EventArgs e)
        {
            GotoNextConflictLine(syntaxEditor1.SelectedView.Selection.StartDocumentPosition.Line);
        }

        private void toolStripButtonPrevConflict_Click(object sender, EventArgs e)
        {
            GotoPrevConflictLine(syntaxEditor1.SelectedView.Selection.StartDocumentPosition.Line);
        }

        private void GotoNextConflictLine(int currentLine)
        {
            Color currentColor = syntaxEditor1.Document.Lines[currentLine].BackColor;
            bool inColor = currentColor != Color.White && !currentColor.IsEmpty;
            bool haveExitedColor = !inColor;
            int offsetLineCount = 0;

            for (int i = currentLine + 1; i < syntaxEditor1.Document.Lines.Count; i++)
            {
                currentColor = syntaxEditor1.Document.Lines[i].BackColor;

                if (!haveExitedColor && inColor && (currentColor.IsEmpty || currentColor == Color.White))
                {
                    haveExitedColor = true;
                }
                if (inColor && haveExitedColor && !currentColor.IsEmpty && currentColor != Color.White)
                {
                    if (i > 2) { offsetLineCount = 2; }
                    syntaxEditor1.SelectedView.GoToLine(i, offsetLineCount); // Allow 2 blank lines above selection
                    currentLine = i;
                    break;
                }
                if (!inColor && !currentColor.IsEmpty && currentColor != Color.White)
                {
                    if (i > 2) { offsetLineCount = 2; }
                    syntaxEditor1.SelectedView.GoToLine(i, offsetLineCount); // Allow 2 blank lines above selection
                    currentLine = i;
                    break;
                }
            }
            SetNavigationButtons();
        }

        private void GotoPrevConflictLine(int currentLine)
        {
            Color currentColor = syntaxEditor1.Document.Lines[currentLine].BackColor;
            bool inColor = currentColor != Color.White && !currentColor.IsEmpty;
            bool haveExitedColor = !inColor;
            int offsetLineCount = 0;
            bool haveReenteredColor = false;

            for (int i = currentLine - 1; i >= 0; i--)
            {
                currentColor = syntaxEditor1.Document.Lines[i].BackColor;

                if (!haveExitedColor && inColor && (currentColor.IsEmpty || currentColor == Color.White))
                {
                    haveExitedColor = true;
                }
                if (haveExitedColor && !currentColor.IsEmpty && currentColor != Color.White)
                {
                    haveReenteredColor = true;
                }
                if (haveReenteredColor && (currentColor.IsEmpty || currentColor == Color.White))
                {
                    if (i > 2) { offsetLineCount = 2; }
                    syntaxEditor1.SelectedView.GoToLine(i + 1, offsetLineCount); // Allow 2 blank lines above selection
                    currentLine = i;
                    break;
                }
                else if (haveReenteredColor && i == 0)
                {
                    syntaxEditor1.SelectedView.GoToLine(0); // Allow 2 blank lines above selection
                    currentLine = i;
                    break;
                }
            }
            SetNavigationButtons();
        }


        /// <summary>
        /// Enables or disables the NextConflict and PrevConflict toolbar buttons, depending on if there are any
        /// more conflicts before or after the current caret position.
        /// </summary>
        private void SetNavigationButtons()
        {
            int currentLine = syntaxEditor1.SelectedView.Selection.StartDocumentPosition.Line;
            Color currentColor = syntaxEditor1.Document.Lines[currentLine].BackColor;
            bool moreExist = false;
            bool inColor = true;

            #region Next
            for (int i = currentLine + 1; i < syntaxEditor1.Document.Lines.Count; i++)
            {
                currentColor = syntaxEditor1.Document.Lines[i].BackColor;

                if (currentColor.IsEmpty || currentColor == Color.White)
                {
                    inColor = false;
                }
                if (!inColor && !currentColor.IsEmpty && currentColor != Color.White)
                {
                    moreExist = true;
                    break;
                }
            }
            toolStripButtonNextConflict.Enabled = moreExist;
            #endregion

            moreExist = false;
            inColor = true;

            #region Previous
            for (int i = currentLine - 1; i >= 0; i--)
            {
                currentColor = syntaxEditor1.Document.Lines[i].BackColor;

                if (currentColor.IsEmpty || currentColor == Color.White)
                {
                    inColor = false;
                }
                if (!inColor && !currentColor.IsEmpty && currentColor != Color.White)
                {
                    moreExist = true;
                    break;
                }
            }
            toolStripButtonPrevConflict.Enabled = moreExist;
            #endregion

            if (CurrentDiffType == TypeOfDiff.Conflict)
            {
                toolStripButtonSwitchView.Enabled = false;

                if (syntaxEditor1.Document.Lines[currentLine].BackColor != Color.White || (toolStripButtonNextConflict.Enabled && toolStripButtonPrevConflict.Enabled))
                {
                    this.Text = "Merge Editor - Conflicts Exist";
                }
                else
                {
                    this.Text = "Merge Editor - No Conflicts";
                }
            }
        }

        private void syntaxEditor1_SelectionChanged(object sender, ActiproSoftware.SyntaxEditor.SelectionEventArgs e)
        {
            if (DisplayingTextMessage)
            {
                return;
            }
            if (syntaxEditor1.SelectedView.Selection.StartDocumentPosition.Line != CurrentLine)
            {
                CurrentLine = syntaxEditor1.SelectedView.Selection.StartDocumentPosition.Line;
                SetNavigationButtons();
            }
        }

        private void toolStripButtonAcceptUserChanges_Click(object sender, EventArgs e)
        {
            AcceptAllUserChanges();
        }

        private void AcceptAllUserChanges()
        {
            bool conflictsRemain = true;
            bool found = false;
            syntaxEditor1.SuspendPainting();

            while (conflictsRemain)
            {
                found = false;

                for (int i = 0; i < syntaxEditor1.Document.Lines.Count; i++)
                {
                    if (syntaxEditor1.Document.Lines[i].BackColor == Slyce.IntelliMerge.UI.Utility.ColourUser)
                    {
                        SelectLines(i);
                        DeselectLines(false);
                        found = true;
                        break;
                    }
                    else if (syntaxEditor1.Document.Lines[i].BackColor == Slyce.IntelliMerge.UI.Utility.ColourNewGen)
                    {
                        SelectLines(i);
                        syntaxEditor1.SelectedView.Selection.StartOffset = syntaxEditor1.Document.Lines[OrigianlLineSpan.StartLine].StartOffset;
                        syntaxEditor1.SelectedView.Selection.EndOffset = syntaxEditor1.Document.Lines[OrigianlLineSpan.EndLine].EndOffset + 1;
                        DeselectLines(false);
                        syntaxEditor1.SelectedView.Delete();
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    conflictsRemain = false;
                }
            }
            syntaxEditor1.ResumePainting();
        }

        private void AcceptAllTemplateChanges()
        {
            bool conflictsRemain = true;
            bool found = false;
            syntaxEditor1.SuspendPainting();

            while (conflictsRemain)
            {
                found = false;

                for (int i = 0; i < syntaxEditor1.Document.Lines.Count; i++)
                {
                    if (syntaxEditor1.Document.Lines[i].BackColor == Slyce.IntelliMerge.UI.Utility.ColourNewGen)
                    {
                        SelectLines(i);
                        DeselectLines(false);
                        found = true;
                        break;
                    }
                    else if (syntaxEditor1.Document.Lines[i].BackColor == Slyce.IntelliMerge.UI.Utility.ColourUser)
                    {
                        SelectLines(i);
                        syntaxEditor1.SelectedView.Selection.StartOffset = syntaxEditor1.Document.Lines[OrigianlLineSpan.StartLine].StartOffset;
                        syntaxEditor1.SelectedView.Selection.EndOffset = syntaxEditor1.Document.Lines[OrigianlLineSpan.EndLine].EndOffset + 1;
                        DeselectLines(false);
                        syntaxEditor1.SelectedView.Delete();
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    conflictsRemain = false;
                }
            }
            syntaxEditor1.ResumePainting();
        }

        private void toolStripButtonAcceptTemplateChanges_Click(object sender, EventArgs e)
        {
            AcceptAllTemplateChanges();
        }

        private void toolStripComboBoxFileType_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            Controller.ShowHelpTopic(this, "ArchAngel Help.chm", "Workbench_Screen_Generation.htm#Merging_Conflicts");
        }

        private void FormMergeEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controller.Instance.UnshadeMainForm();
        }






    }
}