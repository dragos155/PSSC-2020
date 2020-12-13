using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Questions.CheckLanguageOp.CheckLanguageResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CheckLanguageOp
{
    public class CheckLanguageAdaptor : Adapter<CheckLanguageCmd, ICheckLanguageResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public override Task PostConditions(CheckLanguageCmd cmd, ICheckLanguageResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
        public async override Task<ICheckLanguageResult> Work(CheckLanguageCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = VerifyLanguage(state, VerifyAnswerFromCmd(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new ValidationFailed(ex.Message)
                );

            return result;
        }

        private ICheckLanguageResult VerifyLanguage(QuestionsWriteContext state, object v)
        {
            return new ValidationSucceeded("The language was validated");
        }

        private object VerifyAnswerFromCmd(CheckLanguageCmd cmd)
        {
            return new { };
        }
    }
}
