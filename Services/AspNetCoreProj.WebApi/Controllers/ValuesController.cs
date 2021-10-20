using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Dictionary<int, string> Values = Enumerable.Range(1, 10)
            .Select(i => (Id: i, Value: $"Value-{i}")).ToDictionary(i => i.Id, i => i.Value);

        [HttpGet]
        public IActionResult Get() => Ok(Values.Values);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!Values.ContainsKey(id))
                return NotFound();

            return Ok(Values[id]);
        }

        [HttpGet("Count")]
        public IActionResult Count() => Ok(Values.Count);

        [HttpPost]
        [HttpPost("add")]
        public IActionResult Add(string value)
        {
            var id = Values.Count == 0 ? 1 : Values.Keys.Max() + 1;
            Values[id] = value;

            return CreatedAtAction(nameof(GetById), new { id = id });
        }

        [HttpPut("{id}")]
        public IActionResult Replace(int id, [FromBody] string value)
        {
            if (!Values.ContainsKey(id))
                return NotFound();
            Values[id] = value;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Values.ContainsKey(id))
                return NotFound();
            Values.Remove(id);

            return Ok();
        }
    }
}
