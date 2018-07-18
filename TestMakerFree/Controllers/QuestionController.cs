using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFree.Data;
using TestMakerFree.Data.Models;
using TestMakerFree.ViewModels;

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        #region Private Fields
        private ApplicationDbContext _context;
        #endregion

        #region Constructor
        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region RESTful conventions method
        /// <summary>
        /// Retrieves the question with the given {id}
        /// </summary>
        /// <param name="Id">The ID of an existing question</param>
        /// <returns>the question with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = _context.Questions.Where(i => i.Id == id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Question ID {0} has not been found", id)
                });
            }

            return new JsonResult(question.Adapt<QuestionViewModel>(), new Newtonsoft.Json.JsonSerializerSettings(){
                Formatting = Newtonsoft.Json.Formatting.Indented
            });
        }

        /// <summary>
        /// Adds a new question to the database
        /// </summary>
        /// <param name="vm">The question viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post(QuestionViewModel vm)
        {
            if (vm == null) return new StatusCodeResult(500);

            var question = vm.Adapt<Question>();

            question.QuizId = vm.QuizId;
            question.Text = vm.Text;
            question.Notes = vm.Notes;
            question.CreatedDate = vm.CreatedDate;
            question.LastModified = vm.CreatedDate;

            _context.Questions.Add(question);
            _context.SaveChanges();

            return new JsonResult(question.Adapt<QuestionViewModel>(), new Newtonsoft.Json.JsonSerializerSettings(){
                Formatting = Newtonsoft.Json.Formatting.Indented
            });
        }

        /// <summary>
        /// Edit the question with the given {id}
        /// </summary>
        /// <param name="vm">The question viewmodel containing the data to edit</param>
        [HttpPost]
        public IActionResult Put([FromBody]QuestionViewModel vm)
        {
            if (vm == null) return new StatusCodeResult(500);

            var question = _context.Questions.Where(i => i.Id == vm.Id).FirstOrDefault();

            if (question == null)
            {
                return NotFound();
            }

            question.QuizId = vm.QuizId;
            question.Text = vm.Text;
            question.Notes = vm.Notes;
            question.CreatedDate = vm.CreatedDate;
            question.LastModified = vm.CreatedDate;

            _context.SaveChanges();

            return new JsonResult(question.Adapt<QuestionViewModel>(), new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            });
        }

        /// <summary>
        /// Deletes the question with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing question</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var question = _context.Questions.Where(i => i.Id == id).FirstOrDefault();

            if (question == null)
                return NotFound();
            _context.Questions.Remove(question);
            _context.SaveChanges();

            return Ok();
        }
        #endregion

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = _context.Questions.Where(q => q.QuizId == quizId).ToArray();

            return new JsonResult(sampleQuestions.Adapt<List<QuestionViewModel>>(),
                new Newtonsoft.Json.JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });
        }
    }
}
