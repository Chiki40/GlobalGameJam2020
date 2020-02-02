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
    public AlphabetManager _alphabetManager;
    private bool inputAllreadyShow = false;
    private int _actualPalabrasCorrectas = 0;

    private void Start()
    {
        _genericManager.ShowPregame("EndText_1");
    }

    public void MostrarPersonaje()
    {
        //_genericManager.ShowMensaje();
        _actualPalabrasCorrectas = 0;

        if(_genericManager.AllPistasConocidas())
        {
            _alphabetManager.isClickable = true;
        }

    }

    public void EndConverdation()
    {
        List<Pharse> phrases = _genericManager.isBoy ? _genericManager.phrasesBoy : _genericManager.phrasesGirl;
        int numPuzzlePhrases = 0;
        for (int i = 0; i < phrases.Count; ++i)
        {
            if (phrases[i].withPuzzle)
            {
                ++numPuzzlePhrases;
            }
        }
        if (_actualPalabrasCorrectas >= numPuzzlePhrases)
        {
            _genericManager.OnLevelCompleted("EndText_1");
        }
    }

    public void EndPartConversation()
    {
        _textInput.SetActive(false);
        inputAllreadyShow = false;
        int actualPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();
        Pharse phrase = _genericManager.isBoy ? _genericManager.phrasesBoy[actualPhrase] : _genericManager.phrasesGirl[actualPhrase];
        if (phrase.withPuzzle && _genericManager.AllPistasConocidas())
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
            _genericManager.EliminarGlich();
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
        else
        {
            _genericManager.CrearGlich();
        }
        _textInput.SetActive(false);
        _genericManager._conversationManager.Block(false);
    }

    public void pulsadoEnTexto()
    {
        int actualPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();
        if (!inputAllreadyShow)
        {
            Pharse phrase = _genericManager.isBoy ? _genericManager.phrasesBoy[actualPhrase] : _genericManager.phrasesGirl[actualPhrase];
            if (phrase.withPuzzle && _genericManager.AllPistasConocidas())
            {
                _textInput.SetActive(true);
                _textInput.GetComponent<InputField>().text = "";
                inputAllreadyShow = true;
            }
        }
    }
}
