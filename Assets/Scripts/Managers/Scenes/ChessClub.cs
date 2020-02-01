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

    private void Start()
    {
        EnableClickableComponent();
    }

    public void MostrarNivel(int level)
    {
        for (int i = 0; i < _clickableComponents.Count; ++i)
        {
            _clickableComponents[i].GetComponent<ClickableComponent>().enabled = false;
            _clickableComponents[i].GetComponent<ChangeMouse>().enabled = false;
            _clickableComponents[i].GetComponent<SpriteOutline>().enabled = false;
        }

        _chess[currentLevel].SetActive(true);
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
            StartCoroutine("waitToClose");
        }
        else
        {
            _genericManager.CrearGlich();
        }
    }

    IEnumerator waitToClose()
    {
        yield return new WaitForSeconds(1);
        _chess[currentLevel].SetActive(false);
        ++currentLevel;
        if(currentLevel < _levelMustBeWinned.Count)
        {
            EnableClickableComponent();
        }
        else
        {
            Debug.Log("te has pasado el nivel");
        }
    }
}
