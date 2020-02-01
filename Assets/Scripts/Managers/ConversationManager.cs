using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialogTextBox = null;
    [SerializeField]
    private Text _dialogText = null;
    [SerializeField]
    private float _timeBetweenShownChars = 0.1f;
    private List<string> _currentConversationTexts = null;
    private int _currentConversationIndex = -1;
    private int _currentConversationShownChar = 0;
    private float _timeRemainingForNextShownChar = 0.0f;

    private void Awake()
    {
        _currentConversationIndex = -1;
        if (_dialogText == null)
        {
             Debug.LogError("[ConversationManager.Awake] ERROR: Serializable _dialogText not set");
            return;
        }
        if (_dialogTextBox == null)
        {
             Debug.LogError("[ConversationManager.Awake] ERROR: Serializable _dialogTextBox not set");
            return;
        }
    }

    public void SetConversation(List<string> conversationTexts)
    {
        Debug.Log("lolazo");
        if (_dialogTextBox != null)
        {
            _dialogTextBox.gameObject.SetActive(true);
        }
        _currentConversationTexts = conversationTexts;
        _currentConversationIndex = -1;
        NextMessage();
    }

    public void EndConversation()
    {
        if (_dialogTextBox != null)
        {
            _dialogTextBox.gameObject.SetActive(false);
        }
    }

    public void NextMessage()
    {
        if (_dialogText != null && _dialogTextBox != null && _dialogTextBox.activeInHierarchy && _dialogText.enabled && _currentConversationTexts != null)
        {
            if (_currentConversationIndex < _currentConversationTexts.Count - 1)
            {
                ++_currentConversationIndex;
                _currentConversationShownChar = 0;
                _timeRemainingForNextShownChar = _timeBetweenShownChars;
                if (_currentConversationTexts[_currentConversationIndex].Length > 0)
                {
                    _dialogText.text = _currentConversationTexts[_currentConversationIndex][0].ToString();
                }
            }
            else
            {
                EndConversation();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_dialogText != null && _dialogTextBox != null && _dialogTextBox.activeInHierarchy && _currentConversationTexts != null && _currentConversationIndex < _currentConversationTexts.Count)
        {
            if (_currentConversationShownChar < _currentConversationTexts[_currentConversationIndex].Length - 1)
            {
                _timeRemainingForNextShownChar -= Time.deltaTime;
                if (_timeRemainingForNextShownChar <= 0.0f)
                {
                    ++_currentConversationShownChar;
                    _timeRemainingForNextShownChar = _timeBetweenShownChars;
                    _dialogText.text = _currentConversationTexts[_currentConversationIndex].Substring(0, _currentConversationShownChar + 1);   
                }
            }
        }
    }
}
