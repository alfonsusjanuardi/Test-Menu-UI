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

    [TextArea(10,20)]
    public string json;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetInfo(VehicleInfo vInfo)
    {
        if (canvasPanel.activeSelf == false)
                canvasPanel.SetActive(true);

        vehicleName.text = vInfo.name;
        vehicleDescription.text = vInfo.description;
        imageHeavyEquipment.sprite = vInfo.sprite;
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