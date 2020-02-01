using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private string[] _levels;
    private Dictionary<string, bool> _completedLevels = new Dictionary<string, bool>();
    private string _currentLevel = null;
    private static GameController _instance = null;
    public static GameController GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            if (_levels == null || _levels.Length == 0)
            {
                Debug.LogError("[GameController.Awake] ERROR: Serializable _levels notset");
                return;
            }
            for (int i = 0; i < _levels.Length; ++i)
            {
                _completedLevels.Add(_levels[i], false);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void StartLevel(string level)
    {
        if (_currentLevel == null)
        {
            _currentLevel = level;
            SceneManager.LoadScene(level);
        }
    }

    public void LevelCompleted()
    {
        _completedLevels[_currentLevel] = true;
        _currentLevel = null;

        int levelsCompleted = 0;
        foreach(KeyValuePair<string, bool> pair in _completedLevels)
        {
            if (pair.Value)
            {
                ++levelsCompleted;
            }
        }
        if (levelsCompleted >= _levels.Length)
        {
            Victory();
        }
    }

    public void LevelGameOver()
    {
        Debug.Log("GAME OVER!");
        _currentLevel = null;
    }

    public void Victory()
    {
        Debug.Log("VICTORY!");
    }
}
