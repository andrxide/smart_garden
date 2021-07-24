using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public class SensorCurrent : Sensor
{
    public Current Current { get; set; }

    #region constructors
    public SensorCurrent(string id, SensorType type, string name, Current current, Values values)
    {
        Id = id;
        Type = type;
        Name = name;
        Current = current;
        Values = values;
    }
    public SensorCurrent()
    {
        Id = "";
        Type = new SensorType();
        Name = "";
        Current = new Current();
        Values = Values;
    }
    #endregion

    #region methods
    public static List<SensorCurrent> GetAll(string gardenId, Boolean online)
    {
        List<SensorCurrent> list = new List<SensorCurrent>();
        SqlCommand command = new SqlCommand("EXEC pa_getAllSensors @gardenId");
        command.Parameters.AddWithValue("@gardenId", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        foreach (DataRow row in table.Rows)
        {
            Current current = new Current();
            if (online)
            {
                current = new Current(row["s_ID"].ToString(), gardenId);
            }
            list.Add(new SensorCurrent(row["s_ID"].ToString(), new SensorType(row["st_ID"].ToString(), row["st_description"].ToString(), row["st_unit"].ToString(), row["st_icon"].ToString()), row["s_name"].ToString(), current, new Values((decimal)row["minValue"], (decimal)row["maxValue"])));
        }
        return list;
    }
    #endregion
}
