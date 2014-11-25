using System;

namespace ArchAngel.Providers.CodeProvider.VB
{
    [Serializable]
    class VBCommentObject : BaseCommentObject
    {
        public override BaseCommentObject Clone()
        {
            VBCommentObject newCom = new VBCommentObject();
            newCom.PreceedingComments.AddRange(PreceedingComments);
            newCom.TrailingComment = TrailingComment;
            return newCom;
        }
    }
}
