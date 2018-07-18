﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFree.Data;
using TestMakerFree.Data.Models;
using TestMakerFree.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        #region Private Fields
        private ApplicationDbContext context;
        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region RESTful conventions method
        /// <summary>
        /// Get: api/quiz/{id}
        /// Retrieves the quiz with a given {id}
        /// </summary>
        /// <param name="id">The ID of an existing quiz</param>
        /// <returns>The Quiz with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = context.Quizzes.Where(x => x.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz id {0} has not been found", id)
                });
            }

            return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// Adds a new quiz to the database
        /// </summary>
        /// <param name="vm">The quiz viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel vm)
        {
            if (vm == null)
                return new StatusCodeResult(500);

            var quiz = new Quiz();

            quiz.Title = vm.Title;
            quiz.Description = vm.Description;
            quiz.Text = vm.Text;
            quiz.Notes = vm.Notes;

            quiz.CreatedDate = DateTime.Now;
            quiz.LastModified = quiz.CreatedDate;

            quiz.UserId = context.Users.Where(x => x.UserName == "Admin").FirstOrDefault().Id;

            context.Quizzes.Add(quiz);
            context.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Edit the quiz with the given {id}
        /// </summary>
        /// <param name="vm">The quiz viewmodel containing the data to edit</param>
        [HttpPost]
        public IActionResult Put([FromBody]QuizViewModel vm)
        {
            if (vm == null)
                return new StatusCodeResult(500);

            var quiz = context.Quizzes.Where(x => x.Id == vm.Id).FirstOrDefault();

            if (quiz == null)
                return NotFound(new
                {
                    Error = String.Format("Quiz {0} ID has not been found", vm.Id)
                });

            quiz.Title = vm.Title;
            quiz.Description = vm.Description;
            quiz.Text = vm.Text;
            quiz.Notes = vm.Notes;

            quiz.LastModified = quiz.CreatedDate;

            context.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Deletes the quiz with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing quiz</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quiz = context.Quizzes.Where(x => x.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            context.Quizzes.Remove(quiz);
            context.SaveChanges();

            return new OkResult();
        }
        #endregion

        #region Attribute-based routing methods
        /// <summary>
        /// GET: api/quiz/latest
        /// Retrieves the {num} latest Quizzes
        /// </summary>
        /// <param name="num">The number of quizzes to retrieve</param>
        /// <returns>the {num} latest quizzes</returns>

        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = context.Quizzes.OrderByDescending(x => x.CreatedDate).Take(num).ToArray();

            return new JsonResult(
                latest.Adapt<List<QuizViewModel>>(), new Newtonsoft.Json.JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        #endregion


        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num=10)
        {
            var byTitle = context.Quizzes.OrderBy(x => x.Title).Take(num).ToArray();

            return new JsonResult(
                byTitle.Adapt<List<QuizViewModel>>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var random = context.Quizzes.OrderBy(x => Guid.NewGuid()).Take(num).ToArray();

            return new JsonResult(
                random.Adapt<List<QuizViewModel>>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
