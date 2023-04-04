using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Util;
using Sfs2X.Entities;
using Sfs2X.Entities.Variables;

using TMPro;
using UnityEngine.UI;
using Meta.Data;

public class ScenarioController : BaseSceneController
{
    public static ScenarioController instance;

    [Header("Custom ID")]
    [Space(5)]
    [SerializeField] private int customID;
    [SerializeField] private bool useCustomID;

    [Header("UI GameObject")]
    [Space(5)]
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject btn;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI scenarioName;
    [SerializeField] private TMP_InputField taskName, time, location, information;
    [SerializeField] private TextMeshProUGUI addLocation, editLocation;
    public TextMeshProUGUI idScenario;
    [SerializeField] private TMP_InputField addScenarioName, addTaskName, addTime, addInformation;
    [SerializeField] private TMP_InputField editScenarioName, editTaskName, editTime, editInformation;
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private TMP_Dropdown networkDropdown;

    [Header("Smartfox")]
    [Space(5)]
    [SerializeField] private bool debug;
    private SmartFox sfs;
    
    [SerializeField] private NetworkController DBConn;
    public List<ListScenario> listScen = new();
    private string sql;

    private void OnEnable() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        ConnectionDBScenario();
        LoadDataScenario();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClick()
    {
        gm.gamePlay = (GlobalManager.GamePlay)2;
        Connect();
    }

    private void EnableUI(bool enable)
    {

    }
    

    private void Connect()
    {
        // EnableUI(false);

        //Get Current Network
        NetworkServices currentNetwork = NetworkServiceController.instance.GetServices(networkDropdown.value);

        ConfigData cfg = new()
        {
            Host = currentNetwork.host,
            Port = currentNetwork.port,
            Zone = currentNetwork.zone,
            Debug = debug
        };

        // Initialize SmartFox client
		// The singleton class GlobalManager holds a reference to the SmartFox class instance,
		// so that it can be shared among all the scenes
		sfs = gm.CreateSfsClient();

        // Configure SmartFox internal logger
        sfs.Logger.EnableConsoleTrace = debug;

        // Add Event Listeners
        AddSmartFoxListeners();

        // Connect to SmartFoxServer
        sfs.Connect(cfg);
    }

