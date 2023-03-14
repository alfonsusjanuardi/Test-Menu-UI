
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ConsolesInfo
{
    [Serializable]
    public class Consoles
    {
        public string name, vehicleName;

        public static List<Consoles> FromJson(string json) => JsonConvert.DeserializeObject<List<Consoles>>(json);
            public static string ToString(List<Consoles> json) => JsonConvert.SerializeObject(json, Formatting.Indented);
    }
}
