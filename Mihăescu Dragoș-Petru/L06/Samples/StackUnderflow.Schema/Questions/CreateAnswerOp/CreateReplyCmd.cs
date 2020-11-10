using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateAnswerOp
{
    public class CreateReplyCmd
    {
        public CreateReplyCmd(int questionId, int authorUserId, int questionOwnerId,string body)
        {
            QuestionId = questionId;
            AuthorUserId = authorUserId;
            QuestionOwnerId = questionOwnerId;
            Body = body;
        }
        [Required]
        public int QuestionId { get; }
        [Required]
        public int QuestionOwnerId { get; set; }
        [Required]
        public int AuthorUserId { get; }
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Body { get; }
    }
}
