﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ListString
{
    public List<string> lista;
}

public class HallManager : MonoBehaviour
{
    public GenericManager _genericManager;
    public int maxLevels;
    public List<bool> isObjetable;
    public List<GameObject> botones;
    public List<ListString> opciones;
    public List<int> correctOption;
    public GameObject _objetableButton;
    int actualCorrectas = 0;

    private void Start()
    {
        HideButtons();
        ShowObjectionButton();
    }

    public void Objection()
    {
        for(int index = 0; index < botones.Count; ++index)
        {
            GameObject b = botones[index];
            b.SetActive(true);
            int indexPhrase = _genericManager._conversationManager.getCurrentPhraseIndex();
            b.GetComponentInChildren<Text>().text = opciones[indexPhrase].lista[index];
        }
    }

    public void ButtonClicked(int index)
    {
        int ActualLevel = _genericManager._conversationManager.getCurrentPhraseIndex();
        if (_genericManager.AllPistasConocidas())
        {
            if (correctOption[ActualLevel] == index)
            {
                ++actualCorrectas;
            }
            _genericManager.EliminarGlich();
        }
        else
        {
            _genericManager.CrearGlich();
        }
        HideButtons();
    }

    private void HideButtons()
    {
        for (int index = 0; index < botones.Count; ++index)
        {
            GameObject b = botones[index];
            b.SetActive(false);
            b.GetComponentInChildren<Text>().text = "";
        }
    }

    public void ShowObjectionButton()
    {
        int index = _genericManager._conversationManager.getCurrentPhraseIndex();
        if (index >= 0)
        {
            if (isObjetable[index])
            {
                _objetableButton.SetActive(true);
            }
            else
            {
                _objetableButton.SetActive(false);
            }
        }
    }

    public void ShowPersonaje()
    {
        actualCorrectas = 0;
    }

    public void EndPhrases()
    {
        if (actualCorrectas >= maxLevels)
        {
            Debug.Log("ya he acabado el nivel");
        }
    }
}
