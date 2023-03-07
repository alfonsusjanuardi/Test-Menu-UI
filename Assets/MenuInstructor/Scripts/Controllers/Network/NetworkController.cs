using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

using Npgsql;

public class NetworkController : MonoBehaviour
{
    [Header("Connection")]
    [Space(5)]
    [SerializeField] private string Server;
    [SerializeField] private string Port;
    [SerializeField] private string DatabaseName;
    [SerializeField] private string UserID;
    [SerializeField] private string Password;

    private static string connectionString;
    public IDataReader reader;
    public NpgsqlConnection dbcon;
    public IDbCommand dbcmd;

    void Awake() {
        connectionString = 
            "Server   = " + Server + ";" +
            "Port     = " + Port + ";" +
            "Database = " + DatabaseName + ";" + 
            "User ID  = " + UserID + ";" +
            "Password = " + Password + ";";
    }

    public IDataReader getData(string query)
    {
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        return reader;
    }

    public void CloseConnection()
    {
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }
}
