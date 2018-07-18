using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMakerFree.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        #region RESTful conventions method
        /// <summary>
        /// Retrieves the result with the given {id}
        /// </summary>
        /// <param name="Id">The ID of an existing resukt</param>
        /// <returns>the result with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("not implemented yet");
        }

        /// <summary>
        /// Adds a new result to the database
        /// </summary>
        /// <param name="vm">The result viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post(ResultViewModel vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the result with the given {id}
        /// </summary>
        /// <param name="vm">The result viewmodel containing the data to edit</param>
        [HttpPost]
        public IActionResult Put(ResultViewModel vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the result with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing result</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion


        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<ResultViewModel>();

            sampleQuestions.Add(new ResultViewModel()
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleQuestions.Add(new ResultViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = "Sample question " + i,
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
