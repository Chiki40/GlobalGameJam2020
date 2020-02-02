using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessClub : MonoBehaviour
{
    public GenericManager _genericManager;
    public List<GameObject> _clickableComponents;
    public List<GameObject> _chess;
    public List<GameObject> _chessPreGame;
    public List<GameObject> _chessPostGame;
    public List<GameObject> _fondos;
    public List<Sprite> _spritesFlores;
    public Image _imageFlores;

    int currentLevel = 0;
    private int maxLevels = 3;

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
        _fondos[0].SetActive(true);
        _fondos[1].SetActive(false);
        maxLevels = _chessPostGame.Count;
    }

    public void MostrarNivel(int level)
    {
        onTextStart = true;

        _imageFlores.sprite = _spritesFlores[currentLevel * 2];
        switch (level)
        {
            case 0: ShowMessage(_genericManager.isBoy ? key1In_boy : key1In_girl); break;
            case 1: ShowMessage(_genericManager.isBoy ? key2In_boy : key2In_girl); break;
            case 2: ShowMessage(_genericManager.isBoy ? key3In_boy : key3In_girl); break;
        }

        for (int i = 0; i < _clickableComponents.Count; ++i)
        {
            _clickableComponents[i].GetComponent<ClickableComponent>().enabled = false;
            _clickableComponents[i].GetComponent<ChangeMouse>().OnMouseExit();
            _clickableComponents[i].GetComponent<ChangeMouse>().enabled = false;
            _clickableComponents[i].GetComponent<SpriteOutline>().enabled = false;
            _clickableComponents[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void OnEndConversation()
    {
        if(onTextStart)
        {
            _chess[currentLevel].SetActive(true);
            _chessPreGame[currentLevel].SetActive(true);
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
                _genericManager.OnLevelCompleted("EndText_2");
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
        _clickableComponents[currentLevel].GetComponent<Collider2D>().enabled = true;
        _clickableComponents[currentLevel].GetComponent<ChangeMouse>().OnMouseExit();
        _clickableComponents[currentLevel].GetComponent<SpriteOutline>().enabled = false;
    }

    public void PiezePress(bool win)
    {
        if (win)
        {
            UtilSound.instance.PlaySound("chess");
            _chessPreGame[currentLevel].SetActive(false);
            _chessPostGame[currentLevel].SetActive(true);
            _genericManager.EliminarGlich();
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
        _chessPreGame[currentLevel].SetActive(false);
        onTextEnd = true;
        _chessPostGame[currentLevel].SetActive(false);

        _fondos[0].SetActive(false);
        _fondos[1].SetActive(true);

        _imageFlores.sprite = _spritesFlores[currentLevel * 2 + 1];
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
