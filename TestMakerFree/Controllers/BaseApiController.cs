using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFree.Data;

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        protected ApplicationDbContext _context { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
        #region Constructor
        public BaseApiController(ApplicationDbContext context)
        {
            _context = context;

            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }
        #endregion
    }
}
