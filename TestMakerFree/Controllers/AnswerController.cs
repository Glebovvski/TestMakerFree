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
    public class AnswerController : Controller
    {
        #region RESTful conventions method
        /// <summary>
        /// Retrieves the answer with the given {id}
        /// </summary>
        /// <param name="Id">The ID of an existing answer</param>
        /// <returns>the Answer with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("not implemented yet");
        }

        /// <summary>
        /// Adds a new Answer to the database
        /// </summary>
        /// <param name="vm">The answer viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post(AnswerViewModel vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the answer with the given {id}
        /// </summary>
        /// <param name="vm">The answer viewmodel containing the data to edit</param>
        [HttpPost]
        public IActionResult Put(AnswerViewModel vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the answer with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing answer</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>();

            sampleAnswers.Add(new AnswerViewModel()
            {
                Id = 1,
                QuestionId = questionId,
                Text = "Friends and family",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= 5; i++)
            {
                sampleAnswers.Add(new AnswerViewModel()
                {
                    Id = i,
                    QuestionId = questionId,
                    Text = "Sample answer" + i,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleAnswers,
                new Newtonsoft.Json.JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
        }
    }
}
