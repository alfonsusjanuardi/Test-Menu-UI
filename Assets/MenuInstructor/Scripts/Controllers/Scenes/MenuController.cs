using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private GameObject btnScenarioList;
    [SerializeField] private GameObject btnHeavyEquipment;
    [SerializeField] private GameObject btnConsoleSetting;
    [SerializeField] private GameObject panelMainMenu, panelHome;
    [SerializeField] private GameObject panelScenarioList, panelAddScenario, panelEditScenario;
    [SerializeField] private GameObject panelAddDamagePoint, panelEditDamagePoint;
    [SerializeField] private GameObject panelAddRatingParameter, panelEditRatingParameter;
    [SerializeField] private GameObject panelAddDetailParameter, panelEditDetailParameter;
    [SerializeField] private GameObject panelAddVehicleRole, panelEditVehicleRole;
    [SerializeField] private GameObject panelAddPlotLocation, panelEditPlotLocation;
    [SerializeField] private GameObject panelDetailPlotLocation, panelDetailObstaclePlotLocation;
    [SerializeField] private GameObject panelHeavyEquipment;
    [SerializeField] private GameObject panelConsoleSetting;

    //button
    [SerializeField] private GameObject btnBackHomeScenario, btnBackHomeHeavyEquipment, btnBackHomeConsoleSetting, btnBackPlotLocation, btnBackEditPlotLocation, btnBackObstaclePlotLocation, btnBackEditObstaclePlotLocation;

    //camera 
    [SerializeField] private GameObject mainCamera, airportCamera;
    
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        panelMainMenu.SetActive(true);
        panelScenarioList.SetActive(false);
        panelHeavyEquipment.SetActive(false);
        panelConsoleSetting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backHome()
    {
        panelMainMenu.SetActive(true);
        panelScenarioList.SetActive(false);
        panelHeavyEquipment.SetActive(false);
        panelConsoleSetting.SetActive(false);
    }

    public void showScenarioList()
    {
        panelMainMenu.SetActive(false);
        panelScenarioList.SetActive(true);
    }

    public void showAddScenario()
    {
        panelAddScenario.SetActive(true);
        panelScenarioList.SetActive(false);
    }
    public void createScenarioList()
    {
        panelAddScenario.SetActive(false);
        panelScenarioList.SetActive(true);

        ScenarioController.instance.addScenario();
    }

    public void backAddScenario()
    {
        panelAddVehicleRole.SetActive(false);
        panelAddRatingParameter.SetActive(false);
        panelAddDamagePoint.SetActive(false);
        panelAddScenario.SetActive(true);
    }

    public void showAddDamagePoint()
    {
        panelAddDamagePoint.SetActive(true);
        panelAddScenario.SetActive(false);
    }

    public void showDetailObstaclePlotLocation()
    {
        panelDetailObstaclePlotLocation.SetActive(true);
        panelAddDamagePoint.SetActive(false);
        panelHome.SetActive(false);
        btnBackEditObstaclePlotLocation.SetActive(false);
        btnBackObstaclePlotLocation.SetActive(true);
        airportCamera.SetActive(true);
        mainCamera.SetActive(false);
    }

    public void backAddDamagePoint()
    {
        panelAddDamagePoint.SetActive(true);
        panelDetailObstaclePlotLocation.SetActive(false);
        panelMainMenu.SetActive(false);
        panelHome.SetActive(true);
        btnBackEditObstaclePlotLocation.SetActive(false);
        btnBackObstaclePlotLocation.SetActive(false);
        airportCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    public void showAddRatingParameter()
    {
        panelAddRatingParameter.SetActive(true);
        panelAddScenario.SetActive(false);
    }

    public void backAddRatingParameter()
    {
        panelAddDetailParameter.SetActive(false);
        panelAddRatingParameter.SetActive(true);
    }

    public void showAddDetailRatingParameter()
    {
        panelAddDetailParameter.SetActive(true);
        panelAddRatingParameter.SetActive(false);
    }

    public void showAddVehicleRole()
    {
        panelAddVehicleRole.SetActive(true);
        panelAddScenario.SetActive(false);
    }

    public void backAddVehicleRole()
    {
        panelAddVehicleRole.SetActive(true);
        panelAddPlotLocation.SetActive(false);
    }

    public void createVehicleRole()
    {
        VehicleRoleController.instance.addVehicleRole();
    }

    public void showAddPlotLocation()
    {
        panelAddPlotLocation.SetActive(true);
        panelAddVehicleRole.SetActive(false);
    }

    public void showDetailPlotLocation()
    {
        panelDetailPlotLocation.SetActive(true);
        panelAddPlotLocation.SetActive(false);
        panelHome.SetActive(false);
        btnBackEditPlotLocation.SetActive(false);
        btnBackPlotLocation.SetActive(true);
        airportCamera.SetActive(true);
        mainCamera.SetActive(false);
    }

    public void backAddPlotLocation()
    {
        panelAddPlotLocation.SetActive(true);
        panelDetailPlotLocation.SetActive(false);
        panelMainMenu.SetActive(false);
        panelHome.SetActive(true);
        btnBackEditPlotLocation.SetActive(false);
        btnBackPlotLocation.SetActive(false);
        airportCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    public void showEditScenario()
    {
        panelEditScenario.SetActive(true);
        panelScenarioList.SetActive(false);
    }

    public void updateScenarioList()
    {
        panelEditScenario.SetActive(false);
        panelScenarioList.SetActive(true);

        ScenarioController.instance.editScenario();
    }

    public void backEditScenario()
    {
        panelEditVehicleRole.SetActive(false);
        panelEditRatingParameter.SetActive(false);
        panelEditDamagePoint.SetActive(false);
        panelEditScenario.SetActive(true);
    }

    public void showEditDamagePoint()
    {
        panelEditDamagePoint.SetActive(true);
        panelEditScenario.SetActive(false);
    }

    public void showDetailEditObstaclePlotLocation()
    {
        panelEditDamagePoint.SetActive(false);
        panelDetailObstaclePlotLocation.SetActive(true);
        panelHome.SetActive(false);
        btnBackEditObstaclePlotLocation.SetActive(true);
        btnBackObstaclePlotLocation.SetActive(false);
        airportCamera.SetActive(true);
        mainCamera.SetActive(false);

        SpawnObstacleController.instance.LoadObstacles();
    }

    public void backEditDamagePoint()
    {
        panelEditDamagePoint.SetActive(true);
        panelDetailObstaclePlotLocation.SetActive(false);
        panelMainMenu.SetActive(false);
        panelHome.SetActive(true);
        btnBackEditObstaclePlotLocation.SetActive(false);
        btnBackObstaclePlotLocation.SetActive(false);
        airportCamera.SetActive(false);
        mainCamera.SetActive(true);

        for (int i = 0; i < SpawnObstacleController.instance.spawnObstacles.Count; i++)
        {
            Destroy(SpawnObstacleController.instance.spawnObstacles[i].obsPloting);
        }

        SpawnObstacleController.instance.spawnObstacles.Clear();
    }

    public void showEditRatingParameter()
    {
        panelEditRatingParameter.SetActive(true);
        panelEditScenario.SetActive(false);
    }

    public void backEditRatingParameter()
    {
        panelEditDetailParameter.SetActive(false);
        panelEditRatingParameter.SetActive(true);
    }

    public void showEditDetailRatingParameter()
    {
        panelEditDetailParameter.SetActive(true);
        panelEditRatingParameter.SetActive(false);
    }

    public void showEditVehicleRole()
    {
        panelEditVehicleRole.SetActive(true);
        panelEditScenario.SetActive(false);
    }

    public void backEditVehicleRole()
    {
        panelEditVehicleRole.SetActive(true);
        panelEditPlotLocation.SetActive(false);
    }

    public void showEditPlotLocation()
    {
        panelEditPlotLocation.SetActive(true);
        panelEditVehicleRole.SetActive(false);
    }

    public void showDetailEditPlotLocation()
    {
        panelDetailPlotLocation.SetActive(true);
        panelEditPlotLocation.SetActive(false);
        panelHome.SetActive(false);
        btnBackEditPlotLocation.SetActive(true);
        btnBackPlotLocation.SetActive(false);
        airportCamera.SetActive(true);
        mainCamera.SetActive(false);

        SpawnVehicleController.instance.LoadVehicles();
    }

    public void backEditPlotLocation()
    {
        panelEditPlotLocation.SetActive(true);
        panelDetailPlotLocation.SetActive(false);
        panelMainMenu.SetActive(false);
        panelHome.SetActive(true);
        btnBackEditPlotLocation.SetActive(false);
        btnBackPlotLocation.SetActive(false);
        airportCamera.SetActive(false);
        mainCamera.SetActive(true);

        for (int i = 0; i < SpawnVehicleController.instance.spawnVehicles.Count; i++)
        {
            Destroy(SpawnVehicleController.instance.spawnVehicles[i].obsPloting);
        }
        
        SpawnVehicleController.instance.spawnVehicles.Clear();
    }

    public void backScenarioList()
    {
        panelScenarioList.SetActive(true);
        panelAddScenario.SetActive(false);
        panelEditScenario.SetActive(false);
    }

    public void showHeavyEquipment()
    {
        panelMainMenu.SetActive(false);
        panelHeavyEquipment.SetActive(true);
    }

    public void showConsoleSetting()
    {
        panelMainMenu.SetActive(false);
        panelConsoleSetting.SetActive(true);
    }
}
