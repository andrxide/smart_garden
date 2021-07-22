using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


public class User
{
    #region properties

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    #endregion

    #region constructors

    public User()
    {
        this.Id = 0;
        this.Username = "";
        this.Password = "";
    }

    public User(int id, string username, string password)
    {
        this.Id = id;
        this.Username = username;
        this.Password = password;
    }

    public User(string username,string password)
    {
        SqlCommand command = new SqlCommand("Select u_ID, u_username, u_password from users where u_username = @USERNAME and u_password = @PASSWORD");
        command.Parameters.AddWithValue("@USERNAME", username);
        command.Parameters.AddWithValue("@PASSWORD", password);
        DataTable table = SqlServerConnection.GetConnection().ExecuteQuery(command);
        if (table.Rows.Count > 0)
        {
            DataRow row = table.Rows[0];
            Id = (int)row["u_ID"];
            Username = row["u_username"].ToString();
            Password = row["u_password"].ToString();
        }
        else throw new UserNotFoundException(username, password);
    }

    #endregion

    #region methods

    public void Add(string username, string password)
    {
        SqlCommand command = new SqlCommand("insert into users (u_username, u_password) values (@USERNAME, @PASSWORD)");
        command.Parameters.AddWithValue("@USERNAME", username);
        command.Parameters.AddWithValue("@PASSWORD", password);
        int rowsAffected = SqlServerConnection.GetConnection().ExecuteNonQuery(command);
        if (rowsAffected > 0)
        {
            Console.WriteLine(rowsAffected + "row(s) affected");
        }
    }

    #endregion
}
