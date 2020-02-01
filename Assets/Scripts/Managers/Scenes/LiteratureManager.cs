using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

struct Pharse
{
    public string allString;
    public Tuple<string, string> puzzle;
    public bool withPuzzle;
}

public class LiteratureManager : MonoBehaviour
{
    [SerializeField]
    private ConversationManager _conversationManager = null;
    [SerializeField]
    private LocalizationManager _localizationManager = null;

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

    public List<string> _keysBoy;
    public List<string> _keysGirl;

    private List<Pharse> phrasesBoy;
    private List<Pharse> phrasesGirl;

    private void Awake()
    {
        if (_conversationManager == null)
        {
             Debug.LogError("[LiteratureManager.Awake] ERROR: Serializable _conversationManager not set");
            return;
        }

        if (_localizationManager == null)
        {
            Debug.LogError("[LiteratureManager.Awake] ERROR: Serializable _localizationManager not set");
            return;
        }

        //read boy keys
        for(int i = 0; i < _keysBoy.Count; ++i)
        {
            Tuple<string, string> puzzle = new Tuple<string, string>("","");
            bool withPuzzle = false;
            string allString = _localizationManager.GetString(_keysBoy[i], ref puzzle, ref withPuzzle);

            Pharse p;
            p.allString = allString;
            p.puzzle = puzzle;
            p.withPuzzle = withPuzzle;
            phrasesBoy.Add(p);
        }

        for (int i = 0; i < _keysBoy.Count; ++i)
        {
            Tuple<string, string> puzzle = new Tuple<string, string>("", "");
            bool withPuzzle = false;
            string allString = _localizationManager.GetString(_keysGirl[i], ref puzzle, ref withPuzzle);

            Pharse p;
            p.allString = allString;
            p.puzzle = puzzle;
            p.withPuzzle = withPuzzle;
            phrasesGirl.Add(p);
        }
    }

    private void Start()
    {
        _numGlich = _gliches.Length / 2; // 50% de gliches
        for (int i = 0; i < _gliches.Length; ++i)
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
        _conversationManager.SetConversation(new List<string>() {"Primer mensaje", "Bloste, una polla como un poste" });
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
                _conversationManager.Block(true);
            }

            if (currentFrase == 2)
            {
                _textInput[1].SetActive(true);
                _conversationManager.Block(true);
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
                _conversationManager.SetConversation(new List<string>() { "Moraleja final", "eres un desgraciado", "ojala tengas que hacer un build de luces de unity" });
                Debug.Log("has acabado bien el nivel");
            }
            else
            {
                _conversationManager.NextMessage(force:true);
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
        if (_gliches.Length == 0)
        {
            return;
        }
        --_numGlich;
        _gliches[_numGlich].fixImage();
        if (_numGlich <= 0)
        {
            Debug.Log("Todos los glich arreglados");
        }
    }

    public void PalabraIncorrecta()
    {
        CrearGlich();
        if (currentFrase == 1)
        {
            _textInput[0].GetComponent<InputField>().text = "";
        }

        if (currentFrase == 2)
        {
            _textInput[1].GetComponent<InputField>().text = "";
        }
    }

    public void PulsadoElPersonaje()
    {
        MostrarPersonajeBig();

        _allPistas  = _pistaConocida.TrueForAll(b => b == true);

        if (!_allPistas)
        {
           _conversationManager.SetConversation(new List<string>() {"**Glich**", "No tienes todas las pistas" });
            CrearGlich();
        }
        else
        {
            currentFrase = 0;
            _conversationManager.SetConversation(new List<string>() { "Texto final bien, mas te vale poner siempre bloste", "Bloste", "Bloste"});
        }
    }
    
    private void CrearGlich()
    {
        Debug.Log("creamos un glich");
        if (_gliches.Length == 0)
        {
            return;
        }
        _gliches[_numGlich].glich();
        ++_numGlich;
        if (_numGlich >= _gliches.Length)
        {
            Debug.Log("game over");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            
        }

    }
}
