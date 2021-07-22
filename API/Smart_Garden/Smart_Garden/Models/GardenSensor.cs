using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public class GardenSensor
{
    #region properties
    public Sensor Info { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    #endregion

    #region constructors
    public GardenSensor()
    {
        Info = new Sensor();
        MinValue = 0;
        MaxValue = 0;
    }
    public GardenSensor(Sensor sensor, decimal minValue, decimal maxValue)
    {
        Info = sensor;
        MinValue = minValue;
        MaxValue = maxValue;
    }
    public GardenSensor(string gardenId, string sensorId)
    {
        SqlCommand command = new SqlCommand("execute pa_getGardenSensor @gardenId,@sensorId");
        command.Parameters.AddWithValue("@gardenId", gardenId);
        command.Parameters.AddWithValue("@sensorId", sensorId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Info = new Sensor(row["gs_sensor"].ToString());
            MinValue = (decimal)row["gs_minValue"];
            MaxValue = (decimal)row["gs_maxValue"];
        }
        else throw new RecordNotFoundException();
    }
    #endregion

    #region methods
    public static List<GardenSensor> GetAll(string gardenId)
    {
        List<GardenSensor> list = new List<GardenSensor>();
        SqlCommand command = new SqlCommand("execute pa_garden_sensor @gardenId");
        command.Parameters.AddWithValue("@gardenId", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count < 1) throw new RecordNotFoundException();
        foreach (DataRow row in table.Rows)
        {
            list.Add(new GardenSensor(new Sensor(row["gs_sensor"].ToString()), (decimal)row["gs_minValue"], (decimal)row["gs_maxValue"]));
        }
        return list;
    }
    #endregion
}
