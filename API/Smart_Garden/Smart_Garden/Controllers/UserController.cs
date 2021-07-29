using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Smart_Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
         // GET api/<UserController>
        [HttpGet("{username}/{password}")]
        public ActionResult Get(string username, string password)
        {
            try
            {
                UserViewModel vm = new UserViewModel
                {
                    Status = 0,
                    User = new User(username, password)
                };
                return Ok(vm);
            }
            catch (UserNotFoundException e)
            {
                ErrorViewModel evm = new ErrorViewModel
                {
                    Status = 1,
                    ErrorMessage = e.Message
                };
                return Ok(evm);
            }
        }

        // PUT api/<UserController>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
