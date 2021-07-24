using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


public class Current : Reading
{
    public Current()
    {
        Timestamp = null;
        Value = null;
    }
    public Current(string sensorId, string gardenId)
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
    }
}
