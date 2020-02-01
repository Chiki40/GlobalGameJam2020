using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DetectCorrectWordComponent : MonoBehaviour
{
    public UnityEvent _checkWord;

    public void checkWord(InputField text)
    {
        _checkWord.Invoke();
    }
}
