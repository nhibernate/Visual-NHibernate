using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.Products.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Context;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.CSharp;

namespace ArchAngel.Workbench.ContentItems
{
    public class IntelliPromptHelper
    {
        /// <summary>
        /// Occurs before a member list is displayed for a <see cref="SyntaxEditor"/>, allowing for the filtering (removal)
        /// or addition of member list items.
        /// </summary>
        /// <eventdata>
        /// The event handler receives an argument of type <c>IntelliPromptMemberListPreFilterEventArgs</c> containing data related to this event.
        /// </eventdata>
        /// <remarks>
        /// If you choose to perform any filtering, modify the <c>Items</c> property of the event arguments.
        /// </remarks>
        [
            Category("Action"),
            Description("Occurs before a member list is displayed for a SyntaxEditor, allowing for the filtering (removal) or addition of member list items.")
        ]
        public event IntelliPromptMemberListPreFilterEventHandler SyntaxEditorIntelliPromptMemberListPreFilter;

        private DotNetLanguage LanguageType = DotNetLanguage.CSharp;
        private bool CodeSnippetsEnabled = false;

        /// <summary>
        /// Gets the <see cref="DotNetContext"/> for the specified offset.
        /// </summary>
        /// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> to examine.</param>
        /// <param name="offset">The offset at which to base the context.</param>
        /// <param name="beforeOffset">Whether to return the context before the offset.</param>
        /// <param name="forParameterInfo">Whether to return the context for parameter info.</param>
        /// <returns>The <see cref="DotNetContext"/> for the specified offset.</returns>
        protected DotNetContext GetContext(SyntaxEditor syntaxEditor, int offset, bool beforeOffset, bool forParameterInfo)
        {
            // Get the compilation unit and project resolver
            CompilationUnit compilationUnit = syntaxEditor.Document.SemanticParseData as CompilationUnit;
            DotNetProjectResolver projectResolver = syntaxEditor.Document.LanguageData as DotNetProjectResolver;

            // Get the context
            if (beforeOffset)
                return CSharpContext.GetContextBeforeOffset(syntaxEditor.Document, offset, compilationUnit, projectResolver, forParameterInfo);
            else
                return CSharpContext.GetContextAtOffset(syntaxEditor.Document, offset, compilationUnit, projectResolver);
        }

        /// <summary>
        /// Add <see cref="IntelliPromptMemberListItem"/> items that indicate the language keywords to a <see cref="Hashtable"/>.
        /// </summary>
        /// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
        protected void AddKeywordMemberListItems(Hashtable memberListItemHashtable)
        {
            for (int id = CSharpTokenID.ContextualKeywordStart + 1; id < CSharpTokenID.ContextualKeywordEnd; id++)
            {
                string keyword = CSharpTokenID.GetTokenKey(id).ToLower();
                memberListItemHashtable[keyword] = new IntelliPromptMemberListItem(keyword, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);
            }
            for (int id = CSharpTokenID.KeywordStart + 1; id < CSharpTokenID.KeywordEnd; id++)
            {
                string keyword = CSharpTokenID.GetTokenKey(id).ToLower();
                memberListItemHashtable[keyword] = new IntelliPromptMemberListItem(keyword, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);
            }
        }

        /// <summary>
        /// Occurs before a member list is displayed for a <see cref="SyntaxEditor"/>, allowing for the filtering (removal)
        /// or addition of member list items.
        /// </summary>
        /// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will raise the event.</param>
        /// <param name="e">An <c>IntelliPromptMemberListPreFilterEventArgs</c> that contains the event data.</param>
        protected virtual void OnSyntaxEditorIntelliPromptMemberListPreFilter(SyntaxEditor syntaxEditor, IntelliPromptMemberListPreFilterEventArgs e)
        {
            if (this.SyntaxEditorIntelliPromptMemberListPreFilter != null)
                this.SyntaxEditorIntelliPromptMemberListPreFilter(this, e);
        }

