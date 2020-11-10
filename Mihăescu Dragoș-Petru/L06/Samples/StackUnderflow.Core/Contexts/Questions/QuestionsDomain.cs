using Access.Primitives.IO;
using LanguageExt;
using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using StackUnderflow.Domain.Schema.Questions.CreateAnswerOp;
using StackUnderflow.Domain.Schema.Questions.SendQuestionOwnerAcknowledgementOp;
using StackUnderflow.Domain.Schema.Questions.SendReplyAuthorAcknowledgementOp;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Schema.Questions.CheckLanguageOp.CheckLanguageResult;
using static StackUnderflow.Domain.Schema.Questions.CreateAnswerOp.CreateReplyResult;
using static StackUnderflow.Domain.Schema.Questions.SendQuestionOwnerAcknowledgementOp.SendQuestionOwnerAcknowledgementResult;
using static StackUnderflow.Domain.Schema.Questions.SendReplyAuthorAcknowledgementOp.SendReplyAuthorAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionsDomain
    {
        //public static Port<outputResult> CreateReply(inputs) =>
        //    NewPort<inputCmd, outputResult>(new inputCmd(inputs));

        public static Port<ICreateReplyResult> CreateReply(int questionId, int authorUserId, int ownerId, string body) =>
            NewPort<CreateReplyCmd, ICreateReplyResult>(new CreateReplyCmd(questionId, authorUserId,ownerId , body));

        public static Port<ICheckLanguageResult> CheckLanguage(string text) =>
            NewPort<CheckLanguageCmd, ICheckLanguageResult>(new CheckLanguageCmd(text));

        public static Port<ISendQuestionOwnerAcknowledgementResult> SendQuestionOwnerAcknowledgement(int questionId, int questionOwnerId) =>
            NewPort<SendQuestionOwnerAcknowledgementCmd, ISendQuestionOwnerAcknowledgementResult>(new SendQuestionOwnerAcknowledgementCmd(questionId, questionOwnerId));

        public static Port<ISendReplyAuthorAcknowledgementResult> SendReplyAuthorAcknowledgement(int replyAuthorId, int questionId, Option<ReplyCreated> reply) =>
           NewPort<SendReplyAuthorAcknowledgementCmd, ISendReplyAuthorAcknowledgementResult>(new SendReplyAuthorAcknowledgementCmd(replyAuthorId, questionId, reply));
    }
}
