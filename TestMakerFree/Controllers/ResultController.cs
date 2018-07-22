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
    public class ResultController : BaseApiController
    {
        #region Constructor
        public ResultController(ApplicationDbContext context) : base(context)
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
            var result = _context.Results.Where(i => i.Id == id).FirstOrDefault();

            if (result == null)
                return NotFound();

            return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Adds a new Answer to the database
        /// </summary>
        /// <param name="vm">The answer viewmodel containing the data to insert</param>
        [HttpPost]
        public IActionResult Post([FromBody]ResultViewModel vm)
        {
            if (vm == null)
                return new StatusCodeResult(500);

            var result = vm.Adapt<Result>();
            
            result.CreatedDate = DateTime.Now;
            result.LastModified = result.CreatedDate;

            _context.Results.Add(result);
            _context.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Edit the answer with the given {id}
        /// </summary>
        /// <param name="vm">The answer viewmodel containing the data to edit</param>
        [HttpPut]
        public IActionResult Put([FromBody]ResultViewModel vm)
        {
            if (vm == null)
                return new StatusCodeResult(500);

            var result = _context.Results.Where(i => i.Id == vm.Id).FirstOrDefault();

            if (result == null)
                return NotFound();

            result.QuizId = vm.QuizId;
            result.Text = vm.Text;
            result.Notes = vm.Notes;
            result.MinValue = vm.MinValue;
            result.MaxValue = vm.MaxValue;
            result.CreatedDate = DateTime.Now;
            result.LastModified = result.CreatedDate;

            _context.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Deletes the answer with the given {id} from the database
        /// </summary>
        /// <param name="id">The ID of an existing answer</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Results.Where(i => i.Id == id).FirstOrDefault();

            if (result == null)
                return NotFound();

            _context.Results.Remove(result);
            _context.SaveChanges();

            return Ok();
        }
        #endregion

        [HttpGet("All/{questionId}")]
        public IActionResult All(int quizId)
        {
            var results = _context.Results.Where(q => q.QuizId == quizId).ToList();

            return new JsonResult(results.Adapt<List<ResultViewModel>>(), JsonSettings);
        }
    }
}
