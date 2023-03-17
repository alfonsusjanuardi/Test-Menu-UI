using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ParameterController : MonoBehaviour
{
    #region static
    public static ParameterController instance;
    #endregion

    [Header("UI")]
    [Space(5)]
    [SerializeField] private TMP_InputField detailAddParameterVehicle, detailEditParameterVehicle;

    [Header("List")]
    [Space(5)]
    public List<ConsoleInfo> getConsoles;
    [SerializeField] private List<RatingParameter> vehiclesParameters = new();
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
        ConnectionDBVehicleParameter();
        LoadDataVehicleParameter();
        LoadEditDataVehicleParameter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ConnectionDBVehicleParameter()
    {
        sql = "select * from heavy_equipment_role";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            // dropdowns[0].captionText.text = (string) reader["vehicle_name"];
            string vehicle_name = (string) reader["vehicle_name"];

            vehiclesParameters.Add(new RatingParameter(){
                vehicleName = vehicle_name
            });
        }

        DBConn.CloseConnection();
    }

    private void LoadDataVehicleParameter()
    {
        foreach (TMP_Dropdown item in dropdowns)
        {
            item.ClearOptions();
        }
        
        for (int i = 0; i < getConsoles.Count; i++)
        {
            List<string> vehicleNames = VehicleController.instance.GetVehiclesFromConsole(getConsoles[i].name);
            GetVehicleDropdown(dropdowns[i], vehicleNames);

            var vehicleName = vehiclesParameters[i].vehicleName;

            var vehicles = new RatingParameter(){
                vehicleName = vehicleName
            };

            buttons[i].onClick.AddListener(delegate{LoadDetailVehicleParameter(vehicles);});
        }
    }

    private void LoadEditDataVehicleParameter()
    {
        foreach (TMP_Dropdown item in dropdowns2)
        {
            item.ClearOptions();
        }
        
        for (int i = 0; i < getConsoles.Count; i++)
        {
            List<string> vehicleNames = VehicleController.instance.GetVehiclesFromConsole(getConsoles[i].name);
            GetVehicleDropdown(dropdowns2[i], vehicleNames);

            var vehicleName = vehiclesParameters[i].vehicleName;

            var vehicles = new RatingParameter(){
                vehicleName = vehicleName
            };

            buttons2[i].onClick.AddListener(delegate{LoadDetailVehicleParameter(vehicles);});
        }
    }

    public void LoadDetailVehicleParameter(RatingParameter vehicles)
    {
        detailAddParameterVehicle.text = vehicles.vehicleName;
        detailEditParameterVehicle.text = vehicles.vehicleName;
    }

    private void GetVehicleDropdown(TMP_Dropdown dropdown, List<string> vehicleNames)
    {
        dropdown.AddOptions(vehicleNames);
    }
}

[Serializable]

public class RatingParameter
{
    public string vehicleName, consoleName;
}
