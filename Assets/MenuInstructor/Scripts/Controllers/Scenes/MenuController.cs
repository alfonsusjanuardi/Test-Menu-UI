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
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelScenarioList, panelAddScenario, panelEditScenario;
    [SerializeField] private GameObject panelAddDamagePoint, panelEditDamagePoint;
    [SerializeField] private GameObject panelAddVehicleRole, panelEditVehicleRole;
    [SerializeField] private GameObject panelAddPlotLocation, panelEditPlotLocation;
    [SerializeField] private GameObject panelAddPlotLocation1, panelAddPlotLocation2, panelAddPlotLocation3, panelAddPlotLocation4, panelAddPlotLocation5;
    [SerializeField] private GameObject panelEditPlotLocation1, panelEditPlotLocation2, panelEditPlotLocation3, panelEditPlotLocation4, panelEditPlotLocation5;
    [SerializeField] private GameObject panelHeavyEquipment;
    [SerializeField] private GameObject panelConsoleSetting;

    //button
    [SerializeField] private GameObject btnBackHomeScenario, btnBackHomeHeavyEquipment, btnBackHomeConsoleSetting;
    
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
        panelAddDamagePoint.SetActive(false);
        panelAddScenario.SetActive(true);
    }

    public void showAddDamagePoint()
    {
        panelAddDamagePoint.SetActive(true);
        panelAddScenario.SetActive(false);
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
        panelEditDamagePoint.SetActive(false);
        panelEditScenario.SetActive(true);
    }

    public void showEditDamagePoint()
    {
        panelEditDamagePoint.SetActive(true);
        panelEditScenario.SetActive(false);
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
