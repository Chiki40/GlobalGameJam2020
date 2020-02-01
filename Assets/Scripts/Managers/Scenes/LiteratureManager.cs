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

    public void EndPartConversation(int phrase)
    {
        _textInput.SetActive(false);
        inputAllreadyShow = false;
        int actualPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();
        if (_genericManager.AllPistasConocidas() && (actualPhrase == 1 || actualPhrase == 2))
        {
            _genericManager._conversationManager.Block(true);
        }
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
        _genericManager._conversationManager.Block(false);
    }

    public void pulsadoEnTexto()
    {
        if (!_genericManager.AllPistasConocidas())
        {
            return;
        }

        int actualPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();
        //Debug.Log(actualPhrase);
        if (!inputAllreadyShow)
        {
            //Debug.Log(inputAllreadyShow.ToString());
            if (actualPhrase == 1 || actualPhrase == 2)
            {
                _textInput.SetActive(true);
                _textInput.GetComponent<InputField>().text = "";
                inputAllreadyShow = true;
            }
        }
    }
}
