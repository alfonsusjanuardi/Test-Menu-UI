using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Meta.Data;
using Newtonsoft.Json;

public class ConsoleController : MonoBehaviour
{
    public static ConsoleController instance;

    [Header("UI GameObject")]
    [Space(5)]
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject btn;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private TextMeshProUGUI consoleName;
    [SerializeField] private TMP_InputField detailConsoleName, detailIPAddress;

    [Header("List")]
    [Space(5)]
    public List<ConsoleInfo> consoles = new();
    [SerializeField] private List<Toggle> toggles;
    public Dictionary<string, string> getConsoles;
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
        // ConnectionDBConsole();
        LoadDataConsole();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadDataConsole()
    {
        consoles = ConsoleInfo.FromJson(json);
        VehicleRoleController.instance.getConsoles = consoles;
        ParameterController.instance.getConsoles = consoles;

        for (int i = 0; i < consoles.Count; i++)
        {
            var copy = Instantiate(btn, parentTransform);
            var meta = copy.GetComponent<Metadata>();
            var nama = consoles[i].name;

            var console = new ConsoleInfo()
            {
                name = nama
            };

            meta.FindParamComponent<TextMeshProUGUI>("buttonName").text = nama;
            var button = meta.FindParamComponent<Button>("button");
            button.onClick.AddListener(delegate { GetInfo(console); });
        }
    }

    private void GetInfo(ConsoleInfo cInfo)
    {
        foreach (Toggle toggle in toggles)
            toggle.isOn = false;

        if (canvasPanel.activeSelf == false)
                canvasPanel.SetActive(true);
        
        consoleName.text = cInfo.name + " Setting";
        detailConsoleName.text = cInfo.name;

        List<string> vehicleName = VehicleController.instance.GetVehiclesFromConsole(cInfo.name);
        foreach (string item in vehicleName)
        {
            Toggle consoleToggle = GetVehicleToggle(item);

            if (consoleToggle != null)
                consoleToggle.isOn = true;
            else
                Debug.Log("No Console Attach To This Console!!");
        }
    }
    
    public List<Toggle> GetVehicleToggles()
    {
        return toggles;
    }

    /**
    * <summary>
    * Get Vehicle Toggle From Toggles List
    * </summary>
    * <returns>Toggle</returns>
    * <param name="vehicleName">
    * The Name of the Vehicle
    * </param>
    */
    public Toggle GetVehicleToggle(string vehicleName)
    {
        foreach (Toggle toggle in toggles)
        {
            TextMeshProUGUI tmp = toggle.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (tmp.text == vehicleName)
                return toggle;
        }

        return null;
    }
}

[Serializable]
public class ConsoleInfo
{
    public string name, vehicleName;

    public static List<ConsoleInfo> FromJson(string json) => JsonConvert.DeserializeObject<List<ConsoleInfo>>(json);
        public static string ToString(List<ConsoleInfo> json) => JsonConvert.SerializeObject(json, Formatting.Indented);
}
