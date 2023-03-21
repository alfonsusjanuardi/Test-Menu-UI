using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using Npgsql;


public class SpawnVehicleController : MonoBehaviour
{
    public static SpawnVehicleController instance;
    private string sql;
    [SerializeField] private NetworkController DBConn;

    [Header("UI GameObject")]
    [Space(5)]
    public GameObject obstaclePrefab;
    [SerializeField] private Camera cam;

    [Header("List")]
    [Space(5)]
    public List<loadSpawnVehicles> spawnVehicles = new();
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
        
    }

    private void ConnectionDBVehiclePlot()
    {
        sql = "select * from ploting_object";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            int id_scenario = reader.GetInt16(0);
            string vehicle_name = (string) reader["vehicle_name"];
            double pos_x = reader.GetDouble(2);
            double pos_y = reader.GetDouble(3);
            double pos_z = reader.GetDouble(4);
            
            int idScenario = int.Parse(ScenarioController.instance.idScenario.text);
            if (id_scenario == idScenario)
            {
                spawnVehicles.Add(new loadSpawnVehicles(){
                    object_name = vehicle_name,
                    pos_x = (float)pos_x,
                        pos_y = (float)pos_y,
                        pos_z = (float)pos_z,
                        obsPloting = Instantiate(obstaclePrefab, new Vector3(((float)pos_x), ((float)pos_y), ((float)pos_z)), Quaternion.identity)
                });
            }
        }

        DBConn.CloseConnection();
    }

    public void LoadVehicles()
    {
        ConnectionDBVehiclePlot();
    }
}

[System.Serializable]

public class loadSpawnVehicles
{
    public int id_scenario;
    public string object_name, type;
    public float pos_x, pos_y, pos_z;
    public GameObject obsPloting;
}
