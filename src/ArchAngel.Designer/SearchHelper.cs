using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiproSoftware.SyntaxEditor;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
{
	[Serializable]
    public class FoundLocation
    {
        public FunctionInfo Function;
        public int StartPos = -1;
        public int Length;

        public FoundLocation(FunctionInfo function, int startPos, int length)
        {
            Function = function;
            StartPos = startPos;
            Length = length;
        }

        public string Body
        {
            get
            {
                // Check whether this function is open in an editor first
                for (int pageCounter = 0; pageCounter < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; pageCounter++)
                {
                    ucFunction functionScreen = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(pageCounter);

                    if (functionScreen.CurrentFunction == Function)
                    {
                        // We don't need to replace \r\n with \n, because the SyntaxEditor control automatically stores it in this format internally already
                        return functionScreen.syntaxEditor1.Text;
                    }
                }
                // This function is not open in an editor, so just fetch the saved version
                return Function.Body.Replace("\r\n", "\n");
            }
        }
    }

    class SearchHelper
    {
        #region Enums
        public enum Scope
        {
            ScriptOnly,
            OutputOnly,
            Both
        }
        public enum SearchFunctions
        {
            CurrentFunction,
            OpenFunctions,
            AllFunctions
        }
        #endregion

        #region Inner Classes
        #endregion

        private static readonly List<FoundLocation> m_foundLocations = new List<FoundLocation>();
        private static int CurrentIndex = -1;
    	public static FindReplaceOptions Options;
        public static Scope _scope = Scope.Both;
        public static SearchFunctions searchFunctions;
        private const string ValidWordChars = "abcdefghijklmnopqrstuvwxyz0123456789_";
        //internal static string TextToFind = "";

        public static Scope scope
        {
            get
            {
                return _scope;
            }
            set
            {
                _scope = value;
            }
        }

        public static List<FoundLocation> FoundLocations
        {
            get { return m_foundLocations; }
        }

        public static void FindNext()//string textToFind, Scope scope, SearchFunctions searchFunctions, FindReplaceOptions options)
        {
            switch (searchFunctions)
            {
                case SearchFunctions.CurrentFunction:
                    ucFunction functionPage = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();

                    if (functionPage != null)
                    {
                        FunctionInfo function = functionPage.CurrentFunction;
                    	SyntaxEditor editor = functionPage.syntaxEditor1;
                        FindInText(editor.Text, Options.FindText, scope, function, function.IsTemplateFunction, Options, editor.SelectedView.Selection.StartOffset, true);
                    }
                    break;
                case SearchFunctions.OpenFunctions:
                    break;
                case SearchFunctions.AllFunctions:
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
        }

        public static void RunFind()//string textToFind, Scope scope, SearchFunctions searchFunctions, FindReplaceOptions options)
        {
            m_foundLocations.Clear();

            switch (searchFunctions)
            {
                case SearchFunctions.AllFunctions:
                    ArrayList openFunctions = new ArrayList(Project.Instance.Functions.Count);

                    // Process open functions first
                    for (int tabCounter = 0; tabCounter < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; tabCounter++)
                    {
                        FunctionInfo function = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter).CurrentFunction;
                        openFunctions.Add(function.Name);
                        SyntaxEditor editor = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter).syntaxEditor1;
                        FindInText(editor.Text, Options.FindText, scope, function, function.IsTemplateFunction, Options);
                    }
                    openFunctions.Sort();
                    // Process remaining functions
                    for (int functionCounter = 0; functionCounter < Project.Instance.Functions.Count; functionCounter++)
                    {
                        FunctionInfo function = Project.Instance.Functions[functionCounter];

                        // Don't add functions that have already been added as open functions
                        if (openFunctions.BinarySearch(function.Name) < 0)
                        {
                            FindInText(function.Body, Options.FindText, scope, function, function.IsTemplateFunction, Options);
                        }
                    }
                    break;
                case SearchFunctions.CurrentFunction:
                    if (Controller.Instance.MainForm.UcFunctions != null &&
                        Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count > 0)
                    {
                        FunctionInfo function = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage().CurrentFunction;
                        SyntaxEditor editor = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage().syntaxEditor1;
                        FindInText(editor.Text, Options.FindText, scope, function, function.IsTemplateFunction, Options, editor.SelectedView.Selection.StartOffset, false);
                    }
                    break;
                case SearchFunctions.OpenFunctions:
                    if (Controller.Instance.MainForm.UcFunctions != null &&
                            Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count > 0)
                    {
                        for (int tabCounter = 0; tabCounter < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; tabCounter++)
                        {
                            FunctionInfo function = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter).CurrentFunction;
                            SyntaxEditor editor = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter).syntaxEditor1;
                            FindInText(editor.Text, Options.FindText, scope, function, function.IsTemplateFunction, Options);
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textToFind"></param>
        /// <param name="replacementText"></param>
        /// <returns>Number of replacements made.</returns>
        public static int ReplaceAll(string textToFind, string replacementText)
        {
            SearchHelper.Search(textToFind);
        	int padIncrement = replacementText.Length - textToFind.Length;

        	for (int i = 0; i < SearchHelper.FoundLocations.Count; i++)
            {
                FunctionInfo currentFunction = SearchHelper.FoundLocations[i].Function;
                int pad = 0;

            	bool iHasBeenIncremented;
            	switch (SearchHelper.searchFunctions)
                {
                    case SearchHelper.SearchFunctions.CurrentFunction:
                        ucFunction currentFunctionScreen = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();

                        if (currentFunctionScreen.CurrentFunction == currentFunction)
                        {
                            SyntaxEditor editor = currentFunctionScreen.syntaxEditor1;
                            StringBuilder sb = new StringBuilder(editor.Document.Text, editor.Document.Text.Length + 100);
                            sb.Replace("\r\n", "\n");
                            iHasBeenIncremented = false;
                            try
                            {
                                editor.SuspendPainting();

                                while (i < SearchHelper.FoundLocations.Count &&
                                    SearchHelper.FoundLocations[i].Function == currentFunction)
                                {
                                    sb.Remove(SearchHelper.FoundLocations[i].StartPos + pad, SearchHelper.FoundLocations[i].Length);
                                    sb.Insert(SearchHelper.FoundLocations[i].StartPos + pad, replacementText);
                                    pad += padIncrement;
                                    iHasBeenIncremented = true;
                                    i++;
                                }
                                editor.Document.Text = sb.ToString();
                            }
                            finally
                            {
                                editor.ResumePainting();
                            }
                            // Make sure we don't skip any locations, because the for loop at 
                            // the top-level also increments i.
                            if (iHasBeenIncremented)
                            {
                                i--;
                            }
                        }
                        //for (int tabCounter = 0; tabCounter < Controller.Instance.MainForm.UcFunctions.tabStrip1.Pages.Count; tabCounter++)
                        break;
                    case SearchHelper.SearchFunctions.OpenFunctions:
                        for (int tabCounter = 0; tabCounter < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; tabCounter++)
                        {
                            ucFunction functionScreen = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter);

                            if (functionScreen.CurrentFunction == currentFunction)
                            {
                                SyntaxEditor editor = functionScreen.syntaxEditor1;
                                StringBuilder sb = new StringBuilder(editor.Document.Text, editor.Document.Text.Length + 100);
                                sb.Replace("\r\n", "\n");
                                iHasBeenIncremented = false;
                                try
                                {
                                    editor.SuspendPainting();

                                    while (i < SearchHelper.FoundLocations.Count &&
                                        SearchHelper.FoundLocations[i].Function == currentFunction)
                                    {
                                        sb.Remove(SearchHelper.FoundLocations[i].StartPos + pad, SearchHelper.FoundLocations[i].Length);
                                        sb.Insert(SearchHelper.FoundLocations[i].StartPos + pad, replacementText);
                                        pad += padIncrement;
                                        iHasBeenIncremented = true;
                                        i++;
                                    }
                                    editor.Document.Text = sb.ToString();
                                }
                                finally
                                {
                                    editor.ResumePainting();
                                }
                                // Make sure we don't skip any locations, because the for loop at 
                                // the top-level also increments i.
                                if (iHasBeenIncremented)
                                {
                                    i--;
                                }
                            }
                        }
                        break;
                    case SearchHelper.SearchFunctions.AllFunctions:
                        bool functionIsOpen = false;

                        for (int tabCounter = 0; tabCounter < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; tabCounter++)
                        {
                            ucFunction functionScreen2 = Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(tabCounter);

                            if (functionScreen2.CurrentFunction == currentFunction)
                            {
                                functionIsOpen = true;
                                SyntaxEditor editor = functionScreen2.syntaxEditor1;
                                StringBuilder sb = new StringBuilder(editor.Document.Text, editor.Document.Text.Length + 100);
                                sb.Replace("\r\n", "\n");
                                iHasBeenIncremented = false;

                                while (i < SearchHelper.FoundLocations.Count &&
                                    SearchHelper.FoundLocations[i].Function == currentFunction)
                                {
                                    try
                                    {
                                        editor.SuspendPainting();
                                        sb.Remove(SearchHelper.FoundLocations[i].StartPos + pad, SearchHelper.FoundLocations[i].Length);
                                        sb.Insert(SearchHelper.FoundLocations[i].StartPos + pad, replacementText);
                                        editor.Document.Text = sb.ToString();
                                    }
                                    finally
                                    {
                                        editor.ResumePainting();
                                    }
                                    pad += padIncrement;
                                    iHasBeenIncremented = true;
                                    i++;
                                }
                                // Make sure we don't skip any locations, because the for loop at 
                                // the top-level also increments i.
                                if (iHasBeenIncremented)
                                {
                                    i--;
                                }
                            }
                        }
                        if (!functionIsOpen)
                        {
                            FunctionInfo function = SearchHelper.FoundLocations[i].Function;
                            iHasBeenIncremented = false;

                            while (i < SearchHelper.FoundLocations.Count &&
                                SearchHelper.FoundLocations[i].Function == currentFunction)
                            {
                                function.Body = function.Body.Replace("\r\n", "\n");
                                function.Body = function.Body.Remove(SearchHelper.FoundLocations[i].StartPos + pad, SearchHelper.FoundLocations[i].Length);
                                function.Body = function.Body.Insert(SearchHelper.FoundLocations[i].StartPos + pad, replacementText);
                                pad += padIncrement;
                                iHasBeenIncremented = true;
                                i++;
                            }
                            // Make sure we don't skip any locations, because the for loop at 
                            // the top-level also increments i.
                            if (iHasBeenIncremented)
                            {
                                i--;
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException("Not coded yet.");
                }
            }
            return SearchHelper.FoundLocations.Count;
        }

        private static void FindInText(string text, string textToFind, Scope scope, FunctionInfo function, bool isTemplateFunction, FindReplaceOptions options)
        {
            FindInText(text, textToFind, scope, function, isTemplateFunction, options, -1, false);
        }

        /// <summary>
		/// Fills FoundLocations with positions in the text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textToFind"></param>
        /// <param name="scope"></param>
        /// <param name="function"></param>
        /// <param name="isTemplateFunction"></param>
        /// <param name="options"></param>
        /// <param name="userOffset"></param>
        /// <param name="findOneOnly"></param>
        /// <returns></returns>
        private static bool FindInText(string text, string textToFind, Scope scope, FunctionInfo function, bool isTemplateFunction, FindReplaceOptions options, int userOffset, bool findOneOnly)
        {
            text = text.Replace("\r\n", "\n");
            // TODO: this function is in dire need of refactoring. The searching code is repeated in multiple places.
            bool found = false;

            if (string.IsNullOrEmpty(textToFind))
            {
                return false;
            }
            if (!isTemplateFunction)
            {
                // Only template functions should have 'script' and 'output'
                scope = Scope.Both;
            }
            if (!options.MatchCase)
            {
                text = text.ToLower();
                textToFind = textToFind.ToLower();
            }
            int scriptStartPos = 0;
            int scriptEndPos = 0;
            int nextPos;

            switch (scope)
            {
                case Scope.ScriptOnly:
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i] == '<' &&
                            text.Length > (i + 1) &&
                            text[i + 1] == '%')
                        {
                            scriptStartPos = i + 2;
                        }
                        else if (text[i] == '%' &&
                            text.Length > (i + 1) &&
                            text[i + 1] == '>')
                        {
                            scriptEndPos = i;
                            // Replace text
                            string script = text.Substring(scriptStartPos, scriptEndPos - scriptStartPos);
                            nextPos = script.IndexOf(textToFind);

                            while (nextPos >= 0)
                            {
                                if (!options.MatchWholeWord ||
                                    (options.MatchWholeWord && IsWholeWord(ref script, ref textToFind, ref nextPos)))
                                {
                                    m_foundLocations.Add(new FoundLocation(function, scriptStartPos + nextPos, textToFind.Length));

                                    if (scriptStartPos + nextPos > userOffset)
                                    {
                                        found = true;

                                        if (findOneOnly)
                                        {
                                            m_foundLocations.Clear();
                                            m_foundLocations.Add(new FoundLocation(function, scriptStartPos + nextPos, textToFind.Length));
                                            return found;
                                        }
                                    }
                                }
                                nextPos = script.IndexOf(textToFind, nextPos + 1);
                            }
                            i = scriptEndPos + 2;
                        }
                    }
                    break;
                case Scope.OutputOnly:
                    scriptStartPos = 0; // This works for template functions only

                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i] == '%' &&
                            text.Length > (i + 1) &&
                            text[i + 1] == '>')
                        {
                            scriptStartPos = i + 2;
                        }
                        else if (text[i] == '<' &&
                            text.Length > (i + 1) &&
                            text[i + 1] == '%')
                        {
                            scriptEndPos = i;
                            // Replace text
                            string script = text.Substring(scriptStartPos, scriptEndPos - scriptStartPos);
                            nextPos = script.IndexOf(textToFind);

                            while (nextPos >= 0)
                            {
                                if (!options.MatchWholeWord ||
                                    (options.MatchWholeWord && IsWholeWord(ref text, ref textToFind, ref nextPos)))
                                {
                                    m_foundLocations.Add(new FoundLocation(function, scriptStartPos + nextPos, textToFind.Length));

                                    if (scriptStartPos + nextPos > userOffset)
                                    {
                                        found = true;

                                        if (findOneOnly)
                                        {
                                            m_foundLocations.Clear();
                                            m_foundLocations.Add(new FoundLocation(function, scriptStartPos + nextPos, textToFind.Length));
                                            return found;
                                        }
                                    }
                                }
                                nextPos = script.IndexOf(textToFind, nextPos + 1);
                            }
                            i = scriptEndPos + 2;
                        }
                    }
                    // Search the remaining text
                    if (scriptEndPos < text.Length)
                    {
                        string script = text.Substring(scriptStartPos);
                        nextPos = script.IndexOf(textToFind);

                        while (nextPos >= 0)
                        {
                            if (!options.MatchWholeWord ||
                                    (options.MatchWholeWord && IsWholeWord(ref text, ref textToFind, ref nextPos)))
                            {
                                m_foundLocations.Add(new FoundLocation(function, scriptStartPos + nextPos, textToFind.Length));

                                if (scriptStartPos + nextPos > userOffset)
                                {
                                    found = true;

                                    if (findOneOnly)
                                    {
                                        m_foundLocations.Clear();
                                        m_foundLocations.Add(new FoundLocation(function, scriptStartPos + nextPos, textToFind.Length));
                                        return found;
                                    }
                                }
                            }
                            nextPos = script.IndexOf(textToFind, nextPos + 1);
                        }
                    }
                    break;
                case Scope.Both:
                    nextPos = text.IndexOf(textToFind);

                    while (nextPos >= 0)
                    {
                        if (!options.MatchWholeWord ||
                                    (options.MatchWholeWord && IsWholeWord(ref text, ref textToFind, ref nextPos)))
                        {
                            m_foundLocations.Add(new FoundLocation(function, nextPos, textToFind.Length));

                            if (scriptStartPos + nextPos > userOffset)
                            {
                                found = true;

                                if (findOneOnly)
                                {
                                    m_foundLocations.Clear();
                                    m_foundLocations.Add(new FoundLocation(function, nextPos, textToFind.Length));
                                    return found;
                                }
                            }
                        }
                        nextPos = text.IndexOf(textToFind, nextPos + 1);
                    }
                    break;
                default:
                    throw new NotImplementedException("Not coded yet");
            }
            return found;
        }

        /// <summary>
        /// Gets whether the found word is a whole word.
        /// </summary>
        /// <param name="text">The text being searched.</param>
        /// <param name="findText">The text being searched for.</param>
        /// <param name="pos">The position where the word has been found.</param>
        /// <returns>True if the found word is a whole word, false if it is part of a larger word.</returns>
        private static bool IsWholeWord(ref string text, ref string findText, ref int pos)
        {
            // Check MatchWholeWord
            if (pos > 0 &&
                ValidWordChars.IndexOf(text[pos - 1]) >= 0)
            {
                // MatchWholeWord has failed - there is a valid word character in front of it
                return false;
            }
        	if (text.Length > pos + findText.Length &&
        	    ValidWordChars.IndexOf(text[pos + findText.Length]) >= 0)
        	{
        		// MatchWholeWord has failed - there is a valid word character in after it
        		return false;
        	}
        	return true;
        }

        public static void Search()
        {
            Search("NONE_SPECIFIED");
        }

        public static void Search(string textToFind)
        {
			if (Options == null)
				return;

            if (textToFind == "NONE_SPECIFIED")
            {
                textToFind = Options.FindText;
            }
            Options.FindText = textToFind;
            SearchHelper.RunFind();
            if (SearchHelper.FoundLocations.Count == 0)
            {
                return;
            }
            if (CurrentIndex < 0) { CurrentIndex = 0; }

            if (CurrentIndex < SearchHelper.FoundLocations.Count - 1)
            {
                CurrentIndex++;
            }
            else if (SearchHelper.FoundLocations.Count >= 0)
            {
                CurrentIndex = 0;
            }
            bool found;

            // We now have a collection of indexes, so let's look for the actual words
            switch (searchFunctions)
            {
                case SearchFunctions.CurrentFunction:
                    ucFunction currentFunctionPage = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();

                    if (currentFunctionPage == null)
                    {
                        return;
                    }
                    if (SearchHelper.FoundLocations.Count > 0 &&
                        SearchHelper.FoundLocations[0].Function != currentFunctionPage.CurrentFunction)
                    {
                        scope = SearchHelper.Scope.Both;
                        searchFunctions = SearchHelper.SearchFunctions.AllFunctions;
                        SearchHelper.RunFind();
                    }
                    SyntaxEditor editor = currentFunctionPage.syntaxEditor1;
                    int currentStartPos = editor.SelectedView.Selection.StartOffset + 1;
                    CurrentIndex = -1;
                    found = false;

                    for (int foundLocIndex = 0; foundLocIndex < SearchHelper.FoundLocations.Count; foundLocIndex++)
                    {
                        if (SearchHelper.FoundLocations[foundLocIndex].Function == currentFunctionPage.CurrentFunction)
                        {
                            if (CurrentIndex < 0) { CurrentIndex = foundLocIndex; }

                            CurrentIndex = foundLocIndex;

                            if (SearchHelper.FoundLocations[foundLocIndex].StartPos > currentStartPos)
                            {
                                CurrentIndex = foundLocIndex;
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found) { CurrentIndex = 0; }

                    if (CurrentIndex < 0)
                    {
                        return;
                    }
                    editor.SelectedView.Selection.StartOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos;
                    editor.SelectedView.Selection.EndOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos + SearchHelper.FoundLocations[CurrentIndex].Length;
                    break;
                case SearchFunctions.OpenFunctions:
                case SearchFunctions.AllFunctions:// This is just highlighting the text in all open functions
                    // Make sure highlighting starts from the currently selected position
            		CurrentIndex = -1;
                    ucFunction currentFunctionPage2 = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();
                    found = false;

                    if (currentFunctionPage2 != null)
                    {
                    	int selectedPos = currentFunctionPage2.syntaxEditor1.SelectedView.Selection.StartOffset + currentFunctionPage2.syntaxEditor1.SelectedView.Selection.Length;

                    	if (SearchHelper.FoundLocations.Count > 0)
                        {
                            for (int i = 0; i < SearchHelper.FoundLocations.Count; i++)
                            {
                                if (SearchHelper.FoundLocations[i].Function == currentFunctionPage2.CurrentFunction)
                                {
                                    if (CurrentIndex < 0) { CurrentIndex = i; }

                                    CurrentIndex = i;

                                    if (SearchHelper.FoundLocations[i].StartPos > selectedPos)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
            		if (!found)
                    {
                        if (SearchHelper.FoundLocations.Count > 0)
                        {
                            if (CurrentIndex < SearchHelper.FoundLocations.Count - 1)
                            {
                                CurrentIndex++;
                            }
                            else
                            {
                                CurrentIndex = 0;
                            }
                        }
                    }
                    for (int i = 0; i < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; i++)
                    {
                        if (Controller.Instance.MainForm.UcFunctions.GetFunctionScreenByTabIndex(i).CurrentFunction == SearchHelper.FoundLocations[CurrentIndex].Function)
                        {
                            Controller.Instance.MainForm.UcFunctions.tabStrip1.SelectedTabIndex = i;
                        }
                    }
                    if (Controller.Instance.MainForm.UcFunctions.tabStrip1.SelectedTab != null)
                    {
                        currentFunctionPage2 = Controller.Instance.MainForm.UcFunctions.GetCurrentlyDisplayedFunctionPage();
                        currentFunctionPage2.syntaxEditor1.SelectedView.Selection.StartOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos;
                        currentFunctionPage2.syntaxEditor1.SelectedView.Selection.EndOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos + SearchHelper.FoundLocations[CurrentIndex].Length;
                        break;
                    }
                    break;
                default:
                    throw new NotImplementedException("Not coded yet.");
            }

        }

		/// <summary>
		/// Searches the regex matches, typically only the script or output portions only.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="textToFind"></param>
		/// <param name="replacementText"></param>
		/// <param name="replaceAll"></param>
		/// <param name="startPos"></param>
		/// <returns></returns>
        public static string ReplaceTextInScript(string text, string textToFind, string replacementText, bool replaceAll, ref int startPos)
        {
            bool inScript = false;
            int scriptStartPos = 0;
			StringBuilder sb = new StringBuilder(text);

            for (int i = startPos; i < sb.Length; i++)
            {
                if (sb[i] == '<' &&
                    sb.Length > (i + 1) &&
                    sb[i + 1] == '%')
                {
                    if (inScript)
                    {
                        //throw new Exception("Script start tag with no matching end tag.");
                    }
                    inScript = true;
                    scriptStartPos = i + 2;
                }
                else if (sb[i] == '%' &&
                    sb.Length > (i + 1) &&
                    sb[i + 1] == '>')
                {
                    if (!inScript)
                    {
                        //throw new Exception("Script end tag with no matching start tag.");
                    }
                    inScript = false;
                    int scriptEndPos = i;
                    // Replace text
                    string script = sb.ToString(scriptStartPos, scriptEndPos - scriptStartPos);
                    int originalScriptLength = script.Length;
                    script = script.Replace(textToFind, replacementText);
                    int newScriptLength = script.Length;
                    sb.Remove(scriptStartPos, scriptEndPos - scriptStartPos);
                    sb.Insert(scriptStartPos, script);
                    i += newScriptLength - originalScriptLength;

                    if (replaceAll)
                    {
                        return sb.ToString();
                    }
                }
            }
            return sb.ToString();
        }

    }
}
