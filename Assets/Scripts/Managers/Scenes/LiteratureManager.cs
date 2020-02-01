using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public struct Pharse
{
    public string allString;
    public Tuple<string, string> puzzle;
    public bool withPuzzle;
}

public class LiteratureManager : MonoBehaviour
{
    public GenericManager _genericManager;
    public TMPro.TextMeshProUGUI _textOutput;
    public GameObject _textInput;
    private bool inputAllreadyShow = false;
    private int _actualPalabrasCorrectas = 0;

    public void MostrarPersonaje()
    {
        //_genericManager.ShowMensaje();
        _actualPalabrasCorrectas = 0;
    }

    public void EndPartConversation()
    {
        _textInput.SetActive(false);
        inputAllreadyShow = false;
    }

    public void EndAllConversation()
    {
        int a = 2;
        ++a;
    }

    public void CheckPalabra()
    {
        int actualPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();
        InputField field = _textInput.GetComponent<InputField>();
        string value = field.text;
        string texto = _textOutput.text;

        if (value.ToLower() == _genericManager.phrasesBoy[actualPhrase].puzzle.Item2.ToLower())
        {
            if (_genericManager.isBoy)
            {
                //la palabra esta bien
                texto = texto.Replace(_genericManager.phrasesBoy[actualPhrase].puzzle.Item1, _genericManager.phrasesBoy[actualPhrase].puzzle.Item2.ToLower());
                _textOutput.text = texto;
                ++_actualPalabrasCorrectas;
            }
            else
            {
                //la palabra esta bien
                texto = texto.Replace(_genericManager.phrasesGirl[actualPhrase].puzzle.Item1, _genericManager.phrasesGirl[actualPhrase].puzzle.Item2.ToLower());
                _textOutput.text = texto;
                ++_actualPalabrasCorrectas;
            }
        }
        _textInput.SetActive(false);
    }

    public void pulsadoEnTexto()
    {
        int actualPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();

        if (!inputAllreadyShow)
        {
            if (actualPhrase == 1)
            {
                _textInput.SetActive(true);
                _textInput.GetComponent<InputField>().text = "";
                inputAllreadyShow = true;
            }

            if (actualPhrase == 2)
            {
                _textInput.SetActive(true);
                _textInput.GetComponent<InputField>().text = "";
                inputAllreadyShow = true;
            }
        }
    }

    /*
    [SerializeField]
    private ConversationManager _conversationManager = null;
    [SerializeField]
    private LocalizationManager _localizationManager = null;

    public GameObject _personaje;
    public List<GameObject> _pistas;
    public GameObject _textInput;
    public TMPro.TextMeshProUGUI _textOutput;
    private List<bool> _pistaConocida;
    private int _numGlich = 0;
    private int _actualPalabrasCorrectas = 0;
    private int _maxPalabrasCorrectas = 0;
    private bool _allPistas = false;

    public List<string> _keysBoy;
    public List<string> _keysGirl;

    private List<Pharse> phrasesBoy;
    private List<Pharse> phrasesGirl;

    public bool isBoy = true;

    private bool inputAllreadyShow = false;
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
        phrasesBoy = new List<Pharse>();
        phrasesGirl = new List<Pharse>();

        for(int i = 0; i < _keysBoy.Count; ++i)
        {
            Tuple<string, string> puzzle = new Tuple<string, string>("","");
            bool withPuzzle = false;
            string allString = _localizationManager.GetString(_keysBoy[i], ref puzzle, ref withPuzzle);

            Pharse p = new Pharse
            {
                allString = allString,
                puzzle = puzzle,
                withPuzzle = withPuzzle
            };

            if(withPuzzle)
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
        _pistaConocida = new List<bool>();
        for(int i = 0;i < _pistas.Count; ++i)
        {
            _pistaConocida.Add(false);
        }
        ShowMensaje();
    }

    public void ShowMensaje()
    {
        List<string> actualString = new List<string>();

        if(isBoy)
        {
            for(int i = 0; i < phrasesBoy.Count; ++i)
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
        OcultarPersonaje();
    }

    private void OcultarPersonaje()
    {
        ChangeMouse ms = _personaje.GetComponent<ChangeMouse>();
        ms.OnMouseExit();
        _personaje.SetActive(false);
    }

    private void MostrarPersonaje()
    {
        _personaje.SetActive(true);
    }

    public void EndConversation()
    {
        MostrarPersonaje();

        if(_maxPalabrasCorrectas == _actualPalabrasCorrectas)
        {
            Debug.Log("nivel pasado");
        }
    }
    public void EndPartConversation()
    {
        _textInput.SetActive(false);
        inputAllreadyShow = false;
        /*
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

    public void CheckPalabra()
    {
        int actualPhrase = _conversationManager.getCurrentPhraseIndex();
        InputField field = _textInput.GetComponent<InputField>();
        string value = field.text;
        string texto = _textOutput.text;

        if (value.ToLower() == phrasesBoy[actualPhrase].puzzle.Item2.ToLower())
        {
            if (isBoy)
            {
                //la palabra esta bien
                texto = texto.Replace(phrasesBoy[actualPhrase].puzzle.Item1, phrasesBoy[actualPhrase].puzzle.Item2.ToLower());
                _textOutput.text = texto;
                ++_actualPalabrasCorrectas;
            }
            else
            {
                //la palabra esta bien
                texto = texto.Replace(phrasesGirl[actualPhrase].puzzle.Item1, phrasesGirl[actualPhrase].puzzle.Item2.ToLower());
                _textOutput.text = texto;
                ++_actualPalabrasCorrectas;
            }
        }
        _textInput.SetActive(false);
    }   

    public void pulsadoEnTexto()
    {
        int actualPhrase = _conversationManager.getCurrentPhraseIndex();

        if (!inputAllreadyShow)
        {
            if (actualPhrase == 1)
            {
                _textInput.SetActive(true);
                _textInput.GetComponent<InputField>().text = "";
                inputAllreadyShow = true;
            }

            if (actualPhrase == 2)
            {
                _textInput.SetActive(true);
                _textInput.GetComponent<InputField>().text = "";
                inputAllreadyShow = true;
            }
        }
    }

    /*
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
        OcultarPersonaje();

        _allPistas  = _pistaConocida.TrueForAll(b => b == true);

        if (!_allPistas)
        {
            //CrearGlich();
        }
        else
        {
            _actualPalabrasCorrectas = 0;
        }
        ShowMensaje();
    }
    */
}
