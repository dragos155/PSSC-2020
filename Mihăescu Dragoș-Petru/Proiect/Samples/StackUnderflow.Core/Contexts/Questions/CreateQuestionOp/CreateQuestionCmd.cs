using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public class CreateQuestionCmd
    { 
            [Required]
            public string Title { get;  set; }
            [Required]
            [MaxLength(1000)]
            public string Body { get;  set; }
            [Required]
            public List<string> Tags { get; set; }
            [Required]
            public int Votes { get;  set; }

            public CreateQuestionCmd(string questionTitle, string questionBody, List<string> questionTags, int questionVotes)
            {
                Title = questionTitle;
                Body = questionBody;
                Tags = questionTags;
                Votes = questionVotes;
            }
        
    }
}