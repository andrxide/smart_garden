using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class SensorType
{
    #region properties
    public string Id { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }
    public string Icon { get; set; }

    #endregion

    #region constructors
    public SensorType(string id, string description, string unit, string icon)
    {
        Id = id;
        Description = description;
        Unit = unit;
        Icon = icon;
    }
    public SensorType()
    {
        Id = "";
        Description = "";
        Unit = "";
        Icon = "";
    }
    #endregion
}
