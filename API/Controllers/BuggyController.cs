using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        public StoreContext _context;

        public BuggyController(StoreContext context)
        {
            this._context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() {
           var things = _context.Products.Find(42);

           if (things == null)
           {
            return NotFound(new ApiResponse(404));
           }
           return NotFound(new ApiResponse(401));
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError() {
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest() {
            return NotFound(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequet(int id) {
            return BadRequest();
        }
    }
}