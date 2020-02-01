using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenderSelectionManager : MonoBehaviour
{
    private void Start()
    {
        UtilSound.instance.PlaySound("INTRIGANTE", 1.0f, true);
    }

    public void SelectBoy()
    {
        StartGame();
    }

    public void SelectGirl()
    {
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Initial");
    }
}
