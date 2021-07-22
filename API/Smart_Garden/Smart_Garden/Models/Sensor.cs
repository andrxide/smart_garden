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
    #endregion

    #region constructors
    public Sensor(string id, SensorType type, string name, float minValue)
    {
        Id = id;
        Type = type;
        Name = name;
    }
    public Sensor()
    {
        Id = "";
        Type = new SensorType();
        Name = "";
    }
    public Sensor(string id)
    {
        SqlCommand command = new SqlCommand("execute pa_sensor @ID");
        command.Parameters.AddWithValue("@ID", id);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Id = row["s_ID"].ToString();
            Type = new SensorType(row["s_type"].ToString());
            Name = row["s_name"].ToString();
        }
        else throw new RecordNotFoundException(id);
    }
    #endregion
}
