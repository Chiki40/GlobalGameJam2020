using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessClub : MonoBehaviour
{
    public GenericManager _genericManager;
    public List<GameObject> _clickableComponents;
    public List<GameObject> _chess;
    public List<GameObject> _chessEnded;

    int currentLevel = 0;
    public int maxLevels = 3;

    public List<GameObject> objetosCambioOn;
    public List<GameObject> objetosCambioOff;

    public List<string> key1In_boy;
    public List<string> key1Out_boy;
    public List<string> key2In_boy;
    public List<string> key2Out_boy;
    public List<string> key3In_boy;
    public List<string> key3Out_boy;

    public List<string> key1In_girl;
    public List<string> key1Out_girl;
    public List<string> key2In_girl;
    public List<string> key2Out_girl;
    public List<string> key3In_girl;
    public List<string> key3Out_girl;

    private bool onTextStart = false;
    private bool onTextEnd = false;

    private void Start()
    {
        EnableClickableComponent();
    }

    public void MostrarNivel(int level)
    {
        onTextStart = true;

        switch(level)
        {
            case 0: ShowMessage(_genericManager.isBoy ? key1In_boy : key1In_girl); break;
            case 1: ShowMessage(_genericManager.isBoy ? key2In_boy : key2In_girl); break;
            case 2: ShowMessage(_genericManager.isBoy ? key3In_boy : key3In_girl); break;
        }

        for (int i = 0; i < _clickableComponents.Count; ++i)
        {
            _clickableComponents[i].GetComponent<ClickableComponent>().enabled = false;
            _clickableComponents[i].GetComponent<ChangeMouse>().enabled = false;
            _clickableComponents[i].GetComponent<SpriteOutline>().enabled = false;
        }
    }

    public void OnEndConversation()
    {
        if(onTextStart)
        {
            _chess[currentLevel].SetActive(true);
            onTextStart = false;
        }

        if(onTextEnd)
        {
            onTextEnd = false;

            objetosCambioOn[currentLevel].SetActive(true);
            objetosCambioOff[currentLevel].SetActive(false);

            ++currentLevel;
            if (currentLevel < maxLevels)
            {
                EnableClickableComponent();
            }
            else
            {
                _genericManager.OnLevelCompleted();
            }
        }
    }

    private void EnableClickableComponent()
    {
        for(int i = 0;i < _clickableComponents.Count; ++i)
        {
            _clickableComponents[i].GetComponent<ClickableComponent>().enabled = false;
            _clickableComponents[i].GetComponent<ChangeMouse>().enabled = false;
            _clickableComponents[i].GetComponent<SpriteOutline>().enabled = false;
        }
        _clickableComponents[currentLevel].GetComponent<ClickableComponent>().enabled = true;
        _clickableComponents[currentLevel].GetComponent<ChangeMouse>().enabled = true;
        _clickableComponents[currentLevel].GetComponent<ChangeMouse>().OnMouseExit();
        _clickableComponents[currentLevel].GetComponent<SpriteOutline>().enabled = false;
    }

    public void PiezePress(int index, GameObject go)
    {
        if (index == 0)
        {
            _chess[currentLevel].SetActive(false);
            _chessEnded[currentLevel].SetActive(true);
            StartCoroutine("WaitToClose");
        }
        else
        {
            _genericManager.OnGameOver();
        }
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(1);
        _chess[currentLevel].SetActive(false);
        onTextEnd = true;
        _chessEnded[currentLevel].SetActive(false);
        switch (currentLevel)
        {
            case 0: ShowMessage(_genericManager.isBoy ? key1Out_boy : key1Out_girl); break;
            case 1: ShowMessage(_genericManager.isBoy ? key2Out_boy : key2Out_girl); break;
            case 2: ShowMessage(_genericManager.isBoy ? key3Out_boy : key3Out_girl); break;
        }
    }

    void ShowMessage(List<string> keys)
    {
        List<string> values = new List<string>();
        for (int i = 0; i < keys.Count; ++i)
        {
            Tuple<string, string> puzzle = new Tuple<string, string>("", "");
            bool withPuzzle = false;
            values.Add(_genericManager._localizationManager.GetString(keys[i], ref puzzle, ref withPuzzle));
        }
        _genericManager._conversationManager.SetConversation(values);
    }
}
