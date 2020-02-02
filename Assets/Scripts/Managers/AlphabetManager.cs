using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetManager : MonoBehaviour
{
    private List<GameObject> alphabets;
    private int currentAlphabet;
    public bool isClickable = false;
    // Start is called before the first frame update
    void Start()
    {
        int totalHijos = transform.childCount;
        alphabets = new List<GameObject>();

        for(int i = 0; i< totalHijos; ++i)
        {
            alphabets.Add(transform.GetChild(i).gameObject);
        }

        foreach (var a in alphabets)
        {
            a.SetActive(false);
        }
        alphabets[0].SetActive(true);
        currentAlphabet = 0;
    }

    void IncreaseAlphabet()
    {
        if (!isClickable) return;
        foreach (var a in alphabets)
        {
            a.SetActive(false);
        }
        currentAlphabet = (currentAlphabet + 1) % alphabets.Count;
        alphabets[currentAlphabet].SetActive(true);
    }

    public void OnMouseUp()
    {
        IncreaseAlphabet();
    }

    void resetAlphabet()
    {
        foreach (var a in alphabets)
        {
            a.SetActive(false);
        }
        currentAlphabet = 0;
        alphabets[currentAlphabet].SetActive(true);
    }
}
