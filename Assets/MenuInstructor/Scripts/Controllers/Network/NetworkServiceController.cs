using System;
using System.Collections;
using System.Collections.Generic;
using Meta.Data;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkServiceController : MonoBehaviour
{
    #region Instance

    public static NetworkServiceController instance;

    #endregion

    [Header("Network")]
    [Space(5)]
    [SerializeField] private string filePath;
    [SerializeField] private List<NetworkServices> services = new();

    [Header("Prefab")]
    [Space(5)]
    [SerializeField] private GameObject listPrefab;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject networkForm;

    [Header("Login UI Inputs")]
    [Space(5)]
    [SerializeField] private TMP_Dropdown networkDropdown;

    [Header("Inputs")]
    [Space(5)]
    [SerializeField] private Button saveNetworkButton;
    [SerializeField] private TMP_InputField networkNameInput;
    [SerializeField] private TMP_InputField hostInput;
    [SerializeField] private TMP_InputField portInput;
    [SerializeField] private TMP_InputField zoneInput;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetNetworkList();
        ShowNetworkList();
        RefreshDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GetNetworkList()
    {
        string json = ReadWriteFile.ReadTextFilePersistent("NetworkSettings.json");
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("Creating A Local Network...");

            NetworkServices newService = new()
            {
                name = "Local",
                host = "127.0.0.1",
                port = 9933,
                zone = "HeavyVehicle"
            };

            services.Add(newService);
            ReadWriteFile.WriteTextFilePersistent(NetworkServices.ToString(services), "NetworkSettings.json");

            Debug.Log("Local Network Added");
            return;
        }

        services = NetworkServices.FromJson(json);
    }

    public NetworkServices GetServices(int id)
    {
        return services[id];
    }

    private void ShowNetworkList()
    {
        ClearContent();
        for (int i = 0; i < services.Count; i++)
        {
            GameObject copy = Instantiate(listPrefab, content);
            NetworkServices service = services[i];

            Metadata meta = copy.GetComponent<Metadata>();
            meta.FindParamComponent<TextMeshProUGUI>("No.").text = (i + 1).ToString();
            meta.FindParamComponent<TextMeshProUGUI>("Name").text = services[i].name;
            meta.FindParamComponent<TextMeshProUGUI>("Host").text = services[i].host;
            meta.FindParamComponent<TextMeshProUGUI>("Port").text = services[i].port.ToString();
            meta.FindParamComponent<TextMeshProUGUI>("Zone").text = services[i].zone;
            meta.FindParamComponent<Button>("Edit").onClick.AddListener(delegate { OpenEditForm(service, meta); });
            meta.FindParamComponent<Button>("Delete").onClick.AddListener(delegate { RemoveNetwork(service, copy); });
        }
    }

    private void RefreshDropdown()
    {
        List<string> dropdowns = new();
        foreach (NetworkServices service in services)
            dropdowns.Add(service.name);

        networkDropdown.ClearOptions();
        networkDropdown.AddOptions(dropdowns);
    }

    public void OpenServicesSetting()
    {
        canvas.SetActive(true);
        MenuController.instance.panelScenarioList.SetActive(false);
    }

    public void CloseServicesSetting()
    {
        canvas.SetActive(false);
        MenuController.instance.panelScenarioList.SetActive(true);
    }

    private void ClearContent()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddNetwork()
    {
        if (networkNameInput.text == string.Empty)
        {
            Debug.Log("Name Must Not Be Null!!!");
            return;
        }

        if (hostInput.text == string.Empty)
        {
            Debug.Log("Host Must Not Be Null!!!");
            return;
        }

        if (portInput.text == string.Empty)
        {
            Debug.Log("Port Must Not Be Null!!!");
            return;
        }

        if (zoneInput.text == string.Empty)
        {
            Debug.Log("Zone Must Not Be Null!!!");
            return;
        }

        NetworkServices newService = new()
        {
            name = networkNameInput.text,
            host = hostInput.text,
            port = int.Parse(portInput.text),
            zone = zoneInput.text
        };

        services.Add(newService);
        ReadWriteFile.WriteTextFilePersistent(NetworkServices.ToString(services), "NetworkSettings.json");
        ShowNetworkList();

        RefreshDropdown();
        networkForm.SetActive(false);
    }

    private void EditNetwork(NetworkServices service, Metadata serviceObjectMeta)
    {
        if (networkNameInput.text == string.Empty)
        {
            Debug.Log("Name Must Not Be Null!!!");
            return;
        }

        if (hostInput.text == string.Empty)
        {
            Debug.Log("Host Must Not Be Null!!!");
            return;
        }

        if (portInput.text == string.Empty)
        {
            Debug.Log("Port Must Not Be Null!!!");
            return;
        }

        if (zoneInput.text == string.Empty)
        {
            Debug.Log("Zone Must Not Be Null!!!");
            return;
        }

        NetworkServices newService = services.Find(x => x == service);
        newService.name = networkNameInput.text;
        newService.host = hostInput.text;
        newService.port = int.Parse(portInput.text);
        newService.zone = zoneInput.text;

        serviceObjectMeta.FindParamComponent<TextMeshProUGUI>("Name").text = newService.name;
        serviceObjectMeta.FindParamComponent<TextMeshProUGUI>("Host").text = newService.host;
        serviceObjectMeta.FindParamComponent<TextMeshProUGUI>("Port").text = newService.port.ToString();
        serviceObjectMeta.FindParamComponent<TextMeshProUGUI>("Zone").text = newService.zone;

        ReadWriteFile.WriteTextFilePersistent(NetworkServices.ToString(services), "NetworkSettings.json");

        RefreshDropdown();
        networkForm.SetActive(false);
    }

    private void RemoveNetwork(NetworkServices service, GameObject serviceObject)
    {
        services.Remove(service);
        Destroy(serviceObject);
        string servicesJson = services.Count == 0 ? string.Empty : NetworkServices.ToString(services);

        ReadWriteFile.WriteTextFilePersistent(servicesJson, "NetworkSettings.json");
        ShowNetworkList();

        RefreshDropdown();
    }

    private void ResetForm()
    {
        networkNameInput.text = string.Empty;
        hostInput.text = string.Empty;
        portInput.text = string.Empty;
        zoneInput.text = string.Empty;
    }

    public void OpenAddForm()
    {
        saveNetworkButton.onClick.RemoveAllListeners();
        saveNetworkButton.onClick.AddListener(delegate { AddNetwork(); });

        ResetForm();
        networkForm.SetActive(true);
    }

    private void OpenEditForm(NetworkServices service, Metadata serviceObjectMeta)
    {
        networkNameInput.text = service.name;
        hostInput.text = service.host;
        portInput.text = service.port.ToString();
        zoneInput.text = service.zone;

        saveNetworkButton.onClick.RemoveAllListeners();
        saveNetworkButton.onClick.AddListener(delegate { EditNetwork(service, serviceObjectMeta); });

        networkForm.SetActive(true);
    }

    public void CloseForm()
    {
        networkForm.SetActive(false);
    }
}

[System.Serializable]
public class NetworkServices
{
    public string name;
    public string host;
    public int port;
    public string zone;

    public static List<NetworkServices> FromJson(string json) => JsonConvert.DeserializeObject<List<NetworkServices>>(json);
        public static string ToString(List<NetworkServices> json) => JsonConvert.SerializeObject(json, Formatting.Indented);
}
