using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Backend.Interfaces.Responses;
using StackUnderflow.Domain.Core;
using StackUnderflow.Domain.Core.Contexts;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using StackUnderflow.EF.Models;
using Access.Primitives.EFCore;
using StackUnderflow.Domain.Core.Contexts.Questions;
using StackUnderflow.Domain.Schema.Questions.CreateAnswerOp;
using static StackUnderflow.Domain.Schema.Questions.CreateAnswerOp.CreateReplyResult;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("backoffice")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;

        public QuestionsController(IInterpreterAsync interpreter, StackUnderflowContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
        }

        [HttpPost("createReply")]
        public async Task<IActionResult> CreateReplyAndAcknowledgement([FromBody] CreateReplyCmd createReplyCmd)
        {
           
            
            var expr = from createReplyResult in QuestionsDomain.CreateReply(createReplyCmd.QuestionId, createReplyCmd.AuthorUserId,createReplyCmd.QuestionOwnerId, createReplyCmd.Body)
                       let reply = createReplyResult.SafeCast<ReplyCreated>().Select(r => r)
                       from checkLanguageResult in QuestionsDomain.CheckLanguage(createReplyCmd.Body)
                       from sendQuestionOwnerAcknowledgementResult in QuestionsDomain.SendQuestionOwnerAcknowledgement(createReplyCmd.QuestionId, createReplyCmd.QuestionOwnerId)
                       from sendReplyAuthorAcknowledgementResult in QuestionsDomain.SendReplyAuthorAcknowledgement(createReplyCmd.AuthorUserId, createReplyCmd.QuestionOwnerId, reply)
                       select new { createReplyResult, checkLanguageResult, sendQuestionOwnerAcknowledgementResult, sendReplyAuthorAcknowledgementResult };

            var ctx = new QuestionReadContext(new List<Post>());
            var r = await _interpreter.Interpret(expr, ctx);

            await _dbContext.SaveChangesAsync();

            return r.createReplyResult.Match(
                created => (IActionResult)Ok("Reply added"),
                notCreated => BadRequest("Reply not added"),
                whenInvalidRequest => BadRequest("Invalid request"));
        }



    }
}
