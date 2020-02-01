using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private const string kJSONName = "gameStrings";
    private Dictionary<string, object> _JSONObject = null;
    
    private void Awake()
    {
        TextAsset textFile = Resources.Load<TextAsset>(kJSONName);
        _JSONObject = MiniJSON.Json.Deserialize(textFile.text) as Dictionary<string, object>;
    }

    public string GetString(string key)
    {
        object result = null;
        _JSONObject.TryGetValue(key, out result);
        return result as string;
    }
}
