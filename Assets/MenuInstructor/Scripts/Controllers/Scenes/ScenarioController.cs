using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using Meta.Data;

public class ScenarioController : MonoBehaviour
{
    public static ScenarioController instance;

    [Header("UI GameObject")]
    [Space(5)]
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject btn;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI scenarioName;
    [SerializeField] private TMP_InputField taskName, time, location, information;
    [SerializeField] private TextMeshProUGUI addLocation, editLocation;
    [SerializeField] private TMP_InputField addScenarioName, addTaskName, addTime, addInformation;
    [SerializeField] private TMP_InputField editScenarioName, editTaskName, editTime, editInformation;
    [SerializeField] private GameObject canvasPanel;
    
    [SerializeField] private NetworkController DBConn;
    public List<ListScenario> listScen = new();
    private string sql;

    private void Awake() {
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

    private void LoadDataScenario()
    {
        foreach (Transform trans in parentTransform)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < listScen.Count; i++)
        {
            var copy = Instantiate(btn, parentTransform);
            var meta = copy.GetComponent<Metadata>();
            var nama = listScen[i].scenario_name;
            var taskName = listScen[i].task_name;
            var time = listScen[i].time;
            var location = listScen[i].location;
            var information = listScen[i].information;

            string scenarioName = listScen[i].scenario_name;

            var scenario = new ListScenario(){
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
    }

    private void ConnectionDBScenario()
    {
        sql = "Select * from scenario";
        IDataReader reader = DBConn.getData(sql);

        while (reader.Read()) {
            scenarioName.text = (string) reader["scenario_name"];
            taskName.text = (string) reader["task_name"];
            time.text = (string) reader["time"];
            location.text = (string) reader["location"];
            information.text = (string) reader["information"];

            listScen.Add(new ListScenario(){
                scenario_name = scenarioName.text,
                task_name = taskName.text,
                time = time.text,
                location = location.text,
                information = information.text
            });
        }
        
        DBConn.CloseConnection();
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
    public string scenario_name, task_name, time, location, information;
}
