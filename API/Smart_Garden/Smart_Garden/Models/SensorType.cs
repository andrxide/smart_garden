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
    public SensorType(string id)
    {
        SqlCommand command = new SqlCommand("execute pa_sensorType @ID");
        command.Parameters.AddWithValue("@ID", id);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Id = row["st_ID"].ToString();
            Description = row["st_description"].ToString();
            Unit = row["st_unit"].ToString();
            Icon = row["st_icon"].ToString();
        }
        else throw new RecordNotFoundException(id);
    }
    #endregion
}
