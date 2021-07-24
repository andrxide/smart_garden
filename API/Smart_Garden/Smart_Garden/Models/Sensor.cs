using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public class Sensor
{
    #region properties
    public string Id { get; set;}
    public SensorType Type { get; set; }
    public string Name { get; set; }
    public Values Values { get; set; }
    #endregion

    #region constructors
    public Sensor(string id, SensorType type, string name, Current current, Values values)
    {
        Id = id;
        Type = type;
        Name = name;
        Values = values;
    }
    public Sensor()
    {
        Id = "";
        Type = new SensorType();
        Name = "";
        Values = Values;
    }
    #endregion

    
}
