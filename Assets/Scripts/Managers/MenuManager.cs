using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _exitButton = null;

    private void Awake()
    {
        if (_exitButton == null)
        {
             Debug.LogError("[MenuManager.Awake] ERROR: Serializable _exitButton not set");
            return;
        }

        #if !UNITY_EDITOR
            _exitButton.SetActive(true);
        #endif
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GenderSelection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
