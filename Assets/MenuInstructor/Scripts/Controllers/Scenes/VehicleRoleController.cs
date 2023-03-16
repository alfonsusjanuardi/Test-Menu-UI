using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class VehicleRoleController : MonoBehaviour
{
    #region static
    public static VehicleRoleController instance;
    #endregion

    [Header("UI")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI detailPlotAddVehicle, detailPlotEditVehicle;

    [Header("List")]
    [Space(5)]
    [SerializeField] private List<HeavyEquipmentRole> vehiclesRole = new();
    public List<ConsoleInfo> getConsoles;
    [SerializeField] private List<TMP_Dropdown> dropdowns, dropdowns2;
    [SerializeField] private List<Button> buttons, buttons2;
    [SerializeField] private NetworkController DBConn;
    private string sql;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ConnectionDBVehicleRole();
        LoadDataVehicleRole();
        LoadDataEditVehicleRole();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConnectionDBVehicleRole()
    {
        sql = "select * from heavy_equipment_role";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            // dropdowns[0].captionText.text = (string) reader["vehicle_name"];
            string vehicle_name = (string) reader["vehicle_name"];

            vehiclesRole.Add(new HeavyEquipmentRole(){
                vehicleName = vehicle_name
            });
        }

        DBConn.CloseConnection();
    }

    public void LoadDataVehicleRole()
    {
        foreach (TMP_Dropdown item in dropdowns)
        {
            item.ClearOptions();
        }
        
        for (int i = 0; i < getConsoles.Count; i++)
        {
            List<string> vehicleNames = VehicleController.instance.GetVehiclesFromConsole(getConsoles[i].name);
            GetVehicleDropdown(dropdowns[i], vehicleNames);

            var vehicleName = vehiclesRole[i].vehicleName;

            var vehicles = new HeavyEquipmentRole(){
                vehicleName = vehicleName
            };

            buttons[i].onClick.AddListener(delegate{LoadDetailVehicleLocation(vehicles);});
        }
    }

    public void LoadDataEditVehicleRole()
    {
        foreach (TMP_Dropdown item in dropdowns2)
        {
            item.ClearOptions();
        }
        
        for (int i = 0; i < getConsoles.Count; i++)
        {
            List<string> vehicleNames = VehicleController.instance.GetVehiclesFromConsole(getConsoles[i].name);
            GetVehicleDropdown(dropdowns2[i], vehicleNames);

            var vehicleName = vehiclesRole[i].vehicleName;

            var vehicles = new HeavyEquipmentRole(){
                vehicleName = vehicleName
            };

            buttons2[i].onClick.AddListener(delegate{LoadDetailVehicleLocation(vehicles);});
        }
    }

    public void addVehicleRole()
    {
        for (int i = 0; i < getConsoles.Count; i++) {
            HeavyEquipmentRole dataVehicle = new (){
                consoleName = dropdowns[i].name,
                vehicleName = dropdowns[i].captionText.text
            };
            vehiclesRole.Add(dataVehicle);
            SetData(dataVehicle);
        }
    
        LoadDataVehicleRole();
    }

    private void SetData(HeavyEquipmentRole vehicleRole)
    {
        string tabelName = "heavy_equipment_role";
        List<string> columns = new()
        {
            "console_name",
            "vehicle_name"
        };
        List<List<string>> values = new()
        {
            new() { vehicleRole.consoleName, "nonNumeric" },
            new() { vehicleRole.vehicleName, "nonNumeric" }
        };

        IDataAdapter adapter = DBConn.setData(tabelName, columns, values);
        DBConn.CloseConnection();
    }


    public void LoadDetailVehicleLocation(HeavyEquipmentRole vehicles)
    {
        detailPlotAddVehicle.text = vehicles.vehicleName;
        detailPlotEditVehicle.text = vehicles.vehicleName;
    }
    
    private void GetVehicleDropdown(TMP_Dropdown dropdown, List<string> vehicleNames)
    {
        dropdown.AddOptions(vehicleNames);
    }
}

[Serializable]

public class HeavyEquipmentRole
{
    public string vehicleName, consoleName;
}