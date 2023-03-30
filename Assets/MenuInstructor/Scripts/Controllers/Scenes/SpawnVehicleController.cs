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
    public GameObject truckTrailerPrefab, motorGraderPrefab, wheelLoaderPrefab;
    [SerializeField] private Camera cam;

    [Header("List")]
    [Space(5)]
    public List<loadSpawnVehicles> spawnVehicles = new();
    public List<ListScenario> getScenarios;

    public bool isActive;

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
        if (isActive)
            SpawnVehicleAtMousePos();
    }

    private void ConnectionDBVehiclePlot()
    {
        sql = "select * from ploting_object";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            int id_scenario = reader.GetInt16(0);
            string vehicle_name = (string) reader["vehicle_name"];
            string type = (string) reader["type"];
            double pos_x = reader.GetDouble(2);
            double pos_y = reader.GetDouble(3);
            double pos_z = reader.GetDouble(4);
            
            int idScenario = int.Parse(ScenarioController.instance.idScenario.text);
            if (id_scenario == idScenario)
            {
                if (type == VehicleRoleController.instance.detailPlotVehicle.text)
                {
                    
                }
                spawnVehicles.Add(new loadSpawnVehicles(){
                    object_name = vehicle_name,
                    pos_x = (float)pos_x,
                        pos_y = (float)pos_y,
                        pos_z = (float)pos_z,
                        obsPloting = Instantiate(truckTrailerPrefab, new Vector3(((float)pos_x), ((float)pos_y), ((float)pos_z)), Quaternion.identity)
                });
            }
        }

        DBConn.CloseConnection();
    }

    public void LoadVehicles()
    {
        ConnectionDBVehiclePlot();
    }

    private void SpawnVehicleAtMousePos()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var mousePos = Mouse.current.position.ReadValue();

            Ray ray = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.collider.tag == "Concrete" && spawnVehicles.Count == 0) {
                    if (VehicleRoleController.instance.detailPlotVehicle.text == "Truck Trailer") {
                        var vehicles = new loadSpawnVehicles{
                            object_name = truckTrailerPrefab.name,
                            pos_x = hit.point.x,
                            pos_y = hit.point.y,
                            pos_z = hit.point.z,
                            obsPloting = Instantiate(truckTrailerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity)
                        };
                        
                        spawnVehicles.Add(vehicles);
                    } else if (VehicleRoleController.instance.detailPlotVehicle.text == "Excavator") {
                        
                    } else if (VehicleRoleController.instance.detailPlotVehicle.text == "Motor Grader") {
                        var vehicles = new loadSpawnVehicles{
                            object_name = motorGraderPrefab.name,
                            pos_x = hit.point.x,
                            pos_y = hit.point.y,
                            pos_z = hit.point.z,
                            obsPloting = Instantiate(motorGraderPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity)
                        };
                        
                        spawnVehicles.Add(vehicles);
                    } else if (VehicleRoleController.instance.detailPlotVehicle.text == "Skid Steer Loader") {
                        
                    } else if (VehicleRoleController.instance.detailPlotVehicle.text == "Marking Machine") {
                        
                    } else if (VehicleRoleController.instance.detailPlotVehicle.text == "Wheel Loader") {
                        var vehicles = new loadSpawnVehicles{
                            object_name = wheelLoaderPrefab.name,
                            pos_x = hit.point.x,
                            pos_y = hit.point.y,
                            pos_z = hit.point.z,
                            obsPloting = Instantiate(wheelLoaderPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity)
                        };
                        
                        spawnVehicles.Add(vehicles);
                    }
                } else {
                    Debug.Log("Objek alat berat sudah lebih dari 1 kali");
                }
            }
        }
    }

    public void AddSpawnVehicles()
    {
        LoadVehicles();
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
