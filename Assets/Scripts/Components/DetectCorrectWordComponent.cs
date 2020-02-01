﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DetectCorrectWordComponent : MonoBehaviour
{
    public string m_wordToCheck;
    public UnityEvent wordIsCorrect;
    public UnityEvent wordIsIncorrect;

    public void checkWord(InputField text)
    {
        if (text.text == m_wordToCheck)
            wordIsCorrect.Invoke();
        else
            wordIsIncorrect.Invoke();
    }

}