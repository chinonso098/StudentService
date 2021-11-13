using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.DataService;
using StudentService.DomainObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly StudentServiceContext _dbContext;

        public AddressController(StudentServiceContext injectedSSC)
        {
            this._dbContext = injectedSSC;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Address> GetAddressById(int id) 
        {
            if (_dbContext.Addresses.Find(id) == null)
                return NotFound();
            else
                return Ok(_dbContext.Addresses.Find(id));
        }

        [HttpGet("GetAddressByStudentId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Address> GetAddressByStudentId(int id)
        {
            if (_dbContext.Addresses.Where(a => a.StudentID == id).Count()==0)
                return NotFound();
            else
                return Ok(_dbContext.Addresses.Where(a => a.StudentID == id).First());
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostAddress(Address address) 
        {
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAddressById), new { id = address.AddressID }, address);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PutAddress(Address address) 
        {

            if (_dbContext.Addresses.Find(address.AddressID) == null)
                return NotFound();

            else
            {
                Address currentAddress = _dbContext.Addresses.Find(address.AddressID);

                currentAddress.AddressID = address.AddressID;
                currentAddress.StudentID = address.StudentID;
                currentAddress.StreetOne = address.StreetOne;
                currentAddress.StreetTwo = address.StreetTwo;
                currentAddress.City = address.City;
                currentAddress.StateID = address.StateID;
                currentAddress.ZipCode = address.ZipCode;

                _dbContext.Entry(currentAddress).State = EntityState.Modified;

                try 
                {
                    _dbContext.SaveChanges();
                }
                catch(DbUpdateException e) 
                {
                    Console.WriteLine("e" + e.Message);
                    return BadRequest();
                }

                return NoContent();
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteAddress(int id) 
        {
            if (_dbContext.Addresses.Find(id) == null)
                return NotFound();
            else 
            {
                var address = _dbContext.Addresses.Find(id);
                _dbContext.Addresses.Remove(address);

                _dbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
