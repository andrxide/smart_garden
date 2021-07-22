using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Garden.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        [HttpGet("last/{gardenId}/{sensorId}")]
        public ActionResult Get(string gardenId, string sensorId)
        {
            try
            {
                ReadingViewModel vm = new ReadingViewModel();
                vm.Status = 0;
                vm.Garden = new Garden(gardenId);
                vm.Sensor = new GardenSensor(gardenId, sensorId);
                vm.Reading = new Reading(sensorId, gardenId);
                return Ok(vm);
            }
            catch (RecordNotFoundException e)
            {
                ErrorViewModel vm = new ErrorViewModel();
                vm.Status = 1;
                vm.ErrorMessage = e.Message;
                return Ok(vm);
            }
        }
        [HttpGet("hourly/{gardenId}/{sensorId}")]
        public ActionResult GetHourly(string gardenId, string sensorId)
        {
            try
            {
                ReadingsListViewModel vm = new ReadingsListViewModel();
                vm.Status = 0;
                vm.Garden = new Garden(gardenId);
                vm.Sensor = new GardenSensor(gardenId, sensorId);
                vm.Readings = Reading.GetHourly(sensorId, gardenId);
                return Ok(vm);
            }
            catch (RecordNotFoundException e)
            {
                ErrorViewModel vm = new ErrorViewModel();
                vm.Status = 1;
                vm.ErrorMessage = e.Message;
                return Ok(vm);
            }
        }
        [HttpGet("daily/{gardenId}/{sensorId}")]
        public ActionResult GetDaily(string gardenId, string sensorId)
        {
            try
            {
                ReadingsListViewModel vm = new ReadingsListViewModel();
                vm.Status = 0;
                vm.Garden = new Garden(gardenId);
                vm.Sensor = new GardenSensor(gardenId, sensorId);
                vm.Readings = Reading.GetDaily(sensorId, gardenId);
                return Ok(vm);
            }
            catch (RecordNotFoundException e)
            {
                ErrorViewModel vm = new ErrorViewModel();
                vm.Status = 1;
                vm.ErrorMessage = e.Message;
                return Ok(vm);
            }
        }
    }
}
