using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ClientServe.Models;

namespace ClientServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamingConsolesController : ControllerBase
    {
        private static List<GamingСonsoles> _Console = new List<GamingСonsoles>();

        [HttpGet]
        public ActionResult<IEnumerable<GamingСonsoles>> Get()
        {
            return _Console;
        }

        [HttpGet("{id}")]
        public ActionResult<GamingСonsoles> Get(int id)
        {
            if (_Console.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            return _Console[id];
        }

        [HttpPost]
        public void Post([FromBody] GamingСonsoles value)
        {
            _Console.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] GamingСonsoles value)
        {
            if (_Console.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            _Console[id] = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_Console.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            _Console.RemoveAt(id);
        }
    }
}
