using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("UI")]
    [Space(5)]
    [SerializeField] private GameObject btnScenarioList;
    [SerializeField] private GameObject btnHeavyEquipment;
    [SerializeField] private GameObject btnConsoleSetting;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelScenarioList;
    [SerializeField] private GameObject panelHeavyEquipment;
    [SerializeField] private GameObject panelConsoleSetting;

    //button
    [SerializeField] private GameObject btnBackHomeScenario, btnBackHomeHeavyEquipment, btnBackHomeConsoleSetting;
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
