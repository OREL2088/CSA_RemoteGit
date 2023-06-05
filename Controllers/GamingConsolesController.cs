using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ClientServe.Models;
using ClientServe.Storage;

namespace ClientServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamingConsolesController : ControllerBase
    {
        private static IStorage<GamingСonsoles> _Console = new MemCache();

        [HttpGet]
        public ActionResult<IEnumerable<GamingСonsoles>> Get()
        {
            return Ok(_Console.All);
        }

        [HttpGet("{id}")]
        public ActionResult<GamingСonsoles> Get(Guid id)
        {
            if (!_Console.Has(id)) return NotFound("No such");

            return Ok(_Console[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GamingСonsoles value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _Console.Add(value);

            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] GamingСonsoles value)
        {
            if (!_Console.Has(id)) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _Console[id];
            _Console[id] = value;

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_Console.Has(id)) return NotFound("No such");

            var valueToRemove = _Console[id];
            _Console.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}
