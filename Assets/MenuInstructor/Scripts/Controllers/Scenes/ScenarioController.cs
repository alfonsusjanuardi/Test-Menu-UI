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
    [Header("UI GameObject")]
    [Space(5)]
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject btn;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI scenarioName;
    [SerializeField] private TMP_InputField taskName, time, location, information;
    [SerializeField] private GameObject canvasPanel;
    
    [SerializeField] private NetworkController DBConn;
    public List<ListScenario> listScen = new();
    private string sql;

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
}

[Serializable]
public class ListScenario
{
    public string scenario_name, task_name, time, location, information;
}
