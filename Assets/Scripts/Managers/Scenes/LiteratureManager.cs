using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LiteratureManager : MonoBehaviour
{
    public GameObject _personaje;
    public GameObject _personajeBig;
    public List<GameObject> _pistas;
    public List<GameObject> _textInput;
    private List<bool> _pistaConocida;
    private int _numGlich = 0;
    private int _actualPalabrasCorrectas = 0;
    public int _maxPalabrasCorrectas = 5;
    private bool _allPistas = false;
    private int currentFrase = 0;
    public GlichComponent[] _gliches;


    // Start is called before the first frame update
    void Start()
    {
        _numGlich = _gliches.Length / 2; // 50% de gliches
        for (int i = 0; i < _gliches.Length;++i)
        {
            if (i < _numGlich)
            {
                _gliches[i].glich();
            }
            else
            {
                _gliches[i].fixImage();
            }
        }
        _pistaConocida = new List<bool>();
        for(int i = 0;i < _pistas.Count; ++i)
        {
            _pistaConocida.Add(false);
        }
        PrimerMensaje();
    }

    public void PrimerMensaje()
    {
        FindObjectOfType<ConversationManager>().SetConversation(new List<string>() {"Primer mensaje", "Bloste, una polla como un poste" });
        MostrarPersonajeBig();
    }

    private void MostrarPersonajeBig()
    {
        ChangeMouse ms = _personaje.GetComponent<ChangeMouse>();
        ms.OnMouseExit();
        _personaje.SetActive(false);
        _personajeBig.SetActive(true);
    }

    private void OcultarPersonajeBig()
    {
        _personaje.SetActive(true);
        _personajeBig.SetActive(false);
    }

    public void EndConversation()
    {
        OcultarPersonajeBig();
    }
    public void EndPartConversation()
    {
        //deactivate all
        for( int i = 0; i < _textInput.Count; ++i)
        {
            _textInput[i].SetActive(false);
        }

        if (_allPistas)
        {
            //dependin the clue, we activate a different one
            if (currentFrase == 1)
            {
                _textInput[0].SetActive(true);
            }

            if (currentFrase == 2)
            {
                _textInput[1].SetActive(true);
            }
            ++currentFrase;
        }
    }

    public void PistaConocida(int index)
    {
        _pistaConocida[index] = true;
    }

    public void PalabraCorrecta(int index)
    {
        if (_allPistas)
        {
            RemoveGlich();
            ++_actualPalabrasCorrectas;
            if (_actualPalabrasCorrectas >= _maxPalabrasCorrectas)
            {
                FindObjectOfType<ConversationManager>().SetConversation(new List<string>() { "Moraleja final", "eres un desgraciado", "ojala tengas que hacer un build de luces de unity" });
                Debug.Log("has acabado bien el nivel");
            }
        }
        else
        {
            Debug.Log("aunque la palabra es correcta, no tienes todas las pistas");
            PalabraIncorrecta();
        }
    }

    private void RemoveGlich()
    {
        Debug.Log("quitamos un glich");
        --_numGlich;
        if (_numGlich <= 0)
        {
            Debug.Log("Todos los glich arreglados");
        }
        else
        {
            _gliches[_numGlich].fixImage();
        }
    }

    public void PalabraIncorrecta()
    {
        CrearGlich();
    }

    public void PulsadoElPersonaje()
    {
        MostrarPersonajeBig();

        _allPistas  = _pistaConocida.TrueForAll(b => b == true);

        if (!_allPistas)
        {
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>() {"**Glich**", "No tienes todas las pistas" });
            CrearGlich();
        }
        else
        {
            currentFrase = 0;
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>() { "Texto final bien, mas te vale poner siempre bloste", "Bloste", "Bloste"});
        }
    }
    
    private void CrearGlich()
    {
        Debug.Log("creamos un glich");
        ++_numGlich;
        if (_numGlich > _gliches.Length)
        {
            Debug.Log("game over");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            _gliches[_numGlich].glich();
        }

    }
}
