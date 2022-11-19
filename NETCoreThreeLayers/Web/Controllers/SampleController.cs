using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        // GET: api/Sample
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var sid = claimsIdentity?.FindFirst(ClaimTypes.Sid);
            var name = claimsIdentity?.FindFirst(ClaimTypes.Name);

            var resultSid = sid?.Value is null ? "null" : sid.Value;
            var resultName = name?.Value is null ? "null" : name.Value;

            return new string[] { resultSid.ToString(), resultName.ToString() };
        }

        // GET: api/Sample/5
        [Authorize]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sample
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Sample/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Sample/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
