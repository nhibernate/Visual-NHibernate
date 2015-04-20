using System.Collections.Generic;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider
{
    interface CodeFormatter
    {
        Dictionary<int,Comment> HandledComments { get; }
        //Region should be made general
        SortedList<int,Region> UnhandledRegions { get; }
        string ProcessBodyText(IAstNodeList statements, IAstNodeList comments, int startOffset, int length);
        string FormatExpression(Expression exp);
    }
}
