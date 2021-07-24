using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

// https://makolyte.com/system-data-sqlclient-is-missing-in-a-dotnet-core-project/

class SqlServerConnection
{
    private static SqlServerConnection instance;
    private string connectionString = "Data Source=LAPTOP-S1TKJ0FM; Initial Catalog = smartGarden;  Integrated Security = true;";
    private SqlConnection connection;

    // constructor
    private SqlServerConnection()
    {
        if (connection == null) connection = new SqlConnection(connectionString);
    }

    // get connection instance
    public static SqlServerConnection GetConnection()
    {
        try
        {
            if (instance == null) instance = new SqlServerConnection();
            return instance;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.GetType().ToString() + " : " + ex.Message);
        }
        return null;
    }

    // open connection
    private bool OpenConnection()
    {
        bool open = false;
        try
        {
            connection.Open();
            open = true;
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.GetType().ToString() + " : " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetType().ToString() + " : " + e.Message);
        }
        return open;
    }

    // execute query
    public DataTable ExecuteQuery(SqlCommand command)
    {
        DataTable table = new DataTable();
        if(OpenConnection())
        {
            command.Connection = connection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                adapter.Fill(table);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.GetType().ToString() + " : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().ToString() + " : " + e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return table;
    }

    public int ExecuteNonQuery(SqlCommand command)
    {

        int rowsAffected = 0;
        if (OpenConnection())
        {
            command.Connection = connection;
            try
            {
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.GetType().ToString() + " : " + e.Message);
                throw new InsertException(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().ToString() + " : " + e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return rowsAffected;
    }
}
