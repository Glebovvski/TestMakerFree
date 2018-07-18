using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFree.ViewModels;

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        #region RESTful conventions method
        /// <summary>
        /// Retrieves the question with the given {id}
        /// </summary>
        /// <param name="Id">The ID of an existing question</param>
        /// <returns>the question with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("not implemented yet");
        }

        /// <summary>
        /// Adds a new question to the database
        /// </summary>
        /// <param name="vm">The question viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post(QuestionViewModel vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the question with the given {id}
        /// </summary>
        /// <param name="vm">The question viewmodel containing the data to edit</param>
        [HttpPost]
        public IActionResult Put(QuestionViewModel vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the question with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing question</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>();

            sampleQuestions.Add(new QuestionViewModel()
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new QuestionViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = @"Sample question "+i,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleQuestions,
                new Newtonsoft.Json.JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
        }
    }
}
