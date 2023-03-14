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

    [Header("List")]
    [Space(5)]
    public List<ConsoleInfo> getConsoles;
    [SerializeField] private List<TMP_Dropdown> dropdowns, dropdowns2;
    [SerializeField] private NetworkController DBConn;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDataVehicleRole()
    {
        foreach (TMP_Dropdown item in dropdowns)
        {
            item.ClearOptions();
        }
        
        for (int i = 0; i < getConsoles.Count; i++)
        {
            // Debug.Log(getConsoles[i].name);
            List<string> vehicleNames = VehicleController.instance.GetVehiclesFromConsole(getConsoles[i].name);
            GetVehicleDropdown(dropdowns[i], vehicleNames);
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
            // Debug.Log(getConsoles[i].name);
            List<string> vehicleNames = VehicleController.instance.GetVehiclesFromConsole(getConsoles[i].name);
            GetVehicleDropdown(dropdowns2[i], vehicleNames);
        }
    }
    
    private void GetVehicleDropdown(TMP_Dropdown dropdown, List<string> vehicleNames)
    {
        dropdown.AddOptions(vehicleNames);
    }
}