using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using TestMakerFree.Data;
using TestMakerFree.Data.Models;
using TestMakerFree.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFree.Controllers
{
    public class AnswerController : BaseApiController
    {
        #region Constructor
        public AnswerController(ApplicationDbContext context) : base(context)
        { }
        #endregion

        #region RESTful conventions method
        /// <summary>
        /// Retrieves the answer with the given {id}
        /// </summary>
        /// <param name="Id">The ID of an existing answer</param>
        /// <returns>the Answer with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = _context.Answers.Where(i => i.Id == id).FirstOrDefault();

            if (answer == null)
                return NotFound();

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Adds a new Answer to the database
        /// </summary>
        /// <param name="vm">The answer viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post([FromBody]AnswerViewModel vm)
        {
            if (vm == null)
                return new StatusCodeResult(500);

            var answer = vm.Adapt<Answer>();

            answer.QuestionId = vm.QuestionId;
            answer.Text = vm.Text;
            answer.Notes = vm.Notes;
            answer.CreatedDate = DateTime.Now;
            answer.LastModified = answer.CreatedDate;

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Edit the answer with the given {id}
        /// </summary>
        /// <param name="vm">The answer viewmodel containing the data to edit</param>
        [HttpPut]
        public IActionResult Put([FromBody]AnswerViewModel vm)
        {
            if (vm == null)
                return new StatusCodeResult(500);

            var answer = _context.Answers.Where(i => i.Id == vm.Id).FirstOrDefault();

            if (answer == null)
                return NotFound();

            answer.QuestionId = vm.QuestionId;
            answer.Text = vm.Text;
            answer.Notes = vm.Notes;
            answer.CreatedDate = DateTime.Now;
            answer.LastModified = answer.CreatedDate;
            
            _context.SaveChanges();

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Deletes the answer with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing answer</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var answer = _context.Answers.Where(i => i.Id == id).FirstOrDefault();

            if (answer == null)
                return NotFound();

            _context.Answers.Remove(answer);
            _context.SaveChanges();

            return Ok();
        }
        #endregion

        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleAnswers = _context.Answers.Where(q => q.QuestionId == questionId).ToList();

            return new JsonResult(sampleAnswers.Adapt<List<AnswerViewModel>>(), JsonSettings);
        }
    }
}
