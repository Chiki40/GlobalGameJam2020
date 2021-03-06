﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    private float _timeInMenu = 5.0f;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        GameController.GetInstance().Reset();
        yield return new WaitForSeconds(_timeInMenu);
        GoToNextScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GoToNextScene();
        }
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene("GenderSelection");
    }
}
