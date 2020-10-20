using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Net;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> Tags = new List<string> { "JAVA", "C#" , "C++" };
            var cmd = new CreateQuestionCmd("Ce este un constructor", "Cum creez un constructor automat?", Tags);
            var result = CreateQuestion(cmd);
             
            result.Match(
                ProcessQuestionCreated,
                ProcessQuestionNotCreated,
                ProcessInvalidQuestion
                );

            Console.ReadLine();

        }
        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed:");

            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }

            return validationErrors;
        }
        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Reason}");

            return questionNotCreatedResult;
        }

        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.QuestionId}");

            return question;
        }
        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title)) 
            {
                var errors = new List<string>() { "Invlaid Title" };

                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrWhiteSpace(createQuestionCommand.Description))
            {
                var errors = new List<string>() { "Invlaid Description" };

                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotCreated("Question could not be verified");
            }
            var questionId = Guid.NewGuid();

            var result = new QuestionCreated(questionId, createQuestionCommand.Title, createQuestionCommand.Description, createQuestionCommand.Tags);

            return result;
        }
    }
}
