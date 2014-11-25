using System;
using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
    [Serializable]
    public class CommentObject
    {
        public List<string> PreceedingComments = new List<string>();
        public string TrailingComment;

        public bool IsTheSame(CommentObject comments)
        {
            if (comments == null)
                return false;

            if (Utility.StringCollectionsAreTheSame(PreceedingComments, comments.PreceedingComments) == false)
            {
                return false;
            }
            if (Equals(TrailingComment, comments.TrailingComment) == false)
            {
                return false;
            }

            return true;
        }

        public CommentObject Clone()
        {
            CommentObject newCom = new CommentObject();
            newCom.PreceedingComments.AddRange(PreceedingComments);
            newCom.TrailingComment = TrailingComment;
            return newCom;
        }

    }
}