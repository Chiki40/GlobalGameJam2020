using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    LocalizationManager _manager;
    private void Start()
    {
        _manager = GetComponent<LocalizationManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
            string value = _manager.GetString("clave2", ref lista);

            Debug.Log("el value es =>" + value);

            foreach(var v in lista)
            {
                Debug.Log("la palabra especial es =>" + v.Item1);
                Debug.Log("la palabra real es =>" + v.Item2);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
            string value = _manager.GetString("clave1", ref lista);

            Debug.Log("el value es =>" + value);

            foreach (var v in lista)
            {
                Debug.Log("la palabra especial es =>" + v.Item1);
                Debug.Log("la palabra real es =>" + v.Item2);
            }
        }
    }
}
