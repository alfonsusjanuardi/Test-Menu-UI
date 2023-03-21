using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using Npgsql;

public class SpawnObstacleController : MonoBehaviour
{
    public static SpawnObstacleController instance;
    private string sql;
    [SerializeField] private NetworkController DBConn;

    [Header("UI GameObject")]
    [Space(5)]
    public GameObject obstaclePrefab;
    [SerializeField] private Camera cam;

    [Header("List")]
    [Space(5)]
    public List<loadSpawnObstacles> spawnObstacles = new();
    public List<ListScenario> getScenarios;

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObstacleAtMousePos();
    }

    private void ConnectionDBObscatlePlot()
    {
        sql = "select * from damage_point";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            int id_scenario = reader.GetInt16(0);
            string obstacle_name = (string) reader["obstacle_name"];
            double pos_x = reader.GetDouble(2);
            double pos_y = reader.GetDouble(3);
            double pos_z = reader.GetDouble(4);
            
            int idScenario = int.Parse(ScenarioController.instance.idScenario.text);
            if (id_scenario == idScenario)
            {
                spawnObstacles.Add(new loadSpawnObstacles(){
                    object_name = obstacle_name,
                    pos_x = (float)pos_x,
                        pos_y = (float)pos_y,
                        pos_z = (float)pos_z,
                        obsPloting = Instantiate(obstaclePrefab, new Vector3(((float)pos_x), ((float)pos_y), ((float)pos_z)), Quaternion.identity)
                });
            }
        }

        DBConn.CloseConnection();
    }

    public void LoadObstacles()
    {
        ConnectionDBObscatlePlot();
    }

    private void SpawnObstacleAtMousePos()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var mousePos = Mouse.current.position.ReadValue();

            Ray ray = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Asphalt")
                {
                    var obstacles = new loadSpawnObstacles(){
                        object_name = obstaclePrefab.name,
                        pos_x = hit.point.x,
                        pos_y = hit.point.y,
                        pos_z = hit.point.z,
                        obsPloting = Instantiate(obstaclePrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity)
                    };

                    spawnObstacles.Add(obstacles);
                }
            }
        }
    }

    public void AddSpawnObstacles()
    {
        foreach (loadSpawnObstacles item in spawnObstacles)
        {
            SetData(item.object_name, item.pos_x, item.pos_y, item.pos_z);
        }

        LoadObstacles();
    }

    private void SetData(string object_name, float pos_x, float pos_y, float pos_z)
    {
        string tabelName = "damage_point";
        List<string> columns = new()
        {
            "id_scenario",
            "obstacle_name",
            "pos_x",
            "pos_y",
            "pos_z"
        };
        List<List<string>> values = new()
        {
            new() { SetID().ToString(), "numeric" },
            new() {object_name, "nonNumeric"},
            new() {pos_x.ToString(), "numeric"},
            new() {pos_y.ToString(), "numeric"},
            new() {pos_z.ToString(), "numeric"}
        };

        IDataAdapter adapter = DBConn.setData(tabelName, columns, values);
        DBConn.CloseConnection();
    }

    private int SetID()
    {
        return getScenarios != null ? getScenarios.Count : 0;
    }
}

[System.Serializable]

public class loadSpawnObstacles
{
    public int id_scenario;
    public string object_name, type;
    public float pos_x, pos_y, pos_z;
    public GameObject obsPloting;
}
