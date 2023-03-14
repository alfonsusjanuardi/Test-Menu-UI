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

    private string connectionString;
    public IDataReader reader;
    public IDataAdapter adapter;
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

    private void Start()
    {
        // string tableName = "scenario";
        // List<string> columns = new()
        // {
        //     "id_scenario",
        //     "scenario_name",
        //     "task_name",
        //     "time",
        //     "location",
        //     "information"
        // };
        // List<List<string>> values = new()
        // {
        //     new() { "5", "numeric" },
        //     new() { "ardi", "varchar" },
        //     new() { "tes", "varchar"},
        //     new() { "10.00", "varchar"},
        //     new() { "bandara", "varchar"},
        //     new() { "tes", "varchar"}
        // };

        // IDataAdapter testAdapter = setData(tableName, columns, values);
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

    public IDataAdapter setData(string tableName, List<string> columns, List<List<string>> values)
    {
        if (columns.Count != values.Count)
        {
            Debug.Log("Jumlah tak sama");
            return null;
        }

        string column = string.Empty;
        foreach(string obj in columns)
        {
            if (obj != columns[columns.Count - 1])
                column += obj + ", ";
            else
                column += obj;
        }

        string value = string.Empty;
        foreach(List<string> obj in values)
        {
            string valueContent = obj[0];
            string valueType = obj[1];

            string newVal = valueType != "numeric" ? "\'" + valueContent + "\'" : valueContent;

            if (obj != values[values.Count - 1])
                value += newVal + ", ";
            else
                value += newVal;
        }

        string query = "insert into " + tableName + " (" + column + ")" + " values (" + value + ")";

        return setData(query);
    }

    private IDataAdapter setData(string query)
    {
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;
        
        NpgsqlDataAdapter nAdapter = new();
        nAdapter.InsertCommand = dbcmd as NpgsqlCommand;
        nAdapter.InsertCommand.ExecuteNonQuery();

        adapter = nAdapter;
        return adapter;
    }

    public IDataAdapter editData(string tableName, List<string> columns, List<List<string>> values, string checkParameter, string parameter)
    {
        if (columns.Count != values.Count)
            return null;

        string setValue = string.Empty;
        for (int i = 0; i < columns.Count; i++)
        {
            string valueContent = values[i][0];
            string valueType = values[i][1];

            string newVal = valueType != "numeric" ? "\'" + valueContent + "\'" : valueContent;

            if (i != columns.Count - 1)
                setValue += columns[i] + " = " + newVal + ", ";
            else
                setValue += columns[i] + " = " + newVal;
        }

        string query = "update " + tableName + " set " + setValue + " where " + checkParameter + "='" + parameter + "'";
        return editData(query);
    }

    private IDataAdapter editData(string query)
    {
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();

        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;
        
        NpgsqlDataAdapter nAdapter = new();
        nAdapter.UpdateCommand = dbcmd as NpgsqlCommand;
        nAdapter.UpdateCommand.ExecuteNonQuery();

        adapter = nAdapter;
        return adapter;
    }

    public void CloseConnection()
    {
        reader.Close();
        dbcmd.Dispose();
        dbcon.Close();
    }
}
