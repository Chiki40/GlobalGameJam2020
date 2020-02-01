using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericManager : MonoBehaviour
{
    public ConversationManager _conversationManager = null;
    public LocalizationManager _localizationManager = null;
    public GlichManager _glichManager = null;

    public UnityEvent _endConversation;
    public UnityEvent _endPartConversation;
    public UnityEvent _showPersonaje;

    public List<GameObject> _objectsToDeactivate;
    public List<GameObject> _pistas;
    private List<bool> _pistaConocida = new List<bool>();
    public List<string> _keysBoy;
    public List<string> _keysGirl;

    [HideInInspector]
    public List<Pharse> phrasesBoy = new List<Pharse>();
    [HideInInspector]
    public List<Pharse> phrasesGirl = new List<Pharse>();

    public bool isBoy = true;

    private int _maxPalabrasCorrectas = 0;

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

        for(int i = 0; i < _pistas.Count; ++i)
        {
            _pistaConocida.Add(false);
        }

        //read boy keys
        phrasesBoy = new List<Pharse>();
        phrasesGirl = new List<Pharse>();

        for (int i = 0; i < _keysBoy.Count; ++i)
        {
            Tuple<string, string> puzzle = new Tuple<string, string>("", "");
            bool withPuzzle = false;
            string allString = _localizationManager.GetString(_keysBoy[i], ref puzzle, ref withPuzzle);

            Pharse p = new Pharse
            {
                allString = allString,
                puzzle = puzzle,
                withPuzzle = withPuzzle
            };

            if (withPuzzle)
            {
                ++_maxPalabrasCorrectas;
            }
            phrasesBoy.Add(p);
        }

        for (int i = 0; i < _keysBoy.Count; ++i)
        {
            Tuple<string, string> puzzle = new Tuple<string, string>("", "");
            bool withPuzzle = false;
            string allString = _localizationManager.GetString(_keysGirl[i], ref puzzle, ref withPuzzle);

            Pharse p = new Pharse
            {
                allString = allString,
                puzzle = puzzle,
                withPuzzle = withPuzzle
            };
            phrasesGirl.Add(p);
        }
    }

    private void Start()
    {
        ShowMensaje();
    }

    public void ShowMensaje()
    {
        List<string> actualString = new List<string>();

        if (isBoy)
        {
            for (int i = 0; i < phrasesBoy.Count; ++i)
            {
                actualString.Add(phrasesBoy[i].allString);
            }
        }
        else
        {
            for (int i = 0; i < phrasesGirl.Count; ++i)
            {
                actualString.Add(phrasesGirl[i].allString);
            }
        }
        _conversationManager.SetConversation(actualString);
        OcultarGraficos();

        if(_showPersonaje != null)
        {
            _showPersonaje.Invoke();
        }
    }

    private void OcultarGraficos()
    {
        foreach (var p in _objectsToDeactivate)
        {
            p.SetActive(false);

            ChangeMouse ms = p.GetComponent<ChangeMouse>();
            if (p != null)
            {
                ms.OnMouseExit();
            }
        }
    }

    private void MostrarGraficos()
    {
        foreach (var p in _objectsToDeactivate)
        {
            p.SetActive(true);
        }
    }

    public void EndConversation()
    {
        MostrarGraficos();
        if (_endConversation != null)
            _endConversation.Invoke();
    }

    public void EndPartConversation()
    {
        if (_endPartConversation != null)
            _endPartConversation.Invoke();
    }

    public void PistaConocida(int index)
    {
        _pistaConocida[index] = true;
    }

    public bool AllPistasConocidas()
    {
        return _pistaConocida.TrueForAll(b => b == true);
    }

    public void PulsadoElPersonaje()
    {
        OcultarGraficos();

        if (!AllPistasConocidas())
        {
            CrearGlich();
        }
        ShowMensaje();
    }

    public void CrearGlich()
    {
        _glichManager.AddLevelOfGlich();
    }

    public void EliminarGlich()
    {
        _glichManager.ReduceLevelOfGlich();
    }

    public void OnGameOver()
    {

    }
}