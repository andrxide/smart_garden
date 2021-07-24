using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class SensorHistory : Sensor
{
    public List<Reading> Readings { get; set; }

    #region constructors
    public SensorHistory()
    {
        Id = "";
        Type = new SensorType();
        Name = "";
        Readings = new List<Reading>();
        Values = Values;
    }
    public SensorHistory(string id, SensorType type, string name, List<Reading> readings, Values values)
    {
        Id = id;
        Type = type;
        Name = name;
        Readings = readings;
        Values = values;
    }
    #endregion

    #region methods
    public static List<SensorHistory> GetDailyHistory(string gardenId)
    {
        List<SensorHistory> list = new List<SensorHistory>();
        SqlCommand command = new SqlCommand("EXEC pa_getAllSensors @gardenId");
        command.Parameters.AddWithValue("@gardenId", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new SensorHistory(row["s_ID"].ToString(), new SensorType(row["st_ID"].ToString(), row["st_description"].ToString(), row["st_unit"].ToString(), row["st_icon"].ToString()), row["s_name"].ToString(), Reading.GetDaily(row["s_ID"].ToString(), gardenId), new Values((decimal)row["minValue"], (decimal)row["maxValue"])));
        }
        return list;
    }
    public static List<SensorHistory> GetHourlyHistory(string gardenId)
    {
        List<SensorHistory> list = new List<SensorHistory>();
        SqlCommand command = new SqlCommand("EXEC pa_getAllSensors @gardenId");
        command.Parameters.AddWithValue("@gardenId", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        foreach (DataRow row in table.Rows)
        {
            list.Add(new SensorHistory(row["s_ID"].ToString(), new SensorType(row["st_ID"].ToString(), row["st_description"].ToString(), row["st_unit"].ToString(), row["st_icon"].ToString()), row["s_name"].ToString(), Reading.GetHourly(row["s_ID"].ToString(), gardenId), new Values((decimal)row["minValue"], (decimal)row["maxValue"])));
        }
        return list;
    }
    #endregion
}
