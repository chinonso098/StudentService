using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentService.DataService;
using StudentService.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StateController : ControllerBase
    {
        private readonly StudentServiceContext _dbContext;

        public StateController (StudentServiceContext injectedSSC) 
        {
            this._dbContext = injectedSSC;
        }

        // GET: api/States
        [HttpGet]
        public IEnumerable<State> GetStatesAsync() 
        {
            return  _dbContext.States.ToList();
        }
    
        //GET: api/State
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(State))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<State> GetById(int id)
        {
            if (_dbContext.States.Find(id) == null)
                return NotFound();
            else
             return _dbContext.States.Find(id);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateState(State state)
        {
            _dbContext.States.Add(state);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = state.StateID }, state);
        }
    }
}
