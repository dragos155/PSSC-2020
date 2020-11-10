using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using static StackUnderflow.Domain.Schema.Questions.CreateAnswerOp.CreateReplyResult;

namespace StackUnderflow.Domain.Schema.Questions.SendReplyAuthorAcknowledgementOp
{
    public class SendReplyAuthorAcknowledgementCmd
    {
        public SendReplyAuthorAcknowledgementCmd(int replyAuthorId, int questionId, Option<ReplyCreated> reply)
        {
            ReplyAuthorId = replyAuthorId;
            QuestionId = questionId;
            Reply = reply;
        }

        public int ReplyAuthorId { get; }
        public int QuestionId { get; }
        public Option<ReplyCreated> Reply { get; }
    }
}
