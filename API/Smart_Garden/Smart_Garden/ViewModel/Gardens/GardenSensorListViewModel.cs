using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GardenSensorListViewModel : JsonViewModel
{
    public Garden Garden { get; set; }
    public List<GardenSensor> Sensors { get; set; }
}