    private void AddSmartFoxListeners()
    {
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVarsUpdate);
        sfs.AddEventListener(SFSEvent.UDP_INIT, OnUdpInit);
    }

    protected override void RemoveSmartFoxListeners()
    {
        if (sfs != null)
        {
            sfs.RemoveEventListener(SFSEvent.CONNECTION, OnConnection);
            sfs.RemoveEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.RemoveEventListener(SFSEvent.LOGIN, OnLogin);
            sfs.RemoveEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            sfs.RemoveEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVarsUpdate);
            sfs.RemoveEventListener(SFSEvent.UDP_INIT, OnUdpInit);
        }
    }

    private void OnConnection(BaseEvent evt)
    {
        // Check if the conenction was established or not
        if ((bool)evt.Params["success"]) {
            Debug.Log("SFS2X API version: " + sfs.Version);
            Debug.Log("Connection mode is: " + sfs.ConnectionMode);
            sfs.Send(new LoginRequest(""));
        } else {
            Debug.Log("Connection failed; is the server running at all?");
        }
    }

    private void OnConnectionLost(BaseEvent evt)
    {
        // Remove SFS listeners
        RemoveSmartFoxListeners();

        // Show error message
        string reason = (string)evt.Params["reason"];

        if (reason != ClientDisconnectionReason.MANUAL)
            Debug.Log("Connection lost, reason is: " + reason);
    }

    private void OnLogin(BaseEvent evt)
    {
        if (useCustomID)
        {
            List<UserVariable> userVars = new() { new SFSUserVariable("id", customID) };
            sfs.Send(new SetUserVariablesRequest(userVars));
        }

        // Initialize UDP communication
        sfs.InitUDP();
    }

    private void OnLoginError(BaseEvent evt)
    {
        // Disconnect
        // NOTE: this causes a CONNECTION_LOST event with reason "manual", which in turn removes all SFS listeners
        sfs.Disconnect();

        Debug.Log("Login failed due to the following error:\n" + (string)evt.Params["errorMessage"]);
    }

    private void OnUserVarsUpdate(BaseEvent evt)
    {
        // Get the user that changed his variables
        User user = (User)evt.Params["user"];
        
        Debug.Log(user + " Vars Updated");
    }

    private void OnUdpInit(BaseEvent evt)
    {
        if ((bool)evt.Params["success"]) {
            // Load lobby scene
            Debug.Log("success");
        } else {
            // Disconnect
            // NOTE: this causes a CONNECTION_LOST event with reason "manual", which in turn removes all SFS listeners
            sfs.Disconnect();

            Debug.Log("UDP initialization failed due to the following error:\n" + (string)evt.Params["errorMessage"]);
        }
    }

    private void LoadDataScenario()
    {
        SpawnObstacleController.instance.getScenarios = listScen;
        SpawnVehicleController.instance.getScenarios = listScen;
        foreach (Transform trans in parentTransform)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < listScen.Count; i++)
        {
            var copy = Instantiate(btn, parentTransform);
            var meta = copy.GetComponent<Metadata>();
            var id = listScen[i].id_scenario;
            var nama = listScen[i].scenario_name;
            var taskName = listScen[i].task_name;
            var time = listScen[i].time;
            var location = listScen[i].location;
            var information = listScen[i].information;

            string scenarioName = listScen[i].scenario_name;

            var scenario = new ListScenario(){
                id_scenario = id,
                scenario_name = nama,
                task_name = taskName,
                time = time,
                location = location,
                information = information
            };

            meta.FindParamComponent<TextMeshProUGUI>("buttonName").text = nama;
            var button = meta.FindParamComponent<Button>("button");

            button.onClick.AddListener(delegate { btnFunc(scenario); });
        }
    }

    private void btnFunc(ListScenario sInfo)
    {
        if (canvasPanel.activeSelf == false)
            canvasPanel.SetActive(true);
        
        scenarioName.text = sInfo.scenario_name;
        taskName.text = sInfo.task_name;
        time.text = sInfo.time;
        location.text = sInfo.location;
        information.text = sInfo.information;

        //edit scenario
        editScenarioName.text = sInfo.scenario_name;
        editTaskName.text = sInfo.task_name;
        editTime.text = sInfo.time;
        editLocation.text = sInfo.location;
        editInformation.text = sInfo.information;
        idScenario.text = sInfo.id_scenario.ToString();
    }

    private void ConnectionDBScenario()
    {
        sql = "Select * from scenario";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            int id_scenario = reader.GetInt16(0);
            scenarioName.text = (string) reader["scenario_name"];
            taskName.text = (string) reader["task_name"];
            time.text = (string) reader["time"];
            location.text = (string) reader["location"];
            information.text = (string) reader["information"];

            listScen.Add(new ListScenario(){
                id_scenario = id_scenario,
                scenario_name = scenarioName.text,
                task_name = taskName.text,
                time = time.text,
                location = location.text,
                information = information.text
            });
        }
        
        DBConn.CloseConnection();
    }

    private string GetLocalComputerName()
    {
        string host = Dns.GetHostName();
        return host;
        throw new System.Exception("Can't get host name!");
    }

    private string GetLocalIPAddress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    public void addScenario()
    {
        ListScenario scenario = new()
        {
            scenario_name = addScenarioName.text,
            task_name = addTaskName.text,
            time = addTime.text,
            location = addLocation.text,
            information = addInformation.text
        };
        listScen.Add(scenario);
        SetData(scenario);
        SpawnObstacleController.instance.AddSpawnObstacles();

        for (int i = 0; i < SpawnObstacleController.instance.spawnObstacles.Count; i++)
        {
            Destroy(SpawnObstacleController.instance.spawnObstacles[i].obsPloting);
        }
        SpawnObstacleController.instance.spawnObstacles.Clear();

        LoadDataScenario();
    }

    private void SetData(ListScenario scenario)
    {
        string tabelName = "scenario";
        List<string> columns = new()
        {
            "id_scenario",
            "scenario_name",
            "task_name",
            "time",
            "location",
            "information"
        };
        List<List<string>> values = new()
        {
            new() { SetID().ToString(), "numeric" },
            new() { scenario.scenario_name, "nonNumeric" },
            new() { scenario.task_name, "nonNumeric"},
            new() { scenario.time, "nonNumeric"},
            new() { scenario.location, "nonNumeric"},
            new() { scenario.information, "nonNumeric"}
        };

        IDataAdapter adapter = DBConn.setData(tabelName, columns, values);
        DBConn.CloseConnection();
    }

    public void editScenario()
    {
        ListScenario scenario = listScen.Find(x => x.scenario_name == editScenarioName.text);
        scenario.task_name = editTaskName.text;
        scenario.time = editTime.text;
        scenario.location = editLocation.text;
        scenario.information = editInformation.text;

        EditData(scenario);
        LoadDataScenario();
    }

    private void EditData(ListScenario scenario)
    {
        string tabelName = "scenario";
        List<string> columns = new()
        {
            "task_name",
            "time",
            "location",
            "information"
        };
        List<List<string>> values = new()
        {
            new() { scenario.task_name, "nonNumeric"},
            new() { scenario.time, "nonNumeric"},
            new() { scenario.location, "nonNumeric"},
            new() { scenario.information, "nonNumeric"}
        };

        IDataAdapter adapter = DBConn.editData(tabelName, columns, values, "scenario_name", scenario.scenario_name);
        DBConn.CloseConnection();
    }

    private int SetID()
    {
        return listScen != null ? listScen.Count : 0;
    }
}

[Serializable]
public class ListScenario
{
    public int id_scenario;
    public string scenario_name, task_name, time, location, information;
}
