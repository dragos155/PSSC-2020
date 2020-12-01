using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.SendReplyAuthorAcknowledgementOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Questions.SendReplyAuthorAcknowledgementOp.SendReplyAuthorAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAcknowledgementOp
{
    public class SendReplyAuthorAcknowledgement : Adapter<SendReplyAuthorAcknowledgementCmd, ISendReplyAuthorAcknowledgementResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public override Task PostConditions(SendReplyAuthorAcknowledgementCmd cmd, ISendReplyAuthorAcknowledgementResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendReplyAuthorAcknowledgementResult> Work(SendReplyAuthorAcknowledgementCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = SendQuestion(state, SendReplyAuthorAcknowledgementFromCmd(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new AcknowledgementNotSent(ex.Message)
                );

            return result;
        }

        private ISendReplyAuthorAcknowledgementResult SendQuestion(QuestionsWriteContext state, object v)
        {
            return new AcknowledgementSent(1, 2);
        }

        private object SendReplyAuthorAcknowledgementFromCmd(SendReplyAuthorAcknowledgementCmd cmd)
        {
            return new { };
        }
    }
}
