using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenderSelectionManager : MonoBehaviour
{

    [SerializeField]
    private float _timeBetweenSounds = 5.0f;
    private void Start()
    {
        UtilSound.instance.PlaySound("INTRIGANTE", 1.0f, true);
        StartCoroutine(PlaySoundCoroutine());
    }

    private IEnumerator PlaySoundCoroutine()
    {
        while (true)
        {
            UtilSound.instance.PlaySound("bath");
            yield return new WaitForSeconds(_timeBetweenSounds);
            UtilSound.instance.PlaySound("bathLong");
            yield return new WaitForSeconds(_timeBetweenSounds / 2.0f);
            UtilSound.instance.PlaySound("bath");
            yield return new WaitForSeconds(_timeBetweenSounds / 2.0f);
        }
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
