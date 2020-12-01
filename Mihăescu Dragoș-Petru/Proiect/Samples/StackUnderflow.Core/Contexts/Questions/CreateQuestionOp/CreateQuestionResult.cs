using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public int QuestionId { get;  }
            public string Title { get;  }
            public string Body { get;  }
            public List<string> Tags { get;  }
            public int voteCounter { get;  }
           

            public QuestionCreated(int questionId, string postedQuestionTitle, string postedQuestionBody, List<string> postedQuestionTags, int votecounter)
            {
                QuestionId = questionId;
                Title = postedQuestionTitle;
                Body = postedQuestionBody;
                Tags = postedQuestionTags;
                voteCounter = votecounter;
            
            }
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get;  }

            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get;  }

            public QuestionValidationFailed(IEnumerable<string> questionErrors)
            {
                ValidationErrors = questionErrors.AsEnumerable();
            }
        }
    }
}
