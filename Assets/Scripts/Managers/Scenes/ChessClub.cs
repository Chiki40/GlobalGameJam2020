using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessClub : MonoBehaviour
{
    public GenericManager _genericManager;
    public List<GameObject> _clickableComponents;
    public List<bool> _levelMustBeWinned;
    public List<GameObject> _chess;
    public List<int> desplazamientoX;
    public List<int> desplazamientoY;
    int currentLevel = 0;

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
            ++currentLevel;
            if (currentLevel < _levelMustBeWinned.Count)
            {
                EnableClickableComponent();
            }
            else
            {
                Debug.Log("te has pasado el nivel");
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
        _clickableComponents[currentLevel].GetComponent<SpriteOutline>().enabled = true;
    }

    public void PiezePress(int index, GameObject go)
    {
        if (index == 0)
        {
            Vector3 position = go.transform.position;
            Vector2 size = go.GetComponent<SpriteRenderer>().bounds.size;
            position.x += desplazamientoX[currentLevel] * go.GetComponent<SpriteRenderer>().bounds.size.x;
            position.y += desplazamientoY[currentLevel] * go.GetComponent<SpriteRenderer>().bounds.size.y;
            go.transform.position = position;
            StartCoroutine("WaitToClose");
        }
        else
        {
            _genericManager.CrearGlich();
        }
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(1);
        _chess[currentLevel].SetActive(false);
        onTextEnd = true;

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
