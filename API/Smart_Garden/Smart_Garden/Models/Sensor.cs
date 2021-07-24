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
    /*public Sensor(string sensorId, string gardenId)
    {
        SqlCommand command = new SqlCommand("execute pa_sensor @ID");
        command.Parameters.AddWithValue("@ID", sensorId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Id = row["s_ID"].ToString();
            Type = new SensorType(row["s_type"].ToString());
            Name = row["s_name"].ToString();
            Current = new Reading(sensorId, gardenId);
        }
        else throw new RecordNotFoundException(sensorId);
    }*/
    #endregion

    
}
