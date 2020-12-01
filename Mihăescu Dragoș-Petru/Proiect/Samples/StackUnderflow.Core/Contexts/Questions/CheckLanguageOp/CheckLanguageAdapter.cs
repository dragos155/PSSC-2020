using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using Access.Primitives.IO;
using System;
using System.Collections.Generic;
using System.Text;
using static StackUnderflow.Domain.Schema.Questions.CheckLanguageOp.CheckLanguageResult;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CheckLanguageOp
{
    public class CheckLanguageAdapter : Adapter<CheckLanguageCmd, ICheckLanguageResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public override Task PostConditions(CheckLanguageCmd cmd, ICheckLanguageResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICheckLanguageResult> Work(CheckLanguageCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = VerifyLanguage(state, VerifyLanguageFromCmd(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new ValidationFailed(ex.Message)
                );

            return result;
        }

        private ICheckLanguageResult VerifyLanguage(QuestionsWriteContext state, object v)
        {
            return new ValidationSucceeded("The language is valid!");
        }

        private object VerifyLanguageFromCmd(CheckLanguageCmd cmd)
        {
            return new { };
        }
    }
}
