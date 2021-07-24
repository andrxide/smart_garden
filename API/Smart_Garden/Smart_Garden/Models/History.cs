using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

public class History
{
    #region properties
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Boolean Online { get; set; }
    public List<SensorHistory> Sensors { get; set; }
    #endregion

    #region constructors
    public History()
    {
        Id = "";
        CreatedAt = DateTime.Now;
        Name = "";
        Description = "";
        Online = false;
        Sensors = new List<SensorHistory>();
    }
    public History(string id, DateTime createdAt, string name, string description, Boolean online, List<SensorHistory> sensors)
    {
        Id = id;
        CreatedAt = createdAt;
        Name = name;
        Description = description;
        Online = online;
        Sensors = sensors;
    }
    public History(string gardenId, string range)
    {
        SqlCommand command = new SqlCommand("EXECUTE pa_getGarden @ID");
        command.Parameters.AddWithValue("@ID", gardenId);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count < 1) throw new RecordNotFoundException();
            if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Id = row["g_ID"].ToString();
            CreatedAt = DateTime.Parse(row["g_createdAt"].ToString());
            Name = row["g_name"].ToString();
            Description = row["g_description"].ToString();
            if ((int)row["g_online"] == 1) Online = true;
            else Online = false;
            if (range == "daily")
            {
                Sensors = SensorHistory.GetDailyHistory(row["g_ID"].ToString());
            }
            else if (range == "hourly")
            {
                Sensors = SensorHistory.GetHourlyHistory(row["g_ID"].ToString());
            }
        }
    }
    #endregion
}
