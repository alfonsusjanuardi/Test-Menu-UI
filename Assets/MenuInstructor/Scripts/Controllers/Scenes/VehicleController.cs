using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Meta.Data;
using Newtonsoft.Json;

public class VehicleController : MonoBehaviour
{
    public static VehicleController instance;

    [Header("UI GameObject")]
    [Space(5)]
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject btn;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private TextMeshProUGUI vehicleName, vehicleDescription;
    [SerializeField] private Image imageHeavyEquipment;

    [Header("List")]
    [Space(5)]
    [SerializeField] private List<VehicleInfo> vehicles = new();
    [SerializeField] private List<Toggle> toggles;
    public Dictionary<string, string> consoles = new();

    [SerializeField] private NetworkController DBConn;
    private string sql;

    [TextArea(10,20)]
    public string json;

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
        bool dataPulled = ConnectionDBVehicle();
        ConsoleController.instance.getConsoles = dataPulled ? consoles : new();
        LoadDataVehicle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool ConnectionDBVehicle()
    {
        sql = "select * from console_vehicle";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            string console_name = (string) reader["console_name"];
            string vehicle_name = (string) reader["vehicle_name"];

            if (!string.IsNullOrEmpty(console_name) && !string.IsNullOrEmpty(vehicle_name)) {
                consoles.Add(vehicle_name, console_name);
            }
        }

        DBConn.CloseConnection();

        if (consoles.Count > 0)
            return true;
        else
            return false;
    }

    private void GetInfo(VehicleInfo vInfo)
    {
        foreach (Toggle toggle in toggles)
            toggle.isOn = false;

        if (canvasPanel.activeSelf == false)
                canvasPanel.SetActive(true);

        vehicleName.text = vInfo.name;
        vehicleDescription.text = vInfo.description;
        imageHeavyEquipment.sprite = vInfo.sprite;

        string consoleName = GetConsoleFromVehicle(vInfo.name);
        Toggle consoleToggle = GetConsoleToggle(consoleName);

        if (consoleToggle != null)
            consoleToggle.isOn = true;
        else
            Debug.Log("No Console Attach To This Vehicle!!");
    }

    private void LoadDataVehicle()
    {
        vehicles = VehicleInfo.FromJson(json);

        for (int i = 0; i < vehicles.Count; i++)
        {
            var copy = Instantiate(btn, parentTransform);
            var meta = copy.GetComponent<Metadata>();
            var nama = vehicles[i].name;
            var desc = vehicles[i].description;
            var sprite = Resources.Load<Sprite>("Images/" + nama + "/" + nama);

            var vehicle = new VehicleInfo()
            {
                name = nama,
                description = desc,
                sprite = sprite
            };

            meta.FindParamComponent<TextMeshProUGUI>("buttonName").text = nama;
            var button = meta.FindParamComponent<Button>("button");
            button.onClick.AddListener(delegate { GetInfo(vehicle); });
        }
    }

    public List<Toggle> GetConsoleToggles()
    {
        return toggles;
    }

    public Toggle GetConsoleToggle(string consoleName)
    {
        foreach (Toggle toggle in toggles)
        {
            TextMeshProUGUI tmp = toggle.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (tmp.text == consoleName)
                return toggle;
        }

        return null;
    }

    public List<string> GetVehiclesFromConsole(string vehicleName)
    {
        List<string> vehicles = new();
        foreach (KeyValuePair<string,string> keyValue in consoles)
        {
            if (keyValue.Value == vehicleName)
                vehicles.Add(keyValue.Key);
        }
        
        return vehicles;
    }

    public string GetConsoleFromVehicle(string consoleName)
    {
        foreach (KeyValuePair<string,string> keyValue in consoles)
        {
            if (keyValue.Key == consoleName)
                return keyValue.Value;
        }

        return null;
    }
}

[Serializable]
public class VehicleInfo
{
    public string name;
    public string description;
    [JsonIgnore] public Sprite sprite;

    public static List<VehicleInfo> FromJson(string json) => JsonConvert.DeserializeObject<List<VehicleInfo>>(json);
        public static string ToString(List<VehicleInfo> json) => JsonConvert.SerializeObject(json, Formatting.Indented);
}