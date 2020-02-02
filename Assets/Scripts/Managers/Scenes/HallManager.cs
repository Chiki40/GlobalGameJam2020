using System.Collections;
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
    public List<GameObject> botones;
    public List<ListString> opciones;
    public List<int> correctOption;
    public GameObject _objetableButton;
    public List<GameObject> _cartelesGrandes;
    int actualCorrectas = 0;

    private void Start()
    {
        _genericManager.ShowPregame("EndText_3");
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
        if (_genericManager.AllPistasConocidas())
        {
            int index = _genericManager._conversationManager.getCurrentPhraseIndex();
            if (index >= 0 && index < 6)
            {
                _objetableButton.SetActive(true);
            }
            else
            {
                _objetableButton.SetActive(false);
            }
        }
        else
        {
            _objetableButton.SetActive(false);
        }
        HideButtons();
    }

    public void ShowPersonaje()
    {
        actualCorrectas = 0;
    }

    public void EndPhrases()
    {
        if (actualCorrectas >= maxLevels)
        {
            _genericManager.OnLevelCompleted("EndText_3");
        }
    }

    public void PressCartelGrande()
    {
        foreach(var v in _cartelesGrandes)
        {
            v.SetActive(false);
        }
    }

    public void PistaSeleccionada(int index)
    {
        _cartelesGrandes[index].SetActive(true);
    }
}
