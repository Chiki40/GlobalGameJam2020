using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiteratureManager : MonoBehaviour
{
    public GameObject _personaje;
    public List<GameObject> _pistas;
    private List<bool> _pistaConocida;


    // Start is called before the first frame update
    void Start()
    {
        _pistaConocida = new List<bool>();
        for(int i = 0;i < _pistas.Count; ++i)
        {
            _pistaConocida.Add(false);
        }
    }

    public void PistaConocida(int index)
    {
        _pistaConocida[index] = true;
        Debug.Log("ya conocemos la pista =>" + index);
    }

    
}
