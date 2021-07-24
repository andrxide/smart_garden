using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class Garden
{
    #region properties
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Boolean Online { get; set; }
    public List<SensorCurrent> Sensors { get; set; }
    #endregion

    #region constructors
    public Garden()
    {
        Id = "";
        CreatedAt = DateTime.Now;
        Name = "";
        Description = "";
        Online = false;
        Sensors = new List<SensorCurrent>();
    }
    public Garden(string id, DateTime createdAt, string name, string description, Boolean online, List<SensorCurrent> sensors)
    {
        Id = id;
        CreatedAt = createdAt;
        Name = name;
        Description = description;
        Online = online;
        Sensors = sensors;
    }
    /*public Garden(string id)
    {
        SqlCommand command = new SqlCommand("EXECUTE pa_getGarden @ID");
        command.Parameters.AddWithValue("@ID", id);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Id = row["g_ID"].ToString();
            CreatedAt = DateTime.Parse(row["g_createdAt"].ToString());
            Name = row["g_name"].ToString();
            Description = row["g_description"].ToString();
            if ((int)row["g_online"] == 1) Online = true;
            else Online = false;
            Sensors = Sensor.GetAll(row["g_ID"].ToString());
        }
    }*/
    #endregion

    #region methods
    public static List<Garden> GetAll(int userId)
    {
        List<Garden> list = new List<Garden>();
        SqlCommand command = new SqlCommand("execute pa_gardensUser @USERID");
        command.Parameters.AddWithValue("@USERID", userId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count < 1) throw new UserNotFoundException(userId);
        foreach (DataRow row in table.Rows)
        {
            var online = false;
            if ((int)row["g_online"] == 1) online = true;
            list.Add(new Garden(row["g_ID"].ToString(), DateTime.Parse(row["g_createdAt"].ToString()), row["g_name"].ToString(), row["g_description"].ToString(),online, SensorCurrent.GetAll(row["g_ID"].ToString(), online)));
        }
        return list;
    }
    #endregion
}
