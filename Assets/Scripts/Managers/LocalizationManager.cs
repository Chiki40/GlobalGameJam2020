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

    public string GetString(string key, ref Tuple<string, string> palabrasEspeciales, ref bool withPalabraEspecial)
    {
        object result = null;
        _JSONObject.TryGetValue(key, out result);
        string resultString = (string)result;

        string keyStart = "<specialword>";
        string keyEnd = "</specialword>";

        withPalabraEspecial = false;

        string specialWord = getSpecialValue(ref resultString, keyStart, keyEnd);
        if(specialWord.Length > 0)
        {
            withPalabraEspecial = true;
            string solucionPuzzle = getSpecialValue(ref specialWord, "<solution>", "</solution>");
            //clean special world
            string stringToClean = "<solution>" + solucionPuzzle + "</solution>";
            resultString = resultString.Replace(stringToClean, "");

            specialWord = specialWord.Replace(stringToClean, "");
            specialWord = specialWord.Replace(keyStart, "");
            specialWord = specialWord.Replace(keyEnd, "");

            resultString = resultString.Replace(keyStart, "");
            resultString = resultString.Replace(keyEnd, "");

            Tuple<string, string> newTuple = new Tuple<string, string>(specialWord, solucionPuzzle);
            palabrasEspeciales = newTuple;
        }
        return resultString;
    }

    private string getSpecialValue(ref string originaString, string startString, string endString)
    {
        if (originaString.Contains(startString) && originaString.Contains(endString))
        {
            //there is special keys
            int start = originaString.IndexOf(startString, 0) + startString.Length;
            int end = originaString.IndexOf(endString, start);

            string value = originaString.Substring(start, end - start);
            return value;
        }
        return "";
    }
}
