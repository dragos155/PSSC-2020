using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public class CreateQuestionAdapter : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public override Task PostConditions(CreateQuestionCmd cmd, ICreateQuestionResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = CreateQuestion(state, CreateQuestionrFromCmd(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new QuestionNotCreated(ex.Message)
                );

            return result;
        }

        private object CreateQuestionrFromCmd(CreateQuestionCmd cmd)
        {
            throw new NotImplementedException();
        }

        private ICreateQuestionResult CreateQuestion(QuestionsWriteContext state, object v)
        {
            List<string> tags = new List<string> { "Styled-components", "Typescript" };
            return new QuestionCreated(1,"Typescript question","How can we use styled-components with typescript?", tags, 5);
        }

        private object CreateQuestionFromCmd(CreateQuestionCmd cmd)
        {
            return new { };
        }
    }
}
