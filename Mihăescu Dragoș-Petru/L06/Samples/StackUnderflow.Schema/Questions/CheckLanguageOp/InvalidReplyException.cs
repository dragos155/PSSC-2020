using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CheckLanguageOp
{
    class InvalidReplyException : Exception
    {
        public InvalidReplyException() { }
        public InvalidReplyException(string reply) : base($"Invalid text:{reply}") { }
    }
}