        /// <summary>
        /// Provides the core functionality to show an IntelliPrompt member list based on the current context in a <see cref="SyntaxEditor"/>.
        /// </summary>
        /// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
        /// <param name="syntaxEditor">The <see cref="SyntaxEditor"/> that will display the IntelliPrompt member list.</param>
        /// <param name="completeWord">Whether to complete the word.</param>
        /// <returns>
        /// <c>true</c> if an auto-complete occurred or if a IntelliPrompt member list is displayed; otherwise, <c>false</c>.
        /// </returns>
        internal bool ShowIntelliPromptMemberList(DotNetLanguage language, SyntaxEditor syntaxEditor, SyntaxEditor toEditor, bool completeWord, string parameterName)
        {
            // Try and ensure the compilation unit is up-to-date
            SemanticParserService.WaitForParse(SemanticParserServiceRequest.GetParseHashKey(syntaxEditor.Document, syntaxEditor.Document));

            // Get the context
            //DotNetContext context = this.GetContext(syntaxEditor, syntaxEditor.Caret.Offset, true, false);
            DotNetContext context = this.GetContext(syntaxEditor, syntaxEditor.Document.GetText(LineTerminator.Newline).Length - 1, true, false);

            // Initialize the member list
            IntelliPromptMemberList memberList = toEditor.IntelliPrompt.MemberList;// syntaxEditor.IntelliPrompt.MemberList;
            memberList.ResetAllowedCharacters();
            memberList.Clear();
            memberList.ImageList = SyntaxEditor.ReflectionImageList;
            memberList.Context = context;

            // GFH
            if (completeWord && context.InitializationTextRange.StartOffset >= 0)
            {
                string partialWord = syntaxEditor.Document.GetText(LineTerminator.Newline).Substring(context.InitializationTextRange.StartOffset, context.InitializationTextRange.Length);

                if (parameterName.StartsWith(partialWord))
                    memberList.Add(new IntelliPromptMemberListItem(parameterName, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PrivateProperty));
            }

            // Get the member list items
            Hashtable memberListItemHashtable = new Hashtable();

            switch (context.Type)
            {
                case DotNetContextType.AnyCode:
                    // Fill with everything
                    if (context.ProjectResolver != null)
                    {
                        // Fill with child namespace names in the global and imported namespaces
                        //context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, null);
                        //foreach (string namespaceName in context.ImportedNamespaces)
                        //    context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, namespaceName);

                        //// Fill with the types in the global and imported namespaces
                        //context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, null, DomBindingFlags.Default, true);
                        //foreach (string namespaceName in context.ImportedNamespaces)
                        //    context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, namespaceName, DomBindingFlags.Default, true);

                        // Fill with static members of parent types
                        if ((context.TypeDeclarationNode != null) && (context.TypeDeclarationNode.DeclaringType is IDomType))
                            context.ProjectResolver.AddMemberListItemsForDeclaringTypeMembers(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TypeDeclarationNode.DeclaringType, DomBindingFlags.Static | DomBindingFlags.AllAccessTypes);

                        // Fill with nested types
                        if (context.TypeDeclarationNode != null)
                            context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode, DomBindingFlags.Default, true);

                        // Fill with members if in a member (pay attention to if member is instance or static)
                        if (context.TypeDeclarationNode != null)
                        {
                            if (context.MemberDeclarationNode != null)
                            {
                                if (!((IDomMember)context.MemberDeclarationNode).IsStatic)
                                {
                                    // Fill with extension methods
                                    context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                        context.TypeDeclarationNode, DomBindingFlags.Instance |
                                        context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                                }

                                // Fill with members
                                context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode,
                                    (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) |
                                    DomBindingFlags.Static | (((IDomMember)context.MemberDeclarationNode).IsStatic ? DomBindingFlags.None : DomBindingFlags.Instance) |
                                    context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                            }
                            else
                            {
                                // Not within a member so fill with static members
                                context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode,
                                    (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Static |
                                    context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                            }
                        }

                        // Fill with variables defined in the scope
                        context.ProjectResolver.AddMemberListItemsForVariables(memberListItemHashtable, context);

                        // Fill with language keywords
                        //this.AddKeywordMemberListItems(memberListItemHashtable);

                        // Fill with code snippets
                        if (this.CodeSnippetsEnabled)
                            context.ProjectResolver.AddMemberListItemsForCodeSnippets(memberListItemHashtable);
                    }
                    break;
                case DotNetContextType.BaseAccess:
                    // If the context is in an instance member declaration...
                    if ((context.ProjectResolver != null) && (context.MemberDeclarationNode != null) && (!((IDomMember)context.MemberDeclarationNode).IsStatic))
                    {
                        if (context.TargetItem.Type == DotNetContextItemType.Base)
                        {
                            // Fill with extension methods
                            context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.Public | DomBindingFlags.Family | DomBindingFlags.Assembly);

                            // Fill with instance type members
                            context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.Public | DomBindingFlags.Family | DomBindingFlags.Assembly);
                        }
                    }
                    break;
                case DotNetContextType.DocumentationCommentTag:
                    // Add tags
                    if (context.ProjectResolver != null)
                    {
                        context.ProjectResolver.AddMemberListItemsForDocumentationComments(memberListItemHashtable, context,
                            (syntaxEditor.Caret.Offset > 0) && (syntaxEditor.Document[syntaxEditor.Caret.Offset - 1] != '<'));
                    }
                    break;
                case DotNetContextType.AsType:
                case DotNetContextType.IsTypeOfType:
                case DotNetContextType.TryCastType:
                case DotNetContextType.TypeOfType:
                    if (context.ProjectResolver != null)
                    {
                        if (context.TargetItem != null)
                        {
                            switch (context.TargetItem.Type)
                            {
                                case DotNetContextItemType.Namespace:
                                case DotNetContextItemType.NamespaceAlias:
                                    // Fill with child namespaces and types
                                    context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, context.TargetItem.ResolvedInfo.ToString());
                                    context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TargetItem.ResolvedInfo.ToString(), DomBindingFlags.Default, false);
                                    break;
                                case DotNetContextItemType.Type:
                                    // Fill with nested types
                                    context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Default, false);
                                    break;
                            }
                        }
                        else
                        {
                            // VB requires New added for As specifications
                            if ((context.Type == DotNetContextType.AsType) && (language == DotNetLanguage.VB))
                                memberListItemHashtable["New"] = new IntelliPromptMemberListItem("New", (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);

                            // Fill with native types
                            context.ProjectResolver.AddMemberListItemsForNativeTypes(language, memberListItemHashtable, context);

                            // Fill with child namespace names in the global and imported namespaces
                            context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, null);
                            foreach (string namespaceName in context.ImportedNamespaces)
                                context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, namespaceName);

                            // Fill with the types in the imported namespaces
                            context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, null, DomBindingFlags.Default, false);
                            foreach (string namespaceName in context.ImportedNamespaces)
                                context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, namespaceName, DomBindingFlags.Default, false);

                            // Fill with nested types
                            if (context.TypeDeclarationNode != null)
                                context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode, DomBindingFlags.Default, true);
                        }
                    }
                    break;
                case DotNetContextType.NamespaceTypeOrMember:
                    if (context.ProjectResolver != null)
                    {
                        switch (context.TargetItem.Type)
                        {
                            case DotNetContextItemType.Namespace:
                            case DotNetContextItemType.NamespaceAlias:
                                // Fill with child namespaces and types
                                context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, context.TargetItem.ResolvedInfo.ToString());
                                context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TargetItem.ResolvedInfo.ToString(), DomBindingFlags.Default, false);
                                break;
                            case DotNetContextItemType.Constant:
                            case DotNetContextItemType.Type:
                                // Add nested types
                                if (context.TargetItem.ResolvedInfo is IDomType)
                                {
                                    // Fill with nested types
                                    context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Default, false);
                                }

                                // If the context is in a type declaration...
                                if (context.TypeDeclarationNode != null)
                                {
                                    // Fill with static type members
                                    context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                        (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Static |
                                        context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                                }
                                break;
                            case DotNetContextItemType.Member:
                                // If the context is in a type declaration...
                                if (context.TypeDeclarationNode != null)
                                {
                                    // Fill with instance type members of member return type
                                    IDomType type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);
                                    if (type != null)
                                    {
                                        // Fill with extension methods
                                        context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                            type, DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

                                        // Fill with instance type members
                                        context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                            type, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                            context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                                    }
                                }
                                break;
                            case DotNetContextItemType.ArrayItem:
                            case DotNetContextItemType.Parameter:
                            case DotNetContextItemType.Variable:
                                // If the context is in a member declaration...
                                if (context.MemberDeclarationNode != null)
                                {
                                    // Fill with extension methods
                                    context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                        (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance |
                                        context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

                                    // Fill with instance type members
                                    context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                        (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                        context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                                }
                                break;
                        }
                    }
                    break;
                case DotNetContextType.NativeType:
                    // If the context is in a member declaration...
                    if ((context.ProjectResolver != null) && (context.TypeDeclarationNode != null))
                    {
                        if (context.TargetItem.Type == DotNetContextItemType.Type)
                        {
                            // Fill with static type members
                            context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Static |
                                context.AdditionalBindingFlags | DomBindingFlags.Public);
                        }
                    }
                    break;
                case DotNetContextType.NewObjectDeclaration:
                    if ((context.ProjectResolver != null) && (context.TypeDeclarationNode != null))
                    {
                        if (context.TargetItem == null)
                        {
                            // Fill with child namespace names in the global and imported namespaces
                            context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, null);
                            foreach (string namespaceName in context.ImportedNamespaces)
                                context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, namespaceName);

                            // Fill with the creatable types in the global and imported namespaces
                            context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, null, DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);
                            foreach (string namespaceName in context.ImportedNamespaces)
                                context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, namespaceName, DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);

                            // Fill with the creatable nested types 
                            context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TypeDeclarationNode, DomBindingFlags.Default | DomBindingFlags.HasConstructor, true);
                        }
                        else
                        {
                            switch (context.TargetItem.Type)
                            {
                                case DotNetContextItemType.Namespace:
                                case DotNetContextItemType.NamespaceAlias:
                                    // Fill with child namespaces and creatable types
                                    context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable, context.TargetItem.ResolvedInfo.ToString());
                                    context.ProjectResolver.AddMemberListItemsForTypes(memberListItemHashtable, context.TypeDeclarationNode, context.TargetItem.ResolvedInfo.ToString(), DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);
                                    break;
                                case DotNetContextItemType.Type:
                                    // Fill with the creatable nested types 
                                    context.ProjectResolver.AddMemberListItemsForNestedTypes(memberListItemHashtable, context.TypeDeclarationNode, (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Default | DomBindingFlags.HasConstructor, false);

                                    // Fill with extension methods
                                    context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                        (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

                                    // Fill with instance type members
                                    context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                        (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                        context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                                    break;
                            }
                        }
                    }
                    break;
                case DotNetContextType.ThisAccess:
                    // If the context is in an instance member declaration...
                    if ((context.ProjectResolver != null) && (context.MemberDeclarationNode != null) && (!((IDomMember)context.MemberDeclarationNode).IsStatic))
                    {
                        if (context.TargetItem.Type == DotNetContextItemType.This)
                        {
                            // Fill with extension methods
                            context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

                            // Fill with instance type members
                            context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                        }
                    }
                    break;
                case DotNetContextType.BaseMemberAccess:
                case DotNetContextType.ThisMemberAccess:
                    // If the context is in an instance member declaration...
                    if ((context.ProjectResolver != null) && (context.MemberDeclarationNode != null) && (!((IDomMember)context.MemberDeclarationNode).IsStatic))
                    {
                        // Fill with instance type members of member return type
                        IDomType type = null;
                        if (context.TargetItem.ResolvedInfo is IDomType)
                            type = (IDomType)context.TargetItem.ResolvedInfo;
                        else
                            type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 1);

                        if (type != null)
                        {
                            // Fill with extension methods
                            context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                type, DomBindingFlags.Instance | context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);

                            // Fill with instance type members
                            context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                type, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.AllAccessTypes);
                        }
                    }
                    break;
                case DotNetContextType.StringLiteral:
                    // If the context is in a member declaration...
                    if (context.ProjectResolver != null)
                    {
                        if (context.TargetItem.Type == DotNetContextItemType.StringLiteral)
                        {
                            // Fill with extension methods
                            context.ProjectResolver.AddMemberListItemsForExtensionMethods(memberListItemHashtable, context,
                                (IDomType)context.TargetItem.ResolvedInfo, DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.Public);

                            // Fill with string instance type members
                            context.ProjectResolver.AddMemberListItemsForMembers(memberListItemHashtable, context.TypeDeclarationNode,
                                (IDomType)context.TargetItem.ResolvedInfo, (this.LanguageType == DotNetLanguage.CSharp ? DomBindingFlags.ExcludeIndexers : DomBindingFlags.None) | DomBindingFlags.Instance |
                                context.AdditionalBindingFlags | DomBindingFlags.Public);
                        }
                    }
                    break;
                case DotNetContextType.UsingDeclaration:
                    // Fill with namespaces		
                    if (context.ProjectResolver != null)
                        context.ProjectResolver.AddMemberListItemsForChildNamespaces(memberListItemHashtable,
                            (context.TargetItem != null ? context.TargetItem.ResolvedInfo.ToString() : String.Empty));
                    break;
            }

            // Pre-filter the member list
            this.OnSyntaxEditorIntelliPromptMemberListPreFilter(syntaxEditor,
                new IntelliPromptMemberListPreFilterEventArgs(syntaxEditor, context, memberListItemHashtable));

            // Add items
            if (memberListItemHashtable.Count > 0)
            {
                IntelliPromptMemberListItem[] items = new IntelliPromptMemberListItem[memberListItemHashtable.Count];
                memberListItemHashtable.Values.CopyTo(items, 0);
                memberList.AddRange(items);
            }

            // Show the list
            if (memberList.Count > 0)
            {
                if (context.InitializationTextRange.IsDeleted)
                    memberList.Show();
                else if (completeWord)
                {
                    memberList.CompleteWord(toEditor.Caret.Offset - context.InitializationTextRange.Length, context.InitializationTextRange.Length);
                    //memberList.CompleteWord(context.InitializationTextRange.StartOffset, context.InitializationTextRange.Length);
                }
                else
                {
                    memberList.Show(toEditor.Caret.Offset, context.InitializationTextRange.Length);
                    //memberList.Show(context.InitializationTextRange.StartOffset, context.InitializationTextRange.Length);
                }
                return true;
            }
            else if (memberList.Visible)
                memberList.Abort();

            return false;
        }
    }
}
