using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardenController : ControllerBase
    {
        [HttpGet("{userId}")]
        public ActionResult Get(int userId)
        {
            try
            {
                GardenListViewModel vm = new GardenListViewModel();
                vm.Status = 0;
                vm.Gardens = Garden.GetAll(userId: userId);
                return Ok(vm);
            }
            catch (UserNotFoundException e)
            {
                ErrorViewModel vm = new ErrorViewModel();
                vm.Status = 1;
                vm.ErrorMessage = e.Message;
                return Ok(vm);
            }
        }
    }
}
