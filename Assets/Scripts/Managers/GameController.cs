using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float _timeToExitLevels = 3.0f;    
    [SerializeField]
    private string[] _levels;
    private Dictionary<string, bool> _completedLevels = new Dictionary<string, bool>();
    private string _currentLevel = null;
    private bool _alreadyLost = false;
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

    public void Reset()
    {
        for (int i = 0; i < _levels.Length; ++i)
        {
            _completedLevels[_levels[i]] = false;
        }
    }

    public void StartLevel(string level)
    {
        if (_currentLevel == null)
        {
            _alreadyLost = false;
            _currentLevel = level;
            SceneManager.LoadScene(level);
        }
    }

    public int GetLevelsCompleted()
    {
        int levelsCompleted = 0;
        foreach(KeyValuePair<string, bool> pair in _completedLevels)
        {
            if (pair.Value)
            {
                ++levelsCompleted;
            }
        }
        return levelsCompleted;
    }

    public bool HasCompletedLevel(string level)
    {
        return _completedLevels[level];
    }

    public void LevelCompleted()
    {
        _completedLevels[_currentLevel] = true;
        _currentLevel = null;

        int levelsCompleted = GetLevelsCompleted();
        if (levelsCompleted >= _levels.Length)
        {
            Victory();
        }
        else
        {
            StartCoroutine(LevelCompletedCoroutine());
        }
    }

    private IEnumerator LevelCompletedCoroutine()
    {
        yield return new WaitForSeconds(_timeToExitLevels);
        SceneManager.LoadScene("Initial");
    }

    public void LevelGameOver()
    {
        if (!_alreadyLost)
        {
            _alreadyLost = true;
            Debug.Log("GAME OVER!");
            StartCoroutine(LostCoroutine());            
        }
    }    

    public void Victory()
    {
        Debug.Log("VICTORY!");
        StartCoroutine(WonCoroutine());
    }

    private IEnumerator WonCoroutine()
    {
        yield return new WaitForSeconds(_timeToExitLevels);
        SceneManager.LoadScene("Final Scene");
    }

    private IEnumerator LostCoroutine()
    {
        yield return new WaitForSeconds(_timeToExitLevels);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _alreadyLost = false;
    }      
}
