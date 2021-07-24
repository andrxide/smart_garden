using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public class Reading
{
    #region properties
    public DateTime? Timestamp { get; set; }
    public decimal? Value { get; set; }
    #endregion

    #region constructors
    public Reading()
    {
        Timestamp = null;
        Value = null;
    }
    public Reading(DateTime timestamp, decimal value)
    {
        Timestamp = timestamp;
        Value = value;
    }    
    #endregion

    #region methods
    public static List<Reading> GetDaily(string sensorId, string gardenId)
    {
        List<Reading> list = new List<Reading>();
        SqlCommand command = new SqlCommand("execute pa_getReadingsDaily @sensorId,@gardenId");
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
