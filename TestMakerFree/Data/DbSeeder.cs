using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFree.Data.Models;

namespace TestMakerFree.Data
{
    public class DbSeeder
    {
        #region Constructor
        public DbSeeder()
        {
            
        }
        #endregion

        #region Methods
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
                CreateUsers(context);

            if (!context.Quizzes.Any()) 
                CreateQuizzes(context);
        }
        #endregion

        #region Seed Methods
        private static void CreateUsers(ApplicationDbContext context)
        {
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            var user_Admin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
            };

            context.Users.Add(user_Admin);

#if DEBUG
            var user_Ryan = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Ryan",
                Email = "ryan@testmakerfree.com",
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
            };

            var user_Solice = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Solice",
                Email = "solice@testmakerfree.com",
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
            };

            var user_Vodan = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Vodan",
                Email = "vodan@testmakerfree.com",
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
            };

            context.Users.AddRange(user_Ryan, user_Solice, user_Vodan);
#endif
            context.SaveChanges();
        }

        private static void CreateQuizzes(ApplicationDbContext context)
        {
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            var authorId = context.Users.Where(x => x.UserName == "Admin").FirstOrDefault().Id;

#if DEBUG
            var num = 47;
            for (int i = 1; i < num; i++)
            {
                CreateSampleQuiz(
                    context,
                    i,
                    authorId,
                    num - 1,
                    3,
                    3,
                    3,
                    createdDate.AddDays(-num));
            }
#endif
            EntityEntry<Quiz> e1 = context.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Are you more light or dark side of the force?",
                Description = "Star Wars personality test",
                Text = @"Choose wisely you must, young padawan: this test will prove if your will is strong qnough to adhere to the principles of the light side of the Force " +
                "or if you're fated to embrace the dark side. If you want to become a true JEDI, you can't possibly miss this!",

                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
            });

            EntityEntry<Quiz> e2 = context.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "GenX, GenY or GenZ?",
                Description = "Find out what decade most represents you",
                Text = @"Do you feel comfortable in your generation? What year should you have been born in? Here's a bunch of questions that will help you to find out!",

                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModified = lastModifiedDate
            });
        }
        #endregion

        #region Utility Methods
        private static void CreateSampleQuiz(
            ApplicationDbContext context,
            int num,
            string authorId,
            int viewCount,
            int numberOfQuestions,
            int numberOfAnswersPerQuestion,
            int numberOfResults,
            DateTime createdDate)
        {
            var quiz = new Quiz()
            {
                UserId = authorId,
                Title = "Quiz " + num + "Title",
                Description = "This is a sample description for quiz " + num,
                Text = "This is a sample quiz created by the DbSeeder class for testing purposes.",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModified = createdDate
            };

            context.Quizzes.Add(quiz);
            context.SaveChanges();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                var question = new Question()
                {
                    QuizId = quiz.Id,
                    Text = "this is a sample question created by the DbSeeder class for testing purposes",
                    CreatedDate = createdDate,
                    LastModified = createdDate
                };

                context.Questions.Add(question);
                context.SaveChanges();

                for (int i2 = 0; i2 < numberOfAnswersPerQuestion; i2++)
                {
                    var e2 = context.Answers.Add(new Answer()
                    {
                        QuestionId = question.Id,
                        Text = "This is a sample answer created by DbSeeder",
                        Value = i2,
                        CreatedDate = createdDate,
                        LastModified = createdDate
                    });
                }
            }

            for (int i = 0; i < numberOfResults; i++)
            {
                context.Results.Add(new Result()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample result created by the DbSeeder class for testing purposes.",
                    MinValue = 0,
                    MaxValue = numberOfAnswersPerQuestion * 2,
                    CreatedDate = createdDate,
                    LastModified = createdDate
                });
            }
            context.SaveChanges();
        }
        #endregion
    }
}
