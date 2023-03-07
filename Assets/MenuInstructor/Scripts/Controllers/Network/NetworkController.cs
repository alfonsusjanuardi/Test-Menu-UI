using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Npgsql;

public class NetworkController : MonoBehaviour
{
    private static string constr = @"Server=localhost;Database=db_menu_instructor;User Id=postgres;Password=admin;";

    private void Start()
    {
        Read();
    }

    private static void Read()
    { 
        // for the connection to
        // sql server database
        NpgsqlConnection conn;
 
        // Data Source is the name of the
        // server on which the database is stored.
        // The Initial Catalog is used to specify
        // the name of the database
        // The UserID and Password are the credentials
        // required to connect to the database.
 
        conn = new NpgsqlConnection(constr);
 
        // to open the connection
        conn.Open();
 
        // use to perform read and write
        // operations in the database
        NpgsqlCommand cmd;
 
        // use to read a row in
        // table one by one
        NpgsqlDataReader dreader;
 
        // to store SQL command and
        // the output of SQL command
        string sql, output = "";
 
         // use to fetch rows from demo table
        sql = "Select * from scenario";
 
        // to execute the sql statement
        cmd = new NpgsqlCommand(sql, conn);
 
        // fetch all the rows
        // from the demo table
        dreader = cmd.ExecuteReader();
 
        // for one by one reading row
        while (dreader.Read()) {
            output = output + dreader.GetValue(0) + " - " +
                                dreader.GetValue(1) + "\n";
        }
 
        // to display the output
        Debug.Log(output);
 
        // to close all the objects
        dreader.Close();
        cmd.Dispose();
        conn.Close();
    }
}
