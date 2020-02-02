using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenderSelectionManager : MonoBehaviour
{

    [SerializeField]
    private float _timeBetweenSounds = 5.0f;
    [SerializeField]
    private float _timeToStart = 3.0f;
    [SerializeField]
    private Animator _fadeOutAnimator = null;    
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
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        _fadeOutAnimator.enabled = true;
        UtilSound.instance.PlaySound("door");
        yield return new WaitForSeconds(_timeToStart);
        SceneManager.LoadScene("Initial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
