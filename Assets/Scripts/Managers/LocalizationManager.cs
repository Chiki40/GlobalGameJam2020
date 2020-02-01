using System;
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

    public string GetString(string key, ref List<Tuple<string, string>> palabrasEspeciales)
    {
        object result = null;
        _JSONObject.TryGetValue(key, out result);
        string resultString = (string)result;

        int specialWords = 0;
        {
            string keyStart = "<specialNum>";
            string keyEnd = "</specialNum>";
            string specialWords_str = getSpecialValue(ref resultString, keyStart, keyEnd, true);
            if(specialWords_str.Length > 0)
            {
                specialWords = Int32.Parse(specialWords_str);
            }
        }  
        
        for(int i = 0; i < specialWords; ++i)
        {
            string keyStart = "<K" + i + ">";
            string keyEnd = "</K" + i + ">";
            string palabra = getSpecialValue(ref resultString, keyStart, keyEnd, false);
            string palabraPuzzle = getSpecialValue(ref palabra, "<value>", "</value>", true);//here there is the value only
            //make string beautiful
            string str_to_replace = keyStart + palabra + "<value>" + palabraPuzzle + "</value>" + keyEnd;
            resultString = resultString.Replace(str_to_replace, palabra);

            Tuple<string, string> tupla = new Tuple<string, string>(palabra, palabraPuzzle);
            palabrasEspeciales.Add(tupla);
        }

        return resultString;
    }

    private string getSpecialValue(ref string originaString, string startString, string endString, bool removeAll)
    {
        if (originaString.Contains(startString) && originaString.Contains(endString))
        {
            //there is special keys
            int start = originaString.IndexOf(startString, 0) + startString.Length;
            int end = originaString.IndexOf(endString, start);

            string value = originaString.Substring(start, end - start);
            if (removeAll)
            {
                string totalSubString = startString + value + endString;
                originaString = originaString.Replace(totalSubString, "");
            }
            return value;
        }
        return "";
    }
}
