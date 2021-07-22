using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public class Reading
{
    #region properties
    public DateTime Timestamp { get; set; }
    public decimal Value { get; set; }
    #endregion

    #region constructors
    public Reading()
    {
        Timestamp = DateTime.Now;
        Value = 0;
    }
    public Reading(DateTime timestamp, decimal value)
    {
        Timestamp = timestamp;
        Value = value;
    }
    public Reading(string sensorId, string gardenId)
    {
        SqlCommand command = new SqlCommand("execute pa_getLastReading @sensorId, @gardenId");
        command.Parameters.AddWithValue("@gardenId", gardenId);
        command.Parameters.AddWithValue("@sensorId", sensorId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Timestamp = DateTime.Parse(row["r_timestamp"].ToString());
            Value = (decimal)row["r_value"];
        }
        else throw new RecordNotFoundException();
    }
    #endregion

    #region methods
    public static List<Reading> GetDaily(string sensorId, string gardenId)
    {
        List<Reading> list = new List<Reading>();
        SqlCommand command = new SqlCommand("execute pa_getReadings @sensorId,@gardenId");
        command.Parameters.AddWithValue("@sensorId", sensorId);
        command.Parameters.AddWithValue("@gardenId", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Reading(DateTime.Parse(row["r_timestamp"].ToString()), (decimal)row["r_value"]));
        }
        return list;
    }
    public static List<Reading> GetHourly(string sensorId, string gardenId)
    {
        List<Reading> list = new List<Reading>();
        SqlCommand command = new SqlCommand("execute pa_getReadingsHourly @sensorId,@gardenId");
        command.Parameters.AddWithValue("@sensorId", sensorId);
        command.Parameters.AddWithValue("@gardenId", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new Reading(DateTime.Parse(row["r_timestamp"].ToString()), (decimal)row["r_value"]));
        }
        return list;
    }
    #endregion
}
