using System;
using System.Collections;
using System.Collections.Generic;
using Meta.Data;
using Newtonsoft.Json;
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

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GetNetworkList()
    {

    }

    public NetworkServices GetServices(int id)
    {
        return services[id];
    }

    private void ShowNetworkList()
    {
        
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
